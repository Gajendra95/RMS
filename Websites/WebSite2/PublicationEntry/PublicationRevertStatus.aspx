<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPageRMS.master" AutoEventWireup="true" CodeFile="PublicationRevertStatus.aspx.cs" Inherits="PublicationEntry_PublicationRevertStatus" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <style type="text/css">
             .auto-style8 {
          width: 135px;
      }
    </style>
    <center> <asp:Label ID="lablPanelTitle" runat="server" Text="Revert Publication Status" Font-Bold="true"  ></asp:Label></center>
<br />
  
<asp:Panel ID="panelSearchPub" runat="server" ForeColor="Black"   Font-Bold="true" Style="background-color:#E0EBAD" > 
<br />
<center>
<table border="1" style="width: 95%">
<tr>
<td style="height: 36px; font-weight:normal; color:Black" >Publication Entry Type</td>
<td style="height: 36px">             
<asp:DropDownList ID="EntryTypesearch" runat="server" Style="border-style:inset none none inset;"
     DataSourceID="SqlDataSource4" DataTextField="EntryName" DataValueField="TypeEntryId" AppendDataBoundItems="true">
<asp:ListItem Value="A">ALL</asp:ListItem>
</asp:DropDownList> 
<asp:SqlDataSource ID="SqlDataSource4" runat="server"  ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" 
     SelectCommand="select TypeEntryId,EntryName from PublicationTypeEntry_M" ProviderName="System.Data.SqlClient">
</asp:SqlDataSource>
</td>
<td style="height: 36px; font-weight:normal; color:Black"> Publication ID</td>
<td style="height: 36px">
<asp:TextBox ID="PubIDSearch"  runat="server" Style="border-style:inset none none inset;" ></asp:TextBox>
</td>
<td style="height: 36px">  
<asp:Button ID="ButtonSearchPub" runat="server" Text="Search" OnClick="ButtonSearchPubOnClick"  /></td>
</tr>
</table>  
<br />

                              
       <asp:GridView ID="GridViewSearch" runat="server"  AllowPaging="True" 
        PagerSettings-PageButtonCount="5" PageSize="5" DataSourceID="SqlDataSource1"  EmptyDataText="No Data found"
        HeaderStyle-BackColor="#CC6600" HeaderStyle-ForeColor="Black" AutoGenerateColumns="false" OnPageIndexChanging="GridViewSearchPub_PageIndexChanging" 
        PagerStyle-BackColor="#CC6600" PagerStyle-ForeColor="White" CellPadding="3"  OnRowDataBound="GridView2_RowDataBound" OnRowEditing="edit" OnRowCommand="GridView2_RowCommand"
        CellSpacing="3" PagerStyle-Width="4" PagerStyle-Height="4" Visible="False" 
        BorderColor="#FF6600" BorderStyle="Solid">
        <Columns>

     
   <asp:TemplateField ShowHeader="False" >
   <ItemTemplate>
   <asp:ImageButton ID="BtnEdit" runat="server" CausesValidation="False" CommandName="Edit" ImageUrl="~/Images/view.gif" ToolTip="Edit"  />
   </ItemTemplate>
   </asp:TemplateField>
  <asp:BoundField DataField="PublicationID" ReadOnly="true" HeaderText="Publication ID" 
                SortExpression="PublicationID" />
                       <asp:BoundField DataField="EntryName" ReadOnly="true" HeaderText="Type Of Entry" 
                SortExpression="EntryName" />
                  <asp:BoundField DataField="TitleWorkItem" ReadOnly="true" HeaderText="Title Of Work Item" 
                SortExpression="TitleWorkItem" />
                  <asp:BoundField DataField="PubCatName" ReadOnly="true" HeaderText="Category" 
                SortExpression="PubCatName" />
                   <asp:BoundField DataField="StatusName" ReadOnly="true" HeaderText="Status" 
                SortExpression="StatusName" />
                       <asp:TemplateField ShowHeader="false">
             <ItemTemplate>
                 <asp:HiddenField ID="TypeOfEntry" runat="server" Value='<%# Eval("TypeOfEntry") %>'></asp:HiddenField>
            </ItemTemplate>            
        </asp:TemplateField>
            
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
<asp:SqlDataSource ID="SqlDataSource1" runat="server"  ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" 
         SelectCommand=" " ></asp:SqlDataSource>
 <br />
</center>        
</asp:Panel>
<br />

<asp:Panel ID="panel" runat="server" GroupingText="Publication Entries" Enabled="false"  Font-Bold="true"  Style="font-weight:bold; border-style:inset;background-color:#E0EBAD" > 
 <br />
  <center><asp:Label runat="server" ID="Incentivenote" Visible="false" Text="Note: Incevtive Point for the following publication is already processed. You cannnot Revert the status to 'New'"></asp:Label></center>  
    <br />
<asp:Panel ID="panel1" runat="server" GroupingText=""  > 
<table style="width: 92%">
<tr>
<td style="height: 36px; font-weight:normal">
<asp:Label ID="TypeEntry" runat="server" Text="Entry Type" ForeColor="Black"></asp:Label></td>
<td style="height: 36px" >
<asp:DropDownList ID="DropDownListPublicationEntry" runat="server" onchange="ConfirmEntryType(this.options[this.selectedIndex].value);"
     AppendDataBoundItems="true" 
     DataSourceID="SqlDataSourcePublicationEntry" DataTextField="EntryName" 
     DataValueField="TypeEntryId" 
     Style="border-style:inset none none inset;" Width="200px">
 <asp:ListItem Value=" ">--Select--</asp:ListItem>
 </asp:DropDownList>
                                <asp:SqlDataSource ID="SqlDataSourcePublicationEntry" runat="server" 
                                    ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" 
                                    ProviderName="System.Data.SqlClient" 
                                    SelectCommand="select TypeEntryId,EntryName from PublicationTypeEntry_M">
                                </asp:SqlDataSource>
                           
                                 </td>
                        <td style="height: 36px; font-weight: normal;">             
                            <asp:Label ID="lblMUCat" runat="server" Text="MAHE Categorization" ForeColor="Black"></asp:Label>
                       
                         </td>
                                 <td style="height: 36px">
                                     <asp:DropDownList ID="DropDownListMuCategory" runat="server" 
                                         AppendDataBoundItems="true" DataSourceID="SqlDataSourceMuCategory" 
                                         DataTextField="PubCatName" DataValueField="PubCatId" Style="border-style:inset none none inset;" 
                                         Width="200px">
                                         <asp:ListItem Value=" ">--Select--</asp:ListItem>
                                     </asp:DropDownList>
                                     <asp:SqlDataSource ID="SqlDataSourceMuCategory" runat="server" 
                                         ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" 
                                         ProviderName="System.Data.SqlClient" 
                                         SelectCommand="select PubCatId,PubCatName from PubMUCategorization_M">
                                     </asp:SqlDataSource>
                                
                                 </td>
                                 <td style="font-weight:normal">
                                     <asp:Label ID="LabelPubId" runat="server" Text="PublicationId" ForeColor="Black"></asp:Label>
                                 </td>
                                 <td>
                                     <asp:TextBox ID="TextBoxPubId" runat="server" Enabled="false" 
                                         Style="border-style:inset none none inset;" Width="200px"></asp:TextBox>
                                 </td>
                                 </tr>
                                 <tr>
                                     <td style="height: 36px; font-weight:normal">
                                         <asp:Label ID="lblTitileWorkItem" runat="server" Text="Title Of the Work Item" ForeColor="Black"></asp:Label>
                                     </td>
                                     <td colspan="6" style="height: 36px">
                                         <asp:TextBox ID="txtboxTitleOfWrkItem" runat="server" MaxLength="200" 
                                             Style="border-style:inset none none inset;" TextMode="MultiLine" Width="850px"></asp:TextBox>
                                                </td>
                                 </tr>
               
                        
                       
                        
     
                     </table>  

                     </asp:Panel>
                     <br />

                     <asp:Panel ID="panAddAuthor" runat="server" GroupingText="Author Details" Visible="false"  ForeColor="Black"  >
  
   <br />

