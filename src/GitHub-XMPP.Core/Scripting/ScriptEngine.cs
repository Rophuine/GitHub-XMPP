using System;
using System.IO;

namespace GitHub_XMPP.Scripting
{
    public class ScriptEngine
    {
        public static string BaseScriptFolder
        {
            get { return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "BaseScripts"); }
        }
    }
}