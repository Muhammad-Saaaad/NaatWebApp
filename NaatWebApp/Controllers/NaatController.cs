using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Web;
using System.Web.Mvc;
using NaatWebApp.Models;

namespace NaatWebApp.Controllers
{
    public class NaatController : Controller // table name Naats
    {
        DBAccess db = new DBAccess();
        public ActionResult AllNaat(String nid, int ano)
        {
            List<Naat> nlist=new List<Naat>();
            db.OpenConnection();
            string q="select nno,naat,filepath from Naats where nid='"+nid + "' and  ano='"+ano+"'"; 
            SqlDataReader sdr=db.GetData(q);
            while (sdr.Read())
            {
                Naat n=new Naat();
                n.nno = int.Parse(sdr["nno"].ToString());
                n.naat = sdr["nnat"].ToString();
                n.filepath = sdr["filepath"].ToString();
                nlist.Add(n);

            }
            sdr.Close();
            db.CloseConnection();
            return View(nlist);
        }

        [HttpGet] // create Naat is access using url with query string so we use get request
        public ActionResult CreateNaat(Naat n, String AlbumNo)
        {
            n.nid = Session["nid"].ToString();
            n.ano = int.Parse(AlbumNo);

            String filename = n.audiofile.FileName;
            String ext = Path.GetExtension(filename);

            if(ext ==".mp3" || ext == ".MP3")
            {
                n.naat = n.naat.Split('.')[0];
                String path = Path.Combine(Server.MapPath("~/audio"), filename);
                n.audiofile.SaveAs(path); // 100% Important in paper

                n.filepath = "/audio"+filename;
                String query = $"insert into naat values ('{n.nid}', {n.ano}, '{n.nno}','{n.naat}', '{n.filepath}')";
                db.OpenConnection();
                db.InsertUpdateDelete(query);
                db.CloseConnection();
            }
            return View(n);

        }
    }
}