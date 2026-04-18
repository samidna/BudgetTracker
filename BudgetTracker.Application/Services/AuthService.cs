using AutoMapper;
using BudgetTracker.Application.DTOs.User;
using BudgetTracker.Application.Interfaces;
using BudgetTracker.Domain.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BudgetTracker.Application.Services;
public class AuthService : IAuthService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IConfiguration _configuration;
    private readonly IValidator<RegisterDto> _registerValidator;
    private readonly IValidator<LoginDto> _loginValidator;
    private readonly IMapper _mapper;

    public AuthService(UserManager<AppUser> userManager, IConfiguration configuration, IValidator<RegisterDto> registerValidator, IValidator<LoginDto> loginValidator, IMapper mapper)
    {
        _userManager = userManager;
        _configuration = configuration;
        _registerValidator = registerValidator;
        _loginValidator = loginValidator;
        _mapper = mapper;
    }

    public async Task<AuthResponseDto> RegisterAsync(RegisterDto dto)
    {
        var validationResult = await _registerValidator.ValidateAsync(dto);
        if (!validationResult.IsValid)
        {
            var errors = string.Join(" ", validationResult.Errors.Select(e => e.ErrorMessage));
            return new AuthResponseDto(false, errors);
        }

        var userExists = await _userManager.FindByEmailAsync(dto.Email);
        if (userExists != null)
            return new AuthResponseDto(false, "User with this email already exists.");

        var user = _mapper.Map<AppUser>(dto);

        var result = await _userManager.CreateAsync(user, dto.Password);
        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            return new AuthResponseDto(false, $"Registration failed: {errors}");
        }

        return new AuthResponseDto(true, "User registered successfully.");
    }

    public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
    {
        var user = await _userManager.FindByEmailAsync(dto.Email);
        if (user == null || !await _userManager.CheckPasswordAsync(user, dto.Password))
            return new AuthResponseDto(false, "Invalid email or password.");

        var token = GenerateJwtToken(user);
        return new AuthResponseDto(true, "Login successful.", token);
    }

    public string GenerateJwtToken(AppUser user)
    {
        var authClaims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));

        var token = new JwtSecurityToken(
         issuer: _configuration["Jwt:Issuer"],
         audience: _configuration["Jwt:Audience"],
         expires: DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["Jwt:DurationInMinutes"])),
         claims: authClaims,
         signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
