# Sitecore-IdentityServer-ADFS

Small hacks to make Sitecore Identity Server work well with an on-prem ADFS installation.

The PrefixRoles class simply adds the "sitecore\" prefix to all roles served as claims from ADFS, allowing for mapping existing roles in AD to roles in Sitecore.

The AdfsExternalUserBuilder uses the upn from ADFS to create a readable username, instead of the OOTB version which is hard to read (and remember!) :)
