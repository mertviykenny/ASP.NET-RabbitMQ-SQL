<%@ Page Title="Add" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebApplication1._Default" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <script runat="server">

        void Page_Load(object sender, EventArgs e) {
            //Button2.Attributes.Add("onclick", "javascript:alert('ALERT ALERT!!!"+Input1.Value.ToString()+"');");
        }

</script>
<div>
    Site about some abstract entity.<br />
    Name:
          <input id="Input1"
              type="text"
              runat="server" value="myName" /><br />
        Value: 
          <input id="Text2"
              type="text"
              runat="server" value="myValue" /><br />
              &nbsp;&nbsp;
          <asp:Button id="Button2"
           Text="Add"
           OnClick="AddBtn_Click" 
           runat="server"/>

</div>
</asp:Content>
