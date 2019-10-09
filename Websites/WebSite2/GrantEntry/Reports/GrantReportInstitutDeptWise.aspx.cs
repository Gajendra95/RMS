using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class GrantEntry_Reports_GrantReportInstitutDeptWise : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            TextBoxFromDate.Text = DateTime.Now.ToShortDateString();
            TextBoxToDate.Text = DateTime.Now.ToShortDateString();
            if (Session["Pubreport"].ToString() == "2")
            {
                lblTitle.Text = "Project Details-Institute Department Wise (Applied Date Wise)";
            }
            else if (Session["Pubreport"].ToString() == "4")
            {
                lblTitle.Text = "Project Details-Institute Department Wise (Entry Date Wise)";
            }
        }

    }
    protected void backClick(object sender, EventArgs e)
    {
        Response.Redirect("~/GrantEntry/Reports//GrantReport.aspx", false);
    }
    protected void DropDownListInstOnSelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownListDept.Items.Clear();
        DropDownListDept.Items.Add(new ListItem("ALL", "ALL", true));
        SqlDataSourceDept.SelectCommand = "select DeptId,DeptName from Dept_M where Institute_Id='" + DropDownListInst.SelectedValue + "'";
        SqlDataSourceDept.DataBind();
        DropDownListDept.DataBind();
        if (DropDownListDept.Items.Count == 2)
        {
            DropDownListDept.Items.Remove("ALL");
        }
    }

    protected void viewclick(object sender, EventArgs e)
    {
        if (!Page.IsValid)
        {
            return;
        }
        lblnote.Visible = true;
        ReportViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
        string reportpath = ConfigurationManager.AppSettings["ReportPath"].ToString();
        ReportViewer1.ServerReport.ReportServerUrl = new Uri(reportpath);

        if (Session["Pubreport"].ToString() == "2")
        {
            lblTitle.Text = "Project Details-Institute Department Wise (Applied Date Wise)";
            ReportViewer1.ServerReport.ReportPath = "/PublicationReports/ProjectInstDeptwise";

        }
        else if (Session["Pubreport"].ToString() == "4")
        {
            lblTitle.Text = "Project Details-Institute Department Wise (Entry Date Wise)";
            ReportViewer1.ServerReport.ReportPath = "/PublicationReports/ProjectInstDeptEntryDatewise";

        }

        string Dept = DropDownListDept.SelectedValue;
        ReportParameter p2 = new ReportParameter("DeptId", Dept);

        string Institute = DropDownListInst.SelectedValue;
        ReportParameter p4 = new ReportParameter("Institution", Institute);
        string EntryType = DropDownListProjectType.SelectedValue;
        ReportParameter p41 = new ReportParameter("ProjectType", EntryType);

        string Fromdate = TextBoxFromDate.Text.Trim();
        ReportParameter p22 = new ReportParameter("AppliedDate1", Fromdate);

        string Todate = TextBoxToDate.Text.Trim();
        ReportParameter p3 = new ReportParameter("AppliedDate2", Todate);
        string StatusType = DropDownListProjectStatusType1.SelectedValue;
        ReportParameter status = new ReportParameter("ProjectStatus", StatusType);
        ReportParameter[] p = { p2, p4, p41, p22, p3, status };
        ReportViewer1.ServerReport.SetParameters(p);

        ReportViewer1.ShowReportBody = true;
        ReportViewer1.ServerReport.Refresh();
    }
}