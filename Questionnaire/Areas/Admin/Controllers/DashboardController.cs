using Questionnaire.Models.EntityFramework;
using Questionnaire.Models.ServiceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Questionnaire.Areas.Admin.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Admin/Home
        
        public ActionResult Index()
        {
            using (SurveyDB db = new SurveyDB())
            {
                ServiceModel model = new ServiceModel();
                
                model.Customers = db.Customers.Where(x => x.CompanyGuid == model.CurrentCompany.CompanyGuid).ToList();
                model.CustomerAnswers = db.CustomerAnswers.Where(x => x.CompanyGuid == model.CurrentCompany.CompanyGuid).ToList();
                model.Departments = db.Departments.Where(x => x.CompanyGuid == model.CurrentCompany.CompanyGuid).ToList();
                model.Questions = db.Questions.Where(x=>x.CompanyGuid==model.CurrentCompany.CompanyGuid).ToList();
                model.SystemUsers = db.SystemUsers.Where(x => x.CompanyGuid == model.CurrentCompany.CompanyGuid).ToList();
                model.Companies = db.Companies.Where(x =>x.IsPassive==false).ToList();
                model.Languages = db.Languages.Where(x => x.IsPassive == false).ToList();
                var sss = model.CurrentUser;
                return View(model);
            }
        }
    }
}