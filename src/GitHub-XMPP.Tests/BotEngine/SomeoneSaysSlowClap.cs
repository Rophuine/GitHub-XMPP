using System;
using System.IO;
using GitHub_XMPP.EventServices;
using GitHub_XMPP.XMPP;
using GitHub_XMPP.XMPP.Events;
using GitHub_XMPP.XMPP.Scripting;
using NSubstitute;
using NUnit.Framework;
using agsXMPP;
using agsXMPP.protocol.client;

namespace GitHub_XMPP.Tests.BotEngine
{
    [TestFixture]
    internal class SomeoneSaysSlowClap
    {
        private IEventNotifier _notifier;

        [SetUp]
        public void Setup()
        {
            var to = new Jid("to@example.com");
            var from = new Jid("from@example.com");
            var msg = new Message(to, from, MessageType.groupchat, "slow clap");
            var eventObj = new GroupChatMessageArrived(msg, "testroom");
            _notifier = Substitute.For<IEventNotifier>();
            var handler = new GroupChatScriptHandler(_notifier);
            handler.ScriptFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "scripts");
            handler.Handle(eventObj);
        }

        [Test]
        public void AMessageShouldBeSent()
        {
            _notifier.Received().SendText(Arg.Any<string>());
        }
    }
}