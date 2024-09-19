
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>();

var app = builder.Build();

app.UseCors(builder => builder
  .AllowAnyOrigin()
  .AllowAnyHeader()
  .AllowAnyMethod()
);

app.UseSwagger();
app.UseSwaggerUI();
app.MapGroupPlayer();
app.MapTeamEndpoints();

app.Run();
