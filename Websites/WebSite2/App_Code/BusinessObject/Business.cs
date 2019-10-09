using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;

public class Business
{

    Login_DataObject L = new Login_DataObject();

    Journal_DataObject J = new Journal_DataObject();
    Grant_DataObject g = new Grant_DataObject();
    UserDAL u = new UserDAL();

    public int InsertJCFileUploadCSV(JournalData[] jd)
    {
        try
        {

            return J.InsertJCFileUploadCSV(jd);

        }
        catch
        {
            throw;
        }

    }

    public int InsertUserUploadExcel(User[] jd, string path)
    {
        try
        {

            return L.InsertUserUploadExcel(jd, path);

        }
        catch
        {
            throw;
        }

    }
    //public IndexManage selectJournal(string Jid,string year)
    //{
    //    try
    //    {
    //        return J.selectJournal(Jid,year);
    //    }
    //    catch
    //    {
    //        throw;
    //    }

    //}

    public DataSet IndexAgency()
    {
        try
        {
            return J.IndexAgency();
        }
        catch
        {
            throw;
        }

    }

    //public int InsertIndexAgency(IndexManage m, ArrayList listIndexAgency)
    //{
    //    try
    //    {

    //        return J.InsertIndexAgency(m, listIndexAgency);

    //    }
    //    catch
    //    {
    //        throw;
    //    }

    //}



    public IndexManage selectIndexAgency(string empc)
    {
        try
        {

            return J.selectIndexAgency(empc);
        }
        catch
        {
            throw;
        }

    }
    public int IndexAgencyInsert(IndexManage R)
    {
        try
        {

            return J.IndexAgencyInsert(R);
        }
        catch
        {
            throw;
        }

    }
    public int IndexAgencyUpdate(IndexManage R)
    {
        try
        {

            return J.IndexAgencyUpdate(R);
        }
        catch
        {
            throw;
        }

    }


    public JournalData JournalEntryCheckExistance(JournalData jdValueObj)
    {
        try
        {

            return J.JournalEntryCheckExistance(jdValueObj);

        }
        catch
        {
            throw;
        }

    }

    public int JournalEntryUpdateChanges(JournalData jdValueObj, ArrayList list)
    {
        try
        {

            return J.JournalEntryUpdateChanges(jdValueObj, list);

        }
        catch
        {
            throw;
        }

    }

    public JournalData JournalGetImpactFactor(JournalData jdValueObj)
    {
        try
        {

            return J.JournalGetImpactFactor(jdValueObj);

        }
        catch
        {
            throw;
        }

    }

    public JournalData JournalGetImpactFactorPublishEntry(JournalData jdValueObj)
    {
        try
        {

            return J.JournalGetImpactFactorPublishEntry(jdValueObj);

        }
        catch
        {
            throw;
        }

    }


    public int IFcheckSaveOrUpdate(JournalData jdValueObj)
    {
        try
        {

            return J.IFcheckSaveOrUpdate(jdValueObj);

        }
        catch
        {
            throw;
        }

    }
    // JournalEntryUpdateChanges

    public int JournalEntrySaveChanges(JournalData jdValueObj, ArrayList list)
    {
        try
        {

            return J.JournalEntrySaveChanges(jdValueObj, list);

        }
        catch
        {
            throw;
        }




    }

    // JournalEntryUpdateChanges

    public int JournalEntrySaveChanges1(JournalData jdValueObj, ArrayList list)
    {
        try
        {

            return J.JournalEntrySaveChanges1(jdValueObj, list);

        }
        catch
        {
            throw;
        }




    }


    //insert PublishEntry
    public int insertPublishEntry(PublishData j, PublishData[] jd, ArrayList listIndexAgency)
    {
        try
        {

            return J.insertPublishEntry(j, jd, listIndexAgency);
        }
        catch
        {
            throw;
        }

    }


    //update PublishEntry
    public int UpdatePublishEntry(PublishData j, PublishData[] jd, ArrayList listIndexAgency)
    {
        try
        {

            return J.UpdatePublishEntry(j, jd, listIndexAgency);
        }
        catch
        {
            throw;
        }

    }


    //update PublishEntry for librarian
    public int UpdatePublishLibraryEntry(PublishData j, PublishData[] jd, ArrayList listIndexAgency)
    {
        try
        {

            return J.UpdatePublishLibraryEntry(j, jd, listIndexAgency);
        }
        catch
        {
            throw;
        }

    }


    //cancel PublishEntry 
    public int UpdateCancelPublishEntry(PublishData j)
    {
        try
        {

            return J.UpdateCancelPublishEntry(j);
        }
        catch
        {
            throw;
        }

    }


    //update impact factor of PublishEntry 
    public int UpdateImpFactorPublishEntry(PublishData j)
    {
        try
        {

            return J.UpdateImpFactorPublishEntry(j);
        }
        catch
        {
            throw;
        }

    }


    //update post approval of PublishEntry -after the approval

    public int UpdatePostApprovePublishEntry(PublishData j, PublishData[] jd, ArrayList listIndexAgency)
    {
        try
        {

            return J.UpdatePostApprovePublishEntry(j, jd, listIndexAgency);
        }
        catch
        {
            throw;
        }

    }




    //update pdf path---for publish entry

    public int UpdatePfPath(PublishData p, PublishData[] JD, ArrayList agency)
    {
        try
        {

            return J.UpdatePfPath(p, JD, agency);
        }
        catch
        {
            throw;
        }

    }




    //update PublisAcceptReject

    public int UpdatePublishAcceptReject(PublishData p)
    {
        try
        {

            return J.UpdatePublishAcceptReject(p);
        }
        catch
        {
            throw;
        }

    }


