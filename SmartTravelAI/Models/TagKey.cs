using System;
using System.Collections.Generic;

namespace SmartTravelAI.Models;

public partial class TagKey
{
    public long Id { get; set; }

    public string KeyName { get; set; } = null!;

    public virtual ICollection<TagValue> TagValues { get; set; } = new List<TagValue>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
