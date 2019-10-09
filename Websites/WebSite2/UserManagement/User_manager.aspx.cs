using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;

public partial class User_manager : System.Web.UI.Page
{
    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    protected void Page_Load(object sender, EventArgs e)
    {
        popupPanelBaCode.Visible = true;
        if (!IsPostBack)
        {
            setModalWindow(sender, e);

            GridViewUser.Visible = false;
            Butback.Visible = false;

        }
    }
    protected void setModalWindow(object sender, EventArgs e)
    {
        popupPanelBaCode.Visible = true;
        popGridUser.DataSourceID = "SqlDataSourceUSer";
        SqlDataSourceUSer.DataBind();
        popGridUser.DataBind();

    }

    User b = new User();
    User_Mangement bl = new User_Mangement();


    protected void btn_insert(object sender, EventArgs e) //insert button
    {

        if (!Page.IsValid)
        {
            return;
        }

        try
        {
            b.User_Id = txtuserid.Text.Trim();
            // b.Name = txtname.Text;
            b.UserNamePrefix = DropDownListPrefix.SelectedValue;
            b.UserFirstName = TextBoxFname.Text.Trim();
            b.UserMiddleName = TextBoxMname.Text.Trim();
            b.UserLastName = TextBoxLName.Text.Trim();
            b.Department_Id = DDLdeptname.SelectedValue;
            b.EmailId = txtemailid.Text.Trim();
            b.InstituteId = DDLinstitutename.SelectedValue;
            b.AutoApproved = "Y";
            b.Role_Id = Convert.ToInt32(DDLrolename.SelectedValue);
            b.CreatedDate = DateTime.Now.ToString("yyyy-MM-dd");
            b.CreatedBy = Session["UserId"].ToString();
            if (b.Role_Id == 1)
            {
                b.pubinchargedept = RadioButtonListDeparmentPubincharge.SelectedValue;
            }

            int retVal = bl.SaveDepartmentDetails(b);

            //lblStatus.Text = "";
            lblStatus.ForeColor = System.Drawing.Color.Green;
            ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('User detail saved successfully');window.location='" + Request.ApplicationPath + "/UserManagement/User_manager.aspx';</script>");

            GridViewUser.Visible = false;
            btninsert.Enabled = false;
            btninsert.Enabled = true;
            popupPanelBaCode.Visible = false;
            b = null;
            bl = null;
        }
        catch (Exception ex)
        {
            log.Error(ex.StackTrace);
            log.Error(ex.Message);

            log.Error("Error!!!!!!!!!!!!!!!! ");

            ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Error!!!!!!!!!!')</script>");

        }

    }
    protected void btnupdate_Click(object sender, EventArgs e) //update button
    {
        try
        {
            Business B = new Business();
            b.User_Id = txtuserid.Text;
            b.Role_Id = Convert.ToInt32(DDLrolename.SelectedValue);
            b.InstituteId = DDLinstitutename.SelectedValue;
            b.emailId = txtemailid.Text;
            string OldmailID = Session["OldmailID"].ToString();
            b.CreatedBy = Session["UserId"].ToString();
            int isupdatemailid = 0;
            if (OldmailID != "")
            {
                if (OldmailID != b.emailId)
                {
                    isupdatemailid = 1;
                }
            }
            if (b.Role_Id == 1)
            {
                b.pubinchargedept = RadioButtonListDeparmentPubincharge.SelectedValue;
            }
            int retemail = B.Updatemailid(txtemailid.Text, b, isupdatemailid, OldmailID);
            //   b.Role_Name = DDLrolename.SelectedValue;
            int retVal = bl.Update(b); //Business layer
            ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('User detail update successfully')</script>");
            //btninsert.Enabled = false;
            txtemailid.Enabled = false;
            popupPanelBaCode.Visible = false;
            GridViewUser.DataBind();
            GridViewUser.Visible = false;
        }
        catch (Exception ex)
        {
            log.Error(ex.StackTrace);
            log.Error(ex.Message);

            log.Error("Error!!!!!!!!!!!!!!!! ");

            ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Error!!!!!!!!!!')</script>");

        }
    }

