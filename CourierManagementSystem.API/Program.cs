using CourierManagementSystem.API.Data;
using CourierManagementSystem.API.Helpers;
using CourierManagementSystem.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 1️⃣ Connect to SQL Server using the connection string
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2️⃣ Add Identity for User Management (login/signup)
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// 3️⃣ Add Authentication & Authorization
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();
builder.Services.AddScoped<JwtTokenHelper>();

// 4️⃣ Add Swagger and Controllers
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// 5️⃣ Enable Swagger and Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

// Call role seeding
await CreateRolesAsync(app);

app.Run();

async Task CreateRolesAsync(IApplicationBuilder app)
{
    using var scope = app.ApplicationServices.CreateScope();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    await CourierManagementSystem.API.Seeders.DefaultRoles.SeedAsync(roleManager);
}

// Call the method before app.Run();
await CreateRolesAsync(app);
