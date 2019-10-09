<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPageRMS.master" AutoEventWireup="true" CodeFile="ManageAgency.aspx.cs" Inherits="GrantEntry_ManageAgency" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="True" DisplayMode="BulletList" 
     HeaderText="Validation issues" ShowSummary="False" ValidationGroup="validation" />
      <script type = "text/javascript">
        


         function setRow(obj) {

             var sndID = obj.id;
             var sndrID = document.getElementById('<%= senderID.ClientID %>');
             sndrID.value = sndID;

         }

         </script>
    <script type = "text/javascript">
        function setRow(obj) {
            var row = obj.parentNode.parentNode;
            var rowIndex = row.rowIndex - 1;
            var sndID = obj.id;
            var sndrID = document.getElementById('<%= senderID.ClientID %>');
           sndrID.value = sndID;
           var rowNo = document.getElementById('<%= rowVal.ClientID %>');
           rowNo.value = rowIndex;
       }
    </script>
    <asp:ScriptManager ID="Scriptmanager1" runat="server"/>
 
    <asp:Panel ID="Panel1" runat="server" ForeColor="Black"  GroupingText="Manage Agency"    Font-Bold="true" Style="background-color:#E0EBAD">
        <br />
        <center>
        <table>
            <tr>
                <td style="height: 38px">
                    FundingAgencyId
                </td>
                
                <td style="height: 38px">
                <asp:TextBox ID="txtFAId" runat="server" OnTextChanged="AgencyTextChanged" AutoPostBack="true"></asp:TextBox>
                </td>
                <td>
                 <asp:ImageButton ID="imageBkCbtn" runat="server" ImageUrl="~/Images/srchImg.gif" OnClientClick="setRow(this)" OnClick="showPop"  /></td>
                <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="imageBkCbtn" PopupControlID="popupselectNo"
                    BackgroundCssClass="modelBackground"  >
                 </cc1:ModalPopupExtender>
            </tr>
            <tr>
                <td style="height: 38px">
                    FundingAgencyName
                </td>
                <td style="height: 38px">
                <asp:TextBox ID="txtFAname" runat="server"></asp:TextBox>
                </td>
            </tr>           
            <tr>
                <td>
                    AgentType
                </td>
                 <td class="auto-style61">
     <asp:DropDownList ID="DropDownAgentType" runat="server" Width="130px" DataSourceID="SqlDataSourceDropDownListGrUnit" DataTextField="UnitName" DataValueField="UnitId" Enabled="true" Height="16px" ></asp:DropDownList>
     <asp:SqlDataSource ID="SqlDataSourceDropDownListGrUnit" runat="server" ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" 
           SelectCommand="select UnitId,UnitName from ProjectUnit_M">
   </asp:SqlDataSource>                              
 </td>
 </tr>
             <tr>
                <td style="height: 38px">
                    Contact No
                </td>
                <td style="height: 38px">
                    <asp:TextBox ID="txtContactNo" runat="server"></asp:TextBox>
                </td>
            </tr>
                <tr>
                <td style="height: 34px">
                    Pan No
                </td>
                <td style="height: 34px">
                    <asp:TextBox ID="txtPanNo" runat="server"></asp:TextBox>
                </td>
            </tr>
                <tr>
                <td style="height: 30px">
                    EmailId
                </td>
                <td style="height: 30px">
                    <asp:TextBox ID="txtEmailId" runat="server"></asp:TextBox>
                </td>
            </tr>
               <tr>
                <td style="height: 31px">
                    Address
                </td>
                <td style="height: 31px">
                    <asp:TextBox ID="txtAddress" runat="server"></asp:TextBox>
                </td>
            </tr>
               <tr>
                <td style="height: 40px">
                    State
                </td>
                <td style="height: 40px">
                    <asp:TextBox ID="txtState" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="height: 34px">
                    Country
                </td>
                <td style="height: 34px">
                    <asp:TextBox ID="txtCountry" runat="server"></asp:TextBox>
                </td>
            </tr>
        </table>
        </center>
        <br />
        <div style="margin-left:430px;">
        <asp:Button ID="btninsert" runat="server" Text="INSERT" OnClick="btn_insert" ValidationGroup="validation" CausesValidation="true"/>
        <asp:Button ID="btnupdate" runat="server" Text="UPDATE" OnClick="btnupdate_Click"   ValidationGroup="validation" CausesValidation="true"/>
        <%--<asp:Button ID="ButClear" runat="server" Text="CLEAR" OnClick="BtnClear_Click"/>--%>
        <asp:Button ID="Butview" runat="server" Text="VIEW" OnClick="Butview_Click" />     
            <asp:Button ID="Button2" runat="server" Text="Clear" OnClick="Button2_Click" style="height: 26px" />    
        <asp:Label ID="lblStatus" runat="server"></asp:Label>
            </div>

        <asp:GridView ID="GridViewAgency" runat="server" DataSourceID="SqlDataSourceGridview" PagerSettings-PageButtonCount="10" AllowPaging="True" 
       AutoGenerateColumns="False" CellPadding="4" DataKeyNames="FundingAgencyId" 
       ForeColor="#333333" GridLines="None">
             <AlternatingRowStyle BackColor="White" />
             <Columns>
   <asp:BoundField DataField="FundingAgencyId" HeaderText="FundingAgencyId" SortExpression="FundingAgencyId" ReadOnly="True" />
    <asp:BoundField DataField="FundingAgencyName" HeaderText="FundingAgencyName" SortExpression="FundingAgencyName" />
 <asp:BoundField DataField="AgentType" HeaderText="AgentType" SortExpression="AgentType" />
 <asp:BoundField DataField="ContactNo" HeaderText="ContactNo" SortExpression="ContactNo" />
  <asp:BoundField DataField="PanNo" HeaderText="PanNo" SortExpression="PanNo" />
                 <asp:BoundField DataField="EmailId" HeaderText="EmailId" SortExpression="EmailId" />
                 <asp:BoundField DataField="Address" HeaderText="Address" SortExpression="Address" />
                 <asp:BoundField DataField="State" HeaderText="State" SortExpression="State" />
                 <asp:BoundField DataField="Country" HeaderText="Country" SortExpression="Country" />
             </Columns>
    <HeaderStyle BackColor="#0B532D" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#0B532D" ForeColor="#F0F8FF" HorizontalAlign="Center" />
        <RowStyle BackColor="#FFFFFF" ForeColor="#333333" />
        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
        <SortedAscendingCellStyle BackColor="#FDF5AC" />
        <SortedAscendingHeaderStyle BackColor="#4D0000" />
        <SortedDescendingCellStyle BackColor="#FCF6C0" />
        <SortedDescendingHeaderStyle BackColor="#820000" />
        <EditRowStyle BorderColor="#FF6600" BorderStyle="Solid" />
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSourceGridview" runat="server"
       ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" SelectCommand="SELECT CAST(REPLACE (FundingAgencyId,'.','') AS INT) As FundingAgencyId,FundingAgencyName,AgentType,ContactNo,
