using System;
using System.IO;
using GitHub_XMPP.EventServices;
using GitHub_XMPP.GitHub.EventHandlers;
using NSubstitute;
using NUnit.Framework;
using Shouldly;

namespace GitHub_XMPP.Tests
{
    internal class SeeBotResponses
    {
        // This class is a working space to shove events into the bot and see what comes out. It's not meant for automated testing.
        private IEventNotifier _notifier;
        private string _botText;

        [SetUp]
        public void Setup()
        {
            _notifier = Substitute.For<IEventNotifier>();
            _notifier.When(noti => noti.SendText(Arg.Any<string>())).Do(call => _botText = (string) call[0]);
        }

        [Test]
        public void SeeWikiUpdateResponse()
        {
            var _event = new GitHubWikiUpdateEvent(_notifier);

            string text =
                File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SampleJson",
                                              "GitHubGollumJson.txt"));
            _event.Handle(text);

            //if (Debugger.IsAttached) Debugger.Break();
            _botText.ShouldNotBeEmpty();
        }
    }
}