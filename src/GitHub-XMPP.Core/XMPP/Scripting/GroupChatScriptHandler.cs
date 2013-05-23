using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using GitHub_XMPP.NodeEmu;
using GitHub_XMPP.Services;
using GitHub_XMPP.XMPP.Events;

namespace GitHub_XMPP.XMPP.Scripting
{
    public class GroupChatScriptHandler : IHandle<GroupChatMessageArrived>
    {
        private readonly IEventNotifier _eventNotifier;
        private readonly IServiceLocator _locator;

        private string _scriptFolder;

        public string BaseScriptFolder
        {
            get { return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "BaseScripts"); }
        }

        public GroupChatScriptHandler(IEventNotifier eventNotifier, IServiceLocator locator)
        {
            _eventNotifier = eventNotifier;
            _locator = locator;
        }

        public string ScriptFolder
        {
            set { _scriptFolder = value; }
            get
            {
                if (!string.IsNullOrWhiteSpace(_scriptFolder)) return _scriptFolder;
                string configVal = ConfigurationManager.AppSettings["ScriptFolder"];
                if (!string.IsNullOrWhiteSpace(configVal)) return configVal;
                return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Scripts");
            }
        }

        private IEnumerable<string> GetAllScriptFiles()
        {
            return Directory.GetFiles(ScriptFolder, "*.js").Union(Directory.GetFiles(ScriptFolder, "*.coffee"));
        }

        private readonly Dictionary<string, IScriptEngine> engines = new Dictionary<string, IScriptEngine>();

        public void Handle(GroupChatMessageArrived eventObject)
        {
            foreach (string file in GetAllScriptFiles())
            {
                try
                {
                    if (!engines.ContainsKey(file))
                    {
                        engines.Add(file, _locator.Resolve<IScriptEngine>());
                        // FIXME the following line could be .Net provided - it's the hubot api
                        engines[file].RunScriptFromFile(Path.Combine(BaseScriptFolder, "hubotScriptInvoker.js"));
                        engines[file].RunScriptFromFile(file);
                    }
                    IEnumerable<string> result = RunHubot(engines[file], eventObject);
                    NotifyResults(result);
                }
                catch (Exception ex)
                {
                    _eventNotifier.SendText(string.Format("I'm having trouble with {0}", file));
                }
            }
        }

        private void NotifyResults(IEnumerable<string> result)
        {
            if (result != null)
            {
                foreach (string message in result)
                    _eventNotifier.SendText(message);
            }
        }

        private string GetHubotInvoker(IScriptEngine scriptEngine, GroupChatMessageArrived eventObj)
        {
            return string.Format("message = new MessageObject('{0}','{1}', messageBody); module.exports(bot);",
                                 scriptEngine.QuoteString(eventObj.Room),
                                 scriptEngine.QuoteString(eventObj.Message.From));
        }

        private IEnumerable<string> RunHubot(IScriptEngine scriptEngine, GroupChatMessageArrived eventObject)
        {
            scriptEngine.SetGlobalValue("messageBody", eventObject.Message.Body);
            scriptEngine.RunJavascript(string.Format("message = new MessageObject('{0}','{1}', messageBody);",
                                                     scriptEngine.QuoteString(eventObject.Room),
                                                     scriptEngine.QuoteString(eventObject.Message.From)));
            scriptEngine.RunJavascript("module.exports(bot);");
            return scriptEngine.GetGlobalStringArray("messages");
        }
    }
}