using Core.Utilities.Mail;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Utilities.MessageBrokers.RabbitMQ
{
    public class MqConsumerHelper : BackgroundService
    {
        private IModel _channel;
        private IConnection _connection;
        private readonly IConfiguration _configuration;
        private readonly MessageBrokerOptions _brokerOptions;
        private readonly IMailService _mailService;
        private readonly string _hostname;
        private readonly string _queueName;
        private readonly string _userName;
        private readonly string _password;

        public MqConsumerHelper(IConfiguration configuration,IMailService mailService, IOptions<MessageBrokerOptions> rabbitMqOptions)
        {
            _configuration = configuration;
            _brokerOptions = _configuration.GetSection("MessageBrokerOptions").Get<MessageBrokerOptions>();//apsettings'ten geliyor
            _mailService = mailService;
            _hostname = _brokerOptions.HostName;
            _queueName = _brokerOptions.QueueName;
            _userName = _brokerOptions.UserName;
            _password = _brokerOptions.Password;
            InitializeRabbitMqListener();

        }
        private void InitializeRabbitMqListener()
        {
            var factory = new ConnectionFactory()
            {
                HostName = _hostname,
                UserName = _userName,
                Password = _password
            };
            _connection = factory.CreateConnection();
            _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
            _channel = _connection.CreateModel();

            _channel.QueueDeclare(
                    queue: "DArchQueue",
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);
            
             

        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();
            //var consumer = new EventingBasicConsumer(_channel);
            GetQueue();
           // GetEmailQueue();
            //-
            /*
            consumer.Shutdown += OnConsumerShutdown;
            consumer.Registered += OnConsumerRegistered;
            consumer.Unregistered += OnConsumerUnregistered;
            consumer.ConsumerCancelled += OnConsumerCancelled;
            */
            //--
            return Task.CompletedTask;
        }

        public Task GetQueue()
         {
            var consumer = new EventingBasicConsumer(_channel);



            consumer.Received += (model, mq) =>
                {
                    var body = mq.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);

                    
                    _channel.BasicAck(mq.DeliveryTag, false);
                };
            consumer.Shutdown += OnConsumerShutdown;
            consumer.Registered += OnConsumerRegistered;
            consumer.Unregistered += OnConsumerUnregistered;
            consumer.ConsumerCancelled += OnConsumerCancelled;

            _channel.BasicConsume(
                    queue: _queueName,
                    autoAck: false,//??? neden falsea çevirdim
                    consumer: consumer);
            return Task.CompletedTask;
                
           }
        
        public void GetEmailQueue()//yukardakinin email consumer versiyonunu türettim
        {
            var factory = new ConnectionFactory()
            {
                HostName = _brokerOptions.HostName,
                UserName = _brokerOptions.UserName,
                Password = _brokerOptions.Password
            };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(
                    queue: _queueName,
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                var consumer = new EventingBasicConsumer(channel);

                consumer.Received += (model, mq) =>
                {
                    var body = mq.Body.ToArray();
                    var json=System.Text.Encoding.UTF8.GetString(body.ToArray());
                    var email = System.Text.Json.JsonSerializer.Deserialize< EmailMessage > (json);

                    HandleMail(email);

                    _channel.BasicAck(mq.DeliveryTag, false);
                };
                

                channel.BasicConsume(
                    queue: "DArchQueue",
                    autoAck: true,
                    consumer: consumer);
                Console.ReadKey();
            }
        }
        private void HandleMail(EmailMessage email)
        {
            _mailService.Send(email);//
        }

        private void OnConsumerCancelled(object sender, ConsumerEventArgs e)
        {
        }

        private void OnConsumerUnregistered(object sender, ConsumerEventArgs e)
        {
        }

        private void OnConsumerRegistered(object sender, ConsumerEventArgs e)
        {
        }

        private void OnConsumerShutdown(object sender, ShutdownEventArgs e)
        {
        }

        private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e)
        {
        }

        public override void Dispose()
        {
            _channel.Close();
            _connection.Close();
            base.Dispose();
        }
    }
}
