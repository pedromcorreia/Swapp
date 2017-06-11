using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bcredi.Models
{
    public class Error
    {
        public int HttpStatusCode { get; set; }
        public Exception Exception { get; set; }
        public string Url { get; set; }
        public string ControllerSourceError { get; set; }
        public string ActionSourceError { get; set; }
    }
}