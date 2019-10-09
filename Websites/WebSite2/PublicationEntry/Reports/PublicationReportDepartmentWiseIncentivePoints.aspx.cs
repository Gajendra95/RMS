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

public partial class PublicationEntry_Reports_PublicationReportDepartmentWiseIncentivePoints : System.Web.UI.Page
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
        Response.Redirect("~/PublicationEntry/Reports/PublicationReport.aspx", false);
    }
    protected void DropDownListInstOnSelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownListDept.Items.Clear();
        DropDownListDept.Items.Add(new ListItem("ALL", "ALL", true));
        SqlDataSourceDept.SelectCommand = "select DeptId,DeptName from Dept_M where Institute_Id='" + DropDownListInst.SelectedValue + "'";
        SqlDataSourceDept.DataBind();
        DropDownListDept.DataBind();
        if (DropDownListDept.Items.Count == 2)
        {
            DropDownListDept.Items.Remove("ALL");
        }
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


        ReportViewer1.ServerReport.ReportPath = "/PublicationReports/DepartmentWiseIncentivePoints";
       

      //  string UserID = Session["UserId"].ToString();

       // ReportParameter p1 = new ReportParameter("UserID", UserID);

        string Dept = DropDownListDept.SelectedValue;
        ReportParameter p2 = new ReportParameter("DeptId", Dept);
       

        string Institute = DropDownListInst.SelectedValue;
        ReportParameter p4 = new ReportParameter("Institution", Institute);
       


        string Fromdate = TextBoxFromDate.Text.Trim();

        //string date1 = Fromdate.ToString("MM/dd/yyyy");
        ReportParameter p22 = new ReportParameter("CreatedDate1", Fromdate);


        string Todate = TextBoxToDate.Text.Trim();
        //string date2 = Todate.ToString("MM/dd/yyyy");
        ReportParameter p3 = new ReportParameter("CreatedDate2", Todate);
       // string Status = ddlStatus.SelectedValue;
       // ReportParameter pstatus = new ReportParameter("Status", Status);
        ReportParameter studentflag = new ReportParameter("StudentFlag", ddlStudentFlag.SelectedValue);
        ReportParameter[] p = { p2, p4, p22, p3, studentflag };
        ReportViewer1.ServerReport.SetParameters(p);



        ReportViewer1.ShowReportBody = true;
        ReportViewer1.ServerReport.Refresh();
    }

}