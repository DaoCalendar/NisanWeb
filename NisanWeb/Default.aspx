<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Nisan Order</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label runat="server" Text="Agent" />
        <asp:TextBox ID="txtAgent" runat="server" /><asp:Label ID="lblAgent" runat="server" />
        <br />
        <asp:Label runat="server" Text="Stone" />
        <asp:DropDownList ID="ddlStock" runat="server" />
        <br />
        <asp:Label runat="server" Text="Name" />
        <asp:TextBox ID="txtName" runat="server" />
        <br />
        <asp:Label runat="server" Text="Death" />
        <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
        <br />
        <asp:Label runat="server" Text="Remarks" />
        <asp:TextBox ID="txtRemarks" runat="server" Rows="6" TextMode="MultiLine" />
        <br />
        Contact:
        <br />
        <asp:Label runat="server" Text="Email" />
        <asp:TextBox ID="txtEmail" runat="server" />
        <br />
        <asp:Label runat="server" Text="Phone" />
        <asp:TextBox ID="txtPhone" runat="server" />
        <br />
    </div>
    </form>
</body>
</html>
