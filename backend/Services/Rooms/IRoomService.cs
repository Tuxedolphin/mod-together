using Backend.DTOs;
using Backend.Models;

namespace Backend.Services.Rooms;

public interface IRoomService
{
    bool RoomExists(Guid roomId);

    Task RegisterConnectionAsync(Guid userId, string connectionId);
    Task<RoomConnectionMove> CreateOrJoinRoomAsync(Guid roomId, Guid userId, string connectionId);
    Task<RoomConnectionDeparture?> HandleLeaveRoomAsync(Guid roomId, string connectionId);
    Task<ConnectionRemoval?> HandleDisconnectAsync(string connectionId);
    bool TryGetRoomOfConnection(string connectionId, out Guid roomId);
    bool IsConnectionInRoom(string connectionId, Guid roomId);
    bool HandleDeleteTimetable(Guid roomId, Guid userId, Guid timetableId);

    CreateTimetableResult HandleCreateTimetable(
        Guid roomId,
        Guid userId,
        CreateTimetableRequest timetableRequest,
        Guid? CopyOf
    );

    // TODO: Maybe add in some auth feature so that some people can "lock" their timetable
    Task<bool> HandleUpdateTimetableAsync(
        Guid roomId,
        Guid userId,
        Guid timetableId,
        UpdateTimetableRequest timetableRequest
    );

    Task<IReadOnlyCollection<string>> UpdateRoomVisibilityAsync(
        Guid roomId,
        Guid callerId,
        Visibility visibility
    );
    Task<RoomInformation?> GetRoomInformationAsync(Guid roomId, Guid userId);

    Task<IReadOnlyCollection<UserSearchResponse>> FindUsersByHandleAsync(
        string handle,
        Guid roomId,
        Guid callerId
    );
    Task<IReadOnlyCollection<RoomMemberResponse>?> GetRoomMembersAsync(Guid roomId, Guid userId);
    Task<IReadOnlyCollection<TimetableDetailedResponse>?> GetTimetablesDetailedInRoomAsync(
        Guid roomId,
        Guid userId
    );

    bool CloseRoom(Guid roomId);
    Task<bool> CommitChangesAsync(Guid roomId);

    Task SetMemberRoleAsync(Guid roomId, Guid userId, RoomRole role, Guid callerId);
    Task<IReadOnlyCollection<string>> RevokeMemberAccessAsync(
        Guid roomId,
        Guid userId,
        Guid callerId
    );

    Task CopyTimetableTo(Guid userId, Guid roomId, Guid timetableId, Guid timetableIdToCopyTo);
}
