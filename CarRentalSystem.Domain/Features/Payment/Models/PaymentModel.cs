namespace CarRentalSystem.Domain.Features.Payment.Models;

public class PaymentListRequestModel
{
    public int PageNo { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}

public class PaymentListResponseModel
{
    public List<PaymentModel> Payments { get; set; } = new List<PaymentModel>();
}

public class PaymentModel
{
    public int PaymentId { get; set; }
    public int RentalId { get; set; }
    public DateTime PaymentDate { get; set; }
    public decimal Amount { get; set; }
    public string PaymentMethod { get; set; } = null!;
    public string PaymentStatus { get; set; } = null!;
}

public class PaymentCreateRequestModel
{
    public int RentalId { get; set; }
    public DateTime PaymentDate { get; set; } = DateTime.Now;
    public decimal Amount { get; set; }
    public string PaymentMethod { get; set; } = null!;
    public string PaymentStatus { get; set; } = "Pending";
}

public class PaymentUpdateRequestModel
{
    public int RentalId { get; set; }
    public DateTime PaymentDate { get; set; }
    public decimal Amount { get; set; }
    public string PaymentMethod { get; set; } = null!;
    public string PaymentStatus { get; set; } = "Pending";
}