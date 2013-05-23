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
    public class GitHubMemberEventArrives
    {
        private IEventNotifier _notifier;
        private GitHubMemberEvent _event;

        [SetUp]
        public void Setup()
        {
            _notifier = Substitute.For<IEventNotifier>();
            _event = new GitHubMemberEvent(_notifier);

            string text =
                File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SampleJson",
                                              "GitHubMemberJson.txt"));
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
        public void TheActionShouldBeSet()
        {
            _event.EventData.action.ShouldBe("added");
        }

        [Test]
        public void TheSenderShouldBeSet()
        {
            _event.EventData.sender.ShouldNotBe(null);
            _event.EventData.sender.login.ShouldBe("Rophuine");
        }

        [Test]
        public void TheMemberShouldBeSet()
        {
            _event.EventData.member.ShouldNotBe(null);
            _event.EventData.member.login.ShouldBe("NewMember");
        }
    }
}