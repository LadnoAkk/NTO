using System;
using System.Collections.Generic;

namespace Cultura.data;

public partial class Reservation
{
    public long Id { get; set; }

    public string CreateDate { get; set; } = null!;

    public long EventId { get; set; }

    public string BeginningDate { get; set; } = null!;

    public string BeginningTime { get; set; } = null!;

    public string EndingDate { get; set; } = null!;

    public string EndingTime { get; set; } = null!;

    public long SpaceId { get; set; }

    public string? Comments { get; set; }

    public virtual Event Event { get; set; } = null!;

    public virtual Space Space { get; set; } = null!;
}
