using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PRO.Controllers;
using PRO.DTOs;
using PRO.Services.AccountService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRO_TESTS
{
    public class AccountControllerTest
    {
        private readonly Mock<IAccountService> _mockAccountService;
        private readonly Mock<IMapper> _mapper;

        public AccountControllerTest()
        {
            _mockAccountService = new Mock<IAccountService>();
            _mapper = new Mock<IMapper>();
            _mapper.Setup(m => m.Map<AccountDTO>(It.IsAny<AccountDTO>())).Returns((AccountDTO source) => new AccountDTO
            {
                Id = source.Id,
                Balance = source.Balance,
                Email = source.Email
  
            });
        }

        [Fact]
        public async Task GetAccountByIdAsync_ReturnsExpectedAccount()
        {
            var mockAccountService = new Mock<IAccountService>();
            var expectedAccount = new AccountDTO
            {
                Id = 1,
                Balance = 1111,
                Email = "aa@gmail.com"
            };
            mockAccountService.Setup(x => x.GetAccountByIdAsync(1))
                .ReturnsAsync(expectedAccount);
            var controller = new AccountController(mockAccountService.Object, _mapper.Object);

            var result = await controller.GetAccountByIdAsync(1);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var actualAccount = Assert.IsType<AccountDTO>(okResult.Value);
            Assert.Equal(expectedAccount.Id, actualAccount.Id);
            Assert.Equal(expectedAccount.Balance, actualAccount.Balance);
            Assert.Equal(expectedAccount.Email, actualAccount.Email);


        }

        [Fact]
        public async Task GetAccountByIdAsync_ReturnsNotFoundResult()
        {
            int nonExpectedAccountId = 99;
            AccountDTO nullAccount = null;

            _mockAccountService.Setup(x => x.GetAccountByIdAsync(nonExpectedAccountId))
                .ReturnsAsync(nullAccount);
            var controller = new AccountController(_mockAccountService.Object, _mapper.Object);

            var result = await controller.GetAccountByIdAsync(nonExpectedAccountId);

            Assert.IsType<NotFoundResult>(result.Result);
        }


        [Fact]
        public async Task CreateNewAccount_ReturnsExpectedAccount()
        {
            var mockAccountService = new Mock<IAccountService>();
            var expectedAccount = new AccountDTO
            {
                Id = 1,
                Balance = 1111,
                Email = "aa@gmail.com"

            };
            mockAccountService.Setup(x => x.CreateAccountAsync(expectedAccount))
                .ReturnsAsync(expectedAccount);
            var controller = new AccountController(mockAccountService.Object, _mapper.Object);

            var result = await controller.CreateAccountAsync(expectedAccount);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var actualAccount = Assert.IsType<AccountDTO>(okResult.Value);
            Assert.Equal(expectedAccount.Id, actualAccount.Id);
            Assert.Equal(expectedAccount.Balance, actualAccount.Balance);
            Assert.Equal(expectedAccount.Email, actualAccount.Email);
            
        }


        [Fact]
        public async Task Update_ReturnsNoContentFound()
        {
            var mockAccountService = new Mock<IAccountService>();
            var expectedAccount = new AccountDTO
            {
                Id = 1,
                Balance = 1111,
                Email = "aa@gmail.com"
            };
            mockAccountService.Setup(x => x.UpdateAccountAsync(1, expectedAccount));
            var controller = new AccountController(mockAccountService.Object, _mapper.Object);

            var result = await controller.UpdateAccountAsync(1, expectedAccount);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Update_ReturnsBadRequest()
        {
            var mockAccountService = new Mock<IAccountService>();
            var expectedAccount = new AccountDTO
            {
                Id = 1,
                Balance = 1111,
                Email = "aa@gmail.com"
            };
            mockAccountService.Setup(x => x.UpdateAccountAsync(1, expectedAccount));
            var controller = new AccountController(mockAccountService.Object, _mapper.Object);

            var result = await controller.UpdateAccountAsync(10, expectedAccount);

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsBadRequest()
        {
            var mockAccountService = new Mock<IAccountService>();
            mockAccountService.Setup(x => x.DeleteAccountAsync(9999));
            var controller = new AccountController(mockAccountService.Object, _mapper.Object);

            var result = await controller.DeleteAccountAsync(9999);

            Assert.IsType<NotFoundResult>(result);
        }
    }
}
