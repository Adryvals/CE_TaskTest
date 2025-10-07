using AutoMapper;
using CE_TaskTest.BackEnd.Context;
using CE_TaskTest.BackEnd.Controllers;
using CE_TaskTest.BackEnd.Services.Dtos.Request;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CE_TaskTest.TestSector.UnitTest.Controller.Estimacion
{
    public class Estimacion_Put_ReturnsNoContent_WhenEstimacionUpdated
    {
        [Fact]
        public async Task Put_ReturnsNoContent_WhenEstimacionUpdated_AndAnyCheckPasses()
        {
            // Arrange
            var estimaciones = new List<BackEnd.Models.Estimacion>
            {
                new BackEnd.Models.Estimacion { Id = 1, Duracion = 5 },
                new BackEnd.Models.Estimacion { Id = 2, Duracion = 10 }
            };

            var dbSetMock = new Mock<DbSet<BackEnd.Models.Estimacion>>();

            // Mock de FindAsync
            dbSetMock.Setup(m => m.FindAsync(It.IsAny<object[]>()))
                     .ReturnsAsync((object[] ids) => estimaciones.FirstOrDefault(t => t.Id == (int)ids[0]));

            // Mock de Any() para validación de duplicado
            dbSetMock.As<IQueryable<BackEnd.Models.Estimacion>>().Setup(m => m.Provider).Returns(estimaciones.AsQueryable().Provider);
            dbSetMock.As<IQueryable<BackEnd.Models.Estimacion>>().Setup(m => m.Expression).Returns(estimaciones.AsQueryable().Expression);
            dbSetMock.As<IQueryable<BackEnd.Models.Estimacion>>().Setup(m => m.ElementType).Returns(estimaciones.AsQueryable().ElementType);
            dbSetMock.As<IQueryable<BackEnd.Models.Estimacion>>().Setup(m => m.GetEnumerator()).Returns(estimaciones.AsQueryable().GetEnumerator());

            // Mock del contexto
            var mockContext = new Mock<ApplicationDbContext>();
            mockContext.Setup(c => c.Estimaciones).Returns(dbSetMock.Object);
            mockContext.Setup(c => c.SaveChangesAsync(default)).ReturnsAsync(1);

            var mockMapper = new Mock<IMapper>();
            var updateDto = new EstimacionRequestDto
            {
                Duracion = 30,
            };

            mockMapper.Setup(m => m.Map(updateDto, It.IsAny<BackEnd.Models.Estimacion>()))
                      .Callback<EstimacionRequestDto, BackEnd.Models.Estimacion>((src, dest) =>
                      {
                          dest.Duracion = src.Duracion;
                      });

            var mockValidator = new Mock<IValidator<EstimacionRequestDto>>();
            mockValidator.Setup(v => v.ValidateAsync(updateDto, default)).ReturnsAsync(
                new FluentValidation.Results.ValidationResult()
            );

            var controller = new EstimacionController(mockContext.Object, mockMapper.Object, mockValidator.Object);

            // Act
            var result = await controller.Put(1, updateDto);

            // Assert
            var noContentResult = Assert.IsType<NoContentResult>(result);
            Assert.Equal(30, estimaciones.First().Duracion);
        }
    }
}
