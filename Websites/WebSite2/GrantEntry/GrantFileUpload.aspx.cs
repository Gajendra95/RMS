using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using System.IO;
using System.Net;
using AjaxControlToolkit;

public partial class GrantEntry_GrantFileUpload : System.Web.UI.Page
{
   private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    public int validExcel = 1;
    public double amountfrgn = 0;
    public double amountrs = 0;
    int CheckRowsSkipped = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        string EnableProjectUpload = ConfigurationManager.AppSettings["EnableProjectUpload"].ToString();
        if (EnableProjectUpload == "N")
        {
        panel3.Enabled = false;
        F_Upload.Enabled = false;
        lblNote.Visible = true;
        }
        else
        {
             panel3.Enabled = true;
             F_Upload.Enabled = true;
             lblNote.Visible=false;
        }
    }
    protected void upload_Click(object sender, EventArgs e)
    {
        if (!Page.IsValid)
        {
            return;
        }

        log.Debug("Inside Ledger upload_Click page");

        try
        {



            bindGridview(sender, e);

            if (ConfigurationManager.AppSettings["ProjectUploadLimit"].ToString() != "")
            {
                int uploadlimit = Convert.ToInt16(ConfigurationManager.AppSettings["ProjectUploadLimit"]);
                if (GridExcelDataProject.Rows.Count > uploadlimit)
                {
                    Response.Write("<script language='javascript'>alert('Only  " + uploadlimit + " records can be uploaded at a time')</script>");
                    return;
                }
            }
            string PID = null;
            string ProjectUnit = null;
            int result = 0;

            string uniqueid = null;
            string uniqueutnid = null;
            Business b = new Business();
            //   GridExcelDataProject.Visible = false;
            //int validExcel =1;
            //      GridExcelDataInvestigator.Visible = false;
            //int validExcel = 1;
            int CheckRowsSkipped = 0;
            int rowindex = 0;
            int rowindex1 = 0;

            if (GridExcelDataProject.Visible == false && validExcel == 1)
            {
                GrantData[] jd = new GrantData[GridExcelDataProject.Rows.Count-1];

                for (int i= 1;i<=GridExcelDataProject.Rows.Count-1;i++)
                {
                    if (i == 1)
                    {
                        rowindex = 0;
                    }
                    else
                    {
                        rowindex++;
                    }
                    //foreach (GridViewRow gr in GridExcelDataProject.Rows)
                    //{
                    GrantData v = new GrantData();
                    GrantData pd1 = new GrantData();
                    Business B = new Business();
                    GrantData obj = new GrantData();
                     ProjectUnit = GridExcelDataProject.Rows[i].Cells[1].Text.Trim();
                    
                    if (ProjectUnit == "" || ProjectUnit == null || ProjectUnit== "&nbsp;")
                    {
                        Response.Write("<script language='javascript'>alert('ProjectUnit value is empty in Row No "+ i.ToString() + " Please update the value and reupload it')</script>");
                        return;
                    }
                     PID = GridExcelDataProject.Rows[i].Cells[2].Text.Trim();
                    if (PID == "" || PID == null || PID=="&nbsp;")
                    {
                        Response.Write("<script language='javascript'>alert('ProjectID value is empty: in Row No " + i.ToString() + " Please update the value and reupload it')</script>");

                        return;
                    }
                    string UTN = Convert.ToString(GridExcelDataProject.Rows[i].Cells[3].Text.Trim());
                    if (UTN != "")
                    {
                        obj = B.CheckUniqueUTN(UTN, PID, ProjectUnit);
                        if (obj.UTN == UTN.Trim())
                        {
                            if (uniqueutnid == null)
                            {
                                uniqueutnid = obj.UTN;
                            }
                            else
                            {
                                uniqueutnid = uniqueutnid + ',' + obj.UTN;

                            }
                            if (obj.GID != ProjectUnit + PID)
                            {
                                // ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Entered UTN already exists in the system for the Project " + ProjectUnit + " _ " + PID + " ')</script>");
                                //return;
                                // CheckRowsSkipped++;
                                //continue;
                                Response.Write("<script language='javascript'>alert('Entered UTN already exists in the system : Row No " + i.ToString() + " Please update the value and reupload it')</script>");
                                return;
                            }
                        }
                    }
                    string ProjectType = GridExcelDataProject.Rows[i].Cells[4].Text.Trim();
                    if (ProjectType == "" || ProjectType == null || ProjectType == "&nbsp;")
                    {
                        Response.Write("<script language='javascript'>alert('ProjectType value is empty: in Row No " + i.ToString() + " Please update the value and reupload it')</script>");
                        return;
                    }
                    string AppliedDate = (GridExcelDataProject.Rows[i].Cells[5].Text.Trim());
                    if (AppliedDate == "" || AppliedDate == null || AppliedDate == "&nbsp;")
                    {
                        Response.Write("<script language='javascript'>alert('AppliedDate  is empty: in Row No " + i.ToString() + " Please update the value and reupload it')</script>");
                        return;
                    }
                    string AppliedAmount = (GridExcelDataProject.Rows[i].Cells[6].Text.Trim());
                    string Contact_No = (GridExcelDataProject.Rows[i].Cells[7].Text.Trim());
                    string SourceProject = (GridExcelDataProject.Rows[i].Cells[8].Text.Trim());
                    if (SourceProject == "" || SourceProject == null || SourceProject == "&nbsp;")
                    {
                        Response.Write("<script language='javascript'>alert('SourceProject  is empty: in Row No " + i.ToString() + " Please update the value and reupload it')</script>");
                        return;
                    }
                    string DurationOfProject = (GridExcelDataProject.Rows[i].Cells[9].Text.Trim());
                    string ERFRealated = (GridExcelDataProject.Rows[i].Cells[10].Text.Trim());
                    string Title = HttpUtility.HtmlDecode(GridExcelDataProject.Rows[i].Cells[11].Text.Trim());
                    if (Title == "" || Title == null || Title == "&nbsp;") 
                    {
                        Response.Write("<script language='javascript'>alert('Title value is empty: in Row No " + i.ToString() + " Please update the value and reupload it')</script>");
                        return;
                    }
                    string Description = HttpUtility.HtmlDecode(GridExcelDataProject.Rows[i].Cells[12].Text.Trim());
                    string Comments = HttpUtility.HtmlDecode(GridExcelDataProject.Rows[i].Cells[13].Text.Trim());
                    string ProjectStatus = (GridExcelDataProject.Rows[i].Cells[14].Text.Trim());
                    string RevisedAppliedAmount = (GridExcelDataProject.Rows[i].Cells[15].Text.Trim());
                    string SanctionOrderDate = (GridExcelDataProject.Rows[i].Cells[16].Text.Trim());
                    if (SanctionOrderDate == "" || SanctionOrderDate == null || SanctionOrderDate == "&nbsp;")
                    {
                        Response.Write("<script language='javascript'>alert('SanctionOrderDate  is empty: in Row No " + i.ToString() + " Please update the value and reupload it')</script>");
                        return;
                    }
                    string SanctionType = (GridExcelDataProject.Rows[i].Cells[17].Text.Trim());
                    if (SanctionType == "" || SanctionType == null || SanctionType == "&nbsp;")
                    {
                        Response.Write("<script language='javascript'>alert('SanctionType  is empty: in Row No " + i.ToString() + " Please update the value and reupload it')</script>");
                        return;
                    }
                    string FundingAgency = (GridExcelDataProject.Rows[i].Cells[18].Text.Trim());
                    if (FundingAgency == "" || FundingAgency == null || FundingAgency == "&nbsp;")
                    {
                        Response.Write("<script language='javascript'>alert('FundingAgency  is empty: in Row No " + i.ToString() + " Please update the value and reupload it')</script>");
                        return;
                    }


                    string AgencyAddress = HttpUtility.HtmlDecode(GridExcelDataProject.Rows[i].Cells[19].Text.Trim());
                    string AgencyContact = (GridExcelDataProject.Rows[i].Cells[20].Text.Trim());
                    string AgencyEmailId = (GridExcelDataProject.Rows[i].Cells[21].Text.Trim());
                    string AgencyPanNo = (GridExcelDataProject.Rows[i].Cells[22].Text.Trim());
                    string FundingSectorLevel = (GridExcelDataProject.Rows[i].Cells[23].Text.Trim());
                    string TypeofAgency = (GridExcelDataProject.Rows[i].Cells[24].Text.Trim());
                    string State = (GridExcelDataProject.Rows[i].Cells[25].Text.Trim());
                    string Country = (GridExcelDataProject.Rows[i].Cells[26].Text.Trim());

                    string UID = ProjectUnit + PID;

                    if (PID != "")
                    {
                        obj = b.CheckUniquePID(PID, ProjectUnit);
                        if (obj.PID == UID)
                        {
                            if (uniqueid == null)
                            {
                                uniqueid = obj.PID;
                            }
                            else
                            {
                                uniqueid = uniqueid + ',' + obj.PID;

                            }
                            //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Entered Project " + uniqueid + " already exists in the system.Please correct the ID')</script>");
                            //return;
                            CheckRowsSkipped++;
                            continue;


                        }
                    }
                   
                   

                  
                   
                  
                   
                 
                    //string PIInstitutionID = HttpUtility.HtmlDecode(GridExcelDataProject.Rows[i].Cells[11].Text.Trim());
                    //string PIDeptID = HttpUtility.HtmlDecode(GridExcelDataProject.Rows[i].Cells[12].Text.Trim());
                   
                  
                    
                  
                    //string InstitutionID = HttpUtility.HtmlDecode(GridExcelDataProject.Rows[i].Cells[16].Text.Trim());
                    //string DeptID = HttpUtility.HtmlDecode(GridExcelDataProject.Rows[i].Cells[17].Text.Trim());




                    //string ProjectCommencementDate = (GridExcelDataProject.Rows[i].Cells[26].Text.Trim());
                    //string ProjectCloseDate = (GridExcelDataProject.Rows[i].Cells[27].Text.Trim());
                    //string ExtendedDate = (GridExcelDataProject.Rows[i].Cells[28].Text.Trim());
                    //string AuditRequired = (GridExcelDataProject.Rows[i].Cells[29].Text.Trim());
                    //string ServiceTaxAppl = (GridExcelDataProject.Rows[i].Cells[30].Text.Trim());
                    //string InstitutionShare = (GridExcelDataProject.Rows[i].Cells[31].Text.Trim());
                    //string AccountHead = (GridExcelDataProject.Rows[i].Cells[32].Text.Trim());
                   
                  

                 

                    jd[rowindex] = new GrantData();

                    jd[rowindex].ProjectUnit = ProjectUnit;
                    Session["ProjectUnit"] = ProjectUnit;
                    jd[rowindex].PID = PID;
                    Session["PID"] = PID;
                    Session["IDP"] = jd[rowindex].ID;
                    jd[rowindex].UTN = UTN;
                    jd[rowindex].ProjectType = ProjectType;
                    if (AppliedDate != "&nbsp;")
                    {
                        jd[rowindex].AppliedDate = Convert.ToDateTime(AppliedDate);
                    }
                    jd[rowindex].AppliedAmount = AppliedAmount;
                    jd[rowindex].Contact_No = Contact_No;
                    jd[rowindex].SourceProject = SourceProject;
                    if (DurationOfProject != "&nbsp;")
                    {
                        jd[rowindex].DurationOfProject = Convert.ToInt32(DurationOfProject);
                    }
                    jd[rowindex].ERFRealated = ERFRealated;
                    jd[rowindex].Title = Title;
                    jd[rowindex].Description = Description;
                    jd[rowindex].Comments = Comments;
                    jd[rowindex].ProjectStatus = ProjectStatus;
                    jd[rowindex].RevisedAppliedAmount = RevisedAppliedAmount;
                    if (SanctionOrderDate != "&nbsp;")
                    {
                        jd[rowindex].SanctionOrderDate = Convert.ToDateTime(SanctionOrderDate);
                    }
                    jd[rowindex].SanctionType = SanctionType;
                    jd[rowindex].FundingAgency = FundingAgency;
                    jd[rowindex].AgencyAddress = AgencyAddress;
                    jd[rowindex].AgencyContact = AgencyContact;
                    jd[rowindex].AgencyEmailId = AgencyEmailId;
                    jd[rowindex].AgencyPanNo = AgencyPanNo;
                    jd[rowindex].FundingSectorLevel = FundingSectorLevel;
                    jd[rowindex].TypeofAgency = TypeofAgency;
                    jd[rowindex].State = State;
                    jd[rowindex].Country = Country;


                    
                    //jd[rowindex].PIInstitutionID = PIInstitutionID;
                    //jd[rowindex].PIDeptID = PIDeptID;
                    //jd[rowindex].InstitutionID = InstitutionID;
                    //jd[rowindex].DeptID = DeptID;
                    //if (ProjectCommencementDate != "&nbsp;")
                    //{
                    //    jd[rowindex].ProjectCommencementDate = Convert.ToDateTime(ProjectCommencementDate);
                    //}
                    //if (ProjectCloseDate != "&nbsp;")
                    //{
                    //    jd[rowindex].ProjectCloseDate = Convert.ToDateTime(ProjectCloseDate);
                    //}
                    //if (ExtendedDate != "&nbsp;")
                    //{
                    //    jd[rowindex].ExtendedDate = Convert.ToDateTime(ExtendedDate);
                    //}

                    //jd[rowindex].AuditRequired = AuditRequired;
                    //jd[rowindex].ServiceTaxAppl = ServiceTaxAppl;
                    //jd[rowindex].InstitutionShare = InstitutionShare;
                    //jd[rowindex].AccountHead = AccountHead;

                    if (GridExcelDataInvestigator.Visible == false && validExcel == 1)
                    {
                        GrantData[] jdi = new GrantData[GridExcelDataInvestigator.Rows.Count - 1];
                        int isleadPI = 0;
                        int isleadPIS = 0;
                        int isleadPIF = 0;
                        int Investigatortype = 0;
                        int InvestigatortypeF = 0;
                        int InvestigatortypeS = 0;
                        for (int j = 1; j <= GridExcelDataInvestigator.Rows.Count - 1; j++)
                        {
                            if (j == 1)
                            {
                                rowindex1 = 0;
                            }
                            else
                            {
                                rowindex1++;
                            }
                            string ProjectUnit1 = GridExcelDataInvestigator.Rows[j].Cells[1].Text.Trim();
                            string ID1 = GridExcelDataInvestigator.Rows[j].Cells[2].Text.Trim();
                            //jdi[j] = new GrantData();
                            jdi[rowindex1] = new GrantData();
                            jdi[rowindex1].ProjectUnit = ProjectUnit1;
                            jdi[rowindex1].PID = ID1;
                            if (jd[rowindex].ProjectUnit == jdi[rowindex1].ProjectUnit && jd[rowindex].PID == jdi[rowindex1].PID)
                            {
                            string InvestigatorName = GridExcelDataInvestigator.Rows[j].Cells[4].Text.Trim();
                            if (InvestigatorName == "" || InvestigatorName == null || InvestigatorName == "&nbsp;")
                            {
                                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please enter Investigator Name for the Project ID " + ID1 + " and ProjectUnit " + ProjectUnit1 + "')</script>");
                                return;

                            }
                            string MUNonMU = (GridExcelDataInvestigator.Rows[j].Cells[5].Text.Trim());
                            string EmployeeCode = (GridExcelDataInvestigator.Rows[j].Cells[6].Text.ToString());
                            string Institution = HttpUtility.HtmlDecode(GridExcelDataInvestigator.Rows[j].Cells[7].Text.Trim());
                            string Department = HttpUtility.HtmlDecode(GridExcelDataInvestigator.Rows[j].Cells[8].Text.Trim());
                            string EmailId = (GridExcelDataInvestigator.Rows[j].Cells[9].Text.Trim());
                            string InvestigatorType = (GridExcelDataInvestigator.Rows[j].Cells[10].Text.Trim());
                            if (InvestigatorType == "" || InvestigatorType == null || InvestigatorType == "&nbsp;")
                            {
                                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please select Investigator Type for the Project ID " + ID1 + " and ProjectUnit " + ProjectUnit1 + "')</script>");
                                return;

                            }
                            if (InvestigatorType == "P")
                            {
                                Investigatortype++;

                            }
                            string InstitutionName = HttpUtility.HtmlDecode(GridExcelDataInvestigator.Rows[j].Cells[11].Text.Trim());
                            string DepartmentName = HttpUtility.HtmlDecode(GridExcelDataInvestigator.Rows[j].Cells[12].Text.Trim());
                            string isLeadPI = (GridExcelDataInvestigator.Rows[j].Cells[15].Text.Trim());
                            if (isLeadPI == "Y")
                            {
                                isleadPI++;
                            }
                           
                            if (MUNonMU == "M" || MUNonMU == "N")
                            {
                                if (Institution == "" || Institution == null || Institution == "&nbsp;")
                                {
                                    ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please enter InstitutionID for the Project ID " + ID1 + " and ProjectUnit " + ProjectUnit1 + "')</script>");
                                    return;

                                }
                                if (Department == "" || Department == null || Department == "&nbsp;")
                                {
                                    ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please enter DepartmentID for the Project ID " + ID1 + " and ProjectUnit " + ProjectUnit1 + "')</script>");
                                    return;

                                }

                                if (InstitutionName == "" || InstitutionName == null || InstitutionName == "&nbsp;")
                                {
                                    ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please enter Institution Name for the Project ID " + ID1 + " and ProjectUnit " + ProjectUnit1 + "')</script>");
                                    return;

                                }

                                if (DepartmentName == "" || DepartmentName == null || DepartmentName == "&nbsp;")
                                {
                                    ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please enter Department Name for the Project ID " + ID1 + " and ProjectUnit " + ProjectUnit1 + "')</script>");
                                    return;

                                }
                            }
                            if (MUNonMU == "M" || MUNonMU == "S")
                            {
                                if (EmailId == "" || EmailId == null || EmailId == "&nbsp;")
                                {
                                    ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please enter MailId for the Project ID " + ID1 + " and ProjectUnit " + ProjectUnit1 + "')</script>");
                                    return;

                                }
                            }
                            if (isLeadPI == "Y")
                            {
                                if (MUNonMU == "S")
                                {
                                    isleadPIS++;
                                }
                                else if (MUNonMU == "M")
                                {
                                    isleadPIF++;
                                }
                            }
                            if (InvestigatorType == "P")
                            {
                                if (MUNonMU == "S")
                                {
                                    InvestigatortypeS++;

                                }
                                else if (MUNonMU == "M")
                                {
                                    InvestigatortypeF++;

                                }
                               
                            }
                            }
                            
                        }
                        if (Investigatortype == 0)
                        {
                            ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Select atleast one Investigator Type as Primary Investigator for the Project ID " + jd[rowindex].PID + " and ProjectUnit" + jd[rowindex].ProjectUnit + "')</script>");
                            return;
                        }
                        if (isleadPI == 0)
                        {
                            ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Select atleast one Lead PI for the Project ID " + jd[rowindex].PID + " and ProjectUnit" + jd[rowindex].ProjectUnit + "')</script>");
                            return;
                        }
                        if (isleadPI > 1)
                        {
                            ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Lead PI cannot be more than one for the Project ID " + jd[rowindex].PID + " and ProjectUnit" + jd[rowindex].ProjectUnit + "')</script>");
                            return;
                        }
                        if (jd[rowindex].ProjectType == "GS")
                        {
                            if (InvestigatortypeS == 0)
                            {
                                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Select atleast one Primary Investigator  as Student!')</script>");
                                return;
                            }
                            if (isleadPIS == 0)
                            {
                                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Select atleast one Lead PI as Student!')</script>");
                                return;

                            }
                           
                        }
                        else if (jd[rowindex].ProjectType == "SG")
                        {
                            if (InvestigatortypeF == 0)
                            {
                                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Select atleast one Primary Investigator  as Faculty!')</script>");
                                return;
                            }
                            if (isleadPIF == 0)
                            {
                                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Select atleast one Lead PI as Faculty!')</script>");
                                return;

                            }
                            
                        }
                    }


                    if (GridExcelDataInvestigator.Visible == false && validExcel == 1)
                    {
                        GrantData[] jdi = new GrantData[GridExcelDataInvestigator.Rows.Count-1];

                        for (int j = 1; j <= GridExcelDataInvestigator.Rows.Count - 1; j++)
                        {
                            if (j == 1)
                            {
                                rowindex1 = 0;
                            }
                            else
                            {
                                rowindex1++;
                            }
                            string ProjectUnit1 = GridExcelDataInvestigator.Rows[j].Cells[1].Text.Trim();
                            string ID1 = GridExcelDataInvestigator.Rows[j].Cells[2].Text.Trim();

                            string EntryNo = GridExcelDataInvestigator.Rows[j].Cells[3].Text.Trim();
                            string InvestigatorName = GridExcelDataInvestigator.Rows[j].Cells[4].Text.Trim();
                            string MUNonMU = (GridExcelDataInvestigator.Rows[j].Cells[5].Text.Trim());
                            //string EmployeeCode = (GridExcelDataInvestigator.Rows[j].Cells[6].Text.Trim());
                            string EmployeeCode = (GridExcelDataInvestigator.Rows[j].Cells[6].Text.ToString());
                            if (EmployeeCode == "" || EmployeeCode == null || EmployeeCode == "&nbsp;")
                            {
                                EmployeeCode = "";
                            }
                            else
                            {
                                EmployeeCode = (GridExcelDataInvestigator.Rows[j].Cells[6].Text.Trim());
                            }
                            //if (MUNonMU == "M")
                            //{
                            //    if (EmployeeCode == "")
                            //    {
                            //        Response.Write("<script language='javascript'>alert('EmployeeCode does not exist for the Project" + ProjectUnit1 + "-" + ID1 + "')</script>");
                            //        //return;
                            //        CheckRowsSkipped++;
                            //        continue;
                            //    }
                            //}

                            string Institution = HttpUtility.HtmlDecode(GridExcelDataInvestigator.Rows[j].Cells[7].Text.Trim());
                            string Department = HttpUtility.HtmlDecode(GridExcelDataInvestigator.Rows[j].Cells[8].Text.Trim());
                            string EmailId = (GridExcelDataInvestigator.Rows[j].Cells[9].Text.Trim());
                            string InvestigatorType = (GridExcelDataInvestigator.Rows[j].Cells[10].Text.Trim());
                            string InstitutionName =HttpUtility.HtmlDecode(GridExcelDataInvestigator.Rows[j].Cells[11].Text.Trim());
                            string DepartmentName = HttpUtility.HtmlDecode(GridExcelDataInvestigator.Rows[j].Cells[12].Text.Trim());
                            string NationalInternational = (GridExcelDataInvestigator.Rows[j].Cells[13].Text.Trim());
                            if (NationalInternational == "" || NationalInternational == null || NationalInternational == "&nbsp;")
                            {
                                NationalInternational = "";
                            }
                            else
                            {
                                NationalInternational = (GridExcelDataInvestigator.Rows[j].Cells[13].Text.Trim());
                            }
                            string Continent = (GridExcelDataInvestigator.Rows[j].Cells[14].Text.Trim());
                            if (Continent == "" || Continent == null || Continent == "&nbsp;")
                            {
                                Continent = "";
                            }
                            else
                            {
                                Continent = (GridExcelDataInvestigator.Rows[j].Cells[14].Text.Trim());
                            }
                            string isLeadPI = (GridExcelDataInvestigator.Rows[j].Cells[15].Text.Trim());

                            jdi[rowindex1] = new GrantData();

                            jdi[rowindex1].ProjectUnit = ProjectUnit1;
                            jdi[rowindex1].PID = ID1;
                            jdi[rowindex1].EntryNo = Convert.ToInt32(EntryNo);
                            jdi[rowindex1].InvestigatorName = InvestigatorName;
                            jdi[rowindex1].MUNonMU = MUNonMU;
                            jdi[rowindex1].EmployeeCode = EmployeeCode;
                            jdi[rowindex1].Institution = Institution;
                            jdi[rowindex1].Department = Department;
                            jdi[rowindex1].EmailId = EmailId;
                            jdi[rowindex1].InvestigatorType = InvestigatorType;
                            jdi[rowindex1].InstitutionName = InstitutionName;
                            jdi[rowindex1].DepartmentName = DepartmentName;
                            jdi[rowindex1].NationalInternational = NationalInternational;
                            jdi[rowindex1].Continent = Continent;
                            jdi[rowindex1].isLeadPI = isLeadPI;

                            if (jdi[rowindex1].InvestigatorType == "P" && jdi[rowindex1].isLeadPI == "Y")
                            {

                                jd[rowindex].PIInstitutionID = jdi[rowindex1].Institution;
                                jd[rowindex].PIDeptID = jdi[rowindex1].Department;
                                //if (jdi[j].MUNonMU == "M")
                                //{
                                //    jd[rowindex].PIInstitutionID = jdi[j].Institution;
                                //    jd[rowindex].PIDeptID = jdi[j].Department;
                                //}
                                //else if (jdi[j].MUNonMU == "N")
                                //{
                                //    jd[rowindex].PIInstitutionID = jdi[j].Institution;
                                //    jd[rowindex].PIDeptID = jdi[j].Department;
                                //}
                                //else if (jdi[j].MUNonMU == "S")
                                //{
                                //    jd[rowindex].PIInstitutionID = jdi[j].Institution;
                                //    jd[rowindex].PIDeptID = jdi[j].Department;
                                //}
                              
                            }
                            if (jdi[rowindex1].EmployeeCode != "")
                            {
                                if (jdi[rowindex1].MUNonMU == "M")
                                {
                                    jd[rowindex].InstitutionID = jdi[rowindex1].Institution;
                                    jd[rowindex].DeptID = jdi[rowindex1].Department;
                                    jd[rowindex].CreatedBy = jdi[rowindex1].EmployeeCode;
                                    
                                }
                                //else if (jdi[j].MUNonMU == "N")
                                //{
                                //    jd[rowindex].InstitutionID = jdi[j].Institution;
                                //    jd[rowindex].DeptID = jdi[j].Department;
                                //    jd[rowindex].CreatedBy = jdi[j].EmployeeCode;
                                //}
                                //else if (jdi[j].MUNonMU == "S")
                                //{
                                //    jd[rowindex].InstitutionID = jdi[j].Institution;
                                //    jd[rowindex].DeptID = jdi[j].Department;
                                //    jd[rowindex].CreatedBy = jdi[j].EmployeeCode;
                                //}
                            }
                            //if (jdi[j].EmployeeCode != "")
                            //{
                            //    if (jdi[j].MUNonMU == "M")
                            //    {
                            //        if (jdi[j].InvestigatorType == "P" && jdi[j].isLeadPI == "Y")
                            //        {
                            //            jd[rowindex].CreatedBy = jdi[j].EmployeeCode;
                            //        }
                            //        else
                            //        {
                            //            jd[rowindex].CreatedBy = jdi[j].EmployeeCode;
                            //        }
                            //    }

                            //}

                            if (jd[rowindex].ProjectUnit != null && jd[rowindex].PID != null)
                            {
                                if (jd[rowindex].ProjectUnit == jdi[rowindex1].ProjectUnit && jd[rowindex].PID == jdi[rowindex1].PID)
                                {
                                    result = b.Insertfileuploadprojets(jd[rowindex], jdi[rowindex1]);
                                }
                                if (jdi[rowindex1].ProjectUnit == null && jdi[rowindex1].PID == null)
                                {
                                    Response.Write("<script language='javascript'>alert(' Project Investigator details doesnot exists for the project '" + ID + "'+ '" + ProjectUnit + "'')</script>");
                                }
                                //if (jd[rowindex].ProjectUnit != jdi[j].ProjectUnit && jd[rowindex].PID == jdi[j].PID)
                                //{
                                //    Response.Write("<script language='javascript'>alert(' ProjectUnit is not matching for the project '" + ID + "'+ '" + ProjectUnit + "'')</script>");
                                //}
                            }
                        }
                    }
                }

                if (CheckRowsSkipped==0)
                {
                    if (result >= 1)
                    {

                        log.Info("File uploaded Successfull");


                        Response.Write("<script language='javascript'>alert('File uploaded Successfully!')</script>");
                        upload.Enabled = false;
                    }
                }
                else
                    if (CheckRowsSkipped >= 1)
                    {
                        if (result >= 1)
                        {

                            log.Info("File uploaded Successfully  ");


                            Response.Write("<script language='javascript'>alert('File uploaded Successfullyand " + CheckRowsSkipped + "  entries are skipped because project details already exists...')</script>");
                            upload.Enabled = false;
                        }
                        else
                    {
                        Response.Write("<script language='javascript'>alert(' " + CheckRowsSkipped + "  entries are skipped because they were already exisiting...')</script>");
                        upload.Enabled = false;
                    }

                    }
                          else
                        if (CheckRowsSkipped >= 1)
                        {
                            Response.Write("<script language='javascript'>alert(' " + CheckRowsSkipped + "  entries are skipped because they were already exisiting...')</script>");
                            upload.Enabled = false;
                        }
                        else
                        {
                            Response.Write("<script language='javascript'>alert('Error in  File upload')</script>");

                            log.Info(" File upload :");

                        }

            }

            else
            {
               
                Response.Write("<script language='javascript'>alert('Invalid File!')</script>");
                validExcel = 1;
                upload.Enabled = false;
            }
        }



        catch (Exception ex)
        {
            
            log.Error(ex.StackTrace);

            if (ex.Message.Contains("Violation of PRIMARY KEY constraint 'PK_Project'. Cannot insert duplicate key in object 'dbo.Project'."))
            {

                Response.Write("<script language='javascript'>alert('Error..!This ID Already Exists!')</script>");
               
            }
            else if (ex.Message.Contains("String was not recognized as a valid DateTime."))
            {

                Response.Write("<script language='javascript'>alert('Date Format is not correct in the file..Please Check..')</script>");
            }
            else if (ex.Message.Contains("SqlDateTime overflow. Must be between 1/1/1753 12:00:00 AM and 12/31/9999 11:59:59 PM."))
            {

                Response.Write("<script language='javascript'>alert('Date  is valid..Please Check..')</script>");
            }
            else if (ex.Message.Contains("The UPDATE statement conflicted with the FOREIGN KEY constraint \"FK_Project_User_M\". The conflict occurred in database \"RMS\", table \"dbo.User_M\", column 'User_Id'."))
            {

                Response.Write("<script language='javascript'>alert('The User does not does not exist in RMS..')</script>");
            }
            else if (ex.Message.Contains("The INSERT statement conflicted with the FOREIGN KEY constraint \"FK_Project_ProjectSource_M\". The conflict occurred in database \"RMS\", table \"dbo.ProjectSource_M\", column 'SourceId'."))
            {

                Response.Write("<script language='javascript'>alert('The SourceProject does not does not exist in RMS..')</script>");
            }
            else

                Response.Write("<script language='javascript'>alert('Error in Uploading!')</script>");
        }

    }





    protected void bindGridview(object sender, EventArgs e)
    {
        string connectionString = "";
        string servername = ConfigurationManager.AppSettings["ServerName"].ToString();
        string domainame = ConfigurationManager.AppSettings["DomainName"].ToString();
        string username = ConfigurationManager.AppSettings["UserName"].ToString();
        string password = ConfigurationManager.AppSettings["Password"].ToString();
        // string FolderPathServerwrite = ConfigurationManager.AppSettings["UploadDocPath"].ToString();
        using (NetworkShareAccesser.Access(servername, domainame, username, password))
        {

            if (F_Upload.HasFile)
            {

                string savePath = ConfigurationManager.AppSettings["FolderPath"];
                Directory.CreateDirectory(savePath);
                string strFileType = Path.GetExtension(F_Upload.FileName).ToLower();
                string path = Path.Combine(savePath, F_Upload.FileName);
                F_Upload.SaveAs(path);


                if (strFileType == ".xls")
                {
                    connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                }
                else if (strFileType == ".xlsx")
                {
                    connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                }


                else
                {
                    validExcel = 0;
                    return;
                }

                System.Data.DataTable dt = null;
                OleDbConnection conn = new OleDbConnection(connectionString);
                conn.Open();
                dt = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                String[] excelSheets = new String[dt.Rows.Count];
                int i = 0;

                foreach (DataRow row in dt.Rows)
                {
                    excelSheets[i] = row["TABLE_NAME"].ToString();
                    i++;
                }

                for (int j = 0; j < excelSheets.Length; j++)
                {
                    string query = "Select * from[" + excelSheets[j] + "]";

                    //string query = "SELECT * FROM [Projectnvestigator$]";
                    // OleDbConnection conn = new OleDbConnection(connectionString);

                    if (conn.State == ConnectionState.Closed)
                        conn.Open();
                    OleDbCommand cmd = new OleDbCommand(query, conn);
                    OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    if (excelSheets[j] == "'Project Details$'")
                    {
                        GridExcelDataProject.DataSource = ds.Tables[0];
                        GridExcelDataProject.DataBind();
                        da.Dispose();
                    }
                    if (excelSheets[j] == "'Investigator Details$'")
                    {
                        GridExcelDataInvestigator.DataSource = ds.Tables[0];
                        GridExcelDataInvestigator.DataBind();
                        da.Dispose();
                    }
                }


                conn.Close();
                conn.Dispose();




            }
        }
    }

    //protected void GridExcelData_PageChanged(object sender, GridViewPageEventArgs e)
    //{
    //    GridExcelDataProject.PageIndex = e.NewPageIndex;
    //    GridExcelDataProject.DataBind();
    //}
    //protected void GridExcelDataInvestigator_PageChanged(object sender, GridViewPageEventArgs e)
    //{
    //    GridExcelDataInvestigator.PageIndex = e.NewPageIndex;
    //    GridExcelDataInvestigator.DataBind();
    //}

    protected void GridExcelDataInvestigator_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    for (int i = 2; i < e.Row.Cells.Count; i++)
        //    {
        //        if (GridExcelDataInvestigator.Rows[i].Cells[1].Text == "&nbsp;" || e.Row.Cells[1].Text.Trim() == "" || e.Row.Cells[1].Text == null)
        //        {
        //            GridExcelDataInvestigator.Visible = true;
        //            //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Excel Data Is Invalid!')</script>");
        //            Response.Write("<script language='javascript'>alert('Excel Data Is Invalid!')</script>");
        //        }
        //    }
        //}
    }

    protected void GridExcelDataProject_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    for (int i = 2; i < e.Row.Cells.Count; i++)
        //    {
        //        if (GridExcelDataProject.Rows[i].Cells[0].Text == "&nbsp;" || e.Row.Cells[0].Text.Trim() == "" || e.Row.Cells[0].Text == null)
        //        {
        //            GridExcelDataProject.Visible = true;
        //            //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Excel Data Is Invalid!')</script>");
        //            Response.Write("<script language='javascript'>alert('Excel Data Is Invalid!')</script>");
        //        }
        //    }
        //}

    }




    
    protected void lnkExcelFile_Click(object sender, EventArgs e)
    {
        string servername = ConfigurationManager.AppSettings["ServerName"].ToString();
        string domainame = ConfigurationManager.AppSettings["DomainName"].ToString();
        string username = ConfigurationManager.AppSettings["UserName"].ToString();
        string password = ConfigurationManager.AppSettings["Password"].ToString();
        string folderpath;
        using (NetworkShareAccesser.Access(servername, domainame, username, password))
        {

            folderpath = ConfigurationManager.AppSettings["DocumentsPath"].ToString();
            string[] files = Directory.GetFiles(folderpath);
            string filename1 = "ProjectSample.xls";
            string path_BoxId = Path.Combine(folderpath, filename1);
            string newpath = path_BoxId.Replace('\\', '/');
            string filePath = path_BoxId;
            var uri = new Uri(newpath);
            string filename = Path.GetFullPath(uri.LocalPath);
            HttpResponse response = HttpContext.Current.Response;
            response.Clear();
            response.ClearContent();
            response.ClearHeaders();
            response.Buffer = false;

            WebClient wc = new WebClient();
            response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", Path.GetFileName(newpath)));
            response.ContentType = "application/octet-stream";
            byte[] data = wc.DownloadData(filePath);
            response.BinaryWrite(data);
            response.End();
        }
    }

}