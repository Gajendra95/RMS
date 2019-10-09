using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class PublicationEntry_EditResearchData : System.Web.UI.Page
{
    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    public string[] area_value;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Business b = new Business();
            FileUpload f = new FileUpload();
            string role = Session["Role"].ToString();
            string userid = Session["UserId"].ToString();

            if (role != "2")
            {
                lblUserid.Visible = true;
                txtUserID.Visible = true;
                txtUserID.Enabled = false;
                btnIdSearch.Visible = false;
                lblArea.Visible = true;
                btnAdd.Visible = true;
                txtDomain.Enabled = true;
                //panelarea.Visible=true;
                PanelResearch.Visible = true;
                GridResearch.Visible = true;
                btnSave.Enabled = true;
                txtUserID.Text = Session["UserId"].ToString();
              //  bool result1 = b.CheckUserId(userid);

                //if (result1 == false)
                //{
                //    string CloseWindow1 = "alert('User id doesnot exist')";
                //    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow1", CloseWindow1, true);
                //    lblUserid.Visible = false;
                //    txtUserID.Visible = false;
                //    btnIdSearch.Visible = false;
                //    lblArea.Visible = false;
                //    txtDomain.Enabled = false;
                //    btnAdd.Visible = false;
                //    //panelarea.Visible=true;
                //    PanelResearch.Visible = false;
                //    GridResearch.Visible = false;
                //    btnSave.Enabled = false;
                //    return;
                //}

                User u = new User();
             
                u = b.CheckOrcidScopusid(userid);
                txtOrcid.Text = u.orcid;
                txtScopusid.Text = u.scopusid;
                txtScopusid2.Text = u.scopusid2;
                txtScopusid3.Text = u.scopusid3;
                f = b.DomainSearch(userid);
                // string domain = f.Domain.ToString();
                if (f.Domain != null)
                {

                    //string[] domain_value = domain.Split(new string[] { "::" }, StringSplitOptions.None);
                    string[] domain_value = (f.Domain.Split(':'));
                    for (int k = 0; k <= domain_value.GetUpperBound(0); k++)
                    {
                        if (domain_value[k] != "")
                        {

                            if (k == 0)
                            {
                                txtDomain.Text = domain_value[k];
                            }
                            else
                            {
                                txtDomain.Text = txtDomain.Text + ":" + domain_value[k];
                            }
                        }
                    }


                    SqlDataAdapter sda = new SqlDataAdapter();
                    DataTable dt = new DataTable();
                    string cs = ConfigurationManager.ConnectionStrings["RMSConnectionString"].ConnectionString;
                    using (SqlConnection conn = new SqlConnection(cs))
                    {
                        //using (SqlCommand cmd = new SqlCommand("SELECT Area from FacultyResearchArea where UserId='" + userid.ToString() + "'", conn))
                        using (SqlCommand cmd = new SqlCommand("SELECT Area from FacultyResearchArea where UserId=@UserId", conn))
                        {
                            conn.Open();
                            sda.SelectCommand = cmd;
                            sda.SelectCommand.Parameters.AddWithValue("@UserId", userid.ToString()); //added
                            sda.Fill(dt);

                            dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
                            //  dt.Columns.Add(new DataColumn("Area", typeof(string)));

                            var newdt = dt.Clone();
                            int rowIndex = 0;
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {

                                //var areaNames = dt.Rows[i]["Area"].ToString().Split(new string[] { "::" }, StringSplitOptions.None);
                                var areaNames = dt.Rows[i]["Area"].ToString().Split(':');


                                for (int j = 0; j < areaNames.Length; j++)
                                {
                                    DataRow dr = null;
                                    // dr = dt.NewRow();

                                    if (areaNames[j] != "")
                                    {
                                        string res = areaNames[j];

                                        dr = newdt.Rows.Add();


                                        dr["RowNumber"] = res;

                                        dr["Area"] = res;

                                    }
                                }
                            }

                            GridResearch.DataSource = newdt;
                            GridResearch.DataBind();

                            ViewState["CurrentTable"] = newdt;
                        }
                    }
                }
                //if (role == "2")
                //{
                //    lblUserid.Visible = true;
                //    txtUserID.Visible = true;
                //    btnIdSearch.Visible = true;
                //    lblArea.Visible = false;
                //    btnAdd.Visible = false;
                //    txtDomain.Enabled = false;
                //    //panelarea.Visible=true;
                //    PanelResearch.Visible = false;
                //    GridResearch.Visible = false;
                //    btnSave.Enabled = false;
                //}

                else
                {

                    lblUserid.Visible = true;
                    txtUserID.Visible = true;
                    //btnIdSearch.Visible = true;

                    txtDomain.Enabled = true;
                    //panelarea.Visible=true;
                    PanelResearch.Visible = true;
                    GridResearch.Visible = true;
                    btnSave.Enabled = true;
                    lblArea.Visible = true;
                    btnAdd.Visible = true;
                    SetInitialRow();
                }
            }

        }
    }

   

    private void SetInitialRow()
    {
        DataTable dt = new DataTable();
        DataRow dr = null;
        dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
        dt.Columns.Add(new DataColumn("Area", typeof(string)));


        dr = dt.NewRow();
        dr["RowNumber"] = 1;
        dr["Area"] = string.Empty;

        dt.Rows.Add(dr);

        //Store the DataTable in ViewState
        ViewState["CurrentTable"] = dt;

        GridResearch.DataSource = dt;
        GridResearch.DataBind();
        GridResearch.Visible = true;

        TextBox box1 = (TextBox)GridResearch.Rows[0].Cells[1].FindControl("Area");
        dt.Rows[0]["Area"] = null;

        GridResearch.Columns[1].Visible = true;
    }



    protected void btnSave_Click(object sender, EventArgs e)
    {

        try
        {

            Business b = new Business();
            FileUpload f = new FileUpload();

            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];

            FileUpload[] JD = new FileUpload[dtCurrentTable.Rows.Count];
            string role = Session["Role"].ToString();
            string empcode = "";
            if (role == "11")
            {

                empcode = txtUserID.Text;
            }
            else
            {
                empcode = txtUserID.Text.Trim();
            }
            int res = 0;
            res = b.checkExistUserid(empcode);

            if (res == 1)
            {
                User u = new User();
                u.orcid = txtOrcid.Text;
                u.scopusid = txtScopusid.Text;
                u.scopusid2 = txtScopusid2.Text;
                u.scopusid3 = txtScopusid3.Text;
                string domain = txtDomain.Text;

                //string arealist = string.Concat(area + "@" + area1 + "@" + area2 + "@" + area3 + "@" + area4 + "@" + area5 + "@" + area6 + "@" + area7);
                //f.EmployeeCode = empcode;
                f.Domain = domain;
                //f.Area = arealist;

                int rowIndex1 = 0;
                if (dtCurrentTable.Rows.Count > 0)
                {

                    for (int i = 0; i < dtCurrentTable.Rows.Count; i++)
                    {
                        JD[i] = new FileUpload();

                        TextBox Area = (TextBox)GridResearch.Rows[rowIndex1].Cells[0].FindControl("Area");

                        JD[i].Area = Area.Text;


                        rowIndex1++;

                    }



                }

                int result = b.fnUpdateResearchData(empcode, f, JD, u);
                if (result > 0)
                {

                    log.Info("File updated successfully");
                    string CloseWindow = "alert('Record Updated Successfully')";
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow, true);

                    btnSave.Enabled = false;

                }
                else
                {
                    string CloseWindow = "alert('Error while updating')";
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow, true);


                }
            }
            else if (res == 2)
            {
                User u = new User();
                empcode = txtUserID.Text;
                u.orcid = txtOrcid.Text.Trim();
                u.scopusid = txtScopusid.Text.Trim();
                u.scopusid2 = txtScopusid2.Text.Trim();
                u.scopusid3 = txtScopusid3.Text.Trim();
                string domain = txtDomain.Text.Trim();
                f.Domain = domain;
                int rowIndex1 = 0;
                if (dtCurrentTable.Rows.Count > 0)
                {

                    for (int i = 0; i < dtCurrentTable.Rows.Count; i++)
                    {
                        JD[i] = new FileUpload();

                        TextBox Area = (TextBox)GridResearch.Rows[rowIndex1].Cells[0].FindControl("Area");

                        JD[i].Area = Area.Text;


                        rowIndex1++;

                    }

                }
                int result = b.fninsertEditResearchData(empcode, f, JD, u);
                if (result > 0)
                {

                    log.Info("Record inserted successfully");
                    string CloseWindow = "alert('Record inserted Successfully')";
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow, true);

                    btnSave.Enabled = false;

                }
                else
                {
                    string CloseWindow = "alert('Error while inserting')";
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow, true);


                }

            }
        }
        catch (Exception ex)
        {
            log.Error("Inside Catch Block Of btnSave_Click function" + ex.Message);

            log.Error(ex.StackTrace);
        }

    }
    protected void btnIdSearch_Click(object sender, EventArgs e)
    {

        if (txtUserID.Text.Trim() == "")
        {
            string CloseWindow1 = "alert('Please enter user id')";
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow1", CloseWindow1, true);
            return;
        }

        Business b = new Business();
        FileUpload f = new FileUpload();

        lblUserid.Visible = true;
        txtUserID.Visible = true;
        btnIdSearch.Visible = true;
        PanelResearch.Visible = true;
        GridResearch.Visible = true;
        //panelarea.Visible = true;
        lblArea.Visible = true;
        btnAdd.Visible = true;
        txtDomain.Enabled = true;
        btnSave.Visible = true;
        btnSave.Enabled = true;
        string userid = txtUserID.Text.Trim();

        //bool result1 = b.CheckUserId(userid);

        //if (result1 == false)
        //{
        //    string CloseWindow1 = "alert('User id doesnot exist')";
        //    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow1", CloseWindow1, true);
        //    txtUserID.Text = "";

        //    lblArea.Visible = false;
        //    txtDomain.Enabled = false;
        //    btnAdd.Visible = false;
        //    //panelarea.Visible=true;
        //    PanelResearch.Visible = false;
        //    GridResearch.Visible = false;
        //    btnSave.Enabled = false;
        //    return;
        //}


        f = b.DomainSearch(userid);
        string domain = f.Domain.ToString();
        if (domain != null)
        {

            //string[] domain_value = domain.Split(new string[] { "::" }, StringSplitOptions.None);
            string[] domain_value = domain.Split(':');
            for (int k = 0; k <= domain_value.GetUpperBound(0); k++)
            {
                if (domain_value[k] != "")
                {

                    if (k == 0)
                    {
                        txtDomain.Text = domain_value[k];
                    }
                    else
                    {
                        txtDomain.Text = txtDomain.Text + ":" + domain_value[k];
                    }
                }
            }
        }


        SqlDataAdapter sda = new SqlDataAdapter();
        DataTable dt = new DataTable();
        string cs = ConfigurationManager.ConnectionStrings["RMSConnectionString"].ConnectionString;
        using (SqlConnection conn = new SqlConnection(cs))
        {
            //using (SqlCommand cmd = new SqlCommand("SELECT Area from FacultyResearchArea where UserId='" + userid.ToString() + "'", conn))
            using (SqlCommand cmd = new SqlCommand("SELECT Area from FacultyResearchArea where UserId=@UserId", conn))

            {
                conn.Open();
                sda.SelectCommand = cmd;
                sda.SelectCommand.Parameters.AddWithValue("@UserId", userid.ToString()); //added
                sda.Fill(dt);

                dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
                //  dt.Columns.Add(new DataColumn("Area", typeof(string)));

                var newdt = dt.Clone();
                int rowIndex = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    //var areaNames = dt.Rows[i]["Area"].ToString().Split(new string[] { "::" }, StringSplitOptions.None);
                    var areaNames = dt.Rows[i]["Area"].ToString().Split(':');


                    for (int j = 0; j < areaNames.Length; j++)
                    {
                        DataRow dr = null;
                        // dr = dt.NewRow();

                        if (areaNames[j] != "")
                        {
                            string res = areaNames[j];

                            dr = newdt.Rows.Add();


                            dr["RowNumber"] = res;

                            dr["Area"] = res;

                        }
                    }
                }

                GridResearch.DataSource = newdt;
                GridResearch.DataBind();

                ViewState["CurrentTable"] = newdt;
            }
        }

    }


    protected void Grid_AuthorEntry_RowDeleting(Object sender, GridViewDeleteEventArgs e)
    {

        int index = Convert.ToInt32(e.RowIndex);
        DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
        dtCurrentTable.Rows[index].Delete();
        ViewState["CurrentTable"] = dtCurrentTable;
        GridResearch.DataSource = dtCurrentTable;
        GridResearch.DataBind();
    }


    protected void addRow(object sender, EventArgs e)
    {
        //SetInitialRow();

        if (GridResearch.Rows.Count == 0)
        {
            SetInitialRow();
        }
        else
        {

            int rowIndex = 0;
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                DataRow drCurrentRow = null;
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {
                        //extract the TextBox values
                        TextBox box1 = (TextBox)GridResearch.Rows[rowIndex].Cells[1].FindControl("Area");

                        drCurrentRow = dtCurrentTable.NewRow();

                        //drCurrentRow["Area"] = "";
                        dtCurrentTable.Rows[i - 1]["Area"] = box1.Text;
                        rowIndex++;
                    }

                    //add new row to DataTable
                    dtCurrentTable.Rows.Add(drCurrentRow);
                    //Store the current data to ViewState
                    ViewState["CurrentTable"] = dtCurrentTable;

                    //Rebind the Grid with the current data
                    GridResearch.DataSource = dtCurrentTable;
                    GridResearch.DataBind();
                }
            }
            else
            {
                Response.Write("ViewState is null");
            }

            //Set Previous Data on Postbacks
            SetPreviousData();
        }
    }
    private void SetPreviousData()
    {

        int rowIndex = 0;
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    TextBox box1 = (TextBox)GridResearch.Rows[rowIndex].Cells[1].FindControl("Area");


                    box1.Text = dt.Rows[i]["Area"].ToString();


                    rowIndex++;
                }
            }
        }

    }
}
//protected void Grid_AuthorEntry_RowDeleting(Object sender, GridViewDeleteEventArgs e)
//{

//    //SetRowData();
//    if (ViewState["CurrentTable"] != null)
//    {
//        DataTable dt = (DataTable)ViewState["CurrentTable"];
//        DataRow drCurrentRow = null;
//        int rowIndex = Convert.ToInt32(e.RowIndex);

//            if (dt.Rows.Count > 1)
//            {
//                dt.Rows.Remove(dt.Rows[rowIndex]);
//                drCurrentRow = dt.NewRow();
//                ViewState["CurrentTable"] = dt;
//                GridResearch.DataSource = dt;
//                GridResearch.DataBind();

//                //SetPreviousData();
//                // gridAmtChanged(sender, e);

//        }
//            else if (dt.Rows.Count > 1 && rowIndex != 0)
//            {
//                dt.Rows.Remove(dt.Rows[rowIndex]);
//                drCurrentRow = dt.NewRow();
//                ViewState["CurrentTable"] = dt;
//                GridResearch.DataSource = dt;
//                GridResearch.DataBind();

//                //SetPreviousData();
//                // gridAmtChanged(sender, e);
//            }
//        }
//    }
