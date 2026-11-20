using Scalar.AspNetCore;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("APIBaseU4Context") ?? throw new InvalidOperationException("Connection string 'APIBaseU4Context' not found.");

builder.Services.AddDbContext<APIBaseU4Context>(options => options.UseSqlServer(connectionString));

builder.Services.AddAutoMapper(cfg => cfg.AddMaps(typeof(Program).Assembly));


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();

    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
