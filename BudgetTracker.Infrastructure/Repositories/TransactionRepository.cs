using BudgetTracker.Application.Interfaces;
using BudgetTracker.Domain.Entities;
using BudgetTracker.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BudgetTracker.Infrastructure.Repositories;
public class TransactionRepository : GenericRepository<Transaction>, ITransactionRepository
{
    private readonly AppDbContext _context;
    public TransactionRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Transaction>> GetRecentTransactionsAsync(string userId, int count)
    {
        return await _context.Transactions
            .Where(t => t.AppUserId == userId)
            .OrderByDescending(t => t.CreatedAt)
            .Take(count)
            .Include(t => t.Category) 
            .ToListAsync();
    }
}
