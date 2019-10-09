using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Incentive_SNIPSJREntry : System.Web.UI.Page
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
        btnSave.Visible = false;
        btnSave.Visible = false;
        PnlPublicationDetails.Style.Add("display", "none");
        panelJournalArticle.Style.Add("display", "none");
        panAddAuthor.Visible = false;
        if (ChkTypeOfPublication.SelectedValue == "1")
        {
            PanelPatentSearch.Style.Add("display", "none");
            panelSearchPub.Style.Add("display", "true");
            panelJournalArticle.Style.Add("display", "none");
            PnlPatentDetails.Style.Add("display", "none");
            BindPublcationGrid();
        }
        else
        {
            BindPatentGrid();
            PanelPatentSearch.Style.Add("display", "true");
            panelSearchPub.Style.Add("display", "none");
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
        ds = obj.SelectApprovedIncentivePublications(data);
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
        //btnApprove.Visible = false;
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

    }


    private void fnRecordExist(object sender, GridViewEditEventArgs e)
    {
        addclik(sender, e);
        string Pid = Session["TempPid"].ToString();
        string TypeEntry = Session["TempTypeEntry"].ToString();
        panelJournalArticle.Style.Add("display", "true");
        PnlPublicationDetails.Visible = true;
        PnlPublicationDetails.Style.Add("display", "true");
        IncentiveBusiness ince_obj = new IncentiveBusiness();
        Business obj = new Business();
        PublishData v = new PublishData();
        btnSave.Visible = true;
        //btnApprove.Visible = true;
        //btnDiscard.Visible = true;
        btnSave.Enabled = true;
        //btnApprove.Enabled = true;
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
        DropDownListMonthJA.SelectedValue = v.PublishJAMonth.ToString();
        uploadeprint.SelectedValue = v.uploadEPrint;
        if (v.uploadEPrint == "Y")
        {
            lblNote.Visible = false;
            //btnApprove.Enabled = true;
            btnSave.Enabled = true;
        }
        else
        {
            lblNote.Text = "Note: The article is not uploaded to e-Print. Points entry can be done only after uploading to e-Print.";
            lblNote.Visible = true;
            //btnApprove.Enabled = false;
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
        TextBoxYearJA.SelectedValue = v.PublishJAYear;

        int pubmonth = Convert.ToInt32(v.PublishJAMonth.ToString());
        int pubyear = Convert.ToInt32(TextBoxYearJA.SelectedValue);
        if (pubyear >= 2018 && pubmonth >= 7)
           
        {
            Grid_AuthorEntry.Enabled = false;
            lblnoteQuartile.Text = "Note: Post July 2018 Papers are not applicable for SNIP point entry ..";
            lblnoteQuartile.Visible = true;
            btnSave.Enabled = false;
        }
        else if (pubyear >= 2019 && pubmonth >= 1)
        {
            Grid_AuthorEntry.Enabled = false;
            lblnoteQuartile.Text = "Note: Post July 2018 Papers are not applicable for SNIP point entry ..";
            lblnoteQuartile.Visible = true;
            btnSave.Enabled = false;
        }
        else 
        {
            Grid_AuthorEntry.Enabled = true;
            btnSave.Enabled = true;
            lblnoteQuartile.Visible = false;
        }
        TextBoxImpFact.Text = v.ImpactFactor;
        TextBoxImpFact5.Text = v.ImpactFactor5;
        if (v.IFApplicableYear != 0)
        {
            txtIFApplicableYear.Text = v.IFApplicableYear.ToString();
        }

        IncentivePoint incentive = new IncentivePoint();
        incentive = ince_obj.SelectSNIPJRPoint(v.PublisherOfJournal, v.PublishJAYear);
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
                HiddenField EmployeeCode = (HiddenField)Grid_AuthorEntry.Rows[rowIndex].Cells[2].FindControl("EmployeeCode");
                TextBox AuthorName = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[2].FindControl("AuthorName");
                TextBox InstNme = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[2].FindControl("InstitutionName");
                TextBox deptname = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[2].FindControl("DepartmentName");
                DropDownList isCorrAuth = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[2].FindControl("isCorrAuth");
                DropDownList AuthorType = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[2].FindControl("AuthorType");
                DropDownList DropdownMuNonMu1 = (DropDownList)Grid_AuthorEntry.Rows[0].Cells[3].FindControl("DropdownMuNonMu");
                DropDownList DropdownStudentInstitutionName = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[2].FindControl("DropdownStudentInstitutionName");
                DropDownList DropdownStudentDepartmentName = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[2].FindControl("DropdownStudentDepartmentName");

                TextBox total = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[2].FindControl("txtTotalPoint");
                //TextBox basepoint = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[2].FindControl("txtBasePoint");
                TextBox snipsjrpoint = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[2].FindControl("txtSNIPSJRPoint");
                //TextBox thresholdpoint = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[2].FindControl("txtThresholdPoint"); //point 3 (crosses 6 publication) is awarded once in a year

                Grid_AuthorEntry.Columns[5].Visible = true;
                Grid_AuthorEntry.Columns[6].Visible = true;
                total.Enabled = false;
                Grid_AuthorEntry.Columns[8].Visible = true;
                Grid_AuthorEntry.Columns[9].Visible = true;
                //Grid_AuthorEntry.Columns[10].Visible = true; //point 3 (crosses 6 publication) is awarded once in a year

                drCurrentRow = dtCurrentTable.NewRow();
                if (total.Text != null)
                {
                    string a = "";
                    a = dtCurrentTable.Rows[i - 1]["TotalPoint"].ToString();
                }
                //if (total.Text != "")
                //    total.Text = (Convert.ToDouble(snipsjrpoint.Text)).ToString("0.00");
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
                EmployeeCode.Value = dtCurrentTable.Rows[i - 1]["EmployeeCode"].ToString();
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
                publicationobj.PublishJAYear = TextBoxYearJA.SelectedValue;
                publicationobj.EmployeeCode = dtCurrentTable.Rows[i - 1]["EmployeeCode"].ToString();


                int applicableyear = Convert.ToInt16(ConfigurationManager.AppSettings["IncentivePointApplicableYear"]);
                if (Convert.ToInt16(v.PublishJAYear) >= applicableyear)
                {
                    //basepoint.Text = dtCurrentTable.Rows[i - 1]["BasePoint"].ToString();
                    snipsjrpoint.Text = dtCurrentTable.Rows[i - 1]["SNIPSJRPoint"].ToString();
                    //if (dtCurrentTable.Rows[i - 1]["ThresholdPoint"].ToString() != "0")
                    //{
                    //    thresholdpoint.Text = dtCurrentTable.Rows[i - 1]["ThresholdPoint"].ToString(); //point 3 (crosses 6 publication) is awarded once in a year
                    //}

                    int ThresholdPublicationNo = Convert.ToInt16(ConfigurationManager.AppSettings["ThresholdPublicationNo"]);

                    AuthorType.Text = dtCurrentTable.Rows[i - 1]["AuthorType"].ToString();
                    isCorrAuth.Text = dtCurrentTable.Rows[i - 1]["isCorrAuth"].ToString();
                    if (AuthorType.SelectedValue == "P" || isCorrAuth.SelectedValue == "Y")
                    {
                        snipsjrpoint.Enabled = true;
                    }
                    else
                    {
                        snipsjrpoint.Enabled = true;
                        snipsjrpoint.Text = "";
                    }

                    IncentiveBusiness busobj = new IncentiveBusiness();
                    int count = busobj.CountThresholdPublicationPoint(publicationobj);
                    if (count > ThresholdPublicationNo)
                    {
                        if (DropdownMuNonMu.SelectedValue == "M")
                        {
                            // thresholdpoint.Enabled = true; //point 3 (crosses 6 publication) is awarded once in a year
                        }
                        else
                        {
                            //thresholdpoint.Enabled = false;  //point 3 (crosses 6 publication) is awarded once in a year
                        }
                    }
                    else
                    {
                        // thresholdpoint.Enabled = false;
                        //thresholdpoint.Text = "";
                    }
                }
                else
                {
                    AuthorType.Text = dtCurrentTable.Rows[i - 1]["AuthorType"].ToString();
                    isCorrAuth.Text = dtCurrentTable.Rows[i - 1]["isCorrAuth"].ToString();
                    //basepoint.Text = dtCurrentTable.Rows[i - 1]["BasePoint"].ToString();
                    //basepoint.Enabled = false;
                    snipsjrpoint.Enabled = true;
                    //thresholdpoint.Enabled = false; //point 3 (crosses 6 publication) is awarded once in a year
                }
                rowIndex++;
            }


            ViewState["CurrentTable"] = dtCurrentTable;
        }
        if (Convert.ToInt32(v.PublishJAYear) >= applicableyear1 && uploadeprint.SelectedValue == "Y" && v.MUCategorization != "BK")
        {
            for (int j = 0; j < Grid_AuthorEntry.Rows.Count; j++)
            {

                //TextBox BasePoint = (TextBox)Grid_AuthorEntry.Rows[j].Cells[2].FindControl("txtBasePoint");
                TextBox SNIPSJRPoint = (TextBox)Grid_AuthorEntry.Rows[j].Cells[2].FindControl("txtSNIPSJRPoint");
                TextBox TotalPoint = (TextBox)Grid_AuthorEntry.Rows[j].Cells[2].FindControl("txtTotalPoint");
                if (TotalPoint.Text == "")
                {
                    IncentivePointAutomation(Pid, TypeEntry, v);
                }
            }
        }
    }


    private void IncentivePointAutomation(string Pid, string TypeEntry, PublishData v)
    {
        IncentiveBusiness b = new IncentiveBusiness();
        DataSet ds = new DataSet();

        string pubmonth = v.PublishJAMonth.ToString();
        string pubyear = v.PublishJAYear;
        string impactfactor = v.ImpactFactor;
        if (impactfactor == "")
        {
            impactfactor = "0.0";
        }

        ArrayList list = new ArrayList();
        list = b.SelectImpactFactor(v);
        if (list.Count > 1)
        {
            for (int i = 0; i < list.Count; i++)
            {
                double limit = Convert.ToDouble(list[i]);
                double limit2 = Convert.ToDouble(list[i + 1]);

                if (Convert.ToDouble(impactfactor) >= limit && Convert.ToDouble(impactfactor) <= limit2)
                {
                    v.ImpactFactor = limit2.ToString();
                    ds = b.SelectIncentivePoints(v);
                    break;
                }
                else
                {
                    continue;
                }
            }
            double faculty = 0, student = 0;
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            for (int k = 1; k <= dt.Rows.Count; k++)
            {
                string AuthorType1 = dt.Rows[k - 1]["AuthorType"].ToString();
                string isCorrAuth = dt.Rows[k - 1]["isCorrAuth"].ToString();
                string munonmu = dt.Rows[k - 1]["DropdownMuNonMu"].ToString();
                if (munonmu == "M")
                {
                    if (AuthorType1 == "C" && isCorrAuth == "N")
                    {
                        faculty++;

                    }
                }
                else if (munonmu == "S" || munonmu == "O")
                {
                    if (AuthorType1 == "C" && isCorrAuth == "N")
                    {
                        student++;
                    }
                }
            }

             if (dt.Rows.Count > 0)
            {
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    DropDownList isCorrAuth = (DropDownList)Grid_AuthorEntry.Rows[j].Cells[2].FindControl("isCorrAuth");
                    DropDownList AuthorType = (DropDownList)Grid_AuthorEntry.Rows[j].Cells[2].FindControl("AuthorType");
                    DropDownList DropdownMuNonMu = (DropDownList)Grid_AuthorEntry.Rows[j].Cells[2].FindControl("DropdownMuNonMu");
                    //TextBox BasePoint = (TextBox)Grid_AuthorEntry.Rows[j].Cells[2].FindControl("txtBasePoint");
                    TextBox SNIPSJRPoint = (TextBox)Grid_AuthorEntry.Rows[j].Cells[2].FindControl("txtSNIPSJRPoint");
                    TextBox TotalPoint = (TextBox)Grid_AuthorEntry.Rows[j].Cells[2].FindControl("txtTotalPoint");
                    if (DropdownMuNonMu.SelectedValue == "M" || DropdownMuNonMu.SelectedValue == "S" || DropdownMuNonMu.SelectedValue == "O")
                    {
                        if (AuthorType.SelectedValue == "P" && isCorrAuth.SelectedValue == "Y")
                        {
                            string firstauthor = ds.Tables[0].Rows[0]["FirstAuthor"].ToString();
                            //BasePoint.Text = (Convert.ToDouble(firstauthor)).ToString("0.00");
                        }
                        else if (AuthorType.SelectedValue == "P")
                        {
                            string firstauthor = ds.Tables[0].Rows[0]["FirstAuthor"].ToString();
                            //BasePoint.Text = (Convert.ToDouble(firstauthor)).ToString("0.00");
                        }
                        else if (isCorrAuth.SelectedValue == "Y")
                        {
                            string corresauthor = ds.Tables[0].Rows[0]["CorresAuthor"].ToString();
                            //BasePoint.Text = (Convert.ToDouble(corresauthor)).ToString("0.00");
                        }
                        else if (AuthorType.SelectedValue == "C")
                        {
                            if (DropdownMuNonMu.SelectedValue == "M")
                            {
                                string facultycoAuthor = ds.Tables[0].Rows[0]["FacultyCoAuthors"].ToString();
                                double f_author = Convert.ToDouble(facultycoAuthor) / Convert.ToDouble(faculty);
                                //BasePoint.Text = f_author.ToString("0.00");
                            }
                            else
                            {
                                string studentcoAuthors = ds.Tables[0].Rows[0]["StudentCoAuthors"].ToString();
                                double s_author = Convert.ToDouble(studentcoAuthors) / Convert.ToDouble(student);
                                //BasePoint.Text = s_author.ToString("0.00");
                            }
                        }
                        if (SNIPSJRPoint.Text != "")
                            TotalPoint.Text = (Convert.ToDouble(SNIPSJRPoint.Text)).ToString("0.00");
                    }
                    else
                    {
                    }
                }
            }
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
            IncentiveBusiness obj = new IncentiveBusiness();

            int rowIndex1 = 0;
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
            PublishData[] JD = new PublishData[dtCurrentTable.Rows.Count];
            PublishData data = new PublishData();

            if (ChkTypeOfPublication.SelectedValue == "1")
            {
                data.PaublicationID = TextBoxPubId.Text.Trim();
                data.TypeOfEntry = DropDownListPublicationEntry.SelectedValue;
            }
            else
            {
                data.PaublicationID = txtID.Text.Trim();
            }
            if (dtCurrentTable.Rows.Count > 0)
            {

                for (int i = 0; i < dtCurrentTable.Rows.Count; i++)
                {
                    JD[i] = new PublishData();

                    TextBox AuthorName = (TextBox)Grid_AuthorEntry.Rows[rowIndex1].Cells[1].FindControl("AuthorName");
                    DropDownList DropdownMuNonMu = (DropDownList)Grid_AuthorEntry.Rows[rowIndex1].Cells[2].FindControl("DropdownMuNonMu");
                    HiddenField EmployeeCode = (HiddenField)Grid_AuthorEntry.Rows[rowIndex1].Cells[0].FindControl("EmployeeCode");
                    TextBox DepartmentName = (TextBox)Grid_AuthorEntry.Rows[rowIndex1].Cells[0].FindControl("DepartmentName");
                    TextBox InstitutionName = (TextBox)Grid_AuthorEntry.Rows[rowIndex1].Cells[6].FindControl("InstitutionName");

                    DropDownList DropdownStudentInstitutionName = (DropDownList)Grid_AuthorEntry.Rows[rowIndex1].Cells[0].FindControl("DropdownStudentInstitutionName");
                    DropDownList DropdownStudentDepartmentName = (DropDownList)Grid_AuthorEntry.Rows[rowIndex1].Cells[0].FindControl("DropdownStudentDepartmentName");

                    TextBox TotalPoint = (TextBox)Grid_AuthorEntry.Rows[rowIndex1].Cells[0].FindControl("txtTotalPoint");
                    //TextBox BasePoint = (TextBox)Grid_AuthorEntry.Rows[rowIndex1].Cells[6].FindControl("txtBasePoint");
                    TextBox SNIPSJRPoint = (TextBox)Grid_AuthorEntry.Rows[rowIndex1].Cells[0].FindControl("txtSNIPSJRPoint");
                    //TextBox ThresholdPoint = (TextBox)Grid_AuthorEntry.Rows[rowIndex1].Cells[6].FindControl("txtThresholdPoint"); //point 3 (crosses 6 publication) is awarded once in a year


                    JD[i].AuthorName = AuthorName.Text.Trim();
                    JD[i].MUNonMU = DropdownMuNonMu.Text.Trim();
                    JD[i].EmployeeCode = EmployeeCode.Value;
                    if (ChkTypeOfPublication.SelectedValue == "1")
                    {
                        //if (BasePoint.Text != "")
                        //{
                        //    JD[i].BasePoint = Convert.ToDouble(BasePoint.Text);
                        //}
                        //else
                        //{
                        //    JD[i].BasePoint = 0.0;
                        //}
                        if (SNIPSJRPoint.Text != "")
                        {
                            JD[i].SNIPSJRPoint = Convert.ToDouble(SNIPSJRPoint.Text);
                        }
                        else
                        {
                            JD[i].SNIPSJRPoint = 0.0;
                        }

                        //point 3 (crosses 6 publication) is awarded once in a year
                        //if (ThresholdPoint.Text != "")
                        //{
                        //    JD[i].ThresholdPoint = Convert.ToDouble(ThresholdPoint.Text);
                        //}
                        //else
                        //{
                        //    JD[i].ThresholdPoint = 0.0;
                        //}
                        JD[i].ThresholdPoint = 0.0;
                        JD[i].TotalPoint = Convert.ToDouble(JD[i].SNIPSJRPoint + JD[i].ThresholdPoint);
                        // JD[i].TotalPoint = Math.Round(total, 2);
                    }
                    else
                    {
                        JD[i].TotalPoint = Convert.ToDouble(TotalPoint.Text);
                    }
                    JD[i].TransactionType = "SJR";
                    rowIndex1++;

                }

                bool result = false;
                if (ChkTypeOfPublication.SelectedValue == "1")
                {
                    //publication Points
                    result = obj.InsertSJRPointToAuthor(JD, data);
                }
                else
                {
                    //Patent Points
                    result = obj.InsertIncentivePointToPatentAuthor(JD, data);
                }
                if (result == true)
                {
                    string CloseWindow1 = "alert('SNIP/SJR Point Saved successfully')";
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow1, true);
                    btnSave.Enabled = false;
                    SendMail();
                }
                else
                {
                    string CloseWindow1 = "alert('problem while saving')";
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow1, true);
                    //btnApprove.Enabled = false;
                }

            }
        }
        catch (Exception ex)
        {
            string CloseWindow1 = "alert('problem while saving points')";
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow1, true);
            //btnApprove.Enabled = false;
            btnSave.Enabled = true;
            log.Error(ex.Message);
            log.Error(ex.StackTrace);
        }
    }

    //protected void BtnApprove_Click(object sender, EventArgs e)
    //{
    //    if (!Page.IsValid)
    //    {
    //        return;
    //    }
    //    try
    //    {
    //        IncentiveBusiness obj = new IncentiveBusiness();

    //        int rowIndex1 = 0;
    //        DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
    //        PublishData[] JD = new PublishData[dtCurrentTable.Rows.Count];
    //        PublishData data = new PublishData();
    //        data.PaublicationID = TextBoxPubId.Text.Trim();
    //        data.TypeOfEntry = DropDownListPublicationEntry.SelectedValue;
    //        if (dtCurrentTable.Rows.Count > 0)
    //        {

    //            for (int i = 0; i < dtCurrentTable.Rows.Count; i++)
    //            {
    //                JD[i] = new PublishData();

    //                TextBox AuthorName = (TextBox)Grid_AuthorEntry.Rows[rowIndex1].Cells[1].FindControl("AuthorName");
    //                DropDownList DropdownMuNonMu = (DropDownList)Grid_AuthorEntry.Rows[rowIndex1].Cells[2].FindControl("DropdownMuNonMu");
    //                HiddenField EmployeeCode = (HiddenField)Grid_AuthorEntry.Rows[rowIndex1].Cells[0].FindControl("EmployeeCode");
    //                TextBox DepartmentName = (TextBox)Grid_AuthorEntry.Rows[rowIndex1].Cells[0].FindControl("DepartmentName");
    //                TextBox InstitutionName = (TextBox)Grid_AuthorEntry.Rows[rowIndex1].Cells[6].FindControl("InstitutionName");

    //                DropDownList DropdownStudentInstitutionName = (DropDownList)Grid_AuthorEntry.Rows[rowIndex1].Cells[0].FindControl("DropdownStudentInstitutionName");
    //                DropDownList DropdownStudentDepartmentName = (DropDownList)Grid_AuthorEntry.Rows[rowIndex1].Cells[0].FindControl("DropdownStudentDepartmentName");

    //                TextBox TotalPoint = (TextBox)Grid_AuthorEntry.Rows[rowIndex1].Cells[0].FindControl("txtTotalPoint");
    //                //TextBox BasePoint = (TextBox)Grid_AuthorEntry.Rows[rowIndex1].Cells[6].FindControl("txtBasePoint");
    //                TextBox SNIPSJRPoint = (TextBox)Grid_AuthorEntry.Rows[rowIndex1].Cells[0].FindControl("txtSNIPSJRPoint");
    //                TextBox ThresholdPoint = (TextBox)Grid_AuthorEntry.Rows[rowIndex1].Cells[6].FindControl("txtThresholdPoint");


    //                JD[i].AuthorName = AuthorName.Text.Trim();
    //                JD[i].MUNonMU = DropdownMuNonMu.Text.Trim();
    //                JD[i].EmployeeCode = EmployeeCode.Value;
    //                //JD[i].BasePoint = Convert.ToDouble(BasePoint.Text);
    //                //JD[i].SNIPSJRPoint = Convert.ToDouble(SNIPSJRPoint.Text);
    //                //JD[i].ThresholdPoint = Convert.ToDouble(ThresholdPoint.Text);
    //                if (ChkTypeOfPublication.SelectedValue == "1")
    //                {
    //                    //if (BasePoint.Text != "")
    //                    //{
    //                    //    JD[i].BasePoint = Convert.ToDouble(BasePoint.Text);
    //                    //}
    //                    //else
    //                    //{
    //                    //    JD[i].BasePoint = 0.0;
    //                    //}
    //                    if (SNIPSJRPoint.Text != "")
    //                    {
    //                        JD[i].SNIPSJRPoint = Convert.ToDouble(SNIPSJRPoint.Text);
    //                    }
    //                    else
    //                    {
    //                        JD[i].SNIPSJRPoint = 0.0;
    //                    }
    //                    //point 3 (crosses 6 publication) is awarded once in a year
    //                    //if (ThresholdPoint.Text != "")
    //                    //{
    //                    //    JD[i].ThresholdPoint = Convert.ToDouble(ThresholdPoint.Text);
    //                    //}
    //                    //else
    //                    //{
    //                    //    JD[i].ThresholdPoint = 0.0;
    //                    //}
    //                    JD[i].ThresholdPoint = 0.0;
    //                    JD[i].TotalPoint = JD[i].SNIPSJRPoint + JD[i].ThresholdPoint;
    //                }
    //                else
    //                {
    //                    JD[i].TotalPoint = Convert.ToDouble(TotalPoint.Text);
    //                }


    //                PublishData publicationobj = new PublishData();
    //                publicationobj.PaublicationID = TextBoxPubId.Text.Trim();
    //                publicationobj.TypeOfEntry = DropDownListPublicationEntry.SelectedValue;
    //                JD[i].PaublicationID = publicationobj.TypeOfEntry + publicationobj.PaublicationID;
    //                JD[i].PublishJAYear = TextBoxYearJA.SelectedValue;
    //                JD[i].TransactionType = "SJR";
    //                rowIndex1++;

    //            }

    //            bool result = false;
    //            if (ChkTypeOfPublication.SelectedValue == "1")
    //            {
    //                result = obj.ApproveIncentiveStatus(JD, data);
    //                SendMail();
    //            }
    //            else
    //            {
    //                result = obj.ApprovePatenIncentiveStatus(JD, data);
    //            }
    //            if (result == true)
    //            {
    //                string CloseWindow1 = "alert('Incentive Point Approved successfully')";
    //                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow1, true);
    //                btnApprove.Enabled = false;
    //                btnSave.Enabled = false;
    //                BindPublcationGrid();
    //                panelJournalArticle.Visible = false;
    //                panAddAuthor.Visible = false;
    //                PnlPublicationDetails.Visible = false;
    //                btnApprove.Visible = false;
    //                btnSave.Visible = false;
    //                PnlPatentDetails.Visible = false;
    //                //btnDiscard.Visible = false;
    //            }
    //            else
    //            {
    //                string CloseWindow1 = "alert('problem while approving')";
    //                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow1, true);
    //                btnApprove.Enabled = true;
    //                btnSave.Enabled = true;
    //                btnApprove.Visible = true;
    //                btnSave.Visible = true;
    //                //btnDiscard.Visible = true;

    //            }
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        if (ex.Message.Contains("IX_Member_Incentive_Point_Transaction_1"))
    //        {
    //            string CloseWindow1 = "alert('Data has been already approved')";
    //            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow1, true);
    //        }
    //        else
    //        {
    //            string CloseWindow1 = "alert('problem while approving')";
    //            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow1, true);
    //        }
    //        btnApprove.Enabled = true;
    //        btnSave.Enabled = true;
    //        btnApprove.Visible = true;
    //        btnSave.Visible = true;
    //        log.Error(ex.Message);
    //        log.Error(ex.StackTrace);
    //    }
    //}


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

    }

    private void BindPatentGrid()
    {
        if (PatIDSearch.Text == "" && TextBoxtiltleSearch.Text == "")
        {
            SqlDataSource1.SelectCommand = " select p.ID,p.Title,p.Entry_Status,s.StatusName as Filling_Status from Patent_Data p,Patent_Status s where p.Filing_Status=s.Id and (IncentivePointStatus='PEN' or IncentivePointStatus='PRC') ";
        }
        else if (PatIDSearch.Text != "" && TextBoxtiltleSearch.Text == "")
        {
            SqlDataSource1.SelectParameters.Clear();
            SqlDataSource1.SelectParameters.Add("ID", PatIDSearch.Text);

            SqlDataSource1.SelectCommand = " select p.ID,p.Title,p.Entry_Status,s.StatusName as Filling_Status from Patent_Data p,Patent_Status s where p.Filing_Status=s.Id and p.ID like '%' + @ID + '%'  and (IncentivePointStatus='PEN' or IncentivePointStatus='PRC') ";
        }

        else if (PatIDSearch.Text != "" && TextBoxtiltleSearch.Text != "")
        {
            SqlDataSource1.SelectParameters.Clear();
            SqlDataSource1.SelectParameters.Add("Title", TextBoxtiltleSearch.Text.Trim());
            SqlDataSource1.SelectCommand = "  select p.ID,p.Title,p.Entry_Status,s.StatusName as Filling_Status from Patent_Data p,Patent_Status s where p.Filing_Status=s.Id and p.Title  LIKE  '%' + @Title + '%' and (IncentivePointStatus='PEN' or IncentivePointStatus='PRC')  ";
        }
        else
        {
            SqlDataSource1.SelectParameters.Clear();
            SqlDataSource1.SelectParameters.Add("ID", PatIDSearch.Text);
            SqlDataSource1.SelectParameters.Add("Title", TextBoxtiltleSearch.Text.Trim());

            SqlDataSource1.SelectCommand = " select p.ID,p.Title,p.Entry_Status,s.StatusName as Filling_Status from Patent_Data p,Patent_Status s where p.Filing_Status=s.Id and p.ID  LIKE '%'' + @ID + ''%' and p.Title  LIKE  '%' + @Title + '%' and (IncentivePointStatus='PEN' or IncentivePointStatus='PRC')";

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
        GridViewSearchPatent.Visible = true;
        Pat = obj.SelectPatent(ID);
        txtID.Text = ID;
        txtPatUTN.Text = Pat.Pat_UTN;
        txtTitle.Text = Pat.Title;

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
                HiddenField EmployeeCode = (HiddenField)Grid_AuthorEntry.Rows[rowIndex].Cells[2].FindControl("EmployeeCode");
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
                TextBox thresholdpoint = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[2].FindControl("txtThresholdPoint");

                basepoint.Visible = false;
                snipsjrpoint.Visible = false;
                thresholdpoint.Visible = false;

                drCurrentRow = dtCurrentTable.NewRow();
                total.Text = dtCurrentTable.Rows[i - 1]["TotalPoint"].ToString();

                Grid_AuthorEntry.Columns[5].Visible = false;
                Grid_AuthorEntry.Columns[6].Visible = false;
                total.Enabled = true;
                Grid_AuthorEntry.Columns[8].Visible = false;
                Grid_AuthorEntry.Columns[9].Visible = false;
                Grid_AuthorEntry.Columns[10].Visible = false;

                DropdownMuNonMu.Text = dtCurrentTable.Rows[i - 1]["DropdownMuNonMu"].ToString();
                EmployeeCode.Value = dtCurrentTable.Rows[i - 1]["EmployeeCode"].ToString();
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
                else if (DropdownMuNonMu.Text == "S")
                {
                    if (DropDownListPublicationEntry.SelectedValue == "JA" || DropDownListPublicationEntry.SelectedValue == "TS")
                    {
                        DropdownStudentInstitutionName.Visible = false;
                        DropdownStudentDepartmentName.Visible = false;
                        InstNme.Visible = true;
                        deptname.Visible = true;
                        InstNme.Text = dtCurrentTable.Rows[i - 1]["InstitutionName"].ToString();
                        deptname.Text = dtCurrentTable.Rows[i - 1]["DepartmentName"].ToString();
                    }
                    else
                    {
                        DropdownStudentInstitutionName.Visible = true;
                        DropdownStudentDepartmentName.Visible = true;
                        InstNme.Visible = false;
                        deptname.Visible = false;
                        DropdownStudentInstitutionName.Text = dtCurrentTable.Rows[i - 1]["Institution"].ToString();
                        DropdownStudentDepartmentName.Text = dtCurrentTable.Rows[i - 1]["Department"].ToString();
                    }
                }

                rowIndex++;
            }

            ViewState["CurrentTable"] = dtCurrentTable;
        }
    }

    private void SetPanelVisibility()
    {
        btnSave.Visible = true;
        //btnApprove.Visible = true;
        if (ChkTypeOfPublication.SelectedValue == "1")
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
            details.Module = "SNIP";

            details.EmailSubject = "SNIP Point Entry";
            Business e = new Business();
            bool resultv = false;

            int rowIndex = 0;

            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];

            if (dtCurrentTable.Rows.Count > 0)
            {

                for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                {

                    IncentiveBusiness b = new IncentiveBusiness();



                    SendMailObject obj = new SendMailObject();



                    DropDownList DropdownType = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("DropdownMuNonMu");

                    HiddenField EmployeeCode = (HiddenField)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("EmployeeCode");

                    TextBox Author = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("AuthorName");

                    TextBox total = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("txtTotalPoint");

                    //  TextBox InstNme = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[2].FindControl("InstitutionName");



                    //DropDownList DropdownStudentInstitutionName = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[2].FindControl("DropdownStudentInstitutionName");

                    string empcode = EmployeeCode.Value;

                    string emailid = null;

                    if (total.Text != "0")
                    {



                        if (DropdownType.SelectedValue == "M")
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



                        details.ToEmail = emailid;

                        string hremailid = b.SelectHRMailID(empcode, DropdownType.SelectedValue, DropDownListPublicationEntry.SelectedValue + TextBoxPubId.Text);



                        string InstWiseHRMailid = b.SelectInstwiseHRMailid(empcode, DropdownType.SelectedValue, DropDownListPublicationEntry.SelectedValue + TextBoxPubId.Text);
                        if (details.ToEmail != null && details.ToEmail != "")
                        {
                            if (InstWiseHRMailid != null)
                            {
                                if (InstWiseHRMailid != "")
                                {
                                    details.ToEmail = details.ToEmail + "," + InstWiseHRMailid;
                                }
                                else
                                {
                                    // details.ToEmail = details.ToEmail;
                                }
                            }
                            else
                            {
                                // details.ToEmail = details.ToEmail;
                            }
                        }
                        else
                        {
                            if (InstWiseHRMailid != null)
                            {
                                if (InstWiseHRMailid != "")
                                {
                                    if (details.ToEmail != null)
                                    {
                                        details.ToEmail = InstWiseHRMailid;
                                    }
                                    else
                                    {
                                        details.ToEmail = InstWiseHRMailid;
                                    }
                                }
                                else
                                {
                                    // details.ToEmail = details.ToEmail;
                                }
                            }
                            else
                            {
                                // details.ToEmail = details.ToEmail;
                            }
                        }
                        ArrayList list = new ArrayList();

                        list = b.SelectHODMailid(empcode, DropdownType.SelectedValue, DropDownListPublicationEntry.SelectedValue + TextBoxPubId.Text);

                        for (int j = 0; j < list.Count; j++)
                        {
                            if (j == 0)
                            {
                                if (list[j].ToString() != "")
                                {
                                    details.HODmailid = list[j].ToString();
                                }
                            }
                            else
                            {
                                if (list[j].ToString() != "")
                                {
                                    details.HODmailid = details.HODmailid + ',' + list[j].ToString();
                                }

                            }
                        }

                        if (details.ToEmail != null && details.ToEmail != "")
                        {
                            if (hremailid != null)
                            {
                                if (hremailid != "")
                                {
                                    details.ToEmail = details.ToEmail + "," + hremailid;
                                }
                                else
                                {
                                    // details.ToEmail = details.ToEmail;
                                }
                            }
                            else
                            {
                                // details.ToEmail = details.ToEmail;
                            }
                        }
                        else
                        {
                            if (hremailid != null)
                            {
                                if (hremailid != "")
                                {
                                    details.ToEmail = hremailid;
                                }
                                else
                                {
                                    // details.ToEmail = details.ToEmail;
                                }
                            }
                            else
                            {
                                // details.ToEmail = details.ToEmail;
                            }
                        }
                        if (details.ToEmail != null && details.ToEmail != "")
                        {
                            if (details.HODmailid != null)
                            {
                                if (details.HODmailid != "")
                                {
                                    details.ToEmail = details.ToEmail + "," + details.HODmailid;
                                    //details.ToEmail = details.ToEmail + "," + hremailid + "," + details.HODmailid;
                                }
                                else
                                {
                                    //details.ToEmail = details.ToEmail;
                                }

                            }
                            else
                            {
                                //details.ToEmail = details.ToEmail;
                            }
                        }
                        else
                        {
                            if (details.HODmailid != null)
                            {
                                if (details.HODmailid != "")
                                {
                                    details.ToEmail = details.HODmailid;
                                    //details.ToEmail = details.ToEmail + "," + hremailid + "," + details.HODmailid;
                                }
                                else
                                {
                                    //details.ToEmail = details.ToEmail;
                                }
                            }
                            else
                            {
                                //details.ToEmail = details.ToEmail;
                            }
                        }
                        // details.CCEmail = hremailid;

                        // details.CCEmail = Session["emailId"].ToString();

                        details.Module = "SNIP";

                        details.EmailSubject = "SNIP Point Entry";

                        details.Type = DropDownListPublicationEntry.SelectedValue;

                        details.Id = TextBoxPubId.Text;

                        string FooterText = ConfigurationManager.AppSettings["FooterText"].ToString();

                        string isStudent = Session["IsStudent"].ToString();



                        if (isStudent == "Y")
                        {

                            if (DropdownType.SelectedValue == "S")
                            {

                                details.MsgBody = "<span style=\"font-size: 10pt; color: #3300cc; font-family: Verdana\"><h4>Dear Sir/Madam,</h4> <br>" +

                                         "<b> SNIP points with the rating '" + total.Text + "' added. <br> " +

                                         "<br>" +

                                            "Author Name : " + Author.Text + "<br>" +

                                         "Publication Id : " + TextBoxPubId.Text + "<br>" +

                                        "Article Name  :  " + txtboxTitleOfWrkItem.Text + "<br>" + "<br>" + FooterText +

                                        " </b><br><b> </b></span>";

                            }

                            else
                            {

                                details.MsgBody = "<span style=\"font-size: 10pt; color: #3300cc; font-family: Verdana\"><h4>Dear Sir/Madam,</h4> <br>" +

                                         "<b> SNIP points with the rating '" + total.Text + "' added. <br> " +

                                         "<br>" +

                                            "Author Name : " + Author.Text + "<br>" +

                                         "Publication Id : " + TextBoxPubId.Text + "<br>" +

                                        "Article Name  :  " + txtboxTitleOfWrkItem.Text + "<br>" + "<br>" + FooterText +

                                        " </b><br><b> </b></span>";

                            }

                        }



                        else
                        {

                            if (DropdownType.SelectedValue == "S")
                            {

                                details.MsgBody = "<span style=\"font-size: 10pt; color: #3300cc; font-family: Verdana\"><h4>Dear Sir/Madam,</h4> <br>" +

                                         "<b> SNIP points with the rating '" + total.Text + "' added. <br> " +

                                         "<br>" +

                                            "Author Name : " + Author.Text + "<br>" +

                                         "Publication Id : " + TextBoxPubId.Text + "<br>" +

                                        "Article Name  :  " + txtboxTitleOfWrkItem.Text + "<br>" + "<br>" + FooterText +

                                        " </b><br><b> </b></span>";

                            }

                            else
                            {

                                details.MsgBody = "<span style=\"font-size: 10pt; color: #3300cc; font-family: Verdana\"><h4>Dear Sir/Madam,</h4> <br>" +

                                        "<b> SNIP points with the rating '" + total.Text + "' added. <br> " +

                                        "<br>" +

                                           "Author Name : " + Author.Text + "<br>" +

                                        "Publication Id : " + TextBoxPubId.Text + "<br>" +

                                       "Article Name  :  " + txtboxTitleOfWrkItem.Text + "<br>" + "<br>" + FooterText +

                                       " </b><br><b> </b></span>";

                            }

                        }



                        if (details.ToEmail != "" && details.ToEmail != null)
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

                //btnApprove.Enabled = false;

                btnSave.Enabled = false;

            }

            else
            {



                string CloseWindow1 = "alert('Problem while sending mail')";

                ScriptManager.RegisterStartupScript(this, this.GetType(), "newWindow", CloseWindow1, true);

                //btnApprove.Enabled = false;

                btnSave.Enabled = false;

            }

        }

        catch (Exception ex)
        {

            string CloseWindow1 = "alert('Problem while sending mail')";

            ScriptManager.RegisterStartupScript(this, this.GetType(), "newWindow", CloseWindow1, true);

            log.Error(ex.Message);

            log.Error(ex.StackTrace);

            //btnApprove.Enabled = false;

            btnSave.Enabled = false;

        }

    }

}