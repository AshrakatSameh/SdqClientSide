using Microsoft.AspNetCore.Identity;
using sdq.DTOs;
using sdq.Entities;
using sdq.Implementation;
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
        private readonly ILogger<TicketService> _logger;

        public TicketService(ITicketRepo repo, UserManager<ApplicationUser> userManager, ILogger<TicketService> logger)
        {
            _repo=repo;
            _userManager=userManager;
            _logger=logger;
        }

        public async Task<IEnumerable<TicketDto>> getAllWithFilter(int? category, int? status, int? priority)
        {
            return await _repo.GetTicketAllAsync(category, status, priority);
        }
        public async Task<IEnumerable<Ticket>> GetAllTicketsAsync(string? category, string? priority)
        {
            return await _repo.GetFilteredTicketsAsync(category, priority);
        }
        public async Task<TicketDto> GetTicketByIdAsync(int id)
        {
            return await _repo.GetTicketByIdAsync(id);
        }
        public async Task<bool> UpdateTicketAsync(long id, UpdateTicketStatusDto ticket, string userId)
        {
            // Validate the logged-in user
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                _logger.LogWarning($"User with ID {userId} not found.");
                return false;
            }

            // Ensure user is authorized (e.g., SupportAgent role)
            var roles = await _userManager.GetRolesAsync(user);
            if (!roles.Contains("SupportAgent"))
            {
                _logger.LogWarning($"User with ID {userId} does not have the required role to update tickets.");
                return false;
            }

            // Delegate to repository
            return await _repo.UpdateTicketAsync(id, ticket, Guid.Parse(userId));

        }

        public async Task<bool> DeleteTicketAsync(int id)
        {
            return await _repo.DeleteTicketAsync(id);
        }
    }
}
