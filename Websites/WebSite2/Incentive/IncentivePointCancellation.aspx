<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPageRMS.master" AutoEventWireup="true" CodeFile="IncentivePointCancellation.aspx.cs" Inherits="Incentive_IncentivePointCancellation" MaintainScrollPositionOnPostback="true" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script src="../Scripts/jquery-1.7.1.min.js" type="text/javascript"></script>
    <script src="../JavaScript/JavaScript.js" type="text/javascript"></script>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" DisplayMode="List"
        HeaderText="Validation issues" ShowSummary="False" ValidationGroup="validation" />
      <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="True" DisplayMode="List"
        HeaderText="Validation issues" ShowSummary="False" ValidationGroup="validation1" />
    <asp:ScriptManager ID="Scriptmanager1" runat="server" />
    <style type="text/css">
        .hidden-field {
            display: none;
        }
    </style>

   <%-- <script type="text/javascript">
        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you want to discard article to enter incentive point?")) {
                confirm_value.value = "Yes";
                document.getElementById('<%=hdn.ClientID %>').value = "Yes";
            } else {
                confirm_value.value = "No";
                document.getElementById('<%=hdn.ClientID %>').value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
    </script>--%>

    <%--<script type="text/javascript">
        function checkDecimals(fieldName, fieldValue) {
            alert("gdfg");
            decallowed = 2;
            if (isNaN(fieldValue) || fieldValue == "") {
                alert("Not a valid number.try again.");
                fieldName.select();
                fieldName.focus();
            }
            else {
                if (fieldValue.indexOf('.') == -1) fieldValue += ".";
                dectext = fieldValue.substring(fieldValue.indexOf('.') + 1, fieldValue.length);
                if (dectext.length > decallowed) {
                    alert("Enter a number with up to " + decallowed + "decimal places. try again.");
                    fieldName.select();
                    fieldName.focus();
                }
                else {
                    alert("Number validated successfully.");
                }
            }
        }
</script>--%>

    <%--  <script type="text/javascript">
        function onlyDotsAndNumbers(txt, event) {
            var charCode = (event.which) ? event.which : event.keyCode
            if (charCode == 46) {
                if (txt.value.indexOf(".") < 0)
                    return true;
                else
                    return false;
            }

            if (txt.value.indexOf(".") > 0) {
                var txtlen = txt.value.length;
                var dotpos = txt.value.indexOf(".");
                //Change the number here to allow more decimal points than 2
                if ((txtlen - dotpos) > 2)
                    return false;
            }

            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;

            return true;
        }
    </script>--%>
    <%-- <script type="text/javascript" language="javascript">

        function allowOnlyNumber(evt) {

            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 46)
                return false;
            else {
                var len = document.getElementById("txtBasePoint").value.length;
                var index = document.getElementById("txtBasePoint").value.indexOf('.');
                alert(len);
                if (index > 0 && charCode == 46) {
                    return false;
                }
                if (index > 0) {
                    var CharAfterdot = (len + 1) - index;
                    if (CharAfterdot > 3) {
                        return false;
                    }
                }

            }

        }
    </script>--%>
    <%--  <script type="text/javascript" language="javascript">
        function isPrice(evt, value) {
            alert(value);
            var charCode = (evt.which) ? evt.which : event.keyCode;
            if ((value.indexOf('.') != -1) && (charCode != 45 && (charCode < 48 || charCode > 57)))
                return false;
            else if (charCode != 45 && (charCode != 46 || $(this).val().indexOf('.') != -1) && (charCode < 48 || charCode > 57))
                return false;
            return true;
        }
    </script>--%>
    <%--<script type="text/javascript" language="javascript">

        function allowOnlyNumber(evt, value) {
            var charCode = (evt.which) ? evt.which : event.keyCode;
            if ((value.indexOf('.') != -1) && (charCode != 45 && (charCode < 48 || charCode > 57)))
                return false;
            else if (charCode != 45 && (charCode != 46 || $(this).val().indexOf('.') != -1) && (charCode < 48 || charCode > 57))
                return false;
            return true;
        }
    </script>--%>
    <%--  <script type="text/javascript">
        function allowOnlyNumber(value) {
            var RE = "^\d*\.?\d{0,2}$";
            if (RE.test(value)) {
                return true;
            } else {
                return false;
            }
        }
       </script>--%>
    <%-- <script type="text/javascript">
         function allowOnlyNumber(evt) {
             var charCode = (evt.which) ? evt.which : event.keyCode
             var parts = evt.srcElement.value.split('.');
             alert(parts);
             alert(parts.length);
             if (parts.length > 1 && charCode == 46)
             
                 return false;

             else {
                 if (charCode == 46 || (charCode >= 48 && charCode <= 57))
                     return true;
                 return false;
             }
         }

    </script>--%>
    <%--   <script type="text/javascript">

        function allowOnlyNumber(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
            return true;
        }
    </script>--%>
    <%--  <script type="text/javascript" language="javascript">

        function isNumberKey(evt) {

            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 46)
                return false;
            else {
                var len = $('.txtBasePoint').val().length;
                var index = $('.txtBasePoint').val().indexOf('.');

                if (index > 0 && charCode == 46) {
                    return false;
                }
                if (index > 0) {
                    var CharAfterdot = (len + 1) - index;
                    if (CharAfterdot > 3) {
                        return false;
                    }
                }

            }
            return true;
        }
