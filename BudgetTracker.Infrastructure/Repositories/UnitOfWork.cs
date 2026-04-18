using BudgetTracker.Application.Interfaces;
using BudgetTracker.Domain.Entities;
using BudgetTracker.Infrastructure.Data;

namespace BudgetTracker.Infrastructure.Repositories;
public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private ITransactionRepository _transactions;
    private IGenericRepository<Category> _categories;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    public ITransactionRepository Transactions => _transactions ??= new TransactionRepository(_context);
    public IGenericRepository<Category> Categories => _categories ??= new GenericRepository<Category>(_context);

    public async Task<int> CompleteAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
