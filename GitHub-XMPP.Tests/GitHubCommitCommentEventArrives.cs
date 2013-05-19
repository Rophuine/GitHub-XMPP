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
    internal class GitHubCommitCommentEventArrives
    {
        private IEventNotifier _notifier;
        private GitHubCommitCommentEvent _event;

        [SetUp]
        public void Setup()
        {
            _notifier = Substitute.For<IEventNotifier>();
            _event = new GitHubCommitCommentEvent(_notifier);

            string text =
                File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SampleJson",
                                              "GitHubCommitCommentJson.txt"));
            _event.Handle(text);
        }

        [Test]
        public void TheEventNotifierShouldReceiveACall()
        {
            _notifier.Received().SendText(Arg.Any<string>());
        }

        [Test]
        public void TheSenderShouldBeSet()
        {
            _event.EventData.sender.ShouldNotBe(null);
            _event.EventData.sender.login.ShouldBe("Rophuine");
        }

        [Test]
        public void TheRepoShouldBeSet()
        {
            _event.EventData.repository.ShouldNotBe(null);
            _event.EventData.repository.name.ShouldBe("TimeZoneInfoGenerator");
        }

        [Test]
        public void TheCommentShouldBeSet()
        {
            _event.EventData.comment.ShouldNotBe(null);
            _event.EventData.comment.body.ShouldBe("test comment");
        }
    }
}