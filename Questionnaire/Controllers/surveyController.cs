using Questionnaire.Models.EntityFramework;
using Questionnaire.Models.ServiceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Questionnaire.Controllers
{
    public class surveyController : Controller
    {
        // GET: survey
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult completed(FormCollection fc, HttpPostedFileBase[] Documents)
        {
            using (SurveyDB db = new SurveyDB())
            {
                ServiceModel model = new ServiceModel();
                string companyGuid = fc["QustionVM.CompanyGuid"];
                string customerGuid = fc["Customer.CustomerGuid"];
                string languageCode = fc["QustionVM.Language"];
                string departmenGuid = fc["QustionVM.DepartmenGuid"];

                model.Department = db.Departments.FirstOrDefault(x => x.DepartmenGuid == departmenGuid);
                model.Customer = db.Customers.FirstOrDefault(x => x.CustomerGuid == customerGuid);
                model.Company = db.Companies.FirstOrDefault(x => x.CompanyGuid == companyGuid);
                model.Language = db.Languages.FirstOrDefault(x => x.Code == languageCode);
                model.QuestionPaths = db.QuestionPath.Where(x => x.IsPassive == false).ToList();

                var DateTimeNow = DateTime.Now;
                var questions = db.DepartmentQuestions.Where(x => x.CompanyGuid == model.Company.CompanyGuid && x.DepartmentId == model.Department.Id).OrderBy(x => x.QuestionId).ToList();

                foreach (var item in questions)
                {

                    var quest = fc["quest" + item.QuestionId];


                    if (quest != null)
                    {

                        CustomerAnswers answer = new CustomerAnswers();
                        answer.QuestionId = item.QuestionId;
                        answer.CompanyGuid = model.Company.CompanyGuid;
                        answer.CustomerId = model.Customer.Id;
                        answer.AnswerId = Convert.ToInt32(quest);
                        answer.DepartmentId = model.Department.Id;
                        answer.Date = DateTimeNow;
                        db.CustomerAnswers.Add(answer);

                        var descriptions = fc["description" + item.QuestionId];

                        if (descriptions != null)
                        {

                            QuestionPath questionpath = new QuestionPath();
                            questionpath.Description = descriptions;
                            questionpath.CustomerGuid = customerGuid;
                            questionpath.QuestionId = item.QuestionId;
                            questionpath.CustomerAnswersId = answer.Id;
                            questionpath.IsPassive = false;
                            questionpath.Date = DateTime.Now;

                            HttpPostedFileBase Document = Request.Files["documents" + item.QuestionId];

                            if (Document != null)
                            {

                                string filePath = System.IO.Path.Combine(Server.MapPath("/uploads/images/") + Document.FileName);
                                Document.SaveAs(filePath);
                                questionpath.Path = "/Uploads/images/" + Document.FileName;
                            }

                            db.QuestionPath.Add(questionpath);
                        }


                    }

                    db.SaveChanges();

                }
                return View();

            }
        }
    }
}