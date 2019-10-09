<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPageRMS.master" AutoEventWireup="true" CodeFile="SeedMoneyView.aspx.cs" Inherits="GrantEntry_SeedMoneyView" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <script src="../Scripts/jquery-1.7.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-1.7.1.min.js" type="text/javascript"></script>
    
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" DisplayMode="BulletList" 
     HeaderText="Validation issues" ShowSummary="False" ValidationGroup="validation" />
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
     }
           
     .auto-style51 {
         width: 416px;
     }
     .auto-style52 {
         width: 158px;
     }
     .auto-style53 {
         height: 44px;
         width: 578px;
     }
     .auto-style54 {
         height: 44px;
         width: 117px;
     }
           
 </style>
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
       function ViewPdf() {
           debugger;
           window.open('<%= Page.ResolveUrl("~/PublicationEntry/DisplayPdf.aspx")%>', '_blank');
         }

</script>
         <center> <asp:Label ID="lablPanelTitlefaculty" runat="server" Text="Seed Money For Faculty Research" Font-Bold="true" Visible-="false"  ></asp:Label></center>
             <center> <asp:Label ID="lablPanelTitlestudent" runat="server" Text="Seed Money For Student Research" Font-Bold="true"  Visible-="false"  ></asp:Label></center>
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

                        <td>   <asp:TextBox ID="txtstitle"  runat="server" Style="border-style:inset none none inset;" width="200px"></asp:TextBox>
               </td>
                         
                       
             
                      <td style="height: 36px;font-weight:normal; color:Black">
                            Seed Money Status</td>
                        <td style="height: 36px">             
                            <asp:DropDownList ID="SeedMoneyStatusSearch" runat="server"   DataSourceID="SqlDataSource2" DataTextField="StatusName" DataValueField="StatusId" AppendDataBoundItems="true">
             <asp:ListItem Value="A">ALL</asp:ListItem>
                           
                          </asp:DropDownList> 
        <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" 
                
                SelectCommand="select StatusId,StatusName from Status_Seedmoney_M" 
                ProviderName="System.Data.SqlClient">
             
            </asp:SqlDataSource>
                       
                         </td>
                         
                        <td style="height: 36px">  
                              <asp:Button ID="Button1" runat="server" Text="Search" 
                                  OnClick="ButtonSearchProjectOnClick" style="height: 26px"  />
                        </td>
             
       </tr>
               
                     </table>  
                     
       <asp:GridView ID="GridViewSearchGrant" runat="server"  AllowPaging="True" 
        PagerSettings-PageButtonCount="5" PageSize="5" DataSourceID="SqlDataSource1" 
        HeaderStyle-BackColor="#CC6600" HeaderStyle-ForeColor="Black" AutoGenerateColumns="false" OnPageIndexChanging="GridViewSearchGrant_PageIndexChanging" 
        PagerStyle-BackColor="#CC6600" PagerStyle-ForeColor="White" CellPadding="3"  OnRowDataBound="GridViewSearchGrant_RowDataBound" OnRowEditing="GridViewSearchGrant_OnRowedit" OnRowCommand="GridViewSearchGrant_RowCommand"
        CellSpacing="3" PagerStyle-Width="4" PagerStyle-Height="4" Visible="False" 
        BorderColor="#FF6600" BorderStyle="Solid" Width="800px" EmptyDataText="No Data found">
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
 </center>
  </asp:Panel>
    <br />
    <asp:Panel ID="panelSearchPub" runat="server" ForeColor="Black"  GroupingText="New Proposal"    Font-Bold="true" Style="background-color:#E0EBAD"  > 
     <table width="100%">
         <tr>
             <td class="auto-style54">
              <asp:Label ID="Label1" runat="server" Text="ID "></asp:Label>   
             </td>
             <td>
                 <asp:TextBox ID="txtid" runat="server" Enabled="false" Width="200px" Height="25px"></asp:TextBox>
             </td>
             
         </tr>
          <tr>
             <td class="auto-style54">
                <asp:Label ID="Label2" runat="server" Text="Title of the Project " Enabled="false"></asp:Label>    
             </td>
              <td colspan="3" class="auto-style20">
                 <asp:TextBox ID="txttiltle" runat="server" Width="600px" Enabled="false" TextMode="MultiLine"></asp:TextBox>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please enter the title" ControlToValidate="txttiltle" ValidationGroup="validation" Display="None" ></asp:RequiredFieldValidator>
             </td>
         </tr>
          <tr>
             <td class="auto-style54">
              <asp:Label ID="Label3" runat="server" Text="Abstract " ></asp:Label>      
             </td>
              <td>
                 <asp:TextBox ID="txtwriteup" runat="server"  Width="600px" Enabled="false" TextMode="MultiLine"></asp:TextBox>
             </td>
               </tr>
          <tr>
             <td class="auto-style54">
                 <asp:Label ID="Label4" runat="server" Text="Category Applied" Enabled="false"></asp:Label>   
             </td>
              <td>
                  <%--  <asp:TextBox ID="Txtappliedbudget" runat="server"  Width="200px" Height="25px" Enabled="false"></asp:TextBox>--%>
     <asp:DropDownList ID="DropDownbudget" runat="server" DataTextField="Amount" DataValueField="BudgetId" DataSourceID="SqlDataSourceBudget" AppendDataBoundItems="True" AutoPostBack="True" Enabled="false" Width="200px" Height="25px">
              <asp:ListItem Selected="True" Value="0" Text="Select"></asp:ListItem>
     </asp:DropDownList>
                  <asp:SqlDataSource ID="SqlDataSourceBudget" runat="server" ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" SelectCommand="SELECT [BudgetId], [Amount] FROM [SeedMoneyBudget_M]"  ></asp:SqlDataSource>

              <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please select the budgetvalue" ControlToValidate="DropDownbudget" ValidationGroup="validation" Display="None" ></asp:RequiredFieldValidator>
               <asp:LinkButton ID="lnkSeedMoneyFaculty" runat="server" Text="Seed Money For Faculty" Style="color: navy; font-weight: bold;font-size:15px" OnClientClick="window.open('http://muportal/doc.aspx?doc=cms/docs/circular/Registrar/Staff/2018.06.07%20Policy%20on%20Seed%20Money%20for%20Faculty%20Research.pdf');" Visible="false" />
                   <asp:LinkButton ID="lnkSeedMoneyStudent" runat="server" Text="Seed Money For Student " Style="color: navy; font-weight: bold;font-size:15px" OnClientClick="window.open('http://muportal/doc.aspx?doc=cms/docs/circular/Registrar/Students/2018.06.07%20Policy%20on%20Seed%20Funding%20for%20UG-PG%20Students%20Research.pdf');" Visible="false" />
                  <br />
              </td>
         </tr>
         <tr>
             <td class="auto-style54">
                <asp:Label ID="Label5" runat="server" Text="Applied Date"></asp:Label>    
             </td>
              <td>
                 <asp:TextBox ID="txtdate" runat="server" Enabled="false" Width="200px" Height="25px"></asp:TextBox>
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
             <td class="auto-style54" >
                <asp:Label ID="Label6" runat="server" Text="Status"></asp:Label>    
             </td>
              <td>
                  <asp:TextBox ID="txtstatus" runat="server"  Width="200px" Height="25px" Enabled="false" ></asp:TextBox>
              </td>
         </tr>
          <tr>
             <td class="auto-style54" >
                <asp:Label ID="lblbudgetapprove" runat="server" Text="Amount Approved" Visible="false"></asp:Label>    
             </td>
              <td class="auto-style51">
               <%-- <asp:DropDownList ID="DropDownListBudgetapprove" runat="server" DataTextField="Amount" DataValueField="BudgetId" DataSourceID="SqlDataSourceBudgetapprove" AppendDataBoundItems="True" AutoPostBack="True" Enabled="false" Width="200px" Height="25px">
              <asp:ListItem Selected="True" Value="0" Text="Select"></asp:ListItem>
     </asp:DropDownList>
                  <asp:SqlDataSource ID="SqlDataSourceBudgetapprove" runat="server" ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" SelectCommand="SELECT [BudgetId], [Amount] FROM [SeedMoneyBudget_M]"  ></asp:SqlDataSource> --%>
                <asp:TextBox ID="Txtapprovedbudget" runat="server" Visible="false" Width="200px" Height="25px" Enabled="false" ></asp:TextBox>
              </td>
         </tr>
         <tr>
             <td class="auto-style54" >
                <asp:Label ID="lblStatus" runat="server" Text="Approved Remarks" Visible="false"></asp:Label>    
             </td>
              <td>
                 <asp:TextBox ID="TextStatus" runat="server" Visible="false" Width="500px" TextMode="MultiLine" Enabled="false" ></asp:TextBox>
             </td>
         </tr>
         
     </table>
