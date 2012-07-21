<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="About.aspx.cs" Inherits="About" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        <h1>HL GRANITE MANUFACTURING</h1>
        <table width="100%">
        <tr>
            <td>
                Mailing Address:
                <h2>Head Quater:</h2>
                <p />
                600 & 601, Taman saga 2,<br />
                Jalan Alor Mengkudu,<br />
                05400 Alor Star, Kedah. Malaysia
            </td>
            <td>show map on google</td>
        </tr>
        <tr>
            <td>
                <h2>Factory:</h2>
                <p />
                Lot 184, Kampung Padang Kunyit,<br />
                MK Telaga Mas,<br />
                06400 Alor Star, Kedah. Malaysia.<br />
                <p />
                Hotline: +6012-4200963 (Wooysthen)<br />
                Tel. 	: 	+604.7319486<br />
                Fax. 	: 	+604.7319486<br />
                Email: <a href="mailto:hlgranite@gmail.com">hlgranite@gmail.com</a><br />
                Website 	: 	<a href="http://www.hlgranite.com">http://www.hlgranite.com</a>
            </td>
            <td></td>
        </tr>
        </table>
        </ContentTemplate>
</asp:UpdatePanel>
</asp:Content>

