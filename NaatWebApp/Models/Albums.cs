using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NaatWebApp.Models
{
	public class Albums
	{
		public string nid { get; set; }
		public int ano { get; set; }
		public string aname { get;set; }
		public int year { get; set; }
		public String filepath { get; set; }
		public HttpPostedFileBase imgfile { get; set; }

        // HttpPostedFileBase => this is a class and is use to store file.
    }
}