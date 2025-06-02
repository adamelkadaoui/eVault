using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Threading.Tasks;
using eVaultAPI.Interfaces;
using eVaultAPI.Models;

namespace eVaultAPI.Services;

public class ArchiveService(string storagePath) : IArchiveService
{
  private readonly string _storagePath = storagePath;
  public async Task ArchiveDocumentAsync(ArchiveModel archiveModel)
  {
        var filePath = Path.Combine(_storagePath, archiveModel.FileName);
        await using var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write);
        await stream.WriteAsync(archiveModel.FileContent.AsMemory(0, archiveModel.FileContent.Length));

        archiveModel.Status = "Archived";
        archiveModel.UpdatedAt = DateTime.UtcNow;
    }

    public string ComputeHash(byte[] fileContent)
    {
        using var sha256 = SHA256.Create();
        var hashBytes = sha256.ComputeHash(fileContent);
        return Convert.ToBase64String(hashBytes);
    }

    public async Task<ArchiveModel> GetDocumentAsync(string id)
        {
        var filePath = Path.Combine(_storagePath, id);
        if (!File.Exists(filePath))
        {
            return null;
        }

        var fileContent = await File.ReadAllBytesAsync(filePath);
        return new ArchiveModel
        {
            Id = id,
            FileName = Path.GetFileName(filePath),
            FileContent = fileContent,
            Status = "Retrieved",
            CreatedAt = File.GetCreationTimeUtc(filePath),
            UpdatedAt = File.GetLastWriteTimeUtc(filePath)
        };
    }

    public async Task SaveDocumentAsync(ArchiveModel document)
    {
        var filePath = Path.Combine(_storagePath, document.FileName);
        await using var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write);
        await stream.WriteAsync(document.FileContent.AsMemory(0, document.FileContent.Length));

        document.Status = "Archived";
        document.UpdatedAt = DateTime.UtcNow;
    }

    public Task ValidateDocumentAsync(ArchiveModel document)
    {
        document.Status = "Validated";
        document.UpdatedAt = DateTime.UtcNow;
        return Task.CompletedTask;
    }

    public Task DeleteDocumentAsync(string id)
    {
        var filePath = Path.Combine(_storagePath, id);
        if (File.Exists(filePath))
        {
             File.Delete(filePath);
        }
        return Task.CompletedTask;
    }

    public async Task<ArchiveModel> UploadDocumentAsync(ArchiveModel document)
    {
        var filePath = Path.Combine(_storagePath, document.FileName);
        await using var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write); 
        await stream.WriteAsync(document.FileContent.AsMemory(0, document.FileContent.Length));
        
        document.Status = "Uploaded";
        document.CreatedAt = DateTime.UtcNow;
        return document;
    }
}