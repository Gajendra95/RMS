<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPageRMS.master" AutoEventWireup="true" CodeFile="ManageStudent.aspx.cs" Inherits="UserManagement_ManageStudent" MaintainScrollPositionOnPostback="true" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   
   <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" DisplayMode="BulletList" 
     HeaderText="Validation issues" ShowSummary="False" ValidationGroup="Validation" />
<asp:Panel ID="panel" runat="server" GroupingText="" Font-Bold="true"  BackColor="#E0EBAD">

    <style type="text/css">
.modelBackground
{
	background-color:Gray;
	filter:alpha(opacity=70);
	opacity:0.7;
}

.modelPopup
{
	background-color:#EEEEE;
	border-width:1px;
	border-style:solid;
	border-color:Gray;
	font-family:Verdana;
	font-size:medium;
	padding:3px;
	width:450px;
	position:absolute;
	overflow:scroll;
	max-height:400px;
}

.blnkImgCSS
{
	opacity: 0;
	filter: alpha(opacity=0);
}

</style>
    
    
     <script type = "text/javascript">
         function ClearConfirm() {
             var rid = document.getElementById('<%= txtuserid.ClientID %>').value;

             var confirm_value2 = document.createElement("INPUT");
             confirm_value2.type = "hidden";
             confirm_value2.name = "confirm_value2";
             confirm_value2.value = "Yes";

             if (rid != "") {
                 if (confirm("Data Will be lost. Do you want to continue?"))
                     confirm_value2.value = "Yes";
                 else
                     confirm_value2.value = "No";
             }

             document.forms[0].appendChild(confirm_value2);
         }


         function setRow(obj) {

             var sndID = obj.id;
             var sndrID = document.getElementById('<%= senderID.ClientID %>');
             sndrID.value = sndID;

         }
         </script>

    

    <cc1:ToolkitScriptManager ID="Scriptmanager1" runat="server"/>
 <table style="margin-left:350px;">
     
            <tr><td>Roll No
                   <asp:Label ID="Label6" runat="server" ForeColor="Red" Text="*"></asp:Label>
                </td>
<td><asp:TextBox ID="txtuserid" runat="server" OnTextChanged="DeptcodeTextChanged" AutoPostBack="true" ></asp:TextBox>
          
