using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
/// <summary>
/// 作者：刘泽华
/// 时间：2018年5月29日11点51分
/// </summary>
namespace KilyCore.Extension.EmailExtension
{
    /// <summary>
    /// 邮件发送
    /// </summary>
    public class EmailExSend
    {
        /// <summary>
        /// 邮件发送
        /// </summary>
        /// <param name="receive">收件人</param>
        /// <param name="title">标题</param>
        /// <param name="body">内容</param>
        public static bool SendEmail(string receive,string title,string content)
        {
            try
            {
                SmtpClient Client = new SmtpClient();
                Client.DeliveryMethod = SmtpDeliveryMethod.Network;
                Client.Host = "smtp.163.com";
                Client.UseDefaultCredentials = true;
                Client.Credentials = new NetworkCredential("cfdacx@163.com", "cfdacx2016");
                MailAddress From = new MailAddress("cfdacx@163.com", "食品药品监管信息查询平台");
                MailAddress To = new MailAddress(receive);
                MailMessage Msg = new MailMessage(From, To);
                Msg.Subject = title;
                Msg.Body = content;
                Msg.BodyEncoding = Encoding.UTF8;
                Msg.IsBodyHtml = true;
                Msg.Priority = MailPriority.Normal;
                Client.Send(Msg);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
