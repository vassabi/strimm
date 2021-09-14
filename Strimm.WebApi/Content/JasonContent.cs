using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace Strimm.WebApi.Content
{
    public class JasonContent : HttpContent
    {
        private readonly JToken jtoken;

        public JasonContent(JToken jtoken)
        {
            this.jtoken = jtoken;
            Headers.ContentType = new MediaTypeHeaderValue("application/json");
        }

        protected override Task SerializeToStreamAsync(Stream stream, TransportContext context)
        {
            var jsonTextWriter = new Newtonsoft.Json.JsonTextWriter(new StreamWriter(stream))
            {
                Formatting = Formatting.Indented
            };

            this.jtoken.WriteTo(jsonTextWriter);
            jsonTextWriter.Flush();

            return Task.FromResult<object>(null);
        }

        protected override bool TryComputeLength(out long length)
        {
            length = -1;
            return false;
        }
    }
}