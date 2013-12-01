using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Web.Mvc;
using System.Web.Security;
using System.Globalization;

namespace OzonShop.Models
{
    public class ResetPassword
    {
        public string ResetPasswordToken { get; set; }

        [Required]
        [Display(Name = "NewPassword", ResourceType = typeof(GlobalRes.Resource))]
        [StringLength(100, ErrorMessageResourceType = typeof(GlobalRes.Resource),
                           ErrorMessageResourceName = "PasswordRequired",
                           MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "ConfirmPassword", ResourceType = typeof(GlobalRes.Resource))]
        [Compare("NewPassword", ErrorMessageResourceType = typeof(GlobalRes.Resource),
                  ErrorMessageResourceName = "PasswordConfirmRequired")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginModel
    {
        [Required]
        [Display(Name = "Login", ResourceType = typeof(GlobalRes.Resource))]
        public string Login { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password", ResourceType = typeof(GlobalRes.Resource))]
        public string Password { get; set; }

        [Display(Name = "RememberMe", ResourceType = typeof(GlobalRes.Resource))]
        public bool RememberMe { get; set; }
    }

    public class RegisterModel
    {
        [Required(ErrorMessageResourceType = typeof(GlobalRes.Resource),
                  ErrorMessageResourceName = "NameRequired")]
        [Display(Name = "UserName", ResourceType = typeof(GlobalRes.Resource))]
        public string UserName { get; set; }

        [Required(ErrorMessageResourceType = typeof(GlobalRes.Resource),
                  ErrorMessageResourceName = "LoginRequired")]
        [Display(Name = "LogName", ResourceType = typeof(GlobalRes.Resource))]
        public string Login { get; set; }

        [Required(ErrorMessageResourceType = typeof(GlobalRes.Resource),
                  ErrorMessageResourceName = "EmailRequired")]
        [Display(Name = "Email", ResourceType = typeof(GlobalRes.Resource))]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Distribution", ResourceType = typeof(GlobalRes.Resource))]
        public bool Distribution { get; set; }

        [Required(ErrorMessageResourceType = typeof(GlobalRes.Resource),
                  ErrorMessageResourceName = "PhoneRequired")]
        [Display(Name = "Phone", ResourceType = typeof(GlobalRes.Resource))]
        //[DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Required]
        [Display(Name = "Password", ResourceType = typeof(GlobalRes.Resource))]
        [StringLength(100, ErrorMessageResourceType = typeof(GlobalRes.Resource),
                           ErrorMessageResourceName = "PasswordRequired",
                           MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Required]
        [Display(Name = "ConfirmPassword", ResourceType = typeof(GlobalRes.Resource))]
        [Compare("Password", ErrorMessageResourceType = typeof(GlobalRes.Resource),
                  ErrorMessageResourceName = "PasswordConfirmRequired")]
        public string ConfirmPassword { get; set; }
    }
}