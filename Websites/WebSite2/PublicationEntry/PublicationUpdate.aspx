<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPageRMS.master" AutoEventWireup="true" CodeFile="PublicationUpdate.aspx.cs" Inherits="PublicationEntry_PublicationUpdate" MaintainScrollPositionOnPostback="true" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script src="../JScript.js" type="text/javascript"></script>
     <script src="../Scripts/jquery-1.7.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-1.7.1.min.js" type="text/javascript"></script>
    <asp:HiddenField runat="server" ID="hfPosition" Value="" />

     <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" DisplayMode="BulletList" 
     HeaderText="Validation issues" ShowSummary="False" ValidationGroup="validation" />
      <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="True" DisplayMode="BulletList" 
     HeaderText="Validation issues" ShowSummary="False" ValidationGroup="validation1" />

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
	background-color:#EEEEE;
	border-width:3px;
	border-style:solid;
	border-color:Gray;
	font-family:Verdana;
	font-size:small;
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

      .auto-style8 {
          width: 152px;
      }

      .auto-style9 {
          height: 36px;
          width: 131px;
      }
      .auto-style10 {
          height: 36px;
          width: 136px;
      }
      .auto-style11 {
          height: 36px;
          width: 109px;
      }
      .auto-style13 {
          height: 39px;
          width: 129px;
      }

      .auto-style14 {
          height: 36px;
          width: 89px;
      }
      .auto-style15 {
          height: 39px;
      }

      .auto-style19 {
          height: 36px;
          width: 217px;
      }
      .auto-style20 {
          width: 217px;
      }
      .auto-style21 {
          height: 36px;
          width: 122px;
      }
      .auto-style22 {
          height: 36px;
          width: 104px;
      }

      .auto-style26 {
          width: 51px;
      }

      .auto-style27 {
          height: 36px;
          width: 307px;
      }

      .auto-style31 {
          height: 56px;
          width: 91px;
      }

      .auto-style32 {
          height: 56px;
          width: 96px;
      }

      .auto-style34 {
          height: 56px;
          width: 70px;
      }
      .auto-style35 {
          height: 56px;
          width: 72px;
      }
      .auto-style38 {
          width: 38px;
      }
      .auto-style40 {
          width: 46px;
      }
      .auto-style42 {
          height: 56px;
          width: 97px;
      }
      .auto-style44 {
          height: 56px;
          width: 101px;
      }
      .auto-style46 {
          width: 39px;
      }
      .auto-style47 {
          height: 56px;
          width: 90px;
      }
      .auto-style48 {
          height: 56px;
          width: 77px;
      }
      .auto-style49 {
          width: 29px;
      }

      .auto-style50 {
          width: 42px;
      }

  </style>

     

<script type="text/javascript">
    $(function () {
        var f = $("#<%=hfPosition.ClientID%>");
        window.onload = function () {
            var position = parseInt(f.val());
            if (!isNaN(position)) {
                $(window).scrollTop(position);
            }
        };
        window.onscroll = function () {
            var position = $(window).scrollTop();
            f.val(position);
        };
    });
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
  
