using Microsoft.EntityFrameworkCore;
using sdq.Data;
using sdq.DTOs;
using sdq.Entities;
using sdq.Repositories;

namespace sdq.Implementation
{
    public class CommentRepo 
    {
        //private readonly AppDbContext _context;
        //public CommentRepo(AppDbContext context)
        //{
        //    _context=context;
        //}

        //public async Task<bool> AddCommentAsync(CommentDto comment)
        //{
        //    var newComment = new Comment
        //    {
        //        Content = comment.Content,
        //        CreatedBy = comment.CreatedBy,
        //        TicketId = comment.TicketId
        //    };

        //    await _context.Comments.AddAsync(newComment);
        //    await _context.SaveChangesAsync();
        //    return true;
        //}

        //public async Task<bool> DeleteCommentAsync(int id)
        //{
        //    var comment = await _context.Comments.FindAsync(id);
        //    if (comment == null)
        //        return false;

        //    _context.Comments.Remove(comment);
        //    await _context.SaveChangesAsync();
        //    return true;
        //}

        //public async Task<Comment> GetCommentByIdAsync(int id)
        //{
        //    return await _context.Comments.FirstOrDefaultAsync(c => c.Id == id);
        //}

        //public async Task<IEnumerable<Comment>> GetCommentsByTicketIdAsync(int ticketId)
        //{
        //    return await _context.Comments
        //   .Where(c => c.TicketId == ticketId)
        //   .ToListAsync();
        //}

        //public async Task<bool> UpdateCommentAsync(Comment comment)
        //{
        //    var existingComment = await _context.Comments.FindAsync(comment.Id);
        //    if (existingComment == null)
        //        return false;

        //    existingComment.Content = comment.Content;
        //    existingComment.CreatedBy = comment.CreatedBy;
        //    _context.Comments.Update(existingComment);

        //    await _context.SaveChangesAsync();
        //    return true;
        //}
    }
}
