<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPageRMS.master" AutoEventWireup="true" CodeFile="ManageSeedMoney.aspx.cs" Inherits="ManageJournal_ManageSeedMoney" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" DisplayMode="BulletList"
        HeaderText="Validation issues" ShowSummary="False" ValidationGroup="Validation" />
    <script src="../Scripts/jquery-1.7.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-1.7.1.min.js" type="text/javascript"></script>
    <style type="text/css">
        .modelBackground {
            background-color: Gray;
            filter: alpha(opacity=70);
            opacity: 0.7;
        }

        .modelPopup {
            background-color: #EEEEE;
            border-width: 1px;
            border-style: solid;
            border-color: Gray;
            font-family: Verdana;
            font-size: medium;
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
    </style>
    <script type="text/javascript">

        function ViewPdf() {
            debugger;
            window.open('<%= Page.ResolveUrl("~/PublicationEntry/DisplayPdf.aspx")%>', '_blank');
         }
         //function ValidateCheckBoxList(sender, args) {
         // debugger;<CheckboxCategory.ClientID %>
         // var checkBoxList = document.getElementById("");
         // var checkboxes = checkBoxList.getElementsByTagName("input");
         // var isValid = false;
         // for (var i = 0; i < checkboxes.length; i++) {
         //    if (checkboxes[i].checked) {
         //       isValid = true;
         //       break;
         //   }
         //}
         // args.IsValid = isValid;
         // }
         // function ValidateCheckBoxList1(sender, args) {
         //   debugger=CheckboxCategory1.ClientID;
         //  var checkBoxList = document.getElementById("");
         //  var checkboxes = checkBoxList.getElementsByTagName("input");
         //  var isValid = false;
         // for (var i = 0; i < checkboxes.length; i++) {
         //    if (checkboxes[i].checked) {
         //       isValid = true;
         //      break;
         //   }
         //}
         // args.IsValid = isValid;
         // }
         function setRow(obj) {

             var sndID = obj.id;
             var sndrID = document.getElementById('<%= senderID.ClientID %>');
             sndrID.value = sndID;
             {
                 $('#<%=popupPanelFaculty.ClientID %>').show()
                 $('#<%=popupPanelstudent.ClientID %>').hide()

             }
         }
         function setRow1(obj) {

             var sndID = obj.id;
             var sndrID = document.getElementById('<%= senderID.ClientID %>');
             sndrID.value = sndID;
             {
                 $('#<%=popupPanelstudent.ClientID %>').show()
                 $('#<%=popupPanelFaculty.ClientID %>').hide()
             }
         }
         function setRow2(obj) {

             var sndID = obj.id;
             var sndrID = document.getElementById('<%= senderID.ClientID %>');
             sndrID.value = sndID;
             var rowNo = document.getElementById('<%= rowVal.ClientID %>');
             rowNo.value = rowIndex;
             {
                 $('#<%=PanelseedMoney.ClientID %>').show()
             }
         }
    </script>
    <script type="text/javascript">
        function AddConfirm() {
            var validated = Page_ClientValidate('Validation');
            if (validated) {
                //var validated = Page_ClientValidate('validation');
                //if (validated) 
                debugger;
                var OLDprojecttype = document.getElementById('<%= myHiddenOldProjecttype.ClientID %>');
                 var newprojecttype = document.getElementById('<%= StatusofSeedMoney.ClientID %>');
                 var sta = newprojecttype.value;
                 var dfs = OLDprojecttype.value;
                 var confirm_value2 = document.createElement("INPUT");
                 confirm_value2.type = "hidden";
                 confirm_value2.name = "confirm_value2";
                 if (sta == "E") {
                     confirm_value2.value = "Yes";
                     if ((dfs == 0) || (dfs == 1)) {


                     }
                     else {
                         if (confirm("the existing Seed money life Cycle will be disabled do you want to continue?"))
                             confirm_value2.value = "Yes";
                         else
                             confirm_value2.value = "No";
                         document.forms[0].appendChild(confirm_value2);
                     }
                 }
                 else
                     if (sta == "A") {
                         confirm_value2.value = "Yes";
                         if (dfs == 0) {


                         }
                         else {
                             if (confirm("the existing Seed money life Cycle will be disabled do you want to continue?"))
                                 confirm_value2.value = "Yes";
                             else
                                 confirm_value2.value = "No";
                             document.forms[0].appendChild(confirm_value2);
                         }
                     }
             }

         }
    </script>
    <%-- <script type = "text/javascript">

         function setRow(obj) {

             var row = obj.parentNode.parentNode;
             var rowIndex = row.rowIndex - 1;


             var sndID = obj.id;
             var sndrID = document.getElementById('<%= senderID.ClientID %>');
           sndrID.value = sndID;

           var rowNo = document.getElementById('<%= rowVal.ClientID %>');
           rowNo.value = rowIndex;
           var mu = $(row).find("[id*=DropdownMuNonMu]").val();
           if (mu == "M") {
               $('#<%=popupPanelAffil.ClientID %>').show()
               $('#<%=popupstudent.ClientID %>').hide();

           }
           else if (mu == "S") {
               $('#<%=popupstudent.ClientID %>').show();
               $('#<%=popupPanelAffil.ClientID %>').hide();

           }
   }
    </script>--%>
    <asp:ToolkitScriptManager ID="Scriptmanager1" runat="server" />
    <asp:Panel ID="panel" runat="server" GroupingText="" Font-Bold="true" BackColor="#E0EBAD">
        <center>
            <br />
            <br />
            <table>
                <%--<tr>
                    <td></td>
                    <td>
                        <center>
                            <asp:LinkButton ID="lnkSeedMoneyFaculty" runat="server" Text="Last added Seed Money Cycle" Style="color: navy; font-weight: bold; font-size: 15px" OnClick="lnkSeedMoneyFaculty_Click" OnClientClick="setRow2(this)" />
                            <asp:ModalPopupExtender ID="ModalPopupExtender4" runat="server" TargetControlID="lnkSeedMoneyFaculty" PopupControlID="PanelseedMoney"
                                BackgroundCssClass="modelBackground">
                            </asp:ModalPopupExtender>
                        </center>

                    </td>
                </tr>--%>
                <tr>
                    <td>
                        <asp:Label ID="LabelIndexed" runat="server" Text="Add/Update" ForeColor="Black"></asp:Label></td>
                    <td>
                        <asp:RadioButtonList ID="RadioButtonListUserType" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="RadioButtonListUserType_SelectedIndexChanged" AutoPostBack="true" Style="border-style: inset none none inset;">
                            <asp:ListItem Value="A" Selected="True">Add New Seed Money Cycle</asp:ListItem>
                            <asp:ListItem Value="E">View Seed Money Cycle</asp:ListItem>


                        </asp:RadioButtonList>

                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label5" runat="server" Text="ID" ForeColor="Black"></asp:Label>
                    </td>
                    <td style="width: 100px">
                        <div>
                            <asp:TextBox ID="TextBox1" runat="server" Enabled="false"></asp:TextBox>
                            <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/Images/srchImg.gif" OnClientClick="setRow2(this)" OnClick="showPop2" />
                        </div>
                        <asp:ModalPopupExtender ID="ModalPopupExtender3" runat="server" TargetControlID="ImageButton2" PopupControlID="PanelseedMoney"
                            BackgroundCssClass="modelBackground">
                        </asp:ModalPopupExtender>
                    </td>
                </tr>
                <tr>

                    <td>
                        <asp:Label ID="LabelFromDate" runat="server" Text="From Date" ForeColor="Black"></asp:Label>
                    </td>
                    <td style="width: 100px">
                        <div id="Div1" runat="server" style="position: relative;">
                            <asp:TextBox ID="TextBoxFromDate" runat="server">
                            </asp:TextBox>



                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="TextBoxFromDate" Format="dd/MM/yyyy">
                            </asp:CalendarExtender>
                        </div>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TextBoxFromDate" Display="None"
                            ErrorMessage="Please enter From date" InitialValue=" " ValidationGroup="validation"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator15" runat="server" Display="None" ControlToValidate="TextBoxFromDate" ValidationGroup="validation"
                            ErrorMessage="From date in (dd/mm/yyyy) format" SetFocusOnError="true"
                            ValidationExpression="^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$">
                        </asp:RegularExpressionValidator>
                        <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage=" From Date  must be greater than the Current date." Display="None" ControlToValidate="TextBoxFromDate" ValidationGroup="Validation"
                            Operator="GreaterThanEqual" Type="Date" SetFocusOnError="true" ValueToCompare="<%= DateTime.Today.ToShortDateString() %>"> </asp:CompareValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="LabelToDate" runat="server" Text="To Date" ForeColor="Black"></asp:Label>
                    </td>
                    <td style="width: 100px">
                        <div id="Div2" runat="server" style="position: relative;">
                            <asp:TextBox ID="TextBoxToDate" runat="server" Style="border-style: inset none none inset;">
                            </asp:TextBox>

                            <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="TextBoxToDate" Format="dd/MM/yyyy">
                            </asp:CalendarExtender>
                        </div>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="TextBoxToDate" Display="None"
                            ErrorMessage="Please Enter To Date" InitialValue=" " ValidationGroup="validation"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" Display="None" ControlToValidate="TextBoxToDate" ValidationGroup="validation"
                            ErrorMessage="To date in (dd/mm/yyyy) format" SetFocusOnError="true"
                            ValidationExpression="^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$">
                        </asp:RegularExpressionValidator>
                        <asp:CompareValidator ID="CompareValidatorTextBoxDate" runat="server" ErrorMessage=" To Date  must be greater than the from date." Display="None" ControlToValidate="TextBoxToDate" ValidationGroup="Validation"
                            Operator="GreaterThanEqual" Type="Date" SetFocusOnError="true" ControlToCompare="TextBoxFromDate"></asp:CompareValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label1" runat="server" Text="AuthorType"></asp:Label>
                    </td>
                    <td style="width: 100px">
                        <asp:DropDownList ID="DropDownListAuthorType" runat="server" Width="100px" OnSelectedIndexChanged="DropDownListAuthorType_SelectedIndexChanged" AutoPostBack="true">
                            <%-- <asp:ListItem Value="B" Selected="True">Both</asp:ListItem>--%>
                            <%--  <asp:ListItem Value="S" Selected="True">Student</asp:ListItem>--%>
                            <asp:ListItem Value="F" Selected="True">Faculty</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>

                <tr>
                    <td>
                        <asp:Label ID="lblActive" runat="server" Text="Enable" Visible="false"></asp:Label>
                    </td>
                    <td style="width: 100px">
                        <asp:DropDownList ID="DropDownListactive" runat="server" Width="100px" OnSelectedIndexChanged="DropDownListactive_SelectedIndexChanged" AutoPostBack="true" Visible="false">
                            <asp:ListItem Value="Y">Yes</asp:ListItem>
                            <asp:ListItem Value="N">No</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label3" runat="server" Text="Faculty Category"></asp:Label>
                    </td>
                    <td style="width: 100px">
                        <%-- <asp:DropDownList ID="CheckboxCategory" runat="server" Width="100px" ForeColor="Black"  DataSourceID="SqlDataSourceCheckboxCheckboxCategory" 
                  RepeatDirection="Vertical" DataTextField="Amount" DataValueField="BudgetId">
     
                    </asp:DropDownList>--%>
                        <%--  <asp:CheckBoxList ID="CheckboxCategory" runat="server" ForeColor="Black"  DataSourceID="SqlDataSourceCheckboxCheckboxCategory" 
                  RepeatDirection="Vertical" DataTextField="Amount" DataValueField="BudgetId" Visible="false">
                
                </asp:CheckBoxList>--%>
                        <div>
                            <asp:GridView ID="GridViewFacultyCategoty" runat="server" ShowHeader="False" AutoGenerateColumns="false" GridLines="None">
                                <Columns>
                                    <%--  <asp:TemplateField>
        <ItemTemplate>
             <%#Container.DataItemIndex+1 %>
        </ItemTemplate>
    </asp:TemplateField>--%>
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="TextBoxFacultyBudgetId" runat="server" Text='<%# Eval("BudgetId") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Label ID="TextBoxFBudgetAmount" runat="server" Text='<%# Eval("Amount") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>

                            <%--  </td>
                <td >--%>
                            <asp:ImageButton ID="imageBkCbtn" runat="server" ImageUrl="~/Images/srchImg.gif" OnClientClick="setRow(this)" OnClick="showPop" />
                        </div>
                        <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="imageBkCbtn" PopupControlID="popupPanelFaculty"
                            BackgroundCssClass="modelBackground">
                        </asp:ModalPopupExtender>

                        <asp:SqlDataSource ID="SqlDataSourceFacultyCategoty" runat="server" ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>"
                            SelectCommand=""></asp:SqlDataSource>
                        <%--       
<asp:SqlDataSource ID="SqlDataSourceCheckboxCheckboxCategory" runat="server" ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" 
           SelectCommand="">
     
                </asp:SqlDataSource>--%>
                   
                    </td>

                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label4" runat="server" Text="Student Category"></asp:Label>
                    </td>
                    <td style="width: 100px">

                        <%-- <asp:DropDownList ID="CheckboxCategory1" runat="server" Width="100px" ForeColor="Black"  DataSourceID="SqlDataSourceCheckboxCheckboxCategory1" 
                  RepeatDirection="Vertical" DataTextField="Amount" DataValueField="BudgetId" >
     
                    </asp:DropDownList>--%>
                        <%--  <%--<asp:CheckBoxList ID="CheckboxCategory1" runat="server" ForeColor="Black"  DataSourceID="SqlDataSourceCheckboxCheckboxCategory1" 
                  RepeatDirection="Vertical" DataTextField="Amount" DataValueField="BudgetId" Visible="false">
                
                </asp:CheckBoxList>--%>
                        <%--<asp:GridView ID="GridViewstudentCategoty" runat="server">

                </asp:GridView>--%>
                        <div>
                            <asp:GridView ID="GridViewstudentCategoty" runat="server" ShowHeader="False" AutoGenerateColumns="false" GridLines="None">
                                <Columns>
                                    <%--  <asp:TemplateField>
        <ItemTemplate>
             <%#Container.DataItemIndex+1 %>
        </ItemTemplate>
    </asp:TemplateField>--%>
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="TextBoxStudentBudgetId" runat="server" Text='<%# Eval("BudgetId") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Label ID="TextBoxSBudgetAmount" runat="server" Text='<%# Eval("Amount") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>

                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Images/srchImg.gif" OnClientClick="setRow1(this)" OnClick="showPop1" />
                        </div>
                        <asp:ModalPopupExtender ID="ModalPopupExtender2" runat="server" TargetControlID="ImageButton1" PopupControlID="popupPanelstudent"
                            BackgroundCssClass="modelBackground">
                        </asp:ModalPopupExtender>
                        <asp:SqlDataSource ID="SqlDataSourceStudentCategoty" runat="server" ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>"
                            SelectCommand=""></asp:SqlDataSource>
                        <%-- <asp:SqlDataSource ID="SqlDataSourcestudentCategoty" runat="server" ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" 
           SelectCommand="">
     
                </asp:SqlDataSource>--%>
                        <%--<asp:SqlDataSource ID="SqlDataSourceCheckboxCheckboxCategory1" runat="server" ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" 
           SelectCommand="">
     
                </asp:SqlDataSource>--%>
                   
                    </td>

                </tr>
                <tr>
                    <%-- <td><asp:Label ID="Label4" runat="server" Text="Status" ></asp:Label> </td>
            <td style="width:100px">
        <asp:DropDownList ID="DropDownListStatus" runat="server" Width="100px" >
        <asp:ListItem Value="NEW">Draft</asp:ListItem>
          <asp:ListItem Value="SUB">Submitted</asp:ListItem>
             <asp:ListItem Value="APP">Approved</asp:ListItem>
                <asp:ListItem Value="CAN">Cancelled</asp:ListItem>
                    </asp:DropDownList>
                  </td>--%>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="LabelRemarks" runat="server" Text="Remarks"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TextBoxRemarks" runat="server" TextMode="MultiLine" Width="250px">
       
                        </asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="None" ControlToValidate="TextBoxRemarks" ErrorMessage="Enter Remarks" ValidationGroup="Validation"></asp:RequiredFieldValidator>

                    </td>

                </tr>
               <%-- <tr>
                    <td>
                        <asp:Label ID="Label2" runat="server" Text="Message"></asp:Label>
                    </td>
                    <td style="width: 250px">
                        <asp:TextBox ID="TextBoxNote" runat="server" TextMode="MultiLine" Width="250px">
       
                        </asp:TextBox>


                    </td>


                </tr>--%>
                <tr>
                    <td></td>
                    <td style="width: 100px">
                        <asp:Button ID="Button1" runat="server" Text="Save" OnClick="Button1_Click" ValidationGroup="Validation" CausesValidation="true" />
                        <asp:Button ID="Button5" runat="server" Text="Approve" OnClick="Button5_Click" ValidationGroup="Validation" CausesValidation="true" OnClientClick="AddConfirm()" />
                    </td>
                </tr>
            </table>
            <br />
            <br />
        </center>

    </asp:Panel>
    <asp:Panel ID="popupPanelFaculty" runat="server" CssClass="modelPopup" BackColor="#E0EBAD" Visible="false">
        <br />
        <br />
        <center>
            <table style="background-color: white">
                <tr>
                    <td colspan="2">

                        <asp:GridView ID="GridViewFaculty" runat="server" AllowSorting="true" AutoGenerateColumns="false">

                            <Columns>
                                <asp:TemplateField HeaderText="Select">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="cbSelect" runat="server"></asp:CheckBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ID">
                                    <ItemTemplate>
                                        <%--  <asp:TextBox ID="TextBoxFBudgetId" runat="server" Value='<%# Eval("BudgetId")%>'  >--%>
                                        <asp:Label ID="TextBoxFBudgetId" runat="server" Text='<%# Eval("BudgetId") %>'></asp:Label>
                                        <%--  </asp:TextBox>--%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Amount">
                                    <ItemTemplate>
                                        <%--  <asp:TextBox ID="TextBoxFAmount" runat="server" Value='<%# Eval("Amount")%>'  >--%>
                                        <asp:Label ID="TextBoxFAmount" runat="server" Text='<%# Eval("Amount") %>'></asp:Label>
                                        <%--    </asp:TextBox>--%>
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
                        <asp:SqlDataSource ID="SqlDataSourceFaculty" runat="server" ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" SelectCommand=""></asp:SqlDataSource>
                    </td>
                </tr>
            </table>
            <asp:Button ID="Button2" runat="server" Text="Select" OnClick="Button2_Click" />
            <asp:Button ID="Buttonexit" runat="server" Text="EXIT" />
        </center>
        <br />
        <br />
    </asp:Panel>
    <asp:Panel ID="popupPanelstudent" runat="server" CssClass="modelPopup" BackColor="#E0EBAD" Visible="false">
        <br />
        <br />
        <center>
            <table style="background-color: white">
                <tr>
                    <td colspan="2">

                        <asp:GridView ID="GridViewStudent" runat="server" AllowSorting="true" AutoGenerateColumns="false">
                            <Columns>
                                <asp:TemplateField HeaderText="Select">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="csSelect" runat="server"></asp:CheckBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ID">
                                    <ItemTemplate>
                                        <%--    <asp:TextBox ID="TextBoxSBudgetId" runat="server" Value='<%# Eval("BudgetId")%>'  >--%>
                                        <asp:Label ID="TextBoxSBudgetId" runat="server" Text='<%# Eval("BudgetId") %>'></asp:Label>
                                        <%--   </asp:TextBox>--%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Amount">
                                    <ItemTemplate>
                                        <%--  <asp:TextBox ID="TextBoxSAmount" runat="server" Value='<%# Eval("Amount")%>'  >
       
                    </asp:TextBox>--%>
                                        <asp:Label ID="TextBoxSAmount" runat="server" Text='<%# Eval("Amount") %>'></asp:Label>
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
                        <asp:SqlDataSource ID="SqlDataSourceStudent" runat="server" ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" SelectCommand=""></asp:SqlDataSource>
                    </td>
                </tr>
            </table>
            <asp:Button ID="Button4" runat="server" Text="Select" OnClick="Button4_Click" />
            <asp:Button ID="Button3" runat="server" Text="EXIT" />
        </center>
        <br />
        <br />
    </asp:Panel>
    <asp:Panel ID="PanelseedMoney" runat="server" CssClass="modelPopup" BackColor="#E0EBAD" Visible="false">
        <br />
        <br />
        <center>
            <table style="background-color: white">
                <tr>
                    <td colspan="2">

                        <asp:GridView ID="GridViewSeedMoney" runat="server" AllowSorting="true" AutoGenerateSelectButton="true"
                            OnSelectedIndexChanged="GridViewSeedMoney_SelectedIndexChanged" EmptyDataText="No Data Found" OnRowDataBound="GridViewSeedMoney_RowDataBound" AutoGenerateColumns="false">
                            <Columns>
                                <asp:TemplateField HeaderText="ID">
                                    <ItemTemplate>
                                        <asp:Label ID="LabelSId" runat="server" Text='<%# Eval("Id") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="FromDate">
                                    <ItemTemplate>

                                        <asp:Label ID="LabelSFromDate" runat="server" Text='<%# Eval("FromDate","{0:dd/MM/yyyy}") %>' DataFormatString="{0:dd/MM/yyyy}"></asp:Label>

                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ToDate">
                                    <ItemTemplate>

                                        <asp:Label ID="LabelSToDate" runat="server" Text='<%# Eval("ToDate","{0:dd/MM/yyyy}") %>' DataFormatString="{0:dd/MM/yyyy}"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status">
                                    <ItemTemplate>

                                        <asp:Label ID="LabelSStatus" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Type" Visible="false">
                                    <ItemTemplate>

                                        <asp:Label ID="LabelSType" runat="server" Text='<%# Eval("Type") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Active">
                                    <ItemTemplate>

                                        <asp:Label ID="LabelSActive" runat="server" Text='<%# Eval("Active") %>'></asp:Label>
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
                        <asp:SqlDataSource ID="SqlDataSourceseedMoney" runat="server" ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" SelectCommand=""></asp:SqlDataSource>
                    </td>
                </tr>
            </table>
            <br />
            <br />
            <%--   <asp:Button ID="Button6" runat="server" Text="Select" OnClick="Button4_Click" />--%>
            <asp:Button ID="Button7" runat="server" Text="EXIT" />
        </center>
        <br />
        <br />
    </asp:Panel>
    <%-- <asp:CustomValidator ID="CustomValidator1" ErrorMessage="Please select the Category for faculty" Display="None" Enabled="false" ValidationGroup="Validation"
    ForeColor="Red" ClientValidationFunction="ValidateCheckBoxList" runat="server" />
     <asp:CustomValidator ID="CustomValidator2" ErrorMessage="Please select the Category for Student" Display="None" Enabled="false" ValidationGroup="Validation"
    ForeColor="Red" ClientValidationFunction="ValidateCheckBoxList1" runat="server" />--%>

    <asp:HiddenField ID="senderID" runat="server" />
    <asp:HiddenField ID="rowVal" runat="server" />
    <asp:HiddenField ID="myHiddenOldProjecttype" runat="server" />
    <asp:HiddenField ID="StatusofSeedMoney" runat="server" />
</asp:Content>

