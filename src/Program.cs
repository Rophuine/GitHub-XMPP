using System;
using System.Threading;
using GitHub_XMPP.EventServices;
using GitHub_XMPP.Notifiers;
using Nancy.Hosting.Self;

namespace GitHub_XMPP
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var host = new NancyHost(new Uri[] {new Uri("http://localhost:6893")});
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