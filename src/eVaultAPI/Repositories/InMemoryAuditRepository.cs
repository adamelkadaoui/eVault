
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eVaultAPI.Interfaces;
using eVaultAPI.Models;

namespace eVaultAPI.Repositories;

public class InMemoryAuditRepository : IAuditRepository
{
    private readonly ConcurrentBag<AuditEntry> _entries = [];

    public Task SaveAsync(AuditEntry entry)
    {
        _entries.Add(entry);
        return Task.CompletedTask;
    }

    public Task<List<AuditEntry>> GetAllAsync()
    {
        return Task.FromResult(_entries.ToList());
    }
}