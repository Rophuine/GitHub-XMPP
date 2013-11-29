using System.Diagnostics;
using System.IO;
using System.Reflection;
using GitHub_XMPP.Scripting;
using NUnit.Framework;
using Shouldly;

namespace GitHub_XMPP.Tests.ScriptEngine
{
    public class CoffeeScriptCompilerWorks
    {
        [Test]
        public void CompileCoffeeScript()
        {
            string coffee;
            coffee = ReadResourceFile("GitHub_XMPP.Tests.Scripts.applause.coffee");
            var scriptHandler = new CoffeescriptCompiler();
            var compiled = scriptHandler.Compile(coffee);
            var expected = ReadResourceFile("GitHub_XMPP.Tests.Scripts.applause.js");
            compiled.ShouldBe(expected);
        }

        private string ReadResourceFile(string filename)
        {
            string coffee;
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(filename))
            {
                Debug.Assert(stream != null, string.Format("Could not find resource {0}.", filename));
                using (var reader = new StreamReader(stream))
                    coffee = reader.ReadToEnd();
            }
            return coffee;
        }
    }
}