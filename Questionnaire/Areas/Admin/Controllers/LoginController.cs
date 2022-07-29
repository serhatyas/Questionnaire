using Questionnaire.Models.EntityFramework;
using Questionnaire.Models.ServiceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Questionnaire.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Admin/Login
        [HttpGet]
        public ActionResult Index()
        {
            using (SurveyDB db = new SurveyDB())
            {
                //if (HttpContext.User.Identity.IsAuthenticated)
                //    return Redirect("/Admin/dashboard/Index");
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
                var CurrentUser = db.SystemUsers.FirstOrDefault(x => x.Mail == model.SystemUser.Mail && x.Password == model.SystemUser.Password && x.CompanyGuid==model.Company.CompanyGuid);
                if (CurrentUser != null)
                {
                    //if (CurrentUser.CompanyGuid != model.Company.CompanyGuid)
                    //{
                    //    TempData["Error"] = "Şirket seçiminiz hatalı. Lütfen doğru departmanı seçiniz.";
                    //    return Redirect("/Admin/login");
                    //}
                    FormsAuthentication.SetAuthCookie(CurrentUser.Id.ToString(), true);
                    return Redirect("/Admin/dashboard/Index");
                }
                TempData["Error"] = "Kullanıcı adınızı veya parolanızı hatalı girdiniz. Lütfen tekrar deneyiniz.";
                return Redirect("/Admin/login");
            }


        }


        public ActionResult logout()
        {
            FormsAuthentication.SignOut();
            return Redirect("/admin/login");
        }

    }
}