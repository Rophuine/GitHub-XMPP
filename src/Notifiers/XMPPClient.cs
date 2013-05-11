using System;
using System.Configuration;
using System.Threading;
using agsXMPP;
using agsXMPP.protocol.client;
using agsXMPP.protocol.x.muc;

namespace GitHub_XMPP.Notifiers
{
    public class XMPPClient : IEventNotifier, IDisposable
    {
        private XmppClientConnection _connection;
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

        private void TryToReconnect()
        {
            for (int i = 0; i < 3 && _connection.XmppConnectionState == XmppConnectionState.Disconnected; i++, Thread.Sleep(5000))
                ConnectToXmppServer();
        }

        private void ConnectionOnLogin(object sender)
        {
            _man = new MucManager(_connection);
            _man.JoinRoom(GitBotJidString, XMPPUser, XMPPRoomPassword, true);
        }

        public bool Disposed { get; protected set; }

        public void Dispose()
        {
            if (!Disposed)
            {
                Disposed = true;
                _man.LeaveRoom(GitBotJidString, XMPPUser);
            }
        }
    }
}