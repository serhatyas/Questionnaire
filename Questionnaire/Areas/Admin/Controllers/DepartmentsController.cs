using Questionnaire.Helpers;
using Questionnaire.Models.EntityFramework;
using Questionnaire.Models.ServiceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Questionnaire.Areas.Admin.Controllers
{
    [Authorize]
    public class DepartmentsController : Controller
    {
        public DepartmentsController()
        {
            ViewBag.Departman = "active";
        }

        [HttpGet]
        public ActionResult Index()
        {
            using (SurveyDB db = new SurveyDB())
            {

                ServiceModel model = new ServiceModel();
                model.Departments = db.Departments.Where(x =>  x.IsDeleted == false && x.CompanyGuid==model.CurrentUser.CompanyGuid).OrderBy(x => x.Title).ToList();
                return View(model);

            }
        }

        [HttpGet]
        public ActionResult create(int langid)
        {
            using (SurveyDB db = new SurveyDB())
            {
                ServiceModel model = new ServiceModel();
                model.Language = db.Languages.FirstOrDefault(x => x.Id == langid);
                model.SystemUsers = db.SystemUsers.Where(x => x.IsPassive == false && x.IsDeleted == false && x.CompanyGuid==model.CurrentUser.CompanyGuid).ToList();
                model.Departments = db.Departments.Where(x => x.IsDeleted == false && x.CompanyGuid == model.CurrentUser.CompanyGuid && x.ParentId == 0).ToList();
                return View(model);
            }
        }

        [HttpPost]
        public ActionResult create(ServiceModel model, HttpPostedFileBase Icon)
        {
            using (SurveyDB db = new SurveyDB())
            {
                try
                {
                    model.Department.LangId = model.Language.Id;
                    model.Department.CompanyGuid = model.CurrentUser.CompanyGuid;
                    model.Department.IsDeleted = false;
                    model.Department.IsPassive = false;
                    if (model.Department.ParentId==null)
                    {
                        model.Department.ParentId = 0;
                    }
                    //model.Department.ParentId = model.Department.Id;
                    model.Department.DepartmenGuid = FriendlyUrl.FriendlyURLTitle(model.Department.Title);
                    if (Icon != null)
                    {
                        string filePath = System.IO.Path.Combine(Server.MapPath("/uploads/images/") + Icon.FileName);
                        Icon.SaveAs(filePath);
                        model.Department.Icon = "/Uploads/images/" + Icon.FileName;
                    }
                    db.Departments.Add(model.Department);
                    db.SaveChanges();
                    return Redirect("/admin/departments");
                }
                catch (Exception)
                {
                    return Redirect("/500");

                }
            }
        }

        [HttpGet]
        public ActionResult update(int id)
        {
            using (SurveyDB db = new SurveyDB())
            {
                ServiceModel model = new ServiceModel();
                model.Department = db.Departments.FirstOrDefault(x => x.Id == id);
                model.Departments = db.Departments.Where(x => x.IsDeleted == false && x.CompanyGuid == model.CurrentUser.CompanyGuid && x.ParentId==0).ToList();
                model.Language = db.Languages.FirstOrDefault(x => x.Id == model.Department.LangId);
                model.SystemUsers = db.SystemUsers.Where(x => x.IsPassive == false && x.IsDeleted == false && x.CompanyGuid == model.CurrentUser.CompanyGuid).ToList();
                return View(model);
            }
        }


        [HttpPost]
        public ActionResult update(ServiceModel model, HttpPostedFileBase Icon)
        {
            using (SurveyDB db = new SurveyDB())
            {
                try
                {
                    var departmen = db.Departments.FirstOrDefault(x => x.Id == model.Department.Id);
                    departmen.Title = model.Department.Title;
                    departmen.Description = model.Department.Description;
                    departmen.SeniorPersonnelId = model.Department.SeniorPersonnelId;
                    departmen.ParentId = model.Department.SeniorPersonnelId;
                    departmen.ParentId = model.Department.ParentId;
                    departmen.DepartmenGuid = FriendlyUrl.FriendlyURLTitle(model.Department.Title);
                     
                    if (Icon != null)
                    {
                        string filePath = System.IO.Path.Combine(Server.MapPath("/uploads/images/") + Icon.FileName);
                        Icon.SaveAs(filePath);
                        departmen.Icon = "/Uploads/images/" + Icon.FileName;
                    }
                    db.SaveChanges();
                    return Redirect("/admin/departments");
                }
                catch (Exception)
                {
                    return Redirect("/500");

                }

            }
        }

        [HttpGet]
        public ActionResult delete(int id)
        {
            using (SurveyDB db = new SurveyDB())
            {

                var item = db.Departments.FirstOrDefault(x => x.Id == id);
                if (item != null)
                {
                    item.IsDeleted = true;
                    db.SaveChanges();
                    return Json("Basarili",JsonRequestBehavior.AllowGet);
                }
                return View();

            }
        }

        [HttpGet]
        public ActionResult passive(int id)
        {
            using (SurveyDB db = new SurveyDB())
            {

                var item = db.Departments.FirstOrDefault(x => x.Id == id);
                if (item != null)
                {
                    if (item.IsPassive == false)
                        item.IsPassive = true;
                    else
                        item.IsPassive = false;
                    db.SaveChanges();
                    return Json("Basarili", JsonRequestBehavior.AllowGet);
                }
                return View();

            }
        }

    }
}