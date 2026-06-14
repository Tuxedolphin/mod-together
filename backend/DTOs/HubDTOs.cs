using Backend.Models;

namespace Backend.DTOs;

// Both users and timetables are needed even though timetables have userId since not all users
// may have a timetable
public record RoomInformation(
    Guid RoomId,
    IReadOnlyCollection<Profile> Users,
    IReadOnlyCollection<TimetableDetailedResponse> Timetables
);

public record MessageResponse(Guid UserId, string Content, DateTime SentAt);
