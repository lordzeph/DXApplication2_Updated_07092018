//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataHandler
{
    using System;
    using System.Collections.Generic;
    
    public partial class ServicesSet
    {
        public ServicesSet()
        {
            this.ClientsServices = new HashSet<ClientsServices>();
            this.ExpensesSet = new HashSet<ExpensesSet>();
            this.IncomesSet = new HashSet<IncomesSet>();
            this.PartnersServices = new HashSet<PartnersServices>();
        }
    
        public int ServiceId { get; set; }
        public string Servicename { get; set; }
        public int SeviceTypeID { get; set; }
        public int CompanyID { get; set; }
        public decimal NetPrice { get; set; }
        public decimal PrecioBruto { get; set; }
        public System.DateTime CreationDate { get; set; }
        public string Specs { get; set; }
        public string Comission { get; set; }
        public System.DateTime FromDate { get; set; }
        public System.DateTime ToDate { get; set; }
        public int UserId { get; set; }
        public Nullable<int> CountryId { get; set; }
    
        public virtual ICollection<ClientsServices> ClientsServices { get; set; }
        public virtual CompaniesSet CompaniesSet { get; set; }
        public virtual CountriesSet CountriesSet { get; set; }
        public virtual ICollection<ExpensesSet> ExpensesSet { get; set; }
        public virtual ICollection<IncomesSet> IncomesSet { get; set; }
        public virtual ICollection<PartnersServices> PartnersServices { get; set; }
        public virtual ServiceTypes ServiceTypes { get; set; }
        public virtual Users Users { get; set; }
    }
}
