using System;
using System.Collections.Generic;

namespace SmartTravelAI.Models;

public partial class UserTripRoute
{
    public long Id { get; set; }

    public long TripId { get; set; }

    public string? Geometry { get; set; }

    public double? Distance { get; set; }

    public double? Duration { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual UserTrip Trip { get; set; } = null!;
}
