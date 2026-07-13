namespace CarRentalSystem.Domain.Features.Customer.Models;

public class CustomerListRequestModel
{
    public int PageNo { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}

public class CustomerListResponseModel
{
    public List<CustomerModel> Customers { get; set; } = new List<CustomerModel>();
}

public class CustomerModel
{
    public int CustomerId { get; set; }
    public string Name { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public string? Email { get; set; }
    public string? Nrc { get; set; }
    public string? Address { get; set; }
    public string LicenseNumber { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
}

public class CustomerCreateRequestModel
{
    public string Name { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public string? Email { get; set; }
    public string? Nrc { get; set; }
    public string? Address { get; set; }
    public string LicenseNumber { get; set; } = null!;
}

public class CustomerUpdateRequestModel
{
    public string Name { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public string? Email { get; set; }
    public string? Nrc { get; set; }
    public string? Address { get; set; }
    public string LicenseNumber { get; set; } = null!;
}