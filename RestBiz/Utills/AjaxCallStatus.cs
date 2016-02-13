using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestBiz.Ajax
{
    public class AjaxCallStatus
    {

        public AjaxCallStatus(int status, Object retVal)
        {
            this.status = status;
            this.retVal = retVal;
        }

        public int status { get; set; }
        public Object retVal { get; set; }

        
    }
}