using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
namespace visualbasicproject;

public partial class Aartist
{
    public int ArtistId { get; set; }

    public string? Name { get; set; }

    public string? Contact { get; set; }

    public string? Nationality { get; set; }
   
    [JsonIgnore] 
    public virtual ICollection<Art> Arts { get; set; } = new List<Art>();
    [JsonIgnore] 
    public virtual ICollection<Exhibition> Exhibitions { get; set; } = new List<Exhibition>();
}