<script type = "text/javascript">
    function ConfirmButtonStudentPub() {
        var validated = Page_ClientValidate('validation1');
        if (validated) {
            var studentpub = document.getElementById('<%= StudentPub.ClientID %>').value;
            var pagefrom = document.getElementById('<%= TextBoxPageFrom.ClientID %>').value;
            var TextBoxPageTo = document.getElementById('<%= TextBoxPageTo.ClientID %>').value;
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            confirm_value.value = "Yes";
            var PublicationEntry = document.getElementById('<%= DropDownListPublicationEntry.ClientID %>').value;

            if (PublicationEntry == "JA" || PublicationEntry == "TS") {
                if (studentpub == "Y") {
                    if (pagefrom != "") {
                        if (confirm("This will be considered as a Student Publication.Do you want to Continue?"))
                            confirm_value.value = "Yes";
                        else
                            confirm_value.value = "No";
                    }
                    else {
                        if (confirm("Page Number is not entered.This paper will considered as online student Publication..Do you want to Continue?"))
                            confirm_value.value = "Yes";
                        else
                            confirm_value.value = "No";
                    }

                }
                else if (studentpub == "N") {
                    if (pagefrom != "") {
                        if (confirm("This will be considered as a Faculty Publication.Do you want to Continue?"))
                            confirm_value.value = "Yes";
                        else
                            confirm_value.value = "No";
                    }
                    else {
                        if (confirm("Page Number is not entered.This paper will be considered as online Faculty Publication.Do you want to Continue?"))
                            confirm_value.value = "Yes";
                        else
                            confirm_value.value = "No";
                    }

                }
                document.forms[0].appendChild(confirm_value);
            }
        }
    }   
    </script>
 <asp:ToolkitScriptManager ID="Scriptmanager1" runat="server"/>
         <center> <asp:Label ID="lablPanelTitle" runat="server" Text="Publication Update" Font-Bold="true"  ></asp:Label></center>
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
     SelectCommand="select TypeEntryId,EntryName from PublicationTypeEntry_M where (TypeEntryId='JA' or TypeEntryId='Ts')" ProviderName="System.Data.SqlClient">
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
        PagerSettings-PageButtonCount="5" PageSize="5" DataSourceID="SqlDataSource1"  
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

<asp:Panel ID="panel" runat="server"   Font-Bold="true"  Style="font-weight:bold; border-style:inset;background-color:#E0EBAD" > 
<br />
<center><asp:Label runat="server" ID="Incentivenote" Visible="false" Text="Note: Incentive Point for the following publication is already processed. You cannot Update the data."></asp:Label></center>
    <br />
<asp:Panel ID="panel1" runat="server" GroupingText=""  > 
<table style="width: 92%">
<tr><td style="height: 36px; font-weight:normal">
<asp:Label ID="TypeEntry" runat="server" Text="Entry Type" ForeColor="Black"></asp:Label></td>
<td style="height: 36px" >
<asp:DropDownList ID="DropDownListPublicationEntry" runat="server" Enabled="false"
                                    AppendDataBoundItems="true" AutoPostBack="true" 
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
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                    ControlToValidate="DropDownListPublicationEntry" Display="None" 
                                    ErrorMessage="Please select Type Of Entry" InitialValue=" " 
                                    ValidationGroup="validation"></asp:RequiredFieldValidator>
                                 </td>
                        <td style="height: 36px; font-weight: normal;">             
                            <asp:Label ID="lblMUCat" runat="server" Text="MAHE Categorization" ForeColor="Black"></asp:Label>
                       
                         </td>
                                 <td style="height: 36px">
                                     <asp:DropDownList ID="DropDownListMuCategory" runat="server"  AutoPostBack="true"
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
                                         <asp:TextBox ID="txtboxTitleOfWrkItem" runat="server" MaxLength="200" Enabled="false"
                                             Style="border-style:inset none none inset;" TextMode="MultiLine" Width="850px"></asp:TextBox>
                                                </td>
                                 </tr>
               
                        
                       
                        
     
                     </table>  

                     </asp:Panel>
                     <br />

                             <asp:Panel ID="panAddAuthor" runat="server" GroupingText="Author Details" Visible="false">
                         <table width="100%">
                  
                           <tr>
                           <asp:Label ID="line" runat="server">-----------------------------------------------------------------------------------------------------------------------------------</asp:Label>
                           </tr>
                   <tr><td><asp:Label ID="lblnote1" runat="server" Text="Note 1: Please enter authors  in the order of appearance as in the publication" Visible="false"></asp:Label></td>
                  </tr>
                  <tr>
                   <td><asp:Label ID="lblnote2" runat="server"  Visible="false"></asp:Label></td>
                   </tr>
                   <tr><td>&nbsp;</td>
                   </tr>
                           <tr>
                           <td><strong> Author Entry:</strong>
                           <asp:Button ID="BtnAddMU" runat="server" Text="Add New author" onclick="addRow" CausesValidation="true" ValidationGroup="validation"/> 
                            
                           </td>
                           </tr>
                           </table>

   <br />

<asp:SqlDataSource ID="SqlDataSourceAuthorType" runat="server" 
                ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" 
                ProviderName="System.Data.SqlClient"> 
                                     
            </asp:SqlDataSource>


