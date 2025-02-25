using JEPCO.Application.Models.Users.Internal;
using JEPCO.Shared.ModelsAbstractions;

namespace JEPCO.Application.Interfaces.IAM;

public interface IIdentityUserManager
{
    /// <summary>
    /// Returns the created user Id
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    Task<Guid> CreateUserAsync(IAMUser user);
    Task UpdateUserAsync(IAMUser user);
    Task<IAMUser> GetUserAsync(Guid userId);
    Task DeleteUserAsync(Guid userId);

    Task SetUserPasswordAsync(Guid userId, string password, bool isTemporary);
    Task SetUserRolesAsync(Guid userId, IEnumerable<string> roles);
    Task DisableUserAsync(Guid userId);
    Task EnableUserAsync(Guid userId);
    Task<Response<bool>> ResetUserPasswordAsync(Guid userId);
    Task SendVerifyEmailWithResponseAsync(Guid userId);
    Task SendVerifyEmailAsync(Guid userId);
    Task<List<IAMUserSessionData>> GetUserSessionsAsync(Guid userId);

    Task<Response<bool>> LogoutUserAsync(Guid userId);
}
