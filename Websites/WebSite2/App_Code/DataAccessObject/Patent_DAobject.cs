using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Patent_DAobject
/// </summary>
public class Patent_DAobject
{
    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    public string str;
    public string cmdString;
    string seedFinalUTN = "";
    public SqlConnection con;
    public SqlCommand cmd;

    SqlTransaction transaction;
    public Patent_DAobject()
    {
        str = ConfigurationManager.ConnectionStrings["RMSConnectionString"].ConnectionString;
        cmdString = "";
        con = new SqlConnection(str);
        cmd = new SqlCommand(cmdString, con);
    }

    public bool InsertPatent(Patent pat, GrantData[] jd)
    {


        int result = 0, result1 = 0, seed = 0, seed1 = 0, year1 = 0;
        bool result2 = false;

        string seedFinal = "";
        string seedUTN = "";
        string seedFinalUTN = "";
        con = new SqlConnection(str);
        con.Open();
        transaction = con.BeginTransaction();
        try
        {

            cmdString = "select seed from Id_Gen_Patent";
            cmd = new SqlCommand(cmdString, con, transaction);
            cmd.CommandType = CommandType.Text;
            seed = (int)cmd.ExecuteScalar();



            string seedStr = seed.ToString();
            int seedlen = seedStr.Length;
            int idlen = Convert.ToInt32(ConfigurationManager.AppSettings["GrantIdLen"]);
            string pre = "";

            for (int i = 0; i < idlen - seedlen; i++)
            {
                string z = "0";
                pre = pre + z;
            }
            seedFinal = pre + seed.ToString();

            DateTime date = Convert.ToDateTime(pat.Date_Of_Application);
            int yearvalue = date.Year;
            int resultvalue = 0;

            HttpContext.Current.Session["patentseed"] = seedFinal;

            string inst = HttpContext.Current.Session["InstituteId"].ToString();
            string patUTN = HttpContext.Current.Session["Department"].ToString();

            //cmdString = "select Seed,Year from Patent_UTN_Id_Gen where PT_UTN_ID in(select UTN_ID from Dept_M where Institute_Id=@Institute and DeptId=@DeptId)  and Year=" + yearvalue + "";
            cmdString = "select Seed,Year from Patent_UTN_Id_Gen where PT_UTN_ID in(select UTN_ID from Dept_M where Institute_Id=@Institute and DeptId=@DeptId)  and Year=@yearvalue";


            cmd = new SqlCommand(cmdString, con, transaction);

            cmd.CommandType = CommandType.Text;


            cmd.Parameters.AddWithValue("@Institute", inst);
            cmd.Parameters.AddWithValue("@DeptId", patUTN);
            cmd.Parameters.AddWithValue("@yearvalue", yearvalue);

            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    seed1 = (int)reader["Seed"];

                    year1 = (int)reader["Year"];
                }
            }
            else
            {
                seed1 = 1;
                year1 = yearvalue;

                resultvalue = 1;
            }
            reader.Close();

            cmdString = "select UTN_ID from Dept_M where Institute_Id=@Institute and DeptId=@DeptId";
            cmd = new SqlCommand(cmdString, con, transaction);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("@Institute", inst);
            cmd.Parameters.AddWithValue("@DeptId", patUTN);


            string seed2 = (string)cmd.ExecuteScalar();
            string seedStr1 = seed1.ToString();
            string seedStr2 = seed2.ToString();
            int seedlen1 = seedStr1.Length;
            int idlen1 = Convert.ToInt32(ConfigurationManager.AppSettings["GrantIdUTNLen"]);
            string pre1 = "";


            for (int i = 0; i < idlen1 - seedlen1; i++)
            {
                string z = "0";
                pre1 = pre1 + z;
            }
            seedUTN = pre1 + seed1.ToString();

            string convertyear = year1.ToString();
            string year_Utn = convertyear.Substring(convertyear.Length - 2);
            seedFinalUTN = "PT" + seedStr2 + year_Utn + seedUTN;
            HttpContext.Current.Session["patentseedUTNseed"] = seedFinalUTN;


            cmd = new SqlCommand("insertPatent_Data", con, transaction);
            cmd.CommandType = CommandType.StoredProcedure;
            // cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@ID", seedFinal);
            cmd.Parameters.AddWithValue("@Pat_UTN", seedFinalUTN);
            cmd.Parameters.AddWithValue("@Title", pat.Title);
            cmd.Parameters.AddWithValue("@Description", pat.description);
            if (pat.Entry_status != null)
            {
                cmd.Parameters.AddWithValue("@Entry_Status", pat.Entry_status);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Entry_Status", DBNull.Value);

            }
            cmd.Parameters.AddWithValue("@Filing_Status", pat.Filing_Status);
            cmd.Parameters.AddWithValue("@Filing_Office", pat.Filing_Office);
            cmd.Parameters.AddWithValue("@NatureOfPatent", pat.NatureOfPatent);
            cmd.Parameters.AddWithValue("@Funding", pat.Funding);
            cmd.Parameters.AddWithValue("@DetailsColaInstitiuteIndustry", pat.DetailsColaInstitiuteIndustry);
            cmd.Parameters.AddWithValue("@Country", pat.Country);
            if (pat.RevenueGenerated != 0.0)
            {
                cmd.Parameters.AddWithValue("@RevenueGenerated", pat.RevenueGenerated);
            }
            else
            {
                cmd.Parameters.AddWithValue("@RevenueGenerated", DBNull.Value);
            }

            string date3 = pat.Date_Of_Application.ToShortDateString();
            if (date3 != "01/01/0001")
            {
                cmd.Parameters.AddWithValue("@Date_Of_Application", pat.Date_Of_Application);

            }
            else
            {
                cmd.Parameters.AddWithValue("@Date_Of_Application", DBNull.Value);

            }
            cmd.Parameters.AddWithValue("@Application_Stage", pat.Application_Stage);
            //cmd.Parameters.AddWithValue("@Provisional_Number", pat.Provisional_Number);
            //string date4 = pat.FilingDateprovidedPatent.ToShortDateString();
            //if (date4 != "01/01/0001")
            //{
            //    cmd.Parameters.AddWithValue("@FilingDateprovidedPatent", pat.FilingDateprovidedPatent);
            //}
            //else
            //{
            //    cmd.Parameters.AddWithValue("@FilingDateprovidedPatent", DBNull.Value);
            //}
            cmd.Parameters.AddWithValue("@Patent_Number", pat.Patent_Number);
            cmd.Parameters.AddWithValue("@Application_Number", pat.Application_Number);
            string date6 = pat.Grant_Date.ToShortDateString();
            if (date6 != "01/01/0001")
            {
                cmd.Parameters.AddWithValue("@Grant_Date", pat.Grant_Date);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Grant_Date", DBNull.Value);
            }
            //   cmd.Parameters.AddWithValue("@Renewal_Fee", pat.Renewal_Fee);
            string date5 = pat.LastRenewalFeePaiddate.ToShortDateString();
            if (date5 != "01/01/0001")
            {
                cmd.Parameters.AddWithValue("@LastRenewalFeePaiddate", pat.LastRenewalFeePaiddate);
            }
            else
            {
                cmd.Parameters.AddWithValue("@LastRenewalFeePaiddate", DBNull.Value);
            }
            cmd.Parameters.AddWithValue("@Remarks", pat.Remarks);

            cmd.Parameters.AddWithValue("@CreatedDate", pat.Created_Date);
            cmd.Parameters.AddWithValue("@CreatedBy", pat.Created_By);
            if (pat.hasProjectreference != null && pat.hasProjectreference != "")
            {
                cmd.Parameters.AddWithValue("@hasProjectreference", pat.hasProjectreference);
            }
            else
            {
                cmd.Parameters.AddWithValue("@hasProjectreference", DBNull.Value);
            }
            if (pat.ProjectIDlist != null && pat.ProjectIDlist != "")
            {
                cmd.Parameters.AddWithValue("@ProjectIDlist", pat.ProjectIDlist);
            }
            else
            {
                cmd.Parameters.AddWithValue("@ProjectIDlist", DBNull.Value);
            }
            result2 = Convert.ToBoolean(cmd.ExecuteNonQuery());

