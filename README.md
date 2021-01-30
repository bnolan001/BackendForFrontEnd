#Backend For Frontend

This project is a modified version [ leastprivilege /
AspNetCoreSecuritySamples / BFF](https://github.com/leastprivilege/AspNetCoreSecuritySamples/tree/aspnetcore3/BFF).  It demonstrates the same features but utilizes Microsoft's [YARP: Reverse-Proxy](https://github.com/microsoft/reverse-proxy). 
* A server-side backend for user authentication and session management via Identity Server
* SameSite cookies
* Automatic token management
* Proxying calls to back-end services with YARP

The Host project is configured to use cookies and an in-memory token management system.  It requests the following scopes from Identity Server, `openid`, `offline_access`, `profile`, and `api`.  The token from Identity Server is stored in memory and then trasferred to the `Authorization` header as a `Bearer` token for external API requests.  The Api project is configured to use a JWT Bearer token for authentication.  It expects the token to contain an `api` scope, if none is found or the user is not authentiated that `401 Access Denied` response will be sent back.  

Hopefully in the not-too distant future I'll add an example project that incorporates an Angular SPA where specific routes require authentication but not all routes.

