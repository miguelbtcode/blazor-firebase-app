using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using NetFirebase.Api.Data;

namespace NetFirebase.Api.Services.Authorization;

public class PermissionService : IPermissionService
{
    private readonly DatabaseContext _dbContext;
    private readonly IMemoryCache _cache;
    private static readonly TimeSpan CacheDuration = TimeSpan.FromMinutes(5);

    public PermissionService(DatabaseContext dbContext, IMemoryCache cache)
    {
        _dbContext = dbContext;
        _cache = cache;
    }

    public async Task<HashSet<string>> GetPermissionsAsync(
        string firebaseId,
        CancellationToken cancellationToken = default
    )
    {
        var cacheKey = $"permissions:{firebaseId}";

        if (_cache.TryGetValue(cacheKey, out HashSet<string>? cachedPermissions))
        {
            return cachedPermissions!;
        }

        var permissions = await _dbContext
            .Users
            .Include(u => u.Roles)!
            .ThenInclude(r => r.Permissions)!
            .Where(u => u.FirebaseId == firebaseId)
            .SelectMany(u => u.Roles!)
            .SelectMany(r => r.Permissions!)
            .Select(p => p.Name)
            .Distinct()
            .ToListAsync(cancellationToken);

        var permissionSet = permissions.ToHashSet();

        _cache.Set(cacheKey, permissionSet, CacheDuration);

        return permissionSet;
    }
}
