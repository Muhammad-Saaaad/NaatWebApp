using Microsoft.SqlServer.Server;
using NaatWebApp.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Permissions;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace NaatWebApp.Controllers
{
    public class NaatKhuwaanController : Controller
    {
        DBAccess db = new DBAccess();

        // GET: NaatKhawaan
        [HttpGet]
        public ActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SignUp(NaatKhuwaan nk)
        {
            nk.nid = nk.email.Split('@')[0];
            db.OpenConnection();
            string query = "Insert into naatkhuwaan values('" + nk.nid + "' , '" + nk.fullname + "' , '" + nk.city + "' , '" + nk.gender + "', '" + nk.alive + "' , '" + nk.email + "', '" + nk.password + "')";
            db.InsertUpdateDelete(query);
            db.CloseConnection();

            return View(nk);
        }

        public ActionResult Signin()
        {
            return View();
        }


        /*Notes
        web is stateless , so we have to manage the state ourself
        Web state management

        Client Side:
            Query string
            hidden state
            View state 
            Cookies

        Server Side:
            Session
                used to manage user's information
            Application
                used to manage website's information
            
        */

        [HttpPost]
        public ActionResult Signin(NaatKhuwaan nk)
        {
            db.OpenConnection();
            string query = "Select nid , fullname from NaatKhuwaan where nid = '" + nk.nid + "'   and  password = '" + nk.password + "'";
            SqlDataReader sdr = db.GetData(query);
            if (sdr.HasRows)
            {
                sdr.Read();
                Session["uid"] = sdr["nid"].ToString();
                Session["fn"] = sdr["fullname"].ToString();


                sdr.Close();
            }
            else
            {
                Response.Write("<script> alert('Invalid User!!!');</script>");
            }
            db.CloseConnection();

            return RedirectToAction("Dashboard");
        }

        public ActionResult Dashboard()
        {
            if (Session["uid"] != null)
            {
                return View();
            }
            return RedirectToAction("SignUp");
        }


        [HttpGet]
        public ActionResult AllNaatKhuwaan()
        {
            List<NaatKhuwaan> nklist = new List<NaatKhuwaan>();
            db.OpenConnection();
            string query = "select nid,fullname from NaatKhuwaan";
            SqlDataReader sdr = db.GetData(query);
            while (sdr.Read())
            {
                NaatKhuwaan nk = new NaatKhuwaan();

                nk.nid = sdr["nid"].ToString();
                nk.fullname = sdr["fullname"].ToString();
                nklist.Add(nk);
            }
            sdr.Close();
            db.CloseConnection();
            return View(nklist);
        }

        [HttpPost]
        public ActionResult AllNaatKhuwaan(string cities)
        {
            List<NaatKhuwaan> nklist = new List<NaatKhuwaan>();
            db.OpenConnection();
            string query = "select nid,fullname from NaatKhuwaan where city = '" + cities + "'";
            SqlDataReader sdr = db.GetData(query);
            while (sdr.Read())
            {
                NaatKhuwaan nk = new NaatKhuwaan();

                nk.nid = sdr["nid"].ToString();
                nk.fullname = sdr["fullname"].ToString();
                nklist.Add(nk);
            }
            sdr.Close();
            db.CloseConnection();
            return View(nklist);
        }

        [HttpGet]
        public ActionResult Delete(String nid)
        {
            db.OpenConnection();
            string query = "delete from naatKhuwaan where nid = '" + nid + "'";
            db.InsertUpdateDelete(query);
            db.CloseConnection();
            return RedirectToAction("AllNaatKhuwaan");
        }

        public ActionResult Details(string nid)
        {
            db.OpenConnection();
            string query = "select * from NaatKhuwaan where nid ='" + nid + "'";
            SqlDataReader sdr = db.GetData(query);

            sdr.Read();
            NaatKhuwaan nk = new NaatKhuwaan();
            nk.nid = sdr["nid"].ToString();
            nk.fullname = sdr["fullname"].ToString();
            nk.city = sdr["city"].ToString();
            nk.gender = char.Parse(sdr["gender"].ToString());
            nk.alive = bool.Parse(sdr["alive"].ToString());
            nk.email = sdr["Email"].ToString();
            nk.password = sdr["Password"].ToString();
            sdr.Close();

            db.CloseConnection();
            return View(nk);
        }

        [HttpGet]
        public ActionResult Edit(string nid)
        {
            db.OpenConnection();
            string query = "select * from NaatKhuwaan where nid ='" + nid + "'";
            SqlDataReader sdr = db.GetData(query);
            sdr.Read();

            NaatKhuwaan nk = new NaatKhuwaan();

            nk.nid = sdr["nid"].ToString();
            nk.fullname = sdr["fullname"].ToString();
            nk.city = sdr["city"].ToString();
            nk.gender = char.Parse(sdr["gender"].ToString());
            nk.alive = bool.Parse(sdr["alive"].ToString());
            nk.email = sdr["Email"].ToString();
            nk.password = sdr["Password"].ToString();

            sdr.Close();
            db.CloseConnection();

            return View(nk);
        }

        [HttpPost]
        public ActionResult Edit(NaatKhuwaan nk)
        {
            db.OpenConnection();
            string query = "update naatKhuwaan set fullname = '"+nk.fullname+"' , email = '"+nk.email+"' , city = '"+nk.city+"' where nid = '" + nk.nid + "'";
            db.InsertUpdateDelete(query);
            db.CloseConnection();
            return RedirectToAction("AllNaatKhuwaan");
            
        }
    }
}
