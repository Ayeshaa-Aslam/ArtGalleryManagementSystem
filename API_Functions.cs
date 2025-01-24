
using Microsoft.EntityFrameworkCore;
namespace visualbasicproject;
public static class API_Functions
{

    // The ServiceProvider is initialized with dependency injection.
    //It provides access to services like the ArtgalleryMsContext during runtime.
    private static IServiceProvider? ServiceProvider { get; set; }


    public static void Initialize(IServiceProvider serviceProvider)
    {
        ServiceProvider = serviceProvider;
    }

    private static ArtgalleryMsContext GetContext()
    {
        if (ServiceProvider == null)
        {
            throw new InvalidOperationException("ServiceProvider is not initialized.");
        }
        var scope = ServiceProvider.CreateScope();
        return scope.ServiceProvider.GetRequiredService<ArtgalleryMsContext>();
    }

    // Artist Methods
    public static IResult AddArtist(Aartist artist)
    {
        try
        {
            using var context = GetContext();
            context.Aartists.Add(artist);
            context.SaveChanges();
            return Results.Created($"/artists/{artist.ArtistId}", artist);
        }
        catch (Exception ex)
        {
            return Results.BadRequest($"Failed to add artist: {ex.Message}");
        }
    }

    public static IResult GetArtists()
    {
        try
        {
            using var context = GetContext();
            var artists = context.Aartists.ToList();
            return Results.Ok(artists);
        }
        catch (Exception ex)
        {
            return Results.BadRequest($"Failed to get artists: {ex.Message}");
        }
    }

    public static IResult UpdateArtist(int artistId, Aartist updatedArtist)
    {
        try
        {
            using var context = GetContext();
            if (artistId != updatedArtist.ArtistId)
                return Results.BadRequest("Artist ID mismatch");

            var existingArtist = context.Aartists.AsNoTracking().FirstOrDefault(a => a.ArtistId == artistId);
            if (existingArtist == null)
                return Results.NotFound("Artist not found");

            context.Aartists.Attach(updatedArtist);
            context.Entry(updatedArtist).State = EntityState.Modified;
            context.SaveChanges();
            return Results.NoContent();
        }
        catch (Exception ex)
        {
            return Results.BadRequest($"Failed to update artist: {ex.Message}");
        }
    }

    public static IResult DeleteArtist(int artistId)
    {
        try
        {
            using var context = GetContext();
            var artist = context.Aartists.Find(artistId);
            if (artist == null)
                return Results.NotFound("Artist not found");

            context.Aartists.Remove(artist);
            context.SaveChanges();
            return Results.NoContent();
        }
        catch (Exception ex)
        {
            return Results.BadRequest($"Failed to delete artist: {ex.Message}");
        }
    }

    // Art Methods
   public static IResult AddArt(Art art)
    {
        try
        {
            using var context = GetContext();
            context.Arts.Add(art);
            context.SaveChanges();
            return Results.Created($"/arts/{art.ArtId}", art);
        }
        catch (Exception ex)
        {
            return Results.BadRequest($"Failed to add art: {ex.Message}");
        }
    }

    public static IResult GetArts()
    {
        try
        {
            using var context = GetContext();
            var arts = context.Arts.ToList();
            return Results.Ok(arts);
        }
        catch (Exception ex)
        {
            return Results.BadRequest($"Failed to get arts: {ex.Message}");
        }
    }

    public static IResult UpdateArt(int artId, Art updatedArt)
    {
        try
        {
            using var context = GetContext();
            if (artId != updatedArt.ArtId)
                return Results.BadRequest("Art ID mismatch");

            var existingArt = context.Arts.AsNoTracking().FirstOrDefault(a => a.ArtId == artId);
            if (existingArt == null)
                return Results.NotFound("Art not found");

            context.Arts.Attach(updatedArt);
            context.Entry(updatedArt).State = EntityState.Modified;
            context.SaveChanges();
            return Results.NoContent();
        }
        catch (Exception ex)
        {
            return Results.BadRequest($"Failed to update art: {ex.Message}");
        }
    }

    public static IResult DeleteArt(int artId)
    {
        try
        {
            using var context = GetContext();
            var art = context.Arts.Find(artId);
            if (art == null)
                return Results.NotFound("Art not found");

            context.Arts.Remove(art);
            context.SaveChanges();
            return Results.NoContent();
        }
        catch (Exception ex)
        {
            return Results.BadRequest($"Failed to delete art: {ex.Message}");
        }
    }

    // Exhibition Methods
    public static IResult AddExhibition(Exhibition exhibition)
    {
        try
        {
            using var context = GetContext();
            context.Exhibitions.Add(exhibition);
            context.SaveChanges();
            return Results.Created($"/exhibitions/{exhibition.ExhibitionId}", exhibition);
        }
        catch (Exception ex)
        {
            return Results.BadRequest($"Failed to add exhibition: {ex.Message}");
        }
    }

    public static IResult GetExhibitions()
    {
        try
        {
            using var context = GetContext();
            var exhibitions = context.Exhibitions.ToList();
            return Results.Ok(exhibitions);
        }
        catch (Exception ex)
        {
            return Results.BadRequest($"Failed to get exhibitions: {ex.Message}");
        }
    }

    public static IResult UpdateExhibition(int exhibitionId, Exhibition updatedExhibition)
    {
        try
        {
            using var context = GetContext();
            if (exhibitionId != updatedExhibition.ExhibitionId)
                return Results.BadRequest("Exhibition ID mismatch");

            var existingExhibition = context.Exhibitions.AsNoTracking().FirstOrDefault(e => e.ExhibitionId == exhibitionId);
            if (existingExhibition == null)
                return Results.NotFound("Exhibition not found");

            context.Exhibitions.Attach(updatedExhibition);
            context.Entry(updatedExhibition).State = EntityState.Modified;
            context.SaveChanges();
            return Results.NoContent();
        }
        catch (Exception ex)
        {
            return Results.BadRequest($"Failed to update exhibition: {ex.Message}");
        }
    }

    public static IResult DeleteExhibition(int exhibitionId)
    {
        try
        {
            using var context = GetContext();
            var exhibition = context.Exhibitions.Find(exhibitionId);
            if (exhibition == null)
                return Results.NotFound("Exhibition not found");

            context.Exhibitions.Remove(exhibition);
            context.SaveChanges();
            return Results.NoContent();
        }
        catch (Exception ex)
        {
            return Results.BadRequest($"Failed to delete exhibition: {ex.Message}");
        }
    } 

    //create a function that fetch exhiition with art and artist details


    public static object GetExhibitionDetails(int exhibitionId)
{
    using (var context = new ArtgalleryMsContext())
    {
        var exhibition = context.Exhibitions
            .Select(e => new
            {
                ExhibitionDetails = e, // Include all details of the Exhibition
                ArtDetails = context.Arts.FirstOrDefault(a => a.ArtId == e.ArtId), // Fetch Art details
                ArtistDetails = context.Aartists.FirstOrDefault(ar => ar.ArtistId == e.ArtistId) // Fetch Artist details
            })
            .FirstOrDefault(e => e.ExhibitionDetails.ExhibitionId == exhibitionId); // Filter by dynamic ID

        return exhibition;
    }
}

}