<asp:Panel ID="PanelMU" runat="server" ScrollBars="Both" BorderStyle="Double"   Width="1000px" style="margin-right: 0px">

     <asp:GridView ID="Grid_AuthorEntry" runat="server" AutoGenerateColumns="False"  GridLines="None"
     OnRowDeleting="Grid_AuthorEntry_RowDeleting"  OnRowDataBound="OnRowDataBound" CellPadding="4" ForeColor="#333333">

     <AlternatingRowStyle BackColor="White" />

     <Columns>

     
        <asp:TemplateField HeaderText="MAHE/Non MAHE">
             <ItemTemplate>
                 <asp:DropDownList ID="DropdownMuNonMu" runat="server" Width="160" OnSelectedIndexChanged="DropdownMuNonMuOnSelectedIndexChanged" AutoPostBack="true" DataSourceID="SqlDataSourceAuthorType" >
               
                 </asp:DropDownList>
                 
                 <asp:ImageButton ID="MuNonMu" runat="server" ImageUrl="~/Images/srchImg.gif" CssClass="blnkImgCSS"  />

             </ItemTemplate>              
        </asp:TemplateField>

        <asp:TemplateField ShowHeader="true" HeaderText="Roll No/Employee Code">
             <ItemTemplate>
       <asp:TextBox ID="EmployeeCode" runat="server" Width="180" Visible="true" MaxLength="9"></asp:TextBox>
        <asp:Image ID="ImageEmpCode" runat="server" ImageUrl="~/Images/srchImg.gif" CssClass="blnkImgCSS" />       
            </ItemTemplate>            
        </asp:TemplateField>


        <asp:TemplateField HeaderText="Author Name">
             <ItemTemplate>
                 <asp:TextBox ID="AuthorName" runat="server" Width="200" Enabled="false" OnTextChanged="AuthorName_Changed" AutoPostBack="true"></asp:TextBox>
          
                 <asp:ImageButton ID="EmployeeCodeBtn" runat="server" ImageUrl="~/Images/srchImg.gif" OnClientClick="setRow(this)" />

                 <asp:ModalPopupExtender ID="ModalPopupExtender4" runat="server" TargetControlID="EmployeeCodeBtn" PopupControlID="popup"
                    BackgroundCssClass="modelBackground"  >
                                  </asp:ModalPopupExtender>
                                  <asp:ImageButton ID="ImageButton1" runat="server" Visible="false" ImageUrl="~/Images/srchImg.gif" OnClientClick="setRow(this)" />

                 <asp:ModalPopupExtender ID="ModalPopupExtender2" runat="server" TargetControlID="ImageButton1" PopupControlID="popup"
                    BackgroundCssClass="modelBackground"  >
                                  </asp:ModalPopupExtender>
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
             
             <asp:Image ID="Image4" runat="server" ImageUrl="~/Images/srchImg.gif" CssClass="blnkImgCSS" />           
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

                 <asp:Image ID="Image5" runat="server" ImageUrl="~/Images/srchImg.gif" CssClass="blnkImgCSS" />
            </ItemTemplate>            
        </asp:TemplateField>

           <asp:TemplateField HeaderText="MailId">
             <ItemTemplate>
                 <asp:TextBox ID="MailId" runat="server" Width="250"  Enabled="false" ></asp:TextBox>
                 
                 <asp:Image ID="ImageMailId" runat="server" ImageUrl="~/Images/srchImg.gif" CssClass="blnkImgCSS" />
     
      <asp:RegularExpressionValidator
        id="regEmail"
        ControlToValidate="MailId" Display="Static" ErrorMessage="Invalid Email Id"
        Text="(Invalid email)" ValidationGroup="validation"
        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
        Runat="server" />  
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
                 <asp:ListItem Value="P">First Author</asp:ListItem>
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
         <asp:TemplateField ShowHeader="false">
             <ItemTemplate>
                 <asp:HiddenField ID="PublicationLine" runat="server" ></asp:HiddenField>
            </ItemTemplate>            
        </asp:TemplateField>
         <asp:CommandField DeleteText="Remove" ShowDeleteButton="True"   />


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
<asp:Panel ID="panelTechReport" runat="server" Visible="false" GroupingText="Generic Details" ForeColor="Black"  > 


