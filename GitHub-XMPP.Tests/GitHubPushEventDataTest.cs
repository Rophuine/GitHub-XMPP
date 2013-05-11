using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using GitHub_XMPP.EventServices;
using GitHub_XMPP.Notifiers;
using NSubstitute;

namespace GitHub_XMPP.Tests
{
    class GitHubPushEventDataTest
    {
        public void SetUp()
        {
            var notifier = Substitute.For<IEventNotifier>();
            var text = File.ReadAllText(@"C:\dev\GitHub-XMPP\src\EventServices\SampleJson\GitHubPushJson.txt");
            var pushEvent = new GitHubPushEvent(notifier, text);
        }
    }
}
