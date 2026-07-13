using Microsoft.EntityFrameworkCore;
using CarRentalSystem.Database.AppDbContextModels;
using CarRentalSystem.Domain;
using CarRentalSystem.Domain.Features.Car.Models;

namespace CarRentalSystem.Domain.Features.Car;

public class CarService
{
    private readonly AppDbContext _db;

    public CarService(AppDbContext db)
    {
        _db = db;
    }

    public Result<CarListResponseModel> GetList(CarListRequestModel request)
    {
        try
        {
            request.PageNo = request.PageNo <= 0 ? 1 : request.PageNo;
            request.PageSize = request.PageSize <= 0 ? 10 : request.PageSize;

            var cars = _db.TblCars
                .AsNoTracking()
                .Where(x => x.IsDelete == false)
                .OrderByDescending(x => x.CarId)
                .Skip((request.PageNo - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToList();

            return new Result<CarListResponseModel>
            {
                IsSuccess = true,
                Message = "Cars retrieved successfully.",
                Data = new CarListResponseModel
                {
                    Cars = cars.Select(x => new CarModel
                    {
                        CarId = x.CarId,
                        PlateNumber = x.PlateNumber,
                        Brand = x.Brand,
                        Model = x.Model,
                        Year = x.Year,
                        PricePerDay = x.PricePerDay,
                        Status = x.Status,
                        CreatedAt = x.CreatedAt
                    }).ToList()
                }
            };
        }
        catch (Exception ex)
        {
            return new Result<CarListResponseModel>
            {
                IsSuccess = false,
                Message = ex.ToString()
            };
        }
    }

    public Result<CarModel> GetById(int id)
    {
        var car = _db.TblCars
            .AsNoTracking()
            .FirstOrDefault(x => x.CarId == id && x.IsDelete == false);

        if (car is null)
        {
            return new Result<CarModel>
            {
                IsSuccess = false,
                Message = "Car not found."
            };
        }

        return new Result<CarModel>
        {
            IsSuccess = true,
            Message = "Car retrieved successfully.",
            Data = new CarModel
            {
                CarId = car.CarId,
                PlateNumber = car.PlateNumber,
                Brand = car.Brand,
                Model = car.Model,
                Year = car.Year,
                PricePerDay = car.PricePerDay,
                Status = car.Status,
                CreatedAt = car.CreatedAt
            }
        };
    }

    public Result<CarModel> Create(CarCreateRequestModel request)
    {
        var car = new TblCar
        {
            PlateNumber = request.PlateNumber,
            Brand = request.Brand,
            Model = request.Model,
            Year = request.Year,
            PricePerDay = request.PricePerDay,
            Status = request.Status,
            CreatedAt = DateTime.Now,
            IsDelete = false
        };

        _db.TblCars.Add(car);
        _db.SaveChanges();

        return new Result<CarModel>
        {
            IsSuccess = true,
            Message = "Car created successfully."
        };
    }

    public Result<CarModel> Update(int id, CarUpdateRequestModel request)
    {
        var car = _db.TblCars
            .FirstOrDefault(x => x.CarId == id && x.IsDelete == false);

        if (car is null)
        {
            return new Result<CarModel>
            {
                IsSuccess = false,
                Message = "Car not found."
            };
        }

        car.PlateNumber = request.PlateNumber;
        car.Brand = request.Brand;
        car.Model = request.Model;
        car.Year = request.Year;
        car.PricePerDay = request.PricePerDay;
        car.Status = request.Status;
        car.ModifiedDateTime = DateTime.Now;

        _db.SaveChanges();

        return new Result<CarModel>
        {
            IsSuccess = true,
            Message = "Car updated successfully."
        };
    }

    public Result<CarModel> Delete(int id)
    {
        var car = _db.TblCars
            .FirstOrDefault(x => x.CarId == id && x.IsDelete == false);

        if (car is null)
        {
            return new Result<CarModel>
            {
                IsSuccess = false,
                Message = "Car not found."
            };
        }

        car.IsDelete = true;
        car.ModifiedDateTime = DateTime.Now;

        _db.SaveChanges();

        return new Result<CarModel>
        {
            IsSuccess = true,
            Message = "Car deleted successfully."
        };
    }
}