namespace NetFirebase.Api.Services.Authorization;

public interface IPermissionService
{
    Task<HashSet<string>> GetPermissionsAsync(string firebaseId, CancellationToken cancellationToken = default);
}