<asp:RequiredFieldValidator ID="req" runat="server" ControlToValidate="txtuserid" Display="None" ErrorMessage="Roll No Required" ValidationGroup="Validation"></asp:RequiredFieldValidator> 
            <asp:ImageButton ID="imageBkCbtn" runat="server" ImageUrl="~/Images/srchImg.gif" OnClientClick="setRow(this)" OnClick="showPop" /></td>

                 <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="imageBkCbtn" PopupControlID="popupPanelBaCode"
                    BackgroundCssClass="modelBackground"  >
                 </cc1:ModalPopupExtender>
       
            </tr>
         <%--   <tr><td>Name</td>
                <td><asp:TextBox ID="txtname" runat="server"  SetFocusOnError="true" ></asp:TextBox>
                  <asp:RequiredFieldValidator ID="reqname" runat="server" ControlToValidate="txtname" Display="None" ErrorMessage="Name Required" ValidationGroup="Validation" ></asp:RequiredFieldValidator>
                </td>
            </tr>--%>
                <%--<tr><td>Prefix</td>
                <td><asp:DropDownList ID="DropDownListPrefix" runat="server"  >
                <asp:ListItem>Dr</asp:ListItem>
                    <asp:ListItem>Mr</asp:ListItem>
                        <asp:ListItem>Mrs</asp:ListItem>
                            <asp:ListItem>Ms</asp:ListItem>
                                  <asp:ListItem>Prof</asp:ListItem>
                </asp:DropDownList>
                       </td>
            </tr>
                <tr><td>First Name</td>
                <td><asp:TextBox ID="TextBoxFname" runat="server"  SetFocusOnError="true" ></asp:TextBox>
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="TextBoxFname" Display="None" ErrorMessage="Name Required" ValidationGroup="Validation" ></asp:RequiredFieldValidator>
                </td>
            </tr>
                <tr><td>Middle Name</td>
                <td><asp:TextBox ID="TextBoxMname" runat="server"  SetFocusOnError="true" ></asp:TextBox>
                       </td>
            </tr>
                <tr><td>Last Name</td>
                <td><asp:TextBox ID="TextBoxLName" runat="server" SetFocusOnError="true" ></asp:TextBox>
                            </td>
            </tr>--%>
      <tr><td>Name
             <asp:Label ID="Label1" runat="server" ForeColor="Red" Text="*"></asp:Label>
          </td>
                <td><asp:TextBox ID="TextBoxName" runat="server" SetFocusOnError="true" ></asp:TextBox>
                      <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"  ControlToValidate="TextBoxName"  ErrorMessage="Name Required" Display="None" ValidationGroup="Validation" ></asp:RequiredFieldValidator>
                            </td>
            </tr>
            <tr><td>EmailId
                   <asp:Label ID="Label2" runat="server" ForeColor="Red" Text="*"></asp:Label>
                </td>
                <td><asp:TextBox ID="txtemailid" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"  ControlToValidate="txtemailid"   ErrorMessage="EmailId Required" Display="None" ValidationGroup="Validation" ></asp:RequiredFieldValidator>
                    </td>
            </tr>
     <tr><td>Sex</td>
                <td>  <asp:DropDownList ID="DropDownListsex" runat="server" >
        <asp:ListItem Value="M">Male</asp:ListItem>
          <asp:ListItem Value="F">Female</asp:ListItem>
                    </asp:DropDownList>
                   <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"  ControlToValidate="txtemailid" ErrorMessage="EmailId Required" Display="None" ValidationGroup="Validation" ></asp:RequiredFieldValidator>--%>
                    </td>
            </tr>
      <tr><td>DOB</td>
                <td><asp:TextBox ID="txtDOB" runat="server" Width="200px" AutoPostBack="true" ></asp:TextBox>
                  
      <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDOB" Format="dd/MM/yyyy">
                    </cc1:CalendarExtender>
               
                <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" Display="None"  ControlToValidate="txtDOB" ValidationGroup="validation"
                ErrorMessage="Presentation Date in (dd/mm/yyyy) format" SetFocusOnError="true"  
                ValidationExpression="^(((0[1-9]|[12]\d|3[01])\/(0[13578]|1[02])\/((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$">

                </asp:RegularExpressionValidator>
                            </td>
            </tr>
      <tr><td>Mobile No</td>
                <td><asp:TextBox ID="txtMobileNo" runat="server" SetFocusOnError="true" ></asp:TextBox>
                            </td>
            </tr>
    <%-- <tr><td>Password</td>
                <td><asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"  ControlToValidate="txtPassword" ErrorMessage="Password Required" Display="None" ValidationGroup="Validation" ></asp:RequiredFieldValidator>
                    </td>
            </tr>--%>
            <tr>
    <td>InstituteName
           <asp:Label ID="Label3" runat="server" ForeColor="Red" Text="*"></asp:Label>
    </td>
       <td>
       <asp:DropDownList ID="DDLinstitutename" runat="server" DataSourceID="SqlDataSource2"  AppendDataBoundItems="true" DataTextField="InstName" DataValueField="InstID" OnSelectedIndexChanged="DDLinstitutename_SelectedIndexChanged" AutoPostBack="true" >
            <asp:ListItem Value="0">--Select--</asp:ListItem>
       </asp:DropDownList>
   <asp:RequiredFieldValidator ID="Reqinstitue" runat="server" ControlToValidate="DDLinstitutename" ErrorMessage="InstituteName Required" ValidationGroup="Validation" Display="None" InitialValue="0" ></asp:RequiredFieldValidator>
    </td>
       <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" SelectCommand="SELECT DISTINCT InstName ,InstID  FROM SISInstitution a,SISInstnHR b where a.InstID=b.Institute_Id"></asp:SqlDataSource>

