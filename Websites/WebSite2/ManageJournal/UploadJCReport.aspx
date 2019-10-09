<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPageRMS.master" AutoEventWireup="true" CodeFile="UploadJCReport.aspx.cs" Inherits="Upload_JC_Report"  MaintainScrollPositionOnPostback="true" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<asp:ToolkitScriptManager ID="Scriptmanager1" runat="server"/>

    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" DisplayMode="BulletList" 
     HeaderText="Validation issues" ShowSummary="False" ValidationGroup="validation"/>
     <center>

     <asp:Panel ID="panel" runat="server" GroupingText="File Upload" Font-Bold="true" Font-Underline="true" BackColor="#E0EBAD">
<%--<table width="100%" style="text-align:center">
<tr>
<th> File Upload</th>
</tr>
</table>--%>

<br />

<table border="1">

<tr>
<td><b>Year</b></td>

<td><asp:TextBox ID="TextYear" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please enter the proper year"
         ValidationGroup="validation" ControlToValidate="TextYear" Display="None"></asp:RequiredFieldValidator>
          <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" Display="None"  ControlToValidate="TextYear" ValidationGroup="validation"
                ErrorMessage="Entered Year should be in number" SetFocusOnError="true"  
                ValidationExpression="^[0-9]{4}$">
                </asp:RegularExpressionValidator>
</td>
</tr>
<tr>
    <th>Select File To Upload :</th>
    <td>
        <asp:FileUpload ID="F_Upload" runat="server" BorderColor="#996600" BorderStyle="Inset" />
         <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please select file"
         ValidationGroup="validation" ControlToValidate="F_Upload" Display="None"></asp:RequiredFieldValidator>


            <br />
                       
        </td>

 
</tr>



<tr>
    <td colspan="4" align="center"> <asp:Button ID="upload" runat="server" Text="Upload" onclick="upload_Click" ValidationGroup="validation" CausesValidation="true" /></td>
</tr>
</table>


<br />
<br />



<table >

<tr>
<td>
       <asp:LinkButton ID="LinkButton1" runat="server" onclick="LinkButtonJCRUploadInst_Click" ForeColor="Black">Instructions to generate the JCR file</asp:LinkButton>

</td>
</tr>
</table>

    <asp:GridView ID="GridExcelData" runat="server" OnRowDataBound="GridExcelData_RowDataBound" Visible="false">
    </asp:GridView>
    </asp:Panel>
    </center>

</asp:Content>

