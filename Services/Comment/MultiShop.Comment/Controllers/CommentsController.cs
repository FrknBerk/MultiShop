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
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
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
        public IActionResult UpdateComment(UpdateUserCommentDto updateCommentDto)
        {
            _commentContext.Update(_mapper.Map<UserComment>(updateCommentDto));
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

        [HttpGet("GetActiveCommentCount")]
        public IActionResult GetActiveCommentCount()
        {
            int values = _commentContext.UserComments.Where(x => x.Status == true).Count();
            return Ok(values);
        }

        [HttpGet("GetPasiveCommentCount")]
        public IActionResult GetPasiveCommentCount()
        {
            int values = _commentContext.UserComments.Where(x => x.Status == false).Count();
            return Ok(values);
        }

        [HttpGet("GetTotalCommentCount")]
        public IActionResult GetTotalCommentCount()
        {
            int values = _commentContext.UserComments.Count();
            return Ok(values);
        }

        [HttpGet("UnconfirmedProductComment")]
        public IActionResult UnconfirmedProductComment()
        {
            var values = _commentContext.UserComments.Where(x => x.Status == false).ToList();
            return Ok(values);
        }
        [HttpGet("UnconfirmedProductCommentCount")]
        public IActionResult UnconfirmedProductCommentCount()
        {
            var values = _commentContext.UserComments.Where(x => x.Status == false).Count();
            return Ok(values);
        }
    }
}
