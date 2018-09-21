<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddUserTeamMapping.aspx.cs" Inherits="VantageHelpdesk.Pages.hd.AddUserTeamMapping" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container" style="width: 100%;">
        <div class="page-content-inner">
            <div class="row">
                <div class="col-md-12 col-sm-12">
                    <div class="portlet light portlet-fit bordered">
                        <div class="portlet-title">
                            <div class="caption">
                                <i class="icon-microphone font-dark hide"></i>
                                <%-- <span class="caption-subject bold font-dark uppercase"><i class="icon-question"></i> Helpdesk: Feedback/Issue</span>--%>
                                <h3>User Team Mapping</h3>
                                <%--                            <br />--%>
                            </div>
                        </div>
                        <div class="portlet-body">
                            <div class="portlet-body portlet" style="margin: 0; padding: 5px;">
                                <div id="frmHelpdeskDetails" style="display: inherit;">
                                    <div class="row">
                                        <div class="col-md-6">
                                            <label>User</label><br />
                                            <asp:DropDownList ID="ddlUser" CssClass="form-control" runat="server" DataTextField="UserName" DataValueField="Id">
                                                <asp:ListItem Text="Please select a user" Value=""></asp:ListItem>
                                                <asp:ListItem Text="User1" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="User2" Value="2"></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rqdUser" ControlToValidate="ddlUser" Text="Please select a user" ForeColor="Red" ErrorMessage="Please select a user" runat="server"></asp:RequiredFieldValidator>
                                        </div>

                                    </div>
                                    <div class="row">
                                        <div class="col-md-6">
                                            <label>Company</label><br />
                                            <asp:DropDownList ID="ddlCompany" CssClass="form-control" DataTextField="description" DataValueField="id" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged">
                                                <asp:ListItem Text="Shipping Application" Value="4"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                          <asp:RequiredFieldValidator ID="rqdCompany" ControlToValidate="ddlCompany" Text="Please select a company" ForeColor="Red" ErrorMessage="Please select a company" runat="server"></asp:RequiredFieldValidator>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-md-6">
                                            <label>Team</label><br />
                                            <asp:DropDownList ID="ddlTeam" CssClass="form-control" DataTextField="description" DataValueField="id" runat="server">
                                                <asp:ListItem Text="Shipping Application" Value="4"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                         <asp:RequiredFieldValidator ID="rqdTeam" ControlToValidate="ddlTeam" Text="Please select a team" ForeColor="Red" ErrorMessage="Please select a team" runat="server"></asp:RequiredFieldValidator>
                                    </div>
                                    <div class="row" style="text-align: left; margin-bottom: 20px;">
                                        <div class="col-md-6">
                                            <hr />
                                            <asp:Button ID="btnBack" class="btn btn-primary" Style="padding-left: 35px; padding-right: 35px;" Text="Back" runat="server" CausesValidation="False" OnClick="btnBack_Click" />&nbsp;&nbsp;
                                            <asp:Button ID="btnSaveUserTeamMapping" class="btn btn-primary" Style="padding-left: 35px; padding-right: 35px;" Text="Submit" runat="server" OnClick="btnSaveUserTeamMapping_Click" />
                                            <%-- <button id="btnSaveHelpdeskDetails" type="submit" class="btn btn-primary" style="padding-left: 35px; padding-right: 35px;">Submit</button>--%>
                                             <asp:Label ID="lblSuccess" Visible="true" Text="test" CssClass="label label-success" runat="server"></asp:Label>
                                        </div>
                                       
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
