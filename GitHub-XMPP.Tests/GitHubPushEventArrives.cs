using System;
using System.IO;
using System.Reflection;
using GitHub_XMPP.EventHandlers;
using GitHub_XMPP.Notifiers;
using NSubstitute;
using NUnit.Framework;
using Shouldly;

namespace GitHub_XMPP.Tests
{
    [TestFixture]
    internal class GitHubPushEventArrives
    {
        IEventNotifier _notifier;
        GitHubPushEvent _pushEvent;

        [SetUp]
        public void Setup()
        {
            _notifier = Substitute.For<IEventNotifier>();
            _pushEvent = new GitHubPushEvent(_notifier);

            string text = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SampleJson", "GitHubPushJson.txt"));
            _pushEvent.Handle(text);
        }

        [Test]
        public void TheEventNotifierShouldReceiveACall()
        {
            _notifier.Received().SendText(Arg.Any<string>());
        }

        [Test]
        public void TheEventDataShouldIncludeAPusher()
        {
            _pushEvent.EventData.pusher.ShouldNotBe(null);
            _pushEvent.EventData.pusher.name.ShouldBe("none");
        }
    }
}