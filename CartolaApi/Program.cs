using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CartolaApi.Utils;
using CartolaApi.Data.Functions;
using CartolaApi.Routes;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("CartolaConnection");

builder.Services.AddDbContext<AppDbContext>(opts =>
    opts.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services.AddCors();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<Hash>();
builder.Services.AddScoped<UserDbFunctions>();

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
app.MapGroupTournament();
app.MapGroupUser();
app.MapGet("/", () => Results.Redirect("/swagger"));

app.Run();