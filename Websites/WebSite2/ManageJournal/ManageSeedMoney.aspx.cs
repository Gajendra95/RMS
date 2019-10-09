using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using System.Collections;

public partial class ManageJournal_ManageSeedMoney : System.Web.UI.Page
{
    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Label3.Visible = false;
            Label4.Visible = false;
            DateTime time = DateTime.Now;
            string date = time.ToShortDateString();         
            CompareValidator1.ValueToCompare = time.ToString("dd/MM/yyyy");
            TextBoxFromDate.Text = Convert.ToString(date);
            TextBoxToDate.Text = Convert.ToString(date);
            initialsetup();
            initialrow();
            Button5.Visible = false;
            RadioButtonListUserType_SelectedIndexChanged(sender, e);
             //DropDownListAuthorType_SelectedIndexChanged(sender,e);
            int result = 0;
            Business b = new Business();
            result = b.CheckSeedMoneyEntry(DropDownListAuthorType.SelectedValue);
            myHiddenOldProjecttype.Value = result.ToString();
            StatusofSeedMoney.Value = RadioButtonListUserType.SelectedValue;
            setinitialbudgetAmount();
        }
    }

    private void setinitialbudgetAmount()
    {
        SqlDataSourceFacultyCategoty.SelectCommand = "select Amount,BudgetId from SeedMoneyBudget_M where Type='F' and BudgetId in(1,2)";
        GridViewFacultyCategoty.DataSourceID = "SqlDataSourceFacultyCategoty";
        GridViewFacultyCategoty.DataBind();
        GridViewFacultyCategoty.Visible = true;
        ArrayList FAmount = new ArrayList();
        //for (int i = 0; i < GridViewFaculty.Rows.Count; i++)
        //{
        //    CheckBox Checkboxfaculty = (CheckBox)GridViewFaculty.Rows[i].FindControl("cbSelect");
        //    Label BudgetId = (Label)GridViewFaculty.Rows[i].FindControl("TextBoxFBudgetId");
        //    Label Amount = (Label)GridViewFaculty.Rows[i].FindControl("TextBoxFAmount");

        //    if (BudgetId.Text)
        //    {
        //        string id = BudgetId.Text;              
        //        string amount = Amount.Text;
        //        Checkboxfaculty.Checked == true
        //    }

        //}
    }

    private void initialrow()
    {
        PanelseedMoney.Visible = false;
        SqlDataSourceseedMoney.SelectCommand = "select Id,FromDate,ToDate,Status,Type,Active from SeedMoneyActive ";
        GridViewSeedMoney.DataSourceID = "SqlDataSourceseedMoney";
        SqlDataSourceseedMoney.DataBind();
        GridViewSeedMoney.DataBind();
        ModalPopupExtender3.Show();
    }

    private void initialsetup()
    {
        Label3.Visible = true;
        Label4.Visible = true;
        if (DropDownListAuthorType.SelectedValue == "B")
        {

            Label3.Visible = true;
            imageBkCbtn.Visible = true;
            ImageButton1.Visible = true;
            GridViewFacultyCategoty.Visible = false;
            GridViewstudentCategoty.Visible = false;           
            Label4.Visible = true;
            

            SqlDataSourceFaculty.SelectCommand = "select BudgetId,Amount from SeedMoneyBudget_M where Type='F'";
            GridViewFaculty.DataSourceID = "SqlDataSourceFaculty";
            SqlDataSourceFaculty.DataBind();
            GridViewFaculty.DataBind();
          

            SqlDataSourceStudent.SelectCommand = "select BudgetId,Amount from SeedMoneyBudget_M where Type='S'";
            GridViewStudent.DataSourceID = "SqlDataSourceStudent";
            SqlDataSourceStudent.DataBind();
            GridViewStudent.DataBind();
            popupPanelstudent.Visible = false;
            popupPanelFaculty.Visible = false;
           
        }
        else if (DropDownListAuthorType.SelectedValue == "F")
        {
            GridViewFacultyCategoty.Visible = false;
            GridViewstudentCategoty.Visible = false;
            Label3.Visible = true;
          
            Label4.Visible = false;
            imageBkCbtn.Visible = true;
            ImageButton1.Visible = false;
          

            SqlDataSourceFaculty.SelectCommand = "select BudgetId,Amount from SeedMoneyBudget_M where Type='F'";
            GridViewFaculty.DataSourceID = "SqlDataSourceFaculty";
            SqlDataSourceFaculty.DataBind();
            GridViewFaculty.DataBind();
            popupPanelstudent.Visible = false;
            popupPanelFaculty.Visible = false;
          

        }
        else if (DropDownListAuthorType.SelectedValue == "S")
        {
            GridViewFacultyCategoty.Visible = false;
            GridViewstudentCategoty.Visible = false;
            Label4.Visible = true;
          
            Label3.Visible = false;
            imageBkCbtn.Visible = false;
            ImageButton1.Visible = true;
         
           

            SqlDataSourceStudent.SelectCommand = "select BudgetId,Amount from SeedMoneyBudget_M where Type='S'";
            GridViewStudent.DataSourceID = "SqlDataSourceStudent";
            SqlDataSourceStudent.DataBind();
            GridViewStudent.DataBind();
            popupPanelstudent.Visible = false;
            popupPanelFaculty.Visible = false;
          
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (!Page.IsValid)
        {
            return;
        }
        try
        {
            SeedMoney a = new SeedMoney();
            Business b = new Business();
            int CountCycle = b.checkexistingSeedmoneyentry(DropDownListAuthorType.SelectedValue);
            if (CountCycle > 0)
            {
                string CloseWindow = "alert('Please clear the existing  Seed money entries before adding the new cycle!')";
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow, true);
                return;
            }

            if (RadioButtonListUserType.SelectedValue == "A")
            {
                 //string confirmValue2 = Request.Form["confirm_value2"];
                 //if (confirmValue2 == "Yes")
                 //{
                     ArrayList listFaculty = new ArrayList();
                     ArrayList listStudent = new ArrayList();
                     if (TextBoxRemarks.Text == "")
                     {
                         string CloseWindow = "alert('Please enter the Remarks!')";
                         ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow, true);
                         return;
                     }
                     if (DropDownListAuthorType.SelectedValue == "B")
                     {
                         if ((GridViewFacultyCategoty.Rows.Count == 0) && (GridViewstudentCategoty.Rows.Count == 0))
                         {
                             string CloseWindow = "alert('Please enter the Category amount for Student and Faculty!')";
                             ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow, true);
                             return;
                         }
                         else if ((GridViewFacultyCategoty.Rows.Count != 0) && (GridViewstudentCategoty.Rows.Count == 0))
                         {
                             string CloseWindow = "alert('Please enter the Category amount for Student!')";
                             ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow, true);
                             return;
                         }
                         else if ((GridViewFacultyCategoty.Rows.Count == 0) && (GridViewstudentCategoty.Rows.Count != 0))
                         {
                             string CloseWindow = "alert('Please enter the Category amount for Faculty!')";
                             ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow, true);
                             return;
                         }
                     }
                     else if (DropDownListAuthorType.SelectedValue == "F")
                     {

                         if (GridViewFacultyCategoty.Rows.Count == 0)
                         {
                             string CloseWindow = "alert('Please enter the Category amount for Faculty!')";
                             ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow, true);
                             return;
                         }
                     }
                     else if (DropDownListAuthorType.SelectedValue == "S")
                     {
                         if (GridViewstudentCategoty.Rows.Count == 0)
                         {
                             string CloseWindow = "alert('Please enter the Category amount for Student!')";
                             ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow, true);
                             return;
                         }
                     }
                     //if (DropDownListactive.SelectedValue == "N")
                     //{
                     //    if (TextBoxNote.Text == "")
                     //    {
                     //        string CloseWindow = "alert('Please enter the Message to disable the Seed Money Entry!')";
                     //        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow, true);
                     //        return;
                     //    }
                     //}
                     //else 
                     //{
                     //    //if (DropDownListAuthorType.SelectedValue == "F")
                     //    //{
                     //    //    CustomValidator1.Enabled = true;
                     //    //    CustomValidator2.Enabled = false;
                     //    //}
                     //    //else if (DropDownListAuthorType.SelectedValue == "S")
                     //    //{
                     //    //    CustomValidator1.Enabled = false;
                     //    //    CustomValidator2.Enabled = true;

                     //    //}
                     //    //else
                     //    //{
                     //    //    CustomValidator1.Enabled = true;
                     //    //    CustomValidator2.Enabled = true;
                     //    //}
                     //}

                  
                   
                     string category = DropDownListAuthorType.SelectedValue.Trim();
                     string active = DropDownListactive.SelectedValue.Trim();
                     string remarks = TextBoxRemarks.Text.Trim();
                     //string Note = TextBoxNote.Text.Trim();
                     string updatedby = Session["UserId"].ToString();
                     DateTime FromDate = Convert.ToDateTime(TextBoxFromDate.Text.Trim());
                     DateTime ToDate = Convert.ToDateTime(TextBoxToDate.Text.Trim());
                     DateTime updateddate = DateTime.Now;
                     string authorType = DropDownListAuthorType.SelectedValue.Trim();
                     CompareValidatorTextBoxDate.ValueToCompare = FromDate.ToString("dd/MM/yyyy");

                     if (DropDownListAuthorType.SelectedValue == "F")
                     {

                         for (int i = 0; i < GridViewFacultyCategoty.Rows.Count; i++)
                         {
                             Label BudgetId = (Label)GridViewFacultyCategoty.Rows[i].FindControl("TextBoxFacultyBudgetId");
                             Label Amount = (Label)GridViewFacultyCategoty.Rows[i].FindControl("TextBoxFBudgetAmount");
                             string id = BudgetId.Text;
                             listFaculty.Add(id);
                         }
                     }
                     else if (DropDownListAuthorType.SelectedValue == "S")
                     {
                         for (int i = 0; i < GridViewstudentCategoty.Rows.Count; i++)
                         {
                             Label BudgetId = (Label)GridViewstudentCategoty.Rows[i].FindControl("TextBoxStudentBudgetId");
                             Label Amount = (Label)GridViewstudentCategoty.Rows[i].FindControl("TextBoxSBudgetAmount");
                             string id = BudgetId.Text;
                             listFaculty.Add(id);
                         }
                     }
                     else if (DropDownListAuthorType.SelectedValue == "B")
                     {

                         for (int i = 0; i < GridViewFacultyCategoty.Rows.Count; i++)
                         {
                             Label BudgetId = (Label)GridViewFacultyCategoty.Rows[i].FindControl("TextBoxFacultyBudgetId");
                             Label Amount = (Label)GridViewFacultyCategoty.Rows[i].FindControl("TextBoxFBudgetAmount");
                             string id = BudgetId.Text;
                             listFaculty.Add(id);
                         }
                         for (int i = 0; i < GridViewstudentCategoty.Rows.Count; i++)
                         {
                             Label BudgetId = (Label)GridViewstudentCategoty.Rows[i].FindControl("TextBoxStudentBudgetId");
                             Label Amount = (Label)GridViewstudentCategoty.Rows[i].FindControl("TextBoxSBudgetAmount");
                             string id = BudgetId.Text;
                             listFaculty.Add(id);
                         }
                     }
                     //if (DropDownListAuthorType.SelectedValue == "B")
                     //{
                     //    listFaculty.Clear();
                     //    for (int i = 0; i < CheckboxCategory.Items.Count; i++)
                     //    {
                     //        string chkValue = null;
                     //        if (CheckboxCategory.Items[i].Selected == true)
                     //        {
                     //            chkValue = CheckboxCategory.Items[i].Value;
                     //            listFaculty.Add(chkValue);
                     //        }
                     //    }
                     //    for (int i = 0; i < CheckboxCategory1.Items.Count; i++)
                     //    {
                     //        string chkValue1 = null;
                     //        if (CheckboxCategory1.Items[i].Selected == true)
                     //        {
                     //            chkValue1 = CheckboxCategory1.Items[i].Value;
                     //            listFaculty.Add(chkValue1);
                     //        }
                     //    }
                     //}
                     //else
                     //    if (DropDownListAuthorType.SelectedValue == "F")
                     //    {
                     //        listFaculty.Clear();
                     //        for (int i = 0; i < CheckboxCategory.Items.Count; i++)
                     //        {
                     //            string chkValue = null;
                     //            if (CheckboxCategory.Items[i].Selected == true)
                     //            {
                     //                chkValue = CheckboxCategory.Items[i].Value;
                     //                listFaculty.Add(chkValue);
                     //            }
                     //        }
                     //    }
                     //    else
                     //        if (DropDownListAuthorType.SelectedValue == "S")
                     //        {
                     //            listFaculty.Clear();
                     //            for (int i = 0; i < CheckboxCategory1.Items.Count; i++)
                     //            {
                     //                string chkValue1 = null;
                     //                if (CheckboxCategory1.Items[i].Selected == true)
                     //                {
                     //                    chkValue1 = CheckboxCategory1.Items[i].Value;
                     //                    listFaculty.Add(chkValue1);
                     //                }
                     //            }
                     //        }
                     a.Entrytype = category;
                     a.Enable = "N";
                     a.EnableRemarks = remarks;
                     //a.Note = Note;
                     a.UpdatedBy = updatedby;
                     a.EnableDate = Convert.ToDateTime(updateddate);
                     a.Fromdate = Convert.ToDateTime(FromDate);
                     a.Todate = Convert.ToDateTime(ToDate);
                     a.Status = "NEW";
                     string Fromdate1 = FromDate.ToString("yyyy-MM-dd");
                     string ToDate1 = ToDate.ToString("yyyy-MM-dd");
                     ArrayList IDList = new ArrayList();
                     IDList = b.checkexistingCycle(Fromdate1, ToDate1, a.Entrytype);
                     if (IDList.Count > 0)
                     {
                         string CloseWindow = "alert('Seed Money Cycle Already exists!')";
                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow, true);
                         return;
                     }

                     int result = 0;
                     if (TextBox1.Text != "")
                     {
                         a.id = Convert.ToInt32(TextBox1.Text.Trim());
                     }

                     //result = b.disableSeedMoneyEntry(DropDownListAuthorType.SelectedValue);

                     result = b.EnableSeedMoneyEntry(a, listFaculty);
                     TextBox1.Text = Session["Pubseed"].ToString();
                     if (result >= 1)
                     {
                         string CloseWindow = "alert('Seed Money cycle added succesfully for: " + DropDownListAuthorType.SelectedItem + "')";
                         ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow, true);
                         Button5.Visible = true;
                     }
                     else
                     {
                         string CloseWindow = "alert('Error!')";
                         ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow, true);
                     }
                 //}
                 //else if (confirmValue2 == "No")
                 //{

                 //}
                 //else if ((confirmValue2 =="")||(confirmValue2 ==null))
                 //{
                 //    ArrayList listFaculty = new ArrayList();
                 //    ArrayList listStudent = new ArrayList();
                 //    if (TextBoxRemarks.Text == "")
                 //    {
                 //        string CloseWindow = "alert('Please enter the Remarks!')";
                 //        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow, true);
                 //        return;
                 //    }
                 //    if (DropDownListAuthorType.SelectedValue == "B")
                 //    {
                 //        if ((GridViewFacultyCategoty.Rows.Count == 0) && (GridViewstudentCategoty.Rows.Count == 0))
                 //        {
                 //            string CloseWindow = "alert('Please enter the Category amount for Student and Faculty!')";
                 //            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow, true);
                 //            return;
                 //        }
                 //        else if ((GridViewFacultyCategoty.Rows.Count != 0) && (GridViewstudentCategoty.Rows.Count == 0))
                 //        {
                 //            string CloseWindow = "alert('Please enter the Category amount for Student!')";
                 //            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow, true);
                 //            return;
                 //        }
                 //        else if ((GridViewFacultyCategoty.Rows.Count == 0) && (GridViewstudentCategoty.Rows.Count != 0))
                 //        {
                 //            string CloseWindow = "alert('Please enter the Category amount for Faculty!')";
                 //            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow, true);
                 //            return;
                 //        }
                 //    }
                 //    else if (DropDownListAuthorType.SelectedValue == "F")
                 //    {

                 //        if (GridViewFacultyCategoty.Rows.Count == 0)
                 //        {
                 //            string CloseWindow = "alert('Please enter the Category amount for Faculty!')";
                 //            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow, true);
                 //            return;
                 //        }
                 //    }
                 //    else if (DropDownListAuthorType.SelectedValue == "S")
                 //    {
                 //        if (GridViewstudentCategoty.Rows.Count == 0)
                 //        {
                 //            string CloseWindow = "alert('Please enter the Category amount for Student!')";
                 //            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow, true);
                 //            return;
                 //        }
                 //    }
                 //    //if (DropDownListactive.SelectedValue == "N")
                 //    //{
                 //    //    if (TextBoxNote.Text == "")
                 //    //    {
                 //    //        string CloseWindow = "alert('Please enter the Message to disable the Seed Money Entry!')";
                 //    //        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow, true);
                 //    //        return;
                 //    //    }
                 //    //}
                 //    //else 
                 //    //{
                 //    //    //if (DropDownListAuthorType.SelectedValue == "F")
                 //    //    //{
                 //    //    //    CustomValidator1.Enabled = true;
                 //    //    //    CustomValidator2.Enabled = false;
                 //    //    //}
                 //    //    //else if (DropDownListAuthorType.SelectedValue == "S")
                 //    //    //{
                 //    //    //    CustomValidator1.Enabled = false;
                 //    //    //    CustomValidator2.Enabled = true;

                 //    //    //}
                 //    //    //else
                 //    //    //{
                 //    //    //    CustomValidator1.Enabled = true;
                 //    //    //    CustomValidator2.Enabled = true;
                 //    //    //}
                 //    //}
                 //    SeedMoney a = new SeedMoney();
                 //    Business b = new Business();
                 //    string category = DropDownListAuthorType.SelectedValue.Trim();
                 //    string active = DropDownListactive.SelectedValue.Trim();
                 //    string remarks = TextBoxRemarks.Text.Trim();
                 //    string Note = TextBoxNote.Text.Trim();
                 //    string updatedby = Session["UserId"].ToString();
                 //    DateTime FromDate = Convert.ToDateTime(TextBoxFromDate.Text.Trim());
                 //    DateTime ToDate = Convert.ToDateTime(TextBoxToDate.Text.Trim());
                 //    DateTime updateddate = DateTime.Now;
                 //    string authorType = DropDownListAuthorType.SelectedValue.Trim();
                 //    CompareValidatorTextBoxDate.ValueToCompare = FromDate.ToString("dd/MM/yyyy");

                 //    if (DropDownListAuthorType.SelectedValue == "F")
                 //    {

                 //        for (int i = 0; i < GridViewFacultyCategoty.Rows.Count; i++)
                 //        {
                 //            Label BudgetId = (Label)GridViewFacultyCategoty.Rows[i].FindControl("TextBoxFacultyBudgetId");
                 //            Label Amount = (Label)GridViewFacultyCategoty.Rows[i].FindControl("TextBoxFBudgetAmount");
                 //            string id = BudgetId.Text;
                 //            listFaculty.Add(id);
                 //        }
                 //    }
                 //    else if (DropDownListAuthorType.SelectedValue == "S")
                 //    {
                 //        for (int i = 0; i < GridViewstudentCategoty.Rows.Count; i++)
                 //        {
                 //            Label BudgetId = (Label)GridViewstudentCategoty.Rows[i].FindControl("TextBoxStudentBudgetId");
                 //            Label Amount = (Label)GridViewstudentCategoty.Rows[i].FindControl("TextBoxSBudgetAmount");
                 //            string id = BudgetId.Text;
                 //            listFaculty.Add(id);
                 //        }
                 //    }
                 //    else if (DropDownListAuthorType.SelectedValue == "B")
                 //    {

                 //        for (int i = 0; i < GridViewFacultyCategoty.Rows.Count; i++)
                 //        {
                 //            Label BudgetId = (Label)GridViewFacultyCategoty.Rows[i].FindControl("TextBoxFacultyBudgetId");
                 //            Label Amount = (Label)GridViewFacultyCategoty.Rows[i].FindControl("TextBoxFBudgetAmount");
                 //            string id = BudgetId.Text;
                 //            listFaculty.Add(id);
                 //        }
                 //        for (int i = 0; i < GridViewstudentCategoty.Rows.Count; i++)
                 //        {
                 //            Label BudgetId = (Label)GridViewstudentCategoty.Rows[i].FindControl("TextBoxStudentBudgetId");
                 //            Label Amount = (Label)GridViewstudentCategoty.Rows[i].FindControl("TextBoxSBudgetAmount");
                 //            string id = BudgetId.Text;
                 //            listFaculty.Add(id);
                 //        }
                 //    }
                 //    //if (DropDownListAuthorType.SelectedValue == "B")
                 //    //{
                 //    //    listFaculty.Clear();
                 //    //    for (int i = 0; i < CheckboxCategory.Items.Count; i++)
                 //    //    {
                 //    //        string chkValue = null;
                 //    //        if (CheckboxCategory.Items[i].Selected == true)
                 //    //        {
                 //    //            chkValue = CheckboxCategory.Items[i].Value;
                 //    //            listFaculty.Add(chkValue);
                 //    //        }
                 //    //    }
                 //    //    for (int i = 0; i < CheckboxCategory1.Items.Count; i++)
                 //    //    {
                 //    //        string chkValue1 = null;
                 //    //        if (CheckboxCategory1.Items[i].Selected == true)
                 //    //        {
                 //    //            chkValue1 = CheckboxCategory1.Items[i].Value;
                 //    //            listFaculty.Add(chkValue1);
                 //    //        }
                 //    //    }
                 //    //}
                 //    //else
                 //    //    if (DropDownListAuthorType.SelectedValue == "F")
                 //    //    {
                 //    //        listFaculty.Clear();
                 //    //        for (int i = 0; i < CheckboxCategory.Items.Count; i++)
                 //    //        {
                 //    //            string chkValue = null;
                 //    //            if (CheckboxCategory.Items[i].Selected == true)
                 //    //            {
                 //    //                chkValue = CheckboxCategory.Items[i].Value;
                 //    //                listFaculty.Add(chkValue);
                 //    //            }
                 //    //        }
                 //    //    }
                 //    //    else
                 //    //        if (DropDownListAuthorType.SelectedValue == "S")
                 //    //        {
                 //    //            listFaculty.Clear();
                 //    //            for (int i = 0; i < CheckboxCategory1.Items.Count; i++)
                 //    //            {
                 //    //                string chkValue1 = null;
                 //    //                if (CheckboxCategory1.Items[i].Selected == true)
                 //    //                {
                 //    //                    chkValue1 = CheckboxCategory1.Items[i].Value;
                 //    //                    listFaculty.Add(chkValue1);
                 //    //                }
                 //    //            }
                 //    //        }
                 //    a.Entrytype = category;
                 //    a.Enable = "Y";
                 //    a.EnableRemarks = remarks;
                 //    a.Note = Note;
                 //    a.UpdatedBy = updatedby;
                 //    a.EnableDate = Convert.ToDateTime(updateddate);
                 //    a.Fromdate = Convert.ToDateTime(FromDate);
                 //    a.Todate = Convert.ToDateTime(ToDate);
                 //    a.Status = "NEW";

                 //    int result = 0;
                 //    if (TextBox1.Text != "")
                 //    {
                 //        a.id = Convert.ToInt32(TextBox1.Text.Trim());
                 //    }
                 //    result = b.EnableSeedMoneyEntry(a, listFaculty);
                 //    TextBox1.Text = Session["Pubseed"].ToString();
                 //    if (result >= 1)
                 //    {
                 //        string CloseWindow = "alert('Seed Money cycle added succesfully for: " + DropDownListAuthorType.SelectedItem + "')";
                 //        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow, true);
                 //        Button5.Visible = true;
                 //    }
                 //    else
                 //    {
                 //        string CloseWindow = "alert('Error!')";
                 //        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow, true);
                 //    }
                 //}
            }
            else 
            {
                ArrayList listFaculty = new ArrayList();
                ArrayList listStudent = new ArrayList();
                if (TextBoxRemarks.Text == "")
                {
                    string CloseWindow = "alert('Please enter the Remarks!')";
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow, true);
                    return;
                }
                if (DropDownListAuthorType.SelectedValue == "B")
                {
                    if ((GridViewFacultyCategoty.Rows.Count == 0) && (GridViewstudentCategoty.Rows.Count == 0))
                    {
                        string CloseWindow = "alert('Please enter the Category amount for Student and Faculty!')";
                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow, true);
                        return;
                    }
                    else if ((GridViewFacultyCategoty.Rows.Count != 0) && (GridViewstudentCategoty.Rows.Count == 0))
                    {
                        string CloseWindow = "alert('Please enter the Category amount for Student!')";
                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow, true);
                        return;
                    }
                    else if ((GridViewFacultyCategoty.Rows.Count == 0) && (GridViewstudentCategoty.Rows.Count != 0))
                    {
                        string CloseWindow = "alert('Please enter the Category amount for Faculty!')";
                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow, true);
                        return;
                    }
                }
                else if (DropDownListAuthorType.SelectedValue == "F")
                {

                    if (GridViewFacultyCategoty.Rows.Count == 0)
                    {
                        string CloseWindow = "alert('Please enter the Category amount for Faculty!')";
                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow, true);
                        return;
                    }
                }
                else if (DropDownListAuthorType.SelectedValue == "S")
                {
                    if (GridViewstudentCategoty.Rows.Count == 0)
                    {
                        string CloseWindow = "alert('Please enter the Category amount for Student!')";
                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow, true);
                        return;
                    }
                }
                //if (DropDownListactive.SelectedValue == "N")
                //{
                //    if (TextBoxNote.Text == "")
                //    {
                //        string CloseWindow = "alert('Please enter the Message to disable the Seed Money Entry!')";
                //        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow, true);
                //        return;
                //    }
                //}
                //else 
                //{
                //    //if (DropDownListAuthorType.SelectedValue == "F")
                //    //{
                //    //    CustomValidator1.Enabled = true;
                //    //    CustomValidator2.Enabled = false;
                //    //}
                //    //else if (DropDownListAuthorType.SelectedValue == "S")
                //    //{
                //    //    CustomValidator1.Enabled = false;
                //    //    CustomValidator2.Enabled = true;

                //    //}
                //    //else
                //    //{
                //    //    CustomValidator1.Enabled = true;
                //    //    CustomValidator2.Enabled = true;
                //    //}
                //}
                //SeedMoney a = new SeedMoney();
                //Business b = new Business();
                string category = DropDownListAuthorType.SelectedValue.Trim();
                string active = DropDownListactive.SelectedValue.Trim();
                string remarks = TextBoxRemarks.Text.Trim();
                //string Note = TextBoxNote.Text.Trim();
                string updatedby = Session["UserId"].ToString();
                DateTime FromDate = Convert.ToDateTime(TextBoxFromDate.Text.Trim());
                DateTime ToDate = Convert.ToDateTime(TextBoxToDate.Text.Trim());
                DateTime updateddate = DateTime.Now;
                string authorType = DropDownListAuthorType.SelectedValue.Trim();
                CompareValidatorTextBoxDate.ValueToCompare = FromDate.ToString("dd/MM/yyyy");

                if (DropDownListAuthorType.SelectedValue == "F")
                {

                    for (int i = 0; i < GridViewFacultyCategoty.Rows.Count; i++)
                    {
                        Label BudgetId = (Label)GridViewFacultyCategoty.Rows[i].FindControl("TextBoxFacultyBudgetId");
                        Label Amount = (Label)GridViewFacultyCategoty.Rows[i].FindControl("TextBoxFBudgetAmount");
                        string id = BudgetId.Text;
                        listFaculty.Add(id);
                    }
                }
                else if (DropDownListAuthorType.SelectedValue == "S")
                {
                    for (int i = 0; i < GridViewstudentCategoty.Rows.Count; i++)
                    {
                        Label BudgetId = (Label)GridViewstudentCategoty.Rows[i].FindControl("TextBoxStudentBudgetId");
                        Label Amount = (Label)GridViewstudentCategoty.Rows[i].FindControl("TextBoxSBudgetAmount");
                        string id = BudgetId.Text;
                        listFaculty.Add(id);
                    }
                }
                else if (DropDownListAuthorType.SelectedValue == "B")
                {

                    for (int i = 0; i < GridViewFacultyCategoty.Rows.Count; i++)
                    {
                        Label BudgetId = (Label)GridViewFacultyCategoty.Rows[i].FindControl("TextBoxFacultyBudgetId");
                        Label Amount = (Label)GridViewFacultyCategoty.Rows[i].FindControl("TextBoxFBudgetAmount");
                        string id = BudgetId.Text;
                        listFaculty.Add(id);
                    }
                    for (int i = 0; i < GridViewstudentCategoty.Rows.Count; i++)
                    {
                        Label BudgetId = (Label)GridViewstudentCategoty.Rows[i].FindControl("TextBoxStudentBudgetId");
                        Label Amount = (Label)GridViewstudentCategoty.Rows[i].FindControl("TextBoxSBudgetAmount");
                        string id = BudgetId.Text;
                        listFaculty.Add(id);
                    }
                }
                //if (DropDownListAuthorType.SelectedValue == "B")
                //{
                //    listFaculty.Clear();
                //    for (int i = 0; i < CheckboxCategory.Items.Count; i++)
                //    {
                //        string chkValue = null;
                //        if (CheckboxCategory.Items[i].Selected == true)
                //        {
                //            chkValue = CheckboxCategory.Items[i].Value;
                //            listFaculty.Add(chkValue);
                //        }
                //    }
                //    for (int i = 0; i < CheckboxCategory1.Items.Count; i++)
                //    {
                //        string chkValue1 = null;
                //        if (CheckboxCategory1.Items[i].Selected == true)
                //        {
                //            chkValue1 = CheckboxCategory1.Items[i].Value;
                //            listFaculty.Add(chkValue1);
                //        }
                //    }
                //}
                //else
                //    if (DropDownListAuthorType.SelectedValue == "F")
                //    {
                //        listFaculty.Clear();
                //        for (int i = 0; i < CheckboxCategory.Items.Count; i++)
                //        {
                //            string chkValue = null;
                //            if (CheckboxCategory.Items[i].Selected == true)
                //            {
                //                chkValue = CheckboxCategory.Items[i].Value;
                //                listFaculty.Add(chkValue);
                //            }
                //        }
                //    }
                //    else
                //        if (DropDownListAuthorType.SelectedValue == "S")
                //        {
                //            listFaculty.Clear();
                //            for (int i = 0; i < CheckboxCategory1.Items.Count; i++)
                //            {
                //                string chkValue1 = null;
                //                if (CheckboxCategory1.Items[i].Selected == true)
                //                {
                //                    chkValue1 = CheckboxCategory1.Items[i].Value;
                //                    listFaculty.Add(chkValue1);
                //                }
                //            }
                //        }
                a.Entrytype = category;
                a.Enable = "Y";
                a.EnableRemarks = remarks;
                //a.Note = Note;
                a.UpdatedBy = updatedby;
                a.EnableDate = Convert.ToDateTime(updateddate);
                a.Fromdate = Convert.ToDateTime(FromDate);
                a.Todate = Convert.ToDateTime(ToDate);
                a.Status = "NEW";

                int result = 0;
                if (TextBox1.Text != "")
                {
                    a.id = Convert.ToInt32(TextBox1.Text.Trim());
                }
                result = b.UpdateManageSeedMoneyEntry(a, listFaculty);
                if (result >= 1)
                {
                    string CloseWindow = "alert('Seed Money cycle Updated succesfully for: " + DropDownListAuthorType.SelectedItem + "')";
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow, true);
                    Button5.Visible = true;
                }
            }

            //result = b.CheckSeedMoneyEntry(DropDownListAuthorType.SelectedValue);
            //if (result ==0)
            //{
            //    if (TextBox1.Text == "")
            //    {
            //        result = b.EnableSeedMoneyEntry(a, listFaculty);
            //    }
            //    else
            //    {
            //        result = b.UpdateSeedMoneyEntry(a, listFaculty);
            //    }
            //    if (result >= 1)
            //    {
            //        string CloseWindow = "alert('Seed Money cycle added succesfully for: " + DropDownListAuthorType.SelectedItem + "')";
            //        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow, true);
            //        Button5.Visible = true;
            //    }
            //    else
            //    {
            //        string CloseWindow = "alert('Error!')";
            //        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow, true);
            //    }
            //}
            //else
            //{
            //    string confirmValue2 = Request.Form["confirm_value2"];
            //    if (confirmValue2 == "Yes")
            //    {

            //        if (TextBox1.Text == "")
            //        {
            //            result = b.EnableSeedMoneyEntry(a, listFaculty);
            //        }
            //        else
            //        {
            //            result = b.UpdateSeedMoneyEntry(a, listFaculty);
            //        }
            //        if (result >= 1)
            //        {
            //            string CloseWindow = "alert('Seed Money cycle added succesfully for: " + DropDownListAuthorType.SelectedItem + "')";
            //            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow, true);
            //            Button5.Visible = true;
            //        }
            //        else
            //        {
            //            string CloseWindow = "alert('Error!')";
            //            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow, true);
            //        }
            //    }
            //}
        }
        catch (Exception ex)
        {
            log.Error(ex.StackTrace);
            log.Error(ex.Message);

            log.Error("Error!!!!!!!!!!!!!!!! ");

            ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Error!!!!!!!!!!')</script>");

        }

    }

    protected void DropDownListAuthorType_SelectedIndexChanged(object sender, EventArgs e)
    {
       
        if (DropDownListAuthorType.SelectedValue == "B")
        {
            Label3.Visible = true;
            Label4.Visible = true;
            imageBkCbtn.Visible = true;
            ImageButton1.Visible = true;
            GridViewFacultyCategoty.Visible = false;
            GridViewstudentCategoty.Visible = false;
            popupPanelstudent.Visible = false;
            popupPanelFaculty.Visible = false;
            int result = 0;
                Business b=new Business();
            result = b.CheckSeedMoneyEntry(DropDownListAuthorType.SelectedValue);
            myHiddenOldProjecttype.Value = result.ToString();
            //if ((GridViewFacultyCategoty.Rows.Count == 0) && (GridViewstudentCategoty.Rows.Count == 0))
            //{
            //    string CloseWindow = "alert('Please enter the Category amount for Student and Faculty!')";
            //    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow, true);
            //    return;
            //}
            //else if ((GridViewFacultyCategoty.Rows.Count != 0) && (GridViewstudentCategoty.Rows.Count == 0))
            //{
            //    string CloseWindow = "alert('Please enter the Category amount for Student!')";
            //    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow, true);
            //    return;
            //}
            //else if ((GridViewFacultyCategoty.Rows.Count != 0) && (GridViewstudentCategoty.Rows.Count != 0))
            //{
            //    string CloseWindow = "alert('Please enter the Category amount for Faculty!')";
            //    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow, true);
            //    return;
            //}
        }
        else if (DropDownListAuthorType.SelectedValue == "F")
        {
            GridViewFacultyCategoty.Visible = false;
            GridViewstudentCategoty.Visible = false;
            Label3.Visible = true;

            Label4.Visible = false;
            imageBkCbtn.Visible = true;
            ImageButton1.Visible = false;
            popupPanelstudent.Visible = false;
            int result = 0;
            Business b = new Business();
            result = b.CheckSeedMoneyEntry(DropDownListAuthorType.SelectedValue);
            myHiddenOldProjecttype.Value = result.ToString();
            //if (GridViewFacultyCategoty.Rows.Count != 0)
            //{
            //    string CloseWindow = "alert('Please enter the Category amount for Faculty!')";
            //    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow, true);
            //    return;
            //}
        }
        else if (DropDownListAuthorType.SelectedValue == "S")
        {
            GridViewFacultyCategoty.Visible = false;
            GridViewstudentCategoty.Visible = false;
            Label4.Visible = true;

            Label3.Visible = false;
            imageBkCbtn.Visible = false;
            ImageButton1.Visible = true;
            popupPanelFaculty.Visible = false;
            int result = 0;
            Business b = new Business();
            result = b.CheckSeedMoneyEntry(DropDownListAuthorType.SelectedValue);
            myHiddenOldProjecttype.Value = result.ToString();
            //if (GridViewstudentCategoty.Rows.Count != 0)
            //{
            //    string CloseWindow = "alert('Please enter the Category amount for Student!')";
            //    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow, true);
            //    return;
            //}
        }

    }
    protected void DropDownListactive_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownListactive.SelectedValue == "Y")
        {
            if (DropDownListAuthorType.SelectedValue == "F")

            {
                //CustomValidator1.Enabled = true;
                //CustomValidator2.Enabled = false;
                //popupPanelFaculty.Visible = true;
                SqlDataSourceFaculty.SelectCommand = "select BudgetId,Amount from SeedMoneyBudget_M where Type='F'";
                GridViewFaculty.DataSourceID = "SqlDataSourceFaculty";
                SqlDataSourceFaculty.DataBind();
                GridViewFaculty.DataBind();
            }
            else if (DropDownListAuthorType.SelectedValue == "S")
            {
                //CustomValidator1.Enabled = false;
                //CustomValidator2.Enabled = true;
                //popupPanelstudent.Visible = true;
                SqlDataSourceStudent.SelectCommand = "select BudgetId,Amount from SeedMoneyBudget_M where Type='S'";
                GridViewStudent.DataSourceID = "SqlDataSourceStudent";
                SqlDataSourceStudent.DataBind();
                GridViewStudent.DataBind();
            }
            else
            {
                //CustomValidator1.Enabled = true;
                //CustomValidator2.Enabled = true;
                //popupPanelFaculty.Visible = true;
                SqlDataSourceFaculty.SelectCommand = "select BudgetId,Amount from SeedMoneyBudget_M where Type='F' ";
                GridViewFaculty.DataSourceID = "SqlDataSourceFaculty";
                SqlDataSourceFaculty.DataBind();
                GridViewFaculty.DataBind();

                //popupPanelstudent.Visible = true;
                SqlDataSourceStudent.SelectCommand = "select BudgetId,Amount from SeedMoneyBudget_M where Type='S'";
                GridViewStudent.DataSourceID = "SqlDataSourceStudent";
                SqlDataSourceStudent.DataBind();
                GridViewStudent.DataBind();
            }
        }
        else 
        {
            //CustomValidator1.Enabled = false;
            //CustomValidator2.Enabled = false;
            popupPanelFaculty.Visible = false;
            popupPanelstudent.Visible = false;
        }
    }
    protected void showPop(object sender, EventArgs e)
    {
        //if (DropDownListAuthorType.SelectedValue == "F")
        //{
            popupPanelFaculty.Visible = true;
            SqlDataSourceFaculty.SelectCommand = "select BudgetId,Amount from SeedMoneyBudget_M where Type='F'";
            GridViewFaculty.DataSourceID = "SqlDataSourceFaculty";
            SqlDataSourceFaculty.DataBind();
            GridViewFaculty.DataBind();
            ModalPopupExtender1.Show();

            //ModalPopupExtender2.Hide();
        //}
        //else if (DropDownListAuthorType.SelectedValue == "S")
        //{
        //    SqlDataSourceUSer.SelectCommand = "select BudgetId,Amount from SeedMoneyBudget_M where Type='S'";
        //    GridViewStudent.DataSourceID = "SqlDataSourceUSer";
        //    SqlDataSourceUSer.DataBind();
        //    GridViewStudent.DataBind();
        //    ModalPopupExtender2.Show();
        //}
        //else
        //{
        //    SqlDataSourceUSer.SelectCommand = "select BudgetId,Amount from SeedMoneyBudget_M where Type='F'";
        //    GridViewFaculty.DataSourceID = "SqlDataSourceUSer";
        //    SqlDataSourceUSer.DataBind();
        //    GridViewFaculty.DataBind();
        //    ModalPopupExtender1.Show();
        //    //ModalPopupExtender2.Show();
        //}
    }
    protected void showPop1(object sender, EventArgs e)
    {
        //if (DropDownListAuthorType.SelectedValue == "F")
        //{
        popupPanelstudent.Visible = true;
        SqlDataSourceStudent.SelectCommand = "select BudgetId,Amount from SeedMoneyBudget_M where Type='S'";
        GridViewStudent.DataSourceID = "SqlDataSourceStudent";
        SqlDataSourceStudent.DataBind();
        GridViewStudent.DataBind();
        ModalPopupExtender2.Show();
        //ModalPopupExtender1.Hide();
        //}
        //else if (DropDownListAuthorType.SelectedValue == "S")
        //{
        //    SqlDataSourceUSer.SelectCommand = "select BudgetId,Amount from SeedMoneyBudget_M where Type='S'";
        //    GridViewStudent.DataSourceID = "SqlDataSourceUSer";
        //    SqlDataSourceUSer.DataBind();
        //    GridViewStudent.DataBind();
        //    ModalPopupExtender2.Show();
        //}
        //else
        //{
        //    SqlDataSourceUSer.SelectCommand = "select BudgetId,Amount from SeedMoneyBudget_M where Type='F'";
        //    GridViewFaculty.DataSourceID = "SqlDataSourceUSer";
        //    SqlDataSourceUSer.DataBind();
        //    GridViewFaculty.DataBind();
        //    ModalPopupExtender1.Show();
        //    //ModalPopupExtender2.Show();
        //}
    }
    protected void showPop2(object sender, EventArgs e)
    {
        PanelseedMoney.Visible = true;
        SqlDataSourceseedMoney.SelectCommand = "select Id,FromDate,ToDate,b.StatusName as Status,Type,(CASE WHEN Active='Y' then 'Yes' else 'No' END) as Active from SeedMoneyActive a,Status_Seedmoney_M b where a.Status=b.StatusId and Type='F'";
        GridViewSeedMoney.DataSourceID = "SqlDataSourceseedMoney";
        //SqlDataSourceseedMoney.DataBind();
        GridViewSeedMoney.DataBind();
        ModalPopupExtender3.Show();
       
    }
    //protected void setModalWindow(object sender, EventArgs e)
    //{
    //    popupPanelBaCode.Visible = true;
    //    SqlDataSourceUSer.SelectCommand = "";
    //    GridViewFaculty.DataSourceID = "SqlDataSourceUSer";
    //    SqlDataSourceUSer.DataBind();
    //    GridViewFaculty.DataBind();

    //}   
    protected void searchid_Click(object sender, EventArgs e)
    {

    }
    protected void GridViewStudent_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (senderID.Value.Contains("ImageButton1"))
        {

            GridViewStudent.Visible = true;
            GridViewRow row = GridViewStudent.SelectedRow;

            string UserId = row.Cells[1].Text;
            string Amount = row.Cells[2].Text;


            //txtuserid.Text = UserId;

            GridViewFaculty.DataBind();
            //DeptcodeTextChanged(sender, e);
        }

    }
    //protected void GridViewFaculty_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (senderID.Value.Contains("imageBkCbtn"))
    //    {
           
    //        GridViewFaculty.Visible = true;
    //        GridViewRow row = GridViewFaculty.SelectedRow;
    //        string[] result = null;
    //        string tempiitems = null;
    //        string UserId = row.Cells[1].Text;
    //        string Amount = row.Cells[2].Text;
    //       SqlDataSourceCheckboxCheckboxCategory.SelectCommand = " select BudgetId,Amount from SeedMoneyBudget_M where BudgetId='" + UserId + "'";
    //       CheckboxCategory.DataSourceID="SqlDataSourceCheckboxCheckboxCategory";
    //       CheckboxCategory.DataBind();
    //       //CheckboxCategory.SelectedValue = UserId;

    //        //CheckboxCategory.DataSourceID = UserId;
    //        CheckboxCategory.Visible = true;

    //        string items = string.Empty;
         
    //        foreach (ListItem i in CheckboxCategory.Items)
    //        {                               
    //            i.Selected = true;
             
    //        }
    //        ArrayList listFaculty1 = new ArrayList();
    //        //listFaculty1.Clear();
    //        for (int i = 0; i < CheckboxCategory.Items.Count; i++)
    //        {
    //            string chkValue = null;
    //            if (CheckboxCategory.Items[i].Selected == true)
    //            {

    //                chkValue = CheckboxCategory.Items[i].Value;
    //                listFaculty1.Add(chkValue);             
                   
    //                if (Session["InstallationId"] != null)
    //                {
    //                    ArrayList listFaculty2 = (ArrayList)Session["InstallationId"];
    //                    //listFaculty2.Add(chkValue);
    //                    for (int j = 0; j < listFaculty2.Count; j++)
    //                    {
    //                        Session["InstallationId2"] = listFaculty2;
    //                    }
    //                }
    //                else if (Session["InstallationId"] == null)
    //                {
    //                    Session["InstallationId"] = listFaculty1;
    //                }
    //                if (CheckboxCategory.Items.Count == 1)
    //                {
                        
    //                       tempiitems = chkValue;
                          
    //                        result = tempiitems.Split(new char[] { ',' }).ToArray();
                       
    //                        string[] a = result;
    //                        Session["values"] = a;
    //                        Session["values1"] = a;                                                       
    //                               if (Session["values1"] != null)
    //                                {
    //                                    string[] b = (string[])Session["values1"];
    //                                    Session["values2"] = b;
    //                                    //result = tempiitems.Split(new char[] { ',' }).ToArray();
    //                                }
    //                               if (Session["values2"] != null)
    //                               { 

    //                               }
    //                        }
                          
                      

    //                }
    //                else
    //                {
    //                    tempiitems =tempiitems+','+ chkValue;                     
    //                }

    //                //Session["tempiitems1"] = Session["tempiitems"].ToString();
    //                result = tempiitems.Split(new char[] { ',' }).ToArray();
    //            }
               
    //        SqlDataSourceFaculty.SelectCommand = "select BudgetId,Amount from SeedMoneyBudget_M where BudgetId !='" + UserId + "' and Type='F'" ;
    //        GridViewFaculty.DataSourceID = "SqlDataSourceFaculty";
    //        SqlDataSourceFaculty.DataBind();
    //        GridViewFaculty.DataBind();
          

            
    //    }
    //}
    protected void Button2_Click(object sender, EventArgs e)
    {

        ArrayList FAmount = new ArrayList();
        for (int i = 0; i < GridViewFaculty.Rows.Count; i++)
        {
            CheckBox Checkboxfaculty = (CheckBox)GridViewFaculty.Rows[i].FindControl("cbSelect");
            Label BudgetId = (Label)GridViewFaculty.Rows[i].FindControl("TextBoxFBudgetId");
            Label Amount = (Label)GridViewFaculty.Rows[i].FindControl("TextBoxFAmount");
          
            if (Checkboxfaculty.Checked == true)
            {
                string id = BudgetId.Text;
                FAmount.Add(id);
                string amount = Amount.Text;
            }
            
        }
        if (FAmount.Count == 0)
        {
            string CloseWindow = "alert('Please enter the Category amount for Faculty!')";
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow, true);
            return;
        }
        string amountf = "";
        for (int j = 0; j < FAmount.Count; j++)
            {
                if (j == 0)
                {
                    amountf = FAmount[j].ToString();
                }
                else
                {
                    amountf = amountf + ',' + FAmount[j].ToString();
                }
            }
        SqlDataSourceFacultyCategoty.SelectCommand = "select Amount,BudgetId from SeedMoneyBudget_M where Type='F' and BudgetId in("+amountf+")";
        GridViewFacultyCategoty.DataSourceID = "SqlDataSourceFacultyCategoty";
        GridViewFacultyCategoty.DataBind();
        GridViewFacultyCategoty.Visible = true;
        //for (int i = 0; i < GridViewFacultyCategoty.Rows.Count; i++)
        //{
        //    GridViewFacultyCategoty.HeaderRow.Cells(1).Visible = False;
        //}
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        ArrayList SAmount = new ArrayList();
        for (int i = 0; i < GridViewStudent.Rows.Count; i++)
        {
            CheckBox Checkboxfaculty = (CheckBox)GridViewStudent.Rows[i].FindControl("csSelect");
            Label BudgetId = (Label)GridViewStudent.Rows[i].FindControl("TextBoxSBudgetId");
            Label Amount = (Label)GridViewStudent.Rows[i].FindControl("TextBoxSAmount");

            if (Checkboxfaculty.Checked == true)
            {
                string id = BudgetId.Text;
                SAmount.Add(id);
                string amount = Amount.Text;
            }

        }
        if (SAmount.Count == 0)
        {
            string CloseWindow = "alert('Please enter the Category amount for Student!')";
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
        }
        SqlDataSourceStudentCategoty.SelectCommand = "select Amount,BudgetId from SeedMoneyBudget_M where Type='S' and BudgetId in(" + amountf + ")";
        GridViewstudentCategoty.DataSourceID = "SqlDataSourceStudentCategoty";
        GridViewstudentCategoty.DataBind();
        GridViewstudentCategoty.Visible = true;
    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        if (!Page.IsValid)
        {
            return;
        }
        try
        {
            string confirmValue2 = Request.Form["confirm_value2"];
            if (confirmValue2 == "Yes")
            {
                ArrayList listFaculty = new ArrayList();
                ArrayList listStudent = new ArrayList();
                if (TextBoxRemarks.Text == "")
                {
                    string CloseWindow = "alert('Please enter the Remarks!')";
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow, true);
                    return;
                }
                if (DropDownListAuthorType.SelectedValue == "B")
                {
                    if ((GridViewFacultyCategoty.Rows.Count == 0) && (GridViewstudentCategoty.Rows.Count == 0))
                    {
                        string CloseWindow = "alert('Please enter the Category amount for Student and Faculty!')";
                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow, true);
                        return;
                    }
                    else if ((GridViewFacultyCategoty.Rows.Count != 0) && (GridViewstudentCategoty.Rows.Count == 0))
                    {
                        string CloseWindow = "alert('Please enter the Category amount for Student!')";
                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow, true);
                        return;
                    }
                    else if ((GridViewFacultyCategoty.Rows.Count == 0) && (GridViewstudentCategoty.Rows.Count != 0))
                    {
                        string CloseWindow = "alert('Please enter the Category amount for Faculty!')";
                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow, true);
                        return;
                    }
                }
                else if (DropDownListAuthorType.SelectedValue == "F")
                {

                    if (GridViewFacultyCategoty.Rows.Count == 0)
                    {
                        string CloseWindow = "alert('Please enter the Category amount for Faculty!')";
                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow, true);
                        return;
                    }
                }
                else if (DropDownListAuthorType.SelectedValue == "S")
                {
                    if (GridViewstudentCategoty.Rows.Count == 0)
                    {
                        string CloseWindow = "alert('Please enter the Category amount for Student!')";
                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow, true);
                        return;
                    }
                }
                //if (DropDownListactive.SelectedValue == "N")
                //{
                //    if (TextBoxNote.Text == "")
                //    {
                //        string CloseWindow = "alert('Please enter the Message to disable the Seed Money Entry!')";
                //        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow, true);
                //        return;
                //    }
                //}
                //else 
                //{
                //    //if (DropDownListAuthorType.SelectedValue == "F")
                //    //{
                //    //    CustomValidator1.Enabled = true;
                //    //    CustomValidator2.Enabled = false;
                //    //}
                //    //else if (DropDownListAuthorType.SelectedValue == "S")
                //    //{
                //    //    CustomValidator1.Enabled = false;
                //    //    CustomValidator2.Enabled = true;

                //    //}
                //    //else
                //    //{
                //    //    CustomValidator1.Enabled = true;
                //    //    CustomValidator2.Enabled = true;
                //    //}
                //}
                SeedMoney a = new SeedMoney();
                Business b = new Business();
                string category = DropDownListAuthorType.SelectedValue.Trim();
                string active = DropDownListactive.SelectedValue.Trim();
                string remarks = TextBoxRemarks.Text.Trim();
                //string Note = TextBoxNote.Text.Trim();
                string updatedby = Session["UserId"].ToString();
                DateTime FromDate = Convert.ToDateTime(TextBoxFromDate.Text.Trim());
                DateTime ToDate = Convert.ToDateTime(TextBoxToDate.Text.Trim());
                DateTime updateddate = DateTime.Now;
                string authorType = DropDownListAuthorType.SelectedValue.Trim();


                if (DropDownListAuthorType.SelectedValue == "F")
                {

                    for (int i = 0; i < GridViewFacultyCategoty.Rows.Count; i++)
                    {
                        Label BudgetId = (Label)GridViewFacultyCategoty.Rows[i].FindControl("TextBoxFacultyBudgetId");
                        Label Amount = (Label)GridViewFacultyCategoty.Rows[i].FindControl("TextBoxFBudgetAmount");
                        string id = BudgetId.Text;
                        listFaculty.Add(id);
                    }
                }
                else if (DropDownListAuthorType.SelectedValue == "S")
                {
                    for (int i = 0; i < GridViewstudentCategoty.Rows.Count; i++)
                    {
                        Label BudgetId = (Label)GridViewstudentCategoty.Rows[i].FindControl("TextBoxStudentBudgetId");
                        Label Amount = (Label)GridViewstudentCategoty.Rows[i].FindControl("TextBoxSBudgetAmount");
                        string id = BudgetId.Text;
                        listFaculty.Add(id);
                    }
                }
                else if (DropDownListAuthorType.SelectedValue == "B")
                {

                    for (int i = 0; i < GridViewFacultyCategoty.Rows.Count; i++)
                    {
                        Label BudgetId = (Label)GridViewFacultyCategoty.Rows[i].FindControl("TextBoxFacultyBudgetId");
                        Label Amount = (Label)GridViewFacultyCategoty.Rows[i].FindControl("TextBoxFBudgetAmount");
                        string id = BudgetId.Text;
                        listFaculty.Add(id);
                    }
                    for (int i = 0; i < GridViewstudentCategoty.Rows.Count; i++)
                    {
                        Label BudgetId = (Label)GridViewstudentCategoty.Rows[i].FindControl("TextBoxStudentBudgetId");
                        Label Amount = (Label)GridViewstudentCategoty.Rows[i].FindControl("TextBoxSBudgetAmount");
                        string id = BudgetId.Text;
                        listFaculty.Add(id);
                    }
                }
                //if (DropDownListAuthorType.SelectedValue == "B")
                //{
                //    listFaculty.Clear();
                //    for (int i = 0; i < CheckboxCategory.Items.Count; i++)
                //    {
                //        string chkValue = null;
                //        if (CheckboxCategory.Items[i].Selected == true)
                //        {
                //            chkValue = CheckboxCategory.Items[i].Value;
                //            listFaculty.Add(chkValue);
                //        }
                //    }
                //    for (int i = 0; i < CheckboxCategory1.Items.Count; i++)
                //    {
                //        string chkValue1 = null;
                //        if (CheckboxCategory1.Items[i].Selected == true)
                //        {
                //            chkValue1 = CheckboxCategory1.Items[i].Value;
                //            listFaculty.Add(chkValue1);
                //        }
                //    }
                //}
                //else
                //    if (DropDownListAuthorType.SelectedValue == "F")
                //    {
                //        listFaculty.Clear();
                //        for (int i = 0; i < CheckboxCategory.Items.Count; i++)
                //        {
                //            string chkValue = null;
                //            if (CheckboxCategory.Items[i].Selected == true)
                //            {
                //                chkValue = CheckboxCategory.Items[i].Value;
                //                listFaculty.Add(chkValue);
                //            }
                //        }
                //    }
                //    else
                //        if (DropDownListAuthorType.SelectedValue == "S")
                //        {
                //            listFaculty.Clear();
                //            for (int i = 0; i < CheckboxCategory1.Items.Count; i++)
                //            {
                //                string chkValue1 = null;
                //                if (CheckboxCategory1.Items[i].Selected == true)
                //                {
                //                    chkValue1 = CheckboxCategory1.Items[i].Value;
                //                    listFaculty.Add(chkValue1);
                //                }
                //            }
                //        }
                a.Entrytype = category;
                a.Enable = "Y";
                a.EnableRemarks = remarks;
                //a.Note = Note;
                a.UpdatedBy = updatedby;
                a.EnableDate = Convert.ToDateTime(updateddate);
                a.Fromdate = Convert.ToDateTime(FromDate);
                a.Todate = Convert.ToDateTime(ToDate);
                a.Status = "APP";
                a.id = Convert.ToInt32(TextBox1.Text.Trim());
                int result = 0;
                result = b.disableSeedMoneyEntry(DropDownListAuthorType.SelectedValue);
                string Fromdate1 = FromDate.ToString("yyyy-MM-dd");
                string ToDate1 = ToDate.ToString("yyyy-MM-dd");
                //ArrayList IDList = new ArrayList();
                //IDList = b.checkexistingCycleUpdatestatus(Fromdate1, ToDate1, a.Entrytype);
                //if (IDList.Count > 0)
                //{
                //    for (int i = 0; i < IDList.Count; i++)
                //    {
                //        result = b.UpdateexistingcycleStatusSeedMoneyEntry(DropDownListAuthorType.SelectedValue, IDList[i].ToString());
                //    }
                //}
                //if (TextBox1.Text != "")
                //{
                //    a.id = Convert.ToInt32(TextBox1.Text.Trim());
                //}
                //if (TextBox1.Text == "")
                //{
                //    result = b.EnableSeedMoneyEntry(a, listFaculty);
                //}
                //else
                //{
                    result = b.UpdateManageSeedMoneyEntry(a, listFaculty);
                //}
                if (result >= 1)
                {
                    string CloseWindow = "alert('Seed Money cycle Approved succesfully for: " + DropDownListAuthorType.SelectedItem + "')";
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow, true);
                    Button5.Visible = true;
                    Clear();
                }
                else
                {
                    string CloseWindow = "alert('Error!')";
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow, true);
                }
            }
            else if (confirmValue2 == "No")
                 {

                 }
            else if ((confirmValue2 == "") || (confirmValue2 == null))
            {

                ArrayList listFaculty = new ArrayList();
                ArrayList listStudent = new ArrayList();
                if (TextBoxRemarks.Text == "")
                {
                    string CloseWindow = "alert('Please enter the Remarks!')";
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow, true);
                    return;
                }
                if (DropDownListAuthorType.SelectedValue == "B")
                {
                    if ((GridViewFacultyCategoty.Rows.Count == 0) && (GridViewstudentCategoty.Rows.Count == 0))
                    {
                        string CloseWindow = "alert('Please enter the Category amount for Student and Faculty!')";
                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow, true);
                        return;
                    }
                    else if ((GridViewFacultyCategoty.Rows.Count != 0) && (GridViewstudentCategoty.Rows.Count == 0))
                    {
                        string CloseWindow = "alert('Please enter the Category amount for Student!')";
                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow, true);
                        return;
                    }
                    else if ((GridViewFacultyCategoty.Rows.Count == 0) && (GridViewstudentCategoty.Rows.Count != 0))
                    {
                        string CloseWindow = "alert('Please enter the Category amount for Faculty!')";
                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow, true);
                        return;
                    }
                }
                else if (DropDownListAuthorType.SelectedValue == "F")
                {

                    if (GridViewFacultyCategoty.Rows.Count == 0)
                    {
                        string CloseWindow = "alert('Please enter the Category amount for Faculty!')";
                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow, true);
                        return;
                    }
                }
                else if (DropDownListAuthorType.SelectedValue == "S")
                {
                    if (GridViewstudentCategoty.Rows.Count == 0)
                    {
                        string CloseWindow = "alert('Please enter the Category amount for Student!')";
                        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow, true);
                        return;
                    }
                }
                //if (DropDownListactive.SelectedValue == "N")
                //{
                //    if (TextBoxNote.Text == "")
                //    {
                //        string CloseWindow = "alert('Please enter the Message to disable the Seed Money Entry!')";
                //        ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow, true);
                //        return;
                //    }
                //}
                //else 
                //{
                //    //if (DropDownListAuthorType.SelectedValue == "F")
                //    //{
                //    //    CustomValidator1.Enabled = true;
                //    //    CustomValidator2.Enabled = false;
                //    //}
                //    //else if (DropDownListAuthorType.SelectedValue == "S")
                //    //{
                //    //    CustomValidator1.Enabled = false;
                //    //    CustomValidator2.Enabled = true;

                //    //}
                //    //else
                //    //{
                //    //    CustomValidator1.Enabled = true;
                //    //    CustomValidator2.Enabled = true;
                //    //}
                //}
                SeedMoney a = new SeedMoney();
                Business b = new Business();
                string category = DropDownListAuthorType.SelectedValue.Trim();
                string active = DropDownListactive.SelectedValue.Trim();
                string remarks = TextBoxRemarks.Text.Trim();
                //string Note = TextBoxNote.Text.Trim();
                string updatedby = Session["UserId"].ToString();
                DateTime FromDate = Convert.ToDateTime(TextBoxFromDate.Text.Trim());
                DateTime ToDate = Convert.ToDateTime(TextBoxToDate.Text.Trim());
                DateTime updateddate = DateTime.Now;
                string authorType = DropDownListAuthorType.SelectedValue.Trim();


                if (DropDownListAuthorType.SelectedValue == "F")
                {

                    for (int i = 0; i < GridViewFacultyCategoty.Rows.Count; i++)
                    {
                        Label BudgetId = (Label)GridViewFacultyCategoty.Rows[i].FindControl("TextBoxFacultyBudgetId");
                        Label Amount = (Label)GridViewFacultyCategoty.Rows[i].FindControl("TextBoxFBudgetAmount");
                        string id = BudgetId.Text;
                        listFaculty.Add(id);
                    }
                }
                else if (DropDownListAuthorType.SelectedValue == "S")
                {
                    for (int i = 0; i < GridViewstudentCategoty.Rows.Count; i++)
                    {
                        Label BudgetId = (Label)GridViewstudentCategoty.Rows[i].FindControl("TextBoxStudentBudgetId");
                        Label Amount = (Label)GridViewstudentCategoty.Rows[i].FindControl("TextBoxSBudgetAmount");
                        string id = BudgetId.Text;
                        listFaculty.Add(id);
                    }
                }
                else if (DropDownListAuthorType.SelectedValue == "B")
                {

                    for (int i = 0; i < GridViewFacultyCategoty.Rows.Count; i++)
                    {
                        Label BudgetId = (Label)GridViewFacultyCategoty.Rows[i].FindControl("TextBoxFacultyBudgetId");
                        Label Amount = (Label)GridViewFacultyCategoty.Rows[i].FindControl("TextBoxFBudgetAmount");
                        string id = BudgetId.Text;
                        listFaculty.Add(id);
                    }
                    for (int i = 0; i < GridViewstudentCategoty.Rows.Count; i++)
                    {
                        Label BudgetId = (Label)GridViewstudentCategoty.Rows[i].FindControl("TextBoxStudentBudgetId");
                        Label Amount = (Label)GridViewstudentCategoty.Rows[i].FindControl("TextBoxSBudgetAmount");
                        string id = BudgetId.Text;
                        listFaculty.Add(id);
                    }
                }
                //if (DropDownListAuthorType.SelectedValue == "B")
                //{
                //    listFaculty.Clear();
                //    for (int i = 0; i < CheckboxCategory.Items.Count; i++)
                //    {
                //        string chkValue = null;
                //        if (CheckboxCategory.Items[i].Selected == true)
                //        {
                //            chkValue = CheckboxCategory.Items[i].Value;
                //            listFaculty.Add(chkValue);
                //        }
                //    }
                //    for (int i = 0; i < CheckboxCategory1.Items.Count; i++)
                //    {
                //        string chkValue1 = null;
                //        if (CheckboxCategory1.Items[i].Selected == true)
                //        {
                //            chkValue1 = CheckboxCategory1.Items[i].Value;
                //            listFaculty.Add(chkValue1);
                //        }
                //    }
                //}
                //else
                //    if (DropDownListAuthorType.SelectedValue == "F")
                //    {
                //        listFaculty.Clear();
                //        for (int i = 0; i < CheckboxCategory.Items.Count; i++)
                //        {
                //            string chkValue = null;
                //            if (CheckboxCategory.Items[i].Selected == true)
                //            {
                //                chkValue = CheckboxCategory.Items[i].Value;
                //                listFaculty.Add(chkValue);
                //            }
                //        }
                //    }
                //    else
                //        if (DropDownListAuthorType.SelectedValue == "S")
                //        {
                //            listFaculty.Clear();
                //            for (int i = 0; i < CheckboxCategory1.Items.Count; i++)
                //            {
                //                string chkValue1 = null;
                //                if (CheckboxCategory1.Items[i].Selected == true)
                //                {
                //                    chkValue1 = CheckboxCategory1.Items[i].Value;
                //                    listFaculty.Add(chkValue1);
                //                }
                //            }
                //        }
                a.Entrytype = category;
                a.Enable = "Y";
                a.EnableRemarks = remarks;
                //a.Note = Note;
                a.UpdatedBy = updatedby;
                a.EnableDate = Convert.ToDateTime(updateddate);
                a.Fromdate = Convert.ToDateTime(FromDate);
                a.Todate = Convert.ToDateTime(ToDate);
                a.Status = "APP";
                int result = 0;
                a.id = Convert.ToInt32(TextBox1.Text.Trim());
                //if (TextBox1.Text != "")
                //{
                //    a.id = Convert.ToInt32(TextBox1.Text.Trim());
                //}
                //if (TextBox1.Text == "")
                //{
                //    result = b.EnableSeedMoneyEntry(a, listFaculty);
                //}
                //else
                //{
                    result = b.UpdateManageSeedMoneyEntry(a, listFaculty);
                //}
                if (result >= 1)
                {
                    string CloseWindow = "alert('Seed Money cycle Approved succesfully for: " + DropDownListAuthorType.SelectedItem + "')";
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow, true);
                    Button5.Visible = true;
                    Clear();
                }
                else
                {
                    string CloseWindow = "alert('Error!')";
                    ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow, true);
                }
            }
        }
        catch (Exception ex)
        {
            log.Error(ex.StackTrace);
            log.Error(ex.Message);

            log.Error("Error!!!!!!!!!!!!!!!! ");

            ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Error!!!!!!!!!!')</script>");

        }

    }

    private void Clear()
    {
        GridViewFacultyCategoty.Visible = false;
        TextBoxRemarks.Text = "";
        TextBox1.Text = "";
    }
    protected void GridViewSeedMoney_SelectedIndexChanged(object sender, EventArgs e)
    {
        popupPanelFaculty.Visible = false;
        popupPanelstudent.Visible = false;
        DropDownListAuthorType.Enabled = false;


            GridViewSeedMoney.Visible = true;
            GridViewRow row = GridViewSeedMoney.SelectedRow;              
            string country = (GridViewSeedMoney.SelectedRow.FindControl("LabelSId") as Label).Text;
            TextBox1.Text = country;



            string Fdate = (GridViewSeedMoney.SelectedRow.FindControl("LabelSFromDate") as Label).Text;
            TextBoxFromDate.Text = Fdate;
            string Tdate = (GridViewSeedMoney.SelectedRow.FindControl("LabelSToDate") as Label).Text;
            TextBoxToDate.Text = Tdate;
            string Status = (GridViewSeedMoney.SelectedRow.FindControl("LabelSStatus") as Label).Text;

            string Type = (GridViewSeedMoney.SelectedRow.FindControl("LabelSType") as Label).Text;
            DropDownListAuthorType.SelectedValue = Type;

            if (Type == "B")
            {
                SqlDataSourceFacultyCategoty.SelectCommand = "select Amount,BudgetId from SeedMoneyBudget_M a,SeedMoneyCatagoryMap b  where a.BudgetId=b.categotyid and Type='F' and id in(" + country + ")";
                GridViewFacultyCategoty.DataSourceID = "SqlDataSourceFacultyCategoty";
                GridViewFacultyCategoty.DataBind();
                GridViewFacultyCategoty.Visible = true;

                SqlDataSourceStudentCategoty.SelectCommand = "select Amount,BudgetId from SeedMoneyBudget_M a,SeedMoneyCatagoryMap b  where a.BudgetId=b.categotyid and Type='S' and id in(" + country + ")";
                GridViewstudentCategoty.DataSourceID = "SqlDataSourceStudentCategoty";
                GridViewstudentCategoty.DataBind();
                GridViewstudentCategoty.Visible = true;
            }
            else if (Type == "F")
            {
                Label3.Visible = true;
                imageBkCbtn.Visible = true;
                Label4.Visible = false;
                ImageButton1.Visible = false;
                GridViewstudentCategoty.Visible = false;
                SqlDataSourceFacultyCategoty.SelectCommand = "select Amount,BudgetId from SeedMoneyBudget_M a,SeedMoneyCatagoryMap b  where a.BudgetId=b.categotyid and Type='F' and id in(" + country + ")";
                GridViewFacultyCategoty.DataSourceID = "SqlDataSourceFacultyCategoty";
                GridViewFacultyCategoty.DataBind();
                GridViewFacultyCategoty.Visible = true;
            }
            else if (Type == "S")
            {
                Label3.Visible = false;
                imageBkCbtn.Visible = false;
                Label4.Visible = true;
                ImageButton1.Visible = true;
                GridViewFacultyCategoty.Visible = false;
                SqlDataSourceStudentCategoty.SelectCommand = "select Amount,BudgetId from SeedMoneyBudget_M a,SeedMoneyCatagoryMap b  where a.BudgetId=b.categotyid and Type='S' and id in(" + country + ")";
                GridViewstudentCategoty.DataSourceID = "SqlDataSourceStudentCategoty";
                GridViewstudentCategoty.DataBind();
                GridViewstudentCategoty.Visible = true;
            }
            string Active = (GridViewSeedMoney.SelectedRow.FindControl("LabelSActive") as Label).Text;
            if (Status == "Yet to be Submitted")
            {
                Button1.Visible = true;
                Button5.Visible = true;
            }
            else if(Status == "APP")
            {
                Button1.Visible = false;
                Button5.Visible = false;
            }
            else
            {
                Button1.Visible = false;
                Button5.Visible = false;
            }
            GridViewSeedMoney.DataBind();
            PanelseedMoney.Visible = true;
            SqlDataSourceseedMoney.SelectCommand = "select Id,FromDate,ToDate,b.StatusName as Status,Type,(CASE WHEN Active='Y' then 'Yes' else 'No' END) as Active from SeedMoneyActive a,Status_Seedmoney_M b where a.Status=b.StatusId and Type='F'";
            GridViewSeedMoney.DataSourceID = "SqlDataSourceseedMoney";
            //SqlDataSourceseedMoney.DataBind();
            GridViewSeedMoney.DataBind();
          
       
    }
    protected void RadioButtonListUserType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RadioButtonListUserType.SelectedValue == "A")
        {
            Button1.Visible = true;
            Button5.Visible = false;
            ImageButton2.Visible = false;
            initialsetup();
            initialrow();
            TextBox1.Text = "";
            TextBoxRemarks.Text = "";
            DateTime time1 = DateTime.Now;
            string date1 = time1.ToShortDateString();
         
            TextBoxFromDate.Text = Convert.ToString(date1);
            TextBoxToDate.Text = Convert.ToString(date1);
            StatusofSeedMoney.Value = RadioButtonListUserType.SelectedValue;
            DropDownListAuthorType.Enabled=true;
            SqlDataSourceFacultyCategoty.SelectCommand = "select Amount,BudgetId from SeedMoneyBudget_M where Type='F' and BudgetId in(1,2)";
            GridViewFacultyCategoty.DataSourceID = "SqlDataSourceFacultyCategoty";
            GridViewFacultyCategoty.DataBind();
            GridViewFacultyCategoty.Visible = true;
            ArrayList FAmount = new ArrayList();
        }
        else
        {
            Button1.Visible = true;
            Button5.Visible = false;
            ImageButton2.Visible = true;
            initialsetup();
            initialrow();
            TextBox1.Text = "";
            TextBoxRemarks.Text = "";
            DateTime time1 = DateTime.Now;
            string date1 = time1.ToShortDateString();
            GridViewFacultyCategoty.Visible = false;
            TextBoxFromDate.Text = Convert.ToString(date1);
            TextBoxToDate.Text = Convert.ToString(date1);
            StatusofSeedMoney.Value = RadioButtonListUserType.SelectedValue;
            DropDownListAuthorType.Enabled = false;
        }
    }

    protected void GridViewSeedMoney_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
        }
    }
    //protected void lnkSeedMoneyFaculty_Click(object sender, EventArgs e)
    //{

    //    PanelseedMoney.Visible = true;
    //    SqlDataSourceseedMoney.SelectCommand = "select Id,FromDate,ToDate,Status,Type,Active from SeedMoneyActive where  Type='F'";
    //    GridViewSeedMoney.DataSourceID = "SqlDataSourceseedMoney";
    //    //SqlDataSourceseedMoney.DataBind();
    //    GridViewSeedMoney.DataBind();
    //    ModalPopupExtender4.Show();
    //}
}