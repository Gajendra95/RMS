<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPageRMS.master" AutoEventWireup="true" CodeFile="ModifyFileUpload.aspx.cs" Inherits="PublicationEntry_ModifyFileUpload" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" DisplayMode="BulletList"
        HeaderText="Validation issues" ShowSummary="False" ValidationGroup="validation" />
  <style type="text/css">
             .auto-style8 {
          width: 141px;
      }
    </style>
    <center>

        <asp:ToolkitScriptManager ID="Scriptmanager1" runat="server" />

        <asp:Label ID="lablPanelTitle" runat="server" Text="Modify File Upload" Font-Bold="true"></asp:Label>
        <br />
        <br />
        <asp:Panel ID="panelSearchPub" runat="server" GroupingText="Search" Font-Bold="true" Style="background-color: #E0EBAD">
            <br />

            <table border="1" style="width: 100%">
                <tr>
                    <td style="height: 36px">
                        <b>Publication Entry Type:</b></td>
                    <td style="height: 36px">
                        <asp:DropDownList ID="EntryTypesearch" runat="server" Visible="true"
                            DataSourceID="SqlDataSource4" DataTextField="EntryName" DataValueField="TypeEntryId" AppendDataBoundItems="true">

                            <asp:ListItem Value="A">ALL</asp:ListItem>
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="SqlDataSource4" runat="server"
                            ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>"
                            SelectCommand="select TypeEntryId,EntryName from PublicationTypeEntry_M"
                            ProviderName="System.Data.SqlClient"></asp:SqlDataSource>

                    </td>

                    <td style="height: 36px">
                        <b>Publication ID </b>
                    </td>
                    <td style="height: 36px">
                        <asp:TextBox ID="PubIDSearch" runat="server" Width="164px"></asp:TextBox>
                    </td>
                    <td style="height: 36px; color: Black">Title
                    </td>
                    <td style="height: 36px">
                        <asp:TextBox ID="TextBoxWorkItemSearch" runat="server" Style="border-style: inset none none inset;" Width="234px"></asp:TextBox>
                    </td>
                    <td style="height: 36px">
                        <asp:Button ID="ButtonSearchPub" runat="server" Text="Search" OnClick="ButtonSearchPubOnClick" Height="30px" Width="109px" />
                    </td>

                </tr>

            </table>
        </asp:Panel>
        <br />


        <asp:GridView ID="GridViewSearch" runat="server" AllowPaging="True"
            PagerSettings-PageButtonCount="5" PageSize="5" EmptyDataText="No Data found"
            HeaderStyle-BackColor="#CC6600" HeaderStyle-ForeColor="White" AutoGenerateColumns="false" OnPageIndexChanging="GridViewSearchPub_PageIndexChanging"
            PagerStyle-BackColor="#CC6600" PagerStyle-ForeColor="White" CellPadding="3" OnRowDataBound="GridView2_RowDataBound" OnRowEditing="edit" OnRowCommand="GridView2_RowCommand"
            CellSpacing="3" PagerStyle-Width="4" PagerStyle-Height="4" Visible="False"
            BorderColor="#FF6600" BorderStyle="Solid">
            <Columns>
                <asp:TemplateField ShowHeader="False">
                    <ItemTemplate>
                        <asp:ImageButton ID="BtnEdit" runat="server" CausesValidation="False" CommandName="Edit" ImageUrl="~/Images/edit.gif" ToolTip="Edit" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="PublicationID" ReadOnly="true" HeaderText="PublicationID"
                    SortExpression="PublicationID" />
                <asp:BoundField DataField="EntryName" ReadOnly="true" HeaderText="Type Of Entry"
                    SortExpression="EntryName" />
                <asp:BoundField DataField="TitleWorkItem" ReadOnly="true" HeaderText="Title Of Work Item"
                    SortExpression="TitleWorkItem" />
                <asp:BoundField DataField="PubCatName" ReadOnly="true" HeaderText="Category"
                    SortExpression="PubCatName" />
                <asp:BoundField DataField="StatusName" ReadOnly="true" HeaderText="Status"
                    SortExpression="StatusName" />
                <asp:TemplateField ShowHeader="false">
                    <ItemTemplate>
                        <asp:HiddenField ID="TypeOfEntry" runat="server" Value='<%# Eval("TypeOfEntry") %>'></asp:HiddenField>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <HeaderStyle BackColor="#0b532d" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#0b532d" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#FFFFFF" ForeColor="#333333" />
            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
            <SortedAscendingCellStyle BackColor="#FDF5AC" />
            <SortedAscendingHeaderStyle BackColor="#4D0000" />
            <SortedDescendingCellStyle BackColor="#FCF6C0" />
            <SortedDescendingHeaderStyle BackColor="#820000" />
            <EditRowStyle BorderColor="#FF6600" BorderStyle="Solid" />
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server"
            ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>"
            SelectCommand=" "></asp:SqlDataSource>

        <br />
        <table width="100%">
            <tr>
                <td align="center">
                    <asp:Button ID="Buttonadd" runat="server" Text="Add Publication" OnClick="addclik" OnClientClick="AddConfirm()" Visible="false"></asp:Button>
                </td>
            </tr>
        </table>
        <br />

        <asp:Panel ID="panel" runat="server" GroupingText=" " Visible="false" BackColor="#E0EBAD">
            <table border="1" style="width: 100%">
                <tr>
                    <td style="height: 36px">
                        <asp:Label ID="TypeEntry" runat="server" Text="Entry Type:" Font-Bold="true"></asp:Label>

                    </td>
                    <td style="height: 36px" colspan="2">
                        <asp:DropDownList ID="DropDownListPublicationEntry" runat="server" DataSourceID="SqlDataSourcePublicationEntry" DataTextField="EntryName" Enabled="false"
                            DataValueField="TypeEntryId" AppendDataBoundItems="true" Width="200px" OnSelectedIndexChanged="DropDownListPublicationEntryOnSelectedIndexChanged" AutoPostBack="true">
                            <asp:ListItem Value=" ">--Select--</asp:ListItem>

                        </asp:DropDownList>
                        <asp:SqlDataSource ID="SqlDataSourcePublicationEntry" runat="server"
                            ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>"
                            SelectCommand="select TypeEntryId,EntryName from PublicationTypeEntry_M"
                            ProviderName="System.Data.SqlClient"></asp:SqlDataSource>

                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Please select Type Of Entry"
                            ValidationGroup="validation" ControlToValidate="DropDownListPublicationEntry" Display="None" InitialValue=" "></asp:RequiredFieldValidator>

                    </td>

                    <td style="height: 36px">
                        <asp:Label ID="lblMUCat" runat="server" Text="MAHE Categorization:" Font-Bold="true"></asp:Label>
                    </td>
                    <td style="height: 36px" colspan="2">
                        <asp:DropDownList ID="DropDownListMuCategory" Enabled="false" runat="server" Width="200px" DataSourceID="SqlDataSourceMuCategory" DataTextField="PubCatName"
                            DataValueField="PubCatId" AppendDataBoundItems="true">
                            <asp:ListItem Value=" ">--Select--</asp:ListItem>

                        </asp:DropDownList>

                        <asp:SqlDataSource ID="SqlDataSourceMuCategory" runat="server"
                            ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>"
                            SelectCommand="select PubCatId,PubCatName from PubMUCategorization_M"
                            ProviderName="System.Data.SqlClient"></asp:SqlDataSource>

                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please select MU Categorization"
                            ValidationGroup="validation" ControlToValidate="DropDownListMuCategory" Display="None" InitialValue=" "></asp:RequiredFieldValidator>
                    </td>

                    <td>
                        <asp:Label ID="LabelPubId" runat="server" Text="PublicationId :" Font-Bold="true"></asp:Label>
                    </td>

                    <td>
                        <asp:TextBox ID="TextBoxPubId" runat="server" Width="200px" Enabled="false"></asp:TextBox>

                    </td>

                </tr>
                <tr>
                    <td style="height: 36px">
                        <asp:Label ID="lblTitileWorkItem" runat="server" Text="Title Of the Work Item:" Font-Bold="true"></asp:Label></td>
                    <td style="height: 36px" colspan="7">
                        <asp:TextBox ID="txtboxTitleOfWrkItem" Enabled="false" runat="server" Width="900px" TextMode="MultiLine"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Please Enter Title Of Work Item"
                            ValidationGroup="validation" ControlToValidate="txtboxTitleOfWrkItem" Display="None"></asp:RequiredFieldValidator>

                    </td>
                </tr>
            </table>
            <br />

            <asp:Panel ID="panAddAuthor" runat="server" GroupingText="Author Details" Font-Bold="true" Visible="false">

                <br />

                <asp:SqlDataSource ID="SqlDataSourceAuthorType" runat="server"
                    ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>"
                    ProviderName="System.Data.SqlClient"></asp:SqlDataSource>


                <asp:Panel ID="PanelMU" runat="server" ScrollBars="Both" BorderStyle="Double" Width="1000px" Style="margin-right: 0px">

                    <asp:GridView ID="Grid_AuthorEntry" runat="server" AutoGenerateColumns="False" GridLines="None" Enabled="false"
                        OnRowDataBound="OnRowDataBound" CellPadding="4" ForeColor="#333333">

                        <AlternatingRowStyle BackColor="White" />

                        <Columns>



                            <asp:TemplateField HeaderText="MAHE/Non MAHE">
                                <ItemTemplate>
                                    <asp:DropDownList ID="DropdownMuNonMu" runat="server" Width="160" OnSelectedIndexChanged="DropdownMuNonMuOnSelectedIndexChanged" AutoPostBack="true" DataSourceID="SqlDataSourceAuthorType">
                                    </asp:DropDownList>

                                    <asp:ImageButton ID="MuNonMu" runat="server" ImageUrl="~/Images/srchImg.gif" CssClass="blnkImgCSS" />



                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ShowHeader="true" HeaderText="Roll No/Employee Code">
                                <ItemTemplate>
                                    <asp:TextBox ID="EmployeeCode" runat="server" Width="155" Visible="true"></asp:TextBox>
                                    <asp:Image ID="ImageEmpCode" runat="server" ImageUrl="~/Images/srchImg.gif" CssClass="blnkImgCSS" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Author Name">
                                <ItemTemplate>
                                    <asp:TextBox ID="AuthorName" runat="server" Width="200" Enabled="false"></asp:TextBox>

                                    <asp:Image ID="Image4name" runat="server" ImageUrl="~/Images/srchImg.gif" CssClass="blnkImgCSS" />

                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Institution Name">
                                <ItemTemplate>
                                    <asp:TextBox ID="InstitutionName" runat="server" Width="200" Enabled="false"></asp:TextBox>

                                    <asp:Image ID="Image4" runat="server" ImageUrl="~/Images/srchImg.gif" CssClass="blnkImgCSS" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Department Name/Course">
                                <ItemTemplate>
                                    <asp:TextBox ID="DepartmentName" runat="server" Width="200" Enabled="false"></asp:TextBox>

                                    <asp:Image ID="Image5" runat="server" ImageUrl="~/Images/srchImg.gif" CssClass="blnkImgCSS" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="MailId">
                                <ItemTemplate>
                                    <asp:TextBox ID="MailId" runat="server" Width="200" Enabled="false"></asp:TextBox>

                                    <asp:Image ID="ImageMailId" runat="server" ImageUrl="~/Images/srchImg.gif" CssClass="blnkImgCSS" />


                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="isCorrAuth">
                                <ItemTemplate>
                                    <asp:DropDownList ID="isCorrAuth" runat="server" Width="75">
                                        <asp:ListItem Value="Y">Yes</asp:ListItem>
                                        <asp:ListItem Value="N">No</asp:ListItem>
                                    </asp:DropDownList>

                                    <asp:Image ID="ImageisCorrAuth" runat="server" ImageUrl="~/Images/srchImg.gif" CssClass="blnkImgCSS" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="AuthorType">
                                <ItemTemplate>
                                    <asp:DropDownList ID="AuthorType" runat="server" Width="125">
                                          <asp:ListItem Value="0" Selected="True">-Select-</asp:ListItem>
                                        <asp:ListItem Value="P">First Author</asp:ListItem>
                                        <asp:ListItem Value="C">CO-Author</asp:ListItem>
                                    </asp:DropDownList>

                                    <asp:Image ID="ImageisAuthorType" runat="server" ImageUrl="~/Images/srchImg.gif" CssClass="blnkImgCSS" />

                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="NameAsInJournal">
                                <ItemTemplate>
                                    <asp:TextBox ID="NameInJournal" runat="server" Width="190"></asp:TextBox>


                                    <asp:Image ID="ImageNameInJournal" runat="server" ImageUrl="~/Images/srchImg.gif" CssClass="blnkImgCSS" />

                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="IsPresenter">
                                <ItemTemplate>
                                    <asp:DropDownList ID="IsPresenter" runat="server" Width="75">
                                        <asp:ListItem Value="N">No</asp:ListItem>
                                        <asp:ListItem Value="Y">Yes</asp:ListItem>

                                    </asp:DropDownList>

                                    <asp:Image ID="ImageIsPresenter" runat="server" ImageUrl="~/Images/srchImg.gif" CssClass="blnkImgCSS" />

                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="HasAttented">
                                <ItemTemplate>
                                    <asp:CheckBox ID="HasAttented" runat="server" Width="70"></asp:CheckBox>

                                    <asp:Image ID="ImageHasAttented" runat="server" ImageUrl="~/Images/srchImg.gif" CssClass="blnkImgCSS" />
                                </ItemTemplate>
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Colloboration">
                                <ItemTemplate>
                                    <asp:DropDownList ID="NationalType" runat="server" Width="140">
                                        <asp:ListItem Value="N">National</asp:ListItem>
                                        <asp:ListItem Value="I">International</asp:ListItem>
                                    </asp:DropDownList>

                                    <asp:Image ID="ImageisNationalType" runat="server" ImageUrl="~/Images/srchImg.gif" CssClass="blnkImgCSS" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Continent">
                                <ItemTemplate>
                                    <asp:DropDownList ID="ContinentId" runat="server" Width="140" DataSourceID="SqlDataSourceDropdownContinentId" DataTextField="ContinentName" DataValueField="ContinentId">
                                    </asp:DropDownList>
                                    <asp:SqlDataSource ID="SqlDataSourceDropdownContinentId" runat="server"
                                        ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>"
                                        ProviderName="System.Data.SqlClient"
                                        SelectCommand="select ContinentId,ContinentName  from Continent_M "></asp:SqlDataSource>
                                    <asp:Image ID="ImageisContinent" runat="server" ImageUrl="~/Images/srchImg.gif" CssClass="blnkImgCSS" />

                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField ShowHeader="false">
                                <ItemTemplate>
                                    <asp:HiddenField ID="Institution" runat="server"></asp:HiddenField>

                                    <asp:ImageButton ID="InstitutionBtn" runat="server" ImageUrl="~/Images/srchImg.gif" CssClass="blnkImgCSS" />


                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ShowHeader="false">
                                <ItemTemplate>
                                    <asp:HiddenField ID="Department" runat="server"></asp:HiddenField>

                                    <asp:ImageButton ID="DepartmentBtn" runat="server" Enabled="false" ImageUrl="~/Images/srchImg.gif" CssClass="blnkImgCSS" />


                                </ItemTemplate>
                            </asp:TemplateField>


                            <asp:CommandField DeleteText="Remove" ShowDeleteButton="True" />

                        </Columns>
                        <HeaderStyle BackColor="#0b532d" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#000000" ForeColor="#000000" HorizontalAlign="Center" />
                        <RowStyle BackColor="#FFFFFF" ForeColor="#333333" />
                        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                        <SortedAscendingCellStyle BackColor="#FDF5AC" />
                        <SortedAscendingHeaderStyle BackColor="#4D0000" />
                        <SortedDescendingCellStyle BackColor="#FCF6C0" />
                        <SortedDescendingHeaderStyle BackColor="#820000" />
                    </asp:GridView>


                </asp:Panel>
            </asp:Panel>


            <br />


            <asp:Panel ID="panelJournalArticle" runat="server" Enabled="true" GroupingText="Journal Publish Details" Visible="false" ForeColor="Black">

                <br />
                <table style="width: 100%">

                    <tr>
                        <td style="height: 36px; font-weight: normal">
                            <asp:Label ID="LabelPubJournal" runat="server" Text="ISSN" ForeColor="Black"></asp:Label>

                        </td>
                        <td style="height: 36px">
                            <asp:TextBox ID="TextBoxPubJournal" ReadOnly="true" runat="server" Width="180px" OnTextChanged="JournalIDTextChanged" AutoPostBack="true" Style="border-style: inset none none inset;"></asp:TextBox>

                            <asp:ImageButton ID="imageBkCbtn" runat="server" ImageUrl="~/Images/srchImg.gif" OnClientClick="setRow(this)" />

                            <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="imageBkCbtn" PopupControlID="popupPanelJournal"
                                BackgroundCssClass="modelBackground">
                            </asp:ModalPopupExtender>

                        </td>


                        <td style="height: 36px; font-weight: normal">
                            <asp:Label ID="LabelNameJournal" runat="server" Text="Name Of Journal" ForeColor="Black"></asp:Label>
                        </td>
                        <td style="height: 36px">
                            <asp:TextBox ID="TextBoxNameJournal" runat="server" ReadOnly="true" Width="250px" Style="border-style: inset none none inset;"></asp:TextBox>
                        </td>


                        <td>IF Applicable Year:

                        </td>
                        <td>
                            <asp:TextBox ID="txtIFApplicable" runat="server" ReadOnly="true" Width="60px" Style="border-style: inset none none inset;"></asp:TextBox></td>

                        <td style="font-weight:normal" class="auto-style35">
                             <asp:Label ID="lblQuartile" runat="server" Text="Quartile" ForeColor="Black" ></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtquartile" runat="server" ReadOnly="true" Width="100px" Style="border-style:inset none none inset;" ></asp:TextBox>
                        </td>
                        <td  style="font-weight:normal" class="auto-style35">
                             <asp:Label ID="lblQuartileid" runat="server" Text="QuartileID" ForeColor="Black" Visible="false"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtquartileid" runat="server" ReadOnly="true" Width="100px" Visible="false" Style="border-style:inset none none inset;"></asp:TextBox>
                        </td>




                       
                    </tr>
                </table>

                <table>
                    <tr>


                        <td style="height: 56px; font-weight: normal">
                            <asp:Label ID="LabelMonthJA" runat="server" Text="Publish Month" ForeColor="Black"></asp:Label>
                            <asp:DropDownList ID="DropDownListMonthJA" Enabled="false" runat="server" Style="border-style: inset none none inset;">
                                <asp:ListItem Value="1">Jan</asp:ListItem>
                                <asp:ListItem Value="2">Feb</asp:ListItem>
                                <asp:ListItem Value="3">Mar</asp:ListItem>
                                <asp:ListItem Value="4">Apr</asp:ListItem>
                                <asp:ListItem Value="5">May</asp:ListItem>

                                <asp:ListItem Value="6">June</asp:ListItem>

                                <asp:ListItem Value="7">July</asp:ListItem>
                                <asp:ListItem Value="8">Aug</asp:ListItem>
                                <asp:ListItem Value="9">Sep</asp:ListItem>
                                <asp:ListItem Value="10">Oct</asp:ListItem>
                                <asp:ListItem Value="11">Nov</asp:ListItem>
                                <asp:ListItem Value="12">Dec</asp:ListItem>


                            </asp:DropDownList>

                        </td>
                        <td style="height: 56px; font-weight: normal">
                            <asp:Label ID="LabelYearJA" runat="server" Text="Publish Year" ForeColor="Black"></asp:Label>
                            <asp:TextBox ID="TextBoxYearJA" runat="server" Enabled="false"
                                Width="80px" Style="border-style: inset none none inset;">
        


                            </asp:TextBox>


                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" Display="None" ControlToValidate="TextBoxYearJA" ValidationGroup="validation"
                                ErrorMessage="Entered Proper Year" SetFocusOnError="true"
                                ValidationExpression="^[0-9]{4}$">
                            </asp:RegularExpressionValidator>

                        </td>
                        <td style="height: 56px; font-weight: normal">
                            <asp:Label ID="LabelImpFact" runat="server" Text="1-Year Impact Factor" ForeColor="Black"></asp:Label></td>
                        <td>
                            <asp:TextBox ID="TextBoxImpFact" runat="server" ReadOnly="true" Width="85px" Style="border-style: inset none none inset;"></asp:TextBox>

                        </td>
                        <td style="height: 56px; font-weight: normal">
                            <asp:Label ID="LabelImpFact5" runat="server" Text="5-Year Impact Factor" ForeColor="Black"></asp:Label></td>
                        <td>
                            <asp:TextBox ID="TextBoxImpFact5" runat="server" ReadOnly="true" Style="border-style: inset none none inset;" Width="85px"></asp:TextBox>

                        </td>

                        <td style="height: 36px; font-weight: normal">
                            <asp:Label ID="LabelPageFrom" runat="server" Text="Page From" ForeColor="Black"></asp:Label>
                        </td>

                        <td>
                            <asp:TextBox ID="TextBoxPageFrom" ReadOnly="true" runat="server" Width="50px" MaxLength="10" Style="border-style: inset none none inset;"></asp:TextBox>
                        </td>

                        <td style="height: 36px; font-weight: normal">
                            <asp:Label ID="LabelPageTo" runat="server" Text="Page To" ForeColor="Black"></asp:Label>
                        </td>
                        <td style="height: 36px">
                            <asp:TextBox ID="TextBoxPageTo" ReadOnly="true" runat="server" Width="50px" MaxLength="10" Style="border-style: inset none none inset;"></asp:TextBox>

                        </td>


                    </tr>
                </table>
                <table>

                    <tr>

                        <td style="height: 36px; font-weight: normal">
                            <asp:Label ID="LabelVolume" runat="server" Text="Volume" ForeColor="Black"></asp:Label></td>
                        <td style="height: 36px">
                            <asp:TextBox ID="TextBoxVolume" ReadOnly="true" runat="server" Width="140px" Style="border-style: inset none none inset;"></asp:TextBox>


                        </td>
                        <td style="height: 36px; font-weight: normal">
                            <asp:Label ID="Labelissue" runat="server" Text="Issue" ForeColor="Black"></asp:Label>
                        </td>

                        <td style="font-weight: normal">
                            <asp:TextBox ID="TextBoxIssue" ReadOnly="true" runat="server" Width="150px" Style="border-style: inset none none inset;"></asp:TextBox></td>

                        <td style="height: 36px; font-weight: normal">
                            <asp:Label ID="LabelDOINum" runat="server" Text="DOI Number" ForeColor="Black"></asp:Label>

                        </td>
                        <td style="height: 36px">
                            <asp:TextBox ID="TextBoxDOINum" ReadOnly="true" runat="server" Width="150px" MaxLength="20" Style="border-style: inset none none inset;"></asp:TextBox>



                        </td>




                        <td style="height: 36px; font-weight: normal">
                            <asp:Label ID="LabelIndexed" runat="server" Text="Indexed:" ForeColor="Black"></asp:Label></td>
                        <td style="font-weight: normal">
                            <asp:RadioButtonList ID="RadioButtonListIndexed" runat="server" Enabled="false" RepeatDirection="Horizontal" OnSelectedIndexChanged="RadioButtonListIndexedOnSelectedIndexChanged" AutoPostBack="true" Style="border-style: inset none none inset;">
                                <asp:ListItem Value="N" Selected="True">No</asp:ListItem>
                                <asp:ListItem Value="Y">Yes</asp:ListItem>


                            </asp:RadioButtonList>

                        </td>
                        <td style="height: 36px; font-weight: normal">


                            <asp:Label ID="LabelIndexedIn" runat="server" Text="Indexed In" ForeColor="Black"></asp:Label></td>
                        <td style="font-weight: normal">
                            <asp:CheckBoxList ID="CheckboxIndexAgency" runat="server" ForeColor="Black" DataSourceID="SqlDataSourceCheckboxIndexAgency" RepeatDirection="Horizontal" DataTextField="agencyname" DataValueField="agencyid" Enabled="false">
                            </asp:CheckBoxList>


                            <asp:SqlDataSource ID="SqlDataSourceCheckboxIndexAgency" runat="server" ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>"
                                SelectCommand="select agencyid,agencyname from IndexAgency_M where active='Y'"></asp:SqlDataSource>

                        </td>

                    </tr>
                    <tr>
                         <td style="height: 56px; font-weight: normal">
                            <asp:Label ID="LabelPubicationType" runat="server" Text="Publication Type" ForeColor="Black"></asp:Label></td>
                        <td>
                            <asp:DropDownList ID="DropDownListPubType" Enabled="false" runat="server" Style="border-style: inset none none inset;">
                                <asp:ListItem Value="N">National</asp:ListItem>
                                <asp:ListItem Value="I">International</asp:ListItem>
                            </asp:DropDownList>

                        </td>
                    </tr>
                </table>
                <%--                <table style="margin-left: -350px">
                    <tr>
                        <td style="height: 56px; font-weight: normal">
                            <asp:Label ID="lblCitation" runat="server" Text="Citation URL" ForeColor="Black"></asp:Label></td>

                        <td style="font-weight: normal">
                            <asp:TextBox ID="txtCitation" runat="server" MaxLength="200" Width="580px" Style="border-style: inset none none inset;"></asp:TextBox></td>

                    </tr>
                </table>--%>
                  </asp:Panel>
                <asp:Panel ID="panelConfPaper" runat="server" GroupingText=" Conference Paper" Visible="false">


                    <br />
                    <table width="100%">
                        <tr>
                            <td style="height: 36px; font-weight: normal">
                                <asp:Label ID="LabelEventTitle" runat="server" Text="Conference Title"></asp:Label>

                            </td>
                            <td style="height: 36px">
                                <asp:TextBox ID="TextBoxEventTitle" runat="server" Width="700px" MaxLength="200" Style="border-style: inset none none inset;"></asp:TextBox>



                            </td>


                            <td style="height: 36px; font-weight: normal">
                                <asp:Label ID="LabelDate" runat="server" Text="Conference Date"></asp:Label></td>
                            <td style="height: 36px">
                                <asp:TextBox ID="TextBoxDate" runat="server" Width="130px" Style="border-style: inset none none inset;"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender2" runat="server"
                                    TargetControlID="TextBoxDate" Format="dd/MM/yyyy">
                                </asp:CalendarExtender>

                                <asp:RegularExpressionValidator ID="RegularExpressionValidator15" runat="server" Display="None" ControlToValidate="TextBoxDate" ValidationGroup="validation"
                                    ErrorMessage="Presentation date in (dd/mm/yyyy) format" SetFocusOnError="true"
                                    ValidationExpression="^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$">
                                </asp:RegularExpressionValidator>
                            </td>
                        </tr>

                    </table>

                    <table width="100%">

                        <tr>
                            <td style="height: 36px; font-weight: normal;width: 93px;">
                                <asp:Label ID="LabelISBN" runat="server" Text="ISBN"></asp:Label>
                            </td>
                            <td style="height: 36px">
                                <asp:TextBox ID="TextBoxIsbn" runat="server" Width="450px" MaxLength="50" Style="border-style: inset none none inset;"></asp:TextBox>
                            </td>

                            <td style="height: 36px; font-weight: normal">
                                <asp:Label ID="LabelPlace" runat="server" Text="Conference Venue"></asp:Label>
                            </td>
                            <td style="height: 36px">
                                <asp:TextBox ID="TextBoxPlace" runat="server" Width="400px" MaxLength="50" Style="border-style: inset none none inset;"></asp:TextBox>
                            </td>



                        </tr>
                    </table>
                      <table width="100%">
          <tr>
              
                                 <td style="height: 36px; font-weight:normal;width: 92px;">
                           <asp:Label ID="lblCity" runat="server" Text="City" ></asp:Label>
                        </td>
                        <td style="height: 36px">
                             <asp:TextBox ID="txtcity" runat="server"  Width="245px" MaxLength="50" Style="border-style:inset none none inset;"></asp:TextBox>
         </td>
              <td class="auto-style38"></td>
                                 <td style="height: 36px; font-weight:normal">
                           <asp:Label ID="lblState" runat="server" Text="State/Country" ></asp:Label>
                        </td>
                        <td style="height: 36px">
                             <asp:TextBox ID="txtState" runat="server"  Width="245px" MaxLength="50" Style="border-style:inset none none inset;"></asp:TextBox>
         </td>
              <td class="auto-style38"></td>
               <td style="height: 36px; font-weight:normal">
                  <asp:Label ID="LabelConferencetype" runat="server" Text="Type Of Conference" ForeColor="Black"  ></asp:Label>
              </td>
              <td style="height: 36px; font-weight:normal">
                   <asp:DropDownList ID="DropDownListConferencetype" runat="server" Width="245px" DataTextField="Type" DataValueField="Id" Style="border-style:inset none none inset;" >
