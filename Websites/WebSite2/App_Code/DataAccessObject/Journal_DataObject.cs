using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using log4net;
using System.Collections;
using System.Text;



/// </summary>
public class Journal_DataObject
{
    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    public string str;
    public string cmdString;
    public SqlConnection con;
    public SqlCommand cmd;
    SqlTransaction transaction;
    public Journal_DataObject()
    {
        str = ConfigurationManager.ConnectionStrings["RMSConnectionString"].ConnectionString;
        cmdString = "";
        con = new SqlConnection(str);
        cmd = new SqlCommand(cmdString, con);
    }






    public int InsertJCFileUploadCSV(JournalData[] jd)
    {
        log.Debug("Inside Journal_DataObject- InsertJCFileUploadCSV function ");

        int result = 0, result2 = 0, result3 = 0;

        con = new SqlConnection(str);
        con.Open();
        transaction = con.BeginTransaction();
        String ExistJid = "";
        String NewJid = "";

        try
        {



            for (int i = 2; i < jd.Length; i++)
            {


                int c = 0;

                cmdString = "Select  COUNT(*)  from Journal_M where Id=@Id";
                cmd = new SqlCommand(cmdString, con, transaction);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Id", HttpUtility.HtmlDecode(jd[i].JournalID));

                c = (int)cmd.ExecuteScalar();

                if (c == 0)
                {
                    NewJid += " " + jd[i].JournalID + "<br><br>";

                    //  NewJid += " "+ jd[i].JournalID + "<br>";
                    // NewJid += "      " + (i - 1) + ". " + jd[i].JournalID + "<br>";



                    //cmd = new SqlCommand("InsertUploadJCReportM", con, transaction);
                    //cmd.CommandType = CommandType.StoredProcedure;

                    //cmd.Parameters.AddWithValue("@id", HttpUtility.HtmlDecode(jd[i].JournalID));


                    //cmd.Parameters.AddWithValue("@Name", HttpUtility.HtmlDecode(jd[i].jname));


                    //cmd.Parameters.AddWithValue("@Jname", HttpUtility.HtmlDecode(jd[i].jname));


                    //result3 = cmd.ExecuteNonQuery();
                }
                else
                {

                    ExistJid += "     " + (i - 1) + ". " + jd[i].JournalID + "               ";

                    cmd = new SqlCommand("DeleteJCDetails", con, transaction);
                    cmd.CommandType = CommandType.StoredProcedure;


                    cmd.Parameters.AddWithValue("@Year", HttpUtility.HtmlDecode(jd[i].year));

                    cmd.Parameters.AddWithValue("@id", HttpUtility.HtmlDecode(jd[i].JournalID));


                    result2 = cmd.ExecuteNonQuery();

                    cmd = new SqlCommand("InsertUploadJCReport", con, transaction);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@id", HttpUtility.HtmlDecode(jd[i].JournalID));
                    cmd.Parameters.AddWithValue("@Year", HttpUtility.HtmlDecode(jd[i].year));
                    cmd.Parameters.AddWithValue("@ImpactFactor", Convert.ToDouble(jd[i].impctfact));
                    // cmd.Parameters.AddWithValue("@ImIndex", Convert.ToDouble(jd[i].imindex));
                    // cmd.Parameters.AddWithValue("@Total", Convert.ToDouble(jd[i].total));
                    cmd.Parameters.AddWithValue("@fiveImpFact", Convert.ToDouble(jd[i].fiveimpcrfact));
                    // cmd.Parameters.AddWithValue("@ImmdecyIndex", Convert.ToDouble(jd[i].imindex));
                    // cmd.Parameters.AddWithValue("@articles", Convert.ToDouble(jd[i].arcticles));
                    //if (jd[i].halflife == "")
                    //{
                    //    cmd.Parameters.AddWithValue("@halflife", DBNull.Value);
                    //}
                    //else
                    //{
                    //    cmd.Parameters.AddWithValue("@halflife", Convert.ToDouble(jd[i].halflife));
                    //}
                    //cmd.Parameters.AddWithValue("@factor", Convert.ToDouble(jd[i].facorscore));
                    //cmd.Parameters.AddWithValue("@articleinflu", Convert.ToDouble(jd[i].influscore));


                    result = cmd.ExecuteNonQuery();

                }


            }

            HttpContext.Current.Session["NewJidUploadJCR"] = NewJid;
            transaction.Commit();
            return result;
        }

        catch (Exception e)
        {
            log.Error("Inside Journal_DataObject- InsertJCFileUploadCSV catch block ");
            log.Error(e.Message);
            log.Error(e.StackTrace);
            String msg = e.Message + "Item fileds = " + ExistJid;
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
    //public IndexManage selectJournal(string jid,string year)
    //{

    //    try
    //    {
    //        cmdString = "select ImpactFactor,Id,Year from Journal_IF_Details where Id= '" + jid + "'and Year ='" + year + "' ";

    //        con = new SqlConnection(str);
    //        con.Open();
    //        cmd = new SqlCommand(cmdString, con);


    //        cmd.CommandType = CommandType.Text;
    //        IndexManage p = new IndexManage();

    //        SqlDataReader sdr = cmd.ExecuteReader();
    //        while (sdr.Read())
    //        {

    //            if (!Convert.IsDBNull(sdr["ImpactFactor"]))
    //            {
    //                p.ImpactFactor = (double)sdr["ImpactFactor"];
    //            }
    //            if (!Convert.IsDBNull(sdr["Id"]))
    //            {
    //                p.Jid = (string)sdr["Id"];
    //            }
    //            if (!Convert.IsDBNull(sdr["Year"]))
    //            {
    //                p.Year = (string)sdr["Year"];
    //            }
    //        }

    //        return p;
    //    }
    //    catch (Exception ex)
    //    {
    //        log.Error("Inside MasterDataObject- selectJournal catch block ");
    //        log.Error(ex.Message);
    //        log.Error(ex.StackTrace);

    //        transaction.Rollback();
    //        throw ex;
    //    }

    //    finally
    //    {
    //        con.Close();
    //    }
    //}
    public DataSet IndexAgency()
    {
        try
        {


            string cmdstring = "select agencyid,agencyname from IndexAgency_M where active='Y'";


            SqlDataAdapter da;
            DataSet ds;
            con = new SqlConnection(str);
            con.Open();
            da = new SqlDataAdapter(cmdstring, con);

            ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

        catch (Exception e)
        {
            log.Error(e.Message);
            log.Error(e.StackTrace);

            throw e;
        }
        finally
        {
            cmd.Dispose();
            con.Close();
            cmd.Dispose();
        }
    }

    public string SelectQuartile(PublishData v)
    {
        try
        {
            con.Open();
            int PublishJAYear = Convert.ToInt32(v.PublishJAYear);
            int PublishJAMonth = Convert.ToInt32(v.PublishJAMonth);
            cmdString = "SelectQuartileApplicableYearWise";
            cmd = new SqlCommand(cmdString, con, transaction);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID", v.PublisherOfJournal);
            cmd.Parameters.AddWithValue("@PublishJAYear", PublishJAYear);
            cmd.Parameters.AddWithValue("@PublishJAMonth", PublishJAMonth);
            cmd.Parameters.AddWithValue("@QuartileStartMonth", v.Quartilefrommonth);
            cmd.Parameters.AddWithValue("@QuartileEndMonth", v.QuartileTomonth);
            string Quartile = "";

            SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {

                if (!Convert.IsDBNull(sdr["Quartile"]))
                {
                    Quartile = (string)sdr["Quartile"];
                }
            }

            return Quartile;
        }
        catch (Exception ex)
        {
            log.Error("Inside MasterDataObject- SelectQuartile catch block ");
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

    //public int InsertIndexAgency(IndexManage m, ArrayList listIndexAgency)
    //{
    //    log.Debug("Inside Journal_DataObject- InsertIndexAgency function ");

    //    int result = 0, result2 = 0, result3 = 0;

    //    con = new SqlConnection(str);
    //    con.Open();
    //    transaction = con.BeginTransaction();


    //    try
    //    {



    //        for (int i = 0; i < listIndexAgency.Count; i++)
    //        {




    //            cmd = new SqlCommand("InsertIndexAgency", con, transaction);
    //            cmd.CommandType = CommandType.StoredProcedure;



    //            cmd.Parameters.AddWithValue("@Jid", m.Jid);
    //            cmd.Parameters.AddWithValue("@Year", m.Year);
    //                  if (m.ImpactFactor != null)
    //                   {
    //                       cmd.Parameters.AddWithValue("@ImpactFactor", m.ImpactFactor);
    //                  }
    //                  else
    //                   {
    //                       cmd.Parameters.AddWithValue("@ImpactFactor", DBNull.Value);
    //                   }
    //                  cmd.Parameters.AddWithValue("@IndexAgency", listIndexAgency[i]);




    //            result = cmd.ExecuteNonQuery();

    //        }


    //        transaction.Commit();
    //        return result;
    //    }

    //    catch (Exception e)
    //    {
    //        log.Error("Inside Journal_DataObject- InsertIndexAgency catch block ");
    //        log.Error(e.Message);
    //        log.Error(e.StackTrace);

    //        transaction.Rollback();
    //        throw e;

    //    }
    //    finally
    //    {
    //        cmd.Dispose();
    //        con.Close();
    //        cmd.Dispose();

    //    }
    //}


    public IndexManage selectIndexAgency(string AgencyId)
    {

        try
        {
            //cmdString = "select AgencyId,AgencyName,Active from IndexAgency_M where AgencyId='" + AgencyId + "' ";
            cmdString = "select AgencyId,AgencyName,Active from IndexAgency_M where AgencyId=@AgencyId";
           

            con = new SqlConnection(str);
            con.Open();
            cmd = new SqlCommand(cmdString, con);

            cmd.Parameters.AddWithValue("@AgencyId", AgencyId);

            cmd.CommandType = CommandType.Text;
            IndexManage p = new IndexManage();


            SqlDataReader sdr = cmd.ExecuteReader();

            while (sdr.Read())
            {
                if (!Convert.IsDBNull(sdr["AgencyId"]))
                {
                    p.AgencyId = (string)sdr["AgencyId"];
                }
                if (!Convert.IsDBNull(sdr["AgencyName"]))
                {
                    p.AgencyName = (string)sdr["AgencyName"];
                }
                if (!Convert.IsDBNull(sdr["Active"]))
                {
                    p.Active = (string)sdr["Active"];
                }
            }

            return p;
        }
        catch (Exception ex)
        {
            log.Error("Inside - selectIndexAgency catch block ");
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


    public String GetInstitutionName(string InstituteId)
    {

        try
        {
            //cmdString = "select  Institute_Id,Institute_Name from Institute_M where Institute_Id='" + InstituteId + "' ";
            cmdString = "select  Institute_Id,Institute_Name from Institute_M where Institute_Id=@InstituteId ";
            

            con = new SqlConnection(str);
            con.Open();
            cmd = new SqlCommand(cmdString, con);
            cmd.Parameters.AddWithValue("@InstituteId", InstituteId);

            string InstituteId1 = null;
            string InstituteIdname = null;
            cmd.CommandType = CommandType.Text;
            IndexManage p = new IndexManage();


            SqlDataReader sdr = cmd.ExecuteReader();

            while (sdr.Read())
            {
                if (!Convert.IsDBNull(sdr["Institute_Id"]))
                {
                    InstituteId1 = (string)sdr["Institute_Id"];
                }
                if (!Convert.IsDBNull(sdr["Institute_Name"]))
                {
                    InstituteIdname = (string)sdr["Institute_Name"];
                }

            }

            return InstituteIdname;
        }
        catch (Exception ex)
        {
            log.Error("Inside - selectIndexAgency catch block ");
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

    public String GetLibraryId(string InstituteId, string dept)
    {

        try
        {
            // cmdString = "select  distinct l.Library_Id as Library_Id from User_M u ,Library_Institute_Map l where u.InstituteId=l.Institute_Id and Institute_Id='" + InstituteId + "' ";
            //cmdString = "select  distinct l.Library_Id as Library_Id from User_M u ,Library_Dept_Map l where u.Department_Id=l.Dept_Id and Dept_Id='" + dept + "' ";
            cmdString = "select  distinct l.LibraryId as Library_Id from User_M u ,Dept_M l where u.Department_Id=l.DeptId  and l.Institute_Id=u.InstituteId and l.DeptId=@dept and l.Institute_Id=@InstituteId";
           

            con = new SqlConnection(str);
            con.Open();
            cmd = new SqlCommand(cmdString, con);



            string Library_Id = null;
            cmd.Parameters.AddWithValue("@dept", dept);
            cmd.Parameters.AddWithValue("@InstituteId", InstituteId);

            cmd.CommandType = CommandType.Text;
            IndexManage p = new IndexManage();


            SqlDataReader sdr = cmd.ExecuteReader();

            while (sdr.Read())
            {
                if (!Convert.IsDBNull(sdr["Library_Id"]))
                {
                    Library_Id = (string)sdr["Library_Id"];
                }


            }

            return Library_Id;
        }
        catch (Exception ex)
        {
            log.Error("Inside - selectIndexAgency catch block ");
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


    public String GetSupId(string InstituteId, string Uid, string deptId)
    {

        try
        {
            //cmdString = "select SupervisorId from Publication_InchargerM where UserId='" + Uid + "'  and Department_Id='" + deptId + "'  and InstituteId='" + InstituteId + "' ";
            // cmdString = "select UserId as SupervisorId from Publication_InchargerM where  Department_Id='" + deptId + "'  and InstituteId='" + InstituteId + "' ";
            //cmdString = "select UserId as SupervisorId from Publication_InchargerM where   InstituteId='" + InstituteId + "' and Active='Y' ";
            cmdString = "select UserId as SupervisorId from Publication_InchargerM where   InstituteId=@InstituteId and Active='Y' ";

           
            con = new SqlConnection(str);
            con.Open();
            cmd = new SqlCommand(cmdString, con);



            string SupervisorId = null;
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@InstituteId", InstituteId);
            IndexManage p = new IndexManage();


            SqlDataReader sdr = cmd.ExecuteReader();

            while (sdr.Read())
            {
                if (!Convert.IsDBNull(sdr["SupervisorId"]))
                {
                    SupervisorId = (string)sdr["SupervisorId"];
                }


            }

            return SupervisorId;
        }
        catch (Exception ex)
        {
            log.Error("Inside - selectIndexAgency catch block ");
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

    public String GetIstDeptAutoApprove(string InstituteId, string deptId)
    {

        try
        {
            //cmdString = "select SupervisorId from Publication_InchargerM where UserId='" + Uid + "'  and Department_Id='" + deptId + "'  and InstituteId='" + InstituteId + "' ";
            // cmdString = "select UserId as SupervisorId from Publication_InchargerM where  Department_Id='" + deptId + "'  and InstituteId='" + InstituteId + "' ";
            //cmdString = "select AutoApprove from Inst_Dept_AutoApprove where Institute='" + InstituteId + "' and Department='" + deptId + "' ";
            cmdString = "select AutoApprove from Inst_Dept_AutoApprove where Institute=@InstituteId and Department=@deptId";


            con = new SqlConnection(str);
            con.Open();
            cmd = new SqlCommand(cmdString, con);



            string AutoApprove = null;
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@InstituteId", InstituteId);
            cmd.Parameters.AddWithValue("@deptId", deptId);
            IndexManage p = new IndexManage();


            SqlDataReader sdr = cmd.ExecuteReader();

            while (sdr.Read())
            {
                if (!Convert.IsDBNull(sdr["AutoApprove"]))
                {
                    AutoApprove = (string)sdr["AutoApprove"];
                }


            }

            return AutoApprove;
        }
        catch (Exception ex)
        {
            log.Error("Inside - GetIstDeptAutoApprove catch block ");
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



    public String GetDeptName(string DeptId, string institutionid)
    {

        try
        {
            //cmdString = "select DeptName from Dept_M where DeptId='" + DeptId + "' and Institute_Id='" + institutionid + "'";
            cmdString = "select DeptName from Dept_M where DeptId=@DeptId and Institute_Id=@institutionid";


            con = new SqlConnection(str);
            con.Open();
            cmd = new SqlCommand(cmdString, con);



            string DeptName = null;
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("@DeptId", DeptId);
            cmd.Parameters.AddWithValue("@institutionid", institutionid);
            IndexManage p = new IndexManage();


            SqlDataReader sdr = cmd.ExecuteReader();

            while (sdr.Read())
            {
                if (!Convert.IsDBNull(sdr["DeptName"]))
                {
                    DeptName = (string)sdr["DeptName"];
                }


            }

            return DeptName;
        }
        catch (Exception ex)
        {
            log.Error("Inside - GetDeptName catch block ");
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


    public User GetUserName(string EmployeeCode)
    {

        try
        {
            //cmdString = "select * from User_M where User_Id='" + EmployeeCode + "' ";
            cmdString = "select * from User_M where User_Id=@EmployeeCode ";


            con = new SqlConnection(str);
            con.Open();
            cmd = new SqlCommand(cmdString, con);



            User u = new User();
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@EmployeeCode", EmployeeCode);
            IndexManage p = new IndexManage();


            SqlDataReader sdr = cmd.ExecuteReader();

            while (sdr.Read())
            {
                if (!Convert.IsDBNull(sdr["EmailId"]))
                {
                    u.emailId = (string)sdr["EmailId"];
                }
                if (!Convert.IsDBNull(sdr["User_Id"]))
                {
                    u.UserId = (string)sdr["User_Id"];
                }
                if (!Convert.IsDBNull(sdr["Prefix"]))
                {
                    u.UserNamePrefix = (string)sdr["Prefix"];
                }
                else
                {
                    u.UserNamePrefix = "";
                }
                if (!Convert.IsDBNull(sdr["FirstName"]))
                {
                    u.UserFirstName = (string)sdr["FirstName"];
                }
                else
                {
                    u.UserFirstName = "";
                }
                if (!Convert.IsDBNull(sdr["MiddleName"]))
                {
                    u.UserMiddleName = (string)sdr["MiddleName"];
                }
                else
                {
                    u.UserMiddleName = "";
                }
                if (!Convert.IsDBNull(sdr["LastName"]))
                {
                    u.UserLastName = (string)sdr["LastName"];
                }
                else
                {
                    u.UserLastName = "";
                }

                if (!Convert.IsDBNull(sdr["InstituteId"]))
                {
                    u.InstituteId = (string)sdr["InstituteId"];
                }


                if (!Convert.IsDBNull(sdr["Department_Id"]))
                {
                    u.Department = (string)sdr["Department_Id"];
                }

            }

            return u;
        }
        catch (Exception ex)
        {
            log.Error("Inside - GetUserName catch block ");
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



    public User GetPublicationIncharge(string user)
    {

        try
        {

            // cmdString = "select UserId from Publication_InchargerM where InstituteId='" + inst + "'  and Department_Id='" + dept + "' ";

            // cmdString = "select UserId from Publication_InchargerM where InstituteId='" + inst + "' ";
            // cmdString = "select Department_Id from Publication_InchargerM where InstituteId='" + inst + "' and UserId='" + user + "' ";
            //cmdString = "select Department_Id ,InstituteId from Publication_InchargerM where  UserId='" + user + "' and Active='Y'";
            cmdString = "select Department_Id ,InstituteId from Publication_InchargerM where  UserId=@user and Active='Y'";


            con = new SqlConnection(str);
            con.Open();
            cmd = new SqlCommand(cmdString, con);



            User u = new User();
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@user", user);

            IndexManage p = new IndexManage();


            SqlDataReader sdr = cmd.ExecuteReader();

            while (sdr.Read())
            {

                if (!Convert.IsDBNull(sdr["Department_Id"]))
                {
                    u.Department_Id = (string)sdr["Department_Id"];
                }
                if (!Convert.IsDBNull(sdr["InstituteId"]))
                {
                    u.InstituteId = (string)sdr["InstituteId"];
                }


            }

            return u;
        }
        catch (Exception ex)
        {
            log.Error("Inside - GetPublicationIncharge catch block ");
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


    public User GetPublicationInchargeInst(string user)
    {

        try
        {

            // cmdString = "select UserId from Publication_InchargerM where InstituteId='" + inst + "'  and Department_Id='" + dept + "' ";

            //  cmdString = "select InstituteId from Publication_InchargerM where UserId='" + user + "' ";
            //cmdString = "select InstituteId from User_M where User_Id='" + user + "' ";
            cmdString = "select InstituteId from User_M where User_Id=@user";


            con = new SqlConnection(str);
            con.Open();
            cmd = new SqlCommand(cmdString, con);



            User u = new User();
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@user", user);
            IndexManage p = new IndexManage();


            SqlDataReader sdr = cmd.ExecuteReader();

            while (sdr.Read())
            {

                if (!Convert.IsDBNull(sdr["InstituteId"]))
                {
                    u.InstituteId = (string)sdr["InstituteId"];
                }



            }

            return u;
        }
        catch (Exception ex)
        {
            log.Error("Inside - GetPublicationInchargeInst catch block ");
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

    public User GetPublicationInchargeInstLibraryMap(string inst, string dept, string email)
    {

        try
        {

            // cmdString = "select UserId from Publication_InchargerM where InstituteId='" + inst + "'  and Department_Id='" + dept + "' ";

            //     cmdString = "select Library_Id from Library_Institute_Map where Institute_Id='" + inst + "' ";
            // cmdString = "select Library_Id from Library_Dept_Map where Dept_Id='" + dept + "' ";
            // cmdString = "select LibraryId from Dept_M where DeptId='" + dept + "' and Institute_Id='" + inst + "'  ";
            //cmdString = "select LibraryId from Dept_M where DeptId='" + dept + "' and Institute_Id='" + inst + "'  ";
            //cmdString = "select Library_Id from Library_M where EmailId='" + email + "'";

            cmdString = "select Library_Id from Library_M where EmailId=@email";

            con = new SqlConnection(str);
            con.Open();
            cmd = new SqlCommand(cmdString, con);



            User u = new User();
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@email", email);
            IndexManage p = new IndexManage();


            SqlDataReader sdr = cmd.ExecuteReader();

            while (sdr.Read())
            {

                if (!Convert.IsDBNull(sdr["Library_Id"]))
                {
                    u.LibraryId = (string)sdr["Library_Id"];
                }



            }

            return u;
        }
        catch (Exception ex)
        {
            log.Error("Inside - GetPublicationInchargeInstLibraryMap catch block ");
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



    public int IndexAgencyInsert(IndexManage V)
    {


        try
        {
            int result1 = 0;

            con = new SqlConnection(str);
            con.Open();

            SqlCommand cmd = new SqlCommand("InsertIndexAgencyM", con);

            cmd.Parameters.AddWithValue("@AgencyId", V.AgencyId);
            cmd.Parameters.AddWithValue("@AgencyName", V.AgencyName);
            cmd.Parameters.AddWithValue("@UserId", V.Uid);
            cmd.Parameters.AddWithValue("@Date", DateTime.Now);
            cmd.Parameters.AddWithValue("@Active", "Y");
            cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);

            cmd.CommandType = CommandType.StoredProcedure;

            result1 = cmd.ExecuteNonQuery();


            return result1;


        }

        catch (Exception ex)
        {
            log.Error("Inside IndexAgencyInsert catch block ");
            log.Error(ex.Message);
            log.Error(ex.StackTrace);


            throw ex;
        }

        finally
        {
            con.Close();
        }
    }

    public int IndexAgencyUpdate(IndexManage V)
    {

        try
        {
            int result1 = 0;

            con = new SqlConnection(str);
            con.Open();

            SqlCommand cmd = new SqlCommand("UpdateIndexAgencyM", con);

            cmd.Parameters.AddWithValue("@AgencyName", V.AgencyName);
            cmd.Parameters.AddWithValue("@UserID", V.Uid);
            cmd.Parameters.AddWithValue("@Datem", DateTime.Now);
            cmd.Parameters.AddWithValue("@Active", V.Active);
            cmd.Parameters.AddWithValue("@AgencyId", V.AgencyId);

            cmd.CommandType = CommandType.StoredProcedure;
            result1 = cmd.ExecuteNonQuery();


            return result1;


        }

        catch (Exception ex)
        {
            log.Error("Inside  IndexAgencyUpdate catch block ");
            log.Error(ex.Message);
            log.Error(ex.StackTrace);


            throw ex;
        }

        finally
        {
            con.Close();
        }
    }


    public JournalData JournalEntryCheckExistance(JournalData jdValueObj)
    {
        log.Debug("Inside Journal_DataObject- JournalEntryCheckExistance function ");
        int result = 0;

        con = new SqlConnection(str);
        con.Open();

        try
        {
            cmdString = "Select * from Journal_M where Id=@Id ";
            cmd = new SqlCommand(cmdString, con, transaction);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@Id", HttpUtility.HtmlDecode(jdValueObj.JournalID));

            SqlDataReader sdr = cmd.ExecuteReader();
            JournalData j = new JournalData();
            while (sdr.Read())
            {
                if (sdr.HasRows)
                {

                    if (!Convert.IsDBNull(sdr["Title"]))
                    {
                        j.name = (string)sdr["Title"];

                        // j.name = (string)sdr["AbbreviatedTitle"];
                    }
                    if (!Convert.IsDBNull(sdr["AbbreviatedTitle"]))
                    {
                        j.jname = (string)sdr["AbbreviatedTitle"];

                    }
                    if (!Convert.IsDBNull(sdr["JournalCategory"]))
                    {
                        j.Category = (string)sdr["JournalCategory"];
                    }
                    if (!Convert.IsDBNull(sdr["Id"]))
                    {
                        j.jid = (string)sdr["Id"];
                    }

                }
            }
            return j;
        }
        catch (Exception ex)
        {
            log.Error("Inside Journal_DataObject-JournalEntryCheckExistance catch block ");
            log.Error(ex.Message);
            log.Error(ex.StackTrace);
            throw (ex);
        }
        finally
        {
            cmd.Dispose();
            con.Close();
            cmd.Dispose();
        }
    }




    public JournalData JournalGetImpactFactor(JournalData jdValueObj)
    {
        log.Debug("Inside Journal_DataObject- JournalGetImpactFactor function ");
        int result = 0;

        con = new SqlConnection(str);
        con.Open();

        try
        {
            cmdString = "Select * from Journal_IF_Details where Id=@Id AND Year=@Year ";
            cmd = new SqlCommand(cmdString, con, transaction);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@Id", HttpUtility.HtmlDecode(jdValueObj.JournalID));
            cmd.Parameters.AddWithValue("@Year", HttpUtility.HtmlDecode(jdValueObj.year));

            SqlDataReader sdr = cmd.ExecuteReader();
            JournalData j = new JournalData();
            while (sdr.Read())
            {
                if (sdr.HasRows)
                {
                    if (!Convert.IsDBNull(sdr["Id"]))
                    {
                        j.jid = sdr["Id"].ToString();
                    }
                    if (!Convert.IsDBNull(sdr["ImpactFactor"]))
                    {
                        j.impctfact = (double)sdr["ImpactFactor"];
                    }
                    if (!Convert.IsDBNull(sdr["Comments"]))
                    {
                        j.Comments = sdr["Comments"].ToString();
                    }
                    if (!Convert.IsDBNull(sdr["fiveImpFact"]))
                    {
                        j.fiveimpcrfact = (double)sdr["fiveImpFact"];
                    }




                }
            }
            return j;
        }
        catch (Exception ex)
        {
            log.Error("Inside Journal_DataObject-JournalEntryCheckExistance catch block ");
            log.Error(ex.Message);
            log.Error(ex.StackTrace);
            throw (ex);
        }
        finally
        {
            cmd.Dispose();
            con.Close();
            cmd.Dispose();
        }
    }



    public JournalData JournalGetImpactFactorPublishEntry(JournalData jdValueObj)
    {
        log.Debug("Inside Journal_DataObject- JournalGetImpactFactor function ");
        int result = 0;

        con = new SqlConnection(str);
        con.Open();

        try
        {
            cmdString = "Select * from Journal_IF_Details where Id=@Id AND Year=@Year ";
            cmd = new SqlCommand(cmdString, con, transaction);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@Id", HttpUtility.HtmlDecode(jdValueObj.JournalID));
            cmd.Parameters.AddWithValue("@Year", HttpUtility.HtmlDecode(jdValueObj.year));

            SqlDataReader sdr = cmd.ExecuteReader();
            JournalData jd = new JournalData();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {

                    if (!Convert.IsDBNull(sdr["ImpactFactor"]))
                    {
                        jd.impctfact = (double)sdr["ImpactFactor"];
                    }
                    if (!Convert.IsDBNull(sdr["Comments"]))
                    {
                        jd.Comments = sdr["Comments"].ToString();
                    }
                    if (!Convert.IsDBNull(sdr["fiveImpFact"]))
                    {
                        jd.fiveimpcrfact = (double)sdr["fiveImpFact"];
                    }



                }
            }
            else
            {
                jd.Comments = "false";
            }

            return jd;
        }
        catch (Exception ex)
        {
            log.Error("Inside Journal_DataObject-JournalEntryCheckExistance catch block ");
            log.Error(ex.Message);
            log.Error(ex.StackTrace);
            throw (ex);
        }
        finally
        {
            cmd.Dispose();
            con.Close();
            cmd.Dispose();
        }
    }





    public int JournalEntryUpdateChanges(JournalData jdValueObj, ArrayList list)
    {
        log.Debug("Inside Journal_DataObject- JournalEntryUpadteChanges function ");

        int result;
        int res = 0, res2 = 0, res3 = 0, res4 = 0;
        con = new SqlConnection(str);
        con.Open();
        transaction = con.BeginTransaction();
        try
        {
            cmdString = " Update Journal_IF_Details set ImpactFactor=@ImpactFactor,Comments=@Comments,fiveImpFact=@fiveImpFact where Id=@Id and Year=@Year ";
            cmd = new SqlCommand(cmdString, con, transaction);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@Id", HttpUtility.HtmlDecode(jdValueObj.JournalID));
            cmd.Parameters.AddWithValue("@Year", HttpUtility.HtmlDecode(jdValueObj.year));
            cmd.Parameters.AddWithValue("@ImpactFactor", HttpUtility.HtmlDecode(jdValueObj.impctfact.ToString()));
            cmd.Parameters.AddWithValue("@Comments", HttpUtility.HtmlDecode(jdValueObj.Comments.ToString()));
            cmd.Parameters.AddWithValue("@fiveImpFact", HttpUtility.HtmlDecode(jdValueObj.fiveimpcrfact.ToString()));

            res = cmd.ExecuteNonQuery();


            if (res == 1)
            {
                cmdString = "update Journal_M set Title=@Title , AbbreviatedTitle=@AbbreviatedTitle ,JournalCategory =@JournalCategory where Id=@Id";
                cmd = new SqlCommand(cmdString, con, transaction);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Id", HttpUtility.HtmlDecode(jdValueObj.JournalID));
                cmd.Parameters.AddWithValue("@Title", HttpUtility.HtmlDecode(jdValueObj.Title));
                cmd.Parameters.AddWithValue("@AbbreviatedTitle", HttpUtility.HtmlDecode(jdValueObj.AbbTitle));
                cmd.Parameters.AddWithValue("@JournalCategory", HttpUtility.HtmlDecode(jdValueObj.Category));


                res2 = cmd.ExecuteNonQuery();

            }
            if (res2 > 0)
            {

                for (int i = 0; i < list.Count; i++)
                {

                    cmdString = "delete from Journal_Year_Map where Id=@Id and Year=@Year";
                    cmd = new SqlCommand(cmdString, con, transaction);
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue("@Id", jdValueObj.JournalID);
                    cmd.Parameters.AddWithValue("@Year", list[i].ToString());
                    res3 = cmd.ExecuteNonQuery();
                    cmdString = "Insert Into Journal_Year_Map (Id,Year) Values (@Id,@Year)";
                    cmd = new SqlCommand(cmdString, con, transaction);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@Id", HttpUtility.HtmlDecode(jdValueObj.JournalID));
                    cmd.Parameters.AddWithValue("@Year", HttpUtility.HtmlDecode(list[i].ToString()));
                    res4 = cmd.ExecuteNonQuery();
                }


            }
            transaction.Commit();
            return res;
        }

        catch (Exception ex)
        {
            log.Error("Inside Journal_DataObject- JournalEntryUpdateChangescatch block ");
            log.Error(ex.Message);
            log.Error(ex.StackTrace);
            transaction.Rollback();
            throw (ex);
        }
        finally
        {
            cmd.Dispose();
            con.Close();
            cmd.Dispose();
        }
    }

    public int IFcheckSaveOrUpdate(JournalData jdValueObj)
    {
        log.Debug("Inside Journal_DataObject- IFcheckSaveOrUpdate function ");

        int result;
        int res = 2, res2 = 0;
        con = new SqlConnection(str);
        con.Open();
        // transaction = con.BeginTransaction();
        try
        {

            //cmdString = "Select * from Journal_IF_Details where Id=@Id AND Year=@Year ";
            //cmd = new SqlCommand(cmdString, con, transaction);
            //cmd.CommandType = CommandType.Text;
            //cmd.Parameters.AddWithValue("@Id", HttpUtility.HtmlDecode(jdValueObj.JournalID));
            //cmd.Parameters.AddWithValue("@Year", HttpUtility.HtmlDecode(jdValueObj.year));

            //SqlDataReader sdr = cmd.ExecuteReader();



            //while (sdr.Read())
            //{
            //    if (sdr.HasRows)
            //    {
            //        // update
            //        res = 1;
            //        return res;
            //    }
            //    // }
            //    else
            //    {
            //        // insert.
            //        res = 2;
            //        return res;
            //    }
            //}
            //sdr.Close();


            cmdString = "select * from Journal_M where Id=@Id ";
            cmd = new SqlCommand(cmdString, con, transaction);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@Id", HttpUtility.HtmlDecode(jdValueObj.JournalID));
            //cmd.Parameters.AddWithValue("@Year", HttpUtility.HtmlDecode(jdValueObj.year));

            SqlDataReader sdr1 = cmd.ExecuteReader();
            while (sdr1.Read())
            {
                if (sdr1.HasRows)
                {
                    // update
                    res = 3;
                    return res;
                }
                // }
                else
                {
                    // insert.
                    res = 4;
                    return res;
                }
            }
            sdr1.Close();

            return res;

        }

        catch (Exception ex)
        {
            log.Error("Inside Journal_DataObject- IFcheckSaveOrUpdate  block ");
            log.Error(ex.Message);
            log.Error(ex.StackTrace);
            // transaction.Rollback();
            throw (ex);
        }
        finally
        {
            cmd.Dispose();
            con.Close();
            cmd.Dispose();
        }
    }


    //public int JournalEntrySaveChanges(JournalData jdValueObj)
    //{
    //    log.Debug("Inside Journal_DataObject- JournalEntrySaveChanges function ");

    //    int result;
    //    int res = 0, res2 = 0;
    //    con = new SqlConnection(str);
    //    con.Open();
    //    transaction = con.BeginTransaction();
    //    try
    //    {
    //        cmdString = "Insert Into  Journal_M ( Title , AbbreviatedTitle ,JournalCategory , Id) Values( @Title,@AbbreviatedTitle,@JournalCategory,@Id)";
    //        cmd = new SqlCommand(cmdString, con, transaction);
    //        cmd.CommandType = CommandType.Text;
    //        cmd.Parameters.AddWithValue("@Id", HttpUtility.HtmlDecode(jdValueObj.JournalID));
    //        cmd.Parameters.AddWithValue("@Title", HttpUtility.HtmlDecode(jdValueObj.Title));
    //        cmd.Parameters.AddWithValue("@AbbreviatedTitle", HttpUtility.HtmlDecode(jdValueObj.AbbTitle));
    //        cmd.Parameters.AddWithValue("@JournalCategory", HttpUtility.HtmlDecode(jdValueObj.Category));


    //        res2 = cmd.ExecuteNonQuery();
    //        if (jdValueObj.year != "")
    //        {
    //            cmdString = " Insert Into Journal_IF_Details (Id,Year,ImpactFactor,Comments,fiveImpFact) Values (@Id,@Year,@ImpactFactor,@Comments,@fiveImpFact) ";
    //            cmd = new SqlCommand(cmdString, con, transaction);
    //            cmd.CommandType = CommandType.Text;
    //            cmd.Parameters.AddWithValue("@Id", HttpUtility.HtmlDecode(jdValueObj.JournalID));
    //            cmd.Parameters.AddWithValue("@Year", HttpUtility.HtmlDecode(jdValueObj.year));
    //            cmd.Parameters.AddWithValue("@ImpactFactor", HttpUtility.HtmlDecode(jdValueObj.impctfact.ToString()));
    //            cmd.Parameters.AddWithValue("@Comments", HttpUtility.HtmlDecode(jdValueObj.Comments.ToString()));
    //            cmd.Parameters.AddWithValue("@fiveImpFact", HttpUtility.HtmlDecode(jdValueObj.fiveimpcrfact.ToString()));

    //            res = cmd.ExecuteNonQuery();
    //        }

    //        transaction.Commit();
    //        return res2;
    //    }

    //    catch (Exception ex)
    //    {
    //        log.Error("Inside Journal_DataObject- JournalEntryInsertcatch block ");
    //        log.Error(ex.Message);
    //        log.Error(ex.StackTrace);
    //        transaction.Rollback();
    //        throw (ex);
    //    }
    //    finally
    //    {
    //        cmd.Dispose();
    //        con.Close();
    //        cmd.Dispose();
    //    }
    //}

    public int JournalEntrySaveChanges(JournalData jdValueObj, ArrayList list)
    {
        log.Debug("Inside Journal_DataObject- JournalEntrySaveChanges function ");

        int result;
        int res = 0, res2 = 0, res1 = 0, res3 = 0;
        con = new SqlConnection(str);
        con.Open();
        transaction = con.BeginTransaction();
        try
        {
            cmdString = "Insert Into  Journal_M ( Title , AbbreviatedTitle ,JournalCategory , Id) Values( @Title,@AbbreviatedTitle,@JournalCategory,@Id)";
            cmd = new SqlCommand(cmdString, con, transaction);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@Id", HttpUtility.HtmlDecode(jdValueObj.JournalID));
            cmd.Parameters.AddWithValue("@Title", HttpUtility.HtmlDecode(jdValueObj.Title));
            cmd.Parameters.AddWithValue("@AbbreviatedTitle", HttpUtility.HtmlDecode(jdValueObj.AbbTitle));
            cmd.Parameters.AddWithValue("@JournalCategory", HttpUtility.HtmlDecode(jdValueObj.Category));


            res2 = cmd.ExecuteNonQuery();
            //if (jdValueObj.year != "")
            //{
            //    cmdString = " Insert Into Journal_IF_Details (Id,Year,ImpactFactor,Comments,fiveImpFact,ApplicableDate) Values (@Id,@Year,@ImpactFactor,@Comments,@fiveImpFact,@ApplicableDate) ";
            //    cmd = new SqlCommand(cmdString, con, transaction);
            //    cmd.CommandType = CommandType.Text;
            //    cmd.Parameters.AddWithValue("@Id", HttpUtility.HtmlDecode(jdValueObj.JournalID));
            //    cmd.Parameters.AddWithValue("@Year", HttpUtility.HtmlDecode(jdValueObj.year));
            //    cmd.Parameters.AddWithValue("@ImpactFactor", HttpUtility.HtmlDecode(jdValueObj.impctfact.ToString()));
            //    cmd.Parameters.AddWithValue("@Comments", HttpUtility.HtmlDecode(jdValueObj.Comments.ToString()));
            //    cmd.Parameters.AddWithValue("@fiveImpFact", HttpUtility.HtmlDecode(jdValueObj.fiveimpcrfact.ToString()));
            //    cmd.Parameters.AddWithValue("@ApplicableDate", HttpUtility.HtmlDecode(jdValueObj.ApplicableYear.ToString()));

            //    res = cmd.ExecuteNonQuery();
            //    //if (res >= 1)
            //    //{

            //    //    cmdString = " Insert Into Journal_IF_Details (Id,Year,ImpactFactor,Comments,fiveImpFact,ApplicableDate) Values (@Id,@Year,@ImpactFactor,@Comments,@fiveImpFact,@ApplicableDate) ";
            //    //    cmd = new SqlCommand(cmdString, con, transaction);
            //    //    cmd.CommandType = CommandType.Text;
            //    //    cmd.Parameters.AddWithValue("@Id", HttpUtility.HtmlDecode(jdValueObj.JournalID));
            //    //    cmd.Parameters.AddWithValue("@Year", HttpUtility.HtmlDecode(jdValueObj.Year1));
            //    //    cmd.Parameters.AddWithValue("@ImpactFactor", HttpUtility.HtmlDecode(jdValueObj.impctfact.ToString()));
            //    //    cmd.Parameters.AddWithValue("@Comments", HttpUtility.HtmlDecode(jdValueObj.Comments.ToString()));
            //    //    cmd.Parameters.AddWithValue("@fiveImpFact", HttpUtility.HtmlDecode(jdValueObj.fiveimpcrfact.ToString()));
            //    //    cmd.Parameters.AddWithValue("@ApplicableDate", HttpUtility.HtmlDecode(jdValueObj.ApplicableYear1.ToString()));
            //    //    res = cmd.ExecuteNonQuery();
            //    //}

            //}


            if (res2 > 0)
            {



                cmdString = "delete from Journal_Year_Map where Id=@Id ";
                cmd = new SqlCommand(cmdString, con, transaction);
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@Id", jdValueObj.JournalID);
                //cmd.Parameters.AddWithValue("@Year", list[i].ToString());
                res3 = cmd.ExecuteNonQuery();

                for (int i = 0; i < list.Count; i++)
                {
                    cmdString = "Insert Into Journal_Year_Map (Id,Year) Values (@Id,@Year)";
                    cmd = new SqlCommand(cmdString, con, transaction);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@Id", HttpUtility.HtmlDecode(jdValueObj.JournalID));
                    cmd.Parameters.AddWithValue("@Year", HttpUtility.HtmlDecode(list[i].ToString()));
                    res1 = cmd.ExecuteNonQuery();
                }


            }
            transaction.Commit();
            return res2;
        }

        catch (Exception ex)
        {
            log.Error("Inside Journal_DataObject- JournalEntryInsertcatch block ");
            log.Error(ex.Message);
            log.Error(ex.StackTrace);
            transaction.Rollback();
            throw (ex);
        }
        finally
        {
            cmd.Dispose();
            con.Close();
            cmd.Dispose();
        }
    }

    public int JournalEntrySaveChanges1(JournalData jdValueObj, ArrayList list)
    {
        log.Debug("Inside Journal_DataObject- JournalEntrySaveChanges function ");

        int result;
        int res = 0, res2 = 0, res3 = 0, res4 = 0;
        con = new SqlConnection(str);
        con.Open();
        transaction = con.BeginTransaction();
        try
        {
            //if (jdValueObj.year != "")
            //{

            //    cmdString = " Insert Into Journal_IF_Details (Id,Year,ImpactFactor,Comments,fiveImpFact,ApplicableDate) Values (@Id,@Year,@ImpactFactor,@Comments,@fiveImpFact,@ApplicableDate) ";
            //    cmd = new SqlCommand(cmdString, con, transaction);
            //    cmd.CommandType = CommandType.Text;
            //    cmd.Parameters.AddWithValue("@Id", HttpUtility.HtmlDecode(jdValueObj.JournalID));
            //    cmd.Parameters.AddWithValue("@Year", HttpUtility.HtmlDecode(jdValueObj.year));
            //    cmd.Parameters.AddWithValue("@ImpactFactor", HttpUtility.HtmlDecode(jdValueObj.impctfact.ToString()));
            //    cmd.Parameters.AddWithValue("@Comments", HttpUtility.HtmlDecode(jdValueObj.Comments.ToString()));
            //    cmd.Parameters.AddWithValue("@fiveImpFact", HttpUtility.HtmlDecode(jdValueObj.fiveimpcrfact.ToString()));
            //    cmd.Parameters.AddWithValue("@ApplicableDate", HttpUtility.HtmlDecode(jdValueObj.ApplicableYear.ToString()));
            //    res = cmd.ExecuteNonQuery();
            //}
            //if (res == 1)
            //{
            cmdString = "update   Journal_M set Title=@Title , AbbreviatedTitle=@AbbreviatedTitle ,JournalCategory=@JournalCategory where Id=@Id";
            cmd = new SqlCommand(cmdString, con, transaction);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@Id", HttpUtility.HtmlDecode(jdValueObj.JournalID));
            cmd.Parameters.AddWithValue("@Title", HttpUtility.HtmlDecode(jdValueObj.Title));
            cmd.Parameters.AddWithValue("@AbbreviatedTitle", HttpUtility.HtmlDecode(jdValueObj.AbbTitle));
            cmd.Parameters.AddWithValue("@JournalCategory", HttpUtility.HtmlDecode(jdValueObj.Category));

            res2 = cmd.ExecuteNonQuery();

            //}
            if (res2 > 0)
            {



                cmdString = "delete from Journal_Year_Map where Id=@Id";
                cmd = new SqlCommand(cmdString, con, transaction);
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@Id", jdValueObj.JournalID);
                //cmd.Parameters.AddWithValue("@Year", list[i].ToString());
                res3 = cmd.ExecuteNonQuery();

                for (int i = 0; i < list.Count; i++)
                {
                    cmdString = "Insert Into Journal_Year_Map (Id,Year) Values (@Id,@Year)";
                    cmd = new SqlCommand(cmdString, con, transaction);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@Id", HttpUtility.HtmlDecode(jdValueObj.JournalID));
                    cmd.Parameters.AddWithValue("@Year", HttpUtility.HtmlDecode(list[i].ToString()));
                    res4 = cmd.ExecuteNonQuery();
                }


            }
            transaction.Commit();
            return res4;



        }

        catch (Exception ex)
        {
            log.Error("Inside Journal_DataObject- JournalEntryInsertcatch block ");
            log.Error(ex.Message);
            log.Error(ex.StackTrace);
            transaction.Rollback();
            throw (ex);
        }
        finally
        {
            cmd.Dispose();
            con.Close();
            cmd.Dispose();
        }
    }


    public int insertPublishEntry(PublishData j, PublishData[] jd, ArrayList listIndexAgency)
    {
        log.Debug("Inside insertPublishEntry function : User Name :" + HttpContext.Current.Session["UserName"] + "Role :" + HttpContext.Current.Session["RoleName"]);
        int result = 0, result1 = 0, seed = 0, result2 = 0, result3 = 0;
        string seedFinal = "";
        con = new SqlConnection(str);
        con.Open();
        transaction = con.BeginTransaction();
        try
        {

            cmdString = "select seed from Id_Gen_Publication where Type=@Type";
            cmd = new SqlCommand(cmdString, con, transaction);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("@Type", j.TypeOfEntry);
            seed = (int)cmd.ExecuteScalar();

            //to set prefix 0's to seed value
            string seedStr = seed.ToString();
            int seedlen = seedStr.Length;
            int idlen = Convert.ToInt32(ConfigurationManager.AppSettings["PublicationIdLen"]);
            string pre = "";

            for (int i = 0; i < idlen - seedlen; i++)
            {
                string z = "0";
                pre = pre + z;
            }
            seedFinal = pre + seed.ToString();
            HttpContext.Current.Session["Pubseed"] = seedFinal;
            cmd = new SqlCommand("InsertPublicationEntry", con, transaction);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@PaublicationID", seedFinal);
            cmd.Parameters.AddWithValue("@TypeOfEntry", j.TypeOfEntry);

            cmd.Parameters.AddWithValue("@MUCategorization", j.MUCategorization);

            cmd.Parameters.AddWithValue("@TitleWorkItem", j.TitleWorkItem);

            cmd.Parameters.AddWithValue("@PubJournalID", j.PublisherOfJournal);
            cmd.Parameters.AddWithValue("@PubJournalName", j.PublisherOfJournalName);
            cmd.Parameters.AddWithValue("@JAVolume", j.JAVolume);
            cmd.Parameters.AddWithValue("@PublishJAMonth", j.PublishJAMonth);

            cmd.Parameters.AddWithValue("@PublishJAYear", j.PublishJAYear);
            string date3 = j.PublishDate.ToString();
            if (date3 != "01/01/0001 00:00:00")
            {
                cmd.Parameters.AddWithValue("@PublicationDate", j.PublishDate);
            }
            else
            {
                cmd.Parameters.AddWithValue("@PublicationDate", DBNull.Value);
            }
            //  cmd.Parameters.AddWithValue("@PublicationDate", j.PublishDate);

            if (j.ApprovedBy != null)
            {
                if (j.TypeOfEntry == "JA" || j.TypeOfEntry == "TS" ||j.TypeOfEntry == "PR")
                {
                    if (j.PageFrom != "")
                    {
                        cmd.Parameters.AddWithValue("@PageFrom", j.PageFrom);
                    }
                    else
                    {
                        string pagefrom = "PF" + seedFinal;

                        cmd.Parameters.AddWithValue("@PageFrom", DBNull.Value);
                    }

                }
                else
                {
                    cmd.Parameters.AddWithValue("@PageFrom", j.PageFrom);
                }
                if (j.TypeOfEntry == "JA" || j.TypeOfEntry == "TS" || j.TypeOfEntry == "PR")
                {
                    if (j.PageTo != "")
                    {
                        cmd.Parameters.AddWithValue("@PageTo", j.PageTo);
                    }
                    else
                    {
                        string pageto = "PT" + seedFinal;

                        cmd.Parameters.AddWithValue("@PageTo", DBNull.Value);
                    }
                }
                else
                {
                    cmd.Parameters.AddWithValue("@PageTo", j.PageTo);
                }
            }
            else
            {
                cmd.Parameters.AddWithValue("@PageFrom", j.PageFrom);
                cmd.Parameters.AddWithValue("@PageTo", j.PageTo);
            }
            if (j.Indexed != null)
            {
                cmd.Parameters.AddWithValue("@Indexed", j.Indexed);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Indexed", DBNull.Value);
            }
            cmd.Parameters.AddWithValue("@Issue", j.Issue);
            cmd.Parameters.AddWithValue("@Publicationtype", j.Publicationtype);

            if (j.ImpactFactor != "")
            {
                cmd.Parameters.AddWithValue("@ImpactFactor", j.ImpactFactor);
            }
            else
            {
                cmd.Parameters.AddWithValue("@ImpactFactor", DBNull.Value);
            }
            if (j.ImpactFactor5 != "")
            {

                cmd.Parameters.AddWithValue("@fiveImpfact", j.ImpactFactor5);
            }
            else
            {
                cmd.Parameters.AddWithValue("@fiveImpfact", DBNull.Value);
            }
            if (j.IFApplicableYear != 0)
            {
                cmd.Parameters.AddWithValue("@IFApplicableYear", j.IFApplicableYear);
            }
            else
            {
                cmd.Parameters.AddWithValue("@IFApplicableYear", DBNull.Value);
            }

            cmd.Parameters.AddWithValue("@ConferenceTitle", j.ConferenceTitle);

            cmd.Parameters.AddWithValue("@Place", j.Place);
            if (j.CPCity !=""&&j.CPCity !=null)
            {
                cmd.Parameters.AddWithValue("@CPCity", j.CPCity);
            }
            else
            {
                cmd.Parameters.AddWithValue("@CPCity", DBNull.Value);
            }
            if (j.CPState != "" && j.CPState != null)
            {
                cmd.Parameters.AddWithValue("@CPState", j.CPState);
            }
            else
            {
                cmd.Parameters.AddWithValue("@CPState", DBNull.Value);
            }
            string date1 = j.Date.ToString();
            if (date1 != "01/01/0001 00:00:00")
            {
                cmd.Parameters.AddWithValue("@Date", j.Date);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Date", DBNull.Value);
            }

            string date_New = j.todate.ToString();
            if (date_New != "01/01/0001 00:00:00")
            {
                cmd.Parameters.AddWithValue("@Date1", j.todate);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Date1", DBNull.Value);
            }


            if (j.TitleOfBook != "")
            {
                cmd.Parameters.AddWithValue("@TitleOfBook", j.TitleOfBook);
            }
            else
            {
                cmd.Parameters.AddWithValue("@TitleOfBook", DBNull.Value);
            }
            if (j.TitileOfBookChapter != "")
            {
                cmd.Parameters.AddWithValue("@TitileOfChapter", j.TitileOfBookChapter);
            }
            else
            {
                cmd.Parameters.AddWithValue("@TitileOfChapter", DBNull.Value);
            }
            if (j.Edition != "")
            {
                cmd.Parameters.AddWithValue("@Edition", j.Edition);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Edition", DBNull.Value);
            }

            if (j.Publisher != "")
            {
                cmd.Parameters.AddWithValue("@Publisher", j.Publisher);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Publisher", DBNull.Value);
            }

            if (j.BookPublishYear != "")
            {
                cmd.Parameters.AddWithValue("@BookPublishYear", j.BookPublishYear);
            }

            else
            {
                cmd.Parameters.AddWithValue("@BookPublishYear", DBNull.Value);
            }

            if (j.BookPublishMonth != 0)
            {
                cmd.Parameters.AddWithValue("@BookPublishMonth", j.BookPublishMonth);
            }

            else
            {
                cmd.Parameters.AddWithValue("@BookPublishMonth", DBNull.Value);
            }

            if (j.BookPageNum != "")
            {
                cmd.Parameters.AddWithValue("@BookPublishPageNum", j.BookPageNum);
            }
            else
            {
                cmd.Parameters.AddWithValue("@BookPublishPageNum", DBNull.Value);
            }
            if (j.BookVolume != "")
            {
                cmd.Parameters.AddWithValue("@BookPublishVolume", j.BookVolume);
            }
            else
            {
                cmd.Parameters.AddWithValue("@BookPublishVolume", DBNull.Value);
            }
            if (j.BookSection != "")
            {
                cmd.Parameters.AddWithValue("@BookSection", j.BookSection);
            }
            else
            {
                cmd.Parameters.AddWithValue("@BookSection", DBNull.Value);
            }
            if (j.BookChapter != "")
            {
                cmd.Parameters.AddWithValue("@BookChapter", j.BookChapter);
            }
            else
            {
                cmd.Parameters.AddWithValue("@BookChapter", DBNull.Value);
            }
            if (j.BookCountry != "")
            {
                cmd.Parameters.AddWithValue("@BookCountry", j.BookCountry);
            }
            else
            {
                cmd.Parameters.AddWithValue("@BookCountry", DBNull.Value);
            }
            if (j.BookTypeofPublication != "")
            {
                cmd.Parameters.AddWithValue("@BookTypeofPublication", j.BookTypeofPublication);
            }
            else
            {
                cmd.Parameters.AddWithValue("@BookTypeofPublication", DBNull.Value);
            }
            //if (j.url != "")
            //{

            //    cmd.Parameters.AddWithValue("@URL", j.url);
            //}
            //else
            //{
            //    cmd.Parameters.AddWithValue("@URL", DBNull.Value);
            //}

            cmd.Parameters.AddWithValue("@DOINum", j.DOINum);


            cmd.Parameters.AddWithValue("@Keywords", j.Keywords);
            cmd.Parameters.AddWithValue("@Abstract", j.Abstract);
            // cmd.Parameters.AddWithValue("@Reference", j.TechReferences);

            cmd.Parameters.AddWithValue("@UploadPDFPath", j.UploadPDF);

            cmd.Parameters.AddWithValue("@Status", j.Status);

            cmd.Parameters.AddWithValue("@isERF", j.isERF);
            if (j.uploadEPrint != "")
            {
                cmd.Parameters.AddWithValue("@uploadEPrint", j.uploadEPrint);
            }
            else
            {
                cmd.Parameters.AddWithValue("@uploadEPrint", j.uploadEPrint);
            }

            if (j.EprintURL != "")
            {
                cmd.Parameters.AddWithValue("@EprintURL", "");
            }
            else
            {
                cmd.Parameters.AddWithValue("@EprintURL", DBNull.Value);
            }
            cmd.Parameters.AddWithValue("@SupervisorID", j.SupervisorID);
            if (j.LibraryId != null)
            {
                cmd.Parameters.AddWithValue("@LibraryId", j.LibraryId);
            }
            else
            {
                cmd.Parameters.AddWithValue("@LibraryId", DBNull.Value);
            }

            cmd.Parameters.AddWithValue("@AutoApproved", j.AutoAppoval);
            cmd.Parameters.AddWithValue("@CreatedBy", j.CreatedBy);
            if (j.CreatedDate != null)
            {
                cmd.Parameters.AddWithValue("@CreatedDate", j.CreatedDate);
            }
            else
            {
                cmd.Parameters.AddWithValue("@CreatedDate", DBNull.Value);
            }


            if (j.NewsPublisher != null)
            {
                cmd.Parameters.AddWithValue("@NewsPublisher", j.NewsPublisher);
            }
            else
            {
                cmd.Parameters.AddWithValue("@NewsPublisher", DBNull.Value);
            }
            string date2 = j.NewsPublishedDate.ToString();
            if (date2 != "01/01/0001 00:00:00")
            {

                cmd.Parameters.AddWithValue("@NewsPublishedDate", j.NewsPublishedDate);
            }
            else
            {
                cmd.Parameters.AddWithValue("@NewsPublishedDate", DBNull.Value);
            }

            if (j.NewsPageNum != null)
            {

                cmd.Parameters.AddWithValue("@NewsPageNum", j.NewsPageNum);
            }
            else
            {
                cmd.Parameters.AddWithValue("@NewsPageNum", DBNull.Value);
            }

            cmd.Parameters.AddWithValue("@CreditPoint", j.CreditPoint);
            cmd.Parameters.AddWithValue("@AwardedBy", j.AwardedBy);
            cmd.Parameters.AddWithValue("@TypePresentation", j.TypePresentation);
            cmd.Parameters.AddWithValue("@Institution", j.InstUser);
            cmd.Parameters.AddWithValue("@Department", j.DeptUser);
            if (j.ConfISBN != null)
            {
                cmd.Parameters.AddWithValue("@ISBN", j.ConfISBN);
            }
            else 
            {
                cmd.Parameters.AddWithValue("@ISBN", DBNull.Value);
            }
            cmd.Parameters.AddWithValue("@Institutions", j.AppendInstitutionNames);
            //if (j.CitationUrl != "")
            //{
            //    cmd.Parameters.AddWithValue("@CitationUrl", j.CitationUrl);
            //}
            //else
            //{
            //    cmd.Parameters.AddWithValue("@CitationUrl", DBNull.Value);
            //}
            if (j.FundsUtilized != 0.0 && j.FundsUtilized != null)
            {
                cmd.Parameters.AddWithValue("@FundsUtilized", j.FundsUtilized);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FundsUtilized", DBNull.Value);
            }
            if (j.AutoAppoval == "Y")
            {
                if (j.ApprovedBy != null)
                {
                    cmd.Parameters.AddWithValue("@ApprovedBy", j.ApprovedBy);
                    cmd.Parameters.AddWithValue("@ApprovedDate", DateTime.Now);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@ApprovedBy", DBNull.Value);
                    cmd.Parameters.AddWithValue("@ApprovedDate", DBNull.Value);
                }
            }
            else
            {
                cmd.Parameters.AddWithValue("@ApprovedBy", DBNull.Value);
                cmd.Parameters.AddWithValue("@ApprovedDate", DBNull.Value);
            }
            cmd.Parameters.AddWithValue("@IsStudentAuthor", j.IsStudentAuthor);
            if (j.IncentivePointSatatus != null)
            {
                cmd.Parameters.AddWithValue("@IncentivePointStatus", j.IncentivePointSatatus);
            }
            else
            {
                cmd.Parameters.AddWithValue("@IncentivePointStatus", DBNull.Value);
            }
            if (j.hasProjectreference != null && j.hasProjectreference != "")
            {
                cmd.Parameters.AddWithValue("@hasProjectreference", j.hasProjectreference);
            }
            else
            {
                cmd.Parameters.AddWithValue("@hasProjectreference", DBNull.Value);
            }
            if (j.ProjectIDlist != null && j.ProjectIDlist != "")
            {
                cmd.Parameters.AddWithValue("@ProjectIDlist", j.ProjectIDlist);
            }
            else
            {
                cmd.Parameters.AddWithValue("@ProjectIDlist", DBNull.Value);
            }
            //cmd.Parameters.AddWithValue("@@uploadEPrint", j.LibraryId);

            //cmd.Parameters.AddWithValue("@AutoApproved", "N");
            result = cmd.ExecuteNonQuery();
            if (result >= 1)
            {
                for (int i = 0; i < jd.Length; i++)
                {
                    cmd = new SqlCommand("InsertPublicationAuthor", con, transaction);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@PaublicationID", seedFinal);
                    cmd.Parameters.AddWithValue("@TypeOfEntry", j.TypeOfEntry);

                    cmd.Parameters.AddWithValue("@PublicationLine", i + 1);
                    cmd.Parameters.AddWithValue("@AuthorName", jd[i].AuthorName);
                    cmd.Parameters.AddWithValue("@MUNonMU", jd[i].MUNonMU);
                    cmd.Parameters.AddWithValue("@EmployeeCode", jd[i].EmployeeCode);

                    cmd.Parameters.AddWithValue("@Institution", jd[i].Institution);
                    cmd.Parameters.AddWithValue("@Department", jd[i].Department);

                    cmd.Parameters.AddWithValue("@InstitutionName", jd[i].InstitutionName);
                    cmd.Parameters.AddWithValue("@DepartmentName", jd[i].DepartmentName);
                    cmd.Parameters.AddWithValue("@AuthorType", jd[i].AuthorType);


                    cmd.Parameters.AddWithValue("@isCorrAuth", jd[i].isCorrAuth);
                    cmd.Parameters.AddWithValue("@NameInJournal", jd[i].NameInJournal);
                    cmd.Parameters.AddWithValue("@EmailId", jd[i].EmailId);

                    cmd.Parameters.AddWithValue("@IsPresenter", jd[i].IsPresenter);

                    cmd.Parameters.AddWithValue("@HasAttended", jd[i].HasAttented);
                    cmd.Parameters.AddWithValue("@CreditPoint", jd[i].AuthorCreditPoint);


                    cmd.Parameters.AddWithValue("@NationalInternational", jd[i].NationalInternationl);
                    cmd.Parameters.AddWithValue("@Continent", jd[i].continental);

                    result1 = cmd.ExecuteNonQuery();
                }
            }

            if (listIndexAgency.Count > 0)
            {
                for (int i = 0; i < listIndexAgency.Count; i++)
                {
                    cmd = new SqlCommand("InsertPublicationIndexAgency", con, transaction);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PublicationId", seedFinal);
                    cmd.Parameters.AddWithValue("@Type", j.TypeOfEntry);

                    cmd.Parameters.AddWithValue("@IndexAgency", listIndexAgency[i]);
                    result = cmd.ExecuteNonQuery();

                }
            }



            cmdString = "Select count(* ) as Count from Publish_ReviewTracker where Type=@Type AND Publish_Id=@Publish_Id ";
            cmd = new SqlCommand(cmdString, con, transaction);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@Type", j.TypeOfEntry);
            cmd.Parameters.AddWithValue("@Publish_Id", seedFinal);

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

            cmd = new SqlCommand("InsertPublicationReviewTracker", con, transaction);
            cmd.CommandType = CommandType.StoredProcedure;



            cmd.Parameters.AddWithValue("@Publish_Id", seedFinal);
            cmd.Parameters.AddWithValue("@Type", j.TypeOfEntry);

            cmd.Parameters.AddWithValue("@ReviewNo", count + 1);

            cmd.Parameters.AddWithValue("@ApprovedStatus", j.Status);


            cmd.Parameters.AddWithValue("@Remark", "");

            cmd.Parameters.AddWithValue("@EnterdBy", j.CreatedBy);
            cmd.Parameters.AddWithValue("@dateM", DateTime.Now);

            result = cmd.ExecuteNonQuery();
            if (j.TypeOfEntry == "JA")
            {
                if ((Convert.ToInt32(j.PublishJAYear) >= 2018) && (j.PublishJAMonth >= 7))
                {
                    int PublishJAYear = Convert.ToInt32(j.PublishJAYear);
                    int PublishJAMonth = Convert.ToInt32(j.PublishJAMonth);
                    cmdString = "SelectQuartileApplicableYearWise";
                    cmd = new SqlCommand(cmdString, con, transaction);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", j.PublisherOfJournal);
                    cmd.Parameters.AddWithValue("@PublishJAYear", PublishJAYear);
                    cmd.Parameters.AddWithValue("@PublishJAMonth", PublishJAMonth);
                    cmd.Parameters.AddWithValue("@QuartileStartMonth", j.Quartilefrommonth);
                    cmd.Parameters.AddWithValue("@QuartileEndMonth", j.QuartileTomonth);

                    SqlDataReader sdr1 = cmd.ExecuteReader();

                    string Quartile = "NA";

                    while (sdr1.Read())
                    {
                        if (!Convert.IsDBNull(sdr1["Quartile"]))
                        {
                            Quartile = (string)sdr1["Quartile"];
                        }
                        else
                        {
                            Quartile = "NA";
                        }

                    }
                    sdr1.Close();

                    cmdString = "Update Publication set QuartileOnEntry=@Quartile where PublicationID=@PaublicationID and TypeOfEntry=@TypeOfEntry  and MUCategorization=@MUCategorization";

                    cmd = new SqlCommand(cmdString, con, transaction);
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue("@PaublicationID", seedFinal);
                    cmd.Parameters.AddWithValue("@TypeOfEntry", j.TypeOfEntry);
                    cmd.Parameters.AddWithValue("@MUCategorization", j.MUCategorization);
                    cmd.Parameters.AddWithValue("@Quartile", Quartile);


                    result = cmd.ExecuteNonQuery();

                }
                else if ((Convert.ToInt32(j.PublishJAYear) >= 2019) && (j.PublishJAMonth >= 1))
                {
                    cmdString = "select Quartile from Journal_Quartile_Map where JournalId=@PubJournalID and Year=@Year ";
                    cmd = new SqlCommand(cmdString, con, transaction);
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue("@PubJournalID", j.PublisherOfJournal);
                    cmd.Parameters.AddWithValue("@Year", j.PublishJAYear);

                    SqlDataReader sdr1 = cmd.ExecuteReader();

                    string Quartile = "NA";

                    while (sdr1.Read())
                    {
                        if (!Convert.IsDBNull(sdr1["Quartile"]))
                        {
                            Quartile = (string)sdr1["Quartile"];
                        }
                        else
                        {
                            Quartile = "NA";
                        }

                    }
                    sdr1.Close();

                    cmdString = "Update Publication set QuartileOnEntry=@Quartile where PublicationID=@PaublicationID and TypeOfEntry=@TypeOfEntry  and MUCategorization=@MUCategorization";

                    cmd = new SqlCommand(cmdString, con, transaction);
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue("@PaublicationID", seedFinal);
                    cmd.Parameters.AddWithValue("@TypeOfEntry", j.TypeOfEntry);
                    cmd.Parameters.AddWithValue("@MUCategorization", j.MUCategorization);
                    cmd.Parameters.AddWithValue("@Quartile", Quartile);


                    result = cmd.ExecuteNonQuery();

                }
            }
            transaction.Commit();
            if (j.Status == "APP")
            {
                log.Info("Publication details Approved : Publication ID :" + seedFinal + "Type Of Entry :" + j.TypeOfEntry);
                log.Info("Publication details Approved : User Name :" + HttpContext.Current.Session["UserName"] + "Role :" + HttpContext.Current.Session["RoleName"]);

            }
            else if (j.Status == "SUB")
            {
                log.Info("Publication details Submitted : Publication ID :" + seedFinal + "Type Of Entry :" + j.TypeOfEntry);
                log.Info("Publication details Submitted : User Name :" + HttpContext.Current.Session["UserName"] + "Role :" + HttpContext.Current.Session["RoleName"]);
            }
            return result1;
        }

        catch (Exception ex)
        {
            log.Error("Inside Student_GL_DataObject- insertJournalEntry catch block ");
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




    public int UpdatePublishEntry(PublishData j, PublishData[] jd, ArrayList listIndexAgency)
    {

        int result = 0, result1 = 0, seed = 0, result2 = 0, result3 = 0;
        string seedFinal = "";
        con = new SqlConnection(str);
        con.Open();
        transaction = con.BeginTransaction();
        try
        {
            cmd = new SqlCommand("UpdatePublicationEntry", con, transaction);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@PaublicationID", j.PaublicationID);
            cmd.Parameters.AddWithValue("@TypeOfEntry", j.TypeOfEntry);

            cmd.Parameters.AddWithValue("@MUCategorization", j.MUCategorization);

            cmd.Parameters.AddWithValue("@TitleWorkItem", j.TitleWorkItem);

            cmd.Parameters.AddWithValue("@PubJournalID", j.PublisherOfJournal);
            cmd.Parameters.AddWithValue("@PubJournalName", j.PublisherOfJournalName);
            cmd.Parameters.AddWithValue("@JAVolume", j.JAVolume);
            cmd.Parameters.AddWithValue("@PublishJAMonth", j.PublishJAMonth);

            cmd.Parameters.AddWithValue("@PublishJAYear", j.PublishJAYear);
            string date3 = j.PublishDate.ToString();
            if (date3 != "01/01/0001 00:00:00")
            {
                cmd.Parameters.AddWithValue("@PublicationDate", j.PublishDate);
            }
            else
            {
                cmd.Parameters.AddWithValue("@PublicationDate", DBNull.Value);
            }

            cmd.Parameters.AddWithValue("@PageFrom", j.PageFrom);

            cmd.Parameters.AddWithValue("@Issue", j.Issue);

            cmd.Parameters.AddWithValue("@PageTo", j.PageTo);
            if (j.Indexed != null)
            {
                cmd.Parameters.AddWithValue("@Indexed", j.Indexed);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Indexed", DBNull.Value);
            }
            cmd.Parameters.AddWithValue("@Publicationtype", j.Publicationtype);

            if (j.ImpactFactor != "")
            {
                cmd.Parameters.AddWithValue("@ImpactFactor", j.ImpactFactor);
            }
            else
            {
                cmd.Parameters.AddWithValue("@ImpactFactor", DBNull.Value);
            }
            if (j.ImpactFactor5 != "")
            {

                cmd.Parameters.AddWithValue("@fiveImpfact", j.ImpactFactor5);
            }
            else
            {
                cmd.Parameters.AddWithValue("@fiveImpfact", DBNull.Value);
            }
            if (j.IFApplicableYear != 0)
            {
                cmd.Parameters.AddWithValue("@IFApplicableYear", j.IFApplicableYear);
            }
            else
            {
                cmd.Parameters.AddWithValue("@IFApplicableYear", DBNull.Value);
            }

            cmd.Parameters.AddWithValue("@ConferenceTitle", j.ConferenceTitle);

            cmd.Parameters.AddWithValue("@Place", j.Place);

            if (j.CPCity != ""&&j.CPCity !=null)
            {
                cmd.Parameters.AddWithValue("@CPCity", j.CPCity);
            }
            else
            {
                cmd.Parameters.AddWithValue("@CPCity", DBNull.Value);
            }
            if (j.CPState != "" && j.CPState != null)
            {
                cmd.Parameters.AddWithValue("@CPState", j.CPState);
            }
            else
            {
                cmd.Parameters.AddWithValue("@CPState", DBNull.Value);
            }
            string date1 = j.Date.ToString();
            if (date1 != "01/01/0001 00:00:00")
            {
                cmd.Parameters.AddWithValue("@Date", j.Date);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Date", DBNull.Value);
            }

            string date_new = j.todate.ToString();
            if (date_new != "01/01/0001 00:00:00")
            {
                cmd.Parameters.AddWithValue("@Date1", j.todate);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Date1", DBNull.Value);
            }


            if (j.TitleOfBook != "")
            {
                cmd.Parameters.AddWithValue("@TitleOfBook", j.TitleOfBook);
            }
            else
            {
                cmd.Parameters.AddWithValue("@TitleOfBook", DBNull.Value);
            }
            if (j.TitileOfBookChapter != "")
            {
                cmd.Parameters.AddWithValue("@TitileOfChapter", j.TitileOfBookChapter);
            }
            else
            {
                cmd.Parameters.AddWithValue("@TitileOfChapter", DBNull.Value);
            }
            if (j.Edition != "")
            {
                cmd.Parameters.AddWithValue("@Edition", j.Edition);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Edition", DBNull.Value);
            }

            if (j.Publisher != "")
            {
                cmd.Parameters.AddWithValue("@Publisher", j.Publisher);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Publisher", DBNull.Value);
            }

            if (j.BookPublishYear != "")
            {
                cmd.Parameters.AddWithValue("@BookPublishYear", j.BookPublishYear);
            }

            else
            {
                cmd.Parameters.AddWithValue("@BookPublishYear", DBNull.Value);
            }

            if (j.BookPublishMonth != 0)
            {
                cmd.Parameters.AddWithValue("@BookPublishMonth", j.BookPublishMonth);
            }

            else
            {
                cmd.Parameters.AddWithValue("@BookPublishMonth", DBNull.Value);
            }

            if (j.BookPageNum != "")
            {
                cmd.Parameters.AddWithValue("@BookPublishPageNum", j.BookPageNum);
            }
            else
            {
                cmd.Parameters.AddWithValue("@BookPublishPageNum", DBNull.Value);
            }
            if (j.BookVolume != "")
            {
                cmd.Parameters.AddWithValue("@BookPublishVolume", j.BookVolume);
            }
            else
            {
                cmd.Parameters.AddWithValue("@BookPublishVolume", DBNull.Value);
            }
            if (j.BookSection != "")
            {
                cmd.Parameters.AddWithValue("@BookSection", j.BookSection);
            }
            else
            {
                cmd.Parameters.AddWithValue("@BookSection", DBNull.Value);
            }
            if (j.BookChapter != "")
            {
                cmd.Parameters.AddWithValue("@BookChapter", j.BookChapter);
            }
            else
            {
                cmd.Parameters.AddWithValue("@BookChapter", DBNull.Value);
            }
            if (j.BookCountry != "")
            {
                cmd.Parameters.AddWithValue("@BookCountry", j.BookCountry);
            }
            else
            {
                cmd.Parameters.AddWithValue("@BookCountry", DBNull.Value);
            }
            if (j.BookTypeofPublication != "")
            {
                cmd.Parameters.AddWithValue("@BookTypeofPublication", j.BookTypeofPublication);
            }
            else
            {
                cmd.Parameters.AddWithValue("@BookTypeofPublication", DBNull.Value);
            }
            //if (j.url != "")
            //{

            //    cmd.Parameters.AddWithValue("@URL", j.url);
            //}
            //else
            //{
            //    cmd.Parameters.AddWithValue("@URL", DBNull.Value);
            //}

            cmd.Parameters.AddWithValue("@DOINum", j.DOINum);


            cmd.Parameters.AddWithValue("@Keywords", j.Keywords);
            cmd.Parameters.AddWithValue("@Abstract", j.Abstract);
            // cmd.Parameters.AddWithValue("@Reference", j.TechReferences);  
            if (j.FilePath == null)
            {
                cmd.Parameters.AddWithValue("@UploadPDFPath", DBNull.Value);
            }
            else
            {

                cmd.Parameters.AddWithValue("@UploadPDFPath", j.FilePath);
            }
            cmd.Parameters.AddWithValue("@Status", j.Status);

            cmd.Parameters.AddWithValue("@isERF", j.isERF);
            if (j.uploadEPrint != "")
            {
                cmd.Parameters.AddWithValue("@uploadEPrint", j.uploadEPrint);
            }
            else
            {
                cmd.Parameters.AddWithValue("@uploadEPrint", DBNull.Value);
            }

            if (j.EprintURL != "")
            {
                cmd.Parameters.AddWithValue("@EprintURL", "");
            }
            else
            {
                cmd.Parameters.AddWithValue("@EprintURL", DBNull.Value);
            }
            cmd.Parameters.AddWithValue("@SupervisorID", j.SupervisorID);

            cmd.Parameters.AddWithValue("@LibraryId", j.LibraryId);

            cmd.Parameters.AddWithValue("@AutoApproved", j.AutoAppoval);
            if (j.NewsPublisher != null)
            {
                cmd.Parameters.AddWithValue("@NewsPublisher", j.NewsPublisher);
            }
            else
            {
                cmd.Parameters.AddWithValue("@NewsPublisher", DBNull.Value);
            }
            string date2 = j.NewsPublishedDate.ToString();
            if (date2 != "01/01/0001 00:00:00")
            {

                cmd.Parameters.AddWithValue("@NewsPublishedDate", j.NewsPublishedDate);
            }
            else
            {
                cmd.Parameters.AddWithValue("@NewsPublishedDate", DBNull.Value);
            }

            if (j.NewsPageNum != null)
            {

                cmd.Parameters.AddWithValue("@NewsPageNum", j.NewsPageNum);
            }
            else
            {
                cmd.Parameters.AddWithValue("@NewsPageNum", DBNull.Value);
            }
            cmd.Parameters.AddWithValue("@CreditPoint", j.CreditPoint);
            cmd.Parameters.AddWithValue("@AwardedBy", j.AwardedBy);
            cmd.Parameters.AddWithValue("@TypePresentation", j.TypePresentation);
            if (j.FundsUtilized != 0.0 && j.FundsUtilized != null)
            {
                cmd.Parameters.AddWithValue("@FundsUtilized", j.FundsUtilized);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FundsUtilized", DBNull.Value);
            }
            if (j.ConfISBN != null)
            {
                cmd.Parameters.AddWithValue("@ISBN", j.ConfISBN);
            }
            else
            {
                cmd.Parameters.AddWithValue("@ISBN", DBNull.Value);
            }          

            cmd.Parameters.AddWithValue("@Institutions", j.AppendInstitutionNames);
          
            if (j.hasProjectreference != null && j.hasProjectreference != "")
            {
                cmd.Parameters.AddWithValue("@hasProjectreference", j.hasProjectreference);
            }
            else
            {
                cmd.Parameters.AddWithValue("@hasProjectreference", DBNull.Value);
            }
            if (j.ProjectIDlist != null && j.ProjectIDlist != "")
            {
                cmd.Parameters.AddWithValue("@ProjectIDlist", j.ProjectIDlist);
            }
            else
            {
                cmd.Parameters.AddWithValue("@ProjectIDlist", DBNull.Value);
            }
            //cmd.Parameters.AddWithValue("@CitationUrl", j.CitationUrl);
            result = cmd.ExecuteNonQuery();
            if (result >= 1)
            {
                for (int i = 0; i < jd.Length; i++)
                {
                    cmd = new SqlCommand("InsertPublicationAuthor", con, transaction);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@PaublicationID", j.PaublicationID);
                    cmd.Parameters.AddWithValue("@TypeOfEntry", j.TypeOfEntry);

                    cmd.Parameters.AddWithValue("@PublicationLine", i + 1);
                    cmd.Parameters.AddWithValue("@AuthorName", jd[i].AuthorName);
                    cmd.Parameters.AddWithValue("@MUNonMU", jd[i].MUNonMU);
                    cmd.Parameters.AddWithValue("@EmployeeCode", jd[i].EmployeeCode);

                    cmd.Parameters.AddWithValue("@Institution", jd[i].Institution);
                    cmd.Parameters.AddWithValue("@Department", jd[i].Department);
                    cmd.Parameters.AddWithValue("@AuthorType", jd[i].AuthorType);


                    cmd.Parameters.AddWithValue("@isCorrAuth", jd[i].isCorrAuth);
                    cmd.Parameters.AddWithValue("@NameInJournal", jd[i].NameInJournal);
                    cmd.Parameters.AddWithValue("@EmailId", jd[i].EmailId);

                    cmd.Parameters.AddWithValue("@InstitutionName", jd[i].InstitutionName);
                    cmd.Parameters.AddWithValue("@DepartmentName", jd[i].DepartmentName);
                    cmd.Parameters.AddWithValue("@IsPresenter", jd[i].IsPresenter);

                    cmd.Parameters.AddWithValue("@HasAttended", jd[i].HasAttented);
                    cmd.Parameters.AddWithValue("@CreditPoint", jd[i].AuthorCreditPoint);

                    cmd.Parameters.AddWithValue("@NationalInternational", jd[i].NationalInternationl);
                    cmd.Parameters.AddWithValue("@Continent", jd[i].continental);

                    result1 = cmd.ExecuteNonQuery();




                }
            }

            if (listIndexAgency.Count > 0)
            {
                for (int i = 0; i < listIndexAgency.Count; i++)
                {




                    cmd = new SqlCommand("InsertPublicationIndexAgency", con, transaction);
                    cmd.CommandType = CommandType.StoredProcedure;



                    cmd.Parameters.AddWithValue("@PublicationId", j.PaublicationID);
                    cmd.Parameters.AddWithValue("@Type", j.TypeOfEntry);

                    cmd.Parameters.AddWithValue("@IndexAgency", listIndexAgency[i]);




                    result = cmd.ExecuteNonQuery();

                }
            }



            transaction.Commit();
            return result1;
        }

        catch (Exception ex)
        {
            log.Error("Inside Student_GL_DataObject- insertJournalEntry catch block ");
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



    public int UpdatePublishLibraryEntry(PublishData j, PublishData[] jd, ArrayList listIndexAgency)
    {

        int result = 0, result1 = 0, seed = 0, result2 = 0, result3 = 0;
        string seedFinal = "";
        con = new SqlConnection(str);
        con.Open();
        transaction = con.BeginTransaction();
        try
        {





            cmd = new SqlCommand("UpdatePublicationEntryLibrarian", con, transaction);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@PaublicationID", j.PaublicationID);
            cmd.Parameters.AddWithValue("@TypeOfEntry", j.TypeOfEntry);

            cmd.Parameters.AddWithValue("@uploadEPrint", j.uploadEPrint);

            cmd.Parameters.AddWithValue("@EprintURL", j.EprintURL);
            //if (j.IncentivePointSatatus != null)
            //{
            //    cmd.Parameters.AddWithValue("@IncentivePointStatus", j.IncentivePointSatatus);
            //}
            //else
            //{
            //    cmd.Parameters.AddWithValue("@IncentivePointStatus", DBNull.Value);
            //}
            result = cmd.ExecuteNonQuery();
            transaction.Commit();
            log.Info("Article is uploaded to e-print : ID : " + j.PaublicationID + " Publication Type : " + j.TypeOfEntry + " Incentive Point Status : " + j.IncentivePointSatatus);
            log.Info("E-Print Upload : User Name :" + HttpContext.Current.Session["UserName"] + "Role :" + HttpContext.Current.Session["RoleName"]);
            return result;
        }

        catch (Exception ex)
        {
            log.Error("Inside Student_GL_DataObject- insertJournalEntry catch block ");
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


    public int UpdateCancelPublishEntry(PublishData j)
    {

        int result = 0, result1 = 0, seed = 0, result2 = 0, result3 = 0;
        string seedFinal = "";
        con = new SqlConnection(str);
        con.Open();
        transaction = con.BeginTransaction();
        try
        {
            cmd = new SqlCommand("UpdateCancelPublicationEntry", con, transaction);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PaublicationID", j.PaublicationID);
            cmd.Parameters.AddWithValue("@TypeOfEntry", j.TypeOfEntry);
            cmd.Parameters.AddWithValue("@Status", "CAN");
            cmd.Parameters.AddWithValue("@CancelledBy", j.CancelledBy);
            cmd.Parameters.AddWithValue("@cancelledDate", DateTime.Now);
            cmd.Parameters.AddWithValue("@CancelRemarks", j.RemarksFeedback);
            cmd.Parameters.AddWithValue("@IncentivePointStatus", DBNull.Value);
            result = cmd.ExecuteNonQuery();
            transaction.Commit();
            log.Info("Publication ID " + j.PaublicationID + " Type Of Entry " + j.TypeOfEntry + " cancelled sucessfuly");
            log.Info("Publication Cancellation : User Name :" + HttpContext.Current.Session["UserName"] + "Role :" + HttpContext.Current.Session["RoleName"]);

            return result;
        }

        catch (Exception ex)
        {
            log.Error("Inside Student_GL_DataObject- insertJournalEntry catch block ");
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


    public int UpdateImpFactorPublishEntry(PublishData j)
    {

        int result = 0, result1 = 0, seed = 0, result2 = 0, result3 = 0;
        string seedFinal = "";
        con = new SqlConnection(str);
        con.Open();
        transaction = con.BeginTransaction();
        try
        {
            cmd = new SqlCommand("UpdateImpactFactorPublicationEntry", con, transaction);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@PaublicationID", j.PaublicationID);
            cmd.Parameters.AddWithValue("@TypeOfEntry", j.TypeOfEntry);
            cmd.Parameters.AddWithValue("@ImpactFactor", j.ImpactFactor);
            result = cmd.ExecuteNonQuery();
            transaction.Commit();
            return result;
        }

        catch (Exception ex)
        {
            log.Error("Inside Student_GL_DataObject- insertJournalEntry catch block ");
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

    public int UpdatePostApprovePublishEntry(PublishData j, PublishData[] jd, ArrayList listIndexAgency)
    {

        int result = 0, result1 = 0, seed = 0, result2 = 0, result3 = 0;
        string seedFinal = "";
        con = new SqlConnection(str);
        con.Open();
        transaction = con.BeginTransaction();
        try
        {
            cmd = new SqlCommand("UpdatePostApprovalPublicationEntry", con, transaction);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@PaublicationID", j.PaublicationID);
            cmd.Parameters.AddWithValue("@TypeOfEntry", j.TypeOfEntry);


            cmd.Parameters.AddWithValue("@JAVolume", j.JAVolume);
            cmd.Parameters.AddWithValue("@PublishJAMonth", j.PublishJAMonth);

            cmd.Parameters.AddWithValue("@PublishJAYear", j.PublishJAYear);

            if (j.TypeOfEntry == "JA" || j.TypeOfEntry == "TS" || j.TypeOfEntry == "PR")
            {
                if (j.PageFrom != "")
                {
                    cmd.Parameters.AddWithValue("@PageFrom", j.PageFrom);
                }
                else
                {
                    string pagefrom = "PF" + j.PaublicationID;

                    cmd.Parameters.AddWithValue("@PageFrom", DBNull.Value);
                }

            }
            else
            {
                cmd.Parameters.AddWithValue("@PageFrom", j.PageFrom);
            }

            cmd.Parameters.AddWithValue("@Issue", j.Issue);

            if (j.TypeOfEntry == "JA" || j.TypeOfEntry == "TS" || j.TypeOfEntry == "PR")
            {
                if (j.PageTo != "")
                {
                    cmd.Parameters.AddWithValue("@PageTo", j.PageTo);
                }
                else
                {
                    string pageto = "PT" + j.PaublicationID;

                    cmd.Parameters.AddWithValue("@PageTo", DBNull.Value);
                }
            }
            else
            {
                cmd.Parameters.AddWithValue("@PageTo", j.PageTo);
            }

            cmd.Parameters.AddWithValue("@Publicationtype", j.Publicationtype);

            cmd.Parameters.AddWithValue("@ImpactFactor", j.ImpactFactor);

            cmd.Parameters.AddWithValue("@ConferenceTitle", j.ConferenceTitle);

            cmd.Parameters.AddWithValue("@Place", j.Place);
            if (j.CPCity != "" && j.CPCity != null)
            {
                cmd.Parameters.AddWithValue("@CPCity", j.CPCity);
            }
            else
            {
                cmd.Parameters.AddWithValue("@CPCity", DBNull.Value);
            }
            if (j.CPState != "" && j.CPState != null)
            {
                cmd.Parameters.AddWithValue("@CPState", j.CPState);
            }
            else
            {
                cmd.Parameters.AddWithValue("@CPState", DBNull.Value);
            }
            string date1 = j.Date.ToString();
            if (date1 != "01/01/0001 00:00:00")
            {
                cmd.Parameters.AddWithValue("@Date", j.Date);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Date", DBNull.Value);
            }
            if (j.TitleOfBook != "")
            {
                cmd.Parameters.AddWithValue("@TitleOfBook", j.TitleOfBook);
            }
            else
            {
                cmd.Parameters.AddWithValue("@TitleOfBook", DBNull.Value);
            }
            if (j.TitileOfBookChapter != "")
            {
                cmd.Parameters.AddWithValue("@TitileOfChapter", j.TitileOfBookChapter);
            }
            else
            {
                cmd.Parameters.AddWithValue("@TitileOfChapter", DBNull.Value);
            }
            if (j.Edition != "")
            {
                cmd.Parameters.AddWithValue("@Edition", j.Edition);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Edition", DBNull.Value);
            }

            if (j.Publisher != "")
            {
                cmd.Parameters.AddWithValue("@Publisher", j.Publisher);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Publisher", DBNull.Value);
            }

            if (j.BookPublishYear != "")
            {
                cmd.Parameters.AddWithValue("@BookPublishYear", j.BookPublishYear);
            }

            else
            {
                cmd.Parameters.AddWithValue("@BookPublishYear", DBNull.Value);
            }

            if (j.BookPublishMonth != 0)
            {
                cmd.Parameters.AddWithValue("@BookPublishMonth", j.BookPublishMonth);
            }

            else
            {
                cmd.Parameters.AddWithValue("@BookPublishMonth", DBNull.Value);
            }

            if (j.BookPageNum != "")
            {
                cmd.Parameters.AddWithValue("@BookPublishPageNum", j.BookPageNum);
            }
            else
            {
                cmd.Parameters.AddWithValue("@BookPublishPageNum", DBNull.Value);
            }
            if (j.BookVolume != "")
            {
                cmd.Parameters.AddWithValue("@BookPublishVolume", j.BookVolume);
            }
            else
            {
                cmd.Parameters.AddWithValue("@BookPublishVolume", DBNull.Value);
            }
            if (j.BookSection != "")
            {
                cmd.Parameters.AddWithValue("@BookSection", j.BookSection);
            }
            else
            {
                cmd.Parameters.AddWithValue("@BookSection", DBNull.Value);
            }
            if (j.BookChapter != "")
            {
                cmd.Parameters.AddWithValue("@BookChapter", j.BookChapter);
            }
            else
            {
                cmd.Parameters.AddWithValue("@BookChapter", DBNull.Value);
            }
            if (j.BookCountry != "")
            {
                cmd.Parameters.AddWithValue("@BookCountry", j.BookCountry);
            }
            else
            {
                cmd.Parameters.AddWithValue("@BookCountry", DBNull.Value);
            }
            if (j.BookTypeofPublication != "")
            {
                cmd.Parameters.AddWithValue("@BookTypeofPublication", j.BookTypeofPublication);
            }
            else
            {
                cmd.Parameters.AddWithValue("@BookTypeofPublication", DBNull.Value);
            }
            //if (j.url != "")
            //{

            //    cmd.Parameters.AddWithValue("@URL", j.url);
            //}
            //else
            //{
            //    cmd.Parameters.AddWithValue("@URL", DBNull.Value);
            //}

            cmd.Parameters.AddWithValue("@DOINum", j.DOINum);


            cmd.Parameters.AddWithValue("@Keywords", j.Keywords);
            cmd.Parameters.AddWithValue("@Abstract", j.Abstract);


            cmd.Parameters.AddWithValue("@isERF", j.isERF);


            if (j.EprintURL != "")
            {
                cmd.Parameters.AddWithValue("@EprintURL", "");
            }
            else
            {
                cmd.Parameters.AddWithValue("@EprintURL", DBNull.Value);
            }




            if (j.NewsPublisher != null)
            {
                cmd.Parameters.AddWithValue("@NewsPublisher", j.NewsPublisher);
            }
            else
            {
                cmd.Parameters.AddWithValue("@NewsPublisher", DBNull.Value);
            }
            string date2 = j.NewsPublishedDate.ToString();
            if (date2 != "01/01/0001 00:00:00")
            {

                cmd.Parameters.AddWithValue("@NewsPublishedDate", j.NewsPublishedDate);
            }
            else
            {
                cmd.Parameters.AddWithValue("@NewsPublishedDate", DBNull.Value);
            }

            if (j.NewsPageNum != null)
            {

                cmd.Parameters.AddWithValue("@NewsPageNum", j.NewsPageNum);
            }
            else
            {
                cmd.Parameters.AddWithValue("@NewsPageNum", DBNull.Value);
            }
            cmd.Parameters.AddWithValue("@CreditPoint", j.CreditPoint);
            cmd.Parameters.AddWithValue("@AwardedBy", j.AwardedBy);
            cmd.Parameters.AddWithValue("@TypePresentation", j.TypePresentation);
            if (j.ConfISBN != null && j.ConfISBN != "")
            {
                cmd.Parameters.AddWithValue("@ISBN", j.ConfISBN);
            }
            else
            {
                cmd.Parameters.AddWithValue("@ISBN", DBNull.Value);
            }


            cmd.Parameters.AddWithValue("@Institutions", j.AppendInstitutionNames);
            if (j.FundsUtilized != 0.0 && j.FundsUtilized != null)
            {
                cmd.Parameters.AddWithValue("@FundsUtilized", j.FundsUtilized);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FundsUtilized", DBNull.Value);
            }
            //cmd.Parameters.AddWithValue("@CitationUrl", j.CitationUrl);
            result = cmd.ExecuteNonQuery();







            cmdString = "delete from Publishcation_Author where PaublicationID=@PaublicationID and TypeOfEntry=@TypeOfEntry";
            cmd = new SqlCommand(cmdString, con, transaction);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("@PaublicationID", j.PaublicationID);
            cmd.Parameters.AddWithValue("@TypeOfEntry", j.TypeOfEntry);

            result1 = cmd.ExecuteNonQuery();


            for (int i = 0; i < jd.Length; i++)
            {
                cmd = new SqlCommand("InsertPublicationAuthor", con, transaction);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@PaublicationID", j.PaublicationID);
                cmd.Parameters.AddWithValue("@TypeOfEntry", j.TypeOfEntry);

                cmd.Parameters.AddWithValue("@PublicationLine", i + 1);
                cmd.Parameters.AddWithValue("@AuthorName", jd[i].AuthorName);
                cmd.Parameters.AddWithValue("@MUNonMU", jd[i].MUNonMU);
                cmd.Parameters.AddWithValue("@EmployeeCode", jd[i].EmployeeCode);

                cmd.Parameters.AddWithValue("@Institution", jd[i].Institution);
                cmd.Parameters.AddWithValue("@Department", jd[i].Department);
                cmd.Parameters.AddWithValue("@AuthorType", jd[i].AuthorType);


                cmd.Parameters.AddWithValue("@isCorrAuth", jd[i].isCorrAuth);
                cmd.Parameters.AddWithValue("@NameInJournal", jd[i].NameInJournal);
                cmd.Parameters.AddWithValue("@EmailId", jd[i].EmailId);

                cmd.Parameters.AddWithValue("@InstitutionName", jd[i].InstitutionName);
                cmd.Parameters.AddWithValue("@DepartmentName", jd[i].DepartmentName);
                cmd.Parameters.AddWithValue("@IsPresenter", jd[i].IsPresenter);

                cmd.Parameters.AddWithValue("@HasAttended", jd[i].HasAttented);
                cmd.Parameters.AddWithValue("@CreditPoint", jd[i].AuthorCreditPoint);
                cmd.Parameters.AddWithValue("@NationalInternational", jd[i].NationalInternationl);
                cmd.Parameters.AddWithValue("@Continent", jd[i].continental);

                result1 = cmd.ExecuteNonQuery();





            }


            if (listIndexAgency.Count > 0)
            {
                for (int i = 0; i < listIndexAgency.Count; i++)
                {




                    cmd = new SqlCommand("InsertPublicationIndexAgency", con, transaction);
                    cmd.CommandType = CommandType.StoredProcedure;



                    cmd.Parameters.AddWithValue("@PublicationId", j.PaublicationID);
                    cmd.Parameters.AddWithValue("@Type", j.TypeOfEntry);

                    cmd.Parameters.AddWithValue("@IndexAgency", listIndexAgency[i]);




                    result = cmd.ExecuteNonQuery();

                }
            }
            cmdString = "Select count(* ) as Count from PublicationPostApprovalTracker where TypeOfEntry=@Type AND PublicationID=@Publish_Id ";
            cmd = new SqlCommand(cmdString, con, transaction);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@Type", j.TypeOfEntry);
            cmd.Parameters.AddWithValue("@Publish_Id", j.PaublicationID);

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

            if (result1 == 1)
            {
                cmd = new SqlCommand("InsertPublicationPostApprovalTracker", con, transaction);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@PublicationID", j.PaublicationID);
                cmd.Parameters.AddWithValue("@TypeOfEntry", j.TypeOfEntry);
                cmd.Parameters.AddWithValue("@line", count + 1);
                cmd.Parameters.AddWithValue("@UpdatedUser", j.CreatedBy);
                cmd.Parameters.AddWithValue("@UpdatedDate", DateTime.Now);
                cmd.Parameters.AddWithValue("@Remarks", j.PostApprvRemarks);


                result2 = cmd.ExecuteNonQuery();
            }


            transaction.Commit();
            return result2;
        }

        catch (Exception ex)
        {
            log.Error("Inside Student_GL_DataObject- insertJournalEntry catch block ");
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





    public int UpdatePfPath(PublishData j, PublishData[] jd, ArrayList listIndexAgency)
    {

        int result = 0, result1 = 0;
        con = new SqlConnection(str);
        con.Open();
        transaction = con.BeginTransaction();
        try
        {
            cmd = new SqlCommand("UpdatePublicationEntrySub", con, transaction);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@PaublicationID", j.PaublicationID);
            cmd.Parameters.AddWithValue("@TypeOfEntry", j.TypeOfEntry);

            cmd.Parameters.AddWithValue("@MUCategorization", j.MUCategorization);

            cmd.Parameters.AddWithValue("@TitleWorkItem", j.TitleWorkItem);

            cmd.Parameters.AddWithValue("@PubJournalID", j.PublisherOfJournal);
            cmd.Parameters.AddWithValue("@PubJournalName", j.PublisherOfJournalName);
            cmd.Parameters.AddWithValue("@JAVolume", j.JAVolume);
            cmd.Parameters.AddWithValue("@PublishJAMonth", j.PublishJAMonth);

            cmd.Parameters.AddWithValue("@PublishJAYear", j.PublishJAYear);
            string date3 = j.PublishDate.ToString();
            if (date3 != "01/01/0001 00:00:00")
            {
                cmd.Parameters.AddWithValue("@PublicationDate", j.PublishDate);
            }
            else
            {
                cmd.Parameters.AddWithValue("@PublicationDate", DBNull.Value);
            }
            if (j.TypeOfEntry == "JA" || j.TypeOfEntry == "TS" || j.TypeOfEntry == "PR")
            {
                if (j.PageFrom != "")
                {
                    cmd.Parameters.AddWithValue("@PageFrom", j.PageFrom);
                }
                else
                {
                    string pagefrom = "PF" + j.PaublicationID;

                    cmd.Parameters.AddWithValue("@PageFrom", DBNull.Value);
                }

            }
            else
            {
                cmd.Parameters.AddWithValue("@PageFrom", j.PageFrom);
            }

            cmd.Parameters.AddWithValue("@Issue", j.Issue);

            if (j.TypeOfEntry == "JA" || j.TypeOfEntry == "TS" || j.TypeOfEntry == "PR")
            {
                if (j.PageTo != "")
                {
                    cmd.Parameters.AddWithValue("@PageTo", j.PageTo);
                }
                else
                {
                    string pageto = "PT" + j.PaublicationID;

                    cmd.Parameters.AddWithValue("@PageTo", DBNull.Value);
                }
            }
            else
            {
                cmd.Parameters.AddWithValue("@PageTo", j.PageTo);
            }
            if (j.Indexed != null)
            {
                cmd.Parameters.AddWithValue("@Indexed", j.Indexed);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Indexed", DBNull.Value);
            }
            cmd.Parameters.AddWithValue("@Publicationtype", j.Publicationtype);

            if (j.ImpactFactor != "")
            {
                cmd.Parameters.AddWithValue("@ImpactFactor", j.ImpactFactor);
            }
            else
            {
                cmd.Parameters.AddWithValue("@ImpactFactor", DBNull.Value);
            }
            if (j.ImpactFactor5 != "")
            {

                cmd.Parameters.AddWithValue("@fiveImpfact", j.ImpactFactor5);
            }
            else
            {
                cmd.Parameters.AddWithValue("@fiveImpfact", DBNull.Value);
            }
            if (j.IFApplicableYear != 0)
            {
                cmd.Parameters.AddWithValue("@IFApplicableYear", j.IFApplicableYear);
            }
            else
            {
                cmd.Parameters.AddWithValue("@IFApplicableYear", DBNull.Value);
            }

            cmd.Parameters.AddWithValue("@ConferenceTitle", j.ConferenceTitle);

            cmd.Parameters.AddWithValue("@Place", j.Place);
            if (j.CPCity != "" && j.CPCity != null)
            {
                cmd.Parameters.AddWithValue("@CPCity", j.CPCity);
            }
            else
            {
                cmd.Parameters.AddWithValue("@CPCity", DBNull.Value);
            }
            if (j.CPState != "" && j.CPState != null)
            {
                cmd.Parameters.AddWithValue("@CPState", j.CPState);
            }
            else
            {
                cmd.Parameters.AddWithValue("@CPState", DBNull.Value);
            }
            string date1 = j.Date.ToString();
            if (date1 != "01/01/0001 00:00:00")
            {
                cmd.Parameters.AddWithValue("@Date", j.Date);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Date", DBNull.Value);
            }

            string date_new = j.todate.ToString();
            if (date_new != "01/01/0001 00:00:00")
            {
                cmd.Parameters.AddWithValue("@Date1", j.todate);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Date1", DBNull.Value);
            }


            if (j.TitleOfBook != "")
            {
                cmd.Parameters.AddWithValue("@TitleOfBook", j.TitleOfBook);
            }
            else
            {
                cmd.Parameters.AddWithValue("@TitleOfBook", DBNull.Value);
            }
            if (j.TitileOfBookChapter != "")
            {
                cmd.Parameters.AddWithValue("@TitileOfChapter", j.TitileOfBookChapter);
            }
            else
            {
                cmd.Parameters.AddWithValue("@TitileOfChapter", DBNull.Value);
            }
            if (j.Edition != "")
            {
                cmd.Parameters.AddWithValue("@Edition", j.Edition);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Edition", DBNull.Value);
            }

            if (j.Publisher != "")
            {
                cmd.Parameters.AddWithValue("@Publisher", j.Publisher);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Publisher", DBNull.Value);
            }

            if (j.BookPublishYear != "")
            {
                cmd.Parameters.AddWithValue("@BookPublishYear", j.BookPublishYear);
            }

            else
            {
                cmd.Parameters.AddWithValue("@BookPublishYear", DBNull.Value);
            }

            if (j.BookPublishMonth != 0)
            {
                cmd.Parameters.AddWithValue("@BookPublishMonth", j.BookPublishMonth);
            }

            else
            {
                cmd.Parameters.AddWithValue("@BookPublishMonth", DBNull.Value);
            }

            if (j.BookPageNum != "")
            {
                cmd.Parameters.AddWithValue("@BookPublishPageNum", j.BookPageNum);
            }
            else
            {
                cmd.Parameters.AddWithValue("@BookPublishPageNum", DBNull.Value);
            }
            if (j.BookVolume != "")
            {
                cmd.Parameters.AddWithValue("@BookPublishVolume", j.BookVolume);
            }
            else
            {
                cmd.Parameters.AddWithValue("@BookPublishVolume", DBNull.Value);
            }
            if (j.BookSection != "")
            {
                cmd.Parameters.AddWithValue("@BookSection", j.BookSection);
            }
            else
            {
                cmd.Parameters.AddWithValue("@BookSection", DBNull.Value);
            }
            if (j.BookChapter != "")
            {
                cmd.Parameters.AddWithValue("@BookChapter", j.BookChapter);
            }
            else
            {
                cmd.Parameters.AddWithValue("@BookChapter", DBNull.Value);
            }
            if (j.BookCountry != "")
            {
                cmd.Parameters.AddWithValue("@BookCountry", j.BookCountry);
            }
            else
            {
                cmd.Parameters.AddWithValue("@BookCountry", DBNull.Value);
            }
            if (j.BookTypeofPublication != "")
            {
                cmd.Parameters.AddWithValue("@BookTypeofPublication", j.BookTypeofPublication);
            }
            else
            {
                cmd.Parameters.AddWithValue("@BookTypeofPublication", DBNull.Value);
            }
            //if (j.url != "")
            //{

            //    cmd.Parameters.AddWithValue("@URL", j.url);
            //}
            //else
            //{
            //    cmd.Parameters.AddWithValue("@URL", DBNull.Value);
            //}

            cmd.Parameters.AddWithValue("@DOINum", j.DOINum);


            cmd.Parameters.AddWithValue("@Keywords", j.Keywords);
            cmd.Parameters.AddWithValue("@Abstract", j.Abstract);
            // cmd.Parameters.AddWithValue("@Reference", j.TechReferences);  
            if (j.FilePath == null)
            {
                cmd.Parameters.AddWithValue("@UploadPDFPath", DBNull.Value);
            }
            else
            {

                cmd.Parameters.AddWithValue("@UploadPDFPath", j.FilePath);
            }

            cmd.Parameters.AddWithValue("@isERF", j.isERF);
            if (j.uploadEPrint != "")
            {
                cmd.Parameters.AddWithValue("@uploadEPrint", j.uploadEPrint);
            }
            else
            {
                cmd.Parameters.AddWithValue("@uploadEPrint", DBNull.Value);
            }

            if (j.EprintURL != "")
            {
                cmd.Parameters.AddWithValue("@EprintURL", "");
            }
            else
            {
                cmd.Parameters.AddWithValue("@EprintURL", DBNull.Value);
            }
            cmd.Parameters.AddWithValue("@SupervisorID", j.SupervisorID);

            cmd.Parameters.AddWithValue("@LibraryId", j.LibraryId);

            cmd.Parameters.AddWithValue("@AutoApproved", j.AutoAppoval);
            if (j.NewsPublisher != null)
            {
                cmd.Parameters.AddWithValue("@NewsPublisher", j.NewsPublisher);
            }
            else
            {
                cmd.Parameters.AddWithValue("@NewsPublisher", DBNull.Value);
            }
            string date2 = j.NewsPublishedDate.ToString();
            if (date2 != "01/01/0001 00:00:00")
            {

                cmd.Parameters.AddWithValue("@NewsPublishedDate", j.NewsPublishedDate);
            }
            else
            {
                cmd.Parameters.AddWithValue("@NewsPublishedDate", DBNull.Value);
            }

            if (j.NewsPageNum != null)
            {

                cmd.Parameters.AddWithValue("@NewsPageNum", j.NewsPageNum);
            }
            else
            {
                cmd.Parameters.AddWithValue("@NewsPageNum", DBNull.Value);
            }
            cmd.Parameters.AddWithValue("@CreditPoint", j.CreditPoint);
            cmd.Parameters.AddWithValue("@AwardedBy", j.AwardedBy);
            cmd.Parameters.AddWithValue("@TypePresentation", j.TypePresentation);

            if (j.ConfISBN != null)
            {
                cmd.Parameters.AddWithValue("@ISBN", j.ConfISBN);
            }
            else
            {
                cmd.Parameters.AddWithValue("@ISBN", DBNull.Value);
            }
            //cmd.Parameters.AddWithValue("@CitationUrl", j.CitationUrl);
            if (j.IncentivePointSatatus != null)
            {
                cmd.Parameters.AddWithValue("@IncentivePointStatus", j.IncentivePointSatatus);
            }
            else
            {
                cmd.Parameters.AddWithValue("@IncentivePointStatus", DBNull.Value);
            }
            if (j.FundsUtilized != 0.0 && j.FundsUtilized != null)
            {
                cmd.Parameters.AddWithValue("@FundsUtilized", j.FundsUtilized);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FundsUtilized", DBNull.Value);
            }
            if (j.hasProjectreference != null && j.hasProjectreference != "")
            {
                cmd.Parameters.AddWithValue("@hasProjectreference", j.hasProjectreference);
            }
            else
            {
                cmd.Parameters.AddWithValue("@hasProjectreference", DBNull.Value);
            }
            if (j.ProjectIDlist != null && j.ProjectIDlist != "")
            {
                cmd.Parameters.AddWithValue("@ProjectIDlist", j.ProjectIDlist);
            }
            else
            {
                cmd.Parameters.AddWithValue("@ProjectIDlist", DBNull.Value);
            }
            result = cmd.ExecuteNonQuery();
            if (j.hasProjectreference == "Y")
            {
                if (j.ProjectIDlist != "" && j.ProjectIDlist != null)
                {
                    ArrayList ProjectID = new ArrayList();
                    string[] Projectlist = j.ProjectIDlist.Split(',');
                    for (int l = 0; l <= Projectlist.GetUpperBound(0); l++)
                    {
                        ProjectID.Add(Projectlist[l]);
                    }
                    for (int i = 0; i < ProjectID.Count; i++)
                    {
                        cmd = new SqlCommand("Update Project set HasOutcome='Y' where ProjectUnit+ID=@ProjectID", con, transaction);
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@ProjectID", ProjectID[i].ToString());
                        result = cmd.ExecuteNonQuery();
                    }

                }
            }

            if (result >= 1)
            {
                for (int i = 0; i < jd.Length; i++)
                {
                    cmd = new SqlCommand("InsertPublicationAuthor", con, transaction);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@PaublicationID", j.PaublicationID);
                    cmd.Parameters.AddWithValue("@TypeOfEntry", j.TypeOfEntry);

                    cmd.Parameters.AddWithValue("@PublicationLine", i + 1);
                    cmd.Parameters.AddWithValue("@AuthorName", jd[i].AuthorName);
                    cmd.Parameters.AddWithValue("@MUNonMU", jd[i].MUNonMU);
                    cmd.Parameters.AddWithValue("@EmployeeCode", jd[i].EmployeeCode);

                    cmd.Parameters.AddWithValue("@Institution", jd[i].Institution);
                    cmd.Parameters.AddWithValue("@Department", jd[i].Department);

                    cmd.Parameters.AddWithValue("@InstitutionName", jd[i].InstitutionName);
                    cmd.Parameters.AddWithValue("@DepartmentName", jd[i].DepartmentName);
                    cmd.Parameters.AddWithValue("@AuthorType", jd[i].AuthorType);


                    cmd.Parameters.AddWithValue("@isCorrAuth", jd[i].isCorrAuth);
                    cmd.Parameters.AddWithValue("@NameInJournal", jd[i].NameInJournal);
                    cmd.Parameters.AddWithValue("@EmailId", jd[i].EmailId);

                    cmd.Parameters.AddWithValue("@IsPresenter", jd[i].IsPresenter);

                    cmd.Parameters.AddWithValue("@HasAttended", jd[i].HasAttented);
                    cmd.Parameters.AddWithValue("@CreditPoint", jd[i].AuthorCreditPoint);


                    cmd.Parameters.AddWithValue("@NationalInternational", jd[i].NationalInternationl);
                    cmd.Parameters.AddWithValue("@Continent", jd[i].continental);

                    result1 = cmd.ExecuteNonQuery();
                }
            }

            if (listIndexAgency.Count > 0)
            {
                for (int i = 0; i < listIndexAgency.Count; i++)
                {

                    cmd = new SqlCommand("InsertPublicationIndexAgency", con, transaction);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PublicationId", j.PaublicationID);
                    cmd.Parameters.AddWithValue("@Type", j.TypeOfEntry);
                    cmd.Parameters.AddWithValue("@IndexAgency", listIndexAgency[i]);
                    result = cmd.ExecuteNonQuery();

                }
            }

            cmdString = "update Publication set ISStudentAuthor=@IsStudentAuthor,UploadPDFPath=@UploadPDFPath,Status=@Status,AutoApproved=@AutoApproved , ApprovedBy =@ApprovedBy, ApprovedDate=@ApprovedDate where PublicationID=@PublicationID and TypeOfEntry=@TypeOfEntry";

            cmd = new SqlCommand(cmdString, con, transaction);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("@PublicationID", j.PaublicationID);
            cmd.Parameters.AddWithValue("@TypeOfEntry", j.TypeOfEntry);

            cmd.Parameters.AddWithValue("@UploadPDFPath", j.FilePath);

            if (j.AutoAppoval == "Y")
            {
                cmd.Parameters.AddWithValue("@Status", "APP");
                cmd.Parameters.AddWithValue("@ApprovedBy", j.ApprovedBy);
                cmd.Parameters.AddWithValue("@ApprovedDate", DateTime.Now);
            }
            else
            {
                cmd.Parameters.AddWithValue("@ApprovedBy", DBNull.Value);
                cmd.Parameters.AddWithValue("@ApprovedDate", DBNull.Value);
                cmd.Parameters.AddWithValue("@Status", "SUB");
            }
            cmd.Parameters.AddWithValue("@IsStudentAuthor", j.IsStudentAuthor);
            cmd.Parameters.AddWithValue("@AutoApproved", j.AutoAppoval);


            result = cmd.ExecuteNonQuery();
            if (j.TypeOfEntry == "JA")
            {
                if ((Convert.ToInt32(j.PublishJAYear) >= 2018) && (j.PublishJAMonth >= 7))
                {
                    int PublishJAYear = Convert.ToInt32(j.PublishJAYear);
                    int PublishJAMonth = Convert.ToInt32(j.PublishJAMonth);
                    cmdString = "SelectQuartileApplicableYearWise";
                    cmd = new SqlCommand(cmdString, con, transaction);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", j.PublisherOfJournal);
                    cmd.Parameters.AddWithValue("@PublishJAYear", PublishJAYear);
                    cmd.Parameters.AddWithValue("@PublishJAMonth", PublishJAMonth);
                    cmd.Parameters.AddWithValue("@QuartileStartMonth", j.Quartilefrommonth);
                    cmd.Parameters.AddWithValue("@QuartileEndMonth", j.QuartileTomonth);

                    SqlDataReader sdr1 = cmd.ExecuteReader();

                    string Quartile = "NA";

                    while (sdr1.Read())
                    {
                        if (!Convert.IsDBNull(sdr1["Quartile"]))
                        {
                            Quartile = (string)sdr1["Quartile"];
                        }
                        else
                        {
                            Quartile = "NA";
                        }

                    }
                    sdr1.Close();

                    cmdString = "Update Publication set QuartileOnEntry=@Quartile where PublicationID=@PaublicationID and TypeOfEntry=@TypeOfEntry  and MUCategorization=@MUCategorization";

                    cmd = new SqlCommand(cmdString, con, transaction);
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue("@PaublicationID", j.PaublicationID);
                    cmd.Parameters.AddWithValue("@TypeOfEntry", j.TypeOfEntry);
                    cmd.Parameters.AddWithValue("@MUCategorization", j.MUCategorization);
                    cmd.Parameters.AddWithValue("@Quartile", Quartile);


                    result = cmd.ExecuteNonQuery();

                }
                else if ((Convert.ToInt32(j.PublishJAYear) >= 2019) && (j.PublishJAMonth >= 1))
                {
                    int PublishJAYear = Convert.ToInt32(j.PublishJAYear);
                    int PublishJAMonth = Convert.ToInt32(j.PublishJAMonth);
                    cmdString = "SelectQuartileApplicableYearWise";
                    cmd = new SqlCommand(cmdString, con, transaction);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", j.PublisherOfJournal);
                    cmd.Parameters.AddWithValue("@PublishJAYear", PublishJAYear);
                    cmd.Parameters.AddWithValue("@PublishJAMonth", PublishJAMonth);
                    cmd.Parameters.AddWithValue("@QuartileStartMonth", j.Quartilefrommonth);
                    cmd.Parameters.AddWithValue("@QuartileEndMonth", j.QuartileTomonth);

                    SqlDataReader sdr1 = cmd.ExecuteReader();

                    string Quartile = "NA";

                    while (sdr1.Read())
                    {
                        if (!Convert.IsDBNull(sdr1["Quartile"]))
                        {
                            Quartile = (string)sdr1["Quartile"];
                        }
                        else
                        {
                            Quartile = "NA";
                        }

                    }
                    sdr1.Close();

                    cmdString = "Update Publication set QuartileOnEntry=@Quartile where PublicationID=@PaublicationID and TypeOfEntry=@TypeOfEntry  and MUCategorization=@MUCategorization";

                    cmd = new SqlCommand(cmdString, con, transaction);
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue("@PaublicationID", j.PaublicationID);
                    cmd.Parameters.AddWithValue("@TypeOfEntry", j.TypeOfEntry);
                    cmd.Parameters.AddWithValue("@MUCategorization", j.MUCategorization);
                    cmd.Parameters.AddWithValue("@Quartile", Quartile);


                    result = cmd.ExecuteNonQuery();

                }
            }
            cmdString = "Select count(* ) as Count from Publish_ReviewTracker where Type=@Type AND Publish_Id=@Publish_Id ";
            cmd = new SqlCommand(cmdString, con, transaction);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@Type", j.TypeOfEntry);
            cmd.Parameters.AddWithValue("@Publish_Id", j.PaublicationID);

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

            cmd = new SqlCommand("InsertPublicationReviewTracker", con, transaction);
            cmd.CommandType = CommandType.StoredProcedure;



            cmd.Parameters.AddWithValue("@Publish_Id", j.PaublicationID);
            cmd.Parameters.AddWithValue("@Type", j.TypeOfEntry);

            cmd.Parameters.AddWithValue("@ReviewNo", count + 1);
            if (j.AutoAppoval == "Y")
            {
                cmd.Parameters.AddWithValue("@ApprovedStatus", "APP");
            }
            else
            {
                cmd.Parameters.AddWithValue("@ApprovedStatus", "SUB");
            }





            cmd.Parameters.AddWithValue("@Remark", "");

            cmd.Parameters.AddWithValue("@EnterdBy", j.ApprovedBy);
            cmd.Parameters.AddWithValue("@dateM", DateTime.Now);

            result = cmd.ExecuteNonQuery();


            if (j.AutoAppoval == "Y")
            {
                if (j.IsStudentAuthor == "Y")
                {
                    log.Info("Student Publication Approved of = ID " + j.PaublicationID + " and Type " + j.TypeOfEntry + " is approved sucessfully");
                    log.Info("Student Publication details Approved : User Name :" + HttpContext.Current.Session["UserName"] + "Role :" + HttpContext.Current.Session["RoleName"]);
                }
                else
                {
                    log.Info("Faculty Publication Approved of ID " + j.PaublicationID + " and Type " + j.TypeOfEntry + " is approved sucessfully");
                    log.Info("Faculty Publication details Approved : User Name :" + HttpContext.Current.Session["UserName"] + "Role :" + HttpContext.Current.Session["RoleName"]);

                }
            }
            else
            {
                if (j.IsStudentAuthor == "Y")
                {
                    log.Info("Student Publication Submiited  of ID " + j.PaublicationID + " and Type " + j.TypeOfEntry + " is submiited sucessfully");
                    log.Info("Student Publication details Submiited : User Name :" + HttpContext.Current.Session["UserName"] + "Role :" + HttpContext.Current.Session["RoleName"]);

                }
                else
                {
                    log.Info("Faculty Publication of ID " + j.PaublicationID + " and Type " + j.TypeOfEntry + " is submiited sucessfully");
                    log.Info("Faculty Publication details Submiited : User Name :" + HttpContext.Current.Session["UserName"] + "Role :" + HttpContext.Current.Session["RoleName"]);

                }
            }


            transaction.Commit();
            return result;
        }

        catch (Exception ex)
        {
            log.Error("Inside Student_GL_DataObject- insertJournalEntry catch block ");
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


    public int UpdatePublishAcceptReject(PublishData p)
    {

        int result = 0, result1 = 0;
        con = new SqlConnection(str);
        con.Open();
        transaction = con.BeginTransaction();
        try
        {

            cmdString = "update Publication set ISStudentAuthor=@ISStudentAuthor,Status=@Status , ApprovedBy =@ApprovedBy, ApprovedDate=@ApprovedDate,Remarks=@Remarks,SupervisorID=@SupervisorID,IncentivePointStatus=@IncentivePointStatus where PublicationID=@PublicationID and TypeOfEntry=@TypeOfEntry";

            cmd = new SqlCommand(cmdString, con, transaction);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("@PublicationID", p.PaublicationID);
            cmd.Parameters.AddWithValue("@TypeOfEntry", p.TypeOfEntry);

            //cmd.Parameters.AddWithValue("@UploadPDFPath", p.FilePath);


            cmd.Parameters.AddWithValue("@Status", p.Approve);

            if (p.Approve == "APP")
            {
                cmd.Parameters.AddWithValue("@SupervisorID", p.ApprovedBy);
            }
            else
            {
                cmd.Parameters.AddWithValue("@SupervisorID", p.CreatedBy);
            }

            if (p.Approve == "APP")
            {
                cmd.Parameters.AddWithValue("@ApprovedBy", p.ApprovedBy);
                cmd.Parameters.AddWithValue("@ApprovedDate", DateTime.Now);
            }
            else
            {
                cmd.Parameters.AddWithValue("@ApprovedBy", DBNull.Value);
                cmd.Parameters.AddWithValue("@ApprovedDate", DBNull.Value);
            }

            cmd.Parameters.AddWithValue("@Remarks", p.RejectFeedback);
            cmd.Parameters.AddWithValue("@ISStudentAuthor", p.IsStudentAuthor);
            if (p.IncentivePointSatatus != null)
            {
                cmd.Parameters.AddWithValue("@IncentivePointStatus", p.IncentivePointSatatus);
            }
            else
            {
                cmd.Parameters.AddWithValue("@IncentivePointStatus", DBNull.Value);
            }
            result = cmd.ExecuteNonQuery();
            if (p.TypeOfEntry == "JA")
            {
                if ((Convert.ToInt32(p.PublishJAYear) >= 2018) && (p.PublishJAMonth >= 7))
                {
                    cmdString = "select Quartile from Journal_Quartile_Map where JournalId=@PubJournalID and Year=@Year ";
                    cmd = new SqlCommand(cmdString, con, transaction);
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue("@PubJournalID", p.PublisherOfJournal);
                    cmd.Parameters.AddWithValue("@Year", p.PublishJAYear);

                    SqlDataReader sdr = cmd.ExecuteReader();

                    string Quartile = "NA";

                    while (sdr.Read())
                    {
                        if (!Convert.IsDBNull(sdr["Quartile"]))
                        {
                            Quartile = (string)sdr["Quartile"];
                        }
                        else
                        {
                            Quartile = "NA";
                        }

                    }
                    sdr.Close();

                    cmdString = "Update Publication set QuartileOnEntry=@Quartile where PublicationID=@PaublicationID and TypeOfEntry=@TypeOfEntry  and MUCategorization=@MUCategorization";

                    cmd = new SqlCommand(cmdString, con, transaction);
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue("@PaublicationID", p.PaublicationID);
                    cmd.Parameters.AddWithValue("@TypeOfEntry", p.TypeOfEntry);
                    cmd.Parameters.AddWithValue("@MUCategorization", p.MUCategorization);
                    cmd.Parameters.AddWithValue("@Quartile", Quartile);

                    result = cmd.ExecuteNonQuery();

                }
                else
                    if ((Convert.ToInt32(p.PublishJAYear) >= 2019) && (p.PublishJAMonth >= 1))
                    {
                        cmdString = "select Quartile from Journal_Quartile_Map where JournalId=@PubJournalID and Year=@Year ";
                        cmd = new SqlCommand(cmdString, con, transaction);
                        cmd.CommandType = CommandType.Text;

                        cmd.Parameters.AddWithValue("@PubJournalID", p.PublisherOfJournal);
                        cmd.Parameters.AddWithValue("@Year", p.PublishJAYear);

                        SqlDataReader sdr = cmd.ExecuteReader();

                        string Quartile = "NA";

                        while (sdr.Read())
                        {
                            if (!Convert.IsDBNull(sdr["Quartile"]))
                            {
                                Quartile = (string)sdr["Quartile"];
                            }
                            else
                            {
                                Quartile = "NA";
                            }

                        }
                        sdr.Close();

                        cmdString = "Update Publication set QuartileOnEntry=@Quartile where PublicationID=@PaublicationID and TypeOfEntry=@TypeOfEntry  and MUCategorization=@MUCategorization";

                        cmd = new SqlCommand(cmdString, con, transaction);
                        cmd.CommandType = CommandType.Text;

                        cmd.Parameters.AddWithValue("@PaublicationID", p.PaublicationID);
                        cmd.Parameters.AddWithValue("@TypeOfEntry", p.TypeOfEntry);
                        cmd.Parameters.AddWithValue("@MUCategorization", p.MUCategorization);
                        cmd.Parameters.AddWithValue("@Quartile", Quartile);

                        result = cmd.ExecuteNonQuery();

                    }


            }

            cmdString = "Select count(* ) as Count from Publish_ReviewTracker where Type=@Type AND Publish_Id=@Publish_Id ";
            cmd = new SqlCommand(cmdString, con, transaction);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@Type", p.TypeOfEntry);
            cmd.Parameters.AddWithValue("@Publish_Id", p.PaublicationID);

            SqlDataReader sdr1 = cmd.ExecuteReader();

            int count = 0;

            while (sdr1.Read())
            {
                if (!Convert.IsDBNull(sdr1["Count"]))
                {
                    count = (int)sdr1["Count"];
                }

            }
            sdr1.Close();

            cmd = new SqlCommand("InsertPublicationReviewTracker", con, transaction);
            cmd.CommandType = CommandType.StoredProcedure;



            cmd.Parameters.AddWithValue("@Publish_Id", p.PaublicationID);
            cmd.Parameters.AddWithValue("@Type", p.TypeOfEntry);

            cmd.Parameters.AddWithValue("@ReviewNo", count + 1);

            cmd.Parameters.AddWithValue("@ApprovedStatus", p.Approve);






            cmd.Parameters.AddWithValue("@Remark", p.RejectFeedback);

            cmd.Parameters.AddWithValue("@EnterdBy", p.ApprovedBy);
            cmd.Parameters.AddWithValue("@dateM", DateTime.Now);

            result1 = cmd.ExecuteNonQuery();


            if (p.Approve == "APP")
            {
                if (p.IsStudentAuthor == "Y")
                {
                    log.Info("Student Publication of ID " + p.PaublicationID + " and Type " + p.TypeOfEntry + " is approved sucessfully");
                    log.Info("Publication Approval : User Name :" + HttpContext.Current.Session["UserName"] + "Role :" + HttpContext.Current.Session["RoleName"]);

                }
                else
                {
                    log.Info("Faculty Publication of ID " + p.PaublicationID + " and Type " + p.TypeOfEntry + " is approved sucessfully");
                    log.Info("Publication Reowrk : User Name :" + HttpContext.Current.Session["UserName"] + "Role :" + HttpContext.Current.Session["RoleName"]);

                }
            }


            else
            {
                log.Info(" Publication of ID " + p.PaublicationID + " and Type " + p.TypeOfEntry + " is Rejected");
            }



            transaction.Commit();
            return result1;
        }

        catch (Exception ex)
        {
            log.Error("Inside Student_GL_DataObject- insertJournalEntry catch block ");
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
    public int UpdatePfPathCreate(PublishData p)
    {

        int result = 0, result1 = 0;
        con = new SqlConnection(str);
        con.Open();
        transaction = con.BeginTransaction();
        try
        {

            cmdString = "update Publication set UploadPDFPath=@UploadPDFPath,FileUploadRemarks=@FileUploadRemarks where PublicationID=@PublicationID and TypeOfEntry=@TypeOfEntry";

            cmd = new SqlCommand(cmdString, con, transaction);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("@PublicationID", p.PaublicationID);
            cmd.Parameters.AddWithValue("@TypeOfEntry", p.TypeOfEntry);

            cmd.Parameters.AddWithValue("@UploadPDFPath", p.FilePath);
            if (p.RemarksFeedback != null)
            {
                cmd.Parameters.AddWithValue("@FileUploadRemarks", p.RemarksFeedback);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FileUploadRemarks", DBNull.Value);
            }

            // cmd.Parameters.AddWithValue("@Status", "NEW");




            result = cmd.ExecuteNonQuery();










            transaction.Commit();
            return result;
        }

        catch (Exception ex)
        {
            log.Error("Inside Student_GL_DataObject- insertJournalEntry catch block ");
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



    public PublishData fnfindjid(string jid, string bu)
    {
        log.Debug("Inside AP_GL_DataObject- fnfindjid function, journalID: " + jid + "busUnit: " + bu);
        try
        {
            //cmdString = "select publicationID,TypeOfEntry,MUCategorization,TitleWorkItem,PubJournalID,JAVolume,PublishJAMonth, "
            //        + " PublishJAYear,PageFrom,PageTo,Indexed,Publicationtype,ImpactFactor,ConferenceTitle,Place, "
            //        + " Date,TitleOfBook,TitileOfChapter,Edition,Publisher,BookPublishYear,BookPublishPageNum,URL, "
            //        + " NewsPublisher,NewsPublishedDate,NewsPageNum,DOINum,Keywords,Abstract,Reference,UploadPDFPath, "
            //        + " Status,AutoApproved,isERF,uploadEPrint,EprintURL,SupervisorID,LibraryId,CreatedBy,CreatedDate, "
            //        + " ApprovedBy,ApprovedDate,CancelledBy,cancelledDate,Remarks,Journal_M.Title from Publication, Journal_M where "
            //        + " Journal_M.Id =Publication.PubJournalID and PublicationID=@PublicationID and TypeOfEntry=@TypeOfEntry ";
            cmdString = "select * from Publication where PublicationID=@PublicationID and TypeOfEntry=@TypeOfEntry ";

            // cmdString = "select BusinessUnit,JournalID, JournalDate, LineNarration,LongNarration, EntryStatus from GL where JournalID=@JournalID and BusinessUnit=@BusinessUnit";
            con = new SqlConnection(str);
            con.Open();
            cmd = new SqlCommand(cmdString, con);
            cmd.Parameters.Add("@PublicationID", SqlDbType.VarChar, 15);
            cmd.Parameters["@PublicationID"].Value = jid;
            cmd.Parameters.Add("@TypeOfEntry", SqlDbType.VarChar, 12);
            cmd.Parameters["@TypeOfEntry"].Value = bu;
            // cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandType = CommandType.Text;
            SqlDataReader sdr = cmd.ExecuteReader();
            // voucher p = new voucher();
            PublishData V = new PublishData();

            while (sdr.Read())
            {
                if (!Convert.IsDBNull(sdr["MUCategorization"]))
                {
                    V.MUCategorization = (string)sdr["MUCategorization"];
                }
                else if (Convert.IsDBNull(sdr["MUCategorization"]))
                {
                    V.MUCategorization = "";
                }
                if (!Convert.IsDBNull(sdr["TitleWorkItem"]))
                {
                    V.TitleWorkItem = (string)sdr["TitleWorkItem"];
                }
                else if (Convert.IsDBNull(sdr["TitleWorkItem"]))
                {
                    V.TitleWorkItem = "";
                }

                if (!Convert.IsDBNull(sdr["PubJournalID"]))
                {
                    V.PublisherOfJournal = (string)sdr["PubJournalID"];
                }
                else if (Convert.IsDBNull(sdr["PubJournalID"]))
                {
                    V.PublisherOfJournal = "";
                }


                if (!Convert.IsDBNull(sdr["JAVolume"]))
                {
                    V.JAVolume = (string)sdr["JAVolume"];
                }
                else if (Convert.IsDBNull(sdr["JAVolume"]))
                {
                    V.JAVolume = "";
                }

                if (!Convert.IsDBNull(sdr["PublishJAMonth"]))
                {
                    V.PublishJAMonth = (int)sdr["PublishJAMonth"];
                }
                else if (Convert.IsDBNull(sdr["PublishJAMonth"]))
                {
                    V.PublishJAMonth = 0;
                }


                if (!Convert.IsDBNull(sdr["PublishJAYear"]))
                {
                    V.PublishJAYear = (string)sdr["PublishJAYear"];
                }
                else if (Convert.IsDBNull(sdr["PublishJAYear"]))
                {
                    V.PublishJAYear = "";
                }
                if (!Convert.IsDBNull(sdr["PageFrom"]))
                {
                    V.PageFrom = (string)sdr["PageFrom"];
                }
                else if (Convert.IsDBNull(sdr["PageFrom"]))
                {
                    V.PageFrom = "";
                }
                if (!Convert.IsDBNull(sdr["PageTo"]))
                {
                    V.PageTo = (string)sdr["PageTo"];
                }
                else if (Convert.IsDBNull(sdr["PageTo"]))
                {
                    V.PageTo = "";
                }

                if (!Convert.IsDBNull(sdr["Indexed"]))
                {
                    V.Indexed = (string)sdr["Indexed"];
                }
                else if (Convert.IsDBNull(sdr["Indexed"]))
                {
                    V.Indexed = "";
                }
                if (!Convert.IsDBNull(sdr["Publicationtype"]))
                {
                    V.Publicationtype = (string)sdr["Publicationtype"];
                }
                else if (Convert.IsDBNull(sdr["Publicationtype"]))
                {
                    V.Publicationtype = "";
                }

                if (!Convert.IsDBNull(sdr["ImpactFactor"]))
                {
                    V.ImpactFactor = (string)sdr["ImpactFactor"];
                }
                else if (Convert.IsDBNull(sdr["ImpactFactor"]))
                {
                    V.ImpactFactor = "";
                }

                if (!Convert.IsDBNull(sdr["FiveImpFact"]))
                {
                    V.ImpactFactor5 = (string)sdr["FiveImpFact"];
                }
                else if (Convert.IsDBNull(sdr["FiveImpFact"]))
                {
                    V.ImpactFactor5 = "";
                }

                if (!Convert.IsDBNull(sdr["IF_ApplicableYear"]))
                {
                    V.IFApplicableYear = (int)sdr["IF_ApplicableYear"];
                }
                else if (Convert.IsDBNull(sdr["IF_ApplicableYear"]))
                {
                    V.IFApplicableYear = 0;
                }

                if (!Convert.IsDBNull(sdr["ConferenceTitle"]))
                {
                    V.ConferenceTitle = (string)sdr["ConferenceTitle"];
                }
                else if (Convert.IsDBNull(sdr["ConferenceTitle"]))
                {
                    V.ConferenceTitle = "";
                }
                if (!Convert.IsDBNull(sdr["Place"]))
                {
                    V.Place = (string)sdr["Place"];
                }
                else if (Convert.IsDBNull(sdr["Place"]))
                {
                    V.Place = "";
                }
                if (!Convert.IsDBNull(sdr["City"]))
                {
                    V.CPCity = (string)sdr["City"];
                }
                else if (Convert.IsDBNull(sdr["City"]))
                {
                    V.CPCity = "";
                }
                if (!Convert.IsDBNull(sdr["State"]))
                {
                    V.CPState = (string)sdr["State"];
                }
                else if (Convert.IsDBNull(sdr["State"]))
                {
                    V.CPState = "";
                }
                if (!Convert.IsDBNull(sdr["Date"]))
                {
                    V.Date = (DateTime)sdr["Date"];
                }

                else if (Convert.IsDBNull(sdr["Date"]))
                {

                }

                if (!Convert.IsDBNull(sdr["ToDate"]))
                {
                    V.todate = (DateTime)sdr["ToDate"];
                }

                else if (Convert.IsDBNull(sdr["ToDate"]))
                {

                }
                if (!Convert.IsDBNull(sdr["TitleOfBook"]))
                {
                    V.TitleOfBook = (string)sdr["TitleOfBook"];
                }
                else if (Convert.IsDBNull(sdr["TitleOfBook"]))
                {
                    V.TitleOfBook = "";
                }
                if (!Convert.IsDBNull(sdr["TitileOfChapter"]))
                {
                    V.TitileOfChapter = (string)sdr["TitileOfChapter"];
                }
                else if (Convert.IsDBNull(sdr["TitileOfChapter"]))
                {
                    V.TitileOfChapter = "";
                }
                if (!Convert.IsDBNull(sdr["Edition"]))
                {
                    V.Edition = (string)sdr["Edition"];
                }
                else if (Convert.IsDBNull(sdr["Edition"]))
                {
                    V.Edition = "";
                }
                if (!Convert.IsDBNull(sdr["Publisher"]))
                {
                    V.Publisher = (string)sdr["Publisher"];
                }
                else if (Convert.IsDBNull(sdr["Publisher"]))
                {
                    V.Publisher = "";
                }
                if (!Convert.IsDBNull(sdr["BookPublishYear"]))
                {
                    V.BookPublishYear = (string)sdr["BookPublishYear"];
                }
                else if (Convert.IsDBNull(sdr["BookPublishYear"]))
                {
                    V.BookPublishYear = "";
                }

                if (!Convert.IsDBNull(sdr["BookPublishMonth"]))
                {
                    V.BookPublishMonth = (int)sdr["BookPublishMonth"];
                }
                else if (Convert.IsDBNull(sdr["BookPublishMonth"]))
                {
                    V.BookPublishMonth = 0;
                }
                if (!Convert.IsDBNull(sdr["BookPublishPageNum"]))
                {
                    V.BookPageNum = (string)sdr["BookPublishPageNum"];
                }
                else if (Convert.IsDBNull(sdr["BookPublishPageNum"]))
                {
                    V.BookPageNum = "";
                }


                if (!Convert.IsDBNull(sdr["BookVolume"]))
                {
                    V.BookVolume = (string)sdr["BookVolume"];
                }
                else if (Convert.IsDBNull(sdr["BookVolume"]))
                {
                    V.BookVolume = "";
                }
                if (!Convert.IsDBNull(sdr["BkPublicationType"]))
                {
                    V.BookTypeofPublication = (string)sdr["BkPublicationType"];
                }
                else if (Convert.IsDBNull(sdr["BkPublicationType"]))
                {
                    V.BookTypeofPublication = "0";
                }
                 if (!Convert.IsDBNull(sdr["Section"]))
                {
                    V.BookSection = (string)sdr["Section"];
                }
                else if (Convert.IsDBNull(sdr["Section"]))
                {
                    V.BookSection = "";
                }
                 if (!Convert.IsDBNull(sdr["Chapter"]))
                {
                    V.BookChapter = (string)sdr["Chapter"];
                }
                else if (Convert.IsDBNull(sdr["Chapter"]))
                {
                    V.BookChapter = "";
                }
                 if (!Convert.IsDBNull(sdr["Country"]))
                {
                    V.BookCountry = (string)sdr["Country"];
                }
                else if (Convert.IsDBNull(sdr["Country"]))
                {
                    V.BookCountry = "";
                }
                if (!Convert.IsDBNull(sdr["URL"]))
                {
                    V.url = (string)sdr["URL"];
                }
                else if (Convert.IsDBNull(sdr["URL"]))
                {
                    V.url = "";
                }

                if (!Convert.IsDBNull(sdr["NewsPublisher"]))
                {
                    V.NewsPublisher = (string)sdr["NewsPublisher"];
                }
                else if (Convert.IsDBNull(sdr["NewsPublisher"]))
                {
                    V.NewsPublisher = "";
                }

                if (!Convert.IsDBNull(sdr["NewsPublishedDate"]))
                {
                    V.NewsPublishedDate = (DateTime)sdr["NewsPublishedDate"];
                }
                else
                {
                }
                //else if (Convert.IsDBNull(sdr["NewsPublishedDate"]))
                //{
                //    V.NewsPublishedDate = "";
                //}

                if (!Convert.IsDBNull(sdr["NewsPageNum"]))
                {
                    V.NewsPageNum = (string)sdr["NewsPageNum"];
                }
                else if (Convert.IsDBNull(sdr["NewsPageNum"]))
                {
                    V.NewsPageNum = "";
                }

                if (!Convert.IsDBNull(sdr["DOINum"]))
                {
                    V.DOINum = (string)sdr["DOINum"];
                }
                else if (Convert.IsDBNull(sdr["DOINum"]))
                {
                    V.DOINum = "";
                }

                if (!Convert.IsDBNull(sdr["Keywords"]))
                {
                    V.Keywords = (string)sdr["Keywords"];
                }
                else if (Convert.IsDBNull(sdr["Keywords"]))
                {
                    V.Keywords = "";
                }



                if (!Convert.IsDBNull(sdr["Abstract"]))
                {
                    V.Abstract = (string)sdr["Abstract"];
                }
                else if (Convert.IsDBNull(sdr["Abstract"]))
                {
                    V.Abstract = "";
                }

                if (!Convert.IsDBNull(sdr["FundsUtilized"]))
                {
                    V.FundsUtilized = Convert.ToDouble((decimal)sdr["FundsUtilized"]);
                }
                else if (Convert.IsDBNull(sdr["FundsUtilized"]))
                {
                    V.FundsUtilized = 0;
                }


                //if (!Convert.IsDBNull(sdr["Reference"]))
                //{
                //    V.TechReferences = (string)sdr["Reference"];
                //}
                //else if (Convert.IsDBNull(sdr["Reference"]))
                //{
                //    V.TechReferences = "";
                //}


                if (!Convert.IsDBNull(sdr["UploadPDFPath"]))
                {
                    V.UploadPDF = (string)sdr["UploadPDFPath"];
                }
                else if (Convert.IsDBNull(sdr["UploadPDFPath"]))
                {
                    V.UploadPDF = "";
                }



                if (!Convert.IsDBNull(sdr["Status"]))
                {
                    V.Status = (string)sdr["Status"];
                }
                else if (Convert.IsDBNull(sdr["Status"]))
                {
                    V.Status = "";
                }


                if (!Convert.IsDBNull(sdr["isERF"]))
                {
                    V.isERF = (string)sdr["isERF"];
                }
                else if (Convert.IsDBNull(sdr["isERF"]))
                {
                    V.isERF = "";
                }

                if (!Convert.IsDBNull(sdr["uploadEPrint"]))
                {
                    V.uploadEPrint = (string)sdr["uploadEPrint"];
                }
                else if (Convert.IsDBNull(sdr["uploadEPrint"]))
                {
                    V.uploadEPrint = "";
                }

                if (!Convert.IsDBNull(sdr["EprintURL"]))
                {
                    V.EprintURL = (string)sdr["EprintURL"];
                }
                else if (Convert.IsDBNull(sdr["EprintURL"]))
                {
                    V.EprintURL = "";
                }

                if (!Convert.IsDBNull(sdr["SupervisorID"]))
                {
                    V.SupervisorID = (string)sdr["SupervisorID"];
                }
                else if (Convert.IsDBNull(sdr["SupervisorID"]))
                {
                    V.SupervisorID = "";
                }
                if (!Convert.IsDBNull(sdr["LibraryId"]))
                {
                    V.LibraryId = (string)sdr["LibraryId"];
                }
                else if (Convert.IsDBNull(sdr["LibraryId"]))
                {
                    V.LibraryId = "";
                }
                if (!Convert.IsDBNull(sdr["Issue"]))
                {
                    V.Issue = (string)sdr["Issue"];
                }
                else if (Convert.IsDBNull(sdr["Issue"]))
                {
                    V.Issue = "";
                }

                if (!Convert.IsDBNull(sdr["Remarks"]))
                {
                    V.RemarksFeedback = (string)sdr["Remarks"];
                }
                else if (Convert.IsDBNull(sdr["Remarks"]))
                {
                    V.RemarksFeedback = "";
                }
                if (!Convert.IsDBNull(sdr["TypePresentation"]))
                {
                    V.TypePresentation = (string)sdr["TypePresentation"];
                }
                else if (Convert.IsDBNull(sdr["TypePresentation"]))
                {
                    V.TypePresentation = "";
                }

                if (!Convert.IsDBNull(sdr["CreditPoint"]))
                {
                    V.CreditPoint = (int)sdr["CreditPoint"];
                }
                else if (Convert.IsDBNull(sdr["CreditPoint"]))
                {
                    V.CreditPoint = 0;
                }

                if (!Convert.IsDBNull(sdr["AwardedBy"]))
                {
                    V.AwardedBy = (string)sdr["AwardedBy"];
                }
                else if (Convert.IsDBNull(sdr["AwardedBy"]))
                {
                    V.AwardedBy = "";
                }


                if (!Convert.IsDBNull(sdr["ISBN"]))
                {
                    V.ConfISBN = (string)sdr["ISBN"];
                }
                else if (Convert.IsDBNull(sdr["ISBN"]))
                {
                    V.ConfISBN = "";
                }

                if (!Convert.IsDBNull(sdr["CancelRemarks"]))
                {
                    V.PubCancelRemarks = (string)sdr["CancelRemarks"];
                }
                else if (Convert.IsDBNull(sdr["CancelRemarks"]))
                {
                    V.PubCancelRemarks = "";
                }
                if (!Convert.IsDBNull(sdr["PubJournalName"]))
                {
                    V.PublisherOfJournalName = (string)sdr["PubJournalName"];
                }
                else if (Convert.IsDBNull(sdr["PubJournalName"]))
                {
                    V.PublisherOfJournalName = "";
                }


                if (!Convert.IsDBNull(sdr["CitationURL"]))
                {
                    V.CitationUrl = (string)sdr["CitationURL"];
                }
                else if (Convert.IsDBNull(sdr["CitationURL"]))
                {
                    V.CitationUrl = "";
                }
                if (!Convert.IsDBNull(sdr["ISStudentAuthor"]))
                {
                    V.IsStudentAuthor = (string)sdr["ISStudentAuthor"];
                }

                if (!Convert.IsDBNull(sdr["QuartileOnEntry"]))
                {
                    V.QuartileOnEntry = (string)sdr["QuartileOnEntry"];
                }
                else if (Convert.IsDBNull(sdr["QuartileOnEntry"]))
                {
                    V.QuartileOnEntry = "";
                }
                if (!Convert.IsDBNull(sdr["QuartileOnIncentivize"]))
                {
                    V.QuartileOnIncentivize = (string)sdr["QuartileOnIncentivize"];
                }
                else if (Convert.IsDBNull(sdr["QuartileOnIncentivize"]))
                {
                    V.QuartileOnIncentivize = "";
                }
                if (!Convert.IsDBNull(sdr["IncentivePointStatus"]))
                {
                    V.IncentivePointSatatus = (string)sdr["IncentivePointStatus"];
                }
                else if (Convert.IsDBNull(sdr["IncentivePointStatus"]))
                {
                    V.IncentivePointSatatus = "";
                }
                if (!Convert.IsDBNull(sdr["HasProjectRefference"]))
                {
                    V.hasProjectreference = (string)sdr["HasProjectRefference"];
                }
                else if (Convert.IsDBNull(sdr["HasProjectRefference"]))
                {
                    V.hasProjectreference = "0";
                }
                if (!Convert.IsDBNull(sdr["ProjectIDlist"]))
                {
                    V.ProjectIDlist = (string)sdr["ProjectIDlist"];
                }
                else if (Convert.IsDBNull(sdr["ProjectIDlist"]))
                {
                    V.ProjectIDlist = "";
                }
            }
            return V;
        }
        catch (Exception ex)
        {
            log.Error("Inside AP_GL_DataObject- fnfindjid catch block ");
            log.Error(ex.Message);
            log.Error(ex.StackTrace);
            throw ex;
        }

        finally
        {
            con.Close();
        }
    }



    public String fnfindjidgtjname(string jid, string bu)
    {
        log.Debug("Inside AP_GL_DataObject- fnfindjid function, journalID: " + jid + "busUnit: " + bu);
        try
        {
            cmdString = "select Journal_M.Title from Publication, Journal_M where "
                    + " Journal_M.Id =Publication.PubJournalID and PublicationID=@PublicationID and TypeOfEntry=@TypeOfEntry ";
            //cmdString = "select * from Publication where PublicationID=@PublicationID and TypeOfEntry=@TypeOfEntry ";

            // cmdString = "select BusinessUnit,JournalID, JournalDate, LineNarration,LongNarration, EntryStatus from GL where JournalID=@JournalID and BusinessUnit=@BusinessUnit";
            con = new SqlConnection(str);
            con.Open();
            cmd = new SqlCommand(cmdString, con);
            cmd.Parameters.Add("@PublicationID", SqlDbType.VarChar, 15);
            cmd.Parameters["@PublicationID"].Value = jid;
            cmd.Parameters.Add("@TypeOfEntry", SqlDbType.VarChar, 12);
            cmd.Parameters["@TypeOfEntry"].Value = bu;
            // cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandType = CommandType.Text;
            SqlDataReader sdr = cmd.ExecuteReader();
            // voucher p = new voucher();
            PublishData V = new PublishData();

            string jname = "";
            while (sdr.Read())
            {


                if (!Convert.IsDBNull(sdr["Title"]))
                {
                    jname = (string)sdr["Title"];
                }
                else if (Convert.IsDBNull(sdr["Title"]))
                {
                    jname = "";
                }


            }
            return jname;
        }
        catch (Exception ex)
        {
            log.Error("Inside AP_GL_DataObject- fnfindjid catch block ");
            log.Error(ex.Message);
            log.Error(ex.StackTrace);
            throw ex;
        }

        finally
        {
            con.Close();
        }
    }
    public String GetFileUploadPath(string pubid, string entrytype)
    {

        try
        {
            cmdString = "select * from Publication  where PublicationID=@PublicationID and TypeOfEntry=@TypeOfEntry ";


            // cmdString = "select BusinessUnit,JournalID, JournalDate, LineNarration,LongNarration, EntryStatus from GL where JournalID=@JournalID and BusinessUnit=@BusinessUnit";
            con = new SqlConnection(str);
            con.Open();
            cmd = new SqlCommand(cmdString, con);
            cmd.Parameters.Add("@PublicationID", SqlDbType.VarChar, 15);
            cmd.Parameters["@PublicationID"].Value = pubid;
            cmd.Parameters.Add("@TypeOfEntry", SqlDbType.VarChar, 12);
            cmd.Parameters["@TypeOfEntry"].Value = entrytype;
            // cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandType = CommandType.Text;
            SqlDataReader sdr = cmd.ExecuteReader();
            // voucher p = new voucher();
            string fileuploadP = null;

            while (sdr.Read())
            {

                if (!Convert.IsDBNull(sdr["UploadPDFPath"]))
                {
                    fileuploadP = (string)sdr["UploadPDFPath"];
                }
                else if (Convert.IsDBNull(sdr["UploadPDFPath"]))
                {
                    fileuploadP = "";
                }





            }
            return fileuploadP;
        }
        catch (Exception ex)
        {
            log.Error("Inside AP_GL_DataObject- fnfindjid catch block ");
            log.Error(ex.Message);
            log.Error(ex.StackTrace);
            throw ex;
        }

        finally
        {
            con.Close();
        }
    }


    public String GetPublishRejectOwner(string pubid, string entrytype)
    {

        try
        {
            cmdString = "select * from Publication  where PublicationID=@PublicationID and TypeOfEntry=@TypeOfEntry ";


            // cmdString = "select BusinessUnit,JournalID, JournalDate, LineNarration,LongNarration, EntryStatus from GL where JournalID=@JournalID and BusinessUnit=@BusinessUnit";
            con = new SqlConnection(str);
            con.Open();
            cmd = new SqlCommand(cmdString, con);
            cmd.Parameters.Add("@PublicationID", SqlDbType.VarChar, 15);
            cmd.Parameters["@PublicationID"].Value = pubid;
            cmd.Parameters.Add("@TypeOfEntry", SqlDbType.VarChar, 12);
            cmd.Parameters["@TypeOfEntry"].Value = entrytype;
            // cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandType = CommandType.Text;
            SqlDataReader sdr = cmd.ExecuteReader();
            // voucher p = new voucher();
            string CreatedBy = null;

            while (sdr.Read())
            {

                if (!Convert.IsDBNull(sdr["CreatedBy"]))
                {
                    CreatedBy = (string)sdr["CreatedBy"];
                }
                else if (Convert.IsDBNull(sdr["CreatedBy"]))
                {
                    CreatedBy = "";
                }





            }
            return CreatedBy;
        }
        catch (Exception ex)
        {
            log.Error("Inside AP_GL_DataObject- fnfindjid catch block ");
            log.Error(ex.Message);
            log.Error(ex.StackTrace);
            throw ex;
        }

        finally
        {
            con.Close();
        }
    }

    public String GetAuthorsSupervisor(string authors)
    {

        try
        {
            cmdString = "select SupervisorId from User_M where EmailId=@EmailId ";


            // cmdString = "select BusinessUnit,JournalID, JournalDate, LineNarration,LongNarration, EntryStatus from GL where JournalID=@JournalID and BusinessUnit=@BusinessUnit";
            con = new SqlConnection(str);
            con.Open();
            cmd = new SqlCommand(cmdString, con);
            cmd.Parameters.Add("@EmailId", SqlDbType.VarChar, 25);
            cmd.Parameters["@EmailId"].Value = authors;

            // cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandType = CommandType.Text;
            SqlDataReader sdr = cmd.ExecuteReader();
            // voucher p = new voucher();
            string SupervisorId = null;

            while (sdr.Read())
            {

                if (!Convert.IsDBNull(sdr["SupervisorId"]))
                {
                    SupervisorId = (string)sdr["SupervisorId"];
                }
                else if (Convert.IsDBNull(sdr["SupervisorId"]))
                {
                    SupervisorId = "";
                }





            }
            return SupervisorId;
        }
        catch (Exception ex)
        {
            log.Error("Inside AP_GL_DataObject- fnfindjid catch block ");
            log.Error(ex.Message);
            log.Error(ex.StackTrace);
            throw ex;
        }

        finally
        {
            con.Close();
        }
    }

    public String GetAuthorsSupervisorgetMail(string authors)
    {

        try
        {
            cmdString = "select EmailId from User_M where User_Id=@EmailId ";


            // cmdString = "select BusinessUnit,JournalID, JournalDate, LineNarration,LongNarration, EntryStatus from GL where JournalID=@JournalID and BusinessUnit=@BusinessUnit";
            con = new SqlConnection(str);
            con.Open();
            cmd = new SqlCommand(cmdString, con);
            if (authors != null)
            {
                cmd.Parameters.Add("@EmailId", SqlDbType.VarChar, 25);
                cmd.Parameters["@EmailId"].Value = authors;
            }
            else
            {

                cmd.Parameters.Add("@EmailId", SqlDbType.VarChar, 25);
                cmd.Parameters["@EmailId"].Value = "";

            }

            // cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandType = CommandType.Text;
            SqlDataReader sdr = cmd.ExecuteReader();
            // voucher p = new voucher();
            string EmailId = null;

            while (sdr.Read())
            {

                if (!Convert.IsDBNull(sdr["EmailId"]))
                {
                    EmailId = (string)sdr["EmailId"];
                }
                else if (Convert.IsDBNull(sdr["EmailId"]))
                {
                    EmailId = "";
                }





            }
            return EmailId;
        }
        catch (Exception ex)
        {
            log.Error("Inside AP_GL_DataObject- fnfindjid catch block ");
            log.Error(ex.Message);
            log.Error(ex.StackTrace);
            throw ex;
        }

        finally
        {
            con.Close();
        }
    }




    public DataTable fnfindjournalAccount(string jid, string bu)
    {
        log.Debug("Inside AP_GL_DataObject- fnfindjournalAccount function, journalID: " + jid + "busUnit: " + bu);

        con = new SqlConnection(str);
        con.Open();
        try
        {
            //  cmdString = "select  convert(numeric(13,2),Amount) as Amount ,DebitCredit as DrCr, a.account as account, a.oprunit as oprUnit,a.DeptID as dept,a.AffilateBU as affiliate,openitem as openItem, a.linenarration as lineNar, ADesc as accountName, OprUnit_M.OUnitName as oprUnitName,Department_M.DeptName,OpenItem_M.EmpName,a.JournalLine,EmpName as openItemName from GL_Accounting_T a  LEFT OUTER JOIN OprUnit_M ON a.OPRUNIT = OprUnit_M.OPRUNIT LEFT OUTER JOIN Department_M ON a.DeptID = Department_M.DeptCode LEFT OUTER JOIN OpenItem_M ON a.openitem = OpenItem_M.EmpCode, GL d , Account_M where a.JournalID=d.JournalID  and Account_M.Account= a.Account and a.BusinessUnit=d.BusinessUnit and a.JournalID=@JournalID and a.BusinessUnit=@BusinessUnit order by a.JournalLine";
            SqlDataAdapter da;
            DataTable ds;


            cmdString = " select MUNonMU  as DropdownMuNonMu,EmployeeCode,AuthorName,Institution,Department, DepartmentName,InstitutionName ,  EmailId as MailId,AuthorType,isCorrAuth,NameInJournal ,IsPresenter,HasAttended as HasAttented,CreditPoint,NationalInternational as NationalType,Continent as ContinentId,PublicationLine  from Publishcation_Author where PaublicationID=@PaublicationID and TypeOfEntry=@TypeOfEntry";
            cmd = new SqlCommand(cmdString, con);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add("@PaublicationID", SqlDbType.VarChar, 15);
            cmd.Parameters["@PaublicationID"].Value = jid;

            cmd.Parameters.Add("@TypeOfEntry", SqlDbType.VarChar, 5);
            cmd.Parameters["@TypeOfEntry"].Value = bu;
            da = new SqlDataAdapter(cmd);

            ds = new DataTable();
            da.Fill(ds);

            return ds;
        }

        catch (Exception ex)
        {
            log.Error("Inside AP_GL_DataObject- fnfindjournalAccount catch block ");
            log.Error(ex.Message);
            log.Error(ex.StackTrace);
            throw ex;
        }

        finally
        {
            con.Close();
        }
    }


    public DataSet fnfindjournalAccount1(string pid, string typeentry)
    {


        con = new SqlConnection(str);
        con.Open();
        try
        {
            //  cmdString = "select  convert(numeric(13,2),Amount) as Amount ,DebitCredit as DrCr, a.account as account, a.oprunit as oprUnit,a.DeptID as dept,a.AffilateBU as affiliate,openitem as openItem, a.linenarration as lineNar, ADesc as accountName, OprUnit_M.OUnitName as oprUnitName,Department_M.DeptName,OpenItem_M.EmpName,a.JournalLine,EmpName as openItemName from GL_Accounting_T a  LEFT OUTER JOIN OprUnit_M ON a.OPRUNIT = OprUnit_M.OPRUNIT LEFT OUTER JOIN Department_M ON a.DeptID = Department_M.DeptCode LEFT OUTER JOIN OpenItem_M ON a.openitem = OpenItem_M.EmpCode, GL d , Account_M where a.JournalID=d.JournalID  and Account_M.Account= a.Account and a.BusinessUnit=d.BusinessUnit and a.JournalID=@JournalID and a.BusinessUnit=@BusinessUnit order by a.JournalLine";
            SqlDataAdapter da;
            DataSet ds;


            cmdString = " select PublicationId,Type,IndexAgency as agencyid from Publication_IndexedDetails where PublicationId=@PublicationId and Type=@Type ";
            cmd = new SqlCommand(cmdString, con);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add("@PublicationId", SqlDbType.VarChar, 15);
            cmd.Parameters["@PublicationId"].Value = pid;

            cmd.Parameters.Add("@Type", SqlDbType.VarChar, 5);
            cmd.Parameters["@Type"].Value = typeentry;
            da = new SqlDataAdapter(cmd);

            ds = new DataSet();
            da.Fill(ds);

            return ds;
        }

        catch (Exception ex)
        {
            log.Error("Inside AP_GL_DataObject- fnfindjournal catch block ");
            log.Error(ex.Message);
            log.Error(ex.StackTrace);
            throw ex;
        }

        finally
        {
            con.Close();
        }
    }

    public DataSet getAuthorList(string id, string Type)
    {
        log.Debug("Inside function getAuthorList");
        try
        {
            SqlDataAdapter da;
            DataSet ds = new DataSet();

            con = new SqlConnection(str);
            con.Open();
            cmdString = " select EmailId from Publishcation_Author where PaublicationID=@PaublicationID and TypeOfEntry=@TypeOfEntry and (MUNonMU='M' )";
            da = new SqlDataAdapter(cmdString, con);

            da.SelectCommand.Parameters.Add("@PaublicationID", SqlDbType.VarChar, 10);
            da.SelectCommand.Parameters["@PaublicationID"].Value = id;
            da.SelectCommand.Parameters.Add("@TypeOfEntry", SqlDbType.VarChar, 5);
            da.SelectCommand.Parameters["@TypeOfEntry"].Value = Type;

            da.SelectCommand.CommandType = CommandType.Text;


            da.Fill(ds);

            return ds;
        }
        catch (Exception e)
        {
            log.Error(e.Message);
            log.Error(e.StackTrace);

            throw e;
        }
        finally
        {
            con.Close();
        }

    }

    public DataSet getInvetigatorList(string id, string Type)
    {
        log.Debug("Inside function getInvetigatorList");
        try
        {
            SqlDataAdapter da;
            DataSet ds = new DataSet();

            con = new SqlConnection(str);
            con.Open();
            cmdString = " select EmailId from Projectnvestigator where ID=@ID and ProjectType=@ProjectType";
            da = new SqlDataAdapter(cmdString, con);

            da.SelectCommand.Parameters.Add("@ID", SqlDbType.VarChar, 10);
            da.SelectCommand.Parameters["@ID"].Value = id;
            da.SelectCommand.Parameters.Add("@ProjectType", SqlDbType.VarChar, 5);
            da.SelectCommand.Parameters["@ProjectType"].Value = Type;

            da.SelectCommand.CommandType = CommandType.Text;


            da.Fill(ds);

            return ds;
        }
        catch (Exception e)
        {
            log.Error(e.Message);
            log.Error(e.StackTrace);

            throw e;
        }
        finally
        {
            con.Close();
        }

    }

    public DataSet getReserachClerksList()
    {
        log.Debug("Inside function getReserachClerksList");
        try
        {
            SqlDataAdapter da;
            DataSet ds = new DataSet();

            con = new SqlConnection(str);
            con.Open();
            cmdString = " select EmailId from User_M u,User_Role_Map m where u.User_Id=m.User_Id and m.Role_Id=2 and u.Active='Y'";
            da = new SqlDataAdapter(cmdString, con);



            da.SelectCommand.CommandType = CommandType.Text;


            da.Fill(ds);

            return ds;
        }
        catch (Exception e)
        {
            log.Error(e.Message);
            log.Error(e.StackTrace);

            throw e;
        }
        finally
        {
            con.Close();
        }

    }

    public DataSet getReserachDirectorList()
    {
        log.Debug("Inside function getReserachDirectorList");
        try
        {
            SqlDataAdapter da;
            DataSet ds = new DataSet();

            con = new SqlConnection(str);
            con.Open();
            cmdString = " select EmailId from User_M u,User_Role_Map m where u.User_Id=m.User_Id and m.Role_Id=8 and u.Active='Y'";
            da = new SqlDataAdapter(cmdString, con);



            da.SelectCommand.CommandType = CommandType.Text;


            da.Fill(ds);

            return ds;
        }
        catch (Exception e)
        {
            log.Error(e.Message);
            log.Error(e.StackTrace);

            throw e;
        }
        finally
        {
            con.Close();
        }

    }


    public DataSet getReserachFinanceList()
    {
        log.Debug("Inside function getReserachFinanceList");
        try
        {
            SqlDataAdapter da;
            DataSet ds = new DataSet();

            con = new SqlConnection(str);
            con.Open();
            cmdString = " select EmailId from User_M u,User_Role_Map m where u.User_Id=m.User_Id and m.Role_Id=6 and u.Active='Y'";
            da = new SqlDataAdapter(cmdString, con);



            da.SelectCommand.CommandType = CommandType.Text;


            da.Fill(ds);

            return ds;
        }
        catch (Exception e)
        {
            log.Error(e.Message);
            log.Error(e.StackTrace);

            throw e;
        }
        finally
        {
            con.Close();
        }

    }

    public DataSet getAuthorListName(string id, string Type)
    {
        log.Debug("Inside function getAuthorListName");
        try
        {
            SqlDataAdapter da;
            DataSet ds = new DataSet();

            con = new SqlConnection(str);
            con.Open();
            cmdString = "  select AuthorName from Publishcation_Author where PaublicationID=@PaublicationID and TypeOfEntry=@TypeOfEntry";
            da = new SqlDataAdapter(cmdString, con);

            da.SelectCommand.Parameters.Add("@PaublicationID", SqlDbType.VarChar, 10);
            da.SelectCommand.Parameters["@PaublicationID"].Value = id;
            da.SelectCommand.Parameters.Add("@TypeOfEntry", SqlDbType.VarChar, 5);
            da.SelectCommand.Parameters["@TypeOfEntry"].Value = Type;

            da.SelectCommand.CommandType = CommandType.Text;


            da.Fill(ds);

            return ds;
        }
        catch (Exception e)
        {
            log.Error(e.Message);
            log.Error(e.StackTrace);

            throw e;
        }
        finally
        {
            con.Close();
        }

    }

    public DataSet getInvietigatorListName(string id, string Type)
    {
        log.Debug("Inside function  getInvietigatorListName");
        try
        {
            SqlDataAdapter da;
            DataSet ds = new DataSet();

            con = new SqlConnection(str);
            con.Open();
            cmdString = "  select InvestigatorName from Projectnvestigator where ID=@ID and ProjectType=@ProjectType";
            da = new SqlDataAdapter(cmdString, con);

            da.SelectCommand.Parameters.Add("@ID", SqlDbType.VarChar, 10);
            da.SelectCommand.Parameters["@ID"].Value = id;
            da.SelectCommand.Parameters.Add("@ProjectType", SqlDbType.VarChar, 5);
            da.SelectCommand.Parameters["@ProjectType"].Value = Type;

            da.SelectCommand.CommandType = CommandType.Text;


            da.Fill(ds);

            return ds;
        }
        catch (Exception e)
        {
            log.Error(e.Message);
            log.Error(e.StackTrace);

            throw e;
        }
        finally
        {
            con.Close();
        }

    }
    // function for getting the value of indexed or not

    public String GetIndexValue(string PubId, string Title)
    {

        try
        {
            //cmdString = "select Indexed from Publication where PublicationID='" + PubId + "' and TitleWorkItem='" + Title + "' ";
            cmdString = "select Indexed from Publication where PublicationID=@PubId and TitleWorkItem=@Title";


            con = new SqlConnection(str);
            con.Open();
            cmd = new SqlCommand(cmdString, con);
            string indexed = null;
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@PubId", PubId);
            cmd.Parameters.AddWithValue("@Title", Title);
            SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read())// for reading the value from database and to store it in a variable
            {
                if (!Convert.IsDBNull(sdr["Indexed"]))
                {
                    indexed = (string)sdr["Indexed"];
                }


            }

            return indexed;// returning back the received value back to business layer
        }
        catch (Exception ex)
        {
            log.Error("Inside - get indexed value catch block ");
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

    public IndexManage selectIndexAgency1(string name)
    {
        log.Debug("Inside - function - select distict department");
        try
        {
            //cmdString = "select distinct Institution,Department from Publishcation_Author where AuthorName='" + name + "' ";
            //cmdString = "select distinct Institution,Department from Publishcation_Author where EmployeeCode='" + name + "' ";
            cmdString = "select distinct Institution,Department from Publishcation_Author where EmployeeCode=@name";

            con = new SqlConnection(str);
            con.Open();
            cmd = new SqlCommand(cmdString, con);



            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@name", name);
            IndexManage p = new IndexManage();


            SqlDataReader sdr = cmd.ExecuteReader();

            while (sdr.Read())
            {
                if (!Convert.IsDBNull(sdr["Institution"]))
                {
                    p.nInstitution = (string)sdr["Institution"];
                }
                if (!Convert.IsDBNull(sdr["Department"]))
                {
                    p.nDepartment = (string)sdr["Department"];
                }

            }

            return p;
        }
        catch (Exception ex)
        {
            log.Error("Inside - select distict department catch block ");
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

    public IndexManage selectIndexAgency2(string dep1, string inst1)
    {
        log.Debug("Inside function find cc emailid");
        try
        {


            //cmdString = "select EmailId from Library_M where Library_Id=(select  LibraryId from Dept_M where DeptId ='" + dep1 + "' and Institute_Id='" + inst1 + "')";

            cmdString = "select EmailId from Library_M where Library_Id=(select  LibraryId from Dept_M where DeptId =@dep1 and Institute_Id=@inst1)";

            //  cmdString = "select distinct Institution,Department from Publishcation_Author where AuthorName='" + name + "' ";

            con = new SqlConnection(str);
            con.Open();
            cmd = new SqlCommand(cmdString, con);



            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@dep1", dep1);
            cmd.Parameters.AddWithValue("@inst1", inst1);
            IndexManage p = new IndexManage();


            SqlDataReader sdr = cmd.ExecuteReader();

            while (sdr.Read())
            {
                if (!Convert.IsDBNull(sdr["EmailId"]))
                {
                    p.emailid = (string)sdr["EmailId"];
                }

            }

            return p;
        }
        catch (Exception ex)
        {
            log.Error("Inside - cc emailid catch block ");
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

    public User ImagePath()
    {

        try
        {
            //cmdString = "select o.BoX_ID,w.PatientName,w.F_G_SName, TreatmentStartDate,o.Status, TreatmentEndDate,PatientComplianceVisits from OD_Patient_Data o ,Waiting_List w where w.WLID=o.WLID and BoX_ID=@PatientID";
            //  con = new SqlConnection(str);
            con.Open();
            cmd = new SqlCommand("fnfindImagePath", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader reader = cmd.ExecuteReader();
            // PatientPOD p = new PatientPOD();
            User p = new User();
            while (reader.Read())
            {
                p.imgpath = (string)reader["path"];




            }
            return p;
        }
        catch (Exception e)
        {
            log.Debug("Error: Inside catch block of fnfindImagePath");
            log.Error("Error msg:" + e);
            log.Error("Stack trace:" + e.StackTrace);
            e.ToString();
            return null;
        }
        finally
        {
            con.Close();
        }

    }


    public User fnfindPatient(string HospitalNo)
    {

        log.Debug("inside function finddetails ");
        try
        {
            //cmdString = "select o.BoX_ID,w.PatientName,w.F_G_SName, TreatmentStartDate,o.Status, TreatmentEndDate,PatientComplianceVisits from OD_Patient_Data o ,Waiting_List w where w.WLID=o.WLID and BoX_ID=@PatientID";
            //  con = new SqlConnection(str);
            con.Open();
            cmd = new SqlCommand("fnfindPatient3", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@PatientID", SqlDbType.VarChar, 12);
            cmd.Parameters["@PatientID"].Value = HospitalNo;
            SqlDataReader reader = cmd.ExecuteReader();
            // PatientPOD p = new PatientPOD();
            User p = new User();
            while (reader.Read())
            {
                p.TitleWorkItem = (string)reader["TitleWorkItem"];
                p.JAVolume = (string)reader["JAVolume"];
                p.PublishJAMonth2 = (string)reader["months"];
                p.PublishJAYear = (string)reader["PublishJAYear"];
                p.PubJournalName = (string)reader["PubJournalName"];
                p.TypeOfEntry = (string)reader["TypeOfEntry"];
                p.MUCategorization = (string)reader["MUCategorization"];
                p.Indexed = (string)reader["Indexed"];
                p.Publicationtype = (string)reader["Publicationtype"];
                if (!Convert.IsDBNull(reader["PageFrom"]))
                {
                    p.PageFrom = (string)reader["PageFrom"];
                }
                else
                {
                    p.PageFrom = "";
                }
                if (!Convert.IsDBNull(reader["PageTo"]))
                {
                    p.PageTo = (string)reader["PageTo"];
                }
                else
                {
                    p.PageTo = "";
                }
                if (!Convert.IsDBNull(reader["ImpactFactor"]))
                {
                    p.impcat1 = (string)reader["ImpactFactor"];
                }
                if (!Convert.IsDBNull(reader["FiveImpFact"]))
                {
                    p.impcat5 = (string)reader["FiveImpFact"];
                }
                p.issues = (string)reader["Issue"];
                if (!Convert.IsDBNull(reader["IF_ApplicableYear"]))
                {
                    p.IFApplicableYear = (int)reader["IF_ApplicableYear"];
                }
            }
            return p;
        }
        catch (Exception e)
        {
            log.Debug("Error: Inside catch block of finddetails");
            log.Error("Error msg:" + e);
            log.Error("Stack trace:" + e.StackTrace);
            e.ToString();
            return null;
        }
        finally
        {
            con.Close();
        }

    }


    public User tableDisplay(string AuthorID)
    {

        log.Debug("inside function author count");
        try
        {
            //cmdString = "select o.BoX_ID,w.PatientName,w.F_G_SName, TreatmentStartDate,o.Status, TreatmentEndDate,PatientComplianceVisits from OD_Patient_Data o ,Waiting_List w where w.WLID=o.WLID and BoX_ID=@PatientID";
            //  con = new SqlConnection(str);
            con.Open();
            cmd = new SqlCommand("dec_proc1", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@PatientID", SqlDbType.VarChar, 12);
            cmd.Parameters["@PatientID"].Value = AuthorID;
            SqlDataReader reader = cmd.ExecuteReader();
            // PatientPOD p = new PatientPOD();
            User p = new User();
            while (reader.Read())
            {

                p.AuthorCount = (int)reader["PaublicationID"];

            }
            return p;
        }
        catch (Exception e)
        {
            log.Debug("Error: Inside catch block of author count");
            log.Error("Error msg:" + e);
            log.Error("Stack trace:" + e.StackTrace);
            e.ToString();
            return null;
        }
        finally
        {
            con.Close();
        }

    }

    public DataSet fnfindjournalAccount12(string PatientID)
    {

        con = new SqlConnection(str);
        con.Open();
        try
        {
            //  cmdString = "select  convert(numeric(13,2),Amount) as Amount ,DebitCredit as DrCr, a.account as account, a.oprunit as oprUnit,a.DeptID as dept,a.AffilateBU as affiliate,openitem as openItem, a.linenarration as lineNar, ADesc as accountName, OprUnit_M.OUnitName as oprUnitName,Department_M.DeptName,OpenItem_M.EmpName,a.JournalLine,EmpName as openItemName from GL_Accounting_T a  LEFT OUTER JOIN OprUnit_M ON a.OPRUNIT = OprUnit_M.OPRUNIT LEFT OUTER JOIN Department_M ON a.DeptID = Department_M.DeptCode LEFT OUTER JOIN OpenItem_M ON a.openitem = OpenItem_M.EmpCode, GL d , Account_M where a.JournalID=d.JournalID  and Account_M.Account= a.Account and a.BusinessUnit=d.BusinessUnit and a.JournalID=@JournalID and a.BusinessUnit=@BusinessUnit order by a.JournalLine";
            SqlDataAdapter da;
            DataSet ds;
            cmdString = "select AuthorName,EmployeeCode,isCorrAuth,(case when AuthorType='P' then 'First Author' when AuthorType='C' then 'CO_Author' else 'Other' end) as AuthorType,Dept_M.DisplayName+''+Institute_M.DisplayName as Department from Publishcation_Author,Dept_M,Institute_M where PaublicationID=@PublicationId and Publishcation_Author.TypeOfEntry='JA' and MUNonMU='M' and Dept_M.DeptId=Publishcation_Author.Department and Dept_M.Institute_Id=Publishcation_Author.Institution and Institute_M.Institute_Id=Publishcation_Author.Institution and Dept_M.Institute_Id=Institute_M.Institute_Id ";
            cmd = new SqlCommand(cmdString, con);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add("@PublicationId", SqlDbType.VarChar, 15);
            cmd.Parameters["@PublicationId"].Value = PatientID;

            da = new SqlDataAdapter(cmd);

            ds = new DataSet();
            da.Fill(ds);

            return ds;
        }

        catch (Exception ex)
        {
            log.Error("Inside find author type catch block ");
            log.Error(ex.Message);
            log.Error(ex.StackTrace);
            throw ex;
        }

        finally
        {
            con.Close();
        }
    }


    public User find_Catagory(string AuthorID)
    {

        log.Debug("inside find_Catagory");
        try
        {
            //cmdString = "select o.BoX_ID,w.PatientName,w.F_G_SName, TreatmentStartDate,o.Status, TreatmentEndDate,PatientComplianceVisits from OD_Patient_Data o ,Waiting_List w where w.WLID=o.WLID and BoX_ID=@PatientID";
            //  con = new SqlConnection(str);
            con.Open();
            cmd = new SqlCommand("find_Catagory", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@id", SqlDbType.VarChar, 12);
            cmd.Parameters["@id"].Value = AuthorID;
            SqlDataReader reader = cmd.ExecuteReader();
            // PatientPOD p = new PatientPOD();
            User p = new User();
            while (reader.Read())
            {

                p.Article = (string)reader["Article"];

            }
            return p;
        }
        catch (Exception e)
        {
            log.Debug("Error: Inside catch block of find_Catagory");
            log.Error("Error msg:" + e);
            log.Error("Stack trace:" + e.StackTrace);
            e.ToString();
            return null;
        }
        finally
        {
            con.Close();
        }

    }


    public User get_submit_data(string AuthorID)
    {

        log.Debug("inside function submit");
        try
        {
            //cmdString = "select o.BoX_ID,w.PatientName,w.F_G_SName, TreatmentStartDate,o.Status, TreatmentEndDate,PatientComplianceVisits from OD_Patient_Data o ,Waiting_List w where w.WLID=o.WLID and BoX_ID=@PatientID";
            //  con = new SqlConnection(str);
            con.Open();
            cmd = new SqlCommand("submit_data", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@id", SqlDbType.VarChar, 12);
            cmd.Parameters["@id"].Value = AuthorID;
            SqlDataReader reader = cmd.ExecuteReader();
            // PatientPOD p = new PatientPOD();,,,
            User p = new User();
            while (reader.Read())
            {
                p.createdId = (string)reader["CreatedBy"];

            }
            return p;
        }
        catch (Exception e)
        {
            log.Debug("Error: Inside catch block of submit");
            log.Error("Error msg:" + e);
            log.Error("Stack trace:" + e.StackTrace);
            e.ToString();
            return null;
        }
        finally
        {
            con.Close();
        }
    }

    //public int fnPatOrthoAuxiVisitDet(User pa)
    //{

    //    log.Debug("inside function find id");


    //    try
    //    {
    //        int result1 = 0;

    //        con = new SqlConnection(str);
    //        con.Open();

    //        SqlCommand cmd = new SqlCommand("fnPatOrthoAuxiVisitDetR1", con);
    //        cmd.Parameters.AddWithValue("@FilePath", pa.FilePath);
    //        cmd.Parameters.AddWithValue("@PublicationID", pa.PatientID);

    //        cmd.CommandType = CommandType.StoredProcedure;
    //        result1 = cmd.ExecuteNonQuery();


    //        return result1;
    //    }
    //    catch (Exception e)
    //    {
    //        log.Debug("Error: Inside catch block of function findid");
    //        log.Error("Error msg:" + e);
    //        log.Error("Stack trace:" + e.StackTrace);

    //        return 0;
    //    }
    //    finally
    //    {
    //        con.Close();
    //    }


    //}


    public User indexFind(string AuthorID)
    {

        log.Debug("inside function indexFind");
        try
        {
            //cmdString = "select o.BoX_ID,w.PatientName,w.F_G_SName, TreatmentStartDate,o.Status, TreatmentEndDate,PatientComplianceVisits from OD_Patient_Data o ,Waiting_List w where w.WLID=o.WLID and BoX_ID=@PatientID";
            //  con = new SqlConnection(str);
            con.Open();
            cmd = new SqlCommand("find_index", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@id", SqlDbType.VarChar, 12);
            cmd.Parameters["@id"].Value = AuthorID;
            SqlDataReader reader = cmd.ExecuteReader();
            // PatientPOD p = new PatientPOD();
            User p = new User();
            while (reader.Read())
            {

                p.Index = (string)reader["Index_Agency"];


            }
            return p;
        }
        catch (Exception e)
        {
            log.Debug("Error: Inside catch block of indexFind");
            log.Error("Error msg:" + e);
            log.Error("Stack trace:" + e.StackTrace);
            e.ToString();
            return null;
        }
        finally
        {
            con.Close();
        }

    }


    public User fnGetFilePathPdf(string a)
    {
        log.Debug("inside fnGetFilePathPdf while selecting file path");

        try
        {
            con = new SqlConnection(str);
            con.Open();

            cmdString = "select FilePath from Publication where PublicationID=@BoxID";
            cmd = new SqlCommand(cmdString, con);

            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add("@BoxID", SqlDbType.VarChar, 15);
            cmd.Parameters["@BoxID"].Value = a;



            User p = new User();
            SqlDataReader d = cmd.ExecuteReader();
            if (d.HasRows)
            {
                while (d.Read())
                {

                    p.path = (string)d["FilePath"];


                }


            }

            return p;
        }
        catch (Exception ex)
        {
            log.Debug("Error: Inside catch block of fnGetFilePathPdf");
            log.Error("Error msg:" + ex);
            log.Error("Stack trace:" + ex.StackTrace);
            throw ex;
        }
        finally
        {
            con.Close();
        }
    }

    public User GetFirstAuthorName(string id, string TypeOfEntry)
    {

        try
        {

            // cmdString = "select UserId from Publication_InchargerM where InstituteId='" + inst + "'  and Department_Id='" + dept + "' ";

            //     cmdString = "select Library_Id from Library_Institute_Map where Institute_Id='" + inst + "' ";
            // cmdString = "select Library_Id from Library_Dept_Map where Dept_Id='" + dept + "' ";
            // 
            //cmdString = "select Prefix,FirstName,MiddleName,LastName from User_M where EmailId=(select EmailId from Publishcation_Author where PaublicationID='" + id + "' and AuthorType='P' and TypeOfEntry='" + TypeOfEntry + "') ";
            cmdString = "select Prefix,FirstName,MiddleName,LastName from User_M where EmailId=(select EmailId from Publishcation_Author where PaublicationID=@id and AuthorType='P' and TypeOfEntry=@TypeOfEntry) ";

            //cmdString = "select Library_Id from Library_M where EmailId='" + email + "'";
            con = new SqlConnection(str);
            con.Open();
            cmd = new SqlCommand(cmdString, con);
            User u = new User();
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@TypeOfEntry", TypeOfEntry);
            IndexManage p = new IndexManage();
            SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {

                if (!Convert.IsDBNull(sdr["FirstName"]))
                {
                    u.AFirstName = (string)sdr["FirstName"];
                }

                if (!Convert.IsDBNull(sdr["MiddleName"]))
                {
                    u.AMiddleName = (string)sdr["MiddleName"];
                }


                if (!Convert.IsDBNull(sdr["LastName"]))
                {
                    u.ALastName = (string)sdr["LastName"];
                }

                if (!Convert.IsDBNull(sdr["Prefix"]))
                {
                    u.APrefix = (string)sdr["Prefix"];
                }


            }

            return u;
        }
        catch (Exception ex)
        {
            log.Error("Inside - GetFirstAuthorName catch block ");
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

    public DataSet getFirstAuthor(string id, string TypeOfEntry)
    {
        log.Debug("Inside function getFirstAuthor");
        try
        {
            SqlDataAdapter da;
            DataSet ds = new DataSet();

            con = new SqlConnection(str);
            con.Open();
            cmdString = "select EmailId from Publication,User_M where PublicationID=@id and TypeOfEntry=@TypeOfEntry and User_M.User_Id=Publication.CreatedBy";
            da = new SqlDataAdapter(cmdString, con);

            da.SelectCommand.Parameters.Add("@id", SqlDbType.VarChar, 10);
            da.SelectCommand.Parameters["@id"].Value = id;
            da.SelectCommand.Parameters.Add("@TypeOfEntry", SqlDbType.VarChar, 2);
            da.SelectCommand.Parameters["@TypeOfEntry"].Value = TypeOfEntry;
            da.SelectCommand.CommandType = CommandType.Text;
            da.Fill(ds);
            return ds;
        }
        catch (Exception e)
        {
            log.Error(e.Message);
            log.Error(e.StackTrace);

            throw e;
        }
        finally
        {
            con.Close();
        }

    }

    public DataSet getAuthorCCList(string id, string TypeOfEntry)
    {
        log.Debug("Inside function getAuthorCCList");
        try
        {
            SqlDataAdapter da;
            DataSet ds = new DataSet();

            con = new SqlConnection(str);
            con.Open();
            cmdString = "select EmailId from Publishcation_Author where PaublicationID=@id and MUNonMU='M' and TypeOfEntry=@TypeOfEntry ";
            da = new SqlDataAdapter(cmdString, con);

            da.SelectCommand.Parameters.Add("@id", SqlDbType.VarChar, 10);
            da.SelectCommand.Parameters["@id"].Value = id;
            da.SelectCommand.Parameters.Add("@TypeOfEntry", SqlDbType.VarChar, 2);
            da.SelectCommand.Parameters["@TypeOfEntry"].Value = TypeOfEntry;
            da.SelectCommand.CommandType = CommandType.Text;
            da.Fill(ds);
            return ds;
        }
        catch (Exception e)
        {
            log.Error(e.Message);
            log.Error(e.StackTrace);

            throw e;
        }
        finally
        {
            con.Close();
        }

    }


    public DataSet getAuthorListName1(string id, string TypeOfEntry)
    {
        log.Debug("Inside function getAuthorListName1");
        try
        {
            SqlDataAdapter da;
            DataSet ds = new DataSet();

            con = new SqlConnection(str);
            con.Open();//Publishcation_Author where PaublicationID='00000092' and AuthorType='P'
            cmdString = "select AuthorName from Publishcation_Author where PaublicationID=@ID and TypeOfEntry=@TypeOfEntry ";
            da = new SqlDataAdapter(cmdString, con);
            da.SelectCommand.Parameters.Add("@ID", SqlDbType.VarChar, 10);
            da.SelectCommand.Parameters["@ID"].Value = id;
            da.SelectCommand.Parameters.Add("@TypeOfEntry", SqlDbType.VarChar, 2);
            da.SelectCommand.Parameters["@TypeOfEntry"].Value = TypeOfEntry;
            da.SelectCommand.CommandType = CommandType.Text;


            da.Fill(ds);

            return ds;
        }
        catch (Exception e)
        {
            log.Error(e.Message);
            log.Error(e.StackTrace);

            throw e;
        }
        finally
        {
            con.Close();
        }

    }


    public JournalData GetImpactFactor(JournalData jdValueObj)
    {
        log.Debug("Inside Journal_DataObject- GetImpactFactor function ");
        con = new SqlConnection(str);
        con.Open();

        try
        {
            cmdString = "Select * from Journal_IF_Details where Id=@Id";
            cmd = new SqlCommand(cmdString, con, transaction);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@Id", HttpUtility.HtmlDecode(jdValueObj.JournalID));

            SqlDataReader sdr = cmd.ExecuteReader();
            JournalData j = new JournalData();
            while (sdr.Read())
            {
                if (sdr.HasRows)
                {
                    if (!Convert.IsDBNull(sdr["Id"]))
                    {
                        j.jid = sdr["Id"].ToString();
                    }
                    if (!Convert.IsDBNull(sdr["ImpactFactor"]))
                    {
                        j.impctfact = (double)sdr["ImpactFactor"];
                    }
                    if (!Convert.IsDBNull(sdr["Comments"]))
                    {
                        j.Comments = sdr["Comments"].ToString();
                    }
                    if (!Convert.IsDBNull(sdr["FiveImpFact"]))
                    {
                        j.fiveimpcrfact = (double)sdr["FiveImpFact"];
                    }

                }
            }
            return j;
        }
        catch (Exception ex)
        {
            log.Error("Inside Journal_DataObject-GetImpactFactor catch block of ISSN:  " + jdValueObj.JournalID);
            log.Error(ex.Message);
            log.Error(ex.StackTrace);
            throw (ex);
        }
        finally
        {
            cmd.Dispose();
            con.Close();
            cmd.Dispose();
        }
    }

    public User getEnteredBy(string UserId)
    {

        log.Debug("inside function submit");
        try
        {
            //cmdString = "select o.BoX_ID,w.PatientName,w.F_G_SName, TreatmentStartDate,o.Status, TreatmentEndDate,PatientComplianceVisits from OD_Patient_Data o ,Waiting_List w where w.WLID=o.WLID and BoX_ID=@PatientID";
            //  con = new SqlConnection(str);
            con.Open();
            cmd = new SqlCommand("GetAuthorName", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@id", SqlDbType.VarChar, 12);
            cmd.Parameters["@id"].Value = UserId;
            SqlDataReader reader = cmd.ExecuteReader();
            // PatientPOD p = new PatientPOD();,,,
            User p = new User();
            while (reader.Read())
            {


                p.createdName = (string)reader["name"];



            }
            return p;
        }
        catch (Exception e)
        {
            log.Debug("Error: Inside catch block of submit");
            log.Error("Error msg:" + e);
            log.Error("Stack trace:" + e.StackTrace);
            e.ToString();
            return null;
        }
        finally
        {
            con.Close();
        }
    }



    public User get_Author_Details(string UserId)
    {

        log.Debug("inside function submit");
        try
        {
            //cmdString = "select o.BoX_ID,w.PatientName,w.F_G_SName, TreatmentStartDate,o.Status, TreatmentEndDate,PatientComplianceVisits from OD_Patient_Data o ,Waiting_List w where w.WLID=o.WLID and BoX_ID=@PatientID";
            //  con = new SqlConnection(str);
            con.Open();
            cmd = new SqlCommand("get_Author_Details", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@id", SqlDbType.VarChar, 12);
            cmd.Parameters["@id"].Value = UserId;
            SqlDataReader reader = cmd.ExecuteReader();
            // PatientPOD p = new PatientPOD();,,,
            User p = new User();
            while (reader.Read())
            {


                p.adep = (string)reader["DepartmentName"];
                p.aemail = (string)reader["EmailId"];
                p.ainst = (string)reader["InstitutionName"];



            }
            return p;
        }
        catch (Exception e)
        {
            log.Debug("Error: Inside catch block of submit");
            log.Error("Error msg:" + e);
            log.Error("Stack trace:" + e.StackTrace);
            e.ToString();
            return null;
        }
        finally
        {
            con.Close();
        }
    }

    //Akshatha
    //Function to check duplicates journal publication entry
    public ArrayList chekDuplicateJournalEntry(PublishData j)
    {
        ArrayList publicationid = new ArrayList();
        PublishData obj = new PublishData();
        try
        {
            con = new SqlConnection(str);
            con.Open();
            cmd = new SqlCommand("SelectDuplicateJournalEntry", con);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@PubJournalID", SqlDbType.VarChar, 10);
            cmd.Parameters["@PubJournalID"].Value = j.PublisherOfJournal;

            cmd.Parameters.Add("@JAVolume", SqlDbType.VarChar, 10);
            cmd.Parameters["@JAVolume"].Value = j.JAVolume;

            cmd.Parameters.Add("@PageFrom", SqlDbType.VarChar, 10);
            cmd.Parameters["@PageFrom"].Value = j.PageFrom;

            cmd.Parameters.Add("@PageTo", SqlDbType.VarChar, 10);
            cmd.Parameters["@PageTo"].Value = j.PageTo;

            //cmd.Parameters.Add("@Keyword", SqlDbType.VarChar, 100);
            //cmd.Parameters["@Keyword"].Value = j.Keywords;

            cmd.Parameters.Add("@Issue", SqlDbType.VarChar, 100);
            cmd.Parameters["@Issue"].Value = j.Issue;

            cmd.Parameters.Add("@TypeOfEntry", SqlDbType.VarChar, 2);
            cmd.Parameters["@TypeOfEntry"].Value = j.TypeOfEntry;

            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    if (!Convert.IsDBNull(sdr["PublicationID"]))
                    {
                        obj.PaublicationID = sdr["PublicationID"].ToString();
                        publicationid.Add(obj.PaublicationID);
                    }

                }
            }

            return publicationid;
        }
        catch (Exception ex)
        {
            log.Error("Inside - chekDuplicateJournalEntry catch block ");
            log.Error(ex.Message);
            log.Error(ex.StackTrace);
            throw ex;
        }

        finally
        {
            con.Close();
        }
    }

    public JournalData GetImpactFactorApplicableYear(JournalData JournalValueObj)
    {
        log.Debug("Inside - GetImpactFactorApplicableYear function");
        JournalData obj = new JournalData();
        try
        {
            con = new SqlConnection(str);
            con.Open();
            cmd = new SqlCommand("SelectISSNApplicableYear", con);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", HttpUtility.HtmlDecode(JournalValueObj.JournalID));
            cmd.Parameters.AddWithValue("@PublishJAYear", HttpUtility.HtmlDecode(JournalValueObj.year));
            cmd.Parameters.AddWithValue("@PublishJAMonth", HttpUtility.HtmlDecode(JournalValueObj.month));

            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    if (!Convert.IsDBNull(sdr["Id"]))
                    {
                        obj.JournalID = (string)sdr["Id"];
                    }

                    if (!Convert.IsDBNull(sdr["Year"]))
                    {
                        obj.year = (string)sdr["Year"];
                    }
                    if (!Convert.IsDBNull(sdr["ImpactFactor"]))
                    {
                        obj.impctfact = (double)sdr["ImpactFactor"];
                    }

                    if (!Convert.IsDBNull(sdr["fiveImpFact"]))
                    {
                        obj.fiveimpcrfact = (double)sdr["fiveImpFact"];
                    }


                }

            }
            else
            {
                obj.JournalID = "";
            }
            return obj;
        }
        catch (Exception ex)
        {
            log.Error("Inside - GetImpactFactorApplicableYear catch block ");
            log.Error(ex.Message);
            log.Error(ex.StackTrace);
            throw ex;
        }

        finally
        {
            con.Close();
        }
    }



    public PublishData SelectDefaultAuthor(PublishData value)
    {
        PublishData data = new PublishData();
        log.Debug("Inside Journal_DataObject- SelectDefaultAuthor function ");
        con = new SqlConnection(str);
        con.Open();
        string author = "";
        try
        {
            cmdString = "Select Publication.CreatedBy,prefix+' '+firstname+' '+middlename+' '+lastname  as Name from Publication,User_M  where PublicationID=@Id and TypeOfEntry=@TypeOfEntry and User_M.User_Id=Publication.CreatedBy";
            cmd = new SqlCommand(cmdString, con, transaction);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@Id", value.PaublicationID);
            cmd.Parameters.AddWithValue("@TypeOfEntry", value.TypeOfEntry);
            SqlDataReader sdr = cmd.ExecuteReader();
            JournalData j = new JournalData();
            while (sdr.Read())
            {
                if (sdr.HasRows)
                {
                    if (!Convert.IsDBNull(sdr["CreatedBy"]))
                    {
                        data.CreatedBy = sdr["CreatedBy"].ToString();
                    }

                    if (!Convert.IsDBNull(sdr["Name"]))
                    {
                        data.AuthorName = sdr["Name"].ToString();
                    }
                }
            }
            return data;
        }
        catch (Exception ex)
        {
            log.Error("Inside Journal_DataObject-SelectDefaultAuthor catch block of Id:  " + value.PaublicationID);
            log.Error(ex.Message);
            log.Error(ex.StackTrace);
            throw (ex);
        }
        finally
        {
            cmd.Dispose();
            con.Close();
            cmd.Dispose();
        }
    }

    public DataSet BindPublication(PublishData pub)
    {
        log.Debug("Inside BindPublication function");

        try
        {

            SqlDataAdapter da;
            DataSet ds;
            con = new SqlConnection(str);
            con.Open();
            da = new SqlDataAdapter("SelectIndexedPublication", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            if (pub.PaublicationID != "")
            {
                da.SelectCommand.Parameters.AddWithValue("@PublicationID", pub.PaublicationID);
            }
            else
            {
                da.SelectCommand.Parameters.AddWithValue("@PublicationID", String.Empty);
            }
            if (pub.TypeOfEntry != "")
            {

                da.SelectCommand.Parameters.AddWithValue("@TypeOfEntry", pub.TypeOfEntry);
            }
            else
            {
                da.SelectCommand.Parameters.AddWithValue("@TypeOfEntry", "");
            }
            if (pub.Institution != "")
            {
                da.SelectCommand.Parameters.AddWithValue("@Institution", pub.Institution);
            }
            else
            {
                da.SelectCommand.Parameters.AddWithValue("@Institution", "");
            }
            if (pub.Department != "")
            {
                da.SelectCommand.Parameters.AddWithValue("@Department", pub.Department);
            }
            else
            {
                da.SelectCommand.Parameters.AddWithValue("@Department", "");
            }

            if (pub.Indexed != "")
            {
                da.SelectCommand.Parameters.AddWithValue("@Indexed", pub.Indexed);
            }
            else
            {
                da.SelectCommand.Parameters.AddWithValue("@Indexed", "");
            }
            if (pub.CreatedBy != "")
            {
                da.SelectCommand.Parameters.AddWithValue("@CreatedBy", pub.CreatedBy);
            }
            else
            {
                da.SelectCommand.Parameters.AddWithValue("@CreatedBy", "");

            }
            if (pub.TitleWorkItem != "")
            {
                da.SelectCommand.Parameters.AddWithValue("@TitleWorkItem", pub.TitleWorkItem);
            }
            else
            {
                da.SelectCommand.Parameters.AddWithValue("@TitleWorkItem", "");

            }
            if (pub.IndexedIn != "")
            {
                da.SelectCommand.Parameters.AddWithValue("@IndexAgency", pub.IndexedIn);
            }
            if (pub.IsStudentAuthor != "")
            {
                da.SelectCommand.Parameters.AddWithValue("@AuthorType", pub.IsStudentAuthor);
            }

            da.SelectCommand.Parameters.AddWithValue("@Status", pub.Status);
            ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

        catch (Exception e)
        {
            log.Error(e.Message);
            log.Error(e.StackTrace);

            throw e;
        }
        finally
        {
            cmd.Dispose();
            con.Close();
            cmd.Dispose();
        }
    }

    public bool RevertingStatusToNew(PublishData obj)
    {
        log.Debug("Inside RevertingStatusToNew function of Id:  " + obj.PaublicationID + obj.TypeOfEntry);

        con = new SqlConnection(str);
        con.Open();
        transaction = con.BeginTransaction();
        try
        {
            cmdString = "Update Publication set Status='New',IsRevert='Y',IncentivePointStatus=@IncentivePointStatus where TypeOfEntry=@Type AND PublicationID=@Publish_Id ";
            cmd = new SqlCommand(cmdString, con, transaction);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@Type", obj.TypeOfEntry);
            cmd.Parameters.AddWithValue("@Publish_Id", obj.PaublicationID);
            cmd.Parameters.AddWithValue("@IncentivePointStatus", DBNull.Value);
            bool result = Convert.ToBoolean(cmd.ExecuteNonQuery());

            cmdString = "Select count(* ) as Count from Publish_ReviewTracker where Type=@Type AND Publish_Id=@Publish_Id ";
            cmd = new SqlCommand(cmdString, con, transaction);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@Type", obj.TypeOfEntry);
            cmd.Parameters.AddWithValue("@Publish_Id", obj.PaublicationID);

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

            cmd = new SqlCommand("InsertPublicationReviewTracker", con, transaction);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Publish_Id", obj.PaublicationID);
            cmd.Parameters.AddWithValue("@Type", obj.TypeOfEntry);

            cmd.Parameters.AddWithValue("@ReviewNo", count + 1);

            cmd.Parameters.AddWithValue("@ApprovedStatus", "New");


            cmd.Parameters.AddWithValue("@Remark", obj.RemarksFeedback);

            cmd.Parameters.AddWithValue("@EnterdBy", HttpContext.Current.Session["UserId"].ToString());
            cmd.Parameters.AddWithValue("@dateM", DateTime.Now);

            cmd.ExecuteNonQuery();


            transaction.Commit();
            log.Info("Publication Status Reverted Back To New of Publicatiom: " + obj.PaublicationID + obj.TypeOfEntry);
            log.Info("Publication Status Revert : User Name :" + HttpContext.Current.Session["UserName"] + "Role :" + HttpContext.Current.Session["RoleName"]);
            return result;

        }
        catch (Exception ex)
        {
            transaction.Rollback();
            log.Error("Inside RevertingStatusToNew catch block of Id:  " + obj.PaublicationID + obj.TypeOfEntry);
            log.Error(ex.Message);
            log.Error(ex.StackTrace);
            throw (ex);

        }
        finally
        {
            cmd.Dispose();
            con.Close();
            cmd.Dispose();
        }
    }

    public string SelectIsReverFlag(string p, string p_2)
    {
        log.Debug("Inside SelectIsReverFlag function of Id:  " + p + p_2);

        string IsRevert = "";
        con = new SqlConnection(str);
        con.Open();
        cmdString = "Select IsRevert from Publication where TypeOfEntry=@Type AND PublicationID=@Publish_Id ";
        cmd = new SqlCommand(cmdString, con);
        cmd.CommandType = CommandType.Text;
        cmd.Parameters.AddWithValue("@Type", p_2);
        cmd.Parameters.AddWithValue("@Publish_Id", p);

        SqlDataReader sdr = cmd.ExecuteReader();
        while (sdr.Read())
        {
            if (!Convert.IsDBNull(sdr["IsRevert"]))
            {
                IsRevert = (string)sdr["IsRevert"];
            }

        }
        sdr.Close();

        return IsRevert;
    }

    public bool UploadedPdfPath(PublishData obj)
    {
        log.Debug("Inside UploadedPdfPath function of Id:  " + obj.PaublicationID + obj.TypeOfEntry);
        bool result = false;
        con = new SqlConnection(str);
        con.Open();
        try
        {
            cmdString = "Update Publication set UploadPDFPath=@UploadPDFPath where TypeOfEntry=@Type AND PublicationID=@Publish_Id ";
            cmd = new SqlCommand(cmdString, con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@Type", obj.TypeOfEntry);
            cmd.Parameters.AddWithValue("@Publish_Id", obj.PaublicationID);
            cmd.Parameters.AddWithValue("@UploadPDFPath", obj.FilePath);
            result = Convert.ToBoolean(cmd.ExecuteNonQuery());
            log.Debug("Pdf path updated:  " + obj.PaublicationID + obj.TypeOfEntry);
            return result;
        }

        catch (Exception ex)
        {
            log.Error("Inside UploadedPdfPath catch block of Id:  " + obj.PaublicationID + obj.TypeOfEntry);
            log.Error(ex.Message);
            log.Error(ex.StackTrace);
            throw (ex);
        }
        finally
        {
            cmd.Dispose();
            con.Close();
            cmd.Dispose();
        }
    }

    public PublishData CheckIsStudentPublication(string PatientID)
    {
        log.Debug("Inside CheckIsStudentPublication function of Id:  " + PatientID);

        string ISStudentAuthor = "";
        con = new SqlConnection(str);
        con.Open();
        cmdString = "Select ISStudentAuthor,PublishJAYear from Publication where TypeOfEntry=@Type AND PublicationID=@Publish_Id ";
        cmd = new SqlCommand(cmdString, con);
        cmd.CommandType = CommandType.Text;
        cmd.Parameters.AddWithValue("@Type", "JA");
        cmd.Parameters.AddWithValue("@Publish_Id", PatientID);
        PublishData data = new PublishData();
        SqlDataReader sdr = cmd.ExecuteReader();
        while (sdr.Read())
        {
            if (!Convert.IsDBNull(sdr["ISStudentAuthor"]))
            {
                data.IsStudentAuthor = (string)sdr["ISStudentAuthor"];
            }
            if (!Convert.IsDBNull(sdr["PublishJAYear"]))
            {
                data.PublishJAYear = (string)sdr["PublishJAYear"];
            }
        }
        sdr.Close();

        return data;
    }

    public DataSet SelectStudentAuthorDetail(string PatientID)
    {
        con = new SqlConnection(str);
        con.Open();
        try
        {
            //  cmdString = "select  convert(numeric(13,2),Amount) as Amount ,DebitCredit as DrCr, a.account as account, a.oprunit as oprUnit,a.DeptID as dept,a.AffilateBU as affiliate,openitem as openItem, a.linenarration as lineNar, ADesc as accountName, OprUnit_M.OUnitName as oprUnitName,Department_M.DeptName,OpenItem_M.EmpName,a.JournalLine,EmpName as openItemName from GL_Accounting_T a  LEFT OUTER JOIN OprUnit_M ON a.OPRUNIT = OprUnit_M.OPRUNIT LEFT OUTER JOIN Department_M ON a.DeptID = Department_M.DeptCode LEFT OUTER JOIN OpenItem_M ON a.openitem = OpenItem_M.EmpCode, GL d , Account_M where a.JournalID=d.JournalID  and Account_M.Account= a.Account and a.BusinessUnit=d.BusinessUnit and a.JournalID=@JournalID and a.BusinessUnit=@BusinessUnit order by a.JournalLine";
            SqlDataAdapter da;
            DataSet ds;
            cmdString = "select AuthorName,EmployeeCode,isCorrAuth,(case when AuthorType='P' then 'First Author' when AuthorType='C' then 'CO_Author' else 'Other' end) as AuthorType,RTRIM(Publishcation_Author.DepartmentName)+'-'+RTRIM(Publishcation_Author.InstitutionName) as Department from Publishcation_Author where PaublicationID=@PublicationId and Publishcation_Author.TypeOfEntry='JA' and (MUNonMU='S' or MUNonMU='O')";
            cmd = new SqlCommand(cmdString, con);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add("@PublicationId", SqlDbType.VarChar, 15);
            cmd.Parameters["@PublicationId"].Value = PatientID;

            da = new SqlDataAdapter(cmd);

            ds = new DataSet();
            da.Fill(ds);

            return ds;
        }

        catch (Exception ex)
        {
            log.Error("Inside find author type catch block ");
            log.Error(ex.Message);
            log.Error(ex.StackTrace);
            throw ex;
        }

        finally
        {
            con.Close();
        }
    }

    public DataSet BindGridview(PublishData pub)
    {
        try
        {

            SqlDataAdapter da;
            DataSet ds;
            con = new SqlConnection(str);
            con.Open();
            da = new SqlDataAdapter("SelectUploadToEprintPublicationlist", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            if (pub.PaublicationID != "")
            {
                da.SelectCommand.Parameters.AddWithValue("@PublicationID", pub.PaublicationID);
            }
            else
            {
                da.SelectCommand.Parameters.AddWithValue("@PublicationID", String.Empty);
            }
            if (pub.TypeOfEntry != "")
            {

                da.SelectCommand.Parameters.AddWithValue("@TypeOfEntry", pub.TypeOfEntry);
            }
            else
            {
                da.SelectCommand.Parameters.AddWithValue("@TypeOfEntry", "");
            }

            if (pub.TitleWorkItem != "")
            {
                da.SelectCommand.Parameters.AddWithValue("@TitleWorkItem", pub.TitleWorkItem);
            }
            else
            {
                da.SelectCommand.Parameters.AddWithValue("@TitleWorkItem", "");

            }
            if (pub.LibraryId != "")
            {
                da.SelectCommand.Parameters.AddWithValue("@LibraryId", pub.LibraryId);
            }
            else
            {
                da.SelectCommand.Parameters.AddWithValue("@LibraryId", "");

            }
            ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

        catch (Exception e)
        {
            log.Error(e.Message);
            log.Error(e.StackTrace);
            throw e;
        }
        finally
        {
            cmd.Dispose();
            con.Close();
            cmd.Dispose();
        }
    }

    public bool checkPredatoryJournal(string p, string year)
    {
        log.Debug("Inside checkPredatoryJouranl function-to check Predatory issn: ISSN: " + p + "Year: " + year);
        bool result = false;
        try
        {
            con.Open();
            cmd = new SqlCommand("CheckPredatoryJournal", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@PubJournalID", SqlDbType.VarChar, 50);
            cmd.Parameters["@PubJournalID"].Value = p;
            cmd.Parameters.Add("@PubLicationYear", SqlDbType.VarChar, 4);
            cmd.Parameters["@PubLicationYear"].Value = year;
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.HasRows)
            {
                result = true;
                log.Info("Inside checkPredatoryJouranl function-the ISSN : " + p + " is predatory journal");

            }
            else
            {
                result = false;
            }

            return result;
        }
        catch (Exception ex)
        {
            log.Error("Inside - checkPredatoryJouranl catch block ISSN : " + p + "Year: " + year);
            log.Error(ex.Message);
            log.Error(ex.StackTrace);
            throw ex;
        }

        finally
        {
            con.Close();
        }
    }

    public string checkIncentivePointStatus(string Pid, string TypeEntry)
    {
        log.Debug("Inside checkIncentivePointStatus function- of Publication ID " + Pid + "Type of Entry: " + TypeEntry);
        string status = null;
        try
        {
            con.Open();
            cmd = new SqlCommand("Incentive_SelectIncentivePointStatus", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@PublicationID", SqlDbType.VarChar, 10);
            cmd.Parameters["@PublicationID"].Value = Pid;
            cmd.Parameters.Add("@EntryType", SqlDbType.VarChar, 2);
            cmd.Parameters["@EntryType"].Value = TypeEntry;
            SqlDataReader sdr = cmd.ExecuteReader();

            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    if (!Convert.IsDBNull(sdr["IncentivePointStatus"]))
                    {
                        status = (string)sdr["IncentivePointStatus"];
                    }

                }
                log.Info("Inside checkIncentivePointStatus function-of Publication ID " + Pid + "Type of Entry: " + TypeEntry + " Incetive Point Status: " + status);

            }
            return status;
        }
        catch (Exception ex)
        {
            log.Info("Inside checkIncentivePointStatus catch block Publication ID " + Pid + "Type of Entry: " + TypeEntry + " Incetive Point Status: " + status);
            log.Error(ex.Message);
            log.Error(ex.StackTrace);
            throw ex;
        }

        finally
        {
            con.Close();
        }
    }

    public int DeletePublication(PublishData v)
    {
        log.Debug("Inside DeletePublication function- of Publication ID " + v.PaublicationID + "Type of Entry: " + v.TypeOfEntry);
        int result = 0, result1 = 0;
        con = new SqlConnection(str);
        con.Open();
        transaction = con.BeginTransaction();

        try
        {
            cmd = new SqlCommand("DeletePublications", con, transaction);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@PublicationID", v.PaublicationID);
            cmd.Parameters.Add("@TypeOfEntry", v.TypeOfEntry);

            result = cmd.ExecuteNonQuery();
            if (result > 1)
            {
                cmd = new SqlCommand("InsertPublicationDeleteTracker", con, transaction);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@PublicationID", v.PaublicationID);
                cmd.Parameters.Add("@TypeOfEntry", v.TypeOfEntry);
                cmd.Parameters.Add("@MUCategorization", v.MUCategorization);
                cmd.Parameters.Add("@TitleWorkItem", v.TitleWorkItem);
                cmd.Parameters.Add("@DeletedBy", v.DeletedBy);
                cmd.Parameters.Add("@DeletedDate", DateTime.Now);
                cmd.Parameters.Add("@Remarks", v.RemarksFeedback);
            }
            result1 = cmd.ExecuteNonQuery();
            transaction.Commit();
            log.Info("Publication  Deleted  Sucessfully" + v.PaublicationID + "Type Of Entry" + v.TypeOfEntry);
            log.Info("Publication Delete : User Name :" + HttpContext.Current.Session["UserName"] + "Role :" + HttpContext.Current.Session["RoleName"]);

            return result1;
        }
        catch (Exception ex)
        {
            transaction.Rollback();
            log.Info("Inside DeletePublication catch block Publication ID " + v.PaublicationID + "Type of Entry: " + v.TypeOfEntry);
            log.Error(ex.Message);
            log.Error(ex.StackTrace);
            throw ex;
        }

        finally
        {
            con.Close();
        }
    }

    public JournalData JournalYearwiseCheck(JournalData JournalValueObj)
    {
        log.Debug("Inside JournalYearwiseCheck function ");
        int result = 0;
        con.Open();
        try
        {
            cmdString = "Select * from Journal_Year_Map where Id=@Id and Year=@Year";
            cmd = new SqlCommand(cmdString, con, transaction);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@Id", JournalValueObj.JournalID);
            cmd.Parameters.AddWithValue("@Year", JournalValueObj.year);
            SqlDataReader sdr = cmd.ExecuteReader();
            JournalData j = new JournalData();
            while (sdr.Read())
            {
                if (sdr.HasRows)
                {

                    if (!Convert.IsDBNull(sdr["Id"]))
                    {
                        j.jid = (string)sdr["Id"];
                    }

                }
            }
            return j;
        }
        catch (Exception ex)
        {
            log.Error("Inside Journal_DataObject-JournalYearwiseCheck catch block ");
            log.Error(ex.Message);
            log.Error(ex.StackTrace);
            throw (ex);
        }
        finally
        {
            cmd.Dispose();
            con.Close();
            cmd.Dispose();
        }
    }

    public int insertPublishEntryRDC(PublishData j, PublishData[] jd, ArrayList listIndexAgency)
    {
        log.Debug("Inside insertPublishEntryRDC function to of Publication ID:" + j.PaublicationID + "Type Of Entry: " + j.TypeOfEntry);
        int result = 0, result1 = 0, seed = 0;
        string seedFinal = "";
        con = new SqlConnection(str);
        con.Open();
        transaction = con.BeginTransaction();
        try
        {

            cmd = new SqlCommand("Pub_SelectPublicationSeed", con, transaction);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Type", j.TypeOfEntry);
            seed = (int)cmd.ExecuteScalar();
            string seedStr = seed.ToString();
            int seedlen = seedStr.Length;
            int idlen = Convert.ToInt32(ConfigurationManager.AppSettings["PublicationIdLen"]);
            string pre = "";
            for (int i = 0; i < idlen - seedlen; i++)
            {
                string z = "0";
                pre = pre + z;
            }
            seedFinal = pre + seed.ToString();
            HttpContext.Current.Session["Pubseed"] = seedFinal;

            cmd = new SqlCommand("InsertPublicationEntryRDC", con, transaction);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PaublicationID", seedFinal);
            cmd.Parameters.AddWithValue("@TypeOfEntry", j.TypeOfEntry);
            cmd.Parameters.AddWithValue("@MUCategorization", j.MUCategorization);
            cmd.Parameters.AddWithValue("@TitleWorkItem", j.TitleWorkItem);
            cmd.Parameters.AddWithValue("@PubJournalID", j.PublisherOfJournal);
            cmd.Parameters.AddWithValue("@PubJournalName", j.PublisherOfJournalName);
            cmd.Parameters.AddWithValue("@JAVolume", j.JAVolume);
            cmd.Parameters.AddWithValue("@PublishJAMonth", j.PublishJAMonth);
            cmd.Parameters.AddWithValue("@PublishJAYear", j.PublishJAYear);
            cmd.Parameters.AddWithValue("@PublicationDate", j.PublishDate);
            if (j.PageFrom != "")
            {
                cmd.Parameters.AddWithValue("@PageFrom", j.PageFrom);
                cmd.Parameters.AddWithValue("@PageTo", j.PageTo);
            }
            else
            {
                string pagefrom = "PF" + seedFinal;
                string pageto = "PT" + seedFinal;
                cmd.Parameters.AddWithValue("@PageTo", DBNull.Value);
                cmd.Parameters.AddWithValue("@PageFrom", DBNull.Value);
            }
            if (j.Indexed != null)
            {
                cmd.Parameters.AddWithValue("@Indexed", j.Indexed);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Indexed", DBNull.Value);
            }
            cmd.Parameters.AddWithValue("@Issue", j.Issue);
            cmd.Parameters.AddWithValue("@Publicationtype", j.Publicationtype);

            if (j.ImpactFactor != "")
            {
                cmd.Parameters.AddWithValue("@ImpactFactor", j.ImpactFactor);
            }
            else
            {
                cmd.Parameters.AddWithValue("@ImpactFactor", DBNull.Value);
            }
            if (j.ImpactFactor5 != "")
            {

                cmd.Parameters.AddWithValue("@fiveImpfact", j.ImpactFactor5);
            }
            else
            {
                cmd.Parameters.AddWithValue("@fiveImpfact", DBNull.Value);
            }
            if (j.IFApplicableYear != 0)
            {
                cmd.Parameters.AddWithValue("@IFApplicableYear", j.IFApplicableYear);
            }
            else
            {
                cmd.Parameters.AddWithValue("@IFApplicableYear", DBNull.Value);
            }

            //if (j.url != "")
            //{

            //    cmd.Parameters.AddWithValue("@URL", j.url);
            //}
            //else
            //{
            //    cmd.Parameters.AddWithValue("@URL", DBNull.Value);
            //}

            cmd.Parameters.AddWithValue("@DOINum", j.DOINum);
            cmd.Parameters.AddWithValue("@Keywords", j.Keywords);
            cmd.Parameters.AddWithValue("@Abstract", j.Abstract);
            cmd.Parameters.AddWithValue("@Status", j.Status);
            cmd.Parameters.AddWithValue("@isERF", j.isERF);
            if (j.uploadEPrint != "")
            {
                cmd.Parameters.AddWithValue("@uploadEPrint", j.uploadEPrint);
            }
            else
            {
                cmd.Parameters.AddWithValue("@uploadEPrint", j.uploadEPrint);
            }

            if (j.EprintURL != "")
            {
                cmd.Parameters.AddWithValue("@EprintURL", j.EprintURL);
            }
            else
            {
                cmd.Parameters.AddWithValue("@EprintURL", DBNull.Value);
            }
            cmd.Parameters.AddWithValue("@SupervisorID", j.SupervisorID);
            if (j.LibraryId != null)
            {
                cmd.Parameters.AddWithValue("@LibraryId", j.LibraryId);
            }
            else
            {
                cmd.Parameters.AddWithValue("@LibraryId", DBNull.Value);
            }

            cmd.Parameters.AddWithValue("@AutoApproved", j.AutoAppoval);
            cmd.Parameters.AddWithValue("@CreatedBy", j.CreatedBy);
            if (j.CreatedDate != null)
            {
                cmd.Parameters.AddWithValue("@CreatedDate", j.CreatedDate);
            }
            else
            {
                cmd.Parameters.AddWithValue("@CreatedDate", DBNull.Value);
            }
            cmd.Parameters.AddWithValue("@Institution", j.InstUser);
            cmd.Parameters.AddWithValue("@Department", j.DeptUser);
            cmd.Parameters.AddWithValue("@Institutions", j.AppendInstitutionNames);
            //if (j.CitationUrl != "")
            //{
            //    cmd.Parameters.AddWithValue("@CitationUrl", j.CitationUrl);
            //}
            //else
            //{
            //    cmd.Parameters.AddWithValue("@CitationUrl", DBNull.Value);
            //}

            if (j.AutoAppoval == "Y")
            {
                if (j.ApprovedBy != null)
                {
                    cmd.Parameters.AddWithValue("@ApprovedBy", j.ApprovedBy);
                    cmd.Parameters.AddWithValue("@ApprovedDate", DateTime.Now);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@ApprovedBy", DBNull.Value);
                    cmd.Parameters.AddWithValue("@ApprovedDate", DBNull.Value);
                }
            }
            else
            {
                cmd.Parameters.AddWithValue("@ApprovedBy", DBNull.Value);
                cmd.Parameters.AddWithValue("@ApprovedDate", DBNull.Value);
            }
            cmd.Parameters.AddWithValue("@IsStudentAuthor", j.IsStudentAuthor);
            if (j.IncentivePointSatatus != null)
            {
                cmd.Parameters.AddWithValue("@IncentivePointStatus", j.IncentivePointSatatus);
            }
            else
            {
                cmd.Parameters.AddWithValue("@IncentivePointStatus", DBNull.Value);
            }
            cmd.Parameters.AddWithValue("@EntryType", j.EntryType);
            result = cmd.ExecuteNonQuery();
            if (result >= 1)
            {
                for (int i = 0; i < jd.Length; i++)
                {
                    cmd = new SqlCommand("InsertPublicationAuthor", con, transaction);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PaublicationID", seedFinal);
                    cmd.Parameters.AddWithValue("@TypeOfEntry", j.TypeOfEntry);
                    cmd.Parameters.AddWithValue("@PublicationLine", i + 1);
                    cmd.Parameters.AddWithValue("@AuthorName", jd[i].AuthorName);
                    cmd.Parameters.AddWithValue("@MUNonMU", jd[i].MUNonMU);
                    cmd.Parameters.AddWithValue("@EmployeeCode", jd[i].EmployeeCode);
                    cmd.Parameters.AddWithValue("@Institution", jd[i].Institution);
                    cmd.Parameters.AddWithValue("@Department", jd[i].Department);
                    cmd.Parameters.AddWithValue("@InstitutionName", jd[i].InstitutionName);
                    cmd.Parameters.AddWithValue("@DepartmentName", jd[i].DepartmentName);
                    cmd.Parameters.AddWithValue("@AuthorType", jd[i].AuthorType);
                    cmd.Parameters.AddWithValue("@isCorrAuth", jd[i].isCorrAuth);
                    cmd.Parameters.AddWithValue("@NameInJournal", jd[i].NameInJournal);
                    cmd.Parameters.AddWithValue("@EmailId", jd[i].EmailId);
                    cmd.Parameters.AddWithValue("@NationalInternational", jd[i].NationalInternationl);
                    cmd.Parameters.AddWithValue("@Continent", jd[i].continental);
                    cmd.Parameters.AddWithValue("@IsPresenter", DBNull.Value);

                    cmd.Parameters.AddWithValue("@HasAttended", DBNull.Value);
                    cmd.Parameters.AddWithValue("@CreditPoint", DBNull.Value);
                    result1 = cmd.ExecuteNonQuery();
                }
            }
            if (j.TypeOfEntry == "JA")
            {
                if ((Convert.ToInt32(j.PublishJAYear) >= 2018) && (j.PublishJAMonth >= 7))
                {
                    int PublishJAYear = Convert.ToInt32(j.PublishJAYear);
                    int PublishJAMonth = Convert.ToInt32(j.PublishJAMonth);
                    cmdString = "SelectQuartileApplicableYearWise";
                    cmd = new SqlCommand(cmdString, con, transaction);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", j.PublisherOfJournal);
                    cmd.Parameters.AddWithValue("@PublishJAYear", PublishJAYear);
                    cmd.Parameters.AddWithValue("@PublishJAMonth", PublishJAMonth);
                    cmd.Parameters.AddWithValue("@QuartileStartMonth", j.Quartilefrommonth);
                    cmd.Parameters.AddWithValue("@QuartileEndMonth", j.QuartileTomonth);

                    SqlDataReader sdr = cmd.ExecuteReader();

                    string Quartile = "NA";

                    while (sdr.Read())
                    {
                        if (!Convert.IsDBNull(sdr["Quartile"]))
                        {
                            Quartile = (string)sdr["Quartile"];
                        }
                        else
                        {
                            Quartile = "NA";
                        }

                    }
                    sdr.Close();

                    cmdString = "Update Publication set QuartileOnEntry=@Quartile where PublicationID=@PaublicationID and TypeOfEntry=@TypeOfEntry  and MUCategorization=@MUCategorization";

                    cmd = new SqlCommand(cmdString, con, transaction);
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue("@PaublicationID", seedFinal);
                    cmd.Parameters.AddWithValue("@TypeOfEntry", j.TypeOfEntry);
                    cmd.Parameters.AddWithValue("@MUCategorization", j.MUCategorization);
                    cmd.Parameters.AddWithValue("@Quartile", Quartile);


                    result = cmd.ExecuteNonQuery();

                }
                else
                    if ((Convert.ToInt32(j.PublishJAYear) >= 2019) && (j.PublishJAMonth >= 1))
                    {
                        int PublishJAYear = Convert.ToInt32(j.PublishJAYear);
                        int PublishJAMonth = Convert.ToInt32(j.PublishJAMonth);
                        cmdString = "SelectQuartileApplicableYearWise";
                        cmd = new SqlCommand(cmdString, con, transaction);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID", j.PublisherOfJournal);
                        cmd.Parameters.AddWithValue("@PublishJAYear", PublishJAYear);
                        cmd.Parameters.AddWithValue("@PublishJAMonth", PublishJAMonth);
                        cmd.Parameters.AddWithValue("@QuartileStartMonth", j.Quartilefrommonth);
                        cmd.Parameters.AddWithValue("@QuartileEndMonth", j.QuartileTomonth);

                        SqlDataReader sdr = cmd.ExecuteReader();

                        string Quartile = "NA";

                        while (sdr.Read())
                        {
                            if (!Convert.IsDBNull(sdr["Quartile"]))
                            {
                                Quartile = (string)sdr["Quartile"];
                            }
                            else
                            {
                                Quartile = "NA";
                            }

                        }
                        sdr.Close();

                        cmdString = "Update Publication set QuartileOnEntry=@Quartile where PublicationID=@PaublicationID and TypeOfEntry=@TypeOfEntry  and MUCategorization=@MUCategorization";

                        cmd = new SqlCommand(cmdString, con, transaction);
                        cmd.CommandType = CommandType.Text;

                        cmd.Parameters.AddWithValue("@PaublicationID", seedFinal);
                        cmd.Parameters.AddWithValue("@TypeOfEntry", j.TypeOfEntry);
                        cmd.Parameters.AddWithValue("@MUCategorization", j.MUCategorization);
                        cmd.Parameters.AddWithValue("@Quartile", Quartile);


                        result = cmd.ExecuteNonQuery();

                    }

            }
            if (listIndexAgency.Count > 0)
            {
                for (int i = 0; i < listIndexAgency.Count; i++)
                {
                    cmd = new SqlCommand("InsertPublicationIndexAgency", con, transaction);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PublicationId", seedFinal);
                    cmd.Parameters.AddWithValue("@Type", j.TypeOfEntry);
                    cmd.Parameters.AddWithValue("@IndexAgency", listIndexAgency[i]);
                    result = cmd.ExecuteNonQuery();

                }
            }


            cmd = new SqlCommand("Pub_InsertReviewTracker", con, transaction);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Publish_Id", seedFinal);
            cmd.Parameters.AddWithValue("@Type", j.TypeOfEntry);
            cmd.Parameters.AddWithValue("@ApprovedStatus", j.Status);
            cmd.Parameters.AddWithValue("@Remark", "");
            cmd.Parameters.AddWithValue("@EnterdBy", j.CreatedBy);
            cmd.Parameters.AddWithValue("@dateM", DateTime.Now);
            result = cmd.ExecuteNonQuery();
            transaction.Commit();
            log.Info("Publication  Details saved of Publication ID : " + j.PaublicationID + "Type Of Entry" + j.TypeOfEntry);
            log.Info("Publication Entry By RDC : User Name :" + HttpContext.Current.Session["UserName"] + "Role :" + HttpContext.Current.Session["RoleName"]);
            return result1;
        }

        catch (Exception ex)
        {
            log.Error("Inside Student_GL_DataObject- insertJournalEntry catch block ");
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

    public int UpdatePdfPath(PublishData obj)
    {
        int result = 0, result1 = 0;
        con = new SqlConnection(str);
        con.Open();
        transaction = con.BeginTransaction();
        try
        {
            cmd = new SqlCommand("Pub_UpdatePdfPath", con, transaction);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PublicationID", obj.PaublicationID);
            cmd.Parameters.AddWithValue("@TypeOfEntry", obj.TypeOfEntry);
            cmd.Parameters.AddWithValue("@UploadPDFPath", obj.FilePath);
            result = cmd.ExecuteNonQuery();
            transaction.Commit();
            return result1;
        }

        catch (Exception ex)
        {
            log.Error("Inside UpdatePdfPath catch block ");
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

    public DataSet SelectPublications(PublishData pub_obj)
    {
        try
        {

            SqlDataAdapter da;
            DataSet ds = new DataSet();
            con.Open();
            da = new SqlDataAdapter("Pub_SelectPublications", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@PublicationID", SqlDbType.VarChar, 10).Value = pub_obj.PaublicationID;
            da.SelectCommand.Parameters.Add("@TypeofEntry", SqlDbType.VarChar, 2).Value = pub_obj.TypeOfEntry;
            da.SelectCommand.Parameters.Add("@TitleWorkItem", SqlDbType.VarChar, 200).Value = pub_obj.TitleWorkItem;
            da.SelectCommand.Parameters.Add("@CreatedBy", SqlDbType.VarChar, 10).Value = pub_obj.CreatedBy;


            da.Fill(ds);
            return ds;

        }
        catch (Exception ex)
        {
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

    internal int UpdatePublishEntryRDC(PublishData j, PublishData[] jd, ArrayList listIndexAgency)
    {
        log.Debug("Inside UpdatePublishEntryRDC function to of Publication ID:" + j.PaublicationID + "Type Of Entry: " + j.TypeOfEntry);
        int result = 0, result1 = 0;
        con.Open();
        transaction = con.BeginTransaction();
        try
        {

            cmd = new SqlCommand("UpdatePublicationEntryRDC", con, transaction);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PaublicationID", j.PaublicationID);
            cmd.Parameters.AddWithValue("@TypeOfEntry", j.TypeOfEntry);
            cmd.Parameters.AddWithValue("@MUCategorization", j.MUCategorization);
            cmd.Parameters.AddWithValue("@TitleWorkItem", j.TitleWorkItem);
            cmd.Parameters.AddWithValue("@PubJournalID", j.PublisherOfJournal);
            cmd.Parameters.AddWithValue("@PubJournalName", j.PublisherOfJournalName);
            cmd.Parameters.AddWithValue("@JAVolume", j.JAVolume);
            cmd.Parameters.AddWithValue("@PublishJAMonth", j.PublishJAMonth);
            cmd.Parameters.AddWithValue("@PublishJAYear", j.PublishJAYear);
            cmd.Parameters.AddWithValue("@PublicationDate", j.PublishDate);
            if (j.PageFrom != "")
            {
                cmd.Parameters.AddWithValue("@PageFrom", j.PageFrom);
                cmd.Parameters.AddWithValue("@PageTo", j.PageTo);
            }
            else
            {
                string pagefrom = "PF" + j.PaublicationID;
                string pageto = "PT" + j.PaublicationID;
                cmd.Parameters.AddWithValue("@PageTo", DBNull.Value);
                cmd.Parameters.AddWithValue("@PageFrom", DBNull.Value);
            }
            if (j.Indexed != null)
            {
                cmd.Parameters.AddWithValue("@Indexed", j.Indexed);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Indexed", DBNull.Value);
            }
            cmd.Parameters.AddWithValue("@Issue", j.Issue);
            cmd.Parameters.AddWithValue("@Publicationtype", j.Publicationtype);

            if (j.ImpactFactor != "")
            {
                cmd.Parameters.AddWithValue("@ImpactFactor", j.ImpactFactor);
            }
            else
            {
                cmd.Parameters.AddWithValue("@ImpactFactor", DBNull.Value);
            }
            if (j.ImpactFactor5 != "")
            {

                cmd.Parameters.AddWithValue("@fiveImpfact", j.ImpactFactor5);
            }
            else
            {
                cmd.Parameters.AddWithValue("@fiveImpfact", DBNull.Value);
            }
            if (j.IFApplicableYear != 0)
            {
                cmd.Parameters.AddWithValue("@IFApplicableYear", j.IFApplicableYear);
            }
            else
            {
                cmd.Parameters.AddWithValue("@IFApplicableYear", DBNull.Value);
            }

            //if (j.url != "")
            //{

            //    cmd.Parameters.AddWithValue("@URL", j.url);
            //}
            //else
            //{
            //    cmd.Parameters.AddWithValue("@URL", DBNull.Value);
            //}

            cmd.Parameters.AddWithValue("@DOINum", j.DOINum);
            cmd.Parameters.AddWithValue("@Keywords", j.Keywords);
            cmd.Parameters.AddWithValue("@Abstract", j.Abstract);
            cmd.Parameters.AddWithValue("@Status", j.Status);
            cmd.Parameters.AddWithValue("@isERF", j.isERF);
            if (j.uploadEPrint != "")
            {
                cmd.Parameters.AddWithValue("@uploadEPrint", j.uploadEPrint);
            }
            else
            {
                cmd.Parameters.AddWithValue("@uploadEPrint", j.uploadEPrint);
            }

            if (j.EprintURL != "")
            {
                cmd.Parameters.AddWithValue("@EprintURL", j.EprintURL);
            }
            else
            {
                cmd.Parameters.AddWithValue("@EprintURL", DBNull.Value);
            }
            cmd.Parameters.AddWithValue("@SupervisorID", j.SupervisorID);
            if (j.LibraryId != null)
            {
                cmd.Parameters.AddWithValue("@LibraryId", j.LibraryId);
            }
            else
            {
                cmd.Parameters.AddWithValue("@LibraryId", DBNull.Value);
            }

            cmd.Parameters.AddWithValue("@AutoApproved", j.AutoAppoval);

            cmd.Parameters.AddWithValue("@Institution", j.InstUser);
            cmd.Parameters.AddWithValue("@Department", j.DeptUser);
            cmd.Parameters.AddWithValue("@Institutions", j.AppendInstitutionNames);
            //if (j.CitationUrl != "")
            //{
            //    cmd.Parameters.AddWithValue("@CitationUrl", j.CitationUrl);
            //}
            //else
            //{
            //    cmd.Parameters.AddWithValue("@CitationUrl", DBNull.Value);
            //}


            cmd.Parameters.AddWithValue("@IsStudentAuthor", j.IsStudentAuthor);
            if (j.IncentivePointSatatus != null)
            {
                cmd.Parameters.AddWithValue("@IncentivePointStatus", j.IncentivePointSatatus);
            }
            else
            {
                cmd.Parameters.AddWithValue("@IncentivePointStatus", DBNull.Value);
            }
            if (j.AutoAppoval == "Y")
            {
                if (j.ApprovedBy != null)
                {
                    cmd.Parameters.AddWithValue("@ApprovedBy", j.ApprovedBy);
                    cmd.Parameters.AddWithValue("@ApprovedDate", DateTime.Now);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@ApprovedBy", DBNull.Value);
                    cmd.Parameters.AddWithValue("@ApprovedDate", DBNull.Value);
                }
            }
            else
            {
                cmd.Parameters.AddWithValue("@ApprovedBy", DBNull.Value);
                cmd.Parameters.AddWithValue("@ApprovedDate", DBNull.Value);
            }


            result = cmd.ExecuteNonQuery();
            if (result >= 1)
            {
                for (int i = 0; i < jd.Length; i++)
                {
                    cmd = new SqlCommand("InsertPublicationAuthor", con, transaction);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PaublicationID", j.PaublicationID);
                    cmd.Parameters.AddWithValue("@TypeOfEntry", j.TypeOfEntry);
                    cmd.Parameters.AddWithValue("@PublicationLine", i + 1);
                    cmd.Parameters.AddWithValue("@AuthorName", jd[i].AuthorName);
                    cmd.Parameters.AddWithValue("@MUNonMU", jd[i].MUNonMU);
                    cmd.Parameters.AddWithValue("@EmployeeCode", jd[i].EmployeeCode);
                    cmd.Parameters.AddWithValue("@Institution", jd[i].Institution);
                    cmd.Parameters.AddWithValue("@Department", jd[i].Department);
                    cmd.Parameters.AddWithValue("@InstitutionName", jd[i].InstitutionName);
                    cmd.Parameters.AddWithValue("@DepartmentName", jd[i].DepartmentName);
                    cmd.Parameters.AddWithValue("@AuthorType", jd[i].AuthorType);
                    cmd.Parameters.AddWithValue("@isCorrAuth", jd[i].isCorrAuth);
                    cmd.Parameters.AddWithValue("@NameInJournal", jd[i].NameInJournal);
                    cmd.Parameters.AddWithValue("@EmailId", jd[i].EmailId);
                    cmd.Parameters.AddWithValue("@NationalInternational", jd[i].NationalInternationl);
                    cmd.Parameters.AddWithValue("@Continent", jd[i].continental);
                    cmd.Parameters.AddWithValue("@IsPresenter", DBNull.Value);

                    cmd.Parameters.AddWithValue("@HasAttended", DBNull.Value);
                    cmd.Parameters.AddWithValue("@CreditPoint", DBNull.Value);
                    result1 = cmd.ExecuteNonQuery();
                }
            }
            if (j.TypeOfEntry == "JA")
            {
                if ((Convert.ToInt32(j.PublishJAYear) >= 2018) && (j.PublishJAMonth >= 7))
                {
                    int PublishJAYear = Convert.ToInt32(j.PublishJAYear);
                    int PublishJAMonth = Convert.ToInt32(j.PublishJAMonth);
                    cmdString = "SelectQuartileApplicableYearWise";
                    cmd = new SqlCommand(cmdString, con, transaction);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", j.PublisherOfJournal);
                    cmd.Parameters.AddWithValue("@PublishJAYear", PublishJAYear);
                    cmd.Parameters.AddWithValue("@PublishJAMonth", PublishJAMonth);
                    cmd.Parameters.AddWithValue("@QuartileStartMonth", j.Quartilefrommonth);
                    cmd.Parameters.AddWithValue("@QuartileEndMonth", j.QuartileTomonth);

                    SqlDataReader sdr = cmd.ExecuteReader();

                    string Quartile = "NA";

                    while (sdr.Read())
                    {
                        if (!Convert.IsDBNull(sdr["Quartile"]))
                        {
                            Quartile = (string)sdr["Quartile"];
                        }
                        else
                        {
                            Quartile = "NA";
                        }

                    }
                    sdr.Close();

                    cmdString = "Update Publication set QuartileOnEntry=@Quartile where PublicationID=@PaublicationID and TypeOfEntry=@TypeOfEntry  and MUCategorization=@MUCategorization";

                    cmd = new SqlCommand(cmdString, con, transaction);
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue("@PaublicationID", j.PaublicationID);
                    cmd.Parameters.AddWithValue("@TypeOfEntry", j.TypeOfEntry);
                    cmd.Parameters.AddWithValue("@MUCategorization", j.MUCategorization);
                    cmd.Parameters.AddWithValue("@Quartile", Quartile);


                    result = cmd.ExecuteNonQuery();

                }
                else if ((Convert.ToInt32(j.PublishJAYear) >= 2019) && (j.PublishJAMonth >= 1))
                {
                    int PublishJAYear = Convert.ToInt32(j.PublishJAYear);
                    int PublishJAMonth = Convert.ToInt32(j.PublishJAMonth);
                    cmdString = "SelectQuartileApplicableYearWise";
                    cmd = new SqlCommand(cmdString, con, transaction);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", j.PublisherOfJournal);
                    cmd.Parameters.AddWithValue("@PublishJAYear", PublishJAYear);
                    cmd.Parameters.AddWithValue("@PublishJAMonth", PublishJAMonth);
                    cmd.Parameters.AddWithValue("@QuartileStartMonth", j.Quartilefrommonth);
                    cmd.Parameters.AddWithValue("@QuartileEndMonth", j.QuartileTomonth);

                    SqlDataReader sdr = cmd.ExecuteReader();

                    string Quartile = "NA";

                    while (sdr.Read())
                    {
                        if (!Convert.IsDBNull(sdr["Quartile"]))
                        {
                            Quartile = (string)sdr["Quartile"];
                        }
                        else
                        {
                            Quartile = "NA";
                        }

                    }
                    sdr.Close();

                    cmdString = "Update Publication set QuartileOnEntry=@Quartile where PublicationID=@PaublicationID and TypeOfEntry=@TypeOfEntry  and MUCategorization=@MUCategorization";

                    cmd = new SqlCommand(cmdString, con, transaction);
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue("@PaublicationID", j.PaublicationID);
                    cmd.Parameters.AddWithValue("@TypeOfEntry", j.TypeOfEntry);
                    cmd.Parameters.AddWithValue("@MUCategorization", j.MUCategorization);
                    cmd.Parameters.AddWithValue("@Quartile", Quartile);


                    result = cmd.ExecuteNonQuery();

                }

            }

            if (listIndexAgency.Count > 0)
            {
                for (int i = 0; i < listIndexAgency.Count; i++)
                {
                    cmd = new SqlCommand("InsertPublicationIndexAgency", con, transaction);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PublicationId", j.PaublicationID);
                    cmd.Parameters.AddWithValue("@Type", j.TypeOfEntry);
                    cmd.Parameters.AddWithValue("@IndexAgency", listIndexAgency[i]);
                    result = cmd.ExecuteNonQuery();

                }
            }
            transaction.Commit();
            return result1;
        }

        catch (Exception ex)
        {
            log.Error("Inside Student_GL_DataObject- insertJournalEntry catch block ");
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

    public DataSet getAuthorListRDC(string p1, string p2)
    {
        log.Debug("Inside function getAuthorList");
        try
        {
            SqlDataAdapter da;
            DataSet ds = new DataSet();

            con = new SqlConnection(str);
            con.Open();
            cmdString = " select EmailId from Publishcation_Author where PaublicationID=@PaublicationID and TypeOfEntry=@TypeOfEntry and (MUNonMU<>'M' )";
            da = new SqlDataAdapter(cmdString, con);

            da.SelectCommand.Parameters.Add("@PaublicationID", SqlDbType.VarChar, 10);
            da.SelectCommand.Parameters["@PaublicationID"].Value = p1;
            da.SelectCommand.Parameters.Add("@TypeOfEntry", SqlDbType.VarChar, 5);
            da.SelectCommand.Parameters["@TypeOfEntry"].Value = p2;

            da.SelectCommand.CommandType = CommandType.Text;


            da.Fill(ds);

            return ds;
        }
        catch (Exception e)
        {
            log.Error(e.Message);
            log.Error(e.StackTrace);

            throw e;
        }
        finally
        {
            con.Close();
        }
    }

    public DataSet SelecKeywordBasedAuthors(string keyword)
    {
        log.Debug("Inside function SelecKeywordBasedAuthors");
        try
        {
            SqlDataAdapter da;
            DataSet ds = new DataSet();
            con.Open();
            da = new SqlDataAdapter("SelectKeywordBasedAuthors", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@Keywords", SqlDbType.VarChar, 10);
            da.SelectCommand.Parameters["@Keywords"].Value = keyword;
            da.Fill(ds);
            return ds;
        }
        catch (Exception e)
        {
            log.Error(e.Message);
            log.Error(e.StackTrace);
            throw e;
        }
        finally
        {
            con.Close();
        }
    }

    public DataSet SelecEmpCodeBasedAuthors(string empid, string orcid, string scopusid)
    {
        log.Debug("Inside function SelecEmpCodeBasedAuthors");
        try
        {
            SqlDataAdapter da;
            DataSet ds = new DataSet();
            con.Open();
            da = new SqlDataAdapter("SelectEmpCodeBasedAuthors", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@User_Id", SqlDbType.VarChar, 12);
            da.SelectCommand.Parameters["@User_Id"].Value = empid;
            da.SelectCommand.Parameters.Add("@ORCID", SqlDbType.VarChar);
            da.SelectCommand.Parameters["@ORCID"].Value = orcid;
            da.SelectCommand.Parameters.Add("@ScopusID", SqlDbType.VarChar);
            da.SelectCommand.Parameters["@ScopusID"].Value = scopusid;
            da.Fill(ds);
            return ds;
        }
        catch (Exception e)
        {
            log.Error(e.Message);
            log.Error(e.StackTrace);
            throw e;
        }
        finally
        {
            con.Close();
        }
    }


    //Ashwini
    public string SelectAuthorToMailid(string pubid, string typeofentry)
    {
        EmailDetails details = new EmailDetails();
        SqlDataReader sdr = null;
        try
        {
            cmdString = "select EmailId from User_M where User_Id in(select CreatedBy from Publication where PublicationID=@PublicationID and TypeOfEntry=@TypeOfEntry) ";
            con.Open();
            cmd = new SqlCommand(cmdString, con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@PublicationID", pubid);
            cmd.Parameters.AddWithValue("@TypeOfEntry", typeofentry);
            sdr = cmd.ExecuteReader();
            IncentivePoint j = new IncentivePoint();

            while (sdr.Read())
            {
                if (!Convert.IsDBNull(sdr["EmailId"]))
                {
                    details.ToEmail = (string)sdr["EmailId"];
                }
            }
            return details.ToEmail;
        }

        catch (Exception ex)
        {
            log.Debug("Inside  SelectAuthorToMailid function, ID: " + pubid + "Type Of Entry: " + typeofentry);
            log.Error(ex.Message);
            log.Error(ex.StackTrace);
            throw ex;
        }

        finally
        {
            sdr.Close();
            con.Close();
        }
    }

    public string SelectAuthorCCMailid(string employeeid, string pubid, string typeofentry)
    {
        ArrayList list = new ArrayList();
        EmailDetails details = new EmailDetails();
        string emailid = "";
        SqlDataReader sdr = null;
        con.Open();
        try
        {
            cmd = new SqlCommand("Incentive_SelectAuthorCCMailid", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EmployeeCode", employeeid);
            cmd.Parameters.AddWithValue("@PublicationID", pubid);
            cmd.Parameters.AddWithValue("@TypeOfEntry", typeofentry);
            sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                if (!Convert.IsDBNull(sdr["EmailId"]))
                {
                    emailid = (string)sdr["EmailId"];
                }

            }
            return emailid;
        }

        catch (Exception ex)
        {
            log.Error("Inside SelectAuthorCCMailid function,  ID: " + employeeid);
            log.Error(ex.Message);
            log.Error(ex.StackTrace);
            throw ex;
        }

        finally
        {
            sdr.Close();
            con.Close();
        }
    }

    public DataSet BindGridviewFileUpload(PublishData obj)
    {
        try
        {

            SqlDataAdapter da;
            DataSet ds;
            con = new SqlConnection(str);
            con.Open();
            da = new SqlDataAdapter("SelectPublicationToUploadDocument", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            if (obj.PaublicationID != "")
            {
                da.SelectCommand.Parameters.AddWithValue("@PublicationID", obj.PaublicationID);
            }
            else
            {
                da.SelectCommand.Parameters.AddWithValue("@PublicationID", String.Empty);
            }

            if (obj.TitleWorkItem != "")
            {
                da.SelectCommand.Parameters.AddWithValue("@TitleWorkItem", obj.TitleWorkItem);
            }
            else
            {
                da.SelectCommand.Parameters.AddWithValue("@TitleWorkItem", String.Empty);

            }
            if (obj.TypeOfEntry != "")
            {
                da.SelectCommand.Parameters.AddWithValue("@TypeOfEntry", obj.TypeOfEntry);
            }
            else
            {
                da.SelectCommand.Parameters.AddWithValue("@TypeOfEntry", String.Empty);

            }
            da.SelectCommand.Parameters.AddWithValue("@CreatedBy", obj.CreatedBy);
            ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

        catch (Exception e)
        {
            log.Error(e.Message);
            log.Error(e.StackTrace);
            throw e;
        }
        finally
        {
            cmd.Dispose();
            con.Close();
            cmd.Dispose();
        }
    }


    public DataSet BindGrid(string p1, string p2, string createdby)
    {
        try
        {

            SqlDataAdapter da;
            DataSet ds;
            con = new SqlConnection(str);
            con.Open();
            da = new SqlDataAdapter("SelectPublications", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;

            da.SelectCommand.Parameters.AddWithValue("@PublicationID", p1);
            da.SelectCommand.Parameters.AddWithValue("@TypeOfEntry", p2);
            da.SelectCommand.Parameters.AddWithValue("@CreatedBy", createdby);
            ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

        catch (Exception e)
        {
            log.Error(e.Message);
            log.Error(e.StackTrace);
            throw e;
        }
        finally
        {
            cmd.Dispose();
            con.Close();
            cmd.Dispose();
        }
    }
    public User findusername(string UserId)
    {
        try
        {
            //cmdString = "select * from User_M where User_Id='" + UserId + "' ";
            cmdString = "select * from User_M where User_Id=@UserId";


            con = new SqlConnection(str);
            con.Open();
            cmd = new SqlCommand(cmdString, con);



            User u = new User();
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@UserId", UserId);



            SqlDataReader sdr = cmd.ExecuteReader();

            while (sdr.Read())
            {

                if (!Convert.IsDBNull(sdr["Prefix"]))
                {
                    u.UserNamePrefix = (string)sdr["Prefix"];
                }
                else
                {
                    u.UserNamePrefix = "";
                }
                if (!Convert.IsDBNull(sdr["FirstName"]))
                {
                    u.UserFirstName = (string)sdr["FirstName"];
                }
                else
                {
                    u.UserFirstName = "";
                }
                if (!Convert.IsDBNull(sdr["MiddleName"]))
                {
                    u.UserMiddleName = (string)sdr["MiddleName"];
                }
                else
                {
                    u.UserMiddleName = "";
                }
                if (!Convert.IsDBNull(sdr["LastName"]))
                {
                    u.UserLastName = (string)sdr["LastName"];
                }
                else
                {
                    u.UserLastName = "";
                }


            }

            return u;
        }
        catch (Exception ex)
        {
            log.Error("Inside - findusername catch block ");
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
    public User fnfindProject(string ProjectID, string ProjectUnit)
    {
        log.Debug("inside function fnfindProject ");
        try
        {
            //cmdString = "select o.BoX_ID,w.PatientName,w.F_G_SName, TreatmentStartDate,o.Status, TreatmentEndDate,PatientComplianceVisits from OD_Patient_Data o ,Waiting_List w where w.WLID=o.WLID and BoX_ID=@PatientID";
            //  con = new SqlConnection(str);
            con.Open();
            cmd = new SqlCommand("fnfindProjectDetails", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ProjectID", SqlDbType.VarChar, 12);
            cmd.Parameters["@ProjectID"].Value = ProjectID;
            cmd.Parameters.Add("@ProjectUnit", SqlDbType.VarChar, 12);
            cmd.Parameters["@ProjectUnit"].Value = ProjectUnit;
            SqlDataReader reader = cmd.ExecuteReader();
            // PatientPOD p = new PatientPOD();
            User p = new User();
            while (reader.Read())
            {
                p.Title = (string)reader["Title"];
                p.AppliedDate = (DateTime)reader["AppliedDate"];
                p.UTN = (string)reader["UTN"];
                if (!Convert.IsDBNull(reader["AppliedAmount"]))
                {
                    p.AppliedAmount = Convert.ToDouble(reader["AppliedAmount"]);
                }
                else
                {
                    p.AppliedAmount = 0.0;
                }
                //p.AppliedAmount = Convert.ToDouble(reader["AppliedAmount"]);
                p.Created_Date = (DateTime)reader["CreatedDate"];
                if (!Convert.IsDBNull(reader["DurationOfProject"]))
                {
                    p.DurationOfProject = Convert.ToInt32(reader["DurationOfProject"]);
                }
                else
                {
                    p.DurationOfProject = 0;
                }
                p.AgencyId = (string)reader["FundingAgency"];

            }
            return p;
        }
        catch (Exception e)
        {
            log.Debug("Error: Inside catch block of fnfindProject");
            log.Error("Error msg:" + e);
            log.Error("Stack trace:" + e.StackTrace);
            e.ToString();
            return null;
        }
        finally
        {
            con.Close();
        }
    }
    public User ProjectCount(string AuthorID, string ProjectUnit)
    {
        log.Debug("inside function ProjectCount ");
        try
        {
            //cmdString = "select o.BoX_ID,w.PatientName,w.F_G_SName, TreatmentStartDate,o.Status, TreatmentEndDate,PatientComplianceVisits from OD_Patient_Data o ,Waiting_List w where w.WLID=o.WLID and BoX_ID=@PatientID";
            //  con = new SqlConnection(str);
            con.Open();
            cmd = new SqlCommand("ProjectAuthorCount", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ProjectID", SqlDbType.VarChar, 12);
            cmd.Parameters["@ProjectID"].Value = AuthorID;
            cmd.Parameters.Add("@ProjectUnit", SqlDbType.VarChar, 12);
            cmd.Parameters["@ProjectUnit"].Value = ProjectUnit;
            SqlDataReader reader = cmd.ExecuteReader();
            // PatientPOD p = new PatientPOD();
            User p = new User();
            while (reader.Read())
            {

                p.Author_Count = (int)reader["ID"];

            }
            return p;
        }
        catch (Exception e)
        {
            log.Debug("Error: Inside catch block of ProjectCount");
            log.Error("Error msg:" + e);
            log.Error("Stack trace:" + e.StackTrace);
            e.ToString();
            return null;
        }
        finally
        {
            con.Close();
        }
    }

    public DataSet fnInvestigatorsdetail(string ProjectID, string ProjectUnit)
    {
        con = new SqlConnection(str);
        con.Open();
        try
        {
            //  cmdString = "select  convert(numeric(13,2),Amount) as Amount ,DebitCredit as DrCr, a.account as account, a.oprunit as oprUnit,a.DeptID as dept,a.AffilateBU as affiliate,openitem as openItem, a.linenarration as lineNar, ADesc as accountName, OprUnit_M.OUnitName as oprUnitName,Department_M.DeptName,OpenItem_M.EmpName,a.JournalLine,EmpName as openItemName from GL_Accounting_T a  LEFT OUTER JOIN OprUnit_M ON a.OPRUNIT = OprUnit_M.OPRUNIT LEFT OUTER JOIN Department_M ON a.DeptID = Department_M.DeptCode LEFT OUTER JOIN OpenItem_M ON a.openitem = OpenItem_M.EmpCode, GL d , Account_M where a.JournalID=d.JournalID  and Account_M.Account= a.Account and a.BusinessUnit=d.BusinessUnit and a.JournalID=@JournalID and a.BusinessUnit=@BusinessUnit order by a.JournalLine";
            SqlDataAdapter da;
            DataSet ds;
            cmdString = "select InvestigatorName,InstitutionName,InvestigatorType from Projectnvestigator where ID=@ProjectID and ProjectUnit=@ProjectUnit and (MUNonMU='M' OR MUNonMU='N') order by InvestigatorType desc";
            cmd = new SqlCommand(cmdString, con);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add("@ProjectID", SqlDbType.VarChar, 10);
            cmd.Parameters["@ProjectID"].Value = ProjectID;
            cmd.Parameters.Add("@ProjectUnit", SqlDbType.VarChar, 5);
            cmd.Parameters["@ProjectUnit"].Value = ProjectUnit;

            da = new SqlDataAdapter(cmd);

            ds = new DataSet();
            da.Fill(ds);

            return ds;
        }

        catch (Exception ex)
        {
            log.Error("Inside find author type catch block ");
            log.Error(ex.Message);
            log.Error(ex.StackTrace);
            throw ex;
        }

        finally
        {
            con.Close();
        }
    }



    public string FindAgencyName(string p)
    {
        log.Debug("Inside FindAgencyName function of agency ID: " + p);
        con = new SqlConnection(str);
        con.Open();

        try
        {
            cmdString = "Select FundingAgencyName from ProjectFundingAgency_M where FundingAgencyId=@Id";
            cmd = new SqlCommand(cmdString, con, transaction);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@Id", p);
            SqlDataReader sdr = cmd.ExecuteReader();
            User p1 = new User();
            while (sdr.Read())
            {
                if (sdr.HasRows)
                {
                    if (!Convert.IsDBNull(sdr["FundingAgencyName"]))
                    {
                        p1.AgencyName = sdr["FundingAgencyName"].ToString();
                    }
                }
            }
            return p1.AgencyName;
        }
        catch (Exception ex)
        {
            log.Error("Inside FindAgencyName catch block of agency ID: " + p);
            log.Error(ex.Message);
            log.Error(ex.StackTrace);
            throw (ex);
        }
        finally
        {
            cmd.Dispose();
            con.Close();
            cmd.Dispose();
        }
    }


    public GrantData fnGrantData(string pid, string Unit, string user)
    {
        log.Debug("Inside fnGrantData function of Project ID: " + pid);
        con = new SqlConnection(str);
        con.Open();

        try
        {
            cmdString = "Select p.CreatedBy,p1.MUNonMU,p1.InvestigatorType from Project p,Projectnvestigator p1 where p.ID=@ID and p.ProjectUnit=@ProjectUnit and p1.EmployeeCode=@EmployeeCode and p.ID=p1.ID and p.ProjectUnit=p1.ProjectUnit";
            cmd = new SqlCommand(cmdString, con, transaction);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@ID", pid);
            cmd.Parameters.AddWithValue("@ProjectUnit", Unit);
            cmd.Parameters.AddWithValue("@EmployeeCode", user);
            SqlDataReader sdr = cmd.ExecuteReader();
            GrantData g1 = new GrantData();
            while (sdr.Read())
            {
                if (sdr.HasRows)
                {
                    if (!Convert.IsDBNull(sdr["MUNonMU"]))
                    {
                        g1.MUNonMU = sdr["MUNonMU"].ToString();
                    }
                    if (!Convert.IsDBNull(sdr["InvestigatorType"]))
                    {
                        g1.AuthorType = sdr["InvestigatorType"].ToString();

                    }
                    if (!Convert.IsDBNull(sdr["CreatedBy"]))
                    {
                        g1.CreatedBy = sdr["CreatedBy"].ToString();
                    }
                }
            }
            return g1;
        }
        catch (Exception ex)
        {
            log.Error("Inside fnGrantData catch block of Project ID: " + pid);
            log.Error(ex.Message);
            log.Error(ex.StackTrace);
            throw (ex);
        }
        finally
        {
            cmd.Dispose();
            con.Close();
            cmd.Dispose();
        }
    }

    public int fnInsertResearchData(FileUpload[] f, int length)
    {
        log.Debug("Inside Journal_DataObject- insertResearchData function ");

        int result = 0;

        con = new SqlConnection(str);
        con.Open();
        transaction = con.BeginTransaction();
        try
        {


            for (int i = 0; i < length; i++)
            {
                cmd = new SqlCommand("insertResearchData", con, transaction);
                cmd.CommandType = CommandType.StoredProcedure;

                if (f[i].EmployeeCode == null)
                {


                }
                else if (f[i].EmployeeCode != null)
                {
                    cmd.Parameters.AddWithValue("@UserId", f[i].EmployeeCode);
                    if (f[i].Domain != "&nbsp")
                    {
                        cmd.Parameters.AddWithValue("@Domain", HttpUtility.HtmlDecode(f[i].Domain));
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Domain", ";");
                    }
                    if (f[i].Area != "&nbsp")
                    {

                        cmd.Parameters.AddWithValue("@Area", HttpUtility.HtmlDecode(f[i].Area));
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Area", ";");
                    }

                    result = cmd.ExecuteNonQuery();
                }

            }

            transaction.Commit();
            return result;
        }
        catch (Exception ex)
        {
            log.Error("Inside Journal_DataObject- insertResearchData catch block ");
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

    public int fnUpdateResearchData(string userid, FileUpload f, FileUpload[] JD, User u)
    {
        log.Debug("Inside Journal_DataObject- fnUpdateResearchData function ");

        int result = 0;
        int result1 = 0;
        con = new SqlConnection(str);
        con.Open();
        string arealist = "";
        string domainval = "";
        transaction = con.BeginTransaction();
        try
        {

            //cmdString = " Update User_M set ORCID=@ORCID,ScopusID=@ScopusID,ScopusID2=@ScopusID2,ScopusID3=@ScopusID3 where User_Id='" + userid + "'";
            cmdString = " Update User_M set ORCID=@ORCID,ScopusID=@ScopusID,ScopusID2=@ScopusID2,ScopusID3=@ScopusID3 where User_Id=@userid";

            cmd = new SqlCommand(cmdString, con, transaction);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("@ORCID", HttpUtility.HtmlDecode(u.orcid.ToString()));
            cmd.Parameters.AddWithValue("@ScopusID", HttpUtility.HtmlDecode(u.scopusid.ToString()));
            cmd.Parameters.AddWithValue("@ScopusID2", HttpUtility.HtmlDecode(u.scopusid2.ToString()));
            cmd.Parameters.AddWithValue("@ScopusID3", HttpUtility.HtmlDecode(u.scopusid3.ToString()));
            cmd.Parameters.AddWithValue("@userid", userid);


            result1 = cmd.ExecuteNonQuery();
            if (result1 == 1)
            {

                //cmdString = " Update FacultyResearchArea set Domain=@Domain,Area=@Area where UserId= '" + userid + "'";
                cmdString = " Update FacultyResearchArea set Domain=@Domain,Area=@Area where UserId=@userid";

                cmd = new SqlCommand(cmdString, con, transaction);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@userid", userid);
                if (domainval == "")
                {
                    domainval = f.Domain;
                }
                else
                {
                    domainval = domainval + ":" + f.Domain;
                }
                for (int i = 0; i < JD.Length; i++)
                {
                    if (arealist == "")
                    {
                        arealist = JD[i].Area;
                    }
                    else
                    {

                        arealist = arealist + ":" + JD[i].Area;
                    }
                }
                cmd.Parameters.AddWithValue("@Domain", HttpUtility.HtmlDecode(f.Domain));

                cmd.Parameters.AddWithValue("@Area", HttpUtility.HtmlDecode(arealist));
                result = cmd.ExecuteNonQuery();
            }
            transaction.Commit();
            return result;


        }
        catch (Exception ex)
        {
            log.Error("Inside Journal_DataObject- fnUpdateResearchData block ");
            log.Error(ex.Message);
            log.Error(ex.StackTrace);
            transaction.Rollback();
            throw (ex);
        }
        finally
        {
            cmd.Dispose();
            con.Close();
            cmd.Dispose();
        }
    }

    //public bool UserIdSearch(string userid)
    //{
    //    bool result1 = false;
    //    SqlDataReader sdr = null;

    //    try
    //    {

    //        cmdString = "select UserId from FacultyResearchArea where UserId= '" + userid + "' ";
    //        con.Open();
    //        cmd = new SqlCommand(cmdString, con);
    //        cmd.CommandType = CommandType.Text;
    //        cmd.Parameters.AddWithValue("@UserId", userid);
    //        sdr = cmd.ExecuteReader();
    //        FileUpload f = new FileUpload();

    //        while (sdr.Read())
    //        {
    //            if (!Convert.IsDBNull(sdr["UserId"]))
    //            {
    //                f.EmployeeCode = (string)sdr["UserId"];
    //            }
    //        }
    //        if (f.EmployeeCode == null)
    //        {
    //            result1 = false;
    //        }
    //        else
    //        {
    //            result1 = true;
    //        }

    //        return result1;
    //    }

    //    catch (Exception ex)
    //    {
    //        log.Debug("Inside Incentive_DataObjects- UserIdSearch function, ID: " + userid);
    //        log.Error(ex.Message);
    //        log.Error(ex.StackTrace);
    //        throw ex;
    //    }

    //    finally
    //    {
    //        sdr.Close();
    //        con.Close();
    //    }

    //}

    public FileUpload DomainSearch(string userid)
    {
        log.Debug("Inside Journal_DataObject- DomainSearch function, User ID: " + userid);
        SqlDataReader sdr = null;
        FileUpload f = new FileUpload();
        con.Open();
        try
        {
            cmdString = " Select Domain,Area from FacultyResearchArea where UserId=@UserId";
            cmd = new SqlCommand(cmdString, con, transaction);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@UserId", HttpUtility.HtmlDecode(userid));
            sdr = cmd.ExecuteReader();

            if (sdr.HasRows)
            {
                while (sdr.Read())
                {

                    if (!Convert.IsDBNull(sdr["Domain"]))
                    {
                        f.Domain = (string)sdr["Domain"];
                    }
                    if (!Convert.IsDBNull(sdr["Area"]))
                    {
                        f.Area = (string)sdr["Area"];
                    }
                    else
                    {
                        f.Area = "";
                    }

                }
            }

            sdr.Close();
            return f;
        }

        catch (Exception ex)
        {
            log.Error("Inside DomainSearch function,  user ID: " + userid);
            log.Error(ex.Message);
            log.Error(ex.StackTrace);
            throw ex;
        }

        finally
        {
            sdr.Close();
            con.Close();
        }
    }



    public FileUpload CheckEmployeeId(string employeecode)
    {
        log.Debug("Inside Journal_DataObject- CheckEmployeeId function, User ID: " + employeecode);
        SqlDataReader sdr = null;
        FileUpload f = new FileUpload();
        con.Open();
        try
        {
            //cmdString = "select UserId from FacultyResearchArea where UserId= '" + employeecode + "' ";
            cmdString = "select UserId from FacultyResearchArea where UserId=@employeecode ";

            cmd = new SqlCommand(cmdString, con, transaction);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@employeecode", HttpUtility.HtmlDecode(employeecode));
            sdr = cmd.ExecuteReader();

            if (sdr.HasRows)
            {
                while (sdr.Read())
                {

                    if (!Convert.IsDBNull(sdr["UserId"]))
                    {
                        f.EmployeeCode = (string)sdr["UserId"];
                    }

                }
            }

            sdr.Close();
            return f;
        }

        catch (Exception ex)
        {
            log.Error("Inside CheckEmployeeId function,  user ID: " + employeecode);
            log.Error(ex.Message);
            log.Error(ex.StackTrace);
            throw ex;
        }

        finally
        {
            sdr.Close();
            con.Close();
        }
    }




    public int fninsertEditResearchData(string empcode, FileUpload f, FileUpload[] JD, User u)
    {
        log.Debug("Inside Journal_DataObject- fninsertEditResearchData function ");

        int result = 0, result1 = 0;
        string arealist = "";
        string domainval = "";
        con = new SqlConnection(str);
        con.Open();
        transaction = con.BeginTransaction();
        try
        {
            //cmdString = " Update User_M set ORCID=@ORCID,ScopusID=@ScopusID,ScopusID2=@ScopusID2,ScopusID3=@ScopusID3 where User_Id='" + empcode + "'";
            cmdString = " Update User_M set ORCID=@ORCID,ScopusID=@ScopusID,ScopusID2=@ScopusID2,ScopusID3=@ScopusID3 where User_Id=@empcode";

            cmd = new SqlCommand(cmdString, con, transaction);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("@ORCID", HttpUtility.HtmlDecode(u.orcid.ToString()));
            cmd.Parameters.AddWithValue("@ScopusID", HttpUtility.HtmlDecode(u.scopusid.ToString()));
            cmd.Parameters.AddWithValue("@ScopusID2", HttpUtility.HtmlDecode(u.scopusid2.ToString()));
            cmd.Parameters.AddWithValue("@ScopusID3", HttpUtility.HtmlDecode(u.scopusid3.ToString()));
            cmd.Parameters.AddWithValue("@empcode", empcode);


            result1 = cmd.ExecuteNonQuery();
            if (result1 == 1)
            {

                cmd = new SqlCommand("insertEditResearchData", con, transaction);
                cmd.CommandType = CommandType.StoredProcedure;

                if (empcode == "")
                {


                }
                else if (empcode != null)
                {
                    cmd.Parameters.AddWithValue("@UserId", empcode);


                    if (f.Domain == "")
                    {
                        domainval = f.Domain;
                    }
                    else
                    {
                        domainval = domainval + ":" + f.Domain;
                    }
                    for (int i = 0; i < JD.Length; i++)
                    {
                        if (arealist == "")
                        {
                            arealist = JD[i].Area;
                        }
                        else
                        {

                            arealist = arealist + ":" + JD[i].Area;
                        }
                    }
                    cmd.Parameters.AddWithValue("@Domain", HttpUtility.HtmlDecode(f.Domain));

                    cmd.Parameters.AddWithValue("@Area", HttpUtility.HtmlDecode(arealist));
                    result = cmd.ExecuteNonQuery();

                }

            }

            transaction.Commit();
            return result;
        }
        catch (Exception ex)
        {
            log.Error("Inside Journal_DataObject- fninsertEditResearchData catch block ");
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

    public int checkExistUserid(string empcode)
    {
        log.Debug("Inside Journal_DataObject- checkExistUserid function ");

        int result;
        int res = 2, res2 = 0;
        con = new SqlConnection(str);
        con.Open();
        // transaction = con.BeginTransaction();
        try
        {

            //cmdString = "Select * from FacultyResearchArea where UserId= '" + empcode + "' ";
            cmdString = "Select * from FacultyResearchArea where UserId=@empcode ";

            cmd = new SqlCommand(cmdString, con, transaction);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@empcode", HttpUtility.HtmlDecode(empcode));


            SqlDataReader sdr = cmd.ExecuteReader();

            while (sdr.Read())
            {
                if (sdr.HasRows)
                {
                    // update
                    res = 1;
                    return res;
                }
                // }
                else
                {
                    // insert.
                    res = 2;
                    return res;
                }
            }
            sdr.Close();


            return res;

        }

        catch (Exception ex)
        {
            log.Error("Inside Journal_DataObject- checkExistUserid  block ");
            log.Error(ex.Message);
            log.Error(ex.StackTrace);
            // transaction.Rollback();
            throw (ex);
        }
        finally
        {
            cmd.Dispose();
            con.Close();
            cmd.Dispose();
        }
    }


    public User CheckOrcidScopusid(string userid)
    {
        log.Debug("Inside AP_GL_DataObject- CheckOrcidScopusid function, UserId: " + userid);
        try
        {
            //cmdString = "select * from User_M where User_Id='" + userid + "'";
            cmdString = "select * from User_M where User_Id=@userid";


            con = new SqlConnection(str);
            con.Open();
            cmd = new SqlCommand(cmdString, con);



            User u = new User();
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@userid", userid);

            SqlDataReader sdr = cmd.ExecuteReader();

            while (sdr.Read())
            {

                if (!Convert.IsDBNull(sdr["ORCID"]))
                {
                    u.orcid = (string)sdr["ORCID"];
                }
                else if (Convert.IsDBNull(sdr["ORCID"]))
                {
                    u.orcid = "";
                }
                if (!Convert.IsDBNull(sdr["ScopusID"]))
                {
                    u.scopusid = (string)sdr["ScopusID"];
                }
                else if (Convert.IsDBNull(sdr["ScopusID"]))
                {
                    u.scopusid = "";
                }
                if (!Convert.IsDBNull(sdr["ScopusID2"]))
                {
                    u.scopusid2 = (string)sdr["ScopusID2"];
                }
                else if (Convert.IsDBNull(sdr["ScopusID2"]))
                {
                    u.scopusid2 = "";
                }
                if (!Convert.IsDBNull(sdr["ScopusID3"]))
                {
                    u.scopusid3 = (string)sdr["ScopusID3"];
                }
                else if (Convert.IsDBNull(sdr["ScopusID3"]))
                {
                    u.scopusid3 = "";
                }


            }

            return u;
        }
        catch (Exception ex)
        {
            log.Error("Inside - CheckOrcidScopusid catch block ");
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

    public DataSet SelecDomainandResearchArea(string domain, string area)
    {
        log.Debug("Inside function SelectDomainandResearchArea");
        try
        {
            SqlDataAdapter da;
            DataSet ds = new DataSet();
            con.Open();
            da = new SqlDataAdapter("SelectDomainandResearchArea", con);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.Add("@Domain", SqlDbType.VarChar);
            da.SelectCommand.Parameters["@Domain"].Value = domain;
            da.SelectCommand.Parameters.Add("@Area", SqlDbType.VarChar);
            da.SelectCommand.Parameters["@Area"].Value = area;
            da.Fill(ds);
            return ds;
        }
        catch (Exception ex)
        {
            log.Error("Inside - SelecDomainandResearchArea catch block ");
            log.Error(ex.Message);
            log.Error(ex.StackTrace);
            throw ex;
        }
        finally
        {
            con.Close();
        }
    }






    public User findmembername(string memberid)
    {
        try
        {
            //cmdString = "select top 1 AuthorName,MUNonMU from Publishcation_Author where EmployeeCode='" + memberid + "' ";
            cmdString = "select top 1 AuthorName,MUNonMU from Publishcation_Author where EmployeeCode=@memberid";


            con = new SqlConnection(str);
            con.Open();
            cmd = new SqlCommand(cmdString, con);



            User u = new User();
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@memberid", memberid);


            SqlDataReader sdr = cmd.ExecuteReader();

            while (sdr.Read())
            {

                if (!Convert.IsDBNull(sdr["AuthorName"]))
                {
                    u.membername = (string)sdr["AuthorName"];
                }
                else
                {
                    u.membername = "";
                }

                if (!Convert.IsDBNull(sdr["MUNonMU"]))
                {
                    u.MUNonMU = (string)sdr["MUNonMU"];
                }
                else
                {
                    u.MUNonMU = "";
                }
            }

            return u;
        }
        catch (Exception ex)
        {
            log.Error("Inside - findusername catch block ");
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

    public ArrayList SelectActiveYear(JournalData JournalValueObj)
    {
        log.Debug("Inside function SelectActiveYear,  Journal ID: " + JournalValueObj.JournalID);
        ArrayList list = new ArrayList();

        SqlDataReader sdr = null;
        JournalData j = new JournalData();
        con.Open();
        try
        {
            cmd = new SqlCommand("SelectJournalActiveYear", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", HttpUtility.HtmlDecode(JournalValueObj.JournalID));
            sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {

                if (!Convert.IsDBNull(sdr["Year"]))
                {
                    j.ActiveYear = (int)sdr["Year"];
                }
                else
                {
                    j.ActiveYear = 0;

                }

                list.Add(j.ActiveYear);

            }


            return list;
        }

        catch (Exception ex)
        {
            log.Error("Inside SelectActiveYear function,  Journal ID: " + JournalValueObj.JournalID);
            log.Error(ex.Message);
            log.Error(ex.StackTrace);
            throw ex;
        }

        finally
        {
            sdr.Close();
            con.Close();
        }
    }

    public ArrayList CheckDuplicates(string type)
    {
        log.Debug("Inside the CheckDuplicates function Project Type : " + type);
        ArrayList list = new ArrayList();
        SqlDataReader sdr = null;

        try
        {
            con.Open();

            cmd = new SqlCommand("SelectCheckDuplicates", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ProjectType", type);

            sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                string title = (string)sdr["Title"];
                list.Add(title);
            }
            return list;
        }
        catch (Exception e)
        {
            log.Error("Inside catch block of CheckDuplicates function Project Type : " + type);
            log.Error("Error Message :" + e.Message);
            log.Error("Strachk Trace :" + e.StackTrace);
            return null;
        }
        finally
        {
            sdr.Close();
            con.Close();
        }
    }


    public int CheckEmailDetails(string p1, string p2)
    {
        try
        {
            //cmdString = "select Id from  EmailQueue where ReferenceID='" + p1 + "'  and Module='" + p2 + "'";
            cmdString = "select Id from  EmailQueue where ReferenceID=@p1  and Module=@p2";


            con = new SqlConnection(str);
            con.Open();
            cmd = new SqlCommand(cmdString, con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@p1", p1);
            cmd.Parameters.AddWithValue("@p2", p2);


            SqlDataReader sdr = cmd.ExecuteReader();
            int id = 0;
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {

                    if (!Convert.IsDBNull(sdr["Id"]))
                    {
                        id = (Int32)sdr["Id"];
                    }
                    else
                    {
                        id = 0;
                    }


                }
            }
            else
            {
                id = 0;
            }

            return id;
        }
        catch (Exception ex)
        {

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



    public string CheckPublicationType(string ProjectID, string projectunit)
    {
        try
        {
            //cmdString = "select EntryType from  Project where ID='" + ProjectID + "'  and ProjectUnit='" + projectunit + "'";
            cmdString = "select EntryType from  Project where ID=@ProjectID  and ProjectUnit=@projectunit";

            con = new SqlConnection(str);
            con.Open();
            cmd = new SqlCommand(cmdString, con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@ProjectID", ProjectID);
            cmd.Parameters.AddWithValue("@projectunit", projectunit);
            SqlDataReader sdr = cmd.ExecuteReader();
            string id = "";
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {

                    if (!Convert.IsDBNull(sdr["EntryType"]))
                    {
                        id = (string)sdr["EntryType"];
                    }


                }
            }
            return id;
        }
        catch (Exception ex)
        {

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

    public DataSet fnStudentdetail(string ProjectID, string projectunit)
    {
        con = new SqlConnection(str);
        con.Open();
        try
        {

            SqlDataAdapter da;
            DataSet ds;
            cmdString = "select InvestigatorName,InstitutionName,InvestigatorType from Projectnvestigator where ID=@ProjectID and ProjectUnit=@ProjectUnit and (MUNonMU='S' OR MUNonMU='N') order by InvestigatorType desc";
            cmd = new SqlCommand(cmdString, con);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add("@ProjectID", SqlDbType.VarChar, 10);
            cmd.Parameters["@ProjectID"].Value = ProjectID;
            cmd.Parameters.Add("@ProjectUnit", SqlDbType.VarChar, 5);
            cmd.Parameters["@ProjectUnit"].Value = projectunit;

            da = new SqlDataAdapter(cmd);

            ds = new DataSet();
            da.Fill(ds);

            return ds;
        }

        catch (Exception ex)
        {
            log.Error("Inside find author type catch block ");
            log.Error(ex.Message);
            log.Error(ex.StackTrace);
            throw ex;
        }

        finally
        {
            con.Close();
        }
    }



    public DataSet getAuthorDetail(string p1, string p2)
    {
        log.Debug("Inside function getAuthorList");
        try
        {
            SqlDataAdapter da;
            DataSet ds = new DataSet();

            con = new SqlConnection(str);
            con.Open();
            cmdString = " select AuthorName from Publishcation_Author where PaublicationID=@PaublicationID and TypeOfEntry=@TypeOfEntry and (MUNonMU='M' )";
            da = new SqlDataAdapter(cmdString, con);

            da.SelectCommand.Parameters.Add("@PaublicationID", SqlDbType.VarChar, 10);
            da.SelectCommand.Parameters["@PaublicationID"].Value = p1;
            da.SelectCommand.Parameters.Add("@TypeOfEntry", SqlDbType.VarChar, 5);
            da.SelectCommand.Parameters["@TypeOfEntry"].Value = p2;

            da.SelectCommand.CommandType = CommandType.Text;


            da.Fill(ds);

            return ds;
        }
        catch (Exception e)
        {
            log.Error(e.Message);
            log.Error(e.StackTrace);

            throw e;
        }
        finally
        {
            con.Close();
        }

    }

    public int insertAuthorDetailEmailtracker(string AuthorName, EmailDetails details, string p)
    {
        log.Debug("Inside function insertAuthorDetailEmailtracker of of Project ID: " + p);
        try
        {
            con = new SqlConnection(str);
            con.Open();
            transaction = con.BeginTransaction();
            cmd = new SqlCommand("insert into  EmailQueueTrackerTable (Author_InvestigatorName,Publication_ProjectID,Module,subject ) values (@Author_InvestigatorName,@Publication_ProjectID,@Module,@subject)", con, transaction);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@Author_InvestigatorName", AuthorName);
            cmd.Parameters.AddWithValue("@Publication_ProjectID", p);
            cmd.Parameters.AddWithValue("@Module", details.Module);
            cmd.Parameters.AddWithValue("@subject", details.EmailSubject);
            int data = cmd.ExecuteNonQuery();
            transaction.Commit();
            return data;
        }
        catch (Exception e)
        {
            log.Error("Inside Catch block of function insertAuthorDetailEmailtracker of of Project ID: " + p);
            log.Error(e.Message);
            log.Error(e.StackTrace);

            throw e;
        }
        finally
        {
            con.Close();
        }
    }

    public JournalData CheckUniqueIdPublication(string p1, string p2, EmailDetails details)
    {
        log.Debug("Inside function CheckUniqueIdPublication of of Publication ID: " + p1);
        try
        {

            JournalData data = new JournalData();
            con = new SqlConnection(str);
            con.Open();
            string REF = (p2 + p1);
            transaction = con.BeginTransaction();
            //cmdString = "select ID,ReferenceID,subject+Module  as Module from EmailQueue where ReferenceID='" + (p2 + p1) + "' and Module='" + details.Module + "'";
            cmdString = "select ID,ReferenceID,subject+Module  as Module from EmailQueue where ReferenceID=@ReferenceID and Module=@Module";


            cmd = new SqlCommand(cmdString, con, transaction);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@ReferenceID", (p2 + p1));
            cmd.Parameters.AddWithValue("@Module", details.Module);

            SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                if (!Convert.IsDBNull(sdr["ID"]))
                {
                    data.ID = (int)sdr["ID"];
                }
                if (!Convert.IsDBNull(sdr["Module"]))
                {
                    data.Module = (string)sdr["Module"];
                }
                if (!Convert.IsDBNull(sdr["ReferenceID"]))
                {
                    data.RefID = (string)sdr["ReferenceID"];
                }
            }
            sdr.Close();
            transaction.Commit();
            return data;
        }
        catch (Exception e)
        {
            log.Error("Inside Catch block of function CheckUniqueIdPublication of  Publication ID: " + p1);
            log.Error(e.Message);
            log.Error(e.StackTrace);

            throw e;
        }
        finally
        {
            con.Close();
        }
    }

    public int updatePublicationEmailtracker(string p1, string p2, EmailDetails details, JournalData obj3)
    {
        log.Debug("Inside function updatePublicationEmailtracker of  Publication ID: " + p1);
        try
        {
            con = new SqlConnection(str);
            con.Open();
            transaction = con.BeginTransaction();
            //cmdString = "update EmailQueueTrackerTable set EmailqueueId='" + obj3.ID + "',RefferenceID='" + obj3.RefID + "'  where Publication_ProjectID='" + p1 + "' and subject='" + details.EmailSubject + "' and Module='" + details.Module + "' ";
            cmdString = "update EmailQueueTrackerTable set EmailqueueId=@ID,RefferenceID=@RefID  where Publication_ProjectID=@p1 and subject=@EmailSubject and Module=@Module ";        
            cmd = new SqlCommand(cmdString, con, transaction);
            cmd.Parameters.AddWithValue("@ID", obj3.ID);
            cmd.Parameters.AddWithValue("@RefID", obj3.RefID);
            cmd.Parameters.AddWithValue("@p1", p1);
            cmd.Parameters.AddWithValue("@EmailSubject", details.EmailSubject);
            cmd.Parameters.AddWithValue("@Module", details.Module);
            cmd.CommandType = CommandType.Text;

            int data = cmd.ExecuteNonQuery();
            transaction.Commit();
            return data;
        }
        catch (Exception e)
        {
            log.Error("Inside Catch block of function updatePublicationEmailtracker of  Publication ID: " + p1);
            log.Error(e.Message);
            log.Error(e.StackTrace);

            throw e;
        }
        finally
        {
            con.Close();
        }
    }

    public int insertEmailtrackerUpEprint(string AuthorName, EmailDetails details, string p)
    {
        log.Debug("Inside function insertEmailtrackerUpEprint of of Project ID: " + p);
        try
        {
            con = new SqlConnection(str);
            con.Open();
            transaction = con.BeginTransaction();
            cmd = new SqlCommand("insert into  EmailQueueTrackerTable (Author_InvestigatorName,Publication_ProjectID,Module,subject ) values (@Author_InvestigatorName,@Publication_ProjectID,@Module,@subject)", con, transaction);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@Author_InvestigatorName", AuthorName);
            cmd.Parameters.AddWithValue("@Publication_ProjectID", p);
            cmd.Parameters.AddWithValue("@Module", details.Module);
            cmd.Parameters.AddWithValue("@subject", details.EmailSubject);
            int data = cmd.ExecuteNonQuery();
            transaction.Commit();
            return data;
        }
        catch (Exception e)
        {
            log.Error("Inside Catch block of function insertEmailtrackerUpEprint of of Project ID: " + p);
            log.Error(e.Message);
            log.Error(e.StackTrace);

            throw e;
        }
        finally
        {
            con.Close();
        }
    }

    public DataSet getAuthorCCListDetail(string p1, string p2)
    {
        log.Debug("Inside function getAuthorCCListDetail");
        try
        {
            SqlDataAdapter da;
            DataSet ds = new DataSet();

            con = new SqlConnection(str);
            con.Open();
            cmdString = "select AuthorName from Publishcation_Author where PaublicationID=@id and MUNonMU='M' and TypeOfEntry=@TypeOfEntry ";
            da = new SqlDataAdapter(cmdString, con);

            da.SelectCommand.Parameters.Add("@id", SqlDbType.VarChar, 10);
            da.SelectCommand.Parameters["@id"].Value = p1;
            da.SelectCommand.Parameters.Add("@TypeOfEntry", SqlDbType.VarChar, 2);
            da.SelectCommand.Parameters["@TypeOfEntry"].Value = p2;
            da.SelectCommand.CommandType = CommandType.Text;
            da.Fill(ds);
            return ds;
        }
        catch (Exception e)
        {
            log.Error(e.Message);
            log.Error(e.StackTrace);

            throw e;
        }
        finally
        {
            con.Close();
        }
    }

    public JournalData CheckUniqueIdUPEprint(string p1, string p2, EmailDetails details)
    {
        log.Debug("Inside function CheckUniqueIdPublication of of Publication ID: " + p1);
        try
        {

            JournalData data = new JournalData();
            con = new SqlConnection(str);
            con.Open();
            string REF = (p2 + p1);
            transaction = con.BeginTransaction();
            //cmdString = "select ID,ReferenceID,subject+Module  as Module from EmailQueue where ReferenceID='" + (p2 + p1) + "' and Module='" + details.Module + "'";
            cmdString = "select ID,ReferenceID,subject+Module  as Module from EmailQueue where ReferenceID=@ReferenceID and Module=@Module";


            cmd = new SqlCommand(cmdString, con, transaction);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@ReferenceID", (p2 + p1));
            cmd.Parameters.AddWithValue("@Module", details.Module);

            SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                if (!Convert.IsDBNull(sdr["ID"]))
                {
                    data.ID = (int)sdr["ID"];
                }
                if (!Convert.IsDBNull(sdr["Module"]))
                {
                    data.Module = (string)sdr["Module"];
                }
                if (!Convert.IsDBNull(sdr["ReferenceID"]))
                {
                    data.RefID = (string)sdr["ReferenceID"];
                }
            }
            sdr.Close();
            transaction.Commit();
            return data;
        }
        catch (Exception e)
        {
            log.Error("Inside Catch block of function CheckUniqueIdPublication of  Publication ID: " + p1);
            log.Error(e.Message);
            log.Error(e.StackTrace);

            throw e;
        }
        finally
        {
            con.Close();
        }
    }

    public int updateEmailtrackerUpEprint(string p1, string p2, EmailDetails details, JournalData obj3)
    {
        log.Debug("Inside function updatePublicationEmailtracker of  Publication ID: " + p1);
        try
        {
            con = new SqlConnection(str);
            con.Open();
            transaction = con.BeginTransaction();
            //cmdString = "update EmailQueueTrackerTable set EmailqueueId='" + obj3.ID + "',RefferenceID='" + obj3.RefID + "'  where Publication_ProjectID='" + p1 + "' and subject='" + details.EmailSubject + "' and Module='" + details.Module + "' ";
            cmdString = "update EmailQueueTrackerTable set EmailqueueId=@ID,RefferenceID=@RefID  where Publication_ProjectID=@p1 and subject=@EmailSubject and Module=@Module";


            cmd = new SqlCommand(cmdString, con, transaction);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@ID", obj3.ID);
            cmd.Parameters.AddWithValue("@RefID", obj3.RefID);
            cmd.Parameters.AddWithValue("@p1", p1);
            cmd.Parameters.AddWithValue("@EmailSubject", details.EmailSubject);
            cmd.Parameters.AddWithValue("@Module", details.Module);

            int data = cmd.ExecuteNonQuery();
            transaction.Commit();
            return data;
        }
        catch (Exception e)
        {
            log.Error("Inside Catch block of function updatePublicationEmailtracker of  Publication ID: " + p1);
            log.Error(e.Message);
            log.Error(e.StackTrace);

            throw e;
        }
        finally
        {
            con.Close();
        }
    }



    public int insertEmailtrackerIncentive(string AuthorName, EmailDetails details, string p)
    {
        log.Debug("Inside function insertEmailtrackerIncentive of of Publication ID: " + p);
        try
        {
            con = new SqlConnection(str);
            con.Open();
            transaction = con.BeginTransaction();
            cmd = new SqlCommand("insert into  EmailQueueTrackerTable (Author_InvestigatorName,Publication_ProjectID,Module,subject ) values (@Author_InvestigatorName,@Publication_ProjectID,@Module,@subject)", con, transaction);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@Author_InvestigatorName", AuthorName);
            cmd.Parameters.AddWithValue("@Publication_ProjectID", p);
            cmd.Parameters.AddWithValue("@Module", details.Module);
            cmd.Parameters.AddWithValue("@subject", details.EmailSubject);
            int data = cmd.ExecuteNonQuery();
            transaction.Commit();
            return data;
        }
        catch (Exception e)
        {
            log.Error("Inside Catch block of function insertEmailtrackerIncentive of Publication ID: " + p);
            log.Error(e.Message);
            log.Error(e.StackTrace);

            throw e;
        }
        finally
        {
            con.Close();
        }
    }
    public PublishData checkJournalLinkM(string p1, int p2)
    {
        log.Debug("Inside function checkJournalLinkM of  ISSN ID: " + p1);
        try
        {

            PublishData data = new PublishData();
            con = new SqlConnection(str);
            con.Open();
            transaction = con.BeginTransaction();
            //cmdString = "select Quartile from Journal_Quartile_Map where JournalId='" + p1 + "' and Year='" + p2 + "'";
            cmdString = "select Quartile from Journal_Quartile_Map where JournalId=@p1 and Year=@p2";


            cmd = new SqlCommand(cmdString, con, transaction);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@p1", p1);
            cmd.Parameters.AddWithValue("@p2", p2);

            SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                if (!Convert.IsDBNull(sdr["Quartile"]))
                {
                    data.Jquartile = (string)sdr["Quartile"];
                }

            }
            sdr.Close();
            transaction.Commit();
            return data;
        }
        catch (Exception e)
        {
            log.Error("Inside Catch block of function checkJournalLinkM of  ISSN ID: " + p1);
            log.Error(e.Message);
            log.Error(e.StackTrace);

            throw e;
        }
        finally
        {
            con.Close();
        }
    }

    public PublishData CheckQuartilevaluefromJQM(string p1, string p2)
    {
        log.Debug("Inside function checkJournalLinkM of  ISSN ID: " + p2);
        try
        {

            PublishData data = new PublishData();
            con = new SqlConnection(str);
            con.Open();
            transaction = con.BeginTransaction();
            //cmdString = "select Quartile from Journal_Quartile_Map where JournalId='" + p2 + "' and Year='" + p1 + "'";
            cmdString = "select Quartile from Journal_Quartile_Map where JournalId=@p2 and Year=@p1";


            cmd = new SqlCommand(cmdString, con, transaction);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@p2", p2);
            cmd.Parameters.AddWithValue("@p1", p1);

            SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                if (!Convert.IsDBNull(sdr["Quartile"]))
                {
                    data.Jquartile = (string)sdr["Quartile"];
                }

            }
            sdr.Close();
            transaction.Commit();
            return data;
        }
        catch (Exception e)
        {
            log.Error("Inside Catch block of function checkJournalLinkM of  ISSN ID: " + p2);
            log.Error(e.Message);
            log.Error(e.StackTrace);

            throw e;
        }
        finally
        {
            con.Close();
        }

    }

    public PublishData InsertJournalQuartileTracker(string p1, int p2, string p3, string QId, object p4)
    {
        log.Debug("Inside function InsertJournalQuartileTracker of of Publication ID: " + p1);
        try
        {
            PublishData data = new PublishData();
            con = new SqlConnection(str);
            con.Open();
            transaction = con.BeginTransaction();

            //cmdString = " select count(EntryNo) as EntryNo  from Journal_Quartile_Tracker where JournalId='" + p1 + "'and Year='" + p2 + "' ";
            cmdString = " select count(EntryNo) as EntryNo  from Journal_Quartile_Tracker where JournalId=@p1 and Year=@p2 ";


            cmd = new SqlCommand(cmdString, con, transaction);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@p1", p1);
            cmd.Parameters.AddWithValue("@p2", p2);
            SqlDataReader reader = cmd.ExecuteReader();
            int count = 0;
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    if (!Convert.IsDBNull(reader["EntryNo"]))
                    {
                        count = (int)reader["EntryNo"];
                    }

                }
            }
            else
            {
                count = 0;
            }
            reader.Close();





            cmd = new SqlCommand("insert into  Journal_Quartile_Tracker(JournalId,Year,EntryNo,FromQuartile,ToQuartile,UserId,Date ) values (@JournalId,@Year,@EntryNo,@FromQuartile,@ToQuartile,@UserId,@Date)", con, transaction);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@JournalId", p1);
            cmd.Parameters.AddWithValue("@Year", p2);
            cmd.Parameters.AddWithValue("@EntryNo", count + 1);
            cmd.Parameters.AddWithValue("@FromQuartile", p3);
            cmd.Parameters.AddWithValue("@ToQuartile", QId);
            cmd.Parameters.AddWithValue("@UserId", p4);
            cmd.Parameters.AddWithValue("@Date", DateTime.Now);
            int value = cmd.ExecuteNonQuery();

            //cmd = new SqlCommand("Update Journal_Quartile_Map set Quartile=@Quartile,UserId=@UserId,Date=@Date where JournalId='" + p1 + "'and Year='" + p2 + "'", con, transaction);
            cmd = new SqlCommand("Update Journal_Quartile_Map set Quartile=@Quartile,UserId=@UserId,Date=@Date where JournalId=@p1 and Year=@p2 ", con, transaction);

            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@Quartile", QId);
            cmd.Parameters.AddWithValue("@UserId", p4);
            cmd.Parameters.AddWithValue("@Date", DateTime.Now);
            cmd.Parameters.AddWithValue("@p1", p1);
            cmd.Parameters.AddWithValue("@p2", p2);
            int value1 = cmd.ExecuteNonQuery();


            transaction.Commit();
            return data;
        }
        catch (Exception e)
        {
            log.Error("Inside Catch block of function InsertJournalQuartileTracker of Publication ID: " + p1);
            log.Error(e.Message);
            log.Error(e.StackTrace);

            throw e;
        }
        finally
        {
            con.Close();
        }
    }

    public PublishData checkQuartileValue(string p, string jyear)
    {
        log.Debug("Inside function checkQuartileValue of  ISSN ID: " + p);
        try
        {

            PublishData data = new PublishData();
            con = new SqlConnection(str);
            con.Open();
            transaction = con.BeginTransaction();
            //cmdString = "select Quartile from Journal_Quartile_Map where JournalId='" + p + "' and Year='" + jyear + "'";
            cmdString = "select Quartile from Journal_Quartile_Map where JournalId=@p and Year=@jyear";


            cmd = new SqlCommand(cmdString, con, transaction);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@p", p);
            cmd.Parameters.AddWithValue("@jyear", jyear);

            SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                //if (!Convert.IsDBNull(sdr["Year"]))
                //{
                //    data.QYear = (int)sdr["Year"];
                //}
                if (!Convert.IsDBNull(sdr["Quartile"]))
                {
                    data.Jquartile = (string)sdr["Quartile"];
                }

            }
            sdr.Close();
            transaction.Commit();
            return data;
        }
        catch (Exception e)
        {
            log.Error("Inside Catch block of function checkQuartileValue of  ISSN ID: " + p);
            log.Error(e.Message);
            log.Error(e.StackTrace);

            throw e;
        }
        finally
        {
            con.Close();
        }
    }

    public int InsertJournalQuartileMap(string ISSN, int p1, string QId, string CreatedBy)
    {
        log.Debug("Inside function InsertJournalQuartileMap of  ISSN ID: " + ISSN);
        try
        {

            PublishData data = new PublishData();
            con = new SqlConnection(str);
            con.Open();
            transaction = con.BeginTransaction();
            cmdString = "Insert Journal_Quartile_Map(JournalId,Year,Quartile,UserId,Date) values(@JournalId,@Year,@Quartile,@CreatedBy,@CreatedDate)  ";

            cmd = new SqlCommand(cmdString, con, transaction);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@JournalId", ISSN);
            cmd.Parameters.AddWithValue("@Year", p1);
            cmd.Parameters.AddWithValue("@Quartile", QId);
            cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);
            cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);

            int value = cmd.ExecuteNonQuery();

            //cmdString = " select count(EntryNo) as EntryNo  from Journal_Quartile_Tracker where JournalId='" + ISSN + "'and Year='" + p1 + "' ";
            cmdString = " select count(EntryNo) as EntryNo  from Journal_Quartile_Tracker where JournalId=@ISSN and Year=@p1  ";


            cmd = new SqlCommand(cmdString, con, transaction);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@ISSN", ISSN);
            cmd.Parameters.AddWithValue("@p1", p1);
            SqlDataReader reader = cmd.ExecuteReader();
            int count = 0;
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    if (!Convert.IsDBNull(reader["EntryNo"]))
                    {
                        count = (int)reader["EntryNo"];
                    }

                }
            }
            else
            {
                count = 0;
            }
            reader.Close();





            cmd = new SqlCommand("insert into  Journal_Quartile_Tracker(JournalId,Year,EntryNo,FromQuartile,ToQuartile,UserId,Date ) values (@JournalId,@Year,@EntryNo,@FromQuartile,@ToQuartile,@UserId,@Date)", con, transaction);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@JournalId", ISSN);
            cmd.Parameters.AddWithValue("@Year", p1);
            cmd.Parameters.AddWithValue("@EntryNo", count + 1);
            cmd.Parameters.AddWithValue("@FromQuartile", QId);
            cmd.Parameters.AddWithValue("@ToQuartile", QId);
            cmd.Parameters.AddWithValue("@UserId", CreatedBy);
            cmd.Parameters.AddWithValue("@Date", DateTime.Now);
            int value1 = cmd.ExecuteNonQuery();


            transaction.Commit();
            return value;
        }
        catch (Exception e)
        {
            log.Error("Inside Catch block of function InsertJournalQuartileMap of  ISSN ID: " + ISSN);
            log.Error(e.Message);
            log.Error(e.StackTrace);

            throw e;
        }
        finally
        {
            con.Close();
        }
    }

    public JournalData selectQuartilevalue(string p, int jayear, int jamonth, int Quartilefrommonth, int QuartileTomonth)
    {
        log.Debug("Inside function checkQuartileValue of  ISSN ID: " + p);
        try
        {

            JournalData data = new JournalData();
            con = new SqlConnection(str);
            con.Open();
            transaction = con.BeginTransaction();
            //cmdString = "select Quartile,Name from Journal_Quartile_Map a,Quartile_M b where JournalId='" + p + "' and Year='" + jayear + "' and a.Quartile=b.Id";
            cmdString = "select Quartile,Name from Journal_Quartile_Map a,Quartile_M b where JournalId=@p  and Year=@jayear and a.Quartile=b.Id";


            cmd = new SqlCommand(cmdString, con, transaction);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@p", p);
            cmd.Parameters.AddWithValue("@jayear", jayear);
            SqlDataReader sdr = cmd.ExecuteReader();

            data.QName = "NA";

            while (sdr.Read())
            {
                if (!Convert.IsDBNull(sdr["Name"]))
                {
                    data.QName = (string)sdr["Name"];
                }
                else
                {
                    data.QName = "NA";
                }

            }
            sdr.Close();
            transaction.Commit();
            return data;
        }
        catch (Exception e)
        {
            log.Error("Inside Catch block of function checkQuartileValue of  ISSN ID: " + p);
            log.Error(e.Message);
            log.Error(e.StackTrace);

            throw e;
        }
        finally
        {
            con.Close();
        }
    }

    public JournalData selectQuartilevaluefrompublication(string p1, string p2, string p3)
    {
        log.Debug("Inside function selectQuartilevaluefrompublication of  ID: " + p1);
        try
        {

            JournalData data = new JournalData();
            con = new SqlConnection(str);
            con.Open();
            transaction = con.BeginTransaction();
            //cmdString = "select QuartileOnIncentivize,Name from Publication a,Quartile_M b where PublicationID='" + p1 + "'and TypeOfEntry='" + p3 + "'and MUCategorization='" + p2 + "' and a.QuartileOnIncentivize=b.Id";
            cmdString = "select QuartileOnIncentivize,Name from Publication a,Quartile_M b where PublicationID=@p1 and TypeOfEntry=@p3 and MUCategorization=@p2  and a.QuartileOnIncentivize=b.Id";


            cmd = new SqlCommand(cmdString, con, transaction);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@p1", p1);
            cmd.Parameters.AddWithValue("@p3", p3);
            cmd.Parameters.AddWithValue("@p2", p2);

            SqlDataReader sdr = cmd.ExecuteReader();
            data.QName = "NA";
            while (sdr.Read())
            {
                if (!Convert.IsDBNull(sdr["Name"]))
                {
                    data.QName = (string)sdr["Name"];
                }
                if (!Convert.IsDBNull(sdr["QuartileOnIncentivize"]))
                {
                    data.Jquartile = (string)sdr["QuartileOnIncentivize"];
                }

            }
            sdr.Close();
            transaction.Commit();
            return data;
        }
        catch (Exception e)
        {
            log.Error("Inside Catch block of function selectQuartilevaluefrompublication of  ID: " + p1);
            log.Error(e.Message);
            log.Error(e.StackTrace);

            throw e;
        }
        finally
        {
            con.Close();
        }
    }

    public JournalData selectQuartilevaluefrompublicationEntry(string p1, string p2, string p3)
    {
        log.Debug("Inside function selectQuartilevaluefrompublicationEntry of  ID: " + p1);
        try
        {

            JournalData data = new JournalData();
            con = new SqlConnection(str);
            con.Open();
            transaction = con.BeginTransaction();
            //cmdString = "select QuartileOnEntry,Name from Publication a,Quartile_M b where PublicationID='" + p1 + "'and TypeOfEntry='" + p3 + "'and MUCategorization='" + p2 + "' and a.QuartileOnEntry=b.Id";
            cmdString = "select QuartileOnEntry,Name from Publication a,Quartile_M b where PublicationID=@p1 and TypeOfEntry=@p3 and MUCategorization=@p2  and a.QuartileOnEntry=b.Id";


            cmd = new SqlCommand(cmdString, con, transaction);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@p1", p1);
            cmd.Parameters.AddWithValue("@p3", p3);
            cmd.Parameters.AddWithValue("@p2", p2);

            SqlDataReader sdr = cmd.ExecuteReader();
            data.QName = "NA";
            while (sdr.Read())
            {
                if (!Convert.IsDBNull(sdr["Name"]))
                {
                    data.QName = (string)sdr["Name"];
                }
                if (!Convert.IsDBNull(sdr["QuartileOnEntry"]))
                {
                    data.Jquartile = (string)sdr["QuartileOnEntry"];
                }

            }
            sdr.Close();
            transaction.Commit();
            return data;
        }
        catch (Exception e)
        {
            log.Error("Inside Catch block of function selectQuartilevaluefrompublicationEntry of  ID: " + p1);
            log.Error(e.Message);
            log.Error(e.StackTrace);

            throw e;
        }
        finally
        {
            con.Close();
        }
    }

    public string CheckPrintEvaluationEnableQuartile(string p1, string p2, string p3, string p4)
    {
        log.Debug("Inside function CheckPrintEvaluationEnableQuartile of  Mucategory: " + p1);
        try
        {
            con = new SqlConnection(str);
            con.Open();
            transaction = con.BeginTransaction();
            string result = "Y";
            cmdString = "SelectPrintEvaluationEnable";

            cmd = new SqlCommand(cmdString, con, transaction);
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.AddWithValue("@Category", HttpUtility.HtmlDecode(p1));
            cmd.Parameters.AddWithValue("@Month", HttpUtility.HtmlDecode(p2));
            cmd.Parameters.AddWithValue("@Year", HttpUtility.HtmlDecode(p3));
            cmd.Parameters.AddWithValue("@Quartile", HttpUtility.HtmlDecode(p4));

            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    if (!Convert.IsDBNull(sdr["EPrintEvalutionEnable"]))
                    {
                        result = (string)sdr["EPrintEvalutionEnable"];
                    }

                }

            }
            else
            {
                result = "Y";
            }
            sdr.Close();
            transaction.Commit();
            return result;
        }
        catch (Exception e)
        {
            log.Error("Inside Catch block of function CheckPrintEvaluationEnableQuartile of  Mucategory: " + p1);
            log.Error(e.Message);
            log.Error(e.StackTrace);

            throw e;
        }
        finally
        {
            con.Close();
        }
    }


    public JournalData ProceedingEntryCheckExistance(JournalData JournalValueObj)
    {
        log.Debug("Inside Journal_DataObject- ProceedingEntryCheckExistance function ");
        int result = 0;

        con = new SqlConnection(str);
        con.Open();

        try
        {
            cmdString = "Select * from Proceedings_M where ID=@Id";
            cmd = new SqlCommand(cmdString, con, transaction);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@Id", HttpUtility.HtmlDecode(JournalValueObj.JournalID));

            SqlDataReader sdr = cmd.ExecuteReader();
            JournalData j = new JournalData();
            while (sdr.Read())
            {
                if (sdr.HasRows)
                {

                    if (!Convert.IsDBNull(sdr["Title"]))
                    {
                        j.name = (string)sdr["Title"];                   
                    }
                    if (!Convert.IsDBNull(sdr["Abbreviatedtitle"]))
                    {
                        j.jname = (string)sdr["Abbreviatedtitle"];

                    }              
                    if (!Convert.IsDBNull(sdr["ID"]))
                    {
                        j.jid = (string)sdr["ID"];
                    }

                }
            }
            return j;
        }
        catch (Exception ex)
        {
            log.Error("Inside Journal_DataObject-ProceedingEntryCheckExistance catch block ");
            log.Error(ex.Message);
            log.Error(ex.StackTrace);
            throw (ex);
        }
        finally
        {
            cmd.Dispose();
            con.Close();
            cmd.Dispose();
        }
    }

    public JournalData ProceedingYearwiseCheck(JournalData JournalValueObj)
    {
        log.Debug("Inside ProceedingYearwiseCheck function ");
        int result = 0;
        con.Open();
        try
        {
            cmdString = "Select * from Proceedings_Year_Map where ISSN=@Id and Year=@Year";
            cmd = new SqlCommand(cmdString, con, transaction);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@Id", JournalValueObj.JournalID);
            cmd.Parameters.AddWithValue("@Year", JournalValueObj.year);
            SqlDataReader sdr = cmd.ExecuteReader();
            JournalData j = new JournalData();
            while (sdr.Read())
            {
                if (sdr.HasRows)
                {

                    if (!Convert.IsDBNull(sdr["ISSN"]))
                    {
                        j.jid = (string)sdr["ISSN"];
                    }

                }
            }
            return j;
        }
        catch (Exception ex)
        {
            log.Error("Inside Journal_DataObject-ProceedingYearwiseCheck catch block ");
            log.Error(ex.Message);
            log.Error(ex.StackTrace);
            throw (ex);
        }
        finally
        {
            cmd.Dispose();
            con.Close();
            cmd.Dispose();
        }
    }

    public JournalData GetImpactFactorApplicableYearProceeding(JournalData JournalValueObj)
    {
        log.Debug("Inside - GetImpactFactorApplicableYearProceeding function");
        JournalData obj = new JournalData();
        try
        {
            con = new SqlConnection(str);
            con.Open();
            cmd = new SqlCommand("SelectISSNApplicableYearProceeding", con);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", HttpUtility.HtmlDecode(JournalValueObj.JournalID));
            cmd.Parameters.AddWithValue("@PublishJAYear", HttpUtility.HtmlDecode(JournalValueObj.year));
            cmd.Parameters.AddWithValue("@PublishJAMonth", HttpUtility.HtmlDecode(JournalValueObj.month));

            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    if (!Convert.IsDBNull(sdr["Id"]))
                    {
                        obj.JournalID = (string)sdr["Id"];
                    }

                    if (!Convert.IsDBNull(sdr["Year"]))
                    {
                        obj.year = (string)sdr["Year"];
                    }
                    if (!Convert.IsDBNull(sdr["ImpactFactor"]))
                    {
                        obj.impctfact = (double)sdr["ImpactFactor"];
                    }

                    if (!Convert.IsDBNull(sdr["fiveImpFact"]))
                    {
                        obj.fiveimpcrfact = (double)sdr["fiveImpFact"];
                    }


                }

            }
            else
            {
                obj.JournalID = "";
            }
            return obj;
        }
        catch (Exception ex)
        {
            log.Error("Inside - GetImpactFactorApplicableYearProceeding catch block ");
            log.Error(ex.Message);
            log.Error(ex.StackTrace);
            throw ex;
        }

        finally
        {
            con.Close();
        }
    }

    public JournalData ProceedingGetImpactFactorPublishEntry(JournalData JournalValueObj)
    {
        log.Debug("Inside Journal_DataObject- ProceedingGetImpactFactorPublishEntry function ");
        int result = 0;

        con = new SqlConnection(str);
        con.Open();

        try
        {
            cmdString = "Select * from Proceeding_IF_Details where Id=@Id AND Year=@Year ";
            cmd = new SqlCommand(cmdString, con, transaction);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@Id", HttpUtility.HtmlDecode(JournalValueObj.JournalID));
            cmd.Parameters.AddWithValue("@Year", HttpUtility.HtmlDecode(JournalValueObj.year));

            SqlDataReader sdr = cmd.ExecuteReader();
            JournalData jd = new JournalData();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {

                    if (!Convert.IsDBNull(sdr["ImpactFactor"]))
                    {
                        jd.impctfact = (double)sdr["ImpactFactor"];
                    }
                    if (!Convert.IsDBNull(sdr["Comments"]))
                    {
                        jd.Comments = sdr["Comments"].ToString();
                    }
                    if (!Convert.IsDBNull(sdr["fiveImpFact"]))
                    {
                        jd.fiveimpcrfact = (double)sdr["fiveImpFact"];
                    }



                }
            }
            else
            {
                jd.Comments = "false";
            }

            return jd;
        }
        catch (Exception ex)
        {
            log.Error("Inside Journal_DataObject-ProceedingGetImpactFactorPublishEntry catch block ");
            log.Error(ex.Message);
            log.Error(ex.StackTrace);
            throw (ex);
        }
        finally
        {
            cmd.Dispose();
            con.Close();
            cmd.Dispose();
        }
    }

    public string fnfindjidgtPname(string Pid, string TypeEntry)
    {
        log.Debug("Inside AP_GL_DataObject- fnfindjid function, journalID: " + Pid + "busUnit: " + TypeEntry);
        try
        {
            cmdString = "select Proceedings_M.Title from Publication, Proceedings_M where "
                    + " Proceedings_M.ID =Publication.PubJournalID  and PublicationID=@PublicationID and TypeOfEntry=@TypeOfEntry ";
            //cmdString = "select * from Publication where PublicationID=@PublicationID and TypeOfEntry=@TypeOfEntry ";

            // cmdString = "select BusinessUnit,JournalID, JournalDate, LineNarration,LongNarration, EntryStatus from GL where JournalID=@JournalID and BusinessUnit=@BusinessUnit";
            con = new SqlConnection(str);
            con.Open();
            cmd = new SqlCommand(cmdString, con);
            cmd.Parameters.Add("@PublicationID", SqlDbType.VarChar, 15);
            cmd.Parameters["@PublicationID"].Value = Pid;
            cmd.Parameters.Add("@TypeOfEntry", SqlDbType.VarChar, 12);
            cmd.Parameters["@TypeOfEntry"].Value = TypeEntry;
            // cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandType = CommandType.Text;
            SqlDataReader sdr = cmd.ExecuteReader();
            // voucher p = new voucher();
            PublishData V = new PublishData();

            string jname = "";
            while (sdr.Read())
            {


                if (!Convert.IsDBNull(sdr["Title"]))
                {
                    jname = (string)sdr["Title"];
                }
                else if (Convert.IsDBNull(sdr["Title"]))
                {
                    jname = "";
                }


            }
            return jname;
        }
        catch (Exception ex)
        {
            log.Error("Inside AP_GL_DataObject- fnfindjid catch block ");
            log.Error(ex.Message);
            log.Error(ex.StackTrace);
            throw ex;
        }

        finally
        {
            con.Close();
        }
    }

    public string getauthoremailID(string Memberid)
    {
        log.Debug("Inside getauthoremailID function, Memberid: " + Memberid);
        try
        {
            cmdString = "SELECT distinct top 1 EmailId from Member_Incentive_Point_Summary , Publishcation_Author where Publishcation_Author.EmployeeCode=Member_Incentive_Point_Summary.MemberId and MemberType!='N'  and MemberId =@Memberid ";
          
            con = new SqlConnection(str);
            con.Open();
            cmd = new SqlCommand(cmdString, con);
            cmd.Parameters.Add("@Memberid", SqlDbType.VarChar, 15);
            cmd.Parameters["@Memberid"].Value = Memberid;       
            cmd.CommandType = CommandType.Text;
            SqlDataReader sdr = cmd.ExecuteReader();
            // voucher p = new voucher();
            PublishData V = new PublishData();

            string EmailId = "";
            while (sdr.Read())
            {


                if (!Convert.IsDBNull(sdr["EmailId"]))
                {
                    EmailId = (string)sdr["EmailId"];
                }
                else if (Convert.IsDBNull(sdr["EmailId"]))
                {
                    EmailId = "";
                }


            }
            return EmailId;
        }
        catch (Exception ex)
        {
            log.Error("Inside getauthoremailID catch block ");
            log.Error(ex.Message);
            log.Error(ex.StackTrace);
            throw ex;
        }

        finally
        {
            con.Close();
        }
    }

    internal User findquartile(string PatientID)
    {

        log.Debug("inside function findquartile");
        try
        {
           
            con.Open();
            cmd = new SqlCommand("findquartile", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@id", SqlDbType.VarChar, 12);
            cmd.Parameters["@id"].Value = PatientID;
            SqlDataReader reader = cmd.ExecuteReader();
            User p = new User();
            p.Name="NA";
            while (reader.Read())
            {

                p.Name = (string)reader["quartileName"];


            }
            return p;
        }
        catch (Exception e)
        {
            log.Debug("Error: Inside catch block of findquartile");
            log.Error("Error msg:" + e);
            log.Error("Stack trace:" + e.StackTrace);
            e.ToString();
            return null;
        }
        finally
        {
            con.Close();
        }
    }

    public int InsertSeedMoneyBudget(SeedMoney a)
    {
        con = new SqlConnection(str);
        con.Open();
        transaction = con.BeginTransaction();

        try
        {


            int result = 0;


            cmdString = "select BudgetId from SeedMoneyBudget_M";


            cmd = new SqlCommand(cmdString, con, transaction);
            cmd.CommandType = CommandType.Text;
            SqlDataReader sdr = cmd.ExecuteReader();
            int id = 0;
            while (sdr.Read())
            {


                if (!Convert.IsDBNull(sdr["BudgetId"]))
                {
                    id = (int)sdr["BudgetId"];
                }
                else if (Convert.IsDBNull(sdr["BudgetId"]))
                {
                    id = 0;
                }


            }
            sdr.Close();


            cmdString = "insert into SeedMoneyBudget_M (BudgetId,Amount,Type,Active)values(@BudgetId,@Amount,@Type,@Active)";
            cmd = new SqlCommand(cmdString, con, transaction);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("@BudgetId", id + 1);
            cmd.Parameters.AddWithValue("@Amount", a.Budget);
            cmd.Parameters.AddWithValue("@Type", a.Entrytype);
            cmd.Parameters.AddWithValue("@Active", "N");

            result = cmd.ExecuteNonQuery();


            transaction.Commit();
            return result;
        }
        catch (Exception ex)
        {
            log.Error("Inside InsertSeedMoneyBudget of catch block ");
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

    public int getSeedMoneyBudgetExist(SeedMoney a)
    {
        con = new SqlConnection(str);
        con.Open();
        transaction = con.BeginTransaction();

        try
        {


            int result = 0;


            //cmdString = "select count(*) as count from SeedMoneyBudget_M where Amount='" + a.Budget + "'and Type='" + a.Entrytype + "'";
            cmdString = "select count(*) as count from SeedMoneyBudget_M where Amount=@Budget and Type=@Entrytype ";



            cmd = new SqlCommand(cmdString, con, transaction);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@Budget", a.Budget);
            cmd.Parameters.AddWithValue("@Entrytype", a.Entrytype);
            SqlDataReader sdr = cmd.ExecuteReader();
            int id = 0;
            while (sdr.Read())
            {


                if (!Convert.IsDBNull(sdr["count"]))
                {
                    id = (int)sdr["count"];
                }
                else if (Convert.IsDBNull(sdr["count"]))
                {
                    id = 0;
                }


            }
            sdr.Close();

            transaction.Commit();
            return id;
        }
        catch (Exception ex)
        {
            log.Error("Inside getSeedMoneyBudgetExist of catch block ");
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

    public int CheckSeedMoneyEntry(string p)
    {
        try
        {
            SeedMoney b = new SeedMoney();
            //cmdString = "select count(*) as Count from SeedMoneyActive where Active='Y' and Type  ='" + p + "'";
            cmdString = "select count(*) as Count from SeedMoneyActive where Active='Y' and Type=@p";

            con = new SqlConnection(str);
            con.Open();
            cmd = new SqlCommand(cmdString, con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@p", p);
            SqlDataReader sdr = cmd.ExecuteReader();

            int id = 0;
            while (sdr.Read())
            {

                if (!Convert.IsDBNull(sdr["Count"]))
                {
                    id = (int)sdr["Count"];
                }

            }
            return id;
        }
        catch (Exception ex)
        {
            log.Error("Inside CheckSeedMoneyEntry of catch block ");
            log.Error(ex.Message);
            log.Error(ex.StackTrace);
            throw ex;
        }

        finally
        {
            con.Close();
        }
    }

    public int checkexistingSeedmoneyentry(string type)
    {
        try
        {
            SeedMoney b = new SeedMoney();
            //cmdString = "select COUNT(*) as count from SeedMoneyDetails where Status in('NEW') and EntryType='" + type + "'";
            cmdString = "select COUNT(*) as count from SeedMoneyDetails where Status in('NEW') and EntryType=@type ";

            con = new SqlConnection(str);
            con.Open();
            cmd = new SqlCommand(cmdString, con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@type", type);
            SqlDataReader sdr = cmd.ExecuteReader();

            int Count = 0;
            while (sdr.Read())
            {

                if (!Convert.IsDBNull(sdr["count"]))
                {
                    Count = (int)sdr["count"];
                }

            }
            return Count;
        }
        catch (Exception ex)
        {
            log.Error("Inside getSeedMoneydisabledStatus of catch block ");
            log.Error(ex.Message);
            log.Error(ex.StackTrace);
            throw ex;
        }

        finally
        {
            con.Close();
        }
    }


    public int UpdateManageSeedMoneyEntry(SeedMoney a, ArrayList listFaculty)
    {
        con = new SqlConnection(str);
        con.Open();
        transaction = con.BeginTransaction();

        try
        {


            int result = 0;
            int result2 = 0;
            cmdString = "update SeedMoneyActive set Active=@Active,Comments=@Comments,FromDate=@FromDate,ToDate=@ToDate,Type=@Type,Status=@Status where Id=@Id";

            cmd = new SqlCommand(cmdString, con, transaction);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@Id", a.id);
            cmd.Parameters.AddWithValue("@Active", a.Enable);
            cmd.Parameters.AddWithValue("@Comments", a.EnableRemarks);
            //cmd.Parameters.AddWithValue("@Note", a.Note);
            //cmd.Parameters.AddWithValue("@UpdatedBy", a.UpdatedBy);
            //cmd.Parameters.AddWithValue("@UpdatedDate", a.EnableDate);
            cmd.Parameters.AddWithValue("@FromDate", a.Fromdate);
            cmd.Parameters.AddWithValue("@ToDate", a.Todate);
            cmd.Parameters.AddWithValue("@Type", a.Entrytype);
            cmd.Parameters.AddWithValue("@Status", a.Status);

            result = cmd.ExecuteNonQuery();

            if (result >= 1)
            {
                cmdString = "delete  from SeedMoneyCatagoryMap where id=@Id";
                cmd = new SqlCommand(cmdString, con, transaction);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Id", a.id);
                result2 = cmd.ExecuteNonQuery();

                for (int i = 0; i < listFaculty.Count; i++)
                {
                    cmdString = "insert into SeedMoneyCatagoryMap(id,categotyid)values(@id,@categotyid)";

                    cmd = new SqlCommand(cmdString, con, transaction);
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue("@Id", a.id);
                    cmd.Parameters.AddWithValue("@categotyid", listFaculty[i]);

                    result = cmd.ExecuteNonQuery();
                }
            }
            transaction.Commit();
            return result;
        }
        catch (Exception ex)
        {
            log.Error("Inside UpdateManageSeedMoneyEntry of catch block ");
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

    public int EnableSeedMoneyEntry(SeedMoney a, ArrayList listFaculty)
    {
        con = new SqlConnection(str);
        con.Open();
        transaction = con.BeginTransaction();

        try
        {


            int result = 0;
            cmdString = "insert into SeedMoneyActive(Active,Comments,UpdatedBy,UpdatedDate,FromDate,ToDate,Type,Status)values(@Active,@Comments,@UpdatedBy,@UpdatedDate,@FromDate,@ToDate,@Type,@Status)";

            cmd = new SqlCommand(cmdString, con, transaction);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("@Active", a.Enable);
            cmd.Parameters.AddWithValue("@Comments", a.EnableRemarks);
            //cmd.Parameters.AddWithValue("@Note", a.Note);
            cmd.Parameters.AddWithValue("@UpdatedBy", a.UpdatedBy);
            cmd.Parameters.AddWithValue("@UpdatedDate", a.EnableDate);
            cmd.Parameters.AddWithValue("@FromDate", a.Fromdate);
            cmd.Parameters.AddWithValue("@ToDate", a.Todate);
            cmd.Parameters.AddWithValue("@Type", a.Entrytype);
            cmd.Parameters.AddWithValue("@Status", a.Status);

            result = cmd.ExecuteNonQuery();

            if (result >= 1)
            {
                cmdString = "select Id from SeedMoneyActive";


                cmd = new SqlCommand(cmdString, con, transaction);
                cmd.CommandType = CommandType.Text;
                SqlDataReader sdr = cmd.ExecuteReader();
                int id = 0;
                while (sdr.Read())
                {


                    if (!Convert.IsDBNull(sdr["Id"]))
                    {
                        id = (int)sdr["Id"];
                    }
                    else if (Convert.IsDBNull(sdr["Id"]))
                    {
                        id = 0;
                    }


                }
                HttpContext.Current.Session["Pubseed"] = id;
                sdr.Close();

                for (int i = 0; i < listFaculty.Count; i++)
                {
                    cmdString = "insert into SeedMoneyCatagoryMap(id,categotyid)values(@id,@categotyid)";

                    cmd = new SqlCommand(cmdString, con, transaction);
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@categotyid", listFaculty[i]);

                    result = cmd.ExecuteNonQuery();
                }
            }
            transaction.Commit();
            return result;
        }
        catch (Exception ex)
        {
            log.Error("Inside EnableSeedMoneyEntry of catch block ");
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

    public ArrayList checkexistingCycle(string Fromdate1, string ToDate1, string type)
    {
        try
        {
            int result = 0;
            SeedMoney b = new SeedMoney();
            //cmdString = "select Id from SeedMoneyActive where Active='Y' and Type ='" + type + "'";
            cmdString = "select Id from SeedMoneyActive where Active='Y' and Type =@type ";

            con = new SqlConnection(str);
            con.Open();
            cmd = new SqlCommand(cmdString, con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@type", type);
            SqlDataReader sdr = cmd.ExecuteReader();
            ArrayList idlist = new ArrayList();
            ArrayList idlist1 = new ArrayList();
            int id = 0;
            while (sdr.Read())
            {

                if (!Convert.IsDBNull(sdr["Id"]))
                {
                    id = (int)sdr["Id"];
                    idlist.Add(id);
                }

            }
            sdr.Close();
            for (int i = 0; i < idlist.Count; i++)
            {
                //cmdString = "select * from SeedMoneyActive where (CAST('" + Fromdate1 + "' AS DATE) between CAST(FromDate AS DATE) And CAST(ToDate AS DATE)) and (CAST('" + ToDate1 + "' AS DATE) between CAST(FromDate AS DATE) And CAST(ToDate AS DATE)) and  Id=" + idlist[i] + "";
                cmdString = "select * from SeedMoneyActive where (CAST(@Fromdate1  AS DATE) between CAST(FromDate AS DATE) And CAST(ToDate AS DATE)) and (CAST(@ToDate1  AS DATE) between CAST(FromDate AS DATE) And CAST(ToDate AS DATE)) and  Id=@Id";


                cmd = new SqlCommand(cmdString, con, transaction);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Fromdate1", Fromdate1);
                cmd.Parameters.AddWithValue("@ToDate1", ToDate1);
                cmd.Parameters.AddWithValue("@Id", idlist[i]);
                SqlDataReader sdr1 = cmd.ExecuteReader();
                //ArrayList idlist = new ArrayList();
                //int id = 0;
                while (sdr1.Read())
                {

                    if (!Convert.IsDBNull(sdr1["Id"]))
                    {
                        id = (int)sdr1["Id"];
                        idlist1.Add(id);
                    }

                }
                sdr1.Close();
                //cmd.Parameters.AddWithValue("@Id", idlist[i]);
                //cmd.Parameters.AddWithValue("@Active", "N");
                //cmd.Parameters.AddWithValue("@Status", "CAN");


                //cmd.Parameters.AddWithValue("@Comments", a.EnableRemarks);
                //cmd.Parameters.AddWithValue("@Note", a.Note);
                ////cmd.Parameters.AddWithValue("@UpdatedBy", a.UpdatedBy);
                ////cmd.Parameters.AddWithValue("@UpdatedDate", a.EnableDate);
                //cmd.Parameters.AddWithValue("@FromDate", a.Fromdate);
                //cmd.Parameters.AddWithValue("@ToDate", a.Todate);
                //cmd.Parameters.AddWithValue("@Type", a.Entrytype);
                //cmd.Parameters.AddWithValue("@Status", "N");

                result = cmd.ExecuteNonQuery();
            }

            return idlist1;
        }
        catch (Exception ex)
        {
            log.Error("Inside CheckSeedMoneyEntry of catch block ");
            log.Error(ex.Message);
            log.Error(ex.StackTrace);
            throw ex;
        }

        finally
        {
            con.Close();
        }
    }

    public int disableSeedMoneyEntry(string p)
    {
        try
        {
            int result = 0;
            SeedMoney b = new SeedMoney();
            //cmdString = "select Id from SeedMoneyActive where Active='Y' and Type ='" + p + "'";
            cmdString = "select Id from SeedMoneyActive where Active='Y' and Type =@p";

            con = new SqlConnection(str);
            con.Open();
            cmd = new SqlCommand(cmdString, con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@p", p);
            SqlDataReader sdr = cmd.ExecuteReader();
            ArrayList idlist = new ArrayList();
            int id = 0;
            while (sdr.Read())
            {

                if (!Convert.IsDBNull(sdr["Id"]))
                {
                    id = (int)sdr["Id"];
                    idlist.Add(id);
                }

            }
            sdr.Close();
            for (int i = 0; i < idlist.Count; i++)
            {
                //cmdString = "update SeedMoneyActive set Active=@Active,Status=@Status where Id=@Id";
                cmdString = "update SeedMoneyActive set Active=@Active where Id=@Id";

                cmd = new SqlCommand(cmdString, con, transaction);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Id", idlist[i]);
                cmd.Parameters.AddWithValue("@Active", "N");
                //cmd.Parameters.AddWithValue("@Status", "COM");


                //cmd.Parameters.AddWithValue("@Comments", a.EnableRemarks);
                //cmd.Parameters.AddWithValue("@Note", a.Note);
                ////cmd.Parameters.AddWithValue("@UpdatedBy", a.UpdatedBy);
                ////cmd.Parameters.AddWithValue("@UpdatedDate", a.EnableDate);
                //cmd.Parameters.AddWithValue("@FromDate", a.Fromdate);
                //cmd.Parameters.AddWithValue("@ToDate", a.Todate);
                //cmd.Parameters.AddWithValue("@Type", a.Entrytype);
                //cmd.Parameters.AddWithValue("@Status", "N");

                result = cmd.ExecuteNonQuery();
            }

            return result;
        }
        catch (Exception ex)
        {
            log.Error("Inside disableSeedMoneyEntry of catch block ");
            log.Error(ex.Message);
            log.Error(ex.StackTrace);
            throw ex;
        }

        finally
        {
            con.Close();
        }
    }

    public DataSet SelectSeedMoneyActive()
    {
        log.Debug("SelectSeedMoneyActive:- Inside SelectSeedMoneyActive function-to Check Seed Money Active Status");
        con = new SqlConnection(str);
        con.Open();

        //cmd = new SqlCommand("Select id,Type,FromDate,ToDate from SeedMoneyActive where Active='Y' and Status='APP'", con);
        cmd = new SqlCommand("Select id,Type,FromDate,ToDate from SeedMoneyActive where Status='APP'", con);
        cmd.CommandType = CommandType.Text;
        DataSet ds = new DataSet();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds);
        con.Close();
        return ds;
    }

    public bool UpdateSeedMoneyAciveFlagY(string id1, string type)
    {
        try
        {
            con = new SqlConnection(str);
            con.Open();
            bool result = false;
            //cmdString = "update SeedMoneyActive set Active='Y' where Id=" + id1 + " and Type='" + type + "'";
            cmdString = "update SeedMoneyActive set Active='Y' where Id=@id1  and Type=@type";


            cmd = new SqlCommand(cmdString, con, transaction);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@id1", id1);
            cmd.Parameters.AddWithValue("@type", type);


            result = Convert.ToBoolean(cmd.ExecuteNonQuery());

            return result;
        }
        catch (Exception ex)
        {
            log.Error("Inside UpdateSeedMoneyAciveFlagY of catch block ");
            log.Error(ex.Message);
            log.Error(ex.StackTrace);
            throw ex;
        }

        finally
        {
            con.Close();
        }
    }

    public bool UpdateSeedMoneyAciveFlag(string id1, string type)
    {
        try
        {
            con = new SqlConnection(str);
            con.Open();
            bool result = false;
            //cmdString = "update SeedMoneyActive set Active='N' where Id=" + id1 + " and Type='" + type + "'";
            cmdString = "update SeedMoneyActive set Active='N' where Id=@id1  and Type=@type ";


            cmd = new SqlCommand(cmdString, con, transaction);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@id1", id1);
            cmd.Parameters.AddWithValue("@type", type);

            result = Convert.ToBoolean(cmd.ExecuteNonQuery());

            return result;
        }
        catch (Exception ex)
        {
            log.Error("Inside UpdateSeedMoneyAciveFlag of catch block ");
            log.Error(ex.Message);
            log.Error(ex.StackTrace);
            throw ex;
        }

        finally
        {
            con.Close();
        }
    }

    public SeedMoney getSeedMoneyActiveStatus(string EntryType, string p)
    {
        try
        {
            SeedMoney b = new SeedMoney();
            //cmdString = "select Id from SeedMoneyActive where Active='Y' and Type in('B','" + EntryType + "') and ('" + p + "'>=FromDate)and ('" + p + "'<=ToDate)";
            //cmdString = "select Id from SeedMoneyActive where Active='Y' and Type ='" + EntryType + "'";
            cmdString = "select Id from SeedMoneyActive where Active='Y' and Type =@EntryType and Status='APP' ";

            con = new SqlConnection(str);
            con.Open();
            cmd = new SqlCommand(cmdString, con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@EntryType", EntryType);
            SqlDataReader sdr = cmd.ExecuteReader();

            int id = 0;
            while (sdr.Read())
            {

                if (!Convert.IsDBNull(sdr["Id"]))
                {
                    b.id = (int)sdr["Id"];
                }
                //if (!Convert.IsDBNull(sdr["Comments"]))
                //{
                //    b.EnableRemarks = (string)sdr["Comments"];
                //}
                //if (!Convert.IsDBNull(sdr["Note"]))
                //{
                //    b.Note = (string)sdr["Note"];
                //}
            }
            return b;
        }
        catch (Exception ex)
        {
            log.Error("Inside getSeedMoneyActiveStatus of catch block ");
            log.Error(ex.Message);
            log.Error(ex.StackTrace);
            throw ex;
        }

        finally
        {
            con.Close();
        }
    }

    public string selectMemberType(string empcode, string referenceid)
    {
        try
        {
           //cmdString = "select MUNonMU from Publishcation_Author where EmployeeCode='" + empcode + "'and TypeOfEntry+PaublicationID='" + referenceid + "'";
            cmdString = "select MUNonMU from Publishcation_Author where EmployeeCode=@empcode and TypeOfEntry+PaublicationID=@referenceid ";

            con = new SqlConnection(str);
            con.Open();
            cmd = new SqlCommand(cmdString, con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@empcode", empcode);
            cmd.Parameters.AddWithValue("@referenceid", referenceid);
            SqlDataReader sdr = cmd.ExecuteReader();

            string MUNonMU = "";
            while (sdr.Read())
            {

                if (!Convert.IsDBNull(sdr["MUNonMU"]))
                {
                    MUNonMU = (string)sdr["MUNonMU"];
                }
               
            }
            return MUNonMU;
        }
        catch (Exception ex)
        {
            log.Error("Inside selectMemberType of catch block ");
            log.Error(ex.Message);
            log.Error(ex.StackTrace);
            throw ex;
        }

        finally
        {
            con.Close();
        }
    }

    public bool UpdateFeedbackReviewTracker(FeedbackClass feedback, System.DateTime Date, string MemberID,string ID, string EntryType)
    {
        bool res = false;
        log.Debug("Inside function UpdateFeedbackReviewTracker of of MemberID : " + MemberID);
        try
        {
            con = new SqlConnection(str);
            con.Open();
            transaction = con.BeginTransaction();
            cmd = new SqlCommand("update FeedBack_Tracker set Q1=@Q1,Q2=@Q2,Q3=@Q3,Q4=@Q4,CreatedDate=@CreatedDate WHERE MemberID=@MemberID AND ID=@ID AND EntryType=@EntryType", con, transaction);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@MemberID", MemberID);
            cmd.Parameters.AddWithValue("@Q1", feedback.Q1);
            cmd.Parameters.AddWithValue("@Q2", feedback.Q2);
            cmd.Parameters.AddWithValue("@Q3", feedback.Q3);
            cmd.Parameters.AddWithValue("@Q4", feedback.Q4);
            //cmd.Parameters.AddWithValue("@Q5", feedback.Q5);
            //cmd.Parameters.AddWithValue("@Q6", feedback.Q6);
            //cmd.Parameters.AddWithValue("@Q7", feedback.Q7);
            cmd.Parameters.AddWithValue("@CreatedDate", Date);
            cmd.Parameters.AddWithValue("@EntryType",EntryType );
            cmd.Parameters.AddWithValue("@ID", ID);

            res = Convert.ToBoolean(cmd.ExecuteNonQuery());


            transaction.Commit();
            return res;
        }
        catch (Exception e)
        {
            log.Error("Inside Catch block of function UpdateFeedbackReviewTracker of of MemberID : " + MemberID);
            log.Error(e.Message);
            log.Error(e.StackTrace);

            throw e;
        }
        finally
        {
            con.Close();
        }
    }

    public bool InsertintoFeedbackReviewTracker(FeedbackClass feedback,DateTime Date, string MemberID, string EntryType, string ID, string Type)
    {
        bool res = false;
        int count = 0;

        log.Debug("Inside function InsertintoFeedbackReviewTracker of of MemberID : " + MemberID);
        try
        {
            con = new SqlConnection(str);
            con.Open();
            transaction = con.BeginTransaction();

            //cmdString = "Select count(*) as Count from FeedBack_Tracker where  MemberID='" + MemberID + "'";
            cmdString = "Select count(*) as Count from FeedBack_Tracker where  MemberID=@MemberID";

            cmd = new SqlCommand(cmdString, con, transaction);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@MemberID", MemberID);
            SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                if (!Convert.IsDBNull(sdr["Count"]))
                {
                    count = (int)sdr["Count"];
                }

            }
            sdr.Close();



                //cmd = new SqlCommand("insert into  FeedBack_Tracker (MemberID,Q1,Q2,Q3,Q4,Q5,Q6,Q7,Q8,CreatedDate,EntryType,ID,ProjectCreatedDate,PublicationUpdatedDate,ProjectUpdatedDate,Type) values (@MemberID,@Q1,@Q2,@Q3,@Q4,@Q5,@Q6,@Q7,@Q8,@CreatedDate,@EntryType,@ID,@ProjectCreatedDate,@PublicationUpdatedDate,@ProjectUpdatedDate,@Type)", con, transaction);
                //cmd.CommandType = CommandType.Text;
                 cmd = new SqlCommand("insert_into_FeedBack_Tracker", con, transaction);
                 cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MemberID", MemberID);
                cmd.Parameters.AddWithValue("@Q1", feedback.Q1);
                cmd.Parameters.AddWithValue("@Q2", feedback.Q2);
                cmd.Parameters.AddWithValue("@Q3", feedback.Q3);
                cmd.Parameters.AddWithValue("@Q4", feedback.Q4);
                cmd.Parameters.AddWithValue("@Q5", feedback.Q5);
                cmd.Parameters.AddWithValue("@Q6", feedback.Q6);
                cmd.Parameters.AddWithValue("@Q7", feedback.Q7);
                cmd.Parameters.AddWithValue("@Q8", feedback.Q8);
                cmd.Parameters.AddWithValue("@CreatedDate", Date);
                //cmd.Parameters.AddWithValue("@ID", ID);
                cmd.Parameters.AddWithValue("@ProjectCreatedDate", DateTime.Now);
                cmd.Parameters.AddWithValue("@Type", Type);
                cmd.Parameters.AddWithValue("@EntrNo", count+1);

                res = Convert.ToBoolean(cmd.ExecuteNonQuery());


                if (Type == "Pub")
                {

                    //cmd = new SqlCommand("update User_M set PubFeedbackLastDate=@Date  where User_Id='" + MemberID + "'", con, transaction);
                    cmd = new SqlCommand("update User_M set PubFeedbackLastDate=@Date  where User_Id=@MemberID", con, transaction);

                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@Date", Date);
                    cmd.Parameters.AddWithValue("@MemberID", MemberID);

                    res = Convert.ToBoolean(cmd.ExecuteNonQuery());
                }
                else
                {
                    //cmd = new SqlCommand("update User_M set PrjFeedbackLastDate=@Date where User_Id='" + MemberID + "'", con, transaction);
                    cmd = new SqlCommand("update User_M set PrjFeedbackLastDate=@Date where User_Id=@MemberID ", con, transaction);
               
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@Date", Date);
                    cmd.Parameters.AddWithValue("@MemberID", MemberID);
                    res = Convert.ToBoolean(cmd.ExecuteNonQuery());
                }
            transaction.Commit();
            return res;
        }
        catch (Exception e)
        {
            log.Error("Inside Catch block of function InsertintoFeedbackReviewTracker of of MemberID : " + MemberID);
            log.Error(e.Message);
            log.Error(e.StackTrace);

            throw e;
        }
        finally
        {
            con.Close();
        }
    }

    internal int gettotalmonths(DateTime fromdate, DateTime todaydate)
    {
        DateTime earlyDate = (todaydate > fromdate) ? fromdate.Date : todaydate.Date;
        DateTime lateDate = (todaydate > fromdate) ? todaydate.Date : fromdate.Date;

        // Start with 1 month's difference and keep incrementing
        // until we overshoot the late date
        int monthsDiff = 1;
        while (earlyDate.AddMonths(monthsDiff) <= lateDate)
        {
            monthsDiff++;
        }

        return monthsDiff - 1;
    }

    public FeedbackClass SelectFeedbackDetailsfromFeedbackTracker(string MemberID,string Type)
    {
        try
        {

            con.Open();
            //cmd = new SqlCommand("select *  FROM FeedBack_Tracker where MemberID='" + MemberID + "' and Type='" + Type + "'", con);
            cmd = new SqlCommand("select *  FROM FeedBack_Tracker where MemberID=@MemberID  and Type=@Type ", con);

            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@MemberID", MemberID);
            cmd.Parameters.AddWithValue("@Type", Type);
            SqlDataReader sdr = cmd.ExecuteReader();
            FeedbackClass det = new FeedbackClass();

            while (sdr.Read())
            {


                if (!Convert.IsDBNull(sdr["Q1"]))
                {
                    det.Q1 = (string)sdr["Q1"];
                }
                else if (!Convert.IsDBNull(sdr["Q1"]))
                {
                    det.Q1 = (string)sdr["Q1"];
                }

                if (!Convert.IsDBNull(sdr["Q2"]))
                {
                    det.Q2 = (string)sdr["Q2"];
                }
                else if (!Convert.IsDBNull(sdr["Q2"]))
                {
                    det.Q2 = (string)sdr["Q2"];
                }

                if (!Convert.IsDBNull(sdr["Q3"]))
                {
                    det.Q3 = (string)sdr["Q3"];
                }
                else if (!Convert.IsDBNull(sdr["Q3"]))
                {
                    det.Q3 = (string)sdr["Q3"];
                }

                if (!Convert.IsDBNull(sdr["Q4"]))
                {
                    det.Q4 = (string)sdr["Q4"];
                }
                else if (!Convert.IsDBNull(sdr["Q4"]))
                {
                    det.Q4 = (string)sdr["Q4"];
                }

                if (!Convert.IsDBNull(sdr["Q5"]))
                {
                    det.Q5 = (string)sdr["Q5"];
                }
                else if (!Convert.IsDBNull(sdr["Q5"]))
                {
                    det.Q5 = (string)sdr["Q5"];
                }

                if (!Convert.IsDBNull(sdr["Q6"]))
                {
                    det.Q6 = (string)sdr["Q6"];
                }
                else if (!Convert.IsDBNull(sdr["Q6"]))
                {
                    det.Q6 = (string)sdr["Q6"];
                }

                if (!Convert.IsDBNull(sdr["Q7"]))
                {
                    det.Q7 = (string)sdr["Q7"];
                }
                else if (!Convert.IsDBNull(sdr["Q7"]))
                {
                    det.Q7 = (string)sdr["Q7"];
                }

                else if (!Convert.IsDBNull(sdr["Q8"]))
                {
                    det.Q8 = (string)sdr["Q8"];
                }


            }
            return det;

        }
        catch (Exception ex)
        {
            log.Error("Error Inside SelectFeedbackDetailsfromFeedbackTracker function  of MemberID " + MemberID + " ");
            throw ex;
        }

        finally
        {
            con.Close();
        }
    }

    internal bool IsExeFile(byte[] FileU)
    {
        log.Debug("Inside IsExeFile function of checking EXE Files");

        try
        {

            Journal_DataObject J = new Journal_DataObject();
            var twoBytes = SubByteArray(FileU, 0, 2);
            return ((Encoding.UTF8.GetString(twoBytes) == "MZ") || (Encoding.UTF8.GetString(twoBytes) == "ZM"));
        }
        catch (Exception ex)
        {
            log.Error("Inside catch block of IsExeFile function of checking EXE Files ");
            log.Error(ex.Message);
            log.Error(ex.StackTrace);
            throw ex;
        }
    }
    private static byte[] SubByteArray(byte[] data, int index, int length)
    {
        byte[] result = new byte[length];
        Array.Copy(data, index, result, 0, length);
        return result;
    }
    public int GetImpactFactorDetails(JournalData JournalValueObj)
    {
        try
        {
            cmdString = "select COUNT(*) as COUNT from Journal_IF_Details where Id=@ID ";
            con = new SqlConnection(str);
            con.Open();
            cmd = new SqlCommand(cmdString, con);
            cmd.Parameters.Add("@ID", SqlDbType.VarChar, 15);
            cmd.Parameters["@ID"].Value = JournalValueObj.JournalID;

            cmd.CommandType = CommandType.Text;
            SqlDataReader sdr = cmd.ExecuteReader();
            int count = 0;

            while (sdr.Read())
            {

                if (!Convert.IsDBNull(sdr["COUNT"]))
                {
                    count = (int)sdr["COUNT"];
                }
                else if (Convert.IsDBNull(sdr["COUNT"]))
                {
                    count = 0;
                }

            }
            return count;
        }
        catch (Exception ex)
        {
            log.Error("Inside GetImpactFactorDetails catch block ");
            log.Error(ex.Message);
            log.Error(ex.StackTrace);
            throw ex;
        }

        finally
        {
            con.Close();
        }
    }

    public DataTable SelectImpactFactorDetails(JournalData JournalValueObj)
    {
        log.Debug("Inside SelectImpactFactorDetails function of Journal ID: " + JournalValueObj.JournalID);
        con = new SqlConnection(str);
        con.Open();
        IncentiveData data = new IncentiveData();
        try
        {
            cmdString = "select Year,ImpactFactor,FiveImpFact from Journal_IF_Details where Id=@JournalID";
            cmd = new SqlCommand(cmdString, con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@JournalID", JournalValueObj.JournalID);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable ds = new DataTable();
            da.Fill(ds);

            return ds;
        }
        catch (Exception ex)
        {
            log.Error("Inside SelectImpactFactorDetails catch block of Journal ID : " + JournalValueObj.JournalID);
            log.Error(ex.Message);
            log.Error(ex.StackTrace);
            throw (ex);
        }
        finally
        {
            cmd.Dispose();
            con.Close();
            cmd.Dispose();
        }
    }

    public int JournalEntrySaveImpactfactorChanges(JournalData JournalValueObj, ArrayList list, JournalData[] JDP)
    {
        log.Debug("Inside Journal_DataObject- JournalEntrySaveImpactfactorChanges function ");

        int result;
        int res = 0, res2 = 0, res1 = 0, res3 = 0;
        con = new SqlConnection(str);
        con.Open();
        transaction = con.BeginTransaction();
        try
        {




            cmdString = "delete from Journal_IF_Details where Id=@Id ";
            cmd = new SqlCommand(cmdString, con, transaction);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("@Id", JournalValueObj.JournalID);
            //cmd.Parameters.AddWithValue("@Year", list[i].ToString());
            res3 = cmd.ExecuteNonQuery();

            for (int i = 0; i < JDP.Length; i++)
            {
                int applicableYear2 = Convert.ToInt32(JDP[i].year) + 1;
                //string applicableYear3 = applicableYear2.ToString();
                //JournalValueObj.Year1 = applicableYear3;
                string Applicable2 = applicableYear2 + "-" + "06" + "-01";
                JournalValueObj.ApplicableYear = Applicable2;

                cmdString = " Insert Into Journal_IF_Details (Id,Year,ImpactFactor,Comments,fiveImpFact,ApplicableDate) Values (@Id,@Year,@ImpactFactor,@Comments,@fiveImpFact,@ApplicableDate) ";
                cmd = new SqlCommand(cmdString, con, transaction);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Id", HttpUtility.HtmlDecode(JournalValueObj.JournalID));
                cmd.Parameters.AddWithValue("@Year", HttpUtility.HtmlDecode(JDP[i].year));
                cmd.Parameters.AddWithValue("@ImpactFactor", HttpUtility.HtmlDecode(JDP[i].impctfact.ToString()));
                cmd.Parameters.AddWithValue("@Comments", HttpUtility.HtmlDecode(JournalValueObj.Comments.ToString()));
                cmd.Parameters.AddWithValue("@fiveImpFact", HttpUtility.HtmlDecode(JDP[i].fiveimpcrfact.ToString()));
                cmd.Parameters.AddWithValue("@ApplicableDate", HttpUtility.HtmlDecode(JournalValueObj.ApplicableYear.ToString()));
                res3 = cmd.ExecuteNonQuery();
            }



            transaction.Commit();
            return res3;
        }

        catch (Exception ex)
        {
            log.Error("Inside Journal_DataObject- JournalEntrySaveImpactfactorChanges block ");
            log.Error(ex.Message);
            log.Error(ex.StackTrace);
            transaction.Rollback();
            throw (ex);
        }
        finally
        {
            cmd.Dispose();
            con.Close();
            cmd.Dispose();
        }

    }
}