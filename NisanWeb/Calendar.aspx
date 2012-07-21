<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Calendar.aspx.cs" Inherits="Calendar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        <div>
            <asp:Calendar ID="Calendar1" runat="server" OnSelectionChanged="Calendar1_SelectionChanged" />
            <asp:TextBox ID="TextBox1" runat="server" />
            <asp:Button ID="Button1" runat="server" Text="&gt;&gt;" OnClick="Button1_Click" />
            <asp:Label ID="Label1" runat="server" />
        </div>
        <div class="legend">
            <h2>Muslim Month</h2>
              <span class="float-left">
                <ol>
                    <li>Muharram</li>
                    <li>Safar</li>
                    <li>Rabiulawal</li>
                    <li>Rabiulakhir</li>
                    <li>Jamadilawal</li>
                    <li>Jamadilakhir</li>
                    <li>Rejab</li>
                    <li>Syaaban</li>
                    <li>Ramadhan</li>
                    <li>Syawal</li>
                    <li>Zulkaedah</li>
                    <li>Zulhijjah</li>
                </ol>
            </span>
            <span class="float-left">
                <ol>
                    <li>محرّم</li>
                    <li>صفر</li>
                    <li>ربيع الاول</li>
                    <li>ربيع الاخير</li>
                    <li>جمادالاول</li>
                    <li>جمادالاخير</li>
                    <li>رجب</li>
                    <li>شعبان</li>
                    <li>رمضان</li>
                    <li>شوال</li>
                    <li>ذوالقعده</li>
                    <li>ذوالحجه</li>
                </ol>
            </span>
        </div>
        </ContentTemplate>
</asp:UpdatePanel>
</asp:Content>

