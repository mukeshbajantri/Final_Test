using System;
using System.Collections.Generic;

namespace CarRentals.Models;

public class BookingHistory
{
    public int BookingId { get; set; }

    public int? UserId { get; set; }

    public int? CarId { get; set; }

    public string? BookingDate { get; set; }

    public string? FromDate { get; set; }

    public string? ToDate { get; set; }

    public int? Amount { get; set; }

    public virtual CarDetail? Car { get; set; }

    public virtual UserDetail? User { get; set; }
}
