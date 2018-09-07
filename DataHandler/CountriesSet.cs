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
    
    public partial class CountriesSet
    {
        public CountriesSet()
        {
            this.HotelsSet = new HashSet<HotelsSet>();
            this.ServicesSet = new HashSet<ServicesSet>();
        }
    
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public string CountryCode { get; set; }
        public string EnglishName { get; set; }
        public string SpanishName { get; set; }
        public string RussianName { get; set; }
    
        public virtual ICollection<HotelsSet> HotelsSet { get; set; }
        public virtual ICollection<ServicesSet> ServicesSet { get; set; }
    }
}