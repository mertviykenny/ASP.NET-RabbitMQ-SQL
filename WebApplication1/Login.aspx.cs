using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void LoginButton_Click(object sender, EventArgs e)
        {

        }
        public void Register(Messages.User u)
        {
            var factory = _Default.getFactory();
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare("Register", false, false, false, null);
                channel.BasicPublish("", "Register", true, null, Messages.User.SerializeToByte(u));
            }
        }
        protected void RegisterButton_Click(object sender, EventArgs e)
        {
            var userName=UserNameTextBox.Value.ToString();
            var password = PasswordTextBox.Value.ToString();
            Register(new Messages.User { username = userName, password = password });

        }
    }
}