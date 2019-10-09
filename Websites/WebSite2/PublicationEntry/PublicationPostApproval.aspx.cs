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
public partial class PublicationPostApproval : System.Web.UI.Page
{
    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    //string mainpath = ConfigurationManager.AppSettings["PdfPath"].ToString();

    string PublicationYearwebConfig = ConfigurationManager.AppSettings["PublicationYear"];
    // string PublicationYearAddwebConfig = ConfigurationManager.AppSettings["PublicationYearAdd"];

    Business B = new Business();
    Journal_DataObject JournalDataObj = new Journal_DataObject();
    JournalData JournalValueObj = new JournalData();
    public string pageID = "L45";
    protected void Page_Load(object sender, EventArgs e)
    {      //popupPanelJournal.Visible = true;
        if (!IsPostBack)
        {
            //if (!Session["authPage"].ToString().Contains("$" + pageID + "$"))
            //{
            //    string unacces = "Unauthorized Acces!!! Login Again";
            //    Response.Redirect("~/Login.aspx?val=" + unacces);
            //}

            DropDownListPublicationEntry.Enabled = false;

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

            Response.Write("<script>");
            Response.Write("window.open('DisplayPdf.aspx?val=" + newpath + "','_blank')");
            //path sent to display.aspx page
            Response.Write("</script>");
        }

    }
    protected void ButtonSearchPubOnClick(object sender, EventArgs e)
    {
        GridViewSearch.Visible = true;
        GridViewSearch.EditIndex = -1;
        addclik(sender, e);
        dataBind();
        // lblmsg.Visible = false;
        popupstudent.Visible = false;
    }
    protected void GridViewSearchPub_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        dataBind();
        GridViewSearch.PageIndex = e.NewPageIndex;
        GridViewSearch.DataBind();
    }

    private void dataBind()
    {
        panel.Visible = false;
        GridViewSearch.Visible = true;
        string SupId = null;
        User a = new User();
        Business b = new Business();
        string inst = Session["InstituteId"].ToString();
        string dept = Session["Department"].ToString();
        string user = Session["UserId"].ToString();

        SupId = b.GetSupId(inst, Session["UserId"].ToString(), Session["Department"].ToString());
        a = b.GetPublicationIncharge(user);
        string Role = Session["Role"].ToString();
        //User a1 = new User();

        //a1 = b.GetPublicationInchargeInst(user);
        if (Role == "1")
        {
            if (a.Department_Id == "")
            {

                if (EntryTypesearch.SelectedValue != "A")
                {
                    if (PubIDSearch.Text == "" && TextBoxWorkItemSearch.Text == "")
                    {
                        // SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,t.EntryName,TitleWorkItem,c.PubCatName,PublishJAMonth,PublishJAYear,ImpactFactor ,s.StatusName from Publication p,Status_Publication_M s , PubMUCategorization_M c ,PublicationTypeEntry_M t where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization and TypeOfEntry='" + EntryTypesearch.SelectedValue + "' and Status='SUB'  and Institution='" + a1.InstituteId + "' ";
                        SqlDataSource1.SelectParameters.Clear();
                        SqlDataSource1.SelectParameters.Add("TypeOfEntry", EntryTypesearch.SelectedValue);
                        SqlDataSource1.SelectParameters.Add("Institution", a.InstituteId);


                        SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,t.EntryName,TitleWorkItem,c.PubCatName,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName,ISStudentAuthor from Publication p,Status_Publication_M s , PubMUCategorization_M c ,PublicationTypeEntry_M t where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization  and  TypeOfEntry=@TypeOfEntry and Status='APP' and EntryType='F'  and Institution=@Institution and Department not in(select distinct Department_Id from Publication_InchargerM where InstituteId=@Institution and Active='Y')";
                    }
                    else if (PubIDSearch.Text != "" && TextBoxWorkItemSearch.Text == "")
                    {
                        // SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,t.EntryName,TitleWorkItem,c.PubCatName,PublishJAMonth,PublishJAYear,ImpactFactor ,s.StatusName from Publication p,Status_Publication_M s , PubMUCategorization_M c ,PublicationTypeEntry_M t where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization and TypeOfEntry='" + EntryTypesearch.SelectedValue + "' and Status='SUB'  and Institution='" + a1.InstituteId + "' ";
                        SqlDataSource1.SelectParameters.Clear();
                        SqlDataSource1.SelectParameters.Add("TypeOfEntry", EntryTypesearch.SelectedValue);
                        SqlDataSource1.SelectParameters.Add("Institution", a.InstituteId);
                        SqlDataSource1.SelectParameters.Add("PublicationID", PubIDSearch.Text.Trim());


                        SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,t.EntryName,TitleWorkItem,c.PubCatName,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName,ISStudentAuthor from Publication p,Status_Publication_M s , PubMUCategorization_M c ,PublicationTypeEntry_M t where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization  and  TypeOfEntry=@TypeOfEntry and Status='APP' and EntryType='F' and PublicationID like '%' + @PublicationID + '%' and Institution=@Institution and Department not in(select distinct Department_Id from Publication_InchargerM where InstituteId=@Institution and Active='Y')";
                    }
                    else if (PubIDSearch.Text == "" && TextBoxWorkItemSearch.Text != "")
                    {
                        // SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,t.EntryName,TitleWorkItem,c.PubCatName,PublishJAMonth,PublishJAYear,ImpactFactor ,s.StatusName from Publication p,Status_Publication_M s , PubMUCategorization_M c ,PublicationTypeEntry_M t where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization and TypeOfEntry='" + EntryTypesearch.SelectedValue + "' and Status='SUB'  and Institution='" + a1.InstituteId + "' ";

                        SqlDataSource1.SelectParameters.Clear();
                        SqlDataSource1.SelectParameters.Add("TypeOfEntry", EntryTypesearch.SelectedValue);
                        SqlDataSource1.SelectParameters.Add("Institution", a.InstituteId);
                        SqlDataSource1.SelectParameters.Add("TitleWorkItem", TextBoxWorkItemSearch.Text.Trim());

                        SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,t.EntryName,TitleWorkItem,c.PubCatName,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName,ISStudentAuthor from Publication p,Status_Publication_M s , PubMUCategorization_M c ,PublicationTypeEntry_M t where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization  and  TypeOfEntry=@TypeOfEntry and Status='APP' and EntryType='F' and TitleWorkItem like '%' + @TitleWorkItem + '%' and Institution=@Institution and Department not in(select distinct Department_Id from Publication_InchargerM where InstituteId=@Institution and Active='Y')";
                    }
                    else
                    {
                        SqlDataSource1.SelectParameters.Clear();
                        SqlDataSource1.SelectParameters.Add("TypeOfEntry", EntryTypesearch.SelectedValue);
                        SqlDataSource1.SelectParameters.Add("Institution", a.InstituteId);
                        SqlDataSource1.SelectParameters.Add("TitleWorkItem", TextBoxWorkItemSearch.Text.Trim());
                        SqlDataSource1.SelectParameters.Add("PublicationID", PubIDSearch.Text.Trim());

                        SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,t.EntryName,TitleWorkItem,c.PubCatName,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName,ISStudentAuthor from Publication p,Status_Publication_M s , PubMUCategorization_M c ,PublicationTypeEntry_M  t where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization and TypeOfEntry=@TypeOfEntry and Status='APP' and EntryType='F'  and PublicationID like '%' + @PublicationID + '%' and TitleWorkItem like '%' + @TitleWorkItem + '%' and Institution=@Institution and Department not in(select distinct Department_Id from Publication_InchargerM where InstituteId=@Institution and Active='Y')";
                        // SqlDataSource1.SelectCommand = "select j.Id as Id,Year,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,Comments from Journal_IF_Details j,Journal_M k where j.Id=k.Id and j.Id='" + txtJid.Text.Trim() + "'";
                    }
                    GridViewSearch.DataBind();
                    SqlDataSource1.DataBind();
                }
                else
                {
                    if (PubIDSearch.Text == "" && TextBoxWorkItemSearch.Text == "")
                    {
                        //  SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor from Publication where   Status='APP' and SupervisorID='" + SupId + "' ";

                        SqlDataSource1.SelectParameters.Clear();
                        SqlDataSource1.SelectParameters.Add("Institution", a.InstituteId);

                        SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,t.EntryName,TitleWorkItem,c.PubCatName,PublishJAMonth,PublishJAYear,ImpactFactor,ISStudentAuthor from Publication p, PubMUCategorization_M c ,PublicationTypeEntry_M t where   Status='APP' and EntryType='F' and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization and Institution=@Institution and Department not in(select distinct Department_Id from Publication_InchargerM where InstituteId=@Institution and Active='Y')";

                    }
                    else if (PubIDSearch.Text != "" && TextBoxWorkItemSearch.Text == "")
                    {

                        SqlDataSource1.SelectParameters.Clear();
                        SqlDataSource1.SelectParameters.Add("Institution", a.InstituteId);
                        SqlDataSource1.SelectParameters.Add("PublicationID", PubIDSearch.Text.Trim());

                        SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,t.EntryName,TitleWorkItem,c.PubCatName,PublishJAMonth,PublishJAYear,ImpactFactor,ISStudentAuthor from Publication p, PubMUCategorization_M c ,PublicationTypeEntry_M t where  Status='APP' and EntryType='F'  and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization and PublicationID like '%' + @PublicationID + '%' and Institution=@Institution and Department not in(select distinct Department_Id from Publication_InchargerM where InstituteId=@Institution and Active='Y') ";
                        // SqlDataSource1.SelectCommand = "select j.Id as Id,Year,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,Comments from Journal_IF_Details j,Journal_M k where j.Id=k.Id and j.Id='" + txtJid.Text.Trim() + "'";
                    }
                    else if (PubIDSearch.Text == "" && TextBoxWorkItemSearch.Text != "")
                    {
                        SqlDataSource1.SelectParameters.Clear();
                        SqlDataSource1.SelectParameters.Add("Institution", a.InstituteId);
                        SqlDataSource1.SelectParameters.Add("TitleWorkItem", TextBoxWorkItemSearch.Text.Trim());
                        SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,t.EntryName,TitleWorkItem,c.PubCatName,PublishJAMonth,PublishJAYear,ImpactFactor,ISStudentAuthor from Publication p, PubMUCategorization_M c ,PublicationTypeEntry_M t where  Status='APP' and EntryType='F'  and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization and TitleWorkItem like '%' + @TitleWorkItem + '%' and Institution=@Institution and Department not in(select distinct Department_Id from Publication_InchargerM where InstituteId=@Institution and Active='Y')";
                        // SqlDataSource1.SelectCommand = "select j.Id as Id,Year,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,Comments from Journal_IF_Details j,Journal_M k where j.Id=k.Id and j.Id='" + txtJid.Text.Trim() + "'";
                    }
                    else
                    {
                        SqlDataSource1.SelectParameters.Clear();
                        SqlDataSource1.SelectParameters.Add("Institution", a.InstituteId);
                        SqlDataSource1.SelectParameters.Add("TitleWorkItem", TextBoxWorkItemSearch.Text.Trim());
                        SqlDataSource1.SelectParameters.Add("PublicationID", PubIDSearch.Text.Trim());

                        SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,t.EntryName,TitleWorkItem,c.PubCatName,PublishJAMonth,PublishJAYear,ImpactFactor,ISStudentAuthor from Publication p, PubMUCategorization_M c ,PublicationTypeEntry_M t where  Status='APP' and EntryType='F'  and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization and PublicationID like '%' + @PublicationID + '%' and TitleWorkItem like '%' + @TitleWorkItem + '%' and Institution=@Institution and Department not in(select distinct Department_Id from Publication_InchargerM where InstituteId=@Institution and Active='Y')";
                        // SqlDataSource1.SelectCommand = "select j.Id as Id,Year,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,Comments from Journal_IF_Details j,Journal_M k where j.Id=k.Id and j.Id='" + txtJid.Text.Trim() + "'";
                    }
                    GridViewSearch.DataBind();
                    SqlDataSource1.DataBind();
                }
            }
            else
            {
                SqlDataSource1.SelectParameters.Clear();
                if (EntryTypesearch.SelectedValue != "A")
                {
                    if (PubIDSearch.Text == "" && TextBoxWorkItemSearch.Text == "")
                    {
                        // SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,t.EntryName,TitleWorkItem,c.PubCatName,PublishJAMonth,PublishJAYear,ImpactFactor ,s.StatusName from Publication p,Status_Publication_M s , PubMUCategorization_M c ,PublicationTypeEntry_M t where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization and TypeOfEntry='" + EntryTypesearch.SelectedValue + "' and Status='SUB'  and Institution='" + a1.InstituteId + "' ";



                        SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,t.EntryName,TitleWorkItem,c.PubCatName,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName,ISStudentAuthor from Publication p,Status_Publication_M s , PubMUCategorization_M c ,PublicationTypeEntry_M t where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization  and  TypeOfEntry=@EntryTypesearch and Status='APP' and EntryType='F'  and Institution=@InstituteId  and Department=@Department_Id ";
                    }
                    else if (PubIDSearch.Text != "" && TextBoxWorkItemSearch.Text == "")
                    {

                        SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,t.EntryName,TitleWorkItem,c.PubCatName,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName,ISStudentAuthor from Publication p,Status_Publication_M s , PubMUCategorization_M c ,PublicationTypeEntry_M  t where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization and TypeOfEntry=@EntryTypesearch and Status='APP' and EntryType='F'  and PublicationID like  '%' + @PubIDSearch + '%' and Institution=@InstituteId and Department=@Department_Id ";
                        // SqlDataSource1.SelectCommand = "select j.Id as Id,Year,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,Comments from Journal_IF_Details j,Journal_M k where j.Id=k.Id and j.Id='" + txtJid.Text.Trim() + "'";
                        SqlDataSource1.SelectParameters.Add("PubIDSearch", PubIDSearch.Text.Trim());
                    }
                    else if (PubIDSearch.Text == "" && TextBoxWorkItemSearch.Text != "")
                    {

                        SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,t.EntryName,TitleWorkItem,c.PubCatName,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName,ISStudentAuthor from Publication p,Status_Publication_M s , PubMUCategorization_M c ,PublicationTypeEntry_M  t where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization and TypeOfEntry=@EntryTypesearch and Status='APP' and EntryType='F' and TitleWorkItem like '%' + @TextBoxWorkItemSearch  + '%' and Institution=@InstituteId and Department=@Department_Id ";
                        // SqlDataSource1.SelectCommand = "select j.Id as Id,Year,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,Comments from Journal_IF_Details j,Journal_M k where j.Id=k.Id and j.Id='" + txtJid.Text.Trim() + "'";
                        SqlDataSource1.SelectParameters.Add("TextBoxWorkItemSearch", TextBoxWorkItemSearch.Text.Trim());
                    }
                    else
                    {

                        SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,t.EntryName,TitleWorkItem,c.PubCatName,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName,ISStudentAuthor from Publication p,Status_Publication_M s , PubMUCategorization_M c ,PublicationTypeEntry_M  t where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization and TypeOfEntry=@EntryTypesearch and Status='APP'  and EntryType='F' and PublicationID like '%' + @PubIDSearch + '%' and TitleWorkItem like '%' + @TextBoxWorkItemSearch  + '%' and Institution=@InstituteId and Department=@Department_Id   ";
                        // SqlDataSource1.SelectCommand = "select j.Id as Id,Year,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,Comments from Journal_IF_Details j,Journal_M k where j.Id=k.Id and j.Id='" + txtJid.Text.Trim() + "'";
                        SqlDataSource1.SelectParameters.Add("TextBoxWorkItemSearch", TextBoxWorkItemSearch.Text.Trim());
                        SqlDataSource1.SelectParameters.Add("PubIDSearch", PubIDSearch.Text.Trim());
                    }

                    SqlDataSource1.SelectParameters.Add("EntryTypesearch", EntryTypesearch.SelectedValue);
                    SqlDataSource1.SelectParameters.Add("InstituteId", a.InstituteId);
                    SqlDataSource1.SelectParameters.Add("Department_Id", a.Department_Id);
                    GridViewSearch.DataBind();
                    SqlDataSource1.DataBind();
                }
                else
                {
                    if (PubIDSearch.Text == "" && TextBoxWorkItemSearch.Text == "")
                    {
                        //  SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor from Publication where   Status='APP' and SupervisorID='" + SupId + "' ";

                        SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,t.EntryName,TitleWorkItem,c.PubCatName,PublishJAMonth,PublishJAYear,ImpactFactor,ISStudentAuthor from Publication p, PubMUCategorization_M c ,PublicationTypeEntry_M t where   Status='APP' and EntryType='F' and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization and Institution=@InstituteId   and Department=@Department_Id   ";

                    }
                    else if (PubIDSearch.Text != "" && TextBoxWorkItemSearch.Text == "")
                    {

                        SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,t.EntryName,TitleWorkItem,c.PubCatName,PublishJAMonth,PublishJAYear,ImpactFactor,ISStudentAuthor from Publication p, PubMUCategorization_M c ,PublicationTypeEntry_M t where  Status='APP' and EntryType='F' and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization and PublicationID like '%' + @PubIDSearch + '%' and Institution=@InstituteId  and Department=@Department_Id  ";
                        // SqlDataSource1.SelectCommand = "select j.Id as Id,Year,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,Comments from Journal_IF_Details j,Journal_M k where j.Id=k.Id and j.Id='" + txtJid.Text.Trim() + "'";
                        SqlDataSource1.SelectParameters.Add("PubIDSearch", PubIDSearch.Text.Trim());
                    }
                    else if (PubIDSearch.Text == "" && TextBoxWorkItemSearch.Text != "")
                    {

                        SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,t.EntryName,TitleWorkItem,c.PubCatName,PublishJAMonth,PublishJAYear,ImpactFactor,ISStudentAuthor from Publication p, PubMUCategorization_M c ,PublicationTypeEntry_M t where  Status='APP' and EntryType='F'  and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization and TitleWorkItem like '%' + @TextBoxWorkItemSearch  + '%' and Institution=@InstituteId   and Department=@Department_Id  ";
                        // SqlDataSource1.SelectCommand = "select j.Id as Id,Year,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,Comments from Journal_IF_Details j,Journal_M k where j.Id=k.Id and j.Id='" + txtJid.Text.Trim() + "'";
                        SqlDataSource1.SelectParameters.Add("TextBoxWorkItemSearch", TextBoxWorkItemSearch.Text.Trim());
                    }
                    else
                    {

                        SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,t.EntryName,TitleWorkItem,c.PubCatName,PublishJAMonth,PublishJAYear,ImpactFactor,ISStudentAuthor from Publication p, PubMUCategorization_M c ,PublicationTypeEntry_M t where  Status='APP'  and EntryType='F' and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization and PublicationID like '%' + @PubIDSearch + '%' and TitleWorkItem like '%' + @TextBoxWorkItemSearch  + '%' and Institution=@InstituteId  and Department=@Department_Id   ";
                        // SqlDataSource1.SelectCommand = "select j.Id as Id,Year,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,Comments from Journal_IF_Details j,Journal_M k where j.Id=k.Id and j.Id='" + txtJid.Text.Trim() + "'";
                        SqlDataSource1.SelectParameters.Add("TextBoxWorkItemSearch", TextBoxWorkItemSearch.Text.Trim());
                        SqlDataSource1.SelectParameters.Add("PubIDSearch", PubIDSearch.Text.Trim());
                    
                    }
                    SqlDataSource1.SelectParameters.Add("InstituteId", a.InstituteId);
                    SqlDataSource1.SelectParameters.Add("Department_Id", a.Department_Id);
                    GridViewSearch.DataBind();
                    SqlDataSource1.DataBind();
                }
            }
        }
        else
        {
            ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please contact Admin!!!!')</script>");

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
    protected void NationalTypeOnSelectedIndexChanged(object sender, EventArgs e)
    {
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



    }

    protected void setModalWindow(object sender, EventArgs e)
    {
        //popupPanelJournal.Visible = true;
        //popGridJournal.DataSourceID = "SqlDataSourceJournal";
        //SqlDataSourceJournal.DataBind();
        //popGridJournal.DataBind();

        if (DropDownListPublicationEntry.SelectedValue == "JA" || DropDownListPublicationEntry.SelectedValue == "TS" || DropDownListPublicationEntry.SelectedValue == "PR")
        {
            //popupPanelAffil.Visible = true;
            popGridAffil.DataSourceID = "SqlDataSourceAffil";
            SqlDataSourceAffil.DataBind();
            popGridAffil.DataBind();
           // popupstudent.Visible = true;
            popupStudentGrid.DataSourceID = "StudentSQLDS";
            StudentSQLDS.DataBind();
            popupStudentGrid.DataBind();
        }
        else
        {
           // popupPanelAffil.Visible = true;
            popGridAffil.DataSourceID = "SqlDataSourceAffil";
            SqlDataSourceAffil.DataBind();
            popGridAffil.DataBind();
           // popupstudent.Visible = false;
            popupStudentGrid.DataSourceID = "StudentSQLDS";
            StudentSQLDS.DataBind();
            popupStudentGrid.DataBind();
        }


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

        TextBox employc = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("EmployeeCode");
        employc.Text = EmployeeCode1;

        TextBox AuthorName = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("AuthorName");
        //AuthorName.Text = a.UserName;
        AuthorName.Text = a.UserNamePrefix + " " + a.UserFirstName + " " + a.UserMiddleName + " " + a.UserLastName;

        journalcodeSrch.Text = "";
        popGridJournal.DataBind();


        TextBox NameInJournal = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("NameInJournal");
        NameInJournal.Text = a.UserNamePrefix + " " + a.UserFirstName + " " + a.UserMiddleName + " " + a.UserLastName;

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
            SqlDataSourceJournal.SelectParameters.Add("journalcodeSrch", journalcodeSrch.Text);

            SqlDataSourceJournal.SelectCommand = "SELECT Id,Title,AbbreviatedTitle FROM [Journal_M] where Title like '%' + @journalcodeSrch + '%'";
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
            CheckboxIndexAgency.Enabled = false;
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

    protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //Find the DropDownList in the Row
            DropDownList DropdownMuNonMu = (e.Row.FindControl("DropdownMuNonMu") as DropDownList);

            if (DropDownListPublicationEntry.SelectedValue == "BK"  || DropDownListPublicationEntry.SelectedValue == "NM")
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
        dt.Columns.Add(new DataColumn("DropdownStudentInstitutionName", typeof(string)));
        dt.Columns.Add(new DataColumn("DropdownStudentDepartmentName", typeof(string)));
        dt.Columns.Add(new DataColumn("NationalType", typeof(string)));
        dt.Columns.Add(new DataColumn("ContinentId", typeof(string)));

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
        dr["DropdownStudentInstitutionName"] = string.Empty;
        dr["DropdownStudentDepartmentName"] = string.Empty;
        dr["NationalType"] = string.Empty;
        dr["ContinentId"] = string.Empty;


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
        DropDownList NationalType = (DropDownList)Grid_AuthorEntry.Rows[0].Cells[0].FindControl("NationalType");
        DropDownList ContinentId = (DropDownList)Grid_AuthorEntry.Rows[0].Cells[0].FindControl("ContinentId");

        if (DropDownListPublicationEntry.SelectedValue == "BK" || DropDownListPublicationEntry.SelectedValue == "CP" || DropDownListPublicationEntry.SelectedValue == "NM")
        {


            SqlDataSourceAuthorType.SelectCommand = "SELECT Id,Type FROM [Author_Type_M] where (Id! = 'O') and (Id! = 'E')";

            DropdownMuNonMu.DataTextField = "Type";
            DropdownMuNonMu.DataValueField = "Id";
            DropdownMuNonMu.DataBind();

        }
        else if (DropDownListPublicationEntry.SelectedValue == "JA" || DropDownListPublicationEntry.SelectedValue == "TS" || DropDownListPublicationEntry.SelectedValue == "CP" || DropDownListPublicationEntry.SelectedValue == "PR")
        {
            SqlDataSourceAuthorType.SelectCommand = "SELECT Id,DisplayName FROM [Author_Type_M] where (Id = 'M') or (Id = 'S') or (Id = 'N') or (Id = 'O')or and (Id = 'E') ";

            DropdownMuNonMu.DataTextField = "DisplayName";
            DropdownMuNonMu.DataValueField = "Id";
            DropdownMuNonMu.DataBind();

        }

        DropdownMuNonMu.Enabled = false;
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
        //string dir_domain = ConfigurationManager.AppSettings["DirectoryDomain"].ToString();
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

        Grid_AuthorEntry.Columns[11].Visible = false;

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
        //Grid_AuthorEntry.DataBind();


    }

    protected void AuthorName_Changed(object sender, EventArgs e)
    {
       
        GridViewRow currentRow = (GridViewRow)((TextBox)sender).Parent.Parent;
        TextBox AuthorName = (TextBox)currentRow.FindControl("AuthorName");
        TextBox NameInJournal = (TextBox)currentRow.FindControl("NameInJournal");
        TextBox InstitutionName = (TextBox)currentRow.FindControl("InstitutionName");

        //InstitutionName.Focus();
        NameInJournal.Text = AuthorName.Text;



    }
    protected void DropdownMuNonMuOnSelectedIndexChanged(object sender, EventArgs e)
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
        if (DropDownListPublicationEntry.SelectedValue == "JA" || DropDownListPublicationEntry.SelectedValue == "TS" || DropDownListPublicationEntry.SelectedValue == "CP" || DropDownListPublicationEntry.SelectedValue == "PR")
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
        popupPanelJournal.Visible = false;
        popGridJournal.DataSourceID = "SqlDataSourceJournal";
        SqlDataSourceJournal.DataBind();
        popGridJournal.DataBind();

    }
    protected void addRow(object sender, EventArgs e)
    {


        //if (Grid_AuthorEntry.Rows.Count == 0)
        //{
        //    SetInitialRow();
        //}


        //else
        //{
        //    int rowIndex = 0;

        //    if (ViewState["CurrentTable"] != null)
        //    {
        //        DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
        //        DataRow drCurrentRow = null;
        //        if (dtCurrentTable.Rows.Count > 0)
        //        {
        //            for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
        //            {
        //                // TextBox Author = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("Author");
        //                TextBox AuthorName = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[1].FindControl("AuthorName");
        //                DropDownList DropdownMuNonMu = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[2].FindControl("DropdownMuNonMu");
        //                //TextBox amount = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[3].FindControl("amount");
        //                TextBox EmployeeCode = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("EmployeeCode");
        //                HiddenField Institution = (HiddenField)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("Institution");
        //                TextBox InstitutionName = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[6].FindControl("InstitutionName");
        //                HiddenField Department = (HiddenField)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("Department");
        //                TextBox DepartmentName = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("DepartmentName");
        //                ImageButton EmployeeCodeBtnImg = (ImageButton)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("EmployeeCodeBtn");


        //                DropDownList DropdownStudentInstitutionName = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("DropdownStudentInstitutionName");
        //                DropDownList DropdownStudentDepartmentName = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("DropdownStudentDepartmentName");


        //                TextBox MailId = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("MailId");
        //                DropDownList isCorrAuth = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("isCorrAuth");
        //                DropDownList AuthorType = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("AuthorType");

        //                TextBox NameInJournal = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("NameInJournal");

        //                DropDownList IsPresenter = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("IsPresenter");

        //                CheckBox HasAttented = (CheckBox)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("HasAttented");

        //                DropDownList NationalType = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("NationalType");
        //                DropDownList ContinentId = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("ContinentId");
        //                ImageButton ImageButton1 = (ImageButton)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("ImageButton1");


        //                drCurrentRow = dtCurrentTable.NewRow();

        //                //   dtCurrentTable.Rows[i - 1]["amount"] = amount.Text.Trim() == "" ? 0 : Convert.ToDecimal(amount.Text);
        //                dtCurrentTable.Rows[i - 1]["DropdownMuNonMu"] = DropdownMuNonMu.Text;
        //                // dtCurrentTable.Rows[i - 1]["Author"] = Author.Text;
        //                dtCurrentTable.Rows[i - 1]["AuthorName"] = AuthorName.Text;
        //                dtCurrentTable.Rows[i - 1]["EmployeeCode"] = EmployeeCode.Text;


        //                if (DropdownMuNonMu.Text == "M")
        //                {

        //                    DropdownStudentInstitutionName.Visible = false;
        //                    DropdownStudentDepartmentName.Visible = false;
        //                    InstitutionName.Visible = true;
        //                    DepartmentName.Visible = true;

        //                    EmployeeCodeBtnImg.Enabled = true;
        //                    EmployeeCode.Enabled = false;
        //                    NationalType.Visible = false;
        //                    ContinentId.Visible = false;

        //                    dtCurrentTable.Rows[i - 1]["NationalType"] = NationalType.SelectedValue;
        //                    dtCurrentTable.Rows[i - 1]["ContinentId"] = ContinentId.SelectedValue;
        //                    dtCurrentTable.Rows[i - 1]["Institution"] = Institution.Value;
        //                    dtCurrentTable.Rows[i - 1]["InstitutionName"] = InstitutionName.Text;
        //                    dtCurrentTable.Rows[i - 1]["Department"] = Department.Value;
        //                    dtCurrentTable.Rows[i - 1]["DepartmentName"] = DepartmentName.Text;
                        
        //                }
        //                else if (DropdownMuNonMu.Text == "N")
        //                {

        //                    DropdownStudentInstitutionName.Visible = false;
        //                    DropdownStudentDepartmentName.Visible = false;
        //                    InstitutionName.Visible = true;
        //                    DepartmentName.Visible = true;
        //                    EmployeeCode.Enabled = false;
        //                    EmployeeCodeBtnImg.Enabled = false;

        //                    NationalType.Visible = true;

        //                    if (NationalType.SelectedValue == "I")
        //                    {
        //                        ContinentId.Visible = true;
        //                    }
        //                    else
        //                    {
        //                        ContinentId.Visible = false;
        //                    }


        //                    dtCurrentTable.Rows[i - 1]["NationalType"] = NationalType.SelectedValue;
        //                    dtCurrentTable.Rows[i - 1]["ContinentId"] = ContinentId.SelectedValue;

        //                    dtCurrentTable.Rows[i - 1]["Institution"] = Institution.Value;
        //                    dtCurrentTable.Rows[i - 1]["InstitutionName"] = InstitutionName.Text;
        //                    dtCurrentTable.Rows[i - 1]["Department"] = Department.Value;
        //                    dtCurrentTable.Rows[i - 1]["DepartmentName"] = DepartmentName.Text;
        //                }


        //                else if (DropdownMuNonMu.Text == "O")
        //                {

        //                    EmployeeCode.Visible = true;
        //                    EmployeeCode.Enabled = true;
        //                    DropdownStudentInstitutionName.Visible = true;
        //                    DropdownStudentDepartmentName.Visible = true;
        //                    InstitutionName.Visible = false;
        //                    DepartmentName.Visible = false;
        //                    NationalType.Visible = false;
        //                    ContinentId.Visible = false;
        //                    AuthorName.Enabled = false;
        //                    EmployeeCodeBtnImg.Visible = true;
        //                    EmployeeCodeBtnImg.Enabled = false;


        //                    dtCurrentTable.Rows[i - 1]["Institution"] = DropdownStudentInstitutionName.SelectedValue;
        //                    dtCurrentTable.Rows[i - 1]["Department"] = DropdownStudentDepartmentName.SelectedValue;
        //                    dtCurrentTable.Rows[i - 1]["EmployeeCode"] = EmployeeCode.Text.Trim();

        //                    NationalType.Visible = false;
        //                    ContinentId.Visible = false;

        //                    dtCurrentTable.Rows[i - 1]["NationalType"] = NationalType.SelectedValue;
        //                    dtCurrentTable.Rows[i - 1]["ContinentId"] = ContinentId.SelectedValue;
        //                }

        //                else if (DropdownMuNonMu.Text == "S")
        //                {
        //                    if (DropDownListPublicationEntry.SelectedValue == "JA" || DropDownListPublicationEntry.SelectedValue == "TS")
        //                    {
        //                        DropdownStudentInstitutionName.Visible = false;
        //                        DropdownStudentDepartmentName.Visible = false;
        //                        EmployeeCodeBtnImg.Enabled = false;
        //                        EmployeeCodeBtnImg.Visible = false;
        //                        EmployeeCode.Enabled = false;
        //                        InstitutionName.Visible = true;
        //                        DepartmentName.Visible = true;
        //                        dtCurrentTable.Rows[i - 1]["Institution"] = Institution.Value;
        //                        dtCurrentTable.Rows[i - 1]["InstitutionName"] = InstitutionName.Text;
        //                        dtCurrentTable.Rows[i - 1]["Department"] = Department.Value;
        //                        dtCurrentTable.Rows[i - 1]["DepartmentName"] = DepartmentName.Text;
        //                        ImageButton1.Visible = true;
        //                    }
        //                    else
        //                    {
        //                        DropdownStudentInstitutionName.Visible = true;
        //                        DropdownStudentDepartmentName.Visible = true;
        //                        InstitutionName.Visible = false;
        //                        DepartmentName.Visible = false;
        //                        EmployeeCode.Enabled = false;
        //                        dtCurrentTable.Rows[i - 1]["Institution"] = DropdownStudentInstitutionName.SelectedValue;
        //                        dtCurrentTable.Rows[i - 1]["InstitutionName"] = DropdownStudentInstitutionName.SelectedItem;
        //                        dtCurrentTable.Rows[i - 1]["Department"] = DropdownStudentDepartmentName.SelectedValue;
        //                        dtCurrentTable.Rows[i - 1]["DepartmentName"] = DropdownStudentDepartmentName.SelectedItem;
        //                        EmployeeCodeBtnImg.Enabled = false;
        //                        EmployeeCodeBtnImg.Visible = true;
        //                        ImageButton1.Visible = false;

        //                    }

        //                    EmployeeCodeBtnImg.Enabled = false;

        //                    NationalType.Visible = false;
        //                    ContinentId.Visible = false;

        //                    dtCurrentTable.Rows[i - 1]["NationalType"] = NationalType.SelectedValue;
        //                    dtCurrentTable.Rows[i - 1]["ContinentId"] = ContinentId.SelectedValue;

        //                }

        //                dtCurrentTable.Rows[i - 1]["MailId"] = MailId.Text;
        //                dtCurrentTable.Rows[i - 1]["isCorrAuth"] = isCorrAuth.Text;
        //                dtCurrentTable.Rows[i - 1]["AuthorType"] = AuthorType.Text;
        //                dtCurrentTable.Rows[i - 1]["NameInJournal"] = NameInJournal.Text;
        //                dtCurrentTable.Rows[i - 1]["IsPresenter"] = IsPresenter.Text;

        //                if (HasAttented.Checked == true)
        //                {

        //                    dtCurrentTable.Rows[i - 1]["HasAttented"] = "Y";
        //                }
        //                else
        //                {
        //                    dtCurrentTable.Rows[i - 1]["HasAttented"] = "N";
        //                }

        //                rowIndex++;
        //            }

        //            dtCurrentTable.Rows.Add(drCurrentRow);
        //            //  var newRow = dtCurrentTable.NewRow();
        //            // dtCurrentTable.Rows.InsertAt(newRow, 0);

        //            ViewState["CurrentTable"] = dtCurrentTable;

        //            Grid_AuthorEntry.DataSource = dtCurrentTable;
        //            Grid_AuthorEntry.DataBind();



        //        }
        //    }
        //    else
        //    {
        //        Response.Write("ViewState is null");
        //    }

        //    SetPreviousData();
        //}


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

                                NationalType.Visible = false;
                                ContinentId.Visible = false;
                                dtCurrentTable.Rows[i - 1]["Institution"] = Institution.Value;
                                dtCurrentTable.Rows[i - 1]["InstitutionName"] = InstitutionName.Text;
                                dtCurrentTable.Rows[i - 1]["Department"] = Department.Value;
                                dtCurrentTable.Rows[i - 1]["DepartmentName"] = DepartmentName.Text;
                                ImageButton1.Visible = true;
                                ImageButton1.Enabled = true;
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

                            //NationalType.Visible = false;
                            //ContinentId.Visible = false;

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



    private void SetPreviousData()
    {


        //int rowIndex = 0;
        //if (ViewState["CurrentTable"] != null)
        //{
        //    DataTable dt = (DataTable)ViewState["CurrentTable"];
        //    if (dt.Rows.Count > 0)
        //    {
        //        for (int i = 0; i < dt.Rows.Count; i++)
        //        {


        //            //  TextBox Author = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("Author");
        //            TextBox AuthorName = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[1].FindControl("AuthorName");
        //            DropDownList DropdownMuNonMu = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[2].FindControl("DropdownMuNonMu");
        //            ImageButton EmployeeCodeBtnImg = (ImageButton)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("EmployeeCodeBtn");

        //            //TextBox amount = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[3].FindControl("amount");
        //            TextBox EmployeeCode = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("EmployeeCode");
        //            HiddenField Institution = (HiddenField)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("Institution");
        //            TextBox InstitutionName = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[6].FindControl("InstitutionName");
        //            HiddenField Department = (HiddenField)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("Department");
        //            TextBox DepartmentName = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("DepartmentName");
        //            TextBox MailId = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("MailId");

        //            DropDownList isCorrAuth = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("isCorrAuth");

        //            DropDownList AuthorType = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("AuthorType");
        //            TextBox NameInJournal = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("NameInJournal");

        //            DropDownList IsPresenter = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[2].FindControl("IsPresenter");
        //            CheckBox HasAttented = (CheckBox)Grid_AuthorEntry.Rows[rowIndex].Cells[2].FindControl("HasAttented");

        //            DropDownList NationalType = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("NationalType");
        //            DropDownList ContinentId = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("ContinentId");


        //            DropDownList DropdownStudentInstitutionName = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("DropdownStudentInstitutionName");
        //            DropDownList DropdownStudentDepartmentName = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("DropdownStudentDepartmentName");



        //            //  TextBox Author1 = (TextBox)Grid_AuthorEntry.Rows[0].Cells[0].FindControl("Author");
        //            TextBox AuthorName1 = (TextBox)Grid_AuthorEntry.Rows[0].Cells[1].FindControl("AuthorName");
        //            DropDownList DropdownMuNonMu1 = (DropDownList)Grid_AuthorEntry.Rows[0].Cells[2].FindControl("DropdownMuNonMu");
        //            ImageButton EmployeeCodeBtnImg1 = (ImageButton)Grid_AuthorEntry.Rows[0].Cells[0].FindControl("EmployeeCodeBtn");

        //            //TextBox amount = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[3].FindControl("amount");
        //            //HiddenField EmployeeCode1 = (HiddenField)Grid_AuthorEntry.Rows[0].Cells[0].FindControl("EmployeeCode");
        //            HiddenField Institution1 = (HiddenField)Grid_AuthorEntry.Rows[0].Cells[0].FindControl("Institution");
        //            TextBox InstitutionName1 = (TextBox)Grid_AuthorEntry.Rows[0].Cells[6].FindControl("InstitutionName");
        //            HiddenField Department1 = (HiddenField)Grid_AuthorEntry.Rows[0].Cells[0].FindControl("Department");
        //            TextBox DepartmentName1 = (TextBox)Grid_AuthorEntry.Rows[0].Cells[0].FindControl("DepartmentName");
        //            TextBox MailId1 = (TextBox)Grid_AuthorEntry.Rows[0].Cells[0].FindControl("MailId");

        //            DropDownList isCorrAuth1 = (DropDownList)Grid_AuthorEntry.Rows[0].Cells[0].FindControl("isCorrAuth");

        //            DropDownList AuthorType1 = (DropDownList)Grid_AuthorEntry.Rows[0].Cells[0].FindControl("AuthorType");
        //            TextBox NameInJournal1 = (TextBox)Grid_AuthorEntry.Rows[0].Cells[0].FindControl("NameInJournal");

        //            DropDownList IsPresenter1 = (DropDownList)Grid_AuthorEntry.Rows[0].Cells[2].FindControl("IsPresenter");
        //            CheckBox HasAttented1 = (CheckBox)Grid_AuthorEntry.Rows[0].Cells[2].FindControl("HasAttented");
        //            DropDownList DropdownStudentInstitutionName1 = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("DropdownStudentInstitutionName");
        //            DropDownList DropdownStudentDepartmentName1 = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("DropdownStudentDepartmentName");
        //            ImageButton ImageButton1 = (ImageButton)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("ImageButton1");




        //            if (DropDownListPublicationEntry.SelectedValue == "JA" || DropDownListPublicationEntry.SelectedValue == "TS")
        //            {
        //                isCorrAuth.Visible = true;
        //                AuthorType.Visible = true;
        //                NameInJournal.Visible = true;
        //                Grid_AuthorEntry.Columns[6].Visible = true;
        //                Grid_AuthorEntry.Columns[7].Visible = true;
        //                Grid_AuthorEntry.Columns[8].Visible = true;
        //                Grid_AuthorEntry.Columns[9].Visible = false;
        //                Grid_AuthorEntry.Columns[10].Visible = false;
        //            }

        //            else if (DropDownListPublicationEntry.SelectedValue == "CP")
        //            {
        //                isCorrAuth.Visible = true;
        //                AuthorType.Visible = true;
        //                NameInJournal.Visible = true;
        //                Grid_AuthorEntry.Columns[6].Visible = false;
        //                Grid_AuthorEntry.Columns[7].Visible = false;
        //                Grid_AuthorEntry.Columns[8].Visible = false;
        //                Grid_AuthorEntry.Columns[9].Visible = true;
        //                Grid_AuthorEntry.Columns[10].Visible = true;
        //            }
        //            else
        //            {
        //                isCorrAuth.Visible = false;
        //                AuthorType.Visible = false;
        //                NameInJournal.Visible = false;
        //                Grid_AuthorEntry.Columns[6].Visible = false;
        //                Grid_AuthorEntry.Columns[7].Visible = false;
        //                Grid_AuthorEntry.Columns[8].Visible = false;
        //                Grid_AuthorEntry.Columns[9].Visible = false;
        //                Grid_AuthorEntry.Columns[10].Visible = false;
        //            }


        //            //  amount.Text = dt.Rows[i]["amount"].ToString();
        //            DropdownMuNonMu.Text = dt.Rows[i]["DropdownMuNonMu"].ToString();
        //            //  Author.Text = dt.Rows[i]["Author"].ToString();
        //            AuthorName.Text = dt.Rows[i]["AuthorName"].ToString();
        //            EmployeeCode.Text = dt.Rows[i]["EmployeeCode"].ToString();

        //            if (DropdownMuNonMu.Text == "M")
        //            {
        //                AuthorName.Enabled = false;
        //                InstitutionName.Enabled = false;
        //                DepartmentName.Enabled = false;
        //                MailId.Enabled = false;
        //                EmployeeCode.Enabled = false;
        //                EmployeeCodeBtnImg.Enabled = true;

        //                DropdownStudentInstitutionName.Visible = false;
        //                DropdownStudentDepartmentName.Visible = false;
        //                InstitutionName.Visible = true;
        //                DepartmentName.Visible = true;

        //                NationalType.Visible = false;
        //                ContinentId.Visible = false;



        //                NationalType.Text = dt.Rows[i]["NationalType"].ToString();
        //                ContinentId.Text = dt.Rows[i]["ContinentId"].ToString();
        //                EmployeeCode.Text = dt.Rows[i]["EmployeeCode"].ToString();
        //                Institution.Value = dt.Rows[i]["Institution"].ToString();
        //                InstitutionName.Text = dt.Rows[i]["InstitutionName"].ToString();
        //                Department.Value = dt.Rows[i]["Department"].ToString();
        //                DepartmentName.Text = dt.Rows[i]["DepartmentName"].ToString();
        //            }

        //            else if (DropdownMuNonMu.Text == "N")
        //            {
        //                AuthorName.Enabled = true;
        //                InstitutionName.Enabled = true;
        //                DepartmentName.Enabled = true;
        //                MailId.Enabled = true;

        //                EmployeeCodeBtnImg.Enabled = false;

        //                DropdownStudentInstitutionName.Visible = false;
        //                DropdownStudentDepartmentName.Visible = false;
        //                InstitutionName.Visible = true;
        //                DepartmentName.Visible = true;
        //                EmployeeCode.Enabled = false;
        //                NationalType.Visible = true;

        //                NationalType.Text = dt.Rows[i]["NationalType"].ToString();
        //                ContinentId.Text = dt.Rows[i]["ContinentId"].ToString();


        //                if (NationalType.Text == "I")
        //                {
        //                    ContinentId.Visible = true;
        //                }
        //                else
        //                {
        //                    ContinentId.Visible = false;
        //                }


        //                Institution.Value = dt.Rows[i]["Institution"].ToString();
        //                InstitutionName.Text = dt.Rows[i]["InstitutionName"].ToString();
        //                Department.Value = dt.Rows[i]["Department"].ToString();
        //                DepartmentName.Text = dt.Rows[i]["DepartmentName"].ToString();
        //            }

        //            else if (DropdownMuNonMu1.SelectedValue == "O")
        //                {
        //                    EmployeeCode.Visible = true;
        //                    EmployeeCode.Enabled = true;
        //                    AuthorName.Enabled = true;
        //                    InstitutionName.Enabled = false;
        //                    DepartmentName.Enabled = false;
        //                    MailId.Enabled = true;
        //                    AuthorName.Enabled = true;
        //                    EmployeeCodeBtnImg.Enabled = false;
        //                    EmployeeCodeBtnImg1.Visible = true;
        //                    DropdownStudentInstitutionName.Visible = true;
        //                    DropdownStudentDepartmentName.Visible = true;
        //                    InstitutionName.Visible = false;
        //                    DepartmentName.Visible = false;
        //                    EmployeeCode.Text = dt.Rows[i]["EmployeeCode"].ToString();
        //                    Institution.Value = dt.Rows[i]["Institution"].ToString();
        //                    InstitutionName.Text = dt.Rows[i]["InstitutionName"].ToString();
        //                    Department.Value = dt.Rows[i]["Department"].ToString();
        //                    DepartmentName.Text = dt.Rows[i]["DepartmentName"].ToString();

        //                }
        //                else if (DropdownMuNonMu.Text == "S")
        //                {
        //                    if (DropDownListPublicationEntry.SelectedValue == "JA" || DropDownListPublicationEntry.SelectedValue == "TS")
        //                    {

        //                        AuthorName.Enabled = false;
        //                        DropdownStudentInstitutionName.Visible = false;
        //                        DropdownStudentDepartmentName.Visible = false;
        //                        InstitutionName.Enabled = false;
        //                        DepartmentName.Enabled = false;
        //                        EmployeeCodeBtnImg.Enabled = false;
        //                        EmployeeCodeBtnImg.Visible = false;
        //                        EmployeeCode.Enabled = false;
        //                        ImageButton1.Visible = true;
        //                        Institution.Value = dt.Rows[i]["Institution"].ToString();
        //                        InstitutionName.Text = dt.Rows[i]["InstitutionName"].ToString();
        //                        Department.Value = dt.Rows[i]["Department"].ToString();
        //                        DepartmentName.Text = dt.Rows[i]["DepartmentName"].ToString();
        //                    }
        //                    else
        //                    {
        //                        EmployeeCode.Enabled = false;
        //                        AuthorName.Enabled = true;
        //                        DropdownStudentInstitutionName.Visible = true;
        //                        DropdownStudentDepartmentName.Visible = true;
        //                        EmployeeCodeBtnImg.Enabled = false;
        //                        InstitutionName.Visible = false;
        //                        DepartmentName.Visible = false;
        //                        Institution.Value = dt.Rows[i]["Institution"].ToString();
        //                        DropdownStudentInstitutionName.SelectedValue = dt.Rows[i]["Institution"].ToString();
        //                        Department.Value = dt.Rows[i]["Department"].ToString();
        //                        DropdownStudentDepartmentName.SelectedValue = dt.Rows[i]["Department"].ToString();
        //                    }
        //                    MailId.Enabled = true;
        //                    NationalType.Visible = false;
        //                    ContinentId.Visible = false;
        //                    EmployeeCode.Enabled = false;
        //                    NationalType.Text = dt.Rows[i]["NationalType"].ToString();
        //                    ContinentId.Text = dt.Rows[i]["ContinentId"].ToString();
        //                    ////  Institution.Value = dt.Rows[i]["Institution"].ToString();
        //                    //DropdownStudentInstitutionName.SelectedValue = dt.Rows[i]["Institution"].ToString();
        //                    ////  Department.Value = dt.Rows[i]["Department"].ToString();
        //                    //DropdownStudentDepartmentName.SelectedValue = dt.Rows[i]["Department"].ToString();
        //                }
        //            AuthorName1.Enabled = false;

        //            EmployeeCodeBtnImg1.Enabled = false;
        //            DropdownMuNonMu1.Enabled = false;

        //            // EmployeeCode1.Enabled = false;
        //            //  Institution1.Enabled = false;
        //            InstitutionName1.Enabled = false;

        //            //  Department1.Enabled = false;
        //            DepartmentName1.Enabled = false;
        //            MailId1.Enabled = false;
        //            //  isCorrAuth1.Enabled = false;
        //            // AuthorType1.Enabled = false;
        //            // NameInJournal1.Enabled = false;

        //            MailId.Text = dt.Rows[i]["MailId"].ToString();
        //            isCorrAuth.Text = dt.Rows[i]["isCorrAuth"].ToString();
        //            AuthorType.Text = dt.Rows[i]["AuthorType"].ToString();
        //            NameInJournal.Text = dt.Rows[i]["NameInJournal"].ToString();
        //            //EmployeeCode.Text = dt.Rows[i]["EmployeeCode"].ToString();

        //            IsPresenter.Text = dt.Rows[i]["IsPresenter"].ToString();
        //            if (dt.Rows[i]["HasAttented"].ToString() == "Y")
        //            {
        //                HasAttented.Checked = true;
        //            }
        //            else
        //            {
        //                HasAttented.Checked = false;
        //            }


        //            if (rowIndex == 0)
        //            {
        //                // Grid_AuthorEntry.Columns[11].Visible = false;
        //                // Grid_AuthorEntry.Rows[rowIndex].Cells[11].Visible = false;
        //            }
        //            else
        //            {
        //                //Grid_AuthorEntry.Columns[11].Visible = true;
        //                // Grid_AuthorEntry.Rows[rowIndex].Cells[11].Visible = true;
        //            }

        //            if (DropDownListPublicationEntry.SelectedValue == "JA" || DropDownListPublicationEntry.SelectedValue == "TS")
        //            {
        //                if (DropdownMuNonMu.Text == "M")
        //                {
        //                    EmployeeCodeBtnImg1.Enabled = true;
        //                }
        //                else if (DropdownMuNonMu.Text == "N")
        //                {
        //                    EmployeeCodeBtnImg1.Enabled = false;
        //                }
        //                else if (DropdownMuNonMu.Text == "S")
        //                {
        //                    EmployeeCodeBtnImg1.Enabled = false;
        //                }
        //                else if (DropdownMuNonMu.Text == "O")
        //                {
        //                    EmployeeCodeBtnImg1.Enabled = false;
        //                }

        //                DropdownMuNonMu1.Enabled = true;
        //            }
        //            else
        //            {
        //                DropdownMuNonMu1.Enabled = false;
        //            }




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
                        EmployeeCode.Enabled = true;
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

                    if (DropDownListPublicationEntry.SelectedValue == "JA" || DropDownListPublicationEntry.SelectedValue == "TS" || DropDownListPublicationEntry.SelectedValue == "CP" || DropDownListPublicationEntry.SelectedValue == "PR")
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
                    TextBox EmployeeCode = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("EmployeeCode");
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


                    DropDownList NationalType = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("NationalType");
                    DropDownList ContinentId = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("ContinentId");

                    DropDownList DropdownStudentInstitutionName = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("DropdownStudentInstitutionName");
                    DropDownList DropdownStudentDepartmentName = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("DropdownStudentDepartmentName");
                    ImageButton EmployeeCodeBtnImg1 = (ImageButton)Grid_AuthorEntry.Rows[0].Cells[0].FindControl("EmployeeCodeBtn");


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
                        DropdownStudentInstitutionName.Visible = true;
                        DropdownStudentDepartmentName.Visible = true;
                        InstitutionName.Visible = false;
                        DepartmentName.Visible = false;
                        EmployeeCodeBtnImg1.Enabled = false;
                        EmployeeCode.Enabled = false;

                        NationalType.Visible = false;
                        ContinentId.Visible = false;


                        dtCurrentTable.Rows[i - 1]["NationalType"] = NationalType.Text;
                        dtCurrentTable.Rows[i - 1]["ContinentId"] = ContinentId.Text;

                        //dtCurrentTable.Rows[i - 1]["EmployeeCode"] = EmployeeCode.Text;
                        dtCurrentTable.Rows[i - 1]["Institution"] = DropdownStudentInstitutionName.SelectedValue;
                        // dtCurrentTable.Rows[i - 1]["InstitutionName"] = DropdownStudentInstitutionName.SelectedItem;
                        dtCurrentTable.Rows[i - 1]["Department"] = DropdownStudentDepartmentName.SelectedValue;
                        //dtCurrentTable.Rows[i - 1]["DepartmentName"] = DropdownStudentDepartmentName.SelectedItem;
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
            TextBoxImpFact.Text = "";
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

    protected void txtboxYear_TextChanged(object sender, EventArgs e)
    {
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

        //popupPanelAffil.Visible = true;
        //btnSave.Enabled = true;
        //SetInitialRow();
        //Grid_AuthorEntry.Visible = true;


        TextBoxYearJA.Items.Clear();
        TextBoxYear.Items.Clear();

        if (DropDownListPublicationEntry.SelectedValue == "JA")
        {
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
            // TextBoxYearJA.SelectedValue = PublicationYearwebConfig;

            //panelJournalArticle.Visible = true;
            //panelConfPaper.Visible = false;
            //panelTechReport.Visible = true;
            //panelBookPublish.Visible = false;
            //panelOthes.Visible = false;
            //panAddAuthor.Visible = true;
        }
        else if (DropDownListPublicationEntry.SelectedValue == "PR")
        {
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
            // TextBoxYearJA.SelectedValue = PublicationYearwebConfig;

            //panelJournalArticle.Visible = true;
            //panelConfPaper.Visible = false;
            //panelTechReport.Visible = true;
            //panelBookPublish.Visible = false;
            //panelOthes.Visible = false;
            //panAddAuthor.Visible = true;
        }
        else if (DropDownListPublicationEntry.SelectedValue == "CP")
        {
            //panelConfPaper.Visible = true;

            //panelJournalArticle.Visible = false;

            //panelTechReport.Visible = true;
            //panelBookPublish.Visible = false;
            //panelOthes.Visible = false;
            //panAddAuthor.Visible = true;
        }
        else if (DropDownListPublicationEntry.SelectedValue == "TS")
        {
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
            //TextBoxYearJA.SelectedValue = PublicationYearwebConfig;

            //panelTechReport.Visible = true;

            //panelConfPaper.Visible = false;

            //panelJournalArticle.Visible = true;


            //panelBookPublish.Visible = false;
            //panelOthes.Visible = false;
            //panAddAuthor.Visible = true;
        }
        else if (DropDownListPublicationEntry.SelectedValue == "BK")
        {
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
            // TextBoxYear.SelectedValue = PublicationYearwebConfig;

            //panelBookPublish.Visible = true;

            //panelTechReport.Visible = true;

            //panelConfPaper.Visible = false;

            //panelJournalArticle.Visible = false;

            //panelOthes.Visible = false;
            //panAddAuthor.Visible = true;
        }
        else if (DropDownListPublicationEntry.SelectedValue == "NM")
        {
            //panelOthes.Visible = true;
            //panelBookPublish.Visible = false;

            //panelTechReport.Visible = true;

            //panelConfPaper.Visible = false;

            //panelJournalArticle.Visible = false;
            //panAddAuthor.Visible = true;


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



    //            }
    //        }


    //    }
    //    j.FilePath = filelocationpath;
    //    j.PaublicationID = TextBoxPubId.Text.Trim();
    //    j.TypeOfEntry = DropDownListPublicationEntry.SelectedValue;

    //    j.ApprovedBy = Session["UserId"].ToString();
    //    j.ApprovedDate = DateTime.Now;

    //   // j.AutoAppoval = Session["AutoApproval"].ToString();

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


    //   // string dir_domain = ConfigurationManager.AppSettings["DirectoryDomain"].ToString();
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

    //        //ButtonSub.Enabled = false;
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
        string AppendInstitutionNamess = null;
        int countConfAttended = 0;
        int countConfPresent = 0;
        int countAuthType = 0;
        int countCorrAuth = 0;
        try
        {
            if (TextBoxRemarks.Text == "")
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please Enter Remarks!')</script>");

                return;
            }

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
                    //   ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('File is Already uploaded!')</script>");

                    //    return; 
                    //}
                }
                //}
            }

           
            if (TextBoxPageFrom.Text != "")
            {
                if (TextBoxPageTo.Text == "")
                {
                    ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please enter page to')</script>");
                    return;
                }
            }
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
            string fiveyearImpFact = TextBoxImpFact5.Text;
            string ifapplicableyear = txtIFApplicableYear.Text;
            //string citationurl = txtCitation.Text;

            string EventTitle = TextBoxEventTitle.Text;

            string Place = TextBoxPlace.Text;

            string Date = TextBoxDate.Text;

            string TitleBook = TextBoxTitleBook.Text;
            // string TitleBook = TextBoxChapterContributed.Text;
            //string ofURL = TextBoxURL.Text.Trim();


            string ChapterContributed = TextBoxChapterContributed.Text;

            string Edition = TextBoxEdition.Text;

            string Publisher = TextBoxPublisher.Text;

            string Year = TextBoxYearJA.Text;

            string BookYear = TextBoxYear.SelectedValue;
            string BookMonth = DropDownListBookMonth.SelectedValue;

            string PageNum = TextBoxPageNum.Text;

            string Volume1 = TextBoxVolume1.Text;


            string Publish = TextBoxNewsPublish.Text;
            string issue = TextBoxIssue.Text;

            string DateOfPublish = TextBoxDateOfNewsPublish.Text;

            string PageNumNewsPaper = TextBoxPageNumNewsPaper.Text;


            string DOINum = TextBoxDOINum.Text;

            string Keywords = TextBoxKeywords.Text;

            string Abstract = TextBoxAbstract.Text;

            //  string Reference = TextBoxReference.Text;


            //string UploadPdf = "";

            // string Status = "NEW";
            string isERF = DropDownListErf.SelectedValue;
            string BISBN = txtbISBN.Text;
            string BSection = txtbSection.Text;
            string BChapter = txtChapter.Text;
            string BCountry = txtCountry.Text;
            string BTypeofPublication = DropDownListBookPublicationType.SelectedValue;
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
            j.Issue = issue;
            j.Indexed = Indexed;
            j.IndexedIn = IndexAgency;
            j.Publicationtype = PubType;

            j.ImpactFactor = ImpFact;
            j.ImpactFactor5 = TextBoxImpFact5.Text;
            if (txtIFApplicableYear.Text.Trim() != "")
            {
                j.IFApplicableYear = Convert.ToInt16(txtIFApplicableYear.Text.Trim());
            }
            //j.CitationUrl = citationurl;
            if (DropDownListPublicationEntry.SelectedValue == "CP")
            {
                j.ConfISBN = TextBoxIsbn.Text.Trim();
            }
            else if (DropDownListPublicationEntry.SelectedValue == "BK")
            {
                j.ConfISBN = BISBN;
            }
            j.ConferenceTitle = EventTitle;
            j.Place = Place;
            if (TextBoxDate.Text != "")
            {
                j.Date = Convert.ToDateTime(Date);
            }
            j.TitleOfBook = TitleBook;
            j.TitileOfBookChapter = ChapterContributed;
            j.Edition = Edition;
            j.Publisher = Publisher;
            j.BookPublishYear = BookYear;
            if (BookMonth != "")
            {
                j.BookPublishMonth = Convert.ToInt16(BookMonth);
            }
            j.BookPageNum = PageNum;

            j.BookVolume = Volume1;

            j.PostApprvRemarks = TextBoxRemarks.Text.Trim();

            j.BookVolume = Volume1;
            j.BookSection = BSection;
            j.BookChapter = BChapter;
            j.BookCountry = BCountry;
            j.BookTypeofPublication = BTypeofPublication;
            //j.url = ofURL;
            j.DOINum = DOINum;
            j.Keywords = Keywords;
            j.Abstract = Abstract;


            j.CreatedBy = Session["UserId"].ToString();
            j.CreatedDate = DateTime.Now;
            j.ApprovedBy = Session["UserId"].ToString();
            j.ApprovedDate = DateTime.Now;
            j.isERF = DropDownListErf.SelectedValue;
            j.TitileOfChapter = "";
            //  j.SupervisorID = Session["SupervisorId"].ToString();
            // j.AutoAppoval = Session["AutoApproval"].ToString();



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
            if (TextBoxCreditPoint.Text == "")
            {
                TextBoxCreditPoint.Text = "0";
            }

            if (DropDownListPublicationEntry.SelectedValue == "NM")
            {
                j.NewsPublisher = TextBoxNewsPublish.Text.Trim();
                string date2 = TextBoxDateOfNewsPublish.Text.Trim();
                j.NewsPublishedDate = Convert.ToDateTime(date2);
                j.NewsPageNum = TextBoxPageNumNewsPaper.Text.Trim();

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
                    if (DropDownListPublicationEntry.SelectedValue == "JA" || DropDownListPublicationEntry.SelectedValue == "TS" || DropDownListPublicationEntry.SelectedValue == "BK" || DropDownListPublicationEntry.SelectedValue == "PR")
                    {
                        JD[i].AuthorType = AuthorType.Text.Trim();
                    }
                    else
                    {
                        JD[i].AuthorType = "A";
                    }
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



                //for (int i = 0; i < JD.Length; i++)
                //{
                //    if (AppendInstitutionNamess == null)
                //    {
                //        JD[i].AppendInstitutionNames = JD[i].InstitutionName;
                //        AppendInstitutionNamess = JD[i].InstitutionName;
                //        j.AppendInstitutionNames = JD[i].AppendInstitutionNames;
                //        JD[i].AppendInstitutions = JD[i].Institution;
                //        j.AppendInstitutions = JD[i].AppendInstitutions;

                //    }
                //    else
                //    {
                //        for (int l = 0; l < JD.Length - i; l++)
                //        {

                //            //  if (JD[l].AppendInstitutionNames.Contains(JD[i].InstitutionName))
                //            if (JD[l].AppendInstitutions.Contains(JD[i].Institution))
                //            {

                //                //  JD[i].AppendInstitutionNames = JD[i - 1].AppendInstitutionNames + ',' + InstitutionName.Text.Trim();
                //            }
                //            else
                //            {
                //                JD[i].AppendInstitutionNames = JD[i - 1].AppendInstitutionNames + ',' + JD[i].InstitutionName;
                //            }

                //        }

                //    }
                //    j.AppendInstitutionNames = JD[i].AppendInstitutionNames;
                //    j.AppendInstitutions = JD[i].AppendInstitutions;

                //}

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

                if (countCorrAuth > 1)
                {
                    ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Corresponding Author cannot be more than once!')</script>");
                    return;

                }

                if (countCorrAuth == 0)
                {
                    ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Select atleast one Corresponding Author!')</script>");
                    return;

                }
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


            // j.AutoAppoval = Session["AutoApproval"].ToString();
            if (DropDownListPublicationEntry.SelectedValue == "JA" || DropDownListPublicationEntry.SelectedValue == "TS" || DropDownListPublicationEntry.SelectedValue == "PR")
            {
                Business b1 = new Business();
                ArrayList list = new ArrayList();
                PublishData data = new PublishData();
                data = b1.SelectDefaultAuthor(j);


                for (int i = 0; i < JD.Length; i++)
                {
                    list.Add(JD[i].EmployeeCode);
                }
                if (!list.Contains(data.CreatedBy))
                {
                    ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Authors list should contain Submitted Faculty')</script>");
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
            int result = 0;

            result = B.UpdatePostApprovePublishEntry(j, JD, listIndexAgency);


            if (result == 1)
            {

                DSforgridview.SelectParameters.Clear();
                DSforgridview.SelectParameters.Add("PaublicationID", j.PaublicationID);
                DSforgridview.SelectParameters.Add("TypeOfEntry", j.TypeOfEntry);


                DSforgridview.SelectCommand = "select UploadPDFPath from Publication where PublicationID=@PaublicationID and TypeOfEntry=@TypeOfEntry ";
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
                EmailDetails details = new EmailDetails();
                details = SendMail();
                details.Id = TextBoxPubId.Text;
                details.Type = DropDownListPublicationEntry.SelectedValue;
                SendMailObject obj = new SendMailObject();
                bool result1 = obj.InsertIntoEmailQueue(details);
                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Publication data Post Approved Successfully!')</script>");



            }
            else
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Error!!!!!!!!!!!!')</script>");

            }
        }



        catch (Exception ex)
        {
            log.Error("Inside Catch Block Of Publication" + ex.Message + " With UserID :" + Session["UserId"].ToString());

            log.Error(ex.StackTrace);


            ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Publication Post Approval Failed!!!!!!!!!!!!')</script>");

        }

    }

    public EmailDetails SendMail()
    {
        log.Debug("Publication Post Approval: Inside Send Mail function of Entry Type : " + DropDownListPublicationEntry.SelectedValue + " ID " + TextBoxPubId.Text);
        EmailDetails details = new EmailDetails();

        Business b = new Business();
        ArrayList myArrayList = new ArrayList();
        ArrayList myArrayList1 = new ArrayList();
        ArrayList myArrayList2 = new ArrayList();

        DataSet ds = new DataSet();
        Business bus = new Business();

        ds = bus.getAuthorList(TextBoxPubId.Text, DropDownListPublicationEntry.SelectedValue);

        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            myArrayList.Add(ds.Tables[0].Rows[i]["EmailId"].ToString());
        }
        string Supervisor = null;
        string Supervisormail = null;
        string Supervisormailid = null;

        for (int i = 0; i < myArrayList.Count; i++)
        {
            string author = myArrayList[i].ToString();

            string[] author1;
            author1 = author.Split(new[] { "@" }, StringSplitOptions.RemoveEmptyEntries);
            // string[] author1=author.Split(' ');
            if (author != "")
            {
                Supervisor = b.GetAuthorsSupervisor(author1[0]);

                string inst = Session["InstituteId"].ToString();
                string dept = Session["Department"].ToString();
                string uid = Session["UserId"].ToString();
                Supervisormail = b.GetSupId(inst, uid, dept);
                Supervisormailid = b.GetAuthorsSupervisorgetMail(Supervisormail);
                if (Supervisormailid != null)
                {
                    myArrayList1.Add(Supervisormailid);
                }
            }
        }

        DataSet dy = new DataSet();
        dy = bus.getAuthorListName(TextBoxPubId.Text, DropDownListPublicationEntry.SelectedValue);
        for (int i = 0; i < dy.Tables[0].Rows.Count; i++)
        {
            myArrayList2.Add(dy.Tables[0].Rows[i]["AuthorName"].ToString());
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
                if (auhtorsS != null)
                    if (auhtorsS != "")
                    {
                        auhtorsSConc = auhtorsSConc + con + auhtorsS;
                    }
            }

        }

        try
        {
            details.EmailSubject = "Publication Entry <  " + DropDownListPublicationEntry.SelectedValue + " _ " + TextBoxPubId.Text + "  > Modified ";
            string FooterText = ConfigurationManager.AppSettings["FooterText"].ToString();


            details.MsgBody = "<span style=\"font-size: 10pt; color: #3300cc; font-family: Verdana\"><h4>Dear Sir/Madam,</h4> <br>" +
                  "<b> The following Publication Entry has been modified in Publication Repository : <br> " +
                  "<br>" +


                    "Modification Remark : " + "\"" + TextBoxRemarks.Text.Trim() + "\"" + "<br>" +
                 "Modified By  : " + Session["UserName"].ToString() + "<br>" +
                    "Publication Type  : " + DropDownListPublicationEntry.SelectedItem + "<br>" +
                 "Publication Id  :  " + TextBoxPubId.Text.Trim() + "<br>" +

                 "Title of Work Item  : " + txtboxTitleOfWrkItem.Text.Trim() + "<br>" +
                 "Authors  : " + auhtorsSConc + "<br>" + "<br>" + "<br>" + "<br>" + "<br>" + FooterText +



                 " </b><br><b> </b></span>";

            details.FromEmail = ConfigurationManager.AppSettings["FromAddress"].ToString();
            details.Module = "PPAPP";
            for (int i = 0; i < myArrayList.Count; i++)
            {

                string email = myArrayList[i].ToString();
                if (details.ToEmail != null)
                {
                    details.ToEmail = details.ToEmail + ',' + myArrayList[i].ToString();
                }
                else
                {
                    if (i == 0)
                    {
                        details.ToEmail = myArrayList[i].ToString();
                    }
                    else
                    {
                        details.ToEmail = details.ToEmail + ',' + myArrayList[i].ToString();
                    }
                    // details.ToEmail = email;
                }
                log.Info(" Email will be sent to authors '" + i + "' : '" + myArrayList[i] + "' ");
            }

            for (int i = 0; i < myArrayList1.Count; i++)
            {
                // Msg.To.Add(BuyerId_Array[0]+dir_domain);
                string email = myArrayList1[i].ToString();
                if (details.ToEmail != null)
                {
                    details.ToEmail = details.ToEmail + ',' + myArrayList1[i].ToString();
                }
                else
                {
                    if (i == 0)
                    {
                        details.ToEmail = myArrayList1[i].ToString();
                    }
                    else
                    {
                        details.ToEmail = details.ToEmail + ',' + myArrayList1[i].ToString();
                    }
                    //details.ToEmail = details.ToEmail + ',' + email;
                }
                log.Info(" Email will be sent to supervisors '" + i + "' : '" + myArrayList1[i] + "' ");
            }
            return details;
        }
        catch (Exception ex)
        {


            log.Error(ex.StackTrace);
            log.Error(ex.Message);
            return details;
            //if (ex.Message.Contains("The specified string is not in the form required for an e-mail addr"))
            //{
            //    ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Post Approved Successfully!!!Mail sending Error!!!')</script>");


            //}

            //else if (ex.Message.Contains("Unable to send to a recipien"))
            //{
            //    ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Post Approved Successfully!!!Mail sending Error!!!')</script>");

            //}
            //else

            //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Post Approved Successfully!!!Mail sending Error!!!')</script>");


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

    //              //  j.AutoAppoval = Session["AutoApproval"].ToString();

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

            TextBoxYearJA.ClearSelection();

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

            TextBoxYear.ClearSelection();
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
            //TextBoxReference.Text = "";
            DropDownListuploadEPrint.ClearSelection();
            TextBoxEprintURL.Text = "";

            popupPanelAffil.Visible = false;
            popupPanelJournal.Visible = false;


            TextBoxRemarks.Text = "";

        }
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

        TextBoxYearJA.ClearSelection();

        TextBoxImpFact.Text = "";

        DropDownListPubType.ClearSelection();
        TextBoxPageFrom.Text = "";

        TextBoxPageTo.Text = "";
        TextBoxVolume.Text = "";
        RadioButtonListIndexed.SelectedValue = "N";
        CheckboxIndexAgency.ClearSelection();

        TextBoxRemarks.Text = "";

        panelConfPaper.Visible = false;

        TextBoxEventTitle.Text = "";
        TextBoxPlace.Text = "";
        TextBoxDate.Text = "";

        panelBookPublish.Visible = false;

        TextBoxTitleBook.Text = "";
        TextBoxChapterContributed.Text = "";
        TextBoxEdition.Text = "";
        TextBoxPublisher.Text = "";

        TextBoxYear.ClearSelection();
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
        //TextBoxReference.Text = "";
        DropDownListuploadEPrint.ClearSelection();
        TextBoxEprintURL.Text = "";

        popupPanelAffil.Visible = false;
        popupPanelJournal.Visible = false;
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
            pid = GridViewSearch.Rows[rowindex].Cells[2].Text.Trim().ToString();
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
        DSforgridview.SelectParameters.Add("Pid", Pid);
        DSforgridview.SelectParameters.Add("TypeEntry", TypeEntry);

        DSforgridview.SelectCommand = "select UploadPDFPath from Publication where PublicationID=@Pid  and TypeOfEntry=@TypeEntry ";
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

        // FileUploadPdf.Visible = true;

        DropDownListMuCategory.SelectedValue = v.MUCategorization;
        DropDownListPublicationEntry.SelectedValue = TypeEntry;
        TextBoxPubId.Text = Pid;
        txtboxTitleOfWrkItem.Text = v.TitleWorkItem;
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
        //  v = obj.fnfindjid(Pid, TypeEntry);
        if (TypeEntry == "JA")
        {
            panelJournalArticle.Visible = true;
            panelBookPublish.Visible = false;
            panelConfPaper.Visible = false;
            panelOthes.Visible = false;

            txtboxTitleOfWrkItem.Text = v.TitleWorkItem;

            TextBoxPubJournal.Text = v.PublisherOfJournal;

            TextBoxNameJournal.Text = "";

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

            TextBoxVolume.Text = v.JAVolume;
            RadioButtonListIndexed.SelectedValue = v.Indexed;
            // CheckboxIndexAgency.ClearSelection();
        }
        else if ( TypeEntry == "PR")
        {
            panelJournalArticle.Visible = true;
            panelBookPublish.Visible = false;
            panelConfPaper.Visible = false;
            panelOthes.Visible = false;

            txtboxTitleOfWrkItem.Text = v.TitleWorkItem;

            TextBoxPubJournal.Text = v.PublisherOfJournal;

            TextBoxNameJournal.Text = "";

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

            TextBoxYear.SelectedValue = v.BookPublishYear;
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

            TextBoxNewsPublish.Text = v.TitleWorkItem;


            TextBoxDateOfNewsPublish.Text = v.NewsPublishedDate.ToShortDateString();

            TextBoxPageNumNewsPaper.Text = v.NewsPageNum;


            // CheckboxIndexAgency.ClearSelection();
        }
        else if (TypeEntry == "TS")
        {



            panelJournalArticle.Visible = true;
            panelBookPublish.Visible = false;
            panelConfPaper.Visible = false;
            panelOthes.Visible = false;

            txtboxTitleOfWrkItem.Text = v.TitleWorkItem;

            TextBoxPubJournal.Text = v.PublisherOfJournal;

            TextBoxNameJournal.Text = "";

            DropDownListMonthJA.SelectedValue = v.PublishJAMonth.ToString();

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
             }
         }
        setModalWindow(sender, e);

        //ButtonSub.Enabled = false;
        // btnSave.Enabled = true;
        GridViewSearch.Visible = false;

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
            TextBoxAwardedBy.Enabled = false;
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
    public void fnRecordExistApproval(object sender, EventArgs e)
    {
        DropDownListPublicationEntry.Enabled = false;
        //lblmsg.Visible = true;
        panel.Visible = true;
        btnSave.Enabled = true;
        string Pid = Session["TempPid"].ToString();
        string TypeEntry = Session["TempTypeEntry"].ToString();
        btnSave.Enabled = true;

        DSforgridview.SelectParameters.Clear();
        DSforgridview.SelectParameters.Add("Pid", Pid);
        DSforgridview.SelectParameters.Add("TypeEntry", TypeEntry);

        DSforgridview.SelectCommand = "select UploadPDFPath from Publication where PublicationID=@Pid  and TypeOfEntry=@TypeEntry ";
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

        DropDownListMuCategory.DataBind();
        DropDownListMuCategory.SelectedValue = v.MUCategorization;
        DropDownListPublicationEntry.DataBind();
        DropDownListPublicationEntry.SelectedValue = TypeEntry;
        TextBoxPubId.Text = Pid;
        txtboxTitleOfWrkItem.Text = v.TitleWorkItem;
        //  v = obj.fnfindjid(Pid, TypeEntry);

        DropDownListPublicationEntryOnSelectedIndexChanged(sender, e);
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
            TextBoxIssue.Text = v.Issue;
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
            TextBoxIssue.Text = v.Issue;
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

            TextBoxYear.SelectedValue = v.BookPublishYear;
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



            TextBoxNewsPublish.Text = v.NewsPublisher;

            TextBoxDateOfNewsPublish.Text = v.NewsPublishedDate.ToShortDateString();

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
            //TextBoxPageFrom.Text = v.PageFrom;

            //TextBoxPageTo.Text = v.PageTo;
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
                ImageButton EmployeeCodeBtnimg = (ImageButton)Grid_AuthorEntry.Rows[rowIndex].Cells[2].FindControl("EmployeeCodeBtn");

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

                else if (DropdownMuNonMu.Text == "O")
                {
                    DropdownStudentInstitutionName.Visible = true;
                    DropdownStudentDepartmentName.Visible = true;
                    InstNme.Visible = false;
                    deptname.Visible = false;
                    DropdownStudentInstitutionName.SelectedValue = dtCurrentTable.Rows[i - 1]["Institution"].ToString();
                    DropdownStudentDepartmentName.SelectedValue = dtCurrentTable.Rows[i - 1]["Department"].ToString();
                    EmployeeCode.Text = dtCurrentTable.Rows[i - 1]["EmployeeCode"].ToString();

                    ImageButton1.Visible = false;
                    EmployeeCodeBtnimg.Enabled = false;
                    EmployeeCodeBtnimg.Visible = true;
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
                        ImageButton1.Visible = true;
                        EmployeeCodeBtnimg.Visible = false;
                        EmployeeCode.Enabled = false;
                        InstNme.Text = dtCurrentTable.Rows[i - 1]["InstitutionName"].ToString();
                        deptname.Text = dtCurrentTable.Rows[i - 1]["DepartmentName"].ToString();
                        InstId.Value = dtCurrentTable.Rows[i - 1]["Institution"].ToString();
                        deptId.Value = dtCurrentTable.Rows[i - 1]["Department"].ToString();
                    }
                    else
                    {
                        ImageButton1.Visible = false;
                        EmployeeCodeBtnimg.Enabled = false;
                        EmployeeCodeBtnimg.Visible = true;
                        DropdownStudentInstitutionName.Visible = true;
                        DropdownStudentDepartmentName.Visible = true;
                        InstNme.Visible = false;
                        deptname.Visible = false;
                        EmployeeCode.Enabled = false;
                        DropdownStudentInstitutionName.Text = dtCurrentTable.Rows[i - 1]["Institution"].ToString();
                        DropdownStudentDepartmentName.Text = dtCurrentTable.Rows[i - 1]["Department"].ToString();
                    }
                    NationalType.Visible = false;
                    ContinentId.Visible = false;
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
                    EmployeeCode.Enabled = false;
                }
                else if (DropdownMuNonMu.Text == "O")
                {
                    AuthorName.Enabled = true;
                    DropdownStudentInstitutionName.Enabled = true;
                    DropdownStudentInstitutionName.Enabled = true;
                    EmployeeCodeBtnimg.Enabled = false;
                    EmployeeCode.Enabled = false;
                }
                else if (DropdownMuNonMu.Text == "S")
                {
                    if (DropDownListPublicationEntry.SelectedValue == "JA" || DropDownListPublicationEntry.SelectedValue == "TS" || DropDownListPublicationEntry.SelectedValue == "PR")
                    {
                        AuthorName.Enabled = false;
                        DropdownStudentInstitutionName.Visible = false;
                        DropdownStudentInstitutionName.Visible = false;
                        EmployeeCode.Enabled = false;
                    }
                    else
                    {
                        AuthorName.Enabled = true;
                        DropdownStudentInstitutionName.Enabled = true;
                        DropdownStudentInstitutionName.Enabled = true;
                        EmployeeCodeBtnimg.Enabled = false;
                        AuthorName.Enabled = true;
                        EmployeeCode.Enabled = false;
                    }


                    MailId.Enabled = true;
                }
                else
                {
                    MailId.Enabled = true;
                }
                DropdownMuNonMu1.Enabled = false;
                EmployeeCodeBtnimg1.Enabled = false;
                if (DropDownListPublicationEntry.SelectedValue == "JA" || DropDownListPublicationEntry.SelectedValue == "TS" || DropDownListPublicationEntry.SelectedValue == "PR")
                {
                    DropdownMuNonMu1.Enabled = true;
                    EmployeeCodeBtnimg1.Enabled = true;
                }
                else
                {
                    DropdownMuNonMu1.Enabled = false;
                    EmployeeCodeBtnimg1.Enabled = false;
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
                    lblnote1.Visible = true;
                    lblnote2.Visible = true;
                    PublishData j = new PublishData();
                    PublishData j1 = new PublishData();
                    j.PaublicationID = Session["TempPid"].ToString();
                    j.TypeOfEntry = Session["TempTypeEntry"].ToString();
                    j1 = b1.SelectDefaultAuthor(j);
                    lblnote2.Text = " Note 2: The publication is submitted to RMS by " + j1.AuthorName + "";
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
                    lblnote1.Visible = false;
                    lblnote2.Visible = false;
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
                    lblnote1.Visible = false;
                    lblnote2.Visible = false;
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
                    lblnote1.Visible = false;
                    lblnote2.Visible = false;
                }


                // Grid_AuthorEntry.Columns[11].Visible = false;


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
             }
         }
        setModalWindow(sender, e);
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
        //ButtonSub.Enabled = true;
        GridViewSearch.Visible = false;
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
        if (affiliateSrch.Text.Trim() == "" && SrchId.Text.Trim() == "")
        {
            SqlDataSourceAffil.SelectCommand = "SELECT top 10  User_Id,prefix+' '+firstname+' '+middlename+' '+lastname  as Name from User_M where Active='Y'";
            popGridAffil.DataBind();
            popGridAffil.Visible = true;
        }

        else
        {
            string name = affiliateSrch.Text.Replace(" ", String.Empty);

           //SqlDataSourceAffil.SelectParameters.Clear();
           //SqlDataSourceAffil.SelectParameters.Add("name", name);
           // SqlDataSourceAffil.SelectParameters.Add("SrchId", SrchId.Text);

           //SqlDataSourceAffil.SelectCommand = "SELECT top 10  User_Id,prefix+' '+firstname+' '+middlename+' '+lastname  as Name from User_M where prefix+firstname+middlename+lastname like '%' + @name + '%' and User_Id like '%' + @SrchId + '%' and Active='Y'";
            SqlDataSourceAffil.SelectCommand = "SELECT top 10  User_Id,prefix+' '+firstname+' '+middlename+' '+lastname  as Name from User_M where prefix+firstname+middlename+lastname like '%" + name + "%' and User_Id like '%" + SrchId.Text + "%' and Active='Y'";

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
            SqlDataSourceJournal.SelectParameters.Add("journalcodeSrch", journalcodeSrch.Text);

            SqlDataSourceJournal.SelectCommand = "SELECT  Id,Title,AbbreviatedTitle FROM [Journal_M] where Title like '%' + @journalcodeSrch+ '%'";
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
    // bind branch popup grid on text change
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
            StudentSQLDS.SelectCommand = "Select TOP 10  RollNo,Name,SISClass.ClassName as ClassName,SISInstitution.InstName as InstName,EmailID1 ,SISStudentGenInfo.ClassCode as ClassCode,SISInstnHR.HRInstitute as InstID from SISStudentGenInfo,SISClass,SISInstitution,SISInstnHR  where SISStudentGenInfo.ClassCode=SISClass.ClassCode and SISStudentGenInfo.InstID=SISInstitution.InstID and SISInstnHR.Institute_Id=SISInstitution.InstID and SISInstnHR.Institute_Id=SISStudentGenInfo.InstID and   RollNo like '%' + @txtSrchStudentRollNo+ '%' and (SISStudentGenInfo.InstID=@StudentIntddl)";
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
            StudentSQLDS.SelectParameters.Add("StudentName", txtSrchStudentName.Text.Trim());
            StudentSQLDS.SelectParameters.Add("StudentRollNo", txtSrchStudentRollNo.Text.Trim());
            StudentSQLDS.SelectCommand = "Select TOP 10  RollNo,Name,SISClass.ClassName as ClassName,SISInstitution.InstName as InstName,EmailID1 ,SISStudentGenInfo.ClassCode as ClassCode,SISInstnHR.HRInstitute as InstID from SISStudentGenInfo,SISClass,SISInstitution,SISInstnHR  where SISStudentGenInfo.ClassCode=SISClass.ClassCode and SISStudentGenInfo.InstID=SISInstitution.InstID and SISInstnHR.Institute_Id=SISInstitution.InstID and SISInstnHR.Institute_Id=SISStudentGenInfo.InstID and  Name like '%'+@StudentName+'%' and RollNo like '%'+@StudentRollNo+'%'";
            popupStudentGrid.DataBind();
            popupStudentGrid.Visible = true;
        }
        else
        {
            StudentSQLDS.SelectParameters.Clear();
            StudentSQLDS.SelectParameters.Add("StudentName", txtSrchStudentName.Text.Trim());
            StudentSQLDS.SelectParameters.Add("StudentRollNo", txtSrchStudentRollNo.Text.Trim());
            StudentSQLDS.SelectParameters.Add("StudentIntddl", StudentIntddl.SelectedValue);
            StudentSQLDS.SelectCommand = "Select TOP 10  RollNo,Name,SISClass.ClassName as ClassName,SISInstitution.InstName as InstName,EmailID1 ,SISStudentGenInfo.ClassCode as ClassCode,SISInstnHR.HRInstitute as InstID from SISStudentGenInfo,SISClass,SISInstitution,SISInstnHR  where SISStudentGenInfo.ClassCode=SISClass.ClassCode and SISStudentGenInfo.InstID=SISInstitution.InstID and SISInstnHR.Institute_Id=SISInstitution.InstID and SISInstnHR.Institute_Id=SISStudentGenInfo.InstID and  (Name like '%'+@StudentName+'%' and RollNo like '%'+@StudentRollNo+'%' and (SISStudentGenInfo.InstID=@StudentIntddl) ) ";
            popupStudentGrid.DataBind();
            popupStudentGrid.Visible = true;
        }


        // string rowVal = Request.Form["rowIndx"];
        string a = rowVal.Value;
        int rowIndex = Convert.ToInt32(a);

        ModalPopupExtender ModalPopupExtender8 = (ModalPopupExtender)Grid_AuthorEntry.Rows[rowIndex].FindControl("ModalPopupExtender2");
        ModalPopupExtender8.Show();
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
        Department.Value = classcode.Value;
        popupStudentGrid.DataBind();

    }
}