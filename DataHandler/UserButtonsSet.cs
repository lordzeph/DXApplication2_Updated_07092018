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
    
    public partial class UserButtonsSet
    {
        public int ButtonsId { get; set; }
        public string Name { get; set; }
        public int UsertypeID { get; set; }
        public string IconId { get; set; }
    
        public virtual UserType UserType { get; set; }
    }
}
