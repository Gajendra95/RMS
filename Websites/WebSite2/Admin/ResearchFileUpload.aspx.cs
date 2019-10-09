using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using System.IO;
using System.Configuration;

using log4net;
using AjaxControlToolkit;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Data.OleDb;

public partial class Admin_EditResearchArea : System.Web.UI.Page
{

    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    public int validExcel = 1;
    public int skip = 0;
      public int result=0; 
    public int sucess = 0;
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void upload_Click(object sender, EventArgs e)
    {

        if (!Page.IsValid)
        {
            return;
        }

        log.Debug("Inside upload_Click page");
        try
        {
            bindGridview(sender, e);
          
             if (GridExcelData.Visible == false && validExcel == 1)
             {
                 Business b = new Business();
                 FileUpload[] f = new FileUpload[GridExcelData.Rows.Count];
                 int i = 0;
                 foreach (GridViewRow gr in GridExcelData.Rows)
                 {

                     string employeecode = GridExcelData.Rows[gr.RowIndex].Cells[0].Text;
                     if (employeecode == "&nbsp;")
                     {
                         skip++;
                         continue;
                     }
                     else
                     {

                         FileUpload f1 = new FileUpload();

                         f1 = b.CheckEmployeeId(employeecode);
                         if (f1.EmployeeCode != null)
                         {
                             //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('The Employeecode " + employeecode + " in line number " + (gr.RowIndex + 1) + " is already exist...Please verify the data.... ')</script>");
                             //return;
                             skip++;
                             continue;
                         }
                         else
                         {

                             string domain1 = GridExcelData.Rows[gr.RowIndex].Cells[1].Text;

                             if (GridExcelData.Rows[gr.RowIndex].Cells[1].Text == "&nbsp;")
                             {
                                 domain1 = DBNull.Value.ToString();
                             }
                             string domain2 = GridExcelData.Rows[gr.RowIndex].Cells[3].Text;

                             if (GridExcelData.Rows[gr.RowIndex].Cells[3].Text == "&nbsp;")
                             {
                                 domain2 = DBNull.Value.ToString();
                             }
                             string domain3 = GridExcelData.Rows[gr.RowIndex].Cells[5].Text;
                             if (GridExcelData.Rows[gr.RowIndex].Cells[5].Text == "&nbsp;")
                             {
                                 domain3 = DBNull.Value.ToString();
                             }
                             string domain4 = GridExcelData.Rows[gr.RowIndex].Cells[7].Text;
                             if (GridExcelData.Rows[gr.RowIndex].Cells[7].Text == "&nbsp;")
                             {
                                 domain4 = DBNull.Value.ToString();
                             }
                             string domain5 = GridExcelData.Rows[gr.RowIndex].Cells[9].Text;
                             if (GridExcelData.Rows[gr.RowIndex].Cells[9].Text == "&nbsp;")
                             {
                                 domain5 = DBNull.Value.ToString();
                             }
                             string domain6 = GridExcelData.Rows[gr.RowIndex].Cells[11].Text;
                             if (GridExcelData.Rows[gr.RowIndex].Cells[11].Text == "&nbsp;")
                             {
                                 domain6 = DBNull.Value.ToString();
                             }
                             string domain7 = GridExcelData.Rows[gr.RowIndex].Cells[13].Text;
                             if (GridExcelData.Rows[gr.RowIndex].Cells[13].Text == "&nbsp;")
                             {
                                 domain7 = DBNull.Value.ToString();
                             }
                             string domain8 = GridExcelData.Rows[gr.RowIndex].Cells[15].Text;
                             if (GridExcelData.Rows[gr.RowIndex].Cells[15].Text == "&nbsp;")
                             {
                                 domain8 = DBNull.Value.ToString();
                             }
                             string domain9 = GridExcelData.Rows[gr.RowIndex].Cells[17].Text;
                             if (GridExcelData.Rows[gr.RowIndex].Cells[17].Text == "&nbsp;")
                             {
                                 domain9 = DBNull.Value.ToString();
                             }
                             string domain10 = GridExcelData.Rows[gr.RowIndex].Cells[19].Text;
                             if (GridExcelData.Rows[gr.RowIndex].Cells[19].Text == "&nbsp;")
                             {
                                 domain10 = DBNull.Value.ToString();
                             }

                             string area1 = GridExcelData.Rows[gr.RowIndex].Cells[2].Text;
                             if (GridExcelData.Rows[gr.RowIndex].Cells[2].Text == "&nbsp;")
                             {
                                 area1 = DBNull.Value.ToString();
                             }
                             string area2 = GridExcelData.Rows[gr.RowIndex].Cells[4].Text;
                             if (GridExcelData.Rows[gr.RowIndex].Cells[4].Text == "&nbsp;")
                             {
                                 area2 = DBNull.Value.ToString();

                             }
                             string area3 = GridExcelData.Rows[gr.RowIndex].Cells[6].Text;
                             if (GridExcelData.Rows[gr.RowIndex].Cells[6].Text == "&nbsp;")
                             {
                                 area3 = DBNull.Value.ToString();

                             }
                             string area4 = GridExcelData.Rows[gr.RowIndex].Cells[8].Text;
                             if (GridExcelData.Rows[gr.RowIndex].Cells[8].Text == "&nbsp;")
                             {
                                 area4 = DBNull.Value.ToString();

                             }
                             string area5 = GridExcelData.Rows[gr.RowIndex].Cells[10].Text;
                             if (GridExcelData.Rows[gr.RowIndex].Cells[10].Text == "&nbsp;")
                             {
                                 area5 = DBNull.Value.ToString();

                             }
                             string area6 = GridExcelData.Rows[gr.RowIndex].Cells[12].Text;
                             if (GridExcelData.Rows[gr.RowIndex].Cells[12].Text == "&nbsp;")
                             {
                                 area6 = DBNull.Value.ToString();

                             }
                             string area7 = GridExcelData.Rows[gr.RowIndex].Cells[14].Text;
                             if (GridExcelData.Rows[gr.RowIndex].Cells[14].Text == "&nbsp;")
                             {
                                 area7 = DBNull.Value.ToString();

                             }
                             string area8 = GridExcelData.Rows[gr.RowIndex].Cells[16].Text;
                             if (GridExcelData.Rows[gr.RowIndex].Cells[16].Text == "&nbsp;")
                             {
                                 area8 = DBNull.Value.ToString();

                             }
                             string area9 = GridExcelData.Rows[gr.RowIndex].Cells[18].Text;
                             if (GridExcelData.Rows[gr.RowIndex].Cells[18].Text == "&nbsp;")
                             {
                                 area9 = DBNull.Value.ToString();

                             }
                             string area10 = GridExcelData.Rows[gr.RowIndex].Cells[20].Text;
                             if (GridExcelData.Rows[gr.RowIndex].Cells[20].Text == "&nbsp;")
                             {
                                 area10 = DBNull.Value.ToString();

                             }

                             //string arealist = string.Concat(area1 + ";" + area2 + ";" + area3 + ";" + area4 + ";" + area5 + ";" + area6 + ";" + area7 + ";" + area8 + ";" + area9 + ";" + area10 + ";" + area11 + ";" + area12 + ";" + area13 + ";" + area14 + ";" + area15 + ";" + area16 + ";" + area17 + ";" + area18 + ";" + area19);
                             string domain = string.Concat(domain1 + ":" + domain2 + ":" + domain3 + ":" + domain4 + ":" + domain5 + ":" + domain6 + ":" + domain7 + ":" + domain8 + ":" + domain9 + ":" + domain10);
                             string arealist = string.Concat(area1 + ":" + area2 + ":" + area3 + ":" + area4 + ":" + area5 + ":" + area6 + ":" + area7 + ":" + area8 + ":" + area9 + ":" + area10);

                             f[i] = new FileUpload();
                             f[i].EmployeeCode = employeecode;
                             f[i].Domain = domain;
                             f[i].Area = arealist;
                             sucess++;
                             i++;
                         }
                     }
                 }

                 if (sucess > 0 )
                     {

                         result = b.fnInsertResearchData(f,i);
                     }
                 
             }
                 if (result > 0 && skip == 0 && sucess > 0)
                 {
                     string CloseWindow1 = "alert('Successfully Uploaded')";
                     ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow1", CloseWindow1, true);
                     log.Info("Successfully Uploaded");
                     return;
                    
                     //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('File Uploaded Successfully!')</script>");


                 }
                 else if (result >0 && skip > 0)
                 {
                     //ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert(' File uploaded successfullly and these many DataControlRowState are skipped!')</script>");
                     string CloseWindow1 = "alert(' " + sucess + " rows Inserted Successfully & " + skip + " rows are skipped,Beacause Data is already Present')";
                    
                     ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow1", CloseWindow1, true);
                     log.Error("alert(' " + sucess + " rows Inserted Successfully & " + skip + " rows are skipped, Beacause Data is already Present')");
                     return;
                 }

                 else
                 {
                     string CloseWindow1 = "alert('  " + skip + " rows are skipped,Beacause Data is already Present')";
                     ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow1", CloseWindow1, true);
                     log.Error("alert('  " + skip + " rows are skipped, Beacause Data is already Present')");
                     return;
                 }

             
        }catch (Exception ex)
        {
           
            log.Error("Inside Catch Block Of upload_Click" + ex.Message + " With UserID" );

            log.Error(ex.StackTrace);


            if (ex.Message.Contains("Cannot insert duplicate key in object 'dbo.FacultyResearchArea'."))
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('File Upload Failed. UserId Already Exsists')</script>");

            }
            else
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('ERROR! File Upload Failed')</script>");
            }
        }
          


    }

    private void bindGridview(object sender, EventArgs e)
    {
        string connectionString = "";
        if (FileUpload.FileName == "")
        {

            ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Please Select/Browse to Upload the File!')</script>");
        }
        if (FileUpload.HasFile)
        {
            string savePath = ConfigurationManager.AppSettings["UploadUserPath"];

            string strFileType = Path.GetExtension(FileUpload.FileName).ToLower();
            // string path = string.Concat(Server.MapPath(savePath + F_Upload.FileName));
            // F_Upload.PostedFile.SaveAs(path);

            string path = string.Concat(savePath + "/" + FileUpload.FileName);
            FileUpload.SaveAs(path);


            if (strFileType == ".xls")
            {
                //connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                 connectionString = String.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"", path);
            }
            /*else if (strFileType == ".xlsx")
               {
                   //connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                   connectionString = String.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=0\"", path);
               }*/
             

            else
            {
                validExcel = 0;
                return;
            }
            string query = "SELECT * FROM [Sheet1$]";
            OleDbConnection conn = new OleDbConnection(connectionString);
            if (conn.State == ConnectionState.Closed)
                conn.Open();
            OleDbCommand cmd = new OleDbCommand(query, conn);
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            GridExcelData.DataSource = ds.Tables[0];
            GridExcelData.DataBind();
            da.Dispose();
            conn.Close();
            conn.Dispose();
        }
    }
    protected void GridExcelData_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    for (int i = 0; i < e.Row.Cells.Count; i++)
        //    {
        //        if (e.Row.Cells[0].Text == "&nbsp;" || e.Row.Cells[0].Text.Trim() == "" || e.Row.Cells[0].Text == null)
        //        {
        //            GridExcelData.Visible = true;
        //            ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Excel Data Is Invalid!')</script>");
        //        }
        //    }
        //}
    }

  
}