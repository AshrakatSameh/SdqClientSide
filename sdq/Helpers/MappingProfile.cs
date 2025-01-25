using AutoMapper;
using sdq.DTOs;
using sdq.Entities;

namespace sdq.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            // Mapping from TicketMessageDto to TicketMessage
            CreateMap<TicketMessageDto, TicketMessage>()
                .ForMember(dest => dest.TicketId, opt => opt.MapFrom(src => src.TicketId))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.Message, opt => opt.MapFrom(src => src.Message))
                .ForMember(dest => dest.IsSeen, opt => opt.MapFrom(src => src.IsSeen));

            // Mapping from TicketMessage to TicketMessageDto
            CreateMap<TicketMessage, TicketMessageDto>()
                .ForMember(dest => dest.TicketId, opt => opt.MapFrom(src => src.TicketId))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.CreatedBy))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.Message, opt => opt.MapFrom(src => src.Message))
                .ForMember(dest => dest.IsSeen, opt => opt.MapFrom(src => src.IsSeen));


        }
    }
}
