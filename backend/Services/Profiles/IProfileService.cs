using Backend.DTOs;

namespace Backend.Services.Profiles;

public interface IProfileService
{
    Task<ProfileResponse> GetUserProfileAsync(Guid userId);
    Task UpdateUserProfileAsync(Guid userId, UpdateProfileRequest request);
    Task DeleteUserProfileAsync(Guid userId);

    Task<ProfileResponse> UpsertUserAvatarAsync(Guid userId, Stream stream);
    Task DeleteUserAvatarAsync(Guid userId);
}