    //update Publis-pdf path crete-upload

    public int UpdatePfPathCreate(PublishData p)
    {
        try
        {

            return J.UpdatePfPathCreate(p);
        }
        catch
        {
            throw;
        }

    }


    public string GetInstitutionName(string InstituteId)
    {
        try
        {

            return J.GetInstitutionName(InstituteId);

        }
        catch
        {
            throw;
        }




    }



    public string GetLibraryId(string inst, string dept)
    {
        try
        {

            return J.GetLibraryId(inst, dept);

        }
        catch
        {
            throw;
        }




    }


    public string GetSupId(string inst, string uid, string DeptId)
    {
        try
        {

            return J.GetSupId(inst, uid, DeptId);

        }
        catch
        {
            throw;
        }




    }
    public string GetIstDeptAutoApprove(string inst, string DeptId)
    {
        try
        {

            return J.GetIstDeptAutoApprove(inst, DeptId);

        }
        catch
        {
            throw;
        }




    }

    public string GetDeptName(string Deptid, string institutionid)
    {
        try
        {

            return J.GetDeptName(Deptid, institutionid);

        }
        catch
        {
            throw;
        }




    }


    public User GetUserName(string EmployeeCode1)
    {
        try
        {

            return J.GetUserName(EmployeeCode1);

        }
        catch
        {
            throw;
        }




    }

    public string SelectQuartile(PublishData v)
    {
        try
        {

            return J.SelectQuartile(v);

        }
        catch
        {
            throw;
        }
    }

    public User GetPublicationIncharge(string user)
    {
        try
        {

            return J.GetPublicationIncharge(user);

        }
        catch
        {
            throw;
        }




    }


    public User GetPublicationInchargeInst(string user)
    {
        try
        {

            return J.GetPublicationInchargeInst(user);

        }
        catch
        {
            throw;
        }




    }




    public User GetPublicationInchargeInstLibraryMap(string inst, string dept, string email)
    {
        try
        {

            return J.GetPublicationInchargeInstLibraryMap(inst, dept, email);

        }
        catch
        {
            throw;
        }




    }


    public PublishData fnfindjid(string SHNo, string bullec)
    {
        try
        {
            return J.fnfindjid(SHNo, bullec);
        }
        catch
        {
            throw;
        }

    }

    public String fnfindjidgtjname(string SHNo, string bullec)
    {
        try
        {
            return J.fnfindjidgtjname(SHNo, bullec);
        }
        catch
        {
            throw;
        }

    }


    public String GetFileUploadPath(string pubid, string entrytype)
    {
        try
        {
            return J.GetFileUploadPath(pubid, entrytype);
        }
        catch
        {
            throw;
        }

    }

    public String GetPublishRejectOwner(string pubid, string entrytype)
    {
        try
        {
            return J.GetPublishRejectOwner(pubid, entrytype);
        }
        catch
        {
            throw;
        }

    }


    public String GetAuthorsSupervisor(string Authors)
    {
        try
        {
            return J.GetAuthorsSupervisor(Authors);
        }
        catch
        {
            throw;
        }

    }

    public String GetAuthorsSupervisorgetMail(string Authors)
    {
        try
        {
            return J.GetAuthorsSupervisorgetMail(Authors);
        }
        catch
        {
            throw;
        }

    }
    public DataTable fnfindjournalAccount(string SHNo, string bu)
    {
        try
        {
            return J.fnfindjournalAccount(SHNo, bu);
        }
        catch
        {
            throw;
        }

    }
    public DataSet fnfindjournalAccount1(string pid, string typeentry)
    {
        try
        {
            return J.fnfindjournalAccount1(pid, typeentry);
        }
        catch
        {
            throw;
        }

    }

