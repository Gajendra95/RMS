<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPageRMS.master" AutoEventWireup="true" CodeFile="~/PublicationEntry/Reports/PublicationReport.aspx.cs" Inherits="PublicationReport" MaintainScrollPositionOnPostback="true" %>
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
    <b>  Publication Reports</b></td>
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
       <asp:LinkButton ID="LinkButton6" runat="server" onclick="LinkButton6_Click1"> Institution Wise Incentive Points</asp:LinkButton>  </td>
  
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td>
        <asp:LinkButton ID="LinkButton3" runat="server" onclick="LinkButton3_Click">MU wise</asp:LinkButton>    


	</td>
         <td>      <%-- <asp:LinkButton ID="LinkButton1" runat="server" onclick="LinkButton1_Click">Institute wise</asp:LinkButton>   -
             </td>
        
        <td>
          <%--    <asp:LinkButton ID="LinkButton2" runat="server" onclick="LinkButton2_Click">Status wise</asp:LinkButton>   --%>  
		</td>
    </tr>
 
    

  
    <tr>
        <td>
    <asp:LinkButton ID="LinkButton4" runat="server" onclick="LinkButton4_Click">EmployeeId wise</asp:LinkButton>    
</td>
        <td>
       <%--   <asp:LinkButton ID="LinkButton5" runat="server" onclick="LinkButton5_Click">Institute-Department wise</asp:LinkButton>    
--%></td>
        <td>
            &nbsp;</td>
    </tr>
         <tr>
        <td>
    <asp:LinkButton ID="LinkButton14" runat="server" onclick="LinkButton14_Click">RollNumber wise</asp:LinkButton>    
</td>
        <td>
       <%--   <asp:LinkButton ID="LinkButton5" runat="server" onclick="LinkButton5_Click">Institute-Department wise</asp:LinkButton>    
--%></td>
        <td>
            &nbsp;</td>
    </tr>

    <tr>
        <td>
        <asp:LinkButton ID="LinkButton5" runat="server" onclick="LinkButton5_Click">Institute-Department wise</asp:LinkButton>  </td>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td>
       <asp:LinkButton ID="LinkButton8" runat="server" onclick="LinkButton6_Click">Journal wise</asp:LinkButton>  </td>
  
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td>
            <asp:LinkButton ID="LinkButton9" runat="server" onclick="LinkButton7_Click">Continent-wise</asp:LinkButton>  </td>
  
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
     <tr>
        <td>
       <asp:LinkButton ID="LinkButton11" runat="server" onclick="LinkButton11_Click">Faculty Authored - Journal wise</asp:LinkButton>  </td>
  
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
      <tr>
        <td>
       <asp:LinkButton ID="LinkButton10" runat="server" onclick="LinkButton10_Click">Student Authored - Journal wise</asp:LinkButton>  </td>
  
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
        <tr>
        <td>
       <asp:LinkButton ID="LinkButton1" runat="server" onclick="LinkButton1_Click1">Journal Publication - Inter Institution wise</asp:LinkButton>  </td>
  
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
        <tr>
        <td>
       <asp:LinkButton ID="LinkButton2" runat="server" onclick="LinkButton2_Click1">Deparment Wise - Incentive Point</asp:LinkButton>  </td>
  
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
        
        <tr>
        <td>
       <asp:LinkButton ID="LinkButton12" runat="server" onclick="LinkButton12_Click1">Institution Wise Incentive Point Details</asp:LinkButton>  </td>
  
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
            
        <tr>
        <td>
       <asp:LinkButton ID="LinkButton13" runat="server" onclick="LinkButton13_Click1"> Article Wise FAIR-PRAISE Points</asp:LinkButton>  </td>
  
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
        
         <%--<tr>
        <td>
       <asp:LinkButton ID="LinkButton2" runat="server" onclick="LinkButton2_Click1">Publication - Domain wise</asp:LinkButton>  </td>
  
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>--%>
    <tr>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
    </tr>
</table>
</asp:Content>

