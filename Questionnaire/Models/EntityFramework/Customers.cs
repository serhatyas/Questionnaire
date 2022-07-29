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
    
    public partial class Customers
    {
        public int Id { get; set; }
        public Nullable<int> TypeId { get; set; }
        public string CompanyGuid { get; set; }
        public string CustomerGuid { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Mail { get; set; }
        public string Description { get; set; }
        public string IpAddress { get; set; }
        public Nullable<System.DateTime> CreatedDateTime { get; set; }
        public Nullable<bool> IsPassive { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
    
        public virtual CustomerType CustomerType { get; set; }
    }
}
