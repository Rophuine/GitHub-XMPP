using System;
using System.Threading;
using GitHub_XMPP.Installers;
using Nancy.Hosting.Self;

namespace GitHub_XMPP
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            GitHubHookInstaller.InstallGitHubHooksUsingAppConfig();
            IoCInstaller.Install();
            var host = new NancyHost(new[] {new Uri("http://localhost:6893")});
            try
            {
                host.Start();
                while (true)
                    Thread.Sleep(50000);
            }
            finally
            {
                host.Stop();
            }
        }
    }
}