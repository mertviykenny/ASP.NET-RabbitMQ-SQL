using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RabbitMQ
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            var factory = new ConnectionFactory()
            {
                UserName = "dyqxxsuj",
                Password = "N8XsbtsmsuUVSjb0hx6yLV9Gi9QT1-QW",
                HostName = "hound-01.rmq.cloudamqp.com",
                VirtualHost = "dyqxxsuj"
            };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.BasicQos(0, 1, true);
                channel.QueueDeclare("frontend-to-backend", false, false, false, null);
                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (ch, ea) =>
                {
                    Console.WriteLine("Server get={0}", Encoding.UTF8.GetString(ea.Body));
                    channel.BasicAck(ea.DeliveryTag, false);
                };
            }
            while (true)
            {

            }

        }
    }
}
