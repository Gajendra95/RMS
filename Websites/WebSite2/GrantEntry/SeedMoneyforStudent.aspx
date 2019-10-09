<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPageRMS.master" AutoEventWireup="true" CodeFile="SeedMoneyforStudent.aspx.cs" Inherits="GrantEntry_SeedMoneyforStudent" MaintainScrollPositionOnPostback="true" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script src="../Scripts/jquery-1.7.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-1.7.1.min.js" type="text/javascript"></script>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" DisplayMode="BulletList" 
     HeaderText="Validation issues" ShowSummary="False" ValidationGroup="validation" />
     <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="True" DisplayMode="BulletList" 
     HeaderText="Validation issues" ShowSummary="False" ValidationGroup="validation1" />
     <asp:ValidationSummary ID="ValidationSummary3" runat="server" ShowMessageBox="True" DisplayMode="BulletList" 
     HeaderText="Validation issues" ShowSummary="False" ValidationGroup="validation2" />
        <script type="text/javascript">

            function setRow(obj) {

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
 <style type="text/css">
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
     .auto-style50 {
         height: 44px;
         width:200px;
     }
           
     .auto-style51 {
         width: 292px;
     }
           
     .auto-style53 {
         height: 44px;
         width: 104px;
     }
           
     .auto-style54 {
         width: 159px;
     }
           
 </style>
    <%-- <script type = "text/javascript">
         function setRow(obj) {
             var row = obj.parentNode.parentNode;
             var rowIndex = row.rowIndex - 1;
             var sndID = obj.id;
             var sndrID = document.getElementById('<%= senderID.ClientID %>');
             sndrID.value = sndID;
             var rowNo = document.getElementById('<%= rowVal.ClientID %>');
           rowNo.value = rowIndex;
       }
       function ViewPdf() {
           debugger;
           window.open('<%= Page.ResolveUrl("~/PublicationEntry/DisplayPdf.aspx")%>', '_blank');
         }

</script>--%>
    <center> <asp:Label ID="lablPanelTitle" runat="server" Text="Seed Money For Student Research" Font-Bold="true"  ></asp:Label></center>
            <br />
   <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="true"></asp:ToolkitScriptManager>
    <asp:Panel ID="panel2" runat="server" ForeColor="Black"  GroupingText="Search For Existing Proposal"    Font-Bold="true" Style="background-color:#E0EBAD" > 
 <center>
 <table  style="width: 92%">
    <tr>
            
                        <td style="height: 36px; font-weight:normal; color:Black">
                           ID
                        </td>
                        <td style="height: 36px">
                          <asp:TextBox ID="txtSid"  runat="server" Style="border-style:inset none none inset;" ></asp:TextBox>
                        </td>

                          <td style="height: 36px; font-weight:normal; color:Black">
                          Title of the Project
                        </td>

                        <td>   <asp:TextBox ID="txtstitle"  runat="server" Style="border-style:inset none none inset;" width="340px"></asp:TextBox>
               </td>
                         
                        <td style="height: 36px">  
                              <asp:Button ID="ButtonSearchProject" runat="server" Text="Search" 
                                  OnClick="ButtonSearchProjectOnClick" style="height: 26px"  />
                        </td>
             
                      
       </tr>
               
                     </table>  
                     <br />
     <asp:Label ID="lblNote" runat="server" Text="Note - Reception of application for Seed money proposal for this quarter is closed. Request you to apply for the next call during Oct 2018." ForeColor="Black" Visible="false"></asp:Label> 
       <br />
       <asp:GridView ID="GridViewSearchGrant" runat="server"  AllowPaging="True" 
        PagerSettings-PageButtonCount="5" PageSize="5" DataSourceID="SqlDataSource1" 
        HeaderStyle-BackColor="#CC6600" HeaderStyle-ForeColor="Black" AutoGenerateColumns="false" OnPageIndexChanging="GridViewSearchGrant_PageIndexChanging" 
        PagerStyle-BackColor="#CC6600" PagerStyle-ForeColor="White" CellPadding="3"  OnRowDataBound="GridViewSearchGrant_RowDataBound" OnRowEditing="GridViewSearchGrant_OnRowedit" OnRowCommand="GridViewSearchGrant_RowCommand"
        CellSpacing="3" PagerStyle-Width="4" PagerStyle-Height="4" Visible="False" 
        BorderColor="#FF6600" BorderStyle="Solid" Width="800px">
        <Columns>  
   <asp:TemplateField ShowHeader="False" >
   <ItemTemplate>
   <asp:ImageButton ID="BtnEdit" runat="server" CausesValidation="False" CommandName="Edit" ImageUrl="~/Images/edit.gif" ToolTip="Edit"  />
   </ItemTemplate>
   </asp:TemplateField>
        <asp:BoundField DataField="ID" ReadOnly="true" HeaderText="ID" 
                SortExpression="ID" />
                  <asp:BoundField DataField="Title" ReadOnly="true" HeaderText="Title" 
                SortExpression="Title" ItemStyle-Width="400px"/>
           <asp:BoundField DataField="Writeup" ReadOnly="true" HeaderText="Writeup" 
                SortExpression="Writeup" ItemStyle-Width="400px" Visible="false" />
                  <asp:BoundField DataField="StatusName" ReadOnly="true" HeaderText="Status" 
                SortExpression="Status"  ItemStyle-Width="200px" />
             <asp:BoundField DataField="EntryType" ReadOnly="true" HeaderText="EntryType" 
                SortExpression="Status" />
   <asp:TemplateField ShowHeader="False">
   <ItemTemplate>
   <asp:HiddenField ID="hiddenID" runat="server" Value='<%# Eval("ID") %>'/>
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
<%--    <asp:GridView ID="GridViewsanSearch" runat="server"  AllowPaging="True" 
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

    </asp:GridView>
      <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
         ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" 
         SelectCommand=""  >
        </asp:SqlDataSource>--%>
 </center>
  </asp:Panel>
    <br />
    <asp:Panel ID="panelSearchPub" runat="server" ForeColor="Black"  GroupingText="New Proposal"    Font-Bold="true" Style="background-color:#E0EBAD" > 
     <table width="100%">
         <tr>
             <td  class="auto-style53">
              <asp:Label ID="Label1" runat="server" Text="ID "></asp:Label>   
             </td>
             <td  class="auto-style51">
                 <asp:TextBox ID="txtid" runat="server" Enabled="false" Width="200px" Height="25px"></asp:TextBox>
             </td>
             
            </tr>
         <tr>
             <td  class="auto-style53">
                <asp:Label ID="Label2" runat="server"  Text="Title of the Project "></asp:Label>    
             </td>
              <td  class="auto-style51">
                 <asp:TextBox ID="txttiltle" runat="server" Width="600px" TextMode="MultiLine"></asp:TextBox>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please enter the title" ControlToValidate="txttiltle" ValidationGroup="validation" Display="None" ></asp:RequiredFieldValidator>
             </td>
         </tr>
          <tr>
             <td class="auto-style53">
              <asp:Label ID="Label3" runat="server" Text="Abstract " ></asp:Label>      
             </td>
              <td class="auto-style51">
                 <asp:TextBox ID="txtwriteup" runat="server"  Width="600px" TextMode="MultiLine"></asp:TextBox>
             </td>
               </tr>
          <tr>
             <td class="auto-style53">
                 <asp:Label ID="Label4" runat="server" Text="Category Applied "></asp:Label>   
             </td>
              <td class="auto-style53">
               <%--    <asp:TextBox ID="Txtappliedbudget" runat="server"  Width="200px" Height="25px"></asp:TextBox>--%>
     <asp:DropDownList ID="DropDownbudget" runat="server" DataTextField="Amount" DataValueField="BudgetId" DataSourceID="SqlDataSourceBudget" AppendDataBoundItems="True"  Width="200px" Height="25px">
              <asp:ListItem Selected="True" Value="0" Text="Select"></asp:ListItem>
     </asp:DropDownList>
                  <asp:SqlDataSource ID="SqlDataSourceBudget" runat="server" ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" SelectCommand="SELECT [BudgetId], [Amount] FROM [SeedMoneyBudget_M]"  ></asp:SqlDataSource>

              <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please select the budgetvalue" ControlToValidate="DropDownbudget" ValidationGroup="validation" Display="None" ></asp:RequiredFieldValidator>
              
               <asp:LinkButton ID="lnkSeedMoneyStudent" runat="server" Text="Seed Money For Student " Style="color: navy; font-weight: bold;font-size:15px" OnClientClick="window.open('http://muportal/doc.aspx?doc=cms/docs/circular/Registrar/Students/2018.06.07%20Policy%20on%20Seed%20Funding%20for%20UG-PG%20Students%20Research.pdf');" />
             </td>
         </tr>
         <tr>
             <td class="auto-style53">
                <asp:Label ID="Label5" runat="server" Text="Applied Date "  ></asp:Label>    
             </td>
              <td class="auto-style51">
                 <asp:TextBox ID="txtdate" runat="server" Width="200px" Height="25px" Enabled="false"></asp:TextBox>
                   <asp:CalendarExtender ID="CalendarExtender5" runat="server"
                TargetControlID="txtdate" Format="dd/MM/yyyy" >
            </asp:CalendarExtender>

                <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" Display="None"  ControlToValidate="txtdate" ValidationGroup="validation"
                ErrorMessage="Presentation Date in (dd/mm/yyyy) format" SetFocusOnError="true"  
                ValidationExpression="^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$">

                </asp:RegularExpressionValidator>
                   <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please enter the date" ControlToValidate="txtdate" ValidationGroup="validation" Display="None" ></asp:RequiredFieldValidator>
             </td>
         </tr>
         <tr>
             <td class="auto-style53" >
                <asp:Label ID="Label6" runat="server" Text="Status"></asp:Label>    
             </td>
              <td class="auto-style51">
                    <asp:TextBox ID="txtstatus" runat="server"  Width="200px" Height="25px" Enabled="false" ></asp:TextBox>
                <%-- <asp:DropDownList ID="DropDownListseedStatus" runat="server" DataSourceID="SqlDataSourceseedStatus" DataTextField="StatusName" DataValueField="StatusId"  AutoPostBack="true" AppendDataBoundItems="true" OnSelectedIndexChanged="DropDownListseedStatus_SelectedIndexChanged" Width="200px" Height="25px">
                    <asp:ListItem Selected="True" Value="0" Text="Select"></asp:ListItem>
                 </asp:DropDownList>
        <asp:SqlDataSource ID="SqlDataSourceseedStatus" runat="server" ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" 
           SelectCommand="select StatusId,StatusName from Status_Seedmoney_M where StatusId='APP'or StatusId='REJ' or StatusId='REW' or StatusId='SUB' or StatusId='NEW'">
 </asp:SqlDataSource>   --%>  
             </td>
         </tr>
         <tr>
             <td class="auto-style53" >
                <asp:Label ID="lblbudgetapprove" runat="server" Text="Amount Approved" Visible="false"></asp:Label>    
             </td>
              <td class="auto-style51">
                    <asp:TextBox ID="Txtapprovedbudget" runat="server" Visible="false" Width="200px" Height="25px"  ></asp:TextBox>
               <%-- <asp:DropDownList ID="DropDownListBudgetapprove" runat="server" DataTextField="Amount" DataValueField="BudgetId" DataSourceID="SqlDataSourceBudgetapprove" AppendDataBoundItems="True"  Width="200px" Height="25px">
              <asp:ListItem Selected="True" Value="0" Text="Select"></asp:ListItem>
     </asp:DropDownList>
                  <asp:SqlDataSource ID="SqlDataSourceBudgetapprove" runat="server" ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" SelectCommand="SELECT [BudgetId], [Amount] FROM [SeedMoneyBudget_M]"  ></asp:SqlDataSource> --%>
             </td>
         </tr>
         <tr>
             <td class="auto-style53" >
                <asp:Label ID="lblAPPremarks" runat="server" Text="Remarks" Visible="false"></asp:Label>    
             </td>
              <td class="auto-style51">
                 <asp:TextBox ID="TextRemarks" runat="server" Visible="false" Width="500px" TextMode="MultiLine" ></asp:TextBox>
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
<asp:Panel ID="PanelMU" runat="server" ScrollBars="both"  Width="1000px" >
  
     <%--<asp:GridView ID="Grid_AuthorEntry" runat="server" AutoGenerateColumns="False"  GridLines="None" 
     OnRowDeleting="Grid_AuthorEntry_RowDeleting" CellPadding="4" ForeColor="#333333" >

     <AlternatingRowStyle BackColor="White" />
     <Columns>

        <asp:TemplateField HeaderText="MAHE/Non MAHE">
             <ItemTemplate>
                  <asp:DropDownList ID="DropdownMuNonMu" runat="server" Width="150" OnSelectedIndexChanged="DropdownMuNonMuOnSelectedIndexChanged" AutoPostBack="true" DataSourceID="SqlDataSourceAuthorType" DataTextField="DisplayName" DataValueField="Id">
                                            </asp:DropDownList>
                 <asp:ImageButton ID="MuNonMu" runat="server" ImageUrl="~/Images/srchImg.gif" CssClass="blnkImgCSS"  />              
                    <asp:SqlDataSource ID="SqlDataSourceAuthorType" runat="server" SelectCommand="SelectAuthorType" SelectCommandType="StoredProcedure"
                                                ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>"
                                                ProviderName="System.Data.SqlClient"></asp:SqlDataSource>
             </ItemTemplate>              
        </asp:TemplateField>

        <asp:TemplateField ShowHeader="false">
             <ItemTemplate>
                 <asp:HiddenField ID="EmployeeCode" runat="server"  ></asp:HiddenField>
       
            </ItemTemplate>            
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Investigator Name">
             <ItemTemplate>
                 <asp:TextBox ID="AuthorName" runat="server" Width="200" Enabled="false" OnTextChanged="AuthorName_Changed" AutoPostBack="true"></asp:TextBox>
          
                 <asp:ImageButton ID="EmployeeCodeBtn" runat="server" ImageUrl="~/Images/srchImg.gif" OnClientClick="setRow(this)" />

                 <asp:ModalPopupExtender ID="ModalPopupExtender4" runat="server" TargetControlID="EmployeeCodeBtn" PopupControlID="popupPanelAffil"
                    BackgroundCssClass="modelBackground"  >
                                  </asp:ModalPopupExtender>
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

        <asp:TemplateField HeaderText="Department Name/Course">
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
    </asp:GridView>--%>
    <asp:GridView ID="Grid_AuthorEntry" runat="server" AutoGenerateColumns="False" GridLines="None"
                                OnRowDeleting="Grid_AuthorEntry_RowDeleting" OnRowDataBound="OnRowDataBound" CellPadding="4" ForeColor="#333333">

                                <AlternatingRowStyle BackColor="White" />

                                <Columns>

                                    <asp:TemplateField HeaderText="MAHE/Non MAHE">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="DropdownMuNonMu" runat="server" Width="150" OnSelectedIndexChanged="DropdownMuNonMu_SelectedIndexChanged" AutoPostBack="true" DataSourceID="SqlDataSourceAuthorType" DataTextField="type" DataValueField="Id">
                                            </asp:DropDownList>
                                            <asp:ImageButton ID="MuNonMu" runat="server" ImageUrl="~/Images/srchImg.gif" CssClass="blnkImgCSS" />
                                            <asp:SqlDataSource ID="SqlDataSourceAuthorType" runat="server" SelectCommand="select Id,type from Author_Type_M where Id='N'or Id='S'or Id='M' order by Id desc" SelectCommandType="Text"
                                                ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>"
                                                ProviderName="System.Data.SqlClient"></asp:SqlDataSource>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField ShowHeader="true" HeaderText="Roll No/Employee Code">
                                        <ItemTemplate>
                                            <asp:TextBox ID="EmployeeCode" runat="server" Width="155" Visible="true" MaxLength="9"></asp:TextBox>
                                            <asp:Image ID="ImageEmpCode" runat="server" ImageUrl="~/Images/srchImg.gif" CssClass="blnkImgCSS" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Author Name">
                                        <ItemTemplate>
                                            <asp:TextBox ID="AuthorName" runat="server" Width="200" Enabled="false" OnTextChanged="AuthorName_Changed" AutoPostBack="true"></asp:TextBox>

                                            <asp:ImageButton ID="EmployeeCodeBtn" runat="server" ImageUrl="~/Images/srchImg.gif" OnClientClick="setRow(this)" />

                                            <asp:ModalPopupExtender ID="ModalPopupExtender4" runat="server" TargetControlID="EmployeeCodeBtn" PopupControlID="popup"
                                                BackgroundCssClass="modelBackground">
                                            </asp:ModalPopupExtender>
                                            <asp:ImageButton ID="ImageButton1" runat="server" Visible="false" ImageUrl="~/Images/srchImg.gif" OnClientClick="setRow(this)" />
         <asp:HiddenField ID="test3" runat="server" />

                                            <asp:ModalPopupExtender ID="ModalPopupExtender2" runat="server" TargetControlID="test3" PopupControlID="popup"
                                                BackgroundCssClass="modelBackground">
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
                                            <asp:TextBox ID="InstitutionName" runat="server" Width="150" Enabled="false"></asp:TextBox>
                                            <asp:DropDownList ID="DropdownStudentInstitutionName" runat="server" Width="150" Visible="false" AutoPostBack="true" DataSourceID="SqlDataSourceDropdownStudentInstitutionName" DataTextField="Institute_Name" DataValueField="Institute_Id">
                                            </asp:DropDownList>
                                            <asp:SqlDataSource ID="SqlDataSourceDropdownStudentInstitutionName" runat="server"
                                                ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>"
                                                ProviderName="System.Data.SqlClient"
                                                SelectCommand="select Institute_Id,Institute_Name from Institute_M"></asp:SqlDataSource>
                                            <asp:Image ID="Image4" runat="server" ImageUrl="~/Images/srchImg.gif" CssClass="blnkImgCSS" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Department Name/Course">
                                        <ItemTemplate>
                                            <asp:TextBox ID="DepartmentName" runat="server" Width="180" Enabled="false"></asp:TextBox>
                                            <asp:DropDownList ID="DropdownStudentDepartmentName" runat="server" Width="180" Visible="false" DataSourceID="SqlDataSourceDropdownStudentDepartmentName" DataTextField="DeptName" DataValueField="DeptId">
                                            </asp:DropDownList>

                                            <asp:SqlDataSource ID="SqlDataSourceDropdownStudentDepartmentName" runat="server"
                                                ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>"
                                                ProviderName="System.Data.SqlClient"
                                                SelectCommand="select DeptId,DeptName from Dept_M where Institute_Id=@Institute_Id ">
                                                <SelectParameters>
                                                    <%-- <asp:QueryStringParameter Name="Institute_Id" QueryStringField="Institute_Id" />--%>
                                                    <asp:ControlParameter Name="Institute_Id"
                                                        ControlID="DropdownStudentInstitutionName"
                                                        PropertyName="SelectedValue" />
                                                </SelectParameters>
                                            </asp:SqlDataSource>
                                            <asp:Image ID="Image5" runat="server" ImageUrl="~/Images/srchImg.gif" CssClass="blnkImgCSS" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="MailId">
                                        <ItemTemplate>
                                            <asp:TextBox ID="MailId" runat="server" Width="200" Enabled="false"></asp:TextBox>

                                            <asp:Image ID="ImageMailId" runat="server" ImageUrl="~/Images/srchImg.gif" CssClass="blnkImgCSS" />

                                            <asp:RegularExpressionValidator
                                                ID="regEmail"
                                                ControlToValidate="MailId" Display="Static" ErrorMessage="Invalid Email Id"
                                                Text="(Invalid email)" ValidationGroup="validation"
                                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                runat="server" />
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Colloboration">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="NationalType" runat="server" Width="140" OnSelectedIndexChanged="NationalTypeOnSelectedIndexChanged" AutoPostBack="true">
                                                <asp:ListItem Value="N">National</asp:ListItem>
                                                <asp:ListItem Value="I">International</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:Image ID="ImageisNationalType" runat="server" ImageUrl="~/Images/srchImg.gif" CssClass="blnkImgCSS" />

                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Continent">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ContinentId" runat="server" Width="140" DataSourceID="SqlDataSourceDropdownContinentId" DataTextField="ContinentName" DataValueField="ContinentId">
                                            </asp:DropDownList>
                                            <asp:SqlDataSource ID="SqlDataSourceDropdownContinentId" runat="server"
                                                ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>"
                                                ProviderName="System.Data.SqlClient"
                                                SelectCommand="select ContinentId,ContinentName  from Continent_M "></asp:SqlDataSource>
                                            <asp:Image ID="ImageisContinent" runat="server" ImageUrl="~/Images/srchImg.gif" CssClass="blnkImgCSS" />
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField ShowHeader="false">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="Institution" runat="server"></asp:HiddenField>
                                            <asp:ImageButton ID="InstitutionBtn" runat="server" Visible="false" ImageUrl="~/Images/srchImg.gif" CssClass="blnkImgCSS" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ShowHeader="false">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="Department" runat="server"></asp:HiddenField>
                                            <asp:ImageButton ID="DepartmentBtn" runat="server" Visible="false" ImageUrl="~/Images/srchImg.gif" CssClass="blnkImgCSS" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:CommandField DeleteText="Remove" ShowDeleteButton="True" />

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
   <%--  <asp:Panel ID="popupPanelAffil" runat="server" Visible="false" CssClass="modelPopup" Width="460px">--%>

<%--<table style="background:white">
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
</table>--%>
    <asp:Panel ID="popup" runat="server" Visible="false" CssClass="modelPopup" Style="width: 1000px; height: 400px; background-color: ghostwhite;">
                <asp:Panel ID="popupstudent" runat="server" >

                    <center>
                        <table align="center">
                            <tr>
                                <th>Student Details </th>
                            </tr>
                        </table>
                        <br />
                        <table>
                            <tr>
                                <td>Name:</td>
                                <td>
                                    <asp:TextBox ID="txtSrchStudentName" runat="server"></asp:TextBox></td>
                                <td></td>
                                <td>Roll No:</td>
                                <td>
                                    <asp:TextBox ID="txtSrchStudentRollNo" runat="server"></asp:TextBox></td>
                                <td>Institution:</td>
                                <td>
                                    <asp:DropDownList ID="StudentIntddl" DataSourceID="sqlstudentds" DataTextField="InstName" DataValueField="InstID" runat="server" AppendDataBoundItems="true">
                                        <asp:ListItem Value="">--Select--</asp:ListItem>
                                    </asp:DropDownList></td>
                            </tr>
                        </table>
                        <br />
                        <table align="center">
                            <tr>
                                <td>
                                    <asp:Button ID="Button1" runat="server" Text="Search" OnClick="SearchStudentData" /></td>
                                <td>
                                    <asp:Button ID="Button2" runat="server" Text="EXIT" OnClick="exit" />
                                </td>
                            </tr>
                        </table>
                        <br />
                        <table>
                            <tr>
                                <td>
                                    <asp:GridView ID="popupStudentGrid" runat="server" AutoGenerateSelectButton="true" EmptyDataText="No Records Found" GridLines="Both"
                                        OnSelectedIndexChanged="StudentDataSelect" AllowSorting="true" AutoGenerateColumns="false" CellPadding="5" CellSpacing="5">
                                        <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
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
                                                SortExpression="RollNo" ItemStyle-Width="100px" />
                                            <asp:BoundField DataField="Name" HeaderText="Name" ReadOnly="True"
                                                SortExpression="Name" ItemStyle-Width="500px" />
                                            <asp:BoundField DataField="InstName" HeaderText="Institution" ReadOnly="True"
                                                SortExpression="InstName" ItemStyle-Width="500px" />
                                            <asp:BoundField DataField="ClassName" HeaderText="Class" ReadOnly="True"
                                                SortExpression="ClassName" ItemStyle-Width="500px" />
                                            <asp:BoundField DataField="EmailID1" HeaderText="Email" ReadOnly="True"
                                                SortExpression="EmailID1" ItemStyle-Width="500px" />


                                            <asp:TemplateField ShowHeader="false">
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
                                            <asp:SessionParameter Name="UserId" SessionField="User" Type="String" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                    <asp:SqlDataSource ID="sqlstudentds" runat="server" ConnectionString="<%$ ConnectionStrings:SISConStr %>"
                                        SelectCommand="Select InstName,InstID from SISInstitution" ProviderName="System.Data.SqlClient"></asp:SqlDataSource>
                                </td>
                            </tr>
                        </table>
                    </center>
                </asp:Panel>

        <%--<asp:UpdatePanel ID="popupPanelAffilUpdate" UpdateMode="Conditional" runat="server" Visible="true">
                    <ContentTemplate>--%>
                        <asp:Panel ID="popupPanelAffil" runat="server">
                            <br />
                            <center>
                                <table style="background: white">
                                    <tr>
                                        <th align="center" colspan="3">Author </th>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>Search By Name:
                                            <asp:TextBox ID="affiliateSrch" runat="server"></asp:TextBox>


                                            <asp:Button ID="BtnEmployeeSearch" runat="server" Text="Search" OnClick="branchNameChanged" />

                                            <asp:Button ID="Buttonexit4" runat="server" Text="EXIT" OnClick="exit1" />

                                        </td>

                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <asp:GridView ID="popGridAffil" runat="server" AutoGenerateSelectButton="true" EmptyDataText="No User Found" GridLines="Both"
                                                OnSelectedIndexChanged="popSelected1" AllowSorting="true" AutoGenerateColumns="false" Height="215px" CellPadding="5" CellSpacing="5">
                                                <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                                                <HeaderStyle BackColor="#0b532d" Font-Bold="True" ForeColor="White" />
                                                <PagerStyle ForeColor="#000000" HorizontalAlign="Center" />
                                                <RowStyle BackColor="#FFFFFF" ForeColor="#000000" />
                                                <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
                                                <SortedAscendingCellStyle BackColor="#FFF1D4" />
                                                <SortedAscendingHeaderStyle BackColor="#B95C30" />
                                                <SortedDescendingCellStyle BackColor="#F1E5CE" />
                                                <SortedDescendingHeaderStyle BackColor="#93451F" />
                                                <Columns>
                                                    <asp:BoundField DataField="User_Id" HeaderText="User Id" ReadOnly="True"
                                                        SortExpression="User_Id" ItemStyle-Width="200px" />
                                                    <asp:BoundField DataField="Name" HeaderText="Name" ReadOnly="True"
                                                        SortExpression="Name" ItemStyle-Width="500px" />
                                                </Columns>
                                            </asp:GridView>



                                            <asp:SqlDataSource ID="SqlDataSourceAffil" runat="server" ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>"
                                                SelectCommand="SELECT User_Id,prefix+' '+firstname+' '+middlename+' '+lastname  as Name,1 flag from User_M where User_Id=@UserId  UNION SELECT top 10  User_Id, prefix+' '+firstname+' '+middlename+' '+lastname  as Name, 2 flag from User_M  where Active='Y' order by flag" ProviderName="System.Data.SqlClient">
                                                <SelectParameters>
                                                    <asp:SessionParameter Name="UserId" SessionField="User" Type="String" />
                                                </SelectParameters>
                                            </asp:SqlDataSource>

                                        </td>
                                    </tr>
                                </table>
                            </center>
                            <br />
                        </asp:Panel>
                    <%--</ContentTemplate>
                </asp:UpdatePanel>--%>
        </asp:Panel>
<%--</asp:Panel>--%>
    <br />
     <asp:Panel ID="PanelFileUpload" runat="server" GroupingText="File Upload"  ForeColor="Black"  Style="font-weight:bold; border-style:inset;background-color:#E0EBAD"  > 
         <table width="100%">
             <tr>
             <td class="auto-style54" style="font-weight: normal">
                                <asp:Label ID="LabelUploadPfd" runat="server" ForeColor="Black" Text="Upload" Font-Bold="true" > </asp:Label>
                            </td>
                            <td class="auto-style20">
                                <asp:FileUpload ID="FileUploadPdf" runat="server" BorderColor="#996600" BorderStyle="Inset" ClientIDMode="Static"/>
                                  <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Please choose the file" ControlToValidate="FileUploadPdf" ValidationGroup="validation2" Display="None" ></asp:RequiredFieldValidator>
                                <asp:Button ID="BtnUpload" runat="server" Text="Upload" OnClick="BtnUpload_Click" ValidationGroup="validation2" />
                                   </td>
                 </tr>
             <tr>
                 <td class="auto-style54">
                                <asp:Label ID="Labeluploadedfiles" runat="server" ForeColor="Black" Text="View Uploaded Files" Font-Bold="true" Visible="false" > </asp:Label>
                                 
                                </td>
                 <td>
                     <asp:GridView ID="GVViewFile" runat="server" AutoGenerateColumns="False" BorderStyle="Solid" CellPadding="3" CellSpacing="3" DataSourceID="DSforgridview" HeaderStyle-ForeColor="White" OnSelectedIndexChanged="GVViewFile_SelectedIndexChanged" PagerStyle-ForeColor="White" PagerStyle-Height="4" PagerStyle-Width="4">
                                    <Columns>
                                        <asp:TemplateField ShowHeader="false">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnView" runat="server" CausesValidation="False" CommandName="Select" ImageUrl="~/Images/view.gif" ToolTip="View File" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgetid" runat="server" Text='<%# Eval("UploadFilePath") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle BackColor="#0b532d" Font-Bold="True" />
                                </asp:GridView>
                                <asp:SqlDataSource ID="DSforgridview" runat="server" ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" SelectCommand="  "></asp:SqlDataSource>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="FileUploadPdf" ErrorMessage="!Upload only PDF(.pdf,.PDF) file" ForeColor="Red" ValidationExpression="^.*\.((p|P)(d|D)(f|F))$" ValidationGroup="validation"></asp:RegularExpressionValidator>

                            </td>
         </tr>
             <tr>
                 <td class="auto-style54">
                      <asp:Label ID="LabelNote" runat="server" ForeColor="Black" Text="No Files Uploaded" Font-Bold="true" Visible="false" > </asp:Label>
                 </td>
             </tr>
              <tr>
                            <td colspan="6"> <asp:Label ID="LabelNote2" runat="server" ForeColor="Black" Text="Upload only PDF files and the file size has to be less than or equal to 10MB." Font-Bold="true"  > </asp:Label><br /> </td>
                        </tr>
         </table>
     </asp:Panel>

     <asp:Panel ID="PanelRemark" runat="server" GroupingText="Comments"  ForeColor="Black"  Style="font-weight:bold; border-style:inset;background-color:#E0EBAD"  > 
         <table width="100%">
             <tr>
                 <td class="auto-style50">
                <asp:Label ID="lblComments" runat="server" Text="Comments "></asp:Label>    
             </td>
              <td colspan="3" class="auto-style20">
                 <asp:TextBox ID="txtComments" runat="server" Width="700px" TextMode="MultiLine" onkeypress="return this.value.length<1500"></asp:TextBox>
                
             </td>
             </tr>
         </table>
     </asp:Panel>
     <asp:Panel ID="Panel1" runat="server" GroupingText=""  ForeColor="Black"  Style="font-weight:bold; border-style:inset;background-color:#E0EBAD"  > 
   <table width="100%">
    <tr>
<td align="center">
  <asp:Button ID="BtnSave" runat="server" Text="Save" OnClick="BtnSave_Click"  CausesValidation="true" ValidationGroup="validation" Visible="false"  ></asp:Button>
    <asp:Button ID="BtnSubmit" runat="server" Text="Submit" OnClick="BtnSubmit_Click" CausesValidation="true" Visible="false" ></asp:Button>
 <asp:Button ID="BtnApprove" runat="server" Text="Approve" OnClick="BtnApprove_Click"  CausesValidation="true" Visible="false"  ></asp:Button>
 <asp:Button ID="BtnRework" runat="server" Text="Rework" OnClick="BtnRework_Click" CausesValidation="false" Visible="false" ></asp:Button>
     <asp:Button ID="BtnReject" runat="server" Text="Reject" OnClick="BtnReject_Click"  CausesValidation="true" Visible="false"  ></asp:Button>

</td>
 </tr>
</table>
          
          </asp:Panel>
        <asp:HiddenField ID="rowVal" runat="server" />
    <asp:HiddenField ID="senderID" runat="server" />
</asp:Content>

