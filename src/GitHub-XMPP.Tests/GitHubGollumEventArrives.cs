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
    internal class GitHubGollumEventArrives
    {
        private IEventNotifier _notifier;
        private GitHubWikiUpdateEvent _event;

        [SetUp]
        public void Setup()
        {
            _notifier = Substitute.For<IEventNotifier>();
            _event = new GitHubWikiUpdateEvent(_notifier);

            string text =
                File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SampleJson",
                                              "GitHubGollumJson.txt"));
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
        public void ThePageDetailsShouldBeSet()
        {
            _event.EventData.pages.ShouldNotBe(null);
            _event.EventData.pages.Count.ShouldBe(1);
            _event.EventData.pages[0].title.ShouldBe("Home");
            _event.EventData.pages[0].action.ShouldBe("edited");
        }
    }
}