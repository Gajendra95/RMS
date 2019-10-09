using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using System.Collections;

public partial class ManageJournal : System.Web.UI.Page
{
    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    Business B = new Business();
    Journal_DataObject JournalDataObj = new Journal_DataObject();
    JournalData JournalValueObj = new JournalData();

    protected void Page_Load(object sender, EventArgs e)
    {
        popupPanelJournal.Visible = true;
        if (!IsPostBack)
        {
            ArrayList PageRollList = new ArrayList();
            PageRollList.Add("2");
            PageRollList.Add("8");
            PageRollList.Add("9");
            string userrole = Session["Role"].ToString();
            if (PageRollList.Contains(userrole))
            {

            }
            else
            {
                string unacces = "Unauthorized Acces!!! Login Again";
                Response.Redirect("~/Login.aspx?val=" + unacces);
            }
            setModalWindow(sender, e);
            PanelView.Visible = false;
            GridViewIndex.Visible = false;

            // txtboxImpactfactor.Enabled = false;
            // txtboxFiveYearImpactFactor.Enabled = false;
            //   txtboxComments.Enabled = false;
            //  txtboxYear.Enabled = false;
            // txtboxTitle.Enabled = false;
            // txtboxAbrivatedTitle.Enabled = false;

        }
        //else
        //{
        PnlActiveyear.Visible = true;

        //}
    }
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



