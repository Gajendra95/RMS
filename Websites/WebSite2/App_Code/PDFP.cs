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

/// <summary>
/// Summary description for PDFP
/// </summary>
public class PDFP
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

    public void pdfGenerate(string PatentID)
    {

        string boxid = PatentID;
        string AuthorID = PatentID;

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
        actualfilenamewithtime = PatentID + "_" + "merged" + ".pdf";
        try
        {
            using (NetworkShareAccesser.Access(servername, domainame, username, password))
            {
                path1 = Path.Combine(mainpath, "PatentPrintEvaluation");
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

                iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(HttpContext.Current.Request.MapPath("~/Images/MAHE-logo.png"));
                img.Alignment = iTextSharp.text.Image.TEXTWRAP | iTextSharp.text.Image.ALIGN_RIGHT;
                img.ScaleToFit(150f, 150f);
                img.SetAbsolutePosition(58, 760); // left  top
                img.SpacingAfter = 20f;

                doc.Add(img);
                PdfPCell cell = null;
                Paragraph para = null;
                Paragraph para1 = null;
                iTextSharp.text.Font times = null;
                iTextSharp.text.Font times1 = null;
                BaseFont bfTimes = null;
                bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
                times = new iTextSharp.text.Font(bfTimes, 11, iTextSharp.text.Font.BOLD);
                para = new Paragraph("EVALUATION FORM OF STUDENT RESEARCHER FOR PATENT", times);

                para.SpacingBefore = 15f;
                para.IndentationLeft = 150f;
                doc.Add(para);

                times1 = new iTextSharp.text.Font(bfTimes, 9, iTextSharp.text.Font.ITALIC);
                para1 = new Paragraph("with effect from JULY 1, 2016", times1);
                para1.SpacingBefore = 5f;
                para1.Alignment = Element.ALIGN_RIGHT;
                para1.IndentationLeft = 80f;
                doc.Add(para1);


                Paragraph paras = null;
                bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
                times = new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.BOLD);
                paras = new Paragraph("STN 312", times);
                paras.SpacingBefore = 5f;

                paras.IndentationLeft = 380f;
                doc.Add(paras);


                PdfPTable table = new PdfPTable(2);
                table.SpacingBefore = 10f;
                //cell.BorderColor = new BaseColor(0, 0, 0);
                table.TotalWidth = 320f;
                //fix the absolute width of the table
                table.LockedWidth = true;
                table.SpacingBefore = 10f;
                table.SpacingAfter = 40f;
                times = new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.NORMAL);
                PatentBusiness obj = new PatentBusiness();
                Patent p1 = new Patent();
                Patent p2 = new Patent();
                Patent p3 = new Patent();
                p1 = obj.fnfindInventor(PatentID);
                PatentBusiness obj2 = new PatentBusiness();
                p2 = obj.PatentInvetor(PatentID);
                times1 = new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.BOLD);
                para1 = new Paragraph("•", times1);
                para1.SpacingBefore = 40f;// spacing from the top
                para1.SpacingAfter = 10f; // spacing from the bottom
                para1.IndentationLeft = -30f; // left space 
                doc.Add(para1);
                times1 = new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.BOLD);
                para1 = new Paragraph("Patent Id:  ", times1);

                para1.SpacingBefore = -25f;// spacing from the top
                para1.SpacingAfter = 5f; // spacing from the bottom
                para1.IndentationLeft = -20f; // left space 

                doc.Add(para1);
                times1 = new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.NORMAL);
                para1 = new Paragraph("" + AuthorID, times1);

                para1.SpacingBefore = -20f;// spacing from the top
                para1.SpacingAfter = 5f; // spacing from the bottom
                para1.IndentationLeft = 50f; // left space 

                doc.Add(para1);

                times1 = new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.BOLD);
                para1 = new Paragraph("•", times1);

                para1.SpacingBefore = 10f;// spacing from the top
                para1.SpacingAfter = 50f; // spacing from the bottom
                para1.IndentationLeft = -30f; // left space 
                doc.Add(para1);
                times1 = new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.BOLD);
                para1 = new Paragraph("Title of the Patent Granted:", times1);
                para1.SpacingBefore = -65f;// spacing from the top
                para1.SpacingAfter = -20f; // spacing from the bottom
                para1.IndentationLeft = -20f; // left space 
                doc.Add(para1);
                times1 = new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.NORMAL);
                para1 = new Paragraph("" + p1.Title, times1);
                para1.SpacingBefore = 5f;// spacing from the top
                para1.SpacingAfter = 5f; // spacing from the bottom
                para1.IndentationLeft = 100f; // left space 
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

                PdfPTable tabData = new PdfPTable(4);
                tabData.TotalWidth = 480f;
                //fix the absolute width of the table
                tabData.LockedWidth = true;
                times = new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.NORMAL);
                float[] widths = new float[] { 1f, 3f, 2.5f, 3f };
                tabData.AddCell(new PdfPCell(new Phrase(" Sl.No", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD, BaseColor.BLACK))));
                tabData.AddCell(new PdfPCell(new Phrase(" Names of Inventors", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD, BaseColor.BLACK))));
              //  tabData.AddCell(new PdfPCell(new Phrase(" Type of Author", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD, BaseColor.BLACK))));
                tabData.AddCell(new PdfPCell(new Phrase(" Department", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD, BaseColor.BLACK))));
                tabData.AddCell(new PdfPCell(new Phrase(" EMP Code/Registration No. of Student", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD, BaseColor.BLACK))));
               

                tabData.AddCell(cell);
                tabData.SetWidths(widths);

                tabData.SpacingBefore = 20f;
                para1.IndentationLeft = 30f; // left space 
                doc.Add(tabData);

                ArrayList authors = new ArrayList();
                int a;
                int b = p2.AuthorCount;


                string s1 = p2.InvestigatorName;

                for (a = 0; a < b; a++)
                {
                    authors.Add(a);


                }

                DataSet dz = obj2.fnfindPatentAccount12(PatentID);

                DataSet ds = null;
                //string isstudentpublication = obj2.CheckIsStudentPublication(PatentID);
                //if (isstudentpublication == "Y")
                //{
                     ds = obj2.SelectStudentAuthorDetail(PatentID);
                  // dz.Merge(ds);
                //}  
     
                int count = dz.Tables[0].Rows.Count;
                for (int i = 0; i < count; i++)
                {

                    int rownumber = i + 1;
                    PdfPTable tabData21 = new PdfPTable(4);
                    cell.BorderColor = new BaseColor(0, 0, 0);
                    tabData21.TotalWidth = 480f;
                    //fix the absolute width of the table AuthorName1,EmailId2,DepartmentName,Institute_name
                    tabData21.LockedWidth = true;
                    float[] width = new float[] { 1f, 3f, 2.5f, 3f };
                    times = new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.NORMAL);
                    tabData21.AddCell(new PdfPCell(new Phrase("  " + rownumber, FontFactory.GetFont("Arial", 9, iTextSharp.text.Font.NORMAL, BaseColor.BLACK))));
                    string IndexAgency = dz.Tables[0].Rows[i]["InvestigatorName"].ToString();
                    tabData21.AddCell(new PdfPCell(new Phrase("  " + IndexAgency, FontFactory.GetFont("Arial", 9, iTextSharp.text.Font.NORMAL, BaseColor.BLACK))));
                   
                    string IndexAgency3 = dz.Tables[0].Rows[i]["DepartmentName"].ToString();
                    tabData21.AddCell(new PdfPCell(new Phrase(" " + IndexAgency3, FontFactory.GetFont("Arial", 9, iTextSharp.text.Font.NORMAL, BaseColor.BLACK))));
                    string IndexAgency4 = dz.Tables[0].Rows[i]["EmployeeCode"].ToString();
                    tabData21.AddCell(new PdfPCell(new Phrase(" " + IndexAgency4, FontFactory.GetFont("Arial", 9, iTextSharp.text.Font.NORMAL, BaseColor.BLACK))));
                   
                    tabData21.AddCell(cell);
                    tabData21.SetWidths(widths);
                    tabData21.SpacingBefore = 0;
                    tabData21.SpacingAfter = 0;
                    doc.Add(tabData21);

                }



                PdfPTable tablen = new PdfPTable(1);
                //actual width of table in points
                tablen.TotalWidth = 500f;//216f;
                //fix the absolute width of the table
                tablen.LockedWidth = true;
                float[] height = new float[] { 20f };
                //relative col widths in proportions -  2/3 & 1/3 
                float[] widthw = new float[] { 2f };
                tablen.SetWidths(widthw);
                tablen.HorizontalAlignment = -50;
                //leave a gap before and after the table
                tablen.SpacingBefore = 20f;
                
                PdfPCell cell2 = new PdfPCell(new Phrase("\n Declaration by the submitting Inventor: \n\n", new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.BOLD)));

                cell2.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell2.BackgroundColor = new iTextSharp.text.BaseColor(233, 233, 233);
                tablen.AddCell(cell2);
                doc.Add(tablen);


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
                tablen2.SpacingBefore = 0;
                tablen2.SpacingAfter = 0f;
                //PdfPCell cell2 = new PdfPCell(new Phrase("ITEM"));
                PdfPCell cell22 = new PdfPCell(new Phrase("", new iTextSharp.text.Font(iTextSharp.text.Font.NORMAL, 10f, iTextSharp.text.Font.BOLD)));
                cell22.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell22.BackgroundColor = new iTextSharp.text.BaseColor(233, 233, 233);
                tablen2.AddCell(cell22);
                PdfPCell cell222 = new PdfPCell(new Phrase("The patent grant is an outcome of my/our innovative work. The work described is Novel. I/We have taken due care to ensure that the patent does not infringe upon the existing patent and/or is not a part of existing literature or prior art..", new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.NORMAL)));
                cell222.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell222.BackgroundColor = new iTextSharp.text.BaseColor(233, 233, 233);
                tablen2.AddCell(cell222);
                doc.Add(tablen2);

                PdfPTable tablen3 = new PdfPTable(5);
                //actual width of table in points
                tablen3.TotalWidth = 500f;//216f;
                //fix the absolute width of the table
                tablen3.LockedWidth = true;
                //relative col widths in proportions -  2/3 & 1/3 
                float[] widthw3 = new float[] { 1f, 3f, 8f, 2.5f, 6.5f };
                tablen3.SetWidths(widthw3);
                tablen3.HorizontalAlignment = -50;
                //leave a gap before and after the table
                //tablen.SpacingBefore = 20f;
                tablen3.SpacingAfter = 0;
                PdfPCell cell231 = new PdfPCell(new Phrase("\n", new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.NORMAL)));
                cell231.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell231.BackgroundColor = new iTextSharp.text.BaseColor(233, 233, 233);
                tablen3.AddCell(cell231);
                //

                PatentBusiness ob3 = new PatentBusiness();
                Patent u3 = new Patent();

                u3 = ob3.Get_CreatedBy(PatentID);
                string PatentID1 = u3.Created_By;

                u3 = ob3.Get_CreatedName(PatentID1);
                string names = u3.CreatedName;
                u3 = ob3.getPatent_Author_Details(PatentID1);


                //PatentBusiness ob_pat = new PatentBusiness();
                //Patent u31 = new Patent();
              //  u3 = ob3.get_Inventor_Details(PatentID);
                PdfPCell cell232 = new PdfPCell(new Phrase("\n Name", new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.NORMAL)));
                cell232.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell232.BackgroundColor = new iTextSharp.text.BaseColor(233, 233, 233);
                tablen3.AddCell(cell232);
                PdfPCell cell23211 = new PdfPCell(new Phrase("\n " + names, new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.NORMAL)));
                cell23211.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell23211.BackgroundColor = new iTextSharp.text.BaseColor(233, 233, 233);
                tablen3.AddCell(cell23211);
                PdfPCell cell1 = new PdfPCell(new Phrase("\nDesignation: ", new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.NORMAL)));
                cell1.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell1.BackgroundColor = new iTextSharp.text.BaseColor(233, 233, 233);
                tablen3.AddCell(cell1);
                PdfPCell cell3 = new PdfPCell(new Phrase("\n ", new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.NORMAL)));
                cell3.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell3.BackgroundColor = new iTextSharp.text.BaseColor(233, 233, 233);
                tablen3.AddCell(cell3);             
                doc.Add(tablen3);
