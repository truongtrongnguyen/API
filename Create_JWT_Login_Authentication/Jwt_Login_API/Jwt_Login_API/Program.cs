
using FluentValidation;
using FluentValidation.AspNetCore;
using Jwt_Login_API;
using Jwt_Login_API.Commond;
using Jwt_Login_API.Data;
using Jwt_Login_API.DbAccess;
using Jwt_Login_API.Models;
using Jwt_Login_API.Validations;
using JWT_Login_Authentication.Models;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// setup Dapper
builder.Services.AddSingleton<ISqlDataAccess, SqlDataAccess>();
builder.Services.AddSingleton<ICategoryData, CategoryData>();

// setup fluent validation thủ công
// builder.Services.AddScoped<IValidator<CreateCategoryRequest>, CreateCategoryRequestValidation>();

// FluentValidation.DependencyInjectionExtensions
// builder.Services.AddFluentValidation();
 builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<IAssemblyMaker>();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    var connect = builder.Configuration.GetConnectionString("AppDbContext");
    options.UseSqlServer(connect);
});

builder.Services.AddDefaultIdentity<IdentityUser>(option =>
{
    option.SignIn.RequireConfirmedEmail = false;
}).AddEntityFrameworkStores<AppDbContext>();


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

// ConfigureApi
app.ConfigureApi();

app.Run();
