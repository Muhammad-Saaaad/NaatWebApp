using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

        // GET: Album
        public ActionResult CreateAlbum()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateAlbum(Albums abl) // Most important
        {
            abl.nid = Session["uid"].ToString();
            string fname = abl.imgfile.FileName; // fname = name.jpg
            string ext = Path.GetExtension(fname); // get the extension from file (.jpg)

            var allowExt = new[] {".jpg",".png",".git",".bnp",".jpeg"};
            if (allowExt.Contains(ext))
            {
                String severfileName = abl.nid + "_" + abl.ano + ext;
                String path = Path.Combine(Server.MapPath("~/images"), severfileName);

                // check if the value is correct or not
                abl.imgfile.SaveAs(path);
                abl.filepath = "/images/"+severfileName;

                db.OpenConnection();
                string query = "Insert into Albums values('" +abl.nid + "' , '" + abl.ano + "' , '" + abl.aname + "' , '" + abl.year + "','"+abl.filepath+"')";
                db.InsertUpdateDelete(query);
                db.CloseConnection();
            }
            return View(abl);
        }

        [HttpGet] // If not written it will still be a httpGet request
        public ActionResult ShowAlbums(String nid) // Most Important (100% in paper)
        {
            List<Albums> a = new List<Albums>();

            String q = "Select filepath, aname, year from albums where nid = '" + nid+"'";
            db.OpenConnection();
            SqlDataReader sdr = db.GetData(q);

            while (sdr.Read())
            {
                a.Add(new Albums() {
                    filepath = sdr["filepath"].ToString(),
                    aname = sdr["aname"].ToString(),
                    year = (int)sdr["year"]
                });
            }
            return View(a);
        }
    }
}
    