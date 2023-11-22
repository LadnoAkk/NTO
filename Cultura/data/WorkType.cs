using System;
using System.Collections.Generic;

namespace Cultura.data;

public partial class WorkType
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Application> Applications { get; } = new List<Application>();
}
