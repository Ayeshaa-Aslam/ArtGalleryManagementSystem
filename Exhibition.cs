using System;
using System.Collections.Generic;

namespace visualbasicproject;

public partial class Exhibition
{
    public int ExhibitionId { get; set; }

    public int ArtId { get; set; }

    public int ArtistId { get; set; }

    public string? Name { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public string? Location { get; set; }

    public virtual Aartist Artist { get; set; } = null!;
}