//
                PdfPTable tablen31 = new PdfPTable(5);             
                tablen31.TotalWidth = 500f;//216f;                
                tablen31.LockedWidth = true;
                //relative col widths in proportions -  2/3 & 1/3 
                float[] widthw31 = new float[] { 1f, 3f, 8f, 2.5f, 6.5f };
                tablen31.SetWidths(widthw31);
                tablen31.HorizontalAlignment = -50;
                //leave a gap before and after the table
                //tablen.SpacingBefore = 20f;
                tablen3.SpacingAfter = 0;
                PdfPCell cell2311 = new PdfPCell(new Phrase("\n", new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.NORMAL)));
                cell2311.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell2311.BackgroundColor = new iTextSharp.text.BaseColor(233, 233, 233);
                tablen31.AddCell(cell2311);
                //PdfPCell cell2 = new PdfPCell(new Phrase("ITEM"));
                PdfPCell cellD = new PdfPCell(new Phrase("\n Department: ", new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.NORMAL)));
                cellD.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cellD.BackgroundColor = new iTextSharp.text.BaseColor(233, 233, 233);
                tablen31.AddCell(cellD);
                PdfPCell cellD1 = new PdfPCell(new Phrase("\n " + u3.dep_name, new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.NORMAL)));
                cellD1.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cellD1.BackgroundColor = new iTextSharp.text.BaseColor(233, 233, 233);
                tablen31.AddCell(cellD1);
                PdfPCell cell23111 = new PdfPCell(new Phrase("\nInstitution:", new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.NORMAL)));
                cell23111.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell23111.BackgroundColor = new iTextSharp.text.BaseColor(233, 233, 233);
                tablen31.AddCell(cell23111);
                PdfPCell cell2321 = new PdfPCell(new Phrase("\n " + u3.institution_name, new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.NORMAL)));
                cell2321.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell2321.BackgroundColor = new iTextSharp.text.BaseColor(233, 233, 233);
                tablen31.AddCell(cell2321);               
                doc.Add(tablen31);

                PdfPTable tablen311 = new PdfPTable(5);
                //actual width of table in points
                tablen311.TotalWidth = 500f;//216f;
                //fix the absolute width of the table
                tablen311.LockedWidth = true;
                //relative col widths in proportions -  2/3 & 1/3 
                float[] widthw311 = new float[] { 1f, 1.5f, 11f, 4f, 5f };
                tablen311.SetWidths(widthw311);
                tablen311.HorizontalAlignment = -50;
                //leave a gap before and after the table
                //tablen.SpacingBefore = 20f;
                tablen311.SpacingAfter = 0;
                PdfPCell cell231111 = new PdfPCell(new Phrase("\n \n \n", new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.NORMAL)));
                cell231111.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell23111.BackgroundColor = new iTextSharp.text.BaseColor(233, 233, 233);
                tablen311.AddCell(cell23111);
                //PdfPCell cell2 = new PdfPCell(new Phrase("ITEM"));
                PdfPCell cell23a = new PdfPCell(new Phrase("\n \n \n Name:", new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.NORMAL)));
                cell23a.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell23a.BackgroundColor = new iTextSharp.text.BaseColor(233, 233, 233);
                tablen311.AddCell(cell23a);
             
                //doc.Add(tablen311);
                PdfPTable tablen12 = new PdfPTable(5);
                //actual width of table in points
                tablen12.TotalWidth = 500f;//216f;
                //fix the absolute width of the table
                tablen12.LockedWidth = true;
                //relative col widths in proportions -  2/3 & 1/3 
                float[] widthw32 = new float[] { 1f, 2.5f, 7f, 3.5f, 4.5f };
                tablen12.SetWidths(widthw32);
                tablen12.HorizontalAlignment = -50;
                //leave a gap before and after the table
               // tablen12.SpacingBefore = 0;
                tablen12.SpacingAfter = 10f;
               // PdfPCell cell21 = new PdfPCell(new Phrase("ITEM"));
                PdfPCell cell2a = new PdfPCell(new Phrase("\n", new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.NORMAL)));
                cell2a.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell2a.BackgroundColor = new iTextSharp.text.BaseColor(233, 233, 233);
                tablen12.AddCell(cell2a);
                PdfPCell cell11 = new PdfPCell(new Phrase("\nEmail Id:", new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.NORMAL)));
                cell11.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell11.BackgroundColor = new iTextSharp.text.BaseColor(233, 233, 233);
                tablen12.AddCell(cell11);
                PdfPCell cell31 = new PdfPCell(new Phrase("\n " + u3.emailid, new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.NORMAL)));
                cell31.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cell31.BackgroundColor = new iTextSharp.text.BaseColor(233, 233, 233);
                tablen12.AddCell(cell31);
                PdfPCell cellr = new PdfPCell(new Phrase("\nSignature with date:", new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.NORMAL)));
                cellr.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cellr.BackgroundColor = new iTextSharp.text.BaseColor(233, 233, 233);
                tablen12.AddCell(cellr);
                PdfPCell cellr1 = new PdfPCell(new Phrase("\n\n", new iTextSharp.text.Font(iTextSharp.text.Font.NORMAL, 10f, iTextSharp.text.Font.NORMAL)));
                cellr1.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cellr1.BackgroundColor = new iTextSharp.text.BaseColor(233, 233, 233);
                tablen12.AddCell(cellr1);  
                doc.Add(tablen12);

                PdfPTable OfficeUse = new PdfPTable(2);
                //actual width of table in points
                OfficeUse.TotalWidth = 500f;//216f;
                //fix the absolute width of the table
                OfficeUse.LockedWidth = true;
                //relative col widths in proportions -  2/3 & 1/3 
                float[] OfficeUseW = new float[] { 2f, 5f };
                OfficeUse.SetWidths(OfficeUseW);
                OfficeUse.HorizontalAlignment = 250;
                //leave a gap before and after the table
                //tablen.SpacingBefore = 20f;
                OfficeUse.SpacingAfter = 0f;
                PdfPCell OfficeUseCell1 = new PdfPCell(new Phrase("\n\n\n", new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.BOLD)));
                OfficeUseCell1.Right = 50f;
                OfficeUseCell1.Border = iTextSharp.text.Rectangle.NO_BORDER;
                OfficeUseCell1.BackgroundColor = new iTextSharp.text.BaseColor(233, 233, 233);
                OfficeUse.AddCell(OfficeUseCell1);
                //PdfPCell cell2 = new PdfPCell(new Phrase("ITEM"));
                PdfPCell OfficeUseCell = new PdfPCell(new Phrase("\n For the use of forwarding office only: \n\n", new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.BOLD)));
                OfficeUseCell.Right = 50f;
                OfficeUseCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
                OfficeUseCell.BackgroundColor = new iTextSharp.text.BaseColor(233, 233, 233);
                OfficeUse.AddCell(OfficeUseCell);
                doc.Add(OfficeUse);

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
                OfficeUse.SpacingAfter = 0;
                PdfPCell OfficeUseCell11 = new PdfPCell(new Phrase("\n\n\n Name & Signature of the Guide/HoD/HoI", new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.BOLD)));
                OfficeUseCell11.Right = 50f;
                OfficeUseCell11.Border = iTextSharp.text.Rectangle.NO_BORDER;
                OfficeUseCell11.BackgroundColor = new iTextSharp.text.BaseColor(233, 233, 233);
                OfficeUse1.AddCell(OfficeUseCell11);
                PdfPCell OfficeUseCel2 = new PdfPCell(new Phrase("\n\n\n", new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.BOLD)));
                OfficeUseCel2.Right = 50f;
                OfficeUseCel2.Border = iTextSharp.text.Rectangle.NO_BORDER;
                OfficeUseCel2.BackgroundColor = new iTextSharp.text.BaseColor(233, 233, 233);
                OfficeUse1.AddCell(OfficeUseCel2);
                PdfPCell OfficeUseCel5 = new PdfPCell(new Phrase("\n\n\n Name & Signature of Head of Institution", new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.BOLD)));
                OfficeUseCel5.Right = 50f;
                OfficeUseCel5.Border = iTextSharp.text.Rectangle.NO_BORDER;
                OfficeUseCel5.BackgroundColor = new iTextSharp.text.BaseColor(233, 233, 233);
                OfficeUse1.AddCell(OfficeUseCel5);
                doc.Add(OfficeUse1);

                PdfPTable t1 = new PdfPTable(2);
                //actual width of table in points
                t1.TotalWidth = 500f;//216f;
                //fix the absolute width of the table
                t1.LockedWidth = true;
                //relative col widths in proportions -  2/3 & 1/3 
                float[] w1 = new float[] { 0.1f, 2f };
                t1.SetWidths(w1);
                t1.HorizontalAlignment = -50;
                //leave a gap before and after the table
                t1.SpacingBefore = 0;
                t1.SpacingAfter = 10f;
                //PdfPCell cell2 = new PdfPCell(new Phrase("ITEM"));
                PdfPCell c1 = new PdfPCell(new Phrase("", new iTextSharp.text.Font(iTextSharp.text.Font.NORMAL, 10f, iTextSharp.text.Font.NORMAL)));
                c1.Border = iTextSharp.text.Rectangle.NO_BORDER;
                c1.BackgroundColor = new iTextSharp.text.BaseColor(233, 233, 233);
                t1.AddCell(c1);
                PdfPCell c2 = new PdfPCell(new Phrase("\n", new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.NORMAL)));
                c2.Border = iTextSharp.text.Rectangle.NO_BORDER;
                c2.BackgroundColor = new iTextSharp.text.BaseColor(233, 233, 233);
                t1.AddCell(c2);
                doc.Add(t1);
                PdfPTable OfficeUsex = new PdfPTable(2);
                //actual width of table in points
                OfficeUsex.TotalWidth = 500f;//216f;
                //fix the absolute width of the table
                OfficeUsex.LockedWidth = true;
                //relative col widths in proportions -  2/3 & 1/3 
                float[] OfficeUseWw = new float[] { 2f, 5f };
                OfficeUsex.SetWidths(OfficeUseWw);
                OfficeUsex.HorizontalAlignment = 250;
                //leave a gap before and after the table
                //tablen.SpacingBefore = 20f;
                OfficeUsex.SpacingAfter = 0f;
                PdfPCell OfficeUseCell1x = new PdfPCell(new Phrase("\n\n\n", new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.BOLD)));
                OfficeUseCell1x.Right = 50f;
                OfficeUseCell1x.Border = iTextSharp.text.Rectangle.NO_BORDER;
                OfficeUseCell1x.BackgroundColor = new iTextSharp.text.BaseColor(233, 233, 233);
                OfficeUsex.AddCell(OfficeUseCell1x);
                //PdfPCell cell2 = new PdfPCell(new Phrase("ITEM"));
                PdfPCell OfficeUseCell2x = new PdfPCell(new Phrase("\n For the use of Directorate of Research only: \n\n", new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.BOLD)));
                OfficeUseCell2x.Right = 50f;
                OfficeUseCell2x.Border = iTextSharp.text.Rectangle.NO_BORDER;
                OfficeUseCell2x.BackgroundColor = new iTextSharp.text.BaseColor(233, 233, 233);
                OfficeUsex.AddCell(OfficeUseCell2x);
                doc.Add(OfficeUsex);

                PdfPTable tab = new PdfPTable(2);
                //actual width of table in points
                tab.TotalWidth = 500f;//216f;
                //fix the absolute width of the table
                tab.LockedWidth = true;
                //relative col widths in proportions -  2/3 & 1/3 
                float[] w2 = new float[] { 0.1f, 2f };
                tab.SetWidths(w2);
                tab.HorizontalAlignment = -50;
                //leave a gap before and after the table
                tab.SpacingBefore = 0;
                tab.SpacingAfter = 0;
                //PdfPCell cell2 = new PdfPCell(new Phrase("ITEM"));
                PdfPCell cellr21 = new PdfPCell(new Phrase("", new iTextSharp.text.Font(iTextSharp.text.Font.NORMAL, 10f, iTextSharp.text.Font.NORMAL)));
                cellr21.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cellr21.BackgroundColor = new iTextSharp.text.BaseColor(233, 233, 233);
                tab.AddCell(cellr21);
                PdfPCell cellr11 = new PdfPCell(new Phrase("\n Total number of points:\n\n", new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.NORMAL)));
                cellr11.Border = iTextSharp.text.Rectangle.NO_BORDER;
                cellr11.BackgroundColor = new iTextSharp.text.BaseColor(233, 233, 233);
                tab.AddCell(cellr11);
                doc.Add(tab);

                PdfPTable OfficeUse12 = new PdfPTable(3);
                //actual width of table in points
                OfficeUse12.TotalWidth = 500f;//216f;
                //fix the absolute width of the table
                OfficeUse12.LockedWidth = true;
                //relative col widths in proportions -  2/3 & 1/3 
                float[] OfficeUseW12 = new float[] { 7f, 5.3f, 7f };
                OfficeUse12.SetWidths(OfficeUseW1);
                OfficeUse12.HorizontalAlignment = 250;
                //leave a gap before and after the table
                //tablen.SpacingBefore = 20f;
                OfficeUse12.SpacingAfter = 0;
                PdfPCell OfficeUseCell112 = new PdfPCell(new Phrase("\n Coordinator - TTO ", new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.BOLD)));
                OfficeUseCell112.Right = 50f;
                OfficeUseCell112.Border = iTextSharp.text.Rectangle.NO_BORDER;
                OfficeUseCell112.BackgroundColor = new iTextSharp.text.BaseColor(233, 233, 233);
                OfficeUse12.AddCell(OfficeUseCell112);
                PdfPCell OfficeUseCel22 = new PdfPCell(new Phrase("\n", new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.BOLD)));
                OfficeUseCel22.Right = 50f;
                OfficeUseCel22.Border = iTextSharp.text.Rectangle.NO_BORDER;
                OfficeUseCel22.BackgroundColor = new iTextSharp.text.BaseColor(233, 233, 233);
                OfficeUse12.AddCell(OfficeUseCel22);
                PdfPCell OfficeUseCel52 = new PdfPCell(new Phrase("\n Director Research – (Health Sciences / Technical)", new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.BOLD)));
                OfficeUseCel52.Right = 50f;
                OfficeUseCel52.Border = iTextSharp.text.Rectangle.NO_BORDER;
                OfficeUseCel52.BackgroundColor = new iTextSharp.text.BaseColor(233, 233, 233);
                OfficeUse12.AddCell(OfficeUseCel52);
                doc.Add(OfficeUse12);

                PdfPTable OfficeUse121 = new PdfPTable(3);
                //actual width of table in points
                OfficeUse121.TotalWidth = 500f;//216f;
                //fix the absolute width of the table
                OfficeUse121.LockedWidth = true;
                //relative col widths in proportions -  2/3 & 1/3 
                float[] OfficeUseW121 = new float[] { 7f, 5.3f, 7f };
                OfficeUse121.SetWidths(OfficeUseW1);
                OfficeUse121.HorizontalAlignment = 250;
                //leave a gap before and after the table
                //tablen.SpacingBefore = 20f;
                OfficeUse121.SpacingAfter = 0;
                PdfPCell OfficeUseCell1121 = new PdfPCell(new Phrase("\n DoR,MAHE (signature with date) ", new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.NORMAL)));
                OfficeUseCell1121.Right = 50f;
                OfficeUseCell1121.Border = iTextSharp.text.Rectangle.NO_BORDER;
                OfficeUseCell1121.BackgroundColor = new iTextSharp.text.BaseColor(233, 233, 233);
                OfficeUse121.AddCell(OfficeUseCell1121);
                PdfPCell OfficeUseCel221 = new PdfPCell(new Phrase("\n", new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.NORMAL)));
                OfficeUseCel221.Right = 50f;
                OfficeUseCel221.Border = iTextSharp.text.Rectangle.NO_BORDER;
                OfficeUseCel221.BackgroundColor = new iTextSharp.text.BaseColor(233, 233, 233);
                OfficeUse121.AddCell(OfficeUseCel221);
                PdfPCell OfficeUseCel521 = new PdfPCell(new Phrase("\n MAHE (signature with date)", new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.NORMAL)));
                OfficeUseCel521.Right = 50f;
                OfficeUseCel521.Border = iTextSharp.text.Rectangle.NO_BORDER;
                OfficeUseCel521.BackgroundColor = new iTextSharp.text.BaseColor(233, 233, 233);
                OfficeUse121.AddCell(OfficeUseCel521);
                doc.Add(OfficeUse121);

                //Paragraph paras = null;
                //bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
                //times = new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.BOLD);
                //paras = new Paragraph("STN 312", times);
                //paras.SpacingBefore = 5f;

                //paras.IndentationLeft = 380f;
                //doc.Add(paras);
                doc.Close();

