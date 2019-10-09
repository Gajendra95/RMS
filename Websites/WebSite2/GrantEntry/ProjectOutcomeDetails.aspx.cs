 using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class GrantEntry_ProjectOutcomeDetails : System.Web.UI.Page
{
 
    protected void Page_Load(object sender, EventArgs e)
    {
         if (!IsPostBack)
         {
             GridViewSearch.Visible = true;
             //GridViewSearch.DataSourceID = "SqlDataSource1";
             //GridViewSearch.DataBind();
             dataBind();
         }
    }
    protected void GridViewSearchPub_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        dataBind();
        GridViewSearch.PageIndex = e.NewPageIndex;
        GridViewSearch.DataBind();
    }
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        ImageButton EditButton = (ImageButton)e.Row.FindControl("BtnEdit");
    }
    public void GridView2_RowCommand(Object sender, GridViewCommandEventArgs e)
    {
        string pid = null;
        string MemberName = null;
        if (e.CommandName == "Edit")
        {

            GridViewRow rowSelect = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
            int rowindex = rowSelect.RowIndex;
            HiddenField TypeOfEntry = (HiddenField)GridViewSearch.Rows[rowindex].Cells[5].FindControl("MemberId");
            MemberName = GridViewSearch.Rows[rowindex].Cells[2].Text.ToString();
            //typeEntry = TypeOfEntry.Value;

            //pid = GridViewSearch.Rows[rowindex].Cells[1].Text.Trim().ToString();
            Session["TempPid"] = TypeOfEntry.Value;

            Session["MemberName"] = MemberName.ToString();
            //Session["TempTypeEntry"] = typeEntry;//maintaining a session variable, passing it to registration page
            //popSelected(sender, e);

        }
        //else if (e.CommandName == "Sort")
        //{
        //    SqlDataSource1.SelectCommand = "select MemberId,MemberType,(Prefix+SPACE(1)+FirstName+SPACE(1)+MiddleName+SPACE(1)+LastName) as EmployeeName, CurrentBalance from Member_Incentive_Point_Summary a,User_M b where a.MemberId=b.User_Id order by CurrentBalance";
        //    GridViewSearch.DataSourceID = "SqlDataSource1";
        //    GridViewSearch.DataBind();
        //}
        //GridViewSearch.DataBind();
        //SqlDataSource1.DataBind();
        

    }
    public void edit(Object sender, GridViewEditEventArgs e)
    {

       
        GridViewSearch.EditIndex = e.NewEditIndex;

        //fnRecordExist(sender, e);
        //dataBind();

    }
    protected void ButtonSearchPubOnClick(object sender, EventArgs e)
    {

        GridViewSearch.Visible = true;
        GridViewSearch.EditIndex = -1;
        // addclik(sender, e);
        dataBind();
    }

    private void dataBind()
    {
        GridViewSearch.Visible = true;
        //SqlDataSource1.SelectParameters.Clear();
        //DateTime date1 = Convert.ToDateTime(TextBoxFromDate.Text.ToString());
        //DateTime date2 = Convert.ToDateTime(TextBoxFromDate.Text.ToString());
        //SqlDataSource1.SelectParameters.Add("CreatedDate1", date1.ToShortDateString);
        //SqlDataSource1.SelectParameters.Add("CreatedDate2", date2.ToShortDateString);
        GridViewSearch.DataSourceID = "SqlDataSource1";
        SqlDataSource1.SelectCommand = "RepselectProjectOutcomeDetails";    
        GridViewSearch.DataBind();
        SqlDataSource1.DataBind();
    }
    protected void lblMemberId_Click(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow row = (GridViewRow)lb.NamingContainer;
        int index = row.RowIndex; //gets the row index selected      
        var lblMemberId = row.FindControl("lblMemberId") as LinkButton;
        //var lblProjectUnit = row.FindControl("lblProjectUnit") as Label;
        string id = lblMemberId.Text;
        Response.Redirect("~/PublicationEntry/EmployeeDetailSearch.aspx?MemberId=" + id );
    }
    protected void lblSanction_Click(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow row = (GridViewRow)lb.NamingContainer;
        int index = row.RowIndex; //gets the row index selected      
        var lblMemberId = row.FindControl("lblMemberId") as LinkButton;
        //var lblProjectUnit = row.FindControl("lblProjectUnit") as Label;
        string id = lblMemberId.Text;
        Response.Redirect("~/Incentive/IncentivePointView.aspx?MemberId=" + id);
    }


    protected void lblPublication_Click(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow row = (GridViewRow)lb.NamingContainer;
        int index = row.RowIndex; //gets the row index selected      
        var lblMemberId = row.FindControl("lblID") as Label;
        var lblProjectUnit = row.FindControl("lblProject") as Label;
        string id = lblProjectUnit.Text+lblMemberId.Text;

        GridViewProject.DataSourceID = "SqlDataSourceProject";
        SqlDataSourceProject.SelectCommand = "select PublicationID,b.EntryName as  TypeOfEntry,TitleWorkItem  from Publication a,PublicationTypeEntry_M b where a.TypeOfEntry=b.TypeEntryId  and ProjectIDlist like '%" + id + "%'";
        GridViewProject.DataBind();
        SqlDataSourceProject.DataBind();
         UpdatePanel6.Update();
         paneloutcome.Visible = true;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "CallMyFunction", "callthis2()", true);
        return;
    }
    protected void lblPatent_Click(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow row = (GridViewRow)lb.NamingContainer;
        int index = row.RowIndex; //gets the row index selected      
        var lblMemberId = row.FindControl("lblID") as Label;
        var lblProjectUnit = row.FindControl("lblProject") as Label;
        string id = lblProjectUnit.Text + lblMemberId.Text;

        GridViewPatentOutcome.DataSourceID = "SqlDataSourcepatentoutcome";
        SqlDataSourcepatentoutcome.SelectCommand = "select ID ,Title  from Patent_Data a where  ProjectIDlist like '%" + id + "%'";
        GridViewPatentOutcome.DataBind();
        SqlDataSourcepatentoutcome.DataBind();


        UpdatePanel3.Update();
        panel1.Visible = true;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "CallMyFunction", "callthis3()", true);
        return;
    }
    protected void lblProjectOutcome_Click(object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow row = (GridViewRow)lb.NamingContainer;
        int index = row.RowIndex; //gets the row index selected      
        var lblMemberId = row.FindControl("lblID") as Label;
        var lblProjectUnit = row.FindControl("lblProject") as Label;
        string id = lblProjectUnit.Text + lblMemberId.Text;

        GridViewprojecout.DataSourceID = "SqlDataSourceprooutcome";
        SqlDataSourceprooutcome.SelectCommand = "SELECT OutcomeDate,Description  FROM ProjectOutcome where  ProjectID='" + lblMemberId.Text + "' and ProjectUnit='" + lblProjectUnit.Text + "' ";
        GridViewprojecout.DataBind();
        SqlDataSourceprooutcome.DataBind();
        UpdatePanel4.Update();
        panel2.Visible = true;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "CallMyFunction", "callthis4()", true);
        return;
    }
    protected void GridViewProject_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
        }
    }
    protected void GridViewPatent_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
        }
    }
    protected void GridViewprojecout_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
        }
    }
}