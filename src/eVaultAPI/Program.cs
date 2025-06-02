using eVaultAPI.Interfaces;
using eVaultAPI.Repositories;
using eVaultAPI.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IO;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IArchiveService, ArchiveService>(provider =>
    new ArchiveService(Path.Combine(Directory.GetCurrentDirectory(), "Storage")));
builder.Services.AddSingleton<IAuditRepository, InMemoryAuditRepository>();
builder.Services.AddSingleton<AuditService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => 
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "eVault API V1");
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
