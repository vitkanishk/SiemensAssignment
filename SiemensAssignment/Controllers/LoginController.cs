using SiemensAssignment.Helper;
using SiemensAssignment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SiemensAssignment.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }
        
        [HttpPost]
        [HandleError(View ="Error")]
        public ActionResult Login(UserData userData)
        {           
            if (!ModelState.IsValid)
            {
                return View();
            }

            var userList = UserHelper.GetAllUsers();
            var discountPercent = UserHelper.GetDiscountPercent();
            Session["users"] = userList;
            Session["discount"] = discountPercent;

            if (userList.Exists(x=>x.UserName==userData.UserName && x.Password == userData.Password))
            {
                var user = userList.FirstOrDefault(x => x.UserName == userData.UserName);
                return RedirectToAction("Estimation", "Estimation", new { userName = user.UserName });
            }
            else
            {
                ModelState.AddModelError("InvalidUser", "User is not valid. Please try again");
                return View();
            }            
        }
    }
}