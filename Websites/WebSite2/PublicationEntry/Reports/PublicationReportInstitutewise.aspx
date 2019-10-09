<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPageRMS.master" AutoEventWireup="true" CodeFile="PublicationReportInstitutewise.aspx.cs" Inherits="PublicationReportInstitutewise" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" DisplayMode="BulletList" 
     HeaderText="Validation issues" ShowSummary="False" ValidationGroup="validation" />
<center>
<b>Publish Details-Institutewise</b>
</center>

<table>
<tr>
 <td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td>
 <td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td>
 <td>&nbsp;</td><td>&nbsp;</td>
<td><asp:Button id="Buttonback" runat="server" Text="Back" OnClick="backClick" CausesValidation="false" /></td>
</tr>
</table>
<br />

<center>
<table border="1">
<tr>
<td style="height: 56px; font-weight:normal" >
<asp:Label ID="LabelMonthJA" runat="server" Text="From Month" ForeColor="Black"   ></asp:Label>
<asp:DropDownList ID="DropDownListMonthJA" runat="server" Style="border-style:inset none none inset;"
                  DataSourceID="SqlDataSourceFromMonth" DataTextField="MonthName" DataValueField="MonthValue"  >
</asp:DropDownList>  
<asp:SqlDataSource ID="SqlDataSourceFromMonth" runat="server" ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" 
                   SelectCommand="select MonthValue, MonthName from Publication_MonthM ">
</asp:SqlDataSource> 
</td>
<td style="height: 56px; font-weight:normal" >
<asp:Label ID="LabelToPubmonth" runat="server" Text="To Month" ForeColor="Black"   ></asp:Label>
<asp:DropDownList ID="DropDownListTopubMonth" runat="server" Style="border-style:inset none none inset;"
                  DataSourceID="SqlDataSourceToMonth" DataTextField="MonthName" DataValueField="MonthValue"  >
</asp:DropDownList>  
<asp:SqlDataSource ID="SqlDataSourceToMonth" runat="server" ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" 
                   SelectCommand="select MonthValue, MonthName from Publication_MonthM ">
</asp:SqlDataSource> 
</td>
<td style="height: 56px; font-weight:normal"  >
<asp:Label ID="Label1" runat="server" Text="Institution" ForeColor="Black"  ></asp:Label>
<asp:DropDownList ID="DropDownListInst" runat="server" Style="border-style:inset none none inset;" DataSourceID="SqlDataSourceInst" DataTextField="Institute_Name" DataValueField="Institute_Id" AppendDataBoundItems="true">
<asp:ListItem Value="ALL">ALL</asp:ListItem>
</asp:DropDownList>  
 <asp:SqlDataSource ID="SqlDataSourceInst" runat="server" ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" 
                    SelectCommand="select Institute_Id,Institute_Name from Institute_M ">
</asp:SqlDataSource>
</td>
<td align="center"  rowspan="2">
<asp:Button id="view" runat="server" Text="view" OnClick="viewclick"  BorderColor="Black" Width="90px" Height="40px" CausesValidation="true" ValidationGroup="validation" /></td>
</tr>
<tr>
<td style="height: 56px; font-weight:normal" >
<asp:Label ID="LabelYearJA" runat="server" Text="From Year" ForeColor="Black"  ></asp:Label>
<asp:TextBox ID="TextBoxYearJA" runat="server"  Width="80px"  Style="border-style:inset none none inset;">
</asp:TextBox>  
<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TextBoxYearJA" Display="None" 
                            ErrorMessage="Please Enter the From Year" 
                             ValidationGroup="validation"></asp:RequiredFieldValidator>
<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" Display="None"  ControlToValidate="TextBoxYearJA" ValidationGroup="validation"
                                ErrorMessage="Entered From Year should be in number" SetFocusOnError="true"   ValidationExpression="^[0-9]{4}$">
</asp:RegularExpressionValidator>
</td>
<td style="height: 56px; font-weight:normal" >
<asp:Label ID="LabelToPubMyear" runat="server" Text="To Year" ForeColor="Black"  ></asp:Label>
<asp:TextBox ID="TextBoxTopubYear" runat="server"  Width="80px"  Style="border-style:inset none none inset;">
</asp:TextBox>  
<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"   ControlToValidate="TextBoxTopubYear" Display="None" 
                             ErrorMessage="Please Enter the To year"   ValidationGroup="validation"></asp:RequiredFieldValidator>
<asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" Display="None"  ControlToValidate="TextBoxTopubYear" ValidationGroup="validation"
                                ErrorMessage="Entered To Year should be in number" SetFocusOnError="true"  
                               ValidationExpression="^[0-9]{4}$">
</asp:RegularExpressionValidator>
</td>
 <td style="height: 56px; font-weight:normal" >
 <asp:Label ID="Label2" runat="server" Text="Publication Type" ForeColor="Black"  ></asp:Label>
 <asp:DropDownList ID="DropDownListTypeEntry" runat="server" 
     Style="border-style:inset none none inset;" DataSourceID="SqlDataSourceEntryType" DataTextField="EntryName" DataValueField="TypeEntryId" AppendDataBoundItems="true">
<asp:ListItem Value="ALL">ALL</asp:ListItem>
</asp:DropDownList>  
<asp:SqlDataSource ID="SqlDataSourceEntryType" runat="server" ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" 
                   SelectCommand="select TypeEntryId,EntryName from PublicationTypeEntry_M" >
</asp:SqlDataSource>
</td>
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

