using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using eVaultAPI.Models;
using eVaultAPI.Interfaces;
using System.IO;
using eVaultAPI.Services;

namespace eVaultAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ArchiveController(IArchiveService archiveService, AuditService auditService) : ControllerBase
{
    private const string STORAGE_PATH = "Storage";

    [HttpPost("upload")]
    public async Task<IActionResult> UploadDocument([FromBody] ArchiveModel document)
    {
        if (document == null || document.FileContent == null)
        {
            return BadRequest("Invalid document.");
        }

        var filePath = Path.Combine(STORAGE_PATH, document.FileName);
        if (System.IO.File.Exists(filePath))
        {
            return Conflict("A file with the same name already exists.");
        }

        var result = await archiveService.UploadDocumentAsync(document);
        await auditService.LogAsync(User?.Identity?.Name ?? "anonymous", AuditAction.Created, document.Id, "Détails éventuels");
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetDocument(string id)
    {
        var document = await archiveService.GetDocumentAsync(id);
        if (document == null)
        {
            return NotFound();
        }

        await auditService.LogAsync(User?.Identity?.Name ?? "anonymous", AuditAction.Viewed, document.Id, "Détails éventuels");
        return Ok(document);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDocument(string id)
    {
        await archiveService.DeleteDocumentAsync(id);
        await auditService.LogAsync(User?.Identity?.Name ?? "anonymous", AuditAction.Deleted, id, "Détails éventuels");
        return NoContent();
    }

    [HttpPatch("{id}/validate")]
    public async Task<IActionResult> ValidateDocument(string id)
    {
        var document = await archiveService.GetDocumentAsync(id);
        if (document == null)
            return NotFound();

        await archiveService.ValidateDocumentAsync(document);
        await auditService.LogAsync(User?.Identity?.Name ?? "anonymous", AuditAction.Updated, id, "Détails éventuels");
        return Ok(document);
    }

    [HttpPatch("{id}/archive")]
    public async Task<IActionResult> ArchiveDocument(string id)
    {
        var document = await archiveService.GetDocumentAsync(id);
        if (document == null)
            return NotFound();

        await archiveService.ArchiveDocumentAsync(document);
        await auditService.LogAsync(User?.Identity?.Name ?? "anonymous", AuditAction.Archived, id, "Détails éventuels");
        return Ok(document);
    }
    
    [HttpGet("audit")]
    public async Task<IActionResult> GetAudit()
    {
        var logs = await auditService.GetAllAsync();
        return Ok(logs);
    }
}
