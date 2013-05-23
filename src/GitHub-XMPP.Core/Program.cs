using System;
using System.Configuration;
using System.Threading;
using GitHub_XMPP.GitHub;
using GitHub_XMPP.Installers;
using Nancy.Hosting.Self;

namespace GitHub_XMPP
{
    internal class Program
    {
        private static bool RunGitHubListener
        {
            get { return bool.Parse(ConfigurationManager.AppSettings["RunGitHubListener"]); }
        }

        private static void Main(string[] args)
        {
            GitHubHookInstaller.InstallGitHubHooksUsingAppConfig();
            IoC.Install();
            var host = new NancyHost(new[] {new Uri("http://localhost:6893")});
            try
            {
                if (RunGitHubListener) host.Start();
                while (true)
                    Thread.Sleep(50000);
            }
            finally
            {
                if (RunGitHubListener) host.Stop();
            }
        }
    }
}