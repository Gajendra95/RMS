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
public partial class GrantEntry_GrantApproval : System.Web.UI.Page
{
    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

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
            panelReowrkRemarks.Visible = false;
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
        EmployeeCode.Text = Session["UserId"].ToString();

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
            SqlDataSource1.SelectParameters.Clear(); 
            if (PubIDSearch.Text == "" && TextBoxtiltleSearch.Text == "")
            {
                SqlDataSource1.SelectCommand = " select p.ID, r.TypeName,p.ProjectUnit, UTN,Title,Description,CONVERT(DECIMAL(14,0),AppliedAmount) as AppliedAmount,q.StatusName,p.ProjectType from Project p,Status_Project_M q, ProjectType_M r where  p.ProjectType=r.TypeId and p.ProjectStatus=q.StatusId and  ProjectType=@ProjectType and  StatusId='SUB' and  p.IsUploadedProject='Y' ";
            }
            else if (PubIDSearch.Text != "" && TextBoxtiltleSearch.Text == "")
            {
                SqlDataSource1.SelectCommand = " select p.ID, r.TypeName, p.ProjectUnit,UTN,Title,Description,CONVERT(DECIMAL(14,0),AppliedAmount) as AppliedAmount,q.StatusName,p.ProjectType from Project p,Status_Project_M q, ProjectType_M r where p.ProjectType=r.TypeId and p.ProjectStatus=q.StatusId and ProjectType=@ProjectType and  ID like  '%'+@ID+'%' and StatusId='SUB' and p.IsUploadedProject='Y'";
                SqlDataSource1.SelectParameters.Add("ID", PubIDSearch.Text.Trim());
            }
            else if (PubIDSearch.Text == "" && TextBoxtiltleSearch.Text != "")
            {
                SqlDataSource1.SelectCommand = " select p.ID, r.TypeName,p.ProjectUnit, UTN,Title,Description,CONVERT(DECIMAL(14,0),AppliedAmount) as AppliedAmount,q.StatusName,p.ProjectType from Project p,Status_Project_M q, ProjectType_M r where  p.ProjectType=r.TypeId and p.ProjectStatus=q.StatusId and  ProjectType=@ProjectType and   Title like '%'+@Title+'%' and StatusId='SUB' and p.IsUploadedProject='Y'and p.IsUploadedProject='Y'";
                SqlDataSource1.SelectParameters.Add("Title", TextBoxtiltleSearch.Text.Trim());
            }
            else
            {

                SqlDataSource1.SelectCommand = " select p.ID, r.TypeName, p.ProjectUnit,UTN,Title,Description,CONVERT(DECIMAL(14,0),AppliedAmount) as AppliedAmount,q.StatusName,p.ProjectType from Project p,Status_Project_M q, ProjectType_M r where  p.ProjectType=r.TypeId and p.ProjectStatus=q.StatusId and  ProjectType=@ProjectType and  ID like  '%'+@ID+'%' and Title like '%'+@Title+'%' and StatusId='SUB' and p.IsUploadedProject='Y' ";
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
                SqlDataSource1.SelectCommand = "  select p.ID, r.TypeName,p.ProjectUnit, UTN,Title,Description,CONVERT(DECIMAL(14,0),AppliedAmount) as AppliedAmount,q.StatusName,p.ProjectType from Project p,Status_Project_M q, ProjectType_M r where  p.ProjectType=r.TypeId and p.ProjectStatus=q.StatusId   and StatusId='SUB' and p.IsUploadedProject='Y' ";
            }
            else if (PubIDSearch.Text != "" && TextBoxtiltleSearch.Text == "")
            {
                SqlDataSource1.SelectCommand = " select p.ID, r.TypeName,p.ProjectUnit, UTN,Title,Description,CONVERT(DECIMAL(14,0),AppliedAmount) as AppliedAmount,q.StatusName,p.ProjectType from Project p,Status_Project_M q, ProjectType_M r where  p.ProjectType=r.TypeId and p.ProjectStatus=q.StatusId and  ID like '%'+@ID+'%'  and StatusId='SUB' and p.IsUploadedProject='Y'";
                SqlDataSource1.SelectParameters.Add("ID", PubIDSearch.Text.Trim());
            }
            else if (PubIDSearch.Text == "" && TextBoxtiltleSearch.Text != "")
            {
                SqlDataSource1.SelectCommand = " select p.ID, r.TypeName,p.ProjectUnit, UTN,Title,Description,CONVERT(DECIMAL(14,0),AppliedAmount) as AppliedAmount,q.StatusName,p.ProjectType from Project p,Status_Project_M q, ProjectType_M r where  p.ProjectType=r.TypeId and p.ProjectStatus=q.StatusId and  Title like '%'+@Title+'%' and StatusId='SUB' and p.IsUploadedProject='Y'";
                SqlDataSource1.SelectParameters.Add("Title", TextBoxtiltleSearch.Text.Trim());
        
            }
            else
            {

                SqlDataSource1.SelectCommand = " select p.ID, r.TypeName,p.ProjectUnit, UTN,Title,Description,CONVERT(DECIMAL(14,0),AppliedAmount) as AppliedAmount,q.StatusName,p.ProjectType from Project p,Status_Project_M q, ProjectType_M r where  p.ProjectType=r.TypeId and p.ProjectStatus=q.StatusId and  ID like '%'+@ID+'%' and Title like '%'+@Title+'%' and StatusId='SUB' and p.IsUploadedProject='Y' ";

                // SqlDataSource1.SelectCommand = "select j.Id as Id,Year,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,Comments from Journal_IF_Details j,Journal_M k where j.Id=k.Id and j.Id='" + txtJid.Text.Trim() + "'";
                SqlDataSource1.SelectParameters.Add("ID", PubIDSearch.Text.Trim());
                SqlDataSource1.SelectParameters.Add("Title", TextBoxtiltleSearch.Text.Trim());
            }
            GridViewSearchGrant.DataBind();
            SqlDataSource1.DataBind();
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
        txtRework.Text = "";

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