<asp:SqlDataSource ID="SqlDataSourceAuthorType" runat="server" 
                ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" 
                ProviderName="System.Data.SqlClient"> 
                                     
            </asp:SqlDataSource>

<asp:Panel ID="PanelMU" runat="server" ScrollBars="Both"   Width="1000px" style="margin-right: 0px">

     <asp:GridView ID="Grid_AuthorEntry" runat="server" OnRowDataBound="OnRowDataBound" AutoGenerateColumns="False"  GridLines="None"
      CellPadding="4" ForeColor="#333333">

     <AlternatingRowStyle BackColor="White" />

     <Columns>

 <asp:TemplateField HeaderText="MAHE/Non MAHE">
             <ItemTemplate>
                 <asp:DropDownList ID="DropdownMuNonMu" runat="server" Width="160" AutoPostBack="true" DataSourceID="SqlDataSourceAuthorType" >
               
                 </asp:DropDownList>
                 
                 <asp:ImageButton ID="MuNonMu" runat="server" ImageUrl="~/Images/srchImg.gif" CssClass="blnkImgCSS" Visible="false" />

               

             </ItemTemplate>              
        </asp:TemplateField>

        <asp:TemplateField ShowHeader="true" HeaderText="Roll No/Employee Code">
             <ItemTemplate>
       <asp:TextBox ID="EmployeeCode" runat="server" Width="155" Visible="true"></asp:TextBox>
        <asp:Image ID="ImageEmpCode" runat="server" ImageUrl="~/Images/srchImg.gif" CssClass="blnkImgCSS" Visible="false"/>       
            </ItemTemplate>            
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Author Name">
             <ItemTemplate>
                 <asp:TextBox ID="AuthorName" runat="server" Width="200" Enabled="false" ></asp:TextBox>
             </ItemTemplate>              
        </asp:TemplateField>


        <asp:TemplateField HeaderText="Institution Name">
         <ItemTemplate>
             <asp:TextBox ID="InstitutionName" runat="server" Width="150" Enabled="false"></asp:TextBox> 
                <asp:DropDownList ID="DropdownStudentInstitutionName" runat="server" Width="150" Visible="false" AutoPostBack="true" DataSourceID="SqlDataSourceDropdownStudentInstitutionName" DataTextField="Institute_Name"  DataValueField="Institute_Id" >
                 
               
                 </asp:DropDownList>
               <asp:SqlDataSource ID="SqlDataSourceDropdownStudentInstitutionName" runat="server" 
                                         ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" 
                                         ProviderName="System.Data.SqlClient" 
                                         SelectCommand="select Institute_Id,Institute_Name from Institute_M">
                                     </asp:SqlDataSource>           
             </ItemTemplate>              
        </asp:TemplateField>

                      
       

        <asp:TemplateField HeaderText="Department Name/Course">
             <ItemTemplate>
                 <asp:TextBox ID="DepartmentName" runat="server" Width="180" Enabled="false"></asp:TextBox>
                     <asp:DropDownList ID="DropdownStudentDepartmentName" runat="server" Width="180" Visible="false"  DataSourceID="SqlDataSourceDropdownStudentDepartmentName" DataTextField="DeptName" DataValueField="DeptId" >
                 
              
                 </asp:DropDownList>

                      <asp:SqlDataSource ID="SqlDataSourceDropdownStudentDepartmentName" runat="server" 
                                         ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" 
                                         ProviderName="System.Data.SqlClient" 
                                         SelectCommand="select DeptId,DeptName from Dept_M where Institute_Id=@Institute_Id ">
                                           <SelectParameters>
   <%-- <asp:QueryStringParameter Name="Institute_Id" QueryStringField="Institute_Id" />--%>
     <asp:ControlParameter Name="Institute_Id" 
      ControlID="DropdownStudentInstitutionName"
      PropertyName="SelectedValue"/>
  </SelectParameters>
                                     </asp:SqlDataSource>
            </ItemTemplate>            
        </asp:TemplateField>

           <asp:TemplateField HeaderText="MailId">
             <ItemTemplate>
                 <asp:TextBox ID="MailId" runat="server" Width="250" Enabled="false" ></asp:TextBox>
    
             </ItemTemplate>  
                     
        </asp:TemplateField>

 <asp:TemplateField HeaderText="isCorrAuth">
             <ItemTemplate>
                 <asp:DropDownList ID="isCorrAuth" runat="server" Width="75" >
                 <asp:ListItem Value="Y">Yes</asp:ListItem>
                        <asp:ListItem Value="N">No</asp:ListItem>
                 </asp:DropDownList>

             </ItemTemplate>              
        </asp:TemplateField>

         <asp:TemplateField HeaderText="AuthorType">
             <ItemTemplate>
                 <asp:DropDownList ID="AuthorType" runat="server" Width="125" >
                       <asp:ListItem Value="0" Selected="True">-Select-</asp:ListItem>
                 <asp:ListItem Value="P">First Author</asp:ListItem>
                        <asp:ListItem Value="C">CO-Author</asp:ListItem>
                 </asp:DropDownList>
             </ItemTemplate>              
        </asp:TemplateField>

          <asp:TemplateField HeaderText="NameAsInJournal">
             <ItemTemplate>
          <asp:TextBox ID="NameInJournal" runat="server" Width="190" ></asp:TextBox>


             </ItemTemplate>              
        </asp:TemplateField>

       <asp:TemplateField HeaderText="IsPresenter">
             <ItemTemplate>
          <asp:DropDownList ID="IsPresenter" runat="server" Width="75" >
                 <asp:ListItem Value="N">No</asp:ListItem>
                 <asp:ListItem Value="Y">Yes</asp:ListItem>
                 
                 </asp:DropDownList>

             </ItemTemplate>              
        </asp:TemplateField>

               <asp:TemplateField HeaderText="HasAttented">
             <ItemTemplate>
          <asp:CheckBox ID="HasAttented" runat="server" Width="70" ></asp:CheckBox>

             </ItemTemplate>              
        </asp:TemplateField>

          
         <asp:TemplateField  HeaderText="Colloboration">
             <ItemTemplate>
                 <asp:DropDownList ID="NationalType" runat="server" Width="140"   >
                 <asp:ListItem Value="N">National</asp:ListItem>
                        <asp:ListItem Value="I">International</asp:ListItem>
                 </asp:DropDownList>
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
             </ItemTemplate>              
        </asp:TemplateField>

         
                        <asp:TemplateField ShowHeader="false">
         <ItemTemplate>
             <asp:HiddenField ID="Institution" runat="server" ></asp:HiddenField>  
                  
                     
          </ItemTemplate>              
        </asp:TemplateField>
             <asp:TemplateField ShowHeader="false">
             <ItemTemplate>
                 <asp:HiddenField ID="Department" runat="server" ></asp:HiddenField>
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
<asp:Panel ID="panelJournalArticle" runat="server" GroupingText="Journal Publish Details" Visible="false"  ForeColor="Black"  > 

  <br />

  <table>
  
  <tr align="center">
                              
     <td style="height: 36px; font-weight:normal"> <asp:Label ID="LabelIndexed" runat="server" Text="Indexed:" ForeColor="Black"  ></asp:Label></td>
     <td style="font-weight:normal" >   <asp:RadioButtonList ID="RadioButtonListIndexed" runat="server"    RepeatDirection="Horizontal"  Style="border-style:inset none none inset;">
                                        <asp:ListItem Value="N" >No</asp:ListItem>
                             <asp:ListItem Value="Y" Selected="True">Yes</asp:ListItem>
                  
 
                             </asp:RadioButtonList>  
     
     </td> 
     <td></td>
        <td></td>
              <td></td>
                  <td></td>
  
   <td style="height: 36px; font-weight:normal">
    

     <asp:Label ID="LabelIndexedIn" runat="server" Text="Indexed In" ForeColor="Black"  ></asp:Label></td>
     <td  style="font-weight:normal" >
  <asp:CheckBoxList ID="CheckboxIndexAgency" runat="server" ForeColor="Black"  DataSourceID="SqlDataSourceCheckboxIndexAgency" RepeatDirection="Horizontal" DataTextField="agencyname" DataValueField="agencyid" Enabled="false">
                
                </asp:CheckBoxList>

                
