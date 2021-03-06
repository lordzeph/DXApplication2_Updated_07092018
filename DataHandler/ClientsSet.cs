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
    
    public partial class ClientsSet
    {
        public ClientsSet()
        {
            this.ClientsServices = new HashSet<ClientsServices>();
        }
    
        public int ClientId { get; set; }
        public string IDName { get; set; }
        public string IDNumber { get; set; }
        public Nullable<System.DateTime> BirthDate { get; set; }
        public Nullable<bool> Gender { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public Nullable<System.DateTime> VisaLimit { get; set; }
        public string Adress { get; set; }
        public string ExtPassportName { get; set; }
        public string BirthPlace { get; set; }
        public string ExtPassportNumber { get; set; }
        public Nullable<System.DateTime> ExtpassportExpDate { get; set; }
        public string Extra1 { get; set; }
        public string Extra2 { get; set; }
        public Nullable<decimal> Extra3 { get; set; }
        public string Notes { get; set; }
        public bool IsActive { get; set; }
        public string Serial { get; set; }
        public Nullable<System.DateTime> Extra4 { get; set; }
        public Nullable<bool> HasEmail { get; set; }
        public string CitizenShip { get; set; }
    
        public virtual ICollection<ClientsServices> ClientsServices { get; set; }
    }
}
