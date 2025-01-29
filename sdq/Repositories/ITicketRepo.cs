using sdq.DTOs;
using sdq.Entities;

namespace sdq.Repositories
{
    public interface ITicketRepo
    {
        Task<IEnumerable<Ticket>> GetFilteredTicketsAsync(string? category, string? priority);
        Task<TicketDto> GetTicketByIdAsync(int id);
        Task<IEnumerable<TicketDto>> GetTicketAllAsync(int? categoryId = null, int? statusId = null,int? priorityId = null);
        Task<bool> UpdateTicketAsync(long id, UpdateTicketStatusDto ticket, Guid assignedEmployeeId);
        Task<bool> DeleteTicketAsync(int id);
    }
}
