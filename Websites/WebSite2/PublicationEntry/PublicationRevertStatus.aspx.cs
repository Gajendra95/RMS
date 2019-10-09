using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;

public partial class PublicationEntry_PublicationRevertStatus : System.Web.UI.Page
{
    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    string PublicationYearwebConfig = ConfigurationManager.AppSettings["PublicationYear"];
    string FolderPathServerWrite = ConfigurationManager.AppSettings["FolderPath"].ToString();

    Business B = new Business();
    Journal_DataObject JournalDataObj = new Journal_DataObject();
    JournalData JournalValueObj = new JournalData();
    public string pageID = "L42";
    protected void Page_Load(object sender, EventArgs e)
    {

    }


    protected void addclik(object sender, EventArgs e)
    {
            panel.Visible = false;
            panelRemarks.Visible = false;
            panel2.Visible = false;
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



            panelConfPaper.Visible = false;

            TextBoxEventTitle.Text = "";
            TextBoxPlace.Text = "";
            TextBoxDate.Text = "";
            TextBoxDate1.Text = "";
            panelBookPublish.Visible = false;

            TextBoxTitleBook.Text = "";
            TextBoxChapterContributed.Text = "";
            TextBoxEdition.Text = "";
            TextBoxPublisher.Text = "";

            TextBoxYear.ClearSelection();
            DropDownListBookMonth.ClearSelection();
            TextBoxPageNum.Text = "";
            TextBoxVolume1.Text = "";


            panelOthes.Visible = false;

            TextBoxNewsPublish.Text = "";
            TextBoxDateOfNewsPublish.Text = "";
            TextBoxPageNumNewsPaper.Text = "";

            panelTechReport.Visible = false;

            //TextBoxURL.Text = "";
            TextBoxDOINum.Text = "";
            TextBoxAbstract.Text = "";
            DropDownListErf.ClearSelection();
            TextBoxKeywords.Text = "";
            DropDownListuploadEPrint.ClearSelection();
            TextBoxEprintURL.Text = "";
            

            TextBoxIsbn.Text = "";
            RadioButtonListTypePresentaion.ClearSelection();
            TextBoxAwardedBy.Text = "";
            GVViewFile.Visible = false;
            TextBoxCreditPoint.Text = "0";
            TextBoxAwardedBy.Enabled = false;
            RevertComment.Text = "";

    }


    protected void ButtonSearchPubOnClick(object sender, EventArgs e)
    {
        addclik(sender, e);
        GridViewSearch.Visible = true;
        GridViewSearch.EditIndex = -1;
        dataBind();

    }
    private void dataBind()
    {

        GridViewSearch.Visible = true;

        string pubtype = EntryTypesearch.SelectedValue;
        SqlDataSource1.SelectParameters.Clear(); 
        if (PubIDSearch.Text == "")
        {
            //SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,t.EntryName,PubJournalID,PublishJAMonth,TitleWorkItem,c.PubCatName,PublishJAYear,ImpactFactor ,s.StatusName from Publication p,Status_Publication_M s , PubMUCategorization_M c ,PublicationTypeEntry_M t where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization and  (TypeOfEntry='" + EntryTypesearch.SelectedValue + "' or '" + EntryTypesearch.SelectedValue + "'='A') and (Status='APP' or Status='CAN')";
            SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,t.EntryName,PubJournalID,PublishJAMonth,TitleWorkItem,c.PubCatName,PublishJAYear,ImpactFactor ,s.StatusName from Publication p,Status_Publication_M s , PubMUCategorization_M c ,PublicationTypeEntry_M t where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization and  (TypeOfEntry=@EntryTypesearch or @EntryTypesearch='A') and (Status='APP' or Status='CAN')";


        }
        else
        {
            //SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,t.EntryName,PubJournalID,PublishJAMonth,TitleWorkItem,c.PubCatName,PublishJAYear,ImpactFactor ,s.StatusName from Publication p,Status_Publication_M s , PubMUCategorization_M c ,PublicationTypeEntry_M t where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization and  (TypeOfEntry='" + EntryTypesearch.SelectedValue + "' or '" + EntryTypesearch.SelectedValue + "'='A') and (Status='APP' or Status='CAN') and PublicationID like '%" + PubIDSearch.Text.Trim() + "%'";
            SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,t.EntryName,PubJournalID,PublishJAMonth,TitleWorkItem,c.PubCatName,PublishJAYear,ImpactFactor ,s.StatusName from Publication p,Status_Publication_M s , PubMUCategorization_M c ,PublicationTypeEntry_M t where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization and  (TypeOfEntry=@EntryTypesearch or @EntryTypesearch='A') and (Status='APP' or Status='CAN') and PublicationID like '%' + @PubIDSearch + '%'";

            SqlDataSource1.SelectParameters.Add("PubIDSearch", PubIDSearch.Text.Trim());
        
        }
        SqlDataSource1.SelectParameters.Add("EntryTypesearch", EntryTypesearch.SelectedValue);
        GridViewSearch.DataBind();
        SqlDataSource1.DataBind();
        GridViewSearch.Visible = true;
    }

