using AutoMapper;
using Real.Time.Chat.Application.AutoMapper.Mappers;

namespace Real.Time.Chat.Application.AutoMapper
{
    public class AutoMapperConfig
    {
        public static MapperConfiguration RegisterMappings()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new UserMapper());
                cfg.AddProfile(new MessageMapper());
            });
        }
    }
}
