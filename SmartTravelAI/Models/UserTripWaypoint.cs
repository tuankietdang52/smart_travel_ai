using System;
using System.Collections.Generic;

namespace SmartTravelAI.Models;

public partial class UserTripWaypoint
{
    public long Id { get; set; }

    public long TripId { get; set; }

    public int Index { get; set; }

    public long? TagValue { get; set; }

    public string? Name { get; set; }

    public double Lat { get; set; }

    public double Lon { get; set; }

    public DateTime? ArriveAt { get; set; }

    public DateTime? LeaveAt { get; set; }

    public string? Note { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual TagValue? TagValueNavigation { get; set; }

    public virtual UserTrip Trip { get; set; } = null!;
}
