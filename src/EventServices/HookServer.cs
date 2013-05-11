using System.Linq;
using GitHub_XMPP.Notifiers;
using Nancy;

namespace GitHub_XMPP.EventServices
{
    public class HookServer : NancyModule
    {
        private readonly IEventNotifier _eventNotifier;

        public HookServer(IEventNotifier eventNotifier)
            : base("/GitHubHooks")
        {
            _eventNotifier = eventNotifier;
            Post["/event"] = parms =>
                {
                    try
                    {
                        HandleGitHubEvent(Request.Headers, Request.Form["payload"]);
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

        private void HandleGitHubEvent(RequestHeaders headers, string githubHookPayload)
        {
            string githubNotificationType = headers.Where(kvp => kvp.Key == "X-GitHub-Event").FirstOrDefault().Value.FirstOrDefault();

            if (githubNotificationType == "push")
            {
                string message = "Received commit notification from GitHub.";
                _eventNotifier.SendText(message);
            }
        }
    }
}