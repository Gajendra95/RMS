<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPageRMS.master" AutoEventWireup="true" CodeFile="UsersUpload.aspx.cs" Inherits="UserManagement_UsersUpload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" DisplayMode="BulletList" 
     HeaderText="Validation issues" ShowSummary="False" ValidationGroup="validation" />
     <script type="text/javascript" src="../Scripts/jquery-1.4.1.min.js"></script>
<script type="text/javascript">
    function ShowProgress() {
        setTimeout(function () {
            var modal = $('<div />');
            modal.addClass("modal");
            $('body').append(modal);
            var loading = $(".loading");
            loading.show();
            var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
            var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
            loading.css({ top: top, left: left });
        }, 200);
    }
    $('form').live("submit", function () {
        ShowProgress();
    });
</script>
<style type="text/css">
    .modal
    {
        position: absolute;
        top: 0;
        left: 0;
        z-index: 200;
        opacity: 0.8;
        filter: alpha(opacity=80);
        -moz-opacity: 0.8;
        min-height: 100%;
        width: 100%;
    }
    .loading
    {
        font-family: Arial;
        font-size: 8pt;
        border: 2px solid;
        width: 200px;
        height: 100px;
        display: none;
        position: absolute;
        background-color: White;
        z-index: 800;
    }
</style>
     <asp:Panel ID="panel" runat="server" GroupingText="Upload User Data" Font-Bold="true"  BackColor="#E0EBAD">
<center>
<table>

<tr>
<td>
    <%--    <asp:FileUpload ID="F_Upload" runat="server" BorderColor="#996600" BorderStyle="Inset" />
         <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please select file"
         ValidationGroup="validation" ControlToValidate="F_Upload" Display="None"></asp:RequiredFieldValidator>
                             <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
               ErrorMessage="!Upload only EXCEL(.xls,.XLS,.xlsx,.XLSX) file" ControlToValidate="F_Upload" ValidationGroup="validation"
            
                             ValidationExpression="[a-zA-Z0_9].*\b(.xls|.XLS|.xlsx|.XLSX)\b"
                ForeColor="Red"></asp:RegularExpressionValidator>--%>
</td>
</tr>
<tr><td><center>Note: To update HR data click on upload button</center></td></tr>
<tr><td></td></tr>
<tr>
    <td  align="center"> <asp:Button ID="upload" runat="server" Text="Upload" onclick="upload_Click"/></td>
</tr>
<tr><td></td></tr>
<tr><td></td></tr>
<tr><td></td></tr>
</table>
<asp:Panel ID="panel1" runat="server" GroupingText="Previously updated user record(Max. 10)" Font-Bold="true"  BackColor="#E0EBAD">
<table>
<tr><td><asp:GridView ID="GridView1" runat="server"   OnRowCommand="GVViewUploadedRecordsView_SelectedIndexChanged"
             HeaderStyle-BackColor="#CC6600" HeaderStyle-ForeColor="Black" OnRowDataBound="gridDetail_RowDataBound"
             PagerStyle-BackColor="#CC6600" PagerStyle-ForeColor="White" CellPadding="3" CellSpacing="3" PagerStyle-Width="4" PagerStyle-Height="4" Visible="False" 
        BorderColor="#FF6600" BorderStyle="Solid"
             AutoGenerateColumns="False" DataSourceID="SqlDataSource1">
        <Columns>
            <asp:BoundField DataField="Date" HeaderText="Date" SortExpression="Date" />
            <asp:BoundField DataField="NoOfNewRecord" HeaderText="New Record" 
                SortExpression="NoOfNewRecord" />
            <asp:BoundField DataField="NoOfModifiedRecord" HeaderText="Upated Record" 
                SortExpression="NoOfModifiedRecord" />
                <asp:TemplateField ShowHeader="false" >
            <ItemTemplate>
           <asp:Label ID="FilePath" runat="server" Text='<%# Eval("ExcelPath") %>'  Visible="false"></asp:Label>
            </ItemTemplate>
            </asp:TemplateField>
           <asp:TemplateField HeaderText="Download File">
            <ItemTemplate>
                 <asp:LinkButton runat="server" ID="lnkView" CommandArgument='<%#Eval("ExcelPath") %>'
         CommandName="VIEW">Download</asp:LinkButton>
         <asp:Label runat="server" ID="label" Text="No File Generated"></asp:Label>
            </ItemTemplate>
          </asp:TemplateField>
        </Columns>
         <HeaderStyle BackColor="#0B532D" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#0B532D" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#FFFFFF" ForeColor="#333333" />
        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
        <SortedAscendingCellStyle BackColor="#FDF5AC" />
        <SortedAscendingHeaderStyle BackColor="#4D0000" />
        <SortedDescendingCellStyle BackColor="#FCF6C0" />
        <SortedDescendingHeaderStyle BackColor="#820000" />
        <EditRowStyle BorderColor="#FF6600" BorderStyle="Solid" />
    </asp:GridView>
         <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
             ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" 
             SelectCommand="SELECT TOP 10 [Date], [NoOfNewRecord], [NoOfModifiedRecord], [ExcelPath] FROM [HRDataTracker] order by  Date desc">
         </asp:SqlDataSource></td></tr>
</table>
</asp:Panel>
</center>
    <asp:GridView ID="GridExcelData" runat="server" Visible="false">
    </asp:GridView>
 <div id="div"  runat="server" class="loading" align="center">
    Please wait.<br />
    <br />
    <img src="../Images/updating.gif" alt="" />
</div>       
</asp:Panel>

</asp:Content>

