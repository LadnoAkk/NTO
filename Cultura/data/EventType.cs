using System;
using System.Collections.Generic;

namespace Cultura.data;

public partial class EventType
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Event> Events { get; } = new List<Event>();
}
