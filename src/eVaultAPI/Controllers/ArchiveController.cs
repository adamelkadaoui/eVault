using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using eVaultAPI.Models;
using eVaultAPI.Interfaces;
using System.IO;

namespace eVaultAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ArchiveController(IArchiveService archiveService) : ControllerBase
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

        return Ok(document);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDocument(string id)
    {
        await archiveService.DeleteDocumentAsync(id);
        return NoContent();
    }
}
