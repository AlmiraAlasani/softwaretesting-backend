using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PRO.Controllers;
using PRO.DTOs;
using PRO.Services.IncomeService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRO_TESTS
{
    public class IncomeControllerTest
    {
        private readonly Mock<IIncomeService> _mockIncomeService;
        private readonly Mock<IMapper> _mapper;

        public IncomeControllerTest()
        {
            _mockIncomeService = new Mock<IIncomeService>();
            _mapper = new Mock<IMapper>();
            _mapper.Setup(m => m.Map<IncomeDTO>(It.IsAny<IncomeDTO>())).Returns((IncomeDTO source) => new IncomeDTO
            {
                Id = source.Id,
                Description = source.Description,
                Amount = source.Amount

            });
        }

        [Fact]
        public async Task GetIncomeByIdAsync_ReturnsExpectedIncome()
        {
            var mockIncomeService = new Mock<IIncomeService>();
            var expectedIncome = new IncomeDTO
            {
                Id = 1,
                Description = "Test",
                Amount = 1,
            };
            mockIncomeService.Setup(x => x.GetIncomeByIdAsync(1))
                .ReturnsAsync(expectedIncome);
            var controller = new IncomeController(mockIncomeService.Object, _mapper.Object);

            var result = await controller.GetIncomeByIdAsync(1);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var actualIncome = Assert.IsType<IncomeDTO>(okResult.Value);
            Assert.Equal(expectedIncome.Id, actualIncome.Id);
            Assert.Equal(expectedIncome.Description, actualIncome.Description);
            Assert.Equal(expectedIncome.Amount, actualIncome.Amount);


        }

        [Fact]
        public async Task GetIncomeByIdAsync_ReturnsNotFoundResult()
        {
            int nonExpectedIncomeId = 99;
            IncomeDTO nullIncome = null;

            _mockIncomeService.Setup(x => x.GetIncomeByIdAsync(nonExpectedIncomeId))
                .ReturnsAsync(nullIncome);
            var controller = new IncomeController(_mockIncomeService.Object, _mapper.Object);

            var result = await controller.GetIncomeByIdAsync(nonExpectedIncomeId);

            Assert.IsType<NotFoundResult>(result.Result);
        }


        [Fact]
        public async Task CreateNewIncome_ReturnsExpectedIncome()
        {
            var mockIncomeService = new Mock<IIncomeService>();
            var expectedIncome = new IncomeDTO
            {
                Id = 1,
                Description = "Test",
                Amount = 1,

            };
            mockIncomeService.Setup(x => x.CreateIncomeAsync(expectedIncome))
                .ReturnsAsync(expectedIncome);
            var controller = new IncomeController(mockIncomeService.Object, _mapper.Object);

            var result = await controller.CreateIncomeAsync(expectedIncome);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var actualIncome = Assert.IsType<IncomeDTO>(okResult.Value);
            Assert.Equal(expectedIncome.Id, actualIncome.Id);
            Assert.Equal(expectedIncome.Description, actualIncome.Description);
            Assert.Equal(expectedIncome.Amount, actualIncome.Amount);

        }


        [Fact]
        public async Task Update_ReturnsNoContentFound()
        {
            var mockIncomeService = new Mock<IIncomeService>();
            var expectedIncome = new IncomeDTO
            {
                Id = 1,
                Description = "Test",
                Amount = 1,
            };
            mockIncomeService.Setup(x => x.UpdateIncomeAsync(1, expectedIncome));
            var controller = new IncomeController(mockIncomeService.Object, _mapper.Object);

            var result = await controller.UpdateIncomeAsync(1, expectedIncome);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Update_ReturnsBadRequest()
        {
            var mockIncomeService = new Mock<IIncomeService>();
            var expectedIncome = new IncomeDTO
            {
                Id = 1,
                Description = "Test",
                Amount = 1,
            };
            mockIncomeService.Setup(x => x.UpdateIncomeAsync(1, expectedIncome));
            var controller = new IncomeController(mockIncomeService.Object, _mapper.Object);

            var result = await controller.UpdateIncomeAsync(100, expectedIncome);

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsBadRequest()
        {
            var mockIncomeService = new Mock<IIncomeService>();
            mockIncomeService.Setup(x => x.DeleteIncomeAsync(9999));
            var controller = new IncomeController(mockIncomeService.Object, _mapper.Object);

            var result = await controller.DeleteIncomeAsync(9999);

            Assert.IsType<NotFoundResult>(result);
        }
    }
}
