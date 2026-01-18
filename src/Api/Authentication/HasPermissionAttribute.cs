using Microsoft.AspNetCore.Authorization;

namespace NetFirebase.Api.Authentication;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
public class HasPermissionAttribute(Permission permission)
    : AuthorizeAttribute(policy: permission.ToString()) { }
