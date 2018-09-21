<%@ Page Title="Two-Factor Authentication" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TwoFactorAuth.aspx.cs" Inherits="VantageHelpdesk.Account.TwoFactorAuth" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
        <h2><%: Title %>.</h2>
    <asp:PlaceHolder runat="server" ID="sendcode">
        <section>
            <h4>Two-Factor Authentication - Validating Application Access...</h4>
            <hr />
        </section>
    </asp:PlaceHolder>
    <asp:PlaceHolder runat="server" ID="verifycode" Visible="false">
    </asp:PlaceHolder>
</asp:Content>
