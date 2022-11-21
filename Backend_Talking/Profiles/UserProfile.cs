using AutoMapper;
using ChatAPI.Models;
using ChatAPI.Models.Dtos;

namespace ChatAPI.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile() {
            CreateMap<CreateUserDto, User>()
                .ForMember(dest => dest.CreatedAt,
                    opt => opt.MapFrom(src => DateTime.Now));
            CreateMap<User, CreateUserDto>();
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.ConversationIds,
                    opt => opt.MapFrom(src => 
                    src.Conversations.Select(x => x.Id).ToList()));
            CreateMap<Conversation, GetConversationDto>()
                .ForMember(dest => dest.Partitioners,
                    opt => opt.MapFrom(src => src.Partitioners.Select(x => x.Id).ToList()))
                .ForMember(dest => dest.Messages,
                    opt => opt.MapFrom(src => src.Messages.Select(x => new MessageDto()
                    {
                        Id = x.Id,
                        SenderId = x.SenderId,
                        Text = x.Text,
                        CreatedAt = x.CreatedAt
                    }).ToList()));
            CreateMap<CreateMessageDto, Message>()
                .ForMember(dest => dest.CreatedAt,
                    opt => opt.MapFrom(src => DateTime.Now));
        }
    }
}
