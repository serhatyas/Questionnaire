using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Questionnaire.Models.ServiceModel
{
    public class QuestionVM
    {
        public string CompanyGuid { get; set; }
        public string DepartmenGuid { get; set; }
        public string Language { get; set; }

        public string CustomerGuid { get; set; }
        [Required(ErrorMessage ="Boş Geçilemez")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Boş Geçilemez")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "Boş Geçilemez")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Boş Geçilemez")]
        public string Mail { get; set; }
        public string Description { get; set; }
        public string IpAddress { get; set; }

    }
}