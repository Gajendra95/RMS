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

public partial class Grantentry : System.Web.UI.Page
{
    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    string mainpath = ConfigurationManager.AppSettings["FolderPathProject"].ToString();
    Business B = new Business();
    Journal_DataObject JournalDataObj = new Journal_DataObject();
    JournalData JournalValueObj = new JournalData();
    //public string pageID = "L42";
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Form.Attributes.Add("enctype", "multipart/form-data"); 
        if (!IsPostBack)
        {
            string EnableProjectUpload = ConfigurationManager.AppSettings["EnableProjectUpload"].ToString();
            if (EnableProjectUpload == "Y")
            {
                Panel7.Visible = false;
                PanelOPAmount.Visible = false;
                PanelAmount.Visible = false;
                btnAddProject.Visible = false;
            }
            else
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
                    PanelProjectOutcome.Visible = false;
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
                    PanelProjectOutcome.Visible = false;
                }

                setModalWindowAgency(sender, e);
                setModalWindow(sender, e);
                SetInitialRow();
                SanctionSetInitialRow();
                SanctionsetinitialRowPercentage();
            }

            popupstudent.Visible = false;
            popupPanelAffil.Visible = false;
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
    private void SanctionsetinitialRowPercentage()
    {
        DataTable dt1 = new DataTable();
        DataRow dr1 = null;

        dt1.Columns.Add(new DataColumn("DropdownMuNonMuIO", typeof(string)));
        dt1.Columns.Add(new DataColumn("InstitutionNameIO", typeof(string)));
        dt1.Columns.Add(new DataColumn("PercentageIO", typeof(string)));
        dt1.Columns.Add(new DataColumn("InstitutionIO", typeof(string)));
        //dt1.Columns.Add(new DataColumn("DepartmentNameIO", typeof(string)));
        //dt1.Columns.Add(new DataColumn("DepartmentIO", typeof(string)));
        dr1 = dt1.NewRow();

        dr1["DropdownMuNonMuIO"] = string.Empty;
        dr1["InstitutionNameIO"] = string.Empty;
        dr1["PercentageIO"] = string.Empty;
        dr1["InstitutionIO"] = string.Empty;

        dt1.Rows.Add(dr1);

        ViewState["CurrentTableIO"] = dt1;
        GridViewInterOrganization.DataSource = dt1;
        GridViewInterOrganization.DataBind();

        DataTable dt2 = new DataTable();
        DataRow dr2 = null;

        dt2.Columns.Add(new DataColumn("DropdownMuNonMuII", typeof(string)));
        dt2.Columns.Add(new DataColumn("InstitutionNameII", typeof(string)));
        dt2.Columns.Add(new DataColumn("DepartmentNameII", typeof(string)));
        dt2.Columns.Add(new DataColumn("PercentageII", typeof(string)));
        dt2.Columns.Add(new DataColumn("InstitutionII", typeof(string)));
        dt2.Columns.Add(new DataColumn("DepartmentII", typeof(string)));
        dr2 = dt2.NewRow();

        dr2["DropdownMuNonMuII"] = string.Empty;
        dr2["InstitutionNameII"] = string.Empty;
        dr2["DepartmentNameII"] = string.Empty;
        dr2["PercentageII"] = string.Empty;
        dr2["InstitutionII"] = string.Empty;
        dr2["DepartmentII"] = string.Empty;
        dt2.Rows.Add(dr2);

        ViewState["CurrentTableII"] = dt2;
        GridViewInterInstitute.DataSource = dt2;
        GridViewInterInstitute.DataBind();
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
        if (role == "11" || role == "1")
        {
            SqlDataSource1.SelectParameters.Clear(); 
            if (EntryTypesearch.SelectedValue != "A")
            {
                if (PubIDSearch.Text == "" && TextBoxtiltleSearch.Text == "")
                {
                    SqlDataSource1.SelectCommand = " select p.ID, r.TypeName,p.ProjectUnit, UTN,Title,Description,CONVERT(DECIMAL(14,0),AppliedAmount) as AppliedAmount ,q.StatusName,p.ProjectType from Project p,Status_Project_M q, ProjectType_M r where  p.ProjectType=r.TypeId and p.ProjectStatus=q.StatusId and  ProjectType=@ProjectType and (CreatedBy=@UserID or (p.ID+p.ProjectUnit in(Select  ID+ProjectUnit from Projectnvestigator where InvestigatorType='P' and EmployeeCode=@UserID))) and (StatusId='APP' or StatusId='SAN') ";
                }
                else if (PubIDSearch.Text != "" && TextBoxtiltleSearch.Text == "")
                {
                    SqlDataSource1.SelectCommand = " select p.ID, r.TypeName, p.ProjectUnit,UTN,Title,Description,CONVERT(DECIMAL(14,0),AppliedAmount) as AppliedAmount,q.StatusName,p.ProjectType from Project p,Status_Project_M q, ProjectType_M r where p.ProjectType=r.TypeId and p.ProjectStatus=q.StatusId and ProjectType=@ProjectType and (CreatedBy=@UserID or (p.ID+p.ProjectUnit in(Select  ID+ProjectUnit from Projectnvestigator where InvestigatorType='P' and EmployeeCode=@UserID))) and ID like '%'+@ID+'%'   and (StatusId='APP' or StatusId='SAN')  ";
                    SqlDataSource1.SelectParameters.Add("ID", PubIDSearch.Text.Trim());


                }
                else if (PubIDSearch.Text == "" && TextBoxtiltleSearch.Text != "")
                {
                    SqlDataSource1.SelectCommand = " select p.ID, r.TypeName,p.ProjectUnit, UTN,Title,Description,CONVERT(DECIMAL(14,0),AppliedAmount) as AppliedAmount,q.StatusName,p.ProjectType from Project p,Status_Project_M q, ProjectType_M r where  p.ProjectType=r.TypeId and p.ProjectStatus=q.StatusId and  ProjectType=@ProjectType and (CreatedBy=@UserID or (p.ID+p.ProjectUnit in(Select  ID+ProjectUnit from Projectnvestigator where InvestigatorType='P' and EmployeeCode=@UserID)))  and Title like '%'+@Title+'%' and (StatusId='APP' or StatusId='SAN')  ";
                    SqlDataSource1.SelectParameters.Add("Title", TextBoxtiltleSearch.Text.Trim());
                }
                else
                {

                    SqlDataSource1.SelectCommand = " select p.ID, r.TypeName, p.ProjectUnit,UTN,Title,Description,CONVERT(DECIMAL(14,0),AppliedAmount) as AppliedAmount,q.StatusName,p.ProjectType from Project p,Status_Project_M q, ProjectType_M r where  p.ProjectType=r.TypeId and p.ProjectStatus=q.StatusId and  ProjectType=@ProjectType and (CreatedBy=@UserID or (p.ID+p.ProjectUnit in(Select  ID+ProjectUnit from Projectnvestigator where InvestigatorType='P' and EmployeeCode=@UserID))) and ID like '%'+@ID+'%'  and Title like '%'+@Title+'%' and (StatusId='APP' or StatusId='SAN')  ";
                    SqlDataSource1.SelectParameters.Add("ID", PubIDSearch.Text.Trim());
                    SqlDataSource1.SelectParameters.Add("Title", TextBoxtiltleSearch.Text.Trim());
                }
                SqlDataSource1.SelectParameters.Add("ProjectType", EntryTypesearch.SelectedValue);
                SqlDataSource1.SelectParameters.Add("UserID", Session["UserId"].ToString());

                GridViewSearchGrant.DataBind();
                SqlDataSource1.DataBind();
            }
            else
            {
                SqlDataSource1.SelectParameters.Clear(); 
                if (PubIDSearch.Text == "" && TextBoxtiltleSearch.Text == "")
                {
                    SqlDataSource1.SelectCommand = " select p.ID, r.TypeName,p.ProjectUnit, UTN,Title,Description,CONVERT(DECIMAL(14,0),AppliedAmount) as AppliedAmount,q.StatusName,p.ProjectType from Project p,Status_Project_M q, ProjectType_M r where  p.ProjectType=r.TypeId and p.ProjectStatus=q.StatusId and (CreatedBy=@UserID or (p.ID+p.ProjectUnit in(Select  ID+ProjectUnit from Projectnvestigator where InvestigatorType='P' and EmployeeCode=@UserID)))  and (StatusId='APP' or StatusId='SAN')  ";
                }
                else if (PubIDSearch.Text != "" && TextBoxtiltleSearch.Text == "")
                {
                    SqlDataSource1.SelectCommand = " select p.ID, r.TypeName,p.ProjectUnit, UTN,Title,Description,CONVERT(DECIMAL(14,0),AppliedAmount) as AppliedAmount,q.StatusName,p.ProjectType from Project p,Status_Project_M q, ProjectType_M r where  p.ProjectType=r.TypeId and p.ProjectStatus=q.StatusId and (CreatedBy=@UserID or (p.ID+p.ProjectUnit in(Select  ID+ProjectUnit from Projectnvestigator where InvestigatorType='P' and EmployeeCode=@UserID))) and ID like '%'+@ID+'%'  and (StatusId='APP' or StatusId='SAN') ";
                    SqlDataSource1.SelectParameters.Add("ID", PubIDSearch.Text.Trim());
                }
                else if (PubIDSearch.Text == "" && TextBoxtiltleSearch.Text != "")
                {
                    SqlDataSource1.SelectCommand = " select p.ID, r.TypeName,p.ProjectUnit, UTN,Title,Description,CONVERT(DECIMAL(14,0),AppliedAmount) as AppliedAmount,q.StatusName,p.ProjectType from Project p,Status_Project_M q, ProjectType_M r where  p.ProjectType=r.TypeId and p.ProjectStatus=q.StatusId and (CreatedBy=@UserID or (p.ID+p.ProjectUnit in(Select  ID+ProjectUnit from Projectnvestigator where InvestigatorType='P' and EmployeeCode=@UserID))) and Title like '%'+@Title+'%' and (StatusId='APP' or StatusId='SAN') ";
                    SqlDataSource1.SelectParameters.Add("Title", TextBoxtiltleSearch.Text.Trim());
          
                }
                else
                {

                    SqlDataSource1.SelectCommand = " select p.ID, r.TypeName,p.ProjectUnit, UTN,Title,Description,CONVERT(DECIMAL(14,0),AppliedAmount) as AppliedAmount,q.StatusName,p.ProjectType from Project p,Status_Project_M q, ProjectType_M r where  p.ProjectType=r.TypeId and p.ProjectStatus=q.StatusId and (CreatedBy=@UserID or (p.ID+p.ProjectUnit in(Select  ID+ProjectUnit from Projectnvestigator where InvestigatorType='P' and EmployeeCode=@UserID))) and ID like '%'+@ID+'%' and Title like '%'+@Title+'%' and (StatusId='APP' or StatusId='SAN')   ";
                    SqlDataSource1.SelectParameters.Add("Title", TextBoxtiltleSearch.Text.Trim());
                    SqlDataSource1.SelectParameters.Add("ID", PubIDSearch.Text.Trim());
               
                }
                SqlDataSource1.SelectParameters.Add("UserID", Session["UserId"].ToString());

                GridViewSearchGrant.DataBind();
                SqlDataSource1.DataBind();
            }
        }
        else if (role == "16")
        {
            SqlDataSource2.SelectParameters.Clear(); 
            if (EntryTypesearch.SelectedValue != "A")
            {
                if (PubIDSearch.Text == "" && TextBoxtiltleSearch.Text == "")
                {
                    SqlDataSource2.SelectCommand = " select Top 10  p.ID, r.TypeName,p.ProjectUnit, UTN,Title,Description,CONVERT(DECIMAL(14,0),AppliedAmount) as AppliedAmount,  p.SanctionType,q.StatusName,p.ProjectType from Project p,Status_Project_M q, ProjectType_M r where  p.ProjectType=r.TypeId and p.ProjectStatus=q.StatusId and StatusId='SAN' and SanctionType='CA' and ProjectType=@ProjectType";
                }
                else if (PubIDSearch.Text != "" && TextBoxtiltleSearch.Text == "")
                {
                    SqlDataSource2.SelectCommand = " select Top 10 p.ID, r.TypeName,p.ProjectUnit, UTN,Title,Description,CONVERT(DECIMAL(14,0),AppliedAmount) as AppliedAmount,  p.SanctionType,q.StatusName,p.ProjectType from Project p,Status_Project_M q, ProjectType_M r where  p.ProjectType=r.TypeId and p.ProjectStatus=q.StatusId and StatusId='SAN' and SanctionType='CA'  and ProjectType=@ProjectType and ID like '%'+ @ID + '%' ";
                    SqlDataSource2.SelectParameters.Add("ID", PubIDSearch.Text.Trim());
                }
                else if (PubIDSearch.Text == "" && TextBoxtiltleSearch.Text != "")
                {
                    SqlDataSource2.SelectCommand = "  select Top 10 p.ID, r.TypeName,p.ProjectUnit, UTN,Title,Description,CONVERT(DECIMAL(14,0),AppliedAmount) as AppliedAmount,  p.SanctionType,q.StatusName,p.ProjectType from Project p,Status_Project_M q, ProjectType_M r where  p.ProjectType=r.TypeId and p.ProjectStatus=q.StatusId and StatusId='SAN' and SanctionType='CA' and ProjectType=@ProjectType   and Title like '%'+@Title+'%'";
                    SqlDataSource2.SelectParameters.Add("Title", TextBoxtiltleSearch.Text.Trim());
              
                }
                else
                {

                    SqlDataSource2.SelectCommand = "  select Top 10 p.ID, r.TypeName,p.ProjectUnit, UTN,Title,Description,CONVERT(DECIMAL(14,0),AppliedAmount) as AppliedAmount,  p.SanctionType,q.StatusName,p.ProjectType from Project p,Status_Project_M q, ProjectType_M r where  p.ProjectType=r.TypeId and p.ProjectStatus=q.StatusId and StatusId='SAN' and SanctionType='CA' and ProjectType=@ProjectType  and ID like '%'+ @ID + '%' and Title like '%'+@Title+'%'";
                    SqlDataSource2.SelectParameters.Add("Title", TextBoxtiltleSearch.Text.Trim());
                    SqlDataSource2.SelectParameters.Add("ID", PubIDSearch.Text.Trim());
                }
                SqlDataSource2.SelectParameters.Add("ProjectType", EntryTypesearch.SelectedValue);
                GridViewsanSearch.DataBind();
                SqlDataSource2.DataBind();
            }


            else
            {
                if (PubIDSearch.Text == "" && TextBoxtiltleSearch.Text == "")
                {
                    SqlDataSource2.SelectCommand = "  select p.ID, r.TypeName,p.ProjectUnit, UTN,Title,Description,CONVERT(DECIMAL(14,0),AppliedAmount) as AppliedAmount,  p.SanctionType,q.StatusName,p.ProjectType from Project p,Status_Project_M q, ProjectType_M r where  p.ProjectType=r.TypeId and p.ProjectStatus=q.StatusId and SanctionType='CA' and StatusId='SAN'";
                }
                else if (PubIDSearch.Text != "" && TextBoxtiltleSearch.Text == "")
                {
                    SqlDataSource2.SelectCommand = "  select Top 10 p.ID, r.TypeName,p.ProjectUnit, UTN,Title,Description,CONVERT(DECIMAL(14,0),AppliedAmount) as AppliedAmount,  p.SanctionType,q.StatusName,p.ProjectType from Project p,Status_Project_M q, ProjectType_M r where  p.ProjectType=r.TypeId and p.ProjectStatus=q.StatusId  and SanctionType='CA' and StatusId='SAN' and ID like '%'+@ID+'%' ";
                    SqlDataSource2.SelectParameters.Add("ID", PubIDSearch.Text.Trim());
                }
                else if (PubIDSearch.Text == "" && TextBoxtiltleSearch.Text != "")
                {
                    SqlDataSource2.SelectCommand = "  select Top 10 p.ID, r.TypeName, p.ProjectUnit,UTN,Title,Description,CONVERT(DECIMAL(14,0),AppliedAmount) as AppliedAmount,  p.SanctionType,q.StatusName,p.ProjectType from Project p,Status_Project_M q, ProjectType_M r where  p.ProjectType=r.TypeId and p.ProjectStatus=q.StatusId and StatusId='SAN' and SanctionType='CA'   and Title like '%'+@Title+'%' ";
                    SqlDataSource2.SelectParameters.Add("Title", TextBoxtiltleSearch.Text.Trim());
                }
                else
                {

                    SqlDataSource2.SelectCommand = " select Top 10 p.ID, r.TypeName,p.ProjectUnit, UTN,Title,Description,CONVERT(DECIMAL(14,0),AppliedAmount) as AppliedAmount,  p.SanctionType,q.StatusName,p.ProjectType from Project p,Status_Project_M q, ProjectType_M r where  p.ProjectType=r.TypeId and p.ProjectStatus=q.StatusId and StatusId='SAN' and SanctionType='CA' and CreatedBy=@UserID and ID like '%'+@ID+'%' and Title like '%'+@Title+'%'";
                    SqlDataSource2.SelectParameters.Add("ID", PubIDSearch.Text.Trim());
                    SqlDataSource2.SelectParameters.Add("Title", TextBoxtiltleSearch.Text.Trim());
                    SqlDataSource2.SelectParameters.Add("UserID", Session["UserId"].ToString());
                }
                GridViewsanSearch.DataBind();
                SqlDataSource2.DataBind();
            }
        }
        else
        {
            SqlDataSource2.SelectParameters.Clear(); 
            if (EntryTypesearch.SelectedValue != "A")
            {
                if (PubIDSearch.Text == "" && TextBoxtiltleSearch.Text == "")
                {
                    SqlDataSource2.SelectCommand = " select Top 10  p.ID, r.TypeName,p.ProjectUnit, UTN,Title,Description,CONVERT(DECIMAL(14,0),AppliedAmount) as AppliedAmount,  p.SanctionType,q.StatusName,p.ProjectType from Project p,Status_Project_M q, ProjectType_M r where  p.ProjectType=r.TypeId and p.ProjectStatus=q.StatusId and StatusName='Sanctioned' and SanctionType='CA' and ProjectType=@ProjectType and StatusId!='CAN' and StatusId!='REJ' and StatusId!='CLO' and ProjectUnit=@ProjectUnit and InstitutionID in (Select Institute_Id from User_Institution_Map where User_Id=@UserID)";
                }
                else if (PubIDSearch.Text != "" && TextBoxtiltleSearch.Text == "")
                {
                    SqlDataSource2.SelectCommand = " select Top 10 p.ID, r.TypeName,p.ProjectUnit, UTN,Title,Description,CONVERT(DECIMAL(14,0),AppliedAmount) as AppliedAmount,  p.SanctionType,q.StatusName,p.ProjectType from Project p,Status_Project_M q, ProjectType_M r where  p.ProjectType=r.TypeId and p.ProjectStatus=q.StatusId and StatusName='Sanctioned' and SanctionType='CA'  and ProjectType=@ProjectType and ID like'%'+@ID+'%'   and StatusId!='CAN' and StatusId!='REJ' and StatusId!='CLO' and ProjectUnit=@ProjectUnit and InstitutionID in (Select Institute_Id from User_Institution_Map where User_Id=@UserID) ";
                    SqlDataSource2.SelectParameters.Add("ID", PubIDSearch.Text.Trim());
                }
                else if (PubIDSearch.Text == "" && TextBoxtiltleSearch.Text != "")
                {
                    SqlDataSource2.SelectCommand = "  select Top 10 p.ID, r.TypeName,p.ProjectUnit, UTN,Title,Description,CONVERT(DECIMAL(14,0),AppliedAmount) as AppliedAmount,  p.SanctionType,q.StatusName,p.ProjectType from Project p,Status_Project_M q, ProjectType_M r where  p.ProjectType=r.TypeId and p.ProjectStatus=q.StatusId and StatusName='Sanctioned' and SanctionType='CA' and ProjectType=@ProjectType   and Title  like '%'+@Title+'%' and StatusId!='CAN' and StatusId!='REJ' and StatusId!='CLO' and ProjectUnit=@ProjectUnit and InstitutionID in(Select Institute_Id from User_Institution_Map where User_Id=@UserID) ";
                    SqlDataSource2.SelectParameters.Add("Title", TextBoxtiltleSearch.Text.Trim());
                }
                else
                {

                    SqlDataSource2.SelectCommand = "  select Top 10 p.ID, r.TypeName,p.ProjectUnit, UTN,Title,Description,CONVERT(DECIMAL(14,0),AppliedAmount) as AppliedAmount,  p.SanctionType,q.StatusName,p.ProjectType from Project p,Status_Project_M q, ProjectType_M r where  p.ProjectType=r.TypeId and p.ProjectStatus=q.StatusId and StatusName='Sanctioned' and SanctionType='CA' and ProjectType=@ProjectType  and ID like '%'+@ID+'%'  and Title  like '%'+@Title+'%' and StatusId!='CAN'  and StatusId!='REJ' and StatusId!='CLO' and ProjectUnit=@ProjectUnit and InstitutionID in(Select Institute_Id from User_Institution_Map where User_Id=@UserID)";
                    SqlDataSource2.SelectParameters.Add("ID", PubIDSearch.Text.Trim());
                    SqlDataSource2.SelectParameters.Add("Title", TextBoxtiltleSearch.Text.Trim());
                }
                SqlDataSource2.SelectParameters.Add("ProjectType", EntryTypesearch.SelectedValue);
                SqlDataSource2.SelectParameters.Add("UserID", Session["UserId"].ToString());
                SqlDataSource2.SelectParameters.Add("ProjectUnit", unit);

                GridViewsanSearch.DataBind();
                SqlDataSource2.DataBind();
            }


            else
            {
                if (PubIDSearch.Text == "" && TextBoxtiltleSearch.Text == "")
                {
                    SqlDataSource2.SelectCommand = "  select Top 10 p.ID, r.TypeName,p.ProjectUnit, UTN,Title,Description,CONVERT(DECIMAL(14,0),AppliedAmount) as AppliedAmount,  p.SanctionType,q.StatusName,p.ProjectType from Project p,Status_Project_M q, ProjectType_M r where  p.ProjectType=r.TypeId and p.ProjectStatus=q.StatusId and SanctionType='CA' and StatusId='SAN'  and ProjectUnit=@ProjectUnit and InstitutionID in(Select Institute_Id from User_Institution_Map where User_Id=@UserID)";
                }
                else if (PubIDSearch.Text != "" && TextBoxtiltleSearch.Text == "")
                {
                    SqlDataSource2.SelectCommand = "  select Top 10 p.ID, r.TypeName,p.ProjectUnit, UTN,Title,Description,CONVERT(DECIMAL(14,0),AppliedAmount) as AppliedAmount,  p.SanctionType,q.StatusName,p.ProjectType from Project p,Status_Project_M q, ProjectType_M r where  p.ProjectType=r.TypeId and p.ProjectStatus=q.StatusId  and SanctionType='CA' and StatusId='SAN' and ID like '%'+@ID+'%'  and ProjectUnit=@ProjectUnit and InstitutionID in(Select Institute_Id from User_Institution_Map where User_Id=@UserID)";
                    SqlDataSource2.SelectParameters.Add("ID", PubIDSearch.Text.Trim());
                }
                else if (PubIDSearch.Text == "" && TextBoxtiltleSearch.Text != "")
                {
                    SqlDataSource2.SelectCommand = "  select Top 10 p.ID, r.TypeName, p.ProjectUnit,UTN,Title,Description,CONVERT(DECIMAL(14,0),AppliedAmount) as AppliedAmount,  p.SanctionType,q.StatusName,p.ProjectType from Project p,Status_Project_M q, ProjectType_M r where  p.ProjectType=r.TypeId and p.ProjectStatus=q.StatusId and StatusId='SAN' and SanctionType='CA'   and Title like '%'+@Title+'%'  and ProjectUnit=@ProjectUnit and InstitutionID in(Select Institute_Id from User_Institution_Map where User_Id=@UserID)";
                    SqlDataSource2.SelectParameters.Add("Title", TextBoxtiltleSearch.Text.Trim());
                }
                else
                {

                    SqlDataSource2.SelectCommand = " select Top 10 p.ID, r.TypeName,p.ProjectUnit, UTN,Title,Description,CONVERT(DECIMAL(14,0),AppliedAmount) as AppliedAmount,  p.SanctionType,q.StatusName,p.ProjectType from Project p,Status_Project_M q, ProjectType_M r where  p.ProjectType=r.TypeId and p.ProjectStatus=q.StatusId and StatusId='SAN' and SanctionType='CA' and CreatedBy=@UserID and ID like '%'+@ID+'%' and Title like '%'+@Title+'%'  and ProjectUnit=@ProjectUnit and InstitutionID in(Select Institute_Id from User_Institution_Map where User_Id=@UserID)";
                    SqlDataSource2.SelectParameters.Add("ID", PubIDSearch.Text.Trim());
                    SqlDataSource2.SelectParameters.Add("Title", TextBoxtiltleSearch.Text.Trim());
                }
                SqlDataSource2.SelectParameters.Add("UserID", Session["UserId"].ToString());
                SqlDataSource2.SelectParameters.Add("ProjectUnit", unit);
                GridViewsanSearch.DataBind();
                SqlDataSource2.DataBind();
            }
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
        UpdatePanelSearchPub.Update();
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
            GrantData g1 = new GrantData();
            g1 = obj.fnGrantData(pid, Unit, user);
            string createdby = g1.CreatedBy;
            if (createdby == user)
            {
                ButtonSavepdf.Enabled = true;
            }
            else if (createdby != user)
            {
                if (g1.AuthorType == "P" && g1.MUNonMU == "M")
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
        UpdatePanelSearchPub.Update();
        UpdatePanel1.Update();
        UpdatePanel5.Update();
        UpdatePanel6.Update();
        UpdatePanel7.Update();
        UpdatePanel8.Update();
        UpdatePanel9.Update();
        UpdatePanel10.Update();
        UpdatePanel11.Update();
        UpdatePanel12.Update();
        UpdatePanel13.Update();
        UpdatePanel20.Update();
        UpdatePanel8.Update();
        Panel7.Visible = true;
        cleardata();
        string Pid = Session["TempPid"].ToString();
        string TypeEntry = Session["TempTypeEntry"].ToString();
        string CurStatus = Session["TempStatus"].ToString();
        string projectunit = Session["ProjectUnit"].ToString();
        string Role = Session["Role"].ToString();
        GrantData v = new GrantData();
        GrantData v1 = new GrantData();
        GrantData v2 = new GrantData();
        Business obj = new Business();
        GrantData m = new GrantData();
        GrantData n = new GrantData();
        v = obj.fnfindGrantid(Pid, projectunit);

        TextBoxID.Text = Pid;
        TextBoxUTN.Text = v.UTN;
        DropDownListTypeGrant.DataSourceID = "SqlDataSourceDropDownListTypeGrant";
        DropDownListTypeGrant.DataBind();
        DropDownListTypeGrant.SelectedValue = TypeEntry;


        SqlDataSourcePrjStatus.SelectCommand = "Select * from Status_Project_M";
        DropDownListProjStatus.DataSourceID = "SqlDataSourcePrjStatus";
        DropDownListProjStatus.DataBind();
        DropDownListProjStatus.SelectedValue = v.Status;
        txtcontact.Text = v.Contact_No;
        DropDownListGrUnit.DataSourceID = "SqlDataSourceDropDownListGrUnit";
        DropDownListGrUnit.DataBind();
        DropDownListGrUnit.SelectedValue = v.GrantUnit;
        DropDownListSourceGrant.DataSourceID = "SqlDataSourceDropDownListSourceGrant";
        DropDownListSourceGrant.DataBind();
        Session["Amount"] =null;
        if (v.GrantSource != "")
        {
            DropDownListSourceGrant.SelectedValue = v.GrantSource;
        }

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
            UpdatePanel7.Update();

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
       
            
            DropDownAgencyType.Items.Clear();
            DropDownAgencyType.Items.Add(new ListItem("--Select--", "0", true));
            DropDownAgencyType.DataSourceID = "SqlDataSourceAgencyType";
            DropDownAgencyType.DataBind();
            DropDownAgencyType.SelectedValue = v.TypeofAgencyGrant.ToString();
       
      
           
            DropDownSectorLevel.Items.Clear();
            DropDownSectorLevel.Items.Add(new ListItem("--Select--", "0", true));
            DropDownSectorLevel.DataSourceID = "SqlDataSourceSectorLevel";
            DropDownSectorLevel.DataBind();
            DropDownSectorLevel.SelectedValue = v.FundingSectorLevelGrant.ToString();
        
      
        txtEmailId.Text = v.AgencyEmailId;
        txtagencycontact.Text = v.AgencyContact;

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
        //}Select
        if (v.DurationOfProject != 0)
        {
            txtProjectDuration.Text = v.DurationOfProject.ToString();
        }
        if ((Session["Role"].ToString() == "11" || Session["Role"].ToString() == "1"))
        {
            DSforgridview.SelectParameters.Clear();
            DSforgridview.SelectParameters.Add("UserId", Session["UserId"].ToString());
            DSforgridview.SelectParameters.Add("Pid", Pid);
            DSforgridview.SelectParameters.Add("projectunit", projectunit);
            DSforgridview.SelectCommand = "select UploadPDFPath ,q.InfoTypeName as TypeName ,Remark ,AuditFrom,AuditTo, p.CreatedDate, p.CreatedBy as CreatedBy, m.FirstName as AddedBy, EntryNo,Unit_Id ,p.InfoTypeId from ProjectAuxillaryDetails p,Project_AuxInfoTypeM q, User_M m where  p.CreatedBy=@UserId and p.InfoTypeId=q.InfoTypeId and m.User_Id=p.CreatedBy  and ID=@Pid and ProjectUnit=@projectunit and Deleted='N' order by EntryNo";
            DSforgridview.DataBind();
            GVViewFile.DataBind();


            DSforgridview1.SelectParameters.Clear();
            DSforgridview1.SelectParameters.Add("UserId", Session["UserId"].ToString());
            DSforgridview1.SelectParameters.Add("Pid", Pid);
            DSforgridview1.SelectParameters.Add("projectunit", projectunit);
            DSforgridview1.SelectCommand = "select UploadPDFPath ,q.InfoTypeName as TypeName ,Remark ,AuditFrom,AuditTo, p.CreatedDate, p.CreatedBy as CreatedBy, m.FirstName as AddedBy, EntryNo,Unit_Id ,p.InfoTypeId from ProjectAuxillaryDetails p,Project_AuxInfoTypeM q, User_M m where  p.InfoTypeId=q.InfoTypeId and m.User_Id=p.CreatedBy  and ID=@Pid and ProjectUnit=@projectunit and Deleted='N' and p.CreatedBy !=@UserId  order by EntryNo";
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
            DSforgridview.SelectCommand = "select UploadPDFPath ,q.InfoTypeName as TypeName ,Remark ,AuditFrom,AuditTo, p.CreatedDate, p.CreatedBy as CreatedBy, m.FirstName as AddedBy, EntryNo,Unit_Id ,p.InfoTypeId from ProjectAuxillaryDetails p,Project_AuxInfoTypeM q, User_M m where  p.CreatedBy=@UserId and p.InfoTypeId=q.InfoTypeId and m.User_Id=p.CreatedBy  and ID=@Pid and ProjectUnit=@projectunit and Deleted='N' order by EntryNo";
            DSforgridview.DataBind();
            GVViewFile.DataBind();

            DSforgridview1.SelectParameters.Clear();
            DSforgridview1.SelectParameters.Add("UserId", Session["UserId"].ToString());
            DSforgridview1.SelectParameters.Add("Pid", Pid);
            DSforgridview1.SelectParameters.Add("projectunit", projectunit);
            DSforgridview1.SelectCommand = "select UploadPDFPath ,q.InfoTypeName as TypeName ,Remark ,AuditFrom,AuditTo, p.CreatedDate, p.CreatedBy as CreatedBy, m.FirstName as AddedBy, EntryNo,Unit_Id ,p.InfoTypeId from ProjectAuxillaryDetails p,Project_AuxInfoTypeM q, User_M m where  p.InfoTypeId=q.InfoTypeId and m.User_Id=p.CreatedBy  and ID=@Pid and ProjectUnit=@projectunit and Deleted='N' and p.CreatedBy !=@UserId  order by EntryNo";
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
                    EmployeeCode.Enabled = false;
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
                }
                else if (DropdownMuNonMu.Text == "E")
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
                    AuthorName.Enabled = false;
                    EmployeeCodeBtnimg.Enabled = false;
                    DropdownStudentInstitutionName.Enabled = true;
                    DropdownStudentInstitutionName.Enabled = true;
                    MailId.Enabled = true;
                }
                else if (DropdownMuNonMu.Text == "O")
                {
                    DropdownStudentInstitutionName.Enabled = true;
                    DropdownStudentInstitutionName.Enabled = true;
                    EmployeeCodeBtnimg.Enabled = false;
                    EmployeeCodeBtnimg.Visible = true;
                    NationalType.Visible = false;
                    ContinentId.Visible = false;
                    MailId.Enabled = true;
                    AuthorName.Enabled = true;
                    EmployeeCode.Enabled = true;
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
                TextBox PercentAmount = (TextBox)GridViewInterOrganization.Rows[rowIndexIO].Cells[2].FindControl("PercentageIOAmount");
                //TextBox deptNme = (TextBox)GridViewInterInstitute.Rows[rowIndexIO].Cells[2].FindControl("DepartmentNameIO");
                //HiddenField deptId = (HiddenField)GridViewInterInstitute.Rows[rowIndexIO].Cells[2].FindControl("DepartmentIO");

                drCurrentRowIO = dtCurrentTableIO.NewRow();
                Session["PercentAmount"] = null;


                DropdownMuNonMu.Text = dtCurrentTableIO.Rows[p - 1]["MUNonMU"].ToString();
                if (DropdownMuNonMu.Text == "M")
                {

                    string Instname = obj.getMaheInstitutionName(DropdownMuNonMu.Text);

                    InstNme.Visible = true;
                    InstNme.Text = Instname;
                    //InstNme.Text = dtCurrentTableIO.Rows[p - 1]["InstitutionName"].ToString();
                    InstId.Value = dtCurrentTableIO.Rows[p - 1]["Institution"].ToString();
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

                    PercentAmount.Text = m.percentageIOAmount.ToString();
                }
                rowIndexIO++;
                if (dtCurrentTableIO.Rows.Count == 1)
                {
                    Percent.Text = "100";
                    GridViewInterOrganization.Enabled = false;
                    PercentAmount.Text = txtRevisedAppliedAmt.Text.ToString();
                    Session["PercentAmount"] = PercentAmount.Text;
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
                if ((dtCurrentTableIO.Rows.Count == 1) && (dtCurrentTableII.Rows.Count == 1))
                {
                    PercentIAmount.Text = Session["PercentAmount"].ToString();
                }
            }

            ViewState["CurrentTableII"] = dtCurrentTableII;
        }

        //}
        //    else
        //    {
        //        PanelPercentage.Visible = false;
        //    }
        //}
        //else 
        //{
        //    PanelPercentage.Visible = false;
        //}
        if (v.Status == "SAN")
        {
            if (v.SancType == "CA")
            {
                if (Role == "11")
                {
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

            SqlDataSourcePrjStatus.SelectCommand = "select StatusId,StatusName from Status_Project_M where StatusId='APP' or StatusId='REJ'  or  StatusId='SAN' ";
            DropDownListProjStatus.SelectedValue = v.Status;

            btnSave.Enabled = true;
            BtnAddMU.Enabled = true;
            Grid_AuthorEntry.Enabled = true;
            PanelViewUplodedfiles.Visible = true;
            Panel8.Visible = true;
            PanelUploaddetails.Enabled = true;

            DropDownListTypeGrant.Enabled = false;
            DropDownListGrUnit.Enabled = false;
            DropDownListSourceGrant.Enabled = false;
            TextBoxGrantDate.Enabled = false;
            TextBoxTitle.Enabled = false;

            PanelUploaddetails.Visible = true;
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
            txtProjectDuration.Enabled = true;

            DropDownListProjStatus.Enabled = true;
            TextBoxGrantAmt.Enabled = true;
            txtagencycontact.Enabled = true;
            txtpan.Enabled = true;
            txtEmailId.Enabled = true;
            txtAddress.Enabled = true;
            txtstate.Enabled = true;
            txtcountry.Enabled = true;
            panAddAuthor.Enabled = true;
            PanelProjectOutcome.Visible = false;
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
            PanelProjectOutcome.Visible = false;
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
            PanelUploaddetails.Visible = true;
            PanelViewUplodedfiles.Visible = true;
            PanelProjectOutcome.Visible = false;
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
                PanelUploaddetails.Visible = true;
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
                GrantSanction.Visible = true;
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
            if (Role == "11")
            {
                PanelProjectOutcome.Visible = true;
                PanelProjectOutcome.Enabled = true;

            }
            else
            {
                PanelProjectOutcome.Enabled = false;
                PanelProjectOutcome.Visible = true;
            }
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
                PanelUploaddetails.Visible = true;
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
                else if (Session["Role"].ToString() == "16")
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
                GrantSanction.Visible = true;
                PnlBank.Visible = true;
                PanelIncentive.Visible = true;
                PanelOverhead.Visible = true;
                PanelFinanceClosure.Visible = true;
                panelReowrkRemarks.Visible = false;
                panelCanelRemarks.Visible = false;
                LabelSanType.Visible = true;
                DropDownListSanType.Visible = true;
                LabelkindDetails.Visible = false;
                TextBoxKindDetails.Visible = false;
                txtNoOFSanctions.Enabled = false;
                PanelUploaddetails.Visible = true;
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
        if (v.Status == "SAN")
        {
            //ProjectOutcome details
            DataTable dyPO = obj.SelectProjectOutcomeDetails(Pid, projectunit);
            if (dyPO.Rows.Count != 0)
            {
                ViewState["ProjectOutcomeDetails"] = dyPO;
                GridViewProjectsOutcome.DataSource = dyPO;
                GridViewProjectsOutcome.DataBind();
                GridViewProjectsOutcome.Visible = true;

                int rowIndex2 = 0;

                DataTable table = (DataTable)ViewState["ProjectOutcomeDetails"];
                DataRow drCurrentRow2 = null;
                if (table.Rows.Count > 0)
                {
                    for (int i = 1; i <= table.Rows.Count; i++)
                    {
                        TextBox DateofoutcomeDate = (TextBox)GridViewProjectsOutcome.Rows[rowIndex2].Cells[1].FindControl("DateofoutcomeDate");
                        TextBox txtincentivedate = (TextBox)GridViewProjectsOutcome.Rows[rowIndex2].Cells[2].FindControl("txtProjectOutcomeDescription");
                        drCurrentRow2 = table.NewRow();

                        txtincentivedate.Text = table.Rows[i - 1]["Description"].ToString();
                        DateTime date = Convert.ToDateTime(table.Rows[i - 1]["OutcomeDate"].ToString());
                        DateofoutcomeDate.Text = date.ToShortDateString();                  
                        rowIndex2++;

                    }


                    ViewState["ProjectOutcomeDetails"] = table;
                }
            }
            else
            {
                SetIntialRowProjectOutcome();

            }
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
                        GrantSanction.Enabled = true;
                        PnlBank.Enabled = true;
                        PanelIncentive.Enabled = true;
                        PanelOverhead.Enabled = true;
                    }
                    else
                    {
                        PanelFinanceClosure.Visible = true;
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
                    PanelFinanceClosure.Visible = true;
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

        if (v.Status == "SAN" || v.Status == "APP")
        {
            RequiredFieldValidator7.Enabled = false;
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
        UpdatePanel8.Update();
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

        ScriptManager.RegisterStartupScript(this, this.GetType(), "CallMyFunction", "ToggleDisplay5()", true);

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
        UpdatePanel8.Update();
        UpdatePanel15.Update();
        if (txtbankname.Text== "")
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

        //ModalPopupExtender ModalPopupExtender9 = (ModalPopupExtender)GridView_bank.Rows[rowIndex].FindControl("ModalPopupExtender5");
        //ModalPopupExtender9.Show();

        //setModalWindow(sender, e);

    }

    protected void branchNameChanged2(object sender, EventArgs e)
    {
        UpdatePanel8.Update();
        UpdatePanel15.Update();
        if (TextBox2.Text == "")
        {
            SqlDataSourceCreB.SelectCommand = "SELECT top 10 BankID, BankName FROM ProjectBank_M";
            popupbank.DataBind();
            popupbank.Visible = true;
        }

        else
        {
            SqlDataSourceCreB.SelectParameters.Clear();
            SqlDataSourceCreB.SelectParameters.Add("BankName", TextBox2.Text);
            SqlDataSourceCreB.SelectCommand = "SELECT  BankID, BankName FROM ProjectBank_M where BankName  like '%'+@BankName +'%'";

            popupCrB.DataBind();
            popupCbank.Visible = true;
        }


        //string rowVal = Request.Form["rowIndx"];
        //int rowIndex = Convert.ToInt32(rowVal);

        //ModalPopupExtender ModalPopupExtender9 = (ModalPopupExtender)GridView_bank.Rows[rowIndex].FindControl("ModalPopupExtender5");
        //ModalPopupExtender9.Show();

        //setModalWindow(sender, e);

    }

    private void cleardata()
    {
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
        Textsanctionorderdate.Text = "";
    }

    //Drop Down list Project status change
    protected void DropDownListProjStatusOnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownListProjStatus.SelectedValue == "SAN")
        {
            UpdatePanel20.Update();
            UpdatePanel7.Update();
            UpdatePanel1.Update();
            PanelPercentage.Visible = true;
            Textsanctionorderdate.Text = string.Empty;
            lblsanctionorderdate.Visible = true;
            Textsanctionorderdate.Visible = true;
            RequiredFieldValidator7.Enabled = true;

            GrantSanction.Visible = true;
            LabelSanType.Visible = true;
            DropDownListSanType.Visible = true;
            panelCanelRemarks.Visible = false;


            BtnAddMU.Enabled = false;
            Grid_AuthorEntry.Enabled = false;
            PanelUploaddetails.Visible = true;
            PanelViewUplodedfiles.Visible = true;
            PanelUploaddetails.Enabled = true;
            btnSave.Enabled = true;
            TextBoxDescription.Enabled = true;
            txtcontact.Enabled = true;
            TextBoxGrantAmt.Enabled = true;
            DropDownListerfRelated.Enabled = true;
            txtagencycontact.Enabled = true;
            txtpan.Enabled = true;
            txtEmailId.Enabled = true;
            txtAddress.Enabled = true;
            txtstate.Enabled = true;
            txtcountry.Enabled = true;
            lblRevisedAppliedAmt.Visible = true;
            txtRevisedAppliedAmt.Visible = true;
            txtRevisedAppliedAmt.Enabled = true;
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
            lblsanctionorderdate.Visible = false;
            Textsanctionorderdate.Visible = false;
            GrantSanction.Visible = false;
            LabelSanType.Visible = false;
            DropDownListSanType.Visible = false;
            BtnAddMU.Enabled = true;
            Grid_AuthorEntry.Enabled = true;
            panelCanelRemarks.Visible = false;
            PanelUploaddetails.Enabled = true;
            TextKindStartDate.Visible = false;
            TextKindclosedate.Visible = false;
            TextBoxKindDetails.Visible = false;
            LabelkindDetails.Visible = false;
            KindClosedate.Visible = false;
            kindStartdate.Visible = false;
            PanelKindetails.Visible = false;
            txtRevisedAppliedAmt.Enabled = false;
            RequiredFieldValidator7.Enabled = false;
            PanelPercentage.Visible = false;
        }

        else if (DropDownListProjStatus.SelectedValue == "REJ")
        {
            lblsanctionorderdate.Visible = false;
            Textsanctionorderdate.Visible = false;
            panelCanelRemarks.Visible = true;
            GrantSanction.Visible = false;
            TextBoxRemarks.Enabled = true;
            LabelSanType.Visible = false;
            DropDownListSanType.Visible = false;
            PanelKindetails.Visible = false;
            TextBoxGrantAmt.Enabled = false;
            txtcontact.Enabled = false;
            TextBoxDescription.Enabled = false;
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
            RequiredFieldValidator7.Enabled = false;
            PanelPercentage.Visible = false;
            UpdatePanel23.Update();
            UpdatePanel22.Update();

        }
    }

    //Drop Down list Sanction Type changed
    protected void DropDownListSanTypeOnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownListSanType.SelectedValue == "CA")
        {
            PanelPercentage.Visible = true;
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
            PanelPercentage.Visible = false;
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
            PanelPercentage.Visible = false;
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
        //UpdatePanel5.Update();
        popupselectNo.Visible = true;
        MainpanelGrant.Visible = true;
        Panel7.Visible = true;
        int rows = popGridagency.Rows.Count;
        popGridagency.Visible = true;

        if (agencysearch.Text.Trim() == "")
        {
            SqlDataSourceTextBoxGrantAgency.SelectCommand = "SELECT   FundingAgencyId as Id,UPPER([FundingAgencyName]) as Name FROM [ProjectFundingAgency_M]";
            popGridagency.DataBind();
            popGridagency.Visible = true;
        }

        else
        {

            popGridagency.DataSourceID = "SqlDataSourceTextBoxGrantAgency";
            SqlDataSourceTextBoxGrantAgency.SelectParameters.Clear();
            SqlDataSourceTextBoxGrantAgency.SelectParameters.Add("FundingAgencyName", agencysearch.Text);
            SqlDataSourceTextBoxGrantAgency.SelectCommand = "SELECT   FundingAgencyId as Id,UPPER([FundingAgencyName]) as Name FROM [ProjectFundingAgency_M] where FundingAgencyName like '%'+@FundingAgencyName+'%'";
            popGridagency.DataBind();               

        }

       

     


        //model.Show();
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
        EmployeeCode.Enabled = false;
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
        DropdownMuNonMu.SelectedValue = "M";
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
        //popup.Visible = false;
        popupPanelAffil.Visible = false;
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
                        ImageButton ImageButton1 = (ImageButton)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("ImageButton1");
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
                            EmployeeCode.Enabled = false;
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
                            EmployeeCode.Enabled = false;
                            EmployeeCodeBtnImg.Enabled = false;

                            dtCurrentTable.Rows[i - 1]["NationalType"] = NationalType.SelectedValue;
                            dtCurrentTable.Rows[i - 1]["ContinentId"] = ContinentId.SelectedValue;

                            dtCurrentTable.Rows[i - 1]["Institution"] = Institution.Value;
                            dtCurrentTable.Rows[i - 1]["InstitutionName"] = InstitutionName.Text;
                            dtCurrentTable.Rows[i - 1]["Department"] = Department.Value;
                            dtCurrentTable.Rows[i - 1]["DepartmentName"] = DepartmentName.Text;
                        }
                        else  if (DropdownMuNonMu.Text == "E")
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
                            EmployeeCode.Enabled = false;
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
                            DropdownStudentInstitutionName.Visible = false;
                            DropdownStudentDepartmentName.Visible = false;
                            InstitutionName.Visible = false;
                            DepartmentName.Visible = false;
                            EmployeeCode.Enabled = false;
                            NationalType.Visible = false;
                            ContinentId.Visible = false;

                            EmployeeCodeBtnImg.Enabled = false;
                            EmployeeCodeBtnImg.Visible = false;
                            EmployeeCode.Enabled = false;

                            dtCurrentTable.Rows[i - 1]["NationalType"] = NationalType.SelectedValue;
                            dtCurrentTable.Rows[i - 1]["ContinentId"] = ContinentId.SelectedValue;
                            //dtCurrentTable.Rows[i - 1]["Institution"] = DropdownStudentInstitutionName.SelectedValue;

                            //dtCurrentTable.Rows[i - 1]["Department"] = DropdownStudentDepartmentName.SelectedValue;
                            dtCurrentTable.Rows[i - 1]["Institution"] = Institution.Value;
                            dtCurrentTable.Rows[i - 1]["InstitutionName"] = InstitutionName.Text;
                            dtCurrentTable.Rows[i - 1]["Department"] = Department.Value;
                            dtCurrentTable.Rows[i - 1]["DepartmentName"] = DepartmentName.Text;
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
    private void  SetPreviousData()
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

                    TextBox EmployeeCode1 = (TextBox)Grid_AuthorEntry.Rows[0].Cells[0].FindControl("EmployeeCode");
                    HiddenField Institution1 = (HiddenField)Grid_AuthorEntry.Rows[0].Cells[0].FindControl("Institution");
                    TextBox InstitutionName1 = (TextBox)Grid_AuthorEntry.Rows[0].Cells[6].FindControl("InstitutionName");
                    HiddenField Department1 = (HiddenField)Grid_AuthorEntry.Rows[0].Cells[0].FindControl("Department");
                    TextBox DepartmentName1 = (TextBox)Grid_AuthorEntry.Rows[0].Cells[0].FindControl("DepartmentName");
                    TextBox MailId1 = (TextBox)Grid_AuthorEntry.Rows[0].Cells[0].FindControl("MailId");


                    DropDownList AuthorType1 = (DropDownList)Grid_AuthorEntry.Rows[0].Cells[0].FindControl("AuthorType");
                    DropDownList DropdownStudentInstitutionName1 = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("DropdownStudentInstitutionName");
                    DropDownList DropdownStudentDepartmentName1 = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("DropdownStudentDepartmentName");
                    ImageButton ImageButton1 = (ImageButton)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("ImageButton1");

                    DropdownMuNonMu.Text = dt.Rows[i]["DropdownMuNonMu"].ToString();
                    AuthorName.Text = dt.Rows[i]["AuthorName"].ToString();
                    EmployeeCode.Text = dt.Rows[i]["EmployeeCode"].ToString();
                    if (DropdownMuNonMu.Text == "M")
                    {
                        AuthorName.Enabled = false;
                        InstitutionName.Enabled = false;
                        DepartmentName.Enabled = false;
                        MailId.Enabled = false;
                        EmployeeCode.Enabled = false;
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
                        EmployeeCode.Enabled = false;
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
                        EmployeeCode.Enabled = false;
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
                        AuthorName.Enabled = false;
                        InstitutionName.Enabled = false;
                        DepartmentName.Enabled = false;
                        MailId.Enabled = false;
                        EmployeeCode.Enabled = false;

                        NationalType.Visible = false;
                        ContinentId.Visible = false;

                        NationalType.Text = dt.Rows[i]["NationalType"].ToString();
                        ContinentId.Text = dt.Rows[i]["ContinentId"].ToString();

                        EmployeeCodeBtnImg.Enabled = false;

                        //DropdownStudentInstitutionName.Visible = true;
                        //DropdownStudentDepartmentName.Visible = true;
                        //InstitutionName.Visible = false;
                        //DepartmentName.Visible = false;
                        DropdownStudentInstitutionName.Visible = false;
                        DropdownStudentDepartmentName.Visible = false;
                        InstitutionName.Visible = true;
                        DepartmentName.Visible = true;
                        Institution.Value = dt.Rows[i]["Institution"].ToString();
                        InstitutionName.Text = dt.Rows[i]["InstitutionName"].ToString();
                        Department.Value = dt.Rows[i]["Department"].ToString();
                        DepartmentName.Text = dt.Rows[i]["DepartmentName"].ToString();
                        //  Institution.Value = dt.Rows[i]["Institution"].ToString();
                        //DropdownStudentInstitutionName.SelectedValue = dt.Rows[i]["Institution"].ToString();
                        ////  Department.Value = dt.Rows[i]["Department"].ToString();
                        //DropdownStudentDepartmentName.SelectedValue = dt.Rows[i]["Department"].ToString();
                    }
                    else if (DropdownMuNonMu.Text == "O")
                    {

                        EmployeeCode.Visible = true;
                        EmployeeCode.Enabled = true;
                        AuthorName.Enabled = true;
                        InstitutionName.Enabled = false;
                        DepartmentName.Enabled = false;
                        MailId.Enabled = true;
                        AuthorName.Enabled = true;
                        EmployeeCodeBtnImg.Enabled = false;
                        EmployeeCodeBtnImg1.Visible = true;
                        DropdownStudentInstitutionName.Visible = true;
                        DropdownStudentDepartmentName.Visible = true;
                        InstitutionName.Visible = false;
                        DepartmentName.Visible = false;
                        NationalType.Visible = false;
                        ContinentId.Visible = false;
                        EmployeeCode.Text = dt.Rows[i]["EmployeeCode"].ToString();
                        Institution.Value = dt.Rows[i]["Institution"].ToString();
                        DropdownStudentInstitutionName.SelectedValue = dt.Rows[i]["Institution"].ToString();
                        Department.Value = dt.Rows[i]["Department"].ToString();
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
                    else  if (DropdownMuNonMu.Text == "E")
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
    protected void OnRowDataBound1(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList DropdownMuNonMu = (e.Row.FindControl("DropdownMuNonMu") as DropDownList);
            SqlDataSourceAuthorType.SelectCommand = "SELECT Id,DisplayName FROM [Author_Type_M] where (Id = 'M') or (Id = 'S') or (Id = 'N') or (Id = 'O') or (Id = 'E') order by DisplayNumber asc";

            DropdownMuNonMu.DataTextField = "DisplayName";
            DropdownMuNonMu.DataValueField = "Id";
            DropdownMuNonMu.DataBind();
        }

    }

    //Drop Down MU/Non MU change
    protected void DropdownMuNonMuOnSelectedIndexChanged(object sender, EventArgs e)
    {



        TextBox senderBox = sender as TextBox;

        GridViewRow currentRow = (GridViewRow)((DropDownList)sender).Parent.Parent;
        DropDownList DropdownMuNonMu = (DropDownList)currentRow.FindControl("DropdownMuNonMu");
        ImageButton EmployeeCodeBtn = (ImageButton)currentRow.FindControl("EmployeeCodeBtn");
        TextBox EmployeeCode = (TextBox)currentRow.FindControl("EmployeeCode");
        TextBox InstitutionName = (TextBox)currentRow.FindControl("InstitutionName");
        DropDownList DropdownStudentInstitutionName1 = (DropDownList)currentRow.FindControl("DropdownStudentInstitutionName");

        TextBox DepartmentName = (TextBox)currentRow.FindControl("DepartmentName");
        DropDownList DropdownStudentDepartmentName = (DropDownList)currentRow.FindControl("DropdownStudentDepartmentName");

        TextBox AuthorName = (TextBox)currentRow.FindControl("AuthorName");
        TextBox MailId = (TextBox)currentRow.FindControl("MailId");
        DropDownList NationalType = (DropDownList)currentRow.FindControl("NationalType");

        DropDownList ContinentId = (DropDownList)currentRow.FindControl("ContinentId");
        ImageButton ImageButton1 = (ImageButton)currentRow.FindControl("ImageButton1");
        if (DropdownMuNonMu.SelectedValue == "M")
        {
            EmployeeCode.Enabled = false;
           
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
            ImageButton1.Visible = false;
            EmployeeCodeBtn.Visible = true;
        }
        else if (DropdownMuNonMu.SelectedValue == "N")
        {
            EmployeeCode.Enabled = false;
         
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
            ImageButton1.Visible = false;
            EmployeeCodeBtn.Visible = true;
        }
        else if (DropdownMuNonMu.SelectedValue == "E")
        {
            EmployeeCode.Enabled = false;

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
            ImageButton1.Visible = false;
            EmployeeCodeBtn.Visible = true;
        }
        else if (DropdownMuNonMu.SelectedValue == "S")
        {
            EmployeeCode.Enabled = false;
            //popupstudent.Visible = true;
            //popupStudentGrid.DataSourceID = "StudentSQLDS";
            //StudentSQLDS.DataBind();
            //popupStudentGrid.DataBind();
            //popupStudentGrid.Visible = true;
            DropdownStudentInstitutionName1.Visible = false;
            DropdownStudentDepartmentName.Visible = false;
            InstitutionName.Visible = true;
            DepartmentName.Visible = true;
            NationalType.Visible = false;
            EmployeeCodeBtn.Enabled = false;
            InstitutionName.Enabled = false;
            DepartmentName.Enabled = false;
            AuthorName.Enabled = false;
            MailId.Enabled = true;
            AuthorName.Text = "";
            MailId.Text = "";
            InstitutionName.Text = "";
            DepartmentName.Text = "";
            ImageButton1.Visible = true;
            EmployeeCodeBtn.Visible = false;
            ImageButton1.Enabled = true;

        }
        else if (DropdownMuNonMu.SelectedValue == "O")
        {
         
            EmployeeCode.Visible = true;
            EmployeeCode.Enabled = true;
            DropdownStudentInstitutionName1.Visible = true;
            DropdownStudentDepartmentName.Visible = true;
            InstitutionName.Visible = false;
            DepartmentName.Visible = false;
            NationalType.Visible = false;
            ContinentId.Visible = false;
            EmployeeCodeBtn.Enabled = false;
            InstitutionName.Enabled = false;
            DepartmentName.Enabled = false;
            AuthorName.Enabled = true;
            MailId.Enabled = true;
            EmployeeCode.Text = "";
            AuthorName.Text = "";
            MailId.Text = "";
            InstitutionName.Text = "";
            DepartmentName.Text = "";
            // NameInJournal.Text = "";
            ImageButton1.Visible = false;
            EmployeeCodeBtn.Visible = true;

        }
    }
    protected void SearchStudentData(object sender, EventArgs e)
    {
        //StudentSQLDS.SelectParameters.Clear();
        //if (txtSrchStudentName.Text.Trim() == "" && txtSrchStudentRollNo.Text.Trim() == "" && StudentIntddl.SelectedValue == "")
        //{
        //    StudentSQLDS.SelectCommand = "Select TOP 10  RollNo,Name,SISClass.ClassName as ClassName,SISInstitution.InstName as InstName,EmailID1,SISStudentGenInfo.ClassCode as ClassCode,SISInstnHR.HRInstitute as InstID from SISStudentGenInfo,SISClass,SISInstitution,SISInstnHR  where SISStudentGenInfo.ClassCode=SISClass.ClassCode and SISStudentGenInfo.InstID=SISInstitution.InstID and SISInstnHR.Institute_Id=SISInstitution.InstID and SISInstnHR.Institute_Id=SISStudentGenInfo.InstID";
        //    popupStudentGrid.DataBind();
        //    popupStudentGrid.Visible = true;
        //}
        //else if ((txtSrchStudentName.Text.Trim() != "" || txtSrchStudentRollNo.Text.Trim() == "") && StudentIntddl.SelectedValue == "")
        //{
        //    StudentSQLDS.SelectParameters.Add("StudentName", txtSrchStudentName.Text.Trim());
        //    StudentSQLDS.SelectParameters.Add("StudentRollNo", txtSrchStudentRollNo.Text.Trim());
        //    StudentSQLDS.SelectCommand = "Select TOP 10  RollNo,Name,SISClass.ClassName as ClassName,SISInstitution.InstName as InstName,EmailID1 ,SISStudentGenInfo.ClassCode as ClassCode,SISInstnHR.HRInstitute as InstID from SISStudentGenInfo,SISClass,SISInstitution,SISInstnHR  where SISStudentGenInfo.ClassCode=SISClass.ClassCode and SISStudentGenInfo.InstID=SISInstitution.InstID and SISInstnHR.Institute_Id=SISInstitution.InstID and SISInstnHR.Institute_Id=SISStudentGenInfo.InstID and  Name like '%'+@StudentName+'%' and RollNo like '%'+@StudentRollNo+'%' ";
        //    popupStudentGrid.DataBind();
        //    popupStudentGrid.Visible = true;
        //}
        //else
        //{
        //    StudentSQLDS.SelectParameters.Clear();
        //    StudentSQLDS.SelectParameters.Add("StudentName", txtSrchStudentName.Text.Trim());
        //    StudentSQLDS.SelectParameters.Add("StudentRollNo", txtSrchStudentRollNo.Text.Trim());
        //    StudentSQLDS.SelectParameters.Add("StudentInt", StudentIntddl.SelectedValue.Trim());
        //    StudentSQLDS.SelectCommand = "Select TOP 10  RollNo,Name,SISClass.ClassName as ClassName,SISInstitution.InstName as InstName,EmailID1 ,SISStudentGenInfo.ClassCode as ClassCode,SISInstnHR.HRInstitute as InstID from SISStudentGenInfo,SISClass,SISInstitution,SISInstnHR  where SISStudentGenInfo.ClassCode=SISClass.ClassCode and SISStudentGenInfo.InstID=SISInstitution.InstID and SISInstnHR.Institute_Id=SISInstitution.InstID and SISInstnHR.Institute_Id=SISStudentGenInfo.InstID and  (Name like '%'+@StudentName+'%'  and RollNo  like '%'+@StudentRollNo+'%' and (SISStudentGenInfo.InstID=@StudentInt) ) ";


        StudentSQLDS.SelectParameters.Clear();
        if (txtSrchStudentName.Text.Trim() == "" && txtSrchStudentRollNo.Text.Trim() == "" && StudentIntddl.SelectedValue == "")
        {
            StudentSQLDS.SelectCommand = "Select TOP 10  RollNo,Name,SISClass.ClassName as ClassName,SISInstitution.InstName as InstName,EmailID1,SISStudentGenInfo.ClassCode as ClassCode,SISInstnHR.HRInstitute as InstID from SISStudentGenInfo,SISClass,SISInstitution,SISInstnHR  where SISStudentGenInfo.ClassCode=SISClass.ClassCode and SISStudentGenInfo.InstID=SISInstitution.InstID and SISInstnHR.Institute_Id=SISInstitution.InstID and SISInstnHR.Institute_Id=SISStudentGenInfo.InstID";
            popupStudentGrid.DataBind();
            popupStudentGrid.Visible = true;
        }
        //here aug 02
        else if ((txtSrchStudentName.Text.Trim() != "" && txtSrchStudentRollNo.Text.Trim() == "") && StudentIntddl.SelectedValue == "")
        {
            StudentSQLDS.SelectParameters.Add("txtSrchStudentName", txtSrchStudentName.Text.Trim());


            StudentSQLDS.SelectCommand = "Select TOP 10  RollNo,Name,SISClass.ClassName as ClassName,SISInstitution.InstName as InstName,EmailID1 ,SISStudentGenInfo.ClassCode as ClassCode,SISInstnHR.HRInstitute as InstID from SISStudentGenInfo,SISClass,SISInstitution,SISInstnHR  where SISStudentGenInfo.ClassCode=SISClass.ClassCode and SISStudentGenInfo.InstID=SISInstitution.InstID and SISInstnHR.Institute_Id=SISInstitution.InstID and SISInstnHR.Institute_Id=SISStudentGenInfo.InstID and  Name like '%' + @txtSrchStudentName + '%'";

            popupStudentGrid.DataBind();
            popupStudentGrid.Visible = true;
        }
        else if ((txtSrchStudentName.Text.Trim() == "" && txtSrchStudentRollNo.Text.Trim() != "") && StudentIntddl.SelectedValue == "")
        {
            StudentSQLDS.SelectParameters.Add("txtSrchStudentRollNo", txtSrchStudentRollNo.Text.Trim());


            StudentSQLDS.SelectCommand = "Select TOP 10  RollNo,Name,SISClass.ClassName as ClassName,SISInstitution.InstName as InstName,EmailID1 ,SISStudentGenInfo.ClassCode as ClassCode,SISInstnHR.HRInstitute as InstID from SISStudentGenInfo,SISClass,SISInstitution,SISInstnHR  where SISStudentGenInfo.ClassCode=SISClass.ClassCode and SISStudentGenInfo.InstID=SISInstitution.InstID and SISInstnHR.Institute_Id=SISInstitution.InstID and SISInstnHR.Institute_Id=SISStudentGenInfo.InstID and  RollNo like '%' + @txtSrchStudentRollNo+ '%'";

            popupStudentGrid.DataBind();
            popupStudentGrid.Visible = true;
        }
        else if ((txtSrchStudentName.Text.Trim() == "" && txtSrchStudentRollNo.Text.Trim() == "") && StudentIntddl.SelectedValue != "")
        {
            StudentSQLDS.SelectParameters.Add("StudentIntddl", StudentIntddl.SelectedValue);

            StudentSQLDS.SelectCommand = "Select TOP 10  RollNo,Name,SISClass.ClassName as ClassName,SISInstitution.InstName as InstName,EmailID1 ,SISStudentGenInfo.ClassCode as ClassCode,SISInstnHR.HRInstitute as InstID from SISStudentGenInfo,SISClass,SISInstitution,SISInstnHR  where SISStudentGenInfo.ClassCode=SISClass.ClassCode and SISStudentGenInfo.InstID=SISInstitution.InstID and SISInstnHR.Institute_Id=SISInstitution.InstID and SISInstnHR.Institute_Id=SISStudentGenInfo.InstID and   (SISStudentGenInfo.InstID=@StudentIntddl)";

            popupStudentGrid.DataBind();
            popupStudentGrid.Visible = true;
        }
        else if ((txtSrchStudentName.Text.Trim() == "" && txtSrchStudentRollNo.Text.Trim() != "") && StudentIntddl.SelectedValue != "")
        {

            StudentSQLDS.SelectParameters.Add("txtSrchStudentRollNo", txtSrchStudentRollNo.Text.Trim());
            StudentSQLDS.SelectParameters.Add("StudentIntddl", StudentIntddl.SelectedValue);

            //StudentSQLDS.SelectCommand = "Select TOP 10  RollNo,Name,SISClass.ClassName as ClassName,SISInstitution.InstName as InstName,EmailID1 ,SISStudentGenInfo.ClassCode as ClassCode,SISInstnHR.HRInstitute as InstID from SISStudentGenInfo,SISClass,SISInstitution,SISInstnHR  where SISStudentGenInfo.ClassCode=SISClass.ClassCode and SISStudentGenInfo.InstID=SISInstitution.InstID and SISInstnHR.Institute_Id=SISInstitution.InstID and SISInstnHR.Institute_Id=SISStudentGenInfo.InstID and  Name like '" + txtSrchStudentName.Text + "%' and RollNo like '%" + txtSrchStudentRollNo.Text + "%'";
            StudentSQLDS.SelectCommand = "Select TOP 10  RollNo,Name,SISClass.ClassName as ClassName,SISInstitution.InstName as InstName,EmailID1 ,SISStudentGenInfo.ClassCode as ClassCode,SISInstnHR.HRInstitute as InstID from SISStudentGenInfo,SISClass,SISInstitution,SISInstnHR  where SISStudentGenInfo.ClassCode=SISClass.ClassCode and SISStudentGenInfo.InstID=SISInstitution.InstID and SISInstnHR.Institute_Id=SISInstitution.InstID and SISInstnHR.Institute_Id=SISStudentGenInfo.InstID  and RollNo like '%' + @txtSrchStudentRollNo+ '%' and (SISStudentGenInfo.InstID=@StudentIntddl)";

            popupStudentGrid.DataBind();
            popupStudentGrid.Visible = true;
        }
        else if ((txtSrchStudentName.Text.Trim() != "" && txtSrchStudentRollNo.Text.Trim() == "") && StudentIntddl.SelectedValue != "")
        {

            StudentSQLDS.SelectParameters.Add("txtSrchStudentName", txtSrchStudentName.Text.Trim());
            StudentSQLDS.SelectParameters.Add("StudentIntddl", StudentIntddl.SelectedValue);

            //StudentSQLDS.SelectCommand = "Select TOP 10  RollNo,Name,SISClass.ClassName as ClassName,SISInstitution.InstName as InstName,EmailID1 ,SISStudentGenInfo.ClassCode as ClassCode,SISInstnHR.HRInstitute as InstID from SISStudentGenInfo,SISClass,SISInstitution,SISInstnHR  where SISStudentGenInfo.ClassCode=SISClass.ClassCode and SISStudentGenInfo.InstID=SISInstitution.InstID and SISInstnHR.Institute_Id=SISInstitution.InstID and SISInstnHR.Institute_Id=SISStudentGenInfo.InstID and  Name like '" + txtSrchStudentName.Text + "%' and RollNo like '%" + txtSrchStudentRollNo.Text + "%'";
            StudentSQLDS.SelectCommand = "Select TOP 10  RollNo,Name,SISClass.ClassName as ClassName,SISInstitution.InstName as InstName,EmailID1 ,SISStudentGenInfo.ClassCode as ClassCode,SISInstnHR.HRInstitute as InstID from SISStudentGenInfo,SISClass,SISInstitution,SISInstnHR  where SISStudentGenInfo.ClassCode=SISClass.ClassCode and SISStudentGenInfo.InstID=SISInstitution.InstID and SISInstnHR.Institute_Id=SISInstitution.InstID and SISInstnHR.Institute_Id=SISStudentGenInfo.InstID  and Name like '%' + @txtSrchStudentName + '%' and (SISStudentGenInfo.InstID=@StudentIntddl)";

            popupStudentGrid.DataBind();
            popupStudentGrid.Visible = true;
        }


            //ends

        else if ((txtSrchStudentName.Text.Trim() != "" || txtSrchStudentRollNo.Text.Trim() != "") && StudentIntddl.SelectedValue == "")
        {
            StudentSQLDS.SelectParameters.Add("txtSrchStudentName", txtSrchStudentName.Text.Trim());
            StudentSQLDS.SelectParameters.Add("txtSrchStudentRollNo", txtSrchStudentRollNo.Text.Trim());

            //StudentSQLDS.SelectCommand = "Select TOP 10  RollNo,Name,SISClass.ClassName as ClassName,SISInstitution.InstName as InstName,EmailID1 ,SISStudentGenInfo.ClassCode as ClassCode,SISInstnHR.HRInstitute as InstID from SISStudentGenInfo,SISClass,SISInstitution,SISInstnHR  where SISStudentGenInfo.ClassCode=SISClass.ClassCode and SISStudentGenInfo.InstID=SISInstitution.InstID and SISInstnHR.Institute_Id=SISInstitution.InstID and SISInstnHR.Institute_Id=SISStudentGenInfo.InstID and  Name like '" + txtSrchStudentName.Text + "%' and RollNo like '%" + txtSrchStudentRollNo.Text + "%'";
            StudentSQLDS.SelectCommand = "Select TOP 10  RollNo,Name,SISClass.ClassName as ClassName,SISInstitution.InstName as InstName,EmailID1 ,SISStudentGenInfo.ClassCode as ClassCode,SISInstnHR.HRInstitute as InstID from SISStudentGenInfo,SISClass,SISInstitution,SISInstnHR  where SISStudentGenInfo.ClassCode=SISClass.ClassCode and SISStudentGenInfo.InstID=SISInstitution.InstID and SISInstnHR.Institute_Id=SISInstitution.InstID and SISInstnHR.Institute_Id=SISStudentGenInfo.InstID and  Name like '%' + @txtSrchStudentName + '%' and RollNo like '%' + @txtSrchStudentRollNo+ '%'";

            popupStudentGrid.DataBind();
            popupStudentGrid.Visible = true;
        }


        else
        {
            StudentSQLDS.SelectParameters.Add("txtSrchStudentName", txtSrchStudentName.Text);
            StudentSQLDS.SelectParameters.Add("txtSrchStudentRollNo", txtSrchStudentRollNo.Text);
            StudentSQLDS.SelectParameters.Add("StudentIntddl", StudentIntddl.SelectedValue);
            //StudentSQLDS.SelectCommand = "Select TOP 10  RollNo,Name,SISClass.ClassName as ClassName,SISInstitution.InstName as InstName,EmailID1 ,SISStudentGenInfo.ClassCode as ClassCode,SISInstnHR.HRInstitute as InstID from SISStudentGenInfo,SISClass,SISInstitution,SISInstnHR  where SISStudentGenInfo.ClassCode=SISClass.ClassCode and SISStudentGenInfo.InstID=SISInstitution.InstID and SISInstnHR.Institute_Id=SISInstitution.InstID and SISInstnHR.Institute_Id=SISStudentGenInfo.InstID and  (Name like '" + txtSrchStudentName.Text + "%' and RollNo like '%" + txtSrchStudentRollNo.Text + "%' and (SISStudentGenInfo.InstID='" + StudentIntddl.SelectedValue + "') ) ";
            StudentSQLDS.SelectCommand = "Select TOP 10  RollNo,Name,SISClass.ClassName as ClassName,SISInstitution.InstName as InstName,EmailID1 ,SISStudentGenInfo.ClassCode as ClassCode,SISInstnHR.HRInstitute as InstID from SISStudentGenInfo,SISClass,SISInstitution,SISInstnHR  where SISStudentGenInfo.ClassCode=SISClass.ClassCode and SISStudentGenInfo.InstID=SISInstitution.InstID and SISInstnHR.Institute_Id=SISInstitution.InstID and SISInstnHR.Institute_Id=SISStudentGenInfo.InstID and  (Name like '%' + @txtSrchStudentName + '%' and RollNo like '%' + @txtSrchStudentRollNo+ '%' and (SISStudentGenInfo.InstID=@StudentIntddl) ) ";


            
            
            popupStudentGrid.DataBind();
            popupStudentGrid.Visible = true;
        }


        // string rowVal = Request.Form["rowIndx"];
        string a = rowVal.Value;
        int rowIndex = Convert.ToInt32(a);
        DropDownList munonmu = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].FindControl("DropdownMuNonMu");
        if (munonmu.SelectedValue == "M")
        {
            //popupstudent.Visible = false;
            //popupPanelAffil.Visible = true;
            popupPanelAffil.Style.Add("display", "true");
            //popupstudent.Style.Add("display", "none");
        }
        else if (munonmu.SelectedValue == "S")
        {
            //popupstudent.Visible = true;
            //popupPanelAffil.Visible = false;
            popupstudent.Style.Add("display", "true");
            //popupPanelAffil.Style.Add("display", "none");
        }
        //ModalPopupExtender ModalPopupExtender8 = (ModalPopupExtender)Grid_AuthorEntry.Rows[rowIndex].FindControl("ModalPopupExtender2");
        //ModalPopupExtender8.Show();
    }
    protected void StudentDataSelect(Object sender, EventArgs e)
    {
        UpdatePanel1.Update();
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

        //TextBox NameInJournal = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("NameInJournal");

        //NameInJournal.Text = studentname;

        TextBox employc = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("EmployeeCode");
        employc.Text = rollno;


        HiddenField classcode = (HiddenField)popupStudentGrid.SelectedRow.FindControl("lblClassCode");

        HiddenField instnid = (HiddenField)popupStudentGrid.SelectedRow.FindControl("lblInstn");

        HiddenField Institution = (HiddenField)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("Institution");
        Institution.Value = instnid.Value;

        HiddenField Department = (HiddenField)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("Department");
        Department.Value = classcode.Value;
        popupStudentGrid.DataBind();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "CallMyFunction", "ToggleDisplay3()", true);


    }
    //Upload Functionality

    //Button Upload click
    protected void BtnUploadPdf_Click(object sender, EventArgs e)
    {
        UpdatePanel11.Update();
        UpdatePanel17.Update();
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
            UpdatePanel11.Update();
            UpdatePanel17.Update();
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
                        string CloseWindow1 = "alert('Uploaded file is not a valid file.Please upload a valid pdf file')";
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
                        DSforgridview.SelectCommand = "select UploadPDFPath ,q.InfoTypeName as TypeName ,Remark ,AuditFrom,AuditTo, p.CreatedDate, p.CreatedBy as CreatedBy, m.FirstName as AddedBy, EntryNo,Unit_Id ,p.InfoTypeId from ProjectAuxillaryDetails p,Project_AuxInfoTypeM q, User_M m where  p.CreatedBy=@UserId and p.InfoTypeId=q.InfoTypeId and m.User_Id=p.CreatedBy  and ID=@GID and ProjectUnit=@GrantUnit and Deleted='N' order by EntryNo";
                        DSforgridview.DataBind();
                        GVViewFile.DataBind();

                        DSforgridview1.SelectParameters.Clear();
                        DSforgridview1.SelectParameters.Add("GID", j.GID);
                        DSforgridview1.SelectParameters.Add("GrantUnit", j.GrantUnit);
                        DSforgridview1.SelectParameters.Add("UserId", Session["UserId"].ToString());
                        DSforgridview1.SelectCommand = "select UploadPDFPath ,q.InfoTypeName as TypeName ,Remark ,AuditFrom,AuditTo, p.CreatedDate, p.CreatedBy as CreatedBy, m.FirstName as AddedBy, EntryNo,Unit_Id ,p.InfoTypeId from ProjectAuxillaryDetails p,Project_AuxInfoTypeM q, User_M m where  p.InfoTypeId=q.InfoTypeId and m.User_Id=p.CreatedBy  and ID=@GID and ProjectUnit=@GrantUnit and Deleted='N' and p.CreatedBy!=@UserId order by EntryNo";
                        DSforgridview1.DataBind();
                        GridView1.DataBind();
                        Panel8.Visible = true;
                    }

                    if (Session["Role"].ToString() == "6" || Session["Role"].ToString() == "16")
                    {
                        DSforgridview.SelectParameters.Clear();
                        DSforgridview.SelectParameters.Add("UserId", Session["UserId"].ToString());
                        DSforgridview.SelectParameters.Add("GID", j.GID);
                        DSforgridview.SelectParameters.Add("GrantUnit", j.GrantUnit);
                        DSforgridview.SelectCommand = "select UploadPDFPath ,q.InfoTypeName as TypeName ,Remark ,AuditFrom,AuditTo, p.CreatedDate, p.CreatedBy as CreatedBy, m.FirstName as AddedBy, EntryNo,Unit_Id ,p.InfoTypeId from ProjectAuxillaryDetails p,Project_AuxInfoTypeM q, User_M m where  p.InfoTypeId=q.InfoTypeId and m.User_Id=p.CreatedBy  and ID=@GID and ProjectUnit=@GrantUnit and Deleted='N' and p.CreatedBy=@UserId order by EntryNo";
                        DSforgridview.DataBind();
                        GridView1.DataBind();
                        Panel8.Visible = true;


                        DSforgridview1.SelectParameters.Clear();
                        DSforgridview1.SelectParameters.Add("UserId", Session["UserId"].ToString());
                        DSforgridview1.SelectParameters.Add("GID", j.GID);
                        DSforgridview1.SelectParameters.Add("GrantUnit", j.GrantUnit);
                        DSforgridview1.SelectCommand = "select UploadPDFPath ,q.InfoTypeName as TypeName ,Remark ,AuditFrom,AuditTo, p.CreatedDate, p.CreatedBy as CreatedBy, m.FirstName as AddedBy, EntryNo,Unit_Id ,p.InfoTypeId from ProjectAuxillaryDetails p,Project_AuxInfoTypeM q, User_M m where  p.CreatedBy!=@UserId and p.InfoTypeId=q.InfoTypeId and m.User_Id=p.CreatedBy  and ID=@GID and ProjectUnit=@GrantUnit and Deleted='N' order by EntryNo";
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
                        //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('File Successfully uploaded!')</script>");
                        log.Info("File Successfully uploaded : " + TextBoxID.Text.Trim() + " , Project Type : " + DropDownListTypeGrant.SelectedValue);
                        string CloseWindow = "alert('File Successfully uploaded!')";
                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow, true);
                        //string CloseWindow = "alert('File Successfully uploaded!')";
                        //ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);
                        return;

                    }
                    else
                    {
                        //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Error in File upload!!!!')</script>");
                        log.Error("Error in File upload!!!! : " + TextBoxID.Text.Trim() + " , Project Type : " + DropDownListTypeGrant.SelectedValue);

                        string CloseWindow = "alert('Error in File upload!!!!')";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);
                        return;



                    }

                }
                else
                {
                    //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Invalid File!!!File exceeds more than 4MB..Please try to upload the file less than or equal to 4MB!!!!!!')</script>");
                    log.Error("Invalid File!!!File exceeds more than 4MB!!! : " + TextBoxID.Text.Trim() + " , Project Type : " + DropDownListTypeGrant.SelectedValue);
                    string CloseWindow = "alert('Invalid File!!!File exceeds more than 4MB..Please try to upload the file less than or equal to 4MB!!!!!!')";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);

                
                }
            }

        }
        catch (Exception ex)
        {
            log.Error("Inside Catch Block Of Grant-file uplaod" + ex.Message + " UserID : " + Session["UserId"].ToString());

            log.Error(ex.StackTrace);

            //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Error!!!!!!!!!!!!')</script>");
            string CloseWindow = "alert('Error!!!!')";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);

        }
    }


    //Function to view uploaded files
    protected void GVViewFile_SelectedIndexChanged(object sender, EventArgs e)
    {
        UpdatePanel17.Update();
        UpdatePanel11.Update();
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

            //Response.Write("<script>");
            //Response.Write("window.open('DisplayPdf.aspx?val=" + newpath + "','_blank')");
            //Response.Write("</script>");
            Session["path"] = newpath;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Test", "ViewPdf()", true);


         

        }
    }

    protected void GVView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        UpdatePanel11.Update();
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

            //Response.Write("<script>");
            //Response.Write("window.open('DisplayPdf.aspx?val=" + newpath + "','_blank')");
            //Response.Write("</script>");

            Session["path"] = newpath;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Test", "ViewPdf()", true);
        }
    }

    //Function Delete uploaded files
    protected void lnkDeleteClick(object sender, EventArgs e)
    {
        UpdatePanel11.Update();
        UpdatePanel17.Update();
           string confirmValue = hdn.Value;
        string[] vals = confirmValue.Split(Convert.ToChar(","));

        if (vals[0] == "Yes")
        {

        //string confirmValue = Request.Form["confirm_value"];
        //if (confirmValue == "Yes")
        //{
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
                    //ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Atleast one sanction documnet must be uploaded');</script>");
                    string CloseWindow = "alert('Atleast one sanction documnet must be uploaded')";
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow, true);
                    return;

                }
                else
                {
                    result = B.UpdateGrantattachedEntry(j);
                    if (result == 1)
                    {

                        //ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Deleted Successfully');</script>");
                        string CloseWindow = "alert('Deleted Successfully')";
                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow, true);

                        //DSforgridview.SelectCommand = "select UploadPDFPath ,q.InfoTypeName as TypeName ,Remark,CreatedDate,EntryNo,p.CreatedBy,FirstName as AddedBy,Unit_Id ,p.InfoTypeId from ProjectAuxillaryDetails p,Project_AuxInfoTypeM q,User_M u where  p.InfoTypeId=q.InfoTypeId and ID='" + j.GID + "' and ProjectUnit='" + j.GrantUnit + "' and Deleted='N' and u.User_Id=p.CreatedBy order by EntryNo";
                        //DSforgridview.DataBind();
                        //GVViewFile.DataBind();

                        if ((Session["Role"].ToString() == "11" || Session["Role"].ToString() == "1"))
                        {
                            DSforgridview.SelectParameters.Clear();
                            DSforgridview.SelectParameters.Add("UserId", Session["UserId"].ToString());
                            DSforgridview.SelectParameters.Add("GID", j.GID);
                            DSforgridview.SelectParameters.Add("GrantType", j.GrantType); //here
                            DSforgridview.SelectCommand = "select UploadPDFPath ,q.InfoTypeName as TypeName ,Remark ,AuditFrom,AuditTo, p.CreatedDate, p.CreatedBy as CreatedBy, m.FirstName as AddedBy, EntryNo,Unit_Id ,p.InfoTypeId from ProjectAuxillaryDetails p,Project_AuxInfoTypeM q, User_M m where  p.CreatedBy=@UserId and p.InfoTypeId=q.InfoTypeId and m.User_Id=p.CreatedBy  and ID=@GID and ProjectUnit=@GrantType and Deleted='N' order by EntryNo";
                            DSforgridview.DataBind();
                            GVViewFile.DataBind();

                            DSforgridview1.SelectParameters.Clear();
                            DSforgridview1.SelectParameters.Add("GID", j.GID);
                            DSforgridview1.SelectParameters.Add("GrantType", j.GrantType);//here
                            DSforgridview1.SelectParameters.Add("GrantUnit", j.GrantUnit);//here
                            DSforgridview1.SelectCommand = "select UploadPDFPath ,q.InfoTypeName as TypeName ,Remark ,AuditFrom,AuditTo, p.CreatedDate, p.CreatedBy as CreatedBy, m.FirstName as AddedBy, EntryNo,Unit_Id ,p.InfoTypeId from ProjectAuxillaryDetails p,Project_AuxInfoTypeM q, User_M m where  p.InfoTypeId=q.InfoTypeId and m.User_Id=p.CreatedBy  and ID=@GID  and ProjectUnit=@GrantType and Deleted='N' and p.CreatedBy  <>  (Select CreatedBy from ProjectAuxillaryDetails where ProjectUnit=@GrantUnit and ID=@GID and Deleted='N') order by EntryNo";
                            DSforgridview1.DataBind();
                            GridView1.DataBind();
                            Panel8.Visible = true;
                        }

                        if (Session["Role"].ToString() == "6"||Session["Role"].ToString() == "16")
                        {
                            DSforgridview.SelectParameters.Clear();

                            DSforgridview.SelectParameters.Add("GID", j.GID);
                            DSforgridview.SelectParameters.Add("GrantUnit", j.GrantUnit);
                            DSforgridview.SelectParameters.Add("GrantType", j.GrantType); //here
                            DSforgridview.SelectCommand = "select UploadPDFPath ,q.InfoTypeName as TypeName ,Remark ,AuditFrom,AuditTo, p.CreatedDate, p.CreatedBy as CreatedBy, m.FirstName as AddedBy, EntryNo,Unit_Id ,p.InfoTypeId from ProjectAuxillaryDetails p,Project_AuxInfoTypeM q, User_M m where  p.InfoTypeId=q.InfoTypeId and m.User_Id=p.CreatedBy  and ID=@GID and ProjectUnit=@GrantType and Deleted='N' and p.CreatedBy  <>  (Select CreatedBy from ProjectAuxillaryDetails where ProjectUnit=@GrantUnit and ID=@GID and Deleted='N')  order by EntryNo";
                            DSforgridview.DataBind();
                            GridView1.DataBind();
                            Panel8.Visible = true;

                            DSforgridview1.SelectParameters.Clear();
                            DSforgridview1.SelectParameters.Add("UserId", Session["UserId"].ToString());
                            DSforgridview1.SelectParameters.Add("GID", j.GID);
                            DSforgridview1.SelectParameters.Add("GrantType", j.GrantType); //here
                            DSforgridview1.SelectCommand = "select UploadPDFPath ,q.InfoTypeName as TypeName ,Remark ,AuditFrom,AuditTo, p.CreatedDate, p.CreatedBy as CreatedBy, m.FirstName as AddedBy, EntryNo,Unit_Id ,p.InfoTypeId from ProjectAuxillaryDetails p,Project_AuxInfoTypeM q, User_M m where  p.CreatedBy!=@UserId and p.InfoTypeId=q.InfoTypeId and m.User_Id=p.CreatedBy  and ID=@GID and ProjectUnit=@GrantType and Deleted='N' order by EntryNo";
                            DSforgridview1.DataBind();
                            GVViewFile.DataBind();
                        }
                    }
                    else
                    {
                        //ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Error!!!!!!!');</script>");
                        string CloseWindow = "alert(''Error!!!!!!')";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);

                        //DSforgridview.SelectCommand = "select UploadPDFPath ,q.InfoTypeName as TypeName ,Remark,CreatedDate,EntryNo,p.CreatedBy,FirstName as AddedBy,Unit_Id,p.InfoTypeId from ProjectAuxillaryDetails p,Project_AuxInfoTypeM q,User_M u where  p.InfoTypeId=q.InfoTypeId and ID='" + j.GID + "' and ProjectUnit='" + j.GrantUnit + "' and Deleted='N' and u.User_Id=p.CreatedBy  order by EntryNo";
                        //DSforgridview.DataBind();
                        //GVViewFile.DataBind();

                        if ((Session["Role"].ToString() == "11" || Session["Role"].ToString() == "1"))
                        {
                            DSforgridview.SelectParameters.Clear();
                            DSforgridview.SelectParameters.Add("UserId", Session["UserId"].ToString());
                            DSforgridview.SelectParameters.Add("GID", j.GID);
                            DSforgridview.SelectParameters.Add("GrantType", j.GrantType); //here
                            DSforgridview.SelectCommand = "select UploadPDFPath ,q.InfoTypeName as TypeName ,Remark ,AuditFrom,AuditTo, p.CreatedDate, p.CreatedBy as CreatedBy, m.FirstName as AddedBy, EntryNo,Unit_Id ,p.InfoTypeId from ProjectAuxillaryDetails p,Project_AuxInfoTypeM q, User_M m where  p.CreatedBy=@UserId and p.InfoTypeId=q.InfoTypeId and m.User_Id=p.CreatedBy  and ID=@GID and ProjectUnit=@GrantType and Deleted='N' order by EntryNo";
                            DSforgridview.DataBind();
                            GVViewFile.DataBind();

                            DSforgridview1.SelectParameters.Clear();

                            DSforgridview1.SelectParameters.Add("GID", j.GID);
                            DSforgridview1.SelectParameters.Add("GrantUnit", j.GrantUnit);
                            DSforgridview1.SelectCommand = "select UploadPDFPath ,q.InfoTypeName as TypeName ,Remark ,AuditFrom,AuditTo, p.CreatedDate, p.CreatedBy as CreatedBy, m.FirstName as AddedBy, EntryNo,Unit_Id ,p.InfoTypeId from ProjectAuxillaryDetails p,Project_AuxInfoTypeM q, User_M m where  p.InfoTypeId=q.InfoTypeId and m.User_Id=p.CreatedBy  and ID=@GID and ProjectUnit=@GrantUnit and Deleted='N' and p.CreatedBy  <>  (Select CreatedBy from ProjectAuxillaryDetails where ProjectUnit=@GrantUnit and ID=@GID and Deleted='N')  order by EntryNo";
                            DSforgridview1.DataBind();
                            GridView1.DataBind();
                            Panel8.Visible = true;
                        }

                        if (Session["Role"].ToString() == "6" || Session["Role"].ToString() == "16")
                        {
                            DSforgridview.SelectParameters.Clear();

                            DSforgridview.SelectParameters.Add("GID", j.GID);
                            DSforgridview.SelectParameters.Add("GrantUnit", j.GrantUnit);
                            DSforgridview.SelectParameters.Add("GrantType", j.GrantType); //here

                            DSforgridview.SelectCommand = "select UploadPDFPath ,q.InfoTypeName as TypeName ,Remark ,AuditFrom,AuditTo, p.CreatedDate, p.CreatedBy as CreatedBy, m.FirstName as AddedBy, EntryNo,Unit_Id ,p.InfoTypeId from ProjectAuxillaryDetails p,Project_AuxInfoTypeM q, User_M m where  p.InfoTypeId=q.InfoTypeId and m.User_Id=p.CreatedBy  and ID=@GID and ProjectUnit=@GrantType and Deleted='N' and p.CreatedBy  <>  (Select CreatedBy from ProjectAuxillaryDetails where ProjectUnit=@GrantUnit and ID=@GID and Deleted='N') order by EntryNo";
                            DSforgridview.DataBind();
                            GridView1.DataBind();
                            Panel8.Visible = true;

                            DSforgridview1.SelectParameters.Clear();
                            DSforgridview1.SelectParameters.Add("UserId", Session["UserId"].ToString());
                            DSforgridview1.SelectParameters.Add("GID", j.GID);
                            DSforgridview1.SelectParameters.Add("GrantType", j.GrantType);

                            DSforgridview1.SelectCommand = "select UploadPDFPath ,q.InfoTypeName as TypeName ,Remark ,AuditFrom,AuditTo, p.CreatedDate, p.CreatedBy as CreatedBy, m.FirstName as AddedBy, EntryNo,Unit_Id ,p.InfoTypeId from ProjectAuxillaryDetails p,Project_AuxInfoTypeM q, User_M m where  p.CreatedBy!=@UserId and p.InfoTypeId=q.InfoTypeId and m.User_Id=p.CreatedBy  and ID=@GID and ProjectUnit=@GrantType  and Deleted='N' order by EntryNo";
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

                    //ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Deleted Successfully');</script>");
                    string CloseWindow = "alert('Deleted Successfully')";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);

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
                        DSforgridview1.SelectParameters.Add("UserId", Session["UserId"].ToString());
                        DSforgridview1.SelectParameters.Add("GID", j.GID);
                        DSforgridview1.SelectParameters.Add("GrantType", j.GrantType);
                        DSforgridview1.SelectCommand = "select UploadPDFPath ,q.InfoTypeName as TypeName ,Remark ,AuditFrom,AuditTo, p.CreatedDate, p.CreatedBy as CreatedBy, m.FirstName as AddedBy, EntryNo,Unit_Id ,p.InfoTypeId from ProjectAuxillaryDetails p,Project_AuxInfoTypeM q, User_M m where  p.InfoTypeId=q.InfoTypeId and m.User_Id=p.CreatedBy  and ID=@GID and ProjectUnit=@GrantType  and Deleted='N' and p.CreatedBy!=@UserId  order by EntryNo";
                        DSforgridview1.DataBind();
                        GridView1.DataBind();
                        Panel8.Visible = true;
                    }

                    if (Session["Role"].ToString() == "6"||Session["Role"].ToString() == "16")
                    {
                        DSforgridview.SelectParameters.Clear();

                        DSforgridview.SelectParameters.Add("GID", j.GID);
                        DSforgridview.SelectParameters.Add("UserId", Session["UserId"].ToString());
                        DSforgridview.SelectParameters.Add("GrantType", j.GrantType);
                        DSforgridview.SelectCommand = "select UploadPDFPath ,q.InfoTypeName as TypeName ,Remark ,AuditFrom,AuditTo, p.CreatedDate, p.CreatedBy as CreatedBy, m.FirstName as AddedBy, EntryNo,Unit_Id ,p.InfoTypeId from ProjectAuxillaryDetails p,Project_AuxInfoTypeM q, User_M m where  p.InfoTypeId=q.InfoTypeId and m.User_Id=p.CreatedBy  and ID=@GID  and ProjectUnit=@GrantType and Deleted='N' and p.CreatedBy=@UserId order by EntryNo";
                        DSforgridview.DataBind();
                        GridView1.DataBind();
                        Panel8.Visible = true;

                        DSforgridview1.SelectParameters.Clear();

                        DSforgridview1.SelectParameters.Add("GID", j.GID);
                        DSforgridview1.SelectParameters.Add("UserId", Session["UserId"].ToString());
                        DSforgridview1.SelectParameters.Add("GrantType", j.GrantType);
                        DSforgridview1.SelectCommand = "select UploadPDFPath ,q.InfoTypeName as TypeName ,Remark ,AuditFrom,AuditTo, p.CreatedDate, p.CreatedBy as CreatedBy, m.FirstName as AddedBy, EntryNo,Unit_Id ,p.InfoTypeId from ProjectAuxillaryDetails p,Project_AuxInfoTypeM q, User_M m where  p.CreatedBy!=@UserId and p.InfoTypeId=q.InfoTypeId and m.User_Id=p.CreatedBy  and ID=@GID and ProjectUnit=@GrantType and Deleted='N' order by EntryNo";
                        DSforgridview1.DataBind();
                        GVViewFile.DataBind();
                    }
                }
                else
                {
                    //ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Error!!!!!!!');</script>");
                    //DSforgridview.SelectCommand = "select UploadPDFPath ,q.InfoTypeName as TypeName ,Remark,p.CreatedDate,EntryNo,p.CreatedBy,FirstName as AddedBy,Unit_Id,p.InfoTypeId from ProjectAuxillaryDetails p,Project_AuxInfoTypeM q,User_M u where  p.InfoTypeId=q.InfoTypeId and ID='" + j.GID + "' and ProjectUnit='" + j.GrantUnit + "' and Deleted='N' and u.User_Id=p.CreatedBy  order by EntryNo";
                    //DSforgridview.DataBind();
                    //GVViewFile.DataBind();
                    string CloseWindow = "alert('Error!!!!!!!')";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);

                    if ((Session["Role"].ToString() == "11" || Session["Role"].ToString() == "1"))
                    {
                        DSforgridview.SelectParameters.Clear();
                        DSforgridview.SelectParameters.Add("UserId", Session["UserId"].ToString());
                        DSforgridview.SelectParameters.Add("GID", j.GID);
                        DSforgridview.SelectParameters.Add("GrantType", j.GrantType);
                        DSforgridview.SelectCommand = "select UploadPDFPath ,q.InfoTypeName as TypeName ,Remark ,AuditFrom,AuditTo, p.CreatedDate, p.CreatedBy as CreatedBy, m.FirstName as AddedBy, EntryNo,Unit_Id ,p.InfoTypeId from ProjectAuxillaryDetails p,Project_AuxInfoTypeM q, User_M m where  p.CreatedBy=@UserId and p.InfoTypeId=q.InfoTypeId and m.User_Id=p.CreatedBy  and ID=@GID and ProjectUnit=@GrantType  and Deleted='N' order by EntryNo";
                        DSforgridview.DataBind();
                        GVViewFile.DataBind();

                        DSforgridview1.SelectParameters.Clear();

                        DSforgridview1.SelectParameters.Add("GID", j.GID);
                        DSforgridview1.SelectParameters.Add("GrantUnit", j.GrantUnit);
                        DSforgridview1.SelectParameters.Add("GrantType", j.GrantType);
                        DSforgridview1.SelectCommand = "select UploadPDFPath ,q.InfoTypeName as TypeName ,Remark ,AuditFrom,AuditTo, p.CreatedDate, p.CreatedBy as CreatedBy, m.FirstName as AddedBy, EntryNo,Unit_Id ,p.InfoTypeId from ProjectAuxillaryDetails p,Project_AuxInfoTypeM q, User_M m where  p.InfoTypeId=q.InfoTypeId and m.User_Id=p.CreatedBy  and ID=@GID and ProjectUnit=@GrantType and Deleted='N' and p.CreatedBy  not in  (Select CreatedBy from ProjectAuxillaryDetails where ProjectUnit=@GrantUnit and ID=@GID  and Deleted='N')  order by EntryNo";
                        DSforgridview1.DataBind();
                        GridView1.DataBind();
                        Panel8.Visible = true;
                    }

                    if (Session["Role"].ToString() == "6"||Session["Role"].ToString() == "16")
                    {
                        DSforgridview.SelectParameters.Clear();

                        DSforgridview.SelectParameters.Add("GID", j.GID);
                        DSforgridview.SelectParameters.Add("GrantUnit", j.GrantUnit);
                        DSforgridview.SelectParameters.Add("GrantType", j.GrantType);
                        DSforgridview.SelectCommand = "select UploadPDFPath ,q.InfoTypeName as TypeName ,Remark ,AuditFrom,AuditTo, p.CreatedDate, p.CreatedBy as CreatedBy, m.FirstName as AddedBy, EntryNo,Unit_Id ,p.InfoTypeId from ProjectAuxillaryDetails p,Project_AuxInfoTypeM q, User_M m where  p.InfoTypeId=q.InfoTypeId and m.User_Id=p.CreatedBy  and ID=@GID and ProjectUnit=@GrantType and Deleted='N' and p.CreatedBy  not in  (Select CreatedBy from ProjectAuxillaryDetails where ProjectUnit=@GrantUnit and ID=@GID and Deleted='N')  order by EntryNo";
                        DSforgridview.DataBind();
                        GridView1.DataBind();
                        Panel8.Visible = true;

                        DSforgridview1.SelectParameters.Clear();
                        DSforgridview1.SelectParameters.Add("UserId", Session["UserId"].ToString());
                        DSforgridview1.SelectParameters.Add("GID", j.GID);
                        DSforgridview1.SelectParameters.Add("GrantType", j.GrantType);
                        DSforgridview1.SelectCommand = "select UploadPDFPath ,q.InfoTypeName as TypeName ,Remark ,AuditFrom,AuditTo, p.CreatedDate, p.CreatedBy as CreatedBy, m.FirstName as AddedBy, EntryNo,Unit_Id ,p.InfoTypeId from ProjectAuxillaryDetails p,Project_AuxInfoTypeM q, User_M m where  p.CreatedBy!=@UserId and p.InfoTypeId=q.InfoTypeId and m.User_Id=p.CreatedBy  and ID=@GID and ProjectUnit=@GrantType and Deleted='N' order by EntryNo";
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
        //popupPanelAffil.Visible = true;
        //popGridAffil.DataSourceID = "SqlDataSourceAffil";
        //SqlDataSourceAffil.DataBind();
        //popGridAffil.DataBind();
        //popGridAffil.Visible = true;
        //popup.Visible = true;
        popupPanelAffil.Visible = true;
        //popupPanelAffilUpdate.Update();
        popGridAffil.DataSourceID = "SqlDataSourceAffil";
        SqlDataSourceAffil.DataBind();
        popGridAffil.DataBind();
        popupStudentGrid.DataSourceID = "StudentSQLDS";
        StudentSQLDS.DataBind();
        popupStudentGrid.DataBind();
    }
    protected void exit(object sender, EventArgs e)
    {
        UpdatePanel1.Update();

        affiliateSrch.Text = "";
        popGridAffil.DataBind();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "CallMyFunction", "ToggleDisplay3()", true);

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
        string rowVal1 = Request.Form["rowIndx"];
        int rowIndex = Convert.ToInt32(rowVal1);
        int row = Convert.ToInt16(rowVal.Value);
        DropDownList munonmu = (DropDownList)Grid_AuthorEntry.Rows[row].FindControl("DropdownMuNonMu");
        if (munonmu.SelectedValue == "M")
        {
            //popupPanelAffil.Visible = true;
            //popupstudent.Visible = false;
            popupPanelAffil.Style.Add("display", "true");
            //popupstudent.Style.Add("display", "none");
        }
        else if (munonmu.SelectedValue == "S")
        {
            //popupPanelAffil.Visible = false;
            //popupstudent.Visible = true;
            //popupPanelAffil.Style.Add("display", "none");
            popupstudent.Style.Add("display", "true");
        }
        //else if (munonmu.SelectedValue == "O")
        //{
        //    //popupPanelAffil.Visible = false;
        //    //popupstudent.Visible = false;
        //    popupPanelAffil.Style.Add("display", "none");
        //    popupstudent.Style.Add("display", "none");
        //}
        //ModalPopupExtender ModalPopupExtender8 = (ModalPopupExtender)Grid_AuthorEntry.Rows[row].FindControl("ModalPopupExtender4");
        //ModalPopupExtender8.Show();

        //string rowVal = Request.Form["rowIndx"];
        //int rowIndex = Convert.ToInt32(rowVal);

        //ModalPopupExtender ModalPopupExtender8 = (ModalPopupExtender)Grid_AuthorEntry.Rows[rowIndex].FindControl("ModalPopupExtender4");
        //ModalPopupExtender8.Show();

    }

    //on row select of pop up autor
    protected void popSelected1(Object sender, EventArgs e)
    {
        UpdatePanel1.Update();
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

        TextBox employc = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("EmployeeCode");
        employc.Text = EmployeeCode1;

        TextBox AuthorName = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("AuthorName");
        AuthorName.Text = a.UserNamePrefix + " " + a.UserFirstName + " " + a.UserMiddleName + " " + a.UserLastName;


        affiliateSrch.Text = "";
        popGridAffil.DataBind();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "CallMyFunction", "ToggleDisplay2()", true);

    }

    //Functionality- PopUp Agency
    protected void setModalWindowAgency(object sender, EventArgs e)
    {
        popupselectNo.Visible = true;
        string a = DropDownListGrUnit.SelectedValue;
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
            SqlDataSourceTextBoxGrantAgency.SelectCommand = "SELECT  FundingAgencyId as Id,UPPER([FundingAgencyName]) as Name FROM [ProjectFundingAgency_M] where AgentType=@AgentType";
            SqlDataSourceTextBoxGrantAgency.DataBind();
            popGridagency.DataSourceID = "SqlDataSourceTextBoxGrantAgency";
            SqlDataSourceTextBoxGrantAgency.DataBind();
            popGridagency.DataBind();
            popGridagency.Visible = true;
        }


    }
    protected void popGridagency_pageindex(object sender, GridViewPageEventArgs e)
    {
        //popupselectNo.Visible = true;
        //model.Show();
        //string a = DropDownListGrUnit.SelectedValue;

        //SqlDataSourceTextBoxGrantAgency.SelectCommand = "SELECT  FundingAgencyId as Id,UPPER([FundingAgencyName]) as Name FROM [ProjectFundingAgency_M] where AgentType='" + a + "'";
        //SqlDataSourceTextBoxGrantAgency.DataBind();

        //popGridagency.DataSourceID = "SqlDataSourceTextBoxGrantAgency";
        //popGridagency.DataBind();
        //popGridagency.Visible = true;
        //popGridagency.AllowPaging = true;
        //popGridagency.PageIndex = e.NewPageIndex;
        //UpdatePanel5.Update();
        //popupselectNo.Visible = true;
        //MainpanelGrant.Visible = true;
        //Panel7.Visible = true;
        //ScriptManager.RegisterStartupScript(this, this.GetType(), "CallMyFunction", "ToggleDisplay1()", true);


        setModalWindow1(sender, e);
        //ScriptManager.RegisterStartupScript(this, this.GetType(), "CallMyFunction", "callthis1()", true);
        //return;

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

        UpdatePanel5.Update();
        popupselectNo.Visible = true;
        MainpanelGrant.Visible = true;
        Panel7.Visible = true;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "CallMyFunction", "ToggleDisplay1()", true);

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
        UpdatePanel19.Update();

        if (!Page.IsValid)
        {
            return;
        }
        string AppendInstitutionNamess = null;
        int countCorrAuth = 0;
        int countAuthType = 0;

        int countLeadPI = 0;
        int countLeadPIS = 0;
        int countLeadPIF = 0;
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
                    //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Grant Amount must be in numbers!')</script>");
                    string CloseWindow = "alert('Grant Amount must be in numbers!')";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);

                    TextBoxGrantAmt.Text = "";
                    return;
                }
            }

            string GSource = DropDownListSourceGrant.SelectedValue;
            string GType = DropDownListTypeGrant.SelectedValue;
            Session["EntryType"] = GType;
            Session["ID"] = GId;

            string GSectorlevel = DropDownSectorLevel.SelectedValue;
            string GAgencyType = DropDownAgencyType.SelectedValue;
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
                    //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Applied Date  must be greater than" + cutoff + "')</script>");
                    string CloseWindow = "alert('Applied Date  must be greater than" + cutoff + "')";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);

                    TextBoxGrantDate.Text = "";
                    return;
                }
            }
            else
            {
                //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please enter a applied date')</script>");
                string CloseWindow = "alert('Please enter a applied date')";
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow, true);
                return;
            }
            if (TextBoxTitle.Text == "")
            {
                //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please enter title of the project')</script>");
                string CloseWindow = "alert('Please enter title of the project')";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);

                return;
            }

            if (txtagency.Text == "")
            {
                //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please enter project agency')</script>");
                string CloseWindow = "alert('Please enter project agency')";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);
    
                return;
            }
            int CountSancInfoTp = 0;
            if (DropDownListProjStatus.SelectedValue == "SAN")
            {
                Business b1 = new Business();
                CountSancInfoTp = b1.SelectCountUploadSanctionInformationType(TextBoxID.Text, DropDownListGrUnit.SelectedValue);
                if (CountSancInfoTp == 0)
                {
                    //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('File should be uploaded for the information type-Sanctioned Details!')</script>");
                    string CloseWindow = "alert('File should be uploaded for the information type-Sanctioned Details!')";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);
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
            j.FundingSectorLevelGrant =Convert.ToInt32( GSectorlevel);
            j.TypeofAgencyGrant = Convert.ToInt32(GAgencyType);
            j.RejectBy = Session["UserId"].ToString();
            j.RejectFeedback = TextBoxRemarks.Text.Trim();

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
                            //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Project details already exists!')</script>");
                            string CloseWindow = "alert('Project details already exists!!')";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);
 
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
                //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please enter Account Head!')</script>");
                string CloseWindow = "alert('Please enter Account Head!)";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);

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
                        //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please enter Account Head!')</script>");
                        string CloseWindow = "alert('Please enter Account Head!')";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);

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
                        //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please enter Investigator Name!')</script>");
                        string CloseWindow = "alert('Please enter Investigator Name!')";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);

                        return;

                    }

                    if (AuthorType.Text == "")
                    {
                        //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please select Investigator Type!')</script>");
                        string CloseWindow = "alert('Please select Investigator Type!')";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);

                        return;

                    }
                    if (DropdownMuNonMu.SelectedValue == "M")
                    {
                        if (InstitutionName.Text == "")
                        {
                            //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please enter Institution Name!')</script>");
                            string CloseWindow = "alert('Please enter Institution Name!')";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);

                            return;

                        }

                        if (DepartmentName.Text == "")
                        {
                            //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please enter Department Name!')</script>");
                            string CloseWindow = "alert('Please enter Department Name!')";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);

                            return;

                        }
                    }
                    else if (DropdownMuNonMu.SelectedValue == "N" || DropdownMuNonMu.SelectedValue == "E")
                    {
                        if (InstitutionName.Text == "")
                        {
                            //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please enter Institution Name!')</script>");
                            string CloseWindow = "alert('Please enter Institution Name!!')";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);

                            return;

                        }

                        if (DepartmentName.Text == "")
                        {
                            //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please enter Department Name!')</script>");
                            string CloseWindow = "alert('Please enter Department Name!')";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);
  
                            return;

                        }
                    }
                    if (DropdownMuNonMu.SelectedValue == "M" || DropdownMuNonMu.SelectedValue == "S")
                    {
                        if (MailId.Text == "")
                        {
                            //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please enter MailId!')</script>");
                            string CloseWindow = "alert('Please enter MailId!')";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);

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
                        if (JD[i].MUNonMU == "E")
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
                    if (JD[i].LeadPI == "Y")
                    {
                        if (JD[i].MUNonMU == "S")
                        {
                            countLeadPIS = countLeadPIS + 1;
                        }
                        else if (JD[i].MUNonMU == "M")
                        {
                            countLeadPIF = countLeadPIF + 1;
                        }
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
                            //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please enter Received Date!!')</script>");
                            string CloseWindow = "alert('Please enter Received Date!!)";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);
  
                            return;
                        }

                        if (INREquivalent.Text == "")
                        {
                            //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please enter INR Equivalent amount!!')</script>");
                            string CloseWindow = "alert('Please enter INR Equivalent amount!!')";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);
 
                            return;
                        }



                        Regex regex = new Regex("^([0-9]{1,3},([0-9]{3},)*[0-9]{3}|[0-9]+)(.[0-9][0-9]*$)?$");
                        if (INREquivalent.Text != "" && !regex.IsMatch(INREquivalent.Text))
                        {
                            //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('INR Equivalent must be in numbers!')</script>");
                            string CloseWindow = "alert('INR Equivalent must be in numbers!')";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);

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
                                //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please enter Received Date!!')</script>");
                                string CloseWindow = "alert('Please enter Received Date!!')";
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);

                                return;
                            }

                            if (INREquivalent.Text == "")
                            {
                                //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please enter INR Equivalent amount!!')</script>");
                                string CloseWindow = "alert('Please enter INR Equivalent amount!!')";
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);

                                return;
                            }



                            Regex regex = new Regex("^([0-9]{1,3},([0-9]{3},)*[0-9]{3}|[0-9]+)(.[0-9][0-9]*$)?$");
                            if (INREquivalent.Text != "" && !regex.IsMatch(INREquivalent.Text))
                            {
                                //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('INR Equivalent must be in numbers!')</script>");
                                string CloseWindow = "alert('INR Equivalent must be in numbers!')";
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);

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

            DataTable dtCurrentTableRecevie = (DataTable)ViewState["Bank"];
            DataTable dtCurrentTableOverHead = (DataTable)ViewState["OverheadT"];
            RecieptData[] JD2 = null;
            IncentiveData[] JD4 = null;
            GrantData[] OHD = null;
            GrantData journalbank = new GrantData();
            DataTable dtCurrentTable3 = (DataTable)ViewState["IncentiveDetails"];

            GrantData journalbank2 = new GrantData();

            GrantData journalbank1 = new GrantData();
            //insert Fund Reciept

            //Bank Details
            if (Session["Role"].ToString() == "6")
            {

                JD2 = new RecieptData[dtCurrentTableRecevie.Rows.Count];
                JD4 = new IncentiveData[dtCurrentTable3.Rows.Count];
                OHD = new GrantData[dtCurrentTableOverHead.Rows.Count];
                //Received
                int rowIndex2 = 0;
                if (dtCurrentTableRecevie.Rows.Count > 0)
                {

                    for (int i = 0; i < dtCurrentTableRecevie.Rows.Count; i++)
                    {
                        JD2[i] = new RecieptData();
                        DropDownList SanctionEntryNumber = (DropDownList)GridView_bank.Rows[0].Cells[0].FindControl("ddlSanctionEntryNo");

                        DropDownList CurrencyCode = (DropDownList)GridView_bank.Rows[rowIndex2].Cells[1].FindControl("CurrencyCode");
                        DropDownList ModeOfReceive = (DropDownList)GridView_bank.Rows[rowIndex2].Cells[2].FindControl("ModeOfRecevie");
                        TextBox ReceviedDate = (TextBox)GridView_bank.Rows[rowIndex2].Cells[3].FindControl("ReceviedDate");
                        TextBox ReceviedAmount = (TextBox)GridView_bank.Rows[rowIndex2].Cells[4].FindControl("ReceviedAmount");
                        TextBox ReceviedINR = (TextBox)GridView_bank.Rows[rowIndex2].Cells[5].FindControl("ReceviedINR");
                        TextBox TDS = (TextBox)GridView_bank.Rows[rowIndex2].Cells[6].FindControl("TDS");
                        TextBox ReferenceNo = (TextBox)GridView_bank.Rows[rowIndex2].Cells[7].FindControl("ReferenceNo");
                        //TextBox BankName = (TextBox)GridView_bank.Rows[rowIndex2].Cells[8].FindControl("BankName");
                        ImageButton EmployeeCodeBtn1 = (ImageButton)GridView_bank.Rows[rowIndex2].Cells[8].FindControl("EmployeeCodeBtn1");
                        //TextBox BankId = (TextBox)GridView_bank.Rows[rowIndex2].Cells[8].FindControl("BankId");
                        TextBox ReceivedBankID = (TextBox)GridView_bank.Rows[rowIndex2].Cells[9].FindControl("ReceivedBankId");
                        // Id// 
                        TextBox ReceivedBank = (TextBox)GridView_bank.Rows[rowIndex2].Cells[9].FindControl("ReceivedBank");
                        ImageButton EmployeeCodeBtn2 = (ImageButton)GridView_bank.Rows[rowIndex2].Cells[9].FindControl("EmployeeCodeBtn2");
                        TextBox CreditedBankId = (TextBox)GridView_bank.Rows[rowIndex2].Cells[10].FindControl("CreditedBankId");
                        // Id// 
                        TextBox CreditedBank = (TextBox)GridView_bank.Rows[rowIndex2].Cells[10].FindControl("CreditedBank");
                        ImageButton EmployeeCodeBtn3 = (ImageButton)GridView_bank.Rows[rowIndex2].Cells[10].FindControl("EmployeeCodeBtn3");
                        TextBox ReceivedNarration = (TextBox)GridView_bank.Rows[rowIndex2].Cells[11].FindControl("ReceivedNarration");
                        string SanctionEntryNumber1 = SanctionEntryNumber.SelectedValue.Trim();
                        string CurrencyCode1 = CurrencyCode.SelectedValue.Trim();
                        string ModeOfReceive1 = ModeOfReceive.SelectedValue.Trim();
                        string date = ReceviedDate.Text.Trim();
                        string ReceviedAmmount = ReceviedAmount.Text.Trim();
                        string ReceviedINR1 = ReceviedINR.Text.Trim();
                        string TDS1 = TDS.Text.Trim();
                        string ReferenceNo1 = ReferenceNo.Text.Trim();
                        //string BankName1 = BankName.Text.Trim();
                        //string BankID = BankId.Text.Trim();
                        //string ReceivedBank1 = ReceivedBank.Text.Trim();
                        //string ReceivedBankId = ReceivedBankID.Text.Trim();
                        //string CreditedBank1 = CreditedBank.Text.Trim();


                        string ReceivedNarration1 = ReceivedNarration.Text.Trim();
                        if (dtCurrentTableRecevie.Rows.Count == 1)
                        {
                            if (date == "" && ReceviedAmmount == "" && ReceviedINR1 == "")
                            {

                            }

                            else
                            {
                                journalbank.GID = TextBoxID.Text;
                                journalbank.GrantUnit = DropDownListGrUnit.SelectedValue;

                                JD2[i].FRSanctionEntryNo = Convert.ToInt16(SanctionEntryNumber.SelectedValue);
                                JD2[i].CurrencyCode = CurrencyCode.SelectedValue;
                                JD2[i].ModeOfReceive = ModeOfReceive.SelectedValue;
                                JD2[i].ReceviedDate = Convert.ToDateTime(ReceviedDate.Text.Trim());
                                JD2[i].ReceviedAmmount = Convert.ToDouble(ReceviedAmount.Text.Trim());
                                JD2[i].TDS = Convert.ToDouble(TDS.Text.Trim());
                                JD2[i].ReferenceNumber = ReferenceNo.Text.Trim();

                                //JD2[i].BankName = BankId.Text.Trim();
                                JD2[i].CreditedBankName = CreditedBankId.Text.Trim();
                                if (ReceviedINR.Text.Trim() != "")
                                {
                                    JD2[i].ReceviedINR = Convert.ToDouble(ReceviedINR.Text.Trim());
                                }

                                //JD2[i].BankID = Convert.ToInt16(BankId.Text);//id
                                JD2[i].ReceivedBank = ReceivedBankID.Text.Trim();//id
                                JD2[i].CreditedBank = CreditedBankId.Text.Trim();//id
                                JD2[i].ReceivedNarration = ReceivedNarration.Text;
                                journalbank.FinanceProjectStatus = DropDownList3.SelectedValue;
                                if (DropDownList2.SelectedValue != "select")
                                {
                                    journalbank.ServiceTaxApplicable = DropDownList2.SelectedValue;
                                }
                                if (TextBox3.Text.Trim() != "")
                                {
                                    journalbank.DateOfCompletion = Convert.ToDateTime(TextBox3.Text);
                                }
                                journalbank.Remarks = TextBox4.Text;
                            }
                        }
                        else
                        {

                            journalbank.GID = TextBoxID.Text;
                            journalbank.GrantUnit = DropDownListGrUnit.SelectedValue;

                            JD2[i].FRSanctionEntryNo = Convert.ToInt16(SanctionEntryNumber.SelectedValue);
                            JD2[i].CurrencyCode = CurrencyCode.SelectedValue;
                            JD2[i].ModeOfReceive = ModeOfReceive.SelectedValue;
                            JD2[i].ReceviedDate = Convert.ToDateTime(ReceviedDate.Text.Trim());
                            JD2[i].ReceviedAmmount = Convert.ToDouble(ReceviedAmount.Text.Trim());
                            JD2[i].TDS = Convert.ToDouble(TDS.Text.Trim());
                            JD2[i].ReferenceNumber = ReferenceNo.Text.Trim();

                            //JD2[i].BankName = BankName.Text.Trim();
                            //JD2[i].ReceivedBankName = ReceivedBankName.Text.Trim();
                            //JD2[i].CreditedBankName = CreditedBankName.Text.Trim();
                            if (ReceviedINR.Text.Trim() != "")
                            {
                                JD2[i].ReceviedINR = Convert.ToDouble(ReceviedINR.Text.Trim());
                            }

                            //JD2[i].BankID = Convert.ToInt16(BankId.Text);//id
                            JD2[i].ReceivedBank = ReceivedBankID.Text.Trim();//id
                            JD2[i].CreditedBank = CreditedBankId.Text.Trim();//id
                            JD2[i].ReceivedNarration = ReceivedNarration.Text;

                            journalbank.FinanceProjectStatus = DropDownList3.SelectedValue;
                            if (DropDownList2.SelectedValue != "select")
                            {
                                journalbank.ServiceTaxApplicable = DropDownList2.SelectedValue;
                            }
                            if (TextBox3.Text.Trim() != "")
                            {
                                journalbank.DateOfCompletion = Convert.ToDateTime(TextBox3.Text);
                            }
                            journalbank.Remarks = TextBox4.Text;
                        }
                        rowIndex2++;
                    }
                }



                //Insert Incentive

                //incentive

                int rowIndex4 = 0;
                if (dtCurrentTable3 != null)
                {
                    if (dtCurrentTable3.Rows.Count > 0)
                    {

                        for (int i = 0; i < dtCurrentTable3.Rows.Count; i++)
                        {
                            JD4[i] = new IncentiveData();
                            DropDownList SanctionEntryNumber = (DropDownList)gvIncentiveDetails.Rows[0].Cells[0].FindControl("ddlSanctionEntryNo");
                            TextBox txtincentivedate = (TextBox)gvIncentiveDetails.Rows[rowIndex4].Cells[0].FindControl("txtincentivedate");
                            TextBox txtincentiveAmount = (TextBox)gvIncentiveDetails.Rows[rowIndex4].Cells[2].FindControl("txtincentiveAmount");
                            TextBox txtComments = (TextBox)gvIncentiveDetails.Rows[rowIndex4].Cells[2].FindControl("txtComments");

                            string date = txtincentivedate.Text.Trim();
                            string ReceviedAmmount = txtincentiveAmount.Text.Trim();
                            string narration = txtComments.Text.Trim();
                            if (dtCurrentTable3.Rows.Count == 1)
                            {
                                if (date == "" && ReceviedAmmount == "" && narration == "")
                                {

                                }

                                else
                                {
                                    journalbank2.GID = TextBoxID.Text;
                                    JD4[i].SanctionEntryNo = Convert.ToInt16(SanctionEntryNumber.SelectedValue);
                                    journalbank2.GrantUnit = DropDownListGrUnit.SelectedValue;
                                    journalbank2.FinanceProjectStatus = DropDownList3.SelectedValue;
                                    if (TextBox3.Text.Trim() != "")
                                    {
                                        journalbank2.DateOfCompletion = Convert.ToDateTime(TextBox3.Text);
                                    }
                                    journalbank2.Remarks = TextBox4.Text;
                                    if (DropDownList2.SelectedValue != "select")
                                    {
                                        journalbank2.ServiceTaxApplicable = DropDownList2.SelectedValue;
                                    }
                                    JD4[i].IncentivePayDate = Convert.ToDateTime(txtincentivedate.Text.Trim());
                                    JD4[i].IncentivePayAmount = Convert.ToDouble(txtincentiveAmount.Text.Trim());
                                    JD4[i].Narration = txtComments.Text;
                                    journalbank.FinanceProjectStatus = DropDownList3.SelectedValue;
                                    if (DropDownList2.SelectedValue != "select")
                                    {
                                        journalbank.ServiceTaxApplicable = DropDownList2.SelectedValue;
                                    }
                                    if (TextBox3.Text.Trim() != "")
                                    {
                                        journalbank.DateOfCompletion = Convert.ToDateTime(TextBox3.Text);
                                    }
                                    journalbank.Remarks = TextBox4.Text;
                                }
                            }
                            else
                            {
                                JD4[i].SanctionEntryNo = Convert.ToInt16(SanctionEntryNumber.SelectedValue);
                                journalbank2.GID = TextBoxID.Text;
                                journalbank2.GrantUnit = DropDownListGrUnit.SelectedValue;
                                journalbank2.FinanceProjectStatus = DropDownList3.SelectedValue;
                                if (TextBox3.Text.Trim() != "")
                                {
                                    journalbank2.DateOfCompletion = Convert.ToDateTime(TextBox3.Text);
                                }
                                journalbank2.Remarks = TextBox4.Text;
                                if (DropDownList2.SelectedValue != "select")
                                {
                                    journalbank2.ServiceTaxApplicable = DropDownList2.SelectedValue;
                                }
                                JD4[i].IncentivePayDate = Convert.ToDateTime(txtincentivedate.Text.Trim());
                                JD4[i].IncentivePayAmount = Convert.ToDouble(txtincentiveAmount.Text.Trim());
                                JD4[i].Narration = txtComments.Text;
                                journalbank.FinanceProjectStatus = DropDownList3.SelectedValue;
                                if (DropDownList2.SelectedValue != "select")
                                {
                                    journalbank.ServiceTaxApplicable = DropDownList2.SelectedValue;
                                }
                                if (TextBox3.Text.Trim() != "")
                                {
                                    journalbank.DateOfCompletion = Convert.ToDateTime(TextBox3.Text);
                                }
                                journalbank.Remarks = TextBox4.Text;
                            }
                            rowIndex4++;
                        }
                    }
                }


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
                            string date = OHReceviedDate.Text.Trim();
                            string ReceviedAmmount = OHReceviedAmount.Text.Trim();
                            string OHJVNumber1 = OHJVNumber.Text.Trim();
                            string OHNarration1 = OHNarration.Text.Trim();


                            if (dtCurrentTableOverHead.Rows.Count == 1)
                            {
                                if (date == "" && ReceviedAmmount == "" && OHJVNumber1 == "" && OHNarration1 == "")
                                {

                                }
                                else
                                {
                                    journalbank1.GID = TextBoxID.Text;
                                    journalbank1.GrantUnit = DropDownListGrUnit.SelectedValue;
                                    journalbank1.FinanceProjectStatus = DropDownList3.SelectedValue;
                                    if (TextBox3.Text.Trim() != "")
                                    {
                                        journalbank1.DateOfCompletion = Convert.ToDateTime(TextBox3.Text);
                                    }
                                    journalbank1.Remarks = TextBox4.Text;
                                    if (DropDownList2.SelectedValue != "select")
                                    {
                                        journalbank1.ServiceTaxApplicable = DropDownList2.SelectedValue;
                                    }
                                    OHD[i].OHSanctionEntryNo = Convert.ToInt16(OHSanctionEntryNumber.Text.Trim());
                                    OHD[i].OverheadTDate = Convert.ToDateTime(OHReceviedDate.Text.Trim());
                                    OHD[i].OverheadTAmount = Convert.ToDouble(OHReceviedAmount.Text.Trim());
                                    OHD[i].JVNumber = OHJVNumber.Text.Trim();
                                    OHD[i].OverheadNarration = OHNarration.Text.Trim();
                                    journalbank.FinanceProjectStatus = DropDownList3.SelectedValue;
                                    if (DropDownList2.SelectedValue != "select")
                                    {
                                        journalbank.ServiceTaxApplicable = DropDownList2.SelectedValue;
                                    }
                                    if (TextBox3.Text.Trim() != "")
                                    {
                                        journalbank.DateOfCompletion = Convert.ToDateTime(TextBox3.Text);
                                    }
                                    journalbank.Remarks = TextBox4.Text;
                                }


                            }
                            else
                            {
                                journalbank1.GID = TextBoxID.Text;
                                journalbank1.GrantUnit = DropDownListGrUnit.SelectedValue;
                                journalbank1.FinanceProjectStatus = DropDownList3.SelectedValue;
                                if (TextBox3.Text.Trim() != "")
                                {
                                    journalbank1.DateOfCompletion = Convert.ToDateTime(TextBox3.Text);
                                }
                                journalbank1.Remarks = TextBox4.Text;
                                if (DropDownList2.SelectedValue != "select")
                                {
                                    journalbank1.ServiceTaxApplicable = DropDownList2.SelectedValue;
                                }
                                OHD[i].OHSanctionEntryNo = Convert.ToInt16(OHSanctionEntryNumber.Text.Trim());
                                OHD[i].OverheadTDate = Convert.ToDateTime(OHReceviedDate.Text.Trim());
                                OHD[i].OverheadTAmount = Convert.ToDouble(OHReceviedAmount.Text.Trim());
                                OHD[i].JVNumber = OHJVNumber.Text.Trim();
                                OHD[i].OverheadNarration = OHNarration.Text.Trim();
                                journalbank.FinanceProjectStatus = DropDownList3.SelectedValue;
                                if (DropDownList2.SelectedValue != "select")
                                {
                                    journalbank.ServiceTaxApplicable = DropDownList2.SelectedValue;
                                }
                                if (TextBox3.Text.Trim() != "")
                                {
                                    journalbank.DateOfCompletion = Convert.ToDateTime(TextBox3.Text);
                                }
                                journalbank.Remarks = TextBox4.Text;
                            }
                            rowIndex3++;
                        }


                    }
                }
            }



            //if (countAuthType > 2)
            //{
            //    ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Primary Investigator cannot be more than two!')</script>");
            //    return;

            //}

            if (DropDownListProjStatus.SelectedValue == "APP")
            {
                if (countAuthType == 0)
                {
                    //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Select atleast one Investigator Type as Primary Investigator !')</script>");
                    string CloseWindow = "alert('Select atleast one Investigator Type as Primary Investigator')";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);

                    return;

                }


                if (countLeadPI > 1)
                {
                    //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Lead PI cannot be more than one!')</script>");
                    string CloseWindow = "alert('Lead PI cannot be more than one !')";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true); 
                    return;

                }

                if (countLeadPI == 0)
                {
                    //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Select atleast one Lead PI!')</script>");
                    string CloseWindow = "alert('Select atleast one Lead PI!')";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);
                    return;

                }
            }
            if (DropDownListTypeGrant.SelectedValue == "GS")
            {
                if (countLeadPIS == 0)
                {
                    //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Select atleast one Lead PI as Student!')</script>");
                    string CloseWindow = "alert('Select atleast one Lead PI as Student!!')";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true); 
                    return;

                }
            }
            else if (DropDownListTypeGrant.SelectedValue == "SG")
            {
                if (countLeadPIF == 0)
                {
                    //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Select atleast one Lead PI as Faculty!')</script>");
                    string CloseWindow = "alert('Select atleast one Lead PI as Faculty!')";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);
                    return;

                }
            }
            if (TextBoxID.Text == "")
            {
                int result = 0;
                result = B.insertGrantEntry(j, JD);

                if (result >= 1)
                {
                    TextBoxID.Text = Session["Grantseed"].ToString();
                    TextBoxUTN.Text = Session["GrantseedUTNseed"].ToString();
                    btnSave.Enabled = false;
                    //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Grant data Created Successfully.. of ID: " + TextBoxID.Text + " For update Click on search and edit  !')</script>");
                    string CloseWindow = "alert('Grant data Created Successfully.. of ID: " + TextBoxID.Text + " For update Click on search and edit!')";
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow, true);
                    TextBoxID.Text = Session["Grantseed"].ToString();
                    TextBoxUTN.Text = Session["GrantseedUTNseed"].ToString();
                    log.Info("Grant created Successfully, of ID: " + TextBoxID.Text);
                    ButtonSavepdf.Enabled = true;
                    EmailDetails details = new EmailDetails();
                    details = SendMail();
                    details.Id = TextBoxID.Text;
                    details.Type = DropDownListTypeGrant.SelectedItem.ToString();
                    details.ProjectUnit = DropDownListGrUnit.SelectedItem.ToString();
                    details.UnitId = DropDownListGrUnit.SelectedValue.ToString();
                    SendMailObject obj1 = new SendMailObject();
                    bool result1 = obj1.InsertIntoEmailQueue(details);
                    GrantData obj = new GrantData();
                    Business obj2 = new Business();
                    obj = B.CheckUniqueId(TextBoxID.Text, DropDownListGrUnit.SelectedValue, details);
                    if (obj.Module == details.EmailSubject + details.Module)
                    {
                        int data = obj2.updateEmailtracker(TextBoxID.Text, DropDownListGrUnit.SelectedValue, details, obj);
                    }

                    ButtonSearchProjectOnClick(sender, e);

                    BtnSave_Click1(sender, e);
                    UpdatePanel6.Update();
                    TextBoxID.Text = Session["Grantseed"].ToString();
                    TextBoxUTN.Text = Session["GrantseedUTNseed"].ToString();
                    //UpdatePanel19.Update();
                    //UpdatePanel18.Update();
                    //string Type = "Prj";
                    ////BtnSave_Click(sender, e);                   
                    //    string MemberID = Session["UserId"].ToString();
                    //    FeedbackClass u = new FeedbackClass();
                    //    Journal_DataObject Da = new Journal_DataObject();
                    //    u = B.CheckUserforFeedback(MemberID, Type);

                    //    string date3 = Convert.ToString(u.ProjectUpdatedDate);

                    //    if (date3 == "01/01/0001 00:00:00")
                    //    {

                    //        UpdatePanel18.Update();
                    //        panelfeedback.Visible = true;
                    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "CallMyFunction", "callthis7()", true);
                    //        return;

                    //    }
                    //    else
                    //    {

                    //        string monthn = ConfigurationManager.AppSettings["FeedBackMonth"].ToString();
                    //        int month1 = Convert.ToInt32(monthn);
                    //        //DateTime actDate = Convert.ToDateTime(u.PublicationUpdatedDate);


                    //        DateTime fromdate = Convert.ToDateTime(u.ProjectUpdatedDate);
                    //        DateTime todaydate = DateTime.Now;

                    //        int resu = B.gettotalmonths(fromdate, todaydate);
                    //        if (resu >= month1)
                    //        {

                    //            UpdatePanel18.Update();
                    //            panelfeedback.Visible = true;
                    //            ScriptManager.RegisterStartupScript(this, this.GetType(), "CallMyFunction", "callthis7()", true);
                    //            return;
                    //        }
                    //        else
                    //        {

                    //        }
                    //    }

                    


                }
                else
                {
                    log.Error("Grant creation Error of ID: " + TextBoxID.Text);
                    //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Grant Error')</script>");
                    string CloseWindow = "alert('Grant Error!!')";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);

                }
            }
            else
            {

                int result = 0;
                int result1 = 0;
                if (Session["Role"].ToString() != "6" || Session["Role"].ToString() == "11" || Session["Role"].ToString() == "1")
                {

                    if (DropDownListProjStatus.SelectedValue == "SAN")
                    {
                        if (PanelPercentage.Visible == true)
                        {
                            if (DropDownListSanType.SelectedValue == "CA")
                            {

                                DataTable dtCurrentTableIO = (DataTable)ViewState["CurrentTableIO"];
                                GrantData[] PO = new GrantData[dtCurrentTableIO.Rows.Count];
                                DataTable dtCurrentTableII = (DataTable)ViewState["CurrentTableII"];
                                GrantData[] PI = new GrantData[dtCurrentTableII.Rows.Count];
                                int rowIndexIO = 0;
                                int temp = 0;
                                int temp1 = 0;
                                int tempI = 0;
                                if (dtCurrentTableIO.Rows.Count > 0)
                                {
                                    for (int p = 0; p < dtCurrentTableIO.Rows.Count; p++)
                                    {
                                        tempI = 0;
                                        PO[p] = new GrantData();
                                        DropDownList DropdownMuNonMu = (DropDownList)GridViewInterOrganization.Rows[rowIndexIO].Cells[3].FindControl("DropdownMuNonMuIO");
                                        TextBox InstNme = (TextBox)GridViewInterOrganization.Rows[rowIndexIO].Cells[2].FindControl("InstitutionNameIO");
                                        HiddenField InstId = (HiddenField)GridViewInterOrganization.Rows[rowIndexIO].Cells[2].FindControl("InstitutionIO");
                                        TextBox Percent = (TextBox)GridViewInterOrganization.Rows[rowIndexIO].Cells[2].FindControl("PercentageIO");
                                        TextBox PercentAmount = (TextBox)GridViewInterOrganization.Rows[rowIndexIO].Cells[2].FindControl("PercentageIOAmount");
                                        PO[p].MUNonMU = DropdownMuNonMu.Text.Trim();

                                        if (PO[p].MUNonMU == "N")
                                        {
                                            PO[p].Institution = InstId.Value.Trim();
                                            PO[p].InstitutionName = InstNme.Text.Trim();
                                            PO[p].percentageType = "E";
                                            if (Percent.Text != "")
                                            {
                                                PO[p].percentageIO = Convert.ToInt32(Percent.Text);
                                            }
                                        }
                                        else if (PO[p].MUNonMU == "M")
                                        {
                                            PO[p].Institution = InstId.Value.Trim();
                                            PO[p].InstitutionName = InstNme.Text.Trim();
                                            PO[p].percentageType = "I";
                                            if (Percent.Text != "")
                                            {
                                                PO[p].percentageIO = Convert.ToInt32(Percent.Text);
                                            }
                                        }
                                        if (Percent.Text.Trim() != "" &&Percent.Text.Trim() !="0" )
                                        {
                                            PO[p].percentageIO = Convert.ToInt32(Percent.Text);
                                            temp = temp + PO[p].percentageIO;
                                            Session["temp"] = temp;
                                            PO[p].percentageIOAmount = Convert.ToDouble(PercentAmount.Text);
                                        }


                                        //if (temp != 100)
                                        //{
                                        //    ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please enter Valid percentage for Inter Organization!')</script>");
                                        //    return;
                                        //}

                                        rowIndexIO++;
                                    }

                                    if (temp != 100)
                                    {
                                        //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please enter Valid percentage for Inter Organization!')</script>");
                                        string CloseWindow = "alert('Please enter Valid percentage for Inter Organization!')";
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);

                                        return;
                                    }

                                    if (temp == 0)
                                    {
                                        {
                                            //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please enter  percentage for Inter Organization!')</script>");
                                            string CloseWindow = "alert('Please enter  percentage for Inter Organization!')";
                                            ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);

                                            return;
                                        }
                                    }
                                }

                                int rowIndexII = 0;
                                temp1 = 0;
                                if (dtCurrentTableII.Rows.Count > 0)
                                {
                                    for (int q = 0; q < dtCurrentTableII.Rows.Count; q++)
                                    {
                                        PI[q] = new GrantData();
                                        DropDownList DropdownMuNonMuI = (DropDownList)GridViewInterInstitute.Rows[rowIndexII].Cells[3].FindControl("DropdownMuNonMuII");
                                        TextBox InstNmeI = (TextBox)GridViewInterInstitute.Rows[rowIndexII].Cells[2].FindControl("InstitutionNameII");
                                        HiddenField InstIdI = (HiddenField)GridViewInterInstitute.Rows[rowIndexII].Cells[2].FindControl("InstitutionII");
                                        TextBox deptNmeI = (TextBox)GridViewInterInstitute.Rows[rowIndexII].Cells[2].FindControl("DepartmentNameII");
                                        HiddenField deptIdI = (HiddenField)GridViewInterInstitute.Rows[rowIndexII].Cells[2].FindControl("DepartmentII");
                                        TextBox PercentI = (TextBox)GridViewInterInstitute.Rows[rowIndexII].Cells[2].FindControl("PercentageII");
                                        TextBox PercentIAmount = (TextBox)GridViewInterInstitute.Rows[rowIndexII].Cells[2].FindControl("PercentageIIAmount");
                                        PI[q].MUNonMU = DropdownMuNonMuI.Text.Trim();

                                        if (PI[q].MUNonMU == "N")
                                        {
                                            PI[q].Institution = InstIdI.Value.Trim();
                                            PI[q].InstitutionName = InstNmeI.Text.Trim();
                                            PI[q].Department = deptIdI.Value.Trim();
                                            PI[q].DepartmentName = deptNmeI.Text.Trim();
                                            PI[q].percentageType = "E";
                                            if (PercentI.Text != "")
                                            {
                                                PI[q].percentageII = Convert.ToInt32(PercentI.Text);
                                            }
                                        }
                                        else if (PI[q].MUNonMU == "M")
                                        {
                                            PI[q].Institution = InstIdI.Value.Trim();
                                            PI[q].InstitutionName = InstNmeI.Text.Trim();
                                            PI[q].Department = deptIdI.Value.Trim();
                                            PI[q].DepartmentName = deptNmeI.Text.Trim();
                                            PI[q].percentageType = "I";
                                            if (PercentI.Text != "")
                                            {
                                                PI[q].percentageII = Convert.ToInt32(PercentI.Text);
                                            }
                                        }
                                        if (PercentI.Text.Trim() != "" && PercentI.Text.Trim() != "0")
                                        {
                                            PI[q].percentageII = Convert.ToInt32(PercentI.Text);
                                            temp1 = temp1 + PI[q].percentageII;
                                            Session["temp1"] = temp1;
                                            PI[q].percentageIIAmount = Convert.ToDouble(PercentIAmount.Text);
                                        }
                                        //if (PercentI.Text.Trim() != "")
                                        //{
                                        //    if (PO[p].Institution == PI[q].Institution)
                                        //    {
                                        //        PI[q].percentageII = Convert.ToInt32(PercentI.Text);
                                        //        tempI = tempI + PI[q].percentageII;
                                        //        Session["Sum"] = tempI;
                                        //        Session["Inst"] = PI[q].InstitutionName;
                                        //        Session["Dept"] = PI[q].DepartmentName;
                                        //    }
                                        //}
                                        rowIndexII++;
                                    }

                                    if (temp1 != 100)
                                    {
                                        //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please enter Valid percentage for Inter Institute!')</script>");
                                        string CloseWindow = "alert('Please enter Valid percentage for Inter Institute!')";
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);

                                        return;
                                    }

                                    if (temp1 == 0)
                                    {
                                        {
                                            //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please enter  percentage for Inter Institute!')</script>");
                                            string CloseWindow = "alert('Please enter  percentage for Inter Institute!')";
                                            ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);
  
                                            return;
                                        }
                                    }
                                    //if (PO[p].percentageIO != 0)
                                    //{
                                    //    if (PO[p].percentageIO != Convert.ToInt32(Session["Sum"].ToString()))
                                    //    {
                                    //        ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please enter Valid percentage for Inter Institute  " + Session["Inst"].ToString() + "   and deparment  " + Session["Dept"].ToString() + " !')</script>");
                                    //        return;
                                    //    }
                                    //}
                                }
                                if (temp != 0 && temp1 != 0)
                                {
                                    if (Convert.ToInt32(Session["temp1"].ToString()) == Convert.ToInt32(Session["temp"].ToString()))
                                    {
                                        if (Convert.ToInt32(Session["temp1"].ToString()) == 100)
                                        {
                                            if (Convert.ToInt32(Session["temp"].ToString()) == 100)
                                            {
                                                result1 = B.UpdateStatusGrantEntryAcceptApprovalPercentage(j, JD, JD1, SD3, PO, PI);
                                            }
                                        }
                                    }
                                }
                            }

                        }
                        result = B.UpdateStatusGrantEntryAcceptApproval(j, JD, JD1, SD3);
                        if (result > 0)
                        {
                            int id = B.CheckEmailDetails(DropDownListGrUnit.SelectedValue.ToString() + TextBoxID.Text, "GSAN");
                            if (id == 0)
                            {
                                EmailDetails details = new EmailDetails();
                                details = SendMailApprove();
                                details.Id = TextBoxID.Text;
                                details.Type = DropDownListTypeGrant.SelectedItem.ToString();
                                details.ProjectUnit = DropDownListGrUnit.SelectedItem.ToString();
                                details.UnitId = DropDownListGrUnit.SelectedValue.ToString();
                                SendMailObject obj1 = new SendMailObject();
                                bool resultvalue = obj1.InsertIntoEmailQueue(details);
                                GrantData obj = new GrantData();
                                Business obj2 = new Business();
                                obj = B.CheckUniqueId(TextBoxID.Text, DropDownListGrUnit.SelectedValue, details);
                                if (obj.Module == details.EmailSubject + details.Module)
                                {
                                    int data = obj2.updateEmailtracker(TextBoxID.Text, DropDownListGrUnit.SelectedValue, details, obj);
                                }

                            }

                        }
                    }
                    //else if(DropDownListProjStatus.SelectedValue == "SAN")
                    //{
                    //    result = B.UpdateSanctinedGrantEntry(j,JD1);
                    //}


                    else if (DropDownListProjStatus.SelectedValue == "REJ")
                    {
                        if (TextBoxAdComments.Text == "" && TextBoxRemarks.Text == "")
                        {
                            //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please  enter agency comments and Remarks')</script>");
                            string CloseWindow = "alert('Please  enter agency comments and Remarks')";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);
                            return;
                        

                        }
                        else if (TextBoxAdComments.Text == "")
                        {
                            //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please  enter agency comments')</script>");
                            string CloseWindow = "alert('Please  enter agency comments')";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);
                            return;
                        }
                        else if (TextBoxRemarks.Text == "")
                        {
                            //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please  enter  Remarks')</script>");
                            string CloseWindow = "alert('Please  enter  Remarks')";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);
                            return;
                        }

                        result = B.UpdateStatusGrantEntryRejectApproval(j, JD);
                    }
                    else if (DropDownListProjStatus.SelectedValue == "APP" || DropDownListProjStatus.SelectedValue == "REW")
                    {

                        result = B.UpdateGrantEntry(j, JD);

                    }

                    else if (DropDownListProjStatus.SelectedValue == "CLO")
                    {
                        if (DropDownListSanType.SelectedValue == "KI")
                        {
                            if (TextKindclosedate.Text == "")
                            {
                                //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please  enter  project close date')</script>");
                                string CloseWindow = "alert('Please  enter  project close date')";
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);

                                return;

                            }
                            else
                            {
                                result = B.UpdateStatusGrantEntryCLO(j);
                            }
                        }
                        else
                        {
                            result = B.UpdateStatusGrantEntryCLO(j);
                        }

                    }
                    else
                    {

                        result = B.UpdateStatusGrantEntryRejectApproval(j, JD);

                    }
                }
                else
                {
                    // result = B.UpdateStatusGrantEntryAcceptApproval(j, JD, JD1, SD3);
                    // result1 = B.UpdateStatusGrantEntryRejectApproval(j, JD);
                    //if (journalbank.GID != null)
                    //{
                    //    result1 = B.InsertRecieptDetails(j, JD2, journalbank);
                    //}
                    //if (journalbank1.GID != null)
                    //{
                    //    result1 = B.InsertOverheadDetails(j, OHD, journalbank1);
                    //}
                    if (journalbank2.GID != null)
                    {
                        IncentiveData[] JD7 = null;
                        DataTable dtCurrentTable1 = (DataTable)ViewState["temp_dt"];
                        JD7 = new IncentiveData[dtCurrentTable1.Rows.Count];
                        if (dtCurrentTable1.Rows.Count > 0)
                        {
                            for (int i = 0; i < dtCurrentTable1.Rows.Count; i++)
                            {

                                JD7[i] = new IncentiveData();
                                JD7[i].index = Convert.ToInt16(dtCurrentTable1.Rows[i]["index"]);
                                JD7[i].PayedTo = dtCurrentTable1.Rows[i]["InvestigatorName"].ToString();
                                JD7[i].Amount = Convert.ToDouble(dtCurrentTable1.Rows[i]["Amount"]);
                                JD7[i].SanctionEntryNo = Convert.ToInt16(dtCurrentTable1.Rows[i]["SanctionEntryNo"]);
                                JD7[i].InstitutionId = dtCurrentTable1.Rows[i]["Institution"].ToString();
                            }


                            ViewState["temp_dt"] = dtCurrentTable1;
                        }

                        //result1 = B.InsertIncentiveDetails(j,JD4, JD7, journalbank2);
                    }
                }

                if (result == 1 || result >= 1)
                {


                    if (DropDownListProjStatus.SelectedValue == "APP" || DropDownListProjStatus.SelectedValue == "REW")
                    {
                        //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Grant data Updated Successfully  of ID: " + TextBoxID.Text + "')</script>");

                        log.Info("Grant Updated Successfully, of ID: " + TextBoxID.Text);
                        ButtonSearchProjectOnClick(sender, e);
                        btnSave.Enabled = false;
                        string CloseWindow = "alert('Grant data Updated Successfully  of ID: " + TextBoxID.Text + "')";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);


                    }
                    else if (DropDownListProjStatus.SelectedValue == "SUB")
                    {

                        //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Grant data Submitted Successfully  of ID: " + TextBoxID.Text + "')</script>");
                        log.Info("Grant Submitted Successfully, of ID: " + TextBoxID.Text);
                        ButtonSearchProjectOnClick(sender, e);
                        btnSave.Enabled = false;
                        string CloseWindow = "alert('Grant data Submitted Successfully  of ID: " + TextBoxID.Text + "')";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);
                    }
                    //    

                    //    // FilePfdGrantSave(sender, e);
                    if (DropDownListProjStatus.SelectedValue == "SAN")
                    {
                        if (Session["Role"].ToString() == "1")
                        {
                            //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Grant data Sanctioned Successfully  of ID: " + TextBoxID.Text + "')</script>");
                            log.Info("Grant Sanctioned Successfully, of ID: " + TextBoxID.Text);
                            string CloseWindow = "alert('Grant data Sanctioned Successfully  of ID: " + TextBoxID.Text + "')";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);
                        }
                        else
                        {
                            //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Grant data Updated Successfully  of ID: " + TextBoxID.Text + "')</script>");
                            log.Info("Grant Data  Updated Successfully, of ID: " + TextBoxID.Text);
                            string CloseWindow = "alert('Grant data Updated Successfully  of ID: " + TextBoxID.Text + "')";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);

                        }
                        btnSave.Enabled = false;
                    }

                    else if (DropDownListProjStatus.SelectedValue == "CLO")
                    {
                        //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Grant data Closed Successfully  of ID: " + TextBoxID.Text + "')</script>");
                        log.Info("Grant Closed Successfully, of ID: " + TextBoxID.Text);
                        btnSave.Enabled = false;
                        string CloseWindow = "alert('Grant data Closed Successfully  of ID: " + TextBoxID.Text + "')";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);
                        btnSave.Enabled = false;

                    }
                    else if (DropDownListProjStatus.SelectedValue == "REJ")
                    {
                        //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Grant data Rejected Successfully  of ID: " + TextBoxID.Text + "')</script>");
                        log.Info("Grant Rejected Successfully, of ID: " + TextBoxID.Text);
                        string CloseWindow = "alert('Grant data Rejected Successfully  of ID: " + TextBoxID.Text + "')";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);
                        btnSave.Enabled = false;

                    }

                    else
                    {
                        if (result1 == 1)
                        {
                            //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Grant Details updated successfully')</script>");
                            log.Error("Grant Details updated successfully,  of ID: " + TextBoxID.Text);
                            string CloseWindow = "alert('Grant Details updated successfully')";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);
                        }
                        else
                        {

                            //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Grant Error')</script>");
                            log.Error("Grant Updation Error!!!,  of ID: " + TextBoxID.Text);
                            string CloseWindow = "alert('Grant Error')";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);
                        }
                    }
                }
            }
        }

        catch (Exception ex)
        {
            log.Error("Inside Catch Block Of Grant" + ex.Message + " UserID : " + Session["UserId"].ToString());
            log.Error(ex.StackTrace);
            if (ex.Message.Contains("The string was not recognized as a valid DateTime"))
            {
                string CloseWindow = "alert('Date is not valid')";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);
                //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Date is not valid')</script>");
            }
            if (ex.Message.Contains("String was not recognized as a valid DateTime."))
            {
                //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Date is not valid')</script>");
                string CloseWindow = "alert('Date is not valid')";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);
            }

            else if (ex.Message.Contains("Input string was not in a correct format"))
            {
                //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Error')</script>");
                log.Error("Error, of ID: " + TextBoxID.Text + " , Type: " + DropDownListTypeGrant.SelectedValue);
                string CloseWindow = "alert('Error')";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);

            }
            else if (ex.Message.Contains("There is already an open DataReader"))
            {
                //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Grant data Creaton Failed)</script>");
                log.Info("Grant data Creation Saved..Upload failed, of ID: " + ex.Message + " " + TextBoxID.Text + " , Type: " + DropDownListTypeGrant.SelectedValue);
                string CloseWindow = "alert('Grant data Creaton Failed')";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);
            }
            else if (ex.Message.Contains("Mailbox unavailable. The server response was: #5.1.0 Address rejecte"))
            {
                //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Grant data Created / Sanctioned Successfully')</script>");
                log.Info("Grant created Successfully, of ID: " + TextBoxID.Text + " , Type: " + DropDownListTypeGrant.SelectedValue);
                string CloseWindow = "alert('Grant data Created / Sanctioned Successfully')";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);

            }
            else if (ex.Message.Contains("Unable to send to a recipient"))
            {
                //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Grant data Created / Sanctioned Successfully....Error in mail sending!!!!!!!!!!!!!!')</script>");
                log.Info("Grant created Successfully,Error in mail sending!!!!!!!!!!!!!, of ID: " + TextBoxID.Text + " , Type: " + DropDownListTypeGrant.SelectedValue);

                string CloseWindow = "alert('Grant data Created / Sanctioned Successfully....Error in mail sending!!!!!!!!!!!!!!')";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);
            }
            else if (ex.Message.Contains("Object reference not set to an instance of an obje"))
            {
                //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Grant data Creaton Failed....Please contact admin')</script>");
                log.Error("Grant data Creaton Failed.....Please contact admin, id: " + TextBoxID.Text + " , Type: " + DropDownListTypeGrant.SelectedValue);
                string CloseWindow = "alert('Grant data Creaton Failed....Please contact admin')";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);

            }
            else if (ex.Message.Contains("IX_Project"))
            {
                //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Project Creation Failed.This Project Already Present!')</script>");
                string CloseWindow = "alert('Project Creation Failed.This Project Already Present!')";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);
            }

            else if (ex.Message.Contains("Failure sending mail."))
            {
                //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Grant Data submitted successfully.Failure in sending mail.')</script>");
                string CloseWindow = "alert('Grant Data submitted successfully.Failure in sending mail')";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);
            }
            else
            if (ex.Message.Contains("Unable to cast object of type 'System.DBNull' to type 'System.String'."))
            {

                //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('UTN Id is not found')</script>");
                string CloseWindow = "alert('UTN Id is not found')";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);
            }

            else

                //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Grant data Creation failed')</script>");
            log.Error("Grant data Creaton Failed.... id: " + TextBoxID.Text + " , Type: " + DropDownListTypeGrant.SelectedValue);
            string CloseWindow1 = "alert('Grant data Creation failed')";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow1, true);
        }
    }


    protected void addclik(object sender, EventArgs e)
    {
        //panelcancelProject.Visible = false;
        PanelViewUplodedfiles.Visible = false;
        Panel8.Visible = false;
        LabelkindDetails.Visible = false;
        BtnAddMU.Enabled = true;
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
        PanelUploaddetails.Visible = true;
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

    private EmailDetails SendMailApprove()
    {
        log.Debug("Grant Approval: Inside Send Mail for Approval Project of type: " + DropDownListTypeGrant.SelectedValue + " ID: " + TextBoxID.Text + " Project Unit: " + DropDownListGrUnit.Text);
        EmailDetails details = new EmailDetails();
        details.EmailSubject = "Project Entry <  " + DropDownListTypeGrant.SelectedValue + " _ " + TextBoxID.Text + "  > Sanctioned ";
        details.Module = "GSAN";
        if (DropDownListSanType.SelectedValue == "CA")
        {
            ArrayList myArrayListInvestigator = new ArrayList();
            ArrayList myArrayListInvestigatorNAme = new ArrayList();
            ArrayList myArrayListReserachCoOrdinator = new ArrayList();
            ArrayList myArrayListFinanceTeam = new ArrayList();
            ArrayList myArrayListInvestigatorDetail = new ArrayList();

            DataSet ds = new DataSet();
            DataSet dsIN = new DataSet();
            Business bus = new Business();
            GrantData b = new GrantData();
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
                //if (details.CCEmail != null)
                //{
                //    details.CCEmail = details.CCEmail + ',' + myArrayListInvestigator[i].ToString();
                //}
                //else
                //{
                //    if (i == 0)
                //    {
                //        details.CCEmail = myArrayListInvestigator[i].ToString();
                //    }
                //    else
                //    {
                //        details.CCEmail = details.CCEmail + ',' + myArrayListInvestigator[i].ToString();
                //    }
                //    //details.CCEmail = details.CCEmail + ',' + email;
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


                //Msg.CC.Add(myArrayListInvestigator[i].ToString());
                log.Info(" Email will be sent to Investigators '" + i + "' : '" + myArrayListInvestigator[i] + "' ");
            }
            for (int i = 0; i < myArrayListFinanceTeam.Count; i++)
            {
                // Msg.To.Add(myArrayListFinanceTeam[i].ToString());
                //string email = myArrayListFinanceTeam[i].ToString();
                if (details.ToEmail != null)
                {
                    if (myArrayListFinanceTeam[i].ToString() != "")
                    {
                        details.ToEmail = details.ToEmail + ',' + myArrayListFinanceTeam[i].ToString();
                    }
                }
                else
                {
                    if (i == 0)
                    {
                        if (myArrayListFinanceTeam[i].ToString() != "")
                        {
                            details.ToEmail = myArrayListFinanceTeam[i].ToString();
                        }
                    }
                    else
                    {
                        if (myArrayListFinanceTeam[i].ToString() != "")
                        {
                            if (details.ToEmail == null)
                            {
                                details.ToEmail = myArrayListFinanceTeam[i].ToString();
                            }
                            else
                            {
                                details.ToEmail = details.ToEmail + ',' + myArrayListFinanceTeam[i].ToString();
                            }
                        }
                    }
                }
                //details.ToEmail = email;
                log.Info(" Email will be sent to Research Finance team '" + i + "' : '" + myArrayListFinanceTeam[i] + "' ");
            }


        }
        return details;
    }

    private EmailDetails SendMail()
    {
        EmailDetails details = new EmailDetails();
        if (DropDownListProjStatus.SelectedValue == "APP")
        {
            log.Debug(" GrantEntry:Inside Send Mail for Applied Project of type: " + DropDownListTypeGrant.SelectedValue + " ID: " + TextBoxID.Text + " Project Unit: " + DropDownListGrUnit.Text);
            details.Module = "GAPP";
            details.EmailSubject = "Project Entry < " + DropDownListTypeGrant.SelectedValue + " _ " + TextBoxID.Text + "  > Applied ";
            ArrayList myArrayListInvestigator = new ArrayList();
            ArrayList myArrayListInvestigator1 = new ArrayList();
            ArrayList myArrayListInvestigatorName = new ArrayList();
            ArrayList myArrayListReserachCoOrdinator = new ArrayList();
            ArrayList myArrayListInvestigatorDetail = new ArrayList();
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

            details.EmailSubject = "Project Entry < " + DropDownListTypeGrant.SelectedValue + " _ " + TextBoxID.Text + "  > Applied ";
            details.MsgBody = "<span style=\"font-size: 9pt; color: #3300cc; font-family: Verdana\"><h4>Dear Sir/Madam,</h4> <br>" +
             "<b> The following Project  has been Applied in Project Repository : <br> " +
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
            details.Module = "GAPP";


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
                string email = myArrayListInvestigator[i].ToString();
                if (details.ToEmail != null)
                {
                    if (myArrayListInvestigator[i].ToString() != "")
                    {
                        details.ToEmail = details.ToEmail + ',' + myArrayListInvestigator[i].ToString();
                    }

                }
                else
                {
                    if (i == 0)
                    {
                        if (myArrayListInvestigator[i].ToString() != "")
                        {
                            details.ToEmail = myArrayListInvestigator[i].ToString();
                        }
                    }
                    else
                    {
                        if (myArrayListInvestigator[i].ToString() != "")
                        {
                            if (details.ToEmail == null)
                            {
                                details.ToEmail = myArrayListInvestigator[i].ToString();
                            }
                            else
                            {
                                details.ToEmail = details.ToEmail + ',' + myArrayListInvestigator[i].ToString();
                            }
                        }
                    }
                    //details.CCEmail = details.CCEmail + ',' + email;
                }

                // details.ToEmail = email;
                log.Info(" Email will be sent to Investigators '" + i + "' : '" + myArrayListInvestigator[i] + "' ");
            }

            for (int i = 0; i < myArrayListReserachCoOrdinator.Count; i++)
            {
                string email = myArrayListReserachCoOrdinator[i].ToString();
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
                // details.ToEmail = email;
                log.Info(" Email will be sent to Research CoOrdinator '" + i + "' : '" + myArrayListReserachCoOrdinator[i] + "' ");
            }


        }

        if (DropDownListProjStatus.SelectedValue == "SAN")
        {
            log.Debug(" Inside Send Mail for Submitted Project of type: " + DropDownListTypeGrant.SelectedValue + " ID: " + TextBoxID.Text);
            ArrayList myArrayListInvestigator = new ArrayList();
            ArrayList myArrayListInvestigatorNAme = new ArrayList();
            ArrayList myArrayListReserachCoOrdinator = new ArrayList();
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

            details.EmailSubject = "Project Entry <  " + DropDownListTypeGrant.SelectedValue + " _ " + TextBoxID.Text + "  > Submitted ";
            details.MsgBody = "<span style=\"font-size: 10pt; color: #3300cc; font-family: Verdana\"><h4>Dear Sir/Madam,</h4> <br>" +
              "<b> The following Project  has been Submitted in Project Repository : <br> " +
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
            details.Module = "GSAN";
            for (int i = 0; i < myArrayListInvestigator.Count; i++)
            {
                // Msg.CC.Add(myArrayListInvestigator[i].ToString());
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
                }
                log.Info(" Email will be sent to Investigators '" + i + "' : '" + myArrayListInvestigator[i] + "' ");
            }

            for (int i = 0; i < myArrayListReserachCoOrdinator.Count; i++)
            {
                //  Msg.To.Add(myArrayListReserachCoOrdinator[i].ToString());
                string email = myArrayListReserachCoOrdinator[i].ToString();
                if (details.ToEmail != null)
                {
                    details.ToEmail = details.ToEmail + ',' + myArrayListReserachCoOrdinator[i].ToString();
                }
                else
                {
                    if (i == 0)
                    {
                        details.ToEmail = myArrayListReserachCoOrdinator[i].ToString();
                    }
                    else
                    {
                        details.ToEmail = details.ToEmail + ',' + myArrayListReserachCoOrdinator[i].ToString();
                    }
                }
                // details.ToEmail = email;
                log.Info(" Email will be sent to Research CoOrdinator '" + i + "' : '" + myArrayListReserachCoOrdinator[i] + "' ");
            }

        }
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


        

        //fnRecordExist(sender, e);
        //dataBind();

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



    protected void AddIncentive(object sender, EventArgs e)
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
        UpdatePanel9.Update();
        UpdatePanel16.Update();
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

            SqlDataSourceAddAmt.SelectCommand = " Select a.ProjectUnit,a.ID,a.InvestigatorType, a.EntryNo as Row,InvestigatorName,a.Institution as Institution,a.Department as Department, Amount from Projectnvestigator a left outer join ProjectIncentivePayDetails b on a.ProjectUnit=b.ProjectUnit and a.ID=b.ID and a.EntryNo=b.EntryNo and  b.[LineNo]=@value and a.ProjectUnit=@unit and a.ID=@id where a.ProjectUnit=@unit and a.ID=@id";
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

            //ModalPopupExtender ModalPopupExtenderMisc = (ModalPopupExtender)gvIncentiveDetails.Rows[rowindex].FindControl("ModalPopupExtenderAmount");
            //ModalPopupExtenderMisc.Show();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "CallMyFunction", "callthis6()", true);
            return;


        }

        else
        {
            setModalWindowAmount(sender, e);
            //ModalPopupExtender ModalPopupExtenderMisc = (ModalPopupExtender)gvIncentiveDetails.Rows[0].FindControl("ModalPopupExtenderAmount");
            //ModalPopupExtenderMisc.Show();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "CallMyFunction", "callthis6()", true);
            return;

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
        UpdatePanel9.Update();
        UpdatePanel16.Update();
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
                    //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert(' Amount must be in numbers!')</script>");
                    string CloseWindow = "alert('Amount must be in numbers!')";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);
                    setModalWindowAmount(sender, e);

                    string rowVal2 = Request.Form["rowIndx"];
                    int rowIndex = Convert.ToInt32(rowVal2);
                    ModalPopupExtender ModalPopupExtenderMisc = (ModalPopupExtender)gvIncentiveDetails.Rows[rowIndex].FindControl("ModalPopupExtenderAmount");
                    ModalPopupExtenderMisc.Show();
                    return;
                }
                if (cost.Text == "0" || cost.Text == "0.0")
                {
                    //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert(' Amount must be in numbers!')</script>");
                    string CloseWindow = "alert('Amount must be in numbers!')";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);
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
                dr["Amount"] = Math.Round(amount, 2);
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
        ScriptManager.RegisterStartupScript(this, this.GetType(), "CallMyFunction", "ToggleDisplay6()", true);

    }



    //Saving Sanction details
    protected void BtnSanctionSave_Click(object sender, EventArgs e)
    {
        UpdatePanel7.Update();
        UpdatePanel14.Update();
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
                //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please enter Account Head!')</script>");
                //return;
                string CloseWindow = "alert('Please enter Account Head!')";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);
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
            j.Status = DropDownListProjStatus.SelectedValue;
            j.AddtionalComments = TextBoxAdComments.Text.Trim();

            int rowscount = GridViewSanction.Rows.Count;
            for (int i = 0; i < rowscount; i++)
            {
                TextBox santotalAmount1 = (TextBox)GridViewSanction.Rows[i].Cells[3].FindControl("txtsantotalAmount");
                TextBox sancapitalAmount1 = (TextBox)GridViewSanction.Rows[i].Cells[4].FindControl("txtsancapitalAmount");
                TextBox SanOpeAmt1 = (TextBox)GridViewSanction.Rows[i].Cells[5].FindControl("txtSanOpeAmt");

                if (santotalAmount1.Text.Trim() != "")
                {
                    j.SanctionTotalAmount = j.SanctionTotalAmount + Math.Round(Convert.ToDouble(santotalAmount1.Text.Trim()), 2);
                }
                if (sancapitalAmount1.Text.Trim() != "")
                {

                    j.SanctionCapitalAmount = j.SanctionCapitalAmount + Math.Round(Convert.ToDouble(sancapitalAmount1.Text.Trim()), 2);
                }
                else
                {
                    //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please enter capital amount!')</script>");
                    //return;
                    string CloseWindow = "alert('Please enter capital amount!')";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);
                    return;
                  
                }
                if (SanOpeAmt1.Text.Trim() != "")
                {
                    j.SanctionOperatingAmount = j.SanctionOperatingAmount + Math.Round(Convert.ToDouble(SanOpeAmt1.Text.Trim()), 2);
                }
                else
                {
                    //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please enter operating amount!')</script>");
                    //return;
                    string CloseWindow = "alert('Please enter operating amount')";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);
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
                        //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Sanction date is not valid!')</script>");
                        //return;
                        string CloseWindow = "alert('Sanction date is not valid!')";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);
                        return;
                    }
                    if (sanctionNo.Text == "")
                    {
                        //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please enter sanction number')</script>");
                        //return;
                        string CloseWindow = "alert('Please enter sanction number')";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);
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
                            SD3[i].SanctionTotalAmount = Math.Round(Convert.ToDouble(santotalAmount.Text.Trim()), 2);
                            SD3[i].SanctionCapitalAmount = Math.Round(Convert.ToDouble(sancapitalAmount.Text.Trim()), 2);
                            SD3[i].SanctionOperatingAmount = Math.Round(Convert.ToDouble(SanOpeAmt.Text.Trim()), 2);
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
                //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Grant Sanction Details updated Successfully  of ID: " + TextBoxID.Text + "')</script>");
                string CloseWindow = "alert('Grant Sanction Details updated Successfully  of ID: " + TextBoxID.Text + "')";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);
                log.Info("Grant Sanction Details updated Successfully, of ID: " + TextBoxID.Text);
                int rowscount1 = GridViewSanction.Rows.Count;
                for (int i = 0; i < rowscount; i++)
                {
                    TextBox santotalAmount1 = (TextBox)GridViewSanction.Rows[i].Cells[3].FindControl("txtsantotalAmount");
                    TextBox sancapitalAmount1 = (TextBox)GridViewSanction.Rows[i].Cells[4].FindControl("txtsancapitalAmount");
                    TextBox SanOpeAmt1 = (TextBox)GridViewSanction.Rows[i].Cells[5].FindControl("txtSanOpeAmt");
                    TextBoxSanctionedamountTotal.Text = j.SanctionTotalAmount.ToString();
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
                //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Date is not valid')</script>");
                string CloseWindow = "alert('Date is not valid')";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);


            }
            if (ex.Message.Contains("String was not recognized as a valid DateTime."))
            {
                //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Date is not valid')</script>");
                string CloseWindow = "alert('Date is not valid')";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);

            }

            else if (ex.Message.Contains("Input string was not in a correct format"))
            {
                //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Input string was not in a correct format')</script>");
                log.Error("Error, of ID: " + TextBoxID.Text + " , Type: " + DropDownListTypeGrant.SelectedValue);

                string CloseWindow = "alert('Input string was not in a correct format')";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);

            }
            else if (ex.Message.Contains("There is already an open DataReader"))
            {
                //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Grant data Creaton Failed)</script>");
                log.Info("Grant data Creation Saved..Upload failed, of ID: " + ex.Message + " " + TextBoxID.Text + " , Type: " + DropDownListTypeGrant.SelectedValue);
                string CloseWindow = "alert('Grant data Creaton Failed')";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);

            }
            else if (ex.Message.Contains("Mailbox unavailable. The server response was: #5.1.0 Address rejecte"))
            {
                //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Grant Sanction details Updated Successfully')</script>");
                log.Info("Grant created Successfully, of ID: " + TextBoxID.Text + " , Type: " + DropDownListTypeGrant.SelectedValue);
                string CloseWindow = "alert('Grant Sanction details Updated Successfully')";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);

            }
            else if (ex.Message.Contains("Unable to send to a recipient"))
            {
                //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Grant Sanction details Updated Successfully....Error in mail sending!!!!!!!!!!!!!!')</script>");
                log.Info("Grant created Successfully,Error in mail sending!!!!!!!!!!!!!, of ID: " + TextBoxID.Text + " , Type: " + DropDownListTypeGrant.SelectedValue);
                string CloseWindow = "alert('Grant Sanction details Updated Successfully....Error in mail sending!!!!!!!!!!!!!!')";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);

            }
            else if (ex.Message.Contains("Object reference not set to an instance of an obje"))
            {
                //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Grant Sanction details Updation Failed....Please contact admin')</script>");
                log.Error("Grant data Creaton Failed.....Please contact admin, id: " + TextBoxID.Text + " , Type: " + DropDownListTypeGrant.SelectedValue);
                string CloseWindow = "alert('Grant Sanction details Updation Failed....Please contact admin')";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);

            }
            else
                //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Grant Sanction details Updation failed')</script>");
            log.Error("Grant data Creaton Failed.... id: " + TextBoxID.Text + " , Type: " + DropDownListTypeGrant.SelectedValue);
            string CloseWindow1 = "alert('Grant Sanction details Updation failed')";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow1, true);

        }
    }



    protected void BtnSaveFundDetails(object sender, EventArgs e)
    {

        if (!Page.IsValid)
        {
            return;
        }

        UpdatePanel8.Update();
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
                            //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Recieved Amount must be in numbers!')</script>");
                            //return;
                            string CloseWindow = "alert('Recieved Amount must be in numbers!')";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);
                            return;
                        }
                        if (ReceviedINR.Text != "" && !regex.IsMatch(ReceviedINR.Text))
                        {
                            //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('INR amount must be in numbers!')</script>");
                            //return;
                            string CloseWindow = "alert('INR amount must be in numbers!')";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);
                            return;
                        }
                        journalbank.GID = TextBoxID.Text;
                        journalbank.GrantUnit = DropDownListGrUnit.SelectedValue;

                        JD2[i].FRSanctionEntryNo = Convert.ToInt16(SanctionEntryNumber.SelectedValue);
                        JD2[i].CurrencyCode = CurrencyCode.SelectedValue;
                        JD2[i].ModeOfReceive = ModeOfReceive.SelectedValue;
                        if (ReceviedDate.Text.Trim() == "")
                        {
                            string CloseWindow = "alert('Please enter the Reccieved Date')";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);
                            return;
                        }
                        JD2[i].ReceviedDate = Convert.ToDateTime(ReceviedDate.Text.ToString());
                        double amount = Convert.ToDouble(ReceviedAmount.Text);
                        JD2[i].ReceviedAmmount = (Math.Round(amount, 2));
                        if (TDS.Text.Trim() != "")
                        {
                            JD2[i].TDS = Convert.ToDouble(TDS.Text.Trim());
                        }
                        JD2[i].ReferenceNumber = ReferenceNo.Text.Trim();
                        JD2[i].CreditedBankName = CreditedBankId.Text.Trim();
                        if (ReceviedINR.Text.Trim() != "")
                        {
                            JD2[i].ReceviedINR = (Math.Round(Convert.ToDouble(ReceviedINR.Text.Trim()), 2));
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
                result = B.InsertRecieptDetails(journalbank, JD2);
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

            if (result > 0)
            {
                //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Grant Fund Received details saved successfully  of ID: " + TextBoxID.Text + "')</script>");
                log.Info("Grant Fund Received details saved successfully, of ID: " + TextBoxID.Text);
                string CloseWindow = "alert('Grant Fund Received details saved successfully  of ID: " + TextBoxID.Text + "')";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);

            }
        }

        catch (Exception ex)
        {

            log.Error("Inside Catch Block Of Grant" + ex.Message + " UserID : " + Session["UserId"].ToString());
            log.Error(ex.StackTrace);
            if (ex.Message.Contains("Failure sending mail."))
            {
                //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Grant Fund Received details saved successfully  of ID: " + TextBoxID.Text + "')</script>");
                string CloseWindow = "alert('Failure sending mail')";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);

            }
            if (ex.Message.Contains("The string was not recognized as a valid DateTime"))
            {
                //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Date is not valid')</script>");
                string CloseWindow = "alert('Date is not valid')";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);
            }
            if (ex.Message.Contains("String was not recognized as a valid DateTime."))
            {
                //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Date is not valid')</script>");
                string CloseWindow = "alert('Date is not valid')";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);
            }

            else if (ex.Message.Contains("Input string was not in a correct format"))
            {
                //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Error')</script>");
                log.Error("Error, of ID: " + TextBoxID.Text + " , Type: " + DropDownListTypeGrant.SelectedValue);
                string CloseWindow = "alert('Error')";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);


            }
            else if (ex.Message.Contains("There is already an open DataReader"))
            {
                //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Grant data updation Failed)</script>");
                log.Info("Grant data Creation Saved..Upload failed, of ID: " + ex.Message + " " + TextBoxID.Text + " , Type: " + DropDownListTypeGrant.SelectedValue);
                string CloseWindow = "alert('Grant data updation Failed')";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);
            }
            else if (ex.Message.Contains("Mailbox unavailable. The server response was: #5.1.0 Address rejecte"))
            {
                //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Fund details updated Successfully')</script>");
                log.Info("Grant created Successfully, of ID: " + TextBoxID.Text + " , Type: " + DropDownListTypeGrant.SelectedValue);
                string CloseWindow = "alert('Fund details updated Successfully')";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);

            }
            else if (ex.Message.Contains("Unable to send to a recipient"))
            {
                //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Fund details updated Successfully....Error in mail sending!!!!!!!!!!!!!!')</script>");
                log.Info("Fund details updated Successfully,Error in mail sending!!!!!!!!!!!!!, of ID: " + TextBoxID.Text + " , Type: " + DropDownListTypeGrant.SelectedValue);
                string CloseWindow = "alert('Fund details updated Successfully....Error in mail sending!!!!!!!!!!!!!!')";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);

            }
            else if (ex.Message.Contains("Object reference not set to an instance of an obje"))
            {
                //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Grant data Creaton Failed....Please contact admin')</script>");
                log.Error("Fund details updated Successfully.....Please contact admin, id: " + TextBoxID.Text + " , Type: " + DropDownListTypeGrant.SelectedValue);
                string CloseWindow = "alert('Grant data Creaton Failed....Please contact admin')";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);
            }

            else

                //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Grant data Creation failed!!!!!!!!!!!!')</script>");
                log.Error("Grant data Creaton Failed.... id: " + TextBoxID.Text + " , Type: " + DropDownListTypeGrant.SelectedValue);
            string CloseWindow1 = "alert('Grant data Creation failed!!!!!!!!!!!!')";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow1, true);
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
            UpdatePanel9.Update();
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
                //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Grant Incentive details saved successfully  of ID: " + TextBoxID.Text + "')</script>");
                log.Info("Grant Incentive details saved successfully, of ID: " + TextBoxID.Text);

                string CloseWindow = "alert('Grant Incentive details saved successfully  of ID: " + TextBoxID.Text + "')";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);
                return;

            }
        }

        catch (Exception ex)
        {
            log.Error("Inside Catch Block Of Grant" + ex.Message + " UserID : " + Session["UserId"].ToString());
            log.Error(ex.StackTrace);
            if (ex.Message.Contains("The string was not recognized as a valid DateTime"))
            {
                //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Date is not valid')</script>");
                string CloseWindow = "alert('Date is not valid')";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);

            }
            if (ex.Message.Contains("String was not recognized as a valid DateTime."))
            {
                //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Date is not valid')</script>");
                string CloseWindow = "alert('Date is not valid')";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);

            }

            else if (ex.Message.Contains("Input string was not in a correct format"))
            {
                //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Input string was not in a correct format')</script>");
                log.Error("Error, of ID: " + TextBoxID.Text + " , Type: " + DropDownListTypeGrant.SelectedValue);
                string CloseWindow = "alert('Input string was not in a correct format')";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);

            }
            else if (ex.Message.Contains("There is already an open DataReader"))
            {
                //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Grant data Creaton Failed)</script>");
                log.Info("Grant data Creation Saved..Upload failed, of ID: " + ex.Message + " " + TextBoxID.Text + " , Type: " + DropDownListTypeGrant.SelectedValue);
                string CloseWindow = "alert('Grant data Creaton Failed')";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);

            }
            else if (ex.Message.Contains("Mailbox unavailable. The server response was: #5.1.0 Address rejecte"))
            {
                //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Grant data Created / Sanctioned Successfully')</script>");
                log.Info("Grant created Successfully, of ID: " + TextBoxID.Text + " , Type: " + DropDownListTypeGrant.SelectedValue);
                string CloseWindow = "alert('Grant data Created / Sanctioned Successfully')";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);


            }
            else if (ex.Message.Contains("Unable to send to a recipient"))
            {
                //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Grant data Created / Sanctioned Successfully....Error in mail sending!!!!!!!!!!!!!!')</script>");
                log.Info("Grant created Successfully,Error in mail sending!!!!!!!!!!!!!, of ID: " + TextBoxID.Text + " , Type: " + DropDownListTypeGrant.SelectedValue);

                string CloseWindow = "alert('Grant data Created / Sanctioned Successfully....Error in mail sending!!!!!!!!!!!!!!')";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);

            }
            else if (ex.Message.Contains("Object reference not set to an instance of an obje"))
            {
                //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Grant data Creaton Failed....Please contact admin')</script>");
                log.Error("Grant data Creaton Failed.....Please contact admin, id: " + TextBoxID.Text + " , Type: " + DropDownListTypeGrant.SelectedValue);
                string CloseWindow = "alert('Grant data Creaton Failed....Please contact admin')";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);

            }

            else

                //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Grant data Creation failed!!!!!!!!!!!!')</script>");
            log.Error("Grant data Creaton Failed.... id: " + TextBoxID.Text + " , Type: " + DropDownListTypeGrant.SelectedValue);
            string CloseWindow1 = "alert('Grant data Creation failed!!!!!!!!!!!!')";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow1, true);

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
            UpdatePanel13.Update();
            GrantData j = new GrantData();
            j.FinanceProjectStatus = DropDownList3.SelectedValue;
            if (TextBox3.Text == "01/01/0001 00:00:00")
            {
                string CloseWindow = "alert('Please Enter the Date of Completion')";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);
                return;
            }
            else
            j.DateOfCompletion = Convert.ToDateTime(TextBox3.Text);

            
            j.Remarks = TextBox4.Text;
            j.GID = TextBoxID.Text;
            j.GrantUnit = DropDownListGrUnit.SelectedValue;

            int result = B.UpdateFinanceStatus(j);


            if (result == 1)
            {
                if (DropDownList3.SelectedValue == "OPE")
                {
                    //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Finance Details Updated Sucessfully: " + TextBoxID.Text + "')</script>");
                    log.Info("Finance Details Updated Sucessfully, of ID: " + TextBoxID.Text);
                    string CloseWindow = "alert('Finance Details Updated Sucessfully: " + TextBoxID.Text + "')";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);
                    return;

                }
                else
                {
                    //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Finance Details Closed Sucessfully: " + TextBoxID.Text + "')</script>");
                    log.Info("Finance Details Closed Sucessfully, of ID: " + TextBoxID.Text);
                    string CloseWindow = "alert('Finance Details Closed Sucessfully: " + TextBoxID.Text + "')";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);

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
                //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Date is not valid!!!!!!!!!!!!')</script>");
                string CloseWindow = "alert('Date is not valid!!!!!!!!!!!!')";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);
            }
            if (ex.Message.Contains("String was not recognized as a valid DateTime."))
            {
                //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Date is not valid!!!!!!!!!!!!')</script>");
                string CloseWindow = "alert(Date is not valid!!!!!!!!!!!!')";
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow, true);
            }

            else if (ex.Message.Contains("Input string was not in a correct format"))
            {
                //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Error!!!')</script>");
                log.Error("Error, of ID: " + TextBoxID.Text + " , Type: " + DropDownListTypeGrant.SelectedValue);
                string CloseWindow = "alert('Error!!!!')";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);
            }
            else if (ex.Message.Contains("There is already an open DataReader"))
            {
                //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Grant data Creaton Failed)</script>");
                log.Info("Grant data Creation Saved..Upload failed, of ID: " + ex.Message + " " + TextBoxID.Text + " , Type: " + DropDownListTypeGrant.SelectedValue);
                string CloseWindow = "alert('Grant data Creaton Failed')";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);
            }
            else if (ex.Message.Contains("Mailbox unavailable. The server response was: #5.1.0 Address rejecte"))
            {
                //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Grant data Created / Sanctioned Successfully')</script>");
                log.Info("Grant created Successfully, of ID: " + TextBoxID.Text + " , Type: " + DropDownListTypeGrant.SelectedValue);
                string CloseWindow = "alert('Grant data Created / Sanctioned Successfully')";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);

            }
            else if (ex.Message.Contains("Unable to send to a recipient"))
            {
                //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Grant data Created / Sanctioned Successfully....Error in mail sending!!!!!!!!!!!!!!')</script>");
                log.Info("Grant created Successfully,Error in mail sending!!!!!!!!!!!!!, of ID: " + TextBoxID.Text + " , Type: " + DropDownListTypeGrant.SelectedValue);
                string CloseWindow = "alert('Grant data Created / Sanctioned Successfully....Error in mail sending!!!!!!!!!!!!!!')";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);

            }
            else if (ex.Message.Contains("Object reference not set to an instance of an obje"))
            {
                //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Grant data Creaton Failed....Please contact admin')</script>");
                log.Error("Grant data Creaton Failed.....Please contact admin, id: " + TextBoxID.Text + " , Type: " + DropDownListTypeGrant.SelectedValue);
                string CloseWindow = "alert('Grant data Creaton Failed....Please contact admin')";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);

            }

            else

                //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Grant data Creation failed!!!!!!!!!!!!')</script>");
            log.Error("Grant data Creaton Failed.... id: " + TextBoxID.Text + " , Type: " + DropDownListTypeGrant.SelectedValue);
            string CloseWindow1 = "alert('Grant data Creation failed!!!!!!!!!!!!')";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow1, true);
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
                            OHD[i].OverheadTAmount = Math.Round(Convert.ToDouble(OHReceviedAmount.Text.Trim()), 2);
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
                //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Grant Overhead details saved successfully  of ID: " + TextBoxID.Text + "')</script>");
                log.Info("Grant Overhead details saved successfully, of ID: " + TextBoxID.Text);
                string CloseWindow = "alert('Grant Overhead details saved successfully  of ID: " + TextBoxID.Text + "')";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);
                return;


            }
        }

        catch (Exception ex)
        {
            log.Error("Inside Catch Block Of Grant" + ex.Message + " UserID : " + Session["UserId"].ToString());

            log.Error(ex.StackTrace);


            if (ex.Message.Contains("The string was not recognized as a valid DateTime"))
            {
                //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Date is not valid!!!!!!!!!!!!')</script>");
                string CloseWindow = "alert('Date is not valid!!!!!!!!!!!!')";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);

            }
            if (ex.Message.Contains("String was not recognized as a valid DateTime."))
            {
                //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Date is not valid!!!!!!!!!!!!')</script>");
                string CloseWindow = "alert('Date is not valid!!!!!!!!!!!!')";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);

            }

            else if (ex.Message.Contains("Input string was not in a correct format"))
            {
                //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Amount must be in numbers')</script>");
                log.Error("Error, of ID: " + TextBoxID.Text + " , Type: " + DropDownListTypeGrant.SelectedValue);
                string CloseWindow = "alert('Amount must be in numbers')";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);

            }
            else if (ex.Message.Contains("There is already an open DataReader"))
            {
                //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Grant data Creaton Failed)</script>");
                log.Info("Grant data Creation Saved..Upload failed, of ID: " + ex.Message + " " + TextBoxID.Text + " , Type: " + DropDownListTypeGrant.SelectedValue);
                string CloseWindow = "alert('Grant data Creaton Failed')";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);

            }
            else if (ex.Message.Contains("Mailbox unavailable. The server response was: #5.1.0 Address rejecte"))
            {
                //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Grant data Created / Sanctioned Successfully')</script>");
                log.Info("Grant created Successfully, of ID: " + TextBoxID.Text + " , Type: " + DropDownListTypeGrant.SelectedValue);
                string CloseWindow = "alert('Grant data Created / Sanctioned Successfully')";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);


            }
            else if (ex.Message.Contains("Unable to send to a recipient"))
            {
                //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Grant data Created / Sanctioned Successfully....Error in mail sending!!!!!!!!!!!!!!')</script>");
                log.Info("Grant created Successfully,Error in mail sending!!!!!!!!!!!!!, of ID: " + TextBoxID.Text + " , Type: " + DropDownListTypeGrant.SelectedValue);
                string CloseWindow = "alert('Grant data Created / Sanctioned Successfully....Error in mail sending!!!!!!!!!!!!!!')";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);


            }
            else if (ex.Message.Contains("Object reference not set to an instance of an obje"))
            {
                //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Grant data Creaton Failed....Please contact admin')</script>");
                log.Error("Grant data Creaton Failed.....Please contact admin, id: " + TextBoxID.Text + " , Type: " + DropDownListTypeGrant.SelectedValue);
                string CloseWindow = "alert('Grant data Creaton Failed....Please contact admin')";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);


            }

            else

                //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Grant data Creation failed!!!!!!!!!!!!')</script>");
            log.Error("Grant data Creaton Failed.... id: " + TextBoxID.Text + " , Type: " + DropDownListTypeGrant.SelectedValue);
            string CloseWindow1 = "alert('Grant data Creation failed!!!!!!!!!!!!')";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow1, true);

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
        UpdatePanel7.Update();
        UpdatePanel14.Update();
        Button imgButton = (Button)sender;
        GridViewRow parentRow = (GridViewRow)imgButton.NamingContainer;
        int rowindex = parentRow.RowIndex;

        Label11.Text = rowindex.ToString();

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

            SqlDataSource5.SelectCommand = "select ROW_NUMBER() OVER (ORDER BY a.[ID]) AS Row, a.ID,Name ,b.SanctionEntryNo,b.OperatingItemId,b.Amount as Amount,'' as rowIndexParent,'' as rowIndexChild from OperatingItem_M a left outer join SanctionOPAmountDetails b  on a.ID=b.OperatingItemId and b.SanctionEntryNo=@value and b.ProjectUnit=@unit and b.ID=@id";
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
            ScriptManager.RegisterStartupScript(this, this.GetType(), "CallMyFunction", "callthis4()", true);
            return;
            //ModalPopupExtender ModalPopupExtenderMisc = (ModalPopupExtender)GridViewSanction.Rows[rowindex].FindControl("ModalPopupExtenderOPAmount");
            //ModalPopupExtenderMisc.Show();
        }

        else
        {
            setModalWindowOPAmount(sender, e);

            //ModalPopupExtender ModalPopupExtenderMisc = (ModalPopupExtender)GridViewSanction.Rows[rowindex].FindControl("ModalPopupExtenderOPAmount");
            //ModalPopupExtenderMisc.Show();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "CallMyFunction", "callthis4()", true);
            return;
        }
      
       
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
        UpdatePanel7.Update();
        UpdatePanel14.Update();
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
                    //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert(' Amount must be in numbers!')</script>");
                    string CloseWindow = "alert('Amount must be in numbers!')";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);

                    setModalWindowAmount(sender, e);

                    string rowVal2 = Request.Form["rowIndx"];
                    int rowIndex = Convert.ToInt32(rowVal2);
                    //ModalPopupExtender ModalPopupExtenderMisc = (ModalPopupExtender)GridViewSanction.Rows[rowIndex].FindControl("ModalPopupExtenderOPAmount");
                    //ModalPopupExtenderMisc.Show();
                    //return;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "CallMyFunction", "callthis4()", true);
                    return;

                }
                if (cost.Text == "0" || cost.Text == "0.0")
                {
                    //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert(' Amount must be in numbers!')</script>");
                    string CloseWindow = "alert('Amount must be in numbers')";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);

                    setModalWindowAmount(sender, e);

                    string rowVal2 = Request.Form["rowIndx"];
                    int rowIndex = Convert.ToInt32(rowVal2);
                    //ModalPopupExtender ModalPopupExtenderMisc = (ModalPopupExtender)GridViewSanction.Rows[rowIndex].FindControl("ModalPopupExtenderOPAmount");
                    //ModalPopupExtenderMisc.Show();
                    //return;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "CallMyFunction", "callthis4()", true);
                    return;


                }
                IncentiveSum.Add(AddCost);
                dr = dt.NewRow();
                dr["rowIndexParent"] = rowIndexParent + 1;
                dr["rowIndexChild"] = i + 1;
                dr["indexv"] = Label11.Text;
                dr["Row"] = RowNumber.Text;
                dr["ID"] = ID.Text;
                double amount = Math.Round(Convert.ToDouble(cost.Text), 2);
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
            value = Math.Round(TotalDisp, 2);
        }
        TextBox TotalText = (TextBox)GridViewSanction.Rows[index].Cells[3].FindControl("txtSanOpeAmt");
        TotalText.Text = value.ToString();
        setModalWindowOPAmount(sender, e);
        AddTotal(sender, e);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "CallMyFunction", "ToggleDisplay4()", true);

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
            //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Capital Amount must be in numbers!')</script>");
            string CloseWindow = "alert('Capital Amount must be in numbers')";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);

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
            //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert(Evaluation Form generation failed')</script>");
            string CloseWindow = "alert('Evaluation Form generation failed')";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);

        }
    }

    //protected void TextBoxGrantDate_TextChanged(object sender, EventArgs e)
    //{
    //    if (String.IsNullOrEmpty(txtprojectactualdate.Text))
    //    {
    //        txtprojectactualdate.Text = TextBoxGrantDate.Text;
    //    }

    //}
     protected void PercentageIO_TextChanged(object sender, EventArgs e)
    {
        string SanctionAmount = txtRevisedAppliedAmt.Text.Trim();

        double total = Convert.ToDouble(SanctionAmount);
        foreach (GridViewRow gvr in GridViewInterOrganization.Rows)
        {
          DropDownList  DropdownMuNonMuIO =(DropDownList)gvr.Cells[1].FindControl("DropdownMuNonMuIO");
            TextBox Percentage = (TextBox)gvr.Cells[1].FindControl("PercentageIO");
            //double PercentageV = Convert.ToDecimal(Percentage); ;
            //int PercentageV = Convert.ToInt32(Percentage);
            if (Percentage.Text != "")
            {
                double OrigLon = Convert.ToDouble(Percentage.Text, System.Globalization.CultureInfo.InvariantCulture);
                double value = ((total) * (OrigLon)) / 100;
                double value1 = Math.Round(value, 2);
                TextBox Amount = (TextBox)gvr.Cells[1].FindControl("PercentageIOAmount");
                Amount.Text = Convert.ToString(value);
                if (DropdownMuNonMuIO.SelectedValue == "M")
                {
                    Session["Amount"] = Amount.Text;
                }
            }                   
        }
        if (GridViewInterInstitute.Rows.Count == 1)
        {
            foreach (GridViewRow gvr in GridViewInterInstitute.Rows)
            {
                DropDownList DropdownMuNonMuIO = (DropDownList)gvr.Cells[1].FindControl("DropdownMuNonMuIO");
                TextBox Percentage = (TextBox)gvr.Cells[1].FindControl("PercentageII");
                //double PercentageV = Convert.ToDecimal(Percentage); ;
                //int PercentageV = Convert.ToInt32(Percentage);
                if (Percentage.Text == "100")
                {
                    TextBox Amount = (TextBox)gvr.Cells[1].FindControl("PercentageIIAmount");
                    Amount.Text = Convert.ToString(Session["Amount"].ToString());

                }


            }
        }
        else 
        {
            double total2 = Convert.ToDouble(Session["Amount"].ToString());
            foreach (GridViewRow gvr in GridViewInterInstitute.Rows)
            {
                DropDownList DropdownMuNonMuIO = (DropDownList)gvr.Cells[1].FindControl("DropdownMuNonMuIO");
                TextBox Percentage = (TextBox)gvr.Cells[1].FindControl("PercentageII");
                //double PercentageV = Convert.ToDecimal(Percentage); ;
                //int PercentageV = Convert.ToInt32(Percentage);
                if (Percentage.Text != "")
                {
                    double OrigLon = Convert.ToDouble(Percentage.Text, System.Globalization.CultureInfo.InvariantCulture);
                    double value = ((total2) * (OrigLon)) / 100;
                    double value1 = Math.Round(value, 2);
                    TextBox Amount = (TextBox)gvr.Cells[1].FindControl("PercentageIIAmount");
                    Amount.Text = Convert.ToString(value);

                }


            }
        }
       
    }
    protected void PercentageII_TextChanged1(object sender, EventArgs e)
    {
        string SanctionAmount = null;
        if (Session["Amount"] == null)
        {

            SanctionAmount = Session["PercentAmount"].ToString();
        }
        else
        {
            SanctionAmount = Session["Amount"].ToString();
        }
        double total = Convert.ToDouble(SanctionAmount);
        foreach (GridViewRow gvr in GridViewInterInstitute.Rows)
        {
            DropDownList DropdownMuNonMuIO = (DropDownList)gvr.Cells[1].FindControl("DropdownMuNonMuIO");
            TextBox Percentage = (TextBox)gvr.Cells[1].FindControl("PercentageII");
            //double PercentageV = Convert.ToDecimal(Percentage); ;
            //int PercentageV = Convert.ToInt32(Percentage);
            if (Percentage.Text != "")
            {
                double OrigLon = Convert.ToDouble(Percentage.Text, System.Globalization.CultureInfo.InvariantCulture);
                double value = ((total) * (OrigLon)) / 100;
                double value1 = Math.Round(value, 2);
                TextBox Amount = (TextBox)gvr.Cells[1].FindControl("PercentageIIAmount");
                Amount.Text = Convert.ToString(value);
               
            }


        }
        
    }
    protected void txtRevisedAppliedAmt_TextChanged(object sender, EventArgs e)
    {
        string SanctionAmount = null;
        SanctionAmount = txtRevisedAppliedAmt.Text.Trim();

        double total = Convert.ToDouble(SanctionAmount);
        foreach (GridViewRow gvr in GridViewInterOrganization.Rows)
        {
            DropDownList DropdownMuNonMuIO = (DropDownList)gvr.Cells[1].FindControl("DropdownMuNonMuIO");
            TextBox Percentage = (TextBox)gvr.Cells[1].FindControl("PercentageIO");
            //double PercentageV = Convert.ToDecimal(Percentage); ;
            //int PercentageV = Convert.ToInt32(Percentage);
            if (Percentage.Text != "")
            {
                double OrigLon = Convert.ToDouble(Percentage.Text, System.Globalization.CultureInfo.InvariantCulture);
                double value = ((total) * (OrigLon)) / 100;
                double value1 = Math.Round(value, 2);
                TextBox Amount = (TextBox)gvr.Cells[1].FindControl("PercentageIOAmount");
                Amount.Text = Convert.ToString(value);
                if (DropdownMuNonMuIO.SelectedValue == "M")
                {
                    Session["Amount"] = Amount.Text;
                }
            }
        }
        if (GridViewInterInstitute.Rows.Count == 1)
        {
            foreach (GridViewRow gvr in GridViewInterInstitute.Rows)
            {
                DropDownList DropdownMuNonMuIO = (DropDownList)gvr.Cells[1].FindControl("DropdownMuNonMuIO");
                TextBox Percentage = (TextBox)gvr.Cells[1].FindControl("PercentageII");
                //double PercentageV = Convert.ToDecimal(Percentage); ;
                //int PercentageV = Convert.ToInt32(Percentage);
                if (Percentage.Text == "100")
                {
                    TextBox Amount = (TextBox)gvr.Cells[1].FindControl("PercentageIIAmount");
                    Amount.Text = Convert.ToString(Session["Amount"].ToString());

                }


            }
        }

        if (Session["Amount"] == null)
        {

            SanctionAmount = Session["PercentAmount"].ToString();
        }
        else
        {
            SanctionAmount = Session["Amount"].ToString();
        }
        double total1 = Convert.ToDouble(SanctionAmount);
        foreach (GridViewRow gvr in GridViewInterInstitute.Rows)
        {
            DropDownList DropdownMuNonMuIO = (DropDownList)gvr.Cells[1].FindControl("DropdownMuNonMuIO");
            TextBox Percentage = (TextBox)gvr.Cells[1].FindControl("PercentageII");
            //double PercentageV = Convert.ToDecimal(Percentage); ;
            //int PercentageV = Convert.ToInt32(Percentage);
            if (Percentage.Text != "")
            {
                double OrigLon = Convert.ToDouble(Percentage.Text, System.Globalization.CultureInfo.InvariantCulture);
                double value = ((total1) * (OrigLon)) / 100;
                double value1 = Math.Round(value, 2);
                TextBox Amount = (TextBox)gvr.Cells[1].FindControl("PercentageIIAmount");
                Amount.Text = Convert.ToString(value);

            }


        }
    }
    protected void GridViewProjectsOutcome_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        SetRowDataProjectOutcome();
        if (ViewState["ProjectOutcomeDetails"] != null)
        {
            DataTable dt = (DataTable)ViewState["ProjectOutcomeDetails"];
            DataRow drCurrentRow = null;
            int rowIndex = Convert.ToInt32(e.RowIndex);
            if (dt.Rows.Count > 1 && rowIndex != 0)
            {
                dt.Rows.Remove(dt.Rows[rowIndex]);
                drCurrentRow = dt.NewRow();
                ViewState["ProjectOutcomeDetails"] = dt;
                GridViewProjectsOutcome.DataSource = dt;
                GridViewProjectsOutcome.DataBind();

                SetOldDataProjectOutcomeDetails();
                // gridAmtChanged(sender, e);
            }
        }
    }

    private void SetRowDataProjectOutcome()
    {
        int rowIndex = 0;

        if (ViewState["ProjectOutcomeDetails"] != null)
        {
            DataTable dtCurrentTablePO = (DataTable)ViewState["ProjectOutcomeDetails"];
            DataRow drCurrentRow = null;
            if (dtCurrentTablePO.Rows.Count > 0)
            {
                for (int i = 1; i <= dtCurrentTablePO.Rows.Count; i++)
                {
                    TextBox ProjectOutcomeDescription = (TextBox)GridViewProjectsOutcome.Rows[rowIndex].Cells[1].FindControl("txtProjectOutcomeDescription");

                    drCurrentRow = dtCurrentTablePO.NewRow();
                    dtCurrentTablePO.Rows[i - 1]["Description"] = ProjectOutcomeDescription.Text;
                    rowIndex++;
                }
             
                ViewState["ProjectOutcomeDetails"] = dtCurrentTablePO;

            }

            else
            {
                Response.Write("ViewState is null");
            }
            //SetPreviousData();
        }
    }
    protected void GridViewProjectsOutcome_RowDataBound(object sender, GridViewRowEventArgs e)
    {
      
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
               
            }
        

    }  
    protected void Button1ProjectsOutcome_Click(object sender, EventArgs e)
    {
        if (!Page.IsValid)
        {
            return;
        }

        try
        {
            UpdatePanel12.Update();
            int result = 0;

            DataTable dtCurrentTableProjectOutcome = (DataTable)ViewState["ProjectOutcomeDetails"];
            RecieptData[] JDP = null;
            GrantData journalbank = new GrantData();
            //insert Fund Reciept

            JDP = new RecieptData[dtCurrentTableProjectOutcome.Rows.Count];

            int rowIndex2 = 0;
            if (dtCurrentTableProjectOutcome.Rows.Count > 0)
            {

                for (int i = 0; i < dtCurrentTableProjectOutcome.Rows.Count; i++)
                {
                    JDP[i] = new RecieptData();                
                 
                    TextBox outcomeDate = (TextBox)GridViewProjectsOutcome.Rows[rowIndex2].Cells[1].FindControl("DateofoutcomeDate");
                    TextBox ProjectOutcomeDescription = (TextBox)GridViewProjectsOutcome.Rows[rowIndex2].Cells[2].FindControl("txtProjectOutcomeDescription");
                    if (dtCurrentTableProjectOutcome.Rows.Count > 0)
                    {

                      

                       
                        if (ProjectOutcomeDescription.Text == "")
                        {
                            //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please enter Project Outcome Description!')</script>");
                            //return;
                            string CloseWindow = "alert('Please enter Project Outcome Description!')";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);
                            return;

                        }
                        if (outcomeDate.Text == "")
                        {
                            //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please enter Outcome date!')</script>");
                            //return;
                            string CloseWindow = "alert('Please enter Outcome date!')";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);
                            return;

                        }
                        JDP[i].ProjectOutcomeDescription = ProjectOutcomeDescription.Text.Trim();
                        JDP[i].OutcomeDate = Convert.ToDateTime(outcomeDate.Text);

                        JDP[i].Updatedby = Session["UserId"].ToString();
                      
                        DateTime dateToDisplay = DateTime.Now;             
                        JDP[i].UpdatedDate = Convert.ToDateTime(dateToDisplay);
                        journalbank.GrantUnit = DropDownListGrUnit.SelectedValue;

                        journalbank.GID = TextBoxID.Text;

                    }
                    rowIndex2++;
                }


            }

            if (journalbank.GID != null)
            {
                result = B.InsertProjectOutcomeDetails(JDP, journalbank);
               
            }

            if (result > 0)
            {
                //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Grant Outcome details saved successfully  of ID: " + TextBoxID.Text + "')</script>");
                log.Info("Grant Outcome details saved successfully, of ID: " + TextBoxID.Text);
                string CloseWindow = "alert('Grant Outcome details saved successfully  of ID: " + TextBoxID.Text + "')";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);
                return;

            }
        }

        catch (Exception ex)
        {

            log.Error("Inside Catch Block Of Grant" + ex.Message + " UserID : " + Session["UserId"].ToString());
            log.Error(ex.StackTrace);          
            if (ex.Message.Contains("The string was not recognized as a valid DateTime"))
            {
                //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Date is not valid')</script>");
                string CloseWindow = "alert('Date is not valid')";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);

            }
            if (ex.Message.Contains("String was not recognized as a valid DateTime."))
            {
                //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Date is not valid')</script>");
                string CloseWindow = "alert('Date is not valid')";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);

            }

            else if (ex.Message.Contains("Input string was not in a correct format"))
            {
                //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Error')</script>");
                log.Error("Error, of ID: " + TextBoxID.Text + " , Type: " + DropDownListTypeGrant.SelectedValue);
                string CloseWindow = "alert('Error')";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);

            }
            else
            {

                //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Grant data Creation failed!!!!!!!!!!!!')</script>");
                log.Error("Grant data Creaton Failed.... id: " + TextBoxID.Text + " , Type: " + DropDownListTypeGrant.SelectedValue);
                string CloseWindow = "alert('Grant data Creation failed!!!!!!!!!!!!')";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);

            }
        }
    }
    protected void SetIntialRowProjectOutcome()
    {

        DataTable dy = new DataTable();
        dy.Columns.Add("Description", typeof(string));
        dy.Columns.Add("OutcomeDate", typeof(string));
        DataRow dr = dy.NewRow();     
        dr["Description"] = string.Empty;
        dr["OutcomeDate"] = string.Empty;
        dy.Rows.Add(dr);
        ViewState["ProjectOutcomeDetails"] = dy;
        GridViewProjectsOutcome.DataSource = dy;
        GridViewProjectsOutcome.DataBind();
        DateTime d1 = DateTime.Now;
        string d2 = d1.ToShortDateString();
        TextBox DateofoutcomeDate = (TextBox)GridViewProjectsOutcome.Rows[0].Cells[1].FindControl("DateofoutcomeDate");
        DateofoutcomeDate.Text = d2;
    }
    protected void ADDProjectOutcome_Click(object sender, EventArgs e)
    {

        if (GridViewProjectsOutcome.Rows.Count == 0)
        {
            //BindGridview();
        }


        else
        {
            int rowIndex = 0;

            if (ViewState["ProjectOutcomeDetails"] != null)
            {
                DataTable dt = (DataTable)ViewState["ProjectOutcomeDetails"];
                DataRow drCurrentRow = null;
                if (dt.Rows.Count > 0)
                {
                    for (int i = 1; i <= dt.Rows.Count; i++)
                    {
                        TextBox DateofoutcomeDate = (TextBox)GridViewProjectsOutcome.Rows[rowIndex].Cells[1].FindControl("DateofoutcomeDate");                     
                        TextBox ProjectOutcomeDescription = (TextBox)GridViewProjectsOutcome.Rows[rowIndex].Cells[2].FindControl("txtProjectOutcomeDescription");                     
                        drCurrentRow = dt.NewRow();


                        if (ProjectOutcomeDescription.Text != "")
                        {
                            dt.Rows[i - 1]["Description"] = ProjectOutcomeDescription.Text;                                                   
                        }
                        if (DateofoutcomeDate.Text != "")
                        {
                            DateTime date = Convert.ToDateTime(DateofoutcomeDate.Text.Trim());
                            dt.Rows[i - 1]["OutcomeDate"] = date.ToShortDateString();
                        } 
                        rowIndex++;
                    }
                    dt.Rows.Add(drCurrentRow);
                    ViewState["ProjectOutcomeDetails"] = dt;
                    GridViewProjectsOutcome.DataSource = dt;
                    GridViewProjectsOutcome.DataBind();
                }
            }

            else
            {
                Response.Write("ViewState Value is Null");
            }

            SetOldDataProjectOutcomeDetails();
        }
    }

    private void SetOldDataProjectOutcomeDetails()
    {
        int rowIndex = 0;
        if (ViewState["ProjectOutcomeDetails"] != null)
        {
            DataTable dt = (DataTable)ViewState["ProjectOutcomeDetails"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    TextBox DateofoutcomeDate = (TextBox)GridViewProjectsOutcome.Rows[rowIndex].Cells[1].FindControl("DateofoutcomeDate");    
                    TextBox ProjectOutcomeDescription = (TextBox)GridViewProjectsOutcome.Rows[rowIndex].Cells[2].FindControl("txtProjectOutcomeDescription");
                    if (dt.Rows[i]["Description"].ToString() != "")
                    {
                        ProjectOutcomeDescription.Text = dt.Rows[i]["Description"].ToString();
                     
                    }
                    if (dt.Rows[i]["OutcomeDate"].ToString() != "")
                    {                    
                        DateTime date = Convert.ToDateTime(dt.Rows[i]["OutcomeDate"].ToString());
                        DateofoutcomeDate.Text = date.ToShortDateString();    
                    }
                    rowIndex++;
                }
            }
        }
    }
    protected void imageBkCbtn_Click(object sender, ImageClickEventArgs e)
    {
        setModalWindow1(sender, e);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "CallMyFunction", "callthis1()", true);
        return;
    }

    private void setModalWindow1(object sender, EventArgs e)
    {
        //UpdatePanel3.Update();
        UpdatePanel5.Update();
        popupselectNo.Visible = true;
        MainpanelGrant.Visible = true;
        Panel7.Visible = true;
        popGridagency.DataSourceID = "SqlDataSourceTextBoxGrantAgency";
        SqlDataSourceTextBoxGrantAgency.SelectCommand = "SELECT   FundingAgencyId as Id,UPPER([FundingAgencyName]) as Name FROM [ProjectFundingAgency_M]";
        popGridagency.DataBind();
        int rows = popGridagency.Rows.Count;
        popGridagency.Visible = true;

    }


    protected void showpopup(object sender, EventArgs e)
    {
        setModalWindow2(sender, e);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "CallMyFunction", "callthis2()", true);
        return;

    }
    protected void showpopup1(object sender, EventArgs e)
    {
        setModalWindowstudent(sender, e);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "CallMyFunction", "callthis3()", true);
        return;

    }
    protected void setModalWindow2(object sender, EventArgs e)
    {
        UpdatePanel1.Update();
        UpdatePanel2.Update();
        popupPanelAffil.Visible = true;
        popGridAffil.DataSourceID = "SqlDataSourceAffil";
        SqlDataSourceAffil.DataBind();
        popGridAffil.DataBind();
        int rows = popGridAffil.Rows.Count;
        popGridAffil.Visible = true;
    }

    protected void setModalWindowstudent(object sender, EventArgs e)
    {
        UpdatePanel1.Update();
        UpdatePanel4.Update();
        popupstudent.Visible = true;
        popupStudentGrid.DataSourceID = "StudentSQLDS";
        StudentSQLDS.DataBind();
        popupStudentGrid.DataBind();
        int rows = popupStudentGrid.Rows.Count;
        popupStudentGrid.Visible = true;
    }

    protected void CreditedBanksearchclick(object sender, EventArgs e)
    {
        UpdatePanel15.Update();
        UpdatePanel8.Update();
        setModalWindowCB(sender, e);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "CallMyFunction", "callthis5()", true);
        return;
    }
    protected void exitbnk(object sender, EventArgs e)
    {
        UpdatePanel8.Update();

        Button7.Text = "";
        popupCrB.DataBind();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "CallMyFunction", "ToggleDisplay5()", true);

    }
      protected void ExitADD(object sender, EventArgs e)
    {
        UpdatePanel9.Update();
        popupCrB.DataBind();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "CallMyFunction", "ToggleDisplay6()", true);

    }

      protected void BtnSave_Click1(object sender, EventArgs e)
      {
          UpdatePanel19.Update();
          UpdatePanel18.Update();
          string Type = "Prj";
          //BtnSave_Click(sender, e);
        
              string MemberID = Session["UserId"].ToString();
              FeedbackClass u = new FeedbackClass();
              Journal_DataObject Da = new Journal_DataObject();
              Business B = new Business();
              u = B.CheckUserforFeedback(MemberID, Type);

              string date3 = Convert.ToString(u.ProjectUpdatedDate);

              if (date3 == "01/01/0001 00:00:00")
              {

                  UpdatePanel18.Update();
                  panelfeedback.Visible = true;
                  ScriptManager.RegisterStartupScript(this, this.GetType(), "CallMyFunction", "callthis7()", true);
                  return;

              }
              else
              {

                  string month = ConfigurationManager.AppSettings["FeedBackMonth"].ToString();
                  int month1 = Convert.ToInt32(month);
                  //DateTime actDate = Convert.ToDateTime(u.PublicationUpdatedDate);


                  DateTime fromdate = Convert.ToDateTime(u.ProjectUpdatedDate);
                  DateTime todaydate = DateTime.Now;

                  int resu = B.gettotalmonths(fromdate, todaydate);
                  if (resu >= month1)
                  {

                      UpdatePanel18.Update();
                      panelfeedback.Visible = true;
                      ScriptManager.RegisterStartupScript(this, this.GetType(), "CallMyFunction", "callthis7()", true);
                      return;
                  }
                  else
                  {

                  }
              }

          
      }
      protected void exitfeedback(object sender, EventArgs e)
      {
    
        ScriptManager.RegisterStartupScript(this, this.GetType(), "CallMyFunction", "ToggleDisplay7()", true);

    }
      protected void BtnSubmitFeedback_Click(object sender, EventArgs e)
      {
          TextBoxID.Text = Session["Grantseed"].ToString();
          TextBoxUTN.Text = Session["GrantseedUTNseed"].ToString();
          ButtonSearchProjectOnClick(sender, e);
          UpdatePanel18.Update();
          Journal_DataObject Da = new Journal_DataObject();
          FeedbackClass feedback = new FeedbackClass();
          string MemberID = Session["UserId"].ToString();
          string EntryType = Session["EntryType"].ToString();
          string ID = Session["ID"].ToString();
          string Type = "Prj";

          DateTime Date = DateTime.Now;
          DateTime PublicationUpdatedDate = DateTime.Now;

          feedback.Q1 = "";
          feedback.Q2 = "";
          feedback.Q3 = txtq3.SelectedValue;
          feedback.Q4 = txtq4.Text;
          feedback.Q5 = txtq1.Text;
          feedback.Q6 = txtq2.Text;
          feedback.Q7 = "";
          feedback.Q8 = "";

          if (feedback.Q3 == "" && feedback.Q4 == "" && feedback.Q5 == "" && feedback.Q6 == "")
          {
              string CloseWindow = "alert('Please Enter Your Feedback in the next Entry')";
              ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow, true);
              return;
          }
          bool res = false;
           res = Da.InsertintoFeedbackReviewTracker(feedback, Date, MemberID, EntryType, ID, Type);

          if (res == true)
          {
              //string CloseWindow = "alert('Feedbackdetails Saved Succesfully')";
              //ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);
              string CloseWindow = "alert('Feedbackdetails Saved Succesfully')";
              ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow, true);
              ScriptManager.RegisterStartupScript(this, this.GetType(), "CallMyFunction", "ToggleDisplay7()", true);
              btnSave.Enabled = false;
              TextBoxID.Text = Session["Grantseed"].ToString();
              TextBoxUTN.Text = Session["GrantseedUTNseed"].ToString();
              ButtonSearchProjectOnClick(sender, e);
          }
          else
          {
              string CloseWindow1 = "alert('Error while Saving the Feedback')";
              ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow1, true);

          }


      }

      protected void LinkButton1_Click(object sender, EventArgs e)
      {
          UpdatePanel20.Update();
          UpdatePanel21.Update();
          ScriptManager.RegisterStartupScript(this, this.GetType(), "CallMyFunction", "callthis8()", true);
          return;

      }
      protected void Exitt(object sender, EventArgs e)
      {
          UpdatePanel20.Update();
          UpdatePanel21.Update();
          ScriptManager.RegisterStartupScript(this, this.GetType(), "CallMyFunction", "ToggleDisplay8()", true);
          return;

      }
}