</tr>
        <tr>
            <td style="width:160px">Course/Branch Name
                   <asp:Label ID="Label4" runat="server" ForeColor="Red" Text="*"></asp:Label>
            </td>
            <td>
        <asp:DropDownList ID="DDLdeptname" AppendDataBoundItems="true" TextFormatString="" runat="server" DataSourceID="SqlDataSource1" DataTextField="ClassName" DataValueField="ClassCode" >
             <asp:ListItem Value="0">--Select--</asp:ListItem>
        </asp:DropDownList>
       <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Display="None" ControlToValidate="DDLdeptname" ErrorMessage="CourseName Required" ValidationGroup="Validation" InitialValue="0" ></asp:RequiredFieldValidator>
            </td>
       <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" SelectCommand=""></asp:SqlDataSource>

</tr>
              
      <%--<tr><td>Bank Account Number
          </td>
                <td><asp:TextBox ID="txtaccno" runat="server" SetFocusOnError="true" ></asp:TextBox>
                            </td>
            </tr>--%>



<%--<tr>
<td><asp:Label ID="lblActive" runat="server" Text="Active" Visible="false"></asp:Label> </td>
            <td>
        <asp:DropDownList ID="DropDownListactive" runat="server" Visible="false" >
        <asp:ListItem Value="Y">Yes</asp:ListItem>
          <asp:ListItem Value="N">No</asp:ListItem>
                    </asp:DropDownList>
                  </td>
      

</tr>--%>

      <%--    <tr><td>Auto_Approved</td>
            <td><asp:RadioButtonList ID="RBLautoapproval" runat="server" 
            RepeatDirection="Horizontal">
            <asp:ListItem Text="Yes" Value="Y" Enabled="true" Selected="True"></asp:ListItem>
            <asp:ListItem Text="No" Value="N"></asp:ListItem>
        </asp:RadioButtonList></td>
        </tr>--%>
            <%--<tr><td>Role_Name</td>
                <td> <asp:DropDownList ID="DDLrolename" runat="server"  AppendDataBoundItems="true" DataSourceID="SqlDataSource3" DataTextField="Role_Name" DataValueField="Role_Id" OnSelectedIndexChanged="DDLrolenameOnSelectedIndexChanged" AutoPostBack="true">
                     <asp:ListItem Value="">--Select--</asp:ListItem>
                     </asp:DropDownList>
                <asp:RequiredFieldValidator ID="ReqRole" runat="server" ControlToValidate="DDLrolename" ErrorMessage="Role_Name Required" ValidationGroup="Validation" Display="None" InitialValue="" ></asp:RequiredFieldValidator> </td>
                <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" SelectCommand="SELECT DISTINCT [Role_Name], [Role_Id] FROM [Role_M] where Role_Id!=5" ></asp:SqlDataSource>
  </tr>--%>
 <%-- <tr>
  <td>
<asp:Label ID="PubDept" runat="server" Visible="false">Department</asp:Label>  
  </td>
  <td>
  <asp:RadioButtonList ID="RadioButtonListDeparmentPubincharge" runat="server" Visible="false" RepeatDirection="Horizontal">

  </asp:RadioButtonList>
  </td>
  </tr>--%>
