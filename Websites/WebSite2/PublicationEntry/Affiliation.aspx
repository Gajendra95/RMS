<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPageRMS.master" AutoEventWireup="true" CodeFile="Affiliation.aspx.cs" Inherits="PublicationEntry_Affiliation" %>
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
     <asp:Panel ID="panel3" runat="server" ForeColor="Black"  Font-Bold="true" Style="background-color:#E0EBAD" > 
  <center>
           <asp:RadioButtonList ID="RadioButtonList2" runat="server"  RepeatDirection="Horizontal" AutoPostBack="true" Style="border-style:inset none none inset;" OnSelectedIndexChanged="RadioButtonList2_SelectedIndexChanged">
                      <asp:ListItem Value="F" Selected="True">Faculty</asp:ListItem>       
                      <asp:ListItem Value="S"  >Student</asp:ListItem>              
                  </asp:RadioButtonList>
       </center>
         </asp:Panel>
    <br />

     <asp:Panel ID="panelSearchPub" runat="server" ForeColor="Black"  Font-Bold="true" Style="background-color:#E0EBAD" > 
     
         <center> <asp:Label ID="lblEmpDetailSearch" runat="server" Text="Employee Affiliation detail" Font-Bold="true"  ></asp:Label></center>

            <br />
<center>
        <table>
            <tr>
                <td > 
                     <%-- <asp:Label ID="Label2" runat="server" Text="Employee ID :  "></asp:Label>--%>
                     <asp:RadioButtonList ID="RadBtnListEmpDetails" runat="server"  RepeatDirection="Horizontal" AutoPostBack="true" Style="border-style:inset none none inset;" OnSelectedIndexChanged="RadBtnListEmpDetails_SelectedIndexChanged">
                      <asp:ListItem Value="E" Selected="True">Employee ID</asp:ListItem>       
                      <asp:ListItem Value="M"  >Email ID</asp:ListItem>              
                  </asp:RadioButtonList>
                </td>
          
            <td >
                   <asp:TextBox ID="txtEmpDetails" runat="server" Width="141px" Height="18px"></asp:TextBox><asp:Label ID="LabelEmailID" runat="server" Text="@manipal.edu" Visible="false"></asp:Label>
                 <asp:Button ID="BtnSearch" runat="server" Text="Search" OnClick="BtnSearch_Click" Width="105px" Height="22px"  />
            </td>
              
            </tr>
           
        </table>
    <br />
       <asp:Label ID="LabelNote" runat="server" Text="No data found" Visible="false"></asp:Label>
    <br />
    </center>
</asp:Panel>
   
    <asp:Panel ID="panelStudentsearch" runat="server" ForeColor="Black"  Font-Bold="true" Style="background-color:#E0EBAD" Visible="false" > 
     <center> <asp:Label ID="LabelStudentsearch" runat="server" Text="Student Affiliation detail" Font-Bold="true"  ></asp:Label></center>

            <br />
<center>
        <table>
            <tr>
                <td > 
                     <%-- <asp:Label ID="Label2" runat="server" Text="Employee ID :  "></asp:Label>--%>
                     <asp:RadioButtonList ID="RadioButtonList1" runat="server"  RepeatDirection="Horizontal" AutoPostBack="true" Style="border-style:inset none none inset;" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged">
                      <asp:ListItem Value="R" Selected="True">Roll No</asp:ListItem>       
                      <asp:ListItem Value="I"  >Email ID</asp:ListItem>              
                  </asp:RadioButtonList>
                </td>
          
            <td >
                   <asp:TextBox ID="TextBox1" runat="server" Width="141px" Height="18px"></asp:TextBox>
                 <asp:Button ID="Buttonseachstudent" runat="server" Text="Search" OnClick="Buttonseachstudent_Click" Width="105px" Height="22px"  />
            </td>
              
            </tr>
           
        </table>
    <br />
       <asp:Label ID="Label7" runat="server" Text="No data found" Visible="false"></asp:Label>
    <br />
    </center>
</asp:Panel>
    <br />

     <asp:Panel ID="panel1" runat="server" ForeColor="Black"  GroupingText="Author Details"  Font-Bold="true" Style="background-color:#E0EBAD" Visible="false" > 
        <center>
         <table>
            <tr>
                <td style="width:150px">
                    <asp:Label ID="Label1" runat="server" Text="Author Name :  "></asp:Label>
                </td>
                <td>
                    <asp:Label ID="LblauthorName" runat="server"></asp:Label>
                </td>
            </tr>
             <tr>
                <td style="width:150px">
                    <asp:Label ID="Label3" runat="server" Text="Email Id  :"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="LblEmailID" runat="server"></asp:Label>
                </td>
            </tr>
             <tr>
                 <td>

                 </td>
             </tr>
             <tr>
                <td>
                    <asp:Label ID="Label5" runat="server" Text="Affiliation  :"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="LAffiliation" runat="server"></asp:Label>
                </td>
            </tr>
              <tr>
                <td>
                    
                </td>
                <td>
                    <asp:Label ID="Lblinstitute" runat="server"></asp:Label>
                </td>
            </tr>

              <tr>
                <td>

                </td>
                <td>
                    <asp:Label ID="Lbldepartment" runat="server"></asp:Label>
                </td>
            </tr>

           

        </table>
              <br />
              <asp:Label ID="LabelOR" runat="server" Text="OR"></asp:Label>
            <br />

            <table>
                 <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="Center Affiliation  :"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="LAffiliation1" runat="server"></asp:Label>
                </td>
                      <td>
                    <asp:Label ID="Lblinstitute1" runat="server"></asp:Label>
                </td>
            </tr>     
                   
                  <tr>
                <td>
                    
                </td>
                <td>
                    <asp:Label ID="Lblinstitute2" runat="server"></asp:Label>
                </td>
            </tr>
              <tr>
                <td>

                </td>
                <td>
                    <asp:Label ID="Lbldepartment1" runat="server"></asp:Label>
                </td>
            </tr>
            </table>
             </center>
          </asp:Panel>
    <asp:Panel ID="panelStddetails" runat="server" ForeColor="Black"  GroupingText="Student Details"  Font-Bold="true" Style="background-color:#E0EBAD" Visible="false" > 
        <center>
         <table>
            <tr>
                <td>
                    <asp:Label ID="Label4" runat="server" Text="Student Name :  "></asp:Label>
                </td>
                <td>
                    <asp:Label ID="LabelSname" runat="server"></asp:Label>
                </td>
            </tr>
             <tr>
                <td>
                    <asp:Label ID="Label9" runat="server" Text="Email Id  :"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="LabelSEmailID" runat="server"></asp:Label>
                </td>
            </tr>
             <tr>
                 <td>

                 </td>
             </tr>
             <tr>
                <td>
                    <asp:Label ID="Label11" runat="server" Text="Affiliation  :"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="Label1Saffiliation" runat="server"></asp:Label>
                </td>
            </tr>
              <tr>
                <td>
                    
                </td>
                <td>
                    <asp:Label ID="LabelSinst" runat="server"></asp:Label>
                </td>
            </tr>

              <tr>
                <td>

                </td>
                <td>
                    <asp:Label ID="LabelsClass" runat="server"></asp:Label>
                </td>
            </tr>

           

        </table>
          
           
       
             </center>
          </asp:Panel>
    <br />
    <br />
    

   
                <br />
   
   
               
        <br />
       
         
    <asp:HiddenField ID="rowVal" runat="server" />
    <asp:HiddenField ID="senderID" runat="server" />



</asp:Content>

