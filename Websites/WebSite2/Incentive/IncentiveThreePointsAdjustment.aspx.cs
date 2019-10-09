using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Incentive_IncentiveThreePointsAdjustment : System.Web.UI.Page
{
    private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    IncentiveBusiness B = new IncentiveBusiness();
    IncentivePoint obj = new IncentivePoint();
    public string pageID = "L108";

    protected void Page_Load(object sender, EventArgs e)
    {
        //Panel1.Visible = true;
        if (!IsPostBack)
        {
            if (!Session["authPage"].ToString().Contains("$" + pageID + "$"))
            {
                string unacces = "Unauthorized Acces!!! Login Again";
                Response.Redirect("~/Login.aspx?val=" + unacces);
            }
            setModalWindow(sender, e);
           
        }
        //this.RegisterPostBackControl();
    }

    private void setModalWindow(object sender, EventArgs e)
    {
        Panel1.Visible = true;
        popGridSearch.DataSourceID = "SqlDataSourceMember";
        SqlDataSourceMember.DataBind();
        popGridSearch.DataBind();
        popGridSearch.Visible = true;
    }


    protected void showPop(object sender, EventArgs e)
    {
        ModalPopupExtender2.Show();
    }


    protected void popSelected(Object sender, EventArgs e)
    {

        txtboxMemberId.Text = "";
        txtcurbal.Text = "";
        txtRemarks.Text = "";
        txtBasePoint.Text = "";
        txtSNIPSJRPoint.Text = "";
        //txtThresholdPoint.Text = "";
        txtTotalPoint.Text = "";
        txtReferenceId.Text = "";
        btnUpdate.Enabled = true;
        popGridSearch.Visible = true;
        GridViewRow row = popGridSearch.SelectedRow;
        string memberid = row.Cells[1].Text;
        txtboxMemberId.Text = row.Cells[1].Text;
        txtReferenceId.Text = row.Cells[3].Text;

        IncentiveBusiness busobj = new IncentiveBusiness();
        PublishData obj = new PublishData();
        obj = busobj.SelectPublicationData(memberid, row.Cells[3].Text.Trim());
        int ThresholdPublicationNo = Convert.ToInt16(ConfigurationManager.AppSettings["ThresholdPublicationNo"]);
        int count = busobj.CountThresholdPublicationPoint(obj);

        obj = busobj.getIsStudentAuthor(txtReferenceId.Text);
        //point 3 (crosses 6 publication) is awarded once in a year
        //if (count > ThresholdPublicationNo)
        //{
        //    if (obj.MUNonMU == "M")
        //    {
        //        txtThresholdPoint.Enabled = true;
        //    }
        //    else
        //    {
        //        txtThresholdPoint.Enabled = false;
        //    }
        //}
        //else
        //{
        //    txtThresholdPoint.Enabled = false;
        //    txtThresholdPoint.Text = "";
        //}

        if (obj.AuthorType == "P" || obj.isCorrAuth == "Y")
        {
            txtSNIPSJRPoint.Enabled = true;
        }
        else
        {
            txtSNIPSJRPoint.Enabled = true;
            txtSNIPSJRPoint.Text = "";
        }

        IncentivePoint obj1 = new IncentivePoint();
        obj1 = busobj.SelectMemberCurBalance(memberid);
        txtcurbal.Text = obj1.CurrentBalance.ToString();
        Session["IsStudent"] = obj.IsStudentAuthor;
        Session["Title"] = obj.TitleWorkItem;

    }




    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        //EditUpdatePanel.Update();
        if (!Page.IsValid)
        {
            return;
        }
        try
        {
     
                if (txtBasePoint.Text.Trim() == "" && txtSNIPSJRPoint.Text.Trim() == "")
                {
                    string CloseWindow1 = "alert('Please enter atleast one point')";
                    ScriptManager.RegisterStartupScript(EditUpdatePanel, EditUpdatePanel.GetType(), "alert", CloseWindow1, true);
                    return;
                }

                bool result = B.CheckPublcationId(txtReferenceId.Text.Trim(), txtboxMemberId.Text.Trim());
                if (result == false)
                {
                    string CloseWindow1 = "alert('Invalid reference id')";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "newWindow", CloseWindow1, true);
                    return;
                }
                obj.MemberId = txtboxMemberId.Text.Trim();
                obj.CurrentBalance = Convert.ToDouble(txtcurbal.Text.Trim());
                if (txtBasePoint.Text.Trim() != "")
                {
                    obj.BasePoint = Convert.ToDouble(txtBasePoint.Text.Trim());
                }
                if (txtSNIPSJRPoint.Text.Trim() != "")
                {
                    obj.SNIPSJRPoint = Convert.ToDouble(txtSNIPSJRPoint.Text.Trim());
                }
                //point 3 (crosses 6 publication) is awarded once in a year
                //if (txtThresholdPoint.Text.Trim() != "")
                //{
                //    obj.ThresholdPoint = Convert.ToDouble(txtThresholdPoint.Text.Trim());
                //}
                obj.TotalPoint = (obj.BasePoint + obj.SNIPSJRPoint + obj.ThresholdPoint);
                obj.ReferenceNumber = txtReferenceId.Text.Trim();
                obj.Remarks = txtRemarks.Text.Trim();
                obj.CurrentBalance = obj.CurrentBalance + obj.TotalPoint;
                obj.TransactionType = "ADJ";
                if (obj.TotalPoint > 0)
                {
                    obj.NumberType = "Added";
                    Session["Numbertype"] = "Added";
                }
                else
                {
                    obj.NumberType = "Removed";
                    Session["Numbertype"] = "Removed";
                }
                bool result1 = B.UpdateCurBal(obj); //Business layer

                if (result1 == true)
                {
                    string CloseWindow1 = "alert('Incentive Point Saved successfully')";
                    //ScriptManager.RegisterStartupScript(EditUpdatePanel, EditUpdatePanel.GetType(), "alert", CloseWindow1, true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "newWindow", CloseWindow1, true);
                    btnUpdate.Enabled = false;
                    txtcurbal.Text = obj.CurrentBalance.ToString();
                    SendMail();
                    txtBasePoint.Text = "";
                    txtSNIPSJRPoint.Text = "";
                    txtRemarks.Text = "";
                    txtTotalPoint.Text = "";
                   
                
                  
                }
                else
                {
                    string CloseWindow1 = "alert('Problem while updating the Incentive point')";
                   ScriptManager.RegisterStartupScript(EditUpdatePanel, EditUpdatePanel.GetType(), "alert", CloseWindow1, true);
                    btnUpdate.Enabled = false;
                }
            
           
            

        }
        catch (Exception ex)
        {
            log.Error(ex.StackTrace);
            log.Error(ex.Message);
            ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Error')</script>");
        }
    }

    private void SendMail()
    {
        EmailDetails details = new EmailDetails();
         IncentiveBusiness b = new IncentiveBusiness();

                    SendMailObject obj = new SendMailObject();
                    Business e = new Business();
        try
        {
            bool resultv = false;
            int rowIndex = 0;
              string emailid = null;
            details.Module = "IPE";
            string AuthorName = null ;
            string Isstudent = null;
            details.EmailSubject = "Incentive Point Adjustment";                                
                    string empcode = txtboxMemberId.Text;
               string type=e.selectMemberType(empcode,txtReferenceId.Text);  
                        if (type == "M")
                        {
                            //Isstudent = "N";
                            emailid = b.SelectAuthorEmailId(empcode);
                            AuthorName = b.SelectAuthorName(empcode);
                            Session["AuthorName"] = AuthorName;
                            if (emailid == "")
                            {
                                int result;
                                //string AuthorName;
                                AuthorName = b.SelectAuthorName(empcode);
                                result = e.insertEmailtrackerIncentive(AuthorName, details, txtReferenceId.Text);
                               
                            }
                        }
                        else
                        {
                            //Isstudent = "S";
                            emailid = b.SelectStudentEmailId(empcode, txtReferenceId.Text);
                            AuthorName = b.SelectStudentAuthorName(empcode, txtReferenceId.Text);
                            Session["AuthorName"] = AuthorName;
                            if (emailid == "")
                            {
                                int result;
                              
                                AuthorName = b.SelectStudentAuthorName(empcode,  txtReferenceId.Text);
                                result = e.insertEmailtrackerIncentive(AuthorName, details, txtReferenceId.Text);
                                //Session["AuthorName"] = AuthorName;
                            }
                        }

                        details.FromEmail = ConfigurationManager.AppSettings["FromAddress"].ToString();

                        if (emailid != "")
                        {
                            details.ToEmail = emailid;
                        }
                        string hremailid = b.SelectHRMailID(empcode, type, txtReferenceId.Text);

                        string InstWiseHRMailid = b.SelectInstwiseHRMailid(empcode, type, txtReferenceId.Text);
                        if (details.ToEmail != null && details.ToEmail != "")
                        {

                            if (InstWiseHRMailid != null)
                            {
                                if (InstWiseHRMailid != "")
                                {
                                    if (details.ToEmail != null)
                                    {
                                        details.ToEmail = details.ToEmail + "," + InstWiseHRMailid;
                                    }
                                    else
                                    {
                                        details.ToEmail = InstWiseHRMailid;


                                    }
                                }
                                else
                                {
                                    // details.ToEmail = details.ToEmail;
                                }
                            }
                            else
                            {
                                // details.ToEmail = details.ToEmail;
                            }
                        }
                        else
                        {
                            if (InstWiseHRMailid != null)
                            {
                                if (InstWiseHRMailid != "")
                                {
                                    if (details.ToEmail != null)
                                    {
                                        details.ToEmail = InstWiseHRMailid;
                                    }
                                    else
                                    {
                                        details.ToEmail = InstWiseHRMailid;
                                    }
                                }
                                else
                                {
                                    // details.ToEmail = details.ToEmail;
                                }
                            }
                            else
                            {
                                // details.ToEmail = details.ToEmail;
                            }
                        }
                        ArrayList list = new ArrayList();
                        list = b.SelectHODMailid(empcode, type, txtReferenceId.Text);
                        for (int j = 0; j < list.Count; j++)
                        {
                            if (j == 0)
                            {
                                if (list[j].ToString() != "")
                                {
                                    details.HODmailid = list[j].ToString();
                                }
                            }
                            else
                            {
                                if (list[j].ToString() != "")
                                {
                                    details.HODmailid = details.HODmailid + ',' + list[j].ToString();
                                }
                            }

                        }
                        if (details.ToEmail != null && details.ToEmail != "")
                        {
                            if (hremailid != null)
                            {
                                if (hremailid != "")
                                {
                                    details.ToEmail = details.ToEmail + "," + hremailid;
                                }
                                else
                                {
                                    // details.ToEmail = details.ToEmail;
                                }
                            }
                            else
                            {
                                // details.ToEmail = details.ToEmail;
                            }
                        }
                        else
                        {
                            if (hremailid != null)
                            {
                                if (hremailid != "")
                                {
                                    details.ToEmail = hremailid;
                                }
                                else
                                {
                                    // details.ToEmail = details.ToEmail;
                                }
                            }
                            else
                            {
                                // details.ToEmail = details.ToEmail;
                            }
                        }
                        if (details.ToEmail != null && details.ToEmail != "")
                        {
                            if (details.HODmailid != null)
                            {
                                if (details.HODmailid != "")
                                {
                                    details.ToEmail = details.ToEmail + "," + details.HODmailid;
                                    //details.ToEmail = details.ToEmail + "," + hremailid + "," + details.HODmailid;
                                }
                                else
                                {
                                    //details.ToEmail = details.ToEmail;
                                }
                            }
                            else
                            {
                                //details.ToEmail = details.ToEmail;
                            }
                        }
                        else
                        {
                            if (details.HODmailid != null)
                            {
                                if (details.HODmailid != "")
                                {
                                    details.ToEmail = details.HODmailid;
                                    //details.ToEmail = details.ToEmail + "," + hremailid + "," + details.HODmailid;
                                }
                                else
                                {
                                    //details.ToEmail = details.ToEmail;
                                }
                            }
                            else
                            {
                                //details.ToEmail = details.ToEmail;
                            }
                        }
                        // details.CCEmail = hremailid;
                        // details.CCEmail = Session["emailId"].ToString();
                        details.Module = "IPE";
                        details.EmailSubject = "Incentive Point Adjustment";
                        //details.Type = DropDownListPublicationEntry.SelectedValue;
                        details.Id = txtReferenceId.Text;
                        string FooterText = ConfigurationManager.AppSettings["FooterText"].ToString();
                        string isStudent = Session["IsStudent"].ToString();

                        if (isStudent == "Y")
                        {
                            if (type == "S")
                            {
                                details.MsgBody = "<span style=\"font-size: 10pt; color: #3300cc; font-family: Verdana\"><h4>Dear Sir/Madam,</h4> <br>" +
                                    "<p>For Certificate of Appreciation and encashment of research incentive points, request you to contact student research section of your institution/department along with the Bank detail form available in RMS Portal.</p>" + "<b>Incentive points with the rating  : " + txtTotalPoint.Text + "" + Session["Numbertype"].ToString() + "<br>" +
                                         "<br>" +
                                            "Author Name : " + Session["AuthorName"].ToString() + "<br>" +
                                         "Publication Id : " + txtReferenceId.Text + "<br>" +
                                         "Article Name :" + Session["Title"].ToString() + "<br>" + "<br>" + FooterText +
                                        " </b><br><b> </b></span>";
                            }
                            else
                            {
                                details.MsgBody = "<span style=\"font-size: 10pt; color: #3300cc; font-family: Verdana\"><h4>Dear Sir/Madam,</h4> <br>" +
                                    //"<b> Incentive points with the rating '" + total.Text + "' added. <br> " +
                                         "<b>Incentive points with the rating  : " + txtTotalPoint.Text + " " + Session["Numbertype"].ToString() + "<br>" +
                                         "<br>" +
                                            "Author Name : " + Session["AuthorName"].ToString() + "<br>" +
                                         "Publication Id : " + txtReferenceId.Text + "<br>" +
                                          "Article Name :" + Session["Title"].ToString() + "<br>" + "<br>" + FooterText +
                                        " </b><br><b> </b></span>";
                            }
                        }

                        else
                        {
                            if (type == "S")
                            {
                                details.MsgBody = "<span style=\"font-size: 10pt; color: #3300cc; font-family: Verdana\"><h4>Dear Sir/Madam,</h4> <br>" +
                                   "<p>For encashment of research incentive points, request you to contact student research section of your institution/department along with the Bank detail form available in RMS Portal.</p>" + "<b>Incentive points with the rating  : " + txtTotalPoint.Text + " " + Session["Numbertype"].ToString() + "<br>" +
                                    //"<b> Incentive points with the rating '" + total.Text + "' added. <br> " +                              
                                         "<br>" +
                                            "Author Name : " + Session["AuthorName"].ToString() + "<br>" +
                                         "Publication Id : " + txtReferenceId.Text + "<br>"  +
                                          "Article Name :" + Session["Title"].ToString() + "<br>" + "<br>" + FooterText +
                                        " </b><br><b> </b></span>";
                            }
                            else
                            {
                                details.MsgBody = "<span style=\"font-size: 10pt; color: #3300cc; font-family: Verdana\"><h4>Dear Sir/Madam,</h4> <br>" +
                                    //"<b> Incentive points with the rating '" + total.Text + "' added. <br> " +
                                        "<b>Incentive points with the rating  : " + txtTotalPoint.Text + " " + Session["Numbertype"].ToString() + "<br>" +
                                        "<br>" +
                                           "Author Name : " + Session["AuthorName"].ToString() + "<br>" +
                                        "Publication Id : " + txtReferenceId.Text + "<br>" +
                                         "Article Name :" + Session["Title"].ToString() + "<br>" + "<br>" + FooterText +
                                       " </b><br><b> </b></span>";
                            }
                        }

                        if (details.ToEmail != "" && details.ToEmail != null)
                        {
                            resultv = obj.InsertIntoEmailQueue(details);

                        }
                        IncentiveData obj3 = new IncentiveData();
                        IncentiveBusiness C = new IncentiveBusiness();


                        if (emailid == "")
                        {
                            string AuthorName1 = Session["AuthorName"].ToString();
                            obj3 = C.CheckUniqueIdIncentive(txtReferenceId.Text, "JA", details);
                            int data = C.updateEmailtrackerIncentive(txtReferenceId.Text, "JA", details, obj3, AuthorName1);
                        }
                    
                  if (resultv == true)
            {

                string CloseWindow1 = "alert('Mail Sent successfully')";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "newWindow", CloseWindow1, true);
               
            }
            else
            {

                string CloseWindow1 = "alert('Problem while sending mail')";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "newWindow", CloseWindow1, true);
               
            }
                

            }
            
        
        catch (Exception ex)
        {
            string CloseWindow1 = "alert('Problem while sending mail')";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "newWindow", CloseWindow1, true);
            log.Error(ex.Message);
            log.Error(ex.StackTrace);
          
        }
    }
    protected void popGridIncenAdjust_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        popGridSearch.Visible = true;
        GridViewRow row = popGridSearch.SelectedRow;
        string memberid = row.Cells[1].Text;
        txtboxMemberId.Text = memberid;
        popGridSearch.DataBind();

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (txtmidSearch.Text.Trim() == "")
        {
            string CloseWindow1 = "alert('Please Enter Publication Id')";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "newWindow", CloseWindow1, true);
            popGridSearch.DataSourceID = "SqlDataSourceMember";
            popGridSearch.DataBind();
            popGridSearch.Visible = true;
        }

        else
        {
            string type = txtmidSearch.Text.Substring(0, 2);
            if (type != "JA")
            {
                string publicationid = "JA" + txtmidSearch.Text.Trim();
                SqlDataSourceMember.SelectParameters.Clear();
                SqlDataSourceMember.SelectParameters.Add("ReferenceNumber", publicationid);

                SqlDataSourceMember.SelectCommand = "select  MemberId,UPPER(AuthorName) as MemberName,ReferenceNumber from Member_Incentive_Point_Transaction, Publishcation_Author where Publishcation_Author.EmployeeCode=Member_Incentive_Point_Transaction.MemberId and " +
      "TransactionType='IPE' and ReferenceNumber=@ReferenceNumber and Publishcation_Author.TypeOfEntry+PaublicationID=Member_Incentive_Point_Transaction.ReferenceNumber and AuthorType!='N' and Member_Incentive_Point_Transaction.Isdeleted='N'";
            }
            else
            {
                SqlDataSourceMember.SelectParameters.Clear();
                SqlDataSourceMember.SelectParameters.Add("ReferenceNumber", txtmidSearch.Text);

                SqlDataSourceMember.SelectCommand = "select  MemberId,UPPER(AuthorName) as MemberName,ReferenceNumber from Member_Incentive_Point_Transaction, Publishcation_Author where Publishcation_Author.EmployeeCode=Member_Incentive_Point_Transaction.MemberId and " +
 "TransactionType='IPE' and ReferenceNumber=@ReferenceNumber and Publishcation_Author.TypeOfEntry+PaublicationID=Member_Incentive_Point_Transaction.ReferenceNumber and AuthorType!='N'and Member_Incentive_Point_Transaction.Isdeleted='N'";
            }
            popGridSearch.DataSourceID = "SqlDataSourceMember";
            popGridSearch.DataBind();
            popGridSearch.Visible = true;

        }

        ModalPopupExtender2.Show();

    }
    protected void btnexit_Click(object sender, EventArgs e)
    {
        txtmidSearch.Text = "";
        popGridSearch.DataBind();
    }
    protected void txtBasePoint_TextChanged(object sender, EventArgs e)
    {
        double basepoint = 0.0, snipsjrpoint = 0.0, ThresholdPoint = 0.0;
        if (txtBasePoint.Text == "")
        {
            basepoint = 0.0;
        }
        else
        {
            basepoint = Convert.ToDouble(txtBasePoint.Text);
        }
        if (txtSNIPSJRPoint.Text == "")
        {
            snipsjrpoint = 0.0;
        }
        else
        {
            snipsjrpoint = Convert.ToDouble(txtSNIPSJRPoint.Text);
        }
        //if (txtThresholdPoint.Text == "")  //point 3 (crosses 6 publication) is awarded once in a year
        //{
        //    ThresholdPoint = 0.0;
        //}
        //else
        //{
        //    ThresholdPoint = Convert.ToDouble(txtThresholdPoint.Text);
        //}
        double total = basepoint + snipsjrpoint + ThresholdPoint;
        txtTotalPoint.Text = total.ToString();
    }

    protected void radioincentive_SelectedIndexChanged(object sender, EventArgs e)
    {
        //EditUpdatePanel.Update();
        if (radioincentive.SelectedValue == "1")
        {
            Panel2.Visible = true;
            Panel1.Visible = true;
            PanelPatent.Visible = false;
            Panel4.Visible = false;
        }
        else 
        {
            Panel2.Visible = false;
            Panel1.Visible = false;
            PanelPatent.Visible = true;
            Panel4.Visible = true;
        }
    }
    protected void BtnPatentSearch_Click(object sender, EventArgs e)
    {
        if (Txtid.Text.Trim() == "")
        {
            string CloseWindow1 = "alert('Please Enter Patent Id')";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "newWindow", CloseWindow1, true);
            GridViewPatentSearch.DataSourceID = "SqlDataSource1";
            GridViewPatentSearch.DataBind();
            GridViewPatentSearch.Visible = true;
        }

        else
        {
            string type = Txtid.Text.Substring(0, 2);
            string PatentId = Txtid.Text.Trim();
            SqlDataSource1.SelectParameters.Clear();
            SqlDataSource1.SelectParameters.Add("ReferenceNumber", PatentId);

            SqlDataSource1.SelectCommand = "select  MemberId,UPPER(InvestigatorName) as MemberName,ReferenceNumber from Member_Incentive_Point_Transaction, Patent_Inventor where Patent_Inventor.EmployeeCode=Member_Incentive_Point_Transaction.MemberId and " +
                    "TransactionType='PPE' and ReferenceNumber=@ReferenceNumber and Patent_Inventor.Id=Member_Incentive_Point_Transaction.ReferenceNumber ";

                GridViewPatentSearch.DataSourceID = "SqlDataSource1";
                GridViewPatentSearch.DataBind();
                GridViewPatentSearch.Visible = true;

        }

        ModalPopupExtender1.Show();
    }
    protected void GridViewPatentSearch_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridViewPatentSearch.Visible = true;
        GridViewRow row = GridViewPatentSearch.SelectedRow;
        string memberid = row.Cells[1].Text;
        txtmemberPatent.Text = memberid;
        GridViewPatentSearch.DataBind();
    }
    protected void GridViewPatentSearch_SelectedIndexChanged(object sender, EventArgs e)
    {

        txtmemberPatent.Text = "";
        TxtcurrentPatent.Text = "";
        txttotalPointPatent.Text = "";
        //txtBasePoint.Text = "";
        //txtSNIPSJRPoint.Text = "";
        //txtThresholdPoint.Text = "";
        txttotalPointPatent.Text = "";
        txtreferencePatent.Text = "";
        btnupdatePatent.Enabled = true;
        GridViewPatentSearch.Visible = true;
        GridViewRow row = GridViewPatentSearch.SelectedRow;
        string memberid = row.Cells[1].Text;
        txtmemberPatent.Text = row.Cells[1].Text;
        txtreferencePatent.Text = row.Cells[3].Text;

        PatentBusiness busobj = new PatentBusiness();
        Patent obj = new Patent();
        obj = busobj.SelectPatentData(memberid, row.Cells[3].Text.Trim());
        //int ThresholdPublicationNo = Convert.ToInt16(ConfigurationManager.AppSettings["ThresholdPublicationNo"]);
        //int count = busobj.CountThresholdPublicationPoint(obj);
        //point 3 (crosses 6 publication) is awarded once in a year
        //if (count > ThresholdPublicationNo)
        //{
        //    if (obj.MUNonMU == "M")
        //    {
        //        txtThresholdPoint.Enabled = true;
        //    }
        //    else
        //    {
        //        txtThresholdPoint.Enabled = false;
        //    }
        //}
        //else
        //{
        //    txtThresholdPoint.Enabled = false;
        //    txtThresholdPoint.Text = "";
        //}

        //if (obj.AuthorType == "P" || obj.isCorrAuth == "Y")
        //{
        //    txtSNIPSJRPoint.Enabled = true;
        //}
        //else
        //{
        //    txtSNIPSJRPoint.Enabled = true;
        //    txtSNIPSJRPoint.Text = "";
        //}

        IncentivePoint obj1 = new IncentivePoint();
        obj1 = busobj.SelectPatentMemberCurBalance(memberid);
        TxtcurrentPatent.Text = obj1.CurrentBalance.ToString();

    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        Txtid.Text = "";
        GridViewPatentSearch.DataBind();
    }
    protected void showPop1(object sender, EventArgs e)
    {
        ModalPopupExtender1.Show();
    }
    protected void btnupdatePatent_Click(object sender, EventArgs e)
    {
        //if (!Page.IsValid)
        //{
        //    return;
        //}
         try
        {

            if (txttotalPointPatent.Text.Trim() == "")
            {
                string CloseWindow1 = "alert('Please enter total point')";
                ScriptManager.RegisterStartupScript(EditUpdatePanel, EditUpdatePanel.GetType(), "alert", CloseWindow1, true);
                return;
            }
            if (txtPatentRemarks.Text.Trim() == "")
            {
                string CloseWindow1 = "alert('Please enter Remarks')";
                ScriptManager.RegisterStartupScript(EditUpdatePanel, EditUpdatePanel.GetType(), "alert", CloseWindow1, true);
                return;
            }



            bool result = B.CheckPatentId(txtreferencePatent.Text.Trim(), txtmemberPatent.Text.Trim());
                if (result == false)
                {
                    string CloseWindow1 = "alert('Invalid reference id')";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "newWindow", CloseWindow1, true);
                    return;
                }
                obj.MemberId = txtmemberPatent.Text.Trim();
                obj.CurrentBalance = Convert.ToDouble(TxtcurrentPatent.Text.Trim());
                //if (txtBasePoint.Text.Trim() != "")
                //{
                //    obj.BasePoint = Convert.ToDouble(txtBasePoint.Text.Trim());
                //}
                //if (txtSNIPSJRPoint.Text.Trim() != "")
                //{
                //    obj.SNIPSJRPoint = Convert.ToDouble(txtSNIPSJRPoint.Text.Trim());
                //}
                //point 3 (crosses 6 publication) is awarded once in a year
                //if (txtThresholdPoint.Text.Trim() != "")
                //{
                //    obj.ThresholdPoint = Convert.ToDouble(txtThresholdPoint.Text.Trim());
                //}
                obj.TotalPoint =Convert.ToDouble( txttotalPointPatent.Text);
                obj.ReferenceNumber = txtreferencePatent.Text.Trim();
                obj.Remarks = txtPatentRemarks.Text.Trim();
                obj.CurrentBalance = obj.CurrentBalance + obj.TotalPoint;
                obj.TransactionType = "ADJ";
                bool result1 = B.UpdateCurBal(obj); //Business layer

                if (result1 == true)
                {
                    string CloseWindow1 = "alert('Incentive Point Saved successfully')";
                    //ScriptManager.RegisterStartupScript(EditUpdatePanel, EditUpdatePanel.GetType(), "alert", CloseWindow1, true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "newWindow", CloseWindow1, true);
                    btnupdatePatent.Enabled = false;
                    TxtcurrentPatent.Text = obj.CurrentBalance.ToString();
                    //txtBasePoint.Text = "";
                    //txtSNIPSJRPoint.Text = "";
                    txtPatentRemarks.Text = "";
                    txttotalPointPatent.Text = "";
                }
                else
                {
                    string CloseWindow1 = "alert('Incentive Point Saved successfully')";
                   // ScriptManager.RegisterStartupScript(EditUpdatePanel, EditUpdatePanel.GetType(), "alert", CloseWindow1, true);
                    btnupdatePatent.Enabled = false;
                }
            
           
            

        }
        catch (Exception ex)
        {
            log.Error(ex.StackTrace);
            log.Error(ex.Message);
            ClientScript.RegisterStartupScript(Page.GetType(), "validation1", "<script language='javascript'>alert('Error')</script>");
        }
    
    }
}