</asp:Panel>
    <br />
   
    <asp:Panel ID="panAddAuthor" runat="server" GroupingText="Investigator Details"  ForeColor="Black"  Style="font-weight:bold; border-style:inset;background-color:#E0EBAD" Enabled="false"  > 

              <%--       <center>
          Author Details
                     </center>--%>
                     <br />
                         <table width="92%">                  
                           <tr>
                           <td>
                           <asp:Button ID="BtnAddMU" runat="server" Text="Add New Investigator" onclick="addRow" Enabled="false" Visible="false"/> 
                            
                           </td>
                           </tr>
                           </table>  
  <br />
<asp:Panel ID="PanelMU" runat="server" ScrollBars="both"  Width="1000px" >
  
       <asp:GridView ID="Grid_AuthorEntry" runat="server" AutoGenerateColumns="False" GridLines="None"
                                OnRowDeleting="Grid_AuthorEntry_RowDeleting" OnRowDataBound="OnRowDataBound" CellPadding="4" ForeColor="#333333">

                                <AlternatingRowStyle BackColor="White" />

                                <Columns>

                                    <asp:TemplateField HeaderText="MAHE/Non MAHE">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="DropdownMuNonMu" runat="server" Width="150"  AutoPostBack="true" DataSourceID="SqlDataSourceAuthorType" DataTextField="type" DataValueField="Id">
                                            </asp:DropDownList>
                                            <asp:ImageButton ID="MuNonMu" runat="server" ImageUrl="~/Images/srchImg.gif" CssClass="blnkImgCSS" />
                                            <asp:SqlDataSource ID="SqlDataSourceAuthorType" runat="server" SelectCommand="  select Id,type from Author_Type_M where Id='N'or Id='S'or Id='M' order by Id " SelectCommandType="Text"
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
                                    <asp:CommandField DeleteText="Remove" ShowDeleteButton="True" Visible="false" />

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
     <asp:Panel ID="popupPanelAffil" runat="server" Visible="false" CssClass="modelPopup" Width="460px">

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
    <br />
    <asp:Panel ID="PanelFileUpload" runat="server" GroupingText="File Upload"  ForeColor="Black"  Style="font-weight:bold; border-style:inset;background-color:#E0EBAD"  > 
           <table width="100%">
             <tr>
             <td class="auto-style52" style="font-weight: normal">
                                <asp:Label ID="LabelUploadPfd" runat="server" ForeColor="Black" Text="Upload" Font-Bold="true" > </asp:Label>
                            </td>
                            <td class="auto-style20">
                                <asp:FileUpload ID="FileUploadPdf" runat="server" BorderColor="#996600" BorderStyle="Inset" ClientIDMode="Static"/>
                                 
                               
                                   </td>
                 </tr>
             <tr>
                 <td class="auto-style52">
                                <asp:Label ID="Labeluploadedfiles" runat="server" ForeColor="Black" Text="View Uploaded Files" Font-Bold="true" > </asp:Label>
                                 
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
                 <td class="auto-style52">
                      <asp:Label ID="LabelNote" runat="server" ForeColor="Black" Text="No Files Uploaded" Font-Bold="true" > </asp:Label>
                 </td>
             </tr>
         </table>
     </asp:Panel>
    <br />
     <asp:Panel ID="PanelRemark" runat="server" GroupingText="Comments"  ForeColor="Black"  Style="font-weight:bold; border-style:inset;background-color:#E0EBAD"  > 
         <table width="100%">
             <tr>
                 <td class="auto-style50">
                <asp:Label ID="lblComments" runat="server" Text="Comments "></asp:Label>    
             </td>
              <td colspan="3" class="auto-style20">
                 <asp:TextBox ID="txtComments" runat="server" Width="700px" TextMode="MultiLine" Enabled="false" onkeypress="return this.value.length<1500"></asp:TextBox>
                
             </td>
             </tr>
         </table>
     </asp:Panel>
    <asp:Panel ID="Panel3" runat="server" GroupingText="Cancel Comments"  ForeColor="Black"  Style="font-weight:bold; border-style:inset;background-color:#E0EBAD" Visible="false"  > 
         <table width="100%">
             <tr>
                 <td class="auto-style50">
                <asp:Label ID="Label7" runat="server" Text="Comments"></asp:Label>    
             </td>
              <td colspan="3" class="auto-style20">
                 <asp:TextBox ID="txtcancelComments" runat="server" Width="700px" TextMode="MultiLine" onkeypress="return this.value.length<1500"></asp:TextBox>
                
             </td>
             </tr>
         </table>
     </asp:Panel>
         <asp:HiddenField ID="rowVal" runat="server" />
    <asp:HiddenField ID="senderID" runat="server" />
</asp:Content>

