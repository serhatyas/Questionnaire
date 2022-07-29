using Questionnaire.Models.EntityFramework;
using Questionnaire.Models.ServiceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Questionnaire.Areas.Admin.Controllers
{
    [Authorize]
    public class questionsController : Controller
    {
        public questionsController()
        {
            ViewBag.Question = "active";
        }
        // GET: Admin/questions
        [HttpGet]
        public ActionResult Index()
        {
            using (SurveyDB db = new SurveyDB())
            {
                ServiceModel model = new ServiceModel();
                model.Departments = db.Departments.Where(x => x.IsDeleted == false && x.CompanyGuid == model.CurrentUser.CompanyGuid).OrderBy(x => x.Title).ToList();
                model.Questions = db.Questions.Where(x => x.IsDeleted == false && x.CompanyGuid == model.CurrentUser.CompanyGuid).ToList();
                model.DepartmentQuestions = db.DepartmentQuestions.Where(x=>x.Id>0).ToList();
                return View(model);

            }
        }

        [HttpGet]
        public ActionResult create(int langid)
        {
            using (SurveyDB db = new SurveyDB())
            {
                ServiceModel model = new ServiceModel();
                model.Language = db.Languages.FirstOrDefault(x => x.Id == langid);
                model.SystemUsers = db.SystemUsers.Where(x => x.IsPassive == false && x.IsDeleted == false && x.CompanyGuid == model.CurrentUser.CompanyGuid).ToList();
                model.Departments = db.Departments.Where(x => x.IsPassive == false && x.IsDeleted == false && x.LangId == langid).ToList();
                return View(model);
            }
        }


        [HttpPost]
        public ActionResult create(ServiceModel model, HttpPostedFileBase Icon, FormCollection fc)
        {
            using (SurveyDB db = new SurveyDB())
            {
                try
                {
                    var Answers = fc.GetValues("Answers");
                    var departmens = fc.GetValues("departments");
                    model.Question.LangId = model.Language.Id;
                    model.Question.CompanyGuid = model.CurrentUser.CompanyGuid;
                    model.Question.CreatedDateTime = DateTime.Now;
                    model.Question.CreatedUserId = model.CurrentUser.Id;
                    model.Question.IsPassive = false;
                    model.Question.IsDeleted = false;
                    db.Questions.Add(model.Question);
                    db.SaveChanges();

                    foreach (var item in Answers)
                    {
                        Questionnaire.Models.EntityFramework.Answers answers = new Answers();
                        answers.AnswerType = true;
                        answers.CompanyGuid = model.CurrentUser.CompanyGuid;
                        answers.Content = item;
                        answers.CreatedDateTime = DateTime.Now;
                        answers.CreatedUserId = model.CurrentUser.Id;
                        answers.IsDeleted = false;
                        answers.IsPassive = false;
                        answers.LangId = model.Language.Id;
                        answers.QuestionId = model.Question.Id;
                        db.Answers.Add(answers);
                    }

                    foreach (var item in departmens)
                    {
                        DepartmentQuestions dq = new DepartmentQuestions();
                        dq.DepartmentId = Convert.ToInt32(item);
                        dq.Date = DateTime.Now;
                        dq.CompanyGuid = model.CurrentUser.CompanyGuid;
                        dq.QuestionId = model.Question.Id;
                        db.DepartmentQuestions.Add(dq);

                    }
                    if (Icon != null)
                    {
                        string filePath = System.IO.Path.Combine(Server.MapPath("/uploads/images/") + Icon.FileName);
                        Icon.SaveAs(filePath);
                        model.Department.Icon = "/Uploads/images/" + Icon.FileName;
                    }
                    db.SaveChanges();
                    return Redirect("/admin/questions");
                }
                catch (Exception)
                {
                    return Redirect("/500");

                }
            }
        }






        [HttpGet]
        public ActionResult update(int id)
        {
            using (SurveyDB db = new SurveyDB())
            {
                ServiceModel model = new ServiceModel();
                model.Question = db.Questions.FirstOrDefault(x => x.Id == id);

                model.Language = db.Languages.FirstOrDefault(x => x.Id == model.Question.LangId);
                model.SystemUsers = db.SystemUsers.Where(x => x.IsPassive == false && x.IsDeleted == false && x.CompanyGuid == model.CurrentUser.CompanyGuid).ToList();
                model.Answers = db.Answers.Where(x => x.QuestionId == model.Question.Id && x.IsPassive == false && x.IsDeleted == false).ToList();
                model.DepartmentQuestions = db.DepartmentQuestions.Where(x => x.QuestionId == id).ToList();
                model.Departments = db.Departments.Where(x => x.IsPassive == false && x.IsDeleted == false && x.LangId == model.Question.LangId).ToList();

                return View(model);
            }
        }


        [HttpPost]
        public ActionResult update(ServiceModel model, HttpPostedFileBase Icon, FormCollection fc)
        {
            using (SurveyDB db = new SurveyDB())
            {
                try
                {
                    var answers = fc.GetValues("Answers");
                    var currentAnswers = fc.GetValues("currentAnswers");
                    var departmens = fc.GetValues("departments");

                    var question = db.Questions.FirstOrDefault(x => x.Id == model.Question.Id);
                    question.Score = model.Question.Score;
                    question.Queue = model.Question.Queue;
                    question.Content = model.Question.Content;
                    for (int i = 0; i < answers.Length; i++)
                    {
                        if ((currentAnswers.Length) <= i)
                        {
                            Questionnaire.Models.EntityFramework.Answers Answers = new Answers();
                            Answers.AnswerType = true;
                            Answers.CompanyGuid = model.CurrentUser.CompanyGuid;
                            Answers.Content = answers[i];
                            Answers.CreatedDateTime = DateTime.Now;
                            Answers.CreatedUserId = model.CurrentUser.Id;
                            Answers.IsDeleted = false;
                            Answers.IsPassive = false;
                            Answers.LangId = model.Language.Id;
                            Answers.QuestionId = model.Question.Id;
                            db.Answers.Add(Answers);
                        }
                        else
                        {
                            int currentAnswerId = Convert.ToInt32(currentAnswers[i]);
                            var currentAnswer = db.Answers.FirstOrDefault(x => x.Id == currentAnswerId);
                            currentAnswer.Content = answers[i];
                            db.SaveChanges();
                        }

                    }
                    db.DepartmentQuestions.RemoveRange(db.DepartmentQuestions.Where(x => x.QuestionId == model.Question.Id).ToList());
                    db.SaveChanges();
                    foreach (var item in departmens)
                    {
                        DepartmentQuestions dq = new DepartmentQuestions();
                        dq.DepartmentId = Convert.ToInt32(item);
                        dq.Date = DateTime.Now;
                        dq.CompanyGuid = model.CurrentUser.CompanyGuid;
                        dq.QuestionId = model.Question.Id;
                        db.DepartmentQuestions.Add(dq);



                    }
                    db.SaveChanges();
                    return Redirect("/admin/questions");
                }
                catch (Exception)
                {
                    return Redirect("/500");

                }

            }
        }




        [HttpGet]
        public ActionResult delete(int id)
        {
            using (SurveyDB db = new SurveyDB())
            {

                var item = db.Questions.FirstOrDefault(x => x.Id == id);
                if (item != null)
                {
                    item.IsDeleted = true;
                    db.SaveChanges();
                    return Json("Basarili", JsonRequestBehavior.AllowGet);
                }
                return View();

            }
        }


        [HttpGet]
        public ActionResult passive(int id)
        {
            using (SurveyDB db = new SurveyDB())
            {

                var item = db.Questions.FirstOrDefault(x => x.Id == id);
                if (item != null)
                {
                    if (item.IsPassive == false)
                        item.IsPassive = true;
                    else
                        item.IsPassive = false;
                    db.SaveChanges();
                    return Json("Basarili", JsonRequestBehavior.AllowGet);
                }
                return View();

            }
        }



        public ActionResult deleteQuestion(int id)
        {
            using (SurveyDB db = new SurveyDB())
            {

                var item = db.Answers.FirstOrDefault(x => x.Id == id);
                if (item != null)
                {
                    item.IsDeleted = true;
                    db.SaveChanges();
                    return Json("Basarili", JsonRequestBehavior.AllowGet);
                }
                return View();

            }
        }
    }
}