            try
            {
                for (int i = 0; i < jd.Length; i++)
                {
                    cmd = new SqlCommand("InsertPatentInventor", con, transaction);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ID", seedFinal);
                    // cmd.Parameters.AddWithValue("@ProjectUnit", j.GrantUnit);
                    cmd.Parameters.AddWithValue("@GrantLine", i + 1);

                    cmd.Parameters.AddWithValue("@AuthorName", jd[i].AuthorName);
                    cmd.Parameters.AddWithValue("@MUNonMU", jd[i].MUNonMU);
                    cmd.Parameters.AddWithValue("@EmployeeCode", jd[i].EmployeeCode);

                    cmd.Parameters.AddWithValue("@Institution", jd[i].Institution);
                    cmd.Parameters.AddWithValue("@Department", jd[i].Department);

                    cmd.Parameters.AddWithValue("@InstitutionName", jd[i].InstitutionName);
                    cmd.Parameters.AddWithValue("@DepartmentName", jd[i].DepartmentName);
                    //  cmd.Parameters.AddWithValue("@AuthorType", jd[i].AuthorType);

                    cmd.Parameters.AddWithValue("@NationalInternational", jd[i].NationalInternationl);
                    cmd.Parameters.AddWithValue("@Continent", jd[i].continental);
                    cmd.Parameters.AddWithValue("@EmailId", jd[i].EmailId);
                    cmd.ExecuteNonQuery();

                    // log.Info("Grant investigator details inserted sucessfully  of Project Unit: " + j.GrantUnit + "ID: " + j.GID);
                }
                cmdString = "Select count(* ) as Count from Patent_Status_Tracker where  ID=@ID";
                cmd = new SqlCommand(cmdString, con, transaction);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@ID", seedFinal);
                // cmd.Parameters.AddWithValue("@GrantUnit", j.GrantUnit);
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

                cmd = new SqlCommand("InsertPatentReviewTracker", con, transaction);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", seedFinal);
                cmd.Parameters.AddWithValue("@ReviewNo", count + 1);
                cmd.Parameters.AddWithValue("@Entry_status", pat.Filing_Status);
                cmd.Parameters.AddWithValue("@Comment", pat.Remarks);
                cmd.Parameters.AddWithValue("@Created_By", pat.Created_By);
                cmd.Parameters.AddWithValue("@Date", DateTime.Now);
                cmd.ExecuteNonQuery();

                if (resultvalue == 1)
                {
                    cmdString = "select UTN_ID from Dept_M where Institute_Id=@Institute and DeptId=@DeptId";
                    cmd = new SqlCommand(cmdString, con, transaction);
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue("@Institute", inst);
                    cmd.Parameters.AddWithValue("@DeptId", patUTN);


                    SqlDataReader sdr1 = cmd.ExecuteReader();

                    string utn = "";

                    while (sdr1.Read())
                    {
                        if (!Convert.IsDBNull(sdr1["UTN_ID"]))
                        {
                            utn = (string)sdr1["UTN_ID"];
                        }

                    }
                    sdr1.Close();

                    cmdString = "Insert into  Patent_UTN_Id_Gen  (Seed,Year,PT_UTN_ID) VALUES(@value,@Year,@PT_UTN_ID)";
                    cmd = new SqlCommand(cmdString, con, transaction);
                    cmd.CommandType = CommandType.Text;

                    // cmd.Parameters.AddWithValue("@GrantType", j.GrantType);
                    cmd.Parameters.AddWithValue("@value", seed1 + 1);
                    cmd.Parameters.AddWithValue("@Year", year1);
                    cmd.Parameters.AddWithValue("@PT_UTN_ID", utn);
                    cmd.ExecuteNonQuery();
                    int value = seed1 + 1;
                    log.Info("Inserted new value to the ID_Gen table for the year  : " + year1 + "and ID is :" + value);
                }
                else
                {
                    cmdString = "update Patent_UTN_Id_Gen set Seed=@value where PT_UTN_ID in(select UTN_ID from Dept_M where Institute_Id=@Institute and DeptId=@DeptId)";
                    cmd = new SqlCommand(cmdString, con, transaction);
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue("@Institute", inst);
                    cmd.Parameters.AddWithValue("@DeptId", patUTN);

                    //cmd.Parameters.AddWithValue("@GrantType", j.GrantType);
                    cmd.Parameters.AddWithValue("@value", seed1 + 1);
                    cmd.ExecuteNonQuery();
                    int value = seed1 + 1;
                    log.Info("Updated ID_Gen Value with : " + value);

                }
                //cmdString = "Select count(* ) as Count from Patent_App_Status_Tracker where  ID=@ID";
                //cmd = new SqlCommand(cmdString, con, transaction);
                //cmd.CommandType = CommandType.Text;
                //cmd.Parameters.AddWithValue("@ID", seedFinal);
                //// cmd.Parameters.AddWithValue("@GrantUnit", j.GrantUnit);
                //SqlDataReader sdr2 = cmd.ExecuteReader();
                //int count1 = 0;
                //while (sdr2.Read())
                //{
                //    if (!Convert.IsDBNull(sdr2["Count"]))
                //    {
                //        count1 = (int)sdr2["Count"];
                //    }

                //}
                //sdr2.Close();
                //cmd = new SqlCommand("InsertPatentApplicationStage", con, transaction);
                //cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@ID", seedFinal);
                //cmd.Parameters.AddWithValue("@Entry_No", count1 + 1);

                //cmd.Parameters.AddWithValue("@App_Status", pat.App_Status);
                //string date7 = pat.App_Date.ToShortDateString();
                //if (date7 != "01/01/0001")
                //{
                //    cmd.Parameters.AddWithValue("@App_Date", pat.App_Date);

                //}
                //else
                //{
                //    cmd.Parameters.AddWithValue("@App_Date", DBNull.Value);

                //}

                //cmd.Parameters.AddWithValue("@App_Comment", pat.App_Comment);
                //cmd.Parameters.AddWithValue("@Created_By", pat.Created_By);

                //result2 = Convert.ToBoolean(cmd.ExecuteNonQuery());


            }

            catch (Exception ex)
            {
                log.Error("InsertPatentData catch block of patent IDpate: ");
                log.Error(ex.Message);
                log.Error(ex.StackTrace);
                // transaction.Rollback();
                throw ex;
            }

