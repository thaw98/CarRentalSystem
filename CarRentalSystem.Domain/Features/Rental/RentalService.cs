using Microsoft.EntityFrameworkCore;
using CarRentalSystem.Database.AppDbContextModels;
using CarRentalSystem.Domain;
using CarRentalSystem.Domain.Features.Rental.Models;

namespace CarRentalSystem.Domain.Features.Rental;

public class RentalService
{
    private readonly AppDbContext _db;

    public RentalService(AppDbContext db)
    {
        _db = db;
    }

    public Result<RentalListResponseModel> GetList(RentalListRequestModel request)
    {
        try
        {
            request.PageNo = request.PageNo <= 0 ? 1 : request.PageNo;
            request.PageSize = request.PageSize <= 0 ? 10 : request.PageSize;

            var rentals = _db.TblRentals
                .AsNoTracking()
                .Where(x => x.IsDelete == false)
                .OrderByDescending(x => x.RentalId)
                .Skip((request.PageNo - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new RentalModel
                {
                    RentalId = x.RentalId,
                    CustomerId = x.CustomerId,
                    CustomerName = x.Customer.Name,
                    CarId = x.CarId,
                    PlateNumber = x.Car.PlateNumber,
                    RentalDate = x.RentalDate,
                    ReturnDate = x.ReturnDate,
                    TotalAmount = x.TotalAmount,
                    Status = x.Status,
                    CreatedAt = x.CreatedAt
                })
                .ToList();

            return new Result<RentalListResponseModel>
            {
                IsSuccess = true,
                Message = "Rentals retrieved successfully.",
                Data = new RentalListResponseModel
                {
                    Rentals = rentals
                }
            };
        }
        catch (Exception ex)
        {
            return new Result<RentalListResponseModel>
            {
                IsSuccess = false,
                Message = ex.ToString()
            };
        }
    }

    public Result<RentalModel> GetById(int id)
    {
        var rental = _db.TblRentals
            .AsNoTracking()
            .Where(x => x.RentalId == id && x.IsDelete == false)
            .Select(x => new RentalModel
            {
                RentalId = x.RentalId,
                CustomerId = x.CustomerId,
                CustomerName = x.Customer.Name,
                CarId = x.CarId,
                PlateNumber = x.Car.PlateNumber,
                RentalDate = x.RentalDate,
                ReturnDate = x.ReturnDate,
                TotalAmount = x.TotalAmount,
                Status = x.Status,
                CreatedAt = x.CreatedAt
            })
            .FirstOrDefault();

        if (rental is null)
        {
            return new Result<RentalModel>
            {
                IsSuccess = false,
                Message = "Rental not found."
            };
        }

        return new Result<RentalModel>
        {
            IsSuccess = true,
            Message = "Rental retrieved successfully.",
            Data = rental
        };
    }

    public Result<RentalModel> Create(RentalCreateRequestModel request)
    {
        var customer = _db.TblCustomers
            .FirstOrDefault(x => x.CustomerId == request.CustomerId && x.IsDelete == false);

        if (customer is null)
        {
            return new Result<RentalModel>
            {
                IsSuccess = false,
                Message = "Customer not found."
            };
        }

        var car = _db.TblCars
            .FirstOrDefault(x => x.CarId == request.CarId && x.IsDelete == false);

        if (car is null)
        {
            return new Result<RentalModel>
            {
                IsSuccess = false,
                Message = "Car not found."
            };
        }

        var rental = new TblRental
        {
            CustomerId = request.CustomerId,
            CarId = request.CarId,
            RentalDate = request.RentalDate,
            ReturnDate = request.ReturnDate,
            TotalAmount = request.TotalAmount,
            Status = request.Status,
            CreatedAt = DateTime.Now,
            IsDelete = false
        };

        _db.TblRentals.Add(rental);

        if (request.Status == "Active")
        {
            car.Status = "Rented";
        }

        _db.SaveChanges();

        return new Result<RentalModel>
        {
            IsSuccess = true,
            Message = "Rental created successfully."
        };
    }

    public Result<RentalModel> Update(int id, RentalUpdateRequestModel request)
    {
        var rental = _db.TblRentals
            .FirstOrDefault(x => x.RentalId == id && x.IsDelete == false);

        if (rental is null)
        {
            return new Result<RentalModel>
            {
                IsSuccess = false,
                Message = "Rental not found."
            };
        }

        rental.CustomerId = request.CustomerId;
        rental.CarId = request.CarId;
        rental.RentalDate = request.RentalDate;
        rental.ReturnDate = request.ReturnDate;
        rental.TotalAmount = request.TotalAmount;
        rental.Status = request.Status;
        rental.ModifiedDateTime = DateTime.Now;

        var car = _db.TblCars.FirstOrDefault(x => x.CarId == request.CarId);

        if (car is not null)
        {
            if (request.Status == "Active")
            {
                car.Status = "Rented";
            }
            else if (request.Status == "Completed" || request.Status == "Cancelled")
            {
                car.Status = "Available";
            }
        }

        _db.SaveChanges();

        return new Result<RentalModel>
        {
            IsSuccess = true,
            Message = "Rental updated successfully."
        };
    }

    public Result<RentalModel> Delete(int id)
    {
        var rental = _db.TblRentals
            .FirstOrDefault(x => x.RentalId == id && x.IsDelete == false);

        if (rental is null)
        {
            return new Result<RentalModel>
            {
                IsSuccess = false,
                Message = "Rental not found."
            };
        }

        rental.IsDelete = true;
        rental.ModifiedDateTime = DateTime.Now;

        var car = _db.TblCars.FirstOrDefault(x => x.CarId == rental.CarId);

        if (car is not null)
        {
            car.Status = "Available";
        }

        _db.SaveChanges();

        return new Result<RentalModel>
        {
            IsSuccess = true,
            Message = "Rental deleted successfully."
        };
    }
}