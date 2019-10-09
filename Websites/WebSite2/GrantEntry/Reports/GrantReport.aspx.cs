using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class GrantEntry_Reports_GrantReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           
        }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Session["Pubreport"] = "1";
        Response.Redirect("~/GrantEntry/Reports/GrantReportTypewise.aspx");

    }


    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        Session["Pubreport"] = "2";
        Response.Redirect("~/GrantEntry/Reports/GrantReportInstitutDeptWise.aspx");
    }
    protected void LinkButton3_Click(object sender, EventArgs e)
    {
        Session["Pubreport"] = "3";
        Response.Redirect("~/GrantEntry/Reports/GrantReportTypewise.aspx");
    }
    protected void LinkButton4_Click(object sender, EventArgs e)
    {
        Session["Pubreport"] = "4";
        Response.Redirect("~/GrantEntry/Reports/GrantReportInstitutDeptWise.aspx");

    }

    protected void LinkButton5_Click(object sender, EventArgs e)
    {
        Session["Pubreport"] = "5";
        Response.Redirect("~/GrantEntry/Reports/GrantReportTypewise.aspx");

    }

    protected void LinkButton6_Click(object sender, EventArgs e)
    {
        Session["Pubreport"] = "6";
        Response.Redirect("~/GrantEntry/Reports/ProjectYearWiseReceivedAmountDetails.aspx");

    }
    protected void LinkButton7_Click(object sender, EventArgs e)
    {
        Session["Pubreport"] = "7";
        Response.Redirect("~/GrantEntry/Reports/GrantReportInterInstitutionAppliedDateWise.aspx");

    }
    protected void LinkButton8_Click(object sender, EventArgs e)
    {
        Session["Pubreport"] = "8";
        Response.Redirect("~/GrantEntry/Reports/GrantReportInterInstitutionCreatedDateWise.aspx");

    }
    protected void LinkButton9_Click(object sender, EventArgs e)
    {
        Session["Pubreport"] = "9";
        Response.Redirect("~/GrantEntry/Reports/PercentageSharing.aspx");

    }
   
}