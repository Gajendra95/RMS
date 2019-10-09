using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using System.IO;
using System.Configuration;
using System.Net.Mail;
using log4net;
using AjaxControlToolkit;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Text;
public partial class NewFolder1_Patent : System.Web.UI.Page
{
    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
        if (!IsPostBack)
        {

            if (Session["Role"].ToString() == "2")
            {
                Btnsave.Visible = false;
            }
            else
            {
                Btnsave.Visible = true;
            }
            SetInitialRow();
            //setModalWindowdummy(sender, e);
            //setModalWindowApp(sender, e);
            //setModalWindowRenewal(sender, e);
            //setModalWindow(sender, e);
            //setModalWindowStudent(sender, e);
            CompareValidator1.ValueToCompare = DateTime.Now.ToString("dd/MM/yyyy");
            LabelProjectReference.Visible = true;
            DropDownListhasProjectreference.Visible = true;
            LabelProjectDetails.Visible = false;
            TextBoxProjectDetails.Visible = false;
            ImageButtonProject.Visible = false;
        }
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

    //private void setModalWindowdummy(object sender, EventArgs e)
    //{

    //    dummy.DataBind();
    //}
    protected void setModalWindow(object sender, EventArgs e)
    {
        UpdatePanel2.Update();
        popupPanelAffil.Visible = true;
        popGridAffil.DataSourceID = "SqlDataSourceAffil";
        SqlDataSourceAffil.DataBind();
        popGridAffil.DataBind();
        int rows = popGridAffil.Rows.Count;
        popGridAffil.Visible = true;

    }
    protected void SetInitialRow()
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
        dt.Columns.Add(new DataColumn("Continent", typeof(string)));
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
        dr["Continent"] = string.Empty;
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
        EmployeeCode.Enabled = false;
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
        DropdownMuNonMu.SelectedValue = "M";

        if (DropdownMuNonMu.SelectedValue == "M")
        {
            EmployeeCodeBtn.Enabled = false;
        }
        else if (DropdownMuNonMu.SelectedValue == "N")
        {
            EmployeeCodeBtn.Enabled = false;
        }
        else if (DropdownMuNonMu.SelectedValue == "E")
        {
            EmployeeCodeBtn.Enabled = false;
        }
        else if (DropdownMuNonMu.SelectedValue == "S")
        {
            EmployeeCodeBtn.Enabled = false;
        }
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
                            EmployeeCode.Enabled = false;
                            dtCurrentTable.Rows[i - 1]["NationalType"] = NationalType.SelectedValue;
                            dtCurrentTable.Rows[i - 1]["Continent"] = ContinentId.SelectedValue;

                            dtCurrentTable.Rows[i - 1]["Institution"] = Institution.Value;
                            dtCurrentTable.Rows[i - 1]["InstitutionName"] = InstitutionName.Text;
                            dtCurrentTable.Rows[i - 1]["Department"] = Department.Value;
                            dtCurrentTable.Rows[i - 1]["DepartmentName"] = DepartmentName.Text;
                            //popupPanelAffil.Visible = true;
                            //popGridAffil.DataSourceID = "SqlDataSourceAffil";
                            //SqlDataSourceAffil.DataBind();
                            //popGridAffil.DataBind();
                            //popGridAffil.Visible = true;
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
                            EmployeeCode.Enabled = false;

                            dtCurrentTable.Rows[i - 1]["NationalType"] = NationalType.SelectedValue;
                            dtCurrentTable.Rows[i - 1]["Continent"] = ContinentId.SelectedValue;

