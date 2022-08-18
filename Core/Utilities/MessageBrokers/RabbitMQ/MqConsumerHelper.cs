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
        private readonly IMessageBrokerHelper _messageBrokerHelper;

        public MqConsumerHelper(IConfiguration configuration,IMailService mailService, IOptions<MessageBrokerOptions> rabbitMqOptions,IMessageBrokerHelper messageBrokerHelper)
        {
            _configuration = configuration;
            _brokerOptions = _configuration.GetSection("MessageBrokerOptions").Get<MessageBrokerOptions>();//apsettings'ten geliyor
            _mailService = mailService;
            _hostname = _brokerOptions.HostName;
            _queueName = _brokerOptions.QueueName;
            _userName = _brokerOptions.UserName;
            _password = _brokerOptions.Password;
            InitializeRabbitMqListener();
            _messageBrokerHelper= messageBrokerHelper;

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
           // GetQueue();
            GetEmailQueue();
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
        
        public Task GetEmailQueue()//yukardakinin email consumer versiyonunu türettim
        {       
                var consumer = new EventingBasicConsumer(_channel);

                consumer.Received += async (model, mq) =>
                {
                    var body = mq.Body.ToArray();
                    var json=Encoding.UTF8.GetString(body.ToArray());
                    var email = System.Text.Json.JsonSerializer.Deserialize< EmailMessage > (json);

                   await HandleMail(email);

                    _channel.BasicAck(mq.DeliveryTag, false);
                };
                consumer.Shutdown += OnConsumerShutdown;
                consumer.Registered += OnConsumerRegistered;
                consumer.Unregistered += OnConsumerUnregistered;
                consumer.ConsumerCancelled += OnConsumerCancelled;


                _channel.BasicConsume(
                    queue: "DArchQueue",
                    autoAck: true,
                    consumer: consumer);

                return Task.CompletedTask;

            
        }
        private async Task HandleMail(EmailMessage email)
        {
            if (email.TryCount<5)
            {
                try
                {
                    email.TryCount += 1;
                    email.Status = "sent";
                    await _mailService.SendEmailAsync(email);//

                }
                catch (Exception ex)
                {

                    email.TryCount += 1;//deneme sayısını 1 artır ve tekrar kuyruğa gönder
                    email.Status = "sending";

                    _messageBrokerHelper.QueueEmail(email);
                    throw;
                }
            }else if (email.TryCount == 5 )
            {
                email.Status = "failed";//deneme sayısı 5 olan mail tekrar gelir ve statüsü fail olarak değişir
            }
           
            
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
