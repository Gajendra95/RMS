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
using System;
using System.Data.SqlClient;
using System.Net;
using System.Drawing;
using System.Runtime.InteropServices;

public partial class PublicationEntry_ModifyFileUpload : System.Web.UI.Page
{
    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    Business B = new Business();
    Journal_DataObject JournalDataObj = new Journal_DataObject();
    JournalData JournalValueObj = new JournalData();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            PublishData obj = new PublishData();
            obj.PaublicationID = PubIDSearch.Text;
            obj.TypeOfEntry = EntryTypesearch.SelectedValue;
            obj.TitleWorkItem = TextBoxWorkItemSearch.Text;
            obj.CreatedBy = Session["UserId"].ToString();
 
            DataSet ds = B.BindGridviewFileUpload(obj);
            GridViewSearch.DataSource = ds;
            GridViewSearch.DataBind();
            GridViewSearch.Visible = true;
        }

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

            //to view the pdf
            Response.Write("<script>");
            Response.Write("window.open('DisplayPdf.aspx?val=" + newpath + "','_blank')");
            Response.Write("</script>");
        }

    }
    protected void ButtonSearchPubOnClick(object sender, EventArgs e)
    {

        GridViewSearch.Visible = true;
        GridViewSearch.EditIndex = -1;
        addclik(sender, e);
        dataBind();
    }

    protected void addclik(object sender, EventArgs e)
    {
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
       // txtCitation.Text = "";
        DropDownListPubType.ClearSelection();
        TextBoxPageFrom.Text = "";
        TextBoxPageTo.Text = "";
        TextBoxVolume.Text = "";
        RadioButtonListIndexed.SelectedValue = "Y";
        CheckboxIndexAgency.ClearSelection();
        panelConfPaper.Visible = false;
        TextBoxEventTitle.Text = "";
        TextBoxPlace.Text = "";
        TextBoxDate.Text = "";
        panelBookPublish.Visible = false;
        TextBoxTitleBook.Text = "";
        TextBoxChapterContributed.Text = "";
        TextBoxEdition.Text = "";
        TextBoxPublisher.Text = "";
        DropDownListBookMonth.ClearSelection();
        TextBoxPageNum.Text = "";
        TextBoxVolume1.Text = "";
        panelOthes.Visible = false;
        TextBoxPageNumNewsPaper.Text = "";
        panelTechReport.Visible = false;
        TextBoxDOINum.Text = "";
       // DropDownListErf.ClearSelection();
        //TextBoxKeywords.Text = "";
        TextBoxIsbn.Text = "";
        RadioButtonListTypePresentaion.ClearSelection();
        TextBoxAwardedBy.Text = "";
        GVViewFile.Visible = false;
        TextBoxCreditPoint.Text = "0";
        TextBoxAwardedBy.Enabled = false;
        btnSave.Enabled = false;
    }
    private void dataBind()
    {
        panel.Visible = false;

        GridViewSearch.Visible = true;
        Business b = new Business();
        User a1 = new User();

        PublishData obj = new PublishData();
        obj.PaublicationID = PubIDSearch.Text;
        obj.TypeOfEntry = EntryTypesearch.SelectedValue;
        obj.TitleWorkItem = TextBoxWorkItemSearch.Text;
        obj.CreatedBy = Session["UserId"].ToString();
        DataSet ds = new DataSet();
        ds = B.BindGridviewFileUpload(obj);
        GridViewSearch.DataSource = ds;
        GridViewSearch.DataBind();

    }



    protected void RadioButtonListIndexedOnSelectedIndexChanged(object sender, EventArgs e)
    {
        if (RadioButtonListIndexed.SelectedValue == "N")
        {
            CheckboxIndexAgency.Enabled = false;
            CheckboxIndexAgency.ClearSelection();
        }
        else
        {
            CheckboxIndexAgency.Enabled = true;
        }
    }
    protected void RadioButtonListCPIndexedOnSelectedIndexChanged(object sender, EventArgs e)
    {

        if (RadioButtonListCPIndexed.SelectedValue == "N")
        {
            CheckBoxListCPIndexedIn.Enabled = false;
            CheckBoxListCPIndexedIn.ClearSelection();
        }
        else
        {

            CheckBoxListCPIndexedIn.Enabled = true;

        }
    }
    protected void TextBoxPageFromCP_TextChanged(object sender, EventArgs e)
    {
        if (TextBoxPageFromCP.Text != "")
        {
            TextBoxPageToCP.Enabled = true;

        }
        else
        {
            TextBoxPageToCP.Enabled = false;

        }
    }
    protected void JournalIDTextChanged(object sender, EventArgs e)
    {
        JournalValueObj.JournalID = TextBoxPubJournal.Text;
        // JournalValueObj.year = txtBoxYear.Text;
        JournalData j = new JournalData();
        j = B.JournalEntryCheckExistance(JournalValueObj);
        if (j.jid != null)
        {
            // ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Entry ALready Exists')</script>");
            TextBoxNameJournal.Text = j.jname;

            string year = DateTime.Now.Year.ToString();
            int Jyear = Convert.ToInt32(year) - 1;

            TextBoxYearJA.Text = Jyear.ToString();
            txtboxYear_TextChanged(sender, e);

        }
        else
        {
            TextBoxPubJournal.Text = "";
            ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Invalid ID')</script>");
        }

    }

    protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //Find the DropDownList in the Row
            DropDownList DropdownMuNonMu = (e.Row.FindControl("DropdownMuNonMu") as DropDownList);

            if (DropDownListPublicationEntry.SelectedValue == "BK"|| DropDownListPublicationEntry.SelectedValue == "NM")
            {


                SqlDataSourceAuthorType.SelectCommand = "SELECT Id,Type FROM [Author_Type_M] where (Id! = 'O')and (Id! = 'E') ";

                DropdownMuNonMu.DataTextField = "Type";
                DropdownMuNonMu.DataValueField = "Id";
                DropdownMuNonMu.DataBind();


            }
            else if (DropDownListPublicationEntry.SelectedValue == "JA" || DropDownListPublicationEntry.SelectedValue == "TS" || DropDownListPublicationEntry.SelectedValue == "CP" || DropDownListPublicationEntry.SelectedValue == "PR")
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

    private void SetInitialRow()
    {

        DataTable dt = new DataTable();
        DataRow dr = null;
        // dt.Columns.Add(new DataColumn("amount", typeof(string)));
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

        dr = dt.NewRow();
        // dr["amount"] = string.Empty;
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

        dt.Rows.Add(dr);

        ViewState["CurrentTable"] = dt;
        Grid_AuthorEntry.DataSource = dt;
        Grid_AuthorEntry.DataBind();
        //DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
        //DataRow drCurrentRow = null;


        //  TextBox Author = (TextBox)Grid_AuthorEntry.Rows[0].Cells[0].FindControl("Author");
        TextBox AuthorName = (TextBox)Grid_AuthorEntry.Rows[0].Cells[1].FindControl("AuthorName");
        ImageButton EmployeeCodeBtn = (ImageButton)Grid_AuthorEntry.Rows[0].Cells[1].FindControl("EmployeeCodeBtn");

        DropDownList DropdownMuNonMu = (DropDownList)Grid_AuthorEntry.Rows[0].Cells[2].FindControl("DropdownMuNonMu");
        //TextBox amount = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[3].FindControl("amount");
        TextBox EmployeeCode = (TextBox)Grid_AuthorEntry.Rows[0].Cells[0].FindControl("EmployeeCode");
        HiddenField Institution = (HiddenField)Grid_AuthorEntry.Rows[0].Cells[0].FindControl("Institution");
        TextBox InstitutionName = (TextBox)Grid_AuthorEntry.Rows[0].Cells[6].FindControl("InstitutionName");
        HiddenField Department = (HiddenField)Grid_AuthorEntry.Rows[0].Cells[0].FindControl("Department");
        TextBox DepartmentName = (TextBox)Grid_AuthorEntry.Rows[0].Cells[0].FindControl("DepartmentName");
        TextBox MailId = (TextBox)Grid_AuthorEntry.Rows[0].Cells[0].FindControl("MailId");

        DropDownList isCorrAuth = (DropDownList)Grid_AuthorEntry.Rows[0].Cells[0].FindControl("isCorrAuth");
        DropDownList AuthorType = (DropDownList)Grid_AuthorEntry.Rows[0].Cells[0].FindControl("AuthorType");

        TextBox NameInJournal = (TextBox)Grid_AuthorEntry.Rows[0].Cells[0].FindControl("NameInJournal");


        DropdownMuNonMu.Enabled = false;

        if (DropDownListPublicationEntry.SelectedValue == "BK" || DropDownListPublicationEntry.SelectedValue == "CP" || DropDownListPublicationEntry.SelectedValue == "NM")
        {


            SqlDataSourceAuthorType.SelectCommand = "SELECT Id,Type FROM [Author_Type_M] where (Id! = 'O') and (Id! = 'E')";

            DropdownMuNonMu.DataTextField = "Type";
            DropdownMuNonMu.DataValueField = "Id";
            DropdownMuNonMu.DataBind();

        }
        else if (DropDownListPublicationEntry.SelectedValue == "JA" || DropDownListPublicationEntry.SelectedValue == "TS" || DropDownListPublicationEntry.SelectedValue == "PR")
        {
            SqlDataSourceAuthorType.SelectCommand = "SELECT Id,DisplayName FROM [Author_Type_M] where (Id = 'M') or (Id = 'S') or (Id = 'N') or (Id = 'O') or (Id = 'E') ";

            DropdownMuNonMu.DataTextField = "DisplayName";
            DropdownMuNonMu.DataValueField = "Id";
            DropdownMuNonMu.DataBind();

        }

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
        string deptId = Session["Department"].ToString();

        string deptName1 = null;
        deptName1 = b.GetDeptName(deptId, InstituteId);
        DepartmentName.Text = deptName1;
        MailId.Text = Session["emailId"].ToString();
        MailId.Enabled = false;
        NameInJournal.Text = Session["UserName"].ToString();

        if (DropDownListPublicationEntry.SelectedValue == "JA" || DropDownListPublicationEntry.SelectedValue == "TS" || DropDownListPublicationEntry.SelectedValue == "PR")
        {
            isCorrAuth.Visible = true;
            AuthorType.Visible = true;
            NameInJournal.Visible = true;
            Grid_AuthorEntry.Columns[6].Visible = true;

            Grid_AuthorEntry.Columns[7].Visible = true;
            Grid_AuthorEntry.Columns[8].Visible = true;
        }
        else
        {
            isCorrAuth.Visible = false;
            AuthorType.Visible = false;
            NameInJournal.Visible = false;
            Grid_AuthorEntry.Columns[6].Visible = false;
            Grid_AuthorEntry.Columns[7].Visible = false;
            Grid_AuthorEntry.Columns[8].Visible = false;
        }

        Grid_AuthorEntry.Columns[11].Visible = false;

        if (DropdownMuNonMu.SelectedValue == "M")
        {
            EmployeeCodeBtn.Enabled = false;
        }
        else
        {
            EmployeeCodeBtn.Enabled = false;
        }

    }
    protected void DropdownMuNonMuOnSelectedIndexChanged(object sender, EventArgs e)
    {
        TextBox senderBox = sender as TextBox;

        GridViewRow currentRow = (GridViewRow)((DropDownList)sender).Parent.Parent;
        DropDownList DropdownMuNonMu = (DropDownList)currentRow.FindControl("DropdownMuNonMu");
        ImageButton EmployeeCodeBtn = (ImageButton)currentRow.FindControl("EmployeeCodeBtn");
        TextBox InstitutionName = (TextBox)currentRow.FindControl("InstitutionName");
        TextBox DepartmentName = (TextBox)currentRow.FindControl("DepartmentName");
        TextBox AuthorName = (TextBox)currentRow.FindControl("AuthorName");
        TextBox MailId = (TextBox)currentRow.FindControl("MailId");
        TextBox EmployeeCode = (TextBox)currentRow.FindControl("EmployeeCode");

        if (DropdownMuNonMu.SelectedValue == "M")
        {
            EmployeeCodeBtn.Enabled = true;
            InstitutionName.Enabled = false;
            DepartmentName.Enabled = false;
            AuthorName.Enabled = false;
            MailId.Enabled = false;
        }
        else
        {
            EmployeeCodeBtn.Enabled = false;
            InstitutionName.Enabled = true;
            DepartmentName.Enabled = true;
            AuthorName.Enabled = true;
            MailId.Enabled = true;
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



                    drCurrentRow = dtCurrentTable.NewRow();
                    //  dtCurrentTable.Rows[i - 1]["amount"] = amount.Text.Trim() == "" ? 0 : Convert.ToDecimal(amount.Text);
                    dtCurrentTable.Rows[i - 1]["DropdownMuNonMu"] = DropdownMuNonMu.Text;
                    //  dtCurrentTable.Rows[i - 1]["Author"] = Author.Text;
                    dtCurrentTable.Rows[i - 1]["AuthorName"] = AuthorName.Text;
                    dtCurrentTable.Rows[i - 1]["EmployeeCode"] = EmployeeCode.Text;
                    dtCurrentTable.Rows[i - 1]["Institution"] = Institution.Value;
                    dtCurrentTable.Rows[i - 1]["InstitutionName"] = InstitutionName.Text;
                    dtCurrentTable.Rows[i - 1]["Department"] = Department.Value;
                    dtCurrentTable.Rows[i - 1]["DepartmentName"] = DepartmentName.Text;
                    dtCurrentTable.Rows[i - 1]["MailId"] = MailId.Text;
                    dtCurrentTable.Rows[i - 1]["isCorrAuth"] = isCorrAuth.Text;
                    dtCurrentTable.Rows[i - 1]["AuthorType"] = AuthorType.Text;

                    dtCurrentTable.Rows[i - 1]["NameInJournal"] = NameInJournal.Text;

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




    protected void txtboxYear_TextChanged1(object sender, EventArgs e)
    {
        JournalValueObj.year = TextBoxYearJA.Text;
        JournalValueObj.JournalID = TextBoxPubJournal.Text;
        JournalData j = new JournalData();
        if (TextBoxYearJA.Text != "")
        {


            j = B.JournalGetImpactFactor(JournalValueObj);
            if (j.jid != null)
            { TextBoxImpFact.Text = JournalValueObj.impctfact.ToString(); }
        }
        else
        {
            // TextBoxImpFact.Text = "";
        }

    }


    protected void txtboxYear_TextChanged(object sender, EventArgs e)
    {
        JournalValueObj.year = TextBoxYearJA.Text;
        JournalValueObj.JournalID = TextBoxPubJournal.Text;

        if (TextBoxYearJA.Text != "")
        {

            JournalData jd = new JournalData();
            jd = B.JournalGetImpactFactorPublishEntry(JournalValueObj);
            if (jd.impctfact != 0.0)
            {
                TextBoxImpFact.Text = jd.impctfact.ToString();
            }
            else
            {
                //  TextBoxImpFact.Text = "";
            }
        }
        else
        {
            // TextBoxImpFact.Text = "";
        }

    }

    protected void DropDownListPublicationEntryOnSelectedIndexChanged(object sender, EventArgs e)
    {

        btnSave.Enabled = true;
        SetInitialRow();
        Grid_AuthorEntry.Visible = true;
        if (DropDownListPublicationEntry.SelectedValue == "JA")
        {
            panelJournalArticle.Visible = true;
            panelConfPaper.Visible = false;
            panelTechReport.Visible = true;
            panelBookPublish.Visible = false;
            panelOthes.Visible = false;
            panAddAuthor.Visible = true;
        }
        else
            if (DropDownListPublicationEntry.SelectedValue == "PR")
            {
                panelJournalArticle.Visible = true;
                panelConfPaper.Visible = false;
                panelTechReport.Visible = true;
                panelBookPublish.Visible = false;
                panelOthes.Visible = false;
                panAddAuthor.Visible = true;
            }
        else if (DropDownListPublicationEntry.SelectedValue == "CP")
        {
            panelConfPaper.Visible = true;

            panelJournalArticle.Visible = false;

            panelTechReport.Visible = true;
            panelBookPublish.Visible = false;
            panelOthes.Visible = false;
            panAddAuthor.Visible = true;
        }
        else if (DropDownListPublicationEntry.SelectedValue == "TS")
        {
            panelTechReport.Visible = true;

            panelConfPaper.Visible = false;

            panelJournalArticle.Visible = true;


            panelBookPublish.Visible = false;
            panelOthes.Visible = false;
            panAddAuthor.Visible = true;
        }
        else if (DropDownListPublicationEntry.SelectedValue == "BK")
        {
            panelBookPublish.Visible = true;

            panelTechReport.Visible = true;

            panelConfPaper.Visible = false;

            panelJournalArticle.Visible = false;

            panelOthes.Visible = false;
            panAddAuthor.Visible = true;
        }
        else if (DropDownListPublicationEntry.SelectedValue == "NM")
        {
            panelOthes.Visible = true;
            panelBookPublish.Visible = false;

            panelTechReport.Visible = true;

            panelConfPaper.Visible = false;

            panelJournalArticle.Visible = false;
            panAddAuthor.Visible = true;


        }
        else
        {
        }

    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        if (!Page.IsValid)
        {
            return;
        }
        try
        {

            Business b = new Business();
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
            ArrayList listIndexAgency = new ArrayList();
            Business B = new Business();
            PublishData j = new PublishData();
            PublishData[] JD = new PublishData[dtCurrentTable.Rows.Count];
            string FileUpload = "";
            string PublicationEntry = DropDownListPublicationEntry.SelectedValue;
            if (TextBoxPubId.Text != "")
            {
                FileUpload = b.GetFileUploadPath(TextBoxPubId.Text, PublicationEntry);
                j.FilePathNew = FileUpload;
            }

            string PublicationEntry1 = DropDownListPublicationEntry.SelectedValue;
            string filelocationpath = "";
            string UploadPdf1 = "";
            j.PaublicationID = TextBoxPubId.Text.Trim();
            j.TypeOfEntry = DropDownListPublicationEntry.SelectedValue;
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
                        string path_BoxId = Path.Combine(uploadfolder, TextBoxPubId.Text); //main path + location
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
                        j.PaublicationID = TextBoxPubId.Text.Trim();
                        j.TypeOfEntry = DropDownListPublicationEntry.SelectedValue;

                        if (FileUploadPdf.HasFile)
                        {
                            j.FilePath = filelocationpath;
                        }
                        else
                        {
                            j.FilePath = j.FilePathNew;
                        }
                        j.RemarksFeedback = TxtRemarks.Text.Trim();
                    }


                }
                else
                {
                    string CloseWindow = "alert('Invalid File!!!File exceeds more than 10MB..Please try to upload the file less than or equal to 10MB!!!!!!')";
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow, true);
                    log.Error("Invalid File!!!File exceeds more than 10MB!!! : " + TextBoxPubId.Text.Trim() + " , Project Type : " + DropDownListPublicationEntry.SelectedValue);
                    return;
                }
            }
            int result1 = B.UpdatePfPathCreate(j);

            if (result1 == 1)
            {
                DSforgridview.SelectParameters.Clear();
                DSforgridview.SelectParameters.Add("PublicationID", j.PaublicationID);
                DSforgridview.SelectParameters.Add("TypeOfEntry", j.TypeOfEntry);

                DSforgridview.SelectCommand = "select UploadPDFPath from Publication where PublicationID=@PublicationID and TypeOfEntry=@TypeOfEntry";
                DSforgridview.DataBind();
                GVViewFile.DataBind();
                string FileUpload1 = "";
                Business b1 = new Business();
                FileUpload1 = b1.GetFileUploadPath(j.PaublicationID, j.TypeOfEntry);
                if (FileUpload1 != "")
                {
                    GVViewFile.Visible = true;
                }
                else
                {
                    GVViewFile.Visible = false;
                }
                EmailDetails details = new EmailDetails();
                details = SendMail(j.PaublicationID, j.TypeOfEntry);
                details.Id = TextBoxPubId.Text;
                details.Type = DropDownListPublicationEntry.SelectedValue;
                SendMailObject obj = new SendMailObject();
                bool resultv = obj.InsertIntoEmailQueue(details);
                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('File Uploaded Successfully!')</script>");


                DataSet ds = B.BindGrid(j.PaublicationID, j.TypeOfEntry,Session["UserId"].ToString());
                GridViewSearch.DataSource = ds;
                GridViewSearch.DataBind();
                GridViewSearch.Visible = true;
            }
            else
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Error while uploading file')</script>");

            }

            btnSave.Enabled = false;
        }

        catch (Exception ex)
        {
            log.Error("Inside Catch Block Of Publication :" + ex.Message + " UserID : " + Session["UserId"].ToString());
            log.Error(ex.StackTrace);
            ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Error while uploading file')</script>");

        }

    }

    private EmailDetails SendMail(string publicatinid, string typeofentry)
    {
        log.Debug("ModifyFileUpload: Inside Send Mail function of Entry Type : " + typeofentry + " ID " + publicatinid);
        EmailDetails details = new EmailDetails();
        try
        {

            details.FromEmail = ConfigurationManager.AppSettings["FromAddress"].ToString();

            details.Id = publicatinid;
            details.Type = typeofentry;

            Business bus = new Business();
           
            details.Module = "MFUP";

            details.EmailSubject = "Publication Entry <  " + DropDownListPublicationEntry.SelectedValue + " _ " + TextBoxPubId.Text + "  > File Upload  ";

            string FooterText = ConfigurationManager.AppSettings["FooterText"].ToString();

            IndexManage v = new IndexManage();
            Business bs = new Business();
            string userid = Session["UserId"].ToString();
            v = bs.selectIndexAgency1(userid);

            string dep1 = v.nDepartment;
            string inst1 = v.nInstitution;

            v = bs.selectIndexAgency2(dep1, inst1);
            string mailid = v.emailid;
            details.ToEmail = mailid;
            details.CCEmail = Session["emailId"].ToString();

            details.MsgBody = "<span style=\"font-size: 10pt; color: #3300cc; font-family: Verdana\"><h4>Dear Sir/Madam,</h4> <br>" +
                             "<b> New File is uploaded. <br> " +
                             "<br>" +
                               "Publication Type  : " + DropDownListPublicationEntry.SelectedItem + "<br>" +
                            "Publication Id  :  " + TextBoxPubId.Text + "<br>" +

                            "Title of Work Item  : " + txtboxTitleOfWrkItem.Text + "<br>" +

                            "Added By  : " + Session["UserName"].ToString() + "<br>" +
                             "<br>" + "<br>" + "<br>" + "<br>" + FooterText +
                            " </b><br><b> </b></span>";
            return details;
        }

        catch (Exception ex)
        {
            log.Error(ex.StackTrace);
            log.Error(ex.Message);
            return details;
        }
    }
    


    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        ImageButton EditButton = (ImageButton)e.Row.FindControl("BtnEdit");
    }

    public void edit(Object sender, GridViewEditEventArgs e)
    {


        GridViewSearch.EditIndex = e.NewEditIndex;

        fnRecordExist(sender, e);

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
            //  typeEntry = GridViewSearch.Rows[rowindex].Cells[3].Text.ToString();
            typeEntry = TypeOfEntry.Value;
            pid = GridViewSearch.Rows[rowindex].Cells[1].Text.Trim().ToString();
            Session["TempPid"] = pid;
            Session["TempTypeEntry"] = typeEntry;//maintaining a session variable, passing it to registration page
        }

        else if (e.CommandName == "View")
        {
            GridViewRow rowSelect = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
            int rowindex = rowSelect.RowIndex;
            HiddenField TypeOfEntry = (HiddenField)GridViewSearch.Rows[rowindex].Cells[6].FindControl("TypeOfEntry");
            //  typeEntry = GridViewSearch.Rows[rowindex].Cells[3].Text.ToString();
            typeEntry = TypeOfEntry.Value;
            pid = GridViewSearch.Rows[rowindex].Cells[1].Text.Trim().ToString();
            Session["TempPid"] = pid;
            Session["TempTypeEntry"] = typeEntry;

            fnRecordExistApproval(sender, e);
        }
    }
    protected void GridViewSearchPub_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        dataBind();
        GridViewSearch.PageIndex = e.NewPageIndex;
        GridViewSearch.DataBind();
    }
    public void fnRecordExist(object sender, EventArgs e)
    {
    
        panel.Visible = true;
        string Pid = Session["TempPid"].ToString();
        string TypeEntry = Session["TempTypeEntry"].ToString();
        btnSave.Enabled = true;

        DSforgridview.SelectParameters.Clear();
        DSforgridview.SelectParameters.Add("PublicationID", Pid);
        DSforgridview.SelectParameters.Add("TypeOfEntry", TypeEntry);

        DSforgridview.SelectCommand = "select UploadPDFPath from Publication where PublicationID=@PublicationID and TypeOfEntry=@TypeOfEntry";
        DSforgridview.DataBind();
        GVViewFile.DataBind();
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

        // FileUploadPdf.Visible = true;
        LabelUploadPfd.Visible = true;
        Business obj = new Business();
        PublishData v = new PublishData();
        v = obj.fnfindjid(Pid, TypeEntry);

        //  FileUploadPdf.Visible = true;
        DropDownListMuCategory.DataBind();
        DropDownListMuCategory.SelectedValue = v.MUCategorization;
        DropDownListPublicationEntry.DataBind();
        DropDownListPublicationEntry.SelectedValue = TypeEntry;
        DropDownListPublicationEntry.Enabled = false;
        TextBoxPubId.Text = Pid;
        txtboxTitleOfWrkItem.Text = v.TitleWorkItem;

        // v = obj.fnfindjid(Pid, TypeEntry);
        if (TypeEntry == "JA")
        {
            string jname = obj.fnfindjidgtjname(Pid, TypeEntry);
            panelJournalArticle.Visible = true;
            panelBookPublish.Visible = false;
            panelConfPaper.Visible = false;
            panelOthes.Visible = false;

            txtboxTitleOfWrkItem.Text = v.TitleWorkItem;

            TextBoxPubJournal.Text = v.PublisherOfJournal;

            TextBoxNameJournal.Text = jname;

            DropDownListMonthJA.SelectedValue = v.PublishJAMonth.ToString();

            TextBoxYearJA.Text = v.PublishJAYear;

            TextBoxImpFact.Text = v.ImpactFactor;
            TextBoxImpFact5.Text = v.ImpactFactor5;
            if (v.IFApplicableYear != 0)
            {
                txtIFApplicable.Text = v.IFApplicableYear.ToString();
            }
            else
            {
                txtIFApplicable.Text = "";
            }
            //txtCitation.Text = v.CitationUrl;

            // DropDownListPubType.SelectedValue=;
            if (v.PageFrom.Contains("PF"))
            {
            }
            else
            {
                TextBoxPageFrom.Text = v.PageFrom;
                TextBoxPageTo.Text = v.PageTo;
            }
            //TextBoxPageFrom.Text = v.PageFrom;

            TextBoxIssue.Text = v.Issue;

            //TextBoxPageTo.Text = v.PageTo;
            TextBoxVolume.Text = v.JAVolume;
            RadioButtonListIndexed.SelectedValue = v.Indexed;
            // CheckboxIndexAgency.ClearSelection();
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
        else
            if (TypeEntry == "PR")
            {
                string jname = obj.fnfindjidgtPname(Pid, TypeEntry);
                panelJournalArticle.Visible = true;
                panelBookPublish.Visible = false;
                panelConfPaper.Visible = false;
                panelOthes.Visible = false;

                txtboxTitleOfWrkItem.Text = v.TitleWorkItem;

                TextBoxPubJournal.Text = v.PublisherOfJournal;

                TextBoxNameJournal.Text = jname;

                DropDownListMonthJA.SelectedValue = v.PublishJAMonth.ToString();

                TextBoxYearJA.Text = v.PublishJAYear;

                TextBoxImpFact.Text = v.ImpactFactor;
                TextBoxImpFact5.Text = v.ImpactFactor5;
                if (v.IFApplicableYear != 0)
                {
                    txtIFApplicable.Text = v.IFApplicableYear.ToString();
                }
                else
                {
                    txtIFApplicable.Text = "";
                }
                //txtCitation.Text = v.CitationUrl;

                // DropDownListPubType.SelectedValue=;
                if (v.PageFrom.Contains("PF"))
                {
                }
                else
                {
                    TextBoxPageFrom.Text = v.PageFrom;
                    TextBoxPageTo.Text = v.PageTo;
                }
                //TextBoxPageFrom.Text = v.PageFrom;

                TextBoxIssue.Text = v.Issue;

                //TextBoxPageTo.Text = v.PageTo;
                TextBoxVolume.Text = v.JAVolume;
                RadioButtonListIndexed.SelectedValue = v.Indexed;
              
            }
        else if (TypeEntry == "BK")
        {


            panelJournalArticle.Visible = false;
            panelBookPublish.Visible = true;
            panelConfPaper.Visible = false;
            panelOthes.Visible = false;

            TextBoxTitleBook.Text = v.TitleOfBook;

            TextBoxChapterContributed.Text = v.TitileOfChapter;

            TextBoxEdition.Text = v.Edition;

            panelTechReport.Visible = true;

            TextBoxPublisher.Text = v.Publisher;

            TextBoxYear.Text = v.BookPublishYear;
            DropDownListBookMonth.DataBind();
            if (v.BookPublishMonth != 0)
            {
                DropDownListBookMonth.SelectedValue = v.BookPublishMonth.ToString();
            }


            // DropDownListPubType.SelectedValue=;
            TextBoxPageNum.Text = v.BookPageNum;

            TextBoxVolume1.Text = v.BookVolume;
            txtbISBN.Text = v.ConfISBN;
            txtbSection.Text = v.BookSection;
            txtChapter.Text = v.BookChapter;
            txtCountry.Text = v.BookCountry;
            DropDownListBookPublicationType.SelectedValue = v.BookTypeofPublication;
            // CheckboxIndexAgency.ClearSelection();
        }
        else if (TypeEntry == "CP")
        {


            panelJournalArticle.Visible = false;
            panelBookPublish.Visible = false;
            panelConfPaper.Visible = true;
            panelOthes.Visible = false;

            TextBoxEventTitle.Text = v.ConferenceTitle;

            TextBoxPlace.Text = v.Place;

            TextBoxDate.Text = v.Date.ToShortDateString();
            if (v.TypePresentation != "")
            {
                RadioButtonListTypePresentaion.SelectedValue = v.TypePresentation;
            }
            TextBoxCreditPoint.Text = v.CreditPoint.ToString();
            TextBoxAwardedBy.Text = v.AwardedBy;
            TextBoxIsbn.Text = v.ConfISBN;
            TextBoxPageFromCP.Text = v.PageFrom;
            if (v.PageFrom != "")
            {
                TextBoxPageToCP.Enabled = true;
            }
            else
            {
                TextBoxPageToCP.Enabled = false;
            }
            TextBoxPageToCP.Text = v.PageTo;
            TextBoxFunds.Text = Convert.ToString(v.FundsUtilized);
            DropDownListMonthCP.Items.Clear();
            DropDownListMonthCP.Items.Add(new ListItem("--Select--", "0", true));
            DropDownListMonthCP.DataSourceID = "SqlDataSourceCPPubJAmonth";
            DropDownListMonthCP.DataBind();

            if (v.PublishJAMonth != 0)
            {
                DropDownListMonthCP.SelectedValue = v.PublishJAMonth.ToString();
            }
            TextBoxYearCP.Text = v.PublishJAYear;
            txtcity.Text = v.CPCity;
            txtState.Text = v.CPState;
            DropDownListConferencetype.SelectedValue = v.Publicationtype;
            RadioButtonListCPIndexed.ClearSelection();
            if (v.Indexed != "")
            {
                RadioButtonListCPIndexed.SelectedValue = v.Indexed;
            }
            // CheckboxIndexAgency.ClearSelection();
        }
        else if (TypeEntry == "NM")
        {


            panelJournalArticle.Visible = false;
            panelBookPublish.Visible = false;
            panelConfPaper.Visible = false;
            panelOthes.Visible = true;

            TextBoxPublish.Text = v.TitleWorkItem;

            TextBoxDateOfPublish.Text = v.NewsPublishedDate.ToShortDateString();

            TextBoxPageNumNewsPaper.Text = v.NewsPageNum;



            // CheckboxIndexAgency.ClearSelection();
        }
        else if (TypeEntry == "TS")
        {
            string jname = obj.fnfindjidgtjname(Pid, TypeEntry);


            panelJournalArticle.Visible = true;
            panelBookPublish.Visible = false;
            panelConfPaper.Visible = false;
            panelOthes.Visible = false;

            txtboxTitleOfWrkItem.Text = v.TitleWorkItem;

            TextBoxPubJournal.Text = v.PublisherOfJournal;

            TextBoxNameJournal.Text = jname;

            DropDownListMonthJA.SelectedValue = v.PublishJAMonth.ToString();

            TextBoxYearJA.Text = v.PublishJAYear;

            TextBoxIssue.Text = v.Issue;

            TextBoxImpFact.Text = v.ImpactFactor;
            TextBoxImpFact5.Text = v.ImpactFactor5;
            if (v.IFApplicableYear != 0)
            {
                txtIFApplicable.Text = v.IFApplicableYear.ToString();
            }
            else
            {
                txtIFApplicable.Text = "";
            }
            //txtCitation.Text = v.CitationUrl;
            // DropDownListPubType.SelectedValue=;
            if (v.PageFrom.Contains("PF"))
            {
            }
            else
            {
                TextBoxPageFrom.Text = v.PageFrom;
                TextBoxPageTo.Text = v.PageTo;
            }
            //TextBoxPageFrom.Text = v.PageFrom;

            // TextBoxPageTo.Text = v.PageTo;
            TextBoxVolume.Text = v.JAVolume;
            RadioButtonListIndexed.SelectedValue = v.Indexed;
            // CheckboxIndexAgency.ClearSelection();
        }
        panelTechReport.Visible = true;
        TextBoxDOINum.Text = v.DOINum;
       // DropDownListErf.SelectedValue = v.isERF;
        studentauthor.Value = v.IsStudentAuthor;

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




                drCurrentRow = dtCurrentTable.NewRow();

                DropdownMuNonMu.Text = dtCurrentTable.Rows[i - 1]["DropdownMuNonMu"].ToString();
                EmployeeCode.Text = dtCurrentTable.Rows[i - 1]["EmployeeCode"].ToString();
                AuthorName.Text = dtCurrentTable.Rows[i - 1]["AuthorName"].ToString();
                InstNme.Text = dtCurrentTable.Rows[i - 1]["InstitutionName"].ToString();
                deptname.Text = dtCurrentTable.Rows[i - 1]["DepartmentName"].ToString();
                InstId.Value = dtCurrentTable.Rows[i - 1]["Institution"].ToString();
                deptId.Value = dtCurrentTable.Rows[i - 1]["Department"].ToString();
                MailId.Text = dtCurrentTable.Rows[i - 1]["MailId"].ToString();
                AuthorType.Text = dtCurrentTable.Rows[i - 1]["AuthorType"].ToString();
                isCorrAuth.Text = dtCurrentTable.Rows[i - 1]["isCorrAuth"].ToString();
                NameAsInJournal.Text = dtCurrentTable.Rows[i - 1]["NameInJournal"].ToString();

                if (DropdownMuNonMu.Text == "M")
                {
                    NationalType.Visible = false;
                    ContinentId.Visible = false;
                }
                else if (DropdownMuNonMu.Text == "N")
                {
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
                    ContinentId.Enabled = false;
                    NationalType.Enabled = false;
                }
                else if (DropdownMuNonMu.Text == "E")
                {
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
                    ContinentId.Enabled = false;
                    NationalType.Enabled = false;
                }
                else if (DropdownMuNonMu.Text == "S" || DropdownMuNonMu.Text == "O")
                {
                    NationalType.Visible = false;
                    ContinentId.Visible = false;
                }
                else
                {
                }

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
                    MailId.Enabled = true;                          //Change -------------- Shilpa
                }

                if (DropDownListPublicationEntry.SelectedValue == "JA" || DropDownListPublicationEntry.SelectedValue == "TS" || DropDownListPublicationEntry.SelectedValue == "PR")
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
                else if (DropDownListPublicationEntry.SelectedValue == "BK")
                {
                    isCorrAuth.Visible = false;
                    AuthorType.Visible = true;
                    NameAsInJournal.Visible = false;
                    Grid_AuthorEntry.Columns[6].Visible = false;
                    Grid_AuthorEntry.Columns[7].Visible = true;
                    Grid_AuthorEntry.Columns[8].Visible = false;
                    Grid_AuthorEntry.Columns[9].Visible = false;
                    Grid_AuthorEntry.Columns[10].Visible = false;
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

                Grid_AuthorEntry.Columns[13].Visible = false;

                rowIndex++;

            }


            ViewState["CurrentTable"] = dtCurrentTable;
        }
        if (DropDownListPublicationEntry.SelectedValue == "CP")
        {
            CheckBoxListCPIndexedIn.DataBind();
            SqlDataSourceCheckboxCPIndexAgency.DataBind();
            if (RadioButtonListCPIndexed.SelectedValue == "Y")
            {

                CheckBoxListCPIndexedIn.Enabled = true;
                //lblmsgpubnonondexed.Visible = false;
                CheckBoxListCPIndexedIn.Enabled = true;

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
                        ListItem currentCheckBox = (ListItem)CheckBoxListCPIndexedIn.Items.FindByValue(IndexAgency);

                        if (currentCheckBox != null)
                            currentCheckBox.Selected = true;

                    }

                }
            }
            else
            {
                CheckBoxListCPIndexedIn.Enabled = false;
                CheckBoxListCPIndexedIn.Enabled = false;
                CheckBoxListCPIndexedIn.ClearSelection();
            }

        }
        if (DropDownListPublicationEntry.SelectedValue == "JA" || DropDownListPublicationEntry.SelectedValue == "TS" || DropDownListPublicationEntry.SelectedValue == "PR")
        {
            CheckboxIndexAgency.DataBind();
            SqlDataSourceCheckboxIndexAgency.DataBind();
            if (RadioButtonListIndexed.SelectedValue == "Y")
            {
                CheckboxIndexAgency.Enabled = true;

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
            }
        }
        // setModalWindow(sender, e);

        //ButtonSub.Enabled = false;
        btnSave.Enabled = true;


    }

    public void fnRecordExistApproval(object sender, EventArgs e)
    {
        
        string Pid = Session["TempPid"].ToString();
        string TypeEntry = Session["TempTypeEntry"].ToString();
        btnSave.Enabled = true;

        DSforgridview.SelectParameters.Clear();
        DSforgridview.SelectParameters.Add("PublicationID", Pid);
        DSforgridview.SelectParameters.Add("TypeOfEntry", TypeEntry);

        DSforgridview.SelectCommand = "select UploadPDFPath from Publication where PublicationID=@PublicationID and TypeOfEntry=@TypeOfEntry";
        DSforgridview.DataBind();
        GVViewFile.DataBind();
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


        //FileUploadPdf.Visible = true;
        LabelUploadPfd.Visible = true;
        Business obj = new Business();
        PublishData v = new PublishData();
        v = obj.fnfindjid(Pid, TypeEntry);


        DropDownListMuCategory.SelectedValue = v.MUCategorization;
        DropDownListPublicationEntry.SelectedValue = TypeEntry;
        DropDownListPublicationEntry.Enabled = false;
        TextBoxPubId.Text = Pid;
        txtboxTitleOfWrkItem.Text = v.TitleWorkItem;
        // v = obj.fnfindjid(Pid, TypeEntry);
        if (TypeEntry == "JA")
        {
            string jname = obj.fnfindjidgtjname(Pid, TypeEntry);
            panelJournalArticle.Visible = true;
            panelBookPublish.Visible = false;
            panelConfPaper.Visible = false;
            panelOthes.Visible = false;

            txtboxTitleOfWrkItem.Text = v.TitleWorkItem;

            TextBoxPubJournal.Text = v.PublisherOfJournal;

            TextBoxNameJournal.Text = jname;

            DropDownListMonthJA.SelectedValue = v.PublishJAMonth.ToString();

            TextBoxYearJA.Text = v.PublishJAYear;

            TextBoxImpFact.Text = v.ImpactFactor;
            TextBoxImpFact5.Text = v.ImpactFactor5;
           // txtCitation.Text = v.CitationUrl;
            TextBoxIssue.Text = v.Issue;

            // DropDownListPubType.SelectedValue=;
            if (v.PageFrom.Contains("PF"))
            {
            }
            else
            {
                TextBoxPageFrom.Text = v.PageFrom;
                TextBoxPageTo.Text = v.PageTo;
            }
            // TextBoxPageFrom.Text = v.PageFrom;

            //TextBoxPageTo.Text = v.PageTo;
            TextBoxVolume.Text = v.JAVolume;
            RadioButtonListIndexed.SelectedValue = v.Indexed;
            // CheckboxIndexAgency.ClearSelection();
        }
        else if (TypeEntry == "PR")
        {
            string jname = obj.fnfindjidgtPname(Pid, TypeEntry);
            panelJournalArticle.Visible = true;
            panelBookPublish.Visible = false;
            panelConfPaper.Visible = false;
            panelOthes.Visible = false;

            txtboxTitleOfWrkItem.Text = v.TitleWorkItem;

            TextBoxPubJournal.Text = v.PublisherOfJournal;

            TextBoxNameJournal.Text = jname;

            DropDownListMonthJA.SelectedValue = v.PublishJAMonth.ToString();

            TextBoxYearJA.Text = v.PublishJAYear;

            TextBoxImpFact.Text = v.ImpactFactor;
            TextBoxImpFact5.Text = v.ImpactFactor5;
            // txtCitation.Text = v.CitationUrl;
            TextBoxIssue.Text = v.Issue;

            // DropDownListPubType.SelectedValue=;
            if (v.PageFrom.Contains("PF"))
            {
            }
            else
            {
                TextBoxPageFrom.Text = v.PageFrom;
                TextBoxPageTo.Text = v.PageTo;
            }
            // TextBoxPageFrom.Text = v.PageFrom;

            //TextBoxPageTo.Text = v.PageTo;
            TextBoxVolume.Text = v.JAVolume;
            RadioButtonListIndexed.SelectedValue = v.Indexed;
            // CheckboxIndexAgency.ClearSelection();
        }
        else if (TypeEntry == "BK")
        {


            panelJournalArticle.Visible = false;
            panelBookPublish.Visible = true;
            panelConfPaper.Visible = false;
            panelOthes.Visible = false;

            TextBoxTitleBook.Text = v.TitleOfBook;

            TextBoxChapterContributed.Text = v.TitileOfChapter;

            TextBoxEdition.Text = v.Edition;



            TextBoxPublisher.Text = v.Publisher;

            TextBoxYear.Text = v.PublishJAYear;

            // DropDownListPubType.SelectedValue=;
            TextBoxPageNum.Text = v.PageNum;

            TextBoxVolume1.Text = v.Volume;

            // CheckboxIndexAgency.ClearSelection();
        }
        else if (TypeEntry == "CP")
        {


            panelJournalArticle.Visible = false;
            panelBookPublish.Visible = false;
            panelConfPaper.Visible = true;
            panelOthes.Visible = false;

            TextBoxEventTitle.Text = v.ConferenceTitle;

            TextBoxPlace.Text = v.Place;

            TextBoxDate.Text = v.NewsPublishedDate.ToShortDateString();


            // CheckboxIndexAgency.ClearSelection();
        }
        else if (TypeEntry == "NM")
        {


            panelJournalArticle.Visible = false;
            panelBookPublish.Visible = false;
            panelConfPaper.Visible = false;
            panelOthes.Visible = true;

            TextBoxPublish.Text = v.TitleWorkItem;

            TextBoxDateOfPublish.Text = v.NewsPublishedDate.ToShortDateString();
            TextBoxPageNumNewsPaper.Text = v.NewsPageNum;


            // CheckboxIndexAgency.ClearSelection();
        }
        else if (TypeEntry == "TS")
        {

            string jname = obj.fnfindjidgtjname(Pid, TypeEntry);

            panelJournalArticle.Visible = true;
            panelBookPublish.Visible = false;
            panelConfPaper.Visible = false;
            panelOthes.Visible = false;

            txtboxTitleOfWrkItem.Text = v.TitleWorkItem;

            TextBoxPubJournal.Text = v.PublisherOfJournal;

            TextBoxNameJournal.Text = jname;

            DropDownListMonthJA.SelectedValue = v.PublishJAMonth.ToString();

            TextBoxYearJA.Text = v.PublishJAYear;

            TextBoxImpFact.Text = v.ImpactFactor;
            TextBoxImpFact5.Text = v.ImpactFactor5;
           // txtCitation.Text = v.CitationUrl;
            // DropDownListPubType.SelectedValue=;
            if (v.PageFrom.Contains("PF"))
            {
            }
            else
            {
                TextBoxPageFrom.Text = v.PageFrom;
                TextBoxPageTo.Text = v.PageTo;
            }
            // TextBoxPageFrom.Text = v.PageFrom;

            TextBoxIssue.Text = v.Issue;

            // TextBoxPageTo.Text = v.PageTo;
            TextBoxVolume.Text = v.JAVolume;
            RadioButtonListIndexed.SelectedValue = v.Indexed;
            // CheckboxIndexAgency.ClearSelection();
        }
        panelTechReport.Visible = true;

        TextBoxDOINum.Text = v.DOINum;
        //DropDownListErf.SelectedValue = v.isERF;
        //TextBoxKeywords.Text = v.Keywords;


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
                TextBox InstNme = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[2].FindControl("InstitutionName");
                TextBox deptname = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[2].FindControl("DepartmentName");
                HiddenField InstId = (HiddenField)Grid_AuthorEntry.Rows[rowIndex].Cells[2].FindControl("Institution");
                HiddenField deptId = (HiddenField)Grid_AuthorEntry.Rows[rowIndex].Cells[2].FindControl("Department");
                TextBox MailId = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[2].FindControl("MailId");
                DropDownList isCorrAuth = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[2].FindControl("isCorrAuth");
                DropDownList AuthorType = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[2].FindControl("AuthorType");
                TextBox NameAsInJournal = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[2].FindControl("NameInJournal");



                drCurrentRow = dtCurrentTable.NewRow();

                DropdownMuNonMu.Text = dtCurrentTable.Rows[i - 1]["DropdownMuNonMu"].ToString();
                EmployeeCode.Text = dtCurrentTable.Rows[i - 1]["EmployeeCode"].ToString();
                AuthorName.Text = dtCurrentTable.Rows[i - 1]["AuthorName"].ToString();
                InstNme.Text = dtCurrentTable.Rows[i - 1]["InstitutionName"].ToString();
                deptname.Text = dtCurrentTable.Rows[i - 1]["DepartmentName"].ToString();
                InstId.Value = dtCurrentTable.Rows[i - 1]["Institution"].ToString();
                deptId.Value = dtCurrentTable.Rows[i - 1]["Department"].ToString();
                MailId.Text = dtCurrentTable.Rows[i - 1]["MailId"].ToString();
                AuthorType.Text = dtCurrentTable.Rows[i - 1]["AuthorType"].ToString();
                isCorrAuth.Text = dtCurrentTable.Rows[i - 1]["isCorrAuth"].ToString();
                NameAsInJournal.Text = dtCurrentTable.Rows[i - 1]["NameInJournal"].ToString();


                if (DropDownListPublicationEntry.SelectedValue == "JA" || DropDownListPublicationEntry.SelectedValue == "TS" || DropDownListPublicationEntry.SelectedValue == "PR")
                {
                    isCorrAuth.Visible = true;
                    AuthorType.Visible = true;
                    NameAsInJournal.Visible = true;
                    Grid_AuthorEntry.Columns[6].Visible = true;

                    Grid_AuthorEntry.Columns[7].Visible = true;
                    Grid_AuthorEntry.Columns[8].Visible = true;
                }
                else
                {
                    isCorrAuth.Visible = false;
                    AuthorType.Visible = false;
                    NameAsInJournal.Visible = false;
                    Grid_AuthorEntry.Columns[6].Visible = false;
                    Grid_AuthorEntry.Columns[7].Visible = false;
                    Grid_AuthorEntry.Columns[8].Visible = false;
                }

                Grid_AuthorEntry.Columns[11].Visible = false;

                rowIndex++;

            }


            ViewState["CurrentTable"] = dtCurrentTable;
        }
        CheckboxIndexAgency.DataBind();
        SqlDataSourceCheckboxIndexAgency.DataBind();
        if (RadioButtonListIndexed.SelectedValue == "Y")
        {
            CheckboxIndexAgency.Enabled = true;

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
        }

        btnSave.Enabled = false;
    }




}