<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="List.aspx.cs" Inherits="List" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel runat="server">
        <ContentTemplate>
            <p />
            <!-- @see http://iridescence.no/post/FixingBoundFieldSupportforCompositeObjects.aspx -->
            <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
                AutoGenerateColumns="False" CellPadding="4"
                EnableModelValidation="True" ForeColor="#333333" GridLines="None" 
                AutoGenerateSelectButton="True">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            Id | Created At | Sold To | Stock | Name | Amount | Amount
                        </HeaderTemplate>
                        <ItemTemplate>
                            <%# Eval("Id") %>
                            <%# Eval("Parent.CreatedAt","{0:dd/MM/yyyy}") %>
                            <%# Eval("Agent.Code") %>
                            <%# Eval("Stock.Type") %>
                            <%# Eval("Stock.Name") %>
                            <%# Eval("Amount") %>
                            <%# Eval("Remarks") %>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:TemplateField>
                </Columns>
                <EditRowStyle BackColor="#2461BF" />
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#EFF3FB" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            </asp:GridView>
            <%--<asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetSales" TypeName="HLGranite.Nisan.Agent" />--%>
        </ContentTemplate>
</asp:UpdatePanel>
</asp:Content>


  <%--<Columns>
                    <asp:BoundField DataField="Id" HeaderText="Id" SortExpression="Id" />
                    <asp:BoundField DataField="Quantity" HeaderText="Qty" SortExpression="Quantity" />
                    <asp:BoundField DataField='<%# Eval("Stock.Name") %>' HeaderText="Name" SortExpression="Stock" />
                    <asp:BoundField DataField="Amount" HeaderText="Amount" SortExpression="Amount" />
                    <asp:BoundField DataField="Remarks" HeaderText="Remarks" SortExpression="Remarks" />
                </Columns>--%>