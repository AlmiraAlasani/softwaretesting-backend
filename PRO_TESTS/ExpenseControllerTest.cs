using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PRO.Controllers;
using PRO.DTOs;
using PRO.Services.ExpenseService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRO_TESTS
{
    public class ExpenseControllerTest
    {
        private readonly Mock<IExpenseService> _mockExpenseService;
        private readonly Mock<IMapper> _mapper;

        public ExpenseControllerTest()
        {
            _mockExpenseService = new Mock<IExpenseService>();
            _mapper = new Mock<IMapper>();
            _mapper.Setup(m => m.Map<ExpenseDTO>(It.IsAny<ExpenseDTO>())).Returns((ExpenseDTO source) => new ExpenseDTO
            {
                Id = source.Id,
                Description = source.Description,
                Amount = source.Amount

            });
        }

        [Fact]
        public async Task GetExpenseByIdAsync_ReturnsExpectedExpense()
        {
            var mockExpenseService = new Mock<IExpenseService>();
            var expectedExpense = new ExpenseDTO
            {
                Id = 1,
                Description = "Test",
                Amount = 1,
            };
            mockExpenseService.Setup(x => x.GetExpenseByIdAsync(1))
                .ReturnsAsync(expectedExpense);
            var controller = new ExpenseController(mockExpenseService.Object, _mapper.Object);

            var result = await controller.GetExpenseByIdAsync(1);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var actualExpense = Assert.IsType<ExpenseDTO>(okResult.Value);
            Assert.Equal(expectedExpense.Id, actualExpense.Id);
            Assert.Equal(expectedExpense.Description, actualExpense.Description);
            Assert.Equal(expectedExpense.Amount, actualExpense.Amount);


        }

        [Fact]
        public async Task GetExpenseByIdAsync_ReturnsNotFoundResult()
        {
            int nonExpectedExpenseId = 99;
            ExpenseDTO nullExpense = null;

            _mockExpenseService.Setup(x => x.GetExpenseByIdAsync(nonExpectedExpenseId))
                .ReturnsAsync(nullExpense);
            var controller = new ExpenseController(_mockExpenseService.Object, _mapper.Object);

            var result = await controller.GetExpenseByIdAsync(nonExpectedExpenseId);

            Assert.IsType<NotFoundResult>(result.Result);
        }


        [Fact]
        public async Task CreateNewExpense_ReturnsExpectedExpense()
        {
            var mockExpenseService = new Mock<IExpenseService>();
            var expectedExpense = new ExpenseDTO
            {
                Id = 1,
                Description = "Test",
                Amount = 1,

            };
            mockExpenseService.Setup(x => x.CreateExpenseAsync(expectedExpense))
                .ReturnsAsync(expectedExpense);
            var controller = new ExpenseController(mockExpenseService.Object, _mapper.Object);

            var result = await controller.CreateExpenseAsync(expectedExpense);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var actualExpense = Assert.IsType<ExpenseDTO>(okResult.Value);
            Assert.Equal(expectedExpense.Id, actualExpense.Id);
            Assert.Equal(expectedExpense.Description, actualExpense.Description);
            Assert.Equal(expectedExpense.Amount, actualExpense.Amount);

        }


        [Fact]
        public async Task Update_ReturnsNoContentFound()
        {
            var mockExpenseService = new Mock<IExpenseService>();
            var expectedExpense = new ExpenseDTO
            {
                Id = 1,
                Description = "Test",
                Amount = 1,
            };
            mockExpenseService.Setup(x => x.UpdateExpenseAsync(1, expectedExpense));
            var controller = new ExpenseController(mockExpenseService.Object, _mapper.Object);

            var result = await controller.UpdateExpenseAsync(1, expectedExpense);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Update_ReturnsBadRequest()
        {
            var mockExpenseService = new Mock<IExpenseService>();
            var expectedExpense = new ExpenseDTO
            {
                Id = 1,
                Description = "Test",
                Amount = 1,
            };
            mockExpenseService.Setup(x => x.UpdateExpenseAsync(1, expectedExpense));
            var controller = new ExpenseController(mockExpenseService.Object, _mapper.Object);

            var result = await controller.UpdateExpenseAsync(100, expectedExpense);

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsBadRequest()
        {
            var mockExpenseService = new Mock<IExpenseService>();
            mockExpenseService.Setup(x => x.DeleteExpenseAsync(9999));
            var controller = new ExpenseController(mockExpenseService.Object, _mapper.Object);

            var result = await controller.DeleteExpenseAsync(9999);

            Assert.IsType<NotFoundResult>(result);
        }
    }
}
