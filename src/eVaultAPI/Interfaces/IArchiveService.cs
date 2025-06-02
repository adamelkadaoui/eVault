using System.Threading.Tasks;
using eVaultAPI.Models;

namespace eVaultAPI.Interfaces;

public interface IArchiveService
{
    Task ArchiveDocumentAsync(ArchiveModel document);
    Task ValidateDocumentAsync(ArchiveModel document);
    Task DeleteDocumentAsync(string id);
    Task<ArchiveModel> GetDocumentAsync(string id);
    Task<ArchiveModel> UploadDocumentAsync(ArchiveModel document);
}