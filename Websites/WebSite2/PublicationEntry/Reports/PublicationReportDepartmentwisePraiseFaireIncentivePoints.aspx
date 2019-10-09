<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPageRMS.master" AutoEventWireup="true" CodeFile="PublicationReportDepartmentwisePraiseFaireIncentivePoints.aspx.cs" Inherits="PublicationEntry_Reports_PublicationReportDepartmentwisePraiseFaireIncentivePoints" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" DisplayMode="BulletList" 
     HeaderText="Validation issues" ShowSummary="False" ValidationGroup="validation" />
<center>
<b> Institution Wise Incentive Point Details</b>
</center>

  <table>
  <tr>
  <td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td>
  <td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td>
  <td><asp:Button id="Buttonback" runat="server" Text="Back" OnClick="backClick" /></td>
  </tr>
  </table>
  <br />

<center>
<table border="1">
<tr>
<td style="height: 56px; font-weight:normal"  >
<asp:Label ID="Label1" runat="server" Text="Institution" ForeColor="Black"  ></asp:Label>
<asp:DropDownList ID="DropDownListInst" runat="server" OnSelectedIndexChanged="DropDownListInstOnSelectedIndexChanged" AutoPostBack="true"
                 Style="border-style:inset none none inset;" DataSourceID="SqlDataSourceInst" DataTextField="Institute_Name" DataValueField="Institute_Id" AppendDataBoundItems="true">
<asp:ListItem Value="ALL">ALL</asp:ListItem>
</asp:DropDownList>  
<asp:SqlDataSource ID="SqlDataSourceInst" runat="server" ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" 
                   SelectCommand="select Institute_Id,Institute_Name from Institute_M ">
</asp:SqlDataSource>
<%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" InitialValue="S" ControlToValidate="DropDownListInst" Display="None" 
                            ErrorMessage="Please Select the Institute" ValidationGroup="validation"></asp:RequiredFieldValidator>--%>
</td>
<td style="height: 56px; font-weight:normal" >
<asp:Label ID="LabelDept" runat="server" Text="Dept" ForeColor="Black"   ></asp:Label>
<asp:DropDownList ID="DropDownListDept" runat="server"  Style="border-style:inset none none inset;" DataSourceID="SqlDataSourceDept" DataTextField="DeptName" DataValueField="DeptId" AppendDataBoundItems="true">
<asp:ListItem Value="ALL">ALL</asp:ListItem>
</asp:DropDownList>  
<asp:SqlDataSource ID="SqlDataSourceDept" runat="server" ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" SelectCommand="">
</asp:SqlDataSource>  
</td>
 <td> 
<asp:Label ID="Label3" runat="server" Text="Member Type " ForeColor="Black"  ></asp:Label>

<asp:DropDownList ID="ddlMembertype" runat="server" Style="border-style:inset none none inset;"  AppendDataBoundItems="true">
<asp:ListItem Value="A">ALL </asp:ListItem>
<asp:ListItem Value="F">Faculty</asp:ListItem>
    <asp:ListItem Value="S">Student</asp:ListItem>
</asp:DropDownList>  </td>
<td rowspan="2">
<asp:Button id="view" runat="server" Text="view" OnClick="viewclick"  BorderColor="Black" Width="90px" Height="40px"  CausesValidation="true" ValidationGroup="validation" /></td>
</tr>
<%--<tr>
<td style="height: 56px; font-weight:normal" >
<asp:Label ID="LabelFromDate" runat="server" Text="From Date" ForeColor="Black"   ></asp:Label>
<asp:TextBox ID="TextBoxFromDate" runat="server" >
</asp:TextBox> 
<asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="TextBoxFromDate" Format="dd/MM/yyyy" >
</asp:CalendarExtender> 
<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"  ControlToValidate="TextBoxFromDate" Display="None" 
                            ErrorMessage="Please enter From date" InitialValue=" "  ValidationGroup="validation"></asp:RequiredFieldValidator> 
<asp:RegularExpressionValidator ID="RegularExpressionValidator15" runat="server" Display="None"  ControlToValidate="TextBoxFromDate" ValidationGroup="validation"
                ErrorMessage="From date in (dd/mm/yyyy) format" SetFocusOnError="true"  
                ValidationExpression="^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$">
</asp:RegularExpressionValidator>
</td>
<td style="height: 56px; font-weight:normal">
<asp:Label ID="LabelToDate" runat="server" Text="To Date" ForeColor="Black"  ></asp:Label>
<asp:TextBox ID="TextBoxToDate" runat="server"  Style="border-style:inset none none inset;" >
</asp:TextBox>  
<asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="TextBoxToDate" Format="dd/MM/yyyy" >
</asp:CalendarExtender> 
<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"   ControlToValidate="TextBoxToDate" Display="None" 
                            ErrorMessage="Please Enter To Date" InitialValue=" "  ValidationGroup="validation"></asp:RequiredFieldValidator>
<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" Display="None"  ControlToValidate="TextBoxToDate" ValidationGroup="validation"
                ErrorMessage="To date in (dd/mm/yyyy) format" SetFocusOnError="true"  
                ValidationExpression="^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$">
</asp:RegularExpressionValidator>
</td>
 <%--<td style="height: 56px; font-weight:normal" >
 <asp:Label ID="Label3" runat="server" Text="Status" ForeColor="Black"  ></asp:Label>
 <asp:DropDownList ID="ddlStatus" runat="server" Style="border-style:inset none none inset;" AppendDataBoundItems="true" DataSourceID="SqlDataSource1" 
     DataTextField="StatusName" DataValueField="StatusId">
     <asp:ListItem Value="ALL" Selected="True">ALL</asp:ListItem>
 </asp:DropDownList>  
     <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
         ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" 
         SelectCommand="SELECT * FROM [Status_Publication_M] where StatusId='APP' or  StatusId='NEW' or  StatusId='SUB'"></asp:SqlDataSource>
 </td>--%>
  
<%--</tr>--%>
</table>
<br />
</center>
<br />

<center>
<asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release">
</asp:ScriptManager>
    <center><b><asp:Label ID="lblnote" Text="Note : * Indicates Student" Visible="false" Font-Size="20px" runat="server"></asp:Label></b></center>
&nbsp;
<br />
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Height="562px" Width="1000px">
    </rsweb:ReportViewer>
    </center>






</asp:Content>

