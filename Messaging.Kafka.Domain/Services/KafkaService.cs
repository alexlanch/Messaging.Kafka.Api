using Messaging.Kafka.Domain.Interfaces;
using Messaging.Kafka.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messaging.Kafka.Domain.Services
{
    public class KafkaService : IKafkaService
    {
        private readonly IKafkaRepository _kafkaRepository;
        public KafkaService(IKafkaRepository kafkaRepository) 
        {
            _kafkaRepository = kafkaRepository;
        }
        public bool SendMessage(Message message)
        {
            _kafkaRepository.SendMessage(message);

            return true;
        }
    }
}
