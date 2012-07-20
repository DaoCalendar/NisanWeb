<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Register.aspx.cs" Inherits="Register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Panel runat="server" GroupingText="Register">
        <asp:Label runat="server" Text="Code" CssClass="labeling" />
        <asp:TextBox ID="txtCode" runat="server" />
        <br />
        <asp:Label runat="server" Text="Name" CssClass="labeling" />
        <asp:TextBox ID="txtName" runat="server" />
        <br />
        <asp:Label runat="server" Text="Password" CssClass="labeling" />
        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" />
        <br />
        <asp:Label runat="server" Text="Confirm Password" CssClass="labeling" />
        <asp:TextBox ID="txtPassword2" runat="server" TextMode="Password" />
        <br />
        <asp:Label runat="server" Text="Phone" CssClass="labeling" />
        <asp:TextBox ID="txtPhone" runat="server" />
        <br />
        <asp:Label runat="server" Text="Address" CssClass="labeling" />
        <asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine" />
        <asp:Label runat="server" Text="Postal" CssClass="labeling" />
        <asp:TextBox ID="txtPostal" runat="server" />
        <br />
        <asp:Label runat="server" Text="State" CssClass="labeling" />
        <asp:DropDownList ID="ddlState" runat="server" />
        <br />
        <asp:Label ID="Label1" runat="server" Text="Remarks" CssClass="labeling" />
        <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" />
    </asp:Panel>
    <br />
    <div class="center">
        <asp:Button ID="Button1" runat="server" Text="Submit" />
    </div>
</asp:Content>
