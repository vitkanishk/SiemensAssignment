using SiemensAssignment.BusinessModels;
using SiemensAssignment.Helper;
using SiemensAssignment.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace SiemensAssignment.Controllers
{
    public class EstimationController : Controller
    {
        // GET: Estimation
        [HttpGet]
        public ActionResult Estimation(string userName)
        {
            try
            {
                if (Session["users"] == null || userName == null)
                {
                    return RedirectToAction("Login", "Login");
                }

                List<UserDetails> userList = Session["users"] as List<UserDetails>;
                var user = userList.FirstOrDefault(x => x.UserName == userName);               
                
                if (user.Type == "Privileged")
                {
                    ViewData["UserType"] = "Privileged User";
                    ViewData["DisplayDiscount"] = "Yes";
                }
                else
                {
                    ViewData["UserType"] = "Normal User";
                    ViewData["DisplayDiscount"] = "No";
                }

                Session["UserType"] = user.Type;
                return View();
            }
            catch(Exception)
            {
                return RedirectToAction("Login", "Login");
            }
        }

        [HttpPost]
        public ActionResult Estimation(Gold gold)
        {
            try
            {                
                if (Session["discount"] != null)
                {
                    gold.Discount = Convert.ToDouble(Session["discount"]);
                }
                
                var userType = Convert.ToString(Session["UserType"]);
                if (userType == "Privileged")
                {
                    ViewData["UserType"] = "Privileged User";
                    ViewData["DisplayDiscount"] = "Yes";
                }
                else
                {
                    ViewData["UserType"] = "Normal User";
                    ViewData["DisplayDiscount"] = "No";
                }

                var updateGold = UserHelper.CalculateTotalPrice(gold, userType);
                return View(updateGold);
            }
            catch (Exception)
            {
                return RedirectToAction("Login", "Login");
            }
        }

        [HandleError(View = "Error")]
        public FileResult PrintToFile(double? totalPrice)
        {
            try
            {
                string data = $"Total Price = {totalPrice}";
                var byteArray = Encoding.ASCII.GetBytes(data);
                var stream = new MemoryStream(byteArray);

                return File(stream, "text/plain", "GoldPriceEstimation.txt");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HandleError(View = "CustomError", ExceptionType = typeof(NotImplementedException))]
        public ActionResult PrintToPaper()
        {
            throw new NotImplementedException();
        }
    }
}