//student
                PatentBusiness obj3 = new PatentBusiness();
             //   string stupublication = obj3.CheckIsStudentPublication(PatentID);
                //if (stupublication == "Y")
                //{
                    DataSet data = obj3.SelectStudentAuthorDetail(PatentID);
                    int rowcount = data.Tables[0].Rows.Count;
                    for (int i = 0; i < rowcount; i++)
                    {
                        string regno = ds.Tables[0].Rows[i]["EmployeeCode"].ToString();
                        string name = ds.Tables[0].Rows[i]["InvestigatorName"].ToString();

                        Document doc1 = new Document(PageSize.A4, 88f, 88f, 10f, 10f);
                        iTextSharp.text.Font NormalFont1 = FontFactory.GetFont("Arial", 12, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                        string filelocationpath1 = "";
                        string actualfilenamewithtime1 = "";
                        string servername1 = ConfigurationManager.AppSettings["ServerName"].ToString();
                        string domainame1 = ConfigurationManager.AppSettings["DomainName"].ToString();
                        string username1 = ConfigurationManager.AppSettings["UserName"].ToString();
                        string password1 = ConfigurationManager.AppSettings["Password"].ToString();
                        string mainpath1 = ConfigurationManager.AppSettings["PdfPath"].ToString();
                        actualfilenamewithtime1 = PatentID + "_" + regno + ".pdf";

                        // Create a new PdfWriter object, specifying the output stream
                        string gpath1 = Path.Combine(mainpath, "PatentPrintEvaluation");
                        if (Directory.Exists(gpath1))
                        {
                            filelocationpath1 = Path.Combine(gpath1, actualfilenamewithtime1);

                            var output = new FileStream(filelocationpath1, FileMode.Create);
                            var writer = PdfWriter.GetInstance(doc1, output);

                        }
                        else
                        {
                            Directory.CreateDirectory(gpath1);
                            string path = Path.Combine(gpath1, actualfilenamewithtime1);
                            var output = new FileStream(filelocationpath1, FileMode.Create);
                            var writer = PdfWriter.GetInstance(doc1, output);
                        }
                        doc1.Open();


                        Paragraph paraevaluation = null;
                        iTextSharp.text.Font timesevaluation = null;
                        BaseFont bfTimesevaluation = null;
                        bfTimesevaluation = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
                        timesevaluation = new iTextSharp.text.Font(bfTimesevaluation, 11, iTextSharp.text.Font.BOLD);
                        paraevaluation = new Paragraph("Bank Account Details of Student", timesevaluation);

                        paraevaluation.SpacingBefore = 15f;
                        paraevaluation.IndentationLeft = 150f;
                        doc1.Add(paraevaluation);

                        PdfPTable table1 = new PdfPTable(2);
                        table1.SpacingBefore = 10f;
                        table1.TotalWidth = 320f;
                        table1.LockedWidth = true;
                        table1.SpacingBefore = 10f;
                        table1.SpacingAfter = 40f;
                        Paragraph paraevaluation1 = null;

                        iTextSharp.text.Font times2 = null;
                        times2 = new iTextSharp.text.Font(bfTimesevaluation, 10, iTextSharp.text.Font.BOLD);
                        //paraevaluation1 = new Paragraph("•", times1);
                        paraevaluation1 = new Paragraph("", times1);
                        paraevaluation1.SpacingBefore = 40f;// spacing from the top
                        paraevaluation1.SpacingAfter = 10f; // spacing from the bottom
                        paraevaluation1.IndentationLeft = -30f; // left space 
                        doc1.Add(paraevaluation1);
                        times1 = new iTextSharp.text.Font(bfTimesevaluation, 10, iTextSharp.text.Font.BOLD);
                        paraevaluation1 = new Paragraph("Registration Number:  ", times1);

                        paraevaluation1.SpacingBefore = -25f;// spacing from the top
                        paraevaluation1.SpacingAfter = 5f; // spacing from the bottom
                        paraevaluation1.IndentationLeft = -30f; // left space 

                        doc1.Add(paraevaluation1);

                        times1 = new iTextSharp.text.Font(bfTimesevaluation, 10, iTextSharp.text.Font.NORMAL);
                        paraevaluation1 = new Paragraph("" + regno, times1);

                        paraevaluation1.SpacingBefore = -20f;// spacing from the top
                        paraevaluation1.SpacingAfter = 5f; // spacing from the bottom
                        paraevaluation1.IndentationLeft = 60f; // left space 

                        doc1.Add(paraevaluation1);


                        Paragraph paraevaluation2 = null;
                        times2 = new iTextSharp.text.Font(bfTimesevaluation, 10, iTextSharp.text.Font.BOLD);
                        //paraevaluation2 = new Paragraph("•", times1);
                        paraevaluation2 = new Paragraph("", times1);
                        paraevaluation2.SpacingBefore = 40f;// spacing from the top
                        paraevaluation2.SpacingAfter = 10f; // spacing from the bottom
                        paraevaluation2.IndentationLeft = -20f; // left space 
                        doc1.Add(paraevaluation2);
                        times1 = new iTextSharp.text.Font(bfTimesevaluation, 10, iTextSharp.text.Font.BOLD);
                        paraevaluation2 = new Paragraph("Details of Account Holder", times1);

                        paraevaluation2.SpacingBefore = -25f;// spacing from the top
                        paraevaluation2.SpacingAfter = 5f; // spacing from the bottom
                        paraevaluation2.IndentationLeft = -30f; // left space 
                        doc1.Add(paraevaluation2);


                        //times1 = new iTextSharp.text.Font(bfTimesevaluation, 10, iTextSharp.text.Font.NORMAL);
                        //paraevaluation1 = new Paragraph("" + name, times1);

                        //paraevaluation1.SpacingBefore = -20f;// spacing from the top
                        //paraevaluation1.SpacingAfter = 5f; // spacing from the bottom
                        //paraevaluation1.IndentationLeft = 70f; // left space 

                        //doc1.Add(paraevaluation1);

                        cell = PhraseCell(new Phrase("", FontFactory.GetFont("Arial", 14, iTextSharp.text.Font.BOLD, BaseColor.BLACK)), PdfPCell.ALIGN_CENTER);
                        cell.FixedHeight = 70f;
                        cell.BorderColor = new BaseColor(0, 0, 0);
                        PdfPTable tabData2 = new PdfPTable(2);

                        tabData2.TotalWidth = 500f;
                        tabData2.LockedWidth = true;
                        tabData2.SpacingBefore = 20f;
                        times = new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.NORMAL);
                        tabData2.AddCell(new PdfPCell(new Phrase("Name of Account Holder", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD, BaseColor.BLACK))));
                        tabData2.AddCell(new PdfPCell(new Phrase("" + name, FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.NORMAL, BaseColor.BLACK))));

                        tabData2.AddCell(new PdfPCell(new Phrase("Account Number of above account", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD, BaseColor.BLACK))));
                        tabData2.AddCell(new PdfPCell(new Phrase("", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.NORMAL, BaseColor.BLACK))));

                        tabData2.AddCell(new PdfPCell(new Phrase("Bank Name", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD, BaseColor.BLACK))));
                        tabData2.AddCell(new PdfPCell(new Phrase("", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.NORMAL, BaseColor.BLACK))));

                        tabData2.AddCell(new PdfPCell(new Phrase("Branch name & Code", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD, BaseColor.BLACK))));
                        tabData2.AddCell(new PdfPCell(new Phrase("", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.NORMAL, BaseColor.BLACK))));

                        tabData2.AddCell(new PdfPCell(new Phrase("Branch Address", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD, BaseColor.BLACK))));
                        tabData2.AddCell(new PdfPCell(new Phrase(" ", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD, BaseColor.BLACK))));

                        tabData2.AddCell(new PdfPCell(new Phrase("MICR Code", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD, BaseColor.BLACK))));
                        tabData2.AddCell(new PdfPCell(new Phrase("    ", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.NORMAL, BaseColor.BLACK))));

                        tabData2.AddCell(new PdfPCell(new Phrase("IFS Code No. of the Branch ", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD, BaseColor.BLACK))));
                        tabData2.AddCell(new PdfPCell(new Phrase(" ", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.NORMAL, BaseColor.BLACK))));

                        tabData2.AddCell(new PdfPCell(new Phrase("Complete Contact Address of the student ", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD, BaseColor.BLACK))));
                        tabData2.AddCell(new PdfPCell(new Phrase(" ", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.NORMAL, BaseColor.BLACK))));

                        tabData2.AddCell(new PdfPCell(new Phrase("Handheld/Telephone No.", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD, BaseColor.BLACK))));
                        tabData2.AddCell(new PdfPCell(new Phrase("", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.NORMAL, BaseColor.BLACK))));

                        tabData2.AddCell(new PdfPCell(new Phrase("E-Mail", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD, BaseColor.BLACK))));
                        tabData2.AddCell(new PdfPCell(new Phrase(" ", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.NORMAL, BaseColor.BLACK))));

                        //cell.Rowspan = 2;
                        // cell.Colspan = 2;
                        tabData2.AddCell(cell);
                        tabData2.SpacingAfter = 30f;

                        doc1.Add(tabData2);

                        PdfPTable OfficeUse2 = new PdfPTable(2);
                        //actual width of table in points
                        OfficeUse2.TotalWidth = 500f;//216f;
                        //fix the absolute width of the table
                        OfficeUse2.LockedWidth = true;
                        //relative col widths in proportions -  2/3 & 1/3 
                        float[] OfficeUseW2 = new float[] { 2f, 5f };
                        OfficeUse2.SetWidths(OfficeUseW2);
                        OfficeUse2.HorizontalAlignment = 250;
                        //leave a gap before and after the table
                        //tablen.SpacingBefore = 20f;
                        OfficeUse2.SpacingAfter = 0f;
                        PdfPCell OfficeUseCell12 = new PdfPCell(new Phrase("\n\n\n", new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.BOLD)));
                        OfficeUseCell12.Right = 50f;
                        OfficeUseCell12.Border = iTextSharp.text.Rectangle.NO_BORDER;
                        OfficeUseCell12.BackgroundColor = new iTextSharp.text.BaseColor(233, 233, 233);
                        OfficeUse2.AddCell(OfficeUseCell12);
                        //PdfPCell cell2 = new PdfPCell(new Phrase("ITEM"));
                        PdfPCell OfficeUseCell2 = new PdfPCell(new Phrase("\n For the use of forwarding office only: \n\n", new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.BOLD)));
                        OfficeUseCell2.Right = 50f;
                        OfficeUseCell2.Border = iTextSharp.text.Rectangle.NO_BORDER;
                        OfficeUseCell2.BackgroundColor = new iTextSharp.text.BaseColor(233, 233, 233);
                        OfficeUse2.AddCell(OfficeUseCell2);
                        doc1.Add(OfficeUse2);
                        PdfPTable OfficeUse123 = new PdfPTable(3);
                        //actual width of table in points
                        OfficeUse123.TotalWidth = 500f;//216f;
                        //fix the absolute width of the table
                        OfficeUse123.LockedWidth = true;
                        //relative col widths in proportions -  2/3 & 1/3 
                        float[] OfficeUseW123 = new float[] { 7f, 5.3f, 7f };
                        OfficeUse123.SetWidths(OfficeUseW123);
                        OfficeUse123.HorizontalAlignment = 250;
                        //leave a gap before and after the table
                        //tablen.SpacingBefore = 20f;
                        OfficeUse2.SpacingAfter = 0;
                        PdfPCell OfficeUseCell1123 = new PdfPCell(new Phrase("\n\n\n Name & Signature of Head of Department", new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.BOLD)));
                        OfficeUseCell1123.Right = 50f;
                        OfficeUseCell1123.Border = iTextSharp.text.Rectangle.NO_BORDER;
                        OfficeUseCell1123.BackgroundColor = new iTextSharp.text.BaseColor(233, 233, 233);
                        OfficeUse123.AddCell(OfficeUseCell1123);
                        PdfPCell OfficeUseCel234 = new PdfPCell(new Phrase("\n\n\n", new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.BOLD)));
                        OfficeUseCel234.Right = 50f;
                        OfficeUseCel234.Border = iTextSharp.text.Rectangle.NO_BORDER;
                        OfficeUseCel234.BackgroundColor = new iTextSharp.text.BaseColor(233, 233, 233);
                        OfficeUse123.AddCell(OfficeUseCel234);
                        PdfPCell OfficeUseCel45 = new PdfPCell(new Phrase("\n\n\n Name & Signature of Head of Institution", new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.BOLD)));
                        OfficeUseCel45.Right = 50f;
                        OfficeUseCel45.Border = iTextSharp.text.Rectangle.NO_BORDER;
                        OfficeUseCel45.BackgroundColor = new iTextSharp.text.BaseColor(233, 233, 233);
                        OfficeUse123.AddCell(OfficeUseCel5);
                        doc1.Add(OfficeUse123);
                        PdfPTable t4 = new PdfPTable(2);
                        //actual width of table in points
                        t4.TotalWidth = 500f;//216f;
                        //fix the absolute width of the table
                        t4.LockedWidth = true;
                        //relative col widths in proportions -  2/3 & 1/3 
                        float[] w4 = new float[] { 0.1f, 2f };
                        t4.SetWidths(w4);
                        t4.HorizontalAlignment = -50;
                        //leave a gap before and after the table
                        t4.SpacingBefore = 0;
                        t4.SpacingAfter = 10f;
                        //PdfPCell cell2 = new PdfPCell(new Phrase("ITEM"));
                        PdfPCell c4 = new PdfPCell(new Phrase("", new iTextSharp.text.Font(iTextSharp.text.Font.NORMAL, 10f, iTextSharp.text.Font.NORMAL)));
                        c4.Border = iTextSharp.text.Rectangle.NO_BORDER;
                        c4.BackgroundColor = new iTextSharp.text.BaseColor(233, 233, 233);
                        t4.AddCell(c4);
                        PdfPCell c5 = new PdfPCell(new Phrase("\n", new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.NORMAL)));
                        c5.Border = iTextSharp.text.Rectangle.NO_BORDER;
                        c5.BackgroundColor = new iTextSharp.text.BaseColor(233, 233, 233);
                        t4.AddCell(c5);
                        doc1.Add(t4);
                        PdfPTable OfficeUsex1 = new PdfPTable(2);
                        //actual width of table in points
                        OfficeUsex1.TotalWidth = 500f;//216f;
                        //fix the absolute width of the table
                        OfficeUsex1.LockedWidth = true;
                        //relative col widths in proportions -  2/3 & 1/3 
                        float[] OfficeUseWw2 = new float[] { 2f, 5f };
                        OfficeUsex1.SetWidths(OfficeUseWw2);
                        OfficeUsex1.HorizontalAlignment = 250;
                        //leave a gap before and after the table
                        //tablen.SpacingBefore = 20f;
                        OfficeUsex.SpacingAfter = 0f;
                        PdfPCell OfficeUseCell1x1 = new PdfPCell(new Phrase("\n\n\n", new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.BOLD)));
                        OfficeUseCell1x1.Right = 50f;
                        OfficeUseCell1x1.Border = iTextSharp.text.Rectangle.NO_BORDER;
                        OfficeUseCell1x1.BackgroundColor = new iTextSharp.text.BaseColor(233, 233, 233);
                        OfficeUsex1.AddCell(OfficeUseCell1x1);
                        //PdfPCell cell2 = new PdfPCell(new Phrase("ITEM"));
                        PdfPCell OfficeUseCell2x1 = new PdfPCell(new Phrase("\n For the use of Directorate of Research only: \n\n", new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.BOLD)));
                        OfficeUseCell2x1.Right = 50f;
                        OfficeUseCell2x1.Border = iTextSharp.text.Rectangle.NO_BORDER;
                        OfficeUseCell2x1.BackgroundColor = new iTextSharp.text.BaseColor(233, 233, 233);
                        OfficeUsex1.AddCell(OfficeUseCell2x1);
                        doc1.Add(OfficeUsex1);
                        PdfPTable tab4 = new PdfPTable(2);
                        //actual width of table in points
                        tab4.TotalWidth = 500f;//216f;
                        //fix the absolute width of the table
                        tab4.LockedWidth = true;
                        //relative col widths in proportions -  2/3 & 1/3 
                        float[] w5 = new float[] { 0.1f, 2f };
                        tab4.SetWidths(w5);
                        tab4.HorizontalAlignment = -50;
                        //leave a gap before and after the table
                        tab4.SpacingBefore = 0;
                        tab4.SpacingAfter = 0;
                        //PdfPCell cell2 = new PdfPCell(new Phrase("ITEM"));
                        PdfPCell cellr214 = new PdfPCell(new Phrase("", new iTextSharp.text.Font(iTextSharp.text.Font.NORMAL, 10f, iTextSharp.text.Font.NORMAL)));
                        cellr214.Border = iTextSharp.text.Rectangle.NO_BORDER;
                        cellr214.BackgroundColor = new iTextSharp.text.BaseColor(233, 233, 233);
                        tab4.AddCell(cellr214);
                        PdfPCell cellr114 = new PdfPCell(new Phrase("\n Total number of points:\n\n", new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.NORMAL)));
                        cellr114.Border = iTextSharp.text.Rectangle.NO_BORDER;
                        cellr114.BackgroundColor = new iTextSharp.text.BaseColor(233, 233, 233);
                        tab4.AddCell(cellr114);
                        doc1.Add(tab4);
                        PdfPTable OfficeUse126 = new PdfPTable(3);
                        //actual width of table in points
                        OfficeUse126.TotalWidth = 500f;//216f;
                        //fix the absolute width of the table
                        OfficeUse126.LockedWidth = true;
                        //relative col widths in proportions -  2/3 & 1/3 
                        float[] OfficeUseW124 = new float[] { 6f, 6f, 9f };
                        OfficeUse126.SetWidths(OfficeUseW124);
                        OfficeUse126.HorizontalAlignment = 250;
                        //leave a gap before and after the table
                        //tablen.SpacingBefore = 20f;
                        OfficeUse126.SpacingAfter = 0;
                        PdfPCell OfficeUseCell1127 = new PdfPCell(new Phrase("\n ", new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.BOLD)));
                        OfficeUseCell1127.Right = 50f;
                        OfficeUseCell1127.Border = iTextSharp.text.Rectangle.NO_BORDER;
                        OfficeUseCell1127.BackgroundColor = new iTextSharp.text.BaseColor(233, 233, 233);
                        OfficeUse126.AddCell(OfficeUseCell1127);
                        PdfPCell OfficeUseCel228 = new PdfPCell(new Phrase("\n", new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.BOLD)));
                        OfficeUseCel228.Right = 50f;
                        OfficeUseCel228.Border = iTextSharp.text.Rectangle.NO_BORDER;
                        OfficeUseCel228.BackgroundColor = new iTextSharp.text.BaseColor(233, 233, 233);
                        OfficeUse126.AddCell(OfficeUseCel228);
                        PdfPCell OfficeUseCel520 = new PdfPCell(new Phrase("\n Director Research – (Health Sciences / Technical) \n\n", new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.BOLD)));
                        OfficeUseCel520.Right = 50f;
                        OfficeUseCel520.Border = iTextSharp.text.Rectangle.NO_BORDER;
                        OfficeUseCel520.BackgroundColor = new iTextSharp.text.BaseColor(233, 233, 233);
                        OfficeUse126.AddCell(OfficeUseCel520);
                        doc1.Add(OfficeUse126);

                        //Paragraph paras1 = null;
                        //bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
                        //times = new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.BOLD);
                        //paras1 = new Paragraph("STN 312", times);
                        //paras1.SpacingBefore = 5f;

                        //paras1.IndentationLeft = 380f;
                        //doc.Add(paras1);
                        doc1.Close();

                    }
                

                PatentBusiness obj4 = new PatentBusiness();
                //string isstudentpublication1 = obj4.CheckIsStudentPublication(PatentID);
                //if (isstudentpublication1 == "Y")
                //{
                    //using (ZipFile zip = new ZipFile())
                    //{
                    //    zip.AlternateEncodingUsage = ZipOption.AsNecessary;
                    //    zip.AddDirectoryByName("Files");
                    //    string[] filePaths = Directory.GetFiles(path1);
                    //    foreach (string filePath in filePaths)
                    //    {
                    //        zip.AddFile(filePath, "Files");

                    //    }
                    //    HttpContext.Current.Response.Clear();
                    //    HttpContext.Current.Response.BufferOutput = false;
                    //    string zipName = String.Format("PrintEvaluation_{0}.zip", DateTime.Now.ToString("yyyy-MMM-dd-HHmmss"));
                    //    HttpContext.Current.Response.ContentType = "application/zip";
                    //    HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=" + zipName);
                    //    zip.Save(HttpContext.Current.Response.OutputStream);
                    //    HttpContext.Current.Response.End();
                    //}
                
              

                FileInfo myfile = new FileInfo(filelocationpath);
                if (myfile.Exists)
                {
                    log.Info("Evaluation Form Generated Successfully,  for Patent ID: " + PatentID + " by " + HttpContext.Current.Session["UserName"]);
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
            log.Info("Evaluation Form generation failed,  for Patent ID: " + PatentID + " by " + HttpContext.Current.Session["UserName"]);
            log.Error("Error: " + e);
        }

        //User u = new User();
        //u.PatientID = PatientID;
        //// int VisitCount = obj.fnVisitCount(PatientID, info);
        //u.FilePath = filelocationpath;
        //obj.fnPatOrthoAuxiVisitDet(u);

    }
}