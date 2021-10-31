using ApiTemplate.Domain.Models;
using AutoMapper;

namespace ApiTemplate.Contract
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.UserId, src => src.MapFrom(user => user.Id));
        }
    }
}