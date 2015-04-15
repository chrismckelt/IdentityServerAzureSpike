#  Identity Server 3 on Azure Spike

### Demo

-	Single Sign On/Out for customers under any *.website.com website
-	6 Sites ( 1 Identity Server WebAPI host + 5 clients)

```javascript
var identityServer = {
    name: "identityServer",
    url: "https://identity.demo.local/",
    coreUrl: "https://identity.demo.local/core/",
    title: "Identity Server"
};

var siteA = {
    name: "siteA",
    url: "http://sitea.demo.local:9556/",
    title: "Site A",
    description: "Hybrid MVC/API",
    colour: "#FAFAFA"
};
var siteB = {
    name: "siteB",
    url: "http://siteb.demo.local:9557/",
    title: "Site B",
    description: "Hybrid MVC/API",
    colour: "#F5F6CE"
};
var siteC =
{
    name: "siteC",
    url: "http://sitec.demo.local:9558/",
    title: "Site C",
    description: "JavaScript",
    colour: "#A9F5BC"
};
var siteD =
{
    name: "siteD",
    url: "http://sited.demo.local:9559/",
    title: "Site D",
    description: "Code Flow MVC/API",
    colour: "#F8E0F7"
};
var siteE = {
    name: "siteE",
    url: "http://sited.demo.local:9560/",
    title: "Site E",
    description: "Implicit JavaScript Form Post",
    colour: "#A9E2F3"
};
```

    Session management handled by client sites (token refresh/expiry)
	
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
-	Authorization Grants - Hybrid | Authorisation Code Grant Flow  | Implicit 
-	JWT ID Tokens signed using an X509 Certificate

### Assumptions

-	All access is performed under a single parent domain *.demo.local
-	A domain identity cookie is set which contains token information accessible across all child sites.	

### Not implemented
-	3rd party token providers � Google/Facebook/Windows Live
-	Cross domain authentication
- 	Cross site token refresh/expiry

### Install

-	Run KickMe.bat in the root folder

