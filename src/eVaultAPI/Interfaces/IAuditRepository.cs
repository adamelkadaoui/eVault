using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eVaultAPI.Models;

namespace eVaultAPI.Interfaces
{
    public interface IAuditRepository
    {
        Task SaveAsync(AuditEntry entry);
        Task<List<AuditEntry>> GetAllAsync();
    }
}