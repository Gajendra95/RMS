using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class GrantEntry_PatentPrintEvaluationForm : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            PanelMain.Visible = false;
            ButtonSavepdf.Visible = false;
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
        else if (DropdownMuNonMu.SelectedValue == "N")
        {


            ContinentId.Visible = false;


        }
        else
        {
            ContinentId.Visible = false;

        }

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
            }
        }
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
                    else if (DropdownMuNonMu.Text == "S")
                    {
                        DropdownStudentInstitutionName.Visible = true;
                        DropdownStudentDepartmentName.Visible = true;
                        InstitutionName.Visible = false;
                        DepartmentName.Visible = false;

                        NationalType.Visible = false;
                        ContinentId.Visible = false;

                        dtCurrentTable.Rows[i - 1]["NationalType"] = NationalType.Text;
                        dtCurrentTable.Rows[i - 1]["Continent"] = ContinentId.Text;

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
                    HiddenField EmployeeCode = (HiddenField)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("EmployeeCode");
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
                    EmployeeCode.Value = dt.Rows[i]["EmployeeCode"].ToString();
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
                        ContinentId.Text = dt.Rows[i]["Continent"].ToString();

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
                        ContinentId.Text = dt.Rows[i]["Continent"].ToString();


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
                        ContinentId.Text = dt.Rows[i]["Continent"].ToString();


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
                        AuthorName.Enabled = true;
                        InstitutionName.Enabled = false;
                        DepartmentName.Enabled = false;
                        MailId.Enabled = true;


                        NationalType.Visible = false;
                        ContinentId.Visible = false;

                        NationalType.Text = dt.Rows[i]["NationalType"].ToString();
                        ContinentId.Text = dt.Rows[i]["Continent"].ToString();

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

    protected void GridViewSearchPatent_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        ImageButton EditButton = (ImageButton)e.Row.FindControl("BtnEdit");
    }
    public void GridViewSearchPatent_OnRowedit(Object sender, GridViewEditEventArgs e)
    {
        GridViewSearchPatent.EditIndex = e.NewEditIndex;
        SelectPatent(sender, e);
        //if (ddlFilingstatus.SelectedValue == "APP")
        //{
        //    setModalWindowApp(sender, e);
        //}
      //  btnview.Enabled = true;

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
    protected void GridViewSearchPatent_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridViewSearchPatent.PageIndex = e.NewPageIndex;
        GridViewSearchPatent.DataBind();
        dataBind();
    }
    protected void ButtonSearchProjectOnClick(object sender, EventArgs e)
    {
        // int role = Convert.ToInt16(Session["Role"]);

        GridViewSearchPatent.EditIndex = -1;
        dataBind();
    }

    private void dataBind()
    {
        SqlDataSource1.SelectParameters.Clear(); 
        if (PatIDSearch.Text == "" && TextBoxtiltleSearch.Text == "")
        {
            SqlDataSource1.SelectCommand = " select p.ID,p.Title,p.Entry_Status,s.StatusName as Filling_Status from Patent_Data p,Patent_Status s where p.Filing_Status=s.Id and p.Filing_Status='GRN'";
        }
        else if (PatIDSearch.Text != "" && TextBoxtiltleSearch.Text == "")
        {
            SqlDataSource1.SelectCommand = " select p.ID,p.Title,p.Entry_Status,s.StatusName as Filling_Status from Patent_Data p,Patent_Status s where p.Filing_Status=s.Id and p.ID  LIKE '%'+@ID+'%'  and p.Filing_Status='GRN' ";
            SqlDataSource1.SelectParameters.Add("ID", PatIDSearch.Text.Trim());
        }

        else if (PatIDSearch.Text == "" && TextBoxtiltleSearch.Text != "")
        {
            SqlDataSource1.SelectCommand = "  select p.ID,p.Title,p.Entry_Status,s.StatusName as Filling_Status from Patent_Data p,Patent_Status s where p.Filing_Status=s.Id and p.Title  LIKE  '%'+@Title+'%' and p.Filing_Status='GRN' ";
            SqlDataSource1.SelectParameters.Add("Title", TextBoxtiltleSearch.Text.Trim());
        }
        else
        {
            SqlDataSource1.SelectCommand = " select p.ID,p.Title,p.Entry_Status,s.StatusName as Filling_Status from Patent_Data p,Patent_Status s where p.Filing_Status=s.Id and p.ID  LIKE '%'+@ID+'%'  and p.Title  LIKE  '%'+@Title+'%' and p.Filing_Status='GRN' ";
            SqlDataSource1.SelectParameters.Add("ID", PatIDSearch.Text.Trim());
            SqlDataSource1.SelectParameters.Add("Title", TextBoxtiltleSearch.Text.Trim());
        }
        GridViewSearchPatent.DataBind();
        SqlDataSource1.DataBind();
        GridViewSearchPatent.Visible = true;
    }

    protected void OnselectFilingStatus(object sender, EventArgs e)
    {
        if (ddlFilingstatus.SelectedValue == "REJ")
        {
            txtRejectionRemark.Visible = true;
            lblRejectRemarks.Visible = true;
            Panelfilling.Enabled = false;
          //  Btnsave.Enabled = true;
           // Btnsubmit.Enabled = false;
            ddlFilingstatus.Enabled = false;
        //    PoppanelRenewal.Visible = false;
        }
        else
        {
            txtRejectionRemark.Visible = false;
            lblRejectRemarks.Visible = false;
        }
        if (ddlFilingstatus.SelectedValue == "GRN")
        {
            //PoppanelRenewal.Visible = true;
            //Btnsave.Enabled = true;
            //Btnsubmit.Enabled = true;
            //  Btnsubmit.Visible = true;
          //  Btnsave.Visible = true;
          //  BtnDraft.Visible = false;
            txtGrantDate.Enabled = true;
            txtlastRenewal.Enabled = true;
            //btnRenewalview.Enabled = true;
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

    private void SelectPatent(object sender, GridViewEditEventArgs e)
    {
        ButtonSavepdf.Visible = true;
        PanelMain.Visible = true;
        PanelMain.Enabled = false;
        panAddAuthor.Enabled = false;
     //   PopAppStage.Visible = false;
        string ID = Session["ID"].ToString();
        // string PT_UTN = Session["patentseedUTNseed"].ToString();
        Patent Pat = new Patent();
        Patent_DAobject bus_obj = new Patent_DAobject();
       // Btnsave.Enabled = false;
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
           // BtnDraft.Visible = true;
          //  setModalWindowApp(sender, e);
        }
        Pat = bus_obj.SelectPatent(ID);
        txtID.Text = ID;
        txtPatUTN.Text = Pat.Pat_UTN;
        txtTitle.Text = Pat.Title;
        txtdetailsCII.Text = Pat.DetailsColaInstitiuteIndustry;
        txtcountry.Text = Pat.Country;
        txtrevenue.Text =Convert.ToString( Pat.RevenueGenerated);

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
          //  BtnDraft.Visible = false;
          //  Btnsave.Enabled = true;
            //btnRenewalview.Enabled = true;
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
       // txtRenewalFee.Text = Pat.Renewal_Fee;
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
                //ImageButton ImageButton1 = (ImageButton)Grid_AuthorEntry.Rows[rowIndex].Cells[2].FindControl("ImageButton1");


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
                    //ImageButton1.Visible = true;
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
                    //ImageButton1.Visible = false;
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
        //ModalPopupApp.Show();
        //popgridApp.DataBind();
        //popgridApp.DataSourceID = "SqlDataSource5";
        //popgridApp.Visible = true;

    }
    protected void BtnGenetratePdf(object sender, EventArgs e)
    {

        try
        {

            string id = null;
          
            id = Session["ID"].ToString();
            PDFP pdfhelper = new PDFP();
           // PatentPDF pdfhelper = new PatentPDF();
            pdfhelper.pdfGenerate(id);
            //Journal_DataObject obj = new Journal_DataObject();
            //DataSet dz = obj.fnfindjournalAccount1(Pid);
            //  ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('PDF generated sucessfully')</script>");
            ButtonSavepdf.Enabled = false;
          //  BtnPdf.Enabled = true;


            string Pid;
            //Pid = "00000177";
            string hospnum = Session["TempPid"].ToString();
            //string hospnum = Pid;

            Journal_DataObject obj = new Journal_DataObject();
            User p = new User();


            // TreatmentPlan p1 = new TreatmentPlan();
            p = obj.fnGetFilePathPdf(hospnum);
            //pa = p1.fnGetFilePathPdf1(hospnum);
            string path = null;
            path = p.path;


            //FileInfo myfile = new FileInfo(path);
            //try
            //{
            //    if (myfile.Exists)
            //    {

            //        log.Info("Evaluation Form Generated Successfully,  for Publiction ID: " + TextBoxPubId.Text + " of Type: " + DropDownListPublicationEntry.SelectedValue + " by " + Session["UserName"]);
            //        Response.ClearContent();
            //        Response.AddHeader("Content-Disposition", "attachment; filename=" + path);
            //        Response.AddHeader("Content-Length", myfile.Length.ToString());
            //        Response.ContentType = "application/vnd.ms-excel";
            //        Response.TransmitFile(myfile.FullName);
            //        Response.End();
            //    }
            //}
            //catch (Exception e2)
            //{
            //    log.Error("Evaluation Form generation failed,  for Publiction ID: " + TextBoxPubId.Text + " of Type: " + DropDownListPublicationEntry.SelectedValue + " by " + Session["UserName"]);
            //    log.Error("Error: , " + e2);
            //    ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert(Evaluation Form generation failed')</script>");
            //}
        }

        catch (Exception e2)
        {
           // log.Error("Evaluation Form generation failed,  for Publiction ID: " + TextBoxPubId.Text + " of Type: " + DropDownListPublicationEntry.SelectedValue + " by " + Session["UserName"]);
           // log.Error("Error: , " + e2);
            ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert(Evaluation Form generation failed')</script>");
        }
    }
}