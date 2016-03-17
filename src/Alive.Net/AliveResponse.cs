using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Alive.Net
{
    public class AliveResponse
    {
        public string BodyText { get; set; } = String.Empty;

        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;
    }
}