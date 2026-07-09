namespace Backend.DTOs;

public record RoomInformation(
    Guid RoomId,
    IReadOnlyCollection<RoomMemberResponse> Users,
    IReadOnlyCollection<TimetableDetailedResponse> Timetables
);

public record MessageResponse(Guid UserId, string Content, DateTime SentAt);

public record RoomMemberResponse(Guid UserId, string? Username, string? AvatarUrl, RoomRole Role);

public enum RoomRole
{
    Owner,
    Editor,
    Viewer,
}
