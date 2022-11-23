using KMDVFramework.Config;
using System;
using System.Net.Mail;

namespace KMDVFramework.Utils
{
    public class MailUtils : Constants
    {
        public void Send()
        {
            try
            {


                SmtpClient client = new SmtpClient(MailHost)
                {
                    EnableSsl = true,
                    Port = MailPort
                };

                MailMessage newMail = new MailMessage()
                {
                    From = new MailAddress(UserNameCES, SenderName),
                    Subject = "Mail From C#",
                    IsBodyHtml = true,
                    Body = "<p><u><strong>Hello Everyone,</strong></u></p>\r\n\r\n<p>Here I Enclosed Test Report ( Some Time )</p>\r\n\r\n<p>&nbsp;</p>\r\n\r\n<p><em>Thanks &amp; Regards</em><em> </em></p>\r\n\r\n<p><em><strong>Vignesh D</strong></em></p>\r\n"
                };
                client.Credentials = new System.Net.NetworkCredential(UserNameCES, UserPasswordCES);
               // Attachment item = new Attachment(ExtentReportPath);
               // newMail.Attachments.Add(item);
                newMail.To.Add(RecipientMail);
                client.Send(newMail);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Mail Not Sent \n" + ex.ToString());
            }
        }
    }
}