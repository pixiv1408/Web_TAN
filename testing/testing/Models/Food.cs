namespace testing.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Food")]
    public partial class Food
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Food()
        {
            Details = new HashSet<Detail>();
            Promotionals = new HashSet<Promotional>();
        }

        public int FoodID { get; set; }

        [StringLength(30)]
        public string FName { get; set; }

        [StringLength(255)]
        public string Fimage { get; set; }

        [StringLength(200)]
        [DataType(DataType.MultilineText)]
        public string FDescribe { get; set; }

        public bool? FStatus { get; set; }

        public int? FPrice { get; set; }

        public int? CID { get; set; }

        public virtual Category Category { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Detail> Details { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Promotional> Promotionals { get; set; }

        //add list loai mon
        public List<Category> listCategory = new List<Category>();
    }
}
