using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for User_Mangement
/// </summary>
public class User_Mangement
{
	public User_Mangement()
	{
		//
		// TODO: Add constructor logic here
		//
	}
     public Int32 SaveDepartmentDetails(User b)// for saving details in database
    {
        UserDAL objUDal = new UserDAL();//  creating object of data access layer
        try
        {
            return objUDal.InsertDepartment(b);// sending values from Businesslayer to access layer
        }
        catch (Exception ex)
        {
            throw;
        }
   
    }
    public int Update(User b)
    {
        UserDAL objUDal = new UserDAL();
        try
        {

            return objUDal.Update(b);
        }
        catch
        {
            throw;
        }

    }
    public User selectExistingUser(string UserId)
    {
        UserDAL objUDal = new UserDAL();
        try
        {
            return objUDal.selectExistingUser(UserId);
        }
        catch
        {
            throw;
        }
      
    }

    public User selectPubInchargeUM(string UserId)
    {
        UserDAL objUDal = new UserDAL();
        try
        {
            return objUDal.selectPubInchargeUM(UserId);
        }
        catch
        {
            throw;
        }

    }

    public Int32 InsertDepartmentInstituteAutoAppove(User b)// for saving details in database
    {
        UserDAL objUDal = new UserDAL();//  creating object of data access layer
        try
        {
            return objUDal.InsertDepartmentInstituteAutoAppove(b);// sending values from Businesslayer to access layer
        }
        catch (Exception ex)
        {
            throw;
        }

    }

    public System.Data.DataSet GetHREmpData()
    {
        UserDAL objUDal = new UserDAL();
        try
        {
            return objUDal.GetHREmpData();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public int SaveStudentDetails(User b)
    {
        UserDAL objUDal = new UserDAL();//  creating object of data access layer
        try
        {
            return objUDal.SaveStudentDetails(b);// sending values from Businesslayer to access layer
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public int UpdateStudentdetails(User b)
    {
        UserDAL objUDal = new UserDAL();//  creating object of data access layer
        try
        {
            return objUDal.UpdateStudentdetails(b);// sending values from Businesslayer to access layer
        }
        catch (Exception ex)
        {
            throw;
        }  
    }

    public User selectExistingStudentDetails(string p)
    {
        UserDAL objUDal = new UserDAL();//  creating object of data access layer
        try
        {
            return objUDal.selectExistingStudentDetails(p);// sending values from Businesslayer to access layer
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}
         
