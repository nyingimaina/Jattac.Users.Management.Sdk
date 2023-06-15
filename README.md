# Jattac Users Management Sdk

The AuthenticationManager NuGet package provides a set of convenient methods for user authentication and token validation.

## Features

- Easy to use.
- Sign in
- JWT token validity checking
- JWT token expiry information retrieval

## Installation

You can install the Jattac Users Management Sdk package via the NuGet Package Manager or by using the dotnet CLI.

### Package Manager

```shell
PM> Install-Package Jattac.Users.Management.Sdk
```

### .NET CLI

```shell
dotnet add package Jattac.Users.Management.Sdk
```

## Usage

### Configuration

In Program.cs, setup the global static configuration as follows:

```csharp

using Jattac.Users.Management.Sdk.Configuration;

namespace MyApp
{
    class Program
    {
        static void Main(string[] args)
        {
            JattacUserManagementConfigurationManager.Configure(new ConfigurationSettings
            {
                BaseUrl = "http://localhost:5141",
                RocketJwtSecretProvider = new SecretProvider(), // For more on creating your secret provider, see https://github.com/rocket-libs/Rocket.Libraries.Auth#2-configure-your-dotnet-app
            });
        }
    }
}

```

### Signing In

With your username, password and tenantId handy, signing in is as simple as follow snippet

```csharp
// at the top of your class file
using Jattac.Users.Management.Sdk.Users;


// your sign in code
public async Task<string> GetSignedInToken()
{
    var signInResponse = await authenticator.SignInAsync(
        username:"test@example.com", // replace with your username
        password: "test", // replace with your password
        tenantId: new Guid("2dfabe0a-e370-11ed-92a5-0242ac120002") // replace with your tenantId
    );

    return signInResponse.Token;
}

```

On successful sign in the an object of type `SignInResponse` is returned and in its property called `Token` is the token you've been issued.

### Checking a token's validity

This functionality checks that the token is both valid and it has not expired. The snippet below shows how this is done.

```csharp
// at the top of your class file
using Jattac.Users.Management.Sdk.Users;
using Rocket.Libraries.Auth;


// your token validity checking code
public async Task<bool> CheckTokenValidity(string token)
{
    try
    {

        var authenticator = new AuthenticationManager();
        var tokenIsValid = await authenticator.TokenIsValid(signInResponse.Token);
        return tokenIsValid;
    }
    catch(RocketJwtException exception)
    {
        // Examine both the 'Message' and 'ErrorKey' properties for information on what went wrong with token validation.
    }

}

```

### Checking a token's expiry date

IMPORTANT: This method simply checks if the token has expired or not, it **DOES NOT** check if the token is valid.
The main use case for this method is in situations where your previously established that a token is valid, but you now need
a fast way of checking if it has expired or not.

This method will save you a call to the Jattac Users Management server an will check to token's expiry right in your program without
making any external network calls.

```csharp
// at the top of your class file
using Jattac.Users.Management.Sdk.Users;


// sample token expiry date fetcher
public DateTimeOffset GetExpiryDate(string token)
{
    var rocketJwtTokenDecoder = new RocketJwtTokenDecoder(
        JattacUserManagementConfigurationManager.configurationSettings.RocketJwtSecretProvider // Configured globally at program start up
    );
    var tokenDescription = rocketJwtTokenDecoder.GetTokenDescription(token);
    return tokenDescription.Expires;
}

```

## License

This project is licensed under the [MIT License] (https://opensource.org/license/mit/).

## Contributions

Contributions to this project are welcome. If you find any issues or have suggestions for improvements, please open an issue or submit a pull request on the GitHub repository.
