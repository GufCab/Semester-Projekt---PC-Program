//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace dbclases
{
    using System;
    using System.Collections.Generic;
    
    public partial class musik
    {
        public musik()
        {
            this.albums = new HashSet<album>();
            this.artists = new HashSet<artist>();
            this.genres = new HashSet<genre>();
        }
    
        public int Catagory_idCatagory { get; set; }
    
        public virtual ICollection<album> albums { get; set; }
        public virtual ICollection<artist> artists { get; set; }
        public virtual catagory catagory { get; set; }
        public virtual ICollection<genre> genres { get; set; }
    }
}
