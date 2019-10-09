<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPageRMS.master" AutoEventWireup="true" CodeFile="PatentPrintEvaluationForm.aspx.cs" Inherits="GrantEntry_PatentPrintEvaluationForm" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Panel ID="Panel2" runat="server">
        <div style="font-weight:bold; text-align:center; font-size:20px">
          Patent Print Evaluation Form
        </div>
    </asp:Panel>
      
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
   <asp:ImageButton ID="BtnEdit" runat="server" CausesValidation="False" CommandName="Edit" ImageUrl="~/Images/view.gif" ToolTip="Edit"  />
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
         <%-- <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
         ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" 
         SelectCommand="searchPatent" SelectCommandType="StoredProcedure">
      <SelectParameters>
     <asp:ControlParameter ControlID="PatIDSearch" DefaultValue="_" Name="ID" PropertyName="Text" Type="String" />
     <asp:ControlParameter ControlID="TextBoxtiltleSearch" DefaultValue="_"  Name="Title" PropertyName="Text" Type="String" />
     </SelectParameters>
              </asp:SqlDataSource>--%>
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
    <asp:Panel ID="PanelMain" runat="server">
    <asp:Panel ID="Panel6" runat="server" GroupingText="Basic Details" ForeColor="Black"  Style="font-weight:bold; border-style:inset;background-color:#E0EBAD" >

        <table style="width: 878px; margin-bottom: 8px">
              <tr>
                <td class="auto-style1"></td>
            </tr>
            <tr>
                <td class="auto-style1">ID </td>
                <td class="auto-style1">
                    <asp:TextBox ID="txtID" runat="server" Enabled="false"></asp:TextBox>
                </td>  
                <td class="auto-style1">
                    UTN
                </td>   
                <td class="auto-style1">
                      <asp:TextBox ID="txtPatUTN" runat="server" Enabled="false"></asp:TextBox>
                </td>
                </tr>
            <tr>
                <td class="auto-style18">                  
                     Filing Status
                </td>
                <td class="auto-style18">                   
              <asp:DropDownList ID="ddlFilingstatus" runat="server" Width="130px" 
                    DataSourceID="SqlDataSourePatentStatus" DataTextField="StatusName" DataValueField="Id" 
                   OnSelectedIndexChanged="OnselectFilingStatus" AutoPostBack="true" Height="16px"></asp:DropDownList>
           </td>
             
                  <td class="auto-style18">
                    Nature of Patent 
                </td>
                <td class="auto-style9">
                    <asp:DropDownList ID="ddlNatureofPatent" runat="server" Height="16px" Width="129px">
                        <asp:ListItem Text="Select"></asp:ListItem>
                        <asp:ListItem Text="Complete" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Provisional" Value="2"></asp:ListItem>

                        
                    </asp:DropDownList>
                </td>
                
                    <td class="auto-style13">
                    &nbsp;Funding 
                </td>
                <td class="auto-style13">
                    <asp:DropDownList ID="ddlFunding" runat="server" Width="135px">
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
                    <asp:TextBox ID="txtTitle" runat="server" Width="514px"></asp:TextBox>
                </td>
                </tr>
            <tr>
                <td class="auto-style21">
                    Description
                </td>
                <td colspan="3" class="auto-style21">
                     <asp:TextBox ID="txtde" runat="server"  Width="514px"></asp:TextBox>
                </td>
                </tr>

             <tr>
                        <td style="width: 10%">Details of Collaborative Agency / Institution/ Industry
                            <asp:Label ID="lbldetails" runat="server" ForeColor="Red" ></asp:Label>
                        </td>

                        <td colspan="3" class="auto-style23">

                            <asp:TextBox ID="txtdetailsCII" runat="server" TextMode="MultiLine" Width="364px"></asp:TextBox>

                            <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="Enter the Title" ControlToValidate="txtTitle" Display="None" ValidationGroup="validation" SetFocusOnError="true"></asp:RequiredFieldValidator>--%>

                        </td>

                        <td class="auto-style218">Revenue Generated (INR)
                        </td>
                        <td class="auto-style9">
                            <asp:TextBox ID="txtrevenue" runat="server"  ></asp:TextBox>
                        </td>

                 <td class="auto-style18">Country
                        </td>
                        <td colspan="3" class="auto-style18">
                            <asp:TextBox ID="txtcountry" runat="server" Width="141px "></asp:TextBox>
                        </td>


                    </tr>
                    
                        
                      
                        



                       

                </table>
             <%--   <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtFilingDateProvided" Format="dd/MM/yyyy" CssClass="cal_Theme1"> 
   </asp:CalendarExtender>
                <asp:CalendarExtender ID="CalendarExtender6" runat="server" TargetControlID="txtdateofApplication" Format="dd/MM/yyyy" CssClass="cal_Theme1"> 
   </asp:CalendarExtender>
                <asp:CalendarExtender ID="CalendarExtender7" runat="server" TargetControlID="txtGrantDate" Format="dd/MM/yyyy" CssClass="cal_Theme1"> 
  </asp:CalendarExtender>
                 <asp:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtlastRenewalFee" Format="dd/MM/yyyy" CssClass="cal_Theme1"> 
   </asp:CalendarExtender>
               <asp:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="txtAppDate" Format="dd/MM/yyyy" CssClass="cal_Theme1"> 
   </asp:CalendarExtender>
            </tr>
        </table>
         <asp:ModalPopupExtender ID="ModalPopupApp" runat="server" TargetControlID="btnview" PopupControlID="PopAppStage"
                   BackgroundCssClass="modelBackground" ></asp:ModalPopupExtender>
         <asp:ModalPopupExtender ID="Modalpoprenewal" runat="server" TargetControlID="btnRenewalview" PopupControlID="PoppanelRenewal"
                   BackgroundCssClass="modelBackground" ></asp:ModalPopupExtender>--%>
     </asp:Panel>
    <br />
         <asp:SqlDataSource ID="SqlDataSourceAuthorType" runat="server"
                    ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>"
                    ProviderName="System.Data.SqlClient"></asp:SqlDataSource>


    <asp:Panel ID="panAddAuthor" runat="server" GroupingText="Inventors Details"  ForeColor="Black"  Style="font-weight:bold; border-style:inset;background-color:#E0EBAD"  > 
