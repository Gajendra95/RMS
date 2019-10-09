<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPageRMS.master" AutoEventWireup="true" CodeFile="DomainandResearchareaSearch.aspx.cs" Inherits="PublicationEntry_DomainandResearchareaSearch" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript">

       function setRow(obj) {
           debugger;
           var row = obj.parentNode.parentNode;
           var rowIndex = row.rowIndex - 1;
           var mu = $(row).find("[id*=EmployeeCode]").val();
           alert($(row).find("[id*=EmployeeCode]").val());
           '<%Session["EmployeeCode"] = "' + mu + '"; %>';
        }
    </script>
    <script type="text/javascript">

        function ViewPdf() {
            debugger;
            window.open('<%= Page.ResolveUrl("~/PublicationEntry/DisplayPdf.aspx")%>', '_blank');
        }
    </script>

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
  
          
            max-height: 600px;
            position:fixed;
        }

        .blnkImgCSS {
            opacity: 0;
            filter: alpha(opacity=0);
        }

        .auto-style8 {
            width: 152px;
        }

        .auto-style9 {
            height: 36px;
            width: 131px;
        }

        .auto-style10 {
            height: 36px;
            width: 136px;
        }

        .auto-style11 {
            height: 36px;
            width: 109px;
        }

        .auto-style13 {
            height: 39px;
            width: 129px;
        }

        .auto-style14 {
            height: 36px;
            width: 89px;
        }

        .auto-style15 {
            height: 39px;
        }
    </style>

    <style type="text/css">
        .textbox {
            padding: 1px 0 2px 6px;
            border: 1px solid #424242;
            border-radius: 5px;
            margin: 3px;
        }

        .btn {
            background-color: #252724;
            border: medium none;
            border-radius: 5px;
            color: #FFFFFF;
            padding: 2px;
            width: 59px;
        }

       
    </style>

     <asp:ScriptManager ID="Scriptmanager1" runat="server" />
     <center> <asp:Label ID="lblEmpDetailSearch" runat="server" Text="Domain and Area of Research Interest Search" Font-Bold="true"  ></asp:Label></center>
            <br />
    <center>
        <table>
            <tr>
                <td>Domain</td>
                <td>
                    <asp:TextBox ID="txtDomain" runat="server"></asp:TextBox></td>
                <td>&nbsp;</td>
                <td>Area of Research Interest</td>

               <td>
                   <asp:TextBox ID="txtResearchArea" runat="server"></asp:TextBox>

               </td>
                  <td>&nbsp;</td>
                <td>
                    <asp:Button ID="BtnSearch" runat="server" Text="Search" OnClick="BtnSearch_Click" /></td>
            </tr>
        </table>
    </center>
    
   <br />
    <br />
    <center>

<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" AllowPaging="true" PageSize="10" EmptyDataText="No Records Found" 
    OnPageIndexChanging="GridView1_PageIndexChanging" OnRowCommand="grdCustomPagging_RowCommand" >

<Columns>

                <asp:TemplateField HeaderText="Mahe ID">
                    <ItemTemplate>
                       
                           <asp:LinkButton ID="lnkRollNumber" runat="server" SortExpression="Rollno" CommandArgument='<%# Eval("EmployeeCode") %>' Text='<%# Eval("EmployeeCode") %>'
                            CausesValidation="false" OnClientClick="setRow(this)" OnClick="onclickHyperlinkRollNumber"
                            CommandName="VIEW">                
                        </asp:LinkButton>
                        
                    </ItemTemplate>
                </asp:TemplateField>
     <asp:TemplateField HeaderText="Author Name">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="AuthorName" Text='<%# Eval("AuthorName") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
     <asp:TemplateField HeaderText="Email Id">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="EmailId" Text='<%# Eval("EmailId") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Institution">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="Institute" Text='<%# Eval("Institution") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Department">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="Department" Text='<%# Eval("Department") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

        <asp:TemplateField HeaderText="ORC ID">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="ORCID" Text='<%# Eval("ORCID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Scopus ID1" >
                    <ItemTemplate>
                        <asp:Label runat="server" ID="ScopusID" Text='<%# Eval("ScopusID") %>' ></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
     <asp:TemplateField HeaderText="Scopus ID2" Visible="false">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="ScopusID2" Text='<%# Eval("ScopusID2") %>' Visible="false"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            
     <asp:TemplateField HeaderText="Scopus ID3" Visible="false">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="ScopusID3" Text='<%# Eval("ScopusID3") %>' Visible="false"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="false" Visible="false">
                    <ItemTemplate>
                        <asp:HiddenField ID="EmployeeCode" runat="server" Value='<%# Eval("EmployeeCode") %>' Visible="false"></asp:HiddenField>
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
      
