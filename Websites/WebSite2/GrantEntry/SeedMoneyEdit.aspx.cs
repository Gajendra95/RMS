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
using System.Text.RegularExpressions;
using System.Globalization;

public partial class GrantEntry_SeedMoneyEdit : System.Web.UI.Page
{
 private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
               
                panelSearchPub.Visible = true;
                panAddAuthor.Visible = true;
                panAddAuthor.Enabled = false;
                PanelMU.Visible = true;
                Grid_AuthorEntry.Enabled = false;
                //Txtappliedbudget.Text = string.Empty;
                DropDownbudget.Items.Clear();  
                DropDownbudget.Items.Add(new ListItem("Select", "0", true));
                bindBudgetDropDownList();
                //PanelRemark.Visible = true;
                lblAPPremarks.Visible = true;
                TextRemarks.Visible = true;
                Label6.Visible = true;
                txtstatus.Visible = true;
                lblbudgetapprove.Visible = true;
                Txtapprovedbudget.Visible = true;
                //DropDownListBudgetapprove.Visible = true;
                txttiltle.Enabled=false;
                txtwriteup.Enabled=false;
                //Txtappliedbudget.Enabled = false;
                DropDownbudget.Enabled = false;
                txtdate.Enabled = false;
                PanelFileUpload.Visible = false;
                lnkSeedMoneyFaculty.Visible = false;
                lnkSeedMoneyStudent.Visible = false;
                lablPanelTitle.Visible = false;
                //DropDownListseedStatus.Items.Add(new ListItem("Select", "0", true));
                //bindstatusDropDownList();
                BtnSaveUpdate.Enabled = false;
           
