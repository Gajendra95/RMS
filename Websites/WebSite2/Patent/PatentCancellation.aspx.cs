using AjaxControlToolkit;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class GrantEntry_PatentCancellation : System.Web.UI.Page
{
    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    private bool result2, result1;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            PopAppStage.Visible = false;
            PoppanelRenewal.Visible = false;
            //  ModalPopupApp.Show();
            btnview.Enabled = false;
            Btnsubmit.Visible = false;
            BtnDraft.Visible = false;
            //setModalWindow(sender, e);
            SetInitialRow();
        }
    }



    // bind author popup grid on text change
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
            SqlDataSourceAffil.SelectParameters.Clear();
            SqlDataSourceAffil.SelectParameters.Add("affiliateSrch", affiliateSrch.Text);
            //SqlDataSourceAffil.SelectCommand = "SELECT  User_Id,prefix+' '+UPPER(firstname)+' '+UPPER(middlename)+' '+UPPER(lastname)  as Name from User_M where  firstname like '%" + affiliateSrch.Text + "%'";
            SqlDataSourceAffil.SelectCommand = "SELECT  User_Id,prefix+' '+UPPER(firstname)+' '+UPPER(middlename)+' '+UPPER(lastname)  as Name from User_M where  firstname like '%' + @affiliateSrch + '%'";


            popGridAffil.DataBind();
            popGridAffil.Visible = true;
        }

        string rowVal = Request.Form["rowIndx"];
        int rowIndex = Convert.ToInt32(rowVal);

        ModalPopupExtender ModalPopupExtender8 = (ModalPopupExtender)Grid_AuthorEntry.Rows[rowIndex].FindControl("ModalPopupExtender4");
        ModalPopupExtender8.Show();

    }
    //On Row delete of autor grdiview

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
            }
        }
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
        dt.Columns.Add(new DataColumn("ContinentId", typeof(string)));

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
        else if (DropdownMuNonMu.SelectedValue == "E")
        {
            EmployeeCodeBtn.Enabled = false;
        }
        else if (DropdownMuNonMu.SelectedValue == "S")
        {
            EmployeeCodeBtn.Enabled = false;
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
    //Function to Add authors
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

                            NationalType.Visible = false;
                            ContinentId.Visible = false;
                            EmployeeCodeBtnImg.Enabled = true;
                            dtCurrentTable.Rows[i - 1]["NationalType"] = NationalType.SelectedValue;
                            dtCurrentTable.Rows[i - 1]["ContinentId"] = ContinentId.SelectedValue;

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
                        else if (DropdownMuNonMu.Text == "E")
                        {

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
                            dtCurrentTable.Rows[i - 1]["ContinentId"] = ContinentId.SelectedValue;
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

        setModalWindow(sender, e); // initialise popup gridviews
    }


    //set previous row author 
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

                    HiddenField EmployeeCode1 = (HiddenField)Grid_AuthorEntry.Rows[0].Cells[0].FindControl("EmployeeCode");
                    HiddenField Institution1 = (HiddenField)Grid_AuthorEntry.Rows[0].Cells[0].FindControl("Institution");
                    TextBox InstitutionName1 = (TextBox)Grid_AuthorEntry.Rows[0].Cells[6].FindControl("InstitutionName");
                    HiddenField Department1 = (HiddenField)Grid_AuthorEntry.Rows[0].Cells[0].FindControl("Department");
                    TextBox DepartmentName1 = (TextBox)Grid_AuthorEntry.Rows[0].Cells[0].FindControl("DepartmentName");
                    TextBox MailId1 = (TextBox)Grid_AuthorEntry.Rows[0].Cells[0].FindControl("MailId");


                    // DropDownList AuthorType1 = (DropDownList)Grid_AuthorEntry.Rows[0].Cells[0].FindControl("AuthorType");
                    DropDownList DropdownStudentInstitutionName1 = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("DropdownStudentInstitutionName");
                    DropDownList DropdownStudentDepartmentName1 = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("DropdownStudentDepartmentName");


                    DropdownMuNonMu.Text = dt.Rows[i]["DropdownMuNonMu"].ToString();
                    AuthorName.Text = dt.Rows[i]["AuthorName"].ToString();
                    EmployeeCode.Text= dt.Rows[i]["EmployeeCode"].ToString();
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
                        ContinentId.Text = dt.Rows[i]["ContinentId"].ToString();

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
                        ContinentId.Text = dt.Rows[i]["ContinentId"].ToString();


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

                        NationalType.Visible = true;
                        NationalType.Text = dt.Rows[i]["NationalType"].ToString();
                        ContinentId.Text = dt.Rows[i]["ContinentId"].ToString();


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
                        MailId.Enabled = false;
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

    protected void setModalWindow(object sender, EventArgs e)
    {
        popupPanelAffil.Visible = true;
        popGridAffil.DataSourceID = "SqlDataSourceAffil";
        SqlDataSourceAffil.DataBind();
        popGridAffil.DataBind();
        popGridAffil.Visible = true;
    }
    protected void exit(object sender, EventArgs e)
    {
        affiliateSrch.Text = "";
        popGridAffil.DataBind();
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

                    TextBox AuthorName = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[1].FindControl("AuthorName");
                    ImageButton EmployeeCodeBtnImg = (ImageButton)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("EmployeeCodeBtn");

                    DropDownList DropdownMuNonMu = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[2].FindControl("DropdownMuNonMu");
                    //TextBox amount = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[3].FindControl("amount");
                    HiddenField EmployeeCode = (HiddenField)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("EmployeeCode");
                    HiddenField Institution = (HiddenField)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("Institution");
                    TextBox InstitutionName = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[6].FindControl("InstitutionName");
                    HiddenField Department = (HiddenField)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("Department");
                    TextBox DepartmentName = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("DepartmentName");
                    TextBox MailId = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("MailId");

                    //  DropDownList isCorrAuth = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("isCorrAuth");
                    // DropDownList AuthorType = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("AuthorType");

                    DropDownList DropdownStudentInstitutionName = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("DropdownStudentInstitutionName");
                    DropDownList DropdownStudentDepartmentName = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("DropdownStudentDepartmentName");

                    DropDownList NationalType = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("NationalType");
                    DropDownList ContinentId = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("ContinentId");

                    ImageButton EmployeeCodeBtnImg1 = (ImageButton)Grid_AuthorEntry.Rows[0].Cells[0].FindControl("EmployeeCodeBtn");

                    drCurrentRow = dtCurrentTable.NewRow();
                    dtCurrentTable.Rows[i - 1]["DropdownMuNonMu"] = DropdownMuNonMu.Text;
                    dtCurrentTable.Rows[i - 1]["AuthorName"] = AuthorName.Text;
                    dtCurrentTable.Rows[i - 1]["EmployeeCode"] = EmployeeCode.Value;

                    if (DropdownMuNonMu.Text == "M")
                    {
                        DropdownStudentInstitutionName.Visible = false;
                        DropdownStudentDepartmentName.Visible = false;
                        InstitutionName.Visible = true;
                        DepartmentName.Visible = true;

                        NationalType.Visible = false;
                        ContinentId.Visible = false;

                        EmployeeCodeBtnImg.Enabled = true;

                        dtCurrentTable.Rows[i - 1]["NationalType"] = NationalType.Text;
                        dtCurrentTable.Rows[i - 1]["ContinentId"] = ContinentId.Text;

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
                    else if (DropdownMuNonMu.Text == "E")
                    {
                        DropdownStudentInstitutionName.Visible = false;
                        DropdownStudentDepartmentName.Visible = false;
                        InstitutionName.Visible = true;
                        DepartmentName.Visible = true;

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
                    else if (DropdownMuNonMu.Text == "S")
                    {
                        DropdownStudentInstitutionName.Visible = true;
                        DropdownStudentDepartmentName.Visible = true;
                        InstitutionName.Visible = false;
                        DepartmentName.Visible = false;

                        NationalType.Visible = false;
                        ContinentId.Visible = false;

                        dtCurrentTable.Rows[i - 1]["NationalType"] = NationalType.Text;
                        dtCurrentTable.Rows[i - 1]["ContinentId"] = ContinentId.Text;

                        EmployeeCodeBtnImg.Enabled = false;

                        dtCurrentTable.Rows[i - 1]["Institution"] = DropdownStudentInstitutionName.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["Department"] = DropdownStudentDepartmentName.SelectedValue;
                    }
                    EmployeeCodeBtnImg1.Enabled = false;
                    dtCurrentTable.Rows[i - 1]["MailId"] = MailId.Text;
                    // dtCurrentTable.Rows[i - 1]["AuthorType"] = AuthorType.Text;
                    rowIndex++;
                }

                ViewState["CurrentTable"] = dtCurrentTable;
            }
        }
        else
        {
            Response.Write("ViewState is null");
        }

    }
    protected void DropdownMuNonMuOnSelectedIndexChanged(object sender, EventArgs e)
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
        if (DropdownMuNonMu.SelectedValue == "M")
        {
            DropdownStudentInstitutionName1.Visible = false;
            DropdownStudentDepartmentName.Visible = false;
            InstitutionName.Visible = true;
            DepartmentName.Visible = true;

            NationalType.Visible = false;
            EmployeeCodeBtn.Enabled = true;
            InstitutionName.Enabled = false;
            DepartmentName.Enabled = false;
            AuthorName.Enabled = false;
            MailId.Enabled = false;
            AuthorName.Text = "";
            MailId.Text = "";
            InstitutionName.Text = "";
            DepartmentName.Text = "";

        }
        else if (DropdownMuNonMu.SelectedValue == "N")
        {

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
            MailId.Text = "";
            InstitutionName.Text = "";
            DepartmentName.Text = "";

        }
        else if (DropdownMuNonMu.SelectedValue == "E")
        {

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
            MailId.Text = "";
            InstitutionName.Text = "";
            DepartmentName.Text = "";

        }
        else if (DropdownMuNonMu.SelectedValue == "S")
        {

            DropdownStudentInstitutionName1.Visible = true;
            DropdownStudentDepartmentName.Visible = true;
            InstitutionName.Visible = false;
            DepartmentName.Visible = false;

            NationalType.Visible = false;
            EmployeeCodeBtn.Enabled = false;
            InstitutionName.Enabled = true;
            DepartmentName.Enabled = true;
            AuthorName.Enabled = true;
            MailId.Enabled = true;
            AuthorName.Text = "";
            MailId.Text = "";
            InstitutionName.Text = "";
            DepartmentName.Text = "";
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
            setModalWindowApp(sender, e);


            PatentBusiness Pat_Busobj = new PatentBusiness();
            Patent pat = new Patent();

            if (ViewState["ID"] != null)
            {
                pat.ID = ViewState["ID"].ToString();
            }


            if (ddlFilingstatus.SelectedValue != "REJ" || ddlFilingstatus.SelectedValue != "GRN")
            {
                if (ddlNatureofPatent.SelectedValue == "Select")
                {

                    ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('NatureOfPatent !')</script>");

                    return;
                }
                else
                {
                    pat.NatureOfPatent = Convert.ToByte(ddlNatureofPatent.SelectedValue.ToString());
                }
                // 
                if (ddlFunding.SelectedValue == "Select")
                {

                    ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Funding !')</script>");

                    return;
                }
                else
                {
                    pat.Funding = Convert.ToByte(ddlFunding.SelectedValue.ToString());
                }
                if (txtdateofApplication.Text.ToString() != "")
                {
                    pat.Date_Of_Application = DateTime.ParseExact(txtdateofApplication.Text, "dd/MM/yyyy", null);

                }
            }

            if (ddlFilingstatus.SelectedValue != "REJ")
            {
                if (ViewState["App_Status"] != null)
                {
                    pat.App_Status = ViewState["App_Status"].ToString();
                }
                //else
                //{
                //    ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('App_Status..  For update Click on search and edit  !')</script>");

                //    return;
                //}
            }

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
            pat.Pat_UTN = txtPatUTN.Text;
            pat.DetailsColaInstitiuteIndustry = txtdetailsCII.Text;
            pat.Country = txtcountry.Text;
            pat.RevenueGenerated = Convert.ToDouble( txtrevenue.Text);
            if (ddlFilingstatus.SelectedValue == "APP")
            {
                pat.Entry_status = "Draft";
            }
            if (ddlFilingstatus.SelectedValue == "GRN")
            {
                pat.Entry_status = "Submitted";
            }
            if (ddlFilingstatus.SelectedValue == "REJ")
            {
                pat.Entry_status = "Submitted";
            }
            pat.Filing_Status = ddlFilingstatus.SelectedValue.ToString();
            pat.Filing_Office = ddlfilingoffice.SelectedValue.ToString();
            // 

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
            if (txtlastRenewalFee.Text.ToString() != "")
            {
                pat.LastRenewalFeePaiddate = DateTime.ParseExact(txtlastRenewalFee.Text, "dd/MM/yyyy", null);

            }
            // pat.LastRenewalFeePaiddate = Convert.ToDateTime(txtlastRenewalFee.Text);
            pat.Remarks = txtRemark.Text;
            pat.Renewal_Comment = txtRenewalComment.Text;
            pat.RejectedBy = Session["UserId"].ToString();

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
                    HiddenField EmployeeCode = (HiddenField)Grid_AuthorEntry.Rows[rowIndex1].Cells[0].FindControl("EmployeeCode");
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
                    else if (DropdownMuNonMu.SelectedValue == "N" || DropdownMuNonMu.SelectedValue == "E")
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
                    if (JD[i].MUNonMU == "M")
                    {
                        JD[i].EmployeeCode = EmployeeCode.Value;
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
            if (ddlFilingstatus.SelectedValue == "APP")
            {

                bool result = Pat_Busobj.InsertPatent(pat, JD);

                if (result == true)
                {
                    string a = Session["patentseed"].ToString();
                    txtID.Text = a;
                    txtPatUTN.Text = Session["patentseedUTNseed"].ToString();
                    ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Patent Data Created Successfully..  For update Click on search and edit  !')</script>");
                    log.Info("Patent created Successfully, of ID: " + txtID.Text);
                    //txtPatUTN.Text = Session["GrantseedUTNseed"].ToString();
                    //BtnDraft.Visible = true;
                    Btnsave.Visible = false;
                }
                else
                {
                    log.Error("Grant creation Error of ID: " + txtPatUTN.Text);
                    ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Grant Error')</script>");
                }
            }

            //if (ddlFilingstatus.SelectedValue == "GRN")
            //{
            //    result2 = Pat_Busobj.UpdatePatent(pat, JD);
            if (ddlFilingstatus.SelectedValue == "GRN")
            {
                result2 = Pat_Busobj.UpdateGrantPatent(pat, JD);

                if (result2 == true)
                {
                    if (ddlFilingstatus.SelectedValue == "GRN")
                    {
                        ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Patent Data Updated Successfully. !')</script>");
                    }
                }
            }
            else if (ddlFilingstatus.SelectedValue == "REJ")
            {

                result1 = Pat_Busobj.UpdateStatusPatentRejectApproval(pat, JD);

                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Patent Data Rejected Successfully. !')</script>");
                Btnsave.Enabled = false;
                Btnsubmit.Enabled = false;
                Panelfilling.Enabled = false;
            }


        }
        catch (Exception ex)
        {
            log.Error("InsertPatentData catch block of patent IDpate: ");
            log.Error(ex.Message);
            log.Error(ex.StackTrace);
            //transaction.Rollback();
            throw ex;
        }
    }

    protected void GridViewSearchPatent_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        ImageButton EditButton = (ImageButton)e.Row.FindControl("BtnEdit");
    }
    public void GridViewSearchPatent_OnRowedit(Object sender, GridViewEditEventArgs e)
    {
        GridViewSearchPatent.EditIndex = e.NewEditIndex;
        SelectPatent(sender, e);
      
    }
    protected void GridViewSearchPatent_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        dataBind();
        GridViewSearchPatent.PageIndex = e.NewPageIndex;
        GridViewSearchPatent.DataBind();
    }
    protected void ButtonSearchProjectOnClick(object sender, EventArgs e)
    {
        // int role = Convert.ToInt16(Session["Role"]);

        //GridViewSearchPatent.Visible = true;
        //GridViewSearchPatent.EditIndex = -1;
        //GridViewSearchPatent.Visible = true;
        GridViewSearchPatent.EditIndex = -1;
        dataBind();
    }

    private void dataBind()
    {
        SqlDataSource1.SelectParameters.Clear();
        if (PatIDSearch.Text == "" && TextBoxtiltleSearch.Text == "")
        {
            SqlDataSource1.SelectCommand = " select p.ID,p.Title,p.Entry_Status,s.StatusName as Filling_Status from Patent_Data p,Patent_Status s where p.Filing_Status=s.Id and p.Entry_Status!='Cancelled' ";
        }
        else if (PatIDSearch.Text != "" && TextBoxtiltleSearch.Text == "")
        {
            SqlDataSource1.SelectCommand = " select p.ID,p.Title,p.Entry_Status,s.StatusName as Filling_Status from Patent_Data p,Patent_Status s where p.Filing_Status=s.Id and p.ID  LIKE '%' + @ID + '%' and p.Entry_Status!='Cancelled' ";
            SqlDataSource1.SelectParameters.Add("ID", PatIDSearch.Text.Trim());
        }

        else if (PatIDSearch.Text == "" && TextBoxtiltleSearch.Text != "")
        {
            SqlDataSource1.SelectCommand = "  select p.ID,p.Title,p.Entry_Status,s.StatusName as Filling_Status from Patent_Data p,Patent_Status s where p.Filing_Status=s.Id and p.Title  LIKE  '%' + @Title + '%' and p.Entry_Status!='Cancelled' ";
           
            SqlDataSource1.SelectParameters.Add("Title", TextBoxtiltleSearch.Text.Trim());
        }
        else
        {
            SqlDataSource1.SelectCommand = " select p.ID,p.Title,p.Entry_Status,s.StatusName as Filling_Status from Patent_Data p,Patent_Status s where p.Filing_Status=s.Id and p.ID  LIKE '%' + @ID + '%' and p.Title  LIKE '%' + @Title + '%'  and p.Entry_Status!='Cancelled'   ";
            SqlDataSource1.SelectParameters.Add("ID", PatIDSearch.Text.Trim());
            SqlDataSource1.SelectParameters.Add("Title", TextBoxtiltleSearch.Text.Trim());
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
            Session["ID"] = ID;

        }

    }
    //Textbox author name changed
    protected void AuthorName_Changed(object sender, EventArgs e)
    {
        GridViewRow currentRow = (GridViewRow)((TextBox)sender).Parent.Parent;
        TextBox AuthorName = (TextBox)currentRow.FindControl("AuthorName");
        //  TextBox NameInJournal = (TextBox)currentRow.FindControl("NameInJournal");
        TextBox InstitutionName = (TextBox)currentRow.FindControl("InstitutionName");
        InstitutionName.Focus();
    }

    //on row select of pop up autor
    protected void popSelected1(Object sender, EventArgs e)
    {
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

        HiddenField employc = (HiddenField)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("EmployeeCode");
        employc.Value = EmployeeCode1;

        TextBox AuthorName = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("AuthorName");
        AuthorName.Text = a.UserNamePrefix + " " + a.UserFirstName + " " + a.UserMiddleName + " " + a.UserLastName;


        affiliateSrch.Text = "";
        popGridAffil.DataBind();
        popupPanelAffil.Visible = false;
    }
    private void SelectPatent(object sender, GridViewEditEventArgs e)
    {
       

       


        PopAppStage.Visible = false;
        string ID = Session["ID"].ToString();
        // string PT_UTN = Session["patentseedUTNseed"].ToString();
        Patent Pat = new Patent();
        Patent_DAobject bus_obj = new Patent_DAobject();
        Btnsave.Enabled = false;

            panelPatentCanelRemark.Visible = true;
       
        if (ddlFilingstatus.SelectedValue == "REJ")
        {
            ddlFilingstatus.Enabled = false;
            Panelfilling.Enabled = false;
            txtRejectionRemark.Visible = true;
            lblRejectRemarks.Visible = true;
            //txtRejectionRemark.Visible = true;
            //lblRejectRemarks.Visible = true;
        }
        if (ddlFilingstatus.SelectedValue == "APP")
        {
            //BtnDraft.Visible = true;
            //setModalWindowApp(sender, e);
        }
        Pat = bus_obj.SelectPatent(ID);
        txtID.Text = ID;
        txtPatUTN.Text = Pat.Pat_UTN;
        txtTitle.Text = Pat.Title;
        txtdetailsCII.Text = Pat.DetailsColaInstitiuteIndustry;
        txtcountry.Text=Pat.Country;
        if (Pat.RevenueGenerated != 0)
        {
            txtrevenue.Text = Pat.RevenueGenerated.ToString();
        }  

        SqlDataSourePatentStatus.SelectCommand = "Select * from Patent_Status where Id!='CAN' and Id!='EXP' and Id!='LAP'";
        ddlFilingstatus.DataSourceID = "SqlDataSourePatentStatus";
        ddlFilingstatus.DataBind();
        ddlFilingstatus.SelectedValue = Pat.Filing_Status;
        if (ddlFilingstatus.SelectedValue == "GRN")
        {
            SqlDataSourePatentStatus.SelectCommand = "Select * from Patent_Status where Id!='CAN' and Id !='APP' and Id!='REJ' ";
            ddlFilingstatus.DataSourceID = "SqlDataSourePatentStatus";
            ddlFilingstatus.DataBind();
            ddlFilingstatus.SelectedValue = Pat.Filing_Status;
           // PoppanelRenewal.Visible = true;
            BtnDraft.Visible = false;
           // Btnsave.Enabled = true;
            //btnRenewalview.Enabled = true;
            BtnAPPsave.Enabled = false;
        }
       ddlfilingoffice.SelectedValue = Pat.Filing_Office.ToString();
        ddlFunding.SelectedValue = Pat.Funding.ToString();
        ddlNatureofPatent.Text = Pat.NatureOfPatent.ToString();
        if (Pat.Date_Of_Application.ToString() != "01/01/0001 00:00:00")
        {
            txtdateofApplication.Text = Pat.Date_Of_Application.ToShortDateString();
        }
        // txtdateofApplication.Text = Pat.Date_Of_Application.ToShortDateString();
        txtApplicationStage.Text = Pat.Application_Stage;
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
        txtRenewalFee.Text = Pat.Renewal_Fee;
        if (Pat.LastRenewalFeePaiddate.ToString() != "01/01/0001 00:00:00")
        {
            txtlastRenewal.Text = Pat.LastRenewalFeePaiddate.ToShortDateString();
        }
        //if (Pat.LastRenewalFeePaiddate.ToString() != "01/01/0001 00:00:00")
        //{
        //    txtlastRenewalFee.Text = Pat.LastRenewalFeePaiddate.ToShortDateString();
        //}
        txtRemark.Text = Pat.Remarks;
        txtRejectionRemark.Text = Pat.Rejection_Remark;
        GridViewSearchPatent.Visible = false;

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
                    EmployeeCodeBtnimg.Visible = false;
                    
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
        ModalPopupApp.Show();
        popgridApp.DataBind();
        popgridApp.DataSourceID = "SqlDataSource5";
        popgridApp.Visible = true;

    }
    protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //Find the DropDownList in the Row
            DropDownList DropdownMuNonMu = (e.Row.FindControl("DropdownMuNonMu") as DropDownList);

            //if (DropDownListPublicationEntry.SelectedValue == "BK" || DropDownListPublicationEntry.SelectedValue == "CP" || DropDownListPublicationEntry.SelectedValue == "NM")
            //{


            SqlDataSourceAuthorType.SelectCommand = "SELECT Id,DisplayName FROM [Author_Type_M]";

            DropdownMuNonMu.DataTextField = "DisplayName";
            DropdownMuNonMu.DataValueField = "Id";
            DropdownMuNonMu.DataBind();

        }

    }
   
  
    protected void OnselectFilingStatus(object sender, EventArgs e)
    {
        if (ddlFilingstatus.SelectedValue == "REJ")
        {
            txtRejectionRemark.Visible = true;
            lblRejectRemarks.Visible = true;
            Panelfilling.Enabled = false;
            Btnsave.Enabled = true;
            Btnsubmit.Enabled = false;
            ddlFilingstatus.Enabled = false;
            PoppanelRenewal.Visible = false;
        }
        else
        {
            txtRejectionRemark.Visible = false;
            lblRejectRemarks.Visible = false;
        }
        if (ddlFilingstatus.SelectedValue == "GRN")
        {
            PoppanelRenewal.Visible = true;
            Btnsave.Enabled = true;
            //Btnsubmit.Enabled = true;
            //  Btnsubmit.Visible = true;
            Btnsave.Visible = true;
            BtnDraft.Visible = false;
            txtGrantDate.Enabled = true;
            txtlastRenewal.Enabled = true;
            btnRenewalview.Enabled = true;
            BtnAPPsave.Enabled = false;
        }

    }

    protected void Btn_Data_Sumbit(object sender, EventArgs e)
    {
        if (!Page.IsValid)
        {
            return;
        }

        try
        {

            PatentBusiness PatBus_obj = new PatentBusiness();
            Patent pat = new Patent();

            pat.ID = txtID.Text;
            pat.Title = txtTitle.Text;


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
            pat.Entry_status = "Submitted";
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
            pat.RevenueGenerated = Convert.ToDouble(txtrevenue.Text); 
            
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
            if (txtlastRenewalFee.Text.ToString() != "")
            {
                pat.LastRenewalFeePaiddate = DateTime.ParseExact(txtlastRenewalFee.Text, "dd/MM/yyyy", null);

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
                    HiddenField EmployeeCode = (HiddenField)Grid_AuthorEntry.Rows[rowIndex1].Cells[0].FindControl("EmployeeCode");
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
                            ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please enter Institution Name!')</script>");
                            return;

                        }

                        if (DepartmentName.Text == "")
                        {
                            ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please enter Department Name!')</script>");
                            return;

                        }
                    }
                    else if (DropdownMuNonMu.SelectedValue == "N" || DropdownMuNonMu.SelectedValue == "E")
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
                    if (JD[i].MUNonMU == "M")
                    {
                        JD[i].EmployeeCode = EmployeeCode.Value;
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

            if (ddlFilingstatus.SelectedValue == "APP")
            {
                result2 = PatBus_obj.UpdatePatent(pat, JD);
            }
            if (ddlFilingstatus.SelectedValue == "GRN")
            {
                result2 = PatBus_obj.UpdateGrantPatent(pat, JD);
            }
            if (result2 == true)
            {
                Btnsubmit.Enabled = false;
                Btnsave.Enabled = false;
                BtnDraft.Visible = false;
                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Patent Submitted!')</script>");
                return;

            }
        }
        catch (Exception ex)
        {
            log.Error("InsertPatentData catch block of patent IDpate: ");
            log.Error(ex.Message);
            log.Error(ex.StackTrace);
            //transaction.Rollback();
            throw ex;
        }
    }
    protected void Btn_App_View(object sender, EventArgs e)
    {
        setModalWindowApp(sender, e);
        if (ddlFilingstatus.SelectedValue == "GRN")
        {
            BtnAPPsave.Enabled = false;
        }

    }

    private void setModalWindowApp(object sender, EventArgs e)
    {
        ModalPopupApp.Show();
        popgridApp.DataBind();
        popgridApp.DataSourceID = "SqlDataSource5";
        SqlDataSource5.DataBind();
        popgridApp.Visible = true;
        PopAppStage.Visible = true;
    }
    //private void setModalWindowRenewal(object sender, EventArgs e)
    //{
    //    Modalpoprenewal.Show();
    //    grdRenewal.DataBind();
    //    grdRenewal.DataSourceID = "sqlRenewal";
    //    sqlRenewal.DataBind();
    //    grdRenewal.Visible = true;
    //    PoppanelRenewal.Visible = true;
    //}



    protected void btnApp_Submit(object sender, EventArgs e)
    {
        if (!Page.IsValid)
        {
            return;
        }

        try
        {
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
            if (ddlFilingstatus.SelectedValue == "APP")
            {
                result2 = Bus_obj.InsertApplicationStage(pat);
            }

            if (result2 == true)
            {
                Btnsubmit.Enabled = true;
                Btnsave.Enabled = false;
                BtnDraft.Visible = true;
                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Application Stage Saved!')</script>");
                return;

            }


        }
        catch (Exception ex)
        {
            log.Error("InsertPatentData catch block of patent IDpate: ");
            log.Error(ex.Message);
            log.Error(ex.StackTrace);
            //transaction.Rollback();
            throw ex;
        }

    }
    protected void btn_Clear(object sender, EventArgs e)
    {
        txtID.Text = "";
        txtPatUTN.Text = "";
        ddlFilingstatus.ClearSelection();
        ddlNatureofPatent.ClearSelection();
        ddlFunding.ClearSelection();
        txtTitle.Text = "";
        txtde.Text = "";
        txtdetailsCII.Text="";
         txtcountry.Text="";
        txtrevenue.Text="";
        Grid_AuthorEntry.DataSource = null;
        Grid_AuthorEntry.DataBind();
        ddlfilingoffice.ClearSelection();
        txtApplicationStage.Text = "";
        txtapplicationNo.Text = "";
        txtdateofApplication.Text = "";
        //txtProvisionalNo.Text = "";
        txtPatentNo.Text = "";
        //txtFilingDateProvided.Text = "";
        txtGrantDate.Text = "";
        txtRenewalFee.Text = "";
        txtlastRenewalFee.Text = "";
        txtRemark.Text = "";
        txtRejectionRemark.Text = "";

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


            if (ddlFilingstatus.SelectedValue != "REJ" || ddlFilingstatus.SelectedValue != "GRN")
            {
                if (ddlNatureofPatent.SelectedValue == "Select")
                {

                    ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('NatureOfPatent !')</script>");

                    return;
                }
                else
                {
                    pat.NatureOfPatent = Convert.ToByte(ddlNatureofPatent.SelectedValue.ToString());
                }
                // 
                if (ddlFunding.SelectedValue == "Select")
                {

                    ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Funding !')</script>");

                    return;
                }
                else
                {
                    pat.Funding = Convert.ToByte(ddlFunding.SelectedValue.ToString());
                }
                if (txtdateofApplication.Text.ToString() != "")
                {
                    pat.Date_Of_Application = DateTime.ParseExact(txtdateofApplication.Text, "dd/MM/yyyy", null);

                }
            }

            if (ViewState["App_Status"] != null)
            {
                pat.App_Status = ViewState["App_Status"].ToString();
            }
            else
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('App_Status..  For update Click on search and edit  !')</script>");

                return;
            }
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


            //if (ddlFilingstatus.SelectedValue=="APP")
            //{
            //    pat.Entry_status = "Submit";
            //}
            //if (ddlFilingstatus.SelectedValue == "GRN")
            //{
            pat.Entry_status = "Draft";
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
            if (txtlastRenewalFee.Text.ToString() != "")
            {
                pat.LastRenewalFeePaiddate = DateTime.ParseExact(txtlastRenewalFee.Text, "dd/MM/yyyy", null);

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
                    HiddenField EmployeeCode = (HiddenField)Grid_AuthorEntry.Rows[rowIndex1].Cells[0].FindControl("EmployeeCode");
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
                            ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please enter Institution Name!')</script>");
                            return;

                        }

                        if (DepartmentName.Text == "")
                        {
                            ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please enter Department Name!')</script>");
                            return;

                        }
                    }
                    else if (DropdownMuNonMu.SelectedValue == "N" || DropdownMuNonMu.SelectedValue == "E")
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
                    if (JD[i].MUNonMU == "M")
                    {
                        JD[i].EmployeeCode = EmployeeCode.Value;
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
                        else if (JD[i].MUNonMU == "E")
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
                    BtnDraft.Enabled = false;
                    Btnsave.Enabled = false;
                    Btnsubmit.Enabled = true;
                    Btnsubmit.Visible = true;
                    ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Patent Updated!')</script>");
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
                    Btnsubmit.Enabled = true;
                    ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Patent Submitted!')</script>");
                    return;

                }
            }


        }
        catch (Exception ex)
        {
            log.Error("InsertPatentData catch block of patent IDpate: ");
            log.Error(ex.Message);
            log.Error(ex.StackTrace);
            //transaction.Rollback();
            throw ex;
        }
    }
    protected void btnRenewal_Click(object sender, EventArgs e)
    {
        if (!Page.IsValid)
        {
            return;
        }

        try
        {
            PatentBusiness Bus_obj = new PatentBusiness();
            Patent pat = new Patent();

            pat.ID = txtID.Text;
            pat.Renewal_Fee = txtRenewalFee.Text;
            if (txtlastRenewalFee.Text.ToString() != "")
            {
                pat.LastRenewalFeePaiddate = DateTime.ParseExact(txtlastRenewalFee.Text, "dd/MM/yyyy", null);
            }
            pat.Renewal_Comment = txtRenewalComment.Text;

            pat.Created_By = Session["UserId"].ToString();
            pat.Created_Date = DateTime.Now;
            txtlastRenewal.Text = pat.LastRenewalFeePaiddate.ToShortDateString();
            if (ddlFilingstatus.SelectedValue == "GRN")
            {
                result2 = Bus_obj.InsertRenwalaDetails(pat);
            }

            if (result2 == true)
            {
                // Btnsubmit.Enabled = true;
                Btnsave.Enabled = true;

                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Renewal Details Saved!')</script>");
                return;

            }


        }
        catch (Exception ex)
        {
            log.Error("InsertPatentData catch block of patent IDpate: ");
            log.Error(ex.Message);
            log.Error(ex.StackTrace);
            //transaction.Rollback();
            throw ex;
        }

    }
    protected void Btn_Save_Cancel(object sender, EventArgs e)
    {
        try
        {

            PatentBusiness b = new PatentBusiness();

            Patent j = new Patent();

            if (txtCancelRemarks.Text == "")
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please enter the Remarks for the Patent Cancellation!!!')</script>");
                return;
            }
            string Id = txtID.Text.Trim();           
            j.ID = Id;
            j.Filing_Status = ddlFilingstatus.SelectedValue;
            j.Entry_status = "Cancelled";
            j.CancelledBy = Session["UserId"].ToString();
            j.CancelRemarks = txtCancelRemarks.Text.Trim();
            int result = 0;
            result = b.UpdatePatentCancelStatus(j);

            if (result == 1)
            {
               // btnSave.Enabled = false;
                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Patent Data Cancelled Successfully  of ID: " + txtID.Text + "')</script>");
                log.Info("Grant Cancelled Successfully, of ID: " + txtID.Text);
               // SendMail();
            }
            else
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Patent Error')</script>");
                log.Error("Grant Updated Error!!!,  of ID: " + txtID.Text);

            }
        }

        catch (Exception ex)
        {
            log.Error("Inside Catch Block Of Grant Cancellation" + ex.Message + " UserID : " + Session["UserId"].ToString());

            log.Error(ex.StackTrace);

            ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Patent data Cancellation Failed')</script>");

        }

    }
}