<asp:Panel ID="PanelMU" runat="server" ScrollBars="both"  Width="1000px" >
  
     <asp:GridView ID="Grid_AuthorEntry" runat="server" AutoGenerateColumns="False"  GridLines="None" 
     OnRowDeleting="Grid_Patent_RowDeleting" CellPadding="4" ForeColor="#333333" OnRowDataBound="OnRowDataBound" >

     <AlternatingRowStyle BackColor="White" />
     <Columns>

        <asp:TemplateField HeaderText="MAHE/Non MAHE">
             <ItemTemplate>
                 <asp:DropDownList ID="DropdownMuNonMu" runat="server" Width="150" OnSelectedIndexChanged="DropdownMuNonMuOnSelectedIndexChanged" AutoPostBack="true"  DataSourceID="SqlDataSourceAuthorType" >
                     
               
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
                 <asp:TextBox ID="AuthorName" runat="server" Width="200" Enabled="false" ></asp:TextBox>
          
                 <asp:ImageButton ID="EmployeeCodeBtn" runat="server" ImageUrl="~/Images/srchImg.gif" OnClientClick="setRow(this)" />

                 <%--<asp:ModalPopupExtender ID="ModalPopupExtender4" runat="server" TargetControlID="EmployeeCodeBtn" PopupControlID="popupPanelAffil"
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
    <br />
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
                  <%--  <asp:TextBox ID="txtFilingOffice" runat="server"></asp:TextBox>--%>
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
                    <asp:TextBox ID="txtapplicationNo" runat="server"></asp:TextBox>
                </td>
                
                <td class="auto-style10">
                    Application Stage 
                </td>
                <td class="auto-style11">
                    <asp:TextBox ID="txtApplicationStage" runat="server" Enabled="false"></asp:TextBox>
                </td>
                <td>
                    <%-- <asp:ModalPopupExtender ID="ModalPopupApp" runat="server" TargetControlID="btnview" PopupControlID="PopAppStage"
                   BackgroundCssClass="modelBackground" ></asp:ModalPopupExtender>--%>
                    
                </td>
            </tr>
            <tr>
              
                <td class="auto-style13">
                    &nbsp;Date of Application
                </td>
                <td class="auto-style13">
                    <asp:TextBox ID="txtdateofApplication" runat="server"></asp:TextBox>
                     <%--<asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtdateofApplication" Format="dd/MM/yyyy" CssClass="cal_Theme1"> 
   </asp:CalendarExtender>--%>
                </td>
                <%--<td class="auto-style11">
                    &nbsp;Provisional Number
                </td>
                <td class="auto-style11">
                    <asp:TextBox ID="txtProvisionalNo" runat="server"></asp:TextBox>
                </td>--%>
           
                
                <td class="auto-style2" >
                    Patent Number
                </td>
                <td class="auto-style5">
                    <asp:TextBox ID="txtPatentNo" runat="server"></asp:TextBox>
                </td>
               <%-- <td class="auto-style11">
                    &nbsp;Filing Date provided by Patent&nbsp;Office
                </td>
                <td class="auto-style11" >
                    <asp:TextBox ID="txtFilingDateProvided" runat="server"></asp:TextBox>
                     
                </td>--%>
                <td class="auto-style5" >
                    Grant Date
                </td>
                <td class="auto-style5">
                    <asp:TextBox ID="txtGrantDate" runat="server" Enabled="false"></asp:TextBox>
                     
                </td>
               <%-- <td class="auto-style5" >
                    Grant Date
                </td>
                <td class="auto-style5">
                    <asp:TextBox ID="txtGrantDate" runat="server"></asp:TextBox>
                     
                </td>
            </tr>
            
            <tr>
                <td class="auto-style6" >
                    Renewal Fee
                </td>
                <td class="auto-style8">
                    <asp:TextBox ID="txtRenewalFee" runat="server"></asp:TextBox>
                </td>
                <td class="auto-style8">
                    &nbsp;Last Renewal Fee Paid date
                </td>
                <td class="auto-style8" >
                    <asp:TextBox ID="txtlastRenewalFee" runat="server"></asp:TextBox>
                   
                </td>
                --%>
              
            </tr>
            <tr>
              
                <td>
                    Last Renewal Date
                </td>
                <td>
                    <asp:TextBox ID="txtlastRenewal" runat="server" Enabled="false" ></asp:TextBox>

                
                    
                </td>
         
                <td class="auto-style8">
                    Remarks
                </td>
                <td class="auto-style8" colspan="2" >
                    <asp:TextBox ID="txtRemark" runat="server" Width="402px" ></asp:TextBox>
                </td>
                
            
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
       

                                                                      
    <%--<asp:Button ID="BtnPdf" Visible="false" runat="server" Text="View Pdf" CausesValidation="false" ></asp:Button>--%>
               
        </asp:Panel>
     <div style="text-align:center">
        <asp:Button ID="ButtonSavepdf" Height="30" Font-Bold="true" runat="server" Text="Print Evaluation Form" OnClick="BtnGenetratePdf" CausesValidation="false"/>

        </div>
</asp:Content>

