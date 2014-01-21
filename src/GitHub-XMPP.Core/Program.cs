using System;
using System.Configuration;
using System.Threading;
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
            HostConfiguration config = new HostConfiguration();
            config.UrlReservations.CreateAutomatically = true;
            var host = new NancyHost(config, new[] {new Uri("http://localhost:6893")});
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