# Login
<img src="https://img.shields.io/badge/community-driven-brightgreen.svg"/> <br>

This sample application demonstrates how to use the Auth0 OIDC Client in a Xamarin Forms application when using .NET Standard. You will need to refer to the normal [Xamarin Quickstart](https://auth0.com/docs/quickstart/native/xamarin) to see how to configure the configure the Callback URL and also how to handle the callback URL in both iOS and Android. 

For UWP, you will need to refer to the [UWP Quickstart](https://auth0.com/docs/quickstart/native/windows-uwp-csharp).

This repo is supported and maintained by Community Developers, not Auth0. For more information about different support levels check https://auth0.com/docs/support/matrix .

## Getting started

### Installation

Ensure that you have installed the correct Nuget packages for each of your platforms as per the Quickstarts linked to above. You will also need to add the `Auth0.OidcClient.Core` Nuget package to the .NET Standard library which contains your Xamarin Forms pages.

## Usage

* In the .NET Standard project, declare an `IAuthenticationService` interface with a single `Authenticate` method.

```csharp
public interface IAuthenticationService
{
Task<LoginResult> Authenticate();
}
```

* This interface should then be implemented for each of the individual platforms. Take note to also specify the `DependencyAttribute` for the Xamarin DI to work:

```csharp
[assembly: Dependency(typeof(AuthenticationService))]

namespace Your_Namespace
{
public class AuthenticationService : IAuthenticationService
{
private Auth0Client _auth0Client;

public AuthenticationService()
{
_auth0Client = new Auth0Client(new Auth0ClientOptions
{
Domain = "your domain",
ClientId = "your client id"
});
}

public Task<LoginResult> Authenticate()
{
return _auth0Client.LoginAsync();
}
}
}
```

* Finally, you can get an instance of `IAuthenticationService` inside your .NET Standard DLL, and call the `Authenticate` method to invoke the Auth0 OIDC Client:

```csharp
var authenticationService = DependencyService.Get<IAuthenticationService>();
var loginResult = await authenticationService.Authenticate();
```

## Contribute

Feel like contributing to this repo? We're glad to hear that! Before you start contributing please visit our [Contributing Guideline](https://github.com/auth0-community/getting-started/blob/master/CONTRIBUTION.md).

Here you can also find the [PR template](https://github.com/auth0-community/auth0-xamarin-oidc-samples/blob/master/PULL_REQUEST_TEMPLATE.md) to fill once creating a PR. It will automatically appear once you open a pull request.

## Issues Reporting

Spotted a bug or any other kind of issue? We're just humans and we're always waiting for constructive feedback! Check our section on how to [report issues](https://github.com/auth0-community/getting-started/blob/master/CONTRIBUTION.md#issues)!

Here you can also find the [Issue template](https://github.com/auth0-community/auth0-xamarin-oidc-samples/blob/master/ISSUE_TEMPLATE.md) to fill once opening a new issue. It will automatically appear once you create an issue.

## Repo Community

Feel like PRs and issues are not enough? Want to dive into further discussion about the tool? We created topics for each Auth0 Community repo so that you can join discussion on stack available on our repos. Here it is for this one: [auth0-xamarin-oidc-samples](https://community.auth0.com/t/auth0-community-oss-auth0-xamarin-oidc-samples/15973)

<a href="https://community.auth0.com/">
<img src="/Assets/join_auth0_community_badge.png"/>
</a>

## License

This project is licensed under the MIT license. See the [LICENSE](https://github.com/auth0-community/auth0-xamarin-oidc-samples/blob/master/LICENSE) file for more info.

## What is Auth0?

Auth0 helps you to:

* Add authentication with [multiple authentication sources](https://docs.auth0.com/identityproviders), either social like
* Google
* Facebook
* Microsoft
* Linkedin
* GitHub
* Twitter
* Box
* Salesforce
* etc.

**or** enterprise identity systems like:
* Windows Azure AD
* Google Apps
* Active Directory
* ADFS
* Any SAML Identity Provider

* Add authentication through more traditional [username/password databases](https://docs.auth0.com/mysql-connection-tutorial)
* Add support for [linking different user accounts](https://docs.auth0.com/link-accounts) with the same user
* Support for generating signed [JSON Web Tokens](https://docs.auth0.com/jwt) to call your APIs and create user identity flow securely
* Analytics of how, when and where users are logging in
* Pull data from other sources and add it to user profile, through [JavaScript rules](https://docs.auth0.com/rules)

## Create a free Auth0 account

* Go to [Auth0 website](https://auth0.com/signup)
* Hit the **SIGN UP** button in the upper-right corner
