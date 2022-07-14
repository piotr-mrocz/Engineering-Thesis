using Microsoft.AspNetCore.SignalR;

namespace IntranetWebApi;

public class MessageHubClient : Hub<IMessageHubClient>
{
    public Task JoinGroup(string groupName)
    {
        return Groups.AddToGroupAsync(Context.ConnectionId, groupName);
    }
}
