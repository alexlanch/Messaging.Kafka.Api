using Messaging.Kafka.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messaging.Kafka.Domain.Interfaces
{
    public interface IKafkaService
    {
        bool SendMessage(Message message);
    }
}

