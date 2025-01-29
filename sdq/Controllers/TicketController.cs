using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using sdq.DTOs;
using sdq.Entities;
using sdq.Services;
using System.Security.Claims;

namespace sdq.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly TicketService _ticketService;

        public TicketController(TicketService ticketService)
        {
            _ticketService = ticketService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ticket>>> GetAllTicketsAsync([FromQuery] string? category, [FromQuery] string? priority)
        {
            var tickets = await _ticketService.GetAllTicketsAsync(category, priority);
            return Ok(tickets);
        }
        [HttpGet("getAllDetails")]
        public async Task<ActionResult<IEnumerable<TicketDto>>> GetAllTicketsWithFilterAsync([FromQuery] int? category, [FromQuery] int? priority, [FromQuery] int? status)
        {
            var tickets = await _ticketService.getAllWithFilter(category, status, priority);
            return Ok(tickets);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Ticket>> GetTicketByIdAsync(int id)
        {
            var ticketDto = await _ticketService.GetTicketByIdAsync(id);

            if (ticketDto == null)
            {
                return NotFound();
            }

            return Ok(ticketDto);
        }

        [Authorize(Roles = "SupportAgent")]
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTicketAsync(long id, UpdateTicketStatusDto ticket)
        {

            if (ticket == null)
            {
                return BadRequest("Invalid ticket data.");
            }

            // Get user ID from token
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User is not authorized.");
            }

            var result = await _ticketService.UpdateTicketAsync(id, ticket, userId);

            if (!result)
            {
                return NotFound("Ticket not found or the update operation was not successful.");
            }

            return Ok(new { message = "Ticket updated successfully." });

        }

        //[HttpDelete("{id}")]
        //public async Task<ActionResult> DeleteTicketAsync(int id)
        //{
        //    var result = await _ticketService.DeleteTicketAsync(id);
        //    if (!result)
        //    {
        //        return BadRequest("Failed to delete ticket.");
        //    }
        //    return Ok("Ticket deleted successfully.");
        //}
    }
}
