using System.Collections.Generic;
using System.Threading.Tasks;
using eVaultAPI.Models;

namespace eVaultAPI.Interfaces;

public interface IAuditService
{
    Task LogAsync(string user, AuditAction action, string documentId, string details = null);
    Task<List<AuditEntry>> GetAllAsync();
}