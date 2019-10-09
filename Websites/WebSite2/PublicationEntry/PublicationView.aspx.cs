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

public partial class PublicationView : System.Web.UI.Page
{
    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

   // string mainpath = ConfigurationManager.AppSettings["PdfPath"].ToString();
    Business B = new Business();
    Journal_DataObject JournalDataObj = new Journal_DataObject();
    JournalData JournalValueObj = new JournalData();
    public string pageID = "L43";
    protected void Page_Load(object sender, EventArgs e)
    {    
        //popupPanelJournal.Visible = true;
        
         if (!IsPostBack)
         {
            string keyword= Request.QueryString["Keyword"];
            if (keyword == "true" || keyword == "false" || keyword == "isPagesrch")
            {
                BtnBack.Visible = true;
                Session["TempPid"] = Request.QueryString["PubID"];
                Session["TempTypeEntry"] = Request.QueryString["PubType"];
                fnRecordExist(sender, e);
                LabelProjectReference.Visible = false;
                DropDownListhasProjectreference.Visible = false;
                LabelProjectDetails.Visible = false;
                TextBoxProjectDetails.Visible = false;
                ImageButtonProject.Visible = false;
            }
            else
            {
                BtnBack.Visible = false;
                //if (!Session["authPage"].ToString().Contains("$" + pageID + "$"))
                //{
                //    string unacces = "Unauthorized Acces!!! Login Again";
                //    Response.Redirect("~/Login.aspx?val=" + unacces);
                //}
                panel.Visible = false;
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

    protected void GVViewFile_SelectedIndexChanged(object sender, EventArgs e)
    {string BoxId = TextBoxPubId.Text.Trim();
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
        dataBind();
    
    }
    protected void DropDownListInstOnSelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownListDept.Items.Clear();
        DropDownListDept.Items.Add(new ListItem("ALL", "A", true));
        SqlDataSourceDept.SelectCommand = "select DeptId,DeptName from Dept_M where Institute_Id='" + DropDownListInst.SelectedValue + "'";
        SqlDataSourceDept.DataBind();
    }
    protected void GridViewSearchPub_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        dataBind();
        GridViewSearch.PageIndex = e.NewPageIndex;
        GridViewSearch.DataBind();
    }
        private void dataBind()
    {

        GridViewSearch.Visible = true;
        panel.Visible = false;

        if (EntryTypesearch.SelectedValue != "A")
        {
            if (PubIDSearch.Text == "" && drpPubStatusSearch.SelectedValue != "A" && TextBoxWorkItemSearch.Text == "" && TextBoxEnteredBySearch.Text == ""
                && DropDownListInst.SelectedValue == "A" && DropDownListDept.SelectedValue=="A")
            {
              //  SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName ,t.EntryName,TitleWorkItem,c.PubCatName from Publication p,Status_Publication_M s, PubMUCategorization_M c ,PublicationTypeEntry_M t  where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization  and status!='CAN' and TypeOfEntry='" + EntryTypesearch.SelectedValue + "'  and Institution='" + a1.InstituteId + "' ";
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("TypeOfEntry", EntryTypesearch.SelectedValue);
                SqlDataSource1.SelectParameters.Add("Status", drpPubStatusSearch.SelectedValue);

                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor ,s.StatusName,t.EntryName,TitleWorkItem,c.PubCatName,ISStudentAuthor from Publication p,Status_Publication_M s , PubMUCategorization_M c ,PublicationTypeEntry_M t where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization and TypeOfEntry=@TypeOfEntry and Status=@Status";
            }
            else if (PubIDSearch.Text == "" && drpPubStatusSearch.SelectedValue != "A" && TextBoxWorkItemSearch.Text == "" && TextBoxEnteredBySearch.Text == ""
          && DropDownListInst.SelectedValue == "A" && DropDownListDept.SelectedValue != "A")
            {
                //  SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName ,t.EntryName,TitleWorkItem,c.PubCatName from Publication p,Status_Publication_M s, PubMUCategorization_M c ,PublicationTypeEntry_M t  where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization  and status!='CAN' and TypeOfEntry='" + EntryTypesearch.SelectedValue + "'  and Institution='" + a1.InstituteId + "' ";
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("TypeOfEntry", EntryTypesearch.SelectedValue);
                SqlDataSource1.SelectParameters.Add("Status", drpPubStatusSearch.SelectedValue);
                SqlDataSource1.SelectParameters.Add("Department", DropDownListDept.SelectedValue);

                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor ,s.StatusName,t.EntryName,TitleWorkItem,c.PubCatName,ISStudentAuthor from Publication p,Status_Publication_M s , PubMUCategorization_M c ,PublicationTypeEntry_M t where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization and TypeOfEntry=@TypeOfEntry and Status=@Status and Department=@Department ";
            }
            else if (PubIDSearch.Text == "" && drpPubStatusSearch.SelectedValue != "A" && TextBoxWorkItemSearch.Text == "" && TextBoxEnteredBySearch.Text == ""
                && DropDownListInst.SelectedValue != "A" && DropDownListDept.SelectedValue == "A")
            {
                //  SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName ,t.EntryName,TitleWorkItem,c.PubCatName from Publication p,Status_Publication_M s, PubMUCategorization_M c ,PublicationTypeEntry_M t  where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization  and status!='CAN' and TypeOfEntry='" + EntryTypesearch.SelectedValue + "'  and Institution='" + a1.InstituteId + "' ";
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("TypeOfEntry", EntryTypesearch.SelectedValue);
                SqlDataSource1.SelectParameters.Add("Status", drpPubStatusSearch.SelectedValue);
                SqlDataSource1.SelectParameters.Add("Institution", DropDownListInst.SelectedValue);
                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor ,s.StatusName,t.EntryName,TitleWorkItem,c.PubCatName,ISStudentAuthor from Publication p,Status_Publication_M s , PubMUCategorization_M c ,PublicationTypeEntry_M t where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization and TypeOfEntry=@TypeOfEntry and Status=@Status and Institution=@Institution";
            }
            else if (PubIDSearch.Text == "" && drpPubStatusSearch.SelectedValue != "A" && TextBoxWorkItemSearch.Text == "" && TextBoxEnteredBySearch.Text == ""
     && DropDownListInst.SelectedValue != "A" && DropDownListDept.SelectedValue != "A")
            {
                //  SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName ,t.EntryName,TitleWorkItem,c.PubCatName from Publication p,Status_Publication_M s, PubMUCategorization_M c ,PublicationTypeEntry_M t  where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization  and status!='CAN' and TypeOfEntry='" + EntryTypesearch.SelectedValue + "'  and Institution='" + a1.InstituteId + "' ";
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("TypeOfEntry", EntryTypesearch.SelectedValue);
                SqlDataSource1.SelectParameters.Add("Status", drpPubStatusSearch.SelectedValue);
                SqlDataSource1.SelectParameters.Add("Institution", DropDownListInst.SelectedValue);
                SqlDataSource1.SelectParameters.Add("Department", DropDownListDept.SelectedValue);

                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor ,s.StatusName,t.EntryName,TitleWorkItem,c.PubCatName,ISStudentAuthor from Publication p,Status_Publication_M s , PubMUCategorization_M c ,PublicationTypeEntry_M t where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization and TypeOfEntry=@TypeOfEntry and Status=@Status  and Institution=@Institution and Department=@Department";
            }
            else   if (PubIDSearch.Text == "" && drpPubStatusSearch.SelectedValue != "A" && TextBoxWorkItemSearch.Text == "" && TextBoxEnteredBySearch.Text != ""
                && DropDownListInst.SelectedValue == "A" && DropDownListDept.SelectedValue == "A")
            {
                //  SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName ,t.EntryName,TitleWorkItem,c.PubCatName from Publication p,Status_Publication_M s, PubMUCategorization_M c ,PublicationTypeEntry_M t  where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization  and status!='CAN' and TypeOfEntry='" + EntryTypesearch.SelectedValue + "'  and Institution='" + a1.InstituteId + "' ";
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("TypeOfEntry", EntryTypesearch.SelectedValue);
                SqlDataSource1.SelectParameters.Add("Status", drpPubStatusSearch.SelectedValue);
                SqlDataSource1.SelectParameters.Add("CreatedBy", TextBoxEnteredBySearch.Text);
                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor ,s.StatusName,t.EntryName,TitleWorkItem,c.PubCatName,ISStudentAuthor from Publication p,Status_Publication_M s , PubMUCategorization_M c ,PublicationTypeEntry_M t where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization and TypeOfEntry=@TypeOfEntry and Status=@Status and CreatedBy like '%' + @CreatedBy + '%'";
            }
            else if (PubIDSearch.Text == "" && drpPubStatusSearch.SelectedValue != "A" && TextBoxWorkItemSearch.Text == "" && TextBoxEnteredBySearch.Text != ""
      && DropDownListInst.SelectedValue == "A" && DropDownListDept.SelectedValue != "A")
            {
                //  SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName ,t.EntryName,TitleWorkItem,c.PubCatName from Publication p,Status_Publication_M s, PubMUCategorization_M c ,PublicationTypeEntry_M t  where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization  and status!='CAN' and TypeOfEntry='" + EntryTypesearch.SelectedValue + "'  and Institution='" + a1.InstituteId + "' ";
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("TypeOfEntry", EntryTypesearch.SelectedValue);
                SqlDataSource1.SelectParameters.Add("Status", drpPubStatusSearch.SelectedValue);
                SqlDataSource1.SelectParameters.Add("CreatedBy", TextBoxEnteredBySearch.Text);
                SqlDataSource1.SelectParameters.Add("Department", DropDownListDept.SelectedValue);

                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor ,s.StatusName,t.EntryName,TitleWorkItem,c.PubCatName,ISStudentAuthor from Publication p,Status_Publication_M s , PubMUCategorization_M c ,PublicationTypeEntry_M t where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization and TypeOfEntry=@TypeOfEntry and Status=@Status and CreatedBy like '%' + @CreatedBy + '%' and Department=@Department";
            }
            else if (PubIDSearch.Text == "" && drpPubStatusSearch.SelectedValue != "A" && TextBoxWorkItemSearch.Text == "" && TextBoxEnteredBySearch.Text != ""
        && DropDownListInst.SelectedValue != "A" && DropDownListDept.SelectedValue == "A")
            {
                //  SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName ,t.EntryName,TitleWorkItem,c.PubCatName from Publication p,Status_Publication_M s, PubMUCategorization_M c ,PublicationTypeEntry_M t  where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization  and status!='CAN' and TypeOfEntry='" + EntryTypesearch.SelectedValue + "'  and Institution='" + a1.InstituteId + "' ";
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("TypeOfEntry", EntryTypesearch.SelectedValue);
                SqlDataSource1.SelectParameters.Add("Status", drpPubStatusSearch.SelectedValue);
                SqlDataSource1.SelectParameters.Add("CreatedBy", TextBoxEnteredBySearch.Text);
                SqlDataSource1.SelectParameters.Add("Institution", DropDownListInst.SelectedValue);
                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor ,s.StatusName,t.EntryName,TitleWorkItem,c.PubCatName,ISStudentAuthor from Publication p,Status_Publication_M s , PubMUCategorization_M c ,PublicationTypeEntry_M t where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization and TypeOfEntry=@TypeOfEntry and Status=@Status and CreatedBy like '%' + @CreatedBy + '%'  and Institution=@Institution";
            }
            else if (PubIDSearch.Text == "" && drpPubStatusSearch.SelectedValue != "A" && TextBoxWorkItemSearch.Text == "" && TextBoxEnteredBySearch.Text != ""
&& DropDownListInst.SelectedValue != "A" && DropDownListDept.SelectedValue != "A")
            {
                //  SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName ,t.EntryName,TitleWorkItem,c.PubCatName from Publication p,Status_Publication_M s, PubMUCategorization_M c ,PublicationTypeEntry_M t  where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization  and status!='CAN' and TypeOfEntry='" + EntryTypesearch.SelectedValue + "'  and Institution='" + a1.InstituteId + "' ";
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("TypeOfEntry", EntryTypesearch.SelectedValue);
                SqlDataSource1.SelectParameters.Add("Status", drpPubStatusSearch.SelectedValue);
                SqlDataSource1.SelectParameters.Add("CreatedBy", TextBoxEnteredBySearch.Text);
                SqlDataSource1.SelectParameters.Add("Institution", DropDownListInst.SelectedValue);
                SqlDataSource1.SelectParameters.Add("Department", DropDownListDept.SelectedValue);

                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor ,s.StatusName,t.EntryName,TitleWorkItem,c.PubCatName,ISStudentAuthor from Publication p,Status_Publication_M s , PubMUCategorization_M c ,PublicationTypeEntry_M t where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization and TypeOfEntry=@TypeOfEntry and Status=@Status and CreatedBy like '%' + @CreatedBy + '%' and Institution=@Institution and Department=@Department";
            }
            else if (PubIDSearch.Text == "" && drpPubStatusSearch.SelectedValue != "A" && TextBoxWorkItemSearch.Text != "" && TextBoxEnteredBySearch.Text == ""
                && DropDownListInst.SelectedValue == "A" && DropDownListDept.SelectedValue == "A")
            {
                //  SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName ,t.EntryName,TitleWorkItem,c.PubCatName from Publication p,Status_Publication_M s, PubMUCategorization_M c ,PublicationTypeEntry_M t  where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization  and status!='CAN' and TypeOfEntry='" + EntryTypesearch.SelectedValue + "'  and Institution='" + a1.InstituteId + "' ";
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("TypeOfEntry", EntryTypesearch.SelectedValue);
                SqlDataSource1.SelectParameters.Add("Status", drpPubStatusSearch.SelectedValue);
                SqlDataSource1.SelectParameters.Add("TitleWorkItem", TextBoxWorkItemSearch.Text);

                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor ,s.StatusName,t.EntryName,TitleWorkItem,c.PubCatName,ISStudentAuthor from Publication p,Status_Publication_M s , PubMUCategorization_M c ,PublicationTypeEntry_M t where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization and TypeOfEntry=@TypeOfEntry and Status=@Status and TitleWorkItem like '%'+ @TitleWorkItem+ '%' ";
            }
            else if (PubIDSearch.Text == "" && drpPubStatusSearch.SelectedValue != "A" && TextBoxWorkItemSearch.Text != "" && TextBoxEnteredBySearch.Text == ""
     && DropDownListInst.SelectedValue == "A" && DropDownListDept.SelectedValue != "A")
            {
                //  SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName ,t.EntryName,TitleWorkItem,c.PubCatName from Publication p,Status_Publication_M s, PubMUCategorization_M c ,PublicationTypeEntry_M t  where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization  and status!='CAN' and TypeOfEntry='" + EntryTypesearch.SelectedValue + "'  and Institution='" + a1.InstituteId + "' ";
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("TypeOfEntry", EntryTypesearch.SelectedValue);
                SqlDataSource1.SelectParameters.Add("Status", drpPubStatusSearch.SelectedValue);
                SqlDataSource1.SelectParameters.Add("TitleWorkItem", TextBoxWorkItemSearch.Text);
                SqlDataSource1.SelectParameters.Add("Department", DropDownListDept.SelectedValue);

                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor ,s.StatusName,t.EntryName,TitleWorkItem,c.PubCatName,ISStudentAuthor from Publication p,Status_Publication_M s , PubMUCategorization_M c ,PublicationTypeEntry_M t where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization and TypeOfEntry=@TypeOfEntry and Status=@Status  and TitleWorkItem like '%' + @TitleWorkItem + '%' and Department=@Department";
            }
            else if (PubIDSearch.Text == "" && drpPubStatusSearch.SelectedValue != "A" && TextBoxWorkItemSearch.Text != "" && TextBoxEnteredBySearch.Text == ""
         && DropDownListInst.SelectedValue != "A" && DropDownListDept.SelectedValue == "A")
            {
                //  SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName ,t.EntryName,TitleWorkItem,c.PubCatName from Publication p,Status_Publication_M s, PubMUCategorization_M c ,PublicationTypeEntry_M t  where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization  and status!='CAN' and TypeOfEntry='" + EntryTypesearch.SelectedValue + "'  and Institution='" + a1.InstituteId + "' ";
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("TypeOfEntry", EntryTypesearch.SelectedValue);
                SqlDataSource1.SelectParameters.Add("Status", drpPubStatusSearch.SelectedValue);
                SqlDataSource1.SelectParameters.Add("TitleWorkItem", TextBoxWorkItemSearch.Text);
                SqlDataSource1.SelectParameters.Add("Institution", DropDownListInst.SelectedValue);
                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor ,s.StatusName,t.EntryName,TitleWorkItem,c.PubCatName,ISStudentAuthor from Publication p,Status_Publication_M s , PubMUCategorization_M c ,PublicationTypeEntry_M t where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization and TypeOfEntry=@TypeOfEntry and Status=@Status  and TitleWorkItem like '%' + @TitleWorkItem + '%'  and Institution=@Institution";
            }
            else if (PubIDSearch.Text == "" && drpPubStatusSearch.SelectedValue != "A" && TextBoxWorkItemSearch.Text != "" && TextBoxEnteredBySearch.Text == ""
&& DropDownListInst.SelectedValue != "A" && DropDownListDept.SelectedValue != "A")
            {
                //  SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName ,t.EntryName,TitleWorkItem,c.PubCatName from Publication p,Status_Publication_M s, PubMUCategorization_M c ,PublicationTypeEntry_M t  where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization  and status!='CAN' and TypeOfEntry='" + EntryTypesearch.SelectedValue + "'  and Institution='" + a1.InstituteId + "' ";
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("TypeOfEntry", EntryTypesearch.SelectedValue);
                SqlDataSource1.SelectParameters.Add("Status", drpPubStatusSearch.SelectedValue);
                SqlDataSource1.SelectParameters.Add("TitleWorkItem", TextBoxWorkItemSearch.Text);
                SqlDataSource1.SelectParameters.Add("Institution", DropDownListInst.SelectedValue);
                SqlDataSource1.SelectParameters.Add("Department", DropDownListDept.SelectedValue);

                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor ,s.StatusName,t.EntryName,TitleWorkItem,c.PubCatName,ISStudentAuthor from Publication p,Status_Publication_M s , PubMUCategorization_M c ,PublicationTypeEntry_M t where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization and TypeOfEntry=@TypeOfEntry and Status=@Status and TitleWorkItem like '%' + @TitleWorkItem + '%'  and Institution=@Institution and Department=@Department";
            }
            else if (PubIDSearch.Text == "" && drpPubStatusSearch.SelectedValue != "A" && TextBoxWorkItemSearch.Text != "" && TextBoxEnteredBySearch.Text != ""
                && DropDownListInst.SelectedValue == "A" && DropDownListDept.SelectedValue == "A")
            {
                //  SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName ,t.EntryName,TitleWorkItem,c.PubCatName from Publication p,Status_Publication_M s, PubMUCategorization_M c ,PublicationTypeEntry_M t  where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization  and status!='CAN' and TypeOfEntry='" + EntryTypesearch.SelectedValue + "'  and Institution='" + a1.InstituteId + "' ";
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("TypeOfEntry", EntryTypesearch.SelectedValue);
                SqlDataSource1.SelectParameters.Add("Status", drpPubStatusSearch.SelectedValue);
                SqlDataSource1.SelectParameters.Add("TitleWorkItem", TextBoxWorkItemSearch.Text);
                SqlDataSource1.SelectParameters.Add("CreatedBy", TextBoxEnteredBySearch.Text);

                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor ,s.StatusName,t.EntryName,TitleWorkItem,c.PubCatName,ISStudentAuthor from Publication p,Status_Publication_M s , PubMUCategorization_M c ,PublicationTypeEntry_M t where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization and TypeOfEntry=@TypeOfEntry and Status=@Status and TitleWorkItem like '%' + @TitleWorkItem + '%' and CreatedBy like '%' + @CreatedBy + '%'";
            }
            else if (PubIDSearch.Text == "" && drpPubStatusSearch.SelectedValue != "A" && TextBoxWorkItemSearch.Text != "" && TextBoxEnteredBySearch.Text != ""
    && DropDownListInst.SelectedValue == "A" && DropDownListDept.SelectedValue != "A")
            {
                //  SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName ,t.EntryName,TitleWorkItem,c.PubCatName from Publication p,Status_Publication_M s, PubMUCategorization_M c ,PublicationTypeEntry_M t  where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization  and status!='CAN' and TypeOfEntry='" + EntryTypesearch.SelectedValue + "'  and Institution='" + a1.InstituteId + "' ";
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("TypeOfEntry", EntryTypesearch.SelectedValue);
                SqlDataSource1.SelectParameters.Add("Status", drpPubStatusSearch.SelectedValue);
                SqlDataSource1.SelectParameters.Add("TitleWorkItem", TextBoxWorkItemSearch.Text);
                SqlDataSource1.SelectParameters.Add("CreatedBy", TextBoxEnteredBySearch.Text);
                SqlDataSource1.SelectParameters.Add("Department", DropDownListDept.SelectedValue);

                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor ,s.StatusName,t.EntryName,TitleWorkItem,c.PubCatName,ISStudentAuthor from Publication p,Status_Publication_M s , PubMUCategorization_M c ,PublicationTypeEntry_M t where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization and TypeOfEntry=@TypeOfEntry and Status=@Status and TitleWorkItem like '%' + @TitleWorkItem + '%' and CreatedBy like '%' + @CreatedBy + '%' and Department=@Department";
            }
            else if (PubIDSearch.Text == "" && drpPubStatusSearch.SelectedValue != "A" && TextBoxWorkItemSearch.Text != "" && TextBoxEnteredBySearch.Text != ""
                && DropDownListInst.SelectedValue != "A" && DropDownListDept.SelectedValue == "A")
            {
                //  SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName ,t.EntryName,TitleWorkItem,c.PubCatName from Publication p,Status_Publication_M s, PubMUCategorization_M c ,PublicationTypeEntry_M t  where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization  and status!='CAN' and TypeOfEntry='" + EntryTypesearch.SelectedValue + "'  and Institution='" + a1.InstituteId + "' ";
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("TypeOfEntry", EntryTypesearch.SelectedValue);
                SqlDataSource1.SelectParameters.Add("Status", drpPubStatusSearch.SelectedValue);
                SqlDataSource1.SelectParameters.Add("CreatedBy", TextBoxEnteredBySearch.Text);
                SqlDataSource1.SelectParameters.Add("Institution", DropDownListInst.SelectedValue);
                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor ,s.StatusName,t.EntryName,TitleWorkItem,c.PubCatName,ISStudentAuthor from Publication p,Status_Publication_M s , PubMUCategorization_M c ,PublicationTypeEntry_M t where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization and TypeOfEntry=@TypeOfEntry and Status=@Status and CreatedBy like '%' + @CreatedBy + '%'  and Institution=@Institution";
            }
            else if (PubIDSearch.Text == "" && drpPubStatusSearch.SelectedValue != "A" && TextBoxWorkItemSearch.Text != "" && TextBoxEnteredBySearch.Text != ""
      && DropDownListInst.SelectedValue != "A" && DropDownListDept.SelectedValue != "A")
            {
                //  SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName ,t.EntryName,TitleWorkItem,c.PubCatName from Publication p,Status_Publication_M s, PubMUCategorization_M c ,PublicationTypeEntry_M t  where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization  and status!='CAN' and TypeOfEntry='" + EntryTypesearch.SelectedValue + "'  and Institution='" + a1.InstituteId + "' ";
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("TypeOfEntry", EntryTypesearch.SelectedValue);
                SqlDataSource1.SelectParameters.Add("Status", drpPubStatusSearch.SelectedValue);
                SqlDataSource1.SelectParameters.Add("CreatedBy", TextBoxEnteredBySearch.Text);
                SqlDataSource1.SelectParameters.Add("TitleWorkItem", TextBoxWorkItemSearch.Text);
                SqlDataSource1.SelectParameters.Add("Department", DropDownListDept.SelectedValue);
                SqlDataSource1.SelectParameters.Add("Institution", DropDownListInst.SelectedValue);

                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor ,s.StatusName,t.EntryName,TitleWorkItem,c.PubCatName,ISStudentAuthor from Publication p,Status_Publication_M s , PubMUCategorization_M c ,PublicationTypeEntry_M t where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization and TypeOfEntry=@TypeOfEntry and Status=@Status and TitleWorkItem like '%' + @TitleWorkItem + '%' and CreatedBy like '%' + @CreatedBy + '%'  and Institution=@Institution and Department=@Department";
            }
            else if (PubIDSearch.Text == "" && drpPubStatusSearch.SelectedValue == "A" && TextBoxWorkItemSearch.Text == "" && TextBoxEnteredBySearch.Text == ""
                && DropDownListInst.SelectedValue == "A" && DropDownListDept.SelectedValue == "A")
            
            {
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("TypeOfEntry", EntryTypesearch.SelectedValue);
                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName,t.EntryName,TitleWorkItem,c.PubCatName,ISStudentAuthor from Publication p,Status_Publication_M s, PubMUCategorization_M c ,PublicationTypeEntry_M t where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization and   TypeOfEntry=@TypeOfEntry";
                // SqlDataSource1.SelectCommand = "select j.Id as Id,Year,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,Comments from Journal_IF_Details j,Journal_M k where j.Id=k.Id and j.Id='" + txtJid.Text.Trim() + "'";
            }
            else if (PubIDSearch.Text == "" && drpPubStatusSearch.SelectedValue == "A" && TextBoxWorkItemSearch.Text == "" && TextBoxEnteredBySearch.Text == ""
  && DropDownListInst.SelectedValue == "A" && DropDownListDept.SelectedValue != "A")
            {
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("TypeOfEntry", EntryTypesearch.SelectedValue);
                SqlDataSource1.SelectParameters.Add("Department", DropDownListDept.SelectedValue);

                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName,t.EntryName,TitleWorkItem,c.PubCatName,ISStudentAuthor from Publication p,Status_Publication_M s, PubMUCategorization_M c ,PublicationTypeEntry_M t where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization and   TypeOfEntry=@TypeOfEntry and Department=@Department ";
                // SqlDataSource1.SelectCommand = "select j.Id as Id,Year,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,Comments from Journal_IF_Details j,Journal_M k where j.Id=k.Id and j.Id='" + txtJid.Text.Trim() + "'";
            }
            else if (PubIDSearch.Text == "" && drpPubStatusSearch.SelectedValue == "A" && TextBoxWorkItemSearch.Text == "" && TextBoxEnteredBySearch.Text == ""
          && DropDownListInst.SelectedValue != "A" && DropDownListDept.SelectedValue == "A")
            {
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("TypeOfEntry", EntryTypesearch.SelectedValue);
                SqlDataSource1.SelectParameters.Add("Institution", DropDownListInst.SelectedValue);
                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName,t.EntryName,TitleWorkItem,c.PubCatName,ISStudentAuthor from Publication p,Status_Publication_M s, PubMUCategorization_M c ,PublicationTypeEntry_M t where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization and   TypeOfEntry=@TypeOfEntry and Institution=@Institution ";
                // SqlDataSource1.SelectCommand = "select j.Id as Id,Year,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,Comments from Journal_IF_Details j,Journal_M k where j.Id=k.Id and j.Id='" + txtJid.Text.Trim() + "'";
            }
            else if (PubIDSearch.Text == "" && drpPubStatusSearch.SelectedValue == "A" && TextBoxWorkItemSearch.Text == "" && TextBoxEnteredBySearch.Text == ""
&& DropDownListInst.SelectedValue != "A" && DropDownListDept.SelectedValue != "A")
            {
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("TypeOfEntry", EntryTypesearch.SelectedValue);
                SqlDataSource1.SelectParameters.Add("Institution", DropDownListInst.SelectedValue);
                SqlDataSource1.SelectParameters.Add("Department", DropDownListDept.SelectedValue);

                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName,t.EntryName,TitleWorkItem,c.PubCatName,ISStudentAuthor from Publication p,Status_Publication_M s, PubMUCategorization_M c ,PublicationTypeEntry_M t where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization and   TypeOfEntry=@TypeOfEntry  and Institution=@Institution and Department=@Department";
                // SqlDataSource1.SelectCommand = "select j.Id as Id,Year,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,Comments from Journal_IF_Details j,Journal_M k where j.Id=k.Id and j.Id='" + txtJid.Text.Trim() + "'";
            }
            else if (PubIDSearch.Text == "" && drpPubStatusSearch.SelectedValue == "A" && TextBoxWorkItemSearch.Text == "" && TextBoxEnteredBySearch.Text != ""
                && DropDownListInst.SelectedValue == "A" && DropDownListDept.SelectedValue == "A")
            {
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("TypeOfEntry", EntryTypesearch.SelectedValue);
                SqlDataSource1.SelectParameters.Add("CreatedBy", TextBoxEnteredBySearch.Text);
                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName,t.EntryName,TitleWorkItem,c.PubCatName,ISStudentAuthor from Publication p,Status_Publication_M s, PubMUCategorization_M c ,PublicationTypeEntry_M t where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization and   TypeOfEntry=@TypeOfEntry and CreatedBy like '%' + @CreatedBy + '%'";
                // SqlDataSource1.SelectCommand = "select j.Id as Id,Year,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,Comments from Journal_IF_Details j,Journal_M k where j.Id=k.Id and j.Id='" + txtJid.Text.Trim() + "'";
            }
            else if (PubIDSearch.Text == "" && drpPubStatusSearch.SelectedValue == "A" && TextBoxWorkItemSearch.Text == "" && TextBoxEnteredBySearch.Text != ""
     && DropDownListInst.SelectedValue == "A" && DropDownListDept.SelectedValue != "A")
            {
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("TypeOfEntry", EntryTypesearch.SelectedValue);
                SqlDataSource1.SelectParameters.Add("CreatedBy", TextBoxEnteredBySearch.Text);
                SqlDataSource1.SelectParameters.Add("Department", DropDownListDept.SelectedValue);

                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName,t.EntryName,TitleWorkItem,c.PubCatName,ISStudentAuthor from Publication p,Status_Publication_M s, PubMUCategorization_M c ,PublicationTypeEntry_M t where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization and   TypeOfEntry=@TypeOfEntry and CreatedBy like '%' + @CreatedBy + '%' and Department=@Department";
                // SqlDataSource1.SelectCommand = "select j.Id as Id,Year,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,Comments from Journal_IF_Details j,Journal_M k where j.Id=k.Id and j.Id='" + txtJid.Text.Trim() + "'";
            }
            else if (PubIDSearch.Text == "" && drpPubStatusSearch.SelectedValue == "A" && TextBoxWorkItemSearch.Text == "" && TextBoxEnteredBySearch.Text != ""
           && DropDownListInst.SelectedValue != "A" && DropDownListDept.SelectedValue == "A")
            {
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("TypeOfEntry", EntryTypesearch.SelectedValue);
                SqlDataSource1.SelectParameters.Add("CreatedBy", TextBoxEnteredBySearch.Text);
                SqlDataSource1.SelectParameters.Add("Institution", DropDownListInst.SelectedValue);
                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName,t.EntryName,TitleWorkItem,c.PubCatName,ISStudentAuthor from Publication p,Status_Publication_M s, PubMUCategorization_M c ,PublicationTypeEntry_M t where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization and   TypeOfEntry=@TypeOfEntry and CreatedBy like '%' + @CreatedBy + '%'  and Institution=@Institution";
                // SqlDataSource1.SelectCommand = "select j.Id as Id,Year,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,Comments from Journal_IF_Details j,Journal_M k where j.Id=k.Id and j.Id='" + txtJid.Text.Trim() + "'";
            }
            else if (PubIDSearch.Text == "" && drpPubStatusSearch.SelectedValue == "A" && TextBoxWorkItemSearch.Text == "" && TextBoxEnteredBySearch.Text != ""
  && DropDownListInst.SelectedValue != "A" && DropDownListDept.SelectedValue != "A")
            {
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("TypeOfEntry", EntryTypesearch.SelectedValue);
                SqlDataSource1.SelectParameters.Add("CreatedBy", TextBoxEnteredBySearch.Text);
                SqlDataSource1.SelectParameters.Add("Institution", DropDownListInst.SelectedValue);
                SqlDataSource1.SelectParameters.Add("Department", DropDownListDept.SelectedValue);

                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName,t.EntryName,TitleWorkItem,c.PubCatName,ISStudentAuthor from Publication p,Status_Publication_M s, PubMUCategorization_M c ,PublicationTypeEntry_M t where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization and   TypeOfEntry=@TypeOfEntry and CreatedBy like '%' + @CreatedBy + '%'  and Institution=@Institution and Department=@Department";
                // SqlDataSource1.SelectCommand = "select j.Id as Id,Year,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,Comments from Journal_IF_Details j,Journal_M k where j.Id=k.Id and j.Id='" + txtJid.Text.Trim() + "'";
            }
            else if (PubIDSearch.Text == "" && drpPubStatusSearch.SelectedValue == "A" && TextBoxWorkItemSearch.Text != "" && TextBoxEnteredBySearch.Text == ""
                && DropDownListInst.SelectedValue == "A" && DropDownListDept.SelectedValue == "A")
            {
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("TypeOfEntry", EntryTypesearch.SelectedValue);
                SqlDataSource1.SelectParameters.Add("TitleWorkItem", TextBoxWorkItemSearch.Text);

                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName,t.EntryName,TitleWorkItem,c.PubCatName,ISStudentAuthor from Publication p,Status_Publication_M s, PubMUCategorization_M c ,PublicationTypeEntry_M t where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization and   TypeOfEntry=@TypeOfEntry and TitleWorkItem like '%' + @TitleWorkItem + '%'";
                // SqlDataSource1.SelectCommand = "select j.Id as Id,Year,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,Comments from Journal_IF_Details j,Journal_M k where j.Id=k.Id and j.Id='" + txtJid.Text.Trim() + "'";
            }
            else if (PubIDSearch.Text == "" && drpPubStatusSearch.SelectedValue == "A" && TextBoxWorkItemSearch.Text != "" && TextBoxEnteredBySearch.Text == ""
         && DropDownListInst.SelectedValue == "A" && DropDownListDept.SelectedValue != "A")
            {
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("TypeOfEntry", EntryTypesearch.SelectedValue);
                SqlDataSource1.SelectParameters.Add("TitleWorkItem", TextBoxWorkItemSearch.Text);
                SqlDataSource1.SelectParameters.Add("Department", DropDownListDept.SelectedValue);

                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName,t.EntryName,TitleWorkItem,c.PubCatName,ISStudentAuthor from Publication p,Status_Publication_M s, PubMUCategorization_M c ,PublicationTypeEntry_M t where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization and   TypeOfEntry=@TypeOfEntry and TitleWorkItem like '%' + @TitleWorkItem + '%' and Department=@Department";
                // SqlDataSource1.SelectCommand = "select j.Id as Id,Year,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,Comments from Journal_IF_Details j,Journal_M k where j.Id=k.Id and j.Id='" + txtJid.Text.Trim() + "'";
            }
            else if (PubIDSearch.Text == "" && drpPubStatusSearch.SelectedValue == "A" && TextBoxWorkItemSearch.Text != "" && TextBoxEnteredBySearch.Text == ""
    && DropDownListInst.SelectedValue != "A" && DropDownListDept.SelectedValue == "A")
            {
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("TypeOfEntry", EntryTypesearch.SelectedValue);
                SqlDataSource1.SelectParameters.Add("TitleWorkItem", TextBoxWorkItemSearch.Text);
                SqlDataSource1.SelectParameters.Add("Institution", DropDownListInst.SelectedValue);
                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName,t.EntryName,TitleWorkItem,c.PubCatName,ISStudentAuthor from Publication p,Status_Publication_M s, PubMUCategorization_M c ,PublicationTypeEntry_M t where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization and   TypeOfEntry=@TypeOfEntry and TitleWorkItem like '%' + @TitleWorkItem + '%'  and Institution=@Institution";
                // SqlDataSource1.SelectCommand = "select j.Id as Id,Year,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,Comments from Journal_IF_Details j,Journal_M k where j.Id=k.Id and j.Id='" + txtJid.Text.Trim() + "'";
            }
            else if (PubIDSearch.Text == "" && drpPubStatusSearch.SelectedValue == "A" && TextBoxWorkItemSearch.Text != "" && TextBoxEnteredBySearch.Text == ""
&& DropDownListInst.SelectedValue != "A" && DropDownListDept.SelectedValue != "A")
            {
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("TypeOfEntry", EntryTypesearch.SelectedValue);
                SqlDataSource1.SelectParameters.Add("TitleWorkItem", TextBoxWorkItemSearch.Text);
                SqlDataSource1.SelectParameters.Add("Institution", DropDownListInst.SelectedValue);
                SqlDataSource1.SelectParameters.Add("Department", DropDownListDept.SelectedValue);

                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName,t.EntryName,TitleWorkItem,c.PubCatName,ISStudentAuthor from Publication p,Status_Publication_M s, PubMUCategorization_M c ,PublicationTypeEntry_M t where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization and   TypeOfEntry=@TypeOfEntry and TitleWorkItem like '%' + @TitleWorkItem + '%'  and Institution=@Institution and Department=@Department";
                // SqlDataSource1.SelectCommand = "select j.Id as Id,Year,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,Comments from Journal_IF_Details j,Journal_M k where j.Id=k.Id and j.Id='" + txtJid.Text.Trim() + "'";
            }
            else if (PubIDSearch.Text == "" && drpPubStatusSearch.SelectedValue == "A" && TextBoxWorkItemSearch.Text != "" && TextBoxEnteredBySearch.Text != ""
                && DropDownListInst.SelectedValue == "A" && DropDownListDept.SelectedValue == "A")
            {
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("TypeOfEntry", EntryTypesearch.SelectedValue);
                SqlDataSource1.SelectParameters.Add("TitleWorkItem", TextBoxWorkItemSearch.Text);
                SqlDataSource1.SelectParameters.Add("CreatedBy", TextBoxEnteredBySearch.Text);

                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName,t.EntryName,TitleWorkItem,c.PubCatName,ISStudentAuthor from Publication p,Status_Publication_M s, PubMUCategorization_M c ,PublicationTypeEntry_M t where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization and   TypeOfEntry=@TypeOfEntry and TitleWorkItem like '%' + @TitleWorkItem + '%' and CreatedBy like '%' + @CreatedBy + '%'";
                // SqlDataSource1.SelectCommand = "select j.Id as Id,Year,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,Comments from Journal_IF_Details j,Journal_M k where j.Id=k.Id and j.Id='" + txtJid.Text.Trim() + "'";
            }
            else if (PubIDSearch.Text == "" && drpPubStatusSearch.SelectedValue == "A" && TextBoxWorkItemSearch.Text != "" && TextBoxEnteredBySearch.Text != ""
       && DropDownListInst.SelectedValue == "A" && DropDownListDept.SelectedValue != "A")
            {
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("TypeOfEntry", EntryTypesearch.SelectedValue);
                SqlDataSource1.SelectParameters.Add("TitleWorkItem", TextBoxWorkItemSearch.Text);
                SqlDataSource1.SelectParameters.Add("CreatedBy", TextBoxEnteredBySearch.Text);
                SqlDataSource1.SelectParameters.Add("Department", DropDownListDept.SelectedValue);

                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName,t.EntryName,TitleWorkItem,c.PubCatName,ISStudentAuthor from Publication p,Status_Publication_M s, PubMUCategorization_M c ,PublicationTypeEntry_M t where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization and   TypeOfEntry=@TypeOfEntry and TitleWorkItem like '%' + @TitleWorkItem + '%' and CreatedBy like '%' + @CreatedBy + '%' and Department=@Department";
                // SqlDataSource1.SelectCommand = "select j.Id as Id,Year,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,Comments from Journal_IF_Details j,Journal_M k where j.Id=k.Id and j.Id='" + txtJid.Text.Trim() + "'";
            }
            else if (PubIDSearch.Text == "" && drpPubStatusSearch.SelectedValue == "A" && TextBoxWorkItemSearch.Text != "" && TextBoxEnteredBySearch.Text != ""
           && DropDownListInst.SelectedValue != "A" && DropDownListDept.SelectedValue == "A")
            {
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("TypeOfEntry", EntryTypesearch.SelectedValue);
                SqlDataSource1.SelectParameters.Add("TitleWorkItem", TextBoxWorkItemSearch.Text);
                SqlDataSource1.SelectParameters.Add("CreatedBy", TextBoxEnteredBySearch.Text);
                SqlDataSource1.SelectParameters.Add("Institution", DropDownListInst.SelectedValue);

                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName,t.EntryName,TitleWorkItem,c.PubCatName,ISStudentAuthor from Publication p,Status_Publication_M s, PubMUCategorization_M c ,PublicationTypeEntry_M t where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization and   TypeOfEntry=@TypeOfEntry and TitleWorkItem like '%' + @TitleWorkItem + '%' and CreatedBy like '%' + @CreatedBy + '%'  and Institution=@Institution";
                // SqlDataSource1.SelectCommand = "select j.Id as Id,Year,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,Comments from Journal_IF_Details j,Journal_M k where j.Id=k.Id and j.Id='" + txtJid.Text.Trim() + "'";
            }
            else if (PubIDSearch.Text == "" && drpPubStatusSearch.SelectedValue == "A" && TextBoxWorkItemSearch.Text != "" && TextBoxEnteredBySearch.Text != ""
&& DropDownListInst.SelectedValue != "A" && DropDownListDept.SelectedValue != "A")
            {
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("TypeOfEntry", EntryTypesearch.SelectedValue);
                SqlDataSource1.SelectParameters.Add("TitleWorkItem", TextBoxWorkItemSearch.Text);
                SqlDataSource1.SelectParameters.Add("CreatedBy", TextBoxEnteredBySearch.Text);
                SqlDataSource1.SelectParameters.Add("Institution", DropDownListInst.SelectedValue);
                SqlDataSource1.SelectParameters.Add("Department", DropDownListDept.SelectedValue);

                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName,t.EntryName,TitleWorkItem,c.PubCatName,ISStudentAuthor from Publication p,Status_Publication_M s, PubMUCategorization_M c ,PublicationTypeEntry_M t where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization and   TypeOfEntry=@TypeOfEntry and TitleWorkItem like '%' + @TitleWorkItem + '%' and CreatedBy like '%' + @CreatedBy + '%'  and Institution=@Institution and Department=@Department";
                // SqlDataSource1.SelectCommand = "select j.Id as Id,Year,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,Comments from Journal_IF_Details j,Journal_M k where j.Id=k.Id and j.Id='" + txtJid.Text.Trim() + "'";
            }
            else if (PubIDSearch.Text != "" && drpPubStatusSearch.SelectedValue == "A" && TextBoxWorkItemSearch.Text == "" && TextBoxEnteredBySearch.Text == ""
                && DropDownListInst.SelectedValue == "A" && DropDownListDept.SelectedValue == "A")
            {
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("TypeOfEntry", EntryTypesearch.SelectedValue);
                SqlDataSource1.SelectParameters.Add("PublicationID", PubIDSearch.Text.Trim());

                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName ,t.EntryName,TitleWorkItem,c.PubCatName,ISStudentAuthor from Publication p,Status_Publication_M s, PubMUCategorization_M c ,PublicationTypeEntry_M t where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization and TypeOfEntry=@TypeOfEntry  and PublicationID like '%' + @PublicationID + '%' ";
                // SqlDataSource1.SelectCommand = "select j.Id as Id,Year,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,Comments from Journal_IF_Details j,Journal_M k where j.Id=k.Id and j.Id='" + txtJid.Text.Trim() + "'";
            }
            else if (PubIDSearch.Text != "" && drpPubStatusSearch.SelectedValue == "A" && TextBoxWorkItemSearch.Text == "" && TextBoxEnteredBySearch.Text == ""
        && DropDownListInst.SelectedValue == "A" && DropDownListDept.SelectedValue != "A")
            {
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("TypeOfEntry", EntryTypesearch.SelectedValue);
                SqlDataSource1.SelectParameters.Add("PublicationID", PubIDSearch.Text.Trim());
                SqlDataSource1.SelectParameters.Add("Department", DropDownListDept.SelectedValue);

                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName ,t.EntryName,TitleWorkItem,c.PubCatName,ISStudentAuthor from Publication p,Status_Publication_M s, PubMUCategorization_M c ,PublicationTypeEntry_M t where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization and TypeOfEntry=@TypeOfEntry  and PublicationID like '%' + @PublicationID + '%' and Department=@Department";
                // SqlDataSource1.SelectCommand = "select j.Id as Id,Year,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,Comments from Journal_IF_Details j,Journal_M k where j.Id=k.Id and j.Id='" + txtJid.Text.Trim() + "'";
            }
            else if (PubIDSearch.Text != "" && drpPubStatusSearch.SelectedValue == "A" && TextBoxWorkItemSearch.Text == "" && TextBoxEnteredBySearch.Text == ""
                && DropDownListInst.SelectedValue != "A" && DropDownListDept.SelectedValue == "A") 
            {
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("TypeOfEntry", EntryTypesearch.SelectedValue);
                SqlDataSource1.SelectParameters.Add("PublicationID", PubIDSearch.Text.Trim());
                SqlDataSource1.SelectParameters.Add("Institution", DropDownListInst.SelectedValue);

                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName ,t.EntryName,TitleWorkItem,c.PubCatName,ISStudentAuthor from Publication p,Status_Publication_M s, PubMUCategorization_M c ,PublicationTypeEntry_M t where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization and TypeOfEntry=@TypeOfEntry  and PublicationID like '%' + @PublicationID + '%'  and Institution=@Institution";
                // SqlDataSource1.SelectCommand = "select j.Id as Id,Year,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,Comments from Journal_IF_Details j,Journal_M k where j.Id=k.Id and j.Id='" + txtJid.Text.Trim() + "'";
            }
            else if (PubIDSearch.Text != "" && drpPubStatusSearch.SelectedValue == "A" && TextBoxWorkItemSearch.Text == "" && TextBoxEnteredBySearch.Text == ""
      && DropDownListInst.SelectedValue != "A" && DropDownListDept.SelectedValue != "A")
            {
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("TypeOfEntry", EntryTypesearch.SelectedValue);
                SqlDataSource1.SelectParameters.Add("PublicationID", PubIDSearch.Text.Trim());
                SqlDataSource1.SelectParameters.Add("Institution", DropDownListInst.SelectedValue);
                SqlDataSource1.SelectParameters.Add("Department", DropDownListDept.SelectedValue);

                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName ,t.EntryName,TitleWorkItem,c.PubCatName,ISStudentAuthor from Publication p,Status_Publication_M s, PubMUCategorization_M c ,PublicationTypeEntry_M t where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization and TypeOfEntry=@TypeOfEntry  and PublicationID like '%' + @PublicationID + '%'  and Institution=@Institution and Department=@Department";
                // SqlDataSource1.SelectCommand = "select j.Id as Id,Year,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,Comments from Journal_IF_Details j,Journal_M k where j.Id=k.Id and j.Id='" + txtJid.Text.Trim() + "'";
            }
            else if (PubIDSearch.Text != "" && drpPubStatusSearch.SelectedValue == "A" && TextBoxWorkItemSearch.Text == "" && TextBoxEnteredBySearch.Text != ""
                && DropDownListInst.SelectedValue == "A" && DropDownListDept.SelectedValue == "A")
            {
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("TypeOfEntry", EntryTypesearch.SelectedValue);
                SqlDataSource1.SelectParameters.Add("PublicationID", PubIDSearch.Text.Trim());
                SqlDataSource1.SelectParameters.Add("CreatedBy", TextBoxEnteredBySearch.Text);

                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName ,t.EntryName,TitleWorkItem,c.PubCatName,ISStudentAuthor from Publication p,Status_Publication_M s, PubMUCategorization_M c ,PublicationTypeEntry_M t where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization and TypeOfEntry=@TypeOfEntry  and PublicationID like '%' + @PublicationID + '%' and CreatedBy like '%' + @CreatedBy + '%'";
                // SqlDataSource1.SelectCommand = "select j.Id as Id,Year,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,Comments from Journal_IF_Details j,Journal_M k where j.Id=k.Id and j.Id='" + txtJid.Text.Trim() + "'";
            }
            else if (PubIDSearch.Text != "" && drpPubStatusSearch.SelectedValue == "A" && TextBoxWorkItemSearch.Text == "" && TextBoxEnteredBySearch.Text != ""
       && DropDownListInst.SelectedValue == "A" && DropDownListDept.SelectedValue != "A")
            {
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("CreatedBy", TextBoxEnteredBySearch.Text);
                SqlDataSource1.SelectParameters.Add("Department", DropDownListDept.SelectedValue);
                SqlDataSource1.SelectParameters.Add("PublicationID", PubIDSearch.Text.Trim());
                SqlDataSource1.SelectParameters.Add("TypeOfEntry", EntryTypesearch.SelectedValue);

                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName ,t.EntryName,TitleWorkItem,c.PubCatName,ISStudentAuthor from Publication p,Status_Publication_M s, PubMUCategorization_M c ,PublicationTypeEntry_M t where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization and TypeOfEntry=@TypeOfEntry and PublicationID like '%' + @PublicationID + '%' and CreatedBy like '%' + @CreatedBy + '%' and Department=@Department";
                // SqlDataSource1.SelectCommand = "select j.Id as Id,Year,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,Comments from Journal_IF_Details j,Journal_M k where j.Id=k.Id and j.Id='" + txtJid.Text.Trim() + "'";
            }
            else if (PubIDSearch.Text != "" && drpPubStatusSearch.SelectedValue == "A" && TextBoxWorkItemSearch.Text == "" && TextBoxEnteredBySearch.Text != ""
                && DropDownListInst.SelectedValue != "A" && DropDownListDept.SelectedValue == "A")
            {
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("CreatedBy", TextBoxEnteredBySearch.Text);
                SqlDataSource1.SelectParameters.Add("Institution", DropDownListInst.SelectedValue);
                SqlDataSource1.SelectParameters.Add("PublicationID", PubIDSearch.Text.Trim());
                SqlDataSource1.SelectParameters.Add("TypeOfEntry", EntryTypesearch.SelectedValue);
                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName ,t.EntryName,TitleWorkItem,c.PubCatName,ISStudentAuthor from Publication p,Status_Publication_M s, PubMUCategorization_M c ,PublicationTypeEntry_M t where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization and TypeOfEntry=@TypeOfEntry  and PublicationID like '%' + @PublicationID + '%' and CreatedBy like '%' + @CreatedBy + '%'  and Institution=@Institution";
                // SqlDataSource1.SelectCommand = "select j.Id as Id,Year,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,Comments from Journal_IF_Details j,Journal_M k where j.Id=k.Id and j.Id='" + txtJid.Text.Trim() + "'";
            }
            else if (PubIDSearch.Text != "" && drpPubStatusSearch.SelectedValue == "A" && TextBoxWorkItemSearch.Text == "" && TextBoxEnteredBySearch.Text != ""
    && DropDownListInst.SelectedValue != "A" && DropDownListDept.SelectedValue != "A")
            {
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("CreatedBy", TextBoxEnteredBySearch.Text);
                SqlDataSource1.SelectParameters.Add("Institution", DropDownListInst.SelectedValue);
                SqlDataSource1.SelectParameters.Add("PublicationID", PubIDSearch.Text.Trim());
                SqlDataSource1.SelectParameters.Add("TypeOfEntry", EntryTypesearch.SelectedValue);
                SqlDataSource1.SelectParameters.Add("Department", DropDownListDept.SelectedValue);

                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName ,t.EntryName,TitleWorkItem,c.PubCatName,ISStudentAuthor from Publication p,Status_Publication_M s, PubMUCategorization_M c ,PublicationTypeEntry_M t where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization and TypeOfEntry=@TypeOfEntry  and PublicationID like '%' + @PublicationID + '%'  and CreatedBy like '%' + @CreatedBy + '%'  and Institution=@Institution and Department=@Department";
                // SqlDataSource1.SelectCommand = "select j.Id as Id,Year,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,Comments from Journal_IF_Details j,Journal_M k where j.Id=k.Id and j.Id='" + txtJid.Text.Trim() + "'";
            }
            else if (PubIDSearch.Text != "" && drpPubStatusSearch.SelectedValue == "A" && TextBoxWorkItemSearch.Text != "" && TextBoxEnteredBySearch.Text == ""
                && DropDownListInst.SelectedValue == "A" && DropDownListDept.SelectedValue == "A")
            {
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("PublicationID", PubIDSearch.Text.Trim());
                SqlDataSource1.SelectParameters.Add("TitleWorkItem", TextBoxWorkItemSearch.Text);
                SqlDataSource1.SelectParameters.Add("TypeOfEntry", EntryTypesearch.SelectedValue);


                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName ,t.EntryName,TitleWorkItem,c.PubCatName,ISStudentAuthor from Publication p,Status_Publication_M s, PubMUCategorization_M c ,PublicationTypeEntry_M t where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization and TypeOfEntry=@TypeOfEntry  and PublicationID like '%' + @PublicationID + '%' and TitleWorkItem like '%' + @TitleWorkItem + '%' ";
                // SqlDataSource1.SelectCommand = "select j.Id as Id,Year,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,Comments from Journal_IF_Details j,Journal_M k where j.Id=k.Id and j.Id='" + txtJid.Text.Trim() + "'";
            }
            else if (PubIDSearch.Text != "" && drpPubStatusSearch.SelectedValue == "A" && TextBoxWorkItemSearch.Text != "" && TextBoxEnteredBySearch.Text == ""
       && DropDownListInst.SelectedValue == "A" && DropDownListDept.SelectedValue != "A")
            {
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("PublicationID", PubIDSearch.Text.Trim());
                SqlDataSource1.SelectParameters.Add("TitleWorkItem", TextBoxWorkItemSearch.Text);
                SqlDataSource1.SelectParameters.Add("TypeOfEntry", EntryTypesearch.SelectedValue);
                SqlDataSource1.SelectParameters.Add("Department", DropDownListDept.SelectedValue);

                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName ,t.EntryName,TitleWorkItem,c.PubCatName,ISStudentAuthor from Publication p,Status_Publication_M s, PubMUCategorization_M c ,PublicationTypeEntry_M t where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization and TypeOfEntry=@TypeOfEntry  and PublicationID like '%' + @PublicationID + '%' and TitleWorkItem like '%' + @TitleWorkItem + '%' and Department=@Department ";
                // SqlDataSource1.SelectCommand = "select j.Id as Id,Year,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,Comments from Journal_IF_Details j,Journal_M k where j.Id=k.Id and j.Id='" + txtJid.Text.Trim() + "'";
            }
            else if (PubIDSearch.Text != "" && drpPubStatusSearch.SelectedValue == "A" && TextBoxWorkItemSearch.Text != "" && TextBoxEnteredBySearch.Text == ""
       && DropDownListInst.SelectedValue != "A" && DropDownListDept.SelectedValue == "A")
            {
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("PublicationID", PubIDSearch.Text.Trim());
                SqlDataSource1.SelectParameters.Add("TitleWorkItem", TextBoxWorkItemSearch.Text);
                SqlDataSource1.SelectParameters.Add("TypeOfEntry", EntryTypesearch.SelectedValue);
                SqlDataSource1.SelectParameters.Add("Institution", DropDownListInst.SelectedValue);

                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName ,t.EntryName,TitleWorkItem,c.PubCatName,ISStudentAuthor from Publication p,Status_Publication_M s, PubMUCategorization_M c ,PublicationTypeEntry_M t where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization and TypeOfEntry=@TypeOfEntry  and PublicationID like '%' + @PublicationID + '%' and TitleWorkItem like '%' + @TitleWorkItem + '%'  and Institution=@Institution";
                // SqlDataSource1.SelectCommand = "select j.Id as Id,Year,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,Comments from Journal_IF_Details j,Journal_M k where j.Id=k.Id and j.Id='" + txtJid.Text.Trim() + "'";
            }
            else if (PubIDSearch.Text != "" && drpPubStatusSearch.SelectedValue == "A" && TextBoxWorkItemSearch.Text != "" && TextBoxEnteredBySearch.Text == ""
&& DropDownListInst.SelectedValue != "A" && DropDownListDept.SelectedValue != "A")
            {
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("PublicationID", PubIDSearch.Text.Trim());
                SqlDataSource1.SelectParameters.Add("TitleWorkItem", TextBoxWorkItemSearch.Text);
                SqlDataSource1.SelectParameters.Add("TypeOfEntry", EntryTypesearch.SelectedValue);
                SqlDataSource1.SelectParameters.Add("Department", DropDownListDept.SelectedValue);
                SqlDataSource1.SelectParameters.Add("Institution", DropDownListInst.SelectedValue);

                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName ,t.EntryName,TitleWorkItem,c.PubCatName,ISStudentAuthor from Publication p,Status_Publication_M s, PubMUCategorization_M c ,PublicationTypeEntry_M t where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization and TypeOfEntry=@TypeOfEntry  and PublicationID like '%' + @PublicationID + '%' and TitleWorkItem like '%' + @TitleWorkItem + '%'  and Institution=@Institution and Department=@Department";
                // SqlDataSource1.SelectCommand = "select j.Id as Id,Year,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,Comments from Journal_IF_Details j,Journal_M k where j.Id=k.Id and j.Id='" + txtJid.Text.Trim() + "'";
            }
            else if (PubIDSearch.Text != "" && drpPubStatusSearch.SelectedValue == "A" && TextBoxWorkItemSearch.Text != "" && TextBoxEnteredBySearch.Text != ""
                && DropDownListInst.SelectedValue == "A" && DropDownListDept.SelectedValue == "A")
            {
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("PublicationID", PubIDSearch.Text.Trim());
                SqlDataSource1.SelectParameters.Add("TitleWorkItem", TextBoxWorkItemSearch.Text);
                SqlDataSource1.SelectParameters.Add("TypeOfEntry", EntryTypesearch.SelectedValue);
                SqlDataSource1.SelectParameters.Add("CreatedBy", TextBoxEnteredBySearch.Text);

                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName ,t.EntryName,TitleWorkItem,c.PubCatName,ISStudentAuthor from Publication p,Status_Publication_M s, PubMUCategorization_M c ,PublicationTypeEntry_M t where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization and TypeOfEntry=@TypeOfEntry  and PublicationID like '%' + @PublicationID + '%' and TitleWorkItem like '%' + @TitleWorkItem + '%' and CreatedBy like '%' + @CreatedBy + '%'";
                // SqlDataSource1.SelectCommand = "select j.Id as Id,Year,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,Comments from Journal_IF_Details j,Journal_M k where j.Id=k.Id and j.Id='" + txtJid.Text.Trim() + "'";
            }
            else if (PubIDSearch.Text != "" && drpPubStatusSearch.SelectedValue == "A" && TextBoxWorkItemSearch.Text != "" && TextBoxEnteredBySearch.Text != ""
&& DropDownListInst.SelectedValue == "A" && DropDownListDept.SelectedValue != "A")
            {
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("PublicationID", PubIDSearch.Text.Trim());
                SqlDataSource1.SelectParameters.Add("TitleWorkItem", TextBoxWorkItemSearch.Text);
                SqlDataSource1.SelectParameters.Add("TypeOfEntry", EntryTypesearch.SelectedValue);
                SqlDataSource1.SelectParameters.Add("CreatedBy", TextBoxEnteredBySearch.Text);
                SqlDataSource1.SelectParameters.Add("Department", DropDownListDept.SelectedValue);

                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName ,t.EntryName,TitleWorkItem,c.PubCatName,ISStudentAuthor from Publication p,Status_Publication_M s, PubMUCategorization_M c ,PublicationTypeEntry_M t where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization and TypeOfEntry=@TypeOfEntry  and PublicationID like '%' + @PublicationID + '%' and TitleWorkItem like '%' + @TitleWorkItem + '%' and CreatedBy like '%' + @CreatedBy + '%' and Department=@Department";
                // SqlDataSource1.SelectCommand = "select j.Id as Id,Year,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,Comments from Journal_IF_Details j,Journal_M k where j.Id=k.Id and j.Id='" + txtJid.Text.Trim() + "'";
            }
            else if (PubIDSearch.Text != "" && drpPubStatusSearch.SelectedValue == "A" && TextBoxWorkItemSearch.Text != "" && TextBoxEnteredBySearch.Text != ""
                && DropDownListInst.SelectedValue != "A" && DropDownListDept.SelectedValue == "A")
            {
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("Institution", DropDownListInst.SelectedValue);
                SqlDataSource1.SelectParameters.Add("PublicationID", PubIDSearch.Text.Trim());
                SqlDataSource1.SelectParameters.Add("TitleWorkItem", TextBoxWorkItemSearch.Text);
                SqlDataSource1.SelectParameters.Add("TypeOfEntry", EntryTypesearch.SelectedValue);
                SqlDataSource1.SelectParameters.Add("CreatedBy", TextBoxEnteredBySearch.Text);
                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName ,t.EntryName,TitleWorkItem,c.PubCatName,ISStudentAuthor from Publication p,Status_Publication_M s, PubMUCategorization_M c ,PublicationTypeEntry_M t where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization and TypeOfEntry=@TypeOfEntry and PublicationID like '%' + @PublicationID + '%' and TitleWorkItem like '%' + @TitleWorkItem + '%' and CreatedBy like '%' + @CreatedBy + '%'  and Institution=@Institution";
                // SqlDataSource1.SelectCommand = "select j.Id as Id,Year,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,Comments from Journal_IF_Details j,Journal_M k where j.Id=k.Id and j.Id='" + txtJid.Text.Trim() + "'";
            }
            else if (PubIDSearch.Text != "" && drpPubStatusSearch.SelectedValue == "A" && TextBoxWorkItemSearch.Text != "" && TextBoxEnteredBySearch.Text != ""
           && DropDownListInst.SelectedValue != "A" && DropDownListDept.SelectedValue != "A")
            {
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("Institution", DropDownListInst.SelectedValue);
                SqlDataSource1.SelectParameters.Add("CreatedBy", TextBoxEnteredBySearch.Text);
                SqlDataSource1.SelectParameters.Add("PublicationID", PubIDSearch.Text.Trim());
                SqlDataSource1.SelectParameters.Add("TitleWorkItem", TextBoxWorkItemSearch.Text);
                SqlDataSource1.SelectParameters.Add("TypeOfEntry", EntryTypesearch.SelectedValue);
                SqlDataSource1.SelectParameters.Add("Department", DropDownListDept.SelectedValue);

                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName ,t.EntryName,TitleWorkItem,c.PubCatName,ISStudentAuthor from Publication p,Status_Publication_M s, PubMUCategorization_M c ,PublicationTypeEntry_M t where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization and TypeOfEntry=@TypeOfEntry  and PublicationID like '%' + @PublicationID + '%' and TitleWorkItem like '%' + @TitleWorkItem + '%' and CreatedBy like '%' + @CreatedBy + '%'  and Institution=@Institution and Department=@Department";
                // SqlDataSource1.SelectCommand = "select j.Id as Id,Year,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,Comments from Journal_IF_Details j,Journal_M k where j.Id=k.Id and j.Id='" + txtJid.Text.Trim() + "'";
            }
            else if (PubIDSearch.Text != "" && drpPubStatusSearch.SelectedValue != "A" && TextBoxWorkItemSearch.Text == "" && TextBoxEnteredBySearch.Text == ""
                && DropDownListInst.SelectedValue == "A" && DropDownListDept.SelectedValue == "A")
            {
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("TypeOfEntry", EntryTypesearch.SelectedValue);
                SqlDataSource1.SelectParameters.Add("PublicationID", PubIDSearch.Text.Trim());
                SqlDataSource1.SelectParameters.Add("Status", drpPubStatusSearch.SelectedValue);

                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor ,s.StatusName ,t.EntryName,TitleWorkItem,c.PubCatName,ISStudentAuthor from Publication p,Status_Publication_M s, PubMUCategorization_M c ,PublicationTypeEntry_M t where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization and  TypeOfEntry=@TypeOfEntry and Status=@Status  and PublicationID like '%' + @PublicationID + '%' ";
                // SqlDataSource1.SelectCommand = "select j.Id as Id,Year,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,Comments from Journal_IF_Details j,Journal_M k where j.Id=k.Id and j.Id='" + txtJid.Text.Trim() + "'";
            }
            else if (PubIDSearch.Text != "" && drpPubStatusSearch.SelectedValue != "A" && TextBoxWorkItemSearch.Text == "" && TextBoxEnteredBySearch.Text == ""
      && DropDownListInst.SelectedValue == "A" && DropDownListDept.SelectedValue != "A")
            {
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("TypeOfEntry", EntryTypesearch.SelectedValue);
                SqlDataSource1.SelectParameters.Add("PublicationID", PubIDSearch.Text.Trim());
                SqlDataSource1.SelectParameters.Add("Status", drpPubStatusSearch.SelectedValue);
                SqlDataSource1.SelectParameters.Add("Department", DropDownListDept.SelectedValue);

                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor ,s.StatusName ,t.EntryName,TitleWorkItem,c.PubCatName,ISStudentAuthor from Publication p,Status_Publication_M s, PubMUCategorization_M c ,PublicationTypeEntry_M t where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization and  TypeOfEntry=@TypeOfEntry and Status=@Status  and PublicationID like '%' + @PublicationID + '%' and Department=@Department";
                // SqlDataSource1.SelectCommand = "select j.Id as Id,Year,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,Comments from Journal_IF_Details j,Journal_M k where j.Id=k.Id and j.Id='" + txtJid.Text.Trim() + "'";
            }
            else if (PubIDSearch.Text != "" && drpPubStatusSearch.SelectedValue != "A" && TextBoxWorkItemSearch.Text == "" && TextBoxEnteredBySearch.Text == ""
                && DropDownListInst.SelectedValue != "A" && DropDownListDept.SelectedValue == "A")
            {
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("TypeOfEntry", EntryTypesearch.SelectedValue);
                SqlDataSource1.SelectParameters.Add("PublicationID", PubIDSearch.Text.Trim());
                SqlDataSource1.SelectParameters.Add("Status", drpPubStatusSearch.SelectedValue);
                SqlDataSource1.SelectParameters.Add("Institution", DropDownListInst.SelectedValue);
                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor ,s.StatusName ,t.EntryName,TitleWorkItem,c.PubCatName,ISStudentAuthor from Publication p,Status_Publication_M s, PubMUCategorization_M c ,PublicationTypeEntry_M t where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization and  TypeOfEntry=@TypeOfEntry and Status=@Status  and PublicationID like '%' + @PublicationID + '%'  and Institution=@Institution";
                // SqlDataSource1.SelectCommand = "select j.Id as Id,Year,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,Comments from Journal_IF_Details j,Journal_M k where j.Id=k.Id and j.Id='" + txtJid.Text.Trim() + "'";
            }
            else if (PubIDSearch.Text != "" && drpPubStatusSearch.SelectedValue != "A" && TextBoxWorkItemSearch.Text == "" && TextBoxEnteredBySearch.Text == ""
      && DropDownListInst.SelectedValue != "A" && DropDownListDept.SelectedValue != "A")
            {
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("TypeOfEntry", EntryTypesearch.SelectedValue);
                SqlDataSource1.SelectParameters.Add("PublicationID", PubIDSearch.Text.Trim());
                SqlDataSource1.SelectParameters.Add("Status", drpPubStatusSearch.SelectedValue);
                SqlDataSource1.SelectParameters.Add("Institution", DropDownListInst.SelectedValue);
                SqlDataSource1.SelectParameters.Add("Department", DropDownListDept.SelectedValue);

                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor ,s.StatusName ,t.EntryName,TitleWorkItem,c.PubCatName,ISStudentAuthor from Publication p,Status_Publication_M s, PubMUCategorization_M c ,PublicationTypeEntry_M t where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization and  TypeOfEntry=@TypeOfEntry and Status=@Status  and PublicationID like '%' + @PublicationID + '%'  and Institution=@Institution and Department=@Department";
                // SqlDataSource1.SelectCommand = "select j.Id as Id,Year,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,Comments from Journal_IF_Details j,Journal_M k where j.Id=k.Id and j.Id='" + txtJid.Text.Trim() + "'";
            }
            else if (PubIDSearch.Text != "" && drpPubStatusSearch.SelectedValue != "A" && TextBoxWorkItemSearch.Text == "" && TextBoxEnteredBySearch.Text != ""
                && DropDownListInst.SelectedValue == "A" && DropDownListDept.SelectedValue == "A")
            {
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("TypeOfEntry", EntryTypesearch.SelectedValue);
                SqlDataSource1.SelectParameters.Add("PublicationID", PubIDSearch.Text.Trim());
                SqlDataSource1.SelectParameters.Add("Status", drpPubStatusSearch.SelectedValue);
                SqlDataSource1.SelectParameters.Add("CreatedBy", TextBoxEnteredBySearch.Text);

                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor ,s.StatusName ,t.EntryName,TitleWorkItem,c.PubCatName,ISStudentAuthor from Publication p,Status_Publication_M s, PubMUCategorization_M c ,PublicationTypeEntry_M t where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization and  TypeOfEntry=@TypeOfEntry and Status=@Status and PublicationID like '%' + @PublicationID + '%' and CreatedBy like '%' + @CreatedBy + '%' ";
                // SqlDataSource1.SelectCommand = "select j.Id as Id,Year,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,Comments from Journal_IF_Details j,Journal_M k where j.Id=k.Id and j.Id='" + txtJid.Text.Trim() + "'";
            }
            else if (PubIDSearch.Text != "" && drpPubStatusSearch.SelectedValue != "A" && TextBoxWorkItemSearch.Text == "" && TextBoxEnteredBySearch.Text != ""
       && DropDownListInst.SelectedValue == "A" && DropDownListDept.SelectedValue != "A")
            {
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("TypeOfEntry", EntryTypesearch.SelectedValue);
                SqlDataSource1.SelectParameters.Add("PublicationID", PubIDSearch.Text.Trim());
                SqlDataSource1.SelectParameters.Add("Status", drpPubStatusSearch.SelectedValue);
                SqlDataSource1.SelectParameters.Add("CreatedBy", TextBoxEnteredBySearch.Text);
                SqlDataSource1.SelectParameters.Add("Department", DropDownListDept.SelectedValue);

                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor ,s.StatusName ,t.EntryName,TitleWorkItem,c.PubCatName,ISStudentAuthor from Publication p,Status_Publication_M s, PubMUCategorization_M c ,PublicationTypeEntry_M t where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization and  TypeOfEntry=@TypeOfEntry and Status=@Status  and PublicationID like '%' + @PublicationID + '%' and CreatedBy like '%' + @CreatedBy + '%' and Department=@Department";
                // SqlDataSource1.SelectCommand = "select j.Id as Id,Year,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,Comments from Journal_IF_Details j,Journal_M k where j.Id=k.Id and j.Id='" + txtJid.Text.Trim() + "'";
            }
            else if (PubIDSearch.Text != "" && drpPubStatusSearch.SelectedValue != "A" && TextBoxWorkItemSearch.Text == "" && TextBoxEnteredBySearch.Text != ""
                && DropDownListInst.SelectedValue != "A" && DropDownListDept.SelectedValue == "A")
            {
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("TypeOfEntry", EntryTypesearch.SelectedValue);
                SqlDataSource1.SelectParameters.Add("PublicationID", PubIDSearch.Text.Trim());
                SqlDataSource1.SelectParameters.Add("Status", drpPubStatusSearch.SelectedValue);
                SqlDataSource1.SelectParameters.Add("CreatedBy", TextBoxEnteredBySearch.Text);
                SqlDataSource1.SelectParameters.Add("Institution", DropDownListInst.SelectedValue);

                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor ,s.StatusName ,t.EntryName,TitleWorkItem,c.PubCatName,ISStudentAuthor from Publication p,Status_Publication_M s, PubMUCategorization_M c ,PublicationTypeEntry_M t where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization and  TypeOfEntry=@TypeOfEntry and Status=@Status  and PublicationID like '%' + @PublicationID + '%' and CreatedBy like '%' + @CreatedBy + '%'  and Institution=@Institution";
                // SqlDataSource1.SelectCommand = "select j.Id as Id,Year,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,Comments from Journal_IF_Details j,Journal_M k where j.Id=k.Id and j.Id='" + txtJid.Text.Trim() + "'";
            }
            else if (PubIDSearch.Text != "" && drpPubStatusSearch.SelectedValue != "A" && TextBoxWorkItemSearch.Text == "" && TextBoxEnteredBySearch.Text != ""
      && DropDownListInst.SelectedValue != "A" && DropDownListDept.SelectedValue != "A")
            {
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("TypeOfEntry", EntryTypesearch.SelectedValue);
                SqlDataSource1.SelectParameters.Add("PublicationID", PubIDSearch.Text.Trim());
                SqlDataSource1.SelectParameters.Add("Status", drpPubStatusSearch.SelectedValue);
                SqlDataSource1.SelectParameters.Add("CreatedBy", TextBoxEnteredBySearch.Text);
                SqlDataSource1.SelectParameters.Add("Institution", DropDownListInst.SelectedValue);
                SqlDataSource1.SelectParameters.Add("Department", DropDownListDept.SelectedValue);

                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor ,s.StatusName ,t.EntryName,TitleWorkItem,c.PubCatName,ISStudentAuthor from Publication p,Status_Publication_M s, PubMUCategorization_M c ,PublicationTypeEntry_M t where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization and  TypeOfEntry=@TypeOfEntry and Status=@Status  and PublicationID like '%' + @PublicationID + '%'  and CreatedBy like '%' + @CreatedBy + '%'  and Institution=@Institution and Department=@Department";
                // SqlDataSource1.SelectCommand = "select j.Id as Id,Year,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,Comments from Journal_IF_Details j,Journal_M k where j.Id=k.Id and j.Id='" + txtJid.Text.Trim() + "'";
            }
            else if (PubIDSearch.Text != "" && drpPubStatusSearch.SelectedValue != "A" && TextBoxWorkItemSearch.Text != "" && TextBoxEnteredBySearch.Text == ""
                && DropDownListInst.SelectedValue == "A" && DropDownListDept.SelectedValue == "A")
            {
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("TypeOfEntry", EntryTypesearch.SelectedValue);
                SqlDataSource1.SelectParameters.Add("Status", drpPubStatusSearch.SelectedValue);
                SqlDataSource1.SelectParameters.Add("PublicationID", PubIDSearch.Text.Trim());
                SqlDataSource1.SelectParameters.Add("TitleWorkItem", TextBoxWorkItemSearch.Text);

                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor ,s.StatusName ,t.EntryName,TitleWorkItem,c.PubCatName,ISStudentAuthor from Publication p,Status_Publication_M s, PubMUCategorization_M c ,PublicationTypeEntry_M t where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization and  TypeOfEntry=@TypeOfEntry and Status=@Status  and PublicationID like '%' + @PublicationID + '%' and TitleWorkItem like '%' + @TitleWorkItem + '%'";
                // SqlDataSource1.SelectCommand = "select j.Id as Id,Year,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,Comments from Journal_IF_Details j,Journal_M k where j.Id=k.Id and j.Id='" + txtJid.Text.Trim() + "'";
            }
            else if (PubIDSearch.Text != "" && drpPubStatusSearch.SelectedValue != "A" && TextBoxWorkItemSearch.Text != "" && TextBoxEnteredBySearch.Text == ""
        && DropDownListInst.SelectedValue == "A" && DropDownListDept.SelectedValue != "A")
            {
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("TypeOfEntry", EntryTypesearch.SelectedValue);
                SqlDataSource1.SelectParameters.Add("Status", drpPubStatusSearch.SelectedValue);
                SqlDataSource1.SelectParameters.Add("PublicationID", PubIDSearch.Text.Trim());
                SqlDataSource1.SelectParameters.Add("TitleWorkItem", TextBoxWorkItemSearch.Text);
                SqlDataSource1.SelectParameters.Add("Department", DropDownListDept.SelectedValue);

                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor ,s.StatusName ,t.EntryName,TitleWorkItem,c.PubCatName,ISStudentAuthor from Publication p,Status_Publication_M s, PubMUCategorization_M c ,PublicationTypeEntry_M t where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization and  TypeOfEntry=@TypeOfEntry and Status=@Status  and PublicationID like '%' + @PublicationID + '%' and TitleWorkItem like '%' + @TitleWorkItem + '%' and Department=@Department ";
                // SqlDataSource1.SelectCommand = "select j.Id as Id,Year,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,Comments from Journal_IF_Details j,Journal_M k where j.Id=k.Id and j.Id='" + txtJid.Text.Trim() + "'";
            }
            else if (PubIDSearch.Text != "" && drpPubStatusSearch.SelectedValue != "A" && TextBoxWorkItemSearch.Text != "" && TextBoxEnteredBySearch.Text == ""
                && DropDownListInst.SelectedValue != "A" && DropDownListDept.SelectedValue == "A")
            {
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("TypeOfEntry", EntryTypesearch.SelectedValue);
                SqlDataSource1.SelectParameters.Add("Status", drpPubStatusSearch.SelectedValue);
                SqlDataSource1.SelectParameters.Add("PublicationID", PubIDSearch.Text.Trim());
                SqlDataSource1.SelectParameters.Add("TitleWorkItem", TextBoxWorkItemSearch.Text);
                SqlDataSource1.SelectParameters.Add("Institution", DropDownListInst.SelectedValue);

                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor ,s.StatusName ,t.EntryName,TitleWorkItem,c.PubCatName,ISStudentAuthor from Publication p,Status_Publication_M s, PubMUCategorization_M c ,PublicationTypeEntry_M t where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization and  TypeOfEntry=@TypeOfEntry and Status=@Status  and PublicationID like '%' + @PublicationID + '%' and TitleWorkItem like '%' + @TitleWorkItem + '%'  and Institution=@Institution";
                // SqlDataSource1.SelectCommand = "select j.Id as Id,Year,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,Comments from Journal_IF_Details j,Journal_M k where j.Id=k.Id and j.Id='" + txtJid.Text.Trim() + "'";
            }
            else if (PubIDSearch.Text != "" && drpPubStatusSearch.SelectedValue != "A" && TextBoxWorkItemSearch.Text != "" && TextBoxEnteredBySearch.Text == ""
        && DropDownListInst.SelectedValue != "A" && DropDownListDept.SelectedValue != "A")
            {
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("TypeOfEntry", EntryTypesearch.SelectedValue);
                SqlDataSource1.SelectParameters.Add("Status", drpPubStatusSearch.SelectedValue);
                SqlDataSource1.SelectParameters.Add("PublicationID", PubIDSearch.Text.Trim());
                SqlDataSource1.SelectParameters.Add("TitleWorkItem", TextBoxWorkItemSearch.Text);
                SqlDataSource1.SelectParameters.Add("Institution", DropDownListInst.SelectedValue);
                SqlDataSource1.SelectParameters.Add("Department", DropDownListDept.SelectedValue);

                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor ,s.StatusName ,t.EntryName,TitleWorkItem,c.PubCatName,ISStudentAuthor from Publication p,Status_Publication_M s, PubMUCategorization_M c ,PublicationTypeEntry_M t where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization and  TypeOfEntry=@TypeOfEntry and Status=@Status  and PublicationID like '%' + @PublicationID + '%' and TitleWorkItem like '%' + @TitleWorkItem + '%'  and Institution=@Institution and Department=@Department";
                // SqlDataSource1.SelectCommand = "select j.Id as Id,Year,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,Comments from Journal_IF_Details j,Journal_M k where j.Id=k.Id and j.Id='" + txtJid.Text.Trim() + "'";
            }
            else if (PubIDSearch.Text != "" && drpPubStatusSearch.SelectedValue != "A" && TextBoxWorkItemSearch.Text != "" && TextBoxEnteredBySearch.Text != ""
                && DropDownListInst.SelectedValue == "A" && DropDownListDept.SelectedValue == "A")
            {
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("TypeOfEntry", EntryTypesearch.SelectedValue);
                SqlDataSource1.SelectParameters.Add("Status", drpPubStatusSearch.SelectedValue);
                SqlDataSource1.SelectParameters.Add("PublicationID", PubIDSearch.Text.Trim());
                SqlDataSource1.SelectParameters.Add("TitleWorkItem", TextBoxWorkItemSearch.Text);
                SqlDataSource1.SelectParameters.Add("CreatedBy", TextBoxEnteredBySearch.Text);

                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor ,s.StatusName ,t.EntryName,TitleWorkItem,c.PubCatName,ISStudentAuthor from Publication p,Status_Publication_M s, PubMUCategorization_M c ,PublicationTypeEntry_M t where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization and  TypeOfEntry=@TypeOfEntry and Status=@Status  and PublicationID like '%' + @PublicationID + '%' and TitleWorkItem like '%' + @TitleWorkItem + '%' and CreatedBy like '%' + @CreatedBy + '%'";
                // SqlDataSource1.SelectCommand = "select j.Id as Id,Year,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,Comments from Journal_IF_Details j,Journal_M k where j.Id=k.Id and j.Id='" + txtJid.Text.Trim() + "'";
            }
            else if (PubIDSearch.Text != "" && drpPubStatusSearch.SelectedValue != "A" && TextBoxWorkItemSearch.Text != "" && TextBoxEnteredBySearch.Text != ""
        && DropDownListInst.SelectedValue == "A" && DropDownListDept.SelectedValue != "A")
            {
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("TypeOfEntry", EntryTypesearch.SelectedValue);
                SqlDataSource1.SelectParameters.Add("Status", drpPubStatusSearch.SelectedValue);
                SqlDataSource1.SelectParameters.Add("PublicationID", PubIDSearch.Text.Trim());
                SqlDataSource1.SelectParameters.Add("TitleWorkItem", TextBoxWorkItemSearch.Text);
                SqlDataSource1.SelectParameters.Add("CreatedBy", TextBoxEnteredBySearch.Text);
                SqlDataSource1.SelectParameters.Add("Department", DropDownListDept.SelectedValue);

                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor ,s.StatusName ,t.EntryName,TitleWorkItem,c.PubCatName,ISStudentAuthor from Publication p,Status_Publication_M s, PubMUCategorization_M c ,PublicationTypeEntry_M t where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization and  TypeOfEntry=@TypeOfEntry and Status=@Status  and PublicationID like '%' + @PublicationID + '%' and TitleWorkItem like '%' + @TitleWorkItem + '%' and CreatedBy like '%' + @CreatedBy + '%' and Department=@Department";
                // SqlDataSource1.SelectCommand = "select j.Id as Id,Year,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,Comments from Journal_IF_Details j,Journal_M k where j.Id=k.Id and j.Id='" + txtJid.Text.Trim() + "'";
            }
            else if (PubIDSearch.Text != "" && drpPubStatusSearch.SelectedValue != "A" && TextBoxWorkItemSearch.Text != "" && TextBoxEnteredBySearch.Text != ""
                && DropDownListInst.SelectedValue != "A" && DropDownListDept.SelectedValue == "A")
            {
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("TypeOfEntry", EntryTypesearch.SelectedValue);
                SqlDataSource1.SelectParameters.Add("Status", drpPubStatusSearch.SelectedValue);
                SqlDataSource1.SelectParameters.Add("PublicationID", PubIDSearch.Text.Trim());
                SqlDataSource1.SelectParameters.Add("TitleWorkItem", TextBoxWorkItemSearch.Text);
                SqlDataSource1.SelectParameters.Add("CreatedBy", TextBoxEnteredBySearch.Text);
                SqlDataSource1.SelectParameters.Add("Institution", DropDownListInst.SelectedValue);
                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor ,s.StatusName ,t.EntryName,TitleWorkItem,c.PubCatName,ISStudentAuthor from Publication p,Status_Publication_M s, PubMUCategorization_M c ,PublicationTypeEntry_M t where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization and  TypeOfEntry=@TypeOfEntry and Status=@Status  and PublicationID like '%' + @PublicationID + '%' and TitleWorkItem like '%' + @TitleWorkItem + '%' and CreatedBy like '%' + @CreatedBy + '%'  and Institution=@Institution";
                // SqlDataSource1.SelectCommand = "select j.Id as Id,Year,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,Comments from Journal_IF_Details j,Journal_M k where j.Id=k.Id and j.Id='" + txtJid.Text.Trim() + "'";
            }
            else if (PubIDSearch.Text != "" && drpPubStatusSearch.SelectedValue != "A" && TextBoxWorkItemSearch.Text != "" && TextBoxEnteredBySearch.Text != ""
            && DropDownListInst.SelectedValue != "A" && DropDownListDept.SelectedValue != "A")
            {
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("TypeOfEntry", EntryTypesearch.SelectedValue);
                SqlDataSource1.SelectParameters.Add("Status", drpPubStatusSearch.SelectedValue);
                SqlDataSource1.SelectParameters.Add("PublicationID", PubIDSearch.Text.Trim());
                SqlDataSource1.SelectParameters.Add("TitleWorkItem", TextBoxWorkItemSearch.Text);
                SqlDataSource1.SelectParameters.Add("CreatedBy", TextBoxEnteredBySearch.Text);
                SqlDataSource1.SelectParameters.Add("Institution", DropDownListInst.SelectedValue);
                SqlDataSource1.SelectParameters.Add("Department", DropDownListDept.SelectedValue);

                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor ,s.StatusName ,t.EntryName,TitleWorkItem,c.PubCatName,ISStudentAuthor from Publication p,Status_Publication_M s, PubMUCategorization_M c ,PublicationTypeEntry_M t where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization and  TypeOfEntry=@TypeOfEntry and Status=@Status  and PublicationID like '%' + @PublicationID + '%' and TitleWorkItem like '%' + @TitleWorkItem + '%' and CreatedBy like '%' + @CreatedBy + '%'  and Institution=@Institution and Department=@Department ";
                // SqlDataSource1.SelectCommand = "select j.Id as Id,Year,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,Comments from Journal_IF_Details j,Journal_M k where j.Id=k.Id and j.Id='" + txtJid.Text.Trim() + "'";
            }
        }
        else
        {
            if (PubIDSearch.Text == "" && drpPubStatusSearch.SelectedValue != "A" && TextBoxWorkItemSearch.Text == "" && TextBoxEnteredBySearch.Text == ""
                && DropDownListInst.SelectedValue == "A" && DropDownListDept.SelectedValue == "A")
            {//  SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName ,t.EntryName,TitleWorkItem,c.PubCatName from Publication p,Status_Publication_M s, PubMUCategorization_M c ,PublicationTypeEntry_M t  where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization  and status!='CAN' and TypeOfEntry='" + EntryTypesearch.SelectedValue + "'  and Institution='" + a1.InstituteId + "' ";
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("Status", drpPubStatusSearch.SelectedValue);

                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName,t.EntryName,TitleWorkItem,c.PubCatName,ISStudentAuthor from Publication p,Status_Publication_M s , PubMUCategorization_M c ,PublicationTypeEntry_M t  where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization  and   Status=@Status";
            }
            else if (PubIDSearch.Text == "" && drpPubStatusSearch.SelectedValue != "A" && TextBoxWorkItemSearch.Text == "" && TextBoxEnteredBySearch.Text == ""
          && DropDownListInst.SelectedValue == "A" && DropDownListDept.SelectedValue != "A")
            {//  SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName ,t.EntryName,TitleWorkItem,c.PubCatName from Publication p,Status_Publication_M s, PubMUCategorization_M c ,PublicationTypeEntry_M t  where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization  and status!='CAN' and TypeOfEntry='" + EntryTypesearch.SelectedValue + "'  and Institution='" + a1.InstituteId + "' ";
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("Status", drpPubStatusSearch.SelectedValue);
                SqlDataSource1.SelectParameters.Add("Department", DropDownListDept.SelectedValue);

                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName,t.EntryName,TitleWorkItem,c.PubCatName,ISStudentAuthor from Publication p,Status_Publication_M s , PubMUCategorization_M c ,PublicationTypeEntry_M t  where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization  and   Status=@Status and Department=@Department";
            }
            else   if (PubIDSearch.Text == "" && drpPubStatusSearch.SelectedValue != "A" && TextBoxWorkItemSearch.Text == "" && TextBoxEnteredBySearch.Text == ""
          && DropDownListInst.SelectedValue != "A" && DropDownListDept.SelectedValue == "A")
            {//  SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName ,t.EntryName,TitleWorkItem,c.PubCatName from Publication p,Status_Publication_M s, PubMUCategorization_M c ,PublicationTypeEntry_M t  where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization  and status!='CAN' and TypeOfEntry='" + EntryTypesearch.SelectedValue + "'  and Institution='" + a1.InstituteId + "' ";
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("Status", drpPubStatusSearch.SelectedValue);
                SqlDataSource1.SelectParameters.Add("Institution", DropDownListInst.SelectedValue);

                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName,t.EntryName,TitleWorkItem,c.PubCatName,ISStudentAuthor from Publication p,Status_Publication_M s , PubMUCategorization_M c ,PublicationTypeEntry_M t  where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization  and   Status=@Status  and Institution=@Institution";
            }
            else if (PubIDSearch.Text == "" && drpPubStatusSearch.SelectedValue != "A" && TextBoxWorkItemSearch.Text == "" && TextBoxEnteredBySearch.Text == ""
&& DropDownListInst.SelectedValue != "A" && DropDownListDept.SelectedValue != "A")
            {//  SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName ,t.EntryName,TitleWorkItem,c.PubCatName from Publication p,Status_Publication_M s, PubMUCategorization_M c ,PublicationTypeEntry_M t  where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization  and status!='CAN' and TypeOfEntry='" + EntryTypesearch.SelectedValue + "'  and Institution='" + a1.InstituteId + "' ";
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("Status", drpPubStatusSearch.SelectedValue);
                SqlDataSource1.SelectParameters.Add("Institution", DropDownListInst.SelectedValue);
                SqlDataSource1.SelectParameters.Add("Department", DropDownListDept.SelectedValue);

                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName,t.EntryName,TitleWorkItem,c.PubCatName,ISStudentAuthor from Publication p,Status_Publication_M s , PubMUCategorization_M c ,PublicationTypeEntry_M t  where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization  and   Status=@Status  and Institution=@Institution and Department=@Department";
            }
            else if (PubIDSearch.Text == "" && drpPubStatusSearch.SelectedValue != "A" && TextBoxWorkItemSearch.Text == "" && TextBoxEnteredBySearch.Text != ""
                && DropDownListInst.SelectedValue == "A" && DropDownListDept.SelectedValue == "A")
            {//  SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName ,t.EntryName,TitleWorkItem,c.PubCatName from Publication p,Status_Publication_M s, PubMUCategorization_M c ,PublicationTypeEntry_M t  where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization  and status!='CAN' and TypeOfEntry='" + EntryTypesearch.SelectedValue + "'  and Institution='" + a1.InstituteId + "' ";
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("Status", drpPubStatusSearch.SelectedValue);
                SqlDataSource1.SelectParameters.Add("CreatedBy", TextBoxEnteredBySearch.Text);

                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName,t.EntryName,TitleWorkItem,c.PubCatName,ISStudentAuthor from Publication p,Status_Publication_M s , PubMUCategorization_M c ,PublicationTypeEntry_M t  where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization  and   Status=@Status and CreatedBy like '%' + @CreatedBy + '%'";
            }
            else if (PubIDSearch.Text == "" && drpPubStatusSearch.SelectedValue != "A" && TextBoxWorkItemSearch.Text == "" && TextBoxEnteredBySearch.Text != ""
        && DropDownListInst.SelectedValue == "A" && DropDownListDept.SelectedValue != "A")
            {//  SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName ,t.EntryName,TitleWorkItem,c.PubCatName from Publication p,Status_Publication_M s, PubMUCategorization_M c ,PublicationTypeEntry_M t  where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization  and status!='CAN' and TypeOfEntry='" + EntryTypesearch.SelectedValue + "'  and Institution='" + a1.InstituteId + "' ";
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("Status", drpPubStatusSearch.SelectedValue);
                SqlDataSource1.SelectParameters.Add("CreatedBy", TextBoxEnteredBySearch.Text);
                SqlDataSource1.SelectParameters.Add("Department", DropDownListDept.SelectedValue);

                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName,t.EntryName,TitleWorkItem,c.PubCatName,ISStudentAuthor from Publication p,Status_Publication_M s , PubMUCategorization_M c ,PublicationTypeEntry_M t  where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization  and   Status=@Status and CreatedBy like '%' + @CreatedBy + '%' and Department=@Department";
            }
            else if (PubIDSearch.Text == "" && drpPubStatusSearch.SelectedValue != "A" && TextBoxWorkItemSearch.Text == "" && TextBoxEnteredBySearch.Text != ""
    && DropDownListInst.SelectedValue != "A" && DropDownListDept.SelectedValue == "A")
            {//  SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName ,t.EntryName,TitleWorkItem,c.PubCatName from Publication p,Status_Publication_M s, PubMUCategorization_M c ,PublicationTypeEntry_M t  where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization  and status!='CAN' and TypeOfEntry='" + EntryTypesearch.SelectedValue + "'  and Institution='" + a1.InstituteId + "' ";
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("Status", drpPubStatusSearch.SelectedValue);
                SqlDataSource1.SelectParameters.Add("CreatedBy", TextBoxEnteredBySearch.Text);
                SqlDataSource1.SelectParameters.Add("Institution", DropDownListInst.SelectedValue);

                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName,t.EntryName,TitleWorkItem,c.PubCatName,ISStudentAuthor from Publication p,Status_Publication_M s , PubMUCategorization_M c ,PublicationTypeEntry_M t  where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization  and   Status=@Status and CreatedBy like '%' + @CreatedBy + '%'  and Institution=@Institution";
            }
            else if (PubIDSearch.Text == "" && drpPubStatusSearch.SelectedValue != "A" && TextBoxWorkItemSearch.Text == "" && TextBoxEnteredBySearch.Text != ""
&& DropDownListInst.SelectedValue != "A" && DropDownListDept.SelectedValue != "A")
            {//  SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName ,t.EntryName,TitleWorkItem,c.PubCatName from Publication p,Status_Publication_M s, PubMUCategorization_M c ,PublicationTypeEntry_M t  where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization  and status!='CAN' and TypeOfEntry='" + EntryTypesearch.SelectedValue + "'  and Institution='" + a1.InstituteId + "' ";
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("Status", drpPubStatusSearch.SelectedValue);
                SqlDataSource1.SelectParameters.Add("CreatedBy", TextBoxEnteredBySearch.Text);
                SqlDataSource1.SelectParameters.Add("Institution", DropDownListInst.SelectedValue);
                SqlDataSource1.SelectParameters.Add("Department", DropDownListDept.SelectedValue);

                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName,t.EntryName,TitleWorkItem,c.PubCatName,ISStudentAuthor from Publication p,Status_Publication_M s , PubMUCategorization_M c ,PublicationTypeEntry_M t  where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization  and   Status=@Status and CreatedBy like '%' + @CreatedBy + '%'  and Institution=@Institution and Department=@Department";
            }
            else if (PubIDSearch.Text == "" && drpPubStatusSearch.SelectedValue != "A" && TextBoxWorkItemSearch.Text != "" && TextBoxEnteredBySearch.Text == ""
                && DropDownListInst.SelectedValue == "A" && DropDownListDept.SelectedValue == "A")
            {//  SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName ,t.EntryName,TitleWorkItem,c.PubCatName from Publication p,Status_Publication_M s, PubMUCategorization_M c ,PublicationTypeEntry_M t  where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization  and status!='CAN' and TypeOfEntry='" + EntryTypesearch.SelectedValue + "'  and Institution='" + a1.InstituteId + "' ";
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("Status", drpPubStatusSearch.SelectedValue);
                SqlDataSource1.SelectParameters.Add("TitleWorkItem", TextBoxWorkItemSearch.Text);

                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName,t.EntryName,TitleWorkItem,c.PubCatName,ISStudentAuthor from Publication p,Status_Publication_M s , PubMUCategorization_M c ,PublicationTypeEntry_M t  where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization  and   Status=@Status and TitleWorkItem like '%' + @TitleWorkItem + '%'";
            }
            else if (PubIDSearch.Text == "" && drpPubStatusSearch.SelectedValue != "A" && TextBoxWorkItemSearch.Text != "" && TextBoxEnteredBySearch.Text == ""
         && DropDownListInst.SelectedValue == "A" && DropDownListDept.SelectedValue != "A")
            {//  SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName ,t.EntryName,TitleWorkItem,c.PubCatName from Publication p,Status_Publication_M s, PubMUCategorization_M c ,PublicationTypeEntry_M t  where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization  and status!='CAN' and TypeOfEntry='" + EntryTypesearch.SelectedValue + "'  and Institution='" + a1.InstituteId + "' ";
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("Status", drpPubStatusSearch.SelectedValue);
                SqlDataSource1.SelectParameters.Add("TitleWorkItem", TextBoxWorkItemSearch.Text);
                SqlDataSource1.SelectParameters.Add("Department", DropDownListDept.SelectedValue);

                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName,t.EntryName,TitleWorkItem,c.PubCatName,ISStudentAuthor from Publication p,Status_Publication_M s , PubMUCategorization_M c ,PublicationTypeEntry_M t  where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization  and   Status=@Status and TitleWorkItem like '%' + @TitleWorkItem + '%' and Department=@Department";
            }
            else if (PubIDSearch.Text == "" && drpPubStatusSearch.SelectedValue != "A" && TextBoxWorkItemSearch.Text != "" && TextBoxEnteredBySearch.Text == ""
       && DropDownListInst.SelectedValue != "A" && DropDownListDept.SelectedValue == "A")
            {//  SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName ,t.EntryName,TitleWorkItem,c.PubCatName from Publication p,Status_Publication_M s, PubMUCategorization_M c ,PublicationTypeEntry_M t  where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization  and status!='CAN' and TypeOfEntry='" + EntryTypesearch.SelectedValue + "'  and Institution='" + a1.InstituteId + "' ";
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("Status", drpPubStatusSearch.SelectedValue);
                SqlDataSource1.SelectParameters.Add("TitleWorkItem", TextBoxWorkItemSearch.Text);
                SqlDataSource1.SelectParameters.Add("Institution", DropDownListInst.SelectedValue);

                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName,t.EntryName,TitleWorkItem,c.PubCatName,ISStudentAuthor from Publication p,Status_Publication_M s , PubMUCategorization_M c ,PublicationTypeEntry_M t  where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization  and   Status=@Status and TitleWorkItem like '%' + @TitleWorkItem + '%'  and Institution=@Institution";
            }
            else if (PubIDSearch.Text == "" && drpPubStatusSearch.SelectedValue != "A" && TextBoxWorkItemSearch.Text != "" && TextBoxEnteredBySearch.Text == ""
&& DropDownListInst.SelectedValue != "A" && DropDownListDept.SelectedValue != "A")
            {//  SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName ,t.EntryName,TitleWorkItem,c.PubCatName from Publication p,Status_Publication_M s, PubMUCategorization_M c ,PublicationTypeEntry_M t  where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization  and status!='CAN' and TypeOfEntry='" + EntryTypesearch.SelectedValue + "'  and Institution='" + a1.InstituteId + "' ";
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("Status", drpPubStatusSearch.SelectedValue);
                SqlDataSource1.SelectParameters.Add("TitleWorkItem", TextBoxWorkItemSearch.Text);
                SqlDataSource1.SelectParameters.Add("Institution", DropDownListInst.SelectedValue);
                SqlDataSource1.SelectParameters.Add("Department", DropDownListDept.SelectedValue);

                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName,t.EntryName,TitleWorkItem,c.PubCatName,ISStudentAuthor from Publication p,Status_Publication_M s , PubMUCategorization_M c ,PublicationTypeEntry_M t  where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization  and   Status=@Status and TitleWorkItem like '%' + @TitleWorkItem + '%'  and Institution=@Institution and Department=@Department";
            }
            else if (PubIDSearch.Text == "" && drpPubStatusSearch.SelectedValue != "A" && TextBoxWorkItemSearch.Text != "" && TextBoxEnteredBySearch.Text != ""
                && DropDownListInst.SelectedValue == "A" && DropDownListDept.SelectedValue == "A")
            {//  SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName ,t.EntryName,TitleWorkItem,c.PubCatName from Publication p,Status_Publication_M s, PubMUCategorization_M c ,PublicationTypeEntry_M t  where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization  and status!='CAN' and TypeOfEntry='" + EntryTypesearch.SelectedValue + "'  and Institution='" + a1.InstituteId + "' ";
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("Status", drpPubStatusSearch.SelectedValue);
                SqlDataSource1.SelectParameters.Add("TitleWorkItem", TextBoxWorkItemSearch.Text);
                SqlDataSource1.SelectParameters.Add("CreatedBy", TextBoxEnteredBySearch.Text);

                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName,t.EntryName,TitleWorkItem,c.PubCatName,ISStudentAuthor from Publication p,Status_Publication_M s , PubMUCategorization_M c ,PublicationTypeEntry_M t  where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization  and   Status=@Status and TitleWorkItem like '%' + @TitleWorkItem + '%' and CreatedBy like '%' + @CreatedBy + '%'";
            }
            else if (PubIDSearch.Text == "" && drpPubStatusSearch.SelectedValue != "A" && TextBoxWorkItemSearch.Text != "" && TextBoxEnteredBySearch.Text != ""
       && DropDownListInst.SelectedValue == "A" && DropDownListDept.SelectedValue != "A")
            {//  SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName ,t.EntryName,TitleWorkItem,c.PubCatName from Publication p,Status_Publication_M s, PubMUCategorization_M c ,PublicationTypeEntry_M t  where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization  and status!='CAN' and TypeOfEntry='" + EntryTypesearch.SelectedValue + "'  and Institution='" + a1.InstituteId + "' ";
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("Status", drpPubStatusSearch.SelectedValue);
                SqlDataSource1.SelectParameters.Add("TitleWorkItem", TextBoxWorkItemSearch.Text);
                SqlDataSource1.SelectParameters.Add("CreatedBy", TextBoxEnteredBySearch.Text);
                SqlDataSource1.SelectParameters.Add("Department", DropDownListDept.SelectedValue);

                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName,t.EntryName,TitleWorkItem,c.PubCatName,ISStudentAuthor from Publication p,Status_Publication_M s , PubMUCategorization_M c ,PublicationTypeEntry_M t  where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization  and   Status=@Status and TitleWorkItem like '%' + @TitleWorkItem + '%' and CreatedBy like '%' + @CreatedBy + '%' and Department=@Department";
            }
            else if (PubIDSearch.Text == "" && drpPubStatusSearch.SelectedValue != "A" && TextBoxWorkItemSearch.Text != "" && TextBoxEnteredBySearch.Text != ""
         && DropDownListInst.SelectedValue != "A" && DropDownListDept.SelectedValue == "A")
            {//  SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName ,t.EntryName,TitleWorkItem,c.PubCatName from Publication p,Status_Publication_M s, PubMUCategorization_M c ,PublicationTypeEntry_M t  where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization  and status!='CAN' and TypeOfEntry='" + EntryTypesearch.SelectedValue + "'  and Institution='" + a1.InstituteId + "' ";

                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("Status", drpPubStatusSearch.SelectedValue);
                SqlDataSource1.SelectParameters.Add("TitleWorkItem", TextBoxWorkItemSearch.Text);
                SqlDataSource1.SelectParameters.Add("CreatedBy", TextBoxEnteredBySearch.Text);
                SqlDataSource1.SelectParameters.Add("Institution", DropDownListInst.SelectedValue);
                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName,t.EntryName,TitleWorkItem,c.PubCatName,ISStudentAuthor from Publication p,Status_Publication_M s , PubMUCategorization_M c ,PublicationTypeEntry_M t  where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization  and   Status=@Status and TitleWorkItem like '%' + @TitleWorkItem + '%' and CreatedBy like '%' + @CreatedBy + '%'  and Institution=@Institution";
            }
            else if (PubIDSearch.Text == "" && drpPubStatusSearch.SelectedValue != "A" && TextBoxWorkItemSearch.Text != "" && TextBoxEnteredBySearch.Text != ""
&& DropDownListInst.SelectedValue != "A" && DropDownListDept.SelectedValue != "A")
            {//  SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName ,t.EntryName,TitleWorkItem,c.PubCatName from Publication p,Status_Publication_M s, PubMUCategorization_M c ,PublicationTypeEntry_M t  where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization  and status!='CAN' and TypeOfEntry='" + EntryTypesearch.SelectedValue + "'  and Institution='" + a1.InstituteId + "' ";
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("Status", drpPubStatusSearch.SelectedValue);
                SqlDataSource1.SelectParameters.Add("TitleWorkItem", TextBoxWorkItemSearch.Text);
                SqlDataSource1.SelectParameters.Add("CreatedBy", TextBoxEnteredBySearch.Text);
                SqlDataSource1.SelectParameters.Add("Institution", DropDownListInst.SelectedValue);
                SqlDataSource1.SelectParameters.Add("Department", DropDownListDept.SelectedValue);

                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName,t.EntryName,TitleWorkItem,c.PubCatName,ISStudentAuthor from Publication p,Status_Publication_M s , PubMUCategorization_M c ,PublicationTypeEntry_M t  where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization  and   Status=@Status and TitleWorkItem like '%' + @TitleWorkItem + '%' and CreatedBy like '%' + @CreatedBy + '%'  and Institution=@Institution and Department=@Department";
            }
            else if (PubIDSearch.Text == "" && drpPubStatusSearch.SelectedValue == "A" && TextBoxWorkItemSearch.Text == "" && TextBoxEnteredBySearch.Text == ""
                && DropDownListInst.SelectedValue == "A" && DropDownListDept.SelectedValue == "A")
            {

                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName,t.EntryName,TitleWorkItem,c.PubCatName,ISStudentAuthor from Publication p,Status_Publication_M s , PubMUCategorization_M c ,PublicationTypeEntry_M t  where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization    ";
                // SqlDataSource1.SelectCommand = "select j.Id as Id,Year,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,Comments from Journal_IF_Details j,Journal_M k where j.Id=k.Id and j.Id='" + txtJid.Text.Trim() + "'";
            }
            else if (PubIDSearch.Text == "" && drpPubStatusSearch.SelectedValue == "A" && TextBoxWorkItemSearch.Text == "" && TextBoxEnteredBySearch.Text == ""
        && DropDownListInst.SelectedValue == "A" && DropDownListDept.SelectedValue != "A")
            {
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("Department", DropDownListDept.SelectedValue);

                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName,t.EntryName,TitleWorkItem,c.PubCatName,ISStudentAuthor from Publication p,Status_Publication_M s , PubMUCategorization_M c ,PublicationTypeEntry_M t  where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization  and Department=@Department  ";
                // SqlDataSource1.SelectCommand = "select j.Id as Id,Year,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,Comments from Journal_IF_Details j,Journal_M k where j.Id=k.Id and j.Id='" + txtJid.Text.Trim() + "'";
            }
            else if (PubIDSearch.Text == "" && drpPubStatusSearch.SelectedValue == "A" && TextBoxWorkItemSearch.Text == "" && TextBoxEnteredBySearch.Text == ""
       && DropDownListInst.SelectedValue != "A" && DropDownListDept.SelectedValue == "A")
            {
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("Institution", DropDownListInst.SelectedValue);

                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName,t.EntryName,TitleWorkItem,c.PubCatName,ISStudentAuthor from Publication p,Status_Publication_M s , PubMUCategorization_M c ,PublicationTypeEntry_M t  where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization  and Institution=@Institution";
                // SqlDataSource1.SelectCommand = "select j.Id as Id,Year,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,Comments from Journal_IF_Details j,Journal_M k where j.Id=k.Id and j.Id='" + txtJid.Text.Trim() + "'";
            }
            else if (PubIDSearch.Text == "" && drpPubStatusSearch.SelectedValue == "A" && TextBoxWorkItemSearch.Text == "" && TextBoxEnteredBySearch.Text == ""
&& DropDownListInst.SelectedValue != "A" && DropDownListDept.SelectedValue != "A")
            {
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("Institution", DropDownListInst.SelectedValue);
                SqlDataSource1.SelectParameters.Add("Department", DropDownListDept.SelectedValue);

                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName,t.EntryName,TitleWorkItem,c.PubCatName,ISStudentAuthor from Publication p,Status_Publication_M s , PubMUCategorization_M c ,PublicationTypeEntry_M t  where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization   and Institution=@Institution and Department=@Department  ";
                // SqlDataSource1.SelectCommand = "select j.Id as Id,Year,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,Comments from Journal_IF_Details j,Journal_M k where j.Id=k.Id and j.Id='" + txtJid.Text.Trim() + "'";
            }
            else if (PubIDSearch.Text == "" && drpPubStatusSearch.SelectedValue == "A" && TextBoxWorkItemSearch.Text == "" && TextBoxEnteredBySearch.Text != ""
                && DropDownListInst.SelectedValue == "A" && DropDownListDept.SelectedValue == "A")
            {
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("CreatedBy", TextBoxEnteredBySearch.Text);

                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName,t.EntryName,TitleWorkItem,c.PubCatName,ISStudentAuthor from Publication p,Status_Publication_M s , PubMUCategorization_M c ,PublicationTypeEntry_M t  where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization   and CreatedBy like '%' + @CreatedBy + '%' ";
                // SqlDataSource1.SelectCommand = "select j.Id as Id,Year,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,Comments from Journal_IF_Details j,Journal_M k where j.Id=k.Id and j.Id='" + txtJid.Text.Trim() + "'";
            }
            else if (PubIDSearch.Text == "" && drpPubStatusSearch.SelectedValue == "A" && TextBoxWorkItemSearch.Text == "" && TextBoxEnteredBySearch.Text != ""
        && DropDownListInst.SelectedValue == "A" && DropDownListDept.SelectedValue != "A")
            {
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("CreatedBy", TextBoxEnteredBySearch.Text);
                SqlDataSource1.SelectParameters.Add("Department", DropDownListDept.SelectedValue);

                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName,t.EntryName,TitleWorkItem,c.PubCatName,ISStudentAuthor from Publication p,Status_Publication_M s , PubMUCategorization_M c ,PublicationTypeEntry_M t  where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization   and CreatedBy like '%' + @CreatedBy + '%' and Department=@Department ";
                // SqlDataSource1.SelectCommand = "select j.Id as Id,Year,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,Comments from Journal_IF_Details j,Journal_M k where j.Id=k.Id and j.Id='" + txtJid.Text.Trim() + "'";
            }
            else if (PubIDSearch.Text == "" && drpPubStatusSearch.SelectedValue == "A" && TextBoxWorkItemSearch.Text == "" && TextBoxEnteredBySearch.Text != ""
   && DropDownListInst.SelectedValue != "A" && DropDownListDept.SelectedValue == "A")
            {
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("CreatedBy", TextBoxEnteredBySearch.Text);
                SqlDataSource1.SelectParameters.Add("Institution", DropDownListInst.SelectedValue);

                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName,t.EntryName,TitleWorkItem,c.PubCatName,ISStudentAuthor from Publication p,Status_Publication_M s , PubMUCategorization_M c ,PublicationTypeEntry_M t  where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization   and CreatedBy like '%' + @CreatedBy + '%'  and Institution=@Institution ";
                // SqlDataSource1.SelectCommand = "select j.Id as Id,Year,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,Comments from Journal_IF_Details j,Journal_M k where j.Id=k.Id and j.Id='" + txtJid.Text.Trim() + "'";
            }
            else if (PubIDSearch.Text == "" && drpPubStatusSearch.SelectedValue == "A" && TextBoxWorkItemSearch.Text == "" && TextBoxEnteredBySearch.Text != ""
&& DropDownListInst.SelectedValue != "A" && DropDownListDept.SelectedValue != "A")
            {
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("CreatedBy", TextBoxEnteredBySearch.Text);
                SqlDataSource1.SelectParameters.Add("Institution", DropDownListInst.SelectedValue);
                SqlDataSource1.SelectParameters.Add("Department", DropDownListDept.SelectedValue);

                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName,t.EntryName,TitleWorkItem,c.PubCatName,ISStudentAuthor from Publication p,Status_Publication_M s , PubMUCategorization_M c ,PublicationTypeEntry_M t  where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization  and CreatedBy like '%' + @CreatedBy + '%'  and Institution=@Institution and Department=@Department ";
                // SqlDataSource1.SelectCommand = "select j.Id as Id,Year,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,Comments from Journal_IF_Details j,Journal_M k where j.Id=k.Id and j.Id='" + txtJid.Text.Trim() + "'";
            }
            else if (PubIDSearch.Text == "" && drpPubStatusSearch.SelectedValue == "A" && TextBoxWorkItemSearch.Text != "" && TextBoxEnteredBySearch.Text == ""
                && DropDownListInst.SelectedValue == "A" && DropDownListDept.SelectedValue == "A")
            {
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("TitleWorkItem", TextBoxWorkItemSearch.Text);
                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName,t.EntryName,TitleWorkItem,c.PubCatName,ISStudentAuthor from Publication p,Status_Publication_M s , PubMUCategorization_M c ,PublicationTypeEntry_M t  where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization   and TitleWorkItem like '%' + @TitleWorkItem + '%' ";
                // SqlDataSource1.SelectCommand = "select j.Id as Id,Year,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,Comments from Journal_IF_Details j,Journal_M k where j.Id=k.Id and j.Id='" + txtJid.Text.Trim() + "'";
            }
            else if (PubIDSearch.Text == "" && drpPubStatusSearch.SelectedValue == "A" && TextBoxWorkItemSearch.Text != "" && TextBoxEnteredBySearch.Text == ""
        && DropDownListInst.SelectedValue == "A" && DropDownListDept.SelectedValue != "A")
            {
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("TitleWorkItem", TextBoxWorkItemSearch.Text);
                SqlDataSource1.SelectParameters.Add("Department", DropDownListDept.SelectedValue);

                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName,t.EntryName,TitleWorkItem,c.PubCatName,ISStudentAuthor from Publication p,Status_Publication_M s , PubMUCategorization_M c ,PublicationTypeEntry_M t  where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization  and TitleWorkItem like '%' + @TitleWorkItem + '%'  and Department=@Department";
                // SqlDataSource1.SelectCommand = "select j.Id as Id,Year,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,Comments from Journal_IF_Details j,Journal_M k where j.Id=k.Id and j.Id='" + txtJid.Text.Trim() + "'";
            }
            else if (PubIDSearch.Text == "" && drpPubStatusSearch.SelectedValue == "A" && TextBoxWorkItemSearch.Text != "" && TextBoxEnteredBySearch.Text == ""
                && DropDownListInst.SelectedValue != "A" && DropDownListDept.SelectedValue == "A")
            {
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("TitleWorkItem", TextBoxWorkItemSearch.Text);
                SqlDataSource1.SelectParameters.Add("Institution", DropDownListInst.SelectedValue);

                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName,t.EntryName,TitleWorkItem,c.PubCatName,ISStudentAuthor from Publication p,Status_Publication_M s , PubMUCategorization_M c ,PublicationTypeEntry_M t  where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization  and TitleWorkItem like '%' + @TitleWorkItem + '%'  and Institution=@Institution ";
                // SqlDataSource1.SelectCommand = "select j.Id as Id,Year,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,Comments from Journal_IF_Details j,Journal_M k where j.Id=k.Id and j.Id='" + txtJid.Text.Trim() + "'";
            }
            else if (PubIDSearch.Text == "" && drpPubStatusSearch.SelectedValue == "A" && TextBoxWorkItemSearch.Text != "" && TextBoxEnteredBySearch.Text == ""
       && DropDownListInst.SelectedValue != "A" && DropDownListDept.SelectedValue != "A")
            {
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("TitleWorkItem", TextBoxWorkItemSearch.Text);
                SqlDataSource1.SelectParameters.Add("Institution", DropDownListInst.SelectedValue);
                SqlDataSource1.SelectParameters.Add("Department", DropDownListDept.SelectedValue);

                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName,t.EntryName,TitleWorkItem,c.PubCatName,ISStudentAuthor from Publication p,Status_Publication_M s , PubMUCategorization_M c ,PublicationTypeEntry_M t  where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization and TitleWorkItem like '%' + @TitleWorkItem + '%'   and Institution=@Institution and Department=@Department ";
                // SqlDataSource1.SelectCommand = "select j.Id as Id,Year,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,Comments from Journal_IF_Details j,Journal_M k where j.Id=k.Id and j.Id='" + txtJid.Text.Trim() + "'";
            }
            else if (PubIDSearch.Text == "" && drpPubStatusSearch.SelectedValue == "A" && TextBoxWorkItemSearch.Text != "" && TextBoxEnteredBySearch.Text != ""
                && DropDownListInst.SelectedValue == "A" && DropDownListDept.SelectedValue == "A")
            {
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("TitleWorkItem", TextBoxWorkItemSearch.Text);
                SqlDataSource1.SelectParameters.Add("CreatedBy", TextBoxEnteredBySearch.Text);

                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName,t.EntryName,TitleWorkItem,c.PubCatName,ISStudentAuthor from Publication p,Status_Publication_M s , PubMUCategorization_M c ,PublicationTypeEntry_M t  where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization  and TitleWorkItem like '%' + @TitleWorkItem + '%' and CreatedBy like '%' + @CreatedBy + '%' ";
                // SqlDataSource1.SelectCommand = "select j.Id as Id,Year,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,Comments from Journal_IF_Details j,Journal_M k where j.Id=k.Id and j.Id='" + txtJid.Text.Trim() + "'";
            }
            else if (PubIDSearch.Text == "" && drpPubStatusSearch.SelectedValue == "A" && TextBoxWorkItemSearch.Text != "" && TextBoxEnteredBySearch.Text != ""
        && DropDownListInst.SelectedValue == "A" && DropDownListDept.SelectedValue != "A")
            {
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("TitleWorkItem", TextBoxWorkItemSearch.Text);
                SqlDataSource1.SelectParameters.Add("CreatedBy", TextBoxEnteredBySearch.Text);
                SqlDataSource1.SelectParameters.Add("Department", DropDownListDept.SelectedValue);

                // SqlDataSource1.SelectCommand = "select j.Id as Id,Year,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,Comments from Journal_IF_Details j,Journal_M k where j.Id=k.Id and j.Id='" + txtJid.Text.Trim() + "'";and TitleWorkItem like '%" + TextBoxWorkItemSearch.Text + "%'
                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName,t.EntryName,TitleWorkItem,c.PubCatName,ISStudentAuthor from Publication p,Status_Publication_M s , PubMUCategorization_M c ,PublicationTypeEntry_M t  where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization   and TitleWorkItem like '%' + @TitleWorkItem + '%' and CreatedBy like '%' + @CreatedBy + '%' and Department=@Department  ";
            }
            else if (PubIDSearch.Text == "" && drpPubStatusSearch.SelectedValue == "A" && TextBoxWorkItemSearch.Text != "" && TextBoxEnteredBySearch.Text != ""
       && DropDownListInst.SelectedValue != "A" && DropDownListDept.SelectedValue == "A")
            {
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("TitleWorkItem", TextBoxWorkItemSearch.Text);
                SqlDataSource1.SelectParameters.Add("CreatedBy", TextBoxEnteredBySearch.Text);
                SqlDataSource1.SelectParameters.Add("Institution", DropDownListInst.SelectedValue);

                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName,t.EntryName,TitleWorkItem,c.PubCatName,ISStudentAuthor from Publication p,Status_Publication_M s , PubMUCategorization_M c ,PublicationTypeEntry_M t  where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization  and TitleWorkItem like '%' + @TitleWorkItem + '%'   and CreatedBy like '%' + @CreatedBy + '%'  and Institution=@Institution";
                // SqlDataSource1.SelectCommand = "select j.Id as Id,Year,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,Comments from Journal_IF_Details j,Journal_M k where j.Id=k.Id and j.Id='" + txtJid.Text.Trim() + "'";
            }
            else if (PubIDSearch.Text == "" && drpPubStatusSearch.SelectedValue == "A" && TextBoxWorkItemSearch.Text != "" && TextBoxEnteredBySearch.Text != ""
&& DropDownListInst.SelectedValue != "A" && DropDownListDept.SelectedValue != "A")
            {
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("TitleWorkItem", TextBoxWorkItemSearch.Text);
                SqlDataSource1.SelectParameters.Add("CreatedBy", TextBoxEnteredBySearch.Text);
                SqlDataSource1.SelectParameters.Add("Institution", DropDownListInst.SelectedValue);
                SqlDataSource1.SelectParameters.Add("Department", DropDownListDept.SelectedValue);

                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName,t.EntryName,TitleWorkItem,c.PubCatName,ISStudentAuthor from Publication p,Status_Publication_M s , PubMUCategorization_M c ,PublicationTypeEntry_M t  where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization  and TitleWorkItem like '%' + @TitleWorkItem + '%'  and CreatedBy like '%' + @CreatedBy + '%'  and Institution=@Institution and Department=@Department ";
                // SqlDataSource1.SelectCommand = "select j.Id as Id,Year,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,Comments from Journal_IF_Details j,Journal_M k where j.Id=k.Id and j.Id='" + txtJid.Text.Trim() + "'";
            }
            else if (PubIDSearch.Text != "" && drpPubStatusSearch.SelectedValue == "A" && TextBoxWorkItemSearch.Text == "" && TextBoxEnteredBySearch.Text == ""
                && DropDownListInst.SelectedValue == "A" && DropDownListDept.SelectedValue == "A")
            {
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("PublicationID", PubIDSearch.Text.Trim());
                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName ,t.EntryName,TitleWorkItem,c.PubCatName,ISStudentAuthor from Publication p,Status_Publication_M s, PubMUCategorization_M c ,PublicationTypeEntry_M t  where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization  and  PublicationID like '%' + @PublicationID + '%' ";
                // SqlDataSource1.SelectCommand = "select j.Id as Id,Year,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,Comments from Journal_IF_Details j,Journal_M k where j.Id=k.Id and j.Id='" + txtJid.Text.Trim() + "'";
            }
            else if (PubIDSearch.Text != "" && drpPubStatusSearch.SelectedValue == "A" && TextBoxWorkItemSearch.Text == "" && TextBoxEnteredBySearch.Text == ""
      && DropDownListInst.SelectedValue == "A" && DropDownListDept.SelectedValue != "A")
            {
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("PublicationID", PubIDSearch.Text.Trim());
                SqlDataSource1.SelectParameters.Add("Department", DropDownListDept.SelectedValue);

                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName ,t.EntryName,TitleWorkItem,c.PubCatName,ISStudentAuthor from Publication p,Status_Publication_M s, PubMUCategorization_M c ,PublicationTypeEntry_M t  where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization  and  PublicationID like '%' + @PublicationID + '%' and Department=@Department";
                // SqlDataSource1.SelectCommand = "select j.Id as Id,Year,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,Comments from Journal_IF_Details j,Journal_M k where j.Id=k.Id and j.Id='" + txtJid.Text.Trim() + "'";
            }
            else if (PubIDSearch.Text != "" && drpPubStatusSearch.SelectedValue == "A" && TextBoxWorkItemSearch.Text == "" && TextBoxEnteredBySearch.Text == ""
          && DropDownListInst.SelectedValue != "A" && DropDownListDept.SelectedValue == "A")
            {
              

                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("PublicationID", PubIDSearch.Text.Trim());
                SqlDataSource1.SelectParameters.Add("Institution", DropDownListInst.SelectedValue);

                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName ,t.EntryName,TitleWorkItem,c.PubCatName,ISStudentAuthor from Publication p,Status_Publication_M s, PubMUCategorization_M c ,PublicationTypeEntry_M t  where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization  and  PublicationID like '%' + @PublicationID + '%'  and Institution=@Institution";
                // SqlDataSource1.SelectCommand = "select j.Id as Id,Year,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,Comments from Journal_IF_Details j,Journal_M k where j.Id=k.Id and j.Id='" + txtJid.Text.Trim() + "'";
            }
            else if (PubIDSearch.Text != "" && drpPubStatusSearch.SelectedValue == "A" && TextBoxWorkItemSearch.Text == "" && TextBoxEnteredBySearch.Text == ""
&& DropDownListInst.SelectedValue != "A" && DropDownListDept.SelectedValue != "A")
            {
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("PublicationID", PubIDSearch.Text.Trim());
                SqlDataSource1.SelectParameters.Add("Institution", DropDownListInst.SelectedValue);
                SqlDataSource1.SelectParameters.Add("Department", DropDownListDept.SelectedValue);

                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName ,t.EntryName,TitleWorkItem,c.PubCatName,ISStudentAuthor from Publication p,Status_Publication_M s, PubMUCategorization_M c ,PublicationTypeEntry_M t  where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization  and  PublicationID like '%' + @PublicationID + '%'   and Institution=@Institution and Department=@Department";
                // SqlDataSource1.SelectCommand = "select j.Id as Id,Year,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,Comments from Journal_IF_Details j,Journal_M k where j.Id=k.Id and j.Id='" + txtJid.Text.Trim() + "'";
            }
            else if (PubIDSearch.Text != "" && drpPubStatusSearch.SelectedValue == "A" && TextBoxWorkItemSearch.Text == "" && TextBoxEnteredBySearch.Text != ""
                && DropDownListInst.SelectedValue == "A" && DropDownListDept.SelectedValue == "A")
            {
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("PublicationID", PubIDSearch.Text.Trim());
                SqlDataSource1.SelectParameters.Add("CreatedBy", TextBoxEnteredBySearch.Text);

                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName ,t.EntryName,TitleWorkItem,c.PubCatName,ISStudentAuthor from Publication p,Status_Publication_M s, PubMUCategorization_M c ,PublicationTypeEntry_M t  where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization  and  PublicationID like '%' + @PublicationID + '%' and CreatedBy like '%' + @CreatedBy + '%' ";
                // SqlDataSource1.SelectCommand = "select j.Id as Id,Year,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,Comments from Journal_IF_Details j,Journal_M k where j.Id=k.Id and j.Id='" + txtJid.Text.Trim() + "'";
            }
            else if (PubIDSearch.Text != "" && drpPubStatusSearch.SelectedValue == "A" && TextBoxWorkItemSearch.Text == "" && TextBoxEnteredBySearch.Text != ""
        && DropDownListInst.SelectedValue == "A" && DropDownListDept.SelectedValue != "A")
            {
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("PublicationID", PubIDSearch.Text.Trim());
                SqlDataSource1.SelectParameters.Add("CreatedBy", TextBoxEnteredBySearch.Text);
                SqlDataSource1.SelectParameters.Add("Department", DropDownListDept.SelectedValue);

                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName ,t.EntryName,TitleWorkItem,c.PubCatName,ISStudentAuthor from Publication p,Status_Publication_M s, PubMUCategorization_M c ,PublicationTypeEntry_M t  where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization  and  PublicationID like '%' + @PublicationID + '%' and CreatedBy like '%' + @CreatedBy + '%' and Department=@Department";
                // SqlDataSource1.SelectCommand = "select j.Id as Id,Year,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,Comments from Journal_IF_Details j,Journal_M k where j.Id=k.Id and j.Id='" + txtJid.Text.Trim() + "'";
            }
            else if (PubIDSearch.Text != "" && drpPubStatusSearch.SelectedValue == "A" && TextBoxWorkItemSearch.Text == "" && TextBoxEnteredBySearch.Text != ""
         && DropDownListInst.SelectedValue != "A" && DropDownListDept.SelectedValue == "A")
            {
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("PublicationID", PubIDSearch.Text.Trim());
                SqlDataSource1.SelectParameters.Add("CreatedBy", TextBoxEnteredBySearch.Text);
                SqlDataSource1.SelectParameters.Add("Institution", DropDownListInst.SelectedValue);

                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName ,t.EntryName,TitleWorkItem,c.PubCatName,ISStudentAuthor from Publication p,Status_Publication_M s, PubMUCategorization_M c ,PublicationTypeEntry_M t  where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization  and  PublicationID like '%' + @PublicationID + '%' and CreatedBy like '%' + @CreatedBy + '%'  and Institution=@Institution";
                // SqlDataSource1.SelectCommand = "select j.Id as Id,Year,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,Comments from Journal_IF_Details j,Journal_M k where j.Id=k.Id and j.Id='" + txtJid.Text.Trim() + "'";


            }
            else if (PubIDSearch.Text != "" && drpPubStatusSearch.SelectedValue == "A" && TextBoxWorkItemSearch.Text == "" && TextBoxEnteredBySearch.Text != ""
&& DropDownListInst.SelectedValue != "A" && DropDownListDept.SelectedValue != "A")
            {
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("PublicationID", PubIDSearch.Text.Trim());
                SqlDataSource1.SelectParameters.Add("CreatedBy", TextBoxEnteredBySearch.Text);
                SqlDataSource1.SelectParameters.Add("Institution", DropDownListInst.SelectedValue);
                SqlDataSource1.SelectParameters.Add("Department", DropDownListDept.SelectedValue);

                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName ,t.EntryName,TitleWorkItem,c.PubCatName,ISStudentAuthor from Publication p,Status_Publication_M s, PubMUCategorization_M c ,PublicationTypeEntry_M t  where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization  and  PublicationID like '%' + @PublicationID + '%' and CreatedBy like '%' + @CreatedBy + '%' and Institution=@Institution and Department=@Department ";
                // SqlDataSource1.SelectCommand = "select j.Id as Id,Year,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,Comments from Journal_IF_Details j,Journal_M k where j.Id=k.Id and j.Id='" + txtJid.Text.Trim() + "'";
            }
            else if (PubIDSearch.Text != "" && drpPubStatusSearch.SelectedValue == "A" && TextBoxWorkItemSearch.Text != "" && TextBoxEnteredBySearch.Text == ""
                && DropDownListInst.SelectedValue == "A" && DropDownListDept.SelectedValue == "A")
            {
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("PublicationID", PubIDSearch.Text.Trim());
                SqlDataSource1.SelectParameters.Add("TitleWorkItem", TextBoxWorkItemSearch.Text);

                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName ,t.EntryName,TitleWorkItem,c.PubCatName,ISStudentAuthor from Publication p,Status_Publication_M s, PubMUCategorization_M c ,PublicationTypeEntry_M t  where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization  and  PublicationID like '%' + @PublicationID + '%' and TitleWorkItem like '%' + @TitleWorkItem + '%' ";
                // SqlDataSource1.SelectCommand = "select j.Id as Id,Year,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,Comments from Journal_IF_Details j,Journal_M k where j.Id=k.Id and j.Id='" + txtJid.Text.Trim() + "'";
            }
            else if (PubIDSearch.Text != "" && drpPubStatusSearch.SelectedValue == "A" && TextBoxWorkItemSearch.Text != "" && TextBoxEnteredBySearch.Text == ""
  && DropDownListInst.SelectedValue == "A" && DropDownListDept.SelectedValue != "A")
            {
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("PublicationID", PubIDSearch.Text.Trim());
                SqlDataSource1.SelectParameters.Add("TitleWorkItem", TextBoxWorkItemSearch.Text);
                SqlDataSource1.SelectParameters.Add("Department", DropDownListDept.SelectedValue);

                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName ,t.EntryName,TitleWorkItem,c.PubCatName,ISStudentAuthor from Publication p,Status_Publication_M s, PubMUCategorization_M c ,PublicationTypeEntry_M t  where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization  and  PublicationID like '%' + @PublicationID + '%' and TitleWorkItem like '%' + @TitleWorkItem + '%' and Department=@Department ";
                // SqlDataSource1.SelectCommand = "select j.Id as Id,Year,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,Comments from Journal_IF_Details j,Journal_M k where j.Id=k.Id and j.Id='" + txtJid.Text.Trim() + "'";
            }
            else if (PubIDSearch.Text != "" && drpPubStatusSearch.SelectedValue == "A" && TextBoxWorkItemSearch.Text != "" && TextBoxEnteredBySearch.Text == ""
         && DropDownListInst.SelectedValue != "A" && DropDownListDept.SelectedValue == "A")
            {
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("PublicationID", PubIDSearch.Text.Trim());
                SqlDataSource1.SelectParameters.Add("TitleWorkItem", TextBoxWorkItemSearch.Text);
                SqlDataSource1.SelectParameters.Add("Institution", DropDownListInst.SelectedValue);
                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName ,t.EntryName,TitleWorkItem,c.PubCatName,ISStudentAuthor from Publication p,Status_Publication_M s, PubMUCategorization_M c ,PublicationTypeEntry_M t  where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization  and  PublicationID like '%' + @PublicationID + '%' and TitleWorkItem like '%' + @TitleWorkItem + '%'  and Institution=@Institution ";
                // SqlDataSource1.SelectCommand = "select j.Id as Id,Year,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,Comments from Journal_IF_Details j,Journal_M k where j.Id=k.Id and j.Id='" + txtJid.Text.Trim() + "'";
            }
            else if (PubIDSearch.Text != "" && drpPubStatusSearch.SelectedValue == "A" && TextBoxWorkItemSearch.Text != "" && TextBoxEnteredBySearch.Text == ""
&& DropDownListInst.SelectedValue != "A" && DropDownListDept.SelectedValue != "A")
            {
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("PublicationID", PubIDSearch.Text.Trim());
                SqlDataSource1.SelectParameters.Add("TitleWorkItem", TextBoxWorkItemSearch.Text);
                SqlDataSource1.SelectParameters.Add("Institution", DropDownListInst.SelectedValue);
                SqlDataSource1.SelectParameters.Add("Department", DropDownListDept.SelectedValue);

                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName ,t.EntryName,TitleWorkItem,c.PubCatName,ISStudentAuthor from Publication p,Status_Publication_M s, PubMUCategorization_M c ,PublicationTypeEntry_M t  where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization  and  PublicationID like '%' + @PublicationID + '%' and TitleWorkItem like '%' + @TitleWorkItem + '%'  and Institution=@Institution and Department=@Department";
                // SqlDataSource1.SelectCommand = "select j.Id as Id,Year,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,Comments from Journal_IF_Details j,Journal_M k where j.Id=k.Id and j.Id='" + txtJid.Text.Trim() + "'";
            }
            else if (PubIDSearch.Text != "" && drpPubStatusSearch.SelectedValue == "A" && TextBoxWorkItemSearch.Text != "" && TextBoxEnteredBySearch.Text != ""
                && DropDownListInst.SelectedValue == "A" && DropDownListDept.SelectedValue == "A")
            {
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("PublicationID", PubIDSearch.Text.Trim());
                SqlDataSource1.SelectParameters.Add("TitleWorkItem", TextBoxWorkItemSearch.Text);
                SqlDataSource1.SelectParameters.Add("CreatedBy", TextBoxEnteredBySearch.Text);


                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName ,t.EntryName,TitleWorkItem,c.PubCatName,ISStudentAuthor from Publication p,Status_Publication_M s, PubMUCategorization_M c ,PublicationTypeEntry_M t  where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization  and  PublicationID like '%' + @PublicationID + '%' and TitleWorkItem like '%' + @TitleWorkItem + '%'  and CreatedBy like '%' + @CreatedBy + '%'";
                // SqlDataSource1.SelectCommand = "select j.Id as Id,Year,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,Comments from Journal_IF_Details j,Journal_M k where j.Id=k.Id and j.Id='" + txtJid.Text.Trim() + "'";
            }
            else if (PubIDSearch.Text != "" && drpPubStatusSearch.SelectedValue == "A" && TextBoxWorkItemSearch.Text != "" && TextBoxEnteredBySearch.Text != ""
   && DropDownListInst.SelectedValue == "A" && DropDownListDept.SelectedValue != "A")
            {
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("TitleWorkItem", TextBoxWorkItemSearch.Text);
                SqlDataSource1.SelectParameters.Add("CreatedBy", TextBoxEnteredBySearch.Text);
                SqlDataSource1.SelectParameters.Add("Department", DropDownListDept.SelectedValue);
                SqlDataSource1.SelectParameters.Add("PublicationID", PubIDSearch.Text.Trim());

                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName ,t.EntryName,TitleWorkItem,c.PubCatName,ISStudentAuthor from Publication p,Status_Publication_M s, PubMUCategorization_M c ,PublicationTypeEntry_M t  where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization  and  PublicationID like '%' + @PublicationID + '%' and TitleWorkItem like '%' + @TitleWorkItem + '%' and CreatedBy like '%' + @CreatedBy + '%' and Department=@Department";
                // SqlDataSource1.SelectCommand = "select j.Id as Id,Year,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,Comments from Journal_IF_Details j,Journal_M k where j.Id=k.Id and j.Id='" + txtJid.Text.Trim() + "'";
            }
            else if (PubIDSearch.Text != "" && drpPubStatusSearch.SelectedValue == "A" && TextBoxWorkItemSearch.Text != "" && TextBoxEnteredBySearch.Text != ""
         && DropDownListInst.SelectedValue != "A" && DropDownListDept.SelectedValue == "A")
            {
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("TitleWorkItem", TextBoxWorkItemSearch.Text);
                SqlDataSource1.SelectParameters.Add("CreatedBy", TextBoxEnteredBySearch.Text);
                SqlDataSource1.SelectParameters.Add("Institution", DropDownListInst.SelectedValue);
                SqlDataSource1.SelectParameters.Add("PublicationID", PubIDSearch.Text.Trim());
                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName ,t.EntryName,TitleWorkItem,c.PubCatName,ISStudentAuthor from Publication p,Status_Publication_M s, PubMUCategorization_M c ,PublicationTypeEntry_M t  where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization  and  PublicationID like '%' + @PublicationID + '%' and TitleWorkItem like '%' + @TitleWorkItem + '%'  and CreatedBy like '%' + @CreatedBy + '%' and Institution=@Institution";
                // SqlDataSource1.SelectCommand = "select j.Id as Id,Year,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,Comments from Journal_IF_Details j,Journal_M k where j.Id=k.Id and j.Id='" + txtJid.Text.Trim() + "'";
            }
            else if (PubIDSearch.Text != "" && drpPubStatusSearch.SelectedValue == "A" && TextBoxWorkItemSearch.Text != "" && TextBoxEnteredBySearch.Text != ""
&& DropDownListInst.SelectedValue != "A" && DropDownListDept.SelectedValue != "A")
            {
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("TitleWorkItem", TextBoxWorkItemSearch.Text);
                SqlDataSource1.SelectParameters.Add("CreatedBy", TextBoxEnteredBySearch.Text);
                SqlDataSource1.SelectParameters.Add("Institution", DropDownListInst.SelectedValue);
                SqlDataSource1.SelectParameters.Add("PublicationID", PubIDSearch.Text.Trim());
                SqlDataSource1.SelectParameters.Add("Department", DropDownListDept.SelectedValue);

                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName ,t.EntryName,TitleWorkItem,c.PubCatName,ISStudentAuthor from Publication p,Status_Publication_M s, PubMUCategorization_M c ,PublicationTypeEntry_M t  where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization  and  PublicationID like '%' + @PublicationID + '%' and TitleWorkItem like '%' + @TitleWorkItem + '%' and CreatedBy like '%' + @CreatedBy + '%'  and Institution=@Institution and Department=@Department";
                // SqlDataSource1.SelectCommand = "select j.Id as Id,Year,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,Comments from Journal_IF_Details j,Journal_M k where j.Id=k.Id and j.Id='" + txtJid.Text.Trim() + "'";
            }
            else if (PubIDSearch.Text != "" && drpPubStatusSearch.SelectedValue != "A" && TextBoxWorkItemSearch.Text == "" && TextBoxEnteredBySearch.Text == ""
                && DropDownListInst.SelectedValue == "A" && DropDownListDept.SelectedValue == "A")
            {
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("PublicationID", PubIDSearch.Text.Trim());
                SqlDataSource1.SelectParameters.Add("Status", drpPubStatusSearch.SelectedValue);

                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName,t.EntryName,TitleWorkItem,c.PubCatName,ISStudentAuthor from Publication p,Status_Publication_M s, PubMUCategorization_M c ,PublicationTypeEntry_M t  where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization  and  Status=@Status  and PublicationID like '%' + @PublicationID + '%' ";
                // SqlDataSource1.SelectCommand = "select j.Id as Id,Year,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,Comments from Journal_IF_Details j,Journal_M k where j.Id=k.Id and j.Id='" + txtJid.Text.Trim() + "'";
            }
            else if (PubIDSearch.Text != "" && drpPubStatusSearch.SelectedValue != "A" && TextBoxWorkItemSearch.Text == "" && TextBoxEnteredBySearch.Text == ""
&& DropDownListInst.SelectedValue == "A" && DropDownListDept.SelectedValue != "A")
            {
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("PublicationID", PubIDSearch.Text.Trim());
                SqlDataSource1.SelectParameters.Add("Status", drpPubStatusSearch.SelectedValue);
                SqlDataSource1.SelectParameters.Add("Department", DropDownListDept.SelectedValue);

                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName,t.EntryName,TitleWorkItem,c.PubCatName,ISStudentAuthor from Publication p,Status_Publication_M s, PubMUCategorization_M c ,PublicationTypeEntry_M t  where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization  and  Status=@Status  and PublicationID like '%' + @PublicationID + '%' and Department=@Department ";
                // SqlDataSource1.SelectCommand = "select j.Id as Id,Year,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,Comments from Journal_IF_Details j,Journal_M k where j.Id=k.Id and j.Id='" + txtJid.Text.Trim() + "'";
            }
            else if (PubIDSearch.Text != "" && drpPubStatusSearch.SelectedValue != "A" && TextBoxWorkItemSearch.Text == "" && TextBoxEnteredBySearch.Text == ""
        && DropDownListInst.SelectedValue != "A" && DropDownListDept.SelectedValue == "A")
            {
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("PublicationID", PubIDSearch.Text.Trim());
                SqlDataSource1.SelectParameters.Add("Status", drpPubStatusSearch.SelectedValue);
                SqlDataSource1.SelectParameters.Add("Institution", DropDownListInst.SelectedValue);

                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName,t.EntryName,TitleWorkItem,c.PubCatName,ISStudentAuthor from Publication p,Status_Publication_M s, PubMUCategorization_M c ,PublicationTypeEntry_M t  where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization  and  Status=@Status  and PublicationID like '%' + @PublicationID + '%'  and Institution=@Institution ";
                // SqlDataSource1.SelectCommand = "select j.Id as Id,Year,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,Comments from Journal_IF_Details j,Journal_M k where j.Id=k.Id and j.Id='" + txtJid.Text.Trim() + "'";
            }
            else if (PubIDSearch.Text != "" && drpPubStatusSearch.SelectedValue != "A" && TextBoxWorkItemSearch.Text == "" && TextBoxEnteredBySearch.Text == ""
&& DropDownListInst.SelectedValue != "A" && DropDownListDept.SelectedValue != "A")
            {
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("PublicationID", PubIDSearch.Text.Trim());
                SqlDataSource1.SelectParameters.Add("Status", drpPubStatusSearch.SelectedValue);
                SqlDataSource1.SelectParameters.Add("Institution", DropDownListInst.SelectedValue);
                SqlDataSource1.SelectParameters.Add("Department", DropDownListDept.SelectedValue);

                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName,t.EntryName,TitleWorkItem,c.PubCatName,ISStudentAuthor from Publication p,Status_Publication_M s, PubMUCategorization_M c ,PublicationTypeEntry_M t  where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization  and  Status=@Status  and PublicationID like '%' + @PublicationID + '%'  and Institution=@Institution and Department=@Department";
                // SqlDataSource1.SelectCommand = "select j.Id as Id,Year,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,Comments from Journal_IF_Details j,Journal_M k where j.Id=k.Id and j.Id='" + txtJid.Text.Trim() + "'";
            }
            else if (PubIDSearch.Text != "" && drpPubStatusSearch.SelectedValue != "A" && TextBoxWorkItemSearch.Text == "" && TextBoxEnteredBySearch.Text != ""
                && DropDownListInst.SelectedValue == "A" && DropDownListDept.SelectedValue == "A")
            {
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("PublicationID", PubIDSearch.Text.Trim());
                SqlDataSource1.SelectParameters.Add("Status", drpPubStatusSearch.SelectedValue);
                SqlDataSource1.SelectParameters.Add("CreatedBy", TextBoxEnteredBySearch.Text);

                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName,t.EntryName,TitleWorkItem,c.PubCatName,ISStudentAuthor from Publication p,Status_Publication_M s, PubMUCategorization_M c ,PublicationTypeEntry_M t  where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization  and  Status=@Status  and PublicationID like '%' + @PublicationID + '%' and CreatedBy like '%' + @CreatedBy + '%'";
                // SqlDataSource1.SelectCommand = "select j.Id as Id,Year,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,Comments from Journal_IF_Details j,Journal_M k where j.Id=k.Id and j.Id='" + txtJid.Text.Trim() + "'";
            }
            else if (PubIDSearch.Text != "" && drpPubStatusSearch.SelectedValue != "A" && TextBoxWorkItemSearch.Text == "" && TextBoxEnteredBySearch.Text != ""
          && DropDownListInst.SelectedValue == "A" && DropDownListDept.SelectedValue != "A")
            {
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("PublicationID", PubIDSearch.Text.Trim());
                SqlDataSource1.SelectParameters.Add("Status", drpPubStatusSearch.SelectedValue);
                SqlDataSource1.SelectParameters.Add("CreatedBy", TextBoxEnteredBySearch.Text);
                SqlDataSource1.SelectParameters.Add("Department", DropDownListDept.SelectedValue);

                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName,t.EntryName,TitleWorkItem,c.PubCatName,ISStudentAuthor from Publication p,Status_Publication_M s, PubMUCategorization_M c ,PublicationTypeEntry_M t  where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization  and  Status=@Status  and PublicationID like '%' + @PublicationID + '%' and CreatedBy like '%' + @CreatedBy + '%' and Department=@Department";
                // SqlDataSource1.SelectCommand = "select j.Id as Id,Year,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,Comments from Journal_IF_Details j,Journal_M k where j.Id=k.Id and j.Id='" + txtJid.Text.Trim() + "'";
            }
            else if (PubIDSearch.Text != "" && drpPubStatusSearch.SelectedValue != "A" && TextBoxWorkItemSearch.Text == "" && TextBoxEnteredBySearch.Text != ""
          && DropDownListInst.SelectedValue != "A" && DropDownListDept.SelectedValue == "A")
            {
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("PublicationID", PubIDSearch.Text.Trim());
                SqlDataSource1.SelectParameters.Add("Status", drpPubStatusSearch.SelectedValue);
                SqlDataSource1.SelectParameters.Add("CreatedBy", TextBoxEnteredBySearch.Text);
                SqlDataSource1.SelectParameters.Add("Institution", DropDownListInst.SelectedValue);

                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName,t.EntryName,TitleWorkItem,c.PubCatName,ISStudentAuthor from Publication p,Status_Publication_M s, PubMUCategorization_M c ,PublicationTypeEntry_M t  where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization  and  Status=@Status  and PublicationID like '%' + @PublicationID + '%' and CreatedBy like '%' + @CreatedBy + '%'  and Institution=@Institution";
                // SqlDataSource1.SelectCommand = "select j.Id as Id,Year,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,Comments from Journal_IF_Details j,Journal_M k where j.Id=k.Id and j.Id='" + txtJid.Text.Trim() + "'";
            }
            else if (PubIDSearch.Text != "" && drpPubStatusSearch.SelectedValue != "A" && TextBoxWorkItemSearch.Text == "" && TextBoxEnteredBySearch.Text != ""
 && DropDownListInst.SelectedValue != "A" && DropDownListDept.SelectedValue != "A")
            {
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("PublicationID", PubIDSearch.Text.Trim());
                SqlDataSource1.SelectParameters.Add("Status", drpPubStatusSearch.SelectedValue);
                SqlDataSource1.SelectParameters.Add("CreatedBy", TextBoxEnteredBySearch.Text);
                SqlDataSource1.SelectParameters.Add("Institution", DropDownListInst.SelectedValue);
                SqlDataSource1.SelectParameters.Add("Department", DropDownListDept.SelectedValue);

                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName,t.EntryName,TitleWorkItem,c.PubCatName,ISStudentAuthor from Publication p,Status_Publication_M s, PubMUCategorization_M c ,PublicationTypeEntry_M t  where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization  and  Status=@Status  and PublicationID like '%' + @PublicationID + '%' and CreatedBy like '%' + @CreatedBy + '%'  and Institution=@Institution and Department=@Department";
                // SqlDataSource1.SelectCommand = "select j.Id as Id,Year,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,Comments from Journal_IF_Details j,Journal_M k where j.Id=k.Id and j.Id='" + txtJid.Text.Trim() + "'";
            }
            else if (PubIDSearch.Text != "" && drpPubStatusSearch.SelectedValue != "A" && TextBoxWorkItemSearch.Text != "" && TextBoxEnteredBySearch.Text == ""
                && DropDownListInst.SelectedValue == "A" && DropDownListDept.SelectedValue == "A")
            {
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("PublicationID", PubIDSearch.Text.Trim());
                SqlDataSource1.SelectParameters.Add("Status", drpPubStatusSearch.SelectedValue);
                SqlDataSource1.SelectParameters.Add("TitleWorkItem", TextBoxWorkItemSearch.Text);

                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName,t.EntryName,TitleWorkItem,c.PubCatName,ISStudentAuthor from Publication p,Status_Publication_M s, PubMUCategorization_M c ,PublicationTypeEntry_M t  where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization  and  Status=@Status  and PublicationID like '%' + @PublicationID + '%' and TitleWorkItem like '%' + @TitleWorkItem + '%'  ";
                // SqlDataSource1.SelectCommand = "select j.Id as Id,Year,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,Comments from Journal_IF_Details j,Journal_M k where j.Id=k.Id and j.Id='" + txtJid.Text.Trim() + "'";
            }
            else if (PubIDSearch.Text != "" && drpPubStatusSearch.SelectedValue != "A" && TextBoxWorkItemSearch.Text != "" && TextBoxEnteredBySearch.Text == ""
     && DropDownListInst.SelectedValue == "A" && DropDownListDept.SelectedValue != "A")
            {
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("PublicationID", PubIDSearch.Text.Trim());
                SqlDataSource1.SelectParameters.Add("Status", drpPubStatusSearch.SelectedValue);
                SqlDataSource1.SelectParameters.Add("TitleWorkItem", TextBoxWorkItemSearch.Text);
                SqlDataSource1.SelectParameters.Add("Department", DropDownListDept.SelectedValue);

                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName,t.EntryName,TitleWorkItem,c.PubCatName,ISStudentAuthor from Publication p,Status_Publication_M s, PubMUCategorization_M c ,PublicationTypeEntry_M t  where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization  and  Status=@Status  and PublicationID like '%' + @PublicationID + '%' and TitleWorkItem like '%' + @TitleWorkItem + '%' and Department=@Department";
                // SqlDataSource1.SelectCommand = "select j.Id as Id,Year,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,Comments from Journal_IF_Details j,Journal_M k where j.Id=k.Id and j.Id='" + txtJid.Text.Trim() + "'";
            }
            else if (PubIDSearch.Text != "" && drpPubStatusSearch.SelectedValue != "A" && TextBoxWorkItemSearch.Text != "" && TextBoxEnteredBySearch.Text == ""
       && DropDownListInst.SelectedValue != "A" && DropDownListDept.SelectedValue == "A")
            {
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("PublicationID", PubIDSearch.Text.Trim());
                SqlDataSource1.SelectParameters.Add("Status", drpPubStatusSearch.SelectedValue);
                SqlDataSource1.SelectParameters.Add("TitleWorkItem", TextBoxWorkItemSearch.Text);
                SqlDataSource1.SelectParameters.Add("Institution", DropDownListInst.SelectedValue);

                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName,t.EntryName,TitleWorkItem,c.PubCatName,ISStudentAuthor from Publication p,Status_Publication_M s, PubMUCategorization_M c ,PublicationTypeEntry_M t  where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization  and  Status=@Status and PublicationID like '%' + @PublicationID + '%' and TitleWorkItem like '%' + @TitleWorkItem + '%'  and Institution=@Institution ";
                // SqlDataSource1.SelectCommand = "select j.Id as Id,Year,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,Comments from Journal_IF_Details j,Journal_M k where j.Id=k.Id and j.Id='" + txtJid.Text.Trim() + "'";
            }
            else if (PubIDSearch.Text != "" && drpPubStatusSearch.SelectedValue != "A" && TextBoxWorkItemSearch.Text != "" && TextBoxEnteredBySearch.Text == ""
&& DropDownListInst.SelectedValue != "A" && DropDownListDept.SelectedValue != "A")
            {
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("PublicationID", PubIDSearch.Text.Trim());
                SqlDataSource1.SelectParameters.Add("Status", drpPubStatusSearch.SelectedValue);
                SqlDataSource1.SelectParameters.Add("TitleWorkItem", TextBoxWorkItemSearch.Text);
                SqlDataSource1.SelectParameters.Add("Institution", DropDownListInst.SelectedValue);
                SqlDataSource1.SelectParameters.Add("Department", DropDownListDept.SelectedValue);

                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName,t.EntryName,TitleWorkItem,c.PubCatName,ISStudentAuthor from Publication p,Status_Publication_M s, PubMUCategorization_M c ,PublicationTypeEntry_M t  where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization  and  Status=@Status  and PublicationID like '%' + @PublicationID + '%' and TitleWorkItem like '%' + @TitleWorkItem + '%'  and Institution=@Institution and Department=@Department";
                // SqlDataSource1.SelectCommand = "select j.Id as Id,Year,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,Comments from Journal_IF_Details j,Journal_M k where j.Id=k.Id and j.Id='" + txtJid.Text.Trim() + "'";
            }
            else if (PubIDSearch.Text != "" && drpPubStatusSearch.SelectedValue != "A" && TextBoxWorkItemSearch.Text != "" && TextBoxEnteredBySearch.Text != ""
                && DropDownListInst.SelectedValue == "A" && DropDownListDept.SelectedValue == "A")
            {
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("PublicationID", PubIDSearch.Text.Trim());
                SqlDataSource1.SelectParameters.Add("Status", drpPubStatusSearch.SelectedValue);
                SqlDataSource1.SelectParameters.Add("TitleWorkItem", TextBoxWorkItemSearch.Text);
                SqlDataSource1.SelectParameters.Add("CreatedBy", TextBoxEnteredBySearch.Text);

                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName,t.EntryName,TitleWorkItem,c.PubCatName,ISStudentAuthor from Publication p,Status_Publication_M s, PubMUCategorization_M c ,PublicationTypeEntry_M t  where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization  and  Status=@Status  and PublicationID like '%' + @PublicationID + '%' and TitleWorkItem like '%' + @TitleWorkItem + '%' and CreatedBy like '%' + @CreatedBy + '%'";
                // SqlDataSource1.SelectCommand = "select j.Id as Id,Year,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,Comments from Journal_IF_Details j,Journal_M k where j.Id=k.Id and j.Id='" + txtJid.Text.Trim() + "'";
            }
            else if (PubIDSearch.Text != "" && drpPubStatusSearch.SelectedValue != "A" && TextBoxWorkItemSearch.Text != "" && TextBoxEnteredBySearch.Text != ""
      && DropDownListInst.SelectedValue == "A" && DropDownListDept.SelectedValue != "A")
            {

                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("PublicationID", PubIDSearch.Text.Trim());
                SqlDataSource1.SelectParameters.Add("Status", drpPubStatusSearch.SelectedValue);
                SqlDataSource1.SelectParameters.Add("TitleWorkItem", TextBoxWorkItemSearch.Text);
                SqlDataSource1.SelectParameters.Add("CreatedBy", TextBoxEnteredBySearch.Text);
                SqlDataSource1.SelectParameters.Add("Department", DropDownListDept.SelectedValue);

                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName,t.EntryName,TitleWorkItem,c.PubCatName,ISStudentAuthor from Publication p,Status_Publication_M s, PubMUCategorization_M c ,PublicationTypeEntry_M t  where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization  and  Status=@Status  and PublicationID like '%' + @PublicationID + '%' and TitleWorkItem like '%' + @TitleWorkItem + '%'  and CreatedBy like '%' + @CreatedBy + '%' and Department=@Department";
                // SqlDataSource1.SelectCommand = "select j.Id as Id,Year,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,Comments from Journal_IF_Details j,Journal_M k where j.Id=k.Id and j.Id='" + txtJid.Text.Trim() + "'";
            }
            else if (PubIDSearch.Text != "" && drpPubStatusSearch.SelectedValue != "A" && TextBoxWorkItemSearch.Text != "" && TextBoxEnteredBySearch.Text != ""
             && DropDownListInst.SelectedValue != "A" && DropDownListDept.SelectedValue == "A")
            {
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("PublicationID", PubIDSearch.Text.Trim());
                SqlDataSource1.SelectParameters.Add("Status", drpPubStatusSearch.SelectedValue);
                SqlDataSource1.SelectParameters.Add("TitleWorkItem", TextBoxWorkItemSearch.Text);
                SqlDataSource1.SelectParameters.Add("CreatedBy", TextBoxEnteredBySearch.Text);
                SqlDataSource1.SelectParameters.Add("Institution", DropDownListInst.SelectedValue);

                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName,t.EntryName,TitleWorkItem,c.PubCatName,ISStudentAuthor from Publication p,Status_Publication_M s, PubMUCategorization_M c ,PublicationTypeEntry_M t  where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization  and  Status=@Status  and PublicationID like '%' + @PublicationID + '%' and TitleWorkItem like '%' + @TitleWorkItem + '%' and CreatedBy like '%' + @CreatedBy + '%'  and Institution=@Institution ";
                // SqlDataSource1.SelectCommand = "select j.Id as Id,Year,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,Comments from Journal_IF_Details j,Journal_M k where j.Id=k.Id and j.Id='" + txtJid.Text.Trim() + "'";
            }
            else if (PubIDSearch.Text != "" && drpPubStatusSearch.SelectedValue != "A" && TextBoxWorkItemSearch.Text != "" && TextBoxEnteredBySearch.Text != ""
         && DropDownListInst.SelectedValue != "A" && DropDownListDept.SelectedValue != "A")
            {
                SqlDataSource1.SelectParameters.Clear();
                SqlDataSource1.SelectParameters.Add("PublicationID", PubIDSearch.Text.Trim());
                SqlDataSource1.SelectParameters.Add("Status", drpPubStatusSearch.SelectedValue);
                SqlDataSource1.SelectParameters.Add("TitleWorkItem", TextBoxWorkItemSearch.Text);
                SqlDataSource1.SelectParameters.Add("CreatedBy", TextBoxEnteredBySearch.Text);
                SqlDataSource1.SelectParameters.Add("Institution", DropDownListInst.SelectedValue);
                SqlDataSource1.SelectParameters.Add("Department", DropDownListDept.SelectedValue);

                SqlDataSource1.SelectCommand = "select PublicationID,TypeOfEntry,PubJournalID,PublishJAMonth,PublishJAYear,ImpactFactor  ,s.StatusName,t.EntryName,TitleWorkItem,c.PubCatName,ISStudentAuthor from Publication p,Status_Publication_M s, PubMUCategorization_M c ,PublicationTypeEntry_M t  where p.Status=s.StatusId and t.TypeEntryId=p.TypeOfEntry and c.PubCatId=p.MUCategorization  and  Status=@Status  and PublicationID like '%' + @PublicationID + '%' and TitleWorkItem like '%' + @TitleWorkItem + '%'  and CreatedBy like '%' + @CreatedBy + '%'  and Institution=@Institution and Department=@Department";
                // SqlDataSource1.SelectCommand = "select j.Id as Id,Year,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,Comments from Journal_IF_Details j,Journal_M k where j.Id=k.Id and j.Id='" + txtJid.Text.Trim() + "'";
            }
        }
        GridViewSearch.DataBind();
        SqlDataSource1.DataBind();
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


    }

    protected void popSelected(Object sender, EventArgs e)
    {

        if (senderID.Value.Contains("imageBkCbtn"))
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


        }
      

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

        //popupPanelAffil.Visible = false;

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
        if (j.jid!=null)
        {
            // ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Entry ALready Exists')</script>");
            

            string year = DateTime.Now.Year.ToString();
            int Jyear = Convert.ToInt32(year) - 1;

           // TextBoxYearJA.Text = Jyear.ToString();
           // txtboxYear_TextChanged(sender, e);

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


            SqlDataSourceAuthorType.SelectCommand = "SELECT Id,Type FROM [Author_Type_M] where (Id! = 'O') and  (Id! = 'E')";

            DropdownMuNonMu.DataTextField = "Type";
            DropdownMuNonMu.DataValueField = "Id";
            DropdownMuNonMu.DataBind();

        }
        else if (DropDownListPublicationEntry.SelectedValue == "JA" || DropDownListPublicationEntry.SelectedValue == "TS" || DropDownListPublicationEntry.SelectedValue == "PR")
        {
            SqlDataSourceAuthorType.SelectCommand = "SELECT Id,DisplayName FROM [Author_Type_M] where (Id = 'M') or (Id = 'S') or (Id = 'N') or (Id = 'O') or  (Id = 'E') ";

            DropdownMuNonMu.DataTextField = "DisplayName";
            DropdownMuNonMu.DataValueField = "Id";
            DropdownMuNonMu.DataBind();

        }
       // Author.Text = Session["User"].ToString();
      //  Author.Enabled = false;
        AuthorName.Text = Session["UserName"].ToString();
        AuthorName.Enabled = false;
        EmployeeCode.Text = Session["UserId"].ToString();
        EmployeeCode.Enabled = false;

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
           Grid_AuthorEntry.Columns[6].Visible=true;
  
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
            if (DropdownMuNonMu.SelectedValue == "M")
            {
                EmployeeCodeBtn.Enabled = true;
            }
            else
            {
                EmployeeCodeBtn.Enabled = false;
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
                        TextBox EmployeeCode1 = (TextBox)Grid_AuthorEntry.Rows[0].Cells[0].FindControl("EmployeeCode");
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
                        EmployeeCode1.Enabled = false;
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
                if (dt.Rows.Count > 1 && rowIndex!=0)
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




    //protected void txtboxYear_TextChanged(object sender, EventArgs e)
    //{
    //    JournalValueObj.year = TextBoxYearJA.Text;
    //    JournalValueObj.JournalID = TextBoxPubJournal.Text;
    //    if (TextBoxYearJA.Text != "")
    //    {

    //        int res = B.JournalGetImpactFactor(JournalValueObj);
    //        if (res == 1)
    //        { TextBoxImpFact.Text = JournalValueObj.impctfact.ToString(); }
    //    }
    //    else
    //    {
    //        TextBoxImpFact.Text = "";
    //}

    //}
    protected void showPop(object sender, EventArgs e)
    { popupPanelJournal.Visible = true;
        ModalPopupExtender1.Show();
       // popupPanelAffil.Visible = false;
    }

    protected void DropDownListPublicationEntryOnSelectedIndexChanged(object sender, EventArgs e)
    {
        setModalWindow(sender, e);
        
       // popupPanelAffil.Visible = true;
       // btnSave.Enabled = true;
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

  


    


    protected void addclik(object sender, EventArgs e)
    {   string confirmValue2 = Request.Form["confirm_value2"];
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

        TextBoxYearJA.Text="";

        TextBoxImpFact.Text = "";
        TextBoxImpFact5.Text = "";

        DropDownListPubType.ClearSelection();
        TextBoxPageFrom.Text = "";

        TextBoxPageTo.Text = "";
        TextBoxVolume.Text = "";
        RadioButtonListIndexed.SelectedValue="N";
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

       // popupPanelAffil.Visible = false;
        popupPanelJournal.Visible=false;



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
        GridViewSearch.Visible = false;
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
    }

    public void fnRecordExist(object sender, EventArgs e)
    {
        //popupPanelAffil.Visible = false;
        panel.Visible = true;
        string Pid = Session["TempPid"].ToString();
        string TypeEntry = Session["TempTypeEntry"].ToString();
        //btnSave.Enabled = true;

        Business obj = new Business();
        PublishData v = new PublishData();
        v = obj.fnfindjid(Pid, TypeEntry);
        DropDownListMuCategory.DataBind();
        SqlDataSourceMuCategory.DataBind();

        DSforgridview.SelectParameters.Clear();
        DSforgridview.SelectParameters.Add("PublicationID", Pid);
        DSforgridview.SelectParameters.Add("TypeOfEntry", TypeEntry);

        DSforgridview.SelectCommand = "select UploadPDFPath from Publication where PublicationID=@PublicationID and TypeOfEntry=@TypeOfEntry";
        DSforgridview.DataBind();
        GVViewFile.DataBind();
        string FileUpload1 = "";
        Business b1 = new Business();
        FileUpload1 = b1.GetFileUploadPath(Pid, TypeEntry);
        if (FileUpload1 != "")
        {
            GVViewFile.Visible = true;
            lblNoPDF.Visible = false;
        }
        else
        {
            GVViewFile.Visible = false;
            lblNoPDF.Visible = true;
        }

      DropDownListMuCategory.SelectedValue  =v.MUCategorization ;

      DropDownListPublicationEntry.DataBind();
      SqlDataSourcePublicationEntry.DataBind();
      DropDownListPublicationEntry.SelectedValue = TypeEntry;
      TextBoxPubId.Text = Pid;

      DropDownListhasProjectreference.SelectedValue = v.hasProjectreference;
      TextBoxProjectDetails.Text = v.ProjectIDlist;
      if (v.hasProjectreference == "0" || v.hasProjectreference == "N")
      {
          LabelProjectReference.Visible = true;
          DropDownListhasProjectreference.Visible = true;
          LabelProjectDetails.Visible = false;
          TextBoxProjectDetails.Visible = false;
          ImageButtonProject.Visible = false;
      }
      else if (v.hasProjectreference == "Y")
      {
          LabelProjectReference.Visible = true;
          DropDownListhasProjectreference.Visible = true;
          LabelProjectDetails.Visible = true;
          TextBoxProjectDetails.Visible = true;
          ImageButtonProject.Visible = false;
      }
        txtboxTitleOfWrkItem.Text = v.TitleWorkItem;
        if (v.Status == "CAN")
        {
            LabelCanRemarks.Visible = true;
            TextBoxCanRemarks.Visible = true;
            TextBoxCanRemarks.Text = v.PubCancelRemarks;

            LabelRejRemarks.Visible = false;
            TextBoxPubRejRemarks.Visible = false;
            TextBoxPubRejRemarks.Text = "";
        }
        else if (v.Status == "REW")
        {
            LabelCanRemarks.Visible = false;
            TextBoxCanRemarks.Visible = false;
            TextBoxCanRemarks.Text = "";

            LabelRejRemarks.Visible = true;
            TextBoxPubRejRemarks.Visible = true;
            TextBoxPubRejRemarks.Text = v.RemarksFeedback;


        }
        else
        {
            LabelCanRemarks.Visible = false;
            TextBoxCanRemarks.Visible = false;
            TextBoxCanRemarks.Text ="";

            LabelRejRemarks.Visible = false;
            TextBoxPubRejRemarks.Visible = false;
            TextBoxPubRejRemarks.Text = "";

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
            DropDownListPubType.SelectedValue =v.Publicationtype;
            TextBoxPageFrom.Text = v.PageFrom;
            TextBoxPageTo.Text = v.PageTo;
          
            TextBoxIssue.Text = v.Issue;          
            TextBoxVolume.Text = v.JAVolume;
            RadioButtonListIndexed.SelectedValue = v.Indexed;
            // CheckboxIndexAgency.ClearSelection();
            JournalData jd = new JournalData();
            Business B = new Business();
            int jayear = Convert.ToInt16(TextBoxYearJA.Text);
            int jamonth = Convert.ToInt16(DropDownListMonthJA.SelectedValue);
            if (jayear >= 2018 && jamonth >= 7)
            {
                if (v.QuartileOnIncentivize != "")
                {
                    jd = B.selectQuartilevaluefrompublication(TextBoxPubId.Text, DropDownListMuCategory.SelectedValue, DropDownListPublicationEntry.SelectedValue);
                    lblQuartileid.Visible = true;
                    txtquartileid.Visible = true;
                    txtquartileid.Text = jd.QName;
                }
               if(v.QuartileOnEntry!="")
                {
                    jd = B.selectQuartilevaluefrompublicationEntry(TextBoxPubId.Text, DropDownListMuCategory.SelectedValue, DropDownListPublicationEntry.SelectedValue);
                }
               lblQuartile.Visible = true;
               txtquartile.Visible = true;
               txtquartile.Text = jd.QName;
               
            }
            else
                if (jayear >= 2019 && jamonth >= 1)
                {
                    if (v.QuartileOnIncentivize != "")
                    {
                        jd = B.selectQuartilevaluefrompublication(TextBoxPubId.Text, DropDownListMuCategory.SelectedValue, DropDownListPublicationEntry.SelectedValue);
                        lblQuartileid.Visible = true;
                        txtquartileid.Visible = true;
                        txtquartileid.Text = jd.QName;
                    }
                    if (v.QuartileOnEntry != "")
                    {
                        jd = B.selectQuartilevaluefrompublicationEntry(TextBoxPubId.Text, DropDownListMuCategory.SelectedValue, DropDownListPublicationEntry.SelectedValue);
                    }
                    lblQuartile.Visible = true;
                    txtquartile.Visible = true;
                    txtquartile.Text = jd.QName;

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
                DropDownListPubType.SelectedValue = v.Publicationtype;
                TextBoxPageFrom.Text = v.PageFrom;
                TextBoxPageTo.Text = v.PageTo;

                TextBoxIssue.Text = v.Issue;
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

            if (v.Date.ToShortDateString() != "01/01/0001")
            {
                TextBoxDate.Text = v.Date.ToShortDateString();
            }
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

            TextBoxPublish.Text = v.NewsPublisher;
            if (v.NewsPublishedDate.ToShortDateString() != "01/01/0001")
            {
                TextBoxDateOfPublish.Text = v.NewsPublishedDate.ToShortDateString();
            }

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
                txtIFApplicable.Text = v.IFApplicableYear.ToString();
            }
            else
            {
                txtIFApplicable.Text = "";
            }
            //txtCitation.Text = v.CitationUrl;
            // DropDownListPubType.SelectedValue=;
            
                TextBoxPageFrom.Text = v.PageFrom;
                TextBoxPageTo.Text = v.PageTo;
           
           // TextBoxPageFrom.Text = v.PageFrom;
            TextBoxIssue.Text = v.Issue;
            //TextBoxPageTo.Text = v.PageTo;
            TextBoxVolume.Text = v.JAVolume;
            RadioButtonListIndexed.SelectedValue = v.Indexed;
            // CheckboxIndexAgency.ClearSelection();
        }
        panelTechReport.Visible=true;
        //TextBoxURL.Text = v.url;
        TextBoxDOINum.Text=v.DOINum ;
            TextBoxAbstract.Text= v.Abstract ;
            DropDownListErf.SelectedValue = v.isERF; 
            TextBoxKeywords.Text= v.Keywords;
            //TextBoxReference.Text=v.TechReferences ;
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


                }
                else if (DropdownMuNonMu.Text == "S" || DropdownMuNonMu.Text == "O")
                {
                    NationalType.Visible = false;
                    ContinentId.Visible = false;


                }
                else
                {
                    NationalType.Visible = false;
                    ContinentId.Visible = false;

                }
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

                //Grid_AuthorEntry.Columns[11].Visible = false;

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
                //CheckboxIndexAgency.Enabled = true;

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
                //  CheckboxIndexAgency.Enabled = false;
            }
        }
        setModalWindow(sender, e);

        //ButtonSub.Enabled = true;
       // FileUploadPdf.Enabled = true;

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
    protected void BakButtonClick(object sender, EventArgs e)
    {
        string keyword = Request.QueryString["Keyword"];
        if (keyword == "true")
        {
            Response.Redirect("~/PublicationEntry/KeywordSearch.aspx");
        }
        else if (keyword == "false")
        {
            Response.Redirect("~/PublicationEntry/EmployeeDetailSearch.aspx");
        }
        else if (keyword == "isPagesrch")
        {
            Response.Redirect("~/PublicationEntry/DomainandResearchareaSearch.aspx?Keywordback=" + "back");
            //Response.Redirect("PublicationView.aspx?PubID=" + id + "&PubType=" + "JA" + "&Keyword=" + "isPagesrch");
        }
    }
}