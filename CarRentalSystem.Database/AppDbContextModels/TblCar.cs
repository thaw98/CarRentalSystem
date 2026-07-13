using System;
using System.Collections.Generic;

namespace CarRentalSystem.Database.AppDbContextModels;

public partial class TblCar
{
    public int CarId { get; set; }

    public string PlateNumber { get; set; } = null!;

    public string Brand { get; set; } = null!;

    public string Model { get; set; } = null!;

    public int Year { get; set; }

    public decimal PricePerDay { get; set; }

    public string Status { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public bool IsDelete { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? ModifiedDateTime { get; set; }

    public string? ModifiedBy { get; set; }

    public virtual ICollection<TblRental> TblRentals { get; set; } = new List<TblRental>();
}
