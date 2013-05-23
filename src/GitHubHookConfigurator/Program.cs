using System;
using System.Collections.Generic;
using CommandLine;
using GitHub_XMPP.GitHub;
using GitHub_XMPP.Installers;

namespace GitHubHookConfigurator
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var options = new CommandLineOptions();
            if (Parser.Default.ParseArguments(args, options))
            {
                var configurator = new GitHubConfigurator();
                if (options.Interactive)
                    configurator.RunInteractiveConfigurator(options);
                configurator.ConfigureHooks(options);
                if (options.Interactive)
                {
                    Console.WriteLine("Hook successfully configured - enter to exit.");
                    Console.ReadLine();
                }
            }
        }
    }

    internal class GitHubConfigurator
    {
        private GitHubHookInstaller _installer;

        public void RunInteractiveConfigurator(CommandLineOptions options)
        {
            if (string.IsNullOrWhiteSpace(options.Username)) options.Username = GetUsername();
            if (string.IsNullOrWhiteSpace(options.Password)) options.Password = GetPassword();
            if (string.IsNullOrWhiteSpace(options.Repo)) options.Repo = GetRepo();
            _installer = _installer ?? new GitHubHookInstaller(options.Username, options.Password);
            if (string.IsNullOrWhiteSpace(options.HookId)) options.HookId = GetHookId(options);
        }

        private string GetHookId(CommandLineOptions options)
        {
            string hookid = null;
            List<GitHubHookInstaller.GitHubHookResponse> hooks = _installer.GetAllGitHubHooks(options.Username,
                                                                                              options.Repo);
            for (int i = 0; i < hooks.Count; i++)
            {
                Console.WriteLine(string.Format("{0}: {1} {2} ({3})", i + 1, hooks[i].name, hooks[i].config.url,
                                                hooks[i].id));
            }
            Console.WriteLine("Please select which hook to use (enter the first number on the line):");
            for (int i = 0; string.IsNullOrWhiteSpace(hookid) && i < 3; i++)
            {
                string line = Console.ReadLine();
                int index;
                if (int.TryParse(line, out index) && index > 0 && index <= hooks.Count)
                {
                    hookid = hooks[index - 1].id.ToString();
                }
            }
            return hookid;
        }

        private static string GetRepo()
        {
            Console.WriteLine("Enter the GitHub repo name to configure a hook for:");
            return GetStringValue();
        }

        private static string GetPassword()
        {
            Console.WriteLine("Enter your GitHub password:");
            return GetStringValue();
        }

        private static string GetUsername()
        {
            Console.WriteLine("Enter your GitHub username:");
            return GetStringValue();
        }

        private static string GetStringValue()
        {
            string username = null;
            for (int i = 0; String.IsNullOrWhiteSpace(username) && i < 3; i++)
            {
                string readLine = Console.ReadLine();
                if (readLine != null) username = readLine.Trim();
            }
            if (String.IsNullOrWhiteSpace(username)) throw new Exception("Failed to get configuration.");
            return username;
        }

        public void ConfigureHooks(CommandLineOptions options)
        {
            _installer = _installer ?? new GitHubHookInstaller(options.Username, options.Password);
            _installer.ConfigureHook(options.HookId, options.Username, options.Repo);
        }
    }

    internal class CommandLineOptions
    {
        [Option('u', "user", HelpText = "GitHub username", Required = false)]
        public string Username { get; set; }

        [Option('p', "password", HelpText = "GitHub password", Required = false)]
        public string Password { get; set; }

        [Option('r', "repo", HelpText = "GitHub repository name", Required = false)]
        public string Repo { get; set; }

        [Option('h', "hookid", HelpText = "GitHub hook id", Required = false)]
        public string HookId { get; set; }

        [Option('i', "interactive", HelpText = "Run in interactive mode", Required = false, DefaultValue = true)]
        public bool Interactive { get; set; }
    }
}