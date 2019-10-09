<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPageRMS.master" AutoEventWireup="true" CodeFile="ViewImpactFactor.aspx.cs" Inherits="ViewImpactFactor" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

 <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" DisplayMode="BulletList" 
     HeaderText="Validation issues" ShowSummary="False" ValidationGroup="validation"/>

     <center>
     <asp:Panel ID="panelSearch" runat="server" GroupingText="View Impact Factor" Font-Bold="true" Font-Underline="true" BackColor="#E0EBAD">
     

<table  width="100%"  border="1">
<tr>

<td>

<b>Category:</b>
</td>
<td>       <asp:DropDownList ID="DropDownListCategory" runat="server"
                             DataSourceID="SqlDataSourceCategory" DataTextField="Category_Name" DataValueField="Category_Id" AppendDataBoundItems="true">
                             <asp:ListItem Value="A">All</asp:ListItem>
                          </asp:DropDownList> 
            <asp:SqlDataSource ID="SqlDataSourceCategory" runat="server" 
                ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" 
                
                SelectCommand="select Category_Id,Category_Name from JournalCategory_M" 
                ProviderName="System.Data.SqlClient">
           
            </asp:SqlDataSource>
</td>
<td>
<b>Year:</b>
</td>

<td>
<asp:TextBox ID="TextYear" runat="server"></asp:TextBox>
     
          <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" Display="None"  ControlToValidate="TextYear" ValidationGroup="validation"
                ErrorMessage="Entered Proper Year" SetFocusOnError="true"  
                ValidationExpression="^[0-9]{4}$">
                </asp:RegularExpressionValidator>
</td>

<td>

                <asp:Button ID="ButtonSearch" runat="server" Text="Search" OnClick= "searchclick" ValidationGroup="validation" CausesValidation="true" />
</td>
</tr>
</table>
<br />

<br />
 <asp:GridView ID="GridViewJournalSearch" runat="server" AutoGenerateColumns="False"  AllowPaging="True" OnPageIndexChanging="GridViewJournalSearchOnPageIndexChanging"
            PagerSettings-PageButtonCount="10" DataSourceID="SqlDataSourceJournal"
            PageSize="10"  EmptyDataText="No Data found"
           CellPadding="4" ForeColor="#333333" GridLines="None"> 
               
            <AlternatingRowStyle BackColor="White" />
               
            <Columns>
                <asp:BoundField DataField="ISSN" HeaderText="ISSN" ReadOnly="True" SortExpression="ISSN" />
                <asp:BoundField DataField="AbbreviatedTitle" HeaderText="Abbreviated Name" SortExpression="AbbreviatedTitle" ReadOnly="True"/>
                <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title" ReadOnly="True"/>
                   <asp:BoundField DataField="Year" HeaderText="Year" SortExpression="Year" ReadOnly="True" />
            
                <asp:BoundField DataField="ImpactFactor" HeaderText="ImpactFactor" SortExpression="ImpactFactor" ReadOnly="True" />
                <asp:BoundField DataField="fiveImpFact" HeaderText="5-Year Impact Factor" SortExpression="fiveImpFact" ReadOnly="True" />
              
  <%--           <asp:BoundField DataField="Comments" HeaderText="Comments" SortExpression="Comments" ReadOnly="True" />--%>
             
        
                
            </Columns>
           <HeaderStyle BackColor="#0b532d" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#0b532d" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#FFFFFF" ForeColor="#333333" />
        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
        <SortedAscendingCellStyle BackColor="#FDF5AC" />
        <SortedAscendingHeaderStyle BackColor="#4D0000" />
        <SortedDescendingCellStyle BackColor="#FCF6C0" />
        <SortedDescendingHeaderStyle BackColor="#820000" />
        <EditRowStyle BorderColor="#FF6600" BorderStyle="Solid" />
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSourceJournal" runat="server" 
         ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" 
         SelectCommand="select k.Id as ISSN,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact,Comments,Year from Journal_M k   LEFT join Journal_IF_Details j on j.id=k.Id " >
        </asp:SqlDataSource>
        
        <br />
             
     </asp:Panel>
</center>
</asp:Content>

