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
using System;
using System.Data.SqlClient;
using System.Net;
using System.Drawing;
using System.Runtime.InteropServices;

public partial class Publicationentry : System.Web.UI.Page
{
    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    //string mainpath = ConfigurationManager.AppSettings["PdfPath"].ToString();
    Business B = new Business();
    Journal_DataObject JournalDataObj = new Journal_DataObject();
    JournalData JournalValueObj = new JournalData();
    public string pageID = "L47";
    protected void Page_Load(object sender, EventArgs e)
    {      //popupPanelJournal.Visible = true;
        if (!IsPostBack)
        {

            lblmsg.Visible = false;
            //if (!Session["authPage"].ToString().Contains("$" + pageID + "$"))
            //{
            //    string unacces = "Unauthorized Acces!!! Login Again";
            //    Response.Redirect("~/Login.aspx?val=" + unacces);
            //}
            dataBind();
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
            //path sent to display.aspx page
            Response.Write("</script>");
        }

        //to download the pdf

        //WebClient client = new WebClient();
        //    Byte[] buffer = client.DownloadData(path);
        //    if (buffer != null)
        //    {
        //        Response.AddHeader("content-disposition", "attachment; filename=Publication.pdf");
        //    }

        //    Response.AddHeader("content-length", buffer.Length.ToString());
        //    Response.BinaryWrite(buffer);

    }
    protected void ButtonSearchPubOnClick(object sender, EventArgs e)
    {
        GridViewSearch.Visible = true;
        GridViewSearch.EditIndex = -1;
        addclik(sender, e);
        dataBind();
        lblmsg.Visible = false;

    }
    private void dataBind()
    {
        panel.Visible = false;

        GridViewSearch.Visible = true;
        Business b = new Business();
        User a1 = new User();
        string user = Session["UserId"].ToString();
        string inst = Session["InstituteId"].ToString();
        string dept = Session["Department"].ToString();
        string email = Session["emailId"].ToString();

        a1 = b.GetPublicationInchargeInstLibraryMap(inst, dept,email);

        PublishData obj = new PublishData();
        obj.PaublicationID = PubIDSearch.Text;
        obj.TypeOfEntry = EntryTypesearch.SelectedValue;
        obj.TitleWorkItem = TextBoxWorkItemSearch.Text;
        obj.LibraryId = a1.LibraryId;
        DataSet ds = new DataSet();
        ds = B.BindGridview(obj);
        GridViewSearch.DataSource = ds;
        GridViewSearch.DataBind();
        //if (EntryTypesearch.SelectedValue != "A")
        //{
        //    if (PubIDSearch.Text == "")
        //    {
        //        //SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName ,t.EntryName,TitleWorkItem,c.PubCatName from Publication p,Status_Publication_M s, PubMUCategorization_M c ,PublicationTypeEntry_M t  where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization  and status!='CAN' and TypeOfEntry='" + EntryTypesearch.SelectedValue + "'  and Institution='" + a1.InstituteId + "' ";

        //        SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,Status,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName,t.EntryName,TitleWorkItem,c.PubCatName from Publication p,Status_Publication_M s, PubMUCategorization_M c ,PublicationTypeEntry_M t where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization and TypeOfEntry='" + EntryTypesearch.SelectedValue + "' and  Status='APP' and LibraryId='" + a1.LibraryId + "' and uploadEPrint='N' ";
        //    }
        //    else
        //    {

        //        SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,Status,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName,t.EntryName,TitleWorkItem,c.PubCatName from Publication p,Status_Publication_M s, PubMUCategorization_M c ,PublicationTypeEntry_M t where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization and  TypeOfEntry='" + EntryTypesearch.SelectedValue + "' and  Status='APP'    and PublicationID like '%" + PubIDSearch.Text.Trim() + "%' and LibraryId='" + a1.LibraryId + "' and uploadEPrint='N'";
        //        // SqlDataSource1.SelectCommand = "select j.Id as Id,Year,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,Comments from Journal_IF_Details j,Journal_M k where j.Id=k.Id and j.Id='" + txtJid.Text.Trim() + "'";
        //    }
        //    GridViewSearch.DataBind();
        //    SqlDataSource1.DataBind();
        //}
        //else
        //{
        //    if (PubIDSearch.Text == "")
        //    {
        //        SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,Status,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName,t.EntryName,TitleWorkItem,c.PubCatName from Publication p,Status_Publication_M s, PubMUCategorization_M c ,PublicationTypeEntry_M t where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization and   Status='APP' and LibraryId='" + a1.LibraryId + "'  and uploadEPrint='N'";
        //    }
        //    else
        //    {

        //        SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,Status,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName,t.EntryName,TitleWorkItem,c.PubCatName from Publication p,Status_Publication_M s, PubMUCategorization_M c ,PublicationTypeEntry_M t where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization and  Status='APP'    and PublicationID like '%" + PubIDSearch.Text.Trim() + "%' and LibraryId='" + a1.LibraryId + "' and uploadEPrint='N'";
        //        // SqlDataSource1.SelectCommand = "select j.Id as Id,Year,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,Comments from Journal_IF_Details j,Journal_M k where j.Id=k.Id and j.Id='" + txtJid.Text.Trim() + "'";
        //    }
        //    GridViewSearch.DataBind();
        //    SqlDataSource1.DataBind();
        //}
    }

    //protected void MuOrNonMUOnSelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (MuOrNonMU.SelectedValue == "M")
    //    {
    //        PanelMU.Visible = true;
    //        BtnAddMU.Visible = true;
    //        BtnAddNonMU.Visible = false;
    //        PanelNonMu.Visible = false;
    //    }
    //    else
    //    {
    //        PanelMU.Visible = false;
    //        BtnAddMU.Visible = false;
    //        BtnAddNonMU.Visible = true;
    //        PanelNonMu.Visible = true;
    //    }

    //}

    protected void setModalWindow(object sender, EventArgs e)
    {
        popupPanelJournal.Visible = true;
        popGridJournal.DataSourceID = "SqlDataSourceJournal";
        SqlDataSourceJournal.DataBind();
        popGridJournal.DataBind();

        popupPanelAffil.Visible = true;
        popGridAffil.DataSourceID = "SqlDataSourceAffil";
        SqlDataSourceAffil.DataBind();
        popGridAffil.DataBind();

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

        string year = DateTime.Now.Year.ToString();
        int Jyear = Convert.ToInt32(year) - 1;
        // txtboxYear.Text = Jyear.ToString();


        affiliateSrch.Text = "";
        popGridAffil.DataBind();

        journalcodeSrch.Text = "";
        popGridJournal.DataBind();


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


        TextBox AuthorName = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("AuthorName");
        AuthorName.Text = a.UserName;

        journalcodeSrch.Text = "";
        popGridJournal.DataBind();


        TextBox NameInJournal = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("NameInJournal");
        NameInJournal.Text = a.UserName;

        affiliateSrch.Text = "";
        popGridAffil.DataBind();

    }

