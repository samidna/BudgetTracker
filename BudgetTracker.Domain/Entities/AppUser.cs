using Microsoft.AspNetCore.Identity;

namespace BudgetTracker.Domain.Entities;
public class AppUser : IdentityUser
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public decimal TotalBalance { get; set; }
    public ICollection<Transaction> Transactions { get; set; }
}
