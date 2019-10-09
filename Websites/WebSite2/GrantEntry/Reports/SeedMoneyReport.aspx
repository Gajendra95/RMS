<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPageRMS.master" AutoEventWireup="true" CodeFile="SeedMoneyReport.aspx.cs" Inherits="GrantEntry_Reports_SeedMoneyReport" MaintainScrollPositionOnPostback="true" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<center>

<b>

</b></center>
<br />
<br />

    <table style="width: 100%">

    <tr>
          <td>
    <b> Seed Money Reports</b></td>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>

    <tr>
      <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>

 
    <tr>
        <td>
        <asp:LinkButton ID="LinkButtonFaculty" runat="server" onclick="LinkButtonFaculty_Click">Faculty Seed Money Details</asp:LinkButton>    


	</td>
         <td>      <%-- <asp:LinkButton ID="LinkButton1" runat="server" onclick="LinkButton1_Click">Institute wise</asp:LinkButton>   -
             </td>
        
        <td>
          <%--    <asp:LinkButton ID="LinkButton2" runat="server" onclick="LinkButton2_Click">Status wise</asp:LinkButton>   --%>  
		</td>
    </tr>
 
    

  
    <tr>
        <td>
    <asp:LinkButton ID="LinkButtonstudent" runat="server" onclick="LinkButtonstudent_Click">Student Seed Money Details</asp:LinkButton>    
</td>
        <td>
       <%--   <asp:LinkButton ID="LinkButton5" runat="server" onclick="LinkButton5_Click">Institute-Department wise</asp:LinkButton>    
--%></td>
        <td>
            &nbsp;</td>
    </tr>


   
    
      
   
 
</table>



</asp:Content>

