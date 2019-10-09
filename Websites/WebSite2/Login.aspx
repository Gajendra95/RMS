 <%@ Page Language="C#" MasterPageFile="~/MasterPage/MasterPageHead.master"  AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderhead" Runat="Server">
<br />



<br />
<center><b><asp:Label ID="lblmsg" runat="server" ></asp:Label></b>
      
<div class="clients-block">
    <div>
    <form id="frm" runat="server">
    
     <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" DisplayMode="BulletList" 
     HeaderText="Validation issues" ShowSummary="False" ValidationGroup="Validation"/>
   
            <div style="margin-left:80px; position:absolute;">
                         <asp:linkbutton id="lnkUsefulLink" runat="server" text="Useful Links" style="color: navy;font-weight: bold;font-size:17px" OnClick="lnkUsefulLink_Click"  /><br /><br />

                </div>
       
         <div style="margin-left:900px; position:absolute;">
                         <asp:linkbutton id="lnkStudentapplication" runat="server" text="Student Seed Money Application" style="color: navy;font-weight: bold;font-size:17px" OnClick="lnkStudentapplication_Click"   /><br /><br />

               </div>
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
       <asp:Label ID="Label" runat="server" Text="User Name" ForeColor="White" ></asp:Label>
    
     

      
    </td>
    

       <td align="center">
        <asp:TextBox ID="TextBoxUid" runat="server" Width="300px" Height="30px" Style="border-style:ridge"></asp:TextBox> 
      
               <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please enter UserId"
         ValidationGroup="Validation" ControlToValidate="TextBoxUid" Display="None"></asp:RequiredFieldValidator>
        </td>
        <td>
          <asp:Label ID="Label2" runat="server" Text="@manipal.edu" ForeColor="White"></asp:Label>
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
          <asp:Label ID="Label1" runat="server" Text="Password" ForeColor="White" ></asp:Label>
          
        </td>
       
        <td align="center">
          <asp:TextBox ID="TextBoxPassword" runat="server" TextMode="Password" Height="30px" Width="300px" Style="border-style:ridge"></asp:TextBox>
      <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please enter Password"
         ValidationGroup="Validation" ControlToValidate="TextBoxPassword" Display="None"></asp:RequiredFieldValidator>
        </td>
        <td></td>
      </tr>
     
    <tr>
   <td>
   
   </td>
   </tr>



   
        <tr>
        <td colspan="3" align="center">
        <asp:ImageButton ID="Button1" runat="server"  ImageUrl="~/Images/images (2).jpg" Width="70px" Height="30px" onclick="btnLogin_Click" ValidationGroup="Validation" CausesValidation="true" /><br /><br />
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