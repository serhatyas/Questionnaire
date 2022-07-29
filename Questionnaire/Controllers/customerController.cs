using Questionnaire.Helpers;
using Questionnaire.Models.EntityFramework;
using Questionnaire.Models.ServiceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Questionnaire.Controllers
{
    public class customerController : Controller
    {
        public ActionResult Index()
        {
            return Redirect("/customer/login");
        }
        // GET: customer
        [HttpGet]
        public ActionResult Login(string companyguid)
        {
            using (SurveyDB db = new SurveyDB())
            {
                ServiceModel model = new ServiceModel();
                model.Company = db.Companies.FirstOrDefault(x => x.CompanyGuid == companyguid);
                return View(model);
            }
        }

        [HttpPost]
        public ActionResult login(ServiceModel model)
        {
            using (SurveyDB db = new SurveyDB())
            {
                if (ModelState.IsValid)
                {
                    if(model.Company!=null && model.Company.CompanyGuid!=null && model.Company.CompanyGuid.Length>0)
                    {
                        model.Customer.IsPassive = false;
                        model.Customer.IsDeleted = false;
                        model.Customer.CreatedDateTime = DateTime.Now;
                        model.Customer.CustomerGuid = FriendlyUrl.FriendlyURLTitle(model.Customer.Mail);
                        model.Customer.CompanyGuid = model.Company.CompanyGuid;
                        db.Customers.Add(model.Customer);
                        db.SaveChanges();
                        return Redirect("/start/" + model.Company.CompanyGuid+"/"+model.Customer.CustomerGuid);
                    }
                return View();

                }
                return View();
            }
        }
    }
}