<asp:ListItem Value="0">--Select--</asp:ListItem>
        <asp:ListItem Value="N">National</asp:ListItem>
        <asp:ListItem Value="I">International</asp:ListItem>
     </asp:DropDownList>
     
       <%-- <asp:SqlDataSource ID="SqlDataSourceDropDownListConferencetype" runat="server" ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" 
           SelectCommand="select Id,Type from Conference_Type_M">
     
                </asp:SqlDataSource>     --%>                   
                  </td>
          </tr>
      </table>
             <table width="100%">
             <tr>
                   <td style="height: 56px; font-weight:normal;width: 95px;" >
                             <asp:Label ID="Label18" runat="server" Text="Publish Month" ForeColor="Black"   ></asp:Label>
                                  <asp:Label ID="Label19" runat="server" Text="*" ForeColor="Red" Visible="false"></asp:Label></td>
                                  <td>
                              <asp:DropDownList ID="DropDownListMonthCP" runat="server" Style="border-style:inset none none inset;" DataSourceID="SqlDataSourceCPPubJAmonth"
      DataTextField="MonthName" DataValueField="MonthValue"  Width="80px" AutoPostBack="true" AppendDataBoundItems="true">
      <asp:ListItem Value="0">-select-</asp:ListItem>

       
        </asp:DropDownList>   
          <asp:SqlDataSource ID="SqlDataSourceCPPubJAmonth" runat="server" ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" 
           SelectCommand="select MonthValue, MonthName from Publication_MonthM">
     
                </asp:SqlDataSource>
                        </td>
                   <td class="auto-style38"></td>
                        <td style="height: 56px; font-weight:normal" >
                     <asp:Label ID="Label20" runat="server" Text="Publish Year" ForeColor="Black"  ></asp:Label>
                          <asp:Label ID="Label21" runat="server" Text="*" ForeColor="Red" Visible="false" ></asp:Label></td><td>
                          <%--    <asp:DropDownList ID="TextBoxYearJA" runat="server" 
        Width="60px"  Style="border-style:inset none none inset;" AutoPostBack="true">
        </asp:DropDownList>  
                           --%>
           <asp:TextBox ID="TextBoxYearCP" runat="server"  Width="100px" MaxLength="10" AutoPostBack="true"
                 Style="border-style:inset none none inset;" ></asp:TextBox> 
           <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" Display="None"  ControlToValidate="TextBoxYearCP" ValidationGroup="validation"
                ErrorMessage="Entered Proper Year" SetFocusOnError="true"  
                ValidationExpression="^[0-9]{4}$">
                </asp:RegularExpressionValidator> 
        </td>
                  <td class="auto-style38"></td>
                  <td style="font-weight:normal" class="auto-style32">  
                  <asp:Label ID="Label22" runat="server" Text="Page From" ForeColor="Black"  ></asp:Label>
                       <%--<asp:Label ID="Labeljastr4" runat="server" Text="*" ForeColor="Red" Visible="false"></asp:Label>--%>
                        </td>
                         
         <td>
      <asp:TextBox ID="TextBoxPageFromCP" runat="server"  Width="100px" MaxLength="10" AutoPostBack="true"
                 Style="border-style:inset none none inset;" 
                 ontextchanged="TextBoxPageFromCP_TextChanged"></asp:TextBox> 
   </td>
     <td class="auto-style38"></td>
                                <td style="font-weight:normal" class="auto-style32">
                    <asp:Label ID="Label23" runat="server" Text="Page To" ForeColor="Black"  ></asp:Label>
                        <%-- <asp:Label ID="Labeljastr5" runat="server" Text="*" ForeColor="Red" Visible="false"></asp:Label>--%>
                        </td>
                        <td style="height: 36px" >
                         <asp:TextBox ID="TextBoxPageToCP" runat="server"   Width="100px" MaxLength="10" Enabled="false" Style="border-style:inset none none inset;"></asp:TextBox>
      
            </td>
                 <td>
                  <%--       <asp:DropDownList ID="TextBoxYear" runat="server" Style="border-style:inset none none inset;"
                             Width="100px" >

                          </asp:DropDownList> --%>
                 </td>
             </tr>
         </table>
      <table width="100%">
          <tr>
             
        <td style="height: 36px; font-weight:normal;width: 100px;"> <asp:Label ID="LblCPIndexed" runat="server" Text="Indexed:" ForeColor="Black"  ></asp:Label></td>
     <td style="font-weight:normal" >   <asp:RadioButtonList ID="RadioButtonListCPIndexed" runat="server"    RepeatDirection="Horizontal" OnSelectedIndexChanged="RadioButtonListCPIndexedOnSelectedIndexChanged" AutoPostBack="true" Style="border-style:inset none none inset;">
                                        <asp:ListItem Value="N" >No</asp:ListItem>
                             <asp:ListItem Value="Y" Selected="True">Yes</asp:ListItem>
                  
 
                             </asp:RadioButtonList>  
     
     </td> 
     <td class="auto-style22"></td>
  <td class="auto-style38"></td>
  
   <td style="height: 36px; font-weight:normal">
    

     <asp:Label ID="Label15" runat="server" Text="Indexed In:" ForeColor="Black"  ></asp:Label></td>
     <td  style="font-weight:normal" >
  <asp:CheckBoxList ID="CheckBoxListCPIndexedIn" runat="server" ForeColor="Black"  DataSourceID="SqlDataSourceCheckboxCPIndexAgency" RepeatDirection="Horizontal" DataTextField="agencyname" DataValueField="agencyid" Enabled="false">
                
                </asp:CheckBoxList>

                
