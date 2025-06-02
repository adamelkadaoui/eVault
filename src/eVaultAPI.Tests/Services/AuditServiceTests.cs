using System.Threading.Tasks;
using Xunit;
using eVaultAPI.Services;
using eVaultAPI.Models;

namespace eVaultAPI.Tests.Services;

public class AuditServiceTests
{
    [Fact]
    public async Task LogAsync_ShouldAddEntry()
    {
        var auditService = new AuditService();
        string user = "testuser";
        string action = "Upload";
        string documentId = "doc123";
        string details = "Test details";

        await auditService.LogAsync(user, action, documentId, details);
        var entries = await auditService.GetAllAsync();

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
        var auditService = new AuditService();

        await auditService.LogAsync("user1", "Action1", "doc1");
        await auditService.LogAsync("user2", "Action2", "doc2");
        var entries = await auditService.GetAllAsync();

        Assert.Equal(2, entries.Count);
        Assert.Contains(entries, e => e.User == "user1" && e.Action == "Action1" && e.DocumentId == "doc1");
        Assert.Contains(entries, e => e.User == "user2" && e.Action == "Action2" && e.DocumentId == "doc2");
    }
}