        //check if update oe edit.
        try
        {
            ArrayList list = new ArrayList();
            JournalValueObj.JournalID = txtboxID.Text;
            //JournalValueObj.year = txtboxYear.Text;

            // JournalValueObj.Category = dropdownCategory.SelectedValue;


            int saveOrUpdate = B.IFcheckSaveOrUpdate(JournalValueObj);

            //if (saveOrUpdate == 2) // SAVE
            //{
            //    JournalValueObj.Category = dropdownCategory.SelectedValue;
            //    JournalValueObj.JournalID = txtboxID.Text.Trim();

            //    popupPanelJournal.Visible = false;
            //    if (txtboxTitle.Text == "")
            //    {
            //        ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Enter the Title')</script>");
            //        return;
            //    }
            //    if (txtboxAbrivatedTitle.Text == "")
            //    {
            //        ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Enter the Abbreviated Title')</script>");
            //        return;
            //    }
            //    //if (dropdownCategory.SelectedValue == "")
            //    //{
            //    //    ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Enter the Category')</script>");
            //    //    return;
            //    //}
            //    //if (txtboxYear.Text != "")
            //    //{
            //    //    //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Enter the Year')</script>");
            //    //    //return;
            //    //    if (txtboxImpactfactor.Text == "")
            //    //    {
            //    //        ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Enter the Impact Factor')</script>");
            //    //        return;
            //    //    }
            //    //    if (txtboxFiveYearImpactFactor.Text == "")
            //    //    {
            //    //        ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Enter the Five Year Impact Factor')</script>");
            //    //        return;
            //    //    }
            //    //}
            //    //if (txtboxImpactfactor.Text == "")
            //    //{
            //    //    ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Enter the Impact Factor')</script>");
            //    //    return;
            //    //}
            //    //if (txtboxFiveYearImpactFactor.Text == "")
            //    //{
            //    //    ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Enter the Five Year Impact Factor')</script>");
            //    //    return;
            //    //}
            //    //JournalValueObj.year = txtboxYear.Text.Trim();
            //    JournalValueObj.Comments = txtboxComments.Text.ToString();
            //    //int year1 = Convert.ToInt32(txtboxYear.Text) - 1;
            //    //JournalValueObj.year = year1.ToString();
            //    //if (txtboxFiveYearImpactFactor.Text != "")
            //    //{
            //    //    JournalValueObj.fiveimpcrfact = Convert.ToDouble(txtboxFiveYearImpactFactor.Text.Trim());
            //    //}
            //    //if (txtboxImpactfactor.Text != "")
            //    //{
            //    //    JournalValueObj.impctfact = Convert.ToDouble(txtboxImpactfactor.Text.Trim());
            //    //}
            //    JournalValueObj.Title = txtboxTitle.Text.Trim();
            //    JournalValueObj.AbbTitle = txtboxAbrivatedTitle.Text.Trim();
            //    int applicableYear = Convert.ToInt32(JournalValueObj.year) + 1;
            //    string applicableYear1 = applicableYear.ToString();
            //    //JournalValueObj.ApplicableYear =applicableYear+06+01;
            //    string Applicable1 = applicableYear1 + "-" + "06" + "-01";
            //    JournalValueObj.ApplicableYear = Applicable1;

            //    //int applicableYear2 = Convert.ToInt32(JournalValueObj.year) -1 ;
            //    //string applicableYear3 = applicableYear2.ToString();
            //    //JournalValueObj.Year1 = applicableYear3;
            //    //string Applicable2 = year1 + "-" + "06" + "-01";
            //    //JournalValueObj.ApplicableYear1 = Applicable2;

            //    //DateTime Applicable=Convert.  Applicable1
            //    for (int i = 0; i < cblActiveyear.Items.Count; i++)
            //    {
            //        if (cblActiveyear.Items[i].Selected == true)
            //        {
            //            JournalValueObj.ActiveYear = Convert.ToInt32(cblActiveyear.Items[i].Text.ToString());
            //            list.Add(JournalValueObj.ActiveYear);
            //        }

            //    }
            //    if (list.Count == 0)
            //    {
            //        ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please select the Active year')</script>");
            //        return;
            //    }

            //    int res = B.JournalEntrySaveChanges(JournalValueObj, list);
            //    if (res == 1)
            //    {
            //        ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Saved Successfully')</script>");
            //        //log.Info("Saved Successfully , id: " + txtboxID.Text.Trim() + ", year: " + txtboxYear.Text.Trim());


            //    }
            //    else
            //    {
            //        ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Could Not Be Saved Successfully')</script>");
            //        //log.Info("Could Not Be Saved Successfully , id: " + txtboxID.Text.Trim() + ", year: " + txtboxYear.Text.Trim());
            //    }

            //    btnSaveUpdate.Enabled = false;
            //}
            //else if (saveOrUpdate == 1)
            //{

            //    //update
            //    JournalValueObj.Category = dropdownCategory.SelectedValue;
            //    JournalValueObj.JournalID = txtboxID.Text.Trim();

            //    popupPanelJournal.Visible = false;
            //    //if (txtboxYear.Text == "")
            //    //{
            //    //    ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Enter the Year')</script>");
            //    //    return;
            //    //}
            //    //if (txtboxImpactfactor.Text == "")
            //    //{
            //    //    ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Enter the Impact Factor')</script>");
            //    //    return;
            //    //}
            //    //if (txtboxFiveYearImpactFactor.Text == "")
            //    //{
            //    //    ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Enter the Five Year Impact Factor')</script>");
            //    //    return;
            //    //}
            //    //JournalValueObj.year = txtboxYear.Text.Trim();
            //    JournalValueObj.Comments = txtboxComments.Text.ToString();
            //    //if (txtboxFiveYearImpactFactor.Text != "")
            //    //{
            //    //    JournalValueObj.fiveimpcrfact = Convert.ToDouble(txtboxFiveYearImpactFactor.Text.Trim());
            //    //}
            //    //if (txtboxImpactfactor.Text != "")
            //    //{
            //    //    JournalValueObj.impctfact = Convert.ToDouble(txtboxImpactfactor.Text.Trim());
            //    //}
            //    JournalValueObj.Title = txtboxTitle.Text.Trim();
            //    JournalValueObj.AbbTitle = txtboxAbrivatedTitle.Text.Trim();
            //    for (int i = 0; i < cblActiveyear.Items.Count; i++)
            //    {
            //        if (cblActiveyear.Items[i].Selected == true)
            //        {
            //            JournalValueObj.ActiveYear = Convert.ToInt32(cblActiveyear.Items[i].Text.ToString());
            //            list.Add(JournalValueObj.ActiveYear);
            //        }

            //    }
            //    //int year1 = Convert.ToInt32(txtboxYear.Text) - 1;
            //    //JournalValueObj.year = year1.ToString();
            //    JournalValueObj.Title = txtboxTitle.Text.Trim();
            //    JournalValueObj.AbbTitle = txtboxAbrivatedTitle.Text.Trim();
            //    int applicableYear = Convert.ToInt32(JournalValueObj.year) + 1;
            //    string applicableYear1 = applicableYear.ToString();
            //    //JournalValueObj.ApplicableYear =applicableYear+06+01;
            //    string Applicable1 = applicableYear1 + "-" + "06" + "-01";
            //    JournalValueObj.ApplicableYear = Applicable1;
            //    int res = B.JournalEntryUpdateChanges(JournalValueObj, list);

            //    if (res == 1)
            //    {
            //        ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Updated Successfully')</script>");
            //        //log.Info("Updated Successfully , id: " + txtboxID.Text.Trim() + ", year: " + txtboxYear.Text.Trim());

            //    }
            //    else
            //    {
            //        ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Could Not Be Updated Successfully')</script>");
            //        //log.Info("Could Not Be Updated Successfully , id: " + txtboxID.Text.Trim() + ", year: " + txtboxYear.Text.Trim());
            //    }

            //    btnSaveUpdate.Enabled = false;
            //}

           if (saveOrUpdate == 3)
            {
                //update
                JournalValueObj.Category = dropdownCategory.SelectedValue;
                JournalValueObj.JournalID = txtboxID.Text.Trim();
                popupPanelJournal.Visible = false;
                //if (txtboxYear.Text != "")
                //{
                //    //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Enter the Year')</script>");
                //    //return;
                //    if (txtboxImpactfactor.Text == "")
                //    {
                //        ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Enter the Impact Factor')</script>");
                //        return;
                //    }
                //    if (txtboxFiveYearImpactFactor.Text == "")
                //    {
                //        ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Enter the Five Year Impact Factor')</script>");
                //        return;
                //    }
                //}

                if (txtboxTitle.Text == "")
                {
                    ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Enter the Title')</script>");
                    return;
                }
                if (txtboxAbrivatedTitle.Text == "")
                {
                    ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Enter the Abbreviated Title')</script>");
                    return;
                }
                    //}
                    //JournalValueObj.year = txtboxYear.Text.Trim();
                    //JournalValueObj.Comments = txtboxComments.Text.ToString();
                    //if (txtboxFiveYearImpactFactor.Text != "")
                    //{
                    //    JournalValueObj.fiveimpcrfact = Convert.ToDouble(txtboxFiveYearImpactFactor.Text.Trim());
                    //}
                    //if (txtboxImpactfactor.Text != "")
                    //{
                    //    JournalValueObj.impctfact = Convert.ToDouble(txtboxImpactfactor.Text.Trim());
                    //}
                    JournalValueObj.Title = txtboxTitle.Text.Trim();
                    JournalValueObj.AbbTitle = txtboxAbrivatedTitle.Text.Trim();
                    for (int i = 0; i < cblActiveyear.Items.Count; i++)
                    {
                        if (cblActiveyear.Items[i].Selected == true)
                        {
                            JournalValueObj.ActiveYear = Convert.ToInt32(cblActiveyear.Items[i].Text.ToString());
                            list.Add(JournalValueObj.ActiveYear);
                        }
                        JournalValueObj.Title = txtboxTitle.Text.Trim();
                        JournalValueObj.AbbTitle = txtboxAbrivatedTitle.Text.Trim();
                        int applicableYear = Convert.ToInt32(JournalValueObj.year) + 1;
                        string applicableYear1 = applicableYear.ToString();
                        //JournalValueObj.ApplicableYear =applicableYear+06+01;
                        string Applicable1 = applicableYear1 + "-" + "06" + "-01";
                        JournalValueObj.ApplicableYear = Applicable1;
                    }
                    int res = B.JournalEntrySaveChanges1(JournalValueObj, list);

                    if (res == 1)
                    {
                        ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Updated Successfully')</script>");
                        //log.Info(" Updated Successfully , id: " + txtboxID.Text.Trim() + ", year: " + txtboxYear.Text.Trim());



                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Could Not Be Updated Successfully')</script>");
                        //log.Info("Could Not Be Updated Successfully , id: " + txtboxID.Text.Trim() + ", year: " + txtboxYear.Text.Trim());
                    }

                    btnSaveUpdate.Enabled = false;
                
        
            }
           else if (saveOrUpdate == 2)
           {
               JournalValueObj.Category = dropdownCategory.SelectedValue;
               JournalValueObj.JournalID = txtboxID.Text.Trim();

               popupPanelJournal.Visible = false;
               if (txtboxTitle.Text == "")
               {
                   ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Enter the Title')</script>");
                   return;
               }
               if (txtboxAbrivatedTitle.Text == "")
               {
                   ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Enter the Abbreviated Title')</script>");
                   return;
               }
               if (txtboxComments.Text == "")
               {
                   ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please Enter the Remarks')</script>");
                   return;
               }
               //if (dropdownCategory.SelectedValue == "")
               //{
               //    ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Enter the Category')</script>");
               //    return;
               //}
               //if (txtboxYear.Text != "")
               //{
               //    //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Enter the Year')</script>");
               //    //return;
               //    if (txtboxImpactfactor.Text == "")
               //    {
               //        ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Enter the Impact Factor')</script>");
               //        return;
               //    }
               //    if (txtboxFiveYearImpactFactor.Text == "")
               //    {
               //        ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Enter the Five Year Impact Factor')</script>");
               //        return;
               //    }
               //}
               //if (txtboxImpactfactor.Text == "")
               //{
               //    ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Enter the Impact Factor')</script>");
               //    return;
               //}
               //if (txtboxFiveYearImpactFactor.Text == "")
               //{
               //    ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Enter the Five Year Impact Factor')</script>");
               //    return;
               //}
               //JournalValueObj.year = txtboxYear.Text.Trim();
               JournalValueObj.Comments = txtboxComments.Text.ToString();
               //int year1 = Convert.ToInt32(txtboxYear.Text) - 1;
               //JournalValueObj.year = year1.ToString();
               //if (txtboxFiveYearImpactFactor.Text != "")
               //{
               //    JournalValueObj.fiveimpcrfact = Convert.ToDouble(txtboxFiveYearImpactFactor.Text.Trim());
               //}
               //if (txtboxImpactfactor.Text != "")
               //{
               //    JournalValueObj.impctfact = Convert.ToDouble(txtboxImpactfactor.Text.Trim());
               //}
               JournalValueObj.Title = txtboxTitle.Text.Trim();
               JournalValueObj.AbbTitle = txtboxAbrivatedTitle.Text.Trim();
               int applicableYear = Convert.ToInt32(JournalValueObj.year) + 1;
               string applicableYear1 = applicableYear.ToString();
               //JournalValueObj.ApplicableYear =applicableYear+06+01;
               string Applicable1 = applicableYear1 + "-" + "06" + "-01";
               JournalValueObj.ApplicableYear = Applicable1;

               //int applicableYear2 = Convert.ToInt32(JournalValueObj.year) -1 ;
               //string applicableYear3 = applicableYear2.ToString();
               //JournalValueObj.Year1 = applicableYear3;
               //string Applicable2 = year1 + "-" + "06" + "-01";
               //JournalValueObj.ApplicableYear1 = Applicable2;

               //DateTime Applicable=Convert.  Applicable1
               for (int i = 0; i < cblActiveyear.Items.Count; i++)
               {
                   if (cblActiveyear.Items[i].Selected == true)
                   {
                       JournalValueObj.ActiveYear = Convert.ToInt32(cblActiveyear.Items[i].Text.ToString());
                       list.Add(JournalValueObj.ActiveYear);
                   }

               }
               if (list.Count == 0)
               {
                   ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please select the Active year')</script>");
                   return;
               }

               int res = B.JournalEntrySaveChanges(JournalValueObj, list);
               if (res == 1)
               {
                   ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Saved Successfully')</script>");
                   //log.Info("Saved Successfully , id: " + txtboxID.Text.Trim() + ", year: " + txtboxYear.Text.Trim());


               }
               else
               {
                   ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Could Not Be Saved Successfully')</script>");
                   //log.Info("Could Not Be Saved Successfully , id: " + txtboxID.Text.Trim() + ", year: " + txtboxYear.Text.Trim());
               }

               btnSaveUpdate.Enabled = false;
               
           }
        }
        catch (Exception ex)
        {
            log.Error("Inside Catch Block Of Manage Journal" + ex.Message);
            log.Error(ex.StackTrace);
            ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Error!!!!!!!!!')</script>");

        }
    
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        PanelView.Visible = true;
        GridViewIndex.DataBind();
        GridViewIndex.Visible = true;
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        txtboxTitle.Enabled = true;
        txtboxAbrivatedTitle.Enabled = true;