<asp:SqlDataSource ID="SqlDataSourceCheckboxIndexAgency" runat="server" ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" 
           SelectCommand="select agencyid,agencyname from IndexAgency_M where active='Y'">
     
                </asp:SqlDataSource>

     </td>
     <td></td>
         <td></td>
     

  </tr>
  </table>
 <table  style="width: 100%">
                    <tr>
                        <td style="height: 36px; font-weight:normal" >
                          <asp:Label ID="LabelPubJournal" runat="server" Text="ISSN" ForeColor="Black" ></asp:Label>
                                     <asp:Label ID="Labeljastar1" runat="server" Text="*" ForeColor="Red" Visible="false"></asp:Label>
                               </td>
                        <td style="height: 36px" >             
                             <asp:TextBox ID="TextBoxPubJournal" runat="server"  Width="220px" 
  Style="border-style:inset none none inset;"></asp:TextBox>
     

                       
                         </td>
                      
                        <td style="height: 36px; font-weight:normal" >
                           <asp:Label ID="LabelNameJournal" runat="server" Text="Name Of Journal" ForeColor="Black"  ></asp:Label>
                        </td>
                        <td style="height: 36px" >
                             <asp:TextBox ID="TextBoxNameJournal" runat="server"  Enabled="false" Width="600px" Style="border-style:inset none none inset;"></asp:TextBox>
         </td>

               
                           </tr>
                       </table>

                       <table >
                           <tr>

                           
                                <td style="height: 56px; font-weight:normal" >
                             <asp:Label ID="LabelMonthJA" runat="server" Text="Publish Month" ForeColor="Black"   ></asp:Label>
                                  <asp:Label ID="Labeljastr2" runat="server" Text="*" ForeColor="Red" Visible="false"></asp:Label></td>
                                  <td>
                              <asp:DropDownList ID="DropDownListMonthJA" runat="server" Style="border-style:inset none none inset;" DataSourceID="SqlDataSourcePubJAmonth"
      DataTextField="MonthName" DataValueField="MonthValue"  Width="50px" >
      

       
        </asp:DropDownList>   
          <asp:SqlDataSource ID="SqlDataSourcePubJAmonth" runat="server" ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" 
           SelectCommand="select MonthValue, MonthName from Publication_MonthM">
     
                </asp:SqlDataSource>
                        </td>
                        <td style="height: 56px; font-weight:normal" >
                     <asp:Label ID="LabelYearJA" runat="server" Text="Publish Year" ForeColor="Black"  ></asp:Label>
                          <asp:Label ID="Labeljastr3" runat="server" Text="*" ForeColor="Red" Visible="false" ></asp:Label></td><td>
                              <asp:DropDownList ID="TextBoxYearJA" runat="server" 
        Width="60px"  Style="border-style:inset none none inset;" >
        


        </asp:DropDownList>  

        
       
        
                        </td>
              
               <td style="height: 56px; font-weight:normal"> <asp:Label ID="LabelImpFact" runat="server" Text="1-Year Impact Factor" ForeColor="Black"  ></asp:Label></td> 
     <td  >
       <asp:TextBox ID="TextBoxImpFact" runat="server"  ReadOnly="true" Style="border-style:inset none none inset;" Width="100px" ></asp:TextBox>

     </td>

 

