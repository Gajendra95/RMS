using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Incentive_IncentivePointAdjustment : System.Web.UI.Page
{
    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    IncentiveBusiness B = new IncentiveBusiness();
    IncentivePoint obj = new IncentivePoint();
    public string pageID = "L108";

    protected void Page_Load(object sender, EventArgs e)
    {
        Panel1.Visible = true;
        if (!IsPostBack)
        {
            if (!Session["authPage"].ToString().Contains("$" + pageID + "$"))
            {
                string unacces = "Unauthorized Acces!!! Login Again";
                Response.Redirect("~/Login.aspx?val=" + unacces);
            }
            setModalWindow(sender, e);
        }
    }

    private void setModalWindow(object sender, EventArgs e)
    {
        Panel1.Visible = true;
        popGridSearch.DataSourceID = "SqlDataSourceMember";
        SqlDataSourceMember.DataBind();
        popGridSearch.DataBind();
        popGridSearch.Visible = true;
    }


    protected void showPop(object sender, EventArgs e)
    {
        ModalPopupExtender2.Show();
    }


    protected void popSelected(Object sender, EventArgs e)
    {

        txtboxMemberId.Text = "";
        txtcurbal.Text = "";
        txtadjustment.Text = "";
        txtRemarks.Text = "";
        btnUpdate.Enabled = true;
        popGridSearch.Visible = true;
        GridViewRow row = popGridSearch.SelectedRow;
        string memberid = row.Cells[1].Text;
        txtboxMemberId.Text = row.Cells[1].Text;
        IncentiveBusiness B = new IncentiveBusiness();
        IncentivePoint obj = new IncentivePoint();
        obj = B.SelectMemberCurBalance(memberid);
        txtcurbal.Text = obj.CurrentBalance.ToString();

    }




    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        if (!Page.IsValid)
        {
            return;
        }
        try
        {

            obj.MemberId = txtboxMemberId.Text.Trim();
            obj.CurrentBalance = Convert.ToDouble(txtcurbal.Text.Trim());
            obj.TotalPoint = Convert.ToDouble(txtadjustment.Text.Trim());
            obj.Remarks = txtRemarks.Text.Trim();
            obj.CurrentBalance = obj.CurrentBalance + Convert.ToDouble(obj.TotalPoint);
            obj.TransactionType = "ADJ";
            if (obj.TotalPoint > 0)
            {
                obj.NumberType = "Added";
            }
            else
            {
                obj.NumberType = "Removed";
            }
            bool result1 = B.UpdateCurBal(obj); //Business layer
            if (result1 == true)
            {
                string CloseWindow1 = "alert('Incentive Point Saved successfully')";
                //ScriptManager.RegisterStartupScript(EditUpdatePanel, EditUpdatePanel.GetType(), "alert", CloseWindow1, true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "newWindow", CloseWindow1, true);
                btnUpdate.Enabled = false;
                EmailDetails details = new EmailDetails();
                details = SendMail(txtboxMemberId.Text, txtcurbal.Text, txtadjustment.Text, txtRemarks.Text, obj.CurrentBalance, obj.NumberType);             
                SendMailObject obj1 = new SendMailObject();
                bool resultv = obj1.InsertIntoEmailQueue(details);

            }
            else
            {
                string CloseWindow1 = "alert('Incentive Point Saved successfully')";
                ScriptManager.RegisterStartupScript(EditUpdatePanel, EditUpdatePanel.GetType(), "alert", CloseWindow1, true);
                btnUpdate.Enabled = false;
            }


        }
        catch (Exception ex)
        {
            log.Error(ex.StackTrace);
            log.Error(ex.Message);
            ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Error!!!!!!!!!!')</script>");

        }
    }
    protected void popGridIncenAdjust_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        popGridSearch.Visible = true;
        GridViewRow row = popGridSearch.SelectedRow;
        string memberid = row.Cells[1].Text;
        txtboxMemberId.Text = memberid;
        popGridSearch.DataBind();

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (txtmidSearch.Text.Trim() == "")
        {
            //SqlDataSourceMember.SelectCommand = "SELECT   MemberId, FirstName+' '+MiddleName+' '+LastName as MemberName from Member_Incentive_Point_Summary , User_M  where User_M.User_Id=Member_Incentive_Point_Summary.MemberId";
            SqlDataSourceMember.SelectCommand = " SELECT distinct top 10  MemberId,AuthorName as MemberName from Member_Incentive_Point_Summary , Publishcation_Author where Publishcation_Author.EmployeeCode=Member_Incentive_Point_Summary.MemberId and MemberType!='N' ";
            popGridSearch.DataSourceID = "SqlDataSourceMember";
            popGridSearch.DataBind();
            popGridSearch.Visible = true;
        }

        else
        {
            SqlDataSourceMember.SelectParameters.Clear();
            SqlDataSourceMember.SelectParameters.Add("MemberId", txtmidSearch.Text);

            //    SqlDataSourceMember.SelectCommand = "SELECT   MemberId, FirstName+' '+MiddleName+' '+LastName as MemberName from Member_Incentive_Point_Summary , User_M  where User_M.User_Id=Member_Incentive_Point_Summary.MemberId and MemberId like '%" + txtmidSearch.Text + "%'";
            SqlDataSourceMember.SelectCommand = " SELECT distinct top 1  MemberId,AuthorName as MemberName from Member_Incentive_Point_Summary , Publishcation_Author where Publishcation_Author.EmployeeCode=Member_Incentive_Point_Summary.MemberId and MemberType!='N'  and MemberId =@MemberId";
            popGridSearch.DataSourceID = "SqlDataSourceMember";
            popGridSearch.DataBind();
            popGridSearch.Visible = true;

        }

        ModalPopupExtender2.Show();

    }
    protected void btnexit_Click(object sender, EventArgs e)
    {
        txtmidSearch.Text = "";
        popGridSearch.DataBind();
    }

    private EmailDetails SendMail(string Memberid, string currentBalance, string adjustmentpoints, string remarks, double newCurrentBalance ,string NumberType)
    {
        log.Debug("Memberwise point adjustment: Inside Send Mail function of Memberid: " + txtboxMemberId.Text);
        EmailDetails details = new EmailDetails();
        details.EmailSubject = "Incentive Point Adjustment";
        details.Module = "ADJ";
        try
        {
            details.FromEmail = ConfigurationManager.AppSettings["FromAddress"].ToString();
            Business bus = new Business();         
            int result;
            string AuthorEmailID = bus.getauthoremailID(Memberid);

            details.ToEmail = AuthorEmailID;

            details.EmailSubject = "Incentive Point Adjustment";

            string FooterText = ConfigurationManager.AppSettings["FooterText"].ToString();
           
         
                details.MsgBody = "<span style=\"font-size: 10pt; color: #3300cc; font-family: Verdana\"><h4>Dear Sir/Madam,</h4> <br>" +
                     "<b> Incentive points with the rating  : " + adjustmentpoints + "  " + NumberType + ". <br> " +
                     "<br>" +
                    "Member Id : " + Memberid + "<br>" +
                    "Adjustment Points : " + adjustmentpoints + "<br>" +
                    "Current Balance : " + newCurrentBalance + "<br>" +
                    "Remarks : " + txtRemarks.Text + "<br>" + "<br>" + "<br>" + "<br>" + "<br>" + FooterText +             
                    " </b><br><b> </b></span>";

            
           
            return details;
        }

        catch (Exception ex)
        {
            log.Error(ex.StackTrace);
            log.Error(ex.Message);
            return details;
        }
    }
}