    protected void GridViewSearchPub_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        dataBind();
        GridViewSearch.PageIndex = e.NewPageIndex;
        GridViewSearch.DataBind();
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


    protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //Find the DropDownList in the Row
            DropDownList DropdownMuNonMu = (e.Row.FindControl("DropdownMuNonMu") as DropDownList);

            if (DropDownListPublicationEntry.SelectedValue == "BK" || DropDownListPublicationEntry.SelectedValue == "NM")
            {


                SqlDataSourceAuthorType.SelectCommand = "SELECT Id,Type FROM [Author_Type_M] where (Id! = 'O')and (Id! = 'E') ";

                DropdownMuNonMu.DataTextField = "Type";
                DropdownMuNonMu.DataValueField = "Id";
                DropdownMuNonMu.DataBind();


            }
            else if (DropDownListPublicationEntry.SelectedValue == "JA" || DropDownListPublicationEntry.SelectedValue == "TS" || DropDownListPublicationEntry.SelectedValue == "CP" || DropDownListPublicationEntry.SelectedValue == "PR")
            {
                SqlDataSourceAuthorType.SelectCommand = "SELECT Id,DisplayName FROM [Author_Type_M] ";

                DropdownMuNonMu.DataTextField = "DisplayName";
                DropdownMuNonMu.DataValueField = "Id";
                DropdownMuNonMu.DataBind();

            }
            else
            {
            }

        }

    }
    public void edit(Object sender, GridViewEditEventArgs e)
    {
        GridViewSearch.EditIndex = e.NewEditIndex;

        fnRecordExist(sender, e);

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
    public void fnRecordExist(object sender, EventArgs e)
    {
        panel2.Visible = true;
        panelRemarks.Visible = true;
        panel.Visible = true;
        panelRemarks.Visible = true;
        panel2.Visible = true;
        string Pid = Session["TempPid"].ToString();
        string TypeEntry = Session["TempTypeEntry"].ToString();

        string FileUpload = "";
        Business b1 = new Business();
        FileUpload = b1.GetFileUploadPath(Pid, TypeEntry);
        if (FileUpload != "")
        {
            GVViewFile.Visible = true;
            lblNoPDF.Visible = false;
        }
        else
        {
            GVViewFile.Visible = false;
            lblNoPDF.Visible = true;
        }
        DSforgridview.SelectParameters.Clear();
        DSforgridview.SelectParameters.Add("TypeEntry", TypeEntry);
        DSforgridview.SelectParameters.Add("Pid", Pid);
        DSforgridview.SelectCommand = "select UploadPDFPath from Publication where PublicationID=@Pid  and TypeOfEntry=@TypeEntry  ";
        DSforgridview.DataBind();
        GVViewFile.DataBind();

        Business obj = new Business();
        PublishData v = new PublishData();
        v = obj.fnfindjid(Pid, TypeEntry);

        DropDownListMuCategory.SelectedValue = v.MUCategorization;
        DropDownListPublicationEntry.SelectedValue = TypeEntry;
        DropDownListPublicationEntry.Enabled = false;
        TextBoxPubId.Text = Pid;
        txtboxTitleOfWrkItem.Text = v.TitleWorkItem;
        TextBoxRemarks.Text = v.RemarksFeedback;
        TextBoxYearJA.Items.Clear();
        TextBoxYear.Items.Clear();
        //  v = obj.fnfindjid(Pid, TypeEntry);

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
        if (v.Status == "APP")
        {
            Buttonupload.Visible = true;
            FileUploadPdf.Visible = true;
        }
        else
        {
            Buttonupload.Visible = false;
            FileUploadPdf.Visible = false;
        }

        if (TypeEntry == "JA")
        {
            string jname = obj.fnfindjidgtjname(Pid, TypeEntry);
            panelJournalArticle.Visible = true;
            panelBookPublish.Visible = false;
            panelConfPaper.Visible = false;
            panelOthes.Visible = false;

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

            //txtCitation.Text = v.CitationUrl;
            DropDownListPubType.SelectedValue = v.Publicationtype;
            //TextBoxPageFrom.Text = v.PageFrom;

            //TextBoxPageTo.Text = v.PageTo;
            if (v.PageFrom.Contains("PF"))
            {
            }
            else
            {
                TextBoxPageFrom.Text = v.PageFrom;
                TextBoxPageTo.Text = v.PageTo;
            }

            TextBoxIssue.Text = v.Issue;
            TextBoxVolume.Text = v.JAVolume;
            RadioButtonListIndexed.SelectedValue = v.Indexed;
            // CheckboxIndexAgency.ClearSelection();
            //lblnote1.Visible = true;
            JournalData jd = new JournalData();
            Business B = new Business();
            int jayear = Convert.ToInt16(TextBoxYearJA.Text);
            int jamonth = Convert.ToInt16(v.PublishJAMonth);
            if (jayear >= 2018 && jamonth >= 7)
            {
                if (v.QuartileOnIncentivize != "")
                {
                    jd = B.selectQuartilevaluefrompublication(TextBoxPubId.Text, DropDownListMuCategory.SelectedValue, DropDownListPublicationEntry.SelectedValue);
                }
                else
                {
                    jd = B.selectQuartilevaluefrompublicationEntry(TextBoxPubId.Text, DropDownListMuCategory.SelectedValue, DropDownListPublicationEntry.SelectedValue);
                }
                lblQuartile.Visible = true;
                txtquartile.Visible = true;
                txtquartile.Text = jd.QName;
                txtquartileid.Text = jd.Jquartile;
            }
            else
                if (jayear >= 2019 && jamonth >= 1)
                {
                    if (v.QuartileOnIncentivize != "")
                    {
                        jd = B.selectQuartilevaluefrompublication(TextBoxPubId.Text, DropDownListMuCategory.SelectedValue, DropDownListPublicationEntry.SelectedValue);
                    }
                    else
                    {
                        jd = B.selectQuartilevaluefrompublicationEntry(TextBoxPubId.Text, DropDownListMuCategory.SelectedValue, DropDownListPublicationEntry.SelectedValue);
                    }
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

            //txtCitation.Text = v.CitationUrl;
            DropDownListPubType.SelectedValue = v.Publicationtype;
            //TextBoxPageFrom.Text = v.PageFrom;

            //TextBoxPageTo.Text = v.PageTo;
            if (v.PageFrom.Contains("PF"))
            {
            }
            else
            {
                TextBoxPageFrom.Text = v.PageFrom;
                TextBoxPageTo.Text = v.PageTo;
            }

            TextBoxIssue.Text = v.Issue;
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
                TextBoxYear.Items.Add(new ListItem(yeatAppend.ToString(), yeatAppend.ToString(), true));
            }

            TextBoxYear.DataBind();


            TextBoxYear.SelectedValue = v.BookPublishYear;
            DropDownListBookMonth.SelectedValue = v.BookPublishMonth.ToString();
            //DropDownListPubType.SelectedValue=v.Publicationtype;
            TextBoxPageNum.Text = v.BookPageNum;

            TextBoxVolume1.Text = v.BookVolume;
            //lblnote1.Visible = false;
            // CheckboxIndexAgency.ClearSelection();
            txtbISBN.Text = v.ConfISBN;
            txtbSection.Text = v.BookSection;
            txtChapter.Text = v.BookChapter;
            txtCountry.Text = v.BookCountry;
            DropDownListBookPublicationType.SelectedValue = v.BookTypeofPublication;
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
            TextBoxDate1.Text = v.todate.ToShortDateString();
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
        }
        else if (TypeEntry == "NM")
        {


            panelJournalArticle.Visible = false;
            panelBookPublish.Visible = false;
            panelConfPaper.Visible = false;
            panelOthes.Visible = true;

            TextBoxNewsPublish.Text = v.NewsPublisher;

            TextBoxDateOfNewsPublish.Text = v.NewsPublishedDate.ToShortDateString();

            TextBoxPageNumNewsPaper.Text = v.NewsPageNum;
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
            //TextBoxPageFrom.Text = v.PageFrom;
            if (v.PageFrom.Contains("PF"))
            {
            }
            else
            {
                TextBoxPageFrom.Text = v.PageFrom;
                TextBoxPageTo.Text = v.PageTo;
            }
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
                ImageButton EmployeeCodeBtnimg1 = (ImageButton)Grid_AuthorEntry.Rows[0].Cells[2].FindControl("EmployeeCodeBtn");


                drCurrentRow = dtCurrentTable.NewRow();

                DropdownMuNonMu.Text = dtCurrentTable.Rows[i - 1]["DropdownMuNonMu"].ToString();
                EmployeeCode.Text = dtCurrentTable.Rows[i - 1]["EmployeeCode"].ToString();
                AuthorName.Text = dtCurrentTable.Rows[i - 1]["AuthorName"].ToString();
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
                else  if (DropdownMuNonMu.Text == "E")
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

                    EmployeeCode.Enabled = false;
                    NationalType.Visible = false;
                    ContinentId.Visible = false;
                }
                else if (DropdownMuNonMu.Text == "S")
                {
                    if (DropDownListPublicationEntry.SelectedValue == "JA" || DropDownListPublicationEntry.SelectedValue == "TS" || DropDownListPublicationEntry.SelectedValue == "CP" || DropDownListPublicationEntry.SelectedValue == "PR")
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
                        EmployeeCode.Enabled = true;
                        DropdownStudentInstitutionName.Text = dtCurrentTable.Rows[i - 1]["Institution"].ToString();
                        DropdownStudentDepartmentName.Text = dtCurrentTable.Rows[i - 1]["Department"].ToString();

                    }
                    NationalType.Visible = false;
                    ContinentId.Visible = false;
                }
                DropdownMuNonMu1.Enabled = false;
                // EmployeeCodeBtnimg1.Enabled = false;

                if (DropDownListPublicationEntry.SelectedValue == "JA" || DropDownListPublicationEntry.SelectedValue == "TS" || DropDownListPublicationEntry.SelectedValue == "PR")
                {
                    DropdownMuNonMu1.Enabled = true;
                    //EmployeeCodeBtnimg1.Enabled = true;
                }
                else
                {
                    DropdownMuNonMu1.Enabled = false;
                    //EmployeeCodeBtnimg1.Enabled = false;
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
                    //EmployeeCodeBtnimg.Enabled = false;
                    AuthorName.Enabled = true;
                    InstNme.Enabled = true;
                    deptname.Enabled = true;
                    MailId.Enabled = true;
                }
                else if (DropdownMuNonMu.Text == "E")
                {
                    //EmployeeCodeBtnimg.Enabled = false;
                    AuthorName.Enabled = true;
                    InstNme.Enabled = true;
                    deptname.Enabled = true;
                    MailId.Enabled = true;
                }
                else if (DropdownMuNonMu.Text == "M")
                {
                    //EmployeeCodeBtnimg.Enabled = true;
                    AuthorName.Enabled = false;
                    InstNme.Enabled = false;
                    deptname.Enabled = false;
                    MailId.Enabled = false;
                }
                else if (DropdownMuNonMu.Text == "S")
                {
                    //EmployeeCodeBtnimg.Enabled = false;
                    AuthorName.Enabled = true;
                    DropdownStudentInstitutionName.Enabled = true;
                    DropdownStudentInstitutionName.Enabled = true;
                    MailId.Enabled = true;
                }
                else if (DropdownMuNonMu.Text == "O")
                {
                    AuthorName.Enabled = true;
                    DropdownStudentInstitutionName.Enabled = true;
                    DropdownStudentInstitutionName.Enabled = true;

                    EmployeeCode.Enabled = false;
                }
                else
                {
                    MailId.Enabled = true;
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

                Grid_AuthorEntry.Columns[13].Visible = true;




                rowIndex++;

            }


            ViewState["CurrentTable"] = dtCurrentTable;
        }
        //int rowIndex = 0;

        //DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
        //DataRow drCurrentRow = null;
        //if (dtCurrentTable.Rows.Count > 0)
        //{
        //    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
        //    {
        //        DropDownList DropdownMuNonMu = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[3].FindControl("DropdownMuNonMu");
        //        TextBox EmployeeCode = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[2].FindControl("EmployeeCode");
        //        TextBox AuthorName = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[2].FindControl("AuthorName");
        //        TextBox InstNme = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[2].FindControl("InstitutionName");
        //        TextBox deptname = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[2].FindControl("DepartmentName");
        //        HiddenField InstId = (HiddenField)Grid_AuthorEntry.Rows[rowIndex].Cells[2].FindControl("Institution");
        //        HiddenField deptId = (HiddenField)Grid_AuthorEntry.Rows[rowIndex].Cells[2].FindControl("Department");
        //        TextBox MailId = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[2].FindControl("MailId");
        //        DropDownList isCorrAuth = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[2].FindControl("isCorrAuth");
        //        DropDownList AuthorType = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[2].FindControl("AuthorType");
        //        TextBox NameAsInJournal = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[2].FindControl("NameInJournal");



        //        drCurrentRow = dtCurrentTable.NewRow();

        //        DropdownMuNonMu.Text = dtCurrentTable.Rows[i - 1]["DropdownMuNonMu"].ToString();
        //        EmployeeCode.Text = dtCurrentTable.Rows[i - 1]["EmployeeCode"].ToString();
        //        AuthorName.Text = dtCurrentTable.Rows[i - 1]["AuthorName"].ToString();
        //        InstNme.Text = dtCurrentTable.Rows[i - 1]["InstitutionName"].ToString();
        //        deptname.Text = dtCurrentTable.Rows[i - 1]["DepartmentName"].ToString();
        //        InstId.Value = dtCurrentTable.Rows[i - 1]["Institution"].ToString();
        //        deptId.Value = dtCurrentTable.Rows[i - 1]["Department"].ToString();
        //        MailId.Text = dtCurrentTable.Rows[i - 1]["MailId"].ToString();
        //        AuthorType.Text = dtCurrentTable.Rows[i - 1]["AuthorType"].ToString();
        //        isCorrAuth.Text = dtCurrentTable.Rows[i - 1]["isCorrAuth"].ToString();
        //        NameAsInJournal.Text = dtCurrentTable.Rows[i - 1]["NameInJournal"].ToString();



        //        rowIndex++;

        //    }


        //    ViewState["CurrentTable"] = dtCurrentTable;
        //}
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
                //lblmsgpubnonondexed.Visible = false;
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
                CheckboxIndexAgency.Enabled = false;
                CheckboxIndexAgency.ClearSelection();
            }
        }
        //ButtonSub.Enabled = false;
        //btnSave.Enabled = true;


    }



    
    protected void BtnSave_Click(object sender, EventArgs e)
    {
        if (RevertComment.Text != "")
        {
            PublishData obj = new PublishData();
            obj.PaublicationID = TextBoxPubId.Text;
            obj.TypeOfEntry = DropDownListPublicationEntry.SelectedValue;
            obj.RemarksFeedback = RevertComment.Text;
            bool result = B.RevertingStatusToNew(obj);
            if (result == true)
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Status is reverted back to New')</script>");
                btnSave.Enabled = false;
                EmailDetails details = new EmailDetails();
                details.Id = TextBoxPubId.Text;
                details.Type = DropDownListPublicationEntry.SelectedValue;
                details = SendMail();
                details.Id = TextBoxPubId.Text;
                details.Type = DropDownListPublicationEntry.SelectedValue;
                SendMailObject obj1 = new SendMailObject();
                bool result1 = obj1.InsertIntoEmailQueue(details);
            }
            else
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Problem')</script>");

            }
        }
        else
        {
            ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please enter the comment')</script>");
            return;
        }

    }

    protected void BtnUploadPdf_Click(object sender, EventArgs e)
    {

        string filelocationpath = "";
        if (FileUploadPdf.HasFile)
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

            string PublicationEntry1 = DropDownListPublicationEntry.SelectedValue;
            string UploadPdf1 = "";

            string uploadedfilename = Path.GetFileName(FileUploadPdf.PostedFile.FileName);
            double size = FileUploadPdf.PostedFile.ContentLength;
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

            }

            PublishData obj = new PublishData();
            obj.PaublicationID = TextBoxPubId.Text;
            obj.TypeOfEntry = DropDownListPublicationEntry.SelectedValue;
            obj.FilePath = filelocationpath;
            bool result = B.UploadedPdfPath(obj);
            if (result == true)
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('File uploaded')</script>");
                DSforgridview.SelectCommand = "select UploadPDFPath from Publication where PublicationID='" + obj.PaublicationID + "' and TypeOfEntry='" + obj.TypeOfEntry + "'";
                DSforgridview.DataBind();
                GVViewFile.DataBind();
            }
            else
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Problem while uploading')</script>");

            }
        }



    }
    public EmailDetails SendMail()
    {
        log.Debug("Publication RevertStatus: Inside SendMail function of Type: " + DropDownListPublicationEntry.SelectedValue + " ID: " + TextBoxPubId.Text);
        EmailDetails details = new EmailDetails();

        ArrayList authorList = new ArrayList();
        ArrayList authorName = new ArrayList();
        ArrayList AuthorCCList = new ArrayList();
        ArrayList myArrayList2 = new ArrayList();

        DataSet ds = new DataSet();
        Business bus = new Business();
        ds = bus.getFirstAuthor(TextBoxPubId.Text, DropDownListPublicationEntry.SelectedValue);
        //foreach loop to read each DataRow of DataTable stored inside the DataSet
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            authorList.Add(ds.Tables[0].Rows[i]["EmailId"].ToString());
        }

        DataSet dy = new DataSet();
        dy = bus.getAuthorCCList(TextBoxPubId.Text, DropDownListPublicationEntry.SelectedValue);

        for (int i = 0; i < dy.Tables[0].Rows.Count; i++)
        {
            AuthorCCList.Add(dy.Tables[0].Rows[i]["EmailId"].ToString());
        }

        DataSet dZ = new DataSet();
        dZ = bus.getAuthorListName1(TextBoxPubId.Text, DropDownListPublicationEntry.SelectedValue);
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

            string id = TextBoxPubId.Text;
            User a2 = new User();
            a2 = B.GetFirstAuthorName(id, DropDownListPublicationEntry.SelectedValue);
            string FooterText = ConfigurationManager.AppSettings["FooterText"].ToString();

            details.FromEmail = ConfigurationManager.AppSettings["FromAddress"].ToString();
            details.Module = "PRVRT";
            details.EmailSubject = "Publication Entry  <  " + DropDownListPublicationEntry.SelectedValue + " _ " + TextBoxPubId.Text + "  > Status has been reverted to New. ";
            details.MsgBody = "<span style=\"font-size: 10pt; color: #3300cc; font-family: Verdana\"><h4>Dear " + a2.APrefix + "  " + a2.AFirstName + "  " + a2.AMiddleName + "  " + a2.ALastName + "</h4> <br>" +
                 "<b>This is to inform you that the article titled  '" + txtboxTitleOfWrkItem.Text + "' put back to editable status.<br>Please do the necessary changes and send it back to the approval.<br> " +
                 "<br>" + "Authors  : " + auhtorsSConc + "<br>" + "<br>" + "<br>" + "<br>" + "<br>" + FooterText + " </b><br><b> </b></span>";


            for (int i = 0; i < authorList.Count; i++)
            {
                // Msg.To.Add(BuyerId_Array[0]+dir_domain);
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
                   // details.ToEmail = email;
                }
                log.Info(" Email will be sent to authors '" + i + "' : '" + authorList[i] + "' ");
            }

            for (int i = 0; i < AuthorCCList.Count; i++)
            {
                // Msg.To.Add(BuyerId_Array[0]+dir_domain);
                string email = AuthorCCList[i].ToString();
                if (details.CCEmail != null)
                {
                    details.CCEmail = details.CCEmail + ',' + AuthorCCList[i].ToString();
                }
                else
                {
                    if (i == 0)
                    {
                        details.CCEmail = AuthorCCList[i].ToString();
                    }
                    else
                    {
                        details.CCEmail = details.CCEmail + ',' + AuthorCCList[i].ToString();
                    }
                   // details.CCEmail = email;
                }
                log.Info(" CC Email will be sent to authors '" + i + "' : '" + AuthorCCList[i] + "' ");
            }
            return details;
        }
        catch (Exception ex)
        {
            log.Error(ex.StackTrace);
            log.Error(ex.Message);
           // log.Error("Status has been reverted back to New, But Problem in Sending Mail! of Entry Type: " + DropDownListPublicationEntry.SelectedValue + " of ID: " + TextBoxPubId.Text);
            throw ex;
        }

    }
}