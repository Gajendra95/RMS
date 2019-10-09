<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPageRMS.master" AutoEventWireup="true" CodeFile="IncentivePointUtilization.aspx.cs" Inherits="Incentive_IncentivePointUtilization" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../Scripts/jquery-1.7.1.min.js" type="text/javascript"></script>
    <script src="../JavaScript/JavaScript.js" type="text/javascript"></script>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" DisplayMode="BulletList"
        HeaderText="Validation issues" ShowSummary="False" ValidationGroup="validation" />

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

        .auto-style9 {
            height: 38px;
            width: 124px;
        }

        .auto-style10 {
            width: 124px;
        }
        .hiddencol 
        { display: none; }
    </style>
    <%--   <script type="text/javascript">

        function allowOnlyNumber(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
            return true;
        }
    </script>--%>
    <script type="text/javascript" language="javascript">

        function allowOnlyNumber(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 46)

                return false;
            else {
                var len = $('.txtUtilization').val().length;
                var index = $('.txtUtilization').val().indexOf('.');

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
    <%--<script type="text/javascript">
         alert("dfjd");
         function allowOnlyNumber(evt) {
             var charCode = (evt.which) ? evt.which : event.keyCode
             alert(charCode);
             var parts = evt.srcElement.value.split('.');
             if (parts.length > 1 && charCode == 46)
             
                 return false;

             else {
                 if (charCode == 46 || (charCode >= 48 && charCode <= 57))
                     return true;
                 return false;
             }
         }

    </script>--%>

    <asp:ToolkitScriptManager ID="Scriptmanager1" runat="server" />
    <asp:UpdatePanel ID="EditUpdatePanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Panel ID="Panel2" runat="server" Width="1000px" Font-Bold="true" Style="background-color: #E0EBAD" ForeColor="Black">
                <br />
                <center>
                    <asp:Label ID="lablPanelTitle" runat="server" Text="Incentive Point Utilization" Font-Bold="true"></asp:Label></center>
                <center>
                    <table>
                        <tr>
                            <td class="auto-style9">Member Id
                            </td>
                            <td style="height: 36px">
                                <asp:TextBox ID="txtID" runat="server" Width="200px" OnTextChanged="MemberIDChanged" MaxLength="12" AutoPostBack="true"></asp:TextBox>
                                <asp:ImageButton ID="imageBkCbtn" runat="server" ImageUrl="~/Images/srchImg.gif" /></td>
                            <asp:HiddenField runat="server" ID="hdn" />
                            <asp:ModalPopupExtender ID="ModalPopupExtender2" runat="server" TargetControlID="imageBkCbtn" PopupControlID="Panel1"
                                BackgroundCssClass="modelBackground">
                            </asp:ModalPopupExtender>
                        </tr>
                        <tr>
                            <td class="auto-style9">
                               Member Name
                            </td>
                            <td style="height: 36px">

                        <asp:TextBox ID="txtMembername" runat="server"  Width="200px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style9">Old Scheme(Prior to April 2013)
                            </td>
                            <td>
                                <asp:TextBox ID="txtOldBalance" runat="server" Width="200px" ReadOnly="true"></asp:TextBox></td>
                        </tr>

                        <tr>
                            <td class="auto-style9">New Scheme
                            </td>
                            <td>
                                <asp:TextBox ID="txtCurrentBalance" runat="server" Width="200px" ReadOnly="true"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class="auto-style10">
                                <asp:Label ID="Label2" runat="server" Text="Type: " Font-Bold="true"></asp:Label></td>
                            <td>
                                <asp:RadioButtonList RepeatDirection="Horizontal" runat="server" ID="ChkTypeOfUtilization" AutoPostBack="true">
                                    <asp:ListItem Value="UTN" Selected="True">New Scheme</asp:ListItem>
                                    <asp:ListItem Value="UTO">Old Scheme</asp:ListItem>

                                </asp:RadioButtonList></td>
                        </tr>
                        <tr>
                            <td class="auto-style9">Utilization Date
                            </td>
                            <td>
                                <asp:TextBox ID="txtUtilizationDate" runat="server" Width="200px"></asp:TextBox></td>
                        </tr>
                        <asp:CalendarExtender ID="CalendarExtender3" runat="server"
                            TargetControlID="txtUtilizationDate" Format="dd/MM/yyyy">
                        </asp:CalendarExtender>
                        <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Date must be less than or equal to the current date." ValidationGroup="validation" Display="None" ForeColor="Red" ControlToValidate="txtUtilizationDate"
                            Operator="LessThanEqual" Type="Date"></asp:CompareValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" Display="None" ControlToValidate="txtUtilizationDate" ValidationGroup="validation"
                            ErrorMessage="Utilization Date in (dd/mm/yyyy) format" SetFocusOnError="true"
                            ValidationExpression="^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$">
                        </asp:RegularExpressionValidator>
                        <tr>
                            <td class="auto-style9">Utilization Point
                            </td>
                            <td>
                                <asp:TextBox ID="txtUtilization" runat="server" onkeypress="return allowOnlyNumber(event)" Width="200px"></asp:TextBox></td>
                        </tr>

                        <tr>
                            <td class="auto-style9">Remarks
                            </td>
                            <td>
                                <asp:TextBox ID="txtRemarks" runat="server" Width="606px" onkeypress="return this.value.length<500" TextMode="MultiLine" Rows="2"></asp:TextBox></td>
                        </tr>
                    </table>
                </center>
                <br />
                <table width="100%">
                    <tr>
                        <td align="center">
                            <asp:Button ID="btnSave" runat="server" Visible="true" Text="Save" OnClick="BtnSave_Click" CausesValidation="true" ValidationGroup="validation" Enabled="true"></asp:Button>
                        </td>
                    </tr>
                </table>

                <br />
                <br />
                <asp:Panel ID="Panel4" runat="server" Font-Bold="true" Style="background-color: #E0EBAD" ForeColor="Black" Visible="false">
                    <center>
                        <table>
                            <tr>
                                <td>
                                    <asp:Label ID="Label1" runat="server" Text="Points Utilization History" Visible="false"></asp:Label></td>
                            </tr>

                        </table>
                        <br />
                        <div id="div2" runat="server" style="overflow: scroll; Height: 300px">
                            <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" ShowFooter="true" OnRowDataBound="GridView_RowDataBound1" Visible="false" EmptyDataText="No Records Found" Width="920px">
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
                                    <asp:TemplateField HeaderText="Sl.No" ItemStyle-Width="7%">
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex + 1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Description" HeaderText="Transaction Type" ReadOnly="True" HeaderStyle-Wrap="false" ItemStyle-Width="15%" ItemStyle-Wrap="false" SortExpression="Description"></asp:BoundField>
                                    <asp:BoundField DataField="ReferenceNumber" HeaderText="Reference Id" SortExpression="ReferenceNumber" ItemStyle-Width="15%" HeaderStyle-Wrap="false" ItemStyle-Wrap="false" />
                                    <asp:BoundField DataField="NewPoints" HeaderText="New Points Utilized" SortExpression="NewPoints" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"></asp:BoundField>
                                    <asp:BoundField DataField="OldPoints" HeaderText="Old Points Utilized" SortExpression="OldPoints" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Left" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"></asp:BoundField>
                                    <asp:BoundField DataField="UtilizationDate" HeaderText="Utilization Date" ReadOnly="True" SortExpression="UtilizationDate" ItemStyle-Width="10%" DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-Wrap="false" ItemStyle-Wrap="false" />
                                    <asp:BoundField DataField="Remarks" HeaderText="Remarks " SortExpression="Remarks" HeaderStyle-Wrap="false" ItemStyle-Wrap="true" ItemStyle-Width="80%"></asp:BoundField>

                                </Columns>
                            </asp:GridView>
                            <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" SelectCommand="Incentive_ViewMemberwiseUtilizationPoint" SelectCommandType="StoredProcedure">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="txtID" Name="MemberId" PropertyName="Text" Type="String" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                        </div>
                        <br />
                        <br />
                    </center>
                </asp:Panel>
                <asp:Panel ID="Panel1" runat="server" Visible="false" CssClass="modelPopup" Style="width: 1000px; height: 400px; background-color: ghostwhite;"
                    BorderStyle="Groove" BorderColor="Black" BackColor="White">
                    <center>
                        <table style="background: white">
                            <tr>
                                <td align="center"><b>Search Employee Code/Roll No: </b>
                                    <asp:TextBox ID="txtmidSearch" runat="server"></asp:TextBox></td>
                                      <td align="center"><b>Search Name: </b>
                                        <asp:TextBox ID="txtmnameSearch" runat="server" Width="100px" ></asp:TextBox>
                                       
                                    </td>
                                <td>
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
                                        Visible="False" PageSize="18" Width="500px">
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
                                           <asp:BoundField DataField="Institution" HeaderText="ID" SortExpression="Institution" ItemStyle-CssClass="hiddencol"  HeaderStyle-CssClass="hiddencol" />
                                        </Columns>
                                        <EditRowStyle BorderColor="#FF6600" BorderStyle="Solid" />
                                    </asp:GridView>
                                    <%--<asp:SqlDataSource ID="SqlDataSourceMember" runat="server" ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" SelectCommand="SELECT  top 10  MemberId,( UPPER(FirstName)+' '+UPPER(MiddleName)+' '+UPPER(LastName)) as MemberName from Member_Incentive_Point_Summary , User_M  where
 User_M.User_Id=Member_Incentive_Point_Summary.MemberId and  (InstituteId=@InstituteId) and MemberType='M' and Active='Y'
 union
 Select distinct MemberId,Name from Member_Incentive_Point_Summary s,SISStudentGenInfo p where    MemberType='S'
 and p.RollNo=s.MemberId and InstID=(Select Institute_Id from SISInstnHR where HRInstitute=@InstituteId )"
                                        SelectCommandType="Text">
                                        <SelectParameters>
                                            <asp:SessionParameter Name="InstituteId" SessionField="InstituteId" Type="String" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>--%>
                                </td>
                            </tr>
                        </table>
                    </center>
                </asp:Panel>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ControlToValidate="txtRemarks" Display="None"
        ErrorMessage="Please enter the remarks" ValidationGroup="validation"></asp:RequiredFieldValidator>
    <asp:RegularExpressionValidator
        ID="RegularExpressionValidator6"
        runat="server" Display="None" ValidationGroup="validation"
        ControlToValidate="txtUtilization"
        ValidationExpression="([0-9])[0-9]*[.]?[0-9]*"
        ErrorMessage="Invalid Entry">
    </asp:RegularExpressionValidator>

    <asp:RegularExpressionValidator
        ID="RegularExpressionValidator1"
        runat="server" Display="None" ValidationGroup="validation"
        ControlToValidate="txtUtilization"
        ValidationExpression="^\d+\.?\d{0,2}$"
        ErrorMessage="Only two didgits are allowed after decimal">
    </asp:RegularExpressionValidator>

    <%--   <asp:RegularExpressionValidator
        ID="RegularExpressionValidator76"
        runat="server" Display="None" ValidationGroup="validation"
        ControlToValidate="txtUtilization"
        ValidationExpression="^[0-9]\d{0,9}(\.\d{1,3})?%?$"
        ErrorMessage="Invalid Entry1">
    </asp:RegularExpressionValidator>--%>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtUtilization" Display="None"
        ErrorMessage="Please enter Utilization Point" ValidationGroup="validation"></asp:RequiredFieldValidator>
</asp:Content>