<%--   <center>
Generic Details
 </center>--%>
  <center>
 <table style="width: 92%">
                     <%--<tr>
                       <td style="height: 36px; font-weight:normal">
                             <asp:Label ID="LabelURL" runat="server" Text="Official URL" ForeColor="Black"  ></asp:Label>
                             <%--<asp:Label ID="LabelURL1" runat="server" Text="*"  ForeColor="Red"></asp:Label> 
                        </td>
                        <td style="height: 36px" colspan="3">
                     <asp:TextBox ID="TextBoxURL" runat="server" MaxLength="200" Style="border-style:inset none none inset;" Enabled="false" 
        Width="900px" ></asp:TextBox>   
        
                        </td>
                     
                     

               
                           </tr> --%>
                       <tr>
                         <td style="height: 36px; font-weight:normal">
                                <asp:Label ID="LabelAbstract" runat="server" Text="Abstract" ForeColor="Black" ></asp:Label>
                             <%--   <asp:Label ID="Label13" runat="server" Text="*"  ForeColor="Red"></asp:Label>  --%>
                                </td>
                        <td style="height: 36px" colspan="3" >             
                       <asp:TextBox ID="TextBoxAbstract" runat="server"   Width="900px" Enabled="false"  TextMode="MultiLine" MaxLength="250" Style="border-style:inset none none inset;"></asp:TextBox>
     
                       
                         </td>

                        
                       </tr>
                           <tr>

                                    <td style="height: 36px; font-weight:normal">
                           <asp:Label ID="LabelkeyWords" runat="server" Text="Keywords" ForeColor="Black" ></asp:Label>
                        <%--   <asp:Label ID="Label14" runat="server" Text="*"  ForeColor="Red"></asp:Label>  --%>
                        </td>
                        <td style="height: 36px">
                             <asp:TextBox ID="TextBoxKeywords" runat="server"   Width="400px" MaxLength="200" Enabled="false"  Style="border-style:inset none none inset;"></asp:TextBox>
         </td>
                     <td style="height: 36px; font-weight:normal">
                    <asp:Label ID="LabelisErf" runat="server" Text="ERF Related?" ForeColor="Black"  ></asp:Label>
                        </td>
                        <td style="height: 36px">
                         <asp:DropDownList ID="DropDownListErf" runat="server" Style="border-style:inset none none inset;" Enabled="false" >
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
                          
             
       <tr><td>&nbsp;</td></tr>
          <tr align="center">
     
     <td colspan="6" style="font-weight:normal">
     <asp:Label ID="Eprint" runat="server" Text="EPrint" Font-Bold="true" ForeColor="Black" ></asp:Label>
     
     </td>
     
     </tr>
                       <tr>
                     
                   <td style="height: 36px; font-weight:normal">
                             <asp:Label ID="LabeluploadEPrint" runat="server" Width="100px" Text="Upload To EPrint" ForeColor="Black"  ></asp:Label>
                        </td>
                        <td style="height: 36px">
                     <asp:DropDownList ID="DropDownListuploadEPrint" runat="server" Enabled="false" Style="border-style:inset none none inset;"
        Width="200px" >
           <asp:ListItem Value="N">No</asp:ListItem>
           <asp:ListItem Value="Y">Yes</asp:ListItem>
                        
        
        </asp:DropDownList>   
        
                        </td>
                        </tr>
                        <tr>
                         <td style="height: 36px; font-weight:normal" >
                             <asp:Label ID="Label1" runat="server" Text="EprintURL"  ForeColor="Black" ></asp:Label>
                        </td>
                        <td style="height: 36px" colspan="3">
                     <asp:TextBox ID="TextBoxEprintURL" runat="server" ReadOnly="true" Style="border-style:inset none none inset;" Enabled="false" 
        Width="900px" ></asp:TextBox>   
        
                        </td>
                         
                       
                       </tr>
     

               
               
                     </table>  
  </center>
  <table>
