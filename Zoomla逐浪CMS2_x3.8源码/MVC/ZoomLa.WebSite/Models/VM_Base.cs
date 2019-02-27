using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZoomLaCMS.Models
{
    public class VM_Base
    {
        public HttpRequest Request = HttpContext.Current.Request;
    }
}