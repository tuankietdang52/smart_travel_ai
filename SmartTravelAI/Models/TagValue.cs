using System;
using System.Collections.Generic;

namespace SmartTravelAI.Models;

public partial class TagValue
{
    public long Id { get; set; }

    public long KeyId { get; set; }

    public string ValueName { get; set; } = null!;

    public virtual TagKey Key { get; set; } = null!;

    public virtual ICollection<UserTripWaypoint> UserTripWaypoints { get; set; } = new List<UserTripWaypoint>();
}
