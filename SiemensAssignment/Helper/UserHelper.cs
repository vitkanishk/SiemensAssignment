using Newtonsoft.Json.Linq;
using SiemensAssignment.BusinessModels;
using SiemensAssignment.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;

namespace SiemensAssignment.Helper
{
    public class UserHelper
    {
        public static List<UserDetails> GetAllUsers()
        {
            List<UserDetails> userDetails = new List<UserDetails>();
            try
            {
                
                string filePath = ConfigurationManager.AppSettings["JsonFilePath"].ToString();
                if (File.Exists(filePath))
                {
                    var configContent = File.ReadAllText(filePath);
                    var jObject = JObject.Parse(configContent);

                    JArray jList = (JArray)jObject["UserData"];

                    userDetails = jList.ToObject<List<UserDetails>>();
                }                
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return userDetails;
        }

        public static double? GetDiscountPercent()
        {
            double? discountPer = null;
            try
            {
                var dicountPercent = ConfigurationManager.AppSettings["Discount"];
                if (dicountPercent != null)
                {
                    discountPer = Convert.ToDouble(dicountPercent);                    
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return discountPer;
        }

        public static Gold CalculateTotalPrice(Gold gold, string userType)
        {
            var goldObj = new Gold();
            try
            {
                var totalPriceBeforeDiscount = gold.Price * gold.Weight;
                double totalPriceAfterDiscount;
                if (userType == "Privileged")
                {
                    var discount = totalPriceBeforeDiscount * gold.Discount / 100;
                    totalPriceAfterDiscount = totalPriceBeforeDiscount - discount.Value;
                }
                else
                {
                    totalPriceAfterDiscount = totalPriceBeforeDiscount;
                }

                goldObj.Price = gold.Price;
                goldObj.Weight = gold.Weight;
                goldObj.TotalPrice = totalPriceAfterDiscount;
                goldObj.Discount = gold.Discount;                
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return goldObj;
        }
    }
}