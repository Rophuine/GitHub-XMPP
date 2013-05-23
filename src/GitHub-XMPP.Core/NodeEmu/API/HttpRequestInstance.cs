using System.Linq;
using System.Net;
using Jurassic.Library;

namespace GitHub_XMPP.XMPP.Scripting
{
    public class HttpRequestInstance : ObjectInstance
    {
        private ObjectInstance _options;
        private FunctionInstance _callback;
        public HttpRequestInstance(ObjectInstance instancePrototype, ObjectInstance options, FunctionInstance callback)
            : base(instancePrototype)
        {
            _options = options;
            _callback = callback;
        }

        public HttpRequestInstance(ObjectInstance instancePrototype)
            : base(instancePrototype)
        {
        }

        [JSFunction(Name = "end")]
        public void End()
        {
            var client = new WebClient();
            client.DownloadString(_options.Properties.Where(prop => prop.Name.ToLower() == "url").FirstOrDefault().ToString());
        }

        [JSFunction(Name = "on")]
        public void On(string eventString, FunctionInstance callback)
        {
            "".ToString();
        }

        [JSFunction(Name = "write")]
        public void Write(string data, string encoding)
        {
            "".ToString();
        }
    }
}