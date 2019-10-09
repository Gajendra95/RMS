using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ManageJournal_ManageSeedMoneyCategory : System.Web.UI.Page
{
    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Button1.Enabled = false;
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (!Page.IsValid)
        {
            return;
        }
        SeedMoney a = new SeedMoney();
        Business b = new Business();
        string category = DropDownListAuthorType.SelectedValue.Trim();
        a.Entrytype = category;
        double Amount = Convert.ToDouble(txtAmount.Text.Trim());
        a.Budget = Amount;
        int result = b.InsertSeedMoneyBudget(a);
        if (result >= 1)
        {
            string CloseWindow = "alert('Seed Money Category  value  saved  succesfully for: " + DropDownListAuthorType.SelectedItem + "')";
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow, true);
        }
        else
        {
            string CloseWindow = "alert('Error!')";
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow, true);
        }

    }
    protected void txtAmount_TextChanged(object sender, EventArgs e)
    {
        try{
        
            SeedMoney a = new SeedMoney();
            Business b = new Business();
            string category = DropDownListAuthorType.SelectedValue.Trim();
            a.Entrytype = category;
            double Amount = Convert.ToDouble(txtAmount.Text.Trim());
            a.Budget = Amount;
            int result = b.getSeedMoneyBudgetExist(a);

            if (result != 0)
            {
               
               
                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert(' Category Amount Already exists!')</script>");
                Button1.Enabled = false;
                return;
            }

            else
            {
                Button1.Enabled = true;
                //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert(' New User!')</script>");
                return;
            }

        }

        catch (Exception ex)
        {
            log.Error(ex.StackTrace);
            log.Error(ex.Message);

            log.Error("Error!!!!!!!!!!!!!!!! ");
            if (ex.Message.Contains("DDLdeptname' has a SelectedValue which is invalid because it does not exist in the list of"))
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Error!!Institue_Department Map error!!!')</script>");

            }

            else
                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Error!!!!!!!!!!')</script>");

        }
    }
}