using VillaApi.Model.Dtos;

namespace VillaApi.Utility
{
    public static class VillaListHelper
    {
        public static List<VillaResponseDto> villaList = new List<VillaResponseDto>
            {
                new VillaResponseDto { Id = 1, Name = "Villa A" },
                new VillaResponseDto { Id = 2, Name = "Villa B" }
            };
    }
}
