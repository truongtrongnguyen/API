using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebAPI_Version.Data;
using WebAPI_Version.Services;
using WebAPI_Version.Services.AddDataHangHoa;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<MyDbContext>(options =>
{
    // Có thể gán tham số của UserSqlServer là một chuỗi String connect = builder.Configuration.GetConnectionString("MyDb");
    options.UseSqlServer(builder.Configuration.GetConnectionString("MyDb"));    // Connect Database
    /*
     *  HỌC VỀ CÁCH TẠO API (CRUD), Code First (Migrations), CÁCH THIẾT LẬP CHUỖI CONNECT DATABASE QUA FILE appsetting.json 
     *  // Link Youtube:https://www.youtube.com/playlist?list=PLE5Bje814fYbhdwSHiHN9rlwJlwJ2YD3t
     *  
     * B1: Thiết lập chuỗi Connect Database trên file Appsetting.json
     * B2: Add sử dụng dịch vụ Dependency (Services) sử dụng SqlServer thông qua Key trong chuỗi Connect
     * B3: Gọi Hàm khởi tạo DbContextOPtions (base: options)
     * B4: Mở teminal và Add Migrations .....
     */
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
var secretKey = builder.Configuration["AppSettings:SecretKey"];

var SecretkeyBytes = Encoding.UTF8.GetBytes(secretKey);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        // Tự cấp ToKen, nếu sài dịch vụ ngoài thì là true và phải chỉ ra đường dẫn,  config tới dịch vụ mình chọn
        ValidateIssuer = false,
        ValidateAudience = false,

        // Ký vào token
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(SecretkeyBytes),

        ClockSkew = TimeSpan.Zero
    };
});

// Tiến hành đăng ký dịch vụ

builder.Services.AddScoped<IHangHoaReponsitoryAdd2, HangHoaReponsitoryAdd2>();

// builder.Services.AddScoped<ILoaiReponsitory, LoaiResponsitory>();
builder.Services.AddScoped<ILoaiReponsitory, LoaiReponsitoryInMemory>();

builder.Services.AddScoped<IHangHoaReponsitory, HangHoaReponsitory>();

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

// Link Youtube: https://www.youtube.com/watch?v=PKkKCv7Lno0&list=PLE5Bje814fYbhdwSHiHN9rlwJlwJ2YD3t&index=2