<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPageHead.master" AutoEventWireup="true" CodeFile="Loginstudent.aspx.cs" Inherits="Loginstudent" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderhead" Runat="Server">
      <br />
    <div class="alignright" style="width: 1094px;margin-left:900px"> <asp:HyperLink ID="HyperLink2" runat="server"  
            NavigateUrl="~/Login.aspx"><asp:Label ID="Label3" runat="server"   ForeColor="Black" Text="Back"></asp:Label></asp:HyperLink></div>
    <br />
    <div style="margin-left:500px" >
        <h3 style="color: #000000; font-size: 22px;" ><b>Student Login </b></h3>
    </div>
    <br />
<center><b><asp:Label ID="lblmsg" runat="server" ></asp:Label></b>
      
<div class="clients-block">

 

    <div>
    <form id="frm" runat="server">
    
     <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" DisplayMode="BulletList" 
     HeaderText="Validation issues" ShowSummary="False" ValidationGroup="Validation"/>
     
           <%-- <div style="margin-left:80px; position:absolute;">
                         <asp:linkbutton id="lnkUsefulLink" runat="server" text="Useful Links" style="color: navy;font-weight: bold;font-size:17px" /><br /><br />

                </div>
         <div style="margin-left:80px; position:absolute;">
                         <asp:linkbutton id="lnkStudentapplication" runat="server" text="Student Seed Money Application" style="color: navy;font-weight: bold;font-size:17px"   /><br /><br />

                </div>--%>
     <%--   <div style="margin-left:-30px; position:absolute; margin-left:80px">--%>
           <%--   <div style="margin-left:80px; position:absolute;">
         <asp:linkbutton id="LinkButtonDownloadPdf" runat="server" text="A Glimpse of the DST Schemes" style="color: navy;font-weight: bold;" onclick="LinkButtonDownloadPdf_Click" /><br /><br />
       <asp:linkbutton id="LinkBtnGrantCalls" runat="server" text="Grant Calls" style="color: navy;font-weight: bold; margin-left:-122px" onclientclick="window.open('http://muportal/GrantsDetails.aspx');" /><br /><br />
          <asp:linkbutton id="lnkFAIR" runat="server" text="FAIR" style="color: navy;font-weight: bold; margin-left:-164px" onclientclick="window.open('http://muportal/doc.aspx?doc=cms/docs/circular/Registrar/Staff/Faculty%20Award%20Incentive%20for%20Research%20(FAIR)%20Publication%20Policy%202016.12.31.pdf');" /><br /><br />
          <asp:linkbutton id="lnkPRIASE" runat="server" text="PRAISE" style="color: navy;font-weight: bold; margin-left:-145px" onclientclick="window.open('http://muportal/doc.aspx?doc=cms/docs/circular/Registrar/Students/Policy%20on%20Publication%20and%20Research%20Award%20Incentive%20for%20Students%20to%20Excel%20(PRAISE)%202016.12.31.pdf');" /><br /><br />
          <asp:linkbutton id="lnkPubUserMamnual" runat="server" text="Publication User Manual" style="color: navy;font-weight: bold; margin-left:-43px" onclientclick="window.open('http://muportal/doc.aspx?doc=cms/docs/circular/Registrar/Students/Policy%20on%20Publication%20and%20Research%20Award%20Incentive%20for%20Students%20to%20Excel%20(PRAISE)%202016.12.31.pdf');" /><br /><br />
          <asp:linkbutton id="lnkProjectUserManual" runat="server" text="Project User Manual" style="color: navy;font-weight: bold; margin-left:-70px" onclientclick="window.open('http://muportal/doc.aspx?doc=cms/docs/circular/Registrar/Students/Policy%20on%20Publication%20and%20Research%20Award%20Incentive%20for%20Students%20to%20Excel%20(PRAISE)%202016.12.31.pdf');" /><br /><br />

        </div>--%>
         
        
        
     <asp:Panel ID="LoginPanel" runat="server" GroupingText="" style="background-color:#E0EBAD" Width="1400px" Height="200px">  
  
         
<asp:Panel ID="panel" runat="server" GroupingText="" Style="font-weight:bold;margin-left:-200px; border-style:inset;background-color:#0b532d"  Width="500px" Height="200px" > 
            <table style="width: 100%" >
   
  <tr>
   <td>
   
       
   </td>
   </tr>
   <tr>
   <td>
   
   </td>
   </tr>
     <tr>
   <td>
   
   </td>
   </tr>
   <tr>
   <td>
   
   </td>
   </tr>
     <tr>
   <td>
   
   </td>
   </tr>
   <tr>
   <td>
   
   </td>
   </tr>
     <tr>
   <td>
   
   </td>
   </tr>
   <tr>
   <td>
   
   </td>
   </tr>
   <tr >

    <td align="center"> 
       <asp:Label ID="Label" runat="server" Text="Roll Number" ForeColor="White" ></asp:Label>
    
     

      
    </td>
    

       <td align="center">
        <asp:TextBox ID="TextBoxURollNO" runat="server" Width="300px" Height="30px" Style="border-style:ridge"></asp:TextBox> 
      
               <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please enter UserId"
         ValidationGroup="Validation" ControlToValidate="TextBoxURollNO" Display="None"></asp:RequiredFieldValidator>
        </td>
        <td>
        <%--  <asp:Label ID="Label2" runat="server" Text="@manipal.edu" ForeColor="White"></asp:Label>--%>
        </td>
   </tr>
   <tr>
   <td>
   
   </td>
   </tr>
   <tr>
   <td>
   
   </td>
   </tr>
     <tr>
   <td>
   
   </td>
   </tr>
   <tr>
   <td>
   
   </td>
   </tr>
     <tr>
   <td>
   
   </td>
   </tr>
   <tr>
   <td>
   
   </td>
   </tr>
     
   <tr>
   <td>
   
   </td>
   </tr>
        <tr>
        <td align="center">
          <asp:Label ID="Label1" runat="server" Text="Password (DOB)" ForeColor="White" ></asp:Label>
          
        </td>
       
        <td align="center">
          <asp:TextBox ID="TextBoxPassword" runat="server" TextMode="Password" Height="30px" Width="300px" Style="border-style:ridge"></asp:TextBox>
      <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please enter Password"
         ValidationGroup="Validation" ControlToValidate="TextBoxPassword" Display="None"></asp:RequiredFieldValidator>
        </td>
        <td>
             
          <asp:Label ID="Label2" runat="server" Text="ddmmyyyy" ForeColor="White"></asp:Label>
        
        </td>
      </tr>
     
    <tr>
   <td>
   
   </td>
   </tr>



   
        <tr>
        <td colspan="3" align="center">
        <asp:ImageButton ID="BtnLoginstudent" runat="server"  ImageUrl="~/Images/images (2).jpg" Width="70px" Height="30px" ValidationGroup="Validation" CausesValidation="true" OnClick="BtnLoginstudent_Click" /><br /><br />
        <asp:Label ID="LblError" runat="server" ForeColor="Red"></asp:Label>
        <asp:Label ID="LblError1" runat="server" ForeColor="Red"></asp:Label>
        </td>
      </tr>
         
    <tr>
   <td>
   
   </td>
   </tr>
      
    <tr>
   <td>
   
   </td>
   </tr>
      <tr>
      <td >
      <asp:Label ID="lblunauthoaccess" runat="server" Text="Unathorized Access !!! Login again.." Font-Bold="True" Font-Size="Large" ForeColor="Red"></asp:Label>
      </td>
      </tr>
      



            </table>
</asp:Panel>
</asp:Panel>
            </form>
        </div>
        
    </div>
  

</center>       
</asp:Content>

