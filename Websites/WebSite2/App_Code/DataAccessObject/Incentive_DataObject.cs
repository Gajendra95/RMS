using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Incentive_DataObject
/// </summary>
public class Incentive_DataObject
{
    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    public string str;
    public string cmdString;
    public SqlConnection con;
    public SqlCommand cmd;

    SqlTransaction transaction;

    public Incentive_DataObject()
    {
        str = ConfigurationManager.ConnectionStrings["RMSConnectionString"].ConnectionString;
        cmdString = "";
        con = new SqlConnection(str);
        cmd = new SqlCommand(cmdString, con);

    }


    public DataSet SelectPendingProcessedPublications(PublishData data)
    {
        try
        {

            con.Open();
            cmd = new SqlCommand("Incentive_SelectPublications", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID", data.PaublicationID);
            cmd.Parameters.AddWithValue("@Title", data.JournalTitle);
            cmd.Parameters.AddWithValue("@Type", data.TypeOfEntry);
            cmd.Parameters.AddWithValue("@BulkYear", data.bulkpublicationyear);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

        catch (Exception ex)
        {
            log.Error(ex.Message);
            log.Error(ex.StackTrace);
            throw ex;
        }

        finally
        {
            con.Close();
        }
    }

    public DataTable SelectAuthorDetails(string publicationid, string typeofentry)
    {
        log.Debug("Inside SelectAuthorDetails function, PublicationID: " + typeofentry + "Type Of Entry: " + typeofentry);
        con.Open();
        DataTable ds = null;
        try
        {
            SqlDataAdapter da;

            cmd = new SqlCommand("Incentive_SelectAuthorDetails", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@PaublicationID", SqlDbType.VarChar, 15);
            cmd.Parameters["@PaublicationID"].Value = publicationid;
            cmd.Parameters.Add("@TypeOfEntry", SqlDbType.VarChar, 5);
            cmd.Parameters["@TypeOfEntry"].Value = typeofentry;
            da = new SqlDataAdapter(cmd);
            ds = new DataTable();
            da.Fill(ds);
            return ds;
        }

        catch (Exception ex)
        {
            log.Error("Inside catch block function SelectAuthorDetails function, PublicationID: " + typeofentry + "Type Of Entry: " + typeofentry);
            log.Error(ex.Message);
            log.Error(ex.StackTrace);
            throw ex;
        }

        finally
        {
            con.Close();
        }
    }

    public bool InsertIncentivePointToAuthor(PublishData[] JD, PublishData data)
    {
        log.Debug("Inside InsertIncentivePointToAuthor function of Publication ID: " + data.PaublicationID + " Type of Entry : " + data.TypeOfEntry);
        con.Open();
        transaction = con.BeginTransaction();
        bool result = false;
        try
        {
            for (int i = 0; i < JD.Length; i++)
            {
                cmd = new SqlCommand("Incentive_InsertIncentivePoint", con, transaction);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PaublicationID", data.PaublicationID);
                cmd.Parameters.AddWithValue("@TypeOfEntry", data.TypeOfEntry);
                cmd.Parameters.AddWithValue("@EmployeeCode", JD[i].EmployeeCode);
                cmd.Parameters.AddWithValue("@TotalPoints", JD[i].TotalPoint);
                cmd.Parameters.AddWithValue("@BasePoint", JD[i].BasePoint);
                cmd.Parameters.AddWithValue("@SNIPSJRPoint", JD[i].SNIPSJRPoint);
                cmd.Parameters.AddWithValue("@ThresholdPoint", JD[i].ThresholdPoint);
                result = Convert.ToBoolean(cmd.ExecuteNonQuery());
            }
            if (result == true)
            {
                cmd = new SqlCommand("Incentive_UpdateIncentiveStatus", con, transaction);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PaublicationID", data.PaublicationID);
                cmd.Parameters.AddWithValue("@TypeOfEntry", data.TypeOfEntry);
                cmd.Parameters.AddWithValue("@Status", "PRC");
                result = Convert.ToBoolean(cmd.ExecuteNonQuery());
            }
            transaction.Commit();
            log.Info("The Publication with id " + data.PaublicationID + " and  Type of Entry : " + data.TypeOfEntry + "is processed for incentive point entry");
            return result;
        }
        catch (Exception ex)
        {
            transaction.Rollback();
            log.Error("Inside Incentive_DataObjects- InsertIncentivePointToAuthor function, PublicationID: " + data.PaublicationID + "Type Of Entry: " + data.TypeOfEntry);
            log.Error(ex.Message);
            log.Error(ex.StackTrace);
            throw ex;
        }

        finally
        {
            con.Close();
        }

    }



    public bool ApproveIncentiveStatus(PublishData[] JD, PublishData data)
    {
        log.Debug("Inside ApproveIncentiveStatus function : User Name :" + HttpContext.Current.Session["UserName"] + "Role :" + HttpContext.Current.Session["RoleName"]); 
        log.Debug("Inside ApproveIncentiveStatus function of publication id : " + data.PaublicationID + " and type of entry : " + data.TypeOfEntry);
        con.Open();
        transaction = con.BeginTransaction();
        bool result = false;
        try
        {
            for (int i = 0; i < JD.Length; i++)
            {
                cmd = new SqlCommand("Incentive_SelectMemberCurrentBalance", con, transaction);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MemberId", JD[i].EmployeeCode);
                SqlDataReader sdr1 = cmd.ExecuteReader();
                IncentivePoint points = new IncentivePoint();
                if (sdr1.HasRows)
                {
                    while (sdr1.Read())
                    {
                        if (!Convert.IsDBNull(sdr1["Currentbalance"]))
                        {
                            points.CurrentBalance = (double)sdr1["Currentbalance"];
                        }
                        if (!Convert.IsDBNull(sdr1["OldBalance"]))
                        {
                            points.OpeningBalance = (double)sdr1["OldBalance"];
                        }
                        if (!Convert.IsDBNull(sdr1["MemberId"]))
                        {
                            points.MemberId = (string)sdr1["MemberId"];
                        }
                    }
                }

                sdr1.Close();

                if (points.MemberId == null)
                {
                    cmd = new SqlCommand("Incentive_InsertMemberIncentivePointSummary", con, transaction);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MemberId", JD[i].EmployeeCode);
                    cmd.Parameters.AddWithValue("@Membertype", JD[i].MUNonMU);
                    cmd.Parameters.AddWithValue("@Oldbalance", points.OpeningBalance);
                    cmd.Parameters.AddWithValue("@Currentbalance", points.CurrentBalance + JD[i].TotalPoint);
                    cmd.Parameters.AddWithValue("@LastTransactionDate", DateTime.Now);
                    cmd.Parameters.AddWithValue("@UserId", HttpContext.Current.Session["UserId"].ToString());
                    cmd.Parameters.AddWithValue("@Date", DateTime.Now);

                    result = Convert.ToBoolean(cmd.ExecuteNonQuery());
                }
                else
                {
                    cmd = new SqlCommand("Incentive_UpdateMemberIncentivePointSummary", con, transaction);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MemberId", JD[i].EmployeeCode);
                    if (points.CurrentBalance != 0.0)
                    {
                        cmd.Parameters.AddWithValue("@Currentbalance", points.CurrentBalance + JD[i].TotalPoint);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Currentbalance", JD[i].TotalPoint);
                    }
                    cmd.Parameters.AddWithValue("@LastTransactionDate", DateTime.Now);
                    cmd.Parameters.AddWithValue("@UserId", HttpContext.Current.Session["UserId"].ToString());
                    cmd.Parameters.AddWithValue("@Date", DateTime.Now);

                    result = Convert.ToBoolean(cmd.ExecuteNonQuery());
                }
                if (result == true)
                {
                    cmd = new SqlCommand("Incentive_SelectMaxEntryNo", con, transaction);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MemberId", JD[i].EmployeeCode);
                    SqlDataReader sdr = cmd.ExecuteReader();
                    int entryno = 0;
                    if (sdr.HasRows)
                    {
                        while (sdr.Read())
                        {
                            if (!Convert.IsDBNull(sdr["EntryNo"]))
                            {
                                entryno = (int)sdr["EntryNo"];
                            }

                        }
                    }
                    else
                    {
                        entryno = 0;
                    }
                    sdr.Close();

                    cmd = new SqlCommand("Incentive_InsertMemberIncentivePointTransaction", con, transaction);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MemberId", JD[i].EmployeeCode);
                    cmd.Parameters.AddWithValue("@EntryNo", entryno + 1);
                    cmd.Parameters.AddWithValue("@TransactionType", JD[i].TransactionType);
                    cmd.Parameters.AddWithValue("@ReferenceNumber", data.TypeOfEntry + data.PaublicationID);
                    cmd.Parameters.AddWithValue("@TotalPoint", JD[i].TotalPoint);
                    cmd.Parameters.AddWithValue("@UtilizationDate", DBNull.Value);
                    cmd.Parameters.AddWithValue("@BasePoint", JD[i].BasePoint);
                    cmd.Parameters.AddWithValue("@SNIPSJRPoint", JD[i].SNIPSJRPoint);
                    cmd.Parameters.AddWithValue("@ThresholdPoint", JD[i].ThresholdPoint);
                    cmd.Parameters.AddWithValue("@Remarks", DBNull.Value);
                    cmd.Parameters.AddWithValue("@UserId", HttpContext.Current.Session["UserId"].ToString());
                    cmd.Parameters.AddWithValue("@Date", DateTime.Now);
                    result = Convert.ToBoolean(cmd.ExecuteNonQuery());


                    cmd = new SqlCommand("Incentive_SelectMaxPublicationEntryNoForYear", con, transaction);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MemberId", JD[i].EmployeeCode);
                    cmd.Parameters.AddWithValue("@PublishYear", JD[i].PublishJAYear);
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
                    cmd = new SqlCommand("Incentive_InsertYearwisePublication", con, transaction);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MemberId", JD[i].EmployeeCode);
                    cmd.Parameters.AddWithValue("@PublishYear", JD[i].PublishJAYear);
                    cmd.Parameters.AddWithValue("@EntryNo", count + 1);
                    cmd.Parameters.AddWithValue("@PublicationID", data.TypeOfEntry + data.PaublicationID);
                    cmd.Parameters.AddWithValue("@UserId", HttpContext.Current.Session["UserId"].ToString());
                    cmd.Parameters.AddWithValue("@Date", DateTime.Now);
                    result = Convert.ToBoolean(cmd.ExecuteNonQuery());

                    cmd = new SqlCommand("Incentive_InsertYearwisePublicationSummary", con, transaction);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MemberId", JD[i].EmployeeCode);
                    cmd.Parameters.AddWithValue("@PublishYear", JD[i].PublishJAYear);
                    cmd.Parameters.AddWithValue("@UserId", HttpContext.Current.Session["UserId"].ToString());
                    cmd.Parameters.AddWithValue("@Date", DateTime.Now);
                    result = Convert.ToBoolean(cmd.ExecuteNonQuery());

                }


                if (Convert.ToUInt32(JD[i].PublishJAYear) >= 2018 && Convert.ToUInt32(JD[i].PublishJAMonth) >= 7)
                {
                    if (JD[i].TotalPoint > 0.0)
                    {
                        cmd = new SqlCommand("select MAX(EntryNo) as EntryNo from Quartile_Author_LimitCount_Details where MemberId=@MemberId", con, transaction);
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@MemberId", JD[i].EmployeeCode);
                        SqlDataReader sdr = cmd.ExecuteReader();
                        int entryno = 0;
                        if (sdr.HasRows)
                        {
                            while (sdr.Read())
                            {
                                if (!Convert.IsDBNull(sdr["EntryNo"]))
                                {
                                    entryno = (int)sdr["EntryNo"];
                                }

                            }
                        }
                        else
                        {
                            entryno = 0;
                        }
                        sdr.Close();

                        cmd = new SqlCommand("Incentive_Insert_Quartile_Author_LimitCount_Details", con, transaction);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MemberId", JD[i].EmployeeCode);
                        cmd.Parameters.AddWithValue("@Year", JD[i].PublishJAYear);
                        cmd.Parameters.AddWithValue("@Quartile", JD[i].QuartileOnIncentivize);
                        cmd.Parameters.AddWithValue("@EntryNo", entryno + 1);
                        cmd.Parameters.AddWithValue("@PublicationId", data.PaublicationID);
                        cmd.Parameters.AddWithValue("@UserId", HttpContext.Current.Session["UserId"].ToString());
                        cmd.Parameters.AddWithValue("@Date", DateTime.Now);
                        result = Convert.ToBoolean(cmd.ExecuteNonQuery());

                        cmd = new SqlCommand("	select COUNT(EntryNo) as EntryNo from Quartile_Author_LimitCount_Details where MemberId=@MemberId and Year=@PublishYear and Isdeleted='N'", con, transaction);
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@MemberId", JD[i].EmployeeCode);
                        cmd.Parameters.AddWithValue("@PublishYear", JD[i].PublishJAYear);
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


                        if (count == 1)
                        {
                            cmd = new SqlCommand("Incentive_Insert_Quartile_Author_LimitCount_Summary", con, transaction);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@MemberId", JD[i].EmployeeCode);
                            cmd.Parameters.AddWithValue("@Year", JD[i].PublishJAYear);
                            cmd.Parameters.AddWithValue("@Quartile", JD[i].QuartileOnIncentivize);
                            cmd.Parameters.AddWithValue("@Count", count);
                            result = Convert.ToBoolean(cmd.ExecuteNonQuery());
                        }
                        else
                        {
                            cmd = new SqlCommand("Update Quartile_Author_LimitCount_Summary set Count=@Count,Quartile=@Quartile where MemberId=@MemberId and Year=@Year", con, transaction);
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.AddWithValue("@MemberId", JD[i].EmployeeCode);
                            cmd.Parameters.AddWithValue("@Year", JD[i].PublishJAYear);
                            cmd.Parameters.AddWithValue("@Quartile", JD[i].QuartileOnIncentivize);
                            cmd.Parameters.AddWithValue("@Count", count);
                            result = Convert.ToBoolean(cmd.ExecuteNonQuery());
                        }


                        cmd = new SqlCommand(" Update Publication set QuartileOnIncentivize=@Quartile where PublicationID=@PaublicationID and TypeOfEntry=@TypeOfEntry", con, transaction);
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@PaublicationID", data.PaublicationID);
                        cmd.Parameters.AddWithValue("@TypeOfEntry", data.TypeOfEntry);
                        cmd.Parameters.AddWithValue("@Quartile", JD[i].QuartileOnIncentivize);
                        result = Convert.ToBoolean(cmd.ExecuteNonQuery());  
                    }
                    cmd = new SqlCommand(" Update Publishcation_Author set IncentiveStatus=@IncentiveStatus where PaublicationID=@PaublicationID and TypeOfEntry=@TypeOfEntry and EmployeeCode=@EmployeeCode", con, transaction);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@PaublicationID", data.PaublicationID);
                    cmd.Parameters.AddWithValue("@TypeOfEntry", data.TypeOfEntry);
                    cmd.Parameters.AddWithValue("@IncentiveStatus", JD[i].IncentivePointSatatus);
                    cmd.Parameters.AddWithValue("@EmployeeCode", JD[i].EmployeeCode);
                    result = Convert.ToBoolean(cmd.ExecuteNonQuery());
                }
                else if (Convert.ToUInt32(JD[i].PublishJAYear) >= 2019 && Convert.ToUInt32(JD[i].PublishJAMonth) >= 1)
                {
                    if (JD[i].TotalPoint > 0.0)
                    {
                        cmd = new SqlCommand("select MAX(EntryNo) as EntryNo from Quartile_Author_LimitCount_Details where MemberId=@MemberId", con, transaction);
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@MemberId", JD[i].EmployeeCode);
                        SqlDataReader sdr = cmd.ExecuteReader();
                        int entryno = 0;
                        if (sdr.HasRows)
                        {
                            while (sdr.Read())
                            {
                                if (!Convert.IsDBNull(sdr["EntryNo"]))
                                {
                                    entryno = (int)sdr["EntryNo"];
                                }

                            }
                        }
                        else
                        {
                            entryno = 0;
                        }
                        sdr.Close();

                        cmd = new SqlCommand("Incentive_Insert_Quartile_Author_LimitCount_Details", con, transaction);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MemberId", JD[i].EmployeeCode);
                        cmd.Parameters.AddWithValue("@Year", JD[i].PublishJAYear);
                        cmd.Parameters.AddWithValue("@Quartile", JD[i].QuartileOnIncentivize);
                        cmd.Parameters.AddWithValue("@EntryNo", entryno + 1);
                        cmd.Parameters.AddWithValue("@PublicationId", data.PaublicationID);
                        cmd.Parameters.AddWithValue("@UserId", HttpContext.Current.Session["UserId"].ToString());
                        cmd.Parameters.AddWithValue("@Date", DateTime.Now);
                        result = Convert.ToBoolean(cmd.ExecuteNonQuery());

                        cmd = new SqlCommand("	select COUNT(EntryNo) as EntryNo from Quartile_Author_LimitCount_Details where MemberId=@MemberId and Year=@PublishYear and Isdeleted='N'", con, transaction);
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@MemberId", JD[i].EmployeeCode);
                        cmd.Parameters.AddWithValue("@PublishYear", JD[i].PublishJAYear);
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


                        if (count == 1)
                        {
                            cmd = new SqlCommand("Incentive_Insert_Quartile_Author_LimitCount_Summary", con, transaction);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@MemberId", JD[i].EmployeeCode);
                            cmd.Parameters.AddWithValue("@Year", JD[i].PublishJAYear);
                            cmd.Parameters.AddWithValue("@Quartile", JD[i].QuartileOnIncentivize);
                            cmd.Parameters.AddWithValue("@Count", count);
                            result = Convert.ToBoolean(cmd.ExecuteNonQuery());
                        }
                        else
                        {
                            cmd = new SqlCommand("Update Quartile_Author_LimitCount_Summary set Count=@Count,Quartile=@Quartile where MemberId=@MemberId and Year=@Year", con, transaction);
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.AddWithValue("@MemberId", JD[i].EmployeeCode);
                            cmd.Parameters.AddWithValue("@Year", JD[i].PublishJAYear);
                            cmd.Parameters.AddWithValue("@Quartile", JD[i].QuartileOnIncentivize);
                            cmd.Parameters.AddWithValue("@Count", count);
                            result = Convert.ToBoolean(cmd.ExecuteNonQuery());
                        }


                        cmd = new SqlCommand(" Update Publication set QuartileOnIncentivize=@Quartile where PublicationID=@PaublicationID and TypeOfEntry=@TypeOfEntry", con, transaction);
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@PaublicationID", data.PaublicationID);
                        cmd.Parameters.AddWithValue("@TypeOfEntry", data.TypeOfEntry);
                        cmd.Parameters.AddWithValue("@Quartile", JD[i].QuartileOnIncentivize);
                        result = Convert.ToBoolean(cmd.ExecuteNonQuery());
                    }
                    cmd = new SqlCommand(" Update Publishcation_Author set IncentiveStatus=@IncentiveStatus where PaublicationID=@PaublicationID and TypeOfEntry=@TypeOfEntry and EmployeeCode=@EmployeeCode", con, transaction);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@PaublicationID", data.PaublicationID);
                    cmd.Parameters.AddWithValue("@TypeOfEntry", data.TypeOfEntry);
                    cmd.Parameters.AddWithValue("@IncentiveStatus", JD[i].IncentivePointSatatus);
                    cmd.Parameters.AddWithValue("@EmployeeCode", JD[i].EmployeeCode);
                    result = Convert.ToBoolean(cmd.ExecuteNonQuery());
                }
            }
            if (result == true)
            {
                cmd = new SqlCommand("Incentive_UpdateIncentiveStatus", con, transaction);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PaublicationID", data.PaublicationID);
                cmd.Parameters.AddWithValue("@TypeOfEntry", data.TypeOfEntry);
                cmd.Parameters.AddWithValue("@Status", "APP");
                result = Convert.ToBoolean(cmd.ExecuteNonQuery());
            }
           
            transaction.Commit();
            log.Info("The publication with id : " + data.PaublicationID + " and type of entry : " + data.TypeOfEntry + " incentive points process is approved. User :" + HttpContext.Current.Session["UserId"].ToString());
            log.Info("incentive points are approved : User Name :" + HttpContext.Current.Session["UserName"] + "Role :" + HttpContext.Current.Session["RoleName"]); 
            return result;
        }
        catch (Exception ex)
        {
            transaction.Rollback();
            log.Error("Inside ApproveIncentiveStatus function, PublicationID: " + data.PaublicationID + "Type Of Entry: " + data.TypeOfEntry);
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
            log.Error("Inside checkIncentivePointStatus catch block Publication ID " + Pid + "Type of Entry: " + TypeEntry + " Incetive Point Status: " + status);
            log.Error(ex.Message);
            log.Error(ex.StackTrace);
            throw ex;
        }

        finally
        {
            con.Close();
        }
    }

    public int CountThresholdPublicationPoint(PublishData obj1)
    {
        int count = 0;
        try
        {
            con.Open();
            cmd = new SqlCommand("Incentive_SelectTotalNoOfPublcation", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@PublicationID", SqlDbType.VarChar, 12);
            cmd.Parameters["@PublicationID"].Value = obj1.PaublicationID;
            cmd.Parameters.Add("@EntryType", SqlDbType.VarChar, 2);
            cmd.Parameters["@EntryType"].Value = obj1.TypeOfEntry;
            cmd.Parameters.Add("@Employeecode", SqlDbType.VarChar, 12);
            cmd.Parameters["@Employeecode"].Value = obj1.EmployeeCode;
            cmd.Parameters.Add("@publicationyear", SqlDbType.VarChar, 4);
            cmd.Parameters["@publicationyear"].Value = obj1.PublishJAYear;
            SqlDataReader sdr = cmd.ExecuteReader();

            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    if (!Convert.IsDBNull(sdr["countvalue"]))
                    {
                        count = (int)sdr["countvalue"];
                    }

                }

            }
            return count;
        }
        catch (Exception ex)
        {
            log.Error("Inside CountThresholdPublicationPoint catch block Publication ID  " + obj1.PaublicationID + "Type of Entry: " + obj1.TypeOfEntry);
            log.Error(ex.Message);
            log.Error(ex.StackTrace);
            throw ex;
        }

        finally
        {
            con.Close();
        }
    }

    public IncentivePoint SelectSNIPJRPoint(string issn, string year)
    {
        log.Debug("Inside SelectSNIPJRPoint function- of issn " + issn);
        try
        {
            IncentivePoint obj = new IncentivePoint();
            con.Open();
            cmd = new SqlCommand("Incentive_SelectSNIPSJRPoint", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ISSN", SqlDbType.VarChar, 50);
            cmd.Parameters["@ISSN"].Value = issn;
            cmd.Parameters.Add("@Year", SqlDbType.VarChar, 50);
            cmd.Parameters["@Year"].Value = year;
            SqlDataReader sdr = cmd.ExecuteReader();

            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    if (!Convert.IsDBNull(sdr["SNIP"]))
                    {
                        obj.SNIP = (double)sdr["SNIP"];
                    }
                    if (!Convert.IsDBNull(sdr["SJR"]))
                    {
                        obj.SJR = (double)sdr["SJR"];
                    }

                }

            }
            return obj;
        }
        catch (Exception ex)
        {
            log.Error("Inside  SelectSNIPJRPoint function- of issn " + issn);
            log.Error(ex.Message);
            log.Error(ex.StackTrace);
            throw ex;
        }

        finally
        {
            con.Close();
        }
    }

    public DataTable SelectPatentInventorDetail(string ID)
    {
        log.Debug("Inside Incentive_DataObjects- SelectPatentInventorDetail function, Patent ID: " + ID);
        con.Open();
        DataTable ds = null;

        try
        {
            SqlDataAdapter da;

            cmd = new SqlCommand("Incentive_SelectPatentInventorDetails", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@ID", SqlDbType.VarChar, 15);
            cmd.Parameters["@ID"].Value = ID;
            da = new SqlDataAdapter(cmd);

            ds = new DataTable();
            da.Fill(ds);
            return ds;
        }

        catch (Exception ex)
        {
            log.Error("Inside Incentive_DataObjects- SelectAuthorDetails function, Patent ID: " + ID);
            log.Error(ex.Message);
            log.Error(ex.StackTrace);
            throw ex;
        }

        finally
        {
            con.Close();
        }
    }

    public bool InsertIncentivePointToPatentAuthor(PublishData[] JD, PublishData data)
    {
        con.Open();
        transaction = con.BeginTransaction();
        bool result = false;
        try
        {
            for (int i = 0; i < JD.Length; i++)
            {
                cmd = new SqlCommand("Incentive_InsertPatentIncentivePoint", con, transaction);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@PatentID", data.PaublicationID);
                cmd.Parameters.AddWithValue("@EmployeeCode", JD[i].EmployeeCode);
                cmd.Parameters.AddWithValue("@TotalPoints", JD[i].TotalPoint);

                result = Convert.ToBoolean(cmd.ExecuteNonQuery());

            }
            if (result == true)
            {
                cmd = new SqlCommand("Incentive_UpdatePatentIncentiveStatus", con, transaction);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PatentID", data.PaublicationID);
                cmd.Parameters.AddWithValue("@Status", "PRC");
                result = Convert.ToBoolean(cmd.ExecuteNonQuery());
            }
            transaction.Commit();
            return result;
        }
        catch (Exception ex)
        {
            transaction.Rollback();
            log.Error("Inside Incentive_DataObjects- InsertIncentivePointToAuthor function, PublicationID: " + data.PaublicationID + "Type Of Entry: " + data.TypeOfEntry);
            log.Error(ex.Message);
            log.Error(ex.StackTrace);
            throw ex;
        }

        finally
        {
            con.Close();
        }
    }

    public bool ApprovePatenIncentiveStatus(PublishData[] JD, PublishData data)
    {
        con.Open();
        transaction = con.BeginTransaction();
        bool result = false;
        try
        {

            for (int i = 0; i < JD.Length; i++)
            {
                cmd = new SqlCommand("Incentive_SelectMemberCurrentBalance", con, transaction);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MemberId", JD[i].EmployeeCode);
                SqlDataReader sdr1 = cmd.ExecuteReader();
                string memberid = null;
                double currentbalance = 0.0;
                double oldbalance = 0.0;
                if (sdr1.HasRows)
                {
                    while (sdr1.Read())
                    {
                        if (!Convert.IsDBNull(sdr1["Currentbalance"]))
                        {
                            currentbalance = (double)sdr1["Currentbalance"];
                        }
                        if (!Convert.IsDBNull(sdr1["OldBalance"]))
                        {
                            oldbalance = (double)sdr1["OldBalance"];
                        }
                        if (!Convert.IsDBNull(sdr1["MemberId"]))
                        {
                            memberid = (string)sdr1["MemberId"];
                        }
                    }
                }

                sdr1.Close();

                if (memberid == null)
                {
                    cmd = new SqlCommand("Incentive_InsertMemberIncentivePointSummary", con, transaction);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MemberId", JD[i].EmployeeCode);
                    cmd.Parameters.AddWithValue("@Membertype", JD[i].MUNonMU);
                    cmd.Parameters.AddWithValue("@Oldbalance", oldbalance);
                    cmd.Parameters.AddWithValue("@Currentbalance", JD[i].TotalPoint);
                    cmd.Parameters.AddWithValue("@LastTransactionDate", DateTime.Now);
                    cmd.Parameters.AddWithValue("@UserId", HttpContext.Current.Session["UserId"].ToString());
                    cmd.Parameters.AddWithValue("@Date", DateTime.Now);

                    result = Convert.ToBoolean(cmd.ExecuteNonQuery());
                }
                else
                {
                    cmd = new SqlCommand("Incentive_UpdateMemberIncentivePointSummary", con, transaction);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MemberId", JD[i].EmployeeCode);
                    if (currentbalance != 0.0)
                    {
                        cmd.Parameters.AddWithValue("@Currentbalance", currentbalance + JD[i].TotalPoint);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Currentbalance", JD[i].TotalPoint);
                    }
                    cmd.Parameters.AddWithValue("@LastTransactionDate", DateTime.Now);
                    cmd.Parameters.AddWithValue("@UserId", HttpContext.Current.Session["UserId"].ToString());
                    cmd.Parameters.AddWithValue("@Date", DateTime.Now);

                    result = Convert.ToBoolean(cmd.ExecuteNonQuery());
                }
                if (result == true)
                {
                    cmd = new SqlCommand("Incentive_SelectMaxEntryNo", con, transaction);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MemberId", JD[i].EmployeeCode);
                    SqlDataReader sdr = cmd.ExecuteReader();
                    int entryno = 0;
                    if (sdr.HasRows)
                    {
                        while (sdr.Read())
                        {
                            if (!Convert.IsDBNull(sdr["EntryNo"]))
                            {
                                entryno = (int)sdr["EntryNo"];
                            }

                        }
                    }
                    else
                    {
                        entryno = 0;
                    }
                    sdr.Close();

                    cmd = new SqlCommand("Incentive_InsertPatentMemberIncentivePointTransaction", con, transaction);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MemberId", JD[i].EmployeeCode);
                    cmd.Parameters.AddWithValue("@EntryNo", entryno + 1);
                    cmd.Parameters.AddWithValue("@TransactionType", JD[i].TransactionType);
                    cmd.Parameters.AddWithValue("@ReferenceNumber", data.PatentID);
                    cmd.Parameters.AddWithValue("@TotalPoint", JD[i].TotalPoint);
                    cmd.Parameters.AddWithValue("@UserId", HttpContext.Current.Session["UserId"].ToString());
                    cmd.Parameters.AddWithValue("@Date", DateTime.Now);
                    result = Convert.ToBoolean(cmd.ExecuteNonQuery());

                }
            }
            if (result == true)
            {
                cmd = new SqlCommand("Incentive_UpdatePatentIncentiveStatus", con, transaction);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PatentID", data.PatentID);
                cmd.Parameters.AddWithValue("@Status", "APP");
                result = Convert.ToBoolean(cmd.ExecuteNonQuery());
            }
            transaction.Commit();
            return result;
        }
        catch (Exception ex)
        {
            transaction.Rollback();
            log.Error("Inside Incentive_DataObjects- InsertIncentivePointToAuthor function, PublicationID: " + data.PaublicationID + "Type Of Entry: " + data.TypeOfEntry);
            log.Error(ex.Message);
            log.Error(ex.StackTrace);
            throw ex;
        }

        finally
        {
            con.Close();
        }
    }

    public PublishData SelectMemberDetails(string MemberId)
    {

        try
        {
            con.Open();
            cmd = new SqlCommand("Incentive_SelectMemberDetails", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MemberId", MemberId);
            PublishData p = new PublishData();
            SqlDataReader sdr = cmd.ExecuteReader();

            while (sdr.Read())
            {
                if (!Convert.IsDBNull(sdr["CurrentBalance"]))
                {
                    p.CurrentBalance = (double)sdr["CurrentBalance"];
                }

                if (!Convert.IsDBNull(sdr["OldBalance"]))
                {
                    p.OldBalance = (double)sdr["OldBalance"];
                }
            }

            return p;
        }
        catch (Exception ex)
        {
            log.Error("Inside InventoryObject- fnGetVendorId catch block ");
            log.Error(ex.Message);
            log.Error(ex.StackTrace);
            throw ex;
        }

        finally
        {
            con.Close();
        }

    }


    //Ashwini
    public string SelectExistingCurBal(string p)
    {

        try
        {
            IncentiveData i = new IncentiveData();
            string total = null;
            con.Open();
            SqlCommand cmd = new SqlCommand("SelectExistingCurBal", con); //selectExistingUser stored procedure 
            cmd.Parameters.Add("@MemberId", SqlDbType.VarChar, 12);
            cmd.Parameters["@MemberId"].Value = p;
            cmd.CommandType = CommandType.StoredProcedure;

            SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {

                if (!Convert.IsDBNull(sdr["CurrentBalance"]))
                {
                    total = sdr["CurrentBalance"].ToString();
                }

            }
            return total;
        }
        catch (Exception e)
        {
            log.Error("Error: Inside catch block of SelectExistingCurBal : " + p);
            log.Error("Error msg:" + e);
            log.Error("Stack trace:" + e.StackTrace);
            throw e;
        }

        finally
        {
            con.Close();
        }
    }

    public bool UpdateCurBal(IncentivePoint obj)
    {
        log.Debug("Inside UpdateCurrentBal function of ID: " + obj.MemberId);
        bool result = false;
        try
        {
            con.Open();
            transaction = con.BeginTransaction();
            cmd = new SqlCommand("Incentive_IncentivePointAdjustment", con, transaction);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MemberId", obj.MemberId);
            cmd.Parameters.AddWithValue("@CurrentBalance", obj.CurrentBalance);
            result = Convert.ToBoolean(cmd.ExecuteNonQuery());
            if (result == true)
            {
                cmd = new SqlCommand("Incentive_SelectMaxEntryNo", con, transaction);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MemberId", obj.MemberId);
                SqlDataReader sdr = cmd.ExecuteReader();
                int entryno = 0;
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        if (!Convert.IsDBNull(sdr["EntryNo"]))
                        {
                            entryno = (int)sdr["EntryNo"];
                        }

                    }
                }
                else
                {
                    entryno = 0;
                }
                sdr.Close();
                cmd = new SqlCommand("Incentive_InsertMemberIncentivePointTransaction", con, transaction);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MemberId", obj.MemberId);
                cmd.Parameters.AddWithValue("@EntryNo", entryno + 1);
                cmd.Parameters.AddWithValue("@TransactionType", obj.TransactionType);
                if (obj.ReferenceNumber == null)
                {
                    cmd.Parameters.AddWithValue("@ReferenceNumber",DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@ReferenceNumber", obj.ReferenceNumber);
                }
                cmd.Parameters.AddWithValue("@UtilizationDate", DBNull.Value);
                cmd.Parameters.AddWithValue("@TotalPoint", obj.TotalPoint);
                if (obj.BasePoint == 0.0)
                {
                    cmd.Parameters.AddWithValue("@BasePoint", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@BasePoint", obj.BasePoint);
                }
                if (obj.SNIPSJRPoint == 0.0)
                {
                    cmd.Parameters.AddWithValue("@SNIPSJRPoint", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@SNIPSJRPoint", obj.SNIPSJRPoint);
                }
                if (obj.ThresholdPoint == 0.0)
                {
                    cmd.Parameters.AddWithValue("@ThresholdPoint", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@ThresholdPoint", obj.ThresholdPoint);
                }
                cmd.Parameters.AddWithValue("@Remarks", obj.Remarks);
                cmd.Parameters.AddWithValue("@UserId", HttpContext.Current.Session["UserId"].ToString());
                cmd.Parameters.AddWithValue("@Date", DateTime.Now);
                result = Convert.ToBoolean(cmd.ExecuteNonQuery());
            }


            transaction.Commit();
            log.Info("CurrentBalance details updated of ID: " + obj.MemberId);
            log.Info("Current Balance is updated : User Name :" + HttpContext.Current.Session["UserName"] + "Role :" + HttpContext.Current.Session["RoleName"]); 
            return result;
        }
        catch (Exception e)
        {
            transaction.Rollback();
            log.Error("Error: Inside catch block of UpdateCurBal of ID: " + obj.MemberId);
            log.Error("Error msg:" + e);
            log.Error("Stack trace:" + e.StackTrace);
            throw e;
        }

        finally
        {
            con.Close();
        }
    }

    public IncentivePoint SelectMemberCurBalance(string memberid1)
    {
        con.Open();
        try
        {
            IncentivePoint obj = new IncentivePoint();
            cmd = new SqlCommand("Incentive_SelectMemberCurrentBalance", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MemberId", memberid1);
            SqlDataReader sdr1 = cmd.ExecuteReader();
            if (sdr1.HasRows)
            {
                while (sdr1.Read())
                {
                    if (!Convert.IsDBNull(sdr1["Currentbalance"]))
                    {
                        obj.CurrentBalance = (double)sdr1["Currentbalance"];
                    }
                    if (!Convert.IsDBNull(sdr1["OldBalance"]))
                    {
                        obj.OpeningBalance = (double)sdr1["OldBalance"];
                    }
                    if (!Convert.IsDBNull(sdr1["MemberId"]))
                    {
                        obj.MemberId = (string)sdr1["MemberId"];
                    }
                }
            }

            sdr1.Close();
            return obj;
        }
        catch (Exception e)
        {
            log.Error("Error: Inside catch block of SelectMemberCurBalance of ID: " + memberid1);
            log.Error("Error msg:" + e);
            log.Error("Stack trace:" + e.StackTrace);
            throw e;

        }
        finally
        {
            con.Close();
        }
    }

    public bool InsertUtilizationPoint(IncentivePoint data)
    {
        log.Debug("Inside InsertUtilizationPoint function of ID: " + data.MemberId);
        bool result = false;
        try
        {
            con.Open();
            transaction = con.BeginTransaction();
            cmd = new SqlCommand("Incentive_SelectUtilizationSeed", con, transaction);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Description", data.UtilizationType);
            SqlDataReader reader = cmd.ExecuteReader();
            int seed = 0;
            string Description = null;
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    if (!Convert.IsDBNull(reader["seed"]))
                    {
                        seed = (int)reader["seed"];
                    }
                    if (!Convert.IsDBNull(reader["Description"]))
                    {
                        Description = (string)reader["Description"];
                    }
                }
            }
            reader.Close();

            if (data.UtilizationType == "UTO")
            {
                cmd = new SqlCommand("Incentive_UpdateUtilizationPoint", con, transaction);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MemberId", data.MemberId);
                cmd.Parameters.AddWithValue("@CurrentBalance", data.CurrentBalance);
                result = Convert.ToBoolean(cmd.ExecuteNonQuery());
            }
            else
            {

                cmd = new SqlCommand("Incentive_IncentivePointAdjustment", con, transaction);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MemberId", data.MemberId);
                cmd.Parameters.AddWithValue("@CurrentBalance", data.CurrentBalance);
                result = Convert.ToBoolean(cmd.ExecuteNonQuery());
            }
            if (result == true)
            {
                cmd = new SqlCommand("Incentive_SelectMaxEntryNo", con, transaction);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MemberId", data.MemberId);
                SqlDataReader sdr = cmd.ExecuteReader();
                int entryno = 0;
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        if (!Convert.IsDBNull(sdr["EntryNo"]))
                        {
                            entryno = (int)sdr["EntryNo"];
                        }

                    }
                }
                else
                {
                    entryno = 0;
                }
                sdr.Close();
                cmd = new SqlCommand("Incentive_InsertMemberIncentivePointTransaction", con, transaction);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MemberId", data.MemberId);
                cmd.Parameters.AddWithValue("@EntryNo", entryno + 1);
                cmd.Parameters.AddWithValue("@TransactionType", data.TransactionType);
                cmd.Parameters.AddWithValue("@ReferenceNumber", data.UtilizationType +seed);
                cmd.Parameters.AddWithValue("@TotalPoint", data.Utilization);
                cmd.Parameters.AddWithValue("@UtilizationDate", data.UtilizationDate);
                cmd.Parameters.AddWithValue("@BasePoint", DBNull.Value);
                cmd.Parameters.AddWithValue("@SNIPSJRPoint", DBNull.Value);
                cmd.Parameters.AddWithValue("@ThresholdPoint", DBNull.Value);
                cmd.Parameters.AddWithValue("@Remarks", data.Remarks);
                cmd.Parameters.AddWithValue("@UserId", HttpContext.Current.Session["UserId"].ToString());
                cmd.Parameters.AddWithValue("@Date", DateTime.Now);
                result = Convert.ToBoolean(cmd.ExecuteNonQuery());


                cmd = new SqlCommand("Incentive_UpdateUtilizationSeed", con, transaction);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Seed", seed + 1);
                cmd.Parameters.AddWithValue("@TransactionType", data.UtilizationType);
                result = Convert.ToBoolean(cmd.ExecuteNonQuery());
            }


            transaction.Commit();
            log.Info("CurrentBalance details updated of ID: " + data.MemberId);
            log.Info("Points Utilization : User Name :" + HttpContext.Current.Session["UserName"] + "Role :" + HttpContext.Current.Session["RoleName"]); 
            return result;
        }
        catch (Exception e)
        {
            log.Error("Error: Inside catch block of InsertUtilizationPoint of ID: " + data.MemberId);
            log.Error("Error msg:" + e);
            log.Error("Stack trace:" + e.StackTrace);
            transaction.Rollback();
            throw e;

        }
        finally
        {
            con.Close();
        }
    }

    public DataSet SelectMembersInstitutewise(string inst, string memberid)
    {
        con.Open();
        DataSet ds = null;

        try
        {
            SqlDataAdapter da;

            cmd = new SqlCommand("Incentive_SelectMembersIntitutewise", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@InstituteID", SqlDbType.VarChar, 50);
            cmd.Parameters["@InstituteID"].Value = inst;
            cmd.Parameters.Add("@MemberID", SqlDbType.VarChar, 50);
            cmd.Parameters["@MemberID"].Value = memberid;
            da = new SqlDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

        catch (Exception ex)
        {
            log.Error("Inside SelectMembersInstitutewise function: " + memberid);
            log.Error(ex.Message);
            log.Error(ex.StackTrace);
            throw ex;
        }

        finally
        {
            con.Close();
        }
    }

    //public bool DiscardIncentivePointEntry(PublishData obj1)
    //{
    //    bool result = false;
    //    try
    //    {
    //        con.Open();
    //        cmd = new SqlCommand("Incentive_DiscardIncentivePointEntry", con);
    //        cmd.CommandType = CommandType.StoredProcedure;
    //        cmd.Parameters.AddWithValue("@PublicationID", obj1.PaublicationID);
    //        cmd.Parameters.AddWithValue("@TypeOfEntry", obj1.TypeOfEntry);
    //        result = Convert.ToBoolean(cmd.ExecuteNonQuery());
    //        return result;
    //    }
    //    catch (Exception ex)
    //    {
    //        log.Error("Inside DiscardIncentivePointEntry function,  ID: " + obj1.PaublicationID + " Type of Entry: " + obj1.TypeOfEntry);
    //        log.Error(ex.Message);
    //        log.Error(ex.StackTrace);
    //        throw ex;
    //    }

    //    finally
    //    {
    //        con.Close();
    //    }
    //}

    public bool CheckPublcationId(string p, string memberid)
    {
        bool result = false;
        SqlDataReader sdr = null;
        con.Open();
        try
        {
            cmd = new SqlCommand("Incentive_CheckPublicationId", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ReferenceId", p);
            cmd.Parameters.AddWithValue("@MemberId", memberid);
            sdr = cmd.ExecuteReader();
            string ReferenceNumber = null;
            while (sdr.Read())
            {
                if (!Convert.IsDBNull(sdr["ReferenceNumber"]))
                {
                    ReferenceNumber = (string)sdr["ReferenceNumber"];
                }
            }
            if (ReferenceNumber == null)
            {
                result = false;
            }
            else
            {
                result = true;
            }

            return result;
        }

        catch (Exception ex)
        {
            log.Debug("Inside Incentive_DataObjects- CheckPublcationId function, ID: " + memberid);
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



    public string SelectAuthorEmailId(string employeeid)
    {
        string EmailId = null;
        SqlDataReader sdr = null;
        con.Open();
        try
        {
            cmd = new SqlCommand("Incentive_SelectAuthorEmailId", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Employeecode", employeeid);
            sdr = cmd.ExecuteReader();

            while (sdr.Read())
            {
                if (!Convert.IsDBNull(sdr["EmailId"]))
                {
                    EmailId = (string)sdr["EmailId"];
                }
            }


            return EmailId;
        }

        catch (Exception ex)
        {
            log.Error("Inside SelectAuthorEmailId function,  ID: " + employeeid);
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

    public string SelectHREmailId(string employeeid, string referenceid, string transctiontype)
    {
        string EmailId = null;
        SqlDataReader sdr = null;
        con.Open();
        try
        {
            cmd = new SqlCommand("Incentive_SelectHREmailId", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@TransactionType", transctiontype);
            cmd.Parameters.AddWithValue("@ReferenceNumber", referenceid);
            cmd.Parameters.AddWithValue("@MemberId", employeeid);
            sdr = cmd.ExecuteReader();

            while (sdr.Read())
            {
                if (!Convert.IsDBNull(sdr["EmailId"]))
                {
                    EmailId = (string)sdr["EmailId"];
                }
            }


            return EmailId;
        }

        catch (Exception ex)
        {
            log.Debug("Inside SelectHREmailId function,  ID: " + employeeid);
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

    //public string SelectStudentAuthorEmailId(string employeeid)
    //{
    //    string EmailId = null;
    //    SqlDataReader sdr = null;
    //    con.Open();
    //    try
    //    {
    //        cmd = new SqlCommand("Incentive_SelectStudentEmailId", con);
    //        cmd.CommandType = CommandType.StoredProcedure;
    //        cmd.Parameters.AddWithValue("@MemberId", employeeid);
    //        sdr = cmd.ExecuteReader();

    //        while (sdr.Read())
    //        {
    //            if (!Convert.IsDBNull(sdr["EmailId"]))
    //            {
    //                EmailId = (string)sdr["EmailId"];
    //            }
    //        }


    //        return EmailId;
    //    }

    //    catch (Exception ex)
    //    {
    //        log.Debug("Inside SelectStudentAuthorEmailId function,  ID: " + employeeid);
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

    internal bool UpdateUtilizationMailFlag(string employeeid, string referenceid, string transactiontype)
    {
        con.Open();
        bool result = false;
        try
        {
            cmd = new SqlCommand("Incentive_UpdateUtilizationMailFlag", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@employeeid", employeeid);
            cmd.Parameters.AddWithValue("@referenceid", referenceid);
            cmd.Parameters.AddWithValue("@transactiontype", transactiontype);
            result = Convert.ToBoolean(cmd.ExecuteNonQuery());
            return result;
        }

        catch (Exception ex)
        {
            log.Error("Inside UpdateUtilizationMailFlag function,  ID: " + employeeid);
            log.Error(ex.Message);
            log.Error(ex.StackTrace);
            throw ex;
        }

        finally
        {

            con.Close();
        }
    }

    public DataTable SelectMUAuthorDetails(string Pid, string TypeEntry)
    {
        log.Debug("Inside SelectMUAuthorDetails function, PublicationID: " + Pid + "Type Of Entry: " + TypeEntry);
        con.Open();
        DataTable ds = null;
        try
        {
            SqlDataAdapter da;
            cmd = new SqlCommand("Incentive_SelectMUAuthorDetails", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@PaublicationID", SqlDbType.VarChar, 15);
            cmd.Parameters["@PaublicationID"].Value = Pid;
            cmd.Parameters.Add("@TypeOfEntry", SqlDbType.VarChar, 5);
            cmd.Parameters["@TypeOfEntry"].Value = TypeEntry;
            da = new SqlDataAdapter(cmd);
            ds = new DataTable();
            da.Fill(ds);
            return ds;
        }

        catch (Exception ex)
        {
            log.Error("Inside catch block function SelectMUAuthorDetails function, PublicationID: " + Pid + "Type Of Entry: " + TypeEntry);
            log.Error(ex.Message);
            log.Error(ex.StackTrace);
            throw ex;
        }

        finally
        {
            con.Close();
        }
    }


    public PublishData SelectPublicationData(string memberid, string p)
    {
        PublishData obj = new PublishData();
        SqlDataReader sdr = null;
        con.Open();
        try
        {
            cmd = new SqlCommand("Incentive_SelectPublicationDetails", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MemberId", memberid);
            cmd.Parameters.AddWithValue("@ID", p);
            sdr = cmd.ExecuteReader();

            while (sdr.Read())
            {
                if (!Convert.IsDBNull(sdr["AuthorType"]))
                {
                    obj.AuthorType = (string)sdr["AuthorType"];
                }

                if (!Convert.IsDBNull(sdr["isCorrAuth"]))
                {
                    obj.CorrespondingAuthor = (string)sdr["isCorrAuth"];
                }
                if (!Convert.IsDBNull(sdr["MUNonMU"]))
                {
                    obj.MUNonMU = (string)sdr["MUNonMU"];
                }
                if (!Convert.IsDBNull(sdr["PublishJAYear"]))
                {
                    obj.PublishJAYear = (string)sdr["PublishJAYear"];
                }
                if (!Convert.IsDBNull(sdr["PublicationID"]))
                {
                    obj.PaublicationID = (string)sdr["PublicationID"];
                }
                if (!Convert.IsDBNull(sdr["TypeOfEntry"]))
                {
                    obj.TypeOfEntry = (string)sdr["TypeOfEntry"];
                }
                if (!Convert.IsDBNull(sdr["EmployeeCode"]))
                {
                    obj.EmployeeCode = (string)sdr["EmployeeCode"];
                }
            }


            return obj;
        }

        catch (Exception ex)
        {
            log.Debug("Inside SelectPublicationData function of member id:" + memberid +"Publication ID : "+ p);
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

    public string SelectStudentEmailId(string empcode, string id)
    {
        string EmailId = null;
        SqlDataReader sdr = null;
        con.Open();
        try
        {
            cmd = new SqlCommand("Incentive_SelectStudentEmailId", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MemberId", empcode);
            cmd.Parameters.AddWithValue("@ReferenceId", id);
            sdr = cmd.ExecuteReader();

            while (sdr.Read())
            {
                if (!Convert.IsDBNull(sdr["EmailId"]))
                {
                    EmailId = (string)sdr["EmailId"];
                }
            }


            return EmailId;
        }

        catch (Exception ex)
        {
            log.Error("Inside SelectStudentEmailId function,  ID: " + empcode);
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

    public DataTable CountRemainingPoints(string memberid)
    {
        DataTable dt = new DataTable();
        con.Open();
        cmd = new SqlCommand("Incentive_ViewPointsTransaction", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Memberid", memberid);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //DataSet ds = new DataSet();
        da.Fill(dt);
        return dt;
    }

    public DataTable CountUtilizationPoints(string memberid)
    {
        DataTable dt = new DataTable();
        con.Open();
        cmd = new SqlCommand("Incentive_ViewMemberwiseUtilizationPoint", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Memberid", memberid);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //DataSet ds = new DataSet();
        da.Fill(dt);
        return dt;
    }

    public DataSet SelectMembersInstitutewise(string inst, string memberid, string membername)
    {
        con.Open();
        DataSet ds = null;

        try
        {
            SqlDataAdapter da;

            cmd = new SqlCommand("Incentive_SelectMembersIntitutewise", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@InstituteID", SqlDbType.VarChar, 500);
            cmd.Parameters["@InstituteID"].Value = inst;
            cmd.Parameters.Add("@MemberID", SqlDbType.VarChar, 50);
            cmd.Parameters["@MemberID"].Value = memberid;
            cmd.Parameters.Add("@MemberName", SqlDbType.VarChar, 50);
            cmd.Parameters["@MemberName"].Value = membername;
            da = new SqlDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

        catch (Exception ex)
        {
            log.Error("Inside SelectMembersInstitutewise function: " + memberid);
            log.Error(ex.Message);
            log.Error(ex.StackTrace);
            throw ex;
        }

        finally
        {
            con.Close();
        }
    }

    public string SelectHRMailID(string empcode, string p,string p2)
    {
        string EmailId = null;
        SqlDataReader sdr = null;
        con.Open();
        try
        {
            cmd = new SqlCommand("Incentive_SelectHRMailId", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MemberId", empcode);
            cmd.Parameters.AddWithValue("@MemberType", p);
            cmd.Parameters.AddWithValue("@ReferenceId", p2);
            sdr = cmd.ExecuteReader();

            while (sdr.Read())
            {
                if (!Convert.IsDBNull(sdr["EmailId"]))
                {
                    EmailId = (string)sdr["EmailId"];
                }
            }


            return EmailId;
        }

        catch (Exception ex)
        {
            log.Error("Inside SelectStudentEmailId function,  ID: " + empcode);
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

    public IncentivePoint SelectMemberCurBalanceInstitutewise(string memberid, string inst,string role)
    {
        string EmailId = null;
        SqlDataReader sdr = null;
        IncentivePoint obj = new IncentivePoint(); 
        con.Open();
        try
        {
            cmd = new SqlCommand("Incentive_SelectMemberCurBalanceInstitutewise", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MemberId", memberid);
            cmd.Parameters.AddWithValue("@InstituteId", inst);
            cmd.Parameters.AddWithValue("@Role", role);
            sdr = cmd.ExecuteReader();

            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    if (!Convert.IsDBNull(sdr["Currentbalance"]))
                    {
                        obj.CurrentBalance = (double)sdr["Currentbalance"];
                    }
                    if (!Convert.IsDBNull(sdr["OldBalance"]))
                    {
                        obj.OpeningBalance = (double)sdr["OldBalance"];
                    }
                    if (!Convert.IsDBNull(sdr["MemberId"]))
                    {
                        obj.MemberId = (string)sdr["MemberId"];
                    }
                }
            }

            sdr.Close();
            return obj;
        }

        catch (Exception ex)
        {
            log.Error("Inside SelectMemberCurBalanceInstitutewise function,  ID: " + memberid);
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

    public bool AdditionalPointAward(string memberid, IncentivePoint j)
    {
        log.Debug("Inside AdditionalPointAward function of Member ID: " + memberid);

        con.Open();
        transaction = con.BeginTransaction();
        bool result = false;
        try
        {

            cmd = new SqlCommand("Incentive_AdditionalPointAward", con, transaction);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MemberId", j.MemberId);
            cmd.Parameters.AddWithValue("@Year", j.Year);
            cmd.Parameters.AddWithValue("@isAwarded", "Y");
            //cmd.Parameters.AddWithValue("@IncentivePointStatus", "AWD");
            cmd.Parameters.AddWithValue("@Points", j.Points);

            result = Convert.ToBoolean(cmd.ExecuteNonQuery());

            if (result == true)
            {
                cmd = new SqlCommand("Incentive_SelectMaxEntryNo", con, transaction);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MemberId", j.MemberId);
                SqlDataReader sdr = cmd.ExecuteReader();
                int entryno = 0;
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        if (!Convert.IsDBNull(sdr["EntryNo"]))
                        {
                            entryno = (int)sdr["EntryNo"];
                        }

                    }
                }
                else
                {
                    entryno = 0;
                }
                sdr.Close();

                cmd = new SqlCommand("Incentive_InsertMemberIncentivePointTransaction", con, transaction);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MemberId", j.MemberId);
                cmd.Parameters.AddWithValue("@EntryNo", entryno + 1);
                cmd.Parameters.AddWithValue("@TransactionType", "AWD");
                cmd.Parameters.AddWithValue("@ReferenceNumber", DBNull.Value);
                cmd.Parameters.AddWithValue("@TotalPoint", j.TotalPoint);
                cmd.Parameters.AddWithValue("@UtilizationDate", DBNull.Value);
                cmd.Parameters.AddWithValue("@BasePoint", DBNull.Value);
                cmd.Parameters.AddWithValue("@SNIPSJRPoint", DBNull.Value);
                cmd.Parameters.AddWithValue("@ThresholdPoint", j.TotalPoint);
                if (j.Remarks != "")
                {
                    cmd.Parameters.AddWithValue("@Remarks", j.Remarks);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Remarks", DBNull.Value);
                }
                cmd.Parameters.AddWithValue("@UserId", HttpContext.Current.Session["UserId"].ToString());
                cmd.Parameters.AddWithValue("@Date", DateTime.Now);
                result = Convert.ToBoolean(cmd.ExecuteNonQuery());
            }

            if (result == true)
            {
                cmd = new SqlCommand("Incentive_UpdateMemberIncentivePointSummary", con, transaction);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MemberId", j.MemberId);
                cmd.Parameters.AddWithValue("@Currentbalance", j.CurrentBalance);
                cmd.Parameters.AddWithValue("@LastTransactionDate", DateTime.Now);
                cmd.Parameters.AddWithValue("@UserId", HttpContext.Current.Session["UserId"].ToString());
                cmd.Parameters.AddWithValue("@Date", DateTime.Now);
                result = Convert.ToBoolean(cmd.ExecuteNonQuery());
            }
            transaction.Commit();
            log.Info("The MemberId with id " + j.MemberId + " and  Transaction Type : " + j.TransactionType + "is inserted");
            log.Info("Additional Points Award : User Name :" + HttpContext.Current.Session["UserName"] + "Role :" + HttpContext.Current.Session["RoleName"]); 
            return result;
        }
        catch (Exception ex)
        {
            transaction.Rollback();
            log.Error("Inside Incentive_DataObjects- Incentive_AdditionalPointAward function, MemberId: " + j.MemberId + "Transaction Type: " + j.TransactionType);
            log.Error(ex.Message);
            log.Error(ex.StackTrace);
            throw ex;
        }

        finally
        {
            con.Close();
        }
    }

    public bool CheckMemberId(string memberid)
    {
        bool result1 = false;
        SqlDataReader sdr = null;

        try
        {

            cmdString = "select MemberId from Member_Yearwise_Publication_Summary where MemberId=@MemberId ";
            con.Open();
            cmd = new SqlCommand(cmdString, con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@MemberId", memberid);
            sdr = cmd.ExecuteReader();
            IncentivePoint j = new IncentivePoint();

            while (sdr.Read())
            {
                if (!Convert.IsDBNull(sdr["MemberId"]))
                {
                    j.MemberId = (string)sdr["MemberId"];
                }
            }
            if (j.MemberId == null)
            {
                result1 = false;
            }
            else
            {
                result1 = true;
            }

            return result1;
        }

        catch (Exception ex)
        {
            log.Debug("Inside Incentive_DataObjects- CheckMemberId function, ID: " + memberid);
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

    public string SelectMemberCurrentBal(string memberid)
    {
        con.Open();
        try
        {
            IncentivePoint obj = new IncentivePoint();
            cmd = new SqlCommand("Incentive_SelectMemberCurrentBalance", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MemberId", memberid);

            SqlDataReader sdr1 = cmd.ExecuteReader();
            if (sdr1.HasRows)
            {
                while (sdr1.Read())
                {
                    if (!Convert.IsDBNull(sdr1["Currentbalance"]))
                    {
                        obj.CurrentBalance = (double)sdr1["Currentbalance"];
                    }

                }
            }

            sdr1.Close();
            return obj.CurrentBalance.ToString();
        }
        catch (Exception e)
        {
            log.Error("Error: Inside catch block of SelectMemberCurrentBal of ID: " + memberid);
            log.Error("Error msg:" + e);
            log.Error("Stack trace:" + e.StackTrace);
            throw e;

        }
        finally
        {
            con.Close();
        }
    }

    public IncentivePoint SelectPublicationCount(string memberid, string year)
    {
        SqlDataReader sdr = null;
        IncentivePoint j = new IncentivePoint();
        con.Open();
        try
        {
            cmd = new SqlCommand("Incentive_SelectPublicationCount", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MemberId", memberid);
            cmd.Parameters.AddWithValue("@Year", year);
            sdr = cmd.ExecuteReader();

            if (sdr.HasRows)
            {
                while (sdr.Read())
                {

                    if (!Convert.IsDBNull(sdr["TotalNoOfPublications"]))
                    {
                        j.TotalNoOfPublications = (int)sdr["TotalNoOfPublications"];
                    }
                    if (!Convert.IsDBNull(sdr["IsAwarded"]))
                    {
                        j.isAwarded = (string)sdr["IsAwarded"];
                    }
                    //if (!Convert.IsDBNull(sdr["IncentivePointStatus"]))
                    //{
                    //    j.IncentivePointStatus = (string)sdr["IncentivePointStatus"];
                    //}
                    if (!Convert.IsDBNull(sdr["Points"]))
                    {
                        j.Points = (double)sdr["Points"];
                    }

                }
            }

            sdr.Close();
            return j;
        }

        catch (Exception ex)
        {
            log.Error("Inside SelectPublicationCount function of member id:" + memberid + "Year : " + year);
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

    public string SelectMemberType(string memberid)
    {
        string membertype = null;
        SqlDataReader sdr = null;
        try
        {

            cmdString = "select MemberType from Member_Incentive_Point_Summary where MemberId=@MemberId ";
            con.Open();
            cmd = new SqlCommand(cmdString, con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@MemberId", memberid);
            sdr = cmd.ExecuteReader();
            IncentivePoint j = new IncentivePoint();

            while (sdr.Read())
            {
                if (!Convert.IsDBNull(sdr["MemberType"]))
                {
                  membertype= (string)sdr["MemberType"];
                }
            }

            return membertype;
        }

        catch (Exception ex)
        {
            log.Debug("Inside Incentive_DataObjects- SelectMemberType function, ID: " + memberid);
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

    public System.Collections.ArrayList SelectImpactFactor(PublishData v)
    {
        log.Debug("Inside the SelectImpactFactor function Publication Category : "+v.MUCategorization + "Year : "+v.PublishJAYear);
        ArrayList list = new ArrayList();
        list.Add("0");
        SqlDataReader sdr = null;
        SqlDataReader sdr1 = null;
         string schemeid = null;
      
        try
        {
            con.Open();

            cmd = new SqlCommand("SelectSchemeId", con);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PublishJAYear", v.PublishJAYear);
            cmd.Parameters.AddWithValue("@PublishJAMonth", v.PublishJAMonth);
                sdr1 = cmd.ExecuteReader();


                while (sdr1.Read())
                {
                    if (!Convert.IsDBNull(sdr1["SchemeId"]))
                    {
                        schemeid = (string)sdr1["SchemeId"];
                    }
                }

                sdr1.Close();
           

            cmd = new SqlCommand("Incentive_SelectImpactFactor", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@IsStudent", v.IsStudentAuthor);
            cmd.Parameters.AddWithValue("@SchemeId", schemeid);
            cmd.Parameters.AddWithValue("@Category ", v.MUCategorization);
            sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                string impactfactor = (string)sdr["ImpactFactor"];
                list.Add(impactfactor);
            }
            return list;
        }
        catch (Exception e)
        {
            log.Error("Inside catch block of SelectImpactFactor function");
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

    public DataSet SelectIncentivePoints(PublishData v)
    {
        log.Debug("Inside the SelectIncentivePoints function of Impact Factor : " + v.ImpactFactor);
        SqlDataReader sdr1 = null;
        string schemeid = null;
        try
        {

            con.Open();
          
            cmd = new SqlCommand("SelectSchemeId", con);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PublishJAYear", v.PublishJAYear);
            cmd.Parameters.AddWithValue("@PublishJAMonth", v.PublishJAMonth);
            sdr1 = cmd.ExecuteReader();

            while (sdr1.Read())
            {
                if (!Convert.IsDBNull(sdr1["SchemeId"]))
                {
                    schemeid = (string)sdr1["SchemeId"];
                }
            }
            sdr1.Close();   
            cmd = new SqlCommand("Incentive_SelectIncentivePoints", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@IsStudent", v.IsStudentAuthor);
            cmd.Parameters.AddWithValue("@SchemeId", schemeid);
            cmd.Parameters.AddWithValue("@ImpactFactor", v.ImpactFactor);
            cmd.Parameters.AddWithValue("@Category ", v.MUCategorization);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

        catch (Exception ex)
        {
            log.Error(ex.Message);
            log.Error(ex.StackTrace);
            throw ex;
        }

        finally
        {
            con.Close();
        }
    }

    public ArrayList SelectHODMailid(string empcode, string p1, string p2)
    {
        ArrayList list = new ArrayList();
        EmailDetails details = new EmailDetails();
        SqlDataReader sdr = null;
        con.Open();
        try
        {
            cmd = new SqlCommand("Incentive_SelectHODMailid", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MemberId", empcode);
            cmd.Parameters.AddWithValue("@MemberType", p1);
            cmd.Parameters.AddWithValue("@ReferenceId", p2);
            sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {

                if (!Convert.IsDBNull(sdr["HODEmailId"]))
                {
                    details.HODmailid = (string)sdr["HODEmailId"];
                }
                else
                {
                    details.HODmailid = "";

                }
                if (!Convert.IsDBNull(sdr["StudentEmailId"]))
                {
                    details.Studentmailid = (string)sdr["StudentEmailId"];
                }
                else
                {
                    details.Studentmailid = "";

                }

                list.Add(details.HODmailid);
                list.Add(details.Studentmailid);
            }


            return list;
        }

        catch (Exception ex)
        {
            log.Error("Inside SelectHODMailid function,  ID: " + empcode);
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

    public string CheckInstitution(string p)
    {
        SqlDataReader sdr = null;
        try
        {
           
            con.Open();
            string Institution = "";
            cmd = new SqlCommand("Incentive_SelectInstitute", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MemberId", p);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {

                if (!Convert.IsDBNull(sdr["Institution"]))
                {
                    Institution = (string)sdr["Institution"];
                }
                else
                {
                    Institution = "";
                }
               
            }
            return Institution;
            
        }

        catch (Exception ex)
        {
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


    public DataSet SelectFacultyInstitutewise(string inst, string memberid, string membername)
    {
        con.Open();
        DataSet ds = null;

        try
        {
            SqlDataAdapter da;

            cmd = new SqlCommand("Incentive_SelectFacultyIntitutewise", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@InstituteID", SqlDbType.VarChar, 50);
            cmd.Parameters["@InstituteID"].Value = inst;
            cmd.Parameters.Add("@MemberID", SqlDbType.VarChar, 50);
            cmd.Parameters["@MemberID"].Value = memberid;
            cmd.Parameters.Add("@MemberName", SqlDbType.VarChar, 50);
            cmd.Parameters["@MemberName"].Value = membername;
            da = new SqlDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

        catch (Exception ex)
        {
            log.Error("Inside SelectMembersInstitutewise function: " + memberid);
            log.Error(ex.Message);
            log.Error(ex.StackTrace);
            throw ex;
        }

        finally
        {
            con.Close();
        }
    }

    public DataSet SelectStudentInstitutewise(string inst, string memberid, string membername)
    {
        con.Open();
        DataSet ds = null;

        try
        {
            SqlDataAdapter da;

            cmd = new SqlCommand("Incentive_SelectStudentInstitutewise", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@InstituteID", SqlDbType.VarChar, 50);
            cmd.Parameters["@InstituteID"].Value = inst;
            cmd.Parameters.Add("@MemberID", SqlDbType.VarChar, 50);
            cmd.Parameters["@MemberID"].Value = memberid;
            cmd.Parameters.Add("@MemberName", SqlDbType.VarChar, 50);
            cmd.Parameters["@MemberName"].Value = membername;
            da = new SqlDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

        catch (Exception ex)
        {
            log.Error("Inside SelectMembersInstitutewise function: " + memberid);
            log.Error(ex.Message);
            log.Error(ex.StackTrace);
            throw ex;
        }

        finally
        {
            con.Close();
        }
    }

    public ArrayList SelectCountOfRole(string user)
    {
        ArrayList rolelist = new ArrayList();
        SqlDataReader sdr = null;
        try
        {

            cmdString = "select Role_Id from User_Role_Map where User_Id=@User_Id ";
            con.Open();
            cmd = new SqlCommand(cmdString, con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@User_Id", user);
            sdr = cmd.ExecuteReader();
            //IncentivePoint j = new IncentivePoint();
            User u = new User();
            while (sdr.Read())
            {
                if (!Convert.IsDBNull(sdr["Role_Id"]))
                {
                    u.Role_Id = (int)sdr["Role_Id"];
                    rolelist.Add(u.Role_Id);
                }
            }

            return rolelist;
        }

        catch (Exception ex)
        {
            log.Debug("Inside Incentive_DataObjects- SelectCountOfRole function, User_Id: " + user);
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

    public DataSet SelectStudentInstitute(string inst)
    {
        log.Debug("Inside the SelectStudentInstitute function: " + inst);
        con.Open();
        DataSet ds = null;

        try
        {
            SqlDataAdapter da;

            cmd = new SqlCommand("Incentive_SelectStudentInstitute", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@InstituteID", SqlDbType.VarChar, 50);
            cmd.Parameters["@InstituteID"].Value = inst;
          
            da = new SqlDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

        catch (Exception ex)
        {
            log.Error("Inside SelectMembersInstitutewise function: " + inst);
            log.Error(ex.Message);
            log.Error(ex.StackTrace);
            throw ex;
        }

        finally
        {
            con.Close();
        }
    }

    public DataSet SelectFacultyInstitute(string inst)
    {
        log.Debug("Inside the SelectFacultyInstitute function: " + inst);
        con.Open();
        DataSet ds = null;

        try
        {
            SqlDataAdapter da;

            cmd = new SqlCommand("Incentive_SelectFacultyInstitute", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@InstituteID", SqlDbType.VarChar, 50);
            cmd.Parameters["@InstituteID"].Value = inst;

            da = new SqlDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

        catch (Exception ex)
        {
            log.Error("Inside the SelectFacultyInstitute function: " + inst);
            log.Error(ex.Message);
            log.Error(ex.StackTrace);
            throw ex;
        }

        finally
        {
            con.Close();
        }
    }

    public DataSet SelectMembersInstitute(string inst)
    {
        log.Debug("Inside the SelectMembersInstitute function: " + inst);
        con.Open();
        DataSet ds = null;

        try
        {
            SqlDataAdapter da;

            cmd = new SqlCommand("Incentive_SelectMembersInstitute", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@InstituteID", SqlDbType.VarChar, 500);
            cmd.Parameters["@InstituteID"].Value = inst;

            da = new SqlDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

        catch (Exception ex)
        {
            log.Error("Inside the SelectMembersInstitute function: " + inst);
            log.Error(ex.Message);
            log.Error(ex.StackTrace);
            throw ex;
        }

        finally
        {
            con.Close();
        }
    }

   
    public string SelectInstwiseHRMailid(string empcode, string p1, string p2)
    {
        string EmailId = null;
        SqlDataReader sdr = null;
        con.Open();
        try
        {
            cmd = new SqlCommand("Incentive_SelectInstwiseHRMailid", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MemberId", empcode);
            cmd.Parameters.AddWithValue("@MemberType", p1);
            cmd.Parameters.AddWithValue("@ReferenceId", p2);
            sdr = cmd.ExecuteReader();

            while (sdr.Read())
            {
                if (!Convert.IsDBNull(sdr["EmailId"]))
                {
                    EmailId = (string)sdr["EmailId"];
                }
            }


            return EmailId;
        }

        catch (Exception ex)
        {
            log.Error("Inside SelectStudentEmailId function,  ID: " + empcode);
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

    public string SelectYearWisePoints(string memberid, string year)
    {
        con.Open();
        try
        {
            IncentivePoint obj = new IncentivePoint();
            cmd = new SqlCommand("Incentive_SelectMemberCurrentBalanceYearWise", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MemberId", memberid);
            cmd.Parameters.AddWithValue("@Year", year);
            SqlDataReader sdr1 = cmd.ExecuteReader();
            if (sdr1.HasRows)
            {
                while (sdr1.Read())
                {
                    if (!Convert.IsDBNull(sdr1["point"]))
                    {
                        obj.CurrentBalance = (double)sdr1["point"];
                    }

                }
            }

            sdr1.Close();
            return obj.CurrentBalance.ToString();
        }
        catch (Exception e)
        {
            log.Error("Error: Inside catch block of SelectYearWisePoints of ID: " + memberid);
            log.Error("Error msg:" + e);
            log.Error("Stack trace:" + e.StackTrace);
            throw e;

        }
        finally
        {
            con.Close();
        }
    }

    public DataSet SelectApprovedIncentivePublications(PublishData data)
    {

        try
        {

            con.Open();
            cmd = new SqlCommand("Incentive_SelectApprovedPublications", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID", data.PaublicationID);
            cmd.Parameters.AddWithValue("@Title", data.JournalTitle);
            cmd.Parameters.AddWithValue("@Type", data.TypeOfEntry);
            cmd.Parameters.AddWithValue("@BulkYear", data.bulkpublicationyear);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

        catch (Exception ex)
        {
            log.Error(ex.Message);
            log.Error(ex.StackTrace);
            throw ex;
        }

        finally
        {
            con.Close();
        }
    }

    public bool InsertSJRPointToAuthor(PublishData[] JD, PublishData data)
    {
        log.Debug("Inside InsertSJRPointToAuthor function of Publication ID: " + data.PaublicationID + " Type of Entry : " + data.TypeOfEntry);
        con.Open();
        transaction = con.BeginTransaction();
        bool result = false;
        try
        {
            for (int i = 0; i < JD.Length; i++)
            {
                cmd = new SqlCommand("Incentive_InsertSJRPoint", con, transaction);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PaublicationID", data.PaublicationID);
                cmd.Parameters.AddWithValue("@TypeOfEntry", data.TypeOfEntry);
                cmd.Parameters.AddWithValue("@EmployeeCode", JD[i].EmployeeCode);
                cmd.Parameters.AddWithValue("@TotalPoints", JD[i].TotalPoint);
                cmd.Parameters.AddWithValue("@SNIPSJRPoint", JD[i].SNIPSJRPoint);
                result = Convert.ToBoolean(cmd.ExecuteNonQuery());
            }
            for (int i = 0; i < JD.Length; i++)
            {
                cmd = new SqlCommand("Incentive_SelectMemberCurrentBalance", con, transaction);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MemberId", JD[i].EmployeeCode);
                SqlDataReader sdr1 = cmd.ExecuteReader();
                IncentivePoint points = new IncentivePoint();
                if (sdr1.HasRows)
                {
                    while (sdr1.Read())
                    {
                        if (!Convert.IsDBNull(sdr1["Currentbalance"]))
                        {
                            points.CurrentBalance = (double)sdr1["Currentbalance"];
                        }
                        if (!Convert.IsDBNull(sdr1["OldBalance"]))
                        {
                            points.OpeningBalance = (double)sdr1["OldBalance"];
                        }
                        if (!Convert.IsDBNull(sdr1["MemberId"]))
                        {
                            points.MemberId = (string)sdr1["MemberId"];
                        }
                    }
                }

                sdr1.Close();

                if (points.MemberId == null)
                {
                    cmd = new SqlCommand("Incentive_InsertMemberIncentivePointSummary", con, transaction);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MemberId", JD[i].EmployeeCode);
                    cmd.Parameters.AddWithValue("@Membertype", JD[i].MUNonMU);
                    cmd.Parameters.AddWithValue("@Oldbalance", points.OpeningBalance);
                    cmd.Parameters.AddWithValue("@Currentbalance", points.CurrentBalance + JD[i].TotalPoint);
                    cmd.Parameters.AddWithValue("@LastTransactionDate", DateTime.Now);
                    cmd.Parameters.AddWithValue("@UserId", HttpContext.Current.Session["UserId"].ToString());
                    cmd.Parameters.AddWithValue("@Date", DateTime.Now);

                    result = Convert.ToBoolean(cmd.ExecuteNonQuery());
                }
                else
                {
                    cmd = new SqlCommand("Incentive_UpdateMemberIncentivePointSummary", con, transaction);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MemberId", JD[i].EmployeeCode);
                    if (points.CurrentBalance != 0.0)
                    {
                        cmd.Parameters.AddWithValue("@Currentbalance", points.CurrentBalance + JD[i].TotalPoint);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Currentbalance", JD[i].TotalPoint);
                    }
                    cmd.Parameters.AddWithValue("@LastTransactionDate", DateTime.Now);
                    cmd.Parameters.AddWithValue("@UserId", HttpContext.Current.Session["UserId"].ToString());
                    cmd.Parameters.AddWithValue("@Date", DateTime.Now);

                    result = Convert.ToBoolean(cmd.ExecuteNonQuery());
                }
                if (result == true)
                {
                    cmd = new SqlCommand("Incentive_SelectMaxEntryNo", con, transaction);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MemberId", JD[i].EmployeeCode);
                    SqlDataReader sdr = cmd.ExecuteReader();
                    int entryno = 0;
                    if (sdr.HasRows)
                    {
                        while (sdr.Read())
                        {
                            if (!Convert.IsDBNull(sdr["EntryNo"]))
                            {
                                entryno = (int)sdr["EntryNo"];
                            }

                        }
                    }
                    else
                    {
                        entryno = 0;
                    }
                    sdr.Close();

                    cmd = new SqlCommand("Incentive_InsertMemberIncentivePointTransaction", con, transaction);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MemberId", JD[i].EmployeeCode);
                    cmd.Parameters.AddWithValue("@EntryNo", entryno + 1);
                    cmd.Parameters.AddWithValue("@TransactionType", JD[i].TransactionType);
                    cmd.Parameters.AddWithValue("@ReferenceNumber", data.TypeOfEntry + data.PaublicationID);
                    cmd.Parameters.AddWithValue("@TotalPoint", JD[i].TotalPoint);
                    cmd.Parameters.AddWithValue("@UtilizationDate", DBNull.Value);
                    cmd.Parameters.AddWithValue("@BasePoint", 0);
                    cmd.Parameters.AddWithValue("@SNIPSJRPoint", JD[i].SNIPSJRPoint);
                    cmd.Parameters.AddWithValue("@ThresholdPoint", 0);
                    cmd.Parameters.AddWithValue("@Remarks", DBNull.Value);
                    cmd.Parameters.AddWithValue("@UserId", HttpContext.Current.Session["UserId"].ToString());
                    cmd.Parameters.AddWithValue("@Date", DateTime.Now);
                    result = Convert.ToBoolean(cmd.ExecuteNonQuery());

                }
            }
          
            transaction.Commit();
            log.Info("The Publication with id " + data.PaublicationID + " and  Type of Entry : " + data.TypeOfEntry + "is processed for incentive point entry");
            return result;
        }
        catch (Exception ex)
        {
            transaction.Rollback();
            log.Error("Inside Incentive_DataObjects- InsertIncentivePointToAuthor function, PublicationID: " + data.PaublicationID + "Type Of Entry: " + data.TypeOfEntry);
            log.Error(ex.Message);
            log.Error(ex.StackTrace);
            throw ex;
        }

        finally
        {
            con.Close();
        }
    }

    internal DataTable GetArticalWisePoints(string MemberId)
    {
        log.Debug("Incentive_DataObjects.cs :GetArticalWisePoints inside GetData function, MemberId=" + MemberId);
        try
        {
            con.Open();
            //    cmd1 = new SqlCommand("GetInterviewDetails", con1);
            cmd = new SqlCommand("Incentive_SelectArticlewiseIncentivePoint", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MemberId", MemberId);

            //cmd1.Parameters.AddWithValue("@keyword", keyword);

            DataTable dtable = new DataTable();
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            adp.Fill(dtable);
            return dtable;
        }

        catch (Exception e)
        {
            log.Error("Incentive_DataObjects Error: Inside GetArticalWisePoints catch block of GetData MemberId:" + MemberId);
            log.Error("Error msg:" + e);
            log.Error("Stack trace:" + e.StackTrace);
            throw e;
        }
        finally
        {
            con.Close();
        }
    }

    public bool CheckPatentId(string p1, string p2)
    {
        bool result = false;
        SqlDataReader sdr = null;
        con.Open();
        try
        {
            cmd = new SqlCommand("Incentive_CheckPublicationId", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ReferenceId", p1);
            cmd.Parameters.AddWithValue("@MemberId", p2);
            sdr = cmd.ExecuteReader();
            string ReferenceNumber = null;
            while (sdr.Read())
            {
                if (!Convert.IsDBNull(sdr["ReferenceNumber"]))
                {
                    ReferenceNumber = (string)sdr["ReferenceNumber"];
                }
            }
            if (ReferenceNumber == null)
            {
                result = false;
            }
            else
            {
                result = true;
            }

            return result;
        }

        catch (Exception ex)
        {
            log.Debug("Inside Incentive_DataObjects- CheckPublcationId function, ID: " + p2);
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

    public string SelectAuthorName(string empcode)
    {
        string Name = null;
        SqlDataReader sdr = null;
        con.Open();
        try
        {
            cmd = new SqlCommand("Select Prefix+FirstName as Name from User_M where User_Id=@Employeecode", con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@Employeecode", empcode);
            sdr = cmd.ExecuteReader();

            while (sdr.Read())
            {
                if (!Convert.IsDBNull(sdr["Name"]))
                {
                    Name = (string)sdr["Name"];
                }
            }


            return Name;
        }

        catch (Exception ex)
        {
            log.Error("Inside SelectAuthorEmailId function,  ID: " + empcode);
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

    public string SelectStudentAuthorName(string empcode, string p)
    {
        string AuthorName = null;
        SqlDataReader sdr = null;
        con.Open();
        try
        {
            cmd = new SqlCommand("Select AuthorName from Publishcation_Author where EmployeeCode=@MemberId and TypeOfEntry+PaublicationID=@ReferenceId", con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@MemberId", empcode);
            cmd.Parameters.AddWithValue("@ReferenceId", p);
            sdr = cmd.ExecuteReader();

            while (sdr.Read())
            {
                if (!Convert.IsDBNull(sdr["AuthorName"]))
                {
                    AuthorName = (string)sdr["AuthorName"];
                }
            }


            return AuthorName;
        }

        catch (Exception ex)
        {
            log.Error("Inside SelectStudentEmailId function,  ID: " + empcode);
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

    public IncentiveData CheckUniqueIdIncentive(string p1, string p2, EmailDetails details)
    {
        log.Debug("Inside function CheckUniqueIdIncentive of of Publication ID: " + p1);
        try
        {

            IncentiveData data = new IncentiveData();
            con = new SqlConnection(str);
            con.Open();
            string REF = (p2 + p1);
            transaction = con.BeginTransaction();
            //cmdString = "select ID,ReferenceID,subject+Module  as Module from EmailQueue where ReferenceID='" + (p2 + p1) + "' and Module='" + details.Module + "'";
            cmdString = "select ID,ReferenceID,subject+Module  as Module from EmailQueue where ReferenceID=@ReferenceID and Module=@Module";
           
            cmd = new SqlCommand(cmdString, con, transaction);
            cmd.Parameters.AddWithValue("@ReferenceID", (p2 + p1));
            cmd.Parameters.AddWithValue("@Module", details.Module);

            cmd.CommandType = CommandType.Text;

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
            log.Error("Inside Catch block of function CheckUniqueIdIncentive of  Publication ID: " + p1);
            log.Error(e.Message);
            log.Error(e.StackTrace);

            throw e;
        }
        finally
        {
            con.Close();
        }
    }

    public int updateEmailtrackerIncentive(string p1, string p2, EmailDetails details, IncentiveData obj3, string AuthorName1)
    {
        log.Debug("Inside function updateEmailtrackerIncentive of  Publication ID: " + p1);
        try
        {
            con = new SqlConnection(str);
            con.Open();
            transaction = con.BeginTransaction();
            //cmdString = "update EmailQueueTrackerTable set EmailqueueId='" + obj3.ID + "',RefferenceID='" + obj3.RefID + "'  where Publication_ProjectID='" + p1 + "' and subject='" + details.EmailSubject + "' and Module='" + details.Module + "'and Author_InvestigatorName='" + AuthorName1 + "'";
            cmdString = "update EmailQueueTrackerTable set EmailqueueId=@EmailqueueId,RefferenceID=@RefferenceID  where Publication_ProjectID=@p1 and subject=@subject and Module=@Module and Author_InvestigatorName=@AuthorName1";
            cmd = new SqlCommand(cmdString, con, transaction);
            cmd.Parameters.AddWithValue("@EmailqueueId", obj3.ID);
            cmd.Parameters.AddWithValue("@RefferenceID", obj3.RefID);
            cmd.Parameters.AddWithValue("@p1", p1);
            cmd.Parameters.AddWithValue("@subject", details.EmailSubject);
            cmd.Parameters.AddWithValue("@Module", details.Module);
            cmd.Parameters.AddWithValue("@AuthorName1", AuthorName1);

          
            cmd.CommandType = CommandType.Text;
            int data = cmd.ExecuteNonQuery();
            transaction.Commit();
            return data;
        }
        catch (Exception e)
        {
            log.Error("Inside Catch block of function updateEmailtrackerIncentive of  Publication ID: " + p1);
            log.Error(e.Message);
            log.Error(e.StackTrace);

            throw e;
        }
        finally
        {
            con.Close();
        }
    }

    public PublishData getquartileName(string p)
    {
        log.Debug("Inside function getquartileName of  Quartile ID: " + p);
        try
        {
        PublishData data = new PublishData();
            con = new SqlConnection(str);
            con.Open();
            transaction = con.BeginTransaction();
            //cmdString = "  select Name,Code from Quartile_M where Id='" + p + "'";
            cmdString = "select Name,Code from Quartile_M where Id=@p";
         

            cmd = new SqlCommand(cmdString, con, transaction);
            cmd.Parameters.AddWithValue("@p", p);
            cmd.CommandType = CommandType.Text;

            SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                if (!Convert.IsDBNull(sdr["Name"]))
                {
                    data.Name = (string)sdr["Name"];
                }
                if (!Convert.IsDBNull(sdr["Code"]))
                {
                    data.Code = (string)sdr["Code"];
                }
               
            }
            sdr.Close();
            transaction.Commit();
            return data;
        }
        catch (Exception e)
        {
            log.Error("Inside Catch block of function getquartileName of  Quartile ID: " + p);
            log.Error(e.Message);
            log.Error(e.StackTrace);

            throw e;
        }
        finally
        {
            con.Close();
        }
    }

    public PublishData getquartileid(string p)
    {
        log.Debug("Inside function getquartileid of  Quartile Name: " + p);
        try
        {
            PublishData data = new PublishData();
            con = new SqlConnection(str);
            con.Open();
            transaction = con.BeginTransaction();
            //cmdString = "  select Id from Quartile_M where Name='" + p + "'";
            cmdString = "  select Id from Quartile_M where Name=@p";
           

            cmd = new SqlCommand(cmdString, con, transaction);
            cmd.Parameters.AddWithValue("@p", p);
            cmd.CommandType = CommandType.Text;

            SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                if (!Convert.IsDBNull(sdr["Id"]))
                {
                    data.Id = (string)sdr["Id"];
                }

            }
            sdr.Close();
            transaction.Commit();
            return data;
        }
        catch (Exception e)
        {
            log.Error("Inside Catch block of function getquartileid of  Quartile Name: " + p);
            log.Error(e.Message);
            log.Error(e.StackTrace);

            throw e;
        }
        finally
        {
            con.Close();
        }
    }

    public PublishData getquartilecount(string p1, string p2, string p3)
    {
        log.Debug("Inside function getquartilecount of  MemberId:" + p2);
        try
        {
            PublishData data = new PublishData();
            con = new SqlConnection(str);
            con.Open();
            transaction = con.BeginTransaction();
            //cmdString = "SELECT COUNT(Quartile) as Count from Quartile_Author_LimitCount_Details where MemberId='" + p2 + "' and Quartile='" + p1 + "'and Year='" + p3 + "'and Isdeleted='N'";
            cmdString = "SELECT COUNT(Quartile) as Count from Quartile_Author_LimitCount_Details where MemberId=@p2 and Quartile=@p1 and Year=@p3 and Isdeleted='N'";

           
            cmd = new SqlCommand(cmdString, con, transaction);
            cmd.Parameters.AddWithValue("@p2", p2);
            cmd.Parameters.AddWithValue("@p1", p1);
            cmd.Parameters.AddWithValue("@p3", p3);
            cmd.CommandType = CommandType.Text;

            SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                if (!Convert.IsDBNull(sdr["Count"]))
                {
                    data.Count = (int)sdr["Count"];
                }

            }
            sdr.Close();
            transaction.Commit();
            return data;
        }
        catch (Exception e)
        {
            log.Error("Inside Catch block of function getquartilecount of   MemberId:" + p2);
            log.Error(e.Message);
            log.Error(e.StackTrace);

            throw e;
        }
        finally
        {
            con.Close();
        }
    }

    public PublishData getquartilelimit(string p)
    {
        log.Debug("Inside function getquartilelimit of  Quartile:" + p);
        try
        {
            PublishData data = new PublishData();
            con = new SqlConnection(str);
            con.Open();
            transaction = con.BeginTransaction();
            //cmdString = "  select Limit from Quartile_Limit_M where Quartile='"+p+"'";
            cmdString = "  select Limit from Quartile_Limit_M where Quartile=@p";
           

            cmd = new SqlCommand(cmdString, con, transaction);
            cmd.Parameters.AddWithValue("@p", p);
            cmd.CommandType = CommandType.Text;

            SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                if (!Convert.IsDBNull(sdr["Limit"]))
                {
                    data.Limit = (int)sdr["Limit"];
                }

            }
            sdr.Close();
            transaction.Commit();
            return data;
        }
        catch (Exception e)
        {
            log.Error("Inside Catch block of function getquartilelimit of   Quartile:" + p);
            log.Error(e.Message);
            log.Error(e.StackTrace);

            throw e;
        }
        finally
        {
            con.Close();
        }
    }

    public DataTable SelectMUAuthorDetailsforARI(string Pid, string TypeEntry)
    {
        log.Debug("Inside SelectMUAuthorDetailsforARI function, PublicationID: " + Pid + "Type Of Entry: " + TypeEntry);
        con.Open();
        DataTable ds = null;
        try
        {
            SqlDataAdapter da;
            cmd = new SqlCommand("Incentive_SelectMUAuthorDetails", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@PaublicationID", SqlDbType.VarChar, 15);
            cmd.Parameters["@PaublicationID"].Value = Pid;
            cmd.Parameters.Add("@TypeOfEntry", SqlDbType.VarChar, 5);
            cmd.Parameters["@TypeOfEntry"].Value = TypeEntry;
            da = new SqlDataAdapter(cmd);
            ds = new DataTable();
            da.Fill(ds);
            return ds;
        }

        catch (Exception ex)
        {
            log.Error("Inside catch block function SelectMUAuthorDetailsforARI function, PublicationID: " + Pid + "Type Of Entry: " + TypeEntry);
            log.Error(ex.Message);
            log.Error(ex.StackTrace);
            throw ex;
        }

        finally
        {
            con.Close();
        }
    }

    public DataTable SelectAuthorDetailsforARI(string Pid, string TypeEntry)
    {
        log.Debug("Inside SelectAuthorDetailsforARI function, PublicationID: " + Pid + "Type Of Entry: " + TypeEntry);
        con.Open();
        DataTable ds = null;
        try
        {
            SqlDataAdapter da;

            cmd = new SqlCommand("Incentive_SelectAuthorDetailsforARI", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@PaublicationID", SqlDbType.VarChar, 15);
            cmd.Parameters["@PaublicationID"].Value = Pid;
            cmd.Parameters.Add("@TypeOfEntry", SqlDbType.VarChar, 5);
            cmd.Parameters["@TypeOfEntry"].Value = TypeEntry;
            da = new SqlDataAdapter(cmd);
            ds = new DataTable();
            da.Fill(ds);
            return ds;
        }

        catch (Exception ex)
        {
            log.Error("Inside catch block function SelectAuthorDetailsforARI function, PublicationID: " + Pid + "Type Of Entry: " + TypeEntry);
            log.Error(ex.Message);
            log.Error(ex.StackTrace);
            throw ex;
        }

        finally
        {
            con.Close();
        }
    }

    public DataSet SelectApprovedIncentivePublicationsforARI(PublishData data)
    {
        try
        {

            con.Open();
            cmd = new SqlCommand("Incentive_SelectApprovedPublicationsforARI", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID", data.PaublicationID);
            cmd.Parameters.AddWithValue("@Title", data.JournalTitle);
            cmd.Parameters.AddWithValue("@Type", data.TypeOfEntry);
            cmd.Parameters.AddWithValue("@BulkYear", data.bulkpublicationyear);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

        catch (Exception ex)
        {
            log.Error(ex.Message);
            log.Error(ex.StackTrace);
            throw ex;
        }

        finally
        {
            con.Close();
        }
    }

    public bool InsertARIPointToAuthor(PublishData[] JD, PublishData data)
    {
        log.Debug("Inside InsertARIPointToAuthor function of Publication ID: " + data.PaublicationID + " Type of Entry : " + data.TypeOfEntry);
        con.Open();
        transaction = con.BeginTransaction();
        bool result = false;
        try
        {
            for (int i = 0; i < JD.Length; i++)
            {
                cmd = new SqlCommand("Incentive_InsertARIPoint", con, transaction);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@PaublicationID", data.PaublicationID);
                cmd.Parameters.AddWithValue("@TypeOfEntry", data.TypeOfEntry);
                cmd.Parameters.AddWithValue("@EmployeeCode", JD[i].EmployeeCode);
                cmd.Parameters.AddWithValue("@TotalPoints", JD[i].TotalPoint);
                cmd.Parameters.AddWithValue("@ARIPoint", JD[i].ARIPoint);
                result = Convert.ToBoolean(cmd.ExecuteNonQuery());
            }
            for (int i = 0; i < JD.Length; i++)
            {
                cmd = new SqlCommand("Incentive_SelectMemberCurrentBalance", con, transaction);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MemberId", JD[i].EmployeeCode);
                SqlDataReader sdr1 = cmd.ExecuteReader();
                IncentivePoint points = new IncentivePoint();
                if (sdr1.HasRows)
                {
                    while (sdr1.Read())
                    {
                        if (!Convert.IsDBNull(sdr1["Currentbalance"]))
                        {
                            points.CurrentBalance = (double)sdr1["Currentbalance"];
                        }
                        if (!Convert.IsDBNull(sdr1["OldBalance"]))
                        {
                            points.OpeningBalance = (double)sdr1["OldBalance"];
                        }
                        if (!Convert.IsDBNull(sdr1["MemberId"]))
                        {
                            points.MemberId = (string)sdr1["MemberId"];
                        }
                    }
                }

                sdr1.Close();

                if (points.MemberId == null)
                {
                    cmd = new SqlCommand("Incentive_InsertMemberIncentivePointSummary", con, transaction);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MemberId", JD[i].EmployeeCode);
                    cmd.Parameters.AddWithValue("@Membertype", JD[i].MUNonMU);
                    cmd.Parameters.AddWithValue("@Oldbalance", points.OpeningBalance);
                    cmd.Parameters.AddWithValue("@Currentbalance", points.CurrentBalance + JD[i].TotalPoint);
                    cmd.Parameters.AddWithValue("@LastTransactionDate", DateTime.Now);
                    cmd.Parameters.AddWithValue("@UserId", HttpContext.Current.Session["UserId"].ToString());
                    cmd.Parameters.AddWithValue("@Date", DateTime.Now);

                    result = Convert.ToBoolean(cmd.ExecuteNonQuery());
                }
                else
                {
                    cmd = new SqlCommand("Incentive_UpdateMemberIncentivePointSummary", con, transaction);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MemberId", JD[i].EmployeeCode);
                    if (points.CurrentBalance != 0.0)
                    {
                        cmd.Parameters.AddWithValue("@Currentbalance", points.CurrentBalance + JD[i].TotalPoint);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@Currentbalance", JD[i].TotalPoint);
                    }
                    cmd.Parameters.AddWithValue("@LastTransactionDate", DateTime.Now);
                    cmd.Parameters.AddWithValue("@UserId", HttpContext.Current.Session["UserId"].ToString());
                    cmd.Parameters.AddWithValue("@Date", DateTime.Now);

                    result = Convert.ToBoolean(cmd.ExecuteNonQuery());
                }
                if (result == true)
                {
                    cmd = new SqlCommand("Incentive_SelectMaxEntryNo", con, transaction);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MemberId", JD[i].EmployeeCode);
                    SqlDataReader sdr = cmd.ExecuteReader();
                    int entryno = 0;
                    if (sdr.HasRows)
                    {
                        while (sdr.Read())
                        {
                            if (!Convert.IsDBNull(sdr["EntryNo"]))
                            {
                                entryno = (int)sdr["EntryNo"];
                            }

                        }
                    }
                    else
                    {
                        entryno = 0;
                    }
                    sdr.Close();

                    cmd = new SqlCommand("Incentive_InsertMemberIncentivePointTransactionARI", con, transaction);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@MemberId", JD[i].EmployeeCode);
                    cmd.Parameters.AddWithValue("@EntryNo", entryno + 1);
                    cmd.Parameters.AddWithValue("@TransactionType", JD[i].TransactionType);
                    cmd.Parameters.AddWithValue("@ReferenceNumber", data.TypeOfEntry + data.PaublicationID);
                    cmd.Parameters.AddWithValue("@TotalPoint", JD[i].TotalPoint);
                    cmd.Parameters.AddWithValue("@UtilizationDate", DBNull.Value);
                    cmd.Parameters.AddWithValue("@BasePoint", 0);
                    cmd.Parameters.AddWithValue("@ARIPoints", JD[i].ARIPoint);
                    cmd.Parameters.AddWithValue("@ThresholdPoint", 0);
                    cmd.Parameters.AddWithValue("@Remarks", DBNull.Value);
                    cmd.Parameters.AddWithValue("@UserId", HttpContext.Current.Session["UserId"].ToString());
                    cmd.Parameters.AddWithValue("@Date", DateTime.Now);
                    result = Convert.ToBoolean(cmd.ExecuteNonQuery());

                }
            }

            transaction.Commit();
            log.Info("The Publication with id " + data.PaublicationID + " and  Type of Entry : " + data.TypeOfEntry + "is processed for incentive point entry");
            return result;
        }
        catch (Exception ex)
        {
            transaction.Rollback();
            log.Error("Inside Incentive_DataObjects- InsertARIPointToAuthor function, PublicationID: " + data.PaublicationID + "Type Of Entry: " + data.TypeOfEntry);
            log.Error(ex.Message);
            log.Error(ex.StackTrace);
            throw ex;
        }

        finally
        {
            con.Close();
        }
    }

    public PublishData CheckARIApplicability(object p)
    {

        log.Debug("Inside function CheckARIApplicability of  Quartile ID: " + p);
        try
        {
            PublishData data = new PublishData();
            con = new SqlConnection(str);
            con.Open();
            transaction = con.BeginTransaction();
            //cmdString = "select ARIApplicability from Quartile_Limit_M where Quartile='" + p + "'";
            cmdString = "select ARIApplicability from Quartile_Limit_M where Quartile=@p";
           

            cmd = new SqlCommand(cmdString, con, transaction);
            cmd.Parameters.AddWithValue("@p", p);
            cmd.CommandType = CommandType.Text;

            SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                if (!Convert.IsDBNull(sdr["ARIApplicability"]))
                {
                    data.ARIApplicability = (string)sdr["ARIApplicability"];
                }

            }
            sdr.Close();
            transaction.Commit();
            return data;
        }
        catch (Exception e)
        {
            log.Error("Inside Catch block of function CheckARIApplicability of  Quartile ID: " + p);
            log.Error(e.Message);
            log.Error(e.StackTrace);

            throw e;
        }
        finally
        {
            con.Close();
        }

    }

    public DataSet SelectPendingProcessedPublicationsForIncentivePointRevert(PublishData data)
    {
        try
        {

            con.Open();
            cmd = new SqlCommand("Incentive_SelectPublicationsforIncentivePointRevert", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID", data.PaublicationID);
            cmd.Parameters.AddWithValue("@Title", data.JournalTitle);
            cmd.Parameters.AddWithValue("@Type", data.TypeOfEntry);
            cmd.Parameters.AddWithValue("@BulkYear", data.bulkpublicationyear);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

        catch (Exception ex)
        {
            log.Error(ex.Message);
            log.Error(ex.StackTrace);
            throw ex;
        }

        finally
        {
            con.Close();
        }
    }




    public IncentivePoint SelectRevertingPointsforAuthorDetails(string p1, string p2, string p3)
    {
        log.Debug("Inside function SelectRevertingPointsforAuthorDetails of  EmployeeID: " + p1);
        try
        {
            //IncentiveData data = new IncentiveData();
            IncentivePoint data = new IncentivePoint();
            con = new SqlConnection(str);
            con.Open();
            transaction = con.BeginTransaction();
            //cmdString = "select a.CurrentBalance as OldCurrentBalance,sum(b.TotalPoint) as RevertingPoint  from Member_Incentive_Point_Summary a ,Member_Incentive_Point_Transaction b  where a.MemberId=b.MemberId and b.Isdeleted='N' and b.MemberId='" + p1 + "' and b.ReferenceNumber='" + p2 + "' group by a.MemberId,a.CurrentBalance";
            cmdString = "select a.CurrentBalance as OldCurrentBalance,sum(b.TotalPoint) as RevertingPoint  from Member_Incentive_Point_Summary a ,Member_Incentive_Point_Transaction b  where a.MemberId=b.MemberId and b.Isdeleted='N' and b.MemberId=@p1 and b.ReferenceNumber=@p2 group by a.MemberId,a.CurrentBalance";
           
            cmd = new SqlCommand(cmdString, con, transaction);
            cmd.Parameters.AddWithValue("@p1", p1);
            cmd.Parameters.AddWithValue("@p2", p2);

            cmd.CommandType = CommandType.Text;

            SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                if (!Convert.IsDBNull(sdr["OldCurrentBalance"]))
                {
                    data.CurrentBalance = (double)sdr["OldCurrentBalance"];
                }
                if (!Convert.IsDBNull(sdr["RevertingPoint"]))
                {
                    data.TotalPoint = (double)sdr["RevertingPoint"];
                }


            }
            sdr.Close();
            //cmdString = "select IsAwarded,TotalNoOfPublications from Member_Yearwise_Publication_Summary where MemberId='" + p1 + "'and Year='" + p3 + "'";
            cmdString = "select IsAwarded,TotalNoOfPublications from Member_Yearwise_Publication_Summary where MemberId=@p1 and Year=@p3";
       
            cmd = new SqlCommand(cmdString, con, transaction);
            cmd.Parameters.AddWithValue("@p1", p1);
            cmd.Parameters.AddWithValue("@p3", p3);
            cmd.CommandType = CommandType.Text;

            SqlDataReader sdr1 = cmd.ExecuteReader();
            while (sdr1.Read())
            {
                if (!Convert.IsDBNull(sdr1["IsAwarded"]))
                {
                    data.isAwarded = (string)sdr1["IsAwarded"];
                }
                if (!Convert.IsDBNull(sdr1["TotalNoOfPublications"]))
                {
                    data.TotalNoOfPublications = (int)sdr1["TotalNoOfPublications"];
                }
                
                
            }
            sdr1.Close();
            if (data.isAwarded == "Y")
            {

                //cmdString = "select Points from Member_Yearwise_Publication_Summary where MemberId='" + p1 + "'and Year='" + p3 + "'";
                cmdString = "select Points from Member_Yearwise_Publication_Summary where MemberId=@p1 and Year=@p3";
               
            cmd = new SqlCommand(cmdString, con, transaction);
            cmd.Parameters.AddWithValue("@p1", p1);
            cmd.Parameters.AddWithValue("@p3", p3);
            cmd.CommandType = CommandType.Text;

            SqlDataReader sdr2 = cmd.ExecuteReader();
            while (sdr2.Read())
            {
                if (!Convert.IsDBNull(sdr2["Points"]))
                {
                    data.Points = (double)sdr2["Points"];
                }
                
            }
            sdr2.Close();
            }
            transaction.Commit();
            return data;
        }
        catch (Exception e)
        {
            log.Error("Inside Catch block of function SelectRevertingPointsforAuthorDetails of  EmployeeID: " + p1);
            log.Error(e.Message);
            log.Error(e.StackTrace);

            throw e;
        }
        finally
        {
            con.Close();
        }

    }

    public bool RevertIncentivePointToAuthor(IncentivePoint[] JD, PublishData data)
    {
        
        log.Debug("Inside function RevertIncentivePointToAuthor of publicationID:'"+data.PaublicationID);
        try
        {
            //IncentiveData data = new IncentiveData();
            bool result=false;
            IncentivePoint a = new IncentivePoint();
            con = new SqlConnection(str);
            con.Open();
            transaction = con.BeginTransaction();
            for (int i = 0; i < JD.Length; i++)
            {
                //cmd = new SqlCommand("select  max(EntryNo) as EntryNo  from Member_Incentive_Point_Reverting_Tracker  where MemberId='" + JD[i].MemberId + "'", con, transaction);
                cmd = new SqlCommand("select  max(EntryNo) as EntryNo  from Member_Incentive_Point_Reverting_Tracker  where MemberId=@MemberId", con, transaction);
                cmd.Parameters.AddWithValue("@MemberId", JD[i].MemberId);
                cmd.CommandType = CommandType.Text;
                SqlDataReader sdr = cmd.ExecuteReader();
                int entryno = 0;
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        if (!Convert.IsDBNull(sdr["EntryNo"]))
                        {
                            entryno = (int)sdr["EntryNo"];
                        }

                    }
                }
                else
                {
                    entryno = 0;
                }
                sdr.Close();

                   cmdString = "RevertingIncentivePointStatus";
                   cmd = new SqlCommand(cmdString, con, transaction);
                   cmd.CommandType = CommandType.StoredProcedure;
                   cmd.Parameters.AddWithValue("@MemberId", JD[i].MemberId);
                   cmd.Parameters.AddWithValue("@PublishJAYear ",data.PublishJAYear);
                   cmd.Parameters.AddWithValue("@TypeOfEntry",data.TypeOfEntry);
                   cmd.Parameters.AddWithValue("@PaublicationID",data.PaublicationID); 
                   cmd.Parameters.AddWithValue("@CreatedBy",data.CreatedBy);
                   cmd.Parameters.AddWithValue("@CreatedDate",data.CreatedDate);
                   cmd.Parameters.AddWithValue("@EntryNoforRevert", entryno+1);
                   result = Convert.ToBoolean(cmd.ExecuteNonQuery());
                   
                 if ((Convert.ToInt32(data.PublishJAYear) >= 2018) && (data.PublishJAMonth>= 7))
                 {
                     if (data.Quartile != "")
                     {
                         //cmdString = "update Quartile_Author_LimitCount_Details set Isdeleted='Y' where MemberId='" + JD[i].MemberId + "'and Year='" + data.PublishJAYear + "'and PublicationId='" + data.PaublicationID + "' and Quartile='" + data.Quartile + "'";
                         cmdString = "update Quartile_Author_LimitCount_Details set Isdeleted='Y' where MemberId=@MemberId and Year=@PublishJAYear  and PublicationId=@PaublicationID and Quartile=@Quartile";

                         
                         cmd = new SqlCommand(cmdString, con, transaction);
                         cmd.Parameters.AddWithValue("@MemberId", JD[i].MemberId);
                         cmd.Parameters.AddWithValue("@PublishJAYear", data.PublishJAYear);
                         cmd.Parameters.AddWithValue("@PaublicationID", data.PaublicationID);
                         cmd.Parameters.AddWithValue("@Quartile", data.Quartile);
                         cmd.CommandType = CommandType.Text;
                         int result1 = cmd.ExecuteNonQuery();

                         //cmdString = "select count(EntryNo) as EntryNo from Quartile_Author_LimitCount_Details where MemberId='" + JD[i].MemberId + "' and Year='" + data.PublishJAYear + "' and Isdeleted='N' and PublicationId !='" + data.PaublicationID + "'";
                         cmdString = "select count(EntryNo) as EntryNo from Quartile_Author_LimitCount_Details where MemberId=@MemberId and Year=@PublishYear and Isdeleted='N' and PublicationId !=@PaublicationID";
                         cmd = new SqlCommand(cmdString, con, transaction);                        
                         cmd.CommandType = CommandType.Text;
                         cmd.Parameters.AddWithValue("@MemberId", JD[i].MemberId);
                         cmd.Parameters.AddWithValue("@PublishYear",data.PublishJAYear);
                         cmd.Parameters.AddWithValue("@PaublicationID", data.PaublicationID);
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
                         cmdString = "Update Quartile_Author_LimitCount_Summary set Count=@Count,Quartile=@Quartile where MemberId=@MemberId and Year=@Year";
                         cmd = new SqlCommand(cmdString, con, transaction);
                         cmd.CommandType = CommandType.Text;
                         cmd.Parameters.AddWithValue("@MemberId", JD[i].MemberId);
                         cmd.Parameters.AddWithValue("@Year",data.PublishJAYear);
                         cmd.Parameters.AddWithValue("@Quartile", data.Quartile);
                         cmd.Parameters.AddWithValue("@Count", count);
                         int result2 = cmd.ExecuteNonQuery();
                     }

                 }
                
             }
            if (result == true)
            {
                cmd = new SqlCommand("update Publication set  Status=@Status,CancelledBy=@CancelledBy,cancelledDate=@cancelledDate,CancelRemarks=@CancelRemarks,IncentivePointStatus=@IncentivePointStatus where PublicationID=@PaublicationID and TypeOfEntry=@TypeOfEntry", con, transaction);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@PaublicationID", data.PaublicationID);
                cmd.Parameters.AddWithValue("@TypeOfEntry", data.TypeOfEntry);
                cmd.Parameters.AddWithValue("@Status", "CAN");
                cmd.Parameters.AddWithValue("@CancelledBy", data.CreatedBy);
                cmd.Parameters.AddWithValue("@cancelledDate", data.CreatedDate);
                cmd.Parameters.AddWithValue("@CancelRemarks", data.PubCancelRemarks);
                cmd.Parameters.AddWithValue("@IncentivePointStatus", "CAN");
                int result3 = cmd.ExecuteNonQuery();
                log.Info("Reverting Incentive Point : User Name :" + HttpContext.Current.Session["UserName"] + "Role :" + HttpContext.Current.Session["RoleName"]);
            }
            transaction.Commit();
            return result ;
        }
        catch (Exception e)
        {
            log.Error("Inside Catch block of function RevertIncentivePointToAuthor of publicationID:'" + data.PaublicationID);
            log.Error(e.Message);
            log.Error(e.StackTrace);

            throw e;
        }
        finally
        {
            con.Close();
        }
    }

 
    public PublishData CheckIncentivePointEntry(PublishData[] JD, PublishData data)
    {
        log.Debug("Inside function CheckIncentivePointEntry of publicationID:'" + data.PaublicationID);
        try
        {
            PublishData b = new PublishData();
            int result = 0;
            IncentivePoint a = new IncentivePoint();

            con = new SqlConnection(str);
            con.Open();
            transaction = con.BeginTransaction();

            //cmdString = "select IncentivePointStatus from Publication where TypeOfEntry+PublicationID='" + data.TypeOfEntry + data.PaublicationID + "'";
            cmdString = "select IncentivePointStatus from Publication where TypeOfEntry+PublicationID=@data";
         
            cmd = new SqlCommand(cmdString, con, transaction);
            cmd.Parameters.AddWithValue("@data", (data.TypeOfEntry + data.PaublicationID));
            cmd.CommandType = CommandType.Text;
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    if (!Convert.IsDBNull(sdr["IncentivePointStatus"]))
                    {
                        b.IncentivePointSatatus = (string)sdr["IncentivePointStatus"];
                    }

                }
            }
            else
            {
                b.IncentivePointSatatus = "";
            }
            sdr.Close();
            transaction.Commit();
            return b;
        }
        catch (Exception e)
        {
            log.Error("Inside Catch block of function CheckIncentivePointEntry of publicationID:'" + data.PaublicationID);
            log.Error(e.Message);
            log.Error(e.StackTrace);

            throw e;
        }
        finally
        {
            con.Close();
        }
    }

    public PublishData CheckIncentivePointRevertStatus(IncentivePoint[] JD, PublishData data)
    {
        log.Debug("Inside function CheckIncentivePointRevertStatus of publicationID:'" + data.PaublicationID);
        try
        {
            PublishData b = new PublishData();
            int result = 0;
            IncentivePoint a = new IncentivePoint();

            con = new SqlConnection(str);
            con.Open();
            transaction = con.BeginTransaction();
        
                //cmdString = "select Status,IncentivePointStatus from Publication where TypeOfEntry+PublicationID='" + data.TypeOfEntry + data.PaublicationID + "'";
            cmdString = "select Status,IncentivePointStatus from Publication where TypeOfEntry+PublicationID=@data";
            
                cmd = new SqlCommand(cmdString, con, transaction);
                cmd.Parameters.AddWithValue("@data", (data.TypeOfEntry + data.PaublicationID));
                cmd.CommandType = CommandType.Text;
                SqlDataReader sdr = cmd.ExecuteReader();
                if (sdr.HasRows)
                {
                    while (sdr.Read())
                    {
                        if (!Convert.IsDBNull(sdr["Status"]))
                        {
                            b.Status = (string)sdr["Status"];
                        }
                        else
                        {
                            b.Status = "";
                        }
                        if (!Convert.IsDBNull(sdr["IncentivePointStatus"]))
                        {
                            b.IncentivePointSatatus = (string)sdr["IncentivePointStatus"];
                        }
                        else
                        {
                            b.IncentivePointSatatus = "";
                        }

                    }
                }
                
                sdr.Close();

            transaction.Commit();
            return b;
        }
        catch (Exception e)
        {
            log.Error("Inside Catch block of function CheckIncentivePointRevertStatus of publicationID:'" + data.PaublicationID);
            log.Error(e.Message);
            log.Error(e.StackTrace);

            throw e;
        }
        finally
        {
            con.Close();
        }
    }

    public PublishData CheckIncentivePointstatusPatent(PublishData[] JD, PublishData data)
    {
        log.Debug("Inside function CheckIncentivePointstatusPatent of publicationID:'" + data.PaublicationID);
        try
        {
            PublishData b = new PublishData();
            int result = 0;
            IncentivePoint a = new IncentivePoint();

            con = new SqlConnection(str);
            con.Open();
            transaction = con.BeginTransaction();

            //cmdString = "select IncentivePointStatus from Patent_Data where ID='" + data.PaublicationID + "'";
            cmdString = "select IncentivePointStatus from Patent_Data where ID=@ID";
         
            cmd = new SqlCommand(cmdString, con, transaction);
            cmd.Parameters.AddWithValue("@ID", data.PaublicationID);
            cmd.CommandType = CommandType.Text;
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    if (!Convert.IsDBNull(sdr["IncentivePointStatus"]))
                    {
                        b.IncentivePointSatatus = (string)sdr["IncentivePointStatus"];
                    }

                }
            }
            else
            {
                b.IncentivePointSatatus = "";
            }
            sdr.Close();

            transaction.Commit();
            return b;
        }
        catch (Exception e)
        {
            log.Error("Inside Catch block of function CheckIncentivePointstatusPatent of publicationID:'" + data.PaublicationID);
            log.Error(e.Message);
            log.Error(e.StackTrace);

            throw e;
        }
        finally
        {
            con.Close();
        }
    }

    public PublishData getIsStudentAuthor(string p)
    {
        log.Debug("Inside function getIsStudentAuthor of publicationID:'" + p);
        try
        {
            PublishData b = new PublishData();
            int result = 0;
            string ISStudentAuthor ="";

            con = new SqlConnection(str);
            con.Open();


            //cmdString = "select ISStudentAuthor,TitleWorkItem from Publication where TypeOfEntry+PublicationID='" + p + "'";
            cmdString = "select ISStudentAuthor,TitleWorkItem from Publication where TypeOfEntry+PublicationID=@p";
          
            cmd = new SqlCommand(cmdString, con);
            cmd.Parameters.AddWithValue("@p", p);
            cmd.CommandType = CommandType.Text;
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    if (!Convert.IsDBNull(sdr["ISStudentAuthor"]))
                    {
                        b.IsStudentAuthor = (string)sdr["ISStudentAuthor"];
                    }
                    if (!Convert.IsDBNull(sdr["TitleWorkItem"]))
                    {
                        b.TitleWorkItem = (string)sdr["TitleWorkItem"];
                    }

                }
            }
            
            sdr.Close();

           
            return b;
        }
        catch (Exception e)
        {
            log.Error("Inside Catch block of function getIsStudentAuthor of publicationID:'" + p);
            log.Error(e.Message);
            log.Error(e.StackTrace);

            throw e;
        }
        finally
        {
            con.Close();
        }
    }

    internal ArrayList selectHrAdditionalInstitute(string UserId)
    {
        ArrayList list = new ArrayList();
        EmailDetails details = new EmailDetails();
        SqlDataReader sdr = null;
        string institute = null;
        con.Open();
        try
        {
            //cmd = new SqlCommand("select InstituteId from HR_Additional_Institute_Map where UserID='" + UserId + "'", con);
            cmd = new SqlCommand("select InstituteId from HR_Additional_Institute_Map where UserID=@UserId", con);
            cmd.Parameters.AddWithValue("@UserId", UserId);
            cmd.CommandType = CommandType.Text;         
            sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {

                if (!Convert.IsDBNull(sdr["InstituteId"]))
                {
                    institute = (string)sdr["InstituteId"];
                }
                else
                {
                    institute = "";

                }
                list.Add(institute);            
            }


            return list;
        }

        catch (Exception ex)
        {
            log.Error("Inside selectHrAdditionalInstitute function,  ID: " + UserId);
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

    internal DataSet SelectMembersAdditionalInstitute(string UserId)
    {
        log.Debug("Inside the SelectMembersAdditionalInstitute function: " );
        con.Open();
        DataSet ds = null;
        transaction = con.BeginTransaction();
        try
        {
            SqlDataAdapter da;

              cmd = new SqlCommand("Incentive_SelectMembersAdditionalInstitute", con, transaction);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@UserId", SqlDbType.VarChar, 500);
                cmd.Parameters["@UserId"].Value =UserId;

                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds);
               
            
            transaction.Commit();
            return ds;
        }

        catch (Exception ex)
        {
            log.Error("Inside the SelectMembersInstitute function: ");
            log.Error(ex.Message);
            log.Error(ex.StackTrace);
            throw ex;
        }

        finally
        {
            con.Close();
        }
    }

    internal DataSet SelectMembersAdditionalInstitutewise(string UserId, string member, string membername)
    {
       con.Open();
        DataSet ds = null;
     
        try
        {
            SqlDataAdapter da;        
            cmd = new SqlCommand("Incentive_SelectMembersAdditionalIntitutewise", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@UserId", SqlDbType.VarChar, 500);
                cmd.Parameters["@UserId"].Value = UserId;
                cmd.Parameters.Add("@MemberID", SqlDbType.VarChar, 50);
                cmd.Parameters["@MemberID"].Value = member;
                cmd.Parameters.Add("@MemberName", SqlDbType.VarChar, 50);
                cmd.Parameters["@MemberName"].Value = membername;
                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                da.Fill(ds);
            
        
            return ds;
        }

        catch (Exception ex)
        {
            log.Error("Inside SelectMembersInstitutewise function: " + member);
            log.Error(ex.Message);
            log.Error(ex.StackTrace);
            throw ex;
        }

        finally
        {
            con.Close();
        }
    
    }

  
    
    
}