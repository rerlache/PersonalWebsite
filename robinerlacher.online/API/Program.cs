global using General.Models;
global using OfFlyingPorkies.BAL;
global using Microsoft.EntityFrameworkCore;
using API.Data;
using API.Services.GeneralService;
using System.Text.Json.Serialization;
using API.Helpers;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins, policy => policy
        //.WithOrigins("https://localhost", "https://apps.robinerlacher.online", "http://localhost:5173", "https://*:5173", "https://atws2071:5250", "http://10.10.3.77:5173")
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowAnyOrigin()
        );
});

builder.Services.AddControllers();
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IApplicationService, ApplicationService>();
builder.Services.AddDbContext<GeneralContext>();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddTransient<ITokenGenerator, TokenGenerator>();

JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

builder.Services.AddAuthentication(opt => opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(cfg =>
{
    cfg.SaveToken = true;
    cfg.RequireHttpsMetadata = true;
    cfg.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["JWTIssuer"],
        ValidAudience = builder.Configuration["JWTIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWTKey"])),
        ClockSkew = TimeSpan.Zero
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(MyAllowSpecificOrigins);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
