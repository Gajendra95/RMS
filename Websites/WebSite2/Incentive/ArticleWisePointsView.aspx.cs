using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Incentive_ArticleWisePointsView : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Btnsearch_Click(object sender, EventArgs e)
    {
        PublishData v = new PublishData();
        string Pid = txtPublicationId.Text.Trim();
        Business obj = new Business();
        IncentiveBusiness ince_obj = new IncentiveBusiness();
        string TypeEntry="JA";
        v = obj.fnfindjid(Pid, TypeEntry);

        TextBoxPubId.Text = Pid;
        txtboxTitleOfWrkItem.Text = v.TitleWorkItem;
        TextBoxPubJournal.Text = v.PublisherOfJournal;
        TextBoxNameJournal.Text = v.PublisherOfJournalName;
        DropDownListMuCategory.SelectedValue = v.MUCategorization;
        DropDownListMonthJA.SelectedValue = v.PublishJAMonth.ToString();
        string PublicationYearwebConfig = ConfigurationManager.AppSettings["PublicationYear"];
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
        int pubyear = Convert.ToInt32(TextBoxYearJA.SelectedValue);
        int pubmonth = 0;

        pubmonth = Convert.ToInt32(DropDownListMonthJA.SelectedValue);
        if (pubyear >= 2018 && pubmonth >= 7)
        {
            string quartileid =v.QuartileOnIncentivize;
            lblQuartile.Visible = true;
            txtquartile.Visible = true;
            txtquartileid.Text = quartileid.ToString();
            //Session["Quartile"] = txtquartileid.Text;
            //txtquartileid.Text = Session["Quartile"].ToString();
            PublishData v2 = new PublishData();
            if (txtquartileid.Text != "" && txtquartileid.Text != "0")
            {
                v2 = ince_obj.getquartileName(txtquartileid.Text);
                txtquartile.Text = v2.Name;
                if (Convert.ToInt16(v.PublishJAYear) >= 2018 && Convert.ToInt16(v.PublishJAMonth) >= 7)
                {
                    v.ImpactFactor = txtquartileid.Text;
                }
            }
        }
        else 
        {
            lblQuartile.Visible = false;
            txtquartile.Visible = false;
        }
        if (v.IncentivePointSatatus == "CAN" && v.Status == "CAN")
        {
            PanelCancel.Visible = true;
            txtcancelRemarks.Text = v.PubCancelRemarks;
            Gridview.Visible = true;
            Gridview.DataSourceID = "SqlDataSource3";
            Gridview.DataBind();
            if (Gridview.Rows.Count == 0)
            {
            }
        }
        else
        {
            PanelCancel.Visible = false;
            Gridview.Visible = true;
            Gridview.DataSourceID = "SqlDataSource2";
            Gridview.DataBind();
            if (Gridview.Rows.Count == 0)
            {
            }
        }     
              
        TextBoxImpFact.Text = v.ImpactFactor;
        TextBoxImpFact5.Text = v.ImpactFactor5;
        if (v.IFApplicableYear != 0)
        {
            txtIFApplicableYear.Text = v.IFApplicableYear.ToString();
        }
        TextBoxPageFrom.Text = v.PageFrom;
        TextBoxPageTo.Text = v.PageTo;
        TextBoxVolume.Text = v.Volume;
        TextBoxIssue.Text = v.Issue;
        DropDownListPubType.SelectedValue = v.Publicationtype;

        //Gridview.Visible = true;
        //Gridview.DataSourceID = "SqlDataSource2";
        //Gridview.DataBind();
        //if (Gridview.Rows.Count==0)
        //{
        //}
    }
    protected void radioincentive_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (radioincentive.SelectedValue == "1")
        {
            Panel2.Visible = true;
            PnlPublicationDetails.Visible = true;
            Panel1.Visible = true;
            Panel4.Visible = false;
            PanelPatent.Visible = false;
            Panel5.Visible = false;
        }
        else
        {
            Panel2.Visible = false;
            PnlPublicationDetails.Visible = false;
            Panel1.Visible = false;
            Panel4.Visible = true;
            PanelPatent.Visible = true;
            Panel5.Visible = true;
        }
    }
    protected void BtnsearchPatent_Click(object sender, EventArgs e)
    {
        Patent d = new Patent();
        string Pid = txtpatentID.Text.Trim();
        PatentBusiness obj = new PatentBusiness();
        d = obj.fnfindpatent(Pid);
        txtID.Text = Pid;
        txtTitle.Text = d.Title;
        txtdescription.Text = d.description;
        txtfilingoffice.Text = d.Filing_Office;
        if (txtfilingoffice.Text == "C")
        {
            txtfilingoffice.Text = "Chennai";
        }
        else if (txtfilingoffice.Text == "D")
        {
            txtfilingoffice.Text = "Delhi";
        }
        else if (txtfilingoffice.Text == "K")
        {
            txtfilingoffice.Text = "Kolkata";
        }
        else if (txtfilingoffice.Text == "M")
        {
            txtfilingoffice.Text = "Mumbai";
        }
        if (d.Grant_Date !=null)
        {
            txtgrantdate.Text = d.Grant_Date.ToShortDateString();
        }
      
        txtPatentno.Text = d.Patent_Number;
        Gridview1.Visible = true;
        Gridview1.DataSourceID = "SqlDataSource6";
        Gridview1.DataBind();
        if (Gridview.Rows.Count == 0)
        {
        }
    }
}