using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MultiShop.Comment.Context;
using MultiShop.Comment.Dtos;
using MultiShop.Comment.Entities;

namespace MultiShop.Comment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class CommentsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly CommentContext _commentContext;

        public CommentsController(CommentContext commentContext, IMapper mapper)
        {
            _commentContext = commentContext;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult CommentList()
        {
            var values = _commentContext.UserComments.ToList();
            return Ok(values);
        }

        [HttpPost]
        public IActionResult CreateComment(CreateUserCommentDto userComment)
        {
            
            _commentContext.Add(_mapper.Map<UserComment>(userComment));
            _commentContext.SaveChanges();
            return Ok("Yorum Başarıyla eklendi");
        }

        [HttpPut]
        public IActionResult UpdateComment(UserComment userComment)
        {
            _commentContext.Update(userComment);
            _commentContext.SaveChanges();
            return Ok("Yorum Başarıyla güncellendi");
        }

        [HttpDelete]
        public IActionResult DeleteComment(int id)
        {
            var value = _commentContext.UserComments.Find(id);
            _commentContext.UserComments.Remove(value);
            _commentContext.SaveChanges();
            return Ok("Yorum başarıyla silindi");
        }

        [HttpGet("{id}")]
        public IActionResult GetComment(int id)
        {
            var value = _commentContext.UserComments.Find(id);
            return Ok(value);
        }

        [HttpGet("CommentListByProductId")]
        public async Task<IActionResult> CommentListByProductId(string id)
        {
            var value = await _commentContext.UserComments.Where(x => x.ProductId == id).ToListAsync();
            return Ok(value);
        }
    }
}
