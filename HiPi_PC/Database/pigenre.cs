//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Database
{
    using System;
    using System.Collections.Generic;
    
    public partial class PIGenre
    {
        public PIGenre()
        {
            this.PIMusikDatas = new HashSet<PIMusikData>();
        }
    
        public string Genre { get; set; }
        public int Musik_Catagory_idCatagory { get; set; }
    
        public virtual Musik Musik { get; set; }
        public virtual ICollection<PIMusikData> PIMusikDatas { get; set; }
    }
}