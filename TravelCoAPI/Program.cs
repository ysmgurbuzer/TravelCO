using Application.Travel._Helpers;
using Application.Travel.Interfaces;
using Application.Travel.Services;
using Application.Travel.Tools;
using Domain.Travel.Entities;
using Infrastructure.Travel.CustomErrorHandler;
using Infrastructure.Travel.SignalR.Hubs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Persistence.Travel.Context;
using Persistence.Travel.Repositories;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Application.Travel;
using Infrastructure.Travel.Tools;
using Hangfire;
using OfficeOpenXml;
using Microsoft.Extensions.Configuration;
using TravelCoAPI.Controllers;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();
builder.Services.AddDbContext<TravelContext>(opt => opt.UseSqlServer(builder.Configuration.GetSection("ConnectionStrings:DefaultConnectionString").Value));
var hangfireConnectionString = builder.Configuration.GetConnectionString("HangfireConnection");


builder.Services.AddHangfire(config => config
    .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UseSqlServerStorage(hangfireConnectionString));


builder.Services.AddHangfireServer();
builder.Services.AddCors(opt =>
{
    opt.AddPolicy("RoamlyApiCors", opts =>
    {
        opts.WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});
ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
builder.Services.AddCors(opt =>
opt.AddPolicy("CorsPolicy", builder =>
{
    builder.AllowAnyHeader()
    .AllowAnyMethod()
    .SetIsOriginAllowed((host) => true)
    .AllowCredentials();
}));

builder.Services.AddSignalR();
builder.Services.AddScoped<IUow, Uow>();
builder.Services.AddScoped<TravelContext>();
builder.Services.AddScoped<MongoContext>();
builder.Services.AddScoped<AIRecommendationServiceBuilder, AIRecommendationService>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped(typeof(IRepository<Housing>), sp =>
{
    var roamlyDbContext = sp.GetRequiredService<TravelContext>();
    var repository = new Repository<Housing>(roamlyDbContext);
    var redisService = sp.GetRequiredService<RedisService>();
    return new RepositoryWithCacheDecorator<Housing>(redisService, repository);
});

builder.Services.AddScoped(typeof(IRepository<Reservation>), sp =>
{
    var roamlyDbContext = sp.GetRequiredService<TravelContext>();
    var repository = new Repository<Reservation>(roamlyDbContext);
    var redisService = sp.GetRequiredService<RedisService>();
    return new RepositoryWithCacheDecorator<Reservation>(redisService, repository);
});

builder.Services.AddScoped(typeof(IRepository<Survey>), sp =>
{
    var roamlyDbContext = sp.GetRequiredService<TravelContext>();
    var repository = new Repository<Survey>(roamlyDbContext);
    var redisService = sp.GetRequiredService<RedisService>();
    return new RepositoryWithCacheDecorator<Survey>(redisService, repository);
});

builder.Services.AddSingleton<RedisService>();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof
    (DependencyExtension).Assembly));

builder.Services.AddSingleton<string>(provider => ("AIzaSyAP8xFXLmlUSx7OgN0t8_XSCBHUZE4t4AY"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(setup =>
{

    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        BearerFormat = "JWT",
        Name = "JWT Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    setup.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

    setup.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { jwtSecurityScheme, Array.Empty<string>() }
    });
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})


.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWTKey:ValidAudience"],
        ValidIssuer = builder.Configuration["JWTKey:ValidIssuer"],
        ClockSkew = TimeSpan.Zero,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWTKey:Secret"]))
    };
});


builder.Services.AddScoped<JwtTokenGenerator>();
builder.Services.AddHttpContextAccessor();

var profiles = ProfileHelpers.GetProfiles();
var configuration = new AutoMapper.MapperConfiguration(opt =>
{
    opt.AddProfiles(profiles);
});
var mapper = configuration.CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddMemoryCache();

var app = builder.Build();
var redisService = app.Services.GetRequiredService<RedisService>();

    app.UseSwagger();
    app.UseSwaggerUI();
app.UseStaticFiles();
app.UseCors("RoamlyApiCors");
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<ErrorHandlerMiddleware>();
redisService.Connect();
app.MapControllers();
app.MapHub<ReservationHub>("/reservationHub");
app.UseHangfireDashboard();
app.Run();