<td style="height: 56px; font-weight:normal"> <asp:Label ID="LabelImpFact5" runat="server" Text="5-Year Impact Factor" ForeColor="Black"  ></asp:Label></td> 
     <td  >
       <asp:TextBox ID="TextBoxImpFact5" runat="server"  ReadOnly="true" Style="border-style:inset none none inset;" Width="100px" ></asp:TextBox>

     </td>
        <td style="height: 56px; font-weight:normal"> <asp:Label ID="LblIFAY" runat="server" Text="IF-ApplicableYear" ForeColor="Black"  ></asp:Label></td> 
     <td  >
      <asp:TextBox ID="txtIFApplicableYear" runat="server"  ReadOnly="true" Style="border-style:inset none none inset;" Width="100px" ></asp:TextBox>
     </td>
     </tr>
     </table>

     <table>
     <tr>
     <td style="font-weight:normal" class="auto-style35">
                             <asp:Label ID="lblQuartile" runat="server" Text="Quartile" ForeColor="Black" ></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtquartile" runat="server" ReadOnly="true" Width="100px" Style="border-style:inset none none inset;" ></asp:TextBox>
                        </td>
                        <td  style="font-weight:normal" class="auto-style35">
                             <asp:Label ID="lblQuartileid" runat="server" Text="QuartileID" ForeColor="Black" Visible="false"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtquartileid" runat="server" ReadOnly="true" Width="100px" Visible="false" Style="border-style:inset none none inset;"></asp:TextBox>
                        </td>
      
     
  <td style="height: 36px; font-weight:normal">  
                  <asp:Label ID="LabelPageFrom" runat="server" Text="Page From" ForeColor="Black"  ></asp:Label>
                       <asp:Label ID="Labeljastr4" runat="server" Text="*" ForeColor="Red" Visible="false"></asp:Label>
                        </td>
                         
         <td>
      <asp:TextBox ID="TextBoxPageFrom" runat="server"  Width="100px" MaxLength="10" Style="border-style:inset none none inset;"></asp:TextBox> 
   </td>
     
                                <td style="height: 36px; font-weight:normal">
                    <asp:Label ID="LabelPageTo" runat="server" Text="Page To" ForeColor="Black"  ></asp:Label>
                         <asp:Label ID="Labeljastr5" runat="server" Text="*" ForeColor="Red" Visible="false"></asp:Label>
                        </td>
                        <td style="height: 36px" >
                         <asp:TextBox ID="TextBoxPageTo" runat="server"   Width="100px" MaxLength="10" Style="border-style:inset none none inset;"></asp:TextBox>
      
            </td>
            <td style="height: 36px; font-weight:normal">
                                <asp:Label ID="LabelVolume" runat="server" Text="Volume" ForeColor="Black"  ></asp:Label>
                                
                                  <asp:Label ID="Labelvolstar8" runat="server" Text="*" ForeColor="Red" Visible="false"></asp:Label>
                                </td>
                        <td style="height: 36px">             
                       <asp:TextBox ID="TextBoxVolume" runat="server"   Width="120px" Style="border-style:inset none none inset;"></asp:TextBox>
     
                       
                         </td>
                           <td style="height: 36px; font-weight:normal">  <asp:Label ID="Labelissue" runat="server" Text="Issue" ForeColor="Black"  ></asp:Label>
<%--          <asp:Label ID="Labeljastr7" runat="server" Text="*" ForeColor="Red" Visible="false"></asp:Label>--%>
      </td>

    <td  style="font-weight:normal" >  <asp:TextBox ID="TextBoxIssue" runat="server"   Width="210px" Style="border-style:inset none none inset;"></asp:TextBox></td>
                         </tr>
                       
    </table>
    <table>
   
    <tr>
      <td style="height: 36px; font-weight:normal">
                          <asp:Label ID="LabelDOINum" runat="server" Text="DOI Number" ForeColor="Black" ></asp:Label>
                               
                               </td>
                        <td style="height: 36px">             
                             <asp:TextBox ID="TextBoxDOINum" runat="server"   Width="250px" MaxLength="20" Style="border-style:inset none none inset;"></asp:TextBox>
     
                 
                       
                         </td>
                           
           <td style="height: 56px; font-weight:normal"> <asp:Label ID="LabelPubicationType" runat="server" Text="Publication Type" ForeColor="Black"  ></asp:Label></td> 
     <td  >
       <asp:DropDownList ID="DropDownListPubType" runat="server" Style="border-style:inset none none inset;" >
       <asp:ListItem Value="N">National</asp:ListItem>
        <asp:ListItem Value="I">International</asp:ListItem>
       </asp:DropDownList>

     </td>
                                
         
   <%--  <td style="height: 56px; font-weight:normal"> <asp:Label ID="lblCitation" runat="server" Text="Citation URL" ForeColor="Black"  ></asp:Label></td> 

    <td  style="font-weight:normal" >  <asp:TextBox ID="txtCitation" runat="server" MaxLength="200"   Width="580px" Style="border-style:inset none none inset;"></asp:TextBox></td>--%>

          </tr>
      </table>


  <br />

  <table>
    <tr>
  <td >
 </td>
  </tr>
 
                 
               
                     </table>  
  

</asp:Panel>


  <asp:Panel ID="panelConfPaper" runat="server" GroupingText=" Conference Paper" Visible="false" > 

  <br />
 <table width="100%" >
                    <tr>
                        <td style="height: 36px; font-weight:normal">
                          <asp:Label ID="LabelEventTitle" runat="server" Text="Conference Title" ></asp:Label>
                               </td>
                        <td style="height: 36px">             
                             <asp:TextBox ID="TextBoxEventTitle" runat="server"   Width="500px" MaxLength="200" Style="border-style:inset none none inset;"></asp:TextBox>
     
                 
                       
                         </td>
                      
   
                 <td style="height: 36px; font-weight:normal">
                                <asp:Label ID="LabelDate" runat="server" Text="Conference From Date" ></asp:Label>
                                                
                                
                                </td>
                        <td style="height: 36px">             
                       <asp:TextBox ID="TextBoxDate" runat="server"   Width="130px" Style="border-style:inset none none inset;"></asp:TextBox>

              </td>

                        <td style="height: 36px; font-weight:normal">
                                <asp:Label ID="LabelDate1" runat="server" Text="Conference To Date" ></asp:Label>
                                
                                </td>
                        <td style="height: 36px">             
                       <asp:TextBox ID="TextBoxDate1" runat="server"   Width="130px" Style="border-style:inset none none inset;"></asp:TextBox>
 </td></tr>

                           </table>

                            <table width="100%" >
                       
                <tr>
                                 <td style="height: 36px; font-weight:normal;width: 93px;">
                           <asp:Label ID="LabelISBN" runat="server" Text="ISBN" ></asp:Label>
                        </td>
                        <td style="height: 36px">
                             <asp:TextBox ID="TextBoxIsbn" runat="server"  Width="450px" MaxLength="50" Style="border-style:inset none none inset;"></asp:TextBox>
         </td>
                
                                     <td style="height: 36px; font-weight:normal">
                           <asp:Label ID="LabelPlace" runat="server" Text="Conference Venue" ></asp:Label>
                        </td>
                        <td style="height: 36px" >
                             <asp:TextBox ID="TextBoxPlace" runat="server"  Width="400px" MaxLength="50" Style="border-style:inset none none inset;"></asp:TextBox>
         </td>

                  

                </tr>       
                </table>
        
       <table width="100%">
          <tr>
              
                                 <td style="height: 36px; font-weight:normal;width: 93px;">
                           <asp:Label ID="lblCity" runat="server" Text="City" ></asp:Label>
                        </td>
                        <td style="height: 36px">
                             <asp:TextBox ID="txtcity" runat="server"  Width="250px" MaxLength="50" Style="border-style:inset none none inset;"></asp:TextBox>
         </td>
              <td class="auto-style38"></td>
                                 <td style="height: 36px; font-weight:normal">
                           <asp:Label ID="lblState" runat="server" Text="State/Country" ></asp:Label>
                        </td>
                        <td style="height: 36px">
                             <asp:TextBox ID="txtState" runat="server"  Width="250px" MaxLength="50" Style="border-style:inset none none inset;"></asp:TextBox>
         </td>
              <td class="auto-style38"></td>
               <td style="height: 36px; font-weight:normal">
                  <asp:Label ID="LabelConferencetype" runat="server" Text="Type Of Conference" ForeColor="Black"  ></asp:Label>
              </td>
              <td style="height: 36px; font-weight:normal">
                   <asp:DropDownList ID="DropDownListConferencetype" runat="server" Width="250px" DataTextField="Type" DataValueField="Id" Style="border-style:inset none none inset;" >
