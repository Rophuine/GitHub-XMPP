using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jurassic.Library;

namespace GitHub_XMPP.NodeEmu
{
    public interface IScriptEngine
    {
        string QuoteString(string value);

        void RunJavascript(string script);
        void RunCoffeeScript(string script);

        void RunJavascriptFromFile(string filename);
        void RunCoffeeScriptFromFile(string filename);
        void RunScriptFromFile(string filename);
        object GetGlobalValue(string name);
        void SetGlobalValue(string name, object value);
        IEnumerable<string> GetGlobalStringArray(string name);
        void SetGlobalFunction(string name, Delegate method);
    }
}
