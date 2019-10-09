<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPageRMS.master" AutoEventWireup="true" CodeFile="AdditionalPointAward.aspx.cs" Inherits="Incentive_AdditionalPointAward" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../Scripts/jquery-1.7.1.min.js" type="text/javascript"></script>
    <script src="../JavaScript/JavaScript.js" type="text/javascript"></script>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" DisplayMode="List"
        HeaderText="Validation issues" ShowSummary="False" ValidationGroup="validation" />
    <script type="text/javascript">
        window.onbeforeunload = function () {
            $("input[type=button]").attr("disabled", "disabled");
        };
    </script>
    <style type="text/css">
        .hidden-field {
            display: none;
        }
    </style>

    <style type="text/css">
        .modelBackground {
            background-color: Gray;
            filter: alpha(opacity=70);
            opacity: 0.7;
        }

        .modelPopup {
            background-color: #EEEEEE;
            border-width: 3px;
            border-style: solid;
            border-color: Gray;
            font-family: Verdana;
            font-size: medium;
            padding: 3px;
            width: 550px;
            position: absolute;
            overflow: scroll;
            max-height: 400px;
        }
    </style>

    <%-- <script type="text/javascript">
        function setRow(obj) {
            var sndID = obj.id;
            var sndrID = document.getElementById('<%= HiddenField1.ClientID %>');
            sndrID.value = sndID;
        }
    </script>--%>

    <style type="text/css">
        .gridViewHeader {
            padding: 40px 50px 4px 4px;
            border-collapse: collapse;
        }

        .modelBackground {
            background-color: Gray;
            filter: alpha(opacity=70);
            opacity: 0.7;
        }



        .blnkImgCSS {
            opacity: 0;
            filter: alpha(opacity=0);
        }

        .auto-style7 {
            width: 140px;
        }

        .auto-style8 {
            width: 152px;
        }
    </style>

    <script type="text/javascript" language="javascript">

        function allowOnlyNumber(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 46)

                return false;
            else {
                var len = $('.txtPointsAwarded').val().length;
                var index = $('.txtPointsAwarded').val().indexOf('.');

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
    </script>

    <asp:ToolkitScriptManager ID="Scriptmanager1" runat="server" />
    <asp:UpdatePanel ID="EditUpdatePanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Panel ID="Panel2" runat="server" Width="1000px" Font-Bold="true" Style="background-color: #E0EBAD" ForeColor="Black">
                <br />

                <center>
                    <asp:Label ID="lablPanelTitle" runat="server" Text="Additional Points Award" Font-Bold="true"></asp:Label></center>
                <br />
                <br />
                <center>
                    <table>
                        <tr>
                            <td style="height: 38px">Member Id
                            </td>
                            <td style="height: 38px">
                                <asp:TextBox ID="txtMemberId" runat="server" AutoPostBack="true" Width="160px" OnTextChanged="txtMemberId_TextChanged"></asp:TextBox></td>

                        </tr>
                        <tr>
                            <td style="height: 38px">Year
                                <td>
                                    <asp:DropDownList ID="DdlYear" runat="server" OnSelectedIndexChanged="DdlYear_SelectedIndexChanged1"
                                        Width="160px" Style="border-style: inset none none inset;" AutoPostBack="true">
                                        <asp:ListItem Value="0">Select</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                        </tr>
                         <tr>
                            <td style="height: 38px">Publication Count
                            </td>
                            <td>

                                <asp:TextBox ID="txtPubcount" runat="server" Enabled="false" Width="160px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 38px">Year Balance
                            </td>
                            <td style="height: 38px">
                                <asp:TextBox ID="txtcurbal" runat="server" ReadOnly="true" Width="160px"></asp:TextBox>
                            </td>
                        </tr>

                        <tr>
                            <td style="height: 38px">Points Awarded
                            </td>
                            <td style="height: 38px">
                                <asp:TextBox ID="txtPointsAwarded" runat="server" Width="160px" onkeypress="return allowOnlyNumber(event)" Enabled="false" ValidationGroup="validation1"></asp:TextBox>
                            </td>
                        </tr>
                       
                        <tr>
                            <td style="height: 38px">Remarks
                            </td>
                            <td>
                                <asp:TextBox ID="txtRemarks" runat="server" Width="532px" TextMode="MultiLine" onkeypress="return this.value.length<500" Rows="3"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" Width="119px" CausesValidation="true" ValidationGroup="validation" />
                                <asp:Button ID="btnClear" runat="server" Text="Clear" OnClick="btnClear_Click" Width="121px" CausesValidation="true" /></td>
                        </tr>
                    </table>

                </center>
                <br />
                <center>
                    <table>
                        <tr>
                            <td></td>
                            <td>
                                <asp:Label ID="lblNote" runat="server" Text="" ForeColor="Black" Font-Bold="true" Visible="false"></asp:Label>
                                <asp:Label ID="lblNote1" runat="server" Text="" ForeColor="Black" Font-Bold="true" Visible="false"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </center>
                <br />
            </asp:Panel>

        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtPointsAwarded" Display="None"
        ErrorMessage="Please enter the points" ValidationGroup="validation"></asp:RequiredFieldValidator>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ControlToValidate="txtRemarks" Display="None"
        ErrorMessage="Please enter the remarks" ValidationGroup="validation"></asp:RequiredFieldValidator>
    <asp:RegularExpressionValidator
        ID="RegularExpressionValidator6"
        runat="server" Display="None" ValidationGroup="validation"
        ControlToValidate="txtPointsAwarded"
        ValidationExpression="([0-9])[0-9]*[.]?[0-9]*"
        ErrorMessage="Invalid Entry"> </asp:RegularExpressionValidator>

    <asp:RegularExpressionValidator
        ID="RegularExpressionValidator1"
        runat="server" Display="None" ValidationGroup="validation"
        ControlToValidate="txtPointsAwarded"
        ValidationExpression="^\d+\.?\d{0,2}$"
        ErrorMessage="Only two didgits are allowed after decimal"> </asp:RegularExpressionValidator>

    <asp:RequiredFieldValidator InitialValue="0" ID="Req_ID" Display="None"
        ValidationGroup="validation" runat="server" ControlToValidate="DdlYear"
        ErrorMessage="Please seleyct year"></asp:RequiredFieldValidator>
</asp:Content>


