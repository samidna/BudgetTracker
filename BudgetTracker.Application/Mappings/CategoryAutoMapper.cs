using AutoMapper;
using BudgetTracker.Application.DTOs.Category;
using BudgetTracker.Domain.Entities;

namespace BudgetTracker.Application.Mappings;
public class CategoryAutoMapper : Profile
{
    public CategoryAutoMapper()
    {
        CreateMap<Category, CategoryDto>();
        CreateMap<CreateCategoryDto, Category>();
    }
}
