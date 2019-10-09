<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPageRMS.master" AutoEventWireup="true" CodeFile="PublicationReportContinentwise.aspx.cs" Inherits="PublicationReportContinentlwise" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" DisplayMode="BulletList" 
     HeaderText="Validation issues" ShowSummary="False" ValidationGroup="validation" />
<center>
<b>Publish Details-Continentwise</b>
</center>

<table>
<tr>
<td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td>
<td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td>
<td style="width: 4px">&nbsp;</td>
<td style="width: 4px">&nbsp;</td>
<td><asp:Button id="Buttonback" runat="server" Text="Back" OnClick="backClick" CausesValidation="false"/></td>
</tr>
</table>
<br />

<center>
<table border="1">
<tr>
<td style="height: 56px; font-weight:normal" >
<asp:Label ID="LabelMonthJA" runat="server" Text="From Month" ForeColor="Black"   ></asp:Label>
<asp:DropDownList ID="DropDownListMonthJA" runat="server" Style="border-style:inset none none inset;"  >
            <asp:ListItem Value="1">Jan</asp:ListItem>
            <asp:ListItem Value="2">Feb</asp:ListItem>
            <asp:ListItem Value="3">Mar</asp:ListItem>
            <asp:ListItem Value="4">Apr</asp:ListItem>
            <asp:ListItem Value="5">May</asp:ListItem>
            <asp:ListItem Value="6">Jun</asp:ListItem>         
            <asp:ListItem Value="7">Jul</asp:ListItem>
            <asp:ListItem Value="8">Aug</asp:ListItem>
            <asp:ListItem Value="9">Sep</asp:ListItem>
            <asp:ListItem Value="10">Oct</asp:ListItem>
            <asp:ListItem Value="11">Nov</asp:ListItem>
            <asp:ListItem Value="12">Dec</asp:ListItem>
</asp:DropDownList>  
</td>
 <td style="height: 56px; font-weight:normal" >
 <asp:Label ID="LabelToPubmonth" runat="server" Text="To Month" ForeColor="Black"   ></asp:Label>
 <asp:DropDownList ID="DropDownListTopubMonth" runat="server" Style="border-style:inset none none inset;" >
            <asp:ListItem Value="1">Jan</asp:ListItem>
            <asp:ListItem Value="2">Feb</asp:ListItem>
            <asp:ListItem Value="3">Mar</asp:ListItem>
            <asp:ListItem Value="4">Apr</asp:ListItem>
            <asp:ListItem Value="5">May</asp:ListItem>
            <asp:ListItem Value="6">Jun</asp:ListItem>         
            <asp:ListItem Value="7">Jul</asp:ListItem>
            <asp:ListItem Value="8">Aug</asp:ListItem>
            <asp:ListItem Value="9">Sep</asp:ListItem>
            <asp:ListItem Value="10">Oct</asp:ListItem>
            <asp:ListItem Value="11">Nov</asp:ListItem>
            <asp:ListItem Value="12">Dec</asp:ListItem>
</asp:DropDownList>  
</td>
<td style="height: 56px; font-weight:normal" >
<asp:Label ID="Label2" runat="server" Text="Continent" ForeColor="Black"  ></asp:Label>
<asp:DropDownList ID="DropDownListContinent" runat="server" DataSourceID="SqlDataSourceContient" DataTextField="ContinentName" DataValueField="ContinentId"  AppendDataBoundItems="true"
                  Style="border-style:inset none none inset;">
<asp:ListItem Value="ALL">ALL</asp:ListItem>
</asp:DropDownList>  
 <asp:SqlDataSource ID="SqlDataSourceContient" runat="server" ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" 
                    SelectCommand="select ContinentId,ContinentName from Continent_M">
                   </asp:SqlDataSource> 
