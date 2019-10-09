<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPageRMS.master" AutoEventWireup="true" CodeFile="EmployeeDetailSearch.aspx.cs" Inherits="PublicationEntry_EmployeeDetailSearch" MaintainScrollPositionOnPostback="true" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <script type="text/javascript">

         function setRow(obj) {
             debugger;
             var row = obj.parentNode.parentNode;
             var rowIndex = row.rowIndex - 1;
             var mu = $(row).find("[id*=EmployeeCode]").val();
             alert($(row).find("[id*=EmployeeCode]").val());
          
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
            position: absolute;
            overflow: scroll;
            max-height: 600px;
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

        .highlight {
            background: none repeat scroll 0 0 #41C141;
            color: #FFFFFF;
            font-weight: bold;
            text-decoration: none;
        }

        .spaced input[type="radio"]
        {
            margin-left: 40px; 
        }
        .auto-style16 {
            width: 333px;
        }
        .spaced {}
    </style>

<asp:ScriptManager ID="Scriptmanager1" runat="server" />
     <center> <asp:Label ID="lblEmpDetailSearch" runat="server" Text="Employee Detail Search" Font-Bold="true"  ></asp:Label></center>
            <br />
<center>
        <table>
            <tr>
                <td class="auto-style16"> <asp:RadioButtonList ID="RadBtnListEmpDetails" runat="server"  RepeatDirection="Horizontal" CssClass="spaced" OnSelectedIndexChanged="RadBtnListEmpDetails_SelectedIndexChanged" AutoPostBack="true" Style="border-style:inset none none inset;" Width="394px">
                      <asp:ListItem Value="E" Selected="True">Employee ID</asp:ListItem>       
                      <asp:ListItem Value="O"  >ORCID</asp:ListItem>
                      <asp:ListItem Value="S">ScopusID</asp:ListItem>
                  </asp:RadioButtonList></td>
          
            <td >
                   <asp:TextBox ID="txtEmpDetails" runat="server" Width="141px" Height="18px" OnTextChanged="txtEmpDetails_TextChanged"></asp:TextBox>&nbsp;&nbsp;&nbsp;
                 <asp:Button ID="BtnSearch" runat="server" Text="Search" OnClick="BtnSearch_Click" Width="105px" Height="22px"  />
            </td>
              
            </tr>
           
        </table>
    </center>
    <br />
    <br />
     <asp:Panel ID="Panelauthor" runat="server" Visible="false">
                <table align="center">
                   <tr><td></td><td></td><td><b>Author Details</b></td></tr>
               
                
                </table></asp:Panel>
     <center>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" EmptyDataText="No Records Found" AllowPaging="true" OnPageIndexChanging="GridViewSearchPub_PageIndexChanging" PageSize="5" OnRowCommand="grdCustomPagging_RowCommand" Visible="false" Width="800px"
             HeaderStyle-BackColor="#CC6600" HeaderStyle-ForeColor="Black" PagerStyle-BackColor="#CC6600" PagerStyle-ForeColor="White" CellPadding="3"  CellSpacing="3" PagerStyle-Width="4" PagerStyle-Height="4"  BorderColor="#FF6600" BorderStyle="Solid">
            <Columns>

                <asp:TemplateField HeaderText="Employee Code">
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

                <asp:TemplateField HeaderText="Scopus ID">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="ScopusID" Text='<%# Eval("ScopusID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="ORC ID">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="ORCID" Text='<%# Eval("ORCID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                 <%--<asp:TemplateField HeaderText="Publication ID">
                    <ItemTemplate>
                        <%--<asp:Label runat="server" ID="PaublicationID" Text='<%# Eval("PaublicationID") %>'></asp:Label>
                         <%-- <asp:LinkButton ID="lnkPubId" runat="server" Text='<%# Eval("PaublicationID") %>'></asp:LinkButton>
                          <asp:Label ID="lblPubID" runat="server" Text='<%# Eval("PaublicationID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField HeaderText="TypeOfEntry">
                    <ItemTemplate>
                        <asp:Label runat="server" ID="TypeOfEntry" Text='<%# Eval("TypeOfEntry") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>--%>

                <asp:TemplateField ShowHeader="false">
                    <ItemTemplate>
                        <asp:HiddenField ID="EmployeeCode" runat="server" Value='<%# Eval("EmployeeCode") %>'></asp:HiddenField>
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
    <%--<asp:ModalPopupExtender ID="ModalPopupExtender1" PopupControlID="popup" TargetControlID="lnkbtn" BackgroundCssClass="modelBackground" runat="server"></asp:ModalPopupExtender>--%>

   <%-- <asp:Panel ID="popup" runat="server" Visible="false" Style="width: 1200px; height: 2000px; background-color: ghostwhite;">--%>
        <%--<asp:Panel ID="popupstudent" runat="server">--%>

           <%-- <center>
                <br />
                <table align="center">
                    <tr>

                        <td>
                            <asp:Button ID="Button3" runat="server" Text="EXIT" OnClick="exit" />
                        </td>
                    </tr>

                </table>
                <br />--%>
               
                <br />
    <asp:Panel ID="popupstudent" runat="server" Visible="false">
                <table align="center">
                   <tr><td></td><td></td><td><b>Publication Details</b></td></tr>
               
                   <%-- <tr>
                        <td>Author Name:</td>
                        <td>
                            <asp:Label runat="server" ID="txtauthorname"></asp:Label></td>
                        <td>&nbsp;</td>
                        <td>Employee Code:</td>
                        <td>
                            <asp:Label runat="server" ID="txtEmployeeCode"></asp:Label></td>
                    </tr>--%>
                </table></asp:Panel>
   
               <%-- <asp:Panel ID="Panel1" runat="server" ScrollBars="Vertical" Height="300px">--%>
                   <table align="center">
                        <tr>
                            <td> 
                                <asp:GridView ID="popupStudentGrid" runat="server" EmptyDataText="No Records Found" GridLines="Both" PagerStyle-Width="4" PagerStyle-Height="4" Width="800px"
                                    AllowSorting="true" AutoGenerateColumns="false"  CellPadding="3"  CellSpacing="3" DataSourceID="" Visible="false" PageSize="10" AllowPaging="true"
                                      HeaderStyle-BackColor="#CC6600" HeaderStyle-ForeColor="Black" PagerStyle-BackColor="#CC6600" PagerStyle-ForeColor="White"  
                                      BorderColor="#FF6600" BorderStyle="Solid">
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
                                <asp:SqlDataSource ID="StudentSQLDS" runat="server" ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>"
                                    SelectCommand="SelectIdBasedPublications" ProviderName="System.Data.SqlClient" SelectCommandType="StoredProcedure">
                                    <SelectParameters>
                                        <asp:SessionParameter Name="Employeecode" SessionField="Employeecode" Type="String" />
                                      <%--   <asp:SessionParameter Name="PublicationID" SessionField="PublicationID" Type="String" />--%>
                                    </SelectParameters>
                                </asp:SqlDataSource>
                            </td>
                        </tr>
                    </table>
              <%--  </asp:Panel>
            </center>
        </asp:Panel>--%>
        <br />
        <center>
           <asp:Panel ID="Panel1" runat="server" Visible="false">
                <table align="center">
                   <tr><td></td><td></td><td><b>Grant Details</b></td></tr>
                </table></asp:Panel>
            <table>
                <tr>
                    <td>
                        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1" EmptyDataText="No Records Found" Width="800px" Visible="false"  BorderColor="#FF6600" BorderStyle="Solid" PagerStyle-Width="4" PagerStyle-Height="4" 
                                    AllowSorting="true" CellPadding="3"  CellSpacing="3" PageSize="10" AllowPaging="true">
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
                                <asp:TemplateField HeaderText="Project Unit">
                                    <ItemTemplate>
                                        <asp:Label ID="lblProjectUnit" runat="server" Text='<%# Eval("ProjectUnit") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Project Type">
                                    <ItemTemplate>
                                        <asp:Label ID="lblProjectType" runat="server" Text='<%# Eval("TypeName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title" />
                            </Columns>
                               <PagerStyle BackColor="#0B532D" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#FFFFFF" ForeColor="#333333" />
            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
            <SortedAscendingCellStyle BackColor="#FDF5AC" />
            <SortedAscendingHeaderStyle BackColor="#4D0000" />
            <SortedDescendingCellStyle BackColor="#FCF6C0" />
            <SortedDescendingHeaderStyle BackColor="#820000" />
            <EditRowStyle BorderColor="#FF6600" BorderStyle="Solid" />
                        </asp:GridView>
                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" SelectCommand="SelectEmpCodeProjectDetails" SelectCommandType="StoredProcedure">
                            <SelectParameters>
                                <asp:SessionParameter DefaultValue="_" Name="Employeecode" SessionField="EmployeeCode" Type="String" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                    </td>
                </tr>
            </table>
        </center>
   <%-- </asp:Panel>--%>
    <asp:HiddenField ID="rowVal" runat="server" />
    <asp:HiddenField ID="senderID" runat="server" />
</asp:Content>

