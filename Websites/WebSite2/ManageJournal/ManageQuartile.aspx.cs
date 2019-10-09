using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using System.Collections;
using System.Configuration;

public partial class ManageJournal_ManageQuartile : System.Web.UI.Page
{
    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    Business B = new Business();
    Journal_DataObject JournalDataObj = new Journal_DataObject();
    JournalData JournalValueObj = new JournalData();
    string PublicationYearwebConfig = ConfigurationManager.AppSettings["PublicationYear"];

    protected void Page_Load(object sender, EventArgs e)
    {
        popupPanelJournal.Visible = true;
        if (!IsPostBack)
        {
            setModalWindow(sender, e);
          
            // txtboxImpactfactor.Enabled = false;
            // txtboxFiveYearImpactFactor.Enabled = false;
            //   txtboxComments.Enabled = false;
            //  txtboxYear.Enabled = false;
            // txtboxTitle.Enabled = false;
            // txtboxAbrivatedTitle.Enabled = false;

            //bindStatusDropDownList();
            DropDownQuartile.DataSourceID = "SqlDataSourceQuartileM";
            DropDownQuartile.DataValueField = "Id";
            DropDownQuartile.DataTextField = "Name";
            DropDownQuartile.DataBind();
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
                TextBoxYearJAQ.Items.Add(new ListItem(yeatAppend.ToString(), yeatAppend.ToString(), true));
            }
            TextBoxYearJAQ.DataBind();
            TextBoxYearJAQ.SelectedValue = currenntYear.ToString();
            string userid = Session["UserId"].ToString();
        }
      
        //PnlActiveyear.Visible = true;


      
    }
    // protected void bindStatusDropDownList()
    //{


    //    DropDownQuartile.DataSourceID = "SqlDataSourceQuartileM";
    //    DropDownQuartile.DataValueField = "Id";
    //    DropDownQuartile.DataTextField = "Name";
    //    DropDownQuartile.DataBind();
    //}
    protected void setModalWindow(object sender, EventArgs e)
    {
        popupPanelJournal.Visible = true;
        popGridJournal.DataSourceID = "SqlDataSourceJournal";
        SqlDataSourceJournal.DataBind();
        popGridJournal.DataBind();

    }
    protected void btnSaveUpdate_Click(object sender, EventArgs e)
    {


        if (!Page.IsValid)
        {
            return;
        }
        try
        {
            PublishData ob = new PublishData();
            Business b=new Business();

            string CreatedBy = Session["UserId"].ToString();
            string ISSN = txtboxID.Text;
            string QJYear = TextBoxYearJAQ.SelectedValue;
            //string QValue = DropDownQuartile.SelectedValue.ToString();
            string QValue1 = DropDownQuartile.SelectedItem.Value;
            string QValue2 = DropDownQuartile.SelectedItem.Text;
            if (QValue2 == "-Select-")
            { 
                 string CloseWindow1 = "alert('Please select the Quartile Value')";
                 ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow1, true);
                 return;
            }
            //string QJYear=
           
           ob.QYear=Convert.ToUInt16(QJYear);
           ob.PublisherOfJournal = ISSN;
           string QId = QValue1;
           int result = 0;
           ob = b.checkJournalLinkM(ob.PublisherOfJournal, ob.QYear);
           string qr = ob.Jquartile;
           if ((ob.Jquartile) == null)
           {
               result = b.InsertJournalQuartileMap(ISSN, Convert.ToInt16(QJYear), QId, CreatedBy);
           ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Quartile Value saved Succesfully')</script>");
           popupPanelJournal.Visible = false;
           return;
           }
           if (result == 0)
           {
               
           if (ob.Jquartile == Convert.ToString(QId))
               {
                   //ob = b.InsertJournalQuartileTracker(ISSN, Convert.ToUInt16(QJYear), ob.Jquartile, QId, Session["UserId"]);
                   ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please select the Quartile Value')</script>");
                   popupPanelJournal.Visible = false;
                   return;
               }
           else if (ob.Jquartile != Convert.ToString(QId))
               {
                   ob = b.InsertJournalQuartileTracker(ISSN, Convert.ToUInt16(QJYear), ob.Jquartile, QId, Session["UserId"]);
                   ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Quartile Value Updated Succesfully')</script>");
                   popupPanelJournal.Visible = false;
                   return;
               }
           }




     
        }
        catch (Exception ex)
        {
            log.Error("Inside Catch Block Of Manage Journal" + ex.Message);
            log.Error(ex.StackTrace);
            ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Error!!!!!!!!!')</script>");

        }
    }
    

 

    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtboxTitle.Enabled = true;
    

        //txtboxYear.Text = "";
        txtboxTitle.Text = "";
       
        txtboxID.Text = "";
       
        //txtyear.Text = "";
        txtquartileID.Text = "";
        //cblActiveyear.ClearSelection();
        btnSaveUpdate.Enabled = true;

       
        DropDownQuartile.Items.Clear();
        DropDownQuartile.DataSourceID = "SqlDataSourceQuartileM";
        DropDownQuartile.DataBind();
        DropDownQuartile.SelectedItem.Text = "-Select-";
    }

