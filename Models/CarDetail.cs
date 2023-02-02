using System;
using System.Collections.Generic;

namespace CarRentals.Models;

public class CarDetail
{
    public int CarId { get; set; }

    public string? CarName { get; set; }

    public string? CarBrand { get; set; }

    public string? CarType { get; set; }

    public string? ModelYear { get; set; }

    public int? Kilometers { get; set; }

    public string? CarColor { get; set; }

    public int? Amount { get; set; }

    public virtual ICollection<BookingHistory> BookingHistories { get; } = new List<BookingHistory>();

}
