using GatheringDay.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Net;
using System.Net.Mail;

namespace GatheringDay.Controllers
{
    public class HomeController : Controller
    {
        GatheringContext Db;
        public HomeController()
        {
            Db = new GatheringContext();
        }
        // GET: Home
        [HttpGet]
        public ActionResult Index()
        {
            
            return View();
        }

        #region Events
        [HttpGet]
        public ActionResult Events()
        {
            return View();
        }
        #endregion

        #region Logout
        public ActionResult Logout()
        {
            Session["id"] = null;
            Session["log"] = null;
            Session["EventName"] = null;
            return RedirectToAction("Index");
        }
        #endregion

        #region About Us
        [HttpGet]
        public ActionResult AboutUs()
        {
            return View();
        }
        #endregion

        #region ContactUs
        [HttpGet]
        public ActionResult ContactUs()
        {

            return View();
        }
        [HttpPost]
        public ActionResult ContactUs( ContactUs contact)
        {
            
                if (ModelState.IsValid)
                {
                    Db.Contacts.Add(contact);
                    Db.SaveChanges();
                contact.ContacterrorMsg = "Message delivered";
                  return RedirectToAction("ContactUs", contact);
                     }
                else
                {
                contact.ContacterrorMsg = "  The message has not been received";


                    return View("ContactUs", contact);
                }
}
        #endregion

       

        #region REservation

        [HttpGet]
        public ActionResult Reservation( string l)
        {
            Reservation reservation = new Reservation();
            reservation.EventName = l;
            Session["EventName"] = l;
            if (Session["id"] == null)
            {
                return View( "Login" );
            }
            return View( reservation);
        }
        [HttpPost]
        public ActionResult Reservation(Reservation reservation)
        {
            
            if (Session["id"]!=null)
            {
                string id1 = Session["id"].ToString();
                int id = int.Parse(id1);
                reservation.UserId = id;

            }
            if (ModelState.IsValid)
            {
                DateTime now = DateTime.Now;
                var resevationDetails = Db.Reservations.Where(w =>( w.EventName == reservation.EventName) && (w.EventDate == reservation.EventDate )&&( (w.StartTime==reservation.StartTime || w.EndTime== reservation.EndTime)  || ( w.StartTime<reservation.EndTime && w.EndTime>reservation.EndTime)||(w.StartTime<reservation.StartTime && w.EndTime >reservation.StartTime ))).FirstOrDefault();

                if (resevationDetails != null)
                {
                    reservation.ReservationrMsg = " Another person booked the same event at the same time, so please choose another date";
                    
                     return View("Reservation", reservation);
                 
                
                }
                else if (reservation.EventDate <= now )
                {

                    reservation.ReservationrMsg = "Reservations are not permitted on this date";
                    return View("Reservation", reservation);
                }
                else if (reservation.StartTime >= reservation.EndTime)
                {
                    reservation.ReservationrMsg = "The time is not correct";
                    return View("Reservation", reservation);
                }
                else
                {
                    reservation.Price = reservation.numberOfPeople * 50;
                    
                    Db.Reservations.Add(reservation);
                    Db.SaveChanges();
                    reservation.ReservationrMsg = " success";
                    return View("Checkout", reservation);

                }

                

            
             

            }

            else
            {
                return View("Reservation");
            }
           
        }
        #endregion

        #region Log IN

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login( User user)
        {
            

            var userDetails = Db.Users.Where(w => w.Email == user.Email && w.Password == user.Password).FirstOrDefault();
            if (userDetails == null)
            {
                Session["log"] = "Log In";
              
                Session["id"] = null;
                user.LoginErrorMsg = "Invalid Email or Password";
                return View("Login", user);
            }
            Session["log"] = "Log out";

            Session["id"] =  userDetails.Id;
            return RedirectToAction("Events");
        }
        #endregion

        #region sign up
        [HttpGet]
        public ActionResult signup()
        {
            return View();
        }
        [HttpPost]
        public ActionResult signup( User u)
        {
            if (ModelState.IsValid)
            {
                if (Db.Users.Any(w => w.Email == u.Email))
                {
                    u.LoginErrorMsg = "This Email Already Exists.";
                    return View("signup", u);

                }
                else
                {
                    Db.Users.Add(u);
                    Db.SaveChanges();
                    Session["log"] = "Log out";

                    var val = Db.Users.Where(ww => ww.Email == u.Email);
                    int id = val.Select(ww => ww.Id).First();

                    Session["id"] = id;
                    return RedirectToAction("Events");

                }
            }
            else
            {
                return View("signup");
            }
        }
        #endregion

        #region Checkout
        [HttpGet]
        public ActionResult Checkout()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Done()
        {
            return View();
        }
        #endregion

    }
}
