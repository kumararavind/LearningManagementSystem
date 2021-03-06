﻿using System;
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
using DataAccess;
using Business;
using LMSProfile.ExceptionLogger;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace LMSProfile.Controllers
{
    [RoutePrefix("/Login")]
    public class LoginController : Controller
    {
        private SqlConnection con;   
        private void connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["LMSDB"].ConnectionString.ToString();
            con = new SqlConnection(constr);

        }

        [Route("Index")]
        [LogExceptions]
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
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            Session.RemoveAll();
            Session.Abandon(); // it will clear the session at the end of request
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [LogExceptions]
        public ActionResult Index(LoginLogout ll,FormCollection form) //this is the login page which u will see at first.
        {
            //Twilio
            //const string accountSid = "";
            //const string authToken = "";

            //TwilioClient.Init(accountSid, authToken);

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
                        Session["PhoneNo"] = sqd["contact"];
                        Session["AdminCount"] = sqd["admin_count"];
                        Session["InstructorCount"] = sqd["instructor_count"];
                        Session["UserCount"] = sqd["user_count"];
                        Session["CategoryCount"] = sqd["category_count"];
                        Session["CourseCount"] = sqd["course_count"];

                        //var message = MessageResource.Create(
                        //body: "You have Logged in to Learning App Please remember UserID For Future Needs: " + Convert.ToInt32(Session["UserId"]) + " And Your Current Wallet Amount is:" + Convert.ToInt32(Session["Wallet"]) + "",
                        //from: new Twilio.Types.PhoneNumber("+12057758497"),
                        //to: new Twilio.Types.PhoneNumber("+91" + Session["PhoneNo"].ToString())
                        //);
                        return RedirectToAction("Welcome", ll);

                }
                else
                {
                    ViewData["error"] = "Invalid Credentials! Please enter the details properly and try again.";
                }
            }
                con.Close();
                return View(ll);
            }
            catch (SqlException)
            {
                ViewData["error"] = "Invalid Credentials! Please enter the details properly and try again.";
                return View(ll);
            }
            catch(NullReferenceException)
            {
                ViewData["error"] = "Invalid Credentials! Please enter the details properly and try again.";
                return View(ll);
            }

        
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

        [Route("AddUsers")]
        [LogExceptions]
        public ActionResult AddUsers()
        {
            LoginRepo Lr1 = new LoginRepo();
            ProfileModel model1 = Lr1.dropdownrepo1();
            try
            {
                LoginRepo Lr = new LoginRepo();
                ProfileModel model = Lr.dropdownrepo1();
                return View(model);
            }
            catch (SqlException)
            {
                ViewData["Errormessage"] = "Please Enter All The Data.";
                return View(model1);
            }
        }

        [HttpPost]
        [LogExceptions]
        public ActionResult AddUsers(ProfileModel model, FormCollection form)
        {
            LoginRepo Lr1 = new LoginRepo();
            ProfileModel model1 = Lr1.dropdownrepo1();
            try
            {
                LoginRepo Lr = new LoginRepo();
                ProfileModel model2 = Lr.dropdownrepo1();
                if (Lr.AddUsersrepo(model,form))
                {
                    ViewData["Successmessage"] = "Registered successfully.";
                    
                }
                return View(model2);

            }
            catch (SqlException)
            {
                ViewData["Errormessage"] = "This Email Address is Already taken.";
                return View(model1);
            }
            
        }


    }    
}