using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace MessageQueue
{
    public static class DirectExchangeConsumer
    {
        public static void Consume(IModel channel, Models.Activity activity)
        {
            channel.ExchangeDeclare("direct-exchange", ExchangeType.Direct);
            channel.QueueDeclare("direct-queue",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);
            channel.QueueBind("direct-queue", "direct-exchange", "routingKey");
            channel.BasicQos(0, 10, false);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender, e) => {
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(message);
            };

            DataCollector.DataCollector.SaveActivity(activity.Description, activity.Type, activity.Participants, activity.Price, activity.Accessibility);

            channel.BasicConsume("direct-queue", true, consumer);
            Console.WriteLine("Activity Added, Done.");
        }
    }
}