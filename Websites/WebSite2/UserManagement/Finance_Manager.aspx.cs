using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;

public partial class UserManagement_Finance_Manager : System.Web.UI.Page
{
    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["RMSConnectionString"].ToString());
    public string pageID = "L90";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //if (!Session["authPage"].ToString().Contains("$" + pageID + "$"))
            //{
            //    string unacces = "Unauthorized Acces!!! Login Again";
            //    Response.Redirect("~/UnAuthor_Page.aspx?val=" + unacces);
            //}
        }
    }
    protected void Butselect_Click(object sender, EventArgs e)
    {
        if (!Page.IsValid)
        {
            return;
        }
        Butsave.Enabled = true;
        string username = DropDownList1.SelectedValue;
        con.Open();
        SqlDataAdapter adapter = new SqlDataAdapter("select Institute_Id, Institute_Name from Institute_M", con);
  
        DataTable dt = new DataTable();
        adapter.Fill(dt);


        con.Close();
        GridViewBU.DataSource = dt;
        GridViewBU.DataBind();
    }

    protected void GridViewBU_RowDataBound(object sender, GridViewRowEventArgs e)
    {
     //   GridViewDetails.Visible = true;
        GridViewBU.Visible = true;
        Grant_DataObject obj = new Grant_DataObject();
        string Uid = null;
        string username = DropDownList1.SelectedValue;
        Uid = obj.selectUIdDropdown(username);
        con.Open();

   
        SqlCommand cmd = new SqlCommand("select distinct Institute_Id from User_Institution_Map where User_Id=@ID", con);
        cmd.Parameters.AddWithValue("@ID", username);
        SqlDataReader rdr = cmd.ExecuteReader();
        while (rdr.Read())
        {

            string busunit = (string)rdr["Institute_Id".Trim()];
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var checkid = e.Row.FindControl("CheckBoxMark1") as CheckBox;
                string bu = (DataBinder.Eval(e.Row.DataItem, "Institute_Id".Trim()).ToString());
                if (bu.Trim() == busunit.Trim())
                {
                    checkid.Checked = true;
                }
            }
        }
        con.Close();

    }
    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        // CheckBox checkbox = sender as CheckBox;
        GridViewRow currentRow = (GridViewRow)((CheckBox)sender).Parent.Parent;

        Session["row" + currentRow.RowIndex] = "Y";

    }
    protected void DropDownList1OnSelectedIndexChanged(object sender, EventArgs e)
    {
      //  GridViewDetails.Visible = false;
        GridViewBU.Visible = false;
        Butsave.Enabled = false;
    }
    protected void Butsave_Click(object sender, EventArgs e)
    {
        if (!Page.IsValid)
        {
            return;
        }
        Grant_DataObject obj = new Grant_DataObject();
        string Uid = null;
        string username = DropDownList1.SelectedValue;
        Uid = obj.selectUIdDropdown(username);
        string userid = Session["UserId"].ToString();


        ArrayList userBU = new ArrayList();
        int res = 1;
        for (int i = 0; i <= GridViewBU.Rows.Count - 1; i++)
        {
            string ID = GridViewBU.Rows[i].Cells[1].Text.Trim();
            GridViewRow row = GridViewBU.Rows[i];
            CheckBox ck = (CheckBox)row.FindControl("CheckBoxMark1");
            if (ck.Checked == true)
            {
                userBU.Add(ID);
            }
        }

        /* Invoke Business Layer to insert data */

        Business B = new Business();

        res = B.updateAdditionalBU(userBU, username);

        if (res == 1)
        {
            ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('USER UPDATED')</script>");
            log.Info("User Updated successfully UserID:" + Uid);
        }

    }

    public SqlDataAdapter da { get; set; }
}