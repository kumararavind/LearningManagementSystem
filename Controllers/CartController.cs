using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Business;

namespace LMSProfile.Controllers
{
    public class CartController : Controller
    {
        
        // GET: AddToCart  
        public ActionResult AddCoursesToCart(int CourseId)
        {
            EnrollModel model = new EnrollModel();
            if (Session["UserId"] != null && Session["Accountid"] != null)
            {
                if (Session["cart"] == null)
                {
                    List<EnrollModel> li = new List<EnrollModel>();
                    li.Add(model);
                    Session["cart"] = li;
                    ViewBag.cart = li.Count();
                    Session["count"] = 1;

                }
                else
                {
                    List<EnrollModel> li = (List<EnrollModel>)Session["cart"];
                    li.Add(model);
                    Session["cart"] = li;
                    ViewBag.cart = li.Count();
                    Session["count"] = Convert.ToInt32(Session["count"]) + 1;

                }
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }

        }

        public ActionResult Myorder()  
        {  
              
            return View((List<EnrollModel>)Session["cart"]);  
  
        }
    }
}