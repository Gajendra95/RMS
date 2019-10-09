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

public partial class PublicationReportContinentlwise : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string month = DateTime.Now.Month.ToString();
            DropDownListMonthJA.SelectedValue = month;
            DropDownListTopubMonth.SelectedValue = month;
            TextBoxYearJA.Text = DateTime.Today.Year.ToString();
            TextBoxTopubYear.Text = DateTime.Today.Year.ToString();
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
        ReportViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
        string reportpath = ConfigurationManager.AppSettings["ReportPath"].ToString();
        ReportViewer1.ServerReport.ReportServerUrl = new Uri(reportpath);

        ReportViewer1.ServerReport.ReportPath = "/PublicationReports/PublicationContinentwise";

      //  string UserID = Session["UserId"].ToString();

       // ReportParameter p1 = new ReportParameter("UserID", UserID);
        string MonthJA = DropDownListMonthJA.SelectedValue;
        ReportParameter p2 = new ReportParameter("PublishJAMonth", MonthJA);
        string MonthToJA = DropDownListTopubMonth.SelectedValue;
        ReportParameter p21 = new ReportParameter("PublishJAToMonth", MonthToJA);

        string YearJA = TextBoxYearJA.Text.Trim();
        ReportParameter p3 = new ReportParameter("PublishJAYear", YearJA);
        string YearToJA = TextBoxTopubYear.Text.Trim();
        ReportParameter p31 = new ReportParameter("PublishJAToYear", YearToJA);


     //   string Institute = DropDownListInst.SelectedValue;
      //  ReportParameter p4 = new ReportParameter("Institution", Institute);
        string Continent = DropDownListContinent.SelectedValue;
        ReportParameter p41 = new ReportParameter("Continent", Continent);


             string typeEntry = DropDownListTypeEntry.SelectedValue;
             ReportParameter p51 = new ReportParameter("EntryType", typeEntry);

             string Indexed = DropDownListIndexed.SelectedValue;
             ReportParameter p52 = new ReportParameter("Indexed", Indexed);

             //string Status = ddlStatus.SelectedValue;
             //ReportParameter pstatus = new ReportParameter("Status", Status);
        ReportParameter[] p = {p2,p21, p3,p31,p41,p51,p52};
        ReportViewer1.ServerReport.SetParameters(p);



        ReportViewer1.ShowReportBody = true;
        ReportViewer1.ServerReport.Refresh();
    }
}