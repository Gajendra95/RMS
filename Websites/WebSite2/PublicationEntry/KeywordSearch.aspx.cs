using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PublicationEntry_KeywordSearch : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        Business obj = new Business();
        DataSet ds = new DataSet();
        string keyword = TextBox1.Text.Trim();
        Session["Keywords"] = TextBox1.Text;
        ds = obj.SelecKeywordBasedAuthors(keyword);
        GridView1.DataSource = ds.Tables[0];
        GridView1.DataBind();
        setModalWindow(sender, e);

    }

    protected void setModalWindow(object sender, EventArgs e)
    {

        popup.Visible = true;
        popupStudentGrid.DataSourceID = "StudentSQLDS";
        StudentSQLDS.DataBind();
        popupStudentGrid.DataBind();
    }

    protected void onclickHyperlinkRollNumber(object sender, EventArgs e)
    {
        setModalWindow(sender, e);
        popup.Visible = true;
        ModalPopupExtender1.Show();
        int Index = ((GridViewRow)((sender as Control)).NamingContainer).RowIndex;
        HiddenField field = GridView1.Rows[Index].FindControl("EmployeeCode") as HiddenField;
        Label author = GridView1.Rows[Index].FindControl("AuthorName") as Label;
        Session["KeywordAuthor"] = author.Text;
        Session["Employeecode"] = field.Value;
        Session["Keywords"] = TextBox1.Text;

        txtauthorname.Text = Session["KeywordAuthor"].ToString();
        txtEmployeeCode.Text = Session["Employeecode"].ToString();

    }

    protected void GridViewSearchPub_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        BtnSearch_Click(sender, e);
        GridView1.PageIndex = e.NewPageIndex;
        GridView1.DataBind();
    }

    protected void grdCustomPagging_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName == "ShowPopup")
        {
            popup.Visible = true;
        }
    }
    protected void ViewPDF(Object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow row = (GridViewRow)lb.NamingContainer;
        int index = row.RowIndex; //gets the row index selected      
        var lblPubID = row.FindControl("lblPubID") as Label;
        string id = lblPubID.Text;
        string servername = ConfigurationManager.AppSettings["ServerName"].ToString();
        string domainame = ConfigurationManager.AppSettings["DomainName"].ToString();
        string username = ConfigurationManager.AppSettings["UserName"].ToString();
        string password = ConfigurationManager.AppSettings["Password"].ToString();
        string folderpath;
        string path_BoxId;
        using (NetworkShareAccesser.Access(servername, domainame, username, password))
        {

            folderpath = ConfigurationManager.AppSettings["FolderPath"].ToString();
            path_BoxId = Path.Combine(folderpath, id);

            int id1 = popupStudentGrid.SelectedIndex;
            Label filepath = (Label)popupStudentGrid.Rows[index].FindControl("lblgetid");
            string path = filepath.Text;       //actual filelocation path  
            string newpath = path.Replace('\\', '/');  // replacing '\' by '/' to view image or pdf

            Session["path"] = newpath;
            Response.Write("<script>");
            Response.Write("window.open('DisplayPdf.aspx?val=" + newpath + "','_blank')");
            //Response.Redirect("DisplayPdf.aspx?val=" + newpath + "','_blank')");
            Response.Write("</script>");

        }

    }
    protected void Redirect(Object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow row = (GridViewRow)lb.NamingContainer;
        int index = row.RowIndex; //gets the row index selected      
        var lblPubID = row.FindControl("lblPubID") as Label;
        string id = lblPubID.Text;
        Response.Redirect("PublicationView.aspx?PubID=" + id + "&PubType=" + "JA" + "&Keyword=" + "true");

    }
    protected void RedirectProject(Object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow row = (GridViewRow)lb.NamingContainer;
        int index = row.RowIndex; //gets the row index selected      
        var lblProjectID = row.FindControl("lblProjectID") as Label;
        var lblProjectUnit = row.FindControl("lblProjectUnit") as Label;
        string id = lblProjectID.Text;
        string projectunit = lblProjectUnit.Text;
        Response.Redirect("~/GrantEntry/GrantEntryView.aspx?ProjectID=" + id + "&ProjectUnit=" + projectunit + "&Keyword=" + "true");

    }

    public string Highlight(string InputTxt)
    {


        if (TextBox1.Text != null)
        {
            string strSearch = TextBox1.Text;
            Regex RegExp = new Regex(strSearch.Replace(" ", "|").Trim(), RegexOptions.IgnoreCase);
            return
            RegExp.Replace(InputTxt, new MatchEvaluator(ReplaceKeyWords));
        }
        else
            return InputTxt;


    }

    public string ReplaceKeyWords(Match m)
    {
        return "<span class=highlight>" + m.Value + "</span>";
    }

    protected void Select(Object sender, EventArgs e)
    {
        int intId = 100;
        string strPopup = "<script language='javascript' ID='script1'>"
    + "window.open('PublicationView.aspx?data=" + HttpUtility.UrlEncode(intId.ToString())

    + "','new window', 'top=90, left=200, width=1000, height=500, dependant=no, location=0, alwaysRaised=no, menubar=no, resizeable=no, scrollbars=n, toolbar=no, status=no, center=yes')"

    + "</script>";

        ScriptManager.RegisterStartupScript((Page)HttpContext.Current.Handler, typeof(Page), "Script1", strPopup, false);
    }

    protected void exit(object sender, EventArgs e)
    {

    }
}