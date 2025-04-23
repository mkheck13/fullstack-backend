using Microsoft.AspNetCore.SignalR;
using fullstack_backend.Context;
using fullstack_backend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace fullstack_backend.Hubs
{
    public class MessagingHub : Hub
    {
        private readonly DataContext _dataContext;

        public MessagingHub(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        // Add this method to get or create conversations
        private async Task<ConversationModel> GetOrCreateConversation(string user1Id, string user2Id)
        {
            var conversation = await _dataContext.Conversations
                .FirstOrDefaultAsync(c => (c.User1Id == user1Id && c.User2Id == user2Id) || 
                                          (c.User1Id == user2Id && c.User2Id == user1Id));

            if (conversation == null)
            {
                // If no conversation exists, create a new one
                conversation = new ConversationModel
                {
                    User1Id = user1Id,
                    User2Id = user2Id
                };

                _dataContext.Conversations.Add(conversation);
                await _dataContext.SaveChangesAsync();
            }

            return conversation;
        }

        // Posting a message from one user to another
public async Task PostMessage(string receiverId, string content)
{
    var senderId = Context.ConnectionId;

    // Get or create conversation between users
    var conversation = await GetOrCreateConversation(senderId, receiverId);

    // Create a new message and save to the database
    var message = new MessageModel
    {
        SenderId = senderId,  // Updated from Sender to SenderId
        Content = content,
        Timestamp = DateTime.UtcNow,
        ConversationId = conversation.Id  // Assign the conversation ID
    };

    // Add the message to the conversation's message list (in memory for now)
    conversation.Messages.Add(message);

    // Save the message to the database
    _dataContext.Messages.Add(message);
    await _dataContext.SaveChangesAsync();

    // Broadcast the message to the receiver and sender
    await Clients.Client(receiverId).SendAsync("ReceiveMessage", senderId, content, message.Timestamp);
    await Clients.Caller.SendAsync("ReceiveMessage", senderId, content, message.Timestamp);
}


        // Retrieving message history for a conversation
        public async Task RetrieveMessageHistory(string userId)
        {
            // Fetch conversations for the given user
            var conversations = await _dataContext.Conversations
                .Where(c => c.User1Id == userId || c.User2Id == userId)
                .Include(c => c.Messages)
                .ToListAsync();

            // Return the conversations and messages
            await Clients.Caller.SendAsync("MessageHistory", conversations);
        }
    }
}
