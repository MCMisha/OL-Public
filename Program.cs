using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using WebApplicationOperaLublin.Contexts;
using WebApplicationOperaLublin.Converters;
using WebApplicationOperaLublin.Interfaces.Converters;
using WebApplicationOperaLublin.Interfaces.Repositories;
using WebApplicationOperaLublin.Interfaces.Repositories.Admin;
using WebApplicationOperaLublin.Interfaces.Services;
using WebApplicationOperaLublin.Interfaces.Services.Admin;
using WebApplicationOperaLublin.Middlewares;
using WebApplicationOperaLublin.Options;
using WebApplicationOperaLublin.Repositories;
using WebApplicationOperaLublin.Repositories.Admin;
using WebApplicationOperaLublin.Services;
using WebApplicationOperaLublin.Services.Admin;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

AuthOptions.Create(builder.Configuration);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("AppDB")));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAdminService, AdminService>();

builder.Services.AddScoped<IAdminGenreRepository, AdminGenreRepository>();
builder.Services.AddScoped<IAdminGenreService, AdminGenreService>();

builder.Services.AddScoped<IAdminPerformanceRepository, AdminPerformanceRepository>();
builder.Services.AddScoped<IAdminPerformanceService, AdminPerformanceService>();

builder.Services.AddScoped<IGenreRepository, GenreRepository>();
builder.Services.AddScoped<IGenreService, GenreService>();

builder.Services.AddScoped<IPlaceRepository, PlaceRepository>();

builder.Services.AddScoped<IPerformanceRepository, PerformanceRepository>();
builder.Services.AddScoped<IPerformanceService, PerformanceService>();

builder.Services.AddScoped<IAdminPlaceRepository, AdminPlaceRepository>();
builder.Services.AddScoped<IAdminPlaceService, AdminPlaceService>();

builder.Services.AddScoped<IAdminNewsRepository, AdminNewsRepository>();
builder.Services.AddScoped<IAdminNewsService, AdminNewsService>();

builder.Services.AddScoped<IAdminArtistsRepository, AdminArtistsRepository>();
builder.Services.AddScoped<IAdminArtistsService, AdminArtistsService>();

builder.Services.AddScoped<IArtistsRepository, ArtistsRepository>();
builder.Services.AddScoped<IArtistsService, ArtistsService>();

builder.Services.AddScoped<ISectionRepository, SectionRepository>();
builder.Services.AddScoped<IAboutSectionService, AboutSectionService>();
builder.Services.AddScoped<IContactSectionService, ContactSectionService>();

builder.Services.AddScoped<IAdminAboutSectionRepository, AdminAboutSectionRepository>();
builder.Services.AddScoped<IAdminAboutSectionService, AdminAboutSectionService>();

builder.Services.AddScoped<IAdminContactSectionRepository, AdminContactSectionRepository>();
builder.Services.AddScoped<IAdminContactSectionService, AdminContactSectionService>();

builder.Services.AddScoped<IAdminPerformanceInfoRepository, AdminPerformanceInfoRepository>();
builder.Services.AddScoped<IAdminPerformanceInfoService, AdminPerformanceInfoService>();

builder.Services.AddScoped<IPerformanceInfoRepository, PerformanceInfoRepository>();
builder.Services.AddScoped<IPerformanceInfoService, PerformanceInfoService>();

builder.Services.AddScoped<INewsRepository, NewsRepository>();
builder.Services.AddScoped<INewsService, NewsService>();

builder.Services.AddScoped<IAdminTicketPriceRepository, AdminTicketPriceRepository>();
builder.Services.AddScoped<IAdminTicketPriceService, AdminTicketPriceService>();

builder.Services.AddScoped<IAdminCastRepository, AdminCastRepository>();
builder.Services.AddScoped<IAdminCastService, AdminCastService>();

builder.Services.AddScoped<ICastRepository, CastRepository>();
builder.Services.AddScoped<ICastService, CastService>();

builder.Services.AddScoped<ITicketPriceRepository, TicketPriceRepository>();
builder.Services.AddScoped<ITicketPriceService, TicketPriceService>();

builder.Services.AddScoped<IAdminPerformanceEventRepository, AdminPerformanceEventRepository>();
builder.Services.AddScoped<IAdminPerformanceEventService, AdminPerformanceEventService>();

builder.Services.AddScoped<IPerformanceEventRepository, PerformanceEventRepository>();
builder.Services.AddScoped<IPerformanceEventService, PerformanceEventService>();

builder.Services.AddScoped<IAdminPublicCommentRepository, AdminPublicCommentRepository>();
builder.Services.AddScoped<IAdminPublicCommentService, AdminPublicCommentService>();

builder.Services.AddScoped<IPublicCommentRepository, PublicCommentRepository>();
builder.Services.AddScoped<IPublicCommentService, PublicCommentService>();

builder.Services.AddScoped<IAdminMainPageBackgroundRepository, AdminMainPageBackgroundRepository>();
builder.Services.AddScoped<IAdminMainPageBackgroundService, AdminMainPageBackgroundService>();

builder.Services.AddScoped<IMainPageBackgroundRepository, MainPagePageBackgroundRepository>();
builder.Services.AddScoped<IMainPageBackgroundService, MainPageBackgroundService>();

builder.Services.AddScoped<IAdminArtistPhotoRepository, AdminArtistPhotoRepository>();
builder.Services.AddScoped<IAdminArtistPhotoService, AdminArtistPhotoService>();

builder.Services.AddScoped<IArtistPhotoRepository, ArtistPhotoRepository>();
builder.Services.AddScoped<IArtistPhotoService, ArtistPhotoService>();

builder.Services.AddScoped<IAdminPerformancePhotoRepository, AdminPerformancePhotoRepository>();
builder.Services.AddScoped<IAdminPerformancePhotoService, AdminPerformancePhotoService>();

builder.Services.AddScoped<IPerformancePhotoRepository, PerformancePhotoRepository>();
builder.Services.AddScoped<IPerformancePhotoService, PerformancePhotoService>();

builder.Services.AddScoped<IPerformanceConverter, PerformanceConverter>();
builder.Services.AddScoped<IPerformanceEventConverter, PerformanceEventConverter>();
builder.Services.AddScoped<INewsConverter, NewsConverter>();
builder.Services.AddScoped<IArtistConverter, ArtistConverter>();
builder.Services.AddScoped<IImplementerConverter, ImplementerConverter>();
builder.Services.AddScoped<IPublicCommentConverter, PublicCommentConverter>();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                if (context.Request.Cookies.TryGetValue("jwt_token", out var token))
                {
                    context.Token = token;
                }
                return Task.CompletedTask;
            }
        };
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = AuthOptions.Issuer,
            ValidateAudience = true,
            ValidAudience = AuthOptions.Audience,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero,
            IssuerSigningKey = AuthOptions.SymmetricSecurityKey,
            ValidateIssuerSigningKey = true,
        };
    });
builder.Services.AddControllers().AddNewtonsoftJson(options =>
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
    });

builder.Services.AddMemoryCache();
builder.Services.Configure<RateLimitOptions>(options =>
{
    options.GeneralRules = new List<RateLimitRule>
    {
        new()
        {
            Endpoint = "*",
            Period = "1m",
            Limit = 5
        }
    };
});
builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
builder.Services.AddInMemoryRateLimiting();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.WithOrigins("https://mcmisha.github.io")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        });
});

builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "JWTToken_Auth_API",
        Version = "v1"
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description =
            "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            []
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders.XForwardedProto
});

app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<ExceptionMiddleware>();
app.MapControllers();

app.Run();