PanNo,EmailId,Address,State,Country FROM [ProjectFundingAgency_M]order by FundingAgencyId desc">
        </asp:SqlDataSource>
       <asp:Button ID="Butback" runat="server" Text="BACK" OnClick="btn_back" />


    </asp:Panel>

    
        <asp:Panel ID="popupselectNo" runat="server"  style="margin-top:90px; top: 4992px; left: 62px;" CssClass="modelPopup" 
          BorderStyle="Groove" BorderColor="Black" BackColor="White" Visible="false" >
        <table>
            <tr>
                <td>
                    <br />
                </td>
            </tr>
        <tr>
           
<th class="auto-style1">Search Name: </th>
<td class="auto-style1"><asp:TextBox ID="agencysearch" runat="server"></asp:TextBox> </td></tr>
        <tr>
    <td colspan="2" align="center">
    <asp:Button ID="searchid" runat="server" Text="Search" OnClick="AgencyIdChanged" />
</td>
</tr>   
            </table>
        <asp:GridView ID="popGridagency" runat="server" AutoGenerateSelectButton="true" 
            OnPageIndexChanging="popGridagency_pageindex" OnSelectedIndexChanged="popSelected"
             AllowSorting="true" Height="203px" Width="900px" AllowPaging="true"  Visible="false"
            PageSize="5" >
<FooterStyle BackColor="White" ForeColor="#8C4510"  />
        <HeaderStyle BackColor="#0b532d" Font-Bold="True" ForeColor="White" />
        <PagerStyle ForeColor="#0B532D" HorizontalAlign="Center" />
        <RowStyle BackColor="#FFFFFF" ForeColor="#000000" />
        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#FFF1D4" />
        <SortedAscendingHeaderStyle BackColor="#B95C30" />
        <SortedDescendingCellStyle BackColor="#F1E5CE" />
        <SortedDescendingHeaderStyle BackColor="#93451F" />
</asp:GridView>
        <asp:SqlDataSource ID="SqlDataSourceTextBoxGrantAgency" runat="server"
             ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>"
             SelectCommand="SELECT  CAST(REPLACE (FundingAgencyId,'.','') AS INT) As Id,UPPER([FundingAgencyName]) as Name FROM [ProjectFundingAgency_M] order by Id desc">
        
        </asp:SqlDataSource>
    <asp:Button ID="Button1" runat="server" Text="EXIT" />
         </asp:Panel>

    <asp:HiddenField ID="rowVal" runat="server" />
      <asp:HiddenField ID="senderID" runat="server" />
    <asp:RequiredFieldValidator ID="RequiredFieldValidator5"
                        runat="server" 
                        ErrorMessage="Enter Agency Id"
                        ControlToValidate="txtFAId"
                        ValidationGroup="validation"
                        Display="None" ></asp:RequiredFieldValidator>
      <asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                        runat="server" 
                        ErrorMessage="Enter Agency Name"
                        ControlToValidate="txtFAname"
                        ValidationGroup="validation"
                        Display="None" ></asp:RequiredFieldValidator>
    <asp:RegularExpressionValidator ID="RegularExpressionValidator3"
                         runat="server" 
                         ErrorMessage="PAN Number should be in the followed format ABCDE1234X"
                         ValidationExpression="^[A-Z]{5}[0-9]{4}[A-Z]{1}$"
                         ControlToValidate="txtPanNo"
                         ValidationGroup="validation"
                         Display="None"
                          ></asp:RegularExpressionValidator>
    <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ErrorMessage="Mobile no. should be in digits" ValidationExpression="\d+$" ControlToValidate="txtContactNo" ValidationGroup="validation" Display="None" SetFocusOnError="true" ></asp:RegularExpressionValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator12" runat="server" ControlToValidate="txtContactNo" ValidationExpression="^.{10,12}$" Display="None" ValidationGroup="validation" SetFocusOnError="true"   
                ErrorMessage="Mobile no. should be Minimum of 10 digit or maximum of 12 digit"></asp:RegularExpressionValidator>
    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage=" Invalid e-Mail ID " ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="txtEmailId" Display="None" ValidationGroup="validation" SetFocusOnError="true" ></asp:RegularExpressionValidator>
</asp:Content>

