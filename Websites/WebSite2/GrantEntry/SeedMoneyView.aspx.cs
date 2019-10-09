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
public partial class GrantEntry_SeedMoneyView : System.Web.UI.Page
{
    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {         
                panelSearchPub.Visible = true;
                panAddAuthor.Visible = true;
                PanelMU.Visible = true;
                //Txtappliedbudget.Text = string.Empty;
                DropDownbudget.Items.Clear();
                PanelRemark.Visible = false;
                DropDownbudget.Items.Add(new ListItem("Select", "0", true));
                bindBudgetDropDownList();
                PanelRemark.Visible = false;
                PanelFileUpload.Visible = false;
                Txtapprovedbudget.Visible = false;
                //DropDownListBudgetapprove.Visible = false;
                lnkSeedMoneyFaculty.Visible = false;
                lnkSeedMoneyStudent.Visible = false;
                Grid_AuthorEntry.Enabled = false;
                   
            setModalWindow(sender, e);
            SetInitialRow();
            
        }
    }

    private void bindBudgetDropDownList()
    {
        DropDownbudget.DataSourceID = "SqlDataSourceBudget";
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
             if (SeedMoneyStatusSearch.SelectedValue == "A")
             {
                 if (txtSid.Text == "" && txtstitle.Text == "")
                 {
                     SqlDataSource1.SelectCommand = " select s.ID,s.Title,s.Writeup,s.Budget,s.AppliedDate,p.StatusName,s.Createdby,s.CreatedDate,s.Institution,s.Department,s.EntryType from SeedMoneyDetails s , Status_Seedmoney_M p where  (s.Status='NEW' or s.Status='REW' or s.Status='APP' or s.Status='SUB' or s.Status='REJ' or s.Status='REV'  or s.Status='CAN' ) and s.Status=p.StatusId order by ID";
                 }
                 else if (txtSid.Text != "" && txtstitle.Text == "")
                 {
                     SqlDataSource1.SelectParameters.Clear();
                     SqlDataSource1.SelectParameters.Add("ID", txtSid.Text.Trim());

                     SqlDataSource1.SelectCommand = " select s.ID,s.Title,s.Writeup,s.Budget,s.AppliedDate,p.StatusName,s.Createdby,s.CreatedDate,s.Institution,s.Department,s.EntryType from SeedMoneyDetails s, Status_Seedmoney_M p where  (s.Status='NEW' or s.Status='REW' or s.Status='APP' or s.Status='SUB' or s.Status='REJ'  or s.Status='REV'  or s.Status='CAN') and s.ID like'%' + @ID + '%' and s.Status=p.StatusId order by ID";
                 }
                 else if (txtSid.Text == "" && txtstitle.Text != "")
                 {
                     SqlDataSource1.SelectParameters.Clear();
                     SqlDataSource1.SelectParameters.Add("Title", txtstitle.Text.Trim());
                     SqlDataSource1.SelectCommand = "select s.ID,s.Title,s.Writeup,s.Budget,s.AppliedDate,p.StatusName,s.Createdby,s.CreatedDate,s.Institution,s.Department,s.EntryType from SeedMoneyDetails s, Status_Seedmoney_M p where  (s.Status='NEW' or s.Status='REW' or s.Status='APP' or s.Status='SUB' or s.Status='REJ'  or s.Status='REV'  or s.Status='CAN') and s.Title like'%' + @Title + '%' and s.Status=p.StatusId order by ID";
                 }
                 else
                 {
                     SqlDataSource1.SelectParameters.Clear();
                     SqlDataSource1.SelectParameters.Add("ID", txtSid.Text.Trim());
                     SqlDataSource1.SelectParameters.Add("Title", txtstitle.Text.Trim());

                     SqlDataSource1.SelectCommand = "select s.ID,s.Title,s.Writeup,s.Budget,s.AppliedDate,p.StatusName,s.Createdby,s.CreatedDate,s.Institution,s.Department,s.EntryType from SeedMoneyDetails s, Status_Seedmoney_M p where  (s.Status='NEW' or s.Status='REW' or s.Status='APP' or s.Status='SUB' or s.Status='REJ'  or s.Status='REV'  or s.Status='CAN') and s.Title like'%' + @Title + '%' and s.ID like'%' + @ID + '%' and s.Status=p.StatusId order by ID";
                 }
                 GridViewSearchGrant.DataBind();
                 SqlDataSource1.DataBind();
             }
             else
             {
                 if (txtSid.Text == "" && txtstitle.Text == "")
                 {
                     SqlDataSource1.SelectParameters.Clear();
                     SqlDataSource1.SelectParameters.Add("Status", SeedMoneyStatusSearch.SelectedValue);
                     SqlDataSource1.SelectCommand = " select s.ID,s.Title,s.Writeup,s.Budget,s.AppliedDate,p.StatusName,s.Createdby,s.CreatedDate,s.Institution,s.Department,s.EntryType from SeedMoneyDetails s , Status_Seedmoney_M p where  (s.Status=@Status) and s.Status=p.StatusId order by ID";
                 }
                 else if (txtSid.Text != "" && txtstitle.Text == "")
                 {
                     SqlDataSource1.SelectParameters.Clear();
                     SqlDataSource1.SelectParameters.Add("Status", SeedMoneyStatusSearch.SelectedValue);
                     SqlDataSource1.SelectParameters.Add("ID", txtSid.Text.Trim());

                     SqlDataSource1.SelectCommand = " select s.ID,s.Title,s.Writeup,s.Budget,s.AppliedDate,p.StatusName,s.Createdby,s.CreatedDate,s.Institution,s.Department,s.EntryType from SeedMoneyDetails s, Status_Seedmoney_M p where  (s.Status=@Status) and s.ID like'%' + @ID + '%' and s.Status=p.StatusId order by ID";
                 }
                 else if (txtSid.Text == "" && txtstitle.Text != "")
                 {
                     SqlDataSource1.SelectParameters.Clear();
                     SqlDataSource1.SelectParameters.Add("Status", SeedMoneyStatusSearch.SelectedValue);
                     SqlDataSource1.SelectParameters.Add("Title", txtstitle.Text.Trim());

                     SqlDataSource1.SelectCommand = "select s.ID,s.Title,s.Writeup,s.Budget,s.AppliedDate,p.StatusName,s.Createdby,s.CreatedDate,s.Institution,s.Department,s.EntryType from SeedMoneyDetails s, Status_Seedmoney_M p where  (s.Status=@Status) and s.Title like'%' + @Title + '%' and s.Status=p.StatusId order by ID";
                 }
                 else
                 {
                     SqlDataSource1.SelectParameters.Clear();
                     SqlDataSource1.SelectParameters.Add("Status", SeedMoneyStatusSearch.SelectedValue);
                     SqlDataSource1.SelectParameters.Add("Title", txtstitle.Text.Trim());
                     SqlDataSource1.SelectParameters.Add("ID", txtSid.Text.Trim());

                     SqlDataSource1.SelectCommand = "select s.ID,s.Title,s.Writeup,s.Budget,s.AppliedDate,p.StatusName,s.Createdby,s.CreatedDate,s.Institution,s.Department,s.EntryType from SeedMoneyDetails s, Status_Seedmoney_M p where  (s.Status=@Status) and s.Title like'%' + @Title + '%' and s.ID like'%' + @ID + '%' and s.Status=p.StatusId order by ID";
                 }
                 GridViewSearchGrant.DataBind();
                 SqlDataSource1.DataBind();
             }
        //else
        //    {
        //        if (txtSid.Text == "" && txtstitle.Text == "")
        //        {
        //            SqlDataSource1.SelectCommand = " select p.ID, r.TypeName,p.ProjectUnit, UTN,Title,Description,CONVERT(DECIMAL(14,0),AppliedAmount) as AppliedAmount,q.StatusName,p.ProjectType from Project p,Status_Project_M q, ProjectType_M r where  p.ProjectType=r.TypeId and p.ProjectStatus=q.StatusId and (CreatedBy='" + Session["UserId"].ToString() + "' or (p.ID+p.ProjectUnit in(Select  ID+ProjectUnit from Projectnvestigator where InvestigatorType='P' and EmployeeCode='" + Session["UserId"].ToString() + "')))  and (s.Status='NEW' or s.Status='REW' or s.Status='APP' or s.Status='SUB' or s.Status='REJ')  ";
        //        }
        //        else if (txtSid.Text != "" && txtstitle.Text == "")
        //        {
        //            SqlDataSource1.SelectCommand = " select p.ID, r.TypeName,p.ProjectUnit, UTN,Title,Description,CONVERT(DECIMAL(14,0),AppliedAmount) as AppliedAmount,q.StatusName,p.ProjectType from Project p,Status_Project_M q, ProjectType_M r where  p.ProjectType=r.TypeId and p.ProjectStatus=q.StatusId and (CreatedBy='" + Session["UserId"].ToString() + "' or (p.ID+p.ProjectUnit in(Select  ID+ProjectUnit from Projectnvestigator where InvestigatorType='P' and EmployeeCode='" + Session["UserId"].ToString() + "'))) and ID like'%" + txtSid.Text.Trim() + "%'  and (s.Status='NEW' or s.Status='REW' or s.Status='APP' or s.Status='SUB' or s.Status='REJ') ";
        //        }
        //        else if (txtSid.Text == "" && txtstitle.Text != "")
        //        {
        //            SqlDataSource1.SelectCommand = " select p.ID, r.TypeName,p.ProjectUnit, UTN,Title,Description,CONVERT(DECIMAL(14,0),AppliedAmount) as AppliedAmount,q.StatusName,p.ProjectType from Project p,Status_Project_M q, ProjectType_M r where  p.ProjectType=r.TypeId and p.ProjectStatus=q.StatusId and (CreatedBy='" + Session["UserId"].ToString() + "' or (p.ID+p.ProjectUnit in(Select  ID+ProjectUnit from Projectnvestigator where InvestigatorType='P' and EmployeeCode='" + Session["UserId"].ToString() + "'))) and Title like'%" + txtstitle.Text.Trim() + "%' and (s.Status='NEW' or s.Status='REW' or s.Status='APP' or s.Status='SUB' or s.Status='REJ') ";
        //        }
        //        else
        //        {

        //            SqlDataSource1.SelectCommand = " select p.ID, r.TypeName,p.ProjectUnit, UTN,Title,Description,CONVERT(DECIMAL(14,0),AppliedAmount) as AppliedAmount,q.StatusName,p.ProjectType from Project p,Status_Project_M q, ProjectType_M r where  p.ProjectType=r.TypeId and p.ProjectStatus=q.StatusId and (CreatedBy='" + Session["UserId"].ToString() + "' or (p.ID+p.ProjectUnit in(Select  ID+ProjectUnit from Projectnvestigator where InvestigatorType='P' and EmployeeCode='" + Session["UserId"].ToString() + "'))) and ID like'%" + txtSid.Text.Trim() + "%' and Title like'%" + txtstitle.Text.Trim() + "%' and (s.Status='NEW' or s.Status='REW' or s.Status='APP' or s.Status='SUB' or s.Status='REJ')   ";

        //        }
        //        GridViewSearchGrant.DataBind();
        //        SqlDataSource1.DataBind();
        //    }
        //GridViewsanSearch.Visible = true;
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
        string InstUser = Session["InstituteId"].ToString();
        string DeptUser = Session["Department"].ToString();
        string role = Session["Role"].ToString();
        if (role =="21")
        {
            string InstUser1 = Session["InstituteName"].ToString();
            string DeptUser2 = Session["Department"].ToString();
        }

        SeedMoney v = new SeedMoney();
        SeedMoney v1 = new SeedMoney();
        SeedMoney v2 = new SeedMoney();
        Business obj = new Business();
        Business B = new Business();
        v = obj.fnfindSeedid(SID);

        txtid.Text = SID;
        txttiltle.Text = v.Title;
        txtwriteup.Text = v.Writeup;
        string data = null;
        if (v.Status != "")
        {
            data = B.GetStatusName(v.Status);
            txtstatus.Text = data;
        }
        if (v.AppliedDate.ToShortDateString() != "01/01/0001")
        {
            txtdate.Text = v.AppliedDate.ToShortDateString();
        }
        double Sbudget = Convert.ToDouble(v.Budget);
        SqlDataSourceBudget.SelectCommand = "SELECT * FROM [SeedMoneyBudget_M]";
        DropDownbudget.DataSourceID = "SqlDataSourceBudget";
        DropDownbudget.DataBind();
        DropDownbudget.SelectedItem.Text = Sbudget.ToString();
        
        TextStatus.Visible = false;
        lblStatus.Visible = false;
        PanelRemark.Visible = false;
        lblbudgetapprove.Visible = false;
        Txtapprovedbudget.Visible = false;
        //DropDownListBudgetapprove.Visible = false;
        Panel3.Visible = false;
        if (v.Status == "APP")
        {
            PanelRemark.Visible = true;
            txtComments.Text = v.Comments;   
            lblStatus.Visible = true;
            TextStatus.Visible = true;
            TextStatus.Text = v.Remarks;
            lblbudgetapprove.Visible = true;
            Txtapprovedbudget.Visible = true;
            //DropDownListBudgetapprove.Visible = true;
            double SAbudget = Convert.ToDouble(v.ApprovedBudget);
            Txtapprovedbudget.Text = SAbudget.ToString();
        }
        if (v.Status == "REW")
        {          
            PanelRemark.Visible = true;
            txtComments.Text = v.Comments;      
        }
        if (v.Status == "REJ")
        {
            PanelRemark.Visible = true;
            txtComments.Text = v.Comments;
        }
        if (v.Status == "REV")
        {
            PanelRemark.Visible = true;
            txtComments.Text = v.Comments;
        }
        if (v.Status == "CAN")
        {
            PanelRemark.Visible = false;
            Panel3.Enabled = false;
            Panel3.Visible = true;
            txtcancelComments.Text = v.Comments;
        }
        if (v.Status == "NEW")
        {
            string FileUpload = "";
            Business b1 = new Business();
            FileUpload = b1.GetFileUploadPathSeedMoney(txtid.Text);
            if (FileUpload != "")
            {
                PanelFileUpload.Visible = true;
                Labeluploadedfiles.Visible = true;
                GVViewFile.Visible = true;
                GVViewFile.Enabled = true;
                LabelUploadPfd.Visible = false;
                FileUploadPdf.Visible = false;
                LabelNote.Visible = false;
            }
            else
            {
                PanelFileUpload.Visible = true;
                LabelUploadPfd.Visible = false;
                FileUploadPdf.Visible = false;
                LabelNote.Visible = true;
                Labeluploadedfiles.Visible = false;
                GVViewFile.Visible = false;
                GVViewFile.Enabled = false;

            }
        }
        else if (v.Status == "APP")
        {
            string FileUpload = "";
            Business b1 = new Business();
            FileUpload = b1.GetFileUploadPathSeedMoney(txtid.Text);
            if (FileUpload != "")
            {
                PanelFileUpload.Visible = true;
                Labeluploadedfiles.Visible = true;
                GVViewFile.Visible = true;
                GVViewFile.Enabled = true;
                LabelUploadPfd.Visible = false;
                FileUploadPdf.Visible = false;
                LabelNote.Visible = false;
            }
            else
            {
                LabelUploadPfd.Visible = false;
                FileUploadPdf.Visible = false;
                LabelNote.Visible = true;
                Labeluploadedfiles.Visible = false;
                GVViewFile.Visible = false;
                GVViewFile.Enabled = false;
               
            }
        }
        else if (v.Status == "SUB")
        {
            string FileUpload = "";
            Business b1 = new Business();
            FileUpload = b1.GetFileUploadPathSeedMoney(txtid.Text);
            if (FileUpload != "")
            {
                PanelFileUpload.Visible = true;
                Labeluploadedfiles.Visible = true;
                GVViewFile.Visible = true;
                GVViewFile.Enabled = true;
                LabelUploadPfd.Visible = false;
                FileUploadPdf.Visible = false;
                LabelNote.Visible = false;
            }
            else
            {
                LabelUploadPfd.Visible = false;
                FileUploadPdf.Visible = false;
                LabelNote.Visible = true;
                Labeluploadedfiles.Visible = false;
                GVViewFile.Visible = false;
                GVViewFile.Enabled = false;
              
            }
        }
        else if (v.Status == "REW")
        {
            string FileUpload = "";
            Business b1 = new Business();
            FileUpload = b1.GetFileUploadPathSeedMoney(txtid.Text);
            if (FileUpload != "")
            {
                LabelUploadPfd.Visible = false;
                FileUploadPdf.Visible = false;
                PanelFileUpload.Visible = true;
                Labeluploadedfiles.Visible = true;
                GVViewFile.Visible = true;
                GVViewFile.Enabled = true;
                LabelNote.Visible = false;
            }
            else
            {
                LabelUploadPfd.Visible = false;
                FileUploadPdf.Visible = false;
                LabelNote.Visible = true;
                Labeluploadedfiles.Visible = false;
                GVViewFile.Visible = false;
                GVViewFile.Enabled = false;
             
            }
        }
        else if (v.Status == "REJ")
        {
            string FileUpload = "";
            Business b1 = new Business();
            FileUpload = b1.GetFileUploadPathSeedMoney(txtid.Text);
            if (FileUpload != "")
            {
                LabelUploadPfd.Visible = false;
                FileUploadPdf.Visible = false;
                PanelFileUpload.Visible = true;
                Labeluploadedfiles.Visible = true;
                GVViewFile.Visible = true;
                GVViewFile.Enabled = true;
                PanelFileUpload.Visible = false;
                LabelNote.Visible = false;
            }
            else
            {
                LabelUploadPfd.Visible = false;
                FileUploadPdf.Visible = false;
                LabelNote.Visible = true;
                Labeluploadedfiles.Visible = false;
                GVViewFile.Visible = false;
                GVViewFile.Enabled = false;
               
                PanelFileUpload.Visible = false;
            }
        }
        else if (v.Status == "REV")
        {
            string FileUpload = "";
            Business b1 = new Business();
            FileUpload = b1.GetFileUploadPathSeedMoney(txtid.Text);
            if (FileUpload != "")
            {
                LabelUploadPfd.Visible = false;
                FileUploadPdf.Visible = false;
                PanelFileUpload.Visible = true;
                Labeluploadedfiles.Visible = true;
                GVViewFile.Visible = true;
                GVViewFile.Enabled = true;
                PanelFileUpload.Visible = false;
                LabelNote.Visible = false;
            }
            else
            {
                LabelUploadPfd.Visible = false;
                FileUploadPdf.Visible = false;
                LabelNote.Visible = true;
                Labeluploadedfiles.Visible = false;
                GVViewFile.Visible = false;
                GVViewFile.Enabled = false;

                PanelFileUpload.Visible = false;
            }
        }
        DSforgridview.SelectParameters.Clear();
        DSforgridview.SelectParameters.Add("ID", txtid.Text);

        DSforgridview.SelectCommand = "select UploadFilePath from SeedMoneyDetails where ID=@ID";
        DSforgridview.DataBind();
        GVViewFile.DataBind();
        if (v.Entrytype != "")
        {
            if (v.Entrytype == "S")
            {
                lablPanelTitlestudent.Visible = true;
                lablPanelTitlefaculty.Visible = false;
                lnkSeedMoneyFaculty.Visible = false;
                lnkSeedMoneyStudent.Visible = true;
            }
            else
            {
                lablPanelTitlestudent.Visible = false;
                lablPanelTitlefaculty.Visible = true;
                lnkSeedMoneyFaculty.Visible = true;
                lnkSeedMoneyStudent.Visible = false;
            }
        }


        

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



        if (v.Status == "APP")
        {






            //txtprojectactualdate.Enabled = true;



            panAddAuthor.Enabled = true;




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
    //Function to Add authors
    protected void addRow(object sender, EventArgs e)
    {
        if (Grid_AuthorEntry.Rows.Count == 0)
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

                        TextBox AuthorName = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[1].FindControl("AuthorName");
                        DropDownList DropdownMuNonMu = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[2].FindControl("DropdownMuNonMu");
                        HiddenField EmployeeCode = (HiddenField)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("EmployeeCode");
                        HiddenField Institution = (HiddenField)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("Institution");
                        TextBox InstitutionName = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[6].FindControl("InstitutionName");
                        HiddenField Department = (HiddenField)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("Department");
                        TextBox DepartmentName = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("DepartmentName");
                        TextBox MailId = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("MailId");

                        DropDownList AuthorType = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("AuthorType");
                        DropDownList isLeadPI = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("isLeadPI");
                        ImageButton EmployeeCodeBtnImg = (ImageButton)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("EmployeeCodeBtn");

                        DropDownList DropdownStudentInstitutionName = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("DropdownStudentInstitutionName");
                        DropDownList DropdownStudentDepartmentName = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("DropdownStudentDepartmentName");

                        DropDownList NationalType = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("NationalType");
                        DropDownList ContinentId = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("ContinentId");

                        drCurrentRow = dtCurrentTable.NewRow();
                        dtCurrentTable.Rows[i - 1]["DropdownMuNonMu"] = DropdownMuNonMu.Text;
                        dtCurrentTable.Rows[i - 1]["AuthorName"] = AuthorName.Text;
                        dtCurrentTable.Rows[i - 1]["EmployeeCode"] = EmployeeCode.Value;

                        if (DropdownMuNonMu.Text == "M")
                        {

                            DropdownStudentInstitutionName.Visible = false;
                            DropdownStudentDepartmentName.Visible = false;
                            InstitutionName.Visible = true;
                            DepartmentName.Visible = true;

                            NationalType.Visible = false;
                            ContinentId.Visible = false;
                            EmployeeCodeBtnImg.Enabled = true;
                            dtCurrentTable.Rows[i - 1]["NationalType"] = NationalType.SelectedValue;
                            dtCurrentTable.Rows[i - 1]["ContinentId"] = ContinentId.SelectedValue;

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

                            NationalType.Visible = true;
                            if (NationalType.SelectedValue == "I")
                            {
                                ContinentId.Visible = true;
                            }
                            else
                            {
                                ContinentId.Visible = false;
                            }

                            EmployeeCodeBtnImg.Enabled = false;

                            dtCurrentTable.Rows[i - 1]["NationalType"] = NationalType.SelectedValue;
                            dtCurrentTable.Rows[i - 1]["ContinentId"] = ContinentId.SelectedValue;

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

                            EmployeeCodeBtnImg.Enabled = false;

                            dtCurrentTable.Rows[i - 1]["NationalType"] = NationalType.SelectedValue;
                            dtCurrentTable.Rows[i - 1]["ContinentId"] = ContinentId.SelectedValue;
                            dtCurrentTable.Rows[i - 1]["Institution"] = DropdownStudentInstitutionName.SelectedValue;

                            dtCurrentTable.Rows[i - 1]["Department"] = DropdownStudentDepartmentName.SelectedValue;

                        }

                        dtCurrentTable.Rows[i - 1]["MailId"] = MailId.Text;

                        dtCurrentTable.Rows[i - 1]["AuthorType"] = AuthorType.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["isLeadPI"] = isLeadPI.SelectedValue;

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

                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["CurrentTable"] = dtCurrentTable;

                    Grid_AuthorEntry.DataSource = dtCurrentTable;
                    Grid_AuthorEntry.DataBind();

                }
            }
            else
            {
                Response.Write("ViewState is null");
            }

            SetPreviousData();
        }

        setModalWindow(sender, e); // initialise popup gridviews
    }
    //Investigator
    protected void setModalWindow(object sender, EventArgs e)
    {
        popupPanelAffil.Visible = false;
        popGridAffil.DataSourceID = "SqlDataSourceAffil";
        SqlDataSourceAffil.DataBind();
        popGridAffil.DataBind();
        popGridAffil.Visible = true;
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

        TextBox EmployeeCode = (TextBox)Grid_AuthorEntry.Rows[0].Cells[0].FindControl("EmployeeCode");
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
                    TextBox AuthorName = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[1].FindControl("AuthorName");
                    DropDownList DropdownMuNonMu = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[2].FindControl("DropdownMuNonMu");
                    //TextBox amount = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[3].FindControl("amount");
                    TextBox EmployeeCode = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("EmployeeCode");
                    HiddenField Institution = (HiddenField)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("Institution");
                    TextBox InstitutionName = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[6].FindControl("InstitutionName");
                    HiddenField Department = (HiddenField)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("Department");
                    TextBox DepartmentName = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("DepartmentName");
                    TextBox MailId = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("MailId");

                    DropDownList AuthorType = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("AuthorType");
                    DropDownList isLeadPI = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("isLeadPI");
                    DropDownList DropdownStudentInstitutionName = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("DropdownStudentInstitutionName");
                    DropDownList DropdownStudentDepartmentName = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("DropdownStudentDepartmentName");

                    ImageButton EmployeeCodeBtnImg = (ImageButton)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("EmployeeCodeBtn");

                    DropDownList NationalType = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("NationalType");
                    DropDownList ContinentId = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("ContinentId");

                    TextBox AuthorName1 = (TextBox)Grid_AuthorEntry.Rows[0].Cells[1].FindControl("AuthorName");
                    DropDownList DropdownMuNonMu1 = (DropDownList)Grid_AuthorEntry.Rows[0].Cells[2].FindControl("DropdownMuNonMu");
                    ImageButton EmployeeCodeBtnImg1 = (ImageButton)Grid_AuthorEntry.Rows[0].Cells[0].FindControl("EmployeeCodeBtn");

                    HiddenField EmployeeCode1 = (HiddenField)Grid_AuthorEntry.Rows[0].Cells[0].FindControl("EmployeeCode");
                    HiddenField Institution1 = (HiddenField)Grid_AuthorEntry.Rows[0].Cells[0].FindControl("Institution");
                    TextBox InstitutionName1 = (TextBox)Grid_AuthorEntry.Rows[0].Cells[6].FindControl("InstitutionName");
                    HiddenField Department1 = (HiddenField)Grid_AuthorEntry.Rows[0].Cells[0].FindControl("Department");
                    TextBox DepartmentName1 = (TextBox)Grid_AuthorEntry.Rows[0].Cells[0].FindControl("DepartmentName");
                    TextBox MailId1 = (TextBox)Grid_AuthorEntry.Rows[0].Cells[0].FindControl("MailId");


                    DropDownList AuthorType1 = (DropDownList)Grid_AuthorEntry.Rows[0].Cells[0].FindControl("AuthorType");
                    DropDownList DropdownStudentInstitutionName1 = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("DropdownStudentInstitutionName");
                    DropDownList DropdownStudentDepartmentName1 = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("DropdownStudentDepartmentName");


                    DropdownMuNonMu.Text = dt.Rows[i]["DropdownMuNonMu"].ToString();
                    AuthorName.Text = dt.Rows[i]["AuthorName"].ToString();
                    EmployeeCode.Text = dt.Rows[i]["EmployeeCode"].ToString();
                    if (DropdownMuNonMu.Text == "M")
                    {
                        AuthorName.Enabled = false;
                        InstitutionName.Enabled = false;
                        DepartmentName.Enabled = false;
                        MailId.Enabled = false;

                        EmployeeCodeBtnImg.Enabled = true;

                        DropdownStudentInstitutionName.Visible = false;
                        DropdownStudentDepartmentName.Visible = false;
                        InstitutionName.Visible = true;
                        DepartmentName.Visible = true;

                        NationalType.Visible = false;
                        ContinentId.Visible = false;

                        NationalType.Text = dt.Rows[i]["NationalType"].ToString();
                        ContinentId.Text = dt.Rows[i]["ContinentId"].ToString();

                        Institution.Value = dt.Rows[i]["Institution"].ToString();
                        InstitutionName.Text = dt.Rows[i]["InstitutionName"].ToString();
                        Department.Value = dt.Rows[i]["Department"].ToString();
                        DepartmentName.Text = dt.Rows[i]["DepartmentName"].ToString();
                    }

                    else if (DropdownMuNonMu.Text == "N")
                    {
                        AuthorName.Enabled = true;
                        InstitutionName.Enabled = true;
                        DepartmentName.Enabled = true;
                        MailId.Enabled = true;

                        EmployeeCodeBtnImg.Enabled = false;

                        NationalType.Visible = true;
                        NationalType.Text = dt.Rows[i]["NationalType"].ToString();
                        ContinentId.Text = dt.Rows[i]["ContinentId"].ToString();


                        if (NationalType.Text == "I")
                        {
                            ContinentId.Visible = true;
                        }
                        else
                        {
                            ContinentId.Visible = false;
                        }

                        DropdownStudentInstitutionName.Visible = false;
                        DropdownStudentDepartmentName.Visible = false;
                        InstitutionName.Visible = true;
                        DepartmentName.Visible = true;

                        Institution.Value = dt.Rows[i]["Institution"].ToString();
                        InstitutionName.Text = dt.Rows[i]["InstitutionName"].ToString();
                        Department.Value = dt.Rows[i]["Department"].ToString();
                        DepartmentName.Text = dt.Rows[i]["DepartmentName"].ToString();
                    }
                    else if (DropdownMuNonMu.Text == "S")
                    {
                        AuthorName.Enabled = true;
                        InstitutionName.Enabled = false;
                        DepartmentName.Enabled = false;
                        MailId.Enabled = true;


                        NationalType.Visible = false;
                        ContinentId.Visible = false;

                        NationalType.Text = dt.Rows[i]["NationalType"].ToString();
                        ContinentId.Text = dt.Rows[i]["ContinentId"].ToString();

                        EmployeeCodeBtnImg.Enabled = false;

                        DropdownStudentInstitutionName.Visible = true;
                        DropdownStudentDepartmentName.Visible = true;
                        InstitutionName.Visible = false;
                        DepartmentName.Visible = false;
                        //  Institution.Value = dt.Rows[i]["Institution"].ToString();
                        DropdownStudentInstitutionName.SelectedValue = dt.Rows[i]["Institution"].ToString();
                        //  Department.Value = dt.Rows[i]["Department"].ToString();
                        DropdownStudentDepartmentName.SelectedValue = dt.Rows[i]["Department"].ToString();
                    }

                    MailId.Text = dt.Rows[i]["MailId"].ToString();
                    AuthorType.Text = dt.Rows[i]["AuthorType"].ToString();
                    isLeadPI.Text = dt.Rows[i]["isLeadPI"].ToString();
                    if (AuthorType.SelectedValue == "C")
                    {
                        isLeadPI.Enabled = false;
                    }
                    else
                    {
                        isLeadPI.Enabled = true;
                    }
                    AuthorName1.Enabled = false;
                    EmployeeCodeBtnImg1.Enabled = false;
                    DropdownMuNonMu1.Enabled = false;
                    InstitutionName1.Enabled = false;
                    //  Department1.Enabled = false;
                    DepartmentName1.Enabled = false;
                    MailId1.Enabled = false;


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
    //Drop Down MU/Non MU change
    protected void DropdownMuNonMuOnSelectedIndexChanged(object sender, EventArgs e)
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
        if (DropdownMuNonMu.SelectedValue == "M")
        {
            DropdownStudentInstitutionName1.Visible = false;
            DropdownStudentDepartmentName.Visible = false;
            InstitutionName.Visible = true;
            DepartmentName.Visible = true;

            NationalType.Visible = false;
            EmployeeCodeBtn.Enabled = true;
            InstitutionName.Enabled = false;
            DepartmentName.Enabled = false;
            AuthorName.Enabled = false;
            MailId.Enabled = false;
            AuthorName.Text = "";
            MailId.Text = "";
            InstitutionName.Text = "";
            DepartmentName.Text = "";

        }
        else if (DropdownMuNonMu.SelectedValue == "N")
        {

            DropdownStudentInstitutionName1.Visible = false;
            DropdownStudentDepartmentName.Visible = false;
            InstitutionName.Visible = true;
            DepartmentName.Visible = true;

            NationalType.Visible = true;
            EmployeeCodeBtn.Enabled = false;
            InstitutionName.Enabled = true;
            DepartmentName.Enabled = true;
            AuthorName.Enabled = true;
            MailId.Enabled = true;
            AuthorName.Text = "";
            MailId.Text = "";
            InstitutionName.Text = "";
            DepartmentName.Text = "";

        }

        else if (DropdownMuNonMu.SelectedValue == "S")
        {

            DropdownStudentInstitutionName1.Visible = true;
            DropdownStudentDepartmentName.Visible = true;
            InstitutionName.Visible = false;
            DepartmentName.Visible = false;

            NationalType.Visible = false;
            EmployeeCodeBtn.Enabled = false;
            InstitutionName.Enabled = true;
            DepartmentName.Enabled = true;
            AuthorName.Enabled = true;
            MailId.Enabled = true;
            AuthorName.Text = "";
            MailId.Text = "";
            InstitutionName.Text = "";
            DepartmentName.Text = "";
        }
    }
    protected void exit(object sender, EventArgs e)
    {
        affiliateSrch.Text = "";
        popGridAffil.DataBind();
    }
    protected void AuthorNameChanged(object sender, EventArgs e)
    {
        if (affiliateSrch.Text.Trim() == "")
        {
            SqlDataSourceAffil.SelectCommand = "SELECT top 10  User_Id, prefix+' '+UPPER(firstname)+' '+UPPER(middlename)+' '+UPPER(lastname)  as Name from User_M";
            popGridAffil.DataBind();
            popGridAffil.Visible = true;
        }

        else
        {
            string name = affiliateSrch.Text.Replace(" ", String.Empty);
            SqlDataSourceAffil.SelectParameters.Clear();
            SqlDataSourceAffil.SelectParameters.Add("name", name);

            SqlDataSourceAffil.SelectCommand = "SELECT  User_Id,prefix+' '+UPPER(firstname)+' '+UPPER(middlename)+' '+UPPER(lastname)  as Name from User_M where prefix+firstname+middlename+lastname like '%' + @name + '%'";

            popGridAffil.DataBind();
            popGridAffil.Visible = true;
        }

        string rowVal = Request.Form["rowIndx"];
        int rowIndex = Convert.ToInt32(rowVal);

        ModalPopupExtender ModalPopupExtender8 = (ModalPopupExtender)Grid_AuthorEntry.Rows[rowIndex].FindControl("ModalPopupExtender4");
        ModalPopupExtender8.Show();

    }
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
    protected void popSelected1(Object sender, EventArgs e)
    {
        popGridAffil.Visible = true;
        GridViewRow row = popGridAffil.SelectedRow;

        string EmployeeCode1 = row.Cells[1].Text;
        TextBox senderBox = sender as TextBox;


        string rowVal1 = rowVal.Value;
        int rowIndex = Convert.ToInt32(rowVal1);

        TextBox EmployeeCode = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("AuthorName");
        EmployeeCode.Text = EmployeeCode1;

        affiliateSrch.Text = "";
        popGridAffil.DataBind();

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

        HiddenField employc = (HiddenField)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("EmployeeCode");
        employc.Value = EmployeeCode1;

        TextBox AuthorName = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("AuthorName");
        AuthorName.Text = a.UserNamePrefix + " " + a.UserFirstName + " " + a.UserMiddleName + " " + a.UserLastName;


        affiliateSrch.Text = "";
        popGridAffil.DataBind();
    }
    private void cleardata()
    {
        txtid.Text = string.Empty;
        txttiltle.Text = string.Empty;
        txtwriteup.Text = string.Empty;
        txtdate.Text = string.Empty;
        //DropDownbudget.Items.Clear();
        //DropDownbudget.DataSourceID = "SqlDataSourceBudget";
        //DropDownbudget.DataBind();
        SetInitialRow();

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
    
}