        //txtboxYear.Text = "";
        txtboxTitle.Text = "";
        //txtboxImpactfactor.Text = "";
        txtboxID.Text = "";
        txtboxComments.Text = "";
        txtboxAbrivatedTitle.Text = "";
        PanelView.Visible = false;
        //txtboxFiveYearImpactFactor.Text = "";
        cblActiveyear.ClearSelection();
        btnSaveUpdate.Enabled = false;
        dropdownCategory.ClearSelection();
        

    }

    protected void JournalIDTextChanged(object sender, EventArgs e)
    {
        ArrayList list = new ArrayList();
        //txtboxImpactfactor.Text = "";
        //txtboxFiveYearImpactFactor.Text = "";
        txtboxComments.Text = "";
        //txtboxYear.Text = "";
        txtboxTitle.Text = "";
        txtboxAbrivatedTitle.Text = "";
        cblActiveyear.ClearSelection();
        btnSaveUpdate.Enabled = true;
        popupPanelJournal.Visible = false;
        JournalValueObj.JournalID = txtboxID.Text;
        // JournalValueObj.year = txtBoxYear.Text;

        JournalData j = new JournalData();
        j = B.JournalEntryCheckExistance(JournalValueObj);
        if (j.jid != null)
        {
            log.Debug("inside --JournalIDTextChanged--update publish" + j.jid);
            // ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Entry ALready Exists')</script>");
            txtboxTitle.Text = j.name;
            txtboxAbrivatedTitle.Text = j.jname;
            // dropdownCategory.DataBind();
            if (j.Category != null)
            {
                dropdownCategory.SelectedValue = j.Category;

            }
            else
            {
            }
            // txtboxFiveYearImpactFactor.Text = JournalValueObj.fiveimpcrfact.ToString();

            //txtboxImpactfactor.Enabled = false;
            //txtboxFiveYearImpactFactor.Enabled = false;
            txtboxComments.Enabled = true;
            //txtboxYear.Enabled = true;
            //txtboxTitle.Enabled = false;
            //txtboxAbrivatedTitle.Enabled = false;

            string year = DateTime.Now.Year.ToString();
            int Jyear = Convert.ToInt32(year) - 1;
            //txtboxYear.Text = Jyear.ToString();


            //txtboxYear_TextChanged(sender, e);
            list = B.SelectActiveYear(JournalValueObj);
           
            for (int i = 0; i < list.Count; i++)
            {

                ListItem currentCheckBox = cblActiveyear.Items.FindByValue(list[i].ToString());
                if (currentCheckBox != null)
                {
                    currentCheckBox.Selected = true;
                }
                //cblActiveyear.Text = list[i].ToString();
                //cblActiveyear.Items[i].Selected = true;
            }
        }
        else
        {
            log.Debug("inside --JournalIDTextChanged--New Publish Id" + j.jid);
            btnSaveUpdate.Enabled = true;
            ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('New Entry..Please enter the details !!!!')</script>");
            txtboxTitle.Enabled = true;
            txtboxAbrivatedTitle.Enabled = true;
        }


    }

    //protected void txtboxYear_TextChanged(object sender, EventArgs e)
    //{
    //    btnSaveUpdate.Enabled = true;
    //    //JournalValueObj.year = txtboxYear.Text;
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
    //            txtboxImpactfactor.Text = j.impctfact.ToString();
    //            txtboxComments.Text = j.Comments;
    //            txtboxFiveYearImpactFactor.Text = j.fiveimpcrfact.ToString();

    //            txtboxImpactfactor.Enabled = true;
    //            txtboxFiveYearImpactFactor.Enabled = true;
    //            txtboxComments.Enabled = true;
    //            txtboxYear.Enabled = true;
    //            txtboxTitle.Enabled = false;
    //            txtboxAbrivatedTitle.Enabled = false;


    //        }
    //        else
    //        {


    //            log.Debug("inside--txtboxYear_TextChanged--" + j.jid);
    //            // Not mentioned
    //            txtboxImpactfactor.Enabled = true;
    //            txtboxFiveYearImpactFactor.Enabled = true;
    //            txtboxComments.Enabled = true;
    //            //txtboxYear.Text = "";
    //            txtboxYear.Enabled = true;
    //            txtboxImpactfactor.Text = string.Empty;
    //            txtboxFiveYearImpactFactor.Text = "";
    //            txtboxComments.Text = "";
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
        //txtboxYear.Text = Jyear.ToString();


        //txtboxYear_TextChanged(sender, e);

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
}