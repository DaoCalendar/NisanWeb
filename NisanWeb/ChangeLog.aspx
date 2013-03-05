<%@ Page Title="Change Logs" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ChangeLog.aspx.cs" Inherits="ChangeLog" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <h1>Change Logs</h1>
            <h3>22 Aug 2012 ver 0.9.4</h3>
            <ul>
                <li>Fixed display customer info at order page.</li>
                <li>Notify customer and agent when order submit or status has been changed.</li>
            </ul>
            <h3>20 Aug 2012 ver 0.9.3</h3>
            <ul>
                <li>added pay button.</li>
                <li>fixed IE compatible view.</li>
                <li>auto get jawi after key in rumi name.</li>
            </ul>
            <h3>5 Aug 2012 ver 0.9.2</h3>
            <ul>
                <li>handle authorization for customer, agent, and admin.</li>
                <li>able to search by order's name.</li>
                <li>agent able to view his/her own list.</li>
                <li>notify admin once order been created.</li>
                <li>touch up interface looks nice.</li>
            </ul>
            <h3>22 Jul 2012 ver 0.9.1</h3>
            <ul>
                <li>draft database and website layout.</li>
                <li>complete user registration.</li>
                <li>agent able to create an order.</li></ul>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
