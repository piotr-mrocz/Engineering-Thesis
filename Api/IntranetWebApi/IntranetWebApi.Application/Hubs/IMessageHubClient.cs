namespace IntranetWebApi;

public interface IMessageHubClient
{
    Task NewMessage();
    Task JoinGroup(string groupName);
}
