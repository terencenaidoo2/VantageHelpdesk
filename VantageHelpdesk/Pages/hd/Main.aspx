<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="VantageHelpdesk.Pages.hd.Main" %>

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
                                <h3>Helpdesk: Feedback/Issue</h3>
                                <%--                            <br />--%>
                                <span class="caption-helper">We are constantly striving to provide excellent service. We would love to get your feedback.</span>
                            </div>
                        </div>
                        <div class="portlet-body">
                            <div class="portlet-body portlet" style="margin: 0; padding: 5px;">
                                <div id="frmHelpdeskDetails" style="display: inherit;">
                                    <div class="row">
                                        <div class="col-md-6">
                                            <label>Do you want to log an issue or send feedback?</label><br />
                                            <asp:DropDownList ID="ddlFeedbackIssue" CssClass="form-control" runat="server">
                                                <asp:ListItem Text="Please select an option" Value=""></asp:ListItem>
                                                <asp:ListItem Text="Feedback: Tell us whats on your mind" Value="feedback"></asp:ListItem>
                                                <asp:ListItem Text="Issue: Log an issue" Value="issue"></asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="rqdFeedbackIssue" ControlToValidate="ddlFeedBackIssue" Text="Please select the type of issue" ForeColor="Red" ErrorMessage="Please select the type of issue" runat="server"></asp:RequiredFieldValidator>
                                        </div>

                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-md-6">
                                            <label>To which dashboard or application does your query relate?</label><br />
                                            <asp:DropDownList ID="ddlSite" CssClass="form-control" DataTextField="description" DataValueField="id" runat="server">
                                                <asp:ListItem Text="SalesQ" Value="63"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-md-6">
                                            <label>Your comments</label><br />
                                            <asp:TextBox ID="txtComments" CssClass="form-control" TextMode="MultiLine" Rows="5" Columns="40" placeholder="How can we assist you?" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rqdComments" ControlToValidate="txtComments" Text="Please enter your comments" ForeColor="Red" ErrorMessage="Please enter your comments" runat="server"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="row" style="text-align: left; margin-bottom: 20px;">
                                        <div class="col-md-6">
                                            <hr />
                                            <asp:Button ID="btnSaveHelpdeskDetails" class="btn btn-primary" Style="padding-left: 35px; padding-right: 35px;" Text="Submit" runat="server" OnClick="btnSaveHelpdeskDetails_Click" />
                                            <%-- <button id="btnSaveHelpdeskDetails" type="submit" class="btn btn-primary" style="padding-left: 35px; padding-right: 35px;">Submit</button>--%>
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
