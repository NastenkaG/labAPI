using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;

namespace labAPI.Mapping
{
    public class MappingGarden : Profile
    {
        public MappingGarden()
        {
            CreateMap<Garden, GardenDto>()
                .ForMember(g => g.Country,
                opt => opt.MapFrom(x => string.Join(' ', x.Country)));
        }
    }
}
