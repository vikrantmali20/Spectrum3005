//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Spectrum.DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class CharacteristicsValue
    {
        public CharacteristicsValue()
        {
            this.ArticleMatrix = new HashSet<ArticleMatrix>();
        }
    
        public string CharCode { get; set; }
        public string CharValue { get; set; }
        public string CREATEDAT { get; set; }
        public string CREATEDBY { get; set; }
        public Nullable<System.DateTime> CREATEDON { get; set; }
        public string UPDATEDAT { get; set; }
        public string UPDATEDBY { get; set; }
        public Nullable<System.DateTime> UPDATEDON { get; set; }
        public Nullable<bool> STATUS { get; set; }
    
        public virtual MstCharacteristics MstCharacteristics { get; set; }
        public virtual ICollection<ArticleMatrix> ArticleMatrix { get; set; }
    }
}