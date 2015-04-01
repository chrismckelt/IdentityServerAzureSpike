#  Identity Server 3 on Azure Spike

### Demo

-	Single Sign On/Out for customers under any *.website.com website 
-	4 Sites
    - 1 x WebApi
    - 2 x MVC
    - 1 x JavaScript SPA
-	Session management handled by client sites (token refresh/expiry)

### Assumptions

-	All access is performed under a single parent domain *.website.com
-	A domain identity cookie is set which contains token information accessible across all child sites.

## Spike demonstrates

### Setup on Azure  
-	Identity Provider hosted in a Web Role signing X509 Tokens
-	Child web roles authenticating with identity & claims retrieval

-	Unauthenticated user logs into Site A
-	User navigates to Site B and is automatically authenticated
-	User logs out from Site B
-	User is automatically logged out from Site A
-	This process can be performed in reverse


### Tech

-	Identity Server 3
-	Authentication Protocol : Open ID Connect (authentication with a simple identity layer over OAuth 2.0 protocol)
-	Authorization Grant - Authorisation Code Grant Flow  | Implicit | Hybrid
-	JWT ID Tokens signed using an X509 Certificate

### Out of Scope
-	Implementing 3rd party token providers – Google/Facebook/Windows Live
-	Cross domain authentication   


#Projects

## IdentityServerWebApi

- WebApi web role facade over identity provider
- Install-Package Thinktecture.IdentityServer3.AccessTokenValidation

## IdentityServerWebApi

- WebApi web role facade over identity provider
- Install-Package Thinktecture.IdentityServer3

## SiteA

- MVC
- Install-Package Thinktecture.IdentityServer3.AccessTokenValidation

## SiteB

- MVC
- Install-Package Thinktecture.IdentityServer3.AccessTokenValidation

## SiteC

- JavaScript SPA
- Install-Package Thinktecture.IdentityServer3.AccessTokenValidation