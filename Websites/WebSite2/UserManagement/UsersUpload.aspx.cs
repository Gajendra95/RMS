using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Data.OleDb;
using log4net;
using System.Net.Mail;
using Microsoft.Office.Interop.Excel;
public partial class UserManagement_UsersUpload : System.Web.UI.Page
{
    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

    public int validExcel = 1;

    string path = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        //upload.Attributes.Add("onclick", "this.disabled =true;" + ClientScript.GetPostBackEventReference(upload, null) + ";");
        GridView1.Visible = true;       
    }
    protected void bindGridview(object sender, EventArgs e)
    {
        log.Debug("Inside bindGridview function");
        User_Mangement b = new User_Mangement();
        DataSet ds = b.GetHREmpData();
        if (ds.Tables[0].Rows.Count == 0)
        {
            log.Debug("No records to update or insert");
            ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('No HR record exists to update or add')</script>");
            return;
        }
        else
        {
            string connectionString = "";
            string isfilegenerate = ConfigurationManager.AppSettings["UserExcelFileGenerate"].ToString();
            if (isfilegenerate == "Y")
            {
                Microsoft.Office.Interop.Excel.Application xlApp =
                           new Microsoft.Office.Interop.Excel.Application();
                Workbook xlWorkbook = xlApp.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);
                Sheets xlSheets = null;
                Worksheet xlWorksheet = null;
                object misValue = System.Reflection.Missing.Value;


                System.Data.DataTable dataTable = ds.Tables[0];
                int rowNo = dataTable.Rows.Count;
                int columnNo = dataTable.Columns.Count;
                int colIndex = 0;

                //Create Excel Sheets
                xlSheets = xlWorkbook.Sheets;
                xlWorksheet = (Worksheet)xlSheets.Add(xlSheets[1],
                               Type.Missing, Type.Missing, Type.Missing);
                //xlWorksheet.Name = dataSet.DataSetName;

                //Generate Field Names
                foreach (DataColumn dataColumn in dataTable.Columns)
                {
                    colIndex++;
                    xlApp.Cells[1, colIndex] = dataColumn.ColumnName;
                }

                object[,] objData = new object[rowNo, columnNo];

                //Convert DataSet to Cell Data
                for (int row = 0; row < rowNo; row++)
                {
                    for (int col = 0; col < columnNo; col++)
                    {
                        objData[row, col] = dataTable.Rows[row][col];
                    }
                }

                //Add the Data
                Range range = xlWorksheet.Range[xlApp.Cells[2, 1], xlApp.Cells[rowNo + 1, columnNo]];
                range.Value2 = objData;



                //Remove the Default Worksheet
                ((Worksheet)xlApp.ActiveWorkbook.Sheets[xlApp.ActiveWorkbook.Sheets.Count]).Delete();

                string servername = ConfigurationManager.AppSettings["ServerName"].ToString();
                string domainame = ConfigurationManager.AppSettings["DomainName"].ToString();
                string username = ConfigurationManager.AppSettings["UserName"].ToString();
                string password = ConfigurationManager.AppSettings["Password"].ToString();
                string mainpath = ConfigurationManager.AppSettings["UploadUserPath"].ToString();
                string timestamp = DateTime.Now.ToString("yyyy-MM-dd-hh_mm_ss");
                string ExcelFileVersion = ConfigurationManager.AppSettings["ExcelFileVersion"].ToString();
                string FileName = "HRUserData" + timestamp + ExcelFileVersion;
                using (NetworkShareAccesser.Access(servername, domainame, username, password))
                {
                    if (Directory.Exists(mainpath))
                    {

                        path = Path.Combine(mainpath, FileName);
                        //  xlWorkbook.SaveAs(FileName, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                        xlWorkbook.SaveAs(path,
                           System.Reflection.Missing.Value,
                           System.Reflection.Missing.Value,
                           System.Reflection.Missing.Value,
                           System.Reflection.Missing.Value,
                           System.Reflection.Missing.Value,
                           XlSaveAsAccessMode.xlNoChange,
                           System.Reflection.Missing.Value,
                           System.Reflection.Missing.Value,
                           System.Reflection.Missing.Value,
                           System.Reflection.Missing.Value,
                           System.Reflection.Missing.Value);

                        xlWorkbook.Close();
                        xlApp.Quit();
                    }
                    else
                    {
                        Directory.CreateDirectory(mainpath);
                        path = Path.Combine(mainpath, FileName);
                        //  xlWorkbook.SaveAs(FileName, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                        xlWorkbook.SaveAs(path,
                        System.Reflection.Missing.Value,
                        System.Reflection.Missing.Value,
                        System.Reflection.Missing.Value,
                        System.Reflection.Missing.Value,
                        System.Reflection.Missing.Value,
                        XlSaveAsAccessMode.xlNoChange,
                        System.Reflection.Missing.Value,
                        System.Reflection.Missing.Value,
                        System.Reflection.Missing.Value,
                        System.Reflection.Missing.Value,
                        System.Reflection.Missing.Value);

                        xlWorkbook.Close();
                        xlApp.Quit();


                    }
                }
            }
            //string query = "SELECT * FROM [Sheet2$]";
            //connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
            //OleDbConnection conn = new OleDbConnection(connectionString);
            //if (conn.State == ConnectionState.Closed)
            //    conn.Open();
            //OleDbCommand cmd = new OleDbCommand(query, conn);
            //OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            //DataSet ds1 = new DataSet();
            //da.Fill(ds1);
            GridExcelData.DataSource = ds.Tables[0];
            GridExcelData.DataBind();
            //da.Dispose();
            //conn.Close();
            //conn.Dispose();
        }
    }

    protected void GridExcelData_PageChanged(object sender, GridViewPageEventArgs e)
    {
        GridExcelData.PageIndex = e.NewPageIndex;
        GridExcelData.DataBind();
    }
    protected void upload_Click(object sender, EventArgs e)
    {
        log.Debug("Inside upload_Click function");
        if (!Page.IsValid)
        {
            return;
        }
        //System.Threading.Thread.Sleep(5000);
        try
        {
            upload.Enabled = false;
            bindGridview(sender, e);
            Business b = new Business();
            if (GridExcelData.Visible == false && validExcel == 1)
            {
                if (GridExcelData.Rows.Count > 0)
                {
                    User[] jd = new User[GridExcelData.Rows.Count];

                    for (int j = 0; j < GridExcelData.Rows.Count; j++)
                    {

                        string EmpInst = GridExcelData.Rows[j].Cells[0].Text.Trim();
                        string EmpId = GridExcelData.Rows[j].Cells[1].Text.Trim();

                        //string EmpClass = GridExcelData.Rows[j].Cells[2].Text.Trim();
                        string EmpPrefix = GridExcelData.Rows[j].Cells[2].Text.Trim();
                        string EmpFName = GridExcelData.Rows[j].Cells[3].Text.Trim();
                        string EmpMName = GridExcelData.Rows[j].Cells[4].Text.Trim();

                        string EmpLName = GridExcelData.Rows[j].Cells[5].Text.Trim();
                        string EmpDept = GridExcelData.Rows[j].Cells[6].Text.Trim();

                        string EmpSupervisorId = GridExcelData.Rows[j].Cells[7].Text.Trim();
                        string EmpEmail = GridExcelData.Rows[j].Cells[8].Text.Trim();

                        string EntryStatus = GridExcelData.Rows[j].Cells[9].Text.Trim();


                        jd[j] = new User();
                        jd[j].InstituteId = EmpInst;
                        jd[j].User_Id = EmpId;
                        jd[j].UserNamePrefix = EmpPrefix;
                        jd[j].UserFirstName = EmpFName;

                        if (EmpMName == "&nbsp;")
                        {
                            jd[j].UserMiddleName = String.Empty;
                        }
                        else
                        {
                            jd[j].UserMiddleName = EmpMName;
                        }
                        if (EmpLName == "&nbsp;")
                        {
                            jd[j].UserLastName = String.Empty;
                        }
                        else
                        {
                            jd[j].UserLastName = EmpLName;
                        }
                        jd[j].Department = EmpDept;

                        jd[j].SupervisorId = EmpSupervisorId;
                        if (EmpEmail == "&nbsp;")
                        {
                            jd[j].emailId = "";
                        }
                        else
                        {
                            jd[j].emailId = EmpEmail;
                        }

                        jd[j].EntryStatus = EntryStatus;

                        jd[j].CreatedBy = Session["UserId"].ToString();

                    }
                    int result = 0;
                    result = b.InsertUserUploadExcel(jd, path);

                    if (result >= 0)
                    {
                        GridView1.DataBind();
                        GridView1.Visible = true;
                        ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('User data uploaded Successfully')</script>");

                        //  upload.Enabled = false;
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Error in  user data upload')</script>");


                    }
                }
                else
                {
                    ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Error in  user data upload')</script>");
                    return;
                }
            }


            else
            {

                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Invalid File!')</script>");
                validExcel = 1;
                upload.Enabled = true;
                return;
            }
        }

        catch (Exception ex)
        {
            log.Error("Inside Catch Block Of JC FileUpload" + ex.Message + " With UserID" + Session["UserId"].ToString());

            log.Error(ex.StackTrace);

            String det = "HHH";
            int i, j;



            if (ex.Message.Contains("Specified argument was out of the range of valid values"))
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Uploaded File format for user upload is not valid')</script>");

            }

            else if (ex.Message.Contains("Input string was not in a correct format"))
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('File Upload Failed--File format is not valid')</script>");

            }
            else if (ex.Message.Contains("'Sheet2$' is not a valid name"))
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Sheet2 is not there in  Uploaded file')</script>");

            }


            else if (ex.Message.Contains("Could not find a part of the path"))
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('You need to create RMSUserfiles folder ..then try to upload the user data')</script>");

            }

            else if (ex.Message.Contains("Object reference not set to an instance of an object"))
            {
                i = ex.Message.IndexOf('$');
                j = ex.Message.IndexOf('+');
                det = ex.Message.Substring(i + 1, j - i - 1);
                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert(' User data Upload Failed!!!!! EmplId is not there in the system of " + det + " ')</script>");


            }

            else if (ex.Message.Contains("Violation of PRIMARY KEY constraint 'PK_User_M_1'. Cannot insert duplicate key in objec"))
            {
                i = ex.Message.IndexOf('$');
                j = ex.Message.IndexOf('+');
                det = ex.Message.Substring(i + 1, j - i - 1);
                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert(' User data Upload Failed!!!!! EmplId is already exsits of " + det + " ')</script>");

            }

            else
            {
                ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('ERROR! User data Upload Failed')</script>");
            }

        }
    }

    protected void GVViewUploadedRecordsView_SelectedIndexChanged(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "VIEW")
        {
            GridViewRow rowSelect = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
            int rowindex = rowSelect.RowIndex;

            string servername = ConfigurationManager.AppSettings["ServerName"].ToString();
            string domainame = ConfigurationManager.AppSettings["DomainName"].ToString();
            string username = ConfigurationManager.AppSettings["UserName"].ToString();
            string password = ConfigurationManager.AppSettings["Password"].ToString();

            string mainpath = ConfigurationManager.AppSettings["UploadUserPath"].ToString();
            using (NetworkShareAccesser.Access(servername, domainame, username, password))
            {
                int id = GridView1.SelectedIndex;
                System.Web.UI.WebControls.Label filepath = (System.Web.UI.WebControls.Label)GridView1.Rows[rowindex].FindControl("FilePath");
                string path = filepath.Text;       //actual filelocation path  
                string newpath = path.Replace('\\', '/');  // replacing '\' by '/' to view image or pdf


                string extention = Path.GetExtension(newpath);
                int len = extention.Length - 1;
                string extwithoutdot = extention.Substring(1, len);
                string filetype = "";

                FileInfo myfile = new FileInfo(path);

                if (myfile.Exists)
                {

                    HttpContext.Current.Response.ClearContent();
                    HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + newpath);
                    HttpContext.Current.Response.AddHeader("Content-Length", myfile.Length.ToString());
                    HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
                    HttpContext.Current.Response.TransmitFile(myfile.FullName);
                    HttpContext.Current.Response.End();

                }

            }
        }
     

    }

    protected void gridDetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            System.Web.UI.WebControls.Label path = (System.Web.UI.WebControls.Label)e.Row.Cells[3].FindControl("FilePath");
            System.Web.UI.WebControls.LinkButton link = (System.Web.UI.WebControls.LinkButton)e.Row.Cells[4].FindControl("lnkView");
            System.Web.UI.WebControls.Label lbl = (System.Web.UI.WebControls.Label)e.Row.Cells[4].FindControl("label");
            if (path.Text == "")
            {
                lbl.Visible = true;
                link.Visible = false;
            }
            else
            {
                lbl.Visible = false;
                link.Visible = true;
            }

        }
    }
}