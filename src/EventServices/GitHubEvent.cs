﻿using System;
using System.Collections.Generic;
using GitHub_XMPP.EventHandlers;
using Nancy.TinyIoc;

namespace GitHub_XMPP.EventServices
{
    public class GitHubEvent
    {
        private Dictionary<string, Type> githubEventTypeMap = new Dictionary<string, Type>
            {
                {"push", typeof(GitHubPushEvent)},
                {"issues", typeof(GitHubIssueEvent)},
                {"issue_comment", typeof(GitHubIssueCommentEvent)},
                {"commit_comment", typeof(GitHubCommitCommentEvent)},
                {"pull_request", typeof(GitHubPullRequestEvent)},
                {"pull_request_review_comment", typeof(GitHubPullRequestReviewCommentEvent)}
            };

        public void HandleGitHubEvent(string githubHookEvent, string githubHookPayload)
        {
            var container = TinyIoCContainer.Current;
            var handler = container.Resolve(githubEventTypeMap[githubHookEvent]) as IGitHubEventHandler;
            if (handler != null) handler.Handle(githubHookPayload);
        }
    }
}