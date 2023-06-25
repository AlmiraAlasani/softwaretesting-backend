using Microsoft.EntityFrameworkCore;
using PRO.Data;
using PRO.Models;
using PRO.Repositories.ExpenseRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRO_TESTS
{
    public class ExpenseRepositroyTest
    {
        private readonly DbContextOptions<AppDbContext> _dbContextOptions;

        public ExpenseRepositroyTest()
        {
            _dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
        }

        [Fact]
        public async Task GetExpenseByIdAsync_ReturnExpectedExpense()
        {
            using (var context = new AppDbContext(_dbContextOptions))
            {
                int expenseId = 1;
                var expectedExpense = new Expense
                {
                    Id = 1,
                    Description = "Test",
                    Amount = 1,

                };
                context.Expenses.Add(expectedExpense);
                await context.SaveChangesAsync();

                var repository = new ExpenseRepository(context);

                var actualExpense = await repository.GetExpenseByIdAsync(expenseId);

                Assert.NotNull(actualExpense);
                Assert.Equal(expectedExpense.Id, actualExpense.Id);
                Assert.Equal(expectedExpense.Description, actualExpense.Description);
                Assert.Equal(expectedExpense.Amount, actualExpense.Amount);
            }
        }

        [Fact]
        public async Task GetExpenseByIdAsync_ReturnNull()
        {
            using (var context = new AppDbContext(_dbContextOptions))
            {
                int expenseId = 1;
                var repository = new ExpenseRepository(context);
                var actualExpense = await repository.GetExpenseByIdAsync(expenseId);
                Assert.Null(actualExpense);
            }
        }


        [Fact]
        public async Task AddExpenseAsync_ReturnExpectedExpense()
        {
            using (var context = new AppDbContext(_dbContextOptions))
            {
                int expenseId = 1;
                var expectedExpense = new Expense
                {
                    Id = 1,
                    Description = "Test",
                    Amount = 1,

                };


                var repository = new ExpenseRepository(context);
                repository.AddExpenseAsync(expectedExpense);
                var actualExpense = await repository.GetExpenseByIdAsync(expenseId);

                Assert.NotNull(actualExpense);
                Assert.Equal(expectedExpense.Id, actualExpense.Id);
                Assert.Equal(expectedExpense.Description, actualExpense.Description);
                Assert.Equal(expectedExpense.Amount, actualExpense.Amount);
            }
        }

        [Fact]
        public async Task UpdateExpenseAsync_ReturnExpectedExpense()
        {
            using (var context = new AppDbContext(_dbContextOptions))
            {
                int expenseId = 1;
                var expense = new Expense
                {
                    Id = 1,
                    Description = "Test",
                    Amount = 1,
                };

                var repository = new ExpenseRepository(context);
                await repository.AddExpenseAsync(expense);
                var actualExpense = await repository.GetExpenseByIdAsync(expenseId);

                Assert.Equal(expense.Id, actualExpense.Id);
                Assert.Equal(expense.Description, actualExpense.Description);
                Assert.Equal(expense.Amount, actualExpense.Amount);

                repository.UpdateExpense(actualExpense);
                var updatedExpense = await repository.GetExpenseByIdAsync(expenseId);


                Assert.Equal(actualExpense.Id, updatedExpense.Id);
                Assert.Equal(actualExpense.Description, updatedExpense.Description);
                Assert.Equal(actualExpense.Amount, updatedExpense.Amount);
            }
        }

        [Fact]
        public async Task Delete_ReturnExpectedExpense()
        {
            using (var context = new AppDbContext(_dbContextOptions))
            {
                int expenseId = 1;
                var expense = new Expense
                {
                    Id = expenseId,
                    Description = "Test",
                    Amount = 1,

                };

                var repository = new ExpenseRepository(context);
                await repository.AddExpenseAsync(expense);
                var actualExpense = await repository.GetExpenseByIdAsync(expenseId);

                Assert.Equal(expense.Id, actualExpense.Id);
                Assert.Equal(expense.Description, actualExpense.Description);
                Assert.Equal(expense.Amount, actualExpense.Amount);

                repository.RemoveExpense(actualExpense);
                var deletedExpense = await repository.GetExpenseByIdAsync(expenseId);

                Assert.Null(deletedExpense);
            }
        }
    }
}
