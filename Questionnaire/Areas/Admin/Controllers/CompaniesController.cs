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
    public class CompaniesController : Controller
    {
        // GET: Admin/Companies
        public ActionResult Index()
        {
            using (SurveyDB db=new SurveyDB())
            {
                ServiceModel model = new ServiceModel();
                model.Companies = db.Companies.Where(x => x.IsDeleted == false).ToList();
                model.Languages = db.Languages.Where(x => x.IsPassive == false).ToList();
                return View(model);
            }

        }

        [HttpGet]
        public ActionResult Create()
        {
            using (SurveyDB db=new SurveyDB())
            {
                ServiceModel model = new ServiceModel();
                model.Companies = db.Companies.Where(x => x.IsPassive == false).ToList();
                model.Languages = db.Languages.Where(x => x.IsPassive == false).ToList();
                return View(model);
            }
        }

        [HttpPost]
        public ActionResult Create(ServiceModel model, HttpPostedFileBase Logo)
        {
            using (SurveyDB db=new SurveyDB())
            {
                try
                {
                    model.Company.IsPassive = false;
                    model.Company.IsDeleted = false;
                    model.Company.DefaultLanguageId = 1;
                    model.Company.Date = DateTime.Now;
                    model.Company.CompanyGuid = FriendlyUrl.FriendlyURLTitle(model.Company.Name);

                    if (Logo != null)
                    {
                        string filePath = System.IO.Path.Combine(Server.MapPath("/uploads/images/") + Logo.FileName);
                        Logo.SaveAs(filePath);
                        model.Company.Logo = "/Uploads/images/" + Logo.FileName;
                    }
                    db.Companies.Add(model.Company);
                    db.SaveChanges();
                    return Redirect("/admin/Companies");
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
            using (SurveyDB db=new SurveyDB())
            {
                ServiceModel model = new ServiceModel();
                model.Company = db.Companies.FirstOrDefault(x => x.Id == id);
                model.Language = db.Languages.FirstOrDefault(x => x.Id == id);
                return View(model);
            }
        }

        [HttpPost]
        public ActionResult Update( ServiceModel model, HttpPostedFileBase Logo)
        {
            using (SurveyDB db=new SurveyDB())
            {
                try
                {
                    var UpdateCompanies = db.Companies.FirstOrDefault(x => x.Id == model.Company.Id);
                    UpdateCompanies.CompanyGuid = model.Company.CompanyGuid;
                    UpdateCompanies.Name = model.Company.Name;
                    UpdateCompanies.Phone1 = model.Company.Phone1;
                    UpdateCompanies.Phone2 = model.Company.Phone2;
                    UpdateCompanies.Mail = model.Company.Mail;
                    UpdateCompanies.Website = model.Company.Website;
                    UpdateCompanies.Address = model.Company.Address;
                    UpdateCompanies.CompanyGuid = FriendlyUrl.FriendlyURLTitle(model.Company.Name);
                    if (Logo != null)
                    {
                        string filePath = System.IO.Path.Combine(Server.MapPath("/uploads/images/") + Logo.FileName);
                        Logo.SaveAs(filePath);
                        model.Company.Logo = "/Uploads/images/" + Logo.FileName;
                    }

                    db.SaveChanges();
                    return Redirect("/Admin/Companies");

                }
                catch (Exception)
                {
                    return Redirect("/500");
                }
 
            }
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            using (SurveyDB db=new SurveyDB())
            {
                var deleteItem = db.Companies.FirstOrDefault(x => x.Id == id);
                if (deleteItem!=null)
                {
                    deleteItem.IsDeleted = true;
                    db.SaveChanges();
                    return Json("Başarılı", JsonRequestBehavior.AllowGet);
                }
                return View();
            }
        }
    }
}