using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using GitHub_XMPP.EventServices;
using GitHub_XMPP.XMPP.Events;

namespace GitHub_XMPP.XMPP.Bot
{
    public class Counter : GitBot
    {
        private readonly IEventNotifier _eventNotifier;

        public Counter(IEventNotifier eventNotifier)
        {
            _eventNotifier = eventNotifier;
            MessageFilter = new Regex("(.*)");
        }

        private readonly Dictionary<string, int> runningCount = new Dictionary<string, int>();

        private readonly Regex count = new Regex("count (.*)", RegexOptions.IgnoreCase);
        private readonly Regex stopcount = new Regex("stop counting (.*)");
        private readonly Regex tally = new Regex("tally (.*)");

        public override void ReceiveGroupMessage(GroupChatMessageArrived message, MatchCollection matches)
        {
            if (count.IsMatch(message.Message.Body))
                StartCounting(count.Match(message.Message.Body).Groups[1].ToString());
            else if (stopcount.IsMatch(message.Message.Body))
                StopCounting(stopcount.Match(message.Message.Body).Groups[1].ToString());
            else if (tally.IsMatch(message.Message.Body))
                ReportTally(tally.Match(message.Message.Body).Groups[1].ToString());
            else
                foreach (string text in runningCount.Keys.ToList())
                {
                    if (message.Message.Body.IndexOf(text, StringComparison.OrdinalIgnoreCase) >= 0)
                        runningCount[text]++;
                }
        }

        private void ReportTally(string text)
        {
            if (!runningCount.ContainsKey(text))
            {
                _eventNotifier.SendText(string.Format("I'm not counting '{0}'!", text));
            }
            else
            {
                _eventNotifier.SendText(string.Format("I've seen '{0}' {1} times since I started counting.", text,
                                                      runningCount[text]));
            }
        }

        private void StopCounting(string text)
        {
            if (!runningCount.ContainsKey(text))
            {
                _eventNotifier.SendText(string.Format("I'm not counting '{0}'!", text));
            }
            else
            {
                _eventNotifier.SendText(string.Format("No longer counting '{0}', I got up to {1}.", text,
                                                      runningCount[text]));
                runningCount.Remove(text);
            }
        }

        private void StartCounting(string text)
        {
            if (runningCount.ContainsKey(text))
            {
                _eventNotifier.SendText(string.Format("I'm already counting {0}! Tally is at {1}.", text,
                                                      runningCount[text]));
            }
            else
            {
                runningCount.Add(text, 0);
                _eventNotifier.SendText(string.Format("I'll count messages containing '{0}'.", text));
            }
        }
    }
}