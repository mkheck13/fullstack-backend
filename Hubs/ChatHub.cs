using Microsoft.AspNetCore.SignalR;

namespace backend.Hubs
{
    //connect to messagemodel

    public class MessagingHub : Hub
    {
        private readonly DataContext _dataContext;

        public MessagingHub(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        List<MessageModel> MessageHistory = new();

        public async Task PostMessage(string content)
        {
            //connectionID should be changed later on. The connectionID will change each time a user reconnects, so it is not viable to create a history of messages. May need to use UserIdentifier
            var senderId = Context.ConnectionId;
            MessageModel userMessage = new()
            {
                Sender = senderId,
                receiver = receiverId,
                Content = content,
                Timestamp = DateTime.UtcNow,
            };

            MessageHistory.Add(userMessage);
            await Clients
                .Client(receiverId)
                .SendAsync("ReceiveMessage", senderId, content, userMessage.Timestamp);

            await Clients.Caller.SendAsync(
                "ReceiveMessage",
                senderId,
                content,
                userMessage.Timestamp
            );
        }

        public async Task RetrieveMessageHistory() =>
            await Clients.Caller.SendAsync("MessageHistory", MessageHistory);
    }
}
