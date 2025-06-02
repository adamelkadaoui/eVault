using System.Threading.Tasks;
using eVaultAPI.Models;

namespace eVaultAPI.Interfaces;

public interface IArchiveService
{
    Task<ArchiveModel> UploadDocumentAsync(ArchiveModel document);
    Task<ArchiveModel> GetDocumentAsync(string id);
    Task DeleteDocumentAsync(string id);
    Task ValidateDocumentAsync(ArchiveModel document);
    Task ArchiveDocumentAsync(ArchiveModel document);
}