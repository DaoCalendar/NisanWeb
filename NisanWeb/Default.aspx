<%@ Page Language="C#" MasterPageFile="MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <p />
            <asp:Image ID="imgStatus" runat="server" ImageUrl="Images/order.png" />
            <fieldset id="Panel1" class="shadowbox" runat="server" style="width:400px;">
                <legend>Information</legend>
                <asp:Label runat="server" Text="Stone" CssClass="labeling" />
                <asp:DropDownList ID="ddlStock" runat="server" DataSourceID="ObjectDataSource1" 
                    DataTextField="Type" DataValueField="Id" />
                <asp:Label runat="server" Text="*" ToolTip="Compulsory" CssClass="warn" />
                <br />
                <asp:Label runat="server" Text="Name" CssClass="labeling" />
                <asp:TextBox ID="txtName" runat="server" Width="200px" />
                <asp:Label runat="server" Text="*" ToolTip="Compulsory" CssClass="warn" />
                <br />
                <asp:Label runat="server" Text="Jawi" CssClass="labeling" />
                <asp:TextBox ID="txtJawi" runat="server" Width="200px" />
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
                <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" Width="200px" />
                <asp:Label ID="Label6" runat="server" Text="Agent" CssClass="labeling" />
                <asp:TextBox ID="txtAgent" runat="server" Width="40px" />
                <asp:Label ID="lblAgent" runat="server" />
                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
                    SelectMethod="LoadAll" TypeName="HLGranite.Nisan.Stock">
                </asp:ObjectDataSource>
            </fieldset>
            <fieldset id="Panel2" class="shadowbox" runat="server" style="width:400px;">
                <legend><img src="Images/lorry.png" />&nbsp;Contact</legend>
                <asp:Label runat="server" Text="Name" CssClass="labeling" />
                <asp:TextBox ID="txtCustomer" runat="server" />
                <br />
                <asp:Label ID="Label1" runat="server" Text="Email" CssClass="labeling" />
                <asp:TextBox ID="txtEmail" runat="server" />
                <br />
                <asp:Label ID="Label2" runat="server" Text="Phone" CssClass="labeling" />
                <asp:TextBox ID="txtPhone" runat="server" />
                <br />
                <asp:Label ID="Label3" runat="server" Text="Deliver To" CssClass="labeling" />
                <asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine" Width="200px" />
                <asp:Label ID="Label4" runat="server" Text="Postal" CssClass="labeling" />
                <asp:TextBox ID="txtPostal" runat="server" />
                <br />
                <asp:Label ID="Label5" runat="server" Text="State" CssClass="labeling" />
                <asp:DropDownList ID="ddlState" runat="server" />
            </fieldset>
            <p />
            <div class="center">
                <asp:Button ID="btnSubmit" runat="server" Text="Submit" onclick="Submit_Click" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
