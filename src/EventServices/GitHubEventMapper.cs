using System;
using System.Collections.Generic;
using Castle.Windsor;
using GitHub_XMPP.EventHandlers;

namespace GitHub_XMPP.EventServices
{
    public class GitHubEventMapper
    {
        private readonly Dictionary<string, Type> githubEventTypeMap = new Dictionary<string, Type>
            {
                {"push", typeof (GitHubPushEvent)},
                {"issues", typeof (GitHubIssueEvent)},
                {"issue_comment", typeof (GitHubIssueCommentEvent)},
                {"commit_comment", typeof (GitHubCommitCommentEvent)},
                {"pull_request", typeof (GitHubPullRequestEvent)},
                {"pull_request_review_comment", typeof (GitHubPullRequestReviewCommentEvent)},
                {"gollum", typeof (GitHubWikiUpdateEvent)},
                //{"watch", typeof(GitHubRepoWatchEvent)},  // GitHub doesn't seem to send these!
                //{"download", typeof(GitHubDownloadAddedEvent)},   // GitHub has deprecated downloads!
                {"fork", typeof (GitHubForkEvent)},
                //{"fork_apply", typeof(GitHubForkApplyEvent)}, // I think GitHub has deprecated the fork queue.
                {"member", typeof (GitHubMemberEvent)},
                {"public", typeof (GitHubPublicEvent)},
            };

        public void HandleGitHubEvent(string githubHookEvent, string githubHookPayload)
        {
            WindsorContainer container = IoC.Container;
            var handler = container.Resolve(githubEventTypeMap[githubHookEvent]) as IGitHubEventHandler;
            if (handler != null) handler.Handle(githubHookPayload);
        }
    }
}