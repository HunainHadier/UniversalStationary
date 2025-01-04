using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using UniversalStationary.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<MyDbContext>(opt => opt.UseSqlServer(
    builder.Configuration.GetConnectionString("conn")
    ));
builder.Services.AddCors(opt => opt.AddPolicy("AllowOrigin", builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "AllImages")),
    RequestPath = "/AllImages"
});


app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCors("AllowOrigin");
app.MapControllers();

app.Run();
