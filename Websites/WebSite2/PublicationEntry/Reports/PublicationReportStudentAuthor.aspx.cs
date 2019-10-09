using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using Microsoft.Reporting.WebForms;

public partial class PublicationEntry_PublicationReportStudentAuthor : System.Web.UI.Page
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

        ReportViewer1.ServerReport.ReportPath = "/PublicationReports/StudentAuthorPublication";

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
        string Indexed = DropDownListIndexed.SelectedValue;
        ReportParameter p41 = new ReportParameter("Index", Indexed);

        ReportParameter[] p = { p2, p21, p3, p31, p41};
        ReportViewer1.ServerReport.SetParameters(p);



        ReportViewer1.ShowReportBody = true;
        ReportViewer1.ServerReport.Refresh();
    }
}