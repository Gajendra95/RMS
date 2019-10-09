using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using log4net;
using AjaxControlToolkit;
using System.Drawing;
using Microsoft.Reporting.WebForms;
public partial class PublicationEntry_Reports_InterInstitutewise : System.Web.UI.Page
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
        Response.Redirect("~/PublicationEntry/Reports/PublicationReport.aspx", false);
    }

    protected void viewclick(object sender, EventArgs e)
    {
        if (!Page.IsValid)
        {
            return;
        }
        lblnote.Visible = true;
        string UserEprintDisplay = ConfigurationManager.AppSettings["UserEprintDisplay"];


        ReportViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
        string reportpath = ConfigurationManager.AppSettings["ReportPath"].ToString();
        ReportViewer1.ServerReport.ReportServerUrl = new Uri(reportpath);


        ReportViewer1.ServerReport.ReportPath = "/PublicationReports/InterInstitutionwise";
      

        string Fromdate = TextBoxFromDate.Text.Trim();

        //string date1 = Fromdate.ToString("MM/dd/yyyy");
        ReportParameter p22 = new ReportParameter("CreatedDate1", Fromdate);


        string Todate = TextBoxToDate.Text.Trim();
        //string date2 = Todate.ToString("MM/dd/yyyy");
        ReportParameter p3 = new ReportParameter("CreatedDate2", Todate);
       
        ReportParameter[] p = {  p22, p3 };
        ReportViewer1.ServerReport.SetParameters(p);



        ReportViewer1.ShowReportBody = true;
        ReportViewer1.ServerReport.Refresh();
    }
}