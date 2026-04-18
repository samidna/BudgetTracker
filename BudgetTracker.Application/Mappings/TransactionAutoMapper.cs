using AutoMapper;
using BudgetTracker.Application.DTOs.Transaction;
using BudgetTracker.Domain.Entities;

namespace BudgetTracker.Application.Mappings;
public class TransactionAutoMapper : Profile
{
    public TransactionAutoMapper()
    {
        CreateMap<Transaction, TransactionDto>()
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
            .ForMember(dest => dest.TransactionType, opt => opt.MapFrom(src => src.Type.ToString()));

        CreateMap<CreateTransactionDto, Transaction>();
    }
}
