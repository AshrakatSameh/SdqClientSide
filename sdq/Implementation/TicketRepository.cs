using Microsoft.EntityFrameworkCore;
using sdq.Data;
using sdq.DTOs;
using sdq.Entities;
using sdq.Repositories;

namespace sdq.Implementation
{
    public class TicketRepository : ITicketRepo
    {
        private readonly TicketsAndSupportContext _context;
        private readonly ILogger<TicketRepository> _logger;

        public TicketRepository(TicketsAndSupportContext context, ILogger<TicketRepository> logger)
        {
            _context=context;
            _logger=logger;
        }

        public async Task<bool> DeleteTicketAsync(int id)
        {
            var existingTicket = await _context.Set<Ticket>().FindAsync(id);
            if (existingTicket == null) return false;
            if (existingTicket.CategoryId != 4) return false;
            _context.Tickets.Remove(existingTicket);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Ticket>> GetFilteredTicketsAsync(string? category, string? priority)
        {
            var query = _context.Tickets.AsQueryable();

            // Apply filters if query parameters are provided
            if (!string.IsNullOrEmpty(category))
            {
                query = query.Where(t => t.Category.Title == category);
            }

            if (!string.IsNullOrEmpty(priority))
            {
                query = query.Where(t => t.PriorityNavigation.Title == priority);
            }
            return await query.ToListAsync();
        }

        public async Task<IEnumerable<TicketDto>> GetTicketAllAsync(string? category = null, 
            string? status = null,
            string? priority = null)
        {
            // Start with the base query
            var query = _context.Tickets.AsQueryable()
                .Include(t => t.Category)
                .Include(t => t.Status)
                .Include(t => t.PriorityNavigation)
                .Include(t => t.TicketAttachments)
                .Include(t => t.TicketEmployeeAssignmentHistories)
                .Include(t => t.TicketMessages)
                .AsQueryable();

            // Apply filters dynamically
            if (!string.IsNullOrEmpty(category))
            {
                query = query.Where(t => t.Category.Title == category);
            }

            if (!string.IsNullOrEmpty(status))
            {
                query = query.Where(t => t.Status.Title == status);
            }

            if (!string.IsNullOrEmpty(priority))
            {
                query = query.Where(t => t.PriorityNavigation.Title == priority);
            }

            // Project to TicketDto
            var tickets = await query
                .Select(t => new TicketDto
                {
                    Id = t.Id,
                    CustomerId = t.CustomerId,
                    AssignedToEmployee = t.AssignedToEmployee,
                    Title = t.Title,
                    Description = t.Description,
                    Category = t.Category.Title,
                    Status = t.Status.Title,
                    Priority = t.PriorityNavigation.Title,
                    Attachments = t.TicketAttachments,
                    AssignmentHistories = t.TicketEmployeeAssignmentHistories,
                    Messages = t.TicketMessages
                })
                .ToListAsync();

            return tickets;
        }



        public async Task<Ticket> GetTicketByIdAsync(int id)
        {
            return await _context.Tickets.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<bool> UpdateTicketAsync(long id, UpdateTicketStatusDto ticket, string updatedBy)
        {
            var existingTicket = await _context.Tickets.FindAsync(id);
            if (existingTicket == null)
            {
                // Log if ticket is not found
                _logger.LogWarning($"Ticket with ID {id} not found.");
                return false;
            }
            existingTicket.StatusId = ticket.StatusId;

            _context.Tickets.Update(existingTicket);
            await _context.SaveChangesAsync();

            // Log success
            _logger.LogInformation($"Ticket with ID {id} updated successfully.");
            return true;
        }
    }
}
