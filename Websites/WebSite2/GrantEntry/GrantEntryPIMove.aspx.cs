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

public partial class GrantEntry_GrantEntryPIMove : System.Web.UI.Page
{

    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    Business B = new Business();
    Journal_DataObject JournalDataObj = new Journal_DataObject();
    JournalData JournalValueObj = new JournalData();
    private GrantData[] jd;
     protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SetInitialRow();
            setModalWindow(sender, e);
        }

    }


     protected void setModalWindow(object sender, EventArgs e)
     {
         popup.Visible = true;
         //popupPanelAffilUpdate.Update();
         popGridAffil.DataSourceID = "SqlDataSourceAffil";
         SqlDataSourceAffil.DataBind();
         popGridAffil.DataBind();
         popupStudentGrid.DataSourceID = "StudentSQLDS";
         StudentSQLDS.DataBind();
         popupStudentGrid.DataBind();
         //popGridAffil.Visible = true;
     }
     protected void exit(object sender, EventArgs e)
     {

         affiliateSrch.Text = "";
         popGridAffil.DataBind();
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
            SqlDataSourceAffil.SelectParameters.Add("firstname", affiliateSrch.Text);
            SqlDataSourceAffil.SelectCommand = "SELECT  User_Id,prefix+' '+UPPER(firstname)+' '+UPPER(middlename)+' '+UPPER(lastname)  as Name from User_M where  firstname like '%'+@firstname+'%'";

            popGridAffil.DataBind();
            popGridAffil.Visible = true;
        }

        string rowVal1 = Request.Form["rowIndx"];
        int rowIndex = Convert.ToInt32(rowVal1);
        int row = Convert.ToInt16(rowVal.Value);
        DropDownList munonmu = (DropDownList)Grid_AuthorEntry.Rows[row].FindControl("DropdownMuNonMu");
        if (munonmu.SelectedValue == "M")
        {
            popupPanelAffil.Visible = true;
            popupstudent.Visible = false;
            //popupPanelAffil.Style.Add("display", "true");
            //popupstudent.Style.Add("display", "none");
        }
        else if (munonmu.SelectedValue == "S")
        {
            popupPanelAffil.Visible = false;
            popupstudent.Visible = true;
            //popupPanelAffil.Style.Add("display", "none");
            //popupstudent.Style.Add("display", "true");
        }
        else if (munonmu.SelectedValue == "O")
        {
            popupPanelAffil.Visible = false;
            popupstudent.Visible = false;
            //popupPanelAffil.Style.Add("display", "none");
            //popupstudent.Style.Add("display", "none");
        }
        ModalPopupExtender ModalPopupExtender8 = (ModalPopupExtender)Grid_AuthorEntry.Rows[row].FindControl("ModalPopupExtender4");
        ModalPopupExtender8.Show();


    }
    protected void StudentDataSelect(Object sender, EventArgs e)
    {
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

    }
    protected void SearchStudentData(object sender, EventArgs e)
    {
        //if (txtSrchStudentName.Text.Trim() == "" && txtSrchStudentRollNo.Text.Trim() == "" && StudentIntddl.SelectedValue == "")
        //{
        //    StudentSQLDS.SelectCommand = "Select TOP 10  RollNo,Name,SISClass.ClassName as ClassName,SISInstitution.InstName as InstName,EmailID1,SISStudentGenInfo.ClassCode as ClassCode,SISInstnHR.HRInstitute as InstID from SISStudentGenInfo,SISClass,SISInstitution,SISInstnHR  where SISStudentGenInfo.ClassCode=SISClass.ClassCode and SISStudentGenInfo.InstID=SISInstitution.InstID and SISInstnHR.Institute_Id=SISInstitution.InstID and SISInstnHR.Institute_Id=SISStudentGenInfo.InstID";
        //    popupStudentGrid.DataBind();
        //    popupStudentGrid.Visible = true;
        //}

        //else if ((txtSrchStudentName.Text.Trim() != "" || txtSrchStudentRollNo.Text.Trim() != "") && StudentIntddl.SelectedValue == "")
        //{
        //    StudentSQLDS.SelectParameters.Clear();
        //    StudentSQLDS.SelectParameters.Add("Name", txtSrchStudentName.Text);
        //    StudentSQLDS.SelectParameters.Add("RollNo", txtSrchStudentRollNo.Text);
        //    StudentSQLDS.SelectCommand = "Select TOP 10  RollNo,Name,SISClass.ClassName as ClassName,SISInstitution.InstName as InstName,EmailID1 ,SISStudentGenInfo.ClassCode as ClassCode,SISInstnHR.HRInstitute as InstID from SISStudentGenInfo,SISClass,SISInstitution,SISInstnHR  where SISStudentGenInfo.ClassCode=SISClass.ClassCode and SISStudentGenInfo.InstID=SISInstitution.InstID and SISInstnHR.Institute_Id=SISInstitution.InstID and SISInstnHR.Institute_Id=SISStudentGenInfo.InstID and  Name like '%'+@Name+'%' and RollNo like '%'+@RollNo+'%'";
        //    popupStudentGrid.DataBind();
        //    popupStudentGrid.Visible = true;
        //}
        //else
        //{
        //    StudentSQLDS.SelectParameters.Clear();
        //    StudentSQLDS.SelectParameters.Add("Name", txtSrchStudentName.Text);
        //    StudentSQLDS.SelectParameters.Add("RollNo", txtSrchStudentRollNo.Text);
        //    StudentSQLDS.SelectParameters.Add("InstID", StudentIntddl.SelectedValue);
        //    StudentSQLDS.SelectCommand = "Select TOP 10  RollNo,Name,SISClass.ClassName as ClassName,SISInstitution.InstName as InstName,EmailID1 ,SISStudentGenInfo.ClassCode as ClassCode,SISInstnHR.HRInstitute as InstID from SISStudentGenInfo,SISClass,SISInstitution,SISInstnHR  where SISStudentGenInfo.ClassCode=SISClass.ClassCode and SISStudentGenInfo.InstID=SISInstitution.InstID and SISInstnHR.Institute_Id=SISInstitution.InstID and SISInstnHR.Institute_Id=SISStudentGenInfo.InstID and  (Name like '%'+@Name+'%' and RollNo like '%'+@RollNo+'%' and (SISStudentGenInfo.InstID=@InstID) ) ";
        //    popupStudentGrid.DataBind();
        //    popupStudentGrid.Visible = true;

        StudentSQLDS.SelectParameters.Clear();
        if (txtSrchStudentName.Text.Trim() == "" && txtSrchStudentRollNo.Text.Trim() == "" && StudentIntddl.SelectedValue == "")
        {
            StudentSQLDS.SelectCommand = "Select TOP 10  RollNo,Name,SISClass.ClassName as ClassName,SISInstitution.InstName as InstName,EmailID1,SISStudentGenInfo.ClassCode as ClassCode,SISInstnHR.HRInstitute as InstID from SISStudentGenInfo,SISClass,SISInstitution,SISInstnHR  where SISStudentGenInfo.ClassCode=SISClass.ClassCode and SISStudentGenInfo.InstID=SISInstitution.InstID and SISInstnHR.Institute_Id=SISInstitution.InstID and SISInstnHR.Institute_Id=SISStudentGenInfo.InstID";
            popupStudentGrid.DataBind();
            popupStudentGrid.Visible = true;
        }
        //here aug 02
        else if ((txtSrchStudentName.Text.Trim() != "" && txtSrchStudentRollNo.Text.Trim() == "") && StudentIntddl.SelectedValue == "")
        {
            StudentSQLDS.SelectParameters.Add("txtSrchStudentName", txtSrchStudentName.Text.Trim());


            StudentSQLDS.SelectCommand = "Select TOP 10  RollNo,Name,SISClass.ClassName as ClassName,SISInstitution.InstName as InstName,EmailID1 ,SISStudentGenInfo.ClassCode as ClassCode,SISInstnHR.HRInstitute as InstID from SISStudentGenInfo,SISClass,SISInstitution,SISInstnHR  where SISStudentGenInfo.ClassCode=SISClass.ClassCode and SISStudentGenInfo.InstID=SISInstitution.InstID and SISInstnHR.Institute_Id=SISInstitution.InstID and SISInstnHR.Institute_Id=SISStudentGenInfo.InstID and  Name like '%' + @txtSrchStudentName + '%'";

            popupStudentGrid.DataBind();
            popupStudentGrid.Visible = true;
        }
        else if ((txtSrchStudentName.Text.Trim() == "" && txtSrchStudentRollNo.Text.Trim() != "") && StudentIntddl.SelectedValue == "")
        {
            StudentSQLDS.SelectParameters.Add("txtSrchStudentRollNo", txtSrchStudentRollNo.Text.Trim());


            StudentSQLDS.SelectCommand = "Select TOP 10  RollNo,Name,SISClass.ClassName as ClassName,SISInstitution.InstName as InstName,EmailID1 ,SISStudentGenInfo.ClassCode as ClassCode,SISInstnHR.HRInstitute as InstID from SISStudentGenInfo,SISClass,SISInstitution,SISInstnHR  where SISStudentGenInfo.ClassCode=SISClass.ClassCode and SISStudentGenInfo.InstID=SISInstitution.InstID and SISInstnHR.Institute_Id=SISInstitution.InstID and SISInstnHR.Institute_Id=SISStudentGenInfo.InstID and  RollNo like '%' + @txtSrchStudentRollNo+ '%'";

            popupStudentGrid.DataBind();
            popupStudentGrid.Visible = true;
        }
        else if ((txtSrchStudentName.Text.Trim() == "" && txtSrchStudentRollNo.Text.Trim() == "") && StudentIntddl.SelectedValue != "")
        {
            StudentSQLDS.SelectParameters.Add("StudentIntddl", StudentIntddl.SelectedValue);

            StudentSQLDS.SelectCommand = "Select TOP 10  RollNo,Name,SISClass.ClassName as ClassName,SISInstitution.InstName as InstName,EmailID1 ,SISStudentGenInfo.ClassCode as ClassCode,SISInstnHR.HRInstitute as InstID from SISStudentGenInfo,SISClass,SISInstitution,SISInstnHR  where SISStudentGenInfo.ClassCode=SISClass.ClassCode and SISStudentGenInfo.InstID=SISInstitution.InstID and SISInstnHR.Institute_Id=SISInstitution.InstID and SISInstnHR.Institute_Id=SISStudentGenInfo.InstID and   (SISStudentGenInfo.InstID=@StudentIntddl)";

            popupStudentGrid.DataBind();
            popupStudentGrid.Visible = true;
        }
        else if ((txtSrchStudentName.Text.Trim() == "" && txtSrchStudentRollNo.Text.Trim() != "") && StudentIntddl.SelectedValue != "")
        {

            StudentSQLDS.SelectParameters.Add("txtSrchStudentRollNo", txtSrchStudentRollNo.Text.Trim());
            StudentSQLDS.SelectParameters.Add("StudentIntddl", StudentIntddl.SelectedValue);

            //StudentSQLDS.SelectCommand = "Select TOP 10  RollNo,Name,SISClass.ClassName as ClassName,SISInstitution.InstName as InstName,EmailID1 ,SISStudentGenInfo.ClassCode as ClassCode,SISInstnHR.HRInstitute as InstID from SISStudentGenInfo,SISClass,SISInstitution,SISInstnHR  where SISStudentGenInfo.ClassCode=SISClass.ClassCode and SISStudentGenInfo.InstID=SISInstitution.InstID and SISInstnHR.Institute_Id=SISInstitution.InstID and SISInstnHR.Institute_Id=SISStudentGenInfo.InstID and  Name like '" + txtSrchStudentName.Text + "%' and RollNo like '%" + txtSrchStudentRollNo.Text + "%'";
            StudentSQLDS.SelectCommand = "Select TOP 10  RollNo,Name,SISClass.ClassName as ClassName,SISInstitution.InstName as InstName,EmailID1 ,SISStudentGenInfo.ClassCode as ClassCode,SISInstnHR.HRInstitute as InstID from SISStudentGenInfo,SISClass,SISInstitution,SISInstnHR  where SISStudentGenInfo.ClassCode=SISClass.ClassCode and SISStudentGenInfo.InstID=SISInstitution.InstID and SISInstnHR.Institute_Id=SISInstitution.InstID and SISInstnHR.Institute_Id=SISStudentGenInfo.InstID  and RollNo like '%' + @txtSrchStudentRollNo+ '%' and (SISStudentGenInfo.InstID=@StudentIntddl)";

            popupStudentGrid.DataBind();
            popupStudentGrid.Visible = true;
        }
        else if ((txtSrchStudentName.Text.Trim() != "" && txtSrchStudentRollNo.Text.Trim() == "") && StudentIntddl.SelectedValue != "")
        {

            StudentSQLDS.SelectParameters.Add("txtSrchStudentName", txtSrchStudentName.Text.Trim());
            StudentSQLDS.SelectParameters.Add("StudentIntddl", StudentIntddl.SelectedValue);

            //StudentSQLDS.SelectCommand = "Select TOP 10  RollNo,Name,SISClass.ClassName as ClassName,SISInstitution.InstName as InstName,EmailID1 ,SISStudentGenInfo.ClassCode as ClassCode,SISInstnHR.HRInstitute as InstID from SISStudentGenInfo,SISClass,SISInstitution,SISInstnHR  where SISStudentGenInfo.ClassCode=SISClass.ClassCode and SISStudentGenInfo.InstID=SISInstitution.InstID and SISInstnHR.Institute_Id=SISInstitution.InstID and SISInstnHR.Institute_Id=SISStudentGenInfo.InstID and  Name like '" + txtSrchStudentName.Text + "%' and RollNo like '%" + txtSrchStudentRollNo.Text + "%'";
            StudentSQLDS.SelectCommand = "Select TOP 10  RollNo,Name,SISClass.ClassName as ClassName,SISInstitution.InstName as InstName,EmailID1 ,SISStudentGenInfo.ClassCode as ClassCode,SISInstnHR.HRInstitute as InstID from SISStudentGenInfo,SISClass,SISInstitution,SISInstnHR  where SISStudentGenInfo.ClassCode=SISClass.ClassCode and SISStudentGenInfo.InstID=SISInstitution.InstID and SISInstnHR.Institute_Id=SISInstitution.InstID and SISInstnHR.Institute_Id=SISStudentGenInfo.InstID  and Name like '%' + @txtSrchStudentName + '%' and (SISStudentGenInfo.InstID=@StudentIntddl)";

            popupStudentGrid.DataBind();
            popupStudentGrid.Visible = true;
        }


            //ends

        else if ((txtSrchStudentName.Text.Trim() != "" || txtSrchStudentRollNo.Text.Trim() != "") && StudentIntddl.SelectedValue == "")
        {
            StudentSQLDS.SelectParameters.Add("txtSrchStudentName", txtSrchStudentName.Text.Trim());
            StudentSQLDS.SelectParameters.Add("txtSrchStudentRollNo", txtSrchStudentRollNo.Text.Trim());

            //StudentSQLDS.SelectCommand = "Select TOP 10  RollNo,Name,SISClass.ClassName as ClassName,SISInstitution.InstName as InstName,EmailID1 ,SISStudentGenInfo.ClassCode as ClassCode,SISInstnHR.HRInstitute as InstID from SISStudentGenInfo,SISClass,SISInstitution,SISInstnHR  where SISStudentGenInfo.ClassCode=SISClass.ClassCode and SISStudentGenInfo.InstID=SISInstitution.InstID and SISInstnHR.Institute_Id=SISInstitution.InstID and SISInstnHR.Institute_Id=SISStudentGenInfo.InstID and  Name like '" + txtSrchStudentName.Text + "%' and RollNo like '%" + txtSrchStudentRollNo.Text + "%'";
            StudentSQLDS.SelectCommand = "Select TOP 10  RollNo,Name,SISClass.ClassName as ClassName,SISInstitution.InstName as InstName,EmailID1 ,SISStudentGenInfo.ClassCode as ClassCode,SISInstnHR.HRInstitute as InstID from SISStudentGenInfo,SISClass,SISInstitution,SISInstnHR  where SISStudentGenInfo.ClassCode=SISClass.ClassCode and SISStudentGenInfo.InstID=SISInstitution.InstID and SISInstnHR.Institute_Id=SISInstitution.InstID and SISInstnHR.Institute_Id=SISStudentGenInfo.InstID and  Name like '%' + @txtSrchStudentName + '%' and RollNo like '%' + @txtSrchStudentRollNo+ '%'";

            popupStudentGrid.DataBind();
            popupStudentGrid.Visible = true;
        }


        else
        {
            StudentSQLDS.SelectParameters.Add("txtSrchStudentName", txtSrchStudentName.Text);
            StudentSQLDS.SelectParameters.Add("txtSrchStudentRollNo", txtSrchStudentRollNo.Text);
            StudentSQLDS.SelectParameters.Add("StudentIntddl", StudentIntddl.SelectedValue);
            //StudentSQLDS.SelectCommand = "Select TOP 10  RollNo,Name,SISClass.ClassName as ClassName,SISInstitution.InstName as InstName,EmailID1 ,SISStudentGenInfo.ClassCode as ClassCode,SISInstnHR.HRInstitute as InstID from SISStudentGenInfo,SISClass,SISInstitution,SISInstnHR  where SISStudentGenInfo.ClassCode=SISClass.ClassCode and SISStudentGenInfo.InstID=SISInstitution.InstID and SISInstnHR.Institute_Id=SISInstitution.InstID and SISInstnHR.Institute_Id=SISStudentGenInfo.InstID and  (Name like '" + txtSrchStudentName.Text + "%' and RollNo like '%" + txtSrchStudentRollNo.Text + "%' and (SISStudentGenInfo.InstID='" + StudentIntddl.SelectedValue + "') ) ";
            StudentSQLDS.SelectCommand = "Select TOP 10  RollNo,Name,SISClass.ClassName as ClassName,SISInstitution.InstName as InstName,EmailID1 ,SISStudentGenInfo.ClassCode as ClassCode,SISInstnHR.HRInstitute as InstID from SISStudentGenInfo,SISClass,SISInstitution,SISInstnHR  where SISStudentGenInfo.ClassCode=SISClass.ClassCode and SISStudentGenInfo.InstID=SISInstitution.InstID and SISInstnHR.Institute_Id=SISInstitution.InstID and SISInstnHR.Institute_Id=SISStudentGenInfo.InstID and  (Name like '%' + @txtSrchStudentName + '%' and RollNo like '%' + @txtSrchStudentRollNo+ '%' and (SISStudentGenInfo.InstID=@StudentIntddl) ) ";
            popupStudentGrid.DataBind();
            popupStudentGrid.Visible = true;
        }


        // string rowVal = Request.Form["rowIndx"];
        string a = rowVal.Value;
        int rowIndex = Convert.ToInt32(a);
        DropDownList munonmu = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].FindControl("DropdownMuNonMu");
        if (munonmu.SelectedValue == "M")
        {
            popupstudent.Visible = false;
            popupPanelAffil.Visible = true;
            //popupPanelAffil.Style.Add("display", "true");
            //popupstudent.Style.Add("display", "none");
        }
        else if (munonmu.SelectedValue == "S")
        {
            popupstudent.Visible = true;
            popupPanelAffil.Visible = false;
            //popupstudent.Style.Add("display", "true");
            //popupPanelAffil.Style.Add("display", "none");
        }
        ModalPopupExtender ModalPopupExtender8 = (ModalPopupExtender)Grid_AuthorEntry.Rows[rowIndex].FindControl("ModalPopupExtender2");
        ModalPopupExtender8.Show();
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

        TextBox EmployeeCode = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("AuthorName");
        EmployeeCode.Text = EmployeeCode1;

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
    }

 //Investigator
    protected void OnRowDataBound1(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList DropdownMuNonMu = (e.Row.FindControl("DropdownMuNonMu") as DropDownList);
            SqlDataSourceAuthorType.SelectCommand = "SELECT Id,DisplayName FROM [Author_Type_M] where (Id = 'M') or (Id = 'S') or (Id = 'N') or (Id = 'O') or (Id = 'E')";

            DropdownMuNonMu.DataTextField = "DisplayName";
            DropdownMuNonMu.DataValueField = "Id";
            DropdownMuNonMu.DataBind();
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
        dt.Columns.Add(new DataColumn("AuthorType", typeof(string)));
        dt.Columns.Add(new DataColumn("isLeadPI", typeof(string)));
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
        dr["AuthorType"] = string.Empty;
        dr["isLeadPI"] = string.Empty;
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
        DropDownList AuthorType = (DropDownList)Grid_AuthorEntry.Rows[0].Cells[0].FindControl("AuthorType");
        DropDownList isLeadPI = (DropDownList)Grid_AuthorEntry.Rows[0].Cells[0].FindControl("isLeadPI");
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
        else if (DropdownMuNonMu.SelectedValue == "N")
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
                        DropDownList AuthorType = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("AuthorType");
                        DropDownList isLeadPI = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("isLeadPI");
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
                            DropdownStudentInstitutionName.Visible = true;
                            DropdownStudentDepartmentName.Visible = true;
                            InstitutionName.Visible = false;
                            DepartmentName.Visible = false;

                            NationalType.Visible = false;
                            ContinentId.Visible = false;

                            EmployeeCodeBtnImg.Enabled = false;

                            dtCurrentTable.Rows[i - 1]["NationalType"] = NationalType.SelectedValue;
                            dtCurrentTable.Rows[i - 1]["ContinentId"] = ContinentId.SelectedValue;
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
                        
                        dtCurrentTable.Rows[i - 1]["AuthorType"] = AuthorType.SelectedValue;
                        dtCurrentTable.Rows[i - 1]["isLeadPI"] = isLeadPI.SelectedValue;
                        if (AuthorType.SelectedValue == "C")
                        {
                            isLeadPI.Enabled = false;
                        }
                        else
                        {
                            isLeadPI.Enabled = true;
                        }
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

                    DropDownList AuthorType = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("AuthorType");
                    DropDownList isLeadPI = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("isLeadPI");
                    DropDownList DropdownStudentInstitutionName = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("DropdownStudentInstitutionName");
                    DropDownList DropdownStudentDepartmentName = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("DropdownStudentDepartmentName");

                    ImageButton EmployeeCodeBtnImg = (ImageButton)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("EmployeeCodeBtn");

                    DropDownList NationalType = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("NationalType");
                    DropDownList ContinentId = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("ContinentId");

                    TextBox AuthorName1 = (TextBox)Grid_AuthorEntry.Rows[0].Cells[1].FindControl("AuthorName");
                    DropDownList DropdownMuNonMu1 = (DropDownList)Grid_AuthorEntry.Rows[0].Cells[2].FindControl("DropdownMuNonMu");
                    ImageButton EmployeeCodeBtnImg1 = (ImageButton)Grid_AuthorEntry.Rows[0].Cells[0].FindControl("EmployeeCodeBtn");

                    TextBox EmployeeCode1 = (TextBox)Grid_AuthorEntry.Rows[0].Cells[0].FindControl("EmployeeCode");
                    HiddenField Institution1 = (HiddenField)Grid_AuthorEntry.Rows[0].Cells[0].FindControl("Institution");
                    TextBox InstitutionName1 = (TextBox)Grid_AuthorEntry.Rows[0].Cells[6].FindControl("InstitutionName");
                    HiddenField Department1 = (HiddenField)Grid_AuthorEntry.Rows[0].Cells[0].FindControl("Department");
                    TextBox DepartmentName1 = (TextBox)Grid_AuthorEntry.Rows[0].Cells[0].FindControl("DepartmentName");
                    TextBox MailId1 = (TextBox)Grid_AuthorEntry.Rows[0].Cells[0].FindControl("MailId");


                    DropDownList AuthorType1 = (DropDownList)Grid_AuthorEntry.Rows[0].Cells[0].FindControl("AuthorType");
                    DropDownList DropdownStudentInstitutionName1 = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("DropdownStudentInstitutionName");
                    DropDownList DropdownStudentDepartmentName1 = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("DropdownStudentDepartmentName");


                    DropdownMuNonMu.Text = dt.Rows[i]["DropdownMuNonMu"].ToString();
                    AuthorName.Text = dt.Rows[i]["AuthorName"].ToString();
                    EmployeeCode.Text = dt.Rows[i]["EmployeeCode"].ToString();
                    if (DropdownMuNonMu.Text == "M")
                    {
                        AuthorName.Enabled = false;
                        InstitutionName.Enabled = false;
                        DepartmentName.Enabled = false;
                        MailId.Enabled = false;
                        EmployeeCode.Enabled = false;
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
                        EmployeeCode.Enabled = false;
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
                        EmployeeCode.Enabled = false;
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
                        ContinentId.Text = dt.Rows[i]["ContinentId"].ToString();

                        EmployeeCodeBtnImg.Enabled = false;

                        //DropdownStudentInstitutionName.Visible = true;
                        //DropdownStudentDepartmentName.Visible = true;
                        //InstitutionName.Visible = false;
                        //DepartmentName.Visible = false;
                        DropdownStudentInstitutionName.Visible = false;
                        DropdownStudentDepartmentName.Visible = false;
                        InstitutionName.Visible = true;
                        DepartmentName.Visible = true;
                        Institution.Value = dt.Rows[i]["Institution"].ToString();
                        InstitutionName.Text = dt.Rows[i]["InstitutionName"].ToString();
                        Department.Value = dt.Rows[i]["Department"].ToString();
                        DepartmentName.Text = dt.Rows[i]["DepartmentName"].ToString();
                        //  Institution.Value = dt.Rows[i]["Institution"].ToString();
                        //DropdownStudentInstitutionName.SelectedValue = dt.Rows[i]["Institution"].ToString();
                        ////  Department.Value = dt.Rows[i]["Department"].ToString();
                        //DropdownStudentDepartmentName.SelectedValue = dt.Rows[i]["Department"].ToString();
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
                    AuthorType.Text = dt.Rows[i]["AuthorType"].ToString();
                    isLeadPI.Text = dt.Rows[i]["isLeadPI"].ToString();
                    if (AuthorType.SelectedValue == "C")
                    {
                        isLeadPI.Enabled = false;
                    }
                    else
                    {
                        isLeadPI.Enabled = true;
                    }
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

    //Textbox author name changed
    protected void AuthorName_Changed(object sender, EventArgs e)
    {
        GridViewRow currentRow = (GridViewRow)((TextBox)sender).Parent.Parent;
        TextBox AuthorName = (TextBox)currentRow.FindControl("AuthorName");
        //  TextBox NameInJournal = (TextBox)currentRow.FindControl("NameInJournal");
        TextBox InstitutionName = (TextBox)currentRow.FindControl("InstitutionName");
        InstitutionName.Focus();
    }



    //On Row delete of autor grdiview
    protected void Grid_AuthorEntry_RowDeleting(Object sender, GridViewDeleteEventArgs e)
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
                    TextBox EmployeeCode = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("EmployeeCode");
                    HiddenField Institution = (HiddenField)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("Institution");
                    TextBox InstitutionName = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[6].FindControl("InstitutionName");
                    HiddenField Department = (HiddenField)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("Department");
                    TextBox DepartmentName = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("DepartmentName");
                    TextBox MailId = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("MailId");

                    //  DropDownList isCorrAuth = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("isCorrAuth");
                    DropDownList AuthorType = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("AuthorType");
                    DropDownList isLeadPI = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("isLeadPI");
                    DropDownList DropdownStudentInstitutionName = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("DropdownStudentInstitutionName");
                    DropDownList DropdownStudentDepartmentName = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("DropdownStudentDepartmentName");

                    DropDownList NationalType = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("NationalType");
                    DropDownList ContinentId = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("ContinentId");

                    ImageButton EmployeeCodeBtnImg1 = (ImageButton)Grid_AuthorEntry.Rows[0].Cells[0].FindControl("EmployeeCodeBtn");

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
                    dtCurrentTable.Rows[i - 1]["AuthorType"] = AuthorType.Text;
                    dtCurrentTable.Rows[i - 1]["isLeadPI"] = isLeadPI.Text;
                    if (AuthorType.SelectedValue == "C")
                    {
                        isLeadPI.Enabled = false;
                    }
                    else
                    {
                        isLeadPI.Enabled = true;
                    }
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
 

    //Drop Down MU/Non MU change
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
        ImageButton ImageButton1 = (ImageButton)currentRow.FindControl("ImageButton1");
        if (DropdownMuNonMu.SelectedValue == "M")
        {
            EmployeeCode.Enabled = false;

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
            MailId.Text = "";
            InstitutionName.Text = "";
            DepartmentName.Text = "";
            ImageButton1.Visible = false;
            EmployeeCodeBtn.Visible = true;
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
            MailId.Text = "";
            InstitutionName.Text = "";
            DepartmentName.Text = "";
            ImageButton1.Visible = false;
            EmployeeCodeBtn.Visible = true;
        }
        else if (DropdownMuNonMu.SelectedValue == "S")
        {
            EmployeeCode.Enabled = false;
            //popupstudent.Visible = true;
            //popupStudentGrid.DataSourceID = "StudentSQLDS";
            //StudentSQLDS.DataBind();
            //popupStudentGrid.DataBind();
            //popupStudentGrid.Visible = true;
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

    protected void ButtonSearchProjectOnClick(object sender, EventArgs e)
    {
        GridViewSearchGrant.Visible = true;
        GridViewSearchGrant.EditIndex = -1;
        dataBind();
        lblmsg.Visible = false;

    }
    private void dataBind()
    {

        GridViewSearchGrant.Visible = true;

        if (EntryTypesearch.SelectedValue != "A")
        {
            SqlDataSource1.SelectParameters.Clear();
            if (PubIDSearch.Text == "" && TextBoxtiltleSearch.Text == "")
            {
                SqlDataSource1.SelectCommand = " select p.ID, r.TypeName,p.ProjectUnit, UTN,Title,Description,CONVERT(DECIMAL(14,0),AppliedAmount) as AppliedAmount,q.StatusName,p.ProjectType from Project p,Status_Project_M q, ProjectType_M r where  p.ProjectType=r.TypeId and p.ProjectStatus=q.StatusId and  ProjectType=@ProjectType and InstitutionID=(Select InstituteId from Publication_InchargerM where UserId=@UserID and Active='Y')   and StatusId!='CAN' and StatusId!='REJ' and StatusId!='CLO' ";
            }
            else if (PubIDSearch.Text != "" && TextBoxtiltleSearch.Text == "")
            {
                SqlDataSource1.SelectCommand = " select p.ID, r.TypeName, p.ProjectUnit,UTN,Title,Description,CONVERT(DECIMAL(14,0),AppliedAmount) as AppliedAmount,q.StatusName,p.ProjectType from Project p,Status_Project_M q, ProjectType_M r where p.ProjectType=r.TypeId and p.ProjectStatus=q.StatusId and ProjectType=@ProjectType and InstitutionID=(Select InstituteId from Publication_InchargerM where UserId=@UserID and Active='Y')  and ID like '%'+@ID+'%'   and StatusId!='CAN' and StatusId!='REJ' and StatusId!='CLO' ";
                SqlDataSource1.SelectParameters.Add("ID", PubIDSearch.Text.Trim());
            }
            else if (PubIDSearch.Text == "" && TextBoxtiltleSearch.Text != "")
            {
                SqlDataSource1.SelectCommand = " select p.ID, r.TypeName,p.ProjectUnit, UTN,Title,Description,CONVERT(DECIMAL(14,0),AppliedAmount) as AppliedAmount,q.StatusName,p.ProjectType from Project p,Status_Project_M q, ProjectType_M r where  p.ProjectType=r.TypeId and p.ProjectStatus=q.StatusId and  ProjectType=@ProjectType and InstitutionID=(Select InstituteId from Publication_InchargerM where UserId=@UserID and Active='Y')   and Title like '%'+@Title+'%' and StatusId!='CAN' and StatusId!='REJ' and StatusId!='CLO' ";
                SqlDataSource1.SelectParameters.Add("Title", TextBoxtiltleSearch.Text.Trim());
            }
            else
            {

                SqlDataSource1.SelectCommand = " select p.ID, r.TypeName, p.ProjectUnit,UTN,Title,Description,CONVERT(DECIMAL(14,0),AppliedAmount) as AppliedAmount,q.StatusName,p.ProjectType from Project p,Status_Project_M q, ProjectType_M r where  p.ProjectType=r.TypeId and p.ProjectStatus=q.StatusId and  ProjectType=@ProjectType and InstitutionID=(Select InstituteId from Publication_InchargerM where UserId=@UserID and Active='Y')  and Title like '%'+@Title+'%' and StatusId!='CAN'  and StatusId!='REJ' and StatusId!='CLO' ";
                // SqlDataSource1.SelectCommand = "select j.Id as Id,Year,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,Comments from Journal_IF_Details j,Journal_M k where j.Id=k.Id and j.Id='" + txtJid.Text.Trim() + "'";
                SqlDataSource1.SelectParameters.Add("Title", TextBoxtiltleSearch.Text.Trim());
             
            }
            SqlDataSource1.SelectParameters.Add("ProjectType", EntryTypesearch.SelectedValue);
            SqlDataSource1.SelectParameters.Add("UserID", Session["UserId"].ToString());
            GridViewSearchGrant.DataBind();
            SqlDataSource1.DataBind();
        }
        else
        {
            SqlDataSource1.SelectParameters.Clear();
            if (PubIDSearch.Text == "" && TextBoxtiltleSearch.Text == "")
            {
                SqlDataSource1.SelectCommand = " select p.ID, r.TypeName,p.ProjectUnit, UTN,Title,Description,CONVERT(DECIMAL(14,0),AppliedAmount) as AppliedAmount,q.StatusName,p.ProjectType from Project p,Status_Project_M q, ProjectType_M r where  p.ProjectType=r.TypeId and p.ProjectStatus=q.StatusId and  InstitutionID=(Select InstituteId from Publication_InchargerM where UserId=@UserID and Active='Y') and  StatusId!='CAN' and StatusId!='REJ' and StatusId!='CLO' ";
            }
            else if (PubIDSearch.Text != "" && TextBoxtiltleSearch.Text == "")
            {
                SqlDataSource1.SelectCommand = " select p.ID, r.TypeName,p.ProjectUnit, UTN,Title,Description,CONVERT(DECIMAL(14,0),AppliedAmount) as AppliedAmount,q.StatusName,p.ProjectType from Project p,Status_Project_M q, ProjectType_M r where  p.ProjectType=r.TypeId and p.ProjectStatus=q.StatusId and  InstitutionID=(Select InstituteId from Publication_InchargerM where UserId=@UserID and Active='Y')  and ID like '%'+@ID+'%'  and StatusId!='CAN' and StatusId!='REJ' and StatusId!='CLO'";
                SqlDataSource1.SelectParameters.Add("ID", PubIDSearch.Text.Trim());
            }
            else if (PubIDSearch.Text == "" && TextBoxtiltleSearch.Text != "")
            {
                SqlDataSource1.SelectCommand = " select p.ID, r.TypeName,p.ProjectUnit, UTN,Title,Description,CONVERT(DECIMAL(14,0),AppliedAmount) as AppliedAmount,q.StatusName,p.ProjectType from Project p,Status_Project_M q, ProjectType_M r where  p.ProjectType=r.TypeId and p.ProjectStatus=q.StatusId and InstitutionID=(Select InstituteId from Publication_InchargerM where UserId=@UserID and Active='Y')  and Title like '%'+@Title+'%' and StatusId!='CAN'  and StatusId!='REJ' and StatusId!='CLO'";
                SqlDataSource1.SelectParameters.Add("Title", TextBoxtiltleSearch.Text.Trim());
            }
            else
            {

                SqlDataSource1.SelectCommand = " select p.ID, r.TypeName,p.ProjectUnit, UTN,Title,Description,CONVERT(DECIMAL(14,0),AppliedAmount) as AppliedAmount,q.StatusName,p.ProjectType from Project p,Status_Project_M q, ProjectType_M r where  p.ProjectType=r.TypeId and p.ProjectStatus=q.StatusId and InstitutionID=(Select InstituteId from Publication_InchargerM where UserId=@UserID and Active='Y')  and ID like '%'+@ID+'%' and Title like '%'+@Title+'%' and StatusId!='CAN' and StatusId!='REJ' and StatusId!='CLO'  ";
                SqlDataSource1.SelectParameters.Add("ID", PubIDSearch.Text.Trim());
                SqlDataSource1.SelectParameters.Add("Title", TextBoxtiltleSearch.Text.Trim());
            }
            SqlDataSource1.SelectParameters.Add("UserID", Session["UserId"].ToString());
            GridViewSearchGrant.DataBind();
            SqlDataSource1.DataBind();
            GridViewSearchGrant.Visible = true;
        }
    }
    
    protected void ButtonSearchSanOnClick(object sender, EventArgs e)
    {
   
            GridViewSearchGrant.Visible = true;
            GridViewSearchGrant.EditIndex = -1;
            dataBind();
         
    }
    

    //Gridview Grant Row Databound
    protected void GridViewSearchGrant_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        ImageButton EditButton = (ImageButton)e.Row.FindControl("BtnEdit");
    }

    //Gridview Grant Page Index changed
    protected void GridViewSearchGrant_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        dataBind();
        GridViewSearchGrant.PageIndex = e.NewPageIndex;
        GridViewSearchGrant.DataBind();

    }



    //Function of edit button Gridview Grant
    public void GridViewSearchGrant_RowCommand(Object sender, GridViewCommandEventArgs e)
    {
        string pid = null;
        string typeEntry = null;
        string Status = null;
        Session["TempPid"] = null;
        Session["TempTypeEntry"] = null;
        Session["TempStatus"] = null;
        Session["ProjectUnit"] = null;
        if (e.CommandName == "Edit")
        {

            GridViewRow rowSelect = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
            int rowindex = rowSelect.RowIndex;
            HiddenField TypeOfEntry = (HiddenField)GridViewSearchGrant.Rows[rowindex].Cells[8].FindControl("hiddenProjectType");
            typeEntry = TypeOfEntry.Value;

            pid = GridViewSearchGrant.Rows[rowindex].Cells[2].Text.Trim().ToString();
            Status = GridViewSearchGrant.Rows[rowindex].Cells[7].Text.Trim().ToString();
            string Unit = GridViewSearchGrant.Rows[rowindex].Cells[1].Text.Trim().ToString();
            Session["TempPid"] = pid;
            Session["TempTypeEntry"] = typeEntry;
            Session["TempStatus"] = Status;
            Session["ProjectUnit"] = Unit;
        }
    }



    public void GridViewSearchGrant_OnRowedit(Object sender, GridViewEditEventArgs e)
    {
        GridViewSearchGrant.EditIndex = e.NewEditIndex;
        fnRecordExist(sender, e);

    }


    private void cleardata()
    {
        //txtprojectactualdate.Text = "";
        TextBoxSanctionedAmountCapital.Text = "";
        TextBoxSanctionedAmountOperating.Text = "";
        TextBoxSanctionedamountTotal.Text = "";
        TextBoxProjectCommencementDate.Text = "";
        TextBoxProjectCloseDate.Text = "";
        TextBoxExtendedDate.Text = "";
        ddlauditrequired.SelectedValue = "0";
        txtInstitutionshare.Text = "";
        txtaccounthead.Text = "";
    }


    //Function to Select  Grant Data
    public void fnRecordExist(object sender, EventArgs e)
    {

        cleardata();
        string Pid = Session["TempPid"].ToString();
        string TypeEntry = Session["TempTypeEntry"].ToString();
        string CurStatus = Session["TempStatus"].ToString();
        string projectunit = Session["ProjectUnit"].ToString();

        GrantData v = new GrantData();
        GrantData v1 = new GrantData();
        GrantData v2 = new GrantData();
        Business obj = new Business();
       
        string commentv = obj.SelectPIMoveComment(Pid, projectunit);
        v = obj.fnfindGrantid(Pid, projectunit);


        TextBoxID.Text = Pid;
        TextBoxUTN.Text = v.UTN;
        DropDownListTypeGrant.SelectedValue = TypeEntry;

        SqlDataSourcePrjStatus.SelectCommand = "Select * from Status_Project_M";
        DropDownListProjStatus.DataSourceID = "SqlDataSourcePrjStatus";
        DropDownListProjStatus.DataBind();
        DropDownListProjStatus.SelectedValue = v.Status;
        txtcontact.Text = v.Contact_No;
        DropDownListGrUnit.SelectedValue = v.GrantUnit;
        DropDownListSourceGrant.SelectedValue = v.GrantSource;


        if (v.AppliedDate.ToShortDateString() != "01/01/0001")
        {
            TextBoxGrantDate.Text = v.AppliedDate.ToShortDateString();
        }
        if (v.GranAmount != 0)
        {
            TextBoxGrantAmt.Text = v.GranAmount.ToString();
        }
        if (v.RevisedAppliedAmt != 0)
        {
            txtRevisedAppliedAmt.Text = v.RevisedAppliedAmt.ToString();
        }
        else
        {
            txtRevisedAppliedAmt.Text = v.GranAmount.ToString();
        }
        if (DropDownListProjStatus.SelectedValue == "SAN")
        {
            lblsanctionorderdate.Visible = true;
            Textsanctionorderdate.Visible = true;
            if (v.SanctionOrderDate.ToShortDateString() != "01/01/0001")
            {
                Textsanctionorderdate.Text = v.SanctionOrderDate.ToShortDateString();
            }

        }
        else
        {
            lblsanctionorderdate.Visible = false;
            Textsanctionorderdate.Visible = false;

        }
        if (v.ERFRelated != "")
        {
            DropDownListerfRelated.SelectedValue = v.ERFRelated;
        }

        TextBoxTitle.Text = v.Title;
        TextBoxAdComments.Text = v.AddtionalComments;
        TextBoxDescription.Text = v.Description;

        hdnAgencyId.Value = v.GrantingAgency;
        string agency = obj.GetAgencyName(v.GrantingAgency);
        txtagency.Text = agency;
        txtAddress.Text = v.Address;
        txtpan.Text = v.Pan_No;
        txtPImovecomment.Text = commentv;
        if (v.State != "")
        {
            txtstate.Text = v.State;
        }
        if (v.Country != "")
        {
            txtcountry.Text = v.Country;
        }

        DropDownAgencyType.SelectedValue = v.TypeofAgencyGrant.ToString();
        DropDownSectorLevel.SelectedValue = v.FundingSectorLevelGrant.ToString();
        txtEmailId.Text = v.AgencyEmailId;
        txtagencycontact.Text = v.AgencyContact;

        DropDownListProjStatus.SelectedValue = v.Status;
        if (v.SancType != "")
        {
            DropDownListSanType.SelectedValue = v.SancType;
        }
        //if (v.ProjectActualDate.ToShortDateString() != "01/01/0001")
        //{
        //    txtprojectactualdate.Text = v.ProjectActualDate.ToShortDateString();
        //}
        if (v.DurationOfProject != 0)
        {
            txtProjectDuration.Text = v.DurationOfProject.ToString();
        }
        DSforgridview.SelectParameters.Clear();
        DSforgridview.SelectParameters.Add("Pid", Pid);
        DSforgridview.SelectParameters.Add("projectunit", projectunit);

        DSforgridview.SelectCommand = "select UploadPDFPath ,q.InfoTypeName as TypeName ,Remark ,AuditFrom,AuditTo, p.CreatedDate, p.CreatedBy as CreatedBy, m.FirstName as AddedBy, EntryNo,Unit_Id ,p.InfoTypeId from ProjectAuxillaryDetails p,Project_AuxInfoTypeM q, User_M m where   p.InfoTypeId=q.InfoTypeId and m.User_Id=p.CreatedBy  and ID=@Pid and ProjectUnit=@projectunit and Deleted='N' order by EntryNo";
        DSforgridview.DataBind();
        GVViewFile.DataBind();
        PanelUploaddetails.Visible = true;
        PanelViewUplodedfiles.Visible = true;



        DataTable dy = obj.fnfindGrantInvestigatorDetails(Pid, projectunit);
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
                DropDownList DropdownMuNonMu = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[0].FindControl("DropdownMuNonMu");
                TextBox EmployeeCode = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[1].FindControl("EmployeeCode");
                ImageButton EmployeeCodeBtnimg = (ImageButton)Grid_AuthorEntry.Rows[rowIndex].Cells[2].FindControl("EmployeeCodeBtn");
                TextBox AuthorName = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[2].FindControl("AuthorName");
                TextBox InstNme = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[3].FindControl("InstitutionName");
                TextBox deptname = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[4].FindControl("DepartmentName");
                DropDownList DropdownStudentInstitutionName = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[3].FindControl("DropdownStudentInstitutionName");
                DropDownList DropdownStudentDepartmentName = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[4].FindControl("DropdownStudentDepartmentName");
                TextBox MailId = (TextBox)Grid_AuthorEntry.Rows[rowIndex].Cells[5].FindControl("MailId");
                DropDownList AuthorType = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[6].FindControl("AuthorType");
                DropDownList isLeadPI = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[2].FindControl("isLeadPI");
                DropDownList NationalType = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[7].FindControl("NationalType");
                DropDownList ContinentId = (DropDownList)Grid_AuthorEntry.Rows[rowIndex].Cells[8].FindControl("ContinentId");
                HiddenField InstId = (HiddenField)Grid_AuthorEntry.Rows[rowIndex].Cells[9].FindControl("Institution");
                HiddenField deptId = (HiddenField)Grid_AuthorEntry.Rows[rowIndex].Cells[10].FindControl("Department");
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
                    ImageButton1.Visible = false;
                    EmployeeCodeBtnimg.Enabled = false;
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
                else if (DropdownMuNonMu.Text == "E")
                {
                    InstNme.Visible = true;
                    deptname.Visible = true;
                    DropdownStudentInstitutionName.Visible = false;
                    DropdownStudentDepartmentName.Visible = false;
                    NationalType.Visible = true;
                    EmployeeCode.Enabled = false;
                    ImageButton1.Visible = false;
                    EmployeeCodeBtnimg.Enabled = false;
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

                    //InstNme.Visible = false;
                    //deptname.Visible = false;
                    //DropdownStudentInstitutionName.Visible = true;
                    //DropdownStudentDepartmentName.Visible = true;

                    //DropdownStudentInstitutionName.SelectedValue = dtCurrentTable.Rows[i - 1]["Institution"].ToString();
                    //DropdownStudentDepartmentName.SelectedValue = dtCurrentTable.Rows[i - 1]["Department"].ToString();
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



                MailId.Text = dtCurrentTable.Rows[i - 1]["MailId"].ToString();
                AuthorType.Text = dtCurrentTable.Rows[i - 1]["AuthorType"].ToString();
                isLeadPI.Text = dtCurrentTable.Rows[i - 1]["isLeadPI"].ToString();

                if (DropdownMuNonMu.Text == "N")
                {

                    AuthorName.Enabled = true;
                    InstNme.Enabled = true;
                    deptname.Enabled = true;
                    MailId.Enabled = true;
                }
                else if (DropdownMuNonMu.Text == "E")
                {

                    AuthorName.Enabled = true;
                    InstNme.Enabled = true;
                    deptname.Enabled = true;
                    MailId.Enabled = true;
                }
                else if (DropdownMuNonMu.Text == "M")
                {
                    AuthorName.Enabled = false;
                    InstNme.Enabled = false;
                    deptname.Enabled = false;
                    MailId.Enabled = false;
                }
                else if (DropdownMuNonMu.Text == "S")
                {
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

                if (AuthorType.Text == "P")
                {
                    isLeadPI.Enabled = true;
                }
                else
                {
                    isLeadPI.Enabled = false;
                }
                rowIndex++;

            }

            ViewState["CurrentTable"] = dtCurrentTable;
        }


        if (v.Status == "APP")
        {
            PanelKindetails.Visible = false;
            GrantSanction.Visible = false;
            PnlBank.Visible = false;
            PanelIncentive.Visible = false;
            PanelOverhead.Visible = false;
            PanelFinanceClosure.Visible = false;
            LabelSanType.Visible = false;
            DropDownListSanType.Visible = false;
            LabelkindDetails.Visible = false;
            TextBoxKindDetails.Visible = false;
            TextKindStartDate.Visible = false;
            TextKindclosedate.Visible = false;
            TextKindStartDate.Visible = false;
            TextKindclosedate.Visible = false;
            kindStartdate.Visible = false;
            KindClosedate.Visible = false;
            PanelOPAmount.Visible = false;
            PanelAmount.Visible = false;
        }
        
        else if (v.Status == "SUB")
        {
            if (v.SancType == "KI")
            {
                PanelKindetails.Visible = true;
                GrantSanction.Visible = false;
                PnlBank.Visible = false;
                PanelIncentive.Visible = false;
                PanelOverhead.Visible = false;
                PanelFinanceClosure.Visible = false;
                LabelSanType.Visible = true;
                DropDownListSanType.Visible = true;
                LabelkindDetails.Visible = true;
                TextBoxKindDetails.Visible = true;
                kindStartdate.Visible = true;
                TextKindStartDate.Visible = true;
                KindClosedate.Visible = true;
                TextKindclosedate.Visible = true;
                PanelOPAmount.Visible = false;
            }
            else if (v.SancType == "CA")
            {
                PanelKindetails.Visible = false;
                GrantSanction.Visible = true;
                PnlBank.Visible = false;
                PanelIncentive.Visible = false;
                PanelOverhead.Visible = false;
                PanelFinanceClosure.Visible = false;
                LabelSanType.Visible = true;
                DropDownListSanType.Visible = true;
                LabelkindDetails.Visible = false;
                TextBoxKindDetails.Visible = false;
                kindStartdate.Visible = false;
                TextKindStartDate.Visible = false;
                KindClosedate.Visible = false;
                TextKindclosedate.Visible = false;
            }

        }
        else if (v.Status == "REW")
        {
            if (v.SancType == "KI")
            {
                PanelKindetails.Visible = true;
                GrantSanction.Visible = false;
                PnlBank.Visible = false;
                PanelIncentive.Visible = false;
                PanelOverhead.Visible = false;
                PanelFinanceClosure.Visible = false;
                LabelSanType.Visible = true;
                DropDownListSanType.Visible = true;
                LabelkindDetails.Visible = true;
                TextBoxKindDetails.Visible = true;
                kindStartdate.Visible = true;
                TextKindStartDate.Visible = true;
                KindClosedate.Visible = true;
                TextKindclosedate.Visible = true;
                PanelOPAmount.Visible = false;
                PanelAmount.Visible = false;
            }
            else if (v.SancType == "CA")
            {
                PanelKindetails.Visible = false;
                GrantSanction.Visible = true;
                PnlBank.Visible = false;
                PanelIncentive.Visible = false;
                PanelOverhead.Visible = false;
                PanelFinanceClosure.Visible = false;
                DropDownListSanType.Visible = true;
                LabelSanType.Visible = true;
                LabelkindDetails.Visible = false;
                TextBoxKindDetails.Visible = false;
                kindStartdate.Visible = false;
                TextKindStartDate.Visible = false;
                KindClosedate.Visible = false;
                TextKindclosedate.Visible = false;
            }
        }
        else if (v.Status == "SAN")
        {
            if (v.SancType == "KI")
            {
                PanelKindetails.Visible = true;
                GrantSanction.Visible = false;
                PnlBank.Visible = false;
                PanelIncentive.Visible = false;
                PanelOverhead.Visible = false;
                PanelFinanceClosure.Visible = false;
                LabelSanType.Visible = true;
                DropDownListSanType.Visible = true;
                LabelkindDetails.Visible = true;
                TextBoxKindDetails.Visible = true;
                kindStartdate.Visible = true;
                TextKindStartDate.Visible = true;
                KindClosedate.Visible = true;
                TextKindclosedate.Visible = true;
                PanelOPAmount.Visible = false;
                PanelAmount.Visible = false;
            }
            else if (v.SancType == "CA")
            {
                PanelKindetails.Visible = false;
                GrantSanction.Visible = true;
                PnlBank.Visible = true;
                PanelIncentive.Visible = true;
                PanelOverhead.Visible = true;
                PanelFinanceClosure.Visible = false;
                LabelSanType.Visible = true;
                DropDownListSanType.Visible = true;
                LabelkindDetails.Visible = false;
                TextBoxKindDetails.Visible = false; ;
                PanelFinanceClosure.Visible = true;
                kindStartdate.Visible = false;
                TextKindStartDate.Visible = false;
                KindClosedate.Visible = false;
                TextKindclosedate.Visible = false;
            }
        }


        if (v.Status == "SUB" || v.Status == "SAN" || v.Status == "REW")
        {
            if (v.SancType == "CA")
            {
                ////Sanction Details


                v1 = obj.fnfindGrantidSanctionDetails(Pid, projectunit);

                if (v1.SanctionCapitalAmount != 0)
                {
                    TextBoxSanctionedAmountCapital.Text = v1.SanctionCapitalAmount.ToString();
                }
                if (v1.SanctionOperatingAmount != 0)
                {
                    TextBoxSanctionedAmountOperating.Text = v1.SanctionOperatingAmount.ToString();
                }
                if (v1.SanctionTotalAmount != 0)
                {
                    TextBoxSanctionedamountTotal.Text = v1.SanctionTotalAmount.ToString();
                }
                txtNoOFSanctions.Text = v1.SanctionEntryNumber.ToString();

                if (v1.ProjectCommencementDate.ToShortDateString() != "01/01/0001")
                {

                    TextBoxProjectCommencementDate.Text = v1.ProjectCommencementDate.ToShortDateString();
                }
                if (v1.ProjectCloseDate.ToShortDateString() != "01/01/0001")
                {
                    TextBoxProjectCloseDate.Text = v1.ProjectCloseDate.ToShortDateString();
                }
                else
                {
                    TextBoxProjectCloseDate.Text = "";
                }
                if (v1.ExtendedDate.ToShortDateString() != "01/01/0001")
                {
                    TextBoxExtendedDate.Text = v1.ExtendedDate.ToShortDateString();
                }
                else
                {
                    TextBoxExtendedDate.Text = "";
                }
                if (v1.AccountHead != null)
                {
                    txtaccounthead.Text = v1.AccountHead.ToString();
                }
                if (v1.AuditRequired != null)
                {
                    ddlauditrequired.SelectedValue = v1.AuditRequired.ToString();
                }
                if (v1.InstitutionSahre != 0.0)
                {
                    txtInstitutionshare.Text = v1.InstitutionSahre.ToString();
                }

                if (v1.DateOfCompletion.ToShortDateString() != "01/01/0001")
                {
                    TextBox3.Text = v.DateOfCompletion.ToShortDateString();
                }

                TextBox4.Text = v.FinanceClosureRemarks;
                if (v.FinanceProjectStatus != "")
                {
                    DropDownList3.SelectedValue = v.FinanceProjectStatus;
                }

                DropDownList3.SelectedValue = v1.FinanceProjectStatus;
                DropDownList2.SelectedValue = v1.ServiceTaxApplicable;

                DataTable Sanctiondata = obj.SelectSanctionData(Pid, projectunit);

                if (Sanctiondata.Rows.Count != 0)
                {
                    ViewState["Sanction"] = Sanctiondata;
                    GridViewSanction.DataSource = Sanctiondata;
                    GridViewSanction.DataBind();
                    GridViewSanction.Visible = true;

                    int rowIndex2 = 0;
                    DataTable table = (DataTable)ViewState["Sanction"];
                    DataRow drCurrentRow2 = null;
                    if (table != null)
                    {
                        for (int i = 1; i <= table.Rows.Count; i++)
                        {
                            TextBox sanctionNo = (TextBox)GridViewSanction.Rows[rowIndex2].Cells[0].FindControl("txtsanctionNo");
                            TextBox Sanctiondate = (TextBox)GridViewSanction.Rows[rowIndex2].Cells[1].FindControl("txtSanctiondate");
                            TextBox santotalAmount = (TextBox)GridViewSanction.Rows[rowIndex2].Cells[5].FindControl("txtsantotalAmount");
                            TextBox sancapitalAmount = (TextBox)GridViewSanction.Rows[rowIndex2].Cells[2].FindControl("txtsancapitalAmount");
                            TextBox SanOpeAmt = (TextBox)GridViewSanction.Rows[rowIndex2].Cells[3].FindControl("txtSanOpeAmt");
                            TextBox Narration = (TextBox)GridViewSanction.Rows[rowIndex2].Cells[6].FindControl("txtNarration");

                            drCurrentRow2 = table.NewRow();
                            sanctionNo.Text = table.Rows[i - 1]["SanctionNumber"].ToString();
                            if (table.Rows[i - 1]["SanctionDate"].ToString() != "")
                            {
                                DateTime date = Convert.ToDateTime(table.Rows[i - 1]["SanctionDate"].ToString());
                                Sanctiondate.Text = date.ToShortDateString();
                            }

                            if (table.Rows[i - 1]["SanctionTotalAmount"].ToString() != "")
                            {
                                double totamt = Convert.ToDouble((decimal)(table.Rows[i - 1]["SanctionTotalAmount"]));
                                santotalAmount.Text = totamt.ToString();
                            }

                            if (table.Rows[i - 1]["SanctionCapitalAmount"].ToString() != "")
                            {
                                double capamt = Convert.ToDouble((decimal)(table.Rows[i - 1]["SanctionCapitalAmount"]));
                                sancapitalAmount.Text = capamt.ToString();
                            }

                            if (table.Rows[i - 1]["SanctionOperatingAmount"].ToString() != "")
                            {
                                double opamt = Convert.ToDouble((decimal)(table.Rows[i - 1]["SanctionOperatingAmount"]));
                                SanOpeAmt.Text = opamt.ToString();
                            }
                            Narration.Text = table.Rows[i - 1]["Narration"].ToString();
                            rowIndex2++;

                        }

                        ViewState["Sanction"] = table;
                    }
                    //setModalWindowOPAmount(sender, e);
                }
                else
                {
                    SanctionSetInitialRow();
                }

                DataTable dtCurrentTabledata = (DataTable)ViewState["Sanction"];
                if (dtCurrentTabledata != null)
                {
                    if (dtCurrentTabledata.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtCurrentTabledata.Rows.Count; i++)
                        {
                            Session["MiscRow" + i] = null;
                        }
                    }
                }


                Business b1 = new Business();
                DataTable tablevalue = b1.SelectSanctionOPAmountDetailsExists(Pid, projectunit);
                if (tablevalue.Rows.Count != 0)
                {
                    DataTable data1 = null;

                    if (dtCurrentTabledata.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtCurrentTabledata.Rows.Count; i++)
                        {
                            data1 = b1.SelectSanctionOPAmountDetails1(Pid, projectunit, i + 1);
                            if (data1 != null)
                            {
                                Session["MiscRow" + i] = data1;
                            }
                        }
                    }
                }

                if (v.Status == "SAN")
                {
                    DataTable FundRecevieData = obj.SelectRecipetDetails(Pid, projectunit);

                    if (FundRecevieData.Rows.Count != 0)
                    {
                        ViewState["Bank"] = FundRecevieData;
                        GridView_bank.DataSource = FundRecevieData;
                        GridView_bank.DataBind();
                        GridView_bank.Visible = true;

                        int rowIndex1 = 0;

                        if (ViewState["Bank"] != null)
                        {
                            DataTable dtCurrentTable1 = (DataTable)ViewState["Bank"];
                            DataRow drCurrentRowRec = null;
                            if (dtCurrentTable1.Rows.Count > 0)
                            {
                                for (int i = 1; i <= dtCurrentTable1.Rows.Count; i++)
                                {
                                    DropDownList SanctionEntryNumber = (DropDownList)GridView_bank.Rows[rowIndex1].Cells[0].FindControl("ddlSanctionEntryNo");
                                    DropDownList CurrencyCode = (DropDownList)GridView_bank.Rows[rowIndex1].Cells[1].FindControl("CurrencyCode");
                                    DropDownList ModeOfRecevie = (DropDownList)GridView_bank.Rows[rowIndex1].Cells[2].FindControl("ModeOfRecevie");
                                    TextBox ReceviedDate = (TextBox)GridView_bank.Rows[rowIndex1].Cells[3].FindControl("ReceviedDate");
                                    TextBox ReceviedAmount = (TextBox)GridView_bank.Rows[rowIndex1].Cells[4].FindControl("ReceviedAmount");
                                    TextBox ReceviedINR = (TextBox)GridView_bank.Rows[rowIndex1].Cells[5].FindControl("ReceviedINR");
                                    TextBox TDS = (TextBox)GridView_bank.Rows[rowIndex1].Cells[6].FindControl("TDS");
                                    TextBox ReferenceNo = (TextBox)GridView_bank.Rows[rowIndex1].Cells[7].FindControl("ReferenceNo");
                                    TextBox ReceivedBank = (TextBox)GridView_bank.Rows[rowIndex1].Cells[9].FindControl("ReceivedBankId");
                                    TextBox ReceivedBankName = (TextBox)GridView_bank.Rows[rowIndex1].Cells[9].FindControl("Receivedbank");
                                    TextBox CreditedBank = (TextBox)GridView_bank.Rows[rowIndex1].Cells[10].FindControl("CreditedBankId");
                                    TextBox CreditedBankName = (TextBox)GridView_bank.Rows[rowIndex1].Cells[10].FindControl("CreditedBank");
                                    TextBox ReceivedNarration = (TextBox)GridView_bank.Rows[rowIndex1].Cells[11].FindControl("ReceivedNarration");


                                    drCurrentRow = dtCurrentTable1.NewRow();

                                    if (Session["ProjectUnit"].ToString() == "MUIND")
                                    {
                                        GridView_bank.Columns[5].Visible = false;
                                        CurrencyCode.Enabled = false;
                                    }
                                    else
                                    {

                                        CurrencyCode.Items.Remove(CurrencyCode.Items.FindByValue("INR"));
                                    }

                                    SanctionEntryNumber.SelectedValue = dtCurrentTable1.Rows[i - 1]["SanctionEntryNo"].ToString();
                                    CurrencyCode.Text = dtCurrentTable1.Rows[i - 1]["CurrencyCode"].ToString();
                                    ModeOfRecevie.Text = dtCurrentTable1.Rows[i - 1]["ModeOfReceive"].ToString();
                                    if (dtCurrentTable1.Rows[i - 1]["ReceviedDate"].ToString() != "")
                                    {
                                        DateTime date = Convert.ToDateTime(dtCurrentTable1.Rows[i - 1]["ReceviedDate"]);
                                        ReceviedDate.Text = date.ToShortDateString();
                                    }
                                    ReceviedAmount.Text = dtCurrentTable1.Rows[i - 1]["ReceviedAmount"].ToString();
                                    if (dtCurrentTable1.Rows[i - 1]["ReceviedINR"].ToString() != "")
                                    {
                                        double amount = Convert.ToDouble((decimal)dtCurrentTable1.Rows[i - 1]["ReceviedINR"]);
                                        ReceviedINR.Text = amount.ToString();
                                    }
                                    TDS.Text = dtCurrentTable1.Rows[i - 1]["TDS"].ToString();
                                    ReferenceNo.Text = dtCurrentTable1.Rows[i - 1]["ReferenceNumber"].ToString();
                                    CreditedBankName.Text = dtCurrentTable1.Rows[i - 1]["CreditedBankName"].ToString();
                                    ReceivedNarration.Text = dtCurrentTable1.Rows[i - 1]["ReceivedNarration"].ToString();

                                    ReceivedBank.Text = dtCurrentTable1.Rows[i - 1]["ReceivedBank"].ToString();
                                    CreditedBank.Text = dtCurrentTable1.Rows[i - 1]["CreditedBank"].ToString();
                                    //BankId.Text = dtCurrentTable1.Rows[i - 1]["BankId"].ToString();

                                    if (Session["ProjectUnit"].ToString() == "MUIND")
                                    {

                                        CurrencyCode.SelectedValue = "INR";
                                        GridView_bank.Columns[5].Visible = false;
                                    }

                                    rowIndex1++;
                                }

                                ViewState["Bank"] = dtCurrentTable1;
                            }
                        }
                    }
                    else
                    {
                        SetInitialRowBank();
                    }

                    //Incentive details

                    DataTable dy33 = obj.SelectIncentiveDetails(Pid, projectunit);
                    if (dy33.Rows.Count != 0)
                    {
                        ViewState["IncentiveDetails"] = dy33;
                        gvIncentiveDetails.DataSource = dy33;
                        gvIncentiveDetails.DataBind();
                        gvIncentiveDetails.Visible = true;

                        int rowIndex2 = 0;

                        DataTable table = (DataTable)ViewState["IncentiveDetails"];
                        DataRow drCurrentRow2 = null;
                        if (table.Rows.Count > 0)
                        {
                            for (int i = 1; i <= table.Rows.Count; i++)
                            {
                                DropDownList SanctionEntryNumber = (DropDownList)gvIncentiveDetails.Rows[rowIndex2].Cells[0].FindControl("ddlSanctionEntryNo");
                                TextBox txtincentivedate = (TextBox)gvIncentiveDetails.Rows[rowIndex2].Cells[1].FindControl("txtincentivedate");
                                TextBox txtincentiveAmount = (TextBox)gvIncentiveDetails.Rows[rowIndex2].Cells[2].FindControl("txtincentiveAmount");
                                TextBox txtComments = (TextBox)gvIncentiveDetails.Rows[rowIndex2].Cells[3].FindControl("txtComments");

                                drCurrentRow2 = table.NewRow();
                                DateTime date = Convert.ToDateTime(table.Rows[i - 1]["IncentivePayDate"].ToString());
                                SanctionEntryNumber.SelectedValue = table.Rows[i - 1]["SanctionEntryNo"].ToString();
                                txtincentivedate.Text = date.ToShortDateString();
                                double amount = Convert.ToDouble((decimal)(table.Rows[i - 1]["IncentivePayAmount"]));
                                txtincentiveAmount.Text = amount.ToString();
                                txtComments.Text = table.Rows[i - 1]["Narration"].ToString();

                                rowIndex2++;

                            }


                            ViewState["IncentiveDetails"] = table;

                        }
                       // setModalWindowAmount(sender, e);
                    }
                    else
                    {
                        SetIntialRowIncentive();
                    }


                    DataTable dt = new DataTable();
                    dt = (DataTable)ViewState["IncentiveDetails"];

                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                Session["MiscRowIncentive" + i] = null;
                            }
                        }
                    }


                    DataTable tablevalue1 = b1.SelectIncentiveAmountDetailsExists(Pid, projectunit);
                    if (tablevalue1.Rows.Count != 0)
                    {
                        DataTable data1 = null;

                        if (dy33.Rows.Count > 0)
                        {
                            for (int i = 0; i < dy33.Rows.Count; i++)
                            {
                                data1 = b1.SelectIncentiveAmountDetails1(Pid, projectunit, i + 1);

                                if (data1 != null)
                                {
                                    Session["MiscRowIncentive" + i] = data1;
                                }
                            }
                        }
                    }

                    DataTable dy4 = obj.SelectOverheadDetails(Pid, projectunit);
                    if (dy4.Rows.Count != 0)
                    {
                        ViewState["OverheadT"] = dy4;
                        grvoverhead.DataSource = dy4;
                        grvoverhead.DataBind();
                        grvoverhead.Visible = true;

                        int rowIndex3 = 0;

                        DataTable table = (DataTable)ViewState["OverheadT"];
                        DataRow drCurrentRow3 = null;
                        if (table.Rows.Count > 0)
                        {
                            for (int i = 1; i <= table.Rows.Count; i++)
                            {

                                DropDownList SanctionEntryNumber = (DropDownList)grvoverhead.Rows[rowIndex3].Cells[0].FindControl("ddlSanctionEntryNoOH");
                                TextBox txtOverheaddate = (TextBox)grvoverhead.Rows[rowIndex3].Cells[1].FindControl("txtOverheaddate");
                                TextBox txtOverheadAmount = (TextBox)grvoverhead.Rows[rowIndex3].Cells[2].FindControl("txtOverheadAmount");
                                TextBox txtoverheadComments = (TextBox)grvoverhead.Rows[rowIndex3].Cells[4].FindControl("txtoverheadComments");
                                TextBox txtJVNumber = (TextBox)grvoverhead.Rows[rowIndex3].Cells[3].FindControl("txtJVNumber");


                                drCurrentRow3 = table.NewRow();
                                DateTime date = Convert.ToDateTime(table.Rows[i - 1]["OverheadTDate"].ToString());
                                txtOverheaddate.Text = date.ToShortDateString();

                                double amount = Convert.ToDouble((decimal)(table.Rows[i - 1]["OverheadTAmount"]));
                                txtOverheadAmount.Text = amount.ToString();
                                txtJVNumber.Text = table.Rows[i - 1]["JVNumber"].ToString();
                                txtoverheadComments.Text = table.Rows[i - 1]["Narration"].ToString();
                                SanctionEntryNumber.SelectedValue = table.Rows[i - 1]["SanctionEntryNo"].ToString();
                                rowIndex3++;

                            }


                            ViewState["OverheadT"] = table;
                        }
                    }
                    else
                    {
                        SetInitialRowOverhead();
                    }

                }
            }
            else if (v.SancType == "KI")
            {

                PanelKindetails.Visible = true;

                LabelSanType.Visible = true;
                DropDownListSanType.Visible = true;
                LabelkindDetails.Visible = true;
                TextBoxKindDetails.Visible = true;
                TextBoxKindDetails.Text = v.KindComments;
                if (v.KindStartDate.ToShortDateString() != "01/01/0001")
                {
                    TextKindStartDate.Text = v.KindStartDate.ToShortDateString();
                }
                if (v.KindCloseDate.ToShortDateString() != "01/01/0001")
                {
                    TextKindclosedate.Text = v.KindCloseDate.ToShortDateString();
                }

                DataTable dy1 = obj.fnfindGrantSanKindDetails(Pid, projectunit);
                ViewState["SancKindCurrentTable"] = dy1;
                GridViewkindDetails.DataSource = dy1;
                GridViewkindDetails.DataBind();
                GridViewkindDetails.Visible = true;

                int rowIndex123 = 0;

                DataTable dtCurrentTable1 = (DataTable)ViewState["SancKindCurrentTable"];
                DataRow drCurrentRow1 = null;
                if (dtCurrentTable1.Rows.Count > 0)
                {
                    for (int i = 1; i <= dtCurrentTable1.Rows.Count; i++)
                    {
                        TextBox ReceivedDate = (TextBox)GridViewkindDetails.Rows[rowIndex123].Cells[0].FindControl("ReceivedDate");
                        TextBox INREquivalent = (TextBox)GridViewkindDetails.Rows[rowIndex123].Cells[1].FindControl("INREquivalent");
                        TextBox Details = (TextBox)GridViewkindDetails.Rows[rowIndex123].Cells[2].FindControl("Details");

                        drCurrentRow1 = dtCurrentTable1.NewRow();


                        ReceivedDate.Text = dtCurrentTable1.Rows[i - 1]["ReceivedDate"].ToString();
                        INREquivalent.Text = dtCurrentTable1.Rows[i - 1]["INREquivalent"].ToString();
                        Details.Text = dtCurrentTable1.Rows[i - 1]["Details"].ToString();

                        rowIndex123++;

                    }


                    ViewState["SancKindCurrentTable"] = dtCurrentTable1;
                }

            }
        }

    }
    //Initialize row for Bank details
    private void SetInitialRowBank()
    {

        DataTable dt1 = new DataTable();
        DataRow dr1 = null;
        dt1.Columns.Add(new DataColumn("SanctionEntryNo", typeof(string)));
        dt1.Columns.Add(new DataColumn("CurrencyCode", typeof(string)));
        dt1.Columns.Add(new DataColumn("ModeOfReceive", typeof(string)));

        dt1.Columns.Add(new DataColumn("ReceviedDate", typeof(string)));
        dt1.Columns.Add(new DataColumn("ReceviedAmount", typeof(string)));
        dt1.Columns.Add(new DataColumn("ReceviedINR", typeof(string)));

        dt1.Columns.Add(new DataColumn("TDS", typeof(string)));
        dt1.Columns.Add(new DataColumn("ReferenceNumber", typeof(string)));
        dt1.Columns.Add(new DataColumn("BankID", typeof(string)));
        dt1.Columns.Add(new DataColumn("BankName", typeof(string)));
        dt1.Columns.Add(new DataColumn("ReceiedBankBName", typeof(string)));
        dt1.Columns.Add(new DataColumn("ReceivedBank", typeof(string)));
        dt1.Columns.Add(new DataColumn("CreditedBankName", typeof(string)));
        dt1.Columns.Add(new DataColumn("CreditedBank", typeof(string)));

        dt1.Columns.Add(new DataColumn("ReceivedNarration", typeof(string)));

        dr1 = dt1.NewRow();
        dr1["SanctionEntryNo"] = string.Empty;
        dr1["CurrencyCode"] = string.Empty;
        dr1["ModeOfReceive"] = string.Empty;
        dr1["ReceviedDate"] = string.Empty;
        dr1["ReceviedAmount"] = string.Empty;
        dr1["ReceviedINR"] = string.Empty;
        dr1["TDS"] = string.Empty;
        dr1["ReferenceNumber"] = string.Empty;
        dr1["BankId"] = string.Empty;
        dr1["BankName"] = string.Empty;

        dr1["CreditedBankName"] = string.Empty;
        dr1["CreditedBank"] = string.Empty;
        dr1["ReceiedBankBName"] = string.Empty;
        dr1["ReceivedBank"] = string.Empty;
        dr1["ReceivedNarration"] = string.Empty;

        dt1.Rows.Add(dr1);

        ViewState["Bank"] = dt1;
        GridView_bank.DataSource = dt1;
        GridView_bank.DataBind();

        if (Session["ProjectUnit"].ToString() == "MUIND")
        {
            DropDownList CurrencyCode = (DropDownList)GridView_bank.Rows[0].Cells[1].FindControl("CurrencyCode");
            CurrencyCode.SelectedValue = "INR";
            GridView_bank.Columns[5].Visible = false;
            CurrencyCode.Enabled = false;
        }
        else
        {
            DropDownList CurrencyCode = (DropDownList)GridView_bank.Rows[0].Cells[1].FindControl("CurrencyCode");
            CurrencyCode.Items.Remove(CurrencyCode.Items.FindByValue("INR"));
        }


    }


    

    //Function to view uploaded files
    protected void GVViewFile_SelectedIndexChanged(object sender, EventArgs e)
    {
        string BoxId = TextBoxID.Text.Trim();
        string PublicationEntry1 = DropDownListTypeGrant.SelectedValue;
        string servername = ConfigurationManager.AppSettings["ServerName"].ToString();
        string domainame = ConfigurationManager.AppSettings["DomainName"].ToString();
        string username = ConfigurationManager.AppSettings["UserName"].ToString();
        string password = ConfigurationManager.AppSettings["Password"].ToString();
        string folderpath;
        string path_BoxId;
        using (NetworkShareAccesser.Access(servername, domainame, username, password))
        {
            folderpath = ConfigurationManager.AppSettings["FolderPathProject"].ToString();
            path_BoxId = Path.Combine(folderpath, BoxId);
            string path_BoxId_ProType = Path.Combine(path_BoxId, PublicationEntry1);


            int id = GVViewFile.SelectedIndex;
            Label filepath = (Label)GVViewFile.Rows[id].FindControl("lblgetid");
            string path = filepath.Text;       //actual filelocation path  
            string newpath = path.Replace('\\', '/');  // replacing '\' by '/' to view image or pdf

            Response.Write("<script>");
            Response.Write("window.open('DisplayPdf.aspx?val=" + newpath + "','_blank')");
            //path sent to display.aspx page
            Response.Write("</script>");
        }
    }


    //Function Delete uploaded files

    protected void GVViewFile_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        ImageButton EditButton = (ImageButton)e.Row.FindControl("BtnEdit");
    }



    private void SetInitialSancKindRow()
    {
        DataTable dt = new DataTable();
        DataRow dr = null;
        dt.Columns.Add(new DataColumn("ReceivedDate", typeof(string)));
        dt.Columns.Add(new DataColumn("INREquivalent", typeof(string)));
        dt.Columns.Add(new DataColumn("Details", typeof(string)));

        dr = dt.NewRow();

        dr["ReceivedDate"] = string.Empty;
        dr["INREquivalent"] = string.Empty;
        dr["Details"] = string.Empty;

        dt.Rows.Add(dr);

        ViewState["SancKindCurrentTable"] = dt;
        GridViewkindDetails.DataSource = dt;
        GridViewkindDetails.DataBind();

        TextBox ReceivedDate = (TextBox)Grid_AuthorEntry.Rows[0].Cells[1].FindControl("ReceivedDate");
        TextBox INREquivalent = (TextBox)Grid_AuthorEntry.Rows[0].Cells[6].FindControl("INREquivalent");
        TextBox Details = (TextBox)Grid_AuthorEntry.Rows[0].Cells[0].FindControl("Details");

    }




    //Sanction Details

    //Initialize row for sanction details
    private void SanctionSetInitialRow()
    {

        DataTable dt1 = new DataTable();
        DataRow dr1 = null;

        dt1.Columns.Add(new DataColumn("SanctionNumber", typeof(string)));
        dt1.Columns.Add(new DataColumn("SanctionDate", typeof(string)));
        dt1.Columns.Add(new DataColumn("SanctionCapitalAmount", typeof(string)));
        dt1.Columns.Add(new DataColumn("SanctionOperatingAmount", typeof(string)));
        dt1.Columns.Add(new DataColumn("SanctionTotalAmount", typeof(string)));
        dt1.Columns.Add(new DataColumn("Narration", typeof(string)));
        dr1 = dt1.NewRow();


        dr1["SanctionNumber"] = string.Empty;
        dr1["SanctionDate"] = string.Empty;
        dr1["SanctionCapitalAmount"] = string.Empty;
        dr1["SanctionOperatingAmount"] = string.Empty;
        dr1["SanctionTotalAmount"] = string.Empty;
        dr1["Narration"] = string.Empty;
        dt1.Rows.Add(dr1);

        ViewState["Sanction"] = dt1;
        GridViewSanction.DataSource = dt1;
        GridViewSanction.DataBind();


    }



    //Incentive 

    //incentive Details
    protected void SetIntialRowIncentive()
    {

        DataTable dt = new DataTable();
        dt.Columns.Add("SanctionEntryNo", typeof(string));
        dt.Columns.Add("IncentivePayDate", typeof(string));
        dt.Columns.Add("IncentivePayAmount", typeof(string));
        dt.Columns.Add("Narration", typeof(string));
        DataRow dr = dt.NewRow();
        dr["SanctionEntryNo"] = string.Empty;
        dr["IncentivePayDate"] = string.Empty;
        dr["IncentivePayAmount"] = string.Empty;
        dr["Narration"] = string.Empty;
        dt.Rows.Add(dr);
        ViewState["IncentiveDetails"] = dt;
        gvIncentiveDetails.DataSource = dt;
        gvIncentiveDetails.DataBind();

    }




    //OverHead
    private void SetInitialRowOverhead()
    {
        DataTable dt1 = new DataTable();
        DataRow dr1 = null;
        // dt1.Columns.Add("Line", typeof(int));
        dt1.Columns.Add(new DataColumn("SanctionEntryNo", typeof(string)));
        dt1.Columns.Add(new DataColumn("OverheadTDate", typeof(string)));
        dt1.Columns.Add(new DataColumn("OverheadTAmount", typeof(string)));
        dt1.Columns.Add(new DataColumn("JVNumber", typeof(string)));
        dt1.Columns.Add(new DataColumn("Narration", typeof(string)));

        dr1 = dt1.NewRow();
        //dr1["Line"] = 1;
        dr1["SanctionEntryNo"] = string.Empty;
        dr1["OverheadTDate"] = string.Empty;
        dr1["OverheadTAmount"] = string.Empty;
        dr1["JVNumber"] = string.Empty;
        dr1["Narration"] = string.Empty;

        dt1.Rows.Add(dr1);

        ViewState["OverheadT"] = dt1;
        grvoverhead.DataSource = dt1;
        grvoverhead.DataBind();
    }





    //PopUp Amount
    protected void AddAmtClick(object sender, EventArgs e)
    {
        Button imgButton = (Button)sender;
        GridViewRow parentRow = (GridViewRow)imgButton.NamingContainer;
        int rowindex = parentRow.RowIndex;

        rowLabel.Text = rowindex.ToString();

        DropDownList sanctionnumber = (DropDownList)gvIncentiveDetails.Rows[rowindex].Cells[0].FindControl("ddlSanctionEntryNo");
        int sanctionentryno = Convert.ToInt16(sanctionnumber.SelectedValue);
        Sanction.Text = sanctionentryno.ToString();

        Business b = new Business();
        string id = TextBoxID.Text;
        string unit = DropDownListGrUnit.SelectedValue;
        DataTable dy = b.SelectIncentiveAmountDetailsExists(id, unit);

        int value = rowindex + 1;
        if (dy.Rows.Count != 0)
        {
            //SqlDataSourceAddAmt.SelectCommand = "Select ''  AS Row1, [LineNo] as indexv,EntryNo as Row,SanctionEntryNo,'' as rowIndexParent,'' as rowIndexChild,ProjectUnit as ProjectUnit, ID as ID,PayedTo as InvestigatorName ,Amount,InstitutionId as Institution  from ProjectIncentivePayDetails a where a.ID='" + id + "' and a.ProjectUnit='MUFOR' and a.[LineNo]=" + value + " UNION Select ROW_NUMBER() OVER (ORDER BY Projectnvestigator.[ID]) AS Row1,'',EntryNo as Row,'','','',ProjectUnit as ProjectUnit , ID as ID,InvestigatorName as InvestigatorName,'',Institution as Institution  from Projectnvestigator  where InvestigatorName not in  (Select PayedTo from ProjectIncentivePayDetails where ProjectIncentivePayDetails.ID='" + id + "' and ProjectIncentivePayDetails.ProjectUnit='" + unit + "') and ID='" + id + "' and ProjectUnit='" + unit + "'";
            SqlDataSourceAddAmt.SelectParameters.Clear();
            SqlDataSourceAddAmt.SelectParameters.Add("value", value.ToString());
            SqlDataSourceAddAmt.SelectParameters.Add("unit", unit);
            SqlDataSourceAddAmt.SelectParameters.Add("id", id);
            SqlDataSourceAddAmt.SelectCommand = " Select a.ProjectUnit,a.ID,a.InvestigatorType, a.EntryNo as Row,InvestigatorName,a.Institution as Institution,a.Department as Department,Amount from Projectnvestigator a left outer join ProjectIncentivePayDetails b on a.ProjectUnit=b.ProjectUnit and a.ID=b.ID and a.EntryNo=b.EntryNo and  b.[LineNo]=@value and a.ProjectUnit=@unit and a.ID=@id where a.ProjectUnit=@unit and a.ID=@id";
            PanelAmount.Visible = true;
            popGridViewAmount.DataSourceID = "SqlDataSourceAddAmt";
            SqlDataSourceAddAmt.DataBind();
            popGridViewAmount.DataBind();

            int rowMisc = 0;
            for (int i = 1; i <= popGridViewAmount.Rows.Count; i++)
            {
                TextBox TextPopupMisc = (TextBox)popGridViewAmount.Rows[rowMisc].Cells[4].FindControl("txtAmount");
                if (TextPopupMisc.Text == "0")
                {
                    TextPopupMisc.Text = "";
                }
                else if (TextPopupMisc.Text != "")
                {
                    TextPopupMisc.Text = ((decimal)(Convert.ToDouble(TextPopupMisc.Text))).ToString();
                }
                rowMisc++;
            }

            ModalPopupExtender ModalPopupExtenderMisc = (ModalPopupExtender)gvIncentiveDetails.Rows[rowindex].FindControl("ModalPopupExtenderAmount");
            ModalPopupExtenderMisc.Show();
        }

        else
        {
            setModalWindowAmount(sender, e);
            ModalPopupExtender ModalPopupExtenderMisc = (ModalPopupExtender)gvIncentiveDetails.Rows[0].FindControl("ModalPopupExtenderAmount");
            ModalPopupExtenderMisc.Show();
        }

    }


    private void setModalWindowAmount(object sender, EventArgs e)
    {
        PanelAmount.Visible = true;
        popGridViewAmount.DataSourceID = "SqlDataSourceAddAmt";
        SqlDataSourceAddAmt.DataBind();
        popGridViewAmount.DataBind();
        popGridViewAmount.Visible = true;

    }




    protected void AddOPAmtClick(object sender, EventArgs e)
    {
        Button imgButton = (Button)sender;
        GridViewRow parentRow = (GridViewRow)imgButton.NamingContainer;
        int rowindex = parentRow.RowIndex;

        Label11.Text = rowindex.ToString();

        //DropDownList sanctionnumber = (DropDownList)gvIncentiveDetails.Rows[rowindex].Cells[0].FindControl("ddlSanctionEntryNo");
        //int sanctionentryno = Convert.ToInt16(sanctionnumber.SelectedValue);
        //Sanction.Text = sanctionentryno.ToString();

        Business b = new Business();
        string id = TextBoxID.Text;
        string unit = DropDownListGrUnit.SelectedValue;
        DataTable dy = b.SelectSanctionOPAmountDetails(id, unit, rowindex + 1);
        int value = rowindex + 1;
        if (dy.Rows.Count != 0)
        {
            setModalWindowOPAmount(sender, e);
            if (DropDownListProjStatus.SelectedValue == "SAN")
            {
                setModalWindowAmount(sender, e);
            }
            SqlDataSource5.SelectParameters.Clear();
            SqlDataSource5.SelectCommand = "select ROW_NUMBER() OVER (ORDER BY a.[ID]) AS Row, a.ID,Name ,b.SanctionEntryNo,b.OperatingItemId,b.Amount as Amount,'' as rowIndexParent,'' as rowIndexChild from OperatingItem_M a left outer join SanctionOPAmountDetails b  on a.ID=b.OperatingItemId and b.SanctionEntryNo=@value and b.ProjectUnit=@unit and b.ID=@id";
            SqlDataSource5.SelectParameters.Add("unit", unit);
            SqlDataSource5.SelectParameters.Add("id", id);
            SqlDataSource5.SelectParameters.Add("value", value.ToString());
            PanelOPAmount.Visible = true;
            popgridOPAmount.DataSourceID = "SqlDataSource5";
            SqlDataSource5.DataBind();
            popgridOPAmount.DataBind();
            popgridOPAmount.Visible = true;

            int rowMisc = 0;
            for (int i = 1; i <= popgridOPAmount.Rows.Count; i++)
            {
                TextBox TextPopupMisc = (TextBox)popgridOPAmount.Rows[rowMisc].Cells[3].FindControl("txtOPAmount");
                if (TextPopupMisc.Text == "0")
                {
                    TextPopupMisc.Text = "";
                }
                else if (TextPopupMisc.Text != "")
                {
                    TextPopupMisc.Text = ((decimal)(Convert.ToDouble(TextPopupMisc.Text))).ToString();
                }
                rowMisc++;
            }
            ModalPopupExtender ModalPopupExtenderMisc = (ModalPopupExtender)GridViewSanction.Rows[rowindex].FindControl("ModalPopupExtenderOPAmount");
            ModalPopupExtenderMisc.Show();
        }

        else
        {
            setModalWindowOPAmount(sender, e);

            ModalPopupExtender ModalPopupExtenderMisc = (ModalPopupExtender)GridViewSanction.Rows[rowindex].FindControl("ModalPopupExtenderOPAmount");
            ModalPopupExtenderMisc.Show();
        }
        //ModalPopupExtender ModalPopupExtenderMisc2 = (ModalPopupExtender)gvIncentiveDetails.Rows[0].FindControl("ModalPopupExtenderAmount");
        //ModalPopupExtenderMisc2.Hide();
        //ModalPopupExtender ModalPopupExtenderMisc = (ModalPopupExtender)gvIncentiveDetails.Rows[0].FindControl("ModalPopupExtenderAmount");
        //ModalPopupExtenderMisc.Show();

    }

    private void setModalWindowOPAmount(object sender, EventArgs e)
    {
        PanelOPAmount.Visible = true;
        popgridOPAmount.DataSourceID = "SqlDataSource5";
        SqlDataSource5.DataBind();
        popgridOPAmount.DataBind();
        popgridOPAmount.Visible = true;

    }


    protected void IncentiveOnRowDataBound(Object sender, GridViewRowEventArgs e)
    {
        if (txtNoOFSanctions.Text != "")
        {
            int number = Convert.ToInt16(txtNoOFSanctions.Text);
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Find the drop-down (say in 3rd column)
                var dd = e.Row.Cells[1].Controls[0] as DropDownList;
                DropDownList ddl = (DropDownList)e.Row.FindControl("ddlSanctionEntryNo");
                for (int i = 1; i <= number; i++)
                {
                    string value = i.ToString();
                    ddl.Items.Add(new ListItem(value));
                }
            }
        }

    }

    protected void RowDataBoundBank(Object sender, GridViewRowEventArgs e)
    {
        if (txtNoOFSanctions.Text != "")
        {
            int number = Convert.ToInt16(txtNoOFSanctions.Text);
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Find the drop-down (say in 3rd column)
                var dd = e.Row.Cells[1].Controls[0] as DropDownList;
                DropDownList ddl = (DropDownList)e.Row.FindControl("ddlSanctionEntryNo");
                for (int i = 1; i <= number; i++)
                {
                    string value = i.ToString();
                    ddl.Items.Add(new ListItem(value));
                }
            }
        }

    }

    protected void RowDataBoundOverhead(Object sender, GridViewRowEventArgs e)
    {
        if (txtNoOFSanctions.Text != "")
        {
            int number = Convert.ToInt16(txtNoOFSanctions.Text);
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Find the drop-down (say in 3rd column)
                var dd = e.Row.Cells[1].Controls[0] as DropDownList;
                DropDownList ddl = (DropDownList)e.Row.FindControl("ddlSanctionEntryNoOH");
                for (int i = 1; i <= number; i++)
                {
                    string value = i.ToString();
                    ddl.Items.Add(new ListItem(value));
                }
            }
        }

    }

    protected void AuthorType_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow currentRow = (GridViewRow)((DropDownList)sender).Parent.Parent;

        DropDownList AuthorType = (DropDownList)currentRow.FindControl("AuthorType");
        DropDownList isLeadPI = (DropDownList)currentRow.FindControl("isLeadPI");

        int countAuthType = 0;
        for (int i = 0; i < Grid_AuthorEntry.Rows.Count; i++)
        {

            DropDownList AuthorType1 = (DropDownList)Grid_AuthorEntry.Rows[i].Cells[3].FindControl("AuthorType");
            DropDownList isLeadPI1 = (DropDownList)Grid_AuthorEntry.Rows[i].Cells[3].FindControl("isLeadPI");

            if (AuthorType.SelectedValue == "P")
            {
                countAuthType = countAuthType + 1;
            }

        }
        if ((AuthorType.SelectedValue == "P"))
        {

            if (countAuthType == 1)
            {

                isLeadPI.SelectedValue = "Y";

            }

            else
            {

                isLeadPI.SelectedValue = "N";

            }

        }

        if (AuthorType.SelectedValue == "C")
        {

            isLeadPI.Enabled = false;

            isLeadPI.SelectedValue = "N";

        }

        else
        {

            isLeadPI.Enabled = true;

        } 
    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        string AppendInstitutionNamess = null;
        int countCorrAuth = 0;
        int countAuthType = 0;
      
        int countLeadPI = 0;
        int countLeadPIS = 0;
        int countLeadPIF = 0;
        try
        {

            Business b = new Business();
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
            Business B = new Business();
            GrantData j = new GrantData();
            GrantData[] JD = new GrantData[dtCurrentTable.Rows.Count];


            if (txtPImovecomment.Text == "")
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please enter the comment')</script>");
                return;
            }

            string GId = TextBoxID.Text.Trim();
            string GUnit = DropDownListGrUnit.SelectedValue;
            j.GID = GId;
            j.AddtionalComments = TextBoxAdComments.Text.Trim();
            j.GrantUnit = GUnit;
            j.CreatedBy = Session["UserId"].ToString();
            j.CreatedDate = DateTime.Now;
            j.PIMoveFeedback = txtPImovecomment.Text.Trim();
            MainpanelGrant.Enabled = false;

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
                    DropDownList isLeadPI = (DropDownList)Grid_AuthorEntry.Rows[rowIndex1].Cells[0].FindControl("isLeadPI");
                    DropDownList DropdownStudentInstitutionName = (DropDownList)Grid_AuthorEntry.Rows[rowIndex1].Cells[0].FindControl("DropdownStudentInstitutionName");
                    DropDownList DropdownStudentDepartmentName = (DropDownList)Grid_AuthorEntry.Rows[rowIndex1].Cells[0].FindControl("DropdownStudentDepartmentName");

                    DropDownList NationalType = (DropDownList)Grid_AuthorEntry.Rows[rowIndex1].Cells[0].FindControl("NationalType");
                    DropDownList ContinentId = (DropDownList)Grid_AuthorEntry.Rows[rowIndex1].Cells[0].FindControl("ContinentId");

                    if (AuthorName.Text == "")
                    {
                        ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please enter Investigator Name!')</script>");
                        return;

                    }
                    if (AuthorType.Text == "")
                    {
                        ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please select Investigator Type!')</script>");
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
                    if (DropdownMuNonMu.SelectedValue == "M" || DropdownMuNonMu.SelectedValue == "S")
                    {
                        if (MailId.Text == "")
                        {
                            ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please enter MailId!')</script>");
                            return;

                        }
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
                    JD[i].AuthorType = AuthorType.Text.Trim();
                    JD[i].LeadPI = isLeadPI.Text.Trim();
                    if (countAuthType < 1)
                    {
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
                            else if (JD[i].MUNonMU == "O")
                            {
                                j.MUNonMUUTN = "MUTN";
                                j.PiInstId = DropdownStudentInstitutionName.SelectedValue;
                                j.PiDeptId = DropdownStudentDepartmentName.SelectedValue;

                            }
                            else if (JD[i].MUNonMU == "S")
                            {
                                j.MUNonMUUTN = "NUTN";
                                j.PiInstId = Session["InstituteId"].ToString();
                                j.PiDeptId = Session["Department"].ToString();
                            }
                        }
                    }
                    if (JD[i].AuthorType == "P")
                    {
                        countAuthType = countAuthType + 1;
                    }
                   
                    if (JD[i].LeadPI == "Y")
                    {
                        countLeadPI = countLeadPI + 1;
                    }
                    if (JD[i].isCorrAuth == "Y")
                    {
                        countCorrAuth = countCorrAuth + 1;
                    }
                    if (JD[i].LeadPI == "Y")
                    {
                        if (JD[i].MUNonMU == "S")
                        {
                            countLeadPIS = countLeadPIS + 1;
                        }
                        else if (JD[i].MUNonMU == "M")
                        {
                            countLeadPIF = countLeadPIF + 1;
                        }
                    }
                    rowIndex1++;
                }


                for (int i = 0; i < JD.Length; i++)
                {
                    if (AppendInstitutionNamess == null)
                    {
                        JD[i].AppendInstitutionNames = JD[i].InstitutionName;
                        AppendInstitutionNamess = JD[i].InstitutionName;
                        j.AppendInstitutionNames = JD[i].AppendInstitutionNames;
                        JD[i].AppendInstitutions = JD[i].Institution;
                        j.AppendInstitutions = JD[i].AppendInstitutions;

                    }
                    else
                    {
                        for (int l = 0; l < JD.Length - i; l++)
                        {

                            //  if (JD[l].AppendInstitutionNames.Contains(JD[i].InstitutionName))
                            if (JD[l].AppendInstitutions.Contains(JD[i].Institution))
                            {

                                //  JD[i].AppendInstitutionNames = JD[i - 1].AppendInstitutionNames + ',' + InstitutionName.Text.Trim();
                            }
                            else
                            {
                                JD[i].AppendInstitutionNames = JD[i - 1].AppendInstitutionNames + ',' + JD[i].InstitutionName;
                            }

                        }

                    }
                    j.AppendInstitutionNames = JD[i].AppendInstitutionNames;
                    j.AppendInstitutions = JD[i].AppendInstitutions;

                }

            }

            //if (countAuthType > 1)
            //{
            //    ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Primary Investigator cannot be more than once!')</script>");
            //    return;

            //}
            if (countAuthType == 0)
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Select atleast one Investigator Type as Primary Investigator !')</script>");
                return;

            }
           

            if (countLeadPI > 1)
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Lead PI cannot be more than one!')</script>");
                return;

            }

            if (countLeadPI == 0)
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Select atleast one Lead PI!')</script>");
                return;

            }
            if (DropDownListTypeGrant.SelectedValue == "GS")
            {
                if (countLeadPIS == 0)
                {
                    ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Select atleast one Lead PI as Student!')</script>");
                    return;

                }
            }
            else if (DropDownListTypeGrant.SelectedValue == "SG")
            {
                if (countLeadPIF == 0)
                {
                    ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Select atleast one Lead PI as Faculty!')</script>");
                    return;

                }
            }
            int result = 0;


            result = B.UpdateGrantEntryPIMove(j, JD);

            if (result == 1)
            {

                btnSave.Enabled = false;
                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('PI Movement is Successfully  of ID: " + TextBoxID.Text + "')</script>");
                log.Info("Grant Updated Successfully, of ID: " + TextBoxID.Text);

            }
            else
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert(' Error!!!!!!!!!!!!')</script>");
                log.Error("Grant Updated Error!!!,  of ID: " + TextBoxID.Text);

            }
        }

        catch (Exception ex)
        {
            log.Error("Inside Catch Block Of Publication" + ex.Message + " UserID : " + Session["UserId"].ToString());

            log.Error(ex.StackTrace);


            if (ex.Message.Contains("The string was not recognized as a valid DateTime"))
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Date is not valid!!!!!!!!!!!!')</script>");

            }
            else if (ex.Message.Contains("Input string was not in a correct format"))
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Error!!!')</script>");

            }
            else if (ex.Message.Contains("There is already an open DataReader"))
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Grant data Creation Saved..Upload failed!!!!!!!!!!!!')</script>");

            }
            else
                if (ex.Message.Contains("Unable to cast object of type 'System.DBNull' to type 'System.String'."))
                {

                    ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('UTN Id is not found')</script>");

                }
            else
                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Grant data Creation Failed!!!!!!!!!!!!')</script>");

        }

    }
}
