using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Data.SqlClient;
using System.Data.Sql;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
namespace RabbitMQ
{
    class Program
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

        public void InsertIntoDb(Messages.Messages m)
        {
            string conn = getConnectionString();
            using (SqlConnection connection = new SqlConnection(conn))
            {
                SqlCommand command = new SqlCommand("insert into [dbo].[myObjectTable] values ('" + m.name + "','" + m.value + "')", connection);
                command.Connection.Open();
                command.ExecuteNonQuery();
                command.Connection.Close();
            }
        }

static void Main(string[] args)
        {
            var factory = getFactory();
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.BasicQos(0, 1, true);
                channel.QueueDeclare("Add", false, false, false, null);
                Console.WriteLine("Listening");
                while (true)
                {
                    BasicGetResult result = channel.BasicGet("Add", true);
                    if (result == null)
                    {
                        // No message available at this time.
                    }
                    else
                    {
                        IBasicProperties props = result.BasicProperties;
                        byte[] body = result.Body;
                        Messages.Messages m = Messages.Messages.DeserializeFromByte(body);
                        Console.WriteLine("[Add]"+m.name+" "+m.value);
                    }
                }
            }

        }
    }
}
