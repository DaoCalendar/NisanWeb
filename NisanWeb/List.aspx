<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="List.aspx.cs" Inherits="List" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <p />
            <!-- @see http://iridescence.no/post/FixingBoundFieldSupportforCompositeObjects.aspx -->
            <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
                AutoGenerateColumns="False"
                EnableModelValidation="True" CellPadding="4" ForeColor="#333333" 
                GridLines="None">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <td>Status</td>
                            <td>Agent</td>
                            <td>Stock</td>
                            <td>Name</td>
                            <td>Created</td>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <td><%# Eval("Status") %></td>
                            <td><%# Eval("Agent.Code") %></td>
                            <td><%# Eval("Stock.Type") %></td>
                            <td><a href="Default.aspx?Name=<%# Eval("Stock.Name") %>"><%# Eval("Stock.Name") %></a></td>
                            <td><%# Eval("Parent.CreatedAt","{0:dd/MM/yyyy}") %></td>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <EditRowStyle BackColor="#999999" />
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            </asp:GridView>
        </ContentTemplate>
</asp:UpdatePanel>
</asp:Content>