</script>--%>
    <center>
        <asp:Label ID="lablPanelTitle" runat="server" Text="Reverting Incentive Points " Font-Bold="true"></asp:Label></center>
    <br />
    <br />
    <asp:UpdatePanel ID="EditUpdatePanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <center>
                <asp:Panel ID="panel1" runat="server" ForeColor="Black" Font-Bold="true" Style="background-color: #E0EBAD">
                    <center>
                     
                        <table>
                            <tr>
                                <td style="width: 38px">
                                    <asp:Label ID="Label2" runat="server" Text="Type: " Font-Bold="true"></asp:Label></td>
                                <td>
                                    <asp:RadioButtonList RepeatDirection="Horizontal" runat="server" ID="radioincentive" AutoPostBack="true" OnSelectedIndexChanged="PublicationTypeChanged" Enabled="false">
                                        <asp:ListItem Value="1" Selected="True">Publication</asp:ListItem>
                                         <%-- <asp:ListItem Value="2" Selected="False" >Patent</asp:ListItem>--%>
                                    </asp:RadioButtonList></td>
                                    </td>
                            </tr>

                        </table>
                    </center>
                </asp:Panel>
            </center>
            <br />


            <asp:Panel ID="panelSearchPub" runat="server" ForeColor="Black" GroupingText="" Font-Bold="true" Style="background-color: #E0EBAD; display:none">
                <br />
                <center>
                    <table style="width: 92%">
                        <tr>
                            <td style="height: 36px; font-weight: normal; color: Black">Publication Entry Type
                            </td>
                            <td style="height: 36px">
                                <asp:DropDownList ID="EntryTypesearch" runat="server" Style="border-style: inset none none inset;"
                                    DataSourceID="SqlDataSource4" DataTextField="EntryName" DataValueField="TypeEntryId" AppendDataBoundItems="true">

                                    <%--     <asp:ListItem Value="A">ALL</asp:ListItem>--%>
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="SqlDataSource4" runat="server"
                                    ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>"
                                    SelectCommand="select TypeEntryId,EntryName from PublicationTypeEntry_M where (TypeEntryId='JA')"
                                    ProviderName="System.Data.SqlClient"></asp:SqlDataSource>

                            </td>
                            <td style="height: 36px; font-weight: normal; color: Black">Publication ID
                            </td>
                            <td style="height: 36px">
                                <asp:TextBox ID="PubIDSearch" runat="server" Style="border-style: inset none none inset;"></asp:TextBox>
                            </td>

                            <td style="height: 36px; font-weight: normal; color: Black">Title
                            </td>
                            <td style="height: 36px">
                                <asp:TextBox ID="TextBoxWorkItemSearch" runat="server" Style="border-style: inset none none inset;"></asp:TextBox>
                            </td>


                            <td style="height: 36px">
                                <asp:Button ID="ButtonSearchPub" runat="server" Text="Search" OnClick="ButtonSearchPubOnClick" />
                            </td>

                        </tr>

                    </table>
                    <br />


                    <asp:GridView ID="GridViewSearch" runat="server" AllowPaging="True" DataKeyNames="TypeOfEntry"
                        PagerSettings-PageButtonCount="5" PageSize="5" EmptyDataText="No Data found" Width="1050px"
                        HeaderStyle-BackColor="#CC6600" HeaderStyle-ForeColor="Black" AutoGenerateColumns="false" OnPageIndexChanging="GridViewSearchPub_PageIndexChanging"
                        PagerStyle-BackColor="#CC6600" PagerStyle-ForeColor="White" CellPadding="3" OnRowDataBound="GridView2_RowDataBound" OnRowEditing="edit" OnRowCommand="GridView2_RowCommand"
                        CellSpacing="3" PagerStyle-Width="4" PagerStyle-Height="4" Visible="False"
                        BorderColor="#FF6600" BorderStyle="Solid">
                        <Columns>


                            <asp:TemplateField ShowHeader="False">
                                <ItemTemplate>
                                    <asp:ImageButton ID="BtnEdit" runat="server" CausesValidation="False" CommandName="Edit" ImageUrl="~/Images/edit.gif" ToolTip="Edit" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sl.No">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="PublicationID" ReadOnly="true" HeaderText="Publication ID"
                                SortExpression="PublicationID" />

                            <asp:BoundField DataField="EntryName" ReadOnly="true" HeaderText="Type Of Entry"
                                SortExpression="EntryName" />
                            <asp:BoundField DataField="TitleWorkItem" ReadOnly="true" HeaderText="Title Of Work Item"
                                SortExpression="TitleWorkItem">
                                <ItemStyle Width="500px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="TypeOfEntry" ItemStyle-CssClass="hidden-field" HeaderStyle-CssClass="hidden-field" ReadOnly="true" HeaderText="TypeOfEntry"
                                SortExpression="TypeOfEntry" />
                            <asp:BoundField DataField="PublishJAYear" ReadOnly="true" HeaderText="Publication Year"
                                SortExpression="PublishJAYear" />
                            <asp:BoundField DataField="ISStudentAuthor" ReadOnly="true" HeaderText="Student Author"
                                SortExpression="ISStudentAuthor" />
                        </Columns>

                        <HeaderStyle BackColor="#0B532D" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#0B532D" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#FFFFFF" ForeColor="#333333" />
                        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                        <SortedAscendingCellStyle BackColor="#FDF5AC" />
                        <SortedAscendingHeaderStyle BackColor="#4D0000" />
                        <SortedDescendingCellStyle BackColor="#FCF6C0" />
                        <SortedDescendingHeaderStyle BackColor="#820000" />


                    </asp:GridView>
                    <br />
                </center>
                <br />
            </asp:Panel>
            <br />
            <asp:Panel runat="server" ID="PnlPatent">
                <asp:Panel ID="PanelPatentSearch" runat="server" BorderStyle="None" Font-Bold="true" Style="background-color: #E0EBAD; display: none">
                    <table style="margin-left: 200px">
                        <tr>
                            <td style="height: 36px; font-weight: normal; color: Black">Patent ID
                            </td>
                            <td style="height: 36px">
                                <asp:TextBox ID="PatIDSearch" runat="server" Style="border-style: inset none none inset;"></asp:TextBox>
                            </td>

                            <td style="height: 36px; font-weight: normal; color: Black">Title
                            </td>

                            <td>
                                <asp:TextBox ID="TextBoxtiltleSearch" runat="server" Style="border-style: inset none none inset;" Width="340px"></asp:TextBox>
                            </td>
                            <td style="height: 36px">
                                <asp:Button ID="ButtonSearchProject" runat="server" Text="Search"
                                    OnClick="ButtonSearchProjectOnClick" Style="height: 26px" />
                            </td>

                        </tr>
                    </table>
                    <div style="margin-left: 230px">


                        <asp:GridView ID="GridViewSearchPatent" runat="server" AllowPaging="True"
                            PagerSettings-PageButtonCount="5" PageSize="5" DataSourceID="SqlDataSource1"
                            HeaderStyle-BackColor="#CC6600" HeaderStyle-ForeColor="Black" AutoGenerateColumns="False" OnPageIndexChanging="GridViewSearchPatent_PageIndexChanging"
                            PagerStyle-BackColor="#CC6600" PagerStyle-ForeColor="White" CellPadding="3" OnRowDataBound="GridViewSearchPatent_RowDataBound" OnRowEditing="GridViewSearchPatent_OnRowedit" OnRowCommand="GridViewSearchPatent_RowCommand"
                            CellSpacing="3" PagerStyle-Width="4" PagerStyle-Height="4" Visible="False"
                            BorderColor="#FF6600" BorderStyle="Solid" Width="586px" DataKeyNames="ID">
                            <Columns>
                                <asp:TemplateField ShowHeader="False">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="BtnEdit" runat="server" CausesValidation="False" CommandName="Edit" ImageUrl="~/Images/edit.gif" ToolTip="Edit" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID" ReadOnly="True" />
                                <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title" />
                                <asp:BoundField DataField="Filling_Status" HeaderText="Filling_Status"
                                    SortExpression="Filling_Status" />
                                <asp:BoundField DataField="FilingOffice" HeaderText="FilingOffice" SortExpression="FilingOffice" />
                                  <asp:BoundField DataField="Grant_Date" HeaderText="Grant_Date" SortExpression="Grant_Date" DataFormatString="{0:d}" />


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
                        SelectCommand=""></asp:SqlDataSource>
                </asp:Panel>
            </asp:Panel>


            <asp:Panel ID="PnlPublicationDetails" runat="server" GroupingText="Publication Details" Enabled="false" Style="background-color: #E0EBAD; display: none">
                <table style="width: 92%">
                    <tr>
                        <td style="height: 36px; font-weight: normal">
                            <asp:Label ID="TypeEntry" runat="server" Text="Entry Type" ForeColor="Black"></asp:Label>

                        </td>

                        <td style="height: 36px">
                            <asp:DropDownList ID="DropDownListPublicationEntry" runat="server"
                                AppendDataBoundItems="true" AutoPostBack="true"
                                DataSourceID="SqlDataSourcePublicationEntry" DataTextField="EntryName"
                                DataValueField="TypeEntryId"
                                Style="border-style: inset none none inset;" Width="200px">
                                <asp:ListItem Value=" ">--Select--</asp:ListItem>
                            </asp:DropDownList>
                            <asp:SqlDataSource ID="SqlDataSourcePublicationEntry" runat="server"
                                ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>"
                                ProviderName="System.Data.SqlClient"
                                SelectCommand="select TypeEntryId,EntryName from PublicationTypeEntry_M"></asp:SqlDataSource>

                        </td>
                        <td style="height: 36px; font-weight: normal;">
                            <asp:Label ID="lblMUCat" runat="server" Text="MAHE Categorization" ForeColor="Black"></asp:Label>

                        </td>
                        <td style="height: 36px">
                            <asp:DropDownList ID="DropDownListMuCategory" runat="server" AutoPostBack="true"
                                AppendDataBoundItems="true" DataSourceID="SqlDataSourceMuCategory"
                                DataTextField="PubCatName" DataValueField="PubCatId" Style="border-style: inset none none inset;"
                                Width="200px">
                                <asp:ListItem Value=" ">--Select--</asp:ListItem>
                            </asp:DropDownList>
                            <asp:SqlDataSource ID="SqlDataSourceMuCategory" runat="server"
                                ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>"
                                ProviderName="System.Data.SqlClient"
                                SelectCommand="select PubCatId,PubCatName from PubMUCategorization_M"></asp:SqlDataSource>

                        </td>
                        <td style="font-weight: normal">
                            <asp:Label ID="LabelPubId" runat="server" Text="PublicationId" ForeColor="Black"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TextBoxPubId" runat="server" Enabled="false"
                                Style="border-style: inset none none inset;" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 36px; font-weight: normal">
                            <asp:Label ID="lblTitileWorkItem" runat="server" Text="Title Of the Work Item" ForeColor="Black"></asp:Label>
                        </td>
                        <td colspan="6" style="height: 36px">
                            <asp:TextBox ID="txtboxTitleOfWrkItem" runat="server" MaxLength="200"
                                Style="border-style: inset none none inset;" TextMode="MultiLine" Width="850px"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <br />
            <asp:Panel ID="panelJournalArticle" runat="server" GroupingText="Journal Publish Details" Enabled="false" Visible="false" ForeColor="Black" Style="background-color: #E0EBAD">
                <br />
                <center><asp:Label ID="lblNote" runat="server" Text="" ForeColor="Maroon" Font-Bold="true" Visible="false"></asp:Label></center>
                 <center><asp:Label ID="lblnoteQuartile" runat="server" Text="" ForeColor="Maroon" Font-Bold="true" Visible="false"></asp:Label>
                     <%--<asp:LinkButton ID="LinkButton1" runat="server" Visible="false" Text="in Manage Quartile" OnClick="LinkButton1_Click"></asp:LinkButton>--%>
                    
                 </center>
                 <center><asp:Label ID="Label5" runat="server" Text="" ForeColor="Maroon" Font-Bold="true" Visible="false"></asp:Label></center>
                  <table>
                <tr>
                    <td style="height: 36px; font-weight: normal">
                        <asp:Label ID="Label4" runat="server" Text="Upload To E-Print" ForeColor="Black" ></asp:Label>
                    </td>
                    <td>
                        <asp:RadioButtonList runat="server" ID="uploadeprint" RepeatDirection="Horizontal">
                            <asp:ListItem Value="Y">Yes</asp:ListItem>
                            <asp:ListItem Value="N">No</asp:ListItem>
                        </asp:RadioButtonList></td><td>&nbsp;&nbsp;</td>
              <%--      <td style="font-weight: bold" colspan="2">
                        <asp:Label ID="lblNote" runat="server" Text="" ForeColor="Maroon" Visible="false"></asp:Label></td>--%>
                </tr>
                </table>
                <table>
                    <tr>
                        <td style="height: 36px; font-weight: normal">
                            <asp:Label ID="LabelPubJournal" runat="server" Text="ISSN" ForeColor="Black"></asp:Label>
                        </td>
                        <td style="height: 36px">
                            <asp:TextBox ID="TextBoxPubJournal" runat="server" Width="190px" Style="border-style: inset none none inset;"></asp:TextBox>
                        </td>
                        <td style="height: 36px; font-weight: normal">
                            <asp:Label ID="LabelNameJournal" runat="server" Text="Name Of Journal" ForeColor="Black"></asp:Label>
                        </td>
                        <td style="height: 36px">
                            <asp:TextBox ID="TextBoxNameJournal" runat="server" Enabled="false" Width="400px" Style="border-style: inset none none inset;"></asp:TextBox>
                        </td>
                        <td style="height: 56px; font-weight: normal">
                            <asp:Label ID="Label1" runat="server" Text="SNIP" ForeColor="Black"></asp:Label></td>
                        <td>
                            <asp:TextBox ID="txtSNIP" runat="server" ReadOnly="true" Style="border-style: inset none none inset;" Width="100px"></asp:TextBox>
                        </td>
                        <td style="height: 56px; font-weight: normal">
                            <asp:Label ID="Label3" runat="server" Text="SJR" ForeColor="Black"></asp:Label></td>
                        <td>
                            <asp:TextBox ID="txtsjr" runat="server" ReadOnly="true" Style="border-style: inset none none inset;" Width="100px"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <table>
                    <tr>
                        <td style="height: 56px; font-weight: normal">
                            <asp:Label ID="LabelMonthJA" runat="server" Text="Publish Month" ForeColor="Black"></asp:Label></td>
                        <td>
                            <asp:DropDownList ID="DropDownListMonthJA" runat="server" Style="border-style: inset none none inset;" DataSourceID="SqlDataSourcePubJAmonth"
                                DataTextField="MonthName" DataValueField="MonthValue" Width="50px">
                            </asp:DropDownList>
                            <asp:SqlDataSource ID="SqlDataSourcePubJAmonth" runat="server" ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>"
                                SelectCommand="select MonthValue, MonthName from Publication_MonthM"></asp:SqlDataSource>
                        </td>
                        <td style="height: 56px; font-weight: normal">
                            <asp:Label ID="LabelYearJA" runat="server" Text="Publish Year" ForeColor="Black"></asp:Label></td>
                        <td>
                            <asp:DropDownList ID="TextBoxYearJA" runat="server"
                                Width="60px" Style="border-style: inset none none inset;">
                            </asp:DropDownList>
                        </td>

                        <td style="height: 56px; font-weight: normal">
                            <asp:Label ID="LabelImpFact" runat="server" Text="1-Year Impact Factor" ForeColor="Black"></asp:Label></td>
                        <td>
                            <asp:TextBox ID="TextBoxImpFact" runat="server" ReadOnly="true" Style="border-style: inset none none inset;" Width="100px"></asp:TextBox>
                        </td>

                        <td style="height: 56px; font-weight: normal">
                            <asp:Label ID="LabelImpFact5" runat="server" Text="5-Year Impact Factor" ForeColor="Black"></asp:Label></td>
                        <td>
                            <asp:TextBox ID="TextBoxImpFact5" runat="server" ReadOnly="true" Style="border-style: inset none none inset;" Width="100px"></asp:TextBox>

                        </td>
                        <td style="height: 56px; font-weight: normal">
                            <asp:Label ID="LblIFAY" runat="server" Text="IF-ApplicableYear" ForeColor="Black"></asp:Label></td>
                        <td>
                            <asp:TextBox ID="txtIFApplicableYear" runat="server" ReadOnly="true" Style="border-style: inset none none inset;" Width="100px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                             <asp:Label ID="lblQuartile" runat="server" Text="Quartile" ForeColor="Black" ></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtquartile" runat="server" ReadOnly="true" Width="100px"  ></asp:TextBox>
                        </td>
                        <td>
                             <asp:Label ID="lblQuartileid" runat="server" Text="QuartileID" ForeColor="Black" Visible="false"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtquartileid" runat="server" ReadOnly="true" Width="100px" Visible="false" ></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <table>
                </table>
            </asp:Panel>

            <asp:Panel ID="PnlPatentDetails" runat="server" GroupingText="Patent Details" ForeColor="Black" Style="font-weight: normal; border-style: none; background-color: #E0EBAD; display: none">
                <table style="width:100%" >
                    <tr>
                        <td class="auto-style1">ID </td>
                        <td class="auto-style1">
                            <asp:TextBox ID="txtID" runat="server" Enabled="false"></asp:TextBox>
                        </td>
                        <td class="auto-style1" style="display:none">UTN
                        </td>
                        <td class="auto-style1">
                            <asp:TextBox ID="txtPatUTN" runat="server" Visible="false"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style20">Title </td>
                        <td colspan="3" class="auto-style20">
                            <asp:TextBox ID="txtTitle" runat="server" Width="514px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style21">Description
                        </td>
                        <td colspan="3" class="auto-style21">
                            <asp:TextBox ID="txtde" runat="server" Width="514px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                       <td class="auto-style1">Grant Date </td>
                        <td class="auto-style1">
                            <asp:TextBox ID="txtgrantdate" runat="server" Enabled="false"></asp:TextBox>
                        </td>
                          <td class="auto-style1">Filing Office </td>
                        <td class="auto-style1">
                            <asp:TextBox ID="txtfilingoffice" runat="server" Enabled="false"></asp:TextBox>
                        </td>
                        <td class="auto-style1">Patent Number</td>
                        <td class="auto-style1">
                            <asp:TextBox ID="txtPatentno" runat="server" Enabled="false"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </asp:Panel>

            <br />
            <asp:Panel ID="panAddAuthor" runat="server" GroupingText="Author Details" Visible="false" ForeColor="Black" Style="background-color: #E0EBAD">

                <br />

                <asp:Panel ID="PanelMU" runat="server" ScrollBars="Both" Width="1080px" Style="margin-right: 0px">
                    <center>
                    <asp:GridView ID="Grid_AuthorEntry" runat="server" AutoGenerateColumns="False" GridLines="None"
                        CellPadding="4" ForeColor="#333333">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>

                            <asp:TemplateField HeaderText="Type">
                                <ItemStyle Width="5px" />
                                <HeaderStyle Width="5px" />
                                <ItemTemplate>
                                    <asp:DropDownList ID="DropdownMuNonMu" runat="server" Width="100" Enabled="false">
                                        <asp:ListItem Value="M">MAHE-Faculty</asp:ListItem>
                                        <asp:ListItem Value="S">MAHE-Student</asp:ListItem>
                                        <asp:ListItem Value="O">MAHE-Student</asp:ListItem>
                                        <asp:ListItem Value="N">Non MAHE</asp:ListItem>
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ShowHeader="true" HeaderText="Roll No/Employee Code">

                                <ItemTemplate>
                         <%--           <asp:HiddenField ID="EmployeeCode" runat="server"></asp:HiddenField>--%>
                                     <asp:TextBox ID="EmployeeCode" runat="server" Enabled="false"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Author Name" ItemStyle-Width="20px" HeaderStyle-Height="20px">
                                <ItemTemplate>
                                    <asp:TextBox ID="AuthorName" runat="server" Enabled="false"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Institution">
                                <ItemTemplate>
                                    <asp:TextBox ID="InstitutionName" runat="server" Width="150" Enabled="false"></asp:TextBox>
                                    <asp:DropDownList ID="DropdownStudentInstitutionName" runat="server" Width="150" Visible="false" AutoPostBack="true" DataSourceID="SqlDataSourceDropdownStudentInstitutionName" DataTextField="Institute_Name" DataValueField="Institute_Id">
                                    </asp:DropDownList>
                                    <asp:SqlDataSource ID="SqlDataSourceDropdownStudentInstitutionName" runat="server"
                                        ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>"
                                        ProviderName="System.Data.SqlClient"
                                        SelectCommand="select Institute_Id,Institute_Name from Institute_M"></asp:SqlDataSource>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Department/Course">
                                <ItemTemplate>
                                    <asp:TextBox ID="DepartmentName" runat="server" Width="150" Enabled="false"></asp:TextBox>
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
                                </ItemTemplate>
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="CorrAuth" HeaderStyle-Width="50px">
                                <ItemTemplate>
                                    <asp:DropDownList ID="isCorrAuth" runat="server" Width="50" Enabled="false">
                                        <asp:ListItem Value="Y">Yes</asp:ListItem>
                                        <asp:ListItem Value="N" Selected="True">No</asp:ListItem>
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="AuthorType">
                                <ItemTemplate>
                                    <asp:DropDownList ID="AuthorType" runat="server" Width="100" Enabled="false">
                                        <asp:ListItem Value="P">First Author</asp:ListItem>
                                        <asp:ListItem Value="C" Selected="True">CO-Author</asp:ListItem>
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>

                           
                          <%--  <asp:TemplateField HeaderText="Base Point">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtBasePoint" runat="server" Width="80" onChange="onchangebasepointTotal(this,event)"></asp:TextBox>
                                    <asp:RegularExpressionValidator Display="None" ID="regexp1" runat="server"
                                        ControlToValidate="txtBasePoint" ValidationGroup="validation" ErrorMessage="Base Point-Only two didgits are allowed after decimal"
                                        ValidationExpression="\d+(?:(?:\.|,)\d{1,2})?" />
                                </ItemTemplate>

                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="SNIP/SJR Point" >
                                <ItemTemplate>
                                    <asp:TextBox ID="txtSNIPSJRPoint" runat="server" Width="100" Enabled="true" onChange="onchangebasepoint(this)"></asp:TextBox>
                                    <asp:RegularExpressionValidator Display="None" ID="regexp2" runat="server"
                                        ControlToValidate="txtSNIPSJRPoint" ValidationGroup="validation" ErrorMessage="SNIP/SJR Point-Only two didgits are allowed after decimal"
                                        ValidationExpression="\d+(?:(?:\.|,)\d{1,2})?" />
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField HeaderText="Total Point" >
                                <ItemTemplate>
                                    <asp:TextBox ID="txtTotalPoint" runat="server" Width="80" Enabled="false"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ControlToValidate="txtTotalPoint" Display="None"
                                        ErrorMessage="Please enter total point" ValidationGroup="validation"></asp:RequiredFieldValidator>
                                </ItemTemplate>--%>

                          <%--  </asp:TemplateField>--%>
                           <%-- <asp:TemplateField HeaderText=" Threshold Point ">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtThresholdPoint" runat="server" Width="50" Enabled="true" onChange="onchangebasepoint(this)"></asp:TextBox>
                                    <asp:RegularExpressionValidator Display="None" ID="regexp3" runat="server"
                                        ControlToValidate="txtThresholdPoint" ValidationGroup="validation" ErrorMessage="ThresholdPoint-Only two didgits are allowed after decimal"
                                        ValidationExpression="\d+(?:(?:\.|,)\d{1,2})?" />
                                </ItemTemplate>
                            </asp:TemplateField>--%>

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
                    <br />
       
     
                    <center>
                    <asp:GridView ID="GridViewIncentivePints" runat="server" AutoGenerateColumns="False" GridLines="None"
                        CellPadding="4" ForeColor="#333333">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>

                            <asp:TemplateField HeaderText="Type" Visible="false">
                                <ItemStyle Width="5px" />
                                <HeaderStyle Width="5px" />
                                <ItemTemplate>
                                    <asp:DropDownList ID="DropdownMuNonMu" runat="server" Width="100" Enabled="false" Visible="false">
                                        <asp:ListItem Value="M">MAHE-Staff</asp:ListItem>
                                        <asp:ListItem Value="S">MAHE-Student</asp:ListItem>
                                        <asp:ListItem Value="O">MAHE-Student</asp:ListItem>
                                        <asp:ListItem Value="N">Non MAHE</asp:ListItem>
                                    </asp:DropDownList>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ShowHeader="true" HeaderText="Roll No/Employee Code">

                                <ItemTemplate>
                         <%--           <asp:HiddenField ID="EmployeeCode" runat="server"></asp:HiddenField>--%>
                                     <asp:TextBox ID="EmployeeCode" runat="server" Enabled="false"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Author Name" ItemStyle-Width="20px" HeaderStyle-Height="20px">
                                <ItemTemplate>
                                    <asp:TextBox ID="AuthorName" runat="server" Enabled="false"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                     
                          <asp:TemplateField HeaderText="Old CurrentBalance">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtOldCurrentBalance" runat="server"  Enabled="false" Width="80px"></asp:TextBox>
                                   <%-- <asp:RegularExpressionValidator Display="None" ID="regexp1" runat="server"
                                        ControlToValidate="txtOldCurrentBalance" ValidationGroup="validation" ErrorMessage="Base Point-Only two didgits are allowed after decimal"
                                        ValidationExpression="\d+(?:(?:\.|,)\d{1,2})?" />--%>
                                </ItemTemplate>

                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Reverting Points" >
                                <ItemTemplate>
                                    <asp:TextBox ID="txtRevertingPoint" runat="server"  Enabled="false"  Width="80px" ></asp:TextBox>
                                    <%--<asp:RegularExpressionValidator Display="None" ID="regexp2" runat="server"
                                        ControlToValidate="txtRevertingPoint" ValidationGroup="validation" ErrorMessage="SNIP/SJR Point-Only two didgits are allowed after decimal"
                                        ValidationExpression="\d+(?:(?:\.|,)\d{1,2})?" />--%>
                                </ItemTemplate>
                            </asp:TemplateField>

                            
                             <asp:TemplateField HeaderText="AdditionalPoint" >
                                <ItemTemplate>
                                    <asp:TextBox ID="txtAdditionalPoint" runat="server"  Enabled="false" ></asp:TextBox>
                                </ItemTemplate>                          
                           </asp:TemplateField>  

                                  <asp:TemplateField HeaderText="UpdatedAdditionalPoint" >
                                <ItemTemplate>
                                    <asp:TextBox ID="txtUpdatedAdditionalPoint" runat="server"  Enabled="false" ></asp:TextBox>
                                </ItemTemplate>
                                       </asp:TemplateField>  

                             <asp:TemplateField HeaderText="NewCurrentBalance" >
                                <ItemTemplate>
                                    <asp:TextBox ID="txtNewcurrentBalance" runat="server"  Enabled="false" Width="80px" ></asp:TextBox>
                                </ItemTemplate>

                           </asp:TemplateField>              

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
                    <br />
                </asp:Panel>
            </asp:Panel>
            <br />
              <asp:Panel ID="Panel2" runat="server" GroupingText="CancelRemarks" Visible="false" ForeColor="Black" Style="background-color: #E0EBAD" >
                  <table>
                      <tr>
                          <td>
                              <asp:Label ID="Label" runat="server" Text="Remarks"></asp:Label>
                          </td>
                          <td>
                               <asp:TextBox ID="txtcancelRemarks" runat="server" Width="600px" TextMode="MultiLine"></asp:TextBox>
                          </td>
                      </tr>
                  </table>
                  
                 
                   </asp:Panel>
            <br />
            <table width="100%">
                <tr>
                    <td align="center">
                        <asp:Button ID="btnSave" runat="server" Visible="false" Text="Save" OnClick="BtnSave_Click" Enabled="true" CausesValidation="true" ValidationGroup="validation"></asp:Button>
                        <%--<asp:Button ID="btnApprove" runat="server" Visible="false" Text="Approve" OnClick="BtnApprove_Click" Enabled="true" CausesValidation="true" ValidationGroup="validation"></asp:Button>--%>
                        <%--<asp:Button ID="btnDiscard" runat="server" Visible="false" Text="Discard" Enabled="true" OnClick="btnDiscard_Click" OnClientClick="Confirm()"></asp:Button>--%>

                    </td>
                </tr>
            </table>
            <br />

        </ContentTemplate>
       
    </asp:UpdatePanel>
    <%--    <asp:RegularExpressionValidator ID="Regex1" runat="server" ValidationExpression="((\d+)((\.\d{1,2})?))$"
ErrorMessage="Please enter valid integer or decimal number with 2 decimal places."
ControlToValidate="TextBox1" />--%>
    <asp:HiddenField ID="hdn" runat="server" />




</asp:Content>