            setModalWindow(sender, e);
            SetInitialRow();
            
        }
    }

    private void bindBudgetDropDownList()
    {
        DropDownbudget.DataSourceID = "SqlDataSourceBudget";
        SqlDataSourceBudget.SelectCommand = "SELECT [BudgetId], [Amount] FROM [SeedMoneyBudget_M] where Type='F'";
        DropDownbudget.DataValueField = "BudgetId";
        DropDownbudget.DataTextField = "Amount";
        DropDownbudget.DataBind();
    }
   
    //Button Search Project click
    protected void ButtonSearchProjectOnClick(object sender, EventArgs e)
    {
       
            GridViewSearchGrant.Visible = true;
            GridViewSearchGrant.EditIndex = -1;
            GridViewSearchGrant.Visible = true;
            dataBind();


    }

    private void dataBind()
    {
        Business obj = new Business();
        string userid = Session["UserId"].ToString();
        //string unit = Session["ProjectUnit"].ToString();
       

        string role = Session["Role"].ToString();
         string SeedMoneyActive = ConfigurationManager.AppSettings["FacultySeedMoneyActiveFlag"].ToString();

        
             if (txtSid.Text == "" && txtstitle.Text == "")
             {
                 SqlDataSource1.SelectCommand = " select s.ID,s.Title,s.Writeup,s.Budget,s.AppliedDate,p.StatusName,s.Createdby,s.CreatedDate,s.Institution,s.Department,s.EntryType  from SeedMoneyDetails s, Status_Seedmoney_M p where  (s.Status='APP' ) and s.Status=p.StatusId order by ID";
             }
             else if (txtSid.Text != "" && txtstitle.Text == "")
             {
                 SqlDataSource1.SelectParameters.Clear();
                 SqlDataSource1.SelectParameters.Add("ID", txtSid.Text.Trim());

                 SqlDataSource1.SelectCommand = " select s.ID,s.Title,s.Writeup,s.Budget,s.AppliedDate,p.StatusName,s.Createdby,s.CreatedDate,s.Institution,s.Department,s.EntryType  from SeedMoneyDetails s, Status_Seedmoney_M p where  (s.Status='APP' ) and s.ID like'%' + @ID + '%' and s.Status=p.StatusId order by ID";
             }
             else if (txtSid.Text == "" && txtstitle.Text != "")
             {
                 SqlDataSource1.SelectParameters.Clear();
                 SqlDataSource1.SelectParameters.Add("Title", txtstitle.Text.Trim());

                 SqlDataSource1.SelectCommand = "select s.ID,s.Title,s.Writeup,s.Budget,s.AppliedDate,p.StatusName,s.Createdby,s.CreatedDate,s.Institution,s.Department,s.EntryType  from SeedMoneyDetails s, Status_Seedmoney_M p where  (s.Status='APP' ) and s.Title like'%' + @Title + '%' and s.Status=p.StatusId order by ID";
             }
             else
             {
                 SqlDataSource1.SelectParameters.Clear();
                 SqlDataSource1.SelectParameters.Add("ID", txtSid.Text.Trim());
                 SqlDataSource1.SelectParameters.Add("Title", txtstitle.Text.Trim());

                 SqlDataSource1.SelectCommand = "select s.ID,s.Title,s.Writeup,s.Budget,s.AppliedDate,p.StatusName,s.Createdby,s.CreatedDate,s.Institution,s.Department,s.EntryType  from SeedMoneyDetails s, Status_Seedmoney_M p where  (s.Status='APP') and s.Title like'%' + @Title + '%' and s.ID like'%' + @ID + '%' and s.Status=p.StatusId order by ID";
             }
             GridViewSearchGrant.DataBind();
             SqlDataSource1.DataBind();      
                 
                  
        }
    protected void GridViewSearchGrant_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        ImageButton EditButton = (ImageButton)e.Row.FindControl("BtnEdit");
    }

    //Gridview Grant Page Index changed
    protected void GridViewSearchGrant_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        dataBind();
        GridViewSearchGrant.PageIndex = e.NewPageIndex;
        GridViewSearchGrant.DataBind();

    }

    //Function of edit button Gridview Grant
    public void GridViewSearchGrant_RowCommand(Object sender, GridViewCommandEventArgs e)
    {
        string Sid = null;
        string STitle = null;
        string Writeup = null;
        string Status = null;
     
        if (e.CommandName == "Edit")
        {

            GridViewRow rowSelect = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
            int rowindex = rowSelect.RowIndex;
            HiddenField SID = (HiddenField)GridViewSearchGrant.Rows[rowindex].Cells[2].FindControl("hiddenID");
            Sid = SID.Value;

            STitle = GridViewSearchGrant.Rows[rowindex].Cells[2].Text.Trim().ToString();
            Writeup = GridViewSearchGrant.Rows[rowindex].Cells[3].Text.Trim().ToString();
            Status = GridViewSearchGrant.Rows[rowindex].Cells[4].Text.Trim().ToString();
            Session["TempPid"] = Sid;
            Session["TempStatus"] = Status;
           

            string user = Session["UserId"].ToString();
            User u1 = new User();
            Business obj = new Business();
            //u1 = obj.get_createdby(pid, Unit);
            //string createdby = u1.createdId;
            GrantData g1 = new GrantData();
            //g1 = obj.fnGrantData(pid, Unit, user);
            string createdby = g1.CreatedBy;
          
        }

    }

    public void GridViewSearchGrant_OnRowedit(Object sender, GridViewEditEventArgs e)
    {
        GridViewSearchGrant.EditIndex = e.NewEditIndex;
        fnRecordExist(sender, e);

    }
    //Function to Select  Grant Data
    public void fnRecordExist(object sender, EventArgs e)
    {

        cleardata();
        string SID = Session["TempPid"].ToString();
        string CreatedBy = Session["UserId"].ToString();
        string InstUser =  Session["InstituteId"].ToString(); 
        string DeptUser = Session["Department"].ToString();


        SeedMoney v = new SeedMoney();
        SeedMoney v1 = new SeedMoney();
        SeedMoney v2 = new SeedMoney();
        Business obj = new Business();
         Business B = new Business();
        v = obj.fnfindSeedid(SID);
        panelSearchPub.Visible = true;
        txtid.Text = SID;
        txttiltle.Text = v.Title;
        txtwriteup.Text = v.Writeup;
        BtnSaveUpdate.Enabled = true;
       string status = v.Status;
       Label6.Visible = true;
       txtstatus.Visible = true;
       txtstatus.Text = v.Status;
       TextRemarks.Text = v.Remarks;
       Txtapprovedbudget.Text =Convert.ToString( v.ApprovedBudget);
       string data = null;
       if (v.Status != "")
       {
           data = B.GetStatusName(v.Status);
           txtstatus.Text = data;
       }
         
           Panel1.Visible = true;
           //PanelRemark.Visible = true;
           lblAPPremarks.Visible = true;
           TextRemarks.Visible = true;
           panAddAuthor.Enabled = false;
           string Entrytype1 = v.Entrytype;
           Session["Entrytype"] = Entrytype1;
          
           if (v.Entrytype == "S")
           {
               lblstudentapp.Visible = true;
               lblfacultyapp.Visible = false;            
               txttiltle.Enabled = true;
               DropDownbudget.Enabled = true;            
               lnkSeedMoneyStudent.Visible = true;
               lnkSeedMoneyFaculty.Visible = false;
               DropDownbudget.Items.Clear();
               DropDownbudget.Items.Add(new ListItem("Select", "0", true));
               SqlDataSourceBudget.SelectCommand = "SELECT [BudgetId], [Amount] FROM [SeedMoneyBudget_M] where Type='S'";
               DropDownbudget.DataSourceID = "SqlDataSourceBudget";
               DropDownbudget.DataBind();
           }
           else
           {
               lblstudentapp.Visible = false;
               lblfacultyapp.Visible = true;
               txttiltle.Enabled = true;
               DropDownbudget.Enabled = true;
               lnkSeedMoneyStudent.Visible = false;
               lnkSeedMoneyFaculty.Visible = true;
               DropDownbudget.Items.Clear();
               DropDownbudget.Items.Add(new ListItem("Select", "0", true));
               SqlDataSourceBudget.SelectCommand = "SELECT [BudgetId], [Amount] FROM [SeedMoneyBudget_M] where Type='F'";
               DropDownbudget.DataSourceID = "SqlDataSourceBudget";
               DropDownbudget.DataBind();
           }
           
        if (v.AppliedDate.ToShortDateString() != "01/01/0001")
        {
            txtdate.Text = v.AppliedDate.ToShortDateString();
        }
        double Sbudget = Convert.ToDouble(v.Budget);
        //Txtappliedbudget.Text = Sbudget.ToString();
        DropDownbudget.SelectedItem.Text = Sbudget.ToString();
        string FileUpload = "";
        Business b1 = new Business();
        FileUpload = b1.GetFileUploadPathSeedMoney(txtid.Text);
        if (Session["Role"].ToString() == "2")
        {
            if (FileUpload != "")
            {
                PanelFileUpload.Visible = true;
                GVViewFile.Visible = true;
                GVViewFile.Enabled = true;
                Labeluploadedfiles.Visible = true;
                LabelNote.Visible = false;
                LabelUploadPfd.Visible = false;
                FileUploadPdf.Visible = false;
                BtnUpload.Visible = false;
                LabelNote2.Visible = false;
              
            }
            else
            {
                LabelNote.Visible = true;
                GVViewFile.Visible = false;
                Labeluploadedfiles.Visible = false;
                LabelUploadPfd.Visible = false;
                FileUploadPdf.Visible = false;
                BtnUpload.Visible = false;
                LabelNote2.Visible = false;
             
            }
            DSforgridview.SelectParameters.Clear();
            DSforgridview.SelectParameters.Add("ID", txtid.Text);

            DSforgridview.SelectCommand = "select UploadFilePath from SeedMoneyDetails where ID=@ID";
            DSforgridview.DataBind();
            GVViewFile.DataBind();
        }
        else
                if (FileUpload != "")
                {
                    PanelFileUpload.Visible = true;
                    GVViewFile.Visible = true;
                    GVViewFile.Enabled = true;
                    Labeluploadedfiles.Visible = true;
                    LabelNote.Visible = false;
                    LabelUploadPfd.Visible = true;
                    FileUploadPdf.Visible = true;
                    BtnUpload.Visible = true;
                    LabelNote2.Visible = true;
                 
                    LabelNote2.Visible = true;
                }
                else
                {
                    PanelFileUpload.Visible = true;
                 
                    GVViewFile.Visible = false;
                    LabelNote2.Visible = true;
                    Labeluploadedfiles.Visible = false;
                  
                }
        DSforgridview.SelectParameters.Clear();
        DSforgridview.SelectParameters.Add("ID", txtid.Text);

        DSforgridview.SelectCommand = "select UploadFilePath from SeedMoneyDetails where ID=@ID";
                DSforgridview.DataBind();
                GVViewFile.DataBind();
            






        DataTable dy = obj.fnfindseedMoneyInvestigatorDetails(SID);
        ViewState["CurrentTable"] = dy;
        Grid_AuthorEntry.DataSource = dy;
        Grid_AuthorEntry.DataBind();
        Grid_AuthorEntry.Visible = true;
        panAddAuthor.Visible = true;
        int rowIndex = 0;

        DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
        DataRow drCurrentRow = null;
        if (dtCurrentTable.Rows.Count > 0)
        {
            for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
            {
                DropDownList DropdownMuNonMu = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].FindControl("DropdownMuNonMu");
                TextBox EmployeeCode = (TextBox)Grid_AuthorEntry.Rows[rowIndex].FindControl("EmployeeCode");
                TextBox AuthorName = (TextBox)Grid_AuthorEntry.Rows[rowIndex].FindControl("AuthorName");
                ImageButton EmployeeCodeBtnimg = (ImageButton)Grid_AuthorEntry.Rows[rowIndex].FindControl("EmployeeCodeBtn");

                TextBox InstNme = (TextBox)Grid_AuthorEntry.Rows[rowIndex].FindControl("InstitutionName");
                TextBox deptname = (TextBox)Grid_AuthorEntry.Rows[rowIndex].FindControl("DepartmentName");
                HiddenField InstId = (HiddenField)Grid_AuthorEntry.Rows[rowIndex].FindControl("Institution");
                HiddenField deptId = (HiddenField)Grid_AuthorEntry.Rows[rowIndex].FindControl("Department");
                TextBox MailId = (TextBox)Grid_AuthorEntry.Rows[rowIndex].FindControl("MailId");
                DropDownList isCorrAuth = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].FindControl("isCorrAuth");
                DropDownList AuthorType = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].FindControl("AuthorType");
                DropDownList isLeadPI = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].FindControl("isLeadPI");

                DropDownList NationalType = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].FindControl("NationalType");
                DropDownList ContinentId = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].FindControl("ContinentId");

                DropDownList DropdownStudentInstitutionName = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].FindControl("DropdownStudentInstitutionName");
                DropDownList DropdownStudentDepartmentName = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].FindControl("DropdownStudentDepartmentName");
                ImageButton ImageButton1 = (ImageButton)Grid_AuthorEntry.Rows[rowIndex].FindControl("ImageButton1");

                drCurrentRow = dtCurrentTable.NewRow();

                DropdownMuNonMu.Text = dtCurrentTable.Rows[i - 1]["DropdownMuNonMu"].ToString();
                EmployeeCode.Text = dtCurrentTable.Rows[i - 1]["EmployeeCode"].ToString();
                AuthorName.Text = dtCurrentTable.Rows[i - 1]["AuthorName"].ToString();

                if (DropdownMuNonMu.Text == "N")
                {
                    InstNme.Visible = true;
                    deptname.Visible = true;
                    DropdownStudentInstitutionName.Visible = false;
                    DropdownStudentDepartmentName.Visible = false;
                    NationalType.Visible = true;
                    EmployeeCode.Enabled = false;

                    NationalType.SelectedValue = dtCurrentTable.Rows[i - 1]["NationalType"].ToString();

                    if (NationalType.SelectedValue == "N")
                    {
                        ContinentId.Visible = false;
                    }
                    else
                    {
                        ContinentId.Visible = true;
                    }
                    ContinentId.SelectedValue = dtCurrentTable.Rows[i - 1]["ContinentId"].ToString();


                    InstNme.Text = dtCurrentTable.Rows[i - 1]["InstitutionName"].ToString();
                    deptname.Text = dtCurrentTable.Rows[i - 1]["DepartmentName"].ToString();
                    InstId.Value = dtCurrentTable.Rows[i - 1]["Institution"].ToString();
                    deptId.Value = dtCurrentTable.Rows[i - 1]["Department"].ToString();
                    ImageButton1.Visible = true;
                    ImageButton1.Enabled = false;
                    EmployeeCodeBtnimg.Visible = false;
                }

                else if (DropdownMuNonMu.Text == "O")
                {
                    DropdownStudentInstitutionName.Visible = true;
                    DropdownStudentDepartmentName.Visible = true;
                    InstNme.Visible = false;
                    deptname.Visible = false;
                    AuthorName.Enabled = false;
                    DropdownStudentInstitutionName.SelectedValue = dtCurrentTable.Rows[i - 1]["Institution"].ToString();
                    DropdownStudentDepartmentName.SelectedValue = dtCurrentTable.Rows[i - 1]["Department"].ToString();
                    EmployeeCode.Text = dtCurrentTable.Rows[i - 1]["EmployeeCode"].ToString();
                    MailId.Enabled = true;
                    NationalType.Visible = false;
                    ContinentId.Visible = false;
                    ImageButton1.Visible = true;
                    ImageButton1.Enabled = false;
                    EmployeeCodeBtnimg.Visible = false;
                }
                else if (DropdownMuNonMu.Text == "S")
                {

                    DropdownStudentInstitutionName.Visible = false;
                    DropdownStudentDepartmentName.Visible = false;
                    InstNme.Visible = true;
                    deptname.Visible = true;
                    InstNme.Text = dtCurrentTable.Rows[i - 1]["InstitutionName"].ToString();
                    deptname.Text = dtCurrentTable.Rows[i - 1]["DepartmentName"].ToString();
                    InstId.Value = dtCurrentTable.Rows[i - 1]["Institution"].ToString();
                    deptId.Value = dtCurrentTable.Rows[i - 1]["Department"].ToString();
                    EmployeeCodeBtnimg.Visible = false;
                    EmployeeCode.Enabled = false;
                    NationalType.Visible = false;
                    ContinentId.Visible = false;
                    ImageButton1.Visible = true;
                    ImageButton1.Enabled = true;
                    EmployeeCodeBtnimg.Visible = false;
                }
                else if (DropdownMuNonMu.Text == "M")
                {

                    DropdownStudentInstitutionName.Visible = false;
                    DropdownStudentDepartmentName.Visible = false;
                    InstNme.Visible = true;
                    deptname.Visible = true;
                    InstNme.Text = dtCurrentTable.Rows[i - 1]["InstitutionName"].ToString();
                    deptname.Text = dtCurrentTable.Rows[i - 1]["DepartmentName"].ToString();
                    InstId.Value = dtCurrentTable.Rows[i - 1]["Institution"].ToString();
                    deptId.Value = dtCurrentTable.Rows[i - 1]["Department"].ToString();
                    EmployeeCodeBtnimg.Visible = false;
                    EmployeeCode.Enabled = false;
                    NationalType.Visible = false;
                    ContinentId.Visible = false;
                    ImageButton1.Visible = true;
                    ImageButton1.Enabled = true;
                    EmployeeCodeBtnimg.Visible = false;
                }

                MailId.Text = dtCurrentTable.Rows[i - 1]["MailId"].ToString();
                AuthorType.Text = dtCurrentTable.Rows[i - 1]["AuthorType"].ToString();
                isLeadPI.Text = dtCurrentTable.Rows[i - 1]["isLeadPI"].ToString();
                //isCorrAuth.Text = dtCurrentTable.Rows[i - 1]["isCorrAuth"].ToString();
                //NameAsInJournal.Text = dtCurrentTable.Rows[i - 1]["NameInJournal"].ToString();

                //IsPresenter.SelectedValue = dtCurrentTable.Rows[i - 1]["IsPresenter"].ToString();

               
                if (DropdownMuNonMu.Text == "N")
                {
                    EmployeeCodeBtnimg.Enabled = false;
                    AuthorName.Enabled = true;
                    InstNme.Enabled = true;
                    deptname.Enabled = true;
                    MailId.Enabled = true;
                    EmployeeCode.Enabled = false;

                }
                else if (DropdownMuNonMu.Text == "M")
                {
                    EmployeeCodeBtnimg.Enabled = true;
                    AuthorName.Enabled = false;
                    InstNme.Enabled = false;
                    deptname.Enabled = false;
                    MailId.Enabled = false;


                }
                else if (DropdownMuNonMu.Text == "O")
                {

                    DropdownStudentInstitutionName.Enabled = true;
                    DropdownStudentInstitutionName.Enabled = true;
                    NationalType.Visible = false;
                    ContinentId.Visible = false;
                    MailId.Enabled = true;
                    AuthorName.Enabled = true;
                    EmployeeCode.Enabled = true;
                }
                else if (DropdownMuNonMu.Text == "S")
                {         
                    MailId.Enabled = true;
                }
                else
                {
                    MailId.Enabled = true;
                }


                //Grid_AuthorEntry.Columns[13].Visible = true;
                rowIndex++;

            }


            ViewState["CurrentTable"] = dtCurrentTable;
        }
      

        setModalWindow(sender, e);
        }
    //Function to Add authors
    protected void addRow(object sender, EventArgs e)
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

                    DropDownList DropdownMuNonMu = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].FindControl("DropdownMuNonMu");
                    TextBox AuthorName = (TextBox)Grid_AuthorEntry.Rows[rowIndex].FindControl("AuthorName");
                    ImageButton EmployeeCodeBtnImg = (ImageButton)Grid_AuthorEntry.Rows[rowIndex].FindControl("EmployeeCodeBtn");
                    TextBox EmployeeCode = (TextBox)Grid_AuthorEntry.Rows[rowIndex].FindControl("EmployeeCode");
                    HiddenField Institution = (HiddenField)Grid_AuthorEntry.Rows[rowIndex].FindControl("Institution");
                    TextBox InstitutionName = (TextBox)Grid_AuthorEntry.Rows[rowIndex].FindControl("InstitutionName");
                    HiddenField Department = (HiddenField)Grid_AuthorEntry.Rows[rowIndex].FindControl("Department");
                    TextBox DepartmentName = (TextBox)Grid_AuthorEntry.Rows[rowIndex].FindControl("DepartmentName");

                    DropDownList DropdownStudentInstitutionName = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].FindControl("DropdownStudentInstitutionName");
                    DropDownList DropdownStudentDepartmentName = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].FindControl("DropdownStudentDepartmentName");

                    TextBox MailId = (TextBox)Grid_AuthorEntry.Rows[rowIndex].FindControl("MailId");
                    DropDownList AuthorType = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].FindControl("AuthorType");
                    DropDownList isLeadPI = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].FindControl("isLeadPI");
                   

                    DropDownList NationalType = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].FindControl("NationalType");
                    DropDownList ContinentId = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].FindControl("ContinentId");
                    ImageButton ImageButton1 = (ImageButton)Grid_AuthorEntry.Rows[rowIndex].FindControl("ImageButton1");

                    drCurrentRow = dtCurrentTable.NewRow();
                    dtCurrentTable.Rows[i - 1]["DropdownMuNonMu"] = DropdownMuNonMu.Text;
                    dtCurrentTable.Rows[i - 1]["AuthorName"] = AuthorName.Text;
                    dtCurrentTable.Rows[i - 1]["EmployeeCode"] = EmployeeCode.Text;
                    dtCurrentTable.Rows[i - 1]["MailId"] = MailId.Text;
                    dtCurrentTable.Rows[i - 1]["AuthorType"] = AuthorType.Text;
                  

                    if (DropdownMuNonMu.SelectedValue == "S")
                    {
                        dtCurrentTable.Rows[i - 1]["Institution"] = Institution.Value;
                        dtCurrentTable.Rows[i - 1]["InstitutionName"] = InstitutionName.Text;
                        dtCurrentTable.Rows[i - 1]["Department"] = Department.Value;
                        dtCurrentTable.Rows[i - 1]["DepartmentName"] = DepartmentName.Text;
                    }
                    else if (DropdownMuNonMu.SelectedValue == "O")
                    {
                        dtCurrentTable.Rows[i - 1]["Institution"] = DropdownStudentInstitutionName.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["InstitutionName"] = DropdownStudentInstitutionName.SelectedItem;
                        dtCurrentTable.Rows[i - 1]["Department"] = DropdownStudentDepartmentName.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["DepartmentName"] = DropdownStudentDepartmentName.SelectedItem;
                        dtCurrentTable.Rows[i - 1]["NationalType"] = NationalType.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["ContinentId"] = ContinentId.SelectedValue;
                    }
                    else if (DropdownMuNonMu.SelectedValue == "N")
                    {
                        dtCurrentTable.Rows[i - 1]["Institution"] = Institution.Value;
                        dtCurrentTable.Rows[i - 1]["InstitutionName"] = InstitutionName.Text;
                        dtCurrentTable.Rows[i - 1]["Department"] = Department.Value;
                        dtCurrentTable.Rows[i - 1]["DepartmentName"] = DepartmentName.Text;
                        dtCurrentTable.Rows[i - 1]["NationalType"] = NationalType.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["ContinentId"] = ContinentId.SelectedValue;

                    }
                    else if (DropdownMuNonMu.SelectedValue == "M")
                    {
                        dtCurrentTable.Rows[i - 1]["Institution"] = Institution.Value;
                        dtCurrentTable.Rows[i - 1]["InstitutionName"] = InstitutionName.Text;
                        dtCurrentTable.Rows[i - 1]["Department"] = Department.Value;
                        dtCurrentTable.Rows[i - 1]["DepartmentName"] = DepartmentName.Text;
                    }

                    rowIndex++;
                }

                dtCurrentTable.Rows.Add(drCurrentRow);

                ViewState["CurrentTable"] = dtCurrentTable;

                Grid_AuthorEntry.DataSource = dtCurrentTable;
                Grid_AuthorEntry.DataBind();

            }
            SetPreviousData();

        }
        else
        {
            Response.Write("ViewState is null");
        }

        setModalWindow(sender, e); // initialise popup gridviews

    }
    //Investigator
    protected void setModalWindow(object sender, EventArgs e)
    {
        popup.Visible = true;
        //popupPanelAffilUpdate.Update();
        popGridAffil.DataSourceID = "SqlDataSourceAffil";
        SqlDataSourceAffil.DataBind();
        popGridAffil.DataBind();
        popupStudentGrid.DataSourceID = "StudentSQLDS";
        StudentSQLDS.DataBind();
        popupStudentGrid.DataBind();
    }
    protected void SetInitialRow()
    {
        DataTable dt = new DataTable();
        DataRow dr = null;
        dt.Columns.Add(new DataColumn("DropdownMuNonMu", typeof(string)));
        dt.Columns.Add(new DataColumn("Author", typeof(string)));
        dt.Columns.Add(new DataColumn("AuthorName", typeof(string)));
        dt.Columns.Add(new DataColumn("EmployeeCode", typeof(string)));
        dt.Columns.Add(new DataColumn("Institution", typeof(string)));
        dt.Columns.Add(new DataColumn("InstitutionName", typeof(string)));
        dt.Columns.Add(new DataColumn("Department", typeof(string)));
        dt.Columns.Add(new DataColumn("DepartmentName", typeof(string)));
        dt.Columns.Add(new DataColumn("MailId", typeof(string)));
        dt.Columns.Add(new DataColumn("AuthorType", typeof(string)));
        dt.Columns.Add(new DataColumn("isLeadPI", typeof(string)));
        dt.Columns.Add(new DataColumn("NationalType", typeof(string)));
        dt.Columns.Add(new DataColumn("ContinentId", typeof(string)));

        dr = dt.NewRow();

        dr["DropdownMuNonMu"] = string.Empty;
        dr["Author"] = string.Empty;
        dr["AuthorName"] = string.Empty;
        dr["EmployeeCode"] = string.Empty;
        dr["Institution"] = string.Empty;
        dr["InstitutionName"] = string.Empty;
        dr["Department"] = string.Empty;
        dr["DepartmentName"] = string.Empty;
        dr["MailId"] = string.Empty;
        dr["AuthorType"] = string.Empty;
        dr["isLeadPI"] = string.Empty;
        dr["NationalType"] = string.Empty;
        dr["ContinentId"] = string.Empty;
        dt.Rows.Add(dr);

        ViewState["CurrentTable"] = dt;
        Grid_AuthorEntry.DataSource = dt;
        Grid_AuthorEntry.DataBind();
        TextBox AuthorName = (TextBox)Grid_AuthorEntry.Rows[0].Cells[1].FindControl("AuthorName");
        ImageButton EmployeeCodeBtn = (ImageButton)Grid_AuthorEntry.Rows[0].Cells[1].FindControl("EmployeeCodeBtn");
        DropDownList DropdownMuNonMu = (DropDownList)Grid_AuthorEntry.Rows[0].Cells[2].FindControl("DropdownMuNonMu");

        TextBox EmployeeCode = (TextBox)Grid_AuthorEntry.Rows[0].Cells[2].FindControl("EmployeeCode");
        HiddenField Institution = (HiddenField)Grid_AuthorEntry.Rows[0].Cells[0].FindControl("Institution");
        TextBox InstitutionName = (TextBox)Grid_AuthorEntry.Rows[0].Cells[6].FindControl("InstitutionName");
        HiddenField Department = (HiddenField)Grid_AuthorEntry.Rows[0].Cells[0].FindControl("Department");
        TextBox DepartmentName = (TextBox)Grid_AuthorEntry.Rows[0].Cells[0].FindControl("DepartmentName");
        TextBox MailId = (TextBox)Grid_AuthorEntry.Rows[0].Cells[0].FindControl("MailId");
        DropDownList AuthorType = (DropDownList)Grid_AuthorEntry.Rows[0].Cells[0].FindControl("AuthorType");

        DropDownList isLeadPI = (DropDownList)Grid_AuthorEntry.Rows[0].Cells[0].FindControl("isLeadPI");
        DropDownList NationalType = (DropDownList)Grid_AuthorEntry.Rows[0].Cells[0].FindControl("NationalType");
        DropDownList ContinentId = (DropDownList)Grid_AuthorEntry.Rows[0].Cells[0].FindControl("ContinentId");
       
        DropdownMuNonMu.Enabled = false;
        NationalType.Visible = false;
        ContinentId.Visible = false;

        AuthorName.Text = Session["UserName"].ToString();
        AuthorName.Enabled = false;
        EmployeeCode.Text = Session["UserId"].ToString();
        EmployeeCode.Enabled = false;

        Institution.Value = Session["InstituteId"].ToString();
        string InstituteId = Session["InstituteId"].ToString();
        Business b = new Business();
        if (Session["Role"].ToString() == "21")
        {
            string InstituteName1 = null;
            InstituteName1 = Session["InstituteName"].ToString(); ;
            InstitutionName.Text = InstituteName1;
            Department.Value = Session["DepartmentId"].ToString();
            //InstitutionName.Text = Session["InstituteId"].ToString();
            DepartmentName.Text = Session["Department"].ToString();
            if ((Session["emailId"]) != "" && (Session["emailId"]) != null)
            {
                MailId.Text = Session["emailId"].ToString();
                MailId.Enabled = true;
            }
            MailId.Enabled = true;
        }
        else
       
        {
            string InstituteName1 = null;
            InstituteName1 = b.GetInstitutionName(InstituteId);
            InstitutionName.Text = InstituteName1;

            Department.Value = Session["Department"].ToString();

            //Institution.Enabled = false;
            string deptId = Session["Department"].ToString();

            string deptName1 = null;
            deptName1 = b.GetDeptName(deptId, InstituteId);
            DepartmentName.Text = deptName1;

            MailId.Text = Session["emailId"].ToString();
            MailId.Enabled = false;
        }
        

        if (DropdownMuNonMu.SelectedValue == "M")
        {
            EmployeeCodeBtn.Enabled = false;
        }
        else if (DropdownMuNonMu.SelectedValue == "N")
        {
            EmployeeCodeBtn.Enabled = false;
        }

        else if (DropdownMuNonMu.SelectedValue == "S")
        {
            EmployeeCodeBtn.Enabled = false;
        }
    }
    //set previous row author 

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
                    DropDownList DropdownMuNonMu = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].FindControl("DropdownMuNonMu");
                    TextBox AuthorName = (TextBox)Grid_AuthorEntry.Rows[rowIndex].FindControl("AuthorName");
                    ImageButton EmployeeCodeBtnImg = (ImageButton)Grid_AuthorEntry.Rows[rowIndex].FindControl("EmployeeCodeBtn");
                    TextBox EmployeeCode = (TextBox)Grid_AuthorEntry.Rows[rowIndex].FindControl("EmployeeCode");
                    HiddenField Institution = (HiddenField)Grid_AuthorEntry.Rows[rowIndex].FindControl("Institution");
                    TextBox InstitutionName = (TextBox)Grid_AuthorEntry.Rows[rowIndex].FindControl("InstitutionName");
                    HiddenField Department = (HiddenField)Grid_AuthorEntry.Rows[rowIndex].FindControl("Department");
                    TextBox DepartmentName = (TextBox)Grid_AuthorEntry.Rows[rowIndex].FindControl("DepartmentName");

                    DropDownList DropdownStudentInstitutionName = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].FindControl("DropdownStudentInstitutionName");
                    DropDownList DropdownStudentDepartmentName = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].FindControl("DropdownStudentDepartmentName");

                    TextBox MailId = (TextBox)Grid_AuthorEntry.Rows[rowIndex].FindControl("MailId");
                    DropDownList isCorrAuth = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].FindControl("isCorrAuth");
                    DropDownList AuthorType = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].FindControl("AuthorType");
                    DropDownList isLeadPI = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("isLeadPI");
                 

                    DropDownList NationalType = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].FindControl("NationalType");
                    DropDownList ContinentId = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].FindControl("ContinentId");
                    ImageButton ImageButton1 = (ImageButton)Grid_AuthorEntry.Rows[rowIndex].FindControl("ImageButton1");

                    DropdownMuNonMu.Text = dt.Rows[i]["DropdownMuNonMu"].ToString();
                    AuthorName.Text = dt.Rows[i]["AuthorName"].ToString();
                    EmployeeCode.Text = dt.Rows[i]["EmployeeCode"].ToString();
                    MailId.Text = dt.Rows[i]["MailId"].ToString();
                    AuthorType.Text = dt.Rows[i]["AuthorType"].ToString();
                   
                    if (DropdownMuNonMu.SelectedValue == "S")
                    {
                        Institution.Value = dt.Rows[i]["Institution"].ToString();
                        InstitutionName.Text = dt.Rows[i]["InstitutionName"].ToString();
                        Department.Value = dt.Rows[i]["Department"].ToString();
                        DepartmentName.Text = dt.Rows[i]["DepartmentName"].ToString();
                        AuthorName.Enabled = false;
                        InstitutionName.Enabled = false;
                        DepartmentName.Enabled = false;
                        if (MailId.Text != "")
                        {
                            MailId.Enabled = false;
                        }
                        else
                        {
                            MailId.Enabled = true;
                        }
                        DropdownStudentInstitutionName.Visible = false;
                        DropdownStudentDepartmentName.Visible = false;
                        InstitutionName.Visible = true;
                        DepartmentName.Visible = true;
                        EmployeeCodeBtnImg.Enabled = true;
                        EmployeeCode.Enabled = false;
                        NationalType.Visible = false;
                        ContinentId.Visible = false;
                    }
                    else if (DropdownMuNonMu.SelectedValue == "M")
                    {
                        Institution.Value = dt.Rows[i]["Institution"].ToString();
                        InstitutionName.Text = dt.Rows[i]["InstitutionName"].ToString();
                        Department.Value = dt.Rows[i]["Department"].ToString();
                        DepartmentName.Text = dt.Rows[i]["DepartmentName"].ToString();
                        AuthorName.Enabled = false;
                        InstitutionName.Enabled = false;
                        DepartmentName.Enabled = false;
                        if (MailId.Text != "")
                        {
                            MailId.Enabled = false;
                        }
                        else
                        {
                            MailId.Enabled = true;
                        }
                        DropdownStudentInstitutionName.Visible = false;
                        DropdownStudentDepartmentName.Visible = false;
                        InstitutionName.Visible = true;
                        DepartmentName.Visible = true;
                        EmployeeCodeBtnImg.Enabled = true;
                        EmployeeCode.Enabled = false;
                        NationalType.Visible = false;
                        ContinentId.Visible = false;
                    }
                    else if (DropdownMuNonMu.SelectedValue == "O")
                    {
                        Institution.Value = dt.Rows[i]["Institution"].ToString();
                        DropdownStudentInstitutionName.SelectedValue = dt.Rows[i]["Institution"].ToString();
                        Department.Value = dt.Rows[i]["Department"].ToString();
                        DropdownStudentDepartmentName.SelectedValue = dt.Rows[i]["Department"].ToString();
                        AuthorName.Enabled = true;
                        InstitutionName.Enabled = true;
                        DepartmentName.Enabled = true;
                        MailId.Enabled = true;
                        DropdownStudentInstitutionName.Visible = true;
                        DropdownStudentDepartmentName.Visible = true;
                        InstitutionName.Visible = false;
                        DepartmentName.Visible = false;
                        NationalType.Visible = false;
                        ContinentId.Visible = false;
                        EmployeeCodeBtnImg.Enabled = false;
                        EmployeeCode.Enabled = true;
                    }
                    else if (DropdownMuNonMu.SelectedValue == "N")
                    {
                        Institution.Value = dt.Rows[i]["Institution"].ToString();
                        InstitutionName.Text = dt.Rows[i]["InstitutionName"].ToString();
                        Department.Value = dt.Rows[i]["Department"].ToString();
                        DepartmentName.Text = dt.Rows[i]["DepartmentName"].ToString();
                        NationalType.Text = dt.Rows[i]["NationalType"].ToString();
                        ContinentId.Text = dt.Rows[i]["ContinentId"].ToString();
                        AuthorName.Enabled = true;
                        InstitutionName.Enabled = true;
                        DepartmentName.Enabled = true;
                        MailId.Enabled = true;
                        DropdownStudentInstitutionName.Visible = false;
                        DropdownStudentDepartmentName.Visible = false;
                        InstitutionName.Visible = true;
                        DepartmentName.Visible = true;
                        NationalType.Visible = true;
                        if (NationalType.Text == "I")
                        {
                            ContinentId.Visible = true;
                        }
                        else
                        {
                            ContinentId.Visible = false;
                        }
                        EmployeeCodeBtnImg.Enabled = false;
                        EmployeeCode.Enabled = false;
                    }
                    if (i != 0)
                    {
                        DropDownList DropdownMuNonM8 = (DropDownList)Grid_AuthorEntry.Rows[rowIndex - 1].Cells[2].FindControl("DropdownMuNonMu");
                        DropdownMuNonM8.Enabled = false;
                    }
                    rowIndex++;
                }
            }
        }
    }
    //Textbox author name changed
    protected void AuthorName_Changed(object sender, EventArgs e)
    {
        GridViewRow currentRow = (GridViewRow)((TextBox)sender).Parent.Parent;
        TextBox AuthorName = (TextBox)currentRow.FindControl("AuthorName");
        //  TextBox NameInJournal = (TextBox)currentRow.FindControl("NameInJournal");
        TextBox InstitutionName = (TextBox)currentRow.FindControl("InstitutionName");
        InstitutionName.Focus();
    }



    //On Row delete of autor grdiview
    protected void Grid_AuthorEntry_RowDeleting(Object sender, GridViewDeleteEventArgs e)
    {

        SetRowData();
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            DataRow drCurrentRow = null;
            int rowIndex = Convert.ToInt32(e.RowIndex);
            if (dt.Rows.Count > 1 && rowIndex != 0)
            {
                dt.Rows.Remove(dt.Rows[rowIndex]);
                drCurrentRow = dt.NewRow();
                ViewState["CurrentTable"] = dt;
                Grid_AuthorEntry.DataSource = dt;
                Grid_AuthorEntry.DataBind();
                SetPreviousData();
            }
        }
    }

    private void SetRowData()
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

                    TextBox AuthorName = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[1].FindControl("AuthorName");
                    ImageButton EmployeeCodeBtnImg = (ImageButton)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("EmployeeCodeBtn");

                    DropDownList DropdownMuNonMu = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[2].FindControl("DropdownMuNonMu");
                    //TextBox amount = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[3].FindControl("amount");
                    TextBox EmployeeCode = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("EmployeeCode");
                    HiddenField Institution = (HiddenField)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("Institution");
                    TextBox InstitutionName = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[6].FindControl("InstitutionName");
                    HiddenField Department = (HiddenField)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("Department");
                    TextBox DepartmentName = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("DepartmentName");
                    TextBox MailId = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("MailId");

                    //  DropDownList isCorrAuth = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("isCorrAuth");
                    DropDownList AuthorType = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("AuthorType");
                    DropDownList isLeadPI = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("isLeadPI");
                    DropDownList DropdownStudentInstitutionName = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("DropdownStudentInstitutionName");
                    DropDownList DropdownStudentDepartmentName = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("DropdownStudentDepartmentName");

                    DropDownList NationalType = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("NationalType");
                    DropDownList ContinentId = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("ContinentId");

                    ImageButton EmployeeCodeBtnImg1 = (ImageButton)Grid_AuthorEntry.Rows[0].Cells[0].FindControl("EmployeeCodeBtn");

                    drCurrentRow = dtCurrentTable.NewRow();
                    dtCurrentTable.Rows[i - 1]["DropdownMuNonMu"] = DropdownMuNonMu.Text;
                    dtCurrentTable.Rows[i - 1]["AuthorName"] = AuthorName.Text;
                    dtCurrentTable.Rows[i - 1]["EmployeeCode"] = EmployeeCode.Text;

                    if (DropdownMuNonMu.Text == "M")
                    {
                        DropdownStudentInstitutionName.Visible = false;
                        DropdownStudentDepartmentName.Visible = false;
                        InstitutionName.Visible = true;
                        DepartmentName.Visible = true;

                        NationalType.Visible = false;
                        ContinentId.Visible = false;

                        EmployeeCodeBtnImg.Enabled = true;

                        dtCurrentTable.Rows[i - 1]["NationalType"] = NationalType.Text;
                        dtCurrentTable.Rows[i - 1]["ContinentId"] = ContinentId.Text;

                        dtCurrentTable.Rows[i - 1]["Institution"] = Institution.Value;
                        dtCurrentTable.Rows[i - 1]["InstitutionName"] = InstitutionName.Text;
                        dtCurrentTable.Rows[i - 1]["Department"] = Department.Value;
                        dtCurrentTable.Rows[i - 1]["DepartmentName"] = DepartmentName.Text;
                    }
                    else if (DropdownMuNonMu.Text == "N")
                    {
                        DropdownStudentInstitutionName.Visible = false;
                        DropdownStudentDepartmentName.Visible = false;
                        InstitutionName.Visible = true;
                        DepartmentName.Visible = true;

                        EmployeeCodeBtnImg.Enabled = false;

                        NationalType.Visible = true;
                        if (NationalType.Text == "I")
                        {
                            ContinentId.Visible = true;
                        }
                        else
                        {
                            ContinentId.Visible = false;
                        }

                        dtCurrentTable.Rows[i - 1]["NationalType"] = NationalType.Text;
                        dtCurrentTable.Rows[i - 1]["ContinentId"] = ContinentId.Text;

                        dtCurrentTable.Rows[i - 1]["Institution"] = Institution.Value;
                        dtCurrentTable.Rows[i - 1]["InstitutionName"] = InstitutionName.Text;
                        dtCurrentTable.Rows[i - 1]["Department"] = Department.Value;
                        dtCurrentTable.Rows[i - 1]["DepartmentName"] = DepartmentName.Text;
                    }
                    else if (DropdownMuNonMu.Text == "S")
                    {
                        DropdownStudentInstitutionName.Visible = true;
                        DropdownStudentDepartmentName.Visible = true;
                        InstitutionName.Visible = false;
                        DepartmentName.Visible = false;

                        NationalType.Visible = false;
                        ContinentId.Visible = false;

                        dtCurrentTable.Rows[i - 1]["NationalType"] = NationalType.Text;
                        dtCurrentTable.Rows[i - 1]["ContinentId"] = ContinentId.Text;

                        EmployeeCodeBtnImg.Enabled = false;

                        dtCurrentTable.Rows[i - 1]["Institution"] = DropdownStudentInstitutionName.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["Department"] = DropdownStudentDepartmentName.SelectedValue;
                    }

                    EmployeeCodeBtnImg1.Enabled = false;
                    dtCurrentTable.Rows[i - 1]["MailId"] = MailId.Text;
                    dtCurrentTable.Rows[i - 1]["AuthorType"] = AuthorType.Text;
                    dtCurrentTable.Rows[i - 1]["isLeadPI"] = isLeadPI.Text;
                    if (AuthorType.SelectedValue == "C")
                    {
                        isLeadPI.Enabled = false;
                    }
                    else
                    {
                        isLeadPI.Enabled = true;
                    }
                    rowIndex++;
                }

                ViewState["CurrentTable"] = dtCurrentTable;
            }
        }
        else
        {
            Response.Write("ViewState is null");
        }

    }
    protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    //Find the DropDownList in the Row
        //    DropDownList DropdownMuNonMu = (e.Row.FindControl("DropdownMuNonMu") as DropDownList);
        //}
    }
    //Drop Down MU/Non MU change
    protected void exit1(object sender, EventArgs e)
    {
        //popupPanelAffilUpdate.Update();
        affiliateSrch.Text = "";
        popGridAffil.DataBind();
    }
    protected void exit(object sender, EventArgs e)
    {
        txtSrchStudentName.Text = "";
        popupstudent.DataBind();
    }
    //protected void AuthorNameChanged(object sender, EventArgs e)
    //{
    //    if (affiliateSrch.Text.Trim() == "")
    //    {
    //        SqlDataSourceAffil.SelectCommand = "SELECT top 10  User_Id, prefix+' '+UPPER(firstname)+' '+UPPER(middlename)+' '+UPPER(lastname)  as Name from User_M";
    //        popGridAffil.DataBind();
    //        popGridAffil.Visible = true;
    //    }

    //    else
    //    {
    //        string name = affiliateSrch.Text.Replace(" ", String.Empty);
    //        SqlDataSourceAffil.SelectCommand = "SELECT  User_Id,prefix+' '+UPPER(firstname)+' '+UPPER(middlename)+' '+UPPER(lastname)  as Name from User_M where prefix+firstname+middlename+lastname like '%" + name + "%'";

    //        popGridAffil.DataBind();
    //        popGridAffil.Visible = true;
    //    }

    //    string rowVal = Request.Form["rowIndx"];
    //    int rowIndex = Convert.ToInt32(rowVal);

    //    ModalPopupExtender ModalPopupExtender8 = (ModalPopupExtender)Grid_AuthorEntry.Rows[rowIndex].FindControl("ModalPopupExtender4");
    //    ModalPopupExtender8.Show();

    //}
    protected void AuthorType_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow currentRow = (GridViewRow)((DropDownList)sender).Parent.Parent;

        DropDownList AuthorType = (DropDownList)currentRow.FindControl("AuthorType");
        DropDownList isLeadPI = (DropDownList)currentRow.FindControl("isLeadPI");

        int countAuthType = 0;
        for (int i = 0; i < Grid_AuthorEntry.Rows.Count; i++)
        {

            DropDownList AuthorType1 = (DropDownList)Grid_AuthorEntry.Rows[i].Cells[3].FindControl("AuthorType");
            DropDownList isLeadPI1 = (DropDownList)Grid_AuthorEntry.Rows[i].Cells[3].FindControl("isLeadPI");

            if (AuthorType.SelectedValue == "P")
            {
                countAuthType = countAuthType + 1;
            }

        }
        if ((AuthorType.SelectedValue == "P"))
        {

            if (countAuthType == 1)
            {

                isLeadPI.SelectedValue = "Y";

            }

            else
            {

                isLeadPI.SelectedValue = "N";

            }

        }

        if (AuthorType.SelectedValue == "C")
        {

            isLeadPI.Enabled = false;

            isLeadPI.SelectedValue = "N";

        }

        else
        {

            isLeadPI.Enabled = true;

        }

        //if (countAuthType == 1)
        //{
        //    isLeadPI.SelectedValue = "Y";
        //}
        //else
        //{
        //    isLeadPI.SelectedValue = "N";
        //}

        // if (AuthorType.SelectedValue == "C")
        //{
        //    isLeadPI.Enabled = false;
        //}
        //else
        //{
        //    isLeadPI.Enabled = true;
        //}

    }
    protected void NationalTypeOnSelectedIndexChanged(object sender, EventArgs e)
    {

        TextBox senderBox = sender as TextBox;

        GridViewRow currentRow = (GridViewRow)((DropDownList)sender).Parent.Parent;
        DropDownList DropdownMuNonMu = (DropDownList)currentRow.FindControl("DropdownMuNonMu");
        ImageButton EmployeeCodeBtn = (ImageButton)currentRow.FindControl("EmployeeCodeBtn");

        TextBox InstitutionName = (TextBox)currentRow.FindControl("InstitutionName");
        DropDownList DropdownStudentInstitutionName1 = (DropDownList)currentRow.FindControl("DropdownStudentInstitutionName");

        TextBox DepartmentName = (TextBox)currentRow.FindControl("DepartmentName");
        DropDownList DropdownStudentDepartmentName = (DropDownList)currentRow.FindControl("DropdownStudentDepartmentName");


        TextBox AuthorName = (TextBox)currentRow.FindControl("AuthorName");
        TextBox MailId = (TextBox)currentRow.FindControl("MailId");
        DropDownList NationalType = (DropDownList)currentRow.FindControl("NationalType");

        DropDownList ContinentId = (DropDownList)currentRow.FindControl("ContinentId");



        if (NationalType.SelectedValue == "I")
        {

            ContinentId.Visible = true;



        }
        else if (DropdownMuNonMu.SelectedValue == "N")
        {


            ContinentId.Visible = false;


        }
        else
        {
            ContinentId.Visible = false;

        }

    }
    protected void StudentDataSelect(Object sender, EventArgs e)
    {
        popupStudentGrid.Visible = true;
        GridViewRow row = popupStudentGrid.SelectedRow;

        string rollno = row.Cells[1].Text;
        string studentname = row.Cells[2].Text;
        string institution = row.Cells[3].Text;
        string classname = row.Cells[4].Text;
        string mailid = row.Cells[5].Text;


        string rowVal2 = rowVal.Value;
        int rowIndex = Convert.ToInt32(rowVal2);

        TextBox EmployeeCode = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("AuthorName");
        EmployeeCode.Text = studentname;

        TextBox InstitutionName = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("InstitutionName");
        InstitutionName.Text = institution;

        TextBox DepartmentName = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("DepartmentName");
        DepartmentName.Text = classname;

        TextBox MailId = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("MailId");
        if (mailid != "&nbsp;")
        {
            MailId.Text = mailid;
        }
        else
        {
            MailId.Text = "";
        }

        TextBox employc = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("EmployeeCode");
        employc.Text = rollno;


        HiddenField classcode = (HiddenField)popupStudentGrid.SelectedRow.FindControl("lblClassCode");

        HiddenField instnid = (HiddenField)popupStudentGrid.SelectedRow.FindControl("lblInstn");

        HiddenField Institution = (HiddenField)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("Institution");
        Institution.Value = instnid.Value;

        HiddenField Department = (HiddenField)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("Department");
        Department.Value = classcode.Value;
        popupStudentGrid.DataBind();

    }
   protected void SearchStudentData(object sender, EventArgs e)
    {
        popup.Visible = true;
        //PubEntriesUpdatePanel.Update();
        //EditUpdatePanel.Update();
        //popupPanelAffilUpdate.Update();
        //MainUpdate.Update();
        if (txtSrchStudentName.Text.Trim() == "" && txtSrchStudentRollNo.Text.Trim() == "" && StudentIntddl.SelectedValue == "")
        {
            StudentSQLDS.SelectCommand = "Select TOP 10  RollNo,Name,SISClass.ClassName as ClassName,SISInstitution.InstName as InstName,EmailID1,SISStudentGenInfo.ClassCode as ClassCode,SISInstnHR.HRInstitute as InstID from SISStudentGenInfo,SISClass,SISInstitution,SISInstnHR  where SISStudentGenInfo.ClassCode=SISClass.ClassCode and SISStudentGenInfo.InstID=SISInstitution.InstID and SISInstnHR.Institute_Id=SISInstitution.InstID and SISInstnHR.Institute_Id=SISStudentGenInfo.InstID";
            popupStudentGrid.DataBind();
            popupStudentGrid.Visible = true;
        }

        else if ((txtSrchStudentName.Text.Trim() != "" || txtSrchStudentRollNo.Text.Trim() != "") && StudentIntddl.SelectedValue == "")
        {
            StudentSQLDS.SelectParameters.Clear();
            StudentSQLDS.SelectParameters.Add("Name", txtSrchStudentName.Text);
            StudentSQLDS.SelectParameters.Add("RollNo", txtSrchStudentRollNo.Text);

            StudentSQLDS.SelectCommand = "Select TOP 10  RollNo,Name,SISClass.ClassName as ClassName,SISInstitution.InstName as InstName,EmailID1 ,SISStudentGenInfo.ClassCode as ClassCode,SISInstnHR.HRInstitute as InstID from SISStudentGenInfo,SISClass,SISInstitution,SISInstnHR  where SISStudentGenInfo.ClassCode=SISClass.ClassCode and SISStudentGenInfo.InstID=SISInstitution.InstID and SISInstnHR.Institute_Id=SISInstitution.InstID and SISInstnHR.Institute_Id=SISStudentGenInfo.InstID and  Name like '' + @Name + '%' and RollNo like '%' + RollNo + '%'";
            popupStudentGrid.DataBind();
            popupStudentGrid.Visible = true;
        }
        else
        {
            StudentSQLDS.SelectParameters.Clear();
            StudentSQLDS.SelectParameters.Add("Name", txtSrchStudentName.Text);
            StudentSQLDS.SelectParameters.Add("RollNo", txtSrchStudentRollNo.Text);
            StudentSQLDS.SelectParameters.Add("InstID", StudentIntddl.SelectedValue);

            StudentSQLDS.SelectCommand = "Select TOP 10  RollNo,Name,SISClass.ClassName as ClassName,SISInstitution.InstName as InstName,EmailID1 ,SISStudentGenInfo.ClassCode as ClassCode,SISInstnHR.HRInstitute as InstID from SISStudentGenInfo,SISClass,SISInstitution,SISInstnHR  where SISStudentGenInfo.ClassCode=SISClass.ClassCode and SISStudentGenInfo.InstID=SISInstitution.InstID and SISInstnHR.Institute_Id=SISInstitution.InstID and SISInstnHR.Institute_Id=SISStudentGenInfo.InstID and  (Name like '' + @Name + '%' and RollNo like '%' + RollNo + '%' and (SISStudentGenInfo.InstID=@InstID) ) ";
            popupStudentGrid.DataBind();
            popupStudentGrid.Visible = true;
        }

        // string rowVal = Request.Form["rowIndx"];
        string a = rowVal.Value;
        int rowIndex = Convert.ToInt32(a);
        DropDownList munonmu = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].FindControl("DropdownMuNonMu");
        if (munonmu.SelectedValue == "M")
        {
            popupPanelAffil.Style.Add("display", "true");
            popupstudent.Style.Add("display", "none");
        }
        else if (munonmu.SelectedValue == "S")
        {
            //popupstudent.Visible = true;
            popupstudent.Style.Add("display", "true");
            popupPanelAffil.Style.Add("display", "none");
        }
       
        ModalPopupExtender ModalPopupExtender8 = (ModalPopupExtender)Grid_AuthorEntry.Rows[rowIndex].FindControl("ModalPopupExtender2");
        ModalPopupExtender8.Show();
    }

    private void cleardata()
    {
        txtid.Text = string.Empty;
        txttiltle.Text = string.Empty;
        txtwriteup.Text = string.Empty;
        txtdate.Text = string.Empty;
        txtComments.Text = string.Empty;
        //DropDownbudget.Items.Clear();
        //DropDownbudget.DataSourceID = "SqlDataSourceBudget";
        //DropDownbudget.DataBind();
        //DropDownListseedStatus.Items.Clear();
        //txtComments.Text = string.Empty;
        SetInitialRow();
        //DropDownListBudgetapprove.Items.Clear();
        Txtapprovedbudget.Text = string.Empty;
        //DropDownListBudgetapprove.Items.Clear();
        //DropDownListBudgetapprove.Items.Add(new ListItem("Select", "0", true));
        //SqlDataSourceBudgetapprove.SelectCommand = "SELECT [BudgetId], [Amount] FROM [SeedMoneyBudget_M]";
        //DropDownListBudgetapprove.DataSourceID = "SqlDataSourceBudgetapprove";
        //DropDownListBudgetapprove.DataBind();
        PanelFileUpload.Visible = false;
        //bindBudgetDropDownList();

    }
         
    protected void GVViewFile_SelectedIndexChanged(object sender, EventArgs e)
    {
        string BoxId = txtid.Text.Trim();
        string servername = ConfigurationManager.AppSettings["ServerName"].ToString();
        string domainame = ConfigurationManager.AppSettings["DomainName"].ToString();
        string username = ConfigurationManager.AppSettings["UserName"].ToString();
        string password = ConfigurationManager.AppSettings["Password"].ToString();
        string folderpath;
        string path_BoxId;
        using (NetworkShareAccesser.Access(servername, domainame, username, password))
        {

            folderpath = ConfigurationManager.AppSettings["FolderPathSeedMoney"].ToString();
            path_BoxId = Path.Combine(folderpath, BoxId);

            int id = GVViewFile.SelectedIndex;
            Label filepath = (Label)GVViewFile.Rows[id].FindControl("lblgetid");
            string path = filepath.Text;       //actual filelocation path  
            string newpath = path.Replace('\\', '/');  // replacing '\' by '/' to view image or pdf

            Session["path"] = newpath;
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "Test", "ViewPdf()", true);
            Response.Write("<script>");
            Response.Write("window.open('DisplayPdf.aspx?val=" + newpath + "','_blank')");
            //path sent to display.aspx page
            Response.Write("</script>");
        }
    }
     
    protected void popSelected1(Object sender, EventArgs e)
    {
       
        //popupPanelAffilUpdate.Update();
        //popGridAffil.Visible = true;
        GridViewRow row = popGridAffil.SelectedRow;

        string EmployeeCode1 = row.Cells[1].Text;
        TextBox senderBox = sender as TextBox;


        string rowVal1 = rowVal.Value;
        int rowIndex = Convert.ToInt32(rowVal1);

        TextBox EmployeeCode = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("AuthorName");
        EmployeeCode.Text = EmployeeCode1;

      

        Business b = new Business();
        User a = new User();

        a = b.GetUserName(EmployeeCode1);


        string InstituteName1 = null;
        InstituteName1 = b.GetInstitutionName(a.InstituteId);


        string deptName1 = null;
        deptName1 = b.GetDeptName(a.Department, a.InstituteId);


        TextBox InstitutionName = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("InstitutionName");
        InstitutionName.Text = InstituteName1;

        TextBox DepartmentName = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("DepartmentName");
        DepartmentName.Text = deptName1;

        TextBox mailid = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("MailId");
        mailid.Text = a.emailId;

        HiddenField Institution = (HiddenField)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("Institution");
        Institution.Value = a.InstituteId;

        HiddenField Department = (HiddenField)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("Department");
        Department.Value = a.Department;

        TextBox employc = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("EmployeeCode");
        employc.Text = EmployeeCode1;

        TextBox AuthorName = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("AuthorName");
        AuthorName.Text = a.UserNamePrefix + " " + a.UserFirstName + " " + a.UserMiddleName + " " + a.UserLastName;


        affiliateSrch.Text = "";
        popGridAffil.DataBind();
       // popupPanelAffil.Visible = false;
        //popup.Visible = false;
    }


    protected void branchNameChanged(object sender, EventArgs e)
    {
        if (affiliateSrch.Text.Trim() == "")
        {
          

            SqlDataSourceAffil.SelectCommand = "SELECT top 10  User_Id,prefix+' '+firstname+' '+middlename+' '+lastname  as Name from User_M where Active='Y'";
            popGridAffil.DataBind();
            popGridAffil.Visible = true;
        }

        else
        {
          
            string name = affiliateSrch.Text.Replace(" ", String.Empty);
            SqlDataSourceAffil.SelectParameters.Clear();
            SqlDataSourceAffil.SelectParameters.Add("name", name);
            SqlDataSourceAffil.SelectCommand = "SELECT  User_Id,prefix+' '+firstname+' '+middlename+' '+lastname  as Name from User_M where prefix+firstname+middlename+lastname like '%' + @name + '%' and  Active='Y'";
            popGridAffil.DataBind();
            popGridAffil.Visible = true;
        }
        //popupPanelAffilUpdate.Update();
       
        string rowVal = Request.Form["rowIndx"];
        int rowIndex = Convert.ToInt32(rowVal);
        DropDownList munonmu = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].FindControl("DropdownMuNonMu");
        if (munonmu.SelectedValue == "M")
        {
            popupPanelAffil.Style.Add("display", "true");
            popupstudent.Style.Add("display", "none");
        }
        else if (munonmu.SelectedValue == "S")
        {
            popupPanelAffil.Style.Add("display", "none");
            popupstudent.Style.Add("display", "true");
        }

        ModalPopupExtender ModalPopupExtender8 = (ModalPopupExtender)Grid_AuthorEntry.Rows[rowIndex].FindControl("ModalPopupExtender4");
        ModalPopupExtender8.Show();
    }
    protected void BtnUpload_Click(object sender, EventArgs e)
    {
        Business B = new Business();
        SeedMoney j = new SeedMoney();
        SeedMoney s = new SeedMoney();
         int savedfileflag = 0;
            string filelocationpath = "";
            string UploadPdf1 = "";
            if (FileUploadPdf.HasFile)
            {
                string PublicationEntry1 = txtid.Text;
                string UploadPdf = "";
                string uploadedfilename = Path.GetFileName(FileUploadPdf.PostedFile.FileName);
                double size = FileUploadPdf.PostedFile.ContentLength;
                if (size <= 10485760) //10mb
                {
                    Stream fs = FileUploadPdf.PostedFile.InputStream;
                    BinaryReader br = new BinaryReader(fs);
                    byte[] bytes = br.ReadBytes((Int32)fs.Length);
                    bool exeresult = false;
                    Business B1 = new Business();
                    exeresult = B1.IsExeFile(bytes);

                    if (exeresult == true)
                    {
                        string CloseWindow1 = "alert('Uploaded file is not a valid file.Please upload a valid pdf file.')";
                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow1, true);
                        return;
                    }

                    //string path_BoxId = Path.Combine(mainpath, TextBoxPubId.Text); //main path + location
                    string servername = ConfigurationManager.AppSettings["ServerName"].ToString();
                    string domainame = ConfigurationManager.AppSettings["DomainName"].ToString();
                    string username = ConfigurationManager.AppSettings["UserName"].ToString();
                    string password = ConfigurationManager.AppSettings["Password"].ToString();
                    string FolderPathServerwrite = ConfigurationManager.AppSettings["FolderPathSeedMoney"].ToString();

                    using (NetworkShareAccesser.Access(servername, domainame, username, password))
                    {
                        var uploadfolder = FolderPathServerwrite;
                        string path_BoxId = Path.Combine(uploadfolder, txtid.Text); //main path + location
                        if (!Directory.Exists(path_BoxId))   //if directory doesnt exist
                        {
                            Directory.CreateDirectory(path_BoxId);//crete directory of location
                        }
                        string uploadedfilename1 = Path.GetFileName(FileUploadPdf.PostedFile.FileName);
                        string timestamp = DateTime.Now.ToString("dd-MM-yy-hh-mm-ss");
                        string fileextension = Path.GetExtension(uploadedfilename);
                        string actualfilenamewithtime = PublicationEntry1 + "_" + timestamp + fileextension;
                        UploadPdf1 = actualfilenamewithtime;
                        filelocationpath = Path.Combine(path_BoxId, actualfilenamewithtime);
                        FileUploadPdf.SaveAs(filelocationpath);  //saving file

                        s.Id = txtid.Text.Trim();
                        string FileUpload = "";

                        //j.PaublicationID = TextBoxPubId.Text.Trim();
                        if (FileUpload == "")
                        {
                            j.FilePath = filelocationpath;
                        }
                        else
                        {
                            j.FilePath = j.FilePathNew;
                        }

                        int result1 = B.UpdatePfPathCreateSeedMoney(txtid.Text, j);
                        Business b1 = new Business();
                        FileUpload = b1.GetFileUploadPathSeedMoney(txtid.Text);
                        if (FileUpload != "")
                        {
                            Labeluploadedfiles.Visible = true;
                            GVViewFile.Visible = true;
                        }
                        else
                        {
                            Labeluploadedfiles.Visible = false;
                            GVViewFile.Visible = false;
                        }
                        DSforgridview.SelectParameters.Clear();
                        DSforgridview.SelectParameters.Add("ID", s.Id);

                        DSforgridview.SelectCommand = "select UploadFilePath from SeedMoneyDetails where ID=@ID";
                        DSforgridview.DataBind();
                        GVViewFile.DataBind();
                        if (result1 == 1)
                        {

                            Response.Write("<script language=javascript>alert('File Uploaded Succesfully.. of ID: " + txtid.Text + "')</script>");
                            ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('please click on the Submit button after entering all the necessary details')</script>");
                            log.Info("Seed Money Details saved Successfully, of ID: " + txtid.Text);
                           
                        }
                    }
                }
            }

            
        
    }

    protected void DropdownMuNonMu_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow currentRow = (GridViewRow)((DropDownList)sender).Parent.Parent;
        DropDownList DropdownMuNonMu = (DropDownList)currentRow.FindControl("DropdownMuNonMu");
        DropDownList DropdownStudentInstitutionName = (DropDownList)currentRow.FindControl("DropdownStudentInstitutionName");
        DropDownList DropdownStudentDepartmentName = (DropDownList)currentRow.FindControl("DropdownStudentDepartmentName");
        TextBox InstitutionName = (TextBox)currentRow.FindControl("InstitutionName");
        TextBox DepartmentName = (TextBox)currentRow.FindControl("DepartmentName");
        ImageButton EmployeeCodeBtn = (ImageButton)currentRow.FindControl("EmployeeCodeBtn");
        TextBox AuthorName = (TextBox)currentRow.FindControl("AuthorName");
        TextBox EmployeeCode = (TextBox)currentRow.FindControl("EmployeeCode");
        TextBox MailId = (TextBox)currentRow.FindControl("MailId");
        DropDownList NationalType = (DropDownList)currentRow.FindControl("NationalType");
        TextBox NameInJournal = (TextBox)currentRow.FindControl("NameInJournal");
        ImageButton ImageButton1 = (ImageButton)currentRow.FindControl("ImageButton1");
        if (DropdownMuNonMu.SelectedValue == "O")
        {
            DropdownStudentInstitutionName.Visible = true;
            DropdownStudentDepartmentName.Visible = true;
            InstitutionName.Visible = false;
            DepartmentName.Visible = false;
            EmployeeCodeBtn.Visible = false;
            AuthorName.Enabled = true;
            EmployeeCode.Enabled = true;
            MailId.Enabled = true;
            NationalType.Visible = false;
            ImageButton1.Enabled = false;
            ImageButton1.Visible = true;
            MailId.Text = "";
            EmployeeCode.Text = "";
            AuthorName.Text = "";
            MailId.Text = "";
            InstitutionName.Text = "";
            DepartmentName.Text = "";
           
        }
        else if (DropdownMuNonMu.SelectedValue == "S")
        {
            DropdownStudentInstitutionName.Visible = false;
            DropdownStudentDepartmentName.Visible = false;
            InstitutionName.Visible = true;
            DepartmentName.Visible = true;
            NationalType.Visible = false;
            AuthorName.Enabled = false;
            EmployeeCode.Enabled = false;
            EmployeeCode.Text = "";
            AuthorName.Text = "";
            MailId.Text = "";
            InstitutionName.Text = "";
            DepartmentName.Text = "";
            ImageButton1.Enabled = false;
            ImageButton1.Visible = false;
            EmployeeCodeBtn.Visible = true;
        }
        else if (DropdownMuNonMu.SelectedValue == "N")
        {

            DropdownStudentInstitutionName.Visible = false;
            DropdownStudentDepartmentName.Visible = false;
            InstitutionName.Visible = true;
            DepartmentName.Visible = true;
            EmployeeCodeBtn.Visible = false;
            AuthorName.Enabled = true;
            InstitutionName.Enabled = true;
            DepartmentName.Enabled = true;
            MailId.Enabled = true;
            NationalType.Visible = true;
            EmployeeCode.Enabled = false;
            EmployeeCode.Text = "";
            AuthorName.Text = "";
            MailId.Text = "";
            InstitutionName.Text = "";
            DepartmentName.Text = "";
            ImageButton1.Enabled = false;
            ImageButton1.Visible = true;
        }
        else if (DropdownMuNonMu.SelectedValue == "M")
        {
               
                DropdownStudentDepartmentName.Visible = false;
                InstitutionName.Visible = true;
                DepartmentName.Visible = true;
                NationalType.Visible = false;
                EmployeeCode.Visible = true;
                EmployeeCode.Enabled = false;
                EmployeeCodeBtn.Enabled = true;
                InstitutionName.Enabled = false;
                DepartmentName.Enabled = false;
                AuthorName.Enabled = false;
                MailId.Enabled = false;
                EmployeeCode.Text = "";
                AuthorName.Text = "";
                MailId.Text = "";
                InstitutionName.Text = "";
                DepartmentName.Text = "";
                ImageButton1.Visible = false;
                EmployeeCodeBtn.Visible = true;
               

            
        }
    }

    protected void BtnSaveUpdate_Click(object sender, EventArgs e)
    {
        if (!Page.IsValid)
        {
            return;
        }
        try
        {
            Business b = new Business();
            Business B = new Business();
            SeedMoney s = new SeedMoney();
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
            SeedMoney[] JD = new SeedMoney[dtCurrentTable.Rows.Count];

            SeedMoney j = new SeedMoney();

            string Sid = txtid.Text;
            string Stitle = txttiltle.Text;
            string Swriteup = txtwriteup.Text;
            string Scomments = txtComments.Text;
            if (DropDownbudget.SelectedItem.Text == "Select")
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please select the Budget value ')</script>");
                return;
            }
            if (TextRemarks.Text == "")
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please enter the Remarks ')</script>");
                return;
            }
            if (txtComments.Text == "")
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please enter the Comments ')</script>");
                return;
            }
            double Sbudget = Convert.ToDouble(DropDownbudget.SelectedItem.Text);
            DropDownbudget.SelectedItem.Text = Sbudget.ToString();
            string Remarks = TextRemarks.Text;
            double approvedbudget = Convert.ToDouble(Txtapprovedbudget.Text);
            if (txtdate.Text != "")
            {
                s.AppliedDate = Convert.ToDateTime(txtdate.Text.Trim());
            }

            s.Id = Sid;
            s.Title = Stitle;
            s.Budget = Sbudget;
            s.CreatedBy = Session["UserId"].ToString();
            s.InstUser = Session["InstituteId"].ToString();
            s.DeptUser = Session["Department"].ToString();
            s.Entrytype = "F";
            s.Remarks = Remarks;
            s.Comments= Scomments;
            s.ApprovedBudget = approvedbudget;
                if (txtid.Text != "")
                {
                    int result = 0;
                    result = B.UpdateSeedMoneyEntryApproved(s, JD);
                    ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Seed Money Details updated Successfully.. of ID: " + txtid.Text + "')</script>");
                    log.Info("Seed Money Details saved Successfully, of ID: " + txtid.Text);
                    //EmailDetails details = new EmailDetails();
                    //details = SendMail();
                    //details.Id = txtid.Text;
                    //SendMailObject obj1 = new SendMailObject();
                    //bool result2 = obj1.InsertIntoEmailQueue(details);
                    BtnSaveUpdate.Enabled = false;

                }
        }

        catch (Exception ex)
        {
            log.Error("Inside Catch Block Of Grant" + ex.Message + " UserID : " + Session["UserId"].ToString());
            log.Error(ex.StackTrace);
            if (ex.Message.Contains("The string was not recognized as a valid DateTime"))
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Date is not valid')</script>");
            }
            if (ex.Message.Contains("String was not recognized as a valid DateTime."))
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Date is not valid')</script>");

            }

            else if (ex.Message.Contains("Input string was not in a correct format"))
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Error')</script>");
                log.Error("Error, of ID: " + txtid.Text);

            }
            else if (ex.Message.Contains("There is already an open DataReader"))
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Seed data Creatoin Failed)</script>");
                log.Info("Seed data Creation Saved..Upload failed, of ID: " + ex.Message + " " + txtid.Text);

            }
            else if (ex.Message.Contains("Object reference not set to an instance of an object"))
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Seed data Creaton Failed....Please contact admin')</script>");
                log.Error("Seed data Creaton Failed.....Please contact admin, id: " + txtid.Text);


            }
            else if (ex.Message.Contains("IX_Project"))
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Seed Data Creation Failed.This Details Already Present!')</script>");

            }

            else if (ex.Message.Contains("Failure sending mail."))
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Seed Data submitted successfully.Failure in sending mail.')</script>");

            }

            else

                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Seed data Creation failed')</script>");
            log.Error("Grant data Creaton Failed.... id: " + txtid.Text);

        }
    }
}