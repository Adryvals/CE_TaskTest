using AutoMapper;
using CE_TaskTest.BackEnd.Context;
using CE_TaskTest.BackEnd.Controllers;
using CE_TaskTest.BackEnd.Services.Dtos.Request;
using FluentValidation;
using FluentValidation.Results;
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
    public class Estimacion_Post_ReturnsCreated_WhenEstimacionValid
    {
        [Fact]
        public async Task Post_ReturnsCreated_WhenEstimacionValid()
        {
            var mockContext = new Mock<ApplicationDbContext>();
            var mockMapper = new Mock<IMapper>();
            var mockValidator = new Mock<IValidator<EstimacionRequestDto>>();

            var estimacionMock = new List<BackEnd.Models.Estimacion>
            {
                new BackEnd.Models.Estimacion { Id = 1, Duracion = 5, },
                new BackEnd.Models.Estimacion { Id = 2, Duracion = 10, },
            };

            // Arrange
            var newEstimacionDto = new EstimacionRequestDto { Duracion = 25 };
            mockValidator.Setup(v => v.ValidateAsync(newEstimacionDto, default)).ReturnsAsync(new FluentValidation.Results.ValidationResult());
            mockMapper.Setup(m => m.Map<BackEnd.Models.Estimacion>(newEstimacionDto)).Returns(new BackEnd.Models.Estimacion { Id = 3, Duracion = newEstimacionDto.Duracion });
            mockContext.Setup(c => c.Estimaciones).ReturnsDbSet(estimacionMock);

            mockValidator.Setup(v => v.ValidateAsync(newEstimacionDto, default)).ReturnsAsync(
                new ValidationResult()
            );

            var controller = new EstimacionController(mockContext.Object, mockMapper.Object, mockValidator.Object);

            // Act
            var result = await controller.Post(newEstimacionDto);

            // Assert
            var created = Assert.IsType<CreatedAtActionResult>(result);
            var Estimacion = Assert.IsType<BackEnd.Models.Estimacion>(created.Value);
            Assert.Equal(25, Estimacion.Duracion);
        }
    }
}
