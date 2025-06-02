using System;
using System.IO;
using System.Threading.Tasks;
using Xunit;
using eVaultAPI.Services;
using eVaultAPI.Models;

namespace eVaultAPI.Tests.Services;

public class ArchiveServiceTests
{
    private readonly string _testStoragePath = Path.Combine(Path.GetTempPath(), "eVaultTestStorage");
    private readonly ArchiveService _archiveService;

    public ArchiveServiceTests()
    {
        // Crée un répertoire temporaire pour les tests
        if (!Directory.Exists(_testStoragePath))
        {
            Directory.CreateDirectory(_testStoragePath);
        }
        _archiveService = new ArchiveService(_testStoragePath);
    }

    [Fact]
    public async Task ArchiveDocumentAsync_ShouldSaveFile()
    {
        var document = new ArchiveModel
        {
            FileName = "test.txt",
            FileContent = System.Text.Encoding.UTF8.GetBytes("Test content")
        };

        await _archiveService.ArchiveDocumentAsync(document);

        var filePath = Path.Combine(_testStoragePath, document.FileName);
        Assert.True(File.Exists(filePath));
    }

    [Fact]
    public async Task GetDocumentAsync_ShouldReturnDocument_WhenFileExists()
    {
        var fileName = "test.txt";
        var fileContent = System.Text.Encoding.UTF8.GetBytes("Test content");
        var filePath = Path.Combine(_testStoragePath, fileName);
        await File.WriteAllBytesAsync(filePath, fileContent);
        
        var document = await _archiveService.GetDocumentAsync(fileName);

        Assert.NotNull(document);
        Assert.Equal(fileName, document.FileName);
        Assert.Equal(fileContent, document.FileContent);
    }

    [Fact]
    public async Task DeleteDocumentAsync_ShouldRemoveFile_WhenFileExists()
    {
        var fileName = "test.txt";
        var filePath = Path.Combine(_testStoragePath, fileName);
        await File.WriteAllBytesAsync(filePath, System.Text.Encoding.UTF8.GetBytes("Test content"));

        await _archiveService.DeleteDocumentAsync(fileName);

        Assert.False(File.Exists(filePath));
    }
}
