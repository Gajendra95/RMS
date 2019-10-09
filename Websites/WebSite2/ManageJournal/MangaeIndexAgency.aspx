<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPageRMS.master" AutoEventWireup="true" CodeFile="MangaeIndexAgency.aspx.cs" Inherits="MangaeIndexAgency"  MaintainScrollPositionOnPostback="true" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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


              <asp:ValidationSummary ID="Validation1" runat="server" 
        ShowMessageBox="true" ValidationGroup="Validation" ShowSummary="False"/>   
                <cc1:ToolkitScriptManager ID="Scriptmanager1" runat="server"/>
                 
    <asp:Panel ID="PanelIndex" runat="server" Visible="true" GroupingText="Index Agency"  Font-Bold="true" Font-Underline="true" BackColor="#E0EBAD"  >
        <table>
            <tr>
                <td><b>Index Agency:</b></td>
                <td><asp:TextBox ID="TextIndexAgencyCode" runat="server" MaxLength="11" TabIndex="1" Width="350px" OnTextChanged="IndexAgencyTextChanged" AutoPostBack="true"></asp:TextBox></td>
                <td><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Enter the Index code" 
        Display="None" ControlToValidate="TextIndexAgencyCode" ValidationGroup="Validation"></asp:RequiredFieldValidator></td>
                <td> <asp:ImageButton ID="imageBkCbtn" runat="server" ImageUrl="~/Images/srchImg.gif" OnClientClick="setRow(this)" OnClick="showPop"  />

                 <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="imageBkCbtn" PopupControlID="popupPanelPop"
                    BackgroundCssClass="modelBackground"  >
                 </cc1:ModalPopupExtender></td>
         
            </tr>
       
            <tr>
                <td><b>IndexAgnecy Name:</b></td>
                <td><asp:TextBox ID="TextIndexAgencyName" runat="server" MaxLength="50" Width="350px" TabIndex="2"></asp:TextBox></td>
                <td><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Enter the Index name" 
      Display="None" ControlToValidate="TextIndexAgencyName" ValidationGroup="Validation"></asp:RequiredFieldValidator></td>
                
           </tr>
            <tr>
                <td>
                <asp:Label ID="status" runat="server" Text="Active/Inactive:" Visible="false" Font-Bold="true"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="DrpActInactive" runat="server" Visible="false">
                        <asp:ListItem Value="Y">Active</asp:ListItem>
                        <asp:ListItem Value="N">Inactive</asp:ListItem>
                    </asp:DropDownList>

                </td>

            </tr>
            </table>
            <table>
            <tr>
                  <td>
                <br />
               
            <asp:Button ID="ButClear" runat="server" Text="CLEAR" OnClick="BtnClear_Click" OnClientClick="ClearConfirm()"  />
            </td>
                <td>
                    <br />
               
                    <asp:Button ID="Button1" runat="server" Text="SAVE/UPDATE" OnClick ="Butsave_Click" ValidationGroup="Validation" CausesValidation="true" /></td>
                <td>
               
                    <br />
                    <asp:Button ID="Button2" runat="server"  OnClick="Butview_Click" Text="VIEW"  />
                    </td>
 
            </tr>
         </table>

         <asp:Panel ID="popupPanelPop" runat="server" Visible="false" CssClass="modelPopup" Width ="600px">

<table style="background:white">
<tr></tr>
<tr></tr>
<tr>
<th align="center" colspan="3"> Index Agency </th>
</tr>

<tr>
<td align="center"><b>Search Index Agency Name: </b>
<asp:TextBox ID="IndexcodeSrch" runat="server" ></asp:TextBox> 

  <asp:Button ID="BtnEmployeeSearch" runat="server" Text="Search" OnClick="IndexCodeChanged" />
  <asp:Button ID="Buttonexit" runat="server" Text="EXIT" OnClick="exit" />
  </td> 
  
  </tr>   
  <tr>
  
  </tr>
   <tr>
  
  </tr>
<tr>
<td colspan="3" align="center">
<asp:GridView ID="popGridIndex" runat="server"  AutoGenerateSelectButton="true" EmptyDataText="No Index Agency Found"
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


<asp:SqlDataSource ID="SqlDataSourcePopIndex" runat="server" ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" 
SelectCommand="SELECT  top  10  AgencyId, AgencyName FROM IndexAgency_M" ProviderName="System.Data.SqlClient">
</asp:SqlDataSource>

</td>
</tr>
</table>

</asp:Panel>
   
         <br />

             <asp:Panel ID="PanelView" runat="server" Visible="true" GroupingText="View IndexAgency" >
              <asp:GridView ID="GridViewIndex" runat="server" AllowPaging="True" DataSourceID="SqlDataSourceIndexView"
            AutoGenerateColumns="False" 
            CellPadding="4" PagerSettings-PageButtonCount="10" 
            PageSize="8" DataKeyNames="AgencyId"
               ForeColor="#333333" GridLines="None"> 
                      <AlternatingRowStyle BackColor="White" />
                      <Columns>
                <asp:BoundField DataField="AgencyId" HeaderText="AgencyId" ReadOnly="True" SortExpression="AgencyId"/>
                <asp:BoundField DataField="AgencyName" HeaderText="AgencyName" SortExpression="AgencyName"  ReadOnly="false"/>
                <asp:BoundField DataField="UserId" HeaderText="UserId" SortExpression="UserId" ReadOnly="true"/>
                <asp:BoundField DataField="Date" HeaderText="Date" SortExpression="Date" ReadOnly="true" />
                
                 <asp:TemplateField HeaderText="status">
                   <ItemTemplate>
    <asp:Label ID="lblstatus" runat="server" Text='<%# Eval("Active")%>'></asp:Label>
    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList ID="Dropdownactive"  SelectedValue='<%# Eval("Active")%>'  runat="server" >
                            <asp:ListItem  Value="A">
                                Active 
                            </asp:ListItem>
                            <asp:ListItem Value="C">
                                Inactive
                            </asp:ListItem>
                        </asp:DropDownList>
                        
                    </EditItemTemplate>
                 </asp:TemplateField>
               
            </Columns>
           

<PagerSettings PageButtonCount="8"></PagerSettings>

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
         <asp:SqlDataSource ID="SqlDataSourceIndexView" runat="server" 
         ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" 
         SelectCommand="select top 20 * from IndexAgency_M order by CreatedDate desc" >
        

     </asp:SqlDataSource>
     </asp:Panel>

           
   
        

            </asp:Panel>  
       
 </center>
     <asp:HiddenField ID="senderID" runat="server" />

</asp:Content>

