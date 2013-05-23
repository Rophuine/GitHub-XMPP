using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jurassic;
using Jurassic.Library;

namespace GitHub_XMPP.NodeEmu.API
{
    public class Path : INodeAPIComponent
    {
        private PathImpl _pathImpl;

        public void Install(NodeJsEngine engine)
        {
            _pathImpl = new PathImpl(engine.JsEngineProvider);
            engine.SetGlobalValue("path", _pathImpl);
        }
    }
    public class PathImpl : ObjectInstance
    {
        public PathImpl(ScriptEngine engine)
            : base(engine)
        {
            PopulateFunctions();
        }

        [JSFunction(Name = "join")]
        public string Join(string a, string b)
        {
            return String.Join("/", a, b);
        }
    }
}
