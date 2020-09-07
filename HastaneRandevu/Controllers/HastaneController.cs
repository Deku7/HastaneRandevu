using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using System.Web.SessionState;
using HastaneRandevu.Models;

namespace HastaneRandevu.Controllers
{
    public class HastaneController : Controller
    {
        HastaneEntities1 db = new HastaneEntities1();
        // GET: Hastane
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Doktorlar()
        {
            return View();
        }
        public ActionResult Uzmanliklar()
        {
            return View();
        }
        public ActionResult Randevular()
        {
            List<SelectListItem> doktorlar = (from i in db.Doctor.ToList() select new SelectListItem { Text = i.DoctorName, Value = i.DoctorID.ToString() }).ToList();
            ViewBag.dr = doktorlar;
            List<Randevu> randevular = db.Randevu.ToList();
            ViewBag.randevularlistesi = randevular;
            return View();
        }
        public ActionResult Iletisim()
        {
            return View();
        }


        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(HastaneRandevu.Models.User userModel)
        {
            var UserDetails = db.User.Where(x => x.UserName == userModel.UserName && x.Password == userModel.Password).FirstOrDefault();
            if(UserDetails == null)
            {
                Session["userID"] = null;
                Session["userName"] = null;
                return View("Index");
            }
            else
            {
                Session["userID"] = UserDetails.UserID;
                Session["userName"] = UserDetails.UserName;
                return View("Index");
            }
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(User p)
        {
            db.User.Add(p);
            db.SaveChanges();
            return View("Index");
        }

        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Index");
        }



        [HttpGet]
        public ActionResult Randevu()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Randevu(Randevu p)
        {
            int ID = (int)Session["userID"];
            p.UserID = ID;
            db.Randevu.Add(p);
            db.SaveChanges();
            return View("Index");
        }





    }
} 