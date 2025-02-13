using System;
using System.Collections.Generic;

namespace NorthwindRestApi.Models;

public partial class TuoteSummat
{
    public int ProductId { get; set; }

    public decimal? Summa { get; set; }
}
