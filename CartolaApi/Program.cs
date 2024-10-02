using Microsoft.EntityFrameworkCore;
using CartolaApi.Utils;
using CartolaApi.Data.Services;
using CartolaApi.Router;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("CartolaConnection");

builder.Services.AddDbContext<AppDbContext>(opts =>
    opts.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddCors();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<Hash>();
builder.Services.AddScoped<UserServices>();
builder.Services.AddScoped<TeamServices>();
builder.Services.AddScoped<TournamentServices>();
var app = builder.Build();

app.UseCors(builder => builder
    .AllowAnyOrigin()
    .AllowAnyHeader()
    .AllowAnyMethod()
);

app.UseSwagger();
app.UseSwaggerUI();
app.MapV1Routes();
app.MapGet("/", () => Results.Redirect("/swagger"));

app.Run();