<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPageRMS.master" AutoEventWireup="true" CodeFile="GrantReportInstitutDeptWise.aspx.cs" Inherits="GrantEntry_Reports_GrantReportInstitutDeptWise" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" DisplayMode="BulletList"
        HeaderText="Validation issues" ShowSummary="False" ValidationGroup="validation" />
    <center>
        <b><asp:Label runat="server" ID="lblTitle" Text="Project Details-Institute Department Wise"></asp:Label></b>
        <br />
    </center>

    <table>
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>
                <asp:Button ID="Buttonback" runat="server" Text="Back" OnClick="backClick" CausesValidation="false" /></td>
        </tr>
    </table>
    <br />

    <center>
        <table border="1">
            <tr>
                <td style="height: 56px; font-weight: normal">
                    <asp:Label ID="Label3" runat="server" Text="Institution" ForeColor="Black"></asp:Label>
                    <asp:DropDownList ID="DropDownListInst" runat="server" OnSelectedIndexChanged="DropDownListInstOnSelectedIndexChanged" AutoPostBack="true"
                        Style="border-style: inset none none inset;" DataSourceID="SqlDataSourceInst" DataTextField="Institute_Name" DataValueField="Institute_Id" AppendDataBoundItems="true">
                        <asp:ListItem Value="S">--Select--</asp:ListItem>
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="SqlDataSourceInst" runat="server" ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>"
                        SelectCommand="select Institute_Id,Institute_Name from Institute_M "></asp:SqlDataSource>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" InitialValue="S" ControlToValidate="DropDownListInst" Display="None"
                        ErrorMessage="Please Select the Institute" ValidationGroup="validation"></asp:RequiredFieldValidator>
                </td>
                <td style="height: 56px; font-weight: normal">
                    <asp:Label ID="LabelDept" runat="server" Text="Dept" ForeColor="Black"></asp:Label>
                    <asp:DropDownList ID="DropDownListDept" runat="server" Style="border-style: inset none none inset;" DataSourceID="SqlDataSourceDept" DataTextField="DeptName" DataValueField="DeptId" AppendDataBoundItems="true">
                        <asp:ListItem Value="ALL">ALL</asp:ListItem>
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="SqlDataSourceDept" runat="server" ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" SelectCommand=""></asp:SqlDataSource>
                </td>
                <td style="height: 56px; font-weight: normal">
                    <asp:Label ID="Label2" runat="server" Text="Project Type" ForeColor="Black"></asp:Label>
                    <asp:DropDownList ID="DropDownListProjectType" runat="server" Style="border-style: inset none none inset;" DataSourceID="SqlDataSourceProjectType" DataTextField="TypeName" DataValueField="TypeId" AppendDataBoundItems="true">
                        <asp:ListItem Value="ALL">ALL</asp:ListItem>
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="SqlDataSourceProjectType" runat="server" ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>"
                        SelectCommand="select TypeId,TypeName from ProjectType_M"></asp:SqlDataSource>
                </td>

                <td rowspan="2">
                    <asp:Button ID="view" runat="server" Text="view" OnClick="viewclick" BorderColor="Black" Width="90px" Height="40px" CausesValidation="true" ValidationGroup="validation" />
                </td>
            </tr>

            <tr>
                <td style="height: 56px; font-weight: normal">
                    <asp:Label ID="Label1" runat="server" Text="Project Status" ForeColor="Black"></asp:Label>
                    <asp:DropDownList ID="DropDownListProjectStatusType1" runat="server" Style="border-style: inset none none inset;" DataSourceID="SqlDataSourceProjectStatusType" DataTextField="StatusName" DataValueField="StatusId" AppendDataBoundItems="true">
                        <asp:ListItem Value="ALL">ALL</asp:ListItem>
                    </asp:DropDownList>
                    <asp:SqlDataSource ID="SqlDataSourceProjectStatusType" runat="server" ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>"
                        SelectCommand="select StatusId,StatusName from Status_Project_M where StatusId!='CAN'"></asp:SqlDataSource>
                </td>
                <td style="height: 56px; font-weight: normal">
                    <asp:Label ID="LabelFromDate" runat="server" Text="From Date" ForeColor="Black"></asp:Label>
                    <asp:TextBox ID="TextBoxFromDate" runat="server"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="TextBoxFromDate" Format="dd/MM/yyyy">
                    </asp:CalendarExtender>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBoxFromDate" Display="None"
                        ErrorMessage="Please enter From date" InitialValue=" " ValidationGroup="validation"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator15" runat="server" Display="None" ControlToValidate="TextBoxFromDate" ValidationGroup="validation"
                        ErrorMessage="From date in (dd/mm/yyyy) format" SetFocusOnError="true"
                        ValidationExpression="^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$">
                    </asp:RegularExpressionValidator>
                </td>
                <td style="height: 56px; font-weight: normal">
                    <asp:Label ID="LabelToDate" runat="server" Text="To Date" ForeColor="Black"></asp:Label>
                    <asp:TextBox ID="TextBoxToDate" runat="server" Style="border-style: inset none none inset;"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="TextBoxToDate" Format="dd/MM/yyyy">
                    </asp:CalendarExtender>
                </td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TextBoxToDate" Display="None"
                    ErrorMessage="Please enter From date" InitialValue=" " ValidationGroup="validation"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" Display="None" ControlToValidate="TextBoxToDate" ValidationGroup="validation"
                    ErrorMessage="From date in (dd/mm/yyyy) format" SetFocusOnError="true"
                    ValidationExpression="^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$">
                </asp:RegularExpressionValidator>
            </tr>
        </table>
        <br />
    </center>
    <br />

    <center>
        <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release">
        </asp:ScriptManager>
        &nbsp;
  <center><b><asp:Label ID="lblnote" Text="Note : * Indicates LeadPI" Visible="false" Font-Size="20px" runat="server"></asp:Label></b></center>
        <br />
        <rsweb:ReportViewer ID="ReportViewer1" runat="server" Height="562px" Width="1000px"></rsweb:ReportViewer>
    </center>
</asp:Content>


