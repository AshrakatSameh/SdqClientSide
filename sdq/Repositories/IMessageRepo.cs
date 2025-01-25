using sdq.DTOs;
using sdq.Entities;

namespace sdq.Repositories
{
    public interface IMessageRepo
    {
        Task<IEnumerable<TicketMessage>> GetAllAsync();
        Task<TicketMessage> GetByIdAsync(long id);
        Task<List<TicketMessage>> GetByTicketId(long ticketId);
        Task<TicketMessage> AddAsync(TicketMessage ticketMessage);
        Task UpdateAsync(TicketMessage ticketMessage);
        Task DeleteAsync(long id);
    }
}
