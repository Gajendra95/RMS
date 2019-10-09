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

public partial class PublicationEntry_Reports_PublicationReportFAIRPRAISEArticleWise : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
                    
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


        ReportViewer1.ServerReport.ReportPath = "/PublicationReports/PublicationReportArticleWiseFairPraisePoints";

        string Institute = DropDownListInst.SelectedValue;
        ReportParameter p1 = new ReportParameter("Institution", Institute);

        string Dept = DropDownListDept.SelectedValue;
        ReportParameter p2 = new ReportParameter("DeptId", Dept);



        ReportParameter Membertype = new ReportParameter("MemberType", ddlMembertype.SelectedValue);


        string MuCategorization = DropDownListMuCategoryType.SelectedValue;
        ReportParameter p4 = new ReportParameter("MuCategorization", MuCategorization);

        ReportParameter Articletype = new ReportParameter("Articletype", ddlArticletype.SelectedValue);

        string Fromdate = TextBoxFromDate.Text.Trim();
        ReportParameter p5 = new ReportParameter("CreatedDate1", Fromdate);


        string Todate = TextBoxToDate.Text.Trim();
        ReportParameter p6 = new ReportParameter("CreatedDate2", Todate);

        ReportParameter[] p = { p1,p2,Membertype,p4, Articletype,p5,p6 };
        ReportViewer1.ServerReport.SetParameters(p);



        ReportViewer1.ShowReportBody = true;
        ReportViewer1.ServerReport.Refresh();
    }



}