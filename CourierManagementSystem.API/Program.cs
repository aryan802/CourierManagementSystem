using CourierManagementSystem.API.Data;
using CourierManagementSystem.API.Helpers;
using CourierManagementSystem.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

var builder = WebApplication.CreateBuilder(args);

// 1️⃣ Connect to SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2️⃣ Identity setup
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// 3️⃣ JWT + Auth
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();
builder.Services.AddScoped<JwtTokenHelper>();

// 4️⃣ Add Swagger with full OpenAPI metadata
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Courier Management API",
        Version = "v1",
        Description = "API for managing courier bookings, users, roles and tracking",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "Aryan",
            Email = "aryan@example.com"
        },
        License = new Microsoft.OpenApi.Models.OpenApiLicense
        {
            Name = "MIT License",
            Url = new Uri("https://opensource.org/licenses/MIT")
        }
    });

    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Enter 'Bearer' followed by your JWT token.\nExample: Bearer eyJhbGciOi..."
    });

    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });

    options.EnableAnnotations();
});

var app = builder.Build();

// 5️⃣ Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        // ✅ Load only your correct spec and ignore defaults
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Courier API v1");
        c.RoutePrefix = "swagger"; // Serve at /swagger/index.html
        c.ConfigObject.AdditionalItems["urls.primaryName"] = "Courier API v1"; // Prevent fallback load
    });
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

// 6️⃣ Role + User Seeder
await CreateRolesAndUsersAsync(app);

app.Run();

// 🔁 Seeding
async Task CreateRolesAndUsersAsync(IApplicationBuilder app)
{
    using var scope = app.ApplicationServices.CreateScope();

    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    await CourierManagementSystem.API.Seeders.DefaultRoles.SeedAsync(roleManager);

    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
    await CourierManagementSystem.API.Seeders.DefaultUsers.SeedAsync(userManager);
}