<asp:ListItem Value="0">--Select--</asp:ListItem>
        <asp:ListItem Value="N">National</asp:ListItem>
        <asp:ListItem Value="I">International</asp:ListItem>
     </asp:DropDownList>
     
       <%-- <asp:SqlDataSource ID="SqlDataSourceDropDownListConferencetype" runat="server" ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" 
           SelectCommand="select Id,Type from Conference_Type_M">
     
                </asp:SqlDataSource>     --%>                   
                  </td>
          </tr>
      </table>
             <table width="100%">
             <tr>
                   <td style="height: 56px; font-weight:normal;width: 93px;" >
                             <asp:Label ID="Label18" runat="server" Text="Publish Month" ForeColor="Black"   ></asp:Label>
                                  <asp:Label ID="Label19" runat="server" Text="*" ForeColor="Red" Visible="false"></asp:Label></td>
                                  <td>
                              <asp:DropDownList ID="DropDownListMonthCP" runat="server" Style="border-style:inset none none inset;" DataSourceID="SqlDataSourceCPPubJAmonth"
      DataTextField="MonthName" DataValueField="MonthValue"  Width="100px" AutoPostBack="true" AppendDataBoundItems="true">
      <asp:ListItem Value="0">-select-</asp:ListItem>

       
        </asp:DropDownList>   
          <asp:SqlDataSource ID="SqlDataSourceCPPubJAmonth" runat="server" ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" 
           SelectCommand="select MonthValue, MonthName from Publication_MonthM">
     
                </asp:SqlDataSource>
                        </td>
                   <td class="auto-style38"></td>
                        <td style="height: 56px; font-weight:normal" >
                     <asp:Label ID="Label20" runat="server" Text="Publish Year" ForeColor="Black"  ></asp:Label>
                          <asp:Label ID="Label21" runat="server" Text="*" ForeColor="Red" Visible="false" ></asp:Label></td><td>
                          <%--    <asp:DropDownList ID="TextBoxYearJA" runat="server" 
        Width="60px"  Style="border-style:inset none none inset;" AutoPostBack="true">
        </asp:DropDownList>  
                           --%>
           <asp:TextBox ID="TextBoxYearCP" runat="server"  Width="100px" MaxLength="10" AutoPostBack="true"
                 Style="border-style:inset none none inset;" ></asp:TextBox> 
           <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" Display="None"  ControlToValidate="TextBoxYearCP" ValidationGroup="validation"
                ErrorMessage="Entered Proper Year" SetFocusOnError="true"  
                ValidationExpression="^[0-9]{4}$">
                </asp:RegularExpressionValidator> 
        </td>
                  <td class="auto-style38"></td>
                  <td style="font-weight:normal" class="auto-style32">  
                  <asp:Label ID="Label22" runat="server" Text="Page From" ForeColor="Black"  ></asp:Label>
                       <%--<asp:Label ID="Labeljastr4" runat="server" Text="*" ForeColor="Red" Visible="false"></asp:Label>--%>
                        </td>
                         
         <td>
      <asp:TextBox ID="TextBoxPageFromCP" runat="server"  Width="100px" MaxLength="10" AutoPostBack="true"
                 Style="border-style:inset none none inset;" 
                 ontextchanged="TextBoxPageFromCP_TextChanged"></asp:TextBox> 
   </td>
     <td class="auto-style38"></td>
                                <td style="font-weight:normal" class="auto-style32">
                    <asp:Label ID="Label23" runat="server" Text="Page To" ForeColor="Black"  ></asp:Label>
                        <%-- <asp:Label ID="Labeljastr5" runat="server" Text="*" ForeColor="Red" Visible="false"></asp:Label>--%>
                        </td>
                        <td style="height: 36px" >
                         <asp:TextBox ID="TextBoxPageToCP" runat="server"   Width="100px" MaxLength="10" Enabled="false" Style="border-style:inset none none inset;"></asp:TextBox>
      
            </td>
                 <td>
                  <%--       <asp:DropDownList ID="TextBoxYear" runat="server" Style="border-style:inset none none inset;"
                             Width="100px" >

                          </asp:DropDownList> --%>
                 </td>
             </tr>
         </table>
      <table width="100%">
          <tr>
             
        <td style="height: 36px; font-weight:normal;width: 93px;"> <asp:Label ID="LblCPIndexed" runat="server" Text="Indexed:" ForeColor="Black"  ></asp:Label></td>
     <td style="font-weight:normal" >   <asp:RadioButtonList ID="RadioButtonListCPIndexed" runat="server"    RepeatDirection="Horizontal" OnSelectedIndexChanged="RadioButtonListCPIndexedOnSelectedIndexChanged" AutoPostBack="true" Style="border-style:inset none none inset;">
                                        <asp:ListItem Value="N" >No</asp:ListItem>
                             <asp:ListItem Value="Y" Selected="True">Yes</asp:ListItem>
                  
 
                             </asp:RadioButtonList>  
     
     </td> 
     <td class="auto-style22" ></td>
  <td class="auto-style16"></td>
  
   <td style="height: 36px; font-weight:normal;">
    

     <asp:Label ID="Label15" runat="server" Text="Indexed In:" ForeColor="Black"  ></asp:Label></td>
     <td  style="font-weight:normal" >
  <asp:CheckBoxList ID="CheckBoxListCPIndexedIn" runat="server" ForeColor="Black"  DataSourceID="SqlDataSourceCheckboxCPIndexAgency" RepeatDirection="Horizontal" DataTextField="agencyname" DataValueField="agencyid" Enabled="false">
                
                </asp:CheckBoxList>

                
