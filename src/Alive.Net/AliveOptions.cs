using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Alive.Net
{
    public class AliveOptions
    {
        public PathString LivecheckPath { get; set; } = new PathString("/livecheck");
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;
        public string BodyText { get; set; } = string.Empty;

        public Action<AliveResponse> OnLivecheckResponse { get; set; } = null;
    }
}