<tr>
     
      <td style="height: 36px; font-weight:normal">  
                  <asp:Label ID="LabelUploadPfd" runat="server" Text="View Uploaded File" ForeColor="Black" > </asp:Label>
                        </td>
                         
         <td>
         
                  <asp:GridView ID="GVViewFile" runat="server" AutoGenerateColumns="False" 
            DataSourceID="DSforgridview" onselectedindexchanged="GVViewFile_SelectedIndexChanged"
            HeaderStyle-ForeColor="White" ShowHeader="False"
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



   </td>

    
                       </tr>

</table>
</asp:Panel><br />



  <asp:Panel ID="panelJournalArticle" runat="server" GroupingText="Journal Publish Details" Visible="false"  ForeColor="Black"  > 
<%--  <center>
  Journal Article
  </center>--%>
  <br />

  <table width="100%">
  
  <tr>
                              
     <td style="height: 36px; font-weight:normal"> <asp:Label ID="LabelIndexed" runat="server" Text="Indexed:" ForeColor="Black"  ></asp:Label></td>
     <td style="font-weight:normal" >   <asp:RadioButtonList ID="RadioButtonListIndexed" runat="server"  Enabled="false"  RepeatDirection="Horizontal"  AutoPostBack="true" Style="border-style:inset none none inset;">
                                        <asp:ListItem Value="N" >No</asp:ListItem>
                             <asp:ListItem Value="Y" Selected="True">Yes</asp:ListItem>
                  
 
                             </asp:RadioButtonList>  
     
     </td> 
     <td>&nbsp;</td>
 
  
   <td style="height: 36px; font-weight:normal">
    

     <asp:Label ID="LabelIndexedIn" runat="server" Text="Indexed In:" ForeColor="Black"  ></asp:Label></td>
     <td  style="font-weight:normal" >
  <asp:CheckBoxList ID="CheckboxIndexAgency" runat="server" ForeColor="Black"   DataSourceID="SqlDataSourceCheckboxIndexAgency" RepeatDirection="Horizontal" DataTextField="agencyname" DataValueField="agencyid" Enabled="false">
                
                </asp:CheckBoxList>

                
