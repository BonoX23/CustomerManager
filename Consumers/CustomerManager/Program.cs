using CustomerManager.Configuration;
using Data;
using Domain.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Configuração da conexão com o banco de dados
var connection = builder.Configuration.GetConnectionString("Main");

builder.Services.AddDbContext<CustomerDbContext>(options =>
    options.UseSqlServer(connection));

// Configuração do Identity
builder.Services.AddIdentity<User, IdentityRole<int>>()
    .AddEntityFrameworkStores<CustomerDbContext>()
    .AddDefaultTokenProviders();

// Configuração da autenticação JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"], // Obtido do appsettings.json
            ValidAudience = builder.Configuration["Jwt:Audience"], // Obtido do appsettings.json
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])) // Obtido do appsettings.json
        };
    });

builder.Services.InjectDependencyRegister();

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
