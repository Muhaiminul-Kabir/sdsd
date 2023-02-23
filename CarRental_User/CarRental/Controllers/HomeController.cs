using CarRental.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;


namespace CarRental.Controllers
{

    public class HomeController : Controller
    {
        CarEntities2 db = new CarEntities2();
        public ActionResult Edit1()
        {
            return View();
        }
        public ActionResult Delete1()
        {
            return View();
        }
       
        public ActionResult Car()
        {
            
            return View(db.cars.ToList());
        }
        public ActionResult Drivers()
        {
            return View(db.drivers.ToList());

        }
        public ActionResult Rent()
        {
            return View(db.cars.ToList());
         
        }
        
       
        public ActionResult RentCar()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RentCar(rental rental)
        {
            db.rentals.Add(rental);
            db.SaveChanges();

            Session["IdRent"] = rental.Id.ToString();
            Session["IdUss"] = rental.customer_id.ToString();
            Session["IdCar"] = rental.car_id.ToString();
            Session["PickupRent"] = rental.pickup.ToString();
            Session["DropoffRent"] = rental.dropoff.ToString();
            Session["TotalRunRent"] = rental.total_run.ToString();
            Session["RateRent"] = rental.rate.ToString();
            Session["TotalAmountRent"] = rental.total_amount.ToString();
            Session["IdDr"] = rental.driver_id.ToString();
            Session["StatusRent"] = rental.status.ToString();
            return RedirectToAction("Book", "Home");
        }
        public ActionResult Request()
        {

            return View(db.rentals.ToList());

        }
        public ActionResult Book()
        {
           
            return View(db.rentals.ToList());
          
        }
        public ActionResult IndexUser()
        {
            return View(db.customers.ToList());
        }
        public ActionResult IndexDriver()
        {
            return View(db.drivers.ToList());
        }
        public ActionResult Profile()
        {
            return View();
        }
        

        public ActionResult Return()
        {
            return View();
        }
        public ActionResult Home()
        {
            return View();
        }

     

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Home", "Home");
        }

        [HttpGet]
        public ActionResult UserLog()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserLog(customer customer)
        {
            var checkLogin = db.customers.Where(x => x.Name.Equals(customer.Name) && x.Password.Equals(customer.Password)).FirstOrDefault();
            if (checkLogin != null)
            {
                Session["IdUs"] = checkLogin.Id.ToString();
                Session["NameUs"] = checkLogin.Name.ToString();
                Session["EmailUs"] = checkLogin.Email.ToString();
                Session["PhoneUs"] = checkLogin.Phone.ToString();
                Session["PasswordUs"] = checkLogin.Password.ToString();

                return RedirectToAction("UserPro", "Home");
            }
            else
            {
                ViewBag.Notification = "Wrong Name or Password.";
            }
            return View();
        }

        public ActionResult UserPro()
        {
            return View();
        }
        [HttpGet]
        public ActionResult DriverLog()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DriverLog(driver driver)
        {
            var checkLogin = db.drivers.Where(x => x.Name.Equals(driver.Name) && x.Password.Equals(driver.Password)).FirstOrDefault();
            if (checkLogin != null)
            {
                Session["IdDr"] = checkLogin.Id.ToString();
                Session["NameDr"] = checkLogin.Name.ToString();
                Session["EmailDr"] = checkLogin.Email.ToString();
                Session["AgeDr"] = checkLogin.Age.ToString();
                Session["PasswordDr"] = checkLogin.Password.ToString();
                return RedirectToAction("DriverPro", "Home");
            }
            else
            {
                ViewBag.Notification = "Wrong Name or Password.";
            }
            return View();
        }
        public ActionResult DriverPro()
        {
            return View();
        }
        public ActionResult User()
        {
            return View();
        }
        [HttpPost]
        public ActionResult User(customer customer)
        {
            if(db.customers.Any(x=>x.Name == customer.Name))
            {
                ViewBag.Notification = "This account has already existed";
                return View();
            }
            else
            {
                db.customers.Add(customer);
                db.SaveChanges();

                Session["IdUs"] = customer.Id.ToString();
                Session["NameUs"] = customer.Name.ToString();
                Session["EmailUs"] = customer.Email.ToString();
                Session["PhoneUs"] = customer.Phone.ToString();
                Session["PasswordUs"] = customer.Password.ToString();
                return RedirectToAction("IndexUser","Home");
            }
        }
        public ActionResult Driver()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Driver(driver driver)
        {
            if (db.drivers.Any(x => x.Name == driver.Name))
            {
                ViewBag.Notification = "This account has already existed";
                return View();
            }
            else
            {
                db.drivers.Add(driver);
                db.SaveChanges();
                Session["IdDr"] = driver.Id.ToString();
                Session["NameDr"] = driver.Name.ToString();
                Session["EmailDr"] = driver.Email.ToString();
                Session["AgeDr"] = driver.Age.ToString();
                Session["PasswordDr"] = driver.Password.ToString();
                return RedirectToAction("IndexDriver", "Home");
            }
        }




    }
}