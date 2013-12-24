using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;
using System.ServiceModel;
using OzonShop.Filters;
using OzonShop.Models;
using OzonShop.Helpers;

namespace OzonShop.Controllers
{
    [Culture]
    [Authorize]
    [InitializeSimpleMembership]
    public class AccountController : Controller
    {
        [HttpGet]
        [AllowAnonymous]
        public ActionResult ResetPassword(string Id)
        {
            return View("ResetPassword", new ResetPassword() { ResetPasswordToken = Id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(ResetPassword model)
        {
            if (WebSecurity.ResetPassword(model.ResetPasswordToken, model.NewPassword))
            {
                return RedirectToAction("Login", "Account");
            }
            ModelState.AddModelError("Error", "Error while confirming password");
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid && WebSecurity.Login(model.Login, model.Password, persistCookie: model.RememberMe) && WebSecurity.IsConfirmed(model.Login))
            {
                return RedirectToAction("PersonalPage", "Personal");
            }
            ModelState.AddModelError("", GlobalRes.Resource.IncorrectNameOrPassword);
            return View(model);
        }

        [HttpGet]
        public ActionResult LogOff()
        {
            WebSecurity.Logout();
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    String token = WebSecurity.CreateUserAndAccount(model.Login, model.Password,
                        new { UserName = model.UserName, Email = model.Email, Distribution = model.Distribution, Phone = model.Phone }, true);
                    new SendToEMail().SendSuccessRegistration(token, model.Email);
                    //WebSecurity.Login(model.Login, model.Password);
                    //Roles.AddUserToRole(model.Login, "admin");                  
                    SetMessage(GlobalRes.Resource.PostMail, GlobalRes.Resource.PostMail, GlobalRes.Resource.PostOnMailConfirmingOfRegistration);
                    return View("message");
                }
                catch (MembershipCreateUserException e)
                {
                    ModelState.AddModelError("", ErrorCodeToString(e.StatusCode));
                }
            }
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        [Culture]
        public ActionResult RegisterConfirmed(String Id)
        {
            if (WebSecurity.ConfirmAccount(Id))
            {
                SetMessage(GlobalRes.Resource.RegisterConfirmed,
                           GlobalRes.Resource.RegistrationSuccess,
                           GlobalRes.Resource.LoginToTheSystem);
            }
            else
            {
                SetMessage(GlobalRes.Resource.RegisterConfirmed,
                           GlobalRes.Resource.RegistrationFailed,
                           GlobalRes.Resource.RepeatRegisration);
            }
            return View("message");
        }

        private void SetMessage(String Title, String messageTitle, String Message)
        {
            ViewBag.Title = Title;
            ViewBag.MessageTitle = messageTitle;
            ViewBag.Message = Message;
        }

        #region Helpers
        [Culture]
        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return GlobalRes.Resource.DuplicateUserName;

                case MembershipCreateStatus.DuplicateEmail:
                    return GlobalRes.Resource.DuplicateEmail;

                case MembershipCreateStatus.InvalidPassword:
                    return GlobalRes.Resource.InvalidPassword;

                case MembershipCreateStatus.InvalidEmail:
                    return GlobalRes.Resource.InvalidEmail;

                case MembershipCreateStatus.InvalidAnswer:
                    return GlobalRes.Resource.InvalidAnswer;

                case MembershipCreateStatus.InvalidQuestion:
                    return GlobalRes.Resource.InvalidQuestion;

                case MembershipCreateStatus.InvalidUserName:
                    return GlobalRes.Resource.InvalidUserName;

                case MembershipCreateStatus.ProviderError:
                    return GlobalRes.Resource.ProviderError;

                case MembershipCreateStatus.UserRejected:
                    return GlobalRes.Resource.UserRejected;
                default:
                    return GlobalRes.Resource.DefaultMess;
            }
        }
        #endregion
    }
}
