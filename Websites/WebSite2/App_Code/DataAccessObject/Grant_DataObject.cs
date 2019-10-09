using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using log4net;
using System.Collections;




/// </summary>
public class Grant_DataObject
{
    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    public string str;
    public string cmdString;
    string seedFinalUTN = "";
    public SqlConnection con;
    public SqlCommand cmd;

    SqlTransaction transaction;
    public Grant_DataObject()
    {
        str = ConfigurationManager.ConnectionStrings["RMSConnectionString"].ConnectionString;
        cmdString = "";
        con = new SqlConnection(str);
        cmd = new SqlCommand(cmdString, con);

    }


    public int insertGrantEntry(GrantData j, GrantData[] jd)
    {
        log.Debug("Inside insertGrantEntry function of Project Unit: " + j.GrantUnit + "ID: " + j.GID);
        int result = 0, result1 = 0, seed = 0, seed1 = 0, year1 = 0;
        string seedFinal = "";
        string seedUTN = "";
        string seedFinalUTN = "";
        con = new SqlConnection(str);
        con.Open();
        transaction = con.BeginTransaction();
        try
        {

            cmdString = "select seed from Id_Gen_Project where Project_UnitId=@ProjectUnit";
            cmd = new SqlCommand(cmdString, con, transaction);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("@ProjectUnit", j.GrantUnit);
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

            DateTime date = Convert.ToDateTime(j.AppliedDate);
            int yearvalue = date.Year;
            int resultvalue = 0;

            HttpContext.Current.Session["Grantseed"] = seedFinal;

            string inst = HttpContext.Current.Session["InstituteId"].ToString();
            string utnid = HttpContext.Current.Session["Department"].ToString();

            //cmdString = "select Seed,Year from Id_Gen_UTN where UTN_ID in(select UTN_ID from Dept_M where Institute_Id=@Institute and DeptId=@DeptId) and ProjectType='" + j.GrantType + "' and Year=" + yearvalue + "";
            cmdString = "select Seed,Year from Id_Gen_UTN where UTN_ID in(select UTN_ID from Dept_M where Institute_Id=@Institute and DeptId=@DeptId) and ProjectType=@GrantType and Year=@yearvalue";
            cmd = new SqlCommand(cmdString, con, transaction);

            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@GrantType", j.GrantType);
            cmd.Parameters.AddWithValue("@yearvalue", yearvalue);

            if (j.MUNonMUUTN == "NUTN")
            {
                cmd.Parameters.AddWithValue("@Institute", inst);
                cmd.Parameters.AddWithValue("@DeptId", utnid);

            }
            else
            {

                cmd.Parameters.AddWithValue("@Institute", j.PiInstId);
                cmd.Parameters.AddWithValue("@DeptId", j.PiDeptId);

            }
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
            if (j.MUNonMUUTN == "NUTN")
            {
                cmd.Parameters.AddWithValue("@Institute", inst);
                cmd.Parameters.AddWithValue("@DeptId", utnid);

            }
            else
            {

                cmd.Parameters.AddWithValue("@Institute", j.PiInstId);
                cmd.Parameters.AddWithValue("@DeptId", j.PiDeptId);

            }
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
            seedFinalUTN = j.GrantType + seedStr2 + year_Utn + seedUTN;
            HttpContext.Current.Session["GrantseedUTNseed"] = seedFinalUTN;

            if (HttpContext.Current.Session["Role"].ToString() == "2")
            {
                cmd = new SqlCommand("InsertProjectEntryRDC", con, transaction);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@UTN", seedFinalUTN);
                cmd.Parameters.AddWithValue("@ID", seedFinal);


                cmd.Parameters.AddWithValue("@Title", j.Title);

                cmd.Parameters.AddWithValue("@Description", j.Description);
                if (j.Contact_No != null)
                {
                    cmd.Parameters.AddWithValue("@Contact_No", j.Contact_No);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Contact_No", DBNull.Value);
                }
                cmd.Parameters.AddWithValue("@GrantingAgency", j.GrantingAgency);
                cmd.Parameters.AddWithValue("@GrantUnit", j.GrantUnit);
                if (j.GranAmount != 0.0)
                {
                    cmd.Parameters.AddWithValue("@GranAmount", j.GranAmount);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@GranAmount", DBNull.Value);
                }
                cmd.Parameters.AddWithValue("@RevisedAppliedAmount", j.RevisedAppliedAmt);
                if (j.AppliedDate.ToShortDateString() != "01/01/0001")
                {
                    cmd.Parameters.AddWithValue("@AppliedDate", j.AppliedDate);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@AppliedDate", DBNull.Value);
                }
                cmd.Parameters.AddWithValue("@GrantSource", j.GrantSource);

                cmd.Parameters.AddWithValue("@GrantType", j.GrantType);

                cmd.Parameters.AddWithValue("@Status", j.Status);
                cmd.Parameters.AddWithValue("@CreatedBy", j.CreatedBy);
                cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
                cmd.Parameters.AddWithValue("@PIInstitutionID", j.PiInstId);
                cmd.Parameters.AddWithValue("@PIDeptID", j.PiDeptId);
                cmd.Parameters.AddWithValue("@InstitutionID", j.InstUser);
                cmd.Parameters.AddWithValue("@DeptID", j.DeptUser);
                cmd.Parameters.AddWithValue("@comments", j.AddtionalComments);
                if (j.Address != "")
                {
                    cmd.Parameters.AddWithValue("@Address", j.Address);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Address", DBNull.Value);
                }
                if (j.Pan_No != "")
                {
                    cmd.Parameters.AddWithValue("@Pan_No", j.Pan_No);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Pan_No", DBNull.Value);
                }
                if (j.State != "")
                {
                    cmd.Parameters.AddWithValue("@State_Code", j.State);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@State_Code", DBNull.Value);
                }
                if (j.Country != "")
                {
                    cmd.Parameters.AddWithValue("@Country_Code", j.Country);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Country_Code", DBNull.Value);
                }
                if (j.AgencyContact != "")
                {
                    cmd.Parameters.AddWithValue("@Agency_Contact", j.AgencyContact);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Agency_Contact", DBNull.Value);
                }
                if (j.AgencyEmailId != "")
                {
                    cmd.Parameters.AddWithValue("@Email_Id", j.AgencyEmailId);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Email_Id", DBNull.Value);
                }
                if ((j.FundingSectorLevelGrant !=null) && (j.FundingSectorLevelGrant !=0))
                {
                    cmd.Parameters.AddWithValue("@FundingSectorLevel", j.FundingSectorLevelGrant);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@FundingSectorLevel", DBNull.Value);
                }
                if ((j.TypeofAgencyGrant != null)&&(j.TypeofAgencyGrant !=0))
                {
                    cmd.Parameters.AddWithValue("@TypeofAgency", j.TypeofAgencyGrant);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@TypeofAgency", DBNull.Value);
                }
                cmd.Parameters.AddWithValue("@ERFRealated", j.ERFRelated);
                //if (j.ProjectActualDate.ToShortDateString() != "01/01/0001")
                //{
                //    cmd.Parameters.AddWithValue("@ProjectActualDate", j.ProjectActualDate);
                //}
                //else
                //{
                //    cmd.Parameters.AddWithValue("@ProjectActualDate", DBNull.Value);
                //}
                if (j.DurationOfProject != 0)
                {
                    cmd.Parameters.AddWithValue("@DurationOfProject", j.DurationOfProject);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@DurationOfProject", DBNull.Value);
                }

                result = cmd.ExecuteNonQuery();
            }
            else
            {
                cmd = new SqlCommand("InsertProjectEntry", con, transaction);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@UTN", seedFinalUTN);
                cmd.Parameters.AddWithValue("@ID", seedFinal);


                cmd.Parameters.AddWithValue("@Title", j.Title);

                cmd.Parameters.AddWithValue("@Description", j.Description);
                if (j.Contact_No != null)
                {
                    cmd.Parameters.AddWithValue("@Contact_No", j.Contact_No);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Contact_No", DBNull.Value);
                }
                cmd.Parameters.AddWithValue("@GrantingAgency", j.GrantingAgency);
                cmd.Parameters.AddWithValue("@GrantUnit", j.GrantUnit);
                if (j.GranAmount != 0.0)
                {
                    cmd.Parameters.AddWithValue("@GranAmount", j.GranAmount);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@GranAmount", DBNull.Value);
                }
                cmd.Parameters.AddWithValue("@RevisedAppliedAmount", j.RevisedAppliedAmt);
                if (j.AppliedDate.ToShortDateString() != "01/01/0001")
                {
                    cmd.Parameters.AddWithValue("@AppliedDate", j.AppliedDate);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@AppliedDate", DBNull.Value);
                }
                cmd.Parameters.AddWithValue("@GrantSource", j.GrantSource);

                cmd.Parameters.AddWithValue("@GrantType", j.GrantType);

                cmd.Parameters.AddWithValue("@Status", j.Status);
                cmd.Parameters.AddWithValue("@CreatedBy", j.CreatedBy);
                cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
                cmd.Parameters.AddWithValue("@PIInstitutionID", j.PiInstId);
                cmd.Parameters.AddWithValue("@PIDeptID", j.PiDeptId);
                cmd.Parameters.AddWithValue("@InstitutionID", j.InstUser);
                cmd.Parameters.AddWithValue("@DeptID", j.DeptUser);
                cmd.Parameters.AddWithValue("@comments", j.AddtionalComments);
                if (j.Address != "")
                {
                    cmd.Parameters.AddWithValue("@Address", j.Address);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Address", DBNull.Value);
                }
                if (j.Pan_No != "")
                {
                    cmd.Parameters.AddWithValue("@Pan_No", j.Pan_No);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Pan_No", DBNull.Value);
                }
                if (j.State != "")
                {
                    cmd.Parameters.AddWithValue("@State_Code", j.State);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@State_Code", DBNull.Value);
                }
                if (j.Country != "")
                {
                    cmd.Parameters.AddWithValue("@Country_Code", j.Country);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Country_Code", DBNull.Value);
                }
                if (j.AgencyContact != "")
                {
                    cmd.Parameters.AddWithValue("@Agency_Contact", j.AgencyContact);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Agency_Contact", DBNull.Value);
                }
                if (j.AgencyEmailId != "")
                {
                    cmd.Parameters.AddWithValue("@Email_Id", j.AgencyEmailId);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@Email_Id", DBNull.Value);
                }
                if ((j.FundingSectorLevelGrant != null) && (j.FundingSectorLevelGrant != 0))
                {
                    cmd.Parameters.AddWithValue("@FundingSectorLevel", j.FundingSectorLevelGrant);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@FundingSectorLevel", DBNull.Value);
                }
                if ((j.TypeofAgencyGrant != null) && (j.TypeofAgencyGrant != 0))
                {
                    cmd.Parameters.AddWithValue("@TypeofAgency", j.TypeofAgencyGrant);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@TypeofAgency", DBNull.Value);
                }
                cmd.Parameters.AddWithValue("@ERFRealated", j.ERFRelated);
                //if (j.ProjectActualDate.ToShortDateString() != "01/01/0001")
                //{
                //    cmd.Parameters.AddWithValue("@ProjectActualDate", j.ProjectActualDate);
                //}
                //else
                //{
                //    cmd.Parameters.AddWithValue("@ProjectActualDate", DBNull.Value);
                //}
                if (j.DurationOfProject != 0)
                {
                    cmd.Parameters.AddWithValue("@DurationOfProject", j.DurationOfProject);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@DurationOfProject", DBNull.Value);
                }

                result = cmd.ExecuteNonQuery();
            }
            log.Info("Grant Data inserted sucessfully  of Project Unit: " + j.GrantUnit + "ID: " + j.GID);
            if (result >= 1)
            {
                for (int i = 0; i < jd.Length; i++)
                {
                    cmd = new SqlCommand("InsertProjectInvestigator", con, transaction);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ID", seedFinal);
                    cmd.Parameters.AddWithValue("@ProjectUnit", j.GrantUnit);
                    cmd.Parameters.AddWithValue("@GrantLine", i + 1);

                    cmd.Parameters.AddWithValue("@AuthorName", jd[i].AuthorName);
                    cmd.Parameters.AddWithValue("@MUNonMU", jd[i].MUNonMU);
                    cmd.Parameters.AddWithValue("@EmployeeCode", jd[i].EmployeeCode);

                    cmd.Parameters.AddWithValue("@Institution", jd[i].Institution);
                    cmd.Parameters.AddWithValue("@Department", jd[i].Department);

                    cmd.Parameters.AddWithValue("@InstitutionName", jd[i].InstitutionName);
                    cmd.Parameters.AddWithValue("@DepartmentName", jd[i].DepartmentName);
                    cmd.Parameters.AddWithValue("@AuthorType", jd[i].AuthorType);

                    if (jd[i].AuthorType=="P" && jd[i].LeadPI == "Y")
                    {

                        cmd.Parameters.AddWithValue("@isLeadPI", "Y");
                    }
                    else if (jd[i].AuthorType == "P" && jd[i].LeadPI == "N")
                    {

                        cmd.Parameters.AddWithValue("@isLeadPI", "N");
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@isLeadPI", DBNull.Value);
                    }
                    cmd.Parameters.AddWithValue("@NationalInternational", jd[i].NationalInternationl);
                    cmd.Parameters.AddWithValue("@Continent", jd[i].continental);
                    cmd.Parameters.AddWithValue("@EmailId", jd[i].EmailId);
                    result1 = cmd.ExecuteNonQuery();

                    log.Info("Grant investigator details inserted sucessfully  of Project Unit: " + j.GrantUnit + "ID: " + j.GID);
                }

                cmdString = "Select count(* ) as Count from ProjectStatusTracker where  ID=@ID and ProjectUnit=@GrantUnit";
                cmd = new SqlCommand(cmdString, con, transaction);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@ID", seedFinal);
                cmd.Parameters.AddWithValue("@GrantUnit", j.GrantUnit);
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

                cmd = new SqlCommand("InsertProjectReviewTracker", con, transaction);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", seedFinal);
                cmd.Parameters.AddWithValue("@GrantUnit", j.GrantUnit);
                cmd.Parameters.AddWithValue("@ReviewNo", count + 1);
                cmd.Parameters.AddWithValue("@ApprovedStatus", j.Status);
                cmd.Parameters.AddWithValue("@Remark", j.AddtionalComments);
                cmd.Parameters.AddWithValue("@UpdateUser", j.CreatedBy);
                cmd.Parameters.AddWithValue("@Date", DateTime.Now);
                result = cmd.ExecuteNonQuery();

                log.Info("Inserted into project review tracker of Project Unit: " + j.GrantUnit + "ID: " + j.GID);
                if (resultvalue == 1)
                {
                    cmdString = "select UTN_ID from Dept_M where Institute_Id=@Institute and DeptId=@DeptId";
                    cmd = new SqlCommand(cmdString, con, transaction);
                    cmd.CommandType = CommandType.Text;
                    if (j.MUNonMUUTN == "NUTN")
                    {
                        cmd.Parameters.AddWithValue("@Institute", inst);
                        cmd.Parameters.AddWithValue("@DeptId", utnid);

                    }
                    else
                    {

                        cmd.Parameters.AddWithValue("@Institute", j.PiInstId);
                        cmd.Parameters.AddWithValue("@DeptId", j.PiDeptId);

                    }
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

                    cmdString = "Insert into  Id_Gen_UTN  (Seed,Year,UTN_ID,ProjectType) VALUES(@value,@Year,@UTN,@GrantType)";
                    cmd = new SqlCommand(cmdString, con, transaction);
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue("@GrantType", j.GrantType);
                    cmd.Parameters.AddWithValue("@value", seed1 + 1);
                    cmd.Parameters.AddWithValue("@Year", year1);
                    cmd.Parameters.AddWithValue("@UTN", utn);
                    cmd.ExecuteNonQuery();
                    int value = seed1 + 1;
                    log.Info("Inserted new value to the ID_Gen table for the year  : " + year1 + "and ID is :" + value);
                }
                else
                {
                    cmdString = "update Id_Gen_UTN set Seed=@value where UTN_ID in(select UTN_ID from Dept_M where Institute_Id=@Institute and DeptId=@DeptId) and ProjectType=@GrantType";
                    cmd = new SqlCommand(cmdString, con, transaction);
                    cmd.CommandType = CommandType.Text;
                    if (j.MUNonMUUTN == "NUTN")
                    {
                        cmd.Parameters.AddWithValue("@Institute", inst);
                        cmd.Parameters.AddWithValue("@DeptId", utnid);

                    }
                    else
                    {

                        cmd.Parameters.AddWithValue("@Institute", j.PiInstId);
                        cmd.Parameters.AddWithValue("@DeptId", j.PiDeptId);

                    }
                    cmd.Parameters.AddWithValue("@GrantType", j.GrantType);
                    cmd.Parameters.AddWithValue("@value", seed1 + 1);
                    cmd.ExecuteNonQuery();
                    int value = seed1 + 1;
                    log.Info("Updated ID_Gen Value with : " + value);

                }
            }


            transaction.Commit();
            return result;
        }

        catch (Exception ex)
        {
            log.Error("Inside insertGrantEntry catch block of Project Unit: " + j.GrantUnit + "ID: " + j.GID);
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

    //Project Status changed to applied or rework
    public int UpdateGrantEntry(GrantData j, GrantData[] jd)
    {
        log.Debug("Inside UpdateGrantEntry function of Project Unit: " + j.GrantUnit + "ID: " + j.GID);
        int result = 0, result1 = 0;
        con = new SqlConnection(str);
        con.Open();
        transaction = con.BeginTransaction();
        try
        {
            cmd = new SqlCommand("UpdateProjectEntry", con, transaction);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID", j.GID);
            cmd.Parameters.AddWithValue("@Title", j.Title);
            cmd.Parameters.AddWithValue("@Description", j.Description);
            cmd.Parameters.AddWithValue("@GrantingAgency", j.GrantingAgency);
            cmd.Parameters.AddWithValue("@GrantUnit", j.GrantUnit);
            if (j.GranAmount != 0.0)
            {

                cmd.Parameters.AddWithValue("@GranAmount", j.GranAmount);
            }
            else
            {
                cmd.Parameters.AddWithValue("@GranAmount", DBNull.Value);
            }
            cmd.Parameters.AddWithValue("@RevisedAppliedAmount", j.RevisedAppliedAmt);
            if (j.AppliedDate.ToShortDateString() != "01/01/0001")
            {
                cmd.Parameters.AddWithValue("@AppliedDate", j.AppliedDate);
            }
            else
            {
                cmd.Parameters.AddWithValue("@AppliedDate", DBNull.Value);
            }
            cmd.Parameters.AddWithValue("@GrantSource", j.GrantSource);

            cmd.Parameters.AddWithValue("@GrantType", j.GrantType);
            cmd.Parameters.AddWithValue("@comments", j.AddtionalComments);
            if (j.Contact_No != "")
            {
                cmd.Parameters.AddWithValue("@Contact_No", j.Contact_No);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Contact_No", DBNull.Value);
            }

            cmd.Parameters.AddWithValue("@Address", j.Address);
            cmd.Parameters.AddWithValue("@Pan_No", j.Pan_No);
            cmd.Parameters.AddWithValue("@State_Code", j.State);
            cmd.Parameters.AddWithValue("@Country_Code", j.Country);
            cmd.Parameters.AddWithValue("@PIInstitutionID", j.PiInstId);
            cmd.Parameters.AddWithValue("@PIDeptID", j.PiDeptId);
            cmd.Parameters.AddWithValue("@ERFRealated", j.ERFRelated);
            cmd.Parameters.AddWithValue("@Agency_Contact", j.AgencyContact);
            cmd.Parameters.AddWithValue("@Email_Id", j.AgencyEmailId);
            if (j.SanctionOrderDate.ToShortDateString() == "01/01/0001")
            {
                cmd.Parameters.AddWithValue("@SanctionOrderDate", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@SanctionOrderDate", j.SanctionOrderDate);
            }
           
            //if (j.ProjectActualDate.ToShortDateString() != "01/01/0001")
            //{
            //    cmd.Parameters.AddWithValue("@ProjectActualDate", j.ProjectActualDate);
            //}
            //else
            //{
            //    cmd.Parameters.AddWithValue("@ProjectActualDate", DBNull.Value);
            //}

            if (j.DurationOfProject!= 0)
            {
                cmd.Parameters.AddWithValue("@DurationOfProject", j.DurationOfProject);
            }
            else
            {
                cmd.Parameters.AddWithValue("@DurationOfProject", DBNull.Value);
            }
            if ((j.FundingSectorLevelGrant != null) && (j.FundingSectorLevelGrant != 0))
            {
                cmd.Parameters.AddWithValue("@FundingSectorLevel", j.FundingSectorLevelGrant);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FundingSectorLevel", DBNull.Value);
            }
            if ((j.TypeofAgencyGrant != null) && (j.TypeofAgencyGrant != 0))
            {
                cmd.Parameters.AddWithValue("@TypeofAgency", j.TypeofAgencyGrant);
            }
            else
            {
                cmd.Parameters.AddWithValue("@TypeofAgency", DBNull.Value);
            }
            result = cmd.ExecuteNonQuery();
            log.Debug("Grant Data Updated Sucessfully of Project Unit: " + j.GrantUnit + "ID: " + j.GID);

            if (result == 1)
            {
                cmdString = "delete from  Projectnvestigator  where ID=@ID and ProjectUnit=@GrantUnit";
                cmd = new SqlCommand(cmdString, con, transaction);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@ID", j.GID);
                cmd.Parameters.AddWithValue("@GrantUnit", j.GrantUnit);
                result1 = cmd.ExecuteNonQuery();

                for (int i = 0; i < jd.Length; i++)
                {
                    cmd = new SqlCommand("InsertProjectInvestigator", con, transaction);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", j.GID);
                    cmd.Parameters.AddWithValue("@GrantLine", i + 1);
                    cmd.Parameters.AddWithValue("@AuthorName", jd[i].AuthorName);
                    cmd.Parameters.AddWithValue("@MUNonMU", jd[i].MUNonMU);
                    cmd.Parameters.AddWithValue("@EmployeeCode", jd[i].EmployeeCode);
                    cmd.Parameters.AddWithValue("@Institution", jd[i].Institution);
                    cmd.Parameters.AddWithValue("@Department", jd[i].Department);
                    cmd.Parameters.AddWithValue("@InstitutionName", jd[i].InstitutionName);
                    cmd.Parameters.AddWithValue("@DepartmentName", jd[i].DepartmentName);
                    cmd.Parameters.AddWithValue("@AuthorType", jd[i].AuthorType);
                    if (jd[i].AuthorType == "P" && jd[i].LeadPI == "Y")
                    {

                        cmd.Parameters.AddWithValue("@isLeadPI", "Y");
                    }
                    else if (jd[i].AuthorType == "P" && jd[i].LeadPI == "N")
                    {

                        cmd.Parameters.AddWithValue("@isLeadPI", "N");
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@isLeadPI", DBNull.Value);
                    }
                    cmd.Parameters.AddWithValue("@NationalInternational", jd[i].NationalInternationl);
                    cmd.Parameters.AddWithValue("@Continent", jd[i].continental);
                    cmd.Parameters.AddWithValue("@EmailId", jd[i].EmailId);
                    cmd.Parameters.AddWithValue("@ProjectUnit", j.GrantUnit);
                    result1 = cmd.ExecuteNonQuery();
                    log.Debug("Grant investigator Updated Sucessfully of Project Unit: " + j.GrantUnit + "ID: " + j.GID);
                }
            }

            transaction.Commit();
            return result1;
        }

        catch (Exception ex)
        {
            log.Error("Inside UpdateGrantEntry catch block of Project Unit: " + j.GrantUnit + "ID: " + j.GID);
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


    //Project Status changed to submit
    public int UpdateStatusGrantEntryAcceptApproval(GrantData j, GrantData[] jd, GrantData[] jd1, GrantData[] sd1)
    {
        log.Debug("Inside UpdateStatusGrantEntryAcceptApproval function of Project Unit: " + j.GrantUnit + "ID: " + j.GID);
        int result = 0, result1 = 0, result2 = 0, result3 = 0;
        con = new SqlConnection(str);
        con.Open();
        transaction = con.BeginTransaction();
        try
        {
            cmd = new SqlCommand("UpdateProjectEntry", con, transaction);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID", j.GID);
            cmd.Parameters.AddWithValue("@Title", j.Title);
            cmd.Parameters.AddWithValue("@Description", j.Description);
            cmd.Parameters.AddWithValue("@GrantingAgency", j.GrantingAgency);
            cmd.Parameters.AddWithValue("@GrantUnit", j.GrantUnit);
            if (j.GranAmount != 0.0)
            {
                cmd.Parameters.AddWithValue("@GranAmount", j.GranAmount);
            }
            else
            {
                cmd.Parameters.AddWithValue("@GranAmount", DBNull.Value);
            }
            cmd.Parameters.AddWithValue("@RevisedAppliedAmount", j.RevisedAppliedAmt);
            if (j.AppliedDate.ToShortDateString() != "01/01/0001")
            {
                cmd.Parameters.AddWithValue("@AppliedDate", j.AppliedDate);
            }
            else
            {
                cmd.Parameters.AddWithValue("@AppliedDate", DBNull.Value);
            }
            cmd.Parameters.AddWithValue("@GrantSource", j.GrantSource);

            cmd.Parameters.AddWithValue("@GrantType", j.GrantType);
            cmd.Parameters.AddWithValue("@comments", j.AddtionalComments);
            if (j.Contact_No != "")
            {
                cmd.Parameters.AddWithValue("@Contact_No", j.Contact_No);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Contact_No", DBNull.Value);
            }

            cmd.Parameters.AddWithValue("@Address", j.Address);
            cmd.Parameters.AddWithValue("@Pan_No", j.Pan_No);
            cmd.Parameters.AddWithValue("@State_Code", j.State);
            cmd.Parameters.AddWithValue("@Country_Code", j.Country);
            cmd.Parameters.AddWithValue("@PIInstitutionID", j.PiInstId);
            cmd.Parameters.AddWithValue("@PIDeptID", j.PiDeptId);
            cmd.Parameters.AddWithValue("@ERFRealated", j.ERFRelated);
            cmd.Parameters.AddWithValue("@Agency_Contact", j.AgencyContact);
            cmd.Parameters.AddWithValue("@Email_Id", j.AgencyEmailId);
            cmd.Parameters.AddWithValue("@SanctionOrderDate", j.SanctionOrderDate);
            //if (j.ProjectActualDate.ToShortDateString() != "01/01/0001")
            //{
            //    cmd.Parameters.AddWithValue("@ProjectActualDate", j.ProjectActualDate);
            //}
            //else
            //{
            //    cmd.Parameters.AddWithValue("@ProjectActualDate", DBNull.Value);
            //}
            if (j.DurationOfProject != 0)
            {
                cmd.Parameters.AddWithValue("@DurationOfProject", j.DurationOfProject);
            }
            else
            {
                cmd.Parameters.AddWithValue("@DurationOfProject", DBNull.Value);
            }
            if ((j.FundingSectorLevelGrant != null) && (j.FundingSectorLevelGrant != 0))
            {
                cmd.Parameters.AddWithValue("@FundingSectorLevel", j.FundingSectorLevelGrant);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FundingSectorLevel", DBNull.Value);
            }
            if ((j.TypeofAgencyGrant != null) && (j.TypeofAgencyGrant != 0))
            {
                cmd.Parameters.AddWithValue("@TypeofAgency", j.TypeofAgencyGrant);
            }
            else
            {
                cmd.Parameters.AddWithValue("@TypeofAgency", DBNull.Value);
            }
            result = cmd.ExecuteNonQuery();

            cmdString = "update Project set ProjectStatus=@Status,KindProjectStartDate=@KindStartDate  ,KindProjectCloseDate=@KindCloseDate,FinanceStatus=@FinanceStatus,SanctionType=@SanctionType,SyncFinance=@SyncFinance,SactionKindComments=@SactionKindComments where ID=@ID and ProjectUnit=@GrantUnit";
            cmd = new SqlCommand(cmdString, con, transaction);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@ID", j.GID);
            //if (j.Status == "SUB")
            //{
            //    string Role = HttpContext.Current.Session["Role"].ToString();
            //    if (Role == "1")
            //    {
            //        cmd.Parameters.AddWithValue("@Status", "SAN");
            //        cmd.Parameters.AddWithValue("@FinanceStatus", "OPE");
            //    }
            //    else
            //    {
            //        cmd.Parameters.AddWithValue("@Status", "SAN");
            //        cmd.Parameters.AddWithValue("@FinanceStatus", DBNull.Value);
            //    }

            //}
            //else
            //{
            //    cmd.Parameters.AddWithValue("@Status", j.Status);
            //}
            cmd.Parameters.AddWithValue("@Status", j.Status);
            cmd.Parameters.AddWithValue("@FinanceStatus", "OPE");
            cmd.Parameters.AddWithValue("@SanctionType", j.SancType);
            cmd.Parameters.AddWithValue("@GrantUnit", j.GrantUnit);
            if (j.SancType == "CA")
            {
                cmd.Parameters.AddWithValue("@SyncFinance", "N");
            }
            else
            {
                cmd.Parameters.AddWithValue("@SyncFinance", DBNull.Value);
            }

            if (j.SancType == "CA")
            {
                cmd.Parameters.AddWithValue("@SactionKindComments", DBNull.Value);
                cmd.Parameters.AddWithValue("@KindStartDate", DBNull.Value);
                cmd.Parameters.AddWithValue("@KindCloseDate", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@SactionKindComments", j.KindComments);
                if (j.KindStartDate.ToShortDateString() != "01/01/0001")
                {
                    cmd.Parameters.AddWithValue("@KindStartDate", j.KindStartDate);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@KindStartDate", DBNull.Value);
                }
                if (j.KindCloseDate.ToShortDateString() != "01/01/0001")
                {
                    cmd.Parameters.AddWithValue("@KindCloseDate", j.KindCloseDate);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@KindCloseDate", DBNull.Value);
                }
            }

            result = cmd.ExecuteNonQuery();

            if (j.SancType == "CA")
            {
                cmdString = "delete from  ProjectSanctionedDetails  where ID=@ID and ProjectUnit=@GrantUnit";
                cmd = new SqlCommand(cmdString, con, transaction);
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@ID", j.GID);

                cmd.Parameters.AddWithValue("@GrantType", j.GrantType);

                cmd.Parameters.AddWithValue("@GrantUnit", j.GrantUnit);
                result1 = cmd.ExecuteNonQuery();

                for (int i = 0; i < sd1.Length; i++)
                {
                    cmdString = "insert into ProjectSanctionedDetails(ProjectUnit, ID,SanctionEntryNo,SanctionNumber,SanctionDate,SanctionTotalAmount,SanctionCapitalAmount,SanctionOperatingAmount) values (@ProjectUnit, @ID,@EntryNum,@SanctionNumber,@SanctionDate,@SanctionTotalAmount,@SanctionCapitalAmount,@SanctionOperatingAmount)";
                    cmd = new SqlCommand(cmdString, con, transaction);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@ID", j.GID);
                    cmd.Parameters.AddWithValue("@EntryNum", i + 1);
                    cmd.Parameters.AddWithValue("@ProjectType", j.GrantType);
                    cmd.Parameters.AddWithValue("@ProjectUnit", j.GrantUnit);
                    if (sd1[i].SanctionNumber != null)
                    {
                        cmd.Parameters.AddWithValue("@SanctionNumber", sd1[i].SanctionNumber);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@SanctionNumber", DBNull.Value);
                    }
                    if (sd1[i].SanctionDate.ToShortDateString() != "01/01/0001")
                    {

                        cmd.Parameters.AddWithValue("@SanctionDate", sd1[i].SanctionDate);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@SanctionDate", DBNull.Value);
                    }

                    if (sd1[i].SanctionTotalAmount != 0.0)
                    {
                        cmd.Parameters.AddWithValue("@SanctionTotalAmount", sd1[i].SanctionTotalAmount);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@SanctionTotalAmount", DBNull.Value);
                    }
                    if (sd1[i].SanctionCapitalAmount != 0.0)
                    {
                        cmd.Parameters.AddWithValue("@SanctionCapitalAmount", sd1[i].SanctionCapitalAmount);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@SanctionCapitalAmount", DBNull.Value);
                    }
                    if (sd1[i].SanctionOperatingAmount != 0.0)
                    {
                        cmd.Parameters.AddWithValue("@SanctionOperatingAmount", sd1[i].SanctionOperatingAmount);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@SanctionOperatingAmount", DBNull.Value);
                    }
                    result2 = cmd.ExecuteNonQuery();
                    log.Info("Sanction detail inserted  of Project Unit: " + j.GrantUnit + "ID: " + j.GID);
                }


                cmdString = "Update  Project set NoOfSanction=@SanctionEntryNumber,ProjectCommencementDate=@ProjectCommencementDate,ProjectCloseDate=@ProjectCloseDate,ExtendedDate=@ExtendedDate,AuditRequired=@AuditRequired,ServiceTaxAppl=@ServiceTaxApplicable,InstitutionShare=@InstitutionShare,AccountHead=@AccountHead where ID=@ID and ProjectUnit=@ProjectUnit";
                cmd = new SqlCommand(cmdString, con, transaction);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@ID", j.GID);
                cmd.Parameters.AddWithValue("@ProjectUnit", j.GrantUnit);

                string date3 = j.ProjectCommencementDate.ToShortDateString();
                if (date3 == "01/01/0001")
                {
                    cmd.Parameters.AddWithValue("@ProjectCommencementDate", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@ProjectCommencementDate", j.ProjectCommencementDate);
                }
                string date2 = j.ProjectCloseDate.ToShortDateString();
                if (date2 == "01/01/0001")
                {
                    cmd.Parameters.AddWithValue("@ProjectCloseDate", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@ProjectCloseDate", j.ProjectCloseDate);
                }

                string date1 = j.ExtendedDate.ToShortDateString();
                if (date1 == "01/01/0001")
                {
                    cmd.Parameters.AddWithValue("@ExtendedDate", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@ExtendedDate", j.ExtendedDate);
                }

                if (j.AuditRequired != "0")
                {

                    cmd.Parameters.AddWithValue("@AuditRequired", j.AuditRequired);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@AuditRequired", DBNull.Value);
                }

                if (j.ServiceTaxApplicable != "select")
                {

                    cmd.Parameters.AddWithValue("@ServiceTaxApplicable", j.ServiceTaxApplicable);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@ServiceTaxApplicable", DBNull.Value);
                }
                if (j.AccountHead != "")
                {

                    cmd.Parameters.AddWithValue("@AccountHead", j.AccountHead);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@AccountHead", DBNull.Value);
                }
                if (j.InstitutionSahre != 0.0)
                {

                    cmd.Parameters.AddWithValue("@InstitutionShare", j.InstitutionSahre);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@InstitutionShare", DBNull.Value);
                }
                if (j.SanctionEntryNumber != 0)
                {

                    cmd.Parameters.AddWithValue("@SanctionEntryNumber", j.SanctionEntryNumber);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@SanctionEntryNumber", DBNull.Value);
                }
                result2 = cmd.ExecuteNonQuery();
                log.Info("Grant details updated  of Project Unit: " + j.GrantUnit + "ID: " + j.GID);
                log.Info("Grant Entry : User Name :" + HttpContext.Current.Session["UserName"] + "Role :" + HttpContext.Current.Session["RoleName"]); 
            }
            else
            {
                cmdString = "delete from  ProjectKindDetails  where ID=@ID and ProjectUnit=@ProjectUnit";

                cmd = new SqlCommand(cmdString, con, transaction);
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@ID", j.GID);
                cmd.Parameters.AddWithValue("@ProjectUnit", j.GrantUnit);

                result1 = cmd.ExecuteNonQuery();
                for (int i = 0; i < jd1.Length; i++)
                {
                    cmdString = "insert into ProjectKindDetails(ProjectUnit, ID,EntryNo,ReceivedDate,Details,INREquivalent) values (@ProjectUnit, @ID,@EntryNum,@ReceivedDate,@Details,@INREquivalent)";

                    cmd = new SqlCommand(cmdString, con, transaction);
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue("@ID", j.GID);

                    cmd.Parameters.AddWithValue("@EntryNum", i + 1);
                    cmd.Parameters.AddWithValue("@ProjectUnit", j.GrantUnit);
                    cmd.Parameters.AddWithValue("@ReceivedDate", jd1[i].RecievedDate);
                    cmd.Parameters.AddWithValue("@Details", jd1[i].details);
                    cmd.Parameters.AddWithValue("@INREquivalent", jd1[i].INREquivalent);
                    result2 = cmd.ExecuteNonQuery();
                    log.Info("Kind details inserted  of Project Unit: " + j.GrantUnit + "ID: " + j.GID);

                }

            }

            cmdString = "Select count(* ) as Count from ProjectStatusTracker where  ID=@ID and ProjectUnit=@ProjectUnit";
            cmd = new SqlCommand(cmdString, con, transaction);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@ID", j.GID);
            cmd.Parameters.AddWithValue("@GrantType", j.GrantType);
            cmd.Parameters.AddWithValue("@ProjectUnit", j.GrantUnit);
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

            cmd = new SqlCommand("InsertProjectReviewTracker", con, transaction);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID", j.GID);
            cmd.Parameters.AddWithValue("@ReviewNo", count + 1);
            cmd.Parameters.AddWithValue("@ApprovedStatus", j.Status);
            cmd.Parameters.AddWithValue("@Remark", j.AddtionalComments);
            cmd.Parameters.AddWithValue("@GrantUnit", j.GrantUnit);
            cmd.Parameters.AddWithValue("@UpdateUser", HttpContext.Current.Session["UserId"].ToString());
            cmd.Parameters.AddWithValue("@Date", DateTime.Now);
            result3 = cmd.ExecuteNonQuery();
            transaction.Commit();
            log.Info("Updated Project Review tracker of Project Unit: " + j.GrantUnit + "ID: " + j.GID);
            return result3;

        }
        catch (Exception ex)
        {
            log.Error("Inside UpdateStatusGrantEntryAcceptApproval catch block of Project Unit: " + j.GrantUnit + "ID: " + j.GID);
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



    public int UpdateStatusGrantEntryRejectApproval(GrantData j, GrantData[] jd)
    {
        log.Debug("Inside UpdateStatusGrantEntryRejectApproval of Project Unit: " + j.GrantUnit + "ID: " + j.GID);
        int result = 0;
        con = new SqlConnection(str);
        con.Open();
        transaction = con.BeginTransaction();
        try
        {
            cmdString = "update Project set ProjectStatus=@Status,Comments=@Comments, RejectedRemarks=@Remarks,RejectedDate=@RejectedDate,RejectedBy=@RejectedBy where ID=@ID and ProjectUnit=@GrantUnit";
            cmd = new SqlCommand(cmdString, con, transaction);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@ID", j.GID);
            cmd.Parameters.AddWithValue("@Status", j.Status);
            cmd.Parameters.AddWithValue("@Remarks", j.RejectFeedback);
            cmd.Parameters.AddWithValue("@Comments", j.AddtionalComments);
            cmd.Parameters.AddWithValue("@RejectedDate", DateTime.Now);
            cmd.Parameters.AddWithValue("@RejectedBy", j.RejectBy);
            cmd.Parameters.AddWithValue("@GrantUnit", j.GrantUnit);
            result = cmd.ExecuteNonQuery();
            log.Info("Status  of Project Unit: " + j.GrantUnit + "ID: " + j.GID + " updated to : " + j.Status);

            cmdString = "Select count(* ) as Count from ProjectStatusTracker where  ID=@ID and ProjectUnit=@GrantUnit";
            cmd = new SqlCommand(cmdString, con, transaction);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@ID", j.GID);
            cmd.Parameters.AddWithValue("@GrantUnit", j.GrantUnit);
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

            cmd = new SqlCommand("InsertProjectReviewTracker", con, transaction);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID", j.GID);
            cmd.Parameters.AddWithValue("@GrantUnit", j.GrantUnit);
            cmd.Parameters.AddWithValue("@ReviewNo", count + 1);
            cmd.Parameters.AddWithValue("@ApprovedStatus", j.Status);
            cmd.Parameters.AddWithValue("@Remark", j.AddtionalComments);
            cmd.Parameters.AddWithValue("@UpdateUser", j.CreatedBy);
            cmd.Parameters.AddWithValue("@Date", DateTime.Now);
            result = cmd.ExecuteNonQuery();
            transaction.Commit();
            log.Info("Updated Project Review tracker of Project Unit: " + j.GrantUnit + "ID: " + j.GID);
            return result;
        }

        catch (Exception ex)
        {
            log.Error("UpdateStatusGrantEntryRejectApproval catch block of Project Unit: " + j.GrantUnit + "ID: " + j.GID);
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


    public bool UpdateStatusReworkGrantEntry(GrantData grant)
    {
        log.Debug("Inside UpdateStatusReworkGrantEntry of Project Unit: " + grant.GrantUnit + "ID: " + grant.GID);
        bool result = false;
        con = new SqlConnection(str);
        con.Open();
        transaction = con.BeginTransaction();
        try
        {
            if (grant.Status == "REW")
            {
                log.Debug("Inside UpdateStatusReworkGrantEntry to update the status to :" + grant.Status + " Project Unit: " + grant.GrantUnit + "ID: " + grant.GID);
                cmdString = "update Project set ProjectStatus=@Status,ReworkRemarks=@ReworkRemarks where ID=@ID and ProjectUnit=@GrantUnit";
                cmd = new SqlCommand(cmdString, con, transaction);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@ID", grant.GID);
                cmd.Parameters.AddWithValue("@Status", grant.Status);
                cmd.Parameters.AddWithValue("@GrantUnit", grant.GrantUnit);
                cmd.Parameters.AddWithValue("@ReworkRemarks", grant.Remarks);
                result = Convert.ToBoolean(cmd.ExecuteNonQuery());
                log.Info(" Project Unit: " + grant.GrantUnit + " and ID: " + grant.GID + " status changed to :" + grant.Status);
                log.Info("Grant Rework : User Name :" + HttpContext.Current.Session["UserName"] + "Role :" + HttpContext.Current.Session["RoleName"]);

            }
            else if (grant.Status == "SAN")
            {
                log.Debug("Inside UpdateStatusReworkGrantEntry to update the status to :" + grant.Status + " Project Unit: " + grant.GrantUnit + "ID: " + grant.GID);
                cmdString = "update Project set ProjectStatus=@Status, FinanceStatus=@FinanceStatus ,ApprovedBy=@ApprovedBy,ApprovedDate=@ApprovedDate,NoOfSanction=@NoOfSanction where ID=@ID and ProjectUnit=@GrantUnit";
                cmd = new SqlCommand(cmdString, con, transaction);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@ID", grant.GID);
                cmd.Parameters.AddWithValue("@Status", grant.Status);
                cmd.Parameters.AddWithValue("@FinanceStatus", grant.FinanceProjectStatus);
                cmd.Parameters.AddWithValue("@GrantUnit", grant.GrantUnit);
                cmd.Parameters.AddWithValue("@ApprovedBy", HttpContext.Current.Session["UserId"]);
                cmd.Parameters.AddWithValue("@ApprovedDate", DateTime.Now);
                cmd.Parameters.AddWithValue("@NoOfSanction", 1);
                result = Convert.ToBoolean(cmd.ExecuteNonQuery());
                log.Info(" Project Unit: " + grant.GrantUnit + " and ID: " + grant.GID + " status changed to :" + grant.Status);
                log.Info("Grant Approval : User Name :" + HttpContext.Current.Session["UserName"] + "Role :" + HttpContext.Current.Session["RoleName"]);

            }

            cmdString = "Select count(*) as Count from ProjectStatusTracker where  ID=@ID and ProjectUnit=@GrantUnit";
            cmd = new SqlCommand(cmdString, con, transaction);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@ID", grant.GID);
            cmd.Parameters.AddWithValue("@GrantUnit", grant.GrantUnit);
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

            cmd = new SqlCommand("InsertProjectReviewTracker", con, transaction);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID", grant.GID);
            cmd.Parameters.AddWithValue("@GrantUnit", grant.GrantUnit);
            cmd.Parameters.AddWithValue("@ReviewNo", count + 1);
            cmd.Parameters.AddWithValue("@ApprovedStatus", grant.Status);
            cmd.Parameters.AddWithValue("@Remark", grant.Remarks);
            cmd.Parameters.AddWithValue("@UpdateUser", HttpContext.Current.Session["UserId"].ToString());
            cmd.Parameters.AddWithValue("@Date", DateTime.Now);
            result = Convert.ToBoolean(cmd.ExecuteNonQuery());
            log.Info("Updated Project Review tracker of Project Unit: " + grant.GrantUnit + "ID: " + grant.GID);
            transaction.Commit();
            return result;
        }

        catch (Exception ex)
        {
            log.Error(" Inside UpdateStatusReworkGrantEntry of Project Unit: " + grant.GrantUnit + "ID: " + grant.GID);
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

    //Update Sanction details(Finance User)
    public int UpdateSanctionDetails(GrantData j, GrantData[] sd1, GrantData[] SD2)
    {
        log.Debug("Inside UpdateSanctionDetails of Project Unit: " + j.GrantUnit + "ID: " + j.GID);
        int result1 = 0, result2 = 0, result3 = 0;
        con = new SqlConnection(str);
        con.Open();
        transaction = con.BeginTransaction();
        try
        {

            cmdString = "delete from  ProjectSanctionedDetails  where ID=@ID and ProjectUnit=@GrantUnit";
            cmd = new SqlCommand(cmdString, con, transaction);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@ID", j.GID);
            cmd.Parameters.AddWithValue("@GrantUnit", j.GrantUnit);
            result1 = cmd.ExecuteNonQuery();

            cmdString = "delete from SanctionOPAmountDetails where ProjectUnit=@ProjectUnit and ID=@ID";
            cmd = new SqlCommand(cmdString, con, transaction);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@ID", j.GID);
            cmd.Parameters.AddWithValue("@ProjectUnit", j.GrantUnit);
            cmd.ExecuteNonQuery();

            cmdString = "Update  Project set SanctionCapitalAmount=@SanctionCapitalAmount,SanctionOperatingAmount=@SanctionOperatingAmount,SanctionTotalAmount=@SanctionTotalAmount,ProjectCommencementDate=@ProjectCommencementDate,ProjectCloseDate=@ProjectCloseDate,ExtendedDate=@ExtendedDate,AuditRequired=@AuditRequired,ServiceTaxAppl=@ServiceTaxApplicable,InstitutionShare=@InstitutionShare,AccountHead=@AccountHead,NoOfSanction=@SanctionEntryNumber where ID=@ID and ProjectUnit=@ProjectUnit";
            cmd = new SqlCommand(cmdString, con, transaction);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@ID", j.GID);
            cmd.Parameters.AddWithValue("@ProjectUnit", j.GrantUnit);
            cmd.Parameters.AddWithValue("@SanctionEntryNumber", j.SanctionEntryNumber);
            if (j.SanctionCapitalAmount != null)
            {

                cmd.Parameters.AddWithValue("@SanctionCapitalAmount", j.SanctionCapitalAmount);
            }
            else
            {
                cmd.Parameters.AddWithValue("@SanctionCapitalAmount", DBNull.Value);
            }
            if (j.SanctionOperatingAmount != null)
            {
                cmd.Parameters.AddWithValue("@SanctionOperatingAmount", j.SanctionOperatingAmount);
            }
            else
            {
                cmd.Parameters.AddWithValue("@SanctionCapitalAmount", DBNull.Value);
            }

            cmd.Parameters.AddWithValue("@SanctionTotalAmount", j.SanctionTotalAmount);

            string date3 = j.ProjectCommencementDate.ToShortDateString();
            if (date3 == "01/01/0001")
            {
                cmd.Parameters.AddWithValue("@ProjectCommencementDate", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@ProjectCommencementDate", j.ProjectCommencementDate);
            }
            string date2 = j.ProjectCloseDate.ToShortDateString();
            if (date2 == "01/01/0001")
            {
                cmd.Parameters.AddWithValue("@ProjectCloseDate", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@ProjectCloseDate", j.ProjectCloseDate);
            }

            string date1 = j.ExtendedDate.ToShortDateString();
            if (date1 == "01/01/0001")
            {
                cmd.Parameters.AddWithValue("@ExtendedDate", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@ExtendedDate", j.ExtendedDate);
            }

            if (j.AuditRequired != "0")
            {

                cmd.Parameters.AddWithValue("@AuditRequired", j.AuditRequired);
            }
            else
            {
                cmd.Parameters.AddWithValue("@AuditRequired", DBNull.Value);
            }

            if (j.ServiceTaxApplicable != "select")
            {

                cmd.Parameters.AddWithValue("@ServiceTaxApplicable", j.ServiceTaxApplicable);
            }
            else
            {
                cmd.Parameters.AddWithValue("@ServiceTaxApplicable", DBNull.Value);
            }
            if (j.AccountHead != "")
            {

                cmd.Parameters.AddWithValue("@AccountHead", j.AccountHead);
            }
            else
            {
                cmd.Parameters.AddWithValue("@AccountHead", DBNull.Value);
            }
            if (j.InstitutionSahre != 0.0)
            {

                cmd.Parameters.AddWithValue("@InstitutionShare", j.InstitutionSahre);
            }
            else
            {
                cmd.Parameters.AddWithValue("@InstitutionShare", DBNull.Value);
            }

            result2 = cmd.ExecuteNonQuery();

            for (int i = 0; i < sd1.Length; i++)
            {
                cmdString = "insert into ProjectSanctionedDetails(ProjectUnit, ID,SanctionEntryNo,SanctionNumber,SanctionDate,SanctionTotalAmount,SanctionCapitalAmount,SanctionOperatingAmount,Narration) values (@ProjectUnit, @ID,@EntryNum,@SanctionNumber,@SanctionDate,@SanctionTotalAmount,@SanctionCapitalAmount,@SanctionOperatingAmount,@SanctionNarration)";
                cmd = new SqlCommand(cmdString, con, transaction);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@ID", j.GID);
                cmd.Parameters.AddWithValue("@EntryNum", i + 1);
                cmd.Parameters.AddWithValue("@ProjectUnit", j.GrantUnit);
                if (sd1[i].SanctionNumber != null)
                {
                    cmd.Parameters.AddWithValue("@SanctionNumber", sd1[i].SanctionNumber);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@SanctionNumber", DBNull.Value);
                }
                if (sd1[i].SanctionDate.ToShortDateString() != "01/01/0001")
                {

                    cmd.Parameters.AddWithValue("@SanctionDate", sd1[i].SanctionDate);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@SanctionDate", DBNull.Value);
                }

                if (sd1[i].SanctionTotalAmount != 0.0)
                {
                    cmd.Parameters.AddWithValue("@SanctionTotalAmount", sd1[i].SanctionTotalAmount);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@SanctionTotalAmount", DBNull.Value);
                }
                if (sd1[i].SanctionCapitalAmount != 0.0)
                {
                    cmd.Parameters.AddWithValue("@SanctionCapitalAmount", sd1[i].SanctionCapitalAmount);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@SanctionCapitalAmount", DBNull.Value);
                }
                if (sd1[i].SanctionOperatingAmount != 0.0)
                {
                    cmd.Parameters.AddWithValue("@SanctionOperatingAmount", sd1[i].SanctionOperatingAmount);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@SanctionOperatingAmount", DBNull.Value);
                }

                if (sd1[i].SanctionNarration != "")
                {
                    cmd.Parameters.AddWithValue("@SanctionNarration", sd1[i].SanctionNarration);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@SanctionNarration", DBNull.Value);
                }
                result2 = cmd.ExecuteNonQuery();

                DataTable miscrow = (DataTable)HttpContext.Current.Session["MiscRow" + i];
                if (miscrow != null)
                {
                    for (int k = 0; k < miscrow.Rows.Count; k++)
                    {
                        cmd = new SqlCommand("InsertSanctionOPAmountDetails", con, transaction);
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ID", j.GID);
                        cmd.Parameters.AddWithValue("@ProjectUnit", j.GrantUnit);
                        cmd.Parameters.AddWithValue("@GrantLine", i + 1);
                        cmd.Parameters.AddWithValue("@OperatingItemId", miscrow.Rows[k].ItemArray[4].ToString());
                        cmd.Parameters.AddWithValue("@Amount", miscrow.Rows[k].ItemArray[5].ToString());
                        result3 = cmd.ExecuteNonQuery();

                    }
                }

            }
            log.Info("Sanction details inserted successfully of Project Unit: " + j.GrantUnit + "ID: " + j.GID);
            log.Info("Sanction operating amount details inserted successfully of Project Unit: " + j.GrantUnit + "ID: " + j.GID);

            //cmdString = "Select count(* ) as Count from ProjectStatusTracker where  ID=@ID and ProjectUnit=@ProjectUnit";
            //cmd = new SqlCommand(cmdString, con, transaction);
            //cmd.CommandType = CommandType.Text;
            //cmd.Parameters.AddWithValue("@ID", j.GID);
            //cmd.Parameters.AddWithValue("@ProjectUnit", j.GrantUnit);
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

            //cmd = new SqlCommand("InsertProjectReviewTracker", con, transaction);
            //cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.AddWithValue("@ID", j.GID);
            //cmd.Parameters.AddWithValue("@ReviewNo", count + 1);
            //cmd.Parameters.AddWithValue("@ApprovedStatus", j.Status);
            //cmd.Parameters.AddWithValue("@Remark", j.AddtionalComments);
            //cmd.Parameters.AddWithValue("@GrantUnit", j.GrantUnit);
            //cmd.Parameters.AddWithValue("@UpdateUser", HttpContext.Current.Session["UserId"]);
            //cmd.Parameters.AddWithValue("@Date", DateTime.Now);
            //result3 = cmd.ExecuteNonQuery();
            transaction.Commit();
            return result3;
        }

        catch (Exception ex)
        {
            log.Error("UpdateSanctionDetails catch block of Project Unit: " + j.GrantUnit + "ID: " + j.GID);
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


    public int InsertRecieptDetails(GrantData j, RecieptData[] JD)
    {
        log.Debug("Inside InsertRecieptDetails function of Project Unit: " + j.GrantUnit + "ID: " + j.GID);
        con = new SqlConnection(str);
        con.Open();
        transaction = con.BeginTransaction();
        int result = 0;
        try
        {
            cmdString = "delete from ProjectReceiptDetails where ProjectUnit=@ProjectUnit and ID=@ID";
            cmd = new SqlCommand(cmdString, con, transaction);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@ID", j.GID);
            cmd.Parameters.AddWithValue("@ProjectUnit", j.GrantUnit);
            cmd.ExecuteNonQuery();

            for (int i = 0; i < JD.Length; i++)
            {
                cmd = new SqlCommand("InsertReceivedetails", con, transaction);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProjectUnit", j.GrantUnit);
                cmd.Parameters.AddWithValue("@ID", j.GID);
                cmd.Parameters.AddWithValue("@GrantLine", i + 1);
                cmd.Parameters.AddWithValue("@Sanction_Entry_Number ", JD[i].FRSanctionEntryNo);
                cmd.Parameters.AddWithValue("@CurrencyCode", JD[i].CurrencyCode);
                cmd.Parameters.AddWithValue("@ModeOfReceive", JD[i].ModeOfReceive);
                cmd.Parameters.AddWithValue("@ReceviedDate", JD[i].ReceviedDate);
                cmd.Parameters.AddWithValue("@ReceviedAmmount", JD[i].ReceviedAmmount);
                if (JD[i].ReceviedINR != 0.0)
                {
                    cmd.Parameters.AddWithValue("@ReceviedINR", JD[i].ReceviedINR);

                }
                else
                {
                    cmd.Parameters.AddWithValue("@ReceviedINR", DBNull.Value);
                }
                if (JD[i].ReceivedNarration != "")
                {
                    cmd.Parameters.AddWithValue("@ReceivedNarration", JD[i].ReceivedNarration);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@ReceivedNarration", DBNull.Value);
                }
                if (JD[i].TDS != 0.0)
                {
                    cmd.Parameters.AddWithValue("@TDS", JD[i].TDS);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@TDS", DBNull.Value);
                }
                if (JD[i].ReferenceNumber != "")
                {
                    cmd.Parameters.AddWithValue("@ReferenceNumber ", JD[i].ReferenceNumber);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@ReferenceNumber", DBNull.Value);
                }
                //cmd.Parameters.AddWithValue("@ReceivedBank ", JD[i].ReceivedBank);
                if (JD[i].CreditedBank != "")
                {
                    cmd.Parameters.AddWithValue("@CreditedBank ", JD[i].CreditedBank);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@CreditedBank", DBNull.Value);
                }
                cmd.Parameters.AddWithValue("@UserId", HttpContext.Current.Session["UserId"]);
                cmd.Parameters.AddWithValue("@Datem", DateTime.Now);
                result = cmd.ExecuteNonQuery();

            }
            transaction.Commit();
            log.Debug("Fund details inserted successfully of Project Unit: " + j.GrantUnit + "ID: " + j.GID);
            return result;

        }
        catch (Exception ex)
        {
            log.Error("Inside InsertRecieptDetails catch block of Project Unit: " + j.GrantUnit + "ID: " + j.GID);
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

    public int InsertIncentiveDetails(GrantData j, IncentiveData[] JD3, IncentiveData[] JD4)
    {
        log.Debug("Inside InsertIncentiveDetails function of Project Unit: " + j.GrantUnit + "ID: " + j.GID);
        con = new SqlConnection(str);
        con.Open();
        transaction = con.BeginTransaction();
        int result = 0;
        try
        {
            cmdString = "delete from ProjectIncentiveDetails where ProjectUnit=@ProjectUnit and ID=@ID";
            cmd = new SqlCommand(cmdString, con, transaction);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("@ID", j.GID);
            cmd.Parameters.AddWithValue("@ProjectUnit", j.GrantUnit);
            cmd.ExecuteNonQuery();

            cmdString = "delete from ProjectIncentivePayDetails where ProjectUnit=@ProjectUnit and ID=@ID";
            cmd = new SqlCommand(cmdString, con, transaction);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("@ID", j.GID);
            cmd.Parameters.AddWithValue("@ProjectUnit", j.GrantUnit);
            cmd.ExecuteNonQuery();
            for (int i = 0; i < JD3.Length; i++)
            {
                cmd = new SqlCommand("InsertIncentiveDetails", con, transaction);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ID", j.GID);
                cmd.Parameters.AddWithValue("@ProjectUnit", j.GrantUnit);
                cmd.Parameters.AddWithValue("@GrantLine", i + 1);
                cmd.Parameters.AddWithValue("@SanctionEntryNo", JD3[i].SanctionEntryNo);
                cmd.Parameters.AddWithValue("@IncentivePayDate", JD3[i].IncentivePayDate);
                cmd.Parameters.AddWithValue("@IncentivePayAmount", JD3[i].IncentivePayAmount);
                cmd.Parameters.AddWithValue("@Narration", JD3[i].Narration);
                //cmd.Parameters.AddWithValue("@UserId", HttpContext.Current.Session["UserId"]);
                cmd.Parameters.AddWithValue("@Datem", DateTime.Now);
                result = cmd.ExecuteNonQuery();

                DataTable miscrow = (DataTable)HttpContext.Current.Session["MiscRowIncentive" + i];
                if (miscrow != null)
                {
                    for (int k = 0; k < miscrow.Rows.Count; k++)
                    {
                        cmd = new SqlCommand("InsertIncentivePayDetails", con, transaction);
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ID", j.GID);
                        cmd.Parameters.AddWithValue("@ProjectUnit", j.GrantUnit);
                        cmd.Parameters.AddWithValue("@GrantLine", i + 1);
                        cmd.Parameters.AddWithValue("@EntryNo", miscrow.Rows[k].ItemArray[2].ToString());
                        // cmd.Parameters.AddWithValue("@SanctionEntryNo", miscrow.Rows[k].ItemArray[5].ToString());
                        cmd.Parameters.AddWithValue("@PayedTo", miscrow.Rows[k].ItemArray[3].ToString());
                        cmd.Parameters.AddWithValue("@Amount", miscrow.Rows[k].ItemArray[4].ToString());
                        cmd.Parameters.AddWithValue("@InstitutionId", miscrow.Rows[k].ItemArray[6].ToString());
                        cmd.Parameters.AddWithValue("@DeptId", miscrow.Rows[k].ItemArray[7].ToString());
                        cmd.ExecuteNonQuery();

                    }

                }
            }

            log.Debug("Incentive details inserted successfully of Project Unit: " + j.GrantUnit + "ID: " + j.GID);
            transaction.Commit();
            return result;

        }
        catch (Exception ex)
        {
            log.Error("Inside InsertIncentiveDetails  catch block of Project Unit: " + j.GrantUnit + "ID: " + j.GID);
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


    public int InsertOverheadDetails(GrantData j, GrantData[] JD3)
    {
        log.Debug("Inside InsertOverheadDetails function of Project Unit: " + j.GrantUnit + "ID: " + j.GID);
        con = new SqlConnection(str);
        con.Open();
        transaction = con.BeginTransaction();
        int result = 0;
        try
        {
            cmdString = "delete from ProjectOverheadTDetails where ProjectUnit=@ProjectUnit and ID=@ID";
            cmd = new SqlCommand(cmdString, con, transaction);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("@ID", j.GID);
            cmd.Parameters.AddWithValue("@ProjectUnit", j.GrantUnit);
            cmd.ExecuteNonQuery();
            for (int i = 0; i < JD3.Length; i++)
            {
                cmd = new SqlCommand("InsertOverheadT", con, transaction);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@ID", j.GID);
                cmd.Parameters.AddWithValue("@ProjectUnit", j.GrantUnit);
                cmd.Parameters.AddWithValue("@GrantLine", i + 1);
                cmd.Parameters.AddWithValue("@SanctionEntryNo", JD3[i].OHSanctionEntryNo);
                cmd.Parameters.AddWithValue("@JVNumber", JD3[i].JVNumber);
                cmd.Parameters.AddWithValue("@OverheadTDate", JD3[i].OverheadTDate);
                cmd.Parameters.AddWithValue("@OverheadTAmount", JD3[i].OverheadTAmount);
                cmd.Parameters.AddWithValue("@Narration", JD3[i].OverheadNarration);
                result = cmd.ExecuteNonQuery();

            }
            log.Debug("Overhead details inserted sucessfully of Project Unit: " + j.GrantUnit + "ID: " + j.GID);
            transaction.Commit();
            return result;
        }
        catch (Exception ex)
        {
            log.Error("Inside InsertOverheadDetails of Project Unit: " + j.GrantUnit + "ID: " + j.GID);
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

    public int UpdateFinanceStatus(GrantData j)
    {
        log.Debug("Inside UpdateFinanceStatus function of Project Unit: " + j.GrantUnit + "ID: " + j.GID);
        int result2 = 0;
        con = new SqlConnection(str);
        con.Open();
        transaction = con.BeginTransaction();
        try
        {

            cmdString = "Update  Project set FinanceStatus=@FinanceProjectStatus,Remarks=@Remarks,CompletionDate=@DateOfCompletion where ID=@ID and ProjectUnit=@ProjectUnit";
            cmd = new SqlCommand(cmdString, con, transaction);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@ID", j.GID);
            cmd.Parameters.AddWithValue("@ProjectUnit", j.GrantUnit);
            cmd.Parameters.AddWithValue("@FinanceProjectStatus", j.FinanceProjectStatus);
            cmd.Parameters.AddWithValue("@Remarks", j.Remarks);
            string date3 = j.DateOfCompletion.ToShortDateString();
            if (date3 == "01/01/0001")
            {
                cmd.Parameters.AddWithValue("@DateOfCompletion", DBNull.Value);
            }
            else
            {
                cmd.Parameters.AddWithValue("@DateOfCompletion", j.DateOfCompletion);
            }

            result2 = cmd.ExecuteNonQuery();

            //cmdString = "Select count(* ) as Count from ProjectStatusTracker where  ID=@ID and ProjectUnit=@ProjectUnit";
            //cmd = new SqlCommand(cmdString, con, transaction);
            //cmd.CommandType = CommandType.Text;
            //cmd.Parameters.AddWithValue("@ID", j.GID);
            //cmd.Parameters.AddWithValue("@ProjectUnit", j.GrantUnit);
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

            //cmd = new SqlCommand("InsertProjectReviewTracker", con, transaction);
            //cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.AddWithValue("@ID", j.GID);
            //cmd.Parameters.AddWithValue("@ReviewNo", count + 1);
            //cmd.Parameters.AddWithValue("@ApprovedStatus", j.Status);
            //cmd.Parameters.AddWithValue("@Remark", j.AddtionalComments);
            //cmd.Parameters.AddWithValue("@GrantUnit", j.GrantUnit);
            //cmd.Parameters.AddWithValue("@UpdateUser", HttpContext.Current.Session["UserId"]);
            //cmd.Parameters.AddWithValue("@Date", DateTime.Now);
            //result3 = cmd.ExecuteNonQuery();
            log.Debug("Finance status of Project Unit: " + j.GrantUnit + "ID: " + j.GID + "is changed to : " + j.FinanceProjectStatus);
            transaction.Commit();
            return result2;
        }

        catch (Exception ex)
        {
            log.Error("UpdateFinanceStatus catch block of Project Unit: " + j.GrantUnit + "ID: " + j.GID);
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


    public int UpdateSanctinedGrantEntry(GrantData j, GrantData[] jd1)
    {
        log.Debug("Inside UpdateSanctinedGrantEntry function of Project Unit: " + j.GrantUnit + "ID: " + j.GID);
        int result = 0, result1 = 0;
        con = new SqlConnection(str);
        con.Open();
        transaction = con.BeginTransaction();
        try
        {
            cmd = new SqlCommand("UpdateProjectEntry", con, transaction);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID", j.GID);
            cmd.Parameters.AddWithValue("@Title", j.Title);
            cmd.Parameters.AddWithValue("@Description", j.Description);
            cmd.Parameters.AddWithValue("@GrantingAgency", j.GrantingAgency);
            cmd.Parameters.AddWithValue("@GrantUnit", j.GrantUnit);
            if (j.GranAmount != 0.0)
            {

                cmd.Parameters.AddWithValue("@GranAmount", j.GranAmount);
            }
            else
            {
                cmd.Parameters.AddWithValue("@GranAmount", DBNull.Value);
            }
            if (j.AppliedDate.ToShortDateString() != "01/01/0001")
            {
                cmd.Parameters.AddWithValue("@AppliedDate", j.AppliedDate);
            }
            else
            {
                cmd.Parameters.AddWithValue("@AppliedDate", DBNull.Value);
            }
            cmd.Parameters.AddWithValue("@GrantSource", j.GrantSource);

            cmd.Parameters.AddWithValue("@GrantType", j.GrantType);
            cmd.Parameters.AddWithValue("@comments", j.AddtionalComments);
            if (j.Contact_No != "")
            {
                cmd.Parameters.AddWithValue("@Contact_No", j.Contact_No);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Contact_No", DBNull.Value);
            }

            cmd.Parameters.AddWithValue("@Address", j.Address);
            cmd.Parameters.AddWithValue("@Pan_No", j.Pan_No);
            cmd.Parameters.AddWithValue("@State_Code", j.State);
            cmd.Parameters.AddWithValue("@Country_Code", j.Country);
            cmd.Parameters.AddWithValue("@PIInstitutionID", j.PiInstId);
            cmd.Parameters.AddWithValue("@PIDeptID", j.PiDeptId);
            cmd.Parameters.AddWithValue("@ERFRealated", j.ERFRelated);
            cmd.Parameters.AddWithValue("@Agency_Contact", j.AgencyContact);
            cmd.Parameters.AddWithValue("@Email_Id", j.AgencyEmailId);
            //if (j.ProjectActualDate.ToShortDateString() != "01/01/0001")
            //{
            //    cmd.Parameters.AddWithValue("@ProjectActualDate", j.ProjectActualDate);
            //}
            //else
            //{
            //    cmd.Parameters.AddWithValue("@ProjectActualDate", DBNull.Value);
            //}
            if (j.DurationOfProject != 0)
            {
                cmd.Parameters.AddWithValue("@DurationOfProject", j.DurationOfProject);
            }
            else
            {
                cmd.Parameters.AddWithValue("@DurationOfProject", DBNull.Value);
            }
            result = cmd.ExecuteNonQuery();
            if (j.SancType == "KI")
            {
                cmdString = "delete from  ProjectKindDetails  where ID=@ID and ProjectUnit=@ProjectUnit";

                cmd = new SqlCommand(cmdString, con, transaction);
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@ID", j.GID);
                cmd.Parameters.AddWithValue("@ProjectUnit", j.GrantUnit);

                result1 = cmd.ExecuteNonQuery();
                for (int i = 0; i < jd1.Length; i++)
                {
                    cmdString = "insert into ProjectKindDetails(ProjectUnit, ID,EntryNo,ReceivedDate,Details,INREquivalent) values (@ProjectUnit, @ID,@EntryNum,@ReceivedDate,@Details,@INREquivalent)";

                    cmd = new SqlCommand(cmdString, con, transaction);
                    cmd.CommandType = CommandType.Text;

                    cmd.Parameters.AddWithValue("@ID", j.GID);

                    cmd.Parameters.AddWithValue("@EntryNum", i + 1);
                    cmd.Parameters.AddWithValue("@ProjectUnit", j.GrantUnit);
                    cmd.Parameters.AddWithValue("@ReceivedDate", jd1[i].RecievedDate);
                    cmd.Parameters.AddWithValue("@Details", jd1[i].details);
                    cmd.Parameters.AddWithValue("@INREquivalent", jd1[i].INREquivalent);
                    cmd.ExecuteNonQuery();
                    log.Info("Kind details inserted  of Project Unit: " + j.GrantUnit + "ID: " + j.GID);

                }
            }
            transaction.Commit();
            return result;
        }

        catch (Exception ex)
        {
            log.Error("Inside UpdateSanctinedGrantEntry catch block of Project Unit: " + j.GrantUnit + "ID: " + j.GID);
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

    public DataTable SelectOverheadDetails(string Pid, string projectunit11)
    {
        log.Debug("Inside SelectOverheadDetails function of Project Unit: " + projectunit11 + "ID: " + Pid);
        con = new SqlConnection(str);
        con.Open();
        RecieptData data = new RecieptData();
        try
        {
            cmdString = "Select * from ProjectOverheadTDetails where ProjectUnit=@ProjectUnit and ID=@ID";
            cmd = new SqlCommand(cmdString, con, transaction);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@ID", Pid);
            cmd.Parameters.AddWithValue("@ProjectUnit", projectunit11);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable ds = new DataTable();
            da.Fill(ds);
            return ds;
        }
        catch (Exception ex)
        {
            log.Error("Inside SelectOverheadDetails catch block of of Project Unit: " + projectunit11 + "ID: " + Pid);
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

    public string GetAgencyName(string p)
    {
        log.Debug("Inside GetAgencyName function of agency ID: " + p);
        con = new SqlConnection(str);
        con.Open();
        string userid1 = "";
        try
        {
            cmdString = "Select FundingAgencyName from ProjectFundingAgency_M where FundingAgencyId=@Id";
            cmd = new SqlCommand(cmdString, con, transaction);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@Id", p);
            SqlDataReader sdr = cmd.ExecuteReader();

            while (sdr.Read())
            {
                if (sdr.HasRows)
                {
                    if (!Convert.IsDBNull(sdr["FundingAgencyName"]))
                    {
                        userid1 = sdr["FundingAgencyName"].ToString();
                    }
                }
            }
            return userid1;
        }
        catch (Exception ex)
        {
            log.Error("Inside GetAgencyName catch block of agency ID: " + p);
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


    public DataTable fnfindGrantSanKindDetails(string jid, string bu)
    {
        log.Debug("Inside fnfindGrantSanKindDetails function, ID: " + jid + "Project Unit: " + bu);
        con = new SqlConnection(str);
        con.Open();
        try
        {
            SqlDataAdapter da;
            DataTable ds;
            cmdString = "select EntryNo,CONVERT(char(10), ReceivedDate,103) as ReceivedDate,Details,convert(numeric(13,2),INREquivalent) as INREquivalent from ProjectKindDetails where ID=@ID and ProjectUnit=@GrantUnit ";
            cmd = new SqlCommand(cmdString, con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@ID", SqlDbType.VarChar, 15);
            cmd.Parameters["@ID"].Value = jid;
            cmd.Parameters.Add("@GrantUnit", SqlDbType.VarChar, 5);
            cmd.Parameters["@GrantUnit"].Value = bu;
            da = new SqlDataAdapter(cmd);
            ds = new DataTable();
            da.Fill(ds);
            return ds;
        }

        catch (Exception ex)
        {
            log.Error("Inside fnfindGrantSanKindDetails catch block ID: " + jid + "Project Unit: " + bu);
            log.Error(ex.Message);
            log.Error(ex.StackTrace);
            throw ex;
        }

        finally
        {
        }
    }


    public int UpdateGrantattachedEntry(GrantData j)
    {
        log.Debug("Inside UpdateGrantattachedEntry function, ID: " + j.GID + "Project Unit: " + j.GrantUnit);
        int result = 0;
        con = new SqlConnection(str);
        con.Open();
        transaction = con.BeginTransaction();
        try
        {
            cmdString = "update ProjectAuxillaryDetails set Deleted='Y' where ID=@ID and ProjectUnit=@ProjectUnit and EntryNo=@EntryNum";
            cmd = new SqlCommand(cmdString, con, transaction);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@ID", j.GID);
            cmd.Parameters.AddWithValue("@ProjectUnit", j.GrantType);
            cmd.Parameters.AddWithValue("@EntryNum", j.Entrynum);
            result = cmd.ExecuteNonQuery();
            transaction.Commit();
            log.Info("File Upload Status changed to :'Y' of projectID: " + j.GID + "Project Unit: " + j.GrantUnit + "Entry Number : " + j.Entrynum);
            return result;
        }

        catch (Exception ex)
        {
            log.Error("Inside UpdateGrantattachedEntry catch block of projectID: " + j.GID + "Project Unit: " + j.GrantUnit + "Entry Number : " + j.Entrynum);
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


    public int UploadGrnatPathCreate(GrantData p)
    {
        log.Debug("Inside UploadGrnatPathCreate function, ID: " + p.GID + "Project Unit: " + p.GrantUnit);
        int result = 0;
        con = new SqlConnection(str);
        con.Open();
        transaction = con.BeginTransaction();
        try
        {
            int countNum = 0;
            cmdString = " select COUNT(*) as count from ProjectAuxillaryDetails where ID=@ID and  ProjectUnit=@ProjectUnit ";
            cmd = new SqlCommand(cmdString, con, transaction);
            cmd.Parameters.Add("@ID", SqlDbType.VarChar, 15);
            cmd.Parameters["@ID"].Value = p.GID;
            cmd.Parameters.Add("@ProjectUnit", SqlDbType.VarChar, 12);
            cmd.Parameters["@ProjectUnit"].Value = p.GrantUnit;
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
            cmdString = "insert into ProjectAuxillaryDetails  (ProjectUnit, ID,EntryNo,InfoTypeId,UploadPDFPath,CreatedBy,CreatedDate,Remark,AuditFrom,AuditTo)  values (@ProjectUnit,@ID,@EntryNum,@InfoTypeId,@UploadPDFPath,@CreatedBy,@CreatedDate,@Remark,@AuditFrom,@AuditTo) ";
            cmd = new SqlCommand(cmdString, con, transaction);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@ID", p.GID);
            cmd.Parameters.AddWithValue("@EntryNum", countNum + 1);
            cmd.Parameters.AddWithValue("@InfoTypeId", p.infotypeId);
            cmd.Parameters.AddWithValue("@UploadPDFPath", p.FilePath);
            cmd.Parameters.AddWithValue("@CreatedBy", HttpContext.Current.Session["UserId"].ToString());
            cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
            cmd.Parameters.AddWithValue("@Remark", p.UploadRemarks);
            cmd.Parameters.AddWithValue("@ProjectUnit", p.GrantUnit);
            cmd.Parameters.AddWithValue("@AuditFrom", p.AuditFrom);
            cmd.Parameters.AddWithValue("@AuditTo", p.AuditTo);
            result = cmd.ExecuteNonQuery();
            transaction.Commit();
            log.Error("Inside Auxilary details inserted sucessfully ID: " + p.GID + "Project Unit: " + p.GrantUnit);
            return result;
        }

        catch (Exception ex)
        {
            log.Error("Inside UploadGrnatPathCreate catch block ID: " + p.GID + "Project Unit: " + p.GrantUnit);
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

    public int SelectCountUploadSanctionInformationType(string pubid, string entrytype)
    {
        log.Debug("Inside SelectCountUploadSanctionInformationType function, ID: " + pubid + "Project Unit: " + entrytype);
        con = new SqlConnection(str);
        con.Open();
        transaction = con.BeginTransaction();
        try
        {
            int countNum = 0;
            cmdString = " select COUNT(*) as count from ProjectAuxillaryDetails where ID=@ID  and ProjectUnit=@ProjectUnit and InfoTypeId='SAN' and Deleted='N' ";
            cmd = new SqlCommand(cmdString, con, transaction);
            cmd.Parameters.Add("@ID", SqlDbType.VarChar, 15);
            cmd.Parameters["@ID"].Value = pubid;
            cmd.Parameters.Add("@ProjectUnit", SqlDbType.VarChar, 12);
            cmd.Parameters["@ProjectUnit"].Value = entrytype;
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
            log.Error("Inside SelectCountUploadSanctionInformationType catch block ID: " + pubid + "Project Unit: " + entrytype);
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

    public String GetGrantFileUploadPath(string pubid, string grantunit)
    {
        log.Debug("Inside GetGrantFileUploadPath function, ID: " + pubid + "Project Unit: " + grantunit);

        try
        {
            cmdString = "select * from ProjectAuxillaryDetails where ID=@ID and ProjectUnit=@grantunit ";
            con = new SqlConnection(str);
            con.Open();
            cmd = new SqlCommand(cmdString, con);
            cmd.Parameters.Add("@ID", SqlDbType.VarChar, 15);
            cmd.Parameters["@ID"].Value = pubid;
            cmd.Parameters.Add("@GrantUnit", SqlDbType.VarChar, 12);
            cmd.Parameters["@GrantUnit"].Value = grantunit;
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
            log.Error("Inside GetGrantFileUploadPath catch block  ID: " + pubid + "Project Unit: " + grantunit);
            log.Error(ex.Message);
            log.Error(ex.StackTrace);
            throw ex;
        }

        finally
        {
            con.Close();
        }
    }


    public GrantData fnfindGrantid(string jid, string projectunit)
    {
        log.Debug("Inside fnfindGrantid function, Project ID: " + jid + "Project Unit: " + projectunit);
        try
        {
            cmdString = " select * from Project where ID=@ID and ProjectUnit=@GrantUnit ";
            con = new SqlConnection(str);
            con.Open();
            cmd = new SqlCommand(cmdString, con);
            cmd.Parameters.Add("@ID", SqlDbType.VarChar, 10);
            cmd.Parameters["@ID"].Value = jid;
            cmd.Parameters.Add("@GrantUnit", SqlDbType.VarChar, 5);
            cmd.Parameters["@GrantUnit"].Value = projectunit;
            cmd.CommandType = CommandType.Text;
            SqlDataReader sdr = cmd.ExecuteReader();

            GrantData V = new GrantData();

            while (sdr.Read())
            {
                if (!Convert.IsDBNull(sdr["UTN"]))
                {
                    V.UTN = (string)sdr["UTN"];
                }
                else if (Convert.IsDBNull(sdr["UTN"]))
                {
                    V.UTN = "";
                }
                if (!Convert.IsDBNull(sdr["Title"]))
                {
                    V.Title = (string)sdr["Title"];
                }
                else if (Convert.IsDBNull(sdr["Title"]))
                {
                    V.Title = "";
                }
                if (!Convert.IsDBNull(sdr["Description"]))
                {
                    V.Description = (string)sdr["Description"];
                }
                else if (Convert.IsDBNull(sdr["Description"]))
                {
                    V.Description = "";
                }

                if (!Convert.IsDBNull(sdr["FundingAgency"]))
                {
                    V.GrantingAgency = (string)sdr["FundingAgency"];
                }
                else if (Convert.IsDBNull(sdr["FundingAgency"]))
                {
                    V.GrantingAgency = "";
                }

                if (!Convert.IsDBNull(sdr["Contact_No"]))
                {
                    V.Contact_No = (string)sdr["Contact_No"];
                }
                else if (Convert.IsDBNull(sdr["Contact_No"]))
                {
                    V.Contact_No = "";
                }
                if (!Convert.IsDBNull(sdr["ProjectUnit"]))
                {
                    V.GrantUnit = (string)sdr["ProjectUnit"];
                }
                else if (Convert.IsDBNull(sdr["ProjectUnit"]))
                {
                    V.GrantUnit = "";
                }

                if (!Convert.IsDBNull(sdr["AppliedAmount"]))
                {
                    V.GranAmount = Convert.ToDouble((decimal)sdr["AppliedAmount"]);
                }
                else if (Convert.IsDBNull(sdr["AppliedAmount"]))
                {
                    V.GranAmount = 0;
                }
                if (!Convert.IsDBNull(sdr["RevisedAppliedAmount"]))
                {
                    V.RevisedAppliedAmt = Convert.ToDouble((decimal)sdr["RevisedAppliedAmount"]);
                }
                else if (Convert.IsDBNull(sdr["RevisedAppliedAmount"]))
                {
                    V.RevisedAppliedAmt = 0;
                }

                if (!Convert.IsDBNull(sdr["SourceProject"]))
                {
                    V.GrantSource = (string)sdr["SourceProject"];
                }
                else if (Convert.IsDBNull(sdr["SourceProject"]))
                {
                    V.GrantSource = "";
                }
                if (!Convert.IsDBNull(sdr["ProjectStatus"]))
                {
                    V.Status = (string)sdr["ProjectStatus"];
                }
                else if (Convert.IsDBNull(sdr["ProjectStatus"]))
                {
                    V.Status = "";
                }
                if (!Convert.IsDBNull(sdr["RejectedRemarks"]))
                {
                    V.RejectFeedback = (string)sdr["RejectedRemarks"];
                }
                else if (Convert.IsDBNull(sdr["RejectedRemarks"]))
                {
                    V.RejectFeedback = "";
                }
                if (!Convert.IsDBNull(sdr["SanctionType"]))
                {
                    V.SancType = (string)sdr["SanctionType"];
                }
                else if (Convert.IsDBNull(sdr["SanctionType"]))
                {
                    V.SancType = "";
                }

                if (!Convert.IsDBNull(sdr["SactionKindComments"]))
                {
                    V.KindComments = (string)sdr["SactionKindComments"];
                }
                else if (Convert.IsDBNull(sdr["SactionKindComments"]))
                {
                    V.KindComments = "";
                }
                if (!Convert.IsDBNull(sdr["ERFRealated"]))
                {
                    V.ERFRelated = (string)sdr["ERFRealated"];
                }
                else if (Convert.IsDBNull(sdr["ERFRealated"]))
                {
                    V.ERFRelated = "";

                }

                if (!Convert.IsDBNull(sdr["CancelRemarks"]))
                {
                    V.CancelFeedback = (string)sdr["CancelRemarks"];
                }
                else if (Convert.IsDBNull(sdr["CancelRemarks"]))
                {
                    V.CancelFeedback = "";
                }

                if (!Convert.IsDBNull(sdr["AppliedDate"]))
                {
                    V.AppliedDate = (DateTime)sdr["AppliedDate"];
                }

                if (!Convert.IsDBNull(sdr["Comments"]))
                {
                    V.AddtionalComments = (string)sdr["Comments"];
                }
                else if (Convert.IsDBNull(sdr["Comments"]))
                {
                    V.AddtionalComments = "";
                }

                if (!Convert.IsDBNull(sdr["AgencyAddress"]))
                {
                    V.Address = (string)sdr["AgencyAddress"];
                }
                else if (Convert.IsDBNull(sdr["AgencyAddress"]))
                {
                    V.Address = "";
                }
                if (!Convert.IsDBNull(sdr["AgencyPanNo"]))
                {
                    V.Pan_No = (string)sdr["AgencyPanNo"];
                }
                else if (Convert.IsDBNull(sdr["AgencyPanNo"]))
                {
                    V.Pan_No = "";
                }


                if (!Convert.IsDBNull(sdr["State"]))
                {
                    V.State = (string)sdr["State"];
                }
                else if (Convert.IsDBNull(sdr["State"]))
                {
                    V.State = "";
                }


                if (!Convert.IsDBNull(sdr["Country"]))
                {
                    V.Country = (string)sdr["Country"];
                }
                else if (Convert.IsDBNull(sdr["Country"]))
                {
                    V.Country = "";
                }

                if (!Convert.IsDBNull(sdr["AgencyContact"]))
                {
                    V.AgencyContact = (string)sdr["AgencyContact"];
                }
                else if (Convert.IsDBNull(sdr["AgencyContact"]))
                {
                    V.AgencyContact = "";
                }

                if (!Convert.IsDBNull(sdr["AgencyEmailId"]))
                {
                    V.AgencyEmailId = (string)sdr["AgencyEmailId"];
                }
                else if (Convert.IsDBNull(sdr["AgencyEmailId"]))
                {
                    V.AgencyEmailId = "";
                }

                if (!Convert.IsDBNull(sdr["ReworkRemarks"]))
                {
                    V.Remarks = (string)sdr["ReworkRemarks"];
                }
                else if (Convert.IsDBNull(sdr["ReworkRemarks"]))
                {
                    V.Remarks = "";
                }

                if (!Convert.IsDBNull(sdr["FinanceStatus"]))
                {
                    V.FinanceProjectStatus = (string)sdr["FinanceStatus"];
                }
                else if (Convert.IsDBNull(sdr["FinanceStatus"]))
                {
                    V.FinanceProjectStatus = "";
                }

                if (!Convert.IsDBNull(sdr["Remarks"]))
                {
                    V.FinanceClosureRemarks = (string)sdr["Remarks"];
                }
                else if (Convert.IsDBNull(sdr["Remarks"]))
                {
                    V.FinanceClosureRemarks = "";
                }

                if (!Convert.IsDBNull(sdr["CompletionDate"]))
                {
                    V.DateOfCompletion = (DateTime)sdr["CompletionDate"];
                }
                if (!Convert.IsDBNull(sdr["KindProjectCloseDate"]))
                {
                    V.KindCloseDate = (DateTime)sdr["KindProjectCloseDate"];
                }
                if (!Convert.IsDBNull(sdr["KindProjectStartDate"]))
                {
                    V.KindStartDate = (DateTime)sdr["KindProjectStartDate"];
                }
                //if (!Convert.IsDBNull(sdr["ActualAppliedDate"]))
                //{
                //    V.ProjectActualDate = (DateTime)sdr["ActualAppliedDate"];
                //}
                if (!Convert.IsDBNull(sdr["DurationOfProject"]))
                {
                    V.DurationOfProject = (int)sdr["DurationOfProject"];
                }
                if (!Convert.IsDBNull(sdr["ProjectType"]))
                {
                    V.GrantType = (string)sdr["ProjectType"];
                }
                if (!Convert.IsDBNull(sdr["CreatedBy"]))
                {
                    V.CreatedBy = (string)sdr["CreatedBy"];
                }
                if (!Convert.IsDBNull(sdr["SanctionOrderDate"]))
                {
                    V.SanctionOrderDate = (DateTime)sdr["SanctionOrderDate"];
                }
                if (!Convert.IsDBNull(sdr["FundingSectorLevel"]))
                {
                    V.FundingSectorLevelGrant = (int)sdr["FundingSectorLevel"];
                }
                else
                {
                    V.FundingSectorLevelGrant = 0;
                }
                if (!Convert.IsDBNull(sdr["TypeofAgency"]))
                {
                    V.TypeofAgencyGrant = (int)sdr["TypeofAgency"];
                }
                else
                {
                    V.TypeofAgencyGrant = 0;
                }
               
            }
            return V;
        }
        catch (Exception ex)
        {
            log.Error("Inside fnfindGrantid catch block of Project ID: " + jid + " and Project Unit: " + projectunit);
            log.Error("Erroe Message : " + ex.Message);
            log.Error(ex.StackTrace);
            throw ex;
        }

        finally
        {
            con.Close();
        }
    }


    public DataTable SelectIncentiveAmountDetailsExists(string id, string unit)
    {
        log.Debug("Inside SelectIncentiveAmountDetailsExists function of Project Id: " + id + "and Project Unit: " + unit);
        con = new SqlConnection(str);
        con.Open();
        IncentiveData data = new IncentiveData();
        try
        {
            cmdString = "Select * from  ProjectIncentivePayDetails where  ID=@ID and ProjectUnit=@ProjectUnit";
            cmd = new SqlCommand(cmdString, con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@ID", id);
            cmd.Parameters.AddWithValue("@ProjectUnit", unit);
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable ds = new DataTable();
            da.Fill(ds);

            return ds;
        }
        catch (Exception ex)
        {
            log.Error("Inside SelectIncentiveAmountDetailsExists catch block of  Project Id: " + id + "and Project Unit: " + unit);
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

    public DataTable SelectSanctionOPAmountDetails(string id, string unit, int p)
    {
        log.Debug("Inside SelectSanctionOPAmountDetails function of  Project Id: " + id + "and Project Unit: " + unit);
        con = new SqlConnection(str);
        con.Open();
        IncentiveData data = new IncentiveData();
        try
        {
            cmdString = "select ROW_NUMBER() OVER (ORDER BY a.[ID]) AS Row, a.ID,Name ,b.SanctionEntryNo  as indexv,b.OperatingItemId,b.Amount,'' as rowIndexParent,'' as rowIndexChild from OperatingItem_M a left outer join SanctionOPAmountDetails b  on a.ID=b.OperatingItemId and b.SanctionEntryNo=@Line and b.ID=@ID and b.ProjectUnit=@ProjectUnit";
            cmd = new SqlCommand(cmdString, con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@ID", id);
            cmd.Parameters.AddWithValue("@ProjectUnit", unit);
            cmd.Parameters.AddWithValue("@Line", p);
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable ds = new DataTable();
            da.Fill(ds);

            return ds;
        }
        catch (Exception ex)
        {
            log.Error("Inside SelectSanctionOPAmountDetails catch block of of  Project Id: " + id + "and Project Unit: " + unit);
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

    public int UpdateStatusGrantEntryCancel(GrantData j)
    {
        log.Debug("Inside UpdateStatusGrantEntryCancel function of  Project Id: " + j.GID + "and Project Unit: " + j.GrantUnit);
        int result = 0;
        con = new SqlConnection(str);
        con.Open();
        transaction = con.BeginTransaction();
        try
        {

            cmdString = "update Project set ProjectStatus=@Status,CancelRemarks=@CancelRemarks,cancelledDate=@cancelledDate,CancelledBy=@CancelledBy where ID=@ID and ProjectUnit=@GrantUnit";

            cmd = new SqlCommand(cmdString, con, transaction);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("@ID", j.GID);

            cmd.Parameters.AddWithValue("@Status", j.Status);

            cmd.Parameters.AddWithValue("@CancelRemarks", j.CancelFeedback);
            cmd.Parameters.AddWithValue("@GrantUnit", j.GrantUnit);

            cmd.Parameters.AddWithValue("@cancelledDate", DateTime.Now);
            cmd.Parameters.AddWithValue("@CancelledBy", j.CancelledBy);

            result = cmd.ExecuteNonQuery();
            cmdString = "Select count(* ) as Count from ProjectStatusTracker where  ID=@ID and ProjectUnit=@GrantUnit";
            cmd = new SqlCommand(cmdString, con, transaction);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@ID", j.GID);
            cmd.Parameters.AddWithValue("@GrantUnit", j.GrantUnit);

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

            cmd = new SqlCommand("InsertProjectReviewTracker", con, transaction);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@ID", j.GID);
            cmd.Parameters.AddWithValue("@GrantUnit", j.GrantUnit);
            cmd.Parameters.AddWithValue("@ReviewNo", count + 1);

            cmd.Parameters.AddWithValue("@ApprovedStatus", j.Status);

            if (j.CancelFeedback != null)
            {
                cmd.Parameters.AddWithValue("@Remark", j.CancelFeedback);
            }
            else
            {
                cmd.Parameters.AddWithValue("@Remark", DBNull.Value);
            }

            cmd.Parameters.AddWithValue("@UpdateUser", j.CancelledBy);
            cmd.Parameters.AddWithValue("@Date", DateTime.Now);
            result = cmd.ExecuteNonQuery();
            transaction.Commit();
            log.Info("Project  Project Id: " + j.GID + "and Project Unit: " + j.GrantUnit + " Cancelled sucessfully by the user :" + j.CreatedBy);
            log.Info("Grant Cancellation : User Name :" + HttpContext.Current.Session["UserName"] + "Role :" + HttpContext.Current.Session["RoleName"]); 
            return result;
        }

        catch (Exception ex)
        {
            log.Error("UpdateStatusGrantEntryCancel catch block  of Project Id: " + j.GID + "and Project Unit: " + j.GrantUnit);
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

    public int UpdateGrantEntryPIMove(GrantData j, GrantData[] jd)
    {
        log.Debug("Inside UpdateGrantEntryPIMove function of  Project Id: " + j.GID + "and Project Unit: " + j.GrantUnit);

        int result = 0, result1 = 0;
        con = new SqlConnection(str);
        con.Open();

        con = new SqlConnection(str);
        con.Open();

        transaction = con.BeginTransaction();
        try
        {
            cmdString = "update Project set  PIInstitutionID=@PIInstitutionID,PIDeptID=@PIDeptID where ID=@ID and ProjectUnit=@GrantType";
            cmd = new SqlCommand(cmdString, con, transaction);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@UTN", seedFinalUTN);

            cmd.Parameters.AddWithValue("@ID", j.GID);
            cmd.Parameters.AddWithValue("@PIMoveFeedback", j.PIMoveFeedback);

            cmd.Parameters.AddWithValue("@GrantType", j.GrantUnit);

            cmd.Parameters.AddWithValue("@PIInstitutionID", j.PiInstId);
            cmd.Parameters.AddWithValue("@PIDeptID", j.PiDeptId);

            result = cmd.ExecuteNonQuery();

            cmdString = "delete from Projectnvestigator where ID=@ID and Projectunit=@GrantType";
            cmd = new SqlCommand(cmdString, con, transaction);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("@ID", j.GID);
            cmd.Parameters.AddWithValue("@GrantType", j.GrantUnit);

            result = cmd.ExecuteNonQuery();
            if (result >= 1)
            {
                for (int i = 0; i < jd.Length; i++)
                {
                    cmd = new SqlCommand("InsertProjectInvestigator", con, transaction);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@ID", j.GID);

                    cmd.Parameters.AddWithValue("@GrantLine", i + 1);
                    cmd.Parameters.AddWithValue("@ProjectUnit", j.GrantUnit);
                    cmd.Parameters.AddWithValue("@AuthorName", jd[i].AuthorName);
                    cmd.Parameters.AddWithValue("@MUNonMU", jd[i].MUNonMU);
                    cmd.Parameters.AddWithValue("@EmployeeCode", jd[i].EmployeeCode);

                    cmd.Parameters.AddWithValue("@Institution", jd[i].Institution);
                    cmd.Parameters.AddWithValue("@Department", jd[i].Department);

                    cmd.Parameters.AddWithValue("@InstitutionName", jd[i].InstitutionName);
                    cmd.Parameters.AddWithValue("@DepartmentName", jd[i].DepartmentName);
                    cmd.Parameters.AddWithValue("@AuthorType", jd[i].AuthorType);
                    if (jd[i].AuthorType == "P" && jd[i].LeadPI == "Y")
                    {

                        cmd.Parameters.AddWithValue("@isLeadPI", "Y");
                    }
                    else if (jd[i].AuthorType == "P" && jd[i].LeadPI == "N")
                    {

                        cmd.Parameters.AddWithValue("@isLeadPI", "N");
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@isLeadPI", DBNull.Value);
                    }
                    cmd.Parameters.AddWithValue("@NationalInternational", jd[i].NationalInternationl);
                    cmd.Parameters.AddWithValue("@Continent", jd[i].continental);
                    cmd.Parameters.AddWithValue("@EmailId", jd[i].EmailId);
                    result1 = cmd.ExecuteNonQuery();

                }
            }


            cmdString = "Select count(* ) as Count from ProjectPIMoveTracker where ProjectUnit=@Type AND ID=@ProjectID ";
            cmd = new SqlCommand(cmdString, con, transaction);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@Type", j.GrantUnit);
            cmd.Parameters.AddWithValue("@ProjectID", j.GID);

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

            cmd = new SqlCommand("InsertProjectPIMoveTracker", con, transaction);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@ProjectID", j.GID);

            cmd.Parameters.AddWithValue("@GrantLine", count + 1);
            cmd.Parameters.AddWithValue("@ProjectUnit", j.GrantUnit);

            cmd.Parameters.AddWithValue("@UpdatedUser", j.CreatedBy);
            cmd.Parameters.AddWithValue("@UpdatedDate", DateTime.Now);
            cmd.Parameters.AddWithValue("@Remarks", j.PIMoveFeedback);
            result1 = cmd.ExecuteNonQuery();
            transaction.Commit();
            log.Info("Inside PI Movement is sucessfull  of  Project Id: " + j.GID + "and Project Unit: " + j.GrantUnit);
            log.Info("Grant PI move : User Name :" + HttpContext.Current.Session["UserName"] + "Role :" + HttpContext.Current.Session["RoleName"]); 
            return result1;
        }

        catch (Exception ex)
        {
            log.Error("Inside UpdateGrantEntryPIMove catch block of  Project Id: " + j.GID + "and Project Unit: " + j.GrantUnit);
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

    public DataTable fnfindGrantInvestigatorDetails(string jid, string bu)
    {
        log.Debug("Inside fnfindGrantInvestigatorDetails function, ProjectUnit: " + bu + "ID: " + jid);

        con = new SqlConnection(str);
        con.Open();
        try
        {
            SqlDataAdapter da;
            DataTable ds;
            cmdString = "select MUNonMU  as DropdownMuNonMu,EmployeeCode,InvestigatorName as AuthorName,Institution,Department, DepartmentName,InstitutionName ,  EmailId as MailId,InvestigatorType as AuthorType,isLeadPI,NationalInternational as NationalType,Continent as ContinentId from Projectnvestigator where ID=@ID and ProjectUnit=@GrantUnit ";
            cmd = new SqlCommand(cmdString, con);
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add("@ID", SqlDbType.VarChar, 15);
            cmd.Parameters["@ID"].Value = jid;

            cmd.Parameters.Add("@GrantUnit", SqlDbType.VarChar, 5);
            cmd.Parameters["@GrantUnit"].Value = bu;
            da = new SqlDataAdapter(cmd);

            ds = new DataTable();
            da.Fill(ds);

            return ds;
        }

        catch (Exception ex)
        {
            log.Error("Inside fnfindGrantInvestigatorDetails catch block ProjectUnit: " + bu + "ID: " + jid);
            log.Error(ex.Message);
            log.Error(ex.StackTrace);
            throw ex;
        }

        finally
        {
            con.Close();
        }
    }

    public GrantData fnfindGrantidSanctionDetails(string jid, string bu)
    {
        log.Debug("Inside  fnfindGrantidSanctionDetails function, Project Unit: " + bu + "ID: " + jid);
        try
        {

            cmdString = " select * from Project where ID=@ID and ProjectUnit=@GrantType ";
            con = new SqlConnection(str);
            con.Open();
            cmd = new SqlCommand(cmdString, con);
            cmd.Parameters.Add("@ID", SqlDbType.VarChar, 15);
            cmd.Parameters["@ID"].Value = jid;
            cmd.Parameters.Add("@GrantType", SqlDbType.VarChar, 12);
            cmd.Parameters["@GrantType"].Value = bu;
            cmd.CommandType = CommandType.Text;
            SqlDataReader sdr = cmd.ExecuteReader();
            GrantData V = new GrantData();

            while (sdr.Read())
            {

                if (!Convert.IsDBNull(sdr["SanctionCapitalAmount"]))
                {
                    V.SanctionCapitalAmount = Convert.ToDouble((decimal)(sdr["SanctionCapitalAmount"]));
                }
                else if (Convert.IsDBNull(sdr["SanctionCapitalAmount"]))
                {
                    V.SanctionCapitalAmount = 0;
                }
                if (!Convert.IsDBNull(sdr["SanctionOperatingAmount"]))
                {
                    V.SanctionOperatingAmount = Convert.ToDouble((decimal)(sdr["SanctionOperatingAmount"]));
                }
                else if (Convert.IsDBNull(sdr["SanctionOperatingAmount"]))
                {
                    V.SanctionOperatingAmount = 0;
                }

                if (!Convert.IsDBNull(sdr["SanctionTotalAmount"]))
                {
                    V.SanctionTotalAmount = Convert.ToDouble((decimal)(sdr["SanctionTotalAmount"]));
                }
                else if (Convert.IsDBNull(sdr["SanctionTotalAmount"]))
                {
                    V.SanctionTotalAmount = 0;
                }

                if (!Convert.IsDBNull(sdr["ProjectCommencementDate"]))
                {
                    V.ProjectCommencementDate = (DateTime)sdr["ProjectCommencementDate"];
                }

                if (!Convert.IsDBNull(sdr["ProjectCloseDate"]))
                {
                    V.ProjectCloseDate = (DateTime)sdr["ProjectCloseDate"];
                }

                if (!Convert.IsDBNull(sdr["ExtendedDate"]))
                {
                    V.ExtendedDate = (DateTime)sdr["ExtendedDate"];
                }
                if (!Convert.IsDBNull(sdr["AuditRequired"]))
                {
                    V.AuditRequired = (string)sdr["AuditRequired"];
                }
                if (!Convert.IsDBNull(sdr["AccountHead"]))
                {
                    V.AccountHead = (string)sdr["AccountHead"];
                }
                if (!Convert.IsDBNull(sdr["InstitutionShare"]))
                {
                    V.InstitutionSahre = Convert.ToDouble((decimal)sdr["InstitutionShare"]);
                }

                if (!Convert.IsDBNull(sdr["FinanceStatus"]))
                {
                    V.FinanceProjectStatus = (string)sdr["FinanceStatus"];
                }
                if (!Convert.IsDBNull(sdr["CompletionDate"]))
                {
                    V.DateOfCompletion = (DateTime)sdr["CompletionDate"];
                }
                if (!Convert.IsDBNull(sdr["ServiceTaxAppl"]))
                {
                    V.ServiceTaxApplicable = (string)sdr["ServiceTaxAppl"];
                }
                if (!Convert.IsDBNull(sdr["Remarks"]))
                {
                    V.Remarks = (string)sdr["Remarks"];
                }

                if (!Convert.IsDBNull(sdr["NoOfSanction"]))
                {
                    V.SanctionEntryNumber = (int)sdr["NoOfSanction"];
                }


            }
            return V;
        }
        catch (Exception ex)
        {
            log.Error("Inside fnfindGrantidSanctionDetails catch block Project Unit: " + bu + "ID: " + jid);
            log.Error(ex.Message);
            log.Error(ex.StackTrace);
            throw ex;
        }

        finally
        {
            con.Close();
        }
    }

    public DataTable SelectSanctionData(string Pid, string projectunit1)
    {
        log.Debug("Inside SelectSanctionDetails function Project Unit: " + projectunit1 + "ID: " + Pid);
        con = new SqlConnection(str);
        con.Open();
        RecieptData data = new RecieptData();
        try
        {
            cmdString = "select * from ProjectSanctionedDetails where ProjectUnit=@ProjectUnit and ID=@ID";
            cmd = new SqlCommand(cmdString, con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@ID", Pid);
            cmd.Parameters.AddWithValue("@ProjectUnit", projectunit1);
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable ds = new DataTable();
            da.Fill(ds);

            return ds;
        }
        catch (Exception ex)
        {
            log.Error("Inside SelectSanctionDetails catch block of Project Unit: " + projectunit1 + "ID: " + Pid);
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

    public DataTable SelectSanctionOPAmountDetailsExists(string Pid, string projectunit)
    {
        log.Debug("Inside SelectSanctionOPAmountDetailsExists function of Project Unit: " + projectunit + "ID: " + Pid);
        con = new SqlConnection(str);
        con.Open();
        IncentiveData data = new IncentiveData();
        try
        {
            cmdString = "Select * from  SanctionOPAmountDetails where  ID=@ID and ProjectUnit=@ProjectUnit";
            cmd = new SqlCommand(cmdString, con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@ID", Pid);
            cmd.Parameters.AddWithValue("@ProjectUnit", projectunit);
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable ds = new DataTable();
            da.Fill(ds);

            return ds;
        }
        catch (Exception ex)
        {
            log.Error("Inside SelectSanctionOPAmountDetailsExists catch block of Project Unit: " + projectunit + "ID: " + Pid);
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


    public DataTable SelectRecipetDetails(string Pid, string unit)
    {
        log.Debug("Inside SelectRecipetDetails function of Project Unit: " + unit + "ID: " + Pid);
        con = new SqlConnection(str);
        con.Open();
        RecieptData data = new RecieptData();
        try
        {
           // cmdString = "Select *,u2.BankName as CreditedBankName from ProjectReceiptDetails, ProjectBank_M u2 where ProjectUnit=@ProjectUnit and ID=@ID     and u2.BankID=ProjectReceiptDetails.CreditedBank  ";
            cmdString = "SELECT *,BankName as CreditedBankName FROM ProjectReceiptDetails t1  LEFT OUTER JOIN ProjectBank_M t2 ON t1.CreditedBank = t2.BankID where ProjectUnit=@ProjectUnit and ID=@ID";
            cmd = new SqlCommand(cmdString, con, transaction);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@ID", Pid);
            cmd.Parameters.AddWithValue("@ProjectUnit", unit);
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable ds = new DataTable();
            da.Fill(ds);

            return ds;
        }
        catch (Exception ex)
        {
            log.Error("Inside SelectRecipetDetails catch block of Project Unit: " + unit + "ID: " + Pid);
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

    public DataTable SelectIncentiveDetails(string Pid, string unit)
    {
        log.Debug("Inside SelectIncentiveDetails function of Project Unit: " + unit + "ID: " + Pid);
        con = new SqlConnection(str);
        con.Open();
        IncentiveData data = new IncentiveData();
        try
        {
            cmdString = "Select * from ProjectIncentiveDetails where ProjectIncentiveDetails.ProjectUnit=@ProjectUnit and ProjectIncentiveDetails.ID=@ID  ";
            cmd = new SqlCommand(cmdString, con, transaction);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@ID", Pid);
            cmd.Parameters.AddWithValue("@ProjectUnit", unit);
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable ds = new DataTable();
            da.Fill(ds);

            return ds;
        }
        catch (Exception ex)
        {
            log.Error("Inside SelectIncentiveDetails catch block of Project Unit: " + unit + "ID: " + Pid);
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

    public string SelectPIMoveComment(string Pid, string projectunit)
    {
        log.Debug("Inside SelectPIMoveComment function of Project Unit: " + projectunit + "ID: " + Pid);
        con = new SqlConnection(str);
        con.Open();
        IncentiveData data = new IncentiveData();
        try
        {
            cmdString = "Select * from ProjectPIMoveTracker where ProjectUnit=@ProjectUnit and ID=@ID  ";
            cmd = new SqlCommand(cmdString, con, transaction);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@ID", Pid);
            cmd.Parameters.AddWithValue("@ProjectUnit", projectunit);
            SqlDataReader sdr = cmd.ExecuteReader();
            string remarks = "";
            while (sdr.Read())
            {

                if (!Convert.IsDBNull(sdr["Remarks"]))
                {
                    remarks = (string)(sdr["Remarks"]);
                }
            }
            return remarks;
        }
        catch (Exception ex)
        {
            log.Error("Inside SelectPIMoveComment catch block of Project Unit: " + projectunit + "ID: " + Pid);
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

    public int UpdateStatusGrantEntryCLO(GrantData j)
    {
        log.Debug("Inside UpdateStatusGrantEntryCLO function of Project Unit: " + j.GrantUnit + "ID: " + j.GID);

        int result = 0;
        con = new SqlConnection(str);
        con.Open();
        transaction = con.BeginTransaction();
        try
        {
            cmdString = "update Project set ProjectStatus=@Status where ID=@ID and ProjectUnit=@GrantUnit";
            cmd = new SqlCommand(cmdString, con, transaction);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@ID", j.GID);
            cmd.Parameters.AddWithValue("@Status", j.Status);
            cmd.Parameters.AddWithValue("@GrantUnit", j.GrantUnit);
            result = cmd.ExecuteNonQuery();

            cmdString = "Select count(* ) as Count from ProjectStatusTracker where  ID=@ID and ProjectUnit=@GrantUnit";
            cmd = new SqlCommand(cmdString, con, transaction);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@ID", j.GID);
            cmd.Parameters.AddWithValue("@GrantUnit", j.GrantUnit);

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

            cmd = new SqlCommand("InsertProjectReviewTracker", con, transaction);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID", j.GID);
            cmd.Parameters.AddWithValue("@GrantUnit", j.GrantUnit);
            cmd.Parameters.AddWithValue("@ReviewNo", count + 1);
            cmd.Parameters.AddWithValue("@ApprovedStatus", j.Status);
            cmd.Parameters.AddWithValue("@UpdateUser", HttpContext.Current.Session["UserId"].ToString());
            cmd.Parameters.AddWithValue("@Date", DateTime.Now);
            cmd.Parameters.AddWithValue("@Remark", j.AddtionalComments);
            result = cmd.ExecuteNonQuery();
            transaction.Commit();
            log.Error("Project Status is changed to :" + j.Status + " of Project Unit: " + j.GrantUnit + "ID: " + j.GID);
            return result;
        }

        catch (Exception ex)
        {
            log.Error("UpdateStatusGrantEntryCLO catch block of Project Unit: " + j.GrantUnit + "ID: " + j.GID);
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

    public DataTable SelectIncentiveAmountDetails(string id, string unit, int p)
    {
        log.Debug("Inside SelectIncentiveAmountDetails function of Project Unit: " + id + "ID: " + unit);
        con = new SqlConnection(str);
        con.Open();
        IncentiveData data = new IncentiveData();
        try
        {
            //cmdString = "Select a.ProjectUnit,a.ID, a.EntryNo as Row,InvestigatorName,Amount,b.SanctionEntryNo,a.Institution as Institution,a.Department as Department from Projectnvestigator a left outer join ProjectIncentivePayDetails b on a.EntryNo=b.EntryNo and  b.[LineNo]=" + p + " and a.ProjectUnit='" + unit + "' and a.ID='" + id + "' where a.ProjectUnit='" + unit + "' and a.ID='" + id + "'";
            cmdString = "Select a.ProjectUnit,a.ID, a.EntryNo as Row,InvestigatorName,Amount,b.SanctionEntryNo,a.Institution as Institution,a.Department as Department from Projectnvestigator a left outer join ProjectIncentivePayDetails b on a.EntryNo=b.EntryNo and  b.[LineNo]=@p and a.ProjectUnit=@unit and a.ID=@id where a.ProjectUnit=@unit and a.ID=@id";

            cmd = new SqlCommand(cmdString, con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@unit", unit);
            cmd.Parameters.AddWithValue("@p", p);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable ds = new DataTable();
            da.Fill(ds);
            return ds;
        }
        catch (Exception ex)
        {
            log.Error("Inside SelectIncentiveAmountDetails catch block of of Project Unit: " + unit + "ID: " + id);
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


    public GrantData selectExisitingAgency(string FundingAgencyId)
    {
        log.Debug("Inside selectExisitingAgency function of ID: " + FundingAgencyId);

        try
        {
            GrantData b = new GrantData();
            con.Open();
            SqlCommand cmd = new SqlCommand("selectExisitingAgency", con); //selectExistingUser stored procedure 

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FundingAgencyId", SqlDbType.VarChar);
            cmd.Parameters["@FundingAgencyId"].Value = FundingAgencyId;
            cmd.CommandType = CommandType.StoredProcedure;

            SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                if (!Convert.IsDBNull(sdr["FundingAgencyId"]))
                {
                    b.FundingAgencyId = (string)sdr["FundingAgencyId"];
                }
                if (!Convert.IsDBNull(sdr["FundingAgencyName"]))
                {
                    b.FundingAgencyName = (string)sdr["FundingAgencyName"];
                }
                if (!Convert.IsDBNull(sdr["ContactNo"]))
                {
                    b.AgencyContact = (string)sdr["ContactNo"];
                }
                if (!Convert.IsDBNull(sdr["PanNo"]))
                {
                    b.Pan_No = (string)sdr["PanNo"];
                }
                if (!Convert.IsDBNull(sdr["EmailId"]))
                {
                    b.AgencyEmailId = (string)sdr["EmailId"];
                }
                if (!Convert.IsDBNull(sdr["Address"]))
                {
                    b.Address = (string)sdr["Address"];
                }
                if (!Convert.IsDBNull(sdr["State"]))
                {
                    b.State = (string)sdr["State"];
                }
                if (!Convert.IsDBNull(sdr["Country"]))
                {
                    b.Country = (string)sdr["Country"];
                }
                if (!Convert.IsDBNull(sdr["EmailId"]))
                {
                    b.EmailId = (string)sdr["EmailId"];
                }

                if (!Convert.IsDBNull(sdr["AgentType"]))
                {
                    b.AgentType = (string)sdr["AgentType"];
                }
            }

            return b;
        }
        catch (Exception e)
        {
            log.Debug("Error: Inside catch block of selectExisitingAgency");
            log.Error("Error msg:" + e);
            log.Error("Stack trace:" + e.StackTrace);
            return null;
        }

        finally
        {
            con.Close();
        }
    }


    public int UpdateAgency(GrantData b)
    {
        log.Debug("Inside UpdateAgency function of ID: " + b.FundingAgencyId);
        int result1 = 0;
        try
        {
            con.Open();
            transaction = con.BeginTransaction();
            SqlCommand cmd = new SqlCommand("UpdateAgency", con, transaction);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FundingAgencyId", b.FundingAgencyId);
            cmd.Parameters.AddWithValue("@FundingAgencyName", b.FundingAgencyName);
            cmd.Parameters.AddWithValue("@AgentType", b.AgentType);
            cmd.Parameters.AddWithValue("@ContactNo", b.AgencyContact);
            cmd.Parameters.AddWithValue("@PanNo", b.Pan_No);
            cmd.Parameters.AddWithValue("@EmailId", b.AgencyEmailId);
            cmd.Parameters.AddWithValue("@Address", b.Address);
            cmd.Parameters.AddWithValue("@State", b.State);
            cmd.Parameters.AddWithValue("@Country", b.Country);
            result1 = cmd.ExecuteNonQuery();
            transaction.Commit();
            log.Info("Agency details updated of ID: " + b.FundingAgencyId);
            return result1;
        }
        catch (Exception e)
        {
            log.Debug("Error: Inside catch block of UpdateAgency of ID: " + b.FundingAgencyId);
            log.Error("Error msg:" + e);
            log.Error("Stack trace:" + e.StackTrace);
            transaction.Rollback();
            return 0;

        }
    }

    public int SaveAgencyDetails(GrantData b)
    {
        log.Debug("Inside SaveAgencyDetails function of ID: " + b.FundingAgencyId);

        int result = 0;
        try
        {
            con.Open();
            transaction = con.BeginTransaction();
            SqlCommand cmd = new SqlCommand("InsertAgency", con, transaction);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@FundingAgencyId", b.FundingAgencyId);
            cmd.Parameters.AddWithValue("@FundingAgencyName", b.FundingAgencyName);
            cmd.Parameters.AddWithValue("@AgentType", b.AgentType);
            cmd.Parameters.AddWithValue("@ContactNo", b.AgencyContact);
            cmd.Parameters.AddWithValue("@PanNo", b.Pan_No);
            cmd.Parameters.AddWithValue("@EmailId", b.AgencyEmailId);
            cmd.Parameters.AddWithValue("@Address", b.Address);
            cmd.Parameters.AddWithValue("@State", b.State);
            cmd.Parameters.AddWithValue("@Country", b.Country);

            result = cmd.ExecuteNonQuery();
            transaction.Commit();
            log.Debug("Inside Agency details saved sucessfully of ID: " + b.FundingAgencyId);

            return result;
        }
        catch (Exception e)
        {
            log.Debug("Error: Inside catch block of SaveAgencyDetails of ID: " + b.FundingAgencyId);
            log.Error("Error msg:" + e);
            log.Error("Stack trace:" + e.StackTrace);
            transaction.Rollback();
            return 0;

        }
        finally
        {

        }
    }


    public DataTable SelectSanctionOPAmountDetails1(string Pid, string projectunit11, int p)
    {
        log.Debug("Inside SelectSanctionOPAmountDetails1 function of ID: " + Pid + " unit :" + projectunit11);
        con = new SqlConnection(str);
        con.Open();
        IncentiveData data = new IncentiveData();
        try
        {
            cmdString = "Select ROW_NUMBER() OVER (ORDER BY [ID]) AS Row ,OperatingItemId as ID, '' as Name, SanctionEntryNo as indexv,OperatingItemId as ID, Amount from SanctionOPAmountDetails where ID=@ID and ProjectUnit=@ProjectUnit and SanctionEntryNo=1 and  ID=@ID and ProjectUnit=@ProjectUnit";
            cmd = new SqlCommand(cmdString, con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@ID", Pid);
            cmd.Parameters.AddWithValue("@ProjectUnit", projectunit11);
            cmd.Parameters.AddWithValue("@Line", p);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable ds = new DataTable();
            da.Fill(ds);
            return ds;
        }
        catch (Exception ex)
        {
            log.Error("Inside SelectSanctionOPAmountDetails1 catch block of ID: " + Pid + " unit :" + projectunit11);
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

    public DataTable SelectIncentiveAmountDetails1(string Pid, string projectunit11, int p)
    {
        log.Debug("Inside SelectIncentiveAmountDetails1 function of ID: " + Pid + " unit :" + projectunit11);
        con = new SqlConnection(str);
        con.Open();
        IncentiveData data = new IncentiveData();
        try
        {
            //cmdString = "Select ProjectUnit,ID,EntryNo as Row ,PayedTo as InvestigatorName, Amount,'' as SanctionEntryNo,InstitutionId as Institution,DeptId as Department  from ProjectIncentivePayDetails where [LineNo]='" + p + "' and ProjectUnit='" + projectunit11 + "' and ID='" + Pid + "' ";
            cmdString = "Select ProjectUnit,ID,EntryNo as Row ,PayedTo as InvestigatorName, Amount,'' as SanctionEntryNo,InstitutionId as Institution,DeptId as Department  from ProjectIncentivePayDetails where [LineNo]=@p  and ProjectUnit=@projectunit11 and ID=@Pid ";

            cmd = new SqlCommand(cmdString, con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@p", p);
            cmd.Parameters.AddWithValue("@projectunit11", projectunit11);
            cmd.Parameters.AddWithValue("@Pid", Pid);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable ds = new DataTable();
            da.Fill(ds);
            return ds;
        }
        catch (Exception ex)
        {
            log.Error("Inside SelectIncentiveAmountDetails1 catch block of ID: " + Pid + " unit :" + projectunit11);
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


    public string selectUIdDropdown(string username)
    {
        log.Debug("Inside function selectUIdDropdown");
        try
        {
            cmdString = "select u.User_Id, FirstName from User_M u, User_Role_Map r where u.User_Id=r.User_Id and r.Role_Id='6' ";
            con = new SqlConnection(str);
            con.Open();
            cmd = new SqlCommand(cmdString, con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.Add("@FirstName", SqlDbType.VarChar, 50);
            cmd.Parameters["@FirstName"].Value = username;
            string usrID = (string)cmd.ExecuteScalar();
            return usrID;
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

    public int updateAdditionalBU(ArrayList userBU, string username)
    {
        log.Debug("Inside function updateAdditionalBU");
        string cuid = HttpContext.Current.Session["UserId"].ToString();
        try
        {

            con = new SqlConnection(str);
            con.Open();
            transaction = con.BeginTransaction();
            //cmdString = "Delete from User_Institution_Map where User_Id='" + username + "'";
            cmdString = "Delete from User_Institution_Map where User_Id=@username";
          
            cmd = new SqlCommand(cmdString, con, transaction);
            cmd.Parameters.AddWithValue("@username", username);
            cmd.ExecuteNonQuery();

            String buu;
            String cmdString1 = "insert into User_Institution_Map (User_Id,Institute_Id) values (@UserID,@Institute_Id)";
            cmd = new SqlCommand(cmdString1, con, transaction);
            cmd.Parameters.Add("@UserID", SqlDbType.VarChar, 12);
            cmd.Parameters.Add("@Institute_Id", SqlDbType.VarChar, 10);
            int res = 0 ;
            for (int i = 0; i < userBU.Count; i++)
            {

                buu = (String)userBU[i];
                cmd.Parameters["@UserID"].Value = username;
                cmd.Parameters["@Institute_Id"].Value = buu.Trim();
                res = cmd.ExecuteNonQuery();
            }
            transaction.Commit();
            return res;
        }
        catch (Exception ex)
        {
            transaction.Rollback();
            log.Error("Inside MasterDataObject- updateAdditionalBU catch block ");
            log.Error(ex.Message);
            log.Error(ex.StackTrace);
            throw ex;
        }

        finally
        {
            con.Close();
        }
    }

    //Send Mail
    public DataSet getInvetigatorList(string id, string Type)
    {
        log.Debug("Inside function getInvetigatorList of of Project Unit: " + id + "ID: " + Type);
        try
        {
            SqlDataAdapter da;
            DataSet ds = new DataSet();
            con = new SqlConnection(str);
            con.Open();
            cmdString = " select EmailId from Projectnvestigator where ID=@ID and ProjectUnit=@ProjectType and MUNonMU='M'";
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
            log.Error("Inside Catch block of function getInvetigatorList of of Project Unit: " + id + "ID: " + Type);
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
        log.Debug("Inside function getInvietigatorListName of Project Unit: " + id + "ID: " + Type);
        try
        {
            SqlDataAdapter da;
            DataSet ds = new DataSet();

            con = new SqlConnection(str);
            con.Open();
            cmdString = " select InvestigatorName from Projectnvestigator where ID=@ID and ProjectUnit=@ProjectType";
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
            log.Error("Inside Catch block of function getInvietigatorListName of Project Unit: " + id + "ID: " + Type);
            log.Error(e.Message);
            log.Error(e.StackTrace);
            throw e;
        }
        finally
        {
            con.Close();
        }

    }


    public DataSet getReserachCoOrdinator(string p, string p_2)
    {
        log.Debug("Inside function getReserachCoOrdinator of Project Unit: " + p_2 + "ID: " + p);
        try
        {
           // SqlDataAdapter da;
           // DataSet ds = new DataSet();
           //con = new SqlConnection(str);
           // con.Open();
           //cmdString = " select EmailId from User_M u,User_Role_Map m where u.User_Id=m.User_Id and m.Role_Id=1 and u.Active='Y' and InstituteId in (Select InstitutionID from Project where Project.CreatedBy=(Select  CreatedBy from Project where ProjectUnit='" + p_2 + "' and ID='" + p + "') and ProjectUnit='" + p_2 + "' and ID='" + p + "') and Department_Id in(Select DeptID from Project where " +
           // "+Project.CreatedBy=(Select  CreatedBy from Project where  ProjectUnit='" + p_2 + "' and ID='" + p + "') and ProjectUnit='" + p_2 + "' and ID='" + p + "')"; 
           // da = new SqlDataAdapter(cmdString, con);
           // da.SelectCommand.CommandType = CommandType.Text;
           // da.Fill(ds);
           // return ds;

            SqlDataAdapter da;
            DataSet ds = new DataSet();
            con = new SqlConnection(str);
            con.Open();
            cmdString = " select EmailId from User_M u,User_Role_Map m where u.User_Id=m.User_Id and m.Role_Id=1 and u.Active='Y' and InstituteId in (Select InstitutionID from Project where Project.CreatedBy=(Select  CreatedBy from Project where ProjectUnit=@p_2  and ID=@p) and ProjectUnit=@p_2 and ID=@p) and Department_Id in(Select DeptID from Project where " +
            "+Project.CreatedBy=(Select  CreatedBy from Project where  ProjectUnit=@p_2 and ID=@p) and ProjectUnit=@p_2 and ID=@p)";
            cmd = new SqlCommand(cmdString, con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@p_2", p_2);
            cmd.Parameters.AddWithValue("@p", p);
            da = new SqlDataAdapter(cmd);
            
            da.Fill(ds);
            return ds;





        }
        catch (Exception e)
        {
            log.Error("Inside Catch block of function getReserachCoOrdinator of Project Unit: " + p_2 + "ID: " + p);
            log.Error(e.Message);
            log.Error(e.StackTrace);
            throw e;
        }
        finally
        {
            con.Close();
        }

    }

    public DataSet getGrantAuthorList(string id, string Type)
    {
        log.Debug("Inside function getGrantAuthorList of Project Unit: " + Type + "ID: " + id);
        try
        {
            SqlDataAdapter da;
            DataSet ds = new DataSet();
            con = new SqlConnection(str);
            con.Open();
            cmdString = " select EmailId from Projectnvestigator where ID=@ID and ProjectUnit=@ProjectType and InvestigatorType='P' and MUNonMU='M' and ID=@ID and ProjectUnit=@ProjectType";
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
            log.Error("Inside Catch block of function getGrantAuthorList of Project Unit: " + Type + "ID: " + id);
            log.Error(e.Message);
            log.Error(e.StackTrace);
            throw e;
        }
        finally
        {
            con.Close();
        }
    }


    public DataSet getGrantCOAuthorList(string p, string p_2)
    {
        log.Debug("Inside function getGrantCOAuthorList of Project Unit: " + p + "ID: " + p_2);
        try
        {
            SqlDataAdapter da;
            DataSet ds = new DataSet();
            con = new SqlConnection(str);
            con.Open();
            cmdString = " select EmailId from Projectnvestigator where ID=@ID and ProjectUnit=@ProjectType and InvestigatorType='C' and MUNonMU='M' and ID=@ID and ProjectUnit=@ProjectType";
            da = new SqlDataAdapter(cmdString, con);
            da.SelectCommand.Parameters.Add("@ID", SqlDbType.VarChar, 10);
            da.SelectCommand.Parameters["@ID"].Value = p;
            da.SelectCommand.Parameters.Add("@ProjectType", SqlDbType.VarChar, 5);
            da.SelectCommand.Parameters["@ProjectType"].Value = p_2;
            da.SelectCommand.CommandType = CommandType.Text;
            da.Fill(ds);
            return ds;
        }
        catch (Exception e)
        {
            log.Error("Inside Catch block of function getGrantCOAuthorList of Project Unit: " + p + "ID: " + p_2);
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
        log.Debug("Inside function getReserachFinanceList: " + HttpContext.Current.Session["ProjectUnit"].ToString());
        try
        {
            SqlDataAdapter da;
            DataSet ds = new DataSet();
            con = new SqlConnection(str);
            con.Open();
            //cmdString = " select EmailId from User_M u,User_Role_Map m where u.User_Id=m.User_Id and m.Role_Id=6 and u.Active='Y' and Unit_Id='" + HttpContext.Current.Session["ProjectUnit"].ToString() + "'";
           
           // da = new SqlDataAdapter(cmdString, con);
           // da.SelectCommand.CommandType = CommandType.Text;
            cmdString = " select EmailId from User_M u,User_Role_Map m where u.User_Id=m.User_Id and m.Role_Id=6 and u.Active='Y' and Unit_Id=@ProjectUnit";
            cmd = new SqlCommand(cmdString, con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@ProjectUnit", HttpContext.Current.Session["ProjectUnit"].ToString());
     
            da = new SqlDataAdapter(cmd);

            da.Fill(ds);
            return ds;
        }
        catch (Exception e)
        {
            log.Error("Inside Catch block of function getReserachFinanceList of Project Unit: " + HttpContext.Current.Session["ProjectUnit"].ToString());
            log.Error(e.Message);
            log.Error(e.StackTrace);

            throw e;
        }
        finally
        {
            con.Close();
        }

    }






    public DataSet getStudentlist(string id, string Type)
    {
        log.Debug("Inside function getStudentlist");
        try
        {
            SqlDataAdapter da;
            DataSet ds = new DataSet();

            con = new SqlConnection(str);
            con.Open();
            cmdString = " select EmailId from Publishcation_Author where PaublicationID=@PaublicationID and TypeOfEntry=@TypeOfEntry and (MUNonMU='S' )";
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

    public int UpdatePublicationData(PublishData j, PublishData[] JD)
    {
        log.Debug("Inside UpdatePublicationData of ID : " + j.PaublicationID + " and TypeOfEntry : " + j.TypeOfEntry);
        int result = 0;
        int result2 = 0;
        con = new SqlConnection(str);
        con.Open();
        transaction = con.BeginTransaction();
        SqlDataReader sdr = null;
        try
        {
            cmdString = "update Publication set PublishJAMonth=@PublishJAMonth,MUCategorization=@MUCategorization, PublishJAYear=@PublishJAYear,ImpactFactor=@ImpactFactor,FiveImpFact=@FiveImpFact,IF_ApplicableYear=@IF_ApplicableYear,PublicationDate=@PublishDate ,PageFrom=@PageFrom,PageTo=@PageTo ,JAVolume=@JAVolume,Issue=@Issue,ISStudentAuthor=@ISStudentAuthor where PublicationID=@ID and TypeOfEntry=@TypeOfEntry";
            cmd = new SqlCommand(cmdString, con, transaction);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@ID", j.PaublicationID);
            cmd.Parameters.AddWithValue("@TypeOfEntry", j.TypeOfEntry);
            if (j.PageFrom == "")
            {
                string pagefrom = "PF" + j.PaublicationID;
                string pageto = "PT" + j.PaublicationID;
                cmd.Parameters.AddWithValue("@PageFrom", pagefrom);
                cmd.Parameters.AddWithValue("@PageTo", pageto);

            }
            else
            {
                cmd.Parameters.AddWithValue("@PageFrom", j.PageFrom);
                cmd.Parameters.AddWithValue("@PageTo", j.PageTo);
            }

            cmd.Parameters.AddWithValue("@JAVolume", j.JAVolume);
            cmd.Parameters.AddWithValue("@Issue", j.Issue);
            cmd.Parameters.AddWithValue("@IsStudentAuthor", j.IsStudentAuthor);

            cmd.Parameters.AddWithValue("@PublishJAMonth", j.PublishJAMonth);
            cmd.Parameters.AddWithValue("@PublishJAYear", j.PublishJAYear);
            if (j.ImpactFactor != "")
            {
                cmd.Parameters.AddWithValue("@ImpactFactor", j.ImpactFactor);
            }
            else
            {
                cmd.Parameters.AddWithValue("@ImpactFactor", DBNull.Value);
            }
            if (j.ImpactFactor != "")
            {
                cmd.Parameters.AddWithValue("@FiveImpFact", j.ImpactFactor5);
            }
            else
            {
                cmd.Parameters.AddWithValue("@FiveImpFact", DBNull.Value);
            }

            if (j.IFApplicableYear != 0)
            {
                cmd.Parameters.AddWithValue("@IF_ApplicableYear", j.IFApplicableYear);
            }
            else
            {
                cmd.Parameters.AddWithValue("@IF_ApplicableYear", DBNull.Value);
            }
            cmd.Parameters.AddWithValue("@PublishDate", j.PublishDate);
            cmd.Parameters.AddWithValue("@MUCategorization", j.MUCategorization);
            result = cmd.ExecuteNonQuery();
            if (result >= 1)
            {
                //for (int i = 0; i < JD.Length; i++)
                //{
                //    if (JD[i].MUNonMU == "O")
                //    {
                //        cmdString = "update Publishcation_Author set EmployeeCode=@EmployeeCode where PaublicationID=@PaublicationID and TypeOfEntry=@TypeOfEntry  and PublicationLine=@PublicationLine ";
                //        cmd = new SqlCommand(cmdString, con, transaction);
                //        cmd.CommandType = CommandType.Text;
                //        if (JD[i].EmployeeCode != null)
                //        {
                //            cmd.Parameters.AddWithValue("@EmployeeCode", JD[i].EmployeeCode);
                //        }
                //        else
                //        {
                //            cmd.Parameters.AddWithValue("@EmployeeCode", DBNull.Value);
                //        }

                //        cmd.Parameters.AddWithValue("@PaublicationID", j.PaublicationID);
                //        cmd.Parameters.AddWithValue("@TypeOfEntry", j.TypeOfEntry);
                //        cmd.Parameters.AddWithValue("@PublicationLine", JD[i].PublicationLine);

                //    }

                //    cmd.ExecuteNonQuery();
                //}



                cmdString = "delete from Publishcation_Author where PaublicationID=@PaublicationID and TypeOfEntry=@TypeOfEntry";
                cmd = new SqlCommand(cmdString, con, transaction);
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@PaublicationID", j.PaublicationID);
                cmd.Parameters.AddWithValue("@TypeOfEntry", j.TypeOfEntry);

                result2 = cmd.ExecuteNonQuery();


                for (int i = 0; i < JD.Length; i++)
                {
                    cmd = new SqlCommand("InsertPublicationAuthor", con, transaction);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@PaublicationID", j.PaublicationID);
                    cmd.Parameters.AddWithValue("@TypeOfEntry", j.TypeOfEntry);

                    cmd.Parameters.AddWithValue("@PublicationLine", i + 1);
                    cmd.Parameters.AddWithValue("@AuthorName", JD[i].AuthorName);
                    cmd.Parameters.AddWithValue("@MUNonMU", JD[i].MUNonMU);
                    cmd.Parameters.AddWithValue("@EmployeeCode", JD[i].EmployeeCode);

                    cmd.Parameters.AddWithValue("@Institution", JD[i].Institution);
                    cmd.Parameters.AddWithValue("@Department", JD[i].Department);
                    cmd.Parameters.AddWithValue("@AuthorType", JD[i].AuthorType);


                    cmd.Parameters.AddWithValue("@isCorrAuth", JD[i].isCorrAuth);
                    cmd.Parameters.AddWithValue("@NameInJournal", JD[i].NameInJournal);
                    cmd.Parameters.AddWithValue("@EmailId", JD[i].EmailId);

                    cmd.Parameters.AddWithValue("@InstitutionName", JD[i].InstitutionName);
                    cmd.Parameters.AddWithValue("@DepartmentName", JD[i].DepartmentName);
                    if (JD[i].IsPresenter != null)
                    {
                        cmd.Parameters.AddWithValue("@IsPresenter", JD[i].IsPresenter);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@IsPresenter", DBNull.Value);

                    }
                    if (JD[i].HasAttented != null)
                    {
                        cmd.Parameters.AddWithValue("@HasAttended", JD[i].HasAttented);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@HasAttended", DBNull.Value);

                    }
                    if (JD[i].AuthorCreditPoint != null)
                    {
                        cmd.Parameters.AddWithValue("@CreditPoint", JD[i].AuthorCreditPoint);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@CreditPoint", DBNull.Value);

                    }



                    cmd.Parameters.AddWithValue("@NationalInternational", JD[i].NationalInternationl);
                    cmd.Parameters.AddWithValue("@Continent", JD[i].continental);

                    result2 = cmd.ExecuteNonQuery();

                }
            }
            int entryno = 0;
            cmdString = "Select MAX(EntryNo) as  EntryNo from PublicationUpdationTracker where PublicationID=@ID and TypeOfEntry=@TypeOfEntry ";
            cmd = new SqlCommand(cmdString, con, transaction);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@ID", j.PaublicationID);
            cmd.Parameters.AddWithValue("@TypeOfEntry", j.TypeOfEntry);
            sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                if (!Convert.IsDBNull(sdr["EntryNo"]))
                {
                    entryno = Convert.ToInt16(sdr["EntryNo"]);
                }
            }
            sdr.Close();
            if (entryno == 0)
            {
                entryno = 1;

            }
            else
            {
                entryno = entryno + 1;
            }

            cmdString = "Insert into PublicationUpdationTracker values(@ID,@TypeOfEntry,@EntryNo,@Remarks,@UpdatedBy,@UpdatedDate)";
            cmd = new SqlCommand(cmdString, con, transaction);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@ID", j.PaublicationID);
            cmd.Parameters.AddWithValue("@TypeOfEntry", j.TypeOfEntry);
            cmd.Parameters.AddWithValue("@EntryNo", entryno);
            cmd.Parameters.AddWithValue("@Remarks", j.RemarksFeedback);
            cmd.Parameters.AddWithValue("@UpdatedBy", HttpContext.Current.Session["UserId"].ToString());
            cmd.Parameters.AddWithValue("@UpdatedDate", DateTime.Now);
            cmd.ExecuteNonQuery();
            transaction.Commit();
            log.Info("Publication Data Updated Successfully of ID : " + j.PaublicationID + " and TypeOfEntry : " + j.TypeOfEntry);
            log.Info("Publication Update : User Name :" + HttpContext.Current.Session["UserName"] + "Role :" + HttpContext.Current.Session["RoleName"]);
            return result;


        }

        catch (Exception e)
        {
            transaction.Rollback();
            log.Error("Error while updating  publication data ID : " + j.PaublicationID + " and TypeOfEntry : " + j.TypeOfEntry);
            log.Error(e.Message);
            log.Error(e.StackTrace);
            throw e;
        }
        finally
        {

            con.Close();
        }
    }

 public GrantData CheckUniqueUTN(string UTN, string projectid, string projectunit)
    {
        try
        {

            GrantData data = new GrantData();
            con = new SqlConnection(str);
            con.Open();
            transaction = con.BeginTransaction();
            cmdString = " select UTN,ProjectUnit+ID as ID from Project where UTN=@UTN";

            cmd = new SqlCommand(cmdString, con, transaction);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@UTN", UTN);
            cmd.CommandType = CommandType.Text;
            SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                if (!Convert.IsDBNull(sdr["UTN"]))
                {
                    data.UTN = (string)sdr["UTN"];
                }
                if (!Convert.IsDBNull(sdr["ID"]))
                {
                    data.GID = (string)sdr["ID"];
                }
            }
            sdr.Close();
            transaction.Commit();
            return data;
        }
        catch (Exception ex)
        {

            log.Error("Inside CheckUniqueUTN catch block ProjectUnit");
            log.Error(ex.Message);
            log.Error(ex.StackTrace);
            throw ex;
        }
        finally
        {
            con.Close();
        }
    }
 
 public int UpdateUTN(GrantData j, GrantData[] JD)
 {
    try
    {

        con = new SqlConnection(str);
        con.Open();
        transaction = con.BeginTransaction();
        cmdString = "Update Project set UTN=@UTN where  ID=@ID and ProjectUnit=@GrantUnit";

        cmd = new SqlCommand(cmdString, con, transaction);
        cmd.Parameters.AddWithValue("@UTN", j.UTN);
        cmd.Parameters.AddWithValue("@ID", j.GID);
        cmd.Parameters.AddWithValue("@GrantUnit",j.GrantUnit);
        cmd.CommandType = CommandType.Text;
        int data = cmd.ExecuteNonQuery();

        if (data > 0)
        {

            try
            {
                
                cmd = new SqlCommand("InsertBackuptable", con, transaction);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Remarks", j.Remarks);
                cmd.Parameters.AddWithValue("@ProjectUnit", j.GrantUnit);
                cmd.Parameters.AddWithValue("@ID", j.GID);
                cmd.Parameters.AddWithValue("@FromUTN", j.FromUTN);
                cmd.Parameters.AddWithValue("@ToUTN", j.UTN);
                cmd.Parameters.AddWithValue("@CreatedBy", j.CreatedBy);
                cmd.Parameters.AddWithValue("@CreatedDate", j.CreatedDate);
                cmd.ExecuteNonQuery();
               
            }

            catch (Exception ex)
            {
                log.Error(ex.Message);
                log.Error(ex.StackTrace);
                throw ex;
            }

        }


        transaction.Commit();
        return data;
    }
    catch(Exception ex)
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



public int insertbackupUTN(GrantData j, GrantData[] JD)
 {
     try
     {
         con = new SqlConnection(str);
         con.Open();
         transaction = con.BeginTransaction();
         cmd = new SqlCommand("InsertBackuptable", con, transaction);
         cmd.CommandType = CommandType.StoredProcedure;
         cmd.Parameters.AddWithValue("@Remarks", j.Remarks);
         cmd.Parameters.AddWithValue("@ProjectUnit",j.GrantUnit);
         cmd.Parameters.AddWithValue("@ID",j.GID);
         cmd.Parameters.AddWithValue("@FromUTN", j.FromUTN);
         cmd.Parameters.AddWithValue("@ToUTN", j.UTN);
         cmd.Parameters.AddWithValue("@CreatedBy",j.CreatedBy);
         cmd.Parameters.AddWithValue("@CreatedDate",j.CreatedDate);
         int data = cmd.ExecuteNonQuery();
         transaction.Commit();
         return data;
     }

     catch(Exception ex)
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





public string  selectoldutn(string projectid, string projectunit)
{
    try
    {
        GrantData data = new GrantData();
        con = new SqlConnection(str);
        con.Open();
        transaction = con.BeginTransaction();
        cmdString = "select UTN from Project where ID=@ID and ProjectUnit=@ProjectUnit";
        cmd = new SqlCommand(cmdString, con, transaction);
        cmd.Parameters.AddWithValue("@ProjectUnit", projectunit);
        cmd.Parameters.AddWithValue("@ID", projectid);
        cmd.ExecuteNonQuery();
        SqlDataReader sdr = cmd.ExecuteReader();
        while (sdr.Read())
        {
            if (!Convert.IsDBNull(sdr["UTN"]))
            {
               data.UTN = (string)sdr["UTN"];
            }

        }

        sdr.Close();
        transaction.Commit();
        string datautn = Convert.ToString(data.UTN);
        return datautn;
         
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



public string getUserMailIdList(String userid)
{
    try
    {
        GrantData data = new GrantData();  
        con = new SqlConnection(str);
        con.Open();
        cmdString = " select EmailId from User_M  where User_Id=@ID";

        cmd = new SqlCommand(cmdString, con);
        cmd.Parameters.AddWithValue("@ID", userid);
        cmd.ExecuteNonQuery();
        SqlDataReader sdr = cmd.ExecuteReader();
        while (sdr.Read())
        {
            if (!Convert.IsDBNull(sdr["EmailId"]))
            {
                data.EmailId = (string)sdr["EmailId"];
            }

        }
        string dataresult = Convert.ToString(data.EmailId);
        sdr.Close();
        return dataresult;
         
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

public DataSet getInvetigatorDETAIL(string id, string Type)
{
    log.Debug("Inside function getInvetigatorList of of Project Unit: " + id + "ID: " + Type);
    try
    {
        SqlDataAdapter da;
        DataSet ds = new DataSet();
        con = new SqlConnection(str);
        con.Open();
        cmdString = " select InvestigatorName, EmployeeCode,MUNonMU,Institution,Department,InvestigatorType from Projectnvestigator where ID=@ID and ProjectUnit=@ProjectType and MUNonMU='M'";
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
        log.Error("Inside Catch block of function getInvetigatorList of of Project Unit: " + id + "ID: " + Type);
        log.Error(e.Message);
        log.Error(e.StackTrace);

        throw e;
    }
    finally
    {
        con.Close();
    }
}

public int insertEmailtracker(string p1, EmailDetails details, string p2)
{
    log.Debug("Inside function insertEmailtracker of of Project ID: " + p2 );
    try
    {
        con = new SqlConnection(str);
        con.Open();
        transaction = con.BeginTransaction();
        cmd = new SqlCommand("insert into  EmailQueueTrackerTable (Author_InvestigatorName,Publication_ProjectID,Module,subject ) values (@Author_InvestigatorName,@Publication_ProjectID,@Module,@subject)", con, transaction);
        cmd.CommandType = CommandType.Text;
        cmd.Parameters.AddWithValue("@Author_InvestigatorName", p1);
        cmd.Parameters.AddWithValue("@Publication_ProjectID", p2);
        cmd.Parameters.AddWithValue("@Module", details.Module);
        cmd.Parameters.AddWithValue("@subject", details.EmailSubject);
        int data = cmd.ExecuteNonQuery();
        transaction.Commit();
        return data;
    }
    catch (Exception e)
    {
        log.Error("Inside Catch block of function getInvetigatorList of of Project ID: " +p2);
        log.Error(e.Message);
        log.Error(e.StackTrace);

        throw e;
    }
    finally
    {
        con.Close();
    }
}

 public GrantData CheckUniqueId(string p1, string p2, EmailDetails details)
{
    log.Debug("Inside function CheckUniqueId of of Project ID: " + p1 );
    try
    {
      
        GrantData data = new GrantData();
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
        log.Error("Inside Catch block of function CheckUniqueId of of Project ID: " + p1);
        log.Error(e.Message);
        log.Error(e.StackTrace);

        throw e;
    }
           finally
            {
        con.Close();
              }
}

 public int updateEmailtracker(string Publication_ProjectID, string p2, EmailDetails details, GrantData obj)
 {
     log.Debug("Inside function updateEmailtracker of of Project ID: " + Publication_ProjectID);
      try
      {
          con = new SqlConnection(str);
          con.Open();
          transaction = con.BeginTransaction();
          //cmdString = "update EmailQueueTrackerTable set EmailqueueId='" + obj.ID + "',RefferenceID='"+obj.RefID+"'  where Publication_ProjectID='" + Publication_ProjectID + "' and subject='"+details.EmailSubject+"' and Module='"+details.Module+"' ";
          cmdString = "update EmailQueueTrackerTable set EmailqueueId=@ID,RefferenceID=@RefID  where Publication_ProjectID=@Publication_ProjectID and subject=@EmailSubject and Module=@Module";
         
          cmd = new SqlCommand(cmdString, con, transaction);
          cmd.Parameters.AddWithValue("@ID", obj.ID);
          cmd.Parameters.AddWithValue("@RefID", obj.RefID);
          cmd.Parameters.AddWithValue("@Publication_ProjectID", Publication_ProjectID);
          cmd.Parameters.AddWithValue("@EmailSubject", details.EmailSubject);
          cmd.Parameters.AddWithValue("@Module", details.Module);
          cmd.CommandType = CommandType.Text;
          int data = cmd.ExecuteNonQuery();
          transaction.Commit();
          return data;
      }
      catch (Exception e)
      {
          log.Error("Inside Catch block of function updateEmailtracker of of Project ID: " + Publication_ProjectID);
          log.Error(e.Message);
          log.Error(e.StackTrace);

          throw e;
      }
           finally
          {
        con.Close();
          }
 }


 public int insertSeedMoneyEntry(SeedMoney s, SeedMoney[] JD)
 {
     log.Debug("Inside insertSeedMoneyEntry function of ID: " + s.Id);
     int result = 0, result1 = 0, seed = 0, seed1 = 0, year1 = 0;
     bool result2 = false;

     string seedFinal = "";
     con = new SqlConnection(str);
     con.Open();
     transaction = con.BeginTransaction();
     try
     {

         cmdString = "select seed from Id_Gen_SeedMoney";
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

         DateTime date = Convert.ToDateTime(s.AppliedDate);
         int yearvalue = date.Year;
         int resultvalue = 0;

         HttpContext.Current.Session["seedmoney"] = seedFinal;
         cmd = new SqlCommand("InsertSeedMoneyDetails", con, transaction);
         cmd.CommandType = CommandType.StoredProcedure;    
         cmd.Parameters.AddWithValue("@ID", seedFinal);
         cmd.Parameters.AddWithValue("@Title",s.Title);
         cmd.Parameters.AddWithValue("@Writeup",s.Writeup);   
         cmd.Parameters.AddWithValue("@Budget", s.Budget);
         if (s.AppliedDate.ToShortDateString() != "01/01/0001")
         {
             cmd.Parameters.AddWithValue("@AppliedDate", s.AppliedDate);
         }
         else
         {
             cmd.Parameters.AddWithValue("@AppliedDate", DBNull.Value);
         }
         
         cmd.Parameters.AddWithValue("@Status", s.Status);
         cmd.Parameters.AddWithValue("@CreatedBy", s.CreatedBy);
         cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
         cmd.Parameters.AddWithValue("@Institution", s.InstUser);
         cmd.Parameters.AddWithValue("@Department", s.DeptUser);
         cmd.Parameters.AddWithValue("@EntryType", s.Entrytype);
         if (s.Entrytype == "S")
         {
             
           cmd.Parameters.AddWithValue("@cycleid", DBNull.Value);
             
         }
         else
         {
             if (s.cycleid != null)
             {
                 cmd.Parameters.AddWithValue("@cycleid", s.cycleid);
             }
             else
             {
                 cmd.Parameters.AddWithValue("@cycleid", DBNull.Value);
             }
         }
         result = cmd.ExecuteNonQuery();

         log.Info("SeedMoney Details inserted sucessfully  of ID: " + s.Id);
         if (result >= 1)
         {
             for (int i = 0; i < JD.Length; i++)
             {
                 cmd = new SqlCommand("InsertSeedMoneyInvestigator", con, transaction);
                 cmd.CommandType = CommandType.StoredProcedure;
                 cmd.Parameters.AddWithValue("@ID", seedFinal);
                 cmd.Parameters.AddWithValue("@EntryNo", i + 1);
                 cmd.Parameters.AddWithValue("@InvestigatorName", JD[i].AuthorName);
                 cmd.Parameters.AddWithValue("@MUNonMU", JD[i].MUNonMU);
                 cmd.Parameters.AddWithValue("@EmployeeCode", JD[i].EmployeeCode);

                 cmd.Parameters.AddWithValue("@Institution", JD[i].Institution);
                 cmd.Parameters.AddWithValue("@Department", JD[i].Department);

                 cmd.Parameters.AddWithValue("@InstitutionName", JD[i].InstitutionName);
                 cmd.Parameters.AddWithValue("@DepartmentName", JD[i].DepartmentName);
                 cmd.Parameters.AddWithValue("@InvestigatorType", JD[i].AuthorType);

                 if (JD[i].AuthorType == "P" && JD[i].LeadPI == "Y")
                 {

                     cmd.Parameters.AddWithValue("@isLeadPI", "Y");
                 }
                 else if (JD[i].AuthorType == "P" && JD[i].LeadPI == "N")
                 {

                     cmd.Parameters.AddWithValue("@isLeadPI", "N");
                 }
                 else
                 {
                     cmd.Parameters.AddWithValue("@isLeadPI", DBNull.Value);
                 }
                 cmd.Parameters.AddWithValue("@NationalInternational", JD[i].NationalInternationl);
                 cmd.Parameters.AddWithValue("@Continent", JD[i].continental);
                 cmd.Parameters.AddWithValue("@EmailId", JD[i].EmailId);
                 result1 = cmd.ExecuteNonQuery();

                 log.Info("SeedMoney investigator details inserted sucessfully  of Id: " + s.Id);
             }
             cmdString = "Select count(EntryNo ) as Count from SeedMoneyStatustracker where  ID=@ID";
             cmd = new SqlCommand(cmdString, con, transaction);
             cmd.CommandType = CommandType.Text;
             cmd.Parameters.AddWithValue("@ID", seedFinal);
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

             cmd = new SqlCommand("Insert  SeedMoneyStatustracker (ID,EntryNo,Status,UpdatedUser,Date) values (@ID,@EntryNo,@Status,@UpdatedUser,@Date)", con, transaction);
             cmd.CommandType = CommandType.Text;
             cmd.Parameters.AddWithValue("@ID", seedFinal);
             cmd.Parameters.AddWithValue("@EntryNo", count + 1);
             cmd.Parameters.AddWithValue("@Status",s.Status );
             cmd.Parameters.AddWithValue("@UpdatedUser",s.CreatedBy);
             cmd.Parameters.AddWithValue("@Date", DateTime.Now);
             result = cmd.ExecuteNonQuery();

             log.Info("Inserted into SeedMoneyStatustracker of ID: " + seedFinal);

         }
         transaction.Commit();
         return result;
     }

     catch (Exception ex)
     {
         log.Error("Inside insertSeedMoneyEntry catch block of Id: " + s.Id);
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

 public SeedMoney fnfindSeedid(string SID)
 {
     log.Debug("Inside fnfindSeedid function,  ID: " + SID);
     try
     {
         cmdString = " select * from SeedMoneyDetails where ID=@ID  ";
         con = new SqlConnection(str);
         con.Open();
         cmd = new SqlCommand(cmdString, con);
         cmd.Parameters.Add("@ID", SqlDbType.VarChar, 10);
         cmd.Parameters["@ID"].Value = SID;
         cmd.CommandType = CommandType.Text;
         SqlDataReader sdr = cmd.ExecuteReader();

         SeedMoney V = new SeedMoney();

         while (sdr.Read())
         {
             if (!Convert.IsDBNull(sdr["ID"]))
             {
                 V.Id = (string)sdr["ID"];
             }
             else if (Convert.IsDBNull(sdr["UTN"]))
             {
                 V.Id = "";
             }
             if (!Convert.IsDBNull(sdr["Title"]))
             {
                 V.Title = (string)sdr["Title"];
             }
             else if (Convert.IsDBNull(sdr["Title"]))
             {
                 V.Title = "";
             }
             if (!Convert.IsDBNull(sdr["Writeup"]))
             {
                 V.Writeup = (string)sdr["Writeup"];
             }
             else if (Convert.IsDBNull(sdr["Writeup"]))
             {
                 V.Writeup = "";
             }

             if (!Convert.IsDBNull(sdr["Budget"]))
             {
                 V.Budget = Convert.ToDouble((decimal)(sdr["Budget"]));
             }
             else if (Convert.IsDBNull(sdr["Budget"]))
             {
                 V.Budget = 0;
             }
             if (!Convert.IsDBNull(sdr["AppliedDate"]))
             {
                 V.AppliedDate = (DateTime)sdr["AppliedDate"];
             }
             if (!Convert.IsDBNull(sdr["Status"]))
             {
                 V.Status = (string)sdr["Status"];
             }
             else if (Convert.IsDBNull(sdr["Status"]))
             {
                 V.Status = "";
             }
             if (!Convert.IsDBNull(sdr["CreatedBy"]))
             {
                 V.CreatedBy = (string)sdr["CreatedBy"];
             }
             else if (Convert.IsDBNull(sdr["CreatedBy"]))
             {
                 V.CreatedBy = "";
             }

             if (!Convert.IsDBNull(sdr["CreatedDate"]))
             {
                 V.CreatedDate = (DateTime)sdr["CreatedDate"];
             }

             if (!Convert.IsDBNull(sdr["Institution"]))
             {
                 V.Institution = (string)sdr["Institution"];
             }
             else if (Convert.IsDBNull(sdr["Institution"]))
             {
                 V.Institution = "";
             }

             if (!Convert.IsDBNull(sdr["Department"]))
             {
                 V.Department = (string)sdr["Department"];
             }
             else if (Convert.IsDBNull(sdr["Department"]))
             {
                 V.Department = "";
             }
            
             if (!Convert.IsDBNull(sdr["Comments"]))
             {
                 V.Comments = (string)sdr["Comments"];
             }
             else if (Convert.IsDBNull(sdr["Comments"]))
             {
                 V.Comments = "";
             }
             if (!Convert.IsDBNull(sdr["Remarks"]))
             {
                 V.Remarks = (string)sdr["Remarks"];
             }
             else if (Convert.IsDBNull(sdr["Remarks"]))
             {
                 V.Remarks = "";
             }
           
             if (!Convert.IsDBNull(sdr["ApprovedBudget"]))
             {
                 V.ApprovedBudget = Convert.ToDouble((decimal)(sdr["ApprovedBudget"]));
             }
             else if (Convert.IsDBNull(sdr["ApprovedBudget"]))
             {
                 V.ApprovedBudget = 0;
             }
             if (!Convert.IsDBNull(sdr["EntryType"]))
             {
                 V.Entrytype =(string)sdr["EntryType"];
             }
             else if (Convert.IsDBNull(sdr["EntryType"]))
             {
                 V.Entrytype = "";
             }
             if (!Convert.IsDBNull(sdr["IsAutoApproved"]))
             {
                 V.IsAutoApproved = (string)sdr["IsAutoApproved"];
             }
             else if (Convert.IsDBNull(sdr["IsAutoApproved"]))
             {
                 V.IsAutoApproved ="0";
             }
             if (!Convert.IsDBNull(sdr["cycleid"]))
             {
                 V.cycleid = (int)sdr["cycleid"];
             }
             else if (Convert.IsDBNull(sdr["cycleid"]))
             {
                 V.cycleid = 0;
             }
         }
         return V;
     }
     catch (Exception ex)
     {
         log.Error("Inside fnfindSeedid catch block of  ID: " + SID);
         log.Error("Erroe Message : " + ex.Message);
         log.Error(ex.StackTrace);
         throw ex;
     }

     finally
     {
         con.Close();
     }
 }

 public DataTable fnfindseedMoneyInvestigatorDetails(string SID)
 {
     log.Debug("Inside fnfindseedMoneyInvestigatorDetails function, ID: " + SID);

     con = new SqlConnection(str);
     con.Open();
     try
     {
         SqlDataAdapter da;
         DataTable ds;
         cmdString = "select MUNonMU  as DropdownMuNonMu,EmployeeCode,InvestigatorName as AuthorName,Institution,Department, DepartmentName,InstitutionName ,  EmailId as MailId,InvestigatorType as AuthorType,isLeadPI,NationalInternational as NationalType,Continent as ContinentId from SeedMoneyInvestigator where ID=@ID ";
         cmd = new SqlCommand(cmdString, con);
         cmd.CommandType = CommandType.Text;

         cmd.Parameters.Add("@ID", SqlDbType.VarChar, 15);
         cmd.Parameters["@ID"].Value = SID;

         
         da = new SqlDataAdapter(cmd);

         ds = new DataTable();
         da.Fill(ds);

         return ds;
     }

     catch (Exception ex)
     {
         log.Error("Inside fnfindseedMoneyInvestigatorDetails catch block ID: " + SID);
         log.Error(ex.Message);
         log.Error(ex.StackTrace);
         throw ex;
     }

     finally
     {
         con.Close();
     }
 }

 public int UpdateSeedMoneyEntry(SeedMoney s, SeedMoney[] JD)
 {
     log.Debug("Inside UpdateSeedMoneyEntry function of ID: " + s.Id);
     int result = 0, result1 = 0;
     con = new SqlConnection(str);
     con.Open();
     transaction = con.BeginTransaction();
     try
     {
         cmd = new SqlCommand("UpdateSeedMoneyEntry", con, transaction);
         cmd.CommandType = CommandType.StoredProcedure;
         cmd.Parameters.AddWithValue("@ID", s.Id);
         cmd.Parameters.AddWithValue("@Title", s.Title);
         cmd.Parameters.AddWithValue("@Writeup", s.Writeup);
         cmd.Parameters.AddWithValue("@Budget", s.Budget);
         if (s.AppliedDate.ToShortDateString() != "01/01/0001")
         {
             cmd.Parameters.AddWithValue("@AppliedDate", s.AppliedDate);
         }
         else
         {
             cmd.Parameters.AddWithValue("@AppliedDate", DBNull.Value);
         }

         cmd.Parameters.AddWithValue("@Status", s.Status);
         //cmd.Parameters.AddWithValue("@CreatedBy", s.CreatedBy);
         //cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
         cmd.Parameters.AddWithValue("@Institution", s.InstUser);
         cmd.Parameters.AddWithValue("@Department", s.DeptUser);
         if (s.Entrytype == "S")
         {
             cmd.Parameters.AddWithValue("@cycleid", DBNull.Value);
         }
         else
         {
             if (s.cycleid != null)
             {
                 cmd.Parameters.AddWithValue("@cycleid", s.cycleid);
             }
             else
             {
                 cmd.Parameters.AddWithValue("@cycleid", DBNull.Value);
             }
         }
         result = cmd.ExecuteNonQuery();
         log.Debug("seed Data Updated Sucessfully of ID: " + s.Id);

         if (result == 1)
         {
             cmdString = "delete from  SeedMoneyInvestigator  where ID=@ID ";
             cmd = new SqlCommand(cmdString, con, transaction);
             cmd.CommandType = CommandType.Text;
             cmd.Parameters.AddWithValue("@ID", s.Id);
             result1 = cmd.ExecuteNonQuery();


             for (int i = 0; i < JD.Length; i++)
             {
                 cmd = new SqlCommand("InsertSeedMoneyInvestigator", con, transaction);
                 cmd.CommandType = CommandType.StoredProcedure;
                 cmd.Parameters.AddWithValue("@ID", s.Id);
                 cmd.Parameters.AddWithValue("@EntryNo", i + 1);
                 cmd.Parameters.AddWithValue("@InvestigatorName", JD[i].AuthorName);
                 cmd.Parameters.AddWithValue("@MUNonMU", JD[i].MUNonMU);
                 cmd.Parameters.AddWithValue("@EmployeeCode", JD[i].EmployeeCode);

                 cmd.Parameters.AddWithValue("@Institution", JD[i].Institution);
                 cmd.Parameters.AddWithValue("@Department", JD[i].Department);

                 cmd.Parameters.AddWithValue("@InstitutionName", JD[i].InstitutionName);
                 cmd.Parameters.AddWithValue("@DepartmentName", JD[i].DepartmentName);
                 cmd.Parameters.AddWithValue("@InvestigatorType", JD[i].AuthorType);

                 if (JD[i].AuthorType == "P" && JD[i].LeadPI == "Y")
                 {

                     cmd.Parameters.AddWithValue("@isLeadPI", "Y");
                 }
                 else if (JD[i].AuthorType == "P" && JD[i].LeadPI == "N")
                 {

                     cmd.Parameters.AddWithValue("@isLeadPI", "N");
                 }
                 else
                 {
                     cmd.Parameters.AddWithValue("@isLeadPI", DBNull.Value);
                 }
                 cmd.Parameters.AddWithValue("@NationalInternational", JD[i].NationalInternationl);
                 cmd.Parameters.AddWithValue("@Continent", JD[i].continental);
                 cmd.Parameters.AddWithValue("@EmailId", JD[i].EmailId);
                 result1 = cmd.ExecuteNonQuery();

                 log.Info("SeedMoney investigator details Updated sucessfully  of Id: " + s.Id);
             }
             cmdString = "Select count(EntryNo ) as Count from SeedMoneyStatustracker where  ID=@ID";
             cmd = new SqlCommand(cmdString, con, transaction);
             cmd.CommandType = CommandType.Text;
             cmd.Parameters.AddWithValue("@ID", s.Id);
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

             cmd = new SqlCommand("Insert  SeedMoneyStatustracker (ID,EntryNo,Status,UpdatedUser,Date) values (@ID,@EntryNo,@Status,@UpdatedUser,@Date)", con, transaction);
             cmd.CommandType = CommandType.Text;
             cmd.Parameters.AddWithValue("@ID", s.Id);
             cmd.Parameters.AddWithValue("@EntryNo", count + 1);
             cmd.Parameters.AddWithValue("@Status", s.Status);
             cmd.Parameters.AddWithValue("@UpdatedUser", s.CreatedBy);
             cmd.Parameters.AddWithValue("@Date", DateTime.Now);
             result = cmd.ExecuteNonQuery();

             log.Info("Inserted into SeedMoneyStatustracker of ID: " + s.Id);

         }


         transaction.Commit();
         return result1;
     }

     catch (Exception ex)
     {
         log.Error("Inside UpdateSeedMoneyEntry catch block of Id: " + s.Id);
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


 public int UpdatePfPathCreateSeedMoney(string SID,SeedMoney j)
 {
     int result = 0, result1 = 0;
     con = new SqlConnection(str);
     con.Open();
     transaction = con.BeginTransaction();
     try
     {
         cmdString = "update SeedMoneyDetails set UploadFilePath=@UploadFilePath where ID=@ID";
         cmd = new SqlCommand(cmdString, con, transaction);
         cmd.CommandType = CommandType.Text;
         cmd.Parameters.AddWithValue("@ID", SID);
         cmd.Parameters.AddWithValue("@UploadFilePath", j.FilePath);
         result = cmd.ExecuteNonQuery();
         transaction.Commit();
         return result;
     }

     catch (Exception ex)
     {
         log.Error("Inside UpdatePfPathCreateSeedMoney catch block ");
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

 public string GetFileUploadPathSeedMoney(string p)
 {
     try
     {
         cmdString = "select * from SeedMoneyDetails  where ID=@ID ";
         con = new SqlConnection(str);
         con.Open();
         cmd = new SqlCommand(cmdString, con);
         cmd.Parameters.Add("@ID", SqlDbType.VarChar, 15);
         cmd.Parameters["@ID"].Value = p;
        
         cmd.CommandType = CommandType.Text;
         SqlDataReader sdr = cmd.ExecuteReader();
         string fileuploadP = null;

         while (sdr.Read())
         {

             if (!Convert.IsDBNull(sdr["UploadFilePath"]))
             {
                 fileuploadP = (string)sdr["UploadFilePath"];
             }
             else if (Convert.IsDBNull(sdr["UploadFilePath"]))
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

 public bool UpdateStatusReworkSeedMoneyEntry(SeedMoney s)
 {
     log.Debug("Inside UpdateStatusReworkSeedMoneyEntry of Id: " +s.Id );
     bool result = false;
     int result1 = 0;
     con = new SqlConnection(str);
     con.Open();
     transaction = con.BeginTransaction();
     try
     {
             log.Debug("Inside UpdateStatusReworkSeedMoneyEntry to update the status to :" + s.Status+ "ID: " + s.Id);
             cmdString = "update SeedMoneyDetails set Status=@Status,Comments=@Comments where ID=@ID ";
             cmd = new SqlCommand(cmdString, con, transaction);
             cmd.CommandType = CommandType.Text;
             cmd.Parameters.AddWithValue("@ID", s.Id);
             cmd.Parameters.AddWithValue("@Status", s.Status);
             cmd.Parameters.AddWithValue("@Comments", s.Comments);
             result = Convert.ToBoolean(cmd.ExecuteNonQuery());
             if (result == true)
             {
                 cmdString = "Select count(EntryNo ) as Count from SeedMoneyStatustracker where  ID=@ID";
                 cmd = new SqlCommand(cmdString, con, transaction);
                 cmd.CommandType = CommandType.Text;
                 cmd.Parameters.AddWithValue("@ID", s.Id);
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

                 cmd = new SqlCommand("Insert  SeedMoneyStatustracker (ID,EntryNo,Status,UpdatedUser,Date,Comments) values (@ID,@EntryNo,@Status,@UpdatedUser,@Date,@Comments)", con, transaction);
                 cmd.CommandType = CommandType.Text;
                 cmd.Parameters.AddWithValue("@ID", s.Id);
                 cmd.Parameters.AddWithValue("@EntryNo", count + 1);
                 cmd.Parameters.AddWithValue("@Status", s.Status);
                 cmd.Parameters.AddWithValue("@UpdatedUser", s.UpdatedBy);
                 cmd.Parameters.AddWithValue("@Date", DateTime.Now);
                 cmd.Parameters.AddWithValue("@Comments", s.Comments);
                 result1 = cmd.ExecuteNonQuery();
             }
             log.Info(" ID: " + s.Id + " and  status changed to :" + s.Status);
             log.Info("Grant Rework : User Name :" + HttpContext.Current.Session["UserName"] + "Role :" + HttpContext.Current.Session["RoleName"]);
         transaction.Commit();
         return result;
     }

     catch (Exception ex)
     {
         log.Error(" Inside UpdateStatusReworkSeedMoneyEntry of ID: " + s.Id);
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

 public bool UpdateStatusApproveSeedMoneyEntry(SeedMoney s)
 {
     log.Debug("Inside UpdateStatusApproveSeedMoneyEntry of Id: " + s.Id);
     bool result = false;
      int result1 = 0;
     con = new SqlConnection(str);
     con.Open();
     transaction = con.BeginTransaction();
     try
     {     
             log.Debug("Inside UpdateStatusApproveSeedMoneyEntry to update the status to :" + s.Status + "ID: " + s.Id);
             cmdString = "update SeedMoneyDetails set Status=@Status,Remarks=@Remarks,ApprovedBudget=@ApprovedBudget,Comments=@Comments where ID=@ID ";
             cmd = new SqlCommand(cmdString, con, transaction);
             cmd.CommandType = CommandType.Text;
             cmd.Parameters.AddWithValue("@ID", s.Id);
             cmd.Parameters.AddWithValue("@Status", s.Status);
             cmd.Parameters.AddWithValue("@Remarks", s.Remarks);
             cmd.Parameters.AddWithValue("@ApprovedBudget", s.ApprovedBudget);
             cmd.Parameters.AddWithValue("@Comments", s.Comments);
             result = Convert.ToBoolean(cmd.ExecuteNonQuery());
         if (result==true)
         {
              cmdString = "Select count(EntryNo ) as Count from SeedMoneyStatustracker where  ID=@ID";
             cmd = new SqlCommand(cmdString, con, transaction);
             cmd.CommandType = CommandType.Text;
             cmd.Parameters.AddWithValue("@ID", s.Id);
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

             cmd = new SqlCommand("Insert  SeedMoneyStatustracker (ID,EntryNo,Status,UpdatedUser,Date,Comments) values (@ID,@EntryNo,@Status,@UpdatedUser,@Date,@Comments)", con, transaction);
             cmd.CommandType = CommandType.Text;
             cmd.Parameters.AddWithValue("@ID", s.Id);
             cmd.Parameters.AddWithValue("@EntryNo", count + 1);
             cmd.Parameters.AddWithValue("@Status", s.Status);
             cmd.Parameters.AddWithValue("@UpdatedUser", s.UpdatedBy);
             cmd.Parameters.AddWithValue("@Date", DateTime.Now);
             cmd.Parameters.AddWithValue("@Comments", s.Comments);
             result1 = cmd.ExecuteNonQuery();
         }
             log.Info("Inserted into SeedMoneyStatustracker of ID: " + s.Id);
             log.Info(" ID: " + s.Id + " and  status changed to :" + s.Status);
             log.Info("Seed Approve: User Name :" + HttpContext.Current.Session["UserName"] + "Role :" + HttpContext.Current.Session["RoleName"]);
         transaction.Commit();
         return result;
     }

     catch (Exception ex)
     {
         log.Error(" Inside UpdateStatusApproveSeedMoneyEntry of ID: " + s.Id);
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

 public DataSet getInvetigatorListforseed(string p)
 {
     log.Debug("Inside function getInvetigatorListforseed  of ID: " + p);
     try
     {
         SqlDataAdapter da;
         DataSet ds = new DataSet();
         con = new SqlConnection(str);
         con.Open();
         cmdString = " select EmailId from SeedMoneyInvestigator where ID=@ID and MUNonMU!='N' ";
         da = new SqlDataAdapter(cmdString, con);
         da.SelectCommand.Parameters.Add("@ID", SqlDbType.VarChar, 10);
         da.SelectCommand.Parameters["@ID"].Value = p;
         da.SelectCommand.CommandType = CommandType.Text;
         da.Fill(ds);
         return ds;
     }
     catch (Exception e)
     {
         log.Error("Inside Catch block of function getInvetigatorListforseed of ID: " + p);
         log.Error(e.Message);
         log.Error(e.StackTrace);

         throw e;
     }
     finally
     {
         con.Close();
     }
 }

 public DataSet getInvetigatorDETAILforseed(string p)
 {
     log.Debug("Inside function getInvetigatorDETAILforseed of ID: " + p);
     try
     {
         SqlDataAdapter da;
         DataSet ds = new DataSet();
         con = new SqlConnection(str);
         con.Open();
         cmdString = " select InvestigatorName, EmployeeCode,MUNonMU,Institution,Department,InvestigatorType from SeedMoneyInvestigator where ID=@ID and MUNonMU!='N'";
         da = new SqlDataAdapter(cmdString, con);
         da.SelectCommand.Parameters.Add("@ID", SqlDbType.VarChar, 10);
         da.SelectCommand.Parameters["@ID"].Value = p;
         da.SelectCommand.CommandType = CommandType.Text;
         da.Fill(ds);
         return ds;
     }
     catch (Exception e)
     {
         log.Error("Inside Catch block of function getInvetigatorDETAILforseed of ID: " + p);
         log.Error(e.Message);
         log.Error(e.StackTrace);

         throw e;
     }
     finally
     {
         con.Close();
     }
 }

 public DataSet getReserachDirector(string p)
 {
     log.Debug("Inside function getReserachDirector of ID: " + p);
     try
     {
         SqlDataAdapter da;
         DataSet ds = new DataSet();

         con = new SqlConnection(str);
         con.Open();
         cmdString = "select EmailId from User_M u,User_Role_Map m where u.User_Id=m.User_Id and m.Role_Id=2 and u.Active='Y'";
         da = new SqlDataAdapter(cmdString, con);
         da.SelectCommand.CommandType = CommandType.Text;
         da.Fill(ds);

         return ds;
     }
     catch (Exception e)
     {
         log.Error("Inside Catch block of function getReserachDirector of ID: " + p);
         log.Error(e.Message);
         log.Error(e.StackTrace);
         throw e;
     }
     finally
     {
         con.Close();
     }
 }

 public DataSet getInvietigatorListNameofSeed(string p)
 {
     log.Debug("Inside function getInvietigatorListNameofSeed of ID: " + p);
     try
     {
         SqlDataAdapter da;
         DataSet ds = new DataSet();

         con = new SqlConnection(str);
         con.Open();
         cmdString = "select InvestigatorName from SeedMoneyInvestigator where ID=@ID";
         da = new SqlDataAdapter(cmdString, con);
         da.SelectCommand.Parameters.Add("@ID", SqlDbType.VarChar, 10);
         da.SelectCommand.Parameters["@ID"].Value = p;
         da.SelectCommand.CommandType = CommandType.Text;
         da.Fill(ds);
         return ds;
     }
     catch (Exception e)
     {
         log.Error("Inside Catch block of function getInvietigatorListNameofSeed of ID: " + p);
         log.Error(e.Message);
         log.Error(e.StackTrace);
         throw e;
     }
     finally
     {
         con.Close();
     }

 }


 public bool UpdateStatusRejectSeedMoneyEntry(SeedMoney s)
 {
     log.Debug("Inside UpdateStatusRejectSeedMoneyEntry of Id: " + s.Id);
     bool result = false;
     int result1 = 0;
     con = new SqlConnection(str);
     con.Open();
     transaction = con.BeginTransaction();
     try
     {
         log.Debug("Inside UpdateStatusRejectSeedMoneyEntry to update the status to :" + s.Status + "ID: " + s.Id);
         cmdString = "update SeedMoneyDetails set Status=@Status,Comments=@Comments where ID=@ID ";
             cmd = new SqlCommand(cmdString, con, transaction);
             cmd.CommandType = CommandType.Text;
             cmd.Parameters.AddWithValue("@ID", s.Id);
             cmd.Parameters.AddWithValue("@Status", s.Status);
             cmd.Parameters.AddWithValue("@Comments", s.Comments);
             result = Convert.ToBoolean(cmd.ExecuteNonQuery());
             if (result == true)
             {
                 cmdString = "Select count(EntryNo ) as Count from SeedMoneyStatustracker where  ID=@ID";
                 cmd = new SqlCommand(cmdString, con, transaction);
                 cmd.CommandType = CommandType.Text;
                 cmd.Parameters.AddWithValue("@ID", s.Id);
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

                 cmd = new SqlCommand("Insert  SeedMoneyStatustracker (ID,EntryNo,Status,UpdatedUser,Date,Comments) values (@ID,@EntryNo,@Status,@UpdatedUser,@Date,@Comments)", con, transaction);
                 cmd.CommandType = CommandType.Text;
                 cmd.Parameters.AddWithValue("@ID", s.Id);
                 cmd.Parameters.AddWithValue("@EntryNo", count + 1);
                 cmd.Parameters.AddWithValue("@Status", s.Status);
                 cmd.Parameters.AddWithValue("@UpdatedUser", s.UpdatedBy);
                 cmd.Parameters.AddWithValue("@Date", DateTime.Now);
                 cmd.Parameters.AddWithValue("@Comments", s.Comments);
                 result1 = cmd.ExecuteNonQuery();
             }
             log.Info(" ID: " + s.Id + " and  status changed to :" + s.Status);
             log.Info("Grant Rework : User Name :" + HttpContext.Current.Session["UserName"] + "Role :" + HttpContext.Current.Session["RoleName"]);
         transaction.Commit();
         return result;
     }

     catch (Exception ex)
     {
         log.Error(" Inside UpdateStatusRejectSeedMoneyEntry of ID: " + s.Id);
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

 public string GetStatusName(string p)
 {
     log.Debug("Inside function GetStatusName of StatusId: " + p);
     try
     {
         cmdString = "select StatusName from status_SeedMoney_M where StatusId=@StatusId";
         con = new SqlConnection(str);
         con.Open();
         cmd = new SqlCommand(cmdString, con);
         cmd.Parameters.Add("@StatusId", SqlDbType.VarChar, 15);
         cmd.Parameters["@StatusId"].Value = p;
         cmd.CommandType = CommandType.Text;
         SqlDataReader sdr = cmd.ExecuteReader();
         string datavalue = null;

         while (sdr.Read())
         {

             if (!Convert.IsDBNull(sdr["StatusName"]))
             {
                 datavalue = (string)sdr["StatusName"];
             }
             else if (Convert.IsDBNull(sdr["StatusName"]))
             {
                 datavalue = "";
             }

         }
       
         return datavalue;
     }
     catch (Exception e)
     {
         log.Error("Inside Catch block of function GetStatusName of StatusId: " + p);
         log.Error(e.Message);
         log.Error(e.StackTrace);
         throw e;
     }
     finally
     {
         con.Close();
     }
 }



 public int UpdateStatusApproveSeedMoneyEntryStudent(SeedMoney s, SeedMoney[] JD)
 {
     log.Debug("Inside UpdateSeedMoneyEntry function of ID: " + s.Id);
     int result = 0, result1 = 0;
     con = new SqlConnection(str);
     con.Open();
     transaction = con.BeginTransaction();
     try
     {
         cmd = new SqlCommand("update SeedMoneyDetails set  Title=@Title,Writeup=@Writeup,Budget=@Budget,AppliedDate=@AppliedDate,Status=@Status,UpdatedBy=@UpdatedBy,UpdatedDate=@UpdatedDate where ID=@ID", con, transaction);
         cmd.CommandType = CommandType.Text;
         cmd.Parameters.AddWithValue("@ID", s.Id);
         cmd.Parameters.AddWithValue("@Title", s.Title);
         cmd.Parameters.AddWithValue("@Writeup", s.Writeup);
         cmd.Parameters.AddWithValue("@Budget", s.Budget);
         if (s.AppliedDate.ToShortDateString() != "01/01/0001")
         {
             cmd.Parameters.AddWithValue("@AppliedDate", s.AppliedDate);
         }
         else
         {
             cmd.Parameters.AddWithValue("@AppliedDate", DBNull.Value);
         }

         cmd.Parameters.AddWithValue("@Status", s.Status);
         cmd.Parameters.AddWithValue("@UpdatedBy", s.UpdatedBy);
         cmd.Parameters.AddWithValue("@UpdatedDate", DateTime.Now);
         
         result = cmd.ExecuteNonQuery();
         log.Debug("seed Data Updated Sucessfully of ID: " + s.Id);

         if (result == 1)
         {
             cmdString = "delete from  SeedMoneyInvestigator  where ID=@ID ";
             cmd = new SqlCommand(cmdString, con, transaction);
             cmd.CommandType = CommandType.Text;
             cmd.Parameters.AddWithValue("@ID", s.Id);
             result1 = cmd.ExecuteNonQuery();


             for (int i = 0; i < JD.Length; i++)
             {
                 cmd = new SqlCommand("InsertSeedMoneyInvestigator", con, transaction);
                 cmd.CommandType = CommandType.StoredProcedure;
                 cmd.Parameters.AddWithValue("@ID", s.Id);
                 cmd.Parameters.AddWithValue("@EntryNo", i + 1);
                 cmd.Parameters.AddWithValue("@InvestigatorName", JD[i].AuthorName);
                 cmd.Parameters.AddWithValue("@MUNonMU", JD[i].MUNonMU);
                 cmd.Parameters.AddWithValue("@EmployeeCode", JD[i].EmployeeCode);

                 cmd.Parameters.AddWithValue("@Institution", JD[i].Institution);
                 cmd.Parameters.AddWithValue("@Department", JD[i].Department);

                 cmd.Parameters.AddWithValue("@InstitutionName", JD[i].InstitutionName);
                 cmd.Parameters.AddWithValue("@DepartmentName", JD[i].DepartmentName);
                 cmd.Parameters.AddWithValue("@InvestigatorType", JD[i].AuthorType);

                 if (JD[i].AuthorType == "P" && JD[i].LeadPI == "Y")
                 {

                     cmd.Parameters.AddWithValue("@isLeadPI", "Y");
                 }
                 else if (JD[i].AuthorType == "P" && JD[i].LeadPI == "N")
                 {

                     cmd.Parameters.AddWithValue("@isLeadPI", "N");
                 }
                 else
                 {
                     cmd.Parameters.AddWithValue("@isLeadPI", DBNull.Value);
                 }
                 cmd.Parameters.AddWithValue("@NationalInternational", JD[i].NationalInternationl);
                 cmd.Parameters.AddWithValue("@Continent", JD[i].continental);
                 cmd.Parameters.AddWithValue("@EmailId", JD[i].EmailId);
                 result1 = cmd.ExecuteNonQuery();

                 log.Info("SeedMoney investigator details Updated sucessfully  of Id: " + s.Id);
             }


         }


         transaction.Commit();
         return result1;
     }

     catch (Exception ex)
     {
         log.Error("Inside UpdateSeedMoneyEntry catch block of Id: " + s.Id);
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

 public string getentrytype(string p)
 {

     log.Debug("Inside function getentrytype of StatusId: " + p);
     try
     {
         //cmdString = "select EntryType from SeedMoneyDetails where ID='"+p+"'";
         cmdString = "select EntryType from SeedMoneyDetails where ID=@p";
        
         con = new SqlConnection(str);
         con.Open();
         cmd = new SqlCommand(cmdString, con);
         cmd.Parameters.AddWithValue("@p", p);
         cmd.CommandType = CommandType.Text;
         SqlDataReader sdr = cmd.ExecuteReader();
         string EntryType = null;

         while (sdr.Read())
         {

             if (!Convert.IsDBNull(sdr["EntryType"]))
             {
                 EntryType = (string)sdr["EntryType"];
             }
             else if (Convert.IsDBNull(sdr["EntryType"]))
             {
                 EntryType = "";
             }

         }

         return EntryType;
     }
     catch (Exception e)
     {
         log.Error("Inside Catch block of function getentrytype of StatusId: " + p);
         log.Error(e.Message);
         log.Error(e.StackTrace);
         throw e;
     }
     finally
     {
         con.Close();
     }
 }

 public int UpdateSeedMoneyEntryNew(SeedMoney s, SeedMoney[] JD)
 {
     log.Debug("Inside UpdateSeedMoneyEntryNew function of ID: " + s.Id);
     int result = 0, result1 = 0;
     con = new SqlConnection(str);
     con.Open();
     transaction = con.BeginTransaction();
     try
     {
         cmd = new SqlCommand("UpdateSeedMoneyEntry", con, transaction);
         cmd.CommandType = CommandType.StoredProcedure;
         cmd.Parameters.AddWithValue("@ID", s.Id);
         cmd.Parameters.AddWithValue("@Title", s.Title);
         cmd.Parameters.AddWithValue("@Writeup", s.Writeup);
         cmd.Parameters.AddWithValue("@Budget", s.Budget);
         if (s.AppliedDate.ToShortDateString() != "01/01/0001")
         {
             cmd.Parameters.AddWithValue("@AppliedDate", s.AppliedDate);
         }
         else
         {
             cmd.Parameters.AddWithValue("@AppliedDate", DBNull.Value);
         }

         cmd.Parameters.AddWithValue("@Status", s.Status);
         //cmd.Parameters.AddWithValue("@CreatedBy", s.CreatedBy);
         //cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
         cmd.Parameters.AddWithValue("@Institution", s.InstUser);
         cmd.Parameters.AddWithValue("@Department", s.DeptUser);
         if (s.Entrytype == "S")
         {

             cmd.Parameters.AddWithValue("@cycleid", DBNull.Value);

         }
         else
         {
             if (s.cycleid != null)
             {
                 cmd.Parameters.AddWithValue("@cycleid", s.cycleid);
             }
             else
             {
                 cmd.Parameters.AddWithValue("@cycleid", DBNull.Value);
             }
         }
         result = cmd.ExecuteNonQuery();
         log.Debug("seed Data Updated Sucessfully of ID: " + s.Id);

         if (result == 1)
         {
             cmdString = "delete from  SeedMoneyInvestigator  where ID=@ID ";
             cmd = new SqlCommand(cmdString, con, transaction);
             cmd.CommandType = CommandType.Text;
             cmd.Parameters.AddWithValue("@ID", s.Id);
             result1 = cmd.ExecuteNonQuery();


             for (int i = 0; i < JD.Length; i++)
             {
                 cmd = new SqlCommand("InsertSeedMoneyInvestigator", con, transaction);
                 cmd.CommandType = CommandType.StoredProcedure;
                 cmd.Parameters.AddWithValue("@ID", s.Id);
                 cmd.Parameters.AddWithValue("@EntryNo", i + 1);
                 cmd.Parameters.AddWithValue("@InvestigatorName", JD[i].AuthorName);
                 cmd.Parameters.AddWithValue("@MUNonMU", JD[i].MUNonMU);
                 cmd.Parameters.AddWithValue("@EmployeeCode", JD[i].EmployeeCode);

                 cmd.Parameters.AddWithValue("@Institution", JD[i].Institution);
                 cmd.Parameters.AddWithValue("@Department", JD[i].Department);

                 cmd.Parameters.AddWithValue("@InstitutionName", JD[i].InstitutionName);
                 cmd.Parameters.AddWithValue("@DepartmentName", JD[i].DepartmentName);
                 cmd.Parameters.AddWithValue("@InvestigatorType", JD[i].AuthorType);

                 if (JD[i].AuthorType == "P" && JD[i].LeadPI == "Y")
                 {

                     cmd.Parameters.AddWithValue("@isLeadPI", "Y");
                 }
                 else if (JD[i].AuthorType == "P" && JD[i].LeadPI == "N")
                 {

                     cmd.Parameters.AddWithValue("@isLeadPI", "N");
                 }
                 else
                 {
                     cmd.Parameters.AddWithValue("@isLeadPI", DBNull.Value);
                 }
                 cmd.Parameters.AddWithValue("@NationalInternational", JD[i].NationalInternationl);
                 cmd.Parameters.AddWithValue("@Continent", JD[i].continental);
                 cmd.Parameters.AddWithValue("@EmailId", JD[i].EmailId);
                 result1 = cmd.ExecuteNonQuery();

                 log.Info("SeedMoney investigator details Updated sucessfully  of Id: " + s.Id);
             }
            

         }


         transaction.Commit();
         return result1;
     }

     catch (Exception ex)
     {
         log.Error("Inside UpdateSeedMoneyEntryNew catch block of Id: " + s.Id);
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

 public bool UpdateStatusRevisionRequiredSeedMoneyEntry(SeedMoney s)
 {
     log.Debug("Inside UpdateStatusRevisionRequiredSeedMoneyEntry of Id: " + s.Id);
     bool result = false;
     int result1 = 0;
     con = new SqlConnection(str);
     con.Open();
     transaction = con.BeginTransaction();
     try
     {
         log.Debug("Inside UpdateStatusRevisionRequiredSeedMoneyEntry to update the status to :" + s.Status + "ID: " + s.Id);
         cmdString = "update SeedMoneyDetails set Status=@Status,Comments=@Comments where ID=@ID ";
         cmd = new SqlCommand(cmdString, con, transaction);
         cmd.CommandType = CommandType.Text;
         cmd.Parameters.AddWithValue("@ID", s.Id);
         cmd.Parameters.AddWithValue("@Status", s.Status);
         cmd.Parameters.AddWithValue("@Comments", s.Comments);
         result = Convert.ToBoolean(cmd.ExecuteNonQuery());
         if (result == true)
         {
             cmdString = "Select count(EntryNo ) as Count from SeedMoneyStatustracker where  ID=@ID";
             cmd = new SqlCommand(cmdString, con, transaction);
             cmd.CommandType = CommandType.Text;
             cmd.Parameters.AddWithValue("@ID", s.Id);
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

             cmd = new SqlCommand("Insert  SeedMoneyStatustracker (ID,EntryNo,Status,UpdatedUser,Date,Comments) values (@ID,@EntryNo,@Status,@UpdatedUser,@Date,@Comments)", con, transaction);
             cmd.CommandType = CommandType.Text;
             cmd.Parameters.AddWithValue("@ID", s.Id);
             cmd.Parameters.AddWithValue("@EntryNo", count + 1);
             cmd.Parameters.AddWithValue("@Status", s.Status);
             cmd.Parameters.AddWithValue("@UpdatedUser", s.UpdatedBy);
             cmd.Parameters.AddWithValue("@Date", DateTime.Now);
             cmd.Parameters.AddWithValue("@Comments", s.Comments);
             result1 = cmd.ExecuteNonQuery();
         }
         log.Info(" ID: " + s.Id + " and  status changed to :" + s.Status);
         log.Info("Grant Revision required : User Name :" + HttpContext.Current.Session["UserName"] + "Role :" + HttpContext.Current.Session["RoleName"]);
         transaction.Commit();
         return result;
     }
     catch (Exception ex)
     {
         log.Error(" Inside UpdateStatusReworkGrantEntry of ID: " + s.Id);
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

 public GrantData CheckUniquePID(string PID, string ProjectUnit)
 {
     try
     {

         GrantData data = new GrantData();
         con = new SqlConnection(str);
         con.Open();
         transaction = con.BeginTransaction();
         //cmdString = " select ProjectUnit+ID as ID from Project where ID=@ID and ProjectUnit='" + ProjectUnit + "' ";
         cmdString = " select ProjectUnit+ID as ID from Project where ID=@ID and ProjectUnit=@ProjectUnit ";
        

         cmd = new SqlCommand(cmdString, con, transaction);
         cmd.CommandType = CommandType.Text;
         cmd.Parameters.AddWithValue("@ID", PID);
         cmd.Parameters.AddWithValue("@ProjectUnit", ProjectUnit);
         cmd.CommandType = CommandType.Text;
         SqlDataReader sdr = cmd.ExecuteReader();
         while (sdr.Read())
         {
             if (!Convert.IsDBNull(sdr["ID"]))
             {
                 data.PID = (string)sdr["ID"];
             }
         }
         sdr.Close();
         transaction.Commit();
         return data;
     }
     catch (Exception ex)
     {

         log.Error("Inside CheckUniquePID catch block ProjectUnit");
         log.Error(ex.Message);
         log.Error(ex.StackTrace);
         throw ex;
     }
     finally
     {
         con.Close();
     }
 }

 public int Insertfileuploadprojets(GrantData jd, GrantData jdi)
 {
     try
     {
         int data = 0;
         int data1 = 0;
         con = new SqlConnection(str);
         con.Open();
         transaction = con.BeginTransaction();
         cmdString = "   Select count(* ) as Count from Project  where ID=@ID and  ProjectUnit=@ProjectUnit";
         cmd = new SqlCommand(cmdString, con, transaction);
         cmd.CommandType = CommandType.Text;
         cmd.Parameters.AddWithValue("@ID", jd.PID);
         cmd.Parameters.AddWithValue("@ProjectUnit", jd.ProjectUnit);
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
             cmd = new SqlCommand("insert into Project (ProjectUnit,ID,ProjectType,UTN,Title,Description,FundingAgency,AppliedAmount,SourceProject,ProjectStatus,Comments,SanctionType,ERFRealated,AppliedDate,Contact_No,AgencyAddress,AgencyContact,AgencyEmailId,AgencyPanNo,State,Country,DurationOfProject,RevisedAppliedAmount,SanctionOrderDate,IsUploadedProject,CreatedDate,UploadedBy,FundingSectorLevel,TypeofAgency)values(@ProjectUnit,@ID,@ProjectType,@UTN,@Title,@Description,@FundingAgency,@AppliedAmount,@SourceProject,@ProjectStatus,@Comments,@SanctionType,@ERFRealated,@AppliedDate,@Contact_No,@AgencyAddress,@AgencyContact,@AgencyEmailId,@AgencyPanNo,@State,@Country,@DurationOfProject,@RevisedAppliedAmount,@SanctionOrderDate,@IsUploadedProject,@CreatedDate,@UploadedBy,@FundingSectorLevel,@TypeofAgency)", con, transaction);
             cmd.CommandType = CommandType.Text;
             cmd.Parameters.AddWithValue("@ProjectUnit", jd.ProjectUnit);
             cmd.Parameters.AddWithValue("@ID", jd.PID);
             cmd.Parameters.AddWithValue("@ProjectType", jd.ProjectType);
             cmd.Parameters.AddWithValue("@UTN", jd.UTN);
             cmd.Parameters.AddWithValue("@Title", jd.Title);
             cmd.Parameters.AddWithValue("@Description", jd.Description);
             if (jd.FundingAgency == "&nbsp;" || jd.FundingAgency == "" || jd.FundingAgency == null)
             {
                 cmd.Parameters.AddWithValue("@FundingAgency", DBNull.Value);
             }
             else
             {
                 cmd.Parameters.AddWithValue("@FundingAgency", jd.FundingAgency);
             }
             cmd.Parameters.AddWithValue("@AppliedAmount ", jd.AppliedAmount);
             if (jd.SourceProject == "&nbsp;" || jd.SourceProject == "" || jd.SourceProject == null)
             {
                 cmd.Parameters.AddWithValue("@SourceProject", DBNull.Value);
             }
             else
             {
                 cmd.Parameters.AddWithValue("@SourceProject", jd.SourceProject);
             }
             cmd.Parameters.AddWithValue("@ProjectStatus", "NEW");
            
             if (jd.Comments == "&nbsp;" || jd.Comments == "" || jd.Comments == null)
             {
                 cmd.Parameters.AddWithValue("@Comments", DBNull.Value);
             }
             else
             {
                 cmd.Parameters.AddWithValue("@Comments", jd.Comments);
             }
             if (jd.SanctionType == "&nbsp;" || jd.SanctionType == "" || jd.SanctionType == null)
             {
                 cmd.Parameters.AddWithValue("@SanctionType", DBNull.Value);
             }
             else
             {
                 cmd.Parameters.AddWithValue("@SanctionType", jd.SanctionType);
             }
             if (jd.ERFRealated == "&nbsp;" || jd.ERFRealated == "" || jd.ERFRealated == null)
             {
                 cmd.Parameters.AddWithValue("@ERFRealated", DBNull.Value);
             }
             else
             {
                 cmd.Parameters.AddWithValue("@ERFRealated", jd.ERFRealated);
             }

           
             if (jd.AppliedDate.ToShortDateString() != "01/01/0001")
             {
                 cmd.Parameters.AddWithValue("@AppliedDate", jd.AppliedDate);
             }
             else
             {
                 cmd.Parameters.AddWithValue("@AppliedDate", DBNull.Value);
             }
             if (jd.Contact_No == "&nbsp;" || jd.Contact_No == "" || jd.Contact_No == null)
             {
                 cmd.Parameters.AddWithValue("@Contact_No", DBNull.Value);
             }
             else
             {
                 cmd.Parameters.AddWithValue("@Contact_No ", jd.Contact_No);
             }
             if (jd.AgencyAddress == "&nbsp;" || jd.AgencyAddress == "" || jd.AgencyAddress == null)
             {
                 cmd.Parameters.AddWithValue("@AgencyAddress", DBNull.Value);
             }
             else
             {
                 cmd.Parameters.AddWithValue("@AgencyAddress ", jd.AgencyAddress);
             }


             if (jd.AgencyContact == "&nbsp;" || jd.AgencyContact == "" || jd.AgencyContact == null)
             {
                 cmd.Parameters.AddWithValue("@AgencyContact", DBNull.Value);
             }
             else
             {
                 cmd.Parameters.AddWithValue("@AgencyContact ", jd.AgencyContact);
             }


             if (jd.AgencyEmailId == "&nbsp;" || jd.AgencyEmailId == "" || jd.AgencyEmailId == null)
             {
                 cmd.Parameters.AddWithValue("@AgencyEmailId", DBNull.Value);
             }
             else
             {
                 cmd.Parameters.AddWithValue("@AgencyEmailId ", jd.AgencyEmailId);
             }


             if (jd.AgencyPanNo == "&nbsp;" || jd.AgencyPanNo == "" || jd.AgencyPanNo == null)
             {
                 cmd.Parameters.AddWithValue("@AgencyPanNo", DBNull.Value);
             }
             else
             {
                 cmd.Parameters.AddWithValue("@AgencyPanNo ", jd.AgencyPanNo);
             }


             if (jd.State == "&nbsp;" || jd.State == "" || jd.State == null)
             {
                 cmd.Parameters.AddWithValue("@State", DBNull.Value);
             }
             else
             {
                 cmd.Parameters.AddWithValue("@State ", jd.State);
             }


             if (jd.Country == "&nbsp;" || jd.Country == "" || jd.Country == null)
             {
                 cmd.Parameters.AddWithValue("@Country", DBNull.Value);
             }
             else
             {
                 cmd.Parameters.AddWithValue("@Country ", jd.Country);
             }

             if (jd.DurationOfProject != 0)
             {
                 cmd.Parameters.AddWithValue("@DurationOfProject", jd.DurationOfProject);
             }
             else
             {
                 cmd.Parameters.AddWithValue("@DurationOfProject  ", DBNull.Value);
             }

             cmd.Parameters.AddWithValue("@RevisedAppliedAmount  ", jd.RevisedAppliedAmount);
             cmd.Parameters.AddWithValue("@SanctionOrderDate  ", jd.SanctionOrderDate);
             cmd.Parameters.AddWithValue("@IsUploadedProject", 'Y');
             cmd.Parameters.AddWithValue("@CreatedDate  ", DateTime.Now);
             cmd.Parameters.AddWithValue("UploadedBy", HttpContext.Current.Session["UserId"].ToString());
             cmd.Parameters.AddWithValue("@FundingSectorLevel",jd.FundingSectorLevel);
             cmd.Parameters.AddWithValue("@TypeofAgency  ", jd.TypeofAgency);
        


             data = cmd.ExecuteNonQuery();
         }

         cmdString = "   Select count(* ) as Count from Projectnvestigator  where EntryNo!=''and  ID=@ID and  ProjectUnit=@ProjectUnit";
         cmd = new SqlCommand(cmdString, con, transaction);
         cmd.CommandType = CommandType.Text;
         cmd.Parameters.AddWithValue("@ID", jdi.PID);
         cmd.Parameters.AddWithValue("@ProjectUnit", jdi.ProjectUnit);
         SqlDataReader sdr4 = cmd.ExecuteReader();
         int count4 = 0;
         while (sdr4.Read())
         {
             if (!Convert.IsDBNull(sdr4["Count"]))
             {
                 count4 = (int)sdr4["Count"];
             }

         }

         sdr4.Close();

         cmd = new SqlCommand("Insert into Projectnvestigator (ProjectUnit,ID,EntryNo,InvestigatorName,MUNonMU,EmployeeCode,Institution,Department,EmailId,InvestigatorType,InstitutionName,DepartmentName,NationalInternational,Continent,isLeadPI )values(@ProjectUnit,@ID,@EntryNo,@InvestigatorName,@MUNonMU,@EmployeeCode,@Institution,@Department,@EmailId,@InvestigatorType,@InstitutionName,@DepartmentName,@NationalInternational,@Continent,@isLeadPI )", con, transaction);
         cmd.CommandType = CommandType.Text;
         cmd.Parameters.AddWithValue("@ProjectUnit", jdi.ProjectUnit);
         cmd.Parameters.AddWithValue("@ID", jdi.PID);
         cmd.Parameters.AddWithValue("@EntryNo", count4 + 1);
         cmd.Parameters.AddWithValue("@InvestigatorName", jdi.InvestigatorName);
         cmd.Parameters.AddWithValue("@MUNonMU", jdi.MUNonMU);
         cmd.Parameters.AddWithValue("@EmployeeCode", jdi.EmployeeCode);
         cmd.Parameters.AddWithValue("@Institution", jdi.Institution);
         cmd.Parameters.AddWithValue("@Department", jdi.Department);
         cmd.Parameters.AddWithValue("@EmailId", jdi.EmailId);
         cmd.Parameters.AddWithValue("@InvestigatorType", jdi.InvestigatorType);
         cmd.Parameters.AddWithValue("@InstitutionName", jdi.InstitutionName);
         cmd.Parameters.AddWithValue("@DepartmentName", jdi.DepartmentName);
         if (jd.NationalInternational == "&nbsp;" || jd.NationalInternational == "" || jd.NationalInternational == null)
         {
             cmd.Parameters.AddWithValue("@NationalInternational", DBNull.Value);
         }
         else
         {
             cmd.Parameters.AddWithValue("@NationalInternational", jdi.NationalInternational);
         }
         if (jdi.Continent == "&nbsp;" || jdi.Continent == "" || jdi.Continent == null)
         {
             cmd.Parameters.AddWithValue("@Continent", DBNull.Value);
         }
         else
         {
             cmd.Parameters.AddWithValue("@Continent", jdi.Continent);
         }
         if (jdi.isLeadPI == "&nbsp;" || jdi.isLeadPI == "" || jdi.isLeadPI == null)
         {
             cmd.Parameters.AddWithValue("@isLeadPI", DBNull.Value);
         }
         else
         {
             cmd.Parameters.AddWithValue("@isLeadPI", jdi.isLeadPI);
         }


         data1 = cmd.ExecuteNonQuery();

         cmdString = "   Select count(* ) as Count from Project where  CreatedBy !=''and InstitutionID!='' and DeptID!='' and ID=@ID and  ProjectUnit=@ProjectUnit";
         cmd = new SqlCommand(cmdString, con, transaction);
         cmd.CommandType = CommandType.Text;
         cmd.Parameters.AddWithValue("@ID", jd.PID);
         cmd.Parameters.AddWithValue("@ProjectUnit", jd.ProjectUnit);
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

         if (count1 == 0)
         {
             if (jd.CreatedBy != ""&&jd.CreatedBy !=null)
             {
                 cmd = new SqlCommand("Update  Project set CreatedBy=@CreatedBy,InstitutionID=@InstitutionID,DeptID=@DeptID where ID=@ID and ProjectUnit=@ProjectUnit  ", con, transaction);
                 cmd.CommandType = CommandType.Text;
                 cmd.Parameters.AddWithValue("@ProjectUnit", jdi.ProjectUnit);
                 cmd.Parameters.AddWithValue("@ID", jdi.PID);
                 cmd.Parameters.AddWithValue("@CreatedBy", jd.CreatedBy);
                 cmd.Parameters.AddWithValue("@InstitutionID", jd.InstitutionID);
                 cmd.Parameters.AddWithValue("@DeptID", jd.DeptID);                     
                 int data2 = cmd.ExecuteNonQuery();
             }
         }

         cmdString = "   Select count(* ) as Count from Project where PIInstitutionID!='' and PIDeptID!='' and ID=@ID and  ProjectUnit=@ProjectUnit";
         cmd = new SqlCommand(cmdString, con, transaction);
         cmd.CommandType = CommandType.Text;
         cmd.Parameters.AddWithValue("@ID", jd.PID);
         cmd.Parameters.AddWithValue("@ProjectUnit", jd.ProjectUnit);
         SqlDataReader sdr5 = cmd.ExecuteReader();
         int count5 = 0;

         while (sdr5.Read())
         {
             if (!Convert.IsDBNull(sdr5["Count"]))
             {
                 count5 = (int)sdr5["Count"];
             }

         }

         sdr5.Close();

         if (count5 == 0)
         {
             if (jd.PIInstitutionID != "" && jd.PIInstitutionID != null && jd.PIDeptID != "" && jd.PIDeptID !=null)
             {
                 cmd = new SqlCommand("Update  Project set PIInstitutionID=@PIInstitutionID,PIDeptID=@PIDeptID where ID=@ID and ProjectUnit=@ProjectUnit  ", con, transaction);
                 cmd.CommandType = CommandType.Text;
                 cmd.Parameters.AddWithValue("@ProjectUnit", jdi.ProjectUnit);
                 cmd.Parameters.AddWithValue("@ID", jdi.PID);
                 cmd.Parameters.AddWithValue("@PIInstitutionID", jd.PIInstitutionID);
                 cmd.Parameters.AddWithValue("@PIDeptID", jd.PIDeptID);

                 int data2 = cmd.ExecuteNonQuery();
             }
         }

         cmdString = "   Select count(* ) as Count from ProjectStatusTracker where  ID=@ID and  ProjectUnit=@ProjectUnit";
         cmd = new SqlCommand(cmdString, con, transaction);
         cmd.CommandType = CommandType.Text;
         cmd.Parameters.AddWithValue("@ID", jd.PID);
         cmd.Parameters.AddWithValue("@ProjectUnit", jd.ProjectUnit);
         SqlDataReader sdr2 = cmd.ExecuteReader();
         int count2 = 0;
         while (sdr2.Read())
         {
             if (!Convert.IsDBNull(sdr2["Count"]))
             {
                 count2 = (int)sdr2["Count"];
             }

         }

         sdr2.Close();
         if (count2 == 0)
         {
             cmd = new SqlCommand("insert into ProjectStatusTracker (ProjectUnit, ID ,ReviewNo, Status,Remark,UpdatedUser,Date) values(@GrantUnit,@ID ,@ReviewNo, @ApprovedStatus,@Remark,@UpdateUser,@Date)", con, transaction);
             cmd.CommandType = CommandType.Text;
             cmd.Parameters.AddWithValue("@ID", jd.PID);
             cmd.Parameters.AddWithValue("@GrantUnit", jd.ProjectUnit);
             cmd.Parameters.AddWithValue("@ReviewNo", 1);
             if (jd.ProjectStatus == "&nbsp;" || jd.ProjectStatus == "" || jd.ProjectStatus == null)
             {
                 cmd.Parameters.AddWithValue("@ApprovedStatus", DBNull.Value);
             }
             else
             {
                 cmd.Parameters.AddWithValue("@ApprovedStatus", "NEW");
             }
             if (jd.Remarks == "&nbsp;" || jd.Remarks == "" || jd.Remarks == null)
             {
                 cmd.Parameters.AddWithValue("@Remark", DBNull.Value);
             }
             else
             {
                 cmd.Parameters.AddWithValue("@Remark", jd.AddtionalComments);
             }
             if (jd.CreatedBy == "&nbsp;" || jd.CreatedBy == "" || jd.CreatedBy == null)
             {
                 cmd.Parameters.AddWithValue("@UpdateUser", DBNull.Value);
             }
             else
             {
                 cmd.Parameters.AddWithValue("@UpdateUser", jd.CreatedBy);
             }


             cmd.Parameters.AddWithValue("@Date", DateTime.Now);
             int result = cmd.ExecuteNonQuery();
         }

         cmdString = "   Select count(* ) as Count from ProjectStatusTracker where  UpdatedUser !='' and ID=@ID and  ProjectUnit=@ProjectUnit";
         cmd = new SqlCommand(cmdString, con, transaction);
         cmd.CommandType = CommandType.Text;
         cmd.Parameters.AddWithValue("@ID", jd.PID);
         cmd.Parameters.AddWithValue("@ProjectUnit", jd.ProjectUnit);
         SqlDataReader sdr3 = cmd.ExecuteReader();
         int count3 = 0;

         while (sdr3.Read())
         {
             if (!Convert.IsDBNull(sdr3["Count"]))
             {
                 count3 = (int)sdr3["Count"];
             }

         }

         sdr3.Close();
         if (count3 == 0)
         {
             if (jdi.EmployeeCode != "")
             {
                 cmd = new SqlCommand("Update  ProjectStatusTracker set UpdatedUser=@UpdatedUser where ID=@ID and ProjectUnit=@ProjectUnit  ", con, transaction);
                 cmd.CommandType = CommandType.Text;
                 cmd.Parameters.AddWithValue("@ProjectUnit", jdi.ProjectUnit);
                 cmd.Parameters.AddWithValue("@ID", jdi.PID);
                 if (jdi.MUNonMU == "M" && jdi.InvestigatorType == "P")
                 {
                     cmd.Parameters.AddWithValue("@UpdatedUser  ", jdi.EmployeeCode);

                 }
                 else

                     if (jdi.MUNonMU == "M" && jdi.InvestigatorType == "C")
                     {
                         cmd.Parameters.AddWithValue("@UpdatedUser  ", jdi.EmployeeCode);
                     }
                 int data3 = cmd.ExecuteNonQuery();
             }
         }


         transaction.Commit();
         return data1;




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

 public DataSet getReserachDirectorclerk(string p1, string p2)
 {
     log.Debug("Inside function getReserachDirectorclerk of Project Unit: " + p2 + "ID: " + p1);
     try
     {
         SqlDataAdapter da;
         DataSet ds = new DataSet();

         con = new SqlConnection(str);
         con.Open();
         cmdString = "select EmailId from User_M u,User_Role_Map m where u.User_Id=m.User_Id and m.Role_Id=2 and u.Active='Y' ";
         da = new SqlDataAdapter(cmdString, con);
         da.SelectCommand.CommandType = CommandType.Text;
         da.Fill(ds);

         return ds;
     }
     catch (Exception e)
     {
         log.Error("Inside Catch block of function getReserachDirectorclerk of Project Unit: " + p2 + "ID: " + p1);
         log.Error(e.Message);
         log.Error(e.StackTrace);
         throw e;
     }
     finally
     {
         con.Close();
     }
 }

 public int UpdateStatusGrantEntry(GrantData j, GrantData[] JD, GrantData[] JD1, GrantData[] SD3)
 {
     log.Debug("Inside UpdateStatusGrantEntry function of Project Unit: " + j.GrantUnit + "ID: " + j.GID);
     int result = 0, result1 = 0;
     con = new SqlConnection(str);
     con.Open();
     transaction = con.BeginTransaction();
     try
     {
         cmd = new SqlCommand("update Project set Description=@Description,AppliedAmount=@GranAmount,ERFRealated=@ERFRealated,Comments=@comments,Contact_No=@Contact_No,DurationOfProject=@DurationOfProject,ProjectStatus=@Status where ID=@ID and ProjectUnit=@GrantUnit", con, transaction);
         cmd.CommandType = CommandType.Text;
         cmd.Parameters.AddWithValue("@ID", j.GID);
         cmd.Parameters.AddWithValue("@Description", j.Description);
         cmd.Parameters.AddWithValue("@GrantUnit", j.GrantUnit);
         if (j.GranAmount != 0.0)
         {

             cmd.Parameters.AddWithValue("@GranAmount", j.GranAmount);
         }
         else
         {
             cmd.Parameters.AddWithValue("@GranAmount", DBNull.Value);
         }
         if (j.AppliedDate.ToShortDateString() != "01/01/0001")
         {
             cmd.Parameters.AddWithValue("@AppliedDate", j.AppliedDate);
         }
         else
         {
             cmd.Parameters.AddWithValue("@AppliedDate", DBNull.Value);
         }
         cmd.Parameters.AddWithValue("@comments", j.AddtionalComments);
         if (j.Contact_No != "")
         {
             cmd.Parameters.AddWithValue("@Contact_No", j.Contact_No);
         }
         else
         {
             cmd.Parameters.AddWithValue("@Contact_No", DBNull.Value);
         }
         cmd.Parameters.AddWithValue("@ERFRealated", j.ERFRelated);


         if (j.DurationOfProject != 0)
         {
             cmd.Parameters.AddWithValue("@DurationOfProject", j.DurationOfProject);
         }
         else
         {
             cmd.Parameters.AddWithValue("@DurationOfProject", DBNull.Value);
         }
         cmd.Parameters.AddWithValue("@Status", "SUB");
         result = cmd.ExecuteNonQuery();

         cmdString = "Select count(* ) as Count from ProjectStatusTracker where  ID=@ID and ProjectUnit=@GrantUnit";
         cmd = new SqlCommand(cmdString, con, transaction);
         cmd.CommandType = CommandType.Text;
         cmd.Parameters.AddWithValue("@ID", j.GID);
         cmd.Parameters.AddWithValue("@GrantUnit", j.GrantUnit);
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
         cmd = new SqlCommand("insert into ProjectStatusTracker (ProjectUnit, ID ,ReviewNo, Status,Remark,UpdatedUser,Date) values(@GrantUnit,@ID ,@ReviewNo, @ApprovedStatus,@Remark,@UpdateUser,@Date)", con, transaction);
         cmd.CommandType = CommandType.Text;
         cmd.Parameters.AddWithValue("@ID", j.GID);
         cmd.Parameters.AddWithValue("@GrantUnit", j.GrantUnit);
         cmd.Parameters.AddWithValue("@ReviewNo", count + 1);
         cmd.Parameters.AddWithValue("@Remark", j.AddtionalComments);
         cmd.Parameters.AddWithValue("@ApprovedStatus", "SUB");
         cmd.Parameters.AddWithValue("@UpdateUser", j.CreatedBy);
         cmd.Parameters.AddWithValue("@Date", DateTime.Now);
         int result2 = cmd.ExecuteNonQuery();
         log.Debug("Grant Data Updated Sucessfully of Project Unit: " + j.GrantUnit + "ID: " + j.GID);
         transaction.Commit();
         return result;
     }

     catch (Exception ex)
     {
         log.Error("Inside UpdateGrantEntry catch block of Project Unit: " + j.GrantUnit + "ID: " + j.GID);
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

 public bool UpdateStatusCancelSeedMoneyEntry(SeedMoney s)
 {
     log.Debug("Inside UpdateStatusCancelSeedMoneyEntry of Id: " + s.Id);
     bool result = false;
     int result1 = 0;
     con = new SqlConnection(str);
     con.Open();
     transaction = con.BeginTransaction();
     try
     {
         log.Debug("Inside UpdateStatusCancelSeedMoneyEntry to update the status to :" + s.Status + "ID: " + s.Id);
         cmdString = "update SeedMoneyDetails set Status=@Status,Comments=@Comments where ID=@ID ";
         cmd = new SqlCommand(cmdString, con, transaction);
         cmd.CommandType = CommandType.Text;
         cmd.Parameters.AddWithValue("@ID", s.Id);
         cmd.Parameters.AddWithValue("@Status", s.Status);
         cmd.Parameters.AddWithValue("@Comments", s.Comments);
         result = Convert.ToBoolean(cmd.ExecuteNonQuery());
         if (result == true)
         {
             cmdString = "Select count(EntryNo ) as Count from SeedMoneyStatustracker where  ID=@ID";
             cmd = new SqlCommand(cmdString, con, transaction);
             cmd.CommandType = CommandType.Text;
             cmd.Parameters.AddWithValue("@ID", s.Id);
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

             cmd = new SqlCommand("Insert  SeedMoneyStatustracker (ID,EntryNo,Status,UpdatedUser,Date,Comments) values (@ID,@EntryNo,@Status,@UpdatedUser,@Date,@Comments)", con, transaction);
             cmd.CommandType = CommandType.Text;
             cmd.Parameters.AddWithValue("@ID", s.Id);
             cmd.Parameters.AddWithValue("@EntryNo", count + 1);
             cmd.Parameters.AddWithValue("@Status", s.Status);
             cmd.Parameters.AddWithValue("@UpdatedUser", s.UpdatedBy);
             cmd.Parameters.AddWithValue("@Date", DateTime.Now);
             cmd.Parameters.AddWithValue("@Comments", s.Comments);
             result1 = cmd.ExecuteNonQuery();
         }
         log.Info(" ID: " + s.Id + " and  status changed to :" + s.Status);
         log.Info("Grant Rework : User Name :" + HttpContext.Current.Session["UserName"] + "Role :" + HttpContext.Current.Session["RoleName"]);
         transaction.Commit();
         return result;
     }

     catch (Exception ex)
     {
         log.Error(" Inside UpdateStatusCancelSeedMoneyEntry of ID: " + s.Id);
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

 public int Updatemailid(string p, User b, int isupdatemailid, string OldmailID)
 {
     log.Debug("Inside Updatemailid of User_ID: " + b.User_Id);
     int result = 0;
     con = new SqlConnection(str);
     con.Open();
     transaction = con.BeginTransaction();
     try
     {
         log.Debug("Inside Updatemailid to update the EmailID to :" + p + " of  User_ID: " + b.User_Id);
         //cmdString = "update User_M set EmailId='" + p + "' where User_Id='" + b.User_Id + "'";
         cmdString = "update User_M set EmailId=@p where User_Id=@b.User_Id";
        
         cmd = new SqlCommand(cmdString, con, transaction);
         cmd.Parameters.AddWithValue("@p", p);
         cmd.Parameters.AddWithValue("@User_Id", b.User_Id);

         cmd.CommandType = CommandType.Text;
         result = cmd.ExecuteNonQuery();
         if (isupdatemailid == 1)
         {

             cmdString = "insert into UserEmailIdUpdateTracker (EmployeeCode,OldEmailId,UpdatedEmailId,UpdatedBy,Updateddate)values(@User_ID,@OldEmailId,@UpdatedEmailId,@UpdatedBy,@Updateddate)";
             cmd = new SqlCommand(cmdString, con, transaction);
             cmd.CommandType = CommandType.Text;
             cmd.Parameters.AddWithValue("@User_ID", b.User_Id);
             cmd.Parameters.AddWithValue("@OldEmailId", OldmailID);
             cmd.Parameters.AddWithValue("@UpdatedEmailId", p);
             cmd.Parameters.AddWithValue("@UpdatedBy", b.CreatedBy);
             cmd.Parameters.AddWithValue("@Updateddate", DateTime.Now);
             int result1 = cmd.ExecuteNonQuery();
         }

         transaction.Commit();
         return result;
     }

     catch (Exception ex)
     {
         log.Error(" Inside Updatemailid of User_ID: " + b.User_Id);
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

 public int UpdateGrantEntryProjectdetails(GrantData j, GrantData[] JD)
 {
     log.Debug("Inside UpdateGrantEntryProjectdetails function of Project Unit: " + j.GrantUnit + "ID: " + j.GID);
     int result = 0, result1 = 0, seed = 0, seed1 = 0, year1 = 0;
     string seedFinal = "";
     string seedUTN = "";
     string seedFinalUTN = "";
     con = new SqlConnection(str);
     con.Open();
     transaction = con.BeginTransaction();
     try
     {

         if (j.CountType == 1)
         {

             cmdString = "select seed from Id_Gen_Project where Project_UnitId=@ProjectUnit";
             cmd = new SqlCommand(cmdString, con, transaction);
             cmd.CommandType = CommandType.Text;

             cmd.Parameters.AddWithValue("@ProjectUnit", j.GrantUnit);
             seed = (int)cmd.ExecuteScalar();


             //string seedStr = seed.ToString();
             //int seedlen = seedStr.Length;
             //int idlen = Convert.ToInt32(ConfigurationManager.AppSettings["GrantIdLen"]);
             //string pre = "";

             //for (int i = 0; i < idlen - seedlen; i++)
             //{
             //    string z = "0";
             //    pre = pre + z;
             //}
             //seedFinal = pre + seed.ToString();

             DateTime date = Convert.ToDateTime(j.AppliedDate);
             int yearvalue = date.Year;
             int resultvalue = 0;

             //HttpContext.Current.Session["Grantseed"] = seedFinal;

             string inst = HttpContext.Current.Session["InstituteId"].ToString();
             string utnid = HttpContext.Current.Session["Department"].ToString();

             //cmdString = "select Seed,Year from Id_Gen_UTN where UTN_ID in(select UTN_ID from Dept_M where Institute_Id=@Institute and DeptId=@DeptId) and ProjectType='" + j.GrantType + "' and Year=" + yearvalue + "";
             cmdString = "select Seed,Year from Id_Gen_UTN where UTN_ID in(select UTN_ID from Dept_M where Institute_Id=@Institute and DeptId=@DeptId) and ProjectType=@GrantType and Year=@yearvalue";
           
             cmd = new SqlCommand(cmdString, con, transaction);
             cmd.Parameters.AddWithValue("@GrantType", j.GrantType);
             cmd.Parameters.AddWithValue("@yearvalue", yearvalue);

             cmd.CommandType = CommandType.Text;

             if (j.MUNonMUUTN == "NUTN")
             {
                 cmd.Parameters.AddWithValue("@Institute", inst);
                 cmd.Parameters.AddWithValue("@DeptId", utnid);

             }
             else
             {

                 cmd.Parameters.AddWithValue("@Institute", j.PiInstId);
                 cmd.Parameters.AddWithValue("@DeptId", j.PiDeptId);

             }
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
             if (j.MUNonMUUTN == "NUTN")
             {
                 cmd.Parameters.AddWithValue("@Institute", inst);
                 cmd.Parameters.AddWithValue("@DeptId", utnid);

             }
             else
             {

                 cmd.Parameters.AddWithValue("@Institute", j.PiInstId);
                 cmd.Parameters.AddWithValue("@DeptId", j.PiDeptId);

             }
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
             seedFinalUTN = j.GrantType + seedStr2 + year_Utn + seedUTN;
             HttpContext.Current.Session["GrantseedUTNseed"] = seedFinalUTN;

             if (resultvalue == 1)
             {
                 cmdString = "select UTN_ID from Dept_M where Institute_Id=@Institute and DeptId=@DeptId";
                 cmd = new SqlCommand(cmdString, con, transaction);
                 cmd.CommandType = CommandType.Text;
                 if (j.MUNonMUUTN == "NUTN")
                 {
                     cmd.Parameters.AddWithValue("@Institute", inst);
                     cmd.Parameters.AddWithValue("@DeptId", utnid);

                 }
                 else
                 {

                     cmd.Parameters.AddWithValue("@Institute", j.PiInstId);
                     cmd.Parameters.AddWithValue("@DeptId", j.PiDeptId);

                 }
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

                 cmdString = "Insert into  Id_Gen_UTN  (Seed,Year,UTN_ID,ProjectType) VALUES(@value,@Year,@UTN,@GrantType)";
                 cmd = new SqlCommand(cmdString, con, transaction);
                 cmd.CommandType = CommandType.Text;

                 cmd.Parameters.AddWithValue("@GrantType", j.GrantType);
                 cmd.Parameters.AddWithValue("@value", seed1 + 1);
                 cmd.Parameters.AddWithValue("@Year", year1);
                 cmd.Parameters.AddWithValue("@UTN", utn);
                 cmd.ExecuteNonQuery();
                 int value = seed1 + 1;
                 log.Info("Inserted new value to the ID_Gen table for the year  : " + year1 + "and ID is :" + value);
             }
             else
             {
                 cmdString = "update Id_Gen_UTN set Seed=@value where UTN_ID in(select UTN_ID from Dept_M where Institute_Id=@Institute and DeptId=@DeptId) and ProjectType=@GrantType";
                 cmd = new SqlCommand(cmdString, con, transaction);
                 cmd.CommandType = CommandType.Text;
                 if (j.MUNonMUUTN == "NUTN")
                 {
                     cmd.Parameters.AddWithValue("@Institute", inst);
                     cmd.Parameters.AddWithValue("@DeptId", utnid);

                 }
                 else
                 {

                     cmd.Parameters.AddWithValue("@Institute", j.PiInstId);
                     cmd.Parameters.AddWithValue("@DeptId", j.PiDeptId);

                 }
                 cmd.Parameters.AddWithValue("@GrantType", j.GrantType);
                 cmd.Parameters.AddWithValue("@value", seed1 + 1);
                 cmd.ExecuteNonQuery();
                 int value = seed1 + 1;
                 log.Info("Updated ID_Gen Value with : " + value);

             }
         }
         else
         {
             seedFinalUTN = j.UTN;
         }
         cmd = new SqlCommand("insert into ProjectTypeUTNUpdateTracker(ID,ProjectUnit,ProjectType,UTN,UpdatedProjectType,UpdatedUTN,UpdatedBy,UpdatedDate,Remarks) values(@ID,@ProjectUnit,@ProjectType,@UTN,@UpdatedProjectType,@UpdatedUTN,@UpdatedBy,@UpdatedDate,@Remarks)", con, transaction);
         cmd.CommandType = CommandType.Text;
         cmd.Parameters.AddWithValue("@ID", j.GID);
          cmd.Parameters.AddWithValue("@ProjectUnit", j.GrantUnit);
          cmd.Parameters.AddWithValue("@ProjectType", j.OldGrantType);
          cmd.Parameters.AddWithValue("@UTN",j.UTN);
         cmd.Parameters.AddWithValue("@UpdatedProjectType", j.GrantType);
         cmd.Parameters.AddWithValue("@UpdatedUTN", seedFinalUTN);
         cmd.Parameters.AddWithValue("@UpdatedBy", j.CreatedBy);
          cmd.Parameters.AddWithValue("@UpdatedDate",DateTime.Now);
          cmd.Parameters.AddWithValue("@Remarks", j.Remarks);
          result1 = cmd.ExecuteNonQuery();


          cmd = new SqlCommand("update project set ProjectType=@GrantType, UTN=@UTN,SourceProject=@SourceProject,AppliedAmount=@AppliedAmount,RevisedAppliedAmount=@AppliedAmount,Title=@Title where ID=@ID and ProjectUnit=@GrantUnit and ProjectStatus=@Status", con, transaction);
             cmd.CommandType = CommandType.Text;

             cmd.Parameters.AddWithValue("@UTN", seedFinalUTN);
             cmd.Parameters.AddWithValue("@ID", j.GID);
             cmd.Parameters.AddWithValue("@GrantUnit", j.GrantUnit);
             cmd.Parameters.AddWithValue("@GrantType", j.GrantType);
             cmd.Parameters.AddWithValue("@Status", j.Status);
             cmd.Parameters.AddWithValue("@SourceProject", j.GrantSource);
             cmd.Parameters.AddWithValue("@AppliedAmount", j.RevisedAppliedAmt);
             cmd.Parameters.AddWithValue("@RevisedAppliedAmount", j.RevisedAppliedAmt);
             cmd.Parameters.AddWithValue("@Title", j.Title);
             result = cmd.ExecuteNonQuery();
     
         transaction.Commit();
         return result;
     }

     catch (Exception ex)
     {
         log.Error("Inside UpdateGrantEntryProjectdetails catch block of Project Unit: " + j.GrantUnit + "ID: " + j.GID);
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

 public int UpdateSeedMoneyEntryApproved(SeedMoney s, SeedMoney[] JD)
 {
     log.Debug("Inside UpdateSeedMoneyEntryApproved of Id: " + s.Id);
     bool result = false;
     int result1 = 0;
     con = new SqlConnection(str);
     con.Open();
     transaction = con.BeginTransaction();
     try
     {
         log.Debug("Inside UpdateSeedMoneyEntryApproved to update the status to :" + s.Status + "ID: " + s.Id);
         cmdString = "update SeedMoneyDetails set Title=@Title,Budget=@Budget,ApprovedBudget=@ApprovedBudget,Comments=@Comments,Remarks=@Remarks  where ID=@ID ";
         cmd = new SqlCommand(cmdString, con, transaction);
         cmd.CommandType = CommandType.Text;
         cmd.Parameters.AddWithValue("@ID", s.Id);
         cmd.Parameters.AddWithValue("@Title", s.Title);
         cmd.Parameters.AddWithValue("@Budget", s.Budget);
         cmd.Parameters.AddWithValue("@ApprovedBudget", s.ApprovedBudget);
         cmd.Parameters.AddWithValue("@Remarks", s.Remarks);
         cmd.Parameters.AddWithValue("@Comments", s.Comments);
         result = Convert.ToBoolean(cmd.ExecuteNonQuery());
         if (result == true)
         {
             cmd = new SqlCommand("Insert  SeedMoneyUpdateTracker (ID,UpdatedBy,UpdatedDate,Remarks) values (@ID,@UpdatedBy,@UpdatedDate,@Remarks)", con, transaction);
             cmd.CommandType = CommandType.Text;
             cmd.Parameters.AddWithValue("@ID", s.Id);
             cmd.Parameters.AddWithValue("@UpdatedBy", s.CreatedBy);
             cmd.Parameters.AddWithValue("@UpdatedDate", DateTime.Now);
             cmd.Parameters.AddWithValue("@Remarks", s.Comments);
             result1 = cmd.ExecuteNonQuery();
         }
      
         log.Info("Grant Update : User Name :" + HttpContext.Current.Session["UserName"] + "Role :" + HttpContext.Current.Session["RoleName"]);
         transaction.Commit();
         return Convert.ToUInt16(result);
     }

     catch (Exception ex)
     {
         log.Error(" Inside UpdateSeedMoneyEntryApproved of ID: " + s.Id);
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

 public DataTable fnfinddistinctOrganizationforPercentage(string Pid, string projectunit)
 {
     log.Debug("Inside fnfinddistinctOrganizationforPercentage function, ProjectUnit: " + projectunit + "ID: " + Pid);

     con = new SqlConnection(str);
     con.Open();
     try
     {
         SqlDataAdapter da;
         DataTable ds;
         cmdString = "SelectInterOrganizationforPercentageSharing";
         cmd = new SqlCommand(cmdString, con);
         cmd.CommandType = CommandType.StoredProcedure;

         cmd.Parameters.Add("@ID", SqlDbType.VarChar, 15);
         cmd.Parameters["@ID"].Value = Pid;

         cmd.Parameters.Add("@GrantUnit", SqlDbType.VarChar, 5);
         cmd.Parameters["@GrantUnit"].Value = projectunit;
         da = new SqlDataAdapter(cmd);

         ds = new DataTable();
         da.Fill(ds);

         return ds;
     }

     catch (Exception ex)
     {
         log.Error("Inside fnfinddistinctOrganizationforPercentage catch block ProjectUnit: " + projectunit + "ID: " + Pid);
         log.Error(ex.Message);
         log.Error(ex.StackTrace);
         throw ex;
     }

     finally
     {
         con.Close();
     }
 }

 public string getMaheInstitutionName(string p)
 {
     try
     {
         //cmdString = "select Instname from InstitutionType_M where Type='" + p + "' ";
         cmdString = "select Instname from InstitutionType_M where Type=@p";
        
         con = new SqlConnection(str);
         con.Open();
         cmd = new SqlCommand(cmdString, con);
         cmd.Parameters.AddWithValue("@p", p);
         cmd.CommandType = CommandType.Text;
         SqlDataReader sdr = cmd.ExecuteReader();
         string Instname = "";

         while (sdr.Read())
         {

             if (!Convert.IsDBNull(sdr["Instname"]))
             {
                 Instname = (string)sdr["Instname"];
             }
             else if (Convert.IsDBNull(sdr["Instname"]))
             {
                 Instname = "";
             }
         }
         return Instname;
     }
     catch (Exception ex)
     {
         log.Error("Inside getMaheInstitutionName of catch block ");
         log.Error(ex.Message);
         log.Error(ex.StackTrace);
         throw ex;
     }

     finally
     {
         con.Close();
     }
 }

 public GrantData getparcentagevalue(string Pid, string projectunit, string p1, string p2)
 {
     try
     {
         GrantData s = new GrantData();
         int Percentage = 0;
         if (p2 == "M")
         {
             //cmdString = "select InterOrganizationPercentage as Percentage,ActualAmountIO from Project_Percentage_Sharing  where  GId='" + projectunit + Pid + "' and Type='I' ";
             cmdString = "select InterOrganizationPercentage as Percentage,ActualAmountIO from Project_Percentage_Sharing  where  GId=@GId and Type='I' ";
              con = new SqlConnection(str);
             con.Open();
             cmd = new SqlCommand(cmdString, con);
             cmd.Parameters.AddWithValue("@GId", (projectunit + Pid));
          
             cmd.CommandType = CommandType.Text;
             SqlDataReader sdr = cmd.ExecuteReader();
             s.percentageIO = 0;
             s.percentageIOAmount = 0.0;

             while (sdr.Read())
             {

                 if (!Convert.IsDBNull(sdr["Percentage"]))
                 {
                     s.percentageIO = (int)sdr["Percentage"];
                 }
                 else if (Convert.IsDBNull(sdr["Percentage"]))
                 {
                     s.percentageIO = 0;
                 }
                 if (!Convert.IsDBNull(sdr["ActualAmountIO"]))
                 {
                     s.percentageIOAmount = Convert.ToDouble((decimal)sdr["ActualAmountIO"]);
                 }
                 else if (Convert.IsDBNull(sdr["ActualAmountIO"]))
                 {
                     s.percentageIOAmount = 0.0;
                 }
             }
         }
         else
         {
             //cmdString = "select InterOrganizationPercentage as Percentage,ActualAmountIO from Project_Percentage_Sharing  where  GId='" + projectunit + Pid + "' and InstId='" + p1 + "' ";
             cmdString = "select InterOrganizationPercentage as Percentage,ActualAmountIO from Project_Percentage_Sharing  where  GId=@GId and InstId=@p1";
             

             con = new SqlConnection(str);
             con.Open();
             cmd = new SqlCommand(cmdString, con);
             cmd.CommandType = CommandType.Text;
             cmd.Parameters.AddWithValue("@GId", projectunit + Pid);
             cmd.Parameters.AddWithValue("@p1", p1);
             SqlDataReader sdr = cmd.ExecuteReader();


             while (sdr.Read())
             {

                 if (!Convert.IsDBNull(sdr["Percentage"]))
                 {
                     s.percentageIO = (int)sdr["Percentage"];
                 }
                 else if (Convert.IsDBNull(sdr["Percentage"]))
                 {
                     s.percentageIO = 0;
                 }
                 if (!Convert.IsDBNull(sdr["ActualAmountIO"]))
                 {
                     s.percentageIOAmount = Convert.ToDouble((decimal)sdr["ActualAmountIO"]);
                 }
                 else if (Convert.IsDBNull(sdr["ActualAmountIO"]))
                 {
                     s.percentageIOAmount = 0.0;
                 }
             }
         }
         return s;
     }
     catch (Exception ex)
     {
         log.Error("Inside getparcentagevalue of catch block ");
         log.Error(ex.Message);
         log.Error(ex.StackTrace);
         throw ex;
     }

     finally
     {
         con.Close();
     }
 }

 public GrantData getparcentagevaluefordept(string Pid, string projectunit, string p1, string p2)
 {
     try
     {
         GrantData t = new GrantData();
         //cmdString = "select Percentage as Percentage,ActualAmountII from Project_Percentage_Sharing  where  GId='" + projectunit + Pid + "' and InstId='" + p1 + "' and DeptId='" + p2 + "' ";
         cmdString = "select Percentage as Percentage,ActualAmountII from Project_Percentage_Sharing  where  GId=@GId and InstId=@p1 and DeptId=@p2";
         
         con = new SqlConnection(str);
         con.Open();
         cmd = new SqlCommand(cmdString, con);
         cmd.Parameters.AddWithValue("@GId", (projectunit + Pid));
         cmd.Parameters.AddWithValue("@p1", p1);
         cmd.Parameters.AddWithValue("@p2", p2);
         cmd.CommandType = CommandType.Text;
         SqlDataReader sdr = cmd.ExecuteReader();
         t.percentageII = 0;
         t.percentageIIAmount = 0.0;

         while (sdr.Read())
         {

             if (!Convert.IsDBNull(sdr["Percentage"]))
             {
                 t.percentageII = (int)sdr["Percentage"];
             }
             else if (Convert.IsDBNull(sdr["Percentage"]))
             {
                 t.percentageII = 0;
             }
             if (!Convert.IsDBNull(sdr["ActualAmountII"]))
             {
                 t.percentageIIAmount = Convert.ToDouble((decimal)sdr["ActualAmountII"]);
             }
             else if (Convert.IsDBNull(sdr["ActualAmountII"]))
             {
                 t.percentageIIAmount = 0;
             }
         }
         return t;
     }
     catch (Exception ex)
     {
         log.Error("Inside getparcentagevaluefordept of catch block ");
         log.Error(ex.Message);
         log.Error(ex.StackTrace);
         throw ex;
     }

     finally
     {
         con.Close();
     }
 }

 public DataTable fnfinddistinctInstituteforPercentage(string Pid, string projectunit)
 {
     log.Debug("Inside fnfinddistinctInstituteforPercentage function, ProjectUnit: " + projectunit + "ID: " + Pid);

     con = new SqlConnection(str);
     con.Open();
     try
     {
         SqlDataAdapter da;
         DataTable ds;
         cmdString = "SelectInterInstitutionforPercentageSharing";
         cmd = new SqlCommand(cmdString, con);
         cmd.CommandType = CommandType.StoredProcedure;

         cmd.Parameters.Add("@ID", SqlDbType.VarChar, 15);
         cmd.Parameters["@ID"].Value = Pid;

         cmd.Parameters.Add("@GrantUnit", SqlDbType.VarChar, 5);
         cmd.Parameters["@GrantUnit"].Value = projectunit;
         da = new SqlDataAdapter(cmd);

         ds = new DataTable();
         da.Fill(ds);

         return ds;
     }

     catch (Exception ex)
     {
         log.Error("Inside fnfinddistinctInstituteforPercentage catch block ProjectUnit: " + projectunit + "ID: " + Pid);
         log.Error(ex.Message);
         log.Error(ex.StackTrace);
         throw ex;
     }

     finally
     {
         con.Close();
     }
 }

 public int UpdateStatusGrantEntryAcceptApprovalPercentage(GrantData j, GrantData[] jd, GrantData[] jd1, GrantData[] sd1, GrantData[] PO, GrantData[] PI)
 {
     log.Debug("Inside UpdateStatusGrantEntryAcceptApprovalPercentage function of Project Unit: " + j.GrantUnit + "ID: " + j.GID);
     int result = 0, result1 = 0, result2 = 0, result3 = 0, result4 = 0;
     con = new SqlConnection(str);
     con.Open();
     transaction = con.BeginTransaction();
     try
     {
         if (j.SancType == "CA")
         {
             //cmdString = "Select count(* ) as Count from Project_Percentage_Sharing where  GId='" + j.GrantUnit + j.GID + "'  ";
             cmdString = "Select count(* ) as Count from Project_Percentage_Sharing where  GId=@GId  ";
            
             cmd = new SqlCommand(cmdString, con, transaction);
             cmd.CommandType = CommandType.Text;
             cmd.Parameters.AddWithValue("@GId", (j.GrantUnit + j.GID));
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
             if (count >= 0)
             {
                 //cmdString = "delete from  Project_Percentage_Sharing where  GId='" + j.GrantUnit + j.GID + "'";
                 cmdString = "delete from  Project_Percentage_Sharing where  GId=@GId";
               
                 cmd = new SqlCommand(cmdString, con, transaction);
                 cmd.Parameters.AddWithValue("@GId", j.GrantUnit + j.GID);
                 cmd.CommandType = CommandType.Text;
                 //cmd.Parameters.AddWithValue("@ID", j.GID);
                 //cmd.Parameters.AddWithValue("@GrantUnit", j.GrantUnit);
                 result1 = cmd.ExecuteNonQuery();
             }
             for (int i = 0; i < PI.Length; i++)
             {

                 //cmdString = "Select count(* ) as Count from Project_Percentage_Sharing where  GId='" + j.GrantUnit + j.GID + "' and InstId='" + PI[i].Institution + "'and DeptId='" + PI[i].Department + "'  ";
                 cmdString = "Select count(* ) as Count from Project_Percentage_Sharing where  GId=@GId and InstId=@InstId and DeptId=@DeptId  ";
                
                 cmd = new SqlCommand(cmdString, con, transaction);
                 cmd.Parameters.AddWithValue("@GId", j.GrantUnit + j.GID);
                 cmd.Parameters.AddWithValue("@InstId", PI[i].Institution);
                 cmd.Parameters.AddWithValue("@DeptId", PI[i].Department);
                 cmd.CommandType = CommandType.Text;
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
                 if (count1 == 0)
                 {
                     //cmdString = "Select count(* ) as Count from Project_Percentage_Sharing where  GId='" + j.GrantUnit + j.GID + "' ";
                     cmdString = "Select count(* ) as Count from Project_Percentage_Sharing where  GId=@GId ";
                  
                     cmd = new SqlCommand(cmdString, con, transaction);
                     cmd.Parameters.AddWithValue("@GId", j.GrantUnit + j.GID);
                     cmd.CommandType = CommandType.Text;
                     SqlDataReader sdr2 = cmd.ExecuteReader();
                     int count2 = 0;
                     while (sdr2.Read())
                     {
                         if (!Convert.IsDBNull(sdr2["Count"]))
                         {
                             count2 = (int)sdr2["Count"];
                         }

                     }
                     sdr2.Close();
                     cmdString = "insert into Project_Percentage_Sharing (GId,EntryNo,Type,InstId,DeptId,Percentage,ActualAmountII)values(@GId,@EntryNo,@Type,@InstId,@DeptId,@DPercentage,@DPercentageAmount)";
                     cmd = new SqlCommand(cmdString, con, transaction);
                     cmd.CommandType = CommandType.Text;
                     cmd.Parameters.AddWithValue("@GId", j.GrantUnit + j.GID);
                     cmd.Parameters.AddWithValue("@EntryNo", count2 + 1);
                     cmd.Parameters.AddWithValue("@Type", PI[i].percentageType);
                     cmd.Parameters.AddWithValue("@InstId", PI[i].Institution);
                     cmd.Parameters.AddWithValue("@DeptId", PI[i].Department);
                     cmd.Parameters.AddWithValue("@DPercentage", PI[i].percentageII);
                     cmd.Parameters.AddWithValue("@DPercentageAmount", PI[i].percentageIIAmount);
                     result3 = cmd.ExecuteNonQuery();
                 }
                 else
                 {
                     cmdString = "update Project_Percentage_Sharing set Percentage=@DPercentage,ActualAmountII=@DPercentageAmount where GId=@GId and InstId=@InstId and DeptId=@DeptId";
                     cmd = new SqlCommand(cmdString, con, transaction);
                     cmd.CommandType = CommandType.Text;
                     cmd.Parameters.AddWithValue("@GId", j.GrantUnit + j.GID);
                     cmd.Parameters.AddWithValue("@InstId", PI[i].Institution);
                     cmd.Parameters.AddWithValue("@DeptId", PI[i].Department);
                     cmd.Parameters.AddWithValue("@DPercentage", PI[i].percentageII);
                     cmd.Parameters.AddWithValue("@DPercentageAmount", PI[i].percentageIIAmount);
                     result3 = cmd.ExecuteNonQuery();
                 }


             }
             for (int k = 0; k < PO.Length; k++)
             {
                 //cmdString = "Select count(* ) as Count from Project_Percentage_Sharing where  GId='" + j.GrantUnit + j.GID + "' and Type='I' ";
                 //cmd = new SqlCommand(cmdString, con, transaction);
                 //cmd.CommandType = CommandType.Text;
                 //SqlDataReader sdr1 = cmd.ExecuteReader();
                 //int count1 = 0;
                 //while (sdr1.Read())
                 //{
                 //    if (!Convert.IsDBNull(sdr1["Count"]))
                 //    {
                 //        count1 = (int)sdr1["Count"];
                 //    }

                 //}
                 //sdr1.Close();
                 if (PO[k].percentageType == "I")
                 {
                     //cmdString = "update Project_Percentage_Sharing set InterOrganizationPercentage=@DPercentage,ActualAmountIO=@DPercentageIOAmount where GId='" + j.GrantUnit + j.GID + "' and  Type='" + PO[k].percentageType + "' ";
                     cmdString = "update Project_Percentage_Sharing set InterOrganizationPercentage=@DPercentage,ActualAmountIO=@DPercentageIOAmount where GId=@GId and  Type=@Type ";

                     cmd = new SqlCommand(cmdString, con, transaction);
                     cmd.CommandType = CommandType.Text;
                     //cmd.Parameters.AddWithValue("@GId", j.GrantUnit + j.GID);
                     cmd.Parameters.AddWithValue("@DPercentage", PO[k].percentageIO);
                     cmd.Parameters.AddWithValue("@DPercentageIOAmount", PO[k].percentageIOAmount);

                     cmd.Parameters.AddWithValue("@GId", j.GrantUnit + j.GID);
                     cmd.Parameters.AddWithValue("@Type", PO[k].percentageType);
                     result3 = cmd.ExecuteNonQuery();

                 }
                 else
                 {
                     //cmdString = "Select count(* ) as Count from Project_Percentage_Sharing where  GId='" + j.GrantUnit + j.GID + "' ";
                     cmdString = "Select count(* ) as Count from Project_Percentage_Sharing where  GId=@GId ";
                    
                     cmd = new SqlCommand(cmdString, con, transaction);
                     cmd.Parameters.AddWithValue("@GId", j.GrantUnit + j.GID);

                     cmd.CommandType = CommandType.Text;
                     SqlDataReader sdr3 = cmd.ExecuteReader();
                     int count3 = 0;
                     while (sdr3.Read())
                     {
                         if (!Convert.IsDBNull(sdr3["Count"]))
                         {
                             count3 = (int)sdr3["Count"];
                         }

                     }
                     sdr3.Close();
                     cmdString = "insert into Project_Percentage_Sharing (GId,EntryNo,Type,InstId,InterOrganizationPercentage,ActualAmountIO)values(@GId,@EntryNo,@Type,@InstId,@DPercentage,@DPercentageIOAmount)";
                     cmd = new SqlCommand(cmdString, con, transaction);
                     cmd.CommandType = CommandType.Text;
                     cmd.Parameters.AddWithValue("@GId", j.GrantUnit + j.GID);
                     cmd.Parameters.AddWithValue("@EntryNo", count3 + 1);
                     cmd.Parameters.AddWithValue("@Type", PO[k].percentageType);
                     cmd.Parameters.AddWithValue("@InstId", PO[k].Institution);
                     cmd.Parameters.AddWithValue("@DPercentage", PO[k].percentageIO);
                     cmd.Parameters.AddWithValue("@DPercentageIOAmount", PO[k].percentageIOAmount);
                     result3 = cmd.ExecuteNonQuery();
                 }


             }

         }

         transaction.Commit();
         log.Info("Updated Project Review tracker of Project Unit: " + j.GrantUnit + "ID: " + j.GID);
         return result3;

     }
     catch (Exception ex)
     {
         log.Error("Inside UpdateStatusGrantEntryAcceptApprovalPercentage catch block of Project Unit: " + j.GrantUnit + "ID: " + j.GID);
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

 public int CheckPercentageSharingDetails(string id, string unit)
 {
     try
     {
         //cmdString = "select count(*) as Count from Project_Percentage_Sharing where GId='" + (unit + id) + "' ";
         cmdString = "select count(*) as Count from Project_Percentage_Sharing where GId=@GId ";
         
         con = new SqlConnection(str);
         con.Open();
         cmd = new SqlCommand(cmdString, con);
         cmd.Parameters.AddWithValue("@GId", (unit + id));
         cmd.CommandType = CommandType.Text;
         SqlDataReader sdr = cmd.ExecuteReader();
         int count = 0;

         while (sdr.Read())
         {

             if (!Convert.IsDBNull(sdr["Count"]))
             {
                 count = (int)sdr["Count"];
             }
             else if (Convert.IsDBNull(sdr["Count"]))
             {
                 count = 0;
             }
         }
         return count;
     }
     catch (Exception ex)
     {
         log.Error("Inside CheckPercentageSharingDetails of catch block ");
         log.Error(ex.Message);
         log.Error(ex.StackTrace);
         throw ex;
     }

     finally
     {
         con.Close();
     }
 }



 public DataTable SelectProjectOutcomeDetails(string Pid, string projectunit)
 {
     log.Debug("Inside SelectProjectOutcomeDetails function of Project Unit: " + projectunit + "ID: " + Pid);
     con = new SqlConnection(str);
     con.Open();
     IncentiveData data = new IncentiveData();
     try
     {
         cmdString = "Select * from ProjectOutcome where ProjectOutcome.ProjectUnit=@ProjectUnit and ProjectOutcome.ProjectID=@ID  ";
         cmd = new SqlCommand(cmdString, con, transaction);
         cmd.CommandType = CommandType.Text;
         cmd.Parameters.AddWithValue("@ID", Pid);
         cmd.Parameters.AddWithValue("@ProjectUnit", projectunit);
         SqlDataAdapter da = new SqlDataAdapter(cmd);

         DataTable ds = new DataTable();
         da.Fill(ds);

         return ds;
     }
     catch (Exception ex)
     {
         log.Error("Inside SelectProjectOutcomeDetails catch block of Project Unit: " + projectunit + "ID: " + Pid);
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

 public int InsertProjectOutcomeDetails(RecieptData[] JDP, GrantData j)
 {
     log.Debug("Inside InsertProjectOutcomeDetails function of Project Unit: " + j.GrantUnit + "ID: " + j.GID);
     con = new SqlConnection(str);
     con.Open();
     transaction = con.BeginTransaction();
     int result = 0;
     try
     {
         cmdString = "delete from ProjectOutcome where ProjectUnit=@ProjectUnit and ProjectID=@ID";
         cmd = new SqlCommand(cmdString, con, transaction);
         cmd.CommandType = CommandType.Text;

         cmd.Parameters.AddWithValue("@ID", j.GID);
         cmd.Parameters.AddWithValue("@ProjectUnit", j.GrantUnit);
         cmd.ExecuteNonQuery();

         for (int i = 0; i < JDP.Length; i++)
         {
             //cmdString = "Select count(* ) as Count from ProjectOutcome where  ProjectID='" + j.GID + "' and ProjectUnit='" + j.GrantUnit + "' ";
             cmdString = "Select count(* ) as Count from ProjectOutcome where  ProjectID=@ProjectID and ProjectUnit=@ProjectUnit ";
            
             cmd = new SqlCommand(cmdString, con, transaction);
             cmd.Parameters.AddWithValue("@ProjectID", j.GID);
             cmd.Parameters.AddWithValue("@ProjectUnit", j.GrantUnit);
             cmd.CommandType = CommandType.Text;
             SqlDataReader sdr3 = cmd.ExecuteReader();
             int count3 = 0;
             while (sdr3.Read())
             {
                 if (!Convert.IsDBNull(sdr3["Count"]))
                 {
                     count3 = (int)sdr3["Count"];
                 }

             }
             sdr3.Close();
             cmdString = "insert into ProjectOutcome(Id,ProjectID,ProjectUnit,Description,Updatedby,UpdatedDate,OutcomeDate)values(@Id,@ProjectID,@ProjectUnit,@Description,@Updatedby,@UpdatedDate,@OutcomeDate)";
             cmd = new SqlCommand(cmdString, con, transaction);
             cmd.CommandType = CommandType.Text;
             cmd.Parameters.AddWithValue("@Id", count3 + 1);
             cmd.Parameters.AddWithValue("@ProjectID", j.GID);
             cmd.Parameters.AddWithValue("@ProjectUnit", j.GrantUnit);
             cmd.Parameters.AddWithValue("@Description", JDP[i].ProjectOutcomeDescription);
             cmd.Parameters.AddWithValue("@OutcomeDate", JDP[i].OutcomeDate);
             cmd.Parameters.AddWithValue("@Updatedby", JDP[i].Updatedby);
             cmd.Parameters.AddWithValue("@UpdatedDate", JDP[i].UpdatedDate);
             result = cmd.ExecuteNonQuery();

         }



         cmd = new SqlCommand("Update Project set HasOutcome='Y' where ProjectUnit+ID=@ProjectID", con, transaction);
         cmd.CommandType = CommandType.Text;
         cmd.Parameters.AddWithValue("@ProjectID", j.GrantUnit + j.GID);
         result = cmd.ExecuteNonQuery();


         log.Debug("Project Outcome details inserted successfully of Project Unit: " + j.GrantUnit + "ID: " + j.GID);
         transaction.Commit();
         return result;

     }
     catch (Exception ex)
     {
         log.Error("Inside InsertProjectOutcomeDetails  catch block of Project Unit: " + j.GrantUnit + "ID: " + j.GID);
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

 internal string FindMemberIdinFeedBackTracker(string MemberID, string Type, string ID)
 {
     log.Debug("Inside - Select FindMemberIdinFeedBackTracker the MemberID ='" + MemberID + "'");

     string VALUE = null;
     con.Open();

     try
     {

         //cmd = new SqlCommand("select PubFeedbackLastDate from User_M where User_Id='" + MemberID + "'", con);
         cmd = new SqlCommand("select PubFeedbackLastDate from User_M where User_Id=@MemberID", con);
         cmd.Parameters.AddWithValue("@MemberID", MemberID);
         cmd.CommandType = CommandType.Text;
         VALUE = Convert.ToString(cmd.ExecuteScalar());

         return VALUE;

     }

     catch (Exception ex)
     {
         log.Error(" Error Inside -  Select FindMemberIdinFeedBackTracker ");
         log.Error(ex.Message);
         log.Error(ex.StackTrace);
         throw ex;
     }

     finally
     {
         con.Close();
     }
 }

 internal FeedbackClass CheckUserforFeedback(string MemberID, string Type)
 {
     FeedbackClass det = new FeedbackClass();

     try
     {

         con.Open();
         if (Type == "Pub")
         {
             //cmd = new SqlCommand("select PubFeedbackLastDate from User_M where User_Id='" + MemberID + "'", con);
             cmd = new SqlCommand("select PubFeedbackLastDate from User_M where User_Id=@MemberID", con);
             cmd.Parameters.AddWithValue("@MemberID", MemberID);
             cmd.CommandType = CommandType.Text;
             SqlDataReader sdr = cmd.ExecuteReader();

             while (sdr.Read())
             {


                 if (!Convert.IsDBNull(sdr["PubFeedbackLastDate"]))
                 {
                     det.PublicationUpdatedDate =(DateTime)sdr["PubFeedbackLastDate"];
                 }
                


             }
         }
         else
         {
             //cmd = new SqlCommand("select PrjFeedbackLastDate from User_M where User_Id='" + MemberID + "'", con);
             cmd = new SqlCommand("select PrjFeedbackLastDate from User_M where User_Id=@MemberID", con);
             cmd.Parameters.AddWithValue("@MemberID", MemberID);
             cmd.CommandType = CommandType.Text;
             SqlDataReader sdr = cmd.ExecuteReader();

             while (sdr.Read())
             {


                 if (!Convert.IsDBNull(sdr["PrjFeedbackLastDate"]))
                 {
                     det.ProjectUpdatedDate = (DateTime)sdr["PrjFeedbackLastDate"];
                 }


             }
         }
         return det;

     }



     catch (Exception ex)
     {
         log.Error("Error Inside FindMemberIdinFeedBackTracker function  of ID " + MemberID + " ");
         throw ex;
     }

     finally
     {
         con.Close();
     }
 }
}