</center>


    <div class="Pager">
    </div>
    <asp:LinkButton ID="lnkbtn" runat="server"></asp:LinkButton>
    <asp:ModalPopupExtender ID="ModalPopupExtender1" PopupControlID="popup" TargetControlID="lnkbtn" BackgroundCssClass="modelBackground" runat="server"></asp:ModalPopupExtender>

    <asp:Panel ID="popup" runat="server" Visible="false" CssClass="modelPopup" Style="width: 1000px; height: 2000px; background-color: ghostwhite;">
        <asp:Panel ID="popupstudent" runat="server">

            <center>
                <br />
                <table align="center">
                    <tr>

                        <td>
                            <asp:Button ID="Button3" runat="server" Text="EXIT" OnClick="Button3_Click" />
                        </td>
                    </tr>

                </table>
                <br />
                
                <asp:Panel ID="Panel3" runat="server"  GroupingText="User Details">
                 
                <table align="center">

                    <tr>
                        <td style="width:120px"><b>Author Name:</b></td>
                        <td>
                            <asp:Label runat="server" ID="txtauthorname"></asp:Label></td>
                      </tr>
                    <tr>
                        <td style="width:120px"><b>Employee Code:</b></td>
                        <td>
                            <asp:Label runat="server" ID="txtEmployeeCode"></asp:Label></td>
                    </tr>
                    
                    <tr>
                        <td style="width:120px">
                            <b>Domain:</b>
                        </td>
                        <td>
                            <asp:Label ID="textDomain" runat="server" ></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width:200px">
                            <b>
                                Area of Research Interest:
                            </b>
                        </td>
                        <td>
                            <asp:Label ID="textArea" runat="server" ></asp:Label>


                        </td>
                    </tr>
                    <tr>
                        <td style="width:120px">
                            <b>ORCID:</b>
                        </td>
                        <td>   <asp:Label ID="txtOrcid" runat="server" ></asp:Label>
                        </td>
                    </tr>
                    <tr style="width:120px">
                        <td>
                            <b>Scopus ID1:</b>
                          
                        </td>
                        <td>     <asp:Label ID="txtScopusid" runat="server" ></asp:Label> </td>
                    </tr>

                      <tr style="width:120px">
                        <td>
                            <b>Scopus ID2:</b>
                          
                        </td>
                        <td>     <asp:Label ID="txtScopusid2" runat="server" ></asp:Label> </td>
                    </tr>

                      <tr style="width:120px">
                        <td>
                            <b>Scopus ID3:</b>
                          
                        </td>
                        <td>     <asp:Label ID="txtScopusid3" runat="server" ></asp:Label> </td>
                    </tr>
                </table></asp:Panel>
                <br /><br />
                <table align="center">
                    <tr>
                        <th>Publication Details </th>
                    </tr>
                </table>
                <asp:Panel ID="Panel1" runat="server" ScrollBars="Vertical" Height="300px">
                    <table>
                        <tr>
                            <td>
                                <asp:GridView ID="popupStudentGrid" runat="server" EmptyDataText="No Records Found" GridLines="Both"
                                    AllowSorting="true" AutoGenerateColumns="false" CellPadding="5" CellSpacing="5" DataSourceID="StudentSQLDS" Width="100%">
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
                                        <asp:TemplateField HeaderText="Publication ID">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkPubId" runat="server" OnClick="Redirect" Text='<%# Eval("PublicationID ") %>'></asp:LinkButton>
                                                <asp:Label ID="lblPubID" runat="server" Text='<%# Eval("PublicationID ") %>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Entry Type" HeaderStyle-Width="200px">
                                            <ItemTemplate>
                                                <asp:Label ID="lblEntrytype" runat="server" Text='<%# Eval("TypeOfEntry") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Title"  HeaderStyle-Width="390px">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTitle" runat="server" Text='<%# Eval("TitleWorkItem") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="View PDF" HeaderStyle-Width="100px">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lblPdf" runat="server" OnClick="ViewPDF" Text="View"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="E-Print URL">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="lblEPrint" runat="server" Target="_blank" NavigateUrl='<%# Eval("EprintURL") %>'
                                                    Text='<%# Eval("EprintURL") %>'></asp:HyperLink>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                       <%-- <asp:TemplateField HeaderText="Keywords" HeaderStyle-Width="390px">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFIrstName" runat="server" Text='<%# Highlight(Eval("Keywords").ToString()) %>'>
                                                </asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                       
                                        <asp:TemplateField Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgetid" runat="server" Text='<%# Eval("UploadPDFPath") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <asp:SqlDataSource ID="StudentSQLDS" runat="server" ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>"
                                    SelectCommand="SelectIdBasedPublications" ProviderName="System.Data.SqlClient" SelectCommandType="StoredProcedure">
                                    <SelectParameters>
                                        <asp:SessionParameter Name="Employeecode" SessionField="Employeecode" Type="String" />
                                       <%-- <asp:SessionParameter Name="Keywords" SessionField="Keywords" Type="String" />--%>
                                    </SelectParameters>
                                </asp:SqlDataSource>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </center>
        </asp:Panel>
        <br />
        <center>
            <table align="center">
                <tr>
                    <th>Grant Details </th>
                </tr>
            </table>
            <asp:Panel ID="Panel2" runat="server" ScrollBars="Vertical" Height="300px">

            <table>
                
                <tr>
                    <td>
                        <asp:GridView ID="GridView2" runat="server" DataSourceID="SqlDataSource1" EmptyDataText="No Records Found" GridLines="Both"
                                    AllowSorting="true" AutoGenerateColumns="false" CellPadding="5" CellSpacing="5" Width="100%">
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
                                <asp:TemplateField HeaderText="ID">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lblID" runat="server" Text='<%# Eval("ID") %>' OnClick="RedirectProject"></asp:LinkButton>
                                        <asp:Label ID="lblProjectID" Visible="false" runat="server" Text='<%# Eval("ID") %>'></asp:Label>

                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Project Unit" HeaderStyle-Width="200px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblProjectUnit" runat="server" Text='<%# Eval("ProjectUnit") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Project Type" HeaderStyle-Width="200px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblProjectType" runat="server" Text='<%# Eval("TypeName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title"  HeaderStyle-Width="390px" />
                                 
                            </Columns>
                        </asp:GridView>
                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" SelectCommand="SelectKeywordProjectDetails" SelectCommandType="StoredProcedure">
                            <SelectParameters>
                                <asp:SessionParameter DefaultValue="_" Name="Employeecode" SessionField="EmployeeCode" Type="String" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                    </td>
                </tr>
            </table></asp:Panel>
        </center>
    </asp:Panel>
  <asp:HiddenField ID="rowVal" runat="server" />
    <asp:HiddenField ID="senderID" runat="server" />
</asp:Content>

