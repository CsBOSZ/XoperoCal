using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using xopCal.Services;

namespace xopCal.Hubs;

[Authorize]
public class EventHub : Hub
{

}