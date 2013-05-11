using System.Collections.Generic;
using System.Linq;
using GitHub_XMPP.Notifiers;
using Nancy;
using Nancy.TinyIoc;

namespace GitHub_XMPP.EventServices
{
    public class GitHubEvent
    {
        public void HandleGitHubEvent(string githubHookEvent, string githubHookPayload)
        {
            var jsonParameter =
                new NamedParameterOverloads(new Dictionary<string, object> {{"jsonData", githubHookPayload}});
            if (githubHookEvent == "push")
                TinyIoCContainer.Current.Resolve<GitHubPushEvent>(jsonParameter).Handle();
        }
    }
}