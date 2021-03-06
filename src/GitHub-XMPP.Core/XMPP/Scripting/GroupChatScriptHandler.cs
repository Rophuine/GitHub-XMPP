﻿using System;
using System.Configuration;
using System.IO;
using GitHub_XMPP.EventServices;
using GitHub_XMPP.XMPP.Events;
using Jurassic;
using Jurassic.Library;

namespace GitHub_XMPP.XMPP.Scripting
{
    public class GroupChatScriptHandler // : IHandle<GroupChatMessageArrived>
    {
        private readonly IEventNotifier _eventNotifier;

        private string quoteForJS(string value)
        {
            return value.Replace("'", "\\'");
        }

        private string _scriptFolder;

        public GroupChatScriptHandler(IEventNotifier eventNotifier)
        {
            _eventNotifier = eventNotifier;
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

        public string BaseScriptFolder
        {
            get { return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "BaseScripts"); }
        }

        private string GetJSInvoker(GroupChatMessageArrived eventObj)
        {
            return string.Format("message = new MessageObject('{0}','{1}',messageBody);",
                quoteForJS(eventObj.Room),
                quoteForJS(eventObj.Message.From));
        }

        public void Handle(GroupChatMessageArrived eventObject)
        {
            foreach (string file in Directory.GetFiles(ScriptFolder, "*.js"))
            {
                try
                {
                    ArrayInstance result = RunScriptFromFile(eventObject, file);
                    NotifyResults(result);
                }
                catch (Exception ex)
                {
                    _eventNotifier.SendText(string.Format("I'm having trouble with {0}", file));
                }
            }
            foreach (string file in Directory.GetFiles(ScriptFolder, "*.coffee"))
            {
                try
                {
                    ArrayInstance result = RunCoffeeScriptFromFile(eventObject, file);
                    NotifyResults(result);
                }
                catch (Exception ex)
                {
                    _eventNotifier.SendText(string.Format("I'm having trouble with {0}", file));
                }
            }
        }

        private void NotifyResults(ArrayInstance result)
        {
            if (result != null)
            {
                foreach (object message in result.ElementValues)
                    _eventNotifier.SendText(message.ToString());
            }
        }

        private ArrayInstance RunCoffeeScriptFromFile(GroupChatMessageArrived eventObject, string file)
        {
            return RunScript(eventObject, CompileCoffeeScript(File.ReadAllText(file)));
        }

        private ScriptEngine _coffeeCompiler;

        private ScriptEngine CoffeeCompiler
        {
            get
            {
                if (_coffeeCompiler == null)
                {
                    var compiler = new ScriptEngine();
                    compiler.ExecuteFile(Path.Combine(BaseScriptFolder, "coffee-script.js"));
                    compiler.Execute(
                        "var compile = function (src) { return CoffeeScript.compile(src, { bare: true }); };");
                    _coffeeCompiler = compiler;
                }
                return _coffeeCompiler;
            }
        }

        private string CompileCoffeeScript(string coffeeScript)
        {
            return CoffeeCompiler.CallGlobalFunction<string>("compile", coffeeScript);
        }

        private ArrayInstance RunScriptFromFile(GroupChatMessageArrived eventObject, string file)
        {
            return RunScript(eventObject, File.ReadAllText(file));
        }

        private ArrayInstance RunScript(GroupChatMessageArrived eventObject, string js)
        {
            var jsEngine = new ScriptEngine();
            jsEngine.ExecuteFile(Path.Combine(BaseScriptFolder, "hubotScriptInvoker.js"));
            jsEngine.Execute(js);
            jsEngine.SetGlobalValue("messageBody", eventObject.Message.Body);
            jsEngine.Execute(GetJSInvoker(eventObject));
            jsEngine.Execute("module.exports(bot);");
            var result = jsEngine.GetGlobalValue("messages") as ArrayInstance;
            return result;
        }
    }
}