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

public partial class GrantEntry_GrantEntryView : System.Web.UI.Page
{
    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    string mainpath = ConfigurationManager.AppSettings["FolderPathProject"].ToString();
    Business B = new Business();
    Journal_DataObject JournalDataObj = new Journal_DataObject();
    JournalData JournalValueObj = new JournalData();
    public string pageID = "L42";
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            string keyword = Request.QueryString["Keyword"];
            if (keyword == "true" || keyword == "false" || keyword == "isPagesrch")
            {
                BtnBack.Visible = true;
                Session["TempPid"] = Request.QueryString["ProjectID"];
                Session["TempStatus"] = "";
                Session["TempTypeEntry"] = "";
                Session["ProjectUnit"] = Request.QueryString["ProjectUnit"];
                fnRecordExist(sender, e);
            }
            else
            {
                BtnBack.Visible = false;
                PanelAmount.Visible = false;
                PanelOPAmount.Visible = false;
            }
            setProjectdetails(sender, e);

        }


    }


    //Button Search Project click
    protected void ButtonSearchProjectOnClick(object sender, EventArgs e)
    {
        GridViewSearchGrant.Visible = true;
        GridViewSearchGrant.EditIndex = -1;
        dataBind();
    }


    private void dataBind()
    {
        if (EntryTypesearch.SelectedValue != "A")
        {
            if (PubIDSearch.Text == "" && TextBoxtiltleSearch.Text == "" && TextBoxEnteredBySearch.Text == "")
            {
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("ProjectType", EntryTypesearch.SelectedValue);

                SqlDataSource1.SelectCommand = " select p.ID, r.TypeName,p.ProjectUnit, UTN,Title,Description,CONVERT(DECIMAL(14,0),AppliedAmount) as AppliedAmount,q.StatusName,p.ProjectType from Project p,Status_Project_M q, ProjectType_M r where  p.ProjectType=r.TypeId and p.ProjectStatus=q.StatusId and  ProjectType=@ProjectType";
            }
            else if (PubIDSearch.Text != "" && TextBoxtiltleSearch.Text == "" && TextBoxEnteredBySearch.Text == "")
            {
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("ProjectType", EntryTypesearch.SelectedValue);
                SqlDataSource1.SelectParameters.Add("id", PubIDSearch.Text.Trim());

                SqlDataSource1.SelectCommand = " select p.ID, r.TypeName, p.ProjectUnit,UTN,Title,Description,CONVERT(DECIMAL(14,0),AppliedAmount) as AppliedAmount,q.StatusName,p.ProjectType from Project p,Status_Project_M q, ProjectType_M r where p.ProjectType=r.TypeId and p.ProjectStatus=q.StatusId and ProjectType=@ProjectType and ID like'%' + @id + '%'  ";
            }
            else if (PubIDSearch.Text == "" && TextBoxtiltleSearch.Text != "" && TextBoxEnteredBySearch.Text == "")
            {
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("ProjectType", EntryTypesearch.SelectedValue);
                SqlDataSource1.SelectParameters.Add("Title", TextBoxtiltleSearch.Text.Trim());
                SqlDataSource1.SelectCommand = " select p.ID, r.TypeName,p.ProjectUnit, UTN,Title,Description,CONVERT(DECIMAL(14,0),AppliedAmount) as AppliedAmount,q.StatusName,p.ProjectType from Project p,Status_Project_M q, ProjectType_M r where  p.ProjectType=r.TypeId and p.ProjectStatus=q.StatusId and  ProjectType=@ProjectType  and Title like'%' + @Title + '%'";
            }
            else if (PubIDSearch.Text == "" && TextBoxtiltleSearch.Text == "" && TextBoxEnteredBySearch.Text != "")
            {
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("ProjectType", EntryTypesearch.SelectedValue);
                SqlDataSource1.SelectParameters.Add("CreatedBy", TextBoxEnteredBySearch.Text.Trim());
                SqlDataSource1.SelectCommand = " select p.ID, r.TypeName,p.ProjectUnit, UTN,Title,Description,CONVERT(DECIMAL(14,0),AppliedAmount) as AppliedAmount,q.StatusName,p.ProjectType from Project p,Status_Project_M q, ProjectType_M r where  p.ProjectType=r.TypeId and p.ProjectStatus=q.StatusId and  ProjectType=@ProjectType and CreatedBy like'%' + @CreatedBy + '%'";
            }
            else
            {
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("ProjectType", EntryTypesearch.SelectedValue);
                SqlDataSource1.SelectParameters.Add("CreatedBy", TextBoxEnteredBySearch.Text.Trim());
                SqlDataSource1.SelectParameters.Add("Title", TextBoxtiltleSearch.Text.Trim());
                SqlDataSource1.SelectParameters.Add("id", PubIDSearch.Text.Trim());

                SqlDataSource1.SelectCommand = " select p.ID, r.TypeName, p.ProjectUnit,UTN,Title,Description,CONVERT(DECIMAL(14,0),AppliedAmount) as AppliedAmount,q.StatusName,p.ProjectType from Project p,Status_Project_M q, ProjectType_M r where  p.ProjectType=r.TypeId and p.ProjectStatus=q.StatusId and  ProjectType=@ProjectType and (ID like'%' + @id + '%' or @id='' )and (Title like'%' + @Title + '%' or @Title='') and (CreatedBy like'%' + @CreatedBy + '%' or @CreatedBy='')";

            }
            GridViewSearchGrant.DataBind();
            SqlDataSource1.DataBind();
        }
        else
        {
            if (PubIDSearch.Text == "" && TextBoxtiltleSearch.Text == "" && TextBoxEnteredBySearch.Text == "")
            {
                SqlDataSource1.SelectCommand = " select p.ID, r.TypeName,p.ProjectUnit, UTN,Title,Description,CONVERT(DECIMAL(14,0),AppliedAmount) as AppliedAmount,q.StatusName,p.ProjectType from Project p,Status_Project_M q, ProjectType_M r where  p.ProjectType=r.TypeId and p.ProjectStatus=q.StatusId ";
            }
            else if (PubIDSearch.Text != "" && TextBoxtiltleSearch.Text == "" && TextBoxEnteredBySearch.Text == "")
            {
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("id", PubIDSearch.Text.Trim());
                SqlDataSource1.SelectCommand = " select p.ID, r.TypeName,p.ProjectUnit, UTN,Title,Description,CONVERT(DECIMAL(14,0),AppliedAmount) as AppliedAmount,q.StatusName,p.ProjectType from Project p,Status_Project_M q, ProjectType_M r where  p.ProjectType=r.TypeId and p.ProjectStatus=q.StatusId  and ID like'%' + @id + '%'  ";
            }
            else if (PubIDSearch.Text == "" && TextBoxtiltleSearch.Text != "" && TextBoxEnteredBySearch.Text == "")
            {
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("Title", TextBoxtiltleSearch.Text.Trim());
                SqlDataSource1.SelectCommand = " select p.ID, r.TypeName,p.ProjectUnit, UTN,Title,Description,CONVERT(DECIMAL(14,0),AppliedAmount) as AppliedAmount,q.StatusName,p.ProjectType from Project p,Status_Project_M q, ProjectType_M r where  p.ProjectType=r.TypeId and p.ProjectStatus=q.StatusId  and Title like'%' + @Title + '%'";
            }
            else if (PubIDSearch.Text == "" && TextBoxtiltleSearch.Text == "" && TextBoxEnteredBySearch.Text != "")
            {
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("CreatedBy", TextBoxEnteredBySearch.Text.Trim());
                SqlDataSource1.SelectCommand = " select p.ID, r.TypeName,p.ProjectUnit, UTN,Title,Description,CONVERT(DECIMAL(14,0),AppliedAmount) as AppliedAmount,q.StatusName,p.ProjectType from Project p,Status_Project_M q, ProjectType_M r where  p.ProjectType=r.TypeId and p.ProjectStatus=q.StatusId  and CreatedBy like'%' + @CreatedBy + '%'";
            }
            else
            {
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("id", PubIDSearch.Text.Trim());
                SqlDataSource1.SelectParameters.Add("Title", TextBoxtiltleSearch.Text.Trim());
                SqlDataSource1.SelectParameters.Add("CreatedBy", TextBoxEnteredBySearch.Text.Trim());

                SqlDataSource1.SelectCommand = " select p.ID, r.TypeName,p.ProjectUnit, UTN,Title,Description,CONVERT(DECIMAL(14,0),AppliedAmount) as AppliedAmount,q.StatusName,p.ProjectType from Project p,Status_Project_M q, ProjectType_M r where  p.ProjectType=r.TypeId and p.ProjectStatus=q.StatusId  and (ID like'%' + @id + '%' or @id='' )and (Title like'%' + @Title + '%' or @Title='') and (CreatedBy like'%' + @CreatedBy + '%' or @CreatedBy='')";
            }
            GridViewSearchGrant.DataBind();
            SqlDataSource1.DataBind();
        }


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
        Session["TempPid"] = null;
        Session["TempTypeEntry"] = null;
        Session["TempStatus"] = null;
        Session["ProjectUnit"] = null;
        if (e.CommandName == "Edit")
        {

            GridViewRow rowSelect = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
            int rowindex = rowSelect.RowIndex;
            HiddenField TypeOfEntry = (HiddenField)GridViewSearchGrant.Rows[rowindex].Cells[8].FindControl("hiddenProjectType");
            typeEntry = TypeOfEntry.Value;

            pid = GridViewSearchGrant.Rows[rowindex].Cells[2].Text.Trim().ToString();
            Status = GridViewSearchGrant.Rows[rowindex].Cells[7].Text.Trim().ToString();
            string Unit = GridViewSearchGrant.Rows[rowindex].Cells[1].Text.Trim().ToString();
            Session["TempPid"] = pid;
            Session["TempTypeEntry"] = typeEntry;
            Session["TempStatus"] = Status;
            Session["ProjectUnit"] = Unit;
        }
    }



    public void GridViewSearchGrant_OnRowedit(Object sender, GridViewEditEventArgs e)
    {
        GridViewSearchGrant.EditIndex = e.NewEditIndex;
        fnRecordExist(sender, e);

    }


    private void cleardata()
    {
        //txtprojectactualdate.Text = "";
        TextBoxSanctionedAmountCapital.Text = "";
        TextBoxSanctionedAmountOperating.Text = "";
        TextBoxSanctionedamountTotal.Text = "";
        TextBoxProjectCommencementDate.Text = "";
        TextBoxProjectCloseDate.Text = "";
        TextBoxExtendedDate.Text = "";
        ddlauditrequired.SelectedValue = "0";
        txtInstitutionshare.Text = "";
        txtaccounthead.Text = "";
    }


    //Function to Select  Grant Data
    public void fnRecordExist(object sender, EventArgs e)
    {

        cleardata();
        string Pid = Session["TempPid"].ToString();
        string TypeEntry = Session["TempTypeEntry"].ToString();
        string CurStatus = Session["TempStatus"].ToString();
        string projectunit = Session["ProjectUnit"].ToString();
        //string UserId = Session["CreatedBy"].ToString();
        GrantData v = new GrantData();
        GrantData v1 = new GrantData();
        GrantData v2 = new GrantData();
        Business obj = new Business();
        GrantData m = new GrantData();
        GrantData n = new GrantData();
        User u = new User();
        v = obj.fnfindGrantid(Pid, projectunit);

   
        TextBoxID.Text = Pid;
        TextBoxUTN.Text = v.UTN;
        string UserId = v.CreatedBy;
        u = obj.findusername(UserId);
        TextBoxEnteredBy.Text = u.UserNamePrefix + " " + u.UserFirstName + " " + u.UserMiddleName + " " + u.UserLastName;
        setProjectdetails(sender, e);
        DropDownListTypeGrant.SelectedValue = v.GrantType;

        SqlDataSourcePrjStatus.SelectCommand = "Select * from Status_Project_M";
        DropDownListProjStatus.DataSourceID ="SqlDataSourcePrjStatus";
        DropDownListProjStatus.DataBind();
        DropDownListProjStatus.SelectedValue = v.Status;
        txtcontact.Text = v.Contact_No;
        DropDownListGrUnit.SelectedValue = v.GrantUnit;

        if (v.GrantSource != "")
        {
            SqlDataSourceDropDownListSourceGrant.SelectCommand = "select SourceId,SourceName from ProjectSource_M";
            DropDownListSourceGrant.DataSourceID = "SqlDataSourceDropDownListSourceGrant";
            DropDownListSourceGrant.DataBind();
            DropDownListSourceGrant.SelectedValue = v.GrantSource;
        }

        if (v.AppliedDate.ToShortDateString() != "01/01/0001")
        {
            TextBoxGrantDate.Text = v.AppliedDate.ToShortDateString();
        }
        if (v.GranAmount != 0)
        {
            TextBoxGrantAmt.Text = v.GranAmount.ToString();
        }
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

        txtRework.Text = v.Remarks;
        txtReject.Text = v.RejectFeedback;
        TextBoxRemarks.Text = v.CancelFeedback;
        DropDownListProjStatus.SelectedValue = v.Status;
        //if (v.ProjectActualDate.ToShortDateString() != "01/01/0001")
        //{
        //    txtprojectactualdate.Text = v.ProjectActualDate.ToShortDateString();
        //}
        if (v.SancType != "")
        {
            DropDownListSanType.SelectedValue = v.SancType;
        }
        if (v.DurationOfProject != 0)
        {
            txtProjectDuration.Text = v.DurationOfProject.ToString();
        }
        DropDownAgencyType.SelectedValue = v.TypeofAgencyGrant.ToString();
        DropDownSectorLevel.SelectedValue = v.FundingSectorLevelGrant.ToString();
        DSforgridview.SelectCommand = "select UploadPDFPath ,q.InfoTypeName as TypeName ,Remark ,AuditFrom,AuditTo, p.CreatedDate, p.CreatedBy as CreatedBy, m.FirstName as AddedBy, EntryNo,Unit_Id ,p.InfoTypeId from ProjectAuxillaryDetails p,Project_AuxInfoTypeM q, User_M m where   p.InfoTypeId=q.InfoTypeId and m.User_Id=p.CreatedBy  and ID='" + Pid + "' and ProjectUnit='" + projectunit+ "' and Deleted='N' order by EntryNo";
        DSforgridview.DataBind();
        GVViewFile.DataBind();
        PanelUploaddetails.Visible = true;
        PanelViewUplodedfiles.Visible = true;
        //setModalWindowAmount(sender, e);
        //setModalWindowOPAmount(sender, e);


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
                DropDownList DropdownMuNonMu = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("DropdownMuNonMu");
                TextBox EmployeeCode = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[1].FindControl("EmployeeCode");
                TextBox AuthorName = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[2].FindControl("AuthorName");
                TextBox InstNme = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[3].FindControl("InstitutionName");
                TextBox deptname = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[4].FindControl("DepartmentName");
                DropDownList DropdownStudentInstitutionName = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[3].FindControl("DropdownStudentInstitutionName");
                DropDownList DropdownStudentDepartmentName = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[4].FindControl("DropdownStudentDepartmentName");
                TextBox MailId = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[5].FindControl("MailId");
                DropDownList AuthorType = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[6].FindControl("AuthorType");
                DropDownList isLeadPI = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[2].FindControl("isLeadPI");
                DropDownList NationalType = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[7].FindControl("NationalType");
                DropDownList ContinentId = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[8].FindControl("ContinentId");
                HiddenField InstId = (HiddenField)Grid_AuthorEntry.Rows[rowIndex].Cells[9].FindControl("Institution");
                HiddenField deptId = (HiddenField)Grid_AuthorEntry.Rows[rowIndex].Cells[10].FindControl("Department");
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
                    //ImageButton1.Visible = true;
                    //EmployeeCodeBtnimg.Visible = false;
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
                    //ImageButton1.Visible = false;
                    //EmployeeCodeBtnimg.Enabled = false;
                    //EmployeeCodeBtnimg.Visible = true;
                    EmployeeCode.Enabled = true;
                    NationalType.Visible = false;
                    ContinentId.Visible = false;
                }

                MailId.Text = dtCurrentTable.Rows[i - 1]["MailId"].ToString();
                AuthorType.Text = dtCurrentTable.Rows[i - 1]["AuthorType"].ToString();
                isLeadPI.Text = dtCurrentTable.Rows[i - 1]["isLeadPI"].ToString();
                if (DropdownMuNonMu.Text == "N")
                {
                    
                    AuthorName.Enabled = true;
                    InstNme.Enabled = true;
                    deptname.Enabled = true;
                    MailId.Enabled = true;
                }
                else if (DropdownMuNonMu.Text == "E")
                {

                    AuthorName.Enabled = true;
                    InstNme.Enabled = true;
                    deptname.Enabled = true;
                    MailId.Enabled = true;
                }
                else if (DropdownMuNonMu.Text == "M")
                {
                    AuthorName.Enabled = false;
                    InstNme.Enabled = false;
                    deptname.Enabled = false;
                    MailId.Enabled = false;
                }
                else if (DropdownMuNonMu.Text == "S")
                {
                    
                    AuthorName.Enabled = true;
                    DropdownStudentInstitutionName.Enabled = true;
                    DropdownStudentInstitutionName.Enabled = true;
                    MailId.Enabled = true;
                }
                else
                {
                    MailId.Enabled = true;
                }
                if (AuthorType.Text == "P")
                {
                    isLeadPI.Enabled = true;
                }
                else
                {
                    isLeadPI.Enabled = false;
                }
                rowIndex++;

            }

            ViewState["CurrentTable"] = dtCurrentTable;
        }


        if (v.Status == "SAN" || v.Status == "CLO")
        {
            if (v.SancType == "CA")
            {
                PanelPercentage.Visible = true;
                DataTable da = obj.fnfinddistinctOrganizationforPercentage(Pid, projectunit);
                ViewState["CurrentTableIO"] = da;
                GridViewInterOrganization.DataSource = da;
                GridViewInterOrganization.DataBind();
                GridViewInterOrganization.Visible = true;

                int rowIndexIO = 0;

                DataTable dtCurrentTableIO = (DataTable)ViewState["CurrentTableIO"];
                DataRow drCurrentRowIO = null;
                if (dtCurrentTableIO.Rows.Count > 0)
                {
                    for (int p = 1; p <= dtCurrentTableIO.Rows.Count; p++)
                    {
                        DropDownList DropdownMuNonMu = (DropDownList)GridViewInterOrganization.Rows[rowIndexIO].Cells[3].FindControl("DropdownMuNonMuIO");
                        TextBox InstNme = (TextBox)GridViewInterOrganization.Rows[rowIndexIO].Cells[2].FindControl("InstitutionNameIO");
                        HiddenField InstId = (HiddenField)GridViewInterOrganization.Rows[rowIndexIO].Cells[2].FindControl("InstitutionIO");
                        TextBox Percent = (TextBox)GridViewInterOrganization.Rows[rowIndexIO].Cells[2].FindControl("PercentageIO");
                        //TextBox deptNme = (TextBox)GridViewInterInstitute.Rows[rowIndexIO].Cells[2].FindControl("DepartmentNameIO");
                        //HiddenField deptId = (HiddenField)GridViewInterInstitute.Rows[rowIndexIO].Cells[2].FindControl("DepartmentIO");
                        TextBox PercentAmount = (TextBox)GridViewInterOrganization.Rows[rowIndexIO].Cells[2].FindControl("PercentageIOAmount");
                        drCurrentRowIO = dtCurrentTableIO.NewRow();



                        DropdownMuNonMu.Text = dtCurrentTableIO.Rows[p - 1]["MUNonMU"].ToString();
                        if (DropdownMuNonMu.Text == "M")
                        {
                            string Instname = obj.getMaheInstitutionName(DropdownMuNonMu.Text);

                            InstNme.Visible = true;
                            InstNme.Text = Instname;
                            //InstNme.Text = dtCurrentTableIO.Rows[p - 1]["InstitutionName"].ToString();
                            InstId.Value = dtCurrentTableIO.Rows[p - 1]["Institution"].ToString();

                            //InstNme.Visible = true;

                            //InstNme.Text = dtCurrentTableIO.Rows[p - 1]["InstitutionName"].ToString();
                            //InstId.Value = dtCurrentTableIO.Rows[p - 1]["Institution"].ToString();
                            //deptNme.Text = "";
                            //deptId.Value = "";
                            //deptNme.Text = dtCurrentTableIO.Rows[p - 1]["DepartmentName"].ToString();
                            //deptId.Value = dtCurrentTableIO.Rows[p - 1]["Department"].ToString();
                        }

                        else if (DropdownMuNonMu.Text == "N")
                        {
                            InstNme.Visible = true;

                            InstNme.Text = dtCurrentTableIO.Rows[p - 1]["InstitutionName"].ToString();

                            InstId.Value = dtCurrentTableIO.Rows[p - 1]["Institution"].ToString();
                            //deptNme.Text = dtCurrentTableIO.Rows[p - 1]["DepartmentName"].ToString();
                            //deptId.Value = dtCurrentTableIO.Rows[p - 1]["Department"].ToString();
                        }
                        m = obj.getparcentagevalue(Pid, projectunit, InstId.Value, DropdownMuNonMu.Text);
                        if (m.percentageIO == 0)
                        {

                            m.percentageIO = 0;
                        }
                        Percent.Text = m.percentageIO.ToString();
                        if (m.percentageIOAmount != 0.0)
                        {
                            string percentageIOAmount = m.percentageIOAmount.ToString();
                            PercentAmount.Text = percentageIOAmount;
                        }
                        rowIndexIO++;
                        if (dtCurrentTableIO.Rows.Count == 1)
                        {
                            Percent.Text = "100";
                            GridViewInterOrganization.Enabled = false;
                        }
                        else
                        {
                            GridViewInterOrganization.Enabled = true;
                        }
                    }



                    ViewState["CurrentTableIO"] = dtCurrentTableIO;
                }

                DataTable db = obj.fnfinddistinctInstituteforPercentage(Pid, projectunit);
                ViewState["CurrentTableII"] = db;
                GridViewInterInstitute.DataSource = db;
                GridViewInterInstitute.DataBind();
                GridViewInterInstitute.Visible = true;

                int rowIndexII = 0;

                DataTable dtCurrentTableII = (DataTable)ViewState["CurrentTableII"];
                DataRow drCurrentRowII = null;
                if (dtCurrentTableII.Rows.Count > 0)
                {
                    for (int q = 1; q <= dtCurrentTableII.Rows.Count; q++)
                    {
                        DropDownList DropdownMuNonMu = (DropDownList)GridViewInterInstitute.Rows[rowIndexII].Cells[3].FindControl("DropdownMuNonMuII");
                        TextBox InstNme = (TextBox)GridViewInterInstitute.Rows[rowIndexII].Cells[2].FindControl("InstitutionNameII");
                        HiddenField InstId = (HiddenField)GridViewInterInstitute.Rows[rowIndexII].Cells[2].FindControl("InstitutionII");
                        TextBox deptNme = (TextBox)GridViewInterInstitute.Rows[rowIndexII].Cells[2].FindControl("DepartmentNameII");
                        HiddenField deptId = (HiddenField)GridViewInterInstitute.Rows[rowIndexII].Cells[2].FindControl("DepartmentII");
                        TextBox Percent = (TextBox)GridViewInterInstitute.Rows[rowIndexII].Cells[2].FindControl("PercentageII");

                        TextBox PercentIAmount = (TextBox)GridViewInterInstitute.Rows[rowIndexII].Cells[2].FindControl("PercentageIIAmount");
                        //TextBox deptNme = (TextBox)GridViewInterInstitute.Rows[rowIndexIO].Cells[2].FindControl("DepartmentNameIO");

                        drCurrentRowII = dtCurrentTableII.NewRow();

                        DropdownMuNonMu.Text = dtCurrentTableII.Rows[q - 1]["MUNonMU"].ToString();
                        if (DropdownMuNonMu.Text == "M")
                        {


                            InstNme.Visible = true;
                            InstNme.Text = dtCurrentTableII.Rows[q - 1]["InstitutionName"].ToString();
                            InstId.Value = dtCurrentTableII.Rows[q - 1]["Institution"].ToString();
                            deptNme.Text = dtCurrentTableII.Rows[q - 1]["DepartmentName"].ToString();
                            deptId.Value = dtCurrentTableII.Rows[q - 1]["Department"].ToString();

                        }

                        else if (DropdownMuNonMu.Text == "N")
                        {
                            InstNme.Visible = true;
                            InstNme.Text = dtCurrentTableII.Rows[q - 1]["InstitutionName"].ToString();
                            InstId.Value = dtCurrentTableII.Rows[q - 1]["Institution"].ToString();
                            deptNme.Text = dtCurrentTableII.Rows[q - 1]["DepartmentName"].ToString();
                            deptId.Value = dtCurrentTableII.Rows[q - 1]["Department"].ToString();
                        }
                        n = obj.getparcentagevaluefordept(Pid, projectunit, InstId.Value, deptId.Value);
                        if (n.percentageII == 0)
                        {
                            n.percentageII = 0;
                        }
                        Percent.Text = n.percentageII.ToString();
                        if (n.percentageIIAmount != 0.0)
                        {

                            PercentIAmount.Text = n.percentageIIAmount.ToString();
                        }
                        rowIndexII++;
                        if (dtCurrentTableII.Rows.Count == 1)
                        {
                            Percent.Text = "100";
                            GridViewInterInstitute.Enabled = false;
                        }
                        else
                        {
                            GridViewInterInstitute.Enabled = true;
                        }
                    }

                    ViewState["CurrentTableII"] = dtCurrentTableII;
                    int countpercentage = obj.CheckPercentageSharingDetails(TextBoxID.Text, DropDownListGrUnit.SelectedValue);
                    Session["countpercentage"] = countpercentage.ToString();
                    if (countpercentage > 0)
                    {
                        PanelPercentage.Visible = true;
                        PanelPercentage.Enabled = false;
                    }
                    else
                    {
                        PanelPercentage.Visible = false;
                    }
                }
            }
            else
            {
                PanelPercentage.Visible = false;
            }
        }
        else
        {
            PanelPercentage.Visible = false;
        }
        if (v.Status == "APP")
        {
            PanelKindetails.Visible = false;
            GrantSanction.Visible = false;
            PnlBank.Visible = false;
            PanelIncentive.Visible = false;
            PanelOverhead.Visible = false;
            PanelFinanceClosure.Visible = false;
            panelReowrkRemarks.Visible = false;
            panelReject.Visible = false;
            panelCanelRemarks.Visible = false;
            LabelSanType.Visible = false;
            DropDownListSanType.Visible = false;
            LabelkindDetails.Visible = false;
            TextBoxKindDetails.Visible = false;
            panelCanelRemarks.Visible = false;
            PanelAmount.Visible = false;
            PanelOPAmount.Visible = false;
            TextKindStartDate.Visible = false;
            TextKindclosedate.Visible = false;
            kindStartdate.Visible = false;
            KindClosedate.Visible = false;
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
            panelReject.Visible = true;
            panelCanelRemarks.Visible = false;
            LabelSanType.Visible = false;
            DropDownListSanType.Visible = false;
            LabelkindDetails.Visible = false;
            TextBoxKindDetails.Visible = false;
            panelCanelRemarks.Visible = false;
            TextKindStartDate.Visible = false;
            TextKindclosedate.Visible = false;
            kindStartdate.Visible = false;
            KindClosedate.Visible = false;
            PanelOPAmount.Visible = false;
        }
        else if (v.Status == "CLO")
        {
            if (v.SancType == "KI")
            {
                GrantSanction.Visible = false;
                PnlBank.Visible = false;
                PanelIncentive.Visible = false;
                PanelOverhead.Visible = false;
                PanelFinanceClosure.Visible = false;
                LabelSanType.Visible = true;
                DropDownListSanType.Visible = true;
                LabelkindDetails.Visible = true;
                TextBoxKindDetails.Visible = true;
                kindStartdate.Visible = true;
                TextKindStartDate.Visible = true;
                KindClosedate.Visible = true;
                TextKindclosedate.Visible = true;
            }
            else if (v.SancType == "CA")
            {
                GrantSanction.Visible = true;
                PnlBank.Visible = true;
                PanelIncentive.Visible = true;
                PanelOverhead.Visible = true;
                PanelFinanceClosure.Visible = true;
                LabelSanType.Visible = true;
                DropDownListSanType.Visible = true;
                LabelkindDetails.Visible = false;
                TextBoxKindDetails.Visible = false;
                kindStartdate.Visible = false;
                TextKindStartDate.Visible = false;
                KindClosedate.Visible = false;
                TextKindclosedate.Visible = false;
            }
            panelCanelRemarks.Visible = false;
        }
        else if (v.Status == "SUB")
        {
            if (v.SancType == "KI")
            {
                PanelKindetails.Visible = true;
                GrantSanction.Visible = false;
                PnlBank.Visible = false;
                PanelIncentive.Visible = false;
                PanelOverhead.Visible = false;
                PanelFinanceClosure.Visible = false;
                panelReowrkRemarks.Visible = false;
                panelReject.Visible = false;
                panelCanelRemarks.Visible = false;
                LabelSanType.Visible = true;
                DropDownListSanType.Visible = true;
                LabelkindDetails.Visible = true;
                TextBoxKindDetails.Visible = true;
                kindStartdate.Visible = true;
                TextKindStartDate.Visible = true;
                KindClosedate.Visible = true;
                TextKindclosedate.Visible = true;
                PanelOPAmount.Visible = false;
            }
            else if (v.SancType == "CA")
            {
                PanelKindetails.Visible = false;
                GrantSanction.Visible = true;
                PnlBank.Visible = false;
                PanelIncentive.Visible = false;
                PanelOverhead.Visible = false;
                PanelFinanceClosure.Visible = false;
                panelReowrkRemarks.Visible = false;
                panelReject.Visible = false;
                panelCanelRemarks.Visible = false;
                LabelSanType.Visible = true;
                DropDownListSanType.Visible = true;
                LabelkindDetails.Visible = false;
                TextBoxKindDetails.Visible = false;
                kindStartdate.Visible = false;
                TextKindStartDate.Visible = false;
                KindClosedate.Visible = false;
                TextKindclosedate.Visible = false;
            }
            PanelAmount.Visible = false;
        }
        else if (v.Status == "REW")
        {
            if (v.SancType == "KI")
            {
                PanelKindetails.Visible = true;
                GrantSanction.Visible = false;
                PnlBank.Visible = false;
                PanelIncentive.Visible = false;
                PanelOverhead.Visible = false;
                PanelFinanceClosure.Visible = false;
                panelReowrkRemarks.Visible = true;
                panelReject.Visible = false;
                panelCanelRemarks.Visible = false;
                LabelSanType.Visible = true;
                DropDownListSanType.Visible = true;
                LabelkindDetails.Visible = true;
                TextBoxKindDetails.Visible = true;
                kindStartdate.Visible = true;
                TextKindStartDate.Visible = true;
                KindClosedate.Visible = true;
                TextKindclosedate.Visible = true;
                PanelOPAmount.Visible = false;
            }
            else if (v.SancType == "CA")
            {
                PanelKindetails.Visible = false;
                GrantSanction.Visible = true;
                PnlBank.Visible = false;
                PanelIncentive.Visible = false;
                PanelOverhead.Visible = false;
                PanelFinanceClosure.Visible = false;
                panelReowrkRemarks.Visible = true;
                panelReject.Visible = false;
                panelCanelRemarks.Visible = false;
                DropDownListSanType.Visible = true;
                LabelSanType.Visible = true;
                LabelkindDetails.Visible = false;
                TextBoxKindDetails.Visible = false;
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
                PanelKindetails.Visible = true;
                GrantSanction.Visible = false;
                PnlBank.Visible = false;
                PanelIncentive.Visible = false;
                PanelOverhead.Visible = false;
                PanelFinanceClosure.Visible = false;
                panelReowrkRemarks.Visible = false;
                panelReject.Visible = false;
                panelCanelRemarks.Visible = false;
                LabelSanType.Visible = true;
                DropDownListSanType.Visible = true;
                LabelkindDetails.Visible = true;
                TextBoxKindDetails.Visible = true;
                kindStartdate.Visible = true;
                TextKindStartDate.Visible = true;
                KindClosedate.Visible = true;
                TextKindclosedate.Visible = true;
                PanelOPAmount.Visible = false;
            }
            else if (v.SancType == "CA")
            {
                PanelKindetails.Visible = false;
                GrantSanction.Visible = true;
                PnlBank.Visible = true;
                PanelIncentive.Visible = true;
                PanelOverhead.Visible = true;
                PanelFinanceClosure.Visible = false;
                panelReowrkRemarks.Visible = false;
                panelReject.Visible = false;
                panelCanelRemarks.Visible = false;
                LabelSanType.Visible = true;
                DropDownListSanType.Visible = true;
                LabelkindDetails.Visible = false;
                TextBoxKindDetails.Visible = false;
                kindStartdate.Visible = false;
                TextKindStartDate.Visible = false;
                KindClosedate.Visible = false;
                TextKindclosedate.Visible = false;
                PanelOPAmount.Visible = true;
                PanelAmount.Visible = true;
                setModalWindowAmount(sender, e);
                setModalWindowOPAmount(sender, e);
            }
          
            panelCanelRemarks.Visible = false;
        }
        else if (v.Status == "CAN")
        {
            if (v.SancType == "KI")
            {
                PanelKindetails.Visible = true;
                GrantSanction.Visible = false;
                PnlBank.Visible = false;
                PanelIncentive.Visible = false;
                PanelOverhead.Visible = false;
                PanelFinanceClosure.Visible = false;
                panelReowrkRemarks.Visible = false;
                panelReject.Visible = false;
                panelCanelRemarks.Visible = false;
                LabelSanType.Visible = true;
                DropDownListSanType.Visible = true;
                LabelkindDetails.Visible = true;
                TextBoxKindDetails.Visible = true;
                kindStartdate.Visible = true;
                TextKindStartDate.Visible = true;
                KindClosedate.Visible = true;
                TextKindclosedate.Visible = true;
            }
            else if (v.SancType == "CA")
            {
                PanelKindetails.Visible = false;
                GrantSanction.Visible = true;
                PnlBank.Visible = true;
                PanelIncentive.Visible = true;
                PanelOverhead.Visible = true;
                PanelFinanceClosure.Visible = false;
                panelReowrkRemarks.Visible = false;
                panelReject.Visible = false;
                panelCanelRemarks.Visible = false;
                LabelSanType.Visible = true;
                DropDownListSanType.Visible = true;
                LabelkindDetails.Visible = false;
                TextBoxKindDetails.Visible = false;
                kindStartdate.Visible = false;
                TextKindStartDate.Visible = false;
                KindClosedate.Visible = false;
                TextKindclosedate.Visible = false;
            }
            panelCanelRemarks.Visible = true;
            PanelAmount.Visible = false;
            PanelOPAmount.Visible = false;
        }

        if (v.Status == "SUB" || v.Status == "SAN" || v.Status == "REW" || v.Status == "CLO" || v.Status == "CAN")
        {
            if (v.SancType == "CA")
            {
                ////Sanction Details


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

                DataTable Sanctiondata = obj.SelectSanctionData(Pid, projectunit);

                if (Sanctiondata.Rows.Count != 0)
                {
                    
                    ViewState["Sanction"] = Sanctiondata;
                    GridViewSanction.DataSource = Sanctiondata;
                    GridViewSanction.DataBind();
                    GridViewSanction.Visible = true;

                    int rowIndex2 = 0;
                    DataTable table = (DataTable)ViewState["Sanction"];
                    DataRow drCurrentRow2 = null;
                    if (table != null)
                    {
                        for (int i = 1; i <= table.Rows.Count; i++)
                        {
                            TextBox sanctionNo = (TextBox)GridViewSanction.Rows[rowIndex2].Cells[0].FindControl("txtsanctionNo");
                            TextBox Sanctiondate = (TextBox)GridViewSanction.Rows[rowIndex2].Cells[1].FindControl("txtSanctiondate");
                            TextBox santotalAmount = (TextBox)GridViewSanction.Rows[rowIndex2].Cells[5].FindControl("txtsantotalAmount");
                            TextBox sancapitalAmount = (TextBox)GridViewSanction.Rows[rowIndex2].Cells[2].FindControl("txtsancapitalAmount");
                            TextBox SanOpeAmt = (TextBox)GridViewSanction.Rows[rowIndex2].Cells[3].FindControl("txtSanOpeAmt");
                            TextBox Narration = (TextBox)GridViewSanction.Rows[rowIndex2].Cells[6].FindControl("txtNarration");

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

                        }

                        ViewState["Sanction"] = table;
                    }
                    //setModalWindowAmount(sender, e);
                   // PanelAmount.Visible = false;
                    //PanelOPAmount.Visible = false;
                     //PanelAmount.Visible = true;
                    //PanelOPAmount.Visible = true;
                }
                else
                {
                    SanctionSetInitialRow();
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
                                Session["MiscRow" + i] = data1;
                            }
                        }
                    }
                }

                if (v.Status == "SAN" || v.Status == "REW" || v.Status == "CLO" || v.Status == "CAN")
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
                                    TextBox ReceivedBank = (TextBox)GridView_bank.Rows[rowIndex1].Cells[9].FindControl("ReceivedBankId");
                                    TextBox ReceivedBankName = (TextBox)GridView_bank.Rows[rowIndex1].Cells[9].FindControl("Receivedbank");
                                    TextBox CreditedBank = (TextBox)GridView_bank.Rows[rowIndex1].Cells[10].FindControl("CreditedBankId");
                                    TextBox CreditedBankName = (TextBox)GridView_bank.Rows[rowIndex1].Cells[10].FindControl("CreditedBank");
                                    TextBox ReceivedNarration = (TextBox)GridView_bank.Rows[rowIndex1].Cells[11].FindControl("ReceivedNarration");


                                    drCurrentRow = dtCurrentTable1.NewRow();

                                    if (Session["ProjectUnit"].ToString() == "MUIND")
                                    {
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
                                    ReceviedAmount.Text = dtCurrentTable1.Rows[i - 1]["ReceviedAmount"].ToString();
                                    if (dtCurrentTable1.Rows[i - 1]["ReceviedINR"].ToString() != "")
                                    {
                                        double amount = Convert.ToDouble((decimal)dtCurrentTable1.Rows[i - 1]["ReceviedINR"]);
                                        ReceviedINR.Text = amount.ToString();
                                    }
                                    TDS.Text = dtCurrentTable1.Rows[i - 1]["TDS"].ToString();
                                    ReferenceNo.Text = dtCurrentTable1.Rows[i - 1]["ReferenceNumber"].ToString();
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

                                    rowIndex1++;
                                }

                                ViewState["Bank"] = dtCurrentTable1;
                            }
                        }
                    }
                    else
                    {
                        SetInitialRowBank();
                    }

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


                            ViewState["IncentiveDetails"] = table;
                           
                        }
                       // setModalWindowAmount(sender,e);
                    }
                    else
                    {
                        SetIntialRowIncentive();
                        PanelAmount.Visible = false;

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
                                
                                DropDownList SanctionEntryNumber = (DropDownList)grvoverhead.Rows[rowIndex3].Cells[0].FindControl("ddlSanctionEntryNoOH");
                                TextBox txtOverheaddate = (TextBox)grvoverhead.Rows[rowIndex3].Cells[1].FindControl("txtOverheaddate");
                                TextBox txtOverheadAmount = (TextBox)grvoverhead.Rows[rowIndex3].Cells[2].FindControl("txtOverheadAmount");
                                TextBox txtoverheadComments = (TextBox)grvoverhead.Rows[rowIndex3].Cells[4].FindControl("txtoverheadComments");
                                TextBox txtJVNumber = (TextBox)grvoverhead.Rows[rowIndex3].Cells[3].FindControl("txtJVNumber");


                                drCurrentRow3 = table.NewRow(); 
                                DateTime date = Convert.ToDateTime(table.Rows[i - 1]["OverheadTDate"].ToString());
                                txtOverheaddate.Text = date.ToShortDateString();

                                double amount = Convert.ToDouble((decimal)(table.Rows[i - 1]["OverheadTAmount"]));
                                txtOverheadAmount.Text = amount.ToString();
                                txtJVNumber.Text = table.Rows[i - 1]["JVNumber"].ToString();
                                txtoverheadComments.Text = table.Rows[i - 1]["Narration"].ToString();
                                SanctionEntryNumber.SelectedValue = table.Rows[i - 1]["SanctionEntryNo"].ToString();
                                rowIndex3++;

                            }


                            ViewState["OverheadT"] = table;
                        }
                    }
                    else
                    {
                        SetInitialRowOverhead();
                    }

                }
            }
            else if (v.SancType == "KI")
            {

                PanelKindetails.Visible = true;

                LabelSanType.Visible = true;
                DropDownListSanType.Visible = true;
                LabelkindDetails.Visible = true;
                TextBoxKindDetails.Visible = true;
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
                        TextBox INREquivalent = (TextBox)GridViewkindDetails.Rows[rowIndex123].Cells[1].FindControl("INREquivalent");
                        TextBox Details = (TextBox)GridViewkindDetails.Rows[rowIndex123].Cells[2].FindControl("Details");

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
        dt1.Columns.Add(new DataColumn("ReceiedBankBName", typeof(string)));
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

        dr1["CreditedBankName"] = string.Empty;
        dr1["CreditedBank"] = string.Empty;
        dr1["ReceiedBankBName"] = string.Empty;
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

        HiddenField EmployeeCode = (HiddenField)Grid_AuthorEntry.Rows[0].Cells[0].FindControl("EmployeeCode");
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
        EmployeeCode.Value = Session["UserId"].ToString();

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

  
    //Function Delete uploaded files
   
    protected void GVViewFile_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        ImageButton EditButton = (ImageButton)e.Row.FindControl("BtnEdit");
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
            setModalWindowAmount(sender, e);
            SqlDataSourceAddAmt.SelectCommand = " Select a.ProjectUnit,a.ID,a.InvestigatorType, a.EntryNo as Row,InvestigatorName,a.Institution as Institution,a.Department as Department,Amount from Projectnvestigator a left outer join ProjectIncentivePayDetails b on a.ProjectUnit=b.ProjectUnit and a.ID=b.ID and a.EntryNo=b.EntryNo and  b.[LineNo]=" + value + " and a.ProjectUnit='" + unit + "' and a.ID='" + id + "' where a.ProjectUnit='" + unit + "' and a.ID='" + id + "'";
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
                else if (TextPopupMisc.Text != "")
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
            setModalWindowOPAmount(sender, e);
            if (DropDownListProjStatus.SelectedValue == "SAN")
            {
                setModalWindowAmount(sender, e);
            }
            SqlDataSource5.SelectCommand = "select ROW_NUMBER() OVER (ORDER BY a.[ID]) AS Row, a.ID,Name ,b.SanctionEntryNo,b.OperatingItemId,b.Amount as Amount,'' as rowIndexParent,'' as rowIndexChild from OperatingItem_M a left outer join SanctionOPAmountDetails b  on a.ID=b.OperatingItemId and b.SanctionEntryNo=" + value + " and b.ProjectUnit='" + unit + "' and b.ID='" + id + "'";
            PanelOPAmount.Visible = true;
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

    protected void BakButtonClick(object sender, EventArgs e)
    {
        string keyword = Request.QueryString["Keyword"];
        if (keyword == "true")
        {
            Response.Redirect("~/PublicationEntry/KeywordSearch.aspx");
        }
        else if (keyword == "false")
        {
            Response.Redirect("~/PublicationEntry/EmployeeDetailSearch.aspx");
        }

        else if (keyword == "isPagesrch")
        {
            //Response.Redirect("~/PublicationEntry/DomainandResearchareaSearch.aspx");
            Response.Redirect("~/PublicationEntry/DomainandResearchareaSearch.aspx?Keywordback=" + "back");
        }
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {



        ModalPopupExtenderoutcome.Show();

    }
    protected void setProjectdetails(object sender, EventArgs e)
    {
        if (TextBoxID.Text != "" && DropDownListGrUnit.SelectedValue != "")
        {
            string projectid = TextBoxID.Text.Trim();
            string projectunit = DropDownListGrUnit.SelectedValue.Trim();
            GridViewProject.DataSourceID = "SqlDataSourceProject";
            SqlDataSourceProject.SelectCommand = "select PublicationID,b.EntryName as  TypeOfEntry,TitleWorkItem  from Publication a,PublicationTypeEntry_M b where a.TypeOfEntry=b.TypeEntryId  and ProjectIDlist like '%" + (projectunit + projectid) + "%'";
            GridViewProject.DataBind();
            SqlDataSourceProject.DataBind();

            
            GridViewPatentOutcome.DataSourceID = "SqlDataSourcepatentoutcome";
            SqlDataSourcepatentoutcome.SelectCommand = "select ID ,Title  from Patent_Data a where  ProjectIDlist like '%" + (projectunit + projectid) + "%'";
            GridViewPatentOutcome.DataBind();
            SqlDataSourcepatentoutcome.DataBind();


            GridViewprojecout.DataSourceID = "SqlDataSourceprooutcome";
            SqlDataSourceprooutcome.SelectCommand = "SELECT OutcomeDate,Description  FROM ProjectOutcome where  ProjectID='" + projectid + "' and ProjectUnit='" + projectunit + "' ";
            GridViewprojecout.DataBind();
            SqlDataSourceprooutcome.DataBind();

        }
    }
    protected void GridViewProject_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            //GridView grid = (GridView)sender;

            //Label ProjectId = (Label)e.Row.FindControl("TextBoxProjectId");
            //CheckBox nextDay = (CheckBox)e.Row.FindControl("csSelect");
            //if (ProjectId.Text != "" && ProjectId.Text != null)
            //{
            //    nextDay.Visible = true;
            //}
            //else
            //{
            //    Page.Form.Attributes.Add("enctype", "multipart/form-data");
            //    nextDay.Visible = false;
            //}

        }
    }
}



