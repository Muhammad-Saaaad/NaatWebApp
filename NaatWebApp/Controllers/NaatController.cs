using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Web;
using System.Web.Mvc;

namespace NaatWebApp.Controllers
{
    public class NaatController : Controller // table name Naats
    {
        public ActionResult AllNaat(String nid, int ano, String title)
        {
            return View();
        }

        [HttpGet] // create Naat is access using url with query string so we use get request
        public ActionResult CreateNaat(String ano, String title)
        {
            return View();
        }
    }
}