using System.IO;
using GitHub_XMPP.EventHandlers;
using GitHub_XMPP.Notifiers;
using NSubstitute;

namespace GitHub_XMPP.Tests
{
    internal class GitHubPushEventDataTest
    {
        public void SetUp()
        {
            var notifier = Substitute.For<IEventNotifier>();
            string text = File.ReadAllText(@"C:\dev\GitHub-XMPP\src\EventServices\SampleJson\GitHubPushJson.txt");
            var pushEvent = new GitHubPushEvent(notifier);
        }
    }
}