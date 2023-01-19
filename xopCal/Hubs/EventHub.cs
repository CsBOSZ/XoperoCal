using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using xopCal.Services;

namespace xopCal.Hubs;

[Authorize]
public class EventHub : Hub
{
    private readonly IUserService _userService;

    public EventHub(IUserService userService)
    {
        this._userService = userService;
    }

    public override Task OnConnectedAsync()
    {

        _userService.AddConnectUser(int.Parse(Context.UserIdentifier), Context.ConnectionId);

        return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        if (Context.UserIdentifier != null) _userService.DeleteConnectUser(int.Parse(Context.UserIdentifier));

        return base.OnDisconnectedAsync(exception);
    }
}