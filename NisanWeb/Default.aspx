<%@ Page Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div>
                <asp:Panel ID="Panel1" runat="server" GroupingText="Information">
                    <asp:Label runat="server" Text="Agent" CssClass="labeling" />
                    <asp:TextBox ID="txtAgent" runat="server" Width="40px" />
                    <asp:Label ID="lblAgent" runat="server" />
                    <br />
                    <asp:Label runat="server" Text="Stone" CssClass="labeling" />
                    <asp:DropDownList ID="ddlStock" runat="server" DataSourceID="ObjectDataSource1" 
                        DataTextField="Type" DataValueField="Id" />
                    <br />
                    <asp:Label runat="server" Text="Name" CssClass="labeling" />
                    <asp:TextBox ID="txtName" runat="server" Width="200px" />
                    <br />
                    <asp:Label runat="server" Text="Death" CssClass="labeling" />
                    <asp:TextBox ID="txtDeath" runat="server" ToolTip="dd/MM/yyyy" Width="110px" 
                        AutoPostBack="true" ontextchanged="txtDeath_TextChanged" />
                    <asp:ImageButton ID="btnDeath" runat="server" ImageUrl="~/Images/calendar.png" />
                    <asp:CalendarExtender ID="CalendarExtender2" runat="server" PopupButtonID="btnDeath" TargetControlID="txtDeath"
                        Format="dd/MM/yyyy">
                    </asp:CalendarExtender>
                    <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server"
                        MaskType="Date"                   
                        Mask="99/99/9999" 
                        TargetControlID="txtDeath">
                    </asp:MaskedEditExtender>
                    <asp:Label ID="lblDeathm" runat="server"></asp:Label>
                    <br />
                    <asp:Label runat="server" Text="Remarks" CssClass="labeling" />
                    <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" />
                    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
                        SelectMethod="LoadAll" TypeName="HLGranite.Nisan.Stock">
                    </asp:ObjectDataSource>
                </asp:Panel>
                <asp:Panel ID="Panel2" runat="server" GroupingText="Contact">
                    <asp:Label runat="server" Text="Email" CssClass="labeling" />
                    <asp:TextBox ID="txtEmail" runat="server" />
                    <br />
                    <asp:Label runat="server" Text="Phone" CssClass="labeling" />
                    <asp:TextBox ID="txtPhone" runat="server" />
                    <br />
                    <asp:Label runat="server" Text="Deliver To" CssClass="labeling" />
                    <asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine" />
                    <asp:Label runat="server" Text="Postal" CssClass="labeling" />
                    <asp:TextBox ID="txtPostal" runat="server" />
                    <br />
                    <asp:Label runat="server" Text="State" CssClass="labeling" />
                    <asp:DropDownList ID="ddlState" runat="server" />
                </asp:Panel>
                <br />
                <div class="center">
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" onclick="Submit_Click" />
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
