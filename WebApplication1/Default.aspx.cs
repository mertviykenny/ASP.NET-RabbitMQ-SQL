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

        public ConnectionFactory getFactory()
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

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private void InitializeComponent()
        {

        }
    }
}