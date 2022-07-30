using AutoMapper;
using Real.Time.Chat.Domain.Commands.Message;
using Real.Time.Chat.Domain.Entity;
using System;

namespace Real.Time.Chat.Application.AutoMapper.Mappers
{
    public class MessageMapper : Profile
    {
        public MessageMapper()
        {
            CreateMap<MessageAddCommand, Messages>()
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => DateTime.Now));
        }
    }
}
