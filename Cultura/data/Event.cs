using System;
using System.Collections.Generic;

namespace Cultura.data;

public partial class Event
{
    public long Id { get; set; }

    public string Data { get; set; } = null!;

    public string? Description { get; set; }

    public long TypeId { get; set; }

    public virtual EventType Type { get; set; } = null!;
}
