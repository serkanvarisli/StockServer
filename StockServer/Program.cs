using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using StockServer.Contexts;
using StockServer.Entities;
using StockServer.Token;
using System.Reflection.Emit;
using System;
using System.Security.Claims;
using System.Text;
using StockServer.SeedData;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new()
        {
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidAudience = builder.Configuration["Token:Audience"],
            ValidIssuer = builder.Configuration["Token:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),
            LifetimeValidator = (notBefore, expires, securityToken, validationParameters) => expires != null ? expires > DateTime.UtcNow : false,
            NameClaimType = ClaimTypes.Name,
        };
    });
builder.Services.AddScoped<ITokenHandler, StockServer.Token.TokenHandler>();
builder.Services.Configure<JwtBearerOptions>(builder.Configuration.GetSection("Authentication:JwtBearer"));

builder.Services.AddDbContext<StockDbContext>(options =>
{
    //inmemory db için
    //ayrýca aþaðýda yer alan in memory db için yazan kodlarý da aktifleþtirmek gerekiyor
    //options.UseInMemoryDatabase(builder.Configuration.GetConnectionString("DefaultConnection"));

    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddCors(
    p => p.AddPolicy("corspolicy", build =>
    build.WithOrigins("http://localhost:5173", "http://localhost:3000")
        .AllowAnyMethod()
        .AllowAnyHeader()
)
    );

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//  InMemory db için
//var scope = app.Services.CreateScope();
//var context = scope.ServiceProvider.GetService<StockDbContext>();
//CreateSeedData.SeedData(context);


app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.UseCors("corspolicy");

app.Run();