    public DataSet getAuthorList(string Id, string Type)
    {
        try
        {
            return J.getAuthorList(Id, Type);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataSet getReserachClerksList()
    {
        try
        {
            return J.getReserachClerksList();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataSet getReserachDirectorList()
    {
        try
        {
            return J.getReserachDirectorList();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataSet getAuthorListName(string Id, string Type)
    {
        try
        {
            return J.getAuthorListName(Id, Type);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }




    //update grantEntry
    public int UpdateGrantEntry(GrantData j, GrantData[] jd)
    {
        try
        {

            return g.UpdateGrantEntry(j, jd);
        }
        catch
        {
            throw;
        }

    }




    //delete attached file --grant
    public int UpdateGrantattachedEntry(GrantData j)
    {
        try
        {

            return g.UpdateGrantattachedEntry(j);
        }
        catch
        {
            throw;
        }

    }


    //approval-grant-reject
    public int UpdateStatusGrantEntryRejectApproval(GrantData j, GrantData[] jd)
    {
        try
        {

            return g.UpdateStatusGrantEntryRejectApproval(j, jd);
        }
        catch
        {
            throw;
        }

    }
    //approval-grant-Cancel
    public int UpdateStatusGrantEntryCancel(GrantData j)
    {
        try
        {

            return g.UpdateStatusGrantEntryCancel(j);
        }
        catch
        {
            throw;
        }

    }



    public DataTable fnfindGrantSanKindDetails(string SHNo, string bu)
    {
        try
        {
            return g.fnfindGrantSanKindDetails(SHNo, bu);
        }
        catch
        {
            throw;
        }

    }

    //update grant-pdf path crete-upload

    public int UploadGrnatPathCreate(GrantData p)
    {
        try
        {

            return g.UploadGrnatPathCreate(p);
        }
        catch
        {
            throw;
        }

    }


    public String GetGrantFileUploadPath(string pubid, string entrytype)
    {
        try
        {
            return g.GetGrantFileUploadPath(pubid, entrytype);
        }
        catch
        {
            throw;
        }

    }
    public int SelectCountUploadSanctionInformationType(string pubid, string entrytype)
    {
        try
        {
            return g.SelectCountUploadSanctionInformationType(pubid, entrytype);
        }
        catch
        {
            throw;
        }

    }
    // for sending the value to dataaccesslayer to fetch the value of index
    public string GetIndexValue(string PubId, string Title)
    {
        try
        {

            return J.GetIndexValue(PubId, Title);// sending value to dataaccess layer

        }
        catch
        {
            throw;
        }




    }



    public IndexManage selectIndexAgency1(string name)
    {
        try
        {

            return J.selectIndexAgency1(name);
        }
        catch
        {
            throw;
        }


    }

    public IndexManage selectIndexAgency2(string dep1, string inst1)
    {
        try
        {

            return J.selectIndexAgency2(dep1, inst1);
        }
        catch
        {
            throw;



        }
    }


    public User GetFirstAuthorName(string id, string TypeOfEntry)
    {
        try
        {

            return J.GetFirstAuthorName(id, TypeOfEntry);

        }
        catch
        {
            throw;
        }

    }


    public DataSet getFirstAuthor(string Id, string TypeOfEntry)
    {
        try
        {
            return J.getFirstAuthor(Id, TypeOfEntry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    public DataSet getAuthorCCList(string Id, string TypeOfEntry)
    {
        try
        {
            return J.getAuthorCCList(Id, TypeOfEntry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    public DataSet getAuthorListName1(string Id, string TypeOfEntry)
    {
        try
        {
            return J.getAuthorListName1(Id, TypeOfEntry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public JournalData GetImpactFactor(JournalData jdValueObj)
    {
        try
        {

            return J.GetImpactFactor(jdValueObj);

        }
        catch
        {
            throw;
        }

    }

    //Akshatha
    //Function to check duplicates journal publication entry
    public ArrayList chekDuplicateJournalEntry(PublishData j)
    {
        try
        {
            return J.chekDuplicateJournalEntry(j);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }



    public JournalData GetImpactFactorApplicableYear(JournalData JournalValueObj)
    {
        try
        {
            return J.GetImpactFactorApplicableYear(JournalValueObj);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public PublishData SelectDefaultAuthor(PublishData j)
    {
        try
        {
            return J.SelectDefaultAuthor(j);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    public DataSet BindPublication(PublishData pub)
    {
        try
        {
            return J.BindPublication(pub);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public bool RevertingStatusToNew(PublishData obj)
    {
        try
        {
            return J.RevertingStatusToNew(obj);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public string SelectIsReverFlag(string p, string p_2)
    {
        try
        {
            return J.SelectIsReverFlag(p, p_2);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public bool UploadedPdfPath(PublishData obj)
    {
        try
        {
            return J.UploadedPdfPath(obj);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }




    //Grant Module
    //Function to search grant data
    public GrantData fnfindGrantid(string SHNo, string projectunit)
    {
        try
        {
            return g.fnfindGrantid(SHNo, projectunit);
        }
        catch
        {
            throw;
        }

    }


    public DataTable SelectIncentiveAmountDetailsExists(string id, string unit)
    {
        try
        {
            return g.SelectIncentiveAmountDetailsExists(id, unit);// sending values from Businesslayer to access layer
        }
        catch (Exception ex)
        {
            throw;
        }
    }


    public DataTable SelectSanctionOPAmountDetails(string id, string unit, int p)
    {
        try
        {
            return g.SelectSanctionOPAmountDetails(id, unit, p);// sending values from Businesslayer to access layer
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    //update grantEntry
    public int UpdateGrantEntryPIMove(GrantData j, GrantData[] jd)
    {
        try
        {

            return g.UpdateGrantEntryPIMove(j, jd);
        }
        catch
        {
            throw;
        }

    }

    public DataTable fnfindGrantInvestigatorDetails(string SHNo, string bu)
    {
        try
        {
            return g.fnfindGrantInvestigatorDetails(SHNo, bu);
        }
        catch
        {
            throw;
        }

    }

    public GrantData fnfindGrantidSanctionDetails(string SHNo, string bullec)
    {
        try
        {
            return g.fnfindGrantidSanctionDetails(SHNo, bullec);
        }
        catch
        {
            throw;
        }

    }

    public DataTable SelectSanctionData(string Pid, string projectunit1)
    {

        try
        {
            return g.SelectSanctionData(Pid, projectunit1);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable SelectSanctionOPAmountDetailsExists(string Pid, string projectunit11)
    {

        try
        {
            return g.SelectSanctionOPAmountDetailsExists(Pid, projectunit11);
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public DataTable SelectRecipetDetails(string Pid, string unit)
    {
        try
        {
            return g.SelectRecipetDetails(Pid, unit);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable SelectIncentiveDetails(string Pid, string projectunit11)
    {
        try
        {
            return g.SelectIncentiveDetails(Pid, projectunit11);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public string SelectPIMoveComment(string Pid, string projectunit)
    {

        try
        {
            return g.SelectPIMoveComment(Pid, projectunit);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //insert GrantEntry
    public int insertGrantEntry(GrantData j, GrantData[] jd)
    {
        try
        {

            return g.insertGrantEntry(j, jd);
        }
        catch
        {
            throw;
        }

    }

    public bool UpdateStatusReworkGrantEntry(GrantData grant)
    {
        try
        {
            return g.UpdateStatusReworkGrantEntry(grant);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public int UpdateSanctionDetails(GrantData j, GrantData[] SD3, GrantData[] SD4)
    {
        try
        {
            return g.UpdateSanctionDetails(j, SD3, SD4);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public int InsertRecieptDetails(GrantData j, RecieptData[] JD)
    {
        try
        {
            return g.InsertRecieptDetails(j, JD);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public int InsertIncentiveDetails(GrantData j, IncentiveData[] JD3, IncentiveData[] JD4)
    {
        try
        {
            return g.InsertIncentiveDetails(j, JD3, JD4);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    public int InsertOverheadDetails(GrantData j, GrantData[] JD3)
    {
        try
        {
            return g.InsertOverheadDetails(j, JD3);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public int UpdateFinanceStatus(GrantData j)
    {
        try
        {
            return g.UpdateFinanceStatus(j);
        }
        catch
        {
            throw;
        }
    }

    public int UpdateSanctinedGrantEntry(GrantData j, GrantData[] jd1)
    {
        try
        {
            return g.UpdateSanctinedGrantEntry(j, jd1);
        }
        catch
        {
            throw;
        }
    }


    public DataTable SelectOverheadDetails(string Pid, string projectunit11)
    {
        try
        {
            return g.SelectOverheadDetails(Pid, projectunit11);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    public string GetAgencyName(string p)
    {
        try
        {
            return g.GetAgencyName(p);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }




    public int UpdateStatusGrantEntryCLO(GrantData j)
    {
        try
        {
            return g.UpdateStatusGrantEntryCLO(j);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable SelectIncentiveAmountDetails(string id, string unit, int p)
    {
        try
        {
            return g.SelectIncentiveAmountDetails(id, unit, p);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }



    public GrantData selectExisitingAgency(string p)
    {
        try
        {
            return g.selectExisitingAgency(p);
        }
        catch
        {
            throw;
        }
    }





    public int UpdateAgency(GrantData b)
    {
        try
        {
            return g.UpdateAgency(b);// sending values from Businesslayer to access layer
        }
        catch (Exception ex)
        {
            throw;
        }
    }


    public int SaveAgencyDetails(GrantData b)
    {
        try
        {
            return g.SaveAgencyDetails(b);// sending values from Businesslayer to access layer
        }
        catch (Exception ex)
        {
            throw;
        }
    }




    public DataTable SelectSanctionOPAmountDetails1(string Pid, string projectunit11, int p)
    {

        try
        {
            return g.SelectSanctionOPAmountDetails1(Pid, projectunit11, p);// sending values from Businesslayer to access layer
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public DataTable SelectIncentiveAmountDetails1(string Pid, string projectunit11, int p)
    {
        try
        {
            return g.SelectIncentiveAmountDetails1(Pid, projectunit11, p);// sending values from Businesslayer to access layer
        }
        catch (Exception ex)
        {
            throw;
        }
    }


    //approval-grant-accept
    public int UpdateStatusGrantEntryAcceptApproval(GrantData j, GrantData[] jd, GrantData[] jd1, GrantData[] sd1)
    {
        try
        {

            return g.UpdateStatusGrantEntryAcceptApproval(j, jd, jd1, sd1);
        }
        catch
        {
            throw;
        }

    }

    public int updateAdditionalBU(ArrayList userBU, string username)
    {
        try
        {
            return g.updateAdditionalBU(userBU, username);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //Send Mail
    public DataSet getInvetigatorList(string Id, string Type)
    {
        try
        {
            return g.getInvetigatorList(Id, Type);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataSet getInvietigatorListName(string Id, string Type)
    {
        try
        {
            return g.getInvietigatorListName(Id, Type);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataSet getReserachCoOrdinator(string p, string p_2)
    {
        try
        {
            return g.getReserachCoOrdinator(p, p_2);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataSet getGrantAuthorList(string p, string p_2)
    {
        try
        {
            return g.getGrantAuthorList(p, p_2);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataSet getGrantCOAuthorList(string p, string p_2)
    {
        try
        {
            return g.getGrantCOAuthorList(p, p_2);// sending values from Businesslayer to access layer
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public DataSet getReserachFinanceList()
    {
        try
        {
            return g.getReserachFinanceList();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }



    public DataSet getStudentlist(string p, string p_2)
    {
        try
        {
            return g.getStudentlist(p, p_2);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public int UpdatePublicationData(PublishData j, PublishData[] jd)
    {
        try
        {
            return g.UpdatePublicationData(j, jd);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataSet BindGridview(PublishData obj)
    {
        try
        {
            return J.BindGridview(obj);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //Incentive Point
    public bool checkPredatoryJournal(string p1, string p2)
    {
        try
        {
            return J.checkPredatoryJournal(p1, p2);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public string checkIncentivePointStatus(string Pid, string TypeEntry)
    {
        try
        {
            return J.checkIncentivePointStatus(Pid, TypeEntry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public int DeletePublication(PublishData v)
    {
        try
        {
            return J.DeletePublication(v);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public JournalData JournalYearwiseCheck(JournalData JournalValueObj)
    {
        try
        {
            return J.JournalYearwiseCheck(JournalValueObj);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public int insertPublishEntryRDC(PublishData j, PublishData[] JD, ArrayList listIndexAgency)
    {
        try
        {
            return J.insertPublishEntryRDC(j, JD, listIndexAgency);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public int UpdatePdfPath(PublishData obj)
    {
        try
        {
            return J.UpdatePdfPath(obj);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataSet SelectPublications(PublishData pub_obj)
    {
        try
        {
            return J.SelectPublications(pub_obj);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public int UpdatePublishEntryRDC(PublishData j, PublishData[] JD, ArrayList listIndexAgency)
    {
        try
        {
            return J.UpdatePublishEntryRDC(j, JD, listIndexAgency);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataSet getAuthorListRDC(string p1, string p2)
    {
        try
        {
            return J.getAuthorListRDC(p1, p2);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataSet SelecKeywordBasedAuthors(string keyword)
    {
        try
        {
            return J.SelecKeywordBasedAuthors(keyword);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataSet SelecEmpCodeBasedAuthors(string empid, string orcid, string scopusid)
    {
        try
        {
            return J.SelecEmpCodeBasedAuthors(empid, orcid, scopusid);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    //Ashwini
    public string SelectAuthorCCMailid(string employeeid, string pubid, string typeofentry)
    {
        try
        {
            return J.SelectAuthorCCMailid(employeeid, pubid, typeofentry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public string SelectAuthorToMailid(string pubid, string typeofentry)
    {
        try
        {
            return J.SelectAuthorToMailid(pubid, typeofentry);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataSet BindGridviewFileUpload(PublishData obj)
    {
        try
        {
            return J.BindGridviewFileUpload(obj);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    public DataSet BindGrid(string p1, string p2, string createdby)
    {
        try
        {
            return J.BindGrid(p1, p2, createdby);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public User findusername(string UserId)
    {
        try
        {
            return J.findusername(UserId);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    public GrantData fnGrantData(string pid, string Unit, string user)
    {
        try
        {
            return J.fnGrantData(pid, Unit, user);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public int fnInsertResearchData(FileUpload[] f, int length)
    {
        try
        {
            return J.fnInsertResearchData(f, length);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public int fnUpdateResearchData(string userid, FileUpload f, FileUpload[] JD, User u)
    {
        try
        {
            return J.fnUpdateResearchData(userid, f, JD, u);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //public bool CheckUserId(string userid)
    //{
    //    try
    //    {
    //        return J.UserIdSearch(userid);
    //    }
    //    catch
    //    {
    //        throw;
    //    }
    //}

    public FileUpload DomainSearch(string userid)
    {
        try
        {
            return J.DomainSearch(userid);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }




    public FileUpload CheckEmployeeId(string employeecode)
    {
        try
        {
            return J.CheckEmployeeId(employeecode);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    public int fninsertEditResearchData(string empcode, FileUpload f, FileUpload[] JD, User u)
    {
        try
        {
            return J.fninsertEditResearchData(empcode, f, JD, u);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public int checkExistUserid(string empcode)
    {
        try
        {
            return J.checkExistUserid(empcode);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    public User CheckOrcidScopusid(string userid)
    {
        try
        {
            return J.CheckOrcidScopusid(userid);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataSet SelecDomainandResearchArea(string domain, string area)
    {
        try
        {
            return J.SelecDomainandResearchArea(domain, area);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }



    public User findmembername(string memberid)
    {
        try
        {
            return J.findmembername(memberid);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }



    public ArrayList SelectActiveYear(JournalData JournalValueObj)
    {
        try
        {
            return J.SelectActiveYear(JournalValueObj);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public ArrayList CheckDuplicates(string type)
    {
        try
        {
            return J.CheckDuplicates(type);
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    public int CheckEmailDetails(string p1, string p2)
    {
        try
        {
            return J.CheckEmailDetails(p1, p2);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }



    public GrantData CheckUniqueUTN(string UTN, string projectid, string projectunit)
    {
        try
        {
            return g.CheckUniqueUTN(UTN, projectid, projectunit);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }



    public int UpdateUTN(GrantData j, GrantData[] JD)
    {
        try
        {
            return g.UpdateUTN(j, JD);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }



    public int insertbackupUTN(GrantData j, GrantData[] JD)
    {
        try
        {
            return g.insertbackupUTN(j, JD);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }





    public string selectoldutn(string projectid, string projectunit)
    {
        {
            try
            {
                return g.selectoldutn(projectid, projectunit);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }



    public string getUserMailIdList(string userid)
    {
        try
        {
            return g.getUserMailIdList(userid);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataSet getInvetigatorDETAIL(string p1, string p2)
    {
        try
        {
            return g.getInvetigatorDETAIL(p1,p2);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    public int insertEmailtracker(string p1, EmailDetails details, string p2)
    {
        try
        {
            return g.insertEmailtracker(p1, details, p2);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }



    public GrantData CheckUniqueId(string p1, string p2,EmailDetails details)
    {
        try
        {
            return g.CheckUniqueId(p1, p2, details);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public int updateEmailtracker(string p1, string p2, EmailDetails details, GrantData obj)
    {
        try
        {
            return g.updateEmailtracker(p1, p2, details,obj);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataSet getAuthorDetail(string p1, string p2)
    {
        try
        {
            return J.getAuthorDetail(p1, p2);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public int insertAuthorDetailEmailtracker(string AuthorName, EmailDetails details, string p)
    {
        try
        {
            return J.insertAuthorDetailEmailtracker(AuthorName,details, p);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public JournalData CheckUniqueIdPublication(string p1, string p2, EmailDetails details)
    {
        try
        {
            return J.CheckUniqueIdPublication(p1,p2,details);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    public int updatePublicationEmailtracker(string p1, string p2, EmailDetails details, JournalData obj3)
    {
        try
        {
            return J.updatePublicationEmailtracker(p1, p2, details, obj3);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public int insertEmailtrackerUpEprint(string AuthorName, EmailDetails details, string p)
    {
        try
        {
            return J.insertEmailtrackerUpEprint(AuthorName, details, p);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataSet getAuthorCCListDetail(string p1, string p2)
    {
        try
        {
            return J.getAuthorCCListDetail(p1,p2);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public JournalData CheckUniqueIdUPEprint(string p1, string p2, EmailDetails details)
    {
        try
        {
            return J.CheckUniqueIdUPEprint(p1, p2, details);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public int updateEmailtrackerUpEprint(string p1, string p2, EmailDetails details, JournalData obj3)
    {
        try
        {
            return J.updateEmailtrackerUpEprint(p1, p2, details,obj3);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public int insertEmailtrackerIncentive(string AuthorName, EmailDetails details, string p)
    {
        try
        {
            return J.insertEmailtrackerIncentive(AuthorName, details, p);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }



    public PublishData checkJournalLinkM(string p1, int p2)
    {
        try
        {
            return J.checkJournalLinkM(p1, p2);
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    public PublishData CheckQuartilevaluefromJQM(string p1, string p2)
    {
        try
        {
            return J.CheckQuartilevaluefromJQM(p1, p2);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public PublishData InsertJournalQuartileTracker(string p1, int p2, string p3, string QId, object p4)
    {
        try
        {
            return J.InsertJournalQuartileTracker(p1, p2, p3, QId, p4);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public PublishData checkQuartileValue(string p, string jyear)
    {
        try
        {
            return J.checkQuartileValue(p, jyear);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public int InsertJournalQuartileMap(string ISSN, int p1, string QId, string CreatedBy)
    {
        try
        {
            return J.InsertJournalQuartileMap(ISSN, p1, QId, CreatedBy);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public int insertSeedMoneyEntry(SeedMoney s, SeedMoney[] JD)
    {
        try
        {
            return g.insertSeedMoneyEntry(s, JD);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public SeedMoney fnfindSeedid(string SID)
    {
        try
        {
            return g.fnfindSeedid(SID);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable fnfindseedMoneyInvestigatorDetails(string SID)
    {
        try
        {
            return g.fnfindseedMoneyInvestigatorDetails(SID);
        }
        catch
        {
            throw;
        }
    }

    public int UpdateSeedMoneyEntry(SeedMoney s, SeedMoney[] JD)
    {
        try
        {
            return g.UpdateSeedMoneyEntry(s, JD);
        }
        catch
        {
            throw;
        }
    }

    public int UpdatePfPathCreateSeedMoney(string SID, SeedMoney j)
    {
        try
        {
            return g.UpdatePfPathCreateSeedMoney(SID,j);
        }
        catch
        {
            throw;
        }
    }

    public string GetFileUploadPathSeedMoney(string p)
    {
        try
        {
            return g.GetFileUploadPathSeedMoney(p);
        }
        catch
        {
            throw;
        }
    }

    public bool UpdateStatusReworkSeedMoneyEntry(SeedMoney s)
    {
        try
        {
            return g.UpdateStatusReworkSeedMoneyEntry(s);
        }
        catch
        {
            throw;
        }
    }

    public bool UpdateStatusApproveSeedMoneyEntry(SeedMoney s)
    {
        try
        {
            return g.UpdateStatusApproveSeedMoneyEntry(s);
        }
        catch
        {
            throw;
        }
    }

    public DataSet getInvetigatorListforseed(string p)
    {
        try
        {
            return g.getInvetigatorListforseed(p);
        }
        catch
        {
            throw;
        }
    }

    public DataSet getInvetigatorDETAILforseed(string p)
    {
        try
        {
            return g.getInvetigatorDETAILforseed(p);
        }
        catch
        {
            throw;
        }

    }

    public DataSet getReserachDirector(string p)
    {
        try
        {
            return g.getReserachDirector(p);
        }
        catch
        {
            throw;
        }
    }

    public DataSet getInvietigatorListNameofSeed(string p)
    {

        try
        {
            return g.getInvietigatorListNameofSeed(p);
        }
        catch
        {
            throw;
        }
    }



    public bool UpdateStatusRejectSeedMoneyEntry(SeedMoney s)
    {
        try
        {
            return g.UpdateStatusRejectSeedMoneyEntry(s);
        }
        catch
        {
            throw;
        }
    }

    public string GetStatusName(string p)
    {
        try
        {
            return g.GetStatusName(p);
        }
        catch
        {
            throw;
        }
    }



    public int UpdateStatusApproveSeedMoneyEntryStudent(SeedMoney s, SeedMoney[] JD)
    {
        try
        {
            return g.UpdateStatusApproveSeedMoneyEntryStudent(s,JD);
        }
        catch
        {
            throw;
        }
    }

    public string getentrytype(string p)
    {
        try
        {
            return g.getentrytype(p);
        }
        catch
        {
            throw;
        }
    }

    public int UpdateSeedMoneyEntryNew(SeedMoney s, SeedMoney[] JD)
    {
        try
        {
            return g.UpdateSeedMoneyEntryNew(s,JD);
        }
        catch
        {
            throw;
        }
    }

    public bool UpdateStatusRevisionRequiredSeedMoneyEntry(SeedMoney s)
    {
        try
        {
            return g.UpdateStatusRevisionRequiredSeedMoneyEntry(s);
        }
        catch
        {
            throw;
        }
    }

    public JournalData selectQuartilevalue(string p, int jayear, int jamonth, int Quartilefrommonth, int QuartileTomonth)
    {
        try
        {
            return J.selectQuartilevalue(p, jayear, jamonth, Quartilefrommonth, QuartileTomonth);
        }
        catch
        {
            throw;
        }
    }



    public JournalData selectQuartilevaluefrompublication(string p1, string p2, string p3)
    {
        try
        {
            return J.selectQuartilevaluefrompublication(p1, p2, p3);
        }
        catch
        {
            throw;
        }
    }

    public JournalData selectQuartilevaluefrompublicationEntry(string p1, string p2, string p3)
    {
        try
        {
            return J.selectQuartilevaluefrompublicationEntry(p1, p2, p3);
        }
        catch
        {
            throw;
        }
    }

    public string CheckPrintEvaluationEnableQuartile(string p1, string p2, string p3, string p4)
    {
        try
        {
            return J.CheckPrintEvaluationEnableQuartile(p1, p2, p3, p4);
        }
        catch
        {
            throw;
        }
    }


    public GrantData CheckUniquePID(string PID, string ProjectUnit)
    {
        try
        {
            return g.CheckUniquePID(PID, ProjectUnit);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public int Insertfileuploadprojets(GrantData jd, GrantData jdi)
    {
        try
        {
            return g.Insertfileuploadprojets(jd, jdi);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    public DataSet getReserachDirectorclerk(string p1, string p2)
    {
        try
        {
            return g.getReserachDirectorclerk(p1, p2);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public int UpdateStatusGrantEntry(GrantData j, GrantData[] JD, GrantData[] JD1, GrantData[] SD3)
    {
        try
        {
            return g.UpdateStatusGrantEntry(j, JD, JD1, SD3);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public bool UpdateStatusCancelSeedMoneyEntry(SeedMoney s)
    {
        try
        {
            return g.UpdateStatusCancelSeedMoneyEntry(s);
        }
        catch
        {
            throw;
        }
    }

    public int Updatemailid(string p, User b, int isupdatemailid, string OldmailID)
    {
        try
        {
            return g.Updatemailid(p, b, isupdatemailid, OldmailID);
        }
        catch
        {
            throw;
        }
    }

    public int UpdateGrantEntryProjectdetails(GrantData j, GrantData[] JD)
    {
        try
        {
            return g.UpdateGrantEntryProjectdetails(j, JD);
        }
        catch
        {
            throw;
        }
    }

    public int UpdateSeedMoneyEntryApproved(SeedMoney s, SeedMoney[] JD)
    {
        try
        {
            return g.UpdateSeedMoneyEntryApproved(s,JD);
        }
        catch
        {
            throw;
        }
    }

    public DataTable fnfinddistinctOrganizationforPercentage(string Pid, string projectunit)
    {
        try
        {
            return g.fnfinddistinctOrganizationforPercentage(Pid, projectunit);
        }
        catch
        {
            throw;
        }
    }

    public string getMaheInstitutionName(string p)
    {
        try
        {

            return g.getMaheInstitutionName(p);
        }
        catch
        {
            throw;
        }
    }

    public GrantData getparcentagevalue(string Pid, string projectunit, string p1, string p2)
    {
        try
        {
            return g.getparcentagevalue(Pid, projectunit, p1, p2);
        }
        catch
        {
            throw;
        }
    }

    public GrantData getparcentagevaluefordept(string Pid, string projectunit, string p1, string p2)
    {
        try
        {
            return g.getparcentagevaluefordept(Pid, projectunit, p1, p2);
        }
        catch
        {
            throw;
        }
    }

    public DataTable fnfinddistinctInstituteforPercentage(string Pid, string projectunit)
    {
        try
        {
            return g.fnfinddistinctInstituteforPercentage(Pid, projectunit);
        }
        catch
        {
            throw;
        }
    }

    public int UpdateStatusGrantEntryAcceptApprovalPercentage(GrantData j, GrantData[] jd, GrantData[] jd1, GrantData[] sd1, GrantData[] PO, GrantData[] PI)
    {
        try
        {

            return g.UpdateStatusGrantEntryAcceptApprovalPercentage(j, jd, jd1, sd1, PO, PI);
        }
        catch
        {
            throw;
        }
    }

    public int CheckPercentageSharingDetails(string id, string unit)
    {
        try
        {

            return g.CheckPercentageSharingDetails(id, unit);
        }
        catch
        {
            throw;
        }
    }

    public JournalData ProceedingEntryCheckExistance(JournalData JournalValueObj)
    {
        try
        {

            return J.ProceedingEntryCheckExistance(JournalValueObj);
        }
        catch
        {
            throw;
        }
    }

    public JournalData ProceedingYearwiseCheck(JournalData JournalValueObj)
    {
        try
        {

            return J.ProceedingYearwiseCheck(JournalValueObj);
        }
        catch
        {
            throw;
        }
    }

    public JournalData GetImpactFactorApplicableYearProceeding(JournalData JournalValueObj)
    {
        try
        {

            return J.GetImpactFactorApplicableYearProceeding(JournalValueObj);
        }
        catch
        {
            throw;
        }
    }

    public JournalData ProceedingGetImpactFactorPublishEntry(JournalData JournalValueObj)
    {
        try
        {

            return J.ProceedingGetImpactFactorPublishEntry(JournalValueObj);
        }
        catch
        {
            throw;
        }
    }

    public string fnfindjidgtPname(string Pid, string TypeEntry)
    {
        try
        {

            return J.fnfindjidgtPname(Pid, TypeEntry);
        }
        catch
        {
            throw;
        }
    }

    public string getauthoremailID(string Memberid)
    {
        try
        {

            return J.getauthoremailID(Memberid);
        }
        catch
        {
            throw;
        }
    }

    public User getAuthorAffiliationdetails(string empid, string EmailID)
    {
        try
        {

            return u.getAuthorAffiliationdetails(empid, EmailID);
        }
        catch
        {
            throw;
        }
    }

    public User getAuthorInstitutiondetails(string empid, string EmailID)
    {
        try
        {

            return u.getAuthorInstitutiondetails(empid, EmailID);
        }
        catch
        {
            throw;
        }
    }

    public User getAuthorAffiliationdetailsforMAHE(string empid, string EmailID)
    {
        try
        {

            return u.getAuthorAffiliationdetailsforMAHE(empid, EmailID);
        }
        catch
        {
            throw;
        }
    }

    public User getAuthorAffiliationIDType(string InstituteId )
    {
        try
        {

            return u.getAuthorAffiliationIDType(InstituteId);
        }
        catch
        {
            throw;
        }
    }

    public User getAuthorCenterAffiliationdetails(int CenterCode)
    {
        try
        {

            return u.getAuthorCenterAffiliationdetails(CenterCode);
        }
        catch
        {
            throw;
        }
    }

    public User getAuthorAffiliationIDTypeMAHE(string dept, string empid)
    {
        try
        {

            return u.getAuthorAffiliationIDTypeMAHE(dept, empid);
        }
        catch
        {
            throw;
        }
    }

    public User getStudentInstitutiondetails(string empid, string EmailID)
    {
        try
        {

            return u.getStudentInstitutiondetails(empid, EmailID);
        }
        catch
        {
            throw;
        }
    }

    public User getStudentAffiliationdetails(string empid, string EmailID)
    {
        try
        {

            return u.getStudentAffiliationdetails(empid, EmailID);
        }
        catch
        {
            throw;
        }
    }

    public User getAuthorOnlyCenterAffiliationdetails(int CenterCode)
    {
        try
        {

            return u.getAuthorOnlyCenterAffiliationdetails(CenterCode);
        }
        catch
        {
            throw;
        }
    }

    public int InsertSeedMoneyBudget(SeedMoney a)
    {
        try
        {

            return J.InsertSeedMoneyBudget(a);
        }
        catch
        {
            throw;
        } 
    }

    public int getSeedMoneyBudgetExist(SeedMoney a)
    {
        try
        {

            return J.getSeedMoneyBudgetExist(a);
        }
        catch
        {
            throw;
        }
    }

    public int CheckSeedMoneyEntry(string p)
    {
        try
        {

            return J.CheckSeedMoneyEntry(p);
        }
        catch
        {
            throw;
        } 
    }

    public int checkexistingSeedmoneyentry(string type)
    {
        try
        {

            return J.checkexistingSeedmoneyentry(type);
        }
        catch
        {
            throw;
        }
    }

    public int UpdateManageSeedMoneyEntry(SeedMoney a, ArrayList listFaculty)
    {
        try
        {

            return J.UpdateManageSeedMoneyEntry(a, listFaculty);
        }
        catch
        {
            throw;
        }
    }

    public int EnableSeedMoneyEntry(SeedMoney a, ArrayList listFaculty)
    {
        try
        {

            return J.EnableSeedMoneyEntry(a, listFaculty);
        }
        catch
        {
            throw;
        }
    }

    public ArrayList checkexistingCycle(string Fromdate1, string ToDate1, string p)
    {
        try
        {

            return J.checkexistingCycle(Fromdate1, ToDate1, p);
        }
        catch
        {
            throw;
        }
    }

    public int disableSeedMoneyEntry(string p)
    {
        try
        {

            return J.disableSeedMoneyEntry(p);
        }
        catch
        {
            throw;
        }
    }

    public SeedMoney getSeedMoneyActiveStatus(string EntryType, string p)
    {
        try
        {

            return J.getSeedMoneyActiveStatus(EntryType,p);
        }
        catch
        {
            throw;
        }
    }



    public string selectMemberType(string empcode, string referenceid)
    {
        try
        {

            return J.selectMemberType(empcode, referenceid);
        }
        catch
        {
            throw;
        }
    }

    public DataTable SelectProjectOutcomeDetails(string Pid, string projectunit)
    {
        try
        {

            return g.SelectProjectOutcomeDetails(Pid, projectunit);
        }
        catch
        {
            throw;
        }
    }

    public int InsertProjectOutcomeDetails(RecieptData[] JDP, GrantData journalbank)
    {
        try
        {

            return g.InsertProjectOutcomeDetails(JDP, journalbank);
        }
        catch
        {
            throw;
        }
    }

    public string FindMemberIdinFeedBackTracker(string MemberID, string Type, string ID)
    {
        try
        {

            return g.FindMemberIdinFeedBackTracker(MemberID, Type, ID);
        }
        catch
        {
            throw;
        }
    }

    public FeedbackClass CheckUserforFeedback(string MemberID, string Type)
    {
        try
        {

            return g.CheckUserforFeedback(MemberID, Type);
        }
        catch
        {
            throw;
        }
    }

    public int gettotalmonths(DateTime fromdate, DateTime todaydate)
    {
        try
        {
            return J.gettotalmonths(fromdate, todaydate);
        }
        catch
        {
            throw;
        }
    }

    public bool IsExeFile(byte[] FileU)
    {
        try
        {
            return J.IsExeFile(FileU);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public int GetImpactFactorDetails(JournalData JournalValueObj)
    {
        try
        {
            return J.GetImpactFactorDetails(JournalValueObj);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataTable SelectImpactFactorDetails(JournalData JournalValueObj)
    {
        try
        {
            return J.SelectImpactFactorDetails(JournalValueObj);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public int JournalEntrySaveImpactfactorChanges(JournalData JournalValueObj, ArrayList list, JournalData[] JDP)
    {
        try
        {
            return J.JournalEntrySaveImpactfactorChanges(JournalValueObj, list, JDP);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}