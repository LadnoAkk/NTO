using System;
using System.Collections.Generic;

namespace Cultura.data;

public partial class Application
{
    public long Id { get; set; }

    public string Date { get; set; } = null!;

    public long EventId { get; set; }

    public long WorkId { get; set; }

    public long SpaceId { get; set; }

    public string Timing { get; set; } = null!;

    public string Description { get; set; } = null!;

    public long StatusId { get; set; }

    public virtual Event Event { get; set; } = null!;

    public virtual Space Space { get; set; } = null!;

    public virtual Status Status { get; set; } = null!;

    public virtual WorkType Work { get; set; } = null!;
}
