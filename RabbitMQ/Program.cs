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

        public static void InsertIntoDb(Messages.User u)
        {
            string conn = getConnectionString();
            using (SqlConnection connection = new SqlConnection(conn))
            {
                SqlCommand command = new SqlCommand("insert into [dbo].[users] values ('" + u.username+ "','" + u.password + "')", connection);
                command.Connection.Open();
                command.ExecuteNonQuery();
                command.Connection.Close();
            }
        }
        public static bool CheckIfUserExists(Messages.User u)
        {
            string conn = getConnectionString();
            using (SqlConnection connection = new SqlConnection(conn))
            {
                SqlCommand command = new SqlCommand("select * from [dbo].[users] where username='" + u.username + "' and password='" + u.password + "'", connection);
                command.Connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        return true;
                    }
                    reader.Close();
                    command.Connection.Close();
                }
                return false;
            }
        }

        public static void InsertIntoDb(Messages.Message m)
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


        public static void AddThread()
        {
            var factory = getFactory();
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.BasicQos(0, 1, true);
                channel.QueueDeclare("Add", false, false, false, null);
                Console.WriteLine("[Add]Listening");
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
                        Messages.Message m = Messages.Message.DeserializeFromByte(body);
                        InsertIntoDb(m);
                        Console.WriteLine("[Add]" + m.name + " " + m.value);
                    }
                }
            }
        }

        public static void RegisterThread()
        {
            var factory = getFactory();
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.BasicQos(0, 1, true);
                channel.QueueDeclare("Register", false, false, false, null);
                Console.WriteLine("[Register]Listening");
                while (true)
                {
                    BasicGetResult result = channel.BasicGet("Register", true);
                    if (result == null)
                    {
                        // No message available at this time.
                    }
                    else
                    {
                        IBasicProperties props = result.BasicProperties;
                        byte[] body = result.Body;
                        Messages.User u = Messages.User.DeserializeFromByte(body);
                        InsertIntoDb(u);
                        Console.WriteLine("[Register]" + u.username + " " + u.password);
                    }
                }
            }
        }

        public static void LoginThread()
        {
            var factory = getFactory();
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.BasicQos(0, 1, true);
                channel.QueueDeclare("Login", false, false, false, null);
                Console.WriteLine("[Login]Listening");
                while (true)
                {
                    BasicGetResult result = channel.BasicGet("Login", true);
                    if (result == null)
                    {
                        // No message available at this time.
                    }
                    else
                    {
                        IBasicProperties props = result.BasicProperties;
                        byte[] body = result.Body;
                        Messages.User u = Messages.User.DeserializeFromByte(body);
                        bool isLogged=CheckIfUserExists(u);
                        Console.WriteLine("[Login]" + u.username + " " + u.password + " "+((isLogged) ? "is exists" : "doesn't exists"));
                        channel.QueueDeclare(props.CorrelationId, false, false, true, null);
                        if (isLogged)
                        {
                            
                            channel.BasicPublish("",props.CorrelationId, false,null, Encoding.UTF8.GetBytes("200"));   
                            return;//200;
                        }
                        else
                        {
                            channel.BasicPublish("", props.CorrelationId, false, null, Encoding.UTF8.GetBytes("404"));
                            return;//404
                        }
                        
                    }
                }
            }
        }



        static void Main(string[] args)
        {
            System.Threading.Thread t = new System.Threading.Thread(AddThread);
            t.Start();
            var t2= new System.Threading.Thread(RegisterThread);
            t2.Start();
            var t3= new System.Threading.Thread(LoginThread);
            t3.Start();

            Console.ReadLine();
        }
    }
}
