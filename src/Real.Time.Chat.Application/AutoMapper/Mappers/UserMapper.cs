using AutoMapper;
using Real.Time.Chat.Domain.Commands;
using Real.Time.Chat.Domain.Entity;
using Real.Time.Chat.Shared.Kernel.Entity;

namespace Real.Time.Chat.Application.AutoMapper.Mappers
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            CreateMap<UserAddCommand, User>();
            CreateMap<User, UserDto>();
        }
    }
}
