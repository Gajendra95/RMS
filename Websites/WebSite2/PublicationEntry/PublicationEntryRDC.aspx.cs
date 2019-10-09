using AjaxControlToolkit;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;

public partial class PublicationEntry_PublicationEntryRDC : System.Web.UI.Page
{
    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    string PublicationYearwebConfig = ConfigurationManager.AppSettings["PublicationYear"];
    string FolderPathServerWrite = ConfigurationManager.AppSettings["FolderPath"].ToString();
    public string pageID = "L117";
    protected void Page_Load(object sender, EventArgs e)
    {
       
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
        if (!IsPostBack)
        {
            if (!Session["authPage"].ToString().Contains("$" + pageID + "$"))
            {
                string unacces = "Unauthorized Acces!!! Login Again";
                Response.Redirect("~/Login.aspx?val=" + unacces);
            }
        }
    }




    private void SetInitialRow()
    {

        DataTable dt = new DataTable();
        DataRow dr = null;
        dt.Columns.Add(new DataColumn("DropdownMuNonMu", typeof(string)));
        dt.Columns.Add(new DataColumn("AuthorName", typeof(string)));
        dt.Columns.Add(new DataColumn("EmployeeCode", typeof(string)));
        dt.Columns.Add(new DataColumn("Institution", typeof(string)));
        dt.Columns.Add(new DataColumn("InstitutionName", typeof(string)));
        dt.Columns.Add(new DataColumn("Department", typeof(string)));
        dt.Columns.Add(new DataColumn("DepartmentName", typeof(string)));
        dt.Columns.Add(new DataColumn("MailId", typeof(string)));
        dt.Columns.Add(new DataColumn("isCorrAuth", typeof(string)));
        dt.Columns.Add(new DataColumn("AuthorType", typeof(string)));
        dt.Columns.Add(new DataColumn("NameInJournal", typeof(string)));

        dt.Columns.Add(new DataColumn("IsPresenter", typeof(string)));
        dt.Columns.Add(new DataColumn("HasAttented", typeof(string)));
        dt.Columns.Add(new DataColumn("NationalType", typeof(string)));
        dt.Columns.Add(new DataColumn("ContinentId", typeof(string)));

        dt.Columns.Add(new DataColumn("DropdownStudentInstitutionName", typeof(string)));
        dt.Columns.Add(new DataColumn("DropdownStudentDepartmentName", typeof(string)));


        dr = dt.NewRow();

        dr["DropdownMuNonMu"] = string.Empty;
        // dr["Author"] = string.Empty;
        dr["AuthorName"] = string.Empty;
        dr["EmployeeCode"] = string.Empty;
        dr["Institution"] = string.Empty;
        dr["InstitutionName"] = string.Empty;
        dr["Department"] = string.Empty;
        dr["DepartmentName"] = string.Empty;
        dr["MailId"] = string.Empty;
        dr["isCorrAuth"] = string.Empty;
        dr["AuthorType"] = string.Empty;
        dr["NameInJournal"] = string.Empty;

        dr["IsPresenter"] = string.Empty;
        dr["HasAttented"] = string.Empty;
        dr["NationalType"] = string.Empty;
        dr["ContinentId"] = string.Empty;
        dr["DropdownStudentInstitutionName"] = string.Empty;
        dr["DropdownStudentDepartmentName"] = string.Empty;
        dt.Rows.Add(dr);

        ViewState["CurrentTable"] = dt;
        Grid_AuthorEntry.DataSource = dt;
        Grid_AuthorEntry.DataBind();


        DropDownList DropdownMuNonMu = (DropDownList)Grid_AuthorEntry.Rows[0].FindControl("DropdownMuNonMu");
        TextBox AuthorName = (TextBox)Grid_AuthorEntry.Rows[0].FindControl("AuthorName");
        ImageButton EmployeeCodeBtn = (ImageButton)Grid_AuthorEntry.Rows[0].FindControl("EmployeeCodeBtn");
        TextBox EmployeeCode = (TextBox)Grid_AuthorEntry.Rows[0].FindControl("EmployeeCode");

        HiddenField Institution = (HiddenField)Grid_AuthorEntry.Rows[0].FindControl("Institution");
        TextBox InstitutionName = (TextBox)Grid_AuthorEntry.Rows[0].FindControl("InstitutionName");
        HiddenField Department = (HiddenField)Grid_AuthorEntry.Rows[0].FindControl("Department");
        TextBox DepartmentName = (TextBox)Grid_AuthorEntry.Rows[0].FindControl("DepartmentName");
        TextBox MailId = (TextBox)Grid_AuthorEntry.Rows[0].FindControl("MailId");
        DropDownList isCorrAuth = (DropDownList)Grid_AuthorEntry.Rows[0].FindControl("isCorrAuth");
        DropDownList AuthorType = (DropDownList)Grid_AuthorEntry.Rows[0].FindControl("AuthorType");

        TextBox NameInJournal = (TextBox)Grid_AuthorEntry.Rows[0].FindControl("NameInJournal");
        DropDownList NationalType = (DropDownList)Grid_AuthorEntry.Rows[0].FindControl("NationalType");
        DropDownList ContinentId = (DropDownList)Grid_AuthorEntry.Rows[0].FindControl("ContinentId");
        CheckBox hasattended = (CheckBox)Grid_AuthorEntry.Rows[0].FindControl("HasAttented");
        ImageButton ImageButton1 = (ImageButton)Grid_AuthorEntry.Rows[0].FindControl("ImageButton1");

        NationalType.Visible = false;
        ContinentId.Visible = false;
        EmployeeCode.Enabled = false;
        EmployeeCodeBtn.Visible = false;
        ImageButton1.Visible = true;

        if (DropDownListPublicationEntry.SelectedValue == "JA")
        {
            Grid_AuthorEntry.Columns[9].Visible = false;
            Grid_AuthorEntry.Columns[10].Visible = false;
            //Grid_AuthorEntry.Columns[11].Visible = false;
            //Grid_AuthorEntry.Columns[12].Visible = false;
        }

    }




    protected void DropDownListPublicationEntryOnSelectedIndexChanged(object sender, EventArgs e)
    {
        PubEntriesUpdatePanel.Update();
        EditUpdatePanel.Update();
        popupPanelAffilUpdate.Update();
        MainUpdate.Update();

        setModalWindow(sender, e);
        SetInitialRow();
        Grid_AuthorEntry.Visible = true;
        addclikEntryType(sender, e);
        DropDownListYearJA.Items.Clear();
        DropDownListYearJA.Items.Clear();
        btnSave.Enabled = true;
        ButtonSub.Enabled = true;

        if (DropDownListPublicationEntry.SelectedValue != " ")
        {

            string month = DateTime.Now.Month.ToString();
            DropDownListMonthJA.SelectedValue = month;
            int currenntYear = DateTime.Now.Year;
            int year = Convert.ToInt32(PublicationYearwebConfig);
            int yeardiff = currenntYear - year;
            if (yeardiff < 0)
            {
                yeardiff = -(yeardiff);
            }
            for (int i = 0; i <= yeardiff; i++)
            {
                int yeatAppend = year + i;
                DropDownListYearJA.Items.Add(new ListItem(yeatAppend.ToString(), yeatAppend.ToString(), true));
            }
            DropDownListYearJA.DataBind();
            DropDownListYearJA.SelectedValue = currenntYear.ToString();
            TextBoxPageTo.Enabled = false;
            panelJournalArticle.Visible = true;          
            panelTechReport.Visible = true;           
            panAddAuthor.Visible = true;
            lblnote1.Visible = true;
        }
        RequiredFieldValidator1.Enabled = false;
        EnableValidation();
    }

    private void addclikEntryType(object sender, EventArgs e)
    {
        DropDownListMuCategory.ClearSelection();
        DropDownListMuCategoryOnSelectedIndexChanged(sender, e);
        TextBoxPubId.Text = "";
        txtboxTitleOfWrkItem.Text = "";
        BtnAddMU.Enabled = true;
        TextBoxPubJournal.Text = "";
        TextBoxNameJournal.Text = "";
        TextBoxIssue.Text = "";
        DropDownListMonthJA.ClearSelection();
        DropDownListYearJA.ClearSelection();
        TextBoxImpFact.Text = "";
        DropDownListPubType.ClearSelection();
        TextBoxPageFrom.Text = "";
        TextBoxPageTo.Text = "";
        TextBoxVolume.Text = "";
        RadioButtonListIndexed.SelectedValue = "Y";
        RadioButtonListIndexedOnSelectedIndexChanged(sender, e);
        CheckboxIndexAgency.ClearSelection();
        //TextBoxURL.Text = "";
        TextBoxDOINum.Text = "";
        TextBoxAbstract.Text = "";
        DropDownListErf.ClearSelection();
        TextBoxKeywords.Text = "";
        DropDownListuploadEPrint.ClearSelection();
        TextBoxEprintURL.Text = "";
        txtIFApplicableYear.Text = "";
        TextBoxImpFact5.Text = "";
        EnableValidation();
    }

    protected void showPop(object sender, EventArgs e)
    {
        popupPanelJournal.Visible = true;
        ModalPopupExtender1.Show();
        popup.Visible = false;
    }

    protected void setModalWindow(object sender, EventArgs e)
    {
        if (RadioButtonListIndexed.SelectedValue == "Y")
        {
            popupPanelJournal.Visible = true;
            popGridJournal.DataSourceID = "SqlDataSourceJournal";
            SqlDataSourceJournal.DataBind();
            popGridJournal.DataBind();
        }
        else
        {
            popupPanelJournal.Visible = false;
            popGridJournal.DataSourceID = "SqlDataSourceJournal";
            SqlDataSourceJournal.DataBind();
            popGridJournal.DataBind();
        }

        popup.Visible = true;
        popGridAffil.DataSourceID = "SqlDataSourceAffil";
        SqlDataSourceAffil.DataBind();
        popGridAffil.DataBind();
        popupStudentGrid.DataSourceID = "StudentSQLDS";
        StudentSQLDS.DataBind();
        popupStudentGrid.DataBind();
    }

    protected void exit1(object sender, EventArgs e)
    {
        journalcodeSrch.Text = "";
        popGridJournal.DataBind();
        //popupPanelJournal.Visible = false;
    }


