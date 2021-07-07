using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
using testing.Models;

namespace testing.ViewModels
{
    public class DangKy
    {
        
        [StringLength(50)]
        [Required(ErrorMessage = "Nhập họ và tên")]
        public string Fullname { get; set; }

        [Required(ErrorMessage = "Nhập địa chỉ")]
        [StringLength(100)]
        public string Address { get; set; }

        [Required(ErrorMessage ="Nhập Email")]
        [EmailAddress(ErrorMessage ="Sai định dạng email")]
        [StringLength(100)]
        public string Gmail { get; set; }

        [Required(ErrorMessage = "Nhập số điện thoại")]
        [RegularExpression(@"^[0-9]{11}", ErrorMessage = "Tối đa 11 số")]
        [StringLength(11)]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Nhập tên đăng nhập")]
        [StringLength(30)]   
        public string Username { get; set; }

        [Required(ErrorMessage = "Nhập mật khẩu")]
        [StringLength(30)]
        public string Password { get; set; }
        

        [NotMapped]
        [StringLength(30)]
        [Required(ErrorMessage ="Nhập lại mật khẩu")]
        [System.ComponentModel.DataAnnotations.Compare("Password",ErrorMessage ="Mật khẩu không trùng nhau")]
        public string ConfirmPassword { get; set; }


        public int CusID { get; set; }

        
    }
}