using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using NetFirebase.Api;
using NetFirebase.Api.Authentication;
using NetFirebase.Api.Data;
using NetFirebase.Api.Extensions;
using NetFirebase.Api.Services.Authentication;
using NetFirebase.Api.Services.Authorization;
using NetFirebase.Api.Services.Products;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString =
    builder.Configuration.GetConnectionString("ConnectionString")
    ?? throw new ArgumentNullException("Connection string not found.");

builder.Services.AddDbContext<DatabaseContext>(options =>
{
    options.UseNpgsql(connectionString);
});

builder.Services.AddSignalR();
builder.Services.AddHostedService<ServerNotifier>();

FirebaseApp.Create(new AppOptions { Credential = GoogleCredential.FromFile("firebase.json") });

builder.Services.AddHttpClient<IAuthenticationService, AuthenticationService>(
    (sp, httpClient) =>
    {
        var configuration = sp.GetRequiredService<IConfiguration>();
        var firebaseAuthUri =
            configuration["Authentication:TokenUri"]
            ?? throw new InvalidOperationException("TokenUri is not configured.");
        httpClient.BaseAddress = new Uri(firebaseAuthUri);
    }
);

builder
    .Services.AddAuthentication()
    .AddJwtBearer(
        JwtBearerDefaults.AuthenticationScheme,
        jwtOptions =>
        {
            jwtOptions.Authority = builder.Configuration["Authentication:ValidIssuer"];
            jwtOptions.Audience = builder.Configuration["Authentication:Audience"];
            jwtOptions.TokenValidationParameters.ValidIssuer = builder.Configuration[
                "Authentication:ValidIssuer"
            ];
        }
    );

// builder.Services.AddDbContext<DatabaseContext>(opt =>
// {
//     opt.LogTo(Console.WriteLine, [DbLoggerCategory.Database.Command.Name], LogLevel.Information)
//         .EnableSensitiveDataLogging();

//     opt.UseSqlite(builder.Configuration.GetConnectionString("SqliteDatabase"));
// });

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IPermissionService, PermissionService>();
builder.Services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();
builder.Services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
builder.Services.AddMemoryCache();
builder.Services.AddAuthorization();

builder.Services.AddCors(options =>
{
    options.AddPolicy(
        "CorsPolicy",
        builder =>
            builder
                .WithOrigins("http://localhost:5001")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials()
    );
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseTestData();

app.MapHub<NotificationHub>("notifications");

await app.RunAsync();
