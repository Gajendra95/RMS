using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Patent_GrantNotification : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        PatentBusiness Pat_Busobj = new PatentBusiness();
        Patent pat = new Patent();
        
        //RadioButtonList1_SelectedIndexChanged(sender, e);
       
        if (RadioButtonList1.SelectedValue == "PtoC")
        {
            DataSet ds = new DataSet();
            ds = Pat_Busobj.SelectProTocompletelist(RadioButtonList1.SelectedValue);

            GridProTocomplete.Visible = true;
            GridGRNtoRenewal.Visible = false;
            GridProTocomplete.DataSource = ds.Tables[0];
            GridProTocomplete.DataBind();
        }
        else if (RadioButtonList1.SelectedValue == "GtoR")
        {
            DataSet ds1 = new DataSet();
            ds1 = Pat_Busobj.SelectGRNtoRenewallist(RadioButtonList1.SelectedValue);

            GridGRNtoRenewal.Visible = true;
            GridProTocomplete.Visible = false;
            GridGRNtoRenewal.DataSource = ds1.Tables[0];
            GridGRNtoRenewal.DataBind();
        }
     
       
    }
    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RadioButtonList1.SelectedValue == "PtoC")
        {
            GridProTocomplete.DataBind();
            GridGRNtoRenewal.Visible = false;
        }
        else if (RadioButtonList1.SelectedValue == "GtoR")
        {
            GridProTocomplete.Visible = false;
            GridGRNtoRenewal.DataBind();
        }
        Panel1.Visible = false;
        panAddAuthor.Visible = false;
        Panel6.Visible = false;

        BtnSearch_Click(sender, e);
    }
    protected void GridGRNtoRenewal_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        BtnSearch_Click(sender, e);
        GridGRNtoRenewal.PageIndex = e.NewPageIndex;

        GridGRNtoRenewal.DataBind();
    }
    protected void GridProTocomplete_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        BtnSearch_Click(sender, e);
        GridProTocomplete.PageIndex = e.NewPageIndex;
        GridProTocomplete.DataBind();
    }
    protected void addRow(object sender, EventArgs e)
    {

        if (Grid_AuthorEntry.Rows.Count == 0)
        {
            SetInitialRow();
        }
        else
        {

            int rowIndex = 0;

            if (ViewState["CurrentTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                DataRow drCurrentRow = null;
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                    {

                        TextBox AuthorName = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[1].FindControl("AuthorName");

                        DropDownList DropdownMuNonMu = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[2].FindControl("DropdownMuNonMu");
                        TextBox EmployeeCode = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("EmployeeCode");
                        HiddenField Institution = (HiddenField)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("Institution");
                        TextBox InstitutionName = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[6].FindControl("InstitutionName");
                        HiddenField Department = (HiddenField)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("Department");
                        TextBox DepartmentName = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("DepartmentName");
                        TextBox MailId = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("MailId");
                        // DropDownList isCorrAuth = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("isCorrAuth");
                        //  DropDownList AuthorType = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("AuthorType");
                        ImageButton EmployeeCodeBtnImg = (ImageButton)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("EmployeeCodeBtn");

                        DropDownList DropdownStudentInstitutionName = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("DropdownStudentInstitutionName");
                        DropDownList DropdownStudentDepartmentName = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("DropdownStudentDepartmentName");

                        DropDownList NationalType = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("NationalType");
                        DropDownList ContinentId = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("ContinentId");
                        ImageButton ImageButton1 = (ImageButton)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("ImageButton1");
                        drCurrentRow = dtCurrentTable.NewRow();
                        dtCurrentTable.Rows[i - 1]["DropdownMuNonMu"] = DropdownMuNonMu.Text;
                        dtCurrentTable.Rows[i - 1]["AuthorName"] = AuthorName.Text;
                        dtCurrentTable.Rows[i - 1]["EmployeeCode"] = EmployeeCode.Text;

                        if (DropdownMuNonMu.Text == "M")
                        {

                            DropdownStudentInstitutionName.Visible = false;
                            DropdownStudentDepartmentName.Visible = false;
                            InstitutionName.Visible = true;
                            DepartmentName.Visible = true;
                            ImageButton1.Visible = false;
                            NationalType.Visible = false;
                            ContinentId.Visible = false;
                            EmployeeCodeBtnImg.Enabled = true;
                            dtCurrentTable.Rows[i - 1]["NationalType"] = NationalType.SelectedValue;
                            dtCurrentTable.Rows[i - 1]["ContinentId"] = ContinentId.SelectedValue;

                            dtCurrentTable.Rows[i - 1]["Institution"] = Institution.Value;
                            dtCurrentTable.Rows[i - 1]["InstitutionName"] = InstitutionName.Text;
                            dtCurrentTable.Rows[i - 1]["Department"] = Department.Value;
                            dtCurrentTable.Rows[i - 1]["DepartmentName"] = DepartmentName.Text;
                            popupPanelAffil.Visible = true;
                            popGridAffil.DataSourceID = "SqlDataSourceAffil";
                            SqlDataSourceAffil.DataBind();
                            popGridAffil.DataBind();
                            popGridAffil.Visible = true;
                        }
                        else if (DropdownMuNonMu.Text == "N")
                        {
                            setModalWindow(sender, e);
                            DropdownStudentInstitutionName.Visible = false;
                            DropdownStudentDepartmentName.Visible = false;
                            InstitutionName.Visible = true;
                            DepartmentName.Visible = true;

                            NationalType.Visible = true;
                            if (NationalType.SelectedValue == "I")
                            {
                                ContinentId.Visible = true;
                            }
                            else
                            {
                                ContinentId.Visible = false;
                            }

                            EmployeeCodeBtnImg.Enabled = false;

                            dtCurrentTable.Rows[i - 1]["NationalType"] = NationalType.SelectedValue;
                            dtCurrentTable.Rows[i - 1]["ContinentId"] = ContinentId.SelectedValue;

                            dtCurrentTable.Rows[i - 1]["Institution"] = Institution.Value;
                            dtCurrentTable.Rows[i - 1]["InstitutionName"] = InstitutionName.Text;
                            dtCurrentTable.Rows[i - 1]["Department"] = Department.Value;
                            dtCurrentTable.Rows[i - 1]["DepartmentName"] = DepartmentName.Text;
                        }

                        else if (DropdownMuNonMu.Text == "S")
                        {
                            DropdownStudentInstitutionName.Visible = true;
                            DropdownStudentDepartmentName.Visible = true;
                            InstitutionName.Visible = false;
                            DepartmentName.Visible = false;

                            NationalType.Visible = false;
                            ContinentId.Visible = false;
                            ImageButton1.Visible = true;
                            EmployeeCodeBtnImg.Enabled = false;

                            dtCurrentTable.Rows[i - 1]["NationalType"] = NationalType.SelectedValue;
                            dtCurrentTable.Rows[i - 1]["ContinentId"] = ContinentId.SelectedValue;
                            dtCurrentTable.Rows[i - 1]["Institution"] = DropdownStudentInstitutionName.SelectedValue;

                            dtCurrentTable.Rows[i - 1]["Department"] = DropdownStudentDepartmentName.SelectedValue;
                            setModalWindowStudent(sender, e);
                            popupstudent.Visible = true;
                            popupStudentGrid.DataSourceID = "StudentSQLDS";
                            StudentSQLDS.DataBind();
                            popupStudentGrid.DataBind();
                            popupStudentGrid.Visible = true;
                        }
                        dtCurrentTable.Rows[i - 1]["MailId"] = MailId.Text;

                        // dtCurrentTable.Rows[i - 1]["AuthorType"] = AuthorType.SelectedValue;

                        rowIndex++;
                    }

                    dtCurrentTable.Rows.Add(drCurrentRow);
                    ViewState["CurrentTable"] = dtCurrentTable;

                    Grid_AuthorEntry.DataSource = dtCurrentTable;
                    Grid_AuthorEntry.DataBind();

                }
            }
            else
            {
                Response.Write("ViewState is null");
            }

            SetPreviousData();
        }

        setModalWindowApp(sender, e);
        setModalWindowRenewal(sender, e);
        setModalWindow(sender, e);
        setModalWindowStudent(sender, e);

        PoppanelRenewal.Visible = false;
        // popupPanelAffil.Visible = false;
        PopAppStage.Visible = false;
        popupstudent.Visible = false;
    }

    private void SetInitialRow()
    {
         DataTable dt = new DataTable();
        DataRow dr = null;
        dt.Columns.Add(new DataColumn("DropdownMuNonMu", typeof(string)));
        dt.Columns.Add(new DataColumn("Author", typeof(string)));
        dt.Columns.Add(new DataColumn("AuthorName", typeof(string)));
        dt.Columns.Add(new DataColumn("EmployeeCode", typeof(string)));
        dt.Columns.Add(new DataColumn("Institution", typeof(string)));
        dt.Columns.Add(new DataColumn("InstitutionName", typeof(string)));
        dt.Columns.Add(new DataColumn("Department", typeof(string)));
        dt.Columns.Add(new DataColumn("DepartmentName", typeof(string)));
        dt.Columns.Add(new DataColumn("MailId", typeof(string)));
        //  dt.Columns.Add(new DataColumn("AuthorType", typeof(string)));
        dt.Columns.Add(new DataColumn("NationalType", typeof(string)));
        dt.Columns.Add(new DataColumn("ContinentId", typeof(string)));
        dt.Columns.Add(new DataColumn("DropdownStudentInstitutionName", typeof(string)));
        dt.Columns.Add(new DataColumn("DropdownStudentDepartmentName", typeof(string)));
        dr = dt.NewRow();

        dr["DropdownMuNonMu"] = string.Empty;
        dr["Author"] = string.Empty;
        dr["AuthorName"] = string.Empty;
        dr["EmployeeCode"] = string.Empty;
        dr["Institution"] = string.Empty;
        dr["InstitutionName"] = string.Empty;
        dr["Department"] = string.Empty;
        dr["DepartmentName"] = string.Empty;
        dr["MailId"] = string.Empty;
        // dr["AuthorType"] = string.Empty;
        dr["NationalType"] = string.Empty;
        dr["ContinentId"] = string.Empty;
        dr["DropdownStudentInstitutionName"] = string.Empty;
        dr["DropdownStudentDepartmentName"] = string.Empty;

        dt.Rows.Add(dr);

        ViewState["CurrentTable"] = dt;
        Grid_AuthorEntry.DataSource = dt;
        Grid_AuthorEntry.DataBind();

        TextBox AuthorName = (TextBox)Grid_AuthorEntry.Rows[0].Cells[1].FindControl("AuthorName");
        ImageButton EmployeeCodeBtn = (ImageButton)Grid_AuthorEntry.Rows[0].Cells[1].FindControl("EmployeeCodeBtn");

        DropDownList DropdownMuNonMu = (DropDownList)Grid_AuthorEntry.Rows[0].Cells[2].FindControl("DropdownMuNonMu");

        TextBox EmployeeCode = (TextBox)Grid_AuthorEntry.Rows[0].Cells[0].FindControl("EmployeeCode");
        HiddenField Institution = (HiddenField)Grid_AuthorEntry.Rows[0].Cells[0].FindControl("Institution");
        TextBox InstitutionName = (TextBox)Grid_AuthorEntry.Rows[0].Cells[6].FindControl("InstitutionName");
        HiddenField Department = (HiddenField)Grid_AuthorEntry.Rows[0].Cells[0].FindControl("Department");
        TextBox DepartmentName = (TextBox)Grid_AuthorEntry.Rows[0].Cells[0].FindControl("DepartmentName");
        TextBox MailId = (TextBox)Grid_AuthorEntry.Rows[0].Cells[0].FindControl("MailId");
        //DropDownList AuthorType = (DropDownList)Grid_AuthorEntry.Rows[0].Cells[0].FindControl("AuthorType");
        DropDownList NationalType = (DropDownList)Grid_AuthorEntry.Rows[0].Cells[0].FindControl("NationalType");
        DropDownList ContinentId = (DropDownList)Grid_AuthorEntry.Rows[0].Cells[0].FindControl("ContinentId");

        DropdownMuNonMu.Enabled = false;
        NationalType.Visible = false;
        ContinentId.Visible = false;

        AuthorName.Text = Session["UserName"].ToString();
        AuthorName.Enabled = false;
        EmployeeCode.Text = Session["UserId"].ToString();

        Institution.Value = Session["InstituteId"].ToString();
        string InstituteId = Session["InstituteId"].ToString();
        Business b = new Business();

        string InstituteName1 = null;
        InstituteName1 = b.GetInstitutionName(InstituteId);
        InstitutionName.Text = InstituteName1;

        Department.Value = Session["Department"].ToString();

        //Institution.Enabled = false;
        string deptId = Session["Department"].ToString();

        string deptName1 = null;
        deptName1 = b.GetDeptName(deptId, InstituteId);
        DepartmentName.Text = deptName1;

        MailId.Text = Session["emailId"].ToString();
        MailId.Enabled = false;


        if (DropdownMuNonMu.SelectedValue == "M")
        {
            EmployeeCodeBtn.Enabled = false;
        }
        else if (DropdownMuNonMu.SelectedValue == "N")
        {
            EmployeeCodeBtn.Enabled = false;
        }

        else if (DropdownMuNonMu.SelectedValue == "S")
        {
            EmployeeCodeBtn.Enabled = false;
        }
    }
    protected void AuthorName_Changed(object sender, EventArgs e)
    {
    }
    protected void btn_CANCEL_Click(object sender, EventArgs e)
    {

    }
    protected void btnApp_Submit(object sender, EventArgs e)
    {
        if (!Page.IsValid)
        {
            return;
        }

        try
        {
            bool result2 = false, result1 = false;
            PatentBusiness Bus_obj = new PatentBusiness();
            Patent pat = new Patent();

            pat.ID = txtID.Text;
            pat.App_Status = ddlAppstage.SelectedValue.ToString();
            if (txtAppDate.Text.ToString() != "")
            {
                pat.App_Date = DateTime.ParseExact(txtAppDate.Text, "dd/MM/yyyy", null);

            }
            //  pat.App_Date = Convert.ToDateTime(txtAppDate.Text);
            pat.App_Comment = txtAppComment.Text;
            //ViewState["ID"] = txtID.Text;
            ViewState["App_Status"] = ddlAppstage.SelectedValue.ToString();
            //ViewState["App_Date"] = Convert.ToDateTime(txtAppDate.Text).ToShortDateString();
            //ViewState["App_Comment"] = txtAppComment.Text;
            pat.Created_By = Session["UserId"].ToString();

            txtApplicationStage.Text = ddlAppstage.SelectedItem.ToString();
            //   result2 = Bus_obj.AppStagePatent(pat);
            //setModalWindowApp(sender, e);

            if (ddlAppstage.SelectedValue == "0")
            {

                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please Select Application Stage !')</script>");

                return;
            }
            if (ddlFilingstatus.SelectedValue == "APP")
            {
                result2 = Bus_obj.InsertApplicationStage(pat);

            }

            if (result2 == true)
            {
                //Btnsubmit.Enabled = true;
                Btnsave.Enabled = false;
                //BtnDraft.Visible = true;
                string CloseWindow;
                CloseWindow = "alert('Application Stage Saved')";
                ScriptManager.RegisterStartupScript(up, up.GetType(), "CloseWindow", CloseWindow, true);
                //   ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Application Stage Saved!')</script>");
                // setModalWindowApp(sender, e);
                txtAppDate.Text = "";
                txtAppComment.Text = "";
                ddlAppstage.ClearSelection();
                btnview.Text = "Add/View Application Stage Details";
                //lblnote.Visible = true;
                up.Update();
                UpdatePanel2.Update();
                // return;

            }
            else
            {
                string CloseWindow;
                CloseWindow = "alert('Error while saving')";
                ScriptManager.RegisterStartupScript(up, up.GetType(), "CloseWindow", CloseWindow, true);
                //lblnote.Text = "Error while saving";
                //lblnote.Visible = true;
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "CallMyFunction", "ToggleDisplay2()", true);
            // Btn_App_View(sender, e);

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "CallMyFunction", "ToggleDisplay2()", true);
            throw ex;
        }


    }
    protected void btnRenewal_Click(object sender, EventArgs e)
    {
        if (!Page.IsValid)
        {
            return;
        }
        update2.Update();
        up.Update();
        UpdatePanel2.Update();
        PatentBusiness Bus_obj = new PatentBusiness();
        Patent pat = new Patent();
        Patent pat1 = new Patent();
        int res = 0;
        bool result = false;
        if (Convert.ToDateTime(txtRenewalDate.Text) > DateTime.Now)
        {
            txtNextRenewalYear.Text = "";
            txtRenewalDate.Text = "";
            // ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Renewal date cannot be greater than current date!')</script>");
            //return;
        }
        if (ddlFilingstatus.SelectedValue == "GRN")
        {
            // ScriptManager.RegisterStartupScript(this, this.GetType(), "CallMyFunction", "ToggleDisplay3()", true);
            string value = HiddenEntryConfirm1.Value;
            if (value == "Yes")
            {

                pat.ID = txtID.Text;
                pat.Renewal_Fee = txtRenewalFee.Text;
                if (txtRenewalDate.Text.ToString() != "")
                {
                    pat.LastRenewalFeePaiddate = DateTime.ParseExact(txtRenewalDate.Text, "dd/MM/yyyy", null);

                }

                Patent p = new Patent();
                p = Bus_obj.SelectRenewalDate(txtID.Text);
                pat.NextRenewalDate = p.NextRenewalDate.AddYears(Convert.ToInt16(ddlNoOfYears.SelectedValue));
                //pat.NextRenewalDate = pat.LastRenewalFeePaiddate.AddYears(1);
                pat.Renewal_Comment = txtRenewalComment.Text;
                txtlastRenewal.Text = txtRenewalDate.Text;
                pat.NextRenewalYear = pat.NextRenewalDate.Year;
                pat.Created_By = Session["UserId"].ToString();
                pat.Created_Date = DateTime.Now;
                pat.Filing_Status = "GRN";
                result = Bus_obj.InsertRenwalaDetails(pat);
                if (result == true)
                {
                    // Btnsubmit.Enabled = true;
                    Btnsave.Enabled = false;
                    //Btnsubmit.Enabled = false;
                    txtRenewalFee.Text = "";
                    txtRenewalDate.Text = "";
                    txtRenewalComment.Text = "";


                    string CloseWindow;
                    CloseWindow = "alert('Renewal Details Saved!')";
                    ScriptManager.RegisterStartupScript(up, up.GetType(), "CloseWindow", CloseWindow, true);
                    // return;
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "CallMyFunction", "ToggleDisplay3()", true);
            }
            else if (value == "LAPSE")
            {
                Patent Pat1 = new Patent();

                Patent_DAobject bus_obj1 = new Patent_DAobject();
                pat.ID = txtID.Text;
                Pat1 = bus_obj1.SelectPatent(txtID.Text);
                txtlastRenewal.Text = Pat1.LastRenewalFeePaiddate.ToShortDateString();

                if (txtRenewalDate.Text.ToString() != "")
                {
                    pat.LastRenewalFeePaiddate = DateTime.ParseExact(txtRenewalDate.Text, "dd/MM/yyyy", null);

                }
                //  pat.NextRenewalDate = Pat1.LastRenewalFeePaiddate.AddYears(2);
                pat.NextRenewalDate = DateTime.ParseExact(txtnextRenewal.Text, "dd/MM/yyyy", null);
                pat.lapsedate = pat.NextRenewalDate.AddDays(1);
                pat.NextRenewalDate = pat.NextRenewalDate.AddYears(2);
                pat.Created_By = Session["UserId"].ToString();
                pat.Created_Date = DateTime.Now;

                pat.Filing_Status = "LAP";
                pat.Remarks = txtRenewalComment.Text;

                res = Bus_obj.UpdatePatentStatus(pat);
                if (res >= 0)
                {
                    btnSaveRenewal.Enabled = false;
                    Btnsave.Enabled = true;
                    //Btnsubmit.Enabled = false;
                    SqlDataSourePatentStatus.SelectCommand = "Select * from Patent_Status where Id in('EXP', 'LAP') ";
                    ddlFilingstatus.DataSourceID = "SqlDataSourePatentStatus";
                    // ddlFilingstatus.DataBind();
                    ddlFilingstatus.SelectedValue = "LAP";
                    Patent_DAobject bus_obj = new Patent_DAobject();
                    Patent pat2 = new Patent();
                    pat2 = bus_obj.SelectPatent(txtID.Text);
                    txtlastRenewal.Text = pat.LastRenewalFeePaiddate.ToShortDateString();

                    string CloseWindow;
                    CloseWindow = "alert('Patent is lapsed')";
                    ScriptManager.RegisterStartupScript(up, up.GetType(), "CloseWindow", CloseWindow, true);
                    // return;
                    // ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Patent is lapsed')</script>");
                    //return;
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "CallMyFunction", "ToggleDisplay3()", true);
            }
            else if (value == "No")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "CallMyFunction", "ToggleDisplay3()", true);
            }
        }
        else if (ddlFilingstatus.SelectedValue == "LAP")
        {

            string value = HiddenEntryConfirm1.Value;
            if (value == "GRN")
            {
                pat.ID = txtID.Text;
                pat.Renewal_Fee = txtRenewalFee.Text;
                if (txtRenewalDate.Text.ToString() != "")
                {
                    pat.LastRenewalFeePaiddate = DateTime.ParseExact(txtRenewalDate.Text, "dd/MM/yyyy", null);

                }
                pat.NextRenewalDate = pat.LastRenewalFeePaiddate.AddYears(1);
                pat.Renewal_Comment = txtRenewalComment.Text;
                txtlastRenewal.Text = txtRenewalDate.Text;
                pat.NextRenewalYear = pat.NextRenewalDate.Year;
                pat.Created_By = Session["UserId"].ToString();
                pat.Created_Date = DateTime.Now;
                pat.Filing_Status = "GRN";
                result = Bus_obj.InsertRenwalaDetails(pat);
                if (result == true)
                {
                    // Btnsubmit.Enabled = true;

                    SqlDataSourePatentStatus.SelectCommand = "Select * from Patent_Status where Id in('GRN', 'LAP','EXP') ";
                    ddlFilingstatus.DataSourceID = "SqlDataSourePatentStatus";
                    // ddlFilingstatus.DataBind();
                    ddlFilingstatus.SelectedValue = "GRN";
                    Btnsave.Enabled = false;
                    //Btnsubmit.Enabled = false;
                    txtRenewalFee.Text = "";
                    txtRenewalDate.Text = "";
                    txtRenewalComment.Text = "";
                    Patent_DAobject bus_obj = new Patent_DAobject();
                    Patent pat2 = new Patent();
                    pat2 = bus_obj.SelectPatent(txtID.Text);
                    txtlastRenewal.Text = pat.LastRenewalFeePaiddate.ToShortDateString();
                    string CloseWindow;
                    CloseWindow = "alert('Renewal Details Saved!')";
                    ScriptManager.RegisterStartupScript(up, up.GetType(), "CloseWindow", CloseWindow, true);
                    // return;
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "CallMyFunction", "ToggleDisplay3()", true);
            }
            else if (value == "EXP")
            {
                Patent Pat1 = new Patent();
                Patent_DAobject bus_obj1 = new Patent_DAobject();
                Pat1 = bus_obj1.SelectPatent(ID);
                txtlastRenewal.Text = Pat1.LastRenewalFeePaiddate.ToShortDateString();
                pat.ID = txtID.Text;
                if (txtRenewalDate.Text.ToString() != "")
                {
                    pat.LastRenewalFeePaiddate = DateTime.ParseExact(txtRenewalDate.Text, "dd/MM/yyyy", null);

                }
                //  pat.NextRenewalDate = Pat1.LastRenewalFeePaiddate.AddYears(2);
                pat.NextRenewalDate = DateTime.ParseExact(txtnextRenewal.Text, "dd/MM/yyyy", null);
                pat.lapsedate = pat.NextRenewalDate.AddDays(1);
                pat.NextRenewalDate = pat.NextRenewalDate.AddYears(2);
                pat.Created_By = Session["UserId"].ToString();
                pat.Created_Date = DateTime.Now;

                pat.Filing_Status = "EXP";
                pat.Remarks = txtRenewalComment.Text;

                res = Bus_obj.UpdatePatentStatus(pat);
                if (res >= 0)
                {
                    btnSaveRenewal.Enabled = false;
                    Btnsave.Enabled = true;
                    //Btnsubmit.Enabled = false;
                    SqlDataSourePatentStatus.SelectCommand = "Select * from Patent_Status where Id in('EXP') ";
                    ddlFilingstatus.DataSourceID = "SqlDataSourePatentStatus";
                    // ddlFilingstatus.DataBind();
                    ddlFilingstatus.SelectedValue = "EXP";
                    Btnsave.Enabled = false;
                    Patent_DAobject bus_obj = new Patent_DAobject();
                    Patent pat2 = new Patent();
                    pat2 = bus_obj.SelectPatent(txtID.Text);
                    txtlastRenewal.Text = pat.LastRenewalFeePaiddate.ToShortDateString();
                    string CloseWindow;
                    CloseWindow = "alert('Patent is Expired')";
                    ScriptManager.RegisterStartupScript(up, up.GetType(), "CloseWindow", CloseWindow, true);
                    // return;
                    // ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Patent is lapsed')</script>");
                    //return;
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "CallMyFunction", "ToggleDisplay3()", true);

            }
            //Patent Pat1 = new Patent();
            //Patent_DAobject bus_obj1 = new Patent_DAobject();
            //Pat1 = bus_obj1.SelectPatent(ID);
            //txtlastRenewal.Text = Pat1.LastRenewalFeePaiddate.ToShortDateString();

            //if (txtRenewalDate.Text.ToString() != "")
            //{
            //    pat.LastRenewalFeePaiddate = DateTime.ParseExact(txtRenewalDate.Text, "dd/MM/yyyy", null);

            //}
            ////  pat.NextRenewalDate = Pat1.LastRenewalFeePaiddate.AddYears(2);
            //pat.NextRenewalDate = DateTime.ParseExact(txtRenewalDate.Text, "dd/MM/yyyy", null);
            //pat.NextRenewalDate = pat.NextRenewalDate.AddYears(2);
            //pat.Created_By = Session["UserId"].ToString();
            //pat.Created_Date = DateTime.Now;
            //pat.lapsedate = pat.NextRenewalDate.AddDays(1);
            //res = Bus_obj.UpdateLapseStatus(pat);
            //if (res >= 0)
            //{
            //    btnSaveRenewal.Enabled = false;
            //    Btnsave.Enabled = true;
            //    Btnsubmit.Enabled = false;
            //    SqlDataSourePatentStatus.SelectCommand = "Select * from Patent_Status where Id in('EXP', 'LAP') ";
            //    ddlFilingstatus.DataSourceID = "SqlDataSourePatentStatus";
            //    // ddlFilingstatus.DataBind();
            //    ddlFilingstatus.SelectedValue = "LAP";


            //    string CloseWindow;
            //    CloseWindow = "alert('Patent is lapsed')";
            //    ScriptManager.RegisterStartupScript(up, up.GetType(), "CloseWindow", CloseWindow, true);
            //    // return;
            //    // ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Patent is lapsed')</script>");
            //    //return;
            //}
            ScriptManager.RegisterStartupScript(this, this.GetType(), "CallMyFunction", "ToggleDisplay3()", true);
        }
        //pat1 = Bus_obj.SelectRenewalDate(pat.ID);


        //if (txtGrantDate.Text.ToString() != "")
        //{
        //    pat.Grant_Date = DateTime.ParseExact(txtGrantDate.Text, "dd/MM/yyyy", null);

        //}
        //ViewState["App_Status"] = DateTime.ParseExact(txtlastRenewalFee.Text, "dd/MM/yyyy", null);
        //pat.NextRenewalDate = DateTime.ParseExact(txtnextRenewal.Text, "dd/MM/yyyy", null);

        //int result = DateTime.Compare(pat1.NextRenewalDate, pat.LastRenewalFeePaiddate);
        //// int diffMonths = ((pat1.NextRenewalDate.Year - pat.LastRenewalFeePaiddate.Year) * 12) + pat1.NextRenewalDate.Month - pat.LastRenewalFeePaiddate.Month;
        //if (result >= 0)
        //{
        //    pat.NextRenewalDate = DateTime.ParseExact(txtnextRenewal.Text, "dd/MM/yyyy", null);

        //    pat.NextRenewalYear = pat.NextRenewalDate.Year;
        //    pat.Created_By = Session["UserId"].ToString();
        //    pat.Created_Date = DateTime.Now;
        //   // DateTime lastdate = Convert.ToDateTime(ViewState["App_Status"]);
        //    txtlastRenewal.Text = lastdate.ToShortDateString();


        //    //if (pat.NextRenewalDate < DateTime.Now)
        //    //{
        //    //    pat.NextRenewalDate = pat.LastRenewalFeePaiddate.AddYears(2);
        //    //}
        //    txtnextRenewal.Text = pat.NextRenewalDate.ToShortDateString();
        //    //txtlastRenewal.Text = pat.LastRenewalFeePaiddate.ToShortDateString();
        //    pat.NextRenewalYear = Convert.ToInt32(txtNextRenewalYear.Text);
        //    //pat.NextRenewalDate = DateTime.Parse(txtNextRenewalDate.Text);
        //    //int res1 = Bus_obj.InsertLapRenewalTracker(pat);
        //    pat.LastRenewalFeePaiddate = DateTime.ParseExact(txtlastRenewal.Text, "dd/MM/yyyy", null);
        //    result2 = Bus_obj.InsertRenwalaDetails(pat);

        //}
        //else
        //{



        //    if (RadioButtonList2.SelectedValue == "Y")
        //    {
        //        pat.NextRenewalDate = DateTime.ParseExact(txtnextRenewal.Text, "dd/MM/yyyy", null);
        //        pat.NextRenewalYear = pat.NextRenewalDate.Year;
        //        pat.Created_By = Session["UserId"].ToString();
        //        pat.Created_Date = DateTime.Now;
        //        txtlastRenewal.Text = txtlastRenewalFee.Text;
        //        pat.NextRenewalDate = pat.LastRenewalFeePaiddate.AddYears(1);
        //        txtnextRenewal.Text = pat.NextRenewalDate.ToShortDateString();
        //        pat.NextRenewalYear = Convert.ToInt32(txtNextRenewalYear.Text);
        //        pat.lapsedate = pat.NextRenewalDate.AddDays(1);
        //        res = Bus_obj.UpdateLapseStatus(pat);
        //        if (res >= 0)
        //        {
        //            btnSaveRenewal.Enabled = false;
        //            Btnsave.Enabled = false;
        //            Btnsubmit.Enabled = false;
        //            ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Patent is lapsed')</script>");
        //            return;
        //        }
        //    }
        //    else if (RadioButtonList2.SelectedValue == "N")
        //    {

        //        pat.NextRenewalDate = DateTime.ParseExact(txtnextRenewal.Text, "dd/MM/yyyy", null);
        //        //nextrenewaldate1 = Convert.ToDateTime(txtnextRenewal.Text);
        //        //pat.NextRenewalDate = nextrenewaldate1.AddYears(1);
        //        //renewalyear = pat.NextRenewalDate.Year;
        //        pat.NextRenewalYear = pat.NextRenewalDate.Year;
        //        pat.Created_By = Session["UserId"].ToString();
        //        //int res1 = Bus_obj.InsertLapRenewalTracker(pat);
        //        //if (res1 >= 0)
        //        //{
        //        //    btnSaveRenewal.Enabled = false;
        //        //    ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Patent is updated')</script>");
        //        //    return;
        //        //}
        //        result2 = Bus_obj.InsertRenwalaDetails(pat);
        //    }
        //}


        //if (ddlFilingstatus.SelectedValue == "GRN" || ddlFilingstatus.SelectedValue == "LAP")
        //{
        //    if (txtGrantDate.Text.ToString() == "")
        //    {
        //        ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Enter Grant date !')</script>");
        //        txtRenewalFee.Text = "";
        //        txtlastRenewalFee.Text = "";
        //        txtlastRenewal.Text = "";
        //        txtRenewalComment.Text = "";
        //        txtNextRenewalYear.Text = "";
        //        //txtNextRenewalYear.Enabled = true;
        //        btnSaveRenewal.Enabled = true;
        //        txtRenewalFee.Enabled = true;
        //        txtlastRenewalFee.Enabled = true;
        //        txtRenewalComment.Enabled = true;

        //        return;
        //    }
        //}

        ////if (RadioButtonList2.SelectedValue == "")
        ////{
        ////    result2 = Bus_obj.InsertRenwalaDetails(pat);
        ////}

        //if (result2 == true)
        //{
        //    // Btnsubmit.Enabled = true;
        //    Btnsave.Enabled = false;
        //    Btnsubmit.Enabled = false;
        //    setModalWindowRenewal(sender, e);
        //    txtRenewalFee.Text = "";
        //    txtlastRenewalFee.Text = "";
        //    txtRenewalComment.Text = "";

        //    ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Renewal Details Saved!')</script>");
        //    return;

        //}



        //if (txtlastRenewalFee.Text.ToString() != "")
        //{

        //    Patent pat = new Patent();
        //    Patent pat1 = new Patent();
        //    PatentBusiness Bus_obj = new PatentBusiness();
        //    pat.ID = txtID.Text;
        //    pat.LastRenewalFeePaiddate = DateTime.ParseExact(txtlastRenewalFee.Text, "dd/MM/yyyy", null);
        //    pat.NextRenewalDate = pat.LastRenewalFeePaiddate.AddYears(1);
        //    txtnextRenewal.Text = pat.NextRenewalDate.ToShortDateString();
        //    int renewalyear = pat.NextRenewalDate.Year;
        //    txtNextRenewalYear.Text = renewalyear.ToString();
        //    if (pat.LastRenewalFeePaiddate > DateTime.Now)
        //    {
        //        txtNextRenewalYear.Text = "";
        //        txtlastRenewalFee.Text = "";
        //        ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Renewal date cannot be greater than current date!')</script>");
        //        return;
        //    }

        //    if (pat.LastRenewalFeePaiddate < pat.Grant_Date)
        //    {
        //        ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Enter Correct date !')</script>");
        //        return;
        //    }
        //    //renewalyear = Convert.ToInt32(txtNextRenewalYear.Text);
        //    pat1 = Bus_obj.SelectRenewalDate(pat.ID);
        //    int result = DateTime.Compare(pat1.NextRenewalDate, pat.LastRenewalFeePaiddate);
        //    //  int diffMonths = ((pat1.NextRenewalDate.Year - pat.LastRenewalFeePaiddate.Year) * 12) + pat1.NextRenewalDate.Month - pat.LastRenewalFeePaiddate.Month;
        //    if (result >= 0)
        //    {

        //        txtRenewalFee.Enabled = true;
        //        txtlastRenewalFee.Enabled = true;
        //        txtRenewalComment.Enabled = true;
        //        txtNextRenewalYear.Enabled = false;
        //        btnSaveRenewal.Enabled = true;


        //    }
        //    else
        //    {
        //        update2.Update();
        //        UpdatePanel2.Update();
        //        up.Update();
        //       // ScriptManager.RegisterStartupScript(this, this.GetType(), "CallMyFunction", "callthis4()", true);
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "CallMyFunction", "ConfirmRenewalDetails()", true);
        //        //pat.Filing_Status = "LAP";
        //        //txtGrantDate.Enabled = false;
        //        //txtRenewalFee.Enabled = false;
        //        //txtlastRenewalFee.Enabled = false;
        //        //txtRenewalComment.Enabled = false;
        //        //txtNextRenewalYear.Enabled = false;
        //        //btnSaveRenewal.Enabled = true;
        //        //lblnote.Visible = true;
        //        //RadioButtonList2.Visible = true;
        //        //btnSaveRenewal.Enabled = false;
        //    }
        //}



    }

    protected void btnRenewalview_Click(object sender, EventArgs e)
    {
        txtRenewalFee.Text = "";
        txtRenewalDate.Text = "";
        txtRenewalComment.Text = "";
        setModalWindowRenewal(sender, e);
        PatentBusiness Bus_obj = new PatentBusiness();
        Patent p = new Patent();
        p = Bus_obj.SelectRenewalDate(txtID.Text);
        txtnextRenewal.Text = p.NextRenewalDate.ToShortDateString();
        hdnNextRD.Value = p.NextRenewalDate.ToShortDateString();
        if (ddlNoOfYears.SelectedValue != "")
        {

            txtnextRenewal.Text = p.NextRenewalDate.AddYears(Convert.ToInt16(ddlNoOfYears.SelectedValue)).ToShortDateString();
        }
        if (ddlFilingstatus.SelectedValue == "GRN" || ddlFilingstatus.SelectedValue == "LAP")
        {
            btnSaveRenewal.Enabled = true;
        }
        else
        {
            btnSaveRenewal.Enabled = false;
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "CallMyFunction", "callthis3()", true);
    }
    protected void DropdownMuNonMuOnSelectedIndexChanged(object sender, EventArgs e)
    {

        TextBox senderBox = sender as TextBox;

        GridViewRow currentRow = (GridViewRow)((DropDownList)sender).Parent.Parent;
        DropDownList DropdownMuNonMu = (DropDownList)currentRow.FindControl("DropdownMuNonMu");
        ImageButton EmployeeCodeBtn = (ImageButton)currentRow.FindControl("EmployeeCodeBtn");
        TextBox EmployeeCode = (TextBox)currentRow.FindControl("EmployeeCode");
        TextBox InstitutionName = (TextBox)currentRow.FindControl("InstitutionName");
        DropDownList DropdownStudentInstitutionName1 = (DropDownList)currentRow.FindControl("DropdownStudentInstitutionName");

        TextBox DepartmentName = (TextBox)currentRow.FindControl("DepartmentName");
        DropDownList DropdownStudentDepartmentName = (DropDownList)currentRow.FindControl("DropdownStudentDepartmentName");


        TextBox AuthorName = (TextBox)currentRow.FindControl("AuthorName");
        TextBox MailId = (TextBox)currentRow.FindControl("MailId");
        DropDownList NationalType = (DropDownList)currentRow.FindControl("NationalType");

        DropDownList ContinentId = (DropDownList)currentRow.FindControl("ContinentId");
        TextBox NameInJournal = (TextBox)currentRow.FindControl("NameInJournal");
        ImageButton ImageButton1 = (ImageButton)currentRow.FindControl("ImageButton1");

        if (DropdownMuNonMu.SelectedValue == "M")
        {
            DropdownStudentInstitutionName1.Visible = false;
            DropdownStudentDepartmentName.Visible = false;
            InstitutionName.Visible = true;
            DepartmentName.Visible = true;
            NationalType.Visible = false;
            ContinentId.Visible = false;
            EmployeeCode.Visible = true;
            EmployeeCode.Enabled = false;
            EmployeeCodeBtn.Enabled = true;
            InstitutionName.Enabled = false;
            DepartmentName.Enabled = false;
            AuthorName.Enabled = false;
            MailId.Enabled = false;
            EmployeeCode.Text = "";
            AuthorName.Text = "";
            MailId.Text = "";
            InstitutionName.Text = "";
            DepartmentName.Text = "";
            ImageButton1.Visible = false;
            EmployeeCodeBtn.Visible = true;
            NameInJournal.Text = "";
            NameInJournal.Text = "";

        }
        else if (DropdownMuNonMu.SelectedValue == "N")
        {
            DropdownStudentInstitutionName1.Visible = false;
            DropdownStudentDepartmentName.Visible = false;
            InstitutionName.Visible = true;
            DepartmentName.Visible = true;
            NationalType.Visible = true;

            EmployeeCode.Enabled = false;
            EmployeeCodeBtn.Enabled = false;
            InstitutionName.Enabled = true;
            DepartmentName.Enabled = true;
            AuthorName.Enabled = true;
            MailId.Enabled = true;
            AuthorName.Text = "";
            EmployeeCode.Text = "";
            MailId.Text = "";
            InstitutionName.Text = "";
            DepartmentName.Text = "";
            ImageButton1.Visible = false;
            EmployeeCodeBtn.Visible = true;
        }

        else if (DropdownMuNonMu.SelectedValue == "S")
        {

            DropdownStudentInstitutionName1.Visible = true;
            DropdownStudentDepartmentName.Visible = true;
            InstitutionName.Visible = false;
            EmployeeCode.Enabled = false;
            DepartmentName.Visible = false;
            EmployeeCodeBtn.Enabled = false;
            EmployeeCodeBtn.Visible = true;
            ImageButton1.Visible = false;
            AuthorName.Enabled = true;
            NationalType.Visible = false;
            ContinentId.Visible = false;

            NationalType.Visible = false;
            // ContinentId.Visible = false;
            MailId.Enabled = true;
            AuthorName.Text = "";
            MailId.Text = "";
            InstitutionName.Text = "";
            DepartmentName.Text = "";
            EmployeeCode.Text = "";
        }

        else if (DropdownMuNonMu.SelectedValue == "O")
        {

            EmployeeCode.Visible = true;
            EmployeeCode.Enabled = true;
            DropdownStudentInstitutionName1.Visible = true;
            DropdownStudentDepartmentName.Visible = true;
            InstitutionName.Visible = false;
            DepartmentName.Visible = false;
            NationalType.Visible = false;
            ContinentId.Visible = false;
            EmployeeCodeBtn.Enabled = false;
            InstitutionName.Enabled = false;
            DepartmentName.Enabled = false;
            AuthorName.Enabled = true;
            MailId.Enabled = true;
            EmployeeCode.Text = "";
            AuthorName.Text = "";
            MailId.Text = "";
            InstitutionName.Text = "";
            DepartmentName.Text = "";
            // NameInJournal.Text = "";
            ImageButton1.Visible = false;
            EmployeeCodeBtn.Visible = true;

        }


    }
    protected void exit(object sender, EventArgs e)
    {
    }
    private void SetRowData()
    {

        int rowIndex = 0;

        if (ViewState["CurrentTable"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
            DataRow drCurrentRow = null;
            if (dtCurrentTable.Rows.Count > 0)
            {
                for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                {


                    //  TextBox Author = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("Author");
                    TextBox AuthorName = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[1].FindControl("AuthorName");
                    ImageButton EmployeeCodeBtnImg = (ImageButton)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("EmployeeCodeBtn");

                    DropDownList DropdownMuNonMu = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[2].FindControl("DropdownMuNonMu");
                    //TextBox amount = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[3].FindControl("amount");
                    TextBox EmployeeCode = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[1].FindControl("EmployeeCode");
                    HiddenField Institution = (HiddenField)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("Institution");
                    TextBox InstitutionName = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[6].FindControl("InstitutionName");
                    HiddenField Department = (HiddenField)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("Department");
                    TextBox DepartmentName = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("DepartmentName");
                    TextBox MailId = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("MailId");

                    DropDownList isCorrAuth = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("isCorrAuth");
                    DropDownList AuthorType = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("AuthorType");

                    TextBox NameInJournal = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("NameInJournal");
                    DropDownList IsPresenter = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("IsPresenter");

                    CheckBox HasAttented = (CheckBox)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("HasAttented");


                    DropDownList DropdownStudentInstitutionName = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("DropdownStudentInstitutionName");
                    DropDownList DropdownStudentDepartmentName = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("DropdownStudentDepartmentName");
                    DropDownList NationalType = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("NationalType");
                    DropDownList ContinentId = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("ContinentId");



                    ImageButton EmployeeCodeBtnImg1 = (ImageButton)Grid_AuthorEntry.Rows[0].Cells[0].FindControl("EmployeeCodeBtn");
                    ImageButton ImageButton1 = (ImageButton)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("ImageButton1");

                    drCurrentRow = dtCurrentTable.NewRow();
                    //  dtCurrentTable.Rows[i - 1]["amount"] = amount.Text.Trim() == "" ? 0 : Convert.ToDecimal(amount.Text);
                    dtCurrentTable.Rows[i - 1]["DropdownMuNonMu"] = DropdownMuNonMu.Text;
                    //  dtCurrentTable.Rows[i - 1]["Author"] = Author.Text;
                    dtCurrentTable.Rows[i - 1]["AuthorName"] = AuthorName.Text;
                    dtCurrentTable.Rows[i - 1]["EmployeeCode"] = EmployeeCode.Text;

                    if (DropdownMuNonMu.Text == "M")
                    {
                        DropdownStudentInstitutionName.Visible = false;
                        DropdownStudentDepartmentName.Visible = false;
                        InstitutionName.Visible = true;
                        DepartmentName.Visible = true;
                        EmployeeCode.Enabled = false;
                        EmployeeCodeBtnImg.Enabled = true;

                        NationalType.Visible = false;
                        ContinentId.Visible = false;

                        dtCurrentTable.Rows[i - 1]["NationalType"] = NationalType.Text;
                        //dtCurrentTable.Rows[i - 1]["ContinentId"] = ContinentId.Text;


                        dtCurrentTable.Rows[i - 1]["Institution"] = Institution.Value;
                        dtCurrentTable.Rows[i - 1]["InstitutionName"] = InstitutionName.Text;
                        dtCurrentTable.Rows[i - 1]["Department"] = Department.Value;
                        dtCurrentTable.Rows[i - 1]["DepartmentName"] = DepartmentName.Text;
                    }
                    else if (DropdownMuNonMu.Text == "N")
                    {
                        DropdownStudentInstitutionName.Visible = false;
                        DropdownStudentDepartmentName.Visible = false;
                        InstitutionName.Visible = true;
                        DepartmentName.Visible = true;
                        EmployeeCode.Enabled = false;
                        EmployeeCodeBtnImg.Enabled = false;

                        NationalType.Visible = true;
                        if (NationalType.Text == "I")
                        {
                            ContinentId.Visible = true;
                        }
                        else
                        {
                            ContinentId.Visible = false;
                        }

                        dtCurrentTable.Rows[i - 1]["NationalType"] = NationalType.Text;
                        dtCurrentTable.Rows[i - 1]["ContinentId"] = ContinentId.Text;


                        dtCurrentTable.Rows[i - 1]["Institution"] = Institution.Value;
                        dtCurrentTable.Rows[i - 1]["InstitutionName"] = InstitutionName.Text;
                        dtCurrentTable.Rows[i - 1]["Department"] = Department.Value;
                        dtCurrentTable.Rows[i - 1]["DepartmentName"] = DepartmentName.Text;
                    }

                    else if (DropdownMuNonMu.Text == "O")
                    {

                        EmployeeCode.Visible = true;
                        EmployeeCode.Enabled = true;
                        DropdownStudentInstitutionName.Visible = true;
                        DropdownStudentDepartmentName.Visible = true;
                        InstitutionName.Visible = false;
                        DepartmentName.Visible = false;

                        NationalType.Visible = false;
                        ContinentId.Visible = false;
                        MailId.Enabled = true;
                        EmployeeCodeBtnImg.Enabled = false;
                        AuthorName.Enabled = false;
                        dtCurrentTable.Rows[i - 1]["Institution"] = DropdownStudentInstitutionName.SelectedValue;

                        dtCurrentTable.Rows[i - 1]["MailId"] = MailId.Text.Trim();
                        dtCurrentTable.Rows[i - 1]["Department"] = DropdownStudentDepartmentName.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["EmployeeCode"] = EmployeeCode.Text.Trim();

                    }
                    else if (DropdownMuNonMu.Text == "S")
                    {
                        //if (DropDownListPublicationEntry.SelectedValue == "JA" || DropDownListPublicationEntry.SelectedValue == "TS")
                        //{
                        //    DropdownStudentInstitutionName.Visible = false;
                        //    DropdownStudentDepartmentName.Visible = false;
                        //    InstitutionName.Visible = true;
                        //    DepartmentName.Visible = true;
                        //    NationalType.Visible = false;
                        //    ContinentId.Visible = false;
                        //    dtCurrentTable.Rows[i - 1]["Institution"] = Institution.Value;
                        //    dtCurrentTable.Rows[i - 1]["InstitutionName"] = InstitutionName.Text;
                        //    dtCurrentTable.Rows[i - 1]["Department"] = Department.Value;
                        //    dtCurrentTable.Rows[i - 1]["DepartmentName"] = DepartmentName.Text;
                        //    ImageButton1.Visible = true;
                        //    EmployeeCode.Enabled = false;
                        //    EmployeeCodeBtnImg.Enabled = false;
                        //    EmployeeCodeBtnImg.Visible = false;
                        //}
                        //else
                        //{
                        DropdownStudentInstitutionName.Visible = true;
                        DropdownStudentDepartmentName.Visible = true;
                        InstitutionName.Visible = false;
                        DepartmentName.Visible = false;
                        EmployeeCode.Enabled = false;
                        NationalType.Visible = false;
                        ContinentId.Visible = false;
                        dtCurrentTable.Rows[i - 1]["Institution"] = DropdownStudentInstitutionName.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["InstitutionName"] = DropdownStudentInstitutionName.SelectedItem;
                        dtCurrentTable.Rows[i - 1]["Department"] = DropdownStudentDepartmentName.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["DepartmentName"] = DropdownStudentDepartmentName.SelectedItem;
                        EmployeeCodeBtnImg.Enabled = false;
                        EmployeeCodeBtnImg.Visible = true;
                        ImageButton1.Visible = false;
                        //}
                        NationalType.Visible = false;
                        ContinentId.Visible = false;


                        dtCurrentTable.Rows[i - 1]["NationalType"] = NationalType.Text;
                        dtCurrentTable.Rows[i - 1]["ContinentId"] = ContinentId.Text;


                        // dtCurrentTable.Rows[i - 1]["Institution"] = DropdownStudentInstitutionName.SelectedValue;
                        //// dtCurrentTable.Rows[i - 1]["InstitutionName"] = DropdownStudentInstitutionName.SelectedItem;
                        // dtCurrentTable.Rows[i - 1]["Department"] = DropdownStudentDepartmentName.SelectedValue;
                        // //dtCurrentTable.Rows[i - 1]["DepartmentName"] = DropdownStudentDepartmentName.SelectedItem;
                    }



                    EmployeeCodeBtnImg1.Enabled = false;

                    dtCurrentTable.Rows[i - 1]["MailId"] = MailId.Text;

                    //if (DropDownListPublicationEntry.SelectedValue == "JA" || DropDownListPublicationEntry.SelectedValue == "TS")
                    //{
                    //    if (DropdownMuNonMu.Text == "M")
                    //    {
                    //        EmployeeCodeBtnImg1.Enabled = true;
                    //    }
                    //    else if (DropdownMuNonMu.Text == "N")
                    //    {
                    //        EmployeeCodeBtnImg1.Enabled = false;
                    //    }
                    //    else if (DropdownMuNonMu.Text == "S")
                    //    {
                    //        EmployeeCodeBtnImg1.Enabled = false;
                    //    }
                    //    else if (DropdownMuNonMu.Text == "O")
                    //    {
                    //        EmployeeCodeBtnImg1.Enabled = false;
                    //    }

                    //    DropdownMuNonMu.Enabled = true;
                    //}
                    //else
                    //{
                    DropdownMuNonMu.Enabled = false;
                    //}
                    rowIndex++;
                }

                ViewState["CurrentTable"] = dtCurrentTable;
            }
        }
        else
        {
            Response.Write("ViewState is null");
        }
        //SetPreviousData();
    }
    protected void Grid_Patent_RowDeleting(Object sender, GridViewDeleteEventArgs e)
    {

        SetRowData();
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            DataRow drCurrentRow = null;
            int rowIndex = Convert.ToInt32(e.RowIndex);

            if (dt.Rows.Count > 1 && rowIndex != 0)
            {
                dt.Rows.Remove(dt.Rows[rowIndex]);
                drCurrentRow = dt.NewRow();
                ViewState["CurrentTable"] = dt;
                Grid_AuthorEntry.DataSource = dt;
                Grid_AuthorEntry.DataBind();

                SetPreviousData();
                // gridAmtChanged(sender, e);
            }


        }
    }
    protected void NationalTypeOnSelectedIndexChanged(object sender, EventArgs e)
    {
    }
    protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //Find the DropDownList in the Row
            DropDownList DropdownMuNonMu = (e.Row.FindControl("DropdownMuNonMu") as DropDownList);

            //if (DropDownListPublicationEntry.SelectedValue == "BK" || DropDownListPublicationEntry.SelectedValue == "CP" || DropDownListPublicationEntry.SelectedValue == "NM")
            //{


            SqlDataSourceAuthorType.SelectCommand = "SELECT Id,Type FROM [Author_Type_M] where (Id! = 'O')";

            DropdownMuNonMu.DataTextField = "Type";
            DropdownMuNonMu.DataValueField = "Id";
            DropdownMuNonMu.DataBind();


           

        }

    }
    protected void popgridApp_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        PatentBusiness PatBus_obj = new PatentBusiness();
        Patent pat = new Patent();
        if (e.CommandName == "DeleteRow")
        {
            GridViewRow rowSelect = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
            int rowindex = rowSelect.RowIndex;
            string id = popgridApp.Rows[rowindex].Cells[0].Text.ToString();
            Label status = (Label)popgridApp.Rows[rowindex].FindControl("lblstatus");
            Label entryno = (Label)popgridApp.Rows[rowindex].FindControl("lblentryno");

            //string status = popgridApp.Rows[rowindex].Cells[5].Text.ToString();
            // string entryno = popgridApp.Rows[rowindex].Cells[6].Text.ToString();
            // string prodcutid = id.Text;
            string pid = PatBus_obj.deleteid(id, status.Text, entryno.Text);
            setModalWindowApp(sender, e);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "CallMyFunction", "ToggleDisplay2()", true);
            //  ScriptManager.RegisterStartupScript(this, this.GetType(), "CallMyFunction", "callthis2()", true);
        }
    }

    protected void popSelected1(Object sender, EventArgs e)
    {
        UpdatePanel2.Update();
        up.Update();
        popGridAffil.Visible = true;
        GridViewRow row = popGridAffil.SelectedRow;

        string EmployeeCode1 = row.Cells[1].Text;
        TextBox senderBox = sender as TextBox;


        string rowVal1 = rowVal.Value;
        int rowIndex = Convert.ToInt32(rowVal1);



        affiliateSrch.Text = "";
        popGridAffil.DataBind();

        Business b = new Business();
        User a = new User();

        a = b.GetUserName(EmployeeCode1);


        string InstituteName1 = null;
        InstituteName1 = b.GetInstitutionName(a.InstituteId);


        string deptName1 = null;
        deptName1 = b.GetDeptName(a.Department, a.InstituteId);


        TextBox InstitutionName = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("InstitutionName");
        InstitutionName.Text = InstituteName1;

        TextBox DepartmentName = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("DepartmentName");
        DepartmentName.Text = deptName1;

        TextBox mailid = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("MailId");
        mailid.Text = a.emailId;

        HiddenField Institution = (HiddenField)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("Institution");
        Institution.Value = a.InstituteId;

        HiddenField Department = (HiddenField)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("Department");
        Department.Value = a.Department;

        TextBox employc = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("EmployeeCode");
        employc.Text = EmployeeCode1;

        TextBox AuthorName = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("AuthorName");
        AuthorName.Text = a.UserNamePrefix + " " + a.UserFirstName + " " + a.UserMiddleName + " " + a.UserLastName;


        affiliateSrch.Text = "";
        popGridAffil.DataBind();
        //  popupPanelAffil.Visible = false;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "CallMyFunction", "ToggleDisplay()", true);
        //HtmlAnchor anchor = new HtmlAnchor();
        ////anchor.Href = "#"; ;
        ////Page.Controls.Add(anchor);
        //anchor.Attributes["href"] = "#";
        //Page.Controls.Add(anchor);
        // css.Attributes["href"] = "#";
    }
    protected void OnselectFilingStatus(object sender, EventArgs e)
    {
        if (ddlFilingstatus.SelectedValue == "REJ")
        {
            txtRejectionRemark.Visible = false;
            lblRejectRemarks.Visible = true;
            Panelfilling.Enabled = false;
            Btnsave.Enabled = true;
           // Btnsubmit.Visible = false;
            ddlFilingstatus.Enabled = true;
            PoppanelRenewal.Visible = false;
            popupPanelAffil.Visible = false;
            //BtnDraft.Visible = false;
            Btnsave.Visible = true;
           // RequiredFieldValidator3.Enabled = false;
            //RequiredFieldValidator4.Enabled = true;
            lblpatent.Visible = false;
            lblgdate.Visible = false;
            panAddAuthor.Enabled = false;
            ddlNatureofPatent.Enabled = false;
            txtTitle.Enabled = false;
            txtde.Enabled = false;
            ddlFunding.Enabled = false;
            txtRemark.Visible = false;
            tdremarks.Visible = false;

        }
        else if (ddlFilingstatus.SelectedValue == "WDN")
        {
            txtRejectionRemark.Visible = false;
            lblRejectRemarks.Visible = true;
            lblRejectRemarks.Text = "Remarks";
            //RequiredFieldValidator3.Enabled = false;
            //RequiredFieldValidator4.Enabled = true;
            lblpatent.Visible = false;
            lblgdate.Visible = false;
            txtRemark.Visible = false;
            tdremarks.Visible = false;
            //BtnDraft.Visible = false;
            Btnsave.Visible = true;
        }
        if (ddlFilingstatus.SelectedValue == "GRN")
        {
            txtRejectionRemark.Visible = false;
            lblRejectRemarks.Visible = false;
            PoppanelRenewal.Visible = false;
            Btnsave.Enabled = true;
            txtRemark.Visible = true;
            tdremarks.Visible = true;
            //  Btnsubmit.Visible = true;
            Btnsave.Visible = true;
            //BtnDraft.Visible = false;
            txtGrantDate.Enabled = true;
            txtPatentNo.Enabled = true;
            // txtlastRenewal.Enabled = true;
            btnRenewalview.Enabled = false;
            BtnAPPsave.Enabled = false;
            //RequiredFieldValidator3.Enabled = true;
            //RequiredFieldValidator4.Enabled = false;
            lblpatent.Visible = true;
            lblgdate.Visible = true;

            ddlNatureofPatent.Enabled = false;
            ddlFunding.Enabled = false;
            txtTitle.Enabled = false;
            txtde.Enabled = false;
            panAddAuthor.Enabled = false;
            ddlfilingoffice.Enabled = false;
            txtapplicationNo.Enabled = false;
            btnview.Text = "View Application Stage Details";
            txtdateofApplication.Enabled = false;

            //RequiredFieldValidator3.Enabled = true;
            //RequiredFieldValidator4.Enabled = false;
        }
    }
    protected void RadioButtonList2_SelectedIndexChanged(object sender, EventArgs e)
    {
    }
    protected void SearchStudentData(object sender, EventArgs e)
    {
    }
    protected void StudentDataSelect(Object sender, EventArgs e)
    {
    }
    protected void showpopup(object sender, EventArgs e)
    {
       
        ScriptManager.RegisterStartupScript(this, this.GetType(), "CallMyFunction", "callthis1()", true);
        setModalWindow(sender, e);

    }
    protected void Btn_Save(object sender, EventArgs e)
    {
     if (!Page.IsValid)
        {
            return;
        }

        try
        {

            PatentBusiness Pat_Busobj = new PatentBusiness();
            Patent pat = new Patent();


            if (ddlFilingstatus.SelectedValue == "GRN")
            {
                if (txtGrantDate.Text.ToString() == "")
                {
                    //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Enter Grant date !')</script>");
                    //return;
                    string CloseWindow1 = "alert('Enter Grant date !')";
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow1, true);
                    return;
                }
                if (txtPatentNo.Text.ToString() == "")
                {
                    //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Enter Grant date !')</script>");
                    //return;
                    string CloseWindow1 = "alert('Enter patent number !')";
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow1, true);
                    return;
                }

                if (ddlNatureofPatent.SelectedValue == "2")
                {
                    string CloseWindow;
                    CloseWindow = "alert('Patent cannot be submitted because nature of patent is provisional')";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);
                    ddlNatureofPatent.Enabled = true;
                    return;
                }
            }
            //if (ddlFilingstatus.SelectedValue == "REJ")
            //{
            //    if (ViewState["App_Status"] != null)
            //    {
            //        pat.App_Status = ViewState["App_Status"].ToString();
            //    }

            //    if (txtRejectionRemark.Text == "")
            //    {
            //        ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please enter the remarks')</script>");
            //        return;
            //    }

            //}


            pat.ID = txtID.Text;
            pat.Title = txtTitle.Text;
            pat.description = txtde.Text;
            pat.Pat_UTN = txtPatUTN.Text;
            pat.Funding = Convert.ToByte(ddlFunding.SelectedValue.ToString());
            pat.NatureOfPatent = Convert.ToByte(ddlNatureofPatent.SelectedValue.ToString());
            if (txtdateofApplication.Text.ToString() != "")
            {
                pat.Date_Of_Application = DateTime.ParseExact(txtdateofApplication.Text, "dd/MM/yyyy", null);

            }
            if (ddlFilingstatus.SelectedValue == "APP")
            {
                pat.Entry_status = "DRAFT";
            }
            else
            {
                pat.Entry_status = "SUB";
            }
            // string entrystatus = null;


            pat.Filing_Status = ddlFilingstatus.SelectedValue.ToString();
            pat.Filing_Office = ddlfilingoffice.SelectedValue.ToString();
            pat.Application_Stage = txtApplicationStage.Text;
            //pat.Provisional_Number = txtProvisionalNo.Text;
            //if (txtFilingDateProvided.Text.ToString() != "")
            //{
            //    pat.FilingDateprovidedPatent = DateTime.ParseExact(txtFilingDateProvided.Text, "dd/MM/yyyy", null);

            //}
            //  pat.FilingDateprovidedPatent =Convert.ToDateTime(txtFilingDateProvided.Text);
            pat.Patent_Number = txtPatentNo.Text;
            pat.Application_Number = txtapplicationNo.Text;
            if (txtGrantDate.Text.ToString() != "")
            {
                pat.Grant_Date = DateTime.ParseExact(txtGrantDate.Text, "dd/MM/yyyy", null);

            }
            if (ddlFilingstatus.SelectedValue == "GRN")
            {
                DateTime nextrenewaldate = pat.Grant_Date;
                pat.NextRenewalDate = nextrenewaldate.AddYears(1);
                pat.Renewal_Fee = txtRenewalFee.Text;
            }
            //if (txtlastRenewalFee.Text.ToString() != "")
            //{
            //    pat.LastRenewalFeePaiddate = DateTime.ParseExact(txtlastRenewalFee.Text, "dd/MM/yyyy", null);

            //}
            //if (txtlastRenewal.Text.ToString() != "")
            //{

            //    pat.LastRenewalFeePaiddate = Convert.ToDateTime(txtlastRenewal.Text);
            //}
            pat.Remarks = txtRemark.Text;
            //pat.Renewal_Comment = txtRenewalComment.Text;
            //pat.RejectedBy = Session["UserId"].ToString();

            //pat.Rejection_Remark = txtRejectionRemark.Text;

            GrantData j = new GrantData();

            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
            GrantData[] JD = new GrantData[dtCurrentTable.Rows.Count];
            pat.Created_Date = DateTime.Now;
            pat.Created_By = Session["UserId"].ToString();
            int rowIndex1 = 0;
            if (dtCurrentTable.Rows.Count > 0)
            {

                for (int i = 0; i < dtCurrentTable.Rows.Count; i++)
                {
                    JD[i] = new GrantData();
                    TextBox AuthorName = (TextBox)Grid_AuthorEntry.Rows[rowIndex1].Cells[1].FindControl("AuthorName");
                    DropDownList DropdownMuNonMu = (DropDownList)Grid_AuthorEntry.Rows[rowIndex1].Cells[2].FindControl("DropdownMuNonMu");
                    TextBox EmployeeCode = (TextBox)Grid_AuthorEntry.Rows[rowIndex1].Cells[0].FindControl("EmployeeCode");
                    HiddenField Institution = (HiddenField)Grid_AuthorEntry.Rows[rowIndex1].Cells[0].FindControl("Institution");
                    TextBox InstitutionName = (TextBox)Grid_AuthorEntry.Rows[rowIndex1].Cells[6].FindControl("InstitutionName");
                    HiddenField Department = (HiddenField)Grid_AuthorEntry.Rows[rowIndex1].Cells[0].FindControl("Department");
                    TextBox DepartmentName = (TextBox)Grid_AuthorEntry.Rows[rowIndex1].Cells[0].FindControl("DepartmentName");
                    TextBox MailId = (TextBox)Grid_AuthorEntry.Rows[rowIndex1].Cells[0].FindControl("MailId");
                    DropDownList AuthorType = (DropDownList)Grid_AuthorEntry.Rows[rowIndex1].Cells[0].FindControl("AuthorType");
                    DropDownList DropdownStudentInstitutionName = (DropDownList)Grid_AuthorEntry.Rows[rowIndex1].Cells[0].FindControl("DropdownStudentInstitutionName");
                    DropDownList DropdownStudentDepartmentName = (DropDownList)Grid_AuthorEntry.Rows[rowIndex1].Cells[0].FindControl("DropdownStudentDepartmentName");

                    DropDownList NationalType = (DropDownList)Grid_AuthorEntry.Rows[rowIndex1].Cells[0].FindControl("NationalType");
                    DropDownList ContinentId = (DropDownList)Grid_AuthorEntry.Rows[rowIndex1].Cells[0].FindControl("ContinentId");

                    if (AuthorName.Text == "")
                    {
                        ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please enter Investigator Name!')</script>");
                        return;

                    }

                    if (DropdownMuNonMu.SelectedValue == "M")
                    {
                        if (InstitutionName.Text == "")
                        {
                            ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please enter Institution Name!')</script>");
                            return;

                        }

                        if (DepartmentName.Text == "")
                        {
                            ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please enter Department Name!')</script>");
                            return;

                        }
                    }
                    else if (DropdownMuNonMu.SelectedValue == "N")
                    {
                        if (InstitutionName.Text == "")
                        {
                            ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please enter Institution Name!')</script>");
                            return;

                        }

                        if (DepartmentName.Text == "")
                        {
                            ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please enter Department Name!')</script>");
                            return;

                        }
                    }
                    if (MailId.Text == "")
                    {
                        ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please enter MailId!')</script>");
                        return;

                    }


                    JD[i].AuthorName = AuthorName.Text.Trim();
                    JD[i].MUNonMU = DropdownMuNonMu.Text.Trim();
                    if (JD[i].MUNonMU == "M" || JD[i].MUNonMU == "S")
                    {
                        JD[i].EmployeeCode = EmployeeCode.Text;
                    }
                    else
                    {
                        JD[i].EmployeeCode = AuthorName.Text.Trim();
                    }
                    if (JD[i].MUNonMU == "N")
                    {

                        JD[i].NationalInternationl = NationalType.SelectedValue;
                        if (NationalType.SelectedValue == "I")
                        {

                            JD[i].continental = ContinentId.SelectedValue;
                        }
                        else
                        {
                            JD[i].continental = "";
                        }

                        JD[i].Institution = InstitutionName.Text.Trim();
                        JD[i].InstitutionName = InstitutionName.Text.Trim();
                        JD[i].Department = DepartmentName.Text.Trim();
                        JD[i].DepartmentName = DepartmentName.Text.Trim();
                        JD[i].AppendInstitutions = JD[i].Institution;
                    }
                    else if (JD[i].MUNonMU == "M")
                    {
                        JD[i].NationalInternationl = "";
                        JD[i].continental = "";

                        JD[i].Institution = Institution.Value.Trim();
                        JD[i].InstitutionName = InstitutionName.Text.Trim();
                        JD[i].Department = Department.Value.Trim();
                        JD[i].DepartmentName = DepartmentName.Text.Trim();
                        JD[i].AppendInstitutions = JD[i].Institution;

                    }
                    else if (JD[i].MUNonMU == "S")
                    {

                        JD[i].NationalInternationl = "";
                        JD[i].continental = "";
                        JD[i].Institution = DropdownStudentInstitutionName.SelectedValue;

                        JD[i].InstitutionName = DropdownStudentInstitutionName.SelectedItem.ToString();
                        JD[i].Department = DropdownStudentDepartmentName.SelectedValue;
                        JD[i].DepartmentName = DropdownStudentDepartmentName.SelectedItem.ToString();
                        JD[i].AppendInstitutions = JD[i].Institution;

                    }

                    JD[i].AppendInstitutionNames = JD[i].InstitutionName;
                    JD[i].EmailId = MailId.Text.Trim();
                    j.PiInstId = Session["InstituteId"].ToString();
                    j.PiDeptId = Session["Department"].ToString();
                    j.MUNonMUUTN = "MUTN";
                    j.PiInstId = Institution.Value.Trim();
                    j.PiDeptId = Department.Value.Trim();
                    rowIndex1++;
                }

            }
            ArrayList list1 = new ArrayList();
            for (int i = 0; i < JD.Length; i++)
            { //validation
                if (list1.Contains(JD[i].EmployeeCode))
                {
                    ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please remove duplicate authors from the list')</script>");
                    return;
                }
                else
                {
                    list1.Add(JD[i].EmployeeCode);
                }
                //validation
                list1.Add(JD[i].EmployeeCode);
            }
            bool result = false;

            //if (ddlFilingstatus.SelectedValue == "LAP")
            //{

            //    DateTime renewalate = Convert.ToDateTime(txtlastRenewal.Text);
            //    PatentBusiness Bus_obj = new PatentBusiness();
            //    Patent p = new Patent();
            //    p = Bus_obj.SelectRenewalDate(pat.ID);


            //    TimeSpan ts = p.NextRenewalDate.Subtract(renewalate);
            //    int months = Convert.ToInt16(Math.Round((double)(ts.Days) / 30.0));
            //    //  int result = DateTime.Compare(renewalate, p.NextRenewalDate);
            //    if (months <= 18)
            //    {
            //        pat.Status = "GRN";
            //        pat.NextRenewalDate = p.NextRenewalDate.AddYears(2);
            //        result = Pat_Busobj.UpdateStatus(pat);
            //        if (result == true)
            //        {
            //            string a = Session["patentseed"].ToString();
            //            txtID.Text = a;
            //            txtPatUTN.Text = Session["patentseedUTNseed"].ToString();
            //            ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Patent Data Created Successfully..  For update Click on search and edit  !')</script>");
            //            log.Info("Patent created Successfully, of ID: " + txtID.Text);
            //            //txtPatUTN.Text = Session["GrantseedUTNseed"].ToString();
            //            BtnDraft.Visible = true;
            //            Btnsave.Visible = false;
            //            btnview.Enabled = true;
            //        }
            //        else
            //        {
            //            log.Error("Grant creation Error of ID: " + txtPatUTN.Text);
            //            ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Grant Error')</script>");
            //        }
            //    }

            //}


            if (ddlFilingstatus.SelectedValue == "APP")
            {
                if (txtID.Text == "")
                {
                    result = Pat_Busobj.InsertPatent(pat, JD);
                    if (result == true)
                    {
                        string a = Session["patentseed"].ToString();
                        txtID.Text = a;
                        txtPatUTN.Text = Session["patentseedUTNseed"].ToString();
                        //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Patent Data Created Successfully..  For update Click on search and edit  !')</script>");

                       // BtnDraft.Visible = true;
                        Btnsave.Visible = false;
                        btnview.Enabled = true;
                        btnview.Text = "Add Application Stage Details";
                        string CloseWindow;
                        CloseWindow = "alert('Patent Data Created Successfully..  For update Click on search and edit  !')";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);
                        //EmailDetails details = new EmailDetails();
                        //details = SendMail(txtID.Text);
                        //SendMailObject obj1 = new SendMailObject();
                        //bool result1 = obj1.InsertIntoEmailQueue(details);
                    }
                    else
                    {
                        string CloseWindow;
                        CloseWindow = "alert('Grant Error')";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);
                        // ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Grant Error')</script>");
                    }
                }
                else
                {
                    PatentBusiness PatBus_obj = new PatentBusiness();
                    string entrystatus = ViewState["EntryStatus"].ToString();
                    if (entrystatus == "SUB")
                    {
                        pat.Entry_status = "SUB";
                    }
                    else
                    {
                        pat.Entry_status = "DRAFT";
                    }
                    result = PatBus_obj.UpdatePatent(pat, JD);

                    if (result == true)
                    {
                        //BtnDraft.Visible = false;
                        //Btnsubmit.Visible = false;
                        Btnsave.Visible = true;
                        //SqlDataSourePatentStatus.SelectCommand = "Select * from Patent_Status where Id!='CAN' and Id!='EXP' and Id!='LAP'";
                        //ddlFilingstatus.DataSourceID = "SqlDataSourePatentStatus";
                        //ddlFilingstatus.DataBind();
                        //  ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Patent Updated!')</script>");
                        string CloseWindow;
                        CloseWindow = "alert('Patent Updated!')";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);
                        return;

                    }
                }

            }

            else if (ddlFilingstatus.SelectedValue == "REJ")
            {
                pat.Rejection_Remark = txtRejectionRemark.Text;
                pat.RejectedBy = Session["UserId"].ToString();
                result = Pat_Busobj.UpdateStatusPatentRejectApproval(pat, JD);
                string CloseWindow;
                CloseWindow = "alert('Patent Data Rejected Successfully. !')";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);
                // ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Patent Data Rejected Successfully. !')</script>");
                Btnsave.Enabled = false;
                //Btnsubmit.Enabled = false;
                Panelfilling.Enabled = false;
            }
            else if (ddlFilingstatus.SelectedValue == "WDN")
            {
                pat.Rejection_Remark = txtRejectionRemark.Text;
                pat.RejectedBy = Session["UserId"].ToString();
                result = Pat_Busobj.UpdateStatusPatentRejectApproval(pat, JD);
                string CloseWindow;
                CloseWindow = "alert('Patent Data Withdrawn Successfully. !')";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);
                // ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Patent Data Rejected Successfully. !')</script>");
                Btnsave.Enabled = false;
               // Btnsubmit.Enabled = false;
                Panelfilling.Enabled = false;
            }

            //if (ddlFilingstatus.SelectedValue == "APP" || ddlFilingstatus.SelectedValue == "GRN")
            //if (ViewState["EntryStatus"].ToString() == "SUB")
            //{


            //}
            if (ddlFilingstatus.SelectedValue == "GRN")
            {
                pat.Patent_Number = txtPatentNo.Text;
                pat.NextRenewalDate = Convert.ToDateTime(txtGrantDate.Text).AddYears(1);
                pat.Entry_status = "SUB";
                result = Pat_Busobj.UpdatePatent(pat, JD);
                ////if (ddlFilingstatus.SelectedValue == "GRN")
                ////{

                // result = Pat_Busobj.UpdateGrantPatent(pat, JD);

                if (result == true)
                {
                    if (ddlFilingstatus.SelectedValue == "GRN")
                    {
                        SqlDataSourePatentStatus.SelectCommand = "Select * from Patent_Status where Id!='CAN' and Id !='APP' and Id!='REJ'  and Id!='LAP' and  Id!='EXP' ";
                        ddlFilingstatus.DataSourceID = "SqlDataSourePatentStatus";
                        // ddlFilingstatus.DataBind();
                        ddlFilingstatus.SelectedValue = "GRN";
                        btnRenewalview.Enabled = true;
                        //Btnsubmit.Enabled = true;
                        Btnsave.Visible = true;
                        Btnsave.Enabled = true;
                        //BtnDraft.Visible = false;
                        ddlNatureofPatent.Enabled = false;
                        btnRenewalview.Text = "Add/View Renewal Details";

                        //EmailDetails details = new EmailDetails();
                        //details = SendMail(txtID.Text);
                        //SendMailObject obj1 = new SendMailObject();
                        //bool result1 = obj1.InsertIntoEmailQueue(details);

                        string CloseWindow;
                        CloseWindow = "alert('Patent Data Updated Successfully.!')";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);
                        // ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Patent Data Updated Successfully. !')</script>");
                        return;
                    }
                }
            }



        }
        catch (Exception ex)
        {
            //log.Error("InsertPatentData catch block of patent IDpate: ");
            //log.Error(ex.Message);
            //log.Error(ex.StackTrace);
            //transaction.Rollback();
            throw ex;
        }
    }
    protected void Btn_App_View(object sender, EventArgs e)
    {
        lblnoteApp.Visible = false;
        setModalWindowApp(sender, e);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "CallMyFunction", "callthis2()", true);
    }
    protected void AuthorNameChanged(object sender, EventArgs e)
    {
        if (affiliateSrch.Text.Trim() == "")
        {
            SqlDataSourceAffil.SelectCommand = "SELECT top 10  User_Id, prefix+' '+UPPER(firstname)+' '+UPPER(middlename)+' '+UPPER(lastname)  as Name from User_M";
            popGridAffil.DataBind();
            popGridAffil.Visible = true;
        }

        else
        {
            string name = affiliateSrch.Text.Replace(" ", String.Empty);
            //sqli
            SqlDataSourceAffil.SelectParameters.Clear();
            SqlDataSourceAffil.SelectParameters.Add("name", name);

            //SqlDataSourceAffil.SelectCommand = "SELECT  User_Id,prefix+' '+firstname+' '+middlename+' '+lastname  as Name from User_M where prefix+firstname+middlename+lastname like '%" + name + "%'";
            SqlDataSourceAffil.SelectCommand = "SELECT  User_Id,prefix+' '+firstname+' '+middlename+' '+lastname  as Name from User_M where prefix+firstname+middlename+lastname like '%' + @name + '%'";


            popGridAffil.DataBind();
            popGridAffil.Visible = true;
        }

        //string rowVal = Request.Form["rowIndx"];
        //int rowIndex = Convert.ToInt32(rowVal);

        //ModalPopupExtender ModalPopupExtender8 = (ModalPopupExtender)Grid_AuthorEntry.Rows[rowIndex].FindControl("ModalPopupExtender4");
        //ModalPopupExtender8.Show();

    }
    private void SetPreviousData()
    {
        int rowIndex = 0;
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    TextBox AuthorName = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[1].FindControl("AuthorName");
                    DropDownList DropdownMuNonMu = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[2].FindControl("DropdownMuNonMu");
                    //TextBox amount = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[3].FindControl("amount");
                    TextBox EmployeeCode = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("EmployeeCode");
                    HiddenField Institution = (HiddenField)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("Institution");
                    TextBox InstitutionName = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[6].FindControl("InstitutionName");
                    HiddenField Department = (HiddenField)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("Department");
                    TextBox DepartmentName = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("DepartmentName");
                    TextBox MailId = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("MailId");

                    //DropDownList AuthorType = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("AuthorType");

                    DropDownList DropdownStudentInstitutionName = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("DropdownStudentInstitutionName");
                    DropDownList DropdownStudentDepartmentName = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("DropdownStudentDepartmentName");

                    ImageButton EmployeeCodeBtnImg = (ImageButton)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("EmployeeCodeBtn");

                    DropDownList NationalType = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("NationalType");
                    DropDownList ContinentId = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("ContinentId");

                    TextBox AuthorName1 = (TextBox)Grid_AuthorEntry.Rows[0].Cells[1].FindControl("AuthorName");
                    DropDownList DropdownMuNonMu1 = (DropDownList)Grid_AuthorEntry.Rows[0].Cells[2].FindControl("DropdownMuNonMu");
                    ImageButton EmployeeCodeBtnImg1 = (ImageButton)Grid_AuthorEntry.Rows[0].Cells[0].FindControl("EmployeeCodeBtn");

                    //TextBox EmployeeCode1 = (TextBox)Grid_AuthorEntry.Rows[0].Cells[0].FindControl("EmployeeCode");
                    HiddenField Institution1 = (HiddenField)Grid_AuthorEntry.Rows[0].Cells[0].FindControl("Institution");
                    TextBox InstitutionName1 = (TextBox)Grid_AuthorEntry.Rows[0].Cells[6].FindControl("InstitutionName");
                    HiddenField Department1 = (HiddenField)Grid_AuthorEntry.Rows[0].Cells[0].FindControl("Department");
                    TextBox DepartmentName1 = (TextBox)Grid_AuthorEntry.Rows[0].Cells[0].FindControl("DepartmentName");
                    TextBox MailId1 = (TextBox)Grid_AuthorEntry.Rows[0].Cells[0].FindControl("MailId");


                    // DropDownList AuthorType1 = (DropDownList)Grid_AuthorEntry.Rows[0].Cells[0].FindControl("AuthorType");
                    DropDownList DropdownStudentInstitutionName1 = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("DropdownStudentInstitutionName");
                    DropDownList DropdownStudentDepartmentName1 = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("DropdownStudentDepartmentName");
                    ImageButton ImageButton1 = (ImageButton)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("ImageButton1");


                    DropdownMuNonMu.Text = dt.Rows[i]["DropdownMuNonMu"].ToString();
                    AuthorName.Text = dt.Rows[i]["AuthorName"].ToString();
                    EmployeeCode.Text = dt.Rows[i]["EmployeeCode"].ToString();

                    if (DropdownMuNonMu.Text == "M")
                    {
                        AuthorName.Enabled = false;
                        InstitutionName.Enabled = false;
                        DepartmentName.Enabled = false;
                        MailId.Enabled = false;

                        EmployeeCodeBtnImg.Enabled = true;

                        DropdownStudentInstitutionName.Visible = false;
                        DropdownStudentDepartmentName.Visible = false;
                        InstitutionName.Visible = true;
                        DepartmentName.Visible = true;

                        NationalType.Visible = false;
                        ContinentId.Visible = false;

                        NationalType.Text = dt.Rows[i]["NationalType"].ToString();
                        //   ContinentId.Text = dt.Rows[i]["ContinentId"].ToString();

                        Institution.Value = dt.Rows[i]["Institution"].ToString();
                        InstitutionName.Text = dt.Rows[i]["InstitutionName"].ToString();
                        Department.Value = dt.Rows[i]["Department"].ToString();
                        DepartmentName.Text = dt.Rows[i]["DepartmentName"].ToString();
                    }

                    else if (DropdownMuNonMu.Text == "N")
                    {
                        AuthorName.Enabled = true;
                        InstitutionName.Enabled = true;
                        DepartmentName.Enabled = true;
                        MailId.Enabled = true;

                        EmployeeCodeBtnImg.Enabled = false;

                        NationalType.Visible = true;
                        NationalType.Text = dt.Rows[i]["NationalType"].ToString();
                        //  ContinentId.Text = dt.Rows[i]["ContinentId"].ToString();


                        if (NationalType.Text == "I")
                        {
                            ContinentId.Visible = true;
                        }
                        else
                        {
                            ContinentId.Visible = false;
                        }

                        DropdownStudentInstitutionName.Visible = false;
                        DropdownStudentDepartmentName.Visible = false;
                        InstitutionName.Visible = true;
                        DepartmentName.Visible = true;

                        Institution.Value = dt.Rows[i]["Institution"].ToString();
                        InstitutionName.Text = dt.Rows[i]["InstitutionName"].ToString();
                        Department.Value = dt.Rows[i]["Department"].ToString();
                        DepartmentName.Text = dt.Rows[i]["DepartmentName"].ToString();
                    }
                    else if (DropdownMuNonMu.Text == "S")
                    {
                        DropdownStudentInstitutionName1.Visible = false;
                        DropdownStudentDepartmentName.Visible = false;
                        InstitutionName.Visible = true;
                        DepartmentName.Visible = true;
                        EmployeeCodeBtnImg.Enabled = false;
                        EmployeeCodeBtnImg.Visible = false;
                        ImageButton1.Visible = true;
                        AuthorName.Enabled = false;
                        InstitutionName.Enabled = false;
                        DepartmentName.Enabled = false;

                        NationalType.Visible = false;
                        ContinentId.Visible = false;

                        NationalType.Text = dt.Rows[i]["NationalType"].ToString();
                        //   ContinentId.Text = dt.Rows[i]["ContinentId"].ToString();

                        EmployeeCodeBtnImg.Enabled = false;

                        DropdownStudentInstitutionName.Visible = true;
                        DropdownStudentDepartmentName.Visible = true;
                        InstitutionName.Visible = false;
                        DepartmentName.Visible = false;
                        //  Institution.Value = dt.Rows[i]["Institution"].ToString();
                        DropdownStudentInstitutionName.SelectedValue = dt.Rows[i]["Institution"].ToString();
                        //  Department.Value = dt.Rows[i]["Department"].ToString();
                        DropdownStudentDepartmentName.SelectedValue = dt.Rows[i]["Department"].ToString();
                    }
                    MailId.Text = dt.Rows[i]["MailId"].ToString();
                    // AuthorType.Text = dt.Rows[i]["AuthorType"].ToString();

                    AuthorName1.Enabled = false;
                    EmployeeCodeBtnImg1.Enabled = false;
                    DropdownMuNonMu1.Enabled = false;
                    InstitutionName1.Enabled = false;
                    //  Department1.Enabled = false;
                    DepartmentName1.Enabled = false;
                    MailId1.Enabled = false;

                    rowIndex++;
                }
            }
        }
    }

    private void setModalWindowStudent(object sender, EventArgs e)
    {
        popupStudentGrid.DataSourceID = "StudentSQLDS";
        StudentSQLDS.DataBind();
        popupStudentGrid.DataBind();
    }

    private void setModalWindow(object sender, EventArgs e)
    {
        UpdatePanel2.Update();
        popupPanelAffil.Visible = true;
        popGridAffil.DataSourceID = "SqlDataSourceAffil";
        SqlDataSourceAffil.DataBind();
        popGridAffil.DataBind();
        int rows = popGridAffil.Rows.Count;
        popGridAffil.Visible = true;
    }

    private void setModalWindowRenewal(object sender, EventArgs e)
    {
        grdRenewal.DataBind();
        grdRenewal.DataSourceID = "sqlRenewal";
        sqlRenewal.DataBind();
        PoppanelRenewal.Visible = true;
    }

    private void setModalWindowApp(object sender, EventArgs e)
    {
        popgridApp.DataBind();
        popgridApp.DataSourceID = "SqlDataSource5";
        SqlDataSource5.DataBind();
        PopAppStage.Visible = true;
    }
    protected void GridProTocomplete_RowEditing(object sender, GridViewEditEventArgs e)
    {
        PatentBusiness Pat_Busobj = new PatentBusiness();
        GridProTocomplete.EditIndex = e.NewEditIndex;
        ClearPatent();
        SelectPatent();
        btnview.Enabled = true;
        if (RadioButtonList1.SelectedValue == "PtoC")
        {
            DataSet ds = new DataSet();
            ds = Pat_Busobj.SelectProTocompletelist(RadioButtonList1.SelectedValue);

            GridProTocomplete.Visible = true;
            GridGRNtoRenewal.Visible = false;
            GridProTocomplete.DataSource = ds.Tables[0];
            GridProTocomplete.DataBind();
        }
        else if (RadioButtonList1.SelectedValue == "GtoR")
        {
            DataSet ds1 = new DataSet();
            ds1 = Pat_Busobj.SelectGRNtoRenewallist(RadioButtonList1.SelectedValue);

            GridGRNtoRenewal.Visible = true;
            GridProTocomplete.Visible = false;
            GridGRNtoRenewal.DataSource = ds1.Tables[0];
            GridGRNtoRenewal.DataBind();
        }
    }

    private void SelectPatent()
    {
        Panel1.Visible = true;
        panAddAuthor.Visible = true;
        Panel6.Visible = true;
        Patent Pat = new Patent();
        Patent_DAobject bus_obj = new Patent_DAobject();
        string ID = ViewState["PatentID"].ToString();
        Pat = bus_obj.SelectPatent(ID);
        //GridViewSearchPatent.Visible = false;
        txtID.Text = ID;
        txtPatUTN.Text = Pat.Pat_UTN;
        txtTitle.Text = Pat.Title;
        txtde.Text = Pat.description;
        ddlFunding.SelectedValue = Pat.Funding.ToString();
        ddlNatureofPatent.Text = Pat.NatureOfPatent.ToString();
        ddlfilingoffice.DataBind();
        ddlfilingoffice.SelectedValue = Pat.Filing_Office.ToString();

        if (Pat.Date_Of_Application.ToString() != "01/01/0001 00:00:00")
        {
            txtdateofApplication.Text = Pat.Date_Of_Application.ToShortDateString();
        }
        //txtProvisionalNo.Text = Pat.Provisional_Number;
        //if (Pat.FilingDateprovidedPatent.ToString() != "01/01/0001 00:00:00")
        //{
        //    txtFilingDateProvided.Text = Pat.FilingDateprovidedPatent.ToShortDateString();
        //}
        txtPatentNo.Text = Pat.Patent_Number;
        txtapplicationNo.Text = Pat.Application_Number;
        if (Pat.Grant_Date.ToString() != "01/01/0001 00:00:00")
        {
            txtGrantDate.Text = Pat.Grant_Date.ToShortDateString();
        }
        if (Pat.LastRenewalFeePaiddate.ToString() != "01/01/0001 00:00:00")
        {
            txtlastRenewal.Text = Pat.LastRenewalFeePaiddate.ToShortDateString();
        }

        SqlDataSourePatentStatus.SelectCommand = "Select * from Patent_Status";
        ddlFilingstatus.DataSourceID = "SqlDataSourePatentStatus";
        ddlFilingstatus.DataBind();


        if (Pat.Filing_Status == "APP" && Pat.Entry_status == "DRAFT")
        {
            Btnsave.Visible = false;
            //BtnDraft.Visible = true;
            //Btnsubmit.Visible = true;
            SqlDataSourePatentStatus.SelectCommand = "Select * from Patent_Status where Id in ('APP', 'REJ','WDN')";
            ddlFilingstatus.DataSourceID = "SqlDataSourePatentStatus";
            ddlFilingstatus.DataBind();
            ddlFilingstatus.SelectedValue = Pat.Filing_Status;
            popgridApp.DataSourceID = "SqlDataSource5";
            SqlDataSource5.DataBind();
            popgridApp.DataBind();
            if (popgridApp.Rows.Count > 0)
            {
                string appstatus = bus_obj.SelectApplicationStage(ID);
                txtApplicationStage.Text = appstatus;
                btnview.Text = "Add/View Application Stage Details";
                BtnAPPsave.Enabled = true;
            }
            else
            {
                txtApplicationStage.Text = "";
                btnview.Text = "Add Application Stage Details";
                //Btnsubmit.Visible = false;
                BtnAPPsave.Enabled = true;
            }
            ddlFilingstatus.SelectedValue = Pat.Filing_Status;

            ddlNatureofPatent.Enabled = true;
            ddlFunding.Enabled = true;
            txtTitle.Enabled = true;
            txtde.Enabled = true;
            panAddAuthor.Enabled = true;
            ddlfilingoffice.Enabled = false;
            txtapplicationNo.Enabled = false;
            btnview.Text = "Add/View Application Stage Details";
            txtdateofApplication.Enabled = false;
        }

        if (Pat.Entry_status == "SUB" && Pat.Filing_Status == "APP")
        {
            SqlDataSourePatentStatus.SelectCommand = "Select * from Patent_Status where Id in ('GRN','REJ','APP')";
            SqlDataSourePatentStatus.DataBind();
            ddlFilingstatus.DataSourceID = "SqlDataSourePatentStatus";
            ddlFilingstatus.DataBind();
            ddlFilingstatus.SelectedValue = Pat.Filing_Status;

            popgridApp.DataSourceID = "SqlDataSource5";
            SqlDataSource5.DataBind();
            popgridApp.DataBind();
            if (popgridApp.Rows.Count > 0)
            {
                string appstatus = bus_obj.SelectApplicationStage(ID);
                txtApplicationStage.Text = appstatus;
                btnview.Text = "Add/View Application Stage Details";
            }
            else
            {
                txtApplicationStage.Text = "";
                btnview.Text = "Add Application Stage Details";
                //Btnsubmit.Visible = false;
            }

            //BtnDraft.Visible = false;
            //Btnsubmit.Enabled = false;
            btnRenewalview.Enabled = false;
            BtnAPPsave.Enabled = false;
            popgridApp.Enabled = false;
            string appstatus2 = bus_obj.SelectApplicationStage(ID);
            txtApplicationStage.Text = appstatus2;
            txtGrantDate.Enabled = false;
            txtPatentNo.Enabled = false;
            ddlNatureofPatent.Enabled = true;
            ddlFunding.Enabled = true;
            txtTitle.Enabled = true;
            txtde.Enabled = true;
            panAddAuthor.Enabled = true;
            ddlfilingoffice.Enabled = false;
            txtapplicationNo.Enabled = false;
            btnview.Text = "View Application Stage Details";
            txtdateofApplication.Enabled = false;
        }

        if (Pat.Filing_Status == "GRN")
        {
            SqlDataSourePatentStatus.SelectCommand = "Select * from Patent_Status where Id!='CAN' and Id !='APP' and Id!='REJ'  and Id!='LAP'  and Id!='EXP'";
            ddlFilingstatus.DataSourceID = "SqlDataSourePatentStatus";
            ddlFilingstatus.DataBind();
            ddlFilingstatus.SelectedValue = Pat.Filing_Status;

            //BtnDraft.Visible = false;
            Btnsave.Enabled = true;
            btnRenewalview.Enabled = true;
            BtnAPPsave.Enabled = false;

            if (Pat.LastRenewalFeePaiddate.ToString() != "01/01/0001 00:00:00")
            {
                txtlastRenewal.Text = Pat.LastRenewalFeePaiddate.ToShortDateString();
            }

            string appstatus1 = bus_obj.SelectApplicationStage(ID);
            txtApplicationStage.Text = appstatus1;
            txtGrantDate.Enabled = true;
            txtPatentNo.Enabled = true;
            ddlFilingstatus.SelectedValue = Pat.Filing_Status;

            grdRenewal.DataSourceID = "sqlRenewal";
            sqlRenewal.DataBind();
            grdRenewal.DataBind();
            if (grdRenewal.Rows.Count > 0)
            {
                Patent pat = new Patent();
                pat = bus_obj.SelectPatent(ID);
                txtlastRenewal.Text = pat.LastRenewalFeePaiddate.ToShortDateString();
                btnRenewalview.Text = "Add/View Renewal Details";
            }
            else
            {
                txtlastRenewal.Text = "";
                btnRenewalview.Text = "Add Renewal Details";
            }

            if (Session["Role"].ToString() == "2")
            {
                ddlNatureofPatent.Enabled = true;
                ddlFunding.Enabled = true;
                txtTitle.Enabled = true;
                txtde.Enabled = true;
                panAddAuthor.Enabled = true;
                ddlfilingoffice.Enabled = false;
                txtapplicationNo.Enabled = false;
                btnview.Text = "View Application Stage Details";
                txtdateofApplication.Enabled = false;
            }
            else
            {
                ddlNatureofPatent.Enabled = false;
                ddlFunding.Enabled = false;
                txtTitle.Enabled = false;
                txtde.Enabled = false;
                panAddAuthor.Enabled = false;
                ddlfilingoffice.Enabled = false;
                txtapplicationNo.Enabled = false;
                btnview.Text = "View Application Stage Details";
                txtdateofApplication.Enabled = false;
            }

        }

        if (Pat.Filing_Status == "LAP")
        {
            SqlDataSourePatentStatus.SelectCommand = "Select * from Patent_Status where Id!='CAN' and Id in('LAP','GRN','EXP') ";
            ddlFilingstatus.DataSourceID = "SqlDataSourePatentStatus";
            ddlFilingstatus.DataBind();
            ddlFilingstatus.SelectedValue = Pat.Filing_Status;

            //BtnDraft.Visible = false;
            Btnsave.Enabled = true;
            btnRenewalview.Enabled = true;
            BtnAPPsave.Enabled = false;

            if (Pat.LastRenewalFeePaiddate.ToString() != "01/01/0001 00:00:00")
            {
                txtlastRenewal.Text = Pat.LastRenewalFeePaiddate.ToShortDateString();
            }

            string appstatus = bus_obj.SelectApplicationStage(ID);
            txtApplicationStage.Text = appstatus;
            txtGrantDate.Enabled = true;
            ddlFilingstatus.SelectedValue = Pat.Filing_Status;


            grdRenewal.DataSourceID = "sqlRenewal";
            sqlRenewal.DataBind();
            grdRenewal.DataBind();
            if (grdRenewal.Rows.Count > 0)
            {
                Patent pat = new Patent();
                pat = bus_obj.SelectPatent(ID);
                txtlastRenewal.Text = pat.LastRenewalFeePaiddate.ToShortDateString();
                btnRenewalview.Text = "View Renewal Details";
            }
            Btnsave.Enabled = false;
        }
        DataTable dy = bus_obj.fnPatentInventorDetails(ID);
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
                ImageButton EmployeeCodeBtnimg = (ImageButton)Grid_AuthorEntry.Rows[rowIndex].Cells[2].FindControl("EmployeeCodeBtn");
                TextBox AuthorName = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[2].FindControl("AuthorName");
                TextBox InstNme = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[2].FindControl("InstitutionName");
                TextBox deptname = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[2].FindControl("DepartmentName");
                HiddenField InstId = (HiddenField)Grid_AuthorEntry.Rows[rowIndex].Cells[2].FindControl("Institution");
                HiddenField deptId = (HiddenField)Grid_AuthorEntry.Rows[rowIndex].Cells[2].FindControl("Department");
                TextBox MailId = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[2].FindControl("MailId");
                // DropDownList isCorrAuth = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[2].FindControl("isCorrAuth");
                //DropDownList AuthorType = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[2].FindControl("AuthorType");
                DropDownList DropdownStudentInstitutionName = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[2].FindControl("DropdownStudentInstitutionName");
                DropDownList DropdownStudentDepartmentName = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[2].FindControl("DropdownStudentDepartmentName");
                DropDownList NationalType = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[2].FindControl("NationalType");
                DropDownList ContinentId = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[2].FindControl("ContinentId");
                DropDownList DropdownMuNonMu1 = (DropDownList)Grid_AuthorEntry.Rows[0].Cells[3].FindControl("DropdownMuNonMu");
                ImageButton EmployeeCodeBtnimg1 = (ImageButton)Grid_AuthorEntry.Rows[0].Cells[2].FindControl("EmployeeCodeBtn");



                drCurrentRow = dtCurrentTable.NewRow();

                DropdownMuNonMu.Text = dtCurrentTable.Rows[i - 1]["DropdownMuNonMu"].ToString();
                EmployeeCode.Text = dtCurrentTable.Rows[i - 1]["EmployeeCode"].ToString();
                AuthorName.Text = dtCurrentTable.Rows[i - 1]["AuthorName"].ToString();
                if (DropdownMuNonMu.Text == "M")
                {
                    NationalType.Visible = false;
                    ContinentId.Visible = false;

                    InstNme.Visible = true;
                    deptname.Visible = true;
                    DropdownStudentInstitutionName.Visible = false;
                    DropdownStudentDepartmentName.Visible = false;

                    InstNme.Text = dtCurrentTable.Rows[i - 1]["InstitutionName"].ToString();
                    deptname.Text = dtCurrentTable.Rows[i - 1]["DepartmentName"].ToString();
                    InstId.Value = dtCurrentTable.Rows[i - 1]["Institution"].ToString();
                    deptId.Value = dtCurrentTable.Rows[i - 1]["Department"].ToString();
                }

                else if (DropdownMuNonMu.Text == "N")
                {
                    InstNme.Visible = true;
                    deptname.Visible = true;
                    DropdownStudentInstitutionName.Visible = false;
                    DropdownStudentDepartmentName.Visible = false;
                    NationalType.Visible = true;

                    NationalType.SelectedValue = dtCurrentTable.Rows[i - 1]["NationalType"].ToString();

                    if (NationalType.SelectedValue == "N")
                    {
                        ContinentId.Visible = false;
                    }
                    else
                    {
                        ContinentId.Visible = true;
                    }
                    ContinentId.SelectedValue = dtCurrentTable.Rows[i - 1]["ContinentId"].ToString();
                    InstNme.Text = dtCurrentTable.Rows[i - 1]["InstitutionName"].ToString();
                    deptname.Text = dtCurrentTable.Rows[i - 1]["DepartmentName"].ToString();
                    InstId.Value = dtCurrentTable.Rows[i - 1]["Institution"].ToString();
                    deptId.Value = dtCurrentTable.Rows[i - 1]["Department"].ToString();
                }
                else if (DropdownMuNonMu.Text == "S")
                {
                    NationalType.Visible = false;
                    ContinentId.Visible = false;

                    InstNme.Visible = false;
                    deptname.Visible = false;
                    DropdownStudentInstitutionName.Visible = true;
                    DropdownStudentDepartmentName.Visible = true;

                    DropdownStudentInstitutionName.SelectedValue = dtCurrentTable.Rows[i - 1]["Institution"].ToString();
                    DropdownStudentDepartmentName.SelectedValue = dtCurrentTable.Rows[i - 1]["Department"].ToString();
                }
                DropdownMuNonMu1.Enabled = false;
                EmployeeCodeBtnimg1.Enabled = false;

                MailId.Text = dtCurrentTable.Rows[i - 1]["MailId"].ToString();
                //  AuthorType.Text = dtCurrentTable.Rows[i - 1]["AuthorType"].ToString();
                //isCorrAuth.Text = dtCurrentTable.Rows[i - 1]["isCorrAuth"].ToString();

                if (DropdownMuNonMu.Text == "N")
                {
                    EmployeeCodeBtnimg.Enabled = false;
                    AuthorName.Enabled = true;
                    InstNme.Enabled = true;
                    deptname.Enabled = true;
                    MailId.Enabled = true;
                }
                else if (DropdownMuNonMu.Text == "M")
                {
                    EmployeeCodeBtnimg.Enabled = true;
                    AuthorName.Enabled = false;
                    InstNme.Enabled = false;
                    deptname.Enabled = false;
                    MailId.Enabled = false;
                }
                else if (DropdownMuNonMu.Text == "S")
                {
                    EmployeeCodeBtnimg.Enabled = false;
                    AuthorName.Enabled = true;
                    DropdownStudentInstitutionName.Enabled = true;
                    DropdownStudentInstitutionName.Enabled = true;
                    MailId.Enabled = true;
                }
                else
                {
                    MailId.Enabled = true;
                }
                rowIndex++;

            }

            ViewState["CurrentTable"] = dtCurrentTable;
        }
    }

    private void ClearPatent()
    {
        ddlFilingstatus.ClearSelection();
        ddlNatureofPatent.ClearSelection();
        ddlFunding.ClearSelection();
        txtTitle.Text = "";
        txtde.Text = "";
        ddlfilingoffice.ClearSelection();
        txtApplicationStage.Text = "";
        txtdateofApplication.Text = "";
        txtapplicationNo.Text = "";
        //txtProvisionalNo.Text = "";
        txtPatentNo.Text = "";
        //txtFilingDateProvided.Text = "";
        txtGrantDate.Text = "";
        txtlastRenewal.Text = "";

    }
    protected void GridProTocomplete_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string ID = null;
        if (e.CommandName == "View")
        {
            GridViewRow rowSelect = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
            int rowindex = rowSelect.RowIndex;
            ID = GridProTocomplete.Rows[rowindex].Cells[1].Text.Trim().ToString();
            ViewState["PatentID"] = ID;

            string entrystatus = GridProTocomplete.Rows[rowindex].Cells[4].Text.Trim().ToString();
            ViewState["EntryStatus"] = entrystatus;

            PatentBusiness Pat_Busobj = new PatentBusiness();
           
            ClearPatent();
            SelectPatent();
            btnview.Enabled = true;
            if (RadioButtonList1.SelectedValue == "PtoC")
            {
                DataSet ds = new DataSet();
                ds = Pat_Busobj.SelectProTocompletelist(RadioButtonList1.SelectedValue);

                GridProTocomplete.Visible = true;
                GridGRNtoRenewal.Visible = false;
                GridProTocomplete.DataSource = ds.Tables[0];
                GridProTocomplete.DataBind();
            }
            else if (RadioButtonList1.SelectedValue == "GtoR")
            {
                DataSet ds1 = new DataSet();
                ds1 = Pat_Busobj.SelectGRNtoRenewallist(RadioButtonList1.SelectedValue);

                GridGRNtoRenewal.Visible = true;
                GridProTocomplete.Visible = false;
                GridGRNtoRenewal.DataSource = ds1.Tables[0];
                GridGRNtoRenewal.DataBind();
            }
        }
    }
    protected void GridProTocomplete_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        ImageButton EditButton = (ImageButton)e.Row.FindControl("BtnEdit");
    }
    protected void GridGRNtoRenewal_RowEditing(object sender, GridViewEditEventArgs e)
    {
        ClearPatent();
        SelectPatent();
        btnview.Enabled = true;
    }
    protected void GridGRNtoRenewal_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string ID = null;
        if (e.CommandName == "Edit")
        {
            GridViewRow rowSelect = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
            int rowindex = rowSelect.RowIndex;
            ID = GridProTocomplete.Rows[rowindex].Cells[1].Text.Trim().ToString();
            ViewState["PatentID"] = ID;

            string entrystatus = GridProTocomplete.Rows[rowindex].Cells[4].Text.Trim().ToString();
            ViewState["EntryStatus"] = entrystatus;

            ClearPatent();
            SelectPatent();
            btnview.Enabled = true;
        }
    }

    protected void GridGRNtoRenewal_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        ImageButton EditButton = (ImageButton)e.Row.FindControl("BtnEdit");
    }
}