<asp:SqlDataSource ID="SqlDataSourceCheckboxIndexAgency" runat="server" ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" 
           SelectCommand="select agencyid,agencyname from IndexAgency_M where active='Y'">
     
                </asp:SqlDataSource>

     </td>
     <td>&nbsp;</td>      

 


                        <td style="height: 36px; font-weight:normal" >
                          <asp:Label ID="LabelPubJournal" runat="server" Text="ISSN :" ForeColor="Black" ></asp:Label>
                                     <asp:Label ID="Labeljastar1" runat="server" Text="*" ForeColor="Red" Visible="false"></asp:Label>
                               </td>
                        <td style="height: 36px" >             
                             <asp:TextBox ID="TextBoxPubJournal" runat="server"  Width="150px"  Enabled="false"
 AutoPostBack="true" Style="border-style:inset none none inset;"></asp:TextBox>
                            
                         </td>
                      
                        <td style="height: 36px; font-weight:normal" >
                           <asp:Label ID="LabelNameJournal" runat="server" Text="Name Of Journal :" ForeColor="Black"  ></asp:Label>
                        </td>
                        <td style="height: 36px" >
                             <asp:TextBox ID="TextBoxNameJournal" runat="server"  Enabled="false" Width="250px" Style="border-style:inset none none inset;"></asp:TextBox>
         </td>

               
                           </tr>
                       </table>
                          <table width="100%">
   
    <tr>
      <td style="height: 36px; font-weight:normal; width: 113px;">
                          <asp:Label ID="LabelDOINum" runat="server" Text="DOI Number" ForeColor="Black" ></asp:Label>
                               
                               </td>
                        <td style="height: 36px; width: 248px;">             
                             <asp:TextBox ID="TextBoxDOINum" runat="server" Enabled="false"  Width="223px" MaxLength="200" Style="border-style:inset none none inset;"></asp:TextBox>
     
                 
                       
                         </td>
                           
         
                                

        
        <td style="height: 56px; font-weight:normal; width: 134px;"> <asp:Label ID="LabelPubicationType" runat="server" Text="Publication Type" ForeColor="Black"  ></asp:Label></td> 
     <td  >
       <asp:DropDownList ID="DropDownListPubType" runat="server" Style="border-style:inset none none inset;" Enabled="false">
       <asp:ListItem Value="N">National</asp:ListItem>
        <asp:ListItem Value="I">International</asp:ListItem>
       </asp:DropDownList>

     </td>
          
     

                           
                                <td style="height: 56px; font-weight:normal" >
                             <asp:Label ID="LabelMonthJA" runat="server" Text="Publish Month" ForeColor="Black"   ></asp:Label>
                                  <asp:Label ID="Labeljastr2" runat="server" Text="*" ForeColor="Red" Visible="false"></asp:Label></td>
                                  <td>
                              <asp:DropDownList ID="DropDownListMonthJA" runat="server" Style="border-style:inset none none inset;" DataSourceID="SqlDataSourcePubJAmonth"
      DataTextField="MonthName" DataValueField="MonthValue"  Width="50px"  OnSelectedIndexChanged="txtboxYear_TextChanged" AutoPostBack="true">
      

       
        </asp:DropDownList>   
          <asp:SqlDataSource ID="SqlDataSourcePubJAmonth" runat="server" ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" 
           SelectCommand="select MonthValue, MonthName from Publication_MonthM">
     
                </asp:SqlDataSource>
                        </td>
                        <td style="height: 56px; font-weight:normal" >
                     <asp:Label ID="LabelYearJA" runat="server" Text="Publish Year" ForeColor="Black"  ></asp:Label>
                          <asp:Label ID="Labeljastr3" runat="server" Text="*" ForeColor="Red" Visible="false" ></asp:Label></td><td>
                              <asp:DropDownList ID="TextBoxYearJA" runat="server" 
        Width="60px"  Style="border-style:inset none none inset;"  OnSelectedIndexChanged="txtboxYear_TextChanged" AutoPostBack="true">
        


        </asp:DropDownList>  

        
           <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" Display="None"  ControlToValidate="TextBoxYearJA" ValidationGroup="validation"
                ErrorMessage="Entered Proper Year" SetFocusOnError="true"  
                ValidationExpression="^[0-9]{4}$">
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                </asp:RegularExpressionValidator> 
        
                        </td>
                              </tr>
                              </table>
              <table>
                  <tr>
               <td style="height: 56px; font-weight:normal"> <asp:Label ID="LabelImpFact" runat="server" Text="1-Year Impact Factor" ForeColor="Black"  ></asp:Label></td> 
     <td style="width: 142px"  >
       <asp:TextBox ID="TextBoxImpFact" runat="server"  ReadOnly="true" Style="border-style:inset none none inset;" Width="100px" ></asp:TextBox>

     </td>

 

