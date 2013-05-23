using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using GitHub_XMPP.NodeEmu.API;
using GitHub_XMPP.Services;
using Jurassic;
using Jurassic.Library;
using Path = System.IO.Path;

namespace GitHub_XMPP.NodeEmu
{
    public class NodeJsEngine : IScriptEngine
    {
        private readonly ScriptEngine _engine;
        private readonly IServiceLocator _locator;

        public ScriptEngine JsEngineProvider
        {
            get { return _engine; }
        }

        public string QuoteString(string value)
        {
            return value.Replace("'", "\\'");
        }

        public void RunJavascript(string script)
        {
            _engine.Execute(script);
            _partiallyInstalled.ForEach(c => InstallAPIComponent(c, true));
            _partiallyInstalled.Clear();
        }

        public void RunCoffeeScript(string script)
        {
            RunJavascript(CompileCoffeeScript(script));
        }

        public void RunJavascriptFromFile(string filename)
        {
            RunJavascript(File.ReadAllText(filename));
        }

        public void RunCoffeeScriptFromFile(string filename)
        {
            RunCoffeeScript(File.ReadAllText(filename));
        }

        public void RunScriptFromFile(string filename)
        {
            if (filename.ToLower().EndsWith(".coffee"))
                RunCoffeeScriptFromFile(filename);
            else
                RunJavascriptFromFile(filename);
        }

        public object GetGlobalValue(string name)
        {
            return _engine.GetGlobalValue(name);
        }

        public void SetGlobalValue(string name, object value)
        {
            _engine.SetGlobalValue(name, value);
        }

        public IEnumerable<string> GetGlobalStringArray(string name)
        {
            var jsArray = GetGlobalValue(name) as ArrayInstance;
            if (jsArray != null)
                return jsArray.ElementValues.Cast<string>();
            throw new InvalidCastException(string.Format("{0} cannot be converted to an array.", name));
        }

        public void SetGlobalFunction(string name, Delegate method)
        {
            _engine.SetGlobalFunction(name, method);
        }

        public string BaseScriptFolder
        {
            get { return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "BaseScripts"); }
        }

        private readonly List<INodeAPIComponent> _partiallyInstalled = new List<INodeAPIComponent>();

        internal void InstallAPIComponent(Type component)
        {
            var newComponent = _locator.Resolve(component) as INodeAPIComponent;
            InstallAPIComponent(newComponent);
        }

        private void InstallAPIComponent(INodeAPIComponent newComponent, bool secondInstall = false)
        {
            if (newComponent != null)
            {
                newComponent.Install(this);
                if (!secondInstall)
                    _partiallyInstalled.Add(newComponent);
            }
        }

        public NodeJsEngine(IServiceLocator locator)
        {
            _locator = locator;
            _engine = new ScriptEngine(); // I'll pay the script engine having a fixed dependency for now
            _engine.EnableDebugging = true;
            InstallAPIComponent(typeof (Require));
            //InstallAPIComponent(typeof (API.Path));
        }

        private ScriptEngine _coffeeCompiler;
        private readonly Dictionary<int, string> _coffeeCache = new Dictionary<int, string>();

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
            int hash = coffeeScript.GetHashCode();
            if (_coffeeCache.ContainsKey(hash)) return _coffeeCache[hash];
            var js = CoffeeCompiler.CallGlobalFunction<string>("compile", coffeeScript);
            _coffeeCache.Add(hash, js);
            return js;
        }
    }
}