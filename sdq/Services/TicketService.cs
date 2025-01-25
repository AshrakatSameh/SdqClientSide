using Microsoft.AspNetCore.Identity;
using sdq.DTOs;
using sdq.Entities;
using sdq.Repositories;

namespace sdq.Services
{
    public class TicketService
    {
        //The service layer validates roles and handles role-based access.
        //The service layer also handles business logic.
        //The service layer is the only layer that can access the repository layer.

        private readonly ITicketRepo _repo;
        private readonly UserManager<ApplicationUser> _userManager;

        public TicketService(ITicketRepo repo, UserManager<ApplicationUser> userManager)
        {
            _repo=repo;
            _userManager=userManager;
        }

        public async Task<IEnumerable<TicketDto>> getAllWithFilter(string? category, string? status, string? priority)
        {
            return await _repo.GetTicketAllAsync(category, status, priority);
        }
        public async Task<IEnumerable<Ticket>> GetAllTicketsAsync(string? category, string? priority)
        {
            return await _repo.GetFilteredTicketsAsync(category, priority);
        }
        public async Task<Ticket> GetTicketByIdAsync(int id)
        {
            return await _repo.GetTicketByIdAsync(id);
        }
        public async Task<bool> UpdateTicketAsync(long id, UpdateTicketStatusDto ticket, string updatedBy)
        {
            var user = await _userManager.FindByIdAsync(updatedBy);
            if (user is null)
            {
                // Return false if the user does not exist
                return false;
            }
            var roles = await _userManager.GetRolesAsync(user);
            if (!roles.Contains("SupportAgent"))
            {
                // Return false if the user is not an admin
                return false;
            }
            return await _repo.UpdateTicketAsync(id, ticket, updatedBy);
        }

        public async Task<bool> DeleteTicketAsync(int id)
        {
            return await _repo.DeleteTicketAsync(id);
        }
    }
}
