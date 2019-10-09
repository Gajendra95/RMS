using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.IO;
using System.Configuration;

public partial class ClerkDepartment_DisplayPdf : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    string servername = ConfigurationManager.AppSettings["ServerName"].ToString();
        string domainame = ConfigurationManager.AppSettings["DomainName"].ToString();
        string username = ConfigurationManager.AppSettings["UserName"].ToString();
        string password = ConfigurationManager.AppSettings["Password"].ToString();
        try
        {
            using (NetworkShareAccesser.Access(servername, domainame, username, password))
            {
                string path = Session["path"].ToString();
                // string path = Request.QueryString["val"].ToString();

                if (!(string.IsNullOrEmpty(path)))
                {
                    string extention = Path.GetExtension(path);
                    int len = extention.Length - 1;
                    string extwithoutdot = extention.Substring(1, len);

                    if (extwithoutdot.Equals("JPG") || extwithoutdot.Equals("jpg") ||
                        extwithoutdot.Equals("jpeg") || extwithoutdot.Equals("JPEG"))
                    {
                        extwithoutdot = "jpeg";
                    }
                    if (extwithoutdot.Equals("TIF") || extwithoutdot.Equals("tif"))
                    {
                        extwithoutdot = "tiff";
                    }
                    if (extwithoutdot.Equals("GIF") || extwithoutdot.Equals("gif"))
                    {
                        extwithoutdot = "gif";
                    }
                    if (extwithoutdot.Equals("BMP") || extwithoutdot.Equals("bmp"))
                    {
                        extwithoutdot = "bmp";
                    }



                    string filetype = "";
                    if (extention.Equals(".pdf") || extention.Equals(".PDF"))
                    {
                        extwithoutdot = "pdf";
                        filetype = "PDF";
                    }

                    WebClient client = new WebClient();
                    Byte[] buffer = client.DownloadData(path);
                    if (buffer != null)
                    {
                        if (filetype.Equals("PDF"))
                        {
                            Response.ContentType = "application/" + extwithoutdot;
                        }
                        else
                        {
                            Response.ContentType = "image/" + extwithoutdot;
                        }

                        Response.AddHeader("content-length", buffer.Length.ToString());
                        Response.BinaryWrite(buffer);
                    }
                }
                //buffer.Flush();  //take care of buffer and try to clear it.
            }
        }
        catch(Exception ex)
        {
            //log.Error("DisplayPdf.cs : Exception caught");
            //log.Error(" Error Message" + ex);
            //log.Error(" Stack Trace " + ex.StackTrace);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Not able to view the requested document- Please contact support team')", true);
        }
    }
}