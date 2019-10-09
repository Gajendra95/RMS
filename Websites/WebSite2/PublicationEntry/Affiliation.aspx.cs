using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PublicationEntry_Affiliation : System.Web.UI.Page
{
    string empid;
    string EmailID;
    string idtype;
    string maheName;
    string hasdepartment;
    string sempid;
    string sEmailID;
    string sidtype;
    string smaheName;
    string shasdepartment;
    protected void Page_Load(object sender, EventArgs e)
    {
        LabelNote.Visible = false;
    }
    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        LabelNote.Visible = false;
        panel1.Visible = true;
        Business obj = new Business();
        User a = new User();

        if (RadBtnListEmpDetails.SelectedValue == "E")
        {
            empid = txtEmpDetails.Text.Trim();
            EmailID = "";
            if (empid == "")
            {
                panel1.Visible = false;
                string CloseWindow1 = "alert('Pleae Enter the Employee ID')";
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow1, true);
                return;
            }
            panel1.Visible = true;

        }
        else if (RadBtnListEmpDetails.SelectedValue == "M")
        {
            EmailID = txtEmpDetails.Text.Trim()+"@manipal.edu";
            empid = "";
            if (EmailID == "")
            {
                string CloseWindow1 = "alert('Pleae Enter the Email  ID')";
                ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow1, true);
                return;
            }
          
        }

        a = obj.getAuthorInstitutiondetails(empid, EmailID);

           string inst = a.InstituteId;
           int CenterCode = a.centerCode;
           string dept = a.Department_Id;
           if (a.InstituteId == "MAHEU")
           {
               a = obj.getAuthorAffiliationIDTypeMAHE(dept, empid);
               idtype = a.IDType;
               maheName = a.Mahedepartment;
               hasdepartment = a.hasdepartment;
               a = obj.getAuthorAffiliationdetailsforMAHE(empid, EmailID);
               LblauthorName.Text = a.Name;
               LblEmailID.Text = a.emailId;
               if (maheName != null)
               {
                   Lblinstitute.Visible = true;
                   LAffiliation.Visible = true;
                   LAffiliation.Text = maheName;
                   Lblinstitute.Text = "Manipal Academy of Higher Education, Manipal";
                   Lbldepartment.Visible = false;
               }
               else 
               {
                   LAffiliation.Visible = true;
                   LAffiliation.Text = "Manipal Academy of Higher Education, Manipal";
                   Lbldepartment.Visible = false;
                   Lblinstitute.Visible = false;
               }
           }
           else
           {

               a = obj.getAuthorAffiliationIDType(a.InstituteId);
               idtype = a.IDType;
               maheName = a.Mahedepartment;
               hasdepartment = a.hasdepartment;
               a = obj.getAuthorAffiliationdetails(empid, EmailID);
               LblauthorName.Text = a.Name;
               LblEmailID.Text = a.emailId;
               if (idtype == "I")
               {
                   if (hasdepartment == "N")
                   {
                       Label5.Visible = true;
                       Label5.Text = "Affiliation :";
                       Lblinstitute.Visible = true;
                       LAffiliation.Visible = true;
                       LAffiliation.Text = a.Institute_name;
                       Lblinstitute.Text = "Manipal Academy of Higher Education, Manipal";
                       Lbldepartment.Visible = false;
                   }
                   else if (hasdepartment == "Y")
                   {
                       Label5.Visible = true;
                       Label5.Text = "Affiliation :";
                       Lblinstitute.Visible = true;
                       LAffiliation.Visible = true;
                       Lbldepartment.Visible = true;
                       LAffiliation.Text = a.DepartmentName;
                       Lblinstitute.Text = a.Institute_name;
                       Lbldepartment.Text = "Manipal Academy of Higher Education, Manipal";
                   }
               }
               else if (idtype == "D")
               {
                   if (hasdepartment == "N")
                   {
                       Label5.Visible = true;
                       Label5.Text = "Affiliation :";
                       Lblinstitute.Visible = true;
                       LAffiliation.Visible = true;
                       LAffiliation.Text = maheName;
                       Lblinstitute.Text = "Manipal Academy of Higher Education, Manipal";
                       Lbldepartment.Visible = false;
                   }
                   else if (hasdepartment == "Y")
                   {
                       Label5.Visible = true;
                       Label5.Text = "Affiliation :";
                       Lblinstitute.Visible = true;
                       LAffiliation.Visible = true;
                       LAffiliation.Text = maheName;
                       Lblinstitute.Text = "Manipal Academy of Higher Education, Manipal";
                       Lbldepartment.Visible = false;
                   }
               }
           }
           if (CenterCode == 0)
           {
               LabelOR.Visible = false;
               Label2.Visible = false;
               LAffiliation1.Visible = false;
               Lblinstitute1.Visible = false;
               Lblinstitute2.Visible = false;
               Lbldepartment1.Visible = false;
           }
           else 
           {
               a = obj.getAuthorCenterAffiliationdetails(CenterCode);
               LabelOR.Visible = false;
               Label2.Visible = true;
               LAffiliation1.Visible = true;
               Lblinstitute1.Visible = false;
               Lblinstitute2.Visible = true;
               Lbldepartment1.Visible = true;
               if (a.Centername != null && a.SchoolName1 != null)
               {
                   LAffiliation1.Visible = true;
                   Lblinstitute2.Visible = true;
                   Lbldepartment1.Visible = true;
                   LAffiliation1.Text = a.Centername;
                   Lblinstitute2.Text = a.SchoolName1;
                   Lbldepartment1.Text = "Manipal Academy of Higher Education, Manipal";
               }
               else 
               {
                   a = obj.getAuthorOnlyCenterAffiliationdetails(CenterCode);
                   if (idtype == "I")
                   {
                       if (a.Centername != null)
                       {
                           LAffiliation1.Visible = true;
                           Lblinstitute2.Visible = true;
                           Lbldepartment1.Visible = false;
                           LAffiliation1.Text = a.Centername;
                           Lblinstitute2.Text = "Manipal Academy of Higher Education, Manipal";
                           Lbldepartment1.Text = "";
                       }
                   }
                   else
                   if (a.Centername != null)
                   {
                       Label5.Text = "Center Affiliation  :";
                       LAffiliation.Text = a.Centername;
                       Lblinstitute.Text = "Manipal Academy of Higher Education, Manipal";
                       Lbldepartment.Visible = false;
                       Lblinstitute.Visible = true;
                       Label5.Visible = true;
                       Lblinstitute.Visible = true;

                       LabelOR.Visible = false;
                       Label2.Visible = false;
                       LAffiliation1.Visible = false;
                       Lblinstitute1.Visible = false;
                       Lblinstitute2.Visible = false;
                       Lbldepartment1.Visible = false;
                       //Label2.Visible = true;
                       //LAffiliation1.Visible = true;
                       //Lblinstitute2.Visible = true;
                       //Lbldepartment1.Visible = false;
                       //LAffiliation1.Text = a.Centername;
                       //Lblinstitute2.Text = "Manipal Academy of Higher Education, Manipal";

                       //Label5.Visible = false;
                       //LAffiliation.Visible = false;
                       //Lblinstitute.Visible = false;
                       //Lbldepartment.Visible = false;
                       //Lbldepartment1.Text = "";

                   }
                   else
                   {
                       LabelOR.Visible = false;
                       Label2.Visible = false;
                       LAffiliation1.Visible = false;
                       Lblinstitute1.Visible = false;
                       Lblinstitute2.Visible = false;
                       Lbldepartment1.Visible = false;
                   }
               }
           }
      
       

      
       
        //if (a.SchoolName !=null && a.facultyName !=null)
        //{
        //    Lblinstitute.Visible = true;
        //    LAffiliation.Visible = true;
        //    Lbldepartment.Visible = true;
        //    LAffiliation.Text = a.SchoolName;
        //    Lblinstitute.Text = a.facultyName;
        //    Lbldepartment.Text = "Manipal Academy of Higher Education, Manipal";
        //}
        //else if (a.facultyName ==null && a.SchoolName !=null)
        //{
        //    Lblinstitute.Visible = true;
        //    LAffiliation.Visible = true;
        //    LAffiliation.Text = a.SchoolName;
        //    Lblinstitute.Text = "Manipal Academy of Higher Education, Manipal";
        //    Lbldepartment.Visible = false;
        //}
        //else if (a.facultyName != null && a.SchoolName == null)
        //{
        //    Lblinstitute.Visible = true;
        //    LAffiliation.Visible = true;
        //    LAffiliation.Text = a.facultyName;
        //    Lblinstitute.Text = "Manipal Academy of Higher Education, Manipal";
        //    Lbldepartment.Visible = false;
        //}
        //else 
        //{
        //    LAffiliation.Visible = true;
        //    LAffiliation.Text = "Manipal Academy of Higher Education, Manipal";
        //    Lbldepartment.Visible = false;
        //    Lblinstitute.Visible = false;
        //}

        if (inst == null)
        {
            panel1.Visible = false;
            LabelNote.Visible = true;
        }
        else
        {
            panel1.Visible = true;
            LabelNote.Visible = false;
        }

        if (txtEmpDetails.Text == "")
        {
            panel1.Visible = false;
            LabelNote.Visible = false;
            string CloseWindow1 = "alert('Please enter any ID')";
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow1, true);
            return;

        }
      
    }
    protected void RadBtnListEmpDetails_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RadBtnListEmpDetails.SelectedValue == "E")
        {
            LabelEmailID.Visible = false;
            txtEmpDetails.Text = "";
            panel1.Visible = false;
        }
        else
        {
            LabelEmailID.Visible = true;
            txtEmpDetails.Text = "";
            panel1.Visible = false;
        }
    }
    protected void RadioButtonList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RadioButtonList2.SelectedValue == "F")
        {
            panelSearchPub.Visible = true;
            panelStudentsearch.Visible = false;
            panel1.Visible = false;
            panelStddetails.Visible = false;
        }
        else
        {
            panelSearchPub.Visible = false;
            panel1.Visible = false;
            panelStddetails.Visible = false;
            panelStudentsearch.Visible = true;
        }
    }
    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RadioButtonList1.SelectedValue == "R")
        {
            //LabelEmailID.Visible = false;
            TextBox1.Text = "";
            panelStddetails.Visible = false;
        }
        else
        {
            //LabelEmailID.Visible = true;
            TextBox1.Text = "";
            panelStddetails.Visible = false;
        }
    }
    protected void Buttonseachstudent_Click(object sender, EventArgs e)
    {
        panel1.Visible = false;
        LabelNote.Visible = false;
        //panel1.Visible = true;
        Business obj = new Business();
        User a = new User();

        if (RadioButtonList1.SelectedValue == "R")
        {
            sempid = TextBox1.Text.Trim();
            sEmailID = "";


        }
        else if (RadioButtonList1.SelectedValue == "I")
        {
            sEmailID = TextBox1.Text.Trim();
            sempid = "";

        }

        a = obj.getStudentInstitutiondetails(sempid, sEmailID);

        string inst = a.InstituteId;
        string SmaheName = a.Mahedepartment;
        string dept = a.Department_Id;

        if (inst != null)
        {
            a = obj.getStudentAffiliationdetails(sempid, sEmailID);

            LabelSname.Text = a.Name;
            if (a.emailId != null && a.emailId !="")
            {
                Label9.Visible = true;
                LabelSEmailID.Visible = true;
                LabelSEmailID.Text = a.emailId;
            }
            else
            {
                Label9.Visible = false;
                LabelSEmailID.Visible = false;
            }
            LabelSinst.Visible = true;
            Label1Saffiliation.Visible = true;
            Label1Saffiliation.Text = SmaheName;
            LabelSinst.Text = "Manipal Academy of Higher Education, Manipal";
            LabelsClass.Visible = false;
        }
        else
        {
            panelStddetails.Visible = true;
            Label7.Visible = false;
        }
       
       
        //if (CenterCode == 0)
        //{
        //    LabelOR.Visible = false;
        //    Label2.Visible = false;
        //    LAffiliation1.Visible = false;
        //    Lblinstitute1.Visible = false;
        //    Lblinstitute2.Visible = false;
        //    Lbldepartment1.Visible = false;
        //}
        //else
        //{
        //    a = obj.getAuthorCenterAffiliationdetails(CenterCode);
        //    LabelOR.Visible = true;
        //    Label2.Visible = true;
        //    LAffiliation1.Visible = true;
        //    Lblinstitute1.Visible = false;
        //    Lblinstitute2.Visible = true;
        //    Lbldepartment1.Visible = true;
        //    LAffiliation1.Text = a.Centername;
        //    Lblinstitute2.Text = a.SchoolName1;
        //    Lbldepartment1.Text = "Manipal Academy of Higher Education, Manipal";
        //}





        //if (a.SchoolName !=null && a.facultyName !=null)
        //{
        //    Lblinstitute.Visible = true;
        //    LAffiliation.Visible = true;
        //    Lbldepartment.Visible = true;
        //    LAffiliation.Text = a.SchoolName;
        //    Lblinstitute.Text = a.facultyName;
        //    Lbldepartment.Text = "Manipal Academy of Higher Education, Manipal";
        //}
        //else if (a.facultyName ==null && a.SchoolName !=null)
        //{
        //    Lblinstitute.Visible = true;
        //    LAffiliation.Visible = true;
        //    LAffiliation.Text = a.SchoolName;
        //    Lblinstitute.Text = "Manipal Academy of Higher Education, Manipal";
        //    Lbldepartment.Visible = false;
        //}
        //else if (a.facultyName != null && a.SchoolName == null)
        //{
        //    Lblinstitute.Visible = true;
        //    LAffiliation.Visible = true;
        //    LAffiliation.Text = a.facultyName;
        //    Lblinstitute.Text = "Manipal Academy of Higher Education, Manipal";
        //    Lbldepartment.Visible = false;
        //}
        //else 
        //{
        //    LAffiliation.Visible = true;
        //    LAffiliation.Text = "Manipal Academy of Higher Education, Manipal";
        //    Lbldepartment.Visible = false;
        //    Lblinstitute.Visible = false;
        //}

        if (inst == null)
        {
            panelStddetails.Visible = false;
            Label7.Visible = true;
        }
        else
        {
            panelStddetails.Visible = true;
            Label7.Visible = false;
        }

        if (TextBox1.Text == "")
        {
            panelStddetails.Visible = false;
            Label7.Visible = false;
            string CloseWindow1 = "alert('Please enter any ID')";
            ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "CloseWindow", CloseWindow1, true);
            return;

        }
    }
}