<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPageRMS.master" AutoEventWireup="true" CodeFile="~/PublicationEntry/PublicationEdit.aspx.cs" Inherits="PublicationEdit" MaintainScrollPositionOnPostback="true" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<b> page under contruction</b>
</asp:content>

<%--<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

   <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" DisplayMode="BulletList" 
     HeaderText="Validation issues" ShowSummary="False" ValidationGroup="validation" />

<center>


  <style type="text/css">
.modelBackground
{
	background-color:Gray;
	filter:alpha(opacity=70);
	opacity:0.7;
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

.blnkImgCSS
{
	opacity: 0;
	filter: alpha(opacity=0);
}

</style>
 <script type = "text/javascript">
     function AddConfirm() {
         var rid = document.getElementById('<%= TextBoxPubId.ClientID %>').value;

         var confirm_value2 = document.createElement("INPUT");
         confirm_value2.type = "hidden";
         confirm_value2.name = "confirm_value2";
         confirm_value2.value = "Yes";

         if (rid != "") {
             if (confirm("Data Will be lost. Do you want to continue?"))
                 confirm_value2.value = "Yes";
             else
                 confirm_value2.value = "No";
         }

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
            <asp:ToolkitScriptManager ID="Scriptmanager1" runat="server"/>

            <asp:Label ID="lablPanelTitle" runat="server" Text="Publication Edit" Font-Bold="true" ></asp:Label>
            <br />
              <br />
            <asp:Panel ID="panelSearchPub" runat="server" GroupingText="Search" Font-Bold="true" Style="background-color:#E0EBAD"> 
                  <br />
    
                 <table border="1" style="width: 100%">
                    <tr>
    
                        <td style="height: 36px">
                            <b>Publication ID </b>
                        </td>
                        <td style="height: 36px">
                          <asp:TextBox ID="PubIDSearch"  runat="server" ></asp:TextBox>
                        </td>

                                  <td style="height: 36px">
                            <b>Publication Status</b></td>
                        <td style="height: 36px">             
                            <asp:DropDownList ID="drpPubStatusSearch" runat="server"   DataSourceID="SqlDataSource2" DataTextField="StatusName" DataValueField="StatusId" AppendDataBoundItems="true">
             <asp:ListItem Value="A">ALL</asp:ListItem>
                           
                          </asp:DropDownList> 
        <asp:SqlDataSource ID="SqlDataSource2" runat="server" 
                ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" 
                
                SelectCommand=" select StatusId,StatusName from Status_Publication_M" 
                ProviderName="System.Data.SqlClient">
             
            </asp:SqlDataSource>
                       
                         </td>
                  

                
                         
                        <td style="height: 36px">  
                              <asp:Button ID="ButtonSearchPub" runat="server" Text="Search" OnClick="ButtonSearchPubOnClick"  />
                        </td>
                      
       </tr>
               
                     </table>  
                     </asp:Panel>
                     <br />


                        <asp:GridView ID="GridViewSearch" runat="server"  AllowPaging="True" OnPageIndexChanging="GridViewSearchPub_PageIndexChanging"
        PagerSettings-PageButtonCount="5" PageSize="5" DataSourceID="SqlDataSource1"  EmptyDataText="No Data found"
        HeaderStyle-BackColor="#CC6600" HeaderStyle-ForeColor="White" AutoGenerateColumns="false"
        PagerStyle-BackColor="#CC6600" PagerStyle-ForeColor="White" CellPadding="3"  OnRowDataBound="GridView2_RowDataBound" OnRowEditing="edit" OnRowCommand="GridView2_RowCommand"
        CellSpacing="3" PagerStyle-Width="4" PagerStyle-Height="4" Visible="False" 
        BorderColor="#FF6600" BorderStyle="Solid">
        <Columns>

     
   <asp:TemplateField ShowHeader="False" >
   <ItemTemplate>
   <asp:ImageButton ID="BtnEdit" runat="server" CausesValidation="False" CommandName="Edit" ImageUrl="~/Images/edit.gif" ToolTip="Edit" Visible="false" />
   </ItemTemplate>
   </asp:TemplateField>

          <asp:TemplateField ShowHeader="False">
   <ItemTemplate>
   <asp:Button ID="BtnEditGridViewView" runat="server" CausesValidation="False" CommandName="View"  Text="Edit"  ToolTip="Edit" />
   </ItemTemplate>
   </asp:TemplateField>
        <asp:BoundField DataField="PublicationID" ReadOnly="true" HeaderText="PublicationID" 
                SortExpression="PublicationID" />

            <asp:BoundField DataField="TypeOfEntry" ReadOnly="true" HeaderText="TypeOfEntry" 
                SortExpression="TypeOfEntry" />

                  <asp:BoundField DataField="PubJournalID" ReadOnly="true" HeaderText="PubJournalID" 
                SortExpression="PubJournalID" />
  
            <asp:BoundField DataField="PublishJAMonth" ReadOnly="true" HeaderText="PublishJAMonth" 
                SortExpression="PublishJAMonth" />

                <asp:BoundField DataField="PublishJAYear" ReadOnly="true" HeaderText="PublishJAYear" 
                SortExpression="PublishJAYear" />
           <asp:BoundField DataField="ImpactFactor" ReadOnly="true" HeaderText="ImpactFactor" 
                SortExpression="ImpactFactor" />
        <asp:BoundField DataField="StatusName" ReadOnly="true" HeaderText="Status" 
                SortExpression="StatusName" />

            
        </Columns>

           <HeaderStyle BackColor="#0b532d" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#0b532d" ForeColor="White" HorizontalAlign="Center" />
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
<table width="100%">
    <tr>
        <td align="center">
          <asp:Button ID="Buttonadd" runat="server" Text="Add Publication" OnClick="addclik" OnClientClick="AddConfirm()" Visible="false"></asp:Button>
          </td>
          </tr>
          </table>
              <br />

<asp:Panel ID="panel" runat="server" GroupingText=" " Visible="false" BackColor="#E0EBAD"> 


 <table border="1" style="width: 100%">
                    <tr>
                        <td style="height: 36px">
                          <asp:Label ID="TypeEntry" runat="server"  Text="Entry Type:" Font-Bold="true"></asp:Label>
                               
                               </td>
                        <td style="height: 36px" colspan="2">             
                             <asp:DropDownList ID="DropDownListPublicationEntry" runat="server"  DataSourceID="SqlDataSourcePublicationEntry" DataTextField="EntryName" Enabled="false"
                              DataValueField="TypeEntryId" AppendDataBoundItems="true"  Width="200px" OnSelectedIndexChanged="DropDownListPublicationEntryOnSelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem Value=" ">--Select--</asp:ListItem>
                    
                             </asp:DropDownList>
                             <asp:SqlDataSource ID="SqlDataSourcePublicationEntry" runat="server" 
                ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" 
                
                SelectCommand="select TypeEntryId,EntryName from PublicationTypeEntry_M" 
                ProviderName="System.Data.SqlClient">
           
            </asp:SqlDataSource>
                         
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please select Type Of Entry"
         ValidationGroup="validation" ControlToValidate="DropDownListPublicationEntry" Display="None" InitialValue=" "></asp:RequiredFieldValidator>
                       
                         </td>
                      
                        <td style="height: 36px" >
                           <asp:Label ID="lblMUCat" runat="server" Text="MU Categorization:" Font-Bold="true"></asp:Label>
                        </td>
                        <td style="height: 36px" colspan="2">
                            <asp:DropDownList ID="DropDownListMuCategory" runat="server"   Width="200px"  DataSourceID="SqlDataSourceMuCategory" DataTextField="PubCatName"
                              DataValueField="PubCatId" AppendDataBoundItems="true"  >
                                    <asp:ListItem Value=" ">--Select--</asp:ListItem>
                             
                             </asp:DropDownList>   
                             
                                     <asp:SqlDataSource ID="SqlDataSourceMuCategory" runat="server" 
                ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" 
                
                SelectCommand="select PubCatId,PubCatName from PubMUCategorization_M" 
                ProviderName="System.Data.SqlClient">
           
            </asp:SqlDataSource> 
            
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please select MU Categorization"
         ValidationGroup="validation" ControlToValidate="DropDownListMuCategory" Display="None" InitialValue=" "></asp:RequiredFieldValidator>
             </td>

  <td>
          <asp:Label ID="LabelPubId" runat="server" Text="PublicationId :" Font-Bold="true" ></asp:Label>
     </td>

     <td>
         <asp:TextBox ID="TextBoxPubId" runat="server"   Width="200px" Enabled="false"></asp:TextBox>
     
     </td>

                             </tr>
                             <tr>
                 <td style="height: 36px">
                                <asp:Label ID="lblTitileWorkItem" runat="server" Text="Title Of the Work Item:" Font-Bold="true" ></asp:Label></td>
                        <td style="height: 36px" colspan="7">             
                       <asp:TextBox ID="txtboxTitleOfWrkItem" runat="server"   Width="900px" TextMode="MultiLine"></asp:TextBox>
          <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Please Enter Title Of Work Item"
         ValidationGroup="validation" ControlToValidate="txtboxTitleOfWrkItem" Display="None"></asp:RequiredFieldValidator>
                       
                         </td>
                           </tr>
                       
                        
     


               
                     </table>  
                     <br />

                     <asp:Panel ID="panAddAuthor" runat="server" GroupingText="Author Details" Visible="false" Font-Bold="true">
       

  


<asp:Panel ID="PanelMU" runat="server" ScrollBars="Both" BorderStyle="Double"   Width="1000px" style="margin-right: 0px">

     <asp:GridView ID="Grid_AuthorEntry" runat="server" AutoGenerateColumns="False"  GridLines="None"
     OnRowDeleting="Grid_AuthorEntry_RowDeleting" CellPadding="4" ForeColor="#333333" Enabled="false">

     <AlternatingRowStyle BackColor="White" />

     <Columns>


     
        <asp:TemplateField HeaderText="MU/Non MU">
             <ItemTemplate>
                 <asp:DropDownList ID="DropdownMuNonMu" runat="server" Width="90" OnSelectedIndexChanged="DropdownMuNonMuOnSelectedIndexChanged" AutoPostBack="true" >
                 
                 <asp:ListItem Value="M">MU-Staff</asp:ListItem>
                               <asp:ListItem Value="S">MU-Student</asp:ListItem>
                        <asp:ListItem Value="N">Non MU</asp:ListItem>
                 </asp:DropDownList>
                 
                 <asp:ImageButton ID="MuNonMu" runat="server" ImageUrl="~/Images/srchImg.gif" CssClass="blnkImgCSS"  />

               

             </ItemTemplate>              
        </asp:TemplateField>

        <asp:TemplateField ShowHeader="false">
             <ItemTemplate>
                 <asp:HiddenField ID="EmployeeCode" runat="server"  ></asp:HiddenField>
       
   

            </ItemTemplate>            
        </asp:TemplateField>

        <asp:TemplateField HeaderText="Author Name">
             <ItemTemplate>
                 <asp:TextBox ID="AuthorName" runat="server" Width="200" Enabled="false"></asp:TextBox>
          
                 <asp:ImageButton ID="EmployeeCodeBtn" runat="server" ImageUrl="~/Images/srchImg.gif" OnClientClick="setRow(this)" />

                 <asp:ModalPopupExtender ID="ModalPopupExtender4" runat="server" TargetControlID="EmployeeCodeBtn" PopupControlID="popupPanelAffil"
                    BackgroundCssClass="modelBackground"  >
                                  </asp:ModalPopupExtender>
             </ItemTemplate>              
        </asp:TemplateField>





        



        <asp:TemplateField HeaderText="Institution Name">
         <ItemTemplate>
             <asp:TextBox ID="InstitutionName" runat="server" Width="130" Enabled="false"></asp:TextBox> 
             
             <asp:Image ID="Image4" runat="server" ImageUrl="~/Images/srchImg.gif" CssClass="blnkImgCSS" />           
             </ItemTemplate>              
        </asp:TemplateField>

                      
       

        <asp:TemplateField HeaderText="Department Name">
             <ItemTemplate>
                 <asp:TextBox ID="DepartmentName" runat="server" Width="100" Enabled="false"></asp:TextBox>

                 <asp:Image ID="Image5" runat="server" ImageUrl="~/Images/srchImg.gif" CssClass="blnkImgCSS" />
            </ItemTemplate>            
        </asp:TemplateField>

           <asp:TemplateField HeaderText="MailId">
             <ItemTemplate>
                 <asp:TextBox ID="MailId" runat="server" Width="200"  Enabled="false" ></asp:TextBox>
                 
                 <asp:Image ID="ImageMailId" runat="server" ImageUrl="~/Images/srchImg.gif" CssClass="blnkImgCSS" />
     

             </ItemTemplate>              
        </asp:TemplateField>

 <asp:TemplateField HeaderText="isCorrAuth">
             <ItemTemplate>
                 <asp:DropDownList ID="isCorrAuth" runat="server" Width="75" >
                 <asp:ListItem Value="Y">Yes</asp:ListItem>
                        <asp:ListItem Value="N">No</asp:ListItem>
                 </asp:DropDownList>

                 <asp:Image ID="ImageisCorrAuth" runat="server" ImageUrl="~/Images/srchImg.gif" CssClass="blnkImgCSS" />

                

             </ItemTemplate>              
        </asp:TemplateField>

         <asp:TemplateField HeaderText="AuthorType">
             <ItemTemplate>
                 <asp:DropDownList ID="AuthorType" runat="server" Width="125" >
                 <asp:ListItem Value="P">Primary Author</asp:ListItem>
                        <asp:ListItem Value="C">CO-Author</asp:ListItem>
                 </asp:DropDownList>

                 <asp:Image ID="ImageisAuthorType" runat="server" ImageUrl="~/Images/srchImg.gif" CssClass="blnkImgCSS" />

                

             </ItemTemplate>              
        </asp:TemplateField>

          <asp:TemplateField HeaderText="NameAsInJournal">
             <ItemTemplate>
          <asp:TextBox ID="NameInJournal" runat="server" Width="160" ></asp:TextBox>

                 
                 <asp:Image ID="ImageNameInJournal" runat="server" ImageUrl="~/Images/srchImg.gif" CssClass="blnkImgCSS" />

           

                

             </ItemTemplate>              
        </asp:TemplateField>

          <asp:TemplateField HeaderText="IsPresenter">
             <ItemTemplate>
          <asp:DropDownList ID="IsPresenter" runat="server" Width="75" OnSelectedIndexChanged="IsPresenterIsPresenter"  AutoPostBack="true">
                 <asp:ListItem Value="N">No</asp:ListItem>
                 <asp:ListItem Value="Y">Yes</asp:ListItem>
                 
                 </asp:DropDownList>
                 
                 <asp:Image ID="ImageIsPresenter" runat="server" ImageUrl="~/Images/srchImg.gif" CssClass="blnkImgCSS" />

           

                

             </ItemTemplate>              
        </asp:TemplateField>

               <asp:TemplateField HeaderText="HasAttented">
             <ItemTemplate>
          <asp:CheckBox ID="HasAttented" runat="server" Width="70" ></asp:CheckBox>

                 
                 <asp:Image ID="ImageHasAttented" runat="server" ImageUrl="~/Images/srchImg.gif" CssClass="blnkImgCSS" />

           

                

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
        <HeaderStyle BackColor="#0b532d" Font-Bold="True" ForeColor="White" />
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


  <asp:Panel ID="panelJournalArticle" runat="server" GroupingText="Journal Publish Details" Visible="false"  ForeColor="Black"  > 

  <br />
 <table  style="width: 100%">
 <tr>

 </tr>
                    <tr>
                        <td style="height: 36px; font-weight:normal" >
                          <asp:Label ID="LabelPubJournal" runat="server" Text="ISSN" ForeColor="Black" ></asp:Label>
                               
                               </td>
                        <td style="height: 36px" >             
                             <asp:TextBox ID="TextBoxPubJournal" runat="server"   Width="220px" OnTextChanged="JournalIDTextChanged"  AutoPostBack="true" Style="border-style:inset none none inset;"></asp:TextBox>
     
                       <asp:ImageButton ID="imageBkCbtn" runat="server" ImageUrl="~/Images/srchImg.gif" OnClientClick="setRow(this)" OnClick="showPop" />

                 <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="imageBkCbtn" PopupControlID="popupPanelJournal"
                    BackgroundCssClass="modelBackground"  >
                 </asp:ModalPopupExtender>
                       
                         </td>
                      
                        <td style="height: 36px; font-weight:normal" >
                           <asp:Label ID="LabelNameJournal" runat="server" Text="Name Of Journal" ForeColor="Black"  ></asp:Label>
                        </td>
                        <td style="height: 36px" >
                             <asp:TextBox ID="TextBoxNameJournal" runat="server"  ReadOnly="true" Width="600px" Style="border-style:inset none none inset;"></asp:TextBox>
         </td>

               
                           </tr>
                       </table>

                       <table >
                           <tr>

                           
                                <td style="height: 56px; font-weight:normal" >
                             <asp:Label ID="LabelMonthJA" runat="server" Text="Publish Month" ForeColor="Black"   ></asp:Label>
                              <asp:DropDownList ID="DropDownListMonthJA" runat="server" Style="border-style:inset none none inset;"
        >
        <asp:ListItem Value="1">Jan</asp:ListItem>
         <asp:ListItem Value="2">Feb</asp:ListItem>
 <asp:ListItem Value="3">Mar</asp:ListItem>
 <asp:ListItem Value="4">Apr</asp:ListItem>
 <asp:ListItem Value="5">May</asp:ListItem>

 <asp:ListItem Value="6">June</asp:ListItem>

 <asp:ListItem Value="7">July</asp:ListItem>
 <asp:ListItem Value="8">Aug</asp:ListItem>
 <asp:ListItem Value="9">Sep</asp:ListItem>
  <asp:ListItem Value="10">Oct</asp:ListItem>
 <asp:ListItem Value="11">Nov</asp:ListItem>
  <asp:ListItem Value="12">Dec</asp:ListItem>

       
        </asp:DropDownList>   
        
                        </td>
                        <td style="height: 56px; font-weight:normal" >
                     <asp:Label ID="LabelYearJA" runat="server" Text="Publish Year" ForeColor="Black"  ></asp:Label>
                              <asp:DropDownList ID="TextBoxYearJA" runat="server" OnSelectedIndexChanged="txtboxYear_TextChanged" AutoPostBack="true"
        Width="80px"  Style="border-style:inset none none inset;" DataSourceID="SqlDataSourcepubyr" DataTextField="Year" DataValueField="Year">
        


        </asp:DropDownList>  
        <asp:SqlDataSource ID="SqlDataSourcepubyr" runat="server" ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" 
           SelectCommand="select Year from Publication_YearM ">
     
                </asp:SqlDataSource>
        
           <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" Display="None"  ControlToValidate="TextBoxYearJA" ValidationGroup="validation"
                ErrorMessage="Entered Proper Year" SetFocusOnError="true"  
                ValidationExpression="^[0-9]{4}$">
                </asp:RegularExpressionValidator> 
        
                        </td>
                <td style="height: 56px; font-weight:normal"> <asp:Label ID="LabelImpFact" runat="server" Text="Impact Factor" ForeColor="Black"  ></asp:Label></td> 
     <td  >
       <asp:TextBox ID="TextBoxImpFact" runat="server"  ReadOnly="true" Style="border-style:inset none none inset;" ></asp:TextBox>

     </td>

        <td style="height: 56px; font-weight:normal"> <asp:Label ID="LabelPubicationType" runat="server" Text="Publication Type" ForeColor="Black"  ></asp:Label></td> 
     <td  >
       <asp:DropDownList ID="DropDownListPubType" runat="server" Style="border-style:inset none none inset;" >
       <asp:ListItem Value="N">National</asp:ListItem>
        <asp:ListItem Value="I">International</asp:ListItem>
       </asp:DropDownList>

     </td>
  <td style="height: 36px; font-weight:normal">  
                  <asp:Label ID="LabelPageFrom" runat="server" Text="Page From" ForeColor="Black"  ></asp:Label>
                        </td>
                         
         <td>
      <asp:TextBox ID="TextBoxPageFrom" runat="server"  Width="50px" MaxLength="10" Style="border-style:inset none none inset;"></asp:TextBox> 
   </td>
     
                                <td style="height: 36px; font-weight:normal">
                    <asp:Label ID="LabelPageTo" runat="server" Text="Page To" ForeColor="Black"  ></asp:Label>
                        </td>
                        <td style="height: 36px" >
                         <asp:TextBox ID="TextBoxPageTo" runat="server"   Width="50px" MaxLength="10" Style="border-style:inset none none inset;"></asp:TextBox>
      
            </td>


                       </tr>
                       </table>
                       <table>

                           <tr>
  
    <td style="height: 36px; font-weight:normal">
                                <asp:Label ID="LabelVolume" runat="server" Text="Volume" ForeColor="Black"  ></asp:Label></td>
                        <td style="height: 36px">             
                       <asp:TextBox ID="TextBoxVolume" runat="server"   Width="140px" Style="border-style:inset none none inset;"></asp:TextBox>
     
                       
                         </td>
     <td style="height: 36px; font-weight:normal">  <asp:Label ID="Labelissue" runat="server" Text="Issue" ForeColor="Black"  ></asp:Label>
      </td>

    <td  style="font-weight:normal" >  <asp:TextBox ID="TextBoxIssue" runat="server"   Width="150px" Style="border-style:inset none none inset;"></asp:TextBox></td>

      <td style="height: 36px; font-weight:normal">
                          <asp:Label ID="LabelDOINum" runat="server" Text="DOI Number" ForeColor="Black" ></asp:Label>
                               
                               </td>
                        <td style="height: 36px">             
                             <asp:TextBox ID="TextBoxDOINum" runat="server"   Width="150px" MaxLength="20" Style="border-style:inset none none inset;"></asp:TextBox>
     
                 
                       
                         </td>
                           
         
     
                            
     <td style="height: 36px; font-weight:normal"> <asp:Label ID="LabelIndexed" runat="server" Text="Indexed:" ForeColor="Black"  ></asp:Label></td>
     <td style="font-weight:normal" >   <asp:RadioButtonList ID="RadioButtonListIndexed" runat="server"    RepeatDirection="Horizontal" OnSelectedIndexChanged="RadioButtonListIndexedOnSelectedIndexChanged" AutoPostBack="true" Style="border-style:inset none none inset;">
                                        <asp:ListItem Value="N" Selected="True">No</asp:ListItem>
                             <asp:ListItem Value="Y">Yes</asp:ListItem>
                  
 
                             </asp:RadioButtonList>  
     
     </td> 
     <td style="height: 36px; font-weight:normal">
    

     <asp:Label ID="LabelIndexedIn" runat="server" Text="Indexed In" ForeColor="Black"  ></asp:Label></td>
     <td  style="font-weight:normal" >
  <asp:CheckBoxList ID="CheckboxIndexAgency" runat="server" ForeColor="Black"  DataSourceID="SqlDataSourceCheckboxIndexAgency" RepeatDirection="Horizontal" DataTextField="agencyname" DataValueField="agencyid" Enabled="false">
                
                </asp:CheckBoxList>

                
<asp:SqlDataSource ID="SqlDataSourceCheckboxIndexAgency" runat="server" ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" 
           SelectCommand="select agencyid,agencyname from IndexAgency_M where active='Y'">
     
                </asp:SqlDataSource>

     </td>
               

                  </tr>
                  </table> 
   <asp:Panel ID="popupPanelJournal" runat="server" Visible="false" CssClass="modelPopup">

<table style="background:white">

<tr>
<th align="center" colspan="2"> Journal</th>
</tr>

<tr>
<th>Search Journal Name: </th>
<td><asp:TextBox ID="journalcodeSrch" runat="server" ></asp:TextBox> </td>
</tr> 
<tr>
<td colspan="2" align="center"> 
<asp:Button ID="btnPopSearch" runat="server" Text="Search" OnClick="JournalCodePopChanged"  />
</td>
</tr>
     
<tr>
<td colspan="2">
<asp:GridView ID="popGridJournal" runat="server"  AutoGenerateSelectButton="true"
OnSelectedIndexChanged="popSelected" AllowSorting="true" EmptyDataText="No Records Found"  >
<FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510"  />
        <HeaderStyle BackColor="#6386C6" Font-Bold="True" ForeColor="White" />
        <PagerStyle ForeColor="#000000" HorizontalAlign="Center" />
        <RowStyle BackColor="#FFFFFF" ForeColor="#000000" />
        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#FFF1D4" />
        <SortedAscendingHeaderStyle BackColor="#B95C30" />
        <SortedDescendingCellStyle BackColor="#F1E5CE" />
        <SortedDescendingHeaderStyle BackColor="#93451F" />
</asp:GridView>


<asp:SqlDataSource ID="SqlDataSourceJournal" runat="server" ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" 
           SelectCommand="SELECT top 10 Id,Title,AbbreviatedTitle FROM [Journal_M]">
     
                </asp:SqlDataSource>

</td>
</tr>
</table>
<asp:Button ID="Buttonexit" runat="server" Text="EXIT" OnClick="exit1" />
</asp:Panel>


</asp:Panel>


  <asp:Panel ID="panelConfPaper" runat="server" GroupingText="Conference Paper" Visible="false" Font-Bold="true"> 

 <table  style="width: 92%">
                    <tr>
                        <td style="height: 36px; font-weight:normal">
                          <asp:Label ID="LabelEventTitle" runat="server" Text="Event Title:" ></asp:Label>
                               
                               </td>
                        <td style="height: 36px" colspan="3">             
                             <asp:TextBox ID="TextBoxEventTitle" runat="server"   Width="600px" MaxLength="200" Style="border-style:inset none none inset;"></asp:TextBox>
     
                 
                       
                         </td>
                      
   
                 <td style="height: 36px; font-weight:normal">
                                <asp:Label ID="LabelDate" runat="server" Text="Date:" ></asp:Label></td>
                        <td style="height: 36px">             
                       <asp:TextBox ID="TextBoxDate" runat="server"   Width="200px" Style="border-style:inset none none inset;"></asp:TextBox>
     
                         </td>
                           </tr>
                       
                <tr>
                                 <td style="height: 36px; font-weight:normal">
                           <asp:Label ID="LabelISBN" runat="server" Text="ISBN:" ></asp:Label>
                        </td>
                        <td style="height: 36px" colspan="2">
                             <asp:TextBox ID="TextBoxIsbn" runat="server"  Width="400px" MaxLength="50" Style="border-style:inset none none inset;"></asp:TextBox>
         </td>
                
                                     <td style="height: 36px; font-weight:normal">
                           <asp:Label ID="LabelPlace" runat="server" Text="Place:" ></asp:Label>
                        </td>
                        <td style="height: 36px" colspan="2">
                             <asp:TextBox ID="TextBoxPlace" runat="server"  Width="300px" MaxLength="50" Style="border-style:inset none none inset;"></asp:TextBox>
         </td>

                  

                </tr>       
     
        <tr>
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
                      
                        <td style="height: 36px; font-weight:normal">
                           <asp:Label ID="Label3" runat="server" Text="Credit Point:" ></asp:Label>
                        </td>
                        <td style="height: 36px">
                             <asp:TextBox ID="TextBoxCreditPoint" runat="server"  Text="0" Style="border-style:inset none none inset;" OnTextChanged="TextBoxCreditPointOnTextChanged" AutoPostBack="true"></asp:TextBox>
         </td>

                 <td style="height: 36px; font-weight:normal">
                                <asp:Label ID="Label4" runat="server" Text="Awarded By:" ></asp:Label></td>
                        <td style="height: 36px">             
                       <asp:TextBox ID="TextBoxAwardedBy" runat="server" Enabled="false"   Width="200px" Style="border-style:inset none none inset;"></asp:TextBox>
     
                         </td>
                           </tr>
                       


               
                     </table>  
  

</asp:Panel>



  <asp:Panel ID="panelBookPublish" runat="server" GroupingText="Book Publish" Visible="false" Font-Bold="true"> 


 <table border="1" style="width: 100%">
                    <tr>
                        <td style="height: 36px">
                          <asp:Label ID="LabelTitileBook" runat="server" Text="Title Of The Book:" Font-Bold="true"></asp:Label>
                               
                               </td>
                        <td style="height: 36px">             
                             <asp:TextBox ID="TextBoxTitleBook" runat="server"  Width="200px"></asp:TextBox>
     
                 
                       
                         </td>
                      
                        <td style="height: 36px">
                           <asp:Label ID="LabelTitleChapterContributed" runat="server" Text="Title Of the Chapter Contributed:" Font-Bold="true"></asp:Label>
                        </td>
                        <td style="height: 36px">
                             <asp:TextBox ID="TextBoxChapterContributed" runat="server"   Width="200px"></asp:TextBox>
         </td>

                 <td style="height: 36px">
                                <asp:Label ID="LabelEdition" runat="server" Text="Edition:" Font-Bold="true" ></asp:Label></td>
                        <td style="height: 36px">             
                       <asp:TextBox ID="TextBoxEdition" runat="server"   Width="200px"></asp:TextBox>
     
                       
                         </td>
                           </tr>
                       
                           <tr>

                           
                                <td style="height: 36px">
                             <asp:Label ID="LabelPublisher" runat="server" Text="Publisher:"  Font-Bold="true"></asp:Label>
                        </td>
                        <td style="height: 36px">
                     <asp:TextBox ID="TextBoxPublisher" runat="server" 
        Width="200px" ></asp:TextBox>   
        
                        </td>
             
     
      <td style="height: 36px">  
                  <asp:Label ID="LabelYear" runat="server" Text="Year:" Font-Bold="true"></asp:Label>
                        </td>
                         
         <td>
      <asp:TextBox ID="TextBoxYear" runat="server"  Width="200px"></asp:TextBox> 
   </td>
     
                                <td style="height: 36px">
                    <asp:Label ID="Labelpagenum" runat="server" Text="Page Number:" Font-Bold="true"></asp:Label>
                        </td>
                        <td style="height: 36px">
                         <asp:TextBox ID="TextBoxPageNum" runat="server"   Width="200px" ></asp:TextBox>
      
            </td>
                       </tr>
      <tr>
     <td> <asp:Label ID="LabelVolume1" runat="server" Text="Volume:" Font-Bold="true" ></asp:Label></td> 
     <td colspan="4" >
  <asp:TextBox ID="TextBoxVolume1" runat="server"   Width="200px" ></asp:TextBox>
       

     </td>

                  </tr>

               
               
                     </table>  
  

</asp:Panel>


 <asp:Panel ID="panelOthes" runat="server" GroupingText="NewsPaper/Magazine" Visible="false" Font-Bold="true"> 

 
 <table border="1" style="width: 100%">
                    <tr>
                        <td style="height: 36px">
                          <asp:Label ID="LabelPublisherNewsPaper" runat="server" Text="Publisher:" Font-Bold="true"></asp:Label>
                               
                               </td>
                        <td style="height: 36px">             
                             <asp:TextBox ID="TextBoxPublish" runat="server"   Width="200px"></asp:TextBox>
     
                 
                       
                         </td>
                      
                        <td style="height: 36px">
                           <asp:Label ID="LabelDateOfPublish" runat="server" Text="Date Of Publish:" Font-Bold="true"></asp:Label>
                        </td>
                        <td style="height: 36px">
                             <asp:TextBox ID="TextBoxDateOfPublish" runat="server"  Width="200px"></asp:TextBox>
         </td>

                 <td style="height: 36px">
                                <asp:Label ID="LabelPageNumNewsPaper" runat="server" Text="PageNum:" Font-Bold="true" ></asp:Label></td>
                        <td style="height: 36px">             
                       <asp:TextBox ID="TextBoxPageNumNewsPaper" runat="server"   Width="200px"></asp:TextBox>
     
                       
                         </td>
                           </tr>
                       
                       
     
    


               
                     </table>  

  

</asp:Panel>
<br />
<br />


  <asp:Panel ID="panelTechReport" runat="server"  GroupingText="Generic Details" Visible="false" Font-Bold="true"> 


 <table border="1" style="width: 100%">
                    <tr>
                        <td style="height: 36px">
                             <asp:Label ID="LabelURL" runat="server" Text="Official URL:"  Font-Bold="true"></asp:Label>
                        </td>
                        <td style="height: 36px" colspan="3">
                     <asp:TextBox ID="TextBoxURL" runat="server"
        Width="900px" ></asp:TextBox>   
        
                        </td>
               
                      
                     

               
                           </tr>
                       <tr>
                         <td style="height: 36px">
                                <asp:Label ID="LabelAbstract" runat="server" Text="Abstract:" Font-Bold="true" ></asp:Label></td>
                        <td style="height: 36px" colspan="3" >             
                       <asp:TextBox ID="TextBoxAbstract" runat="server"   Width="800px" TextMode="MultiLine"></asp:TextBox>
     
                       
                         </td>

                      
                       </tr>
                           <tr>

                                    <td style="height: 36px">
                           <asp:Label ID="LabelkeyWords" runat="server" Text="Keywords:" Font-Bold="true"></asp:Label>
                        </td>
                        <td style="height: 36px">
                             <asp:TextBox ID="TextBoxKeywords" runat="server"   Width="400px"></asp:TextBox>
         </td>

                       <td style="height: 36px">
                    <asp:Label ID="LabelisErf" runat="server" Text="ERF Related?" Font-Bold="true"></asp:Label>
                        </td>
                        <td style="height: 36px">
                         <asp:DropDownList ID="DropDownListErf" runat="server" >
                          <asp:ListItem Value="N">No</asp:ListItem>
                         <asp:ListItem Value="Y">Yes</asp:ListItem>
                          
                         </asp:DropDownList>

                         ( Environmental Research Fund )
      
            </td>
                   
             </tr>
                             <tr align="center">
     
     <td align="center" colspan="6">
     <asp:Label ID="Eprint" runat="server" Text="EPrint" Font-Bold="true"></asp:Label>
     
     </td>
     
     </tr>
             <tr>
     
      <td style="height: 36px">  
                  <asp:Label ID="LabelUploadPfd" runat="server" Text="View PDF:" Font-Bold="true" ></asp:Label>
                        </td>
                         
         <td>

                <asp:FileUpload ID="FileUploadPdf" runat="server" BorderColor="#996600" BorderStyle="Inset" Visible="false" />

                  <asp:GridView ID="GVViewFile" runat="server" AutoGenerateColumns="False" 
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
        
      
        </asp:GridView>


        <asp:SqlDataSource ID="DSforgridview" runat="server" 
            ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" 
            SelectCommand="  ">
           
        </asp:SqlDataSource>



   </td>
     
                   <td style="height: 36px">
                             <asp:Label ID="LabeluploadEPrint" runat="server" Text="upload To EPrint:"  Font-Bold="true" ></asp:Label>
                        </td>
                        <td style="height: 36px">
                     <asp:DropDownList ID="DropDownListuploadEPrint" runat="server" Enabled="false"
        Width="200px" >
           <asp:ListItem Value="N">No</asp:ListItem>
           <asp:ListItem Value="Y">Yes</asp:ListItem>
                        
        
        </asp:DropDownList>   
        
                        </td>
                       </tr>
       
                       <tr>
                    
                         <td style="height: 36px" >
                             <asp:Label ID="Label1" runat="server" Text="EprintURL:"  Font-Bold="true"></asp:Label>
                        </td>
                        <td style="height: 36px" colspan="3">
                     <asp:TextBox ID="TextBoxEprintURL" runat="server" ReadOnly="true"
        Width="900px" ></asp:TextBox>   
        
                        </td>
                         
                       
                       </tr>
     
     
               <tr>
               
                         <td style="height: 36px" >
                             <asp:Label ID="Label2" runat="server" Text="Remarks:"  Font-Bold="true"></asp:Label>
                        </td>
                        <td style="height: 36px" colspan="3">
                     <asp:TextBox ID="TextBoxRemarks" runat="server" TextMode="MultiLine"
        Width="900px" ></asp:TextBox>   
        
                        </td>
                         
               </tr>
               
               
                     </table>  
  

</asp:Panel>






<asp:Panel ID="popupPanelAffil" runat="server" Visible="false" CssClass="modelPopup">

<table style="background:white">

<tr>
<th align="center" colspan="2"> Employee </th>
</tr>

<tr>
<th>Search By Name: </th>
<td><asp:TextBox ID="affiliateSrch" runat="server"   ></asp:TextBox> </td>
</tr> 
       <tr>
  <td colspan="2" align="center">
  <asp:Button ID="BtnEmployeeSearch" runat="server" Text="Search" OnClick="branchNameChanged" />
  </td> 
  
  </tr>
<tr>
<td colspan="2">
<asp:GridView ID="popGridAffil" runat="server"  AutoGenerateSelectButton="true" EmptyDataText="No User Found"
OnSelectedIndexChanged="popSelected1" AllowSorting="true"  >
<FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510"  />
        <HeaderStyle BackColor="#6386C6" Font-Bold="True" ForeColor="White" />
        <PagerStyle ForeColor="#000000" HorizontalAlign="Center" />
        <RowStyle BackColor="#FFFFFF" ForeColor="#000000" />
        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#FFF1D4" />
        <SortedAscendingHeaderStyle BackColor="#B95C30" />
        <SortedDescendingCellStyle BackColor="#F1E5CE" />
        <SortedDescendingHeaderStyle BackColor="#93451F" />
</asp:GridView>



<asp:SqlDataSource ID="SqlDataSourceAffil" runat="server" ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" 
SelectCommand="SELECT top 10  User_Id,Name from User_M" ProviderName="System.Data.SqlClient">
</asp:SqlDataSource>

</td>
</tr>
</table>
<asp:Button ID="Buttonexit4" runat="server" Text="EXIT" OnClick="exit" />
</asp:Panel>




<br />

 <br />
 <br />
<table width="100%">
    <tr>
        <td align="center">
          
            <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="BtnSave_Click" Enabled="false" CausesValidation="true" ValidationGroup="validation"></asp:Button>
          

    
 </td>
 </tr>
</table>
<br />
</asp:Panel>


</center>


</asp:Panel>

<br />



<asp:HiddenField ID="rowVal" runat="server" />
    <asp:HiddenField ID="senderID" runat="server" />
</asp:Content>--%>

