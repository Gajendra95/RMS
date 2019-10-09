<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPageRMS.master" AutoEventWireup="true" CodeFile="SearchJournal.aspx.cs" Inherits="SearchJournal" MaintainScrollPositionOnPostback="true" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<%--     <asp:Label ID="lablPanelTitle" runat="server" Text="Search Journal" Font-Bold="true"  ></asp:Label>--%>
   <center>




     
  <style type="text/css">
.modelBackground
{
	background-color:Gray;
	filter:alpha(opacity=70);
	opacity:0.7;
}

.modelPopup
{
	background-color:#EEEEE;
	border-width:3px;
	border-style:solid;
	border-color:Gray;
	font-family:Verdana;
	font-size:medium;
	padding:3px;
	width:450px;
	position:absolute;
	overflow:scroll;
	max-height:400px;
}


</style>

   <script type = "text/javascript">

       function setRow(obj) {

           var sndID = obj.id;
           var sndrID = document.getElementById('<%= senderID.ClientID %>');
           sndrID.value = sndID;

       }
    </script>
          <asp:ToolkitScriptManager ID="Scriptmanager1" runat="server"/>
            
     
          
    <asp:Panel ID="PanelIndex" runat="server" Visible="true" GroupingText="Search Journal" Font-Bold="true" Font-Underline="true" BackColor="#E0EBAD" >

<table  width="100%"  border="1">
<tr>


<td>
<b>Journal Category:</b>
</td>

<td>
<asp:DropDownList ID="dropdownCategory" runat="server"  DataSourceID="SqlDataSource4" DataTextField="Category_Name" DataValueField="Category_Id" AppendDataBoundItems="true">
<asp:ListItem Value=" ">---Select---</asp:ListItem>
</asp:DropDownList>
          <asp:SqlDataSource ID="SqlDataSource4" runat="server" 
                ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" 
                
                SelectCommand="select Category_Id,Category_Name from JournalCategory_M" 
                ProviderName="System.Data.SqlClient">
             
            </asp:SqlDataSource>

</td>
<td>

<b>ISSN:</b>
</td>
<td>                  <asp:TextBox ID="txtJid" runat="server"   Width="200px" ></asp:TextBox>
                             <asp:ImageButton ID="imageBkCbtn" runat="server" ImageUrl="~/Images/srchImg.gif" OnClientClick="setRow(this)" OnClick="showPop" />

                 <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="imageBkCbtn" PopupControlID="popupPanelJournal"
                    BackgroundCssClass="modelBackground"  >
                 </asp:ModalPopupExtender>

</td>
<td>

                <asp:Button ID="ButtonSearch" runat="server" Text="Search" OnClick= "searchclick"  />
</td>
</tr>
</table>
<br />

<br />
 <asp:GridView ID="GridViewJournalSearch" runat="server" AutoGenerateColumns="False"  AllowPaging="True" 
            PagerSettings-PageButtonCount="10" DataSourceID="SqlDataSourceJournal" OnPageIndexChanging="GridViewJournalSearchOnPageIndexChanging"
            PageSize="10"  EmptyDataText="No Data found"
           CellPadding="4" ForeColor="#333333" GridLines="None"> 
               
            <AlternatingRowStyle BackColor="White" />
               
            <Columns>
             <asp:BoundField DataField="ISSN" HeaderText="ISSN" ReadOnly="True" SortExpression="ISSN" />
              <asp:BoundField DataField="Year" HeaderText="Year" ReadOnly="True" SortExpression="Year" />
               <asp:BoundField DataField="AbbreviatedTitle" HeaderText="Abbreviated Name" ReadOnly="True" SortExpression="Name" />
                    <asp:BoundField DataField="Title" HeaderText="Title" ReadOnly="True" SortExpression="Jname" />
                <asp:BoundField DataField="ImpactFactor" HeaderText="Impact Factor" ReadOnly="True" SortExpression="ImpactFactor" />
             
                <asp:BoundField DataField="fiveImpFact" HeaderText="5-Year Impact Factor" SortExpression="fiveImpFact" ReadOnly="True" />
                  
        
                
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
         SelectCommand="select k.Id as ISSN,Year,Title,AbbreviatedTitle,ImpactFactor,fiveImpFact Comments from Journal_M k   LEFT join Journal_IF_Details j on j.id=k.Id" >
        </asp:SqlDataSource>
        
        <br />
             
          
 <asp:Panel ID="popupPanelJournal" runat="server" Visible="false" CssClass="modelPopup" Width="980px">

<table style="background:white">
<tr></tr>
<tr></tr>
<tr>
<th align="center" colspan="3"> Journal</th>
</tr>
<tr></tr>
<tr></tr>
<tr>
<td align="center"><b>Search Journal Name: </b>
<asp:TextBox ID="journalcodeSrch" runat="server" ></asp:TextBox> 
  <asp:Button ID="BtnEmployeeSearch" runat="server" Text="Search" OnClick="JournalCodeChanged" />
  
  <asp:Button ID="Buttonexit" runat="server" Text="EXIT" OnClick="exit" />
  </td>
  </tr>  
<tr>
<td colspan="3">
<asp:GridView ID="popGridJournal" runat="server"  AutoGenerateSelectButton="true" EmptyDataText="No Journal Found"
OnSelectedIndexChanged="popSelected" AllowSorting="true"  >
<FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510"  />
        <HeaderStyle BackColor="#0b532d" Font-Bold="True" ForeColor="White" />
        <PagerStyle ForeColor="#000000" HorizontalAlign="Center" />
        <RowStyle BackColor="#FFFFFF" ForeColor="#000000" />
        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#FFF1D4" />
        <SortedAscendingHeaderStyle BackColor="#B95C30" />
        <SortedDescendingCellStyle BackColor="#F1E5CE" />
        <SortedDescendingHeaderStyle BackColor="#93451F" />
</asp:GridView>


<asp:SqlDataSource ID="SqlDataSourcePopJournal" runat="server" ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" 
           SelectCommand="SELECT top 10 Id as ISSN,Title,AbbreviatedTitle FROM [Journal_M]">
     
                </asp:SqlDataSource>

</td>
</tr>
</table>

</asp:Panel>

            </asp:Panel>  
</center>
      <asp:HiddenField ID="senderID" runat="server" />
</asp:Content>

