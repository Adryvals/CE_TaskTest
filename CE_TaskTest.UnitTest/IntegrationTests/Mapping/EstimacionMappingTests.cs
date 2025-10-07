using AutoMapper;
using CE_TaskTest.BackEnd.Models;
using CE_TaskTest.BackEnd.Services.Dtos.Request;
using CE_TaskTest.BackEnd.Services.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CE_TaskTest.TestSector.IntegrationTests.Mapping
{
    public class EstimacionMappingTests
    {
        [Fact]
        public void EstimacionRequestDto_To_Estimacion_Mapping_IsValid()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<EstimacionMappingProfile>();
            });

            var mapper = config.CreateMapper();

            var dto = new EstimacionRequestDto
            {
                Duracion = 10
            };

            var estimacion = mapper.Map<Estimacion>(dto);

            Assert.Equal(dto.Duracion, estimacion.Duracion);
        }

        [Fact]
        public void ListEstimacionRequest_To_ListEstimacion_Mapping_IsValid()
        {
            var config = new MapperConfiguration(cfg => { cfg.AddProfile<EstimacionMappingProfile>(); });

            var mapper = config.CreateMapper();

            var listDtos = new List<EstimacionRequestDto>();

            var Estimacion1 = new EstimacionRequestDto
            {
                Duracion = 10,
            };

            listDtos.Add(Estimacion1);

            var Estimacion2 = new EstimacionRequestDto
            {
                Duracion = 25,
            };

            listDtos.Add(Estimacion2);

            var Estimacions = mapper.Map<List<Estimacion>>(listDtos);

            Assert.Equal(listDtos[0].Duracion, Estimacions[0].Duracion);

            Assert.Equal(listDtos[1].Duracion, Estimacions[1].Duracion);
        }
    }
}
