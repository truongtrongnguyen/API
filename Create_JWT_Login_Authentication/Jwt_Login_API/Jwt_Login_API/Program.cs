using JWT_Login_Authentication.Configurations;
using JWT_Login_Authentication.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    var connect = builder.Configuration.GetConnectionString("AppDbContext");
    options.UseSqlServer(connect);
});
builder.Services.Configure<JwtConfig>(builder.Configuration.GetSection(key: "JwtConfig"));

builder.Services.AddDefaultIdentity<IdentityUser>(option =>
{
    option.SignIn.RequireConfirmedEmail = false;
}).AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(jwt =>
{
    var bytes = Encoding.UTF8.GetBytes(builder.Configuration["JwtConfig:SecretKey"]);

    jwt.SaveToken = true;    // Save Token Header

    jwt.TokenValidationParameters = new TokenValidationParameters()     // Validate form Token
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(bytes),
        ValidateIssuer = false,  // for dev
        ValidateAudience = false,    // for dev
        RequireExpirationTime = true, // for dev --needs to updated when toke is added
        ValidateLifetime = true,  // is a Value

        ClockSkew = TimeSpan.Zero
    };
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
