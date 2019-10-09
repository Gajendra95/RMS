using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for PatentBusiness
/// </summary>
public class PatentBusiness
{
    Patent_DAobject pat_Obj = new Patent_DAobject();


    public bool InsertPatent(Patent pat, GrantData[] jd)
    {
        try
        {

            return pat_Obj.InsertPatent(pat, jd);
        }
        catch
        {
            throw;
        }
    }
    public bool UpdatePatent(Patent pat, GrantData[] jd)
    {
        try
        {
            return pat_Obj.UpdatePatent(pat, jd);
        }
        catch
        {
            throw;
        };
    }
    public Patent SelectPatent(string ID)
    {
        try
        {
            return pat_Obj.SelectPatent(ID);
        }
        catch
        {
            throw;
        }

    }
    public DataTable fnPatentInventorDetails(string ID)
    {
        try
        {
            return pat_Obj.fnPatentInventorDetails(ID);
        }
        catch
        {
            throw;
        }


    }

    public bool UpdateStatusPatentRejectApproval(Patent pat, GrantData[] JD)
    {
        try
        {

            return pat_Obj.UpdateStatusPatentRejectApproval(pat, JD);
        }
        catch
        {
            throw;
        }
    }

    public bool InsertApplicationStage(Patent pat)
    {
        try
        {

            return pat_Obj.InsertApplicationStage(pat);
        }
        catch
        {
            throw;
        }
    }

    public bool UpdateGrantPatent(Patent pat, GrantData[] JD)
    {
        try
        {
            return pat_Obj.UpdateGrantPatent(pat, JD);
        }
        catch
        {
            throw;
        };
    }

    public bool InsertRenwalaDetails(Patent pat)
    {
        try
        {
            return pat_Obj.InsertRenwalaDetails(pat);
        }
        catch
        {
            throw;
        };
    }

    public int UpdatePatentCancelStatus(Patent j)
    {
        try
        {
            return pat_Obj.UpdatePatentCancelStatus(j);
        }
        catch
        {
            throw;
        };
    }



    public Patent fnfindInventor(string PatentID)
    {
        try
        {
            return pat_Obj.fnfindInventor(PatentID);
        }
        catch
        {
            throw;
        };
    }

    public Patent PatentInvetor(string PatentID)
    {
        try
        {
            return pat_Obj.fnfindInventor(PatentID);
        }
        catch
        {
            throw;
        };
    }

    public DataSet fnfindPatentAccount12(string PatentID)
    {
        try
        {
            return pat_Obj.fnfindPatentAccount12(PatentID);
        }
        catch
        {
            throw;
        };
    }

    // public Patent get_Inventor_Details(string PatentID)
    // {
    //try
    //{
    //    return pat_Obj.get_Inventor_Details(PatentID);
    //}
    //catch
    //{
    //    throw;
    //};
    // }

    public Patent Get_CreatedBy(string PatentID)
    {
        try
        {
            return pat_Obj.Get_CreatedBy(PatentID);
        }
        catch
        {
            throw;
        };
    }

    public Patent Get_CreatedName(string PatentID1)
    {
        try
        {
            return pat_Obj.Get_CreatedName(PatentID1);
        }
        catch
        {
            throw;
        };
    }

    public Patent getPatent_Author_Details(string PatentID1)
    {
        try
        {
            return pat_Obj.getPatent_Author_Details(PatentID1);
        }
        catch
        {
            throw;
        };
    }

    public string CheckIsStudentPublication(string PatentID)
    {
        try
        {
            return pat_Obj.CheckIsStudentPublication(PatentID);
        }
        catch
        {
            throw;
        };
    }

    public DataSet SelectStudentAuthorDetail(string PatentID)
    {
        try
        {
            return pat_Obj.SelectStudentAuthorDetail(PatentID);
        }
        catch
        {
            throw;
        };
    }

    public string deleteid(string id, string status, string entryno)
    {
        try
        {
            return pat_Obj.deleteid(id, status, entryno);
        }
        catch
        {
            throw;
        };
    }

    public Patent SelectRenewalYear(string ID)
    {
        try
        {
            return pat_Obj.SelectRenewalYear(ID);
        }
        catch
        {
            throw;
        };
    }



  

    public Patent SelectRenewalDate(string ID)
    {
        try
        {
            return pat_Obj.SelectRenewalDate(ID);
        }
        catch
        {
            throw;
        };
    }

    public int UpdatePatentStatus(Patent p)
    {
        try
        {
            return pat_Obj.UpdatePatentStatus(p);
        }
        catch
        {
            throw;
        };
    }

    public bool UpdateStatus(Patent pat)
    {
        try
        {
            return pat_Obj.UpdateStatus(pat);
        }
        catch
        {
            throw;
        };
    }

    public DataSet SelectPatentDetails(string id, string title,int role, string UserId)
    {
        try
        {
            return pat_Obj.SelectPatentDetails(id, title, role, UserId);
        }
        catch
        {
            throw;
        };
    }

    public DataSet getPatentAuthorList(string p)
    {
        try
        {
            return pat_Obj.getPatentAuthorList(p);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataSet getPatentAuthorListName(string p)
    {
        try
        {
            return pat_Obj.getPatentAuthorListName(p);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public string GetRDCEmail()
    {
        try
        {
            return pat_Obj.GetRDCEmail();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataSet SelectProTocompletelist(string p)
    {
        try
        {
            return pat_Obj.SelectProTocompletelist(p);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public DataSet SelectGRNtoRenewallist(string p)
    {
        try
        {
            return pat_Obj.SelectGRNtoRenewallist(p);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public string CheckEntryStatus(string p)
    {
        try
        {
            return pat_Obj.CheckEntryStatus(p);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public int UploadPatentPathCreate(Patent pat)
    {
        try
        {

            return pat_Obj.UploadPatentPathCreate(pat);
        }
        catch
        {
            throw;
        }
    }

    public string GetPatentFileUploadPath(string p)
    {
        try
        {
            return pat_Obj.GetPatentFileUploadPath(p);
        }
        catch
        {
            throw;
        }

    }

    public int UpdatePatentattachedEntry(Patent j)
    {
        try
        {

            return pat_Obj.UpdatePatentattachedEntry(j);
        }
        catch
        {
            throw;
        }
    }

    public int SelectCountUploadgrantInformationType(string p1, string p2)
    {
        try
        {
            return pat_Obj.SelectCountUploadgrantInformationType(p1, p2);
        }
        catch
        {
            throw;
        }
    }



    public Patent fnfindpatent(string Pid)
    {
        try
        {
            return pat_Obj.fnfindjid(Pid);
        }
        catch
        {
            throw;
        }
    }

    public DataTable GetPatentWisePoints(string MemberId)
    {
        try
        {
            return pat_Obj.GetPatentWisePoints(MemberId);
        }
        catch
        {
            throw;
        }
    }

    public Patent SelectPatentData(string memberid, string p)
    {
        try
        {
            return pat_Obj.SelectPatentData(memberid, p);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }





    public IncentivePoint SelectPatentMemberCurBalance(string memberid)
    {
        try
        {
            return pat_Obj.SelectPatentMemberCurBalance(memberid);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}