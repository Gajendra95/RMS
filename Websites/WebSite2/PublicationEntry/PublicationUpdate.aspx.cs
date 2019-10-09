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
public partial class PublicationEntry_PublicationUpdate : System.Web.UI.Page
{
    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    string PublicationYearwebConfig = ConfigurationManager.AppSettings["PublicationYear"];
    string FolderPathServerWrite = ConfigurationManager.AppSettings["FolderPath"].ToString();

    Business B = new Business();
    Journal_DataObject JournalDataObj = new Journal_DataObject();
    JournalData JournalValueObj = new JournalData();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
        }
    }
    protected void ButtonSearchPubOnClick(object sender, EventArgs e)
    {
        addclik(sender, e);
        GridViewSearch.Visible = true;
        GridViewSearch.EditIndex = -1;
        dataBind();
    }
    protected void addclik(object sender, EventArgs e)
    {
        panel.Visible = false;
        panelRemarks.Visible = false;
        DropDownListPublicationEntry.ClearSelection();
        DropDownListMuCategory.ClearSelection();
        TextBoxPubId.Text = "";
        txtboxTitleOfWrkItem.Text = "";
        TextBoxRemarks.Text = "";

        Grid_AuthorEntry.DataSource = null;
        Grid_AuthorEntry.DataBind();
        Grid_AuthorEntry.Visible = false;

        panelJournalArticle.Visible = false;

        TextBoxPubJournal.Text = "";

        TextBoxNameJournal.Text = "";
        TextBoxIssue.Text = "";

        DropDownListMonthJA.ClearSelection();

        TextBoxYearJA.ClearSelection();

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
        panelTechReport.Visible = false;

        //TextBoxURL.Text = "";
        TextBoxDOINum.Text = "";
        TextBoxAbstract.Text = "";
        DropDownListErf.ClearSelection();
        TextBoxKeywords.Text = "";
        DropDownListuploadEPrint.ClearSelection();
        TextBoxEprintURL.Text = "";
        GVViewFile.Visible = false;;

    }

    private void dataBind()
    {

        GridViewSearch.Visible = true;

        string pubtype = EntryTypesearch.SelectedValue;
        SqlDataSource1.SelectParameters.Clear(); 
        if (PubIDSearch.Text == "")
        {
            SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,t.EntryName,PubJournalID,PublishJAMonth,TitleWorkItem,c.PubCatName,PublishJAYear,ImpactFactor ,s.StatusName from Publication p,Status_Publication_M s , PubMUCategorization_M c ,PublicationTypeEntry_M t where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization and  (TypeOfEntry='JA' or TypeOfEntry='TS')  and (Status='APP')";

        }
        else
        {
            SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,t.EntryName,PubJournalID,PublishJAMonth,TitleWorkItem,c.PubCatName,PublishJAYear,ImpactFactor ,s.StatusName from Publication p,Status_Publication_M s , PubMUCategorization_M c ,PublicationTypeEntry_M t where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization and  (TypeOfEntry=@EntryTypesearch or @EntryTypesearch='A') and (Status='APP') and PublicationID like '%' + @PubIDSearch + '%'";

            SqlDataSource1.SelectParameters.Add("PubIDSearch", PubIDSearch.Text.Trim());
            SqlDataSource1.SelectParameters.Add("EntryTypesearch", EntryTypesearch.SelectedValue);
        }
        GridViewSearch.DataBind();
        SqlDataSource1.DataBind();
        GridViewSearch.Visible = true;
    }

    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        ImageButton EditButton = (ImageButton)e.Row.FindControl("BtnEdit");
    }

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

    public void edit(Object sender, GridViewEditEventArgs e)
    {
        GridViewSearch.EditIndex = e.NewEditIndex;

        fnRecordExist(sender, e);

    }

    protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //Find the DropDownList in the Row
            DropDownList DropdownMuNonMu = (e.Row.FindControl("DropdownMuNonMu") as DropDownList);

            if (DropDownListPublicationEntry.SelectedValue == "BK" || DropDownListPublicationEntry.SelectedValue == "NM")
            {


                SqlDataSourceAuthorType.SelectCommand = "SELECT Id,Type FROM [Author_Type_M] where (Id! = 'O') and (Id! = 'E')";

                DropdownMuNonMu.DataTextField = "Type";
                DropdownMuNonMu.DataValueField = "Id";
                DropdownMuNonMu.DataBind();


            }
            else if (DropDownListPublicationEntry.SelectedValue == "JA" || DropDownListPublicationEntry.SelectedValue == "TS" || DropDownListPublicationEntry.SelectedValue == "CP")
            {
                SqlDataSourceAuthorType.SelectCommand = "SELECT Id,DisplayName FROM [Author_Type_M]";

                DropdownMuNonMu.DataTextField = "DisplayName";
                DropdownMuNonMu.DataValueField = "Id";
             
                DropdownMuNonMu.DataBind();

            }
            else
            {
            }

        }

    }


    protected void addRow(object sender, EventArgs e)
    {
              //PubEntriesUpdatePanel.Update();
       // EditUpdatePanel.Update();
        popupPanelAffilUpdate.Update();
        //MainUpdate.Update();

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
                        // TextBox Author = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("Author");
                        TextBox AuthorName = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[1].FindControl("AuthorName");
                        ImageButton EmployeeCodeBtnImg = (ImageButton)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("EmployeeCodeBtn");
                        DropDownList DropdownMuNonMu = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[2].FindControl("DropdownMuNonMu");
                        TextBox EmployeeCode = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("EmployeeCode");
                        HiddenField Institution = (HiddenField)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("Institution");
                        TextBox InstitutionName = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[6].FindControl("InstitutionName");
                        HiddenField Department = (HiddenField)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("Department");
                        TextBox DepartmentName = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("DepartmentName");

                        DropDownList DropdownStudentInstitutionName = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("DropdownStudentInstitutionName");
                        DropDownList DropdownStudentDepartmentName = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("DropdownStudentDepartmentName");

                        TextBox MailId = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("MailId");
                        DropDownList isCorrAuth = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("isCorrAuth");
                        DropDownList AuthorType = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("AuthorType");

                        TextBox NameInJournal = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("NameInJournal");

                        DropDownList IsPresenter = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("IsPresenter");

                        CheckBox HasAttented = (CheckBox)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("HasAttented");

                        DropDownList NationalType = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("NationalType");
                        DropDownList ContinentId = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("ContinentId");
                        ImageButton ImageButton1 = (ImageButton)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("ImageButton1");

                        //HiddenField PublicationLine = (HiddenField)Grid_AuthorEntry.Rows[rowIndex].Cells[2].FindControl("PublicationLine");

                

                      
                        drCurrentRow = dtCurrentTable.NewRow();

                        //   dtCurrentTable.Rows[i - 1]["amount"] = amount.Text.Trim() == "" ? 0 : Convert.ToDecimal(amount.Text);
                       // dtCurrentTable.Rows[i - 1]["PublicationLine"] = PublicationLine.Value;
                        dtCurrentTable.Rows[i - 1]["DropdownMuNonMu"] = DropdownMuNonMu.Text;
                        // dtCurrentTable.Rows[i - 1]["Author"] = Author.Text;
                        dtCurrentTable.Rows[i - 1]["AuthorName"] = AuthorName.Text;
                        dtCurrentTable.Rows[i - 1]["EmployeeCode"] = EmployeeCode.Text;

                        if (DropdownMuNonMu.Text == "M")
                        {

                            DropdownStudentInstitutionName.Visible = false;
                            DropdownStudentDepartmentName.Visible = false;
                            InstitutionName.Visible = true;
                            DepartmentName.Visible = true;

                            EmployeeCodeBtnImg.Enabled = true;
                            EmployeeCode.Enabled = false;
                            NationalType.Visible = false;
                            ContinentId.Visible = false;

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
                            EmployeeCode.Enabled = false;
                            EmployeeCodeBtnImg.Enabled = false;

                            NationalType.Visible = true;

                            if (NationalType.SelectedValue == "I")
                            {
                                ContinentId.Visible = true;
                            }
                            else
                            {
                                ContinentId.Visible = false;
                            }


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
                            EmployeeCode.Enabled = false;
                            EmployeeCodeBtnImg.Enabled = false;

                            NationalType.Visible = true;

                            if (NationalType.SelectedValue == "I")
                            {
                                ContinentId.Visible = true;
                            }
                            else
                            {
                                ContinentId.Visible = false;
                            }


                            dtCurrentTable.Rows[i - 1]["NationalType"] = NationalType.SelectedValue;
                            dtCurrentTable.Rows[i - 1]["ContinentId"] = ContinentId.SelectedValue;

                            dtCurrentTable.Rows[i - 1]["Institution"] = Institution.Value;
                            dtCurrentTable.Rows[i - 1]["InstitutionName"] = InstitutionName.Text;
                            dtCurrentTable.Rows[i - 1]["Department"] = Department.Value;
                            dtCurrentTable.Rows[i - 1]["DepartmentName"] = DepartmentName.Text;
                        }
                        else if (DropdownMuNonMu.Text == "S")
                        {
                            if (DropDownListPublicationEntry.SelectedValue == "JA" || DropDownListPublicationEntry.SelectedValue == "TS")
                            {
                                DropdownStudentInstitutionName.Visible = false;
                                DropdownStudentDepartmentName.Visible = false;
                                EmployeeCodeBtnImg.Enabled = false;
                                EmployeeCodeBtnImg.Visible = false;
                                EmployeeCode.Enabled = false;
                                InstitutionName.Visible = true;
                                DepartmentName.Visible = true;
                                dtCurrentTable.Rows[i - 1]["Institution"] = Institution.Value;
                                dtCurrentTable.Rows[i - 1]["InstitutionName"] = InstitutionName.Text;
                                dtCurrentTable.Rows[i - 1]["Department"] = Department.Value;
                                dtCurrentTable.Rows[i - 1]["DepartmentName"] = DepartmentName.Text;
                                ImageButton1.Visible = true;
                                NationalType.Visible = false;
                                ContinentId.Visible = false;
                            }
                            else
                            {
                                DropdownStudentInstitutionName.Visible = true;
                                DropdownStudentDepartmentName.Visible = true;
                                InstitutionName.Visible = false;
                                DepartmentName.Visible = false;
                                EmployeeCode.Enabled = false;
                                dtCurrentTable.Rows[i - 1]["Institution"] = DropdownStudentInstitutionName.SelectedValue;
                                dtCurrentTable.Rows[i - 1]["InstitutionName"] = DropdownStudentInstitutionName.SelectedItem;
                                dtCurrentTable.Rows[i - 1]["Department"] = DropdownStudentDepartmentName.SelectedValue;
                                dtCurrentTable.Rows[i - 1]["DepartmentName"] = DropdownStudentDepartmentName.SelectedItem;
                                EmployeeCodeBtnImg.Enabled = false;
                                EmployeeCodeBtnImg.Visible = true;
                                ImageButton1.Visible = false;
                                NationalType.Visible = false;
                                ContinentId.Visible = false;

                            }

                            NationalType.Visible = false;
                            ContinentId.Visible = false;

                            dtCurrentTable.Rows[i - 1]["NationalType"] = NationalType.SelectedValue;
                            dtCurrentTable.Rows[i - 1]["ContinentId"] = ContinentId.SelectedValue;
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

                        rowIndex++;
                    }

                    dtCurrentTable.Rows.Add(drCurrentRow);
                    //  var newRow = dtCurrentTable.NewRow();
                    // dtCurrentTable.Rows.InsertAt(newRow, 0);

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
        popupPanelAffil.Visible = true;
        popGridAffil.DataSourceID = "SqlDataSourceAffil";
        SqlDataSourceAffil.DataBind();
        popGridAffil.DataBind();
        //popupstudent.Visible = true;
        popupStudentGrid.DataSourceID = "StudentSQLDS";
        StudentSQLDS.DataBind();
        popupStudentGrid.DataBind();
    }


    private void SetInitialRow()
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
        dt.Columns.Add(new DataColumn("isCorrAuth", typeof(string)));
        dt.Columns.Add(new DataColumn("AuthorType", typeof(string)));
        dt.Columns.Add(new DataColumn("NameInJournal", typeof(string)));

        dt.Columns.Add(new DataColumn("IsPresenter", typeof(string)));
        dt.Columns.Add(new DataColumn("HasAttented", typeof(string)));
        dt.Columns.Add(new DataColumn("NationalType", typeof(string)));
        dt.Columns.Add(new DataColumn("ContinentId", typeof(string)));

        dt.Columns.Add(new DataColumn("DropdownStudentInstitutionName", typeof(string)));
        dt.Columns.Add(new DataColumn("DropdownStudentDepartmentName", typeof(string)));
      //  dt.Columns.Add(new DataColumn("PublicationLine", typeof(string)));

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
        dr["isCorrAuth"] = string.Empty;
        dr["AuthorType"] = string.Empty;
        dr["NameInJournal"] = string.Empty;

        dr["IsPresenter"] = string.Empty;
        dr["HasAttented"] = string.Empty;
        dr["NationalType"] = string.Empty;
        dr["ContinentId"] = string.Empty;
        dr["DropdownStudentInstitutionName"] = string.Empty;
        dr["DropdownStudentDepartmentName"] = string.Empty;
        //dr["PublicationLine"] = string.Empty;

        dt.Rows.Add(dr);

        ViewState["CurrentTable"] = dt;
        Grid_AuthorEntry.DataSource = dt;
        Grid_AuthorEntry.DataBind();


        //  TextBox Author = (TextBox)Grid_AuthorEntry.Rows[0].Cells[0].FindControl("Author");
        TextBox AuthorName = (TextBox)Grid_AuthorEntry.Rows[0].Cells[1].FindControl("AuthorName");
        ImageButton EmployeeCodeBtn = (ImageButton)Grid_AuthorEntry.Rows[0].Cells[1].FindControl("EmployeeCodeBtn");

        DropDownList DropdownMuNonMu = (DropDownList)Grid_AuthorEntry.Rows[0].Cells[2].FindControl("DropdownMuNonMu");

        TextBox EmployeeCode = (TextBox)Grid_AuthorEntry.Rows[0].Cells[1].FindControl("EmployeeCode");

        HiddenField Institution = (HiddenField)Grid_AuthorEntry.Rows[0].Cells[0].FindControl("Institution");
        TextBox InstitutionName = (TextBox)Grid_AuthorEntry.Rows[0].Cells[6].FindControl("InstitutionName");
        HiddenField Department = (HiddenField)Grid_AuthorEntry.Rows[0].Cells[0].FindControl("Department");
        TextBox DepartmentName = (TextBox)Grid_AuthorEntry.Rows[0].Cells[0].FindControl("DepartmentName");
        TextBox MailId = (TextBox)Grid_AuthorEntry.Rows[0].Cells[0].FindControl("MailId");

        DropDownList isCorrAuth = (DropDownList)Grid_AuthorEntry.Rows[0].Cells[0].FindControl("isCorrAuth");
        DropDownList AuthorType = (DropDownList)Grid_AuthorEntry.Rows[0].Cells[0].FindControl("AuthorType");

        TextBox NameInJournal = (TextBox)Grid_AuthorEntry.Rows[0].Cells[0].FindControl("NameInJournal");
        DropDownList NationalType = (DropDownList)Grid_AuthorEntry.Rows[0].Cells[0].FindControl("NationalType");
        DropDownList ContinentId = (DropDownList)Grid_AuthorEntry.Rows[0].Cells[0].FindControl("ContinentId");
        //HiddenField PublicationLine = (HiddenField)Grid_AuthorEntry.Rows[0].Cells[0].FindControl("PublicationLine");
        NationalType.Visible = false;
        ContinentId.Visible = false;
        EmployeeCode.Enabled = false;
        DropdownMuNonMu.Enabled = true;
        AuthorName.Enabled = false;
        EmployeeCodeBtn.Enabled = false;

        if (DropDownListPublicationEntry.SelectedValue == "BK" || DropDownListPublicationEntry.SelectedValue == "CP" || DropDownListPublicationEntry.SelectedValue == "NM")
        {


            SqlDataSourceAuthorType.SelectCommand = "SELECT Id,Type FROM [Author_Type_M] where (Id! = 'O') and  (Id! = 'E')";

            DropdownMuNonMu.DataTextField = "Type";
            DropdownMuNonMu.DataValueField = "Id";
            DropdownMuNonMu.DataBind();

        }
        else if (DropDownListPublicationEntry.SelectedValue == "JA" || DropDownListPublicationEntry.SelectedValue == "TS")
        {
            SqlDataSourceAuthorType.SelectCommand = "SELECT Id,DisplayName FROM [Author_Type_M]";

            DropdownMuNonMu.DataTextField = "DisplayName";
            DropdownMuNonMu.DataValueField = "Id";
            DropdownMuNonMu.DataBind();

        }

        if (DropDownListPublicationEntry.SelectedValue == "JA" || DropDownListPublicationEntry.SelectedValue == "TS")
        {

            DropdownMuNonMu.Enabled = true;
            if (DropdownMuNonMu.SelectedValue == "M")
            {
                EmployeeCodeBtn.Enabled = true;
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
            else if (DropdownMuNonMu.SelectedValue == "O")
            {
                EmployeeCodeBtn.Enabled = false;
            }
        }
        else
        {
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

            //string dir_domain = ConfigurationManager.AppSettings["DirectoryDomain"].ToString();
            // MailId.Text = Session["emailId"].ToString() + dir_domain;

            MailId.Text = Session["emailId"].ToString();
            MailId.Enabled = false;

            NameInJournal.Text = Session["UserName"].ToString();

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
            else if (DropdownMuNonMu.SelectedValue == "O")
            {
                EmployeeCodeBtn.Enabled = false;
            }

        }



        if (DropDownListPublicationEntry.SelectedValue == "JA" || DropDownListPublicationEntry.SelectedValue == "TS")
        {
            isCorrAuth.Visible = true;
            AuthorType.Visible = true;
            NameInJournal.Visible = true;
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
            NameInJournal.Visible = true;
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
            NameInJournal.Visible = false;
            Grid_AuthorEntry.Columns[6].Visible = false;
            Grid_AuthorEntry.Columns[7].Visible = false;
            Grid_AuthorEntry.Columns[8].Visible = false;
            Grid_AuthorEntry.Columns[9].Visible = false;
            Grid_AuthorEntry.Columns[10].Visible = false;
            Grid_AuthorEntry.Columns[1].Visible = false;
        }

        //Grid_AuthorEntry.Columns[13].Visible = false;


    }
    protected void branchNameChanged(object sender, EventArgs e)
    {
       // PubEntriesUpdatePanel.Update();
        //EditUpdatePanel.Update();
        popupPanelAffilUpdate.Update();
        //MainUpdate.Update();
        if (affiliateSrch.Text.Trim() == "" && SrchId.Text.Trim() == "")
        {
            SqlDataSourceAffil.SelectCommand = "SELECT top 10  User_Id,prefix+' '+firstname+' '+middlename+' '+lastname  as Name from User_M where Active='Y'";
            popGridAffil.DataBind();
            popGridAffil.Visible = true;
        }

        else
        {
            string name = affiliateSrch.Text.Replace(" ", String.Empty);
            SqlDataSourceAffil.SelectCommand = "SELECT  User_Id,prefix+' '+firstname+' '+middlename+' '+lastname  as Name from User_M where prefix+firstname+middlename+lastname like '%" + name + "%' and  User_Id like '%" + SrchId.Text + "%' and Active='Y'";
            popGridAffil.DataBind();
            popGridAffil.Visible = true;
        }


        string rowVal1 = Request.Form["rowIndx"];
        int rowIndex = Convert.ToInt32(rowVal1);
        int row = Convert.ToInt16(rowVal.Value);
        DropDownList munonmu = (DropDownList)Grid_AuthorEntry.Rows[row].FindControl("DropdownMuNonMu");
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
        else if (munonmu.SelectedValue == "O")
        {
            popupPanelAffil.Style.Add("display", "none");
            popupstudent.Style.Add("display", "none");
        }
        ModalPopupExtender ModalPopupExtender8 = (ModalPopupExtender)Grid_AuthorEntry.Rows[row].FindControl("ModalPopupExtender4");
        ModalPopupExtender8.Show();
    }
    protected void exit(object sender, EventArgs e)
    {
        popupPanelAffilUpdate.Update();
        affiliateSrch.Text = "";
        popGridAffil.DataBind();
    }



    protected void Grid_AuthorEntry_RowDeleting(Object sender, GridViewDeleteEventArgs e)
    {
        //PubEntriesUpdatePanel.Update();
        //EditUpdatePanel.Update();
        popupPanelAffilUpdate.Update();
        //MainUpdate.Update();
        SetRowData();
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            DataRow drCurrentRow = null;
            int rowIndex = Convert.ToInt32(e.RowIndex);
            if (DropDownListPublicationEntry.SelectedValue == "JA" || DropDownListPublicationEntry.SelectedValue == "TS")
            {
                if (dt.Rows.Count > 1)
                {
                    dt.Rows.Remove(dt.Rows[rowIndex]);
                    drCurrentRow = dt.NewRow();
                    ViewState["CurrentTable"] = dt;

                    Grid_AuthorEntry.DataSource = dt;
                    Grid_AuthorEntry.DataBind();

                    SetPreviousData();
                    // gridAmtChanged(sender, e);
                }
            }
            else
            {
                if (dt.Rows.Count > 1 && rowIndex != 0)
                {
                    dt.Rows.Remove(dt.Rows[rowIndex]);
                    drCurrentRow = dt.NewRow();
                    ViewState["CurrentTable"] = dt;
                    Grid_AuthorEntry.DataSource = dt;
                    Grid_AuthorEntry.DataBind();

                    SetPreviousData();
                    // gridAmtChanged(sender, e);
                }
            }
        }
    }

    protected void NationalTypeOnSelectedIndexChanged(object sender, EventArgs e)
    {
        //PubEntriesUpdatePanel.Update();
        //EditUpdatePanel.Update();
        popupPanelAffilUpdate.Update();
       // MainUpdate.Update();
        TextBox senderBox = sender as TextBox;

        GridViewRow currentRow = (GridViewRow)((DropDownList)sender).Parent.Parent;
        DropDownList DropdownMuNonMu = (DropDownList)currentRow.FindControl("DropdownMuNonMu");
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


        // ContinentId.Focus();
    }

    protected void popSelected1(Object sender, EventArgs e)
    {


        popGridAffil.Visible = true;
        GridViewRow row = popGridAffil.SelectedRow;

        string EmployeeCode1 = row.Cells[1].Text;
        TextBox senderBox = sender as TextBox;

        // GridViewRow currentRow = (GridViewRow)((TextBox)sender).Parent.Parent;
        // TextBox accountTxt = (TextBox)currentRow.FindControl("EmployeeCode");
        //// TextBox affiliate = (TextBox)currentRow.FindControl("affiliate");
        // accountTxt.
        string rowVal1 = rowVal.Value;
        int rowIndex = Convert.ToInt32(rowVal1);
        //GridViewRow gr =null;
        //TextBox affiliate = (TextBox)Grid_AuthorEntry.Rows[gr.RowIndex].Cells[0].FindControl("EmployeeCode");
        TextBox EmployeeCode = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("AuthorName");
        EmployeeCode.Text = EmployeeCode1;

        affiliateSrch.Text = "";
        popGridAffil.DataBind();

        Business b = new Business();
        User a = new User();

        a = b.GetUserName(EmployeeCode1);

        // string InstituteId = Session["InstituteId"].ToString();


        string InstituteName1 = null;
        InstituteName1 = b.GetInstitutionName(a.InstituteId);


        //Institution.Enabled = false;
        // string deptId = Session["Department"].ToString();

        string deptName1 = null;
        deptName1 = b.GetDeptName(a.Department, a.InstituteId);


        TextBox InstitutionName = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("InstitutionName");
        InstitutionName.Text = InstituteName1;

        TextBox DepartmentName = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("DepartmentName");
        DepartmentName.Text = deptName1;

        // string dir_domain = ConfigurationManager.AppSettings["DirectoryDomain"].ToString();

        TextBox mailid = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("MailId");
        mailid.Text = a.emailId;

        HiddenField Institution = (HiddenField)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("Institution");
        Institution.Value = a.InstituteId;

        HiddenField Department = (HiddenField)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("Department");
        Department.Value = a.Department;

        TextBox employc = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("EmployeeCode");
        employc.Text = EmployeeCode1;

        TextBox AuthorName = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("AuthorName");
        //AuthorName.Text = a.UserName;
        AuthorName.Text = a.UserNamePrefix + " " + a.UserFirstName + " " + a.UserMiddleName + " " + a.UserLastName;

     

        TextBox NameInJournal = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("NameInJournal");
        NameInJournal.Text = a.UserNamePrefix + " " + a.UserFirstName + " " + a.UserMiddleName + " " + a.UserLastName;

        affiliateSrch.Text = "";
        popGridAffil.DataBind();


    }
    protected void IsPresenterIsPresenter(object sender, EventArgs e)
    {
        //PubEntriesUpdatePanel.Update();
        //EditUpdatePanel.Update();
        popupPanelAffilUpdate.Update();
        //MainUpdate.Update();
        DropDownList senderBox = sender as DropDownList;

        GridViewRow currentRow = (GridViewRow)((DropDownList)sender).Parent.Parent;
        DropDownList IsPresenter = (DropDownList)currentRow.FindControl("IsPresenter");
        string IsPresenter1 = IsPresenter.Text.Trim();
        CheckBox HasAttented = (CheckBox)currentRow.FindControl("HasAttented");
        if (IsPresenter1 == "Y")
        {
            HasAttented.Checked = true;
        }
        else
        {
            HasAttented.Checked = false;
        }
        DropDownList DropdownMuNonMu = (DropDownList)currentRow.FindControl("DropdownMuNonMu");
        if (DropdownMuNonMu.SelectedValue == "S" || DropdownMuNonMu.SelectedValue == "O")
        {
            //HasAttented.Focus();
        }
        else
        {
            DropDownList NationalType = (DropDownList)currentRow.FindControl("NationalType");
            //NationalType.Focus();
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

        TextBox NameInJournal = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("NameInJournal");
        NameInJournal.Text = studentname;

        TextBox employc = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("EmployeeCode");
        employc.Text = rollno;


        HiddenField classcode = (HiddenField)popupStudentGrid.SelectedRow.FindControl("lblClassCode");

        HiddenField instnid = (HiddenField)popupStudentGrid.SelectedRow.FindControl("lblInstn");

        HiddenField Institution = (HiddenField)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("Institution");
        Institution.Value = instnid.Value;

        HiddenField Department = (HiddenField)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("Department");
        HiddenField PublicationLine = (HiddenField)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("PublicationLine");
        Department.Value = classcode.Value;
        popupStudentGrid.DataBind();

    }
    protected void SearchStudentData(object sender, EventArgs e)
    {
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

             StudentSQLDS.SelectCommand = "Select TOP 10  RollNo,Name,SISClass.ClassName as ClassName,SISInstitution.InstName as InstName,EmailID1 ,SISStudentGenInfo.ClassCode as ClassCode,SISInstnHR.HRInstitute as InstID from SISStudentGenInfo,SISClass,SISInstitution,SISInstnHR  where SISStudentGenInfo.ClassCode=SISClass.ClassCode and SISStudentGenInfo.InstID=SISInstitution.InstID and SISInstnHR.Institute_Id=SISInstitution.InstID and SISInstnHR.Institute_Id=SISStudentGenInfo.InstID  and RollNo like '%' + @txtSrchStudentRollNo+ '%' and (SISStudentGenInfo.InstID=@StudentIntddl)";

            popupStudentGrid.DataBind();
            popupStudentGrid.Visible = true;
        }
        else if ((txtSrchStudentName.Text.Trim() != "" && txtSrchStudentRollNo.Text.Trim() == "") && StudentIntddl.SelectedValue != "")
        {

            StudentSQLDS.SelectParameters.Add("txtSrchStudentName", txtSrchStudentName.Text.Trim());
            StudentSQLDS.SelectParameters.Add("StudentIntddl", StudentIntddl.SelectedValue);

              StudentSQLDS.SelectCommand = "Select TOP 10  RollNo,Name,SISClass.ClassName as ClassName,SISInstitution.InstName as InstName,EmailID1 ,SISStudentGenInfo.ClassCode as ClassCode,SISInstnHR.HRInstitute as InstID from SISStudentGenInfo,SISClass,SISInstitution,SISInstnHR  where SISStudentGenInfo.ClassCode=SISClass.ClassCode and SISStudentGenInfo.InstID=SISInstitution.InstID and SISInstnHR.Institute_Id=SISInstitution.InstID and SISInstnHR.Institute_Id=SISStudentGenInfo.InstID  and Name like '%' + @txtSrchStudentName + '%' and (SISStudentGenInfo.InstID=@StudentIntddl)";

            popupStudentGrid.DataBind();
            popupStudentGrid.Visible = true;
        }


            //ends

        else if ((txtSrchStudentName.Text.Trim() != "" || txtSrchStudentRollNo.Text.Trim() != "") && StudentIntddl.SelectedValue == "")
        {
            StudentSQLDS.SelectParameters.Add("txtSrchStudentName", txtSrchStudentName.Text.Trim());
            StudentSQLDS.SelectParameters.Add("txtSrchStudentRollNo", txtSrchStudentRollNo.Text.Trim());

             StudentSQLDS.SelectCommand = "Select TOP 10  RollNo,Name,SISClass.ClassName as ClassName,SISInstitution.InstName as InstName,EmailID1 ,SISStudentGenInfo.ClassCode as ClassCode,SISInstnHR.HRInstitute as InstID from SISStudentGenInfo,SISClass,SISInstitution,SISInstnHR  where SISStudentGenInfo.ClassCode=SISClass.ClassCode and SISStudentGenInfo.InstID=SISInstitution.InstID and SISInstnHR.Institute_Id=SISInstitution.InstID and SISInstnHR.Institute_Id=SISStudentGenInfo.InstID and  Name like '%' + @txtSrchStudentName + '%' and RollNo like '%' + @txtSrchStudentRollNo+ '%'";

            popupStudentGrid.DataBind();
            popupStudentGrid.Visible = true;
        }


        else
        {
            StudentSQLDS.SelectParameters.Add("txtSrchStudentName", txtSrchStudentName.Text);
            StudentSQLDS.SelectParameters.Add("txtSrchStudentRollNo", txtSrchStudentRollNo.Text);
            StudentSQLDS.SelectParameters.Add("StudentIntddl", StudentIntddl.SelectedValue);
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
            popupPanelAffil.Style.Add("display", "true");
            popupstudent.Style.Add("display", "none");
        }
        else if (munonmu.SelectedValue == "S")
        {
            popupstudent.Visible = true;
            popupstudent.Style.Add("display", "true");
            popupPanelAffil.Style.Add("display", "none");
        }
        ModalPopupExtender ModalPopupExtender8 = (ModalPopupExtender)Grid_AuthorEntry.Rows[rowIndex].FindControl("ModalPopupExtender2");
        ModalPopupExtender8.Show();
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
                        else if (DropdownMuNonMu.Text == "E")
                        {
                            EmployeeCodeBtnImg1.Enabled = false;
                        }
                        else if (DropdownMuNonMu.Text == "S")
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
                        DropdownMuNonMu.Enabled = true;
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




    protected void DropdownMuNonMuOnSelectedIndexChanged(object sender, EventArgs e)
    {
       
        popupPanelAffilUpdate.Update();
      
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
        ImageButton ImageButton1 = (ImageButton)currentRow.FindControl("ImageButton1");
        TextBox EmployeeCode = (TextBox)currentRow.FindControl("EmployeeCode");
        TextBox NameInJournal = (TextBox)currentRow.FindControl("NameInJournal");
        if (DropdownMuNonMu.SelectedValue == "M")
        {
            DropdownStudentInstitutionName1.Visible = false;
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
            NameInJournal.Text = "";
            NameInJournal.Text = "";

        }
        else if (DropdownMuNonMu.SelectedValue == "N")
        {

            DropdownStudentInstitutionName1.Visible = false;
            DropdownStudentDepartmentName.Visible = false;
            InstitutionName.Visible = true;
            DepartmentName.Visible = true;
            NationalType.Visible = true;

            EmployeeCode.Enabled = false;
            EmployeeCodeBtn.Enabled = false;
            InstitutionName.Enabled = true;
            DepartmentName.Enabled = true;
            AuthorName.Enabled = true;
            MailId.Enabled = true;
            AuthorName.Text = "";
            EmployeeCode.Text = "";
            MailId.Text = "";
            InstitutionName.Text = "";
            DepartmentName.Text = "";
            ImageButton1.Visible = false;
            EmployeeCodeBtn.Visible = true;
        }
        else if (DropdownMuNonMu.SelectedValue == "E")
        {

            DropdownStudentInstitutionName1.Visible = false;
            DropdownStudentDepartmentName.Visible = false;
            InstitutionName.Visible = true;
            DepartmentName.Visible = true;
            NationalType.Visible = true;

            EmployeeCode.Enabled = false;
            EmployeeCodeBtn.Enabled = false;
            InstitutionName.Enabled = true;
            DepartmentName.Enabled = true;
            AuthorName.Enabled = true;
            MailId.Enabled = true;
            AuthorName.Text = "";
            EmployeeCode.Text = "";
            MailId.Text = "";
            InstitutionName.Text = "";
            DepartmentName.Text = "";
            ImageButton1.Visible = false;
            EmployeeCodeBtn.Visible = true;
        }
        else if (DropdownMuNonMu.SelectedValue == "S")
        {
            if (DropDownListPublicationEntry.SelectedValue == "JA" || DropDownListPublicationEntry.SelectedValue == "TS")
            {
                DropdownStudentInstitutionName1.Visible = false;
                DropdownStudentDepartmentName.Visible = false;
                InstitutionName.Visible = true;
                DepartmentName.Visible = true;
                EmployeeCode.Visible = true;
                EmployeeCode.Enabled = false;
                EmployeeCodeBtn.Enabled = false;
                EmployeeCodeBtn.Visible = false;
                ImageButton1.Visible = true;
                AuthorName.Enabled = false;
                InstitutionName.Enabled = false;
                DepartmentName.Enabled = false;

                NationalType.Visible = false;
                ContinentId.Visible = false;
            }
            else
            {
                DropdownStudentInstitutionName1.Visible = true;
                DropdownStudentDepartmentName.Visible = true;
                InstitutionName.Visible = false;
                EmployeeCode.Enabled = false;
                DepartmentName.Visible = false;
                EmployeeCodeBtn.Enabled = false;
                EmployeeCodeBtn.Visible = true;
                ImageButton1.Visible = false;
                AuthorName.Enabled = true;

                NationalType.Visible = false;
                ContinentId.Visible = false;
            }

            NationalType.Visible = false;
            MailId.Enabled = true;
            AuthorName.Text = "";
            MailId.Text = "";
            InstitutionName.Text = "";
            DepartmentName.Text = "";
            EmployeeCode.Text = "";
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
        if (DropDownListPublicationEntry.SelectedValue == "JA" || DropDownListPublicationEntry.SelectedValue == "TS")
        {
            if (DropdownMuNonMu.SelectedValue == "S")
            {
                int rows = Grid_AuthorEntry.Rows.Count;
                if (rows == 1)
                {
                    popupPanelAffil.Visible = false;
                }
                else
                {
                    popupPanelAffil.Visible = true;
                }
                popGridAffil.DataSourceID = "SqlDataSourceAffil";
                SqlDataSourceAffil.DataBind();
                popGridAffil.DataBind();
                popupstudent.Visible = true;
                popupStudentGrid.DataSourceID = "StudentSQLDS";
                StudentSQLDS.DataBind();
                popupStudentGrid.DataBind();
            }
            else
            {
                popupPanelAffil.Visible = true;
                popGridAffil.DataSourceID = "SqlDataSourceAffil";
                SqlDataSourceAffil.DataBind();
                popGridAffil.DataBind();
                popupstudent.Visible = true;
                popupStudentGrid.DataSourceID = "StudentSQLDS";
                StudentSQLDS.DataBind();
                popupStudentGrid.DataBind();
            }

        }
        else
        {
            popupPanelAffil.Visible = true;
            popGridAffil.DataSourceID = "SqlDataSourceAffil";
            SqlDataSourceAffil.DataBind();
            popGridAffil.DataBind();
            popupstudent.Visible = false;
            popupStudentGrid.DataSourceID = "StudentSQLDS";
            StudentSQLDS.DataBind();
            popupStudentGrid.DataBind();
        }
      
    }
    protected void setModalWindow(object sender, EventArgs e)
    {
        //if (RadioButtonListIndexed.SelectedValue == "Y")
        //{
        //    popupPanelJournal.Visible = true;
        //    popGridJournal.DataSourceID = "SqlDataSourceJournal";
        //    SqlDataSourceJournal.DataBind();
        //    popGridJournal.DataBind();
        //}
        //else
        //{
        //    popupPanelJournal.Visible = false;
        //    popGridJournal.DataSourceID = "SqlDataSourceJournal";
        //    SqlDataSourceJournal.DataBind();
        //    popGridJournal.DataBind();
        //}
        popup.Visible = true;
        //popupPanelAffil.Visible = true;
        popGridAffil.DataSourceID = "SqlDataSourceAffil";
        SqlDataSourceAffil.DataBind();
        popGridAffil.DataBind();
        // popupstudent.Visible = false;
        popupStudentGrid.DataSourceID = "StudentSQLDS";
        StudentSQLDS.DataBind();
        popupStudentGrid.DataBind();

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
                    //  TextBox Author = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("Author");
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

                    DropDownList isCorrAuth = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("isCorrAuth");

                    DropDownList AuthorType = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("AuthorType");
                    TextBox NameInJournal = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("NameInJournal");

                    DropDownList DropdownStudentInstitutionName = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("DropdownStudentInstitutionName");
                    DropDownList DropdownStudentDepartmentName = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("DropdownStudentDepartmentName");


                    DropDownList NationalType = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("NationalType");
                    DropDownList ContinentId = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("ContinentId");


                    DropDownList IsPresenter = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[2].FindControl("IsPresenter");
                    CheckBox HasAttented = (CheckBox)Grid_AuthorEntry.Rows[rowIndex].Cells[2].FindControl("HasAttented");

                    //  TextBox Author1 = (TextBox)Grid_AuthorEntry.Rows[0].Cells[0].FindControl("Author");
                    TextBox AuthorName1 = (TextBox)Grid_AuthorEntry.Rows[0].Cells[1].FindControl("AuthorName");
                    DropDownList DropdownMuNonMu1 = (DropDownList)Grid_AuthorEntry.Rows[0].Cells[2].FindControl("DropdownMuNonMu");
                    ImageButton EmployeeCodeBtnImg1 = (ImageButton)Grid_AuthorEntry.Rows[0].Cells[0].FindControl("EmployeeCodeBtn");
               
                    //TextBox amount = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[3].FindControl("amount");
                    TextBox EmployeeCode1 = (TextBox)Grid_AuthorEntry.Rows[0].Cells[0].FindControl("EmployeeCode");
                   // HiddenField Institution1 = (HiddenField)Grid_AuthorEntry.Rows[0].Cells[0].FindControl("Institution");
                    TextBox InstitutionName1 = (TextBox)Grid_AuthorEntry.Rows[0].Cells[0].FindControl("InstitutionName");
                    HiddenField Department1 = (HiddenField)Grid_AuthorEntry.Rows[0].Cells[0].FindControl("Department");
                    TextBox DepartmentName1 = (TextBox)Grid_AuthorEntry.Rows[0].Cells[0].FindControl("DepartmentName");
                    TextBox MailId1 = (TextBox)Grid_AuthorEntry.Rows[0].Cells[0].FindControl("MailId");

                    DropDownList isCorrAuth1 = (DropDownList)Grid_AuthorEntry.Rows[0].Cells[0].FindControl("isCorrAuth");

                    DropDownList AuthorType1 = (DropDownList)Grid_AuthorEntry.Rows[0].Cells[0].FindControl("AuthorType");
                    TextBox NameInJournal1 = (TextBox)Grid_AuthorEntry.Rows[0].Cells[0].FindControl("NameInJournal");

                    DropDownList IsPresenter1 = (DropDownList)Grid_AuthorEntry.Rows[0].Cells[2].FindControl("IsPresenter");
                    CheckBox HasAttented1 = (CheckBox)Grid_AuthorEntry.Rows[0].Cells[2].FindControl("HasAttented");


                    DropDownList DropdownStudentInstitutionName1 = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("DropdownStudentInstitutionName");
                    DropDownList DropdownStudentDepartmentName1 = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("DropdownStudentDepartmentName");
                    ImageButton ImageButton1 = (ImageButton)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("ImageButton1");
                 

                    
                    if (DropDownListPublicationEntry.SelectedValue == "JA" || DropDownListPublicationEntry.SelectedValue == "TS")
                    {
                        if (DropdownMuNonMu1.SelectedValue == "N")
                        {
                            MailId1.Enabled = true;
                            AuthorName1.Enabled = true;
                            EmployeeCodeBtnImg1.Enabled = false;
                            DropdownMuNonMu1.Enabled = true;
                            EmployeeCode.Enabled = false;
                          
                            InstitutionName1.Enabled = true;

                            DepartmentName1.Enabled = true;
                        }
                        else if (DropdownMuNonMu1.SelectedValue == "E")
                        {
                            MailId1.Enabled = true;
                            AuthorName1.Enabled = true;
                            EmployeeCodeBtnImg1.Enabled = false;
                            DropdownMuNonMu1.Enabled = true;
                            EmployeeCode.Enabled = false;

                            InstitutionName1.Enabled = true;

                            DepartmentName1.Enabled = true;
                        }
                        else
                            if (DropdownMuNonMu1.SelectedValue == "O")
                            {
                                EmployeeCode.Visible = true;
                                EmployeeCode.Enabled = true;
                                MailId.Enabled = true;
                                AuthorName1.Enabled = false;
                                EmployeeCodeBtnImg1.Enabled = false;
                                EmployeeCodeBtnImg1.Visible = true;
                                NationalType.Visible = false;
                                ContinentId.Visible = false;
                                DropdownMuNonMu1.Enabled = true;
                                DropdownStudentInstitutionName1.Visible = true;
                                DropdownStudentDepartmentName1.Visible = true;
                                // EmployeeCode1.Enabled = false;
                                //  Institution1.Enabled = false;
                                InstitutionName1.Enabled = false;

                                //  Department1.Enabled = false;
                                DepartmentName1.Enabled = false;

                            }
                        else
                        {
                            if (DropdownMuNonMu1.SelectedValue == "S")
                            {
                                MailId1.Enabled = true;
                            }
                            else
                            {
                                MailId1.Enabled = false;
                            }
                            AuthorName1.Enabled = false;
                            EmployeeCodeBtnImg1.Enabled = false;
                            DropdownMuNonMu1.Enabled = true;
                            EmployeeCode.Enabled = false;
                            // EmployeeCode1.Enabled = false;
                            //  Institution1.Enabled = false;
                            InstitutionName1.Enabled = false;

                            //  Department1.Enabled = false;
                            DepartmentName1.Enabled = false;
                        }
                       
                    }
                    else
                    {
                        MailId1.Enabled = false;
                        AuthorName1.Enabled = false;
                        EmployeeCodeBtnImg1.Enabled = false;
                        DropdownMuNonMu1.Enabled = true;
                        EmployeeCode.Enabled = false;
                        // EmployeeCode1.Enabled = false;
                        //  Institution1.Enabled = false;
                        InstitutionName1.Enabled = false;

                        //  Department1.Enabled = false;
                        DepartmentName1.Enabled = false;
                    }
                    //  isCorrAuth1.Enabled = false;
                    // AuthorType1.Enabled = false;
                    // NameInJournal1.Enabled = false;

                    if (DropDownListPublicationEntry.SelectedValue == "JA" || DropDownListPublicationEntry.SelectedValue == "TS")
                    {
                        isCorrAuth.Visible = true;
                        AuthorType.Visible = true;
                        NameInJournal.Visible = true;
                        Grid_AuthorEntry.Columns[6].Visible = true;
                        Grid_AuthorEntry.Columns[7].Visible = true;
                        Grid_AuthorEntry.Columns[8].Visible = true;
                        Grid_AuthorEntry.Columns[9].Visible = false;
                        Grid_AuthorEntry.Columns[10].Visible = false;
                    }

                    else if (DropDownListPublicationEntry.SelectedValue == "CP")
                    {
                        isCorrAuth.Visible = true;
                        AuthorType.Visible = true;
                        NameInJournal.Visible = true;
                        Grid_AuthorEntry.Columns[6].Visible = false;
                        Grid_AuthorEntry.Columns[7].Visible = false;
                        Grid_AuthorEntry.Columns[8].Visible = false;
                        Grid_AuthorEntry.Columns[9].Visible = true;
                        Grid_AuthorEntry.Columns[10].Visible = true;

                    }
                    else
                    {
                        isCorrAuth.Visible = false;
                        AuthorType.Visible = false;
                        NameInJournal.Visible = false;
                        Grid_AuthorEntry.Columns[6].Visible = false;
                        Grid_AuthorEntry.Columns[7].Visible = false;
                        Grid_AuthorEntry.Columns[8].Visible = false;
                        Grid_AuthorEntry.Columns[9].Visible = false;
                        Grid_AuthorEntry.Columns[10].Visible = false;
                    }

                    //  amount.Text = dt.Rows[i]["amount"].ToString();
                    DropdownMuNonMu.Text = dt.Rows[i]["DropdownMuNonMu"].ToString();
                    //  Author.Text = dt.Rows[i]["Author"].ToString();
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

                        DropdownStudentInstitutionName.Visible = false;
                        DropdownStudentDepartmentName.Visible = false;
                        InstitutionName.Visible = true;
                        DepartmentName.Visible = true;

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

                        DropdownStudentInstitutionName.Visible = false;
                        DropdownStudentDepartmentName.Visible = false;
                        InstitutionName.Visible = true;
                        DepartmentName.Visible = true;

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

                        Institution.Value = dt.Rows[i]["Institution"].ToString();
                        InstitutionName.Text = dt.Rows[i]["InstitutionName"].ToString();
                        Department.Value = dt.Rows[i]["Department"].ToString();
                        DepartmentName.Text = dt.Rows[i]["DepartmentName"].ToString();
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

                    else if (DropdownMuNonMu.Text == "S")
                    {
                        if (DropDownListPublicationEntry.SelectedValue == "JA" || DropDownListPublicationEntry.SelectedValue == "TS")
                        {
                            AuthorName.Enabled = false;
                            DropdownStudentInstitutionName.Visible = false;
                            DropdownStudentDepartmentName.Visible = false;
                            InstitutionName.Enabled = false;
                            DepartmentName.Enabled = false;
                            EmployeeCodeBtnImg.Enabled = false;
                            EmployeeCodeBtnImg.Visible = false;
                            EmployeeCode.Enabled = false;
                            ImageButton1.Visible = true;
                            NationalType.Visible = false;
                            ContinentId.Visible = false;
                            Institution.Value = dt.Rows[i]["Institution"].ToString();
                            InstitutionName.Text = dt.Rows[i]["InstitutionName"].ToString();
                            Department.Value = dt.Rows[i]["Department"].ToString();
                            DepartmentName.Text = dt.Rows[i]["DepartmentName"].ToString();
                        }
                        else
                        {
                            EmployeeCode.Enabled = false;
                            AuthorName.Enabled = true;
                            DropdownStudentInstitutionName.Visible = true;
                            DropdownStudentDepartmentName.Visible = true;
                            EmployeeCodeBtnImg.Enabled = false;
                            InstitutionName.Visible = false;
                            DepartmentName.Visible = false;
                            NationalType.Visible = false;
                            ContinentId.Visible = false;
                            Institution.Value = dt.Rows[i]["Institution"].ToString();
                            DropdownStudentInstitutionName.SelectedValue = dt.Rows[i]["Institution"].ToString();
                            Department.Value = dt.Rows[i]["Department"].ToString();
                            DropdownStudentDepartmentName.SelectedValue = dt.Rows[i]["Department"].ToString();
                        }
                        MailId.Enabled = true;
                        NationalType.Visible = false;
                        ContinentId.Visible = false;
                        EmployeeCode.Enabled = false;
                        NationalType.Text = dt.Rows[i]["NationalType"].ToString();
                        ContinentId.Text = dt.Rows[i]["ContinentId"].ToString();

                      ////  Institution.Value = dt.Rows[i]["Institution"].ToString();
                      //  DropdownStudentInstitutionName.SelectedValue = dt.Rows[i]["Institution"].ToString();
                      ////  Department.Value = dt.Rows[i]["Department"].ToString();
                      //  DropdownStudentDepartmentName.SelectedValue = dt.Rows[i]["Department"].ToString();
                    }


                    MailId.Text = dt.Rows[i]["MailId"].ToString();
                    isCorrAuth.Text = dt.Rows[i]["isCorrAuth"].ToString();
                    AuthorType.Text = dt.Rows[i]["AuthorType"].ToString();
                    NameInJournal.Text = dt.Rows[i]["NameInJournal"].ToString();

                    IsPresenter.Text = dt.Rows[i]["IsPresenter"].ToString();
                    if (dt.Rows[i]["HasAttented"].ToString()=="Y")
                    {
                        HasAttented.Checked=true;
                    }
                    else
                    {
                        HasAttented.Checked = false;
                    }

                    if (rowIndex == 0)
                    {
                         Grid_AuthorEntry.Columns[15].Visible = false;
                        //Grid_AuthorEntry.Rows[rowIndex].Cells[11].Visible = false;
                    }
                    else
                    {
                        Grid_AuthorEntry.Columns[15].Visible = true;
                        //Grid_AuthorEntry.Rows[rowIndex].Cells[11].Visible = true;
                    }

                    if (DropDownListPublicationEntry.SelectedValue == "JA" || DropDownListPublicationEntry.SelectedValue == "TS")
                    {
                        if (DropdownMuNonMu1.Text == "M")
                        {
                            EmployeeCodeBtnImg1.Enabled = true;
                        }
                        else if (DropdownMuNonMu1.Text == "N")
                        {
                            EmployeeCodeBtnImg1.Enabled =false;
                        }
                        else if (DropdownMuNonMu1.Text == "S")
                        {
                            EmployeeCodeBtnImg1.Enabled = true;
                        }
                        else if (DropdownMuNonMu1.Text == "E")
                        {
                            EmployeeCodeBtnImg1.Enabled = true;
                        }
                        else if (DropdownMuNonMu1.Text == "O")
                        {
                            EmployeeCodeBtnImg1.Enabled = false;
                        }
                       
                        DropdownMuNonMu1.Enabled = true;
                    }
                    else
                    {
                        DropdownMuNonMu1.Enabled = true;
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
                            DropdownMuNonMu5.Enabled = true;
                        }
                    }
                    if (i != 0)
                    {
                        DropDownList DropdownMuNonM8 = (DropDownList)Grid_AuthorEntry.Rows[rowIndex - 1].Cells[2].FindControl("DropdownMuNonMu");
                        DropdownMuNonM8.Enabled = true;
                    }

                    rowIndex++;
                }
            }
        }
    }

    
    protected void AuthorName_Changed(object sender, EventArgs e)
    {
        //PubEntriesUpdatePanel.Update();
        //EditUpdatePanel.Update();
        popupPanelAffilUpdate.Update();
       // MainUpdate.Update();
        GridViewRow currentRow = (GridViewRow)((TextBox)sender).Parent.Parent;
        TextBox AuthorName = (TextBox)currentRow.FindControl("AuthorName");
        TextBox NameInJournal = (TextBox)currentRow.FindControl("NameInJournal");
        TextBox InstitutionName = (TextBox)currentRow.FindControl("InstitutionName");

        //InstitutionName.Focus();
        NameInJournal.Text = AuthorName.Text;



    }



    private void fnRecordExist(object sender, GridViewEditEventArgs e)
    {
        panAddAuthor.Enabled = true;
        popup.Visible = false;
        panel.Visible = true;
        panelRemarks.Visible = true;
        popupPanelAffilUpdate.Update();
        string Pid = Session["TempPid"].ToString();
        string TypeEntry = Session["TempTypeEntry"].ToString();
        btnSave.Enabled = true;

        string FileUpload = "";
        Business b1 = new Business();
        FileUpload = b1.GetFileUploadPath(Pid, TypeEntry);
        if (FileUpload != "")
        {
            GVViewFile.Visible = true;
        }
        else
        {
            GVViewFile.Visible = false;
        }
        DSforgridview.SelectParameters.Clear();
        DSforgridview.SelectParameters.Add("TypeEntry", TypeEntry);
        DSforgridview.SelectParameters.Add("Pid", Pid);
        DSforgridview.SelectCommand = "select UploadPDFPath from Publication where PublicationID=@Pid  and TypeOfEntry=@TypeEntry ";
        DSforgridview.DataBind();
        GVViewFile.DataBind();

       
        LabelUploadPfd.Visible = true;
        Business obj = new Business();
        PublishData v = new PublishData();
        v = obj.fnfindjid(Pid, TypeEntry);

        DropDownListPublicationEntry.SelectedValue = TypeEntry;
        DropDownListMuCategory.SelectedValue = v.MUCategorization;
        DropDownListPublicationEntry.Enabled = false;
        TextBoxPubId.Text = Pid;
        txtboxTitleOfWrkItem.Text = v.TitleWorkItem;
       // TextBoxRemarks.Text = v.RemarksFeedback;
        TextBoxYearJA.Items.Clear();
        if (TypeEntry == "JA" || TypeEntry == "TA")
        {
            string incentivestatus = b1.checkIncentivePointStatus(Pid, TypeEntry);
            if (incentivestatus == "PRC" || incentivestatus == "APP")
            {
                Incentivenote.Visible = true;
                btnSave.Enabled = false;
            }
            else
            {
                Incentivenote.Visible = false;
                btnSave.Enabled = true;
            }
        }
        else
        {
            Incentivenote.Visible = false;
            btnSave.Enabled = true;
        }
        if (TypeEntry == "JA")
        {
            string jname = obj.fnfindjidgtjname(Pid, TypeEntry);
            panelJournalArticle.Visible = true;

            DropDownListMuCategory.Enabled = true;
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
                TextBoxYearJA.Items.Add(new ListItem(yeatAppend.ToString(), yeatAppend.ToString(), true));
            }

            TextBoxYearJA.DataBind();

            TextBoxYearJA.SelectedValue = v.PublishJAYear;

            TextBoxImpFact.Text = v.ImpactFactor;
            TextBoxImpFact5.Text = v.ImpactFactor5;
            if (v.IFApplicableYear != 0)
            {
                txtIFApplicableYear.Text = v.IFApplicableYear.ToString();
            }

           // txtCitation.Text = v.CitationUrl;
            DropDownListPubType.SelectedValue = v.Publicationtype;
          //  TextBoxPageFrom.Text = v.PageFrom;
            if (v.PageFrom != "")
            {
                TextBoxPageTo.Enabled = true;
            }
            else
            {
                TextBoxPageTo.Enabled = false;
            }
            //TextBoxPageTo.Text = v.PageTo;
            if (v.PageFrom.Contains("PF") || v.PageFrom=="")
            {
                TextBoxPageFrom.Enabled = true;
                TextBoxPageTo.Enabled = false;
            }
            else
            {
                TextBoxPageFrom.Enabled = true;
                TextBoxPageTo.Enabled = true;
                TextBoxPageFrom.Text = v.PageFrom;
                TextBoxPageTo.Text = v.PageTo;
            }

            TextBoxIssue.Text = v.Issue;
            TextBoxVolume.Text = v.JAVolume;
            RadioButtonListIndexed.SelectedValue = v.Indexed;
            JournalData jd = new JournalData();
            Business B = new Business();
            int jayear = Convert.ToInt16(TextBoxYearJA.Text);
            int jamonth = Convert.ToInt16(v.PublishJAMonth);
            if (jayear >= 2018 && jamonth >= 7)
            {
                jd = B.selectQuartilevaluefrompublicationEntry(TextBoxPubId.Text, DropDownListMuCategory.SelectedValue, DropDownListPublicationEntry.SelectedValue);
                lblQuartile.Visible = true;
                txtquartile.Visible = true;
                txtquartile.Text = jd.QName;
                txtquartileid.Text = jd.Jquartile;
            }
            else
                if (jayear >= 2019 && jamonth >= 1)
                {
                    jd = B.selectQuartilevaluefrompublicationEntry(TextBoxPubId.Text, DropDownListMuCategory.SelectedValue, DropDownListPublicationEntry.SelectedValue);
                    lblQuartile.Visible = true;
                    txtquartile.Visible = true;
                    txtquartile.Text = jd.QName;
                    txtquartileid.Text = jd.Jquartile;
                }
        }


        else if (TypeEntry == "TS")
        {

            string jname = obj.fnfindjidgtjname(Pid, TypeEntry);

            panelJournalArticle.Visible = true;
            DropDownListMuCategory.Enabled = true;

            txtboxTitleOfWrkItem.Text = v.TitleWorkItem;

            TextBoxPubJournal.Text = v.PublisherOfJournal;

            TextBoxNameJournal.Text = jname;

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
                TextBoxYearJA.Items.Add(new ListItem(yeatAppend.ToString(), yeatAppend.ToString(), true));
            }
            TextBoxYearJA.DataBind();



            TextBoxYearJA.SelectedValue = v.PublishJAYear;

            TextBoxImpFact.Text = v.ImpactFactor;
            TextBoxImpFact5.Text = v.ImpactFactor5;
            if (v.IFApplicableYear != 0)
            {
                txtIFApplicableYear.Text = v.IFApplicableYear.ToString();
            }

            //txtCitation.Text = v.CitationUrl;

            // DropDownListPubType.SelectedValue=;
            if (v.PageFrom != "")
            {
                TextBoxPageTo.Enabled = true;
            }
            else
            {
                TextBoxPageTo.Enabled = false;
            }
            //TextBoxPageTo.Text = v.PageTo;
            if (v.PageFrom.Contains("PF") || v.PageFrom == "")
            {
                TextBoxPageFrom.Enabled = true;
                TextBoxPageTo.Enabled = false;
            }
            else
            {
                TextBoxPageFrom.Enabled = true;
                TextBoxPageTo.Enabled = true;
                TextBoxPageFrom.Text = v.PageFrom;
                TextBoxPageTo.Text = v.PageTo;
            }

            //  TextBoxPageFrom.Text = v.PageFrom;
            TextBoxIssue.Text = v.Issue;
            //TextBoxPageTo.Text = v.PageTo;
            TextBoxVolume.Text = v.JAVolume;
            RadioButtonListIndexed.SelectedValue = v.Indexed;

        }
        else
        {
            DropDownListMuCategory.Enabled = false;
        }
        panelTechReport.Visible = true;
        //TextBoxURL.Text = v.url;
        TextBoxDOINum.Text = v.DOINum;
        TextBoxAbstract.Text = v.Abstract;
        DropDownListErf.SelectedValue = v.isERF;
        TextBoxKeywords.Text = v.Keywords;
        //TextBoxReference.Text = v.TechReferences;
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
                DropDownList DropdownMuNonMu = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[3].FindControl("DropdownMuNonMu");
                TextBox EmployeeCode = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[2].FindControl("EmployeeCode");
                TextBox AuthorName = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[2].FindControl("AuthorName");
                ImageButton EmployeeCodeBtnimg = (ImageButton)Grid_AuthorEntry.Rows[rowIndex].Cells[2].FindControl("EmployeeCodeBtn");
                TextBox InstNme = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[2].FindControl("InstitutionName");
                TextBox deptname = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[2].FindControl("DepartmentName");
                HiddenField InstId = (HiddenField)Grid_AuthorEntry.Rows[rowIndex].Cells[2].FindControl("Institution");
                HiddenField deptId = (HiddenField)Grid_AuthorEntry.Rows[rowIndex].Cells[2].FindControl("Department");
                TextBox MailId = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[2].FindControl("MailId");
                DropDownList isCorrAuth = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[2].FindControl("isCorrAuth");
                DropDownList AuthorType = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[2].FindControl("AuthorType");
                TextBox NameAsInJournal = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[2].FindControl("NameInJournal");

                DropDownList IsPresenter = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[2].FindControl("IsPresenter");
                CheckBox HasAttented = (CheckBox)Grid_AuthorEntry.Rows[rowIndex].Cells[2].FindControl("HasAttented");

                DropDownList NationalType = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[2].FindControl("NationalType");
                DropDownList ContinentId = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[2].FindControl("ContinentId");

                DropDownList DropdownStudentInstitutionName = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[2].FindControl("DropdownStudentInstitutionName");
                DropDownList DropdownStudentDepartmentName = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[2].FindControl("DropdownStudentDepartmentName");


                DropDownList DropdownMuNonMu1 = (DropDownList)Grid_AuthorEntry.Rows[0].Cells[3].FindControl("DropdownMuNonMu");
                HiddenField PublicationLine = (HiddenField)Grid_AuthorEntry.Rows[rowIndex].Cells[2].FindControl("PublicationLine");

                drCurrentRow = dtCurrentTable.NewRow();

                PublicationLine.Value = dtCurrentTable.Rows[i - 1]["PublicationLine"].ToString();
                DropdownMuNonMu.Text = dtCurrentTable.Rows[i - 1]["DropdownMuNonMu"].ToString();
                EmployeeCode.Text = dtCurrentTable.Rows[i - 1]["EmployeeCode"].ToString();
                AuthorName.Text = dtCurrentTable.Rows[i - 1]["AuthorName"].ToString();
                NameAsInJournal.Enabled = false;
                AuthorType.Enabled = true;
                isCorrAuth.Enabled = true;
                DropdownStudentDepartmentName.Enabled = false;
                DropdownMuNonMu.Enabled = true;
                if (DropdownMuNonMu.Text == "M")
                {
                    InstNme.Visible = true;
                    deptname.Visible = true;
                    DropdownStudentInstitutionName.Visible = false;
                    DropdownStudentDepartmentName.Visible = false;
                    NationalType.Visible = false;
                    ContinentId.Visible = false;
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
                else   if (DropdownMuNonMu.Text == "E")
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
                else if (DropdownMuNonMu.Text == "O")
                {
                    DropdownStudentInstitutionName.Visible = true;
                    DropdownStudentDepartmentName.Visible = true;
                    InstNme.Visible = false;
                    deptname.Visible = false;
                    DropdownStudentInstitutionName.SelectedValue = dtCurrentTable.Rows[i - 1]["Institution"].ToString();
                    DropdownStudentDepartmentName.SelectedValue = dtCurrentTable.Rows[i - 1]["Department"].ToString();
                    EmployeeCode.Text = dtCurrentTable.Rows[i - 1]["EmployeeCode"].ToString();

                    EmployeeCode.Enabled = true;
                    NationalType.Visible = false;
                    ContinentId.Visible = false;
                }
                else if (DropdownMuNonMu.Text == "S")
                {
                    if (DropDownListPublicationEntry.SelectedValue == "JA" || DropDownListPublicationEntry.SelectedValue == "TS" || DropDownListPublicationEntry.SelectedValue == "CP")
                    {
                        DropdownStudentInstitutionName.Visible = false;
                        DropdownStudentDepartmentName.Visible = false;
                        InstNme.Visible = true;
                        deptname.Visible = true;
                        EmployeeCode.Enabled = false;
                        InstNme.Text = dtCurrentTable.Rows[i - 1]["InstitutionName"].ToString();
                        deptname.Text = dtCurrentTable.Rows[i - 1]["DepartmentName"].ToString();
                        InstId.Value = dtCurrentTable.Rows[i - 1]["Institution"].ToString();
                        deptId.Value = dtCurrentTable.Rows[i - 1]["Department"].ToString();
                  
                    }
                    else
                    {
                        DropdownStudentInstitutionName.Visible = true;
                        DropdownStudentDepartmentName.Visible = true;
                        InstNme.Visible = false;
                        deptname.Visible = false;
                        EmployeeCode.Enabled = false;
                        DropdownStudentInstitutionName.Text = dtCurrentTable.Rows[i - 1]["Institution"].ToString();
                        DropdownStudentDepartmentName.Text = dtCurrentTable.Rows[i - 1]["Department"].ToString();
                        InstId.Value = dtCurrentTable.Rows[i - 1]["Institution"].ToString();
                        deptId.Value = dtCurrentTable.Rows[i - 1]["Department"].ToString();
                    }
                    NationalType.Visible = false;
                    ContinentId.Visible = false;
                }
                DropdownMuNonMu1.Enabled = true;

                if (DropDownListPublicationEntry.SelectedValue == "JA" || DropDownListPublicationEntry.SelectedValue == "TS")
                {
                    DropdownMuNonMu1.Enabled = true;
                }
                else
                {
                    DropdownMuNonMu1.Enabled = true;
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
                    AuthorName.Enabled = false;
                    InstNme.Enabled = false;
                    deptname.Enabled = false;
                    MailId.Enabled = false;
                    EmployeeCode.Enabled = false;
                }
                else if (DropdownMuNonMu.Text == "E")
                {
                    EmployeeCodeBtnimg.Enabled = false;
                    AuthorName.Enabled = false;
                    InstNme.Enabled = false;
                    deptname.Enabled = false;
                    MailId.Enabled = false;
                    EmployeeCode.Enabled = false;
                }
                else if (DropdownMuNonMu.Text == "M")
                {
                    EmployeeCodeBtnimg.Enabled = true;
                    AuthorName.Enabled = false;
                    InstNme.Enabled = false;
                    deptname.Enabled = false;
                    MailId.Enabled = false;
                    EmployeeCode.Enabled = false;
                }
                else if (DropdownMuNonMu.Text == "S")
                {
                    if (DropDownListPublicationEntry.SelectedValue == "JA" || DropDownListPublicationEntry.SelectedValue == "TS")
                    {
                        AuthorName.Enabled = false;
                        DropdownStudentInstitutionName.Visible = false;
                        DropdownStudentInstitutionName.Visible = false;
                        EmployeeCode.Enabled = false;
                    }
                    else
                    {
                        EmployeeCodeBtnimg.Enabled = false;
                        AuthorName.Enabled = false;
                        DropdownStudentInstitutionName.Enabled = false;
                        DropdownStudentInstitutionName.Enabled = false;
                        EmployeeCode.Enabled = true;
                    }

                    MailId.Enabled = false;
                }
                else if (DropdownMuNonMu.Text == "O")
                {
                    AuthorName.Enabled = false;
                    EmployeeCodeBtnimg.Enabled = false;
                    DropdownStudentInstitutionName.Enabled = false;
                    DropdownStudentInstitutionName.Enabled = false;

                    EmployeeCode.Enabled = true;
                }
                else
                {
                    MailId.Enabled = false;
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

                if (DropdownMuNonMu.Text == "O")
                {
                    EmployeeCode.Enabled = true;
                }
                else
                {
                    EmployeeCode.Enabled = false;
                }

                rowIndex++;

            }


            ViewState["CurrentTable"] = dtCurrentTable;
        }
        PanelMU.Enabled = true;
        CheckboxIndexAgency.DataBind();
        SqlDataSourceCheckboxIndexAgency.DataBind();
        if (RadioButtonListIndexed.SelectedValue == "Y")
        {
            
            CheckboxIndexAgency.Enabled = false;
         
            CheckboxIndexAgency.Enabled = false;

            string IndexAgency = null;
            DataSet dz = obj.fnfindjournalAccount1(Pid, TypeEntry);

            int count = dz.Tables[0].Rows.Count;
            for (int i = 0; i < count; i++)
            {

                if (dz.Tables[0].Rows[i]["agencyid"].ToString() != null)
                {
                    IndexAgency = dz.Tables[0].Rows[i]["agencyid"].ToString();

                }

                if (dz.Tables[0].Rows[i]["agencyid"].ToString() != null)
                {
                    ListItem currentCheckBox = (ListItem)CheckboxIndexAgency.Items.FindByValue(IndexAgency);

                    if (currentCheckBox != null)
                        currentCheckBox.Selected = true;

                }

            }
        }
        else
        {
           
            CheckboxIndexAgency.Enabled = false;
            CheckboxIndexAgency.Enabled = false;
            CheckboxIndexAgency.ClearSelection();
        }
        int rowindex3 = 0;
        if (DropDownListPublicationEntry.SelectedValue == "JA" || DropDownListPublicationEntry.SelectedValue == "TS")
        {
            for (int i = 0; i < Grid_AuthorEntry.Rows.Count; i++)
            {

                DropDownList DropdownMuNonMu = (DropDownList)Grid_AuthorEntry.Rows[i].Cells[2].FindControl("DropdownMuNonMu");
                DropDownList AuthorType = (DropDownList)Grid_AuthorEntry.Rows[i].Cells[6].FindControl("AuthorType");
                if (DropdownMuNonMu.SelectedValue == "S")
                {
                    if (AuthorType.SelectedValue == "P")
                    {
                        string publicationmonth = v.PublishJAMonth.ToString();
                        string publicationyear = v.PublishJAYear;
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
                            //    if (DropdownMuNonMu1.SelectedValue == "M" || DropdownMuNonMu1.SelectedValue == "S")
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
                    //rowindex3++;
                    continue;
                }

            }
        }
        else
        {
            StudentPub.Value = "N";
        }
       // btnSave.Enabled = true;
        setModalWindow(sender, e);
    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
       
        if (!Page.IsValid)
        {
            return;
        }
          int countAuthType = 0;
          int countCorrAuth = 0;
        DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];   
                       PublishData[] JD = new PublishData[dtCurrentTable.Rows.Count];
        string confirmValue2 = Request.Form["confirm_value"];

        if (confirmValue2 == "Yes")
        {
            if (TextBoxPageFrom.Text != "" && TextBoxPageTo.Text == "")
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please Enter Page To')</script>");
                TextBoxPageTo.Enabled = true;
                return;
            }
            if (DropDownListPublicationEntry.SelectedValue == "JA" || DropDownListPublicationEntry.SelectedValue == "TS")
            {
                string month = DropDownListMonthJA.SelectedValue;
                int month2 = Convert.ToInt32(month);
                if (month2 < 10 && TextBoxYearJA.Text == "2015")
                {
                    ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please enter the valid month and year!')</script>");

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

                string year = TextBoxYearJA.Text;
                int year2 = Convert.ToInt32(year);

                if (monthnow2 < month2 && yearnow2 <= year2)
                {
                    ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please enter the valid month and year!')</script>");

                    return;
                }

            }

            string PageFrom = TextBoxPageFrom.Text.Trim();
            string PageTo = TextBoxPageTo.Text.Trim();
            string Volume = TextBoxVolume.Text.Trim();
            string Issue = TextBoxIssue.Text.Trim();
            PublishData j = new PublishData();
            j.PublishJAMonth =Convert.ToInt16(DropDownListMonthJA.SelectedValue);
            j.PublishJAYear = TextBoxYearJA.SelectedValue;
            j.ImpactFactor = TextBoxImpFact.Text;
            j.ImpactFactor5 = TextBoxImpFact5.Text;
            if (txtIFApplicableYear.Text != "")
            {
                j.IFApplicableYear = Convert.ToInt16(txtIFApplicableYear.Text);
            }
            j.PageFrom = PageFrom;
            j.PageTo = PageTo;
            j.JAVolume = Volume;
            j.Issue = Issue;
            j.PublisherOfJournal = TextBoxPubJournal.Text.Trim();
            j.PaublicationID = TextBoxPubId.Text;
            j.TypeOfEntry = DropDownListPublicationEntry.SelectedValue;
            j.RemarksFeedback = TextBoxRemarks.Text;

            string PublishDate = "" + j.PublishJAYear + "-" + j.PublishJAMonth + "-01";
            DateTime pubDate = Convert.ToDateTime(PublishDate);
            j.PublishDate = pubDate;
            j.MUCategorization = DropDownListMuCategory.SelectedValue;

         
            if (DropDownListPublicationEntry.SelectedValue == "JA" || DropDownListPublicationEntry.SelectedValue == "TS")
            {
                //for (int i = 0; i < Grid_AuthorEntry.Rows.Count; i++)
                //{
                //    JD[i] = new PublishData();
                //    DropDownList DropdownMuNonMu = (DropDownList)Grid_AuthorEntry.Rows[i].Cells[2].FindControl("DropdownMuNonMu");
                //    DropDownList AuthorType = (DropDownList)Grid_AuthorEntry.Rows[i].Cells[6].FindControl("AuthorType");
                //    TextBox EmployeeCode = (TextBox)Grid_AuthorEntry.Rows[i].Cells[2].FindControl("EmployeeCode");
                //    TextBox AuthorName = (TextBox)Grid_AuthorEntry.Rows[i].Cells[2].FindControl("AuthorName");
                //    HiddenField PublicationLine = (HiddenField)Grid_AuthorEntry.Rows[i].Cells[2].FindControl("PublicationLine");
                //    string lineno = PublicationLine.Value;
                //    if (DropdownMuNonMu.SelectedValue == "O")
                //    {

                //        JD[i].EmployeeCode = EmployeeCode.Text.Trim();
                //        JD[i].AuthorName = AuthorName.Text.Trim();
                //        JD[i].MUNonMU = DropdownMuNonMu.Text.Trim();
                //        JD[i].PublicationLine = Convert.ToInt32(lineno);
                //    }

                //}
            }
             
            int rowIndex1 = 0;
            if (dtCurrentTable.Rows.Count > 0)
            {

                for (int i = 0; i < dtCurrentTable.Rows.Count; i++)
                {
                    JD[i] = new PublishData();

                    // TextBox Author = (TextBox)Grid_AuthorEntry.Rows[rowIndex1].Cells[0].FindControl("Author");
                    TextBox AuthorName = (TextBox)Grid_AuthorEntry.Rows[rowIndex1].Cells[1].FindControl("AuthorName");
                    DropDownList DropdownMuNonMu = (DropDownList)Grid_AuthorEntry.Rows[rowIndex1].Cells[2].FindControl("DropdownMuNonMu");
                    //TextBox amount = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[3].FindControl("amount");
                    TextBox EmployeeCode = (TextBox)Grid_AuthorEntry.Rows[rowIndex1].Cells[0].FindControl("EmployeeCode");
                    HiddenField Institution = (HiddenField)Grid_AuthorEntry.Rows[rowIndex1].Cells[0].FindControl("Institution");
                    TextBox InstitutionName = (TextBox)Grid_AuthorEntry.Rows[rowIndex1].Cells[6].FindControl("InstitutionName");
                    HiddenField Department = (HiddenField)Grid_AuthorEntry.Rows[rowIndex1].Cells[0].FindControl("Department");
                    TextBox DepartmentName = (TextBox)Grid_AuthorEntry.Rows[rowIndex1].Cells[0].FindControl("DepartmentName");
                    TextBox MailId = (TextBox)Grid_AuthorEntry.Rows[rowIndex1].Cells[0].FindControl("MailId");
                    DropDownList isCorrAuth = (DropDownList)Grid_AuthorEntry.Rows[rowIndex1].Cells[0].FindControl("isCorrAuth");
                    DropDownList AuthorType = (DropDownList)Grid_AuthorEntry.Rows[rowIndex1].Cells[0].FindControl("AuthorType");
                    TextBox NameInJournal = (TextBox)Grid_AuthorEntry.Rows[rowIndex1].Cells[0].FindControl("NameInJournal");
                    DropDownList DropdownStudentInstitutionName = (DropDownList)Grid_AuthorEntry.Rows[rowIndex1].Cells[0].FindControl("DropdownStudentInstitutionName");
                    DropDownList DropdownStudentDepartmentName = (DropDownList)Grid_AuthorEntry.Rows[rowIndex1].Cells[0].FindControl("DropdownStudentDepartmentName");

                    DropDownList NationalType = (DropDownList)Grid_AuthorEntry.Rows[rowIndex1].Cells[0].FindControl("NationalType");
                    DropDownList ContinentId = (DropDownList)Grid_AuthorEntry.Rows[rowIndex1].Cells[0].FindControl("ContinentId");


                    DropDownList IsPresenter = (DropDownList)Grid_AuthorEntry.Rows[rowIndex1].Cells[0].FindControl("IsPresenter");
                    CheckBox HasAttented = (CheckBox)Grid_AuthorEntry.Rows[rowIndex1].Cells[0].FindControl("HasAttented");
                    if (AuthorName.Text == "")
                    {
                        ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please enter Author Name!')</script>");
                        return;

                    }

                    //if (DropdownMuNonMu.SelectedValue == "O")
                    //{

                    //    if (EmployeeCode.Text == "")
                    //    {
                    //        ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please enter Roll No!')</script>");
                    //        return;
                    //    }
                    //}
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

                    if (DropDownListPublicationEntry.SelectedValue == "JA" || DropDownListPublicationEntry.SelectedValue == "TS")
                    {
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
                    }
                    else
                    {
                        if (JD[i].MUNonMU == "M")
                        {
                            JD[i].EmployeeCode = EmployeeCode.Text;
                        }
                        else
                        {
                            JD[i].EmployeeCode = AuthorName.Text.Trim();
                        }
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
                    else
                        if (JD[i].MUNonMU == "E")
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
                        JD[i].EmployeeCode = JD[i].EmployeeCode;
                    }
                    else if (JD[i].MUNonMU == "S")
                    {
                        if (DropDownListPublicationEntry.SelectedValue == "JA" || DropDownListPublicationEntry.SelectedValue == "TS")
                        {
                            JD[i].InstitutionName = InstitutionName.Text.Trim();
                            JD[i].Department = Department.Value.Trim();
                            JD[i].DepartmentName = DepartmentName.Text.Trim();
                            JD[i].Institution = Institution.Value.Trim();
                            JD[i].AppendInstitutions = JD[i].Institution;
                            JD[i].EmployeeCode = JD[i].EmployeeCode;
                        }
                        else
                        {
                            JD[i].InstitutionName = DropdownStudentInstitutionName.SelectedItem.ToString();
                            JD[i].Department = DropdownStudentDepartmentName.SelectedValue;
                            JD[i].DepartmentName = DropdownStudentDepartmentName.SelectedItem.ToString();
                            JD[i].Institution = DropdownStudentInstitutionName.SelectedValue;
                            JD[i].AppendInstitutions = JD[i].Institution;
                            JD[i].EmployeeCode = JD[i].EmployeeCode;
                        }
                        JD[i].NationalInternationl = "";
                        JD[i].continental = "";

                    }

                    else if (JD[i].MUNonMU == "O")
                    {
                        JD[i].EmployeeCode = EmployeeCode.Text.Trim();
                        JD[i].InstitutionName = DropdownStudentInstitutionName.SelectedItem.ToString();
                        JD[i].Department = DropdownStudentDepartmentName.SelectedValue;
                        JD[i].DepartmentName = DropdownStudentDepartmentName.SelectedItem.ToString();
                        JD[i].Institution = DropdownStudentInstitutionName.SelectedValue;
                        JD[i].AppendInstitutions = JD[i].Institution;
                        JD[i].EmailId = MailId.Text.Trim();
                        JD[i].NationalInternationl = "";
                        JD[i].continental = "";
                    }


                    //  JD[i].InstitutionName = InstitutionName.Text.Trim();

                    JD[i].AppendInstitutionNames = JD[i].InstitutionName;
                    //  JD[i].DepartmentName = DepartmentName.Text.Trim();
                    JD[i].EmailId = MailId.Text.Trim();

                    JD[i].isCorrAuth = isCorrAuth.Text.Trim();
                    //JD[i].AuthorType = AuthorType.Text.Trim();

                    JD[i].NameInJournal = NameInJournal.Text.Trim();
                    if (DropDownListPublicationEntry.SelectedValue == "JA" || DropDownListPublicationEntry.SelectedValue == "TS")
                    {
                        JD[i].AuthorType = AuthorType.Text.Trim();
                    }
                    else
                    {
                        JD[i].AuthorType = "A";
                    }
                   
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
            int rowIndex2 = 0;
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
                                string publicationmonth = DropDownListMonthJA.SelectedValue;
                                string publicationyear = TextBoxYearJA.SelectedValue;
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
                                    //        //break;
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
           
            int result = 0;
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
                        result = 0;
                    }
                    else
                    {
                        for (int i = 0; i < list.Count; i++)
                        {
                            string id = list[i].ToString();
                            if (id != TextBoxPubId.Text)
                            {
                                result = 1;
                                break;
                            }
                            else
                            {
                                continue;
                            }
                        }
                    }
                }

            }

            if (result == 1)
            {
                log.Info("Entered article already exist for ISSN: " + j.PublisherOfJournal + " , JA Volume: " + j.JAVolume + " , Issue: " + j.Issue + " , Page From :" + j.PageFrom + ",Page To:" + j.PageTo);
                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Entered article already exist for ISSN: " + j.PublisherOfJournal + " , JA Volume: " + j.JAVolume + " , Issue: " + j.Issue + " , Page From :" + j.PageFrom + ",Page To:" + j.PageTo + "')</script>");
                return;
            }
            else
            {

                result = B.UpdatePublicationData(j,JD);
                if (result >= 1)
                {
                    log.Info("Publication Data Updated Successfully of ID : "+j.PaublicationID+" and TypeOfEntry : "+j.TypeOfEntry);
                    ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Publication Updated Successfully.. of Entry Type: " + DropDownListPublicationEntry.SelectedValue + " of ID: " + TextBoxPubId.Text + "')</script>");
                    panel.Visible=false;

                }
            }
        }
        
    }

    protected void GridViewSearchPub_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        dataBind();
        GridViewSearch.PageIndex = e.NewPageIndex;
        GridViewSearch.DataBind();
    }
    protected void GVViewFile_SelectedIndexChanged(object sender, EventArgs e)
    {
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
            Response.Write("<script>");
            Response.Write("window.open('DisplayPdf.aspx?val=" + newpath + "','_blank')");
            //path sent to display.aspx page
            Response.Write("</script>");
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
            TextBoxPageTo.Text = "";
            TextBoxPageTo.Enabled = false;
            RequiredFieldValidator1.Enabled = false;
        }
    }
    protected void txtboxYear_TextChanged(object sender, EventArgs e)
    {
        //UpdatePanel1.Update();
        //Page.MaintainScrollPositionOnPostBack = true;
        //TextBoxYearJA.Focus();
        JournalValueObj.year = TextBoxYearJA.SelectedValue;
        JournalValueObj.JournalID = TextBoxPubJournal.Text;
        JournalValueObj.month = DropDownListMonthJA.SelectedValue;
        if (TextBoxYearJA.SelectedValue != "")
        {

            JournalData j = new JournalData();
            j = B.GetImpactFactorApplicableYear(JournalValueObj);
            if (j.JournalID != "")
            {
                TextBoxImpFact.Text = Convert.ToString(j.impctfact);
                TextBoxImpFact5.Text = Convert.ToString(j.fiveimpcrfact);
                txtIFApplicableYear.Text = j.year;
            }
            else
            {
                TextBoxImpFact.Text = "";
                TextBoxImpFact5.Text = "";
                txtIFApplicableYear.Text = "";
            }

            JournalData jd = new JournalData();
            jd = B.JournalGetImpactFactorPublishEntry(JournalValueObj);

            //if (jd.Comments != "false")
            //{
            //    txtIFApplicableYear.Text = JournalValueObj.year;
            //    TextBoxImpFact.Text = Convert.ToString(jd.impctfact);
            //    TextBoxImpFact5.Text = Convert.ToString(jd.fiveimpcrfact);
            //}
            //else
            //{

            //}
        }
        else
        {
            TextBoxImpFact.Text = "";
            TextBoxImpFact5.Text = "";
            txtIFApplicableYear.Text = "";
        }

        int rowindex3 = 0;
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
                        string publicationmonth = JournalValueObj.month.ToString();
                        string publicationyear = JournalValueObj.year;
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
                            //    if (DropdownMuNonMu1.SelectedValue == "M" || DropdownMuNonMu1.SelectedValue == "S" || DropdownMuNonMu1.SelectedValue == "O")
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
                    //rowindex3++;
                    continue;
                }

            }
        }
        else
        {
            StudentPub.Value = "N";
        }
        TextBoxPageFrom.Focus();
    }
   
}