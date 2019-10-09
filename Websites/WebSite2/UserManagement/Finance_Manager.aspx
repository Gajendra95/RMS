<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPageRMS.master" AutoEventWireup="true" CodeFile="Finance_Manager.aspx.cs" Inherits="UserManagement_Finance_Manager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Panel ID="Panel1" runat="server">
        <center>
     <table>
    
        <tr>

        <td>Select User Name</td>
         <td><asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="SqlDataSource1" OnSelectedIndexChanged="DropDownList1OnSelectedIndexChanged" AutoPostBack="true"
             DataTextField="FirstName" DataValueField="User_Id" AppendDataBoundItems="true">
            <asp:ListItem Value="">--select--</asp:ListItem>
        </asp:DropDownList></td>
                
          <td><asp:Button ID="Butselect" runat="server" onclick="Butselect_Click" Text="Select" ValidationGroup="Validation" CausesValidation="true"
            Width="105px" /></td>

           </tr>
            <tr>

          <td>
                
                </td>
                </tr>
            
   
            <tr>
                <td>
                    <br />
                </td>
            </tr>
            </table>
      
       <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" SelectCommand="select u.User_Id, FirstName from User_M u, User_Role_Map r where u.User_Id=r.User_Id and r.Role_Id='6'  "></asp:SqlDataSource></td>
   
            
                <br />
                <br />
             </center>
        </asp:Panel>
    <div style="margin-left:350px">

   
                 <asp:Panel ID="Panel2" runat="server" ScrollBars="Vertical" Width="500px" Height="350px" >
                <asp:GridView ID="GridViewBU" OnRowDataBound="GridViewBU_RowDataBound" 
                    runat="server" CellPadding="4" ForeColor="#333333" GridLines="None"  >
                    <AlternatingRowStyle BackColor="White" />
            <Columns>
            <asp:TemplateField>
            <ItemTemplate>
                    <asp:CheckBox ID="CheckBoxMark1" runat="server" AutoPostBack="true" Checked="false"  OnCheckedChanged="CheckBox1_CheckedChanged"/>
                </ItemTemplate>
                
            </asp:TemplateField>
            </Columns>
                     <HeaderStyle BackColor="#0B532D" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#6386C6" ForeColor="#000000" HorizontalAlign="Center" />
        <RowStyle BackColor="#FFFFFF" ForeColor="#333333" />
        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
        <SortedAscendingCellStyle BackColor="#FDF5AC" />
        <SortedAscendingHeaderStyle BackColor="#4D0000" />
        <SortedDescendingCellStyle BackColor="#FCF6C0" />
        <SortedDescendingHeaderStyle BackColor="#820000" />
        <EditRowStyle BorderColor="#FF6600" BorderStyle="Solid" />
            </asp:GridView>
            
            </asp:Panel>
        <div style="margin-left:160px">
             <asp:Button ID="Butsave" runat="server" OnClick="Butsave_Click"  Text="SAVE" Enabled="false" ValidationGroup="Validation" CausesValidation="true"
            Width="130px" />
        </div>
       
       </div>
            
 
            <br />
       <br />
       <br />
   
</asp:Content>

