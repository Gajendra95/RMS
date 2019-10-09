using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using Microsoft.SqlServer.Server;
using System.Configuration;
using System.Drawing;
using System.Collections;
using System.Data;
using System.Web.UI.WebControls;
using System.Text;
using log4net;
using Ionic.Zip;
using System.Globalization;

/// <summary>
/// Summary description for ProjectPDFHelper
/// </summary>
public class ProjectPDFHelper
{
    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    string mainpath = ConfigurationManager.AppSettings["PdfPath"].ToString();
    string path1 = null;
    private static PdfPCell PhraseCell(Phrase phrase, int align)
    {
        PdfPCell cell = new PdfPCell(phrase);
        cell.BorderColor = BaseColor.WHITE;
        cell.HorizontalAlignment = align;
        cell.PaddingBottom = 2f;
        cell.PaddingTop = 0f;
        return cell;
    }

    public void pdfGenerate(string ProjectID, string projectunit)
    {
        string pdftype = projectunit;
        string boxid = ProjectID;
        string AuthorID = ProjectID;

        Document doc = new Document(PageSize.A4, 88f, 88f, 10f, 10f);


        iTextSharp.text.Font NormalFont = FontFactory.GetFont("Arial", 12, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);



        string filelocationpath = "";
        string actualfilenamewithtime = "";
        string fileid1 = "";
        string servername = ConfigurationManager.AppSettings["ServerName"].ToString();
        string domainame = ConfigurationManager.AppSettings["DomainName"].ToString();
        string username = ConfigurationManager.AppSettings["UserName"].ToString();
        string password = ConfigurationManager.AppSettings["Password"].ToString();
       string mainpath = ConfigurationManager.AppSettings["PdfPath"].ToString();
       actualfilenamewithtime = ProjectID + "_" + "merged" + ".pdf";
        try
        {
            using (NetworkShareAccesser.Access(servername, domainame, username, password))
            {
                path1 = Path.Combine(mainpath, "PublicationPrintEvaluation");
                // Create a new PdfWriter object, specifying the output stream
                if (Directory.Exists(path1))
                {
                    //string patientid = PatientID;
                    foreach (var files in Directory.GetFiles(path1))
                    {
                        FileInfo info = new FileInfo(files);
                        var fileName = Path.GetFileName(info.FullName);
                        if (info.Exists)
                        {
                            info.Delete();
                        }

                    }

                    filelocationpath = Path.Combine(path1, actualfilenamewithtime);

                    var output = new FileStream(filelocationpath, FileMode.Create);
                    var writer = PdfWriter.GetInstance(doc, output);

                }
                else
                {
                    Directory.CreateDirectory(path1);
                    string path = Path.Combine(path1, actualfilenamewithtime);
                    var output = new FileStream(filelocationpath, FileMode.Create);
                    var writer = PdfWriter.GetInstance(doc, output);
                }


        doc.Open();
        //User p5 = new User();
        //Journal_DataObject obj5 = new Journal_DataObject();
        //p5 = obj5.ImagePath();
        iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(HttpContext.Current.Request.MapPath("~/Images/MAHE-logo.png"));
        img.Alignment = iTextSharp.text.Image.TEXTWRAP | iTextSharp.text.Image.ALIGN_RIGHT;
        img.ScaleToFit(150f, 150f);
        img.SetAbsolutePosition(58, 760); // left  top
        img.SpacingAfter = 20f;
       
        doc.Add(img);
        PdfPCell cell = null;
        Paragraph para = null;
        Paragraph para1 = null;
        Paragraph para2 = null;
        Paragraph para3 = null;
        Paragraph para4 = null;
        iTextSharp.text.Font times = null;
        iTextSharp.text.Font times1 = null;
        BaseFont bfTimes = null;
        bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
        times = new iTextSharp.text.Font(bfTimes, 11, iTextSharp.text.Font.BOLD);
        times1 = new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.BOLD);
        para = new Paragraph("CERTIFICATE FROM THE INVESTIGATORS", times);     
        para.SpacingBefore = 15f;
        para.IndentationLeft = 140f;
        doc.Add(para);


        para4 = new Paragraph("For", times);
        para4.SpacingBefore = 15f;
        para4.IndentationLeft = 200f;
        doc.Add(para4);
        para2 = new Paragraph("Unique Tracking Number (UTN) of the Proposal", times);
        para2.SpacingBefore = 5f;
        para2.IndentationLeft = 130f;
        doc.Add(para2);
        para3 = new Paragraph("(To be submitted by the Principal Investigator before obtaining a letter of endorsement from the Head of the", times1); 
      
        para3.SpacingBefore = 15f;
        para3.IndentationLeft = -20f; 
        doc.Add(para3);
        para3 = new Paragraph("    Institution/Organization)", times1);
        para3.SpacingBefore = 3f;
        para3.IndentationLeft = 140f;
        doc.Add(para3);
        

        PdfPTable table = new PdfPTable(2);
        table.SpacingBefore = 10f;
        //cell.BorderColor = new BaseColor(0, 0, 0);
        table.TotalWidth = 320f;
        //fix the absolute width of the table
        table.LockedWidth = true;
        table.SpacingBefore = 10f;
        table.SpacingAfter = 40f;
        times = new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.NORMAL);
        Journal_DataObject obj = new Journal_DataObject();
        User p1 = new User();
        User p2 = new User();
        User p3 = new User();
        p1 = obj.fnfindProject(ProjectID, projectunit);
        string agency = obj.FindAgencyName(p1.AgencyId);
                  //iTextSharp.text.Font t = null;
                  //t = new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.BOLD);
      
        Journal_DataObject obj2 = new Journal_DataObject();
        p2 = obj.ProjectCount(ProjectID, projectunit);

      

        times1 = new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.BOLD);
        para1 = new Paragraph("", times1);
        para1.SpacingBefore = 40f;// spacing from the top
        para1.SpacingAfter = 10f; // spacing from the bottom
        para1.IndentationLeft = -30f; // left space 
        doc.Add(para1);
        times1 = new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.BOLD);
        para1 = new Paragraph("Project Title:  ", times1);

        para1.SpacingBefore = -25f;// spacing from the top
        para1.SpacingAfter = 5f; // spacing from the bottom
        para1.IndentationLeft = -20f; // left space 

        doc.Add(para1);
        times1 = new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.NORMAL);
        para1 = new Paragraph("" + p1.Title, times1);

        para1.SpacingBefore = -20f;// spacing from the top
        para1.SpacingAfter = 5f; // spacing from the bottom
        para1.IndentationLeft = 36f; // left space 

        doc.Add(para1);

        times1 = new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.BOLD);
        para1 = new Paragraph("", times1);

        para1.SpacingBefore = 10f;// spacing from the top
        para1.SpacingAfter = 50f; // spacing from the bottom
        para1.IndentationLeft = -30f; // left space 
        doc.Add(para1);
        times1 = new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.BOLD);
        para1 = new Paragraph("Date of Application to funding Agency:    ", times1);
        para1.SpacingBefore = -60f;// spacing from the top
        para1.SpacingAfter = -20f; // spacing from the bottom
        para1.IndentationLeft = -20f; // left space 
        doc.Add(para1);
        times1 = new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.NORMAL);
        para1 = new Paragraph("" + p1.AppliedDate.ToShortDateString(), times1);
        para1.SpacingBefore = 6f;// spacing from the top
        para1.SpacingAfter = 5f; // spacing from the bottom
        para1.IndentationLeft = 143f; // left space 
        doc.Add(para1);

        PdfPTable tabl_Orth026 = new PdfPTable(2);
        cell = PhraseCell(new Phrase(), PdfPCell.ALIGN_LEFT);
        cell.PaddingBottom = 10f;
        tabl_Orth026.AddCell(cell);
        tabl_Orth026.SpacingBefore = 30f;
        doc.Add(tabl_Orth026);
        times1 = new iTextSharp.text.Font(bfTimes, 10);
        para1.Alignment = Element.ALIGN_CENTER;
        para1.SpacingBefore = -50f;
        PdfPTable tabData = new PdfPTable(3);
        tabData.TotalWidth = 480f;
        //fix the absolute width of the table
        tabData.LockedWidth = true;       
        times = new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.NORMAL);
        float[] widths = new float[] { 1.8f, 3.5f, 3f};
        tabData.AddCell(new PdfPCell(new Phrase(" Investigators", FontFactory.GetFont("Arial", 11, iTextSharp.text.Font.BOLD, BaseColor.BLACK))));
        tabData.AddCell(new PdfPCell(new Phrase(" Name", FontFactory.GetFont("Arial", 11, iTextSharp.text.Font.BOLD, BaseColor.BLACK))));
        tabData.AddCell(new PdfPCell(new Phrase(" Affiliation", FontFactory.GetFont("Arial", 11, iTextSharp.text.Font.BOLD, BaseColor.BLACK))));
       

        tabData.AddCell(cell);
        tabData.SetWidths(widths);
        
        tabData.SpacingBefore = 20f;
        para1.IndentationLeft = 30f; // left space 
        doc.Add(tabData);

        ArrayList authors = new ArrayList();
        int a;
        int b = p2.AuthorCount;


        string s1 = p2.AuthorName1;

        for (a = 0; a < b; a++)
        {
            authors.Add(a);


        }
        DataSet dz = null;
        string publicationtype = obj2.CheckPublicationType(ProjectID, projectunit);
        if (publicationtype == "F")
        {
             dz = obj2.fnInvestigatorsdetail(ProjectID, projectunit);
        }
        else
        {
             dz = obj2.fnStudentdetail(ProjectID, projectunit);
        }
        
      
        int count = dz.Tables[0].Rows.Count;
        for (int i = 0; i < count; i++)
        {

            int rownumber = i ;
            PdfPTable tabData2 = new PdfPTable(3);
            cell.BorderColor = new BaseColor(0, 0, 0);
            cell.VerticalAlignment = 10;
           
            tabData2.TotalWidth = 480f;
            //fix the absolute width of the table AuthorName1,EmailId2,DepartmentName,Institute_name
            tabData2.LockedWidth = true;
            float[] width = new float[] { 1f, 3f, 2.5f};
            times = new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.NORMAL);
            string investigatortype = dz.Tables[0].Rows[i]["InvestigatorType"].ToString();
            if (investigatortype == "P")
            {

                tabData2.AddCell(new PdfPCell(new Phrase(" Principal Investigator ", FontFactory.GetFont("Arial", 9, iTextSharp.text.Font.NORMAL, BaseColor.BLACK))));
                string str = dz.Tables[0].Rows[i]["InvestigatorName"].ToString();
                string IndexAgency = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(str.ToLower());
                tabData2.AddCell(new PdfPCell(new Phrase("  " + IndexAgency, FontFactory.GetFont("Arial", 9, iTextSharp.text.Font.NORMAL, BaseColor.BLACK))));
                string IndexAgency2 = dz.Tables[0].Rows[i]["InstitutionName"].ToString();
                tabData2.AddCell(new PdfPCell(new Phrase(" " + IndexAgency2, FontFactory.GetFont("Arial", 9, iTextSharp.text.Font.NORMAL, BaseColor.BLACK))));
            }
            else if (investigatortype == "C")
            {

                tabData2.AddCell(new PdfPCell(new Phrase(" Co- Investigator " + rownumber, FontFactory.GetFont("Arial", 9, iTextSharp.text.Font.NORMAL, BaseColor.BLACK))));
                string str = dz.Tables[0].Rows[i]["InvestigatorName"].ToString();
                string IndexAgency = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(str.ToLower());
                tabData2.AddCell(new PdfPCell(new Phrase("  " + IndexAgency, FontFactory.GetFont("Arial", 9, iTextSharp.text.Font.NORMAL, BaseColor.BLACK))));
                string IndexAgency2 = dz.Tables[0].Rows[i]["InstitutionName"].ToString();
                tabData2.AddCell(new PdfPCell(new Phrase(" " + IndexAgency2, FontFactory.GetFont("Arial", 9, iTextSharp.text.Font.NORMAL, BaseColor.BLACK))));
            }

            tabData2.AddCell(cell);
            tabData2.SetWidths(widths);
            doc.Add(tabData2);
            //tabData2.SpacingAfter = 10f; 
        }

          PdfPTable tablen2 = new PdfPTable(2);
        //actual width of table in points
        tablen2.TotalWidth = 500f;//216f;
      
        //fix the absolute width of the table
        tablen2.LockedWidth = true;
      
        //relative col widths in proportions -  2/3 & 1/3 
        float[] widthw2 = new float[] { 0.1f, 2f };
        tablen2.SetWidths(widthw2);
        tablen2.HorizontalAlignment = -50;
        //leave a gap before and after the table
     
            
        //PdfPCell cell2 = new PdfPCell(new Phrase("ITEM"));
        PdfPCell cell22 = new PdfPCell(new Phrase("", new iTextSharp.text.Font(iTextSharp.text.Font.NORMAL, 10, iTextSharp.text.Font.BOLD)));
        cell22.Border = iTextSharp.text.Rectangle.NO_BORDER;
        cell22.BackgroundColor = new iTextSharp.text.BaseColor(233, 233, 233);
        tablen2.SpacingBefore = -30f;
        tablen2.SpacingAfter = 0f;    
        tablen2.AddCell(cell22);


        PdfPCell cell222 = new PdfPCell(new Phrase("Certified that the proposed project will be carried out at MAHE subject to approval of the proposal for funding from " + agency.ToString() + " agency/organization for a sum of Rs " + p1.AppliedAmount + " for a period of " + p1.DurationOfProject + " months. The research work proposed in the scheme/project does not in any way duplicate the work already done or being carried out elsewhere on the subject nor is under consideration for financial support from any other agency.", new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.NORMAL)));
        cell222.Border = iTextSharp.text.Rectangle.NO_BORDER;
        cell222.BackgroundColor = new iTextSharp.text.BaseColor(233, 233, 233);
            
        tablen2.AddCell(cell222);
        tablen2.SpacingBefore = 30f;
        tablen2.SpacingAfter = 0f;
        doc.Add(tablen2);
      
       
        PdfPTable tablen12 = new PdfPTable(2);
        //actual width of table in points
        tablen12.TotalWidth = 500f;//216f;
        //fix the absolute width of the table
        tablen12.LockedWidth = true;
        //relative col widths in proportions -  2/3 & 1/3 
        float[] w = new float[] { 0.1f, 2f };
        tablen12.SetWidths(w);
        tablen12.HorizontalAlignment = -50;
        //leave a gap before and after the table
        tablen12.SpacingBefore = 0;
        tablen12.SpacingAfter = 10f;
        //PdfPCell cell2 = new PdfPCell(new Phrase("ITEM"));
        PdfPCell cellr = new PdfPCell(new Phrase("", new iTextSharp.text.Font(iTextSharp.text.Font.NORMAL, 10, iTextSharp.text.Font.NORMAL)));
        cellr.Border = iTextSharp.text.Rectangle.NO_BORDER;
        cellr.BackgroundColor = new iTextSharp.text.BaseColor(233, 233, 233);
        tablen12.AddCell(cellr);
        PdfPCell cellr1 = new PdfPCell(new Phrase("\nHaving obtained the Ethical Approval from the Concerned Ethical Committee and agreeing to submit a certificate from the Institutional biosafety committee where applicable, the proposal has been assigned a Unique Tracking Number, UTN MAHE/ " + p1.UTN.ToString() + " dated " + p1.Created_Date.ToShortDateString() + " \n\n", new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.NORMAL)));
        cellr1.Border = iTextSharp.text.Rectangle.NO_BORDER;
        cellr1.BackgroundColor = new iTextSharp.text.BaseColor(233, 233, 233);
        tablen12.AddCell(cellr1);
        doc.Add(tablen12);


        PdfPTable OfficeUse1 = new PdfPTable(3);
        //actual width of table in points
        OfficeUse1.TotalWidth = 500f;//216f;
        //fix the absolute width of the table
        OfficeUse1.LockedWidth = true;
        //relative col widths in proportions -  2/3 & 1/3 
        float[] OfficeUseW1 = new float[] { 7f, 5.3f, 7f };
        OfficeUse1.SetWidths(OfficeUseW1);
        OfficeUse1.HorizontalAlignment = 250;
        //leave a gap before and after the table
        //tablen.SpacingBefore = 20f;
        //OfficeUse.SpacingAfter = 0;
        PdfPCell OfficeUseCell11 = new PdfPCell(new Phrase("\n\n\nName & Signature of the Principal Investigator", new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.BOLD)));
        OfficeUseCell11.Right = 50f;
        OfficeUseCell11.PaddingTop = 150f;
       
        OfficeUseCell11.Border = iTextSharp.text.Rectangle.NO_BORDER;
        //OfficeUseCell11.BackgroundColor = new iTextSharp.text.BaseColor(233, 233, 233);
        OfficeUse1.AddCell(OfficeUseCell11);
        PdfPCell OfficeUseCel2 = new PdfPCell(new Phrase("\n\n\n", new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.BOLD)));
        OfficeUseCel2.Right = 50f;
        OfficeUseCel2.Border = iTextSharp.text.Rectangle.NO_BORDER;
        //OfficeUseCel2.BackgroundColor = new iTextSharp.text.BaseColor(233, 233, 233);
        OfficeUse1.AddCell(OfficeUseCel2);
        PdfPCell OfficeUseCel5 = new PdfPCell(new Phrase("\n\n\n Name & Signature of Head of Institution", new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.BOLD)));
        OfficeUseCel5.Right = 50f;
        OfficeUseCel5.PaddingTop = 150f;
        OfficeUseCel5.Border = iTextSharp.text.Rectangle.NO_BORDER;
        //OfficeUseCel5.BackgroundColor = new iTextSharp.text.BaseColor(233, 233, 233);
        OfficeUse1.AddCell(OfficeUseCel5);
        doc.Add(OfficeUse1);
       
        doc.Close();
       
        FileInfo myfile = new FileInfo(filelocationpath);
        if (myfile.Exists)
        {
            log.Info("Evaluation Form Generated Successfully,  for Project ID: " + ProjectID + " of Project Unit: " + projectunit );
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + actualfilenamewithtime);
            HttpContext.Current.Response.AddHeader("Content-Length", myfile.Length.ToString());
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.TransmitFile(myfile.FullName);
            HttpContext.Current.Response.End();
        }
            }

        }


        catch (Exception e)
        {
            log.Info("Evaluation Form generation failed,  for Project ID: " + ProjectID + " of Project Unit: " + projectunit );
            log.Error("Error: " + e);
        }
            
    }
    
}