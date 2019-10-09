<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/MasterPage/MasterPageRMS.master" CodeFile="FeedbackView.aspx.cs" Inherits="ManageJournal_FeedbackView" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <asp:ScriptManager ID="Scriptmanager1" runat="server" />    

      <center>
        <asp:Label ID="lablPanelTitle" runat="server" Text="View Feedback " Font-Bold="true"></asp:Label></center>
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
                                    <asp:RadioButtonList RepeatDirection="Horizontal" runat="server" ID="radioincentive"  AutoPostBack="true" OnSelectedIndexChanged="radioincentive_SelectedIndexChanged">
                                        <asp:ListItem Value="1"  Selected="True">Publication</asp:ListItem>
                                          <asp:ListItem Value="2">Project</asp:ListItem>
                                    </asp:RadioButtonList></td>
                            </tr>

                        </table>
                    </center>
                </asp:Panel>
            </center>
            <br />

            <%-- <asp:Panel ID="panelSearchPub" runat="server" ForeColor="Black" GroupingText="" Font-Bold="true" Style="background-color: #E0EBAD; display:none">
                <br />
                <center>
                    <table style="width: 20%">
                        <tr>
                            
                            <td width="20px"  style="height: 36px; font-weight: normal; color: Black" >MemberID
                            </td>
                            <td style="height: 36px">
                                <asp:TextBox ID="txtmemberID" runat="server" Style="border-style: inset none none inset;"></asp:TextBox>
                            </td>
 <td style="height: 36px">
                                <asp:Button ID="ButtonSearchPub" runat="server" Text="Search" OnClick="ButtonSearchPubOnClick" />
                            </td>
  </tr>

                    </table>
                    </center>
                    <br />

                               </asp:Panel> --%>
                                 <asp:Panel ID="panel2" runat="server" ForeColor="Black" Font-Bold="true" Style="background-color: #E0EBAD">   
          <br />
                                                     <div class="content" style="overflow-x: scroll;">
   
                                        <center>
               <asp:GridView ID="GridviewFeedback" runat="server" AllowPaging="True"
                            PagerSettings-PageButtonCount="5" PageSize="15" DataSourceID="SqlDataSource1"
                            HeaderStyle-BackColor="#CC6600" HeaderStyle-ForeColor="Black" AutoGenerateColumns="False" OnPageIndexChanging="GridViewSearchPublication_PageIndexChanging"
                            PagerStyle-BackColor="#CC6600" PagerStyle-ForeColor="White" CellPadding="3" 
                            CellSpacing="3" PagerStyle-Width="4" PagerStyle-Height="4" Width="100%"
                            BorderColor="#FF6600" BorderStyle="Solid" >
               
                      <Columns>
                            
                          <asp:TemplateField HeaderText="Sl.No">
                    <ItemTemplate>
                         <%#Container.DataItemIndex+1 %>
                      </ItemTemplate>
                      </asp:TemplateField>
                                <asp:BoundField DataField="MemberID" HeaderText="Employee Code"  HeaderStyle-HorizontalAlign="Center" SortExpression="MemberID"  />    
                                 <asp:BoundField DataField="Q1" HeaderText="Rating the Ease of Publication Entry"  HeaderStyle-HorizontalAlign="Center" SortExpression="Q1"  />    
                                 <asp:BoundField DataField="Q2" HeaderText="Ease of navigation to required Journal" HeaderStyle-HorizontalAlign="Center" SortExpression="Q2"/>    
                                <asp:BoundField DataField="Q3" HeaderText="Rating of overall Experience"  HeaderStyle-HorizontalAlign="Center" SortExpression="Q3"  />    
                                <asp:BoundField DataField="Q4" HeaderText="Comment on overall area to improve" HeaderStyle-HorizontalAlign="Center" SortExpression="Q4" />    
<%--                              <asp:BoundField DataField="CreatedDate" HeaderText="Feedback Given Date"  DataFormatString="{0:dd/MM/yyyy}" />--%>



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
                </center>
                                                         </div>
                                     <br />
                                     </asp:Panel>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server"
                        ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>"
                        SelectCommand=""></asp:SqlDataSource>

                                 <asp:Panel ID="panel3" runat="server" ForeColor="Black" Font-Bold="true" Style="background-color: #E0EBAD">   
          <br />
                                                     <div class="content" style="overflow-x: scroll;">
   
                                        <center>
               <asp:GridView ID="Gridviewprj" runat="server" AllowPaging="True"
                            PagerSettings-PageButtonCount="5" PageSize="15" DataSourceID="SqlDataSource2"
                            HeaderStyle-BackColor="#CC6600" HeaderStyle-ForeColor="Black" AutoGenerateColumns="False" OnPageIndexChanging="GridViewSearchPrj_PageIndexChanging"
                            PagerStyle-BackColor="#CC6600" PagerStyle-ForeColor="White" CellPadding="3" 
                            CellSpacing="3" PagerStyle-Width="4" PagerStyle-Height="4" Width="100%"
                            BorderColor="#FF6600" BorderStyle="Solid" >
               
                      <Columns>
                               
                          <asp:TemplateField HeaderText="Sl.No">
                    <ItemTemplate>
                         <%#Container.DataItemIndex+1 %>
                      </ItemTemplate>
                      </asp:TemplateField>
                                <asp:BoundField DataField="MemberID" HeaderText="Employee Code"  HeaderStyle-HorizontalAlign="Center" SortExpression="MemberID"  />    
                                 <asp:BoundField DataField="Q5" HeaderText="Rating the Ease of Project Entry"  HeaderStyle-HorizontalAlign="Center" SortExpression="Q5"  />    
                                 <asp:BoundField DataField="Q6" HeaderText="Ease of navigation to required Project" HeaderStyle-HorizontalAlign="Center" SortExpression="Q6"/>    
                                <asp:BoundField DataField="Q3" HeaderText="Rating of overall Experience"  HeaderStyle-HorizontalAlign="Center" SortExpression="Q3"  />    
                                <asp:BoundField DataField="Q4" HeaderText="Comment on overall area to improve" HeaderStyle-HorizontalAlign="Center" SortExpression="Q4" />    
<%--                              <asp:BoundField DataField="CreatedDate" HeaderText="Feedback Given Date"  DataFormatString="{0:dd/MM/yyyy}" />--%>



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
                </center>
                                                         </div>
                                     <br />
                                     </asp:Panel>

                

               <asp:SqlDataSource ID="SqlDataSource2" runat="server"
                        ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>"
                        SelectCommand=""></asp:SqlDataSource>
            </ContentTemplate>
        </asp:UpdatePanel>
</asp:Content>




                          
                         



