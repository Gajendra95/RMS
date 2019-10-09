<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPageRMS.master" AutoEventWireup="true" CodeFile="PublicationReportRollNowise.aspx.cs" Inherits="PublicationEntry_Reports_PublicationReportRollNowise" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  
<center>
<b>Publish Details-RollNumberwise</b>
</center>

 <table>
 <tr>
 <td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td>
 <td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td>
 <td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td>
 <td>
 <asp:Button id="Buttonback" runat="server" Text="Back" OnClick="backClick" />
 </td>
 </tr>
 </table>
 <br />

<center>
<table border="1">
<tr>
<td style="height: 56px; font-weight:normal" >
<asp:Label ID="LabelStudent" runat="server" Text="RollNumber" ForeColor="Black"   ></asp:Label>
<asp:TextBox ID="TextBoxRollNo" runat="server" ></asp:TextBox>  
</td><td style="height: 56px; font-weight:normal" >
<asp:Label ID="Label2" runat="server" Text="Publication Type" ForeColor="Black"  ></asp:Label>
<asp:DropDownList ID="DropDownListTypeEntry" runat="server" Style="border-style:inset none none inset;" DataSourceID="SqlDataSourceEntryType" DataTextField="EntryName" DataValueField="TypeEntryId" AppendDataBoundItems="true">
<asp:ListItem Value="ALL">ALL</asp:ListItem>
</asp:DropDownList>  
<asp:SqlDataSource ID="SqlDataSourceEntryType" runat="server" ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" 
                   SelectCommand="select TypeEntryId,EntryName from PublicationTypeEntry_M" >
</asp:SqlDataSource>
</td>
 <%--<td style="height: 56px; font-weight:normal" >
 <asp:Label ID="Label1" runat="server" Text="Status" ForeColor="Black"  ></asp:Label>
 <asp:DropDownList ID="ddlStatus" runat="server" Style="border-style:inset none none inset;" AppendDataBoundItems="true" DataSourceID="SqlDataSource1" 
     DataTextField="StatusName" DataValueField="StatusId">
     <asp:ListItem Value="ALL" Selected="True">ALL</asp:ListItem>
 </asp:DropDownList>  
     <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
         ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" 
         SelectCommand="SELECT * FROM [Status_Publication_M] where StatusId='APP' or  StatusId='NEW' or  StatusId='SUB'"></asp:SqlDataSource>
 </td>--%>
<td><asp:Button id="view" runat="server" Text="view" OnClick="viewclick"  BorderColor="Black" Width="90px" Height="40px"/></td>
</tr>
</table>
<br />
</center>
<br />

<center>
<asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release">
</asp:ScriptManager>
&nbsp;
<br />
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Height="562px" Width="865px">
    </rsweb:ReportViewer>
    </center>



</asp:Content>

