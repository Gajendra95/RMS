   using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using System.Collections;
using System.Data;

public partial class ManageJournal_ManageImpactFactor : System.Web.UI.Page
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

            if (saveOrUpdate == 3) // SAVE
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
                    ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please enter the Remarks')</script>");
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
                int applicableYear = Convert.ToInt32(JournalValueObj.year)+1;
                string applicableYear1=applicableYear.ToString();
                //JournalValueObj.ApplicableYear =applicableYear+06+01;
               string Applicable1=  applicableYear1+"-"+"06"+"-01";
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
                DataTable dtCurrentTableProjectOutcome = (DataTable)ViewState["ImpactFactorDetails"];
                JournalData[] JDP = null;
                GrantData journalbank = new GrantData();
                //insert Fund Reciept

                JDP = new JournalData[dtCurrentTableProjectOutcome.Rows.Count];

                int rowIndex2 = 0;
                if (dtCurrentTableProjectOutcome.Rows.Count > 0)
                {

                    for (int i = 0; i < dtCurrentTableProjectOutcome.Rows.Count; i++)
                    {
                        JDP[i] = new JournalData();

                        TextBox impactYear = (TextBox)GridViewProjectsOutcome.Rows[rowIndex2].Cells[1].FindControl("txtYear");
                        TextBox OneImpactFactor = (TextBox)GridViewProjectsOutcome.Rows[rowIndex2].Cells[2].FindControl("txtImpactFactor");
                        TextBox FiveYearImpactFactor = (TextBox)GridViewProjectsOutcome.Rows[rowIndex2].Cells[3].FindControl("txtFiveYearImpactFactor");
                        if (dtCurrentTableProjectOutcome.Rows.Count > 0)
                        {




                            if (impactYear.Text == "")
                            {
                                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please enter the Year!')</script>");
                                return;
                                //string CloseWindow = "alert('Please enter the Year!')";
                                //ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);
                                //return;

                            }
                            if (OneImpactFactor.Text == "")
                            {
                                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please enter the Impact Factor!')</script>");
                                return;
                                //string CloseWindow = "alert('Please enter the Impact Factor!')";
                                //ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);
                                //return;

                            }
                            if (FiveYearImpactFactor.Text == "")
                            {
                                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please enter Five Year Impact Factor!')</script>");
                                return;
                                //string CloseWindow = "alert('Please enter Five Year Impact Factor!')";
                                //ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);
                                //return;

                            }
                            JDP[i].year = impactYear.Text.Trim();
                            JDP[i].impctfact =Convert.ToDouble( OneImpactFactor.Text.Trim());
                            JDP[i].fiveimpcrfact = Convert.ToDouble(FiveYearImpactFactor.Text.Trim());                         
                        }
                        rowIndex2++;
                    }


                }

                int res = B.JournalEntrySaveImpactfactorChanges(JournalValueObj, list, JDP);
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
        Label3.Visible = true;
        ADDProjectOutcome.Visible = true;
        txtboxComments.Enabled = true;
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

        int result = B.GetImpactFactorDetails(JournalValueObj);
        if (result > 0)
        {
            SetExistingimpactFactorDetails(JournalValueObj);
        }
        else
        {
            SetintialRowDataProjectOutcome();
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

    private void SetintialRowDataProjectOutcome()
    {
        DataTable dy = new DataTable();
        dy.Columns.Add("Year", typeof(string));
        dy.Columns.Add("ImpactFactor", typeof(string));
        dy.Columns.Add("FiveImpFact", typeof(string));
        DataRow dr = dy.NewRow();
        dr["Year"] = string.Empty;
        dr["ImpactFactor"] = string.Empty;
        dr["FiveImpFact"] = string.Empty;
        dy.Rows.Add(dr);
        ViewState["ImpactFactorDetails"] = dy;
        GridViewProjectsOutcome.DataSource = dy;
        GridViewProjectsOutcome.DataBind();
        TextBox impactYear = (TextBox)GridViewProjectsOutcome.Rows[0].Cells[1].FindControl("txtYear");
        TextBox OneImpactFactor = (TextBox)GridViewProjectsOutcome.Rows[0].Cells[2].FindControl("txtImpactFactor");
        TextBox FiveYearImpactFactor = (TextBox)GridViewProjectsOutcome.Rows[0].Cells[3].FindControl("txtFiveYearImpactFactor");
        impactYear.Text = "";
        OneImpactFactor.Text = "";
        FiveYearImpactFactor.Text = "";
    }


    private void SetExistingimpactFactorDetails(JournalData JournalValueObj)
    {
        Business b = new Business();

        DataTable dyPO = b.SelectImpactFactorDetails(JournalValueObj);
        if (dyPO.Rows.Count != 0)
        {
            ViewState["ImpactFactorDetails"] = dyPO;
            GridViewProjectsOutcome.DataSource = dyPO;
            GridViewProjectsOutcome.DataBind();
            GridViewProjectsOutcome.Visible = true;

            int rowIndex2 = 0;

            DataTable table = (DataTable)ViewState["ImpactFactorDetails"];
            DataRow drCurrentRow2 = null;
            if (table.Rows.Count > 0)
            {
                for (int i = 1; i <= table.Rows.Count; i++)
                {
                    TextBox impactYear = (TextBox)GridViewProjectsOutcome.Rows[rowIndex2].Cells[1].FindControl("txtYear");
                    TextBox OneImpactFactor = (TextBox)GridViewProjectsOutcome.Rows[rowIndex2].Cells[2].FindControl("txtImpactFactor");
                    TextBox FiveYearImpactFactor = (TextBox)GridViewProjectsOutcome.Rows[rowIndex2].Cells[3].FindControl("txtFiveYearImpactFactor");
                    drCurrentRow2 = table.NewRow();
                    impactYear.Text = table.Rows[i - 1]["Year"].ToString();
                    OneImpactFactor.Text = table.Rows[i - 1]["ImpactFactor"].ToString();
                    FiveYearImpactFactor.Text = table.Rows[i - 1]["FiveImpFact"].ToString();
                    rowIndex2++;

                }


                ViewState["ImpactFactorDetails"] = table;
            }
        }
        else 
        {
            SetRowDataProjectOutcome();
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
       // txtboxYear.Text = Jyear.ToString();


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
    protected void GridViewProjectsOutcome_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {

        }


    }
    protected void GridViewProjectsOutcome_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        SetRowDataProjectOutcome();
        if (ViewState["ImpactFactorDetails"] != null)
        {
            DataTable dt = (DataTable)ViewState["ImpactFactorDetails"];
            DataRow drCurrentRow = null;
            int rowIndex = Convert.ToInt32(e.RowIndex);
            if (dt.Rows.Count > 1 && rowIndex != 0)
            {
                dt.Rows.Remove(dt.Rows[rowIndex]);
                drCurrentRow = dt.NewRow();
                ViewState["ImpactFactorDetails"] = dt;
                GridViewProjectsOutcome.DataSource = dt;
                GridViewProjectsOutcome.DataBind();

                SetOldDataProjectOutcomeDetails();
                // gridAmtChanged(sender, e);
            }
        }
    }

    private void SetRowDataProjectOutcome()
    {
        GridViewProjectsOutcome.Visible=true;
        ADDProjectOutcome.Visible = true;
        int rowIndex = 0;

        if (ViewState["ImpactFactorDetails"] != null)
        {
            DataTable dtCurrentTablePO = (DataTable)ViewState["ImpactFactorDetails"];
            DataRow drCurrentRow = null;
            if (dtCurrentTablePO.Rows.Count > 0)
            {
                for (int i = 1; i <= dtCurrentTablePO.Rows.Count; i++)
                {
                    TextBox impactYear = (TextBox)GridViewProjectsOutcome.Rows[rowIndex].Cells[1].FindControl("txtYear");
                    TextBox OneImpactFactor = (TextBox)GridViewProjectsOutcome.Rows[rowIndex].Cells[2].FindControl("txtImpactFactor");
                    TextBox FiveYearImpactFactor = (TextBox)GridViewProjectsOutcome.Rows[rowIndex].Cells[3].FindControl("txtFiveYearImpactFactor");
                    drCurrentRow = dtCurrentTablePO.NewRow();
                    dtCurrentTablePO.Rows[i - 1]["Year"] = impactYear.Text;
                     dtCurrentTablePO.Rows[i - 1]["ImpactFactor"] = OneImpactFactor.Text;
                     dtCurrentTablePO.Rows[i - 1]["FiveImpFact"] = FiveYearImpactFactor.Text;
                    rowIndex++;
                }
             
                ViewState["ImpactFactorDetails"] = dtCurrentTablePO;

            }

            else
            {
                Response.Write("ViewState is null");
            }
            //SetPreviousData();
        }
    
    }




    private void SetOldDataProjectOutcomeDetails()
    {

        int rowIndex = 0;
        if (ViewState["ImpactFactorDetails"] != null)
        {
            DataTable dt = (DataTable)ViewState["ImpactFactorDetails"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {                    
                    TextBox impactYear = (TextBox)GridViewProjectsOutcome.Rows[rowIndex].Cells[1].FindControl("txtYear");
                    TextBox OneImpactFactor = (TextBox)GridViewProjectsOutcome.Rows[rowIndex].Cells[2].FindControl("txtImpactFactor");
                    TextBox FiveYearImpactFactor = (TextBox)GridViewProjectsOutcome.Rows[rowIndex].Cells[3].FindControl("txtFiveYearImpactFactor");


                    if (dt.Rows[i]["Year"].ToString() != "")
                    {
                        impactYear.Text = dt.Rows[i]["Year"].ToString();
                     
                    }
                    if (dt.Rows[i]["ImpactFactor"].ToString() != "")
                    {
                        OneImpactFactor.Text = dt.Rows[i]["ImpactFactor"].ToString();
                       
                    }
                    if (dt.Rows[i]["FiveImpFact"].ToString() != "")
                    {
                        FiveYearImpactFactor.Text = dt.Rows[i]["FiveImpFact"].ToString();
                    }
                    rowIndex++;
                }
            }
        }
    
    }
    protected void ADDProjectOutcome_Click(object sender, EventArgs e)
    {

        if (GridViewProjectsOutcome.Rows.Count == 0)
        {
            //BindGridview();
        }


        else
        {
            int rowIndex = 0;

            if (ViewState["ImpactFactorDetails"] != null)
            {
                DataTable dt = (DataTable)ViewState["ImpactFactorDetails"];
                DataRow drCurrentRow = null;
                if (dt.Rows.Count > 0)
                {
                    for (int i = 1; i <= dt.Rows.Count; i++)
                    {

                        TextBox impactYear = (TextBox)GridViewProjectsOutcome.Rows[rowIndex].Cells[1].FindControl("txtYear");
                        TextBox OneImpactFactor = (TextBox)GridViewProjectsOutcome.Rows[rowIndex].Cells[2].FindControl("txtImpactFactor");
                        TextBox FiveYearImpactFactor = (TextBox)GridViewProjectsOutcome.Rows[rowIndex].Cells[3].FindControl("txtFiveYearImpactFactor");
                        drCurrentRow = dt.NewRow();


                        if (impactYear.Text != "")
                        {
                            dt.Rows[i - 1]["Year"] = impactYear.Text;
                        }
                        if (OneImpactFactor.Text != "")
                        {
                            dt.Rows[i - 1]["ImpactFactor"] = OneImpactFactor.Text;
                        }
                        if (FiveYearImpactFactor.Text != "")
                        {
                            dt.Rows[i - 1]["FiveImpFact"] = FiveYearImpactFactor.Text;
                        }
                        rowIndex++;
                    }
                    dt.Rows.Add(drCurrentRow);
                    ViewState["ImpactFactorDetails"] = dt;
                    GridViewProjectsOutcome.DataSource = dt;
                    GridViewProjectsOutcome.DataBind();
                }
            }

            else
            {
                Response.Write("ViewState Value is Null");
            }

            SetOldDataProjectOutcomeDetails();
        }
    }

}