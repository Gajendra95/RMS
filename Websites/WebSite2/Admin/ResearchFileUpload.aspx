<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPageRMS.master" AutoEventWireup="true" CodeFile="ResearchFileUpload.aspx.cs" Inherits="Admin_EditResearchArea" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
     <script src="../Scripts/jquery-1.7.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-1.7.1.min.js" type="text/javascript"></script>
       <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" DisplayMode="BulletList" 
     HeaderText="Validation issues" ShowSummary="False" ValidationGroup="validation"/>
 <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="true"></asp:ToolkitScriptManager>
   
    <center>
                    <asp:Label ID="lablPanelTitle" runat="server" Text="Research Area Interest File Upload" Font-Bold="true"></asp:Label></center><br />
       <asp:Panel ID="panel" runat="server" GroupingText="File Upload" Font-Bold="true" Font-Underline="true" BackColor="#E0EBAD">         
    <center> <table border="1">

<tr>
    <th>Select File To Upload :</th>
    <td>
        <asp:FileUpload ID="FileUpload" runat="server" />
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please select file"
         ValidationGroup="validation" ControlToValidate="FileUpload" Display="None"></asp:RequiredFieldValidator>
   </td>


   <td>
     <asp:Button ID="upload" runat="server" Text="Upload"  OnClick="upload_Click" Width="75px" ValidationGroup="validation" CausesValidation="true"/>

        <asp:GridView ID="GridExcelData" runat="server" OnRowDataBound="GridExcelData_RowDataBound" Visible="false">
    </asp:GridView>
   </td>
    </tr>
        </table></center>
              </asp:Panel>
</asp:Content>

