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
using System.Globalization;
public partial class Publicationentry : System.Web.UI.Page
{
    FeedbackClass feedback = new FeedbackClass();

    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    //string mainpath = ConfigurationManager.AppSettings["PdfPath"].ToString();
    string PublicationYearwebConfig = ConfigurationManager.AppSettings["PublicationYear"];
    string FolderPathServerWrite = ConfigurationManager.AppSettings["FolderPath"].ToString();
    // string PublicationYearAddwebConfig = ConfigurationManager.AppSettings["PublicationYearAdd"];


    Business B = new Business();
    Journal_DataObject JournalDataObj = new Journal_DataObject();
    JournalData JournalValueObj = new JournalData();
    public string pageID = "L42";
    public int count = 0;
    protected void Page_Load(object sender, EventArgs e)
    {      //popupPanelJournal.Visible = true;
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
        if (!IsPostBack)
        {
            //if (!Session["authPage"].ToString().Contains("$" + pageID + "$"))
            //{
            //    string unacces = "Unauthorized Acces!!! Login Again";
            //    Response.Redirect("~/Login.aspx?val=" + unacces);
            //}
            GridViewProject.DataSourceID = "SqlDataSourceProject";
            SqlDataSourceProject.DataBind();
            GridViewProject.DataBind();
            string month = DateTime.Now.Month.ToString();
            DropDownListMonthJA.SelectedValue = month;
            lblmsg.Visible = false;
            popupPanelJournal.Visible = false;
            popupPanelProceedingsJournal.Visible = false;
            CompareValidatorTextBoxDate.ValueToCompare = DateTime.Now.ToString("dd/MM/yyyy");
            //  CompareValidatorTextBoxDate1.ValueToCompare = DateTime.Now.ToString("dd/MM/yyyy");
            CompareValidatorTextBoxDateOfNewsPublish.ValueToCompare = DateTime.Now.ToString("dd/MM/yyyy");
            // string autoapp = Session["AutoApproval"].ToString();
            BtnFeedback.Visible = false;

            string Role = Session["Role"].ToString();
            if (Role != "1")
            {
                Business b = new Business();
                string AutoAppr = null;
                AutoAppr = b.GetIstDeptAutoApprove(Session["InstituteId"].ToString(), Session["Department"].ToString());
                if (AutoAppr == null)
                {
                    AutoAppr = "N";
                }



                if (AutoAppr == "Y")
                {
                    BtnFeedback.Text = "Save & Approve";
                }
                else
                {
                    BtnFeedback.Text = "Save & Submit for Approval";
                }
            }
            else
            {
                BtnFeedback.Text = "Save & Approve";

            }
            if (count == 0)
            {
                if (System.Web.HttpContext.Current.Request.QueryString["Module"] != null)
                {
                    count = 1;
                    string Module = Request.QueryString["Module"];
                    DropDownListPublicationEntry.SelectedValue = Module;
                    DropDownListPublicationEntryOnSelectedIndexChanged2(sender, e);

                }
            }
            LabelProjectReference.Visible = false;
            LabelProjectRef.Visible = false;
            LabelhasProjectreferenceNote.Visible = false;
            DropDownListhasProjectreference.Visible = false;
            LabelProjectDetails.Visible = false;
            TextBoxProjectDetails.Visible = false;
            ImageButtonProject.Visible = false;

        }


    }
    protected void DropDownListPublicationEntryOnSelectedIndexChanged2(object sender, EventArgs e)
    {
        panelFileUpload.Visible = true;
        //FileUploadPdf.Enabled = false;
        //Buttonupload.Enabled = false;
        PubEntriesUpdatePanel.Update();
        EditUpdatePanel.Update();
        //popupPanelAffilUpdate.Update();
        MainUpdate.Update();
        lblmsg2.Visible = false;
        panAddAuthor.Enabled = true;
        string Module = Request.QueryString["Module"];

        if (Module != null)
        {
            DropDownListPublicationEntry.Items.Clear();
            DropDownListPublicationEntry.Items.Add(new ListItem("Select", "", true));
            SqlDataSourcePublicationEntry.DataBind();
            DropDownListPublicationEntry.DataSourceID = "SqlDataSourcePublicationEntry";
            DropDownListPublicationEntry.DataBind();
            DropDownListPublicationEntry.SelectedValue = Module;
        }
        setModalWindow(sender, e);
        // popupstudent.Visible = false;
        // popupPanelAffil.Visible = true;
        btnSave.Enabled = true;
        SetInitialRow();
        Grid_AuthorEntry.Visible = true;

        addclikEntryType(sender, e);
        //DropDownListPublicationEntry.Attributes.Add("onchange", "ConfirmEntryType();");

        TextBoxYearJA.Items.Clear();
        TextBoxYear.Items.Clear();

        //string confirmValue2 = HiddenEntryConfirm.Value;
        //string confirmValue2 = Request.Form["HiddenEntryConfirm1"];
        if (DropDownListPublicationEntry.SelectedValue != " ")
        {

            if (DropDownListPublicationEntry.SelectedValue == "JA")
            {
                //DDLinstitutename.Items.Add(new ListItem("Select", "0", true));

                //  int yearadd = Convert.ToInt32( PublicationYearAddwebConfig);
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
                    TextBoxYearJA.Items.Add(new ListItem(yeatAppend.ToString(), yeatAppend.ToString(), true));
                }
                TextBoxYearJA.DataBind();
                TextBoxYearJA.SelectedValue = currenntYear.ToString();
                TextBoxPageTo.Enabled = false;
                panelJournalArticle.Visible = true;
                panelConfPaper.Visible = false;
                panelTechReport.Visible = false;
                panelBookPublish.Visible = false;
                panelOthes.Visible = false;
                panAddAuthor.Visible = true;
                lblnote1.Visible = true;
            }
            else if (DropDownListPublicationEntry.SelectedValue == "TS")
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
                    TextBoxYearJA.Items.Add(new ListItem(yeatAppend.ToString(), yeatAppend.ToString(), true));
                }
                TextBoxYearJA.DataBind();
                TextBoxYearJA.SelectedValue = currenntYear.ToString();
                panelTechReport.Visible = false;

                panelConfPaper.Visible = false;

                panelJournalArticle.Visible = true;

