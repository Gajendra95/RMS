using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using System.Collections;


public partial class UserManagement_ManageStudent : System.Web.UI.Page
{
    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    protected void Page_Load(object sender, EventArgs e)
    {
        popupPanelBaCode.Visible = true;
        if (!IsPostBack)
        {
            ArrayList PageRollList = new ArrayList();
            PageRollList.Add("2");          
            string userrole = Session["Role"].ToString();
            if (PageRollList.Contains(userrole))
            {

            }
            else
            {
                string unacces = "Unauthorized Acces!!! Login Again";
                Response.Redirect("~/Login.aspx?val=" + unacces);
            }
            setModalWindow(sender, e);

            GridViewUser.Visible = false;
            Butback.Visible = false;
            txtuserid.Text = " ";
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
            b.Name = TextBoxName.Text.Trim();
            b.Department_Id = DDLdeptname.SelectedValue;
            b.EmailId = txtemailid.Text.Trim();
            b.InstituteId = DDLinstitutename.SelectedValue;
            //b.BankAccountNumber = txtaccno.Text;
            if (txtDOB.Text != "01/01/0001" && txtDOB.Text != null && txtDOB.Text != "")
            {
                b.StudentDOB = Convert.ToDateTime(txtDOB.Text.Trim());
            }
            if (txtMobileNo.Text != "")
            {
                b.MobileNo = txtMobileNo.Text;
            }     
            b.Sex = DropDownListsex.SelectedValue; 
            b.CreatedDate = DateTime.Now.ToString("yyyy-MM-dd");
            b.CreatedBy = Session["UserId"].ToString();           
            int retVal = bl.SaveStudentDetails(b);          
            lblStatus.ForeColor = System.Drawing.Color.Green;
            ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Student details saved successfully');window.location='" + Request.ApplicationPath + "/UserManagement/ManageStudent.aspx';</script>");

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

    private string Encrypt(string clearText)
    {
        string EncryptionKey = "MAKV2SPBNI99212";
        byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(clearBytes, 0, clearBytes.Length);
                    cs.Close();
                }
                clearText = Convert.ToBase64String(ms.ToArray());
            }
        }
        return clearText;
    }
    protected void btnupdate_Click(object sender, EventArgs e) //update button
    {
        try
        {
            User_Mangement bl = new User_Mangement();
            Business B = new Business();
            b.User_Id = txtuserid.Text.Trim();
            b.Name = TextBoxName.Text.Trim();
            b.Department_Id = DDLdeptname.SelectedValue;
            b.EmailId = txtemailid.Text.Trim();
            b.InstituteId = DDLinstitutename.SelectedValue;
            if (txtDOB.Text != "01/01/0001" && txtDOB.Text != null && txtDOB.Text != "")
            {
                b.StudentDOB = Convert.ToDateTime(txtDOB.Text.Trim());
            }
            if (txtMobileNo.Text != "")
            {
                b.MobileNo = txtMobileNo.Text;
            }
            b.Sex = DropDownListsex.SelectedValue;
            //b.BankAccountNumber = txtaccno.Text;
            b.CreatedDate = DateTime.Now.ToString("yyyy-MM-dd");
            b.UpdatedBy = Session["UserId"].ToString();        
            string OldmailID = Session["OldmailID"].ToString();       
            int isupdatemailid = 0;
            if (OldmailID != "")
            {
                if (OldmailID != b.emailId)
                {
                    isupdatemailid = 1;
                }
            }
            //if (b.Role_Id == 1)
            //{
            //    b.pubinchargedept = RadioButtonListDeparmentPubincharge.SelectedValue;
            //}
           // int retemail = bl.Updatestudentmailid(txtemailid.Text, b, isupdatemailid, OldmailID);
            //   b.Role_Name = DDLrolename.SelectedValue;
            int retVal = bl.UpdateStudentdetails(b); //Business layer
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

            b = v.selectExistingStudentDetails(txtuserid.Text.Trim());

            if (b.User_Id != null)
            {            
                txtuserid.Text = b.User_Id;            
                //TextBoxName.Enabled = false;
                TextBoxName.Text = b.Name;
                DDLinstitutename.SelectedValue = b.Institute_name.Trim();
                //DDLinstitutename.Enabled = false;            
                DDLdeptname.Items.Clear();
                // DDLdeptname.Items.Add(new ListItem("--Select--", "", true));
                SqlDataSource1.SelectCommand = "select ClassCode,ClassName from SISClass";
                SqlDataSource1.DataBind();
                DDLdeptname.DataBind();
                DDLdeptname.SelectedValue = b.Department.Trim();
                DDLdeptname.DataBind();
                //DDLdeptname.Enabled = false;
                txtemailid.Text = b.emailId;
                Session["OldmailID"] = txtemailid.Text;
                DropDownListsex.SelectedValue = b.Sex;
                //txtaccno.Text = b.BankAccountNumber;
                if (b.StudentDOB.ToShortDateString() != "01/01/0001")
                {
                    txtDOB.Text = b.StudentDOB.ToShortDateString();
                }
                if (b.MobileNo != "")
                {
                    txtMobileNo.Text = b.MobileNo;
                }
                if (txtemailid.Text == "" || txtemailid.Text == null)
                {
                    txtemailid.Enabled = true;
                }
                else
                {
                    txtemailid.Enabled = true;
                }         
                txtuserid.Enabled = false;
                btninsert.Enabled = false;
                btnupdate.Enabled = true;
                popupPanelBaCode.Visible = false;
                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert(' User exixts!')</script>");

            }

            else
            {
                //lblActive.Visible = false;
                //DropDownListactive.Visible = false;
                btninsert.Enabled = true;
                btnupdate.Enabled = false;
                txtuserid.Enabled = false;
                popupPanelBaCode.Visible = false;
                txtemailid.Text = " ";
                //txtPassword.Text = " ";
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
        DDLdeptname.Items.Add(new ListItem("--Select--", "0", true));
        SqlDataSource1.SelectCommand = "select ClassCode,ClassName from SISClass";
        SqlDataSource1.DataBind();
        DDLdeptname.DataBind();
    }
   
    protected void BtnClear_Click(object sender, EventArgs e)
    {
        //lblActive.Visible = false;
        //DropDownListactive.Visible = false;
        //PubDept.Visible = false;
        //RadioButtonListDeparmentPubincharge.Visible = false;
        //RadioButtonListDeparmentPubincharge.Items.Clear();
        //DDLrolename.Enabled = true;
        txtuserid.Text = "";
        txtuserid.Enabled = true;
        //TextBoxFname.Text = "";
        //TextBoxFname.Enabled = true;
        //TextBoxMname.Text = "";
        //TextBoxMname.Enabled = true;
        TextBoxName.Text = "";
        TextBoxName.Enabled = true;
        //DropDownListPrefix.ClearSelection();
        //DropDownListPrefix.Enabled = true;
        //  txtname.Text = "";
        //txtname.Enabled = true;
        //    DDLdeptname.Items.Clear();
         txtDOB.Text ="";
               
         txtMobileNo.Text = "";
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
        //DDLrolename.Items.Clear();
        //DDLrolename.Items.Add(new ListItem("--Select--", "0", true));
        //DDLrolename.DataBind();
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
        Response.Redirect("ManageStudent.aspx");
    }

    protected void UserIdChanged(object sender, EventArgs e)
    {
        if (USerIdSrch.Text.Trim() == "" && TextBoxRollno.Text.Trim()=="")
        {
            SqlDataSourceUSer.SelectCommand = "SELECT top 10 RollNo as [User_Id], Name  as Name  FROM [SISStudentGenInfo]";
            popGridUser.DataBind();
            popGridUser.Visible = true;
        }

        else if (USerIdSrch.Text.Trim() != "" && TextBoxRollno.Text.Trim() == "")
        {
            SqlDataSourceUSer.SelectParameters.Clear();
            SqlDataSourceUSer.SelectParameters.Add("Name", USerIdSrch.Text);

            SqlDataSourceUSer.SelectCommand = "SELECT top 10 RollNo as [User_Id], Name  as Name  FROM [SISStudentGenInfo] where Name like '%' + @Name + '%'";
            popGridUser.DataBind();
            popGridUser.Visible = true;
        }
        else if (USerIdSrch.Text.Trim() == "" && TextBoxRollno.Text.Trim() != "")
        {
            SqlDataSourceUSer.SelectParameters.Clear();
            SqlDataSourceUSer.SelectParameters.Add("RollNo", TextBoxRollno.Text);

            SqlDataSourceUSer.SelectCommand = "SELECT top 10 RollNo as [User_Id], Name  as Name  FROM [SISStudentGenInfo] where RollNo like '%' + @RollNo + '%'";
            popGridUser.DataBind();
            popGridUser.Visible = true;
        }
        else if (USerIdSrch.Text.Trim() != "" && TextBoxRollno.Text.Trim() != "")
        {
            SqlDataSourceUSer.SelectParameters.Clear();
            SqlDataSourceUSer.SelectParameters.Add("RollNo", TextBoxRollno.Text);
            SqlDataSourceUSer.SelectParameters.Add("Name", USerIdSrch.Text);

            SqlDataSourceUSer.SelectCommand = "SELECT top 10 RollNo as [User_Id], Name  as Name  FROM [SISStudentGenInfo] where RollNo like '%' + @RollNo  + '%' and Name like '%' + @Name + '%'";
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