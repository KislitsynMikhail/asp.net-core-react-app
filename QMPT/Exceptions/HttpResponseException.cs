using Newtonsoft.Json;
using System;
using System.Net;

namespace QMPT.Exceptions
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class HttpResponseException : Exception
    {
        [JsonProperty]
        [JsonRequired]
        public virtual string Title { get; }

        [JsonProperty]
        public virtual HttpStatusCode StatusCode => HttpStatusCode.BadRequest;

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public new virtual string Data { get; set; } = string.Empty;

        protected HttpResponseException() { }
    }
}