<td style="height: 56px; font-weight:normal; width: 203px;"> <asp:Label ID="LabelImpFact5" runat="server" Text="5-Year Impact Factor" ForeColor="Black"  ></asp:Label></td> 
     <td  >
       <asp:TextBox ID="TextBoxImpFact5" runat="server"  ReadOnly="true" Style="border-style:inset none none inset;" Width="100px" ></asp:TextBox>

     </td>
        <td style="height: 56px; font-weight:normal; width: 173px;"> <asp:Label ID="LblIFAY" runat="server" Text="IF-ApplicableYear" ForeColor="Black"  ></asp:Label></td> 
     <td  >
      <asp:TextBox ID="txtIFApplicableYear" runat="server"  ReadOnly="true" Style="border-style:inset none none inset;" Width="100px" ></asp:TextBox>
     </td>
     <td style="font-weight:normal" class="auto-style35">
                             <asp:Label ID="lblQuartile" runat="server" Text="Quartile" ForeColor="Black" ></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtquartile" runat="server" ReadOnly="true" Width="100px" Enabled="false" Style="border-style:inset none none inset;" ></asp:TextBox>
                        </td>
                        <td  style="font-weight:normal" class="auto-style35">
                             <asp:Label ID="lblQuartileid" runat="server" Text="QuartileID" ForeColor="Black" Visible="false"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtquartileid" runat="server" ReadOnly="true" Width="100px" Visible="false" Style="border-style:inset none none inset;"></asp:TextBox>
                        </td>
                  </tr>
     </table>

     <table>
     <tr>
 
     
  <td style="font-weight:normal; width: 144px;">  
                  <asp:Label ID="LabelPageFrom" runat="server" Text="Page From" ForeColor="Black"  ></asp:Label>
                       <%--<asp:Label ID="Labeljastr4" runat="server" Text="*" ForeColor="Red" Visible="false"></asp:Label>--%>
                        </td>
                         
         <td>
      <asp:TextBox ID="TextBoxPageFrom" runat="server"  Width="122px" MaxLength="10" 
                 Style="border-style:inset none none inset;" onchange="PagefromChange('TextBoxPageFrom','TextBoxPageTo');" 
                 ></asp:TextBox> 
   </td>
     
                                <td style="height: 36px; font-weight:normal; width: 97px;">
                    <asp:Label ID="LabelPageTo" runat="server" Text="Page To" ForeColor="Black"  ></asp:Label>
                        <%-- <asp:Label ID="Labeljastr5" runat="server" Text="*" ForeColor="Red" Visible="false"></asp:Label>--%>
                        </td>
                        <td style="height: 36px" >
                         <asp:TextBox ID="TextBoxPageTo" runat="server"   Width="118px" MaxLength="10" 
                                Enabled="false" Style="border-style:inset none none inset;"></asp:TextBox>
      
            </td>
            <td style="height: 36px; font-weight:normal; width: 83px;">
                                <asp:Label ID="LabelVolume" runat="server" Text="Volume" ForeColor="Black"  ></asp:Label>
                                
                                  <asp:Label ID="Labelvolstar8" runat="server" Text="*" ForeColor="Red" Visible="false"></asp:Label>
                                </td>
                        <td style="height: 36px">             
                       <asp:TextBox ID="TextBoxVolume" runat="server"   Width="137px" MaxLength="10" 
                                Style="border-style:inset none none inset;"></asp:TextBox>
     
                       
                         </td>
                           <td style="height: 36px; font-weight:normal; width: 75px;">  <asp:Label ID="Labelissue" runat="server" Text="Issue" ForeColor="Black"  ></asp:Label>
<%--          <asp:Label ID="Labeljastr7" runat="server" Text="*" ForeColor="Red" Visible="false"></asp:Label>--%>
      </td>

    <td  style="font-weight:normal" >  <asp:TextBox ID="TextBoxIssue" runat="server"  MaxLength="100"   Width="210px" Style="border-style:inset none none inset;"></asp:TextBox></td>
                         </tr>
                       
    </table>
 


  <br />

 
</asp:Panel>

    
<asp:Panel ID="popup" runat="server" Visible="false" CssClass="modelPopup" style="width: 1000px;height:400px;background-color:ghostwhite;">
<asp:Panel ID="popupstudent"   runat="server" >

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
<td><asp:Button ID="Button1" runat="server" Text="Search" OnClick="SearchStudentData" /></td>
<td><asp:Button ID="Button2" runat="server" Text="EXIT" OnClick="exit" /> </td>
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



 <asp:UpdatePanel ID="popupPanelAffilUpdate" UpdateMode="Conditional" runat="server">
        <ContentTemplate>
<asp:Panel ID="popupPanelAffil" runat="server" >
    <br />
    <center>
<table style="background:white">
<tr>
<th align="center" colspan="3"> Author </th>
</tr>
    <tr><td>&nbsp;</td></tr>
<tr>
<td>Search By Name: <asp:TextBox ID="affiliateSrch" runat="server"  ></asp:TextBox>

 Search By Id: <asp:TextBox ID="SrchId" runat="server"   ></asp:TextBox>
  <asp:Button ID="BtnEmployeeSearch" runat="server" Text="Search" OnClick="branchNameChanged" />

<asp:Button ID="Buttonexit4" runat="server" Text="EXIT" OnClick="exit" />

  </td> 
  
  </tr>
    <tr><td>&nbsp;</td></tr>
