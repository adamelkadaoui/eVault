using System.Threading.Tasks;
using Xunit;
using eVaultAPI.Services;
using eVaultAPI.Models;
using eVaultAPI.Interfaces;
using eVaultAPI.Repositories;

namespace eVaultAPI.Tests.Services;

public class AuditServiceTests
{
    private readonly AuditService _auditService;

    public AuditServiceTests()
    {
        _auditService = new AuditService(new InMemoryAuditRepository());
    }

    [Fact]
    public async Task LogAsync_ShouldAddEntry()
    {
        string user = "testuser";
        AuditAction action = AuditAction.Created;
        string documentId = "doc123";
        string details = "Test details";

        await _auditService.LogAsync(user, action, documentId, details);
        var entries = await _auditService.GetAllAsync();

        Assert.Single(entries);
        var entry = entries[0];
        Assert.Equal(user, entry.User);
        Assert.Equal(action, entry.Action);
        Assert.Equal(documentId, entry.DocumentId);
        Assert.Equal(details, entry.Details);
        Assert.True(entry.Timestamp <= System.DateTime.UtcNow);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllEntries()
    {
        await _auditService.LogAsync("user1", AuditAction.Created, "doc1");
        await _auditService.LogAsync("user2", AuditAction.Viewed, "doc2");
        var entries = await _auditService.GetAllAsync();

        Assert.Equal(2, entries.Count);
        Assert.Contains(entries, e => e.User == "user1" && e.Action == AuditAction.Created && e.DocumentId == "doc1");
        Assert.Contains(entries, e => e.User == "user2" && e.Action == AuditAction.Viewed && e.DocumentId == "doc2");
    }
}