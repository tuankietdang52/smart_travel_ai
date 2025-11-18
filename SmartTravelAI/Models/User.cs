using System;
using System.Collections.Generic;

namespace SmartTravelAI.Models;

public partial class User
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<UserTrip> UserTrips { get; set; } = new List<UserTrip>();

    public virtual ICollection<TagKey> TagKeys { get; set; } = new List<TagKey>();
}
