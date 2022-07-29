using Questionnaire.Models.EntityFramework;
using Questionnaire.Models.ServiceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Questionnaire.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        [ValidateInput(false)]
        [Route("step/{company}/{CustomerGuid}/{language}")]
        public ActionResult Index(string company,string customerGuid, string language)
        {
            using (SurveyDB db = new SurveyDB())
            {
                ServiceModel model = new ServiceModel();
                model.Company = db.Companies.FirstOrDefault(x => x.CompanyGuid == company);
                model.Language = db.Languages.FirstOrDefault(x => x.Code == language);
                model.Departments = db.Departments.Where(x => x.IsDeleted == false && x.IsPassive == false && x.LangId==model.Language.Id && x.CompanyGuid==model.Company.CompanyGuid).ToList();
                model.Customer = db.Customers.FirstOrDefault(x => x.CustomerGuid == customerGuid);

                return View(model);

            }
        }

        [Route("step/{company}/{CustomerGuid}/{language}/{id}")]
        public ActionResult Parent(string company, string customerGuid, string language,int id)
        {
            using (SurveyDB db=new SurveyDB())
            {
                ServiceModel model = new ServiceModel();
                model.Company = db.Companies.FirstOrDefault(x => x.CompanyGuid == company);
                model.Language = db.Languages.FirstOrDefault(x => x.Code == language);
                model.Customer = db.Customers.FirstOrDefault(x => x.CustomerGuid == customerGuid);
                model.Departments = db.Departments.Where(x => x.ParentId==id).ToList();
                return View(model);
            }
        }
    }
}