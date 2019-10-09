<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPageRMS.master" AutoEventWireup="true" CodeFile="InstDeptAutoApprove.aspx.cs" Inherits="UserManagement_InstDeptAutoApprove" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" DisplayMode="BulletList" 
     HeaderText="Validation issues" ShowSummary="False" ValidationGroup="Validation" />

     <asp:Panel ID="PanelAutoAprvl" runat="server" GroupingText="AutoApprove" BackColor="#E0EBAD">
<table style="margin-left:350px;">
     
     
            <tr>
    <td>InstituteName</td>
       <td>
       <asp:DropDownList ID="DDLinstitutename" runat="server" DataSourceID="SqlDataSource2"  AppendDataBoundItems="true" DataTextField="Institute_Name" DataValueField="Institute_Id" OnSelectedIndexChanged="DDLinstitutename_SelectedIndexChanged" AutoPostBack="true" >
            <asp:ListItem Value="">--Select--</asp:ListItem>
       </asp:DropDownList>
   <asp:RequiredFieldValidator ID="Reqinstitue" runat="server" ControlToValidate="DDLinstitutename" ErrorMessage="InstituteName Required" ValidationGroup="Validation" Display="None" InitialValue="" ></asp:RequiredFieldValidator>
    </td>
       <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" SelectCommand="SELECT DISTINCT [Institute_Name], [Institute_Id] FROM [Institute_M]"></asp:SqlDataSource>

</tr>
        <tr>
            <td>DepartmentName</td>
            <td>
        <asp:DropDownList ID="DDLdeptname" AppendDataBoundItems="true" TextFormatString="" runat="server" DataSourceID="SqlDataSource1" DataTextField="DeptName" DataValueField="DeptId" >
             <asp:ListItem Value="">--Select--</asp:ListItem>
        </asp:DropDownList>
       <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="None" ControlToValidate="DDLdeptname" ErrorMessage="DepartmentName Required" ValidationGroup="Validation" InitialValue="" ></asp:RequiredFieldValidator>
            </td>
       <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" SelectCommand=""></asp:SqlDataSource>

</tr>

<tr>
<td><asp:Label ID="lblAutoApprove" runat="server" Text="AutoApprove" ></asp:Label> </td>
            <td>
        <asp:DropDownList ID="DropDownListAutoApprovee" runat="server"  >
        <asp:ListItem Value="Y">Yes</asp:ListItem>
          <asp:ListItem Value="N">No</asp:ListItem>
                    </asp:DropDownList>
                  </td>
      

</tr>
<tr>
<td><asp:Label ID="LabelRemarks" runat="server" Text="Remarks" ></asp:Label> </td>
            <td>
        <asp:TextBox ID="TextBoxRemarks" runat="server"  >
       
                    </asp:TextBox>
                       <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="None" ControlToValidate="TextBoxRemarks" ErrorMessage="Enter Remarks" ValidationGroup="Validation"  ></asp:RequiredFieldValidator>
   
                  </td>
      

</tr>
         
<tr>
<td>

</td>

<td></td>
</tr>
<tr>
<td>

</td>

<td></td>
</tr>
<tr>
<td>

</td>

<td></td>
</tr>
<tr>

<td  colspan="2" align="center">
  <asp:Button ID="btninsert" runat="server" Text="INSERT" OnClick="btn_insert" ValidationGroup="Validation" CausesValidation="true"/>
      
</td>
</tr>
</table>

</asp:Panel>
</asp:Content>

