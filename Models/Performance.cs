using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplicationOperaLublin.Models;

[Table("performance")]
public class Performance : Entity
{
    public string Title { get; set; } = null!;

    public int Genre { get; set; }

    public int Place { get; set; }

    public TimeOnly Duration { get; set; }

    public int BreaksCount { get; set; }

    public string Description { get; set; } = null!;

    public byte[] MainImage { get; set; } = null!;
    //public int? BackgroundPositionY { get; set; } = 70;

    public byte[]? Poster { get; set; }

    public DateTime? PremiereDate { get; set; }

    public bool Active { get; set; } = true;

    public virtual Genre GenreNavigation { get; set; } = null!;

    public virtual Place PlaceNavigation { get; set; } = null!;

    public ICollection<PerformanceImplementer> PerformanceImplementers { get; set; } =
        new List<PerformanceImplementer>();

    public ICollection<PerformanceEvent> PerformanceEvents { get; set; } = new List<PerformanceEvent>();

    public ICollection<PublicComment> PublicComments { get; set; } = new List<PublicComment>();

    public ICollection<PerformancePhoto> PerformancePhotos { get; set; } = new List<PerformancePhoto>();
    public MainPageBackground? MainPageBackground { get; set; }
}