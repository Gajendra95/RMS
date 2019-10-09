using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;

public partial class UserManagement_InstDeptAutoApprove : System.Web.UI.Page
{
    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

 
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void DDLinstitutename_SelectedIndexChanged(object sender, EventArgs e)
    {
        DDLdeptname.Items.Clear();
        DDLdeptname.Items.Add(new ListItem("--Select--", "", true));
        SqlDataSource1.SelectCommand = "SELECT DISTINCT [DeptName], [DeptId] FROM [Dept_M] where Institute_Id = '" + DDLinstitutename.SelectedValue + "'";
        SqlDataSource1.DataBind();
        DDLdeptname.DataBind();
    }

    protected void btn_insert(object sender, EventArgs e) //insert button
    {
        User b = new User();
        User_Mangement bl = new User_Mangement();

        if (!Page.IsValid)
        {
            return;
        }

        try
        {
            
            b.Department_Id = DDLdeptname.SelectedValue;
             b.InstituteId = DDLinstitutename.SelectedValue;
             b.AutoApproved = DropDownListAutoApprovee.SelectedValue;

             b.Remarks = TextBoxRemarks.Text.Trim();
             b.UpdatedBy = Session["UserId"].ToString();

             int retVal = bl.InsertDepartmentInstituteAutoAppove(b);
             if (retVal == 1)
             {
                 ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Data saved successfully');window.location='" + Request.ApplicationPath + "/UserManagement/InstDeptAutoApprove.aspx';</script>");

                 btninsert.Enabled = false;
                 btninsert.Enabled = true;
                 b = null;
                 bl = null;
             }
             else
             {
                 ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Error!!!!!!!!!!')</script>");
             }
        }
        catch (Exception ex)
        {
            log.Error(ex.StackTrace);
            log.Error(ex.Message);

            log.Error("Error!!!!!!!!!!!!!!!! ");

            ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Error!!!!!!!!!!')</script>");

        }

    }
}