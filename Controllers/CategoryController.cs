using LMSProfile.ExceptionLogger;
//using LMSProfile.Models;
//using LMSProfile.Repository;
using DataAccess;
using Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LMSProfile.Controllers
{
    public class CategoryController : Controller
    {

        [LogExceptions]
        public ActionResult GetAllCategory()
        {
            if(Session["UserId"] != null && Session["Accountid"] != null)
            {
                CategoryRepo Repo = new CategoryRepo();
                ModelState.Clear();
                return View(Repo.GetAllCategory());
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
            
        }

        [LogExceptions]
        public ActionResult AddCategory()
        {
            if (Session["UserId"] != null && Session["Accountid"] != null)
            {
                CategoryModel model = new CategoryModel();
                
                return View(model);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
            
        }

        [HttpPost]
        [LogExceptions]
        public ActionResult AddCategory(CategoryModel model)
        {
            if (Session["UserId"] != null && Session["Accountid"] != null)
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        CategoryRepo CatRepo = new CategoryRepo();

                        if (CatRepo.AddCategory(model))
                        {
                            ViewBag.Message = "Category details added successfully";
                        }
                    }

                    return View(model);
                }
                catch
                {
                    ViewBag.Message = "Category details Addition Failed";
                    return View(model);
                }
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
            
        }

        [LogExceptions]
        public ActionResult EditCatDetails(int id)
        {
            if (Session["UserId"] != null && Session["Accountid"] != null)
            {
                CategoryRepo CatRepo = new CategoryRepo();
                return View(CatRepo.GetAllCategory().Find(Cat => Cat.catId == id));
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }
   
        [HttpPost]
        [LogExceptions]
        public ActionResult EditCatDetails(int id, CategoryModel obj)
        {
            if (Session["UserId"] != null && Session["Accountid"] != null)
            {
                try
                {
                    CategoryRepo CatRepo = new CategoryRepo();

                    CatRepo.UpdateCategory(obj);

                    return RedirectToAction("GetAllCategory");
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
        public ActionResult DeleteCat(int id)
        {
            if (Session["UserId"] != null && Session["Accountid"] != null)
            {
                try
                {
                    CategoryRepo CatRepo = new CategoryRepo();
                    if (CatRepo.DeleteCategory(id))
                    {
                        ViewBag.AlertMsg = "Category details deleted successfully";

                    }
                    return RedirectToAction("GetAllCategory");

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
    }
}
