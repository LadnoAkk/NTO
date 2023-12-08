using System;
using System.Collections.Generic;

namespace Cultura.data;

public partial class Circle
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public string WorkBeginDate { get; set; } = null!;

    public long TypeId { get; set; }

    public long SpaceId { get; set; }

    public long FirstVarDay { get; set; }

    public long? SecondVarDay { get; set; }

    public long? ThirdVarDay { get; set; }

    public string BeginningTime { get; set; } = null!;

    public string EndingTime { get; set; } = null!;

    public long TeacherId { get; set; }

    public virtual Week FirstVarDayNavigation { get; set; } = null!;

    public virtual Week? SecondVarDayNavigation { get; set; }

    public virtual Space Space { get; set; } = null!;

    public virtual Teacher Teacher { get; set; } = null!;

    public virtual Week? ThirdVarDayNavigation { get; set; }

    public virtual CircleType Type { get; set; } = null!;
}
