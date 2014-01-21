using System;
using System.IO;
using GitHub_XMPP.EventServices;
using GitHub_XMPP.GitHub.EventHandlers;
using NSubstitute;
using NUnit.Framework;
using Shouldly;

namespace GitHub_XMPP.Tests
{
    [TestFixture]
    internal class GitHubPushEventArrives
    {
        private IEventNotifier _notifier;
        private GitHubPushEvent _event;

        [SetUp]
        public void Setup()
        {
            _notifier = Substitute.For<IEventNotifier>();
            _event = new GitHubPushEvent(_notifier);

            string text =
                File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SampleJson", "GitHubPushJson.txt"));
            _event.Handle(text);
        }

        [Test]
        public void TheEventNotifierShouldReceiveACall()
        {
            _notifier.Received().SendText(Arg.Any<string>());
        }

        [Test]
        public void TheEventDataShouldIncludeAPusher()
        {
            _event.EventData.pusher.ShouldNotBe(null);
            _event.EventData.pusher.name.ShouldBe("Rophuine");
            _event.EventData.pusher.email.ShouldBe("owner@example.com");
        }

        [Test]
        public void TheRepoShouldBeSet()
        {
            _event.EventData.repository.ShouldNotBe(null);
            _event.EventData.repository.name.ShouldBe("TimeZoneInfoGenerator");
        }

        [Test]
        public void TheDataShouldIncludeCommits()
        {
            _event.EventData.commits.Length.ShouldBe(2);
            _event.EventData.commits[0].message.ShouldBe("Readme change");
        }
    }
}