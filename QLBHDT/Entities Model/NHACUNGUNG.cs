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
    
    public partial class NHACUNGUNG
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NHACUNGUNG()
        {
            this.CNCHITIETs = new HashSet<CNCHITIET>();
            this.HOADONNHAPs = new HashSet<HOADONNHAP>();
            this.SANPHAMs = new HashSet<SANPHAM>();
        }
    
        public string IdNCU { get; set; }
        public string TenNCU { get; set; }
        public string DiaChi { get; set; }
        public string SĐT { get; set; }
        public string QuyMoNCU { get; set; }
        public Nullable<double> ConnoNCU { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CNCHITIET> CNCHITIETs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HOADONNHAP> HOADONNHAPs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SANPHAM> SANPHAMs { get; set; }
    }
}