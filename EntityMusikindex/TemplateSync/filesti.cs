//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TemplateSync
{
    using System;
    using System.Collections.Generic;
    
    public partial class filesti
    {
        public filesti()
        {
            this.musikdatas = new HashSet<musikdata>();
        }
    
        public int idFilesti { get; set; }
        public string Filesti1 { get; set; }
        public string IP_idIP { get; set; }
    
        public virtual ip ip { get; set; }
        public virtual ICollection<musikdata> musikdatas { get; set; }
    }
}
