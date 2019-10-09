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


public partial class GrantEntry_GrantEntryUTNupdate : System.Web.UI.Page
{
    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    string mainpath = ConfigurationManager.AppSettings["FolderPathProject"].ToString();
    Business B = new Business();
    Journal_DataObject JournalDataObj = new Journal_DataObject();
    JournalData JournalValueObj = new JournalData();
    //public string pageID = "L42";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //CompareValidator1.ValueToCompare = DateTime.Now.ToString("dd/MM/yyyy");
           // CompareValidator2.ValueToCompare = DateTime.Now.ToString("dd/MM/yyyy");
            txtAddress.Attributes.Add("maxlength", txtAddress.MaxLength.ToString());
            if (Session["Role"].ToString() == "6" || Session["Role"].ToString() == "16")
            {
                panel6.Visible = false;
                BtnSaveSan.Visible = true;
                BtnSaveBank.Visible = true;
                BtnSaveIncentive.Visible = true;
                BtnSaveOverhead.Visible = true;
                PanelAmount.Visible = false;
                PanelOPAmount.Visible = false;
                Button11.Visible = true;
            }
            else
            {
                Button11.Visible = false;
                BtnSaveSan.Visible = false;
                BtnSaveIncentive.Visible = false;
                BtnSaveOverhead.Visible = false;
                panel6.Visible = true;
                PanelAmount.Visible = false;
                PanelOPAmount.Visible = false;
            }

