namespace Place.Api.Infrastructure.Cryptography;

using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using Application.Common.Cryptography;
using Domain.Authentication.ValueObjects;
using Domain.Services;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

/// <summary>
/// Represents the password hasher, used for hashing passwords and verifying hashed passwords.
/// </summary>
internal sealed class PasswordHasher : IPasswordHasher, IPasswordHashChecker, IDisposable
{
    private const KeyDerivationPrf Prf = KeyDerivationPrf.HMACSHA256;
    private const int IterationCount = 10000;
    private const int NumberOfBytesRequested = 256 / 8;
    private const int SaltSize = 128 / 8;
    private readonly RandomNumberGenerator rng;

    /// <summary>
    /// Initializes a new instance of the <see cref="PasswordHasher"/> class.
    /// </summary>
    public PasswordHasher() => this.rng = RandomNumberGenerator.Create();

    /// <inheritdoc />
#pragma warning disable CA1822
    public string HashPassword(Password password)
#pragma warning restore CA1822
    {
        string hashedPassword = Convert.ToBase64String(this.HashPasswordInternal(password));

        return hashedPassword;
    }

    /// <inheritdoc />
    public bool HashesMatch(string passwordHash, string providedPassword)
    {
        ArgumentNullException.ThrowIfNull(passwordHash);

        ArgumentNullException.ThrowIfNull(providedPassword);

        byte[] decodedHashedPassword = Convert.FromBase64String(passwordHash);

        if (decodedHashedPassword.Length == 0)
        {
            return false;
        }

        bool verified = VerifyPasswordHashInternal(decodedHashedPassword, providedPassword);

        return verified;
    }

    /// <inheritdoc />
    public void Dispose() => this.rng.Dispose();

    /// <summary>
    /// Returns the bytes of the hash for the specified password.
    /// </summary>
    /// <param name="password">The password to be hashed.</param>
    /// <returns>The bytes of the hash for the specified password.</returns>
    private byte[] HashPasswordInternal(string password)
    {
        byte[] salt = this.GetRandomSalt();

        byte[] subKey = KeyDerivation.Pbkdf2(password, salt, Prf, IterationCount, NumberOfBytesRequested);

        byte[] outputBytes = new byte[salt.Length + subKey.Length];

        Buffer.BlockCopy(salt, 0, outputBytes, 0, salt.Length);

        Buffer.BlockCopy(subKey, 0, outputBytes, salt.Length, subKey.Length);

        return outputBytes;
    }

    /// <summary>
    /// Gets a randomly generated salt.
    /// </summary>
    /// <returns>The randomly generated salt.</returns>
    private byte[] GetRandomSalt()
    {
        byte[] salt = new byte[SaltSize];

        this.rng.GetBytes(salt);

        return salt;
    }

    /// <summary>
    /// Verifies the bytes of the hashed password with the specified password.
    /// </summary>
    /// <param name="hashedPassword">The bytes of the hashed password.</param>
    /// <param name="password">The password to verify with.</param>
    /// <returns>True if the hashes match, otherwise false.</returns>
    private static bool VerifyPasswordHashInternal(byte[] hashedPassword, string password)
    {
        try
        {
            byte[] salt = new byte[SaltSize];

            Buffer.BlockCopy(hashedPassword, 0, salt, 0, salt.Length);

            int subKeyLength = hashedPassword.Length - salt.Length;

            if (subKeyLength < SaltSize)
            {
                return false;
            }

            byte[] expectedSubKey = new byte[subKeyLength];

            Buffer.BlockCopy(hashedPassword, salt.Length, expectedSubKey, 0, expectedSubKey.Length);

            byte[] actualSubKey = KeyDerivation.Pbkdf2(password, salt, Prf, IterationCount, subKeyLength);

            return ByteArraysEqual(actualSubKey, expectedSubKey);
        }
        catch (ArgumentNullException)
        {
            return false;
        }
        catch (ArgumentOutOfRangeException)
        {
            return false;
        }
    }

    /// <summary>
    /// Returns true if the specified byte arrays are equal, otherwise false.
    /// </summary>
    /// <param name="a">The first byte array.</param>
    /// <param name="b">The second byte array.</param>
    /// <returns>True if the arrays are equal, otherwise false.</returns>
    private static bool ByteArraysEqual(IReadOnlyList<byte>? a, IReadOnlyList<byte>? b)
    {
        if (a == null && b == null)
        {
            return true;
        }

        if (a == null || b == null || a.Count != b.Count)
        {
            return false;
        }

        bool areSame = true;

        for (int i = 0; i < a.Count; i++)
        {
            areSame &= a[i] == b[i];
        }

        return areSame;
    }
}
