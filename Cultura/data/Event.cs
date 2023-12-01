using System;
using System.Collections.Generic;

namespace Cultura.data;

public partial class Event
{
    public long Id { get; set; }

    public string Data { get; set; } = null!;

    public string? Description { get; set; }

    public long TypeId { get; set; }

    public virtual ICollection<Application> Applications { get; } = new List<Application>();

    public virtual ICollection<Reservation> Reservations { get; } = new List<Reservation>();

    public virtual EventType Type { get; set; } = null!;
}
