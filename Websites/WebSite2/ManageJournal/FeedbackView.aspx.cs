using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ManageJournal_FeedbackView : System.Web.UI.Page
{
    Journal_DataObject Da = new Journal_DataObject();
    FeedbackClass feedback = new FeedbackClass();

    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        {
            radioincentive_SelectedIndexChanged(sender, e);
        }      
    }

   
    protected void GridViewSearchPublication_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        dataBind();
        GridviewFeedback.PageIndex = e.NewPageIndex;
        GridviewFeedback.DataBind();
    }
   
    
    private void dataBind()
    {

        SqlDataSource1.SelectCommand = "select  MemberID,I.Institute_Name ,D.DeptName ,F.CreatedDate, concat(U.Prefix, ' ', U.FirstName, '  ',U.MiddleName, ' ',U.LastName) AS NAME,Q1,Q2,Q3,Q4 from FeedBack_Tracker F,User_M U,Dept_M D,Institute_M I where U.User_Id=F.MemberID and U.InstituteId=I.Institute_Id AND U.Department_Id=D.DeptId and  I.Institute_Id=D.Institute_Id AND Type='Pub' ORDER BY F.CreatedDate DESC";
        SqlDataSource1.DataBind();
        GridviewFeedback.DataBind();
          
    }


   
    protected void radioincentive_SelectedIndexChanged(object sender, EventArgs e)
    {
        EditUpdatePanel.Update();
        if (radioincentive.SelectedValue == "1")
        {
            //panel2.Style.Add("display", "true");
            //panel3.Style.Add("display", "none");

            panel2.Visible = true;
            panel3.Visible=false;
            dataBind();

        }
         if (radioincentive.SelectedValue == "2")

        {
            //panel3.Style.Add("display", "true");
            //panel2.Style.Add("display", "none");

            panel2.Visible = false;
            panel3.Visible = true;
            dataBindprj();
        }
    }

    protected void GridViewSearchPrj_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        dataBindprj();
        Gridviewprj.PageIndex = e.NewPageIndex;
        Gridviewprj.DataBind();
    }

    private void dataBindprj()
    {

        SqlDataSource2.SelectCommand = "select  MemberID,I.Institute_Name ,D.DeptName ,F.ProjectCreatedDate, concat(U.Prefix, ' ', U.FirstName, '  ',U.MiddleName, ' ',U.LastName) AS NAME,Q3,Q4,Q5,Q6 from FeedBack_Tracker F,User_M U,Dept_M D,Institute_M I where U.User_Id=F.MemberID and U.InstituteId=I.Institute_Id AND U.Department_Id=D.DeptId and  I.Institute_Id=D.Institute_Id AND Type='Prj' ORDER BY F.ProjectCreatedDate DESC";
        SqlDataSource2.DataBind();
        Gridviewprj.DataBind();
       // int count = Gridviewprj.Rows.Count;    
    }
   
}