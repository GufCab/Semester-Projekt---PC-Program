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
    
    public partial class musicdata
    {
        public string UUIDMusikData { get; set; }
        public string Title { get; set; }
        public Nullable<int> NrLenth { get; set; }
        public string FileName { get; set; }
        public string Genre_Genre { get; set; }
        public string Album_Album { get; set; }
        public string Artist_Artist { get; set; }
        public string FilePath_UUIDPath { get; set; }
    
        public virtual album album { get; set; }
        public virtual artist artist { get; set; }
        public virtual filepath filepath { get; set; }
        public virtual genre genre { get; set; }
    }
}
