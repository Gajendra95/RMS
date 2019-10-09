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

public partial class GrantEntry_Reports_SeedMoneyReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           
        }
      
    }
     
    protected void LinkButtonstudent_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/GrantEntry/Reports/SeedMoneyReportStudent.aspx");
    }

    protected void LinkButtonFaculty_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/GrantEntry/Reports/SeedMoneyReportFaculty.aspx");
    }

   
}