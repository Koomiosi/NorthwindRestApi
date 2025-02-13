using System;
using System.Collections.Generic;

namespace NorthwindRestApi.Models;

public partial class TilausSummat
{
    public int OrderId { get; set; }

    public decimal? Summat { get; set; }
}
