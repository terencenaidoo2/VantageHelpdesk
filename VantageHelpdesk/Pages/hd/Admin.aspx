<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="VantageHelpdesk.Pages.hd.Admin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <asp:Button ID="btnAddNewMapping" Text="Add New Mapping" CssClass="btn btn-primary" runat="server" OnClick="btnAddNewMapping_Click" />
    </div>
    <div>
        <asp:GridView ID="gvUserMapping" AutoGenerateColumns="false" CssClass="table table-bordered" runat="server" OnRowCancelingEdit="gvUserMapping_RowCancelingEdit" OnRowEditing="gvUserMapping_RowEditing">
            <AlternatingRowStyle BackColor="#cccccc" />
            <EditRowStyle BackColor="LightSkyBlue" />
            <Columns>
                <asp:TemplateField HeaderText="Username">
                    <ItemTemplate>
                        <asp:Label ID="lblUsername" Text='<%# Bind("UserName") %>' runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Company">
                    <ItemTemplate>
                        <asp:Label ID="lblCompany" Text='<%# Bind("CompanyName") %>' runat="server"></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddlCompany" CssClass="form-control input-xs" runat="server">
                            <asp:ListItem>Alere</asp:ListItem>
                            <asp:ListItem>Monteagle International</asp:ListItem>
                            <asp:ListItem>Vantage</asp:ListItem>
                            <asp:ListItem>SalesQ</asp:ListItem>
                        </asp:DropDownList>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Team">
                    <ItemTemplate>
                        <asp:Label ID="lblTeam" Text='<%# Bind("TeamName") %>' runat="server"></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList ID="ddlTeam" CssClass="form-control input-xs" runat="server">
                            <asp:ListItem>Monteagle Shipping</asp:ListItem>
                            <asp:ListItem>Vantage Support</asp:ListItem>
                            <asp:ListItem>SalesQ Team1</asp:ListItem>

                        </asp:DropDownList>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:CommandField ShowEditButton="True"></asp:CommandField>
            </Columns>
        </asp:GridView>
    </div>
    <%--    <div class="row">
        &nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnBack" CssClass="btn btn-primary" Text="Back" runat="server" CausesValidation="False" OnClick="btnBack_Click" />
    </div>--%>
</asp:Content>
