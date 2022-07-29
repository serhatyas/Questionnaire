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
    public class CustomerCreationController : Controller
    {
        // GET: Admin/CustomerCreation
        public ActionResult Index()
        {
            using (SurveyDB db=new SurveyDB())
            {
                ServiceModel model = new ServiceModel();
                model.Customers = db.Customers.Where(x => x.IsDeleted == false).ToList();
                return View(model);
            }

        }

        [HttpGet]
        public ActionResult Create()
        {
            using (SurveyDB db=new SurveyDB())
            {
                ServiceModel model = new ServiceModel();
                model.Customers = db.Customers.Where(x => x.IsDeleted == false).ToList();
                model.customerTypes = db.CustomerType.Where(x => x.IsPassive == false).ToList();
                model.Companies = db.Companies.Where(x => x.IsPassive == false).ToList();
                return View(model);
            }
        }

        [HttpPost]
        public ActionResult Create(ServiceModel model, FormCollection fc)
        {
            using (SurveyDB db = new SurveyDB())
            {

                var customerAdd = fc.GetValues("Companies");

                foreach (var item in customerAdd)
                {
                    var cmp = Convert.ToInt32(item);
                    var Companyy = db.Companies.FirstOrDefault(x => x.Id == cmp);

                    model.Customer.IsPassive = false;
                    model.Customer.IsDeleted = false;
                    model.Customer.CreatedDateTime = DateTime.Now;
                    model.Customer.CustomerGuid = FriendlyUrl.FriendlyURLTitle(model.Customer.Mail);
                    model.Customer.CompanyGuid = Companyy.CompanyGuid;
                    model.Customer.CustomerType = model.Customer.CustomerType;
                    db.Customers.Add(model.Customer);
                    db.SaveChanges();
                }

                return Redirect("/admin/customercreation/index");
            }
        }
    }
}