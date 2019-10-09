using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Data.OleDb;
using log4net;
using System.Net.Mail;

public partial class Upload_JC_Report : System.Web.UI.Page
{

    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    public int validExcel = 1;
    public double debitTot = 0;
    public double creditTot = 0;

    public string pageID = "L11";


    protected void Page_Load(object sender, EventArgs e)
    {
      

        if (!IsPostBack)
        {
            if (!Session["authPage"].ToString().Contains("$" + pageID + "$"))
            {
                string unacces = "Unauthorized Acces!!! Login Again";
                Response.Redirect("~/Login.aspx?val=" + unacces);
            }
        }

        upload.Attributes.Add("onclick", "this.disabled =true;" + ClientScript.GetPostBackEventReference(upload, null) + ";");

    }



    protected void upload_Click(object sender, EventArgs e)
     {

        if (!Page.IsValid)
        {
            return;
        }

        log.Debug("Inside JC upload_Click page");

        try
        {

            bindGridview(sender, e);



            Business b = new Business();
            string jname = null, jid = null;
            if (GridExcelData.Visible == false && validExcel == 1)
            {

                
                JournalData[] jd = new JournalData[GridExcelData.Rows.Count];


               string Jyear1 = null;
              Jyear1=  GridExcelData.HeaderRow.Cells[0].Text;


              string[] name = Jyear1.Split(' ');
              string year = name[4];

              if (year != TextYear.Text.Trim())
              {
                  ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Entered year is not valid to the file which is selected')</script>");
                  return;
              }


                for (int j = 2; j < GridExcelData.Rows.Count ;j++ )
                {

                    jname = GridExcelData.Rows[j].Cells[0].Text.Trim();
                    jid = GridExcelData.Rows[j].Cells[1].Text.Trim();

                    string impactFactor = GridExcelData.Rows[j].Cells[3].Text.Trim();
                    string IMIndex = GridExcelData.Rows[j].Cells[5].Text.Trim();

                    string total = GridExcelData.Rows[j].Cells[2].Text.Trim();
                    string fiveimpcrfact = GridExcelData.Rows[j].Cells[4].Text.Trim();

                    string arcticles = GridExcelData.Rows[j].Cells[6].Text.Trim();
                    string halflife = GridExcelData.Rows[j].Cells[7].Text.Trim();

                    string facorscore = GridExcelData.Rows[j].Cells[8].Text.Trim();
                    string influscore = GridExcelData.Rows[j].Cells[9].Text.Trim();




                    jd[j] = new JournalData();

                    jd[j].jname = jname;
                    jd[j].JournalID = jid;
                
                    jd[j].year = year;
                    if (impactFactor == "&nbsp;")
                    {
                        jd[j].impctfact =0;
                    }
                    else
                    {

                        jd[j].impctfact = Convert.ToDouble(impactFactor);
                    }
                    if (IMIndex == "&nbsp;")
                    {
                        jd[j].imindex = 0;
                    }
                    else
                    {
                        jd[j].imindex = Convert.ToDouble(IMIndex);
                    }
                    if (total == "&nbsp;")
                    {
                        jd[j].total = 0;
                    }
                    else
                    {
                        jd[j].total = Convert.ToDouble(total);
                    }
                    if (fiveimpcrfact == "&nbsp;")
                    {
                        jd[j].fiveimpcrfact = 0;
                    }
                    else
                    {
                        jd[j].fiveimpcrfact = Convert.ToDouble(fiveimpcrfact);
                    }
                    if (arcticles == "&nbsp;")
                    {
                        jd[j].arcticles = 0;
                    }
                    else
                    {
                        jd[j].arcticles = Convert.ToDouble(arcticles);
                    }
                    if (halflife == "&nbsp;")
                    {
                        jd[j].halflife = "";
                    }
                    else
                    {
                        jd[j].halflife = halflife;
                    }
                    if (facorscore == "&nbsp;")
                    {
                        jd[j].facorscore = 0;
                    }
                    else
                    {
                        jd[j].facorscore = Convert.ToDouble(facorscore);
                    }
                    if (influscore == "&nbsp;")
                    {
                        jd[j].influscore = 0;
                    }
                    else
                    {
                        jd[j].influscore = Convert.ToDouble(influscore);
                    }

                }
                int result = 0;
                result = b.InsertJCFileUploadCSV(jd);

                if (result == 1 || result == 0)
                {
                    sendmail();
                    ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('File uploaded Successfully')</script>");
                    TextYear.Text = "";
                  //  upload.Enabled = false;
                }
                else
                {
                    ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Error in  File upload')</script>");

                 
                }
            }


            else
            {
               
                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Invalid File!')</script>");
                validExcel = 1;
                upload.Enabled = false;
            }
        }

        catch (Exception ex)
        {
            log.Error("Inside Catch Block Of JC FileUpload" + ex.Message + " With UserID" + Session["UserId"].ToString());

            log.Error(ex.StackTrace);
      
            String det = "HHH";
            int i, j;

          

             if (ex.Message.Contains("Specified argument was out of the range of valid values"))
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Uploaded File format for JV upload is not valid')</script>");

            }

            else if (ex.Message.Contains("Input string was not in a correct format"))
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('File Upload Failed--File format is not valid')</script>");

            }
            else if (ex.Message.Contains("'Sheet1$' is not a valid name"))
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Sheet1 is not there in  Uploaded file')</script>");

            }
                

            else if (ex.Message.Contains("Could not find a part of the path"))
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('You need to create SDocument folder ..then try to upload the file')</script>");

            }

            else if (ex.Message.Contains("Object reference not set to an instance of an object"))
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('ERROR!!!Contact Admin')</script>");

            }
              else if (ex.Message.Contains("Mailbox unavailable. The server response was: #5.1.0 "))
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('File uploaded Successfully....Error in mail sending!!!!')</script>");

            }
                   else
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('ERROR! File Upload Failed')</script>");
            }

        }

    }

    protected void sendmail()
    {
        string msgUpload = "";

      string NewJid = Session["NewJidUploadJCR"].ToString();
      

        if (NewJid == "")
        {
            msgUpload = "Impact Factor of all the Journals in the file has been added to RMS.";
        }
        else
        {
            msgUpload = "Please find below the list of Journals which are not present in Journal Master, hence the Impact factor is not updated in the RMS.";
        }

        MailMessage Msg = new MailMessage();
        System.Net.Mail.SmtpClient spcl = new System.Net.Mail.SmtpClient();
        Msg.Subject = "Upload JC Report";

       // string dir_domain = ConfigurationManager.AppSettings["DirectoryDomain"].ToString();
        string FooterText = ConfigurationManager.AppSettings["FooterText"].ToString();

        Msg.Body = "<span style=\"font-size: 10pt; color: #3300cc; font-family: Verdana\"><h4>Dear Sir/Madam,</h4> <br>" +
             "<b> The attached JC Report has been uploaded successfully to RMS. " + msgUpload + "<br>" +
             "<br>" +
               "" + NewJid + "<br>" +
            //  "Journal Id's which are been added :  " + ExistJid + "<br>" +




               "<br>" +
                  "<br>" +
                "<br>" +
                "<br>" +
                 "<br>" +
                 "<br>" +
                 "<br>" + "<br>" + "<br>" + "<br>" + "<br>" + FooterText +
               "<span style=\"font-Style: Italic\">Thank You <br>" +



            " </b><br><b> </b></span>";


        // Msg.Body = "<span style=\"font-size: 10pt; color: #3300cc; font-family: italic\"><br>" +
        // "<b> Thank You <br>" +



        //" </b><br><b> </b></span>";

        Msg.Priority = MailPriority.Normal;
        Msg.IsBodyHtml = true;

        string frmEmail = ConfigurationManager.AppSettings["FromAddress"].ToString();
        Msg.From = new MailAddress(frmEmail);


        // Msg.To.Add(BuyerId_Array[0]+dir_domain);
        string Toemail = HttpContext.Current.Session["emailId"].ToString();

        Msg.To.Add(Toemail);
      //  Msg.To.Add("reema.aroza" + dir_domain);

        string attachmentFile = F_Upload.PostedFile.FileName;
           
      //  System.Net.Mail.Attachment attachment;
      //  attachment = new System.Net.Mail.Attachment(attachmentFile);
      //  Msg.Attachments.Add(attachment);
        Msg.Attachments.Add(new Attachment(F_Upload.PostedFile.InputStream, Path.GetFileName(F_Upload.PostedFile.FileName), F_Upload.PostedFile.ContentType));

        log.Info(" Email will be sent to authors  : '" + Toemail + "' ");




        spcl.Host = ConfigurationManager.AppSettings["MailHost"].ToString();
        string password = ConfigurationManager.AppSettings["MailPassword"].ToString();
        spcl.Port = Convert.ToInt16(ConfigurationManager.AppSettings["SMTPPort"]);
        spcl.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSSL"]);
        spcl.Credentials = new System.Net.NetworkCredential(frmEmail, password);
        spcl.Send(Msg);

       
    }

    protected void GridExcelData_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                //if (e.Row.Cells[0].Text == "&nbsp;" || e.Row.Cells[0].Text.Trim() == "" || e.Row.Cells[0].Text == null)
                //{
                //    GridExcelData.Visible = true;
                //    ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Excel Data Is Invalid!')</script>");
                //}
            }
        }
    }

    protected void GridExcelData_PageChanged(object sender, GridViewPageEventArgs e)
    {
        GridExcelData.PageIndex = e.NewPageIndex;
        GridExcelData.DataBind();
    }

    protected void LinkButtonJCRUploadInst_Click(object sender, EventArgs e)
    {
        Response.Write("<script>");
        Response.Write("window.open('../JCRUploadInst.htm','newWin')");
        //path sent to display.aspx page
        Response.Write("</script>");

    }

    protected void bindGridview(object sender, EventArgs e)
    {
        string connectionString = "";
        if (F_Upload.HasFile)
        {
            string savePath = ConfigurationManager.AppSettings["UploadPath"];

            string strFileType = Path.GetExtension(F_Upload.FileName).ToLower();
         //  string attachmentFile = F_Upload.PostedFile.FileName;
            //string path = string.Concat(Server.MapPath(savePath + F_Upload.FileName));
            //F_Upload.PostedFile.SaveAs(path);
            string path = string.Concat(savePath + "/" + F_Upload.FileName);
            F_Upload.SaveAs(path);
             string attachmentFile = F_Upload.PostedFile.FileName;
       
            if (strFileType == ".xls")
            {
                connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
            }
            else if (strFileType == ".csv")
               { 
               // connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                  // connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Extended Properties=\"text;HDR=Yes;FMT=Fixed\"";
                 //  connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Extended Properties=\"Text;HDR=Yes;FMT=Delimited\"";
              
                   //connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileLocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";

                     connectionString = "Provider=Microsoft.Jet.OleDb.4.0; Data Source = " + System.IO.Path.GetDirectoryName(path) + "; Extended Properties = \"Text;HDR=YES;FMT=Delimited\"";
                
               }
            

            else
            {
                validExcel = 0;
                return;
            }
            string query = String.Format("SELECT * FROM {0}", Path.GetFileName(path));
           // string query = String.Format("SELECT * FROM [S_JVUPLOADcsv$]", Path.GetFileName(path));
            string filename =null;
            filename = Path.GetFileName(path);
            string[] fname = filename.Split('_');
            string nmae = fname[0];
            //  OleDbConnection conn = new OleDbConnection(connectionString);
           // OleDbConnection conn = new OleDbConnection("Provider=Microsoft.Jet.OleDb.4.0; Data Source = " + System.IO.Path.GetDirectoryName(path) + "; Extended Properties = \"Text;HDR=YES;FMT=Delimited\"");


            OleDbConnection conn = new OleDbConnection(connectionString);
       
            if (conn.State == ConnectionState.Closed)
                conn.Open();
            OleDbCommand cmd = new OleDbCommand(query, conn);
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            GridExcelData.DataSource = ds.Tables[0];
            GridExcelData.DataBind();
                    int c =0;
          c = GridExcelData.Rows.Count;
            da.Dispose();
            conn.Close();
            conn.Dispose();

        }
    }
}

