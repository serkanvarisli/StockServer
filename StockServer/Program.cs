using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using StockServer.Contexts;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login/Login";
        // Role gereksinimleri burada tanýmlanýr
        options.AccessDeniedPath = "/Login/AccessDenied"; // Eriþim reddedildiðinde yönlendirme
    });
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("CanUpdateStock", policy => policy.RequireRole("Admin"));
});
builder.Services.AddDbContext<StockDbContext>(options=>
{
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

app.UseHttpsRedirection();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.UseCors("corspolicy");
app.Run();
