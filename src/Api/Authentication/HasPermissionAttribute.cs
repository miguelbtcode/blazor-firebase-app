using Microsoft.AspNetCore.Authorization;
using NetFirebase.Api.Models.Enums;

namespace NetFirebase.Api.Authentication;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
public class HasPermissionAttribute(PermissionEnum permission)
    : AuthorizeAttribute(policy: permission.ToString()) { }
