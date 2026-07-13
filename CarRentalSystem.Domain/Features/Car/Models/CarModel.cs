namespace CarRentalSystem.Domain.Features.Car.Models;

public class CarListRequestModel
{
    public int PageNo { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}

public class CarListResponseModel
{
    public List<CarModel> Cars { get; set; } = new List<CarModel>();
}

public class CarModel
{
    public int CarId { get; set; }
    public string PlateNumber { get; set; } = null!;
    public string Brand { get; set; } = null!;
    public string Model { get; set; } = null!;
    public int Year { get; set; }
    public decimal PricePerDay { get; set; }
    public string Status { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
}

public class CarCreateRequestModel
{
    public string PlateNumber { get; set; } = null!;
    public string Brand { get; set; } = null!;
    public string Model { get; set; } = null!;
    public int Year { get; set; }
    public decimal PricePerDay { get; set; }
    public string Status { get; set; } = "Available";
}

public class CarUpdateRequestModel
{
    public string PlateNumber { get; set; } = null!;
    public string Brand { get; set; } = null!;
    public string Model { get; set; } = null!;
    public int Year { get; set; }
    public decimal PricePerDay { get; set; }
    public string Status { get; set; } = "Available";
}