using Microsoft.EntityFrameworkCore;
using CarRentalSystem.Database.AppDbContextModels;
using CarRentalSystem.Domain;
using CarRentalSystem.Domain.Features.Customer.Models;

namespace CarRentalSystem.Domain.Features.Customer;

public class CustomerService
{
    private readonly AppDbContext _db;

    public CustomerService(AppDbContext db)
    {
        _db = db;
    }

    public Result<CustomerListResponseModel> GetList(CustomerListRequestModel request)
    {
        try
        {
            request.PageNo = request.PageNo <= 0 ? 1 : request.PageNo;
            request.PageSize = request.PageSize <= 0 ? 10 : request.PageSize;

            var customers = _db.TblCustomers
                .AsNoTracking()
                .Where(x => x.IsDelete == false)
                .OrderByDescending(x => x.CustomerId)
                .Skip((request.PageNo - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToList();

            return new Result<CustomerListResponseModel>
            {
                IsSuccess = true,
                Message = "Customers retrieved successfully.",
                Data = new CustomerListResponseModel
                {
                    Customers = customers.Select(x => new CustomerModel
                    {
                        CustomerId = x.CustomerId,
                        Name = x.Name,
                        Phone = x.Phone,
                        Email = x.Email,
                        Nrc = x.Nrc,
                        Address = x.Address,
                        LicenseNumber = x.LicenseNumber,
                        CreatedAt = x.CreatedAt
                    }).ToList()
                }
            };
        }
        catch (Exception ex)
        {
            return new Result<CustomerListResponseModel>
            {
                IsSuccess = false,
                Message = ex.ToString()
            };
        }
    }

    public Result<CustomerModel> GetById(int id)
    {
        var customer = _db.TblCustomers
            .AsNoTracking()
            .FirstOrDefault(x => x.CustomerId == id && x.IsDelete == false);

        if (customer is null)
        {
            return new Result<CustomerModel>
            {
                IsSuccess = false,
                Message = "Customer not found."
            };
        }

        return new Result<CustomerModel>
        {
            IsSuccess = true,
            Message = "Customer retrieved successfully.",
            Data = new CustomerModel
            {
                CustomerId = customer.CustomerId,
                Name = customer.Name,
                Phone = customer.Phone,
                Email = customer.Email,
                Nrc = customer.Nrc,
                Address = customer.Address,
                LicenseNumber = customer.LicenseNumber,
                CreatedAt = customer.CreatedAt
            }
        };
    }

    public Result<CustomerModel> Create(CustomerCreateRequestModel request)
    {
        var customer = new TblCustomer
        {
            Name = request.Name,
            Phone = request.Phone,
            Email = request.Email,
            Nrc = request.Nrc,
            Address = request.Address,
            LicenseNumber = request.LicenseNumber,
            CreatedAt = DateTime.Now,
            IsDelete = false
        };

        _db.TblCustomers.Add(customer);
        _db.SaveChanges();

        return new Result<CustomerModel>
        {
            IsSuccess = true,
            Message = "Customer created successfully."
        };
    }

    public Result<CustomerModel> Update(int id, CustomerUpdateRequestModel request)
    {
        var customer = _db.TblCustomers
            .FirstOrDefault(x => x.CustomerId == id && x.IsDelete == false);

        if (customer is null)
        {
            return new Result<CustomerModel>
            {
                IsSuccess = false,
                Message = "Customer not found."
            };
        }

        customer.Name = request.Name;
        customer.Phone = request.Phone;
        customer.Email = request.Email;
        customer.Nrc = request.Nrc;
        customer.Address = request.Address;
        customer.LicenseNumber = request.LicenseNumber;
        customer.ModifiedDateTime = DateTime.Now;

        _db.SaveChanges();

        return new Result<CustomerModel>
        {
            IsSuccess = true,
            Message = "Customer updated successfully."
        };
    }

    public Result<CustomerModel> Delete(int id)
    {
        var customer = _db.TblCustomers
            .FirstOrDefault(x => x.CustomerId == id && x.IsDelete == false);

        if (customer is null)
        {
            return new Result<CustomerModel>
            {
                IsSuccess = false,
                Message = "Customer not found."
            };
        }

        customer.IsDelete = true;
        customer.ModifiedDateTime = DateTime.Now;

        _db.SaveChanges();

        return new Result<CustomerModel>
        {
            IsSuccess = true,
            Message = "Customer deleted successfully."
        };
    }
}
