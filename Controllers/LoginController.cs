using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Data;
using System.Security;
using System.Web.Security;
using System.Web.Services.Description;
using System.Linq.Expressions;
using System.Configuration;
//using LMSProfile.Models;
//using LMSProfile.Repository;
using DataAccess;
using Business;
using LMSProfile.ExceptionLogger;

namespace LMSProfile.Controllers
{
    [RoutePrefix("/Login")]
    public class LoginController : Controller
    {
        private SqlConnection con;   
        private void connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["Connection"].ConnectionString.ToString();
            con = new SqlConnection(constr);

        }

        [LogExceptions]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult Index()
        {
            connection();
            try
            {
                LoginRepo Lr = new LoginRepo();
                LoginLogout ll = Lr.dropdownrepo();
                return View(ll);
            }
            catch (SqlException)
            {
                ViewData["message"] = "Login Attempt Failed! Check Email And Password";
            }
            return View();
        }

        [LogExceptions]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            Session.RemoveAll();
            Session.Abandon(); // it will clear the session at the end of request
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [Route("Index")]
        [LogExceptions]
        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]
        public ActionResult Index(LoginLogout ll,FormCollection form) //this is the login page which u will see at first.
        {
            connection();
            try
            {
            //string accountid = Convert.ToString(form["AccountList"]);
            con.Open();
            using (SqlCommand cmd = new SqlCommand("SP_Login_Logout", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@email", ll.Email);
                cmd.Parameters.AddWithValue("@password", ll.Password);
                //cmd.Parameters.AddWithValue("@accountid", accountid);//create  A drop down in the login page stating account type and while clicking login dropdown should send the account number .
                cmd.ExecuteNonQuery();
                SqlDataReader sqd = cmd.ExecuteReader();
                if (sqd.Read())
                {
                    FormsAuthentication.SetAuthCookie(ll.Email, true);
                    Session["UserId"] = sqd["userid"];
                    Session["Accountid"] = sqd["account_id"];
                    Session["Name"] = sqd["name"];
                    Session["Wallet"] = sqd["wallet"];
                    return RedirectToAction("Welcome", ll);
                }
                else
                {
                            
                    ViewData["message"] = "Login Attempt Failed! Check Email And Password";
                }
            }
                    
            con.Close();

            }
            catch (SqlException)
            {
                ViewData["message"] = "Login Attempt Failed! Check Email And Password";
            }
            catch(NullReferenceException)
            {
                ViewData["message"] = "Login Attempt Failed! Check Email And Password";
            }

        ViewBag.DuplicateMesaage = "Wrong Credentials";
        return RedirectToAction("Index");
        }

        [LogExceptions]
        public ActionResult Welcome(LoginLogout ll)
        {
            if (Session["UserId"] != null && Session["Accountid"] != null)
            {
                ViewData["email"] = ll.Email;
                return View("Welcome", "AfterLoginView");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
            
        }

      
        [LogExceptions]
        public ActionResult AddUsers(ProfileModel model)
        {
            try
            {
                LoginRepo Lr = new LoginRepo();
                ProfileModel model1 = Lr.dropdownrepo1();
                return View(model1);
            }
            catch (SqlException)
            {
                ViewBag.duplicatemessage = "Please Enter All The Data";
                return RedirectToAction("AddUsers", model);
            }
        }

        [HttpPost]
        [LogExceptions]
        public ActionResult AddUsers(ProfileModel model, FormCollection form)
        {
            try
            {
                LoginRepo Lr = new LoginRepo();
                if(Lr.AddUsersrepo(model,form))
                {
                    ViewBag.SuccessMessage = "Course details added successfully";
                    
                }
                return RedirectToAction("AddUsers", new ProfileModel());

            }
            catch (SqlException)
            {
                ViewBag.duplicatemessage = "This Email Address is Already taken";
                return View("AddUsers", model);
            }
            
        }


    }    
}