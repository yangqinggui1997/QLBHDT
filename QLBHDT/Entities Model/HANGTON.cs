//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QLBHDT.Entities_Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class HANGTON
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HANGTON()
        {
            this.HTCHITIETs = new HashSet<HTCHITIET>();
        }
    
        public string IdHT { get; set; }
        public string IdNV { get; set; }
        public System.DateTime NgayTK { get; set; }
    
        public virtual NHANVIEN NHANVIEN { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HTCHITIET> HTCHITIETs { get; set; }
    }
}