<tr>
<td colspan="3">
<asp:GridView ID="popGridAffil" runat="server"  AutoGenerateSelectButton="true" EmptyDataText="No User Found" GridLines="Both"
OnSelectedIndexChanged="popSelected1" AllowSorting="true"  AutoGenerateColumns="false" Height="215px" CellPadding="5"  CellSpacing="5">
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
         <asp:BoundField DataField="User_Id" HeaderText="User Id" ReadOnly="True" 
            SortExpression="User_Id"  ItemStyle-Width="200px"/>
        <asp:BoundField DataField="Name" HeaderText="Name" ReadOnly="True" 
            SortExpression="Name"  ItemStyle-Width="500px" />
        </Columns>
</asp:GridView>



<asp:SqlDataSource ID="SqlDataSourceAffil" runat="server" ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" 
SelectCommand="SELECT User_Id,prefix+' '+firstname+' '+middlename+' '+lastname  as Name,1 flag from User_M where User_Id=@UserId  UNION SELECT top 10  User_Id, prefix+' '+firstname+' '+middlename+' '+lastname  as Name, 2 flag from User_M  where Active='Y' order by flag" ProviderName="System.Data.SqlClient">
<SelectParameters>
<asp:SessionParameter Name="UserId" SessionField="UserId" Type="String" />
</SelectParameters>
</asp:SqlDataSource>

</td>
</tr>
</table>
        </center>
    <br />
</asp:Panel>
</ContentTemplate>
      <Triggers>
       <asp:PostBackTrigger ControlID="popGridAffil" />
   </Triggers>
     <Triggers>
       <asp:PostBackTrigger ControlID="Buttonexit4" />
   </Triggers>
</asp:UpdatePanel>

</asp:Panel>

 

  

  <asp:Panel ID="panelRemarks" runat="server" Visible="false" GroupingText="Remarks"  > 
 <table style="width: 92%">
                    <tr>
                        <td style="height: 36px; font-weight:normal">
                             <asp:Label ID="Labelrem" runat="server" Text="Remarks"  ></asp:Label>
                        </td>
                        <td style="height: 36px">
                     <asp:TextBox ID="TextBoxRemarks" runat="server" TextMode="MultiLine" Style="border-style:inset none none inset;" Enabled="true"  Rows="4" onkeypress="return this.value.length<500"
        Width="900px" ></asp:TextBox>   
        
                        </td>
                      
                     

               
                           </tr>
                           </table>
</asp:Panel>
<br />
 <br />
 <br />
 <br />
<table width="100%">
<tr>
<td align="center">
<asp:Button ID="btnSave" runat="server" Text="Update" OnClick="BtnSave_Click" Enabled="false" OnClientClick="ConfirmButtonStudentPub()" CausesValidation="true" ></asp:Button>
</td>
</tr>
</table>
<br />
</asp:Panel>
<br />

  <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="TextBoxVolume" Display="None" Enabled="true"
 ErrorMessage="Please enter the Volume"  ValidationGroup="validation1"></asp:RequiredFieldValidator>
  <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="TextBoxIssue" Display="None" Enabled="true"
 ErrorMessage="Please enter the Issue"  ValidationGroup="validation1"></asp:RequiredFieldValidator>--%>
  <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="TextBoxRemarks" Display="None" Enabled="true"
 ErrorMessage="Please enter the Remarks"  ValidationGroup="validation1"></asp:RequiredFieldValidator>
   <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBoxPageTo" Display="None" Enabled="false"
 ErrorMessage="Please enter Page to"  ValidationGroup="validation1"></asp:RequiredFieldValidator>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" 
                                    ControlToValidate="DropDownListMuCategory" Display="None" 
                                    ErrorMessage="Please select MU Category" InitialValue=" " 
                                    ValidationGroup="validation1"></asp:RequiredFieldValidator>

<asp:HiddenField ID="rowVal" runat="server" />
<asp:HiddenField ID="senderID" runat="server" />
<asp:HiddenField ID="rowVal1" runat="server" />
<asp:HiddenField ID="senderID1" runat="server" />
<asp:HiddenField ID="StudentPub" runat="server" />



</asp:Content>