                            dtCurrentTable.Rows[i - 1]["Institution"] = Institution.Value;
                            dtCurrentTable.Rows[i - 1]["InstitutionName"] = InstitutionName.Text;
                            dtCurrentTable.Rows[i - 1]["Department"] = Department.Value;
                            dtCurrentTable.Rows[i - 1]["DepartmentName"] = DepartmentName.Text;
                        }
                        else if (DropdownMuNonMu.Text == "E")
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
                            EmployeeCode.Enabled = false;

                            dtCurrentTable.Rows[i - 1]["NationalType"] = NationalType.SelectedValue;
                            dtCurrentTable.Rows[i - 1]["Continent"] = ContinentId.SelectedValue;

                            dtCurrentTable.Rows[i - 1]["Institution"] = Institution.Value;
                            dtCurrentTable.Rows[i - 1]["InstitutionName"] = InstitutionName.Text;
                            dtCurrentTable.Rows[i - 1]["Department"] = Department.Value;
                            dtCurrentTable.Rows[i - 1]["DepartmentName"] = DepartmentName.Text;
                        }
                        else if (DropdownMuNonMu.Text == "S")
                        {
                            DropdownStudentInstitutionName.Visible = false;
                            DropdownStudentDepartmentName.Visible = false;
                            InstitutionName.Visible = false;
                            DepartmentName.Visible = false;
                            EmployeeCode.Enabled = false;
                            NationalType.Visible = false;
                            ContinentId.Visible = false;

                            EmployeeCodeBtnImg.Enabled = false;
                            EmployeeCodeBtnImg.Visible = false;
                            EmployeeCode.Enabled = false;

                            dtCurrentTable.Rows[i - 1]["NationalType"] = NationalType.SelectedValue;
                            dtCurrentTable.Rows[i - 1]["Continent"] = ContinentId.SelectedValue;
                            //dtCurrentTable.Rows[i - 1]["Institution"] = DropdownStudentInstitutionName.SelectedValue;

                            //dtCurrentTable.Rows[i - 1]["Department"] = DropdownStudentDepartmentName.SelectedValue;
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
                            AuthorName.Enabled = false;
                            EmployeeCodeBtnImg.Visible = true;
                            EmployeeCodeBtnImg.Enabled = false;


                            dtCurrentTable.Rows[i - 1]["Institution"] = DropdownStudentInstitutionName.SelectedValue;
                            dtCurrentTable.Rows[i - 1]["InstitutionName"] = DropdownStudentInstitutionName.SelectedItem;
                            dtCurrentTable.Rows[i - 1]["Department"] = DropdownStudentDepartmentName.SelectedValue;
                            dtCurrentTable.Rows[i - 1]["DepartmentName"] = DropdownStudentDepartmentName.SelectedItem;
                            dtCurrentTable.Rows[i - 1]["EmployeeCode"] = EmployeeCode.Text.Trim();

                            NationalType.Visible = false;
                            ContinentId.Visible = false;

                            dtCurrentTable.Rows[i - 1]["NationalType"] = NationalType.SelectedValue;
                            dtCurrentTable.Rows[i - 1]["Continent"] = ContinentId.SelectedValue;
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
        setModalWindow1(sender, e);

        PoppanelRenewal.Visible = false;
        // popupPanelAffil.Visible = false;
        PopAppStage.Visible = false;
        //popupstudent.Visible = false;
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
                        EmployeeCode.Enabled = false;
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
                        EmployeeCode.Enabled = false;
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
                    else if (DropdownMuNonMu.Text == "E")
                    {
                        AuthorName.Enabled = true;
                        InstitutionName.Enabled = true;
                        DepartmentName.Enabled = true;
                        MailId.Enabled = true;

                        EmployeeCodeBtnImg.Enabled = false;
                        EmployeeCode.Enabled = false;
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
                        AuthorName.Enabled = false;
                        InstitutionName.Enabled = false;
                        DepartmentName.Enabled = false;
                        MailId.Enabled = true;
                        EmployeeCode.Enabled = false;

                        NationalType.Visible = false;
                        ContinentId.Visible = false;

                        NationalType.Text = dt.Rows[i]["NationalType"].ToString();
                        ContinentId.Text = dt.Rows[i]["Continent"].ToString();

                        EmployeeCodeBtnImg.Enabled = false;

                        DropdownStudentInstitutionName.Visible = false;
                        DropdownStudentDepartmentName.Visible = false;
                        InstitutionName.Visible = true;
                        DepartmentName.Visible = true;
                        Institution.Value = dt.Rows[i]["Institution"].ToString();
                        InstitutionName.Text = dt.Rows[i]["InstitutionName"].ToString();
                        Department.Value = dt.Rows[i]["Department"].ToString();
                        DepartmentName.Text = dt.Rows[i]["DepartmentName"].ToString();

                    }
                    else if (DropdownMuNonMu.Text == "O")
                    {

                        EmployeeCode.Visible = true;
                        EmployeeCode.Enabled = true;
                        AuthorName.Enabled = true;
                        InstitutionName.Enabled = false;
                        DepartmentName.Enabled = false;
                        MailId.Enabled = true;
                        AuthorName.Enabled = true;
                        EmployeeCodeBtnImg.Enabled = false;
                        EmployeeCodeBtnImg1.Visible = true;
                        DropdownStudentInstitutionName.Visible = true;
                        DropdownStudentDepartmentName.Visible = true;
                        InstitutionName.Visible = false;
                        DepartmentName.Visible = false;
                        NationalType.Visible = false;
                        ContinentId.Visible = false;
                        EmployeeCode.Text = dt.Rows[i]["EmployeeCode"].ToString();
                        Institution.Value = dt.Rows[i]["Institution"].ToString();
                        DropdownStudentInstitutionName.SelectedValue = dt.Rows[i]["Institution"].ToString();
                        Department.Value = dt.Rows[i]["Department"].ToString();
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

    protected void setModalWindowStudent(object sender, EventArgs e)
    {

        popupStudentGrid.DataSourceID = "StudentSQLDS";
        StudentSQLDS.DataBind();
        popupStudentGrid.DataBind();
    }
    //Textbox author name changed
    protected void AuthorName_Changed(object sender, EventArgs e)
    {
    }
    // bind author popup grid on text change
    protected void AuthorNameChanged(object sender, EventArgs e)
    {
        UpdatePanel2.Update();
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

        string rowVal1 = Request.Form["rowIndx"];
        int rowIndex = Convert.ToInt32(rowVal1);
        int row = Convert.ToInt16(rowVal.Value);
        DropDownList munonmu = (DropDownList)Grid_AuthorEntry.Rows[row].FindControl("DropdownMuNonMu");
        if (munonmu.SelectedValue == "M")
        {
            //popupPanelAffil.Visible = true;
            //popupstudent.Visible = false;
            popupPanelAffil.Style.Add("display", "true");
            //popupstudent.Style.Add("display", "none");
        }
        else 
        {
            popupPanelAffil.Style.Add("display", "none");
        }
        //else if (munonmu.SelectedValue == "S")
        //{
        //    //popupPanelAffil.Visible = false;
        //    //popupstudent.Visible = true;
        //    popupPanelAffil.Style.Add("display", "none");
        //    popupstudent.Style.Add("display", "true");
        //}
        //else if (munonmu.SelectedValue == "O")
        //{
        //    //popupPanelAffil.Visible = false;
        //    //popupstudent.Visible = false;
        //    popupPanelAffil.Style.Add("display", "none");
        //    popupstudent.Style.Add("display", "none");
        //}
        //ModalPopupExtender ModalPopupExtender8 = (ModalPopupExtender)Grid_AuthorEntry.Rows[row].FindControl("ModalPopupExtender4");
        //ModalPopupExtender8.Show();


    }
    protected void Btn_App_View(object sender, EventArgs e)
    {
        lblnoteApp.Visible = false;
        setModalWindowApp(sender, e);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "CallMyFunction", "callthis2()", true);
    }
    protected void btn_Clear(object sender, EventArgs e)
    {
    }
    protected void Btn_Data_Sumbit(object sender, EventArgs e)
    {

        if (!Page.IsValid)
        {
            return;
        }

        try
        {
            if (ddlNatureofPatent.SelectedValue == "2")
            {
                string CloseWindow;
                CloseWindow = "alert('Patent cannot be submitted because nature of patent is provisional')";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);
                return;
            }

            PatentBusiness PatBus_obj = new PatentBusiness();
            Patent pat = new Patent();

            pat.ID = txtID.Text;
            pat.Title = txtTitle.Text;
            pat.description = txtde.Text;

            if (ViewState["App_Status"] != null)
            {
                pat.App_Status = ViewState["App_Status"].ToString();
            }
            if (ViewState["App_Date"] != null)
            {
                pat.App_Date = Convert.ToDateTime(ViewState["App_Date"].ToString());
            }
            if (ViewState["App_Comment"] != null)
            {
                pat.App_Comment = ViewState["App_Comment"].ToString();
            }
            //if (ddlFilingstatus.SelectedValue=="APP")
            //{
            //    pat.Entry_status = "Submit";
            //}
            //if (ddlFilingstatus.SelectedValue == "GRN")
            //{
            pat.Entry_status = "SUB";

            //}
            //else
            //{
            //    pat.Entry_status = "";
            //}
            pat.Filing_Status = ddlFilingstatus.SelectedValue.ToString();
            pat.Filing_Office = ddlfilingoffice.SelectedValue.ToString();
            pat.NatureOfPatent = Convert.ToByte(ddlNatureofPatent.SelectedValue.ToString());
            pat.Funding = Convert.ToByte(ddlFunding.SelectedValue.ToString());
            pat.DetailsColaInstitiuteIndustry = txtdetailsCII.Text;
            pat.Country = txtcountry.Text;
            if (txtrevenue.Text != "" && txtrevenue.Text != null)
            {
                pat.RevenueGenerated = Convert.ToDouble(txtrevenue.Text);
            }
            if (txtdateofApplication.Text.ToString() != "")
            {
                pat.Date_Of_Application = DateTime.ParseExact(txtdateofApplication.Text, "dd/MM/yyyy", null);

            }

            pat.Application_Stage = txtApplicationStage.Text;
            //pat.Provisional_Number = txtProvisionalNo.Text;
            //if (txtFilingDateProvided.Text.ToString() != "")
            //{
            //    pat.FilingDateprovidedPatent = DateTime.ParseExact(txtFilingDateProvided.Text, "dd/MM/yyyy", null);

            //}
            pat.Patent_Number = txtPatentNo.Text;
            pat.Application_Number = txtapplicationNo.Text;
            if (txtGrantDate.Text.ToString() != "")
            {
                pat.Grant_Date = DateTime.ParseExact(txtGrantDate.Text, "dd/MM/yyyy", null);

            }
            //pat.Grant_Date = Convert.ToDateTime(txtGrantDate.Text);
            pat.Renewal_Fee = txtRenewalFee.Text;
            if (txtRenewalDate.Text.ToString() != "")
            {
                pat.LastRenewalFeePaiddate = DateTime.ParseExact(txtRenewalDate.Text, "dd/MM/yyyy", null);

            }
            if (txtlastRenewal.Text.ToString() != "")
            {

                pat.LastRenewalFeePaiddate = Convert.ToDateTime(txtlastRenewal.Text);
            }
            // pat.LastRenewalFeePaiddate = Convert.ToDateTime(txtlastRenewalFee.Text);
            pat.Remarks = txtRemark.Text;
            pat.Rejection_Remark = txtRejectionRemark.Text;


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
                    //     DropDownList AuthorType = (DropDownList)Grid_AuthorEntry.Rows[rowIndex1].Cells[0].FindControl("AuthorType");
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
                            // ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please enter Institution Name!')</script>");
                            string CloseWindow;
                            CloseWindow = "alert('Please enter Institution Name!')";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);
                            return;

                        }

                        if (DepartmentName.Text == "")
                        {
                            //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please enter Department Name!')</script>");
                            string CloseWindow;
                            CloseWindow = "alert('Please enter Department Name!')";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);
                            return;

                        }
                    }
                    else if (DropdownMuNonMu.SelectedValue == "N" || DropdownMuNonMu.SelectedValue == "E")
                    {
                        if (InstitutionName.Text == "")
                        {
                            string CloseWindow;
                            CloseWindow = "alert('Please enter Institution Name!')";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);
                            //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please enter Institution Name!')</script>");
                            return;

                        }

                        if (DepartmentName.Text == "")
                        {
                            string CloseWindow;
                            CloseWindow = "alert('Please enter Department Name!')";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);
                            // ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please enter Department Name!')</script>");
                            return;

                        }
                    }
                    if (MailId.Text == "")
                    {
                        string CloseWindow;
                        CloseWindow = "alert('Please enter MailId!')";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);
                        // ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please enter MailId!')</script>");
                        return;

                    }


                    JD[i].AuthorName = AuthorName.Text.Trim();
                    JD[i].MUNonMU = DropdownMuNonMu.Text.Trim();
                    if (JD[i].MUNonMU == "M" || JD[i].MUNonMU == "S")
                    {
                        JD[i].EmployeeCode = EmployeeCode.Text;
                    }
                    else if (JD[i].MUNonMU == "O")
                    {

                        JD[i].EmployeeCode = EmployeeCode.Text.Trim();
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
                    else if (JD[i].MUNonMU == "E")
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
                        JD[i].Institution = Institution.Value.Trim();
                        JD[i].InstitutionName = InstitutionName.Text.Trim();
                        JD[i].Department = Department.Value.Trim();
                        JD[i].DepartmentName = DepartmentName.Text.Trim();
                        JD[i].AppendInstitutions = JD[i].Institution;

                    }
                    else if (JD[i].MUNonMU == "O")
                    {
                        JD[i].NationalInternationl = "";
                        JD[i].continental = "";
                        JD[i].EmployeeCode = EmployeeCode.Text.Trim();
                        JD[i].InstitutionName = DropdownStudentInstitutionName.SelectedItem.ToString();
                        JD[i].Department = DropdownStudentDepartmentName.SelectedValue;
                        JD[i].DepartmentName = DropdownStudentDepartmentName.SelectedItem.ToString();
                        JD[i].Institution = DropdownStudentInstitutionName.SelectedValue;
                        JD[i].AppendInstitutions = JD[i].Institution;
                        JD[i].EmployeeCode = JD[i].EmployeeCode.Trim();
                        JD[i].EmailId = MailId.Text.Trim();
                    }

                    JD[i].AppendInstitutionNames = JD[i].InstitutionName;

                    JD[i].EmailId = MailId.Text.Trim();
                    // JD[i].AuthorType = AuthorType.Text.Trim();
                    if (JD[i].AuthorType == "P")
                    {
                        if (JD[i].MUNonMU == "N")
                        {
                            j.MUNonMUUTN = "NUTN";
                            //j.PiInstId = InstitutionName.Text.Trim();
                            //j.PiDeptId = DepartmentName.Text.Trim();
                            j.PiInstId = Session["InstituteId"].ToString();
                            j.PiDeptId = Session["Department"].ToString();
                        }
                        if (JD[i].MUNonMU == "E")
                        {
                            j.MUNonMUUTN = "NUTN";
                            //j.PiInstId = InstitutionName.Text.Trim();
                            //j.PiDeptId = DepartmentName.Text.Trim();
                            j.PiInstId = Session["InstituteId"].ToString();
                            j.PiDeptId = Session["Department"].ToString();
                        }
                        else if (JD[i].MUNonMU == "M")
                        {
                            j.MUNonMUUTN = "MUTN";
                            j.PiInstId = Institution.Value.Trim();
                            j.PiDeptId = Department.Value.Trim();

                        }
                        else if (JD[i].MUNonMU == "S")
                        {
                            j.MUNonMUUTN = "MUTN";
                            j.PiInstId = DropdownStudentInstitutionName.SelectedValue;
                            j.PiDeptId = DropdownStudentDepartmentName.SelectedValue;

                        }

                    }
                    if (JD[i].AuthorType == "P")
                    {
                        //  countAuthType = countAuthType + 1;
                    }

                    if (JD[i].isCorrAuth == "Y")
                    {
                        // countCorrAuth = countCorrAuth + 1;
                    }

                    rowIndex1++;
                }

            }
            bool result2 = false;
            if (ddlFilingstatus.SelectedValue == "APP")
            {
                result2 = PatBus_obj.UpdatePatent(pat, JD);
            }
            //if (ddlFilingstatus.SelectedValue == "GRN")
            //{
            //    result2 = PatBus_obj.UpdateGrantPatent(pat, JD);
            //}
            if (result2 == true)
            {


                Btnsubmit.Enabled = false;
                Btnsave.Enabled = false;
                BtnDraft.Visible = false;
                BtnAPPsave.Enabled = false;

                if (ddlFilingstatus.SelectedValue == "APP")
                {
                    SqlDataSourePatentStatus.SelectCommand = "Select * from Patent_Status where Id!='CAN' and Id!='EXP' and Id!='LAP'  and Id!='DRAFT' and  Id!='SUB'";
                    ddlFilingstatus.DataSourceID = "SqlDataSourePatentStatus";
                    ddlFilingstatus.DataBind();
                    ddlFilingstatus.SelectedValue = "APP";
                    txtGrantDate.Enabled = false;
                    txtPatentNo.Enabled = false;
                    Btnsave.Visible = true;
                    Btnsave.Enabled = true;
                    Btnsubmit.Visible = false;
                    //ddlNatureofPatent.Enabled = false;
                    //ddlFunding.Enabled = false;
                    //txtTitle.Enabled = false;
                    //txtde.Enabled = false;
                    //panAddAuthor.Enabled = false;
                    //ddlfilingoffice.Enabled = false;
                    //txtapplicationNo.Enabled = false;
                    btnview.Text = "View Application Stage Details";
                    //txtdateofApplication.Enabled = false;
                }
                string CloseWindow;
                CloseWindow = "alert('Patent Submitted!')";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);
                //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Patent Submitted!')</script>");
                return;

            }
        }
        catch (Exception ex)
        {
            log.Error("Inside Catch Block Of Patent Update" + ex.Message + " UserID : " + Session["UserId"].ToString());

            log.Error(ex.StackTrace);


            if (ex.Message.Contains("Input string was not in a correct format"))
            {
                string CloseWindow = "alert('Please enter valid Amount in Revenue Generated !')";
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow, true);
                return;
            }
            throw ex;
        }
    }

    protected void Btn_Save_Draft(object sender, EventArgs e)
    {
        if (!Page.IsValid)
        {
            return;
        }

        try
        {

            PatentBusiness PatBus_obj = new PatentBusiness();
            Patent pat = new Patent();
            if (ViewState["ID"] != null)
            {
                pat.ID = ViewState["ID"].ToString();
            }


            //if (ddlFilingstatus.SelectedValue != "REJ" || ddlFilingstatus.SelectedValue != "GRN")
            //{ 

            //    if (txtdateofApplication.Text.ToString() != "")
            //    {
            //        pat.Date_Of_Application = DateTime.ParseExact(txtdateofApplication.Text, "dd/MM/yyyy", null);

            //    }
            //}

            if (ViewState["App_Status"] != null)
            {
                pat.App_Status = ViewState["App_Status"].ToString();
            }
            //else
            //{
            //    ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('App_Status..  For update Click on search and edit  !')</script>");

            //    return;
            //}
            PopAppStage.Visible = true;
            if (ViewState["App_Date"] != null)
            {
                pat.App_Date = Convert.ToDateTime(ViewState["App_Date"].ToString());
            }
            if (ViewState["App_Comment"] != null)
            {
                pat.App_Comment = ViewState["App_Comment"].ToString();
            }
            pat.ID = txtID.Text;
            pat.Title = txtTitle.Text;
            pat.NatureOfPatent = Convert.ToByte(ddlNatureofPatent.SelectedValue.ToString());
            pat.description = txtde.Text;
            pat.Funding = Convert.ToByte(ddlFunding.SelectedValue.ToString());
            pat.DetailsColaInstitiuteIndustry = txtdetailsCII.Text;
            pat.Country = txtcountry.Text;
            if (txtrevenue.Text != "" && txtrevenue.Text != null)
            {
                pat.RevenueGenerated = Convert.ToDouble(txtrevenue.Text);
            }
            if (txtdateofApplication.Text.ToString() != "")
            {
                pat.Date_Of_Application = DateTime.ParseExact(txtdateofApplication.Text, "dd/MM/yyyy", null);

            }
            //if (ddlFilingstatus.SelectedValue=="APP")
            //{
            //    pat.Entry_status = "Submit";
            //}
            //if (ddlFilingstatus.SelectedValue == "GRN")
            //{
            pat.Entry_status = "DRAFT";
            //}
            //else
            //{
            //    pat.Entry_status = "";
            //}
            pat.Filing_Status = ddlFilingstatus.SelectedValue.ToString();
            pat.Filing_Office = ddlfilingoffice.SelectedValue.ToString();
            pat.NatureOfPatent = Convert.ToByte(ddlNatureofPatent.SelectedValue.ToString());
            pat.Funding = Convert.ToByte(ddlFunding.SelectedValue.ToString());
            if (txtdateofApplication.Text.ToString() != "")
            {
                pat.Date_Of_Application = DateTime.ParseExact(txtdateofApplication.Text, "dd/MM/yyyy", null);

            }
            //pat.Date_Of_Application =Convert.ToDateTime(txtdateofApplication.Text);

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
            //pat.Grant_Date = Convert.ToDateTime(txtGrantDate.Text);
            pat.Renewal_Fee = txtRenewalFee.Text;
            if (txtRenewalDate.Text.ToString() != "")
            {
                pat.LastRenewalFeePaiddate = DateTime.ParseExact(txtRenewalDate.Text, "dd/MM/yyyy", null);

            }
            // pat.LastRenewalFeePaiddate = Convert.ToDateTime(txtlastRenewalFee.Text);
            pat.Remarks = txtRemark.Text;
            pat.Rejection_Remark = txtRejectionRemark.Text;


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
                    //     DropDownList AuthorType = (DropDownList)Grid_AuthorEntry.Rows[rowIndex1].Cells[0].FindControl("AuthorType");
                    DropDownList DropdownStudentInstitutionName = (DropDownList)Grid_AuthorEntry.Rows[rowIndex1].Cells[0].FindControl("DropdownStudentInstitutionName");
                    DropDownList DropdownStudentDepartmentName = (DropDownList)Grid_AuthorEntry.Rows[rowIndex1].Cells[0].FindControl("DropdownStudentDepartmentName");

                    DropDownList NationalType = (DropDownList)Grid_AuthorEntry.Rows[rowIndex1].Cells[0].FindControl("NationalType");
                    DropDownList ContinentId = (DropDownList)Grid_AuthorEntry.Rows[rowIndex1].Cells[0].FindControl("ContinentId");

                    if (AuthorName.Text == "")
                    {

                        string CloseWindow;
                        CloseWindow = "alert('Please enter Investigator Name!')";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);
                        return;
                        //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please enter Investigator Name!')</script>");
                        //return;

                    }

                    if (DropdownMuNonMu.SelectedValue == "M")
                    {
                        if (InstitutionName.Text == "")
                        {
                            string CloseWindow;
                            CloseWindow = "alert('Please enter Institution Name!')";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);
                            return;
                            //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please enter Institution Name!')</script>");
                            //return;

                        }

                        if (DepartmentName.Text == "")
                        {
                            string CloseWindow;
                            CloseWindow = "alert('Please enter Department Name!')";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);
                            return;
                            //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please enter Department Name!')</script>");
                            //return;

                        }
                    }
                    else if (DropdownMuNonMu.SelectedValue == "N" || DropdownMuNonMu.SelectedValue == "E")
                    {
                        if (InstitutionName.Text == "")
                        {
                            string CloseWindow;
                            CloseWindow = "alert('Please enter Institution Name!')";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);
                            return;
                            //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please enter Institution Name!')</script>");
                            //return;

                        }

                        if (DepartmentName.Text == "")
                        {
                            string CloseWindow;
                            CloseWindow = "alert('Please enter Department Name!')";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);
                            return;
                            //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please enter Department Name!')</script>");
                            //return;

                        }
                    }
                    if (MailId.Text == "")
                    {
                        string CloseWindow;
                        CloseWindow = "alert('Please enter MailId!')";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);
                        return;
                        //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please enter MailId!')</script>");
                        //return;

                    }


                    JD[i].AuthorName = AuthorName.Text.Trim();
                    JD[i].MUNonMU = DropdownMuNonMu.Text.Trim();
                    if (JD[i].MUNonMU == "M" || JD[i].MUNonMU == "S")
                    {
                        JD[i].EmployeeCode = EmployeeCode.Text;
                    }
                    else if (JD[i].MUNonMU == "O")
                    {

                        JD[i].EmployeeCode = EmployeeCode.Text.Trim();
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
                    if (JD[i].MUNonMU == "E")
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
                    // JD[i].AuthorType = AuthorType.Text.Trim();
                    if (JD[i].AuthorType == "P")
                    {
                        if (JD[i].MUNonMU == "N")
                        {
                            j.MUNonMUUTN = "NUTN";
                            //j.PiInstId = InstitutionName.Text.Trim();
                            //j.PiDeptId = DepartmentName.Text.Trim();
                            j.PiInstId = Session["InstituteId"].ToString();
                            j.PiDeptId = Session["Department"].ToString();
                        }
                        else   if (JD[i].MUNonMU == "E")
                        {
                            j.MUNonMUUTN = "NUTN";
                            //j.PiInstId = InstitutionName.Text.Trim();
                            //j.PiDeptId = DepartmentName.Text.Trim();
                            j.PiInstId = Session["InstituteId"].ToString();
                            j.PiDeptId = Session["Department"].ToString();
                        }
                        else if (JD[i].MUNonMU == "M")
                        {
                            j.MUNonMUUTN = "MUTN";
                            j.PiInstId = Institution.Value.Trim();
                            j.PiDeptId = Department.Value.Trim();

                        }
                        else if (JD[i].MUNonMU == "S")
                        {
                            j.MUNonMUUTN = "MUTN";
                            j.PiInstId = DropdownStudentInstitutionName.SelectedValue;
                            j.PiDeptId = DropdownStudentDepartmentName.SelectedValue;

                        }

                    }
                    if (JD[i].AuthorType == "P")
                    {
                        //  countAuthType = countAuthType + 1;
                    }

                    if (JD[i].isCorrAuth == "Y")
                    {
                        // countCorrAuth = countCorrAuth + 1;
                    }

                    rowIndex1++;
                }

            }

            bool result2 = false;
            //if (ddlFilingstatus.SelectedValue == "APP" || ddlFilingstatus.SelectedValue == "GRN")
            if (ddlFilingstatus.SelectedValue == "APP")
            {
                result2 = PatBus_obj.UpdatePatent(pat, JD);

            }
            if (ddlFilingstatus.SelectedValue == "GRN")
            {
                result2 = PatBus_obj.UpdateGrantPatent(pat, JD);
            }
            if (ddlFilingstatus.SelectedValue == "APP")
            {
                if (result2 == true)
                {
                    BtnDraft.Enabled = true;
                    Btnsave.Enabled = false;

                    if (txtApplicationStage.Text == "")
                    {
                        Btnsubmit.Visible = false;
                    }
                    else
                    {
                        Btnsubmit.Enabled = true;
                        Btnsubmit.Visible = true;
                    }
                    //SqlDataSourePatentStatus.SelectCommand = "Select * from Patent_Status where Id!='CAN' and Id!='EXP' and Id!='LAP'";
                    //ddlFilingstatus.DataSourceID = "SqlDataSourePatentStatus";
                    //ddlFilingstatus.DataBind();
                    //  ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Patent Updated!')</script>");
                    string CloseWindow;
                    CloseWindow = "alert('Patent Updated!')";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);
                    return;

                }
                //else 
                //{
                //    BtnDraft.Enabled = false;
                //    Btnsave.Enabled = false;
                //    Btnsubmit.Enabled = true;
                //    ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Patent Submitted!')</script>");
                //    return;

                //}
            }

            else if (ddlFilingstatus.SelectedValue == "GRN")
            {
                if (result2 == true)
                {
                    BtnDraft.Enabled = false;
                    Btnsave.Enabled = false;
                    //Btnsubmit.Enabled = true;
                    string CloseWindow;
                    CloseWindow = "alert('Patent Submitted!')";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);
                    //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Patent Submitted!')</script>");
                    return;

                }
            }


        }
        catch (Exception ex)
        {
            log.Error("Inside Catch Block Of Patent Update" + ex.Message + " UserID : " + Session["UserId"].ToString());

            log.Error(ex.StackTrace);


            if (ex.Message.Contains("Input string was not in a correct format"))
            {
                string CloseWindow = "alert('Please enter valid Amount in Revenue Generated !')";
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow, true);
                return;
            }
            throw ex;
        }
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
                int CountSancInfoTp = 0;
                if (ddlFilingstatus.SelectedValue == "GRN")
                {
                    PatentBusiness b1 = new PatentBusiness();
                    CountSancInfoTp = b1.SelectCountUploadgrantInformationType(txtID.Text, ddlFilingstatus.SelectedValue);
                    if (CountSancInfoTp == 0)
                    {
                        string CloseWindow;
                        CloseWindow = "alert('Please Upload the Grant Certificate!')";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);
                        return;
                    }


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
            pat.DetailsColaInstitiuteIndustry = txtdetailsCII.Text;
            pat.Country = txtcountry.Text;
            if (txtrevenue.Text != "" && txtrevenue.Text != null)
            {
                pat.RevenueGenerated = Math.Round(Convert.ToDouble(txtrevenue.Text), 2);
            }

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

            
                if (DropDownListhasProjectreference.SelectedValue == "0")
                {
                    
                    string CloseWindow1 = "alert('Please select Has Project Reference!')";
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow1, true);
                    return;
                   
                }
                if (DropDownListhasProjectreference.SelectedValue == "Y")
                {
                    if (TextBoxProjectDetails.Text == "")
                    {
                        string CloseWindow1 = "alert('Please select the Project Details!')";
                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow1, true);
                        return;
                    }
                }
                    
                string ProjectIDlist = TextBoxProjectDetails.Text.Trim();
                string hasProjectreference = DropDownListhasProjectreference.SelectedValue.Trim();
                pat.ProjectIDlist = ProjectIDlist;
                pat.hasProjectreference = hasProjectreference;


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
                        string CloseWindow;
                        CloseWindow = "alert('Please enter Investigator Name!')";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);
                        //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please enter Investigator Name!')</script>");
                        return;

                    }

                    if (DropdownMuNonMu.SelectedValue == "M")
                    {
                        if (InstitutionName.Text == "")
                        {
                            string CloseWindow;
                            CloseWindow = "alert('Please enter Institution Name!')";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);
                            //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please enter Institution Name!')</script>");
                            return;

                        }

                        if (DepartmentName.Text == "")
                        {
                            string CloseWindow;
                            CloseWindow = "alert('Please enter Department Name!')";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);
                            // ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please enter Department Name!')</script>");
                            return;

                        }
                    }
                    else if (DropdownMuNonMu.SelectedValue == "N" || DropdownMuNonMu.SelectedValue == "E")
                    {
                        if (InstitutionName.Text == "")
                        {
                            string CloseWindow;
                            CloseWindow = "alert('Please enter Institution Name!')";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);
                            // ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please enter Institution Name!')</script>");
                            return;

                        }

                        if (DepartmentName.Text == "")
                        {
                            string CloseWindow;
                            CloseWindow = "alert('Please enter Department Name!')";
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);
                            //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please enter Department Name!')</script>");
                            return;

                        }
                    }
                    if (MailId.Text == "")
                    {
                        string CloseWindow;
                        CloseWindow = "alert('Please enter MailId!')";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);
                        //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please enter MailId!')</script>");
                        return;

                    }


                    JD[i].AuthorName = AuthorName.Text.Trim();
                    JD[i].MUNonMU = DropdownMuNonMu.Text.Trim();
                    if (JD[i].MUNonMU == "M" || JD[i].MUNonMU == "S")
                    {
                        JD[i].EmployeeCode = EmployeeCode.Text;
                    }
                    else if (JD[i].MUNonMU == "O")
                    {

                        JD[i].EmployeeCode = EmployeeCode.Text.Trim();
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
                    if (JD[i].MUNonMU == "E")
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

                        JD[i].Institution = Institution.Value.Trim();
                        JD[i].InstitutionName = InstitutionName.Text.Trim();
                        JD[i].Department = Department.Value.Trim();
                        JD[i].DepartmentName = DepartmentName.Text.Trim();
                        JD[i].AppendInstitutions = JD[i].Institution;

                    }
                    else if (JD[i].MUNonMU == "O")
                    {
                        JD[i].NationalInternationl = "";
                        JD[i].continental = "";
                        JD[i].EmployeeCode = EmployeeCode.Text.Trim();
                        JD[i].InstitutionName = DropdownStudentInstitutionName.SelectedItem.ToString();
                        JD[i].Department = DropdownStudentDepartmentName.SelectedValue;
                        JD[i].DepartmentName = DropdownStudentDepartmentName.SelectedItem.ToString();
                        JD[i].Institution = DropdownStudentInstitutionName.SelectedValue;
                        JD[i].AppendInstitutions = JD[i].Institution;
                        JD[i].EmployeeCode = JD[i].EmployeeCode.Trim();
                        JD[i].EmailId = MailId.Text.Trim();
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

                        BtnDraft.Visible = true;
                        Btnsave.Visible = false;
                        btnview.Enabled = true;
                        btnview.Text = "Add Application Stage Details";
                        string CloseWindow;
                        CloseWindow = "alert('Patent Data Created Successfully..  For update Click on search and edit  !')";
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);
                        EmailDetails details = new EmailDetails();
                        details = SendMail(txtID.Text);
                        SendMailObject obj1 = new SendMailObject();
                        bool result1 = obj1.InsertIntoEmailQueue(details);
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
                    string entrystatus = PatBus_obj.CheckEntryStatus(txtID.Text);

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
                        BtnDraft.Visible = false;
                        if (entrystatus == "SUB")
                        {
                            Btnsubmit.Visible = false;
                        }
                        else
                        {
                            Btnsubmit.Visible = true;
                        }

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
                Btnsubmit.Enabled = false;
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
                Btnsubmit.Enabled = false;
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
                        SqlDataSourePatentStatus.SelectCommand = "Select * from Patent_Status where Id!='CAN' and Id !='APP' and Id!='REJ'  and Id!='LAP' and  Id!='EXP' and Id!='DRAFT' and  Id!='SUB'";
                        ddlFilingstatus.DataSourceID = "SqlDataSourePatentStatus";
                        // ddlFilingstatus.DataBind();
                        ddlFilingstatus.SelectedValue = "GRN";
                        btnRenewalview.Enabled = true;
                        // Btnsubmit.Enabled = true;                                           
                        BtnDraft.Visible = false;
                        ddlNatureofPatent.Enabled = false;

                        if (Session["Role"].ToString() == "2")
                        {
                            btnRenewalview.Text = "Add/View Renewal Details";
                            btnSaveRenewal.Enabled = true;
                            txtRemark.Enabled = true;
                            ddlFilingstatus.Enabled = true;
                            Btnsave.Visible = true;
                            Btnsave.Enabled = true;
                        }
                        else
                        {
                            btnRenewalview.Text = "View Renewal Details";
                            btnSaveRenewal.Enabled = false;
                            txtPatentNo.Enabled = false;
                            txtGrantDate.Enabled = false;
                            txtRemark.Enabled = false;
                            ddlFilingstatus.Enabled = false;
                            Btnsave.Enabled = false;
                        }

                        EmailDetails details = new EmailDetails();
                        details = SendMail(txtID.Text);
                        SendMailObject obj1 = new SendMailObject();
                        bool result1 = obj1.InsertIntoEmailQueue(details);

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
            log.Error("Inside Catch Block Of Patent Update" + ex.Message + " UserID : " + Session["UserId"].ToString());

            log.Error(ex.StackTrace);


            if (ex.Message.Contains("Input string was not in a correct format"))
            {
                string CloseWindow = "alert('Please enter valid Amount in Revenue Generated !')";
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow, true);
                return;
            }
            throw ex;
        }
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
                Btnsubmit.Enabled = true;
                Btnsave.Enabled = false;
                BtnDraft.Visible = true;
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
                //UpdatePanel2.Update();
                Btnsubmit.Visible = true;
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
        //UpdatePanel2.Update();
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
                    Btnsubmit.Enabled = false;
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
                    Btnsubmit.Enabled = false;
                    SqlDataSourePatentStatus.SelectCommand = "Select * from Patent_Status where Id in('EXP', 'LAP') ";
                    ddlFilingstatus.DataSourceID = "SqlDataSourePatentStatus";
                    ddlFilingstatus.DataBind();
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
                    Btnsubmit.Enabled = false;
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
                    Btnsubmit.Enabled = false;
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
        if (Session["Role"].ToString() == "2")
        {
            if (ddlFilingstatus.SelectedValue == "GRN" || ddlFilingstatus.SelectedValue == "LAP")
            {
                btnSaveRenewal.Enabled = true;
            }
            else
            {
                btnSaveRenewal.Enabled = false;
            }
        }
        else
        {
            btnSaveRenewal.Enabled = false;
        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "CallMyFunction", "callthis3()", true);
    }

    protected void ButtonSearchProjectOnClick(object sender, EventArgs e)
    {
        GridViewSearchPatent.EditIndex = -1;
        dataBind();
        popupstudent.Visible = false;
        popupPanelAffil.Visible = false;
    }
    protected void GridViewSearchGrant_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        ImageButton EditButton = (ImageButton)e.Row.FindControl("BtnEdit");
        ImageButton ViewButton = (ImageButton)e.Row.FindControl("BtnView");
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //Find the DropDownList in the Row
            Label lblEntryStatus = (e.Row.FindControl("lblEntryStatus") as Label);
            Label lblFillingStatus = (e.Row.FindControl("lblFillingStatus") as Label);
            if (Session["Role"].ToString() != "2")
            {
                if (lblFillingStatus.Text == "GRN" || lblFillingStatus.Text == "EXP" || lblFillingStatus.Text == "LAP")
                {
                    EditButton.Visible = false;
                    ViewButton.Visible = true;
                }
                else
                {
                    EditButton.Visible = true;
                    ViewButton.Visible = false;
                }
            }
            else
            {
                EditButton.Visible = true;
                ViewButton.Visible = false;
            }
        }
    }
    private void dataBind()
    {
        ////if (PatIDSearch.Text == "" && TextBoxtiltleSearch.Text == "")
        ////{
        ////    SqlDataSource1.SelectCommand = " select p.ID,p.Title,p.Entry_Status,s.StatusName as Filling_Status from Patent_Data p,Patent_Status s where p.Filing_Status=s.Id and p.Filing_Status!='REJ' and p.Entry_Status!='Cancelled' and   p.Filing_Status!='EXP' ORDER BY p.ID DESC  ";
        ////}
        ////else if (PatIDSearch.Text != "" && TextBoxtiltleSearch.Text == "")
        ////{
        ////    SqlDataSource1.SelectCommand = " select p.ID,p.Title,p.Entry_Status,s.StatusName as Filling_Status from Patent_Data p,Patent_Status s where p.Filing_Status=s.Id and p.ID like '%" + PatIDSearch.Text + "%'  and p.Entry_Status!='Cancelled' and p.Filing_Status!='REJ'  and  p.Filing_Status!='EXP' ORDER BY p.ID DESC ";
        ////}

        ////else if (PatIDSearch.Text != "" && TextBoxtiltleSearch.Text != "")
        ////{
        ////    SqlDataSource1.SelectCommand = "  select p.ID,p.Title,p.Entry_Status,s.StatusName as Filling_Status from Patent_Data p,Patent_Status s where p.Filing_Status=s.Id and p.Title  LIKE  '%" + TextBoxtiltleSearch.Text.Trim() + "%' and p.Entry_Status!='Cancelled' and p.Filing_Status!='REJ'  and  p.Filing_Status!='EXP' ORDER BY p.ID DESC ";
        ////}
        ////else
        ////{
        ////    SqlDataSource1.SelectCommand = " select p.ID,p.Title,p.Entry_Status,s.StatusName as Filling_Status from Patent_Data p,Patent_Status s where p.Filing_Status=s.Id and p.ID  LIKE '%'" + PatIDSearch.Text.Trim() + "'%' and p.Title  LIKE  '%" + TextBoxtiltleSearch.Text.Trim() + "%' and p.Entry_Status!='Cancelled'  and p.Filing_Status!='REJ'  and  p.Filing_Status!='EXP' ORDER BY p.ID DESC  ";

        ////}

        string id = PatIDSearch.Text;
        string title = TextBoxtiltleSearch.Text;
        int role = Convert.ToInt16(Session["Role"]);
        string UserId = Session["UserId"].ToString();

        PatentBusiness obj = new PatentBusiness();
        DataSet ds = obj.SelectPatentDetails(id, title, role, UserId);
        GridViewSearchPatent.DataSource = ds;
        GridViewSearchPatent.DataBind();
        // SqlDataSource1.DataBind();
        GridViewSearchPatent.Visible = true;
    }
    protected void DropdownMuNonMuOnSelectedIndexChanged(object sender, EventArgs e)
    {

        //up.Update();
        //update2.Update();
        //UpdatePanel1.Update();
        //UpdatePanel2.Update();
        //UpdatePanel3.Update();
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
            //NameInJournal.Text = "";
            //NameInJournal.Text = "";
            txtSrchStudentName.Text = "";
            txtSrchStudentRollNo.Text = "";
            popupStudentGrid.DataBind();
            affiliateSrch.Text = "";
            popGridAffil.DataBind();

        }
        else if (DropdownMuNonMu.SelectedValue == "N")
        {
            EmployeeCode.Enabled = false;
            DropdownStudentInstitutionName1.Visible = false;
            DropdownStudentDepartmentName.Visible = false;
            InstitutionName.Visible = true;
            DepartmentName.Visible = true;
            NationalType.Visible = true;


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
            txtSrchStudentName.Text = "";
            txtSrchStudentRollNo.Text = "";
            popupStudentGrid.DataBind();
            affiliateSrch.Text = "";
            popGridAffil.DataBind();
        }
        else if (DropdownMuNonMu.SelectedValue == "E")
        {
            EmployeeCode.Enabled = false;
            DropdownStudentInstitutionName1.Visible = false;
            DropdownStudentDepartmentName.Visible = false;
            InstitutionName.Visible = true;
            DepartmentName.Visible = true;
            NationalType.Visible = true;


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
            txtSrchStudentName.Text = "";
            txtSrchStudentRollNo.Text = "";
            popupStudentGrid.DataBind();
            affiliateSrch.Text = "";
            popGridAffil.DataBind();
        }
        else if (DropdownMuNonMu.SelectedValue == "S")
        {
            EmployeeCode.Enabled = false;
            DropdownStudentInstitutionName1.Visible = false;
            DropdownStudentDepartmentName.Visible = false;
            InstitutionName.Visible = true;
            DepartmentName.Visible = true;
            NationalType.Visible = false;
            EmployeeCodeBtn.Enabled = false;
            InstitutionName.Enabled = false;
            DepartmentName.Enabled = false;
            AuthorName.Enabled = false;
            MailId.Enabled = true;
            AuthorName.Text = "";
            MailId.Text = "";
            InstitutionName.Text = "";
            DepartmentName.Text = "";
            ImageButton1.Visible = true;
            EmployeeCodeBtn.Visible = false;
            ImageButton1.Enabled = true;
            txtSrchStudentName.Text = "";
            txtSrchStudentRollNo.Text = "";
            popupStudentGrid.DataBind();
            affiliateSrch.Text = "";
            popGridAffil.DataBind();

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
            txtSrchStudentName.Text = "";
            txtSrchStudentRollNo.Text = "";
            popupStudentGrid.DataBind();
            affiliateSrch.Text = "";
            popGridAffil.DataBind();

        }
        if (DropdownMuNonMu.SelectedValue == "M")
        {
            setModalWindow(sender, e);
        }
        else if (DropdownMuNonMu.SelectedValue == "S")
        {
            setModalWindow1(sender, e);
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
                        dtCurrentTable.Rows[i - 1]["Continent"] = ContinentId.Text;


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
                        dtCurrentTable.Rows[i - 1]["Continent"] = ContinentId.Text;


                        dtCurrentTable.Rows[i - 1]["Institution"] = Institution.Value;
                        dtCurrentTable.Rows[i - 1]["InstitutionName"] = InstitutionName.Text;
                        dtCurrentTable.Rows[i - 1]["Department"] = Department.Value;
                        dtCurrentTable.Rows[i - 1]["DepartmentName"] = DepartmentName.Text;
                    }
                    else if (DropdownMuNonMu.Text == "E")
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
                        dtCurrentTable.Rows[i - 1]["Continent"] = ContinentId.Text;


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
                        dtCurrentTable.Rows[i - 1]["Continent"] = ContinentId.Text;


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

    public void GridViewSearchPatent_OnRowedit(Object sender, GridViewEditEventArgs e)
    {
        Patent a = new Patent();
        //   GridViewSearchPatent.EditIndex = e.NewEditIndex;
        ClearPatent(sender, e);
        SelectPatent(sender, e);
        btnview.Enabled = true;
    }

    private void ClearPatent(object sender, GridViewEditEventArgs e)
    {
        ddlFilingstatus.ClearSelection();
        ddlNatureofPatent.ClearSelection();
        ddlFunding.ClearSelection();
        txtTitle.Text = "";
        txtde.Text = "";
        txtdetailsCII.Text = "";
        txtcountry.Text = "";
        txtrevenue.Text = "";
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

    private void SelectPatent(object sender, GridViewEditEventArgs e)
    {
        update2.Update();
        up.Update();
        //UpdatePanel2.Update();
        Patent Pat = new Patent();
        Patent_DAobject bus_obj = new Patent_DAobject();
        string ID = ViewState["PatentID"].ToString();
        Pat = bus_obj.SelectPatent(ID);
        GridViewSearchPatent.Visible = false;
        txtID.Text = ID;
        txtPatUTN.Text = Pat.Pat_UTN;
        txtTitle.Text = Pat.Title;
        txtde.Text = Pat.description;
        ddlFunding.SelectedValue = Pat.Funding.ToString();
        ddlNatureofPatent.Text = Pat.NatureOfPatent.ToString();
        txtdetailsCII.Text = Pat.DetailsColaInstitiuteIndustry;
        txtcountry.Text = Pat.Country;
        DropDownListhasProjectreference.SelectedValue = Pat.hasProjectreference;
        TextBoxProjectDetails.Text = Pat.ProjectIDlist;
        if (Pat.hasProjectreference == "0" || Pat.hasProjectreference == "N")
        {
            LabelProjectReference.Visible = true;
            DropDownListhasProjectreference.Visible = true;
            LabelProjectDetails.Visible = false;
            TextBoxProjectDetails.Visible = false;
            ImageButtonProject.Visible = false;
        }
        else if (Pat.hasProjectreference == "Y")
        {
            LabelProjectReference.Visible = true;
            DropDownListhasProjectreference.Visible = true;
            LabelProjectDetails.Visible = true;
            TextBoxProjectDetails.Visible = true;
            ImageButtonProject.Visible = true;
        }
        if (Pat.RevenueGenerated != 0)
        {
            txtrevenue.Text = Pat.RevenueGenerated.ToString();
        }
        if (Pat.Filing_Office != null)
        {
            ddlfilingoffice.SelectedValue = Pat.Filing_Office.ToString();
        }

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

        SqlDataSourePatentStatus.SelectCommand = "Select * from Patent_Status where  Id!='DRAFT' and  Id!='SUB'";
        ddlFilingstatus.DataSourceID = "SqlDataSourePatentStatus";
        ddlFilingstatus.DataBind();
        PatentBusiness b1 = new PatentBusiness();
        if (Pat.Filing_Status == "GRN")
        {
            string FileUpload1 = "";

            FileUpload1 = b1.GetPatentFileUploadPath(txtID.Text);
            GVViewFile.Visible = true;

            PaneUploadFiles.Visible = true;

            PanelViewUplodedfiles.Visible = true;

            if (Session["Role"].ToString() == "11" || Session["Role"].ToString() == "1")
            {
                //sqli
                DSforgridview.SelectParameters.Clear();
                DSforgridview.SelectParameters.Add("UserId", Session["UserId"].ToString());
                DSforgridview.SelectParameters.Add("ID", Pat.ID);

                //DSforgridview.SelectCommand = "select ID, UploadPDFPath  ,Remark , p.CreatedDate, p.CreatedBy as CreatedBy, m.FirstName as AddedBy, EntryNo,Unit_Id,p.Filing_Status  from PatentAuxillaryDetails p, User_M m where  p.CreatedBy='" + Session["UserId"].ToString() + "'  and m.User_Id=p.CreatedBy  and ID='" + Pat.ID + "' and Deleted='N' order by EntryNo";
                DSforgridview.SelectCommand = "select ID, UploadPDFPath  ,Remark , p.CreatedDate, p.CreatedBy as CreatedBy, m.FirstName as AddedBy, EntryNo,Unit_Id,p.Filing_Status  from PatentAuxillaryDetails p, User_M m where  p.CreatedBy=@UserId  and m.User_Id=p.CreatedBy  and ID=@ID and Deleted='N' order by EntryNo";

                DSforgridview.DataBind();
                GVViewFile.DataBind();



                DSforgridview1.SelectParameters.Clear();
                DSforgridview1.SelectParameters.Add("UserId", Session["UserId"].ToString());
                DSforgridview1.SelectParameters.Add("ID", Pat.ID);

                //DSforgridview1.SelectCommand = "select ID,UploadPDFPath  ,Remark , p.CreatedDate, p.CreatedBy as CreatedBy, m.FirstName as AddedBy, EntryNo,Unit_Id ,p.Filing_Status from PatentAuxillaryDetails p, User_M m where   m.User_Id=p.CreatedBy  and ID='" + Pat.ID + "'  and Deleted='N' and p.CreatedBy! ='" + Session["UserId"].ToString() + "'  order by EntryNo";
                DSforgridview1.SelectCommand = "select ID,UploadPDFPath  ,Remark , p.CreatedDate, p.CreatedBy as CreatedBy, m.FirstName as AddedBy, EntryNo,Unit_Id ,p.Filing_Status from PatentAuxillaryDetails p, User_M m where   m.User_Id=p.CreatedBy  and ID=@ID  and Deleted='N' and p.CreatedBy! =@UserId  order by EntryNo";

                DSforgridview1.DataBind();
                GridView1.DataBind();
                Panel8.Visible = true;

            }
            if (Session["Role"].ToString() == "2")
            {
                PaneUploadFiles.Visible = true;
                PanelViewUplodedfiles.Visible = false;
                //sqli
                DSforgridview.SelectParameters.Clear();
                DSforgridview.SelectParameters.Add("UserId", Session["UserId"].ToString());
                DSforgridview.SelectParameters.Add("ID", Pat.ID);
                DSforgridview.SelectCommand = "select ID, UploadPDFPath  ,Remark , p.CreatedDate, p.CreatedBy as CreatedBy, m.FirstName as AddedBy, EntryNo,Unit_Id,p.Filing_Status  from PatentAuxillaryDetails p, User_M m where  p.CreatedBy=@UserId  and m.User_Id=p.CreatedBy  and ID=@ID and Deleted='N' order by EntryNo";


                //DSforgridview.SelectCommand = "select ID, UploadPDFPath  ,Remark , p.CreatedDate, p.CreatedBy as CreatedBy, m.FirstName as AddedBy, EntryNo,Unit_Id,p.Filing_Status  from PatentAuxillaryDetails p, User_M m where  p.CreatedBy='" + Session["UserId"].ToString() + "'  and m.User_Id=p.CreatedBy  and ID='" + Pat.ID + "' and Deleted='N' order by EntryNo";
                DSforgridview.DataBind();
                GVViewFile.DataBind();


                DSforgridview1.SelectParameters.Clear();
                DSforgridview1.SelectParameters.Add("UserId", Session["UserId"].ToString());
                DSforgridview1.SelectParameters.Add("ID", Pat.ID);

                //DSforgridview1.SelectCommand = "select ID,UploadPDFPath  ,Remark , p.CreatedDate, p.CreatedBy as CreatedBy, m.FirstName as AddedBy, EntryNo,Unit_Id ,p.Filing_Status from PatentAuxillaryDetails p, User_M m where   m.User_Id=p.CreatedBy  and ID='" + Pat.ID + "'  and Deleted='N' and p.CreatedBy! ='" + Session["UserId"].ToString() + "'  order by EntryNo";
                DSforgridview1.SelectCommand = "select ID,UploadPDFPath  ,Remark , p.CreatedDate, p.CreatedBy as CreatedBy, m.FirstName as AddedBy, EntryNo,Unit_Id ,p.Filing_Status from PatentAuxillaryDetails p, User_M m where   m.User_Id=p.CreatedBy  and ID=@ID  and Deleted='N' and p.CreatedBy! =@UserId  order by EntryNo";

                DSforgridview1.DataBind();
                GridView1.DataBind();
                Panel8.Visible = true;


            }

            if (Session["Role"].ToString() == "6" || Session["Role"].ToString() == "16")
            {
                DSforgridview.SelectParameters.Clear();
                DSforgridview.SelectParameters.Add("UserId", Session["UserId"].ToString());
                DSforgridview.SelectParameters.Add("ID", Pat.ID);
                //DSforgridview.SelectCommand = "select ID,UploadPDFPath  ,Remark , p.CreatedDate, p.CreatedBy as CreatedBy, m.FirstName as AddedBy, EntryNo,Unit_Id ,p.Filing_Status from PatentAuxillaryDetails p, User_M m where  p.CreatedBy='" + Session["UserId"].ToString() + "'  and m.User_Id=p.CreatedBy  and ID='" + Pat.ID + "' and  Deleted='N' order by EntryNo";
                DSforgridview.SelectCommand = "select ID,UploadPDFPath  ,Remark , p.CreatedDate, p.CreatedBy as CreatedBy, m.FirstName as AddedBy, EntryNo,Unit_Id ,p.Filing_Status from PatentAuxillaryDetails p, User_M m where  p.CreatedBy=@UserId  and m.User_Id=p.CreatedBy  and ID=@ID and  Deleted='N' order by EntryNo";

                DSforgridview.DataBind();
                GVViewFile.DataBind();


                DSforgridview1.SelectParameters.Clear();
                DSforgridview1.SelectParameters.Add("UserId", Session["UserId"].ToString());
                DSforgridview1.SelectParameters.Add("ID", Pat.ID);

                //DSforgridview1.SelectCommand = "select ID,UploadPDFPath  ,Remark , p.CreatedDate, p.CreatedBy as CreatedBy, m.FirstName as AddedBy, EntryNo,Unit_Id ,p.Filing_Status from PatentAuxillaryDetails p, User_M m where  m.User_Id=p.CreatedBy  and ID='" + Pat.ID + "' and  Deleted='N' and p.CreatedBy !='" + Session["UserId"].ToString() + "'  order by EntryNo";
                DSforgridview1.SelectCommand = "select ID,UploadPDFPath  ,Remark , p.CreatedDate, p.CreatedBy as CreatedBy, m.FirstName as AddedBy, EntryNo,Unit_Id ,p.Filing_Status from PatentAuxillaryDetails p, User_M m where  m.User_Id=p.CreatedBy  and ID=@ID and  Deleted='N' and p.CreatedBy !=@UserId  order by EntryNo";

                DSforgridview1.DataBind();
                GridView1.DataBind();
                Panel8.Visible = true;

            }


        }
        else
        {
            PaneUploadFiles.Visible = false;
        }
        string FileUpload2 = "";
        FileUpload2 = b1.GetPatentFileUploadPath(txtID.Text);

        if (FileUpload2 != "")
        {
            PanelViewUplodedfiles.Visible = true;
            PaneUploadFiles.Visible = true;
            DSforgridview.SelectParameters.Clear();
            DSforgridview.SelectParameters.Add("UserId", Session["UserId"].ToString());
            DSforgridview.SelectParameters.Add("ID", Pat.ID);
               
            //DSforgridview.SelectCommand = "select ID, UploadPDFPath  ,Remark , p.CreatedDate, p.CreatedBy as CreatedBy, m.FirstName as AddedBy, EntryNo,Unit_Id,p.Filing_Status  from PatentAuxillaryDetails p, User_M m where  p.CreatedBy='" + Session["UserId"].ToString() + "'  and m.User_Id=p.CreatedBy  and ID='" + Pat.ID + "' and Deleted='N' order by EntryNo";
            DSforgridview.SelectCommand = "select ID, UploadPDFPath  ,Remark , p.CreatedDate, p.CreatedBy as CreatedBy, m.FirstName as AddedBy, EntryNo,Unit_Id,p.Filing_Status  from PatentAuxillaryDetails p, User_M m where  p.CreatedBy=@UserId  and m.User_Id=p.CreatedBy  and ID=@ID and Deleted='N' order by EntryNo";

            DSforgridview.DataBind();
            GVViewFile.DataBind();


            DSforgridview1.SelectParameters.Clear();
            DSforgridview1.SelectParameters.Add("UserId", Session["UserId"].ToString());
            DSforgridview1.SelectParameters.Add("ID", Pat.ID);

            //DSforgridview1.SelectCommand = "select ID,UploadPDFPath  ,Remark , p.CreatedDate, p.CreatedBy as CreatedBy, m.FirstName as AddedBy, EntryNo,Unit_Id ,p.Filing_Status from PatentAuxillaryDetails p, User_M m where   m.User_Id=p.CreatedBy  and ID='" + Pat.ID + "'  and Deleted='N' and p.CreatedBy! ='" + Session["UserId"].ToString() + "'  order by EntryNo";
            DSforgridview1.SelectCommand = "select ID,UploadPDFPath  ,Remark , p.CreatedDate, p.CreatedBy as CreatedBy, m.FirstName as AddedBy, EntryNo,Unit_Id ,p.Filing_Status from PatentAuxillaryDetails p, User_M m where   m.User_Id=p.CreatedBy  and ID=@ID  and Deleted='N' and p.CreatedBy! =@UserId  order by EntryNo";

            DSforgridview1.DataBind();
            GridView1.DataBind();
            Panel8.Visible = true;
        }
        else
        {
            PaneUploadFiles.Visible = false;
        }
        if (Pat.Filing_Status == "APP" && Pat.Entry_status == "DRAFT")
        {
            Btnsave.Visible = false;
            BtnDraft.Visible = true;
            Btnsubmit.Visible = true;
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
                Btnsubmit.Enabled = true;
            }
            else
            {
                txtApplicationStage.Text = "";
                btnview.Text = "Add Application Stage Details";
                Btnsubmit.Visible = false;
                BtnAPPsave.Enabled = true;
            }
            ddlFilingstatus.SelectedValue = Pat.Filing_Status;

            ddlNatureofPatent.Enabled = true;
            ddlFunding.Enabled = true;
            txtTitle.Enabled = true;
            txtde.Enabled = true;
            txtdetailsCII.Enabled = true;
            txtcountry.Enabled = true;
            txtrevenue.Enabled = true;
            panAddAuthor.Enabled = true;
            ddlfilingoffice.Enabled = true;
            txtapplicationNo.Enabled = true;
            btnview.Text = "Add/View Application Stage Details";
            txtdateofApplication.Enabled = true;
            Btnsave.Visible = true;
            Btnsave.Enabled = true;
            BtnDraft.Visible = true;
            ddlFilingstatus.Enabled = true;
            txtRemark.Enabled = true;
            btnview.Enabled = false;


        }

        if (Pat.Entry_status == "SUB" && Pat.Filing_Status == "APP")
        {
            SqlDataSourePatentStatus.SelectCommand = "Select * from Patent_Status where Id in ('GRN','REJ','APP','WDN')";
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
                Btnsubmit.Visible = false;
            }

            BtnDraft.Visible = false;
            Btnsubmit.Enabled = false;
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
            txtdetailsCII.Enabled = true;
            txtcountry.Enabled = true;
            txtrevenue.Enabled = true;
            panAddAuthor.Enabled = true;
            ddlfilingoffice.Enabled = true;
            txtapplicationNo.Enabled = true;
            btnview.Text = "View Application Stage Details";
            txtdateofApplication.Enabled = true;
            Btnsave.Visible = true;
            BtnDraft.Visible = false;
            ddlFilingstatus.Enabled = true;
            txtRemark.Enabled = true;
            btnview.Enabled = false;
        }

        if (Pat.Filing_Status == "GRN")
        {
            PaneUploadFiles.Visible = true;
            SqlDataSourePatentStatus.SelectCommand = "Select * from Patent_Status where Id!='CAN' and Id !='APP' and Id!='REJ'  and Id!='LAP'  and Id!='EXP' and Id!='DRAFT' and  Id!='SUB' and  Id!='WDN'";
            ddlFilingstatus.DataSourceID = "SqlDataSourePatentStatus";
            ddlFilingstatus.DataBind();
            ddlFilingstatus.SelectedValue = Pat.Filing_Status;
            BtnDraft.Visible = false;
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
            //if (Session["Role"].ToString() == "2")
            //{
            //    Btnsave.Visible = false;
            //    BtnAddMU.Visible = false;
            //    FileUploadPdf1.Enabled = false;
            //    Buttonupload.Enabled = false;
            //}
            //else
            //{
            //    Btnsave.Visible = true;
            //    BtnAddMU.Visible = true;
            //    FileUploadPdf1.Enabled = true;
            //    Buttonupload.Enabled = true;
            //}
            if (Session["Role"].ToString() == "2")
            {
                ddlNatureofPatent.Enabled = true;
                ddlFunding.Enabled = true;
                txtTitle.Enabled = true;
                txtde.Enabled = true;
                panAddAuthor.Enabled = true;
                ddlfilingoffice.Enabled = true;
                txtapplicationNo.Enabled = true;
                txtdateofApplication.Enabled = true;
                btnSaveRenewal.Enabled = true;
                btnRenewalview.Text = "Add/View Renewal Details";
                txtPatentNo.Enabled = true;
                txtGrantDate.Enabled = true;
                txtRemark.Enabled = true;
                ddlFilingstatus.Enabled = true;


                ddlNatureofPatent.Enabled = false;
                ddlFunding.Enabled = false;
                txtTitle.Enabled = false;
                txtde.Enabled = false;
                txtdetailsCII.Enabled = false;
                txtcountry.Enabled = false;
                txtrevenue.Enabled = false;
                Btnsave.Visible = false;
                BtnAddMU.Visible = false;
                FileUploadPdf1.Enabled = false;
                Buttonupload.Enabled = false;
            }
            else
            {
                ddlNatureofPatent.Enabled = false;
                ddlFunding.Enabled = false;
                txtTitle.Enabled = false;
                txtde.Enabled = false;
                txtdetailsCII.Enabled = false;
                txtcountry.Enabled = false;
                txtrevenue.Enabled = false;
                panAddAuthor.Enabled = false;
                ddlfilingoffice.Enabled = false;
                txtapplicationNo.Enabled = false;
                txtdateofApplication.Enabled = false;
                btnSaveRenewal.Enabled = false;
                btnRenewalview.Text = "View Renewal Details";
                txtPatentNo.Enabled = false;
                txtGrantDate.Enabled = false;
                txtRemark.Enabled = false;
                ddlFilingstatus.Enabled = false;
                Btnsave.Visible = false;
                BtnAddMU.Visible = true;
                FileUploadPdf1.Enabled = true;
                Buttonupload.Enabled = true;
            }
            btnview.Text = "View Application Stage Details";

        }

        if (Pat.Filing_Status == "LAP")
        {
            SqlDataSourePatentStatus.SelectCommand = "Select * from Patent_Status where Id!='CAN' and Id in('LAP','GRN','EXP') ";
            ddlFilingstatus.DataSourceID = "SqlDataSourePatentStatus";
            ddlFilingstatus.DataBind();
            ddlFilingstatus.SelectedValue = Pat.Filing_Status;

            BtnDraft.Visible = false;
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


            ddlNatureofPatent.Enabled = false;
            ddlFunding.Enabled = false;
            txtTitle.Enabled = false;
            txtde.Enabled = false;
            txtdetailsCII.Enabled = false;
            txtcountry.Enabled = false;
            txtrevenue.Enabled = false;
            panAddAuthor.Enabled = false;
            ddlfilingoffice.Enabled = false;
            txtapplicationNo.Enabled = false;
            txtdateofApplication.Enabled = false;
            btnSaveRenewal.Enabled = false;
            btnRenewalview.Text = "View Renewal Details";
            txtPatentNo.Enabled = false;
            txtGrantDate.Enabled = false;
            txtRemark.Enabled = false;
            ddlFilingstatus.Enabled = false;
            Btnsave.Visible = false;
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
                ImageButton ImageButton1 = (ImageButton)Grid_AuthorEntry.Rows[rowIndex].Cells[2].FindControl("ImageButton1");


                drCurrentRow = dtCurrentTable.NewRow();

                DropdownMuNonMu.Text = dtCurrentTable.Rows[i - 1]["DropdownMuNonMu"].ToString();
                EmployeeCode.Text = dtCurrentTable.Rows[i - 1]["EmployeeCode"].ToString();
                AuthorName.Text = dtCurrentTable.Rows[i - 1]["AuthorName"].ToString();
                if (DropdownMuNonMu.Text == "M")
                {
                    NationalType.Visible = false;
                    ContinentId.Visible = false;
                    EmployeeCode.Enabled = false;
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
                    EmployeeCode.Enabled = false;
                    NationalType.SelectedValue = dtCurrentTable.Rows[i - 1]["NationalType"].ToString();

                    if (NationalType.SelectedValue == "N")
                    {
                        ContinentId.Visible = false;
                    }
                    else
                    {
                        ContinentId.Visible = true;
                    }
                    ContinentId.SelectedValue = dtCurrentTable.Rows[i - 1]["Continent"].ToString();
                    InstNme.Text = dtCurrentTable.Rows[i - 1]["InstitutionName"].ToString();
                    deptname.Text = dtCurrentTable.Rows[i - 1]["DepartmentName"].ToString();
                    InstId.Value = dtCurrentTable.Rows[i - 1]["Institution"].ToString();
                    deptId.Value = dtCurrentTable.Rows[i - 1]["Department"].ToString();
                }
                else if (DropdownMuNonMu.Text == "E")
                {
                    InstNme.Visible = true;
                    deptname.Visible = true;
                    DropdownStudentInstitutionName.Visible = false;
                    DropdownStudentDepartmentName.Visible = false;
                    NationalType.Visible = true;
                    EmployeeCode.Enabled = false;
                    NationalType.SelectedValue = dtCurrentTable.Rows[i - 1]["NationalType"].ToString();

                    if (NationalType.SelectedValue == "N")
                    {
                        ContinentId.Visible = false;
                    }
                    else
                    {
                        ContinentId.Visible = true;
                    }
                    ContinentId.SelectedValue = dtCurrentTable.Rows[i - 1]["Continent"].ToString();
                    InstNme.Text = dtCurrentTable.Rows[i - 1]["InstitutionName"].ToString();
                    deptname.Text = dtCurrentTable.Rows[i - 1]["DepartmentName"].ToString();
                    InstId.Value = dtCurrentTable.Rows[i - 1]["Institution"].ToString();
                    deptId.Value = dtCurrentTable.Rows[i - 1]["Department"].ToString();
                }
                else if (DropdownMuNonMu.Text == "S")
                {
                    NationalType.Visible = false;
                    ContinentId.Visible = false;
                    DropdownStudentInstitutionName.Visible = false;
                    DropdownStudentDepartmentName.Visible = false;
                    InstNme.Visible = true;
                    deptname.Visible = true;
                    InstNme.Text = dtCurrentTable.Rows[i - 1]["InstitutionName"].ToString();
                    deptname.Text = dtCurrentTable.Rows[i - 1]["DepartmentName"].ToString();
                    InstId.Value = dtCurrentTable.Rows[i - 1]["Institution"].ToString();
                    deptId.Value = dtCurrentTable.Rows[i - 1]["Department"].ToString();
                    ImageButton1.Visible = true;
                    EmployeeCodeBtnimg.Visible = false;
                    EmployeeCode.Enabled = false;
                }
                else if (DropdownMuNonMu.Text == "O")
                {
                    DropdownStudentInstitutionName.Visible = true;
                    DropdownStudentDepartmentName.Visible = true;
                    InstNme.Visible = false;
                    deptname.Visible = false;
                    AuthorName.Enabled = false;
                    DropdownStudentInstitutionName.SelectedValue = dtCurrentTable.Rows[i - 1]["Institution"].ToString();
                    DropdownStudentDepartmentName.SelectedValue = dtCurrentTable.Rows[i - 1]["Department"].ToString();
                    EmployeeCode.Text = dtCurrentTable.Rows[i - 1]["EmployeeCode"].ToString();
                    MailId.Enabled = true;
                    ImageButton1.Visible = false;
                    EmployeeCodeBtnimg.Enabled = false;
                    EmployeeCodeBtnimg.Visible = true;
                    EmployeeCode.Enabled = true;
                    NationalType.Visible = false;
                    ContinentId.Visible = false;
                }

                DropdownMuNonMu1.Enabled = false;
                EmployeeCodeBtnimg1.Enabled = false;
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
                else if (DropdownMuNonMu.Text == "E")
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
                    AuthorName.Enabled = false;
                    DropdownStudentInstitutionName.Enabled = true;
                    DropdownStudentInstitutionName.Enabled = true;
                    MailId.Enabled = true;
                }
                else if (DropdownMuNonMu.Text == "O")
                {
                    DropdownStudentInstitutionName.Enabled = true;
                    DropdownStudentInstitutionName.Enabled = true;
                    EmployeeCodeBtnimg.Enabled = false;
                    EmployeeCodeBtnimg.Visible = true;
                    NationalType.Visible = false;
                    ContinentId.Visible = false;
                    MailId.Enabled = true;
                    AuthorName.Enabled = true;
                    EmployeeCode.Enabled = true;
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
    protected void GridViewSearchPatent_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        dataBind();
        GridViewSearchPatent.PageIndex = e.NewPageIndex;
        GridViewSearchPatent.DataBind();
    }
    public void GridViewSearchPatent_RowCommand(Object sender, GridViewCommandEventArgs e)
    {
        string ID = null;
        if (e.CommandName == "Edit")
        {
            GridViewRow rowSelect = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
            int rowindex = rowSelect.RowIndex;
            ID = GridViewSearchPatent.Rows[rowindex].Cells[1].Text.Trim().ToString();
            ViewState["PatentID"] = ID;


            Label lblEntryStatus = (Label)rowSelect.FindControl("lblEntryStatus");
            // Label lblEntryStatus = (e.Row.FindControl("lblEntryStatus") as Label);
            // string entrystatus = GridViewSearchPatent.Rows[rowindex].Cells[4].Text.Trim().ToString();
            ViewState["EntryStatus"] = lblEntryStatus.Text;
        }
    }
    protected void NationalTypeOnSelectedIndexChanged(object sender, EventArgs e)
    {
        TextBox senderBox = sender as TextBox;

        GridViewRow currentRow = (GridViewRow)((DropDownList)sender).Parent.Parent;
        DropDownList DropdownMuNonMu = (DropDownList)currentRow.FindControl("DropdownMuNonMu");
        ImageButton EmployeeCodeBtn = (ImageButton)currentRow.FindControl("EmployeeCodeBtn");

        TextBox InstitutionName = (TextBox)currentRow.FindControl("InstitutionName");
        DropDownList DropdownStudentInstitutionName1 = (DropDownList)currentRow.FindControl("DropdownStudentInstitutionName");

        TextBox DepartmentName = (TextBox)currentRow.FindControl("DepartmentName");
        DropDownList DropdownStudentDepartmentName = (DropDownList)currentRow.FindControl("DropdownStudentDepartmentName");


        TextBox AuthorName = (TextBox)currentRow.FindControl("AuthorName");
        TextBox MailId = (TextBox)currentRow.FindControl("MailId");
        DropDownList NationalType = (DropDownList)currentRow.FindControl("NationalType");

        DropDownList ContinentId = (DropDownList)currentRow.FindControl("ContinentId");



        if (NationalType.SelectedValue == "I")
        {

            ContinentId.Visible = true;



        }
        else if (DropdownMuNonMu.SelectedValue == "N" || DropdownMuNonMu.SelectedValue == "E")
        {


            ContinentId.Visible = false;


        }
        else
        {
            ContinentId.Visible = false;

        }
    }

    protected void OnselectFilingStatus(object sender, EventArgs e)
    {
        if (ddlFilingstatus.SelectedValue == "REJ")
        {
            PaneUploadFiles.Visible = false;
            txtRejectionRemark.Visible = true;
            lblRejectRemarks.Visible = true;
            Panelfilling.Enabled = false;
            Btnsave.Enabled = true;
            Btnsubmit.Visible = false;
            ddlFilingstatus.Enabled = true;
            PoppanelRenewal.Visible = false;
            popupPanelAffil.Visible = false;
            BtnDraft.Visible = false;
            Btnsave.Visible = true;
            RequiredFieldValidator3.Enabled = false;
            RequiredFieldValidator4.Enabled = true;
            lblpatent.Visible = false;
            lblgdate.Visible = false;
            panAddAuthor.Enabled = false;
            ddlNatureofPatent.Enabled = false;
            txtTitle.Enabled = false;
            txtde.Enabled = false;
            txtdetailsCII.Enabled = false;
            txtcountry.Enabled = false;
            txtrevenue.Enabled = false;
            ddlFunding.Enabled = false;
            txtRemark.Visible = false;
            tdremarks.Visible = false;

        }
        else if (ddlFilingstatus.SelectedValue == "WDN")
        {
            PaneUploadFiles.Visible = false;
            txtRejectionRemark.Visible = true;
            lblRejectRemarks.Visible = true;
            lblRejectRemarks.Text = "Remarks";
            RequiredFieldValidator3.Enabled = false;
            RequiredFieldValidator4.Enabled = true;
            lblpatent.Visible = false;
            lblgdate.Visible = false;
            txtRemark.Visible = false;
            tdremarks.Visible = false;
            BtnDraft.Visible = false;
            Btnsave.Visible = true;
        }
        else if (ddlFilingstatus.SelectedValue == "GRN")
        {
            PaneUploadFiles.Visible = true;
            txtRejectionRemark.Visible = false;
            lblRejectRemarks.Visible = false;
            PoppanelRenewal.Visible = false;
            Btnsave.Enabled = true;
            txtRemark.Visible = true;
            tdremarks.Visible = true;
            //  Btnsubmit.Visible = true;
            Btnsave.Visible = true;
            BtnDraft.Visible = false;
            txtGrantDate.Enabled = true;
            txtPatentNo.Enabled = true;
            // txtlastRenewal.Enabled = true;
            btnRenewalview.Enabled = false;
            BtnAPPsave.Enabled = false;
            RequiredFieldValidator3.Enabled = true;
            RequiredFieldValidator4.Enabled = false;
            lblpatent.Visible = true;
            lblgdate.Visible = true;

            if (Session["Role"].ToString() == "2")
            {
                ddlNatureofPatent.Enabled = false;
                ddlFunding.Enabled = true;
                txtTitle.Enabled = true;
                txtde.Enabled = true;
                txtdetailsCII.Enabled = true;
                txtcountry.Enabled = true;
                txtrevenue.Enabled = true;
                panAddAuthor.Enabled = true;
                ddlfilingoffice.Enabled = true;
                txtapplicationNo.Enabled = true;
                btnview.Text = "Add Application Stage Details";
                txtdateofApplication.Enabled = false;
            }
            else
            {
                ddlNatureofPatent.Enabled = false;
                ddlFunding.Enabled = false;
                txtTitle.Enabled = false;
                txtde.Enabled = false;
                txtdetailsCII.Enabled = false;
                txtcountry.Enabled = false;
                txtrevenue.Enabled = false;
                panAddAuthor.Enabled = false;
                ddlfilingoffice.Enabled = false;
                txtapplicationNo.Enabled = false;
                btnview.Text = "View Application Stage Details";
                txtdateofApplication.Enabled = false;
            }

            RequiredFieldValidator3.Enabled = true;
            RequiredFieldValidator4.Enabled = false;
        }
        else if (ddlFilingstatus.SelectedValue == "APP")
        {
            PaneUploadFiles.Visible = false;
            ddlNatureofPatent.Enabled = true;
            ddlFunding.Enabled = true;
            txtTitle.Enabled = true;
            txtde.Enabled = true;
            txtdetailsCII.Enabled = true;
            txtcountry.Enabled = true;
            txtrevenue.Enabled = true;
            panAddAuthor.Enabled = true;
            ddlfilingoffice.Enabled = true;
            txtapplicationNo.Enabled = true;
            btnview.Text = "View Application Stage Details";
            txtdateofApplication.Enabled = true;
            txtPatentNo.Enabled = false;
            txtGrantDate.Enabled = false;
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

    //on row select of pop up autor
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
        affiliateSrch.Text = "";
        popGridAffil.DataBind();
        txtSrchStudentName.Text = "";
        txtSrchStudentRollNo.Text = "";
        popupStudentGrid.DataBind();
        //HtmlAnchor anchor = new HtmlAnchor();
        ////anchor.Href = "#"; ;
        ////Page.Controls.Add(anchor);
        //anchor.Attributes["href"] = "#";
        //Page.Controls.Add(anchor);
        // css.Attributes["href"] = "#";
    }

    protected void RadioButtonList2_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void SearchStudentData(object sender, EventArgs e)
    {

        //UpdatePanel3.Update();
        //up.Update();
        //popupstudent.Visible = true;
        //popupStudentGrid.Visible = true;

        StudentSQLDS.SelectParameters.Clear();//here
        if (txtSrchStudentName.Text.Trim() == "" && txtSrchStudentRollNo.Text.Trim() == "" && StudentIntddl.SelectedValue == "")
        {
           
            StudentSQLDS.SelectCommand = "Select TOP 5  RollNo,Name,SISClass.ClassName as ClassName,SISInstitution.InstName as InstName,EmailID1,SISStudentGenInfo.ClassCode as ClassCode,SISInstnHR.HRInstitute as InstID from SISStudentGenInfo,SISClass,SISInstitution,SISInstnHR  where SISStudentGenInfo.ClassCode=SISClass.ClassCode and SISStudentGenInfo.InstID=SISInstitution.InstID and SISInstnHR.Institute_Id=SISInstitution.InstID and SISInstnHR.Institute_Id=SISStudentGenInfo.InstID";
            popupStudentGrid.DataBind();
            popupStudentGrid.Visible = true;
        }

//here aug 02
        else if ((txtSrchStudentName.Text.Trim() != "" && txtSrchStudentRollNo.Text.Trim() == "") && StudentIntddl.SelectedValue == "")
        {
            StudentSQLDS.SelectParameters.Add("txtSrchStudentName", txtSrchStudentName.Text.Trim());
            StudentSQLDS.SelectCommand = "Select TOP 5  RollNo,Name,SISClass.ClassName as ClassName,SISInstitution.InstName as InstName,EmailID1 ,SISStudentGenInfo.ClassCode as ClassCode,SISInstnHR.HRInstitute as InstID from SISStudentGenInfo,SISClass,SISInstitution,SISInstnHR  where SISStudentGenInfo.ClassCode=SISClass.ClassCode and SISStudentGenInfo.InstID=SISInstitution.InstID and SISInstnHR.Institute_Id=SISInstitution.InstID and SISInstnHR.Institute_Id=SISStudentGenInfo.InstID and    Name like '%' + @txtSrchStudentName + '%'";

            popupStudentGrid.DataBind();
            popupStudentGrid.Visible = true;
        }
        else if ((txtSrchStudentName.Text.Trim() == "" && txtSrchStudentRollNo.Text.Trim() != "") && StudentIntddl.SelectedValue == "")
        {
            StudentSQLDS.SelectParameters.Add("txtSrchStudentRollNo", txtSrchStudentRollNo.Text.Trim());
            StudentSQLDS.SelectCommand = "Select TOP 5  RollNo,Name,SISClass.ClassName as ClassName,SISInstitution.InstName as InstName,EmailID1 ,SISStudentGenInfo.ClassCode as ClassCode,SISInstnHR.HRInstitute as InstID from SISStudentGenInfo,SISClass,SISInstitution,SISInstnHR  where SISStudentGenInfo.ClassCode=SISClass.ClassCode and SISStudentGenInfo.InstID=SISInstitution.InstID and SISInstnHR.Institute_Id=SISInstitution.InstID and SISInstnHR.Institute_Id=SISStudentGenInfo.InstID and    RollNo like '%' + @txtSrchStudentRollNo+ '%'";

            popupStudentGrid.DataBind();
            popupStudentGrid.Visible = true;
        }
        else if ((txtSrchStudentName.Text.Trim() == "" && txtSrchStudentRollNo.Text.Trim() == "") && StudentIntddl.SelectedValue != "")
        {
            StudentSQLDS.SelectParameters.Add("StudentIntddl", StudentIntddl.SelectedValue);

            StudentSQLDS.SelectCommand = "Select TOP 5  RollNo,Name,SISClass.ClassName as ClassName,SISInstitution.InstName as InstName,EmailID1 ,SISStudentGenInfo.ClassCode as ClassCode,SISInstnHR.HRInstitute as InstID from SISStudentGenInfo,SISClass,SISInstitution,SISInstnHR  where SISStudentGenInfo.ClassCode=SISClass.ClassCode and SISStudentGenInfo.InstID=SISInstitution.InstID and SISInstnHR.Institute_Id=SISInstitution.InstID and SISInstnHR.Institute_Id=SISStudentGenInfo.InstID and   (SISStudentGenInfo.InstID=@StudentIntddl)";

            popupStudentGrid.DataBind();
            popupStudentGrid.Visible = true;
        }
        else if ((txtSrchStudentName.Text.Trim() == "" && txtSrchStudentRollNo.Text.Trim() != "") && StudentIntddl.SelectedValue != "")
        {

            StudentSQLDS.SelectParameters.Add("txtSrchStudentRollNo", txtSrchStudentRollNo.Text.Trim());
            StudentSQLDS.SelectParameters.Add("StudentIntddl", StudentIntddl.SelectedValue);
            StudentSQLDS.SelectCommand = "Select TOP 5  RollNo,Name,SISClass.ClassName as ClassName,SISInstitution.InstName as InstName,EmailID1 ,SISStudentGenInfo.ClassCode as ClassCode,SISInstnHR.HRInstitute as InstID from SISStudentGenInfo,SISClass,SISInstitution,SISInstnHR  where SISStudentGenInfo.ClassCode=SISClass.ClassCode and SISStudentGenInfo.InstID=SISInstitution.InstID and SISInstnHR.Institute_Id=SISInstitution.InstID and SISInstnHR.Institute_Id=SISStudentGenInfo.InstID and   RollNo like '%' + @txtSrchStudentRollNo+ '%' and (SISStudentGenInfo.InstID=@StudentIntddl)";

            popupStudentGrid.DataBind();
            popupStudentGrid.Visible = true;
        }
        else if ((txtSrchStudentName.Text.Trim() != "" && txtSrchStudentRollNo.Text.Trim() == "") && StudentIntddl.SelectedValue != "")
        {

            StudentSQLDS.SelectParameters.Add("txtSrchStudentName", txtSrchStudentName.Text.Trim());
            StudentSQLDS.SelectParameters.Add("StudentIntddl", StudentIntddl.SelectedValue);
            StudentSQLDS.SelectCommand = "Select TOP 5  RollNo,Name,SISClass.ClassName as ClassName,SISInstitution.InstName as InstName,EmailID1 ,SISStudentGenInfo.ClassCode as ClassCode,SISInstnHR.HRInstitute as InstID from SISStudentGenInfo,SISClass,SISInstitution,SISInstnHR  where SISStudentGenInfo.ClassCode=SISClass.ClassCode and SISStudentGenInfo.InstID=SISInstitution.InstID and SISInstnHR.Institute_Id=SISInstitution.InstID and SISInstnHR.Institute_Id=SISStudentGenInfo.InstID and Name like '%' + @txtSrchStudentName + '%' and (SISStudentGenInfo.InstID=@StudentIntddl)";

            popupStudentGrid.DataBind();
            popupStudentGrid.Visible = true;
        }




//ends



        else if ((txtSrchStudentName.Text.Trim() != "" || txtSrchStudentRollNo.Text.Trim() != "") && StudentIntddl.SelectedValue == "")
        {
            //sqli

            StudentSQLDS.SelectParameters.Add("txtSrchStudentName", txtSrchStudentName.Text.Trim());
            StudentSQLDS.SelectParameters.Add("txtSrchStudentRollNo", txtSrchStudentRollNo.Text.Trim());


            //StudentSQLDS.SelectCommand = "Select TOP 5  RollNo,Name,SISClass.ClassName as ClassName,SISInstitution.InstName as InstName,EmailID1 ,SISStudentGenInfo.ClassCode as ClassCode,SISInstnHR.HRInstitute as InstID from SISStudentGenInfo,SISClass,SISInstitution,SISInstnHR  where SISStudentGenInfo.ClassCode=SISClass.ClassCode and SISStudentGenInfo.InstID=SISInstitution.InstID and SISInstnHR.Institute_Id=SISInstitution.InstID and SISInstnHR.Institute_Id=SISStudentGenInfo.InstID and  Name like '" + txtSrchStudentName.Text + "%' and RollNo like '%" + txtSrchStudentRollNo.Text + "%'";
            StudentSQLDS.SelectCommand = "Select TOP 5  RollNo,Name,SISClass.ClassName as ClassName,SISInstitution.InstName as InstName,EmailID1 ,SISStudentGenInfo.ClassCode as ClassCode,SISInstnHR.HRInstitute as InstID from SISStudentGenInfo,SISClass,SISInstitution,SISInstnHR  where SISStudentGenInfo.ClassCode=SISClass.ClassCode and SISStudentGenInfo.InstID=SISInstitution.InstID and SISInstnHR.Institute_Id=SISInstitution.InstID and SISInstnHR.Institute_Id=SISStudentGenInfo.InstID and  Name like '%'+@txtSrchStudentName +'%' and RollNo like '%' + @txtSrchStudentRollNo + '%'";

            popupStudentGrid.DataBind();
            popupStudentGrid.Visible = true;
        }
        else
        {

            StudentSQLDS.SelectParameters.Add("txtSrchStudentName", txtSrchStudentName.Text);
            StudentSQLDS.SelectParameters.Add("txtSrchStudentRollNo", txtSrchStudentRollNo.Text);
            StudentSQLDS.SelectParameters.Add("StudentIntddl", StudentIntddl.SelectedValue);


            //StudentSQLDS.SelectCommand = "Select TOP 5  RollNo,Name,SISClass.ClassName as ClassName,SISInstitution.InstName as InstName,EmailID1 ,SISStudentGenInfo.ClassCode as ClassCode,SISInstnHR.HRInstitute as InstID from SISStudentGenInfo,SISClass,SISInstitution,SISInstnHR  where SISStudentGenInfo.ClassCode=SISClass.ClassCode and SISStudentGenInfo.InstID=SISInstitution.InstID and SISInstnHR.Institute_Id=SISInstitution.InstID and SISInstnHR.Institute_Id=SISStudentGenInfo.InstID and  (Name like '" + txtSrchStudentName.Text + "%' and RollNo like '%" + txtSrchStudentRollNo.Text + "%' and (SISStudentGenInfo.InstID='" + StudentIntddl.SelectedValue + "') ) ";
            StudentSQLDS.SelectCommand = "Select TOP 5  RollNo,Name,SISClass.ClassName as ClassName,SISInstitution.InstName as InstName,EmailID1 ,SISStudentGenInfo.ClassCode as ClassCode,SISInstnHR.HRInstitute as InstID from SISStudentGenInfo,SISClass,SISInstitution,SISInstnHR  where SISStudentGenInfo.ClassCode=SISClass.ClassCode and SISStudentGenInfo.InstID=SISInstitution.InstID and SISInstnHR.Institute_Id=SISInstitution.InstID and SISInstnHR.Institute_Id=SISStudentGenInfo.InstID and  (Name like '%' + @txtSrchStudentName + '%' and RollNo like '%' + @txtSrchStudentRollNo + '%' and (SISStudentGenInfo.InstID=@StudentIntddl) ) ";

            popupStudentGrid.DataBind();
            popupStudentGrid.Visible = true;
        }
        string a = rowVal.Value;
        int rowIndex = Convert.ToInt32(a);
        DropDownList munonmu = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].FindControl("DropdownMuNonMu");
        //if (munonmu.SelectedValue == "M")
        //{
        //    popupPanelAffil.Style.Add("display", "true");
        //    popupstudent.Style.Add("display", "none");
        //}
        //else 
        if (munonmu.SelectedValue == "S")
        {
            //popupstudent.Visible = true;
            popupstudent.Style.Add("display", "true");
            //popupPanelAffil.Style.Add("display", "none");
        }
        else
        {
            //popupstudent.Visible = true;
            popupstudent.Style.Add("display", "none");
            //popupPanelAffil.Style.Add("display", "none");
        }
    }
    protected void StudentDataSelect(Object sender, EventArgs e)
    {
        UpdatePanel3.Update();
        up.Update();
        popupStudentGrid.Visible = true;
        GridViewRow row = popupStudentGrid.SelectedRow;


        string rollno = row.Cells[1].Text;
        string studentname = row.Cells[2].Text;
        string institution = row.Cells[3].Text;
        string classname = row.Cells[4].Text;
        string mailid = row.Cells[5].Text;


        string rowVal2 = rowVal.Value;
        int rowIndex = Convert.ToInt32(rowVal2);

        TextBox EmployeeCode = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("AuthorName");
        EmployeeCode.Text = studentname;

        TextBox InstitutionName = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("InstitutionName");
        InstitutionName.Text = institution;

        TextBox DepartmentName = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("DepartmentName");
        DepartmentName.Text = classname;

        TextBox MailId = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("MailId");
        if (mailid != "&nbsp;")
        {
            MailId.Text = mailid;
        }

        //TextBox NameInJournal = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("NameInJournal");

        //NameInJournal.Text = studentname;

        TextBox employc = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("EmployeeCode");
        employc.Text = rollno;


        HiddenField classcode = (HiddenField)popupStudentGrid.SelectedRow.FindControl("lblClassCode");

        HiddenField instnid = (HiddenField)popupStudentGrid.SelectedRow.FindControl("lblInstn");

        HiddenField Institution = (HiddenField)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("Institution");
        Institution.Value = instnid.Value;

        HiddenField Department = (HiddenField)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("Department");
        Department.Value = classcode.Value;
        popupStudentGrid.DataBind();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "CallMyFunction", "ToggleDisplay5()", true);
        txtSrchStudentName.Text = "";
        txtSrchStudentRollNo.Text = "";
        popupStudentGrid.DataBind();
        affiliateSrch.Text = "";
        popGridAffil.DataBind();

    }

    protected void txtlastRenewalFee_TextChanged1(object sender, EventArgs e)
    {
        if (txtRenewalDate.Text.ToString() != "")
        {

            Patent pat = new Patent();
            Patent pat1 = new Patent();
            PatentBusiness Bus_obj = new PatentBusiness();
            pat.ID = txtID.Text;
            pat.LastRenewalFeePaiddate = DateTime.ParseExact(txtRenewalDate.Text, "dd/MM/yyyy", null);
            pat.NextRenewalDate = pat.LastRenewalFeePaiddate.AddYears(1);
            txtnextRenewal.Text = pat.NextRenewalDate.ToShortDateString();
            int renewalyear = pat.NextRenewalDate.Year;
            txtNextRenewalYear.Text = renewalyear.ToString();
            if (pat.LastRenewalFeePaiddate > DateTime.Now)
            {
                txtNextRenewalYear.Text = "";
                txtRenewalDate.Text = "";
                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Renewal date cannot be greater than current date!')</script>");
                return;
            }

            if (pat.LastRenewalFeePaiddate < pat.Grant_Date)
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Enter Correct date !')</script>");
                return;
            }
            //renewalyear = Convert.ToInt32(txtNextRenewalYear.Text);
            pat1 = Bus_obj.SelectRenewalDate(pat.ID);
            int result = DateTime.Compare(pat1.NextRenewalDate, pat.LastRenewalFeePaiddate);
            //  int diffMonths = ((pat1.NextRenewalDate.Year - pat.LastRenewalFeePaiddate.Year) * 12) + pat1.NextRenewalDate.Month - pat.LastRenewalFeePaiddate.Month;
            if (result >= 0)
            {

                txtRenewalFee.Enabled = true;
                txtRenewalDate.Enabled = true;
                txtRenewalComment.Enabled = true;
                txtNextRenewalYear.Enabled = false;
                btnSaveRenewal.Enabled = true;


            }
            else
            {

                pat.Filing_Status = "LAP";
                txtGrantDate.Enabled = false;
                txtPatentNo.Enabled = false;
                txtRenewalFee.Enabled = false;
                txtRenewalDate.Enabled = false;
                txtRenewalComment.Enabled = false;
                txtNextRenewalYear.Enabled = false;
                btnSaveRenewal.Enabled = true;
                //lblnote.Visible = true;
                // RadioButtonList2.Visible = true;
                btnSaveRenewal.Enabled = false;
            }
        }
    }
    protected void showpopup(object sender, EventArgs e)
    {
        //UpdatePanel2.Update();
        // update1.Update();
        //string rowVal = Request.Form["rowIndx"];
        //int rowIndex = Convert.ToInt32(rowVal);

        //ModalPopupExtender ModalPopupExtender8 = (ModalPopupExtender)Grid_AuthorEntry.Rows[1].FindControl("ModalPopupExtender4");
        //ModalPopupExtender8.Show();

        //ModalPopupExtender ModalPopupStudent = (ModalPopupExtender)Grid_AuthorEntry.Rows[1].FindControl("ModalPopupStudent");
        //ModalPopupStudent.Hide();

        //setModalWindowApp(sender, e);
        //setModalWindowRenewal(sender, e);

        //setModalWindowStudent(sender, e);

        ////PoppanelRenewal.Visible = false;
        ////popupPanelAffil.Visible = true;
        ////PopAppStage.Visible = false;
        ////popupstudent.Visible = false;
        //  Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "callthis1()", true);
        ScriptManager.RegisterStartupScript(this, this.GetType(), "CallMyFunction", "callthis1()", true);
        setModalWindow(sender, e);

    }
    protected void showpopup1(object sender, EventArgs e)
    {

        ScriptManager.RegisterStartupScript(this, this.GetType(), "CallMyFunction", "callthis5()", true);
        setModalWindow1(sender, e);

    }

    private void setModalWindow1(object sender, EventArgs e)
    {
        UpdatePanel3.Update();
        popupstudent.Visible = true;
        popupStudentGrid.DataSourceID = "StudentSQLDS";
        StudentSQLDS.DataBind();
        popupStudentGrid.DataBind();
        int rows = popupStudentGrid.Rows.Count;
        popupStudentGrid.Visible = true;

    }
    protected void btn_CANCEL_Click(object sender, EventArgs e)
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


            SqlDataSourceAuthorType.SelectCommand = "SELECT Id,DisplayName FROM [Author_Type_M] order by DisplayNumber asc";

            DropdownMuNonMu.DataTextField = "DisplayName";
            DropdownMuNonMu.DataValueField = "Id";
            DropdownMuNonMu.DataBind();

        }

    }
    private EmailDetails SendMail(string publicatinid)
    {
        EmailDetails details = new EmailDetails();

        try
        {

            details.FromEmail = ConfigurationManager.AppSettings["FromAddress"].ToString();

            details.Id = txtID.Text;
            details.Type = "";

            ArrayList authoremailidlist = new ArrayList();
            ArrayList supervisoremailidlist = new ArrayList();
            ArrayList authorname = new ArrayList();
            ArrayList studentlist = new ArrayList();

            DataSet student = new DataSet();
            DataSet ds = new DataSet();

            PatentBusiness bus = new PatentBusiness();
            ds = bus.getPatentAuthorList(txtID.Text);

            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                authoremailidlist.Add(ds.Tables[0].Rows[i]["EmailId"].ToString());
            }
            if (ddlFilingstatus.SelectedValue == "APP")
            {
                details.Module = "PAPP";
            }
            else if (ddlFilingstatus.SelectedValue == "GRN")
            {
                details.Module = "PGRN";
            }



            DataSet dy = new DataSet();
            dy = bus.getPatentAuthorListName(txtID.Text);
            for (int i = 0; i < dy.Tables[0].Rows.Count; i++)
            {
                authorname.Add(dy.Tables[0].Rows[i]["InvestigatorName"].ToString());
            }

            string auhtorsS = "";
            string auhtorsSConc = "";
            for (int i = 0; i < authorname.Count; i++)
            {
                auhtorsS = authorname[i].ToString();
                string con = " , ";
                if (i == 0)
                {
                    auhtorsSConc = auhtorsS;
                }
                else
                {
                    auhtorsSConc = auhtorsSConc + con + auhtorsS;
                }

            }



            string FooterText = ConfigurationManager.AppSettings["FooterText"].ToString();
            if (ddlFilingstatus.SelectedValue == "APP")
            {
                details.EmailSubject = "Patent Entry <  " + txtID.Text + "  > Applied ";

                details.MsgBody = "<span style=\"font-size: 10pt; color: #3300cc; font-family: Verdana\"><h4>Dear Sir/Madam,</h4> <br>" +
                     "<b> The following Patent  has been Applied in Patent Repository <br> " +
                     "<br>" +

                    "Patent Id  :  " + txtID.Text + "<br>" +

                    "Title of Work Item  : " + txtTitle.Text + "<br>" +

                    "Added By  : " + authorname[0].ToString() + "<br>" +
                    "Authors  : " + auhtorsSConc + "<br>" + "<br>" + "<br>" + "<br>" + "<br>" + FooterText +
                    " </b><br><b> </b></span>";
            }
            else if (ddlFilingstatus.SelectedValue == "GRN")
            {
                details.EmailSubject = "Patent Entry <  " + txtID.Text + "  > Granted ";
                details.MsgBody = "<span style=\"font-size: 10pt; color: #3300cc; font-family: Verdana\"><h4>Dear Sir/Madam,</h4> <br>" +
                     "<b> The following Patent  has been Granted in Patent Repository <br> " +
                     "<br>" +

                    "Patent Id  :  " + txtID.Text + "<br>" +

                    "Title of Work Item  : " + txtTitle.Text + "<br>" +

                    "Added By  : " + authorname[0].ToString() + "<br>" +
                    "Authors  : " + auhtorsSConc + "<br>" + "<br>" + "<br>" + "<br>" + "<br>" + FooterText +
                    " </b><br><b> </b></span>";
            }

            for (int i = 0; i < authoremailidlist.Count; i++)
            {
                if (details.ToEmail != null)
                {
                    details.ToEmail = details.ToEmail + ',' + authoremailidlist[i].ToString();
                }
                else
                {
                    if (i == 0)
                    {
                        details.ToEmail = authoremailidlist[i].ToString();
                    }
                    else
                    {
                        details.ToEmail = details.ToEmail + ',' + authoremailidlist[i].ToString();
                    }
                }
            }

            string email = bus.GetRDCEmail();
            details.CCEmail = email;
            string ttoemailid = ConfigurationManager.AppSettings["TTOEmailID"].ToString();
            if (ttoemailid != "")
            {
                details.CCEmail = details.CCEmail + ',' + ttoemailid;
            }
            return details;
        }

        catch (Exception ex)
        {

            return details;
        }
    }
    protected void BtnUploadPdf_Click(object sender, EventArgs e)
    {
        if (!Page.IsValid)
        {
            return;
        }
        FilePfdPatentSave(sender, e);
    }

    private void FilePfdPatentSave(object sender, EventArgs e)
    {
        up.Update();


        try
        {
            string filelocationpath = "";
            string UploadPdf1 = "";
            int result1 = 0;
            int CountSancInfoTp = 0;
            int result = 0;
            Patent pat = new Patent();
            PatentBusiness bus = new PatentBusiness();

            string NatureofPatent = ddlNatureofPatent.SelectedValue;

            if (FileUploadPdf1.HasFile)
            {
                Session["file1"] = FileUploadPdf1.FileContent;
                string uploadedfilename = Path.GetFileName(FileUploadPdf1.PostedFile.FileName);
                Session["uploadedfilename2"] = uploadedfilename;
                double size = FileUploadPdf1.PostedFile.ContentLength;
                Session["uploadedfilesize"] = size;
                if (ddlFilingstatus.SelectedValue == "GRN" || ddlFilingstatus.SelectedValue == "APP")
                {
                    PatentBusiness b1 = new PatentBusiness();


                    CountSancInfoTp = b1.SelectCountUploadgrantInformationType(txtID.Text, ddlFilingstatus.SelectedValue);
                    if (CountSancInfoTp == 1)
                    {

                        up.Update();
                        UpdatePanel1.Update();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "CallMyFunction", "callthisupload()", true);
                        ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('DO you want to continue???')</script>");
                        //ScriptManager.RegisterStartupScript(this, this.GetType(), "key", "<script>alert('Hello World');</script>", false);           
                    }
                }
                //Session["file1"] = FileUploadPdf1.FileContent;

                //string uploadedfilename = Path.GetFileName(FileUploadPdf1.PostedFile.FileName);
                //double size = FileUploadPdf1.PostedFile.ContentLength;

                if (size < 4194304) //4mb
                {

                    Stream fs = FileUploadPdf1.PostedFile.InputStream;
                    BinaryReader br = new BinaryReader(fs);
                    byte[] bytes = br.ReadBytes((Int32)fs.Length);
                    bool exeresult = false;
                    Business B = new Business();
                    exeresult = B.IsExeFile(bytes);




                    if (exeresult == true)
                    {
                        string CloseWindow1 = "alert('Uploaded file is not a valid file.Please upload a valid pdf file.')";
                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow1, true);
                        return;
                    }
                    string servername = ConfigurationManager.AppSettings["ServerName"].ToString();
                    string domainame = ConfigurationManager.AppSettings["DomainName"].ToString();
                    string username = ConfigurationManager.AppSettings["UserName"].ToString();
                    string password = ConfigurationManager.AppSettings["Password"].ToString();
                    string FolderPathServerwrite = ConfigurationManager.AppSettings["FolderPathPatent"].ToString();
                    using (NetworkShareAccesser.Access(servername, domainame, username, password))
                    {
                        var uploadfolder = FolderPathServerwrite;
                        string path_BoxId = Path.Combine(uploadfolder, txtID.Text); //main path + location
                        if (!Directory.Exists(path_BoxId))   //if directory doesnt exist
                        {
                            Directory.CreateDirectory(path_BoxId);//crete directory of location
                        }
                        string path_BoxId_ProType = Path.Combine(path_BoxId, NatureofPatent);

                        if (!Directory.Exists(path_BoxId_ProType))   //if directory doesnt exist
                        {
                            Directory.CreateDirectory(path_BoxId_ProType);//crete directory of department
                        }

                        string uploadedfilename1 = Path.GetFileName(FileUploadPdf1.PostedFile.FileName);
                        string timestamp = DateTime.Now.ToString("dd-MM-yy-hh-mm-ss");
                        string fileextension = Path.GetExtension(uploadedfilename);
                        string actualfilenamewithtime = NatureofPatent + "_" + timestamp + fileextension;
                        UploadPdf1 = actualfilenamewithtime;
                        filelocationpath = Path.Combine(path_BoxId_ProType, actualfilenamewithtime);
                        FileUploadPdf1.SaveAs(filelocationpath);  //saving file
                        Session["file"] = uploadedfilename;
                    }
                    pat.UploadRemarks = txtRemark.Text.Trim();
                    pat.ID = txtID.Text;
                    pat.FilePath = filelocationpath;
                    Session["FilePath"] = filelocationpath;
                    pat.Filing_Status = ddlFilingstatus.SelectedValue;

                    if (CountSancInfoTp == 0)
                    {

                        result1 = bus.UploadPatentPathCreate(pat);
                    }
                    if ((Session["Role"].ToString() == "11" || Session["Role"].ToString() == "1"))
                    {
                        //sqli
                        DSforgridview.SelectParameters.Clear();
                        DSforgridview.SelectParameters.Add("UserId", Session["UserId"].ToString());
                        DSforgridview.SelectParameters.Add("ID", pat.ID);

                        //DSforgridview.SelectCommand = "select ID, UploadPDFPath  ,Remark , p.CreatedDate, p.CreatedBy as CreatedBy, m.FirstName as AddedBy, EntryNo,Unit_Id, Filing_Status  from PatentAuxillaryDetails p, User_M m where  p.CreatedBy='" + Session["UserId"].ToString() + "'  and m.User_Id=p.CreatedBy  and ID='" + pat.ID + "'  and Deleted='N' order by EntryNo";
                        DSforgridview.SelectCommand = "select ID, UploadPDFPath  ,Remark , p.CreatedDate, p.CreatedBy as CreatedBy, m.FirstName as AddedBy, EntryNo,Unit_Id, Filing_Status  from PatentAuxillaryDetails p, User_M m where  p.CreatedBy=@UserId  and m.User_Id=p.CreatedBy  and ID=@ID  and Deleted='N' order by EntryNo";

                        DSforgridview.DataBind();
                        GVViewFile.DataBind();


                        DSforgridview1.SelectParameters.Clear();
                     
                        DSforgridview1.SelectParameters.Add("ID", pat.ID);

                        //DSforgridview1.SelectCommand = "select ID, UploadPDFPath ,Remark , p.CreatedDate, p.CreatedBy as CreatedBy, m.FirstName as AddedBy, EntryNo,Unit_Id,Filing_Status  from PatentAuxillaryDetails p, User_M m where  m.User_Id=p.CreatedBy  and ID='" + pat.ID + "'  and Deleted='N' and p.CreatedBy  not in  (Select CreatedBy from PatentAuxillaryDetails where  ID='" + pat.ID + "' and Deleted='N')  order by EntryNo";
                        DSforgridview1.SelectCommand = "select ID, UploadPDFPath ,Remark , p.CreatedDate, p.CreatedBy as CreatedBy, m.FirstName as AddedBy, EntryNo,Unit_Id,Filing_Status  from PatentAuxillaryDetails p, User_M m where  m.User_Id=p.CreatedBy  and ID=@ID  and Deleted='N' and p.CreatedBy  not in  (Select CreatedBy from PatentAuxillaryDetails where  ID=@ID and Deleted='N')  order by EntryNo";

                        DSforgridview1.DataBind();
                        GridView1.DataBind();
                        Panel8.Visible = false;
                    }

                    if (Session["Role"].ToString() == "6")
                    {
                        DSforgridview.SelectParameters.Clear();
                        DSforgridview.SelectParameters.Add("UserId", Session["UserId"].ToString());
                        DSforgridview.SelectParameters.Add("ID", pat.ID);

                        //DSforgridview.SelectCommand = "select ID, UploadPDFPath ,Remark , p.CreatedDate, p.CreatedBy as CreatedBy, m.FirstName as AddedBy, EntryNo,Unit_Id,Filing_Status  from PatentAuxillaryDetails p, User_M m where   m.User_Id=p.CreatedBy  and ID='" + pat.ID + "' and Deleted='N' and p.CreatedBy='" + Session["UserId"].ToString() + "' order by EntryNo";
                        DSforgridview.SelectCommand = "select ID, UploadPDFPath ,Remark , p.CreatedDate, p.CreatedBy as CreatedBy, m.FirstName as AddedBy, EntryNo,Unit_Id,Filing_Status  from PatentAuxillaryDetails p, User_M m where   m.User_Id=p.CreatedBy  and ID=@ID and Deleted='N' and p.CreatedBy=@UserId order by EntryNo";

                        DSforgridview.DataBind();
                        GridView1.DataBind();
                        Panel8.Visible = false;

                        DSforgridview1.SelectParameters.Clear();
                        DSforgridview1.SelectParameters.Add("UserId", Session["UserId"].ToString());
                        DSforgridview1.SelectParameters.Add("ID", pat.ID);

                        //DSforgridview1.SelectCommand = "select ID, UploadPDFPath ,Remark , p.CreatedDate, p.CreatedBy as CreatedBy, m.FirstName as AddedBy, EntryNo,Unit_Id,Filing_Status  from PatentAuxillaryDetails p, User_M m where  p.CreatedBy!='" + Session["UserId"].ToString() + "' and  m.User_Id=p.CreatedBy  and ID='" + pat.ID + "' and Deleted='N' order by EntryNo";
                        DSforgridview1.SelectCommand = "select ID, UploadPDFPath ,Remark , p.CreatedDate, p.CreatedBy as CreatedBy, m.FirstName as AddedBy, EntryNo,Unit_Id,Filing_Status  from PatentAuxillaryDetails p, User_M m where  p.CreatedBy!=@UserId and  m.User_Id=p.CreatedBy  and ID=@ID and Deleted='N' order by EntryNo";

                        DSforgridview1.DataBind();
                        GVViewFile.DataBind();
                    }


                    string FileUpload1 = "";
                    PatentBusiness b = new PatentBusiness();
                    FileUpload1 = b.GetPatentFileUploadPath(txtID.Text);
                    Session["file2"] = FileUpload1;
                    if (FileUpload1 != "")
                    {
                        PanelViewUplodedfiles.Visible = true;
                        PaneUploadFiles.Visible = true;

                        DSforgridview.SelectParameters.Clear();
                        DSforgridview.SelectParameters.Add("UserId", Session["UserId"].ToString());
                        DSforgridview.SelectParameters.Add("ID", pat.ID);

                        //DSforgridview.SelectCommand = "select ID, UploadPDFPath  ,Remark , p.CreatedDate, p.CreatedBy as CreatedBy, m.FirstName as AddedBy, EntryNo,Unit_Id,p.Filing_Status  from PatentAuxillaryDetails p, User_M m where  p.CreatedBy='" + Session["UserId"].ToString() + "'  and m.User_Id=p.CreatedBy  and ID='" + pat.ID + "' and Deleted='N' order by EntryNo";
                        DSforgridview.SelectCommand = "select ID, UploadPDFPath  ,Remark , p.CreatedDate, p.CreatedBy as CreatedBy, m.FirstName as AddedBy, EntryNo,Unit_Id,p.Filing_Status  from PatentAuxillaryDetails p, User_M m where  p.CreatedBy=@UserId  and m.User_Id=p.CreatedBy  and ID=@ID and Deleted='N' order by EntryNo";

                        DSforgridview.DataBind();
                        GVViewFile.DataBind();


                        DSforgridview1.SelectParameters.Clear();
                        DSforgridview1.SelectParameters.Add("UserId", Session["UserId"].ToString());
                        DSforgridview1.SelectParameters.Add("ID", pat.ID);

                        //DSforgridview1.SelectCommand = "select ID,UploadPDFPath  ,Remark , p.CreatedDate, p.CreatedBy as CreatedBy, m.FirstName as AddedBy, EntryNo,Unit_Id ,p.Filing_Status from PatentAuxillaryDetails p, User_M m where   m.User_Id=p.CreatedBy  and ID='" + pat.ID + "'  and Deleted='N' and p.CreatedBy! ='" + Session["UserId"].ToString() + "'  order by EntryNo";
                        DSforgridview1.SelectCommand = "select ID,UploadPDFPath  ,Remark , p.CreatedDate, p.CreatedBy as CreatedBy, m.FirstName as AddedBy, EntryNo,Unit_Id ,p.Filing_Status from PatentAuxillaryDetails p, User_M m where   m.User_Id=p.CreatedBy  and ID=@ID   and Deleted='N' and p.CreatedBy! =@UserId  order by EntryNo";

                        DSforgridview1.DataBind();
                        GridView1.DataBind();
                        Panel8.Visible = true;
                    }
                    GVViewFile.Visible = true;
                    PaneUploadFiles.Visible = true;
                    PanelViewUplodedfiles.Visible = true;
                    if (result1 >= 1)
                    {
                        ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('File Successfully uploaded!')</script>");
                        log.Info("File Successfully uploaded : " + txtID.Text.Trim());
                    }
                    else
                        if (result1 == 0)
                        {
                            ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('DO you want to continue???')</script>");
                            log.Error("Error in File upload!!!! : " + txtID.Text.Trim());

                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Error in File upload!!!!')</script>");
                            log.Error("Error in File upload!!!! : " + txtID.Text.Trim());

                        }

                }
                else
                {
                    ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Invalid File!!!File exceeds more than 4MB..Please try to upload the file less than or equal to 4MB!!!!!!')</script>");
                    log.Error("Invalid File!!!File exceeds more than 4MB!!! : " + txtID.Text.Trim());
                }
            }
        }
        //}
        catch (Exception ex)
        {
            log.Error("Inside Catch Block Of Grant-file uplaod" + ex.Message + " UserID : " + Session["UserId"].ToString());

            log.Error(ex.StackTrace);

            ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Error!!!!!!!!!!!!')</script>");

        }
    }
    //Function to view uploaded files
    protected void GVViewFile_SelectedIndexChanged(object sender, EventArgs e)
    {
        string BoxId = txtID.Text.Trim();
        string NatureofPatent = ddlNatureofPatent.SelectedValue;
        string servername = ConfigurationManager.AppSettings["ServerName"].ToString();
        string domainame = ConfigurationManager.AppSettings["DomainName"].ToString();
        string username = ConfigurationManager.AppSettings["UserName"].ToString();
        string password = ConfigurationManager.AppSettings["Password"].ToString();
        string folderpath;
        string path_BoxId;
        using (NetworkShareAccesser.Access(servername, domainame, username, password))
        {
            folderpath = ConfigurationManager.AppSettings["FolderPathPatent"].ToString();
            path_BoxId = Path.Combine(folderpath, BoxId);
            string path_BoxId_ProType = Path.Combine(path_BoxId, NatureofPatent);


            int id = GVViewFile.SelectedIndex;
            Label filepath = (Label)GVViewFile.Rows[id].FindControl("lblgetid");
            string path = filepath.Text;       //actual filelocation path  
            string newpath = path.Replace('\\', '/');  // replacing '\' by '/' to view image or pdf
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Test", "ViewPdf()", true);
            //Response.Write("<script>");
            //Response.Write("window.open('DisplayPdf.aspx?val=" + newpath + "','_blank')");
            ////path sent to display.aspx page
            //Response.Write("</script>");
        }
    }
    protected void GVViewFile_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //UpdatePannelViewUpload.Update();
        string servername = ConfigurationManager.AppSettings["ServerName"].ToString();
        string domainame = ConfigurationManager.AppSettings["DomainName"].ToString();
        string username = ConfigurationManager.AppSettings["UserName"].ToString();
        string password = ConfigurationManager.AppSettings["Password"].ToString();
        string BoxId = txtID.Text.Trim();
        string NatureofPatent = ddlNatureofPatent.SelectedValue;

        string folderpath;
        string path_BoxId;
        string path = string.Empty;

        using (NetworkShareAccesser.Access(servername, domainame, username, password))
        {
            folderpath = ConfigurationManager.AppSettings["FolderPathPatent"].ToString();
            up.Update();
            UpdatePanel1.Update();

            if (e.CommandName == "View")
            {
                GridViewRow rowSelect = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                int rowindex = rowSelect.RowIndex;
                path_BoxId = Path.Combine(folderpath, BoxId);
                string path_BoxId_ProType = Path.Combine(path_BoxId, NatureofPatent);

                Label Pubid = (Label)GVViewFile.Rows[rowindex].FindControl("lblID");
                string PubID = Pubid.Text;
                // int id = GVViewFile.SelectedIndex;
                Label filepath = (Label)GVViewFile.Rows[rowindex].FindControl("lblpath");
                path = filepath.Text;       //actual filelocation path  

                string newpath = path.Replace('\\', '/');  // replacing '\' by '/' to view image or pdf
                Session["path"] = newpath;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Test", "ViewPdf()", true);

                //Response.Write("<script>");
                //Response.Write("window.open('../DisplayPdf1.aspx?val=" + newpath + "','newpath')");
                ////path sent to display.aspx page
                //Response.Write("</script>");
            }


        }
    }
    protected void GVView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        string BoxId = txtID.Text.Trim();
        string NatureofPatent = ddlNatureofPatent.SelectedValue;
        string servername = ConfigurationManager.AppSettings["ServerName"].ToString();
        string domainame = ConfigurationManager.AppSettings["DomainName"].ToString();
        string username = ConfigurationManager.AppSettings["UserName"].ToString();
        string password = ConfigurationManager.AppSettings["Password"].ToString();
        string folderpath;
        string path_BoxId;
        using (NetworkShareAccesser.Access(servername, domainame, username, password))
        {

            folderpath = ConfigurationManager.AppSettings["FolderPathPatent"].ToString();
            path_BoxId = Path.Combine(folderpath, BoxId);
            string path_BoxId_ProType = Path.Combine(path_BoxId, NatureofPatent);


            int id = GridView1.SelectedIndex;
            Label filepath = (Label)GridView1.Rows[id].FindControl("lblgetid");
            string path = filepath.Text;       //actual filelocation path  
            string newpath = path.Replace('\\', '/');  // replacing '\' by '/' to view image or pdf
            Session["path"] = newpath;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Test", "ViewPdf()", true);
            //Response.Write("<script>");
            //Response.Write("window.open('DisplayPdf.aspx?val=" + newpath + "','_blank')");
            ////path sent to display.aspx page
            //Response.Write("</script>");

        }
    }
    protected void GVViewFile_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        ImageButton EditButton = (ImageButton)e.Row.FindControl("BtnEdit");
    }
    //Function Delete uploaded files
    //protected void lnkDeleteClick(object sender, EventArgs e)
    //{
    //    string confirmValue = Request.Form["confirm_value"];
    //    if (confirmValue == "Yes")
    //    {
    //        int rownumaux = 0;
    //        int result = 0;
    //        LinkButton lnkbtn = sender as LinkButton;
    //        //getting particular row linkbutton
    //        GridViewRow gvrow = lnkbtn.NamingContainer as GridViewRow;
    //        //getting aptid of particular row
    //        Patent j = new Patent();
    //        PatentBusiness p = new PatentBusiness();

    //        HiddenField lblEntrynum = (HiddenField)GVViewFile.Rows[gvrow.RowIndex].Cells[6].FindControl("lblEntrynum");
    //        rownumaux = Convert.ToInt32(lblEntrynum.Value);

    //        string protype = ddlFilingstatus.SelectedValue;
    //        string id = txtID.Text.Trim();

    //        j.ID = id;
    //        j.Filing_Status = protype;
    //        j.Entrynum = rownumaux;

    //        int count = 0;
    //        Label Filing_Status = (Label)GVViewFile.Rows[gvrow.RowIndex].Cells[5].FindControl("Filing_Status");
    //        string type = Filing_Status.Text;
    //        if (type == "GRN")
    //        {
    //                result = p.UpdatePatentattachedEntry(j);
    //                if (result == 1)
    //                {

    //                    ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Deleted Successfully');</script>");

    //                    //DSforgridview.SelectCommand = "select UploadPDFPath ,q.InfoTypeName as TypeName ,Remark,CreatedDate,EntryNo,p.CreatedBy,FirstName as AddedBy,Unit_Id ,p.InfoTypeId from ProjectAuxillaryDetails p,Project_AuxInfoTypeM q,User_M u where  p.InfoTypeId=q.InfoTypeId and ID='" + j.GID + "' and ProjectUnit='" + j.GrantUnit + "' and Deleted='N' and u.User_Id=p.CreatedBy order by EntryNo";
    //                    //DSforgridview.DataBind();
    //                    //GVViewFile.DataBind();

    //                    if ((Session["Role"].ToString() == "11" || Session["Role"].ToString() == "1"))
    //                    {

    //                        DSforgridview.SelectCommand = "select UploadPDFPath ,Remark , p.CreatedDate, p.CreatedBy as CreatedBy, m.FirstName as AddedBy, EntryNo,Unit_Id ,p.Filing_Status from PatentAuxillaryDetails p, User_M m where  p.CreatedBy='" + Session["UserId"].ToString() + "' and m.User_Id=p.CreatedBy  and ID='" + j.ID + "'  and Deleted='N' order by EntryNo";
    //                        DSforgridview.DataBind();
    //                        GVViewFile.DataBind();

    //                        DSforgridview1.SelectCommand = "select UploadPDFPath  ,Remark , p.CreatedDate, p.CreatedBy as CreatedBy, m.FirstName as AddedBy, EntryNo,Unit_Id ,p.Filing_Status from PatentAuxillaryDetails p, User_M m where  m.User_Id=p.CreatedBy  and ID='" + j.ID + "' and  Deleted='N' and p.CreatedBy  <>  (Select CreatedBy from PatentAuxillaryDetails where ID='" + j.ID + "' and Deleted='N') order by EntryNo";
    //                        DSforgridview1.DataBind();
    //                        GridView1.DataBind();
    //                        Panel8.Visible = true;
    //                    }

    //                    if (Session["Role"].ToString() == "6")
    //                    {
    //                        DSforgridview.SelectCommand = "select UploadPDFPath ,Remark , p.CreatedDate, p.CreatedBy as CreatedBy, m.FirstName as AddedBy, EntryNo,Unit_Id ,p.Filing_Status from PatentAuxillaryDetails p, User_M m where   m.User_Id=p.CreatedBy  and ID='" + j.ID + "' and Deleted='N' and p.CreatedBy  <>  (Select CreatedBy from PatentAuxillaryDetails where  ID='" + j.ID + "' and Deleted='N')  order by EntryNo";
    //                        DSforgridview.DataBind();
    //                        GridView1.DataBind();
    //                        Panel8.Visible = true;

    //                        DSforgridview1.SelectCommand = "select UploadPDFPath  ,Remark , p.CreatedDate, p.CreatedBy as CreatedBy, m.FirstName as AddedBy, EntryNo,Unit_Id ,p.Filing_Status from PatentAuxillaryDetails p, User_M m where  p.CreatedBy!='" + Session["UserId"].ToString() + "' and m.User_Id=p.CreatedBy  and ID='" + j.ID + "' and  Deleted='N' order by EntryNo";
    //                        DSforgridview1.DataBind();
    //                        GVViewFile.DataBind();
    //                    }
    //                }
    //                else
    //                {
    //                    ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Error!!!!!!!');</script>");
    //                    //DSforgridview.SelectCommand = "select UploadPDFPath ,q.InfoTypeName as TypeName ,Remark,CreatedDate,EntryNo,p.CreatedBy,FirstName as AddedBy,Unit_Id,p.InfoTypeId from ProjectAuxillaryDetails p,Project_AuxInfoTypeM q,User_M u where  p.InfoTypeId=q.InfoTypeId and ID='" + j.GID + "' and ProjectUnit='" + j.GrantUnit + "' and Deleted='N' and u.User_Id=p.CreatedBy  order by EntryNo";
    //                    //DSforgridview.DataBind();
    //                    //GVViewFile.DataBind();

    //                    if ((Session["Role"].ToString() == "11" || Session["Role"].ToString() == "1"))
    //                    {
    //                        DSforgridview.SelectCommand = "select UploadPDFPath  ,Remark , p.CreatedDate, p.CreatedBy as CreatedBy, m.FirstName as AddedBy, EntryNo,Unit_Id ,p.Filing_Status from PatentAuxillaryDetails p, User_M m where  p.CreatedBy='" + Session["UserId"].ToString() + "'  and m.User_Id=p.CreatedBy  and ID='" + j.ID + "' and Deleted='N' order by EntryNo";
    //                        DSforgridview.DataBind();
    //                        GVViewFile.DataBind();

    //                        DSforgridview1.SelectCommand = "select UploadPDFPath ,Remark , p.CreatedDate, p.CreatedBy as CreatedBy, m.FirstName as AddedBy, EntryNo,Unit_Id ,p.Filing_Status from PatentAuxillaryDetails p, User_M m where   m.User_Id=p.CreatedBy  and ID='" + j.ID + "' and  Deleted='N' and p.CreatedBy  <>  (Select CreatedBy from PatentAuxillaryDetails where  ID='" + j.ID + "' and Deleted='N')  order by EntryNo";
    //                        DSforgridview1.DataBind();
    //                        GridView1.DataBind();
    //                        Panel8.Visible = true;
    //                    }

    //                    if (Session["Role"].ToString() == "6")
    //                    {
    //                        DSforgridview.SelectCommand = "select UploadPDFPath  ,Remark , p.CreatedDate, p.CreatedBy as CreatedBy, m.FirstName as AddedBy, EntryNo,Unit_Id ,p.Filing_Status from PatentAuxillaryDetails p, User_M m where  m.User_Id=p.CreatedBy  and ID='" + j.ID + "' and  Deleted='N' and p.CreatedBy  <>  (Select CreatedBy from PatentAuxillaryDetails where  ID='" + j.ID + "' and Deleted='N') order by EntryNo";
    //                        DSforgridview.DataBind();
    //                        GridView1.DataBind();
    //                        Panel8.Visible = true;

    //                        DSforgridview1.SelectCommand = "select UploadPDFPath ,Remark , p.CreatedDate, p.CreatedBy as CreatedBy, m.FirstName as AddedBy, EntryNo,Unit_Id ,p.Filing_Status from PatentAuxillaryDetails p, User_M m where  p.CreatedBy!='" + Session["UserId"].ToString() + "' and  m.User_Id=p.CreatedBy  and ID='" + j.ID + "' and  Deleted='N' order by EntryNo";
    //                        DSforgridview1.DataBind();
    //                        GVViewFile.DataBind();
    //                    }
    //                }


    //        }
    //        else
    //        {
    //            result = p.UpdatePatentattachedEntry(j);
    //            if (result == 1)
    //            {

    //                ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Deleted Successfully');</script>");

    //                //DSforgridview.SelectCommand = "select UploadPDFPath ,q.InfoTypeName as TypeName ,Remark,p.CreatedDate,EntryNo,p.CreatedBy,FirstName as AddedBy,Unit_Id,p.InfoTypeId  from ProjectAuxillaryDetails p,Project_AuxInfoTypeM q,User_M u where  p.InfoTypeId=q.InfoTypeId and ID='" + j.GID + "' and ProjectUnit='" + j.GrantUnit + "' and Deleted='N' and u.User_Id=p.CreatedBy order by EntryNo";
    //                //DSforgridview.DataBind();
    //                //GVViewFile.DataBind();
    //                if ((Session["Role"].ToString() == "11" || Session["Role"].ToString() == "1"))
    //                {
    //                    DSforgridview.SelectCommand = "select UploadPDFPath ,Remark , p.CreatedDate, p.CreatedBy as CreatedBy, m.FirstName as AddedBy, EntryNo,Unit_Id ,p.Filing_Status from PatentAuxillaryDetails p, User_M m where  p.CreatedBy='" + Session["UserId"].ToString() + "' and  m.User_Id=p.CreatedBy  and ID='" + j.ID + "' and  and Deleted='N' order by EntryNo";
    //                    DSforgridview.DataBind();
    //                    GVViewFile.DataBind();

    //                    DSforgridview1.SelectCommand = "select UploadPDFPath ,Remark , p.CreatedDate, p.CreatedBy as CreatedBy, m.FirstName as AddedBy, EntryNo,Unit_Id ,p.Filing_Status from PatentAuxillaryDetails p, User_M m where   m.User_Id=p.CreatedBy  and ID='" + j.ID + "'  and Deleted='N' and p.CreatedBy  not in  (Select CreatedBy from PatentAuxillaryDetails where  ID='" + j.ID + "' and Deleted='N')  order by EntryNo";
    //                    DSforgridview1.DataBind();
    //                    GridView1.DataBind();
    //                    Panel8.Visible = true;
    //                }

    //                if (Session["Role"].ToString() == "6")
    //                {
    //                    DSforgridview.SelectCommand = "select UploadPDFPath ,Remark , p.CreatedDate, p.CreatedBy as CreatedBy, m.FirstName as AddedBy, EntryNo,Unit_Id ,p.Filing_Status from PatentAuxillaryDetails p, User_M m where   m.User_Id=p.CreatedBy  and ID='" + j.ID + "' and  Deleted='N' and p.CreatedBy  not in  (Select CreatedBy from PatentAuxillaryDetails where  ID='" + j.ID + "' and Deleted='N')  order by EntryNo";
    //                    DSforgridview.DataBind();
    //                    GridView1.DataBind();
    //                    Panel8.Visible = true;

    //                    DSforgridview1.SelectCommand = "select UploadPDFPath ,Remark, p.CreatedDate, p.CreatedBy as CreatedBy, m.FirstName as AddedBy, EntryNo,Unit_Id ,p.Filing_Status from PatentAuxillaryDetails p, User_M m where  p.CreatedBy!='" + Session["UserId"].ToString() + "' and  m.User_Id=p.CreatedBy  and ID='" + j.ID + "'  and Deleted='N' order by EntryNo";
    //                    DSforgridview1.DataBind();
    //                    GVViewFile.DataBind();
    //                }
    //            }
    //            else
    //            {
    //                ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Error!!!!!!!');</script>");
    //                //DSforgridview.SelectCommand = "select UploadPDFPath ,q.InfoTypeName as TypeName ,Remark,p.CreatedDate,EntryNo,p.CreatedBy,FirstName as AddedBy,Unit_Id,p.InfoTypeId from ProjectAuxillaryDetails p,Project_AuxInfoTypeM q,User_M u where  p.InfoTypeId=q.InfoTypeId and ID='" + j.GID + "' and ProjectUnit='" + j.GrantUnit + "' and Deleted='N' and u.User_Id=p.CreatedBy  order by EntryNo";
    //                //DSforgridview.DataBind();
    //                //GVViewFile.DataBind();
    //                if ((Session["Role"].ToString() == "11" || Session["Role"].ToString() == "1"))
    //                {
    //                    DSforgridview.SelectCommand = "select UploadPDFPath ,Remark , p.CreatedDate, p.CreatedBy as CreatedBy, m.FirstName as AddedBy, EntryNo,Unit_Id ,p.Filing_Status from PatentAuxillaryDetails p, User_M m where  p.CreatedBy='" + Session["UserId"].ToString() + "'  and m.User_Id=p.CreatedBy  and ID='" + j.ID + "' and Deleted='N' order by EntryNo";
    //                    DSforgridview.DataBind();
    //                    GVViewFile.DataBind();

    //                    DSforgridview1.SelectCommand = "select UploadPDFPath ,Remark , p.CreatedDate, p.CreatedBy as CreatedBy, m.FirstName as AddedBy, EntryNo,Unit_Id ,p.Filing_Status from PatentAuxillaryDetails p, User_M m where   m.User_Id=p.CreatedBy  and ID='" + j.ID + "'  and Deleted='N' and p.CreatedBy  not in  (Select CreatedBy from PatentAuxillaryDetails where ID='" + j.ID + "' and Deleted='N')  order by EntryNo";
    //                    DSforgridview1.DataBind();
    //                    GridView1.DataBind();
    //                    Panel8.Visible = true;
    //                }

    //                if (Session["Role"].ToString() == "6")
    //                {
    //                    DSforgridview.SelectCommand = "select UploadPDFPath ,Remark , p.CreatedDate, p.CreatedBy as CreatedBy, m.FirstName as AddedBy, EntryNo,Unit_Id ,p.Filing_Status from PatentAuxillaryDetails p, User_M m where  m.User_Id=p.CreatedBy  and ID='" + j.ID + "' and Deleted='N' and p.CreatedBy  not in  (Select CreatedBy from PatentAuxillaryDetails where  ID='" + j.ID + "' and Deleted='N')  order by EntryNo";
    //                    DSforgridview.DataBind();
    //                    GridView1.DataBind();
    //                    Panel8.Visible = true;

    //                    DSforgridview1.SelectCommand = "select UploadPDFPath ,Remark , p.CreatedDate, p.CreatedBy as CreatedBy, m.FirstName as AddedBy, EntryNo,Unit_Id ,p.Filing_Status from PatentAuxillaryDetails p, User_M m where  p.CreatedBy!='" + Session["UserId"].ToString() + "' and  m.User_Id=p.CreatedBy  and ID='" + j.ID + "'  and Deleted='N' order by EntryNo";
    //                    DSforgridview1.DataBind();
    //                    GVViewFile.DataBind();
    //                }
    //            }

    //        }


    //    }

    //    confirmValue = "";
    //}
    //protected void btnView_Click1(object sender, EventArgs e)
    //{
    //    string BoxId = txtID.Text.Trim();
    //    string NatureofPatent = ddlNatureofPatent.SelectedValue;
    //    string servername = ConfigurationManager.AppSettings["ServerName"].ToString();
    //    string domainame = ConfigurationManager.AppSettings["DomainName"].ToString();
    //    string username = ConfigurationManager.AppSettings["UserName"].ToString();
    //    string password = ConfigurationManager.AppSettings["Password"].ToString();
    //    string folderpath;
    //    string path_BoxId;
    //    using (NetworkShareAccesser.Access(servername, domainame, username, password))
    //    {

    //        folderpath = ConfigurationManager.AppSettings["FolderPathPatent"].ToString();
    //        path_BoxId = Path.Combine(folderpath, BoxId);
    //        string path_BoxId_ProType = Path.Combine(path_BoxId, NatureofPatent);


    //        int id = GridView1.SelectedIndex;
    //        Label filepath = (Label)GridView1.Rows[id].FindControl("lblgetid");
    //        string path = filepath.Text;

    //        ImageButton lnkbtn = sender as ImageButton;
    //        GridViewRow gvrow = lnkbtn.NamingContainer as GridViewRow;
    //        string filePath = "~//FriendPhoto//" + GVViewFile.DataKeys[gvrow.RowIndex].Value.ToString();

    //        Response.Write(String.Format("<script>window.open('{0}','_blank');</script>", "DisplayPdf.aspx?fn=" + filePath));
    //    }
    //}  

    protected void cancel_Click(object sender, EventArgs e)
    {
        string CloseWindow = "";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "CloseWindow", CloseWindow, true);
    }
    protected void ok_Click(object sender, EventArgs e)
    {
        if (!Page.IsValid)
        {
            return;
        }
        try
        {
            string filelocationpath = "";
            string UploadPdf1 = "";
            int result1 = 0;
            Patent pat = new Patent();
            PatentBusiness bus = new PatentBusiness();

            string NatureofPatent = ddlNatureofPatent.SelectedValue;

            Session["file1"] = FileUploadPdf1.FileContent;

            //string uploadedfilename = Path.GetFileName(FileUploadPdf1.PostedFile.FileName);
            //  string uploadedfilename3= Session["uploadedfilename2"].ToString();
            //    double size = FileUploadPdf1.PostedFile.ContentLength;
            //    double size1 = Convert.ToDouble( Session["uploadedfilesize"]); 
            //    if (size < 4194304) //4mb
            //    {
            //        string servername = ConfigurationManager.AppSettings["ServerName"].ToString();
            //        string domainame = ConfigurationManager.AppSettings["DomainName"].ToString();
            //        string username = ConfigurationManager.AppSettings["UserName"].ToString();
            //        string password = ConfigurationManager.AppSettings["Password"].ToString();
            //        string FolderPathServerwrite = ConfigurationManager.AppSettings["FolderPathPatent"].ToString();
            //        using (NetworkShareAccesser.Access(servername, domainame, username, password))
            //        {
            //            var uploadfolder = FolderPathServerwrite;
            //            string path_BoxId = Path.Combine(uploadfolder, txtID.Text); //main path + location
            //            if (!Directory.Exists(path_BoxId))   //if directory doesnt exist
            //            {
            //                Directory.CreateDirectory(path_BoxId);//crete directory of location
            //            }
            //            string path_BoxId_ProType = Path.Combine(path_BoxId, NatureofPatent);

            //            if (!Directory.Exists(path_BoxId_ProType))   //if directory doesnt exist
            //            {
            //                Directory.CreateDirectory(path_BoxId_ProType);//crete directory of department
            //            }
            //            //FileUpload file= new FileUpload();
            //            //file = (FileUpload)Session["file1"];

            //            string uploadedfilename1 = Path.GetFileName(FileUploadPdf1.PostedFile.FileName);
            //            string timestamp = DateTime.Now.ToString("dd-MM-yy-hh-mm-ss");
            //            string fileextension = Path.GetExtension(uploadedfilename3);
            //            string actualfilenamewithtime = NatureofPatent + "_" + timestamp + fileextension;
            //            UploadPdf1 = actualfilenamewithtime;
            //            filelocationpath = Path.Combine(path_BoxId_ProType, actualfilenamewithtime);
            //            FileUploadPdf1.SaveAs(filelocationpath);  //saving file
            //        }
            pat.UploadRemarks = txtRemark.Text.Trim();
            pat.ID = txtID.Text;
            pat.FilePath = Session["FilePath"].ToString();
            pat.Filing_Status = ddlFilingstatus.SelectedValue;


            if (ddlFilingstatus.SelectedValue == "GRN")
            {
                PatentBusiness b1 = new PatentBusiness();
                result1 = bus.UploadPatentPathCreate(pat);

            }


            if ((Session["Role"].ToString() == "11" || Session["Role"].ToString() == "1"))
            {
                DSforgridview.SelectParameters.Clear();
                DSforgridview.SelectParameters.Add("UserId", Session["UserId"].ToString());
                DSforgridview.SelectParameters.Add("ID", pat.ID);

                //DSforgridview.SelectCommand = "select ID, UploadPDFPath  ,Remark , p.CreatedDate, p.CreatedBy as CreatedBy, m.FirstName as AddedBy, EntryNo,Unit_Id, Filing_Status  from PatentAuxillaryDetails p, User_M m where  p.CreatedBy='" + Session["UserId"].ToString() + "'  and m.User_Id=p.CreatedBy  and ID='" + pat.ID + "'  and Deleted='N' order by EntryNo";
                DSforgridview.SelectCommand = "select ID, UploadPDFPath  ,Remark , p.CreatedDate, p.CreatedBy as CreatedBy, m.FirstName as AddedBy, EntryNo,Unit_Id, Filing_Status  from PatentAuxillaryDetails p, User_M m where  p.CreatedBy=@UserId  and m.User_Id=p.CreatedBy  and ID=@ID  and Deleted='N' order by EntryNo";

                DSforgridview.DataBind();
                GVViewFile.DataBind();

                DSforgridview1.SelectParameters.Clear();
              
                DSforgridview1.SelectParameters.Add("ID", pat.ID);

                //DSforgridview1.SelectCommand = "select ID, UploadPDFPath ,Remark , p.CreatedDate, p.CreatedBy as CreatedBy, m.FirstName as AddedBy, EntryNo,Unit_Id,Filing_Status  from PatentAuxillaryDetails p, User_M m where  m.User_Id=p.CreatedBy  and ID='" + pat.ID + "'  and Deleted='N' and p.CreatedBy  not in  (Select CreatedBy from PatentAuxillaryDetails where  ID='" + pat.ID + "' and Deleted='N')  order by EntryNo";
                DSforgridview1.SelectCommand = "select ID, UploadPDFPath ,Remark , p.CreatedDate, p.CreatedBy as CreatedBy, m.FirstName as AddedBy, EntryNo,Unit_Id,Filing_Status  from PatentAuxillaryDetails p, User_M m where  m.User_Id=p.CreatedBy  and ID=@ID  and Deleted='N' and p.CreatedBy  not in  (Select CreatedBy from PatentAuxillaryDetails where  ID=@ID and Deleted='N')  order by EntryNo";

                DSforgridview1.DataBind();
                GridView1.DataBind();
                Panel8.Visible = false;
            }

            if (Session["Role"].ToString() == "6")
            {
                DSforgridview.SelectParameters.Clear();
                DSforgridview.SelectParameters.Add("UserId", Session["UserId"].ToString());
                DSforgridview.SelectParameters.Add("ID", pat.ID);

                //DSforgridview.SelectCommand = "select ID, UploadPDFPath ,Remark , p.CreatedDate, p.CreatedBy as CreatedBy, m.FirstName as AddedBy, EntryNo,Unit_Id,Filing_Status  from PatentAuxillaryDetails p, User_M m where   m.User_Id=p.CreatedBy  and ID='" + pat.ID + "' and Deleted='N' and p.CreatedBy='" + Session["UserId"].ToString() + "' order by EntryNo";
                DSforgridview.SelectCommand = "select ID, UploadPDFPath ,Remark , p.CreatedDate, p.CreatedBy as CreatedBy, m.FirstName as AddedBy, EntryNo,Unit_Id,Filing_Status  from PatentAuxillaryDetails p, User_M m where   m.User_Id=p.CreatedBy  and ID=@ID and Deleted='N' and p.CreatedBy=@UserId order by EntryNo";

                DSforgridview.DataBind();
                GridView1.DataBind();
                Panel8.Visible = false;

                DSforgridview1.SelectParameters.Clear();
                DSforgridview1.SelectParameters.Add("UserId", Session["UserId"].ToString());
                DSforgridview1.SelectParameters.Add("ID", pat.ID);
                //DSforgridview1.SelectCommand = "select ID, UploadPDFPath ,Remark , p.CreatedDate, p.CreatedBy as CreatedBy, m.FirstName as AddedBy, EntryNo,Unit_Id,Filing_Status  from PatentAuxillaryDetails p, User_M m where  p.CreatedBy!='" + Session["UserId"].ToString() + "' and  m.User_Id=p.CreatedBy  and ID='" + pat.ID + "' and Deleted='N' order by EntryNo";
                DSforgridview1.SelectCommand = "select ID, UploadPDFPath ,Remark , p.CreatedDate, p.CreatedBy as CreatedBy, m.FirstName as AddedBy, EntryNo,Unit_Id,Filing_Status  from PatentAuxillaryDetails p, User_M m where  p.CreatedBy!=@UserId and  m.User_Id=p.CreatedBy  and ID=@ID and Deleted='N' order by EntryNo";

                DSforgridview1.DataBind();
                GVViewFile.DataBind();
            }
            string FileUpload1 = "";
            PatentBusiness b = new PatentBusiness();
            FileUpload1 = b.GetPatentFileUploadPath(txtID.Text);
            FileUpload1 = Session["file2"].ToString();
            if (FileUpload1 != "")
            {
                PanelViewUplodedfiles.Visible = true;
                PaneUploadFiles.Visible = true;
                DSforgridview.SelectParameters.Clear();
                DSforgridview.SelectParameters.Add("UserId", Session["UserId"].ToString());
                DSforgridview.SelectParameters.Add("ID", pat.ID);

                DSforgridview.SelectCommand = "select ID, UploadPDFPath  ,Remark , p.CreatedDate, p.CreatedBy as CreatedBy, m.FirstName as AddedBy, EntryNo,Unit_Id,p.Filing_Status  from PatentAuxillaryDetails p, User_M m where  p.CreatedBy=@UserId  and m.User_Id=p.CreatedBy  and ID=@ID  and Deleted='N' order by EntryNo";
                DSforgridview.DataBind();
                GVViewFile.DataBind();


                DSforgridview1.SelectParameters.Clear();
                DSforgridview1.SelectParameters.Add("UserId", Session["UserId"].ToString());
                DSforgridview1.SelectParameters.Add("ID", pat.ID);

                DSforgridview1.SelectCommand = "select ID,UploadPDFPath  ,Remark , p.CreatedDate, p.CreatedBy as CreatedBy, m.FirstName as AddedBy, EntryNo,Unit_Id ,p.Filing_Status from PatentAuxillaryDetails p, User_M m where   m.User_Id=p.CreatedBy  and ID=@ID  and Deleted='N' and p.CreatedBy! =@UserId  order by EntryNo";
                DSforgridview1.DataBind();
                GridView1.DataBind();
                Panel8.Visible = true;
            }
            GVViewFile.Visible = true;
            PaneUploadFiles.Visible = true;
            PanelViewUplodedfiles.Visible = true;
            if (result1 >= 1)
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('File Successfully uploaded!')</script>");
                log.Info("File Successfully uploaded : " + txtID.Text.Trim());
            }
            else
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Error in File upload!!!!')</script>");
                log.Error("Error in File upload!!!! : " + txtID.Text.Trim());

            }

            //}
            //else
            //{
            //    ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Invalid File!!!File exceeds more than 4MB..Please try to upload the file less than or equal to 4MB!!!!!!')</script>");
            //    log.Error("Invalid File!!!File exceeds more than 4MB!!! : " + txtID.Text.Trim() );
            //}
        }

       // }
        catch (Exception ex)
        {
            log.Error("Inside Catch Block Of Grant-file uplaod" + ex.Message + " UserID : " + Session["UserId"].ToString());

            log.Error(ex.StackTrace);

            ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Error!!!!!!!!!!!!')</script>");

        }


    }

    protected void showpopup7(object sender, EventArgs e)
    {
        if (TextBoxProjectDetails.Text != "")
        {
            UpdatePanel5.Update();
            popupPanelProject.Visible = true;
            ArrayList SAmount = new ArrayList();
            string[] toaddress_value = TextBoxProjectDetails.Text.Split(',');
            for (int j = 0; j <= toaddress_value.GetUpperBound(0); j++)
            {
                SAmount.Add(toaddress_value[j]);
            }



            for (int i = 0; i < GridViewProject.Rows.Count; i++)
            {
                CheckBox Checkboxfaculty = (CheckBox)GridViewProject.Rows[i].FindControl("csSelect");
                Label BudgetId = (Label)GridViewProject.Rows[i].FindControl("TextBoxProjectId");
                Label Amount = (Label)GridViewProject.Rows[i].FindControl("TextBoxProjectUnit");

                string projectunit = Amount.Text.ToString();
                string projectID = BudgetId.Text.ToString();
                string projectunitID = projectunit + projectID.ToString();
                for (int j = 0; j < SAmount.Count; j++)
                {
                    if (SAmount[j].ToString() == projectunitID)
                    {

                        Checkboxfaculty.Checked = true;

                    }
                    //else
                    //{
                    //    Checkboxfaculty.Checked = false;
                    //}
                }
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "CallMyFunction", "callthis7()", true);
            return;
        }
        else 
        {
            UpdatePanel5.Update();
            setModalWindowproject(sender, e);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "CallMyFunction", "callthis7()", true);
           
        }
       

    }

    private void setModalWindowproject(object sender, EventArgs e)
    {
        popupPanelProject.Visible = true;
        GridViewProject.Visible = true;   
        GridViewProject.DataSourceID = "SqlDataSourceProject";
        SqlDataSourceProject.DataBind();
        GridViewProject.DataBind();
    }
    protected void DropDownListhasProjectreference_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownListhasProjectreference.SelectedValue == "Y")
        {
            LabelProjectDetails.Visible = true;
            TextBoxProjectDetails.Visible = true;
            ImageButtonProject.Visible = true;
            
            popupPanelProject.Visible = true;
            GridViewProject.Visible = true;
            //SqlDataSourceProject.SelectCommand = "select Project.ID,Project.ProjectUnit,Title from Project inner join Projectnvestigator on  Project.ID=Projectnvestigator.ID and Project.ProjectUnit=Projectnvestigator.ProjectUnit and Projectnvestigator.EmployeeCode='" + Session["UserId"].ToString() + "' and (ProjectStatus='SAN' or ProjectStatus='CLO')";
            GridViewProject.DataSourceID = "SqlDataSourceProject";
            SqlDataSourceProject.DataBind();
            GridViewProject.DataBind();
        }
        else
        {           
            popupPanelProject.Visible = false;
            GridViewProject.Visible = false;
            //SqlDataSourceProject.SelectCommand = "select Project.ID,Project.ProjectUnit,Title from Project inner join Projectnvestigator on  Project.ID=Projectnvestigator.ID and Project.ProjectUnit=Projectnvestigator.ProjectUnit and Projectnvestigator.EmployeeCode='" + Session["UserId"].ToString() + "' and (ProjectStatus='SAN' or ProjectStatus='CLO')";
            GridViewProject.DataSourceID = "SqlDataSourceProject";
            SqlDataSourceProject.DataBind();
            GridViewProject.DataBind();
            LabelProjectReference.Visible = true;
            DropDownListhasProjectreference.Visible = true;
            //DropDownListhasProjectreference.SelectedValue = "N";
            LabelProjectDetails.Visible = false;
            TextBoxProjectDetails.Visible = false;
            ImageButtonProject.Visible = false;
        }
    }
    protected void Redirect(Object sender, EventArgs e)
    {
        LinkButton lb = (LinkButton)sender;
        GridViewRow row = (GridViewRow)lb.NamingContainer;
        int index = row.RowIndex; //gets the row index selected      
        var lblProjectID = row.FindControl("TextBoxProjectId") as Label;
        var lblProjectUnit = row.FindControl("TextBoxProjectUnit") as Label;
        string id = lblProjectID.Text;
        string projectunit = lblProjectUnit.Text;
        Response.Redirect("~/GrantEntry/GrantEntryView.aspx?ProjectID=" + id + "&ProjectUnit=" + projectunit + "&Keyword=" + "PatentProject");

    }
    protected void Button7_Click(object sender, EventArgs e)
    {
        ArrayList SAmount = new ArrayList();
        for (int i = 0; i < GridViewProject.Rows.Count; i++)
        {
            CheckBox Checkboxfaculty = (CheckBox)GridViewProject.Rows[i].FindControl("csSelect");
            Label BudgetId = (Label)GridViewProject.Rows[i].FindControl("TextBoxProjectId");
            Label Amount = (Label)GridViewProject.Rows[i].FindControl("TextBoxProjectUnit");

            if (Checkboxfaculty.Checked == true)
            {
                string id = Amount.Text + BudgetId.Text;
                SAmount.Add(id);
                //string amount = Amount.Text;
            }

        }
        if (SAmount.Count == 0)
        {
            string CloseWindow = "alert('Please select the Project!')";
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow, true);
            return;
        }
        string amountf = "";
        for (int j = 0; j < SAmount.Count; j++)
        {
            if (j == 0)
            {
                amountf = SAmount[j].ToString();
            }
            else
            {
                amountf = amountf + ',' + SAmount[j].ToString();
            }
            TextBoxProjectDetails.Text = amountf;
        }
    }
    protected void GridViewProject_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            GridView grid = (GridView)sender;

            Label ProjectId = (Label)e.Row.FindControl("TextBoxProjectId");
            CheckBox nextDay = (CheckBox)e.Row.FindControl("csSelect");
            if (ProjectId.Text != "" && ProjectId.Text != null)
            {
                nextDay.Visible = true;
            }
            else
            {
                Page.Form.Attributes.Add("enctype", "multipart/form-data");
                nextDay.Visible = false;
            }

        }
    }
}



