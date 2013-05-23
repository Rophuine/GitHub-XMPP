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
    internal class GitHubPullRequestEventArrives
    {
        private IEventNotifier _notifier;
        private GitHubPullRequestEvent _event;

        [SetUp]
        public void Setup()
        {
            _notifier = Substitute.For<IEventNotifier>();
            _event = new GitHubPullRequestEvent(_notifier);

            string text =
                File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SampleJson",
                                              "GitHubPullRequestJson.txt"));
            _event.Handle(text);
        }

        [Test]
        public void TheEventNotifierShouldReceiveACall()
        {
            _notifier.Received().SendText(Arg.Any<string>());
        }

        [Test]
        public void TheActionShouldBeSet()
        {
            _event.EventData.action.ShouldBe("synchronize");
        }

        [Test]
        public void TheRepoShouldBeSet()
        {
            _event.EventData.repository.ShouldNotBe(null);
            _event.EventData.repository.name.ShouldBe("TimeZoneInfoGenerator");
        }

        [Test]
        public void TheSenderShouldBeSet()
        {
            _event.EventData.sender.ShouldNotBe(null);
            _event.EventData.sender.login.ShouldBe("Rophuine");
        }

        [Test]
        public void ThePullRequestDetailsShouldBeSet()
        {
            _event.EventData.pull_request.ShouldNotBe(null);
            _event.EventData.pull_request.commits.ShouldBe(3);
            _event.EventData.pull_request.title.ShouldBe("test");
        }
    }
}