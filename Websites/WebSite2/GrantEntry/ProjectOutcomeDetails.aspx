<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPageRMS.master" AutoEventWireup="true" CodeFile="ProjectOutcomeDetails.aspx.cs" Inherits="GrantEntry_ProjectOutcomeDetails" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
        <script src="../Scripts/jquery-1.7.1.min.js" type="text/javascript"></script>
    <script src="../JavaScript/JavaScript.js" type="text/javascript"></script>
      <style type="text/css">
        .hiddencol {
            display: none;
        }
    </style>
     <style type="text/css">
        .modalBackground {
            background-color: Gray;
            filter: alpha(opacity=50);
            opacity: 0.7;
        }

        .modalPopup {
            background-color: #FFFFFF;
            border: 3px solid #CCC;
            padding-top: 10px;
            padding-left: 10px;
            width: 300px;
            height: 140px;
            clear: both;
            position: absolute;
            margin-top: 0;
        }

        .overlay {
            position: fixed;
            top: 0;
            bottom: 0;
            left: 0;
            right: 0;
            background: rgba(0, 0, 0, 0.7);
            transition: opacity 500ms;
            visibility: hidden;
            opacity: 0;
        }

            .overlay:target {
                visibility: visible;
                opacity: 1;
            }

        .popup {
            margin: 70px auto;
            padding: 20px;
            background: #fff;
            border-radius: 5px;
            width: 30%;
            position: relative;
            transition: all 5s ease-in-out;
        }

            .popup h2 {
                margin-top: 0;
                color: #333;
                font-family: Tahoma, Arial, sans-serif;
            }

            .popup .close {
                position: absolute;
                top: 20px;
                right: 30px;
                transition: all 50ms;
                font-size: 30px;
                font-weight: bold;
                text-decoration: none;
                color: #333;
            }

                .popup .close:hover {
                    color: #06D85F;
                }

            .popup .content {
                max-height: 90%;
                /*max-height: 30%;*/
                overflow: auto;
            }

        .popupp {
            margin: 120px auto;
            padding: 20px;
            background: #fff;
            border-radius: 5px;
            width: 30%;
            position: relative;
            transition: all 5s ease-in-out;
        }

            .popupp h2 {
                margin-top: 0;
                color: #333;
                font-family: Tahoma, Arial, sans-serif;
            }

            .popupp .close {
                position: absolute;
                top: 20px;
                right: 30px;
                transition: all 50ms;
                font-size: 30px;
                font-weight: bold;
                text-decoration: none;
                color: #333;
            }

                .popupp .close:hover {
                    color: #06D85F;
                }

            .popupp .content {
                max-height: 90%;
                /*max-height: 30%;*/
                overflow: auto;
            }

        .close1 {
            /*position: absolute;*/
            /*transition: all 200ms;*/
            font-size: 20px;
            font-weight: bold;
            text-decoration: underline;
            color: #333;
        }

            .close1:hover {
                color: #06D85F;
            }

        .test2 {
            background: White;
        }

        .auto-style27 {
            width: 14.5%;
        }
    </style>
       <script type="text/javascript">
           function callthis2() {
               debugger;
               // alert("cxvxcvxc");
               var loc = $(location).attr('href');

               if (loc.match(/.*#/)) {
                   // loc = loc.substring(0,loc.length - 1);
                   window.location = loc + 'commentpopup2';
               }
               else {
                   window.location = loc + '#commentpopup2';
               }
           }
           function callthis3() {
               debugger;
               // alert("cxvxcvxc");
               var loc = $(location).attr('href');

               if (loc.match(/.*#/)) {
                   // loc = loc.substring(0,loc.length - 1);
                   window.location = loc + 'commentpopup3';
               }
               else {
                   window.location = loc + '#commentpopup3';
               }
           }
           function callthis4() {
               debugger;
               // alert("cxvxcvxc");
               var loc = $(location).attr('href');

               if (loc.match(/.*#/)) {
                   // loc = loc.substring(0,loc.length - 1);
                   window.location = loc + 'commentpopup4';
               }
               else {
                   window.location = loc + '#commentpopup4';
               }
           }
            </script>



     <%-- <script type="text/javascript">
         function ToggleDisplay() {
             // $("#commentpopup").dialog("close");
             $("#commentpopup2 .close").click()
             var loc = $(location).attr('href');
             window.location = "#"
         }
           </script>
    <script type="text/javascript">
        function ToggleDisplay() {
            // $("#commentpopup").dialog("close");
            $("#commentpopup3 .close").click()
            var loc = $(location).attr('href');
            window.location = "#"
        }
           </script>
    <script type="text/javascript">
        function ToggleDisplay() {
            // $("#commentpopup").dialog("close");
            $("#commentpopup4 .close").click()
            var loc = $(location).attr('href');
            window.location = "#"
        }
           </script>--%>
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" DisplayMode="List"
        HeaderText="Validation issues" ShowSummary="False" ValidationGroup="validation" />
    <asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<%--    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">

        <ContentTemplate> --%>
      <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
    <asp:Panel ID="panelSearchPub" runat="server" ForeColor="Black"  GroupingText="Project Outcome Details"  Font-Bold="true" Style="background-color:#E0EBAD" > 
                
                  <br />
  <center>

        <%--   <asp:Label ID="Label2" runat="server" Text="Search" Font-Bold="true" ></asp:Label>--%>
                      <br />
             <%--<table >
                    <tr>                                   
                        <td style="width:100px" >
                           Employee ID
                        </td>
                        <td>
                          <asp:TextBox ID="PubIDSearch"  runat="server" Style="border-style:inset none none inset;" ></asp:TextBox>
                        </td>
                         
                    
                         
                        <td style="width:100px" >  
                              <asp:Button ID="ButtonSearchPub" runat="server" Text="Search" OnClick="ButtonSearchPubOnClick"  />
                        </td>
                      
       </tr>
               
                     </table> --%>
       <table border="1">
           
            <tr>              
                <td style="height: 56px; font-weight: normal">
                    <asp:Label ID="LabelFromDate" runat="server" Text="From Date" ForeColor="Black"></asp:Label>
                    <asp:TextBox ID="TextBoxFromDate" runat="server" Style="border-style: inset none none inset;"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="TextBoxFromDate" Format="dd/MM/yyyy">
                    </asp:CalendarExtender>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBoxFromDate" Display="None"
                        ErrorMessage="Please enter From date" InitialValue=" " ValidationGroup="validation"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator15" runat="server" Display="None" ControlToValidate="TextBoxFromDate" ValidationGroup="validation"
                        ErrorMessage="From date in (dd/mm/yyyy) format" SetFocusOnError="true"
                        ValidationExpression="^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$">
                    </asp:RegularExpressionValidator>
                </td>
                <td style="height: 56px; font-weight: normal">
                    <asp:Label ID="LabelToDate" runat="server" Text="To Date" ForeColor="Black"></asp:Label>
                    <asp:TextBox ID="TextBoxToDate" runat="server" Style="border-style: inset none none inset;"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="TextBoxToDate" Format="dd/MM/yyyy">
                    </asp:CalendarExtender>
                </td>
                 <td rowspan="2">
                    <asp:Button ID="Search" runat="server" Text="Search" OnClick="ButtonSearchPubOnClick" BorderColor="Black" Width="90px" Height="40px" CausesValidation="true" ValidationGroup="validation" />
                </td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TextBoxToDate" Display="None"
                    ErrorMessage="Please enter To date" InitialValue=" " ValidationGroup="validation"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" Display="None" ControlToValidate="TextBoxToDate" ValidationGroup="validation"
                    ErrorMessage="From date in (dd/mm/yyyy) format" SetFocusOnError="true"
                    ValidationExpression="^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$">
                </asp:RegularExpressionValidator>

            </tr>
           
        </table>
                     <br />

                              
                        <asp:GridView ID="GridViewSearch" runat="server"  AllowPaging="True" 
        PagerSettings-PageButtonCount="10" PageSize="10" DataSourceID="SqlDataSource1"  EmptyDataText="No Data found"
        HeaderStyle-BackColor="#CC6600" HeaderStyle-ForeColor="Black" AutoGenerateColumns="false" OnPageIndexChanging="GridViewSearchPub_PageIndexChanging" 
        PagerStyle-BackColor="#CC6600" PagerStyle-ForeColor="White" CellPadding="3"  OnRowDataBound="GridView2_RowDataBound" OnRowEditing="edit" OnRowCommand="GridView2_RowCommand"
        CellSpacing="3" PagerStyle-Width="4" PagerStyle-Height="4" Visible="False" 
        BorderColor="#FF6600" BorderStyle="Solid" Width="750px" AllowSorting="true">
        <Columns>     
              <asp:TemplateField HeaderText="Sl.No" ItemStyle-Width="6%">
                                    <ItemTemplate>
                                          <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                    <ItemStyle Width="6%" />
                                </asp:TemplateField>
             <asp:TemplateField HeaderText="Project ID" SortExpression="Project ID">
                                    <ItemTemplate>
                                     
                                         <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle />
                                </asp:TemplateField>
               <asp:TemplateField HeaderText="Project Unit" SortExpression="Project Unit">
                                    <ItemTemplate>
                                     
                                         <asp:Label ID="lblProject" runat="server" Text='<%# Eval("ProjectUnit") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle />
                                </asp:TemplateField>
               <asp:TemplateField HeaderText="Title" SortExpression="Title">
                                    <ItemTemplate>
                                     
                                         <asp:Label ID="lblTitle" runat="server" Text='<%# Eval("Title") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle />
                                </asp:TemplateField>
               <asp:TemplateField HeaderText="Funding Agency" SortExpression="FundingAgency">
                                    <ItemTemplate>
                                     
                                         <asp:Label ID="lblFunding" runat="server" Text='<%# Eval("FundingAgency") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle />
                                </asp:TemplateField>
               <asp:TemplateField HeaderText="Investigator Name" SortExpression="InvestigatorName">
                                    <ItemTemplate>
                                     
                                         <asp:Label ID="lblInvestigator" runat="server" Text='<%# Eval("InvestigatorName") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle />
                                </asp:TemplateField>
               <asp:TemplateField HeaderText="Publication Count" SortExpression="PublicationCount">
                                    <ItemTemplate>
                                     
                                         <asp:LinkButton ID="lblPublication" runat="server" Text='<%# Eval("PublicationCount") %>' OnClick="lblPublication_Click"></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle />
                                </asp:TemplateField>
               <asp:TemplateField HeaderText="Project Outcome Count" SortExpression="ProjectOutcomeCount">
                                    <ItemTemplate>
                                     
                                         <asp:LinkButton ID="lblProjectOutcome" runat="server" Text='<%# Eval("ProjectOutcomeCount") %>' OnClick="lblProjectOutcome_Click"></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle />
                                </asp:TemplateField>
         <asp:TemplateField HeaderText="Patent Count" SortExpression="PatentCount">
                                    <ItemTemplate>
                                     
                                         <asp:LinkButton ID="lblPatent" runat="server" Text='<%# Eval("PatentCount") %>' OnClick="lblPatent_Click"></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle />
                                </asp:TemplateField>
                   <%--    <asp:BoundField DataField="MemberType" ReadOnly="true" HeaderText="MemberType" 
                SortExpression="MemberType"  ItemStyle-CssClass="hiddencol"  HeaderStyle-CssClass="hiddencol" />                  
            <asp:BoundField DataField="EmployeeName" ReadOnly="true" HeaderText="Employee Name" 
                SortExpression="EmployeeName" /> 
              <asp:TemplateField HeaderText="CurrentBalance" ItemStyle-Width="6%" SortExpression="CurrentBalance">
                                    <ItemTemplate>
                                           <asp:LinkButton ID="lblCurrentBalance" runat="server" Text='<%# Eval("ProjectUnit") %>'></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle Width="6%" />
                                </asp:TemplateField>

              <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                       <asp:LinkButton ID="lblSanction" runat="server" Text="Details" OnClick="lblSanction_Click" ></asp:LinkButton>
                                      
                                    </ItemTemplate>
                                    <ItemStyle Width="6%" />
                                </asp:TemplateField>--%>
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
           <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
         ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" 
         SelectCommand="RepselectProjectOutcomeDetails" SelectCommandType="StoredProcedure">

                 <SelectParameters>
                      <asp:ControlParameter ControlID="TextBoxFromDate" Name="CreatedDate1" PropertyName="Text" Type="DateTime" />
                     <asp:ControlParameter ControlID="TextBoxToDate" Name="CreatedDate2" PropertyName="Text" Type="DateTime" />
                        </SelectParameters>
        </asp:SqlDataSource>

                         <br />

                             </center>        
                     </asp:Panel>
          </ContentTemplate>  
        </asp:UpdatePanel>

    <div id="commentpopup2" class="overlay">
                <div class="popupp" style="width: 600px; height:500px;margin-top: 50px;">

                    <a class="close" href="#">&times;</a>
                    <div class="content" style="overflow-y: scroll; height: auto; width: 600px;">
                        <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
    <center>
     <asp:Panel ID="paneloutcome"  runat="server" BackColor="GhostWhite"  >
          
<asp:Label ID="Label14" runat="server" Text="Publication Details" Font-Size="20px" Style="color:black" />
                       
              <center>
     <asp:GridView ID="GridViewProject" runat="server" AllowSorting="true" AutoGenerateColumns="false" EmptyDataText="No Data found"
        OnRowDataBound="GridViewProject_RowDataBound" DataSourceID="SqlDataSourceProject" Width="550px" >
      
                            <Columns>     
                                                     
                                <asp:TemplateField HeaderText="Publication ID">
                                    <ItemTemplate>
                                         <%--<asp:UpdatePanel ID="UpdatePanel8" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>--%>
                                     <%--    <asp:LinkButton ID="lnkPubId" runat="server" OnClick="Redirect" Text='<%# Eval("ID ") %>'></asp:LinkButton>--%>
                                          <%-- <asp:Button ID="lnkPubId" runat="server" OnClick="Redirect" Text='<%# Eval("ID ") %>' />--%>
                                        <asp:Label ID="TextBoxProjectId" runat="server" Text='<%# Eval("PublicationID") %>'></asp:Label>
                                    <%-- </ContentTemplate>--%>
                                  <%-- <Triggers> 
                                      <%-- <asp:PostBackTrigger ControlID="lnkPubId" />--%>
                                     <%--  <asp:AsyncPostBackTrigger ControlID="lnkPubId" EventName="OnClick" />--%>
                                   <%--</Triggers>
                                    </asp:UpdatePanel>--%>
                                    </ItemTemplate>
                                </asp:TemplateField>                                                           
                                 <asp:TemplateField HeaderText="Type Of Entry">
                                    <ItemTemplate>                                       
                                        <asp:Label ID="TextBoxProjectTitle" runat="server" Text='<%# Eval("TypeOfEntry") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Title">
                                    <ItemTemplate>                                       
                                        <asp:Label ID="TextBoxFundingAgency" runat="server" Text='<%# Eval("TitleWorkItem") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>                                                              
                                
                            </Columns>
                            <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                            <HeaderStyle BackColor="#0B532D" Font-Bold="True" ForeColor="White" />
                            <PagerStyle ForeColor="#0B532D" HorizontalAlign="Center" />
                            <RowStyle BackColor="#FFFFFF" ForeColor="#000000" />
                            <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
                            <SortedAscendingCellStyle BackColor="#FFF1D4" />
                            <SortedAscendingHeaderStyle BackColor="#B95C30" />
                            <SortedDescendingCellStyle BackColor="#F1E5CE" />
                            <SortedDescendingHeaderStyle BackColor="#93451F" />
                        </asp:GridView>
                                    </center>

                </asp:Panel>
        </center>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    <center><a class="close1" id="a4" href="#">Close</a></center>

                </div>

            </div>
            <br />
     <div id="commentpopup3" class="overlay">
                <div class="popupp" style="width: 600px; height:500px;margin-top: 50px;">

                    <a class="close" href="#">&times;</a>
                    <div class="content" style="overflow-y: scroll; height: auto; width: 600px;">
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                  <asp:Panel ID="panel1"  runat="server" BackColor="GhostWhite"  >
<asp:Label ID="Label16" runat="server" Text="Patents Details" Font-Size="20px" Style="color:black" />
                       
              <center>
     <asp:GridView ID="GridViewPatentOutcome" runat="server" AllowSorting="true" AutoGenerateColumns="false" EmptyDataText="No Data found"
        OnRowDataBound="GridViewPatent_RowDataBound" DataSourceID="SqlDataSourcepatentoutcome"  Width="500px" >
      
                            <Columns>     
                                                     
                                <asp:TemplateField HeaderText="Patent ID">
                                    <ItemTemplate>
                                        
                                        <asp:Label ID="TextBoxProjectId" runat="server" Text='<%# Eval("ID") %>'></asp:Label>                             
                                    </ItemTemplate>
                                </asp:TemplateField>                                                           
                               
                                <asp:TemplateField HeaderText="Title">
                                    <ItemTemplate>                                       
                                        <asp:Label ID="TextBoxFundingAgency" runat="server" Text='<%# Eval("Title") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>                                                              
                                
                            </Columns>
                            <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                            <HeaderStyle BackColor="#0B532D" Font-Bold="True" ForeColor="White" />
                            <PagerStyle ForeColor="#0B532D" HorizontalAlign="Center" />
                            <RowStyle BackColor="#FFFFFF" ForeColor="#000000" />
                            <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
                            <SortedAscendingCellStyle BackColor="#FFF1D4" />
                            <SortedAscendingHeaderStyle BackColor="#B95C30" />
                            <SortedDescendingCellStyle BackColor="#F1E5CE" />
                            <SortedDescendingHeaderStyle BackColor="#93451F" />
                        </asp:GridView>
                                    </center>
                      </asp:Panel>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                                </div>
                    <center><a class="close1" id="a1" href="#">Close</a></center>

                </div>

            </div>
            <br />



                                 <div id="commentpopup4" class="overlay">
                <div class="popupp" style="width: 600px; height:500px;margin-top: 50px;">

                    <a class="close" href="#">&times;</a>
                    <div class="content" style="overflow-y: scroll; height: auto; width: 600px;">
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                 <asp:Panel ID="panel2"  runat="server" BackColor="GhostWhite"  >
<asp:Label ID="Label17" runat="server" Text="Project Outcome Details" Font-Size="20px" Style="color:black" />
                        
              <center>
     <asp:GridView ID="GridViewprojecout" runat="server" AllowSorting="true" AutoGenerateColumns="false" EmptyDataText="No Data found"
        OnRowDataBound="GridViewprojecout_RowDataBound" DataSourceID="SqlDataSourceprooutcome"  Width="500px" >
      
                            <Columns>     
                                                     
                                 <asp:BoundField DataField="OutcomeDate" HeaderText="Outcome Date" DataFormatString="{0:dd/MM/yyyy }"  />
                                                       
                               
                                <asp:TemplateField HeaderText="Description">
                                    <ItemTemplate>                                       
                                        <asp:Label ID="TextBoxFundingAgency" runat="server" Text='<%# Eval("Description") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>                                                              
                                
                            </Columns>
                            <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                            <HeaderStyle BackColor="#0B532D" Font-Bold="True" ForeColor="White" />
                            <PagerStyle ForeColor="#0B532D" HorizontalAlign="Center" />
                            <RowStyle BackColor="#FFFFFF" ForeColor="#000000" />
                            <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
                            <SortedAscendingCellStyle BackColor="#FFF1D4" />
                            <SortedAscendingHeaderStyle BackColor="#B95C30" />
                            <SortedDescendingCellStyle BackColor="#F1E5CE" />
                            <SortedDescendingHeaderStyle BackColor="#93451F" />
                        </asp:GridView>
                                    </center>
</asp:Panel>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                </div>
                    <center><a class="close1" id="a2" href="#">Close</a></center>

                </div>

            </div>
            <br />

<asp:SqlDataSource ID="SqlDataSourceProject" runat="server" ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" 
           SelectCommandType="Text">  

                </asp:SqlDataSource>

             <asp:SqlDataSource ID="SqlDataSourcepatentoutcome" runat="server" ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" 
           SelectCommandType="Text">  

                </asp:SqlDataSource>

                <asp:SqlDataSource ID="SqlDataSourceprooutcome" runat="server" ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" 
           SelectCommandType="Text">  

                </asp:SqlDataSource>

<br />

             
         
                   


</asp:Content>

