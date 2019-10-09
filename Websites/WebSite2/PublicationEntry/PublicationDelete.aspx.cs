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
public partial class PublicationEntry_PublicationDelete : System.Web.UI.Page
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
            string UserId = null;
            UserId = Session["UserId"].ToString();
        }
    }

    protected void ButtonSearchPubOnClick(object sender, EventArgs e)
    {
        addclik(sender, e);
        GridViewSearch.Visible = true;
        GridViewSearch.EditIndex = -1;
        dataBind();
    }

    protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //Find the DropDownList in the Row
            DropDownList DropdownMuNonMu = (e.Row.FindControl("DropdownMuNonMu") as DropDownList);

            if (DropDownListPublicationEntry.SelectedValue == "BK"  || DropDownListPublicationEntry.SelectedValue == "NM")
            {


                SqlDataSourceAuthorType.SelectCommand = "SELECT Id,Type FROM [Author_Type_M] where (Id! = 'O') and  (Id! = 'E')  ";

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
    protected void addclik(object sender, EventArgs e)
    {
        string confirmValue2 = Request.Form["confirm_value2"];
        if (confirmValue2 == "Yes")
        {
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

            DropDownListPubType.ClearSelection();
            TextBoxPageFrom.Text = "";

            TextBoxPageTo.Text = "";
            TextBoxVolume.Text = "";
            RadioButtonListIndexed.SelectedValue = "N";
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
            //TextBoxReference.Text = "";
            DropDownListuploadEPrint.ClearSelection();
            TextBoxEprintURL.Text = "";


        }

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

        DropDownListPubType.ClearSelection();
        TextBoxPageFrom.Text = "";

        TextBoxPageTo.Text = "";
        TextBoxVolume.Text = "";
        RadioButtonListIndexed.SelectedValue = "N";
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

       
        TextBoxRemarks.Text = "";
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
    protected void JournalIDTextChanged(object sender, EventArgs e)
    {
        
        JournalValueObj.JournalID = TextBoxPubJournal.Text;
        // JournalValueObj.year = txtBoxYear.Text;
        JournalData j = new JournalData();
        j = B.JournalEntryCheckExistance(JournalValueObj);
        if (j.jid != null)
        {
            // ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Entry ALready Exists')</script>");


            string year = DateTime.Now.Year.ToString();
            int Jyear = Convert.ToInt32(year) - 1;

            TextBoxYearJA.Text = Jyear.ToString();
            txtboxYear_TextChanged(sender, e);

        }
        else
        {
            ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Invalid ID')</script>");
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
                TextBoxImpFact.Text = "";
            }
        }
        else
        {
            TextBoxImpFact.Text = "";
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



        affiliateSrch.Text = "";
        popGridAffil.DataBind();

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
    protected void exit(object sender, EventArgs e)
    {
        affiliateSrch.Text = "";
        popGridAffil.DataBind();
    }

    protected void exit1(object sender, EventArgs e)
    {

        journalcodeSrch.Text = "";
        popGridJournal.DataBind();

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
    //private void dataBind()
    //{
    //    GridViewSearch.Visible = true;
    //    string pubtype = EntryTypesearch.SelectedValue;
    //    if (PubIDSearch.Text == "")
    //    {
    //        SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,t.EntryName,PubJournalID,PublishJAMonth,TitleWorkItem,c.PubCatName,PublishJAYear,ImpactFactor ,s.StatusName from Publication p,Status_Publication_M s , PubMUCategorization_M c ,PublicationTypeEntry_M t where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization and  (TypeOfEntry='BK' or TypeOfEntry='CP' or TypeOfEntry='JA' or TypeOfEntry='NM' or TypeOfEntry='TS')  and (Status='CAN')";

    //    }
    //    else
    //    {
    //        SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,t.EntryName,PubJournalID,PublishJAMonth,TitleWorkItem,c.PubCatName,PublishJAYear,ImpactFactor ,s.StatusName from Publication p,Status_Publication_M s , PubMUCategorization_M c ,PublicationTypeEntry_M t where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization and  (TypeOfEntry='" + EntryTypesearch.SelectedValue + "' or '" + EntryTypesearch.SelectedValue + "'='A') and (Status='CAN') and PublicationID like '%" + PubIDSearch.Text.Trim() + "%'";

    //    }
    //    GridViewSearch.DataBind();
    //    SqlDataSource1.DataBind();
    //    GridViewSearch.Visible = true;
    //}

    private void dataBind()
    {
        panel.Visible = false;
        GridViewSearch.Visible = true;
        Business b = new Business();
        User a = new User();
        string SupId = null;
        string inst = Session["InstituteId"].ToString();

        string user = Session["UserId"].ToString();
        string dept = Session["Department"].ToString();
        SupId = b.GetSupId(inst, Session["UserId"].ToString(), Session["Department"].ToString());
        string Role = Session["Role"].ToString();
        a = b.GetPublicationIncharge(user);

        User a1 = new User();

        //a1 = b.GetPublicationInchargeInst(user);

        if (Role == "1")
        {
            if (a.Department_Id == "")
            {

                if (EntryTypesearch.SelectedValue != "A")
                {
                    if (PubIDSearch.Text == "")
                    {
                        //SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,t.EntryName,TitleWorkItem,c.PubCatName,PublishJAMonth,PublishJAYear,ImpactFactor ,s.StatusName from Publication p,Status_Publication_M s , PubMUCategorization_M c ,PublicationTypeEntry_M t where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization and TypeOfEntry='" + EntryTypesearch.SelectedValue + "' and Status='SUB'  and Institution='" + a1.InstituteId + "' ";

                        SqlDataSource1.SelectParameters.Clear();
                        SqlDataSource1.SelectParameters.Add("TypeOfEntry", EntryTypesearch.SelectedValue);
                        SqlDataSource1.SelectParameters.Add("Institution", a.InstituteId);

                        SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName ,t.EntryName,TitleWorkItem,c.PubCatName from Publication p,Status_Publication_M s, PubMUCategorization_M c ,PublicationTypeEntry_M t  where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization  and status='CAN' and TypeOfEntry=@TypeOfEntry  and Institution=@Institution and Department not in(select distinct Department_Id from Publication_InchargerM where InstituteId=@Institution and Active='Y')";
                    }
                    else if (PubIDSearch.Text != "")
                    {
                        SqlDataSource1.SelectParameters.Clear();
                        SqlDataSource1.SelectParameters.Add("Institution", a.InstituteId);
                        SqlDataSource1.SelectParameters.Add("TypeOfEntry", EntryTypesearch.SelectedValue);
                        SqlDataSource1.SelectParameters.Add("PublicationID", PubIDSearch.Text.Trim());

                        SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName,t.EntryName,TitleWorkItem,c.PubCatName from Publication p,Status_Publication_M s , PubMUCategorization_M c ,PublicationTypeEntry_M t where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization and status='CAN' and  TypeOfEntry=@TypeOfEntry   and PublicationID like '%' + @PublicationID + '%'  and Institution=@Institution and Department not in(select distinct Department_Id from Publication_InchargerM where InstituteId=@Institution and Active='Y')";
                        // SqlDataSource1.SelectCommand = "select j.Id as Id,Year,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,Comments from Journal_IF_Details j,Journal_M k where j.Id=k.Id and j.Id='" + txtJid.Text.Trim() + "'";
                    }
                 
                    else
                    {
                        SqlDataSource1.SelectParameters.Clear();
                        SqlDataSource1.SelectParameters.Add("Institution", a.InstituteId);
                        SqlDataSource1.SelectParameters.Add("TypeOfEntry", EntryTypesearch.SelectedValue);
                        SqlDataSource1.SelectParameters.Add("PublicationID", PubIDSearch.Text.Trim());

                        SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName,t.EntryName,TitleWorkItem,c.PubCatName from Publication p,Status_Publication_M s , PubMUCategorization_M c ,PublicationTypeEntry_M t where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization and status='CAN' and  TypeOfEntry=@TypeOfEntry   and PublicationID like '%' + @PublicationID + '%' and Institution=@Institution and Department not in(select distinct Department_Id from Publication_InchargerM where InstituteId=@Institution and Active='Y')";
                        // SqlDataSource1.SelectCommand = "select j.Id as Id,Year,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,Comments from Journal_IF_Details j,Journal_M k where j.Id=k.Id and j.Id='" + txtJid.Text.Trim() + "'";
                    }
                    GridViewSearch.DataBind();
                    SqlDataSource1.DataBind();
                }
                else
                {
                    if (PubIDSearch.Text == "" )
                    {
                        SqlDataSource1.SelectParameters.Clear();
                        SqlDataSource1.SelectParameters.Add("Institution", a.InstituteId);
                        SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName,t.EntryName,TitleWorkItem,c.PubCatName from Publication p,Status_Publication_M s , PubMUCategorization_M c ,PublicationTypeEntry_M t where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization and status='CAN'  and Institution=@Institution and Department not in(select distinct Department_Id from Publication_InchargerM where InstituteId=@Institution and Active='Y')";
                    }
                    else if (PubIDSearch.Text != "")
                    {
                        SqlDataSource1.SelectParameters.Clear();
                        SqlDataSource1.SelectParameters.Add("Institution", a.InstituteId);
                        SqlDataSource1.SelectParameters.Add("PublicationID", PubIDSearch.Text.Trim());

                        SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName,t.EntryName,TitleWorkItem,c.PubCatName from Publication p,Status_Publication_M s, PubMUCategorization_M c ,PublicationTypeEntry_M t  where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization  and status='CAN' and   PublicationID like '%' + @PublicationID + '%'  and Institution=@Institution and Department not in(select distinct Department_Id from Publication_InchargerM where InstituteId=@Institution and Active='Y')";
                        // SqlDataSource1.SelectCommand = "select j.Id as Id,Year,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,Comments from Journal_IF_Details j,Journal_M k where j.Id=k.Id and j.Id='" + txtJid.Text.Trim() + "'";
                    }
                  
                    else
                    {
                        SqlDataSource1.SelectParameters.Clear();
                        SqlDataSource1.SelectParameters.Add("Institution", a.InstituteId);
                        SqlDataSource1.SelectParameters.Add("PublicationID", PubIDSearch.Text.Trim());

                        SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName,t.EntryName,TitleWorkItem,c.PubCatName from Publication p,Status_Publication_M s, PubMUCategorization_M c ,PublicationTypeEntry_M t  where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization  and status='CAN' and   PublicationID like '%' + @PublicationID + '%' and Institution=@Institution and Department not in(select distinct Department_Id from Publication_InchargerM where InstituteId=@Institution and Active='Y')";
                        // SqlDataSource1.SelectCommand = "select j.Id as Id,Year,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,Comments from Journal_IF_Details j,Journal_M k where j.Id=k.Id and j.Id='" + txtJid.Text.Trim() + "'";
                    }
                    GridViewSearch.DataBind();
                    SqlDataSource1.DataBind();
                }
            }

            else
            {
                if (EntryTypesearch.SelectedValue != "A")
                {
                    if (PubIDSearch.Text == "")
                    {
                        //SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,t.EntryName,TitleWorkItem,c.PubCatName,PublishJAMonth,PublishJAYear,ImpactFactor ,s.StatusName from Publication p,Status_Publication_M s , PubMUCategorization_M c ,PublicationTypeEntry_M t where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization and TypeOfEntry='" + EntryTypesearch.SelectedValue + "' and Status='SUB'  and Institution='" + a1.InstituteId + "' ";

                        SqlDataSource1.SelectParameters.Clear();
                        SqlDataSource1.SelectParameters.Add("Institution", a.InstituteId);
                        SqlDataSource1.SelectParameters.Add("TypeOfEntry", EntryTypesearch.SelectedValue);
                        SqlDataSource1.SelectParameters.Add("Department", a.Department_Id);


                        SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName ,t.EntryName,TitleWorkItem,c.PubCatName from Publication p,Status_Publication_M s, PubMUCategorization_M c ,PublicationTypeEntry_M t  where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization  and status='CAN' and TypeOfEntry=@TypeOfEntry  and Institution=@Institution and Department=@Department  ";
                    }
                    else if (PubIDSearch.Text != "" )
                    {
                        SqlDataSource1.SelectParameters.Clear();
                        SqlDataSource1.SelectParameters.Add("Institution", a.InstituteId);
                        SqlDataSource1.SelectParameters.Add("PublicationID", PubIDSearch.Text.Trim());
                        SqlDataSource1.SelectParameters.Add("Department", a.Department_Id);
                        SqlDataSource1.SelectParameters.Add("TypeOfEntry", EntryTypesearch.SelectedValue);

                        SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName,t.EntryName,TitleWorkItem,c.PubCatName from Publication p,Status_Publication_M s , PubMUCategorization_M c ,PublicationTypeEntry_M t where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization and status='CAN' and  TypeOfEntry=@TypeOfEntry   and PublicationID like '%' + @PublicationID + '%'  and Institution=@Institution and Department=@Department ";
                        // SqlDataSource1.SelectCommand = "select j.Id as Id,Year,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,Comments from Journal_IF_Details j,Journal_M k where j.Id=k.Id and j.Id='" + txtJid.Text.Trim() + "'";
                    }
                   
                    else
                    {
                        SqlDataSource1.SelectParameters.Clear();
                        SqlDataSource1.SelectParameters.Add("Institution", a.InstituteId);
                        SqlDataSource1.SelectParameters.Add("PublicationID", PubIDSearch.Text.Trim());
                        SqlDataSource1.SelectParameters.Add("Department", a.Department_Id);
                        SqlDataSource1.SelectParameters.Add("TypeOfEntry", EntryTypesearch.SelectedValue);

                        SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName,t.EntryName,TitleWorkItem,c.PubCatName from Publication p,Status_Publication_M s , PubMUCategorization_M c ,PublicationTypeEntry_M t where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization and status='CAN' and  TypeOfEntry=@TypeOfEntry   and PublicationID like '%' + @PublicationID + '%' and Institution=@Institution and Department=@Department ";
                        // SqlDataSource1.SelectCommand = "select j.Id as Id,Year,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,Comments from Journal_IF_Details j,Journal_M k where j.Id=k.Id and j.Id='" + txtJid.Text.Trim() + "'";
                    }
                    GridViewSearch.DataBind();
                    SqlDataSource1.DataBind();
                }
                else
                {
                    if (PubIDSearch.Text == "" )
                    {
                        SqlDataSource1.SelectParameters.Clear();
                        SqlDataSource1.SelectParameters.Add("Institution", a.InstituteId);
                        SqlDataSource1.SelectParameters.Add("Department", a.Department_Id);

                        SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName,t.EntryName,TitleWorkItem,c.PubCatName from Publication p,Status_Publication_M s , PubMUCategorization_M c ,PublicationTypeEntry_M t where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization and status='CAN'  and Institution=@Institution and Department=@Department ";
                    }
                    else if (PubIDSearch.Text != "" )
                    {
                        SqlDataSource1.SelectParameters.Clear();
                        SqlDataSource1.SelectParameters.Add("Institution", a.InstituteId);
                        SqlDataSource1.SelectParameters.Add("Department", a.Department_Id);
                        SqlDataSource1.SelectParameters.Add("PublicationID", PubIDSearch.Text.Trim());

                        SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName,t.EntryName,TitleWorkItem,c.PubCatName from Publication p,Status_Publication_M s, PubMUCategorization_M c ,PublicationTypeEntry_M t  where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization  and status='CAN' and   PublicationID like '%' + @PublicationID + '%'  and Institution=@Institution and Department=@Department ";
                        // SqlDataSource1.SelectCommand = "select j.Id as Id,Year,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,Comments from Journal_IF_Details j,Journal_M k where j.Id=k.Id and j.Id='" + txtJid.Text.Trim() + "'";
                    }
                    
                    else
                    {
                        SqlDataSource1.SelectParameters.Clear();
                        SqlDataSource1.SelectParameters.Add("Institution", a.InstituteId);
                        SqlDataSource1.SelectParameters.Add("Department", a.Department_Id);
                        SqlDataSource1.SelectParameters.Add("PublicationID", PubIDSearch.Text.Trim());

                        SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName,t.EntryName,TitleWorkItem,c.PubCatName from Publication p,Status_Publication_M s, PubMUCategorization_M c ,PublicationTypeEntry_M t  where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization  and status='CAN' and   PublicationID like '%' + @PublicationID + '%'  and Institution=@Institution and Department=@Department ";
                        // SqlDataSource1.SelectCommand = "select j.Id as Id,Year,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,Comments from Journal_IF_Details j,Journal_M k where j.Id=k.Id and j.Id='" + txtJid.Text.Trim() + "'";
                    }
                    GridViewSearch.DataBind();
                    SqlDataSource1.DataBind();
                }
            }
        }
        else if (Role == "2")
        {
            if (EntryTypesearch.SelectedValue != "A")
            {
                if (PubIDSearch.Text == "")
                {
                    SqlDataSource1.SelectParameters.Clear();
                    SqlDataSource1.SelectParameters.Add("TypeOfEntry", EntryTypesearch.SelectedValue);

                    SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName,t.EntryName,TitleWorkItem,c.PubCatName from Publication p,Status_Publication_M s, PubMUCategorization_M c ,PublicationTypeEntry_M t  where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization  and status='CAN' and  ( TypeOfEntry=@TypeOfEntry or @TypeOfEntry='A')";
                }
                else if (PubIDSearch.Text != "")
                {
                    SqlDataSource1.SelectParameters.Clear();
                    SqlDataSource1.SelectParameters.Add("PublicationID", PubIDSearch.Text.Trim());
                    SqlDataSource1.SelectParameters.Add("TypeOfEntry", EntryTypesearch.SelectedValue);

                    SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName,t.EntryName,TitleWorkItem,c.PubCatName from Publication p,Status_Publication_M s, PubMUCategorization_M c ,PublicationTypeEntry_M t  where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization  and status='CAN' and   (PublicationID like '%' + @PublicationID + '%')  and ( TypeOfEntry=@TypeOfEntry or @TypeOfEntry='A')";
                }
            }
            else
            {
                if (PubIDSearch.Text == "")
                {
                    SqlDataSource1.SelectParameters.Clear();
                    SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName,t.EntryName,TitleWorkItem,c.PubCatName from Publication p,Status_Publication_M s, PubMUCategorization_M c ,PublicationTypeEntry_M t  where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization  and status='CAN' ";
                }
                else if (PubIDSearch.Text != "")
                {
                    SqlDataSource1.SelectParameters.Clear();
                    SqlDataSource1.SelectParameters.Add("PublicationID", PubIDSearch.Text.Trim());

                    SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName,t.EntryName,TitleWorkItem,c.PubCatName from Publication p,Status_Publication_M s, PubMUCategorization_M c ,PublicationTypeEntry_M t  where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization  and status='CAN' and   (PublicationID like '%' + @PublicationID + '%' )  ";
                }
            }
           
            GridViewSearch.DataBind();
            SqlDataSource1.DataBind();
        }
        else
        {
            ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please contact Admin!!!!')</script>");

        }

    }

    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        ImageButton EditButton = (ImageButton)e.Row.FindControl("BtnEdit");
    }

    //public void GridView2_RowCommand(Object sender, GridViewCommandEventArgs e)
    //{

    //    string pid = null;
    //    string typeEntry = null;
    //    if (e.CommandName == "Edit")
    //    {

    //        GridViewRow rowSelect = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
    //        int rowindex = rowSelect.RowIndex;
    //        HiddenField TypeOfEntry = (HiddenField)GridViewSearch.Rows[rowindex].Cells[6].FindControl("TypeOfEntry");
    //        typeEntry = TypeOfEntry.Value;
    //        pid = GridViewSearch.Rows[rowindex].Cells[1].Text.Trim().ToString();
    //        Session["TempPid"] = pid;
    //        Session["TempTypeEntry"] = typeEntry;
    //    }

    //}

    public void edit(Object sender, GridViewEditEventArgs e)
    {
        GridViewSearch.EditIndex = e.NewEditIndex;

        fnRecordExist(sender, e);

    }

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

    protected void showPop(object sender, EventArgs e)
    {
        popupPanelJournal.Visible = true;
        ModalPopupExtender1.Show();
        popupPanelAffil.Visible = false;
    }
    public void GridView2_RowCommand(Object sender, GridViewCommandEventArgs e)
    {
        string pid = null;
        string typeEntry = null;
        if (e.CommandName == "Edit")
        {

            GridViewRow rowSelect = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
            int rowindex = rowSelect.RowIndex;
            HiddenField TypeOfEntry = (HiddenField)GridViewSearch.Rows[rowindex].Cells[7].FindControl("TypeOfEntry");
            //  typeEntry = GridViewSearch.Rows[rowindex].Cells[3].Text.ToString();
            typeEntry = TypeOfEntry.Value;
            pid = GridViewSearch.Rows[rowindex].Cells[2].Text.Trim().ToString();
            Session["TempPid"] = pid;
            Session["TempTypeEntry"] = typeEntry;//maintaining a session variable, passing it to registration page
        }

        else if (e.CommandName == "View")
        {
            GridViewRow rowSelect = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
            int rowindex = rowSelect.RowIndex;
            HiddenField TypeOfEntry = (HiddenField)GridViewSearch.Rows[rowindex].Cells[7].FindControl("TypeOfEntry");
            //  typeEntry = GridViewSearch.Rows[rowindex].Cells[3].Text.ToString();
            typeEntry = TypeOfEntry.Value;
            pid = GridViewSearch.Rows[rowindex].Cells[2].Text.Trim().ToString();
            Session["TempPid"] = pid;
            Session["TempTypeEntry"] = typeEntry;

            fnRecordExistApproval(sender, e);
        }
    }

    public void fnRecordExist(object sender, EventArgs e)
    {
        //lblmsg.Visible = false;

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

        // FileUploadPdf.Visible = true;

        DropDownListMuCategory.SelectedValue = v.MUCategorization;
        DropDownListPublicationEntry.SelectedValue = TypeEntry;
        TextBoxPubId.Text = Pid;
        txtboxTitleOfWrkItem.Text = v.TitleWorkItem;
        if (TypeEntry == "JA" || TypeEntry == "TA")
        {
            string incentivestatus = b1.checkIncentivePointStatus(Pid, TypeEntry);
            if (incentivestatus == "PRC")
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

            TextBoxNameJournal.Text = jname;

            DropDownListMonthJA.SelectedValue = v.PublishJAMonth.ToString();

            TextBoxYearJA.Text = v.PublishJAYear;

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
            DropDownListPubType.SelectedValue = v.Publicationtype;
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
            if (v.IFApplicableYear != 0)
            {
                txtIFApplicableYear.Text = v.IFApplicableYear.ToString();
            }
            else
            {
                txtIFApplicableYear.Text = "";
            }
            DropDownListPubType.SelectedValue = v.Publicationtype;
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

            TextBoxDate.Text = v.NewsPublishedDate.ToShortDateString();

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
            TextBoxDateOfPublish.Text = v.NewsPublishedDate.ToString();

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
            if (v.PageFrom.Contains("PF"))
            {
            }
            else
            {
                TextBoxPageFrom.Text = v.PageFrom;
                TextBoxPageTo.Text = v.PageTo;
            }
            //   TextBoxPageFrom.Text = v.PageFrom;

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



                rowIndex++;

            }


            ViewState["CurrentTable"] = dtCurrentTable;
        }
          if(DropDownListPublicationEntry.SelectedValue == "CP" )
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
        //  setModalWindow(sender, e);

        //ButtonSub.Enabled = false;
        btnSave.Enabled = true;

        GridViewSearch.Visible = false;
    }

    public void fnRecordExistApproval(object sender, EventArgs e)
    {
        //lblmsg.Visible = true;

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

        if (TypeEntry == "JA" || TypeEntry == "TA")
        {
            string incentivestatus = b1.checkIncentivePointStatus(Pid, TypeEntry);
            if (incentivestatus == "CAN")
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
        // FileUploadPdf.Visible = true;
        LabelUploadPfd.Visible = true;
        Business obj = new Business();
        PublishData v = new PublishData();
        v = obj.fnfindjid(Pid, TypeEntry);

        DropDownListMuCategory.DataBind();
        DropDownListMuCategory.SelectedValue = v.MUCategorization;
        DropDownListPublicationEntry.DataBind();
        DropDownListPublicationEntry.SelectedValue = TypeEntry;
        TextBoxPubId.Text = Pid;
        txtboxTitleOfWrkItem.Text = v.TitleWorkItem;
        //   v = obj.fnfindjid(Pid, TypeEntry);
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
                txtIFApplicableYear.Text = v.IFApplicableYear.ToString();
            }
            else
            {
                txtIFApplicableYear.Text = "";
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
            // TextBoxPageFrom.Text = v.PageFrom;
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

            TextBoxNameJournal.Text = jname;

            DropDownListMonthJA.SelectedValue = v.PublishJAMonth.ToString();

            TextBoxYearJA.Text = v.PublishJAYear;

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

            TextBoxYear.Text = v.BookPublishYear;
            DropDownListBookMonth.DataBind();
            if (v.BookPublishMonth != 0)
            {
                DropDownListBookMonth.SelectedValue = v.BookPublishMonth.ToString();
            }
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

            TextBoxDate.Text = v.Date.ToShortDateString();
            if (v.TypePresentation != "")
            {
                RadioButtonListTypePresentaion.SelectedValue = v.TypePresentation;
            }
            TextBoxCreditPoint.Text = v.CreditPoint.ToString();
            TextBoxAwardedBy.Text = v.AwardedBy;

            TextBoxIsbn.Text = v.ConfISBN;
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

                IsPresenter.SelectedValue = dtCurrentTable.Rows[i - 1]["IsPresenter"].ToString();
                if (dtCurrentTable.Rows[i - 1]["HasAttented"].ToString() == "Y")
                {
                    HasAttented.Checked = true;
                }
                else
                {
                    HasAttented.Checked = false;
                }
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

                // Grid_AuthorEntry.Columns[13].Visible = false;


                rowIndex++;

            }


            ViewState["CurrentTable"] = dtCurrentTable;
        }
          if(DropDownListPublicationEntry.SelectedValue == "CP" )
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
        //setModalWindow(sender, e);

        //ButtonSub.Enabled = true;
        // btnSave.Enabled = true;


        GridViewSearch.Visible = false;
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

    protected void IsPresenterIsPresenter(object sender, EventArgs e)
    {
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


    }
    protected void BtnSave_Click(object sender, EventArgs e)
    {
        try
        {
            Business B = new Business();
            PublishData v = new PublishData();
            if (TextBoxRemarks.Text == "")
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please enter Remarks!!!!')</script>");

                return;
            }
            v.TypeOfEntry = DropDownListPublicationEntry.SelectedValue;
            v.MUCategorization = DropDownListMuCategory.SelectedValue;
            v.PaublicationID = TextBoxPubId.Text;
            v.TitleWorkItem = txtboxTitleOfWrkItem.Text;
            v.RemarksFeedback = TextBoxRemarks.Text.Trim();
            v.DeletedBy = Session["UserId"].ToString();

            int result = B.DeletePublication(v); //Business layer

            if (result >= 1)
            {
                string CloseWindow1 = "alert('Publication Entry deleted successfully')";
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow1, true);
                btnSave.Enabled = false;
            }
            else
            {
                string CloseWindow1 = "alert('Publication Entry not deleted ')";
                ScriptManager.RegisterStartupScript(Page, typeof(Page), "newWindow", CloseWindow1, true);
                btnSave.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            log.Error(ex.StackTrace);
            log.Error(ex.Message);
            log.Error("Error!!!!!!!!!!!!!!!! ");
            ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Error!!!!!!!!!!')</script>");
        }

    }

  
}

