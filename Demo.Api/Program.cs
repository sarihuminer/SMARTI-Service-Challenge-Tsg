using Demo.Application;
using Demo.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Server.IISIntegration;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.OpenApi.Models;
using System.Reflection;

const string ApplicationName = "Demo";
const string ApplicationDescription = "Demo WebApi";
const string SwaggerVersion = "2.0";

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddMvc().AddFluentValidation(opt =>
{
    opt.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
});
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

builder.Services.AddAuthentication(IISDefaults.AuthenticationScheme);

builder.Services.AddControllers();
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
//add shared services if there
//add rebbit service if there
builder.Services.AddApplicationFactory();
builder.Services.AddRepositoriesFactory();
//add swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
});
// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var app = builder.Build();

// Configure the HTTP request pipeline.
IWebHostEnvironment env = app.Environment;

if(env.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.RoutePrefix = string.Empty;
    });
    app.UseDeveloperExceptionPage();
}

app.Use(async (context, next) =>
{
    // Custom logic before the next middleware
    if(context.Request.Method == "OPTIONS")
    {
        var response = context.Response;
        response.Headers.Add("Access-Control-Allow-Methods", "POST, GET, OPTIONS, PUT, DELETE");
        response.Headers.Add("Access-Control-Allow-Headers", "Content-Type, Authorization");
        response.Headers.Add("Access-Control-Allow-Origin", MyAllowSpecificOrigins);
    }
    else
    {
    // Call the next middleware in the pipeline
    await next.Invoke();
    }
});

app.UseCors(builder => builder
    .AllowAnyHeader()
    .AllowAnyMethod()
    .SetIsOriginAllowed(origin => true)
    .AllowCredentials());
    
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.Run();
