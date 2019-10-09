using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;

public partial class SearchJournal : System.Web.UI.Page
{
    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    Business B = new Business();
    Journal_DataObject JournalDataObj = new Journal_DataObject();
    JournalData JournalValueObj = new JournalData();
    public string pageID = "L31";
    protected void Page_Load(object sender, EventArgs e)
    {
        //popupPanelJournal.Visible = true;

        if (!IsPostBack)
        {
            setModalWindow(sender, e);
            GridViewJournalSearch.Visible = false;
            if (!Session["authPage"].ToString().Contains("$" + pageID + "$"))
            {
                string unacces = "Unauthorized Acces!!! Login Again";
                Response.Redirect("~/Login.aspx?val=" + unacces);
            }
        }

    }

    protected void setModalWindow(object sender, EventArgs e)
    {
        //popupPanelJournal.Visible = true;
        popGridJournal.DataSourceID = "SqlDataSourcePopJournal";
        SqlDataSourceJournal.DataBind();
        popGridJournal.DataBind();

    }


    protected void GridViewJournalSearchOnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        searchclick(sender,e);
        GridViewJournalSearch.PageIndex = e.NewPageIndex;
        GridViewJournalSearch.DataBind();
    }
    protected void searchclick(object sender, EventArgs e)
    {
        //if (!Page.IsValid)
        //{
        //    return;
        //}
        try
        {
            GridViewJournalSearch.Visible = true;
            if (dropdownCategory.SelectedValue != " ")
            {
                if (txtJid.Text != "")
                {

                    SqlDataSourceJournal.SelectParameters.Clear();
                    SqlDataSourceJournal.SelectParameters.Add("Id", txtJid.Text.Trim());
                    SqlDataSourceJournal.SelectParameters.Add("JournalCategory", dropdownCategory.SelectedValue);

                    SqlDataSourceJournal.SelectCommand = "select k.Id as ISSN,Year,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,JournalCategory,Comments from Journal_M k   LEFT join Journal_IF_Details j on j.id=k.Id   where j.Id=@Id and JournalCategory=@JournalCategory";
                }
                //else if (TextYear.Text == "" && txtJid.Text != "")
                //{
                //    SqlDataSourceJournal.SelectCommand = "select j.Id as Id,Year,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,Comments from Journal_IF_Details j,Journal_M k where j.Id=k.Id and j.Id='" + txtJid.Text.Trim() + "'";
                //}
                else if (txtJid.Text == "")
                {
                    SqlDataSourceJournal.SelectParameters.Clear();
                    SqlDataSourceJournal.SelectParameters.Add("JournalCategory", dropdownCategory.SelectedValue);
                    SqlDataSourceJournal.SelectCommand = "select k.Id as ISSN,Year,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,JournalCategory,Comments from Journal_M k   LEFT join Journal_IF_Details j on j.id=k.Id  where   JournalCategory=@JournalCategory";
                }
            }
            else
            {
                if (txtJid.Text != "")
                {
                    SqlDataSourceJournal.SelectParameters.Clear();
                    SqlDataSourceJournal.SelectParameters.Add("Id", txtJid.Text.Trim());
                    SqlDataSourceJournal.SelectCommand = "select k.Id as ISSN,Year,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,JournalCategory,Comments from Journal_M k   LEFT join Journal_IF_Details j on j.id=k.Id   where j.Id=@Id";
                }
                //else if (TextYear.Text == "" && txtJid.Text != "")
                //{
                //    SqlDataSourceJournal.SelectCommand = "select j.Id as Id,Year,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,Comments from Journal_IF_Details j,Journal_M k where j.Id=k.Id and j.Id='" + txtJid.Text.Trim() + "'";
                //}
                else if (txtJid.Text == "")
                {
                    SqlDataSourceJournal.SelectCommand = "   select k.Id as ISSN,Year,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,JournalCategory,Comments from Journal_M k   LEFT join Journal_IF_Details j on j.id=k.Id ";
                }
            }
            //else
            //{
            //    SqlDataSourceJournal.SelectCommand = "select j.Id as Id,Year,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,Comments from Journal_IF_Details j,Journal_M k where j.Id=k.Id ";
            //}
            SqlDataSourceJournal.DataBind();
            GridViewJournalSearch.DataBind();
        }
        catch (Exception ex)
        {
            log.Error(ex.StackTrace);
            log.Error(ex.Message);
            ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Error!!!!!!!!!!!!!!!')</script>");
          
        }

    }

    protected void JournalIDTextChanged(object sender, EventArgs e)
    {

       


        JournalValueObj.JournalID = txtJid.Text;
        // JournalValueObj.year = txtBoxYear.Text;
        JournalData j = new JournalData();
       j = B.JournalEntryCheckExistance(JournalValueObj);
        if (j.jid!=null)
        {
           


        }
        else
        {
            ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Invalid ID')</script>");
        }


    }
    protected void showPop(object sender, EventArgs e)
    {  
      
      
        popupPanelJournal.Visible = true;
        ModalPopupExtender1.Show();
    }
    protected void JournalCodeChanged(object sender, EventArgs e)
    {

        if (journalcodeSrch.Text.Trim() == "")
        {
            SqlDataSourcePopJournal.SelectCommand = "SELECT  Id as ISSN,Title,AbbreviatedTitle FROM [Journal_M]";
            popGridJournal.DataBind();
            popGridJournal.Visible = true;
        }

        else
        {
            SqlDataSourcePopJournal.SelectParameters.Clear();
            SqlDataSourcePopJournal.SelectParameters.Add("Title", journalcodeSrch.Text);

            SqlDataSourcePopJournal.SelectCommand = "SELECT Id as ISSN,Title,AbbreviatedTitle FROM [Journal_M] where Title like '%' + @Title + '%'";
            popGridJournal.DataBind();
            popGridJournal.Visible = true;
        }

        ModalPopupExtender1.Show();
    }
    protected void popSelected(Object sender, EventArgs e)
    {


            popGridJournal.Visible = true;
            GridViewRow row = popGridJournal.SelectedRow;

            string Journalid = row.Cells[1].Text;


            txtJid.Text = Journalid;

  
            popGridJournal.DataBind();
            JournalIDTextChanged(sender, e);
            journalcodeSrch.Text = "";
            popGridJournal.DataBind();
        

    }

    protected void exit(Object sender, EventArgs e)
    {
        journalcodeSrch.Text = "";
        popGridJournal.DataBind();
    }
 
}