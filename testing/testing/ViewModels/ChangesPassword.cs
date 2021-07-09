using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace testing.ViewModels
{
    public class ChangesPassword
    {
        [Required(ErrorMessage = "Nhập mật khẩu cũ")]
        [Display(Name = ("Mật khẩu cũ"))]
        public string oldPassword { get; set; }

        [Required(ErrorMessage ="Nhập mật khẩu mới")]
        [Display(Name = ("Mật khẩu mới"))]
        public string newPassword { get; set; }

        [Required(ErrorMessage ="Nhập lại mật khẩu mới")]
        [System.ComponentModel.DataAnnotations.Compare("newPassword", ErrorMessage = "Mật khẩu mới không trùng nhau")]
        public string newPassword2 { get; set; }
    }
}