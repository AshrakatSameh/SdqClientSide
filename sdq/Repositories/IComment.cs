using sdq.DTOs;
using sdq.Entities;

namespace sdq.Repositories
{
    public interface IComment
    {
        Task<IEnumerable<Comment>> GetCommentsByTicketIdAsync(int ticketId);
        Task<Comment> GetCommentByIdAsync(int id);
        Task<bool> AddCommentAsync(CommentDto comment);
        Task<bool> UpdateCommentAsync(Comment comment);
        Task<bool> DeleteCommentAsync(int id);
    }
}
