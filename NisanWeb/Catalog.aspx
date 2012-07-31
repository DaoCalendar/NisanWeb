<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Catalog.aspx.cs" Inherits="Catalog" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <asp:Repeater ID="Repeater1" runat="server" DataSourceID="ObjectDataSource1">
            <HeaderTemplate>
                <div style="clear:both"></div>
            </HeaderTemplate>
            <ItemTemplate>
                <div class="imagebox">                
                    <asp:Image runat="server" Height="300px" ImageUrl='<%# "~/Images/" + DataBinder.Eval(Container.DataItem,"Uri") %>' />
                    <br />
                    <asp:Label runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Type") %>' />
                    <br />
                    Price: RM <%# DataBinder.Eval(Container.DataItem,"Price") %>
                    <asp:Button runat="server" Text="Order" OnClick="Order_Click" TabIndex='<%# (int)DataBinder.Eval(Container.DataItem, "Id") %>' />
                </div>
            </ItemTemplate>
            <FooterTemplate>
                <div style="clear:both"></div>
            </FooterTemplate>
        </asp:Repeater>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="LoadAll" TypeName="HLGranite.Nisan.Stock">
        </asp:ObjectDataSource>
    </ContentTemplate>
</asp:UpdatePanel>
</asp:Content>

