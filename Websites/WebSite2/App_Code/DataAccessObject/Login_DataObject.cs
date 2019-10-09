using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using log4net;

/// <summary>
/// Summary description for AR_DataObject
/// </summary>
public class Login_DataObject
{
    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    public string str;
    public string cmdString;
    public SqlConnection con;
    public SqlCommand cmd;
    SqlTransaction transaction;
	public Login_DataObject()
	{
        str = ConfigurationManager.ConnectionStrings["RMSConnectionString"].ConnectionString;
        cmdString = "";
        con = new SqlConnection(str);
        cmd = new SqlCommand(cmdString, con);
	}


    /* function to log into the system, returns role if user exists-lan OFF*/
    public User fnLoginLanOff(string mailid, string userid)
    {
        log.Debug("inside the fnLogin: mail:" + mailid);
        //cmdString = " SELECT u.User_Id as User_Id ,Prefix,FirstName,MiddleName,LastName,Department_Id,InstituteId,SupervisorId,Active,m.Role_Id as Role_Id,EmailId,AutoApproved FROM [User_M] u,User_Role_Map m WHERE u.User_Id=m.User_Id and EmailId = @ID ";

//cmdString = "SELECT u.User_Id,Name,Department_Id,InstituteId,SupervisorId,Active ,m.Role_Id  as Role_Id ,EmailId,AutoApproved FROM [User_M] u,User_Role_Map m WHERE u.User_Id=m.User_Id and EmailId = @ID AND u.User_Id= @UserID ";
        cmdString = "SELECT u.User_Id,Unit_Id,Prefix,FirstName,MiddleName,LastName,Department_Id,InstituteId,SupervisorId,Active ,m.Role_Id  as Role_Id ,EmailId,AutoApproved,Role_Name FROM [User_M] u,User_Role_Map m,User_AutoApproveMap a,Role_M r WHERE u.User_Id=m.User_Id and a.User_Id=u.User_Id and EmailId = @ID AND u.User_Id= @UserID and r.Role_Id=m.Role_Id ";
       // ctive,m.Role_Id as Role_Id,EmailId,AutoApproved FROM [User_M] u,User_Role_Map m ,User_AutoApproveMap a WHERE u.User_Id=m.User_Id and a.User_Id=u.User_Id and EmailId = @ID
        con = new SqlConnection(str);
        //  string retValue ="0";
        try
        {

            con.Open();
            cmd = new SqlCommand(cmdString, con);
            cmd.Parameters.Add("@ID", SqlDbType.VarChar, 70);
            cmd.Parameters["@ID"].Value = mailid;
            cmd.Parameters.Add("@UserID", SqlDbType.VarChar, 30);
            cmd.Parameters["@UserID"].Value = userid;
            SqlDataReader sdr = cmd.ExecuteReader();
            User p = new User();

            while (sdr.Read())
            {
                if (!Convert.IsDBNull(sdr["User_Id"]))
                {
                    p.UserId = (string)sdr["User_Id"];
                }
                else
                {
                    p.UserId ="";
                }

                if (!Convert.IsDBNull(sdr["Prefix"]))
                {
                    p.UserNamePrefix = (string)sdr["Prefix"];
                }
                else
                {
                    p.UserNamePrefix = "";
                }

                if (!Convert.IsDBNull(sdr["FirstName"]))
                {
                    p.UserFirstName = (string)sdr["FirstName"];
                }
                else
                {
                    p.UserFirstName = "";
                }

                if (!Convert.IsDBNull(sdr["MiddleName"]))
                {
                    p.UserMiddleName = (string)sdr["MiddleName"];
                }
                else
                {
                    p.UserMiddleName = "";
                }

                if (!Convert.IsDBNull(sdr["LastName"]))
                {
                    p.UserLastName = (string)sdr["LastName"];
                }
                else
                {
                    p.UserLastName = "";
                }
                if (!Convert.IsDBNull(sdr["Department_Id"]))
                {
                    p.Department = (string)sdr["Department_Id"];
                }
                else
                {
                    p.Department = "";
                }

                if (!Convert.IsDBNull(sdr["InstituteId"]))
                {
                    p.InstituteId = (string)sdr["InstituteId"];
                }
                else
                {
                    p.InstituteId = "";
                }
                if (!Convert.IsDBNull(sdr["SupervisorId"]))
                {
                    p.SupervisorId = (string)sdr["SupervisorId"];
                }
                else
                {
                    p.SupervisorId = "";
                }
                if (!Convert.IsDBNull(sdr["Active"]))
                {
                    p.Active = (string)sdr["Active"];
                }
                else
                {
                    p.Active = "";
                }
                if (!Convert.IsDBNull(sdr["Role_Id"]))
                {
                    p.Role = (int)sdr["Role_Id"];
                }
                else
                {
                    p.Role = 0;
                }
                if (!Convert.IsDBNull(sdr["AutoApproved"]))
                {
                    p.AutoApproved = (string)sdr["AutoApproved"];
                }
                else
                {
                    p.AutoApproved = "";
                }

                if (!Convert.IsDBNull(sdr["Unit_Id"]))
                {
                    p.UnitId = (string)sdr["Unit_Id"];
                }
                else
                {
                    p.UnitId = "";
                }
                if (!Convert.IsDBNull(sdr["Role_Name"]))
                {
                    p.Role_Name = (string)sdr["Role_Name"];
                }
                else
                {
                    p.Role_Name = "";
                }
                
            }




            return p;
        }
        catch (Exception e)
        {
            log.Debug("Error: Inside catch block of fnLoginLanOff");
            log.Error("Error msg:" + e);
            log.Error("Stack trace:" + e.StackTrace);
            return null;
        }
        finally
        {
            con.Close();
        }
    }