                TextBoxPageTo.Enabled = false;
                panelBookPublish.Visible = false;
                panelOthes.Visible = false;
                panAddAuthor.Visible = true;
                lblnote1.Visible = true;
            }
            BtnFeedback.Enabled = true;




            if (DropDownListPublicationEntry.SelectedValue == "CP")
            {
                panelConfPaper.Visible = true;

                panelJournalArticle.Visible = false;

                panelTechReport.Visible = false;
                panelBookPublish.Visible = false;
                panelOthes.Visible = false;
                panAddAuthor.Visible = true;
                lblnote1.Visible = false;
            }

            else if (DropDownListPublicationEntry.SelectedValue == "BK")
            {
                string month = DateTime.Now.Month.ToString();
                DropDownListBookMonth.SelectedValue = month;

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
                TextBoxYear.SelectedValue = currenntYear.ToString();

                panelBookPublish.Visible = true;

                panelTechReport.Visible = false;

                panelConfPaper.Visible = false;

                panelJournalArticle.Visible = false;

                panelOthes.Visible = false;
                panAddAuthor.Visible = true;
                lblnote1.Visible = false;
            }
            else if (DropDownListPublicationEntry.SelectedValue == "NM")
            {
                panelOthes.Visible = true;
                panelBookPublish.Visible = false;

                panelTechReport.Visible = false;

                panelConfPaper.Visible = false;

                panelJournalArticle.Visible = false;
                panAddAuthor.Visible = true;
                lblnote1.Visible = false;

            }
            else
            {
            }
            BtnFeedback.Enabled = true;

        }
        else
        {
            addclik(sender, e);
            //popup.Visible = false;
        }
        // BtnFeedback.Enabled = true;
        //lblmsg.Visible = false;
        RequiredFieldValidator1.Enabled = false;
        EnableValidation();
    }
    protected void GVViewFile_SelectedIndexChanged(object sender, EventArgs e)
    {
        PubEntriesUpdatePanel.Update();
        EditUpdatePanel.Update();
        //popupPanelAffilUpdate.Update();
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
            //Response.Write("<script>");
            //Response.Write("window.open('DisplayPdf.aspx?val=" + newpath + "','_blank')");
            ////path sent to display.aspx page
            //Response.Write("</script>");
        }

    }
    protected void ButtonSearchPubOnClick(object sender, EventArgs e)
    {
        addclik(sender, e);
        UpdatePanel1.Update();
        PubEntriesUpdatePanel.Update();
        EditUpdatePanel.Update();
        //popupPanelAffilUpdate.Update();
        MainUpdate.Update();
        btnSave.Enabled = false;
        BtnFeedback.Enabled = false;
        GridViewSearch.Visible = true;
        GridViewSearch.EditIndex = -1;
        panelFileUpload.Visible = false;
        // addclik(sender, e);
        dataBind();
        lblmsg.Visible = false;
        //popup.Visible = false;
        popupPanelJournal.Visible = false;
        popupPanelProceedingsJournal.Visible = false;
    }
    private void dataBind()
    {

        GridViewSearch.Visible = true;

        if (EntryTypesearch.SelectedValue != "A")
        {
            if (PubIDSearch.Text == "" && TextBoxWorkItemSearch.Text == "")
            {
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("TypeOfEntry", EntryTypesearch.SelectedValue);
                SqlDataSource1.SelectParameters.Add("UserId", Session["UserId"].ToString());

                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,t.EntryName,PubJournalID,PublishJAMonth,TitleWorkItem,c.PubCatName,PublishJAYear,ImpactFactor ,s.StatusName from Publication p,Status_Publication_M s , PubMUCategorization_M c ,PublicationTypeEntry_M t where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization and  TypeOfEntry=@TypeOfEntry and (Status='new' or Status='REW') and CreatedBy=@UserId ORDER BY PublicationID DESC";
            }
            else if (PubIDSearch.Text == "" && TextBoxWorkItemSearch.Text != "")
            {
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("TypeOfEntry", EntryTypesearch.SelectedValue);
                SqlDataSource1.SelectParameters.Add("TitleWorkItem", TextBoxWorkItemSearch.Text.Trim());
                SqlDataSource1.SelectParameters.Add("UserId", Session["UserId"].ToString());

                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,t.EntryName,PubJournalID,PublishJAMonth,TitleWorkItem,c.PubCatName,PublishJAYear,ImpactFactor  ,s.StatusName from Publication p,Status_Publication_M s , PubMUCategorization_M c,PublicationTypeEntry_M t  where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization and TypeOfEntry=@TypeOfEntry and (Status='new' or Status='REW' ) and TitleWorkItem like '%' + @TitleWorkItem + '%' and CreatedBy=@UserId ORDER BY PublicationID DESC";
                // SqlDataSource1.SelectCommand = "select j.Id as Id,Year,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,Comments from Journal_IF_Details j,Journal_M k where j.Id=k.Id and j.Id='" + txtJid.Text.Trim() + "'";
            }
            else if (PubIDSearch.Text != "" && TextBoxWorkItemSearch.Text == "")
            {
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("TypeOfEntry", EntryTypesearch.SelectedValue);
                SqlDataSource1.SelectParameters.Add("PublicationID", PubIDSearch.Text.Trim());
                SqlDataSource1.SelectParameters.Add("UserId", Session["UserId"].ToString());

                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,t.EntryName,PubJournalID,PublishJAMonth,TitleWorkItem,c.PubCatName,PublishJAYear,ImpactFactor  ,s.StatusName from Publication p,Status_Publication_M s , PubMUCategorization_M c,PublicationTypeEntry_M t  where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization and TypeOfEntry=@TypeOfEntry and (Status='new' or Status='REW' ) and PublicationID like '%' + @PublicationID + '%' and CreatedBy=@UserId ORDER BY PublicationID DESC ";
                // SqlDataSource1.SelectCommand = "select j.Id as Id,Year,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,Comments from Journal_IF_Details j,Journal_M k where j.Id=k.Id and j.Id='" + txtJid.Text.Trim() + "'";
            }
            else
            {
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("TypeOfEntry", EntryTypesearch.SelectedValue);
                SqlDataSource1.SelectParameters.Add("PublicationID", PubIDSearch.Text.Trim());
                SqlDataSource1.SelectParameters.Add("TitleWorkItem", TextBoxWorkItemSearch.Text.Trim());
                SqlDataSource1.SelectParameters.Add("UserId", Session["UserId"].ToString());

                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,t.EntryName,PubJournalID,PublishJAMonth,TitleWorkItem,c.PubCatName,PublishJAYear,ImpactFactor  ,s.StatusName from Publication p,Status_Publication_M s , PubMUCategorization_M c,PublicationTypeEntry_M t  where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization and TypeOfEntry=@TypeOfEntry and (Status='new' or Status='REW' ) and PublicationID like '%' + @PublicationID + '%'  and TitleWorkItem like '%' + @TitleWorkItem + '%'  and CreatedBy=@UserId ORDER BY PublicationID DESC ";
                // SqlDataSource1.SelectCommand = "select j.Id as Id,Year,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,Comments from Journal_IF_Details j,Journal_M k where j.Id=k.Id and j.Id='" + txtJid.Text.Trim() + "'";
            }
            GridViewSearch.DataBind();
            SqlDataSource1.DataBind();
        }
        else
        {
            if (PubIDSearch.Text == "" && TextBoxWorkItemSearch.Text == "")
            {
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("UserId", Session["UserId"].ToString());
                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,t.EntryName,PubJournalID,PublishJAMonth,TitleWorkItem,c.PubCatName,PublishJAYear,ImpactFactor  ,s.StatusName from Publication p,Status_Publication_M s , PubMUCategorization_M c ,PublicationTypeEntry_M t where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization and   ( Status='new' or Status='REW' ) and CreatedBy=@UserId ORDER BY PublicationID DESC ";
            }
            else if (PubIDSearch.Text == "" && TextBoxWorkItemSearch.Text != "")
            {
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("TitleWorkItem", TextBoxWorkItemSearch.Text.Trim());
                SqlDataSource1.SelectParameters.Add("UserId", Session["UserId"].ToString());
                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,t.EntryName,PubJournalID,PublishJAMonth,TitleWorkItem,c.PubCatName,PublishJAYear,ImpactFactor  ,s.StatusName from Publication p,Status_Publication_M s , PubMUCategorization_M c,PublicationTypeEntry_M t where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization and ( Status='new' or Status='REW' ) and TitleWorkItem like '%' + @TitleWorkItem + '%' and CreatedBy=@UserId ORDER BY PublicationID DESC ";
                // SqlDataSource1.SelectCommand = "select j.Id as Id,Year,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,Comments from Journal_IF_Details j,Journal_M k where j.Id=k.Id and j.Id='" + txtJid.Text.Trim() + "'";
            }
            else if (PubIDSearch.Text != "" && TextBoxWorkItemSearch.Text == "")
            {
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("PublicationID", PubIDSearch.Text.Trim());
                SqlDataSource1.SelectParameters.Add("UserId", Session["UserId"].ToString());
                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,t.EntryName,PubJournalID,PublishJAMonth,TitleWorkItem,c.PubCatName,PublishJAYear,ImpactFactor  ,s.StatusName from Publication p,Status_Publication_M s , PubMUCategorization_M c,PublicationTypeEntry_M t where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization and ( Status='new' or Status='REW' ) and PublicationID like '%' + @PublicationID + '%' and CreatedBy=@UserId ORDER BY PublicationID DESC ";
                // SqlDataSource1.SelectCommand = "select j.Id as Id,Year,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,Comments from Journal_IF_Details j,Journal_M k where j.Id=k.Id and j.Id='" + txtJid.Text.Trim() + "'";
            }
            else
            {
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("PublicationID", PubIDSearch.Text.Trim());
                SqlDataSource1.SelectParameters.Add("TitleWorkItem", TextBoxWorkItemSearch.Text.Trim());
                SqlDataSource1.SelectParameters.Add("UserId", Session["UserId"].ToString());
                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,t.EntryName,PubJournalID,PublishJAMonth,TitleWorkItem,c.PubCatName,PublishJAYear,ImpactFactor  ,s.StatusName from Publication p,Status_Publication_M s , PubMUCategorization_M c,PublicationTypeEntry_M t where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization and ( Status='new' or Status='REW' ) and PublicationID like '%' + @PublicationID + '%' and TitleWorkItem like '%' + @TitleWorkItem + '%' and CreatedBy=@UserId ORDER BY PublicationID DESC";
                // SqlDataSource1.SelectCommand = "select j.Id as Id,Year,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,Comments from Journal_IF_Details j,Journal_M k where j.Id=k.Id and j.Id='" + txtJid.Text.Trim() + "'";
            }
            GridViewSearch.DataBind();
            SqlDataSource1.DataBind();
        }
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

        //if (DropDownListPublicationEntry.SelectedValue == "JA")
        //{
        //    if (RadioButtonListIndexed.SelectedValue == "Y")
        //    {
        //        popupPanelProceedingsJournal.Visible = false;
        //        imageBkCbtn.Visible = true;
        //        imageBkCbtn1.Visible = false;
        //        popupPanelJournal.Visible = true;
        //        popGridJournal.DataSourceID = "SqlDataSourceJournal";
        //        SqlDataSourceJournal.DataBind();
        //        popGridJournal.DataBind();
        //    }
        //    else
        //    {
        //        popupPanelProceedingsJournal.Visible = false;
        //        popupPanelJournal.Visible = false;
        //        popGridJournal.DataSourceID = "SqlDataSourceJournal";
        //        SqlDataSourceJournal.DataBind();
        //        popGridJournal.DataBind();
        //    }
        //}
        //else if (DropDownListPublicationEntry.SelectedValue == "PR")
        //{

        //    if (RadioButtonListIndexed.SelectedValue == "Y")
        //    {
        //        imageBkCbtn.Visible = false;
        //        imageBkCbtn1.Visible = true;
        //        popupPanelJournal.Visible = false;
        //        popupPanelProceedingsJournal.Visible = true;
        //        popGridJournalProceedings.DataSourceID = "SqlDataSourceProceedings";
        //        SqlDataSourceProceedings.DataBind();
        //        popGridJournalProceedings.DataBind();
        //    }
        //    else
        //    {
        //        popupPanelJournal.Visible = false;
        //        popupPanelProceedingsJournal.Visible = false;
        //        popGridJournalProceedings.DataSourceID = "SqlDataSourceProceedings";
        //        SqlDataSourceProceedings.DataBind();
        //        popGridJournalProceedings.DataBind();
        //    }
        //}
        UpdatePanel2.Update();
        popupPanelAffil.Visible = true;
        popGridAffil.DataSourceID = "SqlDataSourceAffil";
        SqlDataSourceAffil.DataBind();
        popGridAffil.DataBind();
        int rows = popGridAffil.Rows.Count;
        popGridAffil.Visible = true;
       
        //popGridAffil.DataSourceID = "SqlDataSourceAffil";
        //SqlDataSourceAffil.DataBind();
        //popGridAffil.DataBind();
        //// popupstudent.Visible = false;
        //popupStudentGrid.DataSourceID = "StudentSQLDS";
        //StudentSQLDS.DataBind();
        //popupStudentGrid.DataBind();


    }

    protected void TextBoxCreditPointOnTextChanged(object sender, EventArgs e)
    {
        int creditpoint = 0;
        creditpoint = Convert.ToInt32(TextBoxCreditPoint.Text.Trim());

        if (creditpoint > 0)
        {
            TextBoxAwardedBy.Enabled = true;
        }
        else
        {
            TextBoxAwardedBy.Text = "";
            TextBoxAwardedBy.Enabled = false;
        }
    }
    protected void popSelected(Object sender, EventArgs e)
    {

        UpdatePanel2.Update();
        MainUpdate.Update();
        EditUpdatePanel.Update();
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
        ScriptManager.RegisterStartupScript(this, this.GetType(), "CallMyFunction", "ToggleDisplay2()", true);
    }
    protected void popSelectedProceeding(Object sender, EventArgs e)
    {

        UpdatePanel2.Update();
        MainUpdate.Update();
        EditUpdatePanel.Update();
        UpdatePanel5.Update();
        popGridJournalProceedings.Visible = true;
        GridViewRow row = popGridJournalProceedings.SelectedRow;

        string Journalid = row.Cells[1].Text;

        string Journalname = row.Cells[2].Text;

        TextBoxPubJournal.Text = Journalid;
        TextBoxNameJournal.Text = Journalname;

        journalcodeSrch.Text = "";
        popGridJournalProceedings.DataBind();
        ProceedingIDTextChanged(sender, e);

        string year = DateTime.Now.Year.ToString();
        int Jyear = Convert.ToInt32(year) - 1;
        // txtboxYear.Text = Jyear.ToString();


        affiliateSrch.Text = "";
        popGridAffil.DataBind();

        journalcodeSrch.Text = "";
        popGridJournalProceedings.DataBind();

        JournalValueObj.year = TextBoxYearJA.SelectedValue;
        JournalValueObj.JournalID = TextBoxPubJournal.Text;
        JournalValueObj.month = DropDownListMonthJA.SelectedValue;
        if (TextBoxYearJA.SelectedValue != "")
        {

            JournalData j = new JournalData();
            j = B.GetImpactFactorApplicableYearProceeding(JournalValueObj);
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
            jd = B.ProceedingGetImpactFactorPublishEntry(JournalValueObj);

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
        ScriptManager.RegisterStartupScript(this, this.GetType(), "CallMyFunction", "ToggleDisplay3()", true);

    }
    protected void popSelectedProject(Object sender, EventArgs e)
    {
        GridViewProject.Visible = true;
        GridViewRow row = GridViewProject.SelectedRow;
    }
    protected void popSelected1(Object sender, EventArgs e)
    {
        PubEntriesUpdatePanel.Update();
        EditUpdatePanel.Update();

        //popupPanelAffilUpdate.Update();
        MainUpdate.Update();
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

        //string dir_domain = ConfigurationManager.AppSettings["DirectoryDomain"].ToString();

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

        //if (DropDownListPublicationEntry.SelectedValue == "JA")
        //{

        //    journalcodeSrch.Text = "";
        //    popGridJournal.DataBind();
        //    //.Visible = false;
        //}
        //else if (DropDownListPublicationEntry.SelectedValue == "PR")
        //{
        //    journalcodeSrchproceeding.Text = "";
        //    popGridJournalProceedings.DataBind();
        //    //popupPanelProceedingsJournal.Visible = false;
        //}

        TextBox NameInJournal = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("NameInJournal");
        NameInJournal.Text = a.UserNamePrefix + " " + a.UserFirstName + " " + a.UserMiddleName + " " + a.UserLastName;

        affiliateSrch.Text = "";
        popGridAffil.DataBind();
        //ModalPopupExtender1.Hide();
        //ModalPopupExtender3.Hide();
        //popGridAffil.Visible = false;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "CallMyFunction", "ToggleDisplay()", true);
    }

    /* Pop--JournalCodeChanged */
    protected void JournalCodeChanged(object sender, EventArgs e)
    {

        if (journalcodeSrch.Text.Trim() == "")
        {
            SqlDataSourceJournal.SelectCommand = "SELECT  Id as ISSN,Title,AbbreviatedTitle FROM [Journal_M]";
            popGridJournal.DataBind();
            popGridJournal.Visible = true;
        }

        else
        {
            SqlDataSourceJournal.SelectParameters.Clear();
            SqlDataSourceJournal.SelectParameters.Add("Title", journalcodeSrch.Text);

            SqlDataSourceJournal.SelectCommand = "SELECT Id as ISSN,Title,AbbreviatedTitle FROM [Journal_M] where Title like '%' + @Title + '%'";
            popGridJournal.DataBind();
            popGridJournal.Visible = true;
        }

        //popupPanelAffil.Visible = false;

        //ModalPopupExtender1.Show();
    }

    protected void RadioButtonListIndexedOnSelectedIndexChanged(object sender, EventArgs e)
    {
        EnableValidation();
        TextBoxPubJournal.Text = "";
        TextBoxNameJournal.Text = "";
        TextBoxImpFact.Text = "";
        TextBoxImpFact5.Text = "";
        txtIFApplicableYear.Text = "";
        if (DropDownListPublicationEntry.SelectedValue == "JA")
        {
            if (RadioButtonListIndexed.SelectedValue == "N")
            {
                popupPanelProceedingsJournal.Visible = false;
                popupPanelJournal.Visible = false;
                CheckboxIndexAgency.Enabled = false;
                CheckboxIndexAgency.ClearSelection();
                imageBkCbtn.Visible = false;
                imageBkCbtn1.Visible = false;
                TextBoxNameJournal.Enabled = true;
                // lblmsgpubnonondexed.Visible = true;
                //TextBoxPubJournal.ReadOnly = false;
                CustomValidator1.Enabled = false;
            }
            else
            {
                popupPanelProceedingsJournal.Visible = false;
                popupPanelJournal.Visible = true;
                // TextBoxPubJournal.ReadOnly = true;
                imageBkCbtn.Visible = true;
                imageBkCbtn.Enabled = true;
                imageBkCbtn1.Visible = false;
                imageBkCbtn1.Enabled = false;
                CheckboxIndexAgency.Enabled = true;
                TextBoxNameJournal.Enabled = false;
                // lblmsgpubnonondexed.Visible = false;
                CustomValidator1.Enabled = true;

            }
        }
        else if (DropDownListPublicationEntry.SelectedValue == "PR")
        {
            if (RadioButtonListIndexed.SelectedValue == "N")
            {
                popupPanelJournal.Visible = false;
                popupPanelProceedingsJournal.Visible = false;
                CheckboxIndexAgency.Enabled = false;
                CheckboxIndexAgency.ClearSelection();
                imageBkCbtn1.Visible = false;
                imageBkCbtn.Visible = false;
                TextBoxNameJournal.Enabled = true;
                // lblmsgpubnonondexed.Visible = true;
                //TextBoxPubJournal.ReadOnly = false;
                CustomValidator1.Enabled = false;
            }
            else
            {
                popupPanelJournal.Visible = false;
                popupPanelProceedingsJournal.Visible = true;
                // TextBoxPubJournal.ReadOnly = true;
                imageBkCbtn.Visible = false;
                imageBkCbtn.Enabled = false;
                imageBkCbtn1.Enabled = true;
                imageBkCbtn1.Visible = true;
                CheckboxIndexAgency.Enabled = true;
                TextBoxNameJournal.Enabled = false;
                // lblmsgpubnonondexed.Visible = false;
                CustomValidator1.Enabled = true;

            }
        }
    }
    protected void RadioButtonListCPIndexedOnSelectedIndexChanged(object sender, EventArgs e)
    {

        if (RadioButtonListCPIndexed.SelectedValue == "N")
        {
            CheckBoxListCPIndexedIn.Enabled = false;
            CheckBoxListCPIndexedIn.ClearSelection();
            CustomValidator1.Enabled = false;
        }
        else
        {

            CheckBoxListCPIndexedIn.Enabled = true;
            CustomValidator1.Enabled = true;

        }
    }
    protected void JournalIDTextChanged(object sender, EventArgs e)
    {
        if (RadioButtonListIndexed.SelectedValue == "Y")
        {

            JournalValueObj.JournalID = TextBoxPubJournal.Text;
            // JournalValueObj.year = txtBoxYear.Text;
            JournalData j = new JournalData();
            j = B.JournalEntryCheckExistance(JournalValueObj);

            if (j.jid != null)
            {
                // ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Entry ALready Exists')</script>");
                TextBoxNameJournal.Text = j.name;

                string year = DateTime.Now.Year.ToString();
                //  int Jyear = Convert.ToInt32(year) - 1;

                //TextBoxYearJA.SelectedValue = year.ToString();
                // txtboxYear_TextChanged(sender, e);
                JournalData j2 = new JournalData();
                JournalValueObj.year = TextBoxYearJA.SelectedValue;
                j2 = B.JournalYearwiseCheck(JournalValueObj);
                if (j2.jid != null)
                {


                    JournalValueObj.year = TextBoxYearJA.SelectedValue;
                    JournalValueObj.JournalID = TextBoxPubJournal.Text;
                    JournalValueObj.month = DropDownListMonthJA.SelectedValue;
                    if (TextBoxYearJA.SelectedValue != "")
                    {

                        JournalData j1 = new JournalData();
                        j1 = B.GetImpactFactorApplicableYear(JournalValueObj);
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
                        jd = B.JournalGetImpactFactorPublishEntry(JournalValueObj);
                        int jayear = Convert.ToInt16(TextBoxYearJA.SelectedValue);
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
                    Business b1 = new Business();
                    bool resultv = B.checkPredatoryJournal(TextBoxPubJournal.Text, TextBoxYearJA.SelectedValue);
                    if (resultv == true)
                    {
                        hdnPredatoryJournal.Value = "PRD";
                    }
                    else
                    {
                        hdnPredatoryJournal.Value = "";
                    }
                }
                else
                {
                    hdnPredatoryJournal.Value = "";
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
                hdnPredatoryJournal.Value = "";
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
    protected void ProceedingIDTextChanged(object sender, EventArgs e)
    {
        if (RadioButtonListIndexed.SelectedValue == "Y")
        {

            JournalValueObj.JournalID = TextBoxPubJournal.Text;
            // JournalValueObj.year = txtBoxYear.Text;
            JournalData j = new JournalData();
            j = B.ProceedingEntryCheckExistance(JournalValueObj);

            if (j.jid != null)
            {
                // ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Entry ALready Exists')</script>");
                TextBoxNameJournal.Text = j.name;

                string year = DateTime.Now.Year.ToString();
                //  int Jyear = Convert.ToInt32(year) - 1;

                //TextBoxYearJA.SelectedValue = year.ToString();
                // txtboxYear_TextChanged(sender, e);
                JournalData j2 = new JournalData();
                JournalValueObj.year = TextBoxYearJA.SelectedValue;
                j2 = B.ProceedingYearwiseCheck(JournalValueObj);
                if (j2.jid != null)
                {


                    JournalValueObj.year = TextBoxYearJA.SelectedValue;
                    JournalValueObj.JournalID = TextBoxPubJournal.Text;
                    JournalValueObj.month = DropDownListMonthJA.SelectedValue;
                    if (TextBoxYearJA.SelectedValue != "")
                    {

                        JournalData j1 = new JournalData();
                        j1 = B.GetImpactFactorApplicableYearProceeding(JournalValueObj);
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
                        jd = B.ProceedingGetImpactFactorPublishEntry(JournalValueObj);
                        int jayear = Convert.ToInt16(TextBoxYearJA.SelectedValue);
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
                    //Business b1 = new Business();
                    //bool resultv = B.checkPredatoryJournal(TextBoxPubJournal.Text, TextBoxYearJA.SelectedValue);
                    //if (resultv == true)
                    //{
                    //    hdnPredatoryJournal.Value = "PRD";
                    //}
                    //else
                    //{
                    //    hdnPredatoryJournal.Value = "";
                    //}
                }
                else
                {
                    //hdnPredatoryJournal.Value = "";
                    TextBoxImpFact.Text = "";
                    TextBoxImpFact5.Text = "";
                    txtIFApplicableYear.Text = "";
                    TextBoxPubJournal.Text = "";
                    TextBoxNameJournal.Text = "";
                    TextBoxNameJournal.Enabled = false;
                    //string CloseWindow1 = "alert('Journal Not Found')";
                    //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow1, true);
                    // ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Journal Not Found')</script>");
                }

            }
            else
            {
                //hdnPredatoryJournal.Value = "";
                TextBoxImpFact.Text = "";
                TextBoxImpFact5.Text = "";
                txtIFApplicableYear.Text = "";
                TextBoxPubJournal.Text = "";
                TextBoxNameJournal.Enabled = false;
                //string CloseWindow1 = "alert('Journal Not Found')";
                //ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow1, true);
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
    protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //Find the DropDownList in the Row
            DropDownList DropdownMuNonMu = (e.Row.FindControl("DropdownMuNonMu") as DropDownList);

            if (DropDownListPublicationEntry.SelectedValue == "BK" || DropDownListPublicationEntry.SelectedValue == "NM")
            {


                SqlDataSourceAuthorType.SelectCommand = "SELECT Id,Type FROM [Author_Type_M] where (Id! = 'O') and (Id! = 'E') order by DisplayNumber asc";

                DropdownMuNonMu.DataTextField = "Type";
                DropdownMuNonMu.DataValueField = "Id";
                DropdownMuNonMu.DataBind();


            }
            else if (DropDownListPublicationEntry.SelectedValue == "JA" || DropDownListPublicationEntry.SelectedValue == "TS" || DropDownListPublicationEntry.SelectedValue == "CP" || DropDownListPublicationEntry.SelectedValue == "PR")
            {
                SqlDataSourceAuthorType.SelectCommand = "SELECT Id,DisplayName FROM [Author_Type_M] where (Id = 'M') or (Id = 'S') or (Id = 'N') or (Id = 'O')or (Id = 'E') order by DisplayNumber asc";

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

        NationalType.Visible = false;
        ContinentId.Visible = false;
        EmployeeCode.Enabled = false;
        DropdownMuNonMu.Enabled = false;
        AuthorName.Enabled = false;
        EmployeeCodeBtn.Enabled = false;

        if (DropDownListPublicationEntry.SelectedValue == "BK" || DropDownListPublicationEntry.SelectedValue == "NM")
        {


            SqlDataSourceAuthorType.SelectCommand = "SELECT Id,Type FROM [Author_Type_M] where (Id! = 'O')and(Id! = 'E') order by DisplayNumber asc";

            DropdownMuNonMu.DataTextField = "Type";
            DropdownMuNonMu.DataValueField = "Id";
            DropdownMuNonMu.DataBind();

        }
        else if (DropDownListPublicationEntry.SelectedValue == "JA" || DropDownListPublicationEntry.SelectedValue == "TS" || DropDownListPublicationEntry.SelectedValue == "CP" || DropDownListPublicationEntry.SelectedValue == "PR")
        {
            SqlDataSourceAuthorType.SelectCommand = "SELECT Id,DisplayName FROM [Author_Type_M] order by DisplayNumber asc";
            DropdownMuNonMu.DataTextField = "DisplayName";
            DropdownMuNonMu.DataValueField = "Id";
            DropdownMuNonMu.DataBind();

        }

        if (DropDownListPublicationEntry.SelectedValue == "JA" || DropDownListPublicationEntry.SelectedValue == "TS" || DropDownListPublicationEntry.SelectedValue == "PR")
        {
            DropdownMuNonMu.SelectedValue = "M";
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
            DropdownMuNonMu.SelectedValue = "M";
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



        if (DropDownListPublicationEntry.SelectedValue == "JA" || DropDownListPublicationEntry.SelectedValue == "TS" || DropDownListPublicationEntry.SelectedValue == "PR")
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
            Grid_AuthorEntry.Columns[1].Visible = true;
        }
        else if (DropDownListPublicationEntry.SelectedValue == "BK")
        {
            isCorrAuth.Visible = false;
            AuthorType.Visible = true;
            NameInJournal.Visible = false;
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
    protected void DropdownMuNonMuOnSelectedIndexChanged(object sender, EventArgs e)
    {
        PubEntriesUpdatePanel.Update();
        EditUpdatePanel.Update();
        //popupPanelAffilUpdate.Update();
        MainUpdate.Update();
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
        TextBox NameInJournal = (TextBox)currentRow.FindControl("NameInJournal");
        ImageButton ImageButton1 = (ImageButton)currentRow.FindControl("ImageButton1");

        if (DropdownMuNonMu.SelectedValue == "M")
        {
            DropdownStudentInstitutionName1.Visible = false;
            DropdownStudentDepartmentName.Visible = false;
            InstitutionName.Visible = true;
            DepartmentName.Visible = true;
            NationalType.Visible = false;
            ContinentId.Visible = false;
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
            if (DropDownListPublicationEntry.SelectedValue == "JA" || DropDownListPublicationEntry.SelectedValue == "TS" || DropDownListPublicationEntry.SelectedValue == "CP" || DropDownListPublicationEntry.SelectedValue == "PR")
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
            // ContinentId.Visible = false;
            MailId.Enabled = true;
            AuthorName.Text = "";
            MailId.Text = "";
            InstitutionName.Text = "";
            DepartmentName.Text = "";
            EmployeeCode.Text = "";
            NameInJournal.Text = "";
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
        if (DropDownListPublicationEntry.SelectedValue == "JA")
        {
            if (RadioButtonListIndexed.SelectedValue == "Y")
            {
                popupPanelProceedingsJournal.Visible = false;
                popupPanelJournal.Visible = true;
                popGridJournal.DataSourceID = "SqlDataSourceJournal";
                SqlDataSourceJournal.DataBind();
                popGridJournal.DataBind();
            }
            else
            {
                popupPanelProceedingsJournal.Visible = false;
                popupPanelJournal.Visible = false;
                popGridJournal.DataSourceID = "SqlDataSourceJournal";
                SqlDataSourceJournal.DataBind();
                popGridJournal.DataBind();
            }
        }
        else if (DropDownListPublicationEntry.SelectedValue == "PR")
        {
            if (RadioButtonListIndexed.SelectedValue == "Y")
            {
                popupPanelJournal.Visible = false;
                popupPanelProceedingsJournal.Visible = true;
                popGridJournalProceedings.DataSourceID = "SqlDataSourceProceedings";
                SqlDataSourceProceedings.DataBind();
                popGridJournalProceedings.DataBind();
            }
            else
            {
                popupPanelJournal.Visible = false;
                popupPanelProceedingsJournal.Visible = false;
                popGridJournalProceedings.DataSourceID = "SqlDataSourceProceedings";
                SqlDataSourceProceedings.DataBind();
                popGridJournalProceedings.DataBind();
            }
        }
        //popupPanelAffil.Visible = true;
        //popGridAffil.DataSourceID = "SqlDataSourceAffil";
        //SqlDataSourceAffil.DataBind();
        //popGridAffil.DataBind();
        //popupstudent.Visible = true;
        //popupStudentGrid.DataSourceID = "StudentSQLDS";
        //StudentSQLDS.DataBind();
        //popupStudentGrid.DataBind();

        //if (DropDownListPublicationEntry.SelectedValue == "JA" || DropDownListPublicationEntry.SelectedValue == "TS")
        //{
        //    if (DropdownMuNonMu.SelectedValue == "S")
        //    {
        //        popupPanelAffil.Visible = false;
        //       popGridAffil.DataSourceID = "SqlDataSourceAffil";
        //        SqlDataSourceAffil.DataBind();
        //        popGridAffil.DataBind();
        //        popupstudent.Visible = true;
        //        popupStudentGrid.DataSourceID = "StudentSQLDS";
        //        StudentSQLDS.DataBind();
        //        popupStudentGrid.DataBind();
        //        //popupPanelJournal.Visible = true;
        //        //popGridJournal.DataSourceID = "SqlDataSourceJournal";
        //        //SqlDataSourceJournal.DataBind();
        //        //popGridJournal.DataBind();
        //    }
        //    else if (DropdownMuNonMu.SelectedValue == "M")
        //    {
        //        popupPanelAffil.Visible = true;
        //        popGridAffil.DataSourceID = "SqlDataSourceAffil";
        //        SqlDataSourceAffil.DataBind();
        //        popGridAffil.DataBind();
        //        popupstudent.Visible = false;
        //        popupStudentGrid.DataSourceID = "StudentSQLDS";
        //        StudentSQLDS.DataBind();
        //        popupStudentGrid.DataBind();
        //        //popupPanelJournal.Visible = true;
        //        //popGridJournal.DataSourceID = "SqlDataSourceJournal";
        //        //SqlDataSourceJournal.DataBind();
        //        //popGridJournal.DataBind();
        //    }
        //    else
        //    {
        //       //// popupPanelAffil.Visible = false;
        //       // popGridAffil.DataSourceID = "SqlDataSourceAffil";
        //       // SqlDataSourceAffil.DataBind();
        //       // popGridAffil.DataBind();
        //       // //popupstudent.Visible = false;
        //       // popupStudentGrid.DataSourceID = "StudentSQLDS";
        //       // StudentSQLDS.DataBind();
        //       // popupStudentGrid.DataBind();
        //       //// popupPanelJournal.Visible = false;
        //       // popGridJournal.DataSourceID = "SqlDataSourceJournal";
        //       // SqlDataSourceJournal.DataBind();
        //       // popGridJournal.DataBind();
        //    }

        //}
        //else
        //{
        //    popupPanelAffil.Visible = true;
        //    popGridAffil.DataSourceID = "SqlDataSourceAffil";
        //    SqlDataSourceAffil.DataBind();
        //    popGridAffil.DataBind();
        //    popupstudent.Visible = false;
        //    popupStudentGrid.DataSourceID = "StudentSQLDS";
        //    StudentSQLDS.DataBind();
        //    popupStudentGrid.DataBind();
        //}
    }

    // change ------------Shilpa
    protected void AuthorName_Changed(object sender, EventArgs e)
    {
        PubEntriesUpdatePanel.Update();
        EditUpdatePanel.Update();
        //popupPanelAffilUpdate.Update();
        MainUpdate.Update();
        GridViewRow currentRow = (GridViewRow)((TextBox)sender).Parent.Parent;
        TextBox AuthorName = (TextBox)currentRow.FindControl("AuthorName");
        TextBox NameInJournal = (TextBox)currentRow.FindControl("NameInJournal");
        TextBox InstitutionName = (TextBox)currentRow.FindControl("InstitutionName");

        //InstitutionName.Focus();
        NameInJournal.Text = AuthorName.Text;



    }




    protected void addRow(object sender, EventArgs e)
    {



        PubEntriesUpdatePanel.Update();
        EditUpdatePanel.Update();
        //popupPanelAffilUpdate.Update();
        MainUpdate.Update();

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

                        drCurrentRow = dtCurrentTable.NewRow();

                        //   dtCurrentTable.Rows[i - 1]["amount"] = amount.Text.Trim() == "" ? 0 : Convert.ToDecimal(amount.Text);
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
                            if (DropDownListPublicationEntry.SelectedValue == "JA" || DropDownListPublicationEntry.SelectedValue == "TS" || DropDownListPublicationEntry.SelectedValue == "CP" || DropDownListPublicationEntry.SelectedValue == "PR")
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

        //setModalWindow(sender, e); // initialise popup gridviews

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



                    if (DropDownListPublicationEntry.SelectedValue == "JA" || DropDownListPublicationEntry.SelectedValue == "TS" || DropDownListPublicationEntry.SelectedValue == "CP" || DropDownListPublicationEntry.SelectedValue == "PR")
                    {
                        if (DropdownMuNonMu1.SelectedValue == "N")
                        {
                            MailId1.Enabled = true;
                            AuthorName1.Enabled = true;
                            EmployeeCodeBtnImg1.Enabled = false;
                            DropdownMuNonMu1.Enabled = false;
                            EmployeeCode.Enabled = false;

                            InstitutionName1.Enabled = true;

                            DepartmentName1.Enabled = true;
                        }
                        else if (DropdownMuNonMu1.SelectedValue == "E")
                        {
                            MailId1.Enabled = true;
                            AuthorName1.Enabled = true;
                            EmployeeCodeBtnImg1.Enabled = false;
                            DropdownMuNonMu1.Enabled = false;
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
                                DropdownMuNonMu1.Enabled = false;
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
                                DropdownMuNonMu1.Enabled = false;
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
                        DropdownMuNonMu1.Enabled = false;
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

                    if (DropDownListPublicationEntry.SelectedValue == "JA" || DropDownListPublicationEntry.SelectedValue == "TS" || DropDownListPublicationEntry.SelectedValue == "PR")
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
                        Grid_AuthorEntry.Columns[1].Visible = true;
                    }
                    else if (DropDownListPublicationEntry.SelectedValue == "BK")
                    {
                        isCorrAuth.Visible = false;
                        AuthorType.Visible = true;
                        NameInJournal.Visible = false;
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
                        if (DropDownListPublicationEntry.SelectedValue == "JA" || DropDownListPublicationEntry.SelectedValue == "TS" || DropDownListPublicationEntry.SelectedValue == "CP" || DropDownListPublicationEntry.SelectedValue == "PR")
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
                    if (dt.Rows[i]["HasAttented"].ToString() == "Y")
                    {
                        HasAttented.Checked = true;
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

                    if (DropDownListPublicationEntry.SelectedValue == "JA" || DropDownListPublicationEntry.SelectedValue == "TS" || DropDownListPublicationEntry.SelectedValue == "PR")
                    {
                        if (DropdownMuNonMu1.Text == "M")
                        {
                            EmployeeCodeBtnImg1.Enabled = true;
                        }
                        else if (DropdownMuNonMu1.Text == "N")
                        {
                            EmployeeCodeBtnImg1.Enabled = false;
                        }
                        else if (DropdownMuNonMu1.Text == "E")
                        {
                            EmployeeCodeBtnImg1.Enabled = false;
                        }
                        else if (DropdownMuNonMu1.Text == "S")
                        {
                            EmployeeCodeBtnImg1.Enabled = true;
                        }
                        else if (DropdownMuNonMu1.Text == "O")
                        {
                            EmployeeCodeBtnImg1.Enabled = false;
                        }

                        DropdownMuNonMu1.Enabled = false;
                    }
                    else
                    {
                        DropdownMuNonMu1.Enabled = false;
                    }
                    if (Grid_AuthorEntry.Rows.Count == 1)
                    {
                        DropDownList DropdownMuNonMu5 = (DropDownList)Grid_AuthorEntry.Rows[0].Cells[2].FindControl("DropdownMuNonMu");
                        if (DropDownListPublicationEntry.SelectedValue == "JA" || DropDownListPublicationEntry.SelectedValue == "TS" || DropDownListPublicationEntry.SelectedValue == "PR")
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




    protected void Grid_AuthorEntry_RowDeleting(Object sender, GridViewDeleteEventArgs e)
    {
        PubEntriesUpdatePanel.Update();
        EditUpdatePanel.Update();
        //popupPanelAffilUpdate.Update();
        MainUpdate.Update();
        SetRowData();
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            DataRow drCurrentRow = null;
            int rowIndex = Convert.ToInt32(e.RowIndex);
            if (DropDownListPublicationEntry.SelectedValue == "JA" || DropDownListPublicationEntry.SelectedValue == "TS" || DropDownListPublicationEntry.SelectedValue == "PR")
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
                        if (DropDownListPublicationEntry.SelectedValue == "JA" || DropDownListPublicationEntry.SelectedValue == "TS" || DropDownListPublicationEntry.SelectedValue == "PR")
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
                    if (DropDownListPublicationEntry.SelectedValue == "JA" || DropDownListPublicationEntry.SelectedValue == "TS" || DropDownListPublicationEntry.SelectedValue == "PR")
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




    protected void txtboxYear_TextChanged1(object sender, EventArgs e)
    {

        JournalValueObj.year = TextBoxYearJA.SelectedValue;
        JournalValueObj.JournalID = TextBoxPubJournal.Text;
        JournalData j = new JournalData();
        if (TextBoxYearJA.SelectedValue != "")
        {


            j = B.JournalGetImpactFactor(JournalValueObj);
            if (j.jid != null)
            { TextBoxImpFact.Text = JournalValueObj.impctfact.ToString(); }
        }
        else
        {
            TextBoxImpFact.Text = "";
        }

    }


    protected void txtboxYear_TextChanged(object sender, EventArgs e)
    {
        TextBoxPubJournal.Text = "";
        TextBoxNameJournal.Text = "";
        TextBoxImpFact.Text = "";
        TextBoxImpFact5.Text = "";
        txtIFApplicableYear.Text = "";
        txtquartile.Text = "";
        int rowindex3 = 0;
        if (DropDownListPublicationEntry.SelectedValue == "JA" || DropDownListPublicationEntry.SelectedValue == "TS" || DropDownListPublicationEntry.SelectedValue == "PR")
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
                        string publicationyear = TextBoxYearJA.SelectedValue;
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
        bool resultv = B.checkPredatoryJournal(TextBoxPubJournal.Text, TextBoxYearJA.SelectedValue);
        if (resultv == true)
        {
            hdnPredatoryJournal.Value = "PRD";
        }
        else
        {
            hdnPredatoryJournal.Value = "";
        }
    }
    protected void showPop(object sender, EventArgs e)
    {
        if (DropDownListPublicationEntry.SelectedValue == "JA")
        {
            popupPanelJournal.Visible = true;
            popupPanelProceedingsJournal.Visible = false;
            //ModalPopupExtender1.Show();
            //ModalPopupExtender3.Hide();
            //popup.Visible = false;
        }
        else if (DropDownListPublicationEntry.SelectedValue == "PR")
        {
            popupPanelJournal.Visible = false;
            popupPanelProceedingsJournal.Visible = true;
            //ModalPopupExtender3.Show();
            //ModalPopupExtender1.Hide();
            //popup.Visible = false;
        }

    }
    
    protected void DropDownListPublicationEntryOnSelectedIndexChanged(object sender, EventArgs e)
    {
        panelFileUpload.Visible = true;
        FileUploadPdf.Enabled = false;
        Buttonupload.Enabled = false;
        PubEntriesUpdatePanel.Update();
        EditUpdatePanel.Update();
        //popupPanelAffilUpdate.Update();
        MainUpdate.Update();
        lblmsg2.Visible = false;
        panAddAuthor.Enabled = true;
        setModalWindow(sender, e);
        // popupstudent.Visible = false;
        // popupPanelAffil.Visible = true;
        btnSave.Enabled = true;
        SetInitialRow();
        Grid_AuthorEntry.Visible = true;

        addclikEntryType(sender, e);
        //DropDownListPublicationEntry.Attributes.Add("onchange", "ConfirmEntryType();");

        TextBoxYearJA.Items.Clear();
        TextBoxYear.Items.Clear();

        //string confirmValue2 = HiddenEntryConfirm.Value;
        //string confirmValue2 = Request.Form["HiddenEntryConfirm1"];
        if (DropDownListPublicationEntry.SelectedValue != " ")
        {
            string confirmValue2 = HiddenEntryConfirm1.Value;
            if (confirmValue2 == "Yes")
            {
                if (DropDownListPublicationEntry.SelectedValue == "JA")
                {
                    //DDLinstitutename.Items.Add(new ListItem("Select", "0", true));

                    //  int yearadd = Convert.ToInt32( PublicationYearAddwebConfig);
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
                        TextBoxYearJA.Items.Add(new ListItem(yeatAppend.ToString(), yeatAppend.ToString(), true));
                    }
                    TextBoxYearJA.DataBind();
                    TextBoxYearJA.SelectedValue = currenntYear.ToString();
                    TextBoxPageTo.Enabled = false;
                    panelJournalArticle.Visible = true;
                    panelConfPaper.Visible = false;
                    panelTechReport.Visible = false;
                    panelBookPublish.Visible = false;
                    panelOthes.Visible = false;
                    panAddAuthor.Visible = true;
                    lblnote1.Visible = true;
                    LabelProjectReference.Visible = true;
                    LabelProjectRef.Visible = true;
                    LabelhasProjectreferenceNote.Visible = true;
                    DropDownListhasProjectreference.Visible = true;
                    //LabelProjectRef.Visible = true;
                    DropDownListhasProjectreference.SelectedValue = "0";
                    LabelProjectDetails.Visible = false;
                    TextBoxProjectDetails.Visible = false;
                    ImageButtonProject.Visible = false;

                }
                else if (DropDownListPublicationEntry.SelectedValue == "TS")
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
                        TextBoxYearJA.Items.Add(new ListItem(yeatAppend.ToString(), yeatAppend.ToString(), true));
                    }
                    TextBoxYearJA.DataBind();
                    TextBoxYearJA.SelectedValue = currenntYear.ToString();
                    panelTechReport.Visible = false;

                    panelConfPaper.Visible = false;

                    panelJournalArticle.Visible = true;
                    imageBkCbtn1.Visible = false;
                    imageBkCbtn.Visible = true;
                    TextBoxPageTo.Enabled = false;
                    panelBookPublish.Visible = false;
                    panelOthes.Visible = false;
                    panAddAuthor.Visible = true;
                    lblnote1.Visible = true;

                    LabelProjectReference.Visible = true;
                    LabelProjectRef.Visible = true;
                    LabelhasProjectreferenceNote.Visible = true;
                    DropDownListhasProjectreference.Visible = true;

                    //LabelProjectRef.Visible = false;
                    DropDownListhasProjectreference.SelectedValue = "0";
                    LabelProjectDetails.Visible = false;
                    TextBoxProjectDetails.Visible = false;
                    ImageButtonProject.Visible = false;
                }
                else if (DropDownListPublicationEntry.SelectedValue == "PR")
                {
                    //  int yearadd = Convert.ToInt32( PublicationYearAddwebConfig);
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
                        TextBoxYearJA.Items.Add(new ListItem(yeatAppend.ToString(), yeatAppend.ToString(), true));
                    }
                    TextBoxYearJA.DataBind();
                    TextBoxYearJA.SelectedValue = currenntYear.ToString();
                    TextBoxPageTo.Enabled = false;
                    panelJournalArticle.Visible = true;
                    panelConfPaper.Visible = false;
                    panelTechReport.Visible = false;
                    panelBookPublish.Visible = false;
                    panelOthes.Visible = false;
                    panAddAuthor.Visible = true;
                    lblnote1.Visible = true;

                    LabelProjectReference.Visible = true;
                    LabelProjectRef.Visible = true;
                    LabelhasProjectreferenceNote.Visible = true;
                    DropDownListhasProjectreference.Visible = true;

                    //LabelProjectRef.Visible = false;
                    DropDownListhasProjectreference.SelectedValue = "0";
                    LabelProjectDetails.Visible = false;
                    TextBoxProjectDetails.Visible = false;
                    ImageButtonProject.Visible = false;
                }
                BtnFeedback.Enabled = true;
            }
            else if (confirmValue2 == "No")
            {
                if (DropDownListPublicationEntry.SelectedValue != "TS" && DropDownListPublicationEntry.SelectedValue != "JA")
                {
                    confirmValue2 = "";

                }
                else
                {
                    addclik(sender, e);
                    btnSave.Enabled = false;
                    //popup.Visible = false;
                    BtnFeedback.Enabled = false;
                    LabelProjectReference.Visible = true;
                    LabelProjectRef.Visible = true;
                    LabelhasProjectreferenceNote.Visible = true;
                    DropDownListhasProjectreference.Visible = true;

                    //LabelProjectRef.Visible = false;
                    DropDownListhasProjectreference.SelectedValue = "0";
                    LabelProjectDetails.Visible = false;
                    TextBoxProjectDetails.Visible = false;
                    ImageButtonProject.Visible = false;
                }
            }
            if (confirmValue2 == "")
            {
                if (DropDownListPublicationEntry.SelectedValue == "CP")
                {
                    panelConfPaper.Visible = true;

                    panelJournalArticle.Visible = false;

                    panelTechReport.Visible = false;
                    panelBookPublish.Visible = false;
                    panelOthes.Visible = false;
                    panAddAuthor.Visible = true;
                    lblnote1.Visible = false;

                    LabelProjectReference.Visible = true;
                    LabelProjectRef.Visible = true;
                    LabelhasProjectreferenceNote.Visible = true;
                    DropDownListhasProjectreference.Visible = true;

                    //LabelProjectRef.Visible = false;
                    DropDownListhasProjectreference.SelectedValue = "0";
                    LabelProjectDetails.Visible = false;
                    TextBoxProjectDetails.Visible = false;
                    ImageButtonProject.Visible = false;
                }

                else if (DropDownListPublicationEntry.SelectedValue == "BK")
                {
                    string month = DateTime.Now.Month.ToString();
                    DropDownListBookMonth.SelectedValue = month;

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
                    TextBoxYear.SelectedValue = currenntYear.ToString();

                    panelBookPublish.Visible = true;

                    panelTechReport.Visible = false;

                    panelConfPaper.Visible = false;

                    panelJournalArticle.Visible = false;

                    panelOthes.Visible = false;
                    panAddAuthor.Visible = true;
                    lblnote1.Visible = false;

                    LabelProjectReference.Visible = true;
                    LabelProjectRef.Visible = true;
                    LabelhasProjectreferenceNote.Visible = true;
                    DropDownListhasProjectreference.Visible = true;

                    //LabelProjectRef.Visible = false;
                    DropDownListhasProjectreference.SelectedValue = "0";
                    LabelProjectDetails.Visible = false;
                    TextBoxProjectDetails.Visible = false;
                    ImageButtonProject.Visible = false;
                }
                else if (DropDownListPublicationEntry.SelectedValue == "NM")
                {
                    panelOthes.Visible = true;
                    panelBookPublish.Visible = false;

                    panelTechReport.Visible = false;

                    panelConfPaper.Visible = false;

                    panelJournalArticle.Visible = false;
                    panAddAuthor.Visible = true;
                    lblnote1.Visible = false;

                    LabelProjectReference.Visible = true;
                    LabelProjectRef.Visible = true;
                    LabelhasProjectreferenceNote.Visible = true;
                    DropDownListhasProjectreference.Visible = true;

                    //LabelProjectRef.Visible = false;
                    DropDownListhasProjectreference.SelectedValue = "0";
                    LabelProjectDetails.Visible = false;
                    TextBoxProjectDetails.Visible = false;
                    ImageButtonProject.Visible = false;

                }

                else
                {

                }
                BtnFeedback.Enabled = true;
            }
        }
        else
        {
            addclik(sender, e);
            //popup.Visible = false;
        }
        // BtnFeedback.Enabled = true;
        //lblmsg.Visible = false;
        RequiredFieldValidator1.Enabled = false;
        EnableValidation();

    }

    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        if (!Page.IsValid)
        {
            return;
        }


        string confirmValue2 = Request.Form["confirm_value"];

        if (confirmValue2 == "Yes" || confirmValue2 == null)
        {
            Business b = new Business();
            PublishData j = new PublishData();
            string PublicationEntry = DropDownListPublicationEntry.SelectedValue;
            Session["PublicationEntry"] = PublicationEntry;
            string filelocationpath = "";
            string UploadPdf = "";
            string FileUpload = "";
            ArrayList listIndexAgency = new ArrayList();
            if (TextBoxPubId.Text != "")
            {
                FileUpload = b.GetFileUploadPath(TextBoxPubId.Text, PublicationEntry);
                j.FilePathNew = FileUpload;
                j.FilePath = FileUpload;
                //if (FileUploadPdf.HasFile)
                //{
                //if (FileUploadPdf.HasFile)
                //{
                string PublicationEntry1 = DropDownListPublicationEntry.SelectedValue;
                string UploadPdf1 = "";

                //string uploadedfilename = Path.GetFileName(FileUploadPdf.PostedFile.FileName);
                //double size = FileUploadPdf.PostedFile.ContentLength;
                //if (size <= 10485760) //10mb
                //{
                //    string servername = ConfigurationManager.AppSettings["ServerName"].ToString();
                //    string domainame = ConfigurationManager.AppSettings["DomainName"].ToString();
                //    string username = ConfigurationManager.AppSettings["UserName"].ToString();
                //    string password = ConfigurationManager.AppSettings["Password"].ToString();
                //    string FolderPathServerwrite = ConfigurationManager.AppSettings["FolderPath"].ToString();
                //    //\\172.16.50.233\backup\reema\read\RMS
                //    using (NetworkShareAccesser.Access(servername, domainame, username, password))
                //    {
                //        // var uploadfolder = @"\\172.16.50.233\backup\reema\read\RMS\";

                //        var uploadfolder = FolderPathServerwrite;
                //        string path_BoxId = Path.Combine(uploadfolder, TextBoxPubId.Text); //main path + location
                //        if (!Directory.Exists(path_BoxId))   //if directory doesnt exist
                //        {
                //            Directory.CreateDirectory(path_BoxId);//crete directory of location
                //        }
                //        string uploadedfilename1 = Path.GetFileName(FileUploadPdf.PostedFile.FileName);
                //        string timestamp = DateTime.Now.ToString("dd-MM-yy-hh-mm-ss");
                //        string fileextension = Path.GetExtension(uploadedfilename);
                //        string actualfilenamewithtime = PublicationEntry1 + "_" + timestamp + fileextension;
                //        UploadPdf1 = actualfilenamewithtime;
                //        filelocationpath = Path.Combine(path_BoxId, actualfilenamewithtime);
                //        FileUploadPdf.SaveAs(filelocationpath);  //saving file
                //    }
                //}

                //else
                //{
                //    ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Invalid File!!!File exceeds more than 10MB..Please try to upload the file less than or equal to 10MB!!!!!!')</script>");
                //    log.Error("Invalid File!!!File exceeds more than 10MB!!! : " + TextBoxPubId.Text.Trim() + " , Project Type : " + PublicationEntry);
                //    return;
                //}
                //}
                //}
                //else
                //{
                if (FileUpload == "" && PublicationEntry != "BK")
                {
                    //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please upload the file before submission!')</script>");
                    //return;
                    string CloseWindow = "alert('Please upload the file before submission!')";
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow, true);
                    return;
                }
                //}

            }
            else
            {
                
                //if (FileUploadPdf.HasFile)
                //{
                //    string PublicationEntry1 = DropDownListPublicationEntry.SelectedValue;
                string UploadPdf1 = "";
                FileUpload = b.GetFileUploadPath(TextBoxPubId.Text, PublicationEntry);
                j.FilePathNew = FileUpload;
                j.FilePath = FileUpload;
                string uploadedfilename = Path.GetFileName(FileUploadPdf.PostedFile.FileName);
                double size = FileUploadPdf.PostedFile.ContentLength;
                //PublishData j = new PublishData();
                //if (size <= 10485760) //10mb
                //{
                ////string path_BoxId = Path.Combine(mainpath, TextBoxPubId.Text); //main path + location
                //string servername = ConfigurationManager.AppSettings["ServerName"].ToString();
                //string domainame = ConfigurationManager.AppSettings["DomainName"].ToString();
                //string username = ConfigurationManager.AppSettings["UserName"].ToString();
                //string password = ConfigurationManager.AppSettings["Password"].ToString();
                //string FolderPathServerwrite = ConfigurationManager.AppSettings["FolderPath"].ToString();
                ////\\172.16.50.233\backup\reema\read\RMS
                //using (NetworkShareAccesser.Access(servername, domainame, username, password))
                //{
                //    // var uploadfolder = @"\\172.16.50.233\backup\reema\read\RMS\";

                //    var uploadfolder = FolderPathServerwrite;
                //    string path_BoxId = Path.Combine(uploadfolder, TextBoxPubId.Text); //main path + location
                //    if (!Directory.Exists(path_BoxId))   //if directory doesnt exist
                //    {
                //        Directory.CreateDirectory(path_BoxId);//crete directory of location
                //    }
                //    string uploadedfilename1 = Path.GetFileName(FileUploadPdf.PostedFile.FileName);
                //    string timestamp = DateTime.Now.ToString("dd-MM-yy-hh-mm-ss");
                //    string fileextension = Path.GetExtension(uploadedfilename);
                //    string actualfilenamewithtime = PublicationEntry1 + "_" + timestamp + fileextension;
                //    UploadPdf1 = actualfilenamewithtime;
                //    filelocationpath = Path.Combine(path_BoxId, actualfilenamewithtime);
                //    FileUploadPdf.SaveAs(filelocationpath);  //saving file

                //}
                //}
                if (FileUpload == "" && PublicationEntry != "BK")
                {
                    string CloseWindow = "alert('Please upload the file before submission!')";
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow, true);
                    return;
                    //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please upload the file before submission!')</script>");
                    //return;
                }
                //else
                //{
                //    ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Invalid File!!!File exceeds more than 10MB..Please try to upload the file less than or equal to 10MB!!!!!!')</script>");
                //    log.Error("Invalid File!!!File exceeds more than 10MB!!! : " + TextBoxPubId.Text.Trim() + " , Project Type : " + PublicationEntry);
                //    return;
                //}
                //}
                //else
                //{

                //}

            }

            j.FilePath = filelocationpath;

            string role = Session["Role"].ToString();

            if (role != "1")
            {
                string AutoAppr = null;
                AutoAppr = b.GetIstDeptAutoApprove(Session["InstituteId"].ToString(), Session["Department"].ToString());
                if (AutoAppr == null)
                {
                    j.AutoAppoval = "";
                }
                else
                {
                    j.AutoAppoval = AutoAppr;
                }
            }
            else
            {
                j.AutoAppoval = "Y";
            }

            if (FileUpload == "")
            {
                j.FilePath = filelocationpath;
            }
            else
            {
                j.FilePath = j.FilePathNew;
            }

            if (DropDownListPublicationEntry.SelectedValue == "JA" || DropDownListPublicationEntry.SelectedValue == "TS" || DropDownListPublicationEntry.SelectedValue == "PR")
            {
                string month = DropDownListMonthJA.SelectedValue;
                int month2 = Convert.ToInt32(month);
                if (month2 < 10 && TextBoxYearJA.Text == "2015")
                {
                    // Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "script", "alert('Please enter the valid month and year!');", true);
                    string CloseWindow1 = "alert('Please enter the valid month and year!')";
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow1, true);

                    //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please enter the valid month and year!')</script>");
                    return;
                }

            }

            if (DropDownListPublicationEntry.SelectedValue == "JA" || DropDownListPublicationEntry.SelectedValue == "TS" || DropDownListPublicationEntry.SelectedValue == "PR")
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
                    //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please enter the valid month and year!')</script>");
                    string CloseWindow1 = "alert('Please enter the valid month and year')";
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow1, true);
                    return;
                }

            }
            if (DropDownListPublicationEntry.SelectedValue == "BK")
            {
                string monthnow = DateTime.Now.Month.ToString();
                string yearnow = DateTime.Now.Year.ToString();

                int monthnow2 = Convert.ToInt32(monthnow);
                int yearnow2 = Convert.ToInt32(yearnow);


                string month = DropDownListBookMonth.SelectedValue;
                int month2 = Convert.ToInt32(month);

                string year = TextBoxYear.Text;
                int year2 = Convert.ToInt32(year);


                if (monthnow2 < month2 && yearnow2 <= year2)
                {
                    //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please enter the valid month and year!')</script>");
                    string CloseWindow1 = "alert('Please enter the valid month and year!')";
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow1, true);
                    return;
                }

            }
            if (DropDownListPublicationEntry.SelectedValue == "JA" || DropDownListPublicationEntry.SelectedValue == "TS" || DropDownListPublicationEntry.SelectedValue == "PR")
            {
                if (TextBoxPubJournal.Text == "")
                {
                    //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please enter the ISSN!')</script>");
                    string CloseWindow1 = "alert('Please enter the ISSN')";
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow1, true);
                    return;
                }
            }


            string MUCategorization = DropDownListMuCategory.SelectedValue;

            string TitleWorkItem = txtboxTitleOfWrkItem.Text.Trim();
            string PubJournal = TextBoxPubJournal.Text.Trim();
            string PubJournalName = TextBoxNameJournal.Text.Trim();
            string NameJournal = TextBoxNameJournal.Text.Trim();

            string Volume = TextBoxVolume.Text.Trim();
            string JAMonthJA = DropDownListMonthJA.SelectedValue;

            string YearJA = TextBoxYearJA.SelectedValue;
            string PageFrom = TextBoxPageFrom.Text.Trim();

            string PageTo = TextBoxPageTo.Text.Trim();

            string Issue = TextBoxIssue.Text.Trim();

            string Indexed = RadioButtonListIndexed.SelectedValue;

            string IndexAgency = CheckboxIndexAgency.SelectedValue;

            string PubType = DropDownListPubType.SelectedValue;

            string ImpFact = TextBoxImpFact.Text.Trim();
            string fiveImpFact = TextBoxImpFact5.Text.Trim();

            string EventTitle = TextBoxEventTitle.Text.Trim();

            string Place = TextBoxPlace.Text.Trim();

            string Date = TextBoxDate.Text.Trim();
            string Date1 = TextBoxDate1.Text.Trim();

            string TitleBook = TextBoxTitleBook.Text.Trim();



            string ChapterContributed = TextBoxChapterContributed.Text.Trim();

            string Edition = TextBoxEdition.Text.Trim();

            string Publisher = TextBoxPublisher.Text.Trim();

            string Year = TextBoxYearJA.SelectedValue;


            string PageNum = TextBoxPageNum.Text.Trim();

            string Volume1 = TextBoxVolume1.Text.Trim();

            //string ofURL = TextBoxURL.Text.Trim();
            string Publish = TextBoxPublisher.Text.Trim();


            string DateOfPublish = TextBoxDateOfNewsPublish.Text.Trim();

            string BookYear = TextBoxYear.SelectedValue;
            string BookMonth = DropDownListBookMonth.SelectedValue;

            string PageNumNewsPaper = TextBoxPageNumNewsPaper.Text.Trim();


            string DOINum = TextBoxDOINum.Text.Trim();

            string Keywords = TextBoxKeywords.Text.Trim();

            string Abstract = TextBoxAbstract.Text.Trim();

            if (j.AutoAppoval == "Y")
            {
                j.Status = "APP";
            }
            else
            {
                j.Status = "SUB";
            }

            string isERF = DropDownListErf.SelectedValue;

            string BISBN = txtbISBN.Text;
            string BSection = txtbSection.Text;
            string BChapter = txtChapter.Text;
            string BCountry = txtCountry.Text;
            string BTypeofPublication = DropDownListBookPublicationType.SelectedValue;
            if (DropDownListhasProjectreference.SelectedValue == "0")
            {            
                string CloseWindow1 = "alert('Please select Has Project Reference!')";
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow1, true);
                return;            
            }
      
                if (DropDownListhasProjectreference.SelectedValue == "Y")
                {
                    if (TextBoxProjectDetails.Text == "")
                    {
                        string CloseWindow1 = "alert('Please select the Project Details!')";
                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow1, true);
                        return;
                    }
                }
            
            string ProjectIDlist = TextBoxProjectDetails.Text.Trim();
            string hasProjectreference = DropDownListhasProjectreference.SelectedValue.Trim();
            j.ProjectIDlist = ProjectIDlist;
            j.hasProjectreference = hasProjectreference;
            j.PaublicationID = TextBoxPubId.Text.Trim();
            j.TypeOfEntry = PublicationEntry;
            j.MUCategorization = MUCategorization;

            j.TitleWorkItem = TitleWorkItem;
            string inst = Session["InstituteId"].ToString();
            string dept = Session["Department"].ToString();

            //   j.CorrespondingAuthor = CorrespondingAuthor;
            if (DropDownListPublicationEntry.SelectedValue == "BK")
            {
                string BookDate = "" + BookYear + "-" + BookMonth + "-01";
                DateTime BkDate = Convert.ToDateTime(BookDate);
                j.PublishDate = BkDate;
            }
            if (DropDownListPublicationEntry.SelectedValue == "JA" || DropDownListPublicationEntry.SelectedValue == "TS" || DropDownListPublicationEntry.SelectedValue == "PR")
            {
                string PublishDate = "" + YearJA + "-" + JAMonthJA + "-01";
                DateTime pubDate = Convert.ToDateTime(PublishDate);
                j.PublishDate = pubDate;
            }
            j.PublisherOfJournal = PubJournal;
            j.PublisherOfJournalName = PubJournalName;
            if (DropDownListPublicationEntry.SelectedValue == "CP")
            {
                j.ConfISBN = TextBoxIsbn.Text.Trim();
            }
            else if (DropDownListPublicationEntry.SelectedValue == "BK")
            {
                j.ConfISBN = BISBN;
            }
            j.NameOfJournal = NameJournal;
            j.Volume = Volume;
            j.MonthJA = JAMonthJA;
            j.JAVolume = Volume;
            if (JAMonthJA != "")
            {
                j.PublishJAMonth = Convert.ToInt16(JAMonthJA);
            }

            j.PublishJAYear = Year;
            j.PageFrom = PageFrom;
            j.PageTo = PageTo;

            if (DropDownListPublicationEntry.SelectedValue == "JA" || DropDownListPublicationEntry.SelectedValue == "TS" || DropDownListPublicationEntry.SelectedValue == "PR")
            {
                j.Indexed = Indexed;
            }
            j.IndexedIn = IndexAgency;
            j.Publicationtype = PubType;

            j.ImpactFactor = ImpFact;
            j.ImpactFactor5 = fiveImpFact;
            if (txtIFApplicableYear.Text.Trim() != "")
            {
                j.IFApplicableYear = Convert.ToInt16(txtIFApplicableYear.Text.Trim());
            }
            j.Issue = Issue;

            j.ConferenceTitle = EventTitle;
            j.Place = Place;
            if (TextBoxDate.Text != "")
            {
                j.Date = Convert.ToDateTime(Date);
            }
            if (TextBoxDate1.Text != "")
            {
                j.todate = Convert.ToDateTime(Date1);
            }
            j.TitleOfBook = TitleBook;
            j.TitileOfBookChapter = TextBoxChapterContributed.Text.Trim();
            j.Edition = Edition;
            j.Publisher = Publish;
            j.BookPublishYear = BookYear;
            if (BookMonth != "")
            {
                j.BookPublishMonth = Convert.ToInt16(BookMonth);
            }
            j.BookPageNum = PageNum;

            j.BookVolume = Volume1;


            j.BookSection = BSection;
            j.BookChapter = BChapter;
            j.BookCountry = BCountry;
            j.BookTypeofPublication = BTypeofPublication;
            string frommonth = ConfigurationManager.AppSettings["QuartileMonthFrom"].ToString();
            j.Quartilefrommonth = Convert.ToInt32(frommonth);
            string Tomonth = ConfigurationManager.AppSettings["QuartileMonthTo"].ToString();
            j.QuartileTomonth = Convert.ToInt32(Tomonth); 
            //j.url = ofURL;
            j.DOINum = DOINum;
            j.Keywords = Keywords;
            j.Abstract = Abstract;
            j.UploadPDF = j.FilePath;
            j.ApprovedBy = Session["UserId"].ToString();
            j.ApprovedDate = DateTime.Now;
            if (TextBoxPubId.Text == "")
            {
                j.CreatedBy = Session["UserId"].ToString();
                j.CreatedDate = DateTime.Now;
            }
            j.isERF = DropDownListErf.SelectedValue;
            j.TitileOfChapter = "";
            j.InstUser = Session["InstituteId"].ToString();
            j.DeptUser = Session["Department"].ToString();
            //j.CitationUrl = txtCitation.Text;

            if (role != "1")
            {
                string AutoAppr = null;
                AutoAppr = b.GetIstDeptAutoApprove(Session["InstituteId"].ToString(), Session["Department"].ToString());
                if (AutoAppr == null)
                {
                    j.AutoAppoval = "";
                }
                else
                {
                    j.AutoAppoval = AutoAppr;
                }
            }
            else
            {
                j.AutoAppoval = "Y";
            }


            j.uploadEPrint = DropDownListuploadEPrint.SelectedValue;
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

            if (DropDownListPublicationEntry.SelectedValue == "NM")
            {
                j.NewsPublisher = TextBoxNewsPublish.Text.Trim();
                if (TextBoxDateOfNewsPublish.Text != "")
                {
                    string date2 = TextBoxDateOfNewsPublish.Text.Trim();
                    j.NewsPublishedDate = Convert.ToDateTime(date2);
                }
                j.NewsPageNum = TextBoxPageNumNewsPaper.Text.Trim();

            }
            string LibId = null;
            LibId = b.GetLibraryId(inst, dept);
            j.LibraryId = LibId;


            if (TextBoxCreditPoint.Text == "")
            {
                TextBoxCreditPoint.Text = "0";
            }
            j.CreditPoint = Convert.ToInt32(TextBoxCreditPoint.Text.Trim());
            j.TypePresentation = RadioButtonListTypePresentaion.SelectedValue;
            j.AwardedBy = TextBoxAwardedBy.Text.Trim();


            if (DropDownListPublicationEntry.SelectedValue == "JA" || DropDownListPublicationEntry.SelectedValue == "TS" || DropDownListPublicationEntry.SelectedValue == "PR")
            {
                for (int i = 0; i < CheckboxIndexAgency.Items.Count; i++)
                {
                    if (CheckboxIndexAgency.Items[i].Selected)
                    {
                        listIndexAgency.Add(CheckboxIndexAgency.Items[i].Value);
                    }
                }
            }
            if (DropDownListPublicationEntry.SelectedValue == "CP")
            {
                string IndexedCP = RadioButtonListCPIndexed.SelectedValue;
                string IndexAgencyCP = CheckBoxListCPIndexedIn.SelectedValue;
                string PageFromCP = TextBoxPageFromCP.Text.Trim();
                string PageToCP = TextBoxPageToCP.Text.Trim();
                string CPMonth = DropDownListMonthCP.SelectedValue;
                string CPYear = TextBoxYearCP.Text.Trim();
                string CPCity = txtcity.Text.Trim();
                string CPState = txtState.Text.Trim();
                string Conferencetype = DropDownListConferencetype.SelectedValue;
                string FundsUtilized = TextBoxFunds.Text.Trim();
                if (FundsUtilized != "" && FundsUtilized != null)
                {
                    j.FundsUtilized = Convert.ToDouble(FundsUtilized);
                }
                j.Indexed = IndexedCP;
                j.IndexedIn = IndexAgencyCP;
                j.PageFrom = PageFromCP;
                j.PageTo = PageToCP;
                j.PublishJAMonth = Convert.ToInt32(CPMonth);
                j.PublishJAYear = CPYear;
                j.CPCity = CPCity;
                j.CPState = CPState;
                j.Publicationtype = Conferencetype;
                for (int i = 0; i < CheckBoxListCPIndexedIn.Items.Count; i++)
                {
                    if (CheckBoxListCPIndexedIn.Items[i].Selected)
                    {
                        listIndexAgency.Add(CheckBoxListCPIndexedIn.Items[i].Value);
                    }
                }
            }

            string AppendInstitutionNamess = null;
            int countCorrAuth = 0;
            int countAuthType = 0;

            ArrayList rollnocount = null;
            int countConfAttended = 0;
            int countConfPresent = 0;
            ArrayList rollnolist = new ArrayList();
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
            Business B = new Business();
            PublishData[] JD = new PublishData[dtCurrentTable.Rows.Count];
            int rowIndex1 = 0;
            if (dtCurrentTable.Rows.Count > 0)
            {

                for (int i = 0; i < dtCurrentTable.Rows.Count; i++)
                {
                    JD[i] = new PublishData();
                    TextBox EmployeeCode = (TextBox)Grid_AuthorEntry.Rows[rowIndex1].Cells[1].FindControl("EmployeeCode");
                    TextBox AuthorName = (TextBox)Grid_AuthorEntry.Rows[rowIndex1].Cells[1].FindControl("AuthorName");
                    DropDownList DropdownMuNonMu = (DropDownList)Grid_AuthorEntry.Rows[rowIndex1].Cells[2].FindControl("DropdownMuNonMu");

                    HiddenField Institution = (HiddenField)Grid_AuthorEntry.Rows[rowIndex1].Cells[0].FindControl("Institution");
                    TextBox InstitutionName = (TextBox)Grid_AuthorEntry.Rows[rowIndex1].Cells[6].FindControl("InstitutionName");
                    HiddenField Department = (HiddenField)Grid_AuthorEntry.Rows[rowIndex1].Cells[0].FindControl("Department");
                    TextBox DepartmentName = (TextBox)Grid_AuthorEntry.Rows[rowIndex1].Cells[0].FindControl("DepartmentName");

                    DropDownList DropdownStudentInstitutionName = (DropDownList)Grid_AuthorEntry.Rows[rowIndex1].Cells[0].FindControl("DropdownStudentInstitutionName");
                    DropDownList DropdownStudentDepartmentName = (DropDownList)Grid_AuthorEntry.Rows[rowIndex1].Cells[0].FindControl("DropdownStudentDepartmentName");


                    TextBox MailId = (TextBox)Grid_AuthorEntry.Rows[rowIndex1].Cells[0].FindControl("MailId");
                    DropDownList isCorrAuth = (DropDownList)Grid_AuthorEntry.Rows[rowIndex1].Cells[0].FindControl("isCorrAuth");
                    DropDownList AuthorType = (DropDownList)Grid_AuthorEntry.Rows[rowIndex1].Cells[0].FindControl("AuthorType");
                    TextBox NameInJournal = (TextBox)Grid_AuthorEntry.Rows[rowIndex1].Cells[0].FindControl("NameInJournal");

                    DropDownList IsPresenter = (DropDownList)Grid_AuthorEntry.Rows[rowIndex1].Cells[0].FindControl("IsPresenter");
                    CheckBox HasAttented = (CheckBox)Grid_AuthorEntry.Rows[rowIndex1].Cells[0].FindControl("HasAttented");


                    DropDownList NationalType = (DropDownList)Grid_AuthorEntry.Rows[rowIndex1].Cells[0].FindControl("NationalType");
                    DropDownList ContinentId = (DropDownList)Grid_AuthorEntry.Rows[rowIndex1].Cells[0].FindControl("ContinentId");

                    HiddenField Institution1 = (HiddenField)Grid_AuthorEntry.Rows[0].Cells[0].FindControl("Institution");
                    string a = Institution1.Value;

                    HiddenField Institution2 = (HiddenField)Grid_AuthorEntry.Rows[0].Cells[0].FindControl("Institution");
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
                            string CloseWindow1 = "alert('Please enter Institution Name!')";
                            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow1, true);
                            //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please enter Institution Name!')</script>");
                            return;

                        }

                        if (DepartmentName.Text == "")
                        {
                            string CloseWindow = "alert('Please enter Department Name!')";
                            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow, true);
                            return;
                            //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please enter Department Name!')</script>");
                            //return;

                        }
                    }

                    if (DropdownMuNonMu.SelectedValue == "N" || DropdownMuNonMu.SelectedValue == "E")
                    {
                        if (InstitutionName.Text == "")
                        {
                            string CloseWindow = "alert('Please enter Institution Name!')";
                            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow, true);
                            return;
                            //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please enter Institution Name!')</script>");
                            //return;

                        }

                        if (DepartmentName.Text == "")
                        {
                            string CloseWindow = "alert('Please enter Department Name!')";
                            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow, true);
                            return;
                            //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please enter Department Name!')</script>");
                            //return;

                        }
                    }
                    if (DropdownMuNonMu.SelectedValue == "M" || DropdownMuNonMu.SelectedValue == "S")
                    {
                        if (MailId.Text == "")
                        {
                            string CloseWindow = "alert('Please enter MailId!')";
                            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow, true);
                            return;
                            //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please enter MailId!')</script>");
                            //return;

                        }
                    }


                    JD[i].AuthorName = AuthorName.Text.Trim();
                    JD[i].MUNonMU = DropdownMuNonMu.Text.Trim();
                    if (DropDownListPublicationEntry.SelectedValue == "JA" || DropDownListPublicationEntry.SelectedValue == "TS" || DropDownListPublicationEntry.SelectedValue == "CP" || DropDownListPublicationEntry.SelectedValue == "PR")
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
                            if (EmployeeCode.Text != "")
                            {
                                JD[i].EmployeeCode = EmployeeCode.Text;
                            }
                            else
                            {
                                JD[i].EmployeeCode = AuthorName.Text.Trim();
                            }
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
                        if (DropDownListPublicationEntry.SelectedValue == "JA" || DropDownListPublicationEntry.SelectedValue == "TS" || DropDownListPublicationEntry.SelectedValue == "CP" || DropDownListPublicationEntry.SelectedValue == "PR")
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
                            //JD[i].EmployeeCode = JD[i].EmployeeCode;

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


                    JD[i].AppendInstitutionNames = JD[i].InstitutionName;


                    JD[i].EmailId = MailId.Text.Trim();

                    JD[i].isCorrAuth = isCorrAuth.Text.Trim();
                    if (DropDownListPublicationEntry.SelectedValue == "JA" || DropDownListPublicationEntry.SelectedValue == "TS" || DropDownListPublicationEntry.SelectedValue == "BK" || DropDownListPublicationEntry.SelectedValue == "PR")
                    {
                        JD[i].AuthorType = AuthorType.Text.Trim();
                    }
                    else
                    {
                        JD[i].AuthorType = "A";
                    }
                    JD[i].NameInJournal = NameInJournal.Text.Trim();

                    if (DropDownListPublicationEntry.SelectedValue == "CP")
                    {
                        JD[i].IsPresenter = IsPresenter.Text.Trim();
                        if (HasAttented.Checked == true)
                        {
                            JD[i].HasAttented = "Y";
                            JD[i].AuthorCreditPoint = Convert.ToInt32(TextBoxCreditPoint.Text.Trim());
                        }
                        else
                        {
                            JD[i].HasAttented = "N";
                            JD[i].AuthorCreditPoint = 0;
                        }
                    }
                    else
                    {
                        JD[i].IsPresenter = "";

                        JD[i].HasAttented = "";

                        JD[i].AuthorCreditPoint = 0;

                    }

                    if (DropDownListPublicationEntry.SelectedValue == "JA" || DropDownListPublicationEntry.SelectedValue == "TS" || DropDownListPublicationEntry.SelectedValue == "PR")
                    {
                        if (JD[i].AuthorType == "0")
                        {
                            string CloseWindow = "alert('Please Select the Author Type!')";
                            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow, true);
                            return;
                            //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please Select the Author Type!')</script>");
                            //return;
                        }
                        if (JD[i].AuthorType == "P")
                        {
                            countAuthType = countAuthType + 1;
                        }

                        if (JD[i].isCorrAuth == "Y")
                        {
                            countCorrAuth = countCorrAuth + 1;
                        }

                        if (rollnolist.Count != 0)
                        {
                            if (rollnolist.Contains(JD[i].EmployeeCode))
                            {
                                string CloseWindow = "alert('Please remove duplicate RollNo from the list!')";
                                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow, true);
                                return;
                                // rollnocount.Add(JD[i].EmployeeCode);
                                //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please remove duplicate RollNo from the list')</script>");
                                //return;

                            }
                            else
                            {
                                string rollno = JD[i].EmployeeCode.ToString();
                                rollnolist.Add(rollno);
                            }
                        }
                        else
                        {
                            string rollno = JD[i].EmployeeCode.ToString();
                            rollnolist.Add(rollno);
                        }
                    }

                    if (DropDownListPublicationEntry.SelectedValue == "CP")
                    {
                        if (JD[i].HasAttented == "Y")
                        {
                            countConfAttended = countConfAttended + 1;
                        }

                        if (JD[i].IsPresenter == "Y")
                        {
                            countConfPresent = countConfPresent + 1;
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
                        //id = Session["InstituteId"].ToString();
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

            for (int i = 0; i < dtCurrentTable.Rows.Count; i++)
            {

            }

            if (DropDownListPublicationEntry.SelectedValue == "JA" || DropDownListPublicationEntry.SelectedValue == "TS" || DropDownListPublicationEntry.SelectedValue == "PR")
            {
                if (countAuthType > 1)
                {
                    string CloseWindow = "alert('Please remove duplicate RollNo from the list!')";
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow, true);
                    return;
                    //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('First Author cannot be more than one!')</script>");
                    //return;

                }
                if (countAuthType == 0)
                {
                    string CloseWindow = "alert('Select atleast one Author Type as First Author!')";
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow, true);
                    return;
                    //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Select atleast one Author Type as First Author !')</script>");
                    //return;

                }

                if (countCorrAuth > 1)
                {
                    string CloseWindow = "alert('Corresponding Author cannot be more than one!')";
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow, true);
                    return;
                    //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Corresponding Author cannot be more than one!')</script>");
                    //return;

                }

                if (countCorrAuth == 0)
                {
                    string CloseWindow = "alert('Select atleast one Corresponding Author!')";
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow, true);
                    return;
                    //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Select atleast one Corresponding Author!')</script>");
                    //return;

                }
            }
            if (DropDownListPublicationEntry.SelectedValue == "CP")
            {
                if (countConfAttended == 0)
                {
                    string CloseWindow = "alert('There should be atleast one presenter must be attended the conference !')";
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow, true);
                    return;
                    //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('There should be atleast one presenter must be attended the conference !')</script>");
                    //return;

                }

                if (countConfPresent == 0)
                {
                    string CloseWindow = "alert('There should be atleast one presenter!')";
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow, true);
                    return;
                    //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('There should be atleast one presenter !')</script>");
                    //return;

                }
            }


            if (DropDownListPublicationEntry.SelectedValue == "JA" || DropDownListPublicationEntry.SelectedValue == "TS" || DropDownListPublicationEntry.SelectedValue == "PR")
            {
                ArrayList list = new ArrayList();

                string authorname = null;
                for (int i = 0; i < JD.Length; i++)
                {

                    list.Add(JD[i].EmployeeCode);

                }

                authorname = Session["UserId"].ToString();
                if (!list.Contains(authorname))
                {
                    string CloseWindow = "alert('Authors list should contain logged in faculty!')";
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow, true);
                    return;
                    //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Authors list should contain logged in faculty')</script>");
                    //return;

                }


            }
            ArrayList list1 = new ArrayList();
            for (int i = 0; i < JD.Length; i++)
            {
                if (list1.Contains(JD[i].EmployeeCode))
                {
                    string CloseWindow = "alert('Please remove duplicate authors from the list!')";
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow, true);
                    return;
                    //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please remove duplicate authors from the list')</script>");
                    //return;
                }
                else
                {
                    list1.Add(JD[i].EmployeeCode);
                }

            }


            //publication data

            int rowIndex2 = 0;
            if (j.AutoAppoval == "Y")
            {
                if (DropDownListPublicationEntry.SelectedValue == "JA" || DropDownListPublicationEntry.SelectedValue == "TS" || DropDownListPublicationEntry.SelectedValue == "PR")
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

                if (DropDownListPublicationEntry.SelectedValue == "JA")
                {
                    if (j.Indexed == "Y")
                    {
                        int publicationmonth2 = Convert.ToInt32(DropDownListMonthJA.SelectedValue);
                        int publicationyear2 = Convert.ToInt32(TextBoxYearJA.SelectedValue);
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
                }
            }
            else
            {
                j.IsStudentAuthor = "N";
            }


            if (DropDownListPublicationEntry.SelectedValue == "JA" && (DropDownListMuCategory.SelectedValue == "LE" || DropDownListMuCategory.SelectedValue == "BK"))
            {
                if (j.Indexed == "Y")
                {
                    int publicationmonth = Convert.ToInt32(DropDownListMonthJA.SelectedValue);
                    int publicationyear = Convert.ToInt32(TextBoxYearJA.SelectedValue);
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
                    string resultPrint = B.CheckPrintEvaluationEnableQuartile(DropDownListMuCategory.SelectedValue, DropDownListMonthJA.SelectedValue, TextBoxYearJA.Text, txtquartileid.Text);
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
                                Session["id"] = list[i].ToString();
                                result = 1;
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

            if (result == 1)
            {
                log.Info("Entered article already exist for ISSN: " + j.PublisherOfJournal + " , JA Volume: " + j.JAVolume + " , Issue: " + j.Issue + " , Page From :" + j.PageFrom + ",Page To:" + j.PageTo + ",ID:" + Session["id"]);
                string CloseWindow = "alert('Entered article already exist for ISSN: " + j.PublisherOfJournal + " , JA Volume: " + j.JAVolume + " , Issue: " + j.Issue + " , Page From :" + j.PageFrom + ",Page To:" + j.PageTo + "')";
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow, true);
                return;
                
                //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Entered article already exist for ISSN: " + j.PublisherOfJournal + " , JA Volume: " + j.JAVolume + " , Issue: " + j.Issue + " , Page From :" + j.PageFrom + ",Page To:" + j.PageTo + "')</script>");
                //return;
            }
            else
            {
                if (TextBoxPubId.Text == "")
                {
                    int result2 = 0;
                    result2 = B.insertPublishEntry(j, JD, listIndexAgency);
                    if (result2 == 1)
                    {
                        TextBoxPubId.Text = Session["Pubseed"].ToString();

                        if (FileUploadPdf.HasFile)
                        {
                            string PublicationEntry1 = DropDownListPublicationEntry.SelectedValue;
                            string UploadPdf1 = "";
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

                                //string path_BoxId = Path.Combine(mainpath, TextBoxPubId.Text); //main path + location
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
                                    //j.PaublicationID = TextBoxPubId.Text.Trim();
                                    if (FileUpload == "")
                                    {
                                        j.FilePath = filelocationpath;
                                    }
                                    else
                                    {
                                        j.FilePath = j.FilePathNew;
                                    }
                                    j.RemarksFeedback = "";
                                    int result1 = B.UpdatePfPathCreate(j);

                                    btnSave.Enabled = false;
                                    Business b1 = new Business();
                                    FileUpload = b1.GetFileUploadPath(j.PaublicationID, j.TypeOfEntry);
                                    if (FileUpload != "")
                                    {
                                        GVViewFile.Visible = true;
                                    }
                                    else
                                    {
                                        GVViewFile.Visible = false;
                                    }
                                    DSforgridview.SelectParameters.Clear();
                                    DSforgridview.SelectParameters.Add("PublicationID", j.PaublicationID);
                                    DSforgridview.SelectParameters.Add("TypeOfEntry", j.TypeOfEntry);

                                    DSforgridview.SelectCommand = "select UploadPDFPath from Publication where PublicationID=@PublicationID and TypeOfEntry=@TypeOfEntry";
                                    DSforgridview.DataBind();
                                    GVViewFile.DataBind();
                                }
                            }
                        }
                        string isrevert = B.SelectIsReverFlag(j.PaublicationID, j.TypeOfEntry);
                        EmailDetails details = new EmailDetails();
                        details = SendMail(j.PaublicationID, j.TypeOfEntry, j.AutoAppoval, isrevert, j.IsStudentAuthor);
                        details.Id = TextBoxPubId.Text;
                        details.Type = DropDownListPublicationEntry.SelectedValue;
                        SendMailObject obj = new SendMailObject();
                        bool resultv = obj.InsertIntoEmailQueue(details);





                        if (j.AutoAppoval == "Y")
                        {
                            string CloseWindow = "alert('Publication data Approved Successfully..of Entry Type: " + DropDownListPublicationEntry.SelectedValue + " of ID: " + TextBoxPubId.Text + " For update Click on search and edit  !')";
                            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow, true);
                            log.Info("Publication Approved Successfully,  of Entry Type: " + DropDownListPublicationEntry.SelectedValue + " of ID: " + TextBoxPubId.Text);
                        }
                        else
                        {
                            if (isrevert == "Y")
                            {
                                string CloseWindow = "alert('Publication data Re-Submitted Successfully..of Entry Type: " + DropDownListPublicationEntry.SelectedValue + " of ID: " + TextBoxPubId.Text + " For update Click on search and edit  !')";
                                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow, true);
                                log.Info("Publication Re-Submitted Successfully,  of Entry Type: " + DropDownListPublicationEntry.SelectedValue + " of ID: " + TextBoxPubId.Text);
                            }
                            else
                            {
                                string CloseWindow = "alert('Publication data Submitted Successfully..of Entry Type: " + DropDownListPublicationEntry.SelectedValue + " of ID: " + TextBoxPubId.Text + " For update Click on search and edit  !')";
                                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow, true);
                                log.Info("Publication Submitted Successfully,  of Entry Type: " + DropDownListPublicationEntry.SelectedValue + " of ID: " + TextBoxPubId.Text);
                            }
                        }
                        addclik(sender, e);
                        //popup.Visible = false;
                        popupPanelJournal.Visible = false;
                        popupPanelProceedingsJournal.Visible = false;
                      
                        dataBind();
                        panelFileUpload.Visible = false;
                        popupPanelProject.Visible = false;
                        Session["TempPid"] = TextBoxPubId.Text.ToString();
                        Session["TempTypeEntry"] = DropDownListPublicationEntry.SelectedValue;
                       
                    }
                    else
                    {
                        log.Error("Publication creation Error!!!,  of Entry Type: " + DropDownListPublicationEntry.SelectedValue + " of ID: " + TextBoxPubId.Text);
                        string CloseWindow = "alert('Publication creation Error')";
                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow, true);
                    }
                }
                else
                {
                    result = B.UpdatePfPath(j, JD, listIndexAgency);
                    if (result >= 1)
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


                        string isrevert = b1.SelectIsReverFlag(j.PaublicationID, j.TypeOfEntry);
                        EmailDetails details = new EmailDetails();
                        details = SendMail(j.PaublicationID, j.TypeOfEntry, j.AutoAppoval, isrevert, j.IsStudentAuthor);
                        details.Id = TextBoxPubId.Text;
                        details.Type = DropDownListPublicationEntry.SelectedValue;
                        SendMailObject obj = new SendMailObject();
                        bool result1 = obj.InsertIntoEmailQueue(details);

                        JournalData obj3 = new JournalData();
                        Business BU = new Business();
                        obj3 = BU.CheckUniqueIdPublication(TextBoxPubId.Text, DropDownListPublicationEntry.SelectedValue, details);
                        if (obj3.Module == details.EmailSubject + details.Module)
                        {
                            int data = BU.updatePublicationEmailtracker(TextBoxPubId.Text, DropDownListPublicationEntry.SelectedValue, details, obj3);
                        }

                        dataBind();
                        panelFileUpload.Visible = false;
                        //popup.Visible = false;
                        popupPanelJournal.Visible = false;
                        popupPanelProceedingsJournal.Visible = false;
                        popupPanelProject.Visible = false;
                        BtnFeedback.Enabled = false;

                        Session["EntryType"] = DropDownListPublicationEntry.SelectedValue;
                        Session["ID"] = TextBoxPubId.Text;


                        if (j.AutoAppoval == "Y")
                        {
                            string CloseWindow = "alert('Publication data Approved Successfully..of Entry Type: " + DropDownListPublicationEntry.SelectedValue + " of ID: " + TextBoxPubId.Text + " For update Click on search and edit  !')";
                            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow, true);
                            log.Info("Publication Approved Successfully,  of Entry Type: " + DropDownListPublicationEntry.SelectedValue + " of ID: " + TextBoxPubId.Text);
                        }
                        else
                        {
                            if (isrevert == "N")
                            {
                                //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Publication Submitted Successfully')</script>");
                                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Publication Submitted Successfully.. of Entry Type: " + DropDownListPublicationEntry.SelectedValue + " of ID: " + TextBoxPubId.Text + "')</script>");
                                string CloseWindow = "alert('Publication Submitted Successfully.. of Entry Type: " + DropDownListPublicationEntry.SelectedValue + " of ID: " + TextBoxPubId.Text + "')";
                                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow, true);                                                  
                                log.Info("Publication Submitted Successfully,  of Entry Type: " + DropDownListPublicationEntry.SelectedValue + " of ID: " + TextBoxPubId.Text);

                            }
                            else
                            {
                                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Publication Re-Submitted Successfully.. of Entry Type: " + DropDownListPublicationEntry.SelectedValue + " of ID: " + TextBoxPubId.Text + "')</script>");
                                log.Info("Publication Re-Submitted Successfully,  of Entry Type: " + DropDownListPublicationEntry.SelectedValue + " of ID: " + TextBoxPubId.Text);

                            }
                        }
                    BtnFeedback.Visible = true;

                        addclik(sender, e);
                        //addclik1(sender, e);

                    }
                    else
                    {

                        log.Info("Problem in submitting publication,  of Entry Type: " + DropDownListPublicationEntry.SelectedValue + " of ID: " + TextBoxPubId.Text);
                        ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Problem in submitting publication')</script>");
                    }
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

    protected void IsPresenterIsPresenter(object sender, EventArgs e)
    {
        PubEntriesUpdatePanel.Update();
        EditUpdatePanel.Update();
        //popupPanelAffilUpdate.Update();
        MainUpdate.Update();
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

    protected void DropDownListMuCategoryOnSelectedIndexChanged(object sender, EventArgs e)
    {
        PubEntriesUpdatePanel.Update();
        EditUpdatePanel.Update();
        //popupPanelAffilUpdate.Update();
        MainUpdate.Update();
        if (DropDownListPublicationEntry.SelectedValue == "JA" || DropDownListPublicationEntry.SelectedValue == "TS" || DropDownListPublicationEntry.SelectedValue == "PR")
        {

            if (DropDownListMuCategory.SelectedValue == " ")
            {
                Labeljastar1.Visible = false;
                Labeljastr2.Visible = false;
                Labeljastr3.Visible = false;
                //Labeljastr4.Visible = false;
                //Labeljastr5.Visible = false;
                //Labeljastr6.Visible = false;
                //Labeljastr7.Visible = false;
                Labelvolstar8.Visible = false;
            }
            else if (DropDownListMuCategory.SelectedValue != "LE")
            {
                Labeljastar1.Visible = true;
                Labeljastr2.Visible = true;
                Labeljastr3.Visible = true;
                //Labeljastr4.Visible = true;
                //Labeljastr5.Visible = true;
                // Labeljastr6.Visible = true;
                //Labeljastr7.Visible = true;
                Labelvolstar8.Visible = true;
            }
            else
            {
                Labeljastar1.Visible = false;
                Labeljastr2.Visible = false;
                Labeljastr3.Visible = false;
                // Labeljastr4.Visible = false;
                //Labeljastr5.Visible = false;
                //Labeljastr6.Visible = false;
                //Labeljastr7.Visible = false;
                Labelvolstar8.Visible = false;
            }
        }
        else
        {
            Labeljastar1.Visible = false;
            Labeljastr2.Visible = false;
            Labeljastr3.Visible = false;
            //Labeljastr4.Visible = false;
            //Labeljastr5.Visible = false;
            // Labeljastr6.Visible = false;
            Labelvolstar8.Visible = false;
            //Labeljastr7.Visible = false;
        }
        EnableValidation();
    }
    protected void BtnSave_Click(object sender, EventArgs e)
    {

        if (!Page.IsValid)
        {
            return;
        }
        string value = confirm_value12.Value;
        if (value == "Yes")
        {
            string AppendInstitutionNamess = null;
            int countCorrAuth = 0;
            int countAuthType = 0;
            int countConfAttended = 0;
            int countConfPresent = 0;

            try
            {

                Business b = new Business();
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                ArrayList listIndexAgency = new ArrayList();
                Business B = new Business();
                PublishData j = new PublishData();
                PublishData[] JD = new PublishData[dtCurrentTable.Rows.Count];

                string FileUpload = "";

                string confirmValue2 = Request.Form["confirm_value"];
                if (confirmValue2 == "Yes" || confirmValue2 == null || confirmValue2 == "")
                {

                    string PublicationEntry = DropDownListPublicationEntry.SelectedValue;
                    if (TextBoxPubId.Text != "")
                    {
                        FileUpload = b.GetFileUploadPath(TextBoxPubId.Text, PublicationEntry);
                        j.FilePathNew = FileUpload;
                    }

                    if (DropDownListPublicationEntry.SelectedValue == "JA" || DropDownListPublicationEntry.SelectedValue == "TS" || DropDownListPublicationEntry.SelectedValue == "PR")
                    {
                        string month = DropDownListMonthJA.SelectedValue;
                        int month2 = Convert.ToInt32(month);
                        if (month2 < 10 && TextBoxYearJA.Text == "2015")
                        {
                            // Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "script", "alert('Please enter the valid month and year!');", true);
                            string CloseWindow1 = "alert('Please enter the valid month and year!')";
                            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow1, true);

                            //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please enter the valid month and year!')</script>");
                            return;
                        }

                    }

                    if (DropDownListPublicationEntry.SelectedValue == "JA" || DropDownListPublicationEntry.SelectedValue == "TS" || DropDownListPublicationEntry.SelectedValue == "PR")
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
                            //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please enter the valid month and year!')</script>");
                            string CloseWindow1 = "alert('Please enter the valid month and year')";
                            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow1, true);
                            return;
                        }

                    }
                    if (DropDownListPublicationEntry.SelectedValue == "BK")
                    {
                        string monthnow = DateTime.Now.Month.ToString();
                        string yearnow = DateTime.Now.Year.ToString();

                        int monthnow2 = Convert.ToInt32(monthnow);
                        int yearnow2 = Convert.ToInt32(yearnow);


                        string month = DropDownListBookMonth.SelectedValue;
                        int month2 = Convert.ToInt32(month);

                        string year = TextBoxYear.Text;
                        int year2 = Convert.ToInt32(year);


                        if (monthnow2 < month2 && yearnow2 <= year2)
                        {
                            //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please enter the valid month and year!')</script>");
                            string CloseWindow1 = "alert('Please enter the valid month and year!')";
                            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow1, true);
                            return;
                        }

                    }


                    string MUCategorization = DropDownListMuCategory.SelectedValue;

                    string TitleWorkItem = txtboxTitleOfWrkItem.Text.Trim();
                    string PubJournal = TextBoxPubJournal.Text.Trim();
                    string PubJournalName = TextBoxNameJournal.Text.Trim();
                    string NameJournal = TextBoxNameJournal.Text.Trim();

                    string Volume = TextBoxVolume.Text.Trim();
                    string JAMonthJA = DropDownListMonthJA.SelectedValue;

                    string YearJA = TextBoxYearJA.SelectedValue;
                    string PageFrom = TextBoxPageFrom.Text.Trim();

                    string PageTo = TextBoxPageTo.Text.Trim();

                    string Issue = TextBoxIssue.Text.Trim();

                    string Indexed = RadioButtonListIndexed.SelectedValue;

                    string IndexAgency = CheckboxIndexAgency.SelectedValue;

                    string PubType = DropDownListPubType.SelectedValue;

                    string ImpFact = TextBoxImpFact.Text.Trim();
                    string fiveImpFact = TextBoxImpFact5.Text.Trim();

                    string EventTitle = TextBoxEventTitle.Text.Trim();

                    string Place = TextBoxPlace.Text.Trim();

                    string Date = TextBoxDate.Text.Trim();
                    string Date1 = TextBoxDate1.Text.Trim();

                    string TitleBook = TextBoxTitleBook.Text.Trim();



                    string ChapterContributed = TextBoxChapterContributed.Text.Trim();

                    string Edition = TextBoxEdition.Text.Trim();

                    string Publisher = TextBoxPublisher.Text.Trim();

                    string Year = TextBoxYearJA.SelectedValue;


                    string PageNum = TextBoxPageNum.Text.Trim();

                    string Volume1 = TextBoxVolume1.Text.Trim();

                    //string ofURL = TextBoxURL.Text.Trim();
                    string Publish = TextBoxPublisher.Text.Trim();


                    string DateOfPublish = TextBoxDateOfNewsPublish.Text.Trim();

                    string BookYear = TextBoxYear.SelectedValue;
                    string BookMonth = DropDownListBookMonth.SelectedValue;

                    string PageNumNewsPaper = TextBoxPageNumNewsPaper.Text.Trim();


                    string DOINum = TextBoxDOINum.Text.Trim();

                    string Keywords = TextBoxKeywords.Text.Trim();

                    string Abstract = TextBoxAbstract.Text.Trim();

                    // string Reference = TextBoxReference.Text.Trim();


                    string UploadPdf = "";

                    string Status = "NEW";
                    string isERF = DropDownListErf.SelectedValue;
                    string BISBN = txtbISBN.Text;
                    string BSection = txtbSection.Text;
                    string BChapter = txtChapter.Text;
                    string BCountry = txtCountry.Text;
                    string BTypeofPublication = DropDownListBookPublicationType.SelectedValue;

                  
                    if (DropDownListhasProjectreference.SelectedValue == "0")
                     {
                            //if (TextBoxProjectDetails.Text == "")
                            //{
                                string CloseWindow1 = "alert('Please select Has Project Reference!')";
                                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow1, true);
                                return;
                            //}
                      }
                 
                        if (DropDownListhasProjectreference.SelectedValue == "Y")
                        {
                            if (TextBoxProjectDetails.Text == "")
                            {
                                string CloseWindow1 = "alert('Please select the Project Details!')";
                                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow1, true);
                                return;
                            }
                        }
                    
                    
                    string ProjectIDlist = TextBoxProjectDetails.Text.Trim();
                    string hasProjectreference = DropDownListhasProjectreference.SelectedValue.Trim();
                    j.ProjectIDlist = ProjectIDlist;
                    j.hasProjectreference = hasProjectreference;

                    j.PaublicationID = TextBoxPubId.Text.Trim();
                    j.TypeOfEntry = PublicationEntry;
                    j.MUCategorization = MUCategorization;

                    j.TitleWorkItem = TitleWorkItem;
                    string inst = Session["InstituteId"].ToString();
                    string dept = Session["Department"].ToString();

                    //   j.CorrespondingAuthor = CorrespondingAuthor;
                    if (DropDownListPublicationEntry.SelectedValue == "BK")
                    {
                        string BookDate = "" + BookYear + "-" + BookMonth + "-01";
                        DateTime BkDate = Convert.ToDateTime(BookDate);
                        j.PublishDate = BkDate;
                    }
                    if (DropDownListPublicationEntry.SelectedValue == "JA" || DropDownListPublicationEntry.SelectedValue == "TS" || DropDownListPublicationEntry.SelectedValue == "PR")
                    {
                        string PublishDate = "" + YearJA + "-" + JAMonthJA + "-01";
                        DateTime pubDate = Convert.ToDateTime(PublishDate);
                        j.PublishDate = pubDate;
                    }
                    j.PublisherOfJournal = PubJournal;
                    j.PublisherOfJournalName = PubJournalName;
                    if (DropDownListPublicationEntry.SelectedValue == "CP")
                    {
                        j.ConfISBN = TextBoxIsbn.Text.Trim();
                    }
                    else if (DropDownListPublicationEntry.SelectedValue == "BK")
                    {
                        j.ConfISBN = BISBN;
                    }
                    j.NameOfJournal = NameJournal;
                    j.Volume = Volume;
                    j.MonthJA = JAMonthJA;
                    j.JAVolume = Volume;
                    if (JAMonthJA != "")
                    {
                        j.PublishJAMonth = Convert.ToInt16(JAMonthJA);
                    }

                    j.PublishJAYear = Year;
                    j.PageFrom = PageFrom;
                    j.PageTo = PageTo;

                    if (DropDownListPublicationEntry.SelectedValue == "JA" || DropDownListPublicationEntry.SelectedValue == "TS" || DropDownListPublicationEntry.SelectedValue == "PR")
                    {
                        j.Indexed = Indexed;
                    }
                    else
                    {
                    }

                    j.IndexedIn = IndexAgency;
                    j.Publicationtype = PubType;

                    j.ImpactFactor = ImpFact;
                    j.ImpactFactor5 = fiveImpFact;
                    if (txtIFApplicableYear.Text.Trim() != "")
                    {
                        j.IFApplicableYear = Convert.ToInt16(txtIFApplicableYear.Text.Trim());
                    }
                    j.Issue = Issue;

                    j.ConferenceTitle = EventTitle;
                    j.Place = Place;
                    if (TextBoxDate.Text != "")
                    {
                        j.Date = Convert.ToDateTime(Date);
                    }
                    if (TextBoxDate1.Text != "")
                    {
                        j.todate = Convert.ToDateTime(Date1);
                    }
                    j.TitleOfBook = TitleBook;
                    j.TitileOfBookChapter = TextBoxChapterContributed.Text.Trim();
                    j.Edition = Edition;
                    j.Publisher = Publish;
                    j.BookPublishYear = BookYear;
                    if (BookMonth != "")
                    {
                        j.BookPublishMonth = Convert.ToInt16(BookMonth);
                    }
                    j.BookPageNum = PageNum;

                    j.BookVolume = Volume1;

                    j.BookSection = BSection;
                    j.BookChapter = BChapter;
                    j.BookCountry = BCountry;
                    j.BookTypeofPublication = BTypeofPublication;

                    //j.url = ofURL;
                    j.DOINum = DOINum;
                    j.Keywords = Keywords;
                    j.Abstract = Abstract;
                    //  j.TechReferences = Reference;
                    j.UploadPDF = UploadPdf;
                    j.Status = Status;
                    j.CreatedBy = Session["UserId"].ToString();
                    j.CreatedDate = DateTime.Now;
                    //j.ApprovedBy = Session["UserId"].ToString();
                    //j.ApprovedDate = DateTime.Now;
                    j.isERF = DropDownListErf.SelectedValue;
                    j.TitileOfChapter = "";
                    j.InstUser = Session["InstituteId"].ToString();
                    j.DeptUser = Session["Department"].ToString();
                    //j.CitationUrl = txtCitation.Text;
                    //  j.SupervisorID = Session["SupervisorId"].ToString();
                    string role = Session["Role"].ToString();
                    string frommonth = ConfigurationManager.AppSettings["QuartileMonthFrom"].ToString();
                    j.Quartilefrommonth = Convert.ToInt32(frommonth);
                    string Tomonth = ConfigurationManager.AppSettings["QuartileMonthTo"].ToString();
                    j.QuartileTomonth = Convert.ToInt32(Tomonth); 
                    if (role != "1")
                    {
                        string AutoAppr = null;
                        AutoAppr = b.GetIstDeptAutoApprove(Session["InstituteId"].ToString(), Session["Department"].ToString());
                        if (AutoAppr == null)
                        {
                            j.AutoAppoval = "";
                        }
                        else
                        {
                            j.AutoAppoval = AutoAppr;
                        }
                    }
                    else
                    {
                        j.AutoAppoval = "Y";
                    }


                    j.uploadEPrint = DropDownListuploadEPrint.SelectedValue;
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

                    if (DropDownListPublicationEntry.SelectedValue == "NM")
                    {
                        j.NewsPublisher = TextBoxNewsPublish.Text.Trim();
                        if (TextBoxDateOfNewsPublish.Text != "")
                        {
                            string date2 = TextBoxDateOfNewsPublish.Text.Trim();
                            j.NewsPublishedDate = Convert.ToDateTime(date2);
                        }
                        j.NewsPageNum = TextBoxPageNumNewsPaper.Text.Trim();

                    }
                    string LibId = null;
                    LibId = b.GetLibraryId(inst, dept);
                    j.LibraryId = LibId;


                    if (TextBoxCreditPoint.Text == "")
                    {
                        TextBoxCreditPoint.Text = "0";
                    }
                    j.CreditPoint = Convert.ToInt32(TextBoxCreditPoint.Text.Trim());
                    j.TypePresentation = RadioButtonListTypePresentaion.SelectedValue;
                    j.AwardedBy = TextBoxAwardedBy.Text.Trim();
                    j.IsStudentAuthor = "N";
                    if (DropDownListPublicationEntry.SelectedValue == "JA" || DropDownListPublicationEntry.SelectedValue == "TS" || DropDownListPublicationEntry.SelectedValue == "PR")
                    {
                        for (int i = 0; i < CheckboxIndexAgency.Items.Count; i++)
                        {
                            if (CheckboxIndexAgency.Items[i].Selected)
                            {
                                listIndexAgency.Add(CheckboxIndexAgency.Items[i].Value);
                            }
                        }
                    }
                    if (DropDownListPublicationEntry.SelectedValue == "CP")
                    {
                        string IndexedCP = RadioButtonListCPIndexed.SelectedValue;
                        string IndexAgencyCP = CheckBoxListCPIndexedIn.SelectedValue;
                        string PageFromCP = TextBoxPageFromCP.Text.Trim();
                        string PageToCP = TextBoxPageToCP.Text.Trim();
                        string CPMonth = DropDownListMonthCP.SelectedValue;
                        string CPYear = TextBoxYearCP.Text.Trim();
                        string CPCity = txtcity.Text.Trim();
                        string CPState = txtState.Text.Trim();
                        string Conferencetype = DropDownListConferencetype.SelectedValue;
                        string FundsUtilized = TextBoxFunds.Text.Trim();
                        if (FundsUtilized != "" && FundsUtilized != null)
                        {
                            j.FundsUtilized = Convert.ToDouble(FundsUtilized);
                        }
                        j.Indexed = IndexedCP;
                        j.IndexedIn = IndexAgencyCP;
                        j.PageFrom = PageFromCP;
                        j.PageTo = PageToCP;
                        j.PublishJAMonth = Convert.ToInt32(CPMonth);
                        j.PublishJAYear = CPYear;
                        j.CPCity = CPCity;
                        j.CPState = CPState;
                        j.Publicationtype = Conferencetype;
                        for (int i = 0; i < CheckBoxListCPIndexedIn.Items.Count; i++)
                        {
                            if (CheckBoxListCPIndexedIn.Items[i].Selected)
                            {
                                listIndexAgency.Add(CheckBoxListCPIndexedIn.Items[i].Value);
                            }
                        }
                    }

                    //if (DropDownListPublicationEntry.SelectedValue == "JA" || DropDownListPublicationEntry.SelectedValue == "TS")
                    //{
                    //    if (RadioButtonListIndexed.SelectedValue == "Y")
                    //    {
                    //        if (listIndexAgency.Count == 0)
                    //        {
                    //            ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please select the index agency!')</script>");
                    //            return;

                    //        }
                    //    }
                    //}



                    int rowIndex1 = 0;
                    if (dtCurrentTable.Rows.Count > 0)
                    {

                        for (int i = 0; i < dtCurrentTable.Rows.Count; i++)
                        {
                            JD[i] = new PublishData();

                            TextBox AuthorName = (TextBox)Grid_AuthorEntry.Rows[rowIndex1].Cells[1].FindControl("AuthorName");
                            DropDownList DropdownMuNonMu = (DropDownList)Grid_AuthorEntry.Rows[rowIndex1].Cells[2].FindControl("DropdownMuNonMu");
                            TextBox EmployeeCode = (TextBox)Grid_AuthorEntry.Rows[rowIndex1].Cells[1].FindControl("EmployeeCode");
                            HiddenField Institution = (HiddenField)Grid_AuthorEntry.Rows[rowIndex1].Cells[0].FindControl("Institution");
                            TextBox InstitutionName = (TextBox)Grid_AuthorEntry.Rows[rowIndex1].Cells[6].FindControl("InstitutionName");
                            HiddenField Department = (HiddenField)Grid_AuthorEntry.Rows[rowIndex1].Cells[0].FindControl("Department");
                            TextBox DepartmentName = (TextBox)Grid_AuthorEntry.Rows[rowIndex1].Cells[0].FindControl("DepartmentName");

                            DropDownList DropdownStudentInstitutionName = (DropDownList)Grid_AuthorEntry.Rows[rowIndex1].Cells[0].FindControl("DropdownStudentInstitutionName");
                            DropDownList DropdownStudentDepartmentName = (DropDownList)Grid_AuthorEntry.Rows[rowIndex1].Cells[0].FindControl("DropdownStudentDepartmentName");


                            TextBox MailId = (TextBox)Grid_AuthorEntry.Rows[rowIndex1].Cells[0].FindControl("MailId");
                            DropDownList isCorrAuth = (DropDownList)Grid_AuthorEntry.Rows[rowIndex1].Cells[0].FindControl("isCorrAuth");
                            DropDownList AuthorType = (DropDownList)Grid_AuthorEntry.Rows[rowIndex1].Cells[0].FindControl("AuthorType");
                            TextBox NameInJournal = (TextBox)Grid_AuthorEntry.Rows[rowIndex1].Cells[0].FindControl("NameInJournal");

                            DropDownList IsPresenter = (DropDownList)Grid_AuthorEntry.Rows[rowIndex1].Cells[0].FindControl("IsPresenter");
                            CheckBox HasAttented = (CheckBox)Grid_AuthorEntry.Rows[rowIndex1].Cells[0].FindControl("HasAttented");


                            DropDownList NationalType = (DropDownList)Grid_AuthorEntry.Rows[rowIndex1].Cells[0].FindControl("NationalType");
                            DropDownList ContinentId = (DropDownList)Grid_AuthorEntry.Rows[rowIndex1].Cells[0].FindControl("ContinentId");

                            HiddenField Institution1 = (HiddenField)Grid_AuthorEntry.Rows[0].Cells[0].FindControl("Institution");
                            string a = Institution1.Value;

                            HiddenField Institution2 = (HiddenField)Grid_AuthorEntry.Rows[0].Cells[0].FindControl("Institution");

                            //if (DropdownMuNonMu.SelectedValue == "O")
                            //{

                            //    if (EmployeeCode.Text == "")
                            //    {
                            //        ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please enter Roll No!')</script>");
                            //        return;
                            //    }
                            //}



                            if (AuthorName.Text == "")
                            {
                                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please enter Author Name!')</script>");
                                return;

                            }

                            if (DropdownMuNonMu.SelectedValue == "M")
                            {
                                if (InstitutionName.Text == "")
                                {
                                    string CloseWindow1 = "alert('Please enter Institution Name!')";
                                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow1, true);
                                    //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please enter Institution Name!')</script>");
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


                            JD[i].EmployeeCode = EmployeeCode.Text.Trim();
                            JD[i].AuthorName = AuthorName.Text.Trim();
                            JD[i].MUNonMU = DropdownMuNonMu.Text.Trim();
                            if (DropDownListPublicationEntry.SelectedValue == "JA" || DropDownListPublicationEntry.SelectedValue == "TS" || DropDownListPublicationEntry.SelectedValue == "CP" || DropDownListPublicationEntry.SelectedValue == "PR")
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
                                if (DropDownListPublicationEntry.SelectedValue == "JA" || DropDownListPublicationEntry.SelectedValue == "TS" || DropDownListPublicationEntry.SelectedValue == "CP" || DropDownListPublicationEntry.SelectedValue == "PR")
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
                                    //JD[i].EmployeeCode = JD[i].EmployeeCode;
                                }
                                JD[i].NationalInternationl = "";
                                JD[i].continental = "";


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

                            JD[i].isCorrAuth = isCorrAuth.Text.Trim();
                            if (DropDownListPublicationEntry.SelectedValue == "JA" || DropDownListPublicationEntry.SelectedValue == "TS" || DropDownListPublicationEntry.SelectedValue == "BK" || DropDownListPublicationEntry.SelectedValue == "PR")
                            {
                                JD[i].AuthorType = AuthorType.Text.Trim();
                            }
                            else
                            {
                                JD[i].AuthorType = "A";
                            }
                            JD[i].NameInJournal = NameInJournal.Text.Trim();

                            if (DropDownListPublicationEntry.SelectedValue == "CP")
                            {
                                JD[i].IsPresenter = IsPresenter.Text.Trim();
                                if (HasAttented.Checked == true)
                                {
                                    JD[i].HasAttented = "Y";
                                    JD[i].AuthorCreditPoint = Convert.ToInt32(TextBoxCreditPoint.Text.Trim());
                                }
                                else
                                {
                                    JD[i].HasAttented = "N";
                                    JD[i].AuthorCreditPoint = 0;
                                }
                            }
                            else
                            {
                                JD[i].IsPresenter = "";

                                JD[i].HasAttented = "";

                                JD[i].AuthorCreditPoint = 0;

                            }

                            if (DropDownListPublicationEntry.SelectedValue == "JA" || DropDownListPublicationEntry.SelectedValue == "TS" || DropDownListPublicationEntry.SelectedValue == "PR")
                            {
                                if (JD[i].AuthorType == "0")
                                {
                                    ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please Select the Author Type!')</script>");
                                    return;
                                }
                                if (JD[i].AuthorType == "P")
                                {
                                    countAuthType = countAuthType + 1;
                                }

                                if (JD[i].isCorrAuth == "Y")
                                {
                                    countCorrAuth = countCorrAuth + 1;
                                }
                            }

                            if (DropDownListPublicationEntry.SelectedValue == "CP")
                            {
                                if (JD[i].HasAttented == "Y")
                                {
                                    countConfAttended = countConfAttended + 1;
                                }

                                if (JD[i].IsPresenter == "Y")
                                {
                                    countConfPresent = countConfPresent + 1;
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
                                //id = Session["InstituteId"].ToString();
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
                    if (DropDownListPublicationEntry.SelectedValue == "JA" || DropDownListPublicationEntry.SelectedValue == "TS")
                    {
                        //if (countAuthType > 1)
                        //{
                        //    ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('First Author cannot be more than one!')</script>");
                        //    return;

                        //}
                        //if (countAuthType == 0)
                        //{
                        //    ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Select atleast one Author Type as First Author !')</script>");
                        //    return;

                        //}

                        //if (countCorrAuth > 1)
                        //{
                        //    ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Corresponding Author cannot be more than one!')</script>");
                        //    return;

                        //}

                        //if (countCorrAuth == 0)
                        //{
                        //    ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Select atleast one Corresponding Author!')</script>");
                        //    return;

                        //}
                    }
                    if (DropDownListPublicationEntry.SelectedValue == "CP")
                    {
                        if (countConfAttended == 0)
                        {
                            ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('There should be atleast one presenter must be attended the conference !')</script>");
                            return;

                        }

                        if (countConfPresent == 0)
                        {
                            ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('There should be atleast one presenter !')</script>");
                            return;

                        }
                    }

                    string PublicationEntry1 = DropDownListPublicationEntry.SelectedValue;
                    int savedfileflag = 0;
                    string filelocationpath = "";
                    string UploadPdf1 = "";

                    if (DropDownListPublicationEntry.SelectedValue == "JA" || DropDownListPublicationEntry.SelectedValue == "TS" || DropDownListPublicationEntry.SelectedValue == "PR")
                    {
                        ArrayList list = new ArrayList();

                        string authorname = null;
                        for (int i = 0; i < JD.Length; i++)
                        {

                            list.Add(JD[i].EmployeeCode);

                        }

                        authorname = Session["UserId"].ToString();
                        if (!list.Contains(authorname))
                        {
                            ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Authors list should contain logged in faculty')</script>");
                            return;

                        }


                    }
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

                        //PubEntriesUpdatePanel.Update();
                        //EditUpdatePanel.Update();
                        //popupPanelAffilUpdate.Update();
                        //MainUpdate.Update();
                        int result = 0;
                        result = B.insertPublishEntry(j, JD, listIndexAgency);
                        if (result == 1)
                        {
                            panelFileUpload.Visible = true;
                            Buttonupload.Enabled = true;
                            FileUploadPdf.Enabled = true;
                            BtnFeedback.Visible = true;
                            int rowIndex = 0;
                            DropDownList DropdownMuNonMu = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[2].FindControl("DropdownMuNonMu");
                            TextBox EmployeeCode = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("EmployeeCode");
                            if (DropdownMuNonMu.Text == "S" || DropdownMuNonMu.Text == "O")
                            {
                                EmployeeCode.Text = "";
                            }
                            TextBoxPubId.Text = Session["Pubseed"].ToString();

                            //if (FileUploadPdf.HasFile)
                            //{
                            //    string uploadedfilename = Path.GetFileName(FileUploadPdf.PostedFile.FileName);
                            //    double size = FileUploadPdf.PostedFile.ContentLength;
                            //    if (size <= 10485760) //10mb
                            //    {
                            //        //string path_BoxId = Path.Combine(mainpath, TextBoxPubId.Text); //main path + location
                            //        string servername = ConfigurationManager.AppSettings["ServerName"].ToString();
                            //        string domainame = ConfigurationManager.AppSettings["DomainName"].ToString();
                            //        string username = ConfigurationManager.AppSettings["UserName"].ToString();
                            //        string password = ConfigurationManager.AppSettings["Password"].ToString();
                            //        string FolderPathServerwrite = ConfigurationManager.AppSettings["FolderPath"].ToString();

                            //        using (NetworkShareAccesser.Access(servername, domainame, username, password))
                            //        {
                            //            var uploadfolder = FolderPathServerwrite;
                            //            string path_BoxId = Path.Combine(uploadfolder, TextBoxPubId.Text); //main path + location
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

                            //            j.PaublicationID = TextBoxPubId.Text.Trim();
                            //            j.TypeOfEntry = DropDownListPublicationEntry.SelectedValue;
                            //            //j.PaublicationID = TextBoxPubId.Text.Trim();
                            //            if (FileUpload == "")
                            //            {
                            //                j.FilePath = filelocationpath;
                            //            }
                            //            else
                            //            {
                            //                j.FilePath = j.FilePathNew;
                            //            }

                            //            int result1 = B.UpdatePfPathCreate(j);

                            //            btnSave.Enabled = false;
                            //            Business b1 = new Business();
                            //            FileUpload = b1.GetFileUploadPath(j.PaublicationID, j.TypeOfEntry);
                            //            if (FileUpload != "")
                            //            {
                            //                GVViewFile.Visible = true;
                            //            }
                            //            else
                            //            {
                            //                GVViewFile.Visible = false;
                            //            }
                            //            DSforgridview.SelectCommand = "select UploadPDFPath from Publication where PublicationID='" + j.PaublicationID + "' and TypeOfEntry='" + j.TypeOfEntry + "'";
                            //            DSforgridview.DataBind();
                            //            GVViewFile.DataBind();

                            //        }
                            //    }


                            //    else
                            //    {

                            //        string CloseWindow1 = "alert('Invalid File!!!File exceeds 10MB..Please try to upload the file less than or equal to 10MB!!!!!!')";
                            //        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow1, true);
                            //        //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Invalid File!!!File exceeds 10MB..Please try to upload the file less than or equal to 10MB!!!!!!')</script>");
                            //        log.Error("Invalid File!!!File exceeds 10MB!!! : " + TextBoxPubId.Text.Trim() + " , Project Type : " + DropDownListPublicationEntry.SelectedValue);
                            //        return;
                            //    }

                            //}


                            //}

                            // FileSave(sender, e);

                            string CloseWindow = "alert('Publication data Created Successfully..of Entry Type: " + DropDownListPublicationEntry.SelectedValue + " of ID: " + TextBoxPubId.Text + " For update Click on search and edit  !')";
                            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow, true);
                            // ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);

                            //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Publication data Created Successfully..of Entry Type: " + DropDownListPublicationEntry.SelectedValue + " of ID: " + TextBoxPubId.Text + " For update Click on search and edit  !')</script>");
                            log.Info("Publication created Successfully,  of Entry Type: " + DropDownListPublicationEntry.SelectedValue + " of ID: " + TextBoxPubId.Text);
                            //popup.Visible = false;
                            popupPanelJournal.Visible = false;
                            popupPanelProceedingsJournal.Visible = false;
                            lblmsg.Visible = true;
                            Session["TempPid"] = TextBoxPubId.Text.ToString();
                            Session["TempTypeEntry"] = DropDownListPublicationEntry.SelectedValue;
                            fnRecordExist(sender, e);
                        }
                        else
                        {
                            log.Error("Publication creation Error!!!,  of Entry Type: " + DropDownListPublicationEntry.SelectedValue + " of ID: " + TextBoxPubId.Text);
                            string CloseWindow = "alert('Publication creation Error')";
                            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow, true);
                            // ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Error!!!!!!!!!!!!')</script>");

                        }
                    }
                    else
                    {
                        //if (Directory.Exists(mainpath))
                        //{
                        //FileUploadPdf.Enabled = true;
                        //if (FileUploadPdf.HasFile)
                        //{
                        //    string uploadedfilename = Path.GetFileName(FileUploadPdf.PostedFile.FileName);
                        //    double size = FileUploadPdf.PostedFile.ContentLength;
                        //    //PublishData j = new PublishData();
                        //    if (size <= 10485760) //10mb
                        //    {
                        //        // string path_BoxId = Path.Combine(mainpath, TextBoxPubId.Text); //main path + location
                        //        string servername = ConfigurationManager.AppSettings["ServerName"].ToString();
                        //        string domainame = ConfigurationManager.AppSettings["DomainName"].ToString();
                        //        string username = ConfigurationManager.AppSettings["UserName"].ToString();
                        //        string password = ConfigurationManager.AppSettings["Password"].ToString();
                        //        string FolderPathServerwrite = ConfigurationManager.AppSettings["FolderPath"].ToString();
                        //        using (NetworkShareAccesser.Access(servername, domainame, username, password))
                        //        {
                        //            // var uploadfolder = @"\\172.16.50.233\backup\reema\read\RMS\";
                        //            var uploadfolder = FolderPathServerwrite;
                        //            string path_BoxId = Path.Combine(uploadfolder, TextBoxPubId.Text); //main path + location
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
                        //            j.PaublicationID = TextBoxPubId.Text.Trim();
                        //            j.TypeOfEntry = DropDownListPublicationEntry.SelectedValue;

                        //            if (FileUploadPdf.HasFile)
                        //            {
                        //                j.FilePath = filelocationpath;
                        //            }
                        //            else
                        //            {
                        //                j.FilePath = j.FilePathNew;
                        //            }

                        //        }


                        //    }
                        //    else
                        //    {
                        //        string CloseWindow = "alert('Invalid File!!!File exceeds more than 10MB..Please try to upload the file less than or equal to 10MB!!!!!!')";
                        //        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow, true);
                        //        //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Invalid File!!!File exceeds more than 10MB..Please try to upload the file less than or equal to 10MB!!!!!!')</script>");
                        //        log.Error("Invalid File!!!File exceeds more than 10MB!!! : " + TextBoxPubId.Text.Trim() + " , Project Type : " + DropDownListPublicationEntry.SelectedValue);
                        //        return;
                        //    }
                        //}

                        ////}
                        int result = 0;

                        if (GVViewFile.Rows.Count != 0)
                        {
                            if (FileUploadPdf.HasFile)
                            {
                            }
                            else
                            {

                                Label filepath = (Label)GVViewFile.Rows[0].FindControl("lblgetid");
                                string path = filepath.Text;
                                if (path != "")
                                {
                                    j.FilePath = path;
                                }
                            }
                        }
                        result = B.UpdatePublishEntry(j, JD, listIndexAgency);

                        if (result == 1)
                        {
                            DSforgridview.SelectParameters.Clear();
                            DSforgridview.SelectParameters.Add("TypeOfEntry", j.TypeOfEntry);
                            DSforgridview.SelectParameters.Add("PaublicationID", j.PaublicationID);
                            DSforgridview.SelectCommand = "select UploadPDFPath from Publication where PublicationID=@PaublicationID  and TypeOfEntry=@TypeOfEntry ";
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
                            // FileSave(sender, e);
                            string CloseWindow = "alert('Publication data Updated Successfully of Entry Type: " + DropDownListPublicationEntry.SelectedValue + " of ID: " + TextBoxPubId.Text + "')";
                            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow, true);
                            // ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Publication data Updated Successfully of Entry Type: " + DropDownListPublicationEntry.SelectedValue + " of ID: " + TextBoxPubId.Text + "')</script>");
                            log.Info("Publication Updated Successfully,  of Entry Type: " + DropDownListPublicationEntry.SelectedValue + " of ID: " + TextBoxPubId.Text);
                            //popup.Visible = false;
                            popupPanelJournal.Visible = false;
                            popupPanelProceedingsJournal.Visible = false;
                            // popupPanelAffil.Visible=false;
                            Session["TempPid"] = TextBoxPubId.Text.ToString();
                            Session["TempTypeEntry"] = DropDownListPublicationEntry.SelectedValue;
                            fnRecordExist(sender, e);

                        }
                        else
                        {

                            string CloseWindow = "alert('Error!!!!!!!!!!!!')";
                            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow, true);
                            // ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Error!!!!!!!!!!!!')</script>");
                            log.Error("Publication Updated Error!!!,  of Entry Type: " + DropDownListPublicationEntry.SelectedValue + " of ID: " + TextBoxPubId.Text);

                        }
                    }

                    // addclik(sender, e);
                    btnSave.Enabled = true;
                    BtnFeedback.Enabled = true;
                }
            }

            catch (Exception ex)
            {
                log.Error("Inside Catch Block Of Publication" + ex.Message + " UserID : " + Session["UserId"].ToString());

                log.Error(ex.StackTrace);


                if (ex.Message.Contains("The string was not recognized as a valid DateTime"))
                {
                    string CloseWindow = "alert('Date is not valid!!!!!!!!!!!!')";
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow, true);

                    //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Date is not valid!!!!!!!!!!!!')</script>");

                }
                else if (ex.Message.Contains("IX_Publication"))
                {
                    string CloseWindow = "alert('Publication Creation Failed. Title of the WorkItem Already Present!')";
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow, true);
                    // ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Publication Creation Failed. Title of the WorkItem Already Present!')</script>");

                }


                else
                {
                    string CloseWindow1 = "alert('Publication data Creation Failed!!!!!!!!!!!!')";
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow1, true);
                    // ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Publication data Creation Failed!!!!!!!!!!!!')</script>");

                }


            }

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
        panelRemarks.Visible = false;
        TextBoxRemarks.Text = "";

        panAddAuthor.Visible = false;
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

        RadioButtonListCPIndexed.ClearSelection();
        CheckBoxListCPIndexedIn.ClearSelection();



        panelConfPaper.Visible = false;

        TextBoxEventTitle.Text = "";
        TextBoxPlace.Text = "";
        TextBoxDate.Text = "";
        TextBoxDate1.Text = "";
        panelBookPublish.Visible = false;
        lblmsg.Visible = false;

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
        BtnFeedback.Enabled = false;

        panelTechReport.Visible = false;

        //TextBoxURL.Text = "";
        TextBoxDOINum.Text = "";
        TextBoxAbstract.Text = "";
        DropDownListErf.ClearSelection();
        TextBoxKeywords.Text = "";
        //TextBoxReference.Text = "";
        DropDownListuploadEPrint.ClearSelection();
        TextBoxEprintURL.Text = "";

        //popup.Visible = false;
        popupPanelJournal.Visible = false;
        popupPanelProceedingsJournal.Visible = false;
        //popupstudent.Visible = false;
        TextBoxIsbn.Text = "";
        RadioButtonListTypePresentaion.ClearSelection();
        TextBoxAwardedBy.Text = "";
        GVViewFile.Visible = false;
        TextBoxCreditPoint.Text = "0";
        TextBoxAwardedBy.Enabled = false;
        btnSave.Enabled = false;
        LabelProjectReference.Visible = false;
        LabelProjectRef.Visible = false;
        LabelhasProjectreferenceNote.Visible = false;

        //LabelProjectRef.Visible = false;
        DropDownListhasProjectreference.Visible = false;
        LabelProjectDetails.Visible = false;
        TextBoxProjectDetails.Visible = false;
        ImageButtonProject.Visible = false;

        //btnSave.Enabled = false;
        //BtnFeedback.Enabled = false;
        //BtnFeedback.Visible = true;

    }


    protected void addclik1(object sender, EventArgs e)
    {
        btnSave.Enabled = false;
        BtnFeedback.Enabled = false;
        BtnFeedback.Visible = true;

    }

    protected void addclikEntryType(object sender, EventArgs e)
    {
        //string confirmValue2 = Request.Form["confirm_value2"];
        //if (confirmValue2 == "Yes")
        //{
        //  DropDownListPublicationEntry.Enabled = true;
        //  DropDownListPublicationEntry.ClearSelection();
        DropDownListMuCategory.ClearSelection();
        DropDownListMuCategoryOnSelectedIndexChanged(sender, e);
        TextBoxPubId.Text = "";
        txtboxTitleOfWrkItem.Text = "";
        // panelRemarks.Visible = false;
        TextBoxRemarks.Text = "";
        BtnAddMU.Enabled = true;

        // panAddAuthor.Visible = false;
        // Grid_AuthorEntry.DataSource = null;
        // Grid_AuthorEntry.DataBind();
        // Grid_AuthorEntry.Visible = false;

        //panelJournalArticle.Visible = false;

        TextBoxPubJournal.Text = "";

        TextBoxNameJournal.Text = "";
        TextBoxIssue.Text = "";

        DropDownListMonthJA.ClearSelection();

        TextBoxYearJA.ClearSelection();

        TextBoxImpFact.Text = "";

        DropDownListPubType.ClearSelection();
        TextBoxPageFrom.Text = "";

        TextBoxPageTo.Text = "";
        TextBoxVolume.Text = "";
        RadioButtonListIndexed.SelectedValue = "Y";
        RadioButtonListIndexedOnSelectedIndexChanged(sender, e);
        CheckboxIndexAgency.ClearSelection();
        RadioButtonListCPIndexed.ClearSelection();
        CheckBoxListCPIndexedIn.ClearSelection();

        //panelConfPaper.Visible = false;

        TextBoxEventTitle.Text = "";
        TextBoxPlace.Text = "";
        TextBoxDate.Text = "";
        TextBoxDate1.Text = "";
        //  panelBookPublish.Visible = false;
        //  lblmsg.Visible = false;

        TextBoxTitleBook.Text = "";
        TextBoxChapterContributed.Text = "";
        TextBoxEdition.Text = "";
        TextBoxPublisher.Text = "";

        TextBoxYear.ClearSelection();
        DropDownListBookMonth.ClearSelection();
        TextBoxPageNum.Text = "";
        TextBoxVolume1.Text = "";


        // panelOthes.Visible = false;

        TextBoxNewsPublish.Text = "";
        TextBoxDateOfNewsPublish.Text = "";
        TextBoxPageNumNewsPaper.Text = "";
        // BtnFeedback.Enabled = false;

        //  panelTechReport.Visible = false;

        //TextBoxURL.Text = "";
        TextBoxDOINum.Text = "";
        TextBoxAbstract.Text = "";
        DropDownListErf.ClearSelection();
        TextBoxKeywords.Text = "";
        //TextBoxReference.Text = "";
        DropDownListuploadEPrint.ClearSelection();
        TextBoxEprintURL.Text = "";

        //  popupPanelAffil.Visible = false;
        //  popupPanelJournal.Visible = false;

        TextBoxIsbn.Text = "";
        RadioButtonListTypePresentaion.ClearSelection();
        TextBoxAwardedBy.Text = "";
        //  GVViewFile.Visible = false;
        TextBoxCreditPoint.Text = "0";
        // TextBoxAwardedBy.Enabled = false;
        txtIFApplicableYear.Text = "";
        TextBoxImpFact5.Text = "";

        //}

    }

    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        ImageButton EditButton = (ImageButton)e.Row.FindControl("BtnEdit");
    }

    public void edit(Object sender, GridViewEditEventArgs e)
    {

        UpdatePanel1.Update();
        PubEntriesUpdatePanel.Update();
        EditUpdatePanel.Update();
        //popupPanelAffilUpdate.Update();
        MainUpdate.Update();
        GridViewSearch.EditIndex = e.NewEditIndex;

        fnRecordExist(sender, e);
        dataBind();

    }
    //Function of edit button
    public void GridView2_RowCommand(Object sender, GridViewCommandEventArgs e)
    {

        //GVViewFile.DataBind();
        //DSforgridview.DataBind="";


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
            UpdatePanel1.Update();
            PubEntriesUpdatePanel.Update();
            EditUpdatePanel.Update();
            //popupPanelAffilUpdate.Update();
            MainUpdate.Update();

            GridViewRow rowSelect = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
            int rowindex = rowSelect.RowIndex;
            HiddenField TypeOfEntry = (HiddenField)GridViewSearch.Rows[rowindex].Cells[7].FindControl("TypeOfEntry");

            typeEntry = TypeOfEntry.Value;
            pid = GridViewSearch.Rows[rowindex].Cells[2].Text.Trim().ToString();
            Session["TempPid"] = pid;
            Session["TempTypeEntry"] = typeEntry;

            fnRecordExistApproval(sender, e);


            //  string autoapp = Session["AutoApproval"].ToString();
            string Role = Session["Role"].ToString();
            if (Role != "1")
            {
                Business b = new Business();
                string AutoAppr = null;
                AutoAppr = b.GetIstDeptAutoApprove(Session["InstituteId"].ToString(), Session["Department"].ToString());
                if (AutoAppr == null)
                {
                    AutoAppr = "N";
                }



                if (AutoAppr == "Y")
                {
                    BtnFeedback.Text = "Save & Approve";
                }
                else
                {
                    BtnFeedback.Text = "Save & Submit for Approval";
                }
            }
            else
            {
                BtnFeedback.Text = "Save & Approve";
            }
        }
    }

    public void fnRecordExist(object sender, EventArgs e)
    {

       // string MemberID = Session["UserId"].ToString();
       // User u = new User();
       // u = B.CheckUserforFeedback(MemberID);
       // string date3 = u.Created_Date.ToString();
        
       // string month = ConfigurationManager.AppSettings["FeedBackMonth"].ToString();
       // int month1 = Convert.ToInt32(month);
       // DateTime fromdate = Convert.ToDateTime(u.Created_Date);
       // DateTime todaydate = DateTime.Now;
       //int resu = B.gettotalmonths(fromdate, todaydate);

       //if (date3 == "01/01/0001 00:00:00" || resu >= month1)
       //{
       //    PanelFELFEEDBACK.Visible = true;
       //}
       //else
       //{
       //    PanelFELFEEDBACK.Visible = false;
       //}


        panelFileUpload.Visible = true;
        FileUploadPdf.Enabled = true;
        Buttonupload.Enabled = true;
        setModalWindow(sender, e);
        panAddAuthor.Enabled = true;
        //popup.Visible = false;
        lblmsg.Visible = true;
        lblmsg2.Visible = false;
        panelRemarks.Visible = false;
        BtnAddMU.Enabled = true;
        FileUploadPdf.Enabled = true;
        BtnFeedback.Visible = true;
        string Pid = Session["TempPid"].ToString();
        string TypeEntry = Session["TempTypeEntry"].ToString();
        btnSave.Enabled = true;
        BtnFeedback.Enabled = true;
        //GVViewFile.Visible = true;

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
        DSforgridview.SelectParameters.Add("PublicationID", Pid);
        DSforgridview.SelectParameters.Add("TypeOfEntry", TypeEntry);

        DSforgridview.SelectCommand = "select UploadPDFPath from Publication where PublicationID=@PublicationID and TypeOfEntry=@TypeOfEntry";
        DSforgridview.DataBind();
        GVViewFile.DataBind();

        FileUploadPdf.Visible = true;
        LabelUploadPfd.Visible = true;
        Business obj = new Business();
        PublishData v = new PublishData();
        v = obj.fnfindjid(Pid, TypeEntry);


        //FileUploadPdf.Visible = true;

        DropDownListMuCategory.SelectedValue = v.MUCategorization;
        DropDownListPublicationEntry.SelectedValue = TypeEntry;
        DropDownListPublicationEntry.Enabled = false;
        TextBoxPubId.Text = Pid;
        txtboxTitleOfWrkItem.Text = v.TitleWorkItem;
        TextBoxRemarks.Text = v.RemarksFeedback;
        TextBoxYearJA.Items.Clear();
        TextBoxYear.Items.Clear();
        DropDownListhasProjectreference.SelectedValue = v.hasProjectreference;
        TextBoxProjectDetails.Text = v.ProjectIDlist;
        if (v.hasProjectreference == "0"||  v.hasProjectreference == "N")
        {
            LabelProjectReference.Visible = true;
            LabelProjectRef.Visible = true;
            LabelhasProjectreferenceNote.Visible = true;

            DropDownListhasProjectreference.Visible = true;
            LabelProjectDetails.Visible = false;
            TextBoxProjectDetails.Visible = false;
            ImageButtonProject.Visible = false;
        }
        else if (v.hasProjectreference == "Y")
        {
            LabelProjectReference.Visible = true;
            LabelProjectRef.Visible = true;
            DropDownListhasProjectreference.Visible = true;
            LabelhasProjectreferenceNote.Visible = true;

            LabelProjectDetails.Visible = true;
            TextBoxProjectDetails.Visible = true;
            ImageButtonProject.Visible = true;

        }
        if (TypeEntry == "JA" || TypeEntry == "TS" || TypeEntry == "PR")
        {
            if (v.Indexed == "Y")
            {
                bool resultv = B.checkPredatoryJournal(TextBoxPubJournal.Text, TextBoxYearJA.SelectedValue);
                if (resultv == true)
                {
                    hdnPredatoryJournal.Value = "PRD";
                }
                else
                {
                    hdnPredatoryJournal.Value = "";
                }
            }
            else
            {
                hdnPredatoryJournal.Value = "";
            }
        }
        else
        {
            hdnPredatoryJournal.Value = "";
        }
        //  v = obj.fnfindjid(Pid, TypeEntry);
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
            TextBoxPageFrom.Text = v.PageFrom;
            if (v.PageFrom != "")
            {
                TextBoxPageTo.Enabled = true;
            }
            else
            {
                TextBoxPageTo.Enabled = false;
            }
            TextBoxPageTo.Text = v.PageTo;


            TextBoxIssue.Text = v.Issue;
            TextBoxVolume.Text = v.JAVolume;
            RadioButtonListIndexed.SelectedValue = v.Indexed;
            // CheckboxIndexAgency.ClearSelection();
            lblnote1.Visible = true;
            JournalData jd = new JournalData();
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
            else if (jayear >= 2019 && jamonth >= 1)
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
            TextBoxPageFrom.Text = v.PageFrom;
            if (v.PageFrom != "")
            {
                TextBoxPageTo.Enabled = true;
            }
            else
            {
                TextBoxPageTo.Enabled = false;
            }
            TextBoxPageTo.Text = v.PageTo;


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
            // DropDownListPubType.SelectedValue=;
            TextBoxPageNum.Text = v.BookPageNum;

            TextBoxVolume1.Text = v.BookVolume;
            lblnote1.Visible = false;
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

            if (v.Date.ToShortDateString() != "01/01/0001")
            {
                TextBoxDate.Text = v.Date.ToShortDateString();
            }
            if (v.todate.ToShortDateString() != "01/01/0001")
            {
                TextBoxDate1.Text = v.todate.ToShortDateString();
            }
            if (v.TypePresentation != "")
            {
                RadioButtonListTypePresentaion.SelectedValue = v.TypePresentation;
            }
            TextBoxCreditPoint.Text = v.CreditPoint.ToString();
            TextBoxAwardedBy.Text = v.AwardedBy;
            TextBoxIsbn.Text = v.ConfISBN;
            lblnote1.Visible = false;
            // CheckboxIndexAgency.ClearSelection();
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

            if (v.NewsPublishedDate.ToShortDateString() != "01/01/0001")
            {
                TextBoxDateOfNewsPublish.Text = v.NewsPublishedDate.ToShortDateString();
            }

            TextBoxPageNumNewsPaper.Text = v.NewsPageNum;

            lblnote1.Visible = false;
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
            TextBoxPageFrom.Text = v.PageFrom;
            TextBoxIssue.Text = v.Issue;
            TextBoxPageTo.Text = v.PageTo;
            TextBoxVolume.Text = v.JAVolume;
            RadioButtonListIndexed.SelectedValue = v.Indexed;
            lblnote1.Visible = true;
            // CheckboxIndexAgency.ClearSelection();
        }
        panelTechReport.Visible = false;
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
                ImageButton ImageButton1 = (ImageButton)Grid_AuthorEntry.Rows[rowIndex].Cells[2].FindControl("ImageButton1");

                drCurrentRow = dtCurrentTable.NewRow();

                DropdownMuNonMu.Text = dtCurrentTable.Rows[i - 1]["DropdownMuNonMu"].ToString();
                EmployeeCode.Text = dtCurrentTable.Rows[i - 1]["EmployeeCode"].ToString();
                AuthorName.Text = dtCurrentTable.Rows[i - 1]["AuthorName"].ToString();
                if (DropdownMuNonMu.Text == "M")
                {
                    ImageButton1.Visible = false;
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
                    ImageButton1.Visible = false;
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
                    ImageButton1.Visible = false;
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
                else if (DropdownMuNonMu.Text == "S")
                {
                    if (DropDownListPublicationEntry.SelectedValue == "JA" || DropDownListPublicationEntry.SelectedValue == "TS" || DropDownListPublicationEntry.SelectedValue == "CP" || DropDownListPublicationEntry.SelectedValue == "PR")
                    {
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
                    else
                    {
                        DropdownStudentInstitutionName.Visible = true;
                        DropdownStudentDepartmentName.Visible = true;
                        InstNme.Visible = false;
                        deptname.Visible = false;
                        DropdownStudentInstitutionName.Text = dtCurrentTable.Rows[i - 1]["Institution"].ToString();
                        DropdownStudentDepartmentName.Text = dtCurrentTable.Rows[i - 1]["Department"].ToString();
                        InstId.Value = dtCurrentTable.Rows[i - 1]["Institution"].ToString();
                        deptId.Value = dtCurrentTable.Rows[i - 1]["Department"].ToString();
                        ImageButton1.Visible = false;
                        EmployeeCodeBtnimg.Enabled = false;
                        EmployeeCodeBtnimg.Visible = true;
                        EmployeeCode.Enabled = false;
                    }
                    NationalType.Visible = false;
                    ContinentId.Visible = false;
                }
                DropdownMuNonMu1.Enabled = false;
                EmployeeCodeBtnimg1.Enabled = false;

                if (DropDownListPublicationEntry.SelectedValue == "JA" || DropDownListPublicationEntry.SelectedValue == "TS" || DropDownListPublicationEntry.SelectedValue == "PR")
                {
                    DropdownMuNonMu1.Enabled = false;
                    EmployeeCodeBtnimg1.Enabled = true;
                }
                else
                {
                    DropdownMuNonMu1.Enabled = false;
                    EmployeeCodeBtnimg1.Enabled = false;
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
                else if (DropdownMuNonMu.Text == "S")
                {
                    if (DropDownListPublicationEntry.SelectedValue == "JA" || DropDownListPublicationEntry.SelectedValue == "TS" || DropDownListPublicationEntry.SelectedValue == "PR")
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

                Grid_AuthorEntry.Columns[13].Visible = true;


                if (Grid_AuthorEntry.Rows.Count == 1)
                {
                    DropDownList DropdownMuNonMu5 = (DropDownList)Grid_AuthorEntry.Rows[0].Cells[2].FindControl("DropdownMuNonMu");
                    if (DropDownListPublicationEntry.SelectedValue == "JA" || DropDownListPublicationEntry.SelectedValue == "TS" || DropDownListPublicationEntry.SelectedValue == "PR")
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
                    if (i < dtCurrentTable.Rows.Count)
                    {
                        DropDownList DropdownMuNonM8 = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[2].FindControl("DropdownMuNonMu");
                        DropdownMuNonM8.Enabled = false;
                    }


                }

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
                popupPanelJournal.Visible = true;
                //TextBoxPubJournal.ReadOnly = true;
                if (DropDownListPublicationEntry.SelectedValue == "PR")
                {
                    imageBkCbtn.Visible = false;
                    imageBkCbtn.Enabled = false;
                    imageBkCbtn1.Visible = true;
                    imageBkCbtn1.Enabled = true;
                }
                else
                {
                    imageBkCbtn.Visible = true;
                    imageBkCbtn.Enabled = true;
                    imageBkCbtn1.Visible = false;
                    imageBkCbtn1.Enabled = false;

                }
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
                popupPanelJournal.Visible = false;
                CheckboxIndexAgency.Enabled = false;
                popupPanelJournal.Visible = false;
                CheckboxIndexAgency.Enabled = false;
                CheckboxIndexAgency.ClearSelection();
                imageBkCbtn.Visible = false;
                // lblmsgpubnonondexed.Visible = true;
                //TextBoxPubJournal.ReadOnly = false;
            }
        }

        setModalWindow(sender, e);

        BtnFeedback.Enabled = true;
        btnSave.Enabled = true;
        lblmsg.Visible = true;
        GridViewSearch.Visible = true;
        EnableValidation();
    }

    public void fnRecordExistApproval(object sender, EventArgs e)
    {
        lblmsg.Visible = false;
        lblmsg2.Visible = true;
        lblnote1.Visible = false;
        panAddAuthor.Enabled = false;
        panelRemarks.Visible = false;
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

        TextBoxYear.Items.Clear();
        TextBoxYearJA.Items.Clear();

        FileUploadPdf.Visible = true;
        LabelUploadPfd.Visible = true;
        Business obj = new Business();
        PublishData v = new PublishData();
        v = obj.fnfindjid(Pid, TypeEntry);

        StudentPub.Value = v.IsStudentAuthor;
        DropDownListMuCategory.SelectedValue = v.MUCategorization;
        DropDownListPublicationEntry.SelectedValue = TypeEntry;
        DropDownListPublicationEntry.Enabled = false;
        TextBoxPubId.Text = Pid;
        txtboxTitleOfWrkItem.Text = v.TitleWorkItem;


        TextBoxRemarks.Text = v.RemarksFeedback;
        //  v = obj.fnfindjid(Pid, TypeEntry);
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
            else
            {
                txtIFApplicableYear.Text = "";
            }

            //txtCitation.Text = v.CitationUrl;
            // DropDownListPubType.SelectedValue=;
            TextBoxPageFrom.Text = v.PageFrom;
            TextBoxIssue.Text = v.Issue;
            TextBoxPageTo.Text = v.PageTo;
            TextBoxVolume.Text = v.JAVolume;
            RadioButtonListIndexed.SelectedValue = v.Indexed;
            if (v.PageFrom != "")
            {
                TextBoxPageTo.Enabled = true;
            }
            else
            {
                TextBoxPageTo.Enabled = false;
            }
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
            else
            {
                txtIFApplicableYear.Text = "";
            }

            //txtCitation.Text = v.CitationUrl;
            // DropDownListPubType.SelectedValue=;
            TextBoxPageFrom.Text = v.PageFrom;
            TextBoxIssue.Text = v.Issue;
            TextBoxPageTo.Text = v.PageTo;
            TextBoxVolume.Text = v.JAVolume;
            RadioButtonListIndexed.SelectedValue = v.Indexed;
            if (v.PageFrom != "")
            {
                TextBoxPageTo.Enabled = true;
            }
            else
            {
                TextBoxPageTo.Enabled = false;
            }
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
            // DropDownListPubType.SelectedValue=;
            TextBoxPageNum.Text = v.BookPageNum;

            TextBoxVolume1.Text = v.BookVolume;

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

            if (v.Date.ToShortDateString() != "01/01/0001")
            {
                TextBoxDate.Text = v.Date.ToShortDateString();
            }
            if (v.todate.ToShortDateString() != "01/01/0001")
            {
                TextBoxDate1.Text = v.todate.ToShortDateString();
            }
            if (v.TypePresentation != "")
            {
                RadioButtonListTypePresentaion.SelectedValue = v.TypePresentation;
            }
            TextBoxCreditPoint.Text = v.CreditPoint.ToString();
            TextBoxAwardedBy.Text = v.AwardedBy;
            TextBoxIsbn.Text = v.ConfISBN;
            RadioButtonListCPIndexed.SelectedValue = v.Indexed;
            // CheckboxIndexAgency.ClearSelection();
        }
        else if (TypeEntry == "NM")
        {


            panelJournalArticle.Visible = false;
            panelBookPublish.Visible = false;
            panelConfPaper.Visible = false;
            panelOthes.Visible = true;

            TextBoxNewsPublish.Text = v.NewsPublisher;

            if (v.NewsPublishedDate.ToShortDateString() != "01/01/0001")
            {
                TextBoxDateOfNewsPublish.Text = v.NewsPublishedDate.ToShortDateString();
            }

            TextBoxPageNumNewsPaper.Text = v.NewsPageNum;
            lblnote1.Visible = false;

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
            TextBoxIssue.Text = v.Issue;
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
            else
            {
                txtIFApplicableYear.Text = "";
            }

            //txtCitation.Text = v.CitationUrl;
            // DropDownListPubType.SelectedValue=;
            TextBoxPageFrom.Text = v.PageFrom;
            if (v.PageFrom != "")
            {
                TextBoxPageTo.Enabled = true;
            }
            else
            {
                TextBoxPageTo.Enabled = false;
            }
            TextBoxPageTo.Text = v.PageTo;
            TextBoxVolume.Text = v.JAVolume;
            RadioButtonListIndexed.SelectedValue = v.Indexed;

            // CheckboxIndexAgency.ClearSelection();
        }
        panelTechReport.Visible = false;

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
                ImageButton EmployeeCodeBtnimg = (ImageButton)Grid_AuthorEntry.Rows[rowIndex].Cells[2].FindControl("EmployeeCodeBtn");

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

                DropDownList DropdownStudentInstitutionName = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[2].FindControl("DropdownStudentInstitutionName");
                DropDownList DropdownStudentDepartmentName = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[2].FindControl("DropdownStudentDepartmentName");
                DropDownList NationalType = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[2].FindControl("NationalType");
                DropDownList ContinentId = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[2].FindControl("ContinentId");


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
                    ContinentId.Enabled = false;
                    NationalType.Enabled = false;

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
                    ContinentId.Enabled = false;
                    NationalType.Enabled = false;

                    InstNme.Text = dtCurrentTable.Rows[i - 1]["InstitutionName"].ToString();
                    deptname.Text = dtCurrentTable.Rows[i - 1]["DepartmentName"].ToString();
                    InstId.Value = dtCurrentTable.Rows[i - 1]["Institution"].ToString();
                    deptId.Value = dtCurrentTable.Rows[i - 1]["Department"].ToString();
                }
                else if (DropdownMuNonMu.Text == "S")
                {
                    if (DropDownListPublicationEntry.SelectedValue == "JA" || DropDownListPublicationEntry.SelectedValue == "TS")
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
                        DropdownStudentInstitutionName.Text = dtCurrentTable.Rows[i - 1]["Institution"].ToString();
                        DropdownStudentDepartmentName.Text = dtCurrentTable.Rows[i - 1]["Department"].ToString();
                        EmployeeCode.Enabled = false;
                    }
                    NationalType.Visible = false;
                    ContinentId.Visible = false;


                    //DropdownStudentInstitutionName.Text = dtCurrentTable.Rows[i - 1]["Institution"].ToString();
                    //DropdownStudentDepartmentName.Text = dtCurrentTable.Rows[i - 1]["Department"].ToString();
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
                    AuthorName.Enabled = false;
                    //ImageButton1.Visible = false;
                    EmployeeCodeBtnimg.Enabled = false;
                    EmployeeCodeBtnimg.Visible = true;
                    EmployeeCode.Enabled = true;
                    NationalType.Visible = false;
                    ContinentId.Visible = false;
                }

                DropdownStudentInstitutionName.Enabled = false;
                DropdownStudentDepartmentName.Enabled = false;
                EmployeeCodeBtnimg.Enabled = false;
                DropdownMuNonMu.Enabled = false;
                BtnAddMU.Enabled = false;
                EmployeeCode.Enabled = false;
                DropdownMuNonMu1.Enabled = false;
                EmployeeCodeBtnimg1.Enabled = false;

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
                }

                Grid_AuthorEntry.Columns[15].Visible = false;

                rowIndex++;

            }


            ViewState["CurrentTable"] = dtCurrentTable;
        }
        CheckboxIndexAgency.DataBind();
        SqlDataSourceCheckboxIndexAgency.DataBind();
        if (RadioButtonListIndexed.SelectedValue == "Y")
        {
            popupPanelJournal.Visible = true;
            // TextBoxPubJournal.ReadOnly = true;
            imageBkCbtn.Visible = true;
            imageBkCbtn.Enabled = true;
            CheckboxIndexAgency.Enabled = true;
            //TextBoxPubJournal.ReadOnly = true;
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
            popupPanelJournal.Visible = false;
            CheckboxIndexAgency.Enabled = false;
            popupPanelJournal.Visible = false;
            popupPanelProceedingsJournal.Visible = false;
            CheckboxIndexAgency.Enabled = false;
            CheckboxIndexAgency.ClearSelection();
            imageBkCbtn.Visible = false;
            // lblmsgpubnonondexed.Visible = true;
            //TextBoxPubJournal.ReadOnly = true;

            CheckboxIndexAgency.Enabled = false;
        }

        setModalWindow(sender, e);
        //popup.Visible = false;
        BtnFeedback.Enabled = true;
        btnSave.Enabled = false;
        EnableValidation();

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
                    // rowindex3++;
                    continue;
                }

            }
        }
        else
        {
            StudentPub.Value = "N";
        }
    }

    protected void NationalTypeOnSelectedIndexChanged(object sender, EventArgs e)
    {
        PubEntriesUpdatePanel.Update();
        EditUpdatePanel.Update();
        //popupPanelAffilUpdate.Update();
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


    // bind branch popup grid on text change
    protected void exit(object sender, EventArgs e)
    {
        PubEntriesUpdatePanel.Update();
        EditUpdatePanel.Update();
        //popupPanelAffilUpdate.Update();
        MainUpdate.Update();
        affiliateSrch.Text = "";
        popGridAffil.DataBind();
    }
    // bind branch popup grid on text change
    protected void branchNameChanged(object sender, EventArgs e)
    {
        PubEntriesUpdatePanel.Update();
        EditUpdatePanel.Update();
        UpdatePanel2.Update();
        //popupPanelAffilUpdate.Update();
        MainUpdate.Update();
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
            //popupstudent.Style.Add("display", "none");
        }
        else if (munonmu.SelectedValue == "S")
        {
            popupPanelAffil.Style.Add("display", "none");
            //popupstudent.Style.Add("display", "true");
        }
        //else if (munonmu.SelectedValue == "O")
        //{
        //    popupPanelAffil.Style.Add("display", "none");
        //    popupstudent.Style.Add("display", "none");
        //}
        //ModalPopupExtender ModalPopupExtender8 = (ModalPopupExtender)Grid_AuthorEntry.Rows[row].FindControl("ModalPopupExtender4");
        //ModalPopupExtender8.Show();

    }

    protected void JournalCodePopChanged(object sender, EventArgs e)
    {
        string year = TextBoxYearJA.SelectedValue;
        if (journalcodeSrch.Text.Trim() == "" && journalcodeISSNSrch.Text.Trim() == "")
        {
            SqlDataSourceJournal.SelectParameters.Clear();
            SqlDataSourceJournal.SelectParameters.Add("Year", year);

            SqlDataSourceJournal.SelectCommand = "SELECT top 10 j.Id,j.Title,j.AbbreviatedTitle FROM [Journal_M] j,[Journal_Year_Map] m where j.Id=m.Id and m.Year=@Year";
            popGridJournal.DataBind();
            popGridJournal.Visible = true;
        }
        else if (journalcodeSrch.Text.Trim() != "" && journalcodeISSNSrch.Text.Trim() == "")
        {
            SqlDataSourceJournal.SelectParameters.Clear();
            SqlDataSourceJournal.SelectParameters.Add("Year", year);
            SqlDataSourceJournal.SelectParameters.Add("Title", journalcodeSrch.Text);

            SqlDataSourceJournal.SelectCommand = "SELECT top 10 j.Id,j.Title,j.AbbreviatedTitle FROM [Journal_M] j,[Journal_Year_Map] m where j.Id=m.Id and m.Year=@Year and Title like '%' + @Title + '%'  ";
            popGridJournal.DataBind();
            popGridJournal.Visible = true;
        }
        else if (journalcodeSrch.Text.Trim() == "" && journalcodeISSNSrch.Text.Trim() != "")
        {
            SqlDataSourceJournal.SelectParameters.Clear();
            SqlDataSourceJournal.SelectParameters.Add("Year", year);
            SqlDataSourceJournal.SelectParameters.Add("id", journalcodeISSNSrch.Text);

            SqlDataSourceJournal.SelectCommand = "SELECT top 10 j.Id,j.Title,j.AbbreviatedTitle FROM [Journal_M] j,[Journal_Year_Map] m where j.Id=m.Id and m.Year=@Year and  j.Id like '%' + @id + '%' ";
            popGridJournal.DataBind();
            popGridJournal.Visible = true;
        }
        else
        {
            SqlDataSourceJournal.SelectParameters.Clear();
            SqlDataSourceJournal.SelectParameters.Add("Title", journalcodeSrch.Text);
            SqlDataSourceJournal.SelectParameters.Add("id", journalcodeISSNSrch.Text);

            SqlDataSourceJournal.SelectCommand = "SELECT  Id,Title,AbbreviatedTitle FROM [Journal_M] where Title like '%' + @Title + '%' and Id like '%' + @id + '%' ";
            popGridJournal.DataBind();
            popGridJournal.Visible = true;
        }

        //ModalPopupExtender1.Show();
    }
    protected void ProceedingCodePopChanged(object sender, EventArgs e)
    {
        string year = TextBoxYearJA.SelectedValue;
        if (journalcodeSrchproceeding.Text.Trim() == "" && journalcodeISSNSrchproceeding.Text.Trim() == "")
        {
            SqlDataSourceProceedings.SelectParameters.Clear();
            SqlDataSourceProceedings.SelectParameters.Add("Year", year);

            SqlDataSourceProceedings.SelectCommand = "SELECT top 10 j.ID,j.Title,j.AbbreviatedTitle FROM [Proceedings_M] j,[Proceedings_Year_Map] m where j.ID=m.ISSN and m.Year=@year";
            popGridJournalProceedings.DataBind();
            popGridJournalProceedings.Visible = true;
        }
        else if (journalcodeSrchproceeding.Text.Trim() != "" && journalcodeISSNSrchproceeding.Text.Trim() == "")
        {
            SqlDataSourceProceedings.SelectParameters.Clear();
            SqlDataSourceProceedings.SelectParameters.Add("Title", journalcodeSrchproceeding.Text);
            SqlDataSourceProceedings.SelectParameters.Add("Year", year);

            SqlDataSourceProceedings.SelectCommand = "SELECT top 10 j.ID,j.Title,j.AbbreviatedTitle FROM [Proceedings_M] j,[Proceedings_Year_Map] m where j.ID=m.ISSN and m.Year=@Year and Title like '%' + @Title + '%'  ";
            popGridJournalProceedings.DataBind();
            popGridJournalProceedings.Visible = true;
        }
        else if (journalcodeSrchproceeding.Text.Trim() == "" && journalcodeISSNSrchproceeding.Text.Trim() != "")
        {
            SqlDataSourceProceedings.SelectParameters.Clear();
            SqlDataSourceProceedings.SelectParameters.Add("Year", year);
            SqlDataSourceProceedings.SelectParameters.Add("ID", journalcodeISSNSrchproceeding.Text);

            SqlDataSourceProceedings.SelectCommand = "SELECT top 10 j.ID,j.Title,j.AbbreviatedTitle FROM [Proceedings_M] j,[Proceedings_Year_Map] m where j.ID=m.ISSN and m.Year=@Year and  j.ID like '%' + @ID + '%' ";
            popGridJournalProceedings.DataBind();
            popGridJournalProceedings.Visible = true;
        }
        else
        {
            SqlDataSourceProceedings.SelectParameters.Clear();
            SqlDataSourceProceedings.SelectParameters.Add("Title", journalcodeSrch.Text);
            SqlDataSourceProceedings.SelectParameters.Add("ID", journalcodeISSNSrchproceeding.Text);

            SqlDataSourceProceedings.SelectCommand = "SELECT  ID,Title,AbbreviatedTitle FROM [Proceedings_M] where Title like '%' + @Title + '%' and ID like '%' + @ID + '%' ";
            popGridJournalProceedings.DataBind();
            popGridJournalProceedings.Visible = true;
        }

        //ModalPopupExtender3.Show();
    }

    protected void exit1(object sender, EventArgs e)
    {

        journalcodeSrch.Text = "";
        popGridJournal.DataBind();
        //ModalPopupExtender1.Hide();

    }
    protected void exit2(object sender, EventArgs e)
    {

        journalcodeSrch.Text = "";
        popGridJournalProceedings.DataBind();
        //ModalPopupExtender3.Hide();

    }
    private void EnableValidation()
    {
        RequiredFieldValidator24.Enabled = true;
        if (DropDownListMuCategory.SelectedValue != "LE")
        {

            if (DropDownListPublicationEntry.SelectedValue == "JA" || DropDownListPublicationEntry.SelectedValue == "TS" || DropDownListPublicationEntry.SelectedValue == "PR")
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
        if (DropDownListPublicationEntry.SelectedValue == "JA" || DropDownListPublicationEntry.SelectedValue == "TS" || DropDownListPublicationEntry.SelectedValue == "PR")
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
        if (DropDownListPublicationEntry.SelectedValue == "BK")
        {
            RequiredFieldValidator8.Enabled = true;
            RequiredFieldValidator9.Enabled = true;
            RequiredFieldValidator10.Enabled = true;
            RequiredFieldValidator11.Enabled = true;
            RequiredFieldValidator12.Enabled = true;
            RequiredFieldValidator13.Enabled = true;
        }
        else
        {
            RequiredFieldValidator8.Enabled = false;
            RequiredFieldValidator9.Enabled = false;
            RequiredFieldValidator10.Enabled = false;
            RequiredFieldValidator11.Enabled = false;
            RequiredFieldValidator12.Enabled = false;
            RequiredFieldValidator13.Enabled = false;
        }
        if (DropDownListPublicationEntry.SelectedValue == "CP")
        {
            RequiredFieldValidator14.Enabled = true;
            RequiredFieldValidator15.Enabled = true;
            RequiredFieldValidator16.Enabled = true;
            RequiredFieldValidator17.Enabled = true;
        }
        else
        {
            RequiredFieldValidator14.Enabled = false;
            RequiredFieldValidator15.Enabled = false;
            RequiredFieldValidator16.Enabled = false;
            RequiredFieldValidator17.Enabled = false;
        }
        if (DropDownListPublicationEntry.SelectedValue == "NM")
        {
            RequiredFieldValidator18.Enabled = true;
            RequiredFieldValidator19.Enabled = true;
        }
        else
        {
            RequiredFieldValidator18.Enabled = false;
            RequiredFieldValidator19.Enabled = false;
        }
    }
    // bind branch popup grid on text change
    protected void SearchStudentData(object sender, EventArgs e)
    {
        StudentSQLDS.SelectParameters.Clear();
        if (txtSrchStudentName.Text.Trim() == "" && txtSrchStudentRollNo.Text.Trim() == "" && StudentIntddl.SelectedValue == "")
        {
             StudentSQLDS.SelectCommand = "Select TOP 10  RollNo,Name,SISClass.ClassName as ClassName,SISInstitution.InstName as InstName,EmailID1,SISStudentGenInfo.ClassCode as ClassCode,SISInstnHR.HRInstitute as InstID from SISStudentGenInfo,SISClass,SISInstitution,SISInstnHR  where SISStudentGenInfo.ClassCode=SISClass.ClassCode and SISStudentGenInfo.InstID=SISInstitution.InstID and SISInstnHR.Institute_Id=SISInstitution.InstID and SISInstnHR.Institute_Id=SISStudentGenInfo.InstID";
           
        }
            //here aug 02
        else if ((txtSrchStudentName.Text.Trim() != "" && txtSrchStudentRollNo.Text.Trim() == "") && StudentIntddl.SelectedValue == "")
        {
            StudentSQLDS.SelectParameters.Add("txtSrchStudentName", txtSrchStudentName.Text.Trim());
            StudentSQLDS.SelectCommand = "Select TOP 10  RollNo,Name,SISClass.ClassName as ClassName,SISInstitution.InstName as InstName,EmailID1 ,SISStudentGenInfo.ClassCode as ClassCode,SISInstnHR.HRInstitute as InstID from SISStudentGenInfo,SISClass,SISInstitution,SISInstnHR  where SISStudentGenInfo.ClassCode=SISClass.ClassCode and SISStudentGenInfo.InstID=SISInstitution.InstID and SISInstnHR.Institute_Id=SISInstitution.InstID and SISInstnHR.Institute_Id=SISStudentGenInfo.InstID and  Name like '%' + @txtSrchStudentName + '%'";

        }
        else if ((txtSrchStudentName.Text.Trim() == "" && txtSrchStudentRollNo.Text.Trim() != "") && StudentIntddl.SelectedValue == "")
        {
            StudentSQLDS.SelectParameters.Add("txtSrchStudentRollNo", txtSrchStudentRollNo.Text.Trim());
            StudentSQLDS.SelectCommand = "Select TOP 10  RollNo,Name,SISClass.ClassName as ClassName,SISInstitution.InstName as InstName,EmailID1 ,SISStudentGenInfo.ClassCode as ClassCode,SISInstnHR.HRInstitute as InstID from SISStudentGenInfo,SISClass,SISInstitution,SISInstnHR  where SISStudentGenInfo.ClassCode=SISClass.ClassCode and SISStudentGenInfo.InstID=SISInstitution.InstID and SISInstnHR.Institute_Id=SISInstitution.InstID and SISInstnHR.Institute_Id=SISStudentGenInfo.InstID and  RollNo like '%' + @txtSrchStudentRollNo+ '%'";


        }
        else if ((txtSrchStudentName.Text.Trim() == "" && txtSrchStudentRollNo.Text.Trim() == "") && StudentIntddl.SelectedValue != "")
        {
            StudentSQLDS.SelectParameters.Add("StudentIntddl", StudentIntddl.SelectedValue);
            StudentSQLDS.SelectCommand = "Select TOP 10  RollNo,Name,SISClass.ClassName as ClassName,SISInstitution.InstName as InstName,EmailID1 ,SISStudentGenInfo.ClassCode as ClassCode,SISInstnHR.HRInstitute as InstID from SISStudentGenInfo,SISClass,SISInstitution,SISInstnHR  where SISStudentGenInfo.ClassCode=SISClass.ClassCode and SISStudentGenInfo.InstID=SISInstitution.InstID and SISInstnHR.Institute_Id=SISInstitution.InstID and SISInstnHR.Institute_Id=SISStudentGenInfo.InstID and   (SISStudentGenInfo.InstID=@StudentIntddl)";
        }
        else if ((txtSrchStudentName.Text.Trim() == "" && txtSrchStudentRollNo.Text.Trim() != "") && StudentIntddl.SelectedValue != "")
        {

            StudentSQLDS.SelectParameters.Add("txtSrchStudentRollNo", txtSrchStudentRollNo.Text.Trim());
            StudentSQLDS.SelectParameters.Add("StudentIntddl", StudentIntddl.SelectedValue);
            StudentSQLDS.SelectCommand = "Select TOP 10  RollNo,Name,SISClass.ClassName as ClassName,SISInstitution.InstName as InstName,EmailID1 ,SISStudentGenInfo.ClassCode as ClassCode,SISInstnHR.HRInstitute as InstID from SISStudentGenInfo,SISClass,SISInstitution,SISInstnHR  where SISStudentGenInfo.ClassCode=SISClass.ClassCode and SISStudentGenInfo.InstID=SISInstitution.InstID and SISInstnHR.Institute_Id=SISInstitution.InstID and SISInstnHR.Institute_Id=SISStudentGenInfo.InstID and RollNo like '%' + @txtSrchStudentRollNo+ '%' and (SISStudentGenInfo.InstID=@StudentIntddl)";
        }
        else if ((txtSrchStudentName.Text.Trim() != "" && txtSrchStudentRollNo.Text.Trim() == "") && StudentIntddl.SelectedValue != "")
        {

            StudentSQLDS.SelectParameters.Add("txtSrchStudentName", txtSrchStudentName.Text.Trim());
            StudentSQLDS.SelectParameters.Add("StudentIntddl", StudentIntddl.SelectedValue);
            StudentSQLDS.SelectCommand = "Select TOP 10  RollNo,Name,SISClass.ClassName as ClassName,SISInstitution.InstName as InstName,EmailID1 ,SISStudentGenInfo.ClassCode as ClassCode,SISInstnHR.HRInstitute as InstID from SISStudentGenInfo,SISClass,SISInstitution,SISInstnHR  where SISStudentGenInfo.ClassCode=SISClass.ClassCode and SISStudentGenInfo.InstID=SISInstitution.InstID and SISInstnHR.Institute_Id=SISInstitution.InstID and SISInstnHR.Institute_Id=SISStudentGenInfo.InstID and Name like '%' + @txtSrchStudentName + '%' and (SISStudentGenInfo.InstID=@StudentIntddl)";
        }




//ends


        else if ((txtSrchStudentName.Text.Trim() != "" || txtSrchStudentRollNo.Text.Trim() != "") && StudentIntddl.SelectedValue == "")
        {

            StudentSQLDS.SelectParameters.Add("Name", txtSrchStudentName.Text);
            StudentSQLDS.SelectParameters.Add("RollNo", txtSrchStudentRollNo.Text);

            StudentSQLDS.SelectCommand = "Select TOP 10  RollNo,Name,SISClass.ClassName as ClassName,SISInstitution.InstName as InstName,EmailID1 ,SISStudentGenInfo.ClassCode as ClassCode,SISInstnHR.HRInstitute as InstID from SISStudentGenInfo,SISClass,SISInstitution,SISInstnHR  where SISStudentGenInfo.ClassCode=SISClass.ClassCode and SISStudentGenInfo.InstID=SISInstitution.InstID and SISInstnHR.Institute_Id=SISInstitution.InstID and SISInstnHR.Institute_Id=SISStudentGenInfo.InstID and  Name like '%' + @Name + '%' and RollNo like '%' + @RollNo + '%'";

        }
        else
        {

            StudentSQLDS.SelectParameters.Add("Name", txtSrchStudentName.Text);
            StudentSQLDS.SelectParameters.Add("RollNo", txtSrchStudentRollNo.Text);
            StudentSQLDS.SelectParameters.Add("InstID", StudentIntddl.SelectedValue);


            StudentSQLDS.SelectCommand = "Select TOP 10  RollNo,Name,SISClass.ClassName as ClassName,SISInstitution.InstName as InstName,EmailID1 ,SISStudentGenInfo.ClassCode as ClassCode,SISInstnHR.HRInstitute as InstID from SISStudentGenInfo,SISClass,SISInstitution,SISInstnHR  where SISStudentGenInfo.ClassCode=SISClass.ClassCode and SISStudentGenInfo.InstID=SISInstitution.InstID and SISInstnHR.Institute_Id=SISInstitution.InstID and SISInstnHR.Institute_Id=SISStudentGenInfo.InstID and  (Name like '%' + @Name + '%' and RollNo like '%' + @RollNo + '%' and (SISStudentGenInfo.InstID=@InstID) ) ";

        }
        popupStudentGrid.DataBind();
        popupStudentGrid.Visible = true;



        // string rowVal = Request.Form["rowIndx"];
        string a = rowVal.Value;
        int rowIndex = Convert.ToInt32(a);
        DropDownList munonmu = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].FindControl("DropdownMuNonMu");
        if (munonmu.SelectedValue == "M")
        {
            popupPanelAffil.Style.Add("display", "true");
            //popupstudent.Style.Add("display", "none");
        }
        else if (munonmu.SelectedValue == "S")
        {
            popupstudent.Visible = true;
            popupstudent.Style.Add("display", "true");
            //popupPanelAffil.Style.Add("display", "none");
        }
        //ModalPopupExtender ModalPopupExtender8 = (ModalPopupExtender)Grid_AuthorEntry.Rows[rowIndex].FindControl("ModalPopupExtender2");
        //ModalPopupExtender8.Show();
    }

    protected void StudentDataSelect(Object sender, EventArgs e)
    {
        MainUpdate.Update();
        UpdatePanel3.Update();
        popupStudentGrid.Visible = true;
        EditUpdatePanel.Update();
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
        Department.Value = classcode.Value;
        popupStudentGrid.DataBind();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "CallMyFunction", "ToggleDisplay5()", true);
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
            RequiredFieldValidator1.Enabled = false;
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
    private EmailDetails SendMail(string publicatinid, string typeofentry, string autoapproval, string isrevert, string studentauthor)
    {
        log.Debug("Publication Entry: Inside Send Mail function of Entry Type : " + typeofentry + " ID " + publicatinid);
        EmailDetails details = new EmailDetails();
        details.EmailSubject = "Publication Entry <  " + DropDownListPublicationEntry.SelectedValue + " _ " + TextBoxPubId.Text + "  > Approved ";
        details.Module = "PAPP";
        try
        {

            details.FromEmail = ConfigurationManager.AppSettings["FromAddress"].ToString();

            details.Id = publicatinid;
            details.Type = typeofentry;

            ArrayList authoremailidlist = new ArrayList();
            ArrayList supervisoremailidlist = new ArrayList();
            ArrayList authorname = new ArrayList();
            ArrayList studentlist = new ArrayList();
            ArrayList authoremailidlistDetail = new ArrayList();

            DataSet student = new DataSet();
            DataSet ds = new DataSet();
            DataSet dsIN = new DataSet();

            Business bus = new Business();
            Business e = new Business();

            int result;

            ds = bus.getAuthorList(TextBoxPubId.Text, DropDownListPublicationEntry.SelectedValue);
            dsIN = bus.getAuthorDetail(TextBoxPubId.Text, DropDownListPublicationEntry.SelectedValue);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                authoremailidlist.Add(ds.Tables[0].Rows[i]["EmailId"].ToString());

                if (authoremailidlist[i].ToString() == "")
                {
                    int j = i;
                    if ((j == i) && (j < dsIN.Tables[0].Rows.Count))
                    {
                        authoremailidlistDetail.Add(dsIN.Tables[0].Rows[j]["AuthorName"].ToString());
                    }

                }
            }

            for (int k = 0; k < authoremailidlistDetail.Count; k++)
            {
                string AuthorName = authoremailidlistDetail[k].ToString();

                result = e.insertAuthorDetailEmailtracker(AuthorName, details, TextBoxPubId.Text);
            }

            string Supervisor = null;
            string Supervisormail = null;
            string Supervisormailid = null;

            if (autoapproval == "N")
            {
                for (int i = 0; i < authoremailidlist.Count; i++)
                {
                    string author = authoremailidlist[i].ToString();

                    string[] author1;
                    author1 = author.Split(new[] { "@" }, StringSplitOptions.RemoveEmptyEntries);
                    // string[] author1=author.Split(' ');
                    if (author != "")
                    {
                        Supervisor = bus.GetAuthorsSupervisor(author1[0]);

                        string inst = Session["InstituteId"].ToString();
                        string dept = Session["Department"].ToString();
                        string uid = Session["UserId"].ToString();
                        Supervisormail = bus.GetSupId(inst, uid, dept);

                        Supervisormailid = bus.GetAuthorsSupervisorgetMail(Supervisormail);
                        if (Supervisormailid != null)
                        {
                            if (!supervisoremailidlist.Contains(Supervisormailid))
                            {
                                supervisoremailidlist.Add(Supervisormailid);
                            }

                        }
                    }
                }
                details.Module = "PSUB";
            }
            else
            {
                details.Module = "PAPP";
            }
            if (studentauthor == "Y")
            {
                if (DropDownListPublicationEntry.SelectedValue == "JA" || DropDownListPublicationEntry.SelectedValue == "TS")
                {
                    student = bus.getStudentlist(TextBoxPubId.Text, DropDownListPublicationEntry.SelectedValue);
                }
                for (int i = 0; i < student.Tables[0].Rows.Count; i++)
                {
                    studentlist.Add(student.Tables[0].Rows[i]["EmailId"].ToString());
                }
            }

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

            string dep1 = v.nDepartment;
            string inst1 = v.nInstitution;

            v = bs.selectIndexAgency2(dep1, inst1);
            string mailid = v.emailid;
            details.CCEmail = mailid;


            for (int i = 0; i < authoremailidlist.Count; i++)
            {
                //if (details.ToEmail != null)
                //{
                //    details.ToEmail =details.ToEmail+','+ authoremailidlist[i].ToString();
                //}
                //else
                //{
                //    if (i == 0)
                //    {
                //        details.ToEmail = authoremailidlist[i].ToString();
                //    }
                //    else
                //    {
                //        details.ToEmail = details.ToEmail + ',' + authoremailidlist[i].ToString();
                //    }
                //}
                string email = authoremailidlist[i].ToString();
                if (details.ToEmail != null)
                {
                    if (authoremailidlist[i].ToString() != "")
                    {
                        details.ToEmail = details.ToEmail + ',' + authoremailidlist[i].ToString();
                    }

                }
                else
                {
                    if (i == 0)
                    {
                        if (authoremailidlist[i].ToString() != "")
                        {
                            details.ToEmail = authoremailidlist[i].ToString();
                        }
                    }
                    else
                    {
                        if (authoremailidlist[i].ToString() != "")
                        {
                            if (details.ToEmail == null)
                            {
                                details.ToEmail = authoremailidlist[i].ToString();
                            }
                            else
                            {
                                details.ToEmail = details.ToEmail + ',' + authoremailidlist[i].ToString();
                            }
                        }
                    }
                    //details.CCEmail = details.CCEmail + ',' + email;
                }
                log.Info(" Email will be sent to authors '" + i + "' : '" + authoremailidlist[i] + "' ");
            }
            if (studentauthor == "Y")
            {
                if (DropDownListPublicationEntry.SelectedValue == "JA" || DropDownListPublicationEntry.SelectedValue == "TS")
                {
                    for (int i = 0; i < studentlist.Count; i++)
                    {

                        if (details.ToEmail != null)
                        {
                            details.ToEmail = details.ToEmail + ',' + studentlist[i].ToString();
                        }
                        else
                        {
                            if (i == 0)
                            {
                                details.ToEmail = studentlist[i].ToString();
                            }
                            else
                            {
                                details.ToEmail = details.ToEmail + ',' + studentlist[i].ToString();
                            }
                        }

                        //details.ToEmail = email;
                        log.Info(" Email will be sent to student authors '" + i + "' : '" + studentlist[i] + "' ");
                    }
                }
            }
            for (int i = 0; i < supervisoremailidlist.Count; i++)
            {
                if (details.ToEmail != null)
                {
                    details.ToEmail = details.ToEmail + ',' + supervisoremailidlist[i].ToString();
                }
                else
                {
                    if (i == 0)
                    {
                        details.ToEmail = supervisoremailidlist[i].ToString();
                    }
                    else
                    {
                        details.ToEmail = details.ToEmail + ',' + supervisoremailidlist[i].ToString();
                    }
                }
                //details.ToEmail = email;
                log.Info(" Email will be sent to supervisors '" + i + "' : '" + supervisoremailidlist[i] + "' ");
            }
            return details;
        }

        catch (Exception ex)
        {
            log.Error(ex.StackTrace);
            log.Error(ex.Message);
            return details;
        }
    }
    protected void Buttonupload_Click(object sender, EventArgs e)
    {
        if (!Page.IsValid)
        {
            return;
        }
        try
        {
            UpdatePanel1.Update();
            MainUpdate.Update();
            Business b = new Business();
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
            ArrayList listIndexAgency = new ArrayList();
            Business B = new Business();
            PublishData j = new PublishData();
            PublishData[] JD = new PublishData[dtCurrentTable.Rows.Count];
            string FileUpload = "";
            string PublicationEntry1 = DropDownListPublicationEntry.SelectedValue;
            int savedfileflag = 0;
            string filelocationpath = "";
            string UploadPdf1 = "";
            int rowIndex = 0;
            //DropDownList DropdownMuNonMu = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[2].FindControl("DropdownMuNonMu");
            //TextBox EmployeeCode = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("EmployeeCode");
            //if (DropdownMuNonMu.Text == "S" || DropdownMuNonMu.Text == "O")
            //{
            //    EmployeeCode.Text = "";
            //}
            //TextBoxPubId.Text = Session["Pubseed"].ToString();

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
                    //string path_BoxId = Path.Combine(mainpath, TextBoxPubId.Text); //main path + location
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
                        //j.PaublicationID = TextBoxPubId.Text.Trim();
                        if (FileUpload == "")
                        {
                            j.FilePath = filelocationpath;
                        }
                        else
                        {
                            j.FilePath = j.FilePathNew;
                        }

                        int result1 = B.UpdatePfPathCreate(j);

                        //btnSave.Enabled = false;
                        Business b1 = new Business();
                        FileUpload = b1.GetFileUploadPath(j.PaublicationID, j.TypeOfEntry);
                        if (FileUpload != "")
                        {
                            GVViewFile.Visible = true;
                        }
                        else
                        {
                            GVViewFile.Visible = false;
                        }

                        DSforgridview.SelectParameters.Clear();
                        DSforgridview.SelectParameters.Add("PaublicationID", j.PaublicationID);
                        DSforgridview.SelectParameters.Add("TypeOfEntry", j.TypeOfEntry);
                        DSforgridview.SelectCommand = "select UploadPDFPath from Publication where PublicationID=@PaublicationID  and TypeOfEntry=@TypeOfEntry";
                        DSforgridview.DataBind();
                        GVViewFile.DataBind();
                        //popup.Visible = false;
                        //popupPanelJournal.Visible = false;
                        //popupPanelProceedingsJournal.Visible = false;
                        //popupPanelProject.Visible = false;
                        string CloseWindow = "alert('File Uploaded successfully..of Entry Type: " + DropDownListPublicationEntry.SelectedValue + " of ID: " + TextBoxPubId.Text + "')";
                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow, true);
                        // ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);

                        //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Publication data Created Successfully..of Entry Type: " + DropDownListPublicationEntry.SelectedValue + " of ID: " + TextBoxPubId.Text + " For update Click on search and edit  !')</script>");
                        log.Info("File Uploaded successfully,  of Entry Type: " + DropDownListPublicationEntry.SelectedValue + " of ID: " + TextBoxPubId.Text);
                       
                    }
                }


                else
                {

                    string CloseWindow1 = "alert('Invalid File!!!File exceeds 10MB..Please try to upload the file less than or equal to 10MB!!!!!!')";
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow1, true);
                    //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Invalid File!!!File exceeds 10MB..Please try to upload the file less than or equal to 10MB!!!!!!')</script>");
                    log.Error("Invalid File!!!File exceeds 10MB!!! : " + TextBoxPubId.Text.Trim() + " , Project Type : " + DropDownListPublicationEntry.SelectedValue);
                    return;
                }

            }
            else
            {
                string CloseWindow1 = "alert('please choose the file ')";
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow1, true);
                return;
            }



        }
        catch (Exception ex)
        {
            log.Error("Inside Catch Block Of Publication-file uplaod" + ex.Message + " UserID : " + Session["UserId"].ToString());

            log.Error(ex.StackTrace);

            ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Error!!!!!!!!!!!!')</script>");

        }

    }
    protected void Button7_Click(object sender, EventArgs e)
    {
        UpdatePanel6.Update();
        MainUpdate.Update();
        PubEntriesUpdatePanel.Update();
        ArrayList SAmount = new ArrayList();
        for (int i = 0; i < GridViewProject.Rows.Count; i++)
        {
            CheckBox Checkboxfaculty = (CheckBox)GridViewProject.Rows[i].FindControl("csSelect");
            Label BudgetId = (Label)GridViewProject.Rows[i].FindControl("TextBoxProjectId");
            Label Amount = (Label)GridViewProject.Rows[i].FindControl("TextBoxProjectUnit");

            if (Checkboxfaculty.Checked == true)
            {
                string id = Amount.Text + BudgetId.Text;
                SAmount.Add(id);
                //string amount = Amount.Text;
            }

        }
        if (SAmount.Count == 0)
        {
            string CloseWindow = "alert('Please select the Project!')";
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow, true);
            return;
        }
        string amountf = "";
        for (int j = 0; j < SAmount.Count; j++)
        {
            if (j == 0)
            {
                amountf = SAmount[j].ToString();
            }
            else
            {
                amountf = amountf + ',' + SAmount[j].ToString();
            }
            TextBoxProjectDetails.Text = amountf;
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "CallMyFunction", "ToggleDisplay4()", true);
    }
    protected void DropDownListhasProjectreference_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownListhasProjectreference.SelectedValue == "Y")
        {

            LabelProjectDetails.Visible = true;
            TextBoxProjectDetails.Visible = true;
            ImageButtonProject.Visible = true;
            PubEntriesUpdatePanel.Update();
            MainUpdate.Update();
            popupPanelProject.Visible = true;
            GridViewProject.Visible = true;
            //SqlDataSourceProject.SelectCommand = "select Project.ID,Project.ProjectUnit,Title from Project inner join Projectnvestigator on  Project.ID=Projectnvestigator.ID and Project.ProjectUnit=Projectnvestigator.ProjectUnit and Projectnvestigator.EmployeeCode='" + Session["UserId"].ToString() + "' and (ProjectStatus='SAN' or ProjectStatus='CLO')";
            GridViewProject.DataSourceID = "SqlDataSourceProject";
            SqlDataSourceProject.DataBind();
            GridViewProject.DataBind();
        }
        else
        {
            PubEntriesUpdatePanel.Update();
            MainUpdate.Update();
            PubEntriesUpdatePanel.Update();
            MainUpdate.Update();
            popupPanelProject.Visible = false;
            GridViewProject.Visible = false;
            //SqlDataSourceProject.SelectCommand = "select Project.ID,Project.ProjectUnit,Title from Project inner join Projectnvestigator on  Project.ID=Projectnvestigator.ID and Project.ProjectUnit=Projectnvestigator.ProjectUnit and Projectnvestigator.EmployeeCode='" + Session["UserId"].ToString() + "' and (ProjectStatus='SAN' or ProjectStatus='CLO')";
            GridViewProject.DataSourceID = "SqlDataSourceProject";
            SqlDataSourceProject.DataBind();
            GridViewProject.DataBind();
            LabelProjectReference.Visible = true;
            LabelProjectRef.Visible = true;
            LabelhasProjectreferenceNote.Visible = true;

            DropDownListhasProjectreference.Visible = true;
            //DropDownListhasProjectreference.SelectedValue = "N";
            LabelProjectDetails.Visible = false;
            TextBoxProjectDetails.Visible = false;
            ImageButtonProject.Visible = false;
        }
    }

    protected void GridViewProject_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        GridViewProject.PageIndex = e.NewPageIndex;
        //SqlDataSourceProject.SelectCommand = "select Project.ID,Project.ProjectUnit,Title from Project inner join Projectnvestigator on  Project.ID=Projectnvestigator.ID and Project.ProjectUnit=Projectnvestigator.ProjectUnit and Projectnvestigator.EmployeeCode='" + Session["UserId"].ToString() + "' and (ProjectStatus='SAN' or ProjectStatus='CLO')";
        GridViewProject.DataSourceID = "SqlDataSourceProject";
        SqlDataSourceProject.DataBind();
        GridViewProject.DataBind();       
        popupPanelProject.Visible = true;
        GridViewProject.Visible = true;
        //ModalPopupExtender5.Show();

    }

    protected void GridViewProject_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
           
            GridView grid = (GridView)sender;

            Label ProjectId = (Label)e.Row.FindControl("TextBoxProjectId");
            CheckBox nextDay = (CheckBox)e.Row.FindControl("csSelect");
            if (ProjectId.Text != "" && ProjectId.Text != null)
            {
                nextDay.Visible = true;
            }
            else
            {
                Page.Form.Attributes.Add("enctype", "multipart/form-data");
                nextDay.Visible = false;
            }
            
        }
    }
    protected void Redirect(Object sender, EventArgs e)
    {
        PubEntriesUpdatePanel.Update();
        EditUpdatePanel.Update();
        UpdatePanel6.Update();
        //UpdatePanel7.Update();
        MainUpdate.Update();
        //LinkButton lb = (LinkButton)sender;
        Button lb = (Button)sender;
        GridViewRow row = (GridViewRow)lb.NamingContainer;
        int index = row.RowIndex; //gets the row index selected      
        var lblProjectID = row.FindControl("TextBoxProjectId") as Label;
        var lblProjectUnit = row.FindControl("TextBoxProjectUnit") as Label;
        string id = lblProjectID.Text;
        string projectunit = lblProjectUnit.Text;
       
        
        setModalWindow7(sender, e);
        //ScriptManager.RegisterStartupScript(this, this.GetType(), "CallMyFunction", "ToggleDisplay4()", true);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "CallMyFunction", "callthis6()", true);
        return;
    }

    private void setModalWindow7(object sender, EventArgs e)
    {
        //GridViewProject.Visible = false;
        //popupPanelProject.Visible = false;
        //ScriptManager.RegisterStartupScript(this, this.GetType(), "CallMyFunction", "ToggleDisplay4()", true);
        //commentpopup6.Visible = true;
        //PanelProjectDetails.Visible = true;
    }   

 
    
    protected void showpopup(object sender, EventArgs e)
    {
        setModalWindow(sender, e);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "CallMyFunction", "callthis1()", true);
        return;

    }
    protected void showpopup1(object sender, EventArgs e)
    {
        setModalWindow1(sender, e);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "CallMyFunction", "callthis5()", true);
        return;

    }
    private void setModalWindow1(object sender, EventArgs e)
    {
        UpdatePanel3.Update();
        popupstudent.Visible = true;
        popupStudentGrid.DataSourceID = "StudentSQLDS";
        StudentSQLDS.DataBind();
        popupStudentGrid.DataBind();
        int rows = popupStudentGrid.Rows.Count;
        popupStudentGrid.Visible = true;

    }
    private void setModalWindow2(object sender, EventArgs e)
    {
        if (DropDownListPublicationEntry.SelectedValue == "JA" || DropDownListPublicationEntry.SelectedValue == "TS")
        {
            if (RadioButtonListIndexed.SelectedValue == "Y")
            {
                //popupPanelProceedingsJournal.Visible = false;
                imageBkCbtn.Visible = true;
                imageBkCbtn1.Visible = false;
                popupPanelJournal.Visible = true;
                popGridJournal.DataSourceID = "SqlDataSourceJournal";
                SqlDataSourceJournal.DataBind();
                popGridJournal.DataBind();
            }
            else
            {
                //popupPanelProceedingsJournal.Visible = false;
                popupPanelJournal.Visible = false;
                popGridJournal.DataSourceID = "SqlDataSourceJournal";
                SqlDataSourceJournal.DataBind();
                popGridJournal.DataBind();
            }
        }
       
    }
    private void setModalWindow3(object sender, EventArgs e)
    {
        if (DropDownListPublicationEntry.SelectedValue == "PR")
        {

            if (RadioButtonListIndexed.SelectedValue == "Y")
            {
                imageBkCbtn.Visible = false;
                imageBkCbtn1.Visible = true;
                //popupPanelJournal.Visible = false;
                popupPanelProceedingsJournal.Visible = true;
                popGridJournalProceedings.DataSourceID = "SqlDataSourceProceedings";
                SqlDataSourceProceedings.DataBind();
                popGridJournalProceedings.DataBind();
            }
            else
            //{
            //    popupPanelJournal.Visible = false;
                popupPanelProceedingsJournal.Visible = false;
                popGridJournalProceedings.DataSourceID = "SqlDataSourceProceedings";
                SqlDataSourceProceedings.DataBind();
                popGridJournalProceedings.DataBind();
            }
        }


    
    protected void showPop2(object sender, EventArgs e)
    {

        PubEntriesUpdatePanel.Update();
        EditUpdatePanel.Update();
        DropDownListhasProjectreference_SelectedIndexChanged(sender, e);
        UpdatePanel4.Update();
        //popupPanelJournal.Visible = false;
        //popupPanelProceedingsJournal.Visible = true;
        popupPanelProject.Visible = true;
        //ModalPopupExtender5.Show();
        //ModalPopupExtender1.Hide();
        setModalWindow2(sender, e);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "CallMyFunction", "callthis2()", true);



    }
    protected void showPop3(object sender, EventArgs e)
    {

        PubEntriesUpdatePanel.Update();
        EditUpdatePanel.Update();
        UpdatePanel5.Update();
        DropDownListhasProjectreference_SelectedIndexChanged(sender, e);
        //popupPanelJournal.Visible = false;
        //popupPanelProceedingsJournal.Visible = true;
        popupPanelProject.Visible = true;
        //ModalPopupExtender5.Show();
        //ModalPopupExtender1.Hide();

        setModalWindow3(sender, e);
    
        ScriptManager.RegisterStartupScript(this, this.GetType(), "CallMyFunction", "callthis3()", true);
        return;

    }
    protected void showPop4(object sender, EventArgs e)
    {

        if (TextBoxProjectDetails.Text != "")
        {
            PubEntriesUpdatePanel.Update();
            EditUpdatePanel.Update();
            UpdatePanel4.Update();
            popupPanelProject.Visible = true;
            UpdatePanel6.Update();
            MainUpdate.Update();
            ArrayList SAmount = new ArrayList();
            string[] toaddress_value = TextBoxProjectDetails.Text.Split(',');
            for (int j = 0; j <= toaddress_value.GetUpperBound(0); j++)
            {
                SAmount.Add(toaddress_value[j]);
            }


            
                for (int i = 0; i < GridViewProject.Rows.Count; i++)
                {
                    CheckBox Checkboxfaculty = (CheckBox)GridViewProject.Rows[i].FindControl("csSelect");
                    Label BudgetId = (Label)GridViewProject.Rows[i].FindControl("TextBoxProjectId");
                    Label Amount = (Label)GridViewProject.Rows[i].FindControl("TextBoxProjectUnit");

                    string projectunit = Amount.Text.ToString();
                    string projectID = BudgetId.Text.ToString();
                    string projectunitID = projectunit + projectID.ToString();
                    for (int j = 0; j < SAmount.Count; j++)
                    {
                        if ( SAmount[j].ToString()==projectunitID)
                        {

                            Checkboxfaculty.Checked = true;

                        }
                        //else
                        //{
                        //    Checkboxfaculty.Checked = false;
                        //}
                    }
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "CallMyFunction", "callthis4()", true);
            return;
        }
        else
        {
            PubEntriesUpdatePanel.Update();
            EditUpdatePanel.Update();
            DropDownListhasProjectreference_SelectedIndexChanged(sender, e);
            UpdatePanel4.Update();
            popupPanelProject.Visible = true;
            setModalWindow4(sender, e);
        }
       
        ScriptManager.RegisterStartupScript(this, this.GetType(), "CallMyFunction", "callthis4()", true);
         return;


    }
    private void setModalWindow4(object sender, EventArgs e)
    {
        UpdatePanel6.Update();
        popupPanelProject.Visible = true;
        GridViewProject.Visible = true;
        //SqlDataSourceProject.SelectCommand = "select Project.ID,Project.ProjectUnit,Title from Project inner join Projectnvestigator on  Project.ID=Projectnvestigator.ID and Project.ProjectUnit=Projectnvestigator.ProjectUnit and Projectnvestigator.EmployeeCode='" + Session["UserId"].ToString() + "' and (ProjectStatus='SAN' or ProjectStatus='CLO')";
        GridViewProject.DataSourceID = "SqlDataSourceProject";
        SqlDataSourceProject.DataBind();
        GridViewProject.DataBind();
    }
    protected void feedbackbutton_Click(object sender, EventArgs e)
    {
        string Type = "Pub";
        BtnSubmit_Click(sender, e);
      if(Session["PublicationEntry"].ToString() =="JA")
      {
      
        string MemberID = Session["UserId"].ToString();
        FeedbackClass u = new FeedbackClass();
        Journal_DataObject Da = new Journal_DataObject();

        u = B.CheckUserforFeedback(MemberID, Type);

        string date3 = u.PublicationUpdatedDate.ToString();

        if (date3 != "01/01/0001 00:00:00")
        {


            string month = ConfigurationManager.AppSettings["FeedBackMonth"].ToString();
            int month1 = Convert.ToInt32(month);
            //DateTime actDate = Convert.ToDateTime(u.PublicationUpdatedDate);


            DateTime fromdate = Convert.ToDateTime(u.PublicationUpdatedDate);
            DateTime todaydate = DateTime.Now;

            int resu = B.gettotalmonths(fromdate, todaydate);
            if (resu >= month1)
            {

                UpdatePanel7.Update();
                panelfeedback.Visible = true;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "CallMyFunction", "callthis7()", true);
                return;
            }
            else
            {

            }
        }
        else
        {
            UpdatePanel7.Update();
            panelfeedback.Visible = true;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "CallMyFunction", "callthis7()", true);
            return;
        }
    }

             

                //UpdatePanel7.Update();
                //panelfeedback.Visible = true;
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "CallMyFunction", "callthis7()", true);
                //return;
    }


    protected void BtnSubmitFeedback_Click(object sender, EventArgs e)
    {

        Journal_DataObject Da = new Journal_DataObject();

        string MemberID = Session["UserId"].ToString();
         string EntryType=Session["EntryType"].ToString();
         string ID=Session["ID"].ToString();
         string Type = "Pub";

        DateTime Date = DateTime.Now;
        DateTime PublicationUpdatedDate = DateTime.Now;      

        feedback.Q1 = txtq1.Text;
        feedback.Q2 = txtq2.Text;
        feedback.Q3 = txtq3.SelectedValue;
        feedback.Q4 = txtq4.Text;
        feedback.Q5 = "";
        feedback.Q6 = "";
        feedback.Q7 = "";
        feedback.Q8 = "";

        if (feedback.Q1 == "" && feedback.Q2 == "" && feedback.Q3 == "" && feedback.Q4 == "")
        {
            string CloseWindow1 = "alert('Please Enter Your Feedback')";
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow1, true);
            return;
        }

        //string res1 = B.FindMemberIdinFeedBackTracker(MemberID, Type, ID);
        //if (res1 != null && res1 != "")
        //{
        //    bool res = Da.InsertintoFeedbackReviewTracker(feedback, Date, MemberID, EntryType, ID, Type);

        //    if (res == true)
        //    {
        //        string CloseWindow1 = "alert('Feedbackdetails updated Succesfully')";
        //        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow1, true);
        //        BtnSubmitFeedback.Enabled = false;
        //        return;

        //    }
        //    else
        //    {
        //        string CloseWindow1 = "alert('Error while updating  Feedback')";
        //        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow1, true);
        //        return;


        //    }



        //}
        //else
        //{
            bool res = Da.InsertintoFeedbackReviewTracker(feedback, Date, MemberID, EntryType, ID, Type);

            if (res == true)
            {               
                string CloseWindow = "alert('Feedbackdetails Saved Succesfully');window.location='../PublicationEntry/PublicationEntry.aspx';";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);
                return; 

            }
            else
            {
                string CloseWindow1 = "alert('Error while Saving the Feedback')";
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow1, true);

            }
        

    }
    protected void exitfeedback(object sender, EventArgs e)
    {
        Response.Redirect("~/PublicationEntry/PublicationEntry.aspx");

    }
}