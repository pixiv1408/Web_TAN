namespace testing.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Detail")]
    public partial class Detail
    {
        public int DetailID { get; set; }

        public int? DAmount { get; set; }

        [StringLength(30)]
        public string DFName { get; set; }

        public int? DfPrice { get; set; }

        [StringLength(255)]
        public string DFimg { get; set; }

        public int? DFoodID { get; set; }

        public int? DOderID { get; set; }

        public virtual Bill Bill { get; set; }

        public virtual Food Food { get; set; }
    }
}
