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
using System.Text.RegularExpressions;

public partial class GrantEntry_ManageAgency : System.Web.UI.Page
{
    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    protected void Page_Load(object sender, EventArgs e)
    {
        popupselectNo.Visible = true;
        if (!IsPostBack)
        {
            ArrayList PageRollList = new ArrayList();
            PageRollList.Add("2");
            string userrole = Session["Role"].ToString();
            if (PageRollList.Contains(userrole))
            {

            }
            else
            {
                string unacces = "Unauthorized Acces!!! Login Again";
                Response.Redirect("~/Login.aspx?val=" + unacces);
            }
            setModalWindowAgency(sender, e);
            GridViewAgency.Visible = false;
            Butback.Visible = false;

        }
    }
    protected void setModalWindowAgency(object sender, EventArgs e)
    {
        popupselectNo.Visible = true;
        popGridagency.DataSourceID = "SqlDataSourceTextBoxGrantAgency";
        SqlDataSourceTextBoxGrantAgency.DataBind();
        popGridagency.DataBind();
        popGridagency.Visible = true;
    }
    Business Bus_Obj = new Business();
    GrantData b = new GrantData();
    protected void btn_insert(object sender, EventArgs e) //insert button
    {
       
        if (!Page.IsValid)
        {
            return;
        }

        try
        {
            b.FundingAgencyId = txtFAId.Text.Trim();
            // b.Name = txtname.Text;
            b.FundingAgencyName = txtFAname.Text;
            b.AgentType = DropDownAgentType.SelectedValue;
            b.AgencyContact = txtContactNo.Text.Trim();
            b.Pan_No = txtPanNo.Text.Trim();
            b.AgencyEmailId = txtEmailId.Text.Trim();
            b.Address = txtAddress.Text.Trim();
            b.State = txtState.Text;
            b.Country = txtCountry.Text;
            int result = Bus_Obj.SaveAgencyDetails(b);

            //lblStatus.Text = "";
            lblStatus.ForeColor = System.Drawing.Color.Green;
            GridViewAgency.Visible = false;
            popupselectNo.Visible = false;
            if (result == 1)
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Agency detail saved successfully');</script>");
                btninsert.Enabled = false;
                btnupdate.Enabled = true;
            }
            else
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Error');</script>");
                btninsert.Enabled = true;
                btnupdate.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            log.Error(ex.StackTrace);
            log.Error(ex.Message);

            log.Error("Error!!!!!!!!!!!!!!!! ");

            ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Error!!!!!!!!!!')</script>");

        }

    }
   
    protected void AgencyTextChanged(object sender, EventArgs e)
    {
        GrantData j = new GrantData();
        Business obj = new Business();
        j.FundingAgencyId = null;
        j = obj.selectExisitingAgency(txtFAId.Text.Trim());
        if (j.FundingAgencyId != null)
        {
            txtFAId.Text = j.FundingAgencyId;
            txtFAname.Text = j.FundingAgencyName;
            DropDownAgentType.SelectedValue = j.AgentType;
            txtContactNo.Text = j.AgencyContact;
            txtPanNo.Text = j.Pan_No;
            txtEmailId.Text = j.EmailId;
            txtAddress.Text = j.Address;
            txtState.Text = j.State;
            txtCountry.Text = j.Country;
            popupselectNo.Visible = false;
            btninsert.Enabled = false;
            btnupdate.Enabled = true;
            ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert(' Agency exixts!')</script>");
            txtFAId.Enabled = false;
            txtFAname.Enabled = false;
            DropDownAgentType.Enabled = false;
        }
        else
        {
            //txtFAId.Text = "";
            txtFAname.Text = "";
            DropDownAgentType.ClearSelection();
            txtContactNo.Text = "";
            txtPanNo.Text = "";
            txtEmailId.Text = "";
            txtAddress.Text = "";
            txtState.Text = "";
            txtCountry.Text = "";
            btnupdate.Enabled = false;
            btninsert.Enabled = true;
        }
    }

 protected void btnupdate_Click(object sender, EventArgs e) //update button
    {
        try
        {
            b.FundingAgencyId = txtFAId.Text.Trim();
            // b.Name = txtname.Text;
            b.FundingAgencyName = txtFAname.Text;
            b.AgentType = DropDownAgentType.SelectedValue;
            b.AgencyContact = txtContactNo.Text.Trim();
            b.Pan_No = txtPanNo.Text.Trim();
            b.AgencyEmailId = txtEmailId.Text.Trim();
            b.Address = txtAddress.Text.Trim();
            b.State = txtState.Text;
            b.Country = txtCountry.Text;

            //   b.Role_Name = DDLrolename.SelectedValue;
            int result1 = Bus_Obj.UpdateAgency(b); //Business layer
            if (result1 == 1)
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Agency detail update successfully')</script>");
                //btninsert.Enabled = false;
                popupselectNo.Visible = false;
                GridViewAgency.DataBind();
                GridViewAgency.Visible = false;
            }
        }
        catch (Exception ex)
        {
            log.Error(ex.StackTrace);
            log.Error(ex.Message);

            log.Error("Error!!!!!!!!!!!!!!!! ");

            ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Error!!!!!!!!!!')</script>");

        }
 }
    protected void Butview_Click(object sender, EventArgs e)
    {
        //log.Debug("inside Butview_Click");

        GridViewAgency.Visible = true;
        Butback.Visible = true;
    }
    protected void btn_back(object sender, EventArgs e)
    {
        Response.Redirect("ManageAgency.aspx");
    }
    protected void showPop(object sender, EventArgs e)
    {
        ModalPopupExtender1.Show();
    }

    protected void popGridagency_pageindex(object sender, GridViewPageEventArgs e)
    {
        popupselectNo.Visible = true;
        ModalPopupExtender1.Show();
        popGridagency.DataSourceID = "SqlDataSourceTextBoxGrantAgency";
        popGridagency.DataBind();
        popGridagency.Visible = true;
        popGridagency.AllowPaging = true;
        popGridagency.PageIndex = e.NewPageIndex;
    }
    protected void popSelected(Object sender, EventArgs e)
    {

        if (senderID.Value.Contains("imageBkCbtn"))
        {

            popGridagency.Visible = true;
            GridViewRow row = popGridagency.SelectedRow;

             string FundingAgencyId = row.Cells[1].Text;


             txtFAId.Text = FundingAgencyId;

           // USerIdSrch.Text = "";
             popGridagency.DataBind();
            AgencyTextChanged(sender, e);
        }
    }
 protected void AgencyIdChanged(object sender, EventArgs e)
    {
        if (agencysearch.Text.Trim() == "")
        {
            SqlDataSourceTextBoxGrantAgency.SelectCommand = "SELECT   FundingAgencyId as Id,UPPER([FundingAgencyName]) as Name FROM [ProjectFundingAgency_M]";
            popGridagency.DataBind();
            popGridagency.Visible = true;
        }

        else
        {
            SqlDataSourceTextBoxGrantAgency.SelectParameters.Clear();
            SqlDataSourceTextBoxGrantAgency.SelectParameters.Add("FundingAgencyName", agencysearch.Text);

            SqlDataSourceTextBoxGrantAgency.SelectCommand = "SELECT   FundingAgencyId as Id,UPPER([FundingAgencyName]) as Name FROM [ProjectFundingAgency_M] where FundingAgencyName like '%' + @FundingAgencyName + '%'";
            popGridagency.DataBind();
            popGridagency.Visible = true;
            popupselectNo.Visible = true;
        }

        ModalPopupExtender1.Show();
    }


 protected void Button2_Click(object sender, EventArgs e)
 {
     txtFAId.Text = "";
     txtFAname.Text = "";
     DropDownAgentType.ClearSelection();
     txtContactNo.Text = "";
     txtPanNo.Text = "";
     txtEmailId.Text = "";
     txtAddress.Text = "";
     txtState.Text = "";
     txtCountry.Text = "";
     txtFAId.Enabled = true;
     txtFAname.Enabled = true;
     DropDownAgentType.Enabled = true;
     btninsert.Enabled = true;
     btnupdate.Enabled = false;
 }
}