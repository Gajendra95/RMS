<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SubUsefulLinks.aspx.cs" Inherits="SubUsefulLinks" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
 
    <form id="form1" runat="server">
           <style type="text/css">
     .modelBackground
     {
         background-color:Gray;
         filter:alpha(opacity=70);
         opacity:0.7;
     }
     .modelPopup
     {
         border: 3px solid Gray;
         background-color:#EEEEE;
         font-family:Verdana;
         font-size:medium;
         padding:3px;
         width:901px;
         position:absolute;
         overflow:scroll;
         max-height:400px;
     }
     .auto-style50 {
         height: 44px;
         width:200px;
     }
           
     .auto-style51 {
         width: 292px;
     }
           
     .auto-style53 {
         height: 44px;
         width: 104px;
     }
           
     .auto-style54 {
         width: 159px;
     }
           
 </style>
   
    <script src="../Scripts/jquery-1.7.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-1.7.1.min.js" type="text/javascript"></script>
    <script type = "text/javascript">
        function Open() {
            $find('popup').show();
            return false;
        }
    </script>
         <asp:ScriptManager ID="Scriptmanager1" runat="server"/>
        

        <asp:Panel ID="Panel3" runat="server" Style="background: #E0EBAD;margin-top:-7px;" Height="50px">
        </asp:Panel>
        <div>
            <asp:Panel ID="Panel2" runat="server" BorderStyle="None" Style="margin-bottom: 28px; background-color: white">

                <div id="header">
                    <div id="logo" style="margin-top: -5px; margin-left: 50px">
                        <br />
                        <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/MAHE-logo.png" />
                    </div>
                    <div id="logo1" style="margin-top: -40px; margin-left: 390px">
                        <asp:Label ID="Title" runat="server" Text="Research Data Management System" ForeColor="Black" Font-Size="40px"></asp:Label>
                    </div>
                    <div style="float: right; margin-right: 150px">
                        <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/UsefulLinks.aspx">
                            <asp:Label ID="Label3" runat="server" ForeColor="Black" Text="Back"></asp:Label></asp:HyperLink>
                    </div>
                 
                      <div style="float: right; margin-right: 50px">
                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Login.aspx">
                            <asp:Label ID="Label1" runat="server" ForeColor="Black" Text="Login"></asp:Label></asp:HyperLink>
                    </div>
                </div>
                <br />
            </asp:Panel>

            <asp:Panel ID="Panel1" runat="server" Style="background: #E0EBAD" Height="500px" >
                <div style="margin-left: 170px; height: 456px; margin-top: 50px;background: #E0EBAD">
                    <div style="margin-left: 80px; position: absolute;"> 
                        <br />   
                         <asp:Panel ID="Panel5" runat="server" Style="background: #E0EBAD" GroupingText="Subject Wise Journal list Links" Width="762px">
            <table >
                <tr>
                    <td>
                        <asp:LinkButton ID="LinkBtnBooklist" runat="server" Text="Book List" Style="color: navy; font-weight: bold;font-size:15px" OnClick="LinkBtnBooklist_Click" /><br />
                    </td>
                </tr> 
                <tr>
                    <td>
                        <asp:LinkButton ID="LinkBtnBusiness" runat="server" Text="Business and Economics" Style="color: navy; font-weight: bold;font-size:15px" OnClick="LinkBtnBusiness_Click" />

                    </td>
                </tr>               
                <tr>
                    <td>
                        <asp:LinkButton ID="LinkBtnClinical" runat="server" Text="Clinical preclinical" Style="color: navy; font-weight: bold;font-size:15px" OnClick="LinkBtnClinical_Click"  /><br />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:LinkButton ID="LinkBtnComputer" runat="server" Text="Computer Science" Style="color: navy; font-weight: bold;font-size:15px" OnClick="LinkBtnComputer_Click"  /><br />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:LinkButton ID="LinkBtnEngineeeringTech" runat="server" Text="Engineeering and Technology" Style="color: navy; font-weight: bold;font-size:15px" OnClick="LinkBtnEngineeeringTech_Click"  /><br />
                    </td>
                </tr>
                 <%-- <tr>
                    <td>
                        <asp:LinkButton ID="LinkBtnEngineeringComputer" runat="server" Text="Engineering and Computer_Common journals" Style="color: navy; font-weight: bold;font-size:15px" OnClick="LinkBtnEngineeringComputer_Click"  /><br />
                    </td>
                </tr>

                 <tr>
                    <td>
                        <asp:LinkButton ID="LinkBtnLifeScienceClinical" runat="server" Text="Life science and clinical_common journals" Style="color: navy; font-weight: bold;font-size:15px" OnClick="LinkBtnLifeScienceClinical_Click"  /><br />
                    </td>
                </tr>--%>

                 <tr>
                    <td>
                        <asp:LinkButton ID="LinkBtnlifescience" runat="server" Text="Life science" Style="color: navy; font-weight: bold;font-size:15px" OnClick="LinkBtnlifescience_Click"   /><br />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:LinkButton ID="LinkBtnPhysical" runat="server" Text="Physical sciences" Style="color: navy; font-weight: bold;font-size:15px" OnClick="LinkBtnPhysical_Click"  /><br />
                    </td>
                </tr>                          
                 <tr>
                    <td>
                        <asp:LinkButton ID="LinkBtnSocialSceince" runat="server" Text="Social sciences" Style="color: navy; font-weight: bold;font-size:15px" OnClick="LinkBtnSocialSceince_Click"  /><br />
                    </td>
                </tr> 
                  <tr>
                    <td>
                        <asp:LinkButton ID="LinkButtonartsandhumanities" runat="server" Text="Arts and Humanities" Style="color: navy; font-weight: bold;font-size:15px" OnClick="LinkBtnArtsandhumanities_Click"  /><br />
                    </td>
                </tr> 
                 <tr>
                    <td>
                        <asp:LinkButton ID="LinkButtonBusiness" runat="server" Text="Business" Style="color: navy; font-weight: bold;font-size:15px" OnClick="LinkBtnBusines_Click"  /><br />
                    </td>
                </tr> 
                   <tr>
                    <td>
                        <asp:LinkButton ID="LinkButtonComputers" runat="server" Text="Computers" Style="color: navy; font-weight: bold;font-size:15px" OnClick="LinkBtnComputers_Click"  /><br />
                    </td>
                </tr> 
                  <tr>
                    <td>
                        <asp:LinkButton ID="LinkButtonEducation" runat="server" Text="Education" Style="color: navy; font-weight: bold;font-size:15px" OnClick="LinkBtnEducation_Click"  /><br />
                    </td>
                </tr> 

                   <tr>
                    <td>
                        <asp:LinkButton ID="LinkButtonLaw" runat="server" Text="Law" Style="color: navy; font-weight: bold;font-size:15px" OnClick="LinkBtnLaw_Click"  /><br />
                    </td>
                </tr> 
                 <tr>
                    <td>
                        <asp:LinkButton ID="LinkButtonPsychology" runat="server" Text="Psychology" Style="color: navy; font-weight: bold;font-size:15px" OnClick="LinkBtnPsychology_Click"  /><br />
                    </td>
                </tr> 
                  <tr>
                    <td>
                        <asp:LinkButton ID="LinkButtonSocial" runat="server" Text="Social" Style="color: navy; font-weight: bold;font-size:15px" OnClick="LinkBtnSocial_Click"  /><br />
                    </td>
                </tr> 

                   <tr>
                    <td>
                        <asp:LinkButton ID="LinkButtonMultidisciplinary" runat="server" Text="Multidisciplinary" Style="color: navy; font-weight: bold;font-size:15px" OnClick="LinkBtnMultidisciplinary_Click"  /><br />
                    </td>
                </tr> 
            </table>
                             </asp:Panel>
        </div>
                </div>
            </asp:Panel>
            <asp:Panel ID="Panel4" runat="server" Style="background: #E0EBAD">
                <div style="background-color: black; height: 5px;"></div>
                <div id="footer" style="text-align: center">
                    <table align="right">
                        <tr>
                            <td>Best Viewed on Google Chrome</td>
                        </tr>
                    </table>
                    <br />

                    <p>
                        <asp:Label ID="ft" runat="server" ForeColor="Black"> Designed and Developed by SOIS and ISD, Manipal University</asp:Label>
                        <%--<a href="http://www.manipal.edu/" target="_blank"><asp:Label id="Labelft1" runat="server" ForeColor="Black"> @manipal.edu</asp:Label></a>--%> </p>

                </div>
            </asp:Panel>
        </div>
    </form>
</body>
</html>
