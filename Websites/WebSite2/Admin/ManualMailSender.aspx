<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage/MasterPageRMS.master" AutoEventWireup="true" CodeFile="ManualMailSender.aspx.cs" Inherits="ManualMailSender" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <center>
 <div>
 <asp:Button ID="BtnSend" runat="server" Text="Send Mail" OnClick="BtnSend_Click" />
 </div>
</center>
</asp:Content>

