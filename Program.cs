// using System.IdentityModel.Tokens.Jwt;
// using System.Text;
using DotNet_WebAPI_doc.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
// using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
// using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the local connection container.
// builder.Services.AddDbContext<ApplicationDbContext>(options =>
//     options.UseInMemoryDatabase("NotesDb"));

// Configure PostgreSQL connection
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));


// Console.WriteLine("JWT Key: " + builder.Configuration["JWT:Key"]);
// Console.WriteLine("JWT Issuer: " + builder.Configuration["JWT:Issuer"]);
// Console.WriteLine("JWT Audience: " + builder.Configuration["JWT:Audience"]);

// var keyBytes = Encoding.UTF8.GetBytes(builder.Configuration["JWT:SigningKey"]);
// Console.WriteLine($"Key Length in Bytes: {keyBytes.Length}");

// Should output 32 or greater
// if (keyBytes.Length < 32)
// {
//     throw new Exception("JWT key must be at least 32 bytes long!");
// }
// jwt cookie authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.HttpOnly = true;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // Use Always for HTTPS
        options.Cookie.SameSite = SameSiteMode.Strict;
        options.LoginPath = "/api/auth/login";
        options.LogoutPath = "/api/auth/logout";
        options.ExpireTimeSpan = TimeSpan.FromDays(1);
    });

builder.Services.AddAuthorization();


// dependency injections


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

var app = builder.Build();

// Middleware
// app.UseSwagger();
// app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthentication(); // Enable Authentication Middleware
app.UseAuthorization();

app.MapControllers();


// var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:SigningKey"]));
// var credentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

// var token = new JwtSecurityToken(
//     issuer: builder.Configuration["JWT:Issuer"],
//     audience: builder.Configuration["JWT:Audience"],
//     expires: DateTime.Now.AddMinutes(30),
//     signingCredentials: credentials
// );

// var tokenHandler = new JwtSecurityTokenHandler();
// string tokenString = tokenHandler.WriteToken(token);
// Console.WriteLine($"Generated Token: {tokenString}");

app.Run();