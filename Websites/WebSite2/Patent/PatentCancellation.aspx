<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPageRMS.master" AutoEventWireup="true" CodeFile="PatentCancellation.aspx.cs" Inherits="GrantEntry_PatentCancellation" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

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
               

            }
            else if (mu == "S") {
              
               $('#<%=popupPanelAffil.ClientID %>').hide();

           }
   }
    </script>
    <%--<script type = "text/javascript">
        function setRow(obj) {
            var row = obj.parentNode.parentNode;
            var rowIndex = row.rowIndex - 1;
            var sndID = obj.id;
            var sndrID = document.getElementById('<%= senderID.ClientID %>');
           sndrID.value = sndID;
           var rowNo = document.getElementById('<%= rowVal.ClientID %>');
           rowNo.value = rowIndex;
       }
    </script>--%>
    <script type="text/javascript" language="JavaScript">
        function Validate(sender, args) {
            var txt1 = document.getElementById("<%= PatIDSearch.ClientID %>");
            var txt2 = document.getElementById("<%= TextBoxtiltleSearch.ClientID%>");

            args.IsValid = (txt1.value != "") || (txt2.value != "");
        }
</script>
     <style>   
    /*Calendar Control CSS*/
  .cal_Theme1 .ajax__calendar_days table tr td, .ajax__calendar_months table tr td, 
   cal_Theme1 .ajax__calendar_years table tr td {
   padding:0px;
   margin: 20px;
   }
   .cal_Theme1 .ajax__calendar_container   {
   background-color: #fff;
   border:solid 1px #808080;
   }
  .cal_Theme1 .ajax__calendar_day {

   text-align:center;
  }
      .style2
      {
          width: 139px;
      }
      .style3
      {
          width: 215px;
      }
  </style> 
    <style type="text/css">
      
