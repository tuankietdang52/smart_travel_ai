using System;
using System.Collections.Generic;

namespace SmartTravelAI.Models;

public partial class UserTrip
{
    public long Id { get; set; }

    public long UserId { get; set; }

    public string Name { get; set; } = null!;

    public double? TotalDistance { get; set; }

    public double? TotalDuration { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual User User { get; set; } = null!;

    public virtual ICollection<UserTripRoute> UserTripRoutes { get; set; } = new List<UserTripRoute>();

    public virtual ICollection<UserTripWaypoint> UserTripWaypoints { get; set; } = new List<UserTripWaypoint>();
}
