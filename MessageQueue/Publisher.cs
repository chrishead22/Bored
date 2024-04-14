using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;

namespace MessageQueue
{
    public static class DirectExchangePublisher
    {
        public static void Publish(IModel channel, Models.Activity activity)
        {
            var ttl = new Dictionary<string, object>
            {
                {"x-message-ttl", 30000 }
            };

            channel.ExchangeDeclare("direct-exchange", ExchangeType.Direct, arguments: ttl);

            Models.Activity newActivity = DataCollector.DataCollector.GetActivityByDescriptionAndType(activity.Description, activity.Type);

            if (newActivity == null)
            {
                var message = new { Name = "Producer", Message = $"Activity Not Found: Adding Activity to DB..." };
                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

                channel.BasicPublish("direct-exchange", "routingKey", null, body);
                Thread.Sleep(1000);
            }
        }
    }
}