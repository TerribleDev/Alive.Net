using Microsoft.AspNet.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Alive.Net
{
    public class AliveOptions
    {
        public PathString LivecheckPath { get; set; } = new PathString("/livecheck");
        public HttpStatusCode ReturnStatusCode { get; set; } = HttpStatusCode.OK;
        public string BodyText { get; set; } = string.Empty;
    }
}