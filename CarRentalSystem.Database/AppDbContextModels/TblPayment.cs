using System;
using System.Collections.Generic;

namespace CarRentalSystem.Database.AppDbContextModels;

public partial class TblPayment
{
    public int PaymentId { get; set; }

    public int RentalId { get; set; }

    public DateTime PaymentDate { get; set; }

    public decimal Amount { get; set; }

    public string PaymentMethod { get; set; } = null!;

    public string PaymentStatus { get; set; } = null!;

    public bool IsDelete { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? ModifiedDateTime { get; set; }

    public string? ModifiedBy { get; set; }

    public virtual TblRental Rental { get; set; } = null!;
}
