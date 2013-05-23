using System;
using System.IO;
using GitHub_XMPP.GitHub.EventHandlers;
using GitHub_XMPP.Services;
using NSubstitute;
using NUnit.Framework;
using Shouldly;

namespace GitHub_XMPP.Tests
{
    [TestFixture]
    internal class GitHubIssueEventArrives
    {
        private IEventNotifier _notifier;
        private GitHubIssueEvent _event;

        [SetUp]
        public void Setup()
        {
            _notifier = Substitute.For<IEventNotifier>();
            _event = new GitHubIssueEvent(_notifier);

            string text =
                File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SampleJson", "GitHubIssueJson.txt"));
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
            _event.EventData.action.ShouldBe("opened");
        }

        [Test]
        public void TheIssueShouldBeSet()
        {
            _event.EventData.issue.ShouldNotBe(null);
            _event.EventData.issue.title.ShouldBe("New issue for testing");
        }

        [Test]
        public void TheUserShouldBeSet()
        {
            _event.EventData.issue.user.ShouldNotBe(null);
            _event.EventData.issue.user.login.ShouldBe("Rophuine");
        }
    }
}