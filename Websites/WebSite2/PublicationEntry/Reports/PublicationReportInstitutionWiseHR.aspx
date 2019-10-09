<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPageRMS.master" AutoEventWireup="true" CodeFile="PublicationReportInstitutionWiseHR.aspx.cs" Inherits="PublicationEntry_Reports_PublicationReportInstitutionWiseHR" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">    
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" DisplayMode="BulletList" 
     HeaderText="Validation issues" ShowSummary="False" ValidationGroup="validation" />
<center>
<b>Institution Wise-Incentive Points</b>
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
                 Style="border-style:inset none none inset;" DataSourceID="SqlDataSourceInst" DataTextField="Institute_Name" DataValueField="Institute_Id" AppendDataBoundItems="true" Enabled="false">
<asp:ListItem Value="ALL">ALL</asp:ListItem>
</asp:DropDownList>  
<asp:SqlDataSource ID="SqlDataSourceInst" runat="server" ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" 
                   SelectCommand="">
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
 <%--<td> 
<asp:Label ID="Label3" runat="server" Text="Inclusive of Student " ForeColor="Black"  ></asp:Label>

<asp:DropDownList ID="ddlStudentFlag" runat="server" Style="border-style:inset none none inset;"  AppendDataBoundItems="true">
<asp:ListItem Value="Y">Yes </asp:ListItem>
<asp:ListItem Value="N">No</asp:ListItem>
</asp:DropDownList>  </td>--%>
<td rowspan="2">
<asp:Button id="view" runat="server" Text="view" OnClick="viewclick"  BorderColor="Black" Width="90px" Height="40px"  CausesValidation="true" ValidationGroup="validation" /></td>
</tr>

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

