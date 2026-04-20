using BudgetTracker.Application.DTOs.AiAdvisor;
using BudgetTracker.Application.Interfaces;
using BudgetTracker.Domain.Entities;
using BudgetTracker.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace BudgetTracker.Infrastructure.Services;

public class AIAdvisorService : IAiAdvisorService
{
    private readonly IGenericRepository<Transaction> _transactionRepository;
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;
    private readonly string _geminiUrl;

    public AIAdvisorService(
        IGenericRepository<Transaction> transactionRepository,
        HttpClient httpClient,
        IConfiguration config)
    {
        _transactionRepository = transactionRepository;
        _httpClient = httpClient;
        _apiKey = config["Gemini:ApiKey"]!;
        _geminiUrl = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.5-flash:generateContent?key={_apiKey}";
    }

    public async Task<string> GetAdviceAsync(string userId, AIAdviceRequestDto request)
    {
        var transactions = await _transactionRepository
            .Find(t => t.AppUserId == userId
                     && t.CreatedAt.Month == request.Month
                     && t.CreatedAt.Year == request.Year)
            .Include(t => t.Category)
            .ToListAsync();

        var totalIncome = transactions
            .Where(t => t.Type == TransactionType.Income)
            .Sum(t => t.Amount);

        var totalExpense = transactions
            .Where(t => t.Type == TransactionType.Expense)
            .Sum(t => t.Amount);

        var categorySummaries = transactions
            .Where(t => t.Type == TransactionType.Expense)
            .GroupBy(t => t.Category)
            .Select(g => new CategorySummaryDto
            {
                CategoryName = g.Key.Name,
                Icon = g.Key.Icon,
                TotalAmount = g.Sum(t => t.Amount),
                TransactionCount = g.Count()
            })
            .OrderByDescending(c => c.TotalAmount)
            .ToList();

        return await GetAIAdviceAsync(totalIncome, totalExpense, categorySummaries, request.Month, request.Year);
    }

    private async Task<string> GetAIAdviceAsync(
        decimal income,
        decimal expense,
        List<CategorySummaryDto> categories,
        int month,
        int year)
    {
        var categoryLines = categories
            .Select(c => $"- {c.Icon} {c.CategoryName}: {c.TotalAmount:F2} AZN ({c.TransactionCount} əməliyyat)");

        var prompt = $"""
            Sən peşəkar maliyyə məsləhətçisisən. Aşağıdakı büdcə məlumatlarını analiz et və Azərbaycan dilində qısa, praktik tövsiyələr ver.

            📅 Dövr: {year}-ci il, {month}-ci ay
            💰 Gəlir: {income:F2} AZN
            💸 Xərc: {expense:F2} AZN
            📊 Balans: {income - expense:F2} AZN

            Kateqoriyalar üzrə xərclər:
            {string.Join("\n", categoryLines)}

            Zəhmət olmasa aşağıdakıları analiz et:
            1. Ümumi maliyyə vəziyyətinin qısa qiymətləndirilməsi
            2. Ən çox xərc olunan 2-3 kateqoriya haqqında konkret tövsiyə
            3. Növbəki ay üçün 2-3 praktik qənaət məsləhəti
            """;

        var requestBody = new
        {
            contents = new[]
            {
                new
                {
                    parts = new[]
                    {
                        new { text = prompt }
                    }
                }
            }
        };

        var response = await _httpClient.PostAsJsonAsync(_geminiUrl, requestBody);
        response.EnsureSuccessStatusCode();

        var result = await response.Content.ReadFromJsonAsync<GeminiResponse>();
        return result?.Candidates?.FirstOrDefault()
                   ?.Content?.Parts?.FirstOrDefault()
                   ?.Text ?? "Məsləhət alınmadı.";
    }
}

internal class GeminiResponse
{
    [JsonPropertyName("candidates")]
    public List<GeminiCandidate> Candidates { get; set; } = new();
}

internal class GeminiCandidate
{
    [JsonPropertyName("content")]
    public GeminiContent Content { get; set; } = new();
}

internal class GeminiContent
{
    [JsonPropertyName("parts")]
    public List<GeminiPart> Parts { get; set; } = new();
}

internal class GeminiPart
{
    [JsonPropertyName("text")]
    public string Text { get; set; } = string.Empty;
}