<asp:SqlDataSource ID="SqlDataSourceCheckboxCPIndexAgency" runat="server" ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" 
           SelectCommand="select agencyid,agencyname from IndexAgency_M where active='Y'">
     
                </asp:SqlDataSource>

     </td>
      <td class="auto-style38"></td>
          <td style="height: 36px; font-weight:normal">
                          <asp:Label ID="Label2Presentation" runat="server" Text="Type Of Presentation:" ></asp:Label>
                               
                               </td>
                        <td style="height: 36px; font-weight:normal">             
                             <asp:RadioButtonList ID="RadioButtonListTypePresentaion" runat="server"  RepeatDirection="Horizontal"  Style="border-style:inset none none inset;">
                             <asp:ListItem Value="P">Poster</asp:ListItem>
                                     <asp:ListItem Value="O">Oral  </asp:ListItem>
                                     <asp:ListItem  Value="B">Both</asp:ListItem >
                             </asp:RadioButtonList>
     
                 
                       
                         </td>

             </tr>
             </table>
         
                 <table width="100%">
     
        <tr>
                         
                         
                    
                     
                      
                        <td style="height: 36px; font-weight:normal;width: 93px;">
                           <asp:Label ID="Label3" runat="server" Text="Credit Point" ></asp:Label>
                        </td>
                        <td style="height: 36px">
                             <asp:TextBox ID="TextBoxCreditPoint" runat="server" Width="100px" Text="0" Style="border-style:inset none none inset;" ></asp:TextBox>
         </td>

                 <td style="height: 36px; font-weight:normal">
                                <asp:Label ID="Label4" runat="server" Text="Awarded By" ></asp:Label></td>
                        <td style="height: 36px">             
                       <asp:TextBox ID="TextBoxAwardedBy" runat="server" Enabled="false"   Width="400px" Style="border-style:inset none none inset;"></asp:TextBox>
     
                         </td>
              <td style="height: 36px; font-weight:normal;width: 125px;">
                           <asp:Label ID="LabelFunds" runat="server" Text="Funds Utilized" ></asp:Label>
                        </td>
                        <td style="font-weight:normal" class="auto-style53">
                             <asp:TextBox ID="TextBoxFunds" runat="server"  Width="150px" MaxLength="50" Style="border-style:inset none none inset;"></asp:TextBox>
         </td>
                           </tr>
                       


               
                     </table>  
  

</asp:Panel>



  <asp:Panel ID="panelBookPublish" runat="server" GroupingText="Book Publish" Visible="false"> 
<%--  <center>
  Book Publish
  </center>--%>
  <br />
 <table  style="width: 100%" cellspacing="5">
                    <tr>
                        <td style="height: 36px; font-weight:normal">
                          <asp:Label ID="LabelTitileBook" runat="server" Text="Title Of The Book" ></asp:Label>
                             <asp:Label ID="Labelstr" runat="server" Text="*"  ForeColor="Red"></asp:Label>   
                               </td>
                        <td style="height: 36px">             
                             <asp:TextBox ID="TextBoxTitleBook" runat="server"  Width="880px" MaxLength="100"  Rows="2" TextMode="MultiLine" Style="border-style:inset none none inset;"></asp:TextBox>
     
                 
                       
                         </td>
                      
                    
                           </tr>
                     
                        <tr>
                      <td style="height: 36px; font-weight:normal">
                           <asp:Label ID="LabelTitleChapterContributed" runat="server" Text="Title Of the Chapter Contributed" ></asp:Label>
                                    <asp:Label ID="Label2str" runat="server" Text="*"  ForeColor="Red"></asp:Label>  
                        </td>
                        <td style="height: 36px">
                             <asp:TextBox ID="TextBoxChapterContributed" runat="server"   Width="880px" MaxLength="100" Rows="2" TextMode="MultiLine" Style="border-style:inset none none inset;"></asp:TextBox>
         </td>
         </tr>
         </table>
     
         <table style="width: 100%" >
         <tr>
         

                 <td style="font-weight:normal" class="auto-style8"  >
                                <asp:Label ID="LabelEdition" runat="server" Text="Edition" ></asp:Label>
                                         <asp:Label ID="Label5" runat="server" Text="*"  ForeColor="Red"></asp:Label>  
                                </td>
                        <td colspan="3">             
                       <asp:TextBox ID="TextBoxEdition" runat="server"   Width="304px" MaxLength="50"  Style="border-style:inset none none inset;"></asp:TextBox>
     
                       
                         </td>
              
                       <td style="width:20px"></td>
                   

                           
                                <td style="font-weight:normal">
                             <asp:Label ID="LabelPublisher" runat="server" Text="Publisher" ></asp:Label>
                                      <asp:Label ID="Label6" runat="server" Text="*"  ForeColor="Red"></asp:Label>  
                        </td>
                        <td   colspan="3">
                     <asp:TextBox ID="TextBoxPublisher" MaxLength="50" Width="308px"  runat="server" Style="border-style:inset none none inset;"
     ></asp:TextBox>   
        
                        </td>
             </tr>
       </table>
      <table style="width: 100%" >
          <tr>
               <td style="font-weight:normal" class="auto-style8">
                           <asp:Label ID="lblbISBN" runat="server" Text="ISBN" ></asp:Label>
                        </td>
                        <td colspan="3">
                             <asp:TextBox ID="txtbISBN" runat="server"  Width="400px" MaxLength="50" Style="border-style:inset none none inset;"></asp:TextBox>
         </td>
              <td style="font-weight:normal">
                           <asp:Label ID="lblbSection" runat="server" Text="Section" ></asp:Label>
                        </td>
                        <td colspan="3">
                             <asp:TextBox ID="txtbSection" runat="server"  Width="350px" MaxLength="50" Style="border-style:inset none none inset;"></asp:TextBox>
         </td>
          </tr>
          </table>
      <table style="width: 100%" >
          <tr>
                <td style="font-weight:normal" class="auto-style8">
                     <asp:Label ID="lblbChapter" runat="server" Text="Chapter" ></asp:Label>
                        </td>
                        <td >
                             <asp:TextBox ID="txtChapter" runat="server"  Width="250px"  Style="border-style:inset none none inset;"></asp:TextBox>
                         
         </td>
              <td>
              </td>
              <td style="font-weight:normal">
                           <asp:Label ID="lblbCountry" runat="server" Text="Country" ></asp:Label>
                        </td>
                        <td >
                             <asp:TextBox ID="txtCountry" runat="server"  Width="250px" Style="border-style:inset none none inset;"></asp:TextBox> 
         </td>
               <td>
              </td>
             <td style="font-weight:normal">
                           <asp:Label ID="lblbPublicationType" runat="server" Text="Type of Publication" ></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="DropDownListBookPublicationType" runat="server" Width="200px" DataTextField="Type" DataValueField="Id" Style="border-style:inset none none inset;" >
