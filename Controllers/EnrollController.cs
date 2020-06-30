using LMSProfile.ExceptionLogger;
//using LMSProfile.Models;
//using LMSProfile.Repository;
using DataAccess;
using Business;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LMSProfile.Controllers
{
    public class EnrollController : Controller
    {
        EnrollModel em = new EnrollModel();
        [Route("GetAllCourseEnroll")]
        [LogExceptions]
        public ActionResult GetAllCourseEnroll()
        {
            if (Session["UserId"] != null && Session["Accountid"] != null)
            {
                EnrollRepo ep = new EnrollRepo();
                return View(ep.GetAllCourseEnrollRepo());
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        [LogExceptions]
        public ActionResult AddEnrollDetails(int id)
        {
            if (Session["UserId"] != null && Session["Accountid"] != null)
            {
                EnrollRepo EnrRepo = new EnrollRepo();
                return View(EnrRepo.GetAllCourseEnrollRepo().Find(enroll => enroll.courseId == id));
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        [HttpPost]
        [LogExceptions]
        public ActionResult AddEnrollDetails(int id, EnrollModel obj)
        {
            if (Session["UserId"] != null && Session["Accountid"] != null)
            {
                try
                {
                    EnrollRepo EnrRepo = new EnrollRepo();

                    EnrRepo.AddEnrollRepo(obj, Session["UserId"]);

                    return RedirectToAction("GetAllCourseEnrolled");
                }
                catch
                {
                    return View();
                }
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }

        }


        [LogExceptions]
        public ActionResult GetAllCourseEnrolled()
        {
            if (Session["UserId"] != null && Session["Accountid"] != null)
            {
                EnrollRepo ep = new EnrollRepo();
                return View(ep.GetAllCourseEnrolledRepo(Session["Accountid"], Session["UserId"]));
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        [LogExceptions]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult UpdateEnrollment(EnrollModel model)
        {
            try
            {
                EnrollRepo Lr = new EnrollRepo();
                EnrollModel model1 = Lr.dropdownrepo();
                return View("UpdateEnrollment", model1);
            }
            catch (SqlException)
            {
                ViewBag.duplicatemessage = "Please Enter All The Data";
                return RedirectToAction("UpdateEnrollment", model);
            }
        }

        [HttpPost]
        [Route("UpdateEnrollment")]
        [LogExceptions]
        public ActionResult UpdateEnrollment(EnrollModel model, FormCollection form)
        {
            try
            {
                EnrollRepo Lr = new EnrollRepo();
                if (Lr.UpdateEnrolledRepo(model, Session["Accountid"], form))
                {
                    ViewBag.SuccessMessage = "Enrollment Updated successfully";

                }
                return RedirectToAction("GetAllCourseEnrolled");

            }
            catch (SqlException)
            {
                ViewBag.duplicatemessage = "SQl Error";
                return RedirectToAction("GetAllCourseEnrolled");
            }

        }


        [LogExceptions]
        public ActionResult DeleteEnroll(int id)
        {
            if (Session["UserId"] != null && Session["Accountid"] != null)
            {
                try
                {
                    EnrollRepo EnrRepo = new EnrollRepo();
                    if (EnrRepo.DeleteEnrollment(id, Session["Accountid"]))
                    {
                        ViewBag.AlertMsg = "Enrollment deleted successfully";

                    }
                    return RedirectToAction("GetAllCourseEnrolled");

                }
                catch
                {
                    return RedirectToAction("Index", "Login");
                }
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }

        }


        [Route("EnrolledVideo")]
        [LogExceptions]
        public ActionResult EnrolledVideo(int id)
        {

            try
            {
                EnrollRepo Lr = new EnrollRepo();
                return View("EnrolledVideo", Lr.EnrolledVideosRepo(id));

            }
            catch (SqlException)
            {
                ViewBag.duplicatemessage = "SQl Error";
                return RedirectToAction("GetAllCourseEnrolled");
            }

        }

        [Route("AddCoursesToCart")]
        [LogExceptions]
        public ActionResult AddCoursesToCart(int CourseId)
        {
            Session["CourseId"] = CourseId;
            EnrollRepo EnrRepo = new EnrollRepo();
            if (Session["cart"] == null)
            {
                
                List<EnrollModel> cart = new List<EnrollModel>();
                var product = EnrRepo.GetAllCourseEnrollRepo().Find(enroll => enroll.courseId == CourseId);
                cart.Add(new EnrollModel()
                {
                    Product = product,
                    Quantity = 1
                });
                Session["cart"] = cart;
            }
            else
            {

                List<EnrollModel> cart =(List<EnrollModel>)Session["cart"];
                var product = EnrRepo.GetAllCourseEnrollRepo().Find(enroll => enroll.courseId == CourseId);
                if (cart.Count==0)
                {
                    cart.Add(new EnrollModel()
                    {
                        Product = product,
                        Quantity = 1
                    });
                }
                else
                {
                    foreach (var item in cart)
                    {
                        if (item.Product.courseId == CourseId)
                        {
                            cart.Remove(item);
                            cart.Add(new EnrollModel()
                            {
                                Product = product,
                                Quantity = 1
                            });
                            break;
                        }
                        else
                        {
                            cart.Add(new EnrollModel()
                            {
                                Product = product,
                                Quantity = 1
                            });
                            break;
                        }
                    }
                }
                Session["cart"] = cart;
            }
            return RedirectToAction("CheckoutDetails");
        }
        [Route("RemoveCoursesFromCart")]
        [LogExceptions]
        public ActionResult RemoveCoursesFromCart(int CourseId)
        {
            List<EnrollModel> cart = (List<EnrollModel>)Session["cart"];
            foreach (var item in cart)
            {
                if(item.Product.courseId==CourseId)
                {
                    cart.Remove(item);
                    break;
                }
            }
            Session["cart"] = cart;
            return RedirectToAction("GetAllCourseEnroll");
        }

        [Route("Checkout")]
        [LogExceptions]
        public ActionResult Checkout()
        {
            return View();
        }

        [Route("CheckoutDetails")]
        [LogExceptions]
        public ActionResult CheckoutDetails()
        {
            return View();
        }
    }
        


}

