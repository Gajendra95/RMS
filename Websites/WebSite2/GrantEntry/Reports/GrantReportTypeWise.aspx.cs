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

public partial class GrantEntry_Reports_GrantReportTypeWise : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string month = DateTime.Now.Month.ToString();
            TextBoxFromDate.Text = DateTime.Now.ToShortDateString();
            TextBoxToDate.Text = DateTime.Now.ToShortDateString();
          //  DropDownListMonthJA.SelectedValue = month;
            //DropDownListTopubMonth.SelectedValue = month;
            //TextBoxYearJA.Text = DateTime.Today.Year.ToString();
           // TextBoxTopubYear.Text = DateTime.Today.Year.ToString();
            if (Session["Pubreport"].ToString() == "1")
            {
                lblTitle.Text = "Project Details-ProjectTypewise (Applied Date Wise)";
            }
            else if (Session["Pubreport"].ToString() == "3")
            {
                lblTitle.Text = "Project Details-ProjectTypewise (Entry Date Wise)";
            }
            else if (Session["Pubreport"].ToString() == "3")
            {
                lblTitle.Text = "Project Details-ProjectTypewise (Sanction Date Wise)";
            }
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
        lblnote.Visible = true;
        ReportViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
        string reportpath = ConfigurationManager.AppSettings["ReportPath"].ToString();
        ReportViewer1.ServerReport.ReportServerUrl = new Uri(reportpath);

        if (Session["Pubreport"].ToString() == "1")
        {
            lblTitle.Text = "Project Details-ProjectTypewise (Applied Date Wise)";
            ReportViewer1.ServerReport.ReportPath = "/PublicationReports/ProjectDetails";

        }
        else if (Session["Pubreport"].ToString() == "3")
        {
            lblTitle.Text = "Project Details-ProjectTypewise (Entry Date Wise)";
            ReportViewer1.ServerReport.ReportPath = "/PublicationReports/ProjectDetailEntryDatewise";

        }
        else if (Session["Pubreport"].ToString() == "5")
        {
            lblTitle.Text = "Project Details-ProjectTypewise (Sanction Date Wise)";
            ReportViewer1.ServerReport.ReportPath = "/PublicationReports/ProjectDetailsSanctionDatewise";

        }
    
        string EntryType = DropDownListProjectType.SelectedValue;
        ReportParameter p41 = new ReportParameter("ProjectType", EntryType);
        string StatusType = DropDownListProjectStatusType1.SelectedValue;
        ReportParameter p4 = new ReportParameter("ProjectStatus", StatusType);
        string Fromdate = TextBoxFromDate.Text.Trim();

        //string date1 = Fromdate.ToString("MM/dd/yyyy");
        ReportParameter p22 = new ReportParameter("CreatedDate1", Fromdate);


        string Todate = TextBoxToDate.Text.Trim();
        //string date2 = Todate.ToString("MM/dd/yyyy");
        ReportParameter p3 = new ReportParameter("CreatedDate2", Todate);
        ReportParameter[] p = {p22,p3,p4,  p41 };
        ReportViewer1.ServerReport.SetParameters(p);




        ReportViewer1.ShowReportBody = true;
        ReportViewer1.ServerReport.Refresh();

    }
}