using System.Linq;
using Nancy;
using Nancy.TinyIoc;

namespace GitHub_XMPP.EventServices
{
    public class HookServer : NancyModule
    {
        public HookServer(GitHubEvent githubEvent)
            : base("/GitHubHooks")
        {
            Post["/event"] = parms =>
                {
                    try
                    {
                        string githubNotificationType = Request.Headers.Where(kvp => kvp.Key == "X-GitHub-Event").FirstOrDefault().Value.FirstOrDefault();
                        githubEvent.HandleGitHubEvent(githubNotificationType, Request.Form["payload"]);
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