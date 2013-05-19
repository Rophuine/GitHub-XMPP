using GitHub_XMPP.EventServices;
using agsXMPP.protocol.client;

namespace GitHub_XMPP.XMPP
{
    public class GroupChatMessageArrived : IDomainEvent
    {
        private readonly string _room;

        public string Room
        {
            get { return _room; }
        }

        private readonly Message _message;

        public Message Message
        {
            get { return _message; }
        }

        public GroupChatMessageArrived(Message message, string room)
        {
            _message = message;
            _room = room;
        }
    }
}