    protected void DeptcodeTextChanged(object sender, EventArgs e)
    {
        try
        {
            User_Mangement v = new User_Mangement();
            User b = new User();
            User b1 = new User();

            //b.UserId = null;
            //b.UserId = txtuserid.Text;
            b.User_Id = null;
            // b.User_Id = txtuserid.Text;

            b = v.selectExistingUser(txtuserid.Text.Trim());

            if (b.User_Id != null)
            {
                lblActive.Visible = true;
                DropDownListactive.Visible = true;
                txtuserid.Text = b.User_Id;
                DropDownListPrefix.SelectedValue = b.UserNamePrefix;
                DropDownListPrefix.Enabled = false;
                TextBoxFname.Text = b.UserFirstName;
                TextBoxFname.Enabled = false;
                TextBoxMname.Text = b.UserMiddleName;
                TextBoxMname.Enabled = false;
                TextBoxLName.Text = b.UserLastName;
                TextBoxLName.Enabled = false;
                DDLinstitutename.SelectedValue = b.Institute_name;
                DDLinstitutename.Enabled = false;

                DropDownListactive.SelectedValue = b.Active;
                DropDownListactive.Enabled = false;

                DDLdeptname.Items.Clear();

                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("Institute_Id", DDLinstitutename.SelectedValue);

                // DDLdeptname.Items.Add(new ListItem("--Select--", "", true));
                SqlDataSource1.SelectCommand = "SELECT DISTINCT [DeptName], [DeptId] FROM [Dept_M] where Institute_Id =@Institute_Id";
                SqlDataSource1.DataBind();
                DDLdeptname.DataBind();

                DDLdeptname.SelectedValue = b.Department;
                DDLdeptname.DataBind();
                DDLdeptname.Enabled = false;
                txtemailid.Text = b.EmailId;
                Session["OldmailID"] = txtemailid.Text;
                if (txtemailid.Text == "" || txtemailid.Text == null)
                {
                    txtemailid.Enabled = true;
                }
                else
                {
                    txtemailid.Enabled = true;
                }
                //RBLautoapproval.Text = b.AutoApproved;
                //RBLautoapproval.Enabled = false;
                // SqlDataSource3.SelectCommand = "";
                if (b.Role_Id == 5)
                {//librarian
                    SqlDataSource3.SelectCommand = "SELECT DISTINCT [Role_Name], [Role_Id] FROM [Role_M] ";
                    SqlDataSource3.DataBind();
                    DDLrolename.DataBind();
                }
                else
                {
                    // SqlDataSource3.SelectCommand = "SELECT DISTINCT [Role_Name], [Role_Id] FROM [Role_M]  where Role_Id!=5";
                    // SqlDataSource3.DataBind();
                    //DDLrolename.DataBind();
                }
                DDLrolename.SelectedValue = Convert.ToString(b.Role_Id);
                if (b.Role_Id == 1)
                {
                    DDLrolename.Enabled = true;
                    btnupdate.Enabled = true;
                    RadioButtonListDeparmentPubincharge.Items.Clear();
                    string deptname = DDLdeptname.SelectedItem.ToString();
                    string deptvalue = DDLdeptname.SelectedValue;
                    PubDept.Visible = true;
                    RadioButtonListDeparmentPubincharge.Visible = true;
                    RadioButtonListDeparmentPubincharge.Items.Add(new ListItem(deptname, deptvalue, true));
                    RadioButtonListDeparmentPubincharge.Items.Add(new ListItem("All Departments of Institute", "", true));

                    b1 = v.selectPubInchargeUM(txtuserid.Text.Trim());

                    RadioButtonListDeparmentPubincharge.SelectedValue = b1.Department_Id;
                    RadioButtonListDeparmentPubincharge.DataBind();
                }
                else if (b.Role_Id == 5)
                {
                    //librarian
                    DDLrolename.Enabled = false;
                    btnupdate.Enabled = false;

                }
                else
                {
                    DDLrolename.Enabled = true;
                    btnupdate.Enabled = true;
                    PubDept.Visible = false;
                    RadioButtonListDeparmentPubincharge.Visible = false;
                    RadioButtonListDeparmentPubincharge.Items.Clear();
                }
                txtuserid.Enabled = false;
                btninsert.Enabled = false;
                // btnupdate.Enabled = true;
                popupPanelBaCode.Visible = false;
                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert(' User exixts!')</script>");

            }

            else
            {
                lblActive.Visible = false;
                DropDownListactive.Visible = false;
                btninsert.Enabled = true;
                btnupdate.Enabled = false;
                txtuserid.Enabled = false;
                popupPanelBaCode.Visible = false;
                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert(' New User!')</script>");

            }

        }

        catch (Exception ex)
        {
            log.Error(ex.StackTrace);
            log.Error(ex.Message);

            log.Error("Error!!!!!!!!!!!!!!!! ");
            if (ex.Message.Contains("DDLdeptname' has a SelectedValue which is invalid because it does not exist in the list of"))
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Error!!Institue_Department Map error!!!')</script>");

            }

            else
                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Error!!!!!!!!!!')</script>");

        }
    }
    protected void DDLinstitutename_SelectedIndexChanged(object sender, EventArgs e)
    {
        DDLdeptname.Items.Clear();
        DDLdeptname.Items.Add(new ListItem("--Select--", "", true));
        SqlDataSource1.SelectCommand = "SELECT DISTINCT [DeptName], [DeptId] FROM [Dept_M] where Institute_Id = '" + DDLinstitutename.SelectedValue + "'";
        SqlDataSource1.DataBind();
        DDLdeptname.DataBind();
    }
    protected void DDLrolenameOnSelectedIndexChanged(object sender, EventArgs e)
    {

        if (DDLrolename.SelectedValue == "1")
        {
            RadioButtonListDeparmentPubincharge.Items.Clear();
            string deptname = DDLdeptname.SelectedItem.ToString();
            string deptvalue = DDLdeptname.SelectedValue;
            PubDept.Visible = true;
            RadioButtonListDeparmentPubincharge.Visible = true;
            RadioButtonListDeparmentPubincharge.Items.Add(new ListItem(deptname, deptvalue, true));
            RadioButtonListDeparmentPubincharge.Items.Add(new ListItem("All Departments of Institute", "", true));
            RadioButtonListDeparmentPubincharge.SelectedValue = "";
            RadioButtonListDeparmentPubincharge.DataBind();
        }
        else
        {
            PubDept.Visible = false;
            RadioButtonListDeparmentPubincharge.Visible = false;
            RadioButtonListDeparmentPubincharge.Items.Clear();
        }
    }
    protected void BtnClear_Click(object sender, EventArgs e)
    {
        lblActive.Visible = false;
        DropDownListactive.Visible = false;
        PubDept.Visible = false;
        RadioButtonListDeparmentPubincharge.Visible = false;
        RadioButtonListDeparmentPubincharge.Items.Clear();
        DDLrolename.Enabled = true;
        txtuserid.Text = "";
        txtuserid.Enabled = true;
        TextBoxFname.Text = "";
        TextBoxFname.Enabled = true;
        TextBoxMname.Text = "";
        TextBoxMname.Enabled = true;
        TextBoxLName.Text = "";
        TextBoxLName.Enabled = true;
        DropDownListPrefix.ClearSelection();
        DropDownListPrefix.Enabled = true;
        //  txtname.Text = "";
        //txtname.Enabled = true;
        //    DDLdeptname.Items.Clear();
        DDLdeptname.Items.Clear();
        DDLdeptname.Items.Add(new ListItem("--Select--", "0", true));
        DDLdeptname.DataBind();
        DDLdeptname.Enabled = true;
        txtemailid.Text = "";
        txtemailid.Enabled = true;
        DDLinstitutename.Items.Clear();
        DDLinstitutename.Items.Add(new ListItem("--Select--", "0", true));
        DDLinstitutename.DataBind();
        DDLinstitutename.Enabled = true;
        //RBLautoapproval.Enabled = true;
        //RBLautoapproval.SelectedValue = "Y";
        DDLrolename.Items.Clear();
        DDLrolename.Items.Add(new ListItem("--Select--", "0", true));
        DDLrolename.DataBind();
        GridViewUser.Visible = false;
        //  DDLrolename.SelectedValue = "";
        btnupdate.Enabled = false;
        btninsert.Enabled = true;


    }
    protected void Butview_Click(object sender, EventArgs e)
    {
        //log.Debug("inside Butview_Click");

        GridViewUser.Visible = true;
        Butback.Visible = true;
    }
    protected void btn_back(object sender, EventArgs e)
    {
        Response.Redirect("User_manager.aspx");
    }

    protected void UserIdChanged(object sender, EventArgs e)
    {
        if (USerIdSrch.Text.Trim() == "")
        {
            SqlDataSourceUSer.SelectCommand = "SELECT [User_Id], Prefix+FirstName+MiddleName+LastName as Name FROM [User_M]";
            popGridUser.DataBind();
            popGridUser.Visible = true;
        }

        else
        {

            SqlDataSourceUSer.SelectCommand = "SELECT [User_Id],   Prefix+FirstName+MiddleName+LastName as Name   FROM [User_M] where Prefix+FirstName+MiddleName+LastName like '%" + USerIdSrch.Text + "%'";
            popGridUser.DataBind();
            popGridUser.Visible = true;
        }

        ModalPopupExtender1.Show();
    }

    protected void popSelected(Object sender, EventArgs e)
    {

        if (senderID.Value.Contains("imageBkCbtn"))
        {

            popGridUser.Visible = true;
            GridViewRow row = popGridUser.SelectedRow;

            string UserId = row.Cells[1].Text;


            txtuserid.Text = UserId;

            USerIdSrch.Text = "";
            popGridUser.DataBind();
            DeptcodeTextChanged(sender, e);
        }

    }


    protected void showPop(object sender, EventArgs e)
    {
        ModalPopupExtender1.Show();
    }


    protected void Buttonexit_Click(object sender, EventArgs e)
    {
        popGridUser.DataBind();
    }
}