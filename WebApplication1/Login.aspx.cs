using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RabbitMQ.Client;
using System.Text;
using RabbitMQ.Client.Events;
namespace WebApplication1
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }




        public void UserLogin(Messages.User u)
        {
            var factory = _Default.getFactory();
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare("Login", false, false, false, null);
                var id = Guid.NewGuid().ToString();
                var properties = channel.CreateBasicProperties();
                properties.CorrelationId = id;
                properties.ReplyTo=id;
                channel.QueueDeclare(id, false, false, true, null);

                var consumer = new EventingBasicConsumer(channel);
                int result = 0;
                consumer.Received += (model, ea) =>
                  {
                      byte[] body = ea.Body;
                      result= int.Parse(Encoding.UTF8.GetString(body));
                      channel.QueueDelete(id, false, false);
                  };
                channel.BasicConsume(id, true, consumer);

                channel.BasicPublish("", "Login", true, properties, Messages.User.SerializeToByte(u));

                while (result.CompareTo(0) == 0)
                {
                    System.Threading.Thread.Sleep(100);
                }
                if (result.CompareTo(200) == 0)
                {
                    Response.Write("<script>alert('Logged!')</script>");
                }
                else
                {
                    Response.Write("<script>alert('User not found')</script>");
                }
              

            }
        }

        protected void LoginButton_Click(object sender, EventArgs e)
        {
            var userName = UserNameTextBox.Value.ToString();
            var password = PasswordTextBox.Value.ToString();
            UserLogin(new Messages.User { username = userName, password = password });

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