<asp:SqlDataSource ID="SqlDataSourceCheckboxCPIndexAgency" runat="server" ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" 
           SelectCommand="select agencyid,agencyname from IndexAgency_M where active='Y'">
     
                </asp:SqlDataSource>

     </td>
      <td class="auto-style38"></td>
          <td style="height: 36px; font-weight: normal">
                                <asp:Label ID="Label2Presentation" runat="server" Text="Type Of Presentation"></asp:Label>

                            </td>
                            <td style="height: 36px; font-weight: normal">
                                <asp:RadioButtonList ID="RadioButtonListTypePresentaion" runat="server" RepeatDirection="Horizontal" Style="border-style: inset none none inset;">
                                    <asp:ListItem Value="P">Poster</asp:ListItem>
                                    <asp:ListItem Value="O">Oral  </asp:ListItem>
                                    <asp:ListItem Value="B">Both</asp:ListItem>
                                </asp:RadioButtonList>



                            </td>

             </tr>
             </table>
                    <table width="100%">

                        <tr>
                         




                            <td style="height: 36px; font-weight: normal">
                                <asp:Label ID="Label3" runat="server" Text="Credit Point"></asp:Label>
                            </td>
                            <td style="height: 36px">
                                <asp:TextBox ID="TextBoxCreditPoint" runat="server" Text="0" Style="border-style: inset none none inset;"></asp:TextBox>
                            </td>

                            <td style="height: 36px; font-weight: normal">
                                <asp:Label ID="Label4" runat="server" Text="Awarded By"></asp:Label></td>
                            <td style="height: 36px">
                                <asp:TextBox ID="TextBoxAwardedBy" runat="server" Enabled="false" Width="400px" Style="border-style: inset none none inset;"></asp:TextBox>

                            </td>
                             <td style="height: 36px; font-weight:normal;width: 125px;">
                           <asp:Label ID="LabelFunds" runat="server" Text="Funds Utilized" ></asp:Label>
                        </td>
                        <td style="font-weight:normal" class="auto-style53">
                             <asp:TextBox ID="TextBoxFunds" runat="server"  Width="150px" MaxLength="50" Style="border-style:inset none none inset;"></asp:TextBox>
         </td>
                
                        </tr>




                    </table>


                </asp:Panel>





                <asp:Panel ID="panelBookPublish" runat="server" GroupingText="Book Publish" Visible="false" Font-Bold="true">

                     <table  style="width: 100%" cellspacing="5">
                    <tr>
                        <td style="height: 36px; font-weight:normal">
                          <asp:Label ID="LabelTitileBook" runat="server" Text="Title Of The Book" ></asp:Label>
                             <asp:Label ID="Labelstr" runat="server" Text="*"  ForeColor="Red"></asp:Label>   
                               </td>
                        <td style="height: 36px">             
                             <asp:TextBox ID="TextBoxTitleBook" runat="server"  Width="880px" MaxLength="100"  Rows="2" TextMode="MultiLine" Style="border-style:inset none none inset;"></asp:TextBox>
     
                 
                       
                         </td>
                      
                    
                           </tr>
                     
                        <tr>
                      <td style="height: 36px; font-weight:normal">
                           <asp:Label ID="LabelTitleChapterContributed" runat="server" Text="Title Of the Chapter Contributed" ></asp:Label>
                                    <asp:Label ID="Label2str" runat="server" Text="*"  ForeColor="Red"></asp:Label>  
                        </td>
                        <td style="height: 36px">
                             <asp:TextBox ID="TextBoxChapterContributed" runat="server"   Width="880px" MaxLength="100" Rows="2" TextMode="MultiLine" Style="border-style:inset none none inset;"></asp:TextBox>
         </td>
         </tr>
         </table>
     
         <table style="width: 100%" >
         <tr>
         

                 <td style="font-weight:normal" class="auto-style8"  >
                                <asp:Label ID="LabelEdition" runat="server" Text="Edition" ></asp:Label>
                                         <asp:Label ID="Label5" runat="server" Text="*"  ForeColor="Red"></asp:Label>  
                                </td>
                        <td colspan="3">             
                       <asp:TextBox ID="TextBoxEdition" runat="server"   Width="304px" MaxLength="50"  Style="border-style:inset none none inset;"></asp:TextBox>
     
                       
                         </td>
              
                       <td style="width:20px"></td>
                   

                           
                                <td style="font-weight:normal">
                             <asp:Label ID="LabelPublisher" runat="server" Text="Publisher" ></asp:Label>
                                      <asp:Label ID="Label7" runat="server" Text="*"  ForeColor="Red"></asp:Label>  
                        </td>
                        <td   colspan="3">
                     <asp:TextBox ID="TextBoxPublisher" MaxLength="50" Width="308px"  runat="server" Style="border-style:inset none none inset;"
     ></asp:TextBox>   
        
                        </td>
             </tr>
       </table>
      <table style="width: 100%" >
          <tr>
               <td style="font-weight:normal" class="auto-style8">
                           <asp:Label ID="lblbISBN" runat="server" Text="ISBN" ></asp:Label>
                        </td>
                        <td colspan="3">
                             <asp:TextBox ID="txtbISBN" runat="server"  Width="400px" MaxLength="50" Style="border-style:inset none none inset;"></asp:TextBox>
         </td>
              <td style="font-weight:normal">
                           <asp:Label ID="lblbSection" runat="server" Text="Section" ></asp:Label>
                        </td>
                        <td colspan="3">
                             <asp:TextBox ID="txtbSection" runat="server"  Width="350px" MaxLength="50" Style="border-style:inset none none inset;"></asp:TextBox>
         </td>
          </tr>
          </table>
      <table style="width: 100%" >
          <tr>
                <td style="font-weight:normal" class="auto-style8">
                     <asp:Label ID="lblbChapter" runat="server" Text="Chapter" ></asp:Label>
                        </td>
                        <td >
                             <asp:TextBox ID="txtChapter" runat="server"  Width="250px"  Style="border-style:inset none none inset;"></asp:TextBox>
                         
         </td>
              <td>
              </td>
              <td style="font-weight:normal">
                           <asp:Label ID="lblbCountry" runat="server" Text="Country" ></asp:Label>
                        </td>
                        <td >
                             <asp:TextBox ID="txtCountry" runat="server"  Width="250px" Style="border-style:inset none none inset;"></asp:TextBox> 
         </td>
               <td>
              </td>
             <td style="font-weight:normal">
                           <asp:Label ID="lblbPublicationType" runat="server" Text="Type of Publication" ></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="DropDownListBookPublicationType" runat="server" Width="200px" DataTextField="Type" DataValueField="Id" Style="border-style:inset none none inset;" >
