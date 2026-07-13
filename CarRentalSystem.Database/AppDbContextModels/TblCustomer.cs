using System;
using System.Collections.Generic;

namespace CarRentalSystem.Database.AppDbContextModels;

public partial class TblCustomer
{
    public int CustomerId { get; set; }

    public string Name { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string? Email { get; set; }

    public string? Nrc { get; set; }

    public string? Address { get; set; }

    public string LicenseNumber { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public bool IsDelete { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? ModifiedDateTime { get; set; }

    public string? ModifiedBy { get; set; }

    public virtual ICollection<TblRental> TblRentals { get; set; } = new List<TblRental>();
}
