using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;

public partial class Admin_UsefulLinks : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        popup.Visible = true;
    }

    protected void LinkButtonDownloadPdf_Click(object sender, EventArgs e)
    {

        string servername = ConfigurationManager.AppSettings["ServerName"].ToString();
        string domainame = ConfigurationManager.AppSettings["DomainName"].ToString();
        string username = ConfigurationManager.AppSettings["UserName"].ToString();
        string password = ConfigurationManager.AppSettings["Password"].ToString();
        string folderpath;
        using (NetworkShareAccesser.Access(servername, domainame, username, password))
        {

            folderpath = ConfigurationManager.AppSettings["DocumentsPath"].ToString();
            string[] files = Directory.GetFiles(folderpath);
            string filename1 = Path.GetFileName(files[0]);
            string path_BoxId = Path.Combine(folderpath, filename1);
            string newpath = path_BoxId.Replace('\\', '/');
            string filePath = path_BoxId;
            var uri = new Uri(newpath);
            string filename = Path.GetFullPath(uri.LocalPath);
            HttpResponse response = HttpContext.Current.Response;
            response.Clear();
            response.ClearContent();
            response.ClearHeaders();
            response.Buffer = false;

            WebClient wc = new WebClient();
            response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", Path.GetFileName(newpath)));
            response.ContentType = "application/octet-stream";
            byte[] data = wc.DownloadData(filePath);
            response.BinaryWrite(data);
            response.End();
        }


    }
    protected void lnkPubUserMamnual_Click(object sender, EventArgs e)
    {
        string servername = ConfigurationManager.AppSettings["ServerName"].ToString();
        string domainame = ConfigurationManager.AppSettings["DomainName"].ToString();
        string username = ConfigurationManager.AppSettings["UserName"].ToString();
        string password = ConfigurationManager.AppSettings["Password"].ToString();
        string folderpath;
        using (NetworkShareAccesser.Access(servername, domainame, username, password))
        {

            folderpath = ConfigurationManager.AppSettings["DocumentsPath"].ToString();
            string[] files = Directory.GetFiles(folderpath);
            string filename1 = "Publication_UserManual.pdf";
            string path_BoxId = Path.Combine(folderpath, filename1);
            string newpath = path_BoxId.Replace('\\', '/');
            string filePath = path_BoxId;
            var uri = new Uri(newpath);
            string filename = Path.GetFullPath(uri.LocalPath);
            HttpResponse response = HttpContext.Current.Response;
            response.Clear();
            response.ClearContent();
            response.ClearHeaders();
            response.Buffer = false;

            WebClient wc = new WebClient();
            response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", Path.GetFileName(newpath)));
            response.ContentType = "application/octet-stream";
            byte[] data = wc.DownloadData(filePath);
            response.BinaryWrite(data);
            response.End();
        }
    }
    protected void lnkProjectUserManual_Click(object sender, EventArgs e)
    {
        string servername = ConfigurationManager.AppSettings["ServerName"].ToString();
        string domainame = ConfigurationManager.AppSettings["DomainName"].ToString();
        string username = ConfigurationManager.AppSettings["UserName"].ToString();
        string password = ConfigurationManager.AppSettings["Password"].ToString();
        string folderpath;
        using (NetworkShareAccesser.Access(servername, domainame, username, password))
        {

            folderpath = ConfigurationManager.AppSettings["DocumentsPath"].ToString();
            string[] files = Directory.GetFiles(folderpath);
            string filename1 = "Project_UserManual.pdf";
            string path_BoxId = Path.Combine(folderpath, filename1);
            string newpath = path_BoxId.Replace('\\', '/');
            string filePath = path_BoxId;
            var uri = new Uri(newpath);
            string filename = Path.GetFullPath(uri.LocalPath);
            HttpResponse response = HttpContext.Current.Response;
            response.Clear();
            response.ClearContent();
            response.ClearHeaders();
            response.Buffer = false;

            WebClient wc = new WebClient();
            response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", Path.GetFileName(newpath)));
            response.ContentType = "application/octet-stream";
            byte[] data = wc.DownloadData(filePath);
            response.BinaryWrite(data);
            response.End();
        }
    }
    protected void lnkBankDetail_Click(object sender, EventArgs e)
    {
        string servername = ConfigurationManager.AppSettings["ServerName"].ToString();
        string domainame = ConfigurationManager.AppSettings["DomainName"].ToString();
        string username = ConfigurationManager.AppSettings["UserName"].ToString();
        string password = ConfigurationManager.AppSettings["Password"].ToString();
        string folderpath;
        using (NetworkShareAccesser.Access(servername, domainame, username, password))
        {

            folderpath = ConfigurationManager.AppSettings["DocumentsPath"].ToString();
            string[] files = Directory.GetFiles(folderpath);
            string filename1 = "Bank Details Form.docx";
            string path_BoxId = Path.Combine(folderpath, filename1);
            string newpath = path_BoxId.Replace('\\', '/');
            string filePath = path_BoxId;
            var uri = new Uri(newpath);
            string filename = Path.GetFullPath(uri.LocalPath);
            HttpResponse response = HttpContext.Current.Response;
            response.Clear();
            response.ClearContent();
            response.ClearHeaders();
            response.Buffer = false;

            WebClient wc = new WebClient();
            response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", Path.GetFileName(newpath)));
            response.ContentType = "application/octet-stream";
            byte[] data = wc.DownloadData(filePath);
            response.BinaryWrite(data);
            response.End();
        }
    }


    protected void lnkSCopusIndex_Click(object sender, EventArgs e)
    {
        string servername = ConfigurationManager.AppSettings["ServerName"].ToString();
        string domainame = ConfigurationManager.AppSettings["DomainName"].ToString();
        string username = ConfigurationManager.AppSettings["UserName"].ToString();
        string password = ConfigurationManager.AppSettings["Password"].ToString();
        string folderpath;
        using (NetworkShareAccesser.Access(servername, domainame, username, password))
        {

            folderpath = ConfigurationManager.AppSettings["DocumentsPath"].ToString();
            string[] files = Directory.GetFiles(folderpath);
            string filename1 = "CiteScore_Metrics_2011-2016_Download_21Jun2017.xlsx";
            string path_BoxId = Path.Combine(folderpath, filename1);
            string newpath = path_BoxId.Replace('\\', '/');
            string filePath = path_BoxId;
            var uri = new Uri(newpath);
            string filename = Path.GetFullPath(uri.LocalPath);
            HttpResponse response = HttpContext.Current.Response;
            response.Clear();
            response.ClearContent();
            response.ClearHeaders();
            response.Buffer = false;

            WebClient wc = new WebClient();
            response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", Path.GetFileName(newpath)));
            response.ContentType = "application/octet-stream";
            byte[] data = wc.DownloadData(filePath);
            response.BinaryWrite(data);
            response.End();
        }
    }
    protected void lnkJCR_Click(object sender, EventArgs e)
    {
        string servername = ConfigurationManager.AppSettings["ServerName"].ToString();
        string domainame = ConfigurationManager.AppSettings["DomainName"].ToString();
        string username = ConfigurationManager.AppSettings["UserName"].ToString();
        string password = ConfigurationManager.AppSettings["Password"].ToString();
        string folderpath;
        using (NetworkShareAccesser.Access(servername, domainame, username, password))
        {

            folderpath = ConfigurationManager.AppSettings["DocumentsPath"].ToString();
            string[] files = Directory.GetFiles(folderpath);
            string filename1 = "WOS-2016 final.xlsx";
            string path_BoxId = Path.Combine(folderpath, filename1);
            string newpath = path_BoxId.Replace('\\', '/');
            string filePath = path_BoxId;
            var uri = new Uri(newpath);
            string filename = Path.GetFullPath(uri.LocalPath);
            HttpResponse response = HttpContext.Current.Response;
            response.Clear();
            response.ClearContent();
            response.ClearHeaders();
            response.Buffer = false;

            WebClient wc = new WebClient();
            response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", Path.GetFileName(newpath)));
            response.ContentType = "application/octet-stream";
            byte[] data = wc.DownloadData(filePath);
            response.BinaryWrite(data);
            response.End();
        }
    }
    protected void exit1(object sender, EventArgs e)
    {

        popup.Visible = false;

    }
    protected void lnkUGCNotification_Click(object sender, EventArgs e)
    {
        string servername = ConfigurationManager.AppSettings["ServerName"].ToString();
        string domainame = ConfigurationManager.AppSettings["DomainName"].ToString();
        string username = ConfigurationManager.AppSettings["UserName"].ToString();
        string password = ConfigurationManager.AppSettings["Password"].ToString();
        string folderpath;
        using (NetworkShareAccesser.Access(servername, domainame, username, password))
        {

            folderpath = ConfigurationManager.AppSettings["DocumentsPath"].ToString();
            string[] files = Directory.GetFiles(folderpath);
            string filename1 = "7771545_academic-integrity-Regulation2018.pdf";
            string path_BoxId = Path.Combine(folderpath, filename1);
            string newpath = path_BoxId.Replace('\\', '/');
            string filePath = path_BoxId;
            var uri = new Uri(newpath);
            string filename = Path.GetFullPath(uri.LocalPath);
            HttpResponse response = HttpContext.Current.Response;
            response.Clear();
            response.ClearContent();
            response.ClearHeaders();
            response.Buffer = false;

            WebClient wc = new WebClient();
            response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", Path.GetFileName(newpath)));
            response.ContentType = "application/octet-stream";
            byte[] data = wc.DownloadData(filePath);
            response.BinaryWrite(data);
            response.End();
        }
    }
    protected void lnkSubjectWiseJournallist_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/SubUsefulLinks.aspx");
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        string servername = ConfigurationManager.AppSettings["ServerName"].ToString();
        string domainame = ConfigurationManager.AppSettings["DomainName"].ToString();
        string username = ConfigurationManager.AppSettings["UserName"].ToString();
        string password = ConfigurationManager.AppSettings["Password"].ToString();
        string folderpath;
        using (NetworkShareAccesser.Access(servername, domainame, username, password))
        {

            folderpath = ConfigurationManager.AppSettings["DocumentsPath"].ToString();
            string[] files = Directory.GetFiles(folderpath);
            string filename1 = "MAHE Hierarchy (00000002).pdf";
            string path_BoxId = Path.Combine(folderpath, filename1);
            string newpath = path_BoxId.Replace('\\', '/');
            string filePath = path_BoxId;
            var uri = new Uri(newpath);
            string filename = Path.GetFullPath(uri.LocalPath);
            HttpResponse response = HttpContext.Current.Response;
            response.Clear();
            response.ClearContent();
            response.ClearHeaders();
            response.Buffer = false;

            WebClient wc = new WebClient();
            response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", Path.GetFileName(newpath)));
            response.ContentType = "application/octet-stream";
            byte[] data = wc.DownloadData(filePath);
            response.BinaryWrite(data);
            response.End();
        }
    }
    protected void LinkButtondiscontinued_Click(object sender, EventArgs e)
    {
        string servername = ConfigurationManager.AppSettings["ServerName"].ToString();
        string domainame = ConfigurationManager.AppSettings["DomainName"].ToString();
        string username = ConfigurationManager.AppSettings["UserName"].ToString();
        string password = ConfigurationManager.AppSettings["Password"].ToString();
        string folderpath;
        using (NetworkShareAccesser.Access(servername, domainame, username, password))
        {

            folderpath = ConfigurationManager.AppSettings["DocumentsPath"].ToString();
            string[] files = Directory.GetFiles(folderpath);
            string filename1 = "Discontinued-sources-from-Scopus.xlsx";
            string path_BoxId = Path.Combine(folderpath, filename1);
            string newpath = path_BoxId.Replace('\\', '/');
            string filePath = path_BoxId;
            var uri = new Uri(newpath);
            string filename = Path.GetFullPath(uri.LocalPath);
            HttpResponse response = HttpContext.Current.Response;
            response.Clear();
            response.ClearContent();
            response.ClearHeaders();
            response.Buffer = false;

            WebClient wc = new WebClient();
            response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", Path.GetFileName(newpath)));
            response.ContentType = "application/octet-stream";
            byte[] data = wc.DownloadData(filePath);
            response.BinaryWrite(data);
            response.End();
        }
    }
    protected void LinkButtonGrandChallenge_Click(object sender, EventArgs e)
    {
        string servername = ConfigurationManager.AppSettings["ServerName"].ToString();
        string domainame = ConfigurationManager.AppSettings["DomainName"].ToString();
        string username = ConfigurationManager.AppSettings["UserName"].ToString();
        string password = ConfigurationManager.AppSettings["Password"].ToString();
        string folderpath;
        using (NetworkShareAccesser.Access(servername, domainame, username, password))
        {

            folderpath = ConfigurationManager.AppSettings["DocumentsPath"].ToString();
            string[] files = Directory.GetFiles(folderpath);
            string filename1 = "Grand Challenge Manipal .pdf";
            string path_BoxId = Path.Combine(folderpath, filename1);
            string newpath = path_BoxId.Replace('\\', '/');
            string filePath = path_BoxId;
            var uri = new Uri(newpath);
            string filename = Path.GetFullPath(uri.LocalPath);
            HttpResponse response = HttpContext.Current.Response;
            response.Clear();
            response.ClearContent();
            response.ClearHeaders();
            response.Buffer = false;

            WebClient wc = new WebClient();
            response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", Path.GetFileName(newpath)));
            response.ContentType = "application/octet-stream";
            byte[] data = wc.DownloadData(filePath);
            response.BinaryWrite(data);
            response.End();
        }
    }
    protected void LinkButtonGrandChallengeFlyer_Click(object sender, EventArgs e)
    {
        string servername = ConfigurationManager.AppSettings["ServerName"].ToString();
        string domainame = ConfigurationManager.AppSettings["DomainName"].ToString();
        string username = ConfigurationManager.AppSettings["UserName"].ToString();
        string password = ConfigurationManager.AppSettings["Password"].ToString();
        string folderpath;
        using (NetworkShareAccesser.Access(servername, domainame, username, password))
        {

            folderpath = ConfigurationManager.AppSettings["DocumentsPath"].ToString();
            string[] files = Directory.GetFiles(folderpath);
            string filename1 = "DOR_ grand challenge Manipal.pdf";
            string path_BoxId = Path.Combine(folderpath, filename1);
            string newpath = path_BoxId.Replace('\\', '/');
            string filePath = path_BoxId;
            var uri = new Uri(newpath);
            string filename = Path.GetFullPath(uri.LocalPath);
            HttpResponse response = HttpContext.Current.Response;
            response.Clear();
            response.ClearContent();
            response.ClearHeaders();
            response.Buffer = false;

            WebClient wc = new WebClient();
            response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", Path.GetFileName(newpath)));
            response.ContentType = "application/octet-stream";
            byte[] data = wc.DownloadData(filePath);
            response.BinaryWrite(data);
            response.End();
        }
    }
}