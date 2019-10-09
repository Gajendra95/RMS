    using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class GrantEntry_Reports_GrantReportInterInstitutionCreatedDateWise : System.Web.UI.Page
{
 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            TextBoxFromDate.Text = DateTime.Now.ToShortDateString();
            TextBoxToDate.Text = DateTime.Now.ToShortDateString();
            
        }

    }
    protected void backClick(object sender, EventArgs e)
    {
        Response.Redirect("~/GrantEntry/Reports//GrantReport.aspx", false);
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


        ReportViewer1.ServerReport.ReportPath = "/PublicationReports/ProjectInterInstitutionCreatedDatewise";

        string StatusType = DropDownListProjectStatusType1.SelectedValue;
        ReportParameter status = new ReportParameter("ProjectStatus", StatusType);

        string Fromdate = TextBoxFromDate.Text.Trim();
        ReportParameter p22 = new ReportParameter("CreatedDate1", Fromdate);

        string Todate = TextBoxToDate.Text.Trim();
        ReportParameter p3 = new ReportParameter("CreatedDate2", Todate);
      
        ReportParameter[] p = { status, p22, p3};
        ReportViewer1.ServerReport.SetParameters(p);

        ReportViewer1.ShowReportBody = true;
        ReportViewer1.ServerReport.Refresh();
    }


}