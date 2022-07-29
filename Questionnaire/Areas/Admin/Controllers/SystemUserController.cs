using Questionnaire.Models.EntityFramework;
using Questionnaire.Models.ServiceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Questionnaire.Areas.Admin.Controllers
{
    public class SystemUserController : Controller
    {
        // GET: Admin/SystemUser
        public ActionResult Index()
        {
            using (SurveyDB db = new SurveyDB())
            {
                ServiceModel model = new ServiceModel();
                model.SystemUsers = db.SystemUsers.Where(x => x.IsDeleted == false).ToList();
                return View(model);
            }
        }

        [HttpGet]
        public ActionResult Create()
        {
            using (SurveyDB db = new SurveyDB())
            {
                ServiceModel model = new ServiceModel();
                model.SystemUsers = db.SystemUsers.Where(x => x.IsPassive == false).ToList();
                model.Companies = db.Companies.Where(x => x.IsPassive == false).ToList();
                return View(model);
            }
        }

        [HttpPost]
        public ActionResult Create(ServiceModel model)
        {
            using (SurveyDB db = new SurveyDB())
            {
                try
                {
                    var compnyguide = Convert.ToInt32(model.SystemUser.CompanyGuid);


                    var companyGuid = db.Companies.FirstOrDefault(x => x.Id == compnyguide);

                    model.SystemUser.IsPassive = false;
                    model.SystemUser.IsDeleted = false;
                    model.SystemUser.CompanyGuid = companyGuid.CompanyGuid;
                    model.SystemUser.Date = DateTime.Now;
                    db.SystemUsers.Add(model.SystemUser);
                    db.SaveChanges();
                    return Redirect("/Admin/SystemUser/Index");
                }
                catch (Exception)
                {

                    return Redirect("/500");
                }

            }
        }

        [HttpGet]
        public ActionResult Update(int id)
        {
            using (SurveyDB db = new SurveyDB())
            {
                ServiceModel model = new ServiceModel();
                model.SystemUser = db.SystemUsers.FirstOrDefault(x => x.Id == id);
                return View(model);
            }
        }

        [HttpPost]
        public ActionResult Update(ServiceModel model)
        {
            using (SurveyDB db = new SurveyDB())
            {
                try
                {
                    var UpdateSystemuser = db.SystemUsers.FirstOrDefault(x => x.Id == model.SystemUser.Id);
                    UpdateSystemuser.CompanyGuid = model.SystemUser.CompanyGuid;
                    UpdateSystemuser.Mail = model.SystemUser.Mail;
                    UpdateSystemuser.Password = model.SystemUser.Password;
                    UpdateSystemuser.Name = model.SystemUser.Name;
                    UpdateSystemuser.Surname = model.SystemUser.Surname;
                    UpdateSystemuser.Phone = model.SystemUser.Phone;
                    UpdateSystemuser.BirthDate = model.SystemUser.BirthDate;
                    UpdateSystemuser.JobStartDate = model.SystemUser.JobStartDate;
                    UpdateSystemuser.SuperAdmin = model.SystemUser.SuperAdmin;

                    db.SaveChanges();
                    return Redirect("/admin/systemuser/index");
                }
                catch (Exception)
                {

                    return Redirect("/500");
                }
            }
        }

        public ActionResult Delete(int id)
        {
            using (SurveyDB db = new SurveyDB())
            {
                var DeleteItem = db.SystemUsers.FirstOrDefault(x => x.Id == id);
                if (DeleteItem != null)
                {
                    DeleteItem.IsDeleted = true;
                    db.SaveChanges();
                    return Json("Basarili", JsonRequestBehavior.AllowGet);
                }
                return View();
            }
        }
    }
}