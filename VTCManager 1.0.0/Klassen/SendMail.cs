using System;
using System.IO;
using System.Net.Mail;
using System.Threading;
using System.Windows.Forms;

namespace VTCManager_1._0._0.Klassen
{
    class SendMail
    {
        Utilities utils = new Utilities();

        public void SendeMail()
        {
     
            File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\VTC_Manager\VTC_LOG_COPY.txt");
            File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\VTC_Manager\VTC_SYSTEM_LOG_COPY.txt");
            
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("vtc_diag@web.de"); 
            mail.To.Add("edv.blasius@gmail.com"); 
            mail.Subject = "Log Datei aus Client";
            mail.Body = "Hier die LOG Dateien";
        
            File.Copy(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\VTC_Manager\VTC_LOG.txt", Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\VTC_Manager\VTC_LOG_COPY.txt", true);
            File.Copy(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\VTC_Manager\VTC_SYSTEM_LOG.txt", Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\VTC_Manager\VTC_SYSTEM_LOG_COPY.txt", true);


            mail.Attachments.Add(new Attachment(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\VTC_Manager\VTC_LOG_COPY.txt"));
            mail.Attachments.Add(new Attachment(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\VTC_Manager\VTC_SYSTEM_LOG_COPY.txt"));
            mail.Attachments.Add(new Attachment(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Euro Truck Simulator 2\game.log.txt"));
            mail.Attachments.Add(new Attachment(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Euro Truck Simulator 2\mods_info.SII"));

            mail.IsBodyHtml = true;
            SmtpClient client = new SmtpClient("smtp.web.de", 587); 
            
            try
            {
                client.Credentials = new System.Net.NetworkCredential("vtc_diag@web.de", "VtcDiagnostic");
                client.EnableSsl = true; 
                client.Send(mail);

                
                MessageBox.Show("E-Mail wurde gesendet !" + Environment.NewLine + Environment.NewLine, "Daten gesendet !", MessageBoxButtons.OK, MessageBoxIcon.Information);
               //Application.Restart();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Fehler beim Senden der E-Mail" + Environment.NewLine + ex.Message);
            }

            mail.Dispose();
        }


    }
}
