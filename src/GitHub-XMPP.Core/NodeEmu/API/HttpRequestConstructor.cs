using Jurassic;
using Jurassic.Library;

namespace GitHub_XMPP.XMPP.Scripting
{
    public class HttpRequestConstructor : ClrFunction
    {
        public HttpRequestConstructor(ScriptEngine engine)
            : base(engine.Function.InstancePrototype, "HttpRequest", new HttpRequestInstance(engine.Object.InstancePrototype))
        {
        }

        [JSConstructorFunction]
        public HttpRequestInstance Construct(ObjectInstance options, FunctionInstance callback)
        {
            return new HttpRequestInstance(this.InstancePrototype, options, callback);
        }
    }
}