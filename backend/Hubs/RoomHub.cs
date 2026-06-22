using Backend.DTOs;
using Backend.Exceptions;
using Backend.Hubs.Clients;
using Backend.Infrastructure;
using Backend.Services.Rooms;
using Backend.Services.Timetables;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace Backend.Hubs;

[Authorize]
public class RoomHub(
    ILogger<RoomHub> logger,
    IRoomService roomService,
    ITimetableService timetableService,
    IRoomTracker roomTracker,
    IProfileTracker profileTracker
) : Hub<IRoomHubClient>
{
    private readonly ILogger<RoomHub> _logger = logger;
    private readonly IRoomService _roomService = roomService;
    private readonly ITimetableService _timetableService = timetableService;
    private readonly IRoomTracker _roomTracker = roomTracker;
    private readonly IProfileTracker profileTracker = profileTracker;

    private Guid GetUserId()
    {
        if (Context.User == null)
            throw new UnauthorizedAccessException("User is not authenticated.");

        return ClaimsHelper.GetUserId(Context.User);
    }

    private Guid GetCurrentRoomId(Guid userId)
    {
        if (!_roomTracker.GetRoomOfUser(userId, out var roomId))
            throw new HubException($"User {userId} has not joined any rooms.");

        return roomId;
    }

    public override async Task OnConnectedAsync()
    {
        var userId = GetUserId();
        RoomHubLogs.LogUserConnected(_logger, userId, Context.ConnectionId);

        // TODO: some sort of timer (with below) which automatically reconnects a user back to a room

        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var userId = GetUserId();
        RoomHubLogs.LogUserDisconnected(_logger, exception, userId, Context.ConnectionId);

        await LeaveRoom(GetCurrentRoomId(userId));
        profileTracker.RemoveUser(userId);

        // TODO: some sort of timer which when expires logs the user out of the room as well

        await base.OnDisconnectedAsync(exception);
    }

    public async Task<RoomInformation> CreateOrJoinRoom(Guid roomId)
    {
        // TODO: Check if the user is allowed to join the room before adding them to the group

        if (!_roomService.RoomExists(roomId))
            _roomService.CreateRoom(roomId);

        var userId = GetUserId();
        _roomService.HandleJoinRoom(userId, roomId);

        await Groups.AddToGroupAsync(Context.ConnectionId, roomId.ToString());
        RoomHubLogs.LogUserJoinedRoom(_logger, userId, roomId);

        return await _roomService.GetRoomInformation(roomId)
            ?? throw new HubException("Failed to retrieve room information");
    }

    public async Task LeaveRoom(Guid roomId)
    {
        var userId = GetUserId();
        _roomService.HandleLeaveRoom(userId, roomId);

        await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomId.ToString());
        RoomHubLogs.LogUserLeftRoom(_logger, userId, roomId);

        await _roomService.CommitChanges(roomId);
    }

    public async Task<RoomInformation> GetRoomInformation(Guid roomId)
    {
        return await _roomService.GetRoomInformation(roomId)
            ?? throw new HubException($"Room {roomId} not found");
    }

    public async Task CreateTimetable(CreateTimetableRequest timetableRequest) =>
        _roomService.HandleCreateTimetable(
            GetCurrentRoomId(GetUserId()),
            GetUserId(),
            timetableRequest,
            null
        );

    public async Task CreateCopyOfTimetable(Guid timetableId)
    {
        try
        {
            var userId = GetUserId();
            var timetable = await _timetableService
                .GetTimetableByIdAsync(timetableId, userId)
                .MapAsync(t => new CreateTimetableRequest()
                {
                    Name = t.Name,
                    MetaData = t.MetaData,
                });

            _roomService.HandleCreateTimetable(
                GetCurrentRoomId(userId),
                userId,
                timetable,
                timetableId
            );
        }
        catch (NotFoundException)
        {
            throw new HubException($"Timetable with id {timetableId} not found");
        }
    }

    public async Task UpdateTimetable(Guid timetableId, UpdateTimetableRequest timetableRequest) =>
        await _roomService.HandleUpdateTimetable(
            GetCurrentRoomId(GetUserId()),
            timetableId,
            timetableRequest
        );

    public async Task DeleteTimetable(Guid timetableId)
    {
        var roomId = GetCurrentRoomId(GetUserId());

        if (!_roomService.HandleDeleteTimetable(roomId, timetableId))
        {
            throw new HubException(
                "Cannot delete main timetable of the room in Hub. Please use DEL /timetable/{id}"
            );
        }
    }

    // This method is used mainly as a placeholder for testing, but it could be used in the future
    // to send a message as a chat feature
    public async Task SendMessageToRoom(string message)
    {
        Guid userId = GetUserId();

        if (!_roomTracker.GetRoomOfUser(userId, out var roomId))
            throw new HubException($"User {userId} has not joined any rooms.");

        // roomId is string here, no nullable issues
        await Clients
            .Group(roomId.ToString())
            .ReceiveMessage(new MessageResponse(userId, message, DateTime.Now));
    }
}
