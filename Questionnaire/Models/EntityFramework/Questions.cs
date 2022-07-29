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
    
    public partial class Questions
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Questions()
        {
            this.Answers = new HashSet<Answers>();
            this.DepartmentQuestions = new HashSet<DepartmentQuestions>();
        }
    
        public int Id { get; set; }
        public string CompanyGuid { get; set; }
        public Nullable<int> LangId { get; set; }
        public Nullable<int> Queue { get; set; }
        public string Content { get; set; }
        public string Documents { get; set; }
        public string Description { get; set; }
        public Nullable<int> Score { get; set; }
        public Nullable<System.DateTime> CreatedDateTime { get; set; }
        public Nullable<int> CreatedUserId { get; set; }
        public Nullable<bool> IsPassive { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Answers> Answers { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DepartmentQuestions> DepartmentQuestions { get; set; }
        public virtual Languages Languages { get; set; }
    }
}
