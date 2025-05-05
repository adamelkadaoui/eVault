using System;
using System.IO;
using System.Threading.Tasks;
using Xunit;
using eVaultAPI.Services;
using eVaultAPI.Models;

namespace eVaultAPI.Tests.Services
{
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
        public async Task ArchiveDocumentAsync_ShouldSaveFileAndReturnHash()
        {
            // Arrange
            var document = new ArchiveModel
            {
                FileName = "test.txt",
                FileContent = System.Text.Encoding.UTF8.GetBytes("Test content")
            };

            // Act
            var hash = await _archiveService.ArchiveDocumentAsync(document);

            // Assert
            var filePath = Path.Combine(_testStoragePath, document.FileName);
            Assert.True(File.Exists(filePath));
            Assert.NotNull(hash);
        }

        [Fact]
        public async Task GetDocumentAsync_ShouldReturnDocument_WhenFileExists()
        {
            // Arrange
            var fileName = "test.txt";
            var fileContent = System.Text.Encoding.UTF8.GetBytes("Test content");
            var filePath = Path.Combine(_testStoragePath, fileName);
            await File.WriteAllBytesAsync(filePath, fileContent);

            // Act
            var document = await _archiveService.GetDocumentAsync(fileName);

            // Assert
            Assert.NotNull(document);
            Assert.Equal(fileName, document.FileName);
            Assert.Equal(fileContent, document.FileContent);
        }

        [Fact]
        public async Task DeleteDocumentAsync_ShouldRemoveFile_WhenFileExists()
        {
            // Arrange
            var fileName = "test.txt";
            var filePath = Path.Combine(_testStoragePath, fileName);
            await File.WriteAllBytesAsync(filePath, System.Text.Encoding.UTF8.GetBytes("Test content"));

            // Act
            await _archiveService.DeleteDocumentAsync(fileName);

            // Assert
            Assert.False(File.Exists(filePath));
        }
    }
}