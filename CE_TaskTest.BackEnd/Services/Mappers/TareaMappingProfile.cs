using AutoMapper;
using CE_TaskTest.BackEnd.Models;
using CE_TaskTest.BackEnd.Services.Dtos.Request;
using CE_TaskTest.BackEnd.Services.Dtos.Response;

namespace CE_TaskTest.BackEnd.Services.Mappers
{
    public class TareaMappingProfile : Profile
    {
        public TareaMappingProfile()
        {
            CreateMap<Tarea, TareaRequestDto>().ReverseMap();
            CreateMap<Tarea, TareaResponseDto>().ReverseMap();
        }
    }
}
