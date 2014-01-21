using System;
using System.Configuration;
using System.Threading;
using agsXMPP;
using agsXMPP.Collections;
using agsXMPP.protocol.client;
using agsXMPP.protocol.x.muc;
using GitHub_XMPP.EventServices;
using GitHub_XMPP.XMPP.Events;

namespace GitHub_XMPP.XMPP
{
    public class XMPPClient : IDisposable
    {
        private readonly XmppClientConnection _connection;
        private MucManager _man;

        private string XMPPServer
        {
            get { return ConfigurationManager.AppSettings["XMPPServer"]; }
        }

        private string XMPPUser
        {
            get { return ConfigurationManager.AppSettings["XMPPUser"]; }
        }

        private string XMPPPassword
        {
            get { return ConfigurationManager.AppSettings["XMPPPassword"]; }
        }

        private string XMPPConferenceServer
        {
            get { return ConfigurationManager.AppSettings["XMPPConferenceServer"]; }
        }

        private string XMPPRoom
        {
            get { return ConfigurationManager.AppSettings["XMPPRoom"]; }
        }

        private string XMPPRoomPassword
        {
            get { return ConfigurationManager.AppSettings["XMPPRoomPassword"]; }
        }

        private string RoomJidString
        {
            get { return string.Format("{0}@{1}", XMPPRoom, XMPPConferenceServer); }
        }

        private string GitBotJidString
        {
            get { return string.Format("{0}@{1}/{2}", XMPPRoom, XMPPConferenceServer, XMPPUser); }
        }

        public XMPPClient()
        {
            _connection = new XmppClientConnection(XMPPServer);
            _connection.OnLogin += ConnectionOnLogin;
            _connection.OnMessage += OnMessage;
            ConnectToXmppServer();
        }

        private void ConnectToXmppServer()
        {
            _connection.Open(XMPPUser, XMPPPassword);
        }

        public void SendText(string text)
        {
            if (_connection.XmppConnectionState == XmppConnectionState.Disconnected)
                TryToReconnect();
            var msg = new Message(RoomJidString, MessageType.groupchat, text);
            _connection.Send(msg);
        }

        private void OnMessage(object sender, Message msg)
        {
            if (msg.Type == MessageType.groupchat)
            {
                return; // We have a separate group-chat handler
            }
            // TODO: Handle non-group messages
        }

        private void OnGroupChatMessage(object sender, Message msg, object data)
        {
            var room = data as string;
            if (msg.Type == MessageType.groupchat && msg.From.ToString().ToLower() != GitBotJidString.ToLower())
                IncomingGroupMessage(msg, room);
        }

        public void IncomingGroupMessage(Message msg, string room)
        {
            if (msg.Subject != null)
            {
                // TODO: Handle room subject change
            }
            if (msg.Body != null)
            {
                DomainEvents.Raise(new GroupChatMessageArrived(msg, room));
            }
        }

        private void TryToReconnect()
        {
            for (int i = 0;
                i < 3 && _connection.XmppConnectionState == XmppConnectionState.Disconnected;
                i++, Thread.Sleep(5000))
                ConnectToXmppServer();
        }

        private void ConnectionOnLogin(object sender)
        {
            // Set up the room message callback
            var roomJid = new Jid(RoomJidString.ToLower());
            _connection.MessageGrabber.Add(roomJid, new BareJidComparer(), OnGroupChatMessage, XMPPRoom);

            _man = new MucManager(_connection);
            _man.JoinRoom(GitBotJidString, XMPPUser, XMPPRoomPassword, true);
            /*Presence pres = new Presence();
            Jid to = new Jid(RoomJidString);
            to.Resource = XMPPUser;
            pres.To = to;
            _connection.Send(pres);*/
        }

        public bool Disposed { get; protected set; }

        public void Dispose()
        {
            if (!Disposed)
            {
                Disposed = true;
                _man.LeaveRoom(GitBotJidString, XMPPUser);
                /*Presence pres = new Presence();
                Jid to = new Jid(RoomJidString);
                to.Resource = XMPPUser;
                pres.To = to;
                pres.Type = PresenceType.unavailable;
                _connection.Send(pres);*/
            }
        }
    }
}