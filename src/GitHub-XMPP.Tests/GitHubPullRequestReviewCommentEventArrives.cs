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
    internal class GitHubPullRequestReviewCommentEventArrives
    {
        private IEventNotifier _notifier;
        private GitHubPullRequestReviewCommentEvent _event;

        [SetUp]
        public void Setup()
        {
            _notifier = Substitute.For<IEventNotifier>();
            _event = new GitHubPullRequestReviewCommentEvent(_notifier);

            string text =
                File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SampleJson",
                                              "GitHubPullRequestReviewCommentJson.txt"));
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
        public void TheSenderShouldBeSet()
        {
            _event.EventData.sender.ShouldNotBe(null);
            _event.EventData.sender.login.ShouldBe("Rophuine");
        }

        [Test]
        public void TheCommentShouldHaveData()
        {
            _event.EventData.comment.ShouldNotBe(null);
            _event.EventData.comment.body.ShouldBe("Woah. Woah, woah, woah.");
        }

        [Test]
        public void TheUserShouldBeSet()
        {
            _event.EventData.comment.user.ShouldNotBe(null);
            _event.EventData.comment.user.login.ShouldBe("Rophuine");
        }
    }
}