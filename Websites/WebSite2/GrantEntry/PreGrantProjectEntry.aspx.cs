using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.IO;
using System.Collections;
using System.Net.Mail;
using log4net;
using System.Data.OleDb;
using System.Text.RegularExpressions;

public partial class GrantEntry_PreGrantProjectEntry : System.Web.UI.Page
{
    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    public int validExcel = 1;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //set intial row of gidview author
            SetInitialRow();
            panAddAuthor.Visible = false;
            GrantSanction.Visible = false;
            Panel2.Visible = false;
            PanelUploaddetails.Visible = false;
            //panelReowrkRemarks.Visible = false;
            //set intial row of gidview author
            ButtonSearchProjectOnClick(sender, e);
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

    //Button Search Project click
    protected void ButtonSearchProjectOnClick(object sender, EventArgs e)
    {
        GridViewSearchGrant.Visible = true;
        GridViewSearchGrant.EditIndex = -1;
        GridViewSearchGrant.Visible = true;
        datasanBind();
    }

    private void datasanBind()
    {
        Business obj = new Business();
        string userid = Session["UserId"].ToString();
        string unit = Session["ProjectUnit"].ToString();
        GridViewSearchGrant.Visible = true;
        User a = new User();
        a = obj.GetPublicationIncharge(userid);
        if (EntryTypesearch.SelectedValue != "A")
        {
            if (PubIDSearch.Text == "" && TextBoxtiltleSearch.Text == "")
            {
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("ProjectType", EntryTypesearch.SelectedValue);

                SqlDataSource1.SelectCommand = " select p.ID, r.TypeName,p.ProjectUnit, UTN,Title,Description,CONVERT(DECIMAL(14,0),AppliedAmount) as AppliedAmount,q.StatusName,p.ProjectType from Project p,Status_Project_M q, ProjectType_M r where  p.ProjectType=r.TypeId and p.ProjectStatus=q.StatusId and  ProjectType=@ProjectType and  (StatusId='NEW' or StatusId='REW') and  p.IsUploadedProject='Y' ";
            }
            else if (PubIDSearch.Text != "" && TextBoxtiltleSearch.Text == "")
            {
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("ProjectType", EntryTypesearch.SelectedValue);
                SqlDataSource1.SelectParameters.Add("ID", PubIDSearch.Text.Trim());

                SqlDataSource1.SelectCommand = " select p.ID, r.TypeName, p.ProjectUnit,UTN,Title,Description,CONVERT(DECIMAL(14,0),AppliedAmount) as AppliedAmount,q.StatusName,p.ProjectType from Project p,Status_Project_M q, ProjectType_M r where p.ProjectType=r.TypeId and p.ProjectStatus=q.StatusId and ProjectType=@ProjectType and  ID like'%' + @ID + '%'  and (StatusId='NEW' or StatusId='REW') and p.IsUploadedProject='Y'";
            }
            else if (PubIDSearch.Text == "" && TextBoxtiltleSearch.Text != "")
            {
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("ProjectType", EntryTypesearch.SelectedValue);
                SqlDataSource1.SelectParameters.Add("Title", TextBoxtiltleSearch.Text.Trim());

                SqlDataSource1.SelectCommand = " select p.ID, r.TypeName,p.ProjectUnit, UTN,Title,Description,CONVERT(DECIMAL(14,0),AppliedAmount) as AppliedAmount,q.StatusName,p.ProjectType from Project p,Status_Project_M q, ProjectType_M r where  p.ProjectType=r.TypeId and p.ProjectStatus=q.StatusId and  ProjectType=@ProjectType and   Title like'%' + @Title + '%' and (StatusId='NEW' or StatusId='REW') and p.IsUploadedProject='Y'and p.IsUploadedProject='Y'";
            }
            else
            {
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("ProjectType", EntryTypesearch.SelectedValue);
                SqlDataSource1.SelectParameters.Add("Title", TextBoxtiltleSearch.Text.Trim());
                SqlDataSource1.SelectParameters.Add("ID", PubIDSearch.Text.Trim());

                SqlDataSource1.SelectCommand = " select p.ID, r.TypeName, p.ProjectUnit,UTN,Title,Description,CONVERT(DECIMAL(14,0),AppliedAmount) as AppliedAmount,q.StatusName,p.ProjectType from Project p,Status_Project_M q, ProjectType_M r where  p.ProjectType=r.TypeId and p.ProjectStatus=q.StatusId and  ProjectType=@ProjectType and  ID like'%' + @ID + '%' and Title like'%' + @Title + '%' and (StatusId='NEW' or StatusId='REW') and p.IsUploadedProject='Y' ";
            }
            GridViewSearchGrant.DataBind();
            SqlDataSource1.DataBind();
        }
        else
        {
            if (PubIDSearch.Text == "" && TextBoxtiltleSearch.Text == "")
            {
                SqlDataSource1.SelectCommand = "  select p.ID, r.TypeName,p.ProjectUnit, UTN,Title,Description,CONVERT(DECIMAL(14,0),AppliedAmount) as AppliedAmount,q.StatusName,p.ProjectType from Project p,Status_Project_M q, ProjectType_M r where  p.ProjectType=r.TypeId and p.ProjectStatus=q.StatusId   and (StatusId='NEW' or StatusId='REW') and p.IsUploadedProject='Y' ";
            }
            else if (PubIDSearch.Text != "" && TextBoxtiltleSearch.Text == "")
            {
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("ID", PubIDSearch.Text.Trim());

                SqlDataSource1.SelectCommand = " select p.ID, r.TypeName,p.ProjectUnit, UTN,Title,Description,CONVERT(DECIMAL(14,0),AppliedAmount) as AppliedAmount,q.StatusName,p.ProjectType from Project p,Status_Project_M q, ProjectType_M r where  p.ProjectType=r.TypeId and p.ProjectStatus=q.StatusId and  ID like'%' + @ID + '%'  and (StatusId='NEW' or StatusId='REW') and p.IsUploadedProject='Y'";
            }
            else if (PubIDSearch.Text == "" && TextBoxtiltleSearch.Text != "")
            {
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("Title", TextBoxtiltleSearch.Text.Trim());
                SqlDataSource1.SelectCommand = " select p.ID, r.TypeName,p.ProjectUnit, UTN,Title,Description,CONVERT(DECIMAL(14,0),AppliedAmount) as AppliedAmount,q.StatusName,p.ProjectType from Project p,Status_Project_M q, ProjectType_M r where  p.ProjectType=r.TypeId and p.ProjectStatus=q.StatusId and  Title like'%' + @Title + '%' and (StatusId='NEW' or StatusId='REW') and p.IsUploadedProject='Y'";
            }
            else
            {
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("Title", TextBoxtiltleSearch.Text.Trim());
                SqlDataSource1.SelectParameters.Add("ID", PubIDSearch.Text.Trim());

                SqlDataSource1.SelectCommand = " select p.ID, r.TypeName,p.ProjectUnit, UTN,Title,Description,CONVERT(DECIMAL(14,0),AppliedAmount) as AppliedAmount,q.StatusName,p.ProjectType from Project p,Status_Project_M q, ProjectType_M r where  p.ProjectType=r.TypeId and p.ProjectStatus=q.StatusId and  ID like'%' + @ID + '%' and Title like'%' + @Title + '%' and (StatusId='NEW' or StatusId='REW') and p.IsUploadedProject='Y' ";

                // SqlDataSource1.SelectCommand = "select j.Id as Id,Year,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,Comments from Journal_IF_Details j,Journal_M k where j.Id=k.Id and j.Id='" + txtJid.Text.Trim() + "'";
            }
            GridViewSearchGrant.DataBind();
            SqlDataSource1.DataBind();
        }
        if (Session["Role"].ToString() != "6" && Session["Role"].ToString() != "16")
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

    }

