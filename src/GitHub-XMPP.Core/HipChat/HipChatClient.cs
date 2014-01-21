using System.Configuration;
using GitHub_XMPP.HipChat.DTOs.Request;
using GitHub_XMPP.HipChat.DTOs.Response;
using GitHub_XMPP.Messaging;
using RestSharp;

namespace GitHub_XMPP.HipChat
{
    public class HipChatClient : IMessagingService
    {
        private const string ApiUri = "http://api.hipchat.com/v2/";

        private string ApiKey
        {
            get { return ConfigurationManager.AppSettings["HipChatAPIKey"]; }
        }

        private string RoomId
        {
            get { return ConfigurationManager.AppSettings["HipChatRoomId"]; }
        }

        private readonly RestClient _client;

        public HipChatClient()
        {
            _client = new RestClient(ApiUri);
            _client.AddDefaultHeader("Authorization", string.Format("Bearer {0}", ApiKey));
        }

        public RoomListResponse GetRooms()
        {
            var req = new RestRequest("room");
            RoomListResponse response = _client.Execute<RoomListResponse>(req).Data;
            return response;
        }

        public IRestResponse SendRoomMessage(string message)
        {
            return SendRoomMessage(RoomId, message);
        }

        public IRestResponse SendRoomMessage(string roomId, string message)
        {
            var req = new RestRequest(string.Format("room/{0}/notification", roomId)) {RequestFormat = DataFormat.Json};
            var data = new RoomNotificationRequest(message) {notify = true};
            req.AddBody(data);
            IRestResponse response = _client.Post(req);
            return response;
        }

        public void SendText(string test)
        {
            SendRoomMessage(test);
        }
    }
}