.modalBackground {
            background-color: Gray;
            filter: alpha(opacity=50);
            opacity: 0.7;
        }

        .modalPopup {
            background-color: #FFFFFF;
            border: 3px solid #CCC;
            padding-top: 10px;
            padding-left: 10px;
            width: 300px;
            height: 140px;
            clear: both;
            position: absolute;
            margin-top: 0;
}
        .auto-style1 {
            width: 197px;
        }
        .auto-style2 {
            width: 219px;
        }
        .auto-style4 {
        }
        .auto-style5 {
            width: 196px;
        }
        .auto-style8 {
            width: 284px;
        }
        .auto-style9 {
            width: 273px;
        }
        .auto-style11 {
            width: 270px;
        }
        .auto-style12 {
            width: 269px;
        }
        .auto-style13 {
            width: 280px;
        }
        .auto-style15 {
            width: 158px;
        }
        .auto-style16 {
            width: 223px;
        }
        .auto-style17 {
            width: 221px;
        }
        .auto-style18 {
            width: 276px;
        }
        .auto-style19 {
            width: 192px;
        }
        .auto-style20 {
            width: 301px;
        }
        .auto-style21 {
            width: 302px;
        }
        .auto-style23 {
            width: 364px;
        }
        .auto-style24 {
            width: 199px;
        }
        .auto-style25 {
            width: 197px;
            height: 26px;
        }
    </style>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="true"></asp:ToolkitScriptManager>
    <asp:Panel ID="Panel4" runat="server">
    <asp:Panel ID="Panel2" runat="server">
        <div style="font-weight:bold; text-align:center; font-size:20px">
            Patent Cancellation
        </div>
    </asp:Panel>
    <br />
    <asp:Panel ID="Panel3" runat="server"  BorderStyle="Inset"   Font-Bold="true" Style="background-color:#E0EBAD">
        <table style="margin-left:200px">
            <tr>
                 <td style="height: 36px; font-weight:normal; color:Black">
                           Patent ID
                        </td>
                        <td style="height: 36px">
                          <asp:TextBox ID="PatIDSearch"  runat="server" Style="border-style:inset none none inset;" ></asp:TextBox>
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
        <div style="margin-left:230px">

       
        <asp:GridView ID="GridViewSearchPatent" runat="server"  AllowPaging="True" 
        PagerSettings-PageButtonCount="5" PageSize="5" DataSourceID="SqlDataSource1" 
        HeaderStyle-BackColor="#CC6600" HeaderStyle-ForeColor="Black" AutoGenerateColumns="False" OnPageIndexChanging="GridViewSearchPatent_PageIndexChanging" 
        PagerStyle-BackColor="#CC6600" PagerStyle-ForeColor="White" CellPadding="3"  OnRowDataBound="GridViewSearchPatent_RowDataBound" OnRowEditing="GridViewSearchPatent_OnRowedit" OnRowCommand="GridViewSearchPatent_RowCommand"
        CellSpacing="3" PagerStyle-Width="4" PagerStyle-Height="4" Visible="False" 
        BorderColor="#FF6600" BorderStyle="Solid" Width="586px" DataKeyNames="ID">
        <Columns>
              <asp:TemplateField ShowHeader="False" >
   <ItemTemplate>
   <asp:ImageButton ID="BtnEdit" runat="server" CausesValidation="False" CommandName="Edit" ImageUrl="~/Images/edit.gif" ToolTip="Edit"  />
   </ItemTemplate>
   </asp:TemplateField>  
            <asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID" ReadOnly="True" />
            <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title" /> 
            <asp:BoundField DataField="Filling_Status" HeaderText="Filling_Status" 
                SortExpression="Filling_Status" />
            
            <asp:BoundField DataField="Entry_Status" HeaderText="Entry_Status" SortExpression="Entry_Status" />
  
     
         <%-- <asp:BoundField DataField="StatusName" HeaderText="StatusName" SortExpression="StatusName" />--%>
  
     
            
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
             </div>
          <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
         ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" 
         SelectCommand="">
      <%--<SelectParameters>
     <asp:ControlParameter ControlID="PatIDSearch" DefaultValue="_" Name="ID" PropertyName="Text" Type="String" />
     <asp:ControlParameter ControlID="TextBoxtiltleSearch" DefaultValue="_"  Name="Title" PropertyName="Text" Type="String" />
     </SelectParameters>--%>
              </asp:SqlDataSource>
    </asp:Panel>
    <br />
    <asp:Panel ID="Panel6" runat="server" GroupingText="Basic Details" ForeColor="Black"  Style="font-weight:bold; border-style:inset;background-color:#E0EBAD" >

        <table style="width: 878px; margin-bottom: 8px">
              <tr>
                <td class="auto-style1"></td>
            </tr>
            <tr>
                <td class="auto-style25">ID </td>
                <td class="auto-style25">
                    <asp:TextBox ID="txtID" runat="server" Enabled="false"></asp:TextBox>
                </td>  
                <td class="auto-style25">
                    UTN
                </td>   
                <td class="auto-style25">
                      <asp:TextBox ID="txtPatUTN" runat="server" Enabled="false"></asp:TextBox>
                </td>
                </tr>
            <tr>
                <td class="auto-style18">                  
                     Filing Status
                </td>
                <td class="auto-style18">                   
              <asp:DropDownList ID="ddlFilingstatus" runat="server" Width="130px" Enabled="false" 
                    DataSourceID="SqlDataSourePatentStatus" DataTextField="StatusName" DataValueField="Id" 
                   OnSelectedIndexChanged="OnselectFilingStatus" AutoPostBack="true" Height="16px"></asp:DropDownList>
           </td>
             
                  <td class="auto-style18">
                    Nature of Patent 
                </td>
                <td class="auto-style9">
                    <asp:DropDownList ID="ddlNatureofPatent" runat="server" Enabled="false" Height="16px" Width="129px"> 
                        <asp:ListItem Text="Select"></asp:ListItem>
                        <asp:ListItem Text="Complete" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Provisional" Value="2"></asp:ListItem>

                        
                    </asp:DropDownList>
                </td>
                
                    <td class="auto-style13">
                    &nbsp;Funding 
                </td>
                <td class="auto-style13">
                    <asp:DropDownList ID="ddlFunding" runat="server" Width="135px" Enabled="false">
                         <asp:ListItem Text="Select"></asp:ListItem>
                        <asp:ListItem Text="MAHE" Value="1"></asp:ListItem>
                        <asp:ListItem Text="External" Value="2"></asp:ListItem>
                        <asp:ListItem Text="Self" Value="2"></asp:ListItem>

                    </asp:DropDownList>
                </td>
              
                   </tr>
            <tr>
                 <td class="auto-style20">Title </td>
                <td colspan="3" class="auto-style20">
                    <asp:TextBox ID="txtTitle" runat="server" Width="514px" Enabled="false"></asp:TextBox>
                </td>
                </tr>
            <tr>
                <td class="auto-style21">
                    Description
                </td>
                <td colspan="3" class="auto-style21">
                     <asp:TextBox ID="txtde" runat="server"  Width="514px" Enabled="false"></asp:TextBox>
                </td>
               <%-- <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtFilingDateProvided" Format="dd/MM/yyyy" CssClass="cal_Theme1"> 
   </asp:CalendarExtender>--%>
                <asp:CalendarExtender ID="CalendarExtender6" runat="server" TargetControlID="txtdateofApplication" Format="dd/MM/yyyy" CssClass="cal_Theme1"> 
   </asp:CalendarExtender>
                <asp:CalendarExtender ID="CalendarExtender7" runat="server" TargetControlID="txtGrantDate" Format="dd/MM/yyyy" CssClass="cal_Theme1"> 
  </asp:CalendarExtender>
                 <asp:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtlastRenewalFee" Format="dd/MM/yyyy" CssClass="cal_Theme1"> 
   </asp:CalendarExtender>
               <asp:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="txtAppDate" Format="dd/MM/yyyy" CssClass="cal_Theme1"> 
   </asp:CalendarExtender>
            </tr>

             

          
                    <tr>
                        <td style="width: 17%">Details of Collaborative Agency / Institution/ Industry
                            <asp:Label ID="Label8" runat="server" ForeColor="Red" ></asp:Label>
                        </td>

                        <td colspan="3" class="auto-style23">

                            <asp:TextBox ID="txtdetailsCII" runat="server" TextMode="MultiLine" Width="364px"></asp:TextBox>

                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="Enter the Title" ControlToValidate="txtTitle" Display="None" ValidationGroup="validation" SetFocusOnError="true"></asp:RequiredFieldValidator>--%>

                        </td>
                         </tr>
                    
                        
        </table>

        <table><tr>
                        <td style="width: 14%">Revenue Generated (INR)
                        </td>
                        <td class="auto-style9">
                            <asp:TextBox ID="txtrevenue" runat="server"  ></asp:TextBox>
                        </td>

                        <td >Country
                        </td>
                        <td colspan="3" class="auto-style18">
                            <asp:TextBox ID="txtcountry" runat="server" Width="141px "></asp:TextBox>
                        </td>

                        

                    </tr>
                    
                        
        </table>

         <asp:ModalPopupExtender ID="ModalPopupApp" runat="server" TargetControlID="btnview" PopupControlID="PopAppStage"
                   BackgroundCssClass="modelBackground" ></asp:ModalPopupExtender>
         <asp:ModalPopupExtender ID="Modalpoprenewal" runat="server" TargetControlID="btnRenewalview" PopupControlID="PoppanelRenewal"
                   BackgroundCssClass="modelBackground" ></asp:ModalPopupExtender>
    </asp:Panel>
    
