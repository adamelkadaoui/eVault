using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using eVaultAPI.Models;
using eVaultAPI.Interfaces;

namespace eVaultAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArchiveController : ControllerBase
    {
        private readonly IArchiveService _archiveService;

        public ArchiveController(IArchiveService archiveService)
        {
            _archiveService = archiveService;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadDocument([FromBody] ArchiveModel document)
        {
            if (document == null || document.FileContent == null)
            {
                return BadRequest("Invalid document.");
            }

            var result = await _archiveService.UploadDocumentAsync(document);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDocument(string id)
        {
            var document = await _archiveService.GetDocumentAsync(id);
            if (document == null)
            {
                return NotFound();
            }

            return Ok(document);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDocument(string id)
        {
            await _archiveService.DeleteDocumentAsync(id);
            return NoContent();
        }
    }
}