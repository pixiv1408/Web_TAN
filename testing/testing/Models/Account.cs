namespace testing.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Account")]
    public partial class Account
    {
        [Key]
        [StringLength(30)]
        public string Username { get; set; }

        [StringLength(30)]
        public string Password { get; set; }

        public int? Position { get; set; }

        public int? CusID { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual Position Position1 { get; set; }
    }
}
