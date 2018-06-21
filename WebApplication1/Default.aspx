<%@ Page Title="Add" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebApplication1._Default" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <script runat="server">

        void Page_Load(object sender, EventArgs e) {
            //Button2.Attributes.Add("onclick", "javascript:alert('ALERT ALERT!!!"+Input1.Value.ToString()+"');");
        }

        void GreetingBtn_Click(Object sender, EventArgs e)
        {
            string name = Input1.Value.ToString();
            string value = Text2.Value.ToString();
            string conn = getConnectionString();
            using (SqlConnection connection = new SqlConnection(conn))
            {
                SqlCommand command = new SqlCommand("insert into [dbo].[myObjectTable] values ('"+name+"','"+value+"')", connection);
                command.Connection.Open();
                command.ExecuteNonQuery();
                command.Connection.Close();
            }
        }

        void createTable()
        {
            string conn = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BackEndSQL;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            using (SqlConnection connection = new SqlConnection(conn))
            {
                SqlCommand command = new SqlCommand("create table myObjectTable(id int identity,name varchar(100),value varchar(100))", connection);
                command.Connection.Open();
                command.ExecuteNonQuery();
            }
        }


        
        public void sendMessage(Messages.Messages m)
        {
            
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
           OnClick="GreetingBtn_Click" 
           runat="server"/>

</div>
</asp:Content>
