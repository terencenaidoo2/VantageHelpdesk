<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FeedbackList.aspx.cs" Inherits="VantageHelpdesk.Pages.hd.FeedbackList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script type='text/javascript'>
        function ShowPopup() {
            $('#myModal').modal('show');
        }
        function ShowPopupHistory() {
            $('#myModalHistory').modal('show');
        }
    </script>
    <h3>Feedback List</h3>
    <div class="panel-group">
        <div class="panel panel-default">
            <div class="panel-heading" style="background-color: lightyellow">
                <h4 class="panel-title">
                    <a data-toggle="collapse" href="#collapse1">Search Filters</a>
                </h4>
            </div>
            <div id="collapse1" class="panel-collapse collapse">
                <div class="panel-body">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <table style="width: 100%">
                                <tr>
                                    <td style="width: 50%; text-align: left; vertical-align: top;">
                                        <table style="width: 100%">
                                            <tr>
                                                <th style="width: 30%; vertical-align: top; text-align: left">Feedback #</th>
                                                <td style="width: 70%; vertical-align: top; text-align: left">
                                                    <asp:TextBox ID="txtSearchFeedbackID" runat="server" Width="100%" CssClass="form-control" TextMode="Number"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td style="width: 50%; text-align: left; vertical-align: top;">
                                        <table style="width: 100%">
                                            <tr>
                                                <th style="width: 30%; vertical-align: top; text-align: left">Comment</th>
                                                <td style="width: 70%; vertical-align: top; text-align: left">
                                                    <asp:TextBox ID="txtSearchComments" runat="server" Width="100%" CssClass="form-control"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 50%; text-align: left; vertical-align: top;">
                                        <table style="width: 100%">
                                            <tr>
                                                <th style="width: 30%; vertical-align: top; text-align: left">Site</th>
                                                <td style="width: 70%; vertical-align: top; text-align: left">
                                                    <asp:DropDownList ID="ddlSearchSite" DataSourceID="edsSite" CssClass="form-control" DataTextField="description" DataValueField="id" runat="server"></asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <th style="width: 30%; vertical-align: top; text-align: left">Status</th>
                                                <td style="width: 70%; vertical-align: top; text-align: left">
                                                    <asp:DropDownList ID="ddlSearchStatus" runat="server" CssClass="form-control" DataSourceID="edsStatus" DataTextField="description" DataValueField="id"></asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td style="width: 50%; text-align: left; vertical-align: top;">
                                        <table style="width: 100%">
                                            <tr>
                                                <th style="width: 30%; vertical-align: top; text-align: left">Logged By</th>
                                                <td style="width: 70%; vertical-align: top; text-align: left">
                                                    <asp:DropDownList ID="ddlSearchLoggedBy" DataSourceID="edsUsers" CssClass="form-control" DataTextField="UserName" DataValueField="id" runat="server"></asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <th style="width: 30%; vertical-align: top; text-align: left">Current Owner</th>
                                                <td style="width: 70%; vertical-align: top; text-align: left">
                                                    <asp:DropDownList ID="ddlSearchCurrentOwner" runat="server" CssClass="form-control" DataSourceID="edsUsersVantage" DataTextField="UserName" DataValueField="id"></asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Button ID="butReset" runat="server" Text="Reset" CssClass="btn btn-default" OnClick="butReset_Click" CausesValidation="false" />
                                        <asp:Button ID="butSearch" runat="server" Text="Search" CssClass="btn btn-primary" OnClick="butSearch_Click" CausesValidation="false" />
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
    <div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:HiddenField ID="hidFeedbackID" runat="server" />
                <asp:GridView ID="gvFeedback" Width="100%" runat="server" DataSourceID="edsFeedback" AutoGenerateColumns="false" AllowSorting="true"
                    AllowPaging="true" DataKeyNames="feedback_id,comment_count" OnRowDataBound="gvFeedback_RowDataBound"
                    OnRowCommand="gvFeedback_RowCommand" CssClass="table table-bordered table-striped table-hover" OnPageIndexChanging="gvFeedback_PageIndexChanging">
                    <Columns>
                        <asp:BoundField DataField="feedback_id" HeaderText="Feedback #" SortExpression="feedback_id">
                            <HeaderStyle Width="5%" BackColor="#f5f5f5" />
                        </asp:BoundField>
                        <asp:BoundField DataField="feedback" HeaderText="Comment" SortExpression="feedback">
                            <HeaderStyle Width="25%" BackColor="#f5f5f5" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Status" SortExpression="status_desc">
                            <HeaderStyle Width="10%" BackColor="#f5f5f5" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <ItemTemplate>
                                <asp:Label ID="lblStatusDesc" runat="server" Text='<%# Eval("status_desc")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%--                        <asp:BoundField DataField="status_desc" HeaderText="Status" SortExpression="status_desc">
                            <HeaderStyle Width="10%" BackColor="#f5f5f5" />
                        </asp:BoundField>--%>
                        <asp:BoundField DataField="site_desc" HeaderText="Site" SortExpression="site_desc">
                            <HeaderStyle Width="10%" BackColor="#f5f5f5" />
                        </asp:BoundField>
                        <asp:BoundField DataField="user_name" HeaderText="Logged By" SortExpression="user_name">
                            <HeaderStyle Width="10%" BackColor="#f5f5f5" />
                        </asp:BoundField>
                        <asp:BoundField DataField="current_owner" HeaderText="Current Owner" SortExpression="current_owner">
                            <HeaderStyle Width="10%" BackColor="#f5f5f5" />
                        </asp:BoundField>
                        <asp:BoundField DataField="account_manager" SortExpression="account_manager" HeaderText="Account Manager">
                            <HeaderStyle Width="10%" BackColor="#f5f5f5" />
                        </asp:BoundField>
                        <asp:BoundField DataField="timestamp" HeaderText="Date/Time" SortExpression="timestamp">
                            <HeaderStyle Width="10%" BackColor="#f5f5f5" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Comment Count" ItemStyle-Width="10%" HeaderStyle-Width="10%" HeaderStyle-HorizontalAlign="Center">
                            <HeaderStyle HorizontalAlign="Center" BackColor="#f5f5f5" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            <ItemTemplate>
                                <asp:Label ID="lblCommentCount" runat="server" Text=""></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Action" ItemStyle-Width="10%" HeaderStyle-Width="10%" HeaderStyle-HorizontalAlign="Center">
                            <HeaderStyle HorizontalAlign="Center" BackColor="#f5f5f5" />
                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            <ItemTemplate>
                                <asp:ImageButton ID="imgbutSave" ImageUrl="~/Content/Images/glyphs/glyphicons-151-edit.png" Height="30" CssClass="btn btn-default" runat="server" ToolTip="Update" CommandName="updateRecord" CommandArgument='<%# Container.DataItemIndex %>' CausesValidation="false" />
                                <asp:ImageButton ID="imgbutHistory" ImageUrl="~/Content/Images/glyphs/glyphicons-58-history.png" Height="30" CssClass="btn btn-default" runat="server" ToolTip="View History" CommandName="viewHistory" CommandArgument='<%# Container.DataItemIndex %>' CausesValidation="false" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <div id="myModal" class="modal fade">
        <div class="modal-dialog">

            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                        &times;</button>
                    <h4 class="modal-title">Update Feedback</h4>
                </div>
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <div class="modal-body">
                            <table style="width: 100%">
                                <tr>
                                    <th style="width: 30%; vertical-align: top; text-align: left">Feedback #</th>
                                    <td style="width: 70%; vertical-align: top; text-align: left">
                                        <asp:Label ID="lblFeedbackID" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <th style="width: 30%; vertical-align: top; text-align: left"><span class="text-danger">*</span> Description</th>
                                    <td style="width: 70%; vertical-align: top; text-align: left">
                                        <asp:TextBox ID="txtDesc" class="form-control" Rows="3" Columns="50" TextMode="MultiLine" Width="100%" runat="server" MaxLength="1000"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvDesc" runat="server" ControlToValidate="txtDesc" SetFocusOnError="true" CssClass="text-danger" Display="Dynamic" ErrorMessage="Description is required"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <th style="width: 30%; vertical-align: top; text-align: left"><span class="text-danger">*</span> Site</th>
                                    <td style="width: 70%; vertical-align: top; text-align: left">
                                        <asp:DropDownList ID="ddlSite" class="form-control" runat="server" DataSourceID="edsSite" DataTextField="description" DataValueField="id"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvSite" runat="server" ControlToValidate="ddlSite" SetFocusOnError="true" CssClass="text-danger" Display="Dynamic" ErrorMessage="Site is required"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <th style="width: 30%; vertical-align: top; text-align: left"><span class="text-danger">*</span> Status</th>
                                    <td style="width: 70%; vertical-align: top; text-align: left">
                                        <asp:DropDownList ID="ddlStatus" class="form-control" runat="server" DataSourceID="edsStatus" DataTextField="description" DataValueField="id"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvStatus" runat="server" ControlToValidate="ddlStatus" SetFocusOnError="true" CssClass="text-danger" Display="Dynamic" ErrorMessage="Status is required"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <th style="width: 30%; vertical-align: top; text-align: left"><span class="text-danger">*</span> Priority</th>
                                    <td style="width: 70%; vertical-align: top; text-align: left">
                                        <asp:DropDownList ID="ddlPriority" class="form-control" runat="server">
                                            <asp:ListItem>High</asp:ListItem>
                                            <asp:ListItem>Normal</asp:ListItem>
                                            <asp:ListItem>Low</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvPriority" runat="server" ControlToValidate="ddlPriority" SetFocusOnError="true" CssClass="text-danger" Display="Dynamic" ErrorMessage="Priority is required"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <th style="width: 30%; vertical-align: top; text-align: left"><span class="text-danger">*</span> Account Manager</th>
                                    <td style="width: 70%; vertical-align: top; text-align: left">
                                        <asp:DropDownList ID="ddlAccountManager" class="form-control" runat="server" DataSourceID="edsUsersVantage" DataTextField="UserName" DataValueField="id">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvAccountManager" runat="server" ControlToValidate="ddlAccountManager" SetFocusOnError="true" CssClass="text-danger" Display="Dynamic" ErrorMessage="Account Manager is required"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <th style="width: 30%; vertical-align: top; text-align: left"><span class="text-danger">*</span> Current Owner</th>
                                    <td style="width: 70%; vertical-align: top; text-align: left">
                                        <asp:DropDownList ID="ddlCurrentOwner" class="form-control" runat="server" DataSourceID="edsUsersVantage" DataTextField="UserName" DataValueField="id">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="rfvCurrentOwner" runat="server" ControlToValidate="ddlCurrentOwner" SetFocusOnError="true" CssClass="text-danger" Display="Dynamic" ErrorMessage="Current Owner is required"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <th style="width: 30%; vertical-align: top; text-align: left"><span class="text-danger">*</span> Comments</th>
                                    <td style="width: 70%; vertical-align: top; text-align: left">
                                        <asp:TextBox ID="txtComments" class="form-control" Rows="3" Columns="50" TextMode="MultiLine" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvComments" runat="server" ControlToValidate="txtComments" SetFocusOnError="true" CssClass="text-danger" Display="Dynamic" ErrorMessage="Comments is required"></asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <div class="modal-footer">
                    <div style="text-align: left"><span class="text-danger">*</span> (Indicates Required Field)</div>
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    <asp:Button ID="butUpdate" runat="server" Text="Update" CssClass="btn btn-primary" OnClick="butUpdate_Click" />
                </div>
            </div>

        </div>
    </div>

    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
        <ContentTemplate>
            <div id="myModalHistory" class="modal fade">
                <div class="modal-dialog" style="width: 1250px;">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                &times;</button>
                            <h4 class="modal-title">Feedback History</h4>
                        </div>
                        <div class="modal-body">

                            <table style="width: 100%">
                                <tr>
                                    <th style="width: 30%; vertical-align: top; text-align: left">Feedback #</th>
                                    <td style="width: 70%; vertical-align: top; text-align: left">
                                        <asp:Label ID="lblHist_feedback_id" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <th style="width: 30%; vertical-align: top; text-align: left">Description</th>
                                    <td style="width: 70%; vertical-align: top; text-align: left">
                                        <asp:Label ID="lblHist_Desc" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <th style="width: 30%; vertical-align: top; text-align: left">Site</th>
                                    <td style="width: 70%; vertical-align: top; text-align: left">
                                        <asp:Label ID="lblHist_Site" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <th style="width: 30%; vertical-align: top; text-align: left">Status</th>
                                    <td style="width: 70%; vertical-align: top; text-align: left">
                                        <asp:Label ID="lblHist_Status" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <th style="width: 30%; vertical-align: top; text-align: left">Account Manager</th>
                                    <td style="width: 70%; vertical-align: top; text-align: left">
                                        <asp:Label ID="lblHist_AccountManager" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <th style="width: 30%; vertical-align: top; text-align: left">Current Owner</th>
                                    <td style="width: 70%; vertical-align: top; text-align: left">
                                        <asp:Label ID="lblHist_CurrentOwner" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2"></td>
                                </tr>
                            </table>

                            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="gvFeedbackHistory" Width="100%" runat="server" DataSourceID="edsFeedbackHistory" AutoGenerateColumns="false"
                                        AllowPaging="false" DataKeyNames="id" CssClass="table table-bordered table-striped table-hover">
                                        <Columns>
                                            <asp:BoundField DataField="Comments" HeaderText="Comments">
                                                <HeaderStyle Width="30%" BackColor="#f5f5f5" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="AccountManager" HeaderText="Account Manager">
                                                <HeaderStyle Width="15%" BackColor="#f5f5f5" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="CurrentOwner" HeaderText="Current Owner">
                                                <HeaderStyle Width="15%" BackColor="#f5f5f5" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Status_Desc" HeaderText="Status">
                                                <HeaderStyle Width="10%" BackColor="#f5f5f5" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="timestamp" HeaderText="Date/Time">
                                                <HeaderStyle Width="10%" BackColor="#f5f5f5" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="LastModifiedBy" HeaderText="Last Modified By">
                                                <HeaderStyle Width="10%" BackColor="#f5f5f5" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="TimeSinceSubmitted" HeaderText="Time Since Submitted">
                                                <HeaderStyle Width="10%" BackColor="#f5f5f5" />
                                            </asp:BoundField>
                                        </Columns>
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-default" data-dismiss="modal">
                                Close</button>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <ef:EntityDataSource ID="edsFeedback" runat="server" ContextTypeName="VantageHelpdesk.Data.HelpdeskModel" EntitySetName="vw_t_feedback_list" OrderBy="it.timestamp desc" Where="(it.[feedback] like '%' + @searchdesc + '%' or @searchdesc is null) and (it.[user_id] like '%' + @searchloggedby + '%' or @searchloggedby is null) and (it.[site_id] = @searchsite or @searchsite is null) and (it.[status_id] = @searchstatus or @searchstatus is null) and (it.[assigned_to_current_owner] like '%' + @searchcurrentowner + '%' or @searchcurrentowner is null) and (it.[feedback_id] = @searchfeedbackid or @searchfeedbackid is null) and (it.[team_id] = @team_id or @team_id is null)">
        <WhereParameters>
            <asp:ControlParameter ControlID="txtSearchComments" Name="searchdesc" Type="String" DefaultValue="" ConvertEmptyStringToNull="true" />
            <asp:ControlParameter ControlID="ddlSearchLoggedBy" Name="searchloggedby" Type="String" DefaultValue="" ConvertEmptyStringToNull="true" />
            <asp:ControlParameter ControlID="ddlSearchSite" Name="searchsite" Type="Int32" DefaultValue="" ConvertEmptyStringToNull="true" />
            <asp:ControlParameter ControlID="ddlSearchStatus" Name="searchstatus" Type="Int32" DefaultValue="" ConvertEmptyStringToNull="true" />
            <asp:ControlParameter ControlID="ddlSearchCurrentOwner" Name="searchcurrentowner" Type="String" DefaultValue="" ConvertEmptyStringToNull="true" />
            <asp:ControlParameter ControlID="txtSearchFeedbackID" Name="searchfeedbackid" Type="Int32" DefaultValue="" ConvertEmptyStringToNull="true" />
            <asp:SessionParameter Name="team_id" Type="Int32" SessionField="team_id" />
        </WhereParameters>
    </ef:EntityDataSource>
    <ef:EntityDataSource ID="edsStatus" runat="server" ContextTypeName="VantageHelpdesk.Data.HelpdeskModel" EntitySetName="t_status" OrderBy="it.sort_order asc"></ef:EntityDataSource>
    <ef:EntityDataSource ID="edsSite" runat="server" ContextTypeName="VantageHelpdesk.Data.HelpdeskModel" EntitySetName="t_site" OrderBy="it.sort_order asc"></ef:EntityDataSource>
    <ef:EntityDataSource ID="edsUsers" runat="server" ContextTypeName="VantageHelpdesk.Data.VantageAppUsersModel" EntitySetName="AspNetUsers" OrderBy="it.UserName asc"></ef:EntityDataSource>
    <ef:EntityDataSource ID="edsUsersVantage" runat="server" ContextTypeName="VantageHelpdesk.Data.VantageAppUsersModel" EntitySetName="AspNetUsers" Where="(it.[Email] Like '%' + 'vantagedata' + '%') " OrderBy="it.UserName asc"></ef:EntityDataSource>
    <ef:EntityDataSource ID="edsFeedbackHistory" runat="server" ContextTypeName="VantageHelpdesk.Data.HelpdeskModel"
        EntitySetName="vw_t_feedback_history_list" OrderBy="it.timestamp desc" Where="it.[feedback_id]=@feedbackid">
        <WhereParameters>
            <asp:ControlParameter ControlID="hidFeedbackID" Name="feedbackid" Type="Int32" />
        </WhereParameters>
    </ef:EntityDataSource>
</asp:Content>
