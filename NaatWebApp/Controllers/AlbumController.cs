using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using NaatWebApp.Models;

namespace NaatWebApp.Controllers
{
    public class AlbumController : Controller
    {
        DBAccess db = new DBAccess();
        static int id = 0;

        // GET: Album
        public ActionResult CreateAlbum()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateAlbum(Albums abl) // Most important
        {
            //abl.nid = abl.email.Split('@')[0];

            abl.nid = Session["nid"].ToString();
            string fname = abl.imgfile.FileName; // fname = name.jpg
            string ext = Path.GetExtension(fname); // get the extension from file (.jpg)

            var allowExt = new[] {".jpg",".png",".git",".bnp"};

            if (allowExt.Contains(ext))
            {
                String severfileName = abl.nid + "_" + abl.ano + ext;
                String path = Path.Combine(Server.MapPath("~/images"), severfileName);
                // check if the value is correct or not

                abl.imgfile.SaveAs(path);
                abl.filepath = "/images/"+severfileName;
            }

            db.OpenConnection();
            string query = "Insert into Albums values('" + id + "' , '" + abl.ano + "' , '" + abl.aname + "' , '" + abl.year + "')";
            db.InsertUpdateDelete(query);
            db.CloseConnection();

            id++;
            return View(abl);
        }
    }
}
    