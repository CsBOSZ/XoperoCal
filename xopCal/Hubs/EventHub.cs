using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using xopCal.Services;

namespace xopCal.Hubs;

[Authorize]
public class EventHub : Hub
{
    private readonly IEventCalService _eventCalService;

    public EventHub(IEventCalService eventCalService)
    {
        _eventCalService = eventCalService;
    }

    public override Task OnConnectedAsync()
    {
        
        _eventCalService.StartWatch(int.Parse(Context.UserIdentifier));
        
        return base.OnConnectedAsync();
    }
}