namespace CarRentalSystem.Domain.Features.Rental.Models;

public class RentalListRequestModel
{
    public int PageNo { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}

public class RentalListResponseModel
{
    public List<RentalModel> Rentals { get; set; } = new List<RentalModel>();
}

public class RentalModel
{
    public int RentalId { get; set; }
    public int CustomerId { get; set; }
    public string CustomerName { get; set; } = null!;
    public int CarId { get; set; }
    public string PlateNumber { get; set; } = null!;
    public DateTime RentalDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    public decimal TotalAmount { get; set; }
    public string Status { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
}

public class RentalCreateRequestModel
{
    public int CustomerId { get; set; }
    public int CarId { get; set; }
    public DateTime RentalDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    public decimal TotalAmount { get; set; }
    public string Status { get; set; } = "Active";
}

public class RentalUpdateRequestModel
{
    public int CustomerId { get; set; }
    public int CarId { get; set; }
    public DateTime RentalDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    public decimal TotalAmount { get; set; }
    public string Status { get; set; } = "Active";
}