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

public partial class Incentive_AdditionalPointAward : System.Web.UI.Page
{
    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    string PublicationYearwebConfig = ConfigurationManager.AppSettings["PublicationYear"];
    string ThresholdPubNowebConfig = ConfigurationManager.AppSettings["ThresholdPublicationNo"];

    protected void Page_Load(object sender, EventArgs e)
    {

    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (!Page.IsValid)
        {
            return;
        }
        try
        {
            IncentiveBusiness obj = new IncentiveBusiness();
            IncentivePoint j = new IncentivePoint();

            if (txtMemberId.Text.Trim() == "")
            {
                string CloseWindow1 = "alert('Please enter member id')";
                ScriptManager.RegisterStartupScript(EditUpdatePanel, EditUpdatePanel.GetType(), "alert", CloseWindow1, true);
                return;
            }
            string memberid = txtMemberId.Text.Trim();
            string year = DdlYear.SelectedValue;
            string pointsAwarded = txtPointsAwarded.Text.Trim();
            j.MemberId = memberid;
            j.Year = Convert.ToInt32(year);


            j.Points = Convert.ToDouble(pointsAwarded);
            j.TotalPoint = j.Points;
            j.ThresholdPoint = j.Points;
            string currentbalance = obj.SelectMemberCurrentBal(memberid);
             j.CurrentBalance = Convert.ToDouble(currentbalance);
            j.CurrentBalance = j.CurrentBalance + j.TotalPoint;
            j.Remarks = txtRemarks.Text;
            bool result = false;


            result = obj.AdditionalPointAward(memberid, j);

            if (result == true)
            {
                SendMail();
                string CloseWindow1 = "alert('Additional Points Saved successfully')";
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow1, true);
                btnSave.Enabled = false;
                txtPointsAwarded.Text = "";
                txtMemberId.Text = "";
                txtcurbal.Text = "";
                txtRemarks.Text = "";
                lblNote.Visible = false;
                lblNote1.Visible = false;
                DdlYear.Items.Clear();
                
            }
            else
            {
                string CloseWindow1 = "alert('problem while saving')";
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow1, true);
                btnSave.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            string CloseWindow1 = "alert('problem while saving points')";
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow1, true);
            btnSave.Enabled = true;
            log.Error(ex.Message);
            log.Error(ex.StackTrace);
        }

    }
    protected void txtMemberId_TextChanged(object sender, EventArgs e)
    {
        IncentivePoint j = new IncentivePoint();

        DdlYear.Items.Clear();
        fnRecordExist(sender, e);



    }

    private void fnRecordExist(object sender, EventArgs e)
    {
        clearData();
        IncentiveBusiness obj = new IncentiveBusiness();

        IncentivePoint j = new IncentivePoint();
        string memberid = txtMemberId.Text.Trim();
        j.MemberId = memberid;
        bool result1 = obj.CheckMemberId(j.MemberId);

        if (result1 == false)
        {
            string CloseWindow1 = "alert('Member id doesnot exist')";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "newWindow", CloseWindow1, true);
            txtMemberId.Text = "";
            return;
        }
        string memebrtype = obj.SelectMemberType(j.MemberId);
        if (memebrtype != "M")
        {
            string CloseWindow1 = "alert('Member id doesnot exist')";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "newWindow", CloseWindow1, true);
            txtMemberId.Text = "";
            return;
        }

        int currenntYear = DateTime.Now.Year;
        int year = Convert.ToInt32(PublicationYearwebConfig);

        int yeardiff = currenntYear - year;

        if (yeardiff < 0)
        {
            yeardiff = -(yeardiff);
        }
        DdlYear.Items.Add(new ListItem("Select", "0", true));
        for (int i = 0; i <= yeardiff; i++)
        {
            int yeatAppend = year + i;

            DdlYear.Items.Add(new ListItem(yeatAppend.ToString(), yeatAppend.ToString(), true));
            DdlYear.Items.Remove("2015");
            DdlYear.Items.Remove("2016");
        }

        DdlYear.DataBind();
        //DdlYear_SelectedIndexChanged1(sender, e);
    }

    private void clearData()
    {
        txtRemarks.Text = "";
        DdlYear.Items.Clear();
        txtcurbal.Text = "";
        txtPointsAwarded.Text = "";
        lblNote.Visible = false;
        lblNote1.Visible = false;
    }

    protected void DdlYear_SelectedIndexChanged1(object sender, EventArgs e)
    {
        IncentiveBusiness obj = new IncentiveBusiness();
        IncentivePoint j = new IncentivePoint();
        int pubCount = 0;
        string memberid = txtMemberId.Text.Trim();
        string year = DdlYear.SelectedValue;
        txtPointsAwarded.Text = "";
        txtRemarks.Text = "";
        j.MemberId = memberid;
        j.Year = Convert.ToInt32(year);

        // string currentbalance = obj.SelectMemberCurrentBal(memberid);
        string currentbalance = obj.SelectYearWisePoints(memberid, year);
        j.CurrentBalance = Convert.ToDouble(currentbalance);
        txtcurbal.Text = j.CurrentBalance.ToString();

        j = obj.SelectPublicationCount(memberid, year);
        pubCount = j.TotalNoOfPublications;
        txtPubcount.Text = pubCount.ToString();
        if (Convert.ToInt32(pubCount) >= Convert.ToInt32(ThresholdPubNowebConfig))
        {
            if (j.isAwarded == "Y")
            {
                txtPointsAwarded.Enabled = false;
                txtPointsAwarded.Text = j.Points.ToString();
                lblNote.Text = "Note: Point is already awarded.";
                lblNote.Visible = true;
                btnSave.Enabled = false;

            }
            else
            {

                txtPointsAwarded.Enabled = true;
                double value = (Convert.ToDouble(currentbalance) * 25) / 100;
                value = Math.Round(value, 2);
                txtPointsAwarded.Text = value.ToString();
                lblNote.Visible = false;
                lblNote.Text = "Note: Minimum System awarded point is '0.25'.";
                lblNote.Visible = true;
                btnSave.Enabled = true;
            }
        }
        else
        {
            txtPointsAwarded.Enabled = false;
            string CloseWindow1 = "alert('To enter points awarded, publication count must be greater than or euqual to 6')";
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow1, true);
            txtMemberId.Text = "";
            DdlYear.Items.Clear();
            txtcurbal.Text = "";
            txtPubcount.Text = "";
            txtPointsAwarded.Text = "";
            btnSave.Enabled = false;
            lblNote.Visible = false;
            lblNote1.Visible = false;
            return;
        }

    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtPubcount.Text = "";
        txtRemarks.Text = "";
        txtMemberId.Text = "";
        DdlYear.Items.Clear();
        txtcurbal.Text = "";
        txtPointsAwarded.Text = "";
        lblNote.Visible = false;
        lblNote1.Visible = false;
    }


    private void SendMail()
    {
        EmailDetails details = new EmailDetails();
        try
        {
            bool resultv = false;

            IncentiveBusiness b = new IncentiveBusiness();
            SendMailObject obj = new SendMailObject();

            string memberid = txtMemberId.Text.Trim();
            string emailid = b.SelectAuthorEmailId(memberid);
            string point = txtPointsAwarded.Text;
            string year = DdlYear.SelectedValue;

            details.FromEmail = ConfigurationManager.AppSettings["FromAddress"].ToString();

            details.ToEmail = emailid;
            details.Module = "APA";
            details.EmailSubject = "Additional Point Award";
            details.Type = "";
            details.Id = "";
            string FooterText = ConfigurationManager.AppSettings["FooterText"].ToString();
            details.MsgBody = "<span style=\"font-size: 10pt; color: #3300cc; font-family: Verdana\"><h4>Dear Sir/Madam,</h4> <br>" +
                                    "<p>An additional incentive point for the year '" + year + "' is awarded '" + point + "' for more than 6 publication.</p>" + "<br>" + "<br>" + FooterText +
                                        " </b><br><b> </b></span>";


            resultv = obj.InsertIntoEmailQueue(details);



            if (resultv == true)
            {

                string CloseWindow1 = "alert('Mail Sent successfully')";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "newWindow", CloseWindow1, true);

                btnSave.Enabled = false;
            }
            else
            {

                string CloseWindow1 = "alert('Problem while sending mail')";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "newWindow", CloseWindow1, true);

                btnSave.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            string CloseWindow1 = "alert('Problem while sending mail')";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "newWindow", CloseWindow1, true);
            log.Error(ex.Message);
            log.Error(ex.StackTrace);

            btnSave.Enabled = false;
        }
    }
}