<asp:ListItem Value="0">--Select--</asp:ListItem>
        <asp:ListItem Value="C">Chapter in a book</asp:ListItem>
        <asp:ListItem Value="F">Full book</asp:ListItem>
     </asp:DropDownList>
         </td>
          </tr>
          </table>
      <table style="width: 100%">
             <tr>
     
      <td style="font-weight:normal" class="auto-style8"  >  
                  <asp:Label ID="LabelYear" runat="server" Text="Year" ></asp:Label>
                         <span>  <asp:Label ID="Label7" runat="server" Text="*"   ForeColor="Red"></asp:Label> </span> 
                        </td>
                         
         <td>

         
                      <asp:DropDownList ID="TextBoxYear" runat="server" Style="border-style:inset none none inset;"
                             Width="100px" >

                          </asp:DropDownList> 

 

   </td>
   <td style="font-weight:normal">  
                  <asp:Label ID="LabelMonth" runat="server" Text="Month" ></asp:Label>
                           <asp:Label ID="Label8" runat="server" Text="*"  ForeColor="Red"></asp:Label>  
                        </td>
                         
         <td>


                      <asp:DropDownList ID="DropDownListBookMonth" runat="server" Style="border-style:inset none none inset;"
                             DataSourceID="SqlDataSourcePubBookMonth" DataTextField="MonthName" DataValueField="MonthValue" Width="100px"  >

                          </asp:DropDownList> 
            <asp:SqlDataSource ID="SqlDataSourcePubBookMonth" runat="server" 
                ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" 
                
                SelectCommand="select MonthValue, MonthName from Publication_MonthM" 
                ProviderName="System.Data.SqlClient">
             
            </asp:SqlDataSource>
   </td>

     
                                <td style="font-weight:normal">
                    <asp:Label ID="Labelpagenum" runat="server" Text="Page Number" ></asp:Label>
                             <asp:Label ID="Label9" runat="server" Text="*"  ForeColor="Red"></asp:Label>  
                        </td>
                        <td>
                         <asp:TextBox ID="TextBoxPageNum" runat="server"   Width="100px" MaxLength="10" Style="border-style:inset none none inset;"></asp:TextBox>
      
            </td>
  <td style="font-weight:normal"> <asp:Label ID="LabelVolume1" runat="server" Text="Volume"  ></asp:Label>
           <asp:Label ID="Label10" runat="server" Text="*"  ForeColor="Red"></asp:Label>  
  </td> 
     <td  >
  <asp:TextBox ID="TextBoxVolume1" runat="server"  MaxLength="10"  Width="100px" Style="border-style:inset none none inset;"></asp:TextBox>
       

     </td>

                       </tr>
</table>


  

</asp:Panel>


 <asp:Panel ID="panelOthes" runat="server" GroupingText="NewsPaper/Magazine" Visible="false" BorderColor="Gray" BorderWidth="1px"> 
<%-- <center>
 NewsPaper/Magazine
 </center>--%>
 <br />
 <table  >
                    <tr>
                        <td style="height: 36px; font-weight:normal">
                          <asp:Label ID="LabelPublisherNewsPaper" runat="server" Text="Publisher" ></asp:Label>
                               
                               </td>
                        <td style="height: 36px">             
                             <asp:TextBox ID="TextBoxNewsPublish" runat="server"   Width="580px" MaxLength="50" Style="border-style:inset none none inset;"></asp:TextBox>
     
                 
                       
                         </td>
                      
                        <td style="height: 36px; font-weight:normal">
                           <asp:Label ID="LabelDateOfPublish" runat="server" Text="Date Of Publish" ></asp:Label>
                        </td>
                        <td style="height: 36px">
                             <asp:TextBox ID="TextBoxDateOfNewsPublish" runat="server"  Width="100px" Style="border-style:inset none none inset;"></asp:TextBox>




         </td>

                 <td style="height: 36px; font-weight:normal">
                                <asp:Label ID="LabelPageNumNewsPaper" runat="server" Text="PageNum" ></asp:Label></td>
                        <td style="height: 36px">             
                       <asp:TextBox ID="TextBoxPageNumNewsPaper" runat="server"   Width="90px" Style="border-style:inset none none inset;"></asp:TextBox>
     
                       
                         </td>
                           </tr>
                       
                       
     
    


               
                     </table>  

  

</asp:Panel>
</asp:Panel>
<br />
<br />

  <asp:Panel ID="panelTechReport" runat="server" Visible="false" GroupingText="Generic Details" ForeColor="Black" Style="font-weight:bold; border-style:inset;background-color:#E0EBAD" > 


