using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CMCOMTDashboard.Models
{
    public class Log
    {
        public int logID { get; set; }
        public string username { get; set; }
        public string activity { get; set; }
        public System.DateTime startDateTime { get; set; }
        public System.DateTime? endDateTime { get; set; }
    }
}