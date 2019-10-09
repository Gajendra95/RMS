<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UsefulLinks.aspx.cs" Inherits="Admin_UsefulLinks" %>
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
                        <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/Login.aspx">
                            <asp:Label ID="Label3" runat="server" ForeColor="Black" Text="Login"></asp:Label></asp:HyperLink>
                    </div>
                </div>
                <br />
            </asp:Panel>

            <asp:Panel ID="Panel1" runat="server" Style="background: #E0EBAD" Height="500px" >
                <div style="margin-left: 170px; height: 456px; margin-top: 50px;background: #E0EBAD">
                    <div style="margin-left: 80px; position: absolute;"> 
                        <br />   
                         <asp:Panel ID="Panel5" runat="server" Style="background: #E0EBAD" GroupingText="Useful Links" Width="762px">
            <table >
                 <tr>
                    <td>
                       
                       <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/new4.png" Width="30px" Height="12px"/>
                        <asp:LinkButton ID="LinkButtonGrandChallenge" runat="server" Text="Grand Challenge Manipal" Style="color: navy; font-weight: bold;font-size:15px" OnClick="LinkButtonGrandChallengeFlyer_Click" /><br />
                    </td>
                </tr>
                 <tr>
                    <td>
                       
                       <asp:Image ID="Image3" runat="server" ImageUrl="~/Images/new4.png" Width="30px" Height="12px"/>
                        <asp:LinkButton ID="LinkButtonGrandChallengeFlyer" runat="server" Text="Grand Challenge Manipal - Details" Style="color: navy; font-weight: bold;font-size:15px" OnClick="LinkButtonGrandChallenge_Click" /><br />
                    </td>
                </tr>
                <tr>
                    <td>
                       
                       <%-- <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/new4.png" Width="30px" Height="12px"/>--%>
                        <asp:LinkButton ID="LinkButtondiscontinued" runat="server" Text="Discontinued Journals list May 2019 " Style="color: navy; font-weight: bold;font-size:15px" OnClick="LinkButtondiscontinued_Click" /><br />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:LinkButton ID="LinkButtonDownloadPdf" runat="server" Text="A Glimpse of the DST Schemes" Style="color: navy; font-weight: bold;font-size:15px" OnClick="LinkButtonDownloadPdf_Click" />

                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:LinkButton ID="LinkBtnGrantCalls" runat="server" Text="Grant Calls" Style="color: navy; font-weight: bold;font-size:15px" OnClientClick="window.open('http://muportal/GrantsDetails.aspx');" /><br />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:LinkButton ID="lnkFAIR" runat="server" Text="FAIR" Style="color: navy; font-weight: bold;font-size:15px" OnClientClick="window.open('http://muportal/doc.aspx?doc=cms/docs/circular/Registrar/Staff/2018.05.18%20Faculty%20Award%20Incentive%20for%20Research%20Publication%20Policy.pdf');" /><br />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:LinkButton ID="lnkPRIASE" runat="server" Text="PRAISE" Style="color: navy; font-weight: bold;font-size:15px" OnClientClick="window.open('http://muportal/doc.aspx?doc=cms/docs/circular/Registrar/Students/2018.05.18%20Policy%20on%20Publication%20and%20Research%20Award%20Incentive%20for%20Students%20to%20Excel.pdf');" /><br />
                    </td>
                </tr>
                  <tr>
                    <td>
                        <asp:LinkButton ID="lnkBankDetail" runat="server" Text="Bank Details Form" Style="color: navy; font-weight: bold;font-size:15px" OnClientClick="LinkButtonBankDetails_Click" OnClick="lnkBankDetail_Click" /><br />
                    </td>
                </tr>

                 <tr>
                    <td>
                        <asp:LinkButton ID="lnkSCopusIndex" runat="server" Text="Scopus Indexed Journal list " Style="color: navy; font-weight: bold;font-size:15px"  OnClientClick="window.open(' https://journalmetrics.scopus.com/');" /><br />
                    </td>
                </tr>

                 <tr>
                    <td>
                        <asp:LinkButton ID="lnkJCR" runat="server" Text="Download JCR indexed Journal list from Web of Science" Style="color: navy; font-weight: bold;font-size:15px" OnClick="lnkJCR_Click"  /><br />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:LinkButton ID="lnkSeedMoneyFaculty" runat="server" Text="Seed Money For Faculty" Style="color: navy; font-weight: bold;font-size:15px" OnClientClick="window.open('http://muportal/doc.aspx?doc=cms/docs/circular/Registrar/Staff/2018.06.07%20Policy%20on%20Seed%20Money%20for%20Faculty%20Research.pdf');" /><br />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:LinkButton ID="lnkSeedMoneyStudent" runat="server" Text="Seed Money For Student " Style="color: navy; font-weight: bold;font-size:15px" OnClientClick="window.open('http://muportal/doc.aspx?doc=cms/docs/circular/Registrar/Students/2018.06.07%20Policy%20on%20Seed%20Funding%20for%20UG-PG%20Students%20Research.pdf');" /><br />
                    </td>
                </tr>
                  <tr>
                    <td>
                        <asp:LinkButton ID="lnkAffilation" runat="server" Text="Format for Affilation " Style="color: navy; font-weight: bold;font-size:15px" OnClientClick="return Open()" /><br />
                    <asp:ModalPopupExtender ID="ModalPopupExtender" runat="server" TargetControlID="lnkAffilation" PopupControlID="popup"
                    BackgroundCssClass="modelBackground"  >
                                  </asp:ModalPopupExtender>
                    </td>
                </tr>
                 <tr>
                    <td>

                     <asp:LinkButton ID="LinkButton2" runat="server" Text="Affilation Hierarchy of MAHE" Style="color: navy; font-weight: bold;font-size:15px" OnClick="LinkButton2_Click" />
                    </td>
                </tr>
                 <tr>
                    <td>
                        <asp:LinkButton ID="lnkUGCNotification" runat="server" Text="UGC Notification " Style="color: navy; font-weight: bold;font-size:15px" OnClick="lnkUGCNotification_Click"  /><br />
                    </td>
                </tr>
                 <tr>
                    <td>
                        <asp:LinkButton ID="lnkSubjectWiseJournallist" runat="server" Text="Subject Wise Journal list" Style="color: navy; font-weight: bold;font-size:15px" OnClick="lnkSubjectWiseJournallist_Click"  /><br />
                    </td>
                </tr>
               <%-- <tr>
                    <td>
                        <asp:LinkButton ID="lnkPubUserMamnual" runat="server" Text="Publication User Manual" Style="color: navy; font-weight: bold;font-size:15px"  OnClick="lnkPubUserMamnual_Click" /><br />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:LinkButton ID="lnkProjectUserManual" runat="server" Text="Project User Manual" Style="color: navy; font-weight: bold;font-size:15px "  OnClick="lnkProjectUserManual_Click" /><br />
                    </td>
                </tr>--%>

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

            <asp:Panel ID="popup" runat="server" CssClass="modelPopup" Style="width: 900px; height: 450px; background-color: ghostwhite;">
                <br />
                <br />
                   <div  style="margin-left:80px" > 
                        <br />
                        <b><u> The format of author details is as follows:</u></b>  
                        <br />
                        <br />
                              <u>Author/ Authors Name </u>   
                       <br />
                                <u> Terminal degree and Designation (as per the journal requirement)</u>
                       <br />
                                <u> Name of the department/ departments, Name of the institution/ institutions,</u>
                       <br />
                               <u>  Manipal Academy of Higher Education, Manipal, Karnataka, India-576104</u>
                       <br />
                       <br />
                       <br />
                       <b> Note:  </b> 
                       <br />
                        <b> Authors are requested not to use short form for Institutions or University name</b> 

 

                      </div>
                <br />
                <br />
                   <center>
                <div>
                    <asp:Button ID="Buttonexit" runat="server" Text="EXIT" OnClick="exit1" />
                </div>
                    </center>
        </asp:Panel>
        </div>
    </form>
</body>
</html>
