using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using System.IO;
using System.Configuration;
using System.Net.Mail;
using log4net;
using AjaxControlToolkit;
public partial class PublicationReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           int role = Convert.ToInt16(Session["Role"]);
           if (role == 1 || role == 2 || role == 14)
           {
               LinkButton2.Visible = true;
               LinkButton12.Visible = true;
               LinkButton13.Visible = true;
               LinkButton6.Visible = false;
           }
           else if (role == 7)
           {
               LinkButton6.Visible = true;
               LinkButton2.Visible = false;
               LinkButton12.Visible = false;
               LinkButton13.Visible = false;
           }
           else
           {
               LinkButton2.Visible = false;
               LinkButton12.Visible = false;
               LinkButton13.Visible = false;
               LinkButton6.Visible = false;
           }
        }
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Session["Pubreport"] = "1";
        Response.Redirect("~/PublicationEntry/Reports/PublicationReportInstitutewise.aspx");

    }

    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        Session["Pubreport"] = "2";
        Response.Redirect("~/PublicationEntry/Reports/PublicationReportStatuswise.aspx");
        
    }
    protected void LinkButton3_Click(object sender, EventArgs e)
    {
        Session["Pubreport"] = "3";
        Response.Redirect("~/PublicationEntry/Reports/PublicationReportMUEntrywise.aspx");


    }
    protected void LinkButton4_Click(object sender, EventArgs e)
    {
        Session["Pubreport"] = "4";
        Response.Redirect("~/PublicationEntry/Reports/PublicationReportEmployeeIdwise.aspx");


    }

    protected void LinkButton5_Click(object sender, EventArgs e)
    {
        Session["Pubreport"] = "5";
        Response.Redirect("~/PublicationEntry/Reports/PublicationReportInstDeptwise.aspx");


    }

    protected void LinkButton6_Click(object sender, EventArgs e)
    {
        Session["Pubreport"] = "6";
        Response.Redirect("~/PublicationEntry/Reports/PublicationReportJournalEntrywise.aspx");


    }

    protected void LinkButton7_Click(object sender, EventArgs e)
    {
        Session["Pubreport"] = "7";
        Response.Redirect("~/PublicationEntry/Reports/PublicationReportContinentwise.aspx");


    }
    protected void LinkButton10_Click(object sender, EventArgs e)
    {
        Session["Pubreport"] = "10";
        Response.Redirect("~/PublicationEntry/Reports/PublicationReportStudentAuthor.aspx");


    }
    protected void LinkButton11_Click(object sender, EventArgs e)
    {
        Session["Pubreport"] = "11";
        Response.Redirect("~/PublicationEntry/Reports/PublicationReportFacultyAuthor.aspx");


    }
    protected void LinkButton1_Click1(object sender, EventArgs e)
    {
        Session["Pubreport"] = "12";
        Response.Redirect("~/PublicationEntry/Reports/InterInstitutewise.aspx");
    }
    //protected void LinkButton2_Click1(object sender, EventArgs e)
    //{
    //    Session["Pubreport"] = "13";
    //    Response.Redirect("~/PublicationEntry/Reports/Domainwise.aspx");
    //}
    protected void LinkButton2_Click1(object sender, EventArgs e)
    {
        
            Session["Pubreport"] = "13";
            Response.Redirect("~/PublicationEntry/Reports/PublicationReportDepartmentWiseIncentivePoints.aspx");
        
    }
    protected void LinkButton12_Click1(object sender, EventArgs e)
    {

        Session["Pubreport"] = "14";
        Response.Redirect("~/PublicationEntry/Reports/PublicationReportDepartmentwisePraiseFaireIncentivePoints.aspx");

    }
    protected void LinkButton13_Click1(object sender, EventArgs e)
    {

        Session["Pubreport"] = "15";
        Response.Redirect("~/PublicationEntry/Reports/PublicationReportFAIRPRAISEArticleWise.aspx");

    }
    protected void LinkButton14_Click(object sender, EventArgs e)
    {
        Session["Pubreport"] = "16";
        Response.Redirect("~/PublicationEntry/Reports/PublicationReportRollNowise.aspx");
    }
    protected void LinkButton6_Click1(object sender, EventArgs e)
    {
        Session["Pubreport"] = "17";
        Response.Redirect("~/PublicationEntry/Reports/PublicationReportInstitutionWiseHR.aspx");
    }
}