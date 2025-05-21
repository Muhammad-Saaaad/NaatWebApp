using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NaatWebApp.Models
{
    public class Naat
    {
        public String nid { get; set; }
        public int ano { get; set; }
        public int nno { get; set; }
        public String naat { get; set; }
        public String filepath { get; set; }
        public HttpPostedFileBase audiofile { get; set; }
    }
}