<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPageRMS.master" AutoEventWireup="true" CodeFile="ManageSeedMoneyCategory.aspx.cs" Inherits="ManageJournal_ManageSeedMoneyCategory" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  
     <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" DisplayMode="BulletList" 
     HeaderText="Validation issues" ShowSummary="False" ValidationGroup="Validation" />
    <asp:ToolkitScriptManager ID="Scriptmanager1" runat="server"/>
    <asp:Panel ID="panel" runat="server" GroupingText="" Font-Bold="true"  BackColor="#E0EBAD">
        <center>
            <br />
            <br />
        <table >
             <%--<tr>
                 <td >
<asp:Label ID="LabelFromDate" runat="server" Text="From Date" ForeColor="Black"   ></asp:Label>
                     </td>
            <td style="width:100px">
<asp:TextBox ID="TextBoxFromDate" runat="server" >
</asp:TextBox> 
<asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="TextBoxFromDate" Format="dd/MM/yyyy" >
</asp:CalendarExtender> 
<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"  ControlToValidate="TextBoxFromDate" Display="None" 
                            ErrorMessage="Please enter From date" InitialValue=" "  ValidationGroup="validation"></asp:RequiredFieldValidator> 
<asp:RegularExpressionValidator ID="RegularExpressionValidator15" runat="server" Display="None"  ControlToValidate="TextBoxFromDate" ValidationGroup="validation"
                ErrorMessage="From date in (dd/mm/yyyy) format" SetFocusOnError="true"  
                ValidationExpression="^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$">
</asp:RegularExpressionValidator>
</td>
                 </tr>--%>
           <%-- <tr>
<td>
<asp:Label ID="LabelToDate" runat="server" Text="To Date" ForeColor="Black"  ></asp:Label>
    </td>
            <td style="width:100px">
<asp:TextBox ID="TextBoxToDate" runat="server"  Style="border-style:inset none none inset;" >
</asp:TextBox>  
<asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="TextBoxToDate" Format="dd/MM/yyyy" >
</asp:CalendarExtender> 
<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"   ControlToValidate="TextBoxToDate" Display="None" 
                            ErrorMessage="Please Enter To Date" InitialValue=" "  ValidationGroup="validation"></asp:RequiredFieldValidator>
<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" Display="None"  ControlToValidate="TextBoxToDate" ValidationGroup="validation"
                ErrorMessage="To date in (dd/mm/yyyy) format" SetFocusOnError="true"  
                ValidationExpression="^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$">
</asp:RegularExpressionValidator>
</td>
                </tr>--%>
            <tr>
                <td><asp:Label ID="Label1" runat="server" Text="AuthorType"></asp:Label> </td>
            <td style="width:100px">
        <asp:DropDownList ID="DropDownListAuthorType" runat="server"  Width="100px" >
             
        <asp:ListItem Value="S">Student</asp:ListItem>
          <asp:ListItem Value="F" >Faculty</asp:ListItem>
                    </asp:DropDownList>
                  </td>
            </tr>
           
           <%-- <tr>
                <td><asp:Label ID="lblActive" runat="server" Text="Enable" ></asp:Label> </td>
            <td style="width:100px">
        <asp:DropDownList ID="DropDownListactive" runat="server" Width="100px" >
        <asp:ListItem Value="Y">Yes</asp:ListItem>
          <asp:ListItem Value="N">No</asp:ListItem>
                    </asp:DropDownList>
                  </td>
            </tr>--%>
           <%-- <tr>
                <td><asp:Label ID="Label3" runat="server" Text="Faculty Category" ></asp:Label> </td>
            <td style="width:100px">
              
              <asp:CheckBoxList ID="CheckboxCategory" runat="server" ForeColor="Black"  DataSourceID="SqlDataSourceCheckboxCheckboxCategory" 
                  RepeatDirection="Vertical" DataTextField="Amount" DataValueField="BudgetId" Enabled="false">
                
                </asp:CheckBoxList>

                
<asp:SqlDataSource ID="SqlDataSourceCheckboxCheckboxCategory" runat="server" ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" 
           SelectCommand="">
     
                </asp:SqlDataSource>
                  </td>

            </tr>--%>
            <%-- <tr>
                <td><asp:Label ID="Label4" runat="server" Text="Student Category" ></asp:Label> </td>
            <td style="width:100px">

               
              <asp:CheckBoxList ID="CheckboxCategory1" runat="server" ForeColor="Black"  DataSourceID="SqlDataSourceCheckboxCheckboxCategory1" 
                  RepeatDirection="Vertical" DataTextField="Amount" DataValueField="BudgetId" Enabled="false">
                
                </asp:CheckBoxList>

                
<asp:SqlDataSource ID="SqlDataSourceCheckboxCheckboxCategory1" runat="server" ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" 
           SelectCommand="">
     
                </asp:SqlDataSource>
                  </td>

            </tr>--%>
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
           <%-- <tr>
                <td><asp:Label ID="LabelRemarks" runat="server" Text="Remarks" ></asp:Label> </td>
            <td>
        <asp:TextBox ID="TextBoxRemarks" runat="server" TextMode="MultiLine" Width="250px"  >
       
                    </asp:TextBox>
                       <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Display="None" ControlToValidate="TextBoxRemarks" ErrorMessage="Enter Remarks" ValidationGroup="Validation"  ></asp:RequiredFieldValidator>
   
                  </td>

            </tr>--%>
              <tr>
                <td><asp:Label ID="Label2" runat="server" Text="Amount" ></asp:Label> </td>
            <td style="width:100px">
     <%--   <asp:TextBox ID="Amount" runat="server" Width="150px"  >
       
                    </asp:TextBox>--%>
                <asp:TextBox ID="txtAmount" runat="server" Text='<%# Eval("Amt", "{0:C}")%>' OnTextChanged="txtAmount_TextChanged" AutoPostBack="true"></asp:TextBox>
<asp:RegularExpressionValidator ID="rev1" runat="server" ControlToValidate="txtAmount" ErrorMessage="Please Enter valid Amount" ValidationExpression="[\$]*\$?\d+(,\d{1,12})?(.\d{1,2})?" Display="None" ValidationGroup="Validation"></asp:RegularExpressionValidator>
                     
   
                  </td>
                

            </tr>
            <tr>
                <td>

                </td>
                <td style="width:100px">
                    <asp:Button ID="Button1" runat="server" Text="Save" OnClick="Button1_Click" ValidationGroup="Validation" />
                </td>
            </tr>
        </table>
            <br />
            <br />
            </center>
        </asp:Panel>







</asp:Content>

