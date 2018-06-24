using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.IO;
using RabbitMQ;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace WebApplication1
{

    public partial class _Default : Page
    {

        public static string getConnectionString()
        {
            return Properties.Settings.Default.conn;
        }

        public static ConnectionFactory getFactory()
        {
            var factory = new ConnectionFactory()
            {
                UserName = Properties.Settings.Default.rabbitMQUsername,
                Password = Properties.Settings.Default.rabbitMQPassword,
                HostName = Properties.Settings.Default.rabbitMQHostName,
                VirtualHost = Properties.Settings.Default.rabbitMQVhost
            };
            return factory;
        }


        public void sendMessage(Messages.Message m)
        {
            var factory = getFactory();
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare("Add", false, false, false, null);
                channel.BasicPublish("", "Add", true, null, Messages.Message.SerializeToByte(m));
            }
        }

        protected void AddBtn_Click(object sender, EventArgs e)
        {
            string name = Input1.Value.ToString();
            string value = Text2.Value.ToString();
            Messages.Message m = new Messages.Message(name, value);
            sendMessage(m);
        }
    }
}