<asp:ListItem Value="0">--Select--</asp:ListItem>
        <asp:ListItem Value="C">Chapter in a book</asp:ListItem>
        <asp:ListItem Value="F">Full book</asp:ListItem>
     </asp:DropDownList>
         </td>
          </tr>
          </table>
      <table style="width: 100%">
             <tr>
     
      <td style="font-weight:normal" class="auto-style8"  >  
                  <asp:Label ID="LabelYear" runat="server" Text="Year" ></asp:Label>
                         <span>  <asp:Label ID="Label8" runat="server" Text="*"   ForeColor="Red"></asp:Label> </span> 
                        </td>
                         
       <td>
      <asp:TextBox ID="TextBoxYear" ReadOnly="true" runat="server"  Width="100px" Style="border-style:inset none none inset;"></asp:TextBox> 
   </td>
   <td style="font-weight:normal">  
                  <asp:Label ID="LabelMonth" runat="server" Text="Month" ></asp:Label>
                           <asp:Label ID="Label9" runat="server" Text="*"  ForeColor="Red"></asp:Label>  
                        </td>
                         
         <td>


                      <asp:DropDownList ID="DropDownListBookMonth" runat="server" Style="border-style:inset none none inset;"
                             DataSourceID="SqlDataSourcePubBookMonth" DataTextField="MonthName" DataValueField="MonthValue" Width="100px"  >

                          </asp:DropDownList> 
            <asp:SqlDataSource ID="SqlDataSourcePubBookMonth" runat="server" 
                ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" 
                
                SelectCommand="select MonthValue, MonthName from Publication_MonthM" 
                ProviderName="System.Data.SqlClient">
             
            </asp:SqlDataSource>
   </td>

     
                                <td style="font-weight:normal">
                    <asp:Label ID="Labelpagenum" runat="server" Text="Page Number" ></asp:Label>
                             <asp:Label ID="Label10" runat="server" Text="*"  ForeColor="Red"></asp:Label>  
                        </td>
                        <td>
                         <asp:TextBox ID="TextBoxPageNum" runat="server"   Width="100px" MaxLength="10" Style="border-style:inset none none inset;"></asp:TextBox>
      
            </td>
  <td style="font-weight:normal"> <asp:Label ID="LabelVolume1" runat="server" Text="Volume"  ></asp:Label>
           <asp:Label ID="Label11" runat="server" Text="*"  ForeColor="Red"></asp:Label>  
  </td> 
     <td  >
  <asp:TextBox ID="TextBoxVolume1" runat="server"  MaxLength="10"  Width="100px" Style="border-style:inset none none inset;"></asp:TextBox>
       

     </td>

                       </tr>
