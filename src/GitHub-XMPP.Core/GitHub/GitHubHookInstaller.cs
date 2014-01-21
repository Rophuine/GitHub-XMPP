using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using Newtonsoft.Json;
using RestSharp;

namespace GitHub_XMPP.GitHub
{
    public class GitHubHookInstaller
    {
        private static string GitHubUsername
        {
            get { return ConfigurationManager.AppSettings["GitHubUsername"]; }
        }

        private static string GitHubPassword
        {
            get { return ConfigurationManager.AppSettings["GitHubPassword"]; }
        }

        private static string GitHubHookId
        {
            get { return ConfigurationManager.AppSettings["GitHubHookId"]; }
        }

        private static string GitHubRepo
        {
            get { return ConfigurationManager.AppSettings["GitHubRepo"]; }
        }

        public static void InstallGitHubHooksUsingAppConfig()
        {
            if (!string.IsNullOrWhiteSpace(GitHubUsername) && !string.IsNullOrWhiteSpace(GitHubPassword))
            {
                try
                {
                    var hookInstaller = new GitHubHookInstaller(GitHubUsername, GitHubPassword);
                    if (string.IsNullOrWhiteSpace(GitHubHookId))
                    {
                        Console.WriteLine("Attempting to get the list of github hooks so you can choose one...");
                        List<GitHubHookResponse> hooks = hookInstaller.GetAllGitHubHooks(GitHubUsername, GitHubRepo);
                        foreach (GitHubHookResponse hook in hooks)
                        {
                            Console.WriteLine(string.Format("Hook Id {0}: {1} {2}", hook.id, hook.name, hook.config.url));
                        }
                        Console.WriteLine(
                            "Please select one of these and put the hook Id (just the number) into app settings under the key GitHubHookId and I'll configure it for you.");
                    }
                    else
                    {
                        hookInstaller.ConfigureHook(GitHubHookId, GitHubUsername, GitHubRepo);
                    }
                }
                finally
                {
                    Console.ReadLine();
                    Environment.Exit(0);
                }
            }
        }

        private readonly RestClient _gitHubClient;

        public GitHubHookInstaller(string gitHubUsername, string gitHubPassword)
        {
            _gitHubClient = GetNewGitHubClient(gitHubUsername, gitHubPassword);
        }

        public void ConfigureHook(string gitHubHookId, string gitHubUsername, string gitHubRepo)
        {
            var config = new HookSettings();
            config.active = true;
            config.events = new[]
            {
                "push",
                "issues",
                "issue_comment",
                "commit_comment",
                "pull_request",
                "pull_request_review_comment",
                "gollum",
                "watch",
                "download",
                "fork",
                "fork_apply",
                "member",
                "public",
                "team_add",
                "status",
            };
            string json = JsonConvert.SerializeObject(config);
            var request =
                new RestRequest(string.Format("/repos/{0}/{1}/hooks/{2}", gitHubUsername, gitHubRepo, gitHubHookId),
                    Method.PATCH);
            request.RequestFormat = DataFormat.Json;
            request.AddBody(config);
            IRestResponse response = _gitHubClient.Execute(request);
            if (!string.IsNullOrWhiteSpace(response.ErrorMessage) || response.StatusCode != HttpStatusCode.OK)
            {
                Console.WriteLine(string.Format("Encountered an error trying to reconfigure the hook: {0}",
                    response.ErrorMessage));
                throw new Exception("Unable to reconfigure hook.");
            }
            Console.WriteLine("I have reconfigured the hook - you should get notifications for all events now.");
        }

        public List<GitHubHookResponse> GetAllGitHubHooks(string gitHubUsername, string gitHubRepo)
        {
            var request = new RestRequest(string.Format("/repos/{0}/{1}/hooks", gitHubUsername, gitHubRepo), Method.GET);
            IRestResponse<List<GitHubHookResponse>> response = _gitHubClient.Get<List<GitHubHookResponse>>(request);
            if (!string.IsNullOrWhiteSpace(response.ErrorMessage) || response.StatusCode != HttpStatusCode.OK)
            {
                Console.WriteLine(string.Format("Encountered an error trying to get the list of hooks: {0}",
                    response.ErrorMessage));
                throw new Exception("Unable to get list of hooks.");
            }
            if (response.Data.Count == 0)
            {
                Console.WriteLine(
                    "It looks like there are no hooks defined. Please set one up in your repo config - I'll change settings but not create one for you.");
                throw new Exception("List of hooks was empty.");
            }
            return response.Data;
        }

        private static RestClient GetNewGitHubClient(string gitHubUsername, string gitHubPassword)
        {
            var client = new RestClient("https://api.github.com");
            IAuthenticator auth = new HttpBasicAuthenticator(gitHubUsername, gitHubPassword);
            client.Authenticator = auth;
            return client;
        }

        private class HookSettings
        {
            public bool active { get; set; }
            public string[] events { get; set; }
        }

        public class Config
        {
            public string url { get; set; }
            public string content_type { get; set; }
            public string insecure_ssl { get; set; }
        }

        public class LastResponse
        {
            public int code { get; set; }
            public string status { get; set; }
            public string message { get; set; }
        }

        public class GitHubHookResponse
        {
            public string url { get; set; }
            public string test_url { get; set; }
            public int id { get; set; }
            public string name { get; set; }
            public bool active { get; set; }
            public List<string> events { get; set; }
            public Config config { get; set; }
            public LastResponse last_response { get; set; }
            public string updated_at { get; set; }
            public string created_at { get; set; }
        }
    }
}