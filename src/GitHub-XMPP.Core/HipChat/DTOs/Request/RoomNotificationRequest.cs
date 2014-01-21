namespace GitHub_XMPP.HipChat.DTOs.Request
{
    internal class RoomNotificationRequest
    {
        public RoomNotificationRequest(string message)
        {
            message_format = "text";
            this.message = message;
        }

        public string color = "gray";
        public string message;
        public string message_format;
        public bool notify = false;
    }
}