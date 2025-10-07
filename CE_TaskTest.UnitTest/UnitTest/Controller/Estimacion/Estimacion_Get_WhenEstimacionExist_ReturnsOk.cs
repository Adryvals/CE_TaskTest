using AutoMapper;
using CE_TaskTest.BackEnd.Context;
using CE_TaskTest.BackEnd.Controllers;
using CE_TaskTest.BackEnd.Models;
using CE_TaskTest.BackEnd.Services.Dtos.Response;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Moq.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CE_TaskTest.TestSector.UnitTest.Controller.Estimacion
{
    public class Estimacion_Get_WhenEstimacionExist_ReturnsOk
    {
        [Fact]
        public async Task Get_WhenEstimacionsExist_ReturnsOk()
        {
            // Arrange
            var estimacionMock = new List<BackEnd.Models.Estimacion>
            {            
                new BackEnd.Models.Estimacion { Id = 1, Duracion = 5, },
                new BackEnd.Models.Estimacion { Id = 2, Duracion = 10, },
            };

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(m => m.Map<List<EstimacionResponseDto>>(estimacionMock))
            .Returns(new List<EstimacionResponseDto>() {
                new EstimacionResponseDto { Id = 1, Duracion = 5, },
                new EstimacionResponseDto { Id = 2, Duracion = 10, },
            });

            var contextMock = new Mock<ApplicationDbContext>();
            contextMock.Setup(c => c.Estimaciones).ReturnsDbSet(estimacionMock);

            var controller = new EstimacionController(contextMock.Object, mapperMock.Object);

            // Act
            var result = await controller.Get();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var list = Assert.IsType<List<EstimacionResponseDto>>(okResult.Value);
            Assert.Equal(2, list.Count);
        }
    }
}
