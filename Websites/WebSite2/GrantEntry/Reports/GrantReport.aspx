<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPageRMS.master" AutoEventWireup="true" CodeFile="GrantReport.aspx.cs" Inherits="GrantEntry_Reports_GrantReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <center>

        <b></b>
    </center>
    <br />
    <br />

    <table style="width: 100%">

        <tr>
            <td>
                <b>Project Reports</b></td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>

        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>


        <tr>
            <td>
                <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">ProjectType wise (Applied Date wise)</asp:LinkButton>


            </td>
        </tr>
            <tr>
            <td>
                <asp:LinkButton ID="LinkButton3" runat="server" OnClick="LinkButton3_Click">ProjectType wise (Entry Date wise)</asp:LinkButton>


            </td>
        </tr>
        
        <tr>
            <td>
                <asp:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton2_Click">Project-Institute-DeptWise (Applied Date wise)</asp:LinkButton>


            </td>
        </tr>

            
        
        <tr>
            <td>
                <asp:LinkButton ID="LinkButton4" runat="server" OnClick="LinkButton4_Click">Project-Institute-DeptWise (Entry Date wise)</asp:LinkButton>


            </td>
        </tr>

         <tr>
            <td>
                <asp:LinkButton ID="LinkButton5" runat="server" OnClick="LinkButton5_Click">ProjectType wise (Sanction Date wise)</asp:LinkButton>


            </td>
        </tr>
        <tr>
            <td>
                <asp:LinkButton ID="LinkButton6" runat="server" OnClick="LinkButton6_Click">Year Wise Grant Recieved Amount Details</asp:LinkButton>


            </td>
        </tr>
         <tr>
            <td>
                <asp:LinkButton ID="LinkButton7" runat="server" OnClick="LinkButton7_Click">Project Details-Inter-Institute (Applied Date wise)</asp:LinkButton>


            </td>
        </tr>
         <tr>
            <td>
                <asp:LinkButton ID="LinkButton8" runat="server" OnClick="LinkButton8_Click">Project Details-Inter-Institute (Entry Date wise)</asp:LinkButton>


            </td>
        </tr>       
            <tr>
            <td>
                <asp:LinkButton ID="LinkButton9" runat="server" OnClick="LinkButton9_Click">Percentage Sharing details(Created Date Wise)</asp:LinkButton>


            </td>
        </tr>
    </table>
</asp:Content>



