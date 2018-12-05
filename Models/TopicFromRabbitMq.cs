using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using quizartsocial_backend.Models;
using quizartsocial_backend;

namespace SocialServer.Consumers
{
    public class TopicConsumer
    {
        ITopic topicObj;
        public TopicConsumer(SocialContext socialContext)
        {
            this.topicObj = new TopicRepo(socialContext);
            GetTopicsFromRabbitMQ();
        }
        public void GetTopicsFromRabbitMQ()
        {
            var factory = new ConnectionFactory() { HostName = "rabbitmq", Port = 5672, UserName = "rabbitmq", Password = "rabbitmq" };
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();
            
            channel.QueueDeclare(queue: "Topic", durable: false, exclusive: false, autoDelete: false, arguments: null);
            
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body;
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(message);
                Topic obj = new Topic();
                obj.topicName = message;
                this.topicObj.AddTopicToDBAsync(obj);
                Console.WriteLine(" [x] Received {0}", message);
            };
            channel.BasicConsume(queue: "Topic", autoAck: true, consumer: consumer);
            Console.WriteLine(" Press [enter] to exit.");
            Console.ReadLine();
        }
    
    }
}