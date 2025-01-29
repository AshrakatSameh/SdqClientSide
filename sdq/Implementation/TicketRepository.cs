using Microsoft.AspNetCore.Http.HttpResults;
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

        public async Task<IEnumerable<TicketDto>> GetTicketAllAsync(int? categoryId = null,
             int? statusId = null,
             int? priorityId = null)
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
            if (categoryId.HasValue)
            {
                query = query.Where(t => t.CategoryId == categoryId.Value);
            }

            if (statusId.HasValue)
            {
                query = query.Where(t => t.StatusId == statusId.Value);
            }

            if (priorityId.HasValue)
            {
                query = query.Where(t => t.Priority == priorityId.Value);
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



        public async Task<TicketDto> GetTicketByIdAsync(int id)
        {
            var ticket = await _context.Tickets
        .Include(t => t.Status)  // Include related Status
        .Include(t => t.Category)  // Include related Category
        .Include(t => t.PriorityNavigation)  // Include related Priority
        .Include(t => t.TicketAttachments)  // Include related Attachments
        .Include(t => t.TicketEmployeeAssignmentHistories)  // Include related Assignment Histories
        .Include(t => t.TicketMessages)  // Include related Messages
        .FirstOrDefaultAsync(t => t.Id == id);  // Get the Ticket by ID

            // Check if the ticket was found
            if (ticket == null)
            {
                return null;  // You can throw an exception here if you prefer
            }

            // Map the Ticket entity to TicketDto
            var ticketDto = new TicketDto
            {
                Id = ticket.Id,
                CustomerId = ticket.CustomerId,
                AssignedToEmployee = ticket.AssignedToEmployee,
                Title = ticket.Title,
                Description = ticket.Description,
                Status = ticket.Status?.Title,  // Map Status to its name
                Category = ticket.Category?.Title,  // Map Category to its name
                Priority = ticket.PriorityNavigation?.Title,  // Map Priority to its name

                // Map related collections to DTOs
                Attachments = ticket.TicketAttachments.Select(ta => new TicketAttachment
                {
                    Id = ta.Id,
                    FileTitle = ta.FileTitle,
                    FilePath = ta.FilePath
                }).ToList(),

                AssignmentHistories = ticket.TicketEmployeeAssignmentHistories.Select(th => new TicketEmployeeAssignmentHistory
                {
                    Id = th.Id,
                    AssignedBy = th.AssignedBy,
                    EmployeeId = th.EmployeeId,
                    AssignmentDate = th.AssignmentDate
                }).ToList(),

                Messages = ticket.TicketMessages.Select(tm => new TicketMessage
                {
                    Id = tm.Id,
                    CreatedBy = tm.CreatedBy,
                    CreatedAt = tm.CreatedAt,
                    Message = tm.Message,
                    IsSeen = tm.IsSeen
                }).ToList()
            };

            // Return the mapped TicketDto
            return ticketDto;
        }

        public async Task<bool> UpdateTicketAsync(long id, UpdateTicketStatusDto ticket, Guid assignedEmployeeId)
        {
            var existingTicket = await _context.Tickets.FindAsync(id);
            if (existingTicket == null)
            {
                _logger.LogWarning($"Ticket with ID {id} not found.");
                return false;
            }
            if (existingTicket.AssignedToEmployee != null)
            {
                existingTicket.StatusId = ticket.StatusId;
                _context.Tickets.Update(existingTicket);
                await _context.SaveChangesAsync();
                throw new InvalidOperationException("Ticket is already assigned.");
            }

            existingTicket.StatusId = ticket.StatusId;
            existingTicket.AssignedToEmployee = assignedEmployeeId;

            _context.Tickets.Update(existingTicket);
            await _context.SaveChangesAsync();

            _logger.LogInformation($"Ticket with ID {id} updated successfully. Assigned to employee {assignedEmployeeId}.");
            return true;
        
    }
    }
}
