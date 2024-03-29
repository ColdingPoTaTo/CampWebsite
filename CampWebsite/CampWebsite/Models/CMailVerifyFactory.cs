﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace CampWebsite.Models
{
    public class CMailVerifyFactory
    {
        public void SendVerificationMail(string userEmail)
        {
            try
            {
                //  MailMessage ( FromEmailAddress, ToEmailAddress)
                MailMessage message = new MailMessage("<service@lookcamp.site>", userEmail);//MailMessage(寄信者, 收信者)
                message.IsBodyHtml = true;
                message.BodyEncoding = System.Text.Encoding.UTF8;//E-mail編碼
                message.SubjectEncoding = System.Text.Encoding.UTF8;//E-mail編碼
                message.Priority = MailPriority.Normal;//設定優先權
                message.Subject = "請驗證電子信箱"; //E-mail主旨
                string domainName = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);
                message.Body = $"請進行郵箱驗證來完成您註冊的最後一步,點擊下面的連結啟動您的帳號：<br><br> <a href={domainName}/Member/Em?email={ userEmail }>啟動帳號</a>"; //郵件內容
                SmtpClient MySmtp = new SmtpClient("mail.gandi.net");//設定gmail的smtp
                                                                            //System.Net.NetworkCredential(帳號,密碼)，hotmail的帳號是整的Email，不是只有@前面的
                MySmtp.Credentials = new System.Net.NetworkCredential("service@lookcamp.site", "P@sswo0rd");
                MySmtp.EnableSsl = true;//開啟ssl
                MySmtp.Send(message);
                MySmtp = null;
                message.Dispose();
            }
            catch { }
        }

        public void SendSubMail(string userEmail)
        {
            try
            {
                //  MailMessage ( FromEmailAddress, ToEmailAddress)
                MailMessage message = new MailMessage("<service@lookcamp.site>", userEmail);//MailMessage(寄信者, 收信者)
                message.IsBodyHtml = true;
                message.BodyEncoding = System.Text.Encoding.UTF8;//E-mail編碼
                message.SubjectEncoding = System.Text.Encoding.UTF8;//E-mail編碼
                message.Priority = MailPriority.Normal;//設定優先權
                message.Subject = "訂閱露過人間"; //E-mail主旨
                message.Body = "<h2>感謝您訂閱露過人間</h2><br><h4>我們將在每個月的五號寄出露營相關的最新資訊！</h4>"; //郵件內容
                SmtpClient MySmtp = new SmtpClient("mail.gandi.net");//設定gmail的smtp
                                                                            //System.Net.NetworkCredential(帳號,密碼)，hotmail的帳號是整的Email，不是只有@前面的
                MySmtp.Credentials = new System.Net.NetworkCredential("service@lookcamp.site", "P@sswo0rd");
                MySmtp.EnableSsl = true;//開啟ssl
                MySmtp.Send(message);
                MySmtp = null;
                message.Dispose();
            }
            catch { }
        }
    }
}