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

public partial class SubUsefulLinks : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void LinkBtnBusiness_Click(object sender, EventArgs e)
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
            string filename1 = "Business and Economics.xlsx";
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
    protected void LinkBtnClinical_Click(object sender, EventArgs e)
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
            string filename1 = "Clinical preclinical.xlsx";
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
    protected void LinkBtnComputer_Click(object sender, EventArgs e)
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
            string filename1 = "Computer Science.xlsx";
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
    protected void LinkBtnEngineeeringTech_Click(object sender, EventArgs e)
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
            string filename1 = "Engineering and technology.xlsx";
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
    protected void LinkBtnEngineeringComputer_Click(object sender, EventArgs e)
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
            string filename1 = "Engineering and Computer_Common journals.xlsx";
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
    protected void LinkBtnLifeScienceClinical_Click(object sender, EventArgs e)
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
            string filename1 = "Life science and clinical_common journals.xlsx";
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
    protected void LinkBtnlifescience_Click(object sender, EventArgs e)
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
            string filename1 = "life science.xlsx";
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
    protected void LinkBtnPhysical_Click(object sender, EventArgs e)
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
            string filename1 = "Physcial science.xlsx";
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
    protected void LinkBtnBooklist_Click(object sender, EventArgs e)
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
            string filename1 = "book list.xlsx";
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
    protected void LinkBtnSocialSceince_Click(object sender, EventArgs e)
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
            string filename1 = "Social science.xlsx";
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

    protected void LinkBtnArtsandhumanities_Click(object sender, EventArgs e)
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
            string filename1 = "Arts and humanities.xlsx";
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



    protected void LinkBtnBusines_Click(object sender, EventArgs e)
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
            string filename1 = "Buisness.xlsx";
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
    protected void LinkBtnComputers_Click(object sender, EventArgs e)
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
            string filename1 = "Computers.xlsx";
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

    protected void LinkBtnEducation_Click(object sender, EventArgs e)
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
            string filename1 = "Education.xlsx";
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


    protected void LinkBtnLaw_Click(object sender, EventArgs e)
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
            string filename1 = "Law.xlsx";
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
    protected void LinkBtnPsychology_Click(object sender, EventArgs e)
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
            string filename1 = "Psychology.xlsx";
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

    protected void LinkBtnSocial_Click(object sender, EventArgs e)
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
            string filename1 = "Social.xlsx";
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

    protected void LinkBtnMultidisciplinary_Click(object sender, EventArgs e)
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
            string filename1 = "Multidisciplinary (1).xlsx";
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