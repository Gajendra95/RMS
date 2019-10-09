using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using System.Data;
using System.Net.Mail;
using System.Configuration;
[assembly: log4net.Config.XmlConfigurator(Watch = true)]
public partial class Admin_BatchJobMailSender : System.Web.UI.Page
{
    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SendEmail();
        }
    }

    private void SendEmail()
    {
        log.Info("BatchJobmailSender : Inside SendEmail function");
        string toaddress = "";
        string cc = "";
        string bcc = "";
        string subject = "";
        string id = "";
        SendMailObject obj = new SendMailObject();
        DataSet ds = new DataSet();
        ds = obj.SelectEMailQueueDetails();
        try
        {
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                try
                {
                    MailMessage Msg = new MailMessage();
                    System.Net.Mail.SmtpClient spcl = new System.Net.Mail.SmtpClient();
                    string password = ConfigurationManager.AppSettings["MailPassword"].ToString();
                    string from = ConfigurationManager.AppSettings["FromAddress"].ToString();
                    string footertext = ConfigurationManager.AppSettings["FooterText"].ToString();

                    subject = ds.Tables[0].Rows[i]["Subject"].ToString();

                    string to = ds.Tables[0].Rows[i]["ToEmail"].ToString();
                    cc = ds.Tables[0].Rows[i]["CC"].ToString();
                    bcc = ds.Tables[0].Rows[i]["BCC"].ToString();
                    string body = ds.Tables[0].Rows[i]["MsgBody"].ToString();
                    id = ds.Tables[0].Rows[i]["Id"].ToString();
                    Session["Module"] = ds.Tables[0].Rows[i]["Module"].ToString();

                    Msg.Subject = subject;
                    if (cc != "")
                    {  
                        string[] ccemaillist = cc.Split(',');
                        for (int j = 0; j <= ccemaillist.GetUpperBound(0); j++)
                        {
                            Msg.CC.Add(ccemaillist[j]);
                        }
                    }
                    if (bcc != "")
                    {
                        string[] bccemaillist = bcc.Split(',');
                        for (int j = 0; j <= bccemaillist.GetUpperBound(0); j++)
                        {
                            Msg.Bcc.Add(bccemaillist[j]);
                        }
                    }
                    Msg.Priority = MailPriority.Normal;
                    Msg.IsBodyHtml = true;
                    Msg.From = new MailAddress(from);
                    Msg.Body = body;
                    toaddress = ds.Tables[0].Rows[i]["ToEmail"].ToString();
                    if (toaddress != null)
                    {
                        string[] toaddress_value = toaddress.Split(',');
                        for (int j = 0; j <= toaddress_value.GetUpperBound(0); j++)
                        {
                            Msg.To.Add(toaddress_value[j]);
                        }
                    }
                    spcl.Host = ConfigurationManager.AppSettings["MailHost"].ToString();
                    spcl.Port = Convert.ToInt16(ConfigurationManager.AppSettings["SMTPPort"]);
                    spcl.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSSL"]);
                    spcl.Credentials = new System.Net.NetworkCredential(from, password);


                    spcl.Send(Msg);
                    bool result = obj.UpdateEmailSendFlag(id,Session["Module"], subject);
                    if (result == true)
                    {

                        log.Info("BatchJobmailSender:Email is sent to sucessfully for the user (To adress) :" + toaddress + " subject: " + subject + " Module: " + Session["Module"] + " Id: " + id);
                        if (cc != "")
                        {
                            log.Info("BatchJobmailSender:Email is sent to sucessfully for the user (CC) : '" + cc + "' ");
                        }
                        if (bcc != "")
                        {
                            log.Info(" BatchJobmailSender:Email is sent to sucessfully for the user (BCC) : '" + bcc + "' ");
                        }
                        ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Email is sent  sucessfully')</script>");

                    }

                }
                catch (Exception ex)
                {
                    //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Problem in sending email')</script>");
                    log.Error(ex.StackTrace);
                    log.Error(ex.Message);
                    log.Error("BatchJobmailSender:Email Send Failed  : '" + ex + "' ");
                    log.Error("BatchJobmailSender:Email Failed for the user :" + toaddress + " subject: " + subject + " Module: " + Session["Module"] + " Id: " + id);
                    log.Error("BatchJobmailSender:Email Failed for the user :" + cc + " subject: " + subject + " Module: " + Session["Module"] + " Id: " + id);
                    log.Error("BatchJobmailSender:Email Failed for the user :" + bcc + " subject: " + subject + " Module: " + Session["Module"] + " Id: " + id);
                    if (cc != "")
                    {
                        log.Info("BatchJobmailSender:Email Failed for the user (CC) : '" + cc + "' ");
                    }
                    if (bcc != "")
                    {
                        log.Info("BatchJobmailSender:Email Failed for the user (BCC) : '" + bcc + "' ");
                    }

                }


            }
        }
        catch (Exception e1)
        {
            log.Error(e1.StackTrace);
            log.Error(e1.Message);
            log.Error("BatchJobmailSender:Email Send Failed  : '" + e1 + "' ");
            ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Problem in sending email')</script>");


        }
        Response.Write("<script language='javascript'> {window.open('', '_self', ''); window.close();}</script>");
    }
}