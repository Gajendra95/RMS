using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for SendMailObject
/// </summary>
public class SendMailObject
{
    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    public string str;
    public string cmdString;
    public SqlConnection con;
    public SqlCommand cmd;
    SqlTransaction transaction;
	public SendMailObject()
	{
        str = ConfigurationManager.ConnectionStrings["RMSConnectionString"].ConnectionString;
        cmdString = "";
        con = new SqlConnection(str);
        cmd = new SqlCommand(cmdString, con);
	}

    public bool InsertIntoEmailQueue(EmailDetails details)
    {
        log.Debug("InsertEmailQueue:- Inside InsertIntoEmailQueue function of Module : " + details.Module+ ",Type : "+ details.Type+",Id:"+details.Id+",Unit: "+details.ProjectUnit);
        bool result = false;
        con = new SqlConnection(str);
        con.Open();
        transaction = con.BeginTransaction();

        try
        {
            cmd = new SqlCommand("InsertEmailDetails", con, transaction);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@EmailSubject", details.EmailSubject);
            if (details.ToEmail == null)
            {
                cmd.Parameters.AddWithValue("@ToEmail", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@ToEmail", details.ToEmail);
            }

            cmd.Parameters.AddWithValue("@FromEmail", details.FromEmail);
            if (details.CCEmail == null)
            {
                cmd.Parameters.AddWithValue("@CCEmail", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@CCEmail", details.CCEmail);
            }
            if (details.BCCEmail == null)
            {
                cmd.Parameters.AddWithValue("@BCCEmail", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@BCCEmail", details.BCCEmail);
            }
            cmd.Parameters.AddWithValue("@Module", details.Module);

            cmd.Parameters.AddWithValue("@MsgBody", details.MsgBody);
            cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
            if (details.Type != "")
            {
                if (details.ProjectUnit == null)
                {
                    cmd.Parameters.AddWithValue("@ReferenceId", details.Type + details.Id);
                }
                else if (details.ProjectUnit != null)
                {
                    cmd.Parameters.AddWithValue("@ReferenceId", details.UnitId + details.Id);
                }
                
            }
            else
            {
                cmd.Parameters.AddWithValue("@ReferenceId", details.Id);
            }
            result = Convert.ToBoolean(cmd.ExecuteNonQuery());
            log.Info("InsertEmailQueue:-Email details inserted to EmailQueue table of : " + details.Module + ",Type : " + details.Type + ",Id:" + details.Id);

            transaction.Commit();
            return result;
        }
        catch (Exception e)
        {
            log.Error("InsertEmailQueue:- Inside InsertIntoEmailQueue function catch block of Module: " + details.Module + ",Type : " + details.Type + ",Id:" + details.Id);
            log.Error(e.Message);
            log.Error(e.StackTrace);
            transaction.Rollback();
            return result;
        }

    }

    public DataSet SelectEMailQueueDetails()
    {
        log.Debug("SelectEMailQueueDetails:- Inside SelectEMailQueueDetails function-to collect pending email details");
        con = new SqlConnection(str);
        con.Open();

        cmd = new SqlCommand("SelectEmailQueueDetails", con);
        cmd.CommandType = CommandType.StoredProcedure;
        DataSet ds = new DataSet();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(ds);
        con.Close();
        return ds;
       
    }

    public bool UpdateEmailSendFlag(string id, object module, string subject)
    {
        log.Debug("Inside UpdateEmailSendFlag function-to set emailsend flag to 'Y' of Id : " + id+ "Module: "+module+"Subject: "+subject);

        con = new SqlConnection(str);
        con.Open();
        transaction = con.BeginTransaction();
        bool result1 = false;
        try
        {
            SqlCommand cmd = new SqlCommand("UpdateEmailQueueSentFlag", con, transaction);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.Parameters.AddWithValue("@Date", DateTime.Now);
            result1 = Convert.ToBoolean(cmd.ExecuteNonQuery());
            log.Info("EmailSend Flag updated to Y  for  the id: " + id +" Module: "+ module+" Subject: "+subject);
            transaction.Commit();
            return result1;
        }

        catch (Exception ex)
        {
            transaction.Rollback();
            log.Error("Inside UpdateEmailSendFlag of catch block of id : " + id + " Module: " + module + " Subject: " + subject);
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