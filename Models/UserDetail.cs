using System;
using System.Collections.Generic;

namespace CarRentals.Models;

public class UserDetail
{
    public int UserId { get; set; }

    public string? UserName { get; set; }

    public string? Gender { get; set; }

    public string? LicenseNumber { get; set; }

    public string? Email { get; set; }

    public string? Address { get; set; }

    public bool? Admin { get; set; }

    public string? Password { get; set; }

    public virtual ICollection<BookingHistory> BookingHistories { get; } = new List<BookingHistory>();

}
