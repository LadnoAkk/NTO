using System;
using System.Collections.Generic;

namespace Cultura.data;

public partial class Teacher
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Circle> Circles { get; } = new List<Circle>();
}
