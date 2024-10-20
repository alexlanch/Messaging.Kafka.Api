using Messaging.Kafka.Application.DTO;
using Messaging.Kafka.Domain.Models;

namespace Messaging.Kafka.Api.Mappers
{
    public static class MessageMapper
    {
        public static Message Map(MessageRequest messageRequest) 
        {
            return new Message
            {
                Name = messageRequest.Name,
                Lastname = messageRequest.Lastname,
                Telephone = messageRequest.Telephone
            };
        }
    }
}
