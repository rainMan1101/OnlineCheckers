using Domain.ValueTypes;
using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCheckers.Client.ClientHubs.Implementation
{
    public class CChatHubClient : IChatHubClient
    {
        public event Action<IEnumerable<CMessage>> OnGetMessageList;
        public event Action<CMessage> OnSendMessage;
        public event Action OnGameClosed;
        public event Action OnUserExistsYet;
        public event Action OnUserDoesNotExists;
        

        private IHubProxy _chatHubProxy;

        public CChatHubClient(IHubProxy chatHubProxy)
        {
            _chatHubProxy = chatHubProxy;
            _chatHubProxy.On("OnGetMessageList", 
                (messages) => OnGetMessageList.Invoke(messages));
            _chatHubProxy.On("OnSendMessage",
                (message) => OnSendMessage.Invoke(message));
            _chatHubProxy.On("OnGameClosed",
                () => OnGameClosed.Invoke());
            _chatHubProxy.On("OnUserExistsYet",
                () => OnUserExistsYet.Invoke());
            _chatHubProxy.On("OnUserDoesNotExists",
                () => OnUserDoesNotExists.Invoke());
        }

        public void JoinToChat(Guid gameId)
        {
            _chatHubProxy.Invoke("JoinToChat", gameId);
        }

        public void LeaveChat()
        {
            _chatHubProxy.Invoke("LeaveChat");
        }

        public void SendMessage(String messageText)
        {
            _chatHubProxy.Invoke("SendMessage", messageText);
        }
    }
}
