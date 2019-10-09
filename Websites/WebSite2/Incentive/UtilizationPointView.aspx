<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPageRMS.master" AutoEventWireup="true" CodeFile="UtilizationPointView.aspx.cs" Inherits="Incentive_UtilizationPointView" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        .hidden-field {
            display: none;
        }
    </style>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" DisplayMode="BulletList"
        HeaderText="Validation issues" ShowSummary="False" ValidationGroup="validation" />
     <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="True" DisplayMode="BulletList"
        HeaderText="Validation issues" ShowSummary="False" ValidationGroup="validation1" />
    <asp:ToolkitScriptManager ID="Scriptmanager1" runat="server" />
    <asp:UpdatePanel ID="EditUpdatePanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Panel ID="Panel1" runat="server" Width="1000px" Font-Bold="true" Style="background-color: #E0EBAD" ForeColor="Black">
                <br />
                <center>
                    <asp:Label ID="Label1" runat="server" Text="Utilization Point View" Font-Bold="true"></asp:Label>
                </center>
                <center>
                    <table>
                        <tr>
                            <td style="height: 38px">Start Date
                            </td>
                            <td>
                                <asp:TextBox ID="txtStartDate" runat="server" Width="100px"></asp:TextBox>
                            </td>
                            <td></td>
                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtStartDate" Format="dd/MM/yyyy">
                            </asp:CalendarExtender>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ControlToValidate="txtStartDate" Display="None"
                                ErrorMessage="Please enter start date" ValidationGroup="validation"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Start Date Date in (dd/mm/yyyy) format" Display="None" ControlToValidate="txtStartDate" ValidationGroup="validation"
                                SetFocusOnError="true" ValidationExpression="^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$">
                            </asp:RegularExpressionValidator>

                            <td style="height: 38px">To Date
                            </td>
                            <td>
                                <asp:TextBox ID="txtEndDate" runat="server" Width="100px"></asp:TextBox>
                            </td>

                            <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtEndDate" Format="dd/MM/yyyy">
                            </asp:CalendarExtender>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtEndDate" Display="None"
                                ErrorMessage="Please enter To date" ValidationGroup="validation1"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="End Date Date in (dd/mm/yyyy) format" Display="None" ControlToValidate="txtStartDate" ValidationGroup="validation1"
                                SetFocusOnError="true" ValidationExpression="^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"></asp:RegularExpressionValidator>
                            <td style="height: 36px">
                                <asp:Button runat="server" ID="BtnSearch" Text="View" OnClick="BtnSearch_Click" Width="79px" Font-Bold="True" CausesValidation="true" ValidationGroup="validation1" />
                            </td>
                        </tr>
                    </table>
                </center>

                <br />
                <center>
                    <asp:GridView runat="server" ID="Gridview" AutoGenerateColumns="False"  OnRowDataBound="GridView_RowDataBound1" AllowPaging="true" PageSize="5" DataKeyNames="MemberId" DataSourceID="SqlDataSource1" EmptyDataText="No Data Found">
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
                            <asp:BoundField DataField="MemberId" HeaderText="Emp Code/Roll No" ReadOnly="True" SortExpression="MemberId" ControlStyle-BorderWidth="10" HeaderStyle-Wrap="false" ItemStyle-Wrap="false" ItemStyle-Width="15%" />
                            <asp:BoundField DataField="ReferenceNumber" HeaderText="Reference Number" SortExpression="ReferenceNumber" ItemStyle-Width="10%" HeaderStyle-Wrap="false" ItemStyle-Wrap="false"  />
                            <asp:BoundField DataField="TotalPoint" HeaderText="Utilization Point" SortExpression="TotalPoint"  ItemStyle-Width="10%" HeaderStyle-Wrap="false" ItemStyle-Wrap="false" />
                            <asp:BoundField DataField="UtilizationDate" HeaderText="Utilization Date" DataFormatString="{0:dd/MM/yyyy}" SortExpression="UtilizationDate" ItemStyle-Width="10%" HeaderStyle-Wrap="false" ItemStyle-Wrap="false" />
                            <asp:BoundField DataField="Remarks" HeaderText="Remarks" SortExpression="Remarks" ItemStyle-Wrap="true" ItemStyle-Width="80%" />
                            <asp:BoundField DataField="TransactionType" HeaderText="TransactionType" ItemStyle-CssClass="hidden-field" HeaderStyle-CssClass="hidden-field" SortExpression="TransactionType" />
                            <asp:BoundField DataField="MemberType" HeaderText="TransactionType" ItemStyle-CssClass="hidden-field" HeaderStyle-CssClass="hidden-field" SortExpression="TransactionType" />
                            <asp:TemplateField ShowHeader="false">
                                <ItemTemplate>

                                    <asp:Button ID="BtnSendMail" runat="server" Text="Send EMail" OnClick="BtnSendMail_Click" />

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
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" SelectCommand="Incentive_SelectUtilizationPointsData" SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="txtStartDate" DbType="Date" Name="StartDate" PropertyName="Text" />
                            <asp:ControlParameter ControlID="txtEndDate" DbType="Date" Name="EndDate" PropertyName="Text" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                    <br />
            </asp:Panel>
            </center>

            <br />
            <br />
            <asp:Panel ID="PanelSendMail" runat="server" ForeColor="Black" Visible="false" GroupingText="Send Email" Width="980px" Font-Bold="true" Style="background-color: #E0EBAD">
                <center>
                    <table border="1">
                        <tr>
                            <td class="auto-style9">To
                            </td>
                            <td style="height: 36px">
                                <asp:TextBox ID="txtTo" runat="server" Width="534px" ReadOnly="true" Style="margin-left: 0px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style9">CC
                            </td>
                            <td>
                                <asp:TextBox ID="txtCC" runat="server" Width="533px" ReadOnly="true"></asp:TextBox></td>
                        </tr>

                        <tr>
                            <td class="auto-style9">Subject
                            </td>
                            <td>
                                <asp:TextBox ID="txtSubject" runat="server" Width="531px" TextMode="MultiLine"  Columns="50" Rows="5"></asp:TextBox></td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtSubject" Display="None"
                                        ErrorMessage="Please enter Subject" ValidationGroup="validation"></asp:RequiredFieldValidator>
                             </tr>

                        <tr>
                            <td class="auto-style9">Message Content
                            </td>
                            <td>
                                <asp:TextBox ID="txtMsgContent" runat="server" Width="529px" TextMode="MultiLine" Columns="50" Rows="5"></asp:TextBox></td>
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtMsgContent" Display="None"
                                        ErrorMessage="Please enter message content" ValidationGroup="validation"></asp:RequiredFieldValidator>
                        </tr>
                        <tr><td>
                            <br />
                            </td></tr>
                        <tr>
                            <td></td>
                            <td align="center">
                                <asp:Button ID="btnSendMail" CausesValidation="true" ValidationGroup="validation" runat="server" Visible="true" Text="Send EMail" OnClick="btnSendMail_Click" Font-Bold="true" Width="210px"></asp:Button>
                            </td>
                        </tr>
                    </table>
                </center>



            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

