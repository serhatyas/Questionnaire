﻿using Questionnaire.Models.EntityFramework;
using Questionnaire.Models.ServiceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Questionnaire.Areas.Admin.Controllers
{
    public class ReportingController : Controller
    {
        // GET: Admin/Reporting
        public ActionResult Index()
        {
            using (SurveyDB db=new SurveyDB())
            {
                ServiceModel model = new ServiceModel();
                model.Customers = db.Customers.Where(x => x.CompanyGuid == model.CurrentCompany.CompanyGuid).ToList();
                model.CustomerAnswers = db.CustomerAnswers.Where(x => x.CompanyGuid == model.CurrentCompany.CompanyGuid).ToList();
                model.Departments = db.Departments.Where(x => x.CompanyGuid == model.CurrentCompany.CompanyGuid).ToList();
                model.Questions = db.Questions.Where(x => x.CompanyGuid == model.CurrentCompany.CompanyGuid).ToList();
                model.SystemUsers = db.SystemUsers.Where(x => x.CompanyGuid == model.CurrentCompany.CompanyGuid).ToList();
                var sss = model.CurrentUser;
                return View(model);
            }
        }
    }
}