    /* Pop--JournalCodeChanged */
    protected void JournalCodeChanged(object sender, EventArgs e)
    {

        if (journalcodeSrch.Text.Trim() == "")
        {
            SqlDataSourceJournal.SelectCommand = "SELECT  Id,Title,AbbreviatedTitle FROM [Journal_M]";
            popGridJournal.DataBind();
            popGridJournal.Visible = true;
        }

        else
        {
            SqlDataSourceJournal.SelectParameters.Clear();
            SqlDataSourceJournal.SelectParameters.Add("Title", journalcodeSrch.Text);

            SqlDataSourceJournal.SelectCommand = "SELECT Id,Title,AbbreviatedTitle FROM [Journal_M] where Title like '%' + @Title + '%'";
            popGridJournal.DataBind();
            popGridJournal.Visible = true;
        }

        popupPanelAffil.Visible = false;

        ModalPopupExtender1.Show();
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

            if (DropDownListPublicationEntry.SelectedValue == "BK" || DropDownListPublicationEntry.SelectedValue == "NM")
            {


                SqlDataSourceAuthorType.SelectCommand = "SELECT Id,Type FROM [Author_Type_M] where (Id! = 'O') and (Id! = 'E')";

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
            SqlDataSourceAuthorType.SelectCommand = "SELECT Id,DisplayName FROM [Author_Type_M] where (Id = 'M') or (Id = 'S') or (Id = 'N') or (Id = 'O') or  (Id = 'E')";

            DropdownMuNonMu.DataTextField = "DisplayName";
            DropdownMuNonMu.DataValueField = "Id";
            DropdownMuNonMu.DataBind();

        }

        // Author.Text = Session["User"].ToString();
        //  Author.Enabled = false;
        AuthorName.Text = Session["UserName"].ToString();
        AuthorName.Enabled = false;
        EmployeeCode.Text = Session["UserId"].ToString();
        //EmployeeCode.Enabled = false;

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
        //  Department.Enabled = false;
        // string dir_domain = ConfigurationManager.AppSettings["DirectoryDomain"].ToString();
        MailId.Text = Session["emailId"].ToString();
        MailId.Enabled = false;
        //isCorrAuth.Text = "";
        NameInJournal.Text = Session["UserName"].ToString();
        // NameInJournal.Enabled = false;

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
        //Grid_AuthorEntry.DataBind();





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
                        // TextBox Author = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("Author");
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

                        //   dtCurrentTable.Rows[i - 1]["amount"] = amount.Text.Trim() == "" ? 0 : Convert.ToDecimal(amount.Text);
                        dtCurrentTable.Rows[i - 1]["DropdownMuNonMu"] = DropdownMuNonMu.Text;
                        // dtCurrentTable.Rows[i - 1]["Author"] = Author.Text;
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

        // setModalWindow(sender, e); // initialise popup gridviews

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

                    //  TextBox Author1 = (TextBox)Grid_AuthorEntry.Rows[0].Cells[0].FindControl("Author");
                    TextBox AuthorName1 = (TextBox)Grid_AuthorEntry.Rows[0].Cells[1].FindControl("AuthorName");
                    DropDownList DropdownMuNonMu1 = (DropDownList)Grid_AuthorEntry.Rows[0].Cells[2].FindControl("DropdownMuNonMu");
                    //TextBox amount = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[3].FindControl("amount");
                    HiddenField EmployeeCode1 = (HiddenField)Grid_AuthorEntry.Rows[0].Cells[0].FindControl("EmployeeCode");
                    HiddenField Institution1 = (HiddenField)Grid_AuthorEntry.Rows[0].Cells[0].FindControl("Institution");
                    TextBox InstitutionName1 = (TextBox)Grid_AuthorEntry.Rows[0].Cells[6].FindControl("InstitutionName");
                    HiddenField Department1 = (HiddenField)Grid_AuthorEntry.Rows[0].Cells[0].FindControl("Department");
                    TextBox DepartmentName1 = (TextBox)Grid_AuthorEntry.Rows[0].Cells[0].FindControl("DepartmentName");
                    TextBox MailId1 = (TextBox)Grid_AuthorEntry.Rows[0].Cells[0].FindControl("MailId");

                    DropDownList isCorrAuth1 = (DropDownList)Grid_AuthorEntry.Rows[0].Cells[0].FindControl("isCorrAuth");

                    DropDownList AuthorType1 = (DropDownList)Grid_AuthorEntry.Rows[0].Cells[0].FindControl("AuthorType");
                    TextBox NameInJournal1 = (TextBox)Grid_AuthorEntry.Rows[0].Cells[0].FindControl("NameInJournal");

                    //  Author1.Enabled = false;
                    AuthorName1.Enabled = false;
                    DropdownMuNonMu1.Enabled = false;
                    // EmployeeCode1.Enabled = false;
                    //  Institution1.Enabled = false;
                    InstitutionName1.Enabled = false;

                    //  Department1.Enabled = false;
                    DepartmentName1.Enabled = false;
                    MailId1.Enabled = false;
                    //  isCorrAuth1.Enabled = false;
                    // AuthorType1.Enabled = false;
                    // NameInJournal1.Enabled = false;

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

                    //  amount.Text = dt.Rows[i]["amount"].ToString();
                    DropdownMuNonMu.Text = dt.Rows[i]["DropdownMuNonMu"].ToString();
                    //  Author.Text = dt.Rows[i]["Author"].ToString();
                    AuthorName.Text = dt.Rows[i]["AuthorName"].ToString();
                    EmployeeCode.Text = dt.Rows[i]["EmployeeCode"].ToString();
                    Institution.Value = dt.Rows[i]["Institution"].ToString();
                    InstitutionName.Text = dt.Rows[i]["InstitutionName"].ToString();
                    Department.Value = dt.Rows[i]["Department"].ToString();
                    DepartmentName.Text = dt.Rows[i]["DepartmentName"].ToString();
                    MailId.Text = dt.Rows[i]["MailId"].ToString();
                    isCorrAuth.Text = dt.Rows[i]["isCorrAuth"].ToString();
                    AuthorType.Text = dt.Rows[i]["AuthorType"].ToString();
                    NameInJournal.Text = dt.Rows[i]["NameInJournal"].ToString();

                    if (rowIndex == 0)
                    {
                        Grid_AuthorEntry.Columns[11].Visible = false;
                        // Grid_AuthorEntry.Rows[rowIndex].Cells[11].Visible = false;
                    }
                    else
                    {
                        Grid_AuthorEntry.Columns[11].Visible = true;
                        // Grid_AuthorEntry.Rows[rowIndex].Cells[11].Visible = true;
                    }

                    rowIndex++;
                }
            }
        }
    }




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
                // gridAmtChanged(sender, e);
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
    protected void TextBoxPageFromCP_TextChanged(object sender, EventArgs e)
    {
        if (TextBoxPageFromCP.Text != "")
        {
            TextBoxPageToCP.Enabled = true;
            RequiredFieldValidator1.Enabled = true;
        }
        else
        {
            TextBoxPageToCP.Enabled = false;
            RequiredFieldValidator1.Enabled = false;
        }
    }
    protected void showPop(object sender, EventArgs e)
    {
        popupPanelJournal.Visible = true;
        ModalPopupExtender1.Show();
        popupPanelAffil.Visible = false;
    }

    protected void DropDownListPublicationEntryOnSelectedIndexChanged(object sender, EventArgs e)
    {
        //setModalWindow(sender, e);

        popupPanelAffil.Visible = true;
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
        else if (DropDownListPublicationEntry.SelectedValue == "PR")
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

    //protected void BtnSubmit_Click(object sender, EventArgs e)
    //{ //sending mail to Buyers
    //    // ButtonSub.Enabled = false;
    //    //FileSave(sender, e);
    //    Business b = new Business();
    //    PublishData j = new PublishData();
    //    string PublicationEntry = DropDownListPublicationEntry.SelectedValue;
    //    int savedfileflag = 0;
    //    string filelocationpath = "";
    //    string UploadPdf = "";

    //    string FileUpload = "";

    //    if (TextBoxPubId.Text != "")
    //    {


    //        FileUpload = b.GetFileUploadPath(TextBoxPubId.Text, PublicationEntry);
    //        j.FilePathNew = FileUpload;
    //        if (Directory.Exists(mainpath))
    //        {
    //            if (FileUploadPdf.HasFile)
    //            {
    //                //if (FileUpload != "")
    //                //{
    //                //    ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('File is Already uploaded!')</script>");

    //                //    return;
    //                //}
    //            }
    //            else
    //            {
    //                if (FileUpload == "")
    //                {
    //                    ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please upload the file before submission!')</script>");

    //                    return;

    //                }
    //            }
    //        }


    //    }
    //    if (Directory.Exists(mainpath))
    //    {
    //        if (FileUploadPdf.HasFile)
    //        {
    //            string uploadedfilename = Path.GetFileName(FileUploadPdf.PostedFile.FileName);
    //            double size = FileUploadPdf.PostedFile.ContentLength;

    //            if (size < 4194304) //4mb
    //            {
    //                string path_BoxId = Path.Combine(mainpath, TextBoxPubId.Text); //main path + location
    //                if (!Directory.Exists(path_BoxId))   //if directory doesnt exist
    //                {
    //                    Directory.CreateDirectory(path_BoxId);//crete directory of location
    //                }
    //                string uploadedfilename1 = Path.GetFileName(FileUploadPdf.PostedFile.FileName);
    //                string timestamp = DateTime.Now.ToString("dd-MM-yy-hh-mm-ss");
    //                string fileextension = Path.GetExtension(uploadedfilename);
    //                string actualfilenamewithtime = PublicationEntry + "_" + timestamp + fileextension;
    //                UploadPdf = actualfilenamewithtime;
    //                filelocationpath = Path.Combine(path_BoxId, actualfilenamewithtime);
    //                FileUploadPdf.SaveAs(filelocationpath);  //saving file


    //                //j.FilePath = filelocationpath;
    //                //j.PaublicationID = TextBoxPubId.Text.Trim();
    //                //j.TypeOfEntry = DropDownListPublicationEntry.SelectedValue;

    //                //j.ApprovedBy = Session["User"].ToString();
    //                //j.ApprovedDate = DateTime.Now;

    //                //j.AutoAppoval = Session["AutoApproval"].ToString();

    //                //if (FileUpload == "")
    //                //{
    //                //    j.FilePath = filelocationpath;
    //                //}
    //                //else
    //                //{
    //                //    j.FilePath = j.FilePathNew;
    //                //}
    //                //if (File.Exists(filelocationpath))//checking whther file is saved or not?
    //                //{
    //                //    savedfileflag = 1;
    //                //}

    //                //if (savedfileflag == 1) //if saved then path is stored in database
    //                //{
    //                //    int result = B.UpdatePfPath(j);
    //                //}

    //            }
    //        }


    //    }
    //    j.FilePath = filelocationpath;
    //    j.PaublicationID = TextBoxPubId.Text.Trim();
    //    j.TypeOfEntry = DropDownListPublicationEntry.SelectedValue;

    //    j.ApprovedBy = Session["UserId"].ToString();
    //    j.ApprovedDate = DateTime.Now;

    //    // j.AutoAppoval = Session["AutoApproval"].ToString();

    //    if (FileUpload == "")
    //    {
    //        j.FilePath = filelocationpath;
    //    }
    //    else
    //    {
    //        j.FilePath = j.FilePathNew;
    //    }

    //    int result = B.UpdatePfPath(j);

    //    DSforgridview.SelectCommand = "select UploadPDFPath from Publication where PublicationID='" + j.PaublicationID + "' and TypeOfEntry='" + j.TypeOfEntry + "'";
    //    DSforgridview.DataBind();
    //    GVViewFile.DataBind();
    //    string FileUpload1 = "";
    //    Business b1 = new Business();
    //    FileUpload1 = b1.GetFileUploadPath(j.PaublicationID, j.TypeOfEntry);
    //    if (FileUpload1 != "")
    //    {
    //        GVViewFile.Visible = true;
    //    }
    //    else
    //    {
    //        GVViewFile.Visible = false;
    //    }

    //    ArrayList myArrayList = new ArrayList();

    //    ArrayList myArrayList1 = new ArrayList();

    //    //  ArrayList myArrayListMAilsend= new ArrayList();
    //    DataSet ds = new DataSet();
    //    Business bus = new Business();

    //    ds = bus.getAuthorList(TextBoxPubId.Text, DropDownListPublicationEntry.SelectedValue);

    //    // foreach loop to read each DataRow of DataTable stored inside the DataSet

    //    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
    //    {
    //        myArrayList.Add(ds.Tables[0].Rows[i]["EmailId"].ToString());
    //    }
    //    string Supervisor = null;
    //    for (int i = 0; i < myArrayList.Count; i++)
    //    {
    //        string author = myArrayList[i].ToString();
    //        Supervisor = b.GetAuthorsSupervisor(author);
    //        if (Supervisor != null)
    //        {
    //            myArrayList1.Add(Supervisor);
    //        }
    //    }


    //    // string dir_domain = ConfigurationManager.AppSettings["DirectoryDomain"].ToString();
    //    try
    //    {
    //        log.Debug(" function--- Before Mail Sending");
    //        MailMessage Msg = new MailMessage();
    //        System.Net.Mail.SmtpClient spcl = new System.Net.Mail.SmtpClient();
    //        Msg.Subject = "Publication Id  :" + TextBoxPubId.Text + " Entry Type  :" + DropDownListPublicationEntry.SelectedValue + " Submitted. Please Approve.";



    //        Msg.Body = "<span style=\"font-size: 10pt; color: #3300cc; font-family: Verdana\"><h4>Dear Sir/Madam,</h4> <br><b> PublicationId  :  " + TextBoxPubId.Text + " Entry Type  :" + DropDownListPublicationEntry.SelectedValue + " Title of Work Item  :" + txtboxTitleOfWrkItem.Text + " </b><br><b> </b></span>";

    //        Msg.Priority = MailPriority.Normal;
    //        Msg.IsBodyHtml = true;

    //        string frmEmail = ConfigurationManager.AppSettings["FromAddress"].ToString();
    //        Msg.From = new MailAddress(frmEmail);

    //        for (int i = 0; i < myArrayList.Count; i++)
    //        {
    //            // Msg.To.Add(BuyerId_Array[0]+dir_domain);
    //            Msg.To.Add(myArrayList[i].ToString());
    //            log.Info(" Email will be sent to authors '" + i + "' : '" + myArrayList[i] + "' ");
    //        }

    //        for (int i = 0; i < myArrayList1.Count; i++)
    //        {
    //            // Msg.To.Add(BuyerId_Array[0]+dir_domain);
    //            Msg.To.Add(myArrayList1[i].ToString());
    //            log.Info(" Email will be sent to supervisors '" + i + "' : '" + myArrayList1[i] + "' ");
    //        }
    //        spcl.Host = ConfigurationManager.AppSettings["MailHost"].ToString();
    //        spcl.Send(Msg);

    //    }
    //    catch (Exception ex)
    //    {

    //        // ButtonSub.Enabled = false;
    //        log.Error(ex.StackTrace);
    //        log.Error(ex.Message);
    //        ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Publication Submitted Successfully, But Problem in Sending Mail!')</script>");
    //        GridViewSearch.Visible = false;
    //    }
    //}
    protected void BtnSave_Click(object sender, EventArgs e)
    {


        if (!Page.IsValid)
        {
            return;
        }
        int countAuthType = 0;
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
                //if (Directory.Exists(mainpath))
                //{
                    if (FileUploadPdf.HasFile)
                    {
                        //if (FileUpload != "")
                        //{
                        //    ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('File is Already uploaded!')</script>");

                        //    return;
                        //}
                    }
                }
            //}

            string MUCategorization = DropDownListMuCategory.SelectedValue;

            string TitleWorkItem = txtboxTitleOfWrkItem.Text.Trim();

            //  string CoAuthor = TextBoxCoAuthor.Text;
            //  string CorrespondingAuthor = TextBoxcorrespondingAuthor.Text;
            string PubJournal = TextBoxPubJournal.Text;

            string NameJournal = TextBoxNameJournal.Text.Trim();

            string Volume = TextBoxVolume.Text;
            string JAMonthJA = DropDownListMonthJA.SelectedValue;

            string YearJA = TextBoxYearJA.Text;
            string PageFrom = TextBoxPageFrom.Text;

            string PageTo = TextBoxPageTo.Text;

            string Indexed = RadioButtonListIndexed.SelectedValue;

            string IndexAgency = CheckboxIndexAgency.SelectedValue;

            string PubType = DropDownListPubType.SelectedValue;

            string ImpFact = TextBoxImpFact.Text;
            string ImpFact5 = TextBoxImpFact5.Text;
            //string CitationUrl = txtCitation.Text;

            string EventTitle = TextBoxEventTitle.Text;

            string Place = TextBoxPlace.Text;

            string Date = TextBoxDate.Text;

            string TitleBook = TextBoxTitleBook.Text;
            //string ofURL = TextBoxURL.Text.Trim();


            string ChapterContributed = TextBoxChapterContributed.Text;

            string Edition = TextBoxEdition.Text;

            string Publisher = TextBoxPublisher.Text;

            string Year = TextBoxYearJA.Text;


            string PageNum = TextBoxPageNum.Text;

            string Volume1 = TextBoxVolume1.Text;


            string Publish = TextBoxPublish.Text;


            string DateOfPublish = TextBoxDateOfPublish.Text;

            string PageNumNewsPaper = TextBoxPageNumNewsPaper.Text;


            string DOINum = TextBoxDOINum.Text;

            string Keywords = TextBoxKeywords.Text;

            string Abstract = TextBoxAbstract.Text;

            // string Reference = TextBoxReference.Text;


            string UploadPdf = "";

            string Status = "NEW";
            string isERF = DropDownListErf.SelectedValue;

            j.TypeOfEntry = PublicationEntry;
            j.MUCategorization = MUCategorization;

            j.TitleWorkItem = TitleWorkItem;
            string inst = Session["InstituteId"].ToString();

            string dept = Session["Department"].ToString();
            //   j.CorrespondingAuthor = CorrespondingAuthor;

            j.PublisherOfJournal = PubJournal;

            j.NameOfJournal = NameJournal;
            j.Volume = Volume;
            j.MonthJA = JAMonthJA;
            j.JAVolume = Volume;

            j.PublishJAMonth = Convert.ToInt16(JAMonthJA);
            j.PublishJAYear = Year;
            j.PageFrom = PageFrom;
            j.PageTo = PageTo;

            j.Indexed = Indexed;
            j.IndexedIn = IndexAgency;
            j.Publicationtype = PubType;

            j.ImpactFactor = ImpFact;


            j.ConferenceTitle = EventTitle;
            j.Place = Place;
            if (TextBoxDate.Text != "")
            {
                j.Date = Convert.ToDateTime(Date);
            }
            j.TitleOfBook = TitleBook;
            j.Edition = Edition;
            j.Publisher = Publish;
            j.Year = Year;
            j.PageNum = PageNum;

            j.BookVolume = Volume1;
            //j.url = ofURL;
            j.DOINum = DOINum;
            j.Keywords = Keywords;
            j.Abstract = Abstract;
            // j.TechReferences = Reference;
            j.UploadPDF = UploadPdf;
            j.Status = Status;
            j.CreatedBy = Session["UserId"].ToString();
            j.CreatedDate = DateTime.Now;
            j.ApprovedBy = Session["UserId"].ToString();
            j.ApprovedDate = DateTime.Now;
            j.isERF = DropDownListErf.SelectedValue;
            j.TitileOfChapter = "";
            //  j.SupervisorID = Session["SupervisorId"].ToString();
            //  j.AutoAppoval = Session["AutoApproval"].ToString();

            j.uploadEPrint = DropDownListuploadEPrint.SelectedValue;

            j.EprintURL = TextBoxEprintURL.Text;

            string SupId = null;
            SupId = b.GetSupId(inst, Session["UserId"].ToString(), Session["Department"].ToString());
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


            for (int i = 0; i < CheckboxIndexAgency.Items.Count; i++)
            {
                if (CheckboxIndexAgency.Items[i].Selected)
                {
                    listIndexAgency.Add(CheckboxIndexAgency.Items[i].Value);
                }
            }


            if (RadioButtonListIndexed.SelectedValue == "Y")
            {
                if (listIndexAgency.Count == 0)
                {
                    ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please select the index agency!')</script>");
                    return;

                }
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



                    JD[i].AuthorName = AuthorName.Text.Trim();
                    JD[i].MUNonMU = DropdownMuNonMu.Text.Trim();

                    JD[i].EmployeeCode = EmployeeCode.Text;
                    JD[i].Institution = Institution.Value.Trim();

                    JD[i].Department = Department.Value.Trim();

                    JD[i].InstitutionName = InstitutionName.Text.Trim();

                    JD[i].DepartmentName = DepartmentName.Text.Trim();
                    JD[i].EmailId = MailId.Text.Trim();

                    JD[i].isCorrAuth = isCorrAuth.Text.Trim();
                    JD[i].AuthorType = AuthorType.Text.Trim();

                    JD[i].NameInJournal = NameInJournal.Text.Trim();
                    if (DropDownListPublicationEntry.SelectedValue == "JA" || DropDownListPublicationEntry.SelectedValue == "TS" || DropDownListPublicationEntry.SelectedValue == "PR")
                    {
                        if (JD[i].AuthorType == "P")
                        {
                            countAuthType = countAuthType + 1;
                        }
                    }


                    rowIndex1++;

                }
            }
            if (DropDownListPublicationEntry.SelectedValue == "JA" || DropDownListPublicationEntry.SelectedValue == "TS" || DropDownListPublicationEntry.SelectedValue == "PR")
            {
                if (countAuthType > 1)
                {
                    ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('First Author cannot be more than once!')</script>");
                    return;

                }
                if (countAuthType == 0)
                {
                    ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Select atleast one Author Type as First Author !')</script>");
                    return;

                }
            }

            string PublicationEntry1 = DropDownListPublicationEntry.SelectedValue;
            int savedfileflag = 0;
            string filelocationpath = "";
            string UploadPdf1 = "";


            //if (Directory.Exists(mainpath))
            //{
            //    if (FileUploadPdf.HasFile)
            //    {
            //        string uploadedfilename = Path.GetFileName(FileUploadPdf.PostedFile.FileName);
            //        double size = FileUploadPdf.PostedFile.ContentLength;
            //        //PublishData j = new PublishData();
            //        if (size < 4194304) //4mb
            //        {
            //            string path_BoxId = Path.Combine(mainpath, TextBoxPubId.Text); //main path + location
            //            if (!Directory.Exists(path_BoxId))   //if directory doesnt exist
            //            {
            //                Directory.CreateDirectory(path_BoxId);//crete directory of location
            //            }
            //            string uploadedfilename1 = Path.GetFileName(FileUploadPdf.PostedFile.FileName);
            //            string timestamp = DateTime.Now.ToString("dd-MM-yy-hh-mm-ss");
            //            string fileextension = Path.GetExtension(uploadedfilename);
            //            string actualfilenamewithtime = PublicationEntry1 + "_" + timestamp + fileextension;
            //            UploadPdf1 = actualfilenamewithtime;
            //            filelocationpath = Path.Combine(path_BoxId, actualfilenamewithtime);
            //            FileUploadPdf.SaveAs(filelocationpath);  //saving file

                  
            //        }
            //    }


            //}
            j.PaublicationID = TextBoxPubId.Text.Trim();
            j.TypeOfEntry = DropDownListPublicationEntry.SelectedValue;
            //j.PaublicationID = TextBoxPubId.Text.Trim();
            if (FileUploadPdf.HasFile)
            {
                j.FilePath = filelocationpath;
            }
            else
            {
                j.FilePath = j.FilePathNew;
            }

            //j.TypeOfEntry = DropDownListPublicationEntry.SelectedValue;

            // j.ApprovedBy = Session["User"].ToString();
            //j.ApprovedDate = DateTime.Now;

            // j.AutoAppoval = Session["AutoApproval"].ToString();
            int result = 0;
            //if (DropDownListPublicationEntry.SelectedValue == "JA")
            //{
            //    if (j.Indexed == "Y")
            //    {
            //        Business b1 = new Business();
            //        bool resultv = B.checkPredatoryJournal(j.PublisherOfJournal, j.PublishJAYear);
            //        if (resultv == true)
            //        {
            //            j.IncentivePointSatatus = "PRD";
            //        }
            //        else
            //        {
            //            j.IncentivePointSatatus = "PEN";
            //        }
            //    }
            //    else
            //    {
            //        j.IncentivePointSatatus = "NAP";
            //    }
            //}
            result = B.UpdatePublishLibraryEntry(j, JD, listIndexAgency);


            if (result == 1)
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
                btnSave.Enabled = false;



                if (DropDownListuploadEPrint.SelectedValue == "N")
                {

                    ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please select Yes in  upload To EPrint!')</script>");

                    btnSave.Enabled = true;

                }


                else
                {

                    EmailDetails details = new EmailDetails();
                    details = SendMail(TextBoxPubId.Text, DropDownListPublicationEntry.SelectedValue);
                    details.Id = TextBoxPubId.Text;
                    details.Type = DropDownListPublicationEntry.SelectedValue;
                    SendMailObject obj = new SendMailObject();
                    bool result1 = obj.InsertIntoEmailQueue(details);
                    JournalData obj3 = new JournalData();
                    Business obj2 = new Business();
                    obj3 = B.CheckUniqueIdUPEprint(TextBoxPubId.Text, DropDownListPublicationEntry.SelectedValue, details);
                    if (obj3.Module == details.EmailSubject + details.Module)
                    {
                        int data = obj2.updateEmailtrackerUpEprint(TextBoxPubId.Text, DropDownListPublicationEntry.SelectedValue, details, obj3);
                    }


                    ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Publication data Updated Successfully!')</script>");



                }

            }
            else
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Error!!!!!!!!!!!!')</script>");

            }


        }

        catch (Exception ex)
        {
            log.Error("Inside Catch Block Of Publication :" + ex.Message + " UserID : " + Session["UserId"].ToString());

            log.Error(ex.StackTrace);


            ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Publication data Updation Failed!!!!!!!!!!!!')</script>");

        }

    }


    //protected void FileSave(object sender, EventArgs e)
    //{
    //    string PublicationEntry = DropDownListPublicationEntry.SelectedValue;
    //    int savedfileflag = 0;
    //    string filelocationpath = "";
    //    string UploadPdf = "";
    //    if (Directory.Exists(mainpath))
    //    {
    //        if (FileUploadPdf.HasFile)
    //        {
    //            string uploadedfilename = Path.GetFileName(FileUploadPdf.PostedFile.FileName);
    //            double size = FileUploadPdf.PostedFile.ContentLength;
    //            PublishData j = new PublishData();
    //            if (size < 4194304) //4mb
    //            {
    //                string path_BoxId = Path.Combine(mainpath, TextBoxPubId.Text); //main path + location
    //                if (!Directory.Exists(path_BoxId))   //if directory doesnt exist
    //                {
    //                    Directory.CreateDirectory(path_BoxId);//crete directory of location
    //                }
    //                string uploadedfilename1 = Path.GetFileName(FileUploadPdf.PostedFile.FileName);
    //                string timestamp = DateTime.Now.ToString("dd-MM-yy-hh-mm-ss");
    //                string fileextension = Path.GetExtension(uploadedfilename);
    //                string actualfilenamewithtime = PublicationEntry + "_" + timestamp + fileextension;
    //                UploadPdf = actualfilenamewithtime;
    //                filelocationpath = Path.Combine(path_BoxId, actualfilenamewithtime);
    //                FileUploadPdf.SaveAs(filelocationpath);  //saving file
    //                j.FilePath = filelocationpath;
    //                j.PaublicationID = TextBoxPubId.Text.Trim();
    //                j.TypeOfEntry = DropDownListPublicationEntry.SelectedValue;

    //                j.ApprovedBy = Session["UserId"].ToString();
    //                j.ApprovedDate = DateTime.Now;

    //                // j.AutoAppoval = Session["AutoApproval"].ToString();

    //                if (File.Exists(filelocationpath))//checking whther file is saved or not?
    //                {
    //                    savedfileflag = 1;
    //                }

    //                if (savedfileflag == 1) //if saved then path is stored in database
    //                {
    //                    int result = B.UpdatePfPath(j);
    //                }

    //            }
    //        }
    //    }

    //    else
    //    {
    //        ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please upload the file before submission!')</script>");
    //        return;
    //    }

    //}


    protected void addclik(object sender, EventArgs e)
    {
        string confirmValue2 = Request.Form["confirm_value2"];
        if (confirmValue2 == "Yes")
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

            DropDownListMonthJA.ClearSelection();

            TextBoxYearJA.Text = "";

            TextBoxImpFact.Text = "";
            TextBoxImpFact5.Text = "";
            //txtCitation.Text = "";

            DropDownListPubType.ClearSelection();
            TextBoxPageFrom.Text = "";

            TextBoxPageTo.Text = "";
            TextBoxVolume.Text = "";
            RadioButtonListIndexed.SelectedValue = "N";
            CheckboxIndexAgency.ClearSelection();

            RadioButtonListCPIndexed.ClearSelection();
            CheckBoxListCPIndexedIn.ClearSelection();

            panelConfPaper.Visible = false;

            TextBoxEventTitle.Text = "";
            TextBoxPlace.Text = "";
            TextBoxDate.Text = "";

            panelBookPublish.Visible = false;
            lblmsg.Visible = false;

            TextBoxTitleBook.Text = "";
            TextBoxChapterContributed.Text = "";
            TextBoxEdition.Text = "";
            TextBoxPublisher.Text = "";

            TextBoxYear.Text = "";
            TextBoxPageNum.Text = "";
            TextBoxVolume1.Text = "";


            panelOthes.Visible = false;

            TextBoxPublish.Text = "";
            TextBoxDateOfPublish.Text = "";
            TextBoxPageNumNewsPaper.Text = "";


            panelTechReport.Visible = false;

            //TextBoxURL.Text = "";
            TextBoxDOINum.Text = "";
            TextBoxAbstract.Text = "";
            DropDownListErf.ClearSelection();
            TextBoxKeywords.Text = "";
            // TextBoxReference.Text = "";
            DropDownListuploadEPrint.ClearSelection();
            TextBoxEprintURL.Text = "";

            popupPanelAffil.Visible = false;
            popupPanelJournal.Visible = false;



        }
        //DropDownListPublicationEntry.Enabled = true;
        //DropDownListPublicationEntry.ClearSelection();
        //DropDownListMuCategory.ClearSelection();
        //TextBoxPubId.Text = "";
        //txtboxTitleOfWrkItem.Text = "";


        //panAddAuthor.Visible = false;
        //Grid_AuthorEntry.DataSource = null;
        //Grid_AuthorEntry.DataBind();
        //Grid_AuthorEntry.Visible = false;

        //panelJournalArticle.Visible = false;

        //TextBoxPubJournal.Text = "";

        //TextBoxNameJournal.Text = "";

        //DropDownListMonthJA.ClearSelection();

        //TextBoxYearJA.Text = "";

        //TextBoxImpFact.Text = "";

        //DropDownListPubType.ClearSelection();
        //TextBoxPageFrom.Text = "";

        //TextBoxPageTo.Text = "";
        //TextBoxVolume.Text = "";
        //RadioButtonListIndexed.SelectedValue = "N";
        //CheckboxIndexAgency.ClearSelection();



        //panelConfPaper.Visible = false;

        //TextBoxEventTitle.Text = "";
        //TextBoxPlace.Text = "";
        //TextBoxDate.Text = "";

        //panelBookPublish.Visible = false;

        //TextBoxTitleBook.Text = "";
        //TextBoxChapterContributed.Text = "";
        //TextBoxEdition.Text = "";
        //TextBoxPublisher.Text = "";

        //TextBoxYear.Text = "";
        //TextBoxPageNum.Text = "";
        //TextBoxVolume1.Text = "";


        //panelOthes.Visible = false;

        //TextBoxPublish.Text = "";
        //TextBoxDateOfPublish.Text = "";
        //TextBoxPageNumNewsPaper.Text = "";


        //panelTechReport.Visible = false;

        //TextBoxURL.Text = "";
        //TextBoxDOINum.Text = "";
        //TextBoxAbstract.Text = "";
        //DropDownListErf.ClearSelection();
        //TextBoxKeywords.Text = "";
        //TextBoxReference.Text = "";
        //DropDownListuploadEPrint.ClearSelection();
        //TextBoxEprintURL.Text = "";

        //popupPanelAffil.Visible = false;
        //popupPanelJournal.Visible = false;
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
        lblmsg.Visible = false;

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
        txtContent.Text = "";
        btnSendNotification.Enabled = true;
        // v = obj.fnfindjid(Pid, TypeEntry);
        if (TypeEntry == "JA" )
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
        //TextBoxURL.Text = v.url;
        TextBoxDOINum.Text = v.DOINum;
        TextBoxAbstract.Text = v.Abstract;
        DropDownListErf.SelectedValue = v.isERF;
        TextBoxKeywords.Text = v.Keywords;
        //TextBoxReference.Text = v.TechReferences;
        DropDownListuploadEPrint.DataBind();
        DropDownListuploadEPrint.SelectedValue = v.uploadEPrint;
        TextBoxEprintURL.Text = v.EprintURL;
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
                else if (DropdownMuNonMu.Text == "E")
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
                    Grid_AuthorEntry.Columns[1].Visible = true;
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
        lblmsg.Visible = true;


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
            //txtCitation.Text = v.CitationUrl;
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
            //txtCitation.Text = v.CitationUrl;
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
           // TextBoxPageFrom.Text = v.PageFrom;

            TextBoxIssue.Text = v.Issue;

           // TextBoxPageTo.Text = v.PageTo;
            TextBoxVolume.Text = v.JAVolume;
            RadioButtonListIndexed.SelectedValue = v.Indexed;
            // CheckboxIndexAgency.ClearSelection();
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


                if (DropDownListPublicationEntry.SelectedValue == "JA" || DropDownListPublicationEntry.SelectedValue == "TS"|| DropDownListPublicationEntry.SelectedValue == "PR")
                {
                    isCorrAuth.Visible = true;
                    AuthorType.Visible = true;
                    NameAsInJournal.Visible = true;
                    Grid_AuthorEntry.Columns[6].Visible = true;

                    Grid_AuthorEntry.Columns[7].Visible = true;
                    Grid_AuthorEntry.Columns[8].Visible = true;
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

        // setModalWindow(sender, e);

        //ButtonSub.Enabled = true;
        btnSave.Enabled = false;
    }

    // bind branch popup grid on text change
    protected void exit(object sender, EventArgs e)
    {
        affiliateSrch.Text = "";
        popGridAffil.DataBind();
    }
    // bind branch popup grid on text change
    protected void branchNameChanged(object sender, EventArgs e)
    {
        if (affiliateSrch.Text.Trim() == "")
        {
            SqlDataSourceAffil.SelectCommand = "SELECT top 10  User_Id,Name from User_M";
            popGridAffil.DataBind();
            popGridAffil.Visible = true;
        }

        else
        {
            SqlDataSourceAffil.SelectParameters.Clear();
            SqlDataSourceAffil.SelectParameters.Add("Name", affiliateSrch.Text);

            SqlDataSourceAffil.SelectCommand = "SELECT  User_Id,Name from User_M where Name like '%' + @Name + '%'";
            popGridAffil.DataBind();
            popGridAffil.Visible = true;
        }


        string rowVal = Request.Form["rowIndx"];
        int rowIndex = Convert.ToInt32(rowVal);

        ModalPopupExtender ModalPopupExtender8 = (ModalPopupExtender)Grid_AuthorEntry.Rows[rowIndex].FindControl("ModalPopupExtender4");
        ModalPopupExtender8.Show();
    }

    protected void JournalCodePopChanged(object sender, EventArgs e)
    {

        if (journalcodeSrch.Text.Trim() == "")
        {
            SqlDataSourceJournal.SelectCommand = "SELECT top 10 Id,Title,AbbreviatedTitle FROM [Journal_M]";
            popGridJournal.DataBind();
            popGridJournal.Visible = true;
        }

        else
        {
            SqlDataSourceJournal.SelectParameters.Clear();
            SqlDataSourceJournal.SelectParameters.Add("Title", journalcodeSrch.Text);

            SqlDataSourceJournal.SelectCommand = "SELECT  Id,Title,AbbreviatedTitle FROM [Journal_M] where Title like '%' + @Title + '%'";
            popGridJournal.DataBind();
            popGridJournal.Visible = true;
        }

        ModalPopupExtender1.Show();
    }

    protected void exit1(object sender, EventArgs e)
    {

        journalcodeSrch.Text = "";
        popGridJournal.DataBind();

    }
    private EmailDetails SendMail(string publicationid, string typeofentry)
    {
        log.Debug("Publication Library Update: Inside Send Mail function of Entry Type : " + typeofentry + " ID " + publicationid);
        EmailDetails details = new EmailDetails();
        details.Module = "PEPRN";
        details.EmailSubject = "Publication Entry  <  " + DropDownListPublicationEntry.SelectedValue + " _ " + TextBoxPubId.Text + "  > Uploaded to ePrints. ";
        ArrayList authorList = new ArrayList();
        ArrayList authorName = new ArrayList();
        ArrayList firstAuthor = new ArrayList();
        ArrayList AuthorCCList = new ArrayList();
        ArrayList ccAuthor = new ArrayList();
        ArrayList myArrayList2 = new ArrayList();
        ArrayList AuthorCCListDetail = new ArrayList();


        DataSet ds = new DataSet();
        Business obj = new Business();
        Business e = new Business();
        ds = obj.getFirstAuthor(TextBoxPubId.Text, DropDownListPublicationEntry.SelectedValue);
        //foreach loop to read each DataRow of DataTable stored inside the DataSet
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            authorList.Add(ds.Tables[0].Rows[i]["EmailId"].ToString());
        }

        DataSet dy = new DataSet();
        DataSet dsIN = new DataSet();
        int result;
        dy = obj.getAuthorCCList(TextBoxPubId.Text, DropDownListPublicationEntry.SelectedValue);
        dsIN = obj.getAuthorCCListDetail(TextBoxPubId.Text, DropDownListPublicationEntry.SelectedValue);

        for (int i = 0; i < dy.Tables[0].Rows.Count; i++)
        {
            AuthorCCList.Add(dy.Tables[0].Rows[i]["EmailId"].ToString());

            if (AuthorCCList[i].ToString() == "")
            {
                int j = i;
                if ((j == i) && (j < dsIN.Tables[0].Rows.Count))
                {
                    AuthorCCListDetail.Add(dsIN.Tables[0].Rows[j]["AuthorName"].ToString());
                }

            }
        }
        for (int k = 0; k < AuthorCCListDetail.Count; k++)
        {
            string AuthorName = AuthorCCListDetail[k].ToString();

            result = e.insertEmailtrackerUpEprint(AuthorName, details, TextBoxPubId.Text);
        }

        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            firstAuthor.Add(ds.Tables[0].Rows[i]["EmailId"].ToString());
        }

        for (int i = 0; i < dy.Tables[0].Rows.Count; i++)
        {
            ccAuthor.Add(dy.Tables[0].Rows[i]["EmailId"].ToString());
        }

        ArrayList studentlist = new ArrayList();
        DataSet student = new DataSet();
        string studentauthor1 = studentauthor.Value;
        if (studentauthor1 == "Y")
        {
            if (DropDownListPublicationEntry.SelectedValue == "JA" || DropDownListPublicationEntry.SelectedValue == "TS")
            {
                student = obj.getStudentlist(TextBoxPubId.Text, DropDownListPublicationEntry.SelectedValue);
            }
            for (int i = 0; i < student.Tables[0].Rows.Count; i++)
            {
                studentlist.Add(student.Tables[0].Rows[i]["EmailId"].ToString());
            }
        }

        DataSet dZ = new DataSet();
        dZ = obj.getAuthorListName1(TextBoxPubId.Text, DropDownListPublicationEntry.SelectedValue);
        for (int i = 0; i < dZ.Tables[0].Rows.Count; i++)
        {
            myArrayList2.Add(dZ.Tables[0].Rows[i]["AuthorName"].ToString());
        }
        string auhtorsS = "";
        string auhtorsSConc = "";
        for (int i = 0; i < myArrayList2.Count; i++)
        {
            auhtorsS = myArrayList2[i].ToString();
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

        try
        {
            details.FromEmail = ConfigurationManager.AppSettings["FromAddress"].ToString();
            details.Module = "PEPRN";
            string id = TextBoxPubId.Text;
            User a2 = new User();
            a2 = obj.GetFirstAuthorName(id, DropDownListPublicationEntry.SelectedValue);
            // code here is to check the publication is indexed or not
            string isIndexed = null;
            string FooterText = ConfigurationManager.AppSettings["FooterText"].ToString();
            if (DropDownListPublicationEntry.SelectedValue == "JA" || DropDownListPublicationEntry.SelectedValue == "TS")
            {
                //  isIndexed = b.GetIndexValue(TextBoxPubId.Text, txtboxTitleOfWrkItem.Text);// code here is to get the indexed value to compare for sending the email
                isIndexed = RadioButtonListIndexed.SelectedValue;// code here is to get the indexed value to compare for sending the email

                if (isIndexed == "Y")
                {
                   
                    details.EmailSubject = "Publication Entry  <  " + DropDownListPublicationEntry.SelectedValue + " _ " + TextBoxPubId.Text + "  > Uploaded to ePrints. ";

                    details.MsgBody = "<span style=\"font-size: 10pt; color: #3300cc; font-family: Verdana\"><h4>Dear Sir/Madam,</h4> <br>" +
                         "<b>This is to inform you that the article titled  '" + txtboxTitleOfWrkItem.Text + "' entered by you in RMS is uploaded to ePrints.<br> We request you to download the evaluation form from  'Print Evaluation' option available under 'Publication Section' and submit the same (on behalf of all) through proper channel to Directorate of Research to claim the research incentives.<br> " +
                         "<br>" + "Authors  : " + auhtorsSConc + "<br>" + "<br>" + "<br>" + "<br>" + "<br>" + FooterText + " </b><br><b> </b></span>";


                    for (int i = 0; i < authorList.Count; i++)
                    {
                        if (details.ToEmail != null)
                        {
                            details.ToEmail = details.ToEmail + ',' + authorList[i].ToString();
                        }
                        else
                        {
                            if (i == 0)
                            {
                                details.ToEmail = authorList[i].ToString();
                            }
                            else
                            {
                                details.ToEmail = details.ToEmail + ',' + authorList[i].ToString();
                            }
                        }
                        //details.ToEmail = email;
                        log.Info(" Email will be sent to authors '" + i + "' : '" + authorList[i] + "' ");
                    }

                    for (int i = 0; i < ccAuthor.Count; i++)
                    {
                        // Msg.To.Add(BuyerId_Array[0]+dir_domain);
                        //string email = ccAuthor[i].ToString();
                        //if (details.CCEmail != null)
                        //{
                        //    details.CCEmail = details.CCEmail + ',' + ccAuthor[i].ToString();
                        //}
                        //else
                        //{
                        //    if (i == 0)
                        //    {
                        //        details.CCEmail = ccAuthor[i].ToString();
                        //    }
                        //    else
                        //    {
                        //        details.CCEmail = details.CCEmail + ',' + ccAuthor[i].ToString();
                        //    }
                        //}
                        string email = ccAuthor[i].ToString();
                        if (details.CCEmail != null)
                        {
                            if (ccAuthor[i].ToString() != "")
                            {
                                details.CCEmail = details.CCEmail + ',' + ccAuthor[i].ToString();
                            }

                        }
                        else
                        {
                            if (i == 0)
                            {
                                if (ccAuthor[i].ToString() != "")
                                {
                                    details.CCEmail = ccAuthor[i].ToString();
                                }
                            }
                            else
                            {
                                if (ccAuthor[i].ToString() != "")
                                {
                                    if (details.CCEmail == null)
                                    {
                                        details.CCEmail = ccAuthor[i].ToString();
                                    }
                                    else
                                    {
                                        details.CCEmail = details.CCEmail + ',' + ccAuthor[i].ToString();
                                    }
                                }
                            }
                            //details.CCEmail = details.CCEmail + ',' + email;
                        }
                        log.Info(" CC Email will be sent to authors '" + i + "' : '" + ccAuthor[i] + "' ");
                    }

                    if (studentauthor1 == "Y")
                    {
                        if (DropDownListPublicationEntry.SelectedValue == "JA" || DropDownListPublicationEntry.SelectedValue == "TS")
                        {
                            for (int i = 0; i < studentlist.Count; i++)
                            {

                                if (details.CCEmail != null)
                                {
                                    details.CCEmail = details.CCEmail + ',' + studentlist[i].ToString();
                                }
                                else
                                {
                                    if (i == 0)
                                    {
                                        details.CCEmail = studentlist[i].ToString();
                                    }
                                    else
                                    {
                                        details.CCEmail = details.CCEmail + ',' + studentlist[i].ToString();
                                    }
                                }

                                //details.ToEmail = email;
                                log.Info(" Email will be sent to student authors '" + i + "' : '" + studentlist[i] + "' ");
                            }
                        }
                    }

                }

                else if (isIndexed == "N")
                {
                   
                    details.EmailSubject = "Publication Entry  <  " + DropDownListPublicationEntry.SelectedValue + " _ " + TextBoxPubId.Text + "  > Uploaded to ePrints ";
                    details.MsgBody = "<span style=\"font-size: 10pt; color: #3300cc; font-family: Verdana\"><h4>Dear Sir/Madam,</h4> <br>" +
                          "<b>This is to inform you that, the article titled " + txtboxTitleOfWrkItem.Text + " is uploaded to eprints. However, this will not be considered for research incentives<br> " +
                          "<br>" + "Authors  : " + auhtorsSConc + "<br>" + "<br>" + "<br>" + "<br>" + "<br>" + FooterText + " </b><br><b> </b></span>";

                    for (int i = 0; i < authorList.Count; i++)
                    {
                        string email = authorList[i].ToString();
                        if (details.ToEmail != null)
                        {
                            details.ToEmail = details.ToEmail + ',' + authorList[i].ToString();
                        }
                        else
                        {
                            if (i == 0)
                            {
                                details.ToEmail = authorList[i].ToString();
                            }
                            else
                            {
                                details.ToEmail = details.ToEmail + ',' + authorList[i].ToString();
                            }
                          //  details.ToEmail = email;
                        }
                        log.Info(" Email will be sent to authors '" + i + "' : '" + authorList[i] + "' ");
                    }

                }
            }

            else
            {
               
                details.EmailSubject = "Publication Entry  <  " + DropDownListPublicationEntry.SelectedValue + " _ " + TextBoxPubId.Text + "  > Uploaded to ePrints ";
                details.MsgBody = "<span style=\"font-size: 10pt; color: #3300cc; font-family: Verdana\"><h4>Dear Sir/Madam,</h4> <br>" +
                     "<b>This is to inform you that, the article titled " + txtboxTitleOfWrkItem.Text + " is uploaded to ePrints.<br> " +
                     "<br>" + "Authors  : " + auhtorsSConc + "<br>" + "<br>" + "<br>" + "<br>" + "<br>" + FooterText + " </b><br><b> </b></span>";

                for (int i = 0; i < authorList.Count; i++)
                {
                   // string email = authorList[i].ToString();
                    if (details.ToEmail != null)
                    {
                        details.ToEmail = details.ToEmail + ',' + authorList[i].ToString();
                    }
                    else
                    {
                        if (i == 0)
                        {
                            details.ToEmail = authorList[i].ToString();
                        }
                        else
                        {
                            details.ToEmail = details.ToEmail + ',' + authorList[i].ToString();
                        }
                        //details.ToEmail = email;
                    }
                    log.Info(" Email will be sent to authors '" + i + "' : '" + authorList[i] + "' ");
                }

            }
            return details;
        }
        catch (Exception ex)
        {
            log.Error(ex.StackTrace);
            log.Error(ex.Message);
            throw ex;
        }
    }
    protected void btnSendNotification_Click(object sender, System.EventArgs e)
    {
         if (!Page.IsValid)
        {
            return;
        }
        try
        {
            Business b = new Business();
            EmailDetails details = new EmailDetails();
            SendMailObject obj = new SendMailObject();
            details.FromEmail = ConfigurationManager.AppSettings["FromAddress"].ToString();
            string ccmailid = "";

            string pubid = TextBoxPubId.Text;
            string typeofentry = DropDownListPublicationEntry.SelectedValue;
            string toEmailId = b.SelectAuthorToMailid(pubid, typeofentry);
            details.ToEmail = toEmailId;
            details.Module = "PEPRN";
            ArrayList ccmailidlist = new ArrayList();
            int rowIndex = 0;
            for (int i = 0; i < Grid_AuthorEntry.Rows.Count; i++)
            {

                TextBox EmployeeCode = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[2].FindControl("EmployeeCode");
                DropDownList DropdownMuNonMu = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[3].FindControl("DropdownMuNonMu");
                string employeeid = EmployeeCode.Text;

                ccmailid = b.SelectAuthorCCMailid(employeeid, pubid, typeofentry);
                if (ccmailid != "")
                {
                    if (ccmailid != toEmailId)
                    {
                        if (ccmailid != "")
                        {
                            details.CCEmail = ccmailid;
                        }
                    }
                }
                else
                {
                    if (ccmailid != toEmailId)
                    {
                        if (ccmailid != "")
                        {
                            details.CCEmail = details.CCEmail + ',' + ccmailid;
                        }
                    }
                }
               
                  rowIndex++;

            }

            details.Id = TextBoxPubId.Text;
            details.Type = DropDownListPublicationEntry.SelectedValue;
            string FooterText = ConfigurationManager.AppSettings["FooterText"].ToString();
            details.EmailSubject ="E-print Update Notification";
            details.MsgBody = "<span style=\"font-size: 10pt; color: #3300cc; font-family: Verdana\"><h4>Dear Sir/Madam,</h4> <br>" +
                          "<b> '" + txtContent.Text + "' <br> " +
                            "<br>" +
                       "Publication Type  : " + DropDownListPublicationEntry.SelectedItem + "<br>" +
                    "Publication Id  :  " + TextBoxPubId.Text + "<br>" +

                    "Title of Work Item  : " + txtboxTitleOfWrkItem.Text + "<br>" +
                          "<br>" + "<br>" + "<br>" + "<br>" + FooterText +
                         " </b><br><b> </b></span>";

            bool resultv = obj.InsertIntoEmailQueue(details);
            if (resultv == true)
            {

                btnSendNotification.Enabled = false;
                //string CloseWindow1 = "alert('Mail Sent successfully')";
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "newWindow", CloseWindow1, true);
                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Mail Sent successfully')</script>");
            }
            else
            {
                btnSendNotification.Enabled = true;
               //string CloseWindow1 = "alert('Problem while sending mail')";
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "newWindow", CloseWindow1, true);
                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Problem while sending mail')</script>");
            }
        }
        catch (Exception ex)
        {
            log.Error(ex.StackTrace);
            log.Error(ex.Message);

        }
    }
    
}