using Questionnaire.Models.EntityFramework;
using Questionnaire.Models.ServiceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Questionnaire.Controllers
{
    public class QuestionsController : Controller
    {
        [ValidateInput(false)]
        [Route("start/{company}/{CustomerGuid}")]
        public ActionResult Index(string company,string customerguid)
        {
            using (SurveyDB db = new SurveyDB())
            {
                ServiceModel model = new ServiceModel();
                model.Company = db.Companies.FirstOrDefault(x => x.CompanyGuid == company);
                model.Customer = db.Customers.FirstOrDefault(x => x.CustomerGuid == customerguid);
                return View(model);

            }
        }

        [ValidateInput(false)]
        [Route("step1/{company}/{department}/{customerguid}/{langid}")]
        [HttpGet]
        public ActionResult step1(string company, string department, string langid, string customerguid)
        {
            using (SurveyDB db = new SurveyDB())
            {
                ServiceModel model = new ServiceModel();

                model.Company = db.Companies.FirstOrDefault(x => x.CompanyGuid.ToLower() == company.ToLower());
                model.Department = db.Departments.FirstOrDefault(x => x.DepartmenGuid.ToLower() == department.ToLower());
                model.Language = db.Languages.FirstOrDefault(x => x.Code.ToLower() == langid.ToLower());
                model.LangValuesDictionary = db.LanguageValues.Where(x => x.LangId == model.Language.Id).ToDictionary(x => x.Name, x => x.Value);
                model.Customer = db.Customers.FirstOrDefault(x => x.CustomerGuid == customerguid);
                var anket = db.CustomerAnswers.Where(x => x.CustomerId == model.Customer.Id && x.DepartmentId == model.Department.Id).ToList();

                if (anket.Count == 0)
                {

                    model.QustionVM = new QuestionVM()
                    {
                        CompanyGuid = company,
                        DepartmenGuid = department,
                        Language = langid
                    };
                    var departmenQuiz = db.DepartmentQuestions.Where(x => x.DepartmentId == model.Department.Id).Select(x => x.QuestionId).ToList();
                    model.Questions = db.Questions.Where(x => departmenQuiz.Contains(x.Id) && x.IsPassive == false && x.IsDeleted == false && x.LangId == model.Language.Id && x.CompanyGuid == model.Company.CompanyGuid).ToList();
                    model.Answers = db.Answers.Where(x => departmenQuiz.Contains(x.QuestionId) && x.IsPassive == false && x.IsDeleted == false && x.LangId == model.Language.Id).ToList();
                return View(model);
            }

                else
            {
                return Redirect("/ErrorPage/AnsweredQuestions");
            }


        }
        }
        [HttpPost]
        public ActionResult step1(ServiceModel model, HttpPostedFileBase Documents, string customerguid)
        {
            using (SurveyDB db = new SurveyDB())
            {
                return Redirect("step2/"+model.QustionVM.CompanyGuid+ "/" + model.QustionVM.DepartmenGuid+ "/" + model.QustionVM.Language + "/" + model.QustionVM.CompanyGuid + "");

            }
        }

        [ValidateInput(false)]
        [Route("step2/{company}/{department}/{langid}/{customer}")]
        public ActionResult step2(string company, string department, string langid, string customer)
        {
            using (SurveyDB db = new SurveyDB())
            {
                ServiceModel model = new ServiceModel();
                model.Company = db.Companies.FirstOrDefault(x => x.CompanyGuid.ToLower() == company.ToLower());
                model.Department = db.Departments.FirstOrDefault(x => x.DepartmenGuid.ToLower() == department.ToLower());
                model.Language = db.Languages.FirstOrDefault(x => x.Code.ToLower() == langid.ToLower());
                model.LangValuesDictionary = db.LanguageValues.Where(x => x.LangId == model.Language.Id).ToDictionary(x => x.Name, x => x.Value);
                return View(model);

            }
        }

    }


}