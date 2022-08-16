
using System.IO;
using System.Net;
using System.Net.Mail;


namespace Email_Testing{

    class Program{

        static void Main(string[] args){
            String SendMailFrom = "flutehero21@gmail.com";
            String SendMailTo = "flutehero21@gmail.com";
            String SendMailSubject = "Email Subject";
            String SendMailBody = "This is a test email.";

            try
            {
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com",587);
                SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
                MailMessage email = new MailMessage();
                // START
                email.From = new MailAddress(SendMailFrom);
                email.To.Add(SendMailTo);
                email.CC.Add(SendMailFrom);
                email.Subject = SendMailSubject;
                email.Body = SendMailBody;
                //END
                SmtpServer.Timeout = 5000;
                SmtpServer.EnableSsl = true;
                SmtpServer.UseDefaultCredentials = false;
                SmtpServer.Credentials = new NetworkCredential(SendMailFrom, "squvmdumukhdejhx");
                SmtpServer.Send(email);

                Console.WriteLine("Email Successfully Sent");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.ReadKey();
            }
        }
    }
}