using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class GrantEntry_Reports_ProjectYearWiseReceivedAmountDetails : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string month = DateTime.Now.Month.ToString();
            TextBoxFromDate.Text = DateTime.Now.ToShortDateString();
            TextBoxToDate.Text = DateTime.Now.ToShortDateString();

            lblTitle.Text = "Year wise Statement of Grant amount received by MU";

        }
    }
    protected void backClick(object sender, EventArgs e)
    {
        Response.Redirect("~/GrantEntry/Reports/GrantReport.aspx", false);
    }
    protected void viewclick(object sender, EventArgs e)
    {
        if (!Page.IsValid)
        {
            return;
        }
        ReportViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
        string reportpath = ConfigurationManager.AppSettings["ReportPath"].ToString();
        ReportViewer1.ServerReport.ReportServerUrl = new Uri(reportpath);
        ReportViewer1.ServerReport.ReportPath = "/PublicationReports/ProjectYearWiseReceivedAmountDetails";

        string EntryType = DropDownListProjectType.SelectedValue;
        ReportParameter p41 = new ReportParameter("ProjectType", EntryType);
        string Fromdate = TextBoxFromDate.Text.Trim();

        ReportParameter p22 = new ReportParameter("CreatedDate1", Fromdate);
        string Todate = TextBoxToDate.Text.Trim();
        ReportParameter p3 = new ReportParameter("CreatedDate2", Todate);
        ReportParameter[] p = { p41,p22, p3};
        ReportViewer1.ServerReport.SetParameters(p);

        ReportViewer1.ShowReportBody = true;
        ReportViewer1.ServerReport.Refresh();

    }

}