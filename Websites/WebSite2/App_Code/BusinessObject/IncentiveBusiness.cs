using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for IncentiveBusiness
/// </summary>
public class IncentiveBusiness
{
    Incentive_DataObject obj = new Incentive_DataObject();

    public System.Data.DataSet SelectPendingProcessedPublications(PublishData data)
    {
        try
        {

            return obj.SelectPendingProcessedPublications(data);

        }
        catch
        {
            throw;
        }
    }

    public System.Data.DataTable SelectAuthorDetails(string Pid, string TypeEntry)
    {
        try
        {

            return obj.SelectAuthorDetails(Pid,TypeEntry);

        }
        catch
        {
            throw;
        }
    }

    public bool InsertIncentivePointToAuthor(PublishData[] JD,PublishData data )
    {
        try
        {

            return obj.InsertIncentivePointToAuthor(JD,data);

        }
        catch
        {
            throw;
        }
    }

    public bool ApproveIncentiveStatus(PublishData[] JD, PublishData data)
    {
        try
        {

            return obj.ApproveIncentiveStatus(JD, data);

        }
        catch
        {
            throw;
        }
    }

    public string checkIncentivePointStatus(string Pid, string TypeEntry)
    {
        try
        {
            return obj.checkIncentivePointStatus(Pid, TypeEntry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public int CountThresholdPublicationPoint(PublishData obj1)
    {
        try
        {
            return obj.CountThresholdPublicationPoint(obj1);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public IncentivePoint SelectSNIPJRPoint(string issn,string year)
    {
        try
        {
            return obj.SelectSNIPJRPoint(issn,year);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    public System.Data.DataTable SelectPatentInventorDetail(string ID)
    {
        try
        {
            return obj.SelectPatentInventorDetail(ID);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public bool InsertIncentivePointToPatentAuthor(PublishData[] JD, PublishData data)
    {
        try
        {
            return obj.InsertIncentivePointToPatentAuthor(JD,data);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public bool ApprovePatenIncentiveStatus(PublishData[] JD, PublishData data)
    {
        try
        {
            return obj.ApprovePatenIncentiveStatus(JD, data);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public PublishData SelectMemberDetails(string MemberId)
    {
        try
        {
            return obj.SelectMemberDetails(MemberId);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public IncentivePoint SelectMemberCurBalance(string memberid)
    {
        try
        {
            return obj.SelectMemberCurBalance(memberid);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //Ashwini
    

    public bool UpdateCurBal(IncentivePoint Id)
    {
        try
        {
            return obj.UpdateCurBal(Id);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }



    public bool InsertUtilizationPoint(IncentivePoint data)
    {
        try
        {
            return obj.InsertUtilizationPoint(data);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public System.Data.DataSet SelectMembersInstitutewise(string inst, string memberid)
    {
        try
        {
            return obj.SelectMembersInstitutewise(inst, memberid);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //public bool DiscardIncentivePointEntry(PublishData obj1)
    //{
    //    try
    //    {
    //        return obj.DiscardIncentivePointEntry(obj1);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    public bool CheckPublcationId(string publicationid,string memberid)
    {
        try
        {
            return obj.CheckPublcationId(publicationid,memberid);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    public string SelectAuthorEmailId(string employeeid)
    {
        try
        {
            return obj.SelectAuthorEmailId(employeeid);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public string SelectHREmailId(string employeeid, string referenceid, string transctiontype)
    {
        try
        {
            return obj.SelectHREmailId(employeeid, referenceid, transctiontype);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //public string SelectStudentAuthorEmailId(string employeeid)
    //{
    //    try
    //    {
    //        return obj.SelectStudentAuthorEmailId(employeeid);
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    public bool UpdateUtilizationMailFlag(string employeeid, string referenceid,string transactiontype)
    {
        try
        {
            return obj.UpdateUtilizationMailFlag(employeeid, referenceid, transactiontype);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public System.Data.DataTable SelectMUAuthorDetails(string Pid, string TypeEntry)
    {
        try
        {
            return obj.SelectMUAuthorDetails(Pid, TypeEntry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }



    public PublishData SelectPublicationData(string memberid, string p)
    {
        try
        {
            return obj.SelectPublicationData(memberid, p);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public string SelectStudentEmailId(string empcode,string id)
    {
        try
        {
            return obj.SelectStudentEmailId(empcode, id);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public System.Data.DataTable CountRemainingPoints( string memberid)
    {
        try
        {
            return obj.CountRemainingPoints(memberid);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public System.Data.DataTable CountUtilizationPoints(string memberid)
    {
        try
        {
            return obj.CountUtilizationPoints(memberid);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public System.Data.DataSet SelectMembersInstitutewise(string inst, string memberid, string membername)
    {
        try
        {
            return obj.SelectMembersInstitutewise(inst, memberid, membername);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public string SelectHRMailID(string empcode, string p, string p2)
    {
        try
        {
            return obj.SelectHRMailID(empcode, p,p2);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public IncentivePoint SelectMemberCurBalanceInstitutewise(string memberid, string inst,string role)
    {
        try
        {
            return obj.SelectMemberCurBalanceInstitutewise(memberid, inst,role);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    public bool AdditionalPointAward(string memberid, IncentivePoint j)
    {
        try
        {
            return obj.AdditionalPointAward(memberid, j);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public bool CheckMemberId(string memberid)
    {
        try
        {
            return obj.CheckMemberId(memberid);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public string SelectMemberCurrentBal(string memberid)
    {
        try
        {
            return obj.SelectMemberCurrentBal(memberid);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public IncentivePoint SelectPublicationCount(string memberid, string year)
    {
        try
        {
            return obj.SelectPublicationCount(memberid, year);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public string SelectMemberType(string memberid)
    {
        try
        {
            return obj.SelectMemberType(memberid);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public System.Collections.ArrayList SelectImpactFactor(PublishData v)
    {
        try
        {
            return obj.SelectImpactFactor(v);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public System.Data.DataSet SelectIncentivePoints(PublishData v)
    {
        try
        {
            return obj.SelectIncentivePoints(v);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public System.Collections.ArrayList SelectHODMailid(string empcode, string p1, string p2)
    {
        try
        {
            return obj.SelectHODMailid(empcode, p1, p2);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public string CheckInstitution(string p)
    {
        try
        {
            return obj.CheckInstitution(p);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    public System.Data.DataSet SelectFacultyInstitutewise(string inst, string memberid, string membername)
    {
        try
        {
            return obj.SelectFacultyInstitutewise(inst, memberid, membername);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public System.Data.DataSet SelectStudentInstitutewise(string inst, string memberid, string membername)
    {
        try
        {
            return obj.SelectStudentInstitutewise(inst, memberid, membername);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public System.Collections.ArrayList SelectCountOfRole(string user)
    {
        try
        {
            return obj.SelectCountOfRole(user);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    public System.Data.DataSet SelectStudentInstitute(string inst)
    {
        try
        {
            return obj.SelectStudentInstitute(inst);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public System.Data.DataSet SelectFacultyInstitute(string inst)
    {
        try
        {
            return obj.SelectFacultyInstitute(inst);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public System.Data.DataSet SelectMembersInstitute(string inst)
    {
        try
        {
            return obj.SelectMembersInstitute(inst);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

   
    public string SelectInstwiseHRMailid(string empcode, string p1, string p2)
    {
         try
         {
             return obj.SelectInstwiseHRMailid(empcode, p1, p2);
         }
         catch (Exception ex)
         {
             throw ex;
         }
    }

    public string SelectYearWisePoints(string memberid, string year)
    {
        try
        {
            return obj.SelectYearWisePoints(memberid, year);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public System.Data.DataSet SelectApprovedIncentivePublications(PublishData data)
    {
        try
        {

            return obj.SelectApprovedIncentivePublications(data);

        }
        catch
        {
            throw;
        }
    }

    public bool InsertSJRPointToAuthor(PublishData[] JD, PublishData data)
    {
        try
        {

            return obj.InsertSJRPointToAuthor(JD, data);

        }
        catch
        {
            throw;
        }
    }

    public System.Data.DataTable GetArticalWisePoints(string MemberId)
    {
        try
        {

            return obj.GetArticalWisePoints(MemberId);

        }
        catch
        {
            throw;
        }
    }

    public bool CheckPatentId(string p1, string p2)
    {
        try
        {
            return obj.CheckPatentId(p1,p2);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public string SelectAuthorName(string empcode)
    {
        try
        {
            return obj.SelectAuthorName(empcode);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }



    public string SelectStudentAuthorName(string empcode, string p)
    {
        try
        {
            return obj.SelectStudentAuthorName(empcode,p);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public IncentiveData CheckUniqueIdIncentive(string p1, string p2, EmailDetails details)
    {
        try
        {
            return obj.CheckUniqueIdIncentive(p1, p2, details );
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public int updateEmailtrackerIncentive(string p1, string p2, EmailDetails details, IncentiveData obj3,string AuthorName1)
    {
        try
        {
            return obj.updateEmailtrackerIncentive(p1, p2, details, obj3, AuthorName1);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public PublishData getquartileName(string p)
    {
        try
        {
            return obj.getquartileName(p);
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    public PublishData getquartileid(string p)
    {
        try
        {
            return obj.getquartileid(p);
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    public PublishData getquartilecount(string p1, string p2,string p3)
    {
        try
        {
            return obj.getquartilecount(p1,p2,p3);
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    public PublishData getquartilelimit(string p)
    {
        try
        {
            return obj.getquartilelimit(p);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public System.Data.DataTable SelectMUAuthorDetailsforARI(string Pid, string TypeEntry)
    {
        try
        {
            return obj.SelectMUAuthorDetailsforARI(Pid, TypeEntry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public System.Data.DataTable SelectAuthorDetailsforARI(string Pid, string TypeEntry)
    {
        try
        {

            return obj.SelectAuthorDetailsforARI(Pid, TypeEntry);

        }
        catch
        {
            throw;
        }
    }

    public System.Data.DataSet SelectApprovedIncentivePublicationsforARI(PublishData data)
    {
        try
        {

            return obj.SelectApprovedIncentivePublicationsforARI(data);

        }
        catch
        {
            throw;
        }
    }

    public bool InsertARIPointToAuthor(PublishData[] JD, PublishData data)
    {
        try
        {

            return obj.InsertARIPointToAuthor(JD, data);

        }
        catch
        {
            throw;
        }
    }

    public PublishData CheckARIApplicability(object p)
    {
        try
        {

            return obj.CheckARIApplicability(p);

        }
        catch
        {
            throw;
        }
    }

    public System.Data.DataSet SelectPendingProcessedPublicationsForIncentivePointRevert(PublishData data)
    {
        try
        {

            return obj.SelectPendingProcessedPublicationsForIncentivePointRevert(data);

        }
        catch
        {
            throw;
        }
    }




    public IncentivePoint SelectRevertingPointsforAuthorDetails(string p1, string p2,string p3)
    {
        try
        {

            return obj.SelectRevertingPointsforAuthorDetails(p1,p2,p3);

        }
        catch
        {
            throw;
        }
    }

    public bool RevertIncentivePointToAuthor(IncentivePoint[] JD, PublishData data)
    {
        try
        {

            return obj.RevertIncentivePointToAuthor(JD, data);

        }
        catch
        {
            throw;
        }
    }

    public PublishData CheckIncentivePointEntry(PublishData[] JD, PublishData data)
    {
        try
        {

            return obj.CheckIncentivePointEntry(JD, data);

        }
        catch
        {
            throw;
        }
    }

    public PublishData CheckIncentivePointRevertStatus(IncentivePoint[] JD, PublishData data)
    {
        try
        {

            return obj.CheckIncentivePointRevertStatus(JD, data);

        }
        catch
        {
            throw;
        }
    }

    public PublishData CheckIncentivePointstatusPatent(PublishData[] JD, PublishData data)
    {
        try
        {

            return obj.CheckIncentivePointstatusPatent(JD, data);

        }
        catch
        {
            throw;
        }
    }

    public PublishData getIsStudentAuthor(string p)
    {
        try
        {

            return obj.getIsStudentAuthor(p);

        }
        catch
        {
            throw;
        }
    }

    public System.Collections.ArrayList selectHrAdditionalInstitute(string UserId)
    {
        try
        {
            return obj.selectHrAdditionalInstitute(UserId);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public System.Data.DataSet SelectMembersAdditionalInstitute(string UserId)
    {
        try
        {
            return obj.SelectMembersAdditionalInstitute(UserId);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public System.Data.DataSet SelectMembersAdditionalInstitutewise(string UserId, string member, string membername)
    {
        try
        {
            return obj.SelectMembersAdditionalInstitutewise(UserId, member, membername);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

  
}