    protected void DropdownMuNonMuOnSelectedIndexChanged(object sender, EventArgs e)
    {
        MainUpdate.Update();
        PubEntriesUpdatePanel.Update();
        EditUpdatePanel.Update();
        popupPanelAffilUpdate.Update();
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
            NameInJournal.Text = "";
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
            NameInJournal.Text = "";
            ImageButton1.Enabled = true;
            ImageButton1.Visible = true;
            EmployeeCodeBtn.Visible = false;
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
            NameInJournal.Text = "";
            ImageButton1.Enabled = false;
            ImageButton1.Visible = true;
        }
        else if (DropdownMuNonMu.SelectedValue == "E")
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
            NameInJournal.Text = "";
            ImageButton1.Enabled = false;
            ImageButton1.Visible = true;
        }
        if (RadioButtonListIndexed.SelectedValue == "Y")
        {
            popupPanelJournal.Visible = true;
            popGridJournal.DataSourceID = "SqlDataSourceJournal";
            SqlDataSourceJournal.DataBind();
            popGridJournal.DataBind();
        }
        else
        {
            popupPanelJournal.Visible = false;
            popGridJournal.DataSourceID = "SqlDataSourceJournal";
            SqlDataSourceJournal.DataBind();
            popGridJournal.DataBind();
        }
    }

    protected void addRow(object sender, EventArgs e)
    {
        PubEntriesUpdatePanel.Update();
        EditUpdatePanel.Update();
        popupPanelAffilUpdate.Update();
        MainUpdate.Update();
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
                        DropDownList isCorrAuth = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].FindControl("isCorrAuth");
                        DropDownList AuthorType = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].FindControl("AuthorType");

                        TextBox NameInJournal = (TextBox)Grid_AuthorEntry.Rows[rowIndex].FindControl("NameInJournal");

                        DropDownList IsPresenter = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].FindControl("IsPresenter");

                        CheckBox HasAttented = (CheckBox)Grid_AuthorEntry.Rows[rowIndex].FindControl("HasAttented");

                        DropDownList NationalType = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].FindControl("NationalType");
                        DropDownList ContinentId = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].FindControl("ContinentId");
                        ImageButton ImageButton1 = (ImageButton)Grid_AuthorEntry.Rows[rowIndex].FindControl("ImageButton1");

                        drCurrentRow = dtCurrentTable.NewRow();
                        dtCurrentTable.Rows[i - 1]["DropdownMuNonMu"] = DropdownMuNonMu.Text;
                        dtCurrentTable.Rows[i - 1]["AuthorName"] = AuthorName.Text;
                        dtCurrentTable.Rows[i - 1]["EmployeeCode"] = EmployeeCode.Text;                      
                        dtCurrentTable.Rows[i - 1]["MailId"] = MailId.Text;
                        dtCurrentTable.Rows[i - 1]["isCorrAuth"] = isCorrAuth.Text;
                        dtCurrentTable.Rows[i - 1]["AuthorType"] = AuthorType.Text;
                        dtCurrentTable.Rows[i - 1]["NameInJournal"] = NameInJournal.Text;
                                        
                        if (DropdownMuNonMu.SelectedValue == "S")
                        {
                            dtCurrentTable.Rows[i - 1]["Institution"] = Institution.Value;
                            dtCurrentTable.Rows[i - 1]["InstitutionName"] = InstitutionName.Text;
                            dtCurrentTable.Rows[i - 1]["Department"] = Department.Value;
                            dtCurrentTable.Rows[i - 1]["DepartmentName"] = DepartmentName.Text;
                        }
                        else if(DropdownMuNonMu.SelectedValue == "O")
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
                        else  if (DropdownMuNonMu.SelectedValue == "E")
                        {
                            dtCurrentTable.Rows[i - 1]["Institution"] = Institution.Value;
                            dtCurrentTable.Rows[i - 1]["InstitutionName"] = InstitutionName.Text;
                            dtCurrentTable.Rows[i - 1]["Department"] = Department.Value;
                            dtCurrentTable.Rows[i - 1]["DepartmentName"] = DepartmentName.Text;
                            dtCurrentTable.Rows[i - 1]["NationalType"] = NationalType.SelectedValue;
                            dtCurrentTable.Rows[i - 1]["ContinentId"] = ContinentId.SelectedValue;

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

                    TextBox NameInJournal = (TextBox)Grid_AuthorEntry.Rows[rowIndex].FindControl("NameInJournal");

                    DropDownList IsPresenter = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].FindControl("IsPresenter");

                    CheckBox HasAttented = (CheckBox)Grid_AuthorEntry.Rows[rowIndex].FindControl("HasAttented");

                    DropDownList NationalType = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].FindControl("NationalType");
                    DropDownList ContinentId = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].FindControl("ContinentId");
                    ImageButton ImageButton1 = (ImageButton)Grid_AuthorEntry.Rows[rowIndex].FindControl("ImageButton1");

                    DropdownMuNonMu.Text = dt.Rows[i]["DropdownMuNonMu"].ToString();
                    AuthorName.Text = dt.Rows[i]["AuthorName"].ToString();
                    EmployeeCode.Text = dt.Rows[i]["EmployeeCode"].ToString();
                    MailId.Text = dt.Rows[i]["MailId"].ToString();
                    isCorrAuth.Text = dt.Rows[i]["isCorrAuth"].ToString();
                    AuthorType.Text = dt.Rows[i]["AuthorType"].ToString();
                    NameInJournal.Text = dt.Rows[i]["NameInJournal"].ToString();

                    IsPresenter.Text = dt.Rows[i]["IsPresenter"].ToString();

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
                    else   if (DropdownMuNonMu.SelectedValue == "E")
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
                    if (Grid_AuthorEntry.Rows.Count == 1)
                    {
                        DropDownList DropdownMuNonMu5 = (DropDownList)Grid_AuthorEntry.Rows[0].Cells[2].FindControl("DropdownMuNonMu");
                        if (DropDownListPublicationEntry.SelectedValue == "JA" || DropDownListPublicationEntry.SelectedValue == "TS")
                        {
                            DropdownMuNonMu5.Enabled = true;
                        }
                        else
                        {
                            DropdownMuNonMu5.Enabled = false;
                        }
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

        TextBox NameInJournal = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("NameInJournal");
        NameInJournal.Text = studentname;

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

    protected void Grid_AuthorEntry_RowDeleting(Object sender, GridViewDeleteEventArgs e)
    {
        MainUpdate.Update();
        PubEntriesUpdatePanel.Update();
        EditUpdatePanel.Update();
        popupPanelAffilUpdate.Update();
        SetRowData();
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            DataRow drCurrentRow = null;
            int rowIndex = Convert.ToInt32(e.RowIndex);
            dt.Rows.Remove(dt.Rows[rowIndex]);
            drCurrentRow = dt.NewRow();
            ViewState["CurrentTable"] = dt;
            Grid_AuthorEntry.DataSource = dt;
            Grid_AuthorEntry.DataBind();
            SetPreviousData();
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


                    //  TextBox Author = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("Author");
                    TextBox AuthorName = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[1].FindControl("AuthorName");
                    ImageButton EmployeeCodeBtnImg = (ImageButton)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("EmployeeCodeBtn");

                    DropDownList DropdownMuNonMu = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[2].FindControl("DropdownMuNonMu");
                    //TextBox amount = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[3].FindControl("amount");
                    TextBox EmployeeCode = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[1].FindControl("EmployeeCode");
                    HiddenField Institution = (HiddenField)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("Institution");
                    TextBox InstitutionName = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[6].FindControl("InstitutionName");
                    HiddenField Department = (HiddenField)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("Department");
                    TextBox DepartmentName = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("DepartmentName");
                    TextBox MailId = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("MailId");

                    DropDownList isCorrAuth = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("isCorrAuth");
                    DropDownList AuthorType = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("AuthorType");

                    TextBox NameInJournal = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("NameInJournal");
                    DropDownList IsPresenter = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("IsPresenter");

                    CheckBox HasAttented = (CheckBox)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("HasAttented");


                    DropDownList DropdownStudentInstitutionName = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("DropdownStudentInstitutionName");
                    DropDownList DropdownStudentDepartmentName = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("DropdownStudentDepartmentName");
                    DropDownList NationalType = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("NationalType");
                    DropDownList ContinentId = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("ContinentId");



                    ImageButton EmployeeCodeBtnImg1 = (ImageButton)Grid_AuthorEntry.Rows[0].Cells[0].FindControl("EmployeeCodeBtn");
                    ImageButton ImageButton1 = (ImageButton)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("ImageButton1");

                    drCurrentRow = dtCurrentTable.NewRow();
                    //  dtCurrentTable.Rows[i - 1]["amount"] = amount.Text.Trim() == "" ? 0 : Convert.ToDecimal(amount.Text);
                    dtCurrentTable.Rows[i - 1]["DropdownMuNonMu"] = DropdownMuNonMu.Text;
                    //  dtCurrentTable.Rows[i - 1]["Author"] = Author.Text;
                    dtCurrentTable.Rows[i - 1]["AuthorName"] = AuthorName.Text;
                    dtCurrentTable.Rows[i - 1]["EmployeeCode"] = EmployeeCode.Text;

                    if (DropdownMuNonMu.Text == "M")
                    {
                        DropdownStudentInstitutionName.Visible = false;
                        DropdownStudentDepartmentName.Visible = false;
                        InstitutionName.Visible = true;
                        DepartmentName.Visible = true;
                        EmployeeCode.Enabled = false;
                        EmployeeCodeBtnImg.Enabled = true;

                        NationalType.Visible = false;
                        ContinentId.Visible = false;

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
                        EmployeeCode.Enabled = false;
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
                        EmployeeCode.Enabled = false;
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
                        MailId.Enabled = true;
                        EmployeeCodeBtnImg.Enabled = false;
                        AuthorName.Enabled = false;
                        dtCurrentTable.Rows[i - 1]["Institution"] = DropdownStudentInstitutionName.SelectedValue;

                        dtCurrentTable.Rows[i - 1]["MailId"] = MailId.Text.Trim();
                        dtCurrentTable.Rows[i - 1]["Department"] = DropdownStudentDepartmentName.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["EmployeeCode"] = EmployeeCode.Text.Trim();

                    }
                    else if (DropdownMuNonMu.Text == "S")
                    {
                        if (DropDownListPublicationEntry.SelectedValue == "JA" || DropDownListPublicationEntry.SelectedValue == "TS")
                        {
                            DropdownStudentInstitutionName.Visible = false;
                            DropdownStudentDepartmentName.Visible = false;
                            InstitutionName.Visible = true;
                            DepartmentName.Visible = true;
                            NationalType.Visible = false;
                            ContinentId.Visible = false;
                            dtCurrentTable.Rows[i - 1]["Institution"] = Institution.Value;
                            dtCurrentTable.Rows[i - 1]["InstitutionName"] = InstitutionName.Text;
                            dtCurrentTable.Rows[i - 1]["Department"] = Department.Value;
                            dtCurrentTable.Rows[i - 1]["DepartmentName"] = DepartmentName.Text;
                            ImageButton1.Visible = true;
                            EmployeeCode.Enabled = false;
                            EmployeeCodeBtnImg.Enabled = false;
                            EmployeeCodeBtnImg.Visible = false;
                        }
                        else
                        {
                            DropdownStudentInstitutionName.Visible = true;
                            DropdownStudentDepartmentName.Visible = true;
                            InstitutionName.Visible = false;
                            DepartmentName.Visible = false;
                            EmployeeCode.Enabled = false;
                            NationalType.Visible = false;
                            ContinentId.Visible = false;
                            dtCurrentTable.Rows[i - 1]["Institution"] = DropdownStudentInstitutionName.SelectedValue;
                            dtCurrentTable.Rows[i - 1]["InstitutionName"] = DropdownStudentInstitutionName.SelectedItem;
                            dtCurrentTable.Rows[i - 1]["Department"] = DropdownStudentDepartmentName.SelectedValue;
                            dtCurrentTable.Rows[i - 1]["DepartmentName"] = DropdownStudentDepartmentName.SelectedItem;
                            EmployeeCodeBtnImg.Enabled = false;
                            EmployeeCodeBtnImg.Visible = true;
                            ImageButton1.Visible = false;
                        }
                        NationalType.Visible = false;
                        ContinentId.Visible = false;


                        dtCurrentTable.Rows[i - 1]["NationalType"] = NationalType.Text;
                        dtCurrentTable.Rows[i - 1]["ContinentId"] = ContinentId.Text;


                        // dtCurrentTable.Rows[i - 1]["Institution"] = DropdownStudentInstitutionName.SelectedValue;
                        //// dtCurrentTable.Rows[i - 1]["InstitutionName"] = DropdownStudentInstitutionName.SelectedItem;
                        // dtCurrentTable.Rows[i - 1]["Department"] = DropdownStudentDepartmentName.SelectedValue;
                        // //dtCurrentTable.Rows[i - 1]["DepartmentName"] = DropdownStudentDepartmentName.SelectedItem;
                    }



                    EmployeeCodeBtnImg1.Enabled = false;

                    dtCurrentTable.Rows[i - 1]["MailId"] = MailId.Text;
                    dtCurrentTable.Rows[i - 1]["isCorrAuth"] = isCorrAuth.Text;
                    dtCurrentTable.Rows[i - 1]["AuthorType"] = AuthorType.Text;

                    dtCurrentTable.Rows[i - 1]["NameInJournal"] = NameInJournal.Text;

                    dtCurrentTable.Rows[i - 1]["IsPresenter"] = IsPresenter.Text;


                    if (HasAttented.Checked == true)
                    {
                        dtCurrentTable.Rows[i - 1]["HasAttented"] = "Y";
                    }
                    else
                    {
                        dtCurrentTable.Rows[i - 1]["HasAttented"] = "N";

                    }
                    if (DropDownListPublicationEntry.SelectedValue == "JA" || DropDownListPublicationEntry.SelectedValue == "TS")
                    {
                        if (DropdownMuNonMu.Text == "M")
                        {
                            EmployeeCodeBtnImg1.Enabled = true;
                        }
                        else if (DropdownMuNonMu.Text == "N")
                        {
                            EmployeeCodeBtnImg1.Enabled = false;
                        }
                        else if (DropdownMuNonMu.Text == "S")
                        {
                            EmployeeCodeBtnImg1.Enabled = false;
                        }
                        else if (DropdownMuNonMu.Text == "E")
                        {
                            EmployeeCodeBtnImg1.Enabled = false;
                        }
                        else if (DropdownMuNonMu.Text == "O")
                        {
                            EmployeeCodeBtnImg1.Enabled = false;
                        }

                        DropdownMuNonMu.Enabled = true;
                    }
                    else
                    {
                        DropdownMuNonMu.Enabled = false;
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
        //SetPreviousData();
    }


    protected void popSelected(Object sender, EventArgs e)
    {
        popGridJournal.Visible = true;
        GridViewRow row = popGridJournal.SelectedRow;

        string Journalid = row.Cells[1].Text;
        string Journalname = row.Cells[2].Text;

        TextBoxPubJournal.Text = Journalid;
        TextBoxNameJournal.Text = Journalname;

        journalcodeSrch.Text = "";
        popGridJournal.DataBind();
        JournalIDTextChanged(sender, e);           
    }

    protected void JournalIDTextChanged(object sender, EventArgs e)
    {
        if (RadioButtonListIndexed.SelectedValue == "Y")
        {
            Business business_obj = new Business();
            JournalData obj = new JournalData();
            obj.JournalID = TextBoxPubJournal.Text;
            JournalData j = new JournalData();
            j = business_obj.JournalEntryCheckExistance(obj);
            if (j.jid != null)
            {
                TextBoxNameJournal.Text = j.name;
                string year = DateTime.Now.Year.ToString();
                JournalData j2 = new JournalData();
                obj.year = DropDownListYearJA.SelectedValue;
                j2 = business_obj.JournalYearwiseCheck(obj);
                if (j2.jid != null)
                {
                    obj.year = DropDownListYearJA.SelectedValue;
                    obj.JournalID = TextBoxPubJournal.Text;
                    obj.month = DropDownListMonthJA.SelectedValue;
                    if (DropDownListYearJA.SelectedValue != "")
                    {

                        JournalData j1 = new JournalData();
                        j1 = business_obj.GetImpactFactorApplicableYear(obj);
                        if (j1.JournalID != "")
                        {
                            TextBoxImpFact.Text = Convert.ToString(j1.impctfact);
                            TextBoxImpFact5.Text = Convert.ToString(j1.fiveimpcrfact);
                            txtIFApplicableYear.Text = j1.year;
                        }
                        else
                        {
                            TextBoxImpFact.Text = "";
                            TextBoxImpFact5.Text = "";
                            txtIFApplicableYear.Text = "";
                        }
                        JournalData jd = new JournalData();
                        Business B = new Business();
                        int jayear = Convert.ToInt16(DropDownListYearJA.SelectedValue);
                        int jamonth = Convert.ToInt16(DropDownListMonthJA.SelectedValue);
                        string frommonth = ConfigurationManager.AppSettings["QuartileMonthFrom"].ToString();
                        int Quartilefrommonth = Convert.ToInt32(frommonth);
                        string Tomonth = ConfigurationManager.AppSettings["QuartileMonthTo"].ToString();
                        int QuartileTomonth = Convert.ToInt32(Tomonth);
                        if (jayear >= 2018 && jamonth >= 7)
                        {
                            jd = B.selectQuartilevalue(TextBoxPubJournal.Text, jayear, jamonth, Quartilefrommonth, QuartileTomonth);
                            lblQuartile.Visible = true;
                            txtquartile.Visible = true;
                            txtquartile.Text = jd.QName;
                            txtquartile.Enabled = false;
                            txtquartileid.Text = jd.Jquartile;
                        }
                        else
                            if (jayear >= 2019 && jamonth >= 1)
                            {
                                jd = B.selectQuartilevalue(TextBoxPubJournal.Text, jayear, jamonth, Quartilefrommonth, QuartileTomonth);
                                lblQuartile.Visible = true;
                                txtquartile.Visible = true;
                                txtquartile.Text = jd.QName;
                                txtquartile.Enabled = false;
                                txtquartileid.Text = jd.Jquartile;
                            }
                    }
                    else
                    {
                        TextBoxImpFact.Text = "";
                        TextBoxImpFact5.Text = "";
                        txtIFApplicableYear.Text = "";
                    }
                    TextBoxNameJournal.Enabled = false;
                    
                }
                else
                {
                    TextBoxImpFact.Text = "";
                    TextBoxImpFact5.Text = "";
                    txtIFApplicableYear.Text = "";
                    TextBoxPubJournal.Text = "";
                    TextBoxNameJournal.Text = "";
                    TextBoxNameJournal.Enabled = false;
                    string CloseWindow1 = "alert('Journal Not Found')";
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow1, true);
                    // ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Journal Not Found')</script>");
                }

            }
            else
            {
                TextBoxImpFact.Text = "";
                TextBoxImpFact5.Text = "";
                txtIFApplicableYear.Text = "";
                TextBoxPubJournal.Text = "";
                TextBoxNameJournal.Enabled = false;
                string CloseWindow1 = "alert('Journal Not Found')";
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow1, true);
                // ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Journal Not Found')</script>");
            }
        }

        else if (RadioButtonListIndexed.SelectedValue == "N")
        {
            TextBoxNameJournal.Enabled = true;
            TextBoxImpFact.Text = "";
            TextBoxImpFact5.Text = "";
            txtIFApplicableYear.Text = "";
        }
    }


    protected void BtnSave_Click(object sender, EventArgs e)
    {

        if (!Page.IsValid)
        {
            return;
        }
        string AppendInstitutionNamess = null;
        int countCorrAuth = 0;
        int countAuthType = 0;
         string value = confirm_value12.Value;
         if (value == "Yes" )
         {
             try
             {

                 Business b = new Business();
                 DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                 ArrayList listIndexAgency = new ArrayList();
                 Business B = new Business();
                 PublishData j = new PublishData();
                 PublishData[] JD = new PublishData[dtCurrentTable.Rows.Count];

                 string confirmValue2 = Request.Form["confirm_value"];
                 if (confirmValue2 == "Yes" || confirmValue2 == null || confirmValue2 == "")
                 {
                     j.PaublicationID = TextBoxPubId.Text.Trim();
                     j.TypeOfEntry = DropDownListPublicationEntry.SelectedValue;
                     j.MUCategorization = DropDownListMuCategory.SelectedValue;
                     j.TitleWorkItem = txtboxTitleOfWrkItem.Text;
                     j.DOINum = TextBoxDOINum.Text.Trim();

                     //Journal Publish Details'
                     string PublishDate = "" + DropDownListYearJA.SelectedValue + "-" + DropDownListMonthJA.SelectedValue + "-01";
                     DateTime pubDate = Convert.ToDateTime(PublishDate);
                     j.PublishDate = pubDate;
                     j.PublishJAYear = DropDownListYearJA.SelectedValue;
                     j.PublishJAMonth = Convert.ToInt16(DropDownListMonthJA.SelectedValue);
                     j.PageFrom = TextBoxPageFrom.Text.Trim();
                     j.PageTo = TextBoxPageTo.Text.Trim();
                     j.Issue = TextBoxIssue.Text.Trim();
                     j.Indexed = RadioButtonListIndexed.SelectedValue;
                     for (int i = 0; i < CheckboxIndexAgency.Items.Count; i++)
                     {
                         if (CheckboxIndexAgency.Items[i].Selected)
                         {
                             listIndexAgency.Add(CheckboxIndexAgency.Items[i].Value);
                         }
                     }
                     j.PublisherOfJournal = TextBoxPubJournal.Text.Trim();
                     j.PublisherOfJournalName = TextBoxNameJournal.Text.Trim();
                     j.ImpactFactor = TextBoxImpFact.Text.Trim(); ;
                     j.ImpactFactor5 = TextBoxImpFact5.Text.Trim();
                     j.Publicationtype = DropDownListPubType.SelectedValue;
                     j.JAVolume = TextBoxVolume.Text.Trim();
                     //


                     //Generic Details
                     j.isERF = DropDownListErf.SelectedValue;
                     //j.url = TextBoxURL.Text.Trim(); ;
                     j.Keywords = TextBoxKeywords.Text.Trim();
                     j.Abstract = TextBoxAbstract.Text.Trim();

                     j.uploadEPrint = DropDownListuploadEPrint.SelectedValue;
                     j.EprintURL = TextBoxEprintURL.Text;
                     if (txtIFApplicableYear.Text.Trim() != "")
                     {
                         j.IFApplicableYear = Convert.ToInt16(txtIFApplicableYear.Text.Trim());
                     }
                     //j.CitationUrl = txtCitation.Text;

                     //
                     string frommonth = ConfigurationManager.AppSettings["QuartileMonthFrom"].ToString();
                     j.Quartilefrommonth = Convert.ToInt32(frommonth);
                     string Tomonth = ConfigurationManager.AppSettings["QuartileMonthTo"].ToString();
                     j.QuartileTomonth = Convert.ToInt32(Tomonth); 
                     j.Status = "NEW";
                     j.CreatedBy = Session["UserId"].ToString();
                     j.CreatedDate = DateTime.Now;
                     j.InstUser = Session["InstituteId"].ToString();
                     j.DeptUser = Session["Department"].ToString();
                     string inst = Session["InstituteId"].ToString();
                     string dept = Session["Department"].ToString();

                     j.uploadEPrint = DropDownListuploadEPrint.SelectedValue;
                     string SupId = null;
                     SupId = b.GetSupId(Session["InstituteId"].ToString(), Session["UserId"].ToString(), Session["Department"].ToString());
                     if (SupId == null)
                     {
                         j.SupervisorID = "";
                     }
                     else
                     {
                         j.SupervisorID = SupId;
                     }

                     string LibId = null;
                     LibId = b.GetLibraryId(inst, dept);
                     j.LibraryId = LibId;
                     j.IsStudentAuthor = "N";
                     j.AutoAppoval = "N";
                     if (j.AutoAppoval == "Y")
                     {
                         j.ApprovedBy = Session["UserId"].ToString();
                         j.ApprovedDate = DateTime.Now;
                     }
                     j.EntryType = "R";

                     int rowIndex1 = 0;
                     if (dtCurrentTable.Rows.Count > 0)
                     {
                         for (int i = 0; i < dtCurrentTable.Rows.Count; i++)
                         {
                             JD[i] = new PublishData();
                             TextBox AuthorName = (TextBox)Grid_AuthorEntry.Rows[rowIndex1].FindControl("AuthorName");
                             DropDownList DropdownMuNonMu = (DropDownList)Grid_AuthorEntry.Rows[rowIndex1].FindControl("DropdownMuNonMu");
                             TextBox EmployeeCode = (TextBox)Grid_AuthorEntry.Rows[rowIndex1].FindControl("EmployeeCode");
                             HiddenField Institution = (HiddenField)Grid_AuthorEntry.Rows[rowIndex1].FindControl("Institution");
                             TextBox InstitutionName = (TextBox)Grid_AuthorEntry.Rows[rowIndex1].FindControl("InstitutionName");
                             HiddenField Department = (HiddenField)Grid_AuthorEntry.Rows[rowIndex1].FindControl("Department");
                             TextBox DepartmentName = (TextBox)Grid_AuthorEntry.Rows[rowIndex1].FindControl("DepartmentName");

                             DropDownList DropdownStudentInstitutionName = (DropDownList)Grid_AuthorEntry.Rows[rowIndex1].FindControl("DropdownStudentInstitutionName");
                             DropDownList DropdownStudentDepartmentName = (DropDownList)Grid_AuthorEntry.Rows[rowIndex1].FindControl("DropdownStudentDepartmentName");


                             TextBox MailId = (TextBox)Grid_AuthorEntry.Rows[rowIndex1].FindControl("MailId");
                             DropDownList isCorrAuth = (DropDownList)Grid_AuthorEntry.Rows[rowIndex1].FindControl("isCorrAuth");
                             DropDownList AuthorType = (DropDownList)Grid_AuthorEntry.Rows[rowIndex1].FindControl("AuthorType");
                             TextBox NameInJournal = (TextBox)Grid_AuthorEntry.Rows[rowIndex1].FindControl("NameInJournal");

                             DropDownList IsPresenter = (DropDownList)Grid_AuthorEntry.Rows[rowIndex1].FindControl("IsPresenter");
                             CheckBox HasAttented = (CheckBox)Grid_AuthorEntry.Rows[rowIndex1].FindControl("HasAttented");


                             DropDownList NationalType = (DropDownList)Grid_AuthorEntry.Rows[rowIndex1].FindControl("NationalType");
                             DropDownList ContinentId = (DropDownList)Grid_AuthorEntry.Rows[rowIndex1].FindControl("ContinentId");

                             if (DropdownMuNonMu.SelectedValue == "O")
                             {
                                 if (EmployeeCode.Text == "")
                                 {
                                     ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please enter Roll No!')</script>");
                                     return;
                                 }
                             }

                             if (AuthorName.Text == "")
                             {
                                 ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please enter Author Name!')</script>");
                                 return;
                             }


                             if (DropdownMuNonMu.SelectedValue == "N" || DropdownMuNonMu.SelectedValue == "E")
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
                             if (DropDownListuploadEPrint.SelectedValue == "N")
                             {

                                 ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please select Yes in  upload To EPrint!')</script>");

                                 btnSave.Enabled = true;

                             }

                             JD[i].AuthorName = AuthorName.Text.Trim();
                             JD[i].MUNonMU = DropdownMuNonMu.Text.Trim();
                             JD[i].EmailId = MailId.Text.Trim();
                             JD[i].isCorrAuth = isCorrAuth.Text.Trim();
                             JD[i].NameInJournal = NameInJournal.Text.Trim();
                             JD[i].AuthorType = AuthorType.Text.Trim();
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
                                 JD[i].EmployeeCode = AuthorName.Text.Trim();
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
                                 JD[i].EmployeeCode = AuthorName.Text.Trim();
                             }
                             else if (JD[i].MUNonMU == "S")
                             {
                                 JD[i].InstitutionName = InstitutionName.Text.Trim();
                                 JD[i].Department = Department.Value.Trim();
                                 JD[i].DepartmentName = DepartmentName.Text.Trim();
                                 JD[i].Institution = Institution.Value.Trim();
                                 JD[i].AppendInstitutions = JD[i].Institution;
                                 JD[i].NationalInternationl = "";
                                 JD[i].continental = "";
                                 JD[i].EmployeeCode = EmployeeCode.Text.Trim();
                             }

                             else if (JD[i].MUNonMU == "O")
                             {
                                 JD[i].NationalInternationl = "";
                                 JD[i].continental = "";
                                 JD[i].InstitutionName = DropdownStudentInstitutionName.SelectedItem.ToString();
                                 JD[i].Department = DropdownStudentDepartmentName.SelectedValue;
                                 JD[i].DepartmentName = DropdownStudentDepartmentName.SelectedItem.ToString();
                                 JD[i].Institution = DropdownStudentInstitutionName.SelectedValue;
                                 JD[i].AppendInstitutions = JD[i].Institution;
                                 JD[i].EmployeeCode = EmployeeCode.Text.Trim();
                             }


                             JD[i].AppendInstitutionNames = JD[i].InstitutionName;
                             JD[i].IsPresenter = "";
                             JD[i].HasAttented = "";
                             JD[i].AuthorCreditPoint = 0;




                             if (DropDownListPublicationEntry.SelectedValue == "JA" || DropDownListPublicationEntry.SelectedValue == "TS")
                             {
                                 if (JD[i].AuthorType == "P")
                                 {
                                     countAuthType = countAuthType + 1;
                                 }

                                 if (JD[i].isCorrAuth == "Y")
                                 {
                                     countCorrAuth = countCorrAuth + 1;
                                 }
                             }

                             rowIndex1++;

                         }

                         string id = null;
                         for (int i = 0; i < JD.Length; i++)
                         {
                             if (AppendInstitutionNamess == null)
                             {
                                 AppendInstitutionNamess = JD[i].InstitutionName.Trim();
                                 id = JD[i].Institution.ToString();
                             }
                             else
                             {
                                 if (!id.Contains(JD[i].Institution))
                                 {
                                     AppendInstitutionNamess = AppendInstitutionNamess + ',' + JD[i].InstitutionName.Trim();
                                     id = id + ',' + JD[i].Institution;
                                 }

                             }
                             j.AppendInstitutionNames = AppendInstitutionNamess.Trim();
                         }

                     }

                     string PublicationEntry1 = DropDownListPublicationEntry.SelectedValue;

                     ArrayList list1 = new ArrayList();
                     for (int i = 0; i < JD.Length; i++)
                     {
                         if (list1.Contains(JD[i].EmployeeCode))
                         {
                             ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please remove duplicate authors from the list')</script>");
                             return;
                         }
                         else
                         {
                             list1.Add(JD[i].EmployeeCode);
                         }

                     }

                     if (TextBoxPubId.Text == "")
                     {
                         int result = 0;

                         result = B.insertPublishEntryRDC(j, JD, listIndexAgency);
                         TextBoxPubId.Text = Session["Pubseed"].ToString();
                         Session["TempPid"] = TextBoxPubId.Text.ToString();
                         Session["TempTypeEntry"] = DropDownListPublicationEntry.SelectedValue;
                         if (result == 1)
                         {
                            
                             if (FileUploadPdf.HasFile)
                             {
                                 SaveUploadedFilePath();
                             }
                             string CloseWindow = "alert('Publication data Created Successfully..of Entry Type: " + DropDownListPublicationEntry.SelectedValue + " of ID: " + TextBoxPubId.Text + " For update Click on search and edit  !')";
                             ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow, true);
                             popup.Visible = false;
                             popupPanelJournal.Visible = false;
                             Session["TempPid"] = TextBoxPubId.Text.ToString();
                             Session["TempTypeEntry"] = DropDownListPublicationEntry.SelectedValue;
                             fnRecordExist(sender, e);
                         }
                         else
                         {
                             string CloseWindow = "alert('Publication creation Error')";
                             ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow, true);

                         }
                     }
                     else
                     {
                         int result = B.UpdatePublishEntryRDC(j, JD, listIndexAgency);
                         if (FileUploadPdf.HasFile)
                         {
                             SaveUploadedFilePath();
                         }
                       
                         popup.Visible = false;
                         popupPanelJournal.Visible = false;
                         Session["TempPid"] = TextBoxPubId.Text.ToString();
                         Session["TempTypeEntry"] = DropDownListPublicationEntry.SelectedValue;
                         fnRecordExist(sender, e);
                         string CloseWindow = "alert('Publication data Updated Successfully of Entry Type: " + DropDownListPublicationEntry.SelectedValue + " of ID: " + TextBoxPubId.Text + "')";
                         ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow, true);

                     }
                     btnSave.Enabled = true;
                     ButtonSub.Enabled = true;

                 }
             }

             catch (Exception ex)
             {
                 if (ex.Message.Contains("The string was not recognized as a valid DateTime"))
                 {
                     string CloseWindow = "alert('Date is not valid!!!!!!!!!!!!')";
                     ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow, true);
                 }
                 else if (ex.Message.Contains("IX_Publication"))
                 {
                     string CloseWindow = "alert('Publication Creation Failed. Title of the WorkItem Already Present!')";
                     ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow, true);
                 }
                 else
                 {
                     string CloseWindow1 = "alert('Publication data Creation Failed!!!!!!!!!!!!')";
                     ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow1, true);
                 }

             }
         }

    }

    private  void SaveUploadedFilePath()
    {
        string PublicationEntry = DropDownListPublicationEntry.SelectedValue;
      
        if (FileUploadPdf.HasFile)
        {
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

                string servername = ConfigurationManager.AppSettings["ServerName"].ToString();
                string domainame = ConfigurationManager.AppSettings["DomainName"].ToString();
                string username = ConfigurationManager.AppSettings["UserName"].ToString();
                string password = ConfigurationManager.AppSettings["Password"].ToString();
                string FolderPathServerwrite = ConfigurationManager.AppSettings["FolderPath"].ToString();

                using (NetworkShareAccesser.Access(servername, domainame, username, password))
                {
                    var uploadfolder = FolderPathServerwrite;
                    string publicationid = Path.Combine(uploadfolder, TextBoxPubId.Text); //main path + location
                    if (!Directory.Exists(publicationid))   //if directory doesnt exist
                    {
                        Directory.CreateDirectory(publicationid);//crete directory of location
                    }
                    string uploadedfilename1 = Path.GetFileName(FileUploadPdf.PostedFile.FileName);
                    string timestamp = DateTime.Now.ToString("dd-MM-yy-hh-mm-ss");
                    string fileextension = Path.GetExtension(uploadedfilename);
                    string actualfilenamewithtime = PublicationEntry + "_" + timestamp + fileextension;

                    string filelocationpath = Path.Combine(publicationid, actualfilenamewithtime);
                    FileUploadPdf.SaveAs(filelocationpath);  //saving file

                    Business bus_obj = new Business();
                    PublishData obj = new PublishData();
                    obj.PaublicationID = TextBoxPubId.Text.Trim();
                    obj.TypeOfEntry = DropDownListPublicationEntry.SelectedValue;
                    obj.FilePath = filelocationpath;
                    int result1 = bus_obj.UpdatePdfPath(obj);
                    btnSave.Enabled = false;
                    Business b1 = new Business();
                    string FileUpload = b1.GetFileUploadPath(obj.PaublicationID, obj.TypeOfEntry);
                    if (FileUpload != "")
                    {
                        GVViewFile.Visible = true;
                    }
                    else
                    {
                        GVViewFile.Visible = false;
                    }
                    DSforgridview.SelectParameters.Clear();
                    DSforgridview.SelectParameters.Add("PaublicationID", obj.PaublicationID);
                    DSforgridview.SelectParameters.Add("TypeOfEntry", obj.TypeOfEntry);
                    DSforgridview.SelectCommand = "select UploadPDFPath from Publication where PublicationID=@PaublicationID  and TypeOfEntry=@TypeOfEntry ";
                    DSforgridview.DataBind();
                    GVViewFile.DataBind();
                }
            }

            else
            {

                string CloseWindow1 = "alert('Invalid File!!!File exceeds 10MB..Please try to upload the file less than or equal to 10MB!!!!!!')";
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow1, true);
                return;
            }
        }
    }

    

    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        if (!Page.IsValid)
        {
            return;
        }

        string AppendInstitutionNamess = null;
        int countCorrAuth = 0;
        int countAuthType = 0;
        try
        {

            Business b = new Business();
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
            ArrayList listIndexAgency = new ArrayList();
            Business B = new Business();
            PublishData j = new PublishData();
            PublishData[] JD = new PublishData[dtCurrentTable.Rows.Count];


            string confirmValue2 = Request.Form["confirm_value"];
            if (confirmValue2 == "Yes" || confirmValue2 == null || confirmValue2 == "")
            {
                if (TextBoxPubId.Text != "")
                {
                    if (FileUploadPdf.HasFile)
                    {
                        SaveUploadedFilePath();
                    }
                    else
                    {
                        if (TextBoxPubId.Text != "")
                        {
                            string FileUpload = b.GetFileUploadPath(TextBoxPubId.Text, DropDownListPublicationEntry.SelectedValue);
                            if (FileUpload == "")
                            {
                                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please upload the file before submission!')</script>");
                                return;
                            }
                        }

                    }
                }
                else
                {
                    if (FileUploadPdf.HasFile)
                    {
                        SaveUploadedFilePath();
                    }
                    else
                    {

                        ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please upload the file before submission!')</script>");
                        return;

                    }
                }


                if (DropDownListPublicationEntry.SelectedValue == "JA" || DropDownListPublicationEntry.SelectedValue == "TS")
                {
                    string month = DropDownListMonthJA.SelectedValue;
                    int month2 = Convert.ToInt32(month);
                    if (month2 < 10 && DropDownListYearJA.Text == "2015")
                    {
                        // Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "script", "alert('Please enter the valid month and year!');", true);
                        string CloseWindow1 = "alert('Please enter the valid month and year!')";
                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow1, true);

                        //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please enter the valid month and year!')</script>");
                        return;
                    }

                }

                if (DropDownListPublicationEntry.SelectedValue == "JA" || DropDownListPublicationEntry.SelectedValue == "TS")
                {
                    string monthnow = DateTime.Now.Month.ToString();
                    string yearnow = DateTime.Now.Year.ToString();

                    int monthnow2 = Convert.ToInt32(monthnow);
                    int yearnow2 = Convert.ToInt32(yearnow);


                    string month = DropDownListMonthJA.SelectedValue;
                    int month2 = Convert.ToInt32(month);

                    string year = DropDownListYearJA.Text;
                    int year2 = Convert.ToInt32(year);

                    if (monthnow2 < month2 && yearnow2 <= year2)
                    {
                        //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please enter the valid month and year!')</script>");
                        string CloseWindow1 = "alert('Please enter the valid month and year')";
                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow1, true);
                        return;
                    }

                }


                if (DropDownListPublicationEntry.SelectedValue == "JA" || DropDownListPublicationEntry.SelectedValue == "TS")
                {
                    if (TextBoxPubJournal.Text == "")
                    {
                        //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please enter the ISSN!')</script>");
                        string CloseWindow1 = "alert('Please enter the ISSN')";
                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow1, true);
                        return;
                    }
                }
                if (DropDownListuploadEPrint.SelectedValue == "Y")
                {
                    if (TextBoxEprintURL.Text == "")
                    {
                        Labeln.Visible = true;
                        ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please enter a Eprint URL')</script>");
                        return;
                    }

                }
                j.PaublicationID = TextBoxPubId.Text.Trim();
                j.TypeOfEntry = DropDownListPublicationEntry.SelectedValue;
                j.MUCategorization = DropDownListMuCategory.SelectedValue;
                j.TitleWorkItem = txtboxTitleOfWrkItem.Text;
                j.DOINum = TextBoxDOINum.Text.Trim();
                string frommonth = ConfigurationManager.AppSettings["QuartileMonthFrom"].ToString();
                j.Quartilefrommonth = Convert.ToInt32(frommonth);
                string Tomonth = ConfigurationManager.AppSettings["QuartileMonthTo"].ToString();
                j.QuartileTomonth = Convert.ToInt32(Tomonth); 
                //Journal Publish Details'
                string PublishDate = "" + DropDownListYearJA.SelectedValue + "-" + DropDownListMonthJA.SelectedValue + "-01";
                DateTime pubDate = Convert.ToDateTime(PublishDate);
                j.PublishDate = pubDate;
                j.PublishJAYear = DropDownListYearJA.SelectedValue;
                j.PublishJAMonth = Convert.ToInt16(DropDownListMonthJA.SelectedValue);
                j.PageFrom = TextBoxPageFrom.Text.Trim();
                j.PageTo = TextBoxPageTo.Text.Trim();
                j.Issue = TextBoxIssue.Text.Trim();
                j.Indexed = RadioButtonListIndexed.SelectedValue;
                for (int i = 0; i < CheckboxIndexAgency.Items.Count; i++)
                {
                    if (CheckboxIndexAgency.Items[i].Selected)
                    {
                        listIndexAgency.Add(CheckboxIndexAgency.Items[i].Value);
                    }
                }
                j.PublisherOfJournal = TextBoxPubJournal.Text.Trim();
                j.PublisherOfJournalName = TextBoxNameJournal.Text.Trim();
                j.ImpactFactor = TextBoxImpFact.Text.Trim(); ;
                j.ImpactFactor5 = TextBoxImpFact5.Text.Trim();
                j.Publicationtype = DropDownListPubType.SelectedValue;
                j.JAVolume = TextBoxVolume.Text.Trim();
                //


                //Generic Details
                j.isERF = DropDownListErf.SelectedValue;
                //j.url = TextBoxURL.Text.Trim(); ;
                j.Keywords = TextBoxKeywords.Text.Trim();
                j.Abstract = TextBoxAbstract.Text.Trim();

                if (txtIFApplicableYear.Text.Trim() != "")
                {
                    j.IFApplicableYear = Convert.ToInt16(txtIFApplicableYear.Text.Trim());
                }
                //j.CitationUrl = txtCitation.Text;

                //

                j.Status = "APP";
                j.CreatedBy = Session["UserId"].ToString();
                j.CreatedDate = DateTime.Now;
                j.InstUser = Session["InstituteId"].ToString();
                j.DeptUser = Session["Department"].ToString();
                string inst = Session["InstituteId"].ToString();
                string dept = Session["Department"].ToString();
                j.uploadEPrint = DropDownListuploadEPrint.SelectedValue;
                j.EprintURL = TextBoxEprintURL.Text;
                string SupId = null;
                SupId = b.GetSupId(Session["InstituteId"].ToString(), Session["UserId"].ToString(), Session["Department"].ToString());
                if (SupId == null)
                {
                    j.SupervisorID = "";
                }
                else
                {
                    j.SupervisorID = SupId;
                }

                string LibId = null;
                LibId = b.GetLibraryId(inst, dept);
                j.LibraryId = LibId;
                j.AutoAppoval = "Y";
                //j.IncentivePointSatatus = "NAP";
                if (j.Indexed == "Y")
                {
                    int publicationmonth2 = Convert.ToInt32(DropDownListMonthJA.SelectedValue);
                    int publicationyear2 = Convert.ToInt32(DropDownListYearJA.SelectedValue);
                    if ((publicationyear2 <= 2016))
                    {
                        Business b1 = new Business();
                        bool resultv = B.checkPredatoryJournal(j.PublisherOfJournal, j.PublishJAYear);
                        if (resultv == true)
                        {
                            j.IncentivePointSatatus = "PRD";
                        }
                        else
                        {
                            j.IncentivePointSatatus = "PEN";
                        }
                    }
                    else
                    {
                        j.IncentivePointSatatus = "PEN";
                    }
                }
                else
                {
                    j.IncentivePointSatatus = "NAP";
                }
                if (DropDownListPublicationEntry.SelectedValue == "JA" && (DropDownListMuCategory.SelectedValue == "LE" || DropDownListMuCategory.SelectedValue == "BK"))
                {
                    if (j.Indexed == "Y")
                    {
                        int publicationmonth = Convert.ToInt32(DropDownListMonthJA.SelectedValue);
                        int publicationyear = Convert.ToInt32(DropDownListYearJA.SelectedValue);
                        Business b1 = new Business();
                        if (DropDownListMuCategory.SelectedValue == "BK")
                        {
                            j.IncentivePointSatatus = "NAP";
                        }
                        else
                        {
                            if ((publicationyear <= 2016))
                            {
                                bool resultv = B.checkPredatoryJournal(j.PublisherOfJournal, j.PublishJAYear);

                                if (resultv == true)
                                {
                                    j.IncentivePointSatatus = "PRD";
                                }
                                else
                                {
                                    j.IncentivePointSatatus = "PEN";
                                }
                            }
                            else
                            {
                                if ((publicationyear >= 2018))
                                {
                                    if (publicationmonth >= 1)
                                    {
                                        j.IncentivePointSatatus = "NAP";
                                    }
                                }
                                if ((publicationyear == 2017))
                                {
                                    if (publicationmonth >= 10)
                                    {
                                        j.IncentivePointSatatus = "NAP";
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        j.IncentivePointSatatus = "NAP";
                    }
                }
                if (DropDownListPublicationEntry.SelectedValue == "JA" && (DropDownListMuCategory.SelectedValue == "SA" || DropDownListMuCategory.SelectedValue == "CR"))
                {
                    if (txtquartileid.Text != "")
                    {
                        string resultPrint = B.CheckPrintEvaluationEnableQuartile(DropDownListMuCategory.SelectedValue, DropDownListMonthJA.SelectedValue, DropDownListYearJA.SelectedValue, txtquartileid.Text);
                        if (resultPrint == "N")
                        {
                            j.IncentivePointSatatus = "NAP";
                        }
                        else
                        {
                            j.IncentivePointSatatus = "PEN";
                        }
                    }
                }
                if (j.AutoAppoval == "Y")
                {
                    for (int i = 0; i < Grid_AuthorEntry.Rows.Count; i++)
                    {

                        DropDownList DropdownMuNonMu = (DropDownList)Grid_AuthorEntry.Rows[i].Cells[2].FindControl("DropdownMuNonMu");
                        DropDownList AuthorType = (DropDownList)Grid_AuthorEntry.Rows[i].Cells[6].FindControl("AuthorType");
                        if (DropdownMuNonMu.SelectedValue == "S" || DropdownMuNonMu.SelectedValue == "O")
                        {
                            if (AuthorType.SelectedValue == "P")
                            {
                                string publicationmonth = DropDownListMonthJA.SelectedValue;
                                string publicationyear = DropDownListYearJA.SelectedValue;
                                DateTime publicationdate = Convert.ToDateTime("01" + '/' + publicationmonth + '/' + publicationyear);
                                string date = ConfigurationManager.AppSettings["StudentAuthorDate"];
                                DateTime datevalue = Convert.ToDateTime(date);
                                int resultdate = DateTime.Compare(publicationdate, datevalue);
                                if (resultdate >= 0)
                                {
                                    /*--changing student author logic :if student is first author consider publication to be student authored -no need to have MU-Staff in author list--*/

                                    //for (int k = 0; k < Grid_AuthorEntry.Rows.Count; k++)
                                    //{
                                    //    DropDownList DropdownMuNonMu1 = (DropDownList)Grid_AuthorEntry.Rows[k].Cells[2].FindControl("DropdownMuNonMu");
                                    //    DropDownList AuthorType1 = (DropDownList)Grid_AuthorEntry.Rows[k].Cells[5].FindControl("isCorrAuth");
                                    //    if (DropdownMuNonMu1.SelectedValue != "N")
                                    //    {
                                    //        if (AuthorType1.SelectedValue == "Y")
                                    //        {
                                    //            j.IsStudentAuthor = "Y";
                                    //            break;
                                    //        }
                                    //        else
                                    //        {
                                    //            j.IsStudentAuthor = "N";
                                    //            continue;
                                    //        }
                                    //    }
                                    //    else
                                    //    {
                                    //        j.IsStudentAuthor = "N";
                                    //        //rowIndex2++;
                                    //        continue;
                                    //    }
                                    //}
                                    j.IsStudentAuthor = "Y";
                                }
                                else
                                {
                                    j.IsStudentAuthor = "N";
                                }
                                break;
                            }
                            else
                            {
                                j.IsStudentAuthor = "N";
                                continue;
                            }
                        }
                        else
                        {
                            j.IsStudentAuthor = "N";
                            //rowIndex2++;
                            continue;
                        }

                    }
                }
                else
                {
                    j.IsStudentAuthor = "N";
                }

                if (j.AutoAppoval == "Y")
                {
                    j.ApprovedBy = Session["UserId"].ToString();
                    j.ApprovedDate = DateTime.Now;
                }
                j.EntryType = "R";

                int rowIndex1 = 0;
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 0; i < dtCurrentTable.Rows.Count; i++)
                    {
                        JD[i] = new PublishData();
                        TextBox AuthorName = (TextBox)Grid_AuthorEntry.Rows[rowIndex1].FindControl("AuthorName");
                        DropDownList DropdownMuNonMu = (DropDownList)Grid_AuthorEntry.Rows[rowIndex1].FindControl("DropdownMuNonMu");
                        TextBox EmployeeCode = (TextBox)Grid_AuthorEntry.Rows[rowIndex1].FindControl("EmployeeCode");
                        HiddenField Institution = (HiddenField)Grid_AuthorEntry.Rows[rowIndex1].FindControl("Institution");
                        TextBox InstitutionName = (TextBox)Grid_AuthorEntry.Rows[rowIndex1].FindControl("InstitutionName");
                        HiddenField Department = (HiddenField)Grid_AuthorEntry.Rows[rowIndex1].FindControl("Department");
                        TextBox DepartmentName = (TextBox)Grid_AuthorEntry.Rows[rowIndex1].FindControl("DepartmentName");

                        DropDownList DropdownStudentInstitutionName = (DropDownList)Grid_AuthorEntry.Rows[rowIndex1].FindControl("DropdownStudentInstitutionName");
                        DropDownList DropdownStudentDepartmentName = (DropDownList)Grid_AuthorEntry.Rows[rowIndex1].FindControl("DropdownStudentDepartmentName");


                        TextBox MailId = (TextBox)Grid_AuthorEntry.Rows[rowIndex1].FindControl("MailId");
                        DropDownList isCorrAuth = (DropDownList)Grid_AuthorEntry.Rows[rowIndex1].FindControl("isCorrAuth");
                        DropDownList AuthorType = (DropDownList)Grid_AuthorEntry.Rows[rowIndex1].FindControl("AuthorType");
                        TextBox NameInJournal = (TextBox)Grid_AuthorEntry.Rows[rowIndex1].FindControl("NameInJournal");

                        DropDownList IsPresenter = (DropDownList)Grid_AuthorEntry.Rows[rowIndex1].FindControl("IsPresenter");
                        CheckBox HasAttented = (CheckBox)Grid_AuthorEntry.Rows[rowIndex1].FindControl("HasAttented");


                        DropDownList NationalType = (DropDownList)Grid_AuthorEntry.Rows[rowIndex1].FindControl("NationalType");
                        DropDownList ContinentId = (DropDownList)Grid_AuthorEntry.Rows[rowIndex1].FindControl("ContinentId");

                        if (DropdownMuNonMu.SelectedValue == "O")
                        {
                            if (EmployeeCode.Text == "")
                            {
                                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please enter Roll No!')</script>");
                                return;
                            }
                        }

                        if (AuthorName.Text == "")
                        {
                            ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please enter Author Name!')</script>");
                            return;
                        }


                        if (DropdownMuNonMu.SelectedValue == "N" || DropdownMuNonMu.SelectedValue == "E")
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
                        JD[i].EmailId = MailId.Text.Trim();
                        JD[i].isCorrAuth = isCorrAuth.Text.Trim();
                        JD[i].NameInJournal = NameInJournal.Text.Trim();
                        JD[i].AuthorType = AuthorType.Text.Trim();
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
                            JD[i].EmployeeCode = AuthorName.Text.Trim();
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
                            JD[i].EmployeeCode = AuthorName.Text.Trim();
                        }

                        else if (JD[i].MUNonMU == "S")
                        {
                            JD[i].InstitutionName = InstitutionName.Text.Trim();
                            JD[i].Department = Department.Value.Trim();
                            JD[i].DepartmentName = DepartmentName.Text.Trim();
                            JD[i].Institution = Institution.Value.Trim();
                            JD[i].AppendInstitutions = JD[i].Institution;
                            JD[i].NationalInternationl = "";
                            JD[i].continental = "";
                            JD[i].EmployeeCode = EmployeeCode.Text.Trim();
                        }

                        else if (JD[i].MUNonMU == "O")
                        {
                            JD[i].NationalInternationl = "";
                            JD[i].continental = "";
                            JD[i].InstitutionName = DropdownStudentInstitutionName.SelectedItem.ToString();
                            JD[i].Department = DropdownStudentDepartmentName.SelectedValue;
                            JD[i].DepartmentName = DropdownStudentDepartmentName.SelectedItem.ToString();
                            JD[i].Institution = DropdownStudentInstitutionName.SelectedValue;
                            JD[i].AppendInstitutions = JD[i].Institution;
                            JD[i].EmployeeCode = EmployeeCode.Text.Trim();
                        }


                        JD[i].AppendInstitutionNames = JD[i].InstitutionName;
                        JD[i].IsPresenter = "";
                        JD[i].HasAttented = "";
                        JD[i].AuthorCreditPoint = 0;




                        if (DropDownListPublicationEntry.SelectedValue == "JA" || DropDownListPublicationEntry.SelectedValue == "TS")
                        {
                            if (JD[i].AuthorType == "P")
                            {
                                countAuthType = countAuthType + 1;
                            }

                            if (JD[i].isCorrAuth == "Y")
                            {
                                countCorrAuth = countCorrAuth + 1;
                            }
                        }

                        rowIndex1++;

                    }

                    string id = null;
                    for (int i = 0; i < JD.Length; i++)
                    {
                        if (AppendInstitutionNamess == null)
                        {
                            AppendInstitutionNamess = JD[i].InstitutionName.Trim();
                            id = JD[i].Institution.ToString();
                        }
                        else
                        {
                            if (!id.Contains(JD[i].Institution))
                            {
                                AppendInstitutionNamess = AppendInstitutionNamess + ',' + JD[i].InstitutionName.Trim();
                                id = id + ',' + JD[i].Institution;
                            }

                        }
                        j.AppendInstitutionNames = AppendInstitutionNamess.Trim();
                    }

                }

                string PublicationEntry1 = DropDownListPublicationEntry.SelectedValue;

                ArrayList list1 = new ArrayList();
                for (int i = 0; i < JD.Length; i++)
                {
                    if (list1.Contains(JD[i].EmployeeCode))
                    {
                        ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please remove duplicate authors from the list')</script>");
                        return;
                    }
                    else
                    {
                        list1.Add(JD[i].EmployeeCode);
                    }

                }
                if (DropDownListPublicationEntry.SelectedValue == "JA" || DropDownListPublicationEntry.SelectedValue == "TS")
                {
                    if (countAuthType > 1)
                    {
                        ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('First Author cannot be more than one!')</script>");
                        return;

                    }
                    if (countAuthType == 0)
                    {
                        ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Select atleast one Author Type as First Author !')</script>");
                        return;

                    }

                    if (countCorrAuth > 1)
                    {
                        ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Corresponding Author cannot be more than one!')</script>");
                        return;

                    }

                    if (countCorrAuth == 0)
                    {
                        ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Select atleast one Corresponding Author!')</script>");
                        return;

                    }
                }
                int result1 = 0;
                if (DropDownListPublicationEntry.SelectedValue == "JA" || DropDownListPublicationEntry.SelectedValue == "TS")
                {
                    if (j.PublisherOfJournal != "" && j.JAVolume != "" && j.PageFrom != "" && j.PageTo != "" && j.Issue != "")
                    {
                        Business b1 = new Business();
                        PublishData publishdata = new PublishData();
                        ArrayList list = new ArrayList();
                        list = b1.chekDuplicateJournalEntry(j);
                        if (list.Count == 0)
                        {
                            result1 = 0;
                        }
                        else
                        {
                            for (int i = 0; i < list.Count; i++)
                            {
                                string id = list[i].ToString();
                                if (id != TextBoxPubId.Text)
                                {
                                    Session["id"] = list[i].ToString();
                                    result1 = 1;
                                    break;
                                }
                                else
                                {
                                    continue;
                                }
                            }
                            //if (publishdata.PaublicationID != TextBoxPubId.Text)
                            //{
                            //    result = 1;
                            //}
                        }
                    }

                }
                if (result1 == 1)
                {
                    log.Info("Entered article already exist for ISSN: " + j.PublisherOfJournal + " , JA Volume: " + j.JAVolume + " , Issue: " + j.Issue + " , Page From :" + j.PageFrom + ",Page To:" + j.PageTo + ",ID:" + Session["id"]);
                    ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Entered article already exist for ISSN: " + j.PublisherOfJournal + " , JA Volume: " + j.JAVolume + " , Issue: " + j.Issue + " , Page From :" + j.PageFrom + ",Page To:" + j.PageTo + "')</script>");
                    return;
                }
                else
                {
                    int result = 0;

                    if (TextBoxPubId.Text == "")
                    {
                       
                        result = B.insertPublishEntryRDC(j, JD, listIndexAgency);
                        TextBoxPubId.Text = Session["Pubseed"].ToString();
                        if (FileUploadPdf.HasFile)
                        {
                            SaveUploadedFilePath();
                        }
                        if (result >= 1)
                        {
                            string isrevert = B.SelectIsReverFlag(j.PaublicationID, j.TypeOfEntry);
                            EmailDetails details = new EmailDetails();
                            details = SendMail(j.PaublicationID, j.TypeOfEntry, j.AutoAppoval, isrevert, j.IsStudentAuthor);
                            details.Id = TextBoxPubId.Text;
                            details.Type = DropDownListPublicationEntry.SelectedValue;
                            SendMailObject obj = new SendMailObject();
                            bool resultv = obj.InsertIntoEmailQueue(details);
                            string CloseWindow = "alert('Publication data Approved Successfully..of Entry Type: " + DropDownListPublicationEntry.SelectedValue + " of ID: " + TextBoxPubId.Text + " For update Click on search and edit  !')";
                            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow, true);
                            addclik(sender, e);
                            popup.Visible = false;
                            popupPanelJournal.Visible = false;
                            dataBind();
                            btnSave.Enabled = false;
                            ButtonSub.Enabled = false;
                        }
                        else
                        {
                            string CloseWindow = "alert('Publication creation Error')";
                            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow, true);
                            btnSave.Enabled = true;
                            ButtonSub.Enabled = true;
                        }
                    }
                    else
                    {
                         result = B.UpdatePublishEntryRDC(j, JD, listIndexAgency);
                        if (result >= 1)
                        {
                            string isrevert = B.SelectIsReverFlag(j.PaublicationID, j.TypeOfEntry);
                            EmailDetails details = new EmailDetails();
                            details = SendMail(j.PaublicationID, j.TypeOfEntry, j.AutoAppoval, isrevert, j.IsStudentAuthor);
                            details.Id = TextBoxPubId.Text;
                            details.Type = DropDownListPublicationEntry.SelectedValue;
                            SendMailObject obj = new SendMailObject();
                            bool resultv = obj.InsertIntoEmailQueue(details);
                            string CloseWindow = "alert('Publication data Approved Successfully..of Entry Type: " + DropDownListPublicationEntry.SelectedValue + " of ID: " + TextBoxPubId.Text + " For update Click on search and edit  !')";
                            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow, true);
                            addclik(sender, e);
                            popup.Visible = false;
                            popupPanelJournal.Visible = false;
                            dataBind();
                            btnSave.Enabled = false;
                            ButtonSub.Enabled = false;
                        }
                        else
                        {
                            string CloseWindow = "alert('Error while approving data')";
                            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow, true);
                            btnSave.Enabled = true;
                            ButtonSub.Enabled = true;

                        }
                    }

                }
            }
        }

        catch (Exception ex)
        {
            if (ex.Message.Contains("The string was not recognized as a valid DateTime"))
            {
                string CloseWindow = "alert('Date is not valid!!!!!!!!!!!!')";
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow, true);
            }
            else if (ex.Message.Contains("IX_Publication"))
            {
                string CloseWindow = "alert('Publication Creation Failed. Title of the WorkItem Already Present!')";
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow, true);
            }
            else
            {
                string CloseWindow1 = "alert('Publication data Creation Failed!!!!!!!!!!!!')";
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow1, true);
            }

        }
    }


    private EmailDetails SendMail(string publicatinid, string typeofentry, string autoapproval, string isrevert, string studentauthor)
    {
        log.Debug("Publication Entry: Inside Send Mail function of Entry Type : " + typeofentry + " ID " + publicatinid);
        EmailDetails details = new EmailDetails();
        try
        {

            details.FromEmail = ConfigurationManager.AppSettings["FromAddress"].ToString();

            details.Id = publicatinid;
            details.Type = typeofentry;

            ArrayList authoremailidlist = new ArrayList();
            ArrayList supervisoremailidlist = new ArrayList();
            ArrayList authorname = new ArrayList();
            ArrayList studentlist = new ArrayList();

            DataSet student = new DataSet();
            DataSet ds = new DataSet();

            Business bus = new Business();
            ds = bus.getAuthorListRDC(TextBoxPubId.Text, DropDownListPublicationEntry.SelectedValue);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                authoremailidlist.Add(ds.Tables[0].Rows[i]["EmailId"].ToString());
            }
            
            details.Module = "PAPP";
                      
            DataSet dy = new DataSet();
            dy = bus.getAuthorListName(TextBoxPubId.Text, DropDownListPublicationEntry.SelectedValue);
            for (int i = 0; i < dy.Tables[0].Rows.Count; i++)
            {
                authorname.Add(dy.Tables[0].Rows[i]["AuthorName"].ToString());
            }

            string auhtorsS = "";
            string auhtorsSConc = "";
            for (int i = 0; i < authorname.Count; i++)
            {
                auhtorsS = authorname[i].ToString();
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

            details.EmailSubject = "Publication Entry <  " + DropDownListPublicationEntry.SelectedValue + " _ " + TextBoxPubId.Text + "  > Approved ";

            string FooterText = ConfigurationManager.AppSettings["FooterText"].ToString();
            if (isrevert == "N")
            {
                details.MsgBody = "<span style=\"font-size: 10pt; color: #3300cc; font-family: Verdana\"><h4>Dear Sir/Madam,</h4> <br>" +
                     "<b> New Publication is added to Publication Repository – following are the details of the entry <br> " +
                     "<br>" +
                       "Publication Type  : " + DropDownListPublicationEntry.SelectedItem + "<br>" +
                    "Publication Id  :  " + TextBoxPubId.Text + "<br>" +

                    "Title of Work Item  : " + txtboxTitleOfWrkItem.Text + "<br>" +

                    "Added By  : " + authorname[0].ToString() + "<br>" +
                    "Authors  : " + auhtorsSConc + "<br>" + "<br>" + "<br>" + "<br>" + "<br>" + FooterText +
                    " </b><br><b> </b></span>";
            }
            else
            {
                details.MsgBody = "<span style=\"font-size: 10pt; color: #3300cc; font-family: Verdana\"><h4>Dear Sir/Madam,</h4> <br>" +
                     "<b> New Publication is added to Publication Repository – following are the details of the entry <br> " +
                     "<br>" +
                       "Publication Type  : " + DropDownListPublicationEntry.SelectedItem + "<br>" +
                    "Publication Id  :  " + TextBoxPubId.Text + "<br>" +

                    "Title of Work Item  : " + txtboxTitleOfWrkItem.Text + "<br>" +

                    "Added By  : " + authorname[0].ToString() + "<br>" +
                    "Authors  : " + auhtorsSConc + "<br>" +
                    " </b><br><b> </b>" +
                    "Note: This article is re-submitted by the author " + "<br>" + "<br>" + "<br>" + "<br>" + "<br>" + FooterText +
                        "</span>";
            }


            IndexManage v = new IndexManage();
            Business bs = new Business();
            string name = Session["UserName"].ToString();
            string userid = Session["UserId"].ToString();
            v = bs.selectIndexAgency1(userid);

            string dep1 = Session["Department"].ToString();
            string inst1 = Session["InstituteId"].ToString();

            v = bs.selectIndexAgency2(dep1, inst1);
            string mailid = v.emailid;
            details.ToEmail = mailid;
                
            return details;
        }

        catch (Exception ex)
        {
            log.Error(ex.StackTrace);
            log.Error(ex.Message);
            return details;
        }
    }

    protected void popSelected1(Object sender, EventArgs e)
    {
        PubEntriesUpdatePanel.Update();
        EditUpdatePanel.Update();
        MainUpdate.Update();
        popupPanelAffilUpdate.Update();
    }

    protected void exit(object sender, EventArgs e)
    {
        PubEntriesUpdatePanel.Update();
        EditUpdatePanel.Update();
        popupPanelAffilUpdate.Update();
        MainUpdate.Update();
        affiliateSrch.Text = "";
        popGridAffil.DataBind();
    }
    protected void branchNameChanged(object sender, EventArgs e)
    {
        PubEntriesUpdatePanel.Update();
        EditUpdatePanel.Update();
        popupPanelAffilUpdate.Update();
        MainUpdate.Update();
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

    protected void txtboxYear_TextChanged(object sender, EventArgs e)
    {
        TextBoxPubJournal.Text = "";
        TextBoxNameJournal.Text = "";
        TextBoxImpFact.Text = "";
        TextBoxImpFact5.Text = "";
        txtIFApplicableYear.Text = "";
        txtquartile.Text = "";
        if (DropDownListPublicationEntry.SelectedValue == "JA" || DropDownListPublicationEntry.SelectedValue == "TS")
        {
            for (int i = 0; i < Grid_AuthorEntry.Rows.Count; i++)
            {

                DropDownList DropdownMuNonMu = (DropDownList)Grid_AuthorEntry.Rows[i].Cells[2].FindControl("DropdownMuNonMu");
                DropDownList AuthorType = (DropDownList)Grid_AuthorEntry.Rows[i].Cells[6].FindControl("AuthorType");
                if (DropdownMuNonMu.SelectedValue == "S" || DropdownMuNonMu.SelectedValue == "O")
                {
                    if (AuthorType.SelectedValue == "P")
                    {
                        string publicationmonth = DropDownListMonthJA.SelectedValue.ToString();
                        string publicationyear = DropDownListYearJA.SelectedValue;
                        DateTime publicationdate = Convert.ToDateTime("01" + '/' + publicationmonth + '/' + publicationyear);
                        string date = ConfigurationManager.AppSettings["StudentAuthorDate"];
                        DateTime datevalue = Convert.ToDateTime(date);
                        int resultdate = DateTime.Compare(publicationdate, datevalue);
                        if (resultdate >= 0)
                        {
                            //for (int k = 0; k < Grid_AuthorEntry.Rows.Count; k++)
                            //{
                            //    DropDownList DropdownMuNonMu1 = (DropDownList)Grid_AuthorEntry.Rows[k].Cells[2].FindControl("DropdownMuNonMu");
                            //    DropDownList AuthorType1 = (DropDownList)Grid_AuthorEntry.Rows[k].Cells[5].FindControl("isCorrAuth");
                            //    if (DropdownMuNonMu1.SelectedValue == "M" || DropdownMuNonMu1.SelectedValue == "S" || DropdownMuNonMu.SelectedValue == "O")
                            //    {
                            //        if (AuthorType1.SelectedValue == "Y")
                            //        {
                            //            StudentPub.Value = "Y";
                            //            break;
                            //        }
                            //        else
                            //        {
                            //            StudentPub.Value = "N";
                            //            continue;
                            //        }
                            //        //break;
                            //    }
                            //    else
                            //    {
                            //        StudentPub.Value = "N";
                            //        //rowIndex2++;
                            //        continue;
                            //    }
                            //}
                            StudentPub.Value = "Y";
                        }
                        else
                        {
                            StudentPub.Value = "N";
                        }
                        break;
                    }
                    else
                    {
                        StudentPub.Value = "N";
                        continue;
                    }
                }
                else
                {
                    StudentPub.Value = "N";
                    //  rowindex3++;
                    continue;
                }

            }
        }
        else
        {
            StudentPub.Value = "N";
        }
    }
    protected void TextBoxPageFrom_TextChanged(object sender, EventArgs e)
    {
        if (TextBoxPageFrom.Text != "")
        {
            TextBoxPageTo.Enabled = true;
            RequiredFieldValidator1.Enabled = true;
        }
        else
        {
            TextBoxPageTo.Enabled = false;
            TextBoxPageTo.Text = "";
            RequiredFieldValidator1.Enabled = false;
        }
    }
    protected void RadioButtonListIndexedOnSelectedIndexChanged(object sender, EventArgs e)
    {
        EnableValidation();
        TextBoxPubJournal.Text = "";
        TextBoxNameJournal.Text = "";
        TextBoxImpFact.Text = "";
        TextBoxImpFact5.Text = "";
        txtIFApplicableYear.Text = "";
        if (RadioButtonListIndexed.SelectedValue == "N")
        {
            popupPanelJournal.Visible = false;
            CheckboxIndexAgency.Enabled = false;
            CheckboxIndexAgency.ClearSelection();
            imageBkCbtn.Visible = false;
            TextBoxNameJournal.Enabled = true;
        }
        else
        {
            popupPanelJournal.Visible = true;
            imageBkCbtn.Visible = true;
            imageBkCbtn.Enabled = true;
            CheckboxIndexAgency.Enabled = true;
            TextBoxNameJournal.Enabled = false;

        }
    }
   
   
    protected void DropDownListMuCategoryOnSelectedIndexChanged(object sender, EventArgs e)
    {
        PubEntriesUpdatePanel.Update();
        EditUpdatePanel.Update();
        popupPanelAffilUpdate.Update();
        MainUpdate.Update();
        if (DropDownListPublicationEntry.SelectedValue == "JA" || DropDownListPublicationEntry.SelectedValue == "TS")
        {

            if (DropDownListMuCategory.SelectedValue == " ")
            {
                Labeljastar1.Visible = false;
                Labeljastr2.Visible = false;
                Labeljastr3.Visible = false;
                //Labeljastr7.Visible = false;
                Labelvolstar8.Visible = false;
            }
            else if (DropDownListMuCategory.SelectedValue != "LE")
            {
                Labeljastar1.Visible = true;
                Labeljastr2.Visible = true;
                Labeljastr3.Visible = true;
                //Labeljastr7.Visible = true;
                Labelvolstar8.Visible = true;
            }
            else
            {
                Labeljastar1.Visible = false;
                Labeljastr2.Visible = false;
                Labeljastr3.Visible = false;
                //Labeljastr7.Visible = false;
                Labelvolstar8.Visible = false;
            }
        }
        else
        {
            Labeljastar1.Visible = false;
            Labeljastr2.Visible = false;
            Labeljastr3.Visible = false;
            Labelvolstar8.Visible = false;
            //Labeljastr7.Visible = false;
        }
        EnableValidation();
    }
  
    protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
    }
    protected void NationalTypeOnSelectedIndexChanged(object sender, EventArgs e)
    {
        PubEntriesUpdatePanel.Update();
        EditUpdatePanel.Update();
        popupPanelAffilUpdate.Update();
        MainUpdate.Update();
        TextBox senderBox = sender as TextBox;
        GridViewRow currentRow = (GridViewRow)((DropDownList)sender).Parent.Parent;
        DropDownList DropdownMuNonMu = (DropDownList)currentRow.FindControl("DropdownMuNonMu");
        DropDownList NationalType = (DropDownList)currentRow.FindControl("NationalType");
        DropDownList ContinentId = (DropDownList)currentRow.FindControl("ContinentId");
        if (NationalType.SelectedValue == "I")
        {
            ContinentId.Visible = true;
        }
        else
        {
            ContinentId.Visible = false;
        }
        NationalType.Visible = true;
    }

    protected void IsPresenterIsPresenter(object sender, EventArgs e)
    {
    }
    

    protected void AuthorName_Changed(object sender, EventArgs e)
    {
        PubEntriesUpdatePanel.Update();
        EditUpdatePanel.Update();
        popupPanelAffilUpdate.Update();
        MainUpdate.Update();
        GridViewRow currentRow = (GridViewRow)((TextBox)sender).Parent.Parent;
        TextBox AuthorName = (TextBox)currentRow.FindControl("AuthorName");
        TextBox NameInJournal = (TextBox)currentRow.FindControl("NameInJournal");
        TextBox InstitutionName = (TextBox)currentRow.FindControl("InstitutionName");
        NameInJournal.Text = AuthorName.Text;

    }

    protected void ButtonSearchPubOnClick(object sender, EventArgs e)
    {
        
        addclik(sender, e);
        PubEntriesUpdatePanel.Update();
        EditUpdatePanel.Update();
        popupPanelAffilUpdate.Update();
        MainUpdate.Update();
        UpdatePanel1.Update();
        SetInitialRow();
        Grid_AuthorEntry.Visible=true;
        UpdatePanel1.Update();
        btnSave.Enabled = false;
        ButtonSub.Enabled = false;
        GridViewSearch.Visible = true;
        GridViewSearch.EditIndex = -1;
        dataBind();
        popup.Visible = false;
        popupPanelJournal.Visible = false;
    }
    private void dataBind()
    {
        Business obj = new Business();
        PublishData pub_obj=new PublishData();
        pub_obj.PaublicationID=PubIDSearch.Text.Trim();
        pub_obj.TypeOfEntry=EntryTypesearch.SelectedValue;
        pub_obj.TitleWorkItem=TextBoxWorkItemSearch.Text.Trim();
        pub_obj.CreatedBy = Session["UserId"].ToString();
        DataSet da = new DataSet();
        da = obj.SelectPublications(pub_obj);
        int a = da.Tables[0].Rows.Count;
        GridViewSearch.Visible = true;
        GridViewSearch.DataSource = da;
        GridViewSearch.DataBind();
        
    }
    public void edit(Object sender, GridViewEditEventArgs e)
    {
        UpdatePanel1.Update();
        PubEntriesUpdatePanel.Update();
        EditUpdatePanel.Update();
        popupPanelAffilUpdate.Update();
        MainUpdate.Update();
        GridViewSearch.EditIndex = e.NewEditIndex;
        fnRecordExist(sender, e);
        dataBind();

    }

    //Function of edit button
    public void GridView2_RowCommand(Object sender, GridViewCommandEventArgs e)
    {

        string pid = null;
        string typeEntry = null;
        if (e.CommandName == "Edit")
        {
             GridViewRow rowSelect = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
            int rowindex = rowSelect.RowIndex;
            HiddenField TypeOfEntry = (HiddenField)GridViewSearch.Rows[rowindex].Cells[6].FindControl("TypeOfEntry");
            typeEntry = TypeOfEntry.Value;
            pid = GridViewSearch.Rows[rowindex].Cells[1].Text.Trim().ToString();
            Session["TempPid"] = pid;
            Session["TempTypeEntry"] = typeEntry;
        }
       
    }


    protected void GridViewSearchPub_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        dataBind();
        GridViewSearch.PageIndex = e.NewPageIndex;
        GridViewSearch.DataBind();
    }


    private void fnRecordExist(object sender, EventArgs e)
    {

        PubEntriesUpdatePanel.Update();
        EditUpdatePanel.Update();
        popupPanelAffilUpdate.Update();
        MainUpdate.Update();
        UpdatePanel1.Update();
        Business obj = new Business();
        panAddAuthor.Enabled = true;
        BtnAddMU.Enabled = true;
        btnSave.Enabled = true;
        ButtonSub.Enabled = true;
        panelJournalArticle.Visible = true;
        panelTechReport.Visible = true;
        string Pid = Session["TempPid"].ToString();
        string TypeEntry = Session["TempTypeEntry"].ToString();

        string FileUpload = obj.GetFileUploadPath(Pid, TypeEntry);
        if (FileUpload != "")
        {
            GVViewFile.Visible = true;
        }
        else
        {
            GVViewFile.Visible = false;
        }
        DSforgridview.SelectParameters.Clear();
        DSforgridview.SelectParameters.Add("PublicationID", Pid);
        DSforgridview.SelectParameters.Add("TypeOfEntry", TypeEntry);

        DSforgridview.SelectCommand = "select UploadPDFPath from Publication where PublicationID=@PublicationID and TypeOfEntry=@TypeOfEntry";
        DSforgridview.DataBind();
        GVViewFile.DataBind();

        PublishData v = new PublishData();
        v = obj.fnfindjid(Pid, TypeEntry);

        DropDownListMuCategory.SelectedValue = v.MUCategorization;
        DropDownListPublicationEntry.SelectedValue = TypeEntry;
        DropDownListPublicationEntry.Enabled = false;
        TextBoxPubId.Text = Pid;
        txtboxTitleOfWrkItem.Text = v.TitleWorkItem;
        TextBoxPubJournal.Text = v.PublisherOfJournal;
        TextBoxNameJournal.Text = v.PublisherOfJournalName;
        DropDownListMonthJA.SelectedValue = v.PublishJAMonth.ToString();

        int currenntYear = DateTime.Now.Year;
        int year = Convert.ToInt32(PublicationYearwebConfig);

        int yeardiff = currenntYear - year;

        if (yeardiff < 0)
        {
            yeardiff = -(yeardiff);
        }
        for (int i = 0; i <= yeardiff; i++)
        {
            int yeatAppend = year + i;
            DropDownListYearJA.Items.Add(new ListItem(yeatAppend.ToString(), yeatAppend.ToString(), true));
        }
        DropDownListYearJA.DataBind();
        DropDownListYearJA.SelectedValue = v.PublishJAYear;

        TextBoxImpFact.Text = v.ImpactFactor;
        TextBoxImpFact5.Text = v.ImpactFactor5;
        if (v.IFApplicableYear != 0)
        {
            txtIFApplicableYear.Text = v.IFApplicableYear.ToString();
        }

        //txtCitation.Text = v.CitationUrl;
        DropDownListPubType.SelectedValue = v.Publicationtype;
        if (TypeEntry == "JA")
        {
            JournalData jd = new JournalData();
            Business B = new Business();
            int jayear = Convert.ToInt16(DropDownListYearJA.SelectedValue);
            int jamonth = Convert.ToInt16(v.PublishJAMonth);
            if (jayear >= 2018 && jamonth >= 7)
            {
                jd = B.selectQuartilevaluefrompublicationEntry(TextBoxPubId.Text, DropDownListMuCategory.SelectedValue, DropDownListPublicationEntry.SelectedValue);
                lblQuartile.Visible = true;
                txtquartile.Visible = true;
                txtquartile.Text = jd.QName;
                txtquartileid.Text = jd.Jquartile;
            }
            else if (jayear >= 2019 && jamonth >= 1)
            {
                jd = B.selectQuartilevaluefrompublicationEntry(TextBoxPubId.Text, DropDownListMuCategory.SelectedValue, DropDownListPublicationEntry.SelectedValue);
                lblQuartile.Visible = true;
                txtquartile.Visible = true;
                txtquartile.Text = jd.QName;
                txtquartileid.Text = jd.Jquartile;
            }
        }
        if (v.PageFrom.Contains("PF"))
        {
            TextBoxPageFrom.Text = "";
            TextBoxPageTo.Text = "";
            TextBoxPageTo.Enabled = false;
        }
        else
        {
            TextBoxPageFrom.Text = v.PageFrom;
            TextBoxPageTo.Text = v.PageTo;
            TextBoxPageTo.Enabled = true;
        }
       
       
        TextBoxIssue.Text = v.Issue;
        TextBoxVolume.Text = v.JAVolume;
        RadioButtonListIndexed.SelectedValue = v.Indexed;
        CheckboxIndexAgency.DataBind();
        SqlDataSourceCheckboxIndexAgency.DataBind();
        if (RadioButtonListIndexed.SelectedValue == "Y")
        {
            popupPanelJournal.Visible = true;
            imageBkCbtn.Visible = true;
            imageBkCbtn.Enabled = true;
            CheckboxIndexAgency.Enabled = true;
            string IndexAgency = null;
            DataSet dz = obj.fnfindjournalAccount1(Pid, TypeEntry);

            int count = dz.Tables[0].Rows.Count;
            for (int i = 0; i < count; i++)
            {

                if (dz.Tables[0].Rows[i]["agencyid"].ToString() != null)
                {
                    IndexAgency = dz.Tables[0].Rows[i]["agencyid"].ToString();
                    ListItem currentCheckBox = (ListItem)CheckboxIndexAgency.Items.FindByValue(IndexAgency);
                    currentCheckBox.Selected = true;
                }

            }
        }
        else
        {
            popupPanelJournal.Visible = false;
            CheckboxIndexAgency.Enabled = false;
            CheckboxIndexAgency.ClearSelection();
            imageBkCbtn.Visible = false;
        }

        //TextBoxURL.Text = v.url;
        TextBoxDOINum.Text = v.DOINum;
        TextBoxAbstract.Text = v.Abstract;
        DropDownListErf.SelectedValue = v.isERF;
        TextBoxKeywords.Text = v.Keywords;
        DropDownListuploadEPrint.DataBind();
        DropDownListuploadEPrint.SelectedValue = v.uploadEPrint;
        TextBoxEprintURL.Text = v.EprintURL;


        DataTable dy = obj.fnfindjournalAccount(Pid, TypeEntry);
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
                TextBox NameAsInJournal = (TextBox)Grid_AuthorEntry.Rows[rowIndex].FindControl("NameInJournal");

                DropDownList IsPresenter = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].FindControl("IsPresenter");
                CheckBox HasAttented = (CheckBox)Grid_AuthorEntry.Rows[rowIndex].FindControl("HasAttented");

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
               
                MailId.Text = dtCurrentTable.Rows[i - 1]["MailId"].ToString();
                AuthorType.Text = dtCurrentTable.Rows[i - 1]["AuthorType"].ToString();
                isCorrAuth.Text = dtCurrentTable.Rows[i - 1]["isCorrAuth"].ToString();
                NameAsInJournal.Text = dtCurrentTable.Rows[i - 1]["NameInJournal"].ToString();

                IsPresenter.SelectedValue = dtCurrentTable.Rows[i - 1]["IsPresenter"].ToString();

                if (dtCurrentTable.Rows[i - 1]["HasAttented"].ToString() == "Y")
                {
                    HasAttented.Checked = true;
                }
                else
                {
                    HasAttented.Checked = false;
                }

                if (DropdownMuNonMu.Text == "N")
                {
                    EmployeeCodeBtnimg.Enabled = false;
                    AuthorName.Enabled = true;
                    InstNme.Enabled = true;
                    deptname.Enabled = true;
                    MailId.Enabled = true;
                    EmployeeCode.Enabled = false;

                }
                else if (DropdownMuNonMu.Text == "E")
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
                    if (DropDownListPublicationEntry.SelectedValue == "JA" || DropDownListPublicationEntry.SelectedValue == "TS")
                    {
                        AuthorName.Enabled = false;
                        DropdownStudentInstitutionName.Visible = false;
                        DropdownStudentInstitutionName.Visible = false;
                        EmployeeCodeBtnimg.Visible = false;
                        EmployeeCode.Enabled = false;
                        NationalType.Visible = false;
                        ContinentId.Visible = false;
                    }
                    else
                    {
                        AuthorName.Enabled = true;
                        DropdownStudentInstitutionName.Enabled = true;
                        DropdownStudentInstitutionName.Enabled = true;
                        EmployeeCodeBtnimg.Enabled = false;
                        EmployeeCode.Enabled = false;
                        NationalType.Visible = false;
                        ContinentId.Visible = false;
                    }

                    MailId.Enabled = true;
                }
                else
                {
                    MailId.Enabled = true;
                }


                if (DropDownListPublicationEntry.SelectedValue == "JA" || DropDownListPublicationEntry.SelectedValue == "TS")
                {

                    isCorrAuth.Visible = true;
                    AuthorType.Visible = true;
                    NameAsInJournal.Visible = true;
                    Grid_AuthorEntry.Columns[6].Visible = true;
                    Grid_AuthorEntry.Columns[7].Visible = true;
                    Grid_AuthorEntry.Columns[8].Visible = true;
                    Grid_AuthorEntry.Columns[9].Visible = false;
                    Grid_AuthorEntry.Columns[10].Visible = false;
                    Grid_AuthorEntry.Columns[1].Visible = true;
                }

                else if (DropDownListPublicationEntry.SelectedValue == "CP")
                {
                    isCorrAuth.Visible = true;
                    AuthorType.Visible = true;
                    NameAsInJournal.Visible = true;
                    Grid_AuthorEntry.Columns[6].Visible = false;
                    Grid_AuthorEntry.Columns[7].Visible = false;
                    Grid_AuthorEntry.Columns[8].Visible = false;
                    Grid_AuthorEntry.Columns[9].Visible = true;
                    Grid_AuthorEntry.Columns[10].Visible = true;
                    Grid_AuthorEntry.Columns[1].Visible = false;
                }
                else
                {
                    isCorrAuth.Visible = false;
                    AuthorType.Visible = false;
                    NameAsInJournal.Visible = false;
                    Grid_AuthorEntry.Columns[6].Visible = false;
                    Grid_AuthorEntry.Columns[7].Visible = false;
                    Grid_AuthorEntry.Columns[8].Visible = false;
                    Grid_AuthorEntry.Columns[9].Visible = false;
                    Grid_AuthorEntry.Columns[10].Visible = false;
                    Grid_AuthorEntry.Columns[1].Visible = false;
                }

                Grid_AuthorEntry.Columns[13].Visible = true;
                rowIndex++;

            }


            ViewState["CurrentTable"] = dtCurrentTable;
        }
      
        setModalWindow(sender, e);
        ButtonSub.Enabled = true;
        btnSave.Enabled = true;
        GridViewSearch.Visible = true;
        EnableValidation();
    }

    protected void GVViewFile_SelectedIndexChanged(object sender, EventArgs e)
    {
        PubEntriesUpdatePanel.Update();
        EditUpdatePanel.Update();
        popupPanelAffilUpdate.Update();
        MainUpdate.Update();
        string BoxId = TextBoxPubId.Text.Trim();
        string servername = ConfigurationManager.AppSettings["ServerName"].ToString();
        string domainame = ConfigurationManager.AppSettings["DomainName"].ToString();
        string username = ConfigurationManager.AppSettings["UserName"].ToString();
        string password = ConfigurationManager.AppSettings["Password"].ToString();
        string folderpath;
        string path_BoxId;
        using (NetworkShareAccesser.Access(servername, domainame, username, password))
        {

            folderpath = ConfigurationManager.AppSettings["FolderPath"].ToString();
            path_BoxId = Path.Combine(folderpath, BoxId);

            int id = GVViewFile.SelectedIndex;
            Label filepath = (Label)GVViewFile.Rows[id].FindControl("lblgetid");
            string path = filepath.Text;       //actual filelocation path  
            string newpath = path.Replace('\\', '/');  // replacing '\' by '/' to view image or pdf

            Session["path"] = newpath;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Test", "ViewPdf()", true);
        }

    }

    protected void addclik(object sender, EventArgs e)
    {
        BtnAddMU.Enabled = true;
        DropDownListPublicationEntry.Enabled = true;
        DropDownListPublicationEntry.ClearSelection();
        DropDownListMuCategory.ClearSelection();
        TextBoxPubId.Text = "";
        txtboxTitleOfWrkItem.Text = "";
        panAddAuthor.Visible = false;
        Grid_AuthorEntry.DataSource = null;
        Grid_AuthorEntry.DataBind();
        Grid_AuthorEntry.Visible = false;
        panelJournalArticle.Visible = false;
        TextBoxPubJournal.Text = "";
        TextBoxNameJournal.Text = "";
        TextBoxIssue.Text = "";
        DropDownListMonthJA.ClearSelection();
        TextBoxImpFact.Text = "";
        TextBoxImpFact5.Text = "";
        //txtCitation.Text = "";
        txtIFApplicableYear.Text = "";
        DropDownListPubType.ClearSelection();
        TextBoxPageFrom.Text = "";
        TextBoxPageTo.Text = "";
        TextBoxVolume.Text = "";
        RadioButtonListIndexed.SelectedValue = "Y";
        CheckboxIndexAgency.ClearSelection();     
        ButtonSub.Enabled = false;
        panelTechReport.Visible = false;
        //TextBoxURL.Text = "";
        TextBoxDOINum.Text = "";
        TextBoxAbstract.Text = "";
        DropDownListErf.ClearSelection();
        TextBoxKeywords.Text = "";
        DropDownListuploadEPrint.ClearSelection();
        TextBoxEprintURL.Text = "";
        popup.Visible = false;
        popupPanelJournal.Visible = false;     
        btnSave.Enabled = false;
    }


    //protected void SearchStudentData(object sender, EventArgs e)
    //{
    //    PubEntriesUpdatePanel.Update();
    //    EditUpdatePanel.Update();
    //    popupPanelAffilUpdate.Update();
    //    MainUpdate.Update();
    //    if (txtSrchStudentName.Text.Trim() == "" && txtSrchStudentRollNo.Text.Trim() == "" && StudentIntddl.SelectedValue == "")
    //    {
           

    //        StudentSQLDS.SelectCommand = "Select TOP 10  RollNo,Name,SISClass.ClassName as ClassName,SISInstitution.InstName as InstName,EmailID1,SISStudentGenInfo.ClassCode as ClassCode,SISInstnHR.HRInstitute as InstID from SISStudentGenInfo,SISClass,SISInstitution,SISInstnHR  where SISStudentGenInfo.ClassCode=SISClass.ClassCode and SISStudentGenInfo.InstID=SISInstitution.InstID and SISInstnHR.Institute_Id=SISInstitution.InstID and SISInstnHR.Institute_Id=SISStudentGenInfo.InstID";
    //        popupStudentGrid.DataBind();
    //        popupStudentGrid.Visible = true;
    //    }

    //    else if ((txtSrchStudentName.Text.Trim() != "" || txtSrchStudentRollNo.Text.Trim() != "") && StudentIntddl.SelectedValue == "")
    //    {
    //        StudentSQLDS.SelectParameters.Clear();
    //        StudentSQLDS.SelectParameters.Add("Name", txtSrchStudentName.Text);
    //        StudentSQLDS.SelectParameters.Add("RollNo", txtSrchStudentRollNo.Text);


    //        StudentSQLDS.SelectCommand = "Select TOP 10  RollNo,Name,SISClass.ClassName as ClassName,SISInstitution.InstName as InstName,EmailID1 ,SISStudentGenInfo.ClassCode as ClassCode,SISInstnHR.HRInstitute as InstID from SISStudentGenInfo,SISClass,SISInstitution,SISInstnHR  where SISStudentGenInfo.ClassCode=SISClass.ClassCode and SISStudentGenInfo.InstID=SISInstitution.InstID and SISInstnHR.Institute_Id=SISInstitution.InstID and SISInstnHR.Institute_Id=SISStudentGenInfo.InstID and  Name like '' + @Name + '%' and RollNo like '%' + @RollNo + '%'";
    //        popupStudentGrid.DataBind();
    //        popupStudentGrid.Visible = true;
    //    }
    //    else
    //    {
    //        StudentSQLDS.SelectParameters.Clear();
    //        StudentSQLDS.SelectParameters.Add("Name", txtSrchStudentName.Text);
    //        StudentSQLDS.SelectParameters.Add("RollNo", txtSrchStudentRollNo.Text);
    //        StudentSQLDS.SelectParameters.Add("InstID", StudentIntddl.SelectedValue);


    //        StudentSQLDS.SelectCommand = "Select TOP 10  RollNo,Name,SISClass.ClassName as ClassName,SISInstitution.InstName as InstName,EmailID1 ,SISStudentGenInfo.ClassCode as ClassCode,SISInstnHR.HRInstitute as InstID from SISStudentGenInfo,SISClass,SISInstitution,SISInstnHR  where SISStudentGenInfo.ClassCode=SISClass.ClassCode and SISStudentGenInfo.InstID=SISInstitution.InstID and SISInstnHR.Institute_Id=SISInstitution.InstID and SISInstnHR.Institute_Id=SISStudentGenInfo.InstID and  (Name like '' + @Name + '%' and RollNo like '%' + RollNo + '%' and (SISStudentGenInfo.InstID=@InstID) ) ";
    //        popupStudentGrid.DataBind();
    //        popupStudentGrid.Visible = true;
    //    }

    //    // string rowVal = Request.Form["rowIndx"];
    //    string a = rowVal.Value;
    //    int rowIndex = Convert.ToInt32(a);
    //    DropDownList munonmu = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].FindControl("DropdownMuNonMu");
    //    if (munonmu.SelectedValue == "M")
    //    {
    //        popupPanelAffil.Style.Add("display", "true");
    //        popupstudent.Style.Add("display", "none");
    //    }
    //    else if (munonmu.SelectedValue == "S")
    //    {
    //        popupstudent.Visible = true;
    //        popupstudent.Style.Add("display", "true");
    //        popupPanelAffil.Style.Add("display", "none");
    //    }
    //    popup.Visible = true;
    //    ModalPopupExtender ModalPopupExtender8 = (ModalPopupExtender)Grid_AuthorEntry.Rows[rowIndex].FindControl("ModalPopupExtender2");
    //    ModalPopupExtender8.Show();
    //}

    protected void SearchStudentData(object sender, EventArgs e)
    {
        //PubEntriesUpdatePanel.Update();
        //EditUpdatePanel.Update();
        //popupPanelAffilUpdate.Update();
        //MainUpdate.Update();
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


            StudentSQLDS.SelectCommand = "Select TOP 10  RollNo,Name,SISClass.ClassName as ClassName,SISInstitution.InstName as InstName,EmailID1 ,SISStudentGenInfo.ClassCode as ClassCode,SISInstnHR.HRInstitute as InstID from SISStudentGenInfo,SISClass,SISInstitution,SISInstnHR  where SISStudentGenInfo.ClassCode=SISClass.ClassCode and SISStudentGenInfo.InstID=SISInstitution.InstID and SISInstnHR.Institute_Id=SISInstitution.InstID and SISInstnHR.Institute_Id=SISStudentGenInfo.InstID and   RollNo like '%' + @txtSrchStudentRollNo+ '%'";
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

            StudentSQLDS.SelectCommand = "Select TOP 10  RollNo,Name,SISClass.ClassName as ClassName,SISInstitution.InstName as InstName,EmailID1 ,SISStudentGenInfo.ClassCode as ClassCode,SISInstnHR.HRInstitute as InstID from SISStudentGenInfo,SISClass,SISInstitution,SISInstnHR  where SISStudentGenInfo.ClassCode=SISClass.ClassCode and SISStudentGenInfo.InstID=SISInstitution.InstID and SISInstnHR.Institute_Id=SISInstitution.InstID and SISInstnHR.Institute_Id=SISStudentGenInfo.InstID and RollNo like '%' + @txtSrchStudentRollNo+ '%' and (SISStudentGenInfo.InstID=@StudentIntddl)";
            popupStudentGrid.DataBind();
            popupStudentGrid.Visible = true;
        }
        else if ((txtSrchStudentName.Text.Trim() != "" && txtSrchStudentRollNo.Text.Trim() == "") && StudentIntddl.SelectedValue != "")
        {

            StudentSQLDS.SelectParameters.Add("txtSrchStudentName", txtSrchStudentName.Text.Trim());
            StudentSQLDS.SelectParameters.Add("StudentIntddl", StudentIntddl.SelectedValue);

            StudentSQLDS.SelectCommand = "Select TOP 10  RollNo,Name,SISClass.ClassName as ClassName,SISInstitution.InstName as InstName,EmailID1 ,SISStudentGenInfo.ClassCode as ClassCode,SISInstnHR.HRInstitute as InstID from SISStudentGenInfo,SISClass,SISInstitution,SISInstnHR  where SISStudentGenInfo.ClassCode=SISClass.ClassCode and SISStudentGenInfo.InstID=SISInstitution.InstID and SISInstnHR.Institute_Id=SISInstitution.InstID and SISInstnHR.Institute_Id=SISStudentGenInfo.InstID and Name like '%' + @txtSrchStudentName + '%' and (SISStudentGenInfo.InstID=@StudentIntddl)";
            popupStudentGrid.DataBind();
            popupStudentGrid.Visible = true;
        }



