using System;

namespace eVaultAPI.Models;

public class AuditEntry
{
    public int Id { get; set; }
    public string User { get; set; }
    public AuditAction Action { get; set; }
    public string DocumentId { get; set; }
    public DateTime Timestamp { get; set; }
    public string Details { get; set; }
}