</table>
<br />
<br />
        <div style="margin-left:430px;">
        <asp:Button ID="btninsert" runat="server" Text="INSERT" OnClick="btn_insert" ValidationGroup="Validation" CausesValidation="true"/>
        <asp:Button ID="btnupdate" runat="server" Text="UPDATE" OnClick="btnupdate_Click" Enabled="false"  ValidationGroup="Validation" CausesValidation="true"/>
        <asp:Button ID="ButClear" runat="server" Text="CLEAR" OnClick="BtnClear_Click"/>
        <asp:Button ID="Butview" runat="server" Text="VIEW" OnClick="Butview_Click" />         
        <asp:Label ID="lblStatus" runat="server"></asp:Label>
            </div>
    <br />
    <br />
    <center>
    <div>
       <asp:GridView ID="GridViewUser" runat="server" DataSourceID="SqlDataSourceGridview" PagerSettings-PageButtonCount="10" AllowPaging="True" 
       AutoGenerateColumns="False" CellPadding="4" DataKeyNames="User_Id" 
       ForeColor="#333333" GridLines="None" Width="1000px">
             <AlternatingRowStyle BackColor="White" />
             <Columns>
   <asp:BoundField DataField="EmailId" HeaderText="EmailId" SortExpression="EmailId" />
    <asp:BoundField DataField="User_Id" HeaderText="Roll No" ReadOnly="True" SortExpression="User_Id" />
 <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
 <asp:BoundField DataField="Department_Id" HeaderText="Course Name" SortExpression="Department_Id" />
  <asp:BoundField DataField="InstituteId" HeaderText="Institute Name" SortExpression="InstituteId" />
             </Columns>
    <HeaderStyle BackColor="#0B532D" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#0B532D" ForeColor="#F0F8FF" HorizontalAlign="Center" />
        <RowStyle BackColor="#FFFFFF" ForeColor="#333333" />
        <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
        <SortedAscendingCellStyle BackColor="#FDF5AC" />
        <SortedAscendingHeaderStyle BackColor="#4D0000" />
        <SortedDescendingCellStyle BackColor="#FCF6C0" />
        <SortedDescendingHeaderStyle BackColor="#820000" />
        <EditRowStyle BorderColor="#FF6600" BorderStyle="Solid" />
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSourceGridview" runat="server"
       ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" SelectCommand="SELECT  [EmailID1] as EmailId, [RollNo] as User_Id,[Name],b.ClassName as [Department_Id],d.Institute_Name as [InstituteId] FROM [SISStudentGenInfo] a,SISClass b,SISInstnHR c,Institute_M d where a.ClassCode=b.ClassCode and a.InstID=c.Institute_Id and c.Institute_Id=d.Institute_Id and d.Institute_Id=a.InstID">
        </asp:SqlDataSource>
        <br />
        <br />
       <asp:Button ID="Butback" runat="server" Text="BACK" OnClick="btn_back" />
   <br />
   </div>
        </center>
   </asp:Panel>
         
<asp:Panel ID="popupPanelBaCode" runat="server" Visible="false" CssClass="modelPopup" BackColor="GhostWhite">
    <table style="background-color:white">

<tr>
<%--<th>Search Name: </th>--%>
<td >
      
  <asp:Label ID="Label5" runat="server" Text="Search Name"></asp:Label>
           </td> <td ><asp:TextBox ID="USerIdSrch" runat="server"></asp:TextBox> </td>
  <%--  <th>Roll No: </th>--%>
<td > <asp:Label ID="Label7" runat="server" Text="Roll No"></asp:Label></td> <td ><asp:TextBox ID="TextBoxRollno" runat="server"></asp:TextBox> </td>
    <td>
         <asp:Button ID="Button1" runat="server" Text="Search" OnClick="UserIdChanged" />
    </td>
</tr>
       <%-- <tr>
    <td colspan="2" align="center">
    <asp:Button ID="searchid" runat="server" Text="Search" OnClick="UserIdChanged" />
</td>
</tr>   --%>

    </table>
  <table>
      <tr>
<td>
        
<asp:GridView ID="popGridUser" runat="server" AutoGenerateSelectButton="true" OnSelectedIndexChanged="popSelected" AllowSorting="true">
<FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510"  />
        <HeaderStyle BackColor="#0B532D" Font-Bold="True" ForeColor="White" />
        <PagerStyle ForeColor="#0B532D" HorizontalAlign="Center" />
        <RowStyle BackColor="#FFFFFF" ForeColor="#000000" />
        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#FFF1D4" />
        <SortedAscendingHeaderStyle BackColor="#B95C30" />
        <SortedDescendingCellStyle BackColor="#F1E5CE" />
        <SortedDescendingHeaderStyle BackColor="#93451F" />
</asp:GridView>
<asp:SqlDataSource ID="SqlDataSourceUSer" runat="server" ConnectionString="<%$ ConnectionStrings:RMSConnectionString %>" SelectCommand="SELECT top 10 RollNo as [User_Id], Name  as Name  FROM [SISStudentGenInfo]">
</asp:SqlDataSource>
    </td>
</tr>
</table>
     <asp:Button ID="Buttonexit" runat="server" Text="EXIT" />
</asp:Panel> 
      <asp:HiddenField ID="senderID" runat="server" />

</asp:Content>

