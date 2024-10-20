using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messaging.Kafka.Domain.Models
{
    public class Message
    {
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Telephone { get; set; }

    }
}