using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;

using log4net;

public partial class MangaeIndexAgency : System.Web.UI.Page
{
    public string pageID = "L21";

    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    protected void Page_Load(object sender, EventArgs e)
    {
      
        if (!IsPostBack)
        {
            setModalWindow(sender, e);

            GridViewIndex.Visible = false;
            PanelView.Visible = false;
           // popupPanelPop.Visible = true;

            if (!Session["authPage"].ToString().Contains("$" + pageID + "$"))
            {
                string unacces = "Unauthorized Acces!!! Login Again";
                Response.Redirect("~/Login.aspx?val=" + unacces);
            }
        }
        
    }
    protected void setModalWindow(object sender, EventArgs e)
    {
     //  popupPanelPop.Visible = true;
        popGridIndex.DataSourceID = "SqlDataSourcePopIndex";
        SqlDataSourcePopIndex.DataBind();
        popGridIndex.DataBind();

    }

    protected void Butsave_Click(object sender, EventArgs e)
    {
        int result = 0, result1 = 0;
        Business B = new Business();
        IndexManage R = new IndexManage();
        log.Debug("inside Butsave_Click");
        string AgencyId = TextIndexAgencyCode.Text;

        try
        {
            string userid = Session["UserId"].ToString();

            string Agencyname = TextIndexAgencyName.Text.Trim();

            R.AgencyId = AgencyId;
            R.AgencyName = Agencyname;
            R.Uid = userid;


            int ret = checkupdateinsert(sender, e);

            if (ret == 0)
            {
                result = B.IndexAgencyInsert(R);
            }
            else
            {
                R.Active = DrpActInactive.SelectedValue;
               result1 = B.IndexAgencyUpdate(R);

            }


            if (result == 1)
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Index Data Saved Successfully')</script>");
                log.Info("Index Data Saved Successfully, Code:" + AgencyId);
                TextIndexAgencyCode.Enabled = true;
                TextIndexAgencyCode.Text = "";
                TextIndexAgencyName.Text = "";
                popupPanelPop.Visible = false;

            }

            if (result1 == 1)
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "validation", "<script language='javascript'>alert('Index Data updated  Successfully')</script>");
                log.Info("Index Data updated Successfully");
                GridViewIndex.DataBind();
                TextIndexAgencyCode.Text = "";
                TextIndexAgencyName.Text = "";
                status.Visible = false;
                TextIndexAgencyCode.Enabled = true;
                DrpActInactive.Visible = false;
                GridViewIndex.Visible = false;
                popupPanelPop.Visible = false;

            }

        }
        catch (Exception ex)
        {
            log.Error("Inside Catch Block Of IndexAgency_M Creation" + ex.Message + " For Index code " + AgencyId + " With UserID" + Session["UserId"].ToString());
            log.Error(ex.StackTrace);

        
                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Error!!!!!!!!! ')</script>");
          
        }

    }



    protected void IndexAgencyTextChanged(object sender, EventArgs e)
    {
        Business B = new Business();
        IndexManage v = new IndexManage();
        string AgencyId = null;
        AgencyId = TextIndexAgencyCode.Text.Trim();
        v = B.selectIndexAgency(AgencyId);

        if (v.AgencyId != null)
        {
            TextIndexAgencyCode.Text = v.AgencyId;
            TextIndexAgencyName.Text = v.AgencyName;
            TextIndexAgencyCode.Enabled = false;
            status.Visible = true;
            DrpActInactive.Visible = true;
            DrpActInactive.Text = v.Active;
            DrpActInactive.DataBind();
            popupPanelPop.Visible = false;

        }

        else
        {
            TextIndexAgencyCode.Enabled = false;
            popupPanelPop.Visible = false;
            ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('New Index ID....Please Continue!')</script>");
        }

    }

    


    protected int checkupdateinsert(object sender, EventArgs e)
    {
        log.Debug("inside the checkupdateinsert");


        Business B = new Business();
        IndexManage v = new IndexManage();
        string AgencyId = null;
        AgencyId = TextIndexAgencyCode.Text.Trim();


        v = B.selectIndexAgency(AgencyId);

        if (v.AgencyId == null)
        {
            return 0;
        }

        return 1;
    }

    protected void BtnClear_Click(object sender, EventArgs e)
    {
        log.Debug("inside the BtnClear_Click -to clear all the fields");
        string confirmValue2 = Request.Form["confirm_value2"];
        if (confirmValue2 == "Yes")
        {
            TextIndexAgencyCode.Enabled = true;
            TextIndexAgencyCode.Text = "";
            TextIndexAgencyCode.Enabled = true;
            TextIndexAgencyName.Text = "";

            DrpActInactive.Visible = false;
            status.Visible = false;
            GridViewIndex.Visible = false;
            PanelView.Visible = false;

        }
        TextIndexAgencyCode.Enabled = true;
        TextIndexAgencyCode.Text = "";
        TextIndexAgencyCode.Enabled = true;
        TextIndexAgencyName.Text = "";

        DrpActInactive.Visible = false;
        status.Visible = false;
        GridViewIndex.Visible = false;
        PanelView.Visible = false;
    }

    protected void Butview_Click(object sender, EventArgs e)
    {
        log.Debug("inside Butview_Click");
        PanelView.Visible = true;
        GridViewIndex.Visible = true;
    }

    protected void showPop(object sender, EventArgs e)
    { popupPanelPop.Visible = true;
        ModalPopupExtender1.Show();
    }

    protected void popSelected(Object sender, EventArgs e)
    {



        popGridIndex.Visible = true;
        GridViewRow row = popGridIndex.SelectedRow;

            string empcode = row.Cells[1].Text;


           TextIndexAgencyCode.Text = empcode;

           IndexcodeSrch.Text = "";
           popGridIndex.DataBind();
            IndexAgencyTextChanged(sender, e);

            IndexcodeSrch.Text = "";
            popGridIndex.DataBind();

    }
    protected void IndexCodeChanged(object sender, EventArgs e)
    {
        if (IndexcodeSrch.Text.Trim() == "")
        {
            SqlDataSourcePopIndex.SelectCommand = "SELECT  AgencyId, AgencyName FROM IndexAgency_M";
            popGridIndex.DataBind();
            popGridIndex.Visible = true;
        }

        else
        {
            SqlDataSourcePopIndex.SelectParameters.Clear();
            SqlDataSourcePopIndex.SelectParameters.Add("AgencyName", IndexcodeSrch.Text);

            SqlDataSourcePopIndex.SelectCommand = "SELECT  AgencyId, AgencyName FROM IndexAgency_M where AgencyName like '%' + @AgencyName + '%'";
            popGridIndex.DataBind();
            popGridIndex.Visible = true;
        }

        ModalPopupExtender1.Show();
    }


    //protected void ButtonSaveOnClick(object sender, EventArgs e)
    //{
    //    ArrayList listIndexAgency = new ArrayList();
    //    Business bus_obj = new Business();

    //    try{
    //    IndexManage indexManage_obj = new IndexManage();

    //    for (int i = 0; i < CheckboxIndexAgency.Items.Count; i++)
    //    {
    //        if (CheckboxIndexAgency.Items[i].Selected)
    //        {
    //            listIndexAgency.Add(CheckboxIndexAgency.Items[i].Value);
    //        }
    //    }


    //    indexManage_obj.Year = DropDownListYear.SelectedValue;
    //    indexManage_obj.Jid = TextJid.Text.Trim();
    //    if (TextImpFactor.Text != "")
    //    {
    //        indexManage_obj.ImpactFactor = Convert.ToDouble(TextImpFactor.Text.Trim());
    //    }

    //    int result = 0;

    //    result = bus_obj.InsertIndexAgency(indexManage_obj, listIndexAgency);
    //    if (result >= 1)
    //    {

    //        ButtonSave.Enabled = false;
 
    //        //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Data saved Successfully')</script>");
    //        // ClientScript.RegisterStartupScript(Page.GetType(), "alert", "alert('Data saved  Successfully..');window.location='../Login.aspx';", true);
    //        ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Data Saved Successfully')</script>");
    //    }
    //    else
    //    {
    //        ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Error!!!!!!!!!!!!!')</script>");
    //    }
    //}
    //            catch (Exception e1)
    //    {

    //        log.Error(" inside the ButtonSaveOnClick " + e1.Message);
    //        log.Error(e1.Message);
    //        log.Error(e1.StackTrace);
    //        ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Error!!!!!!!!!!')</script>");
          
    //    }

            



    //}


    protected void exit(Object sender, EventArgs e)
    {
        IndexcodeSrch.Text = "";
        popGridIndex.DataBind();
    }

}