<asp:Panel ID="panAddAuthor" runat="server" GroupingText="Inventors Details"  ForeColor="Black"  Style="font-weight:bold; border-style:inset;background-color:#E0EBAD"  > 

              <%--       <center>
          Author Details
                     </center>--%>
                     <br />
                         <table width="92%">                  
                           <tr>
                           <td>
                           <asp:Button ID="BtnAddMU" runat="server" Text="Add New Inventor" onclick="addRow" Visible="false" /> 
                            
                           </td>
                           </tr>
                           </table>  
  <br />
    
                <asp:SqlDataSource ID="SqlDataSourceAuthorType" runat="server"
                    ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>"
                    ProviderName="System.Data.SqlClient"></asp:SqlDataSource>

<asp:Panel ID="PanelMU" runat="server" ScrollBars="both"  Width="1000px" Enabled="false" >
  
     <asp:GridView ID="Grid_AuthorEntry" runat="server" AutoGenerateColumns="False"  GridLines="None" 
     OnRowDeleting="Grid_Patent_RowDeleting" CellPadding="4" ForeColor="#333333" OnRowDataBound="OnRowDataBound" >

     <AlternatingRowStyle BackColor="White" />
     <Columns>

        <asp:TemplateField HeaderText="MAHE/Non MAHE">
             <ItemTemplate>
                 <asp:DropDownList ID="DropdownMuNonMu" runat="server" Width="150" OnSelectedIndexChanged="DropdownMuNonMuOnSelectedIndexChanged" AutoPostBack="true" DataSourceID="SqlDataSourceAuthorType" >
                     
                
                 </asp:DropDownList>
                 <asp:ImageButton ID="MuNonMu" runat="server" ImageUrl="~/Images/srchImg.gif" CssClass="blnkImgCSS"  />              

             </ItemTemplate>              
        </asp:TemplateField>

        <asp:TemplateField ShowHeader="true"  HeaderText="Roll No/Employee Code">
                                <ItemTemplate>
                                    <asp:TextBox ID="EmployeeCode" runat="server" Width="155"  MaxLength="9"></asp:TextBox>
                                    <asp:Image ID="ImageEmpCode" runat="server" ImageUrl="~/Images/srchImg.gif" CssClass="blnkImgCSS" />


                                </ItemTemplate>
                            </asp:TemplateField>

        <asp:TemplateField HeaderText="Investigator Name">
             <ItemTemplate>
                 <asp:TextBox ID="AuthorName" runat="server" Width="200" Enabled="false" OnTextChanged="AuthorName_Changed" AutoPostBack="true"></asp:TextBox>
          
                 <asp:ImageButton ID="EmployeeCodeBtn" runat="server" ImageUrl="~/Images/srchImg.gif" OnClientClick="setRowPop(this)" />
                  <asp:ImageButton ID="ImageButton1" runat="server" Visible="false" ImageUrl="~/Images/srchImg.gif" CausesValidation="false" OnClientClick="setRowPop(this)" />
               <%--  <asp:ModalPopupExtender ID="ModalPopupExtender4" runat="server" TargetControlID="EmployeeCodeBtn" PopupControlID="popupPanelAffil"
                    BackgroundCssClass="modelBackground"  >
                                  </asp:ModalPopupExtender>--%>
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
        <asp:RegularExpressionValidator
        id="regEmail"
        ControlToValidate="MailId" Display="Static" ErrorMessage="Invalid Email Id"
        Text="(Invalid email)" ValidationGroup="validation"
        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
        Runat="server" />    
                  </ItemTemplate>         
        </asp:TemplateField>

   <asp:TemplateField ShowHeader="false">
       <ItemTemplate>
      <%--<asp:DropDownList ID="AuthorType" runat="server" Width="170" >
        <asp:ListItem Value="P">Principal Investigator</asp:ListItem>
          <asp:ListItem Value="C">CO-Investigator</asp:ListItem>
        </asp:DropDownList>--%>
  <%--<asp:Image ID="ImageisAuthorType" runat="server" ImageUrl="~/Images/srchImg.gif" CssClass="blnkImgCSS" />--%>
 </ItemTemplate>              
  </asp:TemplateField>
  <asp:TemplateField  HeaderText="Colloboration">
     <ItemTemplate>
    <asp:DropDownList ID="NationalType" runat="server" Width="140" OnSelectedIndexChanged="NationalTypeOnSelectedIndexChanged" AutoPostBack="true"  >
     <asp:ListItem Value="N">National</asp:ListItem>
    <asp:ListItem Value="I">International</asp:ListItem>
   </asp:DropDownList>
 <%--<asp:Image ID="ImageisNationalType" runat="server" ImageUrl="~/Images/srchImg.gif" CssClass="blnkImgCSS" />--%>
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
  <%--<asp:Image ID="ImageisContinent" runat="server" ImageUrl="~/Images/srchImg.gif" CssClass="blnkImgCSS" />--%>
  </ItemTemplate>              
  </asp:TemplateField>
  <asp:TemplateField ShowHeader="false">
    <ItemTemplate>
    <asp:HiddenField ID="Institution" runat="server" ></asp:HiddenField>               
   <%--<asp:ImageButton ID="InstitutionBtn" runat="server"  ImageUrl="~/Images/srchImg.gif" CssClass="blnkImgCSS" />--%>
  </ItemTemplate>              
        </asp:TemplateField>
             <asp:TemplateField ShowHeader="false">
             <ItemTemplate>
         <asp:HiddenField ID="Department" runat="server" ></asp:HiddenField>
 <%--<asp:ImageButton ID="DepartmentBtn" runat="server" Enabled="false" ImageUrl="~/Images/srchImg.gif"  CssClass="blnkImgCSS"  />--%>
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
    


    <asp:Panel ID="Panel1" runat="server" BorderStyle="Inset" GroupingText="Filling Details"   Font-Bold="true" Style="background-color:#E0EBAD">
        <br />
     <asp:Panel ID="Panelfilling" runat="server"  >
        <table style="height: 213px" id="maintable" runat="server">          
            <tr>         
                <td>
                    <br />
                    &nbsp;Filing Office
                </td>
                <td>
                    <br />
                    <%--<asp:TextBox ID="txtFilingOffice" runat="server" Enabled="false"></asp:TextBox>--%>
                   <asp:DropDownList ID="ddlfilingoffice" runat="server" Height="16px" Width="129px" DataSourceID="SqlDataSourceFilingOffice"
      DataTextField="F_OfficeName" DataValueField="Id" AppendDataBoundItems="true" Enabled="false">
                                    
                                </asp:DropDownList>
                                 <asp:SqlDataSource ID="SqlDataSourceFilingOffice" runat="server" ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" 
           SelectCommand="select Id, F_OfficeName from Pat_FilingOffice_M"></asp:SqlDataSource>
                </td>

                <td class="auto-style5" >
                    &nbsp;Application Number
                </td>
                <td class="auto-style5">
                    <asp:TextBox ID="txtapplicationNo" runat="server" Enabled="false"></asp:TextBox>
                </td>
                <td class="auto-style23">
                    Application Stage 
                </td>
                <td class="auto-style24">
                    <asp:TextBox ID="txtApplicationStage" runat="server" Enabled="false"></asp:TextBox>
                </td>
                <td>
                     <%--<asp:ModalPopupExtender ID="ModalPopupApp" runat="server" TargetControlID="btnview" PopupControlID="PopAppStage"
                   BackgroundCssClass="modelBackground" ></asp:ModalPopupExtender>--%>
                    <asp:Button ID="btnview" runat="server" Text="View" OnClick="Btn_App_View" Enabled="false" />
                </td>
            </tr>
            <tr>
               
                
                <td class="auto-style23">
                    &nbsp;Date of Application
                </td>
                <td class="auto-style24">
                    <asp:TextBox ID="txtdateofApplication" runat="server" Enabled="false"></asp:TextBox>
                     <%--<asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtdateofApplication" Format="dd/MM/yyyy" CssClass="cal_Theme1"> 
   </asp:CalendarExtender>--%>
                </td>
                <%--<td class="auto-style11">
                    &nbsp;Provisional Number
                </td>
                <td class="auto-style11">
                    <asp:TextBox ID="txtProvisionalNo" runat="server" Enabled="false"></asp:TextBox>
                </td>--%>
           
                <td class="auto-style2" >
                    Patent Number
                </td>
                <td class="auto-style5">
                    <asp:TextBox ID="txtPatentNo" runat="server" Enabled="false"></asp:TextBox>
                </td>
               <%-- <td class="auto-style23">
                    &nbsp;Filing Date provided by Patent&nbsp;Office
                </td>
                <td class="auto-style24" >
                    <asp:TextBox ID="txtFilingDateProvided" runat="server" Enabled="false"></asp:TextBox>
                     
                </td>--%>
                <td class="auto-style5" >
                    Grant Date
                </td>
                <td class="auto-style5">
                    <asp:TextBox ID="txtGrantDate" runat="server" Enabled="false"></asp:TextBox>
                     
                </td>
              
            </tr>
            <tr>
              
                <td>
                    Last Renewal Date
                </td>
                <td>
                    <asp:TextBox ID="txtlastRenewal" runat="server" Enabled="false" ></asp:TextBox>

                
                    <asp:Button ID="btnRenewalview" runat="server" Text="View" Enabled="false"  />
                </td>
         
                <td class="auto-style23">
                    Remarks
                </td>
                <td class="auto-style8" colspan="2" >
                    <asp:TextBox ID="txtRemark" runat="server" Width="402px" Enabled="false" ></asp:TextBox>
                </td>
                
            <td></td>
            </tr>
            </table>
        </asp:Panel>
        <table id="rejecttable">
            <tr>
                  <td class="auto-style1" >
                      <asp:Label ID="lblRejectRemarks" runat="server" Text="Rejection Remark" Visible="false"></asp:Label> 
                </td>
                <td class="auto-style8" >
                    <asp:TextBox ID="txtRejectionRemark" runat="server" Visible="false"></asp:TextBox>
                </td>
            </tr>
        </table>
               <asp:SqlDataSource ID="SqlDataSourePatentStatus" runat="server" ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" 
           SelectCommand="select Id,StatusName from Patent_Status where Id='APP'">
 </asp:SqlDataSource>
      <%--  <div style="margin-left:500px">
            <asp:Button ID="Button2" runat="server" Text="Submit" OnClick="Btn_Sumbit" />
            <%--<asp:Button ID="Button1" runat="server" Text="Update" />--%>
            <%--<asp:Button ID="Button3" runat="server" Text="Cancel" />--%>

      
               
    </asp:Panel>
    <asp:Panel ID="PoppanelRenewal" runat="server" GroupingText="Renewal Details" Style="font-weight:bold; background-color:#E0EBAD; border:inset; border-color:black; margin-top: 9px;">
    <br />
        <br />
         <center>
        <asp:GridView ID="grdRenewal" runat="server" AutoGenerateColumns="False" DataSourceID="sqlRenewal"  EmptyDataText="No data found">
            <Columns>
                 
              
                <asp:BoundField DataField="RenewalAmount" HeaderText="RenewalAmount" SortExpression="RenewalAmount" />
                <asp:BoundField DataField="LastRenewal_Date" HeaderText="LastRenewal_Date" SortExpression="LastRenewal_Date" DataFormatString="{0:d}" />
                <asp:BoundField DataField="RenewalComment" HeaderText="RenewalComment" SortExpression="RenewalComment" ItemStyle-Width="250px" />
                 
              
            </Columns>
            <HeaderStyle BackColor="#0B532D" Font-Bold="True" ForeColor="White" />
    <PagerStyle BackColor="#000000" ForeColor="#000000" HorizontalAlign="Center" />
        <RowStyle BackColor="#FFFFFF" ForeColor="#333333" />                
        </asp:GridView></center>
        <asp:SqlDataSource ID="sqlRenewal" runat="server" 
            ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" 
             SelectCommand="SELECT [RenewalAmount], [LastRenewal_Date], [RenewalComment] FROM [Patent_Renewal_Tracker] where ID=@ID" >  
             <SelectParameters>
            <asp:ControlParameter ControlID="txtID" DefaultValue="_" Name="ID" PropertyName="Text" Type="String" />

            </SelectParameters>       
        </asp:SqlDataSource>
            <br />  
            <table>
            <tr>
                
                 <td class="auto-style15" >
                     Renewal Fee&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                </td>
               
                <td class="auto-style16">
                    <asp:TextBox ID="txtRenewalFee" runat="server"></asp:TextBox>
                </td>
                <td class="auto-style17">
                    &nbsp;Last Renewal Fee Paid date
                </td>
                <td class="auto-style19" >
                    <asp:TextBox ID="txtlastRenewalFee" runat="server"></asp:TextBox>
                     <%--<asp:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtlastRenewalFee" Format="dd/MM/yyyy" CssClass="cal_Theme1"> 
   </asp:CalendarExtender>--%>
                </td>
                </tr>
            <tr>
                <td class="auto-style15">
                    Comment
                </td>
              <td class="auto-style16">
                    <asp:TextBox ID="txtRenewalComment" runat="server" Width="347px"></asp:TextBox>

              </td>
            </tr>
        </table>
            <center>
        <asp:Button ID="btnSaveRenewal" runat="server" Text="Save" OnClick="btnRenewal_Click" Width="42px"/>
          <asp:Button ID="Button2" runat="server" Text="Exit"  /></center>
          
            
         
    </asp:Panel>
    <asp:Panel ID="Panel5" runat="server">
        
        <div style="margin-left:500px">
            <asp:Button ID="Btnsave" runat="server" Text="Save" OnClick="Btn_Save" Visible="false" Enabled="false"  />
            <asp:Button ID="BtnDraft" runat="server" Text="Save Draft" OnClick="Btn_Save_Draft" Visible="false" Enabled="false" />
            <asp:Button ID="Btnsubmit" runat="server" Text="Submit" OnClick="Btn_Data_Sumbit" Visible="false" Enabled="false" />

            <%--<asp:Button ID="Button1" runat="server" Text="Update" />--%>
            <asp:Button ID="Button3" runat="server" Text="Clear" OnClick="btn_Clear"  Visible="false" />

        </div>
               
    </asp:Panel>
    
     <asp:Panel ID="PopAppStage" runat="server" width="800px" CssClass="modelPopup"  Style="font-weight:bold; background-color:#E0EBAD; border:inset; border-color:black; margin-top: 9px;" >
 <asp:Label ID="Label11" runat="server" Visible="false"></asp:Label>
 <asp:Label ID="Label12" runat="server" Visible="false"></asp:Label>
 <br />
 <center>
        <asp:GridView ID="popgridApp" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource5"  EmptyDataText="No data found">
            <Columns>
                <asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID" />
                <asp:BoundField DataField="Application_Stage" HeaderText="Application_Stage" SortExpression="Application_Stage" />
                <asp:BoundField DataField="App_Date" HeaderText="App_Date" SortExpression="App_Date" DataFormatString="{0:d}" />
                <asp:BoundField DataField="App_Comment" HeaderText="App_Comment" SortExpression="App_Comment" ItemStyle-Width="250px" />           
              
            </Columns>
            <HeaderStyle BackColor="#0B532D" Font-Bold="True" ForeColor="White" />
    <PagerStyle BackColor="#000000" ForeColor="#000000" HorizontalAlign="Center" />
        <RowStyle BackColor="#FFFFFF" ForeColor="#333333" />                
        </asp:GridView></center>
        <asp:SqlDataSource ID="SqlDataSource5" runat="server" 
            ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" 
             SelectCommand="Select_App_Stage" SelectCommandType="StoredProcedure" >         
            <SelectParameters>
            <asp:ControlParameter ControlID="txtID" DefaultValue="_" Name="ID" PropertyName="Text" Type="String" />

            </SelectParameters>
        </asp:SqlDataSource>
            <br />          
         <table>
             <tr>
                 <td>
                     Application Stage:
                 </td>
                 <td class="auto-style12">
                     <asp:DropDownList ID="ddlAppstage"  runat="server" DataTextField="StatusName" DataValueField="Id" DataSourceID="SqlAppStage" AppendDataBoundItems="True">
             <asp:ListItem Selected="True" Text="Select" Value="0"></asp:ListItem>
                     </asp:DropDownList>
                     <asp:SqlDataSource ID="SqlAppStage" runat="server" ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" SelectCommand="SELECT * FROM [Patent_App_Status]"></asp:SqlDataSource>
                 </td>
                 <td class="auto-style4">&nbsp;</td>
                 <td>
                     Date:
                 </td>
                 <td>
                 <asp:TextBox ID="txtAppDate" runat="server"></asp:TextBox>
                       

                 </td>
             </tr>
             <tr>
                   <td>Comment</td>
                 <td colspan="3">
                     <asp:TextBox ID="txtAppComment" runat="server" Width="291px"></asp:TextBox></td>
             </tr>
         </table>
           <center>
        <asp:Button ID="BtnAPPsave" runat="server" Text="Save" OnClick="btnApp_Submit" Width="42px"/>
          <asp:Button ID="Button13" runat="server" Text="Exit"  /></center>
                <br />
         
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
<asp:HiddenField ID="rowVal" runat="server" />
    <asp:HiddenField ID="senderID" runat="server" />
        </asp:Panel>
     <%--<div id="commentpopup5" class="overlay">
                <div class="popupp" style="width: 900px; height: 575px;margin-top: 20px;">

                    <a class="close" href="#">&times;</a>
                    <div class="content" style="overflow-y: scroll; height: 570px; width: 900px;">
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>

                               
                                <asp:Panel ID="popupstudent" Style="background: white" runat="server" Visible="false" CssClass="modalPopup" Width="850px" Height="500px">
                                    <table align="center">
                                        <tr>
                                            <th>Student Details </th>
                                        </tr>
                                    </table>

                                    <table>
                                        <tr>
                                            <td>Name:
                    <asp:TextBox ID="txtSrchStudentName" runat="server"></asp:TextBox></td>
                                            <td></td>
                                            <td>Roll No:
                    <asp:TextBox ID="txtSrchStudentRollNo" runat="server"></asp:TextBox></td>
                                            <td>Institution:<asp:DropDownList ID="StudentIntddl" DataSourceID="sqlstudentds" DataTextField="InstName" DataValueField="InstID" runat="server" AppendDataBoundItems="true">
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
                                                <asp:Button ID="Button4" runat="server" Text="EXIT" OnClick="exit" />
                                            </td>
                                        </tr>
                                    </table>
                                    <br />
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:GridView ID="popupStudentGrid" runat="server" AutoGenerateSelectButton="true" EmptyDataText="No Records Found"
                                                    OnSelectedIndexChanged="StudentDataSelect" AllowSorting="true" AutoGenerateColumns="false" >
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
                                                <asp:SqlDataSource ID="StudentSQLDS" runat="server" ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>"
                                                    SelectCommand="Select TOP 8  RollNo,Name,SISClass.ClassName as ClassName,SISInstitution.InstName as InstName,EmailID1 ,[SISStudentGenInfo].ClassCode as ClassCode,[SISStudentGenInfo].InstID as InstID from [dbo].[SISStudentGenInfo],SISClass,SISInstitution where [SISStudentGenInfo].ClassCode=SISClass.ClassCode and [SISStudentGenInfo].InstID=SISInstitution.InstID" ProviderName="System.Data.SqlClient">
                                                    <SelectParameters>
                                                        <asp:SessionParameter Name="UserId" SessionField="UserId" Type="String" />
                                                    </SelectParameters>
                                                </asp:SqlDataSource>
                                                <asp:SqlDataSource ID="sqlstudentds" runat="server" ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>"
                                                    SelectCommand="Select InstName,InstID from SISInstitution" ProviderName="System.Data.SqlClient"></asp:SqlDataSource>
                                            </td>
                                        </tr>
                                    </table>

                                </asp:Panel>



                            </ContentTemplate>
                        </asp:UpdatePanel>

                    </div>
                    <center><a class="close1" id="a3" href="#">Close</a></center>

                </div>

            </div>--%>
    <asp:Panel ID="panelPatentCanelRemark" runat="server" Visible="false" GroupingText="Remarks for cancellation" Style="font-weight:bold; border-style:inset;background-color:#E0EBAD"  > 
 <table style="width: 92%">
   <tr>
     <td style="height: 36px; font-weight:normal">
      <asp:Label ID="Label13" runat="server" Text="Remarks"  ></asp:Label>
       </td>
        <td style="height: 36px">
          <asp:TextBox ID="txtCancelRemarks" runat="server" TextMode="MultiLine" Style="border-style:inset none none inset;" Enabled="true"
        Width="900px" ></asp:TextBox>   
       </td>
     </tr>
    
        </table>
        <div style="text-align:center">
              <asp:Button ID="btnSaveCan" runat="server" Text="Cancel" OnClick="Btn_Save_Cancel" />

        </div>
         
     

</asp:Panel>
</asp:Content>

