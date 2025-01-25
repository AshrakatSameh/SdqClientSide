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
        [HttpGet("getall details")]
        public async Task<ActionResult<IEnumerable<TicketDto>>> GetAllTicketsWithFilterAsync([FromQuery] string? category, [FromQuery] string? priority, [FromQuery] string? status)
        {
            var tickets = await _ticketService.getAllWithFilter(category, status, priority);
            return Ok(tickets);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Ticket>> GetTicketByIdAsync(int id)
        {
            var ticket = await _ticketService.GetTicketByIdAsync(id);
            if (ticket is null)
            {
                return NotFound();
            }
            return Ok(ticket);
        }

        [Authorize(Roles = "SupportAgent")]
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTicketAsync(long id, UpdateTicketStatusDto ticket)
        {
            var updatedBy = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //var updatedBy = User.FindFirst(JwtRegisteredClaimNames.NameId)?.Value;

            if (string.IsNullOrEmpty(updatedBy))
            {
                return Unauthorized("Invalid user ID.");
            }

            var result = await _ticketService.UpdateTicketAsync(id, ticket, updatedBy);
            if (!result)
            {
                return BadRequest("Failed to update ticket status.");
            }
            return Ok(new { message = "Ticket status updated successfully." });
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
