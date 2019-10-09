using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using System.IO;
using System.Configuration;

using System.Net.Mail;

using log4net;
using AjaxControlToolkit;
using System.Drawing;

public partial class Incentive_UtilizationPointView : System.Web.UI.Page
{
    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    IncentiveBusiness B = new IncentiveBusiness();
    IncentivePoint obj = new IncentivePoint();
    public string pageID = "L112";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!Session["authPage"].ToString().Contains("$" + pageID + "$"))
            {
                string unacces = "Unauthorized Acces!!! Login Again";
                Response.Redirect("~/Login.aspx?val=" + unacces);
            }
            txtStartDate.Text = DateTime.Now.ToShortDateString();
            txtEndDate.Text = DateTime.Now.ToShortDateString();
        }
    }


    protected void BtnSearch_Click(object sender, EventArgs e)
    {

        Gridview.DataBind();
        Gridview.Visible = true;
    }


    protected void BtnSendMail_Click(object sender, EventArgs e)
    {
        try
        {
            IncentiveBusiness b = new IncentiveBusiness();
            EmailDetails details = new EmailDetails();
            SendMailObject obj = new SendMailObject();
            details.FromEmail = ConfigurationManager.AppSettings["FromAddress"].ToString();
            string finance_emailid = Session["emailId"].ToString();
            int Index = ((GridViewRow)((sender as Control)).NamingContainer).RowIndex;
            string employeeid = Gridview.Rows[Index].Cells[0].Text;
            string referenceid = Gridview.Rows[Index].Cells[1].Text;
            string transctiontype = Gridview.Rows[Index].Cells[5].Text;
            Session["transctiontype"] = transctiontype;
            Session["MemberId"] = employeeid;
            Session["referenceid"] = referenceid;
            string membertype = Gridview.Rows[Index].Cells[6].Text;
            string emailid = null;
            if (membertype == "M")
            {
                emailid = b.SelectAuthorEmailId(employeeid);
            }
            else
            {
                //emailid = b.SelectStudentAuthorEmailId(employeeid);
                string id = "";
                emailid = b.SelectStudentEmailId(employeeid, id);
            }
            string hr_emaild = b.SelectHREmailId(employeeid, referenceid, transctiontype);

            PanelSendMail.Visible = true;
            if (emailid != "")
            {
                txtTo.Text = hr_emaild + ',' + emailid;
            }
            else
            {
                txtTo.Text = hr_emaild;
            }
            txtCC.Text = finance_emailid;
            foreach (GridViewRow row in Gridview.Rows)
            {
                if (row.RowIndex == Index)
                {
                    row.BackColor = ColorTranslator.FromHtml("#A1DCF2");
                }
                else
                {
                    row.BackColor = ColorTranslator.FromHtml("#FFFFFF");
                }
            }
        }


        catch (Exception ex)
        {
            log.Error(ex.StackTrace);
            log.Error(ex.Message);
            //return obj1;
        }
    }

    protected void btnSendMail_Click(object sender, EventArgs e)
    {
        if (!Page.IsValid)
        {
            return;
        }
        try
        {
            IncentiveBusiness b = new IncentiveBusiness();
            EmailDetails details = new EmailDetails();
            SendMailObject obj = new SendMailObject();
            details.FromEmail = ConfigurationManager.AppSettings["FromAddress"].ToString();
            string finance_emailid = Session["emailId"].ToString();

            string employeeid = Session["MemberId"].ToString();
            string referenceid = Session["referenceid"].ToString();
            string toemail = txtTo.Text;
            string ccmail = txtCC.Text;
            details.ToEmail = toemail.ToString();
            details.CCEmail = ccmail.ToString();
            details.Type = referenceid;
            details.Id = "";
            details.Module = Session["transctiontype"].ToString();

            string FooterText = ConfigurationManager.AppSettings["FooterText"].ToString();
            details.EmailSubject = txtSubject.Text;
            details.MsgBody = "<span style=\"font-size: 10pt; color: #3300cc; font-family: Verdana\"> <br>" +
                          "<b> '" + txtMsgContent.Text + "' <br> " +
                          "<br>" + "<br>" + "<br>" + "<br>" + FooterText +
                         " </b><br><b> </b></span>";

            bool resultv = obj.InsertIntoEmailQueue(details);
            if (resultv == true)
            {
                bool flag = b.UpdateUtilizationMailFlag(employeeid, referenceid, Session["transctiontype"].ToString());
                Gridview.DataBind();
                Gridview.Visible = true;
                PanelSendMail.Visible = false;
                txtTo.Text = "";
                txtCC.Text = "";
                txtSubject.Text = "";
                txtMsgContent.Text = "";
                string CloseWindow1 = "alert('Mail Sent successfully')";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "newWindow", CloseWindow1, true);
            }
            else
            {
                PanelSendMail.Visible = true;
                string CloseWindow1 = "alert('Problem while sending mail')";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "newWindow", CloseWindow1, true);
            }


        }


        catch (Exception ex)
        {
            log.Error(ex.StackTrace);
            log.Error(ex.Message);

        }

    }
    public void GridView_RowDataBound1(Object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string total = e.Row.Cells[2].Text;
            if (total.Contains("-"))
            {
                e.Row.Cells[2].Text = total.Substring(1, total.Length - 1);
            }
            
            e.Row.Cells[4].Attributes.Add("style", "word-break:break-all;word-wrap:break-word;");
        }
    }

}




