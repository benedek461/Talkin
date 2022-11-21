using Microsoft.AspNetCore.SignalR;

namespace ChatAPI.Providers
{
    public class IdBasedUserIdProvider : IUserIdProvider
    {
        public virtual string GetUserId(HubConnectionContext connection)
        {
            return connection.User?.Claims
                .ToList()
                .FirstOrDefault(x => x.Type == "Id").Value;
        }
    }
}