//ends

        else if ((txtSrchStudentName.Text.Trim() != "" || txtSrchStudentRollNo.Text.Trim() != "") && StudentIntddl.SelectedValue == "")
        {
            StudentSQLDS.SelectParameters.Clear();
            StudentSQLDS.SelectParameters.Add("txtSrchStudentName", txtSrchStudentName.Text);
            StudentSQLDS.SelectParameters.Add("txtSrchStudentRollNo", txtSrchStudentRollNo.Text);


            StudentSQLDS.SelectCommand = "Select TOP 10  RollNo,Name,SISClass.ClassName as ClassName,SISInstitution.InstName as InstName,EmailID1 ,SISStudentGenInfo.ClassCode as ClassCode,SISInstnHR.HRInstitute as InstID from SISStudentGenInfo,SISClass,SISInstitution,SISInstnHR  where SISStudentGenInfo.ClassCode=SISClass.ClassCode and SISStudentGenInfo.InstID=SISInstitution.InstID and SISInstnHR.Institute_Id=SISInstitution.InstID and SISInstnHR.Institute_Id=SISStudentGenInfo.InstID and  Name like '%'+ @txtSrchStudentName + '%' and RollNo like '%' + @txtSrchStudentRollNo + '%'";
            popupStudentGrid.DataBind();
            popupStudentGrid.Visible = true;
        }
        else
        {
            StudentSQLDS.SelectParameters.Clear();
            StudentSQLDS.SelectParameters.Add("txtSrchStudentName", txtSrchStudentName.Text);
            StudentSQLDS.SelectParameters.Add("txtSrchStudentRollNo", txtSrchStudentRollNo.Text);
            StudentSQLDS.SelectParameters.Add("StudentIntddl", StudentIntddl.SelectedValue);


            StudentSQLDS.SelectCommand = "Select TOP 10  RollNo,Name,SISClass.ClassName as ClassName,SISInstitution.InstName as InstName,EmailID1 ,SISStudentGenInfo.ClassCode as ClassCode,SISInstnHR.HRInstitute as InstID from SISStudentGenInfo,SISClass,SISInstitution,SISInstnHR  where SISStudentGenInfo.ClassCode=SISClass.ClassCode and SISStudentGenInfo.InstID=SISInstitution.InstID and SISInstnHR.Institute_Id=SISInstitution.InstID and SISInstnHR.Institute_Id=SISStudentGenInfo.InstID and  (Name like '' + @txtSrchStudentName + '%' and RollNo like '%' + @txtSrchStudentRollNo + '%' and (SISStudentGenInfo.InstID=@StudentIntddl) ) ";
           
            popupStudentGrid.DataBind();
            popupStudentGrid.Visible = true;
        }

        // string rowVal = Request.Form["rowIndx"];
        string a = rowVal.Value;
        int rowIndex = Convert.ToInt32(a);
        DropDownList munonmu = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].FindControl("DropdownMuNonMu");
       // aug 02
        //if (munonmu.SelectedValue == "M")
        //{
        //    popupPanelAffil.Style.Add("display", "true");
        //    popupstudent.Style.Add("display", "none");
        //}
        //else if (munonmu.SelectedValue == "S")
        //{
        //    popupstudent.Visible = true;
        //    popupstudent.Style.Add("display", "true");
        //    popupPanelAffil.Style.Add("display", "none");
        //}
        //popup.Visible = true;
        //ModalPopupExtender ModalPopupExtender8 = (ModalPopupExtender)Grid_AuthorEntry.Rows[rowIndex].FindControl("ModalPopupExtender2");
        //ModalPopupExtender8.Show();


        if (munonmu.SelectedValue == "S")
        {   
            popupPanelAffil.Style.Add("display", "none");
            popupstudent.Style.Add("display", "true");
            popupstudent.Visible = true;
        }
        else
        {
            popupstudent.Style.Add("display", "none");
            //popupPanelAffil.Style.Add("display", "none");
            //popupstudent.Visible = false;
            //popupPanelAffil.Visible = true;
            //popupPanelAffil.Style.Add("display", "none");
        }
        popup.Visible = true;
        ModalPopupExtender ModalPopupExtender8 = (ModalPopupExtender)Grid_AuthorEntry.Rows[rowIndex].FindControl("ModalPopupExtender2");
        ModalPopupExtender8.Show();
       

        
    }


    protected void JournalCodePopChanged(object sender, EventArgs e)
    {
        string year = DropDownListYearJA.SelectedValue;
        SqlDataSourceJournal.SelectParameters.Clear();
        if (journalcodeSrch.Text.Trim() == "" && journalcodeISSNSrch.Text.Trim() == "")
        {
           
            SqlDataSourceJournal.SelectParameters.Add("Year", year);


            SqlDataSourceJournal.SelectCommand = "SELECT top 10 j.Id,j.Title,j.AbbreviatedTitle FROM [Journal_M] j,[Journal_Year_Map] m where j.Id=m.Id and m.Year=@Year";
            popGridJournal.DataBind();
            popGridJournal.Visible = true;
        }
        else if (journalcodeSrch.Text.Trim() != "" && journalcodeISSNSrch.Text.Trim() == "")
        {
           
            SqlDataSourceJournal.SelectParameters.Add("Year", year);
            SqlDataSourceJournal.SelectParameters.Add("Title", journalcodeSrch.Text);

            SqlDataSourceJournal.SelectCommand = "SELECT top 10 j.Id,j.Title,j.AbbreviatedTitle FROM [Journal_M] j,[Journal_Year_Map] m where j.Id=m.Id and m.Year=@Year and Title like '%' + @Title + '%'  ";
            popGridJournal.DataBind();
            popGridJournal.Visible = true;
        }
        else if (journalcodeSrch.Text.Trim() == "" && journalcodeISSNSrch.Text.Trim() != "")
        {
            
            SqlDataSourceJournal.SelectParameters.Add("Year", year);
            SqlDataSourceJournal.SelectParameters.Add("Id", journalcodeISSNSrch.Text);

            SqlDataSourceJournal.SelectCommand = "SELECT top 10 j.Id,j.Title,j.AbbreviatedTitle FROM [Journal_M] j,[Journal_Year_Map] m where j.Id=m.Id and m.Year=@Year and  j.Id like '%' + @Id + '%' ";
            popGridJournal.DataBind();
            popGridJournal.Visible = true;
        }
        else
        {
          
            SqlDataSourceJournal.SelectParameters.Add("Title", journalcodeSrch.Text);
            SqlDataSourceJournal.SelectParameters.Add("Id", journalcodeISSNSrch.Text);
            SqlDataSourceJournal.SelectCommand = "SELECT  Id,Title,AbbreviatedTitle FROM [Journal_M] where Title like '%' + @Title + '%' and Id like '%' + @Id + '%' ";
            popGridJournal.DataBind();
            popGridJournal.Visible = true;
        }

        ModalPopupExtender1.Show();
    }

    private void EnableValidation()
    {
        RequiredFieldValidator24.Enabled = true;
        if (DropDownListMuCategory.SelectedValue != "LE")
        {

            if (DropDownListPublicationEntry.SelectedValue == "JA" || DropDownListPublicationEntry.SelectedValue == "TS")
            {
                RequiredFieldValidator5.Enabled = true;
                RequiredFieldValidator6.Enabled = true;
                //RequiredFieldValidator7.Enabled = true;
            }
            else
            {
                RequiredFieldValidator5.Enabled = false;
                RequiredFieldValidator6.Enabled = false;
                //RequiredFieldValidator7.Enabled = false;
            }
        }
        else
        {
            RequiredFieldValidator5.Enabled = false;
            RequiredFieldValidator6.Enabled = false;
            //RequiredFieldValidator7.Enabled = false;
        }
        if (DropDownListPublicationEntry.SelectedValue == "JA" || DropDownListPublicationEntry.SelectedValue == "TS")
        {
            if (RadioButtonListIndexed.SelectedValue == "Y")
            {
                CustomValidator1.Enabled = true;
            }
            else
            {
                CustomValidator1.Enabled = false;
            }
        }
        else
        {
            CustomValidator1.Enabled = false;
        }
        
    }
}