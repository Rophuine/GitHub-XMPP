using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using GitHub_XMPP.EventHandlers;
using GitHub_XMPP.Notifiers;
using NSubstitute;
using NUnit.Framework;
using Shouldly;

namespace GitHub_XMPP.Tests
{
    class SeeBotResponses
    {
        // This class is a working space to shove events into the bot and see what comes out. It's not meant for automated testing.
        IEventNotifier _notifier;
        string _botText = null;
        [SetUp]
        public void Setup()
        {
            _notifier = Substitute.For<IEventNotifier>();
            _notifier.When(noti => noti.SendText(Arg.Any<string>())).Do(call => _botText = (string)call[0]);
            
        }

        [Test]
        public void SeeWikiUpdateResponse()
        {
            var _event = new GitHubWikiUpdateEvent(_notifier);

            string text =
                File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SampleJson", "GitHubGollumJson.txt"));
            _event.Handle(text);

            if (Debugger.IsAttached) Debugger.Break();
            _botText.ShouldNotBeEmpty();
        }
    }
}
