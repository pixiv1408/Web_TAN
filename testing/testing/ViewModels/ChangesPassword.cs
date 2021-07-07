using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace testing.ViewModels
{
    [NotMapped]
    public class ChangesPassword
    {
        [Required]
        [Display(Name =("Mật khẩu cũ"))]
        public string oldPassword { get; set; }
        [Required]
        [Display(Name = ("Mật khẩu mới"))]
        public string newPassword { get; set; }
    }
}