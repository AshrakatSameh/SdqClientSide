using sdq.DTOs;
using sdq.Entities;

namespace sdq.Repositories
{
    public interface ITicketRepo
    {
        Task<IEnumerable<Ticket>> GetFilteredTicketsAsync(string? category, string? priority);
        Task<Ticket> GetTicketByIdAsync(int id);
        Task<IEnumerable<TicketDto>> GetTicketAllAsync(string? category, string? status, string? priority);
        Task<bool> UpdateTicketAsync(long id, UpdateTicketStatusDto ticket, string updatedBy);
        Task<bool> DeleteTicketAsync(int id);
    }
}
