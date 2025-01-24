using System;
using System.Collections.Generic;
namespace visualbasicproject;

public partial class Art
{
    public int ArtId { get; set; }

    public int ArtistId { get; set; }

    public string? Title { get; set; }

    public DateOnly? YearCreated { get; set; }

    public int? Price { get; set; }

    public virtual Aartist Artist { get; set; } = null!;
}
