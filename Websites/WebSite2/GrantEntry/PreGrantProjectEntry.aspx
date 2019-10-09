<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPageRMS.master" AutoEventWireup="true" CodeFile="PreGrantProjectEntry.aspx.cs" Inherits="GrantEntry_PreGrantProjectEntry" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" DisplayMode="BulletList" 
     HeaderText="Validation issues" ShowSummary="False" ValidationGroup="validation" />
<asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="True" DisplayMode="BulletList" 
     HeaderText="Validation issues" ShowSummary="False" ValidationGroup="validationUpload" />
  <style type="text/css">
      .gridViewHeader {
    padding: 40px 50px 4px 4px;
    border-collapse: collapse;
}
.modelBackground
{
	background-color:Gray;
	filter:alpha(opacity=70);
	opacity:0.7;
}
.modelPopup
{
	  border: 3px solid Gray;
          background-color:#EEEEE;
	      font-family:Verdana;
	      font-size:medium;
	      padding:3px;
	width:901px;
	      position:absolute;
	      overflow:scroll;
	      max-height:400px;
}

.blnkImgCSS
{
	opacity: 0;
	filter: alpha(opacity=0);
}

      .auto-style9 {
          width: 196px;
          height: 34px;
      }
      
      .auto-style19 {
          height: 26px;
      }
      #grvoverhead {
      margin-left:500px;
      margin-top:-80px
      }
      .auto-style33 {
          height: 48px;
      }


      .foot {
          text-space-collapse:collapse;
      }
      .auto-style50 {
          height: 44px;
      }
      .auto-style51 {
          height: 37px;
      }
      .auto-style58 {
          width: 109px;
          height: 37px;
      }
      .auto-style59 {
          width: 109px;
          height: 31px;
      }
      .auto-style60 {
          height: 31px;
      }
      .auto-style61 {
          height: 34px;
          width: 195px;
      }
      .auto-style62 {
          height: 45px;
      }
      .auto-style63 {
          height: 46px;
      }
      .auto-style64 {
          height: 47px;
      }
    
      .auto-style65 {
          height: 34px;
          width: 177px;
      }
      .auto-style66 {
          width: 177px;
      }
      .auto-style67 {
          width: 196px;
      }
      .auto-style71 {
          height: 37px;
          width: 118px;
      }
      .auto-style72 {
          height: 31px;
          width: 118px;
      }
      .auto-style73 {
          height: 34px;
          width: 118px;
      }
      .auto-style74 {
          width: 118px;
      }
    
      .auto-style75 {
          height: 37px;
          width: 225px;
      }
      .auto-style76 {
          height: 31px;
          width: 311px;
      }
      .auto-style77 {
          width: 311px;
      }
      .auto-style78 {
          height: 31px;
          width: 314px;
      }
      .auto-style79 {
          width: 314px;
      }
      .auto-style80 {
          height: 31px;
          width: 315px;
      }
      .auto-style81 {
          width: 315px;
      }
      .auto-style84 {
          height: 31px;
          width: 225px;
      }
      .auto-style86 {
          width: 225px;
      }
      .auto-style87 {
          height: 37px;
          width: 185px;
      }
      .auto-style88 {
          width: 185px;
      }
      .auto-style89 {
          height: 34px;
          width: 185px;
      }
    
      </style>


 <script type = "text/javascript">
     function Confirm() {
         var confirm_value = document.createElement("INPUT");
         confirm_value.type = "hidden";
         confirm_value.name = "confirm_value";
         if (confirm("Do You Want To Continue?"))
             confirm_value.value = "Yes";
         else
             confirm_value.value = "No";
         document.forms[0].appendChild(confirm_value);
     }
