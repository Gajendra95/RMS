<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPageRMS.master" AutoEventWireup="true" CodeFile="IncentivePointAdjustment.aspx.cs" Inherits="Incentive_IncentivePointAdjustment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

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

    <script type="text/javascript">
        function setRow(obj) {
            var sndID = obj.id;
            var sndrID = document.getElementById('<%= HiddenField1.ClientID %>');
            sndrID.value = sndID;
        }
    </script>
     <script language="javascript" type="text/javascript">
        
         function CheckIsRepeat() {
             var submit = 0;
             if (++submit > 1) {
                 alert(submit);
                 alert('An attempt was made to submit this form more than once; this extra attempt will be ignored.');
                 return false;
             }
         }
    </script>
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

        .modelPopup {
            background-color: #EEEEE;
            border-width: 3px;
            border-style: solid;
            border-color: Gray;
            font-family: Verdana;
            font-size: small;
            padding: 3px;
            width: 450px;
            position: absolute;
            overflow: scroll;
            max-height: 400px;
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
    <script type="text/javascript">
        function setRow(obj) {
            var row = obj.parentNode.parentNode;
            var rowIndex = row.rowIndex - 1;
            var sndID = obj.id;
            var sndrID = document.getElementById('<%= HiddenField1.ClientID %>');
            sndrID.value = sndID;
            var rowNo = document.getElementById('<%= hdn.ClientID %>');
            rowNo.value = rowIndex;
        }
    </script>
    <asp:ToolkitScriptManager ID="Scriptmanager1" runat="server" />
    <asp:ValidationSummary ID="ValidationSummary3" runat="server" ShowMessageBox="True" DisplayMode="BulletList"
        HeaderText="Validation issues" ShowSummary="False" ValidationGroup="validation2" />

    <asp:UpdatePanel ID="EditUpdatePanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Panel ID="Panel2" runat="server" Width="980px" Font-Bold="true" Style="background-color: #E0EBAD" ForeColor="Black">
                <br />

                <center>
                    <asp:Label ID="lablPanelTitle" runat="server" Text="Incentive Point Adjustment" Font-Bold="true"></asp:Label></center>
                <br />
                <br />
                <center>
                    <table>
                        <tr>
                            <td style="height: 38px">Member Id
                            </td>
                            <td style="height: 38px">
                                <asp:TextBox ID="txtboxMemberId" ReadOnly="true" runat="server" AutoPostBack="true" Width="160px"></asp:TextBox>


                                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/srchImg.gif" OnClick="showPop" OnClientClick="setRow(this)" Width="22px" /></td>
                            <asp:HiddenField runat="server" ID="hdn" />
                            <asp:ModalPopupExtender ID="ModalPopupExtender2" runat="server" TargetControlID="ImageButton1" PopupControlID="Panel1"
                                BackgroundCssClass="modelBackground">
                            </asp:ModalPopupExtender>
                        </tr>
                        <tr>
                            <td style="height: 38px">Current Balance
                            </td>
                            <td style="height: 38px">
                                <asp:TextBox ID="txtcurbal" runat="server" ReadOnly="true" Width="160px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 38px">Adjustment
                            </td>
                            <td style="height: 38px">
                                <asp:TextBox ID="txtadjustment" runat="server" Width="160px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 38px">Remarks
                            </td>
                            <td>
                                <asp:TextBox ID="txtRemarks" runat="server" Width="532px" onkeypress="return this.value.length<500"  TextMode="MultiLine" Rows="3"></asp:TextBox></td>
                        </tr>
                    </table>
                </center>
                <br />
                <div style="margin-left: 400px; padding-left: 53px;">
                    <asp:Button ID="btnUpdate" runat="server" Text="Save" OnClick="btnUpdate_Click" Width="120px" OnClientClick="return CheckIsRepeat();" CausesValidation="true" ValidationGroup="validation2" />
                </div>
                <br />

            </asp:Panel>

            <asp:Panel ID="Panel1" runat="server" Visible="false" CssClass="modelPopup" Style="width: 1000px; height: 400px; background-color: ghostwhite;"
                BorderStyle="Groove" BorderColor="Black" BackColor="White">
                <center>
                    <table style="background: white">
                        <tr>
                            <td align="center"><b> Employee Code : </b>
                                <asp:TextBox ID="txtmidSearch" runat="server"></asp:TextBox>
                                <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" Width="81px" />
                                <asp:Button ID="btnexit" runat="server" Text="Exit" Width="81px" />
                            </td>
                        </tr>
                    </table>
                </center>
                <br />
                <center>
                    <table>
                        <tr>
                            <td colspan="3">
                                <asp:GridView ID="popGridSearch" runat="server" AutoGenerateSelectButton="True" EmptyDataText="No Data Found"
                                    OnPageIndexChanging="popGridIncenAdjust_PageIndexChanging" OnSelectedIndexChanged="popSelected" AllowSorting="True" AutoGenerateColumns="False" DataKeyNames="MemberId" PagerSettings-PageButtonCount="10" CellPadding="4" ForeColor="#333333"
                                    DataSourceID="SqlDataSourceTextboxSearch" Visible="False" PageSize="18" Width="500px">
                                    <AlternatingRowStyle BackColor="White" />
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
                                        <asp:BoundField DataField="MemberId" HeaderText="Employee Code" ReadOnly="True" SortExpression="MemberId" />
                                        <asp:BoundField DataField="MemberName" HeaderText="Name" ReadOnly="True" SortExpression="MemberId" />
                                    </Columns>
                                    <EditRowStyle BorderColor="#FF6600" BorderStyle="Solid" />
                                </asp:GridView>
                                <asp:SqlDataSource ID="SqlDataSourceMember" runat="server" ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" SelectCommand="  SELECT distinct top 10  MemberId,AuthorName as MemberName from Member_Incentive_Point_Summary , Publishcation_Author where Publishcation_Author.EmployeeCode=Member_Incentive_Point_Summary.MemberId and MemberType!='N'" SelectCommandType="Text"></asp:SqlDataSource>
                            </td>
                        </tr>
                    </table>
                </center>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ControlToValidate="txtRemarks" Display="None"
        ErrorMessage="Please enter the remarks" ValidationGroup="validation2"></asp:RequiredFieldValidator>
    <asp:RegularExpressionValidator
        ID="RegularExpressionValidator6"
        runat="server" Display="None" ValidationGroup="validation"
        ControlToValidate="txtadjustment"
        ValidationExpression="([0-9])[0-9]*[.]?[0-9]*"
        ErrorMessage="Invalid Entry">
    </asp:RegularExpressionValidator>

    
      <asp:RegularExpressionValidator
        ID="RegularExpressionValidator1"
        runat="server" Display="None" ValidationGroup="validation"
        ControlToValidate="txtadjustment"
        ValidationExpression="^\d+\.?\d{0,2}$"
        ErrorMessage="Invalid Entry1">
    </asp:RegularExpressionValidator>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtadjustment" Display="None"
        ErrorMessage="Please enter adjustment point" ValidationGroup="validation2"></asp:RequiredFieldValidator>
    <asp:HiddenField ID="HiddenField1" runat="server" />
</asp:Content>

