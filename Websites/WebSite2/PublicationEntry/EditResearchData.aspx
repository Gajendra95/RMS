<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPageRMS.master" AutoEventWireup="true" CodeFile="EditResearchData.aspx.cs" Inherits="PublicationEntry_EditResearchData" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

     <script src="../Scripts/jquery-1.7.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-1.7.1.min.js" type="text/javascript"></script>
       <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" DisplayMode="BulletList" 
     HeaderText="Validation issues" ShowSummary="False" ValidationGroup="validation"/> 
<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="true"></asp:ToolkitScriptManager>
   
     <center>
                    <asp:Label ID="lablPanelTitle" runat="server" Text="View and Manage Profile" Font-Bold="true"></asp:Label></center>

    <br />
   <center>
            <asp:Panel ID="Panel2" runat="server" Width="1000px" GroupingText="Researcher ID Profile"  Font-Bold="true"  Style="font-weight:bold; background-color:#E0EBAD">
               
                <center>
                    <table >
                        <tr>
                            <td class="auto-style11" style="width: 91px">
                                <asp:Label ID="lblUserid" runat="server" Text="Mahe ID"></asp:Label>
                             
                            </td>
                            <td style="height: 36px">
                                <asp:TextBox ID="txtUserID" runat="server" Width="281px"  MaxLength="12" Height="16px"></asp:TextBox>
                               
                       
                              <asp:Button ID="btnIdSearch" runat="server" Text="Search"  Width="63px" OnClick="btnIdSearch_Click" /></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblOrcid" runat="server" Text="ORCID"></asp:Label>

                            </td>
                            <td>

                                <asp:TextBox ID="txtOrcid" runat="server" Width="281px"  MaxLength="100" Height="16px"></asp:TextBox>
                            </td>
                          
                        </tr>
                        <tr>
                            
                            <td>
                                <asp:Label ID="lblScopusid" runat="server" Text="Scopus ID1"></asp:Label>

                            </td>
                            <td>
                                <asp:TextBox ID="txtScopusid" runat="server" Width="281px"  MaxLength="100" Height="16px"></asp:TextBox>

                            </td>
                        </tr>
                    
                         <tr>
                            
                            <td>
                                <asp:Label ID="lblScopusid2" runat="server" Text="Scopus ID2"></asp:Label>

                            </td>
                            <td>
                                <asp:TextBox ID="txtScopusid2" runat="server" Width="281px"  MaxLength="100" Height="16px"></asp:TextBox>

                            </td>
                        </tr>

                         <tr>
                            
                            <td>
                                <asp:Label ID="lblScopusid3" runat="server" Text="Scopus ID3"></asp:Label>

                            </td>
                            <td>
                                <asp:TextBox ID="txtScopusid3" runat="server" Width="281px"  MaxLength="100" Height="16px"></asp:TextBox>

                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:Label ID="lblnote1" runat="server" Text="Note: Please merge multiple Scopus IDs, update ORCID too if there is any change" ForeColor="Black"></asp:Label>

                            </td>
                        </tr>

                            </table>
                    </center>
                    </asp:Panel></center><br /><br />
    <center>
    <asp:Panel ID="Panel1" runat="server" Width="1000px" GroupingText="Domain & Area of Research Interest"  Font-Bold="true"  Style="font-weight:bold; background-color:#E0EBAD">
         <center>
        <table width="100%">
            <tr><td></td></tr>
               <tr><td></td></tr>   <tr><td></td></tr>   <tr><td></td></tr>
                        <tr>
                            <td class="auto-style11" style="width: 91px">Domain
                            </td>
                            <td>
                                <asp:TextBox ID="txtDomain" runat="server" Width="588px" TextMode="MultiLine" Height="66px" Rows="10" style="margin-left: 0px"  ></asp:TextBox></td>
                      </tr>
                        <tr>
                            <td></td>
                             <td>  <asp:Label ID="lblnote" runat="server" Text="Note: Enter Domain separated with colon(:)" ForeColor="Black"></asp:Label></td>
                            
                        </tr>
                       
                     
                    </table>
                    
            
                <br />
                
                         <table width="100%">
                  
                          
                   <tr><td></td>
                   </tr>
                           <tr><td>
                           <strong> 
                               <asp:Label ID="lblArea" runat="server" Text="Area of Research Interest:"></asp:Label></strong>


                          <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="addRow" Width="46px" />
                            
                           </td>
                           </tr>
                           </table>

                <br />

                    <asp:Panel ID="PanelResearch" runat="server"  BorderStyle="Double" Visible="false"  Width="967px" style="margin-right: 0px">

                    <div style="width: 100%; height: 250px; overflow: scroll">
                
                        <asp:GridView ID="GridResearch" runat="server" AutoGenerateColumns="false" Visible="false" GridLines="None"  OnRowDeleting="Grid_AuthorEntry_RowDeleting" 
    HeaderStyle-BackColor="#CC6600" HeaderStyle-ForeColor="Black"  CellPadding="4"  ForeColor="#333333" Width="956px" Height="16px" 
                             PagerStyle-BackColor="#CC6600" PagerStyle-ForeColor="White" 
                BorderColor="#FF6600" BorderStyle="Solid" style="margin-left: 0px" >

  <AlternatingRowStyle BackColor="White" />

     <Columns>

       
               <asp:TemplateField HeaderText="Area of Research Interest">
            <ItemTemplate>
               

                <asp:TextBox ID="Area" runat="server" Width="685px" Height="25px" TextMode="MultiLine"
     Text='<%# Bind("Area") %>'></asp:TextBox>
            </ItemTemplate>
        </asp:TemplateField>

         
               <asp:CommandField DeleteText="Remove" ShowDeleteButton="True"   />
          

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
                      </div>
                    </asp:Panel>
                   
                    <br />
                

                <table width="100%">
                    <tr>
                        <td align="center">
                            <asp:Button ID="btnSave" runat="server" Visible="true" Text="Save" OnClick="btnSave_Click" CausesValidation="true" ValidationGroup="validation" Enabled="true" Width="65px"></asp:Button>
                        </td>
                    </tr>
                </table>

                <br />
             
                 </center>
                   </asp:Panel>
    </center>
</asp:Content>

