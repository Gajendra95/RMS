<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPageRMS.master" AutoEventWireup="true" CodeFile="ManageQuartile.aspx.cs" Inherits="ManageJournal_ManageQuartile" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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



      .auto-style16 {
          height: 36px;
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
                        <td  colspan="3" class="auto-style16" >
                        <asp:TextBox ID="txtboxTitle" runat="server"  Width="600px" TextMode ="MultiLine" ></asp:TextBox>       </td>

                           </tr>
       <tr>
            <td>
              <asp:Label ID="lblyear" runat="server" Text="Year" Font-Bold="true"></asp:Label>
           </td>
           <td>
        <%--  <asp:TextBox ID="txtyear"  runat="server" CssClass="txtbox" ></asp:TextBox>--%>
                <asp:DropDownList ID="TextBoxYearJAQ" runat="server" 
        Width="60px"  Style="border-style:inset none none inset;"  OnSelectedIndexChanged="TextBoxYearJAQ_SelectedIndexChanged" AutoPostBack="true">
                
        


        </asp:DropDownList>  
               <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" 
           SelectCommand="SELECT top 10 j.Id,j.Title,j.AbbreviatedTitle FROM [Journal_M] j,[Journal_Year_Map] m where j.Id=m.Id and m.Year=@Year" SelectCommandType="Text">
     <SelectParameters>
  <asp:ControlParameter Name="Year"  ControlID="TextBoxYearJAQ" PropertyName="SelectedValue"/>
  </SelectParameters>
                </asp:SqlDataSource>

        
           <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" Display="None"  ControlToValidate="TextBoxYearJAQ" ValidationGroup="validation"
                ErrorMessage="Entered Proper Year" SetFocusOnError="true"  
                ValidationExpression="^[0-9]{4}$">
                </asp:RegularExpressionValidator> 

           </td>
           <td>
                <asp:Label ID="lblquartileid" runat="server" Text="QuartileId" Font-Bold="true" Visible="false"></asp:Label>
           </td>
           <td>
                 <asp:TextBox ID="txtquartileID"  runat="server" CssClass="txtbox" Visible="false"  ></asp:TextBox>
           </td>
            <td>
                <asp:Label ID="lblquartile" runat="server" Text="QuartileName" Font-Bold="true"></asp:Label>
           </td>
           <td>
                <%-- <asp:TextBox ID="txtquartile"  runat="server" CssClass="txtbox" ></asp:TextBox>--%>
                <asp:DropDownList ID="DropDownQuartile" runat="server" Width="130px"   DataSourceID="SqlDataSourceQuartileM" DataTextField="Name" OnSelectedIndexChanged="DropDownQuartile_SelectedIndexChanged" DataValueField="Id"  Height="20px" AutoPostBack="true" AppendDataBoundItems="true">
                      <asp:ListItem Value="0" >-Select-</asp:ListItem>
                </asp:DropDownList>
        <asp:SqlDataSource ID="SqlDataSourceQuartileM" runat="server" ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" 
           SelectCommand="select Name,Id from Quartile_M">
 </asp:SqlDataSource> 
           </td>
       </tr>
      <%-- <tr>
           
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
             
          
       </tr>--%>
                           
                         
                          
     
               
     <%--  <tr>

              <td> <asp:Label ID="lblActiveyear" runat="server" Text="Active Year" Font-Bold="true" ></asp:Label></td>

           <td colspan="3">
          <asp:TextBox ID="txtActiveyear" Text="Select Year" runat="server" CssClass="txtbox" OnTextChanged="txtActiveyear_TextChanged" AutoPostBack="true"
                    Height="18px" Width="128px"></asp:TextBox>
                <asp:Panel ID="PnlActiveyear" runat="server" CssClass="PnlDesign" Width="126px" Visible="true">
                    <asp:CheckBoxList ID="cblActiveyear" runat="server">
                        <asp:ListItem>2015</asp:ListItem>
                        <asp:ListItem>2016</asp:ListItem>
                        <asp:ListItem>2017</asp:ListItem>
<asp:ListItem>2018</asp:ListItem>
                    </asp:CheckBoxList>
                </asp:Panel>
            <asp:PopupControlExtender ID="PceSelectCustomer" runat="server" TargetControlID="txtActiveyear"
                    PopupControlID="PnlActiveyear" Position="Bottom">
                </asp:PopupControlExtender>
             <asp:Label ID="Label2" runat="server" Text=""></asp:Label></td> 
          
       </tr>--%>
      
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
      <asp:Button ID="btnSaveUpdate" runat="server" Text="Save/Update" onclick="btnSaveUpdate_Click"  Enabled="true" ValidationGroup="Validation" CausesValidation="true" />
      &nbsp;&nbsp;&nbsp;
      
      <br />


    </td>

       </tr>

  
  </table>
  <br />
    <br />
      <br />

  
           

          
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

