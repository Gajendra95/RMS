<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPageRMS.master" AutoEventWireup="true" CodeFile="GrantNotification.aspx.cs" Inherits="Patent_GrantNotification" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

      <link href="../Styles/Style.css" rel="stylesheet" />

    <script type="text/javascript">
        function setRow(obj) {
            var row = obj.parentNode.parentNode;
            var rowIndex = row.rowIndex - 1;
            var sndID = obj.id;
            var sndrID = document.getElementById('<%= senderID.ClientID %>');
            sndrID.value = sndID;
            var rowNo = document.getElementById('<%= rowVal.ClientID %>');
            rowNo.value = rowIndex;
        }

        function ToggleDisplay() {
            // $("#commentpopup").dialog("close");
            $("#commentpopup .close").click()
            var loc = $(location).attr('href');
            window.location = "#"
        }
        function ToggleDisplay2() {
            // $("#commentpopup").dialog("close");
            $("#commentpopup2 .close").click()
            var loc = $(location).attr('href');
            window.location = "#"
        }
        function ToggleDisplay3() {
            // $("#commentpopup").dialog("close");
            $("#commentpopup3 .close").click()
            var loc = $(location).attr('href');
            window.location = "#"
        }
    </script>

    <link id="css" runat="server" href="" rel="stylesheet" type="text/css" />
    <a href="../Scripts/momentjs-2.3.0.jar"></a>
    <script src="../Scripts/jquery-1.7.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-1.7.1.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
     <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/moment.js/2.18.1/moment.min.js"></script>
     <script src="http://cdnjs.cloudflare.com/ajax/libs/moment.js/2.18.1/moment.min.js"></script>
    <script type="text/javascript">
        function callthis1() {
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

        function callthis2() {
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

        function callthis3() {
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


        function callthis4() {
            debugger;
            window.location = "#"
            var loc = $(location).attr('href');
            loc = loc + "commentpopup3";
            window.location = loc + '#popup2';
            //if (loc.match(/.*#/)) {
            //    // loc = loc.substring(0,loc.length - 1);
            //    window.location = loc + 'popup2';
            //}
            //else {
            //    window.location = loc + '#popup2';
            //}
        }



    </script>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" DisplayMode="BulletList"
        HeaderText="Validation issues" ShowSummary="False" ValidationGroup="validation" />
    <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="True" DisplayMode="BulletList"
        HeaderText="Validation issues" ShowSummary="False" ValidationGroup="validation1" />
   <%-- <script type="text/javascript" language="JavaScript">
        function Validate(sender, args) {
            var txt1 = document.getElementById("<%= PatIDSearch.ClientID %>");
            var txt2 = document.getElementById("<%= TextBoxtiltleSearch.ClientID%>");

            args.IsValid = (txt1.value != "") || (txt2.value != "");
        }
    </script>--%>
    <style>
        /*Calendar Control CSS*/
        .cal_Theme1 .ajax__calendar_days table tr td, .ajax__calendar_months table tr td,
        cal_Theme1 .ajax__calendar_years table tr td {
            padding: 0px;
            margin: 20px;
        }

        .cal_Theme1 .ajax__calendar_container {
            background-color: #fff;
            border: solid 1px #808080;
        }

        .cal_Theme1 .ajax__calendar_day {
            text-align: center;
        }

        .style2 {
            width: 139px;
        }

        .style3 {
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

        .auto-style18 {
            width: 276px;
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
    </style>
    <script type="text/javascript">
        function GetSelectedTextValue(ddlNoOfYears) {
            var ddlNoOfYears = document.getElementById('<%= ddlNoOfYears.ClientID%>').value;
            // var nextrenewaldate = document.getElementById('<%= txtnextRenewal.ClientID%>').value;
            var nextrenewaldate = document.getElementById('<%= hdnNextRD.ClientID%>').value;
            var new_date = moment(nextrenewaldate, "DD-MM-YYYY").add(ddlNoOfYears, 'years');

            var day = new_date.format('DD');
            var month = new_date.format('MM');
            var year = new_date.format('YYYY');
            document.getElementById('<%= txtnextRenewal.ClientID%>').value = day + "/" + month + "/" + year;
            //var selectedText = ddlFruits.options[ddlFruits.selectedIndex].innerHTML;
            //var selectedValue = ddlFruits.value;
            //alert("Selected Text: " + selectedText + " Value: " + selectedValue);
        }
    </script>
    <script type="text/javascript">
        function ConfirmRenewalDetails() {
            debugger;

            var validated = Page_ClientValidate('validation1');
            if (validated) {
                var nextrenewaldate = document.getElementById('<%= txtnextRenewal.ClientID%>').value;
                var renewaldate = document.getElementById('<%= txtRenewalDate.ClientID%>').value;
                var hiddenStatusFlag = document.getElementById('<%= HiddenEntryConfirm1.ClientID%>');
                var fillingstatus = document.getElementById('<%= ddlFilingstatus.ClientID %>').value;
                if (fillingstatus == "GRN") {

                    //var date1 = new Date(nextrenewaldate);
                    //var date2 = new Date(renewaldate);

                    var date1 = moment(nextrenewaldate, "DD/MM/YYYY");
                    var date2 = moment(renewaldate, "DD/MM/YYYY");

                    if (date2 <= date1) {
                        hiddenStatusFlag.value = "Yes";
                    }
                    else {
                        if (confirm(" Renewal date has exceeded the next renewal date. Patent will be moved to Lapsed state. Do you want to confirm"))
                            hiddenStatusFlag.value = "LAPSE";
                        else
                            // confirm_value12.value = "No";
                            hiddenStatusFlag.value = "No";
                    }
                  
                }
               
            }


        }
        //}
    </script>
         <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="true"></asp:ToolkitScriptManager>
      <center> <asp:Label ID="lblnotification" runat="server" Text="Grant Notification" Font-Bold="true"  ></asp:Label></center>
            <br />
    <center>
        <table>
            <tr>
                <td class="auto-style16"> 
                    
                    <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal" CssClass="spaced" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged" AutoPostBack="true" Style="border-style:inset none none inset;" Width="394px">
                    <asp:ListItem Value="PtoC" Selected="True">Patent to be Complete</asp:ListItem>       
                      <asp:ListItem Value="GtoR"  > Patent to be Renewal</asp:ListItem>
                       </asp:RadioButtonList></td>
                   
          <%--  <td >
                 <asp:Button ID="BtnSearch" runat="server" Text="Search" OnClick="BtnSearch_Click" Width="105px" Height="22px"  />
            </td>--%>
              
            </tr>
            
        </table>
   
        <br />
        <br />
    <asp:GridView ID="GridProTocomplete" runat="server" AllowPaging="True" OnPageIndexChanging="GridProTocomplete_PageIndexChanging" OnRowDataBound="GridProTocomplete_RowDataBound"
        PagerSettings-PageButtonCount="5" PageSize="5"  OnRowCommand="GridProTocomplete_RowCommand"
        HeaderStyle-BackColor="#CC6600" HeaderStyle-ForeColor="Black" AutoGenerateColumns="False" 
        PagerStyle-BackColor="#CC6600" PagerStyle-ForeColor="White" CellPadding="3"  
        CellSpacing="3" PagerStyle-Width="4" PagerStyle-Height="4" Visible="False" 
        BorderColor="#FF6600" BorderStyle="Solid" Width="711px" DataKeyNames="ID">
         <Columns>
              <asp:TemplateField ShowHeader="False" >
   <ItemTemplate>
   <asp:ImageButton ID="BtnEdit" runat="server" CausesValidation="False" CommandName="View" ImageUrl="~/Images/view.gif" ToolTip="View"  />
   </ItemTemplate>
   </asp:TemplateField>  
            <asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID" ReadOnly="True" />
            <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title" /> 
             <asp:BoundField DataField="Date_Of_Application" HeaderText="Date Of Application" DataFormatString="{0:dd/MM/yyyy}"  SortExpression="Date_Of_Application" /> 
             <%--<asp:BoundField DataField="NatureOfPatent" HeaderText="NatureOfPatent" 
                SortExpression="NatureOfPatent" />--%>
            <asp:BoundField DataField="CreatedBy" HeaderText="Created By" SortExpression="CreatedBy" />
  
            
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
       <%-- <EditRowStyle BorderColor="#FF6600" BorderStyle="Solid" />--%>



    </asp:GridView>

    <asp:GridView ID="GridGRNtoRenewal" runat="server" AllowPaging="True" OnPageIndexChanging="GridGRNtoRenewal_PageIndexChanging" OnRowDataBound="GridGRNtoRenewal_RowDataBound"
        PagerSettings-PageButtonCount="5" PageSize="5" OnRowCommand="GridGRNtoRenewal_RowCommand"
        HeaderStyle-BackColor="#CC6600" HeaderStyle-ForeColor="Black" AutoGenerateColumns="False" 
        PagerStyle-BackColor="#CC6600" PagerStyle-ForeColor="White" CellPadding="3"  
        CellSpacing="3" PagerStyle-Width="4" PagerStyle-Height="4" Visible="False" 
        BorderColor="#FF6600" BorderStyle="Solid" Width="710px" DataKeyNames="ID">
         <Columns>
              <asp:TemplateField ShowHeader="False" >
   <ItemTemplate>
   <asp:ImageButton ID="BtnEdit" runat="server" CausesValidation="False" CommandName="Edit" ImageUrl="~/Images/view.gif" ToolTip="Edit"  />
   </ItemTemplate>
   </asp:TemplateField>  
            <asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID" ReadOnly="True" />
            <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title" /> 
             <asp:BoundField DataField="Date_Of_Application" HeaderText="Date Of Application" DataFormatString="{0:dd/MM/yyyy}" SortExpression="Date_Of_Application" /> 
            <%-- <asp:BoundField DataField="NatureOfPatent" HeaderText="NatureOfPatent" 
                SortExpression="NatureOfPatent" />--%>
            <asp:BoundField DataField="CreatedBy" HeaderText="Created By" SortExpression="CreatedBy" />
  
            
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


    </asp:GridView> </center>
    <br /><br />
     <asp:UpdatePanel ID="up" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
           
            
            <asp:Panel ID="Panel6" runat="server" GroupingText="Basic Details" ForeColor="Black" Style="font-weight: bold; border-style: inset; background-color: #E0EBAD" Enabled="false" Visible="false">

                <table style="width: 878px; margin-bottom: 8px">
                    <tr>
                        <td class="auto-style1"></td>
                    </tr>
                    <tr>
                        <td class="auto-style1">ID </td>
                        <td class="auto-style1">
                            <asp:TextBox ID="txtID" runat="server" Enabled="false"></asp:TextBox>
                        </td>
                        <td class="auto-style1" style="visibility: hidden;">UTN
                        </td>
                        <td class="auto-style1">
                            <asp:TextBox ID="txtPatUTN" runat="server" Enabled="false" Visible="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style18">Filing Status
                        </td>
                        <td class="auto-style18">
                            <asp:DropDownList ID="ddlFilingstatus" runat="server" Width="130px"
                                DataSourceID="SqlDataSourePatentStatus" DataTextField="StatusName" DataValueField="Id"
                                OnSelectedIndexChanged="OnselectFilingStatus" AutoPostBack="true" Height="16px">
                            </asp:DropDownList>
                        </td>

                        <td class="auto-style18">Nature of Patent 
                              <asp:Label ID="Label13" runat="server" ForeColor="Red" Text="*"></asp:Label>
                        </td>
                        <td class="auto-style9">
                            <asp:DropDownList ID="ddlNatureofPatent" runat="server" Height="16px" Width="129px" Enabled="false">
                                <asp:ListItem Value="" Text="Select"></asp:ListItem>
                                <asp:ListItem Text="Complete" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Provisional" Value="2"></asp:ListItem>


                            </asp:DropDownList>
                        </td>

                        <td class="auto-style13">&nbsp;Funding 
                              <asp:Label ID="Label1" runat="server" ForeColor="Red" Text="*"></asp:Label>
                        </td>
                        <td class="auto-style13">
                            <asp:DropDownList ID="ddlFunding" runat="server" Width="135px" Enabled="false">
                                <asp:ListItem Text="Select" Value=""></asp:ListItem>
                                <asp:ListItem Text="MAHE" Value="1"></asp:ListItem>
                                <asp:ListItem Text="External" Value="2"></asp:ListItem>
                                <asp:ListItem Text="Self" Value="2"></asp:ListItem>

                            </asp:DropDownList>

                        </td>

                    </tr>
                    <tr>
                        <td class="auto-style20">Title
                            <asp:Label ID="Label5" runat="server" ForeColor="Red" Text="*"></asp:Label>
                        </td>

                        <td colspan="3" class="auto-style20">

                            <asp:TextBox ID="txtTitle" runat="server" Width="514px" Enabled="false"></asp:TextBox>

                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style21">Description
                        </td>
                        <td colspan="3" class="auto-style21">
                            <asp:TextBox ID="txtde" runat="server" Width="514px" Enabled="false"></asp:TextBox>
                        </td>
                        <%-- <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtFilingDateProvided" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                        </asp:CalendarExtender>--%>
                        <asp:CalendarExtender ID="CalendarExtender6" runat="server" TargetControlID="txtdateofApplication" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                        </asp:CalendarExtender>
                        <asp:CalendarExtender ID="CalendarExtender7" runat="server" TargetControlID="txtGrantDate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                        </asp:CalendarExtender>
                        <%--<asp:CalendarExtender ID="CalendarExtender4" runat="server" OnClientDateSelectionChanged="txtlastRenewalFee_TextChanged" TargetControlID="txtlastRenewalFee" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                        </asp:CalendarExtender>--%>
                        <asp:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="txtAppDate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                        </asp:CalendarExtender>
                    </tr>
                </table>

                <%--    <asp:ModalPopupExtender ID="ModalPopupApp" runat="server" TargetControlID="btnview" PopupControlID="PopAppStage" BehaviorID="btnview"
                   BackgroundCssClass="modalBackground" ></asp:ModalPopupExtender>
         <asp:ModalPopupExtender ID="Modalpoprenewal" runat="server" TargetControlID="btnRenewalview" PopupControlID="PoppanelRenewal"
                   BackgroundCssClass="modalBackground" ></asp:ModalPopupExtender>
        <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btndummy" PopupControlID="dummy"
                   BackgroundCssClass="modalBackground" ></asp:ModalPopupExtender>--%>
            </asp:Panel>
            <br />
            <asp:Panel ID="panAddAuthor" runat="server" GroupingText="Inventors Details" ForeColor="Black" Style="font-weight: bold; border-style: inset; background-color: #E0EBAD" Visible="false">

                <%--       <center>
          Author Details
                     </center>--%>
                <br />
                <table width="92%">
                    <tr>
                        <td>
                            <asp:Button ID="BtnAddMU" runat="server" Text="Add New Inventor" CausesValidation="false" OnClick="addRow" Enabled="false" />

                        </td>
                    </tr>
                </table>
                <br />

                <asp:SqlDataSource ID="SqlDataSourceAuthorType" runat="server"
                    ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>"
                    ProviderName="System.Data.SqlClient"></asp:SqlDataSource>
                <%--   <asp:UpdatePanel ID="EditUpdatePanel" runat="server" UpdateMode="Conditional">
    <Triggers>
<asp:AsyncPostBackTrigger ControlID = "Grid_AuthorEntry" />
</Triggers>
        <ContentTemplate>--%>
                <asp:Panel ID="PanelMU" runat="server" ScrollBars="both" Width="1000px" Enabled="false">

                    <asp:GridView ID="Grid_AuthorEntry" runat="server" AutoGenerateColumns="False" GridLines="None" OnRowDataBound="OnRowDataBound"
                        OnRowDeleting="Grid_Patent_RowDeleting" CellPadding="4" ForeColor="#333333">

                        <AlternatingRowStyle BackColor="White" />
                        <Columns>

                            <asp:TemplateField HeaderText="MAHE/Non MAHE">
                                <ItemTemplate>
                                    <asp:DropDownList ID="DropdownMuNonMu" runat="server" Width="95" OnSelectedIndexChanged="DropdownMuNonMuOnSelectedIndexChanged" AutoPostBack="true" DataSourceID="SqlDataSourceAuthorType">

                                        <%--                                        <asp:ListItem Value="M">MU-Staff</asp:ListItem>
                                        <asp:ListItem Value="S">MU-Student</asp:ListItem>
                                        <asp:ListItem Value="N">Non MU</asp:ListItem>--%>
                                    </asp:DropDownList>

                                    <asp:ImageButton ID="MuNonMu" runat="server" ImageUrl="~/Images/srchImg.gif" CssClass="blnkImgCSS" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ShowHeader="false" Visible="false" HeaderText="Roll No/Employee Code">
                                <ItemTemplate>
                                    <asp:TextBox ID="EmployeeCode" runat="server" Width="155" Visible="false" MaxLength="9"></asp:TextBox>
                                    <asp:Image ID="ImageEmpCode" runat="server" ImageUrl="~/Images/srchImg.gif" CssClass="blnkImgCSS" />


                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Investigator Name">
                                <ItemTemplate>
                                    <asp:TextBox ID="AuthorName" runat="server" Width="200" Enabled="false" OnTextChanged="AuthorName_Changed" AutoPostBack="true"></asp:TextBox>

                                    <asp:ImageButton ID="EmployeeCodeBtn" runat="server" ImageUrl="~/Images/srchImg.gif" CausesValidation="false" OnClientClick="setRow(this)" OnClick="showpopup" />
                                    <%--<asp:ModalPopupExtender ID="ModalPopupExtender4" runat="server" TargetControlID="EmployeeCodeBtn" PopupControlID="popupPanelAffil"
                                BackgroundCssClass="modalBackground">
                            </asp:ModalPopupExtender>--%>
                                    <asp:ImageButton ID="ImageButton1" runat="server" Visible="false" ImageUrl="~/Images/srchImg.gif" CausesValidation="false" OnClientClick="setRow(this)" />

                                    <%-- <asp:ModalPopupExtender ID="ModalPopupStudent" runat="server" TargetControlID="ImageButton1" PopupControlID="popupstudent"
                                BackgroundCssClass="modalBackground">
                            </asp:ModalPopupExtender>--%>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Institution Name">
                                <ItemTemplate>
                                    <asp:TextBox ID="InstitutionName" runat="server" Width="200" Enabled="false"></asp:TextBox>
                                    <asp:DropDownList ID="DropdownStudentInstitutionName" runat="server" Width="200" Visible="false" AutoPostBack="true" DataSourceID="SqlDataSourceDropdownStudentInstitutionName" DataTextField="Institute_Name" DataValueField="Institute_Id">
                                    </asp:DropDownList>
                                    <asp:SqlDataSource ID="SqlDataSourceDropdownStudentInstitutionName" runat="server"
                                        ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>"
                                        ProviderName="System.Data.SqlClient"
                                        SelectCommand="select Institute_Id,Institute_Name from Institute_M"></asp:SqlDataSource>

                                    <asp:Image ID="Image4" runat="server" ImageUrl="~/Images/srchImg.gif" CssClass="blnkImgCSS" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Department Name">
                                <ItemTemplate>
                                    <asp:TextBox ID="DepartmentName" runat="server" Width="200" Enabled="false"></asp:TextBox>
                                    <asp:DropDownList ID="DropdownStudentDepartmentName" runat="server" Width="200" Visible="false" DataSourceID="SqlDataSourceDropdownStudentDepartmentName" DataTextField="DeptName" DataValueField="DeptId">
                                    </asp:DropDownList>

                                    <asp:SqlDataSource ID="SqlDataSourceDropdownStudentDepartmentName" runat="server"
                                        ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>"
                                        ProviderName="System.Data.SqlClient"
                                        SelectCommand="select DeptId,DeptName from Dept_M where Institute_Id=@Institute_Id ">
                                        <SelectParameters>

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
                                    <%--<asp:Image ID="ImageMailId" runat="server" ImageUrl="~/Images/srchImg.gif" CssClass="blnkImgCSS" />--%>
                                    <asp:RegularExpressionValidator
                                        ID="regEmail"
                                        ControlToValidate="MailId" Display="Static" ErrorMessage="Invalid Email Id"
                                        Text="(Invalid email)" ValidationGroup="validation"
                                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                        runat="server" />
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
                            <asp:TemplateField HeaderText="Colloboration">
                                <ItemTemplate>
                                    <asp:DropDownList ID="NationalType" runat="server" Width="140" OnSelectedIndexChanged="NationalTypeOnSelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem Value="N">National</asp:ListItem>
                                        <asp:ListItem Value="I">International</asp:ListItem>
                                    </asp:DropDownList>
                                    <%--<asp:Image ID="ImageisNationalType" runat="server" ImageUrl="~/Images/srchImg.gif" CssClass="blnkImgCSS" />--%>
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
                                    <%--<asp:Image ID="ImageisContinent" runat="server" ImageUrl="~/Images/srchImg.gif" CssClass="blnkImgCSS" />--%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ShowHeader="false">
                                <ItemTemplate>
                                    <asp:HiddenField ID="Institution" runat="server"></asp:HiddenField>
                                    <%--<asp:ImageButton ID="InstitutionBtn" runat="server"  ImageUrl="~/Images/srchImg.gif" CssClass="blnkImgCSS" />--%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ShowHeader="false">
                                <ItemTemplate>
                                    <asp:HiddenField ID="Department" runat="server"></asp:HiddenField>
                                    <%--<asp:ImageButton ID="DepartmentBtn" runat="server" Enabled="false" ImageUrl="~/Images/srchImg.gif"  CssClass="blnkImgCSS"  />--%>
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
                <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
            </asp:Panel>
            <br />







            <asp:Panel ID="Panel1" runat="server" BorderStyle="Inset" GroupingText="Filling Details" Font-Bold="true" Style="background-color: #E0EBAD" Visible="false">
                <br />
                <asp:Panel ID="Panelfilling" runat="server">
                    <table style="height: 213px" id="maintable" runat="server">
                        <tr>
                            <td>
                                <br />
                                &nbsp;Filing Office
                                 <asp:Label ID="Label3" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td>
                                <br />
                                <%--   <asp:TextBox ID="txtFilingOffice" runat="server"></asp:TextBox>--%>

                                <asp:DropDownList ID="ddlfilingoffice" runat="server" Height="16px" Width="129px" DataSourceID="SqlDataSourceFilingOffice"
                                    DataTextField="F_OfficeName" DataValueField="Id" AppendDataBoundItems="true" Enabled="false">
                                    <asp:ListItem Value=" ">--Select--</asp:ListItem>
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="SqlDataSourceFilingOffice" runat="server" ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>"
                                    SelectCommand="select Id, F_OfficeName from Pat_FilingOffice_M"></asp:SqlDataSource>

                            </td>

                            <td class="auto-style5">&nbsp;Application Number
                                  <asp:Label ID="Label2" runat="server" ForeColor="Red" Text="*"></asp:Label>

                            </td>
                            <td class="auto-style5">
                                <asp:TextBox ID="txtapplicationNo" runat="server" Enabled="false"></asp:TextBox>
                                
                            </td>

                            <td class="auto-style23">Application Stage 
                            </td>
                            <td class="auto-style24">
                                <asp:TextBox ID="txtApplicationStage" runat="server" Enabled="false"></asp:TextBox>
                            </td>
                            <td>
                                <%--<asp:ModalPopupExtender ID="ModalPopupApp" runat="server" TargetControlID="btnview" PopupControlID="PopAppStage"
                   BackgroundCssClass="modelBackground" ></asp:ModalPopupExtender>--%>
                                <asp:Button ID="btnview" runat="server" Text="View" OnClick="Btn_App_View" CausesValidation="false" Enabled="false" />
                            </td>
                        </tr>
                        <tr>

                            <td class="auto-style23">&nbsp;Date of Application
                                <asp:Label ID="Label4" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="auto-style24">

                                <asp:TextBox ID="txtdateofApplication" runat="server"></asp:TextBox>

                                <%--<asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtdateofApplication" Format="dd/MM/yyyy" CssClass="cal_Theme1"> 
   </asp:CalendarExtender>--%>
                            </td>
                            <%--<td class="auto-style11">&nbsp;Provisional Number
                            </td>
                            <td class="auto-style11">
                                <asp:TextBox ID="txtProvisionalNo" runat="server"></asp:TextBox>
                            </td>--%>

                            <td class="auto-style2">Patent Number
                                 <asp:Label ID="lblpatent" Visible="false" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="auto-style5">
                               
                                <asp:TextBox ID="txtPatentNo" runat="server" Enabled="false"></asp:TextBox>

                            </td>
                            <%-- <td class="auto-style23">&nbsp;Filing Date provided by Patent&nbsp;Office
                            </td>

                            <td class="auto-style24">
                                <asp:TextBox ID="txtFilingDateProvided" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator32" runat="server" Display="None" ValidationGroup="validation" ControlToValidate="txtFilingDateProvided" InitialValue="" ErrorMessage="Please enter filling date" />
                                <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="Enter valid filling date" Display="None" ControlToValidate="txtFilingDateProvided" ValidationGroup="validation"
                                    Type="Date" Operator="DataTypeCheck" SetFocusOnError="true"></asp:CompareValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" Display="None" ControlToValidate="txtFilingDateProvided" ValidationGroup="validation"
                                    ErrorMessage="Enter the filling date in dd/mm/yyyy format" SetFocusOnError="true"
                                    ValidationExpression="^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"> </asp:RegularExpressionValidator>
                            </td>--%>

                            <td class="auto-style5">Grant Date
                                 <asp:Label ID="lblgdate" Visible="false" runat="server" ForeColor="Red" Text="*"></asp:Label>
                            </td>
                            <td class="auto-style5">
                               
                                <asp:TextBox ID="txtGrantDate" runat="server" Enabled="false" AutoPostBack="true"></asp:TextBox>

                            </td>

                        </tr>
                        <tr>

                            <td>Last Renewal Date
                            </td>
                            <td>
                                <asp:TextBox ID="txtlastRenewal" runat="server" Enabled="false"></asp:TextBox>


                                <asp:Button ID="btnRenewalview" runat="server" Text="View" CausesValidation="false" Enabled="false" OnClick="btnRenewalview_Click" />

                            </td>

                            <td class="auto-style23" runat="server" id="tdremarks">Remarks
                            </td>
                            <td class="auto-style8" colspan="3">
                                <asp:TextBox ID="txtRemark" runat="server" Width="402px" TextMode="MultiLine" Rows="2" Enabled="false"></asp:TextBox>

                            </td>

                        </tr>
                    </table>
                </asp:Panel>
                <table id="rejecttable">
                    <tr>
                        <td class="auto-style1">
                            <asp:Label ID="lblRejectRemarks" runat="server" Text="Rejection Remark" Visible="false"></asp:Label>
                        </td>
                        <td class="auto-style8">
                            <asp:TextBox ID="txtRejectionRemark" runat="server" Visible="false" Rows="2" TextMode="MultiLine" Width="654px"></asp:TextBox>

                        </td>
                    </tr>
                </table>
                <asp:SqlDataSource ID="SqlDataSourePatentStatus" runat="server" ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>"
                    SelectCommand="select Id,StatusName from Patent_Status where Id='APP'"></asp:SqlDataSource>
                <%--  <div style="margin-left:500px">
            <asp:Button ID="Button2" runat="server" Text="Submit" OnClick="Btn_Sumbit" />
            <%--<asp:Button ID="Button1" runat="server" Text="Update" />--%>
                <%--<asp:Button ID="Button3" runat="server" Text="Cancel" />--%>
            </asp:Panel>
            <div id="commentpopup3" class="overlay">
                <div class="popupp" style="width: 800px; height: auto;">

                    <a class="close" href="#">&times;</a>
                    <div class="content" style="overflow-y: scroll; height: auto; width: 800px;">
                        <asp:Panel ID="PoppanelRenewal" runat="server" GroupingText="Renewal Details" Visible="false" Style="font-weight: bold; background-color: #E0EBAD; border: inset; border-color: black; margin-top: 9px;">
                            <br />
                            <br />
                            <center>
                                <asp:GridView ID="grdRenewal" runat="server" AutoGenerateColumns="False" DataSourceID="sqlRenewal" EmptyDataText="No data found">
                                    <Columns>

                                        <asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID" />
                                        <asp:BoundField DataField="RenewalAmount" HeaderText="Renewal Amount" SortExpression="RenewalAmount" />
                                        <asp:BoundField DataField="LastRenewal_Date" HeaderText="Last Renewal Date" SortExpression="LastRenewal_Date" DataFormatString="{0:d}" ItemStyle-Width="150px" />
                                        <asp:BoundField DataField="RenewalComment" HeaderText="Renewal Comment" SortExpression="RenewalComment" ItemStyle-Width="250px" />


                                    </Columns>
                                    <HeaderStyle BackColor="#0B532D" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#000000" ForeColor="#000000" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#FFFFFF" ForeColor="#333333" />
                                </asp:GridView>
                            </center>
                            <asp:SqlDataSource ID="sqlRenewal" runat="server"
                                ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>"
                                SelectCommand="SELECT [ID], [RenewalAmount], [LastRenewal_Date], [RenewalComment] FROM [Patent_Renewal_Tracker] where ID=@ID ">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="txtID" DefaultValue="_" Name="ID" PropertyName="Text" Type="String" />

                                </SelectParameters>
                            </asp:SqlDataSource>
                            <br />
                            <table width="100%">
                                <tr>

                                    <td style="width: 20%; " >Renewal Fee
                                    </td>

                                    <td  style="width: 20%; ">
                                        <asp:TextBox ID="txtRenewalFee" runat="server"></asp:TextBox>
                                    </td>
                                    <td  style="width: 20%; ">No of Years
                                    </td>

                                    <td >
                                        <asp:DropDownList ID="ddlNoOfYears" runat="server"  Width="50%" onchange="GetSelectedTextValue(this)">
                                            <asp:ListItem Value="1">1</asp:ListItem>
                                            <asp:ListItem Value="2">2</asp:ListItem>
                                            <asp:ListItem Value="3">3</asp:ListItem>
                                            <asp:ListItem Value="4">4</asp:ListItem>
                                            <asp:ListItem Value="5">5</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td  style="width: 20%; ">&nbsp;Renewal Date
                                    </td>
                                    <td  style="width: 20%; ">
                                        <asp:TextBox ID="txtRenewalDate" runat="server"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtRenewalDate" Format="dd/MM/yyyy" CssClass="cal_Theme1">
                                        </asp:CalendarExtender>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Enter the renewal date" ControlToValidate="txtRenewalDate" Display="None" ValidationGroup="validation1" SetFocusOnError="true"></asp:RequiredFieldValidator>
                                        <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Renewal Date must be less than or equal to the current date." Display="None" ControlToValidate="txtRenewalDate" ValidationGroup="validation1"
                                            Operator="LessThanEqual" Type="Date" SetFocusOnError="true"></asp:CompareValidator>
                                        <asp:CompareValidator ID="CompareValidator8" runat="server" ErrorMessage="Enter valid date" Display="None" ControlToValidate="txtRenewalDate" ValidationGroup="validation"
                                            Type="Date" Operator="DataTypeCheck" SetFocusOnError="true"></asp:CompareValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator15" runat="server" Display="None" ControlToValidate="txtRenewalDate" ValidationGroup="validation"
                                            ErrorMessage="Enter the renewal date in given formate" SetFocusOnError="true"
                                            ValidationExpression="^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"> </asp:RegularExpressionValidator>
                                    </td>
                                    <td  style="visibility: visible;">Next Renewal Date
                                    </td>
                                    <td  style="width: 20%; ">
                                        <asp:TextBox ID="txtnextRenewal" runat="server" Enabled="false" Visible="true"></asp:TextBox>

                                    </td>
                                </tr>
                             
                                    <td  style="width: 20%; ">Comment
                                    </td>
                                    <td >
                                        <asp:TextBox ID="txtRenewalComment" runat="server" Width="347px"></asp:TextBox>
                                    </td>

                                    <%-- <td class="auto-style17">Next Renewal Year
                                    </td>--%>
                                    
                                </tr>
                                <tr>
                                   <td>

                                        <asp:TextBox ID="txtNextRenewalYear" runat="server" Visible="false" Enabled="false"></asp:TextBox>
                                    </td> 
                                    <td><asp:HiddenField ID="hdnNextRD" runat="server" /></td></tr>
                            </table>

                            <center>
                                
                            </center>

                            <center>
                                <asp:Button ID="btnSaveRenewal" runat="server" Text="Save" OnClick="btnRenewal_Click" Width="42px" CausesValidation="true" ValidationGroup="vaidation1" OnClientClick="ConfirmRenewalDetails();" />
                        </asp:Panel>
                    </div>
                    <center><a class="close1" id="a2" href="#">Close</a></center>
                </div>
            </div>

            <br />

            <asp:Panel ID="Panel5" runat="server">

                <div style="margin-left: 500px">
                    <asp:Button ID="Btnsave" runat="server" Text="Save" OnClick="Btn_Save" CausesValidation="true" ValidationGroup="validation" Visible="false" />
                   <%-- <asp:Button ID="BtnDraft" runat="server" Text="Save Draft" OnClick="Btn_Save_Draft" Visible="false" CausesValidation="true" ValidationGroup="validation" />
                    <asp:Button ID="Btnsubmit" runat="server" Text="Submit" OnClick="Btn_Data_Sumbit" Visible="false" CausesValidation="true" ValidationGroup="validation" />

                    <asp:Button ID="Button3" runat="server" Text="Clear" OnClick="btn_Clear" Visible="false" />--%>

                </div>

            </asp:Panel>


            <br />

            <div id="commentpopup2" class="overlay">
                <div class="popupp" style="width: 900px; height: auto;">

                    <a class="close" href="#">&times;</a>
                    <div class="content" style="overflow-x: scroll; height: auto; width: 900px;">



                        <asp:Panel ID="PopAppStage" runat="server" GroupingText="Application Stage Details" Width="850px" Visible="false" Style="font-weight: bold; background-color: #E0EBAD; border: inset; border-color: black; margin-top: 9px; height: auto;">

                            <asp:Label ID="Label11" runat="server" Visible="false"></asp:Label>
                            <asp:Label ID="Label12" runat="server" Visible="false"></asp:Label>
                            <br />
                            <center>
                                <asp:GridView ID="popgridApp" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource5" EmptyDataText="No data found" OnRowCommand="popgridApp_RowCommand">
                                    <Columns>
                                        <asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID" />
                                        <asp:BoundField DataField="Application_Stage" HeaderText="ApplicationStage" SortExpression="Application_Stage" />
                                        <asp:BoundField DataField="App_Date" HeaderText="App Date" SortExpression="App_Date" DataFormatString="{0:d}" />
                                        <asp:BoundField DataField="App_Comment" HeaderText="App Comment" SortExpression="App_Comment" ItemStyle-Width="250px" />
                                        <asp:TemplateField ShowHeader="false">
                                            <ItemTemplate>

                                                <asp:ImageButton ID="lbtnDelete" runat="server" CausesValidation="False" CommandName="DeleteRow" ImageUrl="~/Images/delete.jpg" ToolTip="DeleteRow" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ShowHeader="false">
                                            <ItemTemplate>

                                                <asp:Label ID="lblstatus" runat="server" CausesValidation="False" Text='<%# Eval("statusid") %>' Visible="false" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ShowHeader="false">
                                            <ItemTemplate>

                                                <asp:Label ID="lblentryno" runat="server" CausesValidation="False" Text='<%# Eval("entryno") %>' Visible="false" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <HeaderStyle BackColor="#0B532D" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#000000" ForeColor="#000000" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#FFFFFF" ForeColor="#333333" />
                                </asp:GridView>
                            </center>
                            <asp:SqlDataSource ID="SqlDataSource5" runat="server"
                                ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>"
                                SelectCommand="Select_App_Stage" SelectCommandType="StoredProcedure">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="txtID" DefaultValue="_" Name="ID" PropertyName="Text" Type="String" />

                                </SelectParameters>
                            </asp:SqlDataSource>
                            <br />
                            <table>
                                <tr>
                                    <td>Application Stage:
                                    </td>
                                    <td class="auto-style12">
                                        <asp:DropDownList ID="ddlAppstage" runat="server" DataTextField="StatusName" DataValueField="Id" DataSourceID="SqlAppStage" AppendDataBoundItems="True">
                                            <asp:ListItem Selected="True" Text="Select" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:SqlDataSource ID="SqlAppStage" runat="server" ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" SelectCommand="SELECT * FROM [Patent_App_Status]"></asp:SqlDataSource>
                                    </td>
                                    <td class="auto-style4">&nbsp;</td>
                                    <td>Date:
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
                                <asp:Button ID="BtnAPPsave" runat="server" Text="Save" OnClick="btnApp_Submit" Width="42px" />
                                <br />
                                <center>
                                    <asp:Label runat="server" Visible="false" Text="Application Stage Details Saved Sucessfully" ID="lblnoteApp"></asp:Label></center>
                        </asp:Panel>

                    </div>
                    <center><a class="close1" id="a1" href="#">Close</a></center>

                </div>

            </div>

            <br />
            <br />
            <br />
            <br />
            <div id="commentpopup" class="overlay">
                <div class="popupp" style="width: 700px; height: auto;">

                    <a class="close" href="#">&times;</a>
                    <div class="content" style="overflow-y: scroll; height: auto; width: 700px;">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>

                                <asp:Panel ID="popupstudent" Style="background: white" runat="server" Visible="false" CssClass="modalPopup" Width="850px" Height="400px">
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
                                                    OnSelectedIndexChanged="StudentDataSelect" AllowSorting="true" AutoGenerateColumns="false">
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
                                                    SelectCommand="Select TOP 10  RollNo,Name,SISClass.ClassName as ClassName,SISInstitution.InstName as InstName,EmailID1 ,[SISStudentGenInfo].ClassCode as ClassCode,[SISStudentGenInfo].InstID as InstID from [dbo].[SISStudentGenInfo],SISClass,SISInstitution where [SISStudentGenInfo].ClassCode=SISClass.ClassCode and [SISStudentGenInfo].InstID=SISInstitution.InstID" ProviderName="System.Data.SqlClient">
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



                                <asp:Panel ID="popupPanelAffil" runat="server" Visible="false" Width="600px">

                                    <table>
                                        <tr>
                                            <th align="center" colspan="3">Author </th>
                                        </tr>
                                        <tr>
                                            <td>Search By Name:
                                <asp:TextBox ID="affiliateSrch" runat="server"></asp:TextBox>
                                                <asp:Button ID="BtnEmployeeSearch" runat="server" Text="Search" OnClick="AuthorNameChanged" />
                                                <asp:Button ID="Buttonexit4" runat="server" Text="EXIT" OnClick="exit" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                                <asp:GridView ID="popGridAffil" runat="server" AutoGenerateSelectButton="true" EmptyDataText="No User Found" Width="600px"
                                                    OnSelectedIndexChanged="popSelected1">
                                                    <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
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
                                                    SelectCommand="SELECT top 10  User_Id,prefix+' '+UPPER(firstname)+' '+UPPER(middlename)+' '+UPPER(lastname)  as Name from User_M" ProviderName="System.Data.SqlClient"></asp:SqlDataSource>

                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                    </div>
                    <center><a class="close1" id="anch" href="#">Close</a></center>

                </div>

            </div>
            <br />
            <br />
            <br />




            <div id="popup2" class="overlay">
                <div class="popup" style="width: 500px; height: 234px;">
                    <div class="content" style="height: 234px; width: 500px;">
                        <br />
                        <br />
                        <br />

                        <asp:UpdatePanel ID="update2" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <center>

                                    <asp:Label ID="lblconfirm1" runat="server" Text="Current date has exceeded the next renewal date.Patent will be moved to Lapsed state. Do you want to confirm??" Font-Size="Medium" ForeColor="brown" Visible="false"></asp:Label>
                                    <%--<asp:Label ID="lblconfirm2" runat="server" Text="This Request is been created by the Student. Do you want to continue??" Font-Size="Medium" ForeColor="brown" Visible="false"></asp:Label><br />--%>

                                    <br />
                                    <br />
                                    <asp:Button ID="btn_ok" runat="server" Text="OK" Visible="true" Width="57px" Height="25px" />
                                    <asp:Button ID="btn_CANCEL" runat="server" Text="CANCEL" Visible="true" OnClick="btn_CANCEL_Click" Height="25px" />

                                </center>
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="btn_ok" />
                                <asp:PostBackTrigger ControlID="btn_CANCEL" />

                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>


        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:HiddenField ID="HiddenEntryConfirm1" runat="server" />
    <asp:HiddenField ID="rowVal" runat="server" />
    <asp:HiddenField ID="senderID" runat="server" />

</asp:Content>

