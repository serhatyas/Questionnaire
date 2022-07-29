using Questionnaire.Models.EntityFramework;
using Questionnaire.Models.ServiceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Questionnaire.Controllers
{
    public class CustomerLoginController : Controller
    {
        // GET: CustomerLogin
        [HttpGet]
        public ActionResult Index()
        {
            using (SurveyDB db = new SurveyDB())
            {
                //if (HttpContext.User.Identity.IsAuthenticated)
                //    return Redirect("/customerlogin/Index");
                ServiceModel model = new ServiceModel();
                model.Companies = db.Companies.Where(x => x.IsDeleted == false && x.IsPassive == false).ToList();
                return View(model);
            }
        }

        [HttpPost]
        public ActionResult Index(ServiceModel model)
        {
            using (SurveyDB db = new SurveyDB())
            {
                model.Companies = db.Companies.Where(x => x.IsDeleted == false && x.IsPassive == false).ToList();
                var CurrentCompany = db.Customers.FirstOrDefault(x => x.Mail == model.Customer.Mail && x.Password == model.Customer.Password && x.CompanyGuid == model.Company.CompanyGuid);
                
                if (CurrentCompany != null)
                {
                    FormsAuthentication.SetAuthCookie(CurrentCompany.Id.ToString(), true);
                    return Redirect("/start/"+ CurrentCompany.CompanyGuid+"/"+ CurrentCompany.CustomerGuid);
                }
                TempData["Error"] = "Kullanıcı adınızı veya parolanızı hatalı girdiniz. Lütfen tekrar deneyiniz.";
                return Redirect("/CustomerLogin/Index");
            }


        }
    }
}