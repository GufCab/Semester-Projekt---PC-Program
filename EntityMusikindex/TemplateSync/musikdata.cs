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
    
    public partial class musikdata
    {
        public long idMusikData { get; set; }
        public string Title { get; set; }
        public string Artist { get; set; }
        public string Album { get; set; }
        public string Genre { get; set; }
        public Nullable<int> NrLenth { get; set; }
        public string FilePath_idFilePath { get; set; }
        public string FileName { get; set; }
    
        public virtual filepath filepath { get; set; }
    }
}
