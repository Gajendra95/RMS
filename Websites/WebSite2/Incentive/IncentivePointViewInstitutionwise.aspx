<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPageRMS.master" AutoEventWireup="true" CodeFile="IncentivePointViewInstitutionwise.aspx.cs" Inherits="Incentive_IncentivePointViewInstitutionwise" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ToolkitScriptManager ID="Scriptmanager1" runat="server" />

    <style type="text/css">
        .AlgRgh {
            text-align: right;
            font-family: Verdana, Arial, Helvetica, sans-serif;
        }
        .grid {}
    </style>
    <asp:UpdatePanel ID="EditUpdatePanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Panel ID="Panel2" runat="server" Font-Bold="true" Style="background-color: #E0EBAD" ForeColor="Black">
                <br />
                <center>
                    <table runat="server" id="table" visible="true">
                        <tr>
                            <td>Employee Code/Roll No</td>
                            <td>
                                <asp:TextBox ID="txtEmployeecode" runat="server"></asp:TextBox></td>
                            <td>
                                <asp:Button runat="server" ID="Btnsearch" Text="Search" OnClick="Btnsearch_Click" /></td>
                        </tr>
                    </table>
                    <table runat="server" id="table1" visible="false">
                        <tr>
                            <td>
                                <asp:Label runat="server" ID="lblnote" Text=""></asp:Label></td>
                        </tr>
                    </table>
                </center>
            </asp:Panel>
            <br />
            <asp:Panel ID="Panel1" runat="server" Font-Bold="true" Style="background-color: #E0EBAD" ForeColor="Black" Visible="false">
                <br />
                <center>
                    <asp:GridView runat="server" ID="Gridview" CssClass="grid" AutoGenerateColumns="False" EmptyDataText="No Records Found" Visible="False" OnRowDataBound="GridView_RowDataBound" OnRowCreated="grvMergeHeader_RowCreated" DataSourceID="SqlDataSource2" BorderWidth="1px" CellPadding="3" DataKeyNames="MemberId">
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

                            <asp:BoundField DataField="MemberId" HeaderText="Employee Code/Roll No" SortExpression="MemberId" ControlStyle-BorderWidth="10" ReadOnly="True"></asp:BoundField>
                            <asp:BoundField DataField="MemberName" HeaderText="Member Name" SortExpression="MemberName" ControlStyle-BorderWidth="10"></asp:BoundField>
                            <asp:BoundField DataField="OldOpeningBalance" HeaderText="Old Scheme Point" SortExpression="OldOpeningBalance" ControlStyle-BorderWidth="10"></asp:BoundField>
                            <asp:BoundField DataField="NewOpeningBalance" HeaderText="New Scheme Point" SortExpression="NewOpeningBalance" ControlStyle-BorderWidth="10"></asp:BoundField>
                            <asp:BoundField DataField="CurrentBalance" HeaderText="New Scheme Points" SortExpression="CurrentBalance" ControlStyle-BorderWidth="10"></asp:BoundField>
                            <asp:BoundField DataField="OldBalance" HeaderText="Old Scheme Points" SortExpression="OldBalance" ControlStyle-BorderWidth="10"></asp:BoundField>

                        </Columns>
                    </asp:GridView>
                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" SelectCommand="Incentive_ViewMemberwiseIncentivePoint" SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="txtEmployeecode" Name="MemberId" PropertyName="Text" Type="String" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </center>
                <br />
                <br />
                <center>
                    <asp:GridView runat="server" ID="GridviewStudent" CssClass="grid" EmptyDataText="No Records Found" AutoGenerateColumns="False" Visible="False" OnRowCreated="GridviewStudent_RowCreated" DataSourceID="SqlDataSourceStudent" BorderWidth="1px" CellPadding="3" DataKeyNames="RollNo" Width="773px">
                        <AlternatingRowStyle BackColor="White" />
                        <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                        <HeaderStyle BackColor="#0b532d" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                        <PagerStyle ForeColor="#000000" HorizontalAlign="Center" />
                        <RowStyle BackColor="#FFFFFF" ForeColor="#000000" />
                        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#FFF1D4" />
                        <SortedAscendingHeaderStyle BackColor="#B95C30" />
                        <SortedDescendingCellStyle BackColor="#F1E5CE" />
                        <SortedDescendingHeaderStyle BackColor="#93451F" />
                        <Columns>

                            <asp:BoundField DataField="RollNo" HeaderText="Roll No" SortExpression="RollNo" ControlStyle-BorderWidth="10" ReadOnly="True"></asp:BoundField>
                            <asp:BoundField DataField="StudentName" HeaderText="Student Name" SortExpression="StudentName" ControlStyle-BorderWidth="10"></asp:BoundField>
                            <asp:BoundField DataField="PRAISE" HeaderText="PRAISE Point" SortExpression="PRAISE" ControlStyle-BorderWidth="10"></asp:BoundField>
                            <asp:BoundField DataField="FAIR" HeaderText="FAIR Point" SortExpression="FAIR" ControlStyle-BorderWidth="10"></asp:BoundField>
                            <asp:BoundField DataField="Total" HeaderText="Total" SortExpression="Total" ControlStyle-BorderWidth="10"></asp:BoundField>

                        </Columns>
                    </asp:GridView>
                    <asp:SqlDataSource ID="SqlDataSourceStudent" runat="server" ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" SelectCommand="Incentive_StudentIncentivePointDetails" SelectCommandType="StoredProcedure">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="txtEmployeecode" Name="MemberId" PropertyName="Text" Type="String" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </center>
                <br />

            </asp:Panel>
            <br />
            <br />
            <asp:Panel ID="Panel5" runat="server" Font-Bold="true" Style="background-color: #E0EBAD" ForeColor="Black" Visible="false" Height="264px">

                <center>
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="lbl5" runat="server" Text="Article Wise Points" Visible="false"></asp:Label></td>
                        </tr>

                    </table>
                    <br />
                    <div id="div2" runat="server" style="overflow: scroll; height: 193px; margin-bottom: 0px;">
                        <asp:GridView ID="GridView5" runat="server" AutoGenerateColumns="False" Visible="false" OnRowDataBound="GridView5_RowDataBound2" DataSourceID="SqlDataSource5" EmptyDataText="No Records Found" Width="800px">
                            <AlternatingRowStyle BackColor="White" />
                            <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                            <HeaderStyle BackColor="#0b532d" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"/>
                            <PagerStyle ForeColor="#000000" HorizontalAlign="Center" />
                            <RowStyle BackColor="#FFFFFF" ForeColor="#000000" />
                            <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
                            <SortedAscendingCellStyle BackColor="#FFF1D4" />
                            <SortedAscendingHeaderStyle BackColor="#B95C30" />
                            <SortedDescendingCellStyle BackColor="#F1E5CE" />
                            <SortedDescendingHeaderStyle BackColor="#93451F" />
                            <Columns>
                                <asp:BoundField DataField="ReferenceNumber" HeaderText="Publication Id" HeaderStyle-Wrap="false" SortExpression="ReferenceNumber" ItemStyle-Wrap="false" ItemStyle-Width="10%" />
                                <asp:BoundField DataField="TitleWorkItem" HeaderText="Title" SortExpression="TitleWorkItem" HeaderStyle-Wrap="false" ItemStyle-Wrap="true" ItemStyle-Width="80%" />
                                <asp:BoundField DataField="PublishMonthYear" HeaderText="Month&Year of Publication" SortExpression="PublishMonthYear" HeaderStyle-Wrap="false" ItemStyle-Wrap="true" ItemStyle-Width="80%" DataFormatString="{0:MMM}" />
                                <asp:BoundField DataField="MUCategorization" HeaderText="Publication Category" HeaderStyle-Wrap="false" SortExpression="MUCategorization" ItemStyle-Wrap="false" ItemStyle-Width="10%" />
                                <asp:BoundField DataField="Points" HeaderText="Points" ReadOnly="True" HeaderStyle-Wrap="false" SortExpression="Points" ItemStyle-Wrap="false" ItemStyle-Width="20%" />
                            </Columns>
                        </asp:GridView>
                        <asp:SqlDataSource ID="SqlDataSource5" runat="server" ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" SelectCommand="Incentive_SelectArticlewiseIncentivePoint" SelectCommandType="StoredProcedure">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="txtEmployeecode" Name="MemberId" PropertyName="Text" Type="String" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                    </div>
                    <asp:Button ID="btnExpt" runat="server" Text="Excel Export"   OnClick="btnExpt_Click" />
                </center>
            </asp:Panel>
            <br />
               <asp:Panel ID="PanelPRAISE" runat="server" Font-Bold="true" Style="background-color: #E0EBAD" ForeColor="Black" Visible="false" Height="300px">

                <center>
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="Label1" runat="server" Text="Article Wise Points" Visible="true"></asp:Label></td>
                        </tr>

                    </table>
                    <br />
                    <div id="div3" runat="server" style="overflow: scroll; height: 216px;">
                        <asp:GridView ID="GridViewPRAISE" runat="server" AutoGenerateColumns="False" OnRowCreated="grvMergeHeader_RowCreated1" Visible="true" OnRowDataBound="GridView5_RowDataBound2" DataSourceID="SqlDataSource1" EmptyDataText="PRAISE articles are not found" Width="800px">
                            <AlternatingRowStyle BackColor="White" />
                            <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                            <HeaderStyle BackColor="#0b532d" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                            <PagerStyle ForeColor="#000000" HorizontalAlign="Center" />
                            <RowStyle BackColor="#FFFFFF" ForeColor="#000000" />
                            <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
                            <SortedAscendingCellStyle BackColor="#FFF1D4" />
                            <SortedAscendingHeaderStyle BackColor="#B95C30" />
                            <SortedDescendingCellStyle BackColor="#F1E5CE" />
                            <SortedDescendingHeaderStyle BackColor="#93451F" />
                            <Columns>
                                <asp:BoundField DataField="ReferenceNumber" HeaderText="Publication Id" HeaderStyle-Wrap="false"  SortExpression="ReferenceNumber"  ItemStyle-Wrap="false" ItemStyle-Width="10%"/>
                                <asp:BoundField DataField="TitleWorkItem" HeaderText="Title" SortExpression="TitleWorkItem" HeaderStyle-Wrap="false" ItemStyle-Wrap="true" ItemStyle-Width="80%"/>
                                 <asp:BoundField DataField="PublishMonthYear" HeaderText="Month&Year of Publication" SortExpression="PublishMonthYear" HeaderStyle-Wrap="false" ItemStyle-Wrap="true" ItemStyle-Width="80%" DataFormatString="{0:MMM}" />
                                 <asp:BoundField DataField="MUCategorization" HeaderText="Publication Category" HeaderStyle-Wrap="false"  SortExpression="MUCategorization" ItemStyle-Wrap="false" ItemStyle-Width="10%" />
                                <asp:BoundField DataField="Points" HeaderText="Points" ReadOnly="True" HeaderStyle-Wrap="false"  SortExpression="Points" ItemStyle-Wrap="false" ItemStyle-Width="20%" />
                            </Columns>
                        </asp:GridView>
                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" SelectCommand="Incentive_PRAISEArticlewiseIncentivePoint" SelectCommandType="StoredProcedure">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="txtEmployeecode" Name="MemberId" PropertyName="Text" Type="String" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                        <br />
                        <br />
                         <asp:GridView ID="GridViewFAIR" runat="server" AutoGenerateColumns="False" OnRowCreated="grvMergeHeader"  Visible="true" OnRowDataBound="GridView5_RowDataBound2" DataSourceID="SqlDataSource3" EmptyDataText="FAIR articles are not found" Width="800px">
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
                                <asp:BoundField DataField="ReferenceNumber" HeaderText="Publication Id" HeaderStyle-Wrap="false"  SortExpression="ReferenceNumber"  ItemStyle-Wrap="false" ItemStyle-Width="10%"/>
                                <asp:BoundField DataField="TitleWorkItem" HeaderText="Title" SortExpression="TitleWorkItem" HeaderStyle-Wrap="false" ItemStyle-Wrap="true" ItemStyle-Width="80%"/>
                                   <asp:BoundField DataField="PublishMonthYear" HeaderText="Month&Year of Publication" SortExpression="PublishMonthYear" HeaderStyle-Wrap="false" ItemStyle-Wrap="true" ItemStyle-Width="80%" DataFormatString="{0:MMM}" />
                                <asp:BoundField DataField="MUCategorization" HeaderText="Publication Category" HeaderStyle-Wrap="false"  SortExpression="MUCategorization" ItemStyle-Wrap="false" ItemStyle-Width="10%" />
                                <asp:BoundField DataField="Points" HeaderText="Points" ReadOnly="True" HeaderStyle-Wrap="false"  SortExpression="Points" ItemStyle-Wrap="false" ItemStyle-Width="20%" />
                            </Columns>
                        </asp:GridView>
                        <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" SelectCommand="Incentive_FAIRArticlewiseIncentivePoint" SelectCommandType="StoredProcedure">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="txtEmployeecode" Name="MemberId" PropertyName="Text" Type="String" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                    </div>
                </center>
            </asp:Panel>
            <asp:Panel ID="Panel3" runat="server" Font-Bold="true" Style="background-color: #E0EBAD" ForeColor="Black" Visible="false" Height="414px">
                <center>
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="lbl1" runat="server" Text="Points Transaction Detail" Visible="false"></asp:Label></td>
                        </tr>

                    </table>
                    <br />
                    <div id="div1" runat="server" style="overflow: scroll; height: 372px;">
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Visible="false" OnRowDataBound="GridView_RowDataBound1" ShowFooter="true" EmptyDataText="No Records Found" Width="1036px">
                            <AlternatingRowStyle BackColor="White" />
                            <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" HorizontalAlign="Left" />
                            <HeaderStyle BackColor="#0b532d" Font-Bold="True" ForeColor="White" HorizontalAlign="Left" />
                            <PagerStyle ForeColor="#000000" HorizontalAlign="Center" />
                            <RowStyle BackColor="#FFFFFF" ForeColor="#000000" />
                            <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
                            <SortedAscendingCellStyle BackColor="#FFF1D4" />
                            <SortedAscendingHeaderStyle BackColor="#B95C30" />
                            <SortedDescendingCellStyle BackColor="#F1E5CE" />
                            <SortedDescendingHeaderStyle BackColor="#93451F" />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No" ItemStyle-Width="6%">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                    <ItemStyle Width="6%" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="Description" HeaderText="Transaction Type" HeaderStyle-Wrap="false" ItemStyle-Wrap="false" ReadOnly="True" SortExpression="Description" ItemStyle-Width="15%">
                                    <HeaderStyle Wrap="False" />
                                    <ItemStyle Width="15%" Wrap="False" />
                                </asp:BoundField>
                                <asp:BoundField DataField="ReferenceNumber" HeaderText="Reference Id" HeaderStyle-Wrap="false" SortExpression="ReferenceNumber" ItemStyle-Wrap="false" ItemStyle-Width="10%">
                                    <HeaderStyle Wrap="False" />
                                    <ItemStyle Width="10%" Wrap="False" />
                                </asp:BoundField>
                                <asp:BoundField DataField="NewPoints" HeaderText="New Points" SortExpression="NewPoints" HeaderStyle-Wrap="false" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%">
                                    <HeaderStyle Wrap="False" />
                                    <ItemStyle HorizontalAlign="Left" Width="10%" Wrap="False" />
                                </asp:BoundField>
                                <asp:BoundField DataField="OldPoints" HeaderText="Old Points" SortExpression="OldPoints" HeaderStyle-Wrap="false" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="10%">
                                    <HeaderStyle Wrap="False" />
                                    <ItemStyle HorizontalAlign="Left" Width="10%" Wrap="False" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Date" HeaderText="Transaction Date" ReadOnly="True" SortExpression="Date" ItemStyle-Wrap="false" DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-Wrap="false" ItemStyle-Width="15%">
                                    <HeaderStyle Wrap="False" />
                                    <ItemStyle Width="15%" Wrap="False" />
                                </asp:BoundField>
                                <%--                            <asp:BoundField DataField="UtilizationDate" HeaderText="Utilization Date" ReadOnly="True" SortExpression="UtilizationDate" ItemStyle-CssClass="hidden-field" HeaderStyle-CssClass="hidden-field"  DataFormatString="{0:dd/MM/yyyy}" />--%>
                                <asp:BoundField DataField="Remarks" HeaderText="Remarks " SortExpression="Remarks" ItemStyle-Wrap="true" ItemStyle-Width="80%">
                                    <ItemStyle Width="80%" Wrap="True" />
                                </asp:BoundField>

                            </Columns>
                        </asp:GridView>
                        <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" SelectCommand="Incentive_ViewPointsTransaction" SelectCommandType="StoredProcedure">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="txtEmployeecode" Name="MemberId" PropertyName="Text" Type="String" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                    </div>
                </center>
            </asp:Panel>
            <br />
            <%--<asp:Panel ID="Panel4" runat="server" Font-Bold="true" Style="background-color: #E0EBAD" ForeColor="Black" Visible="false">
                <center>
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="Label1" runat="server" Text="Points Utilization" Visible="false"></asp:Label></td>
                        </tr>

                    </table>
                    <br />
                    <div id="div2" runat="server" style="overflow: scroll; height: auto">
                        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource3" Visible="false" EmptyDataText="No Records Found" Width="393px">
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
                                <asp:BoundField DataField="UtilizationDate" HeaderText="Utilization Date" SortExpression="UtilizationDate" DataFormatString="{0:dd/MM/yyyy}" />
                                <asp:BoundField DataField="UtilizedPoint" HeaderText="Utilized Point" SortExpression="UtilizedPoint" />
                                <asp:BoundField DataField="Remarks" HeaderText="Remarks" SortExpression="Remarks" />
                            </Columns>
                        </asp:GridView>
                        <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" SelectCommand="Incentive_ViewMemberwiseUtilizationPoint" SelectCommandType="StoredProcedure">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="txtEmployeecode" Name="MemberId" PropertyName="Text" Type="String" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                    </div>

                </center>
            </asp:Panel>--%>
            <br />
            <br />
        </ContentTemplate>
         <Triggers>
        
  <asp:PostBackTrigger ControlID="btnExpt" />
  
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

