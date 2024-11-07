using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Message.Dtos;
using MultiShop.Message.Services;

namespace MultiShop.Message.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserMessagesController : ControllerBase
    {
        private readonly IUserMessageService _userMessageService;

        public UserMessagesController(IUserMessageService userMessageService)
        {
            _userMessageService = userMessageService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMEssage()
        {
            var values = await _userMessageService.GetAllMessageAsync();
            return Ok(values);
        }

        [HttpGet("GetMessageSendBox")]
        public async Task<IActionResult> GetMessageSendBox(string id)
        {
            var values = await _userMessageService.GetSendMessageAsync(id);
            return Ok(values);
        }

        [HttpGet("GetMessageInBox")]
        public async Task<IActionResult> GetMessageInBox(string id)
        {
            var values = await _userMessageService.GetInboxMessageAsync(id);
            return Ok(values);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMessageAsync(CreateMessageDto createMessageDto)
        {
            await _userMessageService.CreateMessageCouponAsync(createMessageDto);
            return Ok("Mesaj başarıyla eklendi");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteMessageAsync(int id)
        {
            var result = await _userMessageService.DeleteMessageAsync(id);
            if (result) return Ok(true);
            else return BadRequest(false);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateMessageAsync(UpdateMessageDto updateMessageDto)
        {
            await _userMessageService.UpdateMessageCouponAsync(updateMessageDto);
            return Ok("Mesaj başarıyla güncellendi");
        }

        [HttpGet("GetTotalMessageCount")]
        public async Task<IActionResult> GetTotalMessageCountAsync()
        {
            var values = await _userMessageService.GetTotalMessageCountAsync();
            return Ok(values);
        } 
        
        [HttpGet("GetTotalMessageCountByReceiverId")]
        public async Task<IActionResult> GetTotalMessageCountByReceiverId(string id)
        {
            var values = await _userMessageService.GetTotalMessageCountByReceiverId(id);
            return Ok(values);
        }
        
        [HttpGet("GetFalseMessageCount")]
        public async Task<IActionResult> GetFalseMessageCountAsync()
        {
            var values = await _userMessageService.GetFalseMessageCountAsync();
            return Ok(values);
        }
        
        [HttpGet("GetByIdSendId/{id}")]
        public async Task<IActionResult> GetByIdSendIdAsync(string id)
        {
            var values = await _userMessageService.GetByIdSendIdAsync(id);
            return Ok(values);
        }
        
        [HttpGet("GetAdminMessageList/{id}")]
        public async Task<IActionResult> GetAdminMessageListAsync(string id)
        {
            var values = await _userMessageService.GetAdminMessageListAsync(id);
            return Ok(values);
        }
        
        [HttpGet("GetAdminUnReadMessageTotalCount/{id}")]
        public async Task<IActionResult> GetAdminUnReadMessageTotalCountAsync(string id)
        {
            var values = await _userMessageService.GetAdminUnReadMessageTotalCountAsync(id);
            return Ok(values);
        }
        
        [HttpGet("GetUnReadMessageList/{id}")]
        public async Task<IActionResult> GetUnReadMessageListASync(string id)
        {
            var values = await _userMessageService.GetUnReadMessageList(id);
            return Ok(values);
        }
        
        [HttpGet("AdminAnswerUserMessageIdUpdateTrue/{id}")]
        public async Task<IActionResult> AdminAnswerUserMessageIdUpdateTrueAsync(int id)
        {
            var values = await _userMessageService.AdminAnswerUserMessageIdUpdateTrue(id);
            if (values)
                return Ok(true);
            return Ok(false);
        }
    }
}
