using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using sdq.DTOs;
using sdq.Entities;
using sdq.Repositories;

namespace sdq.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly IComment _commentRepository;

        public CommentController(IComment commentRepository)
        {
            _commentRepository = commentRepository;
        }

        [HttpGet("ticket/{ticketId}")]
        public async Task<IActionResult> GetCommentsByTicketId(int ticketId)
        {
            var comments = await _commentRepository.GetCommentsByTicketIdAsync(ticketId);
            return Ok(comments);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCommentById(int id)
        {
            var comment = await _commentRepository.GetCommentByIdAsync(id);
            if (comment == null)
                return NotFound("Comment not found.");
            return Ok(comment);
        }

        [HttpPost]
        public async Task<IActionResult> AddComment([FromBody] CommentDto comment)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _commentRepository.AddCommentAsync(comment);
            if (!result)
                return BadRequest("Failed to add comment.");

            return Ok("Comment added successfully.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateComment(int id, [FromBody] Comment comment)
        {
            if (id != comment.Id)
                return BadRequest("Comment ID mismatch.");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _commentRepository.UpdateCommentAsync(comment);
            if (!result)
                return NotFound("Failed to update comment or comment not found.");

            return Ok("Comment updated successfully.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var result = await _commentRepository.DeleteCommentAsync(id);
            if (!result)
                return NotFound("Failed to delete comment or comment not found.");

            return Ok("Comment deleted successfully.");
        }
    }
}
