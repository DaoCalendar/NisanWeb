<%@ Page Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div>
                <asp:Panel ID="Panel1" runat="server" GroupingText="Information">
                    <asp:Label runat="server" Text="Agent" CssClass="labeling" />
                    <asp:TextBox ID="txtAgent" runat="server" />
                    <asp:Label ID="lblAgent" runat="server" />
                    <br />
                    <asp:Label runat="server" Text="Stone" CssClass="labeling" />
                    <asp:DropDownList ID="ddlStock" runat="server" />
                    <br />
                    <asp:Label runat="server" Text="Name" CssClass="labeling" />
                    <asp:TextBox ID="txtName" runat="server" />
                    <br />
                    <asp:Label runat="server" Text="Death" CssClass="labeling" />
                    <asp:TextBox ID="txtDeath" runat="server" ToolTip="dd/MM/yyyy" Width="110px"></asp:TextBox>
                    <asp:ImageButton ID="btnDeath" runat="server" ImageUrl="~/Images/calendar.png" />
                    <asp:CalendarExtender ID="CalendarExtender2" runat="server" PopupButtonID="btnDeath" TargetControlID="txtDeath">
                    </asp:CalendarExtender>
                    <asp:MaskedEditExtender ID="MaskedEditExtender1" runat="server"
                        MaskType="Date"                   
                        Mask="99/99/9999" 
                        TargetControlID="txtDeath">
                    </asp:MaskedEditExtender>
                    <br />
                    <asp:Label runat="server" Text="Remarks" CssClass="labeling" />
                    <asp:TextBox ID="txtRemarks" runat="server" Rows="4" TextMode="MultiLine" />
                </asp:Panel>
                <asp:Panel ID="Panel2" runat="server" GroupingText="Contact">
                    <asp:Label runat="server" Text="Email" CssClass="labeling" />
                    <asp:TextBox ID="txtEmail" runat="server" />
                    <br />
                    <asp:Label runat="server" Text="Phone" CssClass="labeling" />
                    <asp:TextBox ID="txtPhone" runat="server" />
                </asp:Panel>
                <br />
                <div class="center">
                    <asp:Button runat="server" Text="Submit" />
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
