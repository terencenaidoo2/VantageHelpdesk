<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master" CodeBehind="ssolegacy.aspx.cs" Inherits="VantageHelpdesk.ssolegacy" %>
 
<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">
    <%@ Register Src="~/Account/OpenAuthProviders.ascx" TagPrefix="uc" TagName="OpenAuthProviders" %>

    <div class="accountHeader">
    <h2 runat="server" ID="PageHeader">Single sign-on</h2>
    <p runat="server" ID="PageDescription">Please enter and confirm your password below. You will only need to do this once; the next time you access this application you will be signed in automatically.</p>
    <p style="color:red">
      <asp:Literal runat="server" ID="ErrorMessage" />
    </p>
</div>
<br />
<div class="form-horizontal" id="login_form" runat="server">
    <div class="form-group">
        <asp:Label runat="server" AssociatedControlID="txtPassword" CssClass="col-md-2 control-label">Password</asp:Label>
        <div class="col-md-4">
            <asp:TextBox runat="server" ID="txtPassword" CssClass="form-control" TextMode="Password" />
            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtPassword" CssClass="text-danger" ForeColor="Red"  ErrorMessage="The Password field is required." />
        </div>
    </div>
    <div class="form-group">
        <asp:Label runat="server" AssociatedControlID="txtConfirmPassword" CssClass="col-md-2 control-label">Confirm Password</asp:Label>
        <div class="col-md-4">
            <asp:TextBox runat="server" ID="txtConfirmPassword" TextMode="Password" CssClass="form-control" />
            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtConfirmPassword" CssClass="text-danger" Display="Dynamic" ForeColor="Red"  ErrorMessage="The Confirm Password field is required." />
            <asp:CompareValidator ID="cvPass" runat="server" ControlToCompare="txtPassword" ControlToValidate="txtConfirmPassword" Display="Dynamic" ForeColor="Red" SetFocusOnError="true" ErrorMessage="The passwords entered do no match"></asp:CompareValidator>
        </div> 
    </div>
    <div class="form-group">
        <div class="col-md-offset-2 col-md-4">
            <asp:Button runat="server" OnClick="btnLogin_Click" ID="btnLogin" Text="Login" CssClass="btn btn-default" />
        </div>
    </div>
</div>
 
</asp:Content>