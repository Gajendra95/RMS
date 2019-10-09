<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPageRMS.master" AutoEventWireup="true" CodeFile="GrantFileUpload.aspx.cs" Inherits="GrantEntry_GrantFileUpload" MaintainScrollPositionOnPostback="true" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" DisplayMode="BulletList" 
     HeaderText="Validation issues" ShowSummary="False" ValidationGroup="validation" />
       <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="True" DisplayMode="BulletList" 
     HeaderText="Validation issues" ShowSummary="False" ValidationGroup="validation1" />
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="true"></asp:ToolkitScriptManager>
<asp:Panel ID="panel3" runat="server"  GroupingText="PROJECT FILE UPLOAD" Style="font-weight:bold; border-style:inset;background-color:#E0EBAD"  > 
<center> <div >
      <asp:LinkButton ID="lnkExcelFile" runat="server" Text="Download Sample excel file " Style="color: navy; font-weight: bold;font-size:15px"  OnClick="lnkExcelFile_Click" />
      <br />
      <br />
  
       <asp:Label ID="lblNote" runat="server" Text="Note - Currently Project Upload has been disabled. please contact Directorate of Research (help.rms@manipal.edu)." ForeColor="Black" Visible="false"></asp:Label> 
  </div>    
</center>
    <br />
      <br />
      <br />
 
 <table style="width: 92%">
   <tr>
     <td style="font-weight:normal; width: 120px;">
      <asp:Label ID="Label13" runat="server" Text="UPLOAD"  ></asp:Label>
       </td>
       <td class="auto-style8" style="width: 226px">
            <asp:FileUpload ID="F_Upload" runat="server" BorderColor="#996600" 
                 BorderStyle="Inset" Width="222px" />
             <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server"
                ErrorMessage="! Choose File" ControlToValidate="F_Upload" ForeColor="Red" Display="None" ValidationGroup="validation"></asp:RequiredFieldValidator>
       </td>
        <td style="height: 36px">
            <asp:Button ID="upload" runat="server" Text="Upload" OnClick="upload_Click" ValidationGroup="validation" CausesValidation="true" Width="73px"  />
       </td>
     </tr>
</table>
            
       <br />
      <br />
   <br />
      <br />
   <br />
      <br />
  
                
      
    <asp:GridView ID="GridExcelDataProject" runat="server" OnRowDataBound="GridExcelDataProject_RowDataBound" Visible="false">
    </asp:GridView>
     <asp:GridView ID="GridExcelDataInvestigator" runat="server" OnRowDataBound="GridExcelDataInvestigator_RowDataBound"  Visible="false">
    </asp:GridView>

 
</asp:Panel>
   

</asp:Content>

