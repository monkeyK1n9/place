# Users

Utilisez l’API REST pour obtenir des informations publiques et privées sur les utilisateurs authentifiés.

## Get authenticated user

Provide user info

### Request url

> **GET:** /user

### Code exameple

```bash
curl -L \
fetch('https://api.place.com/user', {
    headers: {
        'Accept': 'application/application/json',
        'Authorization': 'Bearer <YOUR-TOKEN>',
        'X-Place-Api-Version': '2023-11-28'
    }
})

````

### Respnse schema

```json
{
    "login": "johdoe@gmail.com",
    "firstName": "John",
    "lastName": "Done",
    "emailConfirmed": false,
    "username": "zgenjirusuperuser"
}
```

### HTTP Status code for the resource

| Status code | Description             |
|-------------|-------------------------|
| 200         | Ok                      |
| 304         | Not modified            |
| 401         | Requires authentication |
| 403         | Forbidden               |

<br/>
<br/>

## Register new user

Register as new user inside the system to be authenticated.

### Request url

> **POST:** /register

### Code exameple

```javascript
fetch('https://api.place.com/user', {
    headers: {
        'Accept': 'application/application/json',
        'Authorization': 'Bearer <YOUR-TOKEN>',
        'X-Place-Api-Version': '2023-11-28',
        'Content-type': 'application/json'
    },
    body: {
        "email": "chirsdoe@gmail.com",
        "password": "!MySuperPaswword",
        "confirmPassword": "!MySuperPaswword"
    }
})

````

### Respnse schema

```json
{
    "login": "chirsdoe@gmail.com",
    "firstName": null,
    "lastName": null,
    "emailConfirmed": false,
    "username": "@chirst502"
}
```

### Request parameters

| Name                                       | Type   | Description   |
|--------------------------------------------|--------|---------------|
| <code>Email</code> **required**            | string | User email    |
| <code>Password</code> **required**         | string | User password |
| <code>CqonfirmPassword</code> **required** | string | User password |

Setting to application/json

### HTTP Status code for the resource

| Status code | Description      |
|-------------|------------------|
| 201         | Created          |
| 403         | Forbidden        |
| 422         | Validation error |
