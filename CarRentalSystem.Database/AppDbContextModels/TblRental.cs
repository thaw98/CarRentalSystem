using System;
using System.Collections.Generic;

namespace CarRentalSystem.Database.AppDbContextModels;

public partial class TblRental
{
    public int RentalId { get; set; }

    public int CustomerId { get; set; }

    public int CarId { get; set; }

    public DateTime RentalDate { get; set; }

    public DateTime? ReturnDate { get; set; }

    public decimal TotalAmount { get; set; }

    public string Status { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public bool IsDelete { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? ModifiedDateTime { get; set; }

    public string? ModifiedBy { get; set; }

    public virtual TblCar Car { get; set; } = null!;

    public virtual TblCustomer Customer { get; set; } = null!;

    public virtual ICollection<TblPayment> TblPayments { get; set; } = new List<TblPayment>();
}
