using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using GitHub_XMPP.Services;
using Jurassic;

namespace GitHub_XMPP.NodeEmu.API
{
    public class Require : INodeAPIComponent
    {
        private NodeJsEngine _installedEngine;

        public void Install(NodeJsEngine engine)
        {
            _installedEngine = engine;
            engine.SetGlobalFunction("require", new Action<string>(RequireImpl));
        }

        public void RequireImpl(string filename)
        {
            var apiType = Assembly.GetExecutingAssembly()
                    .GetTypes()
                    .First(type => type.IsClass && type.Namespace == this.GetType().Namespace && type.Name.ToLower() == filename.ToLower());
            _installedEngine.InstallAPIComponent(apiType);
        }
    }
}
