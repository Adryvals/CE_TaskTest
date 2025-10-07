using AutoMapper;
using CE_TaskTest.BackEnd.Models;
using CE_TaskTest.BackEnd.Services.Dtos.Request;
using CE_TaskTest.BackEnd.Services.Dtos.Response;

namespace CE_TaskTest.BackEnd.Services.Mappers
{
    public class EstimacionMappingProfile : Profile
    {
        public EstimacionMappingProfile()
        {
            CreateMap<Estimacion, EstimacionResponseDto>().ReverseMap();
            CreateMap<Estimacion, EstimacionRequestDto>().ReverseMap();
        }
    }
}
