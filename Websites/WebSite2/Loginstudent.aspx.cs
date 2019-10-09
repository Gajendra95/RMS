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


public partial class Loginstudent : System.Web.UI.Page
{
    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            Session["UserName"] = "";
            Session["Role"] = "";
            Session["User"] = "";
            Session["ClassCode"] ="";
            Session["InstituteId"] ="";
            Session["StudentDOB"] = "";
        }
        lblunauthoaccess.Text = "";
    }

    public bool AuthenticateUser(string domain, string username, string password, string LdapPath, out string Errmsg)
    {
        log.Debug(" inside AuthenticateUser : " + username);
        Errmsg = "";
        string domainAndUsername =username;
        User u = new User();

        Login_DataObject obj1 = new Login_DataObject();
        DirectoryEntry entry = new DirectoryEntry(LdapPath, domainAndUsername, password);
        try
        {
            string SUserName = domainAndUsername;
            string SPassword = password;
            SPassword = SPassword.Replace("/", "").Replace(",", "").Replace("-", "");
            u = obj1.CheckUserNamePassword(SUserName);
            string studenttablepassword = u.StudentDOB.ToShortDateString(); ;
            studenttablepassword = studenttablepassword.Replace("/", "").Replace(",", "").Replace("-", "");
            //DirectorySearcher search = new DirectorySearcher(entry);
            //search.Filter = "(SAMAccountName=" + username + ")";
            //search.PropertiesToLoad.Add("cn");
            //SearchResult result = search.FindOne();
            if (SPassword != studenttablepassword)
            {
                LblError.Visible = true;
                LblError.Text = "Invalid RollNumber or Password!";
                return false;
            }
            else
            {
                return true;
            }
            //LdapPath = result.Path;
            //string _filterAttribute = (String)result.Properties["cn"][0];

             ////Object obj = entry.NativeObject;
             //DirectorySearcher search = new DirectorySearcher(entry);
             //search.Filter = "(SAMAccountName=" + username + ")";
             //search.PropertiesToLoad.Add("cn");
             //SearchResult result = search.FindOne();
             //if (null == result)
             //{
             //    return false;
             //}
             //// Update the new path to the user in the directory
             //LdapPath = result.Path;
             //string _filterAttribute = (String)result.Properties["cn"][0];
        }
        catch (Exception ex)
        {
            Errmsg = ex.Message;
            return false;
            throw new Exception("Error authenticating user." + ex.Message);
        }
       
    }

 
    public void Login_Click()
    {
        try
        {
            User u = new User();

            Login_DataObject obj1 = new Login_DataObject();
            string StudentRollNO = TextBoxURollNO.Text.Trim();

            string dir_domain = ConfigurationManager.AppSettings["DirectoryDomain"].ToString();

            u = obj1.fnLoginLanOnStudent(StudentRollNO);


            if (u.StudentName != null)
            {
                int a = u.Role;
                Session["UserName"] = u.StudentName;
                Session["Role"] = u.Role;
                Session["User"] = TextBoxURollNO.Text;
                Session["Department"] = u.StudentClassName;
                Session["DepartmentId"] = u.StudentClassCode;
                Session["InstituteId"] = u.StudentInstCode;
                Session["InstituteName"] = u.StudentInstName;
                Session["StudentDOB"] = u.StudentDOB;
                Session["emailId"] = u.EmailId1;
                Session["UserId"] = TextBoxURollNO.Text;
                LblError.Visible = false;
                LblError.Text = "";

                if (u.Role != null)
                {
                    if (a == 21)              //redirecting based on conditions
                    {
                        Response.Redirect("~/GrantEntry/SeedMoneyforStudent.aspx", false);
                    }
                    else //displaying error message in case of invalid credentials
                    {

                        string unacces = "Invalid Credentials Entered........Please Contact Admin";
                        Response.Redirect("~/Loginstudent.aspx?val=" + unacces);

                    }
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

            log.Error("Login Failed...For MailId : " + TextBoxURollNO.Text);

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
            string mailid = TextBoxURollNO.Text.Trim();
            string userid = TextBoxPassword.Text.Trim();
            u = obj1.fnLoginLanOff(mailid + dir_domain, userid);// calling login function



            if (u.Role != null)
            {
                int a = u.Role;
                Session["User"] = TextBoxURollNO.Text.Trim();
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
                    else
                    {
                        Response.Redirect("~/PublicationEntry/Reports/PublicationReport.aspx", false);
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

            log.Error("Login Failed...For MailId : " + TextBoxURollNO.Text);

            ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Login Failed for the user......Please Contact Admin')</script>");
        
        }
    }

  

    //protected void lnkUsefulLink_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect("~/UsefulLinks.aspx");
    //   // Response.Write("<script type='text/javascript'>window.open('UsefulLinks.aspx?');</script>");
    //}

    protected void BtnLoginstudent_Click(object sender, ImageClickEventArgs e)
    {
        if (!Page.IsValid)
        {
            return;
        }

        string lan = ConfigurationManager.AppSettings["LAN"].ToString();
        string dominName = string.Empty;
        string adPath = string.Empty;
        string userName = TextBoxURollNO.Text.Trim();
        string strError = string.Empty;

        //if (lan == "OFF")
        //{
            log.Debug(" inside btnLogin_Click--Lan-OFF : " + TextBoxURollNO.Text.Trim());
            //    LoginLanOff_Click();
            //}

            //else
            //{
            //    log.Debug(" inside btnLogin_Click--Lan-ON : " + TextBoxURollNO.Text.Trim());

            int flag = 0;
            int ret = selectRoleStudent(sender, e);


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
                            if (true == AuthenticateUser(dominName, userName, TextBoxPassword.Text.Trim(), adPath, out strError))
                            {
                                FormsAuthentication.RedirectFromLoginPage(TextBoxURollNO.Text, true);
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

    //}
    }

    private int selectRoleStudent(object sender, ImageClickEventArgs e)
    {

        User u = new User();

        Login_DataObject obj1 = new Login_DataObject();
        string RollNumber = TextBoxURollNO.Text.Trim();

        u = obj1.fnLoginLanOnStudent(RollNumber);
        //if (u.StudentName != "")
        //{

        //}
        int a = u.Role;
        if (a == 0)
        {
            return 0;
        }



        return 1;
    }
}