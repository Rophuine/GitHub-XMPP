using System.Collections.Generic;

namespace GitHub_XMPP.HipChat.DTOs.Response
{
    public class Links
    {
        public string self { get; set; }
        public string webhooks { get; set; }
    }

    public class Room
    {
        public int id { get; set; }
        public Links links { get; set; }
        public string name { get; set; }
    }

    public class ApiLink
    {
        public string self { get; set; }
    }

    public class RoomListResponse
    {
        public List<Room> items { get; set; }
        public ApiLink links { get; set; }
        public int maxResults { get; set; }
        public int startIndex { get; set; }
    }
}