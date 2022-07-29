using Questionnaire.Models.EntityFramework;
using Questionnaire.Models.ServiceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Questionnaire.Areas.Admin.Controllers
{
    public class customerAnswersController : Controller
    {
        // GET: Admin/customerAnswers
        public ActionResult Index(int custommerId,int departmentId)
        {
            using (SurveyDB db = new SurveyDB())
            {
                ServiceModel model = new ServiceModel();
                model.CustomerAnswers = db.CustomerAnswers.Where(x => x.CustomerId == custommerId && x.DepartmentId==departmentId).ToList();
            
                model.Customer = db.Customers.FirstOrDefault(x => x.Id == departmentId);
                model.Questions = db.Questions.Where(x => x.CompanyGuid == model.Customer.CompanyGuid).ToList();
                model.Answers = db.Answers.Where(x => x.CompanyGuid == model.Customer.CompanyGuid).ToList();
                model.Department = db.Departments.FirstOrDefault(x => x.Id == departmentId);
                model.DepartmentQuestions = db.DepartmentQuestions.Where(x => x.DepartmentId == departmentId && x.CompanyGuid == model.CurrentCompany.CompanyGuid).ToList();
                model.QuestionPaths = db.QuestionPath.Where(x => x.IsPassive==false).ToList();
                return View(model);
            }
        }
    }
}