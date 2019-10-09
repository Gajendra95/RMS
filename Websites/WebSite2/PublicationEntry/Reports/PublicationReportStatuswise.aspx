<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPageRMS.master" AutoEventWireup="true" CodeFile="PublicationReportStatuswise.aspx.cs" Inherits="PublicationReportInstitutewise" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

   <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" DisplayMode="BulletList" 
     HeaderText="Validation issues" ShowSummary="False" ValidationGroup="validation" />


 <table>
 <tr>
 <td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td>
 <td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td>
 <td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td>
 <td><asp:Button id="Buttonback" runat="server" Text="Back" OnClick="backClick" /></td>
 </tr>
</table>
<br />

<center>
<table border="1">
<tr>
 <td style="height: 56px; font-weight:normal" >
 <asp:Label ID="LabelFromDate" runat="server" Text="From Date" ForeColor="Black"   ></asp:Label>
 <asp:TextBox ID="TextBoxFromDate" runat="server" >
 </asp:TextBox> 
 <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="TextBoxFromDate" Format="dd/MM/yyyy" >
 </asp:CalendarExtender> 
 <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"  ControlToValidate="TextBoxFromDate" Display="None" 
                             ErrorMessage="Please enter From date" InitialValue=" "  ValidationGroup="validation"></asp:RequiredFieldValidator> 
 <asp:RegularExpressionValidator ID="RegularExpressionValidator15" runat="server" Display="None"  ControlToValidate="TextBoxFromDate" ValidationGroup="validation"
                ErrorMessage="From date in (dd/mm/yyyy) format" SetFocusOnError="true"  
                ValidationExpression="^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$">
 </asp:RegularExpressionValidator>
 </td>
 <td style="height: 56px; font-weight:normal" colspan="4">
 <asp:Label ID="LabelToDate" runat="server" Text="To Date" ForeColor="Black"  ></asp:Label>
 <asp:TextBox ID="TextBoxToDate" runat="server"  Style="border-style:inset none none inset;" ></asp:TextBox>  
 <asp:CalendarExtender ID="CalendarExtender2" runat="server"  TargetControlID="TextBoxToDate" Format="dd/MM/yyyy" >
 </asp:CalendarExtender> 
 <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"  ControlToValidate="TextBoxToDate" Display="None" 
                             ErrorMessage="Please Enter To Date" InitialValue=" "  ValidationGroup="validation"></asp:RequiredFieldValidator>
 <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" Display="None"  ControlToValidate="TextBoxToDate" ValidationGroup="validation"
                ErrorMessage="To date in (dd/mm/yyyy) format" SetFocusOnError="true"  
                ValidationExpression="^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$">
 </asp:RegularExpressionValidator>
 </td>
 <td style="height: 56px; font-weight:normal" colspan="4">
 <asp:Label ID="LabelStatus" runat="server" Text="Status" ForeColor="Black"  ></asp:Label>
 <asp:DropDownList ID="DropDownListStatus" runat="server" Style="border-style:inset none none inset;" 
                   DataSourceID="SqlDataSourceStatus" DataTextField="StatusName" DataValueField="StatusId">
 </asp:DropDownList>  
 <asp:SqlDataSource ID="SqlDataSourceStatus" runat="server" ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" 
                    SelectCommand="select StatusId,StatusName from Status_Publication_M ">
 </asp:SqlDataSource>
 </td>
 <td>
 <asp:Button id="view" runat="server" Text="view" OnClick="viewclick"  CausesValidation="true" ValidationGroup="validation"  BorderColor="Black" Width="90px" Height="40px"/></td>
 </tr>
 </table>
 <br />
 </center>
 <br />
 <center>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
&nbsp;

<br />
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Height="562px" Width="865px">
    </rsweb:ReportViewer>
    </center>
</asp:Content>

