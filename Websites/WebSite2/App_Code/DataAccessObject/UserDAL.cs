using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
/// <summary>
/// Summary description for UserDAL
/// </summary>
public class UserDAL
{
    public string cmdString1;
    SqlTransaction transaction;

    public class myConnection
    {
        public string str;
        public string cmdString;
        public SqlConnection con;
        public SqlCommand cmd;
        public SqlCommand cmd1;
        public SqlCommand cmd2;

        public string HRstr;
        public string HRcmdString;
        public SqlConnection HRcon;
        public SqlCommand HRcmd;
        /* constructor*/
        public myConnection()
        {
            cmdString = "";
            con = new SqlConnection(str);
            cmd = new SqlCommand(cmdString, con);
            HRcmdString = "";
            HRcmd = new SqlCommand(HRcmdString, HRcon);
        }
    }
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["RMSConnectionString"].ConnectionString);
    SqlConnection HRcon = new SqlConnection(ConfigurationManager.ConnectionStrings["HRDataConStr"].ConnectionString);
    public Int32 InsertDepartment(User b)
    {
        int result = 0, result1 = 0;
        try
        {
            con.Open();
            transaction = con.BeginTransaction();
            SqlCommand cmd = new SqlCommand("InsertUser", con, transaction);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@userid", b.User_Id);
            cmd.Parameters.AddWithValue("@Prefix", b.UserNamePrefix);
            cmd.Parameters.AddWithValue("@FirstName", b.UserFirstName);
            cmd.Parameters.AddWithValue("@MiddleName", b.UserMiddleName);
            cmd.Parameters.AddWithValue("@LastName", b.UserLastName);
            //cmd.Parameters.AddWithValue("@name", b.Name);
            cmd.Parameters.AddWithValue("@department_Id", b.Department_Id);
            cmd.Parameters.AddWithValue("@instituteId", b.InstituteId);
            cmd.Parameters.AddWithValue("@emailId", b.EmailId);
            cmd.Parameters.AddWithValue("@autoApproved", b.AutoApproved);
            cmd.Parameters.AddWithValue("@roleid", b.Role_Id);
            cmd.Parameters.AddWithValue("@CreatedDate", b.CreatedDate);
            cmd.Parameters.AddWithValue("@createdBy", b.CreatedBy);




            result = cmd.ExecuteNonQuery();
            if (result >= 1)
            {
                if (b.Role_Id == 1)
                {
                    cmdString1 = "select COUNT(*) as entrynum from Publication_InchargerM where UserId=@UserId";
                    cmd = new SqlCommand(cmdString1, con, transaction);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@UserId", b.User_Id);


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

                    cmdString1 = "insert into Publication_InchargerM (UserId,EntryNum,InstituteId,Department_Id ,Active,CreatedBy,CreatedDate) values(@UserId1,@EntryNum,@InstituteId1,@Department_Id1,@Active,@CreatedBy,@CreatedDate)";
                    SqlCommand cmd1 = new SqlCommand(cmdString1, con, transaction);
                    cmd1.CommandType = CommandType.Text;
                    cmd1.Parameters.AddWithValue("@UserId1", b.User_Id);
                    cmd1.Parameters.AddWithValue("@EntryNum", entrynum + 1);
                    cmd1.Parameters.AddWithValue("@InstituteId1", b.InstituteId);
                    cmd1.Parameters.AddWithValue("@Department_Id1", b.pubinchargedept);


                    cmd1.Parameters.AddWithValue("@Active", "Y");
                    cmd1.Parameters.AddWithValue("@CreatedBy", HttpContext.Current.Session["UserId"].ToString());

                    cmd1.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
                    //cmd1.Parameters.AddWithValue("@UpdatedBy", HttpContext.Current.Session["UserId"].ToString());

                    // cmd1.Parameters.AddWithValue("@UpdatedDate", DateTime.Now);



                    result1 = cmd1.ExecuteNonQuery();
                }

            }
            transaction.Commit();
            return result;
        }
        catch (Exception e)
        {
            log.Debug("Error: Inside catch block of insert user");
            log.Error("Error msg:" + e);
            log.Error("Stack trace:" + e.StackTrace);
            transaction.Rollback();
            return 0;

        }
        finally
        {
            if (con.State != ConnectionState.Closed)
            {
                con.Close();
            }
        }
    }

    public int Update(User b)
    {

        try
        {
            int result1 = 0, result2 = 0, result3 = 0;

            // con = new SqlConnection(str);
            con.Open();

            SqlCommand cmd = new SqlCommand("Update_UserRole", con);
            cmd.Parameters.AddWithValue("@userid", b.User_Id);
            cmd.Parameters.AddWithValue("@roleid", b.Role_Id);
            //cmd.Parameters.AddWithValue("@role_name", b.Role_Name);
            cmd.CommandType = CommandType.StoredProcedure;
            result1 = cmd.ExecuteNonQuery();
            if (result1 >= 1)
            {
                cmdString1 = "select COUNT(*) as entrynum from Publication_InchargerM where UserId=@UserId";
                cmd = new SqlCommand(cmdString1, con, transaction);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@UserId", b.User_Id);


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
                if (b.Role_Id == 1)
                {

                    cmdString1 = "update  Publication_InchargerM set Active=@Active,UpdatedBy=@UpdatedBy,UpdatedDate=@UpdatedDate where  UserId= @UserId1 and EntryNum=@entrynum";
                    SqlCommand cmd11 = new SqlCommand(cmdString1, con, transaction);
                    cmd11.CommandType = CommandType.Text;
                    cmd11.Parameters.AddWithValue("@Active", "N");
                    cmd11.Parameters.AddWithValue("@UserId1", b.User_Id);
                    cmd11.Parameters.AddWithValue("@entrynum", entrynum);
                    cmd11.Parameters.AddWithValue("@UpdatedBy", HttpContext.Current.Session["UserId"].ToString());
                    cmd11.Parameters.AddWithValue("@UpdatedDate", DateTime.Now);
                    //cmd11.Parameters.AddWithValue("@InstituteId1", b.InstituteId);
                    // cmd11.Parameters.AddWithValue("@Department_Id1", b.pubinchargedept);
                    result3 = cmd11.ExecuteNonQuery();

                    cmdString1 = "insert into Publication_InchargerM (UserId,EntryNum,InstituteId,Department_Id ,Active,CreatedBy,CreatedDate) values(@UserId1,@EntryNum,@InstituteId1,@Department_Id1,@Active,@CreatedBy,@CreatedDate)";
                    SqlCommand cmd1 = new SqlCommand(cmdString1, con, transaction);
                    cmd1.CommandType = CommandType.Text;
                    cmd1.Parameters.AddWithValue("@UserId1", b.User_Id);
                    cmd1.Parameters.AddWithValue("@EntryNum", entrynum + 1);
                    cmd1.Parameters.AddWithValue("@InstituteId1", b.InstituteId);
                    cmd1.Parameters.AddWithValue("@Department_Id1", b.pubinchargedept);

                    cmd1.Parameters.AddWithValue("@Active", "Y");
                    cmd1.Parameters.AddWithValue("@CreatedBy", HttpContext.Current.Session["UserId"].ToString());

                    cmd1.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
                    //cmd1.Parameters.AddWithValue("@UpdatedBy", HttpContext.Current.Session["UserId"].ToString());

                    // cmd1.Parameters.AddWithValue("@UpdatedDate", DateTime.Now);

                    result2 = cmd1.ExecuteNonQuery();
                }

                else
                {
                    cmdString1 = "update  Publication_InchargerM set Active=@Active,UpdatedBy=@UpdatedBy,UpdatedDate=@UpdatedDate where  UserId= @UserId1 and EntryNum=@entrynum";
                    SqlCommand cmd11 = new SqlCommand(cmdString1, con, transaction);
                    cmd11.CommandType = CommandType.Text;
                    cmd11.Parameters.AddWithValue("@Active", "N");
                    cmd11.Parameters.AddWithValue("@UserId1", b.User_Id);
                    cmd11.Parameters.AddWithValue("@entrynum", entrynum);
                    cmd11.Parameters.AddWithValue("@UpdatedBy", HttpContext.Current.Session["UserId"].ToString());
                    cmd11.Parameters.AddWithValue("@UpdatedDate", DateTime.Now);
                    //cmd11.Parameters.AddWithValue("@InstituteId1", b.InstituteId);
                    // cmd11.Parameters.AddWithValue("@Department_Id1", b.pubinchargedept);
                    result3 = cmd11.ExecuteNonQuery();
                }

            }

            return result1;


        }

        catch (Exception e)
        {
            log.Debug("Error: Inside catch block of Update user");
            log.Error("Error msg:" + e);
            log.Error("Stack trace:" + e.StackTrace);
            return 0;
        }

        finally
        {
            con.Close();
        }
    }

    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    public User selectExistingUser(string UserId)
    {

        try
        {

            User b = new User();
            con.Open();
            SqlCommand cmd = new SqlCommand("selectExistingUser", con); //selectExistingUser stored procedure 

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@userid", SqlDbType.VarChar, 12);
            cmd.Parameters["@userid"].Value = UserId;

            cmd.CommandType = CommandType.StoredProcedure;

            SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                if (!Convert.IsDBNull(sdr["User_Id"]))
                {
                    b.User_Id = (string)sdr["User_Id"];
                }
                if (!Convert.IsDBNull(sdr["Prefix"]))
                {
                    b.UserNamePrefix = (string)sdr["Prefix"];
                }
                if (!Convert.IsDBNull(sdr["FirstName"]))
                {
                    b.UserFirstName = (string)sdr["FirstName"];
                }
                if (!Convert.IsDBNull(sdr["MiddleName"]))
                {
                    b.UserMiddleName = (string)sdr["MiddleName"];
                }
                if (!Convert.IsDBNull(sdr["LastName"]))
                {
                    b.UserLastName = (string)sdr["LastName"];
                }
                if (!Convert.IsDBNull(sdr["Department_Id"]))
                {
                    b.Department = (string)sdr["Department_Id"];
                }
                if (!Convert.IsDBNull(sdr["InstituteId"]))
                {
                    b.Institute_name = (string)sdr["InstituteId"];
                }
                if (!Convert.IsDBNull(sdr["EmailId"]))
                {
                    b.EmailId = (string)sdr["EmailId"];
                }
                if (!Convert.IsDBNull(sdr["AutoApproved"]))
                {
                    b.AutoApproved = (string)sdr["AutoApproved"];
                }
                if (!Convert.IsDBNull(sdr["Role_Id"]))
                {
                    b.Role_Id = (int)sdr["Role_Id"];
                }

                if (!Convert.IsDBNull(sdr["Active"]))
                {
                    b.Active = (string)sdr["Active"];
                }
            }

            return b;
        }

        catch (Exception e)
        {
            log.Debug("Error: Inside catch block of selectExistingUser");
            log.Error("Error msg:" + e);
            log.Error("Stack trace:" + e.StackTrace);
            return null;
        }

        finally
        {
            con.Close();
        }

    }

    public User selectPubInchargeUM(string UserId)
    {

        try
        {

            User b = new User();
            con.Open();
            cmdString1 = "select Department_Id, UserId from Publication_InchargerM where UserId=@UserId and Active='Y'";
            SqlCommand cmd = new SqlCommand(cmdString1, con); //selectExistingUser stored procedure 

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@UserId", SqlDbType.VarChar, 12);
            cmd.Parameters["@UserId"].Value = UserId;

            cmd.CommandType = CommandType.Text;

            SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                if (!Convert.IsDBNull(sdr["Department_Id"]))
                {
                    b.Department_Id = (string)sdr["Department_Id"];
                }
                if (!Convert.IsDBNull(sdr["UserId"]))
                {
                    b.UserId = (string)sdr["UserId"];
                }

            }

            return b;
        }

        catch (Exception e)
        {
            log.Debug("Error: Inside catch block of selectExistingUser");
            log.Error("Error msg:" + e);
            log.Error("Stack trace:" + e.StackTrace);
            return null;
        }

        finally
        {
            con.Close();
        }

    }

    public Int32 InsertDepartmentInstituteAutoAppove(User b)
    {
        int result = 0, result1 = 0;
        try
        {
            con.Open();
            transaction = con.BeginTransaction();

            cmdString1 = "Select count(*) as Count from Inst_Dept_AutoApprove where  Institute=@Institute and Department=@Department";
            SqlCommand cmd = new SqlCommand(cmdString1, con, transaction);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@Institute", b.InstituteId);
            cmd.Parameters.AddWithValue("@Department", b.Department_Id);

            SqlDataReader sdr = cmd.ExecuteReader();

            int count = 0;

            while (sdr.Read())
            {
                if (!Convert.IsDBNull(sdr["Count"]))
                {
                    count = (int)sdr["Count"];
                }

            }
            sdr.Close();
            if (count == 0)
            {
                cmdString1 = "insert Inst_Dept_AutoApprove (Institute,Department,AutoApprove)values(@Institute,@Department,@AutoApprove) ";
                cmd = new SqlCommand(cmdString1, con, transaction);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Department", b.Department_Id);
                cmd.Parameters.AddWithValue("@Institute", b.InstituteId);

                cmd.Parameters.AddWithValue("@AutoApprove", b.AutoApproved);

                result = cmd.ExecuteNonQuery();
            }
            else
            {
                cmdString1 = "update Inst_Dept_AutoApprove set AutoApprove=@AutoApprove where Institute=@Institute and Department=@Department";
                cmd = new SqlCommand(cmdString1, con, transaction);
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@Department", b.Department_Id);
                cmd.Parameters.AddWithValue("@Institute", b.InstituteId);

                cmd.Parameters.AddWithValue("@AutoApprove", b.AutoApproved);





                result = cmd.ExecuteNonQuery();

            }


            cmdString1 = "Select count(*) as Count from Inst_Dept_AutoApproveTracker where  Institute=@Institute and Department=@Department";
            cmd = new SqlCommand(cmdString1, con, transaction);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@Institute", b.InstituteId);
            cmd.Parameters.AddWithValue("@Department", b.Department_Id);

            SqlDataReader sdr1 = cmd.ExecuteReader();

            int count1 = 0;

            while (sdr1.Read())
            {
                if (!Convert.IsDBNull(sdr1["Count"]))
                {
                    count1 = (int)sdr1["Count"];
                }

            }
            sdr1.Close();

            if (result >= 1)
            {

                cmdString1 = "insert into Inst_Dept_AutoApproveTracker (Institute , Department ,ReviewNum,AutoApprove,Remarks,UpdatedBy,UpdatedDate) values (@Institute,@Department,@ReviewNum,@AutoApprove,@Remarks,@UpdatedBy,@UpdatedDate)";
                SqlCommand cmd1 = new SqlCommand(cmdString1, con, transaction);
                cmd1.CommandType = CommandType.Text;
                cmd1.Parameters.AddWithValue("@AutoApprove", b.AutoApproved);
                cmd1.Parameters.AddWithValue("@Institute", b.InstituteId);
                cmd1.Parameters.AddWithValue("@Department", b.Department_Id);
                cmd1.Parameters.AddWithValue("@ReviewNum", count1 + 1);
                cmd1.Parameters.AddWithValue("@Remarks", b.Remarks);
                cmd1.Parameters.AddWithValue("@UpdatedBy", b.UpdatedBy);
                cmd1.Parameters.AddWithValue("@UpdatedDate", DateTime.Now);

                result1 = cmd1.ExecuteNonQuery();


            }
            transaction.Commit();
            return result1;
        }
        catch (Exception e)
        {
            log.Debug("Error: Inside catch block of insert user");
            log.Error("Error msg:" + e);
            log.Error("Stack trace:" + e.StackTrace);
            transaction.Rollback();
            return 0;

        }
        finally
        {
            if (con.State != ConnectionState.Closed)
            {
                con.Close();
            }
        }
    }



    public DataSet GetHREmpData()
    {
        DataSet dataset1 = new DataSet();
        log.Debug("Inside GetHREmpData");
        try
        {
            con.Open();
            transaction = con.BeginTransaction();
            string cmdstr = "Delete from EmpMaster";
            SqlCommand cmd1 = new SqlCommand(cmdstr, con, transaction);
            cmd1.CommandType = CommandType.Text;
            bool result = Convert.ToBoolean(cmd1.ExecuteNonQuery());
            log.Info("Emp Master data deleted successfully in local RMS database");

            HRcon.Open();
            log.Debug("Copying HR data from HR database");
            String cmdstr1 = "Select Rtrim(BU),RTrim(Emplid),rTrim(EmpClass),rTrim(EmpPrefix),RTrim(EmpFName),RTrim(EmpMName),RTrim(EmpLName),rTrim(Deptid),rTrim(EmpSuperID),rTRIM(EmpEml),rTRIM(IsActive) from  MUEmpMaster where IsActive='Y' and EmpClass='TEC'";
            DataTable ds = new DataTable();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmdstr1, HRcon);
            da.Fill(ds);
            HRcon.Close();


            string sql = "";
            foreach (DataRow dr in ds.Rows)
            {
                string s = dr[6].ToString().Trim();
                dr[6] = s.Replace("'", "''");

                string secondName = dr[5].ToString().Trim();
                dr[5] = secondName.Replace("'", "''");

                string FName = dr[4].ToString().Trim();
                dr[4] = FName.Replace("'", "''");

                sql += "INSERT INTO EmpMaster  VALUES ('" + dr[0] + "','" + dr[1] + "','" + dr[2] + "','" + dr[3] + "','" + dr[4] + "','" + dr[5] + "','" + dr[6] + "','" + dr[7] + "','" + dr[8] + "','" + dr[9] + "','" + dr[10] + "'); ";

            }
            log.Info("HR data from HR database seleted succsessfully");

            log.Debug("Inserting HR data from HR database to local(RMS) database into Emp master table.");
            SqlCommand cmd2 = new SqlCommand(sql, con, transaction);
            cmd2.CommandType = CommandType.Text;
            int result1 = Convert.ToInt16(cmd2.ExecuteNonQuery());
            log.Info("Inserted HR data from HR database to local(RMS) database");

            if (result1 > 0)
            {
                SqlCommand cmd3 = new SqlCommand("HRDataCopy", con, transaction);
                cmd3.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da1 = new SqlDataAdapter(cmd3);
                da1.Fill(dataset1);
            }

            string sql2 = "";
            string CopyUpdatedHRDatatotable = ConfigurationManager.AppSettings["CopyUpdatedHRDatatotable"].ToString();
            if (CopyUpdatedHRDatatotable == "Y")
            {
                DataTable firstTable = dataset1.Tables[0];
                foreach (DataRow dr in firstTable.Rows)
                {
                    sql2 += "INSERT INTO UpdatedHRDataCopy  VALUES ('" + dr[0] + "','" + dr[1] + "','" + dr[2] + "','" + dr[3] + "','" + dr[4] + "','" + dr[5] + "','" + dr[6] + "','" + dr[7] + "','" + dr[8] + "','" + dr[9] + "'); ";

                }
                SqlCommand cmd4 = new SqlCommand(sql2, con, transaction);
                cmd4.CommandType = CommandType.Text;
                int result2 = Convert.ToInt16(cmd4.ExecuteNonQuery());
            }

            transaction.Commit();
            return dataset1;
        }

        catch (Exception e)
        {
            log.Debug("Error: Inside catch block of GetHREmpData");
            log.Error("Error msg:" + e);
            log.Error("Stack trace:" + e.StackTrace);
            transaction.Rollback();
            return dataset1;
        }
        finally
        {
            if (con.State != ConnectionState.Closed)
            {
                con.Close();
            }
        }

    }

    public User getAuthorAffiliationdetails(string empid, string EmailID)
    {
        User b = new User();
        SqlCommand cmd;
        int count = 0;
        try
        {
            con.Open();
            cmd = new SqlCommand("SelectEmpCodeBasedAuthorsAffiliation", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@User_Id", SqlDbType.VarChar, 12);
            cmd.Parameters["@User_Id"].Value = empid;
            cmd.Parameters.Add("@EmailID", SqlDbType.VarChar, 200);
            cmd.Parameters["@EmailID"].Value = EmailID;           
            SqlDataReader sdr = cmd.ExecuteReader();

            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    if (!Convert.IsDBNull(sdr["AuthorName"]))
                    {
                        b.Name = (string)sdr["AuthorName"];
                    }
                    if (!Convert.IsDBNull(sdr["EmailId"]))
                    {
                        b.emailId = (string)sdr["EmailId"];
                    }                                
                    if (!Convert.IsDBNull(sdr["Institution"]))
                    {
                        b.Institute_name = (string)sdr["Institution"];
                    }
                    if (!Convert.IsDBNull(sdr["Department"]))
                    {
                        b.DepartmentName = (string)sdr["Department"];
                    }                                       

                }

            }
            return b;
        }
        catch (Exception ex)
        {
            log.Error("Inside getAuthorAffiliationdetails catch block User ID  " + empid);
            log.Error(ex.Message);
            log.Error(ex.StackTrace);
            throw ex;
        }

        finally
        {
            con.Close();
        }
    }

    public User getAuthorInstitutiondetails(string empid, string EmailID)
    {
        User b = new User();
        SqlCommand cmd;
        int count = 0;
        try
        {
            con.Open();

            if (empid =="")
            {
                empid =null;
            }
            if (EmailID == "")
            {
                EmailID =null;
            }
            if (empid != null)
            {
                //cmd = new SqlCommand("select InstituteId,centerCode,Department_Id from User_M where (User_Id='" + empid + "')", con);
                cmd = new SqlCommand("select InstituteId,centerCode,Department_Id from User_M where (User_Id=@empid)", con);

                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@empid", empid);
                SqlDataReader sdr = cmd.ExecuteReader();

                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        if (!Convert.IsDBNull(sdr["InstituteId"]))
                        {
                            b.InstituteId = (string)sdr["InstituteId"];
                        }
                        if (!Convert.IsDBNull(sdr["centerCode"]))
                        {
                            b.centerCode = (int)sdr["centerCode"];
                        }
                        if (!Convert.IsDBNull(sdr["Department_Id"]))
                        {
                            b.Department_Id = (string)sdr["Department_Id"];
                        }
                    }

                }
            }
            else
            {
                //cmd = new SqlCommand("select InstituteId,centerCode,Department_Id from User_M where ( EmailId='" + EmailID + "')", con);
                cmd = new SqlCommand("select InstituteId,centerCode,Department_Id from User_M where ( EmailId=@EmailID)", con);

                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@EmailID", EmailID);
                SqlDataReader sdr = cmd.ExecuteReader();

                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        if (!Convert.IsDBNull(sdr["InstituteId"]))
                        {
                            b.InstituteId = (string)sdr["InstituteId"];
                        }
                        if (!Convert.IsDBNull(sdr["centerCode"]))
                        {
                            b.centerCode = (int)sdr["centerCode"];
                        }
                        if (!Convert.IsDBNull(sdr["Department_Id"]))
                        {
                            b.Department_Id = (string)sdr["Department_Id"];
                        }
                    }
                   
                }
            }
            //cmd.Parameters.Add("@User_Id", SqlDbType.VarChar, 12);
            //cmd.Parameters["@User_Id"].Value = empid;
            //cmd.Parameters.Add("@EmailID", SqlDbType.VarChar, 200);
            //cmd.Parameters["@EmailID"].Value = EmailID;
           
            return b;
        }
        catch (Exception ex)
        {
            log.Error("Inside getAuthorInstitutiondetails catch block User ID  " + empid);
            log.Error(ex.Message);
            log.Error(ex.StackTrace);
            throw ex;
        }

        finally
        {
            con.Close();
        }
    }

    public User getAuthorAffiliationdetailsforMAHE(string empid, string EmailID)
    {
        User b = new User();
        SqlCommand cmd;
        int count = 0;
        try
        {
            con.Open();
            cmd = new SqlCommand("SelectEmpCodeBasedAuthorsAffiliationMAHE", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@User_Id", SqlDbType.VarChar, 12);
            cmd.Parameters["@User_Id"].Value = empid;
            cmd.Parameters.Add("@EmailID", SqlDbType.VarChar, 200);
            cmd.Parameters["@EmailID"].Value = EmailID;      
            SqlDataReader sdr = cmd.ExecuteReader();

            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    if (!Convert.IsDBNull(sdr["AuthorName"]))
                    {
                        b.Name = (string)sdr["AuthorName"];
                    }
                    if (!Convert.IsDBNull(sdr["EmailId"]))
                    {
                        b.emailId = (string)sdr["EmailId"];
                    }                            
                    if (!Convert.IsDBNull(sdr["Institution"]))
                    {
                        b.Institute_name = (string)sdr["Institution"];
                    }                   
                    if (!Convert.IsDBNull(sdr["Department"]))
                    {
                        b.DepartmentName = (string)sdr["Department"];
                    }
                                 
                }

            }
            return b;
        }
        catch (Exception ex)
        {
            log.Error("Inside getAuthorAffiliationdetails catch block User ID  " + empid);
            log.Error(ex.Message);
            log.Error(ex.StackTrace);
            throw ex;
        }

        finally
        {
            con.Close();
        }
    }

    public User getAuthorAffiliationIDType(string InstituteId)
    {
        User b = new User();
        SqlCommand cmd;
        int count = 0;
        try
        {
            con.Open();
            //cmd = new SqlCommand(" select Type,HasDepartment,Name from I_and_D_Master where ID='" + InstituteId + "' ", con);
            cmd = new SqlCommand(" select Type,HasDepartment,Name from I_and_D_Master where ID=@InstituteId ", con);

            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@InstituteId", InstituteId);

            //cmd.Parameters.Add("@User_Id", SqlDbType.VarChar, 12);
            //cmd.Parameters["@User_Id"].Value = empid;
            //cmd.Parameters.Add("@EmailID", SqlDbType.VarChar, 200);
            //cmd.Parameters["@EmailID"].Value = EmailID;
            SqlDataReader sdr = cmd.ExecuteReader();

            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    if (!Convert.IsDBNull(sdr["Type"]))
                    {
                        b.IDType = (string)sdr["Type"];
                    }
                    if (!Convert.IsDBNull(sdr["HasDepartment"]))
                    {
                        b.hasdepartment = (string)sdr["HasDepartment"];
                    }
                    if (!Convert.IsDBNull(sdr["Name"]))
                    {
                        b.Mahedepartment = (string)sdr["Name"];
                    }
                   
                }

            }
            return b;
        }
        catch (Exception ex)
        {
            log.Error("Inside getAuthorAffiliationdetails catch block Institute ID  " + InstituteId);
            log.Error(ex.Message);
            log.Error(ex.StackTrace);
            throw ex;
        }

        finally
        {
            con.Close();
        }
    }

    public User getAuthorCenterAffiliationdetails(int CenterCode)
    {
         User b = new User();
        SqlCommand cmd;
        int count = 0;
        try
        {
            con.Open();
            //cmd = new SqlCommand("select a.Name as CenterName,b.Name as SchoolName from Center_M a,I_and_D_Master b where (a.InstId=b.ID or a.DeptId=b.ID )and  a.id=" + CenterCode + "", con);
            cmd = new SqlCommand("select a.Name as CenterName,b.Name as SchoolName from Center_M a,I_and_D_Master b where (a.InstId=b.ID or a.DeptId=b.ID )and  a.id=@CenterCode", con);

            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@CenterCode", CenterCode);
            //cmd.Parameters.Add("@User_Id", SqlDbType.VarChar, 12);
            //cmd.Parameters["@User_Id"].Value = empid;
            //cmd.Parameters.Add("@EmailID", SqlDbType.VarChar, 200);
            //cmd.Parameters["@EmailID"].Value = EmailID;
            SqlDataReader sdr = cmd.ExecuteReader();

            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    if (!Convert.IsDBNull(sdr["CenterName"]))
                    {
                        b.Centername = (string)sdr["CenterName"];
                    }
                    if (!Convert.IsDBNull(sdr["SchoolName"]))
                    {
                        b.SchoolName1 = (string)sdr["SchoolName"];
                    }                  
                }

            }
            return b;
        }
        catch (Exception ex)
        {
            log.Error("Inside getAuthorAffiliationdetails catch block CenterCode " + CenterCode);
            log.Error(ex.Message);
            log.Error(ex.StackTrace);
            throw ex;
        }

        finally
        {
            con.Close();
        }
       
    }

    public User getAuthorAffiliationIDTypeMAHE(string dept, string empid)
    {
        User b = new User();
        SqlCommand cmd;
        int count = 0;
        try
        {
            con.Open();
            //cmd = new SqlCommand(" select Type,HasDepartment,Name from I_and_D_Master where ID='" + dept + "' ", con);
            cmd = new SqlCommand(" select Type,HasDepartment,Name from I_and_D_Master where ID=@dept ", con);

            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@dept", dept);
            //cmd.Parameters.Add("@User_Id", SqlDbType.VarChar, 12);
            //cmd.Parameters["@User_Id"].Value = empid;
            //cmd.Parameters.Add("@EmailID", SqlDbType.VarChar, 200);
            //cmd.Parameters["@EmailID"].Value = EmailID;
            SqlDataReader sdr = cmd.ExecuteReader();

            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    if (!Convert.IsDBNull(sdr["Type"]))
                    {
                        b.IDType = (string)sdr["Type"];
                    }
                    if (!Convert.IsDBNull(sdr["HasDepartment"]))
                    {
                        b.hasdepartment = (string)sdr["HasDepartment"];
                    }
                    if (!Convert.IsDBNull(sdr["Name"]))
                    {
                        b.Mahedepartment = (string)sdr["Name"];
                    }

                }

            }
            return b;
        }
        catch (Exception ex)
        {
            log.Error("Inside getAuthorAffiliationdetails catch block EmployeeID ID  " + empid);
            log.Error(ex.Message);
            log.Error(ex.StackTrace);
            throw ex;
        }

        finally
        {
            con.Close();
        }
    }

    public User getStudentInstitutiondetails(string empid, string EmailID)
    {
        User b = new User();
        SqlCommand cmd;
        int count = 0;
        try
        {
            con.Open();

            if (empid == "")
            {
                empid = null;
            }
            if (EmailID == "")
            {
                EmailID = null;
            }
            if (empid != null)
            {
                //cmd = new SqlCommand("select b.ID as InstituteId,b.Name as InstName ,ClassCode as Department_Id from SISStudentGenInfo a,I_and_D_Master b  where a.InstID=b.SISCode and RollNo='" + empid + "'", con);
                cmd = new SqlCommand("select b.ID as InstituteId,b.Name as InstName ,ClassCode as Department_Id from SISStudentGenInfo a,I_and_D_Master b  where a.InstID=b.SISCode and RollNo=@empid", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@empid", empid);
                SqlDataReader sdr = cmd.ExecuteReader();

                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        if (!Convert.IsDBNull(sdr["InstituteId"]))
                        {
                            b.InstituteId = (string)sdr["InstituteId"];
                        }
                        if (!Convert.IsDBNull(sdr["InstName"]))
                        {
                            b.Mahedepartment = (string)sdr["InstName"];
                        }
                        if (!Convert.IsDBNull(sdr["Department_Id"]))
                        {
                            b.Department_Id = (string)sdr["Department_Id"];
                        }
                    }

                }
            }
            else
            {
                //cmd = new SqlCommand("select b.ID as InstituteId,b.Name as InstName ,ClassCode as Department_Id from SISStudentGenInfo a,I_and_D_Master b  where a.InstID=b.SISCode and  EmailID1='" + EmailID + "'", con);
                cmd = new SqlCommand("select b.ID as InstituteId,b.Name as InstName ,ClassCode as Department_Id from SISStudentGenInfo a,I_and_D_Master b  where a.InstID=b.SISCode and  EmailID1=@EmailID", con);

                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@EmailID", EmailID);
                SqlDataReader sdr = cmd.ExecuteReader();

                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        if (!Convert.IsDBNull(sdr["InstituteId"]))
                        {
                            b.InstituteId = (string)sdr["InstituteId"];
                        }
                        if (!Convert.IsDBNull(sdr["InstName"]))
                        {
                            b.Mahedepartment = (string)sdr["InstName"];
                        }
                        if (!Convert.IsDBNull(sdr["Department_Id"]))
                        {
                            b.Department_Id = (string)sdr["Department_Id"];
                        }
                    }

                }
            }
            //cmd.Parameters.Add("@User_Id", SqlDbType.VarChar, 12);
            //cmd.Parameters["@User_Id"].Value = empid;
            //cmd.Parameters.Add("@EmailID", SqlDbType.VarChar, 200);
            //cmd.Parameters["@EmailID"].Value = EmailID;

            return b;
        }
        catch (Exception ex)
        {
            log.Error("Inside getAuthorInstitutiondetails catch block User ID  " + empid);
            log.Error(ex.Message);
            log.Error(ex.StackTrace);
            throw ex;
        }

        finally
        {
            con.Close();
        }
    }

    public User getStudentAffiliationdetails(string empid, string EmailID)
    {
        User b = new User();
        SqlCommand cmd;
        int count = 0;
        try
        {
            con.Open();
            cmd = new SqlCommand("SelectRollNoBasedStudentAffiliation", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@User_Id", SqlDbType.VarChar, 12);
            cmd.Parameters["@User_Id"].Value = empid;
            cmd.Parameters.Add("@EmailID", SqlDbType.VarChar, 200);
            cmd.Parameters["@EmailID"].Value = EmailID;
            SqlDataReader sdr = cmd.ExecuteReader();

            if (sdr.HasRows)
            {
                while (sdr.Read())
                {                
                    if (!Convert.IsDBNull(sdr["Name"]))
                    {
                        b.Name = (string)sdr["Name"];
                    }
                    if (!Convert.IsDBNull(sdr["EmailID1"]))
                    {
                        b.emailId = (string)sdr["EmailID1"];
                    }
                    if (!Convert.IsDBNull(sdr["instID"]))
                    {
                        b.Institute_name = (string)sdr["instID"];
                    }
                    //if (!Convert.IsDBNull(sdr["ClassName"]))
                    //{
                    //    b.DepartmentName = (string)sdr["ClassName"];
                    //}

                }

            }
            return b;
        }
        catch (Exception ex)
        {
            log.Error("Inside getAuthorAffiliationdetails catch block User ID  " + empid);
            log.Error(ex.Message);
            log.Error(ex.StackTrace);
            throw ex;
        }

        finally
        {
            con.Close();
        }
    }

    public User getAuthorOnlyCenterAffiliationdetails(int CenterCode)
    {
        User b = new User();
        SqlCommand cmd;
        int count = 0;
        try
        {
            con.Open();
            //cmd = new SqlCommand("select Name as CenterName from Center_M  where  id=" + CenterCode + "", con);
            cmd = new SqlCommand("select Name as CenterName from Center_M  where  id=@CenterCode", con);

            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@CenterCode", CenterCode);
            //cmd.Parameters.Add("@User_Id", SqlDbType.VarChar, 12);
            //cmd.Parameters["@User_Id"].Value = empid;
            //cmd.Parameters.Add("@EmailID", SqlDbType.VarChar, 200);
            //cmd.Parameters["@EmailID"].Value = EmailID;
            SqlDataReader sdr = cmd.ExecuteReader();

            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    if (!Convert.IsDBNull(sdr["CenterName"]))
                    {
                        b.Centername = (string)sdr["CenterName"];
                    }
                    //if (!Convert.IsDBNull(sdr["SchoolName"]))
                    //{
                    //    b.SchoolName1 = (string)sdr["SchoolName"];
                    //}
                }

            }
            return b;
        }
        catch (Exception ex)
        {
            log.Error("Inside getAuthorOnlyCenterAffiliationdetails catch block CenterCode " + CenterCode);
            log.Error(ex.Message);
            log.Error(ex.StackTrace);
            throw ex;
        }

        finally
        {
            con.Close();
        }
       
    }

    public int SaveStudentDetails(User b)
    {
        log.Debug("Inside SaveStudentDetails function");
        int result = 0, result1 = 0, ID = 0;
        string seedFinal = "";

        string cmdString = "";


        try
        {
            con.Open();
            transaction = con.BeginTransaction();
            cmdString1 = "insert into SISStudentGenInfo(RollNo,Name,ClassCode,InstID,EmailID1,DOB,MobileNo,Sex,StudStatus)values(@RollNo,@Name,@ClassCode,@InstID,@EmailID1,@DOB,@MobileNo,@Sex,@StudStatus)";
            SqlCommand cmd1 = new SqlCommand(cmdString1, con, transaction);
            cmd1.CommandType = CommandType.Text;
            cmd1.Parameters.AddWithValue("@RollNo", b.User_Id);
            cmd1.Parameters.AddWithValue("@Name", b.Name);
            cmd1.Parameters.AddWithValue("@ClassCode", b.Department_Id);
            cmd1.Parameters.AddWithValue("@InstID", b.InstituteId);
            cmd1.Parameters.AddWithValue("@EmailID1", b.EmailId);
            if (b.StudentDOB.ToShortDateString() != "01/01/0001")
            {
                cmd1.Parameters.AddWithValue("@DOB", b.StudentDOB);
            }
            else
            {
                cmd1.Parameters.AddWithValue("@DOB", DBNull.Value);
            }
            if (b.MobileNo != null)
            {
                cmd1.Parameters.AddWithValue("@MobileNo", b.MobileNo);
            }
            else
            {
                cmd1.Parameters.AddWithValue("@MobileNo", DBNull.Value);
            }
            cmd1.Parameters.AddWithValue("@Sex", b.Sex);
            cmd1.Parameters.AddWithValue("@StudStatus", "M");

            result1 = cmd1.ExecuteNonQuery();
            transaction.Commit();
            return result1;
        }

        catch (Exception ex)
        {
            log.Error("Inside SaveStudentDetails catch block");
            log.Error(ex.Message);
            log.Error(ex.StackTrace);
            transaction.Rollback();
            throw ex;
        }

        finally
        {
            con.Close();
        }
    }

    public int UpdateStudentdetails(User b)
    {
        log.Debug("Inside UpdateStudentdetails function");
        int result1 = 0;
        try
        {
            con.Open();
            transaction = con.BeginTransaction();

            cmdString1 = "update SISStudentGenInfo set Name=@Name,ClassCode=@ClassCode,InstID=@InstID,EmailID1=@EmailID1,DOB=@DOB,MobileNo=@MobileNo,Sex=@Sex where  RollNo=@RollNo";
            SqlCommand cmd1 = new SqlCommand(cmdString1, con, transaction);
            cmd1.CommandType = CommandType.Text;
            cmd1.Parameters.AddWithValue("@RollNo", b.User_Id);
            cmd1.Parameters.AddWithValue("@Name", b.Name);
            cmd1.Parameters.AddWithValue("@ClassCode", b.Department_Id);
            cmd1.Parameters.AddWithValue("@InstID", b.InstituteId);
            cmd1.Parameters.AddWithValue("@EmailID1", b.EmailId);
            if (b.StudentDOB.ToShortDateString() != "01/01/0001")
            {
                cmd1.Parameters.AddWithValue("@DOB", b.StudentDOB);
            }
            else
            {
                cmd1.Parameters.AddWithValue("@DOB", DBNull.Value);
            }
            if (b.MobileNo != null)
            {
                cmd1.Parameters.AddWithValue("@MobileNo", b.MobileNo);
            }
            else
            {
                cmd1.Parameters.AddWithValue("@MobileNo", DBNull.Value);
            }
            cmd1.Parameters.AddWithValue("@Sex", b.Sex);
           

            result1 = cmd1.ExecuteNonQuery();


            transaction.Commit();
            return result1;
        }

        catch (Exception ex)
        {
            log.Error("Inside UpdateStudentdetails catch block");
            log.Error(ex.Message);
            log.Error(ex.StackTrace);
            transaction.Rollback();
            throw ex;
        }

        finally
        {
            con.Close();
        }
    }

    public User selectExistingStudentDetails(string UserId)
    {
        try
        {

            User b = new User();
            con.Open();
            SqlCommand cmd = new SqlCommand("select *from SISStudentGenInfo where RollNo=@userid", con); //selectExistingUser stored procedure 

            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@userid", SqlDbType.VarChar, 12);
            cmd.Parameters["@userid"].Value = UserId;

            SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                if (!Convert.IsDBNull(sdr["RollNo"]))
                {
                    b.User_Id = (string)sdr["RollNo"];
                }
                if (!Convert.IsDBNull(sdr["Name"]))
                {
                    b.Name = (string)sdr["Name"];
                }
                if (!Convert.IsDBNull(sdr["ClassCode"]))
                {
                    b.Department = (string)sdr["ClassCode"];
                }
                if (!Convert.IsDBNull(sdr["EmailID1"]))
                {
                    b.emailId = (string)sdr["EmailID1"];
                }
                if (!Convert.IsDBNull(sdr["InstID"]))
                {
                    b.Institute_name = (string)sdr["InstID"];
                }
                if (!Convert.IsDBNull(sdr["DOB"]))
                {
                    b.StudentDOB = (DateTime)sdr["DOB"];
                }
                else if (Convert.IsDBNull(sdr["DOB"]))
                {

                }
                if (!Convert.IsDBNull(sdr["MobileNo"]))
                {
                    b.MobileNo = (string)sdr["MobileNo"];
                }
                else if (Convert.IsDBNull(sdr["MobileNo"]))
                {
                    b.MobileNo = "";
                }

                if (!Convert.IsDBNull(sdr["Sex"]))
                {
                    b.Active = (string)sdr["Sex"];
                }
               
            }

            return b;
        }

        catch (Exception e)
        {
            log.Error("Error: Inside catch block of selectExistingStudentDetails");
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