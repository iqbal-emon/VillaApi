using AutoMapper;
using VillaApi.Model.Dtos;

namespace VillaApi.Model
{
    public class MapperConfig:Profile
    {
        public MapperConfig()
        {
            CreateMap<VillaRequestDto, Villa>().ReverseMap();
            CreateMap<Villa, VillaResponseDto>().ReverseMap();
        }

    }
}
