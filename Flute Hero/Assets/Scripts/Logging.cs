using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Net.Mail;
using System.Net;
public class Logging : MonoBehaviour
{

    
    string fullFilePath;
    void OnEnable() {
        Application.logMessageReceived+= Log;
    }
    void OnDisable() {
        Application.logMessageReceived-= Log;
    }
    // Start is called before the first frame update
    void Start()
    {
        fullFilePath = Application.streamingAssetsPath + "/LogFile.txt";
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Log(string logString, string stackTrace, LogType type){
        TextWriter tw = new StreamWriter(fullFilePath, true);
        tw.WriteLine("[" + System.DateTime.Now + "] " + logString);
        
        tw.Close();
    }
    private void OnApplicationQuit() {
        SendLogEmail();
    }

    private void SendLogEmail(){
        string SendMailFrom = "flutehero21@gmail.com";
        string SendMailTo = "flutehero21@gmail.com";
        string SendMailSubject = "Log File From: " + System.DateTime.Now;
        string SendMailBody = "Logging email, see attached text file.";

        
        
        SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com",587);
        SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
        MailMessage email = new MailMessage();
    
        // START
        email.From = new MailAddress(SendMailFrom);
        email.To.Add(SendMailTo);
        email.CC.Add(SendMailFrom);
        email.Subject = SendMailSubject;
        email.Body = SendMailBody;

        //ATTACHMENT
        System.Net.Mail.Attachment attachment;
        attachment = new System.Net.Mail.Attachment(fullFilePath);
        email.Attachments.Add(attachment);

        //END
        SmtpServer.Timeout = 5000;
        SmtpServer.EnableSsl = true;
        SmtpServer.UseDefaultCredentials = false;
        SmtpServer.Credentials = new NetworkCredential(SendMailFrom, "squvmdumukhdejhx");
        SmtpServer.Send(email);

            
        
        

    }


}
