using Messaging.Kafka.Api.Mappers;
using Messaging.Kafka.Application.DTO;
using Messaging.Kafka.Domain.Interfaces;
using Messaging.Kafka.Domain.Models;
using Messaging.Kafka.Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Messaging.Kafka.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IKafkaService _kafkaService;
        public MessageController(IKafkaService kafkaService)
        {
            _kafkaService = kafkaService;
        }

        /// <summary>
        /// Metodo para guardar mensajes en un topico
        /// </summary>
        /// <param name="messageRequest"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult SendMessage([FromBody] MessageRequest messageRequest)
        {
            // Verificar si el modelo es válido
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Devolver un error de validación si el modelo no es válido
            }

            try
            {
                // Mapear el mensaje
                Message message1 = MessageMapper.Map(messageRequest);

                // Enviar el mensaje a Kafka o servicio correspondiente
                bool result = _kafkaService.SendMessage(message1);

                if (result)
                {
                    return Ok(new { message = "Mensaje enviado con éxito" }); // Devuelve una respuesta informativa
                }
                else
                {
                    return BadRequest(new { error = "No se pudo enviar el mensaje" });
                }
            }
            catch (Exception ex)
            {

                // Devolver un mensaje genérico al cliente
                return StatusCode(500, new { error = "Error interno en el servidor" });
            }
        }


    }
}