</script>

 <script type = "text/javascript">
     function AddConfirm() {
         var rid = document.getElementById('<%= TextBoxUTN.ClientID %>').value;
         var confirm_value2 = document.createElement("INPUT");
         confirm_value2.type = "hidden";
         confirm_value2.name = "confirm_value2";
         confirm_value2.value = "Yes";

         if (confirm("Any previously entered data Will be lost. Do you want to continue?"))
             confirm_value2.value = "Yes";
         else
             confirm_value2.value = "No";
         document.forms[0].appendChild(confirm_value2);
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
              
 <asp:Panel ID="panelSearchPub" runat="server" ForeColor="Black"  GroupingText="Existing Project Entry"    Font-Bold="true" Style="background-color:#E0EBAD" > 
 <center>
 <table  style="width: 92%">
    <tr>
     <td style="height: 36px; font-weight:normal; color:Black" >
      Project Type
      </td>
     <td style="height: 36px">             
     <asp:DropDownList ID="EntryTypesearch" runat="server" Style="border-style:inset none none inset;"
      DataSourceID="SqlDataSource4" DataTextField="TypeName" DataValueField="TypeId" AppendDataBoundItems="true">
     <asp:ListItem Value="A" Selected="true">ALL</asp:ListItem>
     </asp:DropDownList> 
      <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" 
           SelectCommand="select TypeId,TypeName from ProjectType_M" ProviderName="System.Data.SqlClient">
      </asp:SqlDataSource>
      </td>
                      
                        <td style="height: 36px; font-weight:normal; color:Black">
                           Project ID
                        </td>
                        <td style="height: 36px">
                          <asp:TextBox ID="PubIDSearch"  runat="server" Style="border-style:inset none none inset;" ></asp:TextBox>
                        </td>

                          <td style="height: 36px; font-weight:normal; color:Black">
                           Title
                        </td>

                        <td>   <asp:TextBox ID="TextBoxtiltleSearch"  runat="server" Style="border-style:inset none none inset;" width="340px"></asp:TextBox>
               </td>
                         
                        <td style="height: 36px">  
                              <asp:Button ID="ButtonSearchProject" runat="server" Text="Search" 
                                  OnClick="ButtonSearchProjectOnClick" style="height: 26px"  />
                        </td>
             
                      
       </tr>
               
                     </table>  
                     
      <asp:GridView ID="GridViewSearchGrant" runat="server"  AllowPaging="True" 
        PagerSettings-PageButtonCount="5" PageSize="5" DataSourceID="SqlDataSource1" 
        HeaderStyle-BackColor="#CC6600" HeaderStyle-ForeColor="Black" AutoGenerateColumns="false" OnPageIndexChanging="GridViewSearchGrant_PageIndexChanging" 
        PagerStyle-BackColor="#CC6600" PagerStyle-ForeColor="White" CellPadding="3"  OnRowDataBound="GridViewSearchGrant_RowDataBound" OnRowEditing="GridViewSearchGrant_OnRowedit" OnRowCommand="GridViewSearchGrant_RowCommand"
        CellSpacing="3" PagerStyle-Width="4" PagerStyle-Height="4" Visible="False" 
        BorderColor="#FF6600" BorderStyle="Solid">
        <Columns>  
   <asp:TemplateField ShowHeader="False" >
   <ItemTemplate>
   <asp:ImageButton ID="BtnEdit" runat="server" CausesValidation="False" CommandName="Edit" ImageUrl="~/Images/edit.gif" ToolTip="Edit"  />
   </ItemTemplate>
   </asp:TemplateField>

          <asp:TemplateField ShowHeader="False">
   <ItemTemplate>
   <asp:Button ID="BtnEditGridViewView" runat="server" CausesValidation="False" CommandName="View"  Text="Approval"  ToolTip="Approval" Visible="false" />
   </ItemTemplate>
   </asp:TemplateField>
     <asp:BoundField DataField="ProjectUnit" ReadOnly="true" HeaderText="Project Unit" 
                SortExpression="ProjectUnit" />

        <asp:BoundField DataField="ID" ReadOnly="true" HeaderText="ID" 
                SortExpression="ID" />

          <asp:BoundField DataField="TypeName" ReadOnly="true" HeaderText="Type" 
                SortExpression="TypeName" />
              
                  <asp:BoundField DataField="Title" ReadOnly="true" HeaderText="Title" 
                SortExpression="Title" />
           <asp:BoundField DataField="Description" ReadOnly="true" HeaderText="Description" 
                SortExpression="Description" />
                   <asp:BoundField DataField="AppliedAmount" ReadOnly="true" HeaderText="AppliedAmount" 
                SortExpression="AppliedAmount" />
                  <asp:BoundField DataField="StatusName" ReadOnly="true" HeaderText="Status" 
                SortExpression="StatusName" />
   <asp:TemplateField ShowHeader="False">
   <ItemTemplate>
   <asp:HiddenField ID="hiddenProjectType" runat="server" Value='<%# Eval("ProjectType") %>'/>
   </ItemTemplate>
   </asp:TemplateField>
                  <%--  <asp:BoundField DataField="ProjectType" ReadOnly="true" HeaderText="ProjectType" 
                SortExpression="ProjectType" />--%>

            
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
         SelectCommand=" " >
        </asp:SqlDataSource>
<br />
    <br />
 </center>
  </asp:Panel>
  <br />

  <asp:Panel ID="Panel7" runat="server" > 
<asp:Panel ID="MainpanelGrant" runat="server"  Enabled="false" GroupingText="Basic Details" Style="font-weight:bold; border-style:inset;background-color:#E0EBAD; margin-bottom: 0px;"  > 
 <table>
     <tr>
          <td class="auto-style58">
 
   <asp:Label ID="LabelGrabtID" runat="server" Text="ID" ForeColor="Black"></asp:Label>
                    
 </td>
 <td class="auto-style75">
     <asp:TextBox ID="TextBoxID" runat="server" Enabled="false" Width="200px" ></asp:TextBox>
                              
 </td>
 <td class="auto-style71">
 
   <asp:Label ID="LabelUTN" runat="server" Text="UTN" ForeColor="Black"></asp:Label>
                    
 </td>
 <td class="auto-style87">
     <asp:TextBox ID="TextBoxUTN" runat="server" Enabled="false" Width="150px" Height="16px" ></asp:TextBox>
                              
 </td>
     </tr>
 <tr>
  <td class="auto-style59">
 
   <asp:Label ID="LabelTypeGrant" runat="server" Text="Project Type" ForeColor="Black"></asp:Label>
                    
 </td>
 <td class="auto-style84">
     <asp:DropDownList ID="DropDownListTypeGrant" runat="server" Width="200px"   DataSourceID="SqlDataSourceDropDownListTypeGrant" DataTextField="TypeName" DataValueField="TypeId" ></asp:DropDownList>
        <asp:SqlDataSource ID="SqlDataSourceDropDownListTypeGrant" runat="server" ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" 
           SelectCommand="select TypeId,TypeName from ProjectType_M">
     
                </asp:SqlDataSource>                        
 </td>
 
<td class="auto-style73">
 
   <asp:Label ID="LabelGrantUnit" runat="server" Text="Project Unit" ForeColor="Black"></asp:Label>
                    
 </td>
 <td class="auto-style89">
     <asp:DropDownList ID="DropDownListGrUnit" runat="server" Width="150px" DataSourceID="SqlDataSourceDropDownListGrUnit" DataTextField="UnitName" DataValueField="UnitId" Enabled="true" Height="20px" ></asp:DropDownList>
     <asp:SqlDataSource ID="SqlDataSourceDropDownListGrUnit" runat="server" ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" 
           SelectCommand="select UnitId,UnitName from ProjectUnit_M">
     
                </asp:SqlDataSource>
                              
 </td>
 
 

  <td class="auto-style72">
 
   <asp:Label ID="LabelStatus" runat="server" Text="Project Status" ForeColor="Black"></asp:Label>
                    
 </td>
 <td class="auto-style60">
     <asp:DropDownList ID="DropDownListProjStatus" runat="server" Width="150px"   DataSourceID="SqlDataSourcePrjStatus" DataTextField="StatusName" DataValueField="StatusId"   AutoPostBack="true" Height="20px"></asp:DropDownList>
        <asp:SqlDataSource ID="SqlDataSourcePrjStatus" runat="server" ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" 
           SelectCommand="select StatusId,StatusName from Status_Project_M where StatusId='NEW'">
 </asp:SqlDataSource>                        
 </td>
   
 </tr>
     <tr>

<td>
    <asp:Label ID="LabelGrantDate" runat="server" Text="Applied Date" ForeColor="Black"></asp:Label>
    </td>
              <td class="auto-style86">
    <asp:TextBox ID="TextBoxGrantDate" runat="server" Width="200px" ></asp:TextBox></td>
    <asp:CalendarExtender ID="CalendarExtender5" runat="server"
                TargetControlID="TextBoxGrantDate" Format="dd/MM/yyyy" >
            </asp:CalendarExtender>

                <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" Display="None"  ControlToValidate="TextBoxGrantDate" ValidationGroup="validation"
                ErrorMessage="Presentation Date in (dd/mm/yyyy) format" SetFocusOnError="true"  
                ValidationExpression="^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$">
                </asp:RegularExpressionValidator>
 <%--         
  <td>
    <asp:Label ID="Label10" runat="server" Text="Actual Applied Date" ForeColor="Black"></asp:Label>
    </td>
              <td class="auto-style88">
    <asp:TextBox ID="txtprojectactualdate" runat="server" Width="150px" ></asp:TextBox></td> --%>


    
  <td class="auto-style74">
 
   <asp:Label ID="LabelGrantAmt" runat="server" Text="Applied Amount (in  INR)" ForeColor="Black"></asp:Label></td>
                    
<td> <asp:TextBox ID="TextBoxGrantAmt" runat="server" Width="150px" style="margin-left: 0px" ></asp:TextBox></td>
    
            <%--<td >
 
   <asp:Label ID="Labelerfrelated" runat="server" Text="ERF Related?" ForeColor="Black"></asp:Label>
                    
 </td>--%>
 <%-- <td >
     <asp:DropDownList ID="DropDownListerfRelated" runat="server"   Width="50px"  >
     <asp:ListItem Value="N">No</asp:ListItem>
          <asp:ListItem Value="Y">Yes</asp:ListItem>
     </asp:DropDownList>
      <asp:Label ID="Label2" runat="server" Text="( Environmental Research Fund )" ForeColor="Black"></asp:Label>

</td>  --%>                   
 
   <%-- 
         <td class="auto-style62">
      <asp:Label ID="Label10" runat="server" Text="Current Status" ForeColor="Black"></asp:Label>

         </td>--%>
        <%-- <td class="auto-style62">
     <asp:TextBox ID="TextBoxCurStatus" runat="server"  Enabled="false" ></asp:TextBox>

         </td>--%>
     


  <td>
         <asp:Label ID="Label2" runat="server" Text="Contact Number">
           
         </asp:Label>
          </td>
              <td class="auto-style86"><asp:TextBox ID="txtcontact" runat="server" Width="200px"></asp:TextBox>
     </td>
     </tr>
     <tr>
 
      <td class="auto-style65">
 
   <asp:Label ID="LabelSrcGrnat" runat="server" Text="Project Source" ForeColor="Black"></asp:Label>
                    
 </td>
 <td class="auto-style89">
     <asp:DropDownList ID="DropDownListSourceGrant" runat="server" DataSourceID="SqlDataSourceDropDownListSourceGrant" AppendDataBoundItems="true" DataTextField="SourceName" DataValueField="SourceId" Enabled="true" Height="20px" Width="150px" >
             <asp:ListItem Value="">--Select--</asp:ListItem>
     </asp:DropDownList>
            <asp:SqlDataSource ID="SqlDataSourceDropDownListSourceGrant" runat="server" ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" 
           SelectCommand="select SourceId,SourceName from ProjectSource_M">
     
                </asp:SqlDataSource>
                        
 </td>
         <td class="auto-style65">   <asp:Label ID="Label14" runat="server" Text="Duration Of The Project(in months)" ForeColor="Black"></asp:Label>
</td>
         <td class="auto-style102"><asp:TextBox ID="txtProjectDuration" runat="server" MaxLength="2" Width="150px"></asp:TextBox></td>
       
    
         <td class="auto-style66">
   <asp:Label ID="Labelerfrelated" runat="server" Text="ERF Related?(Environmental Research Fund)" ForeColor="Black"></asp:Label>

         </td>
          <td class="auto-style67" >
     <asp:DropDownList ID="DropDownListerfRelated" runat="server"   Width="150px" Height="20px"  >
     <asp:ListItem Value="N">No</asp:ListItem>
          <asp:ListItem Value="Y">Yes</asp:ListItem>
     </asp:DropDownList>
     <%-- <asp:Label ID="Label9" runat="server" Text="( Environmental Research Fund )" ForeColor="Black"></asp:Label>--%>

</td>
 </tr>
          
     <tr>
         <td class="auto-style63">
         <asp:Label ID="Label7" runat="server" Text="Project Title" ForeColor="Black"></asp:Label>
                    
 </td>
 <td colspan="4" class="auto-style63">
     <asp:TextBox ID="TextBoxTitle" runat="server" Width="628px" Height="31px" Enabled="true"  ></asp:TextBox>
</td>
</tr>
     <tr>
         <td class="auto-style62">
 
   <asp:Label ID="LabelDescription" runat="server" Text="Description" ForeColor="Black"></asp:Label>
                    
 </td>
 <td colspan="4" class="auto-style62">
     <asp:TextBox ID="TextBoxDescription" runat="server" TextMode="MultiLine" Width="628px"></asp:TextBox>
                              
 </td>
     </tr>
      <tr>
         <td class="auto-style64">
 
   <asp:Label ID="Label1" runat="server" Text="Agency Comment" ForeColor="Black"></asp:Label>
                    
 </td>
 <td colspan="4" class="auto-style64">
     <asp:TextBox ID="TextBoxAdComments" runat="server" TextMode="MultiLine" Width="628px"></asp:TextBox>                              
 </td>
     </tr>

      <tr>

          <td class="style74" >

    <asp:Label ID="lblRevisedAppliedAmt" runat="server" Text="Revised Applied Amount" ForeColor="Black" Visible="false" ></asp:Label>
</td>
<td> <asp:TextBox ID="txtRevisedAppliedAmt" runat="server" Width="200px" style="margin-left: 0px"  Visible="false"></asp:TextBox> </td>
<td  class="style74"> 
     <asp:Label ID="lblsanctionorderdate" runat="server" Text="Sanction Ordered Date" ForeColor="Black" Visible="false" ></asp:Label>
</td>
         <td>
<asp:TextBox ID="Textsanctionorderdate" runat="server" Enabled="true" Width="150px" Height="16px" AutoPostBack="true" Visible="false" ></asp:TextBox>
              <asp:CalendarExtender ID="CalendarExtender8" runat="server"
                TargetControlID="Textsanctionorderdate" Format="dd/MM/yyyy" >
            </asp:CalendarExtender>

                <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" Display="None"  ControlToValidate="Textsanctionorderdate" ValidationGroup="validation"
                ErrorMessage="Sanction Order Date in (dd/mm/yyyy) format" SetFocusOnError="true"  
                ValidationExpression="^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$">
                </asp:RegularExpressionValidator>

             <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Please enter Sanction Order Date" Display="None" ControlToValidate="Textsanctionorderdate" ValidationGroup="validation" Enabled="false"></asp:RequiredFieldValidator>
         </td>
     </tr>
 </table>
 
 <asp:Panel ID="Panel10" runat="server" Style="font-weight:bold; background-color:#E0EBAD; margin-top: 9px;" GroupingText="Agency Details" Enabled="false">

          <table style="width: 1050px">
              <tr>
               
                   <td class="auto-style81">

   <asp:Label ID="Label3" runat="server" Text="Project Agency" ForeColor="Black"></asp:Label>
                    
 </td>
<%--  <td>

   <asp:Label ID="LabelGrnatAgency" runat="server" Text="Project Agency" ForeColor="Black"></asp:Label>
                    
     
                    
 </td>--%>
     <td class="auto-style76">
     <asp:TextBox ID="txtagency" runat="server"  AutoPostBack="true" ></asp:TextBox>


      <asp:ImageButton ID="imageBkCbtn" runat="server" ImageUrl="~/Images/srchImg.gif" OnClientClick="setRow(this)"  /></td>

                 <asp:ModalPopupExtender ID="model" runat="server" TargetControlID="imageBkCbtn" PopupControlID="popupselectNo"></asp:ModalPopupExtender>
     <%--<asp:DropDownList ID="TextBoxGrantAgency" runat="server" Width="230px"  DataSourceID="SqlDataSourceTextBoxGrantAgency" DataTextField="FundingAgencyName" DataValueField="FundingAgencyId" style="margin-top: 6px" Enabled="false"  ></asp:DropDownList>--%>
                  
    
                  <td class="auto-style76">
         <asp:Label ID="Label5" runat="server" Text="Contact No">
           
         </asp:Label>
          </td>
              <td class="auto-style76"><asp:TextBox ID="txtagencycontact" runat="server" MaxLength="12"></asp:TextBox>
     </td>
                  
                     <td class="auto-style76">
             <asp:Label ID="Label8" runat="server" Text="PAN No"></asp:Label>
         </td>
             <td class="auto-style79">
         <asp:TextBox ID="txtpan" runat="server" style="margin-top: 10px; margin-right: 15px;" MaxLength="20"></asp:TextBox>
         </td>
                         <td class="auto-style77">
             <asp:Label ID="Label4" runat="server" Text="Email Id"></asp:Label>
         </td>
             <td class="auto-style78">
         <asp:TextBox ID="txtEmailId" runat="server" style="margin-top: 10px" MaxLength="30"></asp:TextBox>
         </td>    
           
              </tr>
              <tr>
                       <td class="auto-style81">
             <asp:Label ID="Label6" runat="server" Text="Address"></asp:Label>
         </td>
                   <td class="auto-style76">
         <asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine" MaxLength="100"></asp:TextBox>
         </td>
                  <td class="auto-style80">
                      State:
                  </td>
                  <td>
                      <asp:TextBox ID="txtstate" runat="server" MaxLength="30"></asp:TextBox>                 
                  </td>

                  <td>
                      Country:
                      </td>
                      <td>
                      <asp:TextBox ID="txtcountry" runat="server" MaxLength="30"></asp:TextBox>                 
                  </td>
                <td>
                      
                      </td>
                      <td>
                      <asp:HiddenField runat="server" ID="hdnAgencyId" ></asp:HiddenField>                 
                  </td>
              </tr>
               <tr>
                  <td>
 Sector Level:
                      </td>
                      <td>
                      <asp:DropDownList ID="DropDownSectorLevel" runat="server" DataSourceID="SqlDataSourceSectorLevel" AppendDataBoundItems="true"   AutoPostBack="true"  DataTextField="SectorLevelName" DataValueField="Id" Enabled="true" Height="20px"  Width="150px">
         <asp:ListItem Value="0">--Select--</asp:ListItem>
     </asp:DropDownList>
     <asp:SqlDataSource ID="SqlDataSourceSectorLevel" runat="server" ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" 
           SelectCommand="select Id,SectorLevelName from ProjectFundingSectorLevel_M"></asp:SqlDataSource>                 
                  </td>
                   <td>
 Agency type:
                      </td>
                      <td>
                      <asp:DropDownList ID="DropDownAgencyType" runat="server" DataSourceID="SqlDataSourceAgencyType" AppendDataBoundItems="true"  AutoPostBack="true"  DataTextField="TypeName" DataValueField="Id" Enabled="true" Height="20px"  Width="150px">
         <asp:ListItem Value="0">--Select--</asp:ListItem>
     </asp:DropDownList>
     <asp:SqlDataSource ID="SqlDataSourceAgencyType" runat="server" ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" 
           SelectCommand="select Id,TypeName from ProjectTypeofAgency_M"></asp:SqlDataSource>                 
                  </td>
              </tr>
          </table>        
      </asp:Panel>

      <br />
    <table>
  <tr>
   <td class="auto-style19" >
  <asp:Label ID="LabelSanType" runat="server" Text="Sanction-Type" ForeColor="Black" Visible="false"></asp:Label>
 
       &nbsp;&nbsp;&nbsp;
 
 </td>
 <td class="auto-style19">
 <asp:DropDownList ID="DropDownListSanType" runat="server"  Width="90px" DataSourceID="SqlDataSourceDropDownListSanType" Visible="false" DataTextField="SanctionTypeName" DataValueField="SanctionTypeId"   AutoPostBack="true"></asp:DropDownList>
 <asp:SqlDataSource ID="SqlDataSourceDropDownListSanType" runat="server" ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" 
  SelectCommand="select SanctionTypeId,SanctionTypeName from SanctionTypeM">
 </asp:SqlDataSource> </td></tr>
 <tr>
  <td class="auto-style50">
   <asp:Label ID="kindStartdate" Visible="false" runat="server" Text="Project Start Date" ForeColor="Black"></asp:Label>
                    
 </td>
 <td class="auto-style50">
     <asp:TextBox ID="TextKindStartDate" Visible="false" runat="server" Width="120px" ></asp:TextBox>
     
                 <asp:RegularExpressionValidator ID="RegularExpressionValidatorKind1" runat="server" Display="None"  ControlToValidate="TextKindStartDate" ValidationGroup="validation"
                ErrorMessage="Project Start Date in (dd/mm/yyyy) format" SetFocusOnError="true"  
                ValidationExpression="^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$">
                </asp:RegularExpressionValidator>        
                     <asp:CalendarExtender ID="CalendarExtender2" runat="server"
                TargetControlID="TextKindStartDate" Format="dd/MM/yyyy" >
            </asp:CalendarExtender>                 
 </td>

    <td class="auto-style50">
 
   <asp:Label ID="KindClosedate" runat="server" Visible="false" Text="Project close Date" ForeColor="Black"></asp:Label>
                    
 </td>
 <td class="auto-style50">
     <asp:TextBox ID="TextKindclosedate" runat="server" Visible="false"  Width="120px"  ></asp:TextBox>
     
                 <asp:RegularExpressionValidator ID="RegularExpressionValidatorKind2" runat="server" Display="None"  ControlToValidate="TextKindclosedate" ValidationGroup="validation"
                ErrorMessage="Project Close Date in (dd/mm/yyyy) format" SetFocusOnError="true"  
                ValidationExpression="^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$">
                </asp:RegularExpressionValidator>  
        <asp:CalendarExtender ID="CalendarExtender7" runat="server"
                TargetControlID="TextKindclosedate" Format="dd/MM/yyyy" >
            </asp:CalendarExtender>    
                              
 </td>
<td class="auto-style19">
  <asp:Label ID="LabelkindDetails" runat="server" Text="Kind Details" ForeColor="Black" Visible="false"></asp:Label>
 </td>
 <td class="auto-style19">
 <asp:TextBox ID="TextBoxKindDetails" runat="server"  ForeColor="Black" Visible="false" Width="400px"></asp:TextBox>
 </td>
</tr>
</table>
     </asp:Panel>
 <br />
      
    <asp:Panel ID="panAddAuthor" Enabled="false" runat="server" GroupingText="Investigator Details"  ForeColor="Black"  Style="font-weight:bold; border-style:inset;background-color:#E0EBAD"  > 

              <%--       <center>
          Author Details
                     </center>--%>
                     <br />
                       
  <br />
         <asp:SqlDataSource ID="SqlDataSourceAuthorType" runat="server" 
                ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" 
                ProviderName="System.Data.SqlClient"> 
                                     
            </asp:SqlDataSource>
<asp:Panel ID="PanelMU" runat="server" ScrollBars="both"  Width="1000px" >
  
     <asp:GridView ID="Grid_AuthorEntry" runat="server" AutoGenerateColumns="False"  GridLines="None"
    CellPadding="4" ForeColor="#333333" OnRowDataBound="OnRowDataBound1"  >

     <AlternatingRowStyle BackColor="White" />
     <Columns>

        <asp:TemplateField HeaderText="MAHE/Non MAHE">
             <ItemTemplate>
                 <asp:DropDownList ID="DropdownMuNonMu" runat="server" Width="150"  AutoPostBack="true" DataSourceID="SqlDataSourceAuthorType" >
                     
                <%-- <asp:ListItem Value="M">MAHE-Staff</asp:ListItem>
                               <asp:ListItem Value="S">MAHE-Student</asp:ListItem>
                        <asp:ListItem Value="N">Non MAHE</asp:ListItem>--%>
                 </asp:DropDownList>
                 
                 <asp:ImageButton ID="MuNonMu" runat="server" ImageUrl="~/Images/srchImg.gif" CssClass="blnkImgCSS"  />              

             </ItemTemplate>              
        </asp:TemplateField>

        <asp:TemplateField ShowHeader="true" HeaderText="Roll No/Employee Code">
             <ItemTemplate>
                 <asp:TextBox ID="EmployeeCode" runat="server" Width="90" Visible="true" MaxLength="9"></asp:TextBox>
        <asp:Image ID="ImageEmpCode" runat="server" ImageUrl="~/Images/srchImg.gif" CssClass="blnkImgCSS" />
            </ItemTemplate>            
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Investigator Name">
             <ItemTemplate>
                 <asp:TextBox ID="AuthorName" runat="server" Width="200" Enabled="false" AutoPostBack="true"></asp:TextBox>
          
                 <asp:ImageButton ID="EmployeeCodeBtn" runat="server" ImageUrl="~/Images/srchImg.gif" OnClientClick="setRow(this)" />

                 <asp:ModalPopupExtender ID="ModalPopupExtender4" runat="server" TargetControlID="EmployeeCodeBtn" PopupControlID="popupPanelAffil"
                    BackgroundCssClass="modelBackground"  >
                                  </asp:ModalPopupExtender>
                 
                 <asp:ImageButton ID="ImageButton1" runat="server" Visible="false" ImageUrl="~/Images/srchImg.gif" OnClientClick="setRowPop(this)" />
                
                  <asp:ModalPopupExtender ID="ModalPopupExtender2" runat="server" TargetControlID="ImageButton1" PopupControlID="popup"
                    BackgroundCssClass="modelBackground"  >
                                  </asp:ModalPopupExtender>
                                  <asp:RequiredFieldValidator ID="RequiredFieldValidator23" Display="None" 
                 ControlToValidate="AuthorName" ValidationGroup="validation2"
                 runat="server" 
                 ErrorMessage="Please enter Author Name" /> 
             </ItemTemplate>              
        </asp:TemplateField>
          <asp:TemplateField HeaderText="InvestigatorType">
       <ItemTemplate>
      <asp:DropDownList ID="AuthorType" runat="server" Width="170" >
        <asp:ListItem Value="P">Principal Investigator</asp:ListItem>
          <asp:ListItem Value="C">CO-Investigator</asp:ListItem>
        </asp:DropDownList>
  <asp:Image ID="ImageisAuthorType" runat="server" ImageUrl="~/Images/srchImg.gif" CssClass="blnkImgCSS" />
 </ItemTemplate>              
  </asp:TemplateField>

           <asp:TemplateField HeaderText="isLeadPI">
             <ItemTemplate>
                 <asp:DropDownList ID="isLeadPI" runat="server" Width="75"  >
                 <asp:ListItem Value="Y">Yes</asp:ListItem>
                        <asp:ListItem Value="N" Selected="True">No</asp:ListItem>
                 </asp:DropDownList>

                 <asp:Image ID="ImageisLeadPI" runat="server" ImageUrl="~/Images/srchImg.gif" CssClass="blnkImgCSS" />

                   </ItemTemplate>              
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Institution Name">
         <ItemTemplate>
        <asp:TextBox ID="InstitutionName" runat="server" Width="200" Enabled="false"></asp:TextBox> 
        <asp:DropDownList ID="DropdownStudentInstitutionName" runat="server" Width="200" Visible="false" AutoPostBack="true" DataSourceID="SqlDataSourceDropdownStudentInstitutionName" DataTextField="Institute_Name"  DataValueField="Institute_Id" >
  </asp:DropDownList>
     <asp:SqlDataSource ID="SqlDataSourceDropdownStudentInstitutionName" runat="server" 
                                         ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" 
                                         ProviderName="System.Data.SqlClient" 
                                         SelectCommand="select Institute_Id,Institute_Name from Institute_M">
                                     </asp:SqlDataSource>
             
             <asp:Image ID="Image4" runat="server" ImageUrl="~/Images/srchImg.gif" CssClass="blnkImgCSS" />           
             </ItemTemplate>              
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Department Name">
             <ItemTemplate>
                 <asp:TextBox ID="DepartmentName" runat="server" Width="200" Enabled="false"></asp:TextBox>
                     <asp:DropDownList ID="DropdownStudentDepartmentName" runat="server" Width="200" Visible="false"  DataSourceID="SqlDataSourceDropdownStudentDepartmentName" DataTextField="DeptName" DataValueField="DeptId" >
              </asp:DropDownList>

                      <asp:SqlDataSource ID="SqlDataSourceDropdownStudentDepartmentName" runat="server" 
                                         ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" 
                                         ProviderName="System.Data.SqlClient" 
                                         SelectCommand="select DeptId,DeptName from Dept_M where Institute_Id=@Institute_Id ">
  <SelectParameters>
  
     <asp:ControlParameter Name="Institute_Id" 
      ControlID="DropdownStudentInstitutionName"
      PropertyName="SelectedValue"/>
  </SelectParameters>
    </asp:SqlDataSource>

     <asp:Image ID="Image5" runat="server" ImageUrl="~/Images/srchImg.gif" CssClass="blnkImgCSS" />
            </ItemTemplate>            
        </asp:TemplateField>

           <asp:TemplateField HeaderText="MailId">
             <ItemTemplate>
                 <asp:TextBox ID="MailId" runat="server" Width="200"  Enabled="false" ></asp:TextBox>
       <asp:Image ID="ImageMailId" runat="server" ImageUrl="~/Images/srchImg.gif" CssClass="blnkImgCSS" />
                  </ItemTemplate>              
        </asp:TemplateField>

  
  <asp:TemplateField  HeaderText="Colloboration">
     <ItemTemplate>
    <asp:DropDownList ID="NationalType" runat="server" Width="140"  AutoPostBack="true"  >
     <asp:ListItem Value="N">National</asp:ListItem>
    <asp:ListItem Value="I">International</asp:ListItem>
   </asp:DropDownList>
 <asp:Image ID="ImageisNationalType" runat="server" ImageUrl="~/Images/srchImg.gif" CssClass="blnkImgCSS" />
  </ItemTemplate>              
        </asp:TemplateField>
            <asp:TemplateField  HeaderText="Continent">
             <ItemTemplate>
   <asp:DropDownList ID="ContinentId" runat="server" Width="140"   DataSourceID="SqlDataSourceDropdownContinentId" DataTextField="ContinentName" DataValueField="ContinentId" >
  </asp:DropDownList>
   <asp:SqlDataSource ID="SqlDataSourceDropdownContinentId" runat="server" 
                                         ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" 
                                         ProviderName="System.Data.SqlClient" 
                                         SelectCommand="select ContinentId,ContinentName  from Continent_M ">
 </asp:SqlDataSource>
  <asp:Image ID="ImageisContinent" runat="server" ImageUrl="~/Images/srchImg.gif" CssClass="blnkImgCSS" />
  </ItemTemplate>              
  </asp:TemplateField>
  <asp:TemplateField ShowHeader="false">
    <ItemTemplate>
    <asp:HiddenField ID="Institution" runat="server" ></asp:HiddenField>               
   <asp:ImageButton ID="InstitutionBtn" runat="server"  ImageUrl="~/Images/srchImg.gif" CssClass="blnkImgCSS" />
  </ItemTemplate>              
        </asp:TemplateField>
             <asp:TemplateField ShowHeader="false">
             <ItemTemplate>
         <asp:HiddenField ID="Department" runat="server" ></asp:HiddenField>
 <asp:ImageButton ID="DepartmentBtn" runat="server" Enabled="false" ImageUrl="~/Images/srchImg.gif"  CssClass="blnkImgCSS"  />
 </ItemTemplate>            
        </asp:TemplateField>
  </Columns>
        <HeaderStyle BackColor="#0B532D" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#000000" ForeColor="#000000" HorizontalAlign="Center" />
        <RowStyle BackColor="#FFFFFF" ForeColor="#333333" />
        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
        <SortedAscendingCellStyle BackColor="#FDF5AC" />
        <SortedAscendingHeaderStyle BackColor="#4D0000" />
        <SortedDescendingCellStyle BackColor="#FCF6C0" />
        <SortedDescendingHeaderStyle BackColor="#820000" />
    </asp:GridView>
</asp:Panel>
</asp:Panel>   
<br />

<div style="margin-left:0px" runat="server" id="div1">
<asp:Panel ID="GrantSanction" runat="server" Enabled="false" GroupingText="Sanction - Details" Visible="true" Style="font-weight:bold; border-style:inset;background-color:#E0EBAD"  > 
<table style="width: 100%">

 <tr>
  <td class="auto-style50">
 
   <asp:Label ID="LabelProjectCommencementdate" runat="server" Text="Project Commencement Date" ForeColor="Black"></asp:Label>
                    
 </td>
 <td class="auto-style50">
     <asp:TextBox ID="TextBoxProjectCommencementDate" runat="server" Width="120px" ></asp:TextBox>
     
                 <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" Display="None"  ControlToValidate="TextBoxProjectCommencementDate" ValidationGroup="validation"
                ErrorMessage="Project Commencement Date in (dd/mm/yyyy) format" SetFocusOnError="true"  
                ValidationExpression="^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$">
                </asp:RegularExpressionValidator>        
                     <asp:CalendarExtender ID="CalendarExtender1" runat="server"
                TargetControlID="TextBoxProjectCommencementDate" Format="dd/MM/yyyy" >
            </asp:CalendarExtender>                 
 </td>

    <td class="auto-style50">
 
   <asp:Label ID="LabelProjectclosedate" runat="server" Text="Project close Date" ForeColor="Black"></asp:Label>
                    
 </td>
 <td class="auto-style50">
     <asp:TextBox ID="TextBoxProjectCloseDate" runat="server"   Width="120px"  ></asp:TextBox>
     
<%--                 <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" Display="None"  ControlToValidate="TextBoxProjectCloseDate" ValidationGroup="validation"
                ErrorMessage="Project Close Date in (dd/mm/yyyy) format" SetFocusOnError="true"  
                ValidationExpression="^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$">
                </asp:RegularExpressionValidator>--%>  
        <asp:CalendarExtender ID="CalendarExtender3" runat="server"
                TargetControlID="TextBoxProjectCloseDate" Format="dd/MM/yyyy" >
            </asp:CalendarExtender>    
                              
 </td>
  <td class="auto-style50">
 
   <asp:Label ID="LabelExtendedDate" runat="server" Text="Extended date" ForeColor="Black"></asp:Label>
                    
 </td>
 <td class="auto-style50">
     <asp:TextBox ID="TextBoxExtendedDate" runat="server"   Width="120px"  ></asp:TextBox>
     
                <%-- <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" Display="None"  ControlToValidate="TextBoxExtendedDate" ValidationGroup="validation"
                ErrorMessage="Project Extended Date in (dd/mm/yyyy) format" SetFocusOnError="true"  
                ValidationExpression="^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$">
                </asp:RegularExpressionValidator>--%>  
             <asp:CalendarExtender ID="CalendarExtender4" runat="server"
                TargetControlID="TextBoxExtendedDate" Format="dd/MM/yyyy" >
            </asp:CalendarExtender>    
                                                   
 </td>

 </tr>
           <tr>
                   <td class="auto-style51">
 
   <asp:Label ID="LabelSanctionedamountCapital" runat="server" Text="Amount-Capital" ForeColor="Black"></asp:Label>
                    
 </td>
 <td class="auto-style51">
     <asp:TextBox ID="TextBoxSanctionedAmountCapital" runat="server"  Width="120px" AutoPostBack="true"  ></asp:TextBox>
                              
 </td>
       <td class="auto-style51">
 
   <asp:Label ID="LabelSanctionedAmountOperating" runat="server" Text="Amount-Operating" ForeColor="Black" ></asp:Label>
                    
 </td>
 <td class="auto-style51">
     <asp:TextBox ID="TextBoxSanctionedAmountOperating" runat="server"  Width="120px" AutoPostBack="true"  ></asp:TextBox>
                              
 </td>
                <td class="auto-style50">
 
   <asp:Label ID="LabelSanctionedamountTotal" runat="server" Text="Sanction-Amount" ForeColor="Black"></asp:Label>
                    
 </td>
 <td class="auto-style50">
     <asp:TextBox ID="TextBoxSanctionedamountTotal" runat="server" Width="120px" ></asp:TextBox>
                              
 </td>
           </tr>
           
              <tr>
                 
                  <td class="auto-style33">Audit Requirment</td>

                  <td class="auto-style33">
                      <asp:DropDownList ID="ddlauditrequired" runat="server" Width="120px">
                          <asp:ListItem Text="Select" value="0" ></asp:ListItem>
                          <asp:ListItem Value="Y">Yes</asp:ListItem>
                          <asp:ListItem Value="N">No</asp:ListItem>
                          </asp:DropDownList></td>
                  
                  <td class="auto-style19">Institutional Share</td>
          <td class="auto-style19">  <asp:TextBox ID="txtInstitutionshare" runat="server" Width="120px" ></asp:TextBox></td>
                   <td class="auto-style51">Account Head:</td>
          <td class="auto-style51">  <asp:TextBox ID="txtaccounthead" runat="server" Width="120px"></asp:TextBox>
                 </td>
              </tr>
              <tr>
              <td class="auto-style51">
 
   <asp:Label ID="Label9" runat="server" Text="NoOfSanction" ForeColor="Black" ></asp:Label>
                    
 </td>
 <td class="auto-style51">
     <asp:TextBox ID="txtNoOFSanctions" runat="server"  Width="120px" AutoPostBack="true"  ></asp:TextBox>
                              
 </td>
                   <td class="auto-style56">Service Tax Applicable</td>
          <td class="auto-style57" >  <asp:DropDownList ID="DropDownList2" runat="server" Width="100px">
                          <asp:ListItem Text="select"></asp:ListItem>
                           <asp:ListItem Value="Y">Yes</asp:ListItem>
                            <asp:ListItem Value="N">No</asp:ListItem>
                          </asp:DropDownList></td>
              </tr>
 </table>
     </asp:Panel>

     <asp:Panel ID="Panel2" runat="server"  Enabled="false" GroupingText="Sanction-Amount-Details" Style="font-weight:bold; border-style:inset;background-color:#E0EBAD" Visible="true">
           <table width="92%">                  
          <tr>
            <td>
            <asp:Button ID="addsanction" runat="server" Text="Add" Enabled="false" />                             
              </td>
            </tr>
            </table>
            <div style="margin-left:50px">
     <asp:GridView ID="GridViewSanction" runat="server" AutoGenerateColumns="False"   
   CellPadding="2" ForeColor="#333333" >

     <AlternatingRowStyle BackColor="White" />

     <Columns>     
            <asp:TemplateField HeaderText="Sanction Number">
         <ItemTemplate>
             <asp:TextBox ID="txtsanctionNo" runat="server" Width="150px"  ></asp:TextBox>     
             </ItemTemplate>              
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Sanction date">
             <ItemTemplate>
                 <asp:TextBox ID="txtSanctiondate" runat="server"  Enabled="true" Width="150px" ></asp:TextBox>
           <asp:CalendarExtender ID="CalendarExtender1" runat="server"
    TargetControlID="txtSanctiondate" Format="dd/MM/yyyy" >
  </asp:CalendarExtender>    
<%-- <asp:Image ID="ImageRecevieddate" runat="server" ImageUrl="~/Images/srchImg.gif" CssClass="blnkImgCSS" />--%>
<asp:RegularExpressionValidator ID="RegularExpressionValidator15" runat="server" Display="None"  ControlToValidate="txtSanctiondate" ValidationGroup="validation"
                ErrorMessage="Sanction date in (dd/mm/yyyy) format" SetFocusOnError="true"  
                ValidationExpression="^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$">
                </asp:RegularExpressionValidator>   
               
             </ItemTemplate>              
        </asp:TemplateField>
       
        

        <asp:TemplateField HeaderText="Capital Amount" >
             <ItemTemplate>
                 <asp:TextBox ID="txtsancapitalAmount" runat="server"  Enabled="true" Width="150px" onchange="SanctionCapitalAmtChange(this,GridId,'txtsantotalAmount');"></asp:TextBox>                
                 
                                  
            </ItemTemplate>            
        </asp:TemplateField>
         
           <asp:TemplateField HeaderText="Operating Amount">
             <ItemTemplate>
                 <asp:TextBox ID="txtSanOpeAmt" runat="server" Width="150px"  ></asp:TextBox>
          
             </ItemTemplate>              
        </asp:TemplateField>
          <asp:TemplateField HeaderText="Total Amount">
             <ItemTemplate>
                     <asp:TextBox ID="txtsantotalAmount" runat="server"  Enabled="true" Width="150px" ></asp:TextBox>         
              </ItemTemplate>              
        </asp:TemplateField>
     <asp:TemplateField HeaderText = "Narration">
<ItemTemplate>
<asp:TextBox ID="txtNarration" Width="255px" runat="server" />
</ItemTemplate>
</asp:TemplateField>

        </Columns>
        <HeaderStyle BackColor="#0B532D" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#000000" ForeColor="#000000" HorizontalAlign="Center" />
        <RowStyle BackColor="#FFFFFF" ForeColor="#333333" />
        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
        <SortedAscendingCellStyle BackColor="#FDF5AC" />
        <SortedAscendingHeaderStyle BackColor="#4D0000" />
        <SortedDescendingCellStyle BackColor="#FCF6C0" />
        <SortedDescendingHeaderStyle BackColor="#820000" />
    </asp:GridView>
    </div>

      </asp:Panel>
     </div>

      <br />
     <asp:Panel ID="PanelKindetails" runat="server" Enabled="false" GroupingText="Details of Recieved Items" Visible="false" Style="font-weight:bold; border-style:inset;background-color:#E0EBAD">
      
          <div style="margin-left:50px">
<asp:GridView ID="GridViewkindDetails" runat="server" AutoGenerateColumns="False"  GridLines="None"
    CellPadding="4" ForeColor="#333333" >
 <AlternatingRowStyle BackColor="White" />
 <Columns>
<asp:TemplateField HeaderText="Recieved Date">
  <ItemTemplate>
  <asp:TextBox ID="ReceivedDate" runat="server" Width="200"   ></asp:TextBox>
  <asp:CalendarExtender ID="CalendarExtender1" runat="server"
    TargetControlID="ReceivedDate" Format="dd/MM/yyyy" >
  </asp:CalendarExtender>    
 <asp:Image ID="ImageReceivedDate" runat="server" ImageUrl="~/Images/srchImg.gif" CssClass="blnkImgCSS" />
 <asp:RegularExpressionValidator ID="RegularExpressionValidator15" runat="server" Display="None"  ControlToValidate="ReceivedDate" ValidationGroup="validation"
                ErrorMessage="Received date in (dd/mm/yyyy) format" SetFocusOnError="true"  
                ValidationExpression="^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$">
                </asp:RegularExpressionValidator>   
 </ItemTemplate>              
 </asp:TemplateField>
 <asp:TemplateField HeaderText="INR Equivalent">
         <ItemTemplate>
 <asp:TextBox ID="INREquivalent" runat="server" ></asp:TextBox>               
                          
  <asp:ImageButton ID="INREquivalentBtn" runat="server"  ImageUrl="~/Images/srchImg.gif" CssClass="blnkImgCSS" />
  </ItemTemplate>              
  </asp:TemplateField>
  <asp:TemplateField HeaderText="Details">
  <ItemTemplate>
  <asp:TextBox ID="Details" runat="server" Width="400"></asp:TextBox>
   <asp:ImageButton ID="DetailsBtn" runat="server" Enabled="false" ImageUrl="~/Images/srchImg.gif"  CssClass="blnkImgCSS"  />
 </ItemTemplate>            
   </asp:TemplateField>
 
 </Columns>
   <HeaderStyle BackColor="#0B532D" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#000000" ForeColor="#000000" HorizontalAlign="Center" />
        <RowStyle BackColor="#FFFFFF" ForeColor="#333333" />
        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
        <SortedAscendingCellStyle BackColor="#FDF5AC" />
        <SortedAscendingHeaderStyle BackColor="#4D0000" />
        <SortedDescendingCellStyle BackColor="#FCF6C0" />
        <SortedDescendingHeaderStyle BackColor="#820000" />
    </asp:GridView>
   </div>
 </asp:Panel>

  <asp:Panel ID="PanelUploaddetails" runat="server" GroupingText="Documents Upload"   ForeColor="Black" Style="font-weight:bold; border-style:inset;background-color:#E0EBAD"   Visible="false"  >
        <%--  <div style="margin-left:450px; margin-top:20px; position:absolute ">
               <asp:Label ID="Labelremarks" runat="server" ForeColor="Black"
                 Text="Remarks"> </asp:Label>
        
          </div>--%>
     <table style="height: 28px; width: 90%;">
     
     <tr>
     
         <td class="auto-style84">
             <asp:Label ID="LabelTypeInfo" runat="server" ForeColor="Black" 
                 Text="Information Type"> </asp:Label>
         </td>
         <td class="auto-style86">
             <asp:DropDownList ID="DropDownListInfoType" runat="server" 
                 DataSourceID="SqlDataSourceInfoType" DataTextField="InfoTypeName" 
                 DataValueField="InfoTypeId" AppendDataBoundItems="true" Height="16px" Width="146px">
                 <asp:ListItem Value=" ">--Select--</asp:ListItem>
             </asp:DropDownList>
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                    ControlToValidate="DropDownListInfoType" Display="None" 
                                    ErrorMessage="Please select Information Type for upload" InitialValue=" " 
                                    ValidationGroup="validationUpload"></asp:RequiredFieldValidator>
             <asp:SqlDataSource ID="SqlDataSourceInfoType" runat="server" 
                 ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" 
                 SelectCommand="select InfoTypeId,InfoTypeName from Project_AuxInfoTypeM where Role=@Role">
                 <SelectParameters>
                 <asp:SessionParameter Name="Role" SessionField="Role"/>
                 </SelectParameters>
             </asp:SqlDataSource>
         </td>
        <td class="auto-style91"> Audit From</td>
        <td class="auto-style87"><asp:TextBox ID="AuditFrom" runat="server" > </asp:TextBox></td> 
        <td class="auto-style91"> Audit To</td>
        <td class="auto-style91"><asp:TextBox ID="AuditTo" runat="server" > </asp:TextBox></td> 
         </tr>
         </table>
         <table style="height: 61px; margin-top: 0px">
         <tr>
               <td class="auto-style90">
              Remarks </td>
         <td class="auto-style90">
           <asp:TextBox runat="server" ID="txtuploadRemarks" Width="577px" TextMode="MultiLine" Rows="2"></asp:TextBox>
         </td>
           
             <td style="font-weight:normal; font-weight:bold" class="auto-style90">
             <asp:Label ID="LabelUploadPfd" runat="server" ForeColor="Black" Text="Upload"> </asp:Label>
                </td>
         <td class="auto-style90" >
             <asp:FileUpload ID="FileUploadPdf" runat="server" BorderColor="#996600" 
                 BorderStyle="Inset" Width="190px" />
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                    ControlToValidate="FileUploadPdf" Display="None" 
                                    ErrorMessage="Please select File for upload"
                                    ValidationGroup="validationUpload"></asp:RequiredFieldValidator>
             <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
                 ControlToValidate="FileUploadPdf" Display="None" 
                 ErrorMessage="!Upload only PDF(.pdf,.PDF) file" ForeColor="Red" 
                 ValidationExpression="^.*\.((p|P)(d|D)(f|F))$" ValidationGroup="validationUpload"></asp:RegularExpressionValidator>
</td>
                 <td class="auto-style90">
         <asp:Button ID="Buttonupload" runat="server" Text="Upload" OnClick="BtnUploadPdf_Click"  CausesValidation="true" ValidationGroup="validationUpload" ></asp:Button>

                 </td>
         

         

         </tr>
         </table>


         <table>
         <tr>

         <td align="center">
             <asp:Panel ID="PanelViewUplodedfiles" runat="server" ForeColor="Black" 
                 GroupingText="View Uploaded files" Visible="false" Width="1000px">
                 <asp:GridView ID="GVViewFile" runat="server" AutoGenerateColumns="False" OnRowDataBound="GVViewFile_RowDataBound1"
                     BorderStyle="Solid" CellPadding="3" CellSpacing="3" 
                     DataSourceID="DSforgridview" EmptyDataText="No Records Found"  Width="1000px"
                     HeaderStyle-ForeColor="White" 
                     onselectedindexchanged="GVViewFile_SelectedIndexChanged1"
                     PagerStyle-ForeColor="White" PagerStyle-Height="4" PagerStyle-Width="4">
                     <Columns>
                         <asp:TemplateField ShowHeader="false">
                             <ItemTemplate>
                                 <asp:ImageButton ID="btnView" runat="server" CausesValidation="False" 
                                     CommandName="Select" ImageUrl="~/Images/view.gif" ToolTip="View File" />
                             </ItemTemplate>
                         </asp:TemplateField>
                         <asp:TemplateField HeaderText="Sl.No">   
         <ItemTemplate>
                 <%# Container.DataItemIndex + 1 %>   
         </ItemTemplate>
     </asp:TemplateField>
                         <asp:TemplateField HeaderText="InformationType" ShowHeader="true">
                             <ItemTemplate>
                                 <asp:Label ID="lblgetypename" runat="server" Text='<%# Eval("TypeName") %>'></asp:Label>
                             </ItemTemplate>
                         </asp:TemplateField>
                             
                               <asp:BoundField DataField="CreatedDate" ReadOnly="true" HeaderText="Added On" DataFormatString="{0:d}"
                SortExpression="CreatedDate" />
                     <%--<asp:BoundField DataField="CreatedBy" ReadOnly="true" HeaderText="Added by" 
                SortExpression="FirstName" />--%>
                         <asp:TemplateField HeaderText="Added by" ShowHeader="true">
                         <ItemTemplate>
                                 <asp:Label ID="lbladded" runat="server" Text='<%# Eval("AddedBy") %>'></asp:Label>
                             </ItemTemplate>
                             </asp:TemplateField>
                         <asp:TemplateField Visible="false">
                             <ItemTemplate >
                                 <asp:Label ID="lblgetid" runat="server" Text='<%# Eval("UploadPDFPath") %>'></asp:Label>
                             </ItemTemplate>
                         </asp:TemplateField>
                           <asp:TemplateField HeaderText="Audit From" ShowHeader="true">
                             <ItemTemplate>
                                 <asp:Label ID="lblAuditFrom" runat="server" Text='<%# Eval("AuditFrom") %>'></asp:Label>
                             </ItemTemplate>
                         </asp:TemplateField>
                            <asp:TemplateField HeaderText="Audit To" ShowHeader="true">
                             <ItemTemplate>
                                 <asp:Label ID="lblAuditTo" runat="server" Text='<%# Eval("AuditTo") %>'></asp:Label>
                             </ItemTemplate>
                         </asp:TemplateField>
                         <asp:TemplateField HeaderText="Remark" ShowHeader="true" ItemStyle-Width="250px">
                             <ItemTemplate>
                                 <asp:Label ID="lblgetRemark" runat="server" Text='<%# Eval("Remark") %>'></asp:Label>
                             </ItemTemplate>
                         </asp:TemplateField>
                       
                          <asp:TemplateField  Visible="false">
                             <ItemTemplate>
                                 <asp:Label ID="Unit" runat="server" Text='<%# Eval("Unit_Id") %>'></asp:Label>
                             </ItemTemplate>
                         </asp:TemplateField>
                         <asp:TemplateField  Visible="false">
                             <ItemTemplate>
                                 <asp:Label ID="InfoType" runat="server" Text='<%# Eval("InfoTypeId") %>'></asp:Label>
                             </ItemTemplate>
                         </asp:TemplateField>
                           <asp:TemplateField ShowHeader="false">
                             <ItemTemplate>
                                 <asp:HiddenField ID="lblEntrynum" runat="server" Value='<%# Eval("EntryNo") %>' ></asp:HiddenField>
                             </ItemTemplate>
                         </asp:TemplateField>
                          <asp:TemplateField HeaderText="Delete">
    <ItemTemplate>
    <asp:LinkButton ID="lnkDel" runat="server" OnClick="lnkDeleteClick"  OnClientClick="Confirm()" >Delete</asp:LinkButton>
    </ItemTemplate>
    </asp:TemplateField>

      <asp:TemplateField HeaderText="" Visible="false">
    <ItemTemplate>
    <asp:Label ID="lblUser" runat="server" Visible="false" Text='<%# Eval("CreatedBy") %>'></asp:Label>
    </ItemTemplate>
    </asp:TemplateField>
                     </Columns>
                     <HeaderStyle BackColor="#0b532d" Font-Bold="True" />
                 </asp:GridView>
                 <asp:SqlDataSource ID="DSforgridview" runat="server" 
                     ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" 
                     SelectCommand="  "></asp:SqlDataSource>
             </asp:Panel>
         </td>
         </tr>
         <tr>
         <td align="center">
         <asp:Panel ID="Panel8" runat="server" ForeColor="Black" 
                 GroupingText="Uploaded files by other user" Visible="false" Width="1000px">
                 <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
                     BorderStyle="Solid" CellPadding="3" CellSpacing="3" 
                     DataSourceID="DSforgridview1" 
                     HeaderStyle-ForeColor="White" 
                     onselectedindexchanged="GridView1_SelectedIndexChanged" 
                     PagerStyle-ForeColor="White" PagerStyle-Height="4" PagerStyle-Width="4">
                     <Columns>
                         <asp:TemplateField ShowHeader="false">
                             <ItemTemplate>
                                 <asp:ImageButton ID="btnView" runat="server" CausesValidation="False" 
                                     CommandName="Select" ImageUrl="~/Images/view.gif" ToolTip="View File" />
                             </ItemTemplate>
                         </asp:TemplateField>
                         <asp:TemplateField HeaderText="Sl.No">   
         <ItemTemplate>
                 <%# Container.DataItemIndex + 1 %>   
         </ItemTemplate>
     </asp:TemplateField>
                         <asp:TemplateField HeaderText="InformationType" ShowHeader="true">
                             <ItemTemplate>
                                 <asp:Label ID="lblgetypename" runat="server" Text='<%# Eval("TypeName") %>'></asp:Label>
                             </ItemTemplate>
                         </asp:TemplateField>
                             
                               <asp:BoundField DataField="CreatedDate" ReadOnly="true" HeaderText="Added On" DataFormatString="{0:d}"
                SortExpression="CreatedDate" />
                     <%--<asp:BoundField DataField="CreatedBy" ReadOnly="true" HeaderText="Added by" 
                SortExpression="FirstName" />--%>
                         <asp:TemplateField HeaderText="Added by" ShowHeader="true">
                         <ItemTemplate>
                                 <asp:Label ID="lbladded" runat="server" Text='<%# Eval("AddedBy") %>'></asp:Label>
                             </ItemTemplate>
                             </asp:TemplateField>
                         <asp:TemplateField Visible="false">
                             <ItemTemplate >
                                 <asp:Label ID="lblgetid" runat="server" Text='<%# Eval("UploadPDFPath") %>'></asp:Label>
                             </ItemTemplate>
                         </asp:TemplateField>
                          <asp:TemplateField HeaderText="Audit From" ShowHeader="true">
                             <ItemTemplate>
                                 <asp:Label ID="lblAuditFrom" runat="server" Text='<%# Eval("AuditFrom") %>'></asp:Label>
                             </ItemTemplate>
                         </asp:TemplateField>
                            <asp:TemplateField HeaderText="Audit To" ShowHeader="true" >
                             <ItemTemplate>
                                 <asp:Label ID="lblAuditTo" runat="server" Text='<%# Eval("AuditTo") %>'></asp:Label>
                             </ItemTemplate>
                         </asp:TemplateField>
                         <asp:TemplateField HeaderText="Remark" ShowHeader="true" ItemStyle-Width="250px">
                             <ItemTemplate>
                                 <asp:Label ID="lblgetRemark" runat="server" Text='<%# Eval("Remark") %>'></asp:Label>
                             </ItemTemplate>
                         </asp:TemplateField>
                          <asp:TemplateField  Visible="false">
                             <ItemTemplate>
                                 <asp:Label ID="Unit" runat="server" Text='<%# Eval("Unit_Id") %>'></asp:Label>
                             </ItemTemplate>
                         </asp:TemplateField>
                         <asp:TemplateField  Visible="false">
                             <ItemTemplate>
                                 <asp:Label ID="InfoType" runat="server" Text='<%# Eval("InfoTypeId") %>'></asp:Label>
                             </ItemTemplate>
                         </asp:TemplateField>
                           <asp:TemplateField ShowHeader="false">
                             <ItemTemplate>
                                 <asp:HiddenField ID="lblEntrynum" runat="server" Value='<%# Eval("EntryNo") %>' ></asp:HiddenField>
                             </ItemTemplate>
                         </asp:TemplateField>
                        

      <asp:TemplateField HeaderText="" Visible="false">
    <ItemTemplate>
    <asp:Label ID="lblUser" runat="server" Visible="false" Text='<%# Eval("CreatedBy") %>'></asp:Label>
    </ItemTemplate>
    </asp:TemplateField>
                     </Columns>
                     <HeaderStyle BackColor="#0b532d" Font-Bold="True" />
                 </asp:GridView>
                 <asp:SqlDataSource ID="DSforgridview1" runat="server" 
                     ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" 
                     SelectCommand="  "></asp:SqlDataSource>
             </asp:Panel>
         </td>
         </tr>
      </table>
     </asp:Panel>
        <br />
         <asp:Panel ID="panelReowrkRemarks" runat="server" Visible="false" Style="font-weight:bold; border-style:groove;background-color:#E0EBAD" GroupingText="Remarks for Rework"  > 
 <table style="width: 92%">
   <tr>
     <td style="height: 36px; font-weight:normal">
      <asp:Label ID="Labelrem" runat="server" Text="Remarks"  ></asp:Label>
       </td>
        <td style="height: 36px">
          <asp:TextBox ID="txtRework" runat="server" TextMode="MultiLine" Style="border-style:inset none none inset;" 
        Width="900px" ></asp:TextBox>   
       </td>
     </tr>
</table>
</asp:Panel>
  <asp:HiddenField ID="rowVal" runat="server" />
    <asp:HiddenField ID="senderID" runat="server" />         
<asp:Panel ID="panel6" runat="server" Style="font-weight:bold; border-style:groove;background-color:#E0EBAD" Visible="true" > 
<table width="100%">
    <tr>
<td align="center">
 <%-- <asp:Button ID="BtnRework" runat="server" Text="Rework" OnClick="BtnRework_Click" Enabled="false"  ></asp:Button>--%>
 <asp:Button ID="BtnSanction" runat="server" Text="SUBMIT" OnClick="BtnSanction_Click" Enabled="false" ValidationGroup="validation" CausesValidation="true" ></asp:Button>
  <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="Please Upload the file before Submitting the Project " Display="None" ControlToValidate="FileUploadPdf" ValidationGroup="validation" Enabled="false"></asp:RequiredFieldValidator>
</td>
 </tr>
</table>
</asp:Panel>

   
</asp:Panel>






</asp:Content>