    /* function to log into the system, returns role if user exists*/
    public User fnLoginLanOn(string mailId)
    {
        log.Debug("inside the fnLoginmailid: mail:" + mailId);
      //  cmdString = " SELECT u.User_Id as User_Id ,Name,Department_Id,InstituteId,SupervisorId,Active,m.Role_Id as Role_Id,EmailId,AutoApproved FROM [User_M] u,User_Role_Map m WHERE u.User_Id=m.User_Id and EmailId = @ID ";
        cmdString = " SELECT u.User_Id as User_Id ,Unit_Id,Prefix,FirstName,MiddleName,LastName,Department_Id,InstituteId,SupervisorId,Active,m.Role_Id as Role_Id,EmailId,AutoApproved,Role_Name FROM [User_M] u,User_Role_Map m ,User_AutoApproveMap a,Role_M r WHERE u.User_Id=m.User_Id and a.User_Id=u.User_Id and EmailId = @ID and r.Role_Id=m.Role_Id ";

        con = new SqlConnection(str);
        // string retValue = "0";
        try
        {

            con.Open();
            cmd = new SqlCommand(cmdString, con);
            cmd.Parameters.Add("@ID", SqlDbType.VarChar, 70);
            cmd.Parameters["@ID"].Value = mailId;
            SqlDataReader sdr = cmd.ExecuteReader();
            User p = new User();

            while (sdr.Read())
            {
                if (!Convert.IsDBNull(sdr["User_Id"]))
                {
                    p.UserId = (string)sdr["User_Id"];
                }
                else
                {
                    p.UserId = "";
                }

                if (!Convert.IsDBNull(sdr["Prefix"]))
                {
                    p.UserNamePrefix = (string)sdr["Prefix"];
                }
                else
                {
                    p.UserNamePrefix = "";
                }
                if (!Convert.IsDBNull(sdr["FirstName"]))
                {
                    p.UserFirstName = (string)sdr["FirstName"];
                }
                else
                {
                    p.UserFirstName = "";
                }
                if (!Convert.IsDBNull(sdr["MiddleName"]))
                {
                    p.UserMiddleName = (string)sdr["MiddleName"];
                }
                else
                {
                    p.UserMiddleName = "";
                }
                if (!Convert.IsDBNull(sdr["LastName"]))
                {
                    p.UserLastName = (string)sdr["LastName"];
                }
                else
                {
                    p.UserLastName = "";
                }
                if (!Convert.IsDBNull(sdr["Department_Id"]))
                {
                    p.Department = (string)sdr["Department_Id"];
                }
                else
                {
                    p.Department = "";
                }
                if (!Convert.IsDBNull(sdr["InstituteId"]))
                {
                    p.InstituteId = (string)sdr["InstituteId"];
                }
                else
                {
                    p.InstituteId = "";
                }
                if (!Convert.IsDBNull(sdr["SupervisorId"]))
                {
                    p.SupervisorId = (string)sdr["SupervisorId"];
                }
                else
                {
                    p.SupervisorId = "";
                }
                if (!Convert.IsDBNull(sdr["Active"]))
                {
                    p.Active = (string)sdr["Active"];
                }
                else
                {
                    p.Active = "";
                }
                if (!Convert.IsDBNull(sdr["Role_Id"]))
                {
                    p.Role = (int)sdr["Role_Id"];
                }
                else
                {
                    p.Role = 0;
                }

                if (!Convert.IsDBNull(sdr["AutoApproved"]))
                {
                    p.AutoApproved = (string)sdr["AutoApproved"];
                }
                else
                {
                    p.AutoApproved = "";
                }
                if (!Convert.IsDBNull(sdr["Unit_Id"]))
                {
                    p.UnitId = (string)sdr["Unit_Id"];
                }
                else
                {
                    p.UnitId = "";
                }
                if (!Convert.IsDBNull(sdr["Role_Name"]))
                {
                    p.Role_Name = (string)sdr["Role_Name"];
                }
                else
                {
                    p.Role_Name = "";
                }
            }




            return p;
        }
        catch (Exception e)
        {
            log.Debug("Error: Inside catch block of DynamicMenu");
            log.Error("Error msg:" + e);
            log.Error("Stack trace:" + e.StackTrace);
            return null;
        }
        finally
        {
            con.Close();
        }
    }


