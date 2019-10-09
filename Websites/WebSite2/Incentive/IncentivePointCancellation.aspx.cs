using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Incentive_IncentivePointCancellation : System.Web.UI.Page
{
    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    public string pageID = "L107";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!Session["authPage"].ToString().Contains("$" + pageID + "$"))
            {
                string unacces = "Unauthorized Acces!!! Login Again";
                Response.Redirect("~/Login.aspx?val=" + unacces);
            }
            PublicationTypeChanged(sender, e);
        }
    }
    protected void PublicationTypeChanged(object sender, EventArgs e)
    {
        EditUpdatePanel.Update();

        btnSave.Visible = false;
        btnSave.Visible = false;
        PnlPublicationDetails.Style.Add("display", "none");
        panelJournalArticle.Style.Add("display", "none");
        panAddAuthor.Visible = false;
        Panel2.Visible = false;
        if (radioincentive.SelectedValue == "1")
        {
            PanelPatentSearch.Style.Add("display", "none");
            panelSearchPub.Style.Add("display", "true");
            panelJournalArticle.Style.Add("display", "none");
            PnlPatentDetails.Style.Add("display", "none");
            BindPublcationGrid();
        }
        else
        {

            PanelPatentSearch.Style.Add("display", "true");
            panelSearchPub.Style.Add("display", "none");
            panelJournalArticle.Style.Add("display", "none");
            BindPatentGrid();
        }
    }


    protected void ButtonSearchPubOnClick(object sender, EventArgs e)
    {

        GridViewSearch.Visible = true;
        GridViewSearch.EditIndex = -1;
        BindPublcationGrid();
        PanelPatentSearch.Style.Add("display", "none");
        PnlPatentDetails.Style.Add("display", "none");
        PnlPublicationDetails.Style.Add("display", "none");
        panelJournalArticle.Style.Add("display", "none");
        panAddAuthor.Visible = false;
        Panel2.Visible = false;
    }

    private void BindPublcationGrid()
    {
        GridViewSearch.Visible = true;
        DataSet ds = new DataSet();
        PublishData data = new PublishData();
        data.PaublicationID = PubIDSearch.Text;
        data.JournalTitle = TextBoxWorkItemSearch.Text;
        data.TypeOfEntry = EntryTypesearch.SelectedValue;
        data.bulkpublicationyear = ConfigurationManager.AppSettings["BulkPubIncentiveYear"];

        IncentiveBusiness obj = new IncentiveBusiness();
        ds = obj.SelectPendingProcessedPublicationsForIncentivePointRevert(data);
        if (ds.Tables[0].Rows.Count > 0)
        {
            GridViewSearch.DataSource = ds;
            GridViewSearch.DataBind();
            GridViewSearch.Visible = true;
        }
        else
        {
            GridViewSearch.DataBind();
            GridViewSearch.Visible = false;
        }
        btnSave.Visible = false;
      
        //btnDiscard.Visible = false;
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
            pid = GridViewSearch.Rows[rowindex].Cells[2].Text.Trim().ToString();
            typeEntry = GridViewSearch.Rows[rowindex].Cells[5].Text.Trim().ToString();
            Session["TempPid"] = pid;
            Session["TempTypeEntry"] = typeEntry;//maintaining a session variable, passing it to registration page
        }
    }

    protected void GridViewSearchPub_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        BindPublcationGrid();
        GridViewSearch.PageIndex = e.NewPageIndex;
        GridViewSearch.DataBind();
    }
    protected void addclik(object sender, EventArgs e)
    {

        DropDownListPublicationEntry.ClearSelection();
        DropDownListMuCategory.ClearSelection();
        DropDownListMonthJA.ClearSelection();
        TextBoxYearJA.ClearSelection();
        TextBoxImpFact.Text = "";
        TextBoxImpFact5.Text = "";
        txtIFApplicableYear.Text = "";
        TextBoxPubId.Text = "";
        txtboxTitleOfWrkItem.Text = "";
        TextBoxPubJournal.Text = "";
        TextBoxNameJournal.Text = "";
        txtSNIP.Text = "";
        txtsjr.Text = "";
        txtSNIP.Text = "";
        txtSNIP.Text = "";
        lblnoteQuartile.Text = "";
        Label5.Text = "";

    }


    private void fnRecordExist(object sender, GridViewEditEventArgs e)
    {
        btnSave.Enabled = true;
        addclik(sender, e);
        string Pid = Session["TempPid"].ToString();
        string TypeEntry = Session["TempTypeEntry"].ToString();
        panelJournalArticle.Style.Add("display", "true");
        PnlPublicationDetails.Visible = true;
        Panel2.Visible = true;
        PnlPublicationDetails.Style.Add("display", "true");
        IncentiveBusiness ince_obj = new IncentiveBusiness();
        Business obj = new Business();
        PublishData v = new PublishData();
        btnSave.Visible = true;     
        //btnDiscard.Visible = true;
        btnSave.Enabled = true;
        v = obj.fnfindjid(Pid, TypeEntry);
        Session["IsStudent"] = v.IsStudentAuthor;
        PnlPublicationDetails.Style.Add("display", "true");
        DropDownListMuCategory.SelectedValue = v.MUCategorization;
        DropDownListPublicationEntry.SelectedValue = TypeEntry;
        TextBoxPubId.Text = Pid;
        txtboxTitleOfWrkItem.Text = v.TitleWorkItem;
        panelJournalArticle.Visible = true;
        TextBoxPubJournal.Text = v.PublisherOfJournal;
        TextBoxNameJournal.Text = v.PublisherOfJournalName;
        DropDownListMonthJA.DataSourceID = "SqlDataSourcePubJAmonth";
        DropDownListMonthJA.DataBind();
        DropDownListMonthJA.SelectedValue = v.PublishJAMonth.ToString();
        uploadeprint.SelectedValue = v.uploadEPrint;
        string OneYearImpFact = v.ImpactFactor;
        string FiveYearImpFact = v.ImpactFactor5;
        if (v.uploadEPrint == "Y")
        {
            lblNote.Visible = false;
            btnSave.Enabled = true;
        }
        else
        {
            lblNote.Text = "Note: The article is not uploaded to e-Print. Points entry can be done only after uploading to e-Print.";
            lblNote.Visible = true;
            btnSave.Enabled = false;
        }
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
        TextBoxYearJA.SelectedValue = v.PublishJAYear.ToString();



        string quartileid = obj.SelectQuartile(v);

        txtquartileid.Text = quartileid.ToString();
        //Session["Quartile"] = txtquartileid.Text;
        //txtquartileid.Text = Session["Quartile"].ToString();
        PublishData v2 = new PublishData();
        if (txtquartileid.Text != "")
        {
            if (txtquartileid.Text != "0")
            {
                v2 = ince_obj.getquartileName(txtquartileid.Text);
                txtquartile.Text = v2.Name;
                if (Convert.ToInt16(v.PublishJAYear) >= 2018 && Convert.ToInt16(v.PublishJAMonth) >= 7)
                {
                    if (txtquartileid.Text == "NA")
                    {
                        v.ImpactFactor = "NA";
                    }
                    else
                    {
                        v.ImpactFactor = txtquartileid.Text;
                    }

                }
            }
        }

        string quartilecode = v.Code;
        lblnoteQuartile.Visible = false;
        int pubyear = Convert.ToInt32(TextBoxYearJA.SelectedValue);
        int pubmonth = 0;

        pubmonth = Convert.ToInt32(DropDownListMonthJA.SelectedValue);
        if (pubyear >= 2018 && pubmonth >= 7)
        {
            if (txtquartileid.Text != "" && txtquartileid.Text != "NA")
            {
                lblQuartile.Visible = true;
                txtquartile.Visible = true;
                Label1.Visible = false;
                txtSNIP.Visible = false;
                Label3.Visible = false;
                txtsjr.Visible = false;
                lblnoteQuartile.Visible = false;
                //LinkButton1.Visible = false;
                //btnApprove.Enabled = true;
                //btnSave.Enabled = true;
                Grid_AuthorEntry.Enabled = true;

            }
            else
            {
                ////lblnoteQuartile.Text = "Note: Quartile Value not found please Update the Quartile Value.";
                //Grid_AuthorEntry.Enabled = false;
                ////lblnoteQuartile.Visible = true;
                //btnSave.Enabled = false;
                //txtquartile.Visible = true;
                //lblQuartile.Visible = true;
                //EditUpdatePanel.Update();
                //v.ImpactFactor = "NA";
                ////LinkButton1.Visible = true;

            }

        }
        else
        {
            lblnoteQuartile.Visible = false;
            txtquartile.Visible = false;
            lblQuartile.Visible = false;
            Grid_AuthorEntry.Enabled = true;

        }

        TextBoxImpFact.Text = OneYearImpFact;
        TextBoxImpFact5.Text = FiveYearImpFact;
        if (v.IFApplicableYear != 0)
        {
            txtIFApplicableYear.Text = v.IFApplicableYear.ToString();
        }

        IncentivePoint incentive = new IncentivePoint();
        incentive = ince_obj.SelectSNIPJRPoint(TextBoxPubJournal.Text, TextBoxYearJA.SelectedValue);
        if (incentive.SJR != 0)
        {
            txtsjr.Text = incentive.SJR.ToString();
        }
        if (incentive.SNIP != 0)
        {
            txtSNIP.Text = incentive.SNIP.ToString();
        }

        DataTable dy = null;
        int applicableyear1 = Convert.ToInt16(ConfigurationManager.AppSettings["IncentivePointApplicableYear"]);
        int publicationyear = Convert.ToInt16(TextBoxYearJA.Text);
        if (publicationyear < applicableyear1)
        {
            dy = ince_obj.SelectMUAuthorDetails(Pid, TypeEntry);
        }

        else
        {
            dy = ince_obj.SelectAuthorDetails(Pid, TypeEntry);
        }

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
                DropDownList isCorrAuth = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[2].FindControl("isCorrAuth");
                DropDownList AuthorType = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[2].FindControl("AuthorType");
                DropDownList DropdownMuNonMu1 = (DropDownList)Grid_AuthorEntry.Rows[0].Cells[3].FindControl("DropdownMuNonMu");
                DropDownList DropdownStudentInstitutionName = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[2].FindControl("DropdownStudentInstitutionName");
                DropDownList DropdownStudentDepartmentName = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[2].FindControl("DropdownStudentDepartmentName");

                //TextBox total = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[2].FindControl("txtTotalPoint");
                //TextBox basepoint = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[2].FindControl("txtBasePoint");
                //TextBox snipsjrpoint = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[2].FindControl("txtSNIPSJRPoint");
                //TextBox thresholdpoint = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[2].FindControl("txtThresholdPoint"); //point 3 (crosses 6 publication) is awarded once in a year

                Grid_AuthorEntry.Columns[5].Visible = true;
                Grid_AuthorEntry.Columns[6].Visible = true;
                //total.Enabled = false;
                //Grid_AuthorEntry.Columns[8].Visible = true;
                //Grid_AuthorEntry.Columns[9].Visible = true;
                //Grid_AuthorEntry.Columns[10].Visible = true; //point 3 (crosses 6 publication) is awarded once in a year

                drCurrentRow = dtCurrentTable.NewRow();

                //total.Text = dtCurrentTable.Rows[i - 1]["TotalPoint"].ToString();

                if (dtCurrentTable.Rows[i - 1]["DropdownMuNonMu"].ToString() == "S")
                {
                    DropdownMuNonMu.Text = dtCurrentTable.Rows[i - 1]["DropdownMuNonMu"].ToString();
                }
                else if (dtCurrentTable.Rows[i - 1]["DropdownMuNonMu"].ToString() == "O")
                {
                    dtCurrentTable.Rows[i - 1]["DropdownMuNonMu"] = "S";
                    DropdownMuNonMu.Text = dtCurrentTable.Rows[i - 1]["DropdownMuNonMu"].ToString();
                }
                else
                {
                    DropdownMuNonMu.Text = dtCurrentTable.Rows[i - 1]["DropdownMuNonMu"].ToString();
                }
                EmployeeCode.Text = dtCurrentTable.Rows[i - 1]["EmployeeCode"].ToString();
                AuthorName.Text = dtCurrentTable.Rows[i - 1]["AuthorName"].ToString();
                if (DropdownMuNonMu.Text == "M")
                {
                    InstNme.Visible = true;
                    deptname.Visible = true;
                    DropdownStudentInstitutionName.Visible = false;
                    DropdownStudentDepartmentName.Visible = false;

                    InstNme.Text = dtCurrentTable.Rows[i - 1]["InstitutionName"].ToString();
                    deptname.Text = dtCurrentTable.Rows[i - 1]["DepartmentName"].ToString();
                }

                else if (DropdownMuNonMu.Text == "N")
                {

                    InstNme.Visible = true;
                    deptname.Visible = true;
                    DropdownStudentInstitutionName.Visible = false;
                    DropdownStudentDepartmentName.Visible = false;
                    InstNme.Text = dtCurrentTable.Rows[i - 1]["InstitutionName"].ToString();
                    deptname.Text = dtCurrentTable.Rows[i - 1]["DepartmentName"].ToString();
                }
                else if (DropdownMuNonMu.Text == "S" || DropdownMuNonMu.Text == "O")
                {
                    DropdownStudentInstitutionName.Visible = false;
                    DropdownStudentDepartmentName.Visible = false;
                    InstNme.Visible = true;
                    deptname.Visible = true;
                    InstNme.Text = dtCurrentTable.Rows[i - 1]["InstitutionName"].ToString();
                    deptname.Text = dtCurrentTable.Rows[i - 1]["DepartmentName"].ToString();

                }


                PublishData publicationobj = new PublishData();
                publicationobj.PaublicationID = Session["TempPid"].ToString();
                publicationobj.TypeOfEntry = Session["TempTypeEntry"].ToString();
                string ReffenceNumber=publicationobj.TypeOfEntry+ publicationobj.PaublicationID;
                publicationobj.PublishJAYear = TextBoxYearJA.SelectedValue;
                publicationobj.EmployeeCode = dtCurrentTable.Rows[i - 1]["EmployeeCode"].ToString();
                publicationobj.AuthorName = dtCurrentTable.Rows[i - 1]["AuthorName"].ToString();
                //if (txtquartile.Text != "" || txtquartileid.Text == "NA")
                //{

                //    snipsjrpoint.Visible = false;
                //    Grid_AuthorEntry.Columns[8].Visible = false;
                //    // total.Text = basepoint.Text;
                //}
                //Grid_AuthorEntry.Columns[8].Visible = false;
                PublishData v4 = new PublishData();
                PublishData v5 = new PublishData();
                PublishData v6 = new PublishData();
                if (pubyear >= 2018 && pubmonth >= 7)
                {
                    if (txtquartileid.Text != "" && txtquartileid.Text != "0" && txtquartileid.Text != "NA")
                    {
                        v4 = ince_obj.getquartilecount(txtquartileid.Text, publicationobj.EmployeeCode, TextBoxYearJA.SelectedValue);
                        int Qcount = v4.Count;
                        v5 = ince_obj.getquartilelimit(txtquartileid.Text.ToString());
                        int Qlimit = v5.Limit;
                        v6 = ince_obj.getquartileName(txtquartileid.Text);
                        //txtquartile.Text = v.Name;
                        string quartilecode1 = v6.Code;
                        if (Qlimit == 0)
                        {
                            Label5.Visible = false;
                            //basepoint.Enabled = true;
                        }
                        else
                            if (Qlimit == Qcount)
                            {


                                //Label5.Text = "Note: The Authors has already claimed Points for  '" + Qlimit + "' papers in '" + quartilecode1 + "' journals in publication year so they are not applicable for Point Entry .";

                                //Label5.Visible = true;
                                //basepoint.Enabled = false;
                            }
                    }
                }
                else
                {
                    Label5.Visible = false;
                    //basepoint.Enabled = true;
                }
                rowIndex++;
            }


            ViewState["CurrentTable"] = dtCurrentTable;
        }

        DataTable dz = null;
        int applicableyear2 = Convert.ToInt16(ConfigurationManager.AppSettings["IncentivePointApplicableYear"]);
        int publicationyear3 = Convert.ToInt16(TextBoxYearJA.Text);
        if (publicationyear3 < applicableyear2)
        {
            dz = ince_obj.SelectMUAuthorDetails(Pid, TypeEntry);
        }

        else
        {
            dz = ince_obj.SelectAuthorDetails(Pid, TypeEntry);
        }

        ViewState["CurrentTablePoint"] = dz;
        GridViewIncentivePints.DataSource = dz;
        GridViewIncentivePints.DataBind();
        GridViewIncentivePints.Visible = true;
        panAddAuthor.Visible = true;

        int rowIndexPoints = 0;

        DataTable dtCurrentTablePoints = (DataTable)ViewState["CurrentTablePoint"];
        DataRow drCurrentRowPoints = null;
        if (dtCurrentTablePoints.Rows.Count > 0)
        {
            for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
            {
                DropDownList DropdownMuNonMu = (DropDownList)GridViewIncentivePints.Rows[rowIndexPoints].Cells[2].FindControl("DropdownMuNonMu");
                TextBox EmployeeCode = (TextBox)GridViewIncentivePints.Rows[rowIndexPoints].Cells[2].FindControl("EmployeeCode");
                TextBox AuthorName = (TextBox)GridViewIncentivePints.Rows[rowIndexPoints].Cells[2].FindControl("AuthorName");      
                TextBox OldCurrentBalance = (TextBox)GridViewIncentivePints.Rows[rowIndexPoints].Cells[2].FindControl("txtOldCurrentBalance");
                TextBox RevertingPoint = (TextBox)GridViewIncentivePints.Rows[rowIndexPoints].Cells[2].FindControl("txtRevertingPoint");
                TextBox NewCurrentBalance = (TextBox)GridViewIncentivePints.Rows[rowIndexPoints].Cells[2].FindControl("txtNewcurrentBalance");
                TextBox AdditionalPoint = (TextBox)GridViewIncentivePints.Rows[rowIndexPoints].Cells[2].FindControl("txtAdditionalPoint");
                TextBox UpdatedAdditionalPoint = (TextBox)GridViewIncentivePints.Rows[rowIndexPoints].Cells[2].FindControl("txtUpdatedAdditionalPoint");
                drCurrentRowPoints = dtCurrentTablePoints.NewRow();

                //total.Text = dtCurrentTable.Rows[i - 1]["TotalPoint"].ToString();
                EmployeeCode.Text = dtCurrentTablePoints.Rows[i - 1]["EmployeeCode"].ToString();
                AuthorName.Text = dtCurrentTablePoints.Rows[i - 1]["AuthorName"].ToString();
                DropdownMuNonMu.Text = dtCurrentTablePoints.Rows[i - 1]["DropdownMuNonMu"].ToString();
                IncentivePoint data = new IncentivePoint();
                string PaublicationID = Session["TempPid"].ToString();
                string TypeOfEntry = Session["TempTypeEntry"].ToString();
                string ReffenceNumber = TypeOfEntry + PaublicationID;
                data = ince_obj.SelectRevertingPointsforAuthorDetails(EmployeeCode.Text, ReffenceNumber, TextBoxYearJA.SelectedValue);
                OldCurrentBalance.Text = data.CurrentBalance.ToString();
             
                RevertingPoint.Text = data.TotalPoint.ToString();
                double currentbalance = Convert.ToDouble( data.CurrentBalance);
                double Totalpoint =Convert.ToDouble( data.TotalPoint);
                double APoint =Convert.ToDouble( data.Points);
                int TotalNOfPub = data.TotalNoOfPublications - 1;
                if (TotalNOfPub >= 6)
                {
                    if (Convert.ToDouble(APoint) != 0.0)
                    {
                        AdditionalPoint.Text = APoint.ToString();                 
                        UpdatedAdditionalPoint.Text = "0.0";
                        double ADCurrentBalance = currentbalance - Totalpoint - APoint ;
                        NewCurrentBalance.Text = ADCurrentBalance.ToString();
                    }
                    else
                    {
                        AdditionalPoint.Text = "0.0";
                        double ADCurrentBalance = currentbalance - Totalpoint;
                        NewCurrentBalance.Text = ADCurrentBalance.ToString();
                        UpdatedAdditionalPoint.Text = "0.0";

                    }
                }
                else
                {
                    if (Convert.ToDouble(APoint) != 0.0)
                    {
                        AdditionalPoint.Text = APoint.ToString();
                        double ADCurrentBalance = currentbalance - Totalpoint - APoint;
                        NewCurrentBalance.Text = ADCurrentBalance.ToString();
                        //string Upcurrentbalance = ince_obj.UpdatedSelectYearWisePoints(EmployeeCode.Text, TextBoxYearJA.SelectedValue, ReffenceNumber);
                        //data.UpCurrentBalance = Convert.ToDouble(Upcurrentbalance);
                        //double value = (Convert.ToDouble(Upcurrentbalance) * 25) / 100;
                        //value = Math.Round(value, 2);
                        UpdatedAdditionalPoint.Text = "0.0";
                    }
                    else
                    {
                        AdditionalPoint.Text = "0.0";
                        double ADCurrentBalance = currentbalance - Totalpoint;
                        NewCurrentBalance.Text = ADCurrentBalance.ToString();
                        UpdatedAdditionalPoint.Text = "0.0";

                    }
                }
               
                

                rowIndexPoints++;
            }
            ViewState["CurrentTablePoint"] = dtCurrentTablePoints;
        }
     
    }





    protected void BtnSave_Click(object sender, EventArgs e)
    {
        if (!Page.IsValid)
        {
            return;
        }
        try
        {
              PublishData data = new PublishData();
            IncentiveBusiness obj = new IncentiveBusiness();
            data.CreatedBy = Session["UserId"].ToString();
            data.CreatedDate=DateTime.Now;
         data.PaublicationID = Session["TempPid"].ToString();
               data.TypeOfEntry= Session["TempTypeEntry"].ToString();
            data.PublishJAYear=TextBoxYearJA.SelectedValue;
            string PublishMonth = DropDownListMonthJA.SelectedValue;
            int PUBMonth = Convert.ToInt16(PublishMonth);
            data.PublishJAMonth = PUBMonth;
           data.Quartile= txtquartileid.Text;
            if (txtcancelRemarks.Text == "")
            {
                string CloseWindow1 = "alert('Please enter Remarks!!!!')";
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow1, true);
                //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('')</script>");

                return;
            }
            data.PubCancelRemarks = txtcancelRemarks.Text.Trim();
            //int rowIndex1 = 0;
            //DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
            //PublishData[] JD = new PublishData[dtCurrentTable.Rows.Count];
        

            //if (radioincentive.SelectedValue == "1")
            //{
            //    data.PaublicationID = TextBoxPubId.Text.Trim();
            //    data.TypeOfEntry = DropDownListPublicationEntry.SelectedValue;
            //}
            //else
            //{
            //    data.PaublicationID = txtID.Text.Trim();
            //}
            //if (dtCurrentTable.Rows.Count > 0)
            //{

            //    for (int i = 0; i < dtCurrentTable.Rows.Count; i++)
            //    {
            //        JD[i] = new PublishData();

            //        TextBox AuthorName = (TextBox)Grid_AuthorEntry.Rows[rowIndex1].Cells[1].FindControl("AuthorName");
            //        DropDownList DropdownMuNonMu = (DropDownList)Grid_AuthorEntry.Rows[rowIndex1].Cells[2].FindControl("DropdownMuNonMu");
            //        TextBox EmployeeCode = (TextBox)Grid_AuthorEntry.Rows[rowIndex1].Cells[0].FindControl("EmployeeCode");
            //        TextBox DepartmentName = (TextBox)Grid_AuthorEntry.Rows[rowIndex1].Cells[0].FindControl("DepartmentName");
            //        TextBox InstitutionName = (TextBox)Grid_AuthorEntry.Rows[rowIndex1].Cells[6].FindControl("InstitutionName");

            //        DropDownList DropdownStudentInstitutionName = (DropDownList)Grid_AuthorEntry.Rows[rowIndex1].Cells[0].FindControl("DropdownStudentInstitutionName");
            //        DropDownList DropdownStudentDepartmentName = (DropDownList)Grid_AuthorEntry.Rows[rowIndex1].Cells[0].FindControl("DropdownStudentDepartmentName");

            //        TextBox TotalPoint = (TextBox)Grid_AuthorEntry.Rows[rowIndex1].Cells[0].FindControl("txtTotalPoint");
            //        TextBox BasePoint = (TextBox)Grid_AuthorEntry.Rows[rowIndex1].Cells[6].FindControl("txtBasePoint");
            //        TextBox SNIPSJRPoint = (TextBox)Grid_AuthorEntry.Rows[rowIndex1].Cells[0].FindControl("txtSNIPSJRPoint");
            //        //TextBox ThresholdPoint = (TextBox)Grid_AuthorEntry.Rows[rowIndex1].Cells[6].FindControl("txtThresholdPoint"); //point 3 (crosses 6 publication) is awarded once in a year


            //        JD[i].AuthorName = AuthorName.Text.Trim();
            //        JD[i].MUNonMU = DropdownMuNonMu.Text.Trim();
            //        JD[i].EmployeeCode = EmployeeCode.Text;
            //        if (radioincentive.SelectedValue == "1")
            //        {
            //            if (BasePoint.Text != "")
            //            {
            //                JD[i].BasePoint = Convert.ToDouble(BasePoint.Text);
            //            }
            //            else
            //            {
            //                JD[i].BasePoint = 0.0;
            //            }
            //            if (SNIPSJRPoint.Text != "")
            //            {
            //                JD[i].SNIPSJRPoint = Convert.ToDouble(SNIPSJRPoint.Text);
            //            }
            //            else
            //            {
            //                JD[i].SNIPSJRPoint = 0.0;
            //            }

            //            //point 3 (crosses 6 publication) is awarded once in a year
            //            //if (ThresholdPoint.Text != "")
            //            //{
            //            //    JD[i].ThresholdPoint = Convert.ToDouble(ThresholdPoint.Text);
            //            //}
            //            //else
            //            //{
            //            //    JD[i].ThresholdPoint = 0.0;
            //            //}
            //            JD[i].ThresholdPoint = 0.0;
            //            JD[i].TotalPoint = Convert.ToDouble(JD[i].BasePoint + JD[i].SNIPSJRPoint + JD[i].ThresholdPoint);
            //            // JD[i].TotalPoint = Math.Round(total, 2);
            //        }
            //        else
            //        {
            //            JD[i].TotalPoint = Convert.ToDouble(TotalPoint.Text);
            //        }
            //        rowIndex1++;

            //    }
        //ViewState["CurrentTablePoint"] = dz;
        //GridViewIncentivePints.DataSource = dz;
        //GridViewIncentivePints.DataBind();
        //GridViewIncentivePints.Visible = true;

               //int rowIndex1 = 0;
            //DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
         
        panAddAuthor.Visible = true;

        int rowIndexPoints = 0;

        DataTable dtCurrentTablePoints = (DataTable)ViewState["CurrentTablePoint"];
        DataRow drCurrentRowPoints = null;
            IncentivePoint[] JD = new IncentivePoint[dtCurrentTablePoints.Rows.Count];
            IncentivePoint value = new IncentivePoint();
        if (dtCurrentTablePoints.Rows.Count > 0)
        {
            for (int i = 0; i < dtCurrentTablePoints.Rows.Count; i++)
            {
                JD[i] = new IncentivePoint();
                TextBox EmployeeCode = (TextBox)GridViewIncentivePints.Rows[rowIndexPoints].Cells[2].FindControl("EmployeeCode");
                TextBox AuthorName = (TextBox)GridViewIncentivePints.Rows[rowIndexPoints].Cells[2].FindControl("AuthorName");
                TextBox OldCurrentBalance = (TextBox)GridViewIncentivePints.Rows[rowIndexPoints].Cells[2].FindControl("txtOldCurrentBalance");
                TextBox RevertingPoint = (TextBox)GridViewIncentivePints.Rows[rowIndexPoints].Cells[2].FindControl("txtRevertingPoint");
                TextBox NewCurrentBalance = (TextBox)GridViewIncentivePints.Rows[rowIndexPoints].Cells[2].FindControl("txtNewcurrentBalance");
                TextBox AdditionalPoint = (TextBox)GridViewIncentivePints.Rows[rowIndexPoints].Cells[2].FindControl("txtAdditionalPoint");
                //drCurrentRowPoints = dtCurrentTablePoints.NewRow();


                   JD[i].MemberId = EmployeeCode.Text;
                   JD[i].OldCurrentBalance =Convert.ToDouble( OldCurrentBalance.Text);
                   JD[i].RevertingPoint = Convert.ToDouble(RevertingPoint.Text);
                   JD[i].NewCurrentBalance = Convert.ToDouble(NewCurrentBalance.Text);


             


                rowIndexPoints++;
            }
        }
                bool result = false;
                if (radioincentive.SelectedValue == "1")
                {
                    PublishData d = new PublishData();
                    d = obj.CheckIncentivePointRevertStatus(JD, data);
                    //publication Points
                    if ((d.Status == "APP") && (d.IncentivePointSatatus == "APP"))
                    {
                    result = obj.RevertIncentivePointToAuthor(JD, data);
                    }
                    else if ((d.Status == "CAN") && (d.IncentivePointSatatus == "CAN"))
                    {
                        string CloseWindow1 = "alert('Points Already Reverted Succesfully')";
                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow1, true);
                        btnSave.Enabled = false;
                    }
                    }   
                if (result == true)
                {
                    string CloseWindow1 = "alert('Incentive Point Reverted successfully')";
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow1, true);
                    btnSave.Enabled = false;
                    SendMail();
                }
                else
                {
                    string CloseWindow1 = "alert('problem while Revering')";
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow1, true);
                  
                }

            }
        
        catch (Exception ex)
        {
            string CloseWindow1 = "alert('problem while Reverting points')";
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow1, true);
          
            btnSave.Enabled = true;
            log.Error(ex.Message);
            log.Error(ex.StackTrace);
        }
    }


    //Patent
    protected void GridViewSearchPatent_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        ImageButton EditButton = (ImageButton)e.Row.FindControl("BtnEdit");
    }
    public void GridViewSearchPatent_OnRowedit(Object sender, GridViewEditEventArgs e)
    {
        Patent a = new Patent();
        GridViewSearchPatent.EditIndex = e.NewEditIndex;
        SelectPatent(sender, e);

    }
    protected void GridViewSearchPatent_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        BindPatentGrid();
        GridViewSearchPatent.PageIndex = e.NewPageIndex;
        GridViewSearchPatent.DataBind();
    }
    protected void ButtonSearchProjectOnClick(object sender, EventArgs e)
    {
        GridViewSearchPatent.EditIndex = -1;
        BindPatentGrid();
        PanelPatentSearch.Style.Add("display", "true");
        PnlPatentDetails.Style.Add("display", "none");
        PnlPublicationDetails.Style.Add("display", "none");
        panelJournalArticle.Style.Add("display", "none");
        panAddAuthor.Visible = false;
        Panel2.Visible = false;
    }

    private void BindPatentGrid()
    {
        if (PatIDSearch.Text == "" && TextBoxtiltleSearch.Text == "")
        {
            SqlDataSource1.SelectCommand = " select p.ID,p.Title,r.F_OfficeName as FilingOffice,p.Grant_Date,s.StatusName as Filling_Status from Patent_Data p,Patent_Status s ,Pat_FilingOffice_M r where p.Filing_Status=s.Id and p.Filing_Office=r.Id and(IncentivePointStatus='PEN' or IncentivePointStatus='PRC')and StatusName='Granted' ";
        }
        else if (PatIDSearch.Text != "" && TextBoxtiltleSearch.Text == "")
        {
            SqlDataSource1.SelectParameters.Clear();
            SqlDataSource1.SelectParameters.Add("ID", PatIDSearch.Text);

            SqlDataSource1.SelectCommand = " select p.ID,p.Title,r.F_OfficeName as FilingOffice,p.Grant_Date,s.StatusName as Filling_Status from Patent_Data p,Patent_Status s ,Pat_FilingOffice_M r where p.Filing_Status=s.Id and p.Filing_Office=r.Id and p.ID like '%' + @ID + '%'  and (IncentivePointStatus='PEN' or IncentivePointStatus='PRC') and StatusName='Granted'";
        }

        else if (PatIDSearch.Text != "" && TextBoxtiltleSearch.Text != "")
        {
            SqlDataSource1.SelectParameters.Clear();
            SqlDataSource1.SelectParameters.Add("Title", TextBoxtiltleSearch.Text.Trim());

            SqlDataSource1.SelectCommand = "  select p.ID,p.Title,r.F_OfficeName as FilingOffice,p.Grant_Date,s.StatusName as Filling_Status from Patent_Data p,Patent_Status s,Pat_FilingOffice_M r where p.Filing_Status=s.Id and p.Filing_Office=r.Id and p.Title  LIKE  '%' + @Title + '%' and (IncentivePointStatus='PEN' or IncentivePointStatus='PRC') and StatusName='Granted'  ";
        }
        else
        {
            SqlDataSource1.SelectParameters.Clear();
            SqlDataSource1.SelectParameters.Add("ID", PatIDSearch.Text);
            SqlDataSource1.SelectParameters.Add("Title", TextBoxtiltleSearch.Text.Trim());

            SqlDataSource1.SelectCommand = " select p.ID,p.Title,p,r.F_OfficeName as FilingOffice,p.Grant_Date,s.StatusName as Filling_Status from Patent_Data p,Patent_Status s,Pat_FilingOffice_M r where p.Filing_Status=s.Id and  p.Filing_Office=r.Id and  p.ID  LIKE '%'' + @ID + ''%' and p.Title  LIKE  '%' + @Title + '%' and (IncentivePointStatus='PEN' or IncentivePointStatus='PRC') and StatusName='Granted'";

        }
        GridViewSearchPatent.DataBind();
        SqlDataSource1.DataBind();
        GridViewSearchPatent.Visible = true;
    }

    public void GridViewSearchPatent_RowCommand(Object sender, GridViewCommandEventArgs e)
    {
        string ID = null;
        if (e.CommandName == "Edit")
        {
            GridViewRow rowSelect = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
            int rowindex = rowSelect.RowIndex;
            ID = GridViewSearchPatent.Rows[rowindex].Cells[1].Text.Trim().ToString();
            Session["TempPid"] = ID;
            Session["TempTypeEntry"] = "";
        }

    }

    private void SelectPatent(object sender, GridViewEditEventArgs e)
    {
        string ID = Session["TempPid"].ToString();

        // string PT_UTN = Session["patentseedUTNseed"].ToString();
        Patent Pat = new Patent();
        Patent_DAobject obj = new Patent_DAobject();
        IncentiveBusiness bus_obj = new IncentiveBusiness();
        PanelPatentSearch.Style.Add("display", "true");
        PnlPublicationDetails.Style.Add("display", "true");
        PnlPatentDetails.Style.Add("display", "true");
        GridViewSearchPatent.Visible = true;
        Pat = obj.SelectPatent(ID);
        txtID.Text = ID;
        txtPatentno.Text = Pat.Patent_Number;
        txtPatUTN.Text = Pat.Pat_UTN;
        txtTitle.Text = Pat.Title;
        txtde.Text = Pat.description;
        txtgrantdate.Text = Pat.Grant_Date.ToShortDateString();
        txtfilingoffice.Text = Pat.Filing_Office;
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






        SetPanelVisibility();


        DataTable dy = bus_obj.SelectPatentInventorDetail(ID);
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
                DropDownList isCorrAuth = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[2].FindControl("isCorrAuth");
                DropDownList AuthorType = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[2].FindControl("AuthorType");
                DropDownList DropdownStudentInstitutionName = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[2].FindControl("DropdownStudentInstitutionName");
                DropDownList DropdownStudentDepartmentName = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[2].FindControl("DropdownStudentDepartmentName");

                TextBox total = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[2].FindControl("txtTotalPoint");
                TextBox basepoint = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[2].FindControl("txtBasePoint");
                TextBox snipsjrpoint = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[2].FindControl("txtSNIPSJRPoint");
                //TextBox thresholdpoint = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[2].FindControl("txtThresholdPoint");

                basepoint.Visible = false;
                snipsjrpoint.Visible = false;
                //thresholdpoint.Visible = false;

                drCurrentRow = dtCurrentTable.NewRow();
                total.Text = dtCurrentTable.Rows[i - 1]["TotalPoint"].ToString();

                Grid_AuthorEntry.Columns[5].Visible = false;
                Grid_AuthorEntry.Columns[6].Visible = false;
                total.Enabled = true;
                Grid_AuthorEntry.Columns[8].Visible = false;
                Grid_AuthorEntry.Columns[7].Visible = false;
                Grid_AuthorEntry.Columns[9].Visible = true;
                //Grid_AuthorEntry.Columns[10].Visible = false;

                DropdownMuNonMu.Text = dtCurrentTable.Rows[i - 1]["DropdownMuNonMu"].ToString();
                EmployeeCode.Text = dtCurrentTable.Rows[i - 1]["EmployeeCode"].ToString();
                AuthorName.Text = dtCurrentTable.Rows[i - 1]["AuthorName"].ToString();
                if (DropdownMuNonMu.Text == "M")
                {
                    InstNme.Visible = true;
                    deptname.Visible = true;
                    DropdownStudentInstitutionName.Visible = false;
                    DropdownStudentDepartmentName.Visible = false;

                    InstNme.Text = dtCurrentTable.Rows[i - 1]["InstitutionName"].ToString();
                    deptname.Text = dtCurrentTable.Rows[i - 1]["DepartmentName"].ToString();
                }

                else if (DropdownMuNonMu.Text == "N")
                {

                    InstNme.Visible = true;
                    deptname.Visible = true;
                    DropdownStudentInstitutionName.Visible = false;
                    DropdownStudentDepartmentName.Visible = false;
                    InstNme.Text = dtCurrentTable.Rows[i - 1]["InstitutionName"].ToString();
                    deptname.Text = dtCurrentTable.Rows[i - 1]["DepartmentName"].ToString();
                }
                else if (DropdownMuNonMu.Text == "S" || DropdownMuNonMu.Text == "O")
                {
                    DropdownStudentInstitutionName.Visible = false;
                    DropdownStudentDepartmentName.Visible = false;
                    InstNme.Visible = true;
                    deptname.Visible = true;
                    InstNme.Text = dtCurrentTable.Rows[i - 1]["InstitutionName"].ToString();
                    deptname.Text = dtCurrentTable.Rows[i - 1]["DepartmentName"].ToString();

                }


                rowIndex++;
            }

            ViewState["CurrentTable"] = dtCurrentTable;
        }
    }

    private void SetPanelVisibility()
    {
        btnSave.Visible = true;
        if (radioincentive.SelectedValue == "1")
        {
            PanelPatentSearch.Style.Add("display", "none");
            PnlPatentDetails.Style.Add("display", "none");
            PnlPublicationDetails.Style.Add("display", "true");
            panelJournalArticle.Style.Add("display", "true");
        }
        else
        {
            PanelPatentSearch.Style.Add("display", "true");
            PnlPatentDetails.Style.Add("display", "true");
            PnlPublicationDetails.Style.Add("display", "none");
            panelJournalArticle.Style.Add("display", "none");
        }
    }


    //private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
    //{
    //    if (!char.IsControl(e.KeyChar)
    //        && !char.IsDigit(e.KeyChar)
    //        && e.KeyChar != '.')
    //    {
    //        e.Handled = true;
    //    }

    //    // only allow one decimal point 
    //    if (e.KeyChar == '.'
    //        && (sender as TextBox).Text.IndexOf('.') > -1)
    //    {
    //        e.Handled = true;
    //    }
    //}
    //protected void btnDiscard_Click(object sender, EventArgs e)
    //{
    //    string confirmValue = hdn.Value;

    //    if (confirmValue == "Yes")
    //    {
    //        PublishData obj = new PublishData();
    //        IncentiveBusiness bus_obj = new IncentiveBusiness();
    //        obj.PaublicationID = TextBoxPubId.Text.Trim();
    //        obj.TypeOfEntry = DropDownListPublicationEntry.SelectedValue;
    //        bool flag = bus_obj.DiscardIncentivePointEntry(obj);
    //        if (flag == true)
    //        {
    //            string CloseWindow1 = "alert('Article has been discarded for incetive point entry')";
    //            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow1, true);
    //            btnApprove.Enabled = false;
    //            btnSave.Enabled = false;
    //            BindPublcationGrid();
    //            panelJournalArticle.Visible = false;
    //            panAddAuthor.Visible = false;
    //            PnlPublicationDetails.Visible = false;
    //            btnApprove.Visible = false;
    //            btnSave.Visible = false;
    //            btnDiscard.Visible = false;
    //            PnlPatentDetails.Visible = false;
    //        }
    //        else
    //        {
    //            string CloseWindow1 = "alert('problem while discarding')";
    //            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow1, true);
    //            btnApprove.Enabled = true;
    //            btnSave.Enabled = true;
    //            btnApprove.Visible = true;
    //            btnDiscard.Visible = true;
    //            btnSave.Visible = true;

    //        }
    //    }
    //    else
    //    {
    //    }

    //}

    private void SendMail()
    {
        EmailDetails details = new EmailDetails();
        try
        {
            bool resultv = false;
            int rowIndex = 0;
            details.Module = "IPR";
            details.EmailSubject = "Publication Cancelled & Points withdrawn";
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
            if (dtCurrentTable.Rows.Count > 0)
            {
                for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                {
                    IncentiveBusiness b = new IncentiveBusiness();

                    SendMailObject obj = new SendMailObject();
                    Business e = new Business();

                    //DropDownList DropdownType = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("DropdownMuNonMu");
                    //TextBox EmployeeCode = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("EmployeeCode");
                    //TextBox Author = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("AuthorName");
                    //TextBox total = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("txtTotalPoint");


                    DropDownList DropdownMuNonMu = (DropDownList)GridViewIncentivePints.Rows[rowIndex].Cells[0].FindControl("DropdownMuNonMu");
                    TextBox EmployeeCode = (TextBox)GridViewIncentivePints.Rows[rowIndex].Cells[0].FindControl("EmployeeCode");
                    TextBox Author= (TextBox)GridViewIncentivePints.Rows[rowIndex].Cells[0].FindControl("AuthorName");
                    TextBox OldCurrentBalance = (TextBox)GridViewIncentivePints.Rows[rowIndex].Cells[0].FindControl("txtOldCurrentBalance");
                    TextBox RevertingPoint = (TextBox)GridViewIncentivePints.Rows[rowIndex].Cells[0].FindControl("txtRevertingPoint");
                    TextBox NewCurrentBalance = (TextBox)GridViewIncentivePints.Rows[rowIndex].Cells[0].FindControl("txtNewcurrentBalance");
                    TextBox AdditionalPoint = (TextBox)GridViewIncentivePints.Rows[rowIndex].Cells[0].FindControl("txtAdditionalPoint");
                    TextBox UpdatedAdditionalPoint = (TextBox)GridViewIncentivePints.Rows[rowIndex].Cells[0].FindControl("txtUpdatedAdditionalPoint");
                    string empcode = EmployeeCode.Text;
                    string emailid = null;
                    double Revertpoint = Convert.ToDouble(RevertingPoint.Text);
                    double additionalpoint = Convert.ToDouble(AdditionalPoint.Text);
                    double total1 = Revertpoint + additionalpoint;
                    string total = Convert.ToString(total1);
                    if (total != "0")
                    {
                      
                        if (DropdownMuNonMu.SelectedValue == "M")
                        {
                            emailid = b.SelectAuthorEmailId(empcode);
                            if (emailid == "")
                            {
                                int result;
                                string AuthorName;
                                AuthorName = b.SelectAuthorName(empcode);
                                result = e.insertEmailtrackerIncentive(AuthorName, details, TextBoxPubId.Text);
                                Session["AuthorName"] = AuthorName;
                            }
                        }
                        else
                        {
                            emailid = b.SelectStudentEmailId(empcode, DropDownListPublicationEntry.SelectedValue + TextBoxPubId.Text);
                            if (emailid == "")
                            {
                                int result;
                                string AuthorName;
                                AuthorName = b.SelectStudentAuthorName(empcode, DropDownListPublicationEntry.SelectedValue + TextBoxPubId.Text);
                                result = e.insertEmailtrackerIncentive(AuthorName, details, TextBoxPubId.Text);
                                Session["AuthorName"] = AuthorName;
                            }
                        }

                        details.FromEmail = ConfigurationManager.AppSettings["FromAddress"].ToString();

                        if (emailid != "")
                        {
                            details.ToEmail = emailid;
                        }
                        string ToIPRMailID = ConfigurationManager.AppSettings["ToIPRMailID"].ToString();
                        if (details.CCEmail != "")
                        {
                            details.CCEmail = ToIPRMailID;
                        }
                        //string hremailid = b.SelectHRMailID(empcode, DropdownMuNonMu.SelectedValue, DropDownListPublicationEntry.SelectedValue + TextBoxPubId.Text);

                        //string InstWiseHRMailid = b.SelectInstwiseHRMailid(empcode, DropdownMuNonMu.SelectedValue, DropDownListPublicationEntry.SelectedValue + TextBoxPubId.Text);
                        //if (InstWiseHRMailid != null)
                        //{
                        //    if (InstWiseHRMailid != "")
                        //    {
                        //        if (details.ToEmail != null)
                        //        {
                        //            details.ToEmail = details.ToEmail + "," + InstWiseHRMailid;
                        //        }
                        //        else
                        //        {
                        //            details.ToEmail = InstWiseHRMailid;


                        //        }
                        //    }
                        //    else
                        //    {
                        //        // details.ToEmail = details.ToEmail;
                        //    }
                        //}
                        //else
                        //{
                        //    // details.ToEmail = details.ToEmail;
                        //}
                        //ArrayList list = new ArrayList();
                        //list = b.SelectHODMailid(empcode, DropdownMuNonMu.SelectedValue, DropDownListPublicationEntry.SelectedValue + TextBoxPubId.Text);
                        //for (int j = 0; j < list.Count; j++)
                        //{
                        //    if (j == 0)
                        //    {
                        //        if (list[j].ToString() != "")
                        //        {
                        //            details.HODmailid = list[j].ToString();
                        //        }
                        //    }
                        //    else
                        //    {
                        //        if (list[j].ToString() != "")
                        //        {
                        //            details.HODmailid = details.HODmailid + ',' + list[j].ToString();
                        //        }
                        //    }

                        //}

                        //if (hremailid != null)
                        //{
                        //    if (hremailid != "")
                        //    {
                        //        details.ToEmail = details.ToEmail + "," + hremailid;
                        //    }
                        //    else
                        //    {
                        //        // details.ToEmail = details.ToEmail;
                        //    }
                        //}
                        //else
                        //{
                        //    // details.ToEmail = details.ToEmail;
                        //}
                        //if (details.HODmailid != null)
                        //{
                        //    if (details.HODmailid != "")
                        //    {
                        //        details.ToEmail = details.ToEmail + "," + details.HODmailid;
                        //        //details.ToEmail = details.ToEmail + "," + hremailid + "," + details.HODmailid;
                        //    }
                        //    else
                        //    {
                        //        //details.ToEmail = details.ToEmail;
                        //    }
                        //}
                        //else
                        //{
                        //    //details.ToEmail = details.ToEmail;
                        //}
                        // details.CCEmail = hremailid;
                        // details.CCEmail = Session["emailId"].ToString();
                        details.Module = "IPR";
                        details.EmailSubject = "Publication Cancelled & Points withdrawn";
                        details.Type = DropDownListPublicationEntry.SelectedValue;
                        details.Id = TextBoxPubId.Text;
                        string FooterText = ConfigurationManager.AppSettings["FooterText"].ToString();
                        string isStudent = Session["IsStudent"].ToString();
                      
                        if (isStudent == "Y")
                        {
                            if (DropdownMuNonMu.SelectedValue == "S")
                            {
                                details.MsgBody = "<span style=\"font-size: 10pt; color: #3300cc; font-family: Verdana\"><h4>Dear Sir/Madam,</h4> <br>" +
                                    "<b> The following publication has been cancelled.<br>  "+                                    
                                         "<br>" +
                                            "Author Name : " + Author.Text + "<br>" +
                                         "Publication Id : " + TextBoxPubId.Text + "<br>" +
                                        "Article Name  :  " + txtboxTitleOfWrkItem.Text + "<br>" +
                                        "Withdrawn Points  :  " + total + "<br>" +
                                        "Current Balance:" + NewCurrentBalance.Text + "<br>" +
                                        "Remarks  :  " + txtcancelRemarks.Text + "<br>" + "<br>" + FooterText +
                                        " </b><br><b> </b></span>";
                            }
                            else
                            {
                                details.MsgBody = "<span style=\"font-size: 10pt; color: #3300cc; font-family: Verdana\"><h4>Dear Sir/Madam,</h4> <br>" +
                                         "<b> The following publication has been cancelled.<br>  " + 
                                         "<br>" +
                                            "Author Name : " + Author.Text + "<br>" +
                                         "Publication Id : " + TextBoxPubId.Text + "<br>" +
                                        "Article Name  :  " + txtboxTitleOfWrkItem.Text + "<br>" +
                                        "Withdrawn Points  :  " + total + "<br>" +
                                        "Current Balance:" + NewCurrentBalance.Text + "<br>" +
                                          "Remarks  :  " + txtcancelRemarks.Text + "<br>" + "<br>" + FooterText +
                                        " </b><br><b> </b></span>";
                            }
                        }

                        else
                        {
                            if (DropdownMuNonMu.SelectedValue == "S")
                            {
                                details.MsgBody = "<span style=\"font-size: 10pt; color: #3300cc; font-family: Verdana\"><h4>Dear Sir/Madam,</h4> <br>" +
                                           "<b> The following publication has been cancelled.<br>  "+
                                         "<br>" +
                                            "Author Name : " + Author.Text + "<br>" +
                                         "Publication Id : " + TextBoxPubId.Text + "<br>" +
                                        "Article Name  :  " + txtboxTitleOfWrkItem.Text + "<br>" +
                                        "Withdrawn Points  :  " + total + "<br>" +
                                        "Current Balance:" + NewCurrentBalance.Text + "<br>" +
                                        "Remarks  :  " + txtcancelRemarks.Text + "<br>" + "<br>" + FooterText +
                                        " </b><br><b> </b></span>";
                            }
                            else
                            {
                                details.MsgBody = "<span style=\"font-size: 10pt; color: #3300cc; font-family: Verdana\"><h4>Dear Sir/Madam,</h4> <br>" +
                                        "<b> The following publication has been cancelled.<br>  " +
                                        "<br>" +
                                           "Author Name : " + Author.Text + "<br>" +
                                        "Publication Id : " + TextBoxPubId.Text + "<br>" +
                                       "Article Name  :  " + txtboxTitleOfWrkItem.Text + "<br>" +
                                       "Withdrawn Points  :  " + total + "<br>" +
                                        "Current Balance:" + NewCurrentBalance.Text + "<br>" +
                                        "Remarks  :  " + txtcancelRemarks.Text + "<br>" + "<br>" + FooterText +
                                       " </b><br><b> </b></span>";
                            }
                        }

                        if (emailid!="")
                        {

                        resultv = obj.InsertIntoEmailQueue(details);

                        }
                        IncentiveData obj3 = new IncentiveData();
                        IncentiveBusiness C = new IncentiveBusiness();


                        if (emailid == "")
                        {
                            string AuthorName1 = Session["AuthorName"].ToString();
                            obj3 = C.CheckUniqueIdIncentive(TextBoxPubId.Text, DropDownListPublicationEntry.SelectedValue, details);
                            int data = C.updateEmailtrackerIncentive(TextBoxPubId.Text, DropDownListPublicationEntry.SelectedValue, details, obj3, AuthorName1);
                        }
                    }
                    rowIndex++;
                }

            }
            if (resultv == true)
            {

                string CloseWindow1 = "alert('Mail Sent successfully')";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "newWindow", CloseWindow1, true);
              
                btnSave.Enabled = false;
            }
            else
            {

                string CloseWindow1 = "alert('Problem while sending mail')";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "newWindow", CloseWindow1, true);
              
                btnSave.Enabled = false;
            }
        }
        catch (Exception ex)
        {
            string CloseWindow1 = "alert('Problem while sending mail')";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "newWindow", CloseWindow1, true);
            log.Error(ex.Message);
            log.Error(ex.StackTrace);
            btnSave.Enabled = false;
        }
    }




}