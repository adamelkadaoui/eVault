using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using eVaultAPI.Models;

namespace eVaultAPI.Services;

public class AuditService
{
    private readonly List<AuditEntry> _entries = new();
    public Task LogAsync(string user, string action, string documentId, string details = null)
    {
        _entries.Add(new AuditEntry
        {
            User = user,
            Action = action,
            DocumentId = documentId,
            Timestamp = DateTime.UtcNow,
            Details = details
        });
        return Task.CompletedTask;
    }
    public Task<List<AuditEntry>> GetAllAsync() => Task.FromResult(_entries);
}
