using System.Net.Mail;
using System.Net;
using System.Net.Sockets;
using TestFramework;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System;
using System.Net.Mime;
using System.Text;

namespace Net.Test
{
    public class Mail_Test:ITest
    {
        public Mail_Test()
            :base("Mail_Test",1)
        {

        }
        public override void Run(UpdateTaskProgress update)
        {
            MailMessage msg = new()
            {
                Subject = "测试",
                From = new MailAddress("tyf_xjdd@163.com")
            };
            string s = "<img src=\"https://d13kpx15pujacf.cloudfront.net/images/edsv2/logo/logo-header-7f58bb8e63f9a1a1924ca758fce0c271.png\" width=\"150\" height=\"74\" alt=\"ExpressVPN\" border=\"0\">";
            msg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(s, Encoding.UTF8, MediaTypeNames.Text.Html));
            msg.To.Add("tyf_xjdd@163.com");
            SmtpClient client = new()
            {
                Host = "smtp.163.com",
                Port = 25,
                UseDefaultCredentials = false,
                EnableSsl = true,
                Credentials = new NetworkCredential("tyf_xjdd@163.com", "ZHVHMVKZXQFZDBPW"),
                DeliveryMethod = SmtpDeliveryMethod.Network,
            };
            client.Send(msg);
        }
    }
}
