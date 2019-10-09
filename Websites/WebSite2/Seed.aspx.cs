using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using System.Data;
using System.Configuration;

public partial class Seed : System.Web.UI.Page
{
    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CheckseedMoneyActive();
        }
    }

    private void CheckseedMoneyActive()
    {
        log.Info("BatchJobSeedMoney : Inside CheckSeedMoneyActive function");
        try 
        {
            string toaddress = "";
            string cc = "";
            string bcc = "";
            string subject = "";
            string id = "";
            Journal_DataObject obj = new Journal_DataObject();
            DataSet ds = new DataSet();
            bool result = false;
            bool result1 = false;
            ds = obj.SelectSeedMoneyActive();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                try
                {
                    string id1 = ds.Tables[0].Rows[i]["id"].ToString();
                    string type = ds.Tables[0].Rows[i]["Type"].ToString();

                    DateTime FromDate = Convert.ToDateTime(ds.Tables[0].Rows[i]["FromDate"].ToString());

                    DateTime ToDate =  Convert.ToDateTime(ds.Tables[0].Rows[i]["ToDate"].ToString());
                    
                    DateTime todaydate = DateTime.Now;
                    string FromDate1 = FromDate.ToString("dd-MM-yyyy");
                    string ToDate1 = ToDate.ToString("dd-MM-yyyy");
                    string todaydate1 = todaydate.ToString("dd-MM-yyyy");

                    DateTime dt1 = DateTime.ParseExact(FromDate1, "dd-MM-yyyy", null);
                    DateTime dt2 = DateTime.ParseExact(ToDate1, "dd-MM-yyyy", null);

                   
                    //DateTime todaydate = DateTime.Now;

                    DateTime dt3 = DateTime.ParseExact(todaydate1, "dd-MM-yyyy", null);

                    if ((dt3 >= dt1) && (dt3 <= dt2))
                    {
                        result1 = obj.UpdateSeedMoneyAciveFlagY(id1, type);
                    }
                    else 
                    {
                        result = obj.UpdateSeedMoneyAciveFlag(id1, type);
                    }

                                        
                 
                    if (result == true)
                    {

                        log.Info("BatchJobSeedMoney:Seed Money Status disabled for id:" + id1 + " type: " + type);                       
                        ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Seed Money Status disabled Succesfully')</script>");

                    }

                }
                catch (Exception ex)
                {
                    //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Problem in sending email')</script>");
                    log.Error(ex.StackTrace);
                    log.Error(ex.Message);
                    log.Error("BatchJobSeedMoney:Error   : '" + ex + "' ");                 
                }


            }
        }
        catch (Exception e)
        {
            log.Error(e.StackTrace);
            log.Error(e.Message);
            log.Error("BatchJobSeedMoney:Error: '" + e + "' ");           
        }
        Response.Write("<script language='javascript'> {window.open('', '_self', ''); window.close();}</script>");
    }
    
   

}