﻿using Confluent.Kafka;
using Messaging.Kafka.Domain.Interfaces;
using Messaging.Kafka.Domain.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace Messaging.Kafka.Infrastructure.Repositories
{
    public class KafkaRepository : IKafkaRepository
    {
        readonly IProducer<Null, string> producer;
       
        public KafkaRepository()
        {
            var config = new ProducerConfig
            {
                BootstrapServers = "192.9.201.145:9092,192.9.201.146:9092,192.9.201.147:9092",
                ClientId = "Message.Kafka.Api"
            };
            producer = new ProducerBuilder<Null, string>(config).Build();
        }
        public bool SendMessage(Message message)
        {
            JsonSerializerSettings microsoftDateFormatSettings = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
            };
            string topicMessage = JsonConvert.SerializeObject(message, microsoftDateFormatSettings);

            producer.ProduceAsync("tracking.traffic.networkpktlistenertest", new Message<Null, string> { Value = topicMessage });
            
            return true;
        }
    }
}