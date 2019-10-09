<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPageRMS.master" AutoEventWireup="true" CodeFile="ArticleWisePointsView.aspx.cs" Inherits="Incentive_ArticleWisePointsView" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ToolkitScriptManager ID="Scriptmanager1" runat="server" />

    <asp:UpdatePanel ID="EditUpdatePanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Panel ID="panel3" runat="server" ForeColor="Black" Font-Bold="true" Style="background-color: #E0EBAD">
                    <center>
                     
                        <table>
                            <tr>
                                <td style="width: 38px">
                                    <asp:Label ID="Label2" runat="server" Text="Type: " Font-Bold="true"></asp:Label></td>
                                <td>
                                    <asp:RadioButtonList RepeatDirection="Horizontal" runat="server" ID="radioincentive" AutoPostBack="true" OnSelectedIndexChanged="radioincentive_SelectedIndexChanged">
                                        <asp:ListItem Value="1" Selected="True">Publication</asp:ListItem>
                                          <asp:ListItem Value="2" >Patent</asp:ListItem>
                                    </asp:RadioButtonList></td>
                                    </td>
                            </tr>

                        </table>
                    </center>
                </asp:Panel>
            <br />
            <asp:Panel ID="Panel2" runat="server" Font-Bold="true" Style="background-color: #E0EBAD" ForeColor="Black">
                <br />
                <center>
                    <asp:Label ID="Label1" runat="server" Text="Article-Wise Point View" Font-Bold="true"></asp:Label>
                </center>
                <br />
                <br />

                <center>
                    <table runat="server" id="table" visible="true">
                        <tr>
                            <td>Publication Id:</td>
                            <td>
                                <asp:TextBox ID="txtPublicationId" runat="server"></asp:TextBox></td>
                            <td>
                                <asp:Button runat="server" ID="Btnsearch" Text="Search" OnClick="Btnsearch_Click" /></td>
                        </tr>
                    </table>
                </center>


                <br />
                <asp:Panel ID="PnlPublicationDetails" runat="server" GroupingText="Publication Details" Enabled="false" Style="background-color: #E0EBAD;">
                    <table style="width: 97%">
                        <tr>


                            <td style="font-weight: normal; width: 181px;">
                                <asp:Label ID="LabelPubId" runat="server" Text="PublicationId" ForeColor="Black"></asp:Label>
                            </td>
                            <td style="width: 207px">
                                <asp:TextBox ID="TextBoxPubId" runat="server" Enabled="false"
                                    Style="border-style: inset none none inset;" Width="200px"></asp:TextBox>
                            </td>
                            <td style="height: 36px; font-weight: normal; width: 213px;">
                                <asp:Label ID="lblTitileWorkItem" runat="server" Text="Title" ForeColor="Black"></asp:Label>
                            </td>
                            <td colspan="6" style="height: 36px">
                                <asp:TextBox ID="txtboxTitleOfWrkItem" runat="server" MaxLength="200"
                                    Style="border-style: inset none none inset;" TextMode="MultiLine" Width="623px"></asp:TextBox>
                            </td>

                        </tr>
                        <tr>
                            <td style="height: 36px; font-weight: normal; width: 181px;">
                                <asp:Label ID="lblMUCat" runat="server" Text="MAHE Categorization" ForeColor="Black"></asp:Label>

                            </td>

                            <td style="height: 36px; width: 207px;">
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

                            <td style="height: 36px; font-weight: normal; width: 213px;">
                                <asp:Label ID="LabelPubJournal" runat="server" Text="ISSN" ForeColor="Black"></asp:Label>
                            </td>
                            <td style="height: 36px">
                                <asp:TextBox ID="TextBoxPubJournal" runat="server" Width="190px" Style="border-style: inset none none inset;"></asp:TextBox>
                            </td>
                            <td style="font-weight: normal; width: 143px;">
                                <asp:Label ID="LabelNameJournal" runat="server" Text="Name Of Journal" ForeColor="Black"></asp:Label>
                            </td>
                            <td style="height: 36px">
                                <asp:TextBox ID="TextBoxNameJournal" runat="server" Enabled="false" Width="275px" Style="border-style: inset none none inset;" Height="25px"></asp:TextBox>
                            </td>

                        </tr>
                    </table>
                    <asp:SqlDataSource ID="SqlDataSourcePubJAmonth" runat="server" ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>"
                                    SelectCommand="select MonthValue, MonthName from Publication_MonthM"></asp:SqlDataSource>
                    <table width="100%">
                        <tr>
                            <td style="height: 56px; font-weight: normal; width: 114px;">
                                <asp:Label ID="LabelMonthJA" runat="server" Text="Publish Month" ForeColor="Black"></asp:Label></td>
                            <td style="width: 203px">
                                <asp:DropDownList ID="DropDownListMonthJA" runat="server" Style="border-style: inset none none inset;" DataSourceID="SqlDataSourcePubJAmonth"
                                    DataTextField="MonthName" DataValueField="MonthValue" Width="196px" Height="21px">
                                </asp:DropDownList>
                                
                            </td>
                            <td style="height: 56px; font-weight: normal; width: 94px;">
                                <asp:Label ID="LabelYearJA" runat="server" Text="Publish Year" ForeColor="Black"></asp:Label></td>
                            <td style="width: 202px">
                                <asp:DropDownList ID="TextBoxYearJA" runat="server"
                                    Width="182px" Style="border-style: inset none none inset;" Height="22px">
                                </asp:DropDownList>
                            </td>
                            </tr>
                        <tr>
                            <td style="height: 56px; font-weight: normal; width: 114px;">
                                <asp:Label ID="LabelImpFact" runat="server" Text="1-Year Impact Factor" ForeColor="Black"></asp:Label></td>
                            <td style="width: 203px">
                                <asp:TextBox ID="TextBoxImpFact" runat="server" ReadOnly="true" Style="border-style: inset none none inset;" Width="188px"></asp:TextBox>
                            </td>

                            <td style="height: 56px; font-weight: normal; width: 94px;">
                                <asp:Label ID="LabelImpFact5" runat="server" Text="5-Year Impact Factor" ForeColor="Black"></asp:Label></td>
                            <td style="width: 202px">
                                <asp:TextBox ID="TextBoxImpFact5" runat="server" ReadOnly="true" Style="border-style: inset none none inset;" Width="181px"></asp:TextBox>

                            </td>
                            <td style="height: 56px; font-weight: normal; width: 142px;">
                                <asp:Label ID="LblIFAY" runat="server" Text="IF-ApplicableYear" ForeColor="Black"></asp:Label></td>
                            <td>
                                <asp:TextBox ID="txtIFApplicableYear" runat="server" ReadOnly="true" Style="border-style: inset none none inset;" Width="164px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>

                            <td style="height: 54px; font-weight: normal; width: 114px;">
                                <asp:Label ID="LabelPubicationType" runat="server" Text="Publication Type" ForeColor="Black"></asp:Label></td>
                            <td style="width: 204px; height: 54px;">
                                <asp:DropDownList ID="DropDownListPubType" runat="server" Style="border-style: inset none none inset; margin-left: 2px;" Height="17px" Width="187px">
                                    <asp:ListItem Value="N">National</asp:ListItem>
                                    <asp:ListItem Value="I">International</asp:ListItem>
                                </asp:DropDownList>

                            </td>

                            <td style="height: 54px; font-weight: normal; width: 93px;">
                                <asp:Label ID="LabelPageFrom" runat="server" Text="Page From" ForeColor="Black"></asp:Label>
                                <%--<asp:Label ID="Labeljastr4" runat="server" Text="*" ForeColor="Red" Visible="false"></asp:Label>--%>
                            </td>

                            <td style="width: 195px; height: 54px;">
                                <asp:TextBox ID="TextBoxPageFrom" runat="server" Width="184px" MaxLength="10" AutoPostBack="true"
                                    Style="border-style: inset none none inset; margin-left: 0px;"></asp:TextBox>
                            </td>

                            <td style="height: 54px; font-weight: normal; width: 140px;">
                                <asp:Label ID="LabelPageTo" runat="server" Text="Page To" ForeColor="Black"></asp:Label>
                                <%-- <asp:Label ID="Labeljastr5" runat="server" Text="*" ForeColor="Red" Visible="false"></asp:Label>--%>
                            </td>
                        
                            <td style="height: 54px; width: 291px;">
                                <asp:TextBox ID="TextBoxPageTo" runat="server" Width="151px" MaxLength="10" Enabled="false" Style="border-style: inset none none inset;"></asp:TextBox>

                            </td>
                                </tr>
                        </table>
                    <table>
                        <tr>
                            <td style="height: 36px; font-weight: normal; width: 110px;">
                                <asp:Label ID="LabelVolume" runat="server" Text="Volume" ForeColor="Black"></asp:Label>
                            </td>
                            <td style="height: 36px; width: 208px;">
                                <asp:TextBox ID="TextBoxVolume" runat="server" Width="190px" Style="border-style: inset none none inset;"></asp:TextBox>


                            </td>
                            <td style="height: 36px; font-weight: normal; width: 89px;">
                                <asp:Label ID="Labelissue" runat="server" Text="Issue" ForeColor="Black"></asp:Label>
                            </td>

                            <td style="font-weight: normal; width: 186px;">
                                <asp:TextBox ID="TextBoxIssue" runat="server" Width="179px" Style="border-style: inset none none inset;"></asp:TextBox></td>
                         <td>
                             <asp:Label ID="lblQuartile" runat="server" Text="Quartile" ForeColor="Black" Visible="false"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtquartile" runat="server" ReadOnly="true" Width="100px" Visible="false" ></asp:TextBox>
                        </td>
                        </tr>
                         <tr>
                       
                        <td>
                             <asp:Label ID="lblQuartileid" runat="server" Text="QuartileID" ForeColor="Black" Visible="false"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtquartileid" runat="server" ReadOnly="true" Width="100px" Visible="false" ></asp:TextBox>
                        </td>
                    </tr>

                    </table>
                </asp:Panel>
                <br />
                <br />
                <br />
                <asp:Panel ID="Panel1" runat="server" GroupingText="Author Details" Enabled="false" Style="background-color: #E0EBAD;">

                    <center>
                        <asp:GridView runat="server" ID="Gridview" CssClass="grid" EmptyDataText="No Records Found" AutoGenerateColumns="False" Visible="False" DataSourceID="SqlDataSource2" BorderWidth="1px" CellPadding="3" DataKeyNames="MemberId" Width="748px">
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
                                   
                            <asp:TemplateField HeaderText="Sl.No">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                                <asp:BoundField DataField="MemberId" HeaderText="Employee Code/Roll No" HeaderStyle-Wrap="false" ItemStyle-Wrap="false" ItemStyle-Width="15%" SortExpression="MemberId" ControlStyle-BorderWidth="10" ReadOnly="True"></asp:BoundField>
                                <asp:BoundField DataField="MemberName" HeaderText="Member Name" SortExpression="MemberName" ControlStyle-BorderWidth="10" HeaderStyle-Wrap="false" ItemStyle-Wrap="false" ItemStyle-Width="60%"></asp:BoundField>
                                <asp:TemplateField HeaderText="isCorr Author" HeaderStyle-Width="70%">
                                    <ItemTemplate>

                                        <asp:DropDownList ID="isCorrAuth" runat="server" Width="75" Text='<%# Eval("isCorrAuth") %>'>
                                            <asp:ListItem Value="Y">Yes</asp:ListItem>
                                            <asp:ListItem Value="N" Selected="True">No</asp:ListItem>
                                        </asp:DropDownList>



                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Author Type">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="AuthorType" runat="server" Width="125" Text='<%# Eval("AuthorType") %>'>
                                            <asp:ListItem Value="P">First Author</asp:ListItem>
                                            <asp:ListItem Value="C" Selected="True">CO-Author</asp:ListItem>
                                        </asp:DropDownList>




                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Points" HeaderText="Point" SortExpression="Points" ControlStyle-BorderWidth="10" HeaderStyle-Wrap="false" ItemStyle-Wrap="false" ItemStyle-Width="30%"></asp:BoundField>
                            </Columns>
                        </asp:GridView>
                        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" SelectCommand="Incentive_ViewArticleWisePoints" SelectCommandType="StoredProcedure">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="txtPublicationId" Name="PublicationId" PropertyName="Text" Type="String" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                        <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" SelectCommand="Incentive_ViewArticleWisePointsforCancelledPublication" SelectCommandType="StoredProcedure">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="txtPublicationId" Name="PublicationId" PropertyName="Text" Type="String" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                    </center>
                    <br />
                    <br />
                </asp:Panel>
                <br />
                 <asp:Panel ID="PanelCancel" runat="server" GroupingText="CancelRemarks" Visible="false" ForeColor="Black" Style="background-color: #E0EBAD" >
                  <table>
                      <tr>
                          <td>
                              <asp:Label ID="Label" runat="server" Text="Remarks"></asp:Label>
                          </td>
                          <td>
                               <asp:TextBox ID="txtcancelRemarks" runat="server" Width="600px" TextMode="MultiLine" Enabled="false"></asp:TextBox>
                          </td>
                      </tr>
                  </table>
                  
                 
                   </asp:Panel>
            </asp:Panel>
            <br />
            <asp:Panel ID="Panel4" runat="server" Font-Bold="true" Style="background-color: #E0EBAD" ForeColor="Black" Visible="false">
                <br />
                <center>
                    <asp:Label ID="Label3" runat="server" Text="Patent-Wise Point View" Font-Bold="true"></asp:Label>
                </center>
                <br />
                <br />

                <center>
                    <table runat="server" id="table1" visible="true">
                        <tr>
                            <td>Patent Id:</td>
                            <td>
                                <asp:TextBox ID="txtpatentID" runat="server"></asp:TextBox></td>
                            <td>
                                <asp:Button runat="server" ID="BtnsearchPatent" Text="Search" OnClick="BtnsearchPatent_Click" /></td>
                        </tr>
                    </table>
                </center>
                <asp:Panel ID="PanelPatent" runat="server" GroupingText="Patent Details" Enabled="false" Style="background-color: #E0EBAD;" Visible="false">
              <table style="width:100%" >
                    <tr>
                        <td class="auto-style1">ID </td>
                        <td class="auto-style1">
                            <asp:TextBox ID="txtID" runat="server" Enabled="false"></asp:TextBox>
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
                            <asp:TextBox ID="txtdescription" runat="server" Width="514px"></asp:TextBox>
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
                <asp:Panel ID="Panel5" runat="server" GroupingText="Author Details" Enabled="false" Style="background-color: #E0EBAD;" Visible="false">

                    <center>
                        <asp:GridView runat="server" ID="Gridview1" CssClass="grid" EmptyDataText="No Records Found" AutoGenerateColumns="False" Visible="False" DataSourceID="SqlDataSource6" BorderWidth="1px" CellPadding="3" DataKeyNames="MemberId" Width="748px">
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
                                   
                            <asp:TemplateField HeaderText="Sl.No">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                                <asp:BoundField DataField="MemberId" HeaderText="Employee Code/Roll No" HeaderStyle-Wrap="false" ItemStyle-Wrap="false" ItemStyle-Width="15%" SortExpression="MemberId" ControlStyle-BorderWidth="10" ReadOnly="True"></asp:BoundField>
                                <asp:BoundField DataField="InvestigatorName" HeaderText="Member Name" SortExpression="InvestigatorName" ControlStyle-BorderWidth="10" HeaderStyle-Wrap="false" ItemStyle-Wrap="false" ItemStyle-Width="60%"></asp:BoundField>
                               <%-- <asp:TemplateField HeaderText="isCorr Author" HeaderStyle-Width="70%" Visible="false">
                                    <ItemTemplate>

                                        <asp:DropDownList ID="isCorrAuth" runat="server" Width="75" Visible="false" Text='<%# Eval("isCorrAuth") %>'>
                                            <asp:ListItem Value="Y">Yes</asp:ListItem>
                                            <asp:ListItem Value="N" Selected="True">No</asp:ListItem>
                                        </asp:DropDownList>



                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Author Type" Visible="false">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="AuthorType" runat="server" Width="125" Text='<%# Eval("AuthorType") %>'>
                                            <asp:ListItem Value="P">First Author</asp:ListItem>
                                            <asp:ListItem Value="C" Selected="True">CO-Author</asp:ListItem>
                                        </asp:DropDownList>




                                    </ItemTemplate>
                                </asp:TemplateField>--%>
                                <asp:BoundField DataField="Points" HeaderText="Point" SortExpression="Points" ControlStyle-BorderWidth="10" HeaderStyle-Wrap="false" ItemStyle-Wrap="false" ItemStyle-Width="30%"></asp:BoundField>


                            </Columns>
                        </asp:GridView>
                         <asp:SqlDataSource ID="SqlDataSource6" runat="server" ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" SelectCommand="SELECT O.ReferenceNumber,i.Title,p.InvestigatorName,i.Grant_Date,o.MemberId, sum(O.TotalPoint) as Points
  FROM [Member_Incentive_Point_Transaction] O 
,Patent_Data I ,Patent_Inventor P where (O.ReferenceNumber = I.ID) and (P.Id = I.ID and p.EmployeeCode=o.MemberId) and I.ID=@ID
GROUP BY O.ReferenceNumber,Title,Grant_Date,InvestigatorName,MemberId">
                             <SelectParameters>
                                <asp:ControlParameter ControlID="txtID" Name="ID" PropertyName="Text" Type="String" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                    </center>
                    <br />
                    <br />
                </asp:Panel>
            </asp:Panel>
            
            <br />
            <br />
        </ContentTemplate>
    </asp:UpdatePanel>
    <br />
    <br />
</asp:Content>

