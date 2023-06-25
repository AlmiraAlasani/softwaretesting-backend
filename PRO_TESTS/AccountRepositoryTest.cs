using Microsoft.EntityFrameworkCore;
using PRO.Data;
using PRO.Models;
using PRO.Repositories.AccountRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRO_TESTS
{
    public class AccountRepositoryTest
    {
        private readonly DbContextOptions<AppDbContext> _dbContextOptions;

        public AccountRepositoryTest()
        {
            _dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
        }

        [Fact]
        public async Task GetAccountByIdAsync_ReturnExpectedAccount()
        {
            using (var context = new AppDbContext(_dbContextOptions))
            {
                int accountId = 1;
                var expectedAccount = new Account
                {
                    Id = 1,
                    Balance = 11111,
                    Email = "aa@gmail.com"

                };
                context.Accounts.Add(expectedAccount);
                await context.SaveChangesAsync();

                var repository = new AccountRepository(context);

                var actualAccount = await repository.GetAccountByIdAsync(accountId);

                Assert.NotNull(actualAccount);
                Assert.Equal(expectedAccount.Id, actualAccount.Id);
                Assert.Equal(expectedAccount.Balance, actualAccount.Balance);
                Assert.Equal(expectedAccount.Email, actualAccount.Email);

            }
        }

        [Fact]
        public async Task GetAccountByIdAsync_ReturnNull()
        {
            using (var context = new AppDbContext(_dbContextOptions))
            {
                int accountId = 1;
                var repository = new AccountRepository(context);
                var actualAccount = await repository.GetAccountByIdAsync(accountId);
                Assert.Null(actualAccount);
            }
        }


        [Fact]
        public async Task AddAccountAsync_ReturnExpectedAccount()
        {
            using (var context = new AppDbContext(_dbContextOptions))
            {
                int accountId = 1;
                var expectedAccount = new Account
                {
                    Id = 1,
                    Balance = 11111,
                    Email = "aa@gmail.com"

                };


                var repository = new AccountRepository(context);
                repository.AddAccountAsync(expectedAccount);
                var actualAccount = await repository.GetAccountByIdAsync(accountId);

                Assert.NotNull(actualAccount);
                Assert.Equal(expectedAccount.Id, actualAccount.Id);
                Assert.Equal(expectedAccount.Balance, actualAccount.Balance);
                Assert.Equal(expectedAccount.Email, actualAccount.Email);
            }
        }

        [Fact]
        public async Task UpdateAccountAsync_ReturnExpectedAccount()
        {
            using (var context = new AppDbContext(_dbContextOptions))
            {
                int accountId = 1;
                var account = new Account
                {
                    Id = 1,
                    Balance = 11111,
                    Email = "aa@gmail.com"
                };

                var repository = new AccountRepository(context);
                await repository.AddAccountAsync(account);
                var actualAccount = await repository.GetAccountByIdAsync(accountId);

                Assert.Equal(account.Id, actualAccount.Id);
                Assert.Equal(account.Balance, actualAccount.Balance);
                Assert.Equal(account.Email, actualAccount.Email);

                repository.UpdateAccount(actualAccount);
                var updatedAccount = await repository.GetAccountByIdAsync(accountId);


                Assert.Equal(actualAccount.Id, updatedAccount.Id);
                Assert.Equal(actualAccount.Balance, actualAccount.Balance);
                Assert.Equal(actualAccount.Email, actualAccount.Email);
            }
        }

        [Fact]
        public async Task Delete_ReturnExpectedAccount()
        {
            using (var context = new AppDbContext(_dbContextOptions))
            {
                int accountId = 1;
                var account = new Account
                {
                    Id = accountId,
                    Balance = 11111,
                    Email = "aa@gmail.com"

                };

                var repository = new AccountRepository(context);
                await repository.AddAccountAsync(account);
                var actualAccount = await repository.GetAccountByIdAsync(accountId);

                Assert.Equal(account.Id, actualAccount.Id);
                Assert.Equal(account.Balance, actualAccount.Balance);
                Assert.Equal(account.Email, actualAccount.Email);

                repository.RemoveAccount(actualAccount);
                var deletedAccount = await repository.GetAccountByIdAsync(accountId);

                Assert.Null(deletedAccount);
            }
        }
    }
}
