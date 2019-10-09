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

public partial class PublicationReportInstitutewise : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

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
        ReportViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
        string reportpath = ConfigurationManager.AppSettings["ReportPath"].ToString();
        ReportViewer1.ServerReport.ReportServerUrl = new Uri(reportpath);

        ReportViewer1.ServerReport.ReportPath = "/PublicationReports/PublicationStatuswise";

      //  string UserID = Session["UserId"].ToString();

       // ReportParameter p1 = new ReportParameter("UserID", UserID);
        //DateTime Fromdate =Convert.ToDateTime( TextBoxFromDate.Text.Trim());

        //string date1 = Fromdate.ToString("MM/dd/yyyy");
        //ReportParameter p2 = new ReportParameter("CreatedDate1", date1);
        //DateTime Todate = Convert.ToDateTime(TextBoxToDate.Text.Trim());
        //string date2 = Todate.ToString("MM/dd/yyyy");
        //ReportParameter p3 = new ReportParameter("CreatedDate2", date2);


        string Fromdate = TextBoxFromDate.Text.Trim();

        //string date1 = Fromdate.ToString("MM/dd/yyyy");
        ReportParameter p2 = new ReportParameter("CreatedDate1", Fromdate);


        string Todate = TextBoxToDate.Text.Trim();
        //string date2 = Todate.ToString("MM/dd/yyyy");
        ReportParameter p3 = new ReportParameter("CreatedDate2", Todate);
        string Status = DropDownListStatus.SelectedValue;
        ReportParameter p4 = new ReportParameter("Status", Status);

        ReportParameter[] p = {p2, p3,p4};
        ReportViewer1.ServerReport.SetParameters(p);



        ReportViewer1.ShowReportBody = true;
        ReportViewer1.ServerReport.Refresh();
    }
}