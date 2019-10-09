<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPageRMS.master" AutoEventWireup="true" CodeFile="GrantEntry.aspx.cs" Inherits="Grantentry" MaintainScrollPositionOnPostback="true" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script src="../Scripts/jquery-1.7.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-1.7.1.min.js" type="text/javascript"></script>
     <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" DisplayMode="BulletList" 
     HeaderText="Validation issues" ShowSummary="False" ValidationGroup="validation" />
<asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="True" DisplayMode="BulletList" 
     HeaderText="Validation issues" ShowSummary="False" ValidationGroup="validationUpload" />
  <style type="text/css">
      /*.gridViewHeader {
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
}*/

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

        .overlay {
            position: fixed;
            top: 0;
            bottom: 0;
            left: 0;
            right: 0;
            background: rgba(0, 0, 0, 0.7);
            transition: opacity 500ms;
            visibility: hidden;
            opacity: 0;
        }

            .overlay:target {
                visibility: visible;
                opacity: 1;
            }

        .popup {
            margin: 70px auto;
            padding: 20px;
            background: #fff;
            border-radius: 5px;
            width: 30%;
            position: relative;
            transition: all 5s ease-in-out;
        }

            .popup h2 {
                margin-top: 0;
                color: #333;
                font-family: Tahoma, Arial, sans-serif;
            }

            .popup .close {
                position: absolute;
                top: 20px;
                right: 30px;
                transition: all 50ms;
                font-size: 30px;
                font-weight: bold;
                text-decoration: none;
                color: #333;
            }

                .popup .close:hover {
                    color: #06D85F;
                }

            .popup .content {
                max-height: 90%;
                /*max-height: 30%;*/
                overflow: auto;
            }

        .popupp {
            margin: 120px auto;
            padding: 20px;
            background: #fff;
            border-radius: 5px;
            width: 30%;
            position: relative;
            transition: all 5s ease-in-out;
        }

            .popupp h2 {
                margin-top: 0;
                color: #333;
                font-family: Tahoma, Arial, sans-serif;
            }

            .popupp .close {
                position: absolute;
                top: 20px;
                right: 30px;
                transition: all 50ms;
                font-size: 30px;
                font-weight: bold;
                text-decoration: none;
                color: #333;
            }

                .popupp .close:hover {
                    color: #06D85F;
                }

            .popupp .content {
                max-height: 90%;
                /*max-height: 30%;*/
                overflow: auto;
            }

        .close1 {
            /*position: absolute;*/
            /*transition: all 200ms;*/
            font-size: 20px;
            font-weight: bold;
            text-decoration: underline;
            color: #333;
        }

            .close1:hover {
                color: #06D85F;
            }

        .test2 {
            background: White;
        }

        .auto-style27 {
            width: 14.5%;
        }
    </style>

     <script type="text/javascript">

         function ViewPdf() {
             debugger;
             window.open('<%= Page.ResolveUrl("~/PublicationEntry/DisplayPdf.aspx")%>', '_blank');
         }
         function callthis1() {
             debugger;
             var loc = $(location).attr('href');
             if (loc.match(/.*#/)) {
                 // loc = loc.substring(0,loc.length - 1);
                 window.location = loc + 'popup1';
             }
             else {
                 window.location = loc + '#popup1';
             }
         }



         function callthis2() {
             debugger;
             var loc = $(location).attr('href');
             if (loc.match(/.*#/)) {
                 // loc = loc.substring(0,loc.length - 1);
                 window.location = loc + 'commentpopup';
             }
             else {
                 window.location = loc + '#commentpopup';
             }
         }


         function callthis3() {
             debugger;
             var loc = $(location).attr('href');
             if (loc.match(/.*#/)) {
                 // loc = loc.substring(0,loc.length - 1);
                 window.location = loc + 'commentpopup2';
             }
             else {
                 window.location = loc + '#commentpopup2';
             }
         }

         function callthis4() {
             debugger;
             var loc = $(location).attr('href');
             if (loc.match(/.*#/)) {
                 // loc = loc.substring(0,loc.length - 1);
                 window.location = loc + 'commentpopup3';
             }
             else {
                 window.location = loc + '#commentpopup3';
             }
         }

         function callthis5() {
             debugger;
             var loc = $(location).attr('href');
             if (loc.match(/.*#/)) {
                 // loc = loc.substring(0,loc.length - 1);
                 window.location = loc + 'commentpopup4';
             }
             else {
                 window.location = loc + '#commentpopup4';
             }
         }
         function callthis6() {
             debugger;
             var loc = $(location).attr('href');
             if (loc.match(/.*#/)) {
                 // loc = loc.substring(0,loc.length - 1);
                 window.location = loc + 'commentpopup5';
             }
             else {
                 window.location = loc + '#commentpopup5';
             }
         }
         function callthis7() {
             debugger;
             var loc = $(location).attr('href');
             if (loc.match(/.*#/)) {
                 // loc = loc.substring(0,loc.length - 1);
                 window.location = loc + 'commentpopup6';
             }
             else {
                 window.location = loc + '#commentpopup6';
             }
         }
         function callthis8() {
             debugger;
             var loc = $(location).attr('href');
             if (loc.match(/.*#/)) {
                 // loc = loc.substring(0,loc.length - 1);
                 window.location = loc + 'commentpopup7';
             }
             else {
                 window.location = loc + '#commentpopup7';
             }
         }
    </script>

         <script type="text/javascript">
     function ToggleDisplay1() {
             debugger;
             // $("#commentpopup").dialog("close");
             $("#popup1.close").click()
             var loc = $(location).attr('href');
             window.location = "#"
     }


     function ToggleDisplay2() {
         debugger;
         // $("#commentpopup").dialog("close");
         $("#commentpopup.close").click()
         var loc = $(location).attr('href');
         window.location = "#"
     }


     function ToggleDisplay3() {
         debugger;
         // $("#commentpopup").dialog("close");
         $("#commentpopup2.close").click()
         var loc = $(location).attr('href');
         window.location = "#"
     }

     function ToggleDisplay4() {
         debugger;
         // $("#commentpopup").dialog("close");
         $("#commentpopup3.close").click()
         var loc = $(location).attr('href');
         window.location = "#"
     }
     function ToggleDisplay5() {
         debugger;
         // $("#commentpopup").dialog("close");
         $("#commentpopup4.close").click()
         var loc = $(location).attr('href');
         window.location = "#"
     }
     function ToggleDisplay6() {
         debugger;
         // $("#commentpopup").dialog("close");
         $("#commentpopup5.close").click()
         var loc = $(location).attr('href');
         window.location = "#"
     }

     function ToggleDisplay7() {
         debugger;
         // $("#commentpopup").dialog("close");
         $("#commentpopup6.close").click()
         var loc = $(location).attr('href');
         window.location = "#"
     }


     function ToggleDisplay8() {
         debugger;
         // $("#commentpopup").dialog("close");
         $("#commentpopup7.close").click()
         var loc = $(location).attr('href');
         window.location = "#"
     }
                 </script>



 <script type = "text/javascript">
     //function Confirm() {
     //    var confirm_value = document.createElement("INPUT");
     //    confirm_value.type = "hidden";
     //    confirm_value.name = "confirm_value";
     //    if (confirm("Do You Want To Continue?"))
     //        confirm_value.value = "Yes";
     //    else
     //        confirm_value.value = "No";
     //    document.forms[0].appendChild(confirm_value);
     //}

     function Confirm() {       
             var confirm_value = document.createElement("INPUT");
             confirm_value.type = "hidden";
             confirm_value.name = "confirm_value";
             confirm_value.value = "Yes";
             var hiddenStatusFlag = document.getElementById('<%= hdn.ClientID%>');
                if (confirm("Do you want to Delete the User Role??")) {
                    hiddenStatusFlag.value = "Yes";

                }
                else {
                   hiddenStatusFlag.value = "No";
                }            
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
<%--<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="true"></asp:ToolkitScriptManager>--%>
  <asp:ScriptManager ID="Scriptmanager1" runat="server"/>
     
         <asp:UpdatePanel ID="UpdatePanelSearchPub" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
     
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
    <asp:GridView ID="GridViewsanSearch" runat="server"  AllowPaging="True" 
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
        </asp:SqlDataSource>
 </center>

  </asp:Panel>
                                     </ContentTemplate>
             <Triggers>
                       <asp:PostBackTrigger ControlID="ButtonSearchProject" /> 
                   </Triggers>
            
</asp:UpdatePanel>
  <br />



  <asp:Panel ID="Panel7" runat="server" > 
        <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
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
     <asp:DropDownList ID="DropDownListTypeGrant" runat="server" Width="200px" AppendDataBoundItems="true"   DataSourceID="SqlDataSourceDropDownListTypeGrant" DataTextField="TypeName" DataValueField="TypeId" >
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

    <asp:Label ID="lblRevisedAppliedAmt" runat="server" Text="Sanction Amount" ForeColor="Black" Visible="false"></asp:Label>
</td>
<td> <asp:TextBox ID="txtRevisedAppliedAmt" runat="server" Width="200px" style="margin-left: 0px"  Visible="false" OnTextChanged="txtRevisedAppliedAmt_TextChanged" AutoPostBack="true"></asp:TextBox> </td>
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
 
    <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>


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


      <asp:ImageButton ID="imageBkCbtn" runat="server" ImageUrl="~/Images/srchImg.gif" OnClick="imageBkCbtn_Click" OnClientClick="setRow(this)"  /></td>

<%--                 <asp:ModalPopupExtender ID="model" runat="server" TargetControlID="imageBkCbtn" PopupControlID="popupselectNo"></asp:ModalPopupExtender>--%>
                  
    
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
                                </ContentTemplate>
        </asp:UpdatePanel>

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
</ContentTemplate>
            </asp:UpdatePanel>
 <br />
      
      <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>

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
          
                 <asp:ImageButton ID="EmployeeCodeBtn" runat="server" ImageUrl="~/Images/srchImg.gif" OnClick="showpopup" OnClientClick="setRowPop(this)" />

                <%-- <asp:ModalPopupExtender ID="ModalPopupExtender4" runat="server" TargetControlID="EmployeeCodeBtn" PopupControlID="popup"
                    BackgroundCssClass="modelBackground"  >
                                  </asp:ModalPopupExtender>--%>

                 <asp:ImageButton ID="ImageButton1" runat="server" Visible="false" ImageUrl="~/Images/srchImg.gif" OnClick="showpopup1" OnClientClick="setRowPop(this)" />
                
                <%--  <asp:ModalPopupExtender ID="ModalPopupExtender2" runat="server" TargetControlID="ImageButton1" PopupControlID="popup"
                    BackgroundCssClass="modelBackground"  >
                                  </asp:ModalPopupExtender>--%>
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
</ContentTemplate>
          </asp:UpdatePanel>



<br />
          <asp:UpdatePanel ID="UpdatePanel20" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
      <asp:Panel ID="PanelPercentage" runat="server" GroupingText="Percentage Sharing"  ForeColor="Black"  Style="font-weight:bold; border-style:inset;background-color:#E0EBAD" Visible="false"  > 
    <br />
    <asp:Button ID="LinkButton1" runat="server" Text="Instructions to enter  Percentage sharing" OnClick="LinkButton1_Click" />
    <asp:HiddenField ID="test3" runat="server" />
    <%-- <asp:ModalPopupExtender ID="ModalPopupExtender3" runat="server" TargetControlID="LinkButton1" PopupControlID="panelpopuptest"
                    BackgroundCssClass="modelBackground">
                 </asp:ModalPopupExtender>--%>

   
    <br />
    <br />
             <asp:Panel ID="Panel3" runat="server"   Width="1000px" GroupingText="Inter Organization" >
  
<asp:SqlDataSource ID="SqlDataSourceMuNonMuIO" runat="server" SelectCommand="select Type,InstId from InstitutionType_M" 
                ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" 
                ProviderName="System.Data.SqlClient"> 
                                     
            </asp:SqlDataSource>
     <asp:GridView ID="GridViewInterOrganization" runat="server" AutoGenerateColumns="False"  GridLines="None" 
      CellPadding="4" ForeColor="#333333"  >

     <AlternatingRowStyle BackColor="White" />
     <Columns>

        <asp:TemplateField HeaderText="MAHE/Non MAHE">
             <ItemTemplate>
                 <asp:DropDownList ID="DropdownMuNonMuIO" runat="server" Width="200"  AutoPostBack="true" Enabled="false" DataSourceID="SqlDataSourceMuNonMuIO" DataTextField="InstId" DataValueField="Type"  >
                     
          <%-- <asp:ListItem Value="M">MAHE</asp:ListItem>--%>
                           <%--    <asp:ListItem Value="S">MAHE-Student</asp:ListItem>--%>
                 <%--  <asp:ListItem Value="N">Non MAHE</asp:ListItem>--%>
                 </asp:DropDownList>
                 
                 <asp:ImageButton ID="MuNonMuP" runat="server" ImageUrl="~/Images/srchImg.gif" CssClass="blnkImgCSS"  />              

             </ItemTemplate>              
        </asp:TemplateField>
   
        <asp:TemplateField HeaderText="Institution Name">
         <ItemTemplate>
        <asp:TextBox ID="InstitutionNameIO" runat="server" Width="300" Enabled="false"></asp:TextBox> 
        <asp:DropDownList ID="DropdownStudentInstitutionName" runat="server" Width="200" Visible="false" AutoPostBack="true"  >
  </asp:DropDownList>
    <%-- <asp:SqlDataSource ID="SqlDataSourceDropdownStudentInstitutionName" runat="server" 
                                         ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" 
                                         ProviderName="System.Data.SqlClient" 
                                         SelectCommand="select Institute_Id,Institute_Name from Institute_M">
                                     </asp:SqlDataSource>--%>
             
             <asp:Image ID="Image4" runat="server" ImageUrl="~/Images/srchImg.gif" CssClass="blnkImgCSS" />           
             </ItemTemplate>              
        </asp:TemplateField>
            <%-- <asp:TemplateField HeaderText="Department Name" Visible="false">
             <ItemTemplate>
                 <asp:TextBox ID="DepartmentNameIO" runat="server" Width="200" Enabled="false"></asp:TextBox>
                     <asp:DropDownList ID="DropdownStudentDepartmentName" runat="server" Width="200" Visible="false"  >
              </asp:DropDownList>

                     <%-- <asp:SqlDataSource ID="SqlDataSourceDropdownStudentDepartmentName" runat="server" 
                                         ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" 
                                         ProviderName="System.Data.SqlClient" 
                                         SelectCommand="select DeptId,DeptName from Dept_M where Institute_Id=@Institute_Id ">
  <SelectParameters>
  
     <asp:ControlParameter Name="Institute_Id" 
      ControlID="DropdownStudentInstitutionName"
      PropertyName="SelectedValue"/>
  </SelectParameters>
    </asp:SqlDataSource>--%>

<%--     <asp:Image ID="Image5" runat="server" ImageUrl="~/Images/srchImg.gif" CssClass="blnkImgCSS" />
            </ItemTemplate>            
        </asp:TemplateField>--%>
        <%--<asp:TemplateField HeaderText="Department Name" Visible="false">
             <ItemTemplate>
                 <asp:TextBox ID="DepartmentNameIO" runat="server" Width="200" Enabled="false"></asp:TextBox>
                     <asp:DropDownList ID="DropdownStudentDepartmentName" runat="server" Width="200" Visible="false"  >
              </asp:DropDownList>--%>

                     <%-- <asp:SqlDataSource ID="SqlDataSourceDropdownStudentDepartmentName" runat="server" 
                                         ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" 
                                         ProviderName="System.Data.SqlClient" 
                                         SelectCommand="select DeptId,DeptName from Dept_M where Institute_Id=@Institute_Id ">
  <SelectParameters>
  
     <asp:ControlParameter Name="Institute_Id" 
      ControlID="DropdownStudentInstitutionName"
      PropertyName="SelectedValue"/>
  </SelectParameters>
    </asp:SqlDataSource>--%>

    <%-- <asp:Image ID="Image5" runat="server" ImageUrl="~/Images/srchImg.gif" CssClass="blnkImgCSS" />
            </ItemTemplate>            
        </asp:TemplateField>--%>
           <asp:TemplateField HeaderText="Sharing Amount( in %)">
         <ItemTemplate>
        <asp:TextBox ID="PercentageIO" runat="server" Width="150" OnTextChanged="PercentageIO_TextChanged" AutoPostBack="true"></asp:TextBox> 
                       
             </ItemTemplate>              
        </asp:TemplateField>
          <asp:TemplateField HeaderText="Sharing Amount(In INR)">
         <ItemTemplate>
        <asp:TextBox ID="PercentageIOAmount" runat="server" Width="150"></asp:TextBox> 
                       
             </ItemTemplate>              
        </asp:TemplateField>
   
  <asp:TemplateField ShowHeader="false">
    <ItemTemplate>
    <asp:HiddenField ID="InstitutionIO" runat="server" ></asp:HiddenField>               
   <asp:ImageButton ID="InstitutionBtn" runat="server"  ImageUrl="~/Images/srchImg.gif" CssClass="blnkImgCSS" />
  </ItemTemplate>              
        </asp:TemplateField>
            <%-- <asp:TemplateField ShowHeader="false">
             <ItemTemplate>
         <asp:HiddenField ID="DepartmentIO" runat="server" ></asp:HiddenField>
 <asp:ImageButton ID="DepartmentBtn" runat="server" Enabled="false" ImageUrl="~/Images/srchImg.gif"  CssClass="blnkImgCSS"  />
 </ItemTemplate>            
        </asp:TemplateField>--%>
<%--  <asp:CommandField DeleteText="Remove" ShowDeleteButton="True"   />--%>
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
          <br />
<asp:Panel ID="Panel4" runat="server"   Width="1000px" GroupingText="Inter Mahe Institute" >
  
     <asp:GridView ID="GridViewInterInstitute" runat="server" AutoGenerateColumns="False"  GridLines="None" 
      CellPadding="4" ForeColor="#333333"  >

     <AlternatingRowStyle BackColor="White" />
     <Columns>

        <asp:TemplateField HeaderText="MAHE/Non MAHE" Visible="false">
             <ItemTemplate>
                 <asp:DropDownList ID="DropdownMuNonMuII" runat="server" Width="250"  AutoPostBack="true" Enabled="false"  >
                     
                 <asp:ListItem Value="M">MAHE</asp:ListItem>
                           <%--    <asp:ListItem Value="S">MAHE-Student</asp:ListItem>--%>
                        <asp:ListItem Value="N">Non MAHE</asp:ListItem>
                 </asp:DropDownList>
                 
                 <asp:ImageButton ID="MuNonMuP" runat="server" ImageUrl="~/Images/srchImg.gif" CssClass="blnkImgCSS"  />              

             </ItemTemplate>              
        </asp:TemplateField>
   
        <asp:TemplateField HeaderText="Institution Name">
         <ItemTemplate>
        <asp:TextBox ID="InstitutionNameII" runat="server" Width="250" Enabled="false"></asp:TextBox> 
        <asp:DropDownList ID="DropdownStudentInstitutionName" runat="server" Width="250" Visible="false" AutoPostBack="true"  >
  </asp:DropDownList>
    <%-- <asp:SqlDataSource ID="SqlDataSourceDropdownStudentInstitutionName" runat="server" 
                                         ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" 
                                         ProviderName="System.Data.SqlClient" 
                                         SelectCommand="select Institute_Id,Institute_Name from Institute_M">
                                     </asp:SqlDataSource>--%>
             
             <asp:Image ID="Image4" runat="server" ImageUrl="~/Images/srchImg.gif" CssClass="blnkImgCSS" />           
             </ItemTemplate>              
        </asp:TemplateField>        
        <asp:TemplateField HeaderText="Department Name">
             <ItemTemplate>
                 <asp:TextBox ID="DepartmentNameII" runat="server" Width="250" Enabled="false"></asp:TextBox>
                     <asp:DropDownList ID="DropdownStudentDepartmentName" runat="server" Width="250" Visible="false"  >
              </asp:DropDownList>

                     <%-- <asp:SqlDataSource ID="SqlDataSourceDropdownStudentDepartmentName" runat="server" 
                                         ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" 
                                         ProviderName="System.Data.SqlClient" 
                                         SelectCommand="select DeptId,DeptName from Dept_M where Institute_Id=@Institute_Id ">
  <SelectParameters>
  
     <asp:ControlParameter Name="Institute_Id" 
      ControlID="DropdownStudentInstitutionName"
      PropertyName="SelectedValue"/>
  </SelectParameters>
    </asp:SqlDataSource>--%>

     <asp:Image ID="Image5" runat="server" ImageUrl="~/Images/srchImg.gif" CssClass="blnkImgCSS" />
            </ItemTemplate>            
        </asp:TemplateField>
           <asp:TemplateField HeaderText="Sharing Amount( in %)">
         <ItemTemplate>
        <asp:TextBox ID="PercentageII" runat="server" Width="150" OnTextChanged="PercentageII_TextChanged1" AutoPostBack="true" > </asp:TextBox> 
                       
             </ItemTemplate>              
        </asp:TemplateField>
          <asp:TemplateField HeaderText="Sharing Amount( in INR)">
         <ItemTemplate>
        <asp:TextBox ID="PercentageIIAmount" runat="server" Width="150"></asp:TextBox> 
                       
             </ItemTemplate>              
        </asp:TemplateField>
   
  <asp:TemplateField ShowHeader="false">
    <ItemTemplate>
    <asp:HiddenField ID="InstitutionII" runat="server" ></asp:HiddenField>               
   <asp:ImageButton ID="InstitutionBtn" runat="server"  ImageUrl="~/Images/srchImg.gif" CssClass="blnkImgCSS" />
  </ItemTemplate>              
        </asp:TemplateField>
             <asp:TemplateField ShowHeader="false">
             <ItemTemplate>
         <asp:HiddenField ID="DepartmentII" runat="server" ></asp:HiddenField>
 <asp:ImageButton ID="DepartmentBtn" runat="server" Enabled="false" ImageUrl="~/Images/srchImg.gif"  CssClass="blnkImgCSS"  />
 </ItemTemplate>            
        </asp:TemplateField>
<%--  <asp:CommandField DeleteText="Remove" ShowDeleteButton="True"   />--%>
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
                                </ContentTemplate>
                                </asp:UpdatePanel>
                                
 <br />
     <asp:Panel ID="PanelKindetails" runat="server" GroupingText="Details of Recieved Items" Visible="false" Style="font-weight:bold; border-style:inset;background-color:#E0EBAD">
        <table width="92%">
 <tr>
  <td>
<asp:Button ID="Button1" runat="server" Text="Add" onclick="addRowSancKind" /> 
 </td>
 </tr>
</table>
          <div style="margin-left:50px">
<asp:GridView ID="GridViewkindDetails" runat="server" AutoGenerateColumns="False"  GridLines="None" 
     OnRowDeleting="GridViewkindDetails_RowDeleting" CellPadding="4" ForeColor="#333333" >
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
   </div>
 </asp:Panel>

<div style="margin-left:0px" runat="server" id="div1">
   <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
<asp:Panel ID="GrantSanction" runat="server" GroupingText="Sanction - Details" Visible="false" Style="font-weight:bold; border-style:inset;background-color:#E0EBAD"  > 
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
     <asp:TextBox ID="TextBoxSanctionedAmountCapital" runat="server" Enabled="false" Width="120px" AutoPostBack="true"  ></asp:TextBox>
                              
 </td>
       <td class="auto-style51">
 
   <asp:Label ID="LabelSanctionedAmountOperating" runat="server" Text="Amount-Operating" ForeColor="Black" ></asp:Label>
                    
 </td>
 <td class="auto-style51">
     <asp:TextBox ID="TextBoxSanctionedAmountOperating" runat="server" Enabled="false" Width="120px" AutoPostBack="true"  ></asp:TextBox>
                              
 </td>
                <td class="auto-style50">
 
   <asp:Label ID="LabelSanctionedamountTotal" runat="server" Text="Total-Amount" ForeColor="Black"></asp:Label>
                    
 </td>
 <td class="auto-style50">
     <asp:TextBox ID="TextBoxSanctionedamountTotal" runat="server" Enabled="false" Width="120px" ></asp:TextBox>
                              
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
 
   <asp:Label ID="Label9" runat="server" Text="No Of Sanction" ForeColor="Black" ></asp:Label>
                    
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
  <center>
 <table>
 <tr>
 <td> <asp:Label runat="server" ID="lblnote" Visible="false" Text=" Note: Sanction details entered by finance users only"></asp:Label></td>
 </tr>   
 </table>

     </center> 
     <asp:Panel ID="Panel2" runat="server" GroupingText="Sanction-Amount-Details" Style="font-weight:bold; border-style:inset;background-color:#E0EBAD">
           <table width="92%">                  
          <tr>
            <td>
            <asp:Button ID="addsanction" runat="server" Text="Add" onclick="btnaddsanction" />                             
              </td>
            </tr>
            </table>
            <div style="margin-left:-3px">
     <asp:GridView ID="GridViewSanction" runat="server" AutoGenerateColumns="False"   OnRowDataBound="onRowDataBound"
     OnRowDeleting="Grid_Sanction_RowDeleting" CellPadding="2" ForeColor="#333333" >

     <AlternatingRowStyle BackColor="White" />

     <Columns>     
            <asp:TemplateField HeaderText="Sanction No">
         <ItemTemplate>
             <asp:TextBox ID="txtsanctionNo" runat="server" Width="120px"  ></asp:TextBox>     
             </ItemTemplate>              
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Sanction date">
             <ItemTemplate>
                 <asp:TextBox ID="txtSanctiondate" runat="server"  Enabled="true" Width="130px" ></asp:TextBox>
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
                 <asp:TextBox ID="txtsancapitalAmount" runat="server"  Enabled="true" Width="150px"></asp:TextBox>                
                 
                                  
            </ItemTemplate>            
        </asp:TemplateField>
         
           <asp:TemplateField HeaderText="Operating Amount">
             <ItemTemplate>
                 <asp:TextBox ID="txtSanOpeAmt" runat="server" Enabled="false" Width="130px"  AutoPostBack="true" OnTextChanged="AddTotal" ></asp:TextBox>
          
             </ItemTemplate>              
        </asp:TemplateField>

          <asp:TemplateField HeaderText = "Add Amount">
<ItemTemplate>
       <asp:Button ID="btnAddOPAmount" runat="server" Text="Add" OnClick="AddOPAmtClick"  OnClientClick="setRow(this)" Visible="true" />
  <asp:Button ID="dummybutton" runat="server" Style="display: none" />

   <%-- <asp:ModalPopupExtender ID="ModalPopupExtenderOPAmount" runat="server" TargetControlID="dummybutton" PopupControlID="PanelOPAmount"
                   BackgroundCssClass="modelBackground" ></asp:ModalPopupExtender>--%>
</ItemTemplate>

</asp:TemplateField>
          <asp:TemplateField HeaderText="Total Amount">
             <ItemTemplate>
                     <asp:TextBox ID="txtsantotalAmount" runat="server"   Width="100px" ></asp:TextBox>         
              </ItemTemplate>              
        </asp:TemplateField>
       
        <asp:TemplateField HeaderText = "Narration">
<ItemTemplate>
<asp:TextBox ID="txtNarration" Width="255px" runat="server" />
</ItemTemplate>
</asp:TemplateField>
  <asp:CommandField DeleteText ="Remove" ShowDeleteButton="true" ControlStyle-CssClass="btnsanremove"    />

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

      </asp:Panel><br />
        <center><asp:Button ID="BtnSaveSan" runat="server" Text="Save" OnClick="BtnSanctionSave_Click" Visible="false" CausesValidation="true" ValidationGroup="validation"></asp:Button></center>
</asp:Panel>
                                </ContentTemplate>
       </asp:UpdatePanel>
</div>

     <br />
      <asp:UpdatePanel ID="UpdatePanel8" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
            <asp:Panel ID="PnlBank" runat="server" Visible="false" GroupingText="Fund Received Details"   ForeColor="Black" Style="font-weight:bold; border-style:inset;background-color:#E0EBAD"  ScrollBars="Horizontal" > 
<asp:Panel ID="Panel1" runat="server" >
 <table width="92%">
                  
                           <tr>
                           <td>
                           <asp:Button ID="Button2" runat="server" Text="Add" onclick="addBank" Width="36px" /> 
                            
                           </td>
                           </tr>
                           </table>
                             
     <asp:GridView ID="GridView_bank" runat="server" AutoGenerateColumns="False"   OnRowDataBound="RowDataBoundBank"
     OnRowDeleting="Grid_Bank_RowDeleting" CellPadding="2" ForeColor="#333333" >

     <AlternatingRowStyle BackColor="White" />

     <Columns>     
         <asp:TemplateField HeaderText="Sanction Entry No">
<ItemTemplate>
<center>
<asp:DropDownList ID="ddlSanctionEntryNo" runat="server"  />
  </center>          
</ItemTemplate>      
</asp:TemplateField>  

         <asp:TemplateField HeaderText="Currency">
             <ItemTemplate>
                 <asp:DropDownList ID="CurrencyCode" runat="server" DataSourceID="SqlDataSourceCurrency" DataTextField="Code" Width="50px"  DataValueField="Code">
                 
                 </asp:DropDownList>
                   <asp:SqlDataSource ID="SqlDataSourceCurrency" runat="server" ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" 
           SelectCommand="select Code,CurrencyDescription from ProjectCurrency_M">
     
                </asp:SqlDataSource>  
               <%--  <asp:Image ID="ImageisAuthorType" runat="server" ImageUrl="~/Images/srchImg.gif" CssClass="blnkImgCSS" />--%>

                

             </ItemTemplate>              
        </asp:TemplateField>
     
          <asp:TemplateField HeaderText="Mode Of Recieve">
         <ItemTemplate>
            <asp:DropDownList ID="ModeOfRecevie" runat="server" DataSourceID="SqlDataSourceModeOfRec" DataTextField="RefName" Width="130px"  DataValueField="RefType">
  </asp:DropDownList>
     <asp:SqlDataSource ID="SqlDataSourceModeOfRec" runat="server" ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" 
   SelectCommand="select RefType, RefName from Mode_Recevie_M">     
                </asp:SqlDataSource>
             </ItemTemplate>              
        </asp:TemplateField>                  


         
        <asp:TemplateField HeaderText="Reccieved Date">
             <ItemTemplate>
                 <asp:TextBox ID="ReceviedDate" runat="server"  Enabled="true" Width="80px" ></asp:TextBox>
           <asp:CalendarExtender ID="CalendarExtender1" runat="server"
    TargetControlID="RecevieDdate" Format="dd/MM/yyyy" >
  </asp:CalendarExtender>    
<%-- <asp:Image ID="ImageRecevieddate" runat="server" ImageUrl="~/Images/srchImg.gif" CssClass="blnkImgCSS" />--%>
 <asp:RegularExpressionValidator ID="RegularExpressionValidator15" runat="server" Display="None"  ControlToValidate="Recevieddate" ValidationGroup="validation"
                ErrorMessage="Received date in (dd/mm/yyyy) format" SetFocusOnError="true"  
                ValidationExpression="^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$">



                </asp:RegularExpressionValidator>   
               
             </ItemTemplate>              
        </asp:TemplateField>
          <asp:TemplateField HeaderText="Received Amount">
         <ItemTemplate>
             <asp:TextBox ID="ReceviedAmount" runat="server" Width="70px"  ></asp:TextBox>     
             </ItemTemplate>              
        </asp:TemplateField>
         

           <asp:TemplateField HeaderText="INR Equivalent">
             <ItemTemplate>
                 <asp:TextBox ID="ReceviedINR" runat="server" Width="70px"  ></asp:TextBox>
          
             </ItemTemplate>              
        </asp:TemplateField>
          <asp:TemplateField HeaderText="TDS">
             <ItemTemplate>
                 <asp:TextBox ID="TDS" runat="server" Width="130px"  ></asp:TextBox>          
             </ItemTemplate>              
        </asp:TemplateField>
            <asp:TemplateField HeaderText="Reference No">
             <ItemTemplate>
                 <asp:TextBox ID="ReferenceNo" runat="server" Width="130px"  ></asp:TextBox>          
             </ItemTemplate>              
        </asp:TemplateField>

<%--
                <asp:TemplateField HeaderText="Bank Name"  Visible="true">
             <ItemTemplate>
                 <asp:TextBox ID="BankName" runat="server" ReadOnly="true" Enabled="false" Width="120px"></asp:TextBox>
                   <asp:ImageButton ID="EmployeeCodeBtn1" runat="server" ImageUrl="~/Images/srchImg.gif" OnClientClick="setRow(this)"  />

                 <asp:ModalPopupExtender ID="ModalPopupExtender5" runat="server" TargetControlID="EmployeeCodeBtn1"  PopupControlID="popupbank"
                    BackgroundCssClass="modelBackground"  >
                                  </asp:ModalPopupExtender>
                                  
            </ItemTemplate>            
        </asp:TemplateField>   --%>     
 <asp:TemplateField HeaderText="Received Bank" Visible="false" >
             <ItemTemplate>
         <asp:TextBox ID="Receivedbank" runat="server" ReadOnly="true" Enabled="false" Width="120px" Visible="false"></asp:TextBox>
          <asp:ImageButton ID="EmployeeCodeBtn2" runat="server" ImageUrl="~/Images/srchImg.gif" OnClientClick="setRow(this)"  />
 <%-- <asp:ModalPopupExtender ID="ModalPopupExtender6" runat="server" TargetControlID="EmployeeCodeBtn2"  PopupControlID="popupRbank"
                    BackgroundCssClass="modelBackground"  >
  </asp:ModalPopupExtender>--%>
    </ItemTemplate>            
        </asp:TemplateField>
    <asp:TemplateField HeaderText="Credited Bank" >
             <ItemTemplate>
      <asp:TextBox ID="CreditedBank" runat="server" ReadOnly="true" Enabled="false" Width="150px"></asp:TextBox>
    <asp:ImageButton ID="EmployeeCodeBtn3" runat="server" ImageUrl="~/Images/srchImg.gif" OnClick="CreditedBanksearchclick" OnClientClick="setRow(this)"  />
  <%-- <asp:ModalPopupExtender ID="ModalPopupExtender7" runat="server" TargetControlID="EmployeeCodeBtn3"  PopupControlID="popupCbank"
                    BackgroundCssClass="modelBackground"  >
   </asp:ModalPopupExtender>--%>
     </ItemTemplate>            
  </asp:TemplateField>        
  <asp:TemplateField Visible="false">
  <ItemTemplate>
  <asp:TextBox ID="ReceivedBankId" runat="server" Visible="false"></asp:TextBox>
 </ItemTemplate>            
   </asp:TemplateField>
          <asp:TemplateField Visible="false">
  <ItemTemplate>
  <asp:TextBox ID="CreditedBankId" runat="server" Visible="false"></asp:TextBox>
 </ItemTemplate>            
   </asp:TemplateField>
   <asp:TemplateField Visible="false">
  <ItemTemplate>
  <asp:TextBox ID="BankId" runat="server" Visible="false"></asp:TextBox>
 </ItemTemplate>            
   </asp:TemplateField>
   <asp:TemplateField HeaderText = "Narration" >
<ItemTemplate>
<asp:TextBox ID="ReceivedNarration" runat="server" Width="200"></asp:TextBox>
    </ItemTemplate>            
   </asp:TemplateField>

   <asp:CommandField ShowDeleteButton="true" />
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
    <br />
    <center><asp:Button ID="BtnSaveBank" runat="server" Text="Save" Visible="false" OnClick="BtnSaveFundDetails" CausesValidation="true" ValidationGroup="validation" ></asp:Button></center>
</asp:Panel>
                                </ContentTemplate>
          </asp:UpdatePanel>
<br />



 <br />
      <asp:UpdatePanel ID="UpdatePanel9" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>

 <asp:Panel ID="PanelIncentive" runat="server" GroupingText="Incentive  Details" Visible="false" ForeColor="Black" Style="font-weight:bold; border-style:inset;background-color:#E0EBAD"  >
 <table width="92%">
                  
                           <tr>
                           <td>
                       <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="AddIncentive" />
                            
                           </td>
                           </tr>
                           </table>
 

 <div style="margin-left:20px; margin-top:30px">
<asp:GridView runat="server" ID="gvIncentiveDetails"   ShowFooter="false" 
          PageSize="10" AutoGenerateColumns="false"  OnRowDataBound="IncentiveOnRowDataBound"
         ShowHeaderWhenEmpty="true" OnRowDeleting="gvIncentiveDetails_RowDeleting" 
         CellPadding="4" Width="697px">
<HeaderStyle CssClass="headerstyle" />
<Columns>
<asp:TemplateField HeaderText="Sanction Entry Number" ItemStyle-Width="100px">
<ItemTemplate>
<asp:DropDownList ID="ddlSanctionEntryNo" runat="server" Width="155px" />
            
</ItemTemplate>      
</asp:TemplateField> 
   
<asp:TemplateField HeaderText="Incentive Payment Date" ItemStyle-Width="50px">
<ItemTemplate>
<asp:TextBox ID="txtincentivedate" runat="server" Width="155px"  />
    <asp:CalendarExtender ID="CalendarExtender5" runat="server"
                TargetControlID="txtincentivedate" PopupButtonID="txtincentivedate"   Format="dd/MM/yyyy" >
</asp:CalendarExtender>
</ItemTemplate>      
</asp:TemplateField> 
<asp:TemplateField HeaderText = "Incentive Payment Amount" >
<ItemTemplate >
<asp:TextBox ID="txtincentiveAmount" Enabled="false" runat="server"  />
</ItemTemplate>
</asp:TemplateField>
    <asp:TemplateField HeaderText = "Add Amount">
<ItemTemplate>
       <asp:Button ID="btnAddAmount" runat="server" Text="Add Amount" OnClick="AddAmtClick" Visible="true" />
      <asp:HiddenField ID="Imagemisc" runat="server"   />
   <%-- <asp:ModalPopupExtender ID="ModalPopupExtenderAmount" runat="server" TargetControlID="Imagemisc" PopupControlID="PanelAmount"
                    BackgroundCssClass="modelBackground" ></asp:ModalPopupExtender>--%>
</ItemTemplate>

</asp:TemplateField>
    <asp:TemplateField HeaderText = "Narration">
<ItemTemplate>
<asp:TextBox ID="txtComments" Width="380px" runat="server" />
</ItemTemplate>

</asp:TemplateField>
<asp:CommandField ShowDeleteButton="true" />
</Columns>
     <HeaderStyle BackColor="#0B532D" Font-Bold="True" ForeColor="White" />
    <PagerStyle BackColor="#000000" ForeColor="#000000" HorizontalAlign="Center" />
        <RowStyle BackColor="#FFFFFF" ForeColor="#333333" />
        
</asp:GridView>

   </div>  
   <br />                  
<center><asp:Button ID="BtnSaveIncentive" runat="server" Text="Save" OnClick="BtnSaveIncentiveDetails"  CausesValidation="true" ValidationGroup="validation" ></asp:Button></center>
 </asp:Panel>
</ContentTemplate>
          </asp:UpdatePanel>
 
      <asp:UpdatePanel ID="UpdatePanel10" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>

 <asp:Panel ID="PanelOverhead" runat="server"  Visible="false" GroupingText="Overhead details" ForeColor="Black" Style="font-weight:bold; border-style:inset;background-color:#E0EBAD"   > 
  <table width="92%">
                  
                           <tr>
                           <td>
                     <asp:Button ID="Button9" runat="server" OnClick="btnAdd_Click1" Text="Add" />
                            
                           </td>
                           </tr>
                           </table>
  
  <div style="margin-left:-10px; margin-top:50px">
  <center>
  <asp:GridView ID="grvoverhead" runat="server"   AutoGenerateColumns="false" OnRowDeleting="grvoverhead_RowDeleting1" OnRowDataBound="RowDataBoundOverhead" PageSize="10" ShowFooter="false" ShowHeaderWhenEmpty="true" CellPadding="4">
 <HeaderStyle CssClass="headerstyle"  />
<Columns>
                  
    <asp:TemplateField HeaderText="Sanction Entry Number" ItemStyle-Width="100px">
<ItemTemplate>
<asp:DropDownList ID="ddlSanctionEntryNoOH" runat="server" Width="155px" />
            
</ItemTemplate>      
</asp:TemplateField>   
 <asp:TemplateField HeaderText="Overhead transfer Date">
     <ItemTemplate>
   <asp:TextBox ID="txtOverheaddate" runat="server" />
    <asp:CalendarExtender ID="CalendarExtender5" runat="server"
    TargetControlID="txtOverheaddate" Format="dd/MM/yyyy" >
  </asp:CalendarExtender> 
  </ItemTemplate>   
  </asp:TemplateField>
  <asp:TemplateField HeaderText="Overhead transfer Amount">
   <ItemTemplate>
 <asp:TextBox ID="txtOverheadAmount" runat="server" />
 </ItemTemplate>
  </asp:TemplateField>
     <asp:TemplateField HeaderText="JV Number">
   <ItemTemplate>
 <asp:TextBox ID="txtJVNumber" runat="server" />
 </ItemTemplate>
  </asp:TemplateField>
  <asp:TemplateField HeaderText = "Narration">
<ItemTemplate>
<asp:TextBox ID="txtoverheadComments" Width="295px" runat="server" />
</ItemTemplate>
</asp:TemplateField>
                <asp:CommandField ShowDeleteButton="true" />
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
            </center>
                  </div>
     <br />
           <center><asp:Button ID="BtnSaveOverhead" runat="server" Text="Save" OnClick="BtnSaveOverheadDetails"  CausesValidation="true" ValidationGroup="validation" ></asp:Button> </center> 
          </asp:Panel>
</ContentTemplate>
          </asp:UpdatePanel>
      <asp:UpdatePanel ID="UpdatePanel11" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>

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
               <asp:UpdatePanel ID="UpdatePanel17" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
             <asp:Panel ID="PanelViewUplodedfiles" runat="server" ForeColor="Black" 
                 GroupingText="View Uploaded files" Visible="false" Width="1000px">
                 <asp:GridView ID="GVViewFile" runat="server" AutoGenerateColumns="False" OnRowDataBound="GVViewFile_RowDataBound"
                     BorderStyle="Solid" CellPadding="3" CellSpacing="3" 
                     DataSourceID="DSforgridview" EmptyDataText="No Records Found"  Width="1000px"
                     HeaderStyle-ForeColor="White" 
                     onselectedindexchanged="GVViewFile_SelectedIndexChanged" 
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
                                </ContentTemplate>

                   </asp:UpdatePanel>
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
                     onselectedindexchanged="GVView1_SelectedIndexChanged" 
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
                                </ContentTemplate>
           <Triggers>
                                <asp:PostBackTrigger ControlID="Buttonupload" />

                            </Triggers>
          </asp:UpdatePanel>
        <br />
      <asp:UpdatePanel ID="UpdatePanel12" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>

       <asp:Panel ID="PanelProjectOutcome" runat="server" GroupingText="Projects Outcome" Visible="false" ForeColor="Black" Style="font-weight:bold; border-style:inset;background-color:#E0EBAD"  >
 <table width="92%">
                  
                           <tr>
                           <td>
                       <asp:Button ID="ADDProjectOutcome" runat="server" Text="Add" OnClick="ADDProjectOutcome_Click" />
                            
                           </td>
                           </tr>
                           </table>
 

 <div style="margin-left:20px; margin-top:30px">
<asp:GridView runat="server" ID="GridViewProjectsOutcome" ShowFooter="false" 
          PageSize="10" AutoGenerateColumns="false"  OnRowDataBound="GridViewProjectsOutcome_RowDataBound"
         ShowHeaderWhenEmpty="true" OnRowDeleting="GridViewProjectsOutcome_RowDeleting"
         CellPadding="4" Width="697px">
<HeaderStyle CssClass="headerstyle" />
<Columns>  
    <asp:TemplateField>
        <ItemTemplate>
             <%#Container.DataItemIndex+1 %>
        </ItemTemplate>
    </asp:TemplateField>
            <asp:TemplateField HeaderText="Date of Activity">
             <ItemTemplate>
                 <asp:TextBox ID="DateofoutcomeDate" runat="server"  Enabled="true" Width="100px"></asp:TextBox>
           <asp:CalendarExtender ID="CalendarExtender1" runat="server"
    TargetControlID="DateofoutcomeDate" Format="dd/MM/yyyy" >
  </asp:CalendarExtender>    
<%-- <asp:Image ID="ImageRecevieddate" runat="server" ImageUrl="~/Images/srchImg.gif" CssClass="blnkImgCSS" />--%>
 <asp:RegularExpressionValidator ID="RegularExpressionValidator15" runat="server" Display="None"  ControlToValidate="DateofoutcomeDate" ValidationGroup="validation"
                ErrorMessage="OutCome date in (dd/mm/yyyy) format" SetFocusOnError="true"  
                ValidationExpression="^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$">



                </asp:RegularExpressionValidator>   
               
             </ItemTemplate>              
        </asp:TemplateField>
    <asp:TemplateField HeaderText = "Outcome Details">
<ItemTemplate>
<asp:TextBox ID="txtProjectOutcomeDescription" Width="500px" runat="server" />
</ItemTemplate>

</asp:TemplateField>
    <%-- <asp:TemplateField HeaderText = "Add">
<ItemTemplate>
       <asp:Button ID="btnAddDescription" runat="server" Text="Add" OnClick="btnAddDescription_Click" Visible="true" />        
</ItemTemplate>

</asp:TemplateField>--%>
<asp:CommandField ShowDeleteButton="true" />
</Columns>
     <HeaderStyle BackColor="#0B532D" Font-Bold="True" ForeColor="White" />
    <PagerStyle BackColor="#000000" ForeColor="#000000" HorizontalAlign="Center" />
        <RowStyle BackColor="#FFFFFF" ForeColor="#333333" />
        
</asp:GridView>

   </div>  
   <br />                  
<center><asp:Button ID="Button1ProjectsOutcome" runat="server" Text="Save" OnClick="Button1ProjectsOutcome_Click"  CausesValidation="true" ValidationGroup="validation" ></asp:Button></center>
 </asp:Panel>
                                </ContentTemplate>
          </asp:UpdatePanel>
      <br />
      <asp:UpdatePanel ID="UpdatePanel13" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>

        <asp:Panel ID="PanelFinanceClosure"  Visible="false" runat="server" GroupingText="Finance Status Closure" Style="font-weight:bold; border-style:inset;background-color:#E0EBAD" Height="95px" >
                   <table>
              <tr>
                  <td >Finance Status of the project </td>
          <td class="auto-style57"><asp:DropDownList ID="DropDownList3" runat="server" Width="141px" Height="16px">
                          <asp:ListItem Value="OPE">Open</asp:ListItem>
                           <asp:ListItem Value="CLO">Closed</asp:ListItem>
                          </asp:DropDownList></td>
              
                  <td >Date of Completion</td>
          <td class="auto-style57" >  <asp:TextBox ID="TextBox3" runat="server" Width="100px"></asp:TextBox></td>
          <asp:CalendarExtender ID="CalendarExtender6" runat="server"
                TargetControlID="TextBox3" Format="dd/MM/yyyy" >
            </asp:CalendarExtender>
                 

<td>Remarks</td>
               
             
                  <td> <asp:TextBox ID="TextBox4" runat="server" Width="400px" Height="31px" TextMode="MultiLine"></asp:TextBox>
              </td> 
              </tr>
           
                </table>                  
                <center><asp:Button ID="Button11" runat="server" Text="Save" OnClick="BtnSaveFinanceStatus"  CausesValidation="true" ValidationGroup="validation" ></asp:Button> </center>
          </asp:Panel>
                                </ContentTemplate>
          </asp:UpdatePanel>
            <asp:UpdatePanel ID="UpdatePanel22" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
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
                    </ContentTemplate>
                </asp:UpdatePanel>
        <asp:UpdatePanel ID="UpdatePanel23" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
 <asp:Panel ID="panelCanelRemarks" runat="server" Visible="false" GroupingText="Remarks for Rejection" Style="font-weight:bold; border-style:inset;background-color:#E0EBAD"  > 
 <table style="width: 92%">
   <tr>
     <td style="height: 36px; font-weight:normal">
      <asp:Label ID="Label10" runat="server" Text="Remarks"  ></asp:Label>
       </td>
        <td style="height: 36px">
          <asp:TextBox ID="TextBoxRemarks" runat="server" TextMode="MultiLine" Style="border-style:inset none none inset;" Enabled="false"
        Width="900px" ></asp:TextBox>   
       </td>
     </tr>
</table>
</asp:Panel>
</ContentTemplate>
            </asp:UpdatePanel>

      <div id="commentpopup" class="overlay">
                <div class="popupp" style="width: 650px; height:400px;margin-top: 20px;">

                    <a class="close" href="#">&times;</a>
<%--                    <div class="content" style="overflow-y: scroll; height: auto; width: 700px;">--%>
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
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

 </ContentTemplate>

                        </asp:UpdatePanel>

<%--                    </div>--%>
                    <center><a class="close1" id="anch" href="#">Close</a></center>

                </div>

            </div>
            <br />


        <div id="commentpopup2" class="overlay">
                <div class="popupp" style="width: 800px; height:500px;margin-top: 20px;">

                    <a class="close" href="#">&times;</a>
                    <div class="content" style="overflow-y: scroll; height: auto; width: 800px;">
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>

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
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                            </div>

            </div>
            <br />

    </asp:Panel>
<asp:HiddenField ID="rowVal" runat="server" />
    <asp:HiddenField ID="senderID" runat="server" />
<%--    <asp:RequiredFieldValidator ID="RVDOR" runat="server" ErrorMessage="Enter the Applied date" ControlToValidate="TextBoxGrantDate" Display="None" ValidationGroup="validation" SetFocusOnError="true" ></asp:RequiredFieldValidator>--%>
       <asp:ValidationSummary ID="ValidationSummary3" runat="server" ShowMessageBox="True" DisplayMode="BulletList"  HeaderText="Validation issues" ShowSummary="False"  ValidationGroup="Validation"/>

    <asp:UpdatePanel ID="UpdatePanel19" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
<asp:Panel ID="panel6" runat="server" Style="font-weight:bold; border-style:groove;background-color:#E0EBAD" Visible="true" > 
<table width="100%">
    <tr>
<td align="center">
  <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="BtnSave_Click"  CausesValidation="true" ValidationGroup="validation" ></asp:Button>
 <asp:Button ID="Buttonclr" runat="server" Text="Clear" OnClick="addclik" CausesValidation="false"></asp:Button>
  <asp:Button ID="btnAddProject" runat="server" Text="Add New Project" 
        Visible="true"   CausesValidation="false" onclick="btnAddProject_Click"  ></asp:Button>
    <asp:Button ID="ButtonSavepdf" Height="20" Font-Bold="true" runat="server" Text="Download UTN Form" OnClick="BtnGenetratePdf" CausesValidation="false" Enabled="false"/>


</td>
 </tr>
</table>
</asp:Panel>
                                </ContentTemplate>
        </asp:UpdatePanel>




        <div id="popup1" class="overlay">
                <div class="popupp" style="width: 900px; height: 400px;margin-top: 20px;">

                    <a class="close" href="#">&times;</a>
                    <div class="content" style="overflow-y: scroll; height: 570px; width: 900px;">
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>


    <asp:Panel ID="popupselectNo" runat="server">
        <table>
            <tr>
                <td>
                    <br />
                </td>
            </tr>
        <tr>
           
<th class="auto-style1">Search Name: </th>
<td class="auto-style1"><asp:TextBox ID="agencysearch" runat="server"></asp:TextBox> </td></tr>
        <tr>
    <td colspan="2" align="center">
    <asp:Button ID="searchid" runat="server" Text="Search" OnClick="AgencyIdChanged" />
</td>
</tr>   
            </table>
        <asp:GridView ID="popGridagency" runat="server" AutoGenerateSelectButton="true" 
            OnPageIndexChanging="popGridagency_pageindex" OnSelectedIndexChanged="popSelectedagency"
             AllowSorting="true" Height="203px" Width="900px" AllowPaging="true" 
            PageSize="5" >
<FooterStyle BackColor="White" ForeColor="#8C4510"  />
        <HeaderStyle BackColor="#0b532d" Font-Bold="True" ForeColor="White" />
        <PagerStyle ForeColor="#0B532D" HorizontalAlign="Center" />
        <RowStyle BackColor="#FFFFFF" ForeColor="#000000" />
        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#FFF1D4" />
        <SortedAscendingHeaderStyle BackColor="#B95C30" />
        <SortedDescendingCellStyle BackColor="#F1E5CE" />
        <SortedDescendingHeaderStyle BackColor="#93451F" />
</asp:GridView>
        <asp:SqlDataSource ID="SqlDataSourceTextBoxGrantAgency" runat="server" ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>">
        
        </asp:SqlDataSource>
    <asp:Button ID="Buttonexit" runat="server" Text="EXIT" />
         </asp:Panel>


                            </ContentTemplate>

                          <%--  <Triggers>
                                <asp:PostBackTrigger ControlID="searchid" />
                                <asp:PostBackTrigger ControlID="Buttonexit" />

                            </Triggers>--%>


                        </asp:UpdatePanel>

                    </div>
                    <center><a class="close1" id="a3" href="#">Close</a></center>

                </div>

            </div>
            <br />

      

<asp:Panel ID="popupbank" runat="server" Visible="false" CssClass="modelPopup" Width="460px">

<table style="background:white">
<tr>
<th align="center" colspan="3"> Bank Details  </th>
</tr>
<tr>
<td>Search By Name: <asp:TextBox ID="txtbankname" runat="server"></asp:TextBox>
<asp:Button ID="Button4" runat="server" Text="Search" OnClick="branchNameChanged1" />
<asp:Button ID="Button5" runat="server" Text="EXIT" OnClick="exit" />
</td> 
</tr>
<tr>
<td colspan="3">
<asp:GridView ID="popupbankGrid" runat="server"  AutoGenerateSelectButton="true" EmptyDataText="No User Found"
OnSelectedIndexChanged="popSelected2" AllowSorting="true"  >
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
<asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" 
SelectCommand="SELECT [BankID], [BankName] FROM [ProjectBank_M]">
</asp:SqlDataSource>

</td>
</tr>
</table>
</asp:Panel>
     


<asp:Panel ID="popupRbank" runat="server" Visible="false" CssClass="modelPopup" Width="460px">

<table style="background:white">
<tr>
<th align="center" colspan="3"> Bank Details </th>
</tr>
<tr>
<td>Search By Name: <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
<asp:Button ID="Button10" runat="server" Text="Search" OnClick="branchNameChanged1" />
<asp:Button ID="Button6" runat="server" Text="EXIT" OnClick="exit" />
</td> 
</tr>
<tr>
<td colspan="3">
<asp:GridView ID="popupRecBank" runat="server"  AutoGenerateSelectButton="true" EmptyDataText="No User Found"
OnSelectedIndexChanged="popSelectedRecBank" AllowSorting="true"  >
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
<asp:SqlDataSource ID="SqlDataSourceRecB" runat="server" ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" 
SelectCommand="SELECT [BankID], [BankName] FROM [ProjectBank_M]">
</asp:SqlDataSource>

</td>
</tr>
</table>
</asp:Panel>

    <div id="commentpopup4" class="overlay">
                <div class="popupp" style="width: 700px; height: 450px;margin-top: 20px;">

                    <a class="close" href="#">&times;</a>
                    <div class="content" style="height: 500px; width: 900px;">
                        <asp:UpdatePanel ID="UpdatePanel15" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                <asp:Panel ID="popupCbank" runat="server" Visible="false" CssClass="modelPopup" Width="620px" Height="650px">

<table style="background:white">
<tr>
<th align="center" colspan="3"> Bank Details  </th>
</tr>
<tr>
<td>Search By Name: <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
<asp:Button ID="Button7" runat="server" Text="Search" OnClick="branchNameChanged2" />
<asp:Button ID="Button8" runat="server" Text="EXIT" OnClick="exitbnk" />
</td> 
</tr>
<tr>
<td colspan="3">
    <center>
<asp:GridView ID="popupCrB" runat="server"  AutoGenerateSelectButton="true" EmptyDataText="No User Found"
OnSelectedIndexChanged="popSelectedCreBank" AllowSorting="true"  >
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
        </center>
<asp:SqlDataSource ID="SqlDataSourceCreB" runat="server" ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" 
SelectCommand="SELECT [BankID], [BankName] FROM [ProjectBank_M]">
</asp:SqlDataSource>

</td>
</tr>
</table>
</asp:Panel>

</ContentTemplate>
                        </asp:UpdatePanel>

                    </div>
                    <center><a class="close1" id="a1" href="#"></a></center>

                </div>

            </div>
            <br />
           

      <div id="commentpopup5" class="overlay">
                <div class="popupp" style="width: 890px; height:200px;margin-top: 8px;">

                    <a class="close" href="#">&times;</a>
                    <div class="content" style="overflow-y: scroll; height: auto; width: 700px;">
                        <asp:UpdatePanel ID="UpdatePanel16" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
<asp:Panel ID="PanelAmount" runat="server" GroupingText="Incentive Add" CssClass="modelPopup" Style="font-weight:bold; background-color:#E0EBAD; width:800px ;height:300px margin-top: 9px;" >
 <asp:Label ID="rowLabel" runat="server" Visible="false"></asp:Label>
 <asp:Label ID="Sanction" runat="server" Visible="false"></asp:Label>
        <asp:GridView ID="popGridViewAmount" runat="server" AutoGenerateColumns="False" 
            DataKeyNames="ProjectUnit,ID"  EmptyDataText="No data found">
            <Columns>

             <asp:TemplateField HeaderText="Sl No" SortExpression="ID" ItemStyle-HorizontalAlign="Left">
                  
                    <ItemTemplate>
                    <asp:Label ID="LabelRow" runat="server" Text='<%# Bind("Row") %>'></asp:Label>
                    </ItemTemplate>

<ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:TemplateField> 
                    <asp:TemplateField ShowHeader="false" SortExpression="ProjectUnit" ItemStyle-HorizontalAlign="Left">
                 
                    <ItemTemplate>
                    <asp:Label ID="ProjectUnitLabel" runat="server" Text='<%# Bind("ProjectUnit") %>' Visible="false"></asp:Label>
                    </ItemTemplate>

<ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:TemplateField>
            
                       <asp:TemplateField HeaderText="Project Id" SortExpression="ID" ItemStyle-HorizontalAlign="Left">
                  
                    <ItemTemplate>
                    <asp:Label ID="IDLabel" runat="server" Text='<%# Bind("ID") %>'></asp:Label>
                    </ItemTemplate>

<ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:TemplateField>     
                
                   <asp:TemplateField HeaderText="Investigator Name" SortExpression="InvestigatorName" ItemStyle-HorizontalAlign="Left">
                  
                    <ItemTemplate>
                    <asp:Label ID="InvestigatorNameLabel" runat="server" Text='<%# Bind("InvestigatorName") %>'></asp:Label>
                    </ItemTemplate>

<ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:TemplateField>      
                <asp:TemplateField HeaderText = "Amount"  >
<ItemTemplate >
<asp:TextBox ID="txtAmount" runat="server" Enabled="true"  Text='<%# Bind("Amount") %>'/>
</ItemTemplate>
</asp:TemplateField>
      <asp:TemplateField HeaderText ="Institution">              
                <ItemTemplate >
                    <asp:Label ID="Institution" runat="server" Text='<%# Bind("Institution") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>            
            
            <asp:TemplateField HeaderText ="Dept">              
                <ItemTemplate >
                    <asp:Label ID="Dept" runat="server" Text='<%# Bind("Department") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField> 
                       
            </Columns>
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
        <asp:SqlDataSource ID="SqlDataSourceAddAmt" runat="server" 
            ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" 
             SelectCommand="SELECT ROW_NUMBER() OVER (ORDER BY [ID]) AS Row ,[ProjectUnit], [ID], [InvestigatorName] , '' as Amount,Institution,Department from Projectnvestigator where ProjectUnit=@ProjectUnit and ID=@ID" >         
             <SelectParameters>
                <asp:ControlParameter ControlID="DropDownListGrUnit" Name="ProjectUnit" PropertyName="SelectedValue" />
                <asp:ControlParameter ControlID="TextBoxID" Name="ID" PropertyName="Text" />
            </SelectParameters>
        </asp:SqlDataSource>
            <br />
            <center>
        <asp:Button ID="brnSubPo" runat="server" Text="Submit" OnClick="btnsubmitAmt" />
          <asp:Button ID="Button3" runat="server" Text="Exit" OnClick="ExitADD"  /></center>
                <br />
     </asp:Panel>

</ContentTemplate>

                        </asp:UpdatePanel>

                    </div>
                    <center><a class="close1" id="a4" href="#"></a></center>

                </div>

            </div>
            <br />
    
                                <div id="commentpopup3" class="overlay">
                <div class="popupp" style="width: 890px; height:370px;margin-top: 8px;">

                    <a class="close" href="#">&times;</a>
                    <div class="content" style="overflow-y: scroll; height: auto; width: 700px;">
                        <asp:UpdatePanel ID="UpdatePanel14" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>


      <asp:Panel ID="PanelOPAmount" runat="server"  width="800px" CssClass="modelPopup"  Style="font-weight:bold; background-color:#E0EBAD; margin-top: 9px;" >
 <asp:Label ID="Label11" runat="server" Visible="false"></asp:Label>
 <asp:Label ID="Label12" runat="server" Visible="false"></asp:Label>
 <br />
 <center>
        <asp:GridView ID="popgridOPAmount" runat="server" AutoGenerateColumns="False"  
            DataKeyNames="ID"  EmptyDataText="No data found">
            <Columns>
            <asp:TemplateField HeaderText="Sl No" SortExpression="ID" ItemStyle-HorizontalAlign="Left">
                  
                    <ItemTemplate>
                    <asp:Label ID="LabelRow" runat="server" Text='<%# Bind("Row") %>'></asp:Label>
                    </ItemTemplate>

<ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:TemplateField> 

                    <asp:TemplateField ShowHeader="false" SortExpression="ID" ItemStyle-HorizontalAlign="Left">
                 
                    <ItemTemplate>
                    <asp:Label ID="OPID" runat="server" Text='<%# Bind("ID") %>' Visible="false"></asp:Label>
                    </ItemTemplate>

<ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:TemplateField>
            
            <asp:TemplateField HeaderText="Item Name" SortExpression="ID" ItemStyle-HorizontalAlign="Left">
                  
                    <ItemTemplate>
                    <asp:Label ID="OPItemName" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                    </ItemTemplate>

<ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:TemplateField> 
        
                <asp:TemplateField HeaderText = "Amount"  >
<ItemTemplate >
<asp:TextBox ID="txtOPAmount" runat="server" Enabled="true"  Text='<%# Bind("Amount") %>'/>
</ItemTemplate>
</asp:TemplateField>
              
            </Columns>
            <HeaderStyle BackColor="#0B532D" Font-Bold="True" ForeColor="White" />
    <PagerStyle BackColor="#000000" ForeColor="#000000" HorizontalAlign="Center" />
        <RowStyle BackColor="#FFFFFF" ForeColor="#333333" />                
        </asp:GridView></center>
        <asp:SqlDataSource ID="SqlDataSource5" runat="server" 
            ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" 
             SelectCommand="SELECT ROW_NUMBER() OVER (ORDER BY [ID]) AS Row,ID, Name ,'' as Amount from OperatingItem_M " >         
            
        </asp:SqlDataSource>
            <br />
            <center>
        <asp:Button ID="Button12" runat="server" Text="Submit" OnClick="btnsubmitOPAmt" />
          <asp:Button ID="Button13" runat="server" Text="Exit" OnClick="exit"  /></center>
                <br />
     </asp:Panel>

  </ContentTemplate>

                        </asp:UpdatePanel>

                    </div>
                    <center><a class="close1" id="a2" href="#"></a></center>

                </div>

            </div>
            <br />


     <div id="commentpopup6"  class="overlay">
                <div class="popupp" style="width: 700px;background-color:#CCC; height:500px;margin-top: 20px;">                
                     <div class="content" style="height: auto; width: 700px;">

                        <asp:UpdatePanel ID="UpdatePanel18" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
         <asp:Panel ID="panelfeedback" Visible="false" runat="server"  >
       




    <table style="width: 80%; border-color:black">

        <tr>
<th align="center" colspan="3" style="font-size:19px; color:green">Please Enter Your Feedback</th>
</tr>
                    <tr>
                        <td style="font-weight:normal;color:Black" class="auto-style22">
                             <asp:Label ID="qustar" runat="server" ForeColor="Black" Text="*"></asp:Label>
                          <asp:Label ID="Q1" runat="server" Text="Please Rate your Ease of Project Entry" ForeColor="Black"></asp:Label>
                             
                               </td>
                          </tr>
                            <tr>
                                         <td >
                                             <asp:TextBox ID="txtq1" runat="server"
                                         Style="border-style:inset none none inset;" Width="100%"></asp:TextBox>
                             </td>
                        </tr>
                     
                        <tr>
                        <td style="font-weight:normal" class="auto-style22">
                                                         <asp:Label ID="qustar2" runat="server" ForeColor="Black" Text="*"></asp:Label>

                          <asp:Label ID="lblq2" runat="server" Text="Please indicate the ease of navigation to required Project" ForeColor="Black"></asp:Label>
                             
                               </td>
                          </tr>
                            <tr>
                                         <td >
                                             <asp:TextBox ID="txtq2" runat="server"
                                         Style="border-style:inset none none inset;" Width="100%"></asp:TextBox>
                             </td>
                        </tr>




                   <tr>
                        <td style="font-weight:normal" class="auto-style22">
                                                                                     <asp:Label ID="qustar3" runat="server" ForeColor="Black" Text="*"></asp:Label>

                          <asp:Label ID="lblq3" runat="server" Text="How do you rate the overall experience" ForeColor="Black"></asp:Label>
                             
                               </td>
                          </tr>
                            <tr>
                                         <td >
                                         <asp:RadioButtonList ID="txtq3" runat="server"    RepeatDirection="Horizontal"   Style="border-style:inset none none inset;">
                                        <asp:ListItem Value="Excellent" >Excellent</asp:ListItem>
                                       <asp:ListItem Value="Good">Good</asp:ListItem>
                                       <asp:ListItem Value="Improve">Can Improve</asp:ListItem>

 
                             </asp:RadioButtonList>     
                             </td>
                        </tr>




                           <tr>
                        <td style="font-weight:normal" class="auto-style22">
                             <asp:Label ID="qustar4" runat="server" ForeColor="Black" Text="*"></asp:Label>

                          <asp:Label ID="lblq4" runat="server" Text="Please provide your comment on overall area to improve" ForeColor="Black"></asp:Label>
                             
                               </td>
                          </tr>
                            <tr>
                                         <td >
                                             <asp:TextBox ID="txtq4" runat="server"
                                         Style="border-style:inset none none inset;" Width="100%" TextMode="MultiLine"></asp:TextBox>
                             </td>
                        </tr>                    

    </table>

<br />

             
            <asp:Button ID="BtnSubmitFeedback" runat="server"  Text="Submit" OnClick="BtnSubmitFeedback_Click" />
           <asp:Button ID="btnfedbackexit" runat="server" Text="EXIT" OnClick="exitfeedback" />    
                   
</asp:Panel>


                                  <asp:Panel ID="panel5" Visible="false" runat="server"  >
                                      <table>
                                          <tr>
                                              <td>
                                                  <asp:Label ID="lablfeedback" runat="server" Text="Feedback Details Already Entered."></asp:Label>
                                              </td>
                                          </tr>
                                      </table>
                                      </asp:Panel>
</ContentTemplate>
                           
                            <Triggers>
                                <asp:PostBackTrigger ControlID="BtnSubmitFeedback" />
                                 <asp:PostBackTrigger ControlID="btnfedbackexit" />

                            </Triggers>
                        </asp:UpdatePanel>
                         
                    </div>
                </div>

            </div>

                 <div id="commentpopup7" class="overlay">
                <div class="popupp" style="width: 1000px; height: 100px;margin-top: 20px;">

                    <a class="close" href="#">&times;</a>
                    <div class="content" style="height: 200px; width: 900px;">
                        <asp:UpdatePanel ID="UpdatePanel21" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
     <asp:Panel ID="panelpopuptest" runat="server" width="700px" CssClass="modelPopup"  >
    <center><b>Instructions to Enter Percentage Sharing</b></center>
<table border="1" align="center">

<tr>
<td>
1.Entered during the movement of Grant entry Changes to Sanctioned status. 

</td>
</tr>
<tr>
<td>
2.Applicable only to sanctions of the type CASH.



</td>
</tr>
<tr>
<td>
3.Percentage sharing of grants applicable among the MAHE and Non MAHE Institution .



</td>
</tr>
<tr>
<td>
4.Percentage sharing is based on sanctioned applied amount.

</td>
</tr>

</table>
         <br />
       <center>   <asp:Button ID="Button16" runat="server" Text="Exit" OnClick="Exitt"  /></center>
         <br />
         <br />
    </asp:Panel>
</ContentTemplate>
                        </asp:UpdatePanel>

                    </div>
                    <center><a class="close1" id="a5" href="#"></a></center>

                </div>

            </div>
            <br />
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
                                     <asp:HiddenField runat="server" ID="hdn" />

</asp:Content>

