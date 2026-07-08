namespace Backend.DTOs;

public record ProfileResponse(Guid UserId, string? Username, string? AvatarUrl);

public record UpdateProfileRequest(Guid UserId, string Username);

public record UpsertAvatarRequest(IFormFile File);
