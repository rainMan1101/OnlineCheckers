using Domain.ValueTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCheckers.Client.ClientHubs
{
    public interface IChatHubClient
    {
        event Action<IEnumerable<CMessage>> OnGetMessageList;
        event Action<CMessage> OnSendMessage;
        event Action OnGameClosed;
        event Action OnUserExistsYet;
        event Action OnUserDoesNotExists;
        

        void JoinToChat(Guid gameId);
        void LeaveChat();
        void SendMessage(String messageText);
    }
}
