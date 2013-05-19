using System;
using System.IO;
using GitHub_XMPP.EventHandlers;
using GitHub_XMPP.Notifiers;
using NSubstitute;
using NUnit.Framework;
using Shouldly;

namespace GitHub_XMPP.Tests
{
    [TestFixture]
    internal class GitHubForkEventArrives
    {
        private IEventNotifier _notifier;
        private GitHubForkEvent _event;

        [SetUp]
        public void Setup()
        {
            _notifier = Substitute.For<IEventNotifier>();
            _event = new GitHubForkEvent(_notifier);

            string text =
                File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SampleJson", "GitHubForkJson.txt"));
            _event.Handle(text);
        }

        [Test]
        public void TheEventNotifierShouldReceiveACall()
        {
            _notifier.Received().SendText(Arg.Any<string>());
        }

        [Test]
        public void TheRepoShouldBeSet()
        {
            _event.EventData.repository.ShouldNotBe(null);
            _event.EventData.repository.name.ShouldBe("TimeZoneInfoGenerator");
        }

        [Test]
        public void TheSourceOwnerShouldBeSet()
        {
            _event.EventData.repository.owner.ShouldNotBe(null);
            _event.EventData.repository.owner.login.ShouldBe("Rophuine");
        }

        [Test]
        public void TheSenderShouldBeSet()
        {
            _event.EventData.sender.ShouldNotBe(null);
            _event.EventData.sender.login.ShouldBe("NewOwner");
        }

        [Test]
        public void TheForkeeShouldBeSet()
        {
            _event.EventData.forkee.ShouldNotBe(null);
            _event.EventData.forkee.name.ShouldBe("TimeZoneInfoGenerator");
        }

        [Test]
        public void TheForkeeOwnerShouldBeSet()
        {
            _event.EventData.forkee.owner.ShouldNotBe(null);
            _event.EventData.forkee.owner.login.ShouldBe("NewOwner");
        }
    }
}