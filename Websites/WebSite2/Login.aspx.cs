using System;
using System.Collections.Generic;
using System.Linq;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data;
using System.Data.SqlClient;
using System.Web.Security;
using System.Runtime.InteropServices;
using System.DirectoryServices;
using System.Security.Permissions;
using System.Security.Principal;
using System.IO;
using System.Net;
using log4net;
using System.Security.Cryptography;
using System.Text;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]

public partial class Login : System.Web.UI.Page
{
    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            Session["User"] = "";
            Session["Role"] = "";
            Session["UserId"] = "";
            Session["UserName"] = "";
            Session["InstituteId"] = "";
            Session["Department"] = "";
            Session["emailId"] = "";
            Session["SupervisorId"] = "";
            Session["AutoApproval"] = "";
            Session["ActiveUser"] = "";
            Session["ProjectUnit"] = "";
            Session["RoleName"] = "";
            string a = (System.Web.HttpContext.Current.Request.QueryString["UserId"]);
            if (a != null)
            {
                string UserId = Request.QueryString["UserId"];
                string Password = Request.QueryString["Password"];
                string Module = Request.QueryString["Module"];
                string LAN= Request.QueryString["LAN"];

                UserId = Decrypt(UserId);
                Password = Decrypt(Password);

                TextBoxUid.Text = UserId;
                TextBoxPassword.Text = Password;
                btnLogin_Click(sender, e);
            }
        }
        lblunauthoaccess.Text = "";
    }

    private string Decrypt(string cipherText)
    {
        string EncryptionKey = "MAKV2SPBNI99212";
        cipherText = cipherText.Replace(" ", "+");
        byte[] cipherBytes = Convert.FromBase64String(cipherText);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(cipherBytes, 0, cipherBytes.Length);
                    cs.Close();
                }
                cipherText = Encoding.Unicode.GetString(ms.ToArray());
            }
        }
        return cipherText;
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {

        //if (!Page.IsValid)
        //{
        //    return;
        //}

        string lan = "";
        string dominName = string.Empty;
        string adPath = string.Empty;
        string userName = TextBoxUid.Text.Trim();
        string strError = string.Empty;
        string LANv = (System.Web.HttpContext.Current.Request.QueryString["LAN"]);
        if (LANv != "" && LANv != null)
        {
            lan = Request.QueryString["LAN"];
        }
        else
        {
            lan = ConfigurationManager.AppSettings["LAN"].ToString();
        }
        if (lan == "OFF")
        {
            log.Debug(" inside btnLogin_Click--Lan-OFF : " + TextBoxUid.Text.Trim());
            LoginLanOff_Click();
        }

        else
        {
            log.Debug(" inside btnLogin_Click--Lan-ON : " + TextBoxUid.Text.Trim());

            int flag = 0;
            int ret = selectRole(sender, e);


            if (ret == 0)
            {
                flag = 1;

                //string unacces = "Unauthorized Acces!!! Login Again";
                //Response.Redirect("~/Login.aspx?val=" + unacces);
                lblunauthoaccess.Text = "Unauthorized Acces!!! Login Again";
                return;
            }
            else
            {
                try
                {
                    foreach (string key in ConfigurationSettings.AppSettings.Keys)
                    {
                       // dominName = key.Contains("DirectoryDomain") ? ConfigurationSettings.AppSettings[key] : dominName;
                        dominName = "@manipal.edu";
                        adPath = key.Contains("DirectoryPath") ? ConfigurationSettings.AppSettings[key] : adPath;
                        if (!String.IsNullOrEmpty(dominName) && !String.IsNullOrEmpty(adPath))
                        {
                            if (true == AuthenticateUser(dominName, userName, TextBoxPassword.Text, adPath, out strError))
                            {
                                FormsAuthentication.RedirectFromLoginPage(TextBoxUid.Text, true);
                                Login_Click();
                            }
                            dominName = string.Empty;
                            adPath = string.Empty;
                            if (String.IsNullOrEmpty(strError)) break;
                        }

                    }
                    if (!string.IsNullOrEmpty(strError))
                    {
                        lblunauthoaccess.Text = "";
                        LblError.Text = "Invalid user name or Password!";

                    }
                }
                catch (Exception ex)
                {

                }
                finally
                {

                }

            }
        }

    }

    public bool AuthenticateUser(string domain, string username, string password, string LdapPath, out string Errmsg)
    {
        log.Debug(" inside AuthenticateUser : " + username);
        Errmsg = "";
        string domainAndUsername = domain + @"\" + username;
        DirectoryEntry entry = new DirectoryEntry(LdapPath, domainAndUsername, password);
        try
        {
            // Bind to the native AdsObject to force authentication.
            Object obj = entry.NativeObject;
            DirectorySearcher search = new DirectorySearcher(entry);
            search.Filter = "(SAMAccountName=" + username + ")";
            search.PropertiesToLoad.Add("cn");
            SearchResult result = search.FindOne();
            if (null == result)
            {
                return false;
            }
            // Update the new path to the user in the directory
            LdapPath = result.Path;
            string _filterAttribute = (String)result.Properties["cn"][0];
        }
        catch (Exception ex)
        {
            Errmsg = ex.Message;
            return false;
            throw new Exception("Error authenticating user." + ex.Message);
        }
        return true;
    }


    public void Login_Click()
    {
        try
        {
            User u = new User();

            Login_DataObject obj1 = new Login_DataObject();
            string mailid = TextBoxUid.Text.Trim();

            string dir_domain = ConfigurationManager.AppSettings["DirectoryDomain"].ToString();

            u = obj1.fnLoginLanOn(mailid + dir_domain);


            if (u.Role != null)
            {
                int a = u.Role;
                Session["User"] = TextBoxUid.Text;
                Session["Role"] = u.Role;
                Session["UserId"] = u.UserId;
                Session["UserName"] = u.UserNamePrefix + " " + u.UserFirstName + " " + u.UserMiddleName + " " + u.UserLastName;
                Session["InstituteId"] = u.InstituteId;
                Session["Department"] = u.Department;
                Session["emailId"] = mailid + dir_domain;
                Session["SupervisorId"] = u.SupervisorId;
                // Session["AutoApproval"] = u.AutoApproved;
                Session["ActiveUser"] = u.Active;
                Session["ProjectUnit"] = u.UnitId;
                Session["RoleName"] = u.Role_Name;
                LblError.Visible = false;
                LblError.Text = "";

                LblError1.Visible = false;
                LblError1.Text = "";
                if (u.Active == "Y")
                {
                    log.Debug("inside the Login_Click if  u.status =A");
                    string b = (System.Web.HttpContext.Current.Request.QueryString["UserId"]);
                    if (b != null)
                    {
                        string Module = Request.QueryString["Module"];
                        if (Module != "" && Module != null)
                        {
                            if (Module == "JA")
                            {
                                Response.Redirect("~/PublicationEntry/PublicationEntry.aspx?Module=" + Module + "", false);
                            }
                            else if (Module == "BK")
                            {
                                Response.Redirect("~/PublicationEntry/PublicationEntry.aspx?Module=" + Module + "", false);
                            }
                            else if (Module == "CP")
                            {
                                Response.Redirect("~/PublicationEntry/PublicationEntry.aspx?Module=" + Module + "", false);
                            }
                            else if (Module == "NM")
                            {
                                Response.Redirect("~/PublicationEntry/PublicationEntry.aspx?Module=" + Module + "", false);
                            }
                            else if (Module == "TS")
                            {
                                Response.Redirect("~/PublicationEntry/PublicationEntry.aspx?Module=" + Module + "", false);
                            }
                            else if (Module == "Patent")
                            {
                                Response.Redirect("~/Patent/Patent.aspx", false);
                            }
                            else if (Module == "Grant")
                            {
                                Response.Redirect("~/GrantEntry/GrantEntry.aspx", false);
                            }
                            //else if (Module == "Conference")
                            //{
                            //    Login_DataObject obj = new Login_DataObject();
                            //    string Enable = obj.GetConferenceMenuForLoginUser(Session["InstituteId"].ToString(), Session["Department"].ToString(), Session["UserId"].ToString());
                            //    if (Enable == "Y")
                            //    {
                            //        Response.Redirect("~/Conference/ConferencePrePresentationEntry.aspx", false);
                            //    }
                            //    else
                            //    {
                            //        ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Currently Conference Entry has been disabled. please contact Directorate of Research (help.rms@manipal.edu)')</script>");
                            //        return;
                            //    }
                            //}
                            else if (Module == "Report")
                            {
                                Response.Redirect("~/PublicationEntry/Reports/PublicationReport.aspx", false);

                            }
                        }
                    }
                    else
                    {
                        if (a == 1)              //redirecting based on conditions
                        {
                            Response.Redirect("~/PublicationEntry/PublicationView.aspx", false);
                        }
                        else if (a == 2)
                        {
                            Response.Redirect("~/ManageJournal/ManageJournal.aspx", false);
                        }
                        else if (a == 3)
                        {
                            Response.Redirect("~/PublicationEntry/PublicationView.aspx", false);
                        }
                        else if (a == 5)
                        {
                            Response.Redirect("~/PublicationEntry/PublicationLibraryUpdate.aspx", false);
                        }
                        else if (a == 6)
                        {
                            //Response.Redirect("~/PublicationEntry.aspx", false);
                            Response.Redirect("~/GrantEntry/GrantEntry.aspx", false);
                        }
                        else if (a == 11)
                        {
                            //Response.Redirect("~/PublicationEntry.aspx", false);
                            Response.Redirect("~/PublicationEntry/PublicationEntry.aspx", false);
                        }
                        else if (a == 8)
                        {
                            Response.Redirect("~/ManageJournal/ManageJournal.aspx", false);
                        }
                        else if (a == 13)
                        {
                            Response.Redirect("~/ManageJournal/ManageJournal.aspx", false);
                        }
                        else if (a == 14)
                        {
                            Response.Redirect("~/PublicationEntry/Reports/PublicationReport.aspx", false);
                        }
                        else if (a == 15)
                        {
                            Response.Redirect("~/PublicationEntry/Reports/PublicationReport.aspx", false);
                        }
                        else if (a == 15)
                        {
                            Response.Redirect("~/PublicationEntry/Reports/PublicationReport.aspx", false);
                        }
                        else if (a == 16)
                        {
                            Response.Redirect("~/GrantEntry/GrantEntry.aspx", false);
                        }
                        //else if (a == 17)
                        //{
                        //    Response.Redirect("~/Incentive/IncentivePointUtilization.aspx", false);
                        //}
                        else if (a == 20)
                        {
                            Response.Redirect("~/GrantEntry/GrantFileUpload.aspx", false);
                        }
                        else if (a == 22)
                        {
                            Response.Redirect("~/PublicationEntry/Reports/PublicationReport.aspx", false);
                        }
                        else
                        {
                            Response.Redirect("~/PublicationEntry/Reports/PublicationReport.aspx", false);
                        }
                    }

                }
                else //displaying error message in case of invalid credentials
                {
                    //  Response.Redirect("~/Login.aspx", false);
                    //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Invalid Credentials Entered........Please Contact Admin')</script>");
                    // ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Invalid Credentials Entered........Please Contact Admin');window.location='" + Request.ApplicationPath + "/Login.aspx';</script>");
                    string unacces = "Invalid Credentials Entered........Please Contact Admin";
                    Response.Redirect("~/Login.aspx?val=" + unacces);
                    //LblError.Visible = true;
                    // LblError.Text = "Invalid Credentials Entered...Try again...";
                }
            }
            else
            {
                //Response.Redirect("~/Login.aspx", false);
                //  ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Invalid User...');window.location='" + Request.ApplicationPath + "~/Login.aspx';</script>");

                LblError1.Visible = true;
                LblError1.Text = "Invalid User...";

            }
        }
        catch (Exception ex)
        {
            log.Error(ex.StackTrace);
            log.Error(ex.Message);

            log.Error("Login Failed...For MailId : " + TextBoxUid.Text);

            ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Login Failed for the user......Please Contact Admin')</script>");

        }

    }

    public void LoginLanOff_Click()
    {
        try
        {
            User u = new User();

            string dir_domain = ConfigurationManager.AppSettings["DirectoryDomain"].ToString();
            Login_DataObject obj1 = new Login_DataObject();
            string mailid = TextBoxUid.Text.Trim();
            string userid = TextBoxPassword.Text.Trim();
            u = obj1.fnLoginLanOff(mailid + dir_domain, userid);// calling login function



            if (u.Role != null)
            {
                int a = u.Role;
                Session["User"] = TextBoxUid.Text;
                Session["Role"] = a;
                Session["UserId"] = u.UserId;
                Session["UserName"] = u.UserNamePrefix + " " + u.UserFirstName + " " + u.UserMiddleName + " " + u.UserLastName;
                Session["InstituteId"] = u.InstituteId;
                Session["Department"] = u.Department;
                Session["emailId"] = mailid + dir_domain;
                Session["SupervisorId"] = u.SupervisorId;
                // Session["AutoApproval"] = u.AutoApproved;
                Session["ActiveUser"] = u.Active;
                Session["ProjectUnit"] = u.UnitId;
                Session["RoleName"] = u.Role_Name;
                LblError.Visible = false;
                LblError.Text = "";

                LblError1.Visible = false;
                LblError1.Text = "";
                if (u.Active == "Y")
                {
                    string Module = Request.QueryString["Module"];
                    if (Module != "" && Module != null)
                    {
                        if (Module == "JA")
                        {
                            Response.Redirect("~/PublicationEntry/PublicationEntry.aspx?Module=" + Module + "", false);
                        }
                        else if (Module == "BK")
                        {
                            Response.Redirect("~/PublicationEntry/PublicationEntry.aspx?Module=" + Module + "", false);
                        }
                        else if (Module == "CP")
                        {
                            Response.Redirect("~/PublicationEntry/PublicationEntry.aspx?Module=" + Module + "", false);
                        }
                        else if (Module == "NM")
                        {
                            Response.Redirect("~/PublicationEntry/PublicationEntry.aspx?Module=" + Module + "", false);
                        }
                        else if (Module == "TS")
                        {
                            Response.Redirect("~/PublicationEntry/PublicationEntry.aspx?Module=" + Module + "", false);
                        }
                        else if (Module == "Patent")
                        {
                            Response.Redirect("~/Patent/Patent.aspx", false);
                        }
                        else if (Module == "Grant")
                        {
                            Response.Redirect("~/GrantEntry/GrantEntry.aspx", false);
                        }
                        //else if (Module == "Conference")
                        //{
                        //    Login_DataObject obj = new Login_DataObject();
                        //    string Enable = obj.GetConferenceMenuForLoginUser(Session["InstituteId"].ToString(), Session["Department"].ToString(), Session["UserId"].ToString());
                        //    if (Enable == "Y")
                        //    {
                        //        Response.Redirect("~/Conference/ConferencePrePresentationEntry.aspx", false);
                        //    }
                        //    else
                        //    {
                        //        ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Currently Conference Entry has been disabled. please contact Directorate of Research (help.rms@manipal.edu)')</script>");
                        //        return;
                        //    }
                        //}
                        else if (Module == "Report")
                        {
                            Response.Redirect("~/PublicationEntry/Reports/PublicationReport.aspx", false);

                        }
                    }
                    else
                    {
                        if (a == 1)                   //redirecting based on conditions
                        {
                            Response.Redirect("~/PublicationEntry/PublicationView.aspx", false);
                        }
                        else if (a == 2)
                        {
                            Response.Redirect("~/ManageJournal/ManageJournal.aspx", false);
                        }
                        else if (a == 3)
                        {
                            Response.Redirect("~/PublicationEntry/PublicationView.aspx", false);
                        }
                        else if (a == 5)
                        {
                            Response.Redirect("~/PublicationEntry/PublicationLibraryUpdate.aspx", false);
                        }
                        else if (a == 6)
                        {
                            //Response.Redirect("~/PublicationEntry.aspx", false);
                            Response.Redirect("~/GrantEntry/GrantEntry.aspx", false);
                        }
                        else if (a == 11)
                        {
                            //Response.Redirect("~/PublicationEntry.aspx", false);

                            Response.Redirect("~/PublicationEntry/PublicationEntry.aspx", false);

                        }
                        else if (a == 8)
                        {
                            Response.Redirect("~/ManageJournal/ManageJournal.aspx", false);
                        }
                        else if (a == 13)
                        {
                            Response.Redirect("~/ManageJournal/ManageJournal.aspx", false);
                        }
                        else if (a == 14)
                        {
                            Response.Redirect("~/PublicationEntry/Reports/PublicationReport.aspx", false);
                        }
                        else if (a == 15)
                        {
                            Response.Redirect("~/PublicationEntry/Reports/PublicationReport.aspx", false);
                        }
                        else if (a == 16)
                        {
                            //Response.Redirect("~/PublicationEntry.aspx", false);
                            Response.Redirect("~/GrantEntry/GrantEntry.aspx", false);
                        }
                        else if (a == 20)
                        {
                            Response.Redirect("~/GrantEntry/GrantFileUpload.aspx", false);
                        }
                        else if (a == 22)
                        {
                            Response.Redirect("~/PublicationEntry/Reports/PublicationReport.aspx", false);
                        }
                        else
                        {
                            Response.Redirect("~/PublicationEntry/Reports/PublicationReport.aspx", false);
                        }
                    }

                }
                else //displaying error message in case of invalid credentials
                {
                    // Response.Redirect("~/Login.aspx",false);

                    //  LblError.Visible = true;
                    //  LblError.Text = "Invalid Credentials Entered...Try again...";

                    string unacces = "Invalid Credentials Entered........Please Contact Admin";
                    Response.Redirect("~/Login.aspx?val=" + unacces);
                }
            }
            else
            {
                LblError1.Visible = true;
                LblError1.Text = "Invalid User...";

            }
        }
        catch (Exception ex)
        {
            log.Error(ex.StackTrace);
            log.Error(ex.Message);

            log.Error("Login Failed...For MailId : " + TextBoxUid.Text);

            ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Login Failed for the user......Please Contact Admin')</script>");

        }
    }

    protected int selectRole(object sender, EventArgs e)
    {

        User u = new User();

        Login_DataObject obj1 = new Login_DataObject();
        string mailid = TextBoxUid.Text.Trim();


        string dir_domain = ConfigurationManager.AppSettings["DirectoryDomain"].ToString();


        u = obj1.fnLoginLanOn(mailid + dir_domain);
        int a = u.Role;
        if (a == 0)
        {
            return 0;
        }



        return 1;
    }



    protected void lnkUsefulLink_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/UsefulLinks.aspx");
        // Response.Write("<script type='text/javascript'>window.open('UsefulLinks.aspx?');</script>");
    }
    protected void lnkStudentapplication_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Loginstudent.aspx");
    }
}