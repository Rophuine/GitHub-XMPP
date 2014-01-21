using System;
using System.Configuration;
using System.Net;
using GitHub_XMPP.HipChat.DTOs;
using GitHub_XMPP.HipChat.DTOs.Request;
using GitHub_XMPP.HipChat.DTOs.Response;
using GitHub_XMPP.Notifiers;
using Newtonsoft.Json;
using RestSharp;

namespace GitHub_XMPP.HipChat
{
    public class HipChatClient
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

        private RestClient _client;

        public HipChatClient()
        {
            _client = new RestClient(ApiUri);
            _client.AddDefaultHeader("Authorization", "Bearer LJ2ftPf1PvAkDdobP7dmUdzqcg4eWprIrdEfGUUp");
        }

        public RoomListResponse GetRooms()
        {
            var req = new RestRequest("room");
            var response = _client.Execute<RoomListResponse>(req).Data;
            return response;
        }

        public IRestResponse SendRoomMessage(string message)
        {
            return SendRoomMessage(RoomId, message);
        }

        public IRestResponse SendRoomMessage(string roomId, string message)
        {
            var req = new RestRequest(string.Format("room/{0}/notification", roomId));
            req.RequestFormat = DataFormat.Json;
            var data = new RoomNotificationRequest(message) {notify=true};
            req.AddBody(data);
            var response = _client.Post(req);
            return response;
        }

        public void SendText(string test)
        {
            SendRoomMessage(test);
        }
    }
}