protected void JournalIDTextChanged(object sender, EventArgs e)
    {
        ArrayList list = new ArrayList();
       
        //txtboxYear.Text = "";
        txtboxTitle.Text = "";
    
        //cblActiveyear.ClearSelection();

        popupPanelJournal.Visible = false;
        JournalValueObj.JournalID = txtboxID.Text;
      
        // JournalValueObj.year = txtBoxYear.Text;

        JournalData j = new JournalData();
        j = B.JournalEntryCheckExistance(JournalValueObj);
        if (j.jid != null)
        {
            log.Debug("inside --JournalIDTextChanged--update publish" + j.jid);
            txtboxTitle.Text = j.name;
            string year = DateTime.Now.Year.ToString();
            int Jyear = Convert.ToInt32(year) - 1;
           
            list = B.SelectActiveYear(JournalValueObj);

            //for (int i = 0; i < list.Count; i++)
            //{

            //    //ListItem currentCheckBox = cblActiveyear.Items.FindByValue(list[i].ToString());
            //    //if (currentCheckBox != null)
            //    //{
            //    //    currentCheckBox.Selected = true;
            //    //}
             
            //}
            PublishData obj = new PublishData();
           string jyear= TextBoxYearJAQ.SelectedItem.Text;
           obj = B.checkQuartileValue(txtboxID.Text, jyear);
          
          txtquartileID.Text =Convert.ToString( obj.Jquartile);
          IncentiveBusiness ince_obj = new IncentiveBusiness();
          obj = ince_obj.getquartileName(txtquartileID.Text);
            //txtquartile.Text = obj.Name;
            //DropDownQuartile.DataSourceID = "SqlDataSourceQuartileM";
            //DropDownQuartile.DataTextField = "Name";
            //DropDownQuartile.DataValueField = "Id";
            //DropDownQuartile.DataBind();
            DropDownQuartile.Items.Clear();
            DropDownQuartile.Items.Add(new ListItem("-Select-", "0", true));
            DropDownQuartile.DataSourceID = "SqlDataSourceQuartileM";
            DropDownQuartile.DataBind();
            if (txtquartileID.Text != "")
            {
                DropDownQuartile.SelectedValue = txtquartileID.Text;
            }

          
          //DropDownQuartile.SelectedItem.Value = txtquartileID.Text;




        }
        else
        {
            log.Debug("inside --JournalIDTextChanged--New Publish Id" + j.jid);
            btnSaveUpdate.Enabled = true;
            ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('New Entry..Please enter the details !!!!')</script>");
            txtboxTitle.Enabled = true;
        }


    } 


    //protected void txtboxYear_TextChanged(object sender, EventArgs e)
    //{
    //    btnSaveUpdate.Enabled = true;
    //    JournalValueObj.year = txtboxYear.Text;
    //    JournalValueObj.JournalID = txtboxID.Text;

    //    JournalData j = new JournalData();


    //    // get Impact factor
    //    if (txtboxYear.Text != "")
    //    {

    //        j = B.JournalGetImpactFactor(JournalValueObj);
    //        if (j.jid != null)
    //        {

    //            log.Debug("inside--txtboxYear_TextChanged--" + j.jid);
    //            // impact farcotr entry exists
    //            // ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('IF entry Exists')</script>");
                
    //            txtboxYear.Enabled = true;
    //            txtboxTitle.Enabled = false;
          

    //        }
    //        else
    //        {


    //            log.Debug("inside--txtboxYear_TextChanged--" + j.jid);
    //            // Not mentioned
             
    //            //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Entry does not Exists')</script>");


    //        }
    //    }
    //}


    protected void popSelected(Object sender, EventArgs e)
    {



        popGridJournal.Visible = true;
        GridViewRow row = popGridJournal.SelectedRow;

        string Journalid = row.Cells[1].Text;


        txtboxID.Text = Journalid;

        // journalcodeSrch.Text = "";
        popGridJournal.DataBind();
        JournalIDTextChanged(sender, e);

        string year = DateTime.Now.Year.ToString();
        int Jyear = Convert.ToInt32(year) - 1;
       

        journalcodeSrch.Text = "";

        popGridJournal.DataBind();
    }


    protected void showPop(object sender, EventArgs e)
    {
        ModalPopupExtender1.Show();
    }
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

        ModalPopupExtender1.Show();
    }
    protected void exit(Object sender, EventArgs e)
    {
        journalcodeSrch.Text = "";
        popGridJournal.DataBind();
    }

    protected void txtActiveyear_TextChanged(object sender, EventArgs e)
    {
        // PnlActiveyear.Visible = true;
    }

    protected void DropDownQuartile_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void TextBoxYearJAQ_SelectedIndexChanged(object sender, EventArgs e)
    {
        PublishData c = new PublishData();

     
        if (TextBoxYearJAQ.SelectedValue != "")
        {
            c = B.CheckQuartilevaluefromJQM(TextBoxYearJAQ.SelectedValue, txtboxID.Text);
            string Jquartilevalue=Convert.ToString( c.Jquartile);
            IncentiveBusiness ince_obj = new IncentiveBusiness();
            PublishData obj = new PublishData();
            obj = ince_obj.getquartileName(Jquartilevalue);
            DropDownQuartile.SelectedValue = c.Jquartile;
           // DropDownQuartile.SelectedItem.Text = obj.Name;
            if (obj.Name == null)
            {
                DropDownQuartile.Items.Clear();
                DropDownQuartile.Items.Add(new ListItem("-Select-", "0", true));
                DropDownQuartile.DataSourceID = "SqlDataSourceQuartileM";
                DropDownQuartile.DataBind();
            }
            //if (txtquartileID.Text != "")
            //{
            //    DropDownQuartile.SelectedValue = txtquartileID.Text;
            //}
            //DropDownQuartile.SelectedItem.Value = Jquartilevalue;
            //DropDownQuartile.DataBind();
        }
    }
}