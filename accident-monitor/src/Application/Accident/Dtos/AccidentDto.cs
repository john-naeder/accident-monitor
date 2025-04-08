using AccidentMonitor.Domain.Entities.Accident;
using AccidentMonitor.Domain.Enums;

namespace AccidentMonitor.Application.Accident.Dtos;

public class AccidentDto
{
    public Guid Guid { get; set; }
    public DateTime Timestamp { get; set; }
    public double Longitude { get; set; }
    public double Latitude { get; set; }
    public AccidentSeverity Severity { get; set; }
    public AccidentResolvedStatus ResolvedStatus { get; set; }
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<AccidentEntity, AccidentDto>()
                .ForMember(dest => dest.Guid, opt => opt.MapFrom(src => src.Guid))
                .ForMember(dest => dest.Timestamp, opt => opt.MapFrom(src => src.Timestamp))
                .ForMember(dest => dest.Longitude, opt => opt.MapFrom(src => src.Longitude))
                .ForMember(dest => dest.Latitude, opt => opt.MapFrom(src => src.Latitude))
                .ForMember(dest => dest.Severity, opt => opt.MapFrom(src => src.Severity))
                .ForMember(dest => dest.ResolvedStatus, opt => opt.MapFrom(src => src.ResolvedStatus));
        }
    }
}
