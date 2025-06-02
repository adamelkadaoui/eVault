using System;

namespace eVaultAPI.Models;
public class ArchiveModel
{
    public string Id { get; set; }
    public string FileName { get; set; }
    public byte[] FileContent { get; set; }
    public string Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
