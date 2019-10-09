using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;

public partial class MasterPage_MasterPageRMS : System.Web.UI.MasterPage
{
    public string authSpace = "$";
    protected void Page_Init(Object sender, EventArgs e)
    {
        if (Session["User"] == null || Session["User"].ToString() == "")
        {
            string unacces = "Session time expired";
            Response.Redirect("~/Login.aspx?val=" + unacces);
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            if (Session["Role"].ToString().Equals("1") || Session["Role"].ToString().Equals("2") ||
                Session["Role"].ToString().Equals("3") || Session["Role"].ToString().Equals("4") || Session["Role"].ToString().Equals("5")
                || Session["Role"].ToString().Equals("6") || Session["Role"].ToString().Equals("7") || Session["Role"].ToString().Equals("8") ||
                Session["Role"].ToString().Equals("9") || Session["Role"].ToString().Equals("10") || Session["Role"].ToString().Equals("11") ||
                Session["Role"].ToString().Equals("12") || Session["Role"].ToString().Equals("13") || Session["Role"].ToString().Equals("14") || Session["Role"].ToString().Equals("15") || Session["Role"].ToString().Equals("16") || Session["Role"].ToString().Equals("17") || Session["Role"].ToString().Equals("18") || Session["Role"].ToString().Equals("19") || Session["Role"].ToString().Equals("20") || Session["Role"].ToString().Equals("21") || Session["Role"].ToString().Equals("22") || Session["Role"].ToString().Equals("23"))
            {
                Session["authPage"] = authSpace;
                getMenu();
                Label1.Text = "         " + Session["UserName"].ToString();
            }
            else
            {
                string unacces = "Unauthorized Acces!!! Login Again";
                Response.Redirect("~/Login.aspx?val=" + unacces);
            }

        }
    }
    public void getMenu()
    {
        Login_DataObject obj = new Login_DataObject();

        int role = (int)Session["Role"];
        string userid = Session["UserId"].ToString();
        string InstituteId = Session["InstituteId"].ToString();
        string Department = Session["Department"].ToString();

        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter();
        if (role == 21)
        {
            da = obj.DynamicMenuStudent(role);
        }
        else
        {
            da = obj.DynamicMenu(userid);
        }


        da.Fill(ds);
        dt = ds.Tables[0];

        // DataRow[] drowpar = dt.Select("RoleID=" + role);
        foreach (DataRow dr in dt.Rows)
        {
            if (dr["LinkLevel"].Equals("M1"))
            {
                menuBar.Items.Add(new MenuItem(dr["LinkName"].ToString(),
                    dr["Id"].ToString(), "",
                    dr["URL"].ToString()));
            }
            else
            {

            }
            if (dr["Id"] != null)
            {
                DataSet ds1 = new DataSet();
                DataTable dt1 = new DataTable();
                SqlDataAdapter da1 = new SqlDataAdapter();
                string role1 = Convert.ToString(role);
                if (role1 == "21")
                {
                    da1 = obj.DynamicMenuStudent1(role1, dr["Id"].ToString());
                }
                else
                {
                    da1 = obj.DynamicMenu1(userid, dr["Id"].ToString());
                }


                da1.Fill(ds1);
                dt1 = ds1.Tables[0];

                // DataRow[] drowpar1 = dt1.Select("ParentID =" + dr["Id"].ToString());
                //DataRow[] drowpar1 = dt1.Select("RoleID=" + role);

                foreach (DataRow dr1 in dt1.Rows)
                {

                    MenuItem mnu = new MenuItem(dr1["LinkName"].ToString(),
                                   dr1["Id"].ToString(),
                                   "", dr1["URL"].ToString());

                    menuBar.FindItem(dr1["ParentID"].ToString()).ChildItems.Add(mnu);
                    Session["authPage"] = Session["authPage"].ToString() + dr1["id"].ToString() + authSpace;

                }
            }

        }
    }
}