            transaction.Commit();
            return result2;
        }
        catch (Exception ex)
        {
            log.Error("InsertPatentData catch block of patent IDpate: ");
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

    public bool UpdatePatent(Patent pat, GrantData[] jd)
    {
        log.Debug("Inside UpdatePatent function of Patent ID: " + pat.ID);
        int result = 0, result1 = 0;
        con = new SqlConnection(str);
        con.Open();
        transaction = con.BeginTransaction();
        try
        {
            cmd = new SqlCommand("UpdatePatent", con, transaction);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID", pat.ID);
            cmd.Parameters.AddWithValue("@Title", pat.Title);
            cmd.Parameters.AddWithValue("@Description", pat.description);
            cmd.Parameters.AddWithValue("@Entry_Status", pat.Entry_status);

            cmd.Parameters.AddWithValue("@Filing_Status", pat.Filing_Status);
            cmd.Parameters.AddWithValue("@Filing_Office", pat.Filing_Office);
            cmd.Parameters.AddWithValue("@NatureOfPatent", pat.NatureOfPatent);
            cmd.Parameters.AddWithValue("@Funding", pat.Funding);
            cmd.Parameters.AddWithValue("@DetailsColaInstitiuteIndustry", pat.DetailsColaInstitiuteIndustry);
            cmd.Parameters.AddWithValue("@Country", pat.Country);   
            if (pat.RevenueGenerated != 0.0)
            {
                cmd.Parameters.AddWithValue("@RevenueGenerated", pat.RevenueGenerated);
            }
            else
            {
                cmd.Parameters.AddWithValue("@RevenueGenerated", DBNull.Value);
            }
            string date3 = pat.Date_Of_Application.ToShortDateString();
            if (date3 != "01/01/0001")
            {
                cmd.Parameters.AddWithValue("@Date_Of_Application", pat.Date_Of_Application);

            }
            else
            {
                cmd.Parameters.AddWithValue("@Date_Of_Application", DBNull.Value);

            }
            cmd.Parameters.AddWithValue("@Application_Stage", pat.Application_Stage);
            //cmd.Parameters.AddWithValue("@Provisional_Number", pat.Provisional_Number);
            //string date4 = pat.FilingDateprovidedPatent.ToShortDateString();
            //if (date4 != "01/01/0001")
            //{
            //    cmd.Parameters.AddWithValue("@FilingDateprovidedPatent", pat.FilingDateprovidedPatent);
            //}
            //else
            //{
            //    cmd.Parameters.AddWithValue("@FilingDateprovidedPatent", DBNull.Value);
            //}
            cmd.Parameters.AddWithValue("@Patent_Number", pat.Patent_Number);
            cmd.Parameters.AddWithValue("@Application_Number", pat.Application_Number);
            string date6 = pat.Grant_Date.ToShortDateString();
            if (date6 != "01/01/0001")
            {
                cmd.Parameters.AddWithValue("@Grant_Date", pat.Grant_Date);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Grant_Date", DBNull.Value);
            }
            //   cmd.Parameters.AddWithValue("@Renewal_Fee", pat.Renewal_Fee);
            string date5 = pat.LastRenewalFeePaiddate.ToShortDateString();
            if (date5 != "01/01/0001")
            {
                cmd.Parameters.AddWithValue("@LastRenewalFeePaiddate", pat.LastRenewalFeePaiddate);
            }
            else
            {
                cmd.Parameters.AddWithValue("@LastRenewalFeePaiddate", DBNull.Value);
            }
            cmd.Parameters.AddWithValue("@Remarks", pat.Remarks);
            // cmd.Parameters.AddWithValue("@Rejection_Remark", pat.Rejection_Remark);
            cmd.Parameters.Add("@UpdatedBy", SqlDbType.VarChar, 10);
            cmd.Parameters["@UpdatedBy"].Value = HttpContext.Current.Session["UserId"].ToString();

            cmd.Parameters.Add("@UpdatedDate", SqlDbType.DateTime);
            cmd.Parameters["@UpdatedDate"].Value = DateTime.Now;
            if (pat.hasProjectreference != null && pat.hasProjectreference != "")
            {
                cmd.Parameters.AddWithValue("@hasProjectreference", pat.hasProjectreference);
            }
            else
            {
                cmd.Parameters.AddWithValue("@hasProjectreference", DBNull.Value);
            }
            if (pat.ProjectIDlist != null && pat.ProjectIDlist != "")
            {
                cmd.Parameters.AddWithValue("@ProjectIDlist", pat.ProjectIDlist);
            }
            else
            {
                cmd.Parameters.AddWithValue("@ProjectIDlist", DBNull.Value);
            }
            bool result2 = Convert.ToBoolean(cmd.ExecuteNonQuery());


            if (pat.hasProjectreference == "Y")
            {
                if (pat.ProjectIDlist != "" && pat.ProjectIDlist != null)
                {
                    ArrayList ProjectID = new ArrayList();
                    string[] Projectlist = pat.ProjectIDlist.Split(',');
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

            try
            {
                if (result2 == true)
                {
                    cmdString = "delete from  Patent_Inventor  where ID=@ID ";
                    cmd = new SqlCommand(cmdString, con, transaction);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@ID", pat.ID);
                    //  cmd.Parameters.AddWithValue("@GrantUnit", j.GrantUnit);
                    result1 = cmd.ExecuteNonQuery();

                    for (int i = 0; i < jd.Length; i++)
                    {
                        cmd = new SqlCommand("InsertPatentInventor", con, transaction);
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ID", pat.ID);
                        // cmd.Parameters.AddWithValue("@ProjectUnit", j.GrantUnit);
                        cmd.Parameters.AddWithValue("@GrantLine", i + 1);

                        cmd.Parameters.AddWithValue("@AuthorName", jd[i].AuthorName);
                        cmd.Parameters.AddWithValue("@MUNonMU", jd[i].MUNonMU);
                        cmd.Parameters.AddWithValue("@EmployeeCode", jd[i].EmployeeCode);

                        cmd.Parameters.AddWithValue("@Institution", jd[i].Institution);
                        cmd.Parameters.AddWithValue("@Department", jd[i].Department);

                        cmd.Parameters.AddWithValue("@InstitutionName", jd[i].InstitutionName);
                        cmd.Parameters.AddWithValue("@DepartmentName", jd[i].DepartmentName);
                        //   cmd.Parameters.AddWithValue("@AuthorType", jd[i].AuthorType);

                        cmd.Parameters.AddWithValue("@NationalInternational", jd[i].NationalInternationl);
                        cmd.Parameters.AddWithValue("@Continent", jd[i].continental);
                        cmd.Parameters.AddWithValue("@EmailId", jd[i].EmailId);
                        cmd.ExecuteNonQuery();

                        // log.Info("Grant investigator details inserted sucessfully  of Project Unit: " + j.GrantUnit + "ID: " + j.GID);
                    }


                } cmdString = "update Patent_Data set Filing_Status=@Filing_Status,Entry_status=@Entry_status, Grant_Date=@Grant_Date, LastRenewalFeePaiddate=@LastRenewalFeePaiddate,NextRenewalDate=@NextRenewalDate, IncentivePointStatus=@IncentivePointStatus where ID=@ID";
                cmd = new SqlCommand(cmdString, con, transaction);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@ID", pat.ID);
                cmd.Parameters.AddWithValue("@Filing_Status", pat.Filing_Status);
                cmd.Parameters.AddWithValue("@Entry_status", pat.Entry_status);


                string date17 = pat.Grant_Date.ToShortDateString();
                if (date17 != "01/01/0001")
                {
                    cmd.Parameters.AddWithValue("@Grant_Date", pat.Grant_Date);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Grant_Date", DBNull.Value);
                }
                //  cmd.Parameters.AddWithValue("@Renewal_Fee", pat.Renewal_Fee);
                string date18 = pat.LastRenewalFeePaiddate.ToShortDateString();
                if (date18 != "01/01/0001")
                {
                    cmd.Parameters.AddWithValue("@LastRenewalFeePaiddate", pat.LastRenewalFeePaiddate);

                }
                else
                {
                    cmd.Parameters.AddWithValue("@LastRenewalFeePaiddate", DBNull.Value);

                }

                string date19 = pat.NextRenewalDate.ToShortDateString();
                if (date19 != "01/01/0001")
                {
                    cmd.Parameters.AddWithValue("@NextRenewalDate", pat.NextRenewalDate);

                }
                else
                {
                    cmd.Parameters.AddWithValue("@NextRenewalDate", DBNull.Value);

                }
                cmd.Parameters.AddWithValue("@IncentivePointStatus", "PEN");
                //if (pat.IncentivePointStatus != null)
                //{
                //    cmd.Parameters.AddWithValue("@IncentivePointStatus", pat.IncentivePointStatus);

                //}
                //else
                //{
                //    cmd.Parameters.AddWithValue("@IncentivePointStatus", DBNull.Value);

                //}
                //cmd.Parameters.AddWithValue("@RejectedDate", DateTime.Now);
                //cmd.Parameters.AddWithValue("@RejectedBy", pat.RejectedBy);
                cmd.ExecuteNonQuery();
                log.Info("Status  of ID: " + pat.ID);

                cmdString = "Select count(* ) as Count from Patent_Status_Tracker where  ID=@ID";
                cmd = new SqlCommand(cmdString, con, transaction);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@ID", pat.ID);
                // cmd.Parameters.AddWithValue("@GrantUnit", j.GrantUnit);
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

                cmd = new SqlCommand("InsertPatentReviewTracker", con, transaction);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", pat.ID);
                cmd.Parameters.AddWithValue("@ReviewNo", count + 1);
                cmd.Parameters.AddWithValue("@Entry_status", pat.Filing_Status);
                cmd.Parameters.AddWithValue("@Comment", pat.Remarks);
                cmd.Parameters.AddWithValue("@Created_By", pat.Created_By);
                cmd.Parameters.AddWithValue("@Date", DateTime.Now);
                cmd.ExecuteNonQuery();


                //cmdString = "Select count(* ) as Count from Patent_Status_Tracker where  ID=@ID";
                //cmd = new SqlCommand(cmdString, con, transaction);
                //cmd.CommandType = CommandType.Text;
                //cmd.Parameters.AddWithValue("@ID", pat.ID);
                //// cmd.Parameters.AddWithValue("@GrantUnit", j.GrantUnit);
                //SqlDataReader sdr = cmd.ExecuteReader();
                //int count = 0;
                //while (sdr.Read())
                //{
                //    if (!Convert.IsDBNull(sdr["Count"]))
                //    {
                //        count = (int)sdr["Count"];
                //    }

                //}
                //sdr.Close();

                //cmd = new SqlCommand("InsertPatentReviewTracker", con, transaction);
                //cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@ID", pat.ID);
                //cmd.Parameters.AddWithValue("@ReviewNo", count + 1);
                //cmd.Parameters.AddWithValue("@Entry_status", pat.Filing_Status);
                //cmd.Parameters.AddWithValue("@Comment", pat.Remarks);
                //cmd.Parameters.AddWithValue("@Created_By", pat.Created_By);
                //cmd.Parameters.AddWithValue("@Date", DateTime.Now);
                //cmd.ExecuteNonQuery();
                //if (pat.Filing_Status == "GRN")
                //{
                //    cmdString = "Select count(* ) as Count from Patent_Renewal_Tracker where  ID=@ID";
                //    cmd = new SqlCommand(cmdString, con, transaction);
                //    cmd.CommandType = CommandType.Text;
                //    cmd.Parameters.AddWithValue("@ID", pat.ID);
                //    // cmd.Parameters.AddWithValue("@GrantUnit", j.GrantUnit);
                //    SqlDataReader sdr2 = cmd.ExecuteReader();
                //    int count1 = 0;
                //    while (sdr2.Read())
                //    {
                //        if (!Convert.IsDBNull(sdr2["Count"]))
                //        {
                //            count1 = (int)sdr2["Count"];
                //        }

                //    }
                //    sdr2.Close();
                //    cmd = new SqlCommand("InsertPatentRenewal", con, transaction);
                //    cmd.CommandType = CommandType.StoredProcedure;
                //    cmd.Parameters.AddWithValue("@ID", pat.ID);
                //    cmd.Parameters.AddWithValue("@Entry_No", count1 + 1);

                //    string date7 = pat.LastRenewalFeePaiddate.ToShortDateString();
                //    if (date7 != "01/01/0001")
                //    {
                //        cmd.Parameters.AddWithValue("@LastRenewal_Date", pat.LastRenewalFeePaiddate);

                //    }
                //    else
                //    {
                //        cmd.Parameters.AddWithValue("@LastRenewal_Date", DBNull.Value);

                //    }
                //    cmd.Parameters.AddWithValue("@RenewalAmount", pat.Renewal_Fee);
                //cmd.Parameters.AddWithValue("@Entry_Status", pat.Filing_Status);
                //cmd.Parameters.AddWithValue("@RenewalComment", pat.Renewal_Comment);
                //    cmd.Parameters.AddWithValue("@Created_By", pat.Created_By);
                //    cmd.Parameters.AddWithValue("@Created_Date", pat.Created_Date);

                //    result2 = Convert.ToBoolean(cmd.ExecuteNonQuery());


                //}
            }
            catch (Exception ex)
            {
                log.Error("InsertPatentData catch block of patent IDpate: ");
                log.Error(ex.Message);
                log.Error(ex.StackTrace);
                // transaction.Rollback();
                throw ex;
            }


            log.Debug("UpdatePatent: ");
            transaction.Commit();
            return result2;
        }

        catch (Exception ex)
        {
            log.Error("UpdatePatent catch block of patent IDpate: ");
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
    public Patent SelectPatent(string ID)
    {
        log.Debug("Inside fnfindGrantid function, Project ID: " + ID);
        try
        {
            cmdString = " select * from Patent_Data where ID=@ID ";
            con = new SqlConnection(str);
            con.Open();
            cmd = new SqlCommand(cmdString, con);
            cmd.Parameters.Add("@ID", SqlDbType.VarChar, 10);
            cmd.Parameters["@ID"].Value = ID;

            cmd.CommandType = CommandType.Text;
            SqlDataReader sdr = cmd.ExecuteReader();

            Patent V = new Patent();

            while (sdr.Read())
            {
                if (!Convert.IsDBNull(sdr["ID"]))
                {
                    V.ID = (string)sdr["ID"];
                }
                if (!Convert.IsDBNull(sdr["Pat_UTN"]))
                {
                    V.Pat_UTN = (string)sdr["Pat_UTN"];
                }
                if (!Convert.IsDBNull(sdr["Title"]))
                {
                    V.Title = (string)sdr["Title"];
                }
                if (!Convert.IsDBNull(sdr["Description"]))
                {
                    V.description = (string)sdr["Description"];
                }

                if (!Convert.IsDBNull(sdr["DetailsColaInstitiuteIndustry"]))
                {
                    V.DetailsColaInstitiuteIndustry = (string)sdr["DetailsColaInstitiuteIndustry"];
                }
                if (!Convert.IsDBNull(sdr["Country"]))
                {
                    V.Country = (string)sdr["Country"];
                }              
                if (!Convert.IsDBNull(sdr["RevenueGenerated"]))
                {
                    V.RevenueGenerated = Convert.ToDouble((decimal)sdr["RevenueGenerated"]);
                }
                else if (Convert.IsDBNull(sdr["RevenueGenerated"]))
                {
                    V.RevenueGenerated = 0;
                }
                if (!Convert.IsDBNull(sdr["Filing_Status"]))
                {
                    V.Filing_Status = (string)sdr["Filing_Status"];
                }
                if (!Convert.IsDBNull(sdr["Entry_Status"]))
                {
                    V.Entry_status = (string)sdr["Entry_Status"];
                }
                if (!Convert.IsDBNull(sdr["Filing_Office"]))
                {
                    V.Filing_Office = (string)sdr["Filing_Office"];

                }
                if (!Convert.IsDBNull(sdr["NatureOfPatent"]))
                {
                    V.NatureOfPatent = (byte)sdr["NatureOfPatent"];
                }

                if (!Convert.IsDBNull(sdr["Entry_Status"]))
                {
                    V.Entry_status = (string)sdr["Entry_Status"];
                }

                if (!Convert.IsDBNull(sdr["Funding"]))
                {
                    V.Funding = (byte)sdr["Funding"];
                }
                if (!Convert.IsDBNull(sdr["Date_Of_Application"]))
                {
                    V.Date_Of_Application = (DateTime)sdr["Date_Of_Application"];
                }
                if (!Convert.IsDBNull(sdr["Application_Stage"]))
                {
                    V.Application_Stage = (string)sdr["Application_Stage"];

                }



                //if (!Convert.IsDBNull(sdr["Provisional_Number"]))
                //{
                //    V.Provisional_Number = (string)sdr["Provisional_Number"];
                //}
                //if (!Convert.IsDBNull(sdr["FilingDateprovidedPatent"]))
                //{
                //    V.FilingDateprovidedPatent = (DateTime)sdr["FilingDateprovidedPatent"];
                //}
                if (!Convert.IsDBNull(sdr["Patent_Number"]))
                {
                    V.Patent_Number = (string)sdr["Patent_Number"];
                }

                if (!Convert.IsDBNull(sdr["Application_Number"]))
                {
                    V.Application_Number = (string)sdr["Application_Number"];
                }
                if (!Convert.IsDBNull(sdr["Grant_Date"]))
                {
                    V.Grant_Date = (DateTime)sdr["Grant_Date"];
                }
                //if (!Convert.IsDBNull(sdr["Renewal_Fee"]))
                //{
                //    V.Renewal_Fee = (string)sdr["Renewal_Fee"];

                //}
                if (!Convert.IsDBNull(sdr["LastRenewalFeePaiddate"]))
                {
                    V.LastRenewalFeePaiddate = (DateTime)sdr["LastRenewalFeePaiddate"];
                }
                if (!Convert.IsDBNull(sdr["Remarks"]))
                {
                    V.Remarks = (string)sdr["Remarks"];
                }
                if (!Convert.IsDBNull(sdr["Rejection_Remark"]))
                {
                    V.Rejection_Remark = (string)sdr["Rejection_Remark"];
                }



                if (!Convert.IsDBNull(sdr["CancelRemarks"]))
                {
                    V.CancelRemarks = (string)sdr["CancelRemarks"];
                }

                if (!Convert.IsDBNull(sdr["CreatedDate"]))
                {
                    V.Created_Date = (DateTime)sdr["CreatedDate"];
                }
                if (!Convert.IsDBNull(sdr["CreatedBy"]))
                {
                    V.Created_By = (string)sdr["CreatedBy"];
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
            log.Error("Inside fnfindGrantid catch block of Project ID: " + ID);
            log.Error("Erroe Message : " + ex.Message);
            log.Error(ex.StackTrace);
            throw ex;
        }

        finally
        {
            con.Close();
        }
    }
    public DataTable fnPatentInventorDetails(string ID)
    {
        //  log.Debug("Inside fnfindGrantInvestigatorDetails function);

        con = new SqlConnection(str);
        con.Open();
        try
        {
            SqlDataAdapter da;
            DataTable ds;
            cmdString = "select MUNonMU  as DropdownMuNonMu,EmployeeCode,InvestigatorName as AuthorName,Institution,Department, DepartmentName,InstitutionName ,  EmailId as MailId,NationalInternational as NationalType, Continent from Patent_Inventor where ID=@ID";
            cmd = new SqlCommand(cmdString, con);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add("@ID", SqlDbType.VarChar, 10);
            cmd.Parameters["@ID"].Value = ID;
            da = new SqlDataAdapter(cmd);

            ds = new DataTable();
            da.Fill(ds);

            return ds;
        }

        catch (Exception ex)
        {
            // log.Error("Inside fnfindGrantInvestigatorDetails catch block ProjectUnit: " + bu + "ID: " + jid);
            log.Error(ex.Message);
            log.Error(ex.StackTrace);
            throw ex;
        }

        finally
        {
            con.Close();
        }
    }

    public bool UpdateStatusPatentRejectApproval(Patent pat, GrantData[] JD)
    {
        log.Debug("Inside UpdateStatusPatentRejectApproval of ID: " + pat.ID);
        bool result3 = false;
        con = new SqlConnection(str);
        con.Open();
        transaction = con.BeginTransaction();
        try
        {
            if (pat.Filing_Status == "REJ")
            {
                cmdString = "update Patent_Data set Filing_Status=@Filing_Status,Entry_status=@Entry_status, Rejection_Remark=@Rejection_Remark,RejectedDate=@RejectedDate,RejectedBy=@RejectedBy where ID=@ID";

            }
            else if (pat.Filing_Status == "WDN")
            {
                cmdString = "update Patent_Data set Filing_Status=@Filing_Status,Entry_status=@Entry_status, WithdrawnRemarks=@Rejection_Remark,WithdrawnDate=@RejectedDate,WithdrawnBy=@RejectedBy where ID=@ID";

            }
            cmd = new SqlCommand(cmdString, con, transaction);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@ID", pat.ID);
            cmd.Parameters.AddWithValue("@Filing_Status", pat.Filing_Status);
            cmd.Parameters.AddWithValue("@Entry_status", pat.Entry_status);
            cmd.Parameters.AddWithValue("@Rejection_Remark", pat.Rejection_Remark);
            cmd.Parameters.AddWithValue("@RejectedDate", DateTime.Now);
            cmd.Parameters.AddWithValue("@RejectedBy", pat.RejectedBy);
            result3 = Convert.ToBoolean(cmd.ExecuteNonQuery());
            log.Info("Status  of ID: " + pat.ID);

            cmdString = "Select count(* ) as Count from Patent_Status_Tracker where  ID=@ID";
            cmd = new SqlCommand(cmdString, con, transaction);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("@ID", pat.ID);
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

            cmd = new SqlCommand("InsertPatentReviewTracker", con, transaction);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID", pat.ID);
            cmd.Parameters.AddWithValue("@ReviewNo", count + 1);
            cmd.Parameters.AddWithValue("@Entry_status", pat.Filing_Status);
            if (pat.Filing_Status == "REJ" || pat.Filing_Status == "WDN")
            {
                cmd.Parameters.AddWithValue("@Comment", pat.Rejection_Remark);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Comment", pat.Remarks);
            }
            cmd.Parameters.AddWithValue("@Created_By", pat.Created_By);
            cmd.Parameters.AddWithValue("@Date", DateTime.Now);
            result3 = Convert.ToBoolean(cmd.ExecuteNonQuery());
            transaction.Commit();
            return result3;
        }

        catch (Exception ex)
        {
            log.Error("UpdateStatusGrantEntryRejectApproval catch block of ID: " + pat.ID);
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

    public bool InsertApplicationStage(Patent pat)
    {
        log.Debug("Inside InsertApplicationStage of ID: " + pat.ID);
        bool result3 = false;
        con = new SqlConnection(str);
        con.Open();
        transaction = con.BeginTransaction();
        try
        {
            cmdString = "Select count(* ) as Count from Patent_App_Status_Tracker where  ID=@ID";
            cmd = new SqlCommand(cmdString, con, transaction);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@ID", pat.ID);
            // cmd.Parameters.AddWithValue("@GrantUnit", j.GrantUnit);
            SqlDataReader sdr2 = cmd.ExecuteReader();
            int count1 = 0;
            while (sdr2.Read())
            {
                if (!Convert.IsDBNull(sdr2["Count"]))
                {
                    count1 = (int)sdr2["Count"];
                }

            }
            sdr2.Close();
            cmd = new SqlCommand("InsertPatentApplicationStage", con, transaction);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID", pat.ID);
            cmd.Parameters.AddWithValue("@Entry_No", count1 + 1);

            cmd.Parameters.AddWithValue("@App_Status", pat.App_Status);
            string date7 = pat.App_Date.ToShortDateString();
            if (date7 != "01/01/0001")
            {
                cmd.Parameters.AddWithValue("@App_Date", pat.App_Date);

            }
            else
            {
                cmd.Parameters.AddWithValue("@App_Date", DBNull.Value);

            }

            cmd.Parameters.AddWithValue("@App_Comment", pat.App_Comment);
            cmd.Parameters.AddWithValue("@Created_By", pat.Created_By);

            result3 = Convert.ToBoolean(cmd.ExecuteNonQuery());

            cmd = new SqlCommand("Update Patent_Data set Application_Stage=@App_Status where ID=@ID", con, transaction);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@ID", pat.ID);
            cmd.Parameters.AddWithValue("@App_Status", pat.App_Status);
            result3 = Convert.ToBoolean(cmd.ExecuteNonQuery());
            transaction.Commit();
            return result3;
        }

        catch (Exception ex)
        {
            log.Error("InsertApplicationStage catch block of ID: " + pat.ID);
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

    public bool UpdateGrantPatent(Patent pat, GrantData[] JD)
    {
        log.Debug("Inside UpdateGrantPatent of ID: " + pat.ID);
        bool result3 = false;
        con = new SqlConnection(str);
        con.Open();
        transaction = con.BeginTransaction();
        try
        {

            cmdString = "update Patent_Data set Filing_Status=@Filing_Status,Entry_status=@Entry_status, Grant_Date=@Grant_Date, LastRenewalFeePaiddate=@LastRenewalFeePaiddate,NextRenewalDate=@NextRenewalDate, IncentivePointStatus=@IncentivePointStatus where ID=@ID";
            cmd = new SqlCommand(cmdString, con, transaction);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@ID", pat.ID);
            cmd.Parameters.AddWithValue("@Filing_Status", pat.Filing_Status);
            cmd.Parameters.AddWithValue("@Entry_status", pat.Entry_status);
            string date17 = pat.Grant_Date.ToShortDateString();
            if (date17 != "01/01/0001")
            {
                cmd.Parameters.AddWithValue("@Grant_Date", pat.Grant_Date);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Grant_Date", DBNull.Value);
            }
            //  cmd.Parameters.AddWithValue("@Renewal_Fee", pat.Renewal_Fee);
            string date18 = pat.LastRenewalFeePaiddate.ToShortDateString();
            if (date18 != "01/01/0001")
            {
                cmd.Parameters.AddWithValue("@LastRenewalFeePaiddate", pat.LastRenewalFeePaiddate);

            }
            else
            {
                cmd.Parameters.AddWithValue("@LastRenewalFeePaiddate", DBNull.Value);

            }

            string date19 = pat.NextRenewalDate.ToShortDateString();
            if (date19 != "01/01/0001")
            {
                cmd.Parameters.AddWithValue("@NextRenewalDate", pat.NextRenewalDate);

            }
            else
            {
                cmd.Parameters.AddWithValue("@NextRenewalDate", DBNull.Value);

            }
            if (pat.IncentivePointStatus != null)
            {
                cmd.Parameters.AddWithValue("@IncentivePointStatus", pat.IncentivePointStatus);

            }
            else
            {
                cmd.Parameters.AddWithValue("@IncentivePointStatus", DBNull.Value);

            }
            //cmd.Parameters.AddWithValue("@RejectedDate", DateTime.Now);
            //cmd.Parameters.AddWithValue("@RejectedBy", pat.RejectedBy);
            result3 = Convert.ToBoolean(cmd.ExecuteNonQuery());
            log.Info("Status  of ID: " + pat.ID);

            cmdString = "Select count(* ) as Count from Patent_Status_Tracker where  ID=@ID";
            cmd = new SqlCommand(cmdString, con, transaction);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@ID", pat.ID);
            // cmd.Parameters.AddWithValue("@GrantUnit", j.GrantUnit);
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

            cmd = new SqlCommand("InsertPatentReviewTracker", con, transaction);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID", pat.ID);
            cmd.Parameters.AddWithValue("@ReviewNo", count + 1);
            cmd.Parameters.AddWithValue("@Entry_status", pat.Filing_Status);
            cmd.Parameters.AddWithValue("@Comment", pat.Remarks);
            cmd.Parameters.AddWithValue("@Created_By", pat.Created_By);
            cmd.Parameters.AddWithValue("@Date", DateTime.Now);
            cmd.ExecuteNonQuery();

            //    cmdString = "Select count(* ) as Count from Patent_Renewal_Tracker where  ID=@ID";
            //    cmd = new SqlCommand(cmdString, con, transaction);
            //    cmd.CommandType = CommandType.Text;
            //    cmd.Parameters.AddWithValue("@ID", pat.ID);
            //    // cmd.Parameters.AddWithValue("@GrantUnit", j.GrantUnit);
            //    SqlDataReader sdr2 = cmd.ExecuteReader();
            //    int count1 = 0;
            //    while (sdr2.Read())
            //    {
            //        if (!Convert.IsDBNull(sdr2["Count"]))
            //        {
            //            count1 = (int)sdr2["Count"];
            //        }

            //    }
            //    sdr2.Close();
            //    cmd = new SqlCommand("InsertPatentRenewal", con, transaction);
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    cmd.Parameters.AddWithValue("@ID", pat.ID);
            //    cmd.Parameters.AddWithValue("@Entry_No", count1 + 1);

            //    string date7 = pat.LastRenewalFeePaiddate.ToShortDateString();
            //    if (date7 != "01/01/0001")
            //    {
            //        cmd.Parameters.AddWithValue("@LastRenewal_Date", pat.LastRenewalFeePaiddate);

            //    }
            //    else
            //    {
            //        cmd.Parameters.AddWithValue("@LastRenewal_Date", DBNull.Value);

            //    }
            //    cmd.Parameters.AddWithValue("@RenewalAmount", pat.Renewal_Fee);
            //cmd.Parameters.AddWithValue("@Entry_Status", pat.Filing_Status);
            //cmd.Parameters.AddWithValue("@RenewalComment", pat.Renewal_Comment);
            //    cmd.Parameters.AddWithValue("@Created_By", pat.Created_By);
            //    cmd.Parameters.AddWithValue("@Created_Date", pat.Created_Date);

            //    result3 = Convert.ToBoolean(cmd.ExecuteNonQuery());
            transaction.Commit();
            return result3;

        }
        catch (Exception ex)
        {
            log.Error("UpdateGrantPatent catch block of ID: " + pat.ID);
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

    public bool InsertRenwalaDetails(Patent pat)
    {
        log.Debug("Inside InsertRenwalaDetails of ID: " + pat.ID);
        bool result3 = false;
        int result = 0;
        con = new SqlConnection(str);
        con.Open();
        transaction = con.BeginTransaction();
        try
        {
            //cmdString = "Select count(* ) as Count from Patent_Renewal_Tracker where  ID=@ID";
            //cmd = new SqlCommand(cmdString, con, transaction);
            //cmd.CommandType = CommandType.Text;
            //cmd.Parameters.AddWithValue("@ID", pat.ID);
            //// cmd.Parameters.AddWithValue("@GrantUnit", j.GrantUnit);
            //SqlDataReader sdr2 = cmd.ExecuteReader();
            //int count1 = 0;
            //while (sdr2.Read())
            //{
            //    if (!Convert.IsDBNull(sdr2["Count"]))
            //    {
            //        count1 = (int)sdr2["Count"];
            //    }

            //}
            //sdr2.Close();
            cmd = new SqlCommand("InsertPatentRenewal", con, transaction);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID", pat.ID);
            //cmd.Parameters.AddWithValue("@Entry_No", count1 + 1);
            cmd.Parameters.AddWithValue("@NextRenewalYear", pat.NextRenewalYear);

            string date7 = pat.LastRenewalFeePaiddate.ToShortDateString();
            if (date7 != "01/01/0001")
            {
                cmd.Parameters.AddWithValue("@LastRenewal_Date", pat.LastRenewalFeePaiddate);

            }
            else
            {
                cmd.Parameters.AddWithValue("@LastRenewal_Date", DBNull.Value);

            }
            //cmd.Parameters.AddWithValue("@NextRenewalDate", pat.NextRenewalDate);
            cmd.Parameters.AddWithValue("@RenewalAmount", pat.Renewal_Fee);
            //  cmd.Parameters.AddWithValue("@Entry_status", pat.Filing_Status);
            cmd.Parameters.AddWithValue("@RenewalComment", pat.Renewal_Comment);
            cmd.Parameters.AddWithValue("@Created_By", pat.Created_By);
            cmd.Parameters.AddWithValue("@Created_Date", DateTime.Now);
            result3 = Convert.ToBoolean(cmd.ExecuteNonQuery());

            cmdString = "update Patent_Data set NextRenewalDate=@NextRenewalDate,LastRenewalFeePaiddate=@LastRenewalFeePaiddate,Filing_Status=@Status where ID=@ID ";

            cmd = new SqlCommand(cmdString, con, transaction);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("@ID", pat.ID);
            cmd.Parameters.AddWithValue("@Status", pat.Filing_Status);
            cmd.Parameters.AddWithValue("@NextRenewalDate", pat.NextRenewalDate);
            cmd.Parameters.AddWithValue("@LastRenewalFeePaiddate", pat.LastRenewalFeePaiddate);
            result = cmd.ExecuteNonQuery();

            transaction.Commit();
            return result3;
        }

        catch (Exception ex)
        {
            log.Error("InsertApplicationStage catch block of ID: " + pat.ID);
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

    public Patent getID(string PatentID)
    {
        log.Debug("Inside the getID function");
        // string TypeOfTest = null;
        Patent vd = new Patent();
        SqlDataReader sdr = null;
        try
        {
            con.Open();
            string query = "select CreatedBy, CreatedDate from Patent_Data where ID=@ID";
            cmd = new SqlCommand(query, con);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add("@ID", SqlDbType.VarChar, 12);
            cmd.Parameters["@ID"].Value = PatentID;

            sdr = cmd.ExecuteReader();

            while (sdr.Read())
            {
                vd.Created_By = (string)sdr["Created_By"];
                vd.Created_Date = (DateTime)sdr["Created_Date"];

            }
            return vd;
        }

        catch (Exception e)
        {
            log.Error("Inside getID function");
            log.Error(e.Message);
            log.Error(e.StackTrace);
            return null;
        }
        finally
        {
            sdr.Close();
            con.Close();
        }
    }

    //public Patent get_Inventor_Details(string PatentID)
    //{
    //    //log.Debug("Inside the get_Inventor_Details function");
    //// string TypeOfTest = null;
    //Patent vd = new Patent();
    //SqlDataReader sdr = null;
    //try
    //{
    //    con.Open();
    //    string query = "select InvestigatorName,DepartmentName, EmailId, InstitutionName from Patent_Inventor where ID=@ID";
    //    cmd = new SqlCommand(query, con);
    //    cmd.CommandType = CommandType.Text;

    //    cmd.Parameters.Add("@ID", SqlDbType.VarChar, 12);
    //    cmd.Parameters["@ID"].Value = PatentID;

    //    sdr = cmd.ExecuteReader();

    //    while (sdr.Read())
    //    {
    //        vd.InvestigatorName = (string)sdr["InvestigatorName"];

    //        vd.DepartmentName = (string)sdr["DepartmentName"];
    //        vd.EmailId = (string)sdr["EmailId"];
    //        vd.InstitutionName = (string)sdr["InstitutionName"];

    //    }
    //    return vd;
    //}

    //catch (Exception e)
    //{
    //    log.Error("Inside get_Inventor_Details function");
    //    log.Error(e.Message);
    //    log.Error(e.StackTrace);
    //    return null;
    //}
    //finally
    //{
    //    sdr.Close();
    //    con.Close();
    //}
    // }

    public int UpdatePatentCancelStatus(Patent j)
    {
        log.Debug("Inside UpdatePatentCancelStatus function of  Patent Id: " + j.ID);
        int result = 0;
        con = new SqlConnection(str);
        con.Open();
        transaction = con.BeginTransaction();
        try
        {

            cmdString = "update Patent_Data set Filing_Status=@Filing_Status,Entry_status=@Entry_status, CancelRemarks=@CancelRemarks,cancelledDate=@cancelledDate,CancelledBy=@CancelledBy where ID=@ID ";

            cmd = new SqlCommand(cmdString, con, transaction);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("@ID", j.ID);

            cmd.Parameters.AddWithValue("@Filing_Status", j.Filing_Status);
            cmd.Parameters.AddWithValue("@Entry_status", j.Entry_status);
            cmd.Parameters.AddWithValue("@CancelRemarks", j.CancelRemarks);

            cmd.Parameters.AddWithValue("@cancelledDate", DateTime.Now);
            cmd.Parameters.AddWithValue("@CancelledBy", j.CancelledBy);

            result = cmd.ExecuteNonQuery();
            cmdString = "Select count(* ) as Count from Patent_Status_Tracker where  ID=@ID";
            cmd = new SqlCommand(cmdString, con, transaction);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@ID", j.ID);
            // cmd.Parameters.AddWithValue("@GrantUnit", j.GrantUnit);
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

            cmd = new SqlCommand("InsertPatentReviewTracker", con, transaction);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID", j.ID);
            cmd.Parameters.AddWithValue("@ReviewNo", count + 1);
            cmd.Parameters.AddWithValue("@Entry_status", j.Entry_status);
            cmd.Parameters.AddWithValue("@Comment", j.CancelRemarks);
            cmd.Parameters.AddWithValue("@Created_By", j.CancelledBy);
            cmd.Parameters.AddWithValue("@Date", DateTime.Now);
            result = cmd.ExecuteNonQuery();
            transaction.Commit();
            return result;

        }

        catch (Exception ex)
        {
            log.Error("UpdatePatentCancelStatus catch block  of Project Id: " + j.ID);
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

    public Patent PatentInvetor(string PatentID)
    {
        log.Debug("inside function author count");
        try
        {
            //cmdString = "select o.BoX_ID,w.PatientName,w.F_G_SName, TreatmentStartDate,o.Status, TreatmentEndDate,PatientComplianceVisits from OD_Patient_Data o ,Waiting_List w where w.WLID=o.WLID and BoX_ID=@PatientID";
            //  con = new SqlConnection(str);
            con.Open();
            cmd = new SqlCommand("InventorCount", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ID", SqlDbType.VarChar, 12);
            cmd.Parameters["@ID"].Value = PatentID;
            SqlDataReader reader = cmd.ExecuteReader();
            // PatientPOD p = new PatientPOD();
            Patent p = new Patent();
            while (reader.Read())
            {

                p.AuthorCount = (int)reader["ID"];

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

    public DataSet fnfindPatentAccount12(string PatentID)
    {
        con = new SqlConnection(str);
        con.Open();
        try
        {
            //  cmdString = "select  convert(numeric(13,2),Amount) as Amount ,DebitCredit as DrCr, a.account as account, a.oprunit as oprUnit,a.DeptID as dept,a.AffilateBU as affiliate,openitem as openItem, a.linenarration as lineNar, ADesc as accountName, OprUnit_M.OUnitName as oprUnitName,Department_M.DeptName,OpenItem_M.EmpName,a.JournalLine,EmpName as openItemName from GL_Accounting_T a  LEFT OUTER JOIN OprUnit_M ON a.OPRUNIT = OprUnit_M.OPRUNIT LEFT OUTER JOIN Department_M ON a.DeptID = Department_M.DeptCode LEFT OUTER JOIN OpenItem_M ON a.openitem = OpenItem_M.EmpCode, GL d , Account_M where a.JournalID=d.JournalID  and Account_M.Account= a.Account and a.BusinessUnit=d.BusinessUnit and a.JournalID=@JournalID and a.BusinessUnit=@BusinessUnit order by a.JournalLine";
            SqlDataAdapter da;
            DataSet ds;
            cmdString = "select InvestigatorName, DepartmentName, EmployeeCode from Patent_Inventor where ID=@ID";
            cmd = new SqlCommand(cmdString, con);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add("@ID", SqlDbType.VarChar, 10);
            cmd.Parameters["@ID"].Value = PatentID;

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

    public Patent fnfindInventor(string PatentID)
    {
        con = new SqlConnection(str);
        con.Open();
        try
        {
            //  cmdString = "select  convert(numeric(13,2),Amount) as Amount ,DebitCredit as DrCr, a.account as account, a.oprunit as oprUnit,a.DeptID as dept,a.AffilateBU as affiliate,openitem as openItem, a.linenarration as lineNar, ADesc as accountName, OprUnit_M.OUnitName as oprUnitName,Department_M.DeptName,OpenItem_M.EmpName,a.JournalLine,EmpName as openItemName from GL_Accounting_T a  LEFT OUTER JOIN OprUnit_M ON a.OPRUNIT = OprUnit_M.OPRUNIT LEFT OUTER JOIN Department_M ON a.DeptID = Department_M.DeptCode LEFT OUTER JOIN OpenItem_M ON a.openitem = OpenItem_M.EmpCode, GL d , Account_M where a.JournalID=d.JournalID  and Account_M.Account= a.Account and a.BusinessUnit=d.BusinessUnit and a.JournalID=@JournalID and a.BusinessUnit=@BusinessUnit order by a.JournalLine";

            string title;
            cmdString = "select Title from Patent_Data where ID=@ID";
            cmd = new SqlCommand(cmdString, con);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add("@ID", SqlDbType.VarChar, 12);
            cmd.Parameters["@ID"].Value = PatentID;
            SqlDataReader reader = cmd.ExecuteReader();

            Patent p = new Patent();
            while (reader.Read())
            {

                p.Title = (string)reader["Title"];

            }
            return p;
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

    public Patent Get_CreatedBy(string PatentID)
    {

        log.Debug("inside function Get_CreatedBy");
        try
        {
            //cmdString = "select o.BoX_ID,w.PatientName,w.F_G_SName, TreatmentStartDate,o.Status, TreatmentEndDate,PatientComplianceVisits from OD_Patient_Data o ,Waiting_List w where w.WLID=o.WLID and BoX_ID=@PatientID";
            //  con = new SqlConnection(str);
            con.Open();
            cmd = new SqlCommand("Get_Createdby_Data", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@id", SqlDbType.VarChar, 12);
            cmd.Parameters["@id"].Value = PatentID;
            SqlDataReader reader = cmd.ExecuteReader();
            // PatientPOD p = new PatientPOD();,,,
            Patent p = new Patent();
            while (reader.Read())
            {
                p.Created_By = (string)reader["CreatedBy"];

            }
            return p;
        }
        catch (Exception e)
        {
            log.Debug("Error: Inside catch block of Get_CreatedBy");
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



    public Patent Get_CreatedName(string PatentID1)
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
            cmd.Parameters["@id"].Value = PatentID1;
            SqlDataReader reader = cmd.ExecuteReader();
            // PatientPOD p = new PatientPOD();,,,
            Patent p = new Patent();
            while (reader.Read())
            {


                p.CreatedName = (string)reader["name"];



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

    public Patent getPatent_Author_Details(string PatentID1)
    {
        log.Debug("inside function Patent_Author_Details");
        try
        {
            //cmdString = "select o.BoX_ID,w.PatientName,w.F_G_SName, TreatmentStartDate,o.Status, TreatmentEndDate,PatientComplianceVisits from OD_Patient_Data o ,Waiting_List w where w.WLID=o.WLID and BoX_ID=@PatientID";
            //  con = new SqlConnection(str);
            con.Open();
            cmd = new SqlCommand("getPatent_Author_Details", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@id", SqlDbType.VarChar, 12);
            cmd.Parameters["@id"].Value = PatentID1;
            SqlDataReader reader = cmd.ExecuteReader();
            // PatientPOD p = new PatientPOD();,,,
            Patent p = new Patent();
            while (reader.Read())
            {


                p.dep_name = (string)reader["DepartmentName"];
                p.emailid = (string)reader["EmailId"];
                p.institution_name = (string)reader["InstitutionName"];



            }
            return p;
        }
        catch (Exception e)
        {
            log.Debug("Error: Inside catch block of Patent_Author_Details");
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

    public string CheckIsStudentPublication(string PatentID)
    {
        string ISStudentAuthor = "";
        log.Debug("Inside fnfindGrantid function, Project ID: " + PatentID);
        try
        {
            cmdString = " select * from Patent_Data where ID=@ID ";
            con = new SqlConnection(str);
            con.Open();
            cmd = new SqlCommand(cmdString, con);
            cmd.Parameters.Add("@ID", SqlDbType.VarChar, 10);

            cmd.Parameters["@ID"].Value = PatentID;
            SqlDataReader reader = cmd.ExecuteReader();
            // PatientPOD p = new PatientPOD();,,,
            Patent p = new Patent();
            while (reader.Read())
            {
                if (!Convert.IsDBNull(reader["ISStudentAuthor"]))
                {
                    ISStudentAuthor = (string)reader["ISStudentAuthor"];
                }
            }
            return ISStudentAuthor;
        }
        catch (Exception e)
        {
            log.Debug("Error: Inside catch block of Patent_Author_Details");
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




    public DataSet SelectStudentAuthorDetail(string PatentID)
    {
        con = new SqlConnection(str);
        con.Open();
        try
        {
            //  cmdString = "select  convert(numeric(13,2),Amount) as Amount ,DebitCredit as DrCr, a.account as account, a.oprunit as oprUnit,a.DeptID as dept,a.AffilateBU as affiliate,openitem as openItem, a.linenarration as lineNar, ADesc as accountName, OprUnit_M.OUnitName as oprUnitName,Department_M.DeptName,OpenItem_M.EmpName,a.JournalLine,EmpName as openItemName from GL_Accounting_T a  LEFT OUTER JOIN OprUnit_M ON a.OPRUNIT = OprUnit_M.OPRUNIT LEFT OUTER JOIN Department_M ON a.DeptID = Department_M.DeptCode LEFT OUTER JOIN OpenItem_M ON a.openitem = OpenItem_M.EmpCode, GL d , Account_M where a.JournalID=d.JournalID  and Account_M.Account= a.Account and a.BusinessUnit=d.BusinessUnit and a.JournalID=@JournalID and a.BusinessUnit=@BusinessUnit order by a.JournalLine";
            SqlDataAdapter da;
            DataSet ds;
            cmdString = "select InvestigatorName, MUNonMU, EmployeeCode, DepartmentName  from Patent_Inventor where MUNonMU='S' and ID=@id ";
            cmd = new SqlCommand(cmdString, con);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add("@id", SqlDbType.VarChar, 12);
            cmd.Parameters["@id"].Value = PatentID;

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

    public string deleteid(string id, string status, string entryno)
    {
        int pid = 0;
        //string result = "";
        try
        {

            con = new SqlConnection(str);
            con.Open();
            transaction = con.BeginTransaction();
            //cmdString = "select Id from Patent_App_Status where StatusName= '" + status + "'";
            //cmd = new SqlCommand(cmdString, con, transaction);
            //cmd.CommandType = CommandType.Text;
            //cmd.ExecuteNonQuery();
            //SqlDataReader sdr = cmd.ExecuteReader();

            //while (sdr.Read())
            //{


            //    if (!Convert.IsDBNull(sdr["Id"]))
            //    {
            //        pid = (int)sdr["Id"];
            //    }



            //}
            //sdr.Close();
            //cmdString = "delete from Patent_App_Status_Tracker where ID= '" + id + "' and App_Status='" + status + "' and Entry_No= '" + entryno + "'  ";
            cmdString = "delete from Patent_App_Status_Tracker where ID=@id  and App_Status=@status  and Entry_No=@entryno   ";

            cmd = new SqlCommand(cmdString, con, transaction);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@status", status);
            cmd.Parameters.AddWithValue("@entryno", entryno);
            cmd.ExecuteNonQuery();
            transaction.Commit();
            return id;
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



    public Patent SelectRenewalYear(string ID)
    {
        log.Debug("Inside SelectRenewalYear function, Patent ID: " + ID);
        try
        {
            cmdString = " select max(NextRenewalYear) from Patent_Renewal_Tracker where ID=@ID ";
            con = new SqlConnection(str);
            con.Open();
            cmd = new SqlCommand(cmdString, con);
            cmd.Parameters.Add("@ID", SqlDbType.VarChar, 10);
            cmd.Parameters["@ID"].Value = ID;

            cmd.CommandType = CommandType.Text;
            SqlDataReader sdr = cmd.ExecuteReader();

            Patent V = new Patent();

            while (sdr.Read())
            {
                if (!Convert.IsDBNull(sdr["NextRenewalYear"]))
                {
                    V.NextRenewalYear = (int)sdr["NextRenewalYear"];
                }

            }
            return V;
        }
        catch (Exception ex)
        {
            log.Error("Inside SelectRenewalYear catch block of Patent ID: " + ID);
            log.Error("Erroe Message : " + ex.Message);
            log.Error(ex.StackTrace);
            throw ex;
        }

        finally
        {
            con.Close();
        }
    }



    public Patent SelectRenewalDate(string ID)
    {
        log.Debug("Inside SelectRenewalYear function, Patent ID: " + ID);
        try
        {
            cmdString = " select NextRenewalDate from Patent_Data where ID=@ID ";
            con = new SqlConnection(str);
            con.Open();
            cmd = new SqlCommand(cmdString, con);
            cmd.Parameters.Add("@ID", SqlDbType.VarChar, 10);
            cmd.Parameters["@ID"].Value = ID;

            cmd.CommandType = CommandType.Text;
            SqlDataReader sdr = cmd.ExecuteReader();

            Patent pat1 = new Patent();

            while (sdr.Read())
            {
                if (!Convert.IsDBNull(sdr["NextRenewalDate"]))
                {
                    pat1.NextRenewalDate = (DateTime)sdr["NextRenewalDate"];
                }

            }
            return pat1;
        }
        catch (Exception ex)
        {
            log.Error("Inside SelectRenewalYear catch block of Patent ID: " + ID);
            log.Error("Erroe Message : " + ex.Message);
            log.Error(ex.StackTrace);
            throw ex;
        }

        finally
        {
            con.Close();
        }
    }

    public int UpdatePatentStatus(Patent p)
    {
        log.Debug("Inside UpdatePatentStatus of ID:  ");

        int res = 0;
        con = new SqlConnection(str);
        con.Open();
        transaction = con.BeginTransaction();
        try
        {
            //cmdString = "update Patent_Data set Filing_Status=@Status,LapseDate=@LapseDate,NextRenewalDate=@NextRenewalDate where ID= '" + p.ID + "'";
            cmdString = "update Patent_Data set Filing_Status=@Status,LapseDate=@LapseDate,NextRenewalDate=@NextRenewalDate where ID=@ID";

            cmd = new SqlCommand(cmdString, con, transaction);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@LapseDate", p.lapsedate);
            cmd.Parameters.AddWithValue("@NextRenewalDate", p.NextRenewalDate);
            cmd.Parameters.AddWithValue("@Status", p.Filing_Status);
            cmd.Parameters.AddWithValue("@ID", p.ID);

            cmd.ExecuteNonQuery();
            cmdString = "Select count(* ) as Count from Patent_Status_Tracker where  ID=@ID";
            cmd = new SqlCommand(cmdString, con, transaction);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@ID", p.ID);
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

            cmd = new SqlCommand("InsertPatentReviewTracker", con, transaction);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID", p.ID);
            cmd.Parameters.AddWithValue("@ReviewNo", count + 1);
            cmd.Parameters.AddWithValue("@Entry_status", p.Filing_Status);
            cmd.Parameters.AddWithValue("@Comment", p.Remarks);
            cmd.Parameters.AddWithValue("@Created_By", p.Created_By);
            cmd.Parameters.AddWithValue("@Date", DateTime.Now);
            cmd.ExecuteNonQuery();
            transaction.Commit();
            return res;
        }

        catch (Exception ex)
        {
            log.Error("Inside  UpdatePatentStatus catch block ");
            log.Error(ex.Message);
            log.Error(ex.StackTrace);


            throw ex;
        }

        finally
        {
            con.Close();
        }

    }

    internal bool UpdateStatus(Patent pat)
    {
        log.Debug("Inside UpdateLapseStatus of ID:  ");

        bool res = false;
        con = new SqlConnection(str);
        con.Open();
        transaction = con.BeginTransaction();
        try
        {
            //cmdString = "update Patent_Data set Filing_Status='GRN',NextRenewalDate=@NextRenewalDate where ID= '" + pat.ID + "'";
            cmdString = "update Patent_Data set Filing_Status='GRN',NextRenewalDate=@NextRenewalDate where ID=@ID";

            cmd = new SqlCommand(cmdString, con, transaction);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@NextRenewalDate", pat.NextRenewalDate);
            cmd.Parameters.AddWithValue("@ID", pat.ID);
            //cmd.Parameters.AddWithValue("@Filing_Status", "LAP");

            res = Convert.ToBoolean(cmd.ExecuteNonQuery());
            transaction.Commit();
            return res;
        }

        catch (Exception ex)
        {
            log.Error("Inside  UpdateLapseStatus catch block ");
            log.Error(ex.Message);
            log.Error(ex.StackTrace);


            throw ex;
        }

        finally
        {
            con.Close();
        }
    }

    public string SelectApplicationStage(string ID)
    {
        try
        {
            con = new SqlConnection(str);
            con.Open();
            cmd = new SqlCommand("Patent_SelectAppStatus", con);
            cmd.Parameters.Add("@ID", SqlDbType.VarChar, 10);
            cmd.Parameters["@ID"].Value = ID;

            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader sdr = cmd.ExecuteReader();
            string StatusName = "";
            while (sdr.Read())
            {
                if (!Convert.IsDBNull(sdr["StatusName"]))
                {
                    StatusName = (string)sdr["StatusName"];
                }

            }
            return StatusName;
        }
        catch (Exception ex)
        {
            log.Error("Erroe Message : " + ex.Message);
            log.Error(ex.StackTrace);
            throw ex;
        }

        finally
        {
            con.Close();
        }
    }

    public DataSet SelectPatentDetails(string id, string title, int role, string UserId)
    {
        con = new SqlConnection(str);
        con.Open();
        try
        {
            SqlDataAdapter da;
            DataSet ds;
            cmd = new SqlCommand("SelectPatentDetails", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@ID", SqlDbType.VarChar, 10);
            cmd.Parameters["@ID"].Value = id;
            cmd.Parameters.Add("@Title", SqlDbType.VarChar, 500);
            cmd.Parameters["@Title"].Value = title;
            cmd.Parameters.Add("@Role", SqlDbType.Int);
            cmd.Parameters["@Role"].Value = role;
            cmd.Parameters.Add("@UserId", SqlDbType.VarChar, 12);
            cmd.Parameters["@UserId"].Value = UserId;
            da = new SqlDataAdapter(cmd);

            ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

        catch (Exception ex)
        {
            // log.Error("Inside fnfindGrantInvestigatorDetails catch block ProjectUnit: " + bu + "ID: " + jid);
            log.Error(ex.Message);
            log.Error(ex.StackTrace);
            throw ex;
        }

        finally
        {
            con.Close();
        }
    }

    public DataSet getPatentAuthorList(string p)
    {

        try
        {
            SqlDataAdapter da;
            DataSet ds = new DataSet();
            con = new SqlConnection(str);
            con.Open();
            cmdString = " select EmailId from Patent_Inventor where ID=@ID and (MUNonMU='M' )";
            da = new SqlDataAdapter(cmdString, con);
            da.SelectCommand.Parameters.Add("@ID", SqlDbType.VarChar, 10);
            da.SelectCommand.Parameters["@ID"].Value = p;
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

    internal DataSet getPatentAuthorListName(string p)
    {

        try
        {
            SqlDataAdapter da;
            DataSet ds = new DataSet();

            con = new SqlConnection(str);
            con.Open();
            cmdString = "  select InvestigatorName from Patent_Inventor where ID=@ID";
            da = new SqlDataAdapter(cmdString, con);
            da.SelectCommand.Parameters.Add("@ID", SqlDbType.VarChar, 10);
            da.SelectCommand.Parameters["@ID"].Value = p;
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

    public string GetRDCEmail()
    {
        try
        {
            con = new SqlConnection(str);
            con.Open();
            cmd = new SqlCommand("Patent_SelectRDCEmailId", con);

            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader sdr = cmd.ExecuteReader();
            string EmailId = "";
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
            log.Error("Erroe Message : " + ex.Message);
            log.Error(ex.StackTrace);
            throw ex;
        }

        finally
        {
            con.Close();
        }
    }

    public DataSet SelectProTocompletelist(string p)
    {
        log.Debug("Inside function SelectProTocompletelist");
        con = new SqlConnection(str);
        con.Open();
        try
        {

            SqlDataAdapter da;
            DataSet ds;
            //cmdString = "select * from Patent_Data where Filing_Status='APP' and NatureofPatent=2 and Date_Of_Application=  DATEADD(month, 1, cast(convert(varchar(10), getdate(), 110) as datetime) )   ";
            cmd = new SqlCommand("Patent_SelectProToComplist", con);
            cmd.CommandType = CommandType.StoredProcedure;
            da = new SqlDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

        catch (Exception ex)
        {
            log.Error("Inside SelectProTocompletelist catch block ");
            log.Error(ex.Message);
            log.Error(ex.StackTrace);
            throw ex;
        }

        finally
        {
            con.Close();
        }
    }

    public DataSet SelectGRNtoRenewallist(string p)
    {
        log.Debug("Inside function SelectGRNtoRenewallist");
        con = new SqlConnection(str);
        con.Open();
        try
        {

            SqlDataAdapter da;
            DataSet ds;
            //cmdString = "select * from Patent_Data where Filing_Status='GRN' and NextRenewalDate=  DATEADD(month, 1, cast(convert(varchar(10), getdate(), 110) as datetime) ) ";
            cmd = new SqlCommand("Patent_SelectPatentGRNtoRNW", con);
            cmd.CommandType = CommandType.StoredProcedure;
            da = new SqlDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

        catch (Exception ex)
        {
            log.Error("Inside SelectGRNtoRenewallist catch block ");
            log.Error(ex.Message);
            log.Error(ex.StackTrace);
            throw ex;
        }

        finally
        {
            con.Close();
        }
    }

    public string CheckEntryStatus(string p)
    {
        try
        {
            con = new SqlConnection(str);
            con.Open();
            //cmd = new SqlCommand("Select Entry_Status from Patent_Data where ID='" + p + "'", con);
            cmd = new SqlCommand("Select Entry_Status from Patent_Data where ID=@p ", con);


            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@p", p);
            SqlDataReader sdr = cmd.ExecuteReader();
            string status = "";
            while (sdr.Read())
            {
                if (!Convert.IsDBNull(sdr["Entry_Status"]))
                {
                    status = (string)sdr["Entry_Status"];
                }

            }
            return status;
        }
        catch (Exception ex)
        {
            log.Error("Erroe Message : " + ex.Message);
            log.Error(ex.StackTrace);
            throw ex;
        }

        finally
        {
            con.Close();
        }
    }

    public int UploadPatentPathCreate(Patent pat)
    {
        log.Debug("Inside UploadPatentPathCreate function, ID: " + pat.ID + "Nature of Patent: " + pat.NatureOfPatent);
        int result = 0;
        con = new SqlConnection(str);
        con.Open();
        transaction = con.BeginTransaction();
        try
        {
            int countNum = 0;
            cmdString = " select COUNT(*) as count from PatentAuxillaryDetails where ID=@ID";
            cmd = new SqlCommand(cmdString, con, transaction);
            cmd.Parameters.Add("@ID", SqlDbType.VarChar, 15);
            cmd.Parameters["@ID"].Value = pat.ID;
            cmd.CommandType = CommandType.Text;
            SqlDataReader sdr = cmd.ExecuteReader();
            GrantData V = new GrantData();
            while (sdr.Read())
            {

                if (!Convert.IsDBNull(sdr["count"]))
                {
                    countNum = (int)sdr["count"];
                }
                else if (Convert.IsDBNull(sdr["count"]))
                {
                    countNum = 0;
                }
            }
            sdr.Close();
            if (countNum == 0)
            {
                cmdString = "insert into PatentAuxillaryDetails  (ID,EntryNo,UploadPDFPath,CreatedBy,CreatedDate,Remark,Filing_Status)  values (@ID,@EntryNum,@UploadPDFPath,@CreatedBy,@CreatedDate,@Remark,@Filing_Status) ";
                cmd = new SqlCommand(cmdString, con, transaction);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@ID", pat.ID);
                cmd.Parameters.AddWithValue("@EntryNum", countNum + 1);
                cmd.Parameters.AddWithValue("@UploadPDFPath", pat.FilePath);
                cmd.Parameters.AddWithValue("@CreatedBy", HttpContext.Current.Session["UserId"].ToString());
                cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
                cmd.Parameters.AddWithValue("@Remark", pat.UploadRemarks);
                cmd.Parameters.AddWithValue("@Filing_Status", pat.Filing_Status);
                result = cmd.ExecuteNonQuery();
                transaction.Commit();
                log.Error("Inside Auxilary details updated  sucessfully ID: " + pat.ID + "NatureOfPatent: " + pat.NatureOfPatent);

            }
            else if (countNum >= 1)
            {
                cmdString = "Update  PatentAuxillaryDetails set EntryNo=@EntryNum,UploadPDFPath=@UploadPDFPath,CreatedBy=@CreatedBy,CreatedDate=@CreatedDate,Remark=@Remark,Filing_Status=@Filing_Status,Deleted='N' where ID=@ID";
                cmd = new SqlCommand(cmdString, con, transaction);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@ID", pat.ID);
                cmd.Parameters.AddWithValue("@EntryNum", countNum);
                cmd.Parameters.AddWithValue("@UploadPDFPath", pat.FilePath);
                cmd.Parameters.AddWithValue("@CreatedBy", HttpContext.Current.Session["UserId"].ToString());
                cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
                cmd.Parameters.AddWithValue("@Remark", pat.UploadRemarks);
                cmd.Parameters.AddWithValue("@Filing_Status", pat.Filing_Status);

                result = cmd.ExecuteNonQuery();
                transaction.Commit();
                log.Error("Inside Auxilary details inserted sucessfully ID: " + pat.ID + "NatureOfPatent: " + pat.NatureOfPatent);

            }
            return result;
        }

        catch (Exception ex)
        {
            log.Error("Inside UploadPatentPathCreate catch block ID: " + pat.ID + "Nature of Patent: " + pat.NatureOfPatent);
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


    public string GetPatentFileUploadPath(string p)
    {
        log.Debug("Inside GetPatentFileUploadPath function, ID: " + p);

        try
        {
            cmdString = "select * from PatentAuxillaryDetails where ID=@ID";
            con = new SqlConnection(str);
            con.Open();
            cmd = new SqlCommand(cmdString, con);
            cmd.Parameters.Add("@ID", SqlDbType.VarChar, 15);
            cmd.Parameters["@ID"].Value = p;
            cmd.CommandType = CommandType.Text;
            SqlDataReader sdr = cmd.ExecuteReader();
            string fileuploadP = "";

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
            log.Error("Inside GetPatentFileUploadPath catch block  ID: " + p);
            log.Error(ex.Message);
            log.Error(ex.StackTrace);
            throw ex;
        }

        finally
        {
            con.Close();
        }
    }

    public int UpdatePatentattachedEntry(Patent j)
    {
        log.Debug("Inside UpdatePatentattachedEntry function, ID: " + j.ID);
        int result = 0;
        con = new SqlConnection(str);
        con.Open();
        transaction = con.BeginTransaction();
        try
        {
            cmdString = "update PatentAuxillaryDetails set Deleted='Y' where ID=@ID and  EntryNo=@EntryNum";
            cmd = new SqlCommand(cmdString, con, transaction);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@ID", j.ID);
            cmd.Parameters.AddWithValue("@EntryNum", j.Entrynum);
            result = cmd.ExecuteNonQuery();
            transaction.Commit();
            log.Info("File Upload Status changed to :'Y' of projectID: " + j.ID + "Entry Number : " + j.Entrynum);
            return result;
        }

        catch (Exception ex)
        {
            log.Error("Inside UpdatePatentattachedEntry catch block of projectID: " + j.ID + "Entry Number : " + j.Entrynum);
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



    public int SelectCountUploadgrantInformationType(string p1, string p2)
    {
        log.Debug("Inside SelectCountUploadgrantInformationType function, ID: " + p1 + "Filing_status: " + p2);
        con = new SqlConnection(str);
        con.Open();
        transaction = con.BeginTransaction();
        try
        {
            int countNum = 0;
            cmdString = " select COUNT(*) as count from PatentAuxillaryDetails where ID=@ID  and Filing_status='GRN' and Deleted='N' ";
            cmd = new SqlCommand(cmdString, con, transaction);
            cmd.Parameters.Add("@ID", SqlDbType.VarChar, 15);
            cmd.Parameters["@ID"].Value = p1;
            cmd.CommandType = CommandType.Text;
            SqlDataReader sdr = cmd.ExecuteReader();
            GrantData V = new GrantData();
            while (sdr.Read())
            {

                if (!Convert.IsDBNull(sdr["count"]))
                {
                    countNum = (int)sdr["count"];
                }
                else if (Convert.IsDBNull(sdr["count"]))
                {
                    countNum = 0;
                }

            }
            sdr.Close();
            transaction.Commit();
            return countNum;
        }

        catch (Exception ex)
        {
            log.Error("Inside SelectCountUploadgrantInformationType catch block ID: " + p1 + "Project Unit: " + p2);
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

    public Patent fnfindjid(string Pid)
    {
        log.Debug("Inside Patent_DataObject- fnfindPatent function, PatentId: " + Pid);
        try
        {

            cmdString = "select * from Patent_Data where ID=@PatentID ";

            // cmdString = "select BusinessUnit,JournalID, JournalDate, LineNarration,LongNarration, EntryStatus from GL where JournalID=@JournalID and BusinessUnit=@BusinessUnit";
            con = new SqlConnection(str);
            con.Open();
            cmd = new SqlCommand(cmdString, con);
            cmd.Parameters.Add("@PatentID", SqlDbType.VarChar, 15);
            cmd.Parameters["@PatentID"].Value = Pid;

            // cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandType = CommandType.Text;
            SqlDataReader sdr = cmd.ExecuteReader();
            // voucher p = new voucher();
            Patent V = new Patent();

            while (sdr.Read())
            {
                if (!Convert.IsDBNull(sdr["Title"]))
                {
                    V.Title = (string)sdr["Title"];
                }
                else if (Convert.IsDBNull(sdr["Title"]))
                {
                    V.Title = "";
                }

                if (!Convert.IsDBNull(sdr["ID"]))
                {
                    V.ID = (string)sdr["ID"];
                }
                else if (Convert.IsDBNull(sdr["ID"]))
                {
                    V.ID = "";
                }


                if (!Convert.IsDBNull(sdr["Description"]))
                {
                    V.description = (string)sdr["Description"];
                }
                else if (Convert.IsDBNull(sdr["Description"]))
                {
                    V.description = "";
                }

                if (!Convert.IsDBNull(sdr["Grant_Date"]))
                {
                    V.Grant_Date = (DateTime)sdr["Grant_Date"];
                }
                else if (Convert.IsDBNull(sdr["Grant_Date"]))
                {

                }


                if (!Convert.IsDBNull(sdr["Filing_Office"]))
                {
                    V.Filing_Office = (string)sdr["Filing_Office"];
                }
                else if (Convert.IsDBNull(sdr["Filing_Office"]))
                {
                    V.Filing_Office = "";
                }
                if (!Convert.IsDBNull(sdr["Patent_Number"]))
                {
                    V.Patent_Number = (string)sdr["Patent_Number"];
                }
                else if (Convert.IsDBNull(sdr["Patent_Number"]))
                {
                    V.Patent_Number = "";
                }
            }
            return V;
        }
        catch (Exception ex)
        {
            log.Error("Inside Patent_DataObject- fnfindjid catch block ");
            log.Error(ex.Message);
            log.Error(ex.StackTrace);
            throw ex;
        }

        finally
        {
            con.Close();
        }
    }



    internal DataTable GetPatentWisePoints(string MemberId)
    {
        log.Debug("Patent_DataObjects.cs :GetPatentWisePoints inside GetData function, MemberId=" + MemberId);
        try
        {
            con.Open();
            //    cmd1 = new SqlCommand("GetInterviewDetails", con1);
            cmd = new SqlCommand("SELECT ReferenceNumber,Title,Grant_Date, sum(a.TotalPoint) as Points FROM Member_Incentive_Point_Transaction a INNER JOIN Patent_Data b ON a.ReferenceNumber=b.ID and  MemberId=@MemberId GROUP BY a.ReferenceNumber,Title,Grant_Date", con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@MemberId", MemberId);

            //cmd1.Parameters.AddWithValue("@keyword", keyword);

            DataTable dtable = new DataTable();
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            adp.Fill(dtable);
            return dtable;
        }

        catch (Exception e)
        {
            log.Error("Patent_DataObjects Error: Inside GetPatentWisePoints catch block of GetData MemberId:" + MemberId);
            log.Error("Error msg:" + e);
            log.Error("Stack trace:" + e.StackTrace);
            throw e;
        }
        finally
        {
            con.Close();
        }
    }

    public Patent SelectPatentData(string memberid, string p)
    {
        Patent obj = new Patent();
        SqlDataReader sdr = null;
        con.Open();
        try
        {
            cmd = new SqlCommand("select a.ID, EmployeeCode,Grant_Date,Title,Patent_Number,Filing_Office  from Patent_Data a,Patent_Inventor b where a.ID=b.ID and EmployeeCode=@MemberId and a.ID=@ID", con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@MemberId", memberid);
            cmd.Parameters.AddWithValue("@ID", p);
            sdr = cmd.ExecuteReader();

            while (sdr.Read())
            {
                if (!Convert.IsDBNull(sdr["ID"]))
                {
                    obj.ID = (string)sdr["ID"];
                }

                if (!Convert.IsDBNull(sdr["EmployeeCode"]))
                {
                    obj.EmployeeCode = (string)sdr["EmployeeCode"];
                }
                if (!Convert.IsDBNull(sdr["Grant_Date"]))
                {
                    obj.Grant_Date = (DateTime)sdr["Grant_Date"];
                }
                if (!Convert.IsDBNull(sdr["Title"]))
                {
                    obj.Title = (string)sdr["Title"];
                }
                if (!Convert.IsDBNull(sdr["Patent_Number"]))
                {
                    obj.Patent_Number = (string)sdr["Patent_Number"];
                }
                if (!Convert.IsDBNull(sdr["Filing_Office"]))
                {
                    obj.Filing_Office = (string)sdr["Filing_Office"];
                }

            }


            return obj;
        }

        catch (Exception ex)
        {
            log.Debug("Inside SelectPublicationData function of member id:" + memberid + "Publication ID : " + p);
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

    public IncentivePoint SelectPatentMemberCurBalance(string memberid)
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
            log.Error("Error: Inside catch block of SelectMemberCurBalance of ID: " + memberid);
            log.Error("Error msg:" + e);
            log.Error("Stack trace:" + e.StackTrace);
            throw e;

        }
        finally
        {
            con.Close();
        }
    }
}