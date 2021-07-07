namespace testing.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Promotional")]
    public partial class Promotional
    {
        public int ID { get; set; }

        public int? Promoprice { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Expirationdate { get; set; }

        public int? FoodID { get; set; }

        public virtual Food Food { get; set; }
    }
}
