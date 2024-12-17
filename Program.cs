using almb.Data;
using almb.Services;
using almb.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                      ?? Environment.GetEnvironmentVariable("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins",
        policy =>
        {
            policy.WithOrigins("http://localhost:5173", "https://minhalinda.vercel.app/")
                    .AllowAnyHeader()
                    .AllowAnyMethod();
        });
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "API ALMB", Description = "Api álmbum de imagens", Version = "v1" });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "APISaudacao v1");
});

//app.MapPost("/users", (AppDbContext context, [FromBody] CreateUserViewModel model) =>
//{
//    if (!model.IsValid)
//    {
//        return Results.BadRequest(model.Notifications);
//    }

//    var user = model.MapTo();
//    user.EncryptPassword();
//    context.Users.Add(user);
//    context.SaveChanges();

//    return Results.Created($"/users/{user.Id}", user.Id);
//});

app.MapGet("/images", (AppDbContext context) =>
{
    var images = context.Images.ToList();

    return Results.Ok(images);
});

app.MapPost("/images", (AppDbContext context, [FromBody] CreateImageViewModel model) =>
{
    var lastSequence = context.Images
                                .OrderByDescending(x => x.Sequence)
                                .FirstOrDefault()?.Sequence ?? 0;

    model.SetSequence(lastSequence + 1);

    if (!model.IsValid)
        return Results.BadRequest(model.Notifications);

    var image = model.MapTo();

    context.Images.Add(image);
    context.SaveChanges();

    return Results.Created($"/images/{image.Id}", image);
});

app.Run();