        BtnRework.Enabled = true;
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
        if (v.Status != "")
        {
            DropDownListProjStatus.SelectedValue = v.Status;
            if (DropDownListProjStatus.SelectedValue == "SUB")
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
        }
        //panelCanelRemarks.Visible = true;
        //TextBoxRemarks.Text = v.RejectFeedback;
        DropDownListProjStatus.Items.Clear();
        //  TextBoxCurStatus.Text = CurStatus;

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
        //if (v.ProjectActualDate.ToShortDateString() != "01/01/0001")
        //{
        //    txtprojectactualdate.Text = v.ProjectActualDate.ToShortDateString();
        //}
        if (v.DurationOfProject != 0)
        {
            txtProjectDuration.Text = v.DurationOfProject.ToString();
        }
        txtEmailId.Text = v.AgencyEmailId;
        txtagencycontact.Text = v.AgencyContact;
        DropDownAgencyType.SelectedValue = v.TypeofAgencyGrant.ToString();
        DropDownSectorLevel.SelectedValue = v.FundingSectorLevelGrant.ToString();

        DSforgridview.SelectParameters.Clear();
        DSforgridview.SelectParameters.Add("Pid", Pid);
        DSforgridview.SelectParameters.Add("projectunit1", projectunit1);

        DSforgridview.SelectCommand = "select UploadPDFPath ,q.InfoTypeName as TypeName ,Remark ,AuditFrom,AuditTo, p.CreatedDate, p.CreatedBy as CreatedBy, m.FirstName as AddedBy, EntryNo,Unit_Id ,p.InfoTypeId from ProjectAuxillaryDetails p,Project_AuxInfoTypeM q, User_M m where  p.InfoTypeId=q.InfoTypeId and m.User_Id=p.CreatedBy  and ID=@Pid   and ProjectUnit=@projectunit1   and Deleted='N' order by EntryNo";
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
                LabelSanType.Visible = true;
                DropDownListSanType.Visible = true;

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
        panelReowrkRemarks.Visible = true;
        BtnRework.Enabled = true;
        BtnSanction.Enabled = true;
        PanelUploaddetails.Visible = true;

    }
    protected void OnRowDataBound1(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList DropdownMuNonMu = (e.Row.FindControl("DropdownMuNonMu") as DropDownList);
            SqlDataSourceAuthorType.SelectCommand = "SELECT Id,DisplayName FROM [Author_Type_M] where (Id = 'M') or (Id = 'S') or (Id = 'N') or (Id = 'O') or (Id = 'E')";

            DropdownMuNonMu.DataTextField = "DisplayName";
            DropdownMuNonMu.DataValueField = "Id";
            DropdownMuNonMu.DataBind();
        }

    }
    protected void GVViewFile_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        ImageButton EditButton = (ImageButton)e.Row.FindControl("BtnEdit");
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


    protected void BtnRework_Click(object sender, EventArgs e)
    {
        if (txtRework.Text == "")
        {
            ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Pleasse Enter the Remarks!!!!')</script>");
            return;
        }
        else
        {
            Business obj = new Business();
            GrantData j = new GrantData();
            j.GID = TextBoxID.Text;
            j.Status = "REW";
            j.Remarks = txtRework.Text.Trim();
            j.GrantUnit = DropDownListGrUnit.SelectedValue;

            bool result = obj.UpdateStatusReworkGrantEntry(j);
            if (result == true)
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Project is sent for Rework')</script>");
                BtnRework.Enabled = false;
                BtnSanction.Enabled = false;
                EmailDetails details = new EmailDetails();
                details = SendMail();
                details.Id = TextBoxID.Text;
                details.Type = DropDownListTypeGrant.SelectedItem.ToString();
                details.ProjectUnit = DropDownListGrUnit.SelectedItem.ToString();
                details.UnitId = DropDownListGrUnit.SelectedValue.ToString();
                SendMailObject obj1 = new SendMailObject();
                bool result1 = obj1.InsertIntoEmailQueue(details);
            }
            else
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Grant Error')</script>");
            }
        }

    }

    private EmailDetails SendMail()
    {
        log.Debug("Grant Approval: Inside Send Mail for Rework Project of type: " + DropDownListTypeGrant.SelectedValue + " ID: " + TextBoxID.Text + " Project Unit: " + DropDownListGrUnit.Text);
        EmailDetails details = new EmailDetails();

        ArrayList myArrayListGrantAuthor = new ArrayList();
        ArrayList myArrayListInvestigatorNAme = new ArrayList();
        ArrayList myArrayListReserachCoOrdinator = new ArrayList();
        ArrayList myArrayListFinanceTeam = new ArrayList();
        ArrayList myArrayListCoAuthor = new ArrayList();
        DataSet ds = new DataSet();
        Business bus = new Business();

        ds = bus.getGrantAuthorList(TextBoxID.Text, DropDownListGrUnit.SelectedValue);
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            myArrayListGrantAuthor.Add(ds.Tables[0].Rows[i]["EmailId"].ToString());
        }

        DataSet dscoauthor = bus.getGrantCOAuthorList(TextBoxID.Text, DropDownListGrUnit.SelectedValue);

        for (int i = 0; i < dscoauthor.Tables[0].Rows.Count; i++)
        {
            myArrayListCoAuthor.Add(dscoauthor.Tables[0].Rows[i]["EmailId"].ToString());
        }

        DataSet ds1 = new DataSet();
        ds1 = bus.getReserachCoOrdinator(TextBoxID.Text, DropDownListGrUnit.SelectedValue);
        for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
        {
            myArrayListReserachCoOrdinator.Add(ds1.Tables[0].Rows[i]["EmailId"].ToString());
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

        details.EmailSubject = "Project Entry <  " + DropDownListTypeGrant.SelectedValue + " _ " + TextBoxID.Text + "  > Rework ";
        details.MsgBody = "<span style=\"font-size: 9pt; color: #3300cc; font-family: Verdana\"><h4>Dear Sir/Madam,</h4> <br>" +
          "<b> The following Project Entry needs to be rework : <br> " +
              "<br>" +
                "Project Type  : " + DropDownListTypeGrant.SelectedItem + "<br>" +
             "Project Id  :  " + TextBoxID.Text + "<br>" +
             "UTN  :  " + TextBoxUTN.Text + "<br>" +
             "Title  : " + TextBoxTitle.Text + "<br>" +

             "Added By  : " + myArrayListInvestigatorNAme[0].ToString() + "<br>" +
             "Investigators  : " + auhtorsSConc + "<br>" + "<br>" + "<br>" + "<br>" + "<br>" + "<br>" + FooterText +
             "</span>";


        details.FromEmail = ConfigurationManager.AppSettings["FromAddress"].ToString();
        details.Module = "GREW";

        for (int i = 0; i < myArrayListGrantAuthor.Count; i++)
        {

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
            log.Info(" Email will be sent to author '" + i + "' : '" + myArrayListGrantAuthor[i] + "' ");
        }

        for (int i = 0; i < myArrayListReserachCoOrdinator.Count; i++)
        {
            //Msg.CC.Add(myArrayListReserachCoOrdinator[i].ToString());
            //string email = myArrayListReserachCoOrdinator[i].ToString();
            if (details.CCEmail != null)
            {
                details.CCEmail = details.CCEmail + ',' + myArrayListReserachCoOrdinator[i].ToString();
            }
            else
            {
                if (i == 0)
                {
                    details.CCEmail = myArrayListReserachCoOrdinator[i].ToString();
                }
                else
                {
                    details.CCEmail = details.CCEmail + ',' + myArrayListReserachCoOrdinator[i].ToString();
                }
            }
            // details.CCEmail = email;
            log.Info(" Email will be sent to research cordinator '" + i + "' : '" + myArrayListReserachCoOrdinator[i] + "' ");
        }

        for (int i = 0; i < myArrayListCoAuthor.Count; i++)
        {
            // Msg.CC.Add(myArrayListCoAuthor[i].ToString());
            string email = myArrayListCoAuthor[i].ToString();
            if (details.CCEmail != null)
            {
                details.CCEmail = details.CCEmail + ',' + myArrayListCoAuthor[i].ToString();
            }
            else
            {
                if (i == 0)
                {
                    details.CCEmail = myArrayListCoAuthor[i].ToString();
                }
                else
                {
                    details.CCEmail = details.CCEmail + ',' + myArrayListCoAuthor[i].ToString();
                }
            }
            log.Info(" Email will be sent to co-authors'" + i + "' : '" + myArrayListCoAuthor[i] + "' ");
        }

        return details;

    }
    protected void BtnSanction_Click(object sender, EventArgs e)
    {
        Business obj = new Business();
        GrantData j = new GrantData();
        j.GID = TextBoxID.Text;
        j.Status = "SAN";
        j.Remarks = txtRework.Text.Trim();
        j.GrantUnit = DropDownListGrUnit.SelectedValue;
        j.FinanceProjectStatus = "OPE";

        bool result = obj.UpdateStatusReworkGrantEntry(j);
        if (result == true)
        {
            ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Grant Sanction Data Approved Successfully')</script>");
            BtnRework.Enabled = false;
            BtnSanction.Enabled = false;
            EmailDetails details = new EmailDetails();
            details = SendMailApprove();
            details.Id = TextBoxID.Text;
            details.Type = DropDownListTypeGrant.SelectedItem.ToString();
            details.ProjectUnit = DropDownListGrUnit.SelectedItem.ToString();
            details.UnitId = DropDownListGrUnit.SelectedValue.ToString();
            SendMailObject obj1 = new SendMailObject();
            bool result1 = obj1.InsertIntoEmailQueue(details);
        }
        else
        {
            ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Grant Error')</script>");
        }
    }



    private EmailDetails SendMailApprove()
    {
        log.Debug("Grant Approval: Inside Send Mail for Approval Project of type: " + DropDownListTypeGrant.SelectedValue + " ID: " + TextBoxID.Text + " Project Unit: " + DropDownListGrUnit.Text);
        EmailDetails details = new EmailDetails();
        if (DropDownListSanType.SelectedValue == "CA")
        {
            ArrayList myArrayListInvestigator = new ArrayList();
            ArrayList myArrayListInvestigatorNAme = new ArrayList();
            ArrayList myArrayListReserachCoOrdinator = new ArrayList();
            ArrayList myArrayListFinanceTeam = new ArrayList();
            DataSet ds = new DataSet();
            Business bus = new Business();

            ds = bus.getInvetigatorList(TextBoxID.Text, DropDownListGrUnit.SelectedValue);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                myArrayListInvestigator.Add(ds.Tables[0].Rows[i]["EmailId"].ToString());
            }

            DataSet ds1 = new DataSet();
            ds1 = bus.getReserachCoOrdinator(TextBoxID.Text, DropDownListGrUnit.SelectedValue);
            for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
            {
                myArrayListReserachCoOrdinator.Add(ds1.Tables[0].Rows[i]["EmailId"].ToString());
            }

            DataSet dy = new DataSet();
            dy = bus.getInvietigatorListName(TextBoxID.Text, DropDownListGrUnit.SelectedValue);
            for (int i = 0; i < dy.Tables[0].Rows.Count; i++)
            {
                myArrayListInvestigatorNAme.Add(dy.Tables[0].Rows[i]["InvestigatorName"].ToString());
            }

            DataSet ds3 = new DataSet();
            ds3 = bus.getReserachFinanceList();
            for (int i = 0; i < ds3.Tables[0].Rows.Count; i++)
            {
                myArrayListFinanceTeam.Add(ds3.Tables[0].Rows[i]["EmailId"].ToString());
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
             "<b> The following Project Entry has been Approved in Project Repository : <br> " +
                 "<br>" +
                   "Project Type  : " + DropDownListTypeGrant.SelectedItem + "<br>" +
                "Project Id  :  " + TextBoxID.Text + "<br>" +
                  "UTN  :  " + TextBoxUTN.Text + "<br>" +
                "Title  : " + TextBoxTitle.Text + "<br>" +
                "Added By  : " + myArrayListInvestigatorNAme[0].ToString() + "<br>" +
                "Investigators  : " + auhtorsSConc + "<br>" + "<br>" + "<br>" + "<br>" + "<br>" + FooterText +
                "</span>";


            details.FromEmail = ConfigurationManager.AppSettings["FromAddress"].ToString();
            details.Module = "GSAN";
            for (int i = 0; i < myArrayListReserachCoOrdinator.Count; i++)
            {
                if (details.CCEmail != null)
                {
                    details.CCEmail = details.CCEmail + ',' + myArrayListReserachCoOrdinator[i].ToString();
                }
                else
                {
                    if (i == 0)
                    {
                        details.CCEmail = myArrayListReserachCoOrdinator[i].ToString();
                    }
                    else
                    {
                        details.CCEmail = details.CCEmail + ',' + myArrayListReserachCoOrdinator[i].ToString();
                    }
                }
                //details.CCEmail = email;
                log.Info(" Email will be sent to Research CoOrdinator '" + i + "' : '" + myArrayListReserachCoOrdinator[i] + "' ");
            }
            for (int i = 0; i < myArrayListInvestigator.Count; i++)
            {
                //string email = myArrayListInvestigator[i].ToString();
                if (details.CCEmail != null)
                {
                    details.CCEmail = details.CCEmail + ',' + myArrayListInvestigator[i].ToString();
                }
                else
                {
                    if (i == 0)
                    {
                        details.CCEmail = myArrayListInvestigator[i].ToString();
                    }
                    else
                    {
                        details.CCEmail = details.CCEmail + ',' + myArrayListInvestigator[i].ToString();
                    }
                    //details.CCEmail = details.CCEmail + ',' + email;
                }
                //Msg.CC.Add(myArrayListInvestigator[i].ToString());
                log.Info(" Email will be sent to Investigators '" + i + "' : '" + myArrayListInvestigator[i] + "' ");
            }
            for (int i = 0; i < myArrayListFinanceTeam.Count; i++)
            {
                // Msg.To.Add(myArrayListFinanceTeam[i].ToString());
                //string email = myArrayListFinanceTeam[i].ToString();
                if (details.ToEmail != null)
                {
                    details.ToEmail = details.ToEmail + ',' + myArrayListFinanceTeam[i].ToString();
                }
                else
                {
                    if (i == 0)
                    {
                        details.ToEmail = myArrayListFinanceTeam[i].ToString();
                    }
                    else
                    {
                        details.ToEmail = details.ToEmail + ',' + myArrayListFinanceTeam[i].ToString();
                    }
                }
                //details.ToEmail = email;
                log.Info(" Email will be sent to Research Finance team '" + i + "' : '" + myArrayListFinanceTeam[i] + "' ");
            }

        }
        return details;
    }
}