//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Questionnaire.Models.EntityFramework
{
    using System;
    using System.Collections.Generic;
    
    public partial class DepartmentQuestions
    {
        public int Id { get; set; }
        public string CompanyGuid { get; set; }
        public Nullable<int> DepartmentId { get; set; }
        public Nullable<int> QuestionId { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
    
        public virtual Departments Departments { get; set; }
        public virtual Questions Questions { get; set; }
    }
}