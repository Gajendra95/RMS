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

public partial class PublicationEntry_Reports_PublicationReportRollNowise : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            

        }
    }
    protected void backClick(object sender, EventArgs e)
    {
        Response.Redirect("~/PublicationEntry/Reports/PublicationReport.aspx", false);
    }
    protected void viewclick(object sender, EventArgs e)
    {      
        ReportViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
        string reportpath = ConfigurationManager.AppSettings["ReportPath"].ToString();
        ReportViewer1.ServerReport.ReportServerUrl = new Uri(reportpath);

        ReportViewer1.ServerReport.ReportPath = "/PublicationReports/PublicationRollNumber";
        

      //  string UserID = Session["UserId"].ToString();

       // ReportParameter p1 = new ReportParameter("UserID", UserID);

        string RollNo = TextBoxRollNo.Text;
        if (RollNo == "")
        {
            RollNo = " ";
        }
        ReportParameter p21 = new ReportParameter("RollNo", RollNo);


     //   string Institute = DropDownListInst.SelectedValue;
      //  ReportParameter p4 = new ReportParameter("Institution", Institute);
        string EntryType = DropDownListTypeEntry.SelectedValue;
        ReportParameter p41 = new ReportParameter("EntryType", EntryType);

        //string Status = ddlStatus.SelectedValue;
        //ReportParameter pstatus = new ReportParameter("Status", Status);

        ReportParameter[] p = { p21, p41};
        ReportViewer1.ServerReport.SetParameters(p);



        ReportViewer1.ShowReportBody = true;
        ReportViewer1.ServerReport.Refresh();
    }

}