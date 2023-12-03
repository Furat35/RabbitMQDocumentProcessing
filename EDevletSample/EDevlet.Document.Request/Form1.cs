using EDevlet.Document.Common;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace EDevlet.Document.Request
{
    public partial class Form1 : Form
    {
        IConnection connection;
        private static readonly string createDocument = "create_document_queue";
        private static readonly string createDocumentRoutingKey = "create_document_queue_key";
        private static readonly string documentCreated = "document_created_queue";
        private static readonly string documentCreatedRoutingKey = "document_created_queue_key";
        private static readonly string documentCreatedExchange = "document_create_exchange";

        IModel _channel;
        IModel channel => _channel ?? (_channel = GetChannel());

        private IModel GetChannel()
        {
            return connection.CreateModel();
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (connection == null || !connection.IsOpen)
                connection = GetConnection();

            btnCreateDocument.Enabled = true;

            channel.ExchangeDeclare(documentCreatedExchange, ExchangeType.Direct);

            channel.QueueDeclare(createDocument, false, false, false);
            channel.QueueBind(createDocument, documentCreatedExchange, createDocumentRoutingKey);

            channel.QueueDeclare(documentCreated, false, false, false);
            channel.QueueBind(documentCreated, documentCreatedExchange, documentCreatedRoutingKey);

            Console.WriteLine("Connection is open now");
        }

        private void btnCreateDocument_Click(object sender, EventArgs e)
        {
            int randomDocType = new Random().Next(0, Enum.GetNames<DocumentType>().Count());
            var model = new CreateDocumentModel()
            {
                UserId = Guid.NewGuid(),
                DocumentType = Enum.GetValues<DocumentType>()[randomDocType],
            };


            WriteToQueue(createDocumentRoutingKey, model);

            var consumerEvent = new EventingBasicConsumer(channel);
            consumerEvent.Received += (ch, ea) =>
            {
                var modelReceived = JsonConvert.DeserializeObject<CreateDocumentModel>(Encoding.UTF8.GetString(ea.Body.ToArray()));
                AddLog($"Received Data Url: {modelReceived.Url}");
            };

            channel.BasicConsume(documentCreated, true, consumerEvent);
        }

        private void WriteToQueue(string routingKey, CreateDocumentModel model)
        {
            var messageArr = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(model));
            channel.BasicPublish(documentCreatedExchange, routingKey, null, messageArr);
            AddLog("Message published");
        }

        private IConnection GetConnection()
        {
            var conenctionFactory = new ConnectionFactory()
            {
                Uri = new Uri(txtConnectionString.Text)
            };
            return conenctionFactory.CreateConnection();
        }

        private void AddLog(string logStr)
        {
            if (txtLog.InvokeRequired)
            {
                txtLog.Invoke(new Action(() => AddLog(logStr)));
                return;
            }
            logStr = $"{DateTime.Now:dd.MM.yyyy HH:mm:ss} - {logStr}";
            txtLog.AppendText($"{logStr}\n");

            txtLog.SelectionStart = txtLog.Text.Length;
            txtLog.ScrollToCaret();
        }
    }
}