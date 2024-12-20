﻿using MultiShop.DtoLayer.IdentityDtos.UserDtos;

namespace MultiShop.WebUI.Services.IdentityServices.UserIdentityServices
{
    public interface IUserIdentityService
    {
        Task<List<ResultUserDto>> GetAllUserListAsync();
        Task<UpdateUserDto> GetByIdUser(string id);
        Task UpdateUser(UpdateUserDto user);
    }
}
