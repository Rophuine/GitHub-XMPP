using System.Linq;
using GitHub_XMPP.EventServices;
using Nancy;

namespace GitHub_XMPP.GitHub
{
    public class GitHubHookServer : NancyModule
    {
        public GitHubHookServer(GitHubEventMapper githubEventMapper)
            : base("/GitHubHooks")
        {
            Post["/event"] = parms =>
                {
                    try
                    {
                        string githubNotificationType =
                            Request.Headers.Where(kvp => kvp.Key == "X-GitHub-Event")
                                   .FirstOrDefault()
                                   .Value.FirstOrDefault();
                        githubEventMapper.HandleGitHubEvent(githubNotificationType, Request.Form["payload"]);
                        return Response.AsText("Thanks GitHub!");
                    }
                    catch
                    {
                        Response error = Response.AsText("Failed to process message.");
                        error.StatusCode = HttpStatusCode.InternalServerError;
                        return error;
                    }
                };
        }
    }
}