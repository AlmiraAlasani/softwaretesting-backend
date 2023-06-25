using Microsoft.EntityFrameworkCore;
using PRO.Data;
using PRO.Models;
using PRO.Repositories.AccountRepository;
using PRO.Repositories.IncomeRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRO_TESTS
{
    public class IncomeRepositoryTest

    {
        private readonly DbContextOptions<AppDbContext> _dbContextOptions;

        public IncomeRepositoryTest()
        {
            _dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
        }

        [Fact]
        public async Task GetIncomeByIdAsync_ReturnExpectedIncome()
        {
            using (var context = new AppDbContext(_dbContextOptions))
            {
                int incomeId = 1;
                var expectedIncome = new Income
                {
                    Id = 1,
                    Description = "Test",
                    Amount = 1,

                };
                context.Incomes.Add(expectedIncome);
                await context.SaveChangesAsync();

                var repository = new IncomeRepository(context);

                var actualIncome = await repository.GetIncomeByIdAsync(incomeId);

                Assert.NotNull(actualIncome);
                Assert.Equal(expectedIncome.Id, actualIncome.Id);
                Assert.Equal(expectedIncome.Description, actualIncome.Description);
                Assert.Equal(expectedIncome.Amount, actualIncome.Amount);
            }
        }

        [Fact]
        public async Task GetIncomeByIdAsync_ReturnNull()
        {
            using (var context = new AppDbContext(_dbContextOptions))
            {
                int incomeId = 1;
                var repository = new IncomeRepository(context);
                var actualIncome = await repository.GetIncomeByIdAsync(incomeId);
                Assert.Null(actualIncome);
            }
        }


        [Fact]
        public async Task AddIncomeAsync_ReturnExpectedIncome()
        {
            using (var context = new AppDbContext(_dbContextOptions))
            {
                int incomeId = 1;
                var expectedIncome = new Income
                {
                    Id = 1,
                    Description = "Test",
                    Amount = 1,

                };


                var repository = new IncomeRepository(context);
                repository.AddIncomeAsync(expectedIncome);
                var actualIncome = await repository.GetIncomeByIdAsync(incomeId);

                Assert.NotNull(actualIncome);
                Assert.Equal(expectedIncome.Id, actualIncome.Id);
                Assert.Equal(expectedIncome.Description, actualIncome.Description);
                Assert.Equal(expectedIncome.Amount, actualIncome.Amount);
            }
        }

        [Fact]
        public async Task UpdateIncomeAsync_ReturnExpectedIncome()
        {
            using (var context = new AppDbContext(_dbContextOptions))
            {
                int incomeId = 1;
                var income = new Income
                {
                    Id = 1,
                    Description = "Test",
                    Amount = 1,
                };

                var repository = new IncomeRepository(context);
                await repository.AddIncomeAsync(income);
                var actualIncome = await repository.GetIncomeByIdAsync(incomeId);

                Assert.Equal(income.Id, actualIncome.Id);
                Assert.Equal(income.Description, actualIncome.Description);
                Assert.Equal(income.Amount, actualIncome.Amount);

                repository.UpdateIncome(actualIncome);
                var updatedIncome = await repository.GetIncomeByIdAsync(incomeId);


                Assert.Equal(actualIncome.Id, updatedIncome.Id);
                Assert.Equal(actualIncome.Description, updatedIncome.Description);
                Assert.Equal(actualIncome.Amount, updatedIncome.Amount);
            }
        }

        [Fact]
        public async Task Delete_ReturnExpectedIncome()
        {
            using (var context = new AppDbContext(_dbContextOptions))
            {
                int incomeId = 1;
                var income = new Income
                {
                    Id = incomeId,
                    Description = "Test",
                    Amount = 1,

                };

                var repository = new IncomeRepository(context);
                await repository.AddIncomeAsync(income);
                var actualIncome = await repository.GetIncomeByIdAsync(incomeId);

                Assert.Equal(income.Id, actualIncome.Id);
                Assert.Equal(income.Description, actualIncome.Description);
                Assert.Equal(income.Amount, actualIncome.Amount);

                repository.RemoveIncome(actualIncome);
                var deletedIncome = await repository.GetIncomeByIdAsync(incomeId);

                Assert.Null(deletedIncome);
            }
        }
    }
}
