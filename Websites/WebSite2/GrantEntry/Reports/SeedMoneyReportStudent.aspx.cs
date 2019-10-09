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

public partial class GrantEntry_Reports_SeedMoneyReportStudent : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            TextBoxFromDate.Text = DateTime.Now.ToShortDateString();
            TextBoxToDate.Text = DateTime.Now.ToShortDateString();


            // DropDownListInst.DropDownContainerMaxHeight ="100px";
        }

    }
    protected void backClick(object sender, EventArgs e)
    {
        Response.Redirect("~/GrantEntry/Reports/SeedMoneyReport.aspx", false);
    }
    protected void DropDownListInstOnSelectedIndexChanged(object sender, EventArgs e)
    {
        ddlStatus.Items.Clear();
        ddlStatus.Items.Add(new ListItem("ALL", "ALL", true));
        SqlDataSource1.SelectCommand = "SELECT * FROM Status_Seedmoney_M where StatusId='SUB' or  StatusId='NEW' or  StatusId='APP' or StatusId='REJ' or  StatusId='REW' or  StatusId='REV'";
        SqlDataSource1.DataBind();
        ddlStatus.DataBind();
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


        ReportViewer1.ServerReport.ReportPath = "/PublicationReports/SeedMoneyDetailsStudent";


        //  string UserID = Session["UserId"].ToString();

        // ReportParameter p1 = new ReportParameter("UserID", UserID);




        string Fromdate = TextBoxFromDate.Text.Trim();

        //string date1 = Fromdate.ToString("MM/dd/yyyy");
        ReportParameter p22 = new ReportParameter("CreatedDate1", Fromdate);


        string Todate = TextBoxToDate.Text.Trim();
        //string date2 = Todate.ToString("MM/dd/yyyy");
        ReportParameter p3 = new ReportParameter("CreatedDate2", Todate);
        // string Status = ddlStatus.SelectedValue;
        // ReportParameter pstatus = new ReportParameter("Status", Status);
        ReportParameter status = new ReportParameter("Status", ddlStatus.SelectedValue);
        ReportParameter[] p = { p22, p3, status };
        ReportViewer1.ServerReport.SetParameters(p);



        ReportViewer1.ShowReportBody = true;
        ReportViewer1.ServerReport.Refresh();
    }

}