<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WebApplication1.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    Username:<input id="UserNameTextBox" type="text" runat="server" value="test"  /><br />
    Password: <input id="PasswordTextBox" type="password" runat="server" value="test"  /><br />
    &nbsp;<asp:Button ID="RegisterButton" runat="server" OnClick="RegisterButton_Click" Text="Register" />
    <asp:Button ID="LoginButton" runat="server" OnClick="LoginButton_Click" Text="Login" />
&nbsp;   
</asp:Content>
