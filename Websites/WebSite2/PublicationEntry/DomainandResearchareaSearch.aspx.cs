using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PublicationEntry_DomainandResearchareaSearch : System.Web.UI.Page
{

    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string keywordback = Request.QueryString["Keywordback"];
            if (keywordback == "back")
             
            {
                 setModalWindow(sender, e);
                 popup.Visible = true;
                 ModalPopupExtender1.Show();
                 //int Index = ((GridViewRow)((sender as Control)).NamingContainer).RowIndex;
                 //HiddenField field = GridView1.Rows[0].FindControl("EmployeeCode") as HiddenField;
                 //Label author = GridView1.Rows[0].FindControl("AuthorName") as Label;
                 //Session["KeywordAuthor"] = author.Text;
                 //Session["Employeecode"] = field.Value;
                 ////Session["Keywords"] = TextBox1.Text;

                 //txtauthorname.Text = Session["KeywordAuthor"].ToString();
                 //txtEmployeeCode.Text = Session["Employeecode"].ToString();
                 Session["Employeecode"] = Session["EmployCodeForSessionParameter"].ToString();
                 txtauthorname.Text = Session["KeywordAuthor"].ToString();
                 txtEmployeeCode.Text = Session["Employeecode"].ToString();
                 txtOrcid.Text = Session["ORCID"].ToString();
                 txtScopusid.Text = Session["ScopusID"].ToString();
                 txtScopusid2.Text=Session["ScopusID2"].ToString();
                 txtScopusid3.Text = Session["ScopusID3"].ToString();
                 textDomain.Text = Session["sessiondomain"].ToString();
                 textArea.Text=Session["sessionarea"].ToString();
                

             }
        }
    }
    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        Business obj = new Business();
        DataSet ds = new DataSet();
        string domain = txtDomain.Text.Trim();
        Session["Domain"] = domain;
        string area = txtResearchArea.Text.Trim();
        Session["Area"] = area;
        ds = obj.SelecDomainandResearchArea(domain,area);
        GridView1.DataSource = ds.Tables[0];
        GridView1.DataBind();
        setModalWindow(sender, e);
        if (txtDomain.Text == "" && txtResearchArea.Text == "")
        {
            string CloseWindow1 = "alert('Please enter atleast one field')";
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow1, true);
            return;

        }

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
        Business b = new Business();
        FileUpload f = new FileUpload();
     
        setModalWindow(sender, e);
        popup.Visible = true;
        ModalPopupExtender1.Show();
        int Index = ((GridViewRow)((sender as Control)).NamingContainer).RowIndex;
        HiddenField field = GridView1.Rows[Index].FindControl("EmployeeCode") as HiddenField;
        Label author = GridView1.Rows[Index].FindControl("AuthorName") as Label;
        Label orcid = GridView1.Rows[Index].FindControl("ORCID") as Label;
        Label scopusid = GridView1.Rows[Index].FindControl("ScopusID") as Label;
        Label scopusid2 = GridView1.Rows[Index].FindControl("ScopusID2") as Label;
        Label scopusid3 = GridView1.Rows[Index].FindControl("ScopusID3") as Label;
        f = b.DomainSearch(field.Value);

        string[] domain_value = (f.Domain.Split(':'));

        for (int k = 0; k <= domain_value.GetUpperBound(0); k++)
        {
            if (domain_value[k] != "")
            {

                if (k == 0)
                {
                    textDomain.Text = domain_value[k];
                    
                }
                else
                {
                    textDomain.Text = textDomain.Text + ":" + domain_value[k];
                   
                }
                   
            }
        }
        Session["sessiondomain"] = textDomain.Text;

        string[] area_value = (f.Area.Split(':'));

        for (int m = 0; m <= area_value.GetUpperBound(0); m++)
        {
            if (area_value[m] != "")
            {

                if (m == 0)
                {
                    textArea.Text = area_value[m];
                }
                else
                {
                    textArea.Text = textArea.Text + ":" + area_value[m];
                }
              
            }
        }
        Session["sessionarea"] = textArea.Text;
        Session["KeywordAuthor"] = author.Text;
        Session["Employeecode"] = field.Value;
        Session["ORCID"] = orcid.Text;
        Session["ScopusID"] = scopusid.Text;
        Session["ScopusID2"] = scopusid2.Text;
        Session["ScopusID3"] = scopusid3.Text;
        Session["EmployCodeForSessionParameter"] = field.Value;
       
        txtauthorname.Text = Session["KeywordAuthor"].ToString();
        txtEmployeeCode.Text = Session["Employeecode"].ToString();
        txtOrcid.Text = Session["ORCID"].ToString();
        txtScopusid.Text = Session["ScopusID"].ToString();
        txtScopusid2.Text = Session["ScopusID2"].ToString();
        txtScopusid3.Text = Session["ScopusID3"].ToString();

    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        //BtnSearch_Click(sender, e);
        Business obj = new Business();
        DataSet ds = new DataSet();
        string domain = txtDomain.Text.Trim();
        //Session["Domain"] = domain;
        string area = txtResearchArea.Text.Trim();
        //Session["Area"] = area;
        ds = obj.SelecDomainandResearchArea(Session["Domain"].ToString(), Session["Area"].ToString());
        GridView1.DataSource = ds.Tables[0];
        //GridView1.DataBind();
        //setModalWindow(sender, e);
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
        Response.Redirect("PublicationView.aspx?PubID=" + id + "&PubType=" + "JA" + "&Keyword=" + "isPagesrch");

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
        Response.Redirect("~/GrantEntry/GrantEntryView.aspx?ProjectID=" + id + "&ProjectUnit=" + projectunit + "&Keyword=" + "isPagesrch");

    }

    //protected void Redirect(Object sender, EventArgs e)
    //{
       
    //    int Index = ((GridViewRow)((sender as Control)).NamingContainer).RowIndex;
    //    HiddenField field = GridView1.Rows[Index].FindControl("EmployeeCode") as HiddenField;
               
    //    Response.Redirect("~/PublicationEntry/EditResearchData.aspx?UserId=" + field.Value  + "&IsPagesearch=" + "true");


    //}
    protected void Button3_Click(object sender, EventArgs e)
    {
        Business obj = new Business();
        DataSet ds = new DataSet();
        string domain = Session["Domain"].ToString();
       
        txtDomain.Text = domain;
        string area = Session["Area"].ToString();
        txtResearchArea.Text = area;
        ds = obj.SelecDomainandResearchArea(domain, area);
        GridView1.DataSource = ds.Tables[0];
        GridView1.DataBind();
        setModalWindow(sender, e);
    }
}