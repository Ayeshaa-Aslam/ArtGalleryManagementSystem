using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using visualbasicproject;

// Create and configure the WebApplication builder
var builder = WebApplication.CreateBuilder(args);

// Add services for EF Core and Swagger
builder.Services.AddDbContext<ArtgalleryMsContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "ArtGalleryMS API",
        Description = "See Art, Artist, and Exhibition details",
        Version = "v1"
    });
});

var app = builder.Build();

// Enable Swagger in development environment
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "ArtGalleryMS API V1");
    });
}


app.MapGet("/", () => "Hello..! See ArtGallery Management System here");

//create own details
app.MapGet("/me", () =>  Aboutme());

 static string Aboutme(){
        string name = "Ayesha Aslam";
        string AridNumber = "21_Arid_3960";
        string projectname = "Art gallery MS";
        string details = $"Name: {name}\n AridNumber: {AridNumber}\nProject: {projectname}";
        return details;
    }

    
// API Routes for Artists
app.MapPost("/artists", (Aartist artist, ArtgalleryMsContext db) =>
{
    db.Aartists.Add(artist);
    db.SaveChanges();
    return Results.Created($"/artists/{artist.ArtistId}", artist);
});

app.MapGet("/artists", (ArtgalleryMsContext db) => db.Aartists.ToList());

app.MapPut("/artists/{id}", (int id, Aartist updatedArtist, ArtgalleryMsContext db) =>
{
    var artist = db.Aartists.Find(id);
    if (artist == null) return Results.NotFound();

    artist.Name = updatedArtist.Name;
    artist.Contact = updatedArtist.Contact;
    db.SaveChanges();

    return Results.Ok(artist);
});

app.MapDelete("/artists/{id}", (int id, ArtgalleryMsContext db) =>
{
    var artist = db.Aartists.Find(id);
    if (artist == null) return Results.NotFound();

    db.Aartists.Remove(artist);
    db.SaveChanges();

    return Results.NoContent();
});

// API Routes for Arts
app.MapPost("/arts", (Art art, ArtgalleryMsContext db) =>
{
    db.Arts.Add(art);
    db.SaveChanges();
    return Results.Created($"/arts/{art.ArtId}", art);
});

app.MapGet("/arts", (ArtgalleryMsContext db) => db.Arts.ToList());

app.MapPut("/arts/{id}", (int id, Art updatedArt, ArtgalleryMsContext db) =>
{
    var art = db.Arts.Find(id);
    if (art == null) return Results.NotFound();

    art.Title = updatedArt.Title;
    art.Price = updatedArt.Price;
    db.SaveChanges();

    return Results.Ok(art);
});

app.MapDelete("/arts/{id}", (int id, ArtgalleryMsContext db) =>
{
    var art = db.Arts.Find(id);
    if (art == null) return Results.NotFound();

    db.Arts.Remove(art);
    db.SaveChanges();

    return Results.NoContent();
});

// API Routes for Exhibitions
app.MapPost("/exhibitions", (Exhibition exhibition, ArtgalleryMsContext db) =>
{
    db.Exhibitions.Add(exhibition);
    db.SaveChanges();
    return Results.Created($"/exhibitions/{exhibition.ExhibitionId}", exhibition);
});

app.MapGet("/exhibitions", (ArtgalleryMsContext db) => db.Exhibitions.ToList());

//app.MapGet("/exhibitions/{id}", (int id) => API_Functions.GetExhibitionDetails(id));

// API Routes for Exhibition Details
app.MapGet("/exhibitions/{id}/details", (int id) =>
{
    var exhibitionDetails = API_Functions.GetExhibitionDetails(id);
    if (exhibitionDetails == null)
    {
        return Results.NotFound($"Exhibition with ID {id} not found.");
    }
    return Results.Ok(exhibitionDetails);
});


app.MapPut("/exhibitions/{id}", (int id, Exhibition updatedExhibition, ArtgalleryMsContext db) =>
{
    var exhibition = db.Exhibitions.Find(id);
    if (exhibition == null) return Results.NotFound();

    exhibition.Name = updatedExhibition.Name;
    exhibition.EndDate = updatedExhibition.EndDate;
    exhibition.Location = updatedExhibition.Location;
    db.SaveChanges();

    return Results.Ok(exhibition);
});

app.MapDelete("/exhibitions/{id}", (int id, ArtgalleryMsContext db) =>
{
    var exhibition = db.Exhibitions.Find(id);
    if (exhibition == null) return Results.NotFound();

    db.Exhibitions.Remove(exhibition);
    db.SaveChanges();

    return Results.NoContent();
});  

app.Run();

