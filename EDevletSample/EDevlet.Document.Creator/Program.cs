using EDevlet.Document.Common;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace EDevlet.Document.Creator
{
    internal class Program
    {

        static IConnection connection;
        private static readonly string createDocument = "create_document_queue";
        private static readonly string createDocumentRoutingKey = "create_document_queue_key";
        private static readonly string documentCreated = "document_created_queue";
        private static readonly string documentCreatedRoutingKey = "document_created_queue_key";
        private static readonly string documentCreatedExchange = "document_create_exchange";

        static IModel _channel;
        static IModel channel => _channel ?? (_channel = GetChannel());

        static void Main(string[] args)
        {
            connection = GetConnection();

            channel.ExchangeDeclare(documentCreatedExchange, ExchangeType.Direct);

            channel.QueueDeclare(createDocument, false, false, false);
            channel.QueueBind(createDocument, documentCreatedExchange, createDocumentRoutingKey);

            channel.QueueDeclare(documentCreated, false, false, false);
            channel.QueueBind(documentCreated, documentCreatedExchange, documentCreatedRoutingKey);

            ConsumeMessage();

            Console.WriteLine($"{documentCreatedExchange} listening...");
            Console.ReadLine();
        }

        private static void ConsumeMessage()
        {
            var consumerEvent = new EventingBasicConsumer(channel);
            consumerEvent.Received += (ch, ea) =>
            {
                var modelJson = Encoding.UTF8.GetString(ea.Body.ToArray());
                var model = JsonConvert.DeserializeObject<CreateDocumentModel>(modelJson);
                Console.WriteLine($"Received Data : {modelJson}");

                //create document
                //Task.Delay(5000).Wait();

                //document goest to ftp
                model.Url = $"http://www.turkiye.gov.tr/docs/x?user={model.UserId}.pdf";
                WriteToQueue(documentCreatedRoutingKey, model);
            };

            channel.BasicConsume(createDocument, true, consumerEvent);
        }

        private static IModel GetChannel()
        {
            return connection.CreateModel();
        }

        private static IConnection GetConnection()
        {
            var conenctionFactory = new ConnectionFactory()
            {
                Uri = new Uri("amqp://guest:guest@localhost:5672")
            };
            return conenctionFactory.CreateConnection();
        }

        private static void WriteToQueue(string routingKey, CreateDocumentModel model)
        {
            var messageArr = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(model));
            channel.BasicPublish(documentCreatedExchange, routingKey, null, messageArr);
            Console.WriteLine("Created document is published");
        }
    }
}