using System;
using System.Collections.Generic;

namespace Cultura.data;

public partial class Week
{
    public long Id { get; set; }

    public string Day { get; set; } = null!;

    public virtual ICollection<Circle> CircleFirstVarDayNavigations { get; } = new List<Circle>();

    public virtual ICollection<Circle> CircleSecondVarDayNavigations { get; } = new List<Circle>();

    public virtual ICollection<Circle> CircleThirdVarDayNavigations { get; } = new List<Circle>();
}
