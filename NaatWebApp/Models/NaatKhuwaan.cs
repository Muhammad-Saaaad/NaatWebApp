using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NaatWebApp.Models
{
	public class NaatKhuwaan
	{
		public string nid { get; set; }
        public string fullname { get; set; }
        public string city { get; set; }
        public char gender { get; set; }
        public bool alive { get; set; }
        public string email { get; set; }
        public string password { get; set; }

    }
}