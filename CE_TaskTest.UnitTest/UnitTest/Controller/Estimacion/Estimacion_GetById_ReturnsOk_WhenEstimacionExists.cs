using AutoMapper;
using CE_TaskTest.BackEnd.Context;
using CE_TaskTest.BackEnd.Controllers;
using CE_TaskTest.BackEnd.Models;
using CE_TaskTest.BackEnd.Services.Dtos.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CE_TaskTest.TestSector.UnitTest.Controller.Estimacion
{
    public class Estimacion_GetById_ReturnsOk_WhenEstimacionExists
    {
        [Fact]
        public async Task GetById_ReturnsOk_WhenEstimacionExists()
        {
            // Arrange
            var estimacionMock = new List<BackEnd.Models.Estimacion>
            {
                new BackEnd.Models.Estimacion { Id = 1, Duracion = 5, },
                new BackEnd.Models.Estimacion { Id = 2, Duracion = 10, },
            };

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(m => m.Map<EstimacionResponseDto>(It.IsAny<BackEnd.Models.Estimacion>()))
                      .Returns((BackEnd.Models.Estimacion Estimacion) => new EstimacionResponseDto
                      {
                          Id = Estimacion.Id,
                          Duracion = Estimacion.Duracion,
                      });


            var dbSetMock = new Mock<DbSet<BackEnd.Models.Estimacion>>();

            dbSetMock.Setup(m => m.FindAsync(It.IsAny<object[]>()))
                     .ReturnsAsync((object[] ids) => estimacionMock.FirstOrDefault(t => t.Id == (int)ids[0]));

            var contextMock = new Mock<ApplicationDbContext>();
            contextMock.Setup(c => c.Estimaciones).Returns(dbSetMock.Object);

            var controller = new EstimacionController(contextMock.Object, mapperMock.Object);

            // Act
            var result = await controller.GetById(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var dto = Assert.IsType<EstimacionResponseDto>(okResult.Value);
            Assert.Equal(1, dto.Id);
        }
    }
}
