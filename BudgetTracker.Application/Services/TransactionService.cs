using AutoMapper;
using BudgetTracker.Application.DTOs.Transaction;
using BudgetTracker.Application.Interfaces;
using BudgetTracker.Domain.Entities;
using FluentValidation;

namespace BudgetTracker.Application.Services;
public class TransactionService : ITransactionService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IValidator<CreateTransactionDto> _validator;

    public TransactionService(IUnitOfWork unitOfWork, IMapper mapper, IValidator<CreateTransactionDto> validator)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<bool> CreateTransactionAsync(CreateTransactionDto dto, string userId)
    {
        var validationResult = await _validator.ValidateAsync(dto);
        if (!validationResult.IsValid)
        {
            var errorMessages = string.Join(" ", validationResult.Errors.Select(e => e.ErrorMessage));
            throw new ValidationException(errorMessages);
        }

        var category = await _unitOfWork.Categories.GetByIdAsync(dto.CategoryId);
        if (category == null)
        {
            throw new KeyNotFoundException($"Category not found.");
        }

        var transaction = _mapper.Map<Transaction>(dto);
        transaction.AppUserId = userId;

        await _unitOfWork.Transactions.AddAsync(transaction);
        var result = await _unitOfWork.CompleteAsync();

        if (result <= 0)
        {
            throw new Exception("An error occurred while saving the transaction to the database.");
        }

        return true;
    }

    public async Task<IEnumerable<TransactionDto>> GetAllTransactionsAsync(string userId)
    {
        var transactions = await _unitOfWork.Transactions.GetRecentTransactionsAsync(userId, 100);

        return _mapper.Map<IEnumerable<TransactionDto>>(transactions);
    }
}
