using System;
using System.Diagnostics;
using System.IO;
using IronJS;

namespace GitHub_XMPP.Scripting
{
    public class CoffeescriptCompiler
    {
        //private EcmaScriptComponent _coffeeCompiler;

        /*private EcmaScriptComponent CoffeeCompiler
        {
            get
            {
                if (_coffeeCompiler == null)
                {
                    var compiler = new EcmaScriptComponent();
                    compiler.Source = File.ReadAllText(Path.Combine(ScriptEngine.BaseScriptFolder, "coffee-script.js"));
                    compiler.Run();
                    compiler.Source = "function compiler(src) { return CoffeeScript.compile(src, { bare: true }); };";
                    compiler.Run();
                    _coffeeCompiler = compiler;
                }
                return _coffeeCompiler;
            }
        }*/

        public string Compile(string coffeeScript)
        {
            var cx = new IronJS.Hosting.CSharp.Context();
            string result;
            //var scope = cx.InitStandardObjects();
            var compString = File.ReadAllText(Path.Combine(ScriptEngine.BaseScriptFolder, "coffee-script.js"));
            var frame = new StackTrace().GetFrame(0);
            cx.Execute(compString);
            cx.Execute("function compiler(src) { return CoffeeScript.compile(src, { bare: true }); };");
            var fn = cx.GetGlobal("compiler");
            //result = (string)fn.Call(cx, scope, scope, new object[] {coffeeScript});
            result = cx.GetFunctionAs<Func<string, string>>("compiler").Invoke(coffeeScript);
            return result;
            //return (string) CoffeeCompiler.RunFunction("CoffeeScript.compile", coffeeScript);
        }
    }
}