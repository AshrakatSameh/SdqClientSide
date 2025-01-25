using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using sdq.DTOs;
using sdq.Entities;
using sdq.Repositories;

namespace sdq.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketMessageController : ControllerBase
    {
        private readonly IMessageRepo _Message;
        public TicketMessageController(IMessageRepo messageRepo)
        {
            _Message = messageRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TicketMessage>>> GetAll()
        {
            var ticketMessages = await _Message.GetAllAsync();
            return Ok(ticketMessages);
        }

        // GET: api/TicketMessage/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TicketMessageDto>> GetById(long id)
        {
            var ticketMessage = await _Message.GetByIdAsync(id);
            if (ticketMessage == null)
            {
                return NotFound();
            }
            return Ok(ticketMessage);
        }

        // POST: api/TicketMessage
        [HttpPost]
        public async Task<ActionResult<TicketMessageDto>> Create(TicketMessageDto ticketMessageDto)
        {
            if (ticketMessageDto == null)
            {
                return BadRequest("Invalid input data.");
            }

            // Manually map TicketMessageDto to TicketMessage
            var ticketMessage = new TicketMessage
            {
                TicketId = ticketMessageDto.TicketId,
                CreatedBy = ticketMessageDto.CreatedBy,
                CreatedAt = ticketMessageDto.CreatedAt,
                Message = ticketMessageDto.Message,
                IsSeen = ticketMessageDto.IsSeen
            };

            // Call the repository to add the new TicketMessage
            var createdTicketMessage = await _Message.AddAsync(ticketMessage);

            // Return the created ticket message with a 201 Created status

            return Ok(ticketMessage);
        }

        // PUT: api/TicketMessage/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(long id, TicketMessageDto ticketMessageDto)
        {
            if (id != ticketMessageDto.Id)
            {
                return BadRequest();
            }

            var existingTicketMessage = await _Message.GetByIdAsync(id);
            if (existingTicketMessage == null)
            {
                return NotFound();
            }

            await _Message.UpdateAsync(existingTicketMessage);
            return NoContent();
        }

        // DELETE: api/TicketMessage/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var ticketMessage = await _Message.GetByIdAsync(id);
            if (ticketMessage == null)
            {
                return NotFound();
            }

            await _Message.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("ticketMessages/{ticketId}")]
        public async Task<ActionResult<List<TicketMessageDto>>> GetByTicketId(long ticketId)
        {
            var ticketMessages = await _Message.GetByTicketId(ticketId);
            if (ticketMessages == null || !ticketMessages.Any())
            {
                return NotFound();
            }
            var ticketMessageDtos = ticketMessages.Select(t => new TicketMessageDto
            {
                TicketId = t.TicketId,
                CreatedBy = t.CreatedBy,
                CreatedAt = t.CreatedAt,
                Message = t.Message,
                IsSeen = t.IsSeen
            }).ToList();

            return Ok(ticketMessageDtos);
        }

    }
}
