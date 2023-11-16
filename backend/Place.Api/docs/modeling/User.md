# User

#### User Object

User is an object representing an Place account.

#### Public fields

Use these fields to specify information about an User. For publicly User, this information can be retrieved when the object is requested by the authenticated user.

<table>
   <thead>
      <tr>
         <th>Field</th>
         <th>Property</th>
         <th>Required</th>
         <th>Description</th>
      </tr>
   </thead>
   <tbody>
      <tr>
         <td>
            <code>Username</code>
         </td>
         <td>
            <code>string</code>
         </td>
         <td>
            <code>false</code>
         </td>
         <td>Username unique identifier. The username is auto generated for the registration process, but can be modified later.</td>
      </tr>
      <tr>
         <td>
            <code>Email</code>
         </td>
         <td>
            <code>string</code>
         </td>
         <td>
            <code>true</code>
         </td>
         <td>User email address. </td>
      </tr>
      <tr>
         <td>
            <code>FirstName</code>
         </td>
         <td>
            <code>string</code>
         </td>
         <td>
            <code>false</code>
         </td>
         <td>User first name. </td>
      </tr>
      <tr>
         <td>
            <code>LastName</code>
         </td>
         <td>
            <code>string</code>
         </td>
         <td>
            <code>false</code>
         </td>
         <td>User last name. </td>
      </tr>
      <tr>
         <td>
            <code>IsPublic</code>
         </td>
         <td>
            <code>boolean</code>
         </td>
         <td>
            <code>true</code>
         </td>
         <td>Make the user profile available for external</td>
      </tr>
   </tbody>
</table>


#### Private fields
Use these fields to specify properties of an User that are only available for him.

<table>
   <thead>
      <tr>
         <th>Field</th>
         <th>Property</th>
         <th>Required</th>
         <th>Description</th>
      </tr>
   </thead>
   <tbody>
      <tr>
         <td>
            <code>Password</code>
         </td>
         <td>
            <code>string</code>
         </td>
         <td>
            <code>true</code>
         </td>
         <td>User password</td>
      </tr>
   </tbody>
</table>



## Methods

User public methods allowed for user interaction


> Change username

Update the username of the authenticated user. The username should be unique in the system

<br/>


Method definition : 
```csharp
public void ChangeName(string name)
```

<table>
<thead>
<tr>
<th>
Parameters
</th>
<th>
Type
</th>
<th>
Description
</th>
<th>
Constraints
</th>
</tr>
</thead>
<tbody>
<tr>
<td>
<code>name</code>
</td>
<td>
<code lang="csharp">string</code>
</td>
<td>
The new username
</td>
<td>
<ul>
<li>
Could not be null or empty string
</li>
<li>
Minimum length 3 characters
</li>
</ul> 
</td>
</tr>
</tbody>
</table>


<br/>
<br/>


> Change First name

Update the first name of the authenticated user.

<br/>


Method definition :
```csharp
public void ChangeFristName(string name)
```

<table>
<thead>
<tr>
<th>
Parameters
</th>
<th>
Type
</th>
<th>
Description
</th>
<th>
Constraints
</th>
</tr>
</thead>
<tbody>
<tr>
<td>
<code>name</code>
</td>
<td>
<code lang="csharp">string</code>
</td>
<td>
The new first name.
</td>
<td>
<ul>
<li>
Could not be null or empty string
</li>
<li>
Minimum length 3 characters
</li>
</ul> 
</td>
</tr>
</tbody>
</table>


<br/> <br/>

> Change Password

Changes the user password.

<br/>

Method definition :
```csharp
public void ChangePassword(string passwordHash)
```

<table>
<thead>
<tr>
<th>
Parameters
</th>
<th>
Type
</th>
<th>
Description
</th>
<th>
Constraints
</th>
</tr>
</thead>
<tbody>
<tr>
<td>
<code>passwordHash</code>
</td>
<td>
<code lang="csharp">string</code>
</td>
<td>
The password hash of the new password.
</td>
<td>
<ul>
<li>
Could not be null or empty string
</li>
<li>
Minimum length 3 characters
</li>
</ul> 
</td>
</tr>
</tbody>
</table>

<br/> <br/>

> Verify password hash

Verifies that the provided password hash matches the password hash.

<br/>

Method definition :
```csharp
public bool VerifyPasswordHash(string password, IPasswordHashChecker passwordHashChecker)
```

<table>
<thead>
<tr>
<th>
Parameters
</th>
<th>
Type
</th>
<th>
Description
</th>
<th>
Constraints
</th>
</tr>
</thead>
<tbody>
<tr>
<td>
<code>password</code>
</td>
<td>
<code lang="csharp">string</code>
</td>
<td>
The password to be checked against the user password hash.
</td>
<td>
<ul>
<li>
Could not be null or empty string
</li>
<li>
The password hashes should match
</li>
</ul> 
</td>
</tr>
<tr>
<td>
<code>passwordHashChecker</code>
</td>
<td><code>IPasswordChecker</code></td>
<td>The password hash checker.</td>
<td></td>
</tr>
</tbody>
</table>
