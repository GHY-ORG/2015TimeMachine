using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _2015TimeMachineCookie.Models
{
    public class RegisterForm
    {
        [Required(ErrorMessage = "学号必填")]
        [Display(Name = "学号")]
        public string StudentNo { set; get; }


        [Required(ErrorMessage = "上网密码必填")]
        [Display(Name = "上网密码")]
        [DataType(DataType.Password)]
        public string SPassword { set; get; }


        [Required(ErrorMessage = "手机号必填")]
        [Display(Name = "手机号")]
        [DataType(DataType.PhoneNumber,ErrorMessage="请填写正确格式手机号")]
        [RegularExpression("[0-9]+")]
        [MinLength(6)]
        public string Phone { set; get; }


        [Required(ErrorMessage = "用户名必填")]
        [Display(Name = "用户名")]
        [MinLength(5)]
        public string UserName { set; get; }


        [Required(ErrorMessage = "密码必填")]
        [Display(Name = "密码")]
        [DataType(DataType.Password)]
        [MinLength(6)]
        public string Password1 { set; get; }


        [Required(ErrorMessage = "请确认密码")]
        [Display(Name = "密码确认")]
        [DataType(DataType.Password)]
        [MinLength(6)]
        public string Password2 { set; get; }
    }
}