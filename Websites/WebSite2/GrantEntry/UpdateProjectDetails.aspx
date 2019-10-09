<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPageRMS.master" AutoEventWireup="true" CodeFile="UpdateProjectDetails.aspx.cs" Inherits="GrantEntry_UpdateProjectDetails" MaintainScrollPositionOnPostback="true" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
          <script src="../Scripts/jquery-1.7.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-1.7.1.min.js" type="text/javascript"></script>
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
      .auto-style56 {
          width: 228px;
          height: 37px;
      }
      .auto-style57 {
          width: 70px;
          height: 37px;
      }
      .auto-style58 {
          width: 128px;
          height: 37px;
      }
      .auto-style59 {
          width: 128px;
          height: 31px;
      }
      .auto-style60 {
          height: 31px;
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
      .auto-style67 {
          width: 196px;
      }
      .auto-style71 {
          height: 37px;
          width: 118px;
      }
      .auto-style72 {
          height: 31px;
          width: 217px;
      }
      .auto-style73 {
          height: 34px;
          width: 118px;
      }
          
      .style1
      {
          height: 36px;
          width: 506px;
      }
    
      .style2
      {
          width: 128px;
      }
    
      .auto-style75 {
          height: 37px;
          width: 226px;
      }
      .auto-style76 {
          height: 31px;
          width: 308px;
      }
      .auto-style77 {
          width: 308px;
      }
      .auto-style78 {
          height: 31px;
          width: 299px;
      }
      .auto-style79 {
          width: 299px;
      }
      .auto-style80 {
          height: 31px;
          width: 295px;
      }
      .auto-style81 {
          width: 295px;
      }
      .auto-style82 {
          height: 31px;
          width: 226px;
      }
      .auto-style84 {
          width: 186px;
      }
      .auto-style86 {
          width: 110px;
      }
      .auto-style87 {
          height: 46px;
          width: 110px;
      }
      .auto-style88 {
          height: 45px;
          width: 128px;
      }
      .auto-style89 {
          height: 47px;
          width: 128px;
      }
      .auto-style90 {
          width: 111px;
      }
      .auto-style91 {
          height: 46px;
          width: 111px;
      }
      .auto-style93 {
          height: 46px;
          width: 128px;
      }
      .auto-style95 {
          width: 217px;
      }
      .auto-style96 {
          height: 37px;
          width: 178px;
      }
      .auto-style97 {
          width: 178px;
      }
      .auto-style102 {
          height: 34px;
          width: 178px;
      }
      .auto-style103 {
          width: 226px;
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
          //var validated = Page_ClientValidate('validation');
          //if (validated) 
          
              var OLDprojecttype = document.getElementById('<%= myHiddenOldProjecttype.ClientID %>');
              var NEWprojecttype = document.getElementById('<%= DropDownListTypeGrant.ClientID %>').value;
              var confirm_value2 = document.createElement("INPUT");
              confirm_value2.type = "hidden";
              confirm_value2.name = "confirm_value2";
              confirm_value2.value = "Yes";
               if (OLDprojecttype != NEWprojecttype) {
                
                  if (confirm("UTN id will be Updated. Do you want to continue?"))
                      confirm_value2.value = "Yes";                
                  else
                      confirm_value2.value = "No";
                  document.forms[0].appendChild(confirm_value2);
           }
             
          
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
   
   
    <script type = "text/javascript">

        function setRowPop(obj) {

            var row = obj.parentNode.parentNode;
            var rowIndex = row.rowIndex - 1;


            var sndID = obj.id;
            var sndrID = document.getElementById('<%= senderID.ClientID %>');
            sndrID.value = sndID;

            var rowNo = document.getElementById('<%= rowVal.ClientID %>');
            rowNo.value = rowIndex;
            var mu = $(row).find("[id*=DropdownMuNonMu]").val();
            if (mu == "M") {
                $('#<%=popupPanelAffil.ClientID %>').show()
               $('#<%=popupstudent.ClientID %>').hide();

           }
           else if (mu == "S") {
               $('#<%=popupstudent.ClientID %>').show();
               $('#<%=popupPanelAffil.ClientID %>').hide();

           }
   }
    </script>

   <script language="javascript" type="text/javascript">
       function CalculateAmount(txtBdlsClientID, txtPcsClientID, lblBdlCtClientID) {
           var Bdls = document.getElementById(txtBdlsClientID);
           var BdlCt = document.getElementById(lblBdlCtClientID);
           var Pcs = document.getElementById(txtPcsClientID);

           Pcs.value = parseInt(Bdls.value) * parseInt(BdlCt.innerHTML);
       }
</script>

<script language="javascript" type="text/javascript">
    function CalculateAmount(txtBdlsClientID, txtPcsClientID, lblBdlCtClientID) {
        var Bdls = document.getElementById(txtBdlsClientID);
        var BdlCt = document.getElementById(txtPcsClientID);
        var Pcs = document.getElementById(lblBdlCtClientID);
        if (BdlCt.value != "") {
            Pcs.value = parseInt(Bdls.value) + parseInt(BdlCt.value);
        }
        else
            Pcs.value = parseInt(Bdls.value);
    }
</script>

<script language="javascript" type="text/javascript">
    function CalculateAmount1(txtBdlsClientID, txtPcsClientID, lblBdlCtClientID) {
        var Bdls = document.getElementById(txtBdlsClientID);
        var BdlCt = document.getElementById(txtPcsClientID);
        var Pcs = document.getElementById(lblBdlCtClientID);
        if (Bdls.value != "") {
            Pcs.value = parseInt(Bdls.value) + parseInt(BdlCt.value);
        }
        else
            Pcs.value = parseInt(BdlCt.value);
    }
</script>
<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="true"></asp:ToolkitScriptManager>
 
               
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
    <%--<asp:GridView ID="GridViewsanSearch" runat="server"  AllowPaging="True" 
        PagerSettings-PageButtonCount="5" PageSize="5" DataSourceID="SqlDataSource2"  
        HeaderStyle-BackColor="#CC6600" HeaderStyle-ForeColor="Black" AutoGenerateColumns="False" OnPageIndexChanging="GridViewSearchsan_PageIndexChanging" 
        PagerStyle-BackColor="#CC6600" PagerStyle-ForeColor="White" CellPadding="3" OnRowEditing="edit1" OnRowDataBound="GridViewsanSearch_RowDataBound" 
        CellSpacing="3" PagerStyle-Width="4" PagerStyle-Height="4" Visible="False" OnRowCommand="GridViewsanSearch_RowCommand"
        BorderColor="#FF6600" BorderStyle="Solid" DataKeyNames="ID,ProjectType" Width="1030px">
        <Columns>

     
<asp:TemplateField ShowHeader="False" >
   <ItemTemplate>
   <asp:ImageButton ID="BtnEdit1" runat="server" CausesValidation="False" CommandName="Edit" ImageUrl="~/Images/edit.gif" ToolTip="Edit"  />
   </ItemTemplate>
   </asp:TemplateField>
       <asp:TemplateField ShowHeader="False">
   <ItemTemplate>
   <asp:Button ID="BtnEditGridViewView" runat="server" CausesValidation="False" CommandName="View"  Text="Approval"  ToolTip="Approval" Visible="false" />
   </ItemTemplate>
   </asp:TemplateField>
    <asp:BoundField DataField="ProjectUnit" ReadOnly="true" HeaderText="Project Unit" 
                SortExpression="ProjectUnit" />

            <asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID" ReadOnly="True" />
            <asp:BoundField DataField="TypeName" HeaderText="TypeName" SortExpression="TypeName" />
            <asp:BoundField DataField="UTN" HeaderText="UTN" SortExpression="UTN" />

            
            <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title" />
            <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" />
            <asp:BoundField DataField="AppliedAmount" HeaderText="AppliedAmount" SortExpression="AppliedAmount" />
            <asp:BoundField DataField="SanctionType" HeaderText="SanctionType" SortExpression="SanctionType" />

            
            <asp:BoundField DataField="StatusName" HeaderText="StatusName" SortExpression="StatusName" />
            
    <asp:TemplateField ShowHeader="False">
   <ItemTemplate>
   <asp:HiddenField ID="hiddenProjectType" runat="server" Value='<%# Eval("ProjectType") %>'/>
   </ItemTemplate>
   </asp:TemplateField>

            
        </Columns>

           <HeaderStyle BackColor="#0B532D" Font-Bold="True" ForeColor="White" />
           <PagerSettings PageButtonCount="5" />
        <PagerStyle BackColor="#0B532D" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#FFFFFF" ForeColor="#333333" />
        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
        <SortedAscendingCellStyle BackColor="#FDF5AC" />
        <SortedAscendingHeaderStyle BackColor="#4D0000" />
        <SortedDescendingCellStyle BackColor="#FCF6C0" />
        <SortedDescendingHeaderStyle BackColor="#820000" />
        <EditRowStyle BorderColor="#FF6600" BorderStyle="Solid" />

    </asp:GridView>--%>
     <%-- <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
         ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" 
         SelectCommand=""  >
        </asp:SqlDataSource>--%>
 </center>
  </asp:Panel>
  <br />

  <asp:Panel ID="Panel7" runat="server" > 
<asp:Panel ID="MainpanelGrant" runat="server" GroupingText="Basic Details" Style="font-weight:bold; border-style:inset;background-color:#E0EBAD; margin-bottom: 0px;"  > 
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
 <td class="auto-style96">
     <asp:TextBox ID="TextBoxUTN" runat="server" Enabled="false" Width="150px" Height="16px" ></asp:TextBox>
                              
 </td>
     </tr>
 <tr>
  <td class="auto-style59">
 
   <asp:Label ID="LabelTypeGrant" runat="server" Text="Project Type" ForeColor="Black"></asp:Label>
                    
 </td>
 <td class="auto-style82">
     <asp:DropDownList ID="DropDownListTypeGrant" runat="server" Width="200px" AppendDataBoundItems="true"   DataSourceID="SqlDataSourceDropDownListTypeGrant" DataTextField="TypeName" DataValueField="TypeId" onchange="AddConfirm()" OnSelectedIndexChanged="DropDownListTypeGrant_SelectedIndexChanged" AutoPostBack="true" >
         <asp:ListItem Value="">--Select--</asp:ListItem>
     </asp:DropDownList>
     
        <asp:SqlDataSource ID="SqlDataSourceDropDownListTypeGrant" runat="server" ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" 
           SelectCommand="select TypeId,TypeName from ProjectType_M">
     
                </asp:SqlDataSource>                        
 </td>
 
 <td class="auto-style73">
 
   <asp:Label ID="LabelGrantUnit" runat="server" Text="Project Unit" ForeColor="Black"></asp:Label>
                    
 </td>
 <td class="auto-style102">
     <asp:DropDownList ID="DropDownListGrUnit" runat="server" DataSourceID="SqlDataSourceDropDownListGrUnit" AppendDataBoundItems="true"  OnSelectedIndexChanged="onchangeUnit" AutoPostBack="true"  DataTextField="UnitName" DataValueField="UnitId" Enabled="true" Height="20px"  Width="150px">
         <asp:ListItem Value="">--Select--</asp:ListItem>
     </asp:DropDownList>
     <asp:SqlDataSource ID="SqlDataSourceDropDownListGrUnit" runat="server" ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" 
           SelectCommand="select UnitId,UnitName from ProjectUnit_M">
     
                </asp:SqlDataSource>
                              
 </td>

 

  <td class="auto-style72">
 
   <asp:Label ID="LabelStatus" runat="server" Text="Project Status" ForeColor="Black"></asp:Label>
                    
 </td>
 <td class="auto-style60">
     <asp:DropDownList ID="DropDownListProjStatus" runat="server" Width="130px"   DataSourceID="SqlDataSourcePrjStatus" DataTextField="StatusName" DataValueField="StatusId"  OnSelectedIndexChanged="DropDownListProjStatusOnSelectedIndexChanged" AutoPostBack="true" Height="20px"></asp:DropDownList>
        <asp:SqlDataSource ID="SqlDataSourcePrjStatus" runat="server" ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" 
           SelectCommand="select StatusId,StatusName from Status_Project_M where StatusId='APP'">
 </asp:SqlDataSource>                        
 </td>
 
 </tr>
     <tr>

<td class="style2">
    <asp:Label ID="LabelGrantDate" runat="server" Text="Applied Date" ForeColor="Black"></asp:Label>
    </td>
              <td class="auto-style103">
    <asp:TextBox ID="TextBoxGrantDate" runat="server" Width="200px" AutoPostBack="true" ></asp:TextBox></td>
    <asp:CalendarExtender ID="CalendarExtender5" runat="server"
                TargetControlID="TextBoxGrantDate" Format="dd/MM/yyyy" >
            </asp:CalendarExtender>

                <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" Display="None"  ControlToValidate="TextBoxGrantDate" ValidationGroup="validation"
                ErrorMessage="Presentation Date in (dd/mm/yyyy) format" SetFocusOnError="true"  
                ValidationExpression="^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$">

                </asp:RegularExpressionValidator>
<%--          <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="Applied date must be less than or equal to the current date."  ValidationGroup="validation" Display="None"  ControlToValidate="TextBoxGrantDate"
                Operator="LessThanEqual" Type="Date"></asp:CompareValidator> --%>


<%-- <td>
    <asp:Label ID="Label13" runat="server" Text="Actual Applied Date" ForeColor="Black"></asp:Label>
    </td>
              <td class="auto-style97">
    <asp:TextBox ID="txtprojectactualdate" runat="server" Width="150px" ></asp:TextBox></td>
    <asp:CalendarExtender ID="CalendarExtender8" runat="server"
                TargetControlID="txtprojectactualdate" Format="dd/MM/yyyy" >
            </asp:CalendarExtender>

                <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" Display="None"  ControlToValidate="txtprojectactualdate" ValidationGroup="validation"
                ErrorMessage="Please enter  actual applieddate in (dd/mm/yyyy) format" SetFocusOnError="true"  
                ValidationExpression="^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$">
                </asp:RegularExpressionValidator> 
     <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Actual applied date must be less than or equal to the current date."  ValidationGroup="validation" Display="None"  ControlToValidate="txtprojectactualdate"
                Operator="LessThanEqual" Type="Date"></asp:CompareValidator> --%>
    
  <td class="auto-style95">
 
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
    <%-- </tr>--%>
 <%--<tr>--%>

  <td class="style2">
         <asp:Label ID="Label2" runat="server" Text="Contact Number">

         </asp:Label>
          </td>
              <td class="auto-style103"><asp:TextBox ID="txtcontact" runat="server" MaxLength="12" Width="200px" ></asp:TextBox>
     </td>
     <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ErrorMessage="Enter valid Phone number"  ValidationGroup="validation"
ControlToValidate="txtcontact" ValidationExpression= "^([0-9\(\)\/\+ \-]*)$" Display="None"></asp:RegularExpressionValidator>
</tr>
 <tr>
      <td class="auto-style65">
 
   <asp:Label ID="LabelSrcGrnat" runat="server" Text="Project Source" ForeColor="Black"></asp:Label>
                    
 </td>
 <td class="auto-style102">
     <asp:DropDownList ID="DropDownListSourceGrant" runat="server" DataSourceID="SqlDataSourceDropDownListSourceGrant" DataTextField="SourceName" DataValueField="SourceId" Enabled="true" Height="20px" Width="150px" ></asp:DropDownList>
            <asp:SqlDataSource ID="SqlDataSourceDropDownListSourceGrant" runat="server" ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" 
           SelectCommand="select SourceId,SourceName from ProjectSource_M">
     
                </asp:SqlDataSource>
                        
 </td>

          <td class="auto-style65">   <asp:Label ID="Label14" runat="server" Text="Duration Of The Project(in months)" ForeColor="Black"></asp:Label>
</td>
         <td class="auto-style102"><asp:TextBox ID="txtProjectDuration" runat="server" MaxLength="2" Width="150px"></asp:TextBox></td>
       
 
     
   <td class="auto-style95">
   <asp:Label ID="Labelerfrelated" runat="server" Text="ERF Related?(Environmental Research Fund)" ForeColor="Black"></asp:Label>

         </td>
          <td class="auto-style67" >
     <asp:DropDownList ID="DropDownListerfRelated" runat="server"   Width="147px" Height="20px"  >
     <asp:ListItem Value="N">No</asp:ListItem>
          <asp:ListItem Value="Y">Yes</asp:ListItem>
     </asp:DropDownList>
     <%-- <asp:Label ID="Label9" runat="server" Text="( Environmental Research Fund )" ForeColor="Black"></asp:Label>--%>

</td>

     </tr>
          
     <tr>
         <td class="auto-style93">
         <asp:Label ID="Label7" runat="server" Text="Project Title" ForeColor="Black"></asp:Label>
                    
 </td>
 <td colspan="4" class="auto-style63">
     <asp:TextBox ID="TextBoxTitle" runat="server" Width="628px" Height="40px" TextMode="MultiLine" Enabled="true" Rows="2" MaxLength="500" onkeypress="return this.value.length<500"  ></asp:TextBox>
</td>
</tr>
     <tr>
         <td class="auto-style88">
 
   <asp:Label ID="LabelDescription" runat="server" Text="Description" ForeColor="Black"></asp:Label>
                    
 </td>
 <td colspan="4" class="auto-style62">
     <asp:TextBox ID="TextBoxDescription" runat="server" TextMode="MultiLine" Width="628px"  onkeypress="return this.value.length<500"></asp:TextBox>
                              
 </td>
     </tr>
      <tr>
         <td class="auto-style89">
 
   <asp:Label ID="Label1" runat="server" Text="Agency Comment" ForeColor="Black"></asp:Label>
                    
 </td>
 <td colspan="4" class="auto-style64">
     <asp:TextBox ID="TextBoxAdComments" runat="server" TextMode="MultiLine" Width="628px" MaxLength="500" onkeypress="return this.value.length<500"></asp:TextBox>                              
 </td>
     </tr>
     <tr>

          <td class="style74" >

    <asp:Label ID="lblRevisedAppliedAmt" runat="server" Text="Sanction Amount" ForeColor="Black" Visible="false" ></asp:Label>
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
 
 <asp:Panel ID="Panel10" runat="server" Style="font-weight:bold; background-color:#E0EBAD; margin-top: 9px;" GroupingText="Agency Details">

          <table style="width: 1050px">
              <tr>
               
                   <td class="auto-style81">

   <asp:Label ID="Label3" runat="server" Text="Project Agency" ForeColor="Black"></asp:Label>
                    
 </td>
<%--  <td>

   <asp:Label ID="LabelGrnatAgency" runat="server" Text="Project Agency" ForeColor="Black"></asp:Label>
                    
     
                    
 </td>--%>
     <td class="auto-style76">
     <asp:TextBox ID="txtagency" runat="server" AutoPostBack="true" Enabled="false"></asp:TextBox>


      <asp:ImageButton ID="imageBkCbtn" runat="server" ImageUrl="~/Images/srchImg.gif" OnClientClick="setRow(this)"  /></td>

                 <asp:ModalPopupExtender ID="model" runat="server" TargetControlID="imageBkCbtn" PopupControlID="popupselectNo"></asp:ModalPopupExtender>
     <%--<asp:DropDownList ID="TextBoxGrantAgency" runat="server" Width="230px"  DataSourceID="SqlDataSourceTextBoxGrantAgency" DataTextField="FundingAgencyName" DataValueField="FundingAgencyId" style="margin-top: 6px" Enabled="false"  ></asp:DropDownList>--%>
                  
    
                  <td class="auto-style76">
         <asp:Label ID="Label5" runat="server" Text="Contact No">

         </asp:Label>
          </td>
              <td class="auto-style76"><asp:TextBox ID="txtagencycontact" runat="server" MaxLength="12"></asp:TextBox>
     </td>
         <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ErrorMessage="Enter valid Phone number"  ValidationGroup="validation"
ControlToValidate="txtagencycontact" ValidationExpression= "^([0-9\(\)\/\+ \-]*)$" Display="None"></asp:RegularExpressionValidator>          
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
         <asp:TextBox ID="txtAddress" runat="server" MaxLength="100" onkeypress="return this.value.length<100"  TextMode="MultiLine"></asp:TextBox>
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
                      <asp:DropDownList ID="DropDownSectorLevel" runat="server" DataSourceID="SqlDataSourceSectorLevel" AppendDataBoundItems="true"   AutoPostBack="true"  DataTextField="SectorLevelName" DataValueField="Id" Enabled="false" Height="20px"  Width="150px">
         <asp:ListItem Value="0">--Select--</asp:ListItem>
     </asp:DropDownList>
     <asp:SqlDataSource ID="SqlDataSourceSectorLevel" runat="server" ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" 
           SelectCommand="select Id,SectorLevelName from ProjectFundingSectorLevel_M"></asp:SqlDataSource>                 
                  </td>
                   <td>
 Agency type:
                      </td>
                      <td>
                      <asp:DropDownList ID="DropDownAgencyType" runat="server" DataSourceID="SqlDataSourceAgencyType" AppendDataBoundItems="true"   AutoPostBack="true"  DataTextField="TypeName" DataValueField="Id" Enabled="false" Height="20px"  Width="150px">
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
 
      
 </td>
 <td class="auto-style19">
 <asp:DropDownList ID="DropDownListSanType" runat="server"  Width="90px" DataSourceID="SqlDataSourceDropDownListSanType" Visible="false" DataTextField="SanctionTypeName" DataValueField="SanctionTypeId"  OnSelectedIndexChanged="DropDownListSanTypeOnSelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
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
 <asp:TextBox ID="TextBoxKindDetails" runat="server" TextMode="MultiLine"  ForeColor="Black" Visible="false" Rows="2" Width="400px" Height="40px"></asp:TextBox>
 </td>
</tr>
</table>
     </asp:Panel>
 <br />
      
    <asp:Panel ID="panAddAuthor" runat="server" GroupingText="Investigator Details"  ForeColor="Black"  Style="font-weight:bold; border-style:inset;background-color:#E0EBAD"  > 

              <%--       <center>
          Author Details
                     </center>--%>
                     <br />
                         <table width="92%">                  
                           <tr>
                           <td>
                           <asp:Button ID="BtnAddMU" runat="server" Text="Add New Investigator" onclick="addRow"/> 
                            
                           </td>
                           </tr>
                           </table>  
  <br />
        <asp:SqlDataSource ID="SqlDataSourceAuthorType" runat="server" 
                ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" 
                ProviderName="System.Data.SqlClient"> 
                                     
            </asp:SqlDataSource>
<asp:Panel ID="PanelMU" runat="server" ScrollBars="both"  Width="1000px" >
  
     <asp:GridView ID="Grid_AuthorEntry" runat="server" AutoGenerateColumns="False"  GridLines="None" 
     OnRowDeleting="Grid_AuthorEntry_RowDeleting" CellPadding="4" ForeColor="#333333" OnRowDataBound="OnRowDataBound1" >

     <AlternatingRowStyle BackColor="White" />
     <Columns>

        <asp:TemplateField HeaderText="MAHE/Non MAHE">
             <ItemTemplate>
                 <asp:DropDownList ID="DropdownMuNonMu" runat="server" Width="150" OnSelectedIndexChanged="DropdownMuNonMuOnSelectedIndexChanged" AutoPostBack="true" DataSourceID="SqlDataSourceAuthorType" >
                     
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
                 <asp:TextBox ID="AuthorName" runat="server" Width="200" Enabled="false" OnTextChanged="AuthorName_Changed" AutoPostBack="true"></asp:TextBox>
          
                 <asp:ImageButton ID="EmployeeCodeBtn" runat="server" ImageUrl="~/Images/srchImg.gif" OnClientClick="setRowPop(this)" />

                 <asp:ModalPopupExtender ID="ModalPopupExtender4" runat="server" TargetControlID="EmployeeCodeBtn" PopupControlID="popup"
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
      <asp:DropDownList ID="AuthorType" runat="server" Width="170" OnSelectedIndexChanged="AuthorType_SelectedIndexChanged" AutoPostBack="true" >
             <asp:ListItem Value="">--Select--</asp:ListItem>
        <asp:ListItem Value="P">Principal Investigator</asp:ListItem>
          <asp:ListItem Value="C">CO-Investigator</asp:ListItem>
        </asp:DropDownList>
  <asp:Image ID="ImageisAuthorType" runat="server" ImageUrl="~/Images/srchImg.gif" CssClass="blnkImgCSS" />
 </ItemTemplate>              
  </asp:TemplateField>
         <asp:TemplateField HeaderText="isLeadPI"  >
             <ItemTemplate>
                 <asp:DropDownList ID="isLeadPI" runat="server" Width="75" AutoPostBack="true"  >
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
                 <asp:TextBox ID="MailId" runat="server" Width="200"  Enabled="false" MaxLength="30"  ></asp:TextBox>
       <asp:Image ID="ImageMailId" runat="server" ImageUrl="~/Images/srchImg.gif" CssClass="blnkImgCSS" />
        <asp:RegularExpressionValidator
        id="regEmail"
        ControlToValidate="MailId" Display="Static" ErrorMessage="Invalid Email Id"
        Text="(Invalid email)" ValidationGroup="validation"
        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
        Runat="server" />    
                  </ItemTemplate>         
        </asp:TemplateField>

  
  <asp:TemplateField  HeaderText="Colloboration">
     <ItemTemplate>
    <asp:DropDownList ID="NationalType" runat="server" Width="140" OnSelectedIndexChanged="NationalTypeOnSelectedIndexChanged" AutoPostBack="true"  >
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
  <asp:CommandField DeleteText="Remove" ShowDeleteButton="True"   />
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
   <asp:Panel ID="panel3" runat="server" Visible="true" GroupingText="Remarks for ProjectType Update" Style="font-weight:bold; border-style:inset;background-color:#E0EBAD"  > 
 <table style="width: 92%">
   <tr>
     <td style="height: 36px; font-weight:normal">
      <asp:Label ID="Label13" runat="server" Text="Remarks"  ></asp:Label>
       </td>
        <td style="height: 36px">
          <asp:TextBox ID="txtProjectTypeRemarks" runat="server" TextMode="MultiLine" Style="border-style:inset none none inset;" Enabled="true"
        Width="900px" ></asp:TextBox>   
       </td>
       <td>
           <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please enter the remarks" ValidationGroup="validation" ControlToValidate="txtProjectTypeRemarks"  Display="None" ></asp:RequiredFieldValidator>
       </td>
     </tr>
</table>
</asp:Panel>
 <br />
    </asp:Panel>

       <asp:Panel ID="popup" runat="server" Visible="false" CssClass="modelPopup" style="width: 1000px;height:400px;background-color:ghostwhite;">
     <asp:Panel ID="popupPanelAffil" runat="server">

<table style="background:white">
<tr>
<th align="center" colspan="3"> Author </th>
</tr>
<tr>
<td>Search By Name: <asp:TextBox ID="affiliateSrch" runat="server"></asp:TextBox>
<asp:Button ID="BtnEmployeeSearch" runat="server" Text="Search" OnClick="AuthorNameChanged" />
<asp:Button ID="Buttonexit4" runat="server" Text="EXIT" OnClick="exit" />
 </td> 
</tr>
<tr>
<td colspan="3">
<asp:GridView ID="popGridAffil" runat="server"  AutoGenerateSelectButton="true" EmptyDataText="No User Found"
OnSelectedIndexChanged="popSelected1" AllowSorting="true"  >
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
<asp:SqlDataSource ID="SqlDataSourceAffil" runat="server" ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" 
SelectCommand="SELECT top 10  User_Id,prefix+' '+UPPER(firstname)+' '+UPPER(middlename)+' '+UPPER(lastname)  as Name from User_M"  ProviderName="System.Data.SqlClient">
</asp:SqlDataSource>

</td>
</tr>
</table>
</asp:Panel>
            <asp:Panel ID="popupstudent" runat="server" >

<center>
<table align="center">
<tr>
<th > Student Details </th>
</tr>
</table>
<br />
<table>
<tr>
<td>Name:</td><td> <asp:TextBox ID="txtSrchStudentName" runat="server"   ></asp:TextBox></td> 
<td></td>
<td>Roll No:</td><td> <asp:TextBox ID="txtSrchStudentRollNo" runat="server"></asp:TextBox></td> 
<td>Institution:</td><td><asp:DropDownList ID="StudentIntddl" DataSourceID="sqlstudentds" DataTextField="InstName" DataValueField="InstID" runat="server" AppendDataBoundItems="true">
<asp:ListItem Value="">--Select--</asp:ListItem>
</asp:DropDownList></td>
</tr>
</table>
<br />
<table align="center">
<tr>
<td><asp:Button ID="Button14" runat="server" Text="Search" OnClick="SearchStudentData" /></td>
<td><asp:Button ID="Button15" runat="server" Text="EXIT" OnClick="exit" /> </td>
</tr>
</table>
<br />
<table>
<tr>
<td>
<asp:GridView ID="popupStudentGrid" runat="server"  AutoGenerateSelectButton="true" EmptyDataText="No Records Found" GridLines="Both"
OnSelectedIndexChanged="StudentDataSelect" AllowSorting="true"  AutoGenerateColumns="false" CellPadding="5"  CellSpacing="5">
<FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510"  />
        <HeaderStyle BackColor="#0b532d" Font-Bold="True" ForeColor="White" />
        <PagerStyle ForeColor="#000000" HorizontalAlign="Center" />
        <RowStyle BackColor="#FFFFFF" ForeColor="#000000" />
        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#FFF1D4" />
        <SortedAscendingHeaderStyle BackColor="#B95C30" />
        <SortedDescendingCellStyle BackColor="#F1E5CE" />
        <SortedDescendingHeaderStyle BackColor="#93451F" />
         <Columns>
         <asp:BoundField DataField="RollNo" HeaderText="Roll No" ReadOnly="True" 
            SortExpression="RollNo"  ItemStyle-Width="100px"/>
        <asp:BoundField DataField="Name" HeaderText="Name" ReadOnly="True" 
            SortExpression="Name"  ItemStyle-Width="500px" />
            <asp:BoundField DataField="InstName" HeaderText="Institution" ReadOnly="True" 
            SortExpression="InstName"  ItemStyle-Width="500px" />
             <asp:BoundField DataField="ClassName" HeaderText="Class" ReadOnly="True" 
            SortExpression="ClassName"  ItemStyle-Width="500px" />
             <asp:BoundField DataField="EmailID1" HeaderText="Email" ReadOnly="True" 
            SortExpression="EmailID1"  ItemStyle-Width="500px" />
          
           
            <asp:TemplateField ShowHeader="false" >
            <ItemTemplate>
            <asp:HiddenField ID="lblClassCode" runat="server" Value='<%# Eval("ClassCode") %>'></asp:HiddenField>
            </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField ShowHeader="false">
            <ItemTemplate>
             <asp:HiddenField ID="lblInstn" runat="server" Value='<%# Eval("InstID") %>'></asp:HiddenField>
            </ItemTemplate>
            </asp:TemplateField>
        </Columns>
</asp:GridView>
<asp:SqlDataSource ID="StudentSQLDS" runat="server" ConnectionString="<%$ ConnectionStrings:SISConStr %>" 
SelectCommand="Select TOP 10  RollNo,Name,SISClass.ClassName as ClassName,SISInstitution.InstName as InstName,EmailID1 
,SISStudentGenInfo.ClassCode as ClassCode,SISInstnHR.HRInstitute as InstID from SISStudentGenInfo,SISClass,SISInstitution,SISInstnHR  where 
SISStudentGenInfo.ClassCode=SISClass.ClassCode and SISStudentGenInfo.InstID=SISInstitution.InstID
and SISInstnHR.Institute_Id=SISInstitution.InstID and SISInstnHR.Institute_Id=SISStudentGenInfo.InstID" ProviderName="System.Data.SqlClient">
<SelectParameters>
<asp:SessionParameter Name="UserId" SessionField="UserId" Type="String" />
</SelectParameters>
</asp:SqlDataSource>
<asp:SqlDataSource ID="sqlstudentds" runat="server" ConnectionString="<%$ ConnectionStrings:SISConStr %>" 
SelectCommand="Select InstName,InstID from SISInstitution" ProviderName="System.Data.SqlClient">
</asp:SqlDataSource>
</td>
</tr>
</table>
</center>
</asp:Panel>
</asp:Panel>
<asp:HiddenField ID="rowVal" runat="server" />
    <asp:HiddenField ID="senderID" runat="server" />
<%--    <asp:RequiredFieldValidator ID="RVDOR" runat="server" ErrorMessage="Enter the Applied date" ControlToValidate="TextBoxGrantDate" Display="None" ValidationGroup="validation" SetFocusOnError="true" ></asp:RequiredFieldValidator>--%>
       <asp:ValidationSummary ID="ValidationSummary3" runat="server" ShowMessageBox="True" DisplayMode="BulletList"  HeaderText="Validation issues" ShowSummary="False"  ValidationGroup="Validation"/>

<asp:Panel ID="panel6" runat="server" Style="font-weight:bold; border-style:groove;background-color:#E0EBAD" Visible="true" > 
<table width="100%">
    <tr>
<td align="center">
  <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="BtnSave_Click"  CausesValidation="true" ValidationGroup="validation"  ></asp:Button>
<%-- <asp:Button ID="Buttonclr" runat="server" Text="Clear" OnClick="addclik" CausesValidation="false"></asp:Button>
  <asp:Button ID="btnAddProject" runat="server" Text="Add New Project" 
        Visible="true"   CausesValidation="false" onclick="btnAddProject_Click"  ></asp:Button>
    <asp:Button ID="ButtonSavepdf" Height="20" Font-Bold="true" runat="server" Text="Download UTN Form" OnClick="BtnGenetratePdf" CausesValidation="false" Enabled="false"/>--%>


</td>
 </tr>
</table>
</asp:Panel>

      
<asp:RegularExpressionValidator ID="RegularExpressionValidator3"
                         runat="server" 
                         ErrorMessage="PAN Number should be in the followed format ABCDE1234X"
                         ValidationExpression="^[A-Z]{5}[0-9]{4}[A-Z]{1}$"
                         ControlToValidate="txtpan"
                         ValidationGroup="validation"
                         Display="None"
                          ></asp:RegularExpressionValidator>


<asp:RequiredFieldValidator ID="RequiredFieldValidator5"
                        runat="server" Enabled="false"
                        ErrorMessage="Enter Agency Contact number"
                        ControlToValidate="txtagencycontact"
                        ValidationGroup="validation"
                        Display="None" ></asp:RequiredFieldValidator>
<asp:RequiredFieldValidator ID="RequiredFieldValidator4"
                        runat="server" Enabled="false"
                        ErrorMessage="Enter Agency EmailId "
                        ControlToValidate="txtEmailId"
                        ValidationGroup="validation"
                        Display="None" ></asp:RequiredFieldValidator>
    
    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Select Project Type"  ControlToValidate="DropDownListTypeGrant"
                        ValidationGroup="validation" Display="None" SetFocusOnError="true"></asp:RequiredFieldValidator>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator3"
                        runat="server" InitialValue="" SetFocusOnError="true"
                        ErrorMessage="Select Project Unit"
                        ControlToValidate="DropDownListGrUnit"
                        ValidationGroup="validation"
                        Display="None" ></asp:RequiredFieldValidator>
    <asp:CompareValidator ID="CompareValidator3" runat="server" Operator="DataTypeCheck" Type="Integer"  ValidationGroup="validation" Display="None"
 ControlToValidate="txtProjectDuration" ErrorMessage="Duration of project must be in months" />
     <asp:RegularExpressionValidator ID="RegularExpressionValidator10" runat="server" ErrorMessage="Mobile no. should be in digits" ValidationExpression="\d+$" ControlToValidate="txtcontact" ValidationGroup="validation" Display="None" SetFocusOnError="true" ></asp:RegularExpressionValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator11" runat="server" ControlToValidate="txtcontact" ValidationExpression="^.{10,12}$" Display="None" ValidationGroup="validation" SetFocusOnError="true"   
                ErrorMessage="Mobile no. should be Minimum of 10 digit or maximum of 12 digit"></asp:RegularExpressionValidator>
     <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ErrorMessage="Mobile no. should be in digits" ValidationExpression="\d+$" ControlToValidate="txtagencycontact" ValidationGroup="validation" Display="None" SetFocusOnError="true" ></asp:RegularExpressionValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator12" runat="server" ControlToValidate="txtagencycontact" ValidationExpression="^.{10,12}$" Display="None" ValidationGroup="validation" SetFocusOnError="true"   
                ErrorMessage="Mobile no. should be Minimum of 10 digit or maximum of 12 digit"></asp:RegularExpressionValidator>
    <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" ErrorMessage=" Invalid e-Mail ID " ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="txtEmailId" Display="None" ValidationGroup="validation" SetFocusOnError="true" ></asp:RegularExpressionValidator>
                          <asp:HiddenField ID="amountBtn1" runat="server"   />
    <asp:HiddenField ID="myHiddenOldProjecttype" runat="server" />
</asp:Content>

