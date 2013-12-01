using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Net;

namespace OzonShop.Helpers
{
    public class SendToEMail
    {
        private static String homeAddress = @"http://localhost:58819/";
        private static String senderEmail = "vyshinskayan@gmail.com";

        public void SendResetPasswordToken(String token, String email)
        {
            var smtp = new SmtpClient();
            smtp.Send(new MailMessage(senderEmail, email, "Reset password",
                String.Format("Please, follow the link, to reset your password \n{0}{1}{2}", homeAddress, @"Account/ResetPassword/", token)));
        }

        public void SendSuccessRegistration(String token, String email)
        {
            var smtp = new SmtpClient();
            smtp.Send(new MailMessage(senderEmail, email, "The registration is success!",
                String.Format("Please, follow the link, for confirmation \n{0}{1}{2}", homeAddress, @"Account/RegisterConfirmed/", token)));
        }
    }
}