using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ViewImpactFactor : System.Web.UI.Page
{
    public string pageID = "L13";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GridViewJournalSearch.Visible = false;
            if (!Session["authPage"].ToString().Contains("$" + pageID + "$"))
            {
                string unacces = "Unauthorized Acces!!! Login Again";
                Response.Redirect("~/Index.aspx?val=" + unacces);
            }
        }

    }
    protected void GridViewJournalSearchOnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        searchclick(sender, e);
        GridViewJournalSearch.PageIndex = e.NewPageIndex;
        GridViewJournalSearch.DataBind();
    }
    protected void searchclick(object sender, EventArgs e)
    {
        //if (!Page.IsValid)
        //{
        //    return;
        //}

        GridViewJournalSearch.Visible = true;
        if (DropDownListCategory.SelectedValue != "A")
        {
            if (TextYear.Text != "")
            {
                SqlDataSourceJournal.SelectParameters.Clear();
                SqlDataSourceJournal.SelectParameters.Add("JournalCategory", DropDownListCategory.SelectedValue);
                SqlDataSourceJournal.SelectParameters.Add("Year", TextYear.Text.Trim());

                SqlDataSourceJournal.SelectCommand = "select k.Id as ISSN,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,Comments,Year from Journal_M k   LEFT join Journal_IF_Details j on j.id=k.Id  WHERE JournalCategory=@JournalCategory and Year= @Year";
            }
            else
            {
                SqlDataSourceJournal.SelectParameters.Clear();
                SqlDataSourceJournal.SelectParameters.Add("JournalCategory", DropDownListCategory.SelectedValue);
                SqlDataSourceJournal.SelectCommand = "select k.Id as ISSN,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,Comments,Year from Journal_M k   LEFT join Journal_IF_Details j on j.id=k.Id  WHERE JournalCategory=@JournalCategory";
            }

        }
        else
        {
            if (TextYear.Text != "")
            {
                SqlDataSourceJournal.SelectParameters.Clear();
                SqlDataSourceJournal.SelectParameters.Add("Year", TextYear.Text.Trim());
                SqlDataSourceJournal.SelectCommand = "select k.Id as ISSN,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,Comments,Year from Journal_M k   LEFT join Journal_IF_Details j on j.id=k.Id WHERE Year=@Year";
            }
            else
            {
                SqlDataSourceJournal.SelectCommand = "select k.Id as ISSN,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,Comments,Year from Journal_M k   LEFT join Journal_IF_Details j on j.id=k.Id  ";
            }


        }
        SqlDataSourceJournal.DataBind();
        GridViewJournalSearch.DataBind();

    }
}