    protected void GridViewSearchGrant_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        datasanBind();
        GridViewSearchGrant.PageIndex = e.NewPageIndex;
        GridViewSearchGrant.DataBind();
    }

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
            //  typeEntry = GridViewSearch.Rows[rowindex].Cells[8].Text.ToString();
            pid = GridViewSearchGrant.Rows[rowindex].Cells[3].Text.Trim().ToString();
            Status = GridViewSearchGrant.Rows[rowindex].Cells[8].Text.Trim().ToString();
            string Unit = GridViewSearchGrant.Rows[rowindex].Cells[2].Text.Trim().ToString();
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
            HiddenField TypeOfEntry = (HiddenField)GridViewSearchGrant.Rows[rowindex].Cells[8].FindControl("hiddenProjectType");
            typeEntry = TypeOfEntry.Value;
            pid = GridViewSearchGrant.Rows[rowindex].Cells[2].Text.Trim().ToString();
            Status = GridViewSearchGrant.Rows[rowindex].Cells[10].Text.Trim().ToString();
            Session["TempPid"] = pid;
            Session["TempTypeEntry"] = typeEntry;
            Session["TempStatus"] = Status;


        }
    }

    protected void GridViewSearchGrant_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        ImageButton EditButton = (ImageButton)e.Row.FindControl("BtnEdit1");
    }

    public void GridViewSearchGrant_OnRowedit(Object sender, GridViewEditEventArgs e)
    {

        GridViewSearchGrant.EditIndex = e.NewEditIndex;
        fnRecordExist(sender, e);

    }
    private void cleardata()
    {

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
        //txtRework.Text = "";

    }
    private void fnRecordExist(object sender, GridViewEditEventArgs e)
    {
        cleardata();
        string Pid = Session["TempPid"].ToString();
        string TypeEntry = Session["TempTypeEntry"].ToString();
        string CurStatus = Session["TempStatus"].ToString();
        string projectunit1 = Session["ProjectUnit"].ToString();

        GrantData v = new GrantData();
        GrantData v1 = new GrantData();
        GrantData v2 = new GrantData();
        Business obj = new Business();

        //BtnRework.Enabled = true;
        BtnSanction.Enabled = true;
        v = obj.fnfindGrantid(Pid, projectunit1);


        DropDownListTypeGrant.SelectedValue = TypeEntry;
        if (v.ERFRelated != "")
        {
            DropDownListerfRelated.SelectedValue = v.ERFRelated;
        }
        TextBoxID.Text = Pid;
        TextBoxUTN.Text = v.UTN;
        TextBoxTitle.Text = v.Title;
        TextBoxAdComments.Text = v.AddtionalComments;
        TextBoxDescription.Text = v.Description;
        TextBoxDescription.Visible = true;
        TextBoxDescription.Enabled = true;
        TextBoxAdComments.Enabled = true;
        string agency = obj.GetAgencyName(v.GrantingAgency);

        txtagency.Text = agency;
        DropDownListGrUnit.SelectedValue = v.GrantUnit;
        if (v.GrantSource != "")
        {
            DropDownListSourceGrant.SelectedValue = v.GrantSource;
        }
      
        DropDownListSourceGrant.Enabled = false;
        DropDownListTypeGrant.Enabled = false;
        txtcontact.Text = v.Contact_No;

        TextBoxTitle.Enabled = false;
        txtagency.Enabled = false;
        DropDownListGrUnit.Enabled = false;
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
        SqlDataSourcePrjStatus.SelectCommand = "Select * from Status_Project_M";
        DropDownListProjStatus.DataSourceID = "SqlDataSourcePrjStatus";
        DropDownListProjStatus.DataBind();
        DropDownListProjStatus.SelectedValue = v.Status;
        if (v.Status == "NEW")
        {

            SqlDataSourcePrjStatus.SelectCommand = "select StatusId,StatusName from Status_Project_M where StatusId='NEW' or StatusId='SUB'   ";
            DropDownListProjStatus.SelectedValue = v.Status;
        }
        if (v.Status == "REW")
        {

            SqlDataSourcePrjStatus.SelectCommand = "select StatusId,StatusName from Status_Project_M where StatusId='NEW' or StatusId='SUB'or StatusId='REW' ";
            DropDownListProjStatus.SelectedValue = v.Status;
           
           
          
        }
        DropDownListProjStatus.Enabled=false;
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

        if (v.AppliedDate.ToShortDateString() != "01/01/0001")
        {
            TextBoxGrantDate.Text = v.AppliedDate.ToShortDateString();
        }
        TextBoxGrantDate.Enabled = false;
        //if (v.ProjectActualDate.ToShortDateString() != "01/01/0001")
        //{
        //    txtprojectactualdate.Text = v.ProjectActualDate.ToShortDateString();
        //}
        if (v.DurationOfProject != 0)
        {
            txtProjectDuration.Text = v.DurationOfProject.ToString();
        }
        if (v.SanctionOrderDate.ToShortDateString() != "01/01/0001")
        {
            Textsanctionorderdate.Text = v.SanctionOrderDate.ToShortDateString();
        }
        txtEmailId.Text = v.AgencyEmailId;

        txtagencycontact.Text = v.AgencyContact;
        DropDownAgencyType.SelectedValue = v.TypeofAgencyGrant.ToString();
        DropDownSectorLevel.SelectedValue = v.FundingSectorLevelGrant.ToString();
        PanelUploaddetails.Visible = true;
        PanelViewUplodedfiles.Visible = true;
        DSforgridview.SelectParameters.Clear();
        DSforgridview.SelectParameters.Add("ID", Pid);
        DSforgridview.SelectParameters.Add("projectunit", projectunit1);

        DSforgridview.SelectCommand = "select UploadPDFPath ,q.InfoTypeName as TypeName ,Remark ,AuditFrom,AuditTo, p.CreatedDate, p.CreatedBy as CreatedBy, m.FirstName as AddedBy, EntryNo,Unit_Id ,p.InfoTypeId from ProjectAuxillaryDetails p,Project_AuxInfoTypeM q, User_M m where  p.InfoTypeId=q.InfoTypeId and m.User_Id=p.CreatedBy  and ID=@ID and ProjectUnit=@projectunit and Deleted='N' order by EntryNo";
        DSforgridview.DataBind();
        GVViewFile.DataBind();
        GVViewFile.Visible = true;


        DataTable dy = obj.fnfindGrantInvestigatorDetails(Pid, projectunit1);
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



                drCurrentRow = dtCurrentTable.NewRow();

                DropdownMuNonMu.Text = dtCurrentTable.Rows[i - 1]["DropdownMuNonMu"].ToString();
                EmployeeCode.Text= dtCurrentTable.Rows[i - 1]["EmployeeCode"].ToString();
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
                //DropdownMuNonMu1.Enabled = false;
                //EmployeeCodeBtnimg1.Enabled = false;

                MailId.Text = dtCurrentTable.Rows[i - 1]["MailId"].ToString();
                AuthorType.Text = dtCurrentTable.Rows[i - 1]["AuthorType"].ToString();
                isLeadPI.Text = dtCurrentTable.Rows[i - 1]["isLeadPI"].ToString();
                //isCorrAuth.Text = dtCurrentTable.Rows[i - 1]["isCorrAuth"].ToString();

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

        //Sanction Details
        DataTable Sanctiondata = obj.SelectSanctionData(Pid, projectunit1);

        if (Sanctiondata.Rows.Count != 0)
        {
            ViewState["Sanction"] = Sanctiondata;
            GridViewSanction.DataSource = Sanctiondata;
            GridViewSanction.DataBind();
            GridViewSanction.Visible = true;

            int rowIndex2 = 0;

            DataTable table = (DataTable)ViewState["Sanctiondata"];
            DataRow drCurrentRow2 = null;

            if (table != null)
            {
                for (int i = 1; i <= table.Rows.Count; i++)
                {
                    TextBox sanctionNo = (TextBox)GridViewSanction.Rows[rowIndex].Cells[1].FindControl("txtsanctionNo");
                    TextBox Sanctiondate = (TextBox)GridViewSanction.Rows[rowIndex].Cells[2].FindControl("txtSanctiondate");
                    TextBox santotalAmount = (TextBox)GridViewSanction.Rows[rowIndex].Cells[3].FindControl("txtsantotalAmount");
                    TextBox sancapitalAmount = (TextBox)GridViewSanction.Rows[rowIndex].Cells[4].FindControl("txtsancapitalAmount");
                    TextBox SanOpeAmt = (TextBox)GridViewSanction.Rows[rowIndex].Cells[5].FindControl("txtSanOpeAmt");
                    TextBox Narration = (TextBox)GridViewSanction.Rows[rowIndex2].Cells[5].FindControl("txtNarration");

                    drCurrentRow2 = table.NewRow();
                    sanctionNo.Text = table.Rows[i - 1]["SanctionNumber"].ToString();

                    DateTime date = Convert.ToDateTime(table.Rows[i - 1]["SanctionDate"].ToString());
                    Sanctiondate.Text = date.ToShortDateString();

                    santotalAmount.Text = table.Rows[i - 1]["SanctionTotalAmount"].ToString();
                    sancapitalAmount.Text = table.Rows[i - 1]["SanctionCapitalAmount"].ToString();
                    SanOpeAmt.Text = table.Rows[i - 1]["SanctionOperatingAmount"].ToString();
                    Narration.Text = table.Rows[i - 1]["Narration"].ToString();
                    rowIndex2++;
                }

                ViewState["Sanction"] = table;
            }
        }
        else
        {
            SanctionSetInitialRow();
        }



        if (v.Status == "SUB")
        {
            SqlDataSourcePrjStatus.SelectCommand = "select StatusId,StatusName from Status_Project_M ";
            DropDownListProjStatus.SelectedValue = v.Status;

        }

        if (v.SancType != "")
        {
            DropDownListSanType.SelectedValue = v.SancType;
            DropDownListSanType.Enabled = false;

            if (v.SancType == "CA")
            {
                string projectunit = v.GrantUnit;
                v1 = obj.fnfindGrantidSanctionDetails(Pid, projectunit);

                GrantSanction.Visible = true;
                TextKindStartDate.Visible = false;
                TextKindclosedate.Visible = false;
                kindStartdate.Visible = false;
                KindClosedate.Visible = false;
                PanelKindetails.Visible = false;
                LabelSanType.Visible = false;
                DropDownListSanType.Visible = false;

                LabelkindDetails.Visible = false;
                TextBoxKindDetails.Visible = false;
                TextBoxKindDetails.Text = "";

                // TextBoxSanctionnNumber.Text = v1.SanctionNumber;
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
                //if (v1.SanctionDate.ToShortDateString() != "01/01/0001")
                //{
                //    TextBoxSanctionDate.Text = v1.SanctionDate.ToShortDateString();
                //}
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

                //if (v1.DateOfCompletion.ToShortDateString() != "01/01/0001")
                //{
                //    TextBox3.Text = v1.DateOfCompletion.ToShortDateString();
                //}
                //TextBox4.Text = v1.Remarks;
                //DropDownList3.SelectedValue = v1.FinanceProjectStatus;


                //DropDownList2.SelectedValue = v1.ServiceTaxApplicable;
                //if (DropDownList3.SelectedValue == "CLO")
                //{
                //    SqlDataSourcePrjStatus.SelectCommand = "select StatusId,StatusName from Status_Project_M where StatusId='CLO' or StatusId='SAN' and  StatusId!='CAN'";
                //    DropDownListProjStatus.SelectedValue = "SAN";
                //    GrantSanction.Enabled = false;
                //    // TextBoxDescription.Enabled = false;
                //    //TextBoxAdComments.Enabled = false;
                //    TextBoxGrantDate.Enabled = false;
                //    TextBoxGrantAmt.Enabled = false;
                //    DropDownListerfRelated.Enabled = false;
                //    PanelUploaddetails.Enabled = false;
                //}
                //else
                //{
                //    PanelUploaddetails.Enabled = true;
                //}
                //else
                //{
                //    GrantSanction.Enabled = true;
                //    GrantSanction.Enabled = true;
                //    TextBoxDescription.Enabled = true;
                //    TextBoxAdComments.Enabled = true;
                //    TextBoxGrantDate.Enabled = true;
                //    TextBoxGrantAmt.Enabled = true;
                //}
                //if (Session["Role"].ToString() == "6")
                //{
                //    if (v1.FinanceProjectStatus == "CLO")
                //    {
                //        Panel2.Enabled = false;
                //        Panel3.Enabled = false;
                //        Panel4.Enabled = false;
                //        Panel5.Enabled = false;
                //        PanelUploaddetails.Enabled = false;
                //    }
                //}

                ViewState["SancKindCurrentTable"] = null;

                //TextBoxRecievedDate.Text = "";
                //TextBoxReceivedAmount.Text = "";
                //TextBoxINREquivalent.Text = "";
                //TextBoxRecievedFrom.Text = "";
                //Panel2.Visible = true;
                //Panel3.Visible = true;
                //Panel4.Visible = true;
                //Panel5.Visible = true;
            }
            else if (v.SancType == "KI")
            {
                GrantSanction.Visible = false;
                Panel2.Visible = false;
                PanelKindetails.Visible = true;
                kindStartdate.Visible = true;
                TextKindStartDate.Visible = true;
                KindClosedate.Visible = true;
                TextKindclosedate.Visible = true;

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
                DataTable dy1 = obj.fnfindGrantSanKindDetails(Pid, projectunit1);
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

                //TextBoxSanctionnNumber.Text = "";
                TextBoxSanctionedAmountCapital.Text = "";
                TextBoxSanctionedAmountOperating.Text = "";
                TextBoxSanctionedamountTotal.Text = "";
                // TextBoxSanctionDate.Text = "";
                TextBoxProjectCommencementDate.Text = "";
                TextBoxProjectCloseDate.Text = "";
                TextBoxExtendedDate.Text = "";
                PanelKindetails.Visible = true;

            }
            else
            {
                GrantSanction.Visible = false;
                LabelSanType.Visible = false;
                DropDownListSanType.Visible = false;
                PanelKindetails.Visible = false;
                // TextBoxSanctionnNumber.Text = "";
                TextBoxSanctionedAmountCapital.Text = "";
                TextBoxSanctionedAmountOperating.Text = "";
                TextBoxSanctionedamountTotal.Text = "";
                //TextBoxSanctionDate.Text = "";
                TextBoxProjectCommencementDate.Text = "";
                TextBoxProjectCloseDate.Text = "";
                TextBoxExtendedDate.Text = "";

            }
        }
        //panelReowrkRemarks.Visible = true;
        //BtnRework.Enabled = true;
        BtnSanction.Enabled = true;
        PanelUploaddetails.Visible = true;
        if (DropDownListProjStatus.SelectedValue == "REW")
        {
            panelReowrkRemarks.Visible = true;
            txtRework.Enabled = false;
            txtRework.Text = v.Remarks;
        }
        else
        {
            txtRework.Text = "";
            panelReowrkRemarks.Visible = false;
        }

    }

    protected void GVViewFile_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        ImageButton EditButton = (ImageButton)e.Row.FindControl("BtnEdit");
    }
    protected void OnRowDataBound1(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList DropdownMuNonMu = (e.Row.FindControl("DropdownMuNonMu") as DropDownList);
            SqlDataSourceAuthorType.SelectCommand = "SELECT Id,DisplayName FROM [Author_Type_M] where (Id = 'M') or (Id = 'S') or (Id = 'N') or (Id = 'O')  or (Id = 'E')";

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


    //Initialize row for sanction details
    private void SanctionSetInitialRow()
    {

        DataTable dt1 = new DataTable();
        DataRow dr1 = null;

        dt1.Columns.Add(new DataColumn("SanctionNumber", typeof(string)));
        dt1.Columns.Add(new DataColumn("SanctionDate", typeof(string)));
        dt1.Columns.Add(new DataColumn("SanctionTotalAmount", typeof(string)));
        dt1.Columns.Add(new DataColumn("SanctionCapitalAmount", typeof(string)));
        dt1.Columns.Add(new DataColumn("SanctionOperatingAmount", typeof(string)));

        dr1 = dt1.NewRow();


        dr1["SanctionNumber"] = string.Empty;
        dr1["SanctionDate"] = string.Empty;
        dr1["SanctionTotalAmount"] = string.Empty;
        dr1["SanctionCapitalAmount"] = string.Empty;
        dr1["SanctionOperatingAmount"] = string.Empty;


        dt1.Rows.Add(dr1);

        ViewState["Sanction"] = dt1;
        GridViewSanction.DataSource = dt1;
        GridViewSanction.DataBind();

        TextBox sanctionNo = (TextBox)GridViewSanction.Rows[0].Cells[1].FindControl("txtsanctionNo");
        TextBox Sanctiondate = (TextBox)GridViewSanction.Rows[0].Cells[2].FindControl("txtSanctiondate");
        TextBox santotalAmount = (TextBox)GridViewSanction.Rows[0].Cells[3].FindControl("txtsantotalAmount");
        TextBox sancapitalAmount = (TextBox)GridViewSanction.Rows[0].Cells[3].FindControl("txtsancapitalAmount");
        TextBox SanOpeAmt = (TextBox)GridViewSanction.Rows[0].Cells[4].FindControl("txtSanOpeAmt");

    }


  
    protected void BtnSanction_Click(object sender, EventArgs e)
    {

        if (!Page.IsValid)
        {
            return;
        }
        string AppendInstitutionNamess = null;
        int countCorrAuth = 0;
        int countAuthType = 0;
      
        int countLeadPI = 0;
        try
        {

            Business b = new Business();

            ArrayList listIndexAgency = new ArrayList();
            Business B = new Business();
            GrantData j = new GrantData();

            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
            GrantData[] JD = new GrantData[dtCurrentTable.Rows.Count];

            GrantData[] JD1 = null;

            string GId = TextBoxID.Text.Trim();
            string UTN = TextBoxUTN.Text.Trim();
            string title = TextBoxTitle.Text.Trim();
            string dec = TextBoxDescription.Text.Trim();
            string granttagency = txtagency.Text;
            string Contact_No = txtcontact.Text;

            string GUnit = DropDownListGrUnit.SelectedValue;
            if (TextBoxGrantDate.Text != "")
            {
                string Applieddate = TextBoxGrantDate.Text.Trim();
            }

            string GAmt = TextBoxGrantAmt.Text.Trim();
            if (TextBoxGrantAmt.Text != "")
            {
                Regex regex1 = new Regex("^([0-9]{1,3},([0-9]{3},)*[0-9]{3}|[0-9]+)(.[0-9][0-9]*$)?$");
                if (TextBoxGrantAmt.Text != "" && !regex1.IsMatch(TextBoxGrantAmt.Text))
                {
                    ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Grant Amount must be in numbers!')</script>");

                    TextBoxGrantAmt.Text = "";
                    return;
                }
            }

            string GSource = DropDownListSourceGrant.SelectedValue;
            string GType = DropDownListTypeGrant.SelectedValue;

            string Status = DropDownListProjStatus.SelectedValue;

            string inst1 = Session["InstituteId"].ToString();
            if (TextBoxGrantDate.Text != "")
            {
                j.AppliedDate = Convert.ToDateTime(TextBoxGrantDate.Text.Trim());
            }
            string month = ((System.DateTime)j.AppliedDate).ToString("MMM");
            int year = j.AppliedDate.Year;
            j.ERFRelated = DropDownListerfRelated.SelectedValue;

            if (TextBoxGrantDate.Text != "")
            {
                string cuttOffDate = ConfigurationManager.AppSettings["CutOffDate"];
                DateTime cutoff = Convert.ToDateTime(cuttOffDate);
                int resultdate = DateTime.Compare(j.AppliedDate, cutoff);
                if (resultdate < 0)
                {
                    ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Applied Date  must be greater than" + cutoff + "')</script>");
                    TextBoxGrantDate.Text = "";
                    return;
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please enter a applied date')</script>");
                return;
            }
            if (TextBoxTitle.Text == "")
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please enter title of the project')</script>");
                return;
            }

            if (txtagency.Text == "")
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please enter project agency')</script>");
                return;
            }
            int CountSancInfoTp = 0;
            if (DropDownListProjStatus.SelectedValue == "NEW")
            {
                Business b1 = new Business();
                CountSancInfoTp = b1.SelectCountUploadSanctionInformationType(TextBoxID.Text, DropDownListGrUnit.SelectedValue);
                if (CountSancInfoTp == 0)
                {
                    ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('File should be uploaded for the information type-Sanctioned Details!')</script>");
                    return;
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
            j.UTN = UTN;
            j.Title = title;
            j.Description = dec;
            j.GrantingAgency = hdnAgencyId.Value;
            j.AddtionalComments = TextBoxAdComments.Text.Trim();

            j.GrantUnit = GUnit;

            if (TextBoxGrantAmt.Text != "")
            {
                j.GranAmount = Math.Round(Convert.ToDouble(GAmt), 2);
            }
            j.GrantSource = GSource;
            j.GrantType = GType;
            j.Status = Status;
            j.CreatedBy = Session["UserId"].ToString();
            j.CreatedDate = DateTime.Now;
            j.InstUser = Session["InstituteId"].ToString();
            j.DeptUser = Session["Department"].ToString();
            j.Contact_No = txtcontact.Text.Trim();
            j.Address = txtAddress.Text;
            j.Pan_No = txtpan.Text;
            j.State = txtstate.Text;
            j.Country = txtcountry.Text;
            j.AgencyContact = txtagencycontact.Text;
            j.AgencyEmailId = txtEmailId.Text;
            j.RejectBy = Session["UserId"].ToString();


            if (DropDownListProjStatus.SelectedValue == "APP")
            {
                j.RevisedAppliedAmt = j.GranAmount;
            }
            else
            {
                j.RevisedAppliedAmt = Convert.ToDouble(txtRevisedAppliedAmt.Text.Trim());
            }
            //if (txtprojectactualdate.Text != "")
            //{
            //    j.ProjectActualDate = Convert.ToDateTime(txtprojectactualdate.Text);
            //}
            //else
            //{
            //    j.ProjectActualDate = Convert.ToDateTime(TextBoxGrantDate.Text);
            //}
            //if (txtprojectactualdate.Text != "")
            //{
            //    int resultdate1 = DateTime.Compare(Convert.ToDateTime(TextBoxGrantDate.Text), Convert.ToDateTime(txtprojectactualdate.Text));
            //    if (resultdate1 < 0)
            //    {
            //        ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Actual Applied Date  must be less than or equal to applied date')</script>");
            //        return;
            //    }
            //}
            if (DropDownListProjStatus.SelectedValue == "APP")
            {
                if (TextBoxID.Text == "")
                {
                    ArrayList list = new ArrayList();
                    list = B.CheckDuplicates(j.GrantType);
                    for (int i = 0; i < list.Count; i++)
                    {
                        string title1 = list[i].ToString().ToLower();
                        string title2 = j.Title.ToLower();
                        title1 = title1.Replace(" ", String.Empty);
                        title2 = title2.Replace(" ", String.Empty);
                        if (title2 == title1)
                        {
                            ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Project details already exists!')</script>");
                            return;
                        }
                        else
                        {
                        }

                    }
                }
            }

            if (j.AccountHead == "")
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please enter Account Head!')</script>");
                return;
            }
            j.SancType = DropDownListSanType.SelectedValue;

            if (TextBoxSanctionedAmountCapital.Text != "")
            {
                j.SanctionCapitalAmount = Convert.ToDouble(TextBoxSanctionedAmountCapital.Text.Trim());
            }

            if (TextBoxSanctionedAmountOperating.Text != "")
            {
                j.SanctionOperatingAmount = Convert.ToDouble(TextBoxSanctionedAmountOperating.Text.Trim());
            }

            if (TextBoxSanctionedamountTotal.Text != "")
            {
                j.SanctionTotalAmount = Convert.ToDouble(TextBoxSanctionedamountTotal.Text.Trim());
            }

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
                j.InstitutionSahre = Math.Round((Convert.ToDouble(txtInstitutionshare.Text.Trim())), 2);
            }
            j.ServiceTaxApplicable = DropDownList2.SelectedValue;
            j.Status = DropDownListProjStatus.SelectedValue;
            j.AddtionalComments = TextBoxAdComments.Text.Trim();
            if (txtProjectDuration.Text != "")
            {
                j.DurationOfProject = Convert.ToInt16(txtProjectDuration.Text.Trim());
            }


            if (DropDownListProjStatus.SelectedValue == "SUB" || DropDownListProjStatus.SelectedValue == "SAN")
            {
                j.SancType = DropDownListSanType.SelectedValue;
                j.AuditRequired = ddlauditrequired.SelectedValue.Trim();

                j.AccountHead = txtaccounthead.Text.Trim();
                if (Session["Role"].ToString() == "6")
                {
                    if (j.AccountHead == "")
                    {
                        ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please enter Account Head!')</script>");
                        return;
                    }
                }
                if (txtInstitutionshare.Text != "")
                {
                    j.InstitutionSahre = Math.Round(Convert.ToDouble(txtInstitutionshare.Text.Trim()), 2);
                }
                if (DropDownListSanType.SelectedValue == "CA")
                {
                    if (DropDownListProjStatus.SelectedValue == "SAN")
                    {
                        TextBox santotalAmount = (TextBox)GridViewSanction.Rows[0].Cells[3].FindControl("txtsantotalAmount");
                        TextBox sancapitalAmount = (TextBox)GridViewSanction.Rows[0].Cells[4].FindControl("txtsancapitalAmount");
                        TextBox SanOpeAmt = (TextBox)GridViewSanction.Rows[0].Cells[5].FindControl("txtSanOpeAmt");

                        int rowscount = GridViewSanction.Rows.Count;

                        for (int i = 0; i < rowscount; i++)
                        {
                            TextBox santotalAmount1 = (TextBox)GridViewSanction.Rows[i].Cells[3].FindControl("txtsantotalAmount");
                            TextBox sancapitalAmount1 = (TextBox)GridViewSanction.Rows[i].Cells[4].FindControl("txtsancapitalAmount");
                            TextBox SanOpeAmt1 = (TextBox)GridViewSanction.Rows[i].Cells[5].FindControl("txtSanOpeAmt");

                            if (santotalAmount1.Text.Trim() != "")
                            {
                                j.SanctionTotalAmount = j.SanctionTotalAmount + Convert.ToDouble(santotalAmount1.Text.Trim());
                            }
                            if (sancapitalAmount1.Text.Trim() != "")
                            {
                                j.SanctionCapitalAmount = j.SanctionCapitalAmount + Convert.ToDouble(sancapitalAmount1.Text.Trim());
                            }
                            if (SanOpeAmt1.Text.Trim() != "")
                            {
                                j.SanctionOperatingAmount = j.SanctionOperatingAmount + Convert.ToDouble(SanOpeAmt1.Text.Trim());
                            }
                        }

                    }
                    else
                    {
                        // j.SanctionNumber = TextBoxSanctionnNumber.Text.Trim();
                        if (TextBoxSanctionedAmountCapital.Text == "")
                        {
                            // j.SanctionCapitalAmount = Convert.ToDouble(0);
                        }
                        else
                        {
                            j.SanctionCapitalAmount = Convert.ToDouble(TextBoxSanctionedAmountCapital.Text.Trim());
                        }
                        if (TextBoxSanctionedAmountOperating.Text == "")
                        {
                            //j.SanctionOperatingAmount = Convert.ToDouble(0);
                        }
                        else
                        {
                            j.SanctionOperatingAmount = Convert.ToDouble(TextBoxSanctionedAmountOperating.Text.Trim());
                        }
                        if (TextBoxSanctionedamountTotal.Text != "")
                        {
                            j.SanctionTotalAmount = Convert.ToDouble(TextBoxSanctionedamountTotal.Text.Trim());
                        }
                    }
                    //if (TextBoxSanctionDate.Text != "")
                    //{
                    //    j.SanctionDate = Convert.ToDateTime(TextBoxSanctionDate.Text.Trim());
                    //}
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
                    DataTable dtSanDetailCurrentTable2 = (DataTable)ViewState["Sanction"];
                    j.SanctionEntryNumber = dtSanDetailCurrentTable2.Rows.Count;

                }
                else if (DropDownListSanType.SelectedValue == "KI")
                {
                    //j.SanctionNumber = TextBoxSanctionnNumber.Text.Trim();
                    //j.RecievedFrom = TextBoxRecievedFrom.Text.Trim();
                    //j.INREquivalent = Convert.ToDouble(TextBoxINREquivalent.Text.Trim());
                    //j.RecievedAmount = Convert.ToDouble(TextBoxReceivedAmount.Text.Trim());
                    //j.RecievedDate = Convert.ToDateTime(TextBoxRecievedDate.Text.Trim());
                }
            }

            int rowIndex1 = 0;
            if (dtCurrentTable.Rows.Count > 0)
            {

                for (int i = 0; i < dtCurrentTable.Rows.Count; i++)
                {
                    JD[i] = new GrantData();
                    TextBox AuthorName = (TextBox)Grid_AuthorEntry.Rows[rowIndex1].Cells[1].FindControl("AuthorName");
                    DropDownList DropdownMuNonMu = (DropDownList)Grid_AuthorEntry.Rows[rowIndex1].Cells[2].FindControl("DropdownMuNonMu");
                    TextBox EmployeeCode = (TextBox)Grid_AuthorEntry.Rows[rowIndex1].Cells[0].FindControl("EmployeeCode");
                    HiddenField Institution = (HiddenField)Grid_AuthorEntry.Rows[rowIndex1].Cells[0].FindControl("Institution");
                    TextBox InstitutionName = (TextBox)Grid_AuthorEntry.Rows[rowIndex1].Cells[6].FindControl("InstitutionName");
                    HiddenField Department = (HiddenField)Grid_AuthorEntry.Rows[rowIndex1].Cells[0].FindControl("Department");
                    TextBox DepartmentName = (TextBox)Grid_AuthorEntry.Rows[rowIndex1].Cells[0].FindControl("DepartmentName");
                    TextBox MailId = (TextBox)Grid_AuthorEntry.Rows[rowIndex1].Cells[0].FindControl("MailId");
                    DropDownList AuthorType = (DropDownList)Grid_AuthorEntry.Rows[rowIndex1].Cells[0].FindControl("AuthorType");
                    DropDownList isLeadPI = (DropDownList)Grid_AuthorEntry.Rows[rowIndex1].Cells[0].FindControl("isLeadPI");
                    DropDownList DropdownStudentInstitutionName = (DropDownList)Grid_AuthorEntry.Rows[rowIndex1].Cells[0].FindControl("DropdownStudentInstitutionName");
                    DropDownList DropdownStudentDepartmentName = (DropDownList)Grid_AuthorEntry.Rows[rowIndex1].Cells[0].FindControl("DropdownStudentDepartmentName");

                    DropDownList NationalType = (DropDownList)Grid_AuthorEntry.Rows[rowIndex1].Cells[0].FindControl("NationalType");
                    DropDownList ContinentId = (DropDownList)Grid_AuthorEntry.Rows[rowIndex1].Cells[0].FindControl("ContinentId");

                    if (AuthorName.Text == "")
                    {
                        ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please enter Investigator Name!')</script>");
                        return;

                    }

                    if (AuthorType.Text == "")
                    {
                        ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please select Investigator Type!')</script>");
                        return;

                    }
                    if (DropdownMuNonMu.SelectedValue == "M")
                    {
                        if (InstitutionName.Text == "")
                        {
                            ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please enter Institution Name!')</script>");
                            return;

                        }

                        if (DepartmentName.Text == "")
                        {
                            ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please enter Department Name!')</script>");
                            return;

                        }
                    }
                    else if (DropdownMuNonMu.SelectedValue == "N"||DropdownMuNonMu.SelectedValue == "E")
                    {
                        if (InstitutionName.Text == "")
                        {
                            ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please enter Institution Name!')</script>");
                            return;

                        }

                        if (DepartmentName.Text == "")
                        {
                            ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please enter Department Name!')</script>");
                            return;

                        }
                    }
                    if (DropdownMuNonMu.SelectedValue == "M" || DropdownMuNonMu.SelectedValue == "S")
                    {
                        if (MailId.Text == "")
                        {
                            ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please enter MailId!')</script>");
                            return;

                        }
                    }


                    JD[i].AuthorName = AuthorName.Text.Trim();
                    JD[i].MUNonMU = DropdownMuNonMu.Text.Trim();
                    if (JD[i].MUNonMU == "M" || JD[i].MUNonMU == "S")
                    {
                        JD[i].EmployeeCode = EmployeeCode.Text;
                    }
                    else if (JD[i].MUNonMU == "O")
                    {

                        JD[i].EmployeeCode = EmployeeCode.Text.Trim();
                    }
                    else
                    {
                        JD[i].EmployeeCode = AuthorName.Text.Trim();
                    }
                    if (JD[i].MUNonMU == "N")
                    {

                        JD[i].NationalInternationl = NationalType.SelectedValue;
                        if (NationalType.SelectedValue == "I")
                        {

                            JD[i].continental = ContinentId.SelectedValue;
                        }
                        else
                        {
                            JD[i].continental = "";
                        }

                        JD[i].Institution = InstitutionName.Text.Trim();
                        JD[i].InstitutionName = InstitutionName.Text.Trim();
                        JD[i].Department = DepartmentName.Text.Trim();
                        JD[i].DepartmentName = DepartmentName.Text.Trim();
                        JD[i].AppendInstitutions = JD[i].Institution;
                    }
                    else if (JD[i].MUNonMU == "E")
                    {

                        JD[i].NationalInternationl = NationalType.SelectedValue;
                        if (NationalType.SelectedValue == "I")
                        {

                            JD[i].continental = ContinentId.SelectedValue;
                        }
                        else
                        {
                            JD[i].continental = "";
                        }

                        JD[i].Institution = InstitutionName.Text.Trim();
                        JD[i].InstitutionName = InstitutionName.Text.Trim();
                        JD[i].Department = DepartmentName.Text.Trim();
                        JD[i].DepartmentName = DepartmentName.Text.Trim();
                        JD[i].AppendInstitutions = JD[i].Institution;
                    }
                    else if (JD[i].MUNonMU == "M")
                    {
                        JD[i].NationalInternationl = "";
                        JD[i].continental = "";

                        JD[i].Institution = Institution.Value.Trim();
                        JD[i].InstitutionName = InstitutionName.Text.Trim();
                        JD[i].Department = Department.Value.Trim();
                        JD[i].DepartmentName = DepartmentName.Text.Trim();
                        JD[i].AppendInstitutions = JD[i].Institution;

                    }
                    else if (JD[i].MUNonMU == "S")
                    {

                        JD[i].NationalInternationl = "";
                        JD[i].continental = "";

                        JD[i].Institution = Institution.Value.Trim();
                        JD[i].InstitutionName = InstitutionName.Text.Trim();
                        JD[i].Department = Department.Value.Trim();
                        JD[i].DepartmentName = DepartmentName.Text.Trim();
                        JD[i].AppendInstitutions = JD[i].Institution;

                    }
                    else if (JD[i].MUNonMU == "O")
                    {
                        JD[i].NationalInternationl = "";
                        JD[i].continental = "";
                        JD[i].EmployeeCode = EmployeeCode.Text.Trim();
                        JD[i].InstitutionName = DropdownStudentInstitutionName.SelectedItem.ToString();
                        JD[i].Department = DropdownStudentDepartmentName.SelectedValue;
                        JD[i].DepartmentName = DropdownStudentDepartmentName.SelectedItem.ToString();
                        JD[i].Institution = DropdownStudentInstitutionName.SelectedValue;
                        JD[i].AppendInstitutions = JD[i].Institution;
                        JD[i].EmployeeCode = JD[i].EmployeeCode.Trim();
                        JD[i].EmailId = MailId.Text.Trim();
                    }

                    JD[i].AppendInstitutionNames = JD[i].InstitutionName;

                    JD[i].EmailId = MailId.Text.Trim();
                    JD[i].AuthorType = AuthorType.Text.Trim();
                    JD[i].LeadPI = isLeadPI.Text.Trim();
                    //if (countAuthType < 1)
                    //{

                    if (JD[i].AuthorType == "P" && JD[i].LeadPI == "Y")
                    {
                        if (JD[i].MUNonMU == "N")
                        {
                            j.MUNonMUUTN = "NUTN";
                            //j.PiInstId = InstitutionName.Text.Trim();
                            //j.PiDeptId = DepartmentName.Text.Trim();
                            j.PiInstId = Session["InstituteId"].ToString();
                            j.PiDeptId = Session["Department"].ToString();
                        }
                        else if (JD[i].MUNonMU == "E")
                        {
                            j.MUNonMUUTN = "NUTN";
                            //j.PiInstId = InstitutionName.Text.Trim();
                            //j.PiDeptId = DepartmentName.Text.Trim();
                            j.PiInstId = Session["InstituteId"].ToString();
                            j.PiDeptId = Session["Department"].ToString();
                        }
                        else if (JD[i].MUNonMU == "M")
                        {
                            j.MUNonMUUTN = "MUTN";
                            j.PiInstId = Institution.Value.Trim();
                            j.PiDeptId = Department.Value.Trim();

                        }
                        else if (JD[i].MUNonMU == "O")
                        {
                            j.MUNonMUUTN = "MUTN";
                            j.PiInstId = DropdownStudentInstitutionName.SelectedValue;
                            j.PiDeptId = DropdownStudentDepartmentName.SelectedValue;

                        }
                        else if (JD[i].MUNonMU == "S")
                        {
                            j.MUNonMUUTN = "NUTN";
                            j.PiInstId = Session["InstituteId"].ToString();
                            j.PiDeptId = Session["Department"].ToString();
                        }


                    }
                    //}
                    if (JD[i].AuthorType == "P")
                    {
                        countAuthType = countAuthType + 1;
                    }

                    if (JD[i].isCorrAuth == "Y")
                    {
                        countCorrAuth = countCorrAuth + 1;
                    }
                    if (JD[i].LeadPI == "Y")
                    {
                        countLeadPI = countLeadPI + 1;
                    }

                    rowIndex1++;
                }

            }


            DataTable SancKindCurrentTable = (DataTable)ViewState["SancKindCurrentTable"];
            if (DropDownListSanType.SelectedValue == "KI")
            {

                JD1 = new GrantData[SancKindCurrentTable.Rows.Count];

                int rowIndex11 = 0;
                j.KindComments = TextBoxKindDetails.Text.Trim();
                if (SancKindCurrentTable.Rows.Count > 0)
                {

                    for (int i = 0; i < SancKindCurrentTable.Rows.Count; i++)
                    {
                        JD1[i] = new GrantData();

                        TextBox ReceivedDate = (TextBox)GridViewkindDetails.Rows[rowIndex11].Cells[0].FindControl("ReceivedDate");
                        TextBox INREquivalent = (TextBox)GridViewkindDetails.Rows[rowIndex11].Cells[1].FindControl("INREquivalent");
                        TextBox Details = (TextBox)GridViewkindDetails.Rows[rowIndex11].Cells[0].FindControl("Details");




                        JD1[i].details = Details.Text.Trim();

                        if (ReceivedDate.Text == "")
                        {
                            ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please enter Received Date!!')</script>");
                            return;
                        }

                        if (INREquivalent.Text == "")
                        {
                            ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please enter INR Equivalent amount!!')</script>");
                            return;
                        }



                        Regex regex = new Regex("^([0-9]{1,3},([0-9]{3},)*[0-9]{3}|[0-9]+)(.[0-9][0-9]*$)?$");
                        if (INREquivalent.Text != "" && !regex.IsMatch(INREquivalent.Text))
                        {
                            ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('INR Equivalent must be in numbers!')</script>");

                            INREquivalent.Text = "";
                            return;
                        }

                        JD1[i].RecievedDate = Convert.ToDateTime(ReceivedDate.Text.Trim());

                        JD1[i].INREquivalent = Convert.ToDouble(INREquivalent.Text.Trim());

                        rowIndex11++;
                    }
                    j.AppendInstitutionNames = "";
                    j.AppendInstitutions = "";
                }
            }
            if (DropDownListProjStatus.SelectedValue == "SAN")
            {
                if (DropDownListSanType.SelectedValue == "KI")
                {
                    int rowIndex11 = 0;
                    j.KindComments = TextBoxKindDetails.Text.Trim();
                    if (TextKindStartDate.Text != "")
                    {
                        j.KindStartDate = Convert.ToDateTime(TextKindStartDate.Text);
                    }
                    if (TextKindclosedate.Text != "")
                    {
                        j.KindCloseDate = Convert.ToDateTime(TextKindclosedate.Text);
                    }

                    if (SancKindCurrentTable.Rows.Count > 0)
                    {

                        for (int i = 0; i < SancKindCurrentTable.Rows.Count; i++)
                        {
                            JD1[i] = new GrantData();

                            TextBox ReceivedDate = (TextBox)GridViewkindDetails.Rows[rowIndex11].Cells[0].FindControl("ReceivedDate");
                            TextBox INREquivalent = (TextBox)GridViewkindDetails.Rows[rowIndex11].Cells[1].FindControl("INREquivalent");
                            TextBox Details = (TextBox)GridViewkindDetails.Rows[rowIndex11].Cells[0].FindControl("Details");
                            JD1[i].details = Details.Text.Trim();

                            if (ReceivedDate.Text == "")
                            {
                                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please enter Received Date!!')</script>");
                                return;
                            }

                            if (INREquivalent.Text == "")
                            {
                                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please enter INR Equivalent amount!!')</script>");
                                return;
                            }



                            Regex regex = new Regex("^([0-9]{1,3},([0-9]{3},)*[0-9]{3}|[0-9]+)(.[0-9][0-9]*$)?$");
                            if (INREquivalent.Text != "" && !regex.IsMatch(INREquivalent.Text))
                            {
                                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('INR Equivalent must be in numbers!')</script>");

                                INREquivalent.Text = "";
                                return;
                            }

                            JD1[i].RecievedDate = Convert.ToDateTime(ReceivedDate.Text.Trim());

                            JD1[i].INREquivalent = Convert.ToDouble(INREquivalent.Text.Trim());

                            rowIndex11++;
                        }
                        j.AppendInstitutionNames = "";
                        j.AppendInstitutions = "";
                    }

                }

            }




            //sanction
            DataTable dtSanDetailCurrentTable1 = (DataTable)ViewState["Sanction"];
            GrantData[] SD3 = new GrantData[dtSanDetailCurrentTable1.Rows.Count];
            GrantData journalSanction = new GrantData();
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
                    string SanctionNo = sanctionNo.Text.Trim();
                    string Sanction_date = Sanctiondate.Text.Trim();

                    string Total_Amt = santotalAmount.Text.Trim();
                    string Capital_Amt = sancapitalAmount.Text.Trim();
                    string Operating_Amt = SanOpeAmt.Text.Trim();
                    if (dtSanDetailCurrentTable1.Rows.Count == 1)
                    {
                        if (SanctionNo == "" && Sanction_date == "" && Total_Amt == "" && Capital_Amt == "")
                        {

                        }

                        else
                        {
                            journalSanction.GID = TextBoxID.Text;
                            journalSanction.GrantUnit = DropDownListGrUnit.SelectedValue;

                            SD3[i].SanctionNumber = sanctionNo.Text.Trim();
                            SD3[i].SanctionDate = Convert.ToDateTime(Sanctiondate.Text.Trim());
                            SD3[i].SanctionTotalAmount = Convert.ToDouble(santotalAmount.Text.Trim());
                            SD3[i].SanctionCapitalAmount = Convert.ToDouble(sancapitalAmount.Text.Trim());
                            SD3[i].SanctionOperatingAmount = Convert.ToDouble(SanOpeAmt.Text.Trim());


                        }
                    }
                    else
                    {
                        journalSanction.GID = TextBoxID.Text;
                        journalSanction.GrantUnit = DropDownListGrUnit.SelectedValue;

                        SD3[i].SanctionNumber = sanctionNo.Text.Trim();
                        SD3[i].SanctionDate = Convert.ToDateTime(Sanctiondate.Text.Trim());
                        SD3[i].SanctionTotalAmount = Convert.ToDouble(santotalAmount.Text.Trim());
                        SD3[i].SanctionCapitalAmount = Convert.ToDouble(sancapitalAmount.Text.Trim());
                        SD3[i].SanctionOperatingAmount = Convert.ToDouble(SanOpeAmt.Text.Trim());

                    }
                    rowIndex++;
                }

            }


            if (DropDownListProjStatus.SelectedValue == "APP")
            {
                if (countAuthType == 0)
                {
                    ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Select atleast one Investigator Type as Primary Investigator !')</script>");
                    return;

                }


                if (countLeadPI > 1)
                {
                    ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Lead PI cannot be more than one!')</script>");
                    return;

                }

                if (countLeadPI == 0)
                {
                    ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Select atleast one Lead PI!')</script>");
                    return;

                }
            }

            int resultsub = 0;
            resultsub = B.UpdateStatusGrantEntry(j, JD, JD1, SD3);
                if(resultsub>=0)
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Grant data Submitted Successfully.. of ID: " + TextBoxID.Text + " !')</script>");
                EmailDetails details = new EmailDetails();
                details = SendMail();
                details.Id = TextBoxID.Text;
                details.Type = DropDownListTypeGrant.SelectedItem.ToString();
                details.ProjectUnit = DropDownListGrUnit.SelectedItem.ToString();
                details.UnitId = DropDownListGrUnit.SelectedValue.ToString();
                SendMailObject obj1 = new SendMailObject();
                bool result1 = obj1.InsertIntoEmailQueue(details);
                BtnSanction.Enabled = false;
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
            else if (ex.Message.Contains("IX_Project"))
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Project Creation Failed.This Project Already Present!')</script>");

            }

            else if (ex.Message.Contains("Failure sending mail."))
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Grant Data submitted successfully.Failure in sending mail.')</script>");

            }

            else

                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Grant data Creation failed')</script>");
            log.Error("Grant data Creaton Failed.... id: " + TextBoxID.Text + " , Type: " + DropDownListTypeGrant.SelectedValue);

        }
    }




    private EmailDetails SendMail()
    {
        EmailDetails details = new EmailDetails();
       
       log.Debug(" GrantEntry:Inside Send Mail for Applied Project of type: " + DropDownListTypeGrant.SelectedValue + " ID: " + TextBoxID.Text + " Project Unit: " + DropDownListGrUnit.Text);
           
            details.Module = "GSUB";
            details.EmailSubject = "Project Entry < " + DropDownListTypeGrant.SelectedValue + " _ " + TextBoxID.Text + "  > submitted ";
            ArrayList myArrayListInvestigator = new ArrayList();
            ArrayList myArrayListInvestigator1 = new ArrayList();
            ArrayList myArrayListInvestigatorName = new ArrayList();
            ArrayList myArrayListReserachCoOrdinator = new ArrayList();
            ArrayList myArrayListInvestigatorDetail = new ArrayList();
            ArrayList myArrayListResearchDirector = new ArrayList();
            DataSet ds = new DataSet();
            DataSet dsIN = new DataSet();
            Business bus = new Business();
            Business e = new Business();
            int result;

            ds = bus.getInvetigatorList(TextBoxID.Text, DropDownListGrUnit.SelectedValue);
            dsIN = bus.getInvetigatorDETAIL(TextBoxID.Text, DropDownListGrUnit.SelectedValue);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                myArrayListInvestigator.Add(ds.Tables[0].Rows[i]["EmailId"].ToString());
                if (myArrayListInvestigator[i].ToString() == "")
                {
                    int j = i;
                    if ((j == i) && (j < dsIN.Tables[0].Rows.Count))
                    {
                        myArrayListInvestigatorDetail.Add(dsIN.Tables[0].Rows[j]["InvestigatorName"].ToString());
                    }

                }
            }

            for (int k = 0; k < myArrayListInvestigatorDetail.Count; k++)
            {
                string InvestigatorName = myArrayListInvestigatorDetail[k].ToString();

                result = e.insertEmailtracker(InvestigatorName, details, TextBoxID.Text);
            }

            DataSet ds2 = new DataSet();
            ds2 = bus.getReserachDirectorclerk(TextBoxID.Text, DropDownListGrUnit.SelectedValue);
            for (int i = 0; i < ds2.Tables[0].Rows.Count; i++)
            {
                myArrayListResearchDirector.Add(ds2.Tables[0].Rows[i]["EmailId"].ToString());
            }



            //DataSet ds1 = new DataSet();
            //ds1 = bus.getReserachCoOrdinator(TextBoxID.Text, DropDownListGrUnit.SelectedValue);
            //for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
            //{
            //    myArrayListReserachCoOrdinator.Add(ds1.Tables[0].Rows[i]["EmailId"].ToString());
            //}
            DataSet dy = new DataSet();
            dy = bus.getInvietigatorListName(TextBoxID.Text, DropDownListGrUnit.SelectedValue);

            for (int i = 0; i < dy.Tables[0].Rows.Count; i++)
            {
                myArrayListInvestigatorName.Add(dy.Tables[0].Rows[i]["InvestigatorName"].ToString());
            }

            string auhtorsS = "";
            string auhtorsSConc = "";
            for (int i = 0; i < myArrayListInvestigatorName.Count; i++)
            {
                auhtorsS = myArrayListInvestigatorName[i].ToString();
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

            details.EmailSubject = "Project Entry < " + DropDownListTypeGrant.SelectedValue + " _ " + TextBoxID.Text + "  > Submitted ";
            details.MsgBody = "<span style=\"font-size: 9pt; color: #3300cc; font-family: Verdana\"><h4>Dear Sir/Madam,</h4> <br>" +
             "<b> The following Project  has been Uploaded in Project Repository : <br> " +
                 "<br>" +
                   "Project Type  : " + DropDownListTypeGrant.SelectedItem + "<br>" +
                    "Project Unit  :  " + DropDownListGrUnit.SelectedItem + "<br>" +
                "Project Id  :  " + TextBoxID.Text + "<br>" +
                 "UTN  :  " + TextBoxUTN.Text + "<br>" +
                "Title  : " + TextBoxTitle.Text + "<br>" +
                "Added By  : " + myArrayListInvestigatorName[0].ToString() + "<br>" +
                "Investigators  : " + auhtorsSConc + "<br>" + "<br>" + "<br>" + "<br>" + "<br>" + FooterText +
                "</span>";

            details.FromEmail = ConfigurationManager.AppSettings["FromAddress"].ToString();
            details.Module = "GSUB";


            for (int i = 0; i < myArrayListInvestigator.Count; i++)
            {
                //string email = myArrayListInvestigator[i].ToString();
                //if (details.ToEmail != null)
                //{

                //    details.ToEmail = details.ToEmail + ',' + myArrayListInvestigator[i].ToString();
                //}
                //else
                //{
                //    if (i == 0)
                //    {
                //        details.ToEmail = myArrayListInvestigator[i].ToString();
                //    }
                //    else
                //    {
                //        details.ToEmail = details.ToEmail + ',' + myArrayListInvestigator[i].ToString();
                //    }
                //}
            //    string email = myArrayListInvestigator[i].ToString();
            //    if (details.ToEmail != null)
            //    {
            //        if (myArrayListInvestigator[i].ToString() != "")
            //        {
            //            details.ToEmail = details.ToEmail + ',' + myArrayListInvestigator[i].ToString();
            //        }

            //    }
            //    else
            //    {
            //        if (i == 0)
            //        {
            //            if (myArrayListInvestigator[i].ToString() != "")
            //            {
            //                details.ToEmail = myArrayListInvestigator[i].ToString();
            //            }
            //        }
            //        else
            //        {
            //            if (myArrayListInvestigator[i].ToString() != "")
            //            {
            //                if (details.ToEmail == null)
            //                {
            //                    details.ToEmail = myArrayListInvestigator[i].ToString();
            //                }
            //                else
            //                {
            //                    details.ToEmail = details.ToEmail + ',' + myArrayListInvestigator[i].ToString();
            //                }
            //            }
            //        }
            //        //details.CCEmail = details.CCEmail + ',' + email;
            //    }

            //    // details.ToEmail = email;
            //    log.Info(" Email will be sent to Investigators '" + i + "' : '" + myArrayListInvestigator[i] + "' ");
            //}
                string email = myArrayListInvestigator[i].ToString();
                if (details.CCEmail != null)
                {
                    if (myArrayListInvestigator[i].ToString() != "")
                    {
                        details.CCEmail = details.CCEmail + ',' + myArrayListInvestigator[i].ToString();
                    }

                }
                else
                {
                    if (i == 0)
                    {
                        if (myArrayListInvestigator[i].ToString() != "")
                        {
                            details.CCEmail = myArrayListInvestigator[i].ToString();
                        }
                    }
                    else
                    {
                        if (myArrayListInvestigator[i].ToString() != "")
                        {
                            if (details.CCEmail == null)
                            {
                                details.CCEmail = myArrayListInvestigator[i].ToString();
                            }
                            else
                            {
                                details.CCEmail = details.CCEmail + ',' + myArrayListInvestigator[i].ToString();
                            }
                        }
                    }
                    //details.CCEmail = details.CCEmail + ',' + email;
                }

                // details.ToEmail = email;
                log.Info(" Email will be sent to Investigators '" + i + "' : '" + myArrayListInvestigator[i] + "' ");
            }
            //for (int i = 0; i < myArrayListReserachCoOrdinator.Count; i++)
            //{
            //    string email = myArrayListReserachCoOrdinator[i].ToString();
            //    if (details.CCEmail != null)
            //    {
            //        details.CCEmail = details.CCEmail + ',' + myArrayListReserachCoOrdinator[i].ToString();
            //    }
            //    else
            //    {
            //        if (i == 0)
            //        {
            //            details.CCEmail = myArrayListReserachCoOrdinator[i].ToString();
            //        }
            //        else
            //        {
            //            details.CCEmail = details.CCEmail + ',' + myArrayListReserachCoOrdinator[i].ToString();
            //        }
            //    }

            //    // details.ToEmail = email;
               
            //}
            for (int i = 0; i < myArrayListResearchDirector.Count; i++)
            {
                string emailRD = myArrayListResearchDirector[i].ToString();
                if (details.ToEmail != null)
                {
                    details.ToEmail = details.ToEmail + ',' + myArrayListResearchDirector[i].ToString();
                }
                else
                {
                    if (i == 0)
                    {
                        details.ToEmail = myArrayListResearchDirector[i].ToString();
                    }
                    else
                    {
                        details.ToEmail = details.ToEmail + ',' + myArrayListResearchDirector[i].ToString();
                    }
                }

                // details.ToEmail = email;
                log.Info(" Email will be sent to Research CoOrdinator '" + i + "' : '" + myArrayListResearchDirector[i] + "' ");
            }
           
        return details;
    }

   
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
                    //Business B = new Business();
                    result1 = B.UploadGrnatPathCreate(j);


                    if ((Session["Role"].ToString() == "11" || Session["Role"].ToString() == "1"))
                    {
                        DSforgridview.SelectParameters.Clear();
                        DSforgridview.SelectParameters.Add("ID", j.GID);
                        DSforgridview.SelectParameters.Add("GrantUnit", j.GrantUnit);

                        DSforgridview.SelectCommand = "select UploadPDFPath ,q.InfoTypeName as TypeName ,Remark ,AuditFrom,AuditTo, p.CreatedDate, p.CreatedBy as CreatedBy, m.FirstName as AddedBy, EntryNo,Unit_Id ,p.InfoTypeId from ProjectAuxillaryDetails p,Project_AuxInfoTypeM q, User_M m where  p.CreatedBy='" + Session["UserId"].ToString() + "' and p.InfoTypeId=q.InfoTypeId and m.User_Id=p.CreatedBy  and ID=@ID and ProjectUnit=@GrantUnit and Deleted='N' order by EntryNo";
                        DSforgridview.DataBind();
                        GVViewFile.DataBind();

                        DSforgridview1.SelectParameters.Add("ID", j.GID);
                        DSforgridview1.SelectParameters.Add("GrantUnit", j.GrantUnit);
                        DSforgridview1.SelectCommand = "select UploadPDFPath ,q.InfoTypeName as TypeName ,Remark ,AuditFrom,AuditTo, p.CreatedDate, p.CreatedBy as CreatedBy, m.FirstName as AddedBy, EntryNo,Unit_Id ,p.InfoTypeId from ProjectAuxillaryDetails p,Project_AuxInfoTypeM q, User_M m where  p.InfoTypeId=q.InfoTypeId and m.User_Id=p.CreatedBy  and ID=@ID and ProjectUnit=@GrantUnit and Deleted='N' and p.CreatedBy  not in  (Select CreatedBy from ProjectAuxillaryDetails where ProjectUnit=@GrantUnit and ID=@ID and Deleted='N')  order by EntryNo";
                        DSforgridview1.DataBind();
                        GridView1.DataBind();
                        Panel8.Visible = true;
                    }

                    if (Session["Role"].ToString() == "20")
                    {
                        DSforgridview.SelectParameters.Clear();
                        DSforgridview.SelectParameters.Add("ID", j.GID);
                        DSforgridview.SelectParameters.Add("GrantUnit", j.GrantUnit);
                        DSforgridview.SelectCommand = "select UploadPDFPath ,q.InfoTypeName as TypeName ,Remark ,AuditFrom,AuditTo, p.CreatedDate, p.CreatedBy as CreatedBy, m.FirstName as AddedBy, EntryNo,Unit_Id ,p.InfoTypeId from ProjectAuxillaryDetails p,Project_AuxInfoTypeM q, User_M m where  p.InfoTypeId=q.InfoTypeId and m.User_Id=p.CreatedBy  and ID=@ID and ProjectUnit=@GrantUnit and Deleted='N' and p.CreatedBy='" + Session["UserId"].ToString() + "' order by EntryNo";
                        DSforgridview.DataBind();
                        GridView1.DataBind();
                        Panel8.Visible = true;

                        DSforgridview1.SelectParameters.Clear();
                        DSforgridview1.SelectParameters.Add("ID", j.GID);
                        DSforgridview1.SelectParameters.Add("GrantUnit", j.GrantUnit);
                        DSforgridview1.SelectCommand = "select UploadPDFPath ,q.InfoTypeName as TypeName ,Remark ,AuditFrom,AuditTo, p.CreatedDate, p.CreatedBy as CreatedBy, m.FirstName as AddedBy, EntryNo,Unit_Id ,p.InfoTypeId from ProjectAuxillaryDetails p,Project_AuxInfoTypeM q, User_M m where  p.CreatedBy!='" + Session["UserId"].ToString() + "' and p.InfoTypeId=q.InfoTypeId and m.User_Id=p.CreatedBy  and ID=@ID and ProjectUnit=@GrantUnit and Deleted='N' order by EntryNo";
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






    protected void GVViewFile_SelectedIndexChanged1(object sender, EventArgs e)
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
    protected void GVViewFile_RowDataBound1(object sender, GridViewRowEventArgs e)
    {
        ImageButton EditButton = (ImageButton)e.Row.FindControl("BtnEdit");
    }
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
            Business B = new Business();
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

                        if ((Session["Role"].ToString() == "11" || Session["Role"].ToString() == "1" || Session["Role"].ToString() == "20"))
                        {
                            DSforgridview.SelectParameters.Clear();
                            DSforgridview.SelectParameters.Add("ID", j.GID);
                            DSforgridview.SelectParameters.Add("ProjectUnit", j.GrantType);
                            DSforgridview.SelectParameters.Add("ProjectUnit", j.GrantType);

                            DSforgridview.SelectCommand = "select UploadPDFPath ,q.InfoTypeName as TypeName ,Remark ,AuditFrom,AuditTo, p.CreatedDate, p.CreatedBy as CreatedBy, m.FirstName as AddedBy, EntryNo,Unit_Id ,p.InfoTypeId from ProjectAuxillaryDetails p,Project_AuxInfoTypeM q, User_M m where  p.CreatedBy='" + Session["UserId"].ToString() + "' and p.InfoTypeId=q.InfoTypeId and m.User_Id=p.CreatedBy  and ID=@ID and ProjectUnit=@ProjectUnit and Deleted='N' order by EntryNo";
                            DSforgridview.DataBind();
                            GVViewFile.DataBind();

                            DSforgridview1.SelectParameters.Clear();
                            DSforgridview1.SelectParameters.Add("ID", j.GID);
                            DSforgridview1.SelectParameters.Add("ProjectUnit", j.GrantType);
                            DSforgridview1.SelectCommand = "select UploadPDFPath ,q.InfoTypeName as TypeName ,Remark ,AuditFrom,AuditTo, p.CreatedDate, p.CreatedBy as CreatedBy, m.FirstName as AddedBy, EntryNo,Unit_Id ,p.InfoTypeId from ProjectAuxillaryDetails p,Project_AuxInfoTypeM q, User_M m where  p.InfoTypeId=q.InfoTypeId and m.User_Id=p.CreatedBy  and ID=@ID and ProjectUnit=@ProjectUnit and Deleted='N' and p.CreatedBy  <>  (Select CreatedBy from ProjectAuxillaryDetails where ProjectUnit='" + j.GrantUnit + "' and ID=@ID and Deleted='N') order by EntryNo";
                            DSforgridview1.DataBind();
                            GridView1.DataBind();
                            Panel8.Visible = true;
                        }

                        if (Session["Role"].ToString() == "6")
                        {
                            DSforgridview.SelectParameters.Clear();
                            DSforgridview.SelectParameters.Add("ID", j.GID);
                            DSforgridview.SelectParameters.Add("ProjectUnit", j.GrantType);
                            DSforgridview.SelectCommand = "select UploadPDFPath ,q.InfoTypeName as TypeName ,Remark ,AuditFrom,AuditTo, p.CreatedDate, p.CreatedBy as CreatedBy, m.FirstName as AddedBy, EntryNo,Unit_Id ,p.InfoTypeId from ProjectAuxillaryDetails p,Project_AuxInfoTypeM q, User_M m where  p.InfoTypeId=q.InfoTypeId and m.User_Id=p.CreatedBy  and ID=@ID and ProjectUnit=@ProjectUnit and Deleted='N' and p.CreatedBy  <>  (Select CreatedBy from ProjectAuxillaryDetails where ProjectUnit='" + j.GrantUnit + "' and ID=@ID and Deleted='N')  order by EntryNo";
                            DSforgridview.DataBind();
                            GridView1.DataBind();
                            Panel8.Visible = true;

                            DSforgridview1.SelectParameters.Clear();
                            DSforgridview1.SelectParameters.Add("ID", j.GID);
                            DSforgridview1.SelectParameters.Add("ProjectUnit", j.GrantType);
                            DSforgridview1.SelectCommand = "select UploadPDFPath ,q.InfoTypeName as TypeName ,Remark ,AuditFrom,AuditTo, p.CreatedDate, p.CreatedBy as CreatedBy, m.FirstName as AddedBy, EntryNo,Unit_Id ,p.InfoTypeId from ProjectAuxillaryDetails p,Project_AuxInfoTypeM q, User_M m where  p.CreatedBy!='" + Session["UserId"].ToString() + "' and p.InfoTypeId=q.InfoTypeId and m.User_Id=p.CreatedBy  and ID=@ID and ProjectUnit=@ProjectUnit and Deleted='N' order by EntryNo";
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
                            DSforgridview.SelectParameters.Add("ID", j.GID);
                            DSforgridview.SelectParameters.Add("ProjectUnit", j.GrantType);
                            DSforgridview.SelectCommand = "select UploadPDFPath ,q.InfoTypeName as TypeName ,Remark ,AuditFrom,AuditTo, p.CreatedDate, p.CreatedBy as CreatedBy, m.FirstName as AddedBy, EntryNo,Unit_Id ,p.InfoTypeId from ProjectAuxillaryDetails p,Project_AuxInfoTypeM q, User_M m where  p.CreatedBy='" + Session["UserId"].ToString() + "' and p.InfoTypeId=q.InfoTypeId and m.User_Id=p.CreatedBy  and ID=@ID and ProjectUnit=@ProjectUnit and Deleted='N' order by EntryNo";
                            DSforgridview.DataBind();
                            GVViewFile.DataBind();

                            DSforgridview1.SelectParameters.Clear();
                            DSforgridview1.SelectParameters.Add("ID", j.GID);
                            DSforgridview1.SelectCommand = "select UploadPDFPath ,q.InfoTypeName as TypeName ,Remark ,AuditFrom,AuditTo, p.CreatedDate, p.CreatedBy as CreatedBy, m.FirstName as AddedBy, EntryNo,Unit_Id ,p.InfoTypeId from ProjectAuxillaryDetails p,Project_AuxInfoTypeM q, User_M m where  p.InfoTypeId=q.InfoTypeId and m.User_Id=p.CreatedBy  and ID=@ID and ProjectUnit='" + j.GrantUnit + "' and Deleted='N' and p.CreatedBy  <>  (Select CreatedBy from ProjectAuxillaryDetails where ProjectUnit='" + j.GrantUnit + "' and ID=@ID and Deleted='N')  order by EntryNo";
                            DSforgridview1.DataBind();
                            GridView1.DataBind();
                            Panel8.Visible = true;
                        }

                        if (Session["Role"].ToString() == "6")
                        {
                            DSforgridview.SelectParameters.Clear();
                            DSforgridview.SelectParameters.Add("ID", j.GID);
                            DSforgridview.SelectParameters.Add("ProjectUnit", j.GrantType);
                            DSforgridview.SelectCommand = "select UploadPDFPath ,q.InfoTypeName as TypeName ,Remark ,AuditFrom,AuditTo, p.CreatedDate, p.CreatedBy as CreatedBy, m.FirstName as AddedBy, EntryNo,Unit_Id ,p.InfoTypeId from ProjectAuxillaryDetails p,Project_AuxInfoTypeM q, User_M m where  p.InfoTypeId=q.InfoTypeId and m.User_Id=p.CreatedBy  and ID=@ID and ProjectUnit=@ProjectUnit and Deleted='N' and p.CreatedBy  <>  (Select CreatedBy from ProjectAuxillaryDetails where ProjectUnit='" + j.GrantUnit + "' and ID=@ID and Deleted='N') order by EntryNo";
                            DSforgridview.DataBind();
                            GridView1.DataBind();
                            Panel8.Visible = true;

                            DSforgridview1.SelectParameters.Clear();
                            DSforgridview1.SelectParameters.Add("ID", j.GID);
                            DSforgridview1.SelectParameters.Add("ProjectUnit", j.GrantType);
                            DSforgridview1.SelectCommand = "select UploadPDFPath ,q.InfoTypeName as TypeName ,Remark ,AuditFrom,AuditTo, p.CreatedDate, p.CreatedBy as CreatedBy, m.FirstName as AddedBy, EntryNo,Unit_Id ,p.InfoTypeId from ProjectAuxillaryDetails p,Project_AuxInfoTypeM q, User_M m where  p.CreatedBy!='" + Session["UserId"].ToString() + "' and p.InfoTypeId=q.InfoTypeId and m.User_Id=p.CreatedBy  and ID=@ID and ProjectUnit=@ProjectUnit and Deleted='N' order by EntryNo";
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
                    if ((Session["Role"].ToString() == "11" || Session["Role"].ToString() == "1" || Session["Role"].ToString()=="20"))
                    {
                        DSforgridview.SelectParameters.Clear();
                        DSforgridview.SelectParameters.Add("ID", j.GID);
                        DSforgridview.SelectParameters.Add("ProjectUnit", j.GrantType);

                        DSforgridview.SelectCommand = "select UploadPDFPath ,q.InfoTypeName as TypeName ,Remark ,AuditFrom,AuditTo, p.CreatedDate, p.CreatedBy as CreatedBy, m.FirstName as AddedBy, EntryNo,Unit_Id ,p.InfoTypeId from ProjectAuxillaryDetails p,Project_AuxInfoTypeM q, User_M m where  p.CreatedBy='" + Session["UserId"].ToString() + "' and p.InfoTypeId=q.InfoTypeId and m.User_Id=p.CreatedBy  and ID=@ID and ProjectUnit=@ProjectUnit and Deleted='N' order by EntryNo";
                        DSforgridview.DataBind();
                        GVViewFile.DataBind();

                        DSforgridview1.SelectParameters.Clear();
                        DSforgridview1.SelectParameters.Add("ID", j.GID);
                        DSforgridview1.SelectParameters.Add("ProjectUnit", j.GrantUnit);
                        DSforgridview1.SelectCommand = "select UploadPDFPath ,q.InfoTypeName as TypeName ,Remark ,AuditFrom,AuditTo, p.CreatedDate, p.CreatedBy as CreatedBy, m.FirstName as AddedBy, EntryNo,Unit_Id ,p.InfoTypeId from ProjectAuxillaryDetails p,Project_AuxInfoTypeM q, User_M m where  p.InfoTypeId=q.InfoTypeId and m.User_Id=p.CreatedBy  and ID=@id and ProjectUnit='" + j.GrantType + "' and Deleted='N' and p.CreatedBy  not in  (Select CreatedBy from ProjectAuxillaryDetails where ProjectUnit=@ProjectUnit and ID=@ID and Deleted='N')  order by EntryNo";
                        DSforgridview1.DataBind();
                        GridView1.DataBind();
                        Panel8.Visible = true;
                    }

                    if (Session["Role"].ToString() == "6")
                    {
                        DSforgridview.SelectParameters.Clear();
                        DSforgridview.SelectParameters.Add("ID", j.GID);
                        DSforgridview.SelectParameters.Add("ProjectUnit", j.GrantType);
                        DSforgridview.SelectCommand = "select UploadPDFPath ,q.InfoTypeName as TypeName ,Remark ,AuditFrom,AuditTo, p.CreatedDate, p.CreatedBy as CreatedBy, m.FirstName as AddedBy, EntryNo,Unit_Id ,p.InfoTypeId from ProjectAuxillaryDetails p,Project_AuxInfoTypeM q, User_M m where  p.InfoTypeId=q.InfoTypeId and m.User_Id=p.CreatedBy  and ID=@ID and ProjectUnit=@ProjectUnit and Deleted='N' and p.CreatedBy  not in  (Select CreatedBy from ProjectAuxillaryDetails where ProjectUnit='" + j.GrantUnit + "' and ID=@ID and Deleted='N')  order by EntryNo";
                        DSforgridview.DataBind();
                        GridView1.DataBind();
                        Panel8.Visible = true;

                        DSforgridview.SelectParameters.Clear();
                        DSforgridview.SelectParameters.Add("ID", j.GID);
                        DSforgridview.SelectParameters.Add("ProjectUnit", j.GrantType);
                        DSforgridview1.SelectCommand = "select UploadPDFPath ,q.InfoTypeName as TypeName ,Remark ,AuditFrom,AuditTo, p.CreatedDate, p.CreatedBy as CreatedBy, m.FirstName as AddedBy, EntryNo,Unit_Id ,p.InfoTypeId from ProjectAuxillaryDetails p,Project_AuxInfoTypeM q, User_M m where  p.CreatedBy!='" + Session["UserId"].ToString() + "' and p.InfoTypeId=q.InfoTypeId and m.User_Id=p.CreatedBy  and ID=@ID and ProjectUnit=@ProjectUnit and Deleted='N' order by EntryNo";
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
                        DSforgridview.SelectParameters.Add("ID", j.GID);
                        DSforgridview.SelectParameters.Add("ProjectUnit", j.GrantType);
                        DSforgridview.SelectCommand = "select UploadPDFPath ,q.InfoTypeName as TypeName ,Remark ,AuditFrom,AuditTo, p.CreatedDate, p.CreatedBy as CreatedBy, m.FirstName as AddedBy, EntryNo,Unit_Id ,p.InfoTypeId from ProjectAuxillaryDetails p,Project_AuxInfoTypeM q, User_M m where  p.CreatedBy='" + Session["UserId"].ToString() + "' and p.InfoTypeId=q.InfoTypeId and m.User_Id=p.CreatedBy  and ID=@ID and ProjectUnit=@ProjectUnit and Deleted='N' order by EntryNo";
                        DSforgridview.DataBind();
                        GVViewFile.DataBind();

                        DSforgridview1.SelectParameters.Clear();
                        DSforgridview1.SelectParameters.Add("ID", j.GID);
                        DSforgridview1.SelectParameters.Add("ProjectUnit", j.GrantType);
                        DSforgridview1.SelectCommand = "select UploadPDFPath ,q.InfoTypeName as TypeName ,Remark ,AuditFrom,AuditTo, p.CreatedDate, p.CreatedBy as CreatedBy, m.FirstName as AddedBy, EntryNo,Unit_Id ,p.InfoTypeId from ProjectAuxillaryDetails p,Project_AuxInfoTypeM q, User_M m where  p.InfoTypeId=q.InfoTypeId and m.User_Id=p.CreatedBy  and ID=@ID and ProjectUnit='" + j.GrantType + "' and Deleted='N' and p.CreatedBy  not in  (Select CreatedBy from ProjectAuxillaryDetails where ProjectUnit='" + j.GrantUnit + "' and ID=@ID and Deleted='N')  order by EntryNo";
                        DSforgridview1.DataBind();
                        GridView1.DataBind();
                        Panel8.Visible = true;
                    }

                    if (Session["Role"].ToString() == "6")
                    {
                        DSforgridview.SelectParameters.Clear();
                        DSforgridview.SelectParameters.Add("ID", j.GID);
                        DSforgridview.SelectParameters.Add("ProjectUnit", j.GrantType);
                        DSforgridview.SelectCommand = "select UploadPDFPath ,q.InfoTypeName as TypeName ,Remark ,AuditFrom,AuditTo, p.CreatedDate, p.CreatedBy as CreatedBy, m.FirstName as AddedBy, EntryNo,Unit_Id ,p.InfoTypeId from ProjectAuxillaryDetails p,Project_AuxInfoTypeM q, User_M m where  p.InfoTypeId=q.InfoTypeId and m.User_Id=p.CreatedBy  and ID=@ID and ProjectUnit=@ProjectUnit and Deleted='N' and p.CreatedBy  not in  (Select CreatedBy from ProjectAuxillaryDetails where ProjectUnit='" + j.GrantUnit + "' and ID=@ID and Deleted='N')  order by EntryNo";
                        //DSforgridview.SelectCommand = "select UploadPDFPath ,q.InfoTypeName as TypeName ,Remark ,AuditFrom,AuditTo, p.CreatedDate, p.CreatedBy as CreatedBy, m.FirstName as AddedBy, EntryNo,Unit_Id ,p.InfoTypeId from ProjectAuxillaryDetails p,Project_AuxInfoTypeM q, User_M m where  p.InfoTypeId=q.InfoTypeId and m.User_Id=p.CreatedBy  and ID='" + j.GID + "' and ProjectUnit='" + j.GrantType + "' and Deleted='N' and p.CreatedBy  not in  (Select CreatedBy from ProjectAuxillaryDetails where ProjectUnit='" + j.GrantUnit + "' and ID='" + j.GID + "' and Deleted='N')  order by EntryNo";
                        DSforgridview.DataBind();
                        GridView1.DataBind();
                        Panel8.Visible = true;

                        DSforgridview1.SelectParameters.Clear();
                        DSforgridview1.SelectParameters.Add("ID", j.GID);
                        DSforgridview1.SelectParameters.Add("ProjectUnit", j.GrantType);
                        DSforgridview1.SelectCommand = "select UploadPDFPath ,q.InfoTypeName as TypeName ,Remark ,AuditFrom,AuditTo, p.CreatedDate, p.CreatedBy as CreatedBy, m.FirstName as AddedBy, EntryNo,Unit_Id ,p.InfoTypeId from ProjectAuxillaryDetails p,Project_AuxInfoTypeM q, User_M m where  p.CreatedBy!='" + Session["UserId"].ToString() + "' and p.InfoTypeId=q.InfoTypeId and m.User_Id=p.CreatedBy  and ID=@ID and ProjectUnit=@ProjectUnit and Deleted='N' order by EntryNo";
                        //DSforgridview1.SelectCommand = "select UploadPDFPath ,q.InfoTypeName as TypeName ,Remark ,AuditFrom,AuditTo, p.CreatedDate, p.CreatedBy as CreatedBy, m.FirstName as AddedBy, EntryNo,Unit_Id ,p.InfoTypeId from ProjectAuxillaryDetails p,Project_AuxInfoTypeM q, User_M m where  p.CreatedBy!='" + Session["UserId"].ToString() + "' and p.InfoTypeId=q.InfoTypeId and m.User_Id=p.CreatedBy  and ID='" + j.GID + "' and ProjectUnit='" + j.GrantType + "' and Deleted='N' order by EntryNo";
                        DSforgridview1.DataBind();
                        GVViewFile.DataBind();
                    }
                }

            }


        }

        confirmValue = "";
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
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

    

}