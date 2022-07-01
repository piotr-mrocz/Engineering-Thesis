namespace IntranetWebApi;

public interface IMessageHubClient
{
    Task NewMessageWasSend();
}