</table>




                </asp:Panel>


                <asp:Panel ID="panelOthes" runat="server" GroupingText="NewsPaper/Magazine" Visible="false" Font-Bold="true">


                    <table border="1" style="width: 100%">
                        <tr>
                            <td style="height: 36px">
                                <asp:Label ID="LabelPublisherNewsPaper" runat="server" Text="Publisher:" Font-Bold="true"></asp:Label>

                            </td>
                            <td style="height: 36px">
                                <asp:TextBox ID="TextBoxPublish" runat="server" Width="200px"></asp:TextBox>



                            </td>

                            <td style="height: 36px">
                                <asp:Label ID="LabelDateOfPublish" runat="server" Text="Date Of Publish:" Font-Bold="true"></asp:Label>
                            </td>
                            <td style="height: 36px">
                                <asp:TextBox ID="TextBoxDateOfPublish" runat="server" Width="200px"></asp:TextBox>
                            </td>

                            <td style="height: 36px">
                                <asp:Label ID="LabelPageNumNewsPaper" runat="server" Text="PageNum:" Font-Bold="true"></asp:Label></td>
                            <td style="height: 36px">
                                <asp:TextBox ID="TextBoxPageNumNewsPaper" runat="server" Width="200px"></asp:TextBox>


                            </td>
                        </tr>
                    </table>

                </asp:Panel>
                <br />
                <br />

                <asp:Panel ID="panelTechReport" runat="server" GroupingText="Modify File Upload" Enabled="true" Visible="true" Font-Bold="true">
                    <table border="1" style="width: 100%">
                        <%--<tr>

                            <td style="height: 36px">
                                <asp:Label ID="LabelkeyWords" runat="server" Text="Keywords:" Font-Bold="true"></asp:Label>
                            </td>
                            <td style="height: 36px">
                                <asp:TextBox ID="TextBoxKeywords" ReadOnly="true" runat="server" Width="400px"></asp:TextBox>
                            </td>

                            <td style="height: 36px">
                                <asp:Label ID="LabelisErf" runat="server" Text="ERF Related?" Font-Bold="true"></asp:Label>
                            </td>
                            <td style="height: 36px">
                                <asp:DropDownList Enabled="false" ID="DropDownListErf" runat="server">
                                    <asp:ListItem Value="N">No</asp:ListItem>
                                    <asp:ListItem Value="Y">Yes</asp:ListItem>

                                </asp:DropDownList>

                                ( Environmental Research Fund )
      
                            </td>

                        </tr>
                        <tr align="center">

                            <td align="center" colspan="6">
                                <asp:Label ID="Eprint" runat="server" Text="EPrint" Font-Bold="true"></asp:Label>

                            </td>

                        </tr>--%>
                        <tr>


                            <td style="height: 36px; font-weight: normal">
                                <asp:Label ID="LabelUploadPfd" runat="server" Text="Modify File Upload" ForeColor="Black"> </asp:Label>
                            </td>


                            <td colspan="3">

                                <asp:FileUpload ID="FileUploadPdf" runat="server" BorderColor="#996600" BorderStyle="Inset" />
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"
                                    ErrorMessage="!Upload only PDF(.pdf,.PDF) file" ControlToValidate="FileUploadPdf" ValidationGroup="validation"
                                    ValidationExpression="^.*\.((p|P)(d|D)(f|F))$"
                                    ForeColor="Red"></asp:RegularExpressionValidator>


                                <asp:GridView ID="GVViewFile" runat="server" AutoGenerateColumns="False"
                                    DataSourceID="DSforgridview" OnSelectedIndexChanged="GVViewFile_SelectedIndexChanged"
                                    HeaderStyle-ForeColor="White"
                                    PagerStyle-ForeColor="White" CellPadding="3"
                                    CellSpacing="3" PagerStyle-Width="4" PagerStyle-Height="4"
                                    BorderStyle="Solid">
                                    <Columns>
                                        <asp:TemplateField ShowHeader="false">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnView" runat="server"
                                                    CausesValidation="False" CommandName="Select"
                                                    ImageUrl="~/Images/view.gif" ToolTip="View File" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgetid" runat="server" Text='<%# Eval("UploadPDFPath") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                    </Columns>
                                    <HeaderStyle BackColor="#0b532d" Font-Bold="True" />


                                </asp:GridView>


                                <asp:SqlDataSource ID="DSforgridview" runat="server"
                                    ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>"
                                    SelectCommand="  "></asp:SqlDataSource>

                            </td>

                        </tr>

                        <tr>

                            <td colspan="4"><span style="margin-top: 25px;">Upload only PDF files (optional for Books) and the file size has to be less than or equal to 10MB.</span><br />
                                <span style="margin-top: 25px;">If the file size exceeds  10MB please contact Directorate of Research (help.rms@manipal.edu).</span>
                            </td>
                        </tr>
                        <tr>
                            <td>Remarks</td>
                            <td colspan="3">
                                <asp:TextBox runat="server" ID="TxtRemarks" Rows="2" Enabled="true" TextMode="MultiLine" Width="512px"></asp:TextBox></td>
                        </tr>
                    </table>
                </asp:Panel>

                <br />
                <br />

                <table width="100%">
                    <tr>
                        <td align="center">

                            <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="BtnSave_Click" CausesValidation="true" ValidationGroup="validation" Enabled="true"></asp:Button>
                        </td>
                    </tr>
                </table>
                <br />
          
    </center>


    </asp:Panel>

    <br />

    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
        ControlToValidate="TxtRemarks" Display="None"
        ErrorMessage="Please Enter a Remarks"
        ValidationGroup="validation"></asp:RequiredFieldValidator>
    <asp:HiddenField ID="rowVal" runat="server" />
    <asp:HiddenField ID="senderID" runat="server" />
    <asp:HiddenField ID="studentauthor" runat="server" />
</asp:Content>

