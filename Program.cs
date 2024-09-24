using almb;
using almb.Models;
using almb.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

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
