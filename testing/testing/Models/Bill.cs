namespace testing.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Bill")]
    public partial class Bill
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Bill()
        {
            Details = new HashSet<Detail>();
        }

        public int BillID { get; set; }

        [Column(TypeName = "date")]
        public DateTime? BDate { get; set; }

        public int? BTotal { get; set; }

        public bool? BCart { get; set; }

        [StringLength(500)]
        public string note { get; set; }

        public int? CusID { get; set; }

        public virtual Customer Customer { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Detail> Details { get; set; }
    }
}
