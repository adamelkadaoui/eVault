using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using eVaultAPI.Interfaces;
using eVaultAPI.Models;

namespace eVaultAPI.Services;

public class AuditService(IAuditRepository repository) : IAuditService
{

    public async Task LogAsync(string user, AuditAction action, string documentId, string details = null)
    {
        var entry = new AuditEntry
        {
            User = user,
            Action = action,
            DocumentId = documentId,
            Timestamp = DateTime.UtcNow,
            Details = details
        };
        await repository.SaveAsync(entry);
    }

    public Task<List<AuditEntry>> GetAllAsync() => repository.GetAllAsync();
}
