
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Video: https://www.youtube.com/watch?v=Zvh-tVs50q4&list=PLcvTyQIWJ_ZpumOgCCify-wDY_G-Kx34a&index=6

// Updating th middlewear to use versioning
builder.Services.AddApiVersioning(option =>
{
    // No need to add "?api-version=1.0" after https://localhost:7053/api/users
    option.AssumeDefaultVersionWhenUnspecified = true;

    // Version default is 1.0
    //option.DefaultApiVersion = new ApiVersion(1, 1);     // ApiVersion.Default();
    option.DefaultApiVersion = ApiVersion.Default;

    // this is going to return all available api version 
    option.ReportApiVersions = true;

    // Add Media type versioning
    option.ApiVersionReader = ApiVersionReader.Combine(
        new MediaTypeApiVersionReader("x-api-version"),
        new HeaderApiVersionReader("x-api-version")
        );
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