            setModalWindowAgency(sender, e);
            setModalWindow(sender, e);
            SetInitialRow();
            SanctionSetInitialRow();
        }

        if (Session["Role"].ToString() != "6" || Session["Role"].ToString() == "16")
        {
            lblnote.Visible = true;
        }
        else
        {
            lblnote.Visible = false;
        }

    }

    //Button Search Project click
    protected void ButtonSearchProjectOnClick(object sender, EventArgs e)
    {
        int role = Convert.ToInt16(Session["Role"]);
        if (role == 6)
        {
            GridViewsanSearch.Visible = true;
            GridViewsanSearch.EditIndex = -1;
            GridViewsanSearch.Visible = true;
            dataBind();
        }
        else if (role == 16)
        {
            GridViewsanSearch.Visible = true;
            GridViewsanSearch.EditIndex = -1;
            GridViewsanSearch.Visible = true;
            dataBind();
        }
        else
        {
            GridViewSearchGrant.Visible = true;
            GridViewSearchGrant.EditIndex = -1;
            GridViewSearchGrant.Visible = true;
            dataBind();
        }


    }

    private void dataBind()
    {
        Business obj = new Business();
        string userid = Session["UserId"].ToString();
        string unit = Session["ProjectUnit"].ToString();
        GridViewsanSearch.Visible = true;

        string role = Session["Role"].ToString();
       
            if (EntryTypesearch.SelectedValue != "A")
            {
                SqlDataSource1.SelectParameters.Clear(); 
                if (PubIDSearch.Text == "" && TextBoxtiltleSearch.Text == "")
                {
                    SqlDataSource1.SelectCommand = " select p.ID, r.TypeName,p.ProjectUnit, UTN,Title,Description,CONVERT(DECIMAL(14,0),AppliedAmount) as AppliedAmount ,q.StatusName,p.ProjectType from Project p,Status_Project_M q, ProjectType_M r where  p.ProjectType=r.TypeId and p.ProjectStatus=q.StatusId and  ProjectType=@ProjectType   and (StatusId='APP' or StatusId='REW' or StatusId='SAN') ";
                }
                else if (PubIDSearch.Text != "" && TextBoxtiltleSearch.Text == "")
                {
                    SqlDataSource1.SelectCommand = " select p.ID, r.TypeName, p.ProjectUnit,UTN,Title,Description,CONVERT(DECIMAL(14,0),AppliedAmount) as AppliedAmount,q.StatusName,p.ProjectType from Project p,Status_Project_M q, ProjectType_M r where p.ProjectType=r.TypeId and p.ProjectStatus=q.StatusId and ProjectType=@ProjectType   and ID like '%'+@ID+'%'  and (StatusId='APP' or StatusId='REW' or StatusId='SAN')  ";
                    SqlDataSource1.SelectParameters.Add("ID", PubIDSearch.Text.Trim());
                }
                else if (PubIDSearch.Text == "" && TextBoxtiltleSearch.Text != "")
                {
                    SqlDataSource1.SelectCommand = " select p.ID, r.TypeName,p.ProjectUnit, UTN,Title,Description,CONVERT(DECIMAL(14,0),AppliedAmount) as AppliedAmount,q.StatusName,p.ProjectType from Project p,Status_Project_M q, ProjectType_M r where  p.ProjectType=r.TypeId and p.ProjectStatus=q.StatusId and  ProjectType=@ProjectType   and Title  like '%'+@Title+'%' and (StatusId='APP' or StatusId='REW' or StatusId='SAN')  ";
                    SqlDataSource1.SelectParameters.Add("Title", TextBoxtiltleSearch.Text.Trim());
                }
                else
                {

                    SqlDataSource1.SelectCommand = " select p.ID, r.TypeName, p.ProjectUnit,UTN,Title,Description,CONVERT(DECIMAL(14,0),AppliedAmount) as AppliedAmount,q.StatusName,p.ProjectType from Project p,Status_Project_M q, ProjectType_M r where  p.ProjectType=r.TypeId and p.ProjectStatus=q.StatusId and  ProjectType=@ProjectType   and ID like '%'+@ID+'%' and Title  like '%'+@Title+'%' and (StatusId='APP' or StatusId='REW' or StatusId='SAN')  ";
                    SqlDataSource1.SelectParameters.Add("ID", PubIDSearch.Text.Trim());
                    SqlDataSource1.SelectParameters.Add("Title", TextBoxtiltleSearch.Text.Trim());
                }
                SqlDataSource1.SelectParameters.Add("ProjectType", EntryTypesearch.SelectedValue);
                GridViewSearchGrant.DataBind();
                SqlDataSource1.DataBind();
            }
            else
             {
                 SqlDataSource1.SelectParameters.Clear(); 
                if (PubIDSearch.Text == "" && TextBoxtiltleSearch.Text == "")
                {
                    SqlDataSource1.SelectCommand = " select p.ID, r.TypeName,p.ProjectUnit, UTN,Title,Description,CONVERT(DECIMAL(14,0),AppliedAmount) as AppliedAmount,q.StatusName,p.ProjectType from Project p,Status_Project_M q, ProjectType_M r where  p.ProjectType=r.TypeId and p.ProjectStatus=q.StatusId   and (StatusId='APP' or StatusId='REW' or StatusId='SAN')  ";
                }
                else if (PubIDSearch.Text != "" && TextBoxtiltleSearch.Text == "")
                {
                    SqlDataSource1.SelectCommand = " select p.ID, r.TypeName,p.ProjectUnit, UTN,Title,Description,CONVERT(DECIMAL(14,0),AppliedAmount) as AppliedAmount,q.StatusName,p.ProjectType from Project p,Status_Project_M q, ProjectType_M r where  p.ProjectType=r.TypeId and p.ProjectStatus=q.StatusId  and ID like '%'+@ID+'%'   and (StatusId='APP' or StatusId='REW' or StatusId='SAN') ";
                    SqlDataSource1.SelectParameters.Add("ID", PubIDSearch.Text.Trim());
                }
                else if (PubIDSearch.Text == "" && TextBoxtiltleSearch.Text != "")
                {
                    SqlDataSource1.SelectCommand = " select p.ID, r.TypeName,p.ProjectUnit, UTN,Title,Description,CONVERT(DECIMAL(14,0),AppliedAmount) as AppliedAmount,q.StatusName,p.ProjectType from Project p,Status_Project_M q, ProjectType_M r where  p.ProjectType=r.TypeId and p.ProjectStatus=q.StatusId  and Title like '%'+@Title+'%' and (StatusId='APP' or StatusId='REW' or StatusId='SAN') ";
                    SqlDataSource1.SelectParameters.Add("Title", TextBoxtiltleSearch.Text.Trim());
                }
                else
                {

                    SqlDataSource1.SelectCommand = " select p.ID, r.TypeName,p.ProjectUnit, UTN,Title,Description,CONVERT(DECIMAL(14,0),AppliedAmount) as AppliedAmount,q.StatusName,p.ProjectType from Project p,Status_Project_M q, ProjectType_M r where  p.ProjectType=r.TypeId and p.ProjectStatus=q.StatusId  and ID like '%'+@ID+'%'  and Title like '%'+@Title+'%' and (StatusId='APP' or StatusId='REW' or StatusId='SAN')   ";
                    SqlDataSource1.SelectParameters.Add("ID", PubIDSearch.Text.Trim());
                    SqlDataSource1.SelectParameters.Add("Title", TextBoxtiltleSearch.Text.Trim());
                
                }
                GridViewSearchGrant.DataBind();
                SqlDataSource1.DataBind();
            }
       
        
        GridViewsanSearch.Visible = true;
        ButtonSavepdf.Enabled = true;
    }

    //Gridview Grant Row Databound
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
        string pid = null;
        string typeEntry = null;
        string Status = null;
        if (e.CommandName == "Edit")
        {

            GridViewRow rowSelect = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
            int rowindex = rowSelect.RowIndex;
            HiddenField TypeOfEntry = (HiddenField)GridViewSearchGrant.Rows[rowindex].Cells[8].FindControl("hiddenProjectType");
            typeEntry = TypeOfEntry.Value;

            pid = GridViewSearchGrant.Rows[rowindex].Cells[3].Text.Trim().ToString();
            Status = GridViewSearchGrant.Rows[rowindex].Cells[8].Text.Trim().ToString();
            string Unit = GridViewSearchGrant.Rows[rowindex].Cells[2].Text.Trim().ToString();
            Session["TempPid"] = pid;
            Session["TempTypeEntry"] = typeEntry;//maintaining a session variable, passing it to registration page
            Session["TempStatus"] = Status;
            Session["ProjectUnit"] = Unit;

            string user = Session["UserId"].ToString();
            User u1 = new User();
            Business obj = new Business();
            //u1 = obj.get_createdby(pid, Unit);
            //string createdby = u1.createdId;
            GrantData g1 = new GrantData();
            g1 = obj.fnGrantData(pid, Unit, user);
            string createdby = g1.CreatedBy;
            if (createdby == user)
            {
                ButtonSavepdf.Enabled = true;
            }
            else if (createdby != user)
            {
                if (g1.AuthorType=="P" && g1.MUNonMU=="M")
                {
                    ButtonSavepdf.Enabled = true;
                }
                else
                {
                    ButtonSavepdf.Enabled = false;
                }

            }
            else
            {
                ButtonSavepdf.Enabled = false;
            }

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
        string Pid = Session["TempPid"].ToString();
        string TypeEntry = Session["TempTypeEntry"].ToString();
        string CurStatus = Session["TempStatus"].ToString();
        string projectunit = Session["ProjectUnit"].ToString();

        GrantData v = new GrantData();
        GrantData v1 = new GrantData();
        GrantData v2 = new GrantData();
        Business obj = new Business();

        GrantData c = new GrantData();

        v = obj.fnfindGrantid(Pid, projectunit);

        TextBoxID.Text = Pid;
        TextBoxUTN.Text = v.UTN;
       

        GrantData d = new GrantData();
       

        DropDownListTypeGrant.SelectedValue = TypeEntry;

        SqlDataSourcePrjStatus.SelectCommand = "Select * from Status_Project_M";
        DropDownListProjStatus.DataSourceID = "SqlDataSourcePrjStatus";
        DropDownListProjStatus.DataBind();
        DropDownListProjStatus.SelectedValue = v.Status;
        txtcontact.Text = v.Contact_No;
        DropDownListGrUnit.SelectedValue = v.GrantUnit;
        DropDownListSourceGrant.SelectedValue = v.GrantSource;
        txtRevisedAppliedAmt.Enabled = false;


        if (v.AppliedDate.ToShortDateString() != "01/01/0001")
        {
            TextBoxGrantDate.Text = v.AppliedDate.ToShortDateString();
        }
        if (v.GranAmount != 0)
        {
            TextBoxGrantAmt.Text = v.GranAmount.ToString();
        }
        lblRevisedAppliedAmt.Visible = true;
         txtRevisedAppliedAmt.Visible = true;
        
         if (v.RevisedAppliedAmt != 0)
         {
             txtRevisedAppliedAmt.Text = v.RevisedAppliedAmt.ToString();
         }
         else
         {
             txtRevisedAppliedAmt.Text = v.GranAmount.ToString();
         }
         if (DropDownListProjStatus.SelectedValue == "SAN")
         {
             lblsanctionorderdate.Visible = true;
             Textsanctionorderdate.Visible = true;
             if (v.SanctionOrderDate.ToShortDateString() != "01/01/0001")
             {
                 Textsanctionorderdate.Text = v.SanctionOrderDate.ToShortDateString();
             }
         }
         else
         {
             lblsanctionorderdate.Visible = false;
             Textsanctionorderdate.Visible = false;

         }
        if (v.ERFRelated != "")
        {
            DropDownListerfRelated.SelectedValue = v.ERFRelated;
        }

        TextBoxTitle.Text = v.Title;
        TextBoxAdComments.Text = v.AddtionalComments;
        TextBoxDescription.Text = v.Description;
        Textsanctionorderdate.Text = v.SanctionOrderDate.ToShortDateString();
        hdnAgencyId.Value = v.GrantingAgency;
        string agency = obj.GetAgencyName(v.GrantingAgency);
        txtagency.Text = agency;
        txtAddress.Text = v.Address;
        txtpan.Text = v.Pan_No;
        if (v.State != "")
        {
            txtstate.Text = v.State;
        }
        if (v.Country != "")
        {
            txtcountry.Text = v.Country;
        }


        txtEmailId.Text = v.AgencyEmailId;
        txtagencycontact.Text = v.AgencyContact;
        DropDownAgencyType.SelectedValue = v.TypeofAgencyGrant.ToString();
        DropDownSectorLevel.SelectedValue = v.FundingSectorLevelGrant.ToString();
        txtRework.Text = v.Remarks;
        TextBoxRemarks.Text = v.CancelFeedback;
        DropDownListProjStatus.SelectedValue = v.Status;
        if (v.SancType != "")
        {
            DropDownListSanType.SelectedValue = v.SancType;
        }
        //if (v.ProjectActualDate.ToShortDateString() != "01/01/0001")
        //{
        //    txtprojectactualdate.Text = v.ProjectActualDate.ToShortDateString();
        //}
        if (v.DurationOfProject!=0)
        {
            txtProjectDuration.Text = v.DurationOfProject.ToString();
        }
        if ((Session["Role"].ToString() == "11" || Session["Role"].ToString() == "1"))
        {
            DSforgridview.SelectParameters.Clear();
            DSforgridview.SelectParameters.Add("UserId", Session["UserId"].ToString());
            DSforgridview.SelectParameters.Add("Pid", Pid);
            DSforgridview.SelectParameters.Add("projectunit", projectunit);
            DSforgridview.SelectCommand = "select UploadPDFPath ,q.InfoTypeName as TypeName ,Remark ,AuditFrom,AuditTo, p.CreatedDate, p.CreatedBy as CreatedBy, m.FirstName as AddedBy, EntryNo,Unit_Id ,p.InfoTypeId from ProjectAuxillaryDetails p,Project_AuxInfoTypeM q, User_M m where  p.CreatedBy=@UserId and p.InfoTypeId=q.InfoTypeId and m.User_Id=p.CreatedBy  and ID=@Pid  and ProjectUnit=@projectunit  and Deleted='N' order by EntryNo";
            DSforgridview.DataBind();
            GVViewFile.DataBind();

            DSforgridview1.SelectParameters.Clear();
            DSforgridview1.SelectParameters.Add("UserId", Session["UserId"].ToString());
            DSforgridview1.SelectParameters.Add("Pid", Pid);
            DSforgridview1.SelectParameters.Add("projectunit", projectunit);
            DSforgridview1.SelectCommand = "select UploadPDFPath ,q.InfoTypeName as TypeName ,Remark ,AuditFrom,AuditTo, p.CreatedDate, p.CreatedBy as CreatedBy, m.FirstName as AddedBy, EntryNo,Unit_Id ,p.InfoTypeId from ProjectAuxillaryDetails p,Project_AuxInfoTypeM q, User_M m where  p.InfoTypeId=q.InfoTypeId and m.User_Id=p.CreatedBy  and ID=@Pid  and ProjectUnit=@projectunit  and Deleted='N' and p.CreatedBy !=@UserId  order by EntryNo";
            DSforgridview1.DataBind();
            GridView1.DataBind();
            Panel8.Visible = true;
        }

        if (Session["Role"].ToString() == "6" || Session["Role"].ToString() == "16")
        {
            DSforgridview.SelectParameters.Clear();
            DSforgridview.SelectParameters.Add("UserId", Session["UserId"].ToString());
            DSforgridview.SelectParameters.Add("Pid", Pid);
            DSforgridview.SelectParameters.Add("projectunit", projectunit);
            DSforgridview.SelectCommand = "select UploadPDFPath ,q.InfoTypeName as TypeName ,Remark ,AuditFrom,AuditTo, p.CreatedDate, p.CreatedBy as CreatedBy, m.FirstName as AddedBy, EntryNo,Unit_Id ,p.InfoTypeId from ProjectAuxillaryDetails p,Project_AuxInfoTypeM q, User_M m where  p.CreatedBy=@UserId and p.InfoTypeId=q.InfoTypeId and m.User_Id=p.CreatedBy  and ID=@Pid  and ProjectUnit=@projectunit  and Deleted='N' order by EntryNo";
            DSforgridview.DataBind();
            GVViewFile.DataBind();

            DSforgridview1.SelectParameters.Clear();
            DSforgridview1.SelectParameters.Add("UserId", Session["UserId"].ToString());
            DSforgridview1.SelectParameters.Add("Pid", Pid);
            DSforgridview1.SelectParameters.Add("projectunit", projectunit);
            DSforgridview1.SelectCommand = "select UploadPDFPath ,q.InfoTypeName as TypeName ,Remark ,AuditFrom,AuditTo, p.CreatedDate, p.CreatedBy as CreatedBy, m.FirstName as AddedBy, EntryNo,Unit_Id ,p.InfoTypeId from ProjectAuxillaryDetails p,Project_AuxInfoTypeM q, User_M m where  p.InfoTypeId=q.InfoTypeId and m.User_Id=p.CreatedBy  and ID=@Pid  and ProjectUnit=@projectunit  and Deleted='N' and p.CreatedBy !=@UserId  order by EntryNo";
            DSforgridview1.DataBind();
            GridView1.DataBind();
            Panel8.Visible = true;
            //DSforgridview.SelectCommand = "select UploadPDFPath ,q.InfoTypeName as TypeName ,Remark ,AuditFrom,AuditTo, p.CreatedDate, p.CreatedBy as CreatedBy, m.FirstName as AddedBy, EntryNo,Unit_Id ,p.InfoTypeId from ProjectAuxillaryDetails p,Project_AuxInfoTypeM q, User_M m where  p.InfoTypeId=q.InfoTypeId and m.User_Id=p.CreatedBy  and ID='" + Pid + "' and ProjectUnit='" + projectunit + "' and Deleted='N' p.CreatedBy='" + Session["UserId"].ToString() + "' order by EntryNo";
            //DSforgridview.DataBind();
            //GridView1.DataBind();
            //Panel8.Visible = true;

            //DSforgridview1.SelectCommand = "select UploadPDFPath ,q.InfoTypeName as TypeName ,Remark ,AuditFrom,AuditTo, p.CreatedDate, p.CreatedBy as CreatedBy, m.FirstName as AddedBy, EntryNo,Unit_Id ,p.InfoTypeId from ProjectAuxillaryDetails p,Project_AuxInfoTypeM q, User_M m where  p.InfoTypeId=q.InfoTypeId and m.User_Id=p.CreatedBy  and ID='" + Pid + "' and ProjectUnit='" + projectunit + "' and Deleted='N' and p.CreatedBy !='" + Session["UserId"].ToString() + "'  order by EntryNo"; 
            //DSforgridview1.DataBind();
            //GridView1.DataBind();

            setModalWindowAmount(sender, e);
            setModalWindowOPAmount(sender, e);
        }

        if (Session["Role"].ToString() != "6" &&  Session["Role"].ToString() != "16")
        {

            DropDownListInfoType.Items.Clear();
            DropDownListInfoType.Items.Add(new ListItem("Select", "", true));
            SqlDataSourceInfoType.SelectCommand = "select InfoTypeId,InfoTypeName from Project_AuxInfoTypeM where Role=11";
            DropDownListInfoType.DataSourceID = "SqlDataSourceInfoType";
            DropDownListInfoType.DataBind();
        }
        else if (Session["Role"].ToString() == "16")
        {
            DropDownListInfoType.Items.Clear();
            DropDownListInfoType.Items.Add(new ListItem("Select", "", true));
            SqlDataSourceInfoType.SelectCommand = "select InfoTypeId,InfoTypeName from Project_AuxInfoTypeM where Role=6";
            DropDownListInfoType.DataSourceID = "SqlDataSourceInfoType";
            DropDownListInfoType.DataBind();
        }
        else
        {
            DropDownListInfoType.Items.Clear();
            DropDownListInfoType.Items.Add(new ListItem("Select", "", true));
            DropDownListInfoType.DataSourceID = "SqlDataSourceInfoType";
            DropDownListInfoType.DataBind();
        }


        DataTable dy = obj.fnfindGrantInvestigatorDetails(Pid, projectunit);
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
                DropDownList DropdownMuNonMu = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[3].FindControl("DropdownMuNonMu");
                TextBox EmployeeCode = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[2].FindControl("EmployeeCode");
                ImageButton EmployeeCodeBtnimg = (ImageButton)Grid_AuthorEntry.Rows[rowIndex].Cells[2].FindControl("EmployeeCodeBtn");
                TextBox AuthorName = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[2].FindControl("AuthorName");
                TextBox InstNme = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[2].FindControl("InstitutionName");
                TextBox deptname = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[2].FindControl("DepartmentName");
                HiddenField InstId = (HiddenField)Grid_AuthorEntry.Rows[rowIndex].Cells[2].FindControl("Institution");
                HiddenField deptId = (HiddenField)Grid_AuthorEntry.Rows[rowIndex].Cells[2].FindControl("Department");
                TextBox MailId = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[2].FindControl("MailId");
                // DropDownList isCorrAuth = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[2].FindControl("isCorrAuth");
                DropDownList AuthorType = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[2].FindControl("AuthorType");
                DropDownList isLeadPI = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[2].FindControl("isLeadPI");
                DropDownList DropdownStudentInstitutionName = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[2].FindControl("DropdownStudentInstitutionName");
                DropDownList DropdownStudentDepartmentName = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[2].FindControl("DropdownStudentDepartmentName");
                DropDownList NationalType = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[2].FindControl("NationalType");
                DropDownList ContinentId = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[2].FindControl("ContinentId");
                DropDownList DropdownMuNonMu1 = (DropDownList)Grid_AuthorEntry.Rows[0].Cells[3].FindControl("DropdownMuNonMu");
                ImageButton EmployeeCodeBtnimg1 = (ImageButton)Grid_AuthorEntry.Rows[0].Cells[2].FindControl("EmployeeCodeBtn");
                ImageButton ImageButton1 = (ImageButton)Grid_AuthorEntry.Rows[rowIndex].Cells[2].FindControl("ImageButton1");


                drCurrentRow = dtCurrentTable.NewRow();

                DropdownMuNonMu.Text = dtCurrentTable.Rows[i - 1]["DropdownMuNonMu"].ToString();
                EmployeeCode.Text = dtCurrentTable.Rows[i - 1]["EmployeeCode"].ToString();
                AuthorName.Text = dtCurrentTable.Rows[i - 1]["AuthorName"].ToString();
                if (DropdownMuNonMu.Text == "M")
                {
                    NationalType.Visible = false;
                    ContinentId.Visible = false;

                    InstNme.Visible = true;
                    deptname.Visible = true;
                    DropdownStudentInstitutionName.Visible = false;
                    DropdownStudentDepartmentName.Visible = false;

                    InstNme.Text = dtCurrentTable.Rows[i - 1]["InstitutionName"].ToString();
                    deptname.Text = dtCurrentTable.Rows[i - 1]["DepartmentName"].ToString();
                    InstId.Value = dtCurrentTable.Rows[i - 1]["Institution"].ToString();
                    deptId.Value = dtCurrentTable.Rows[i - 1]["Department"].ToString();
                }

                else if (DropdownMuNonMu.Text == "N")
                {
                    InstNme.Visible = true;
                    deptname.Visible = true;
                    DropdownStudentInstitutionName.Visible = false;
                    DropdownStudentDepartmentName.Visible = false;
                    NationalType.Visible = true;

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
                }
                else if (DropdownMuNonMu.Text == "E")
                {
                    InstNme.Visible = true;
                    deptname.Visible = true;
                    DropdownStudentInstitutionName.Visible = false;
                    DropdownStudentDepartmentName.Visible = false;
                    NationalType.Visible = true;

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
                }
                else if (DropdownMuNonMu.Text == "S")
                {
                    NationalType.Visible = false;
                    ContinentId.Visible = false;

                    //InstNme.Visible = false;
                    //deptname.Visible = false;
                    //DropdownStudentInstitutionName.Visible = true;
                    //DropdownStudentDepartmentName.Visible = true;

                    //DropdownStudentInstitutionName.SelectedValue = dtCurrentTable.Rows[i - 1]["Institution"].ToString();
                    //DropdownStudentDepartmentName.SelectedValue = dtCurrentTable.Rows[i - 1]["Department"].ToString();
                    DropdownStudentInstitutionName.Visible = false;
                    DropdownStudentDepartmentName.Visible = false;
                    InstNme.Visible = true;
                    deptname.Visible = true;
                    InstNme.Text = dtCurrentTable.Rows[i - 1]["InstitutionName"].ToString();
                    deptname.Text = dtCurrentTable.Rows[i - 1]["DepartmentName"].ToString();
                    InstId.Value = dtCurrentTable.Rows[i - 1]["Institution"].ToString();
                    deptId.Value = dtCurrentTable.Rows[i - 1]["Department"].ToString();
                    ImageButton1.Visible = true;
                    EmployeeCodeBtnimg.Visible = false;
                    EmployeeCode.Enabled = false;
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
                    ImageButton1.Visible = false;
                    EmployeeCodeBtnimg.Enabled = false;
                    EmployeeCodeBtnimg.Visible = true;
                    EmployeeCode.Enabled = true;
                    NationalType.Visible = false;
                    ContinentId.Visible = false;
                }
                DropdownMuNonMu1.Enabled = false;
                EmployeeCodeBtnimg1.Enabled = false;

                MailId.Text = dtCurrentTable.Rows[i - 1]["MailId"].ToString();
                AuthorType.Text = dtCurrentTable.Rows[i - 1]["AuthorType"].ToString();
                isLeadPI.Text = dtCurrentTable.Rows[i - 1]["isLeadPI"].ToString();

                if (DropdownMuNonMu.Text == "N")
                {
                    EmployeeCodeBtnimg.Enabled = false;
                    AuthorName.Enabled = true;
                    InstNme.Enabled = true;
                    deptname.Enabled = true;
                    MailId.Enabled = true;
                }
                else if (DropdownMuNonMu.Text == "E")
                {
                    EmployeeCodeBtnimg.Enabled = false;
                    AuthorName.Enabled = true;
                    InstNme.Enabled = true;
                    deptname.Enabled = true;
                    MailId.Enabled = true;
                }
                else if (DropdownMuNonMu.Text == "M")
                {
                    EmployeeCodeBtnimg.Enabled = true;
                    AuthorName.Enabled = false;
                    InstNme.Enabled = false;
                    deptname.Enabled = false;
                    MailId.Enabled = false;
                }
                else if (DropdownMuNonMu.Text == "S")
                {
                    EmployeeCodeBtnimg.Enabled = false;
                    AuthorName.Enabled = true;
                    DropdownStudentInstitutionName.Enabled = true;
                    DropdownStudentInstitutionName.Enabled = true;
                    MailId.Enabled = true;
                }
                else
                {
                    MailId.Enabled = true;
                }
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

        setModalWindow(sender, e);



        if (v.Status == "APP")
        {

            SqlDataSourcePrjStatus.SelectCommand = "select StatusId,StatusName from Status_Project_M where StatusId='APP' or StatusId='REJ'  or  StatusId='SAN' ";
            DropDownListProjStatus.SelectedValue = v.Status;

            btnSave.Enabled = true;
            BtnAddMU.Enabled = false;
            Grid_AuthorEntry.Enabled = false;
            PanelViewUplodedfiles.Visible = false;
            Panel8.Visible = true;
            PanelUploaddetails.Enabled = false;

            DropDownListTypeGrant.Enabled = false;
            DropDownListGrUnit.Enabled = false;
            DropDownListSourceGrant.Enabled = false;
            TextBoxGrantDate.Enabled = false;
            TextBoxTitle.Enabled = false;

            PanelUploaddetails.Visible = false;
            PanelKindetails.Visible = false;
            GrantSanction.Visible = false;
            PnlBank.Visible = false;
            PanelIncentive.Visible = false;
            PanelOverhead.Visible = false;
            PanelFinanceClosure.Visible = false;
            panelReowrkRemarks.Visible = false;
            panelCanelRemarks.Visible = false;
            LabelSanType.Visible = false;
            DropDownListSanType.Visible = false;
            LabelkindDetails.Visible = false;
            TextBoxKindDetails.Visible = false;
            PanelFinanceClosure.Visible = false;
            TextKindStartDate.Visible = false;
            TextKindclosedate.Visible = false;
            kindStartdate.Visible = false;
            KindClosedate.Visible = false;
            //txtprojectactualdate.Enabled = true;
            txtProjectDuration.Enabled = false;
           
            DropDownListProjStatus.Enabled = false;
            TextBoxGrantAmt.Enabled = false;
            txtagencycontact.Enabled = false;
            txtpan.Enabled = false;
            txtEmailId.Enabled = false;
            txtAddress.Enabled = false;
            txtstate.Enabled = false;
            txtcountry.Enabled = false;
            panAddAuthor.Enabled = false;
        }
        else if (v.Status == "REJ")
        {
            PanelKindetails.Visible = false;
            GrantSanction.Visible = false;
            PnlBank.Visible = false;
            PanelIncentive.Visible = false;
            PanelOverhead.Visible = false;
            PanelFinanceClosure.Visible = false;
            panelReowrkRemarks.Visible = false;
            panelCanelRemarks.Visible = false;
            LabelSanType.Visible = false;
            DropDownListSanType.Visible = false;
            LabelkindDetails.Visible = false;
            TextBoxKindDetails.Visible = false;
            PanelFinanceClosure.Visible = false;
            TextKindStartDate.Visible = false;
            TextKindclosedate.Visible = false;
            kindStartdate.Visible = false;
            KindClosedate.Visible = false;
        }
        //else if (v.Status == "SAN")
        //{

        //    if (v.SancType == "KI")
        //    {
        //        PanelKindetails.Visible = true;
        //        GrantSanction.Visible = false;
        //        PnlBank.Visible = false;
        //        PanelIncentive.Visible = false;
        //        PanelOverhead.Visible = false;
        //        PanelFinanceClosure.Visible = false;
        //        panelReowrkRemarks.Visible = false;
        //        panelCanelRemarks.Visible = false;
        //        LabelSanType.Visible = true;
        //        DropDownListSanType.Visible = true;
        //        LabelkindDetails.Visible = true;
        //        TextBoxKindDetails.Visible = true;
        //        PanelFinanceClosure.Visible = false;
        //        kindStartdate.Visible = true;
        //        TextKindStartDate.Visible = true;
        //        KindClosedate.Visible = true;
        //        TextKindclosedate.Visible = true;
        //    }
        //    else if (v.SancType == "CA")
        //    {
        //        PanelKindetails.Visible = false;
        //        GrantSanction.Visible = true;
        //        PnlBank.Visible = false;
        //        PanelIncentive.Visible = false;
        //        PanelOverhead.Visible = false;
        //        PanelFinanceClosure.Visible = false;
        //        panelReowrkRemarks.Visible = false;
        //        panelCanelRemarks.Visible = false;
        //        LabelSanType.Visible = true;
        //        DropDownListSanType.Visible = true;
        //        LabelkindDetails.Visible = false;
        //        TextBoxKindDetails.Visible = false;
        //        PanelFinanceClosure.Visible = false;
        //        kindStartdate.Visible = false;
        //        TextKindStartDate.Visible = false;
        //        KindClosedate.Visible = false;
        //        TextKindclosedate.Visible = false;
        //    }
        //    txtProjectDuration.Enabled = false;

        //}
        else if (v.Status == "REW")
        {
            SqlDataSourcePrjStatus.SelectCommand = "select StatusId,StatusName from Status_Project_M where StatusId='REW' or StatusId='SUB'";
            DropDownListProjStatus.SelectedValue = v.Status;


            DropDownListTypeGrant.Enabled = false;
            DropDownListGrUnit.Enabled = false;
            DropDownListSourceGrant.Enabled = false;
            TextBoxGrantDate.Enabled = false;
            panelReowrkRemarks.Enabled = false;
            DropDownListSanType.Enabled = false;
            panAddAuthor.Enabled = false;
            TextBoxTitle.Enabled = false;
            PanelUploaddetails.Visible = false;
            PanelViewUplodedfiles.Visible = true;
            //if (txtprojectactualdate.Text != "")
            //{
            //    txtprojectactualdate.Enabled = false;
            //}
            //else
            //{
            //    txtprojectactualdate.Enabled = true;
            //}
            if (v.SancType == "KI")
            {
                PanelKindetails.Visible = true;
                GrantSanction.Visible = false;
                PnlBank.Visible = false;
                PanelIncentive.Visible = false;
                PanelOverhead.Visible = false;
                PanelFinanceClosure.Visible = false;
                panelReowrkRemarks.Visible = true;
                panelCanelRemarks.Visible = false;
                LabelSanType.Visible = true;
                DropDownListSanType.Visible = true;
                LabelkindDetails.Visible = true;
                TextBoxKindDetails.Visible = true;
                PanelFinanceClosure.Visible = false;
                PanelUploaddetails.Visible = false;
                kindStartdate.Visible = true;
                TextKindStartDate.Visible = true;
                KindClosedate.Visible = true;
                TextKindclosedate.Visible = true;
            }
            else if (v.SancType == "CA")
            {

                Panel2.Enabled = false;
                txtNoOFSanctions.Enabled = false;
                PanelKindetails.Visible = false;
                GrantSanction.Visible = false;
                PnlBank.Visible = false;
                PanelIncentive.Visible = false;
                PanelOverhead.Visible = false;
                PanelFinanceClosure.Visible = false;
                panelReowrkRemarks.Visible = true;
                panelCanelRemarks.Visible = false;
                DropDownListSanType.Visible = true;
                LabelSanType.Visible = true;
                LabelkindDetails.Visible = false;
                TextBoxKindDetails.Visible = false;
                PanelFinanceClosure.Visible = false;
                kindStartdate.Visible = false;
                TextKindStartDate.Visible = false;
                KindClosedate.Visible = false;
                TextKindclosedate.Visible = false;
            }
        }
        else if (v.Status == "SAN")
        {
            if (v.SancType == "KI")
            {
                SqlDataSourcePrjStatus.SelectCommand = "select StatusId,StatusName from Status_Project_M where StatusId='CLO' or StatusId='SAN'";
                DropDownListProjStatus.SelectedValue = v.Status;
                PanelKindetails.Visible = true;
                GrantSanction.Visible = false;
                PnlBank.Visible = false;
                PanelIncentive.Visible = false;
                PanelOverhead.Visible = false;
                PanelFinanceClosure.Visible = false;
                panelReowrkRemarks.Visible = false;
                panelCanelRemarks.Visible = false;
                LabelSanType.Visible = true;
                DropDownListSanType.Visible = true;
                LabelkindDetails.Visible = true;
                TextBoxKindDetails.Visible = true;
                PanelFinanceClosure.Visible = false;
                PanelUploaddetails.Visible = false;
                PanelViewUplodedfiles.Visible = true;
                DropDownListTypeGrant.Enabled = false;
                DropDownListGrUnit.Enabled = false;
                DropDownListSourceGrant.Enabled = false;
                TextBoxGrantDate.Enabled = false;
                TextBoxTitle.Enabled = false;
                panAddAuthor.Enabled = false;
                kindStartdate.Visible = true;
                TextKindStartDate.Visible = true;
                KindClosedate.Visible = true;
                TextKindclosedate.Visible = true;
                TextBoxKindDetails.Enabled = false;
                DropDownListProjStatus.Enabled = true;
                btnSave.Enabled = true;
                TextBoxGrantAmt.Enabled = false;


                DropDownListProjStatus.Enabled = false;
                TextBoxGrantAmt.Enabled = false;
                txtagencycontact.Enabled = false;
                txtpan.Enabled = false;
                txtEmailId.Enabled = false;
                txtAddress.Enabled = false;
                txtstate.Enabled = false;
                txtcountry.Enabled = false;
                panAddAuthor.Enabled = false;
            }
            else if (v.SancType == "CA")
            {
                if (Session["Role"].ToString() == "6")
                {
                    MainpanelGrant.Enabled = false;
                    PanelFinanceClosure.Enabled = true;
                }
                else if (Session["Role"].ToString() == "11" || Session["Role"].ToString() == "1")
                {
                    DropDownListTypeGrant.Enabled = false;
                    DropDownListGrUnit.Enabled = false;
                    DropDownListSourceGrant.Enabled = false;
                    TextBoxGrantDate.Enabled = false;
                    TextBoxTitle.Enabled = false;
                    GrantSanction.Enabled = false;
                    PnlBank.Enabled = false;
                    PanelIncentive.Enabled = false;
                    PanelOverhead.Enabled = false;
                    TextBoxGrantAmt.Enabled = false;
                    PanelFinanceClosure.Enabled = false;

                }

               
                panAddAuthor.Enabled = false;
                PanelKindetails.Visible = false;
                GrantSanction.Visible = false;
                PnlBank.Visible = false;
                PanelIncentive.Visible = false;
                PanelOverhead.Visible = false;
                PanelFinanceClosure.Visible = false;
                panelReowrkRemarks.Visible = false;
                panelCanelRemarks.Visible = false;
                LabelSanType.Visible = true;
                DropDownListSanType.Visible = true;
                LabelkindDetails.Visible = false;
                TextBoxKindDetails.Visible = false;
                txtNoOFSanctions.Enabled = false;
                PanelUploaddetails.Visible = false;
                PanelViewUplodedfiles.Visible = true;
                kindStartdate.Visible = false;
                TextKindStartDate.Visible = false;
                KindClosedate.Visible = false;
                TextKindclosedate.Visible = false;
                btnSave.Enabled = true;
            }
            DropDownListSanType.Enabled = false;
            txtagency.Enabled = false;
            txtagencycontact.Enabled = false;
            txtpan.Enabled = false;
            txtEmailId.Enabled = false;
            txtAddress.Enabled = false;
            txtstate.Enabled = false;
            txtcountry.Enabled = false;
            //if (txtprojectactualdate.Text != "")
            //{
            //    txtprojectactualdate.Enabled = false;
            //}
            //else
            //{
            //    txtprojectactualdate.Enabled = true;
            //}
          
        }


        if (v.Status == "SUB" || v.Status == "SAN" || v.Status == "REW")
        {
            if (v.SancType == "CA")
            {
                //Sanction Details
                v1 = obj.fnfindGrantidSanctionDetails(Pid, projectunit);

                if (v1.SanctionCapitalAmount != 0)
                {
                    TextBoxSanctionedAmountCapital.Text = v1.SanctionCapitalAmount.ToString();
                }
                if (v1.SanctionOperatingAmount != 0)
                {
                    TextBoxSanctionedAmountOperating.Text = v1.SanctionOperatingAmount.ToString();
                }
                if (v1.SanctionTotalAmount != 0)
                {
                    TextBoxSanctionedamountTotal.Text = v1.SanctionTotalAmount.ToString();
                }
                txtNoOFSanctions.Text = v1.SanctionEntryNumber.ToString();
                if (v1.SanctionDate.ToShortDateString() != "01/01/0001")
                {
                    //  TextBoxSanctionDate.Text = v1.SanctionDate.ToShortDateString();
                }
                if (v1.ProjectCommencementDate.ToShortDateString() != "01/01/0001")
                {

                    TextBoxProjectCommencementDate.Text = v1.ProjectCommencementDate.ToShortDateString();
                }
                if (v1.ProjectCloseDate.ToShortDateString() != "01/01/0001")
                {
                    TextBoxProjectCloseDate.Text = v1.ProjectCloseDate.ToShortDateString();
                }
                else
                {
                    TextBoxProjectCloseDate.Text = "";
                }
                if (v1.ExtendedDate.ToShortDateString() != "01/01/0001")
                {
                    TextBoxExtendedDate.Text = v1.ExtendedDate.ToShortDateString();
                }
                else
                {
                    TextBoxExtendedDate.Text = "";
                }
                if (v1.AccountHead != null)
                {
                    txtaccounthead.Text = v1.AccountHead.ToString();
                }
                if (v1.AuditRequired != null)
                {
                    ddlauditrequired.SelectedValue = v1.AuditRequired.ToString();
                }
                if (v1.InstitutionSahre != 0.0)
                {
                    txtInstitutionshare.Text = v1.InstitutionSahre.ToString();
                }

                if (v1.DateOfCompletion.ToShortDateString() != "01/01/0001")
                {
                    TextBox3.Text = v.DateOfCompletion.ToShortDateString();
                }
                TextBox4.Text = v.FinanceClosureRemarks;
                if (v.FinanceProjectStatus != "")
                {
                    DropDownList3.SelectedValue = v.FinanceProjectStatus;
                }

                DropDownList3.SelectedValue = v1.FinanceProjectStatus;
                DropDownList2.SelectedValue = v1.ServiceTaxApplicable;


                if (v1.FinanceProjectStatus == "OPE")
                {
                    if (Session["Role"].ToString() == "6" || Session["Role"].ToString() == "16")
                    {
                        PanelFinanceClosure.Enabled = true;
                        GrantSanction.Enabled = false;
                        PnlBank.Enabled = false;
                        PanelIncentive.Enabled = false;
                        PanelOverhead.Enabled = false;
                    }
                    else
                    {
                        PanelFinanceClosure.Visible = false;
                        PanelFinanceClosure.Enabled = false;
                        DropDownListProjStatus.Enabled = false;
                    }
                    DropDownListProjStatus.Enabled = false;
                }
                else if (v1.FinanceProjectStatus == "CLO")
                {
                    GrantSanction.Enabled = false;
                    PnlBank.Enabled = false;
                    PanelIncentive.Enabled = false;
                    PanelOverhead.Enabled = false;
                    PanelFinanceClosure.Visible = false;
                    PanelFinanceClosure.Enabled = false;
                    DropDownListProjStatus.Enabled = true;
                    SqlDataSourcePrjStatus.SelectCommand = "select StatusId,StatusName from Status_Project_M where StatusId='CLO' or StatusId='SAN'";
                    DropDownListProjStatus.SelectedValue = v.Status;
                }

                DataTable Sanctiondata = obj.SelectSanctionData(Pid, projectunit);

                if (Sanctiondata.Rows.Count != 0)
                {
                    ViewState["Sanction"] = Sanctiondata;
                    GridViewSanction.DataSource = Sanctiondata;
                    GridViewSanction.DataBind();
                    GridViewSanction.Visible = true;

                    int rowIndex2 = 0;
                    ViewState["Sanction"] = Sanctiondata;
                    DataTable table = (DataTable)ViewState["Sanction"];
                    DataRow drCurrentRow2 = null;
                    if (table != null)
                    {
                        for (int i = 1; i <= table.Rows.Count; i++)
                        {
                            TextBox sanctionNo = (TextBox)GridViewSanction.Rows[rowIndex2].Cells[0].FindControl("txtsanctionNo");
                            TextBox Sanctiondate = (TextBox)GridViewSanction.Rows[rowIndex2].Cells[1].FindControl("txtSanctiondate");
                            TextBox santotalAmount = (TextBox)GridViewSanction.Rows[rowIndex2].Cells[5].FindControl("txtsantotalAmount");
                            TextBox sancapitalAmount = (TextBox)GridViewSanction.Rows[rowIndex2].Cells[3].FindControl("txtsancapitalAmount");
                            TextBox SanOpeAmt = (TextBox)GridViewSanction.Rows[rowIndex2].Cells[4].FindControl("txtSanOpeAmt");
                            TextBox Narration = (TextBox)GridViewSanction.Rows[rowIndex2].Cells[5].FindControl("txtNarration");

                            drCurrentRow2 = table.NewRow();
                            sanctionNo.Text = table.Rows[i - 1]["SanctionNumber"].ToString();
                            if (table.Rows[i - 1]["SanctionDate"].ToString() != "")
                            {
                                DateTime date = Convert.ToDateTime(table.Rows[i - 1]["SanctionDate"].ToString());
                                Sanctiondate.Text = date.ToShortDateString();
                            }

                            if (table.Rows[i - 1]["SanctionTotalAmount"].ToString() != "")
                            {
                                double totamt = Convert.ToDouble((decimal)(table.Rows[i - 1]["SanctionTotalAmount"]));
                                santotalAmount.Text = totamt.ToString();
                            }

                            if (table.Rows[i - 1]["SanctionCapitalAmount"].ToString() != "")
                            {
                                double capamt = Convert.ToDouble((decimal)(table.Rows[i - 1]["SanctionCapitalAmount"]));
                                sancapitalAmount.Text = capamt.ToString();
                            }

                            if (table.Rows[i - 1]["SanctionOperatingAmount"].ToString() != "")
                            {
                                double opamt = Convert.ToDouble((decimal)(table.Rows[i - 1]["SanctionOperatingAmount"]));
                                SanOpeAmt.Text = opamt.ToString();
                            }
                            Narration.Text = table.Rows[i - 1]["Narration"].ToString();
                            rowIndex2++;
                            if (Session["Role"].ToString() == "11")
                            {
                                GridViewSanction.Columns[7].Visible = false;
                                GridViewSanction.Columns[4].Visible = false;
                            }
                        }

                        ViewState["Sanction"] = table;
                    }
                }
                else
                {
                    SanctionSetInitialRow();
                    if (Session["Role"].ToString() == "11")
                    {
                        GridViewSanction.Columns[7].Visible = false;
                        GridViewSanction.Columns[4].Visible = false;
                    }
                }

                DataTable dtCurrentTabledata = (DataTable)ViewState["Sanction"];
                if (dtCurrentTabledata != null)
                {
                    if (dtCurrentTabledata.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtCurrentTabledata.Rows.Count; i++)
                        {
                            Session["MiscRow" + i] = null;
                        }
                    }
                }

                Business b1 = new Business();

                DataTable tablevalue = b1.SelectSanctionOPAmountDetailsExists(Pid, projectunit);
                if (tablevalue.Rows.Count != 0)
                {
                    DataTable data1 = null;

                    if (dtCurrentTabledata.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtCurrentTabledata.Rows.Count; i++)
                        {
                            data1 = b1.SelectSanctionOPAmountDetails1(Pid, projectunit, i + 1);
                            if (data1 != null)
                            {
                                //popgridOPAmount.DataSource = data1;
                                Session["MiscRow" + i] = data1;
                            }
                        }
                    }
                }

                if (v.Status == "SAN")
                {
                    DataTable FundRecevieData = obj.SelectRecipetDetails(Pid, projectunit);

                    if (FundRecevieData.Rows.Count != 0)
                    {
                        ViewState["Bank"] = FundRecevieData;
                        GridView_bank.DataSource = FundRecevieData;
                        GridView_bank.DataBind();
                        GridView_bank.Visible = true;

                        int rowIndex1 = 0;

                        if (ViewState["Bank"] != null)
                        {
                            DataTable dtCurrentTable1 = (DataTable)ViewState["Bank"];
                            DataRow drCurrentRowRec = null;
                            if (dtCurrentTable1.Rows.Count > 0)
                            {
                                for (int i = 1; i <= dtCurrentTable1.Rows.Count; i++)
                                {
                                    DropDownList SanctionEntryNumber = (DropDownList)GridView_bank.Rows[rowIndex1].Cells[0].FindControl("ddlSanctionEntryNo");
                                    DropDownList CurrencyCode = (DropDownList)GridView_bank.Rows[rowIndex1].Cells[1].FindControl("CurrencyCode");
                                    DropDownList ModeOfRecevie = (DropDownList)GridView_bank.Rows[rowIndex1].Cells[2].FindControl("ModeOfRecevie");
                                    TextBox ReceviedDate = (TextBox)GridView_bank.Rows[rowIndex1].Cells[3].FindControl("ReceviedDate");
                                    TextBox ReceviedAmount = (TextBox)GridView_bank.Rows[rowIndex1].Cells[4].FindControl("ReceviedAmount");
                                    TextBox ReceviedINR = (TextBox)GridView_bank.Rows[rowIndex1].Cells[5].FindControl("ReceviedINR");
                                    TextBox TDS = (TextBox)GridView_bank.Rows[rowIndex1].Cells[6].FindControl("TDS");
                                    TextBox ReferenceNo = (TextBox)GridView_bank.Rows[rowIndex1].Cells[7].FindControl("ReferenceNo");
                                    ImageButton EmployeeCodeBtn1 = (ImageButton)GridView_bank.Rows[rowIndex1].Cells[8].FindControl("EmployeeCodeBtn1");

                                    // Id// 
                                    TextBox ReceivedBank = (TextBox)GridView_bank.Rows[rowIndex1].Cells[9].FindControl("ReceivedBankId");
                                    TextBox ReceivedBankName = (TextBox)GridView_bank.Rows[rowIndex1].Cells[9].FindControl("Receivedbank");
                                    ImageButton EmployeeCodeBtn2 = (ImageButton)GridView_bank.Rows[rowIndex1].Cells[9].FindControl("EmployeeCodeBtn2");

                                    // Id// 

                                    TextBox CreditedBank = (TextBox)GridView_bank.Rows[rowIndex1].Cells[10].FindControl("CreditedBankId");
                                    TextBox CreditedBankName = (TextBox)GridView_bank.Rows[rowIndex1].Cells[10].FindControl("CreditedBank");
                                    ImageButton EmployeeCodeBtn3 = (ImageButton)GridView_bank.Rows[rowIndex1].Cells[10].FindControl("EmployeeCodeBtn3");
                                    TextBox ReceivedNarration = (TextBox)GridView_bank.Rows[rowIndex1].Cells[11].FindControl("ReceivedNarration");


                                    drCurrentRow = dtCurrentTable1.NewRow();

                                    if (Session["ProjectUnit"].ToString() == "MUIND")
                                    {

                                        //CurrencyCode.SelectedValue = "INR";
                                        GridView_bank.Columns[5].Visible = false;
                                        CurrencyCode.Enabled = false;
                                    }
                                    else
                                    {

                                        CurrencyCode.Items.Remove(CurrencyCode.Items.FindByValue("INR"));
                                    }

                                    SanctionEntryNumber.SelectedValue = dtCurrentTable1.Rows[i - 1]["SanctionEntryNo"].ToString();
                                    CurrencyCode.Text = dtCurrentTable1.Rows[i - 1]["CurrencyCode"].ToString();
                                    ModeOfRecevie.Text = dtCurrentTable1.Rows[i - 1]["ModeOfReceive"].ToString();
                                    if (dtCurrentTable1.Rows[i - 1]["ReceviedDate"].ToString() != "")
                                    {
                                        DateTime date = Convert.ToDateTime(dtCurrentTable1.Rows[i - 1]["ReceviedDate"]);
                                        ReceviedDate.Text = date.ToShortDateString();
                                    }
                                    double recievedamount = Convert.ToDouble(dtCurrentTable1.Rows[i - 1]["ReceviedAmount"]);
                                    ReceviedAmount.Text = recievedamount.ToString();
                                    if (dtCurrentTable1.Rows[i - 1]["ReceviedINR"].ToString() != "")
                                    {
                                        double amount = Convert.ToDouble((decimal)dtCurrentTable1.Rows[i - 1]["ReceviedINR"]);
                                        ReceviedINR.Text = amount.ToString();
                                    }
                                    TDS.Text = dtCurrentTable1.Rows[i - 1]["TDS"].ToString();
                                    ReferenceNo.Text = dtCurrentTable1.Rows[i - 1]["ReferenceNumber"].ToString();
                                    //BankName.Text = dtCurrentTable1.Rows[i - 1]["BankName"].ToString();
                                    // ReceivedBankName.Text = dtCurrentTable1.Rows[i - 1]["BankName1"].ToString();
                                    CreditedBankName.Text = dtCurrentTable1.Rows[i - 1]["CreditedBankName"].ToString();
                                    ReceivedNarration.Text = dtCurrentTable1.Rows[i - 1]["ReceivedNarration"].ToString();

                                    ReceivedBank.Text = dtCurrentTable1.Rows[i - 1]["ReceivedBank"].ToString();
                                    CreditedBank.Text = dtCurrentTable1.Rows[i - 1]["CreditedBank"].ToString();
                                    //BankId.Text = dtCurrentTable1.Rows[i - 1]["BankId"].ToString();

                                    if (Session["ProjectUnit"].ToString() == "MUIND")
                                    {

                                        CurrencyCode.SelectedValue = "INR";
                                        GridView_bank.Columns[5].Visible = false;
                                    }
                                    if (Session["Role"].ToString() == "11")
                                    {
                                        GridView_bank.Columns[14].Visible = false;
                                    }
                                    rowIndex1++;
                                }

                                ViewState["Bank"] = dtCurrentTable1;
                            }
                        }
                        else
                        {
                            SetInitialRowBank();
                            if (Session["Role"].ToString() == "11")
                            {
                                GridView_bank.Columns[14].Visible = false;
                            }

                        }
                        //setModalWindowRB(sender, e);
                        if (Session["Role"].ToString() == "6" || Session["Role"].ToString() == "16")
                        {
                            setModalWindowCB(sender, e);
                        }
                        else
                        {
                            popupbank.Visible = false;
                        }
                    }
                    else
                    {
                        if (Session["Role"].ToString() == "6" || Session["Role"].ToString() == "16")
                        {
                            SetInitialRowBank();
                            //setModalWindowRB(sender, e);
                            setModalWindowCB(sender, e);
                        }
                        else
                        {
                            SetInitialRowBank();
                            popupbank.Visible = false;
                        }
                    }

                    //setModalWindow3(sender, e); // initialise popup gridviews



                    //Incentive

                    //Incentive details
                    DataTable dy33 = obj.SelectIncentiveDetails(Pid, projectunit);
                    if (dy33.Rows.Count != 0)
                    {
                        ViewState["IncentiveDetails"] = dy33;
                        gvIncentiveDetails.DataSource = dy33;
                        gvIncentiveDetails.DataBind();
                        gvIncentiveDetails.Visible = true;

                        int rowIndex2 = 0;

                        DataTable table = (DataTable)ViewState["IncentiveDetails"];
                        DataRow drCurrentRow2 = null;
                        if (table.Rows.Count > 0)
                        {
                            for (int i = 1; i <= table.Rows.Count; i++)
                            {
                                DropDownList SanctionEntryNumber = (DropDownList)gvIncentiveDetails.Rows[rowIndex2].Cells[0].FindControl("ddlSanctionEntryNo");
                                TextBox txtincentivedate = (TextBox)gvIncentiveDetails.Rows[rowIndex2].Cells[1].FindControl("txtincentivedate");
                                TextBox txtincentiveAmount = (TextBox)gvIncentiveDetails.Rows[rowIndex2].Cells[2].FindControl("txtincentiveAmount");
                                TextBox txtComments = (TextBox)gvIncentiveDetails.Rows[rowIndex2].Cells[3].FindControl("txtComments");

                                drCurrentRow2 = table.NewRow();
                                DateTime date = Convert.ToDateTime(table.Rows[i - 1]["IncentivePayDate"].ToString());
                                SanctionEntryNumber.SelectedValue = table.Rows[i - 1]["SanctionEntryNo"].ToString();
                                txtincentivedate.Text = date.ToShortDateString();
                                double amount = Convert.ToDouble((decimal)(table.Rows[i - 1]["IncentivePayAmount"]));
                                txtincentiveAmount.Text = amount.ToString();
                                txtComments.Text = table.Rows[i - 1]["Narration"].ToString();

                                rowIndex2++;

                            }

                            if (Session["Role"].ToString() == "11")
                            {
                                gvIncentiveDetails.Columns[5].Visible = false;
                                gvIncentiveDetails.Columns[3].Visible = false;
                            }
                            ViewState["IncentiveDetails"] = table;
                        }
                    }
                    else
                    {
                        SetIntialRowIncentive();
                        if (Session["Role"].ToString() == "11")
                        {
                            gvIncentiveDetails.Columns[5].Visible = false;
                            gvIncentiveDetails.Columns[3].Visible = false;
                        }
                    }


                    DataTable dt = new DataTable();
                    dt = (DataTable)ViewState["IncentiveDetails"];

                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                Session["MiscRowIncentive" + i] = null;
                            }
                        }
                    }


                    DataTable tablevalue1 = b1.SelectIncentiveAmountDetailsExists(Pid, projectunit);
                    if (tablevalue1.Rows.Count != 0)
                    {
                        DataTable data1 = null;

                        if (dy33.Rows.Count > 0)
                        {
                            for (int i = 0; i < dy33.Rows.Count; i++)
                            {
                                data1 = b1.SelectIncentiveAmountDetails1(Pid, projectunit, i + 1);

                                if (data1 != null)
                                {
                                    //popGridViewAmount.DataSource = data1;

                                    // popGridmisc.DataBind();
                                    Session["MiscRowIncentive" + i] = data1;
                                }
                            }
                        }
                    }


                    DataTable dy4 = obj.SelectOverheadDetails(Pid, projectunit);

                    if (dy4.Rows.Count != 0)
                    {
                        ViewState["OverheadT"] = dy4;
                        grvoverhead.DataSource = dy4;
                        grvoverhead.DataBind();
                        grvoverhead.Visible = true;

                        int rowIndex3 = 0;

                        DataTable table = (DataTable)ViewState["OverheadT"];
                        DataRow drCurrentRow3 = null;
                        if (table.Rows.Count > 0)
                        {
                            for (int i = 1; i <= table.Rows.Count; i++)
                            {
                                //TextBox Line = (TextBox)grvoverhead.Rows[rowIndex3].Cells[0].FindControl("Line");
                                DropDownList SanctionEntryNumber = (DropDownList)grvoverhead.Rows[rowIndex3].Cells[0].FindControl("ddlSanctionEntryNoOH");
                                TextBox txtOverheaddate = (TextBox)grvoverhead.Rows[rowIndex3].Cells[1].FindControl("txtOverheaddate");
                                TextBox txtOverheadAmount = (TextBox)grvoverhead.Rows[rowIndex3].Cells[2].FindControl("txtOverheadAmount");
                                TextBox txtoverheadComments = (TextBox)grvoverhead.Rows[rowIndex3].Cells[4].FindControl("txtoverheadComments");
                                TextBox txtJVNumber = (TextBox)grvoverhead.Rows[rowIndex3].Cells[3].FindControl("txtJVNumber");


                                drCurrentRow3 = table.NewRow();
                                // Line.Text = table.Rows[i - 1]["Line"].ToString(); 
                                DateTime date = Convert.ToDateTime(table.Rows[i - 1]["OverheadTDate"].ToString());
                                txtOverheaddate.Text = date.ToShortDateString();
                                double amount = Convert.ToDouble((decimal)(table.Rows[i - 1]["OverheadTAmount"]));
                                txtOverheadAmount.Text = amount.ToString();
                                txtJVNumber.Text = table.Rows[i - 1]["JVNumber"].ToString();
                                txtoverheadComments.Text = table.Rows[i - 1]["Narration"].ToString();
                                SanctionEntryNumber.SelectedValue = table.Rows[i - 1]["SanctionEntryNo"].ToString();
                                rowIndex3++;
                                if (Session["Role"].ToString() == "11")
                                {
                                    grvoverhead.Columns[5].Visible = false;
                                }

                            }


                            ViewState["OverheadT"] = table;
                        }

                    }
                    else
                    {
                        SetInitialRowOverhead();
                        if (Session["Role"].ToString() == "11")
                        {
                            grvoverhead.Columns[5].Visible = false;
                        }
                    }

                }
            }
            else if (v.SancType == "KI")
            {
                PanelKindetails.Visible = true;
                TextBoxKindDetails.Text = v.KindComments;


                if (v.KindStartDate.ToShortDateString() != "01/01/0001")
                {
                    TextKindStartDate.Text = v.KindStartDate.ToShortDateString();
                }
                if (v.KindCloseDate.ToShortDateString() != "01/01/0001")
                {
                    TextKindclosedate.Text = v.KindCloseDate.ToShortDateString();
                }
                DataTable dy1 = obj.fnfindGrantSanKindDetails(Pid, projectunit);
                ViewState["SancKindCurrentTable"] = dy1;
                GridViewkindDetails.DataSource = dy1;
                GridViewkindDetails.DataBind();
                GridViewkindDetails.Visible = true;

                int rowIndex123 = 0;

                DataTable dtCurrentTable1 = (DataTable)ViewState["SancKindCurrentTable"];
                DataRow drCurrentRow1 = null;
                if (dtCurrentTable1.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTable1.Rows.Count; i++)
                    {
                        TextBox ReceivedDate = (TextBox)GridViewkindDetails.Rows[rowIndex123].Cells[0].FindControl("ReceivedDate");
                        TextBox INREquivalent = (TextBox)GridViewkindDetails.Rows[rowIndex123].Cells[0].FindControl("INREquivalent");
                        TextBox Details = (TextBox)GridViewkindDetails.Rows[rowIndex123].Cells[0].FindControl("Details");

                        drCurrentRow1 = dtCurrentTable1.NewRow();


                        ReceivedDate.Text = dtCurrentTable1.Rows[i - 1]["ReceivedDate"].ToString();
                        INREquivalent.Text = dtCurrentTable1.Rows[i - 1]["INREquivalent"].ToString();
                        Details.Text = dtCurrentTable1.Rows[i - 1]["Details"].ToString();

                        rowIndex123++;

                    }


                    ViewState["SancKindCurrentTable"] = dtCurrentTable1;
                }

            }

        }



    }

   //Initialize row for Bank details
    private void SetInitialRowBank()
    {

        DataTable dt1 = new DataTable();
        DataRow dr1 = null;
        dt1.Columns.Add(new DataColumn("SanctionEntryNo", typeof(string)));
        dt1.Columns.Add(new DataColumn("CurrencyCode", typeof(string)));
        dt1.Columns.Add(new DataColumn("ModeOfReceive", typeof(string)));

        dt1.Columns.Add(new DataColumn("ReceviedDate", typeof(string)));
        dt1.Columns.Add(new DataColumn("ReceviedAmount", typeof(string)));
        dt1.Columns.Add(new DataColumn("ReceviedINR", typeof(string)));
       
        dt1.Columns.Add(new DataColumn("TDS", typeof(string)));
        dt1.Columns.Add(new DataColumn("ReferenceNumber", typeof(string)));
        dt1.Columns.Add(new DataColumn("BankID", typeof(string)));
        dt1.Columns.Add(new DataColumn("BankName", typeof(string)));
        dt1.Columns.Add(new DataColumn("BankName1", typeof(string)));
        dt1.Columns.Add(new DataColumn("ReceivedBank", typeof(string)));
        dt1.Columns.Add(new DataColumn("CreditedBankName", typeof(string)));
        dt1.Columns.Add(new DataColumn("CreditedBank", typeof(string)));
      
        dt1.Columns.Add(new DataColumn("ReceivedNarration", typeof(string)));

        dr1 = dt1.NewRow();
        dr1["SanctionEntryNo"] = string.Empty;
        dr1["CurrencyCode"] = string.Empty;
        dr1["ModeOfReceive"] = string.Empty;
        dr1["ReceviedDate"] = string.Empty;
        dr1["ReceviedAmount"] = string.Empty;
        dr1["ReceviedINR"] = string.Empty;
        dr1["TDS"] = string.Empty;
        dr1["ReferenceNumber"] = string.Empty; 
        dr1["BankId"] = string.Empty;
        dr1["BankName"] = string.Empty;
     
        dr1["BankName1"] = string.Empty;
        dr1["CreditedBank"] = string.Empty;
        dr1["CreditedBankName"] = string.Empty;
        dr1["ReceivedBank"] = string.Empty;     
        dr1["ReceivedNarration"] = string.Empty;

        dt1.Rows.Add(dr1);

        ViewState["Bank"] = dt1;
        GridView_bank.DataSource = dt1;
        GridView_bank.DataBind();

        if (Session["ProjectUnit"].ToString() == "MUIND")
        {
            DropDownList CurrencyCode = (DropDownList)GridView_bank.Rows[0].Cells[1].FindControl("CurrencyCode");
            CurrencyCode.SelectedValue = "INR";
            GridView_bank.Columns[5].Visible = false;
            CurrencyCode.Enabled = false;
        }
        else
        {
            DropDownList CurrencyCode = (DropDownList)GridView_bank.Rows[0].Cells[1].FindControl("CurrencyCode");
            CurrencyCode.Items.Remove(CurrencyCode.Items.FindByValue("INR"));
        }


    }
     //add new row in bank
    protected void addBank(object sender, EventArgs e)
    {
        if (GridView_bank.Rows.Count == 0)
        {
            SetInitialRowBank();
        }


        else
        {
            int rowIndex = 0;

            if (ViewState["Bank"] != null)
            {
                DataTable dtCurrentTable1 = (DataTable)ViewState["Bank"];
                DataRow drCurrentRow = null;
                if (dtCurrentTable1.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTable1.Rows.Count; i++)
                    {
                        DropDownList SanctionEntryNumber = (DropDownList)GridView_bank.Rows[rowIndex].Cells[0].FindControl("ddlSanctionEntryNo");
                        DropDownList CurrencyCode = (DropDownList)GridView_bank.Rows[rowIndex].Cells[1].FindControl("CurrencyCode");
                        DropDownList ModeOfRecevie = (DropDownList)GridView_bank.Rows[rowIndex].Cells[2].FindControl("ModeOfRecevie");
                        TextBox ReceviedDate = (TextBox)GridView_bank.Rows[rowIndex].Cells[3].FindControl("ReceviedDate");
                        TextBox ReceviedAmount = (TextBox)GridView_bank.Rows[rowIndex].Cells[4].FindControl("ReceviedAmount");
                        TextBox ReceviedINR = (TextBox)GridView_bank.Rows[rowIndex].Cells[5].FindControl("ReceviedINR");
                        TextBox TDS = (TextBox)GridView_bank.Rows[rowIndex].Cells[6].FindControl("TDS");
                        TextBox ReferenceNo = (TextBox)GridView_bank.Rows[rowIndex].Cells[7].FindControl("ReferenceNo");      
                        ImageButton EmployeeCodeBtn1 = (ImageButton)GridView_bank.Rows[rowIndex].Cells[8].FindControl("EmployeeCodeBtn1");
                        TextBox ReceivedBank = (TextBox)GridView_bank.Rows[rowIndex].Cells[9].FindControl("ReceivedBankId");
                        TextBox ReceivedBankName = (TextBox)GridView_bank.Rows[rowIndex].Cells[9].FindControl("Receivedbank");
                        ImageButton EmployeeCodeBtn2 = (ImageButton)GridView_bank.Rows[rowIndex].Cells[9].FindControl("EmployeeCodeBtn2");

                        TextBox CreditedBank = (TextBox)GridView_bank.Rows[rowIndex].Cells[10].FindControl("CreditedBankId");
                        TextBox CreditedBankName = (TextBox)GridView_bank.Rows[rowIndex].Cells[10].FindControl("CreditedBank");
                        ImageButton EmployeeCodeBtn3 = (ImageButton)GridView_bank.Rows[rowIndex].Cells[10].FindControl("EmployeeCodeBtn3");
                        TextBox ReceivedNarration = (TextBox)GridView_bank.Rows[rowIndex].Cells[11].FindControl("ReceivedNarration");


                        drCurrentRow = dtCurrentTable1.NewRow();

                        if (Session["ProjectUnit"].ToString() == "MUIND")
                        {
                           
                            CurrencyCode.SelectedValue = "INR";
                            GridView_bank.Columns[5].Visible = false;
                            CurrencyCode.Enabled = false;
                        }
                        else
                        {
                            
                            CurrencyCode.Items.Remove(CurrencyCode.Items.FindByValue("INR"));
                        }
                        dtCurrentTable1.Rows[i - 1]["SanctionEntryNo"] = SanctionEntryNumber.SelectedValue;
                        dtCurrentTable1.Rows[i - 1]["CurrencyCode"] = CurrencyCode.SelectedValue;
                        dtCurrentTable1.Rows[i - 1]["ModeOfReceive"] = ModeOfRecevie.SelectedValue;
                        if (ReceviedDate.Text != "")
                        {
                            dtCurrentTable1.Rows[i - 1]["RecevieDdate"] = ReceviedDate.Text;
                        }
                        if (ReceviedAmount.Text != "")
                        {
                            dtCurrentTable1.Rows[i - 1]["ReceviedAmount"] = ReceviedAmount.Text;
                        }
                        if (ReceviedINR.Text != "")
                        {
                            dtCurrentTable1.Rows[i - 1]["ReceviedINR"] = ReceviedINR.Text;
                        }
                        else
                        {
                            dtCurrentTable1.Rows[i - 1]["ReceviedINR"] = "0.0";
                        }
                        if (TDS.Text != "")
                        {
                            dtCurrentTable1.Rows[i - 1]["TDS"] = TDS.Text;
                        }
                        if (ReferenceNo.Text != "")
                        {
                            dtCurrentTable1.Rows[i - 1]["ReferenceNumber"] = ReferenceNo.Text;
                        }
                        //if (BankName.Text != "")
                        //{
                        //    dtCurrentTable1.Rows[i - 1]["BankName"] = BankName.Text;
                        //}
                        //if (ReceivedBankName.Text != "")
                        //{
                        //    dtCurrentTable1.Rows[i - 1]["BankName1"] = ReceivedBankName.Text;
                        //}
                        if (CreditedBankName.Text != "")
                        {
                            dtCurrentTable1.Rows[i - 1]["CreditedBankName"] = CreditedBankName.Text;
                        }
                        if (ReceivedBank.Text != "")
                        {
                            dtCurrentTable1.Rows[i - 1]["ReceivedBank"] = ReceivedBank.Text;
                        }

                        if (CreditedBank.Text != "")
                        {
                            dtCurrentTable1.Rows[i - 1]["CreditedBank"] = CreditedBank.Text;
                        }
                        //if (BankId.Text != "")
                        //{
                        //    dtCurrentTable1.Rows[i - 1]["BankId"] = BankId.Text;
                        //}
                        if (ReceivedNarration.Text != "")
                        {
                            dtCurrentTable1.Rows[i - 1]["ReceivedNarration"] = ReceivedNarration.Text;
                        }
                        rowIndex++;
                    }

                    dtCurrentTable1.Rows.Add(drCurrentRow);
                    ViewState["Bank"] = dtCurrentTable1;

                    GridView_bank.DataSource = dtCurrentTable1;
                    GridView_bank.DataBind();
                }
            }
            else
            {
                Response.Write("ViewState is null");
            }

            SetPreviousDataBank();
        }

       // setModalWindow3(sender, e); // initialise popup gridviews
        //setModalWindowRB(sender, e);
        setModalWindowCB(sender, e);
    }

    private void SetPreviousDataBank()
    {
        int rowIndex = 0;
        if (ViewState["Bank"] != null)
        {
            DataTable dt = (DataTable)ViewState["Bank"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DropDownList SanctionEntryNumber = (DropDownList)GridView_bank.Rows[rowIndex].Cells[0].FindControl("ddlSanctionEntryNo");
                    DropDownList CurrencyCode = (DropDownList)GridView_bank.Rows[rowIndex].Cells[1].FindControl("CurrencyCode");
                    DropDownList ModeOfReceive = (DropDownList)GridView_bank.Rows[rowIndex].Cells[2].FindControl("ModeOfRecevie");
                    TextBox ReceviedDate = (TextBox)GridView_bank.Rows[rowIndex].Cells[3].FindControl("ReceviedDate");
                    TextBox ReceviedAmount = (TextBox)GridView_bank.Rows[rowIndex].Cells[4].FindControl("ReceviedAmount");
                    TextBox ReceviedINR = (TextBox)GridView_bank.Rows[rowIndex].Cells[5].FindControl("ReceviedINR");
                    TextBox TDS = (TextBox)GridView_bank.Rows[rowIndex].Cells[6].FindControl("TDS");
                    TextBox ReferenceNo = (TextBox)GridView_bank.Rows[rowIndex].Cells[7].FindControl("ReferenceNo");
                    //TextBox BankName = (TextBox)GridView_bank.Rows[rowIndex].Cells[8].FindControl("BankName");
                    ImageButton EmployeeCodeBtn1 = (ImageButton)GridView_bank.Rows[rowIndex].Cells[8].FindControl("EmployeeCodeBtn1");
                    //TextBox BankId = (TextBox)GridView_bank.Rows[rowIndex].Cells[8].FindControl("BankId");

                    // Id// 
                    TextBox ReceivedBank = (TextBox)GridView_bank.Rows[rowIndex].Cells[9].FindControl("ReceivedBankId");
                    TextBox ReceivedBankName = (TextBox)GridView_bank.Rows[rowIndex].Cells[9].FindControl("ReceivedBank");
                    ImageButton EmployeeCodeBtn2 = (ImageButton)GridView_bank.Rows[rowIndex].Cells[9].FindControl("EmployeeCodeBtn2");

                    // Id// 

                    TextBox CreditedBank = (TextBox)GridView_bank.Rows[rowIndex].Cells[10].FindControl("CreditedBankId");
                    TextBox CreditedBankName = (TextBox)GridView_bank.Rows[rowIndex].Cells[10].FindControl("CreditedBank");
                    ImageButton EmployeeCodeBtn3 = (ImageButton)GridView_bank.Rows[rowIndex].Cells[10].FindControl("EmployeeCodeBtn3");
                    TextBox ReceivedNarration = (TextBox)GridView_bank.Rows[rowIndex].Cells[11].FindControl("ReceivedNarration");

                    SanctionEntryNumber.Text = dt.Rows[i]["SanctionEntryNo"].ToString();
                    CurrencyCode.Text = dt.Rows[i]["CurrencyCode"].ToString();
                    ModeOfReceive.Text = dt.Rows[i]["ModeOfReceive"].ToString();
                    if (dt.Rows[i]["ReceviedDate"].ToString() != "")
                    {
                        DateTime date = Convert.ToDateTime(dt.Rows[i]["ReceviedDate"]);
                        ReceviedDate.Text = date.ToShortDateString();
                    }
                    ReceviedAmount.Text = dt.Rows[i]["ReceviedAmount"].ToString();
                    if (dt.Rows[i]["ReceviedINR"].ToString() != "0.0")
                    {
                        ReceviedINR.Text = dt.Rows[i]["ReceviedINR"].ToString();
                    }
                    TDS.Text = dt.Rows[i]["TDS"].ToString();
                    ReferenceNo.Text = dt.Rows[i]["ReferenceNumber"].ToString();
                    //BankName.Text = dt.Rows[i]["BankName"].ToString();
                    //ReceivedBankName.Text = dt.Rows[i]["BankName1"].ToString();
                    ReceivedBank.Text = dt.Rows[i]["ReceivedBank"].ToString();
                    CreditedBankName.Text = dt.Rows[i]["CreditedBankName"].ToString();
                    CreditedBank.Text = dt.Rows[i]["CreditedBank"].ToString();
                    ReceivedNarration.Text = dt.Rows[i]["ReceivedNarration"].ToString();

                    if (Session["ProjectUnit"].ToString() == "MUIND")
                    {

                        CurrencyCode.SelectedValue = "INR";
                        GridView_bank.Columns[5].Visible = false;
                        CurrencyCode.Enabled = false;
                    }
                    else
                    {

                        CurrencyCode.Items.Remove(CurrencyCode.Items.FindByValue("INR"));
                    }
                    //BankId.Text = dt.Rows[i]["BankId"].ToString();
                    rowIndex++;
                }
            }
        }
    }

    protected void Grid_Bank_RowDeleting(Object sender, GridViewDeleteEventArgs e)
    {
        SetRowDataBank();
        if (ViewState["Bank"] != null)
        {
            DataTable dt = (DataTable)ViewState["Bank"];
            DataRow drCurrentRow = null;
            int rowIndex = Convert.ToInt32(e.RowIndex);
            if (dt.Rows.Count > 1 && rowIndex != 0)
            {
                dt.Rows.Remove(dt.Rows[rowIndex]);
                drCurrentRow = dt.NewRow();
                ViewState["Bank"] = dt;
                GridView_bank.DataSource = dt;
                GridView_bank.DataBind();

                SetPreviousDataBank();
                // gridAmtChanged(sender, e);
            }
        }
    }


    private void SetRowDataBank()
    {
        int rowIndex = 0;

        if (ViewState["Bank"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["Bank"];
            DataRow drCurrentRow = null;
            if (dtCurrentTable.Rows.Count > 0)
            {
                for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                {
                    DropDownList SanctionEntryNumber = (DropDownList)GridView_bank.Rows[0].Cells[0].FindControl("ddlSanctionEntryNo");
                    DropDownList CurrencyCode = (DropDownList)GridView_bank.Rows[rowIndex].Cells[1].FindControl("CurrencyCode");
                    DropDownList ModeOfReceive = (DropDownList)GridView_bank.Rows[rowIndex].Cells[2].FindControl("ModeOfRecevie");
                    TextBox ReceviedDate = (TextBox)GridView_bank.Rows[rowIndex].Cells[3].FindControl("ReceviedDate");
                    TextBox ReceviedAmount = (TextBox)GridView_bank.Rows[rowIndex].Cells[4].FindControl("ReceviedAmount");
                    TextBox ReceviedINR = (TextBox)GridView_bank.Rows[rowIndex].Cells[5].FindControl("ReceviedINR");
                    TextBox TDS = (TextBox)GridView_bank.Rows[rowIndex].Cells[6].FindControl("TDS");
                    TextBox ReferenceNo = (TextBox)GridView_bank.Rows[rowIndex].Cells[7].FindControl("ReferenceNo");
                    //TextBox BankName = (TextBox)GridView_bank.Rows[rowIndex].Cells[8].FindControl("BankName");
                    ImageButton EmployeeCodeBtn1 = (ImageButton)GridView_bank.Rows[rowIndex].Cells[8].FindControl("EmployeeCodeBtn1");
                    //TextBox BankId = (TextBox)GridView_bank.Rows[rowIndex].Cells[8].FindControl("BankId");

                    // Id// 
                    TextBox ReceivedBank = (TextBox)GridView_bank.Rows[rowIndex].Cells[9].FindControl("ReceivedBankId");
                    TextBox ReceivedBankName = (TextBox)GridView_bank.Rows[rowIndex].Cells[9].FindControl("ReceivedBankName");
                    ImageButton EmployeeCodeBtn2 = (ImageButton)GridView_bank.Rows[rowIndex].Cells[9].FindControl("EmployeeCodeBtn2");

                    // Id// 

                    TextBox CreditedBank = (TextBox)GridView_bank.Rows[rowIndex].Cells[10].FindControl("CreditedBankId");
                    TextBox CreditedBankName = (TextBox)GridView_bank.Rows[rowIndex].Cells[10].FindControl("CreditedBankName");
                    ImageButton EmployeeCodeBtn3 = (ImageButton)GridView_bank.Rows[rowIndex].Cells[10].FindControl("EmployeeCodeBtn3");
                    TextBox ReceivedNarration = (TextBox)GridView_bank.Rows[rowIndex].Cells[11].FindControl("ReceivedNarration");
                    drCurrentRow = dtCurrentTable.NewRow();

                }
                ViewState["Bank"] = dtCurrentTable;

            }

            else
            {
                Response.Write("ViewState is null");
            }
            //SetPreviousData();
        }

    }

    protected void RowDataBoundBank(Object sender, GridViewRowEventArgs e)
    {
        if (txtNoOFSanctions.Text != "")
        {
            int number = Convert.ToInt16(txtNoOFSanctions.Text);
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Find the drop-down (say in 3rd column)
                var dd = e.Row.Cells[1].Controls[0] as DropDownList;
                DropDownList ddl = (DropDownList)e.Row.FindControl("ddlSanctionEntryNo");
                for (int i = 1; i <= number; i++)
                {
                    string value = i.ToString();
                    ddl.Items.Add(new ListItem(value));
                }
            }
        }

    }

    protected void setModalWindow3(object sender, EventArgs e)
    {
        popupbank.Visible = true;
        popupbankGrid.DataSourceID = "SqlDataSource3";
        SqlDataSource3.DataBind();
        popupbankGrid.DataBind();
        popupbankGrid.Visible = true;
    }

    protected void popSelected2(Object sender, EventArgs e)
    {
        popupbankGrid.Visible = true;
        GridViewRow row = popupbankGrid.SelectedRow;
        string rowVal1 = rowVal.Value;
        int rowIndex = Convert.ToInt32(rowVal1);

        Business b = new Business();
        User a = new User();
        //    string BankName1 = null;
        // BankName1 = b.GetBankName(a.BankId);
        string BankName1 = row.Cells[2].Text;
        string id = row.Cells[1].Text;

        //TextBox BankName = (TextBox)GridView_bank.Rows[rowIndex].Cells[2].FindControl("BankName");
        TextBox BankName = (TextBox)GridView_bank.Rows[rowIndex].Cells[8].FindControl("BankName");

        BankName.Text = BankName1;

        TextBox BankName2 = (TextBox)GridView_bank.Rows[rowIndex].Cells[5].FindControl("BankId");
        BankName2.Text = id;
        txtbankname.Text = "";
        popupbankGrid.DataBind();

    }

    //ReciveBnak
    protected void popSelectedRecBank(Object sender, EventArgs e)
    {
        popupRbank.Visible = true;
        GridViewRow row = popupRecBank.SelectedRow;
        string rowVal1 = rowVal.Value;
        int rowIndex = Convert.ToInt32(rowVal1);

        Business b = new Business();
        User a = new User();
        //    string BankName1 = null;
        // BankName1 = b.GetBankName(a.BankId);        

        //TextBox BankName = (TextBox)GridView_bank.Rows[rowIndex].Cells[2].FindControl("BankName");
        string ReceivedBankName1 = row.Cells[2].Text;
        string RId = row.Cells[1].Text;
        TextBox ReceivedBankName = (TextBox)GridView_bank.Rows[rowIndex].Cells[9].FindControl("Receivedbank");

        ReceivedBankName.Text = ReceivedBankName1;
        TextBox ReceivedBank = (TextBox)GridView_bank.Rows[rowIndex].Cells[11].FindControl("ReceivedBankId");

        ReceivedBank.Text = RId;
        popupRecBank.DataBind();

    }

    protected void setModalWindowRB(object sender, EventArgs e)
    {


        popupRbank.Visible = true;
        popupRecBank.DataSourceID = "SqlDataSourceRecB";
        SqlDataSourceRecB.DataBind();
        popupRecBank.DataBind();
        popupRecBank.Visible = true;
    }

    //Credited bank


    protected void popSelectedCreBank(Object sender, EventArgs e)
    {
        popupCrB.Visible = true;
        GridViewRow row = popupCrB.SelectedRow;
        string rowVal1 = rowVal.Value;
        int rowIndex = Convert.ToInt32(rowVal1);

        Business b = new Business();
        User a = new User();
        //    string BankName1 = null;
        // BankName1 = b.GetBankName(a.BankId);        

        //TextBox BankName = (TextBox)GridView_bank.Rows[rowIndex].Cells[2].FindControl("BankName");
        string CreditedBankName1 = row.Cells[2].Text;
        string CId = row.Cells[1].Text;

        TextBox CreditedBankName = (TextBox)GridView_bank.Rows[rowIndex].Cells[10].FindControl("CreditedBank");
        CreditedBankName.Text = CreditedBankName1;
        TextBox CreditedBank = (TextBox)GridView_bank.Rows[rowIndex].Cells[10].FindControl("CreditedBankId");
        CreditedBank.Text = CId;
        popupCrB.DataBind();

    }

    protected void setModalWindowCB(object sender, EventArgs e)
    {


        popupCbank.Visible = true;
        popupCrB.DataSourceID = "SqlDataSourceCreB";
        SqlDataSourceCreB.DataBind();
        popupCrB.DataBind();
        popupCrB.Visible = true;
    }

    //search
    protected void branchNameChanged1(object sender, EventArgs e)
    {
        if (txtbankname.Text.Trim() == "")
        {
            SqlDataSourceAffil.SelectCommand = "SELECT top 10 BankID, BankName FROM ProjectBank_M";
            popupbank.DataBind();
            popupbank.Visible = true;
        }

        else
        {
            SqlDataSourceAffil.SelectParameters.Clear();
            SqlDataSourceAffil.SelectParameters.Add("BankName", txtbankname.Text);
            SqlDataSourceAffil.SelectCommand = "SELECT  BankID, BankName FROM ProjectBank_M where BankName  like '%'+@BankName +'%'";

            popupbank.DataBind();
            popupbank.Visible = true;
        }


        string rowVal = Request.Form["rowIndx"];
        int rowIndex = Convert.ToInt32(rowVal);

        ModalPopupExtender ModalPopupExtender9 = (ModalPopupExtender)GridView_bank.Rows[rowIndex].FindControl("ModalPopupExtender5");
        ModalPopupExtender9.Show();

        //setModalWindow(sender, e);

    }
    
    

    private void cleardata()
    {
        TextBox5.Text ="";
        //txtprojectactualdate.Text = "";
        AuditFrom.Text = "";
        AuditTo.Text = "";
        TextBoxSanctionedAmountCapital.Text = "";
        TextBoxSanctionedAmountOperating.Text = "";
        TextBoxSanctionedamountTotal.Text = "";
        TextBoxProjectCommencementDate.Text = "";
        TextBoxProjectCloseDate.Text = "";
        TextBoxExtendedDate.Text = "";
        ddlauditrequired.SelectedValue = "0";
        txtInstitutionshare.Text = "";
        txtaccounthead.Text = "";
        TextKindStartDate.Text = "";
        TextKindclosedate.Text = "";
        DropDownListSanType.Items.Clear();
        DropDownListSanType.DataSourceID = "SqlDataSourceDropDownListSanType";
        DropDownListSanType.DataBind();
        txtProjectDuration.Text = "";
    }

    //Drop Down list Project status change
    protected void DropDownListProjStatusOnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownListProjStatus.SelectedValue == "SAN")
        {
            GrantSanction.Visible = false;
            LabelSanType.Visible = true;
            DropDownListSanType.Visible = true;
            panelCanelRemarks.Visible = false;


            BtnAddMU.Enabled = false;
            Grid_AuthorEntry.Enabled = false;
            PanelUploaddetails.Visible = false;
            PanelViewUplodedfiles.Visible = true;
            PanelUploaddetails.Enabled = false;
            btnSave.Enabled = true;
            TextBoxDescription.Enabled = false;
            TextBoxAdComments.Enabled = false;
            txtcontact.Enabled = false;
            TextBoxGrantAmt.Enabled = true;
            DropDownListerfRelated.Enabled = false;
            txtagencycontact.Enabled = true;
            txtpan.Enabled = true;
            txtEmailId.Enabled = true;
            txtAddress.Enabled = true;
            txtstate.Enabled = true;
            txtcountry.Enabled = true;
            lblRevisedAppliedAmt.Visible = true;
            txtRevisedAppliedAmt.Visible = true;           
            txtRevisedAppliedAmt.Enabled = true;
            lblsanctionorderdate.Visible = true;
            Textsanctionorderdate.Visible = true;
            Textsanctionorderdate.Enabled = true;
            DropDownListSanTypeOnSelectedIndexChanged(sender, e);
            if (Session["Role"].ToString() == "11" || Session["Role"].ToString() == "1")
            {
                addsanction.Enabled = false;
                GridViewSanction.Enabled = false;
                txtNoOFSanctions.Text = "1";
                txtNoOFSanctions.Enabled = false;
            }
            else
            {
                addsanction.Enabled = true;
                GridViewSanction.Enabled = true;
                txtNoOFSanctions.Text = "1";
                txtNoOFSanctions.Enabled = true;
            }
            TextBoxSanctionedAmountCapital.Enabled = false;
            TextBoxSanctionedAmountOperating.Enabled = false;
            TextBoxSanctionedamountTotal.Enabled = false;

           
        }
        else if (DropDownListProjStatus.SelectedValue == "APP")
        {
            GrantSanction.Visible = false;
            LabelSanType.Visible = false;
            DropDownListSanType.Visible = false;
            BtnAddMU.Enabled = true;
            Grid_AuthorEntry.Enabled = true;
            panelCanelRemarks.Visible = false;
            PanelUploaddetails.Enabled = false;
            TextKindStartDate.Visible = false;
            TextKindclosedate.Visible = false;
            TextBoxKindDetails.Visible = false;
            LabelkindDetails.Visible = false;
            KindClosedate.Visible = false;
            kindStartdate.Visible = false;
            PanelKindetails.Visible = false;
            txtRevisedAppliedAmt.Enabled = false;
        }

        else if (DropDownListProjStatus.SelectedValue == "REJ")
        {
            panelCanelRemarks.Visible = true;
            GrantSanction.Visible = false;
            TextBoxRemarks.Enabled = true;
            LabelSanType.Visible = false;
            DropDownListSanType.Visible = false;
            PanelKindetails.Visible = false;
            TextBoxGrantAmt.Enabled = false;
            txtcontact.Enabled = false;
            TextBoxDescription.Enabled = false;
            TextBoxAdComments.Enabled = false;
            DropDownListerfRelated.Enabled = false;
            Panel10.Enabled = false;
            PanelUploaddetails.Enabled = false;
            BtnAddMU.Enabled = false;
            Grid_AuthorEntry.Enabled = false;
            panelReowrkRemarks.Visible = false;
            TextKindStartDate.Visible = false;
            TextKindclosedate.Visible = false;
            TextBoxKindDetails.Visible = false;
            LabelkindDetails.Visible = false;
            KindClosedate.Visible = false;
            kindStartdate.Visible = false;
            txtRevisedAppliedAmt.Enabled = false;
        }
    }

    //Drop Down list Sanction Type changed
    protected void DropDownListSanTypeOnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownListSanType.SelectedValue == "CA")
        {
            GrantSanction.Visible = true;
            PanelKindetails.Visible = false;
            LabelSanType.Visible = true;
            DropDownListSanType.Visible = true;
            LabelkindDetails.Visible = false;
            TextBoxKindDetails.Visible = false;
            kindStartdate.Visible = false;
            TextKindStartDate.Visible = false;
            TextKindclosedate.Visible = false;
            KindClosedate.Visible = false;

            if (Session["Role"].ToString() == "11" || Session["Role"].ToString() == "1")
            {
                addsanction.Enabled = false;
                GridViewSanction.Enabled = false;
                txtNoOFSanctions.Text = "1";
                txtNoOFSanctions.Enabled = false;
            }
            else
            {
                addsanction.Enabled = true;
                GridViewSanction.Enabled = true;
                txtNoOFSanctions.Text = "1";
                txtNoOFSanctions.Enabled = true;
            }
        }
        else if (DropDownListSanType.SelectedValue == "KI")
        {
            GrantSanction.Visible = false;
            PanelKindetails.Visible = true;
            LabelSanType.Visible = true;
            DropDownListSanType.Visible = true;
            LabelkindDetails.Visible = true;
            TextBoxKindDetails.Visible = true;
            kindStartdate.Visible = true;
            TextKindStartDate.Visible = true;
            TextKindclosedate.Visible = true;
            KindClosedate.Visible = true;
            if (ViewState["SancKindCurrentTable"] == null)
            {
                SetInitialSancKindRow();
            }
        }
        else
        {
            DropDownListSanType.Enabled = true;

            //div1.Visible = true;
            //GrantSanction.Visible = true;
            //Panel2.Visible = false;
            //Panel3.Visible = false;
            //Panel4.Visible = false;
            //PanelKindetails.Visible = false;
            //LabelSanType.Visible = true;
            //DropDownListSanType.Visible = true;
            //LabelkindDetails.Visible = false;
            //TextBoxKindDetails.Visible = false;
        }
    }

    //Agency textbox changed
    protected void AgencyIdChanged(object sender, EventArgs e)
    {
        if (agencysearch.Text.Trim() == "")
        {
            SqlDataSourceTextBoxGrantAgency.SelectCommand = "SELECT   FundingAgencyId as Id,UPPER([FundingAgencyName]) as Name FROM [ProjectFundingAgency_M]";
            popGridagency.DataBind();
            popGridagency.Visible = true;
        }

        else
        {
            SqlDataSourceTextBoxGrantAgency.SelectParameters.Clear();
            SqlDataSourceTextBoxGrantAgency.SelectParameters.Add("FundingAgencyName", agencysearch.Text);
            SqlDataSourceTextBoxGrantAgency.SelectCommand = "SELECT   FundingAgencyId as Id,UPPER([FundingAgencyName]) as Name FROM [ProjectFundingAgency_M] where FundingAgencyName like '%'+@FundingAgencyName+'%'";
            popGridagency.DataBind();
            popGridagency.Visible = true;
            popupselectNo.Visible = true;
        }

        model.Show();
    }


    //Investigator

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
        EmployeeCode.Text= Session["UserId"].ToString();

        Institution.Value = Session["InstituteId"].ToString();
        string InstituteId = Session["InstituteId"].ToString();
        Business b = new Business();

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


        if (DropdownMuNonMu.SelectedValue == "M")
        {
            EmployeeCodeBtn.Enabled = false;
        }
        else if (DropdownMuNonMu.SelectedValue == "N")
        {
            EmployeeCodeBtn.Enabled = false;
        }
        else if (DropdownMuNonMu.SelectedValue == "E")
        {
            EmployeeCodeBtn.Enabled = false;
        }
        else if (DropdownMuNonMu.SelectedValue == "S")
        {
            EmployeeCodeBtn.Enabled = false;
        }
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
        else if (DropdownMuNonMu.SelectedValue == "N" || DropdownMuNonMu.SelectedValue == "E")
        {


            ContinentId.Visible = false;


        }
        else
        {
            ContinentId.Visible = false;

        }

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
                        TextBox EmployeeCode = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("EmployeeCode");
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
                        else if (DropdownMuNonMu.Text == "E")
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
                        else if (DropdownMuNonMu.Text == "O")
                        {
                            EmployeeCode.Visible = true;
                            EmployeeCode.Enabled = true;
                            DropdownStudentInstitutionName.Visible = true;
                            DropdownStudentDepartmentName.Visible = true;
                            InstitutionName.Visible = false;
                            DepartmentName.Visible = false;
                            NationalType.Visible = false;
                            ContinentId.Visible = false;
                            AuthorName.Enabled = false;
                            EmployeeCodeBtnImg.Visible = true;
                            EmployeeCodeBtnImg.Enabled = false;


                            dtCurrentTable.Rows[i - 1]["Institution"] = DropdownStudentInstitutionName.SelectedValue;
                            dtCurrentTable.Rows[i - 1]["InstitutionName"] = DropdownStudentInstitutionName.SelectedItem;
                            dtCurrentTable.Rows[i - 1]["Department"] = DropdownStudentDepartmentName.SelectedValue;
                            dtCurrentTable.Rows[i - 1]["DepartmentName"] = DropdownStudentDepartmentName.SelectedItem;
                            dtCurrentTable.Rows[i - 1]["EmployeeCode"] = EmployeeCode.Text.Trim();

                            NationalType.Visible = false;
                            ContinentId.Visible = false;

                            dtCurrentTable.Rows[i - 1]["NationalType"] = NationalType.SelectedValue;
                            dtCurrentTable.Rows[i - 1]["ContinentId"] = ContinentId.SelectedValue;
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
                    HiddenField EmployeeCode = (HiddenField)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("EmployeeCode");
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
                    EmployeeCode.Value = dt.Rows[i]["EmployeeCode"].ToString();
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
                    else if (DropdownMuNonMu.Text == "E")
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
                    HiddenField EmployeeCode = (HiddenField)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("EmployeeCode");
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
                    else if (DropdownMuNonMu.Text == "E")
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
        else if (DropdownMuNonMu.SelectedValue == "E")
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

   //Upload Functionality

    //Button Upload click
    protected void BtnUploadPdf_Click(object sender, EventArgs e)
    {
        if (!Page.IsValid)
        {
            return;
        }
        FilePfdGrantSave(sender, e);
    }

    private void FilePfdGrantSave(object sender, EventArgs e)
    {
        try
        {
            
            string filelocationpath = "";
            string UploadPdf1 = "";
            int result1 = 0;
            GrantData j = new GrantData();
            string PublicationEntry1 = DropDownListTypeGrant.SelectedValue;
            
            if (FileUploadPdf.HasFile)
            {
                string uploadedfilename = Path.GetFileName(FileUploadPdf.PostedFile.FileName);
                double size = FileUploadPdf.PostedFile.ContentLength;

                if (size < 4194304) //4mb
                {

                    Stream fs = FileUploadPdf.PostedFile.InputStream;
                    BinaryReader br = new BinaryReader(fs);
                    byte[] bytes = br.ReadBytes((Int32)fs.Length);
                    bool exeresult = false;
                    Business B = new Business();
                    exeresult = B.IsExeFile(bytes);




                    if (exeresult == true)
                    {
                        string CloseWindow1 = "alert('Uploaded file is not a valid file.Please upload a valid pdf file.')";
                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow1, true);
                        return;
                    }
                    string servername = ConfigurationManager.AppSettings["ServerName"].ToString();
                    string domainame = ConfigurationManager.AppSettings["DomainName"].ToString();
                    string username = ConfigurationManager.AppSettings["UserName"].ToString();
                    string password = ConfigurationManager.AppSettings["Password"].ToString();
                    string FolderPathServerwrite = ConfigurationManager.AppSettings["FolderPathProject"].ToString();
                    using (NetworkShareAccesser.Access(servername, domainame, username, password))
                    {
                        var uploadfolder = FolderPathServerwrite;
                        string path_BoxId = Path.Combine(uploadfolder, TextBoxID.Text); //main path + location
                        if (!Directory.Exists(path_BoxId))   //if directory doesnt exist
                        {
                            Directory.CreateDirectory(path_BoxId);//crete directory of location
                        }
                        string path_BoxId_ProType = Path.Combine(path_BoxId, PublicationEntry1);

                        if (!Directory.Exists(path_BoxId_ProType))   //if directory doesnt exist
                        {
                            Directory.CreateDirectory(path_BoxId_ProType);//crete directory of department
                        }

                        string uploadedfilename1 = Path.GetFileName(FileUploadPdf.PostedFile.FileName);
                        string timestamp = DateTime.Now.ToString("dd-MM-yy-hh-mm-ss");
                        string fileextension = Path.GetExtension(uploadedfilename);
                        string actualfilenamewithtime = PublicationEntry1 + "_" + timestamp + fileextension;
                        UploadPdf1 = actualfilenamewithtime;
                        filelocationpath = Path.Combine(path_BoxId_ProType, actualfilenamewithtime);
                        FileUploadPdf.SaveAs(filelocationpath);  //saving file
                    }
                    j.UploadRemarks = txtuploadRemarks.Text.Trim();
                    j.GID = TextBoxID.Text.Trim();
                    j.GrantType = DropDownListTypeGrant.SelectedValue;
                    j.FilePath = filelocationpath;
                    j.infotypeId = DropDownListInfoType.SelectedValue;
                    j.GrantUnit = DropDownListGrUnit.SelectedValue;
                    j.AuditFrom = AuditFrom.Text;
                    j.AuditTo = AuditTo.Text;
               
                    result1 = B.UploadGrnatPathCreate(j);


                    if ((Session["Role"].ToString() == "11" || Session["Role"].ToString() == "1"))
                    {
                        DSforgridview.SelectParameters.Clear();
                        DSforgridview.SelectParameters.Add("UserId", Session["UserId"].ToString());
                        DSforgridview.SelectParameters.Add("GID", j.GID);
                        DSforgridview.SelectParameters.Add("GrantUnit", j.GrantUnit);
                        DSforgridview.SelectCommand = "select UploadPDFPath ,q.InfoTypeName as TypeName ,Remark ,AuditFrom,AuditTo, p.CreatedDate, p.CreatedBy as CreatedBy, m.FirstName as AddedBy, EntryNo,Unit_Id ,p.InfoTypeId from ProjectAuxillaryDetails p,Project_AuxInfoTypeM q, User_M m where  p.CreatedBy=@UserId and p.InfoTypeId=q.InfoTypeId and m.User_Id=p.CreatedBy  and ID=@GID  and ProjectUnit=@GrantUnit  and Deleted='N' order by EntryNo";
                        DSforgridview.DataBind();
                        GVViewFile.DataBind();

                        DSforgridview1.SelectParameters.Clear();
                        DSforgridview1.SelectParameters.Add("GID", j.GID);
                        DSforgridview1.SelectParameters.Add("GrantUnit", j.GrantUnit);
                        DSforgridview1.SelectCommand = "select UploadPDFPath ,q.InfoTypeName as TypeName ,Remark ,AuditFrom,AuditTo, p.CreatedDate, p.CreatedBy as CreatedBy, m.FirstName as AddedBy, EntryNo,Unit_Id ,p.InfoTypeId from ProjectAuxillaryDetails p,Project_AuxInfoTypeM q, User_M m where  p.InfoTypeId=q.InfoTypeId and m.User_Id=p.CreatedBy  and ID=@GID  and ProjectUnit=@GrantUnit  and Deleted='N' and p.CreatedBy  not in  (Select CreatedBy from ProjectAuxillaryDetails where ProjectUnit=@GrantUnit  and ID=@GID  and Deleted='N')  order by EntryNo";
                        DSforgridview1.DataBind();
                        GridView1.DataBind();
                        Panel8.Visible = true;
                    }

                    if (Session["Role"].ToString() == "6")
                    {
                        DSforgridview.SelectParameters.Clear();
                        DSforgridview.SelectParameters.Add("UserId", Session["UserId"].ToString());
                        DSforgridview.SelectParameters.Add("GID", j.GID);
                        DSforgridview.SelectParameters.Add("GrantUnit", j.GrantUnit);
                        DSforgridview.SelectCommand = "select UploadPDFPath ,q.InfoTypeName as TypeName ,Remark ,AuditFrom,AuditTo, p.CreatedDate, p.CreatedBy as CreatedBy, m.FirstName as AddedBy, EntryNo,Unit_Id ,p.InfoTypeId from ProjectAuxillaryDetails p,Project_AuxInfoTypeM q, User_M m where  p.InfoTypeId=q.InfoTypeId and m.User_Id=p.CreatedBy  and ID=@GID  and ProjectUnit=@GrantUnit  and Deleted='N' and p.CreatedBy=@UserId order by EntryNo";
                        DSforgridview.DataBind();
                        GridView1.DataBind();
                        Panel8.Visible = true;

                        DSforgridview1.SelectParameters.Clear();
                        DSforgridview1.SelectParameters.Add("UserId", Session["UserId"].ToString());
                        DSforgridview1.SelectParameters.Add("GID", j.GID);
                        DSforgridview1.SelectParameters.Add("GrantUnit", j.GrantUnit);
                        DSforgridview1.SelectCommand = "select UploadPDFPath ,q.InfoTypeName as TypeName ,Remark ,AuditFrom,AuditTo, p.CreatedDate, p.CreatedBy as CreatedBy, m.FirstName as AddedBy, EntryNo,Unit_Id ,p.InfoTypeId from ProjectAuxillaryDetails p,Project_AuxInfoTypeM q, User_M m where  p.CreatedBy!=@UserId and p.InfoTypeId=q.InfoTypeId and m.User_Id=p.CreatedBy  and ID=@GID  and ProjectUnit=@GrantUnit  and Deleted='N' order by EntryNo";
                        DSforgridview1.DataBind();
                        GVViewFile.DataBind();
                    }
                    string FileUpload1 = "";
                    Business b1 = new Business();
                    FileUpload1 = b1.GetGrantFileUploadPath(TextBoxID.Text, DropDownListGrUnit.SelectedValue);

                    GVViewFile.Visible = true;
                    
                    PanelViewUplodedfiles.Visible = true;
                    if (result1 >= 1)
                    {
                        ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('File Successfully uploaded!')</script>");
                        log.Info("File Successfully uploaded : " + TextBoxID.Text.Trim() + " , Project Type : " + DropDownListTypeGrant.SelectedValue);
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Error in File upload!!!!')</script>");
                        log.Error("Error in File upload!!!! : " + TextBoxID.Text.Trim() + " , Project Type : " + DropDownListTypeGrant.SelectedValue);

                    }

                }
                else
                {
                    ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Invalid File!!!File exceeds more than 4MB..Please try to upload the file less than or equal to 4MB!!!!!!')</script>");
                    log.Error("Invalid File!!!File exceeds more than 4MB!!! : " + TextBoxID.Text.Trim() + " , Project Type : " + DropDownListTypeGrant.SelectedValue);
                }
            }

        }
        catch (Exception ex)
        {
            log.Error("Inside Catch Block Of Grant-file uplaod" + ex.Message + " UserID : " + Session["UserId"].ToString());

            log.Error(ex.StackTrace);

            ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Error!!!!!!!!!!!!')</script>");

        }
    }


    //Function to view uploaded files
    protected void GVViewFile_SelectedIndexChanged(object sender, EventArgs e)
    {
        string BoxId = TextBoxID.Text.Trim();
        string PublicationEntry1 = DropDownListTypeGrant.SelectedValue;
        string servername = ConfigurationManager.AppSettings["ServerName"].ToString();
        string domainame = ConfigurationManager.AppSettings["DomainName"].ToString();
        string username = ConfigurationManager.AppSettings["UserName"].ToString();
        string password = ConfigurationManager.AppSettings["Password"].ToString();
        string folderpath;
        string path_BoxId;
        using (NetworkShareAccesser.Access(servername, domainame, username, password))
        {
            folderpath = ConfigurationManager.AppSettings["FolderPathProject"].ToString();
            path_BoxId = Path.Combine(folderpath, BoxId);
            string path_BoxId_ProType = Path.Combine(path_BoxId, PublicationEntry1);


            int id = GVViewFile.SelectedIndex;
            Label filepath = (Label)GVViewFile.Rows[id].FindControl("lblgetid");
            string path = filepath.Text;       //actual filelocation path  
            string newpath = path.Replace('\\', '/');  // replacing '\' by '/' to view image or pdf

            Response.Write("<script>");
            Response.Write("window.open('DisplayPdf.aspx?val=" + newpath + "','_blank')");
            //path sent to display.aspx page
            Response.Write("</script>");
        }
    }

    protected void GVView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        string BoxId = TextBoxID.Text.Trim();
        string PublicationEntry1 = DropDownListTypeGrant.SelectedValue;
        string servername = ConfigurationManager.AppSettings["ServerName"].ToString();
        string domainame = ConfigurationManager.AppSettings["DomainName"].ToString();
        string username = ConfigurationManager.AppSettings["UserName"].ToString();
        string password = ConfigurationManager.AppSettings["Password"].ToString();
        string folderpath;
        string path_BoxId;
        using (NetworkShareAccesser.Access(servername, domainame, username, password))
        {
            folderpath = ConfigurationManager.AppSettings["FolderPathProject"].ToString();
            path_BoxId = Path.Combine(folderpath, BoxId);
            string path_BoxId_ProType = Path.Combine(path_BoxId, PublicationEntry1);


            int id = GridView1.SelectedIndex;
            Label filepath = (Label)GridView1.Rows[id].FindControl("lblgetid");
            string path = filepath.Text;       //actual filelocation path  
            string newpath = path.Replace('\\', '/');  // replacing '\' by '/' to view image or pdf

            Response.Write("<script>");
            Response.Write("window.open('DisplayPdf.aspx?val=" + newpath + "','_blank')");
            //path sent to display.aspx page
            Response.Write("</script>");
        }
    }

    //Function Delete uploaded files
    protected void lnkDeleteClick(object sender, EventArgs e)
    {
        string confirmValue = Request.Form["confirm_value"];
        if (confirmValue == "Yes")
        {
            int rownumaux = 0;
            int result = 0;
            LinkButton lnkbtn = sender as LinkButton;
            //getting particular row linkbutton
            GridViewRow gvrow = lnkbtn.NamingContainer as GridViewRow;
            //getting aptid of particular row
            GrantData j = new GrantData();

            HiddenField lblEntrynum = (HiddenField)GVViewFile.Rows[gvrow.RowIndex].Cells[6].FindControl("lblEntrynum");
            rownumaux = Convert.ToInt32(lblEntrynum.Value);

            string protype = DropDownListGrUnit.SelectedValue;
            string id = TextBoxID.Text.Trim();

            j.GID = id;
            j.GrantType = protype;
            j.Entrynum = rownumaux;

            int count = 0;
            Label InfoType = (Label)GVViewFile.Rows[gvrow.RowIndex].Cells[5].FindControl("InfoType");
            string type = InfoType.Text;
            if (type == "SAN")
            {
                for (int i = 0; i < GVViewFile.Rows.Count; i++)
                {
                    Label InfoType1 = (Label)GVViewFile.Rows[i].Cells[5].FindControl("InfoType");
                    string type1 = InfoType1.Text;
                    if (type1 == "SAN")
                    {
                        count++;
                    }

                }

                if (count == 1)
                {
                    ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Atleast one sanction documnet must be uploaded');</script>");
                    return;
                }
                else
                {
                    result = B.UpdateGrantattachedEntry(j);
                    if (result == 1)
                    {

                        ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Deleted Successfully');</script>");

                        //DSforgridview.SelectCommand = "select UploadPDFPath ,q.InfoTypeName as TypeName ,Remark,CreatedDate,EntryNo,p.CreatedBy,FirstName as AddedBy,Unit_Id ,p.InfoTypeId from ProjectAuxillaryDetails p,Project_AuxInfoTypeM q,User_M u where  p.InfoTypeId=q.InfoTypeId and ID='" + j.GID + "' and ProjectUnit='" + j.GrantUnit + "' and Deleted='N' and u.User_Id=p.CreatedBy order by EntryNo";
                        //DSforgridview.DataBind();
                        //GVViewFile.DataBind();

                        if ((Session["Role"].ToString() == "11" || Session["Role"].ToString() == "1"))
                        {
                            DSforgridview.SelectParameters.Clear();
                            DSforgridview.SelectParameters.Add("UserId", Session["UserId"].ToString());
                            DSforgridview.SelectParameters.Add("GID", j.GID);
                            DSforgridview.SelectParameters.Add("GrantType", j.GrantType); //here
                            DSforgridview.SelectCommand = "select UploadPDFPath ,q.InfoTypeName as TypeName ,Remark ,AuditFrom,AuditTo, p.CreatedDate, p.CreatedBy as CreatedBy, m.FirstName as AddedBy, EntryNo,Unit_Id ,p.InfoTypeId from ProjectAuxillaryDetails p,Project_AuxInfoTypeM q, User_M m where  p.CreatedBy=@UserId and p.InfoTypeId=q.InfoTypeId and m.User_Id=p.CreatedBy  and ID=@GID  and ProjectUnit=@GrantType  and Deleted='N' order by EntryNo";
                            DSforgridview.DataBind();
                            GVViewFile.DataBind();



                            DSforgridview1.SelectParameters.Clear();

                            DSforgridview1.SelectParameters.Add("GID", j.GID);
                            DSforgridview1.SelectParameters.Add("GrantUnit", j.GrantUnit);
                            DSforgridview1.SelectParameters.Add("GrantType", j.GrantType);
                            DSforgridview1.SelectCommand = "select UploadPDFPath ,q.InfoTypeName as TypeName ,Remark ,AuditFrom,AuditTo, p.CreatedDate, p.CreatedBy as CreatedBy, m.FirstName as AddedBy, EntryNo,Unit_Id ,p.InfoTypeId from ProjectAuxillaryDetails p,Project_AuxInfoTypeM q, User_M m where  p.InfoTypeId=q.InfoTypeId and m.User_Id=p.CreatedBy  and ID=@GID  and ProjectUnit=@GrantType  and Deleted='N' and p.CreatedBy  <>  (Select CreatedBy from ProjectAuxillaryDetails where ProjectUnit=@GrantUnit  and ID=@GID  and Deleted='N') order by EntryNo";
                            DSforgridview1.DataBind();
                            GridView1.DataBind();
                            Panel8.Visible = true;
                        }

                        if (Session["Role"].ToString() == "6")
                        {
                            DSforgridview.SelectParameters.Clear();

                            DSforgridview.SelectParameters.Add("GID", j.GID);
                            DSforgridview.SelectParameters.Add("GrantUnit", j.GrantUnit);
                            DSforgridview.SelectParameters.Add("GrantType", j.GrantType); //here
                            DSforgridview.SelectCommand = "select UploadPDFPath ,q.InfoTypeName as TypeName ,Remark ,AuditFrom,AuditTo, p.CreatedDate, p.CreatedBy as CreatedBy, m.FirstName as AddedBy, EntryNo,Unit_Id ,p.InfoTypeId from ProjectAuxillaryDetails p,Project_AuxInfoTypeM q, User_M m where  p.InfoTypeId=q.InfoTypeId and m.User_Id=p.CreatedBy  and ID=@GID  and ProjectUnit=@GrantType  and Deleted='N' and p.CreatedBy  <>  (Select CreatedBy from ProjectAuxillaryDetails where ProjectUnit=@GrantUnit  and ID=@GID  and Deleted='N')  order by EntryNo";
                            DSforgridview.DataBind();
                            GridView1.DataBind();
                            Panel8.Visible = true;


                            DSforgridview1.SelectParameters.Clear();
                            DSforgridview1.SelectParameters.Add("UserId", Session["UserId"].ToString());
                            DSforgridview1.SelectParameters.Add("GID", j.GID);
                            DSforgridview1.SelectParameters.Add("GrantType", j.GrantType);
                            DSforgridview1.SelectCommand = "select UploadPDFPath ,q.InfoTypeName as TypeName ,Remark ,AuditFrom,AuditTo, p.CreatedDate, p.CreatedBy as CreatedBy, m.FirstName as AddedBy, EntryNo,Unit_Id ,p.InfoTypeId from ProjectAuxillaryDetails p,Project_AuxInfoTypeM q, User_M m where  p.CreatedBy!=@UserId and p.InfoTypeId=q.InfoTypeId and m.User_Id=p.CreatedBy  and ID=@GID  and ProjectUnit=@GrantType  and Deleted='N' order by EntryNo";
                            DSforgridview1.DataBind();
                            GVViewFile.DataBind();
                        }
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Error!!!!!!!');</script>");
                        //DSforgridview.SelectCommand = "select UploadPDFPath ,q.InfoTypeName as TypeName ,Remark,CreatedDate,EntryNo,p.CreatedBy,FirstName as AddedBy,Unit_Id,p.InfoTypeId from ProjectAuxillaryDetails p,Project_AuxInfoTypeM q,User_M u where  p.InfoTypeId=q.InfoTypeId and ID='" + j.GID + "' and ProjectUnit='" + j.GrantUnit + "' and Deleted='N' and u.User_Id=p.CreatedBy  order by EntryNo";
                        //DSforgridview.DataBind();
                        //GVViewFile.DataBind();

                        if ((Session["Role"].ToString() == "11" || Session["Role"].ToString() == "1"))
                        {
                            DSforgridview.SelectParameters.Clear();
                            DSforgridview.SelectParameters.Add("UserId", Session["UserId"].ToString());
                            DSforgridview.SelectParameters.Add("GID", j.GID);
                            DSforgridview.SelectParameters.Add("GrantType", j.GrantType);
                            DSforgridview.SelectCommand = "select UploadPDFPath ,q.InfoTypeName as TypeName ,Remark ,AuditFrom,AuditTo, p.CreatedDate, p.CreatedBy as CreatedBy, m.FirstName as AddedBy, EntryNo,Unit_Id ,p.InfoTypeId from ProjectAuxillaryDetails p,Project_AuxInfoTypeM q, User_M m where  p.CreatedBy=@UserId and p.InfoTypeId=q.InfoTypeId and m.User_Id=p.CreatedBy  and ID=@GID  and ProjectUnit=@GrantType  and Deleted='N' order by EntryNo";
                            DSforgridview.DataBind();
                            GVViewFile.DataBind();

                            DSforgridview1.SelectParameters.Clear();

                            DSforgridview1.SelectParameters.Add("GID", j.GID);
                            DSforgridview1.SelectParameters.Add("GrantUnit", j.GrantUnit);
                            
                            DSforgridview1.SelectCommand = "select UploadPDFPath ,q.InfoTypeName as TypeName ,Remark ,AuditFrom,AuditTo, p.CreatedDate, p.CreatedBy as CreatedBy, m.FirstName as AddedBy, EntryNo,Unit_Id ,p.InfoTypeId from ProjectAuxillaryDetails p,Project_AuxInfoTypeM q, User_M m where  p.InfoTypeId=q.InfoTypeId and m.User_Id=p.CreatedBy  and ID=@GID  and ProjectUnit=@GrantUnit  and Deleted='N' and p.CreatedBy  <>  (Select CreatedBy from ProjectAuxillaryDetails where ProjectUnit=@GrantUnit  and ID=@GID  and Deleted='N')  order by EntryNo";
                            DSforgridview1.DataBind();
                            GridView1.DataBind();
                            Panel8.Visible = true;
                        }

                        if (Session["Role"].ToString() == "6")
                        {
                            DSforgridview.SelectParameters.Clear();

                            DSforgridview.SelectParameters.Add("GID", j.GID);
                            DSforgridview.SelectParameters.Add("GrantUnit", j.GrantUnit);
                            DSforgridview.SelectParameters.Add("GrantType", j.GrantType);
                            DSforgridview.SelectCommand = "select UploadPDFPath ,q.InfoTypeName as TypeName ,Remark ,AuditFrom,AuditTo, p.CreatedDate, p.CreatedBy as CreatedBy, m.FirstName as AddedBy, EntryNo,Unit_Id ,p.InfoTypeId from ProjectAuxillaryDetails p,Project_AuxInfoTypeM q, User_M m where  p.InfoTypeId=q.InfoTypeId and m.User_Id=p.CreatedBy  and ID=@GID  and ProjectUnit=@GrantType  and Deleted='N' and p.CreatedBy  <>  (Select CreatedBy from ProjectAuxillaryDetails where ProjectUnit=@GrantUnit  and ID=@GID  and Deleted='N') order by EntryNo";
                            DSforgridview.DataBind();
                            GridView1.DataBind();
                            Panel8.Visible = true;


                            DSforgridview1.SelectParameters.Clear();
                            DSforgridview1.SelectParameters.Add("UserId", Session["UserId"].ToString());
                            DSforgridview1.SelectParameters.Add("GID", j.GID);
                            DSforgridview1.SelectParameters.Add("GrantType", j.GrantType);
                            DSforgridview1.SelectCommand = "select UploadPDFPath ,q.InfoTypeName as TypeName ,Remark ,AuditFrom,AuditTo, p.CreatedDate, p.CreatedBy as CreatedBy, m.FirstName as AddedBy, EntryNo,Unit_Id ,p.InfoTypeId from ProjectAuxillaryDetails p,Project_AuxInfoTypeM q, User_M m where  p.CreatedBy!=@UserId and p.InfoTypeId=q.InfoTypeId and m.User_Id=p.CreatedBy  and ID=@GID  and ProjectUnit=@GrantType  and Deleted='N' order by EntryNo";
                            DSforgridview1.DataBind();
                            GVViewFile.DataBind();
                        }
                    }

                }
            }
            else
            {
                result = B.UpdateGrantattachedEntry(j);
                if (result == 1)
                {

                    ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Deleted Successfully');</script>");

                    //DSforgridview.SelectCommand = "select UploadPDFPath ,q.InfoTypeName as TypeName ,Remark,p.CreatedDate,EntryNo,p.CreatedBy,FirstName as AddedBy,Unit_Id,p.InfoTypeId  from ProjectAuxillaryDetails p,Project_AuxInfoTypeM q,User_M u where  p.InfoTypeId=q.InfoTypeId and ID='" + j.GID + "' and ProjectUnit='" + j.GrantUnit + "' and Deleted='N' and u.User_Id=p.CreatedBy order by EntryNo";
                    //DSforgridview.DataBind();
                    //GVViewFile.DataBind();
                    if ((Session["Role"].ToString() == "11" || Session["Role"].ToString() == "1"))
                    {
                        DSforgridview.SelectParameters.Clear();
                        DSforgridview.SelectParameters.Add("UserId", Session["UserId"].ToString());
                        DSforgridview.SelectParameters.Add("GID", j.GID);
                        DSforgridview.SelectParameters.Add("GrantType", j.GrantType);
                        DSforgridview.SelectCommand = "select UploadPDFPath ,q.InfoTypeName as TypeName ,Remark ,AuditFrom,AuditTo, p.CreatedDate, p.CreatedBy as CreatedBy, m.FirstName as AddedBy, EntryNo,Unit_Id ,p.InfoTypeId from ProjectAuxillaryDetails p,Project_AuxInfoTypeM q, User_M m where  p.CreatedBy=@UserId and p.InfoTypeId=q.InfoTypeId and m.User_Id=p.CreatedBy  and ID=@GID  and ProjectUnit=@GrantType  and Deleted='N' order by EntryNo";
                        DSforgridview.DataBind();
                        GVViewFile.DataBind();


                        DSforgridview1.SelectParameters.Clear();

                        DSforgridview1.SelectParameters.Add("GID", j.GID);
                        DSforgridview1.SelectParameters.Add("GrantUnit", j.GrantUnit);
                        DSforgridview1.SelectParameters.Add("GrantType", j.GrantType);
                        DSforgridview1.SelectCommand = "select UploadPDFPath ,q.InfoTypeName as TypeName ,Remark ,AuditFrom,AuditTo, p.CreatedDate, p.CreatedBy as CreatedBy, m.FirstName as AddedBy, EntryNo,Unit_Id ,p.InfoTypeId from ProjectAuxillaryDetails p,Project_AuxInfoTypeM q, User_M m where  p.InfoTypeId=q.InfoTypeId and m.User_Id=p.CreatedBy  and ID=@GID  and ProjectUnit=@GrantType  and Deleted='N' and p.CreatedBy  not in  (Select CreatedBy from ProjectAuxillaryDetails where ProjectUnit=@GrantUnit  and ID=@GID  and Deleted='N')  order by EntryNo";
                        DSforgridview1.DataBind();
                        GridView1.DataBind();
                        Panel8.Visible = true;
                    }

                    if (Session["Role"].ToString() == "6")
                    {
                        DSforgridview.SelectParameters.Clear();

                        DSforgridview.SelectParameters.Add("GID", j.GID);
                        DSforgridview.SelectParameters.Add("GrantUnit", j.GrantUnit);
                        DSforgridview.SelectParameters.Add("GrantType", j.GrantType);
                        DSforgridview.SelectCommand = "select UploadPDFPath ,q.InfoTypeName as TypeName ,Remark ,AuditFrom,AuditTo, p.CreatedDate, p.CreatedBy as CreatedBy, m.FirstName as AddedBy, EntryNo,Unit_Id ,p.InfoTypeId from ProjectAuxillaryDetails p,Project_AuxInfoTypeM q, User_M m where  p.InfoTypeId=q.InfoTypeId and m.User_Id=p.CreatedBy  and ID=@GID  and ProjectUnit=@GrantType  and Deleted='N' and p.CreatedBy  not in  (Select CreatedBy from ProjectAuxillaryDetails where ProjectUnit=@GrantUnit  and ID=@GID  and Deleted='N')  order by EntryNo";
                        DSforgridview.DataBind();
                        GridView1.DataBind();
                        Panel8.Visible = true;


                        DSforgridview1.SelectParameters.Clear();
                        DSforgridview1.SelectParameters.Add("UserId", Session["UserId"].ToString());
                        DSforgridview1.SelectParameters.Add("GID", j.GID);
                        DSforgridview1.SelectParameters.Add("GrantType", j.GrantType);

                        DSforgridview1.SelectCommand = "select UploadPDFPath ,q.InfoTypeName as TypeName ,Remark ,AuditFrom,AuditTo, p.CreatedDate, p.CreatedBy as CreatedBy, m.FirstName as AddedBy, EntryNo,Unit_Id ,p.InfoTypeId from ProjectAuxillaryDetails p,Project_AuxInfoTypeM q, User_M m where  p.CreatedBy!=@UserId and p.InfoTypeId=q.InfoTypeId and m.User_Id=p.CreatedBy  and ID=@GID  and ProjectUnit=@GrantType  and Deleted='N' order by EntryNo";
                        DSforgridview1.DataBind();
                        GVViewFile.DataBind();
                    }
                }
                else
                {
                    ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Error!!!!!!!');</script>");
                    //DSforgridview.SelectCommand = "select UploadPDFPath ,q.InfoTypeName as TypeName ,Remark,p.CreatedDate,EntryNo,p.CreatedBy,FirstName as AddedBy,Unit_Id,p.InfoTypeId from ProjectAuxillaryDetails p,Project_AuxInfoTypeM q,User_M u where  p.InfoTypeId=q.InfoTypeId and ID='" + j.GID + "' and ProjectUnit='" + j.GrantUnit + "' and Deleted='N' and u.User_Id=p.CreatedBy  order by EntryNo";
                    //DSforgridview.DataBind();
                    //GVViewFile.DataBind();
                    if ((Session["Role"].ToString() == "11" || Session["Role"].ToString() == "1"))
                    {
                        DSforgridview.SelectParameters.Clear();

                        DSforgridview.SelectParameters.Add("GID", j.GID);
                        DSforgridview.SelectParameters.Add("UserId", Session["UserId"].ToString());
                        DSforgridview.SelectParameters.Add("GrantType", j.GrantType);
                        DSforgridview.SelectCommand = "select UploadPDFPath ,q.InfoTypeName as TypeName ,Remark ,AuditFrom,AuditTo, p.CreatedDate, p.CreatedBy as CreatedBy, m.FirstName as AddedBy, EntryNo,Unit_Id ,p.InfoTypeId from ProjectAuxillaryDetails p,Project_AuxInfoTypeM q, User_M m where  p.CreatedBy=@UserId and p.InfoTypeId=q.InfoTypeId and m.User_Id=p.CreatedBy  and ID=@GID  and ProjectUnit=@GrantType  and Deleted='N' order by EntryNo";
                        DSforgridview.DataBind();
                        GVViewFile.DataBind();

                        DSforgridview1.SelectParameters.Clear();

                        DSforgridview1.SelectParameters.Add("GID", j.GID);
                        DSforgridview1.SelectParameters.Add("GrantUnit", j.GrantUnit);
                        DSforgridview1.SelectParameters.Add("GrantType", j.GrantType);
                        DSforgridview1.SelectCommand = "select UploadPDFPath ,q.InfoTypeName as TypeName ,Remark ,AuditFrom,AuditTo, p.CreatedDate, p.CreatedBy as CreatedBy, m.FirstName as AddedBy, EntryNo,Unit_Id ,p.InfoTypeId from ProjectAuxillaryDetails p,Project_AuxInfoTypeM q, User_M m where  p.InfoTypeId=q.InfoTypeId and m.User_Id=p.CreatedBy  and ID=@GID  and ProjectUnit=@GrantType  and Deleted='N' and p.CreatedBy  not in  (Select CreatedBy from ProjectAuxillaryDetails where ProjectUnit=@GrantUnit  and ID=@GID  and Deleted='N')  order by EntryNo";
                        DSforgridview1.DataBind();
                        GridView1.DataBind();
                        Panel8.Visible = true;
                    }

                    if (Session["Role"].ToString() == "6")
                    {
                        DSforgridview.SelectParameters.Clear();

                        DSforgridview.SelectParameters.Add("GID", j.GID);
                        DSforgridview.SelectParameters.Add("GrantUnit", j.GrantUnit);
                        DSforgridview.SelectParameters.Add("GrantType", j.GrantType);
                        DSforgridview.SelectCommand = "select UploadPDFPath ,q.InfoTypeName as TypeName ,Remark ,AuditFrom,AuditTo, p.CreatedDate, p.CreatedBy as CreatedBy, m.FirstName as AddedBy, EntryNo,Unit_Id ,p.InfoTypeId from ProjectAuxillaryDetails p,Project_AuxInfoTypeM q, User_M m where  p.InfoTypeId=q.InfoTypeId and m.User_Id=p.CreatedBy  and ID=@GID  and ProjectUnit=@GrantType  and Deleted='N' and p.CreatedBy  not in  (Select CreatedBy from ProjectAuxillaryDetails where ProjectUnit=@GrantUnit   and ID=@GID  and Deleted='N')  order by EntryNo";
                        DSforgridview.DataBind();
                        GridView1.DataBind();
                        Panel8.Visible = true;

                        DSforgridview1.SelectParameters.Clear();
                        DSforgridview1.SelectParameters.Add("UserId", Session["UserId"].ToString());
                        DSforgridview1.SelectParameters.Add("GID", j.GID);
                        DSforgridview1.SelectParameters.Add("GrantType", j.GrantType);
                        DSforgridview1.SelectCommand = "select UploadPDFPath ,q.InfoTypeName as TypeName ,Remark ,AuditFrom,AuditTo, p.CreatedDate, p.CreatedBy as CreatedBy, m.FirstName as AddedBy, EntryNo,Unit_Id ,p.InfoTypeId from ProjectAuxillaryDetails p,Project_AuxInfoTypeM q, User_M m where  p.CreatedBy!=@UserId and p.InfoTypeId=q.InfoTypeId and m.User_Id=p.CreatedBy  and ID=@GID  and ProjectUnit=@GrantType  and Deleted='N' order by EntryNo";
                        DSforgridview1.DataBind();
                        GVViewFile.DataBind();
                    }
                }

            }


        }

        confirmValue = "";
    }

    protected void GVViewFile_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        ImageButton EditButton = (ImageButton)e.Row.FindControl("BtnEdit");
    }


    //Functionality- PopUp Author

    protected void setModalWindow(object sender, EventArgs e)
    {
        popupPanelAffil.Visible = false;
        popGridAffil.DataSourceID = "SqlDataSourceAffil";
        SqlDataSourceAffil.DataBind();
        popGridAffil.DataBind();
        popGridAffil.Visible = false;
    }
    protected void exit(object sender, EventArgs e)
    {
        affiliateSrch.Text = "";
        popGridAffil.DataBind();
    }


    // bind author popup grid on text change
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
            SqlDataSourceAffil.SelectCommand = "SELECT  User_Id,prefix+' '+UPPER(firstname)+' '+UPPER(middlename)+' '+UPPER(lastname)  as Name from User_M where prefix+firstname+middlename+lastname like '%' + @name +'%'";

            popGridAffil.DataBind();
            popGridAffil.Visible = true;
        }

        string rowVal = Request.Form["rowIndx"];
        int rowIndex = Convert.ToInt32(rowVal);

        ModalPopupExtender ModalPopupExtender8 = (ModalPopupExtender)Grid_AuthorEntry.Rows[rowIndex].FindControl("ModalPopupExtender4");
        ModalPopupExtender8.Show();

    }

    //on row select of pop up autor
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

    //Functionality- PopUp Agency
    protected void setModalWindowAgency(object sender, EventArgs e)
    {
        popupselectNo.Visible = true;
          string a=DropDownListGrUnit.SelectedValue;
          if (a == "")
          {
              SqlDataSourceTextBoxGrantAgency.SelectCommand = "SELECT  FundingAgencyId as Id,UPPER([FundingAgencyName]) as Name FROM [ProjectFundingAgency_M] where AgentType='MUFOR'";
              SqlDataSourceTextBoxGrantAgency.DataBind();
              popGridagency.DataSourceID = "SqlDataSourceTextBoxGrantAgency";
              SqlDataSourceTextBoxGrantAgency.DataBind();
              popGridagency.DataBind();
          }
          else
          {
              SqlDataSourceTextBoxGrantAgency.SelectParameters.Clear();
              SqlDataSourceTextBoxGrantAgency.SelectParameters.Add("AgentType", a);
              SqlDataSourceTextBoxGrantAgency.SelectCommand = "SELECT  FundingAgencyId as Id,UPPER([FundingAgencyName]) as Name FROM [ProjectFundingAgency_M] where  AgentType=@AgentType";
              SqlDataSourceTextBoxGrantAgency.DataBind();
              popGridagency.DataSourceID = "SqlDataSourceTextBoxGrantAgency";
              SqlDataSourceTextBoxGrantAgency.DataBind();
              popGridagency.DataBind();
              popGridagency.Visible = true;
          }
        
    }
    protected void popGridagency_pageindex(object sender, GridViewPageEventArgs e)
    {
        popupselectNo.Visible = true;
        model.Show();
      string a=DropDownListGrUnit.SelectedValue;
      SqlDataSourceTextBoxGrantAgency.SelectParameters.Clear();
      SqlDataSourceTextBoxGrantAgency.SelectParameters.Add("AgentType", a);
      SqlDataSourceTextBoxGrantAgency.SelectCommand = "SELECT  FundingAgencyId as Id,UPPER([FundingAgencyName]) as Name FROM [ProjectFundingAgency_M] where AgentType=@AgentType";
        SqlDataSourceTextBoxGrantAgency.DataBind();

        popGridagency.DataSourceID = "SqlDataSourceTextBoxGrantAgency";
        popGridagency.DataBind();
        popGridagency.Visible = true;
        popGridagency.AllowPaging = true;
        popGridagency.PageIndex = e.NewPageIndex;
    }

    //on row select of pop up agency
    protected void popSelectedagency(Object sender, EventArgs e)
    {
        popGridagency.Visible = true;
        GridViewRow row = popGridagency.SelectedRow;
        string granttagency = row.Cells[2].Text;
        txtagency.Text = granttagency;
        hdnAgencyId.Value = row.Cells[1].Text;
        popGridagency.DataBind();
        AgencyTextChanged(sender, e);
    }

    //onselect agency
    protected void AgencyTextChanged(object sender, EventArgs e)
    {
        GrantData j = new GrantData();
        Business obj = new Business();
        j.FundingAgencyId = null;
        j = obj.selectExisitingAgency(hdnAgencyId.Value.Trim());
        if (j.FundingAgencyId != null)
        {
            txtagency.Text = j.FundingAgencyName;
            txtagencycontact.Text = j.AgencyContact;
            txtpan.Text = j.Pan_No;
            txtEmailId.Text = j.EmailId;
            txtAddress.Text = j.Address;
            txtstate.Text = j.State;
            txtcountry.Text = j.Country;
            //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert(' Agency exixts!')</script>");

        }
        setModalWindowAgency(sender, e);
    }


    protected void BtnSave_Click(object sender, EventArgs e)
    {

        if (!Page.IsValid)
        {
            return;
        }
        string confirmValue2 = Request.Form["confirm_value2"];
        if (confirmValue2 == "Yes")
          {
              try
              {
                  GrantData V = new GrantData();
                  Business b = new Business();

                  ArrayList listIndexAgency = new ArrayList();
                  Business B = new Business();
                  GrantData j = new GrantData();

                  DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                  GrantData[] JD = new GrantData[dtCurrentTable.Rows.Count];
                  string GId = TextBoxID.Text.Trim();

                  string UTN = TextBoxUTN.Text.Trim();


                  string GUnit = DropDownListGrUnit.SelectedValue;

                  string projectid = TextBoxID.Text.Trim();
                  string projectunit = DropDownListGrUnit.SelectedValue;
                  string projecttype = DropDownListTypeGrant.SelectedValue;
                  string Remarks = TextBox5.Text.Trim();

                  GrantData obj1 = new GrantData();


                  string resultutn = B.selectoldutn(projectid, projectunit);
                  Session[" resultutn"] = resultutn;



                  GrantData obj = new GrantData();
                  if (TextBoxUTN.Text != "")
                  {
                      obj = B.CheckUniqueUTN(UTN, projectid, projectunit);
                      if (obj.UTN == TextBoxUTN.Text.Trim())
                      {
                          if (obj.GID != projectunit + projectid)
                          {
                              ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Entered UTN already exists in the system.Please correct the UTN')</script>");
                              return;
                          }
                      }
                  }
                  if (Textsanctionorderdate.Text != "")
                  {
                      string SanctionOrderDATE = Textsanctionorderdate.Text.Trim();
                  }
                  if (Textsanctionorderdate.Text != "")
                  {
                      j.SanctionOrderDate = Convert.ToDateTime(Textsanctionorderdate.Text.Trim());
                  }
                  j.GID = GId;
                  j.FromUTN = Session[" resultutn"].ToString(); ;
                  j.UTN = UTN;
                  j.GrantUnit = GUnit;
                  j.CreatedBy = Session["UserId"].ToString();
                  j.CreatedDate = DateTime.Now;
                  j.Remarks = Remarks;

                 // int result1 = B.insertbackupUTN(j, JD);

                  int result = B.UpdateUTN(j, JD);
                  if (TextBoxUTN.Text!="")
                  {
                      ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('UTN value Updated Successfully  of ID: " + TextBoxID.Text + "')</script>");
   
                      ButtonSearchProjectOnClick(sender, e);
                      btnSave.Enabled = false;
                      EmailDetails details = new EmailDetails();
                      details = SendMail();
                      details.Id = TextBoxID.Text;
                      details.Type = DropDownListTypeGrant.SelectedItem.ToString();
                      details.ProjectUnit = DropDownListGrUnit.SelectedItem.ToString();
                      details.UnitId = DropDownListGrUnit.SelectedValue.ToString();
                      SendMailObject obj2 = new SendMailObject();
                      if(details.ToEmail!="" && details.ToEmail!=null)
                      {
                      bool result1 = obj2.InsertIntoEmailQueue(details);
                      }
                      ButtonSearchProjectOnClick(sender, e);
                    

                  }
              }
              catch (Exception ex)
              {
                  throw ex;
              }

          }
    
    }    

    protected void addclik(object sender, EventArgs e)
    {
        //panelcancelProject.Visible = false;
        PanelViewUplodedfiles.Visible = false;
        Panel8.Visible = false;
        LabelkindDetails.Visible = false;
        BtnAddMU.Enabled =false;
        Grid_AuthorEntry.Enabled = true;
        TextBoxKindDetails.Visible = false;
        DropDownListSanType.ClearSelection();
        DropDownListerfRelated.ClearSelection();
        TextBoxTitle.Text = "";
        //panelCanelRemarks.Visible = false;
        TextBoxUTN.Text = "";
        TextBoxID.Text = "";
        panAddAuthor.Visible = true;
        //Panel2.Visible = true;
        PanelUploaddetails.Visible = false;
        Grid_AuthorEntry.DataSource = null;
        Grid_AuthorEntry.DataBind();
        SetInitialRow();
        SetInitialRowBank();
        Grid_AuthorEntry.Visible = true;
        //panelCanelRemarks.Visible = false;
        // panelJournalArticle.Visible = false;
        //  TextBoxOnlineUid.Text = "";
        //  TextBoxOnlinePassword.Text = "";
        TextBoxDescription.Text = "";
        // TextBoxCurStatus.Text = "Submitted";
        txtAddress.Text = "";
        txtpan.Text = "";
        txtstate.Text = "";
        txtcountry.Text = "";
        txtagencycontact.Text = "";
        txtcontact.Text = "";
        txtEmailId.Text = "";
        txtagency.Text = "";
        hdnAgencyId.Value = "";
        TextBoxGrantDate.Text = "";
        txtRevisedAppliedAmt.Text = "";

        //TextBoxNameInstituion.Text = "";
        txtagency.Enabled = true;
        DropDownListTypeGrant.Enabled = true;
        TextBoxTitle.Enabled = true;
        DropDownListSourceGrant.Enabled = true;
        DropDownListGrUnit.Enabled = true;
        // TextBoxDepartment.Text = "";

        DropDownListSanType.Enabled = true;
        DropDownListGrUnit.ClearSelection();

        TextBoxGrantAmt.Text = "";

        DropDownListSourceGrant.ClearSelection();


        DropDownListTypeGrant.ClearSelection();

        btnSave.Enabled = true;
        DropDownListProjStatus.Items.Clear();
        SqlDataSourcePrjStatus.SelectCommand = "select StatusId,StatusName from Status_Project_M where StatusId='APP'";
        DropDownListProjStatus.DataBind();
        GrantSanction.Visible = true;
        //TextBoxSanctionnNumber.Text = "";
        TextBoxSanctionedAmountCapital.Text = "";

        TextBoxSanctionedAmountOperating.Text = "";
        TextBoxSanctionedamountTotal.Text = "";

       // TextBoxSanctionDate.Text = "";
        TextBoxProjectCommencementDate.Text = "";

        TextBoxProjectCloseDate.Text = "";
        TextBoxExtendedDate.Text = "";
        //PanelKindetails.Visible = false;
        //GridViewkindDetails.DataSource = null;
        DropDownListSanType.Visible = false;
        LabelSanType.Visible = false;
        TextBoxAdComments.Text = "";
        //TextBoxRemarks.Text = "";
        txtuploadRemarks.Text = "";
    }
    protected void btnAddProject_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/GrantEntry/GrantEntry.aspx");
    }

   


    //Gridview Kind 
    protected void GridViewkindDetails_RowDeleting(Object sender, GridViewDeleteEventArgs e)
    {

        SetRowSancKindData();
        if (ViewState["SancKindCurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["SancKindCurrentTable"];
            DataRow drCurrentRow = null;
            int rowIndex = Convert.ToInt32(e.RowIndex);
            if (dt.Rows.Count > 1 && rowIndex != 0)
            {
                dt.Rows.Remove(dt.Rows[rowIndex]);
                drCurrentRow = dt.NewRow();
                ViewState["SancKindCurrentTable"] = dt;
                GridViewkindDetails.DataSource = dt;
                GridViewkindDetails.DataBind();

                SetPreviousSancKindData();
                // gridAmtChanged(sender, e);
            }
        }
    }

    private void SetPreviousSancKindData()
    {
       int rowIndex = 0;
        if (ViewState["SancKindCurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["SancKindCurrentTable"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    TextBox ReceivedDate = (TextBox)GridViewkindDetails.Rows[rowIndex].Cells[1].FindControl("ReceivedDate");
                    TextBox INREquivalent = (TextBox)GridViewkindDetails.Rows[rowIndex].Cells[0].FindControl("INREquivalent");

                    TextBox Details = (TextBox)GridViewkindDetails.Rows[rowIndex].Cells[1].FindControl("Details");



                    ReceivedDate.Text = dt.Rows[i]["ReceivedDate"].ToString();
                    INREquivalent.Text = dt.Rows[i]["INREquivalent"].ToString();
                    Details.Text = dt.Rows[i]["Details"].ToString();

                    rowIndex++;
                }
            }
        }
    }


    private void SetRowSancKindData()
    {
        int rowIndex = 0;

        if (ViewState["SancKindCurrentTable"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["SancKindCurrentTable"];
            DataRow drCurrentRow = null;
            if (dtCurrentTable.Rows.Count > 0)
            {
                for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                {
                    TextBox ReceivedDate = (TextBox)GridViewkindDetails.Rows[rowIndex].Cells[1].FindControl("ReceivedDate");
                    TextBox INREquivalent = (TextBox)GridViewkindDetails.Rows[rowIndex].Cells[0].FindControl("INREquivalent");
                    TextBox Details = (TextBox)GridViewkindDetails.Rows[rowIndex].Cells[0].FindControl("Details");

                    drCurrentRow = dtCurrentTable.NewRow();

                    dtCurrentTable.Rows[i - 1]["ReceivedDate"] = ReceivedDate.Text;
                    dtCurrentTable.Rows[i - 1]["INREquivalent"] = INREquivalent.Text;
                    dtCurrentTable.Rows[i - 1]["Details"] = Details.Text;

                    rowIndex++;
                }

                ViewState["SancKindCurrentTable"] = dtCurrentTable;
            }
        }
        else
        {
            Response.Write("ViewState is null");
        }

    }

    protected void addRowSancKind(object sender, EventArgs e)
    {

        if (Grid_AuthorEntry.Rows.Count == 0)
        {
            SetInitialSancKindRow();
        }

        else
        {
            int rowIndex = 0;

            if (ViewState["SancKindCurrentTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["SancKindCurrentTable"];
                DataRow drCurrentRow = null;
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {
                        TextBox ReceivedDate = (TextBox)GridViewkindDetails.Rows[rowIndex].Cells[1].FindControl("ReceivedDate");
                        TextBox INREquivalent = (TextBox)GridViewkindDetails.Rows[rowIndex].Cells[0].FindControl("INREquivalent");
                        TextBox Details = (TextBox)GridViewkindDetails.Rows[rowIndex].Cells[0].FindControl("Details");

                        drCurrentRow = dtCurrentTable.NewRow();

                        dtCurrentTable.Rows[i - 1]["ReceivedDate"] = ReceivedDate.Text;
                        dtCurrentTable.Rows[i - 1]["INREquivalent"] = INREquivalent.Text;
                        dtCurrentTable.Rows[i - 1]["Details"] = Details.Text;

                        rowIndex++;
                    }

                    dtCurrentTable.Rows.Add(drCurrentRow);

                    ViewState["SancKindCurrentTable"] = dtCurrentTable;

                    GridViewkindDetails.DataSource = dtCurrentTable;
                    GridViewkindDetails.DataBind();

                }
            }
            else
            {
                Response.Write("ViewState is null");
            }

            SetPreviousSancKindData();
        }

    }

    private void SetInitialSancKindRow()
    {
        DataTable dt = new DataTable();
        DataRow dr = null;
        dt.Columns.Add(new DataColumn("ReceivedDate", typeof(string)));
        dt.Columns.Add(new DataColumn("INREquivalent", typeof(string)));
        dt.Columns.Add(new DataColumn("Details", typeof(string)));

        dr = dt.NewRow();

        dr["ReceivedDate"] = string.Empty;
        dr["INREquivalent"] = string.Empty;
        dr["Details"] = string.Empty;

        dt.Rows.Add(dr);

        ViewState["SancKindCurrentTable"] = dt;
        GridViewkindDetails.DataSource = dt;
        GridViewkindDetails.DataBind();

        TextBox ReceivedDate = (TextBox)Grid_AuthorEntry.Rows[0].Cells[1].FindControl("ReceivedDate");
        TextBox INREquivalent = (TextBox)Grid_AuthorEntry.Rows[0].Cells[6].FindControl("INREquivalent");
        TextBox Details = (TextBox)Grid_AuthorEntry.Rows[0].Cells[0].FindControl("Details");

    }



    private EmailDetails SendMail()
    {
        EmailDetails details = new EmailDetails();


        string FooterText = ConfigurationManager.AppSettings["FooterText"].ToString();

        GrantData j = new GrantData();

        j.FromUTN = Session[" resultutn"].ToString();

        details.EmailSubject = "Project Entry UTN Update < " + DropDownListTypeGrant.SelectedValue + " _ " + TextBoxID.Text + "_";
        details.MsgBody = "<span style=\"font-size: 9pt; color: #3300cc; font-family: Verdana\"><h4>Dear Sir/Madam,</h4> <br>" +
         "<b> The following Project UTN value  has been Updated in Project Repository : <br> " +
             "<br>" +
               "Project Type  : " + DropDownListTypeGrant.SelectedItem + "<br>" +
                "Project Unit  :  " + DropDownListGrUnit.SelectedItem + "<br>" +
            "Project Id  :  " + TextBoxID.Text + "<br>" +
             "From UTN  :  " + j.FromUTN + "<br>" +
              "To UTN  :  " + TextBoxUTN.Text + "<br>" +
            "Title  : " + TextBoxTitle.Text + "<br>" +"<br>" + "<br>" + "<br>" + "<br>" + FooterText +
            //"Added By  : " + myArrayListInvestigatorName[0].ToString() + "<br>" +
            //"Investigators  : " + auhtorsSConc + "<br>" + "<br>" + "<br>" + "<br>" + "<br>" + FooterText +
            "</span>";

        details.FromEmail = ConfigurationManager.AppSettings["FromAddress"].ToString();
        
            details.Module = "GAPP";
        
        
        
            for (int i = 0; i < Grid_AuthorEntry.Rows.Count; i++)
            {
                DropDownList DropdownMuNonMu=(DropDownList)Grid_AuthorEntry.Rows[i].Cells[0].FindControl("DropdownMuNonMu");
                DropDownList AuthorType = (DropDownList)Grid_AuthorEntry.Rows[i].Cells[3].FindControl("AuthorType");
                TextBox MailId = (TextBox)Grid_AuthorEntry.Rows[i].Cells[7].FindControl("MailId");
                DropDownList isLeadPI = (DropDownList)Grid_AuthorEntry.Rows[i].Cells[3].FindControl("isLeadPI");



                if (DropdownMuNonMu.SelectedValue == "M")
                {
                    if (AuthorType.SelectedValue == "P")
                    {
                        if (details.ToEmail == null)
                        {
                            details.ToEmail = MailId.Text;

                        }
                        else
                        {
                            details.ToEmail = details.ToEmail + ',' + MailId.Text;

                        }
                    }
                }
             }
      

            Business bus = new Business();
            string dataresult;
            dataresult = bus.getUserMailIdList(Session["UserId"].ToString());
            details.CCEmail = dataresult.ToString();
            
        return details;
    }


    //Sanction Details

    //Initialize row for sanction details
    private void SanctionSetInitialRow()
    {

        DataTable dt1 = new DataTable();
        DataRow dr1 = null;

        dt1.Columns.Add(new DataColumn("SanctionNumber", typeof(string)));
        dt1.Columns.Add(new DataColumn("SanctionDate", typeof(string)));      
        dt1.Columns.Add(new DataColumn("SanctionCapitalAmount", typeof(string)));
        dt1.Columns.Add(new DataColumn("SanctionOperatingAmount", typeof(string)));
        dt1.Columns.Add(new DataColumn("SanctionTotalAmount", typeof(string)));
        dt1.Columns.Add(new DataColumn("Narration", typeof(string)));
        dr1 = dt1.NewRow();

        dr1["SanctionNumber"] = string.Empty;
        dr1["SanctionDate"] = string.Empty;
        dr1["SanctionCapitalAmount"] = string.Empty;
        dr1["SanctionOperatingAmount"] = string.Empty;
        dr1["SanctionTotalAmount"] = string.Empty;
        dr1["Narration"] = string.Empty;
        dt1.Rows.Add(dr1);

        ViewState["Sanction"] = dt1;
        GridViewSanction.DataSource = dt1;
        GridViewSanction.DataBind();


    }

    protected void btnaddsanction(object sender, EventArgs e)
    {
        if (GridViewSanction.Rows.Count == 0)
        {
            SanctionSetInitialRow();
        }

        else
        {
            int rowIndex = 0;

            if (ViewState["Sanction"] != null)
            {
                DataTable dtSanDetailCurrentTable1 = (DataTable)ViewState["Sanction"];

                DataRow drCurrentRow = null;
                if (dtSanDetailCurrentTable1.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtSanDetailCurrentTable1.Rows.Count; i++)
                    {

                        TextBox sanctionNo = (TextBox)GridViewSanction.Rows[rowIndex].Cells[0].FindControl("txtsanctionNo");
                        TextBox Sanctiondate = (TextBox)GridViewSanction.Rows[rowIndex].Cells[1].FindControl("txtSanctiondate");
                        TextBox sancapitalAmount = (TextBox)GridViewSanction.Rows[rowIndex].Cells[2].FindControl("txtsancapitalAmount");
                        TextBox SanOpeAmt = (TextBox)GridViewSanction.Rows[rowIndex].Cells[3].FindControl("txtSanOpeAmt");
                        TextBox santotalAmount = (TextBox)GridViewSanction.Rows[rowIndex].Cells[4].FindControl("txtsantotalAmount");
                        TextBox Narration = (TextBox)GridViewSanction.Rows[rowIndex].Cells[4].FindControl("txtNarration");
                        drCurrentRow = dtSanDetailCurrentTable1.NewRow();
                        if (sanctionNo.Text != "")
                        {
                            dtSanDetailCurrentTable1.Rows[i - 1]["SanctionNumber"] = sanctionNo.Text;
                        }

                        if (Sanctiondate.Text != "")
                        {
                            dtSanDetailCurrentTable1.Rows[i - 1]["SanctionDate"] = Sanctiondate.Text;
                        }
                        if (santotalAmount.Text != "")
                        {
                            dtSanDetailCurrentTable1.Rows[i - 1]["SanctionTotalAmount"] = santotalAmount.Text;
                        }
                        if (sancapitalAmount.Text != "")
                        {
                            dtSanDetailCurrentTable1.Rows[i - 1]["SanctionCapitalAmount"] = sancapitalAmount.Text;
                        }
                        if (SanOpeAmt.Text != "")
                        {
                            dtSanDetailCurrentTable1.Rows[i - 1]["SanctionOperatingAmount"] = SanOpeAmt.Text;
                        }
                        if (Narration.Text != "")
                        {
                            dtSanDetailCurrentTable1.Rows[i - 1]["Narration"] = Narration.Text;
                        }
                        rowIndex++;
                    }
                    dtSanDetailCurrentTable1.Rows.Add(drCurrentRow);
                    ViewState["Sanction"] = dtSanDetailCurrentTable1;
                    GridViewSanction.DataSource = dtSanDetailCurrentTable1;
                    GridViewSanction.DataBind();
                }
            }
            else
            {
                Response.Write("ViewState is null");
            }

            SanctionSetPreviousData();
        }

    }

    //Sanction Set Previous Data
    private void SanctionSetPreviousData()
    {
        int rowIndex = 0;
        if (ViewState["Sanction"] != null)
        {
            DataTable dt = (DataTable)ViewState["Sanction"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    TextBox sanctionNo = (TextBox)GridViewSanction.Rows[rowIndex].Cells[0].FindControl("txtsanctionNo");
                    TextBox Sanctiondate = (TextBox)GridViewSanction.Rows[rowIndex].Cells[1].FindControl("txtSanctiondate");
                    TextBox santotalAmount = (TextBox)GridViewSanction.Rows[rowIndex].Cells[4].FindControl("txtsantotalAmount");
                    TextBox sancapitalAmount = (TextBox)GridViewSanction.Rows[rowIndex].Cells[2].FindControl("txtsancapitalAmount");
                    TextBox SanOpeAmt = (TextBox)GridViewSanction.Rows[rowIndex].Cells[3].FindControl("txtSanOpeAmt");
                    TextBox Narration = (TextBox)GridViewSanction.Rows[rowIndex].Cells[5].FindControl("txtNarration");
                    sanctionNo.Text = dt.Rows[i]["SanctionNumber"].ToString();
                    if (dt.Rows[i]["SanctionDate"].ToString() != "")
                    {
                        DateTime date = Convert.ToDateTime(dt.Rows[i]["SanctionDate"]);
                        Sanctiondate.Text = date.ToShortDateString();

                    }
                    santotalAmount.Text = dt.Rows[i]["SanctionTotalAmount"].ToString();
                    sancapitalAmount.Text = dt.Rows[i]["SanctionCapitalAmount"].ToString();
                    SanOpeAmt.Text = dt.Rows[i]["SanctionOperatingAmount"].ToString();
                    Narration.Text = dt.Rows[i]["Narration"].ToString();
                    rowIndex++;
                }
            }
        }
    }
    protected void OnRowDataBound1(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList DropdownMuNonMu = (e.Row.FindControl("DropdownMuNonMu") as DropDownList);
            SqlDataSourceAuthorType.SelectCommand = "SELECT Id,DisplayName FROM [Author_Type_M] where (Id = 'M') or (Id = 'S') or (Id = 'N') or (Id = 'O') or (Id = 'E') ";

            DropdownMuNonMu.DataTextField = "DisplayName";
            DropdownMuNonMu.DataValueField = "Id";
            DropdownMuNonMu.DataBind();
        }

    }


    protected void Grid_Sanction_RowDeleting(Object sender, GridViewDeleteEventArgs e)
    {
        SanSetRowData();
        if (ViewState["Sanction"] != null)
        {
            DataTable dt = (DataTable)ViewState["Sanction"];
            DataRow drCurrentRow = null;
            int rowIndex = Convert.ToInt32(e.RowIndex);
            if (dt.Rows.Count > 1 && rowIndex != 0)
            {
                dt.Rows.Remove(dt.Rows[rowIndex]);
                drCurrentRow = dt.NewRow();
                ViewState["Sanction"] = dt;
                GridViewSanction.DataSource = dt;
                GridViewSanction.DataBind();

                SanctionSetPreviousData();
                // gridAmtChanged(sender, e);
            }
        }
    }

    private void SanSetRowData()
    {
        int rowIndex = 0;

        if (ViewState["Sanction"] != null)
        {
            DataTable dtSanDetailCurrentTable1 = (DataTable)ViewState["Sanction"];
            DataRow drCurrentRow = null;
            if (dtSanDetailCurrentTable1.Rows.Count > 0)
            {
                for (int i = 1; i <= dtSanDetailCurrentTable1.Rows.Count; i++)
                {
                    TextBox sanctionNo = (TextBox)GridViewSanction.Rows[rowIndex].Cells[1].FindControl("txtsanctionNo");
                    TextBox Sanctiondate = (TextBox)GridViewSanction.Rows[rowIndex].Cells[2].FindControl("txtSanctiondate");
                    TextBox santotalAmount = (TextBox)GridViewSanction.Rows[rowIndex].Cells[3].FindControl("txtsantotalAmount");
                    TextBox sancapitalAmount = (TextBox)GridViewSanction.Rows[rowIndex].Cells[4].FindControl("txtsancapitalAmount");
                    TextBox SanOpeAmt = (TextBox)GridViewSanction.Rows[rowIndex].Cells[5].FindControl("txtSanOpeAmt");
                    TextBox Narration = (TextBox)GridViewSanction.Rows[rowIndex].Cells[6].FindControl("txtNarration");

                    drCurrentRow = dtSanDetailCurrentTable1.NewRow();

                }
                ViewState["Sanction"] = dtSanDetailCurrentTable1;

            }

            else
            {
                Response.Write("ViewState is null");
            }
            //SetPreviousData();
        }

    }


    //Gridview Sanction (Finance)
    protected void GridViewSearchsan_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        dataBind();
        GridViewsanSearch.PageIndex = e.NewPageIndex;
        GridViewsanSearch.DataBind();
    }
    protected void GridViewsanSearch_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        ImageButton EditButton = (ImageButton)e.Row.FindControl("BtnEdit1");
    }

    public void GridViewsanSearch_RowCommand(Object sender, GridViewCommandEventArgs e)
    {
        string pid = null;
        string typeEntry = null;
        string Status = null;
        if (e.CommandName == "Edit")
        {

            GridViewRow rowSelect = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
            int rowindex = rowSelect.RowIndex;
            HiddenField TypeOfEntry = (HiddenField)GridViewsanSearch.Rows[rowindex].Cells[8].FindControl("hiddenProjectType");
            typeEntry = TypeOfEntry.Value;
            //  typeEntry = GridViewSearch.Rows[rowindex].Cells[8].Text.ToString();
            pid = GridViewsanSearch.Rows[rowindex].Cells[3].Text.Trim().ToString();
            Status = GridViewsanSearch.Rows[rowindex].Cells[10].Text.Trim().ToString();
            string Unit = GridViewsanSearch.Rows[rowindex].Cells[2].Text.Trim().ToString();
            Session["TempPid"] = pid;
            Session["TempTypeEntry"] = typeEntry;//maintaining a session variable, passing it to registration page
            Session["TempStatus"] = Status;
            Session["ProjectUnit"] = Unit;
        }

        else if (e.CommandName == "View")
        {
            GridViewRow rowSelect = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
            int rowindex = rowSelect.RowIndex;
            //  typeEntry = GridViewSearch.Rows[rowindex].Cells[8].Text.ToString();
            HiddenField TypeOfEntry = (HiddenField)GridViewsanSearch.Rows[rowindex].Cells[8].FindControl("hiddenProjectType");
            typeEntry = TypeOfEntry.Value;
            pid = GridViewsanSearch.Rows[rowindex].Cells[2].Text.Trim().ToString();
            Status = GridViewsanSearch.Rows[rowindex].Cells[10].Text.Trim().ToString();
            Session["TempPid"] = pid;
            Session["TempTypeEntry"] = typeEntry;
            Session["TempStatus"] = Status;
         

        }
    }

    public void edit1(Object sender, GridViewEditEventArgs e)
    {
        GridViewsanSearch.EditIndex = e.NewEditIndex;
        fnRecordExist(sender, e);
    }

    protected void onRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            // get the controls for each row, create a JS event, and pass the client ids into the js event
            TextBox txtBdls = (TextBox)e.Row.FindControl("txtsancapitalAmount");
            TextBox txtPcs = (TextBox)e.Row.FindControl("txtSanOpeAmt");
            TextBox lblBdlCt = (TextBox)e.Row.FindControl("txtsantotalAmount");

            Regex regex = new Regex("^([0-9]{1,3},([0-9]{3},)*[0-9]{3}|[0-9]+)(.[0-9][0-9]*$)?$");
            if (txtBdls.Text != "" && !regex.IsMatch(txtBdls.Text))
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Capital Amount must be in numbers!')</script>");
            }
            else
            {
                txtBdls.Attributes.Add("onchange", "CalculateAmount('" + txtBdls.ClientID + "', '" + txtPcs.ClientID + "','" + lblBdlCt.ClientID + "')");
                txtPcs.Attributes.Add("onchange", "CalculateAmount1('" + txtBdls.ClientID + "', '" + txtPcs.ClientID + "','" + lblBdlCt.ClientID + "')");
            }
        }
    }


    //Incentive 

    //incentive Details
    protected void SetIntialRowIncentive()
    {
       
        DataTable dt = new DataTable();
        dt.Columns.Add("SanctionEntryNo", typeof(string));
        dt.Columns.Add("IncentivePayDate", typeof(string));
        dt.Columns.Add("IncentivePayAmount", typeof(string));
        dt.Columns.Add("Narration", typeof(string));
        DataRow dr = dt.NewRow();
        dr["SanctionEntryNo"] = string.Empty;
        dr["IncentivePayDate"] = string.Empty;
        dr["IncentivePayAmount"] = string.Empty;
        dr["Narration"] = string.Empty;
        dt.Rows.Add(dr);
        ViewState["IncentiveDetails"] = dt;
        gvIncentiveDetails.DataSource = dt;
        gvIncentiveDetails.DataBind();

    }

    protected void gvIncentiveDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (ViewState["IncentiveDetails"] != null)
        {
            DataTable dt = (DataTable)ViewState["IncentiveDetails"];
            DataRow drCurrentRow = null;
            int rowIndex = Convert.ToInt32(e.RowIndex);
            if (dt.Rows.Count > 1)
            {
                dt.Rows.Remove(dt.Rows[rowIndex]);
                drCurrentRow = dt.NewRow();
                ViewState["IncentiveDetails"] = dt;
                gvIncentiveDetails.DataSource = dt;
                gvIncentiveDetails.DataBind();

                SetOldData();
            }
        }

    }

    private void SetOldData()
    {
        int rowIndex = 0;
        if (ViewState["IncentiveDetails"] != null)
        {
            DataTable dt = (DataTable)ViewState["IncentiveDetails"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DropDownList ddlSanctionEntryNo = (DropDownList)gvIncentiveDetails.Rows[rowIndex].Cells[0].FindControl("ddlSanctionEntryNo");
                    TextBox txtincentivedate = (TextBox)gvIncentiveDetails.Rows[rowIndex].Cells[1].FindControl("txtincentivedate");
                    TextBox txtincentiveAmount = (TextBox)gvIncentiveDetails.Rows[rowIndex].Cells[2].FindControl("txtincentiveAmount");
                    TextBox txtComments = (TextBox)gvIncentiveDetails.Rows[rowIndex].Cells[4].FindControl("txtComments");

                    if (ddlSanctionEntryNo.SelectedValue != "")
                    {
                        dt.Rows[i]["SanctionEntryNo"] = ddlSanctionEntryNo.SelectedValue;
                    }
                    if (dt.Rows[i]["IncentivePayDate"].ToString() != "")
                    {
                        DateTime date = Convert.ToDateTime(dt.Rows[i]["IncentivePayDate"].ToString());
                        txtincentivedate.Text = date.ToShortDateString();
                    }
                    if (dt.Rows[i]["IncentivePayAmount"].ToString() != "")
                    {
                        txtincentiveAmount.Text = dt.Rows[i]["IncentivePayAmount"].ToString();
                    }
                    txtComments.Text = dt.Rows[i]["Narration"].ToString();

                    rowIndex++;
                }
            }
        }
    }


    
    protected void AddIncentive(object sender,EventArgs e)
    {

        if (gvIncentiveDetails.Rows.Count == 0)
        {
            //BindGridview();
        }


        else
        {
            int rowIndex = 0;

            if (ViewState["IncentiveDetails"] != null)
            {
                DataTable dt = (DataTable)ViewState["IncentiveDetails"];
                DataRow drCurrentRow = null;
                if (dt.Rows.Count > 0)
                {
                    for (int i = 1; i <= dt.Rows.Count; i++)
                    {
                        DropDownList ddlSanctionEntryNo = (DropDownList)gvIncentiveDetails.Rows[rowIndex].Cells[0].FindControl("ddlSanctionEntryNo");
                        TextBox txtincentivedate = (TextBox)gvIncentiveDetails.Rows[rowIndex].Cells[1].FindControl("txtincentivedate");
                        TextBox txtincentiveAmount = (TextBox)gvIncentiveDetails.Rows[rowIndex].Cells[2].FindControl("txtincentiveAmount");
                        //TextBox txtpayedto = (TextBox)gvIncentiveDetails.Rows[rowIndex].Cells[2].FindControl("txtpayedto");
                        //// TextBox txtInstitution = (TextBox)gvDetails.Rows[rowIndex].Cells[1].FindControl("txtInstitution");

                        TextBox txtComments = (TextBox)gvIncentiveDetails.Rows[rowIndex].Cells[4].FindControl("txtComments");
                        drCurrentRow = dt.NewRow();

                        //drCurrentRow["rowid"] = i + 1;
                        dt.Rows[i - 1]["SanctionEntryNo"] = ddlSanctionEntryNo.SelectedValue;
                        if (txtincentivedate.Text != "")
                        {
                            dt.Rows[i - 1]["IncentivePayDate"] = txtincentivedate.Text;
                        }

                        if (txtincentiveAmount.Text != "")
                        {
                            dt.Rows[i - 1]["IncentivePayAmount"] = txtincentiveAmount.Text;
                        }
                        //dt.Rows[i - 1]["PayedTo"] = txtpayedto.Text;
                        dt.Rows[i - 1]["Narration"] = txtComments.Text;
                        rowIndex++;
                    }
                    dt.Rows.Add(drCurrentRow);
                    ViewState["IncentiveDetails"] = dt;
                    gvIncentiveDetails.DataSource = dt;
                    gvIncentiveDetails.DataBind();
                }
            }

            else
            {
                Response.Write("ViewState Value is Null");
            }

            SetOldDataIncentiveDetails();
        }
    }


    private void SetOldDataIncentiveDetails()
    {
        int rowIndex = 0;
        if (ViewState["IncentiveDetails"] != null)
        {
            DataTable dt = (DataTable)ViewState["IncentiveDetails"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DropDownList ddlSanctionEntryNo = (DropDownList)gvIncentiveDetails.Rows[rowIndex].Cells[0].FindControl("ddlSanctionEntryNo");
                    TextBox txtincentivedate = (TextBox)gvIncentiveDetails.Rows[rowIndex].Cells[1].FindControl("txtincentivedate");
                    TextBox txtincentiveAmount = (TextBox)gvIncentiveDetails.Rows[rowIndex].Cells[2].FindControl("txtincentiveAmount");
                    TextBox txtComments = (TextBox)gvIncentiveDetails.Rows[rowIndex].Cells[4].FindControl("txtComments");


                    if (dt.Rows[i]["IncentivePayDate"].ToString() != "")
                    {
                        DateTime date = Convert.ToDateTime(dt.Rows[i]["IncentivePayDate"].ToString());
                        txtincentivedate.Text = date.ToShortDateString();
                    }
                    if (dt.Rows[i]["IncentivePayAmount"].ToString() != "")
                    {
                        txtincentiveAmount.Text = dt.Rows[i]["IncentivePayAmount"].ToString();
                    }
                    ddlSanctionEntryNo.SelectedValue = dt.Rows[i]["SanctionEntryNo"].ToString();
                    txtComments.Text = dt.Rows[i]["Narration"].ToString();

                    rowIndex++;
                }
            }
        }
    }


    protected void IncentiveOnRowDataBound(Object sender, GridViewRowEventArgs e)
    {
        if (txtNoOFSanctions.Text != "")
        {
            int number = Convert.ToInt16(txtNoOFSanctions.Text);
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Find the drop-down (say in 3rd column)
                var dd = e.Row.Cells[1].Controls[0] as DropDownList;
                DropDownList ddl = (DropDownList)e.Row.FindControl("ddlSanctionEntryNo");
                for (int i = 1; i <= number; i++)
                {
                    string value = i.ToString();
                    ddl.Items.Add(new ListItem(value));
                }
            }
        }

    }


    //OverHead
    private void SetInitialRowOverhead()
    {
        DataTable dt1 = new DataTable();
        DataRow dr1 = null;
        // dt1.Columns.Add("Line", typeof(int));
        dt1.Columns.Add(new DataColumn("SanctionEntryNo", typeof(string)));
        dt1.Columns.Add(new DataColumn("OverheadTDate", typeof(string)));
        dt1.Columns.Add(new DataColumn("OverheadTAmount", typeof(string)));
        dt1.Columns.Add(new DataColumn("JVNumber", typeof(string)));
        dt1.Columns.Add(new DataColumn("Narration", typeof(string)));

        dr1 = dt1.NewRow();
        //dr1["Line"] = 1;
        dr1["SanctionEntryNo"] = string.Empty;
        dr1["OverheadTDate"] = string.Empty;
        dr1["OverheadTAmount"] = string.Empty;
        dr1["JVNumber"] = string.Empty;
        dr1["Narration"] = string.Empty;

        dt1.Rows.Add(dr1);

        ViewState["OverheadT"] = dt1;
        grvoverhead.DataSource = dt1;
        grvoverhead.DataBind();
    }

    protected void grvoverhead_RowDeleting1(object sender, GridViewDeleteEventArgs e)
    {
        if (ViewState["OverheadT"] != null)
        {
            DataTable dt = (DataTable)ViewState["OverheadT"];
            DataRow drCurrentRow = null;
            int rowIndex = Convert.ToInt32(e.RowIndex);
            if (dt.Rows.Count > 1)
            {
                dt.Rows.Remove(dt.Rows[rowIndex]);
                drCurrentRow = dt.NewRow();
                ViewState["OverheadT"] = dt;
                grvoverhead.DataSource = dt;
                grvoverhead.DataBind();
                SetPreviousDataOverheadT();
            }
        }
    }
    protected void SetPreviousDataOverheadT()
    {
        int rowIndex = 0;
        if (ViewState["OverheadT"] != null)
        {
            DataTable dt = (DataTable)ViewState["OverheadT"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DropDownList OHSanctionEntryNumber = (DropDownList)grvoverhead.Rows[rowIndex].Cells[0].FindControl("ddlSanctionEntryNoOH");
                    TextBox OHReceviedDate = (TextBox)grvoverhead.Rows[rowIndex].Cells[1].FindControl("txtOverheaddate");
                    TextBox OHReceviedAmount = (TextBox)grvoverhead.Rows[rowIndex].Cells[2].FindControl("txtOverheadAmount");
                    TextBox OHJVNumber = (TextBox)grvoverhead.Rows[rowIndex].Cells[3].FindControl("txtJVNumber");
                    TextBox OHNarration = (TextBox)grvoverhead.Rows[rowIndex].Cells[4].FindControl("txtoverheadComments");

                    OHSanctionEntryNumber.Text = dt.Rows[i]["SanctionEntryNo"].ToString();
                    if (dt.Rows[i]["OverheadTDate"].ToString() != "")
                    {
                        DateTime date = Convert.ToDateTime(dt.Rows[i]["OverheadTDate"].ToString());
                        OHReceviedDate.Text = date.ToShortDateString();
                    }
                    if (dt.Rows[i]["OverheadTAmount"].ToString() != "")
                    {
                        OHReceviedAmount.Text = dt.Rows[i]["OverheadTAmount"].ToString();
                    }
                    OHJVNumber.Text = dt.Rows[i]["JVNumber"].ToString();
                    OHNarration.Text = dt.Rows[i]["Narration"].ToString();
                    rowIndex++;
                }
            }
        }
    }
    private void AddOverhead()
    {
        if (grvoverhead.Rows.Count == 0)
        {
            SetInitialRowOverhead();
        }


        else
        {
            int rowIndex = 0;
            if (ViewState["OverheadT"] != null)
            {
                DataTable dt = (DataTable)ViewState["OverheadT"];
                DataRow drCurrentRow = null;
                if (dt.Rows.Count > 0)
                {
                    for (int i = 1; i <= dt.Rows.Count; i++)
                    {
                        DropDownList OHSanctionEntryNumber = (DropDownList)grvoverhead.Rows[0].Cells[0].FindControl("ddlSanctionEntryNoOH");
                        TextBox OHReceviedDate = (TextBox)grvoverhead.Rows[rowIndex].Cells[1].FindControl("txtOverheaddate");
                        TextBox OHReceviedAmount = (TextBox)grvoverhead.Rows[rowIndex].Cells[2].FindControl("txtOverheadAmount");
                        TextBox OHJVNumber = (TextBox)grvoverhead.Rows[rowIndex].Cells[3].FindControl("txtJVNumber");
                        TextBox OHNarration = (TextBox)grvoverhead.Rows[rowIndex].Cells[4].FindControl("txtoverheadComments");
                        drCurrentRow = dt.NewRow();
                        // drCurrentRow["Rowid"] = i + 1;
                        dt.Rows[i - 1]["SanctionEntryNo"] = OHSanctionEntryNumber.Text;
                        dt.Rows[i - 1]["OverheadTDate"] = OHReceviedDate.Text;
                        dt.Rows[i - 1]["OverheadTAmount"] = OHReceviedAmount.Text;
                        dt.Rows[i - 1]["JVNumber"] = OHJVNumber.Text;
                        dt.Rows[i - 1]["Narration"] = OHNarration.Text;

                        rowIndex++;
                    }
                    dt.Rows.Add(drCurrentRow);
                    ViewState["OverheadT"] = dt;
                    grvoverhead.DataSource = dt;
                    grvoverhead.DataBind();
                }
            }
            else
            {
                Response.Write("ViewState Value is Null");
            }
            SetPreviousDataOverheadT();
        }
    }
    protected void btnAdd_Click1(object sender, EventArgs e)
    {
        AddOverhead();
    }



    protected void RowDataBoundOverhead(Object sender, GridViewRowEventArgs e)
    {
        if (txtNoOFSanctions.Text != "")
        {
            int number = Convert.ToInt16(txtNoOFSanctions.Text);
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Find the drop-down (say in 3rd column)
                var dd = e.Row.Cells[1].Controls[0] as DropDownList;
                DropDownList ddl = (DropDownList)e.Row.FindControl("ddlSanctionEntryNoOH");
                for (int i = 1; i <= number; i++)
                {
                    string value = i.ToString();
                    ddl.Items.Add(new ListItem(value));
                }
            }
        }

    }

  

     //PopUp Amount
    protected void AddAmtClick(object sender, EventArgs e)
    {
        Button imgButton = (Button)sender;
        GridViewRow parentRow = (GridViewRow)imgButton.NamingContainer;
        int rowindex = parentRow.RowIndex;

        rowLabel.Text = rowindex.ToString();

        DropDownList sanctionnumber = (DropDownList)gvIncentiveDetails.Rows[rowindex].Cells[0].FindControl("ddlSanctionEntryNo");
        int sanctionentryno = Convert.ToInt16(sanctionnumber.SelectedValue);
        Sanction.Text = sanctionentryno.ToString();

        Business b = new Business();
        string id = TextBoxID.Text;
        string unit = DropDownListGrUnit.SelectedValue;
        DataTable dy = b.SelectIncentiveAmountDetailsExists(id, unit);

        int value = rowindex + 1;
        if (dy.Rows.Count != 0)
        {
            //SqlDataSourceAddAmt.SelectCommand = "Select ''  AS Row1, [LineNo] as indexv,EntryNo as Row,SanctionEntryNo,'' as rowIndexParent,'' as rowIndexChild,ProjectUnit as ProjectUnit, ID as ID,PayedTo as InvestigatorName ,Amount,InstitutionId as Institution  from ProjectIncentivePayDetails a where a.ID='" + id + "' and a.ProjectUnit='MUFOR' and a.[LineNo]=" + value + " UNION Select ROW_NUMBER() OVER (ORDER BY Projectnvestigator.[ID]) AS Row1,'',EntryNo as Row,'','','',ProjectUnit as ProjectUnit , ID as ID,InvestigatorName as InvestigatorName,'',Institution as Institution  from Projectnvestigator  where InvestigatorName not in  (Select PayedTo from ProjectIncentivePayDetails where ProjectIncentivePayDetails.ID='" + id + "' and ProjectIncentivePayDetails.ProjectUnit='" + unit + "') and ID='" + id + "' and ProjectUnit='" + unit + "'";

            SqlDataSourceAddAmt.SelectParameters.Clear();

            SqlDataSourceAddAmt.SelectParameters.Add("unit", unit);
            SqlDataSourceAddAmt.SelectParameters.Add("id", id);
            SqlDataSourceAddAmt.SelectParameters.Add("value", value.ToString());
            SqlDataSourceAddAmt.SelectCommand = " Select a.ProjectUnit,a.ID,a.InvestigatorType, a.EntryNo as Row,InvestigatorName,a.Institution as Institution,a.Department as Department, Amount from Projectnvestigator a left outer join ProjectIncentivePayDetails b on a.ProjectUnit=b.ProjectUnit and a.ID=b.ID and a.EntryNo=b.EntryNo and  b.[LineNo]=@value  and a.ProjectUnit=@unit  and a.ID=@id  where a.ProjectUnit=@unit   and a.ID=@id ";
            PanelAmount.Visible = true;
            popGridViewAmount.DataSourceID = "SqlDataSourceAddAmt";
            SqlDataSourceAddAmt.DataBind();
            popGridViewAmount.DataBind();

            int rowMisc = 0;
            for (int i = 1; i <= popGridViewAmount.Rows.Count; i++)
            {
                TextBox TextPopupMisc = (TextBox)popGridViewAmount.Rows[rowMisc].Cells[4].FindControl("txtAmount");
                if (TextPopupMisc.Text == "0")
                {
                    TextPopupMisc.Text = "";
                }
                else if(TextPopupMisc.Text != "")
                {
                    TextPopupMisc.Text = ((decimal)(Convert.ToDouble(TextPopupMisc.Text))).ToString();
                }
             
                rowMisc++;
            }

            ModalPopupExtender ModalPopupExtenderMisc = (ModalPopupExtender)gvIncentiveDetails.Rows[rowindex].FindControl("ModalPopupExtenderAmount");
            ModalPopupExtenderMisc.Show();
        }

        else
        {
            setModalWindowAmount(sender, e);
            ModalPopupExtender ModalPopupExtenderMisc = (ModalPopupExtender)gvIncentiveDetails.Rows[0].FindControl("ModalPopupExtenderAmount");
            ModalPopupExtenderMisc.Show();
        }

    }

    private void setModalWindowAmount(object sender, EventArgs e)
    {
        PanelAmount.Visible = true;
        popGridViewAmount.DataSourceID = "SqlDataSourceAddAmt";
        SqlDataSourceAddAmt.DataBind();
        popGridViewAmount.DataBind();
        popGridViewAmount.Visible = true;

    }

    //incentive amount break submit
    //protected void btnsubmitAmt(object sender, EventArgs e)
    //{
    //    ArrayList IncentiveSum = new ArrayList();
    //    DataRow dr = null;
    //    int rowIndexChild = 0, rowIndexParent = 0;
    //    double TotalCost = 0.0, TotalDisp = 0.0, Nul = 0.0;
    //    string AddCost = null, Investigator1 = null;
    //    TextBox cost = null;
    //    Label RowNumber = null;

    //    if (ViewState["temp_dt"] != null)
    //    {
    //        DataTable dtCurrentTable2 = (DataTable)ViewState["temp_dt"];
    //        for (int i = 0; i < popGridViewAmount.Rows.Count; i++)
    //        {

    //            GridViewRow row = popGridViewAmount.Rows[i];
    //            TextBox Amount = (TextBox)row.FindControl("txtAmount");
    //            if (Amount.Text != "")
    //            {
    //                RowNumber = (Label)popGridViewAmount.Rows[rowIndexChild].Cells[1].FindControl("LabelRow");
    //                cost = (TextBox)popGridViewAmount.Rows[rowIndexChild].Cells[4].FindControl("txtAmount");
    //                Label Investigator = (Label)popGridViewAmount.Rows[rowIndexChild].Cells[3].FindControl("InvestigatorNameLabel");
    //                Label Institution = (Label)popGridViewAmount.Rows[rowIndexChild].Cells[4].FindControl("Institution");

    //                AddCost = cost.Text.Trim();
    //                if (AddCost == "")
    //                {
    //                    AddCost = Nul.ToString();
    //                }
    //                Regex regex = new Regex("^([0-9]{1,3},([0-9]{3},)*[0-9]{3}|[0-9]+)(.[0-9][0-9]*$)?$");

    //                if (cost.Text != "" && !regex.IsMatch(cost.Text))
    //                {
    //                    ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert(' Amount must be in numbers!')</script>");
    //                    setModalWindowAmount(sender, e);

    //                    string rowVal2 = Request.Form["rowIndx"];
    //                    int rowIndex = Convert.ToInt32(rowVal2);
    //                    ModalPopupExtender ModalPopupExtenderMisc = (ModalPopupExtender)gvIncentiveDetails.Rows[rowIndex].FindControl("ModalPopupExtenderAmount");
    //                    ModalPopupExtenderMisc.Show();
    //                    return;
    //                }
    //                if (cost.Text == "0" || cost.Text == "0.0")
    //                {
    //                    ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert(' Amount must be in numbers!')</script>");
    //                    setModalWindowAmount(sender, e);

    //                    string rowVal2 = Request.Form["rowIndx"];
    //                    int rowIndex = Convert.ToInt32(rowVal2);
    //                    ModalPopupExtender ModalPopupExtenderMisc = (ModalPopupExtender)gvIncentiveDetails.Rows[rowIndex].FindControl("ModalPopupExtenderAmount");
    //                    ModalPopupExtenderMisc.Show();
    //                    // MiscPopupTextChanged(sender, e);
    //                    return;
    //                }
    //                IncentiveSum.Add(AddCost);
    //                dr = dtCurrentTable2.NewRow();
    //                dr["rowIndexParent"] = rowIndexParent + 1;
    //                dr["rowIndexChild"] = i + 1;
    //                int a = Convert.ToInt16(rowLabel.Text) + 1;
    //                dr["indexv"] = a;
    //                dr["Row"] = RowNumber.Text;
    //                dr["InvestigatorName"] = Investigator.Text;
    //                dr["Amount"] = cost.Text;
    //                dr["SanctionEntryNo"] = Sanction.Text;
    //                dr["Institution"] = Institution.Text;
    //                dtCurrentTable2.Rows.Add(dr);
    //            }
    //            rowIndexChild++;
    //        }
    //        ViewState["temp_dt"] = dtCurrentTable2;
    //    }



    //    else
    //    {
    //        DataTable dt = new DataTable();
    //        dt.Columns.Add(new DataColumn("rowIndexParent", typeof(string)));
    //        dt.Columns.Add(new DataColumn("rowIndexChild", typeof(string)));
    //        dt.Columns.Add(new DataColumn("indexv", typeof(string)));
    //        dt.Columns.Add(new DataColumn("Row", typeof(string)));
    //        dt.Columns.Add(new DataColumn("InvestigatorName", typeof(string)));
    //        dt.Columns.Add(new DataColumn("Amount", typeof(string)));
    //        dt.Columns.Add(new DataColumn("SanctionEntryNo", typeof(string)));
    //        dt.Columns.Add(new DataColumn("Institution", typeof(string)));

    //        for (int i = 0; i < popGridViewAmount.Rows.Count; i++)
    //        {

    //            GridViewRow row = popGridViewAmount.Rows[i];
    //            TextBox Amount = (TextBox)row.FindControl("txtAmount");
    //            if (Amount.Text != "")
    //            {
    //                RowNumber = (Label)popGridViewAmount.Rows[rowIndexChild].Cells[1].FindControl("LabelRow");
    //                cost = (TextBox)popGridViewAmount.Rows[rowIndexChild].Cells[4].FindControl("txtAmount");
    //                Label Investigator = (Label)popGridViewAmount.Rows[rowIndexChild].Cells[3].FindControl("InvestigatorNameLabel");
    //                Label Institution = (Label)popGridViewAmount.Rows[rowIndexChild].Cells[4].FindControl("Institution");

    //                AddCost = cost.Text.Trim();
    //                if (AddCost == "")
    //                {
    //                    AddCost = Nul.ToString();
    //                }
    //                Regex regex = new Regex("^([0-9]{1,3},([0-9]{3},)*[0-9]{3}|[0-9]+)(.[0-9][0-9]*$)?$");

    //                if (cost.Text != "" && !regex.IsMatch(cost.Text))
    //                {
    //                    ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert(' Amount must be in numbers!')</script>");
    //                    setModalWindowAmount(sender, e);

    //                    string rowVal2 = Request.Form["rowIndx"];
    //                    int rowIndex = Convert.ToInt32(rowVal2);
    //                    ModalPopupExtender ModalPopupExtenderMisc = (ModalPopupExtender)gvIncentiveDetails.Rows[rowIndex].FindControl("ModalPopupExtenderAmount");
    //                    ModalPopupExtenderMisc.Show();
    //                    return;
    //                }
    //                if (cost.Text == "0" || cost.Text == "0.0")
    //                {
    //                    ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert(' Amount must be in numbers!')</script>");
    //                    setModalWindowAmount(sender, e);

    //                    string rowVal2 = Request.Form["rowIndx"];
    //                    int rowIndex = Convert.ToInt32(rowVal2);
    //                    ModalPopupExtender ModalPopupExtenderMisc = (ModalPopupExtender)gvIncentiveDetails.Rows[rowIndex].FindControl("ModalPopupExtenderAmount");
    //                    ModalPopupExtenderMisc.Show();
    //                    // MiscPopupTextChanged(sender, e);
    //                    return;
    //                }
    //                IncentiveSum.Add(AddCost);
    //                dr = dt.NewRow();
    //                dr["rowIndexParent"] = rowIndexParent + 1;
    //                dr["rowIndexChild"] = i + 1;
    //                dr["indexv"] = rowLabel.Text;
    //                dr["Row"] = RowNumber.Text;
    //                dr["InvestigatorName"] = Investigator.Text;
    //                dr["Amount"] = cost.Text;
    //                dr["SanctionEntryNo"] = Sanction.Text;
    //                dr["Institution"] = Institution.Text;
    //                dt.Rows.Add(dr);
    //            }
    //            rowIndexChild++;
    //        }
    //        ViewState["temp_dt"] = dt;
    //    }

 
    //    Session["MiscRow" + rowIndexParent] = ViewState["temp_dt"];

    //    string rowV = rowLabel.Text;
    //    int index = Convert.ToInt32(rowV);

    //    for (int j = 0; j < IncentiveSum.Count; j++)
    //    {
    //        TotalCost = Convert.ToDouble(IncentiveSum[j]);
    //        double TotalCost1 = Convert.ToDouble(TotalCost);
    //        TotalDisp = TotalDisp + TotalCost1;
    //    }
    //    TextBox TotalText = (TextBox)gvIncentiveDetails.Rows[index].Cells[2].FindControl("txtincentiveAmount");
    //    TotalText.Text = TotalDisp.ToString();
    //    setModalWindowAmount(sender, e);

    //    //DataTable dtCurrentTable1 = (DataTable)ViewState["temp_dt"];
    //    //DataRow drCurrentRow1 = null;
    //    //if (dtCurrentTable1.Rows.Count > 0)
    //    //{
    //    //    for (int i = 1; i <= dtCurrentTable1.Rows.Count; i++)
    //    //    {

    //    //        drCurrentRow1 = dtCurrentTable1.NewRow();
    //    //        string a1 = dtCurrentTable1.Rows[i - 1]["Row"].ToString();
    //    //        string a2 = dtCurrentTable1.Rows[i - 1]["InvestigatorName"].ToString();
    //    //        string a3 = dtCurrentTable1.Rows[i - 1]["Amount"].ToString();


    //    //    }


    //    //    ViewState["temp_dt"] = dtCurrentTable1;
    //    //}

    //}


    protected void btnsubmitAmt(object sender, EventArgs e)
    {
        ArrayList IncentiveSum = new ArrayList();
        DataRow dr = null;
        int rowIndexChild = 0, rowIndexParent = 0;
        double TotalCost = 0.0, TotalDisp = 0.0, Nul = 0.0;
        string AddCost = null;
        TextBox cost = null;
        Label RowNumber = null;
        
        string rowVal1 = rowLabel.Text;

        rowIndexParent = Convert.ToInt32(rowVal1);
     
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("rowIndexParent", typeof(string)));
            dt.Columns.Add(new DataColumn("rowIndexChild", typeof(string)));
            dt.Columns.Add(new DataColumn("Row", typeof(string)));
            dt.Columns.Add(new DataColumn("InvestigatorName", typeof(string)));
            dt.Columns.Add(new DataColumn("Amount", typeof(string)));
            dt.Columns.Add(new DataColumn("SanctionEntryNo", typeof(string)));
            dt.Columns.Add(new DataColumn("Institution", typeof(string)));
            dt.Columns.Add(new DataColumn("Department", typeof(string)));
            for (int i = 0; i < popGridViewAmount.Rows.Count; i++)
            {

                GridViewRow row = popGridViewAmount.Rows[i];
                TextBox Amount = (TextBox)row.FindControl("txtAmount");
                if (Amount.Text != "")
                {
                    RowNumber = (Label)popGridViewAmount.Rows[rowIndexChild].Cells[0].FindControl("LabelRow");
                    cost = (TextBox)popGridViewAmount.Rows[rowIndexChild].Cells[4].FindControl("txtAmount");
                    Label Investigator = (Label)popGridViewAmount.Rows[rowIndexChild].Cells[3].FindControl("InvestigatorNameLabel");
                    Label Institution = (Label)popGridViewAmount.Rows[rowIndexChild].Cells[5].FindControl("Institution");
                    Label Dept = (Label)popGridViewAmount.Rows[rowIndexChild].Cells[6].FindControl("Dept");
                   
                    AddCost = cost.Text.Trim();
                    if (AddCost == "")
                    {
                        AddCost = Nul.ToString();
                    }
                    Regex regex = new Regex("^([0-9]{1,3},([0-9]{3},)*[0-9]{3}|[0-9]+)(.[0-9][0-9]*$)?$");

                    if (cost.Text != "" && !regex.IsMatch(cost.Text))
                    {
                        ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert(' Amount must be in numbers!')</script>");
                        setModalWindowAmount(sender, e);

                        string rowVal2 = Request.Form["rowIndx"];
                        int rowIndex = Convert.ToInt32(rowVal2);
                        ModalPopupExtender ModalPopupExtenderMisc = (ModalPopupExtender)gvIncentiveDetails.Rows[rowIndex].FindControl("ModalPopupExtenderAmount");
                        ModalPopupExtenderMisc.Show();
                        return;
                    }
                    if (cost.Text == "0" || cost.Text == "0.0")
                    {
                        ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert(' Amount must be in numbers!')</script>");
                        setModalWindowAmount(sender, e);

                        string rowVal2 = Request.Form["rowIndx"];
                        int rowIndex = Convert.ToInt32(rowVal2);
                        ModalPopupExtender ModalPopupExtenderMisc = (ModalPopupExtender)gvIncentiveDetails.Rows[rowIndex].FindControl("ModalPopupExtenderAmount");
                        ModalPopupExtenderMisc.Show();
                        // MiscPopupTextChanged(sender, e);
                        return;
                    }
                    IncentiveSum.Add(AddCost);
                    dr = dt.NewRow();
                    dr["rowIndexParent"] = rowIndexParent + 1;
                    dr["rowIndexChild"] = i + 1;
                    dr["Row"] = RowNumber.Text;
                    dr["InvestigatorName"] = Investigator.Text;
                    double amount = Convert.ToDouble(cost.Text);
                    dr["Amount"] = Math.Round(amount,2);
                    dr["SanctionEntryNo"] = Sanction.Text;
                    dr["Institution"] = Institution.Text;
                    dr["Department"] = Dept.Text;
                    dt.Rows.Add(dr);
                }
                rowIndexChild++;
            }
         ViewState["temp_dt"] = dt;
         Session["MiscRowIncentive" + rowIndexParent] = dt;

        int a = dt.Rows.Count;
        string rowV = rowLabel.Text;
        int index = Convert.ToInt32(rowV);
        double value = 0.0;
        for (int j = 0; j < IncentiveSum.Count; j++)
        {
            TotalCost = Convert.ToDouble(IncentiveSum[j]);
            double TotalCost1 = Convert.ToDouble(TotalCost);
            TotalDisp = TotalDisp + TotalCost1;
             value = Math.Round(TotalDisp, 2);
        }
        if (TotalDisp != 0.0)
        {
            TextBox TotalText = (TextBox)gvIncentiveDetails.Rows[index].Cells[2].FindControl("txtincentiveAmount");
            TotalText.Text = value.ToString();
        }
        setModalWindowAmount(sender, e);
    }



    //Saving Sanction details
    protected void BtnSanctionSave_Click(object sender, EventArgs e)
    {

        if (!Page.IsValid)
        {
            return;
        }
       
        try
        {

            Business B = new Business();
            GrantData j = new GrantData();
            DataTable dtSanDetailCurrentTable2 = (DataTable)ViewState["Sanction"];


            //Sanction Details
            j.AccountHead = txtaccounthead.Text.Trim();
            if (j.AccountHead == "")
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please enter Account Head!')</script>");
                return;
            }
            j.SancType = DropDownListSanType.SelectedValue;

            if (TextBoxProjectCommencementDate.Text != "")
            {
                j.ProjectCommencementDate = Convert.ToDateTime(TextBoxProjectCommencementDate.Text.Trim());
            }
            if (TextBoxProjectCloseDate.Text != "")
            {
                j.ProjectCloseDate = Convert.ToDateTime(TextBoxProjectCloseDate.Text.Trim());
            }
            if (TextBoxExtendedDate.Text != "")
            {
                j.ExtendedDate = Convert.ToDateTime(TextBoxExtendedDate.Text.Trim());
            }
            j.AuditRequired = ddlauditrequired.SelectedValue.Trim();
            j.AccountHead = txtaccounthead.Text.Trim();
            if (txtInstitutionshare.Text != "")
            {
                j.InstitutionSahre = Math.Round(Convert.ToDouble(txtInstitutionshare.Text.Trim()), 2);
            }
            j.SanctionEntryNumber = dtSanDetailCurrentTable2.Rows.Count;
            j.ServiceTaxApplicable = DropDownList2.SelectedValue;
            j.Status=DropDownListProjStatus.SelectedValue;
            j.AddtionalComments=TextBoxAdComments.Text.Trim();

            int rowscount = GridViewSanction.Rows.Count;
            for (int i = 0; i < rowscount; i++)
            {
                TextBox santotalAmount1 = (TextBox)GridViewSanction.Rows[i].Cells[3].FindControl("txtsantotalAmount");
                TextBox sancapitalAmount1 = (TextBox)GridViewSanction.Rows[i].Cells[4].FindControl("txtsancapitalAmount");
                TextBox SanOpeAmt1 = (TextBox)GridViewSanction.Rows[i].Cells[5].FindControl("txtSanOpeAmt");

                if (santotalAmount1.Text.Trim() != "")
                {
                    j.SanctionTotalAmount = j.SanctionTotalAmount + Math.Round(Convert.ToDouble(santotalAmount1.Text.Trim()),2);
                }
                if (sancapitalAmount1.Text.Trim() != "")
                {

                    j.SanctionCapitalAmount = j.SanctionCapitalAmount + Math.Round(Convert.ToDouble(sancapitalAmount1.Text.Trim()), 2);
                }
                else
                {
                    ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please enter capital amount!')</script>");
                    return;
                }
                if (SanOpeAmt1.Text.Trim() != "")
                {
                    j.SanctionOperatingAmount = j.SanctionOperatingAmount + Math.Round(Convert.ToDouble(SanOpeAmt1.Text.Trim()), 2);
                }
                else
                {
                    ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please enter operating amount!')</script>");
                    return;
                }

                j.GID = TextBoxID.Text;
                j.GrantUnit = DropDownListGrUnit.SelectedValue;
                j.SanctionEntryNumber = dtSanDetailCurrentTable2.Rows.Count;
            }

    
            //sanction
            DataTable dtSanDetailCurrentTable1 = (DataTable)ViewState["Sanction"];
            GrantData[] SD3 = new GrantData[dtSanDetailCurrentTable1.Rows.Count];
            
            int rowIndex = 0;
            if (dtSanDetailCurrentTable1.Rows.Count > 0)
            {

                for (int i = 0; i < dtSanDetailCurrentTable1.Rows.Count; i++)
                {
                    SD3[i] = new GrantData();

                    TextBox sanctionNo = (TextBox)GridViewSanction.Rows[rowIndex].Cells[1].FindControl("txtsanctionNo");
                    TextBox Sanctiondate = (TextBox)GridViewSanction.Rows[rowIndex].Cells[2].FindControl("txtSanctiondate");
                    TextBox santotalAmount = (TextBox)GridViewSanction.Rows[rowIndex].Cells[3].FindControl("txtsantotalAmount");
                    TextBox sancapitalAmount = (TextBox)GridViewSanction.Rows[rowIndex].Cells[4].FindControl("txtsancapitalAmount");
                    TextBox SanOpeAmt = (TextBox)GridViewSanction.Rows[rowIndex].Cells[5].FindControl("txtSanOpeAmt");
                    TextBox Narration = (TextBox)GridViewSanction.Rows[rowIndex].Cells[6].FindControl("txtNarration");

                    if (Sanctiondate.Text == "")
                    {
                        ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Sanction date is not valid!')</script>");
                        return;
                    }
                    if (sanctionNo.Text == "")
                    {
                        ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please enter sanction number')</script>");
                        return;
                    }
                    string SanctionNo = sanctionNo.Text.Trim();
                    string Sanction_date = Sanctiondate.Text.Trim();

                    string Total_Amt = santotalAmount.Text.Trim();
                    string Capital_Amt = sancapitalAmount.Text.Trim();
                    string Operating_Amt = SanOpeAmt.Text.Trim();
                    if (dtSanDetailCurrentTable1.Rows.Count > 0)
                    {
                        if (SanctionNo == "" && Sanction_date == "" && Total_Amt == "" && Capital_Amt == "")
                        {
                        }
                        else
                        {
                            SD3[i].SanctionNumber = sanctionNo.Text.Trim();
                            SD3[i].SanctionDate = Convert.ToDateTime(Sanctiondate.Text.Trim());
                            SD3[i].SanctionTotalAmount = Math.Round(Convert.ToDouble(santotalAmount.Text.Trim()),2);
                            SD3[i].SanctionCapitalAmount = Math.Round(Convert.ToDouble(sancapitalAmount.Text.Trim()),2);
                            SD3[i].SanctionOperatingAmount = Math.Round(Convert.ToDouble(SanOpeAmt.Text.Trim()),2);
                            SD3[i].SanctionNarration = Narration.Text.Trim();
                        }

                    }
                    
                    rowIndex++;
                }

            }

            GrantData[] JD7 = null;
            
                int result = B.UpdateSanctionDetails(j, SD3, JD7);
             if (result == 1)
             {
                 ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Grant Sanction Details updated Successfully  of ID: " + TextBoxID.Text + "')</script>");
                 log.Info("Grant Sanction Details updated Successfully, of ID: " + TextBoxID.Text);
                 int rowscount1 = GridViewSanction.Rows.Count;
                 for (int i = 0; i < rowscount; i++)
                 {
                     TextBox santotalAmount1 = (TextBox)GridViewSanction.Rows[i].Cells[3].FindControl("txtsantotalAmount");
                     TextBox sancapitalAmount1 = (TextBox)GridViewSanction.Rows[i].Cells[4].FindControl("txtsancapitalAmount");
                     TextBox SanOpeAmt1 = (TextBox)GridViewSanction.Rows[i].Cells[5].FindControl("txtSanOpeAmt");
                     TextBoxSanctionedamountTotal .Text= j.SanctionTotalAmount.ToString();
                     TextBoxSanctionedAmountCapital.Text = j.SanctionCapitalAmount.ToString();
                     TextBoxSanctionedAmountOperating.Text = j.SanctionOperatingAmount.ToString();
                     txtNoOFSanctions.Text = rowscount1.ToString();
                     
                 }
             }

                  
        }

        catch (Exception ex)
        {
            log.Error("Inside Catch Block Of Grant Sanction" + ex.Message + " UserID : " + Session["UserId"].ToString());

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
                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Input string was not in a correct format')</script>");
                log.Error("Error, of ID: " + TextBoxID.Text + " , Type: " + DropDownListTypeGrant.SelectedValue);

            }
            else if (ex.Message.Contains("There is already an open DataReader"))
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Grant data Creaton Failed)</script>");
                log.Info("Grant data Creation Saved..Upload failed, of ID: " + ex.Message + " " + TextBoxID.Text + " , Type: " + DropDownListTypeGrant.SelectedValue);

            }
            else if (ex.Message.Contains("Mailbox unavailable. The server response was: #5.1.0 Address rejecte"))
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Grant Sanction details Updated Successfully')</script>");
                log.Info("Grant created Successfully, of ID: " + TextBoxID.Text + " , Type: " + DropDownListTypeGrant.SelectedValue);
            }
            else if (ex.Message.Contains("Unable to send to a recipient"))
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Grant Sanction details Updated Successfully....Error in mail sending!!!!!!!!!!!!!!')</script>");
                log.Info("Grant created Successfully,Error in mail sending!!!!!!!!!!!!!, of ID: " + TextBoxID.Text + " , Type: " + DropDownListTypeGrant.SelectedValue);

            }
            else if (ex.Message.Contains("Object reference not set to an instance of an obje"))
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Grant Sanction details Updation Failed....Please contact admin')</script>");
                log.Error("Grant data Creaton Failed.....Please contact admin, id: " + TextBoxID.Text + " , Type: " + DropDownListTypeGrant.SelectedValue);
            }
            else
                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Grant Sanction details Updation failed')</script>");
            log.Error("Grant data Creaton Failed.... id: " + TextBoxID.Text + " , Type: " + DropDownListTypeGrant.SelectedValue);

        }
    }



    protected void BtnSaveFundDetails(object sender, EventArgs e)
    {

        if (!Page.IsValid)
        {
            return;
        }
        

        try
        {
            int result = 0;

            DataTable dtCurrentTableRecevie = (DataTable)ViewState["Bank"];
            RecieptData[] JD2 = null;
            GrantData journalbank = new GrantData();
            //insert Fund Reciept

            JD2 = new RecieptData[dtCurrentTableRecevie.Rows.Count];

                int rowIndex2 = 0;
                if (dtCurrentTableRecevie.Rows.Count > 0)
                {

                    for (int i = 0; i < dtCurrentTableRecevie.Rows.Count; i++)
                    {
                        JD2[i] = new RecieptData();
                        DropDownList SanctionEntryNumber = (DropDownList)GridView_bank.Rows[rowIndex2].Cells[0].FindControl("ddlSanctionEntryNo");

                        DropDownList CurrencyCode = (DropDownList)GridView_bank.Rows[rowIndex2].Cells[1].FindControl("CurrencyCode");
                        DropDownList ModeOfReceive = (DropDownList)GridView_bank.Rows[rowIndex2].Cells[2].FindControl("ModeOfRecevie");
                        TextBox ReceviedDate = (TextBox)GridView_bank.Rows[rowIndex2].Cells[3].FindControl("ReceviedDate");
                        TextBox ReceviedAmount = (TextBox)GridView_bank.Rows[rowIndex2].Cells[4].FindControl("ReceviedAmount");
                        TextBox ReceviedINR = (TextBox)GridView_bank.Rows[rowIndex2].Cells[5].FindControl("ReceviedINR");
                        TextBox TDS = (TextBox)GridView_bank.Rows[rowIndex2].Cells[6].FindControl("TDS");
                        TextBox ReferenceNo = (TextBox)GridView_bank.Rows[rowIndex2].Cells[7].FindControl("ReferenceNo");
                        //TextBox ReceivedBankID = (TextBox)GridView_bank.Rows[rowIndex2].Cells[9].FindControl("ReceivedBankId");
                        //TextBox ReceivedBank = (TextBox)GridView_bank.Rows[rowIndex2].Cells[9].FindControl("ReceivedBank");
                        TextBox CreditedBankId = (TextBox)GridView_bank.Rows[rowIndex2].Cells[10].FindControl("CreditedBankId");
                        TextBox CreditedBank = (TextBox)GridView_bank.Rows[rowIndex2].Cells[10].FindControl("CreditedBank");
                        TextBox ReceivedNarration = (TextBox)GridView_bank.Rows[rowIndex2].Cells[11].FindControl("ReceivedNarration");

                        if (dtCurrentTableRecevie.Rows.Count > 0)
                        {

                            //if (CreditedBank.Text == "")
                            //{
                            //    ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please enter credit bank')</script>");
                            //    return;
                            //}

                            Regex regex = new Regex("^([0-9]{1,3},([0-9]{3},)*[0-9]{3}|[0-9]+)(.[0-9][0-9]*$)?$");
                            if (ReceviedAmount.Text != "" && !regex.IsMatch(ReceviedAmount.Text))
                            {
                                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Recieved Amount must be in numbers!')</script>");
                                return;
                            }
                            if (ReceviedINR.Text != "" && !regex.IsMatch(ReceviedINR.Text))
                            {
                                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('INR amount must be in numbers!')</script>");
                                return;
                            }
                            journalbank.GID = TextBoxID.Text;
                            journalbank.GrantUnit = DropDownListGrUnit.SelectedValue;

                            JD2[i].FRSanctionEntryNo = Convert.ToInt16(SanctionEntryNumber.SelectedValue);
                            JD2[i].CurrencyCode = CurrencyCode.SelectedValue;
                            JD2[i].ModeOfReceive = ModeOfReceive.SelectedValue;
                            JD2[i].ReceviedDate = Convert.ToDateTime(ReceviedDate.Text.Trim());
                            double amount=Convert.ToDouble(ReceviedAmount.Text.Trim());
                            JD2[i].ReceviedAmmount = (Math.Round(amount, 2));
                            if (TDS.Text.Trim() != "")
                            {
                                JD2[i].TDS = Convert.ToDouble(TDS.Text.Trim());
                            }
                            JD2[i].ReferenceNumber = ReferenceNo.Text.Trim();
                            JD2[i].CreditedBankName = CreditedBankId.Text.Trim();
                            if (ReceviedINR.Text.Trim() != "")
                            {
                                JD2[i].ReceviedINR =(Math.Round( Convert.ToDouble(ReceviedINR.Text.Trim()),2));
                            }

                            //JD2[i].ReceivedBank = ReceivedBankID.Text.Trim();//id
                            JD2[i].CreditedBank = CreditedBankId.Text.Trim();//id
                            JD2[i].ReceivedNarration = ReceivedNarration.Text;
                          
                        }
                        rowIndex2++;
                    }

                    
                }

                if (journalbank.GID != null)
                {
                    result = B.InsertRecieptDetails(journalbank,JD2);
                    if (result > 0)
                    {
                        EmailDetails details = new EmailDetails();
                        details = SendFundMail();
                        details.Id = TextBoxID.Text;
                        details.Type = DropDownListTypeGrant.SelectedItem.ToString();
                        details.ProjectUnit = DropDownListGrUnit.SelectedItem.ToString();
                        details.UnitId = DropDownListGrUnit.SelectedValue.ToString();
                        SendMailObject obj1 = new SendMailObject();
                        bool result1 = obj1.InsertIntoEmailQueue(details);
                    }
                }

                if (result>0)
                {
                    ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Grant Fund Received details saved successfully  of ID: " + TextBoxID.Text + "')</script>");
                    log.Info("Grant Fund Received details saved successfully, of ID: " + TextBoxID.Text);
                }
        }

        catch (Exception ex)
        {
            
            log.Error("Inside Catch Block Of Grant" + ex.Message + " UserID : " + Session["UserId"].ToString());
            log.Error(ex.StackTrace);
            if (ex.Message.Contains("Failure sending mail."))
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Grant Fund Received details saved successfully  of ID: " + TextBoxID.Text + "')</script>");
            }
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
                log.Error("Error, of ID: " + TextBoxID.Text + " , Type: " + DropDownListTypeGrant.SelectedValue);

            }
            else if (ex.Message.Contains("There is already an open DataReader"))
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Grant data updation Failed)</script>");
                log.Info("Grant data Creation Saved..Upload failed, of ID: " + ex.Message + " " + TextBoxID.Text + " , Type: " + DropDownListTypeGrant.SelectedValue);

            }
            else if (ex.Message.Contains("Mailbox unavailable. The server response was: #5.1.0 Address rejecte"))
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Fund details updated Successfully')</script>");
                log.Info("Grant created Successfully, of ID: " + TextBoxID.Text + " , Type: " + DropDownListTypeGrant.SelectedValue);


            }
            else if (ex.Message.Contains("Unable to send to a recipient"))
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Fund details updated Successfully....Error in mail sending!!!!!!!!!!!!!!')</script>");
                log.Info("Fund details updated Successfully,Error in mail sending!!!!!!!!!!!!!, of ID: " + TextBoxID.Text + " , Type: " + DropDownListTypeGrant.SelectedValue);


            }
            else if (ex.Message.Contains("Object reference not set to an instance of an obje"))
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Grant data Creaton Failed....Please contact admin')</script>");
                log.Error("Fund details updated Successfully.....Please contact admin, id: " + TextBoxID.Text + " , Type: " + DropDownListTypeGrant.SelectedValue);

            }

            else

                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Grant data Creation failed!!!!!!!!!!!!')</script>");
            log.Error("Grant data Creaton Failed.... id: " + TextBoxID.Text + " , Type: " + DropDownListTypeGrant.SelectedValue);

        }
    }

    private EmailDetails SendFundMail()
    {
        log.Debug(" GrantEntry:Inside SendFundMail of type: " + DropDownListTypeGrant.SelectedValue + " ID: " + TextBoxID.Text + " Project Unit: " + DropDownListGrUnit.Text);
        EmailDetails details = new EmailDetails();
        ArrayList myArrayListInvestigator = new ArrayList();
        ArrayList myArrayListInvestigatorNAme = new ArrayList();
        ArrayList myArrayListReserachCoOrdinator = new ArrayList();
        ArrayList myArrayListFinanceTeam = new ArrayList();
        ArrayList myArrayListGrantAuthor = new ArrayList();
        DataSet ds = new DataSet();
        Business bus = new Business();

        DataSet author2 = bus.getGrantAuthorList(TextBoxID.Text, DropDownListGrUnit.SelectedValue);
        for (int i = 0; i < author2.Tables[0].Rows.Count; i++)
        {
            myArrayListGrantAuthor.Add(author2.Tables[0].Rows[i]["EmailId"].ToString());
        }


        DataSet ds3 = new DataSet();
        ds3 = bus.getReserachFinanceList();

        for (int i = 0; i < ds3.Tables[0].Rows.Count; i++)
        {
            myArrayListFinanceTeam.Add(ds3.Tables[0].Rows[i]["EmailId"].ToString());
        }


        DataSet dy = new DataSet();

        dy = bus.getInvietigatorListName(TextBoxID.Text, DropDownListGrUnit.SelectedValue);


        for (int i = 0; i < dy.Tables[0].Rows.Count; i++)
        {
            myArrayListInvestigatorNAme.Add(dy.Tables[0].Rows[i]["InvestigatorName"].ToString());
        }

        string auhtorsS = "";
        string auhtorsSConc = "";
        for (int i = 0; i < myArrayListInvestigatorNAme.Count; i++)
        {
            auhtorsS = myArrayListInvestigatorNAme[i].ToString();
            string con = " , ";
            if (i == 0)
            {
                auhtorsSConc = auhtorsS;
            }
            else
            {
                auhtorsSConc = auhtorsSConc + con + auhtorsS;
            }

        }

        string FooterText = ConfigurationManager.AppSettings["FooterText"].ToString();
        
        details.EmailSubject = "Project Entry <  " + DropDownListTypeGrant.SelectedValue + " _ " + TextBoxID.Text + "  > Sanctioned ";
        details.MsgBody = "<span style=\"font-size: 10pt; color: #3300cc; font-family: Verdana\"><h4>Dear Sir/Madam,</h4> <br>" +
          "<b>Installment amount added to RMS. Please login to application to check the details: <br> " +
              "<br>" +
                "Project Type  : " + DropDownListTypeGrant.SelectedItem + "<br>" +
                 "Project Unit  :  " + DropDownListGrUnit.SelectedItem + "<br>" +
             "Project Id  :  " + TextBoxID.Text + "<br>" +
              "UTN  :  " + TextBoxUTN.Text + "<br>" +
             "Title  : " + TextBoxTitle.Text + "<br>" +

             "Added By  : " + myArrayListInvestigatorNAme[0].ToString() + "<br>" +
             "Investigators  : " + auhtorsSConc + "<br>" + "<br>" + "<br>" + "<br>" + "<br>" + FooterText +
                 "</span>";



        details.FromEmail = ConfigurationManager.AppSettings["FromAddress"].ToString();
        details.Module = "GFUND";
        for (int i = 0; i < myArrayListGrantAuthor.Count; i++)
        {
            // Msg.To.Add(BuyerId_Array[0]+dir_domain);
            //Msg.To.Add(myArrayListGrantAuthor[i].ToString());
            string email = myArrayListGrantAuthor[i].ToString();
            if (details.ToEmail != null)
            {
                details.ToEmail = details.ToEmail + ',' + myArrayListGrantAuthor[i].ToString();
            }
            else
            {
                if (i == 0)
                {
                    details.ToEmail = myArrayListGrantAuthor[i].ToString();
                }
                else
                {
                    details.ToEmail = details.ToEmail + ',' + myArrayListGrantAuthor[i].ToString();
                }
            }
            
            //details.ToEmail = email;
            log.Info(" Email will be sent to Investigators '" + i + "' : '" + myArrayListGrantAuthor[i] + "' ");
        }


        for (int i = 0; i < myArrayListFinanceTeam.Count; i++)
        {
            // Msg.To.Add(BuyerId_Array[0]+dir_domain);
            // Msg.CC.Add(myArrayListFinanceTeam[i].ToString());
            string email = myArrayListFinanceTeam[i].ToString();
            if (details.CCEmail != null)
            {
                details.CCEmail = details.CCEmail + ',' + myArrayListFinanceTeam[i].ToString();
            }
            else
            {
                if (i == 0)
                {
                    details.CCEmail = myArrayListFinanceTeam[i].ToString();
                }
                else
                {
                    details.CCEmail = details.CCEmail + ',' + myArrayListFinanceTeam[i].ToString();
                }
               // details.CCEmail = email;
            }
            log.Info(" Email will be sent to Research Finance team '" + i + "' : '" + myArrayListFinanceTeam[i] + "' ");
        }

        return details;
    }


    //SAve Overhead Details
    protected void BtnSaveIncentiveDetails(object sender, EventArgs e)
    {

        if (!Page.IsValid)
        {
            return;
        }
      
        try
        {
            int result = 0;
            Business b = new Business();
            IncentiveData[] JD4 = null;
           
            GrantData journalbank = new GrantData();
            DataTable dtCurrentTable3 = (DataTable)ViewState["IncentiveDetails"];

            JD4 = new IncentiveData[dtCurrentTable3.Rows.Count];
            //Insert Incentive
            int rowIndex4 = 0;
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("rowIndexParent", typeof(string)));
            dt.Columns.Add(new DataColumn("rowIndexChild", typeof(string)));
            dt.Columns.Add(new DataColumn("Row", typeof(string)));
            dt.Columns.Add(new DataColumn("InvestigatorName", typeof(string)));
            dt.Columns.Add(new DataColumn("Amount", typeof(string)));
            dt.Columns.Add(new DataColumn("SanctionEntryNo", typeof(string)));
            dt.Columns.Add(new DataColumn("Institution", typeof(string)));
            dt.Columns.Add(new DataColumn("Department", typeof(string)));
            if (dtCurrentTable3 != null)
            {
                if (dtCurrentTable3.Rows.Count > 0)
                {
                    for (int i = 0; i < dtCurrentTable3.Rows.Count; i++)
                    {
                        JD4[i] = new IncentiveData();
                        DropDownList SanctionEntryNumber = (DropDownList)gvIncentiveDetails.Rows[rowIndex4].Cells[0].FindControl("ddlSanctionEntryNo");
                        TextBox txtincentivedate = (TextBox)gvIncentiveDetails.Rows[rowIndex4].Cells[0].FindControl("txtincentivedate");
                        TextBox txtincentiveAmount = (TextBox)gvIncentiveDetails.Rows[rowIndex4].Cells[2].FindControl("txtincentiveAmount");
                        TextBox txtComments = (TextBox)gvIncentiveDetails.Rows[rowIndex4].Cells[2].FindControl("txtComments");

                        string date = txtincentivedate.Text.Trim();
                        string ReceviedAmmount = txtincentiveAmount.Text.Trim();
                        string narration = txtComments.Text.Trim();
                        if (dtCurrentTable3.Rows.Count > 0)
                        {
                            journalbank.GID = TextBoxID.Text;
                            journalbank.GrantUnit = DropDownListGrUnit.SelectedValue;
                            JD4[i].SanctionEntryNo = Convert.ToInt16(SanctionEntryNumber.SelectedValue);
                            JD4[i].IncentivePayDate = Convert.ToDateTime(txtincentivedate.Text.Trim());
                            JD4[i].IncentivePayAmount = Convert.ToDouble(txtincentiveAmount.Text.Trim());
                            JD4[i].Narration = txtComments.Text;

                            DataTable miscrow = (DataTable)HttpContext.Current.Session["MiscRowIncentive" + i];

                            //if (miscrow == null)
                            //{
                            //Business obj = new Business();
                            //GrantData data = new GrantData();
                            //data = obj.SelectPrimaryInvestigator(TextBoxID.Text, DropDownListGrUnit.SelectedValue);

                            //    dr = dt.NewRow();
                            //    dr["rowIndexParent"] = i + 1;
                            //    dr["rowIndexChild"] = i + 1;
                            //    dr["Row"] = data.Entrynum;
                            //    dr["InvestigatorName"] = data.AuthorName;
                            //    dr["Amount"] = Convert.ToDouble(txtincentiveAmount.Text.Trim());
                            //    dr["SanctionEntryNo"] = Convert.ToInt16(SanctionEntryNumber.SelectedValue);
                            //    dr["Institution"] = data.Institution;
                            //    dr["Department"] = data.Department;
                            //    dt.Rows.Add(dr);
                            //   // Session["MiscRowIncentive" + i] = dt;
                            //}
                        }
                        //Session["MiscRowIncentive" + rowIndex4] = dt;
                        rowIndex4++;
                    }
                    
                }
            }
                 
                if (journalbank.GID != null)
                {
                    IncentiveData[] JD7 = null;
                    //DataTable dtCurrentTable1 = (DataTable)ViewState["temp_dt"];
                    //JD7 = new IncentiveData[dtCurrentTable1.Rows.Count];
                    //if (dtCurrentTable1.Rows.Count > 0)
                    //{
                    //    for (int i = 0; i < dtCurrentTable1.Rows.Count; i++)
                    //    {

                    //        JD7[i] = new IncentiveData();
                    //        JD7[i].index = Convert.ToInt16(dtCurrentTable1.Rows[i]["indexv"]);
                    //        JD7[i].PayedTo = dtCurrentTable1.Rows[i]["InvestigatorName"].ToString();
                    //        JD7[i].Amount = Convert.ToDouble(dtCurrentTable1.Rows[i]["Amount"]);
                    //        JD7[i].SanctionEntryNo = Convert.ToInt16(dtCurrentTable1.Rows[i]["SanctionEntryNo"]);
                    //        JD7[i].InstitutionId = dtCurrentTable1.Rows[i]["Institution"].ToString();
                    //    }


                    //    ViewState["temp_dt"] = dtCurrentTable1;
                    //}

                    result = B.InsertIncentiveDetails(journalbank, JD4, JD7);
                }
                if (result == 1)
                {
                    ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Grant Incentive details saved successfully  of ID: " + TextBoxID.Text + "')</script>");
                    log.Info("Grant Incentive details saved successfully, of ID: " + TextBoxID.Text);
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
                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Input string was not in a correct format')</script>");
                log.Error("Error, of ID: " + TextBoxID.Text + " , Type: " + DropDownListTypeGrant.SelectedValue);

            }
            else if (ex.Message.Contains("There is already an open DataReader"))
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Grant data Creaton Failed)</script>");
                log.Info("Grant data Creation Saved..Upload failed, of ID: " + ex.Message + " " + TextBoxID.Text + " , Type: " + DropDownListTypeGrant.SelectedValue);

            }
            else if (ex.Message.Contains("Mailbox unavailable. The server response was: #5.1.0 Address rejecte"))
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Grant data Created / Sanctioned Successfully')</script>");
                log.Info("Grant created Successfully, of ID: " + TextBoxID.Text + " , Type: " + DropDownListTypeGrant.SelectedValue);


            }
            else if (ex.Message.Contains("Unable to send to a recipient"))
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Grant data Created / Sanctioned Successfully....Error in mail sending!!!!!!!!!!!!!!')</script>");
                log.Info("Grant created Successfully,Error in mail sending!!!!!!!!!!!!!, of ID: " + TextBoxID.Text + " , Type: " + DropDownListTypeGrant.SelectedValue);


            }
            else if (ex.Message.Contains("Object reference not set to an instance of an obje"))
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Grant data Creaton Failed....Please contact admin')</script>");
                log.Error("Grant data Creaton Failed.....Please contact admin, id: " + TextBoxID.Text + " , Type: " + DropDownListTypeGrant.SelectedValue);
            }

            else

            ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Grant data Creation failed!!!!!!!!!!!!')</script>");
            log.Error("Grant data Creaton Failed.... id: " + TextBoxID.Text + " , Type: " + DropDownListTypeGrant.SelectedValue);

        }
    }



    //SAve Incentive Details
    protected void BtnSaveFinanceStatus(object sender, EventArgs e)
    {

        if (!Page.IsValid)
        {
            return;
        }
        
        try
        {
            GrantData j=new GrantData();
            j.FinanceProjectStatus=DropDownList3.SelectedValue;
            j.DateOfCompletion=Convert.ToDateTime(TextBox3.Text);
            j.Remarks=TextBox4.Text;
            j.GID = TextBoxID.Text;
            j.GrantUnit = DropDownListGrUnit.SelectedValue;

            int result = B.UpdateFinanceStatus(j);
                    
        
            if (result == 1)
            {
                if (DropDownList3.SelectedValue == "OPE")
                {
                    ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Finance Details Updated Sucessfully: " + TextBoxID.Text + "')</script>");
                    log.Info("Finance Details Updated Sucessfully, of ID: " + TextBoxID.Text);
                }
                else
                {
                    ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Finance Details Closed Sucessfully: " + TextBoxID.Text + "')</script>");
                    log.Info("Finance Details Closed Sucessfully, of ID: " + TextBoxID.Text);
                    Button11.Enabled = false;
                    BtnSaveOverhead.Enabled = false;
                    BtnSaveIncentive.Enabled = false;
                    BtnSaveBank.Enabled = false;
                    BtnSaveSan.Enabled = false;
                }
                
            }
        }

        catch (Exception ex)
        {
            log.Error("Inside Catch Block Of Grant" + ex.Message + " UserID : " + Session["UserId"].ToString());

            log.Error(ex.StackTrace);


            if (ex.Message.Contains("The string was not recognized as a valid DateTime"))
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Date is not valid!!!!!!!!!!!!')</script>");

            }
            if (ex.Message.Contains("String was not recognized as a valid DateTime."))
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Date is not valid!!!!!!!!!!!!')</script>");

            }

            else if (ex.Message.Contains("Input string was not in a correct format"))
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Error!!!')</script>");
                log.Error("Error, of ID: " + TextBoxID.Text + " , Type: " + DropDownListTypeGrant.SelectedValue);

            }
            else if (ex.Message.Contains("There is already an open DataReader"))
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Grant data Creaton Failed)</script>");
                log.Info("Grant data Creation Saved..Upload failed, of ID: " + ex.Message + " " + TextBoxID.Text + " , Type: " + DropDownListTypeGrant.SelectedValue);

            }
            else if (ex.Message.Contains("Mailbox unavailable. The server response was: #5.1.0 Address rejecte"))
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Grant data Created / Sanctioned Successfully')</script>");
                log.Info("Grant created Successfully, of ID: " + TextBoxID.Text + " , Type: " + DropDownListTypeGrant.SelectedValue);


            }
            else if (ex.Message.Contains("Unable to send to a recipient"))
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Grant data Created / Sanctioned Successfully....Error in mail sending!!!!!!!!!!!!!!')</script>");
                log.Info("Grant created Successfully,Error in mail sending!!!!!!!!!!!!!, of ID: " + TextBoxID.Text + " , Type: " + DropDownListTypeGrant.SelectedValue);


            }
            else if (ex.Message.Contains("Object reference not set to an instance of an obje"))
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Grant data Creaton Failed....Please contact admin')</script>");
                log.Error("Grant data Creaton Failed.....Please contact admin, id: " + TextBoxID.Text + " , Type: " + DropDownListTypeGrant.SelectedValue);


            }

            else

                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Grant data Creation failed!!!!!!!!!!!!')</script>");
            log.Error("Grant data Creaton Failed.... id: " + TextBoxID.Text + " , Type: " + DropDownListTypeGrant.SelectedValue);

        }
    }

    //SAve Incentive Details
    protected void BtnSaveOverheadDetails(object sender, EventArgs e)
    {

        if (!Page.IsValid)
        {
            return;
        }

        try
        {
            int result = 0;
            DataTable dtCurrentTableOverHead = (DataTable)ViewState["OverheadT"];
            GrantData[] OHD = null;
            GrantData journalbank = new GrantData();
            OHD = new GrantData[dtCurrentTableOverHead.Rows.Count];
            //Insert overhead

            int rowIndex3 = 0;
            if (dtCurrentTableOverHead != null)
            {
                if (dtCurrentTableOverHead.Rows.Count > 0)
                {
                    for (int i = 0; i < dtCurrentTableOverHead.Rows.Count; i++)
                    {
                        OHD[i] = new GrantData();

                        DropDownList OHSanctionEntryNumber = (DropDownList)grvoverhead.Rows[rowIndex3].Cells[0].FindControl("ddlSanctionEntryNoOH");
                        TextBox OHReceviedDate = (TextBox)grvoverhead.Rows[rowIndex3].Cells[1].FindControl("txtOverheaddate");
                        TextBox OHReceviedAmount = (TextBox)grvoverhead.Rows[rowIndex3].Cells[2].FindControl("txtOverheadAmount");
                        TextBox OHJVNumber = (TextBox)grvoverhead.Rows[rowIndex3].Cells[2].FindControl("txtJvNumber");
                        TextBox OHNarration = (TextBox)grvoverhead.Rows[rowIndex3].Cells[3].FindControl("txtoverheadComments");

                        if (dtCurrentTableOverHead.Rows.Count > 0)
                        {
                            journalbank.GID = TextBoxID.Text;
                            journalbank.GrantUnit = DropDownListGrUnit.SelectedValue;
                            journalbank.FinanceProjectStatus = DropDownList3.SelectedValue;
                            if (TextBox3.Text.Trim() != "")
                            {
                                journalbank.DateOfCompletion = Convert.ToDateTime(TextBox3.Text);
                            }
                            journalbank.Remarks = TextBox4.Text;
                            if (DropDownList2.SelectedValue != "select")
                            {
                                journalbank.ServiceTaxApplicable = DropDownList2.SelectedValue;
                            }
                            OHD[i].OHSanctionEntryNo = Convert.ToInt16(OHSanctionEntryNumber.Text.Trim());
                            OHD[i].OverheadTDate = Convert.ToDateTime(OHReceviedDate.Text.Trim());
                            OHD[i].OverheadTAmount =Math.Round(Convert.ToDouble(OHReceviedAmount.Text.Trim()),2);
                            OHD[i].JVNumber = OHJVNumber.Text.Trim();
                            OHD[i].OverheadNarration = OHNarration.Text.Trim();

                        }

                        rowIndex3++;
                    }
                }
                if (journalbank.GID != null)
                {
                    result = B.InsertOverheadDetails(journalbank, OHD);
                }
            }
            if (result == 1)
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Grant Overhead details saved successfully  of ID: " + TextBoxID.Text + "')</script>");
                log.Info("Grant Overhead details saved successfully, of ID: " + TextBoxID.Text);
            }
        }

        catch (Exception ex)
        {
            log.Error("Inside Catch Block Of Grant" + ex.Message + " UserID : " + Session["UserId"].ToString());

            log.Error(ex.StackTrace);


            if (ex.Message.Contains("The string was not recognized as a valid DateTime"))
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Date is not valid!!!!!!!!!!!!')</script>");

            }
            if (ex.Message.Contains("String was not recognized as a valid DateTime."))
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Date is not valid!!!!!!!!!!!!')</script>");

            }

            else if (ex.Message.Contains("Input string was not in a correct format"))
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Amount must be in numbers')</script>");
                log.Error("Error, of ID: " + TextBoxID.Text + " , Type: " + DropDownListTypeGrant.SelectedValue);

            }
            else if (ex.Message.Contains("There is already an open DataReader"))
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Grant data Creaton Failed)</script>");
                log.Info("Grant data Creation Saved..Upload failed, of ID: " + ex.Message + " " + TextBoxID.Text + " , Type: " + DropDownListTypeGrant.SelectedValue);

            }
            else if (ex.Message.Contains("Mailbox unavailable. The server response was: #5.1.0 Address rejecte"))
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Grant data Created / Sanctioned Successfully')</script>");
                log.Info("Grant created Successfully, of ID: " + TextBoxID.Text + " , Type: " + DropDownListTypeGrant.SelectedValue);


            }
            else if (ex.Message.Contains("Unable to send to a recipient"))
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Grant data Created / Sanctioned Successfully....Error in mail sending!!!!!!!!!!!!!!')</script>");
                log.Info("Grant created Successfully,Error in mail sending!!!!!!!!!!!!!, of ID: " + TextBoxID.Text + " , Type: " + DropDownListTypeGrant.SelectedValue);


            }
            else if (ex.Message.Contains("Object reference not set to an instance of an obje"))
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Grant data Creaton Failed....Please contact admin')</script>");
                log.Error("Grant data Creaton Failed.....Please contact admin, id: " + TextBoxID.Text + " , Type: " + DropDownListTypeGrant.SelectedValue);


            }

            else

                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Grant data Creation failed!!!!!!!!!!!!')</script>");
            log.Error("Grant data Creaton Failed.... id: " + TextBoxID.Text + " , Type: " + DropDownListTypeGrant.SelectedValue);

        }
    }
    protected void onchangeUnit(object sender, EventArgs e)
    {
        string a = DropDownListGrUnit.SelectedValue;
        SqlDataSourceTextBoxGrantAgency.SelectParameters.Clear();
        SqlDataSourceTextBoxGrantAgency.SelectParameters.Add("AgentType", a);
        SqlDataSourceTextBoxGrantAgency.SelectCommand = "SELECT  FundingAgencyId as Id,UPPER([FundingAgencyName]) as Name FROM [ProjectFundingAgency_M] where AgentType=@AgentType";
        SqlDataSourceTextBoxGrantAgency.DataBind();
        popGridagency.DataSourceID = "SqlDataSourceTextBoxGrantAgency";
        popGridagency.DataBind();

        txtagency.Text = "";
        txtagencycontact.Text = "";
        txtpan.Text = "";
        txtEmailId.Text = "";
        txtAddress.Text = "";
        txtstate.Text = "";
        txtcountry.Text = "";
        hdnAgencyId.Value = "";

    }


    //PopUp OP Amount
    protected void AddOPAmtClick(object sender, EventArgs e)
    {
        Button imgButton = (Button)sender;
        GridViewRow parentRow = (GridViewRow)imgButton.NamingContainer;
        int rowindex = parentRow.RowIndex;

        Label11.Text = rowindex.ToString();

        //DropDownList sanctionnumber = (DropDownList)gvIncentiveDetails.Rows[rowindex].Cells[0].FindControl("ddlSanctionEntryNo");
        //int sanctionentryno = Convert.ToInt16(sanctionnumber.SelectedValue);
        //Sanction.Text = sanctionentryno.ToString();

        Business b = new Business();
        string id = TextBoxID.Text;
        string unit = DropDownListGrUnit.SelectedValue;
        DataTable dy = b.SelectSanctionOPAmountDetails(id, unit, rowindex + 1);
        int value = rowindex + 1;
        if (dy.Rows.Count != 0)
        {
            SqlDataSource5.SelectParameters.Clear();
            SqlDataSource5.SelectParameters.Add("value", value.ToString());
            SqlDataSource5.SelectParameters.Add("unit", unit);
            SqlDataSource5.SelectParameters.Add("id", id);

            SqlDataSource5.SelectCommand = "select ROW_NUMBER() OVER (ORDER BY a.[ID]) AS Row, a.ID,Name ,b.SanctionEntryNo,b.OperatingItemId,b.Amount as Amount,'' as rowIndexParent,'' as rowIndexChild from OperatingItem_M a left outer join SanctionOPAmountDetails b  on a.ID=b.OperatingItemId and b.SanctionEntryNo=@value  and b.ProjectUnit=@unit  and b.ID=@id ";
            PanelAmount.Visible = true;
            popgridOPAmount.DataSourceID = "SqlDataSource5";
            SqlDataSource5.DataBind();
            popgridOPAmount.DataBind();
            popgridOPAmount.Visible = true;

            int rowMisc = 0;
            for (int i = 1; i <= popgridOPAmount.Rows.Count; i++)
            {
                TextBox TextPopupMisc = (TextBox)popgridOPAmount.Rows[rowMisc].Cells[3].FindControl("txtOPAmount");
                if (TextPopupMisc.Text == "0")
                {
                    TextPopupMisc.Text = "";
                }
                else if (TextPopupMisc.Text != "")
                {
                    TextPopupMisc.Text = ((decimal)(Convert.ToDouble(TextPopupMisc.Text))).ToString();
                }
             
                rowMisc++;
            }
            ModalPopupExtender ModalPopupExtenderMisc = (ModalPopupExtender)GridViewSanction.Rows[rowindex].FindControl("ModalPopupExtenderOPAmount");
            ModalPopupExtenderMisc.Show();
        }

        else
        {
        setModalWindowOPAmount(sender, e);
       
        ModalPopupExtender ModalPopupExtenderMisc = (ModalPopupExtender)GridViewSanction.Rows[rowindex].FindControl("ModalPopupExtenderOPAmount");
        ModalPopupExtenderMisc.Show();
        }
        //ModalPopupExtender ModalPopupExtenderMisc2 = (ModalPopupExtender)gvIncentiveDetails.Rows[0].FindControl("ModalPopupExtenderAmount");
        //ModalPopupExtenderMisc2.Hide();
        //ModalPopupExtender ModalPopupExtenderMisc = (ModalPopupExtender)gvIncentiveDetails.Rows[0].FindControl("ModalPopupExtenderAmount");
        //ModalPopupExtenderMisc.Show();

    }

    private void setModalWindowOPAmount(object sender, EventArgs e)
    {
        PanelOPAmount.Visible = true;
        popgridOPAmount.DataSourceID = "SqlDataSource5";
        SqlDataSource5.DataBind();
        popgridOPAmount.DataBind();
        popgridOPAmount.Visible = true;

    }

    //incentive amount break submit
    //protected void btnsubmitOPAmt(object sender, EventArgs e)
    //{
    //    ArrayList IncentiveSum = new ArrayList();
    //    DataRow dr = null;
    //    int rowIndexChild = 0, rowIndexParent = 0;
    //    double TotalCost = 0.0, TotalDisp = 0.0, Nul = 0.0;
    //    string AddCost = null, Investigator1 = null;
    //    TextBox cost = null;
    //    Label RowNumber = null;

    //    if (ViewState["temp_OPdt"] != null)
    //    {
    //        DataTable dtCurrentTable2 = (DataTable)ViewState["temp_OPdt"];
    //        for (int i = 0; i < popgridOPAmount.Rows.Count; i++)
    //        {

    //            GridViewRow row = popgridOPAmount.Rows[i];
    //            TextBox Amount = (TextBox)row.FindControl("txtOPAmount");
    //            if (Amount.Text != "")
    //            {
    //                RowNumber = (Label)popgridOPAmount.Rows[rowIndexChild].Cells[0].FindControl("LabelRow");
    //                cost = (TextBox)popgridOPAmount.Rows[rowIndexChild].Cells[3].FindControl("txtOPAmount");
    //                Label ID = (Label)popgridOPAmount.Rows[rowIndexChild].Cells[1].FindControl("OPID");

    //                AddCost = cost.Text.Trim();
    //                if (AddCost == "")
    //                {
    //                    AddCost = Nul.ToString();
    //                }
    //                Regex regex = new Regex("^([0-9]{1,3},([0-9]{3},)*[0-9]{3}|[0-9]+)(.[0-9][0-9]*$)?$");

    //                if (cost.Text != "" && !regex.IsMatch(cost.Text))
    //                {
    //                    ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert(' Amount must be in numbers!')</script>");
    //                    setModalWindowAmount(sender, e);

    //                    string rowVal2 = Request.Form["rowIndx"];
    //                    int rowIndex = Convert.ToInt32(rowVal2);
    //                    ModalPopupExtender ModalPopupExtenderMisc = (ModalPopupExtender)GridViewSanction.Rows[rowIndex].FindControl("ModalPopupExtenderOPAmount");
    //                    ModalPopupExtenderMisc.Show();
    //                    return;
    //                }
    //                if (cost.Text == "0" || cost.Text == "0.0")
    //                {
    //                    ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert(' Amount must be in numbers!')</script>");
    //                    setModalWindowOPAmount(sender, e);

    //                    string rowVal2 = Request.Form["rowIndx"];
    //                    int rowIndex = Convert.ToInt32(rowVal2);
    //                    ModalPopupExtender ModalPopupExtenderMisc = (ModalPopupExtender)GridViewSanction.Rows[rowIndex].FindControl("ModalPopupExtenderOPAmount");
    //                    ModalPopupExtenderMisc.Show();
    //                    // MiscPopupTextChanged(sender, e);
    //                    return;
    //                }
    //                IncentiveSum.Add(AddCost);
    //                dr = dtCurrentTable2.NewRow();
    //                dr["rowIndexParent"] = rowIndexParent + 1;
    //                dr["rowIndexChild"] = i + 1;
    //                dr["indexv"] = Label11.Text;
    //                dr["Row"] = RowNumber.Text;
    //                dr["ID"] = ID.Text;
    //                dr["Amount"] = cost.Text;
    //                // dr["SanctionEntryNo"] = Label12.Text;

    //                dtCurrentTable2.Rows.Add(dr);
    //            }
    //            rowIndexChild++;
    //        }
    //        ViewState["temp_OPdt"] = dtCurrentTable2;
    //    }



    //    else
    //    {
    //        DataTable dt = new DataTable();
    //        dt.Columns.Add(new DataColumn("rowIndexParent", typeof(string)));
    //        dt.Columns.Add(new DataColumn("rowIndexChild", typeof(string)));
    //        dt.Columns.Add(new DataColumn("indexv", typeof(string)));
    //        dt.Columns.Add(new DataColumn("Row", typeof(string)));
    //        dt.Columns.Add(new DataColumn("ID", typeof(string)));
    //        dt.Columns.Add(new DataColumn("Amount", typeof(string)));
    //        //dt.Columns.Add(new DataColumn("SanctionEntryNo", typeof(string)));

    //        for (int i = 0; i < popgridOPAmount.Rows.Count; i++)
    //        {

    //            GridViewRow row = popgridOPAmount.Rows[i];
    //            TextBox Amount = (TextBox)row.FindControl("txtOPAmount");
    //            if (Amount.Text != "")
    //            {
    //                RowNumber = (Label)popgridOPAmount.Rows[rowIndexChild].Cells[0].FindControl("LabelRow");
    //                cost = (TextBox)popgridOPAmount.Rows[rowIndexChild].Cells[3].FindControl("txtOPAmount");
    //                Label ID = (Label)popgridOPAmount.Rows[rowIndexChild].Cells[1].FindControl("OPID");

    //                AddCost = cost.Text.Trim();
    //                if (AddCost == "")
    //                {
    //                    AddCost = Nul.ToString();
    //                }
    //                Regex regex = new Regex("^([0-9]{1,3},([0-9]{3},)*[0-9]{3}|[0-9]+)(.[0-9][0-9]*$)?$");

    //                if (cost.Text != "" && !regex.IsMatch(cost.Text))
    //                {
    //                    ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert(' Amount must be in numbers!')</script>");
    //                    setModalWindowAmount(sender, e);

    //                    string rowVal2 = Request.Form["rowIndx"];
    //                    int rowIndex = Convert.ToInt32(rowVal2);
    //                    ModalPopupExtender ModalPopupExtenderMisc = (ModalPopupExtender)GridViewSanction.Rows[rowIndex].FindControl("ModalPopupExtenderOPAmount");
    //                    ModalPopupExtenderMisc.Show();
    //                    return;
    //                }
    //                if (cost.Text == "0" || cost.Text == "0.0")
    //                {
    //                    ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert(' Amount must be in numbers!')</script>");
    //                    setModalWindowAmount(sender, e);

    //                    string rowVal2 = Request.Form["rowIndx"];
    //                    int rowIndex = Convert.ToInt32(rowVal2);
    //                    ModalPopupExtender ModalPopupExtenderMisc = (ModalPopupExtender)GridViewSanction.Rows[rowIndex].FindControl("ModalPopupExtenderOPAmount");
    //                    ModalPopupExtenderMisc.Show();
    //                    // MiscPopupTextChanged(sender, e);
    //                    return;
    //                }
    //                IncentiveSum.Add(AddCost);
    //                dr = dt.NewRow();
    //                dr["rowIndexParent"] = rowIndexParent + 1;
    //                dr["rowIndexChild"] = i + 1;
    //                dr["indexv"] = Label11.Text;
    //                dr["Row"] = RowNumber.Text;
    //                dr["ID"] = ID.Text;
    //                dr["Amount"] = cost.Text;
    //                //dr["SanctionEntryNo"] = Sanction.Text;
    //                dt.Rows.Add(dr);
    //            }
    //            rowIndexChild++;
    //        }
    //        ViewState["temp_OPdt"] = dt;
    //    }


    //    Session["MiscRow" + rowIndexParent] = ViewState["temp_OPdt"];

    //    string rowV = Label11.Text;
    //    int index = Convert.ToInt32(rowV);

    //    for (int j = 0; j < IncentiveSum.Count; j++)
    //    {
    //        TotalCost = Convert.ToDouble(IncentiveSum[j]);
    //        double TotalCost1 = Convert.ToDouble(TotalCost);
    //        TotalDisp = TotalDisp + TotalCost1;
    //    }
    //    TextBox TotalText = (TextBox)GridViewSanction.Rows[index].Cells[3].FindControl("txtSanOpeAmt");
    //    TotalText.Text = TotalDisp.ToString();
    //    setModalWindowOPAmount(sender, e);

    //    DataTable dtCurrentTable1 = (DataTable)ViewState["temp_OPdt"];
    //    DataRow drCurrentRow1 = null;


    //    ViewState["temp_OPdt"] = dtCurrentTable1;
    //}

    protected void btnsubmitOPAmt(object sender, EventArgs e)
    {
        ArrayList IncentiveSum = new ArrayList();
        DataRow dr = null;
        int rowIndexChild = 0, rowIndexParent = 0;
        double TotalCost = 0.0, TotalDisp = 0.0, Nul = 0.0;
        string AddCost = null, Investigator1 = null;
        TextBox cost = null;
        Label RowNumber = null;
        string rowVal1 = Label11.Text;

        rowIndexParent = Convert.ToInt32(rowVal1);
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn("rowIndexParent", typeof(string)));
            dt.Columns.Add(new DataColumn("rowIndexChild", typeof(string)));
            dt.Columns.Add(new DataColumn("indexv", typeof(string)));
            dt.Columns.Add(new DataColumn("Row", typeof(string)));
            dt.Columns.Add(new DataColumn("ID", typeof(string)));
            dt.Columns.Add(new DataColumn("Amount", typeof(string)));
            //dt.Columns.Add(new DataColumn("SanctionEntryNo", typeof(string)));

            for (int i = 0; i < popgridOPAmount.Rows.Count; i++)
            {

                GridViewRow row = popgridOPAmount.Rows[i];
                TextBox Amount = (TextBox)row.FindControl("txtOPAmount");
                if (Amount.Text != "")
                {
                    RowNumber = (Label)popgridOPAmount.Rows[rowIndexChild].Cells[0].FindControl("LabelRow");
                    cost = (TextBox)popgridOPAmount.Rows[rowIndexChild].Cells[3].FindControl("txtOPAmount");
                    Label ID = (Label)popgridOPAmount.Rows[rowIndexChild].Cells[1].FindControl("OPID");

                    AddCost = cost.Text.Trim();
                    if (AddCost == "")
                    {
                        AddCost = Nul.ToString();
                    }
                    Regex regex = new Regex("^([0-9]{1,3},([0-9]{3},)*[0-9]{3}|[0-9]+)(.[0-9][0-9]*$)?$");

                    if (cost.Text != "" && !regex.IsMatch(cost.Text))
                    {
                        ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert(' Amount must be in numbers!')</script>");
                        setModalWindowAmount(sender, e);

                        string rowVal2 = Request.Form["rowIndx"];
                        int rowIndex = Convert.ToInt32(rowVal2);
                        ModalPopupExtender ModalPopupExtenderMisc = (ModalPopupExtender)GridViewSanction.Rows[rowIndex].FindControl("ModalPopupExtenderOPAmount");
                        ModalPopupExtenderMisc.Show();
                        return;
                    }
                    if (cost.Text == "0" || cost.Text == "0.0")
                    {
                        ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert(' Amount must be in numbers!')</script>");
                        setModalWindowAmount(sender, e);

                        string rowVal2 = Request.Form["rowIndx"];
                        int rowIndex = Convert.ToInt32(rowVal2);
                        ModalPopupExtender ModalPopupExtenderMisc = (ModalPopupExtender)GridViewSanction.Rows[rowIndex].FindControl("ModalPopupExtenderOPAmount");
                        ModalPopupExtenderMisc.Show();
                        // MiscPopupTextChanged(sender, e);
                        return;
                    }
                    IncentiveSum.Add(AddCost);
                    dr = dt.NewRow();
                    dr["rowIndexParent"] = rowIndexParent + 1;
                    dr["rowIndexChild"] = i + 1;
                    dr["indexv"] = Label11.Text;
                    dr["Row"] = RowNumber.Text;
                    dr["ID"] = ID.Text;
                    double amount = Math.Round(Convert.ToDouble(cost.Text),2);
                    dr["Amount"] = amount.ToString();
                    //dr["SanctionEntryNo"] = Sanction.Text;
                    dt.Rows.Add(dr);
                }
                rowIndexChild++;
            }
            ViewState["temp_OPdt"] = dt;



            Session["MiscRow" + rowIndexParent] = dt;

        string rowV = Label11.Text;
        int index = Convert.ToInt32(rowV);
        double value = 0.0;
        for (int j = 0; j < IncentiveSum.Count; j++)
        {
            TotalCost = Convert.ToDouble(IncentiveSum[j]);
            double TotalCost1 = Convert.ToDouble(TotalCost);
            TotalDisp = TotalDisp + TotalCost1;
             value = Math.Round(TotalDisp,2);
        }
        TextBox TotalText = (TextBox)GridViewSanction.Rows[index].Cells[3].FindControl("txtSanOpeAmt");
        TotalText.Text = value.ToString();
        setModalWindowOPAmount(sender, e);
        AddTotal(sender, e);
    }

    protected void AddTotal(object sender, EventArgs e)
    {
        //TextBox txt = (TextBox)sender;
        //GridViewRow gvRow = (GridViewRow)txt.Parent.Parent;
        //TextBox capitalamount = (TextBox)gvRow.FindControl("txtsancapitalAmount");
        //TextBox opeamount = (TextBox)gvRow.FindControl("txtSanOpeAmt");
        //TextBox totalamount = (TextBox)gvRow.FindControl("txtsantotalAmount");
        int row = Convert.ToInt16(Label11.Text);
        TextBox capitalamount = (TextBox)GridViewSanction.Rows[row].Cells[2].FindControl("txtsancapitalAmount");
        TextBox opeamount = (TextBox)GridViewSanction.Rows[row].Cells[3].FindControl("txtSanOpeAmt");
        TextBox totalamount = (TextBox)GridViewSanction.Rows[row].Cells[5].FindControl("txtsantotalAmount");
        Regex regex = new Regex("^([0-9]{1,3},([0-9]{3},)*[0-9]{3}|[0-9]+)(.[0-9][0-9]*$)?$");
        if (capitalamount.Text != "" && !regex.IsMatch(capitalamount.Text))
        {
            ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Capital Amount must be in numbers!')</script>");
            return;
        }


        if (Label11.Text != "")
        {
            //int row = Convert.ToInt16(Label11.Text);
            //TextBox capitalamount = (TextBox)GridViewSanction.Rows[row].Cells[2].FindControl("txtsancapitalAmount");
            //TextBox opeamount = (TextBox)GridViewSanction.Rows[row].Cells[3].FindControl("txtSanOpeAmt");
            //TextBox totalamount = (TextBox)GridViewSanction.Rows[row].Cells[5].FindControl("txtsantotalAmount");

            if (capitalamount.Text != "" && opeamount.Text != "")
            {
                totalamount.Text = ((Convert.ToDouble(capitalamount.Text)) + (Convert.ToDouble(opeamount.Text))).ToString();

            }
        }
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
    protected void BtnGenetratePdf(object sender, EventArgs e)
    {

        try
        {
            string id = null;
            id = TextBoxID.Text;
            string type = null;
            type = DropDownListTypeGrant.SelectedValue;
            string projectunit = null;
            projectunit = DropDownListGrUnit.SelectedValue;
            // pdfGenerate1(id);
            ProjectPDFHelper pdfhelper = new ProjectPDFHelper();
            pdfhelper.pdfGenerate(id, projectunit);
            //Journal_DataObject obj = new Journal_DataObject();
            //DataSet dz = obj.fnfindjournalAccount1(Pid);
            //  ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('PDF generated sucessfully')</script>");
            //ButtonSavepdf.Enabled = false;
         
            string Pid;
            //Pid = "00000177";
            string hospnum = Session["TempPid"].ToString();
            //string hospnum = Pid;

            Journal_DataObject obj = new Journal_DataObject();
            User p = new User();


            // TreatmentPlan p1 = new TreatmentPlan();
            p = obj.fnGetFilePathPdf(hospnum);
            //pa = p1.fnGetFilePathPdf1(hospnum);
            string path = null;
            path = p.path;


            //FileInfo myfile = new FileInfo(path);
            //try
            //{
            //    if (myfile.Exists)
            //    {

            //        log.Info("Evaluation Form Generated Successfully,  for Publiction ID: " + TextBoxPubId.Text + " of Type: " + DropDownListPublicationEntry.SelectedValue + " by " + Session["UserName"]);
            //        Response.ClearContent();
            //        Response.AddHeader("Content-Disposition", "attachment; filename=" + path);
            //        Response.AddHeader("Content-Length", myfile.Length.ToString());
            //        Response.ContentType = "application/vnd.ms-excel";
            //        Response.TransmitFile(myfile.FullName);
            //        Response.End();
            //    }
            //}
            //catch (Exception e2)
            //{
            //    log.Error("Evaluation Form generation failed,  for Publiction ID: " + TextBoxPubId.Text + " of Type: " + DropDownListPublicationEntry.SelectedValue + " by " + Session["UserName"]);
            //    log.Error("Error: , " + e2);
            //    ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert(Evaluation Form generation failed')</script>");
            //}
        }

        catch (Exception e2)
        {
            //log.Error("Evaluation Form generation failed,  for Publiction ID: " + TextBoxPubId.Text + " of Type: " + DropDownListPublicationEntry.SelectedValue + " by " + Session["UserName"]);
            log.Error("Error: , " + e2);
            ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert(Evaluation Form generation failed')</script>");
        }
    }

    //protected void TextBoxGrantDate_TextChanged(object sender, EventArgs e)
    //{
    //    if (String.IsNullOrEmpty(txtprojectactualdate.Text))
    //    {
    //        txtprojectactualdate.Text = TextBoxGrantDate.Text;
    //    }
       
    //}


   
}







