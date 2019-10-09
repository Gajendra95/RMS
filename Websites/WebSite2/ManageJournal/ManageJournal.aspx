<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPageRMS.master" AutoEventWireup="true" CodeFile="ManageJournal.aspx.cs" Inherits="ManageJournal" MaintainScrollPositionOnPostback="true" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <center>

 
   <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" DisplayMode="BulletList" 
     HeaderText="Validation issues" ShowSummary="False" ValidationGroup="Validation" />
  <style type="text/css">
.modelBackground
{
	background-color:Gray;
	filter:alpha(opacity=70);
	opacity:0.7;
}

.PnlDesign
        {
            border: solid 1px #000000;
            overflow-y:scroll;
            background-color: #EAEAEA;
            font-size: 15px;
            font-family: Arial;
        }

  .txtbox
        {
            background-image: url(../images/drpdwn.png);
            background-position: right top;
            background-repeat: no-repeat;
            cursor: pointer;
            cursor: hand;
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
      
       
 <asp:Panel ID="panel" runat="server" GroupingText="Managing Journal" Font-Bold="true" Font-Underline="true" BackColor="#E0EBAD">
   <table border="1" style="width: 100%">
                    <tr>
                        <td style="height: 36px">
                               <asp:Label ID="lblID" runat="server" Text="ISSN:" Font-Bold="true"></asp:Label>
                               
                               </td>
                        <td style="height: 36px">             
                             <asp:TextBox ID="txtboxID" runat="server"  OnTextChanged="JournalIDTextChanged"  AutoPostBack="true" Width="200px" ></asp:TextBox>
                             <asp:ImageButton ID="imageBkCbtn" runat="server" ImageUrl="~/Images/srchImg.gif" OnClientClick="setRow(this)" OnClick="showPop" />

                 <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="imageBkCbtn" PopupControlID="popupPanelJournal"
                    BackgroundCssClass="modelBackground"  >
                 </asp:ModalPopupExtender>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
    ErrorMessage="Enter the ID" Display="None" 
    ControlToValidate="txtboxID" ValidationGroup="Validation"></asp:RequiredFieldValidator>
                       
                         </td>
                      
                        <td style="height: 36px">
                           <asp:Label ID="lblTitle" runat="server" Text="Title:" Font-Bold="true"></asp:Label>
                        </td>
                        <td style="height: 36px"  colspan="3" >
                        <asp:TextBox ID="txtboxTitle" runat="server"  Width="600px" TextMode ="MultiLine" ></asp:TextBox>       </td>

                           </tr>

                           <tr>
                                          
                 <td style="height: 36px">
                                <asp:Label ID="Label1" runat="server" Text="Category:" Font-Bold="true" ></asp:Label></td>
                        <td style="height: 36px" >             
             <asp:DropDownList ID="dropdownCategory" runat="server"  DataSourceID="SqlDataSource4" DataTextField="Category_Name" DataValueField="Category_Id" AppendDataBoundItems="true">
<asp:ListItem Value=" ">---Select---</asp:ListItem>
</asp:DropDownList>
          <asp:SqlDataSource ID="SqlDataSource4" runat="server" 
                ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" 
                
                SelectCommand="select Category_Id,Category_Name from JournalCategory_M" 
                ProviderName="System.Data.SqlClient">
             
            </asp:SqlDataSource>
     
                       
                         </td>
                 <td style="height: 36px">
                                <asp:Label ID="lblAbrivatedTitle" runat="server" Text="Abbreviated Title:" Font-Bold="true" ></asp:Label></td>
                        <td style="height: 36px" colspan="3" >             
                       <asp:TextBox ID="txtboxAbrivatedTitle" runat="server"   Width="600px" ></asp:TextBox>
     
                       
                         </td>
                           
                           </tr>
                          <%-- <tr>
                           <td colspan="6" align="center">
                           <asp:Label ID="ImpFactTitle" runat="server" Text="Impact Factor" Font-Bold="true" ></asp:Label>
                           </td>
                           </tr>--%>
                           <%--<tr>

                           
                                <td style="height: 36px">
                             <asp:Label ID="lblYear" runat="server" Text="Year:"  Font-Bold="true"></asp:Label>
                        </td>
                        <td style="height: 36px">
                     <asp:TextBox ID="txtboxYear" runat="server" 
                 ontextchanged="txtboxYear_TextChanged" AutoPostBack="true" Width="200px" ></asp:TextBox>   
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" Display="None"  ControlToValidate="txtboxYear" ValidationGroup="Validation"
                ErrorMessage="Entered Year should be in number" SetFocusOnError="true"  
                ValidationExpression="^[0-9]{4}$">
                </asp:RegularExpressionValidator>
 
                        </td>
             
     
      <td style="height: 36px">  
                  <asp:Label ID="lblImpactFactor" runat="server" Text="Impact Factor:" Font-Bold="true"></asp:Label>
                        </td>
                         
         <td>
      <asp:TextBox ID="txtboxImpactfactor" runat="server"  Width="200px"></asp:TextBox> 
                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" Display="None"  ControlToValidate="txtboxImpactfactor" ValidationGroup="Validation"
                ErrorMessage="Enter the valid Impact Factor" SetFocusOnError="true"  
                ValidationExpression="^[0-9]*(?:\.[0-9]*)?$">
                </asp:RegularExpressionValidator>

 </td>
     
                                <td style="height: 36px">
                    <asp:Label ID="lblFiveYearImpactFactor" runat="server" Text="Five Year ImpactFactor:" Font-Bold="true"></asp:Label>
                        </td>
                        <td style="height: 36px">
                         <asp:TextBox ID="txtboxFiveYearImpactFactor" runat="server"   Width="200px" ></asp:TextBox>
                         <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" Display="None"  ControlToValidate="txtboxFiveYearImpactFactor" ValidationGroup="Validation"
                ErrorMessage="Enter the valid be in Five-Year Impact Factor" SetFocusOnError="true"  
                ValidationExpression="^[0-9]*(?:\.[0-9]*)?$">
                </asp:RegularExpressionValidator>


            </td>
                       </tr>--%>
      <tr>
     <td> <asp:Label ID="lblCommnets" runat="server" Text="Comments:" Font-Bold="true" ></asp:Label></td> 
     <td  colspan="5">
       <asp:TextBox ID="txtboxComments" runat="server" TextWrapping="Wrap" TextMode="MultiLine" Width="970px" Height="30px"></asp:TextBox>

     </td>
                  </tr>
               
       <tr>

              <td> <asp:Label ID="lblActiveyear" runat="server" Text="Active Year" Font-Bold="true" ></asp:Label></td>

           <td colspan="5">
          <asp:TextBox ID="txtActiveyear" Text="Select Year" runat="server" CssClass="txtbox" OnTextChanged="txtActiveyear_TextChanged" AutoPostBack="true"
                    Height="18px" Width="128px"></asp:TextBox>
                <asp:Panel ID="PnlActiveyear" runat="server" CssClass="PnlDesign" Width="126px" Visible="true">
                    <asp:CheckBoxList ID="cblActiveyear" runat="server">
                        <asp:ListItem>2015</asp:ListItem>
                        <asp:ListItem>2016</asp:ListItem>
                        <asp:ListItem>2017</asp:ListItem>
<asp:ListItem>2018</asp:ListItem>
                        <asp:ListItem>2019</asp:ListItem>
                    </asp:CheckBoxList>
                </asp:Panel>
            <asp:PopupControlExtender ID="PceSelectCustomer" runat="server" TargetControlID="txtActiveyear"
                    PopupControlID="PnlActiveyear" Position="Bottom">
                </asp:PopupControlExtender>
             <asp:Label ID="Label2" runat="server" Text=""></asp:Label></td> 
           <td>

              


           </td>
       </tr>
                     </table>  
  


<br />
<br />



   <table width="100%">
   <tr align="center">
   

         <td >       
      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;           
      <asp:Button ID="btnClear" runat="server" Text="Clear" onclick="btnClear_Click" 
          Width="100px" />
      &nbsp;&nbsp;&nbsp;
      <asp:Button ID="btnSaveUpdate" runat="server" Text="Save/Update" onclick="btnSaveUpdate_Click"  Enabled="false" ValidationGroup="Validation" CausesValidation="true" />
      &nbsp;&nbsp;&nbsp;
      
      <br />


    </td>

       </tr>

  
  </table>
  <br />
    <br />
      <br />

  
             <asp:Panel ID="PanelView" runat="server" Visible="false" GroupingText=" " >
              <asp:GridView ID="GridViewIndex" runat="server" AllowPaging="True" DataSourceID="SqlDataSourceIndexView"
            AutoGenerateColumns="False" 
            CellPadding="4" PagerSettings-PageButtonCount="10" 
            PageSize="8" DataKeyNames="Id,Year"
               ForeColor="#333333" GridLines="None"> 
                      <AlternatingRowStyle BackColor="White" />
                      <Columns>
                <asp:BoundField DataField="Id" HeaderText="Id" ReadOnly="True" SortExpression="Id"/>
                <asp:BoundField DataField="Year" HeaderText="Year" SortExpression="Year"  ReadOnly="false"/>
                <asp:BoundField DataField="ImpactFactor" HeaderText="Impact Factor" SortExpression="ImpactFactor" ReadOnly="true"/>
                <asp:BoundField DataField="fiveImpFact" HeaderText="5-Year Impact Factor" SortExpression="fiveImpFact" ReadOnly="true" />
                
             
               
            </Columns>
            <FooterStyle BackColor="#990000" ForeColor="White" Font-Bold="True" />
            <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />

<PagerSettings PageButtonCount="8"></PagerSettings>

            <HeaderStyle BackColor="#0b532d" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#0b532d" ForeColor="#000000" HorizontalAlign="Center" />
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
         SelectCommand="select top 20 * from  Journal_IF_Details" >
        

     </asp:SqlDataSource>



     </asp:Panel>

          
 <asp:Panel ID="popupPanelJournal" runat="server" Visible="false" CssClass="modelPopup" Width="980px">

<table style="background:white">

<tr>
<th align="center" colspan="3"> Journal</th>
</tr>
<tr>
<td align="center"><b>Search Journal Name: </b>
<asp:TextBox ID="journalcodeSrch" runat="server" ></asp:TextBox> 
  <asp:Button ID="BtnEmployeeSearch" runat="server" Text="Search" OnClick="JournalCodeChanged" />
  
  <asp:Button ID="Button1" runat="server" Text="EXIT" OnClick="exit" />
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


<asp:SqlDataSource ID="SqlDataSourceJournal" runat="server" ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" 
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



