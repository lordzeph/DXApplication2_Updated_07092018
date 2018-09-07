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
    
    public partial class CompaniesSet
    {
        public CompaniesSet()
        {
            this.EalthServicesSet = new HashSet<EalthServicesSet>();
            this.IncomeTableSet = new HashSet<IncomeTableSet>();
            this.ServicesSet = new HashSet<ServicesSet>();
            this.UserCompanies = new HashSet<UserCompanies>();
            this.CashOperationsSet = new HashSet<CashOperationsSet>();
            this.BankAccountsSet = new HashSet<BankAccountsSet>();
        }
    
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string VisualProp { get; set; }
        public string LogoAdress { get; set; }
        public string Color { get; set; }
        public string CompanyAdress { get; set; }
    
        public virtual ICollection<EalthServicesSet> EalthServicesSet { get; set; }
        public virtual ICollection<IncomeTableSet> IncomeTableSet { get; set; }
        public virtual ICollection<ServicesSet> ServicesSet { get; set; }
        public virtual ICollection<UserCompanies> UserCompanies { get; set; }
        public virtual ICollection<CashOperationsSet> CashOperationsSet { get; set; }
        public virtual ICollection<BankAccountsSet> BankAccountsSet { get; set; }
    }
}
