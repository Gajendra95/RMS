using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Incentive_IncentivePointViewInstitutionwise : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lbl1.Visible = true;
            Panel1.Visible = true;
            Panel3.Visible = true;
            Gridview.Visible = true;
            GridView1.Visible = true;
            IncentiveBusiness obj = new IncentiveBusiness();
            string membertype = obj.SelectMemberType(txtEmployeecode.Text.Trim());
            string institution = obj.CheckInstitution(txtEmployeecode.Text.Trim());
            string role = Session["Role"].ToString();
            string user = Session["UserId"].ToString();
            ArrayList rolelist = new ArrayList();
            rolelist = obj.SelectCountOfRole(user);

            if(rolelist.Contains(18))
            {
                if (institution == Session["InstituteId"].ToString())
                {
                    GridviewStudent.DataBind();
                    GridviewStudent.Visible = true;
                    GridView1.Visible = true;
                    Gridview.Visible = false;
                    PanelPRAISE.Visible = true;
                    Panel5.Visible = false;
                }
                else
                {
                    Panel1.Visible = false;
                    GridviewStudent.Visible = false;
                    Gridview.Visible = false;
                    Panel1.Visible = false;
                    GridviewStudent.Visible = false;
                    PanelPRAISE.Visible = false;
                    Panel5.Visible = false;
                    GridView5.Visible = false;
                    lbl5.Visible = false;
                    Panel3.Visible = false;
                }
                if (institution == Session["InstituteId"].ToString())
                {
                    DataTable dt = new DataTable();
                    dt = obj.CountRemainingPoints(txtEmployeecode.Text);
                    if (dt.Rows.Count > 0)
                    {
                        GridView1.Columns[2].FooterText = "Total";
                        GridView1.Columns[3].FooterText = dt.Rows[0]["count1"].ToString();
                        GridView1.Columns[4].FooterText = dt.Rows[0]["count2"].ToString();
                        GridView1.DataSource = dt;
                        GridView1.DataBind();
                        GridView1.Visible = true;
                    }
                    {
                        GridView1.Visible = true;
                        GridView1.DataBind();
                    }
                }
                else
                {
                    Gridview.Visible = false;
                    Panel1.Visible = false;
                    GridviewStudent.Visible = false;
                    PanelPRAISE.Visible = false;
                    Panel5.Visible = false;
                    GridView5.Visible = false;
                    lbl5.Visible = false;
                    Panel1.Visible = false;
                    Panel3.Visible = false;
                    GridviewStudent.Visible = false;
                }
            }
            else if (rolelist.Contains(17))
            {
                if (institution == Session["InstituteId"].ToString())
                {
                    GridviewStudent.Visible = false;
                    Gridview.Visible = true;
                    PanelPRAISE.Visible = false;
                    Panel5.Visible = true;
                    GridView5.Visible = true;
                    lbl5.Visible = true;
                }
                else
                {
                    Gridview.Visible = false;
                    Panel1.Visible = false;
                    GridviewStudent.Visible = false;
                    PanelPRAISE.Visible = false;
                    Panel5.Visible = false;
                    GridView5.Visible = false;
                    lbl5.Visible = false;
                    Panel1.Visible = false;
                    Panel3.Visible = false;
                    GridviewStudent.Visible = false;
                }
            }
            else if (rolelist.Contains(19) || rolelist.Contains(7))
            {
                if (membertype == "S")
                {
                    if (institution == Session["InstituteId"].ToString())
                    {
                        GridviewStudent.DataBind();
                        GridviewStudent.Visible = true;
                        GridView1.Visible = true;
                        Gridview.Visible = false;
                        PanelPRAISE.Visible = true;
                        Panel5.Visible = false;
                    }
                    else
                    {
                        Panel1.Visible = false;
                        GridviewStudent.Visible = false;
                        Gridview.Visible = false;
                        Panel1.Visible = false;
                        GridviewStudent.Visible = false;
                        PanelPRAISE.Visible = false;
                        Panel5.Visible = false;
                        GridView5.Visible = false;
                        lbl5.Visible = false;
                        Panel3.Visible = false;
                    }

                }
                else
                {

                    if (institution == Session["InstituteId"].ToString())
                    {
                        GridviewStudent.Visible = false;
                        Gridview.Visible = true;
                        PanelPRAISE.Visible = false;
                        Panel5.Visible = true;
                        GridView5.Visible = true;
                        lbl5.Visible = true;
                    }
                    else
                    {
                        Gridview.Visible = false;
                        Panel1.Visible = false;
                        GridviewStudent.Visible = false;
                        PanelPRAISE.Visible = false;
                        Panel5.Visible = false;
                        GridView5.Visible = false;
                        lbl5.Visible = false;
                        Panel1.Visible = false;
                        Panel3.Visible = false;
                        GridviewStudent.Visible = false;
                    }
                }
                if (institution == Session["InstituteId"].ToString())
                {
                    DataTable dt = new DataTable();
                    dt = obj.CountRemainingPoints(txtEmployeecode.Text);
                    if (dt.Rows.Count > 0)
                    {
                        GridView1.Columns[2].FooterText = "Total";
                        GridView1.Columns[3].FooterText = dt.Rows[0]["count1"].ToString();
                        GridView1.Columns[4].FooterText = dt.Rows[0]["count2"].ToString();
                        GridView1.DataSource = dt;
                        GridView1.DataBind();
                        GridView1.Visible = true;
                    }
                    {
                        GridView1.Visible = true;
                        GridView1.DataBind();
                    }
                }
                else
                {
                    Gridview.Visible = false;
                    Panel1.Visible = false;
                    GridviewStudent.Visible = false;
                    PanelPRAISE.Visible = false;
                    Panel5.Visible = false;
                    GridView5.Visible = false;
                    lbl5.Visible = false;
                    Panel1.Visible = false;
                    Panel3.Visible = false;
                    GridviewStudent.Visible = false;
                }

            }       
            else 
            {
                //if (Session["Role"].ToString() == "11")
                //{
                table.Visible = false;
                txtEmployeecode.Text = Session["UserId"].ToString();
                txtEmployeecode.Visible = false;
                lbl1.Visible = true;
                Panel1.Visible = true;
                Panel3.Visible = true;
                Panel5.Visible = true;
                lbl5.Visible = true;
                Gridview.Visible = true;
                GridView1.Visible = true;
                GridView5.Visible = true;
                table1.Visible = true;
                GridviewStudent.Visible = false;
                PanelPRAISE.Visible = false;
                lblnote.Text = "Note: For any queries please contact Directorate of Research (help.rms@manipal.edu)";
                //IncentiveBusiness obj = new IncentiveBusiness();
                DataTable dt = new DataTable();
                dt = obj.CountRemainingPoints(txtEmployeecode.Text);
                if (dt.Rows.Count > 0)
                {
                    GridView1.Columns[2].FooterText = "Total";
                    GridView1.Columns[3].FooterText = dt.Rows[0]["count1"].ToString();
                    GridView1.Columns[4].FooterText = dt.Rows[0]["count2"].ToString();
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                }
                {
                    GridView1.DataBind();
                }
            }
            
            //else
            //{
            //    table.Visible = true;
            //    txtEmployeecode.Text = "";
            //    lbl1.Visible = false;
            //    Panel1.Visible = false;
            //    Panel3.Visible = false;
            //    Panel5.Visible = true;
            //    lbl5.Visible = true;
            //    //IncentiveBusiness obj = new IncentiveBusiness();
            //    string membertype1 = obj.SelectMemberType(txtEmployeecode.Text.Trim());
            //    if (membertype1 == "S")
            //    {
            //        GridviewStudent.Visible = true;
            //    }
            //    else
            //    {
            //        Gridview.Visible = false;
            //    }
            //    Gridview.Visible = false;
            //    GridView1.Visible = false;
            //    GridView5.Visible = true;
            //}

        }
    }
    protected void Btnsearch_Click(object sender, EventArgs e)
    {
        lbl1.Visible = true;
        Panel1.Visible = true;
        Panel3.Visible = true;
        Gridview.Visible = true;
        GridView1.Visible = true;
        IncentiveBusiness obj = new IncentiveBusiness();
        string membertype = obj.SelectMemberType(txtEmployeecode.Text.Trim());
        string institution = obj.CheckInstitution(txtEmployeecode.Text.Trim());
        string role = Session["Role"].ToString();
        string user = Session["UserId"].ToString();
        ArrayList rolelist = new ArrayList();
        rolelist = obj.SelectCountOfRole(user);
        if (rolelist.Contains(18))
        {
            if (membertype != "M")
            {
                if (institution == Session["InstituteId"].ToString())
                {
                    GridviewStudent.DataBind();
                    GridviewStudent.Visible = true;
                    GridView1.Visible = true;
                    Gridview.Visible = false;
                    PanelPRAISE.Visible = true;
                    Panel5.Visible = false;
                }
                else
                {
                    Panel1.Visible = false;
                    GridviewStudent.Visible = false;
                    Gridview.Visible = false;
                    Panel1.Visible = false;
                    GridviewStudent.Visible = false;
                    PanelPRAISE.Visible = false;
                    Panel5.Visible = false;
                    GridView5.Visible = false;
                    lbl5.Visible = false;
                    Panel3.Visible = false;
                }
                if (institution == Session["InstituteId"].ToString())
                {
                    DataTable dt = new DataTable();
                    dt = obj.CountRemainingPoints(txtEmployeecode.Text);
                    if (dt.Rows.Count > 0)
                    {
                        GridView1.Columns[2].FooterText = "Total";
                        GridView1.Columns[3].FooterText = dt.Rows[0]["count1"].ToString();
                        GridView1.Columns[4].FooterText = dt.Rows[0]["count2"].ToString();
                        GridView1.DataSource = dt;
                        GridView1.DataBind();
                        GridView1.Visible = true;
                    }
                    {
                        GridView1.Visible = true;
                        GridView1.DataBind();
                    }
                }
                else
                {
                    Gridview.Visible = false;
                    Panel1.Visible = false;
                    GridviewStudent.Visible = false;
                    PanelPRAISE.Visible = false;
                    Panel5.Visible = false;
                    GridView5.Visible = false;
                    lbl5.Visible = false;
                    Panel1.Visible = false;
                    Panel3.Visible = false;
                    GridviewStudent.Visible = false;
                }
            }
            else
            {
                Gridview.Visible = false;
                GridView1.Visible = false;
            }
        }
        else if (rolelist.Contains(17))
        {
            if (membertype != "S")
            {
                if (institution == Session["InstituteId"].ToString())
                {
                    GridviewStudent.Visible = false;
                    Gridview.Visible = true;
                    PanelPRAISE.Visible = false;
                    Panel5.Visible = true;
                    GridView5.Visible = true;
                    lbl5.Visible = true;
                }
                else
                {
                    Gridview.Visible = false;
                    Panel1.Visible = false;
                    GridviewStudent.Visible = false;
                    PanelPRAISE.Visible = false;
                    Panel5.Visible = false;
                    GridView5.Visible = false;
                    lbl5.Visible = false;
                    Panel1.Visible = false;
                    Panel3.Visible = false;
                    GridviewStudent.Visible = false;
                }
            }
            else
            {
                Gridview.Visible = false;
                GridView1.Visible = false;
            }
        }
        else if ((rolelist.Contains(19)) || rolelist.Contains(7))
        {
            if (membertype == "S")
            {
                if (institution == Session["InstituteId"].ToString())
                {
                    GridviewStudent.DataBind();
                    GridviewStudent.Visible = true;
                    GridView1.Visible = true;
                    Gridview.Visible = false;
                    PanelPRAISE.Visible = true;
                    Panel5.Visible = false;
                }
                else
                {
                    Panel1.Visible = false;
                    GridviewStudent.Visible = false;
                    Gridview.Visible = false;
                    Panel1.Visible = false;
                    GridviewStudent.Visible = false;
                    PanelPRAISE.Visible = false;
                    Panel5.Visible = false;
                    GridView5.Visible = false;
                    lbl5.Visible = false;
                    Panel3.Visible = false;
                }

            }
            else
            {

                if (institution == Session["InstituteId"].ToString())
                {
                    GridviewStudent.Visible = false;
                    Gridview.Visible = true;
                    PanelPRAISE.Visible = false;
                    Panel5.Visible = true;
                    GridView5.Visible = true;
                    lbl5.Visible = true;
                }
                else
                {
                    Gridview.Visible = false;
                    Panel1.Visible = false;
                    GridviewStudent.Visible = false;
                    PanelPRAISE.Visible = false;
                    Panel5.Visible = false;
                    GridView5.Visible = false;
                    lbl5.Visible = false;
                    Panel1.Visible = false;
                    Panel3.Visible = false;
                    GridviewStudent.Visible = false;
                }
            }
            if (institution == Session["InstituteId"].ToString())
            {
                DataTable dt = new DataTable();
                dt = obj.CountRemainingPoints(txtEmployeecode.Text);
                if (dt.Rows.Count > 0)
                {
                    GridView1.Columns[2].FooterText = "Total";
                    GridView1.Columns[3].FooterText = dt.Rows[0]["count1"].ToString();
                    GridView1.Columns[4].FooterText = dt.Rows[0]["count2"].ToString();
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                    GridView1.Visible = true;
                }
                {
                    GridView1.Visible = true;
                    GridView1.DataBind();
                }
            }
            else
            {
                Gridview.Visible = false;
                Panel1.Visible = false;
                GridviewStudent.Visible = false;
                PanelPRAISE.Visible = false;
                Panel5.Visible = false;
                GridView5.Visible = false;
                lbl5.Visible = false;
                Panel1.Visible = false;
                Panel3.Visible = false;
                GridviewStudent.Visible = false;
            }

        }
        else
        {
            //if (Session["Role"].ToString() == "11")
            //{
            table.Visible = false;
            txtEmployeecode.Text = Session["UserId"].ToString();
            txtEmployeecode.Visible = false;
            lbl1.Visible = true;
            Panel1.Visible = true;
            Panel3.Visible = true;
            Panel5.Visible = true;
            lbl5.Visible = true;
            Gridview.Visible = true;
            GridView1.Visible = true;
            GridView5.Visible = true;
            table1.Visible = true;
            GridviewStudent.Visible = false;
            PanelPRAISE.Visible = false;
            lblnote.Text = "Note: For any queries please contact Directorate of Research (help.rms@manipal.edu)";
            //IncentiveBusiness obj = new IncentiveBusiness();
            DataTable dt = new DataTable();
            dt = obj.CountRemainingPoints(txtEmployeecode.Text);
            if (dt.Rows.Count > 0)
            {
                GridView1.Columns[2].FooterText = "Total";
                GridView1.Columns[3].FooterText = dt.Rows[0]["count1"].ToString();
                GridView1.Columns[4].FooterText = dt.Rows[0]["count2"].ToString();
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
            {
                GridView1.DataBind();
            }
        }
            //else
            //{
            //    table.Visible = true;
            //    txtEmployeecode.Text = "";
            //    lbl1.Visible = false;
            //    Panel1.Visible = false;
            //    Panel3.Visible = false;
            //    Panel5.Visible = true;
            //    lbl5.Visible = true;
            //    //IncentiveBusiness obj = new IncentiveBusiness();
            //    string membertype1 = obj.SelectMemberType(txtEmployeecode.Text.Trim());
            //    if (membertype1 == "S")
            //    {
            //        GridviewStudent.Visible = true;
            //    }
            //    else
            //    {
            //        Gridview.Visible = false;
            //    }
            //    Gridview.Visible = false;
            //    GridView1.Visible = false;
            //    GridView5.Visible = true;
            //}

        
    }

    protected void grvMergeHeader_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header,
                                                        DataControlRowState.Insert);  //creating new Header Type 
            TableCell HeaderCell = new TableCell(); //creating HeaderCell
            HeaderCell.Text = "Employee Details";
            HeaderCell.ColumnSpan = 2;
            HeaderGridRow.Cells.Add(HeaderCell);//Adding HeaderCell to header.

            HeaderCell = new TableCell();
            HeaderCell.Text = "Points Migrated [Feb 2017]";
            HeaderCell.ColumnSpan = 2;
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = "Current Balalnce";
            HeaderCell.ColumnSpan = 2;
            HeaderGridRow.Cells.Add(HeaderCell);

            Gridview.Controls[0].Controls.AddAt(0, HeaderGridRow);
        }


    }


    public void GridView_RowDataBound(Object sender, GridViewRowEventArgs e)
    {

        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{

        //    if (e.Row.Cells[2].Text == "&nbsp;" || e.Row.Cells[2].Text == "")
        //    {
        //        e.Row.Cells[2].Text = "0";

        //    }
        //    if (e.Row.Cells[3].Text == "&nbsp;" || e.Row.Cells[3].Text == "")
        //    {
        //        e.Row.Cells[3].Text = "0";

        //    }
        //    if (e.Row.Cells[4].Text == "&nbsp;" || e.Row.Cells[4].Text == "")
        //    {
        //        e.Row.Cells[4].Text = "0";

        //    }
        //    if (e.Row.Cells[5].Text == "&nbsp;" || e.Row.Cells[5].Text == "")
        //    {
        //        e.Row.Cells[5].Text = "0";

        //    }
        //}

    }

    public void GridView_RowDataBound1(Object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string total = e.Row.Cells[3].Text;
            string description = e.Row.Cells[1].Text;
            if (total.Contains("-"))
            {
                if (description != "Adjustment")
                {
                    e.Row.Cells[3].Text = total.Substring(1, total.Length - 1);
                }
            }
            string utltotal = e.Row.Cells[4].Text;
            if (utltotal.Contains("-"))
            {
                e.Row.Cells[4].Text = utltotal.Substring(1, utltotal.Length - 1);
            }

            e.Row.Cells[6].Attributes.Add("style", "word-break:break-all;word-wrap:break-word;");
        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            string total = e.Row.Cells[3].Text;
            if (total.Contains("-"))
            {
                e.Row.Cells[3].Text = total.Substring(1, total.Length - 1);
            }
            string utltotal = e.Row.Cells[4].Text;
            if (utltotal.Contains("-"))
            {
                e.Row.Cells[4].Text = utltotal.Substring(1, utltotal.Length - 1);
            }
        }

    }

    public void GridView5_RowDataBound2(Object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            e.Row.Cells[1].Attributes.Add("style", "word-break:break-all;word-wrap:break-word;");
        }



    }

    protected void GridviewStudent_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header,
                                                        DataControlRowState.Insert);  //creating new Header Type 
            TableCell HeaderCell = new TableCell(); //creating HeaderCell
            HeaderCell.Text = "Student Details";
            HeaderCell.ColumnSpan = 5;
            HeaderGridRow.Cells.Add(HeaderCell);//Adding HeaderCell to header.

            //HeaderCell = new TableCell();
            //HeaderCell.Text = "Points Migrated [Feb 2017]";
            //HeaderCell.ColumnSpan = 2;
            //HeaderGridRow.Cells.Add(HeaderCell);

            //HeaderCell = new TableCell();
            //HeaderCell.Text = "Current Balalnce";
            //HeaderCell.ColumnSpan = 2;
            //HeaderGridRow.Cells.Add(HeaderCell);

            GridviewStudent.Controls[0].Controls.AddAt(0, HeaderGridRow);
        }


    }

    protected void grvMergeHeader_RowCreated1(object sender, GridViewRowEventArgs e)
    {
        DataSet ds = new DataSet();
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();

            HeaderCell.Text = "PRAISE Articles";
            HeaderCell.ColumnSpan = 4;
            HeaderGridRow.Cells.Add(HeaderCell);

            GridViewPRAISE.Controls[0].Controls.AddAt(0, HeaderGridRow);


        }
    }

    protected void grvMergeHeader(object sender, GridViewRowEventArgs e)
    {
        DataSet ds = new DataSet();
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell HeaderCell = new TableCell();

            HeaderCell.Text = "FAIR Articles";
            HeaderCell.ColumnSpan = 4;
            HeaderGridRow.Cells.Add(HeaderCell);

            GridViewFAIR.Controls[0].Controls.AddAt(0, HeaderGridRow);


        }
    }
    protected void btnExpt_Click(object sender, EventArgs e)
    {
        DataTable dy = null;

        DataTable dz = null;

        IncentiveBusiness obj = new IncentiveBusiness();

        string MemberId = txtEmployeecode.Text;
        DataTable dt = obj.GetArticalWisePoints(MemberId);


        if (dt.Rows.Count == 0)
        {

            GridView5.Visible = false;
            Panel1.Visible = false;
            //   noField.Visible = true;
            //  noField.Text = "No Records Found";

        }
        else
        {
            ViewState["GridViewStudentlist"] = dt;

        }

        //  DataTable dt = (DataTable)ViewState["GridViewStudentlist"];

        string filename = "ArticalWisePointExcel.xls";

        string headerTable = @"<table width='100%' border='1'  align='center'  class='TestCssStyle'><h4>Artical Wise Points Summary<br/></h4></table>";


        HttpContext.Current.Response.Write(headerTable);



        System.IO.StringWriter tw = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
        DataGrid dgGrid = new DataGrid();
        dgGrid.DataSource = dt;
        dgGrid.DataBind();
        dgGrid.ShowHeader = true;
        dgGrid.RenderControl(hw);

        Response.ContentType = "application/vnd.ms-excel";
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename + "");

        this.EnableViewState = false;
        Response.Write(tw.ToString());
        Response.End();

    }
}