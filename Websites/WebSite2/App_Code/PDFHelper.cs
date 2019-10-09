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
/// Summary description for PDFHelper
/// </summary>
public class PDFHelper
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




    public void pdfGenerate(string PatientID, string type)
    {
        string pdftype = type;
        string boxid = PatientID;
        string AuthorID = PatientID;

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
        actualfilenamewithtime = PatientID + "_" + "merged" + ".pdf";
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

        //catch (Exception e)
        //{
        //}
        //iTextSharp.text.Image jpg1 = iTextSharp.text.Image.GetInstance(HttpContext.Current.Request.MapPath("~/Images/arrow.jpg"));
        //jpg1.Alignment = iTextSharp.text.Image.TEXTWRAP | iTextSharp.text.Image.ALIGN_RIGHT;

        //PdfPCell cell = null;
        //Paragraph para = null;
        //Paragraph para1 = null;
        //iTextSharp.text.Font times = null;
        //iTextSharp.text.Font times1 = null;
        //BaseFont bfTimes = null;
        //bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
        //times = new iTextSharp.text.Font(bfTimes, 11, iTextSharp.text.Font.BOLD);
        //para = new Paragraph("EVALUATION FORM FOR PUBLICATIONS", times);
        //para.SpacingBefore = 550f;
        //para.IndentationLeft = 150f;

        //doc.Open();
        //doc.Add(para);

        //User p5 = new User();
        //Journal_DataObject obj5 = new Journal_DataObject();
        //p5 = obj5.ImagePath();

        ////C:\Program Files\Common Files\Microsoft Shared\DevServer\11.0\mu_logo.jpg
        ////  var logo = iTextSharp.text.Image.GetInstance(Path.GetFileName("~/mu_logo.png"));

        //string path = null;
        //path = p5.imgpath;
        //if (path != null)
        //{
        //    iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(path);
        //    jpg.ScaleToFit(150f, 150f);
        //    jpg.SetAbsolutePosition(58, 780); // left  top
        //    jpg.SpacingAfter = 20f;


        //    doc.Add(jpg);
        //}


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
        iTextSharp.text.Font times = null;
        iTextSharp.text.Font times1 = null;
        BaseFont bfTimes = null;
        bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
        times = new iTextSharp.text.Font(bfTimes, 11, iTextSharp.text.Font.BOLD);
        Journal_DataObject obj6 = new Journal_DataObject();
        PublishData value = new PublishData();
        value = obj6.CheckIsStudentPublication(PatientID);
        if (value.IsStudentAuthor == "Y")
        {
            para = new Paragraph("EVALUATION FORM FOR STUDENT PUBLICATIONS", times);
        }
        else
        {
            para = new Paragraph("EVALUATION FORM FOR PUBLICATIONS", times);
        }       
       
       
        para.SpacingBefore = 15f;
        para.IndentationLeft = 150f;
        doc.Add(para);
        
        if (value.IsStudentAuthor == "Y")
        {
            times1 = new iTextSharp.text.Font(bfTimes, 9, iTextSharp.text.Font.ITALIC);
            para1 = new Paragraph("with effect from July 1, 2016", times1);
            para1.SpacingBefore = 5f;
            para1.Alignment = Element.ALIGN_CENTER;
            para1.IndentationLeft = 80f;
            doc.Add(para1);
        }
        else
        {
            times1 = new iTextSharp.text.Font(bfTimes, 9, iTextSharp.text.Font.ITALIC);
            para1 = new Paragraph("with effect from October 1, 2015", times1);
            para1.SpacingBefore = 5f;
            para1.Alignment = Element.ALIGN_CENTER;
            para1.IndentationLeft = 80f;
            doc.Add(para1);
        }

        if (value.IsStudentAuthor == "Y")
        {
            Paragraph paras = null;
            bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
            times = new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.BOLD);
            paras = new Paragraph("STN 313", times);
            paras.SpacingBefore = 5f;

            paras.IndentationLeft = 380f;
            doc.Add(paras);
        }

        else
        {
            Paragraph paras = null;
            bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
            times = new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.BOLD);
            paras = new Paragraph("STN 311", times);
            paras.SpacingBefore = 5f;

            paras.IndentationLeft = 380f;
            doc.Add(paras);
        }
      



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
        p1 = obj.fnfindPatient(PatientID);
        Journal_DataObject obj2 = new Journal_DataObject();
        p2 = obj.tableDisplay(PatientID);
        times1 = new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.BOLD);
        para1 = new Paragraph("•", times1);
        para1.SpacingBefore = 40f;// spacing from the top
        para1.SpacingAfter = 10f; // spacing from the bottom
        para1.IndentationLeft = -30f; // left space 
        doc.Add(para1);
        times1 = new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.BOLD);
        para1 = new Paragraph("Publication Id:  ", times1);

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
        para1 = new Paragraph("Title of the paper:    ", times1);
        para1.SpacingBefore = -65f;// spacing from the top
        para1.SpacingAfter = -20f; // spacing from the bottom
        para1.IndentationLeft = -20f; // left space 
        doc.Add(para1);
        times1 = new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.NORMAL);
        para1 = new Paragraph("" + p1.TitleWorkItem, times1);
        para1.SpacingBefore = 5f;// spacing from the top
        para1.SpacingAfter = 5f; // spacing from the bottom
        para1.IndentationLeft = 60f; // left space 
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
        PdfPTable tabData = new PdfPTable(6);
        tabData.TotalWidth = 480f;
        //fix the absolute width of the table
        tabData.LockedWidth = true;       
        times = new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.NORMAL);
        float[] widths = new float[] { 1.8f, 3.5f, 3f, 3.5f, 2.5f, 2.5f };
        tabData.AddCell(new PdfPCell(new Phrase(" Sl.No", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD, BaseColor.BLACK))));
        tabData.AddCell(new PdfPCell(new Phrase(" Names of Authors", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD, BaseColor.BLACK))));
        tabData.AddCell(new PdfPCell(new Phrase(" Type of Author", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD, BaseColor.BLACK))));
        if (value.IsStudentAuthor == "Y")
        {
            tabData.AddCell(new PdfPCell(new Phrase(" Department/Course", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD, BaseColor.BLACK))));

        }
        else
        {
            tabData.AddCell(new PdfPCell(new Phrase(" Department", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD, BaseColor.BLACK))));

        }
        if (value.IsStudentAuthor == "Y")
        {
            tabData.AddCell(new PdfPCell(new Phrase("Employee Code/Registration No", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD, BaseColor.BLACK))));

        }
        else
        {
            tabData.AddCell(new PdfPCell(new Phrase("Employee Code", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD, BaseColor.BLACK))));
        }
        tabData.AddCell(new PdfPCell(new Phrase("Corres-Author", FontFactory.GetFont("Arial", 10, iTextSharp.text.Font.BOLD, BaseColor.BLACK))));

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
        
       DataSet dz = obj2.fnfindjournalAccount12(PatientID);
       DataSet ds = null;
       value = obj2.CheckIsStudentPublication(PatientID);
       int yearv = Convert.ToInt16(value.PublishJAYear);
       if (yearv < 2017)
       {
           if (value.IsStudentAuthor == "Y")
           { 
               //if student authored below 2016 publication-displaying faculty authors
               ds = obj2.SelectStudentAuthorDetail(PatientID);
               dz.Merge(ds);
           }
       }
       else
       {
           //if faculty authored above 2017 publication-displaying student+faculty authors
           ds = obj2.SelectStudentAuthorDetail(PatientID);
           dz.Merge(ds);
       }
        int count = dz.Tables[0].Rows.Count;
        for (int i = 0; i < count; i++)
        {

            int rownumber = i + 1;
            PdfPTable tabData2 = new PdfPTable(6);
            cell.BorderColor = new BaseColor(0, 0, 0);
            tabData2.TotalWidth = 480f;
            //fix the absolute width of the table AuthorName1,EmailId2,DepartmentName,Institute_name
            tabData2.LockedWidth = true;
            float[] width = new float[] { 1f, 3f, 2.5f, 3f, 2.5f, 2.5f };
            times = new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.NORMAL);
            tabData2.AddCell(new PdfPCell(new Phrase("  " + rownumber, FontFactory.GetFont("Arial", 9, iTextSharp.text.Font.NORMAL, BaseColor.BLACK))));
            string str=dz.Tables[0].Rows[i]["AuthorName"].ToString();
            string IndexAgency = System.Threading.Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(str.ToLower());
            tabData2.AddCell(new PdfPCell(new Phrase("  " + IndexAgency, FontFactory.GetFont("Arial", 9, iTextSharp.text.Font.NORMAL, BaseColor.BLACK))));
            string IndexAgency2 = dz.Tables[0].Rows[i]["AuthorType"].ToString();
            tabData2.AddCell(new PdfPCell(new Phrase(" " + IndexAgency2, FontFactory.GetFont("Arial", 9, iTextSharp.text.Font.NORMAL, BaseColor.BLACK))));
            string IndexAgency3 = dz.Tables[0].Rows[i]["Department"].ToString();
            tabData2.AddCell(new PdfPCell(new Phrase(" " + IndexAgency3, FontFactory.GetFont("Arial", 9, iTextSharp.text.Font.NORMAL, BaseColor.BLACK))));
            string IndexAgency4 = dz.Tables[0].Rows[i]["EmployeeCode"].ToString();
            tabData2.AddCell(new PdfPCell(new Phrase(" " + IndexAgency4, FontFactory.GetFont("Arial", 9, iTextSharp.text.Font.NORMAL, BaseColor.BLACK))));
            string IndexAgency5 = dz.Tables[0].Rows[i]["isCorrAuth"].ToString();
            tabData2.AddCell(new PdfPCell(new Phrase("   "+ IndexAgency5, FontFactory.GetFont("Arial", 9, iTextSharp.text.Font.NORMAL, BaseColor.BLACK))));
            tabData2.AddCell(cell);
            tabData2.SetWidths(widths);
            doc.Add(tabData2);
        }

        times1 = new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.BOLD);
        para1 = new Paragraph("•", times1);
        para1.SpacingBefore = 20f;// spacing from the top
        para1.SpacingAfter = 10f; // spacing from the bottom
        para1.IndentationLeft = -30f; // left space 
        doc.Add(para1);

        times1 = new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.BOLD);
        para1 = new Paragraph("Name of the journal:    ", times1);//
        para1.SpacingBefore = -25f;// spacing from the top
        para1.SpacingAfter = 5f; // spacing from the bottom
        para1.IndentationLeft = -20f; // left space 
        doc.Add(para1);

        times1 = new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.NORMAL);
        para1 = new Paragraph("" + p1.PubJournalName, times1);
        para1.SpacingBefore = -20f;// spacing from the top
        para1.SpacingAfter = 5f; // spacing from the bottom
        para1.IndentationLeft = 70f; // left space 
        doc.Add(para1);



        times1 = new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.NORMAL);
        para1 = new Paragraph("Volume.:   " + p1.JAVolume, times1);
        para1.SpacingBefore = 10f;// spacing from the top
        para1.SpacingAfter = 5f; // spacing from the bottom
        para1.IndentationLeft = -20f; // left space 
        doc.Add(para1);
        times1 = new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.NORMAL);
        para1 = new Paragraph("Month & Year :  " + p1.PublishJAMonth2 + "-" + p1.PublishJAYear, times1);



        times1 = new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.NORMAL);
        para1 = new Paragraph("Issue.:   " + p1.issues, times1);
        para1.SpacingBefore = -20f;// spacing from the top
        para1.SpacingAfter = 5f; // spacing from the bottom
        para1.IndentationLeft = 60f; // left space 
        doc.Add(para1);

        times1 = new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.NORMAL);
        para1 = new Paragraph("Month & Year :  " + p1.PublishJAMonth2 + "-" + p1.PublishJAYear, times1);
        para1.SpacingBefore = -20f;// spacing from the top
        para1.SpacingAfter = 5f; // spacing from the bottom
        para1.IndentationLeft = 140f; // left space 
        doc.Add(para1);


        if (p1.PageFrom.Contains("PF"))
        {
            times1 = new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.NORMAL);
            para1 = new Paragraph("Page: From:  " + "", times1);
            para1.SpacingBefore = -20f;// spacing from the top
            para1.SpacingAfter = 5f; // spacing from the bottom
            para1.IndentationLeft = 300f; // left space 
            doc.Add(para1);

            times1 = new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.NORMAL);
            para1 = new Paragraph("To: " + "", times1);
            para1.SpacingBefore = -20f;// spacing from the top
            para1.SpacingAfter = 5f; // spacing from the bottom
            para1.IndentationLeft = 370f; // left space
        }
        else
        {
            times1 = new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.NORMAL);
            para1 = new Paragraph("Page: From:  " + p1.PageFrom, times1);
            para1.SpacingBefore = -20f;// spacing from the top
            para1.SpacingAfter = 5f; // spacing from the bottom
            para1.IndentationLeft = 300f; // left space 
            doc.Add(para1);

            times1 = new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.NORMAL);
            para1 = new Paragraph("To: " + p1.PageTo, times1);
            para1.SpacingBefore = -20f;// spacing from the top
            para1.SpacingAfter = 5f; // spacing from the bottom
            para1.IndentationLeft = 370f; // left space
        }
        

        doc.Add(para1);
        times1 = new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.BOLD);
        para1 = new Paragraph("•", times1);
        para1.SpacingBefore = 10f;// spacing from the top
        para1.SpacingAfter = 10f; // spacing from the bottom
        para1.IndentationLeft = -30f; // left space 
        doc.Add(para1);
        Journal_DataObject ob2 = new Journal_DataObject();
        User u2 = new User();
        u2 = ob2.find_Catagory(PatientID);
        times1 = new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.BOLD);
        para1 = new Paragraph("Category of article:   ", times1);
        para1.SpacingBefore = -25f;// spacing from the top
        para1.SpacingAfter = 5f; // spacing from the bottom
        para1.IndentationLeft = -20f; // left space 
        doc.Add(para1);
        times1 = new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.NORMAL);
        para1 = new Paragraph("" + u2.Article, times1);
        para1.SpacingBefore = -20f;// spacing from the top
        para1.SpacingAfter = 5f; // spacing from the bottom
        para1.IndentationLeft = 70f; // left space 
        doc.Add(para1);
        times1 = new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.BOLD);
        para1 = new Paragraph("•", times1);
        para1.SpacingBefore = 10f;// spacing from the top
        para1.SpacingAfter = 10f; // spacing from the bottom
        para1.IndentationLeft = -30f; // left space 
        doc.Add(para1);

        times1 = new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.BOLD);
        para1 = new Paragraph("Journal Articles:    ", times1);
        para1.SpacingBefore = -25f;// spacing from the top
        para1.SpacingAfter = 30f; // spacing from the bottom
        para1.IndentationLeft = -20f; // left space 
        doc.Add(para1);

        Journal_DataObject ob = new Journal_DataObject();
        User u1 = new User();
        u1 = ob.indexFind(PatientID);
        times1 = new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.NORMAL);
        para1 = new Paragraph("Indexed In:    " + u1.Index, times1);
        para1.SpacingBefore = -45f;// spacing from the top
        para1.SpacingAfter = 50f; // spacing from the bottom
        para1.IndentationLeft = 60f; // left space
        doc.Add(para1);



        times1 = new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.BOLD);
        para1 = new Paragraph("•", times1);
        para1.SpacingBefore = -25f;// spacing from the top
        para1.SpacingAfter = 10f; // spacing from the bottom
        para1.IndentationLeft = -30f; // left space 
        doc.Add(para1);




        times1 = new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.NORMAL);
        para1 = new Paragraph("1- Year Impact Factor:   " + p1.impcat1, times1);
        para1.SpacingBefore = -25f;// spacing from the top
        para1.SpacingAfter = 5f; // spacing from the bottom
        para1.IndentationLeft = -20f; // left space 
        doc.Add(para1);



        times1 = new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.NORMAL);
        para1 = new Paragraph("5- Year Impact Factor: " + p1.impcat5, times1);
        para1.SpacingBefore = -20f;// spacing from the top
        para1.SpacingAfter = 5f; // spacing from the bottom
        para1.IndentationLeft = 120f; // left space 
        doc.Add(para1);


        times1 = new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.NORMAL);
        if (p1.IFApplicableYear != 0)
        {
            para1 = new Paragraph("Impact Factor Applied Year: " + p1.IFApplicableYear, times1);
        }
        else
        {
            para1 = new Paragraph("Impact Factor Applied Year: " + "", times1);
        }
        para1.SpacingBefore = -20f;// spacing from the top
        para1.SpacingAfter = 5f; // spacing from the bottom
        para1.IndentationLeft = 260f; // left space 
        doc.Add(para1);





        times1 = new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.BOLD);
        para1 = new Paragraph("•", times1);
        para1.SpacingBefore = -35f;// spacing from the top
        para1.SpacingAfter = 10f; // spacing from the bottom
        para1.IndentationLeft = -30f; // left space 
        doc.Add(para1);

        User u8 = new User();
        u8 = ob.findquartile(PatientID);

        times1 = new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.BOLD);
        para1 = new Paragraph("Quartile:   ", times1);
        para1.SpacingBefore = -24f;// spacing from the top
        para1.SpacingAfter = 5f; // spacing from the bottom
        para1.IndentationLeft = -20f; // left space 
        doc.Add(para1);


        times1 = new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.NORMAL);
        para1 = new Paragraph("" + u8.Name, times1);
        para1.SpacingBefore = -20f;// spacing from the top
        para1.SpacingAfter = 5f; // spacing from the bottom
        para1.IndentationLeft = 20f; // left space 
        doc.Add(para1);
       


        times1 = new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.NORMAL);
        para1 = new Paragraph(" ", times1);
        para1.SpacingBefore = 5f;// spacing from the top
        para1.SpacingAfter = 5f; // spacing from the bottom
        para1.IndentationLeft = 120f; // left space 
        doc.Add(para1);

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
        //tablen.SpacingBefore = 20f;
        tablen.SpacingAfter = 0;
        //PdfPCell cell2 = new PdfPCell(new Phrase("ITEM"));
        PdfPCell cell2 = new PdfPCell(new Phrase("\n Declaration by the submitting Author: \n\n", new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.BOLD)));

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
        PdfPCell cell222 = new PdfPCell(new Phrase("I/we certify that I/we have published article which is devoid of plagiarism. I/We have taken due care to ensure that  \n\n my/our published paper does not contain plagiarized material.", new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.NORMAL)));
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
        float[] widthw3 = new float[] { 1f, 3f, 7.5f, 3.2f, 6.5f };
        tablen3.SetWidths(widthw3);
        tablen3.HorizontalAlignment = -50;
        //leave a gap before and after the table
        //tablen.SpacingBefore = 20f;
        tablen3.SpacingAfter = 0;
        PdfPCell cell231 = new PdfPCell(new Phrase("\n", new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.NORMAL)));
        cell231.Border = iTextSharp.text.Rectangle.NO_BORDER;
        cell231.BackgroundColor = new iTextSharp.text.BaseColor(233, 233, 233);
        tablen3.AddCell(cell231);
        Journal_DataObject ob3 = new Journal_DataObject();
        User u3 = new User();

        u3 = ob2.get_submit_data(PatientID);
     string UserId =    u3.createdId;

     u3 = ob2.getEnteredBy(UserId);
     string names = u3.createdName;
     u3 = ob2.get_Author_Details(UserId);


        //PdfPCell cell2 = new PdfPCell(new Phrase("ITEM"));
        PdfPCell cell23 = new PdfPCell(new Phrase("\nName:", new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.NORMAL)));
        cell23.Border = iTextSharp.text.Rectangle.NO_BORDER;
        cell23.BackgroundColor = new iTextSharp.text.BaseColor(233, 233, 233);
        tablen3.AddCell(cell23);
        PdfPCell cell232 = new PdfPCell(new Phrase("\n " + names, new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.NORMAL)));
        cell232.Border = iTextSharp.text.Rectangle.NO_BORDER;
        cell232.BackgroundColor = new iTextSharp.text.BaseColor(233, 233, 233);
        tablen3.AddCell(cell232);
        PdfPCell cell1 = new PdfPCell(new Phrase("\n Department: ", new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.NORMAL)));
        cell1.Border = iTextSharp.text.Rectangle.NO_BORDER;
        cell1.BackgroundColor = new iTextSharp.text.BaseColor(233, 233, 233);
        tablen3.AddCell(cell1);
        PdfPCell cell3 = new PdfPCell(new Phrase("\n " + u3.adep, new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.NORMAL)));
        cell3.Border = iTextSharp.text.Rectangle.NO_BORDER;
        cell3.BackgroundColor = new iTextSharp.text.BaseColor(233, 233, 233);
        tablen3.AddCell(cell3);
        doc.Add(tablen3);
        PdfPTable tablen31 = new PdfPTable(5);
        //actual width of table in points
        tablen31.TotalWidth = 500f;//216f;
        //fix the absolute width of the table
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
        PdfPCell cell23111 = new PdfPCell(new Phrase("\nInstitution:", new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.NORMAL)));
        cell23111.Border = iTextSharp.text.Rectangle.NO_BORDER;
        cell23111.BackgroundColor = new iTextSharp.text.BaseColor(233, 233, 233);
        tablen31.AddCell(cell23111);
        PdfPCell cell2321 = new PdfPCell(new Phrase("\n " + u3.ainst, new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.NORMAL)));
        cell2321.Border = iTextSharp.text.Rectangle.NO_BORDER;
        cell2321.BackgroundColor = new iTextSharp.text.BaseColor(233, 233, 233);
        tablen31.AddCell(cell2321);
        PdfPCell cell11 = new PdfPCell(new Phrase("\nEmail Id:", new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.NORMAL)));
        cell11.Border = iTextSharp.text.Rectangle.NO_BORDER;
        cell11.BackgroundColor = new iTextSharp.text.BaseColor(233, 233, 233);
        tablen31.AddCell(cell11);
        PdfPCell cell31 = new PdfPCell(new Phrase("\n " + u3.aemail, new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.NORMAL)));
        cell31.Border = iTextSharp.text.Rectangle.NO_BORDER;
        cell31.BackgroundColor = new iTextSharp.text.BaseColor(233, 233, 233);
        tablen31.AddCell(cell31);
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
        PdfPCell cell232a = new PdfPCell(new Phrase("\n" + u3.aname, new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.NORMAL)));
        cell232a.Border = iTextSharp.text.Rectangle.NO_BORDER;
        cell232a.BackgroundColor = new iTextSharp.text.BaseColor(233, 233, 233);
        tablen311.AddCell(cell232a);
        PdfPCell cell1a = new PdfPCell(new Phrase("\n \n \n" + u3.aname, new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.NORMAL)));
        cell1a.Border = iTextSharp.text.Rectangle.NO_BORDER;
        cell1a.BackgroundColor = new iTextSharp.text.BaseColor(233, 233, 233);
        tablen311.AddCell(cell1a);
        doc.Add(tablen311);
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
        PdfPCell cellr = new PdfPCell(new Phrase("", new iTextSharp.text.Font(iTextSharp.text.Font.NORMAL, 10f, iTextSharp.text.Font.NORMAL)));
        cellr.Border = iTextSharp.text.Rectangle.NO_BORDER;
        cellr.BackgroundColor = new iTextSharp.text.BaseColor(233, 233, 233);
        tablen12.AddCell(cellr);
        PdfPCell cellr1 = new PdfPCell(new Phrase("\nSignature with date:\n\n", new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.NORMAL)));
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
        PdfPCell OfficeUseCell11 = new PdfPCell(new Phrase("\n\n\n Name & Signature of Head of Department", new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.BOLD)));
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
        //PdfPCell cellr11 = new PdfPCell(new Phrase("\n Total number of points:\n\n", new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.NORMAL)));
        //cellr11.Border = iTextSharp.text.Rectangle.NO_BORDER;
        //cellr11.BackgroundColor = new iTextSharp.text.BaseColor(233, 233, 233);
        //tab.AddCell(cellr11);
        PdfPCell cellr11 = new PdfPCell(new Phrase("\n", new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.NORMAL)));
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
        float[] OfficeUseW12 = new float[] { 6f, 6f, 9f };
        OfficeUse12.SetWidths(OfficeUseW12);
        OfficeUse12.HorizontalAlignment = 250;
        //leave a gap before and after the table
        //tablen.SpacingBefore = 20f;
        OfficeUse.SpacingAfter = 0;
        PdfPCell OfficeUseCell112 = new PdfPCell(new Phrase("\n ", new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.BOLD)));
        OfficeUseCell112.Right = 50f;
        OfficeUseCell112.Border = iTextSharp.text.Rectangle.NO_BORDER;
        OfficeUseCell112.BackgroundColor = new iTextSharp.text.BaseColor(233, 233, 233);
        OfficeUse12.AddCell(OfficeUseCell112);
        PdfPCell OfficeUseCel22 = new PdfPCell(new Phrase("\n", new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.BOLD)));
        OfficeUseCel22.Right = 50f;
        OfficeUseCel22.Border = iTextSharp.text.Rectangle.NO_BORDER;
        OfficeUseCel22.BackgroundColor = new iTextSharp.text.BaseColor(233, 233, 233);
        OfficeUse12.AddCell(OfficeUseCel22);
        PdfPCell OfficeUseCel52 = new PdfPCell(new Phrase("\n Director Research – (Health Sciences / Technical) \n\n", new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.BOLD)));
        OfficeUseCel52.Right = 50f;
        OfficeUseCel52.Border = iTextSharp.text.Rectangle.NO_BORDER;
        OfficeUseCel52.BackgroundColor = new iTextSharp.text.BaseColor(233, 233, 233);
        OfficeUse12.AddCell(OfficeUseCel52);
        doc.Add(OfficeUse12);
        PdfPTable tababc = new PdfPTable(3);
        //actual width of table in points
        tababc.TotalWidth = 500f;//216f;
        //fix the absolute width of the table
        tababc.LockedWidth = true;
        //relative col widths in proportions -  2/3 & 1/3 
        float[] tababac = new float[] { 6f, 8f, 9f };
        tababc.SetWidths(tababac);
        tababc.HorizontalAlignment = 250;
        //leave a gap before and after the table
        //tablen.SpacingBefore = 20f;
        tababc.SpacingAfter = 0;
        // PdfPCell a1 = new PdfPCell(new Phrase("\nSTN No: STN 311", new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.BOLD)));
        // a1.Right = 50f;
        // a1.Border = iTextSharp.text.Rectangle.NO_BORDER;
        // a1.BackgroundColor = new iTextSharp.text.BaseColor(233, 233, 233);
        //tababc.AddCell(a1);
        PdfPCell a2 = new PdfPCell(new Phrase("\n ", new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.BOLD)));
        a2.Right = 50f;
        a2.Border = iTextSharp.text.Rectangle.NO_BORDER;
        a2.BackgroundColor = new iTextSharp.text.BaseColor(233, 233, 233);
        tababc.AddCell(a2);
        PdfPCell a3 = new PdfPCell(new Phrase("\n MAHE (signature with date) \n\n\n\n", new iTextSharp.text.Font(bfTimes, 10, iTextSharp.text.Font.NORMAL)));
        a3.Right = 50f;
        a3.Border = iTextSharp.text.Rectangle.NO_BORDER;
        a3.BackgroundColor = new iTextSharp.text.BaseColor(233, 233, 233);
        tababc.AddCell(a3);
        doc.Add(tababc);
        doc.Close();
        //Student Details


        Journal_DataObject obj3 = new Journal_DataObject();
        PublishData data1 = new PublishData();
        data1 = obj3.CheckIsStudentPublication(PatientID);
                int year=Convert.ToInt16(data1.PublishJAYear);
                if (year < 2017)
                {
                    if (data1.IsStudentAuthor == "Y")
                    {
                        DataSet data = obj3.SelectStudentAuthorDetail(PatientID);
                        int rowcount = data.Tables[0].Rows.Count;
                        for (int i = 0; i < rowcount; i++)
                        {
                            string regno = ds.Tables[0].Rows[i]["EmployeeCode"].ToString();
                            string name = ds.Tables[0].Rows[i]["AuthorName"].ToString();

                            Document doc1 = new Document(PageSize.A4, 88f, 88f, 10f, 10f);
                            iTextSharp.text.Font NormalFont1 = FontFactory.GetFont("Arial", 12, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                            string filelocationpath1 = "";
                            string actualfilenamewithtime1 = "";
                            string servername1 = ConfigurationManager.AppSettings["ServerName"].ToString();
                            string domainame1 = ConfigurationManager.AppSettings["DomainName"].ToString();
                            string username1 = ConfigurationManager.AppSettings["UserName"].ToString();
                            string password1 = ConfigurationManager.AppSettings["Password"].ToString();
                            string mainpath1 = ConfigurationManager.AppSettings["PdfPath"].ToString();
                            actualfilenamewithtime1 = PatientID + "_" + regno + ".pdf";

                            // Create a new PdfWriter object, specifying the output stream
                            string gpath1 = Path.Combine(mainpath, "PublicationPrintEvaluation");
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


                            doc1.Close();

                        }
                    }
                }

        Journal_DataObject obj4 = new Journal_DataObject();
        data1 = obj4.CheckIsStudentPublication(PatientID);
        int year1 = Convert.ToInt16(data1.PublishJAYear);
        if (year1 <2017)
        {
            if (data1.IsStudentAuthor == "Y")
            {
                using (ZipFile zip = new ZipFile())
                {
                    zip.AlternateEncodingUsage = ZipOption.AsNecessary;
                    zip.AddDirectoryByName("Files");
                    string[] filePaths = Directory.GetFiles(path1);
                    foreach (string filePath in filePaths)
                    {
                        zip.AddFile(filePath, "Files");

                    }
                    HttpContext.Current.Response.Clear();
                    HttpContext.Current.Response.BufferOutput = false;
                    string zipName = String.Format("PrintEvaluation_{0}.zip", DateTime.Now.ToString("yyyy-MMM-dd-HHmmss"));
                    HttpContext.Current.Response.ContentType = "application/zip";
                    HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=" + zipName);
                    zip.Save(HttpContext.Current.Response.OutputStream);
                    HttpContext.Current.Response.End();
                }
            }
            else
            {

            }
        }
        FileInfo myfile = new FileInfo(filelocationpath);
        if (myfile.Exists)
        {
            log.Info("Evaluation Form Generated Successfully,  for Publiction ID: " + PatientID + " of Type: " + type + " by " + HttpContext.Current.Session["UserName"]);
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
            log.Info("Evaluation Form generation failed,  for Publiction ID: " + PatientID + " of Type: " + type + " by " + HttpContext.Current.Session["UserName"]);
            log.Error("Error: " + e);
        }

        //User u = new User();
        //u.PatientID = PatientID;
        //// int VisitCount = obj.fnVisitCount(PatientID, info);
        //u.FilePath = filelocationpath;
        //obj.fnPatOrthoAuxiVisitDet(u);
        
    }

}


