using Microsoft.EntityFrameworkCore;
using sdq.DTOs;
using sdq.Entities;
using sdq.Repositories;

namespace sdq.Implementation
{
    public class MessageRepository : IMessageRepo
    {
        private readonly TicketsAndSupportContext _context;
        public MessageRepository(TicketsAndSupportContext context)
        {
            _context=context;
        }

        public async Task<TicketMessage> AddAsync(TicketMessage ticketMessage)
        {
            _context.TicketMessages.Add(ticketMessage);
            await _context.SaveChangesAsync();
            return ticketMessage;

        }

        public async Task DeleteAsync(long id)
        {
            var ticketMessage = await _context.TicketMessages.FindAsync(id);
            if (ticketMessage != null)
            {
                _context.TicketMessages.Remove(ticketMessage);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<TicketMessage>> GetAllAsync()
        {
            return await _context.TicketMessages
                    .ToListAsync();
        }

        public async Task<TicketMessage> GetByIdAsync(long id)
        {
            return await _context.TicketMessages.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<List<TicketMessage>> GetByTicketId(long ticketId)
        {
            return await _context.TicketMessages
                                 .Where(t => t.TicketId == ticketId)
                                 .ToListAsync();
        }


        public async Task UpdateAsync(TicketMessage ticketMessage)
        {
            _context.TicketMessages.Update(ticketMessage);
            await _context.SaveChangesAsync();
        }
    }
}
