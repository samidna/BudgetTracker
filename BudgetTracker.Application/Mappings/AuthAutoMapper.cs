using AutoMapper;
using BudgetTracker.Application.DTOs.Transaction;
using BudgetTracker.Application.DTOs.User;
using BudgetTracker.Domain.Entities;

namespace BudgetTracker.Application.Mappings;
public class AuthAutoMapper : Profile
{
    public AuthAutoMapper()
    {
        CreateMap<RegisterDto, AppUser>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Username))
            .ForSourceMember(src => src.ConfirmPassword, opt => opt.DoNotValidate());

        CreateMap<CreateTransactionDto, Transaction>();
        CreateMap<Transaction, TransactionDto>()
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
            .ForMember(dest => dest.TransactionType, opt => opt.MapFrom(src => src.Type.ToString()));
    }
}
