using AutoMapper;
using Loymark.Back.Application.DTOs;
using Loymark.Back.Domain.Entities;

namespace Loymark.Back.Application.Profiles
{
    public class UserProfiles : Profile
    {
        public UserProfiles()
        {
            CreateMap<User, UserDto>().ReverseMap();
        }
    }
}
