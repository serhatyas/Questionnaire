using Questionnaire.Helpers;
using Questionnaire.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Questionnaire.Models.ServiceModel
{
    public class ServiceModel
    {
        public ServiceModel()
        {
            using (SurveyDB db = new SurveyDB())
            {
                IpAddressManager ipadres = new IpAddressManager();
                //var language = ipadres.GetCountry(ipadres.GetClientIp());
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    var userid = Convert.ToInt32(HttpContext.Current.User.Identity.Name);

                    CurrentUser = db.SystemUsers.FirstOrDefault(x => x.Id == userid);
                    if(CurrentUser!=null)
                    {
                        CurrentCompany = db.Companies.FirstOrDefault(x => x.CompanyGuid == CurrentUser.CompanyGuid);
                        //HttpCookie Cookie = null;
                        //Cookie = new HttpCookie("language");
                        //Cookie.Expires = DateTime.Now.AddDays(100);
                        //Cookie["language"] = Convert.ToString("tr-TR");
                        //HttpContext.Current.Response.Cookies.Add(Cookie);
                    }
                  
                }
                Languages = db.Languages.Where(x => x.IsPassive == false && x.IsDeleted == false).ToList();
            }

        }

        public Departments Department { get; set; }
        public List<Departments> Departments { get; set; }

        public Answers Answer { get; set; }
        public List<Answers> Answers { get; set; }

        public Companies Company { get; set; }
        public List<Companies> Companies { get; set; }

        public Customers Customer { get; set; }
        public List<Customers> Customers { get; set; }

        public CustomerType customerType { get; set; }
        public List<CustomerType> customerTypes { get; set; }

        public DepartmentQuestions DepartmentQuestion { get; set; }
        public List<DepartmentQuestions> DepartmentQuestions { get; set; }

        public Languages Language { get; set; }
        public List<Languages> Languages { get; set; }

        public LanguageValues LanguageValue { get; set; }
        public List<LanguageValues> LanguageValues { get; set; }

        public Questions Question { get; set; }
        public List<Questions> Questions { get; set; }

        public SystemUsers SystemUser { get; set; }
        public List<SystemUsers> SystemUsers { get; set; }

        public SystemUsers CurrentUser { get; set; }
        public Companies CurrentCompany { get; set; }

        public CustomerAnswers  CustomerAnswer { get; set; }
        public List<CustomerAnswers> CustomerAnswers { get; set; }

        public IDictionary<string,string> LangValuesDictionary { get; set; }

        public QuestionVM QustionVM { get; set; }

        public  QuestionPath questionPath { get; set; }
        public List<QuestionPath> QuestionPaths { get; set; }

    }
}