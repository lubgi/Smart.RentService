using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Smart.RentService.Application;
using Smart.RentService.Infrastructure;
using Smart.RentService.Infrastructure.Data;
using Smart.RentService.WebAPI;
using Smart.RentService.WebAPI.Filters.Authorization;
using Smart.RentService.WebAPI.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure(builder.Configuration)
    .AddApplication(builder.Configuration)
    .AddWebUI();

var app = builder.Build();

await app.Services.SeedData();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

var option = new RewriteOptions();
option.AddRedirect("^$", "swagger");
app.UseRewriter(option);

app.Run();
