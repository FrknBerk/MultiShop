﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MultiShop.Message.DAL.Context;
using MultiShop.Message.DAL.Entities;
using MultiShop.Message.Dtos;

namespace MultiShop.Message.Services
{
    public class UserMessageService : IUserMessageService
    {
        private readonly MessageContext _messageContext;
        private readonly IMapper _mapper;

        public UserMessageService(MessageContext messageContext, IMapper mapper)
        {
            _messageContext = messageContext;
            _mapper = mapper;
        }

        public async Task CreateMessageCouponAsync(CreateMessageDto createMessageDto)
        {
            var value = _mapper.Map<UserMessage>(createMessageDto);
            await _messageContext.UserMessages.AddAsync(value);
            await _messageContext.SaveChangesAsync();
        }

        public async Task DeleteMessageCouponAsync(int id)
        {
            var values = await _messageContext.UserMessages.FindAsync(id);
            _messageContext.UserMessages.Remove(values);
            await _messageContext.SaveChangesAsync();
        }

        public async Task<List<ResultMessageDto>> GetAllMessageAsync()
        {
            var values = await _messageContext.UserMessages.ToListAsync();
            return _mapper.Map<List<ResultMessageDto>>(values);
        }

        public async Task<GetByIdMessageDto> GetByIdMessageCouponAsync(int id)
        {
            var values = await _messageContext.UserMessages.FindAsync(id);
            return _mapper.Map<GetByIdMessageDto>(values);
        }

        public async Task<int> GetFalseMessageCountAsync()
        {
            var values = await _messageContext.UserMessages.Where(x => x.IsRead == false).CountAsync();
            return values;
        }

        public async Task<List<ResultInboxMessageDto>> GetInboxMessageAsync(string recevierId)
        {
            var values = await _messageContext.UserMessages.Where(x => x.ReceiverId ==recevierId).ToListAsync();
            return _mapper.Map<List<ResultInboxMessageDto>>(values);
        }

        public async Task<List<ResultSendboxMessageDto>> GetSendMessageAsync(string sendId)
        {
            var values = await _messageContext.UserMessages.Where(x => x.SendedId == sendId).ToListAsync();
            return _mapper.Map<List<ResultSendboxMessageDto>>(values);
        }

        public async Task<int> GetTotalMessageCountAsync()
        {
            var values = await _messageContext.UserMessages.CountAsync();
            return values;
        }

        public async Task<int> GetTotalMessageCountByReceiverId(string id)
        {
            var values =await _messageContext.UserMessages.Where(x => x.ReceiverId == id).CountAsync();
            return values;
        }

        public async Task UpdateMessageCouponAsync(UpdateMessageDto updateMessageDto)
        {
            var values = _mapper.Map<UserMessage>(updateMessageDto);
            _messageContext.UserMessages.Update(values);
            await _messageContext.SaveChangesAsync();
        }
    }
}