    public SqlDataAdapter DynamicMenu(string userid)
    {

        try
        {
            // cmdString = "SELECT l.ID,l.LinkName ,l.URL,l.Link,l.parentID,l.DisplayOrder,l.Value,r.RoleID FROM Link_M l, Role_Link_Map r where l.ID=r.LinkID and RoleID=@RoleID order by displayorder";

            cmdString = "SELECT distinct l.ID,l.LinkName ,l.URL,l.parentID as ParentID,l.DisplayOrder,l.LinkLevel as LinkLevel FROM Link_M l, Role_Link_Map r where l.ID=r.LinkID and RoleID in (Select Role_Id from User_Role_Map where User_Id=@userid) and l.Active='Y' order by displayorder";

            con = new SqlConnection(str);
            con.Open();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmdString, con);
            da.SelectCommand.Parameters.AddWithValue("@userid", userid);
            return da;
        }

        catch (Exception e)
        {
            log.Debug("Error: Inside catch block of DynamicMenu");
            log.Error("Error msg:" + e);
            log.Error("Stack trace:" + e.StackTrace);
            return null;
        }
        finally
        {
            con.Close();
        }
    }


    public SqlDataAdapter DynamicMenu1(string userid, string pid)
    {

        try
        {
            // cmdString = "SELECT l.ID,l.LinkName ,l.URL,l.Link,l.parentID,l.DisplayOrder,l.Value,r.RoleID FROM Link_M l, Role_Link_Map r where l.ID=r.LinkID and RoleID=@RoleID order by displayorder";

            cmdString = "SELECT distinct l.ID,l.LinkName ,l.URL,l.parentID as ParentID,l.DisplayOrder,l.LinkLevel as LinkLevel  FROM Link_M l, Role_Link_Map r where l.ID=r.LinkID and RoleID in (Select Role_Id from User_Role_Map where User_Id=@userid) and ParentID=@ParentID and l.Active='Y' order by displayorder";

            con = new SqlConnection(str);
            con.Open();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmdString, con);
            da.SelectCommand.Parameters.AddWithValue("@userid", userid);
            da.SelectCommand.Parameters.AddWithValue("@ParentID", pid);
            return da;
        }

        catch (Exception e)
        {
            log.Debug("Error: Inside catch block of DynamicMenu");
            log.Error("Error msg:" + e);
            log.Error("Stack trace:" + e.StackTrace);
            return null;
        }
        finally
        {
            con.Close();
        }
    }




    public int InsertUserUploadExcel(User[] jd,string path)
    {
        log.Debug("Inside InsertUserUploadExcel ");

        int result = 0;
        String EmployeeFields = "";

        con = new SqlConnection(str);
        con.Open();
        transaction = con.BeginTransaction();
        int countA = 0;
        int countM = 0;
    
        try
        {



            for (int i = 0; i < jd.Length; i++)
            {
                EmployeeFields = "";
                if (jd[i].EntryStatus == "A")
                {
                    countA++;
                     cmd = new SqlCommand("InsertUser", con, transaction);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@userid", HttpUtility.HtmlDecode(jd[i].User_Id));
                    EmployeeFields += "$MaheId = " + jd[i].User_Id + "+";
                    cmd.Parameters.AddWithValue("@Prefix", HttpUtility.HtmlDecode(jd[i].UserNamePrefix));
                    cmd.Parameters.AddWithValue("@FirstName", HttpUtility.HtmlDecode(jd[i].UserFirstName));
                    cmd.Parameters.AddWithValue("@MiddleName", HttpUtility.HtmlDecode(jd[i].UserMiddleName));
                    cmd.Parameters.AddWithValue("@LastName", HttpUtility.HtmlDecode(jd[i].UserLastName));
                    //cmd.Parameters.AddWithValue("@name", b.Name);
                    cmd.Parameters.AddWithValue("@department_Id", HttpUtility.HtmlDecode(jd[i].Department));
                    cmd.Parameters.AddWithValue("@instituteId", HttpUtility.HtmlDecode(jd[i].InstituteId));
                    cmd.Parameters.AddWithValue("@emailId", HttpUtility.HtmlDecode(jd[i].emailId));
                    cmd.Parameters.AddWithValue("@autoApproved", "Y");
                    cmd.Parameters.AddWithValue("@roleid", "11");
                    cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
                    cmd.Parameters.AddWithValue("@createdBy", jd[i].CreatedBy);
                    result = cmd.ExecuteNonQuery();
                }
                else   if (jd[i].EntryStatus == "M")
                {
                    countM++;
                    int Role_Id = 0;
                    cmdString = "select Role_Id from User_Role_Map where User_Id=@User_Id ";


                    
                    cmd = new SqlCommand(cmdString, con,transaction);
                    cmd.Parameters.Add("@User_Id", SqlDbType.VarChar, 12);
                    cmd.Parameters["@User_Id"].Value = HttpUtility.HtmlDecode(jd[i].User_Id);

                    EmployeeFields += "$MaheId = " + jd[i].User_Id + "+";
                    Role_Id = (int)cmd.ExecuteScalar();


                


                    // if user is not research coordinator
                    if (Role_Id !=1)
                    {
                        cmdString = "update User_M set EmailId=@EmailId ,Prefix=@Prefix,FirstName=@FirstName,MiddleName=@MiddleName,LastName=@LastName,Department_Id=@Department_Id,InstituteId=@InstituteId,SupervisorId=@SupervisorId where User_Id=@User_Id";
                        cmd = new SqlCommand(cmdString, con, transaction);
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@User_Id", HttpUtility.HtmlDecode(jd[i].User_Id));
                        cmd.Parameters.AddWithValue("@Prefix", HttpUtility.HtmlDecode(jd[i].UserNamePrefix));
                        cmd.Parameters.AddWithValue("@FirstName", HttpUtility.HtmlDecode(jd[i].UserFirstName));
                        cmd.Parameters.AddWithValue("@MiddleName", HttpUtility.HtmlDecode(jd[i].UserMiddleName));
                        cmd.Parameters.AddWithValue("@LastName", HttpUtility.HtmlDecode(jd[i].UserLastName));
                        //cmd.Parameters.AddWithValue("@name", b.Name);
                        cmd.Parameters.AddWithValue("@Department_Id", HttpUtility.HtmlDecode(jd[i].Department));
                        cmd.Parameters.AddWithValue("@InstituteId", HttpUtility.HtmlDecode(jd[i].InstituteId));
                        cmd.Parameters.AddWithValue("@EmailId", HttpUtility.HtmlDecode(jd[i].emailId));

                        cmd.Parameters.AddWithValue("@SupervisorId",HttpUtility.HtmlDecode(jd[i].SupervisorId));

                        result = cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        // if user is  research coordinator

                        cmdString = "select InstituteId,Department_Id from User_M where User_Id=@UserId";
                        cmd = new SqlCommand(cmdString, con, transaction);
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@UserId", HttpUtility.HtmlDecode(jd[i].User_Id));


                        SqlDataReader sdr1 = cmd.ExecuteReader();

                        string Currntinst = null,Currentdept=null;

                        while (sdr1.Read())
                        {
                            if (!Convert.IsDBNull(sdr1["InstituteId"]))
                            {
                                Currntinst = (string)sdr1["InstituteId"];
                            }
                            if (!Convert.IsDBNull(sdr1["Department_Id"]))
                            {
                                Currentdept = (string)sdr1["Department_Id"];
                            }
                        }
                        sdr1.Close();

                        // if user is  research coordinator----if inst/dept is equal to modified inst/dept in the file
                        if (Currntinst == HttpUtility.HtmlDecode(jd[i].InstituteId) && Currentdept == HttpUtility.HtmlDecode(jd[i].Department))
                        {
                            
                            cmdString = "update User_M set EmailId=@EmailId ,Prefix=@Prefix,FirstName=@FirstName,MiddleName=@MiddleName,LastName=@LastName,Department_Id=@Department_Id,InstituteId=@InstituteId,SupervisorId=@SupervisorId where User_Id=@User_Id";
                            cmd = new SqlCommand(cmdString, con, transaction);
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.AddWithValue("@User_Id", HttpUtility.HtmlDecode(jd[i].User_Id));
                            cmd.Parameters.AddWithValue("@Prefix", HttpUtility.HtmlDecode(jd[i].UserNamePrefix));
                            cmd.Parameters.AddWithValue("@FirstName", HttpUtility.HtmlDecode(jd[i].UserFirstName));
                            cmd.Parameters.AddWithValue("@MiddleName", HttpUtility.HtmlDecode(jd[i].UserMiddleName));
                            cmd.Parameters.AddWithValue("@LastName", HttpUtility.HtmlDecode(jd[i].UserLastName));
                            //cmd.Parameters.AddWithValue("@name", b.Name);
                            cmd.Parameters.AddWithValue("@Department_Id", HttpUtility.HtmlDecode(jd[i].Department));
                            cmd.Parameters.AddWithValue("@InstituteId", HttpUtility.HtmlDecode(jd[i].InstituteId));
                            cmd.Parameters.AddWithValue("@EmailId", HttpUtility.HtmlDecode(jd[i].emailId));

                            cmd.Parameters.AddWithValue("@SupervisorId", HttpUtility.HtmlDecode(jd[i].SupervisorId));

                            result = cmd.ExecuteNonQuery();
                        }
                        else
                        {

                            // if user is  research coordinator----if inst/dept is not equal to modified inst/dept in the file


                            cmdString = "select COUNT(*) as entrynum from Publication_InchargerM where UserId=@UserId";
                            cmd = new SqlCommand(cmdString, con, transaction);
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.AddWithValue("@UserId", HttpUtility.HtmlDecode(jd[i].User_Id));


                            SqlDataReader sdr = cmd.ExecuteReader();

                            int entrynum = 0;

                            while (sdr.Read())
                            {
                                if (!Convert.IsDBNull(sdr["entrynum"]))
                                {
                                    entrynum = (int)sdr["entrynum"];
                                }

                            }
                            sdr.Close();


                            //pubinchargem inactive

                            cmdString = "update  Publication_InchargerM set Active=@Active,UpdatedBy=@UpdatedBy,UpdatedDate=@UpdatedDate where  UserId= @UserId1 and EntryNum=@entrynum";
                            SqlCommand cmd11 = new SqlCommand(cmdString, con, transaction);
                            cmd11.CommandType = CommandType.Text;
                            cmd11.Parameters.AddWithValue("@Active", "N");
                            cmd11.Parameters.AddWithValue("@UserId1", HttpUtility.HtmlDecode(jd[i].User_Id));
                            cmd11.Parameters.AddWithValue("@entrynum", entrynum);
                            cmd11.Parameters.AddWithValue("@UpdatedBy", HttpContext.Current.Session["UserId"].ToString());
                            cmd11.Parameters.AddWithValue("@UpdatedDate", DateTime.Now);
                        
                            result = cmd11.ExecuteNonQuery();

                            cmdString = "update User_Role_Map set Role_Id=@Role_Id where User_Id=@User_Id";
                            cmd = new SqlCommand(cmdString, con, transaction);
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.AddWithValue("@User_Id", HttpUtility.HtmlDecode(jd[i].User_Id));
                            cmd.Parameters.AddWithValue("@Role_Id", "11");


                            result = cmd.ExecuteNonQuery();



                            cmdString = "update User_M set EmailId=@EmailId ,Prefix=@Prefix,FirstName=@FirstName,MiddleName=@MiddleName,LastName=@LastName,Department_Id=@Department_Id,InstituteId=@InstituteId,SupervisorId=@SupervisorId where User_Id=@User_Id";
                            cmd = new SqlCommand(cmdString, con, transaction);
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.AddWithValue("@User_Id", HttpUtility.HtmlDecode(jd[i].User_Id));
                            cmd.Parameters.AddWithValue("@Prefix", HttpUtility.HtmlDecode(jd[i].UserNamePrefix));
                            cmd.Parameters.AddWithValue("@FirstName", HttpUtility.HtmlDecode(jd[i].UserFirstName));
                            cmd.Parameters.AddWithValue("@MiddleName", HttpUtility.HtmlDecode(jd[i].UserMiddleName));
                            cmd.Parameters.AddWithValue("@LastName", HttpUtility.HtmlDecode(jd[i].UserLastName));
                       
                            cmd.Parameters.AddWithValue("@Department_Id", HttpUtility.HtmlDecode(jd[i].Department));
                            cmd.Parameters.AddWithValue("@InstituteId", HttpUtility.HtmlDecode(jd[i].InstituteId));
                            cmd.Parameters.AddWithValue("@EmailId", HttpUtility.HtmlDecode(jd[i].emailId));

                            cmd.Parameters.AddWithValue("@SupervisorId", HttpUtility.HtmlDecode(jd[i].SupervisorId));

                            result = cmd.ExecuteNonQuery();
                        }
                    }

                    

                }
             


            }
            cmdString = "Insert into HRDataTracker (Date,NoOfNewRecord,NoOfModifiedRecord,ExcelPath,UserId) values(@Date,@NoOfNewRecord,@NoOfModifiedRecord,@ExcelPath,@UserId)";
            cmd = new SqlCommand(cmdString, con, transaction);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@Date", DateTime.Now);
            cmd.Parameters.AddWithValue("@NoOfNewRecord", countA);
            cmd.Parameters.AddWithValue("@NoOfModifiedRecord", countM);
            if (path == null)
            {
                cmd.Parameters.AddWithValue("@ExcelPath", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@ExcelPath", path);
            }
            cmd.Parameters.AddWithValue("@UserId", HttpContext.Current.Session["UserId"].ToString());
            result = cmd.ExecuteNonQuery();
            transaction.Commit();
            return result;
        }

        catch (Exception e)
        {
            log.Error("Inside - InsertUserUploadExcel catch block ");
            log.Error(e.Message);
            log.Error(e.StackTrace);
            String msg = e.Message + "Employee fileds = " + EmployeeFields;
            transaction.Rollback();
            throw new Exception(msg);

        }
        finally
        {
            cmd.Dispose();
            con.Close();
            cmd.Dispose();


        }
    }




    public User fnLoginLanOnStudent(string RollNumber)
    {
        log.Debug("inside the fnLoginLanOnSudent: RollNumber:" + RollNumber);
        //  cmdString = " SELECT u.User_Id as User_Id ,Name,Department_Id,InstituteId,SupervisorId,Active,m.Role_Id as Role_Id,EmailId,AutoApproved FROM [User_M] u,User_Role_Map m WHERE u.User_Id=m.User_Id and EmailId = @ID ";
        cmdString = "select Name, ClassName,EmailID1,c.ClassCode,InstName,c.InstID, HRInstitute,DOB,Role_Id from SISStudentGenInfo c,Role_M a,SISInstnHR b,SISClass d,SISInstitution e where RollNo=@RollNumber and Role_Id='21' and c.InstID=b.Institute_Id and c.ClassCode=d.ClassCode and e.InstID=c.InstID";

        con = new SqlConnection(str);
          try
        {

            con.Open();
            cmd = new SqlCommand(cmdString, con);
            cmd.Parameters.Add("@RollNumber", SqlDbType.VarChar, 70);
            cmd.Parameters["@RollNumber"].Value = RollNumber;
            SqlDataReader sdr = cmd.ExecuteReader();
            User p = new User();

            while (sdr.Read())
            {
                if (!Convert.IsDBNull(sdr["Name"]))
                {
                    p.StudentName = (string)sdr["Name"];
                }
                else
                {
                    p.StudentName = "";
                }

                if (!Convert.IsDBNull(sdr["ClassName"]))
                {
                    p.StudentClassName = (string)sdr["ClassName"];
                }
                else
                {
                    p.StudentClassName = "";
                }
                if (!Convert.IsDBNull(sdr["HRInstitute"]))
                {
                    p.StudentInstCode = (string)sdr["HRInstitute"];
                }
                else
                {
                    p.StudentInstCode = "";
                }
                if (!Convert.IsDBNull(sdr["ClassCode"]))
                {
                    p.StudentClassCode = (string)sdr["ClassCode"];
                }
                else
                {
                    p.StudentClassCode = "";
                }
                if (!Convert.IsDBNull(sdr["InstName"]))
                {
                    p.StudentInstName = (string)sdr["InstName"];
                }
                else
                {
                    p.StudentInstName = "";
                }
                if (!Convert.IsDBNull(sdr["EmailID1"]))
                {
                    p.EmailId1 = (string)sdr["EmailID1"];
                }
                else
                {
                    p.EmailId1 = "";
                }
                if (!Convert.IsDBNull(sdr["DOB"]))
                {
                    p.StudentDOB = (DateTime)sdr["DOB"];
                }
                //else
                //{
                //    p.StudentDOB ="";
                //}
                if (!Convert.IsDBNull(sdr["Role_Id"]))
                {
                    p.Role = (int)sdr["Role_Id"];
                }
                else
                {
                    p.Role = 0;
                }

            }
            return p;
        }
        catch (Exception e)
        {
            log.Debug("Error: Inside catch block of fnLoginLanOnSudent");
            log.Error("Error msg:" + e);
            log.Error("Stack trace:" + e.StackTrace);
            return null;
        }
        finally
        {
            con.Close();
        }
    }

    public User CheckUserNamePassword(string SUserName)
    {
        log.Debug("inside the CheckUserNamePassword: RollNumber:" + SUserName);
        //  cmdString = " SELECT u.User_Id as User_Id ,Name,Department_Id,InstituteId,SupervisorId,Active,m.Role_Id as Role_Id,EmailId,AutoApproved FROM [User_M] u,User_Role_Map m WHERE u.User_Id=m.User_Id and EmailId = @ID ";
        cmdString = "    select  CAST(DOB AS DATE)as DOB from SISStudentGenInfo where RollNo=@RollNumber";

        con = new SqlConnection(str);
        try
        {

            con.Open();
            cmd = new SqlCommand(cmdString, con);
            cmd.Parameters.Add("@RollNumber", SqlDbType.VarChar, 70);
            cmd.Parameters["@RollNumber"].Value = SUserName;
            SqlDataReader sdr = cmd.ExecuteReader();
            User p = new User();

            while (sdr.Read())
            {
                if (!Convert.IsDBNull(sdr["DOB"]))
                {
                    p.StudentDOB = (DateTime)sdr["DOB"];
                }         
            }
            return p;
        }
        catch (Exception e)
        {
            log.Debug("Error: Inside catch block of CheckUserNamePassword");
            log.Error("Error msg:" + e);
            log.Error("Stack trace:" + e.StackTrace);
            return null;
        }
        finally
        {
            con.Close();
        }
    }

    public SqlDataAdapter DynamicMenuStudent(int role)
    {
        try
        {
            // cmdString = "SELECT l.ID,l.LinkName ,l.URL,l.Link,l.parentID,l.DisplayOrder,l.Value,r.RoleID FROM Link_M l, Role_Link_Map r where l.ID=r.LinkID and RoleID=@RoleID order by displayorder";

            cmdString = "   SELECT distinct l.ID,l.LinkName ,l.URL,l.parentID as ParentID,l.DisplayOrder,l.LinkLevel as LinkLevel FROM Link_M l, Role_Link_Map r where l.ID=r.LinkID and RoleID=@role ";

            con = new SqlConnection(str);
            con.Open();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmdString, con);
            da.SelectCommand.Parameters.AddWithValue("@role", role);
            return da;
        }

        catch (Exception e)
        {
            log.Debug("Error: Inside catch block of DynamicMenu");
            log.Error("Error msg:" + e);
            log.Error("Stack trace:" + e.StackTrace);
            return null;
        }
        finally
        {
            con.Close();
        }
    }

    public SqlDataAdapter DynamicMenuStudent1(string Role, string p)
    {
        try
        {
            // cmdString = "SELECT l.ID,l.LinkName ,l.URL,l.Link,l.parentID,l.DisplayOrder,l.Value,r.RoleID FROM Link_M l, Role_Link_Map r where l.ID=r.LinkID and RoleID=@RoleID order by displayorder";

              //cmdString = "SELECT distinct l.ID,l.LinkName ,l.URL,l.parentID as ParentID,l.DisplayOrder,l.LinkLevel as LinkLevel  FROM Link_M l, Role_Link_Map r where l.ID=r.LinkID and RoleID='" + Role + "' and ParentID=@ParentID ";
            cmdString = "SELECT distinct l.ID,l.LinkName ,l.URL,l.parentID as ParentID,l.DisplayOrder,l.LinkLevel as LinkLevel  FROM Link_M l, Role_Link_Map r where l.ID=r.LinkID and RoleID=@Role  and ParentID=@ParentID ";

           con = new SqlConnection(str);
            con.Open();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmdString, con);
            //da.SelectCommand.Parameters.AddWithValue("@userid", userid);
            da.SelectCommand.Parameters.AddWithValue("@ParentID", p);
            da.SelectCommand.Parameters.AddWithValue("@Role", Role);
            return da;
        }

        catch (Exception e)
        {
            log.Debug("Error: Inside catch block of DynamicMenuStudent1");
            log.Error("Error msg:" + e);
            log.Error("Stack trace:" + e.StackTrace);
            return null;
        }
        finally
        {
            con.Close();
        }
    }

    public string GetConferenceMenuForLoginUser(string InstituteId, string Department,string userid)
    {
        log.Debug("inside the GetConferenceMenuForLoginUser: Userid:" + userid);
        //  cmdString = " SELECT u.User_Id as User_Id ,Name,Department_Id,InstituteId,SupervisorId,Active,m.Role_Id as Role_Id,EmailId,AutoApproved FROM [User_M] u,User_Role_Map m WHERE u.User_Id=m.User_Id and EmailId = @ID ";
        //cmdString = " select Enabled from Conference_Inst_M where Institute_Id='"+InstituteId+"'";
        cmdString = " select Enabled from Conference_Inst_M where Institute_Id=@InstituteId";


        con = new SqlConnection(str);
        try
        {

            con.Open();
            cmd = new SqlCommand(cmdString, con);

            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@InstituteId", InstituteId);
            SqlDataReader sdr = cmd.ExecuteReader();
            string Enabled = null;

            while (sdr.Read())
            {
                if (!Convert.IsDBNull(sdr["Enabled"]))
                {
                    Enabled = (string)sdr["Enabled"];
                }
            }
            return Enabled;
        }
        catch (Exception e)
        {
            log.Debug("Error: Inside catch block of GetConferenceMenuForLoginUser");
            log.Error("Error msg:" + e);
            log.Error("Stack trace:" + e.StackTrace);
            return null;
        }
        finally
        {
            con.Close();
        }
    }

    public SqlDataAdapter DynamicMenuDisableConferenceMenu(string userid)
    {
        try
        {
            // cmdString = "SELECT l.ID,l.LinkName ,l.URL,l.Link,l.parentID,l.DisplayOrder,l.Value,r.RoleID FROM Link_M l, Role_Link_Map r where l.ID=r.LinkID and RoleID=@RoleID order by displayorder";

            cmdString = "SELECT distinct l.ID,l.LinkName ,l.URL,l.parentID as ParentID,l.DisplayOrder,l.LinkLevel as LinkLevel FROM Link_M l, Role_Link_Map r where l.ID=r.LinkID and RoleID in (Select Role_Id from User_Role_Map where User_Id=@userid) and l.Active='Y'and  (l.ID!='L155') order by displayorder";

            con = new SqlConnection(str);
            con.Open();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmdString, con);
            da.SelectCommand.Parameters.AddWithValue("@userid", userid);
            return da;
        }

        catch (Exception e)
        {
            log.Debug("Error: Inside catch block of DynamicMenuDisableConferenceMenu");
            log.Error("Error msg:" + e);
            log.Error("Stack trace:" + e.StackTrace);
            return null;
        }
        finally
        {
            con.Close();
        }
    }
}