</td>
<td rowspan="2" align="center" >
<asp:Button id="view" runat="server" Text="view" OnClick="viewclick"  BorderColor="Black" Width="90px" Height="40px" CausesValidation="true" ValidationGroup="validation" /></td>   
</tr>
<tr>
<td style="height: 56px; font-weight:normal" >
<asp:Label ID="LabelYearJA" runat="server" Text="From Year" ForeColor="Black"  ></asp:Label>
<asp:TextBox ID="TextBoxYearJA" runat="server" Width="80px"  Style="border-style:inset none none inset;" >
</asp:TextBox>  
<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TextBoxYearJA" Display="None" 
                            ErrorMessage="Please Enter the From Year"  ValidationGroup="validation"></asp:RequiredFieldValidator>
<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" Display="None"  ControlToValidate="TextBoxYearJA" ValidationGroup="validation"
                ErrorMessage="Entered From Year should be in number" SetFocusOnError="true"    ValidationExpression="^[0-9]{4}$">
</asp:RegularExpressionValidator>
</td>
<td style="height: 56px; font-weight:normal" >
<asp:Label ID="LabelToPubMyear" runat="server" Text="To Year" ForeColor="Black"  ></asp:Label>
<asp:TextBox ID="TextBoxTopubYear" runat="server"  Width="80px"  Style="border-style:inset none none inset;" >
</asp:TextBox>  
<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"   ControlToValidate="TextBoxTopubYear" Display="None" 
                             ErrorMessage="Please Enter the To year"   ValidationGroup="validation"></asp:RequiredFieldValidator>
<asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" Display="None"  ControlToValidate="TextBoxTopubYear" ValidationGroup="validation"
                ErrorMessage="Entered To Year should be in number" SetFocusOnError="true"  ValidationExpression="^[0-9]{4}$">
</asp:RegularExpressionValidator>
</td>
 <td style="height: 56px; font-weight:normal" >
 <asp:Label ID="Label1" runat="server" Text="Publication Type" ForeColor="Black"  ></asp:Label>
 <asp:DropDownList ID="DropDownListTypeEntry" runat="server" 
     Style="border-style:inset none none inset;" DataSourceID="SqlDataSourceEntryType" DataTextField="EntryName" DataValueField="TypeEntryId" AppendDataBoundItems="true">
 <asp:ListItem Value="ALL">ALL</asp:ListItem>
 </asp:DropDownList>  
 <asp:SqlDataSource ID="SqlDataSourceEntryType" runat="server" ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" 
                    SelectCommand="select TypeEntryId,EntryName from PublicationTypeEntry_M" >
 </asp:SqlDataSource>
 </td>
 </tr>
 <tr>
  <td style="height: 56px; font-weight:normal" >
 <asp:Label ID="Label3" runat="server" Text="Indexed" ForeColor="Black"  ></asp:Label>
 <asp:DropDownList ID="DropDownListIndexed" runat="server" Style="border-style:inset none none inset;">
 <asp:ListItem Value="ALL">ALL</asp:ListItem>
 <asp:ListItem Value="Y">YES</asp:ListItem>
 <asp:ListItem Value="N">NO</asp:ListItem>
 </asp:DropDownList>  
 </td>
<%-- <td style="height: 56px; font-weight:normal" >
 <asp:Label ID="Label4" runat="server" Text="Status" ForeColor="Black"  ></asp:Label>
 <asp:DropDownList ID="ddlStatus" runat="server" Style="border-style:inset none none inset;" AppendDataBoundItems="true" DataSourceID="SqlDataSource1" 
     DataTextField="StatusName" DataValueField="StatusId">
     <asp:ListItem Value="ALL" Selected="True">ALL</asp:ListItem>
 </asp:DropDownList>  
     <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
         ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" 
         SelectCommand="SELECT * FROM [Status_Publication_M] where StatusId='APP' or  StatusId='NEW' or  StatusId='SUB'"></asp:SqlDataSource>
 </td>--%>
 </tr>
 </table>
 <br />
</center>
<br />
<center>
<asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release">
</asp:ScriptManager>
<br />
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Height="562px" Width="865px">
    </rsweb:ReportViewer>
    </center>
</asp:Content>