<%--   <center>
Generic Details
 </center>--%>
  <center>
 <table border="1" style="width: 100%">
                   <%-- <tr>
                        <td style="height: 36px; font-weight:normal">
                             <asp:Label ID="LabelURL" runat="server" Text="Official URL"  ForeColor="Black"  ></asp:Label>
                        </td>
                        <td style="height: 36px" colspan="3">
                     <asp:TextBox ID="TextBoxURL" runat="server" MaxLength="200"  Enabled="false" Style="border-style:inset none none inset;"
        Width="900px" ></asp:TextBox>   
        
                        </td>
                     
                     

               
                           </tr>--%>
                       <tr>
                         <td style="height: 36px; font-weight:normal">
                                <asp:Label ID="LabelAbstract" runat="server" Text="Abstract" ForeColor="Black" ></asp:Label>
                                </td>
                        <td style="height: 36px" colspan="3" >             
                       <asp:TextBox ID="TextBoxAbstract" runat="server"   Width="900px" Enabled="false" TextMode="MultiLine" MaxLength="250" Style="border-style:inset none none inset;"></asp:TextBox>
     
                       
                         </td>

                        
                       </tr>
                           <tr>

                                    <td style="height: 36px; font-weight:normal">
                           <asp:Label ID="LabelkeyWords" runat="server" Text="Keywords" ForeColor="Black" ></asp:Label>
                        </td>
                        <td style="height: 36px">
                             <asp:TextBox ID="TextBoxKeywords" runat="server" Enabled="false"  Width="400px" MaxLength="200" Style="border-style:inset none none inset;"></asp:TextBox>
         </td>
                     <td style="height: 36px; font-weight:normal">
                    <asp:Label ID="LabelisErf" runat="server" Text="ERF Related?" ForeColor="Black"  ></asp:Label>
                        </td>
                        <td style="height: 36px">
                         <asp:DropDownList ID="DropDownListErf" runat="server" Enabled="false" Style="border-style:inset none none inset;">
                          <asp:ListItem Value="N">No</asp:ListItem>
                         <asp:ListItem Value="Y">Yes</asp:ListItem>
                          
                         </asp:DropDownList>
                            <asp:Label ID="Label234" runat="server" Text="( Environmental Research Fund )" ForeColor="Black" ></asp:Label>
                       
      
            </td>
                       <%--         <td style="height: 36px; font-weight:normal">
                             <asp:Label ID="LabelReferences" runat="server" Text="References" ForeColor="Black"  ></asp:Label>
                        </td>
                        <td style="height: 36px">
                     <asp:TextBox ID="TextBoxReference" runat="server" MaxLength="50" Style="border-style:inset none none inset;"
        Width="400px" ></asp:TextBox>   
        
                        </td>--%>
           
             </tr>
                          
             <tr>
     
     
           <td>Upload</td>               
         <td>
                  <asp:FileUpload ID="FileUploadPdf" runat="server" BorderColor="#996600" BorderStyle="Inset" />
                      
                
           <asp:Button ID="Buttonupload" runat="server" Text="Upload" OnClick="BtnUploadPdf_Click"  CausesValidation="true" ValidationGroup="validationUpload" ></asp:Button>
             <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
               ErrorMessage="!Upload only PDF(.pdf,.PDF) file" ControlToValidate="FileUploadPdf" ValidationGroup="validation"
            
                ValidationExpression="^.*\.((p|P)(d|D)(f|F))$" 
                ForeColor="Red"></asp:RegularExpressionValidator>
                <br />
                <br />
                
            
                  <asp:GridView ID="GVViewFile" runat="server" AutoGenerateColumns="False" EmptyDataText="No records found"
            DataSourceID="DSforgridview" onselectedindexchanged="GVViewFile_SelectedIndexChanged"
            HeaderStyle-ForeColor="White" 
         PagerStyle-ForeColor="White" CellPadding="3" 
        CellSpacing="3" PagerStyle-Width="4" PagerStyle-Height="4" 
      BorderStyle="Solid">
            <Columns>
                <asp:TemplateField ShowHeader="false">
            <ItemTemplate>
                <asp:ImageButton ID="btnView" runat="server" 
                    CausesValidation="False" CommandName="Select"
                    ImageUrl="~/Images/view.gif" ToolTip="View File" />
            </ItemTemplate>
          </asp:TemplateField>

          <asp:TemplateField Visible="false">
              <ItemTemplate>
                  <asp:Label ID="lblgetid" runat="server" Text='<%# Eval("UploadPDFPath") %>' ></asp:Label>
              </ItemTemplate>
            </asp:TemplateField>
              
           
            </Columns>
           <HeaderStyle BackColor="#0b532d" Font-Bold="True"  />
  
      
        </asp:GridView>


        <asp:SqlDataSource ID="DSforgridview" runat="server" 
            ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" 
            SelectCommand="  ">
           
        </asp:SqlDataSource>

 <asp:Label ID="lblNoPDF" runat="server" Text="No PDF Found" Visible="false"></asp:Label>

   </td>
     <td style="height: 36px; font-weight:normal">
                             <asp:Label ID="LabeluploadEPrint" runat="server" Width="100px" Text="Upload To EPrint" ForeColor="Black"  ></asp:Label>
                        </td>
                        <td style="height: 36px">
                     <asp:DropDownList ID="DropDownListuploadEPrint" Enabled="false" runat="server"  Style="border-style:inset none none inset;"
        Width="200px" >
           <asp:ListItem Value="N">No</asp:ListItem>
           <asp:ListItem Value="Y">Yes</asp:ListItem>
                        
        
        </asp:DropDownList>   
        
                        </td>
    
                       </tr>

          <tr align="center">
     
     <td colspan="6" style="font-weight:normal">
     <asp:Label ID="Eprint" runat="server" Text="EPrint" Font-Bold="true"  ForeColor="Black" ></asp:Label>
     
     </td>
     
     </tr>
  
                        <tr>
                         <td style="height: 36px; font-weight:normal" >
                             <asp:Label ID="Label1" runat="server" Text="EprintURL"  ForeColor="Black" ></asp:Label>
                        </td>
                        <td style="height: 36px" colspan="3">
                     <asp:TextBox ID="TextBoxEprintURL" runat="server" Enabled="false" ReadOnly="true" Style="border-style:inset none none inset;"
        Width="900px" ></asp:TextBox>   
        
                        </td>
                         
                       
                       </tr>
     

               
               
                     </table>  
  </center>
  <asp:Panel ID="panelRemarks" runat="server" Visible="false" Enabled="false" GroupingText="Remarks"> 
 <table style="width: 92%">
                    <tr>
                        <td style="height: 36px; font-weight:normal">
                             <asp:Label ID="Labelrem" runat="server" Text="Remarks"  ></asp:Label>
                        </td>
                        <td style="height: 36px">
                     <asp:TextBox ID="TextBoxRemarks" runat="server" TextMode="MultiLine" Style="border-style:inset none none inset;" Enabled="false"
        Width="900px" ></asp:TextBox>   
        
                        </td>
                      
                     

               
                           </tr>
                           </table>
</asp:Panel>

</asp:Panel>

  
 <br />


     <asp:Panel ID="panel2" runat="server" Visible="false"  Style="font-weight:bold; border-style:inset;background-color:#E0EBAD"   > 
      <asp:Panel ID="panel3" runat="server" Visible="true" GroupingText="Revert Comment"  > 

 <table style="width: 92%">
                    <tr>
                        <td style="height: 36px; font-weight:normal">
                             <asp:Label ID="Label2" runat="server" Text="Remarks"  ></asp:Label>
                        </td>
                        <td style="height: 36px">
                     <asp:TextBox ID="RevertComment" runat="server" TextMode="MultiLine" Style="border-style:inset none none inset;" Enabled="true"
        Width="900px" ></asp:TextBox>   
        
                        </td>
                      
                     

               
                           </tr>
                           </table>
          </asp:Panel>
         <br />

         <table width="100%">
    <tr>
        <td align="center">
          
            <asp:Button ID="btnSave" runat="server" Text="Revert To Author" OnClick="BtnSave_Click" Enabled="true" OnClientClick="ConfirmButtonJournal()" CausesValidation="false"  ></asp:Button>
   
 </td>
 </tr>
</table>
    </asp:Panel>


<br />



<br />



<asp:HiddenField ID="rowVal" runat="server" />
    <asp:HiddenField ID="senderID" runat="server" />


</asp:Content>

