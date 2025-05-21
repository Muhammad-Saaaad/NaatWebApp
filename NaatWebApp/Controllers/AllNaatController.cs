using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Web;
using System.Web.Mvc;

namespace NaatWebApp.Controllers
{
    public class AllNaatController : Controller // table name Naats
    {
        public ActionResult AllNaat(String nid, int ano, String title)
        {
            return View();
        }

        public ActionResult CreateNaat()
        {
            return View();
        }
    }
}