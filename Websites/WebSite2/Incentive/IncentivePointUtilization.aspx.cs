using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Incentive_IncentivePointUtilization : System.Web.UI.Page
{
    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    IncentiveBusiness B = new IncentiveBusiness();
    IncentivePoint obj = new IncentivePoint();
    public string pageID = "L109";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!Session["authPage"].ToString().Contains("$" + pageID + "$"))
            {
                string unacces = "Unauthorized Acces!!! Login Again";
                Response.Redirect("~/Login.aspx?val=" + unacces);
            }
            CompareValidator1.ValueToCompare = DateTime.Now.ToString("dd/MM/yyyy");
            setModalWindow(sender, e);
        }
    }

    private void setModalWindow(object sender, EventArgs e)
    {
      
        string inst = Session["InstituteId"].ToString();
        string member = txtmidSearch.Text.ToString();
        IncentiveBusiness obj = new IncentiveBusiness();
        string membername = "";
        string role = Session["Role"].ToString();
        string UserId = Session["UserId"].ToString();
        ArrayList InstitutionList = new ArrayList();
        InstitutionList = obj.selectHrAdditionalInstitute(UserId);
        if (InstitutionList.Count != 0)
        {          
           InstitutionList.Add(inst);           
        }
        if (role == "7")
        {
            if (InstitutionList.Count == 0)
            {
                DataSet ds = null;

                if (member != "")
                {
                    if (ds.Tables.Count > 0)
                    {

                        ds = obj.SelectMembersInstitutewise(inst, member, membername);
                        Panel1.Visible = true;
                        popGridSearch.DataSource = ds;
                        popGridSearch.DataBind();
                        popGridSearch.Visible = true;
                    }
                }
                else
                {

                    ds = obj.SelectMembersInstitute(inst);
                    Panel1.Visible = true;
                    popGridSearch.DataSource = ds;
                    popGridSearch.DataBind();
                    popGridSearch.Visible = true;
                }
            }
            else 
            {
                DataSet ds = null;

                if (member != "")
                {
                    if (ds.Tables.Count > 0)
                    {

                        ds = obj.SelectMembersAdditionalInstitutewise(UserId, member, membername);
                        Panel1.Visible = true;
                        popGridSearch.DataSource = ds;
                        popGridSearch.DataBind();
                        popGridSearch.Visible = true;
                    }
                }
                else
                {

                    ds = obj.SelectMembersAdditionalInstitute(UserId);
                    Panel1.Visible = true;
                    popGridSearch.DataSource = ds;
                    popGridSearch.DataBind();
                    popGridSearch.Visible = true;
                }
            }
        }
        else if (role == "17")
        {

            DataSet ds1 = null;
            if (member != "")
            {
                if (ds1.Tables.Count > 0)
                {
                    ds1 = obj.SelectFacultyInstitutewise(inst, member, membername);
                    Panel1.Visible = true;
                    popGridSearch.DataSource = ds1;
                    popGridSearch.DataBind();
                    popGridSearch.Visible = true;
                }
            }
            else
            {

                ds1 = obj.SelectFacultyInstitute(inst);
                Panel1.Visible = true;
                popGridSearch.DataSource = ds1;
                popGridSearch.DataBind();
                popGridSearch.Visible = true;
            }
        }
        else
        {

            DataSet ds2 = null;
            if (member != "")
            {

                //    ds2 = obj.SelectStudentInstitutewise(inst, member, membername);
                //    Panel1.Visible = true;
                //    popGridSearch.DataSource = ds2;
                //    popGridSearch.DataBind();
                //    popGridSearch.Visible = true;
                //}

                if (ds2.Tables.Count > 0)
                {
                    ds2 = obj.SelectStudentInstitutewise(inst, member, membername);
                    Panel1.Visible = true;
                    popGridSearch.DataSource = ds2;
                    popGridSearch.DataBind();
                    popGridSearch.Visible = true;

                }
            }
            else
            {
                ds2 = obj.SelectStudentInstitute(inst);

                Panel1.Visible = true;
                popGridSearch.DataSource = ds2;
                popGridSearch.DataBind();
                popGridSearch.Visible = true;
            }
        }
          
    }
    protected void showPop(object sender, EventArgs e)
    {
        ModalPopupExtender2.Show();
    }
    protected void popSelected(Object sender, EventArgs e)
    {
        txtOldBalance.Text = "";
        txtID.Text = "";
        txtCurrentBalance.Text = "";
        txtUtilization.Text = "";
        txtRemarks.Text = "";
        btnSave.Enabled = true;
        popGridSearch.Visible = true;
        GridViewRow row = popGridSearch.SelectedRow;
        string memberid = row.Cells[1].Text;
        txtID.Text = row.Cells[1].Text;

        txtMembername.Text = HttpUtility.HtmlDecode(row.Cells[2].Text);
        //string inst = row.Cells[3].Text;
        IncentiveBusiness B = new IncentiveBusiness();
        IncentivePoint obj = new IncentivePoint();
        obj = B.SelectMemberCurBalance(memberid);
        txtCurrentBalance.Text = obj.CurrentBalance.ToString();
        txtOldBalance.Text = obj.OpeningBalance.ToString();
        txtUtilizationDate.Text = DateTime.Now.ToShortDateString();
        //string inst = Session["InstituteId"].ToString();
        string UserId = Session["UserId"].ToString();
        string member = row.Cells[1].Text;
        string membername = row.Cells[2].Text;
        string inst = row.Cells[3].Text;
        string role = Session["Role"].ToString();    
        if (role == "7")
        {
            DataSet ds = B.SelectMembersInstitutewise(inst, member, membername);
            Panel1.Visible = true;
            popGridSearch.DataSource = ds;
            popGridSearch.DataBind();
            popGridSearch.Visible = true;           
        }
        else if (role == "17")
        {
            DataSet ds1 = B.SelectFacultyInstitutewise(inst, member, membername);
            Panel1.Visible = true;
            popGridSearch.DataSource = ds1;
            popGridSearch.DataBind();
            popGridSearch.Visible = true;
        }
        else
        {

            DataSet ds2 = B.SelectStudentInstitutewise(inst, member, membername);
            Panel1.Visible = true;
            popGridSearch.DataSource = ds2;
            popGridSearch.DataBind();
            popGridSearch.Visible = true;
        }
        Panel4.Visible = true;
        GridView2.Visible = true;
        Label1.Visible = true;

        DataTable dt = new DataTable();
        dt = B.CountUtilizationPoints(memberid);
        if (dt.Rows.Count > 0)
        {
            GridView2.Columns[2].FooterText = "Total";
            GridView2.Columns[3].FooterText = dt.Rows[0]["count1"].ToString();
            GridView2.Columns[4].FooterText = dt.Rows[0]["count2"].ToString();
            GridView2.DataSource = dt;
            GridView2.DataBind();
        }
        {
            GridView2.DataBind();
        }
    }
    protected void popGridIncenAdjust_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        popGridSearch.Visible = true;
        GridViewRow row = popGridSearch.SelectedRow;
        string memberid = row.Cells[1].Text;
        txtID.Text = memberid;
        popGridSearch.DataBind();

    }
    protected void btnSearch_Click(object sender, EventArgs e)
     {

        string inst = Session["InstituteId"].ToString();
        string member = txtmidSearch.Text.ToString();
        string membername = txtmnameSearch.Text.ToString();
        string role = Session["Role"].ToString();
        string UserId = Session["UserId"].ToString();
        IncentiveBusiness obj = new IncentiveBusiness();
        ArrayList InstitutionList = new ArrayList();
        InstitutionList = obj.selectHrAdditionalInstitute(UserId);
        if (InstitutionList.Count != 0)
        {         
        }
        if (role == "7")
        {
            if (InstitutionList.Count == 0)
            {
                DataSet ds = obj.SelectMembersInstitutewise(inst, member, membername);
                if (txtmidSearch.Text.Trim() == "")
                {

                    Panel1.Visible = true;
                    popGridSearch.DataSource = ds;
                    popGridSearch.DataBind();
                    popGridSearch.Visible = true;
                }
                else
                {
                    Panel1.Visible = true;
                    popGridSearch.DataSource = ds;
                    popGridSearch.DataBind();
                    popGridSearch.Visible = true;
                }
            }
            else
            {
                DataSet ds = obj.SelectMembersAdditionalInstitutewise(UserId, member, membername);
                if (txtmidSearch.Text.Trim() == "")
                {

                    Panel1.Visible = true;
                    popGridSearch.DataSource = ds;
                    popGridSearch.DataBind();
                    popGridSearch.Visible = true;
                }
                else
                {
                    Panel1.Visible = true;
                    popGridSearch.DataSource = ds;
                    popGridSearch.DataBind();
                    popGridSearch.Visible = true;
                }
            }
        }
        if (role == "17")
        {
            DataSet ds1 = obj.SelectFacultyInstitutewise(inst, member, membername);
            if (txtmidSearch.Text.Trim() == "")
            {

                Panel1.Visible = true;
                popGridSearch.DataSource = ds1;
                popGridSearch.DataBind();
                popGridSearch.Visible = true;
            }
            else
            {
                Panel1.Visible = true;
                popGridSearch.DataSource = ds1;
                popGridSearch.DataBind();
                popGridSearch.Visible = true;
            }
        }
        if (role == "18")
        {
            User u = new User();
            Business b = new Business();
            u = b.findmembername(member);
            if (u.MUNonMU != "M")
            {
                DataSet ds2 = obj.SelectStudentInstitutewise(inst, member, membername);
                if (txtmidSearch.Text.Trim() == "")
                {

                    Panel1.Visible = true;
                    popGridSearch.DataSource = ds2;
                    popGridSearch.DataBind();
                    popGridSearch.Visible = true;
                }
                else
                {
                    Panel1.Visible = true;
                    popGridSearch.DataSource = ds2;
                    popGridSearch.DataBind();
                    popGridSearch.Visible = true;
                }
            }

        }
       
        ModalPopupExtender2.Show();
    }
         

    protected void btnexit_Click(object sender, EventArgs e)
    {
        txtmidSearch.Text = "";
        popGridSearch.DataBind();
    }
    [System.Web.Services.WebMethod]
    public static double SelectMember(string Vid)
    {

        if (!(string.IsNullOrEmpty(Vid)))
        {
            IncentiveBusiness v = new IncentiveBusiness();
            PublishData obj = new PublishData();
            obj = v.SelectMemberDetails(Vid);
            if (obj.CurrentBalance != 0.0)
            {

                return obj.CurrentBalance;
            }
            else
            {
                return 0.00;

            }
        }
        else
        {
            return 0.00;
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
            if (ChkTypeOfUtilization.SelectedValue == "UTO")
            {
                if (txtOldBalance.Text == "0")
                {
                    string CloseWindow1 = "alert('Old scheme is zero.Cannot be used for Utilization')";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "newWindow", CloseWindow1, true);
                    return;
                }
                if (Convert.ToDouble(txtOldBalance.Text) < Convert.ToDouble(txtUtilization.Text.Trim()))
                {
                    string CloseWindow1 = "alert('Utilization point must be eqaul or less than old scheme')";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "newWindow", CloseWindow1, true);
                    return;
                }
            }
            else
            {
                if (txtCurrentBalance.Text == "0")
                {
                    string CloseWindow1 = "alert('Current  scheme is zero.Cannot be used for Utilization')";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "newWindow", CloseWindow1, true);
                    return;
                }
                if (Convert.ToDouble(txtCurrentBalance.Text) < Convert.ToDouble(txtUtilization.Text.Trim()))
                {
                    string CloseWindow1 = "alert('Utilization point must be eqaul or less than current scheme')";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "newWindow", CloseWindow1, true);
                    return;
                }
            }
            IncentivePoint obj = new IncentivePoint();
            obj.MemberId = txtID.Text.Trim();
            obj.Utilization = Convert.ToDouble(txtUtilization.Text.Trim());
            obj.Remarks = txtRemarks.Text.Trim();           
            if (ChkTypeOfUtilization.SelectedValue == "UTN")
            {
                obj.CurrentBalance = Convert.ToDouble(txtCurrentBalance.Text.Trim());
                obj.CurrentBalance = obj.CurrentBalance - obj.Utilization;
            }
            else
            {
                obj.CurrentBalance = Convert.ToDouble(txtOldBalance.Text.Trim());
                obj.CurrentBalance = obj.CurrentBalance - obj.Utilization;
            }
            obj.Utilization = Convert.ToDouble("-"+txtUtilization.Text.Trim());
            obj.TransactionType = ChkTypeOfUtilization.SelectedValue;
            obj.UtilizationType = ChkTypeOfUtilization.SelectedValue;
           // obj.UtilizationType = "UTN";
            obj.UtilizationDate = Convert.ToDateTime(txtUtilizationDate.Text.Trim());
            bool result1 = B.InsertUtilizationPoint(obj); //Business layer
            if (result1 == true)
            {
                string CloseWindow1 = "alert('Utilization point entered successfully')";
                //ScriptManager.RegisterStartupScript(EditUpdatePanel, EditUpdatePanel.GetType(), "alert", CloseWindow1, true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "newWindow", CloseWindow1, true);
                btnSave.Enabled = false;
                Label1.Visible = true;
                DataTable dt = new DataTable();
                dt = B.CountUtilizationPoints(txtID.Text.Trim());
                if (dt.Rows.Count > 0)
                {
                    GridView2.Columns[2].FooterText = "Total";
                    GridView2.Columns[3].FooterText = dt.Rows[0]["count1"].ToString();
                    GridView2.Columns[4].FooterText = dt.Rows[0]["count2"].ToString();
                    GridView2.DataSource = dt;
                    GridView2.DataBind();
                }
                {
                    GridView2.DataBind();
                }
                GridView2.Visible = true;
                if (ChkTypeOfUtilization.SelectedValue == "UTN")
                {
                    obj.CurrentBalance = Convert.ToDouble(txtCurrentBalance.Text.Trim());
                    txtCurrentBalance.Text = (obj.CurrentBalance - Convert.ToDouble(txtUtilization.Text.Trim())).ToString();
                }
                else
                {
                    obj.CurrentBalance = Convert.ToDouble(txtOldBalance.Text.Trim());
                    txtOldBalance.Text = (obj.CurrentBalance - Convert.ToDouble(txtUtilization.Text.Trim())).ToString();
                }
                txtUtilization.Text = "";
                txtRemarks.Text = "";
                txtUtilizationDate.Text = DateTime.Now.ToShortDateString();
            }
            else
            {
                string CloseWindow1 = "alert('Problem while saving data')";
                ScriptManager.RegisterStartupScript(EditUpdatePanel, EditUpdatePanel.GetType(), "alert", CloseWindow1, true);
                btnSave.Enabled = true;
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

    public void GridView_RowDataBound1(Object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //if (e.Row.Cells[3].Text == "&nbsp;" || e.Row.Cells[3].Text == "")
            //{
            //    e.Row.Cells[3].Text = "0";

            //}
            //if (e.Row.Cells[4].Text == "&nbsp;" || e.Row.Cells[4].Text == "")
            //{
            //    e.Row.Cells[4].Text = "0";

            //}
            string total = e.Row.Cells[3].Text;
            if (total.Contains("-"))
            {
                e.Row.Cells[3].Text = total.Substring(1, total.Length - 1);
            }
            string utltotal = e.Row.Cells[4].Text;
            if (utltotal.Contains("-"))
            {
                e.Row.Cells[4].Text = utltotal.Substring(1, utltotal.Length - 1);
            }
            e.Row.Cells[6].Attributes.Add("style", "word-break:break-all;word-wrap:break-word;");
        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            string total = e.Row.Cells[3].Text;
            if (total.Contains("-"))
            {
                e.Row.Cells[3].Text = total.Substring(1, total.Length-1);
            }
            string utltotal = e.Row.Cells[4].Text;
            if (utltotal.Contains("-"))
            {
                e.Row.Cells[4].Text = utltotal.Substring(1, utltotal.Length - 1);
            }
        }
    }

    protected void MemberIDChanged(object sender, EventArgs e)
    {

        txtCurrentBalance.Text = "";
        txtUtilization.Text = "";
        txtRemarks.Text = "";
        txtOldBalance.Text = "";
        btnSave.Enabled = true;
        string memberid = txtID.Text.Trim();
        User u = new User();
        Business b = new Business();
        u = b.findmembername(memberid);
        txtMembername.Text = u.membername;
        IncentiveBusiness B = new IncentiveBusiness();
        IncentivePoint obj = new IncentivePoint();
        string inst = Session["InstituteId"].ToString();
        string role = Session["Role"].ToString();

        obj = B.SelectMemberCurBalanceInstitutewise(memberid, inst,role);
        if (obj.MemberId != null)
        {
            txtCurrentBalance.Text = obj.CurrentBalance.ToString();
            txtOldBalance.Text = obj.OpeningBalance.ToString();
            txtUtilizationDate.Text = DateTime.Now.ToShortDateString();

            string member = txtmidSearch.Text.ToString();
            string membername = txtmnameSearch.Text.ToString();
            

             if (role == "7")
             {
                 DataSet ds = B.SelectMembersInstitutewise(inst, memberid, u.membername);

                 Panel1.Visible = true;
                 popGridSearch.DataSource = ds;
                 popGridSearch.DataBind();
                 popGridSearch.Visible = true;
                 Panel4.Visible = true;
             }
             else if (role == "17")
             {
                 DataSet ds1 = B.SelectFacultyInstitutewise(inst, memberid, u.membername);
                 Panel1.Visible = true;
                 popGridSearch.DataSource = ds1;
                 popGridSearch.DataBind();
                 popGridSearch.Visible = true;
                 Panel4.Visible = true;
             }
             else
             {
                 DataSet ds2 = B.SelectStudentInstitutewise(inst, memberid, u.membername);
                 if (u.MUNonMU !="M")
                 {
                     Panel1.Visible = true;
                     popGridSearch.DataSource = ds2;
                     popGridSearch.DataBind();
                     popGridSearch.Visible = true;
                     Panel4.Visible = true;
                 }
             }
            GridView2.Visible = true;
            Label1.Visible = true;
        }
      
        DataTable dt = new DataTable();
        dt = B.CountUtilizationPoints(memberid);
        if (dt.Rows.Count > 0)
        {
            GridView2.Columns[2].FooterText = "Total";
            GridView2.Columns[3].FooterText = dt.Rows[0]["count1"].ToString();
            GridView2.Columns[4].FooterText = dt.Rows[0]["count2"].ToString();
            GridView2.DataSource = dt;
            GridView2.DataBind();
        }
        else
        {
            GridView2.DataBind();
        }
    }
}

