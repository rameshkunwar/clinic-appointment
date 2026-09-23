using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using Wolverine;
using Scalar.AspNetCore;
using Clinic.API.Data;
using Wolverine.FluentValidation;
using JasperFx.CodeGeneration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Host.UseWolverine(options =>
{
    options.CodeGeneration.TypeLoadMode = TypeLoadMode.Static;
    options.UseFluentValidation();
    options.InvokeTracing = InvokeTracingMode.Full;
});

builder.Host.UseDefaultServiceProvider(options => options.ValidateScopes = true);


// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference("/api-docs");
}

app.UseHttpsRedirection();

app.Run();