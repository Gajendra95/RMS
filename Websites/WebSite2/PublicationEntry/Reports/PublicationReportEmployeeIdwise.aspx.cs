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

public partial class PublicationReportMUEntrywise : System.Web.UI.Page
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
        string UserEprintDisplay = ConfigurationManager.AppSettings["UserEprintDisplay"];

        ReportViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
        string reportpath = ConfigurationManager.AppSettings["ReportPath"].ToString();
        ReportViewer1.ServerReport.ReportServerUrl = new Uri(reportpath);
        if (UserEprintDisplay == "Y")
        {

            ReportViewer1.ServerReport.ReportPath = "/PublicationReports/PublicationEprintEmployeeId";
        }
        else
        {
            ReportViewer1.ServerReport.ReportPath = "/PublicationReports/PublicationEmployeeId";
        }

      //  string UserID = Session["UserId"].ToString();

       // ReportParameter p1 = new ReportParameter("UserID", UserID);

        string EmplyeeId = TextBoxEmplyeeId.Text;
        if (EmplyeeId == "")
        {
            EmplyeeId = " ";
        }
        ReportParameter p21 = new ReportParameter("EmployeeId", EmplyeeId);


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