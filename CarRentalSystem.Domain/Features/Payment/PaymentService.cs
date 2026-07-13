using Microsoft.EntityFrameworkCore;
using CarRentalSystem.Database.AppDbContextModels;
using CarRentalSystem.Domain;
using CarRentalSystem.Domain.Features.Payment.Models;

namespace CarRentalSystem.Domain.Features.Payment;

public class PaymentService
{
    private readonly AppDbContext _db;

    public PaymentService(AppDbContext db)
    {
        _db = db;
    }

    public Result<PaymentListResponseModel> GetList(PaymentListRequestModel request)
    {
        try
        {
            request.PageNo = request.PageNo <= 0 ? 1 : request.PageNo;
            request.PageSize = request.PageSize <= 0 ? 10 : request.PageSize;

            var payments = _db.TblPayments
                .AsNoTracking()
                .Where(x => x.IsDelete == false)
                .OrderByDescending(x => x.PaymentId)
                .Skip((request.PageNo - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToList();

            return new Result<PaymentListResponseModel>
            {
                IsSuccess = true,
                Message = "Payments retrieved successfully.",
                Data = new PaymentListResponseModel
                {
                    Payments = payments.Select(x => new PaymentModel
                    {
                        PaymentId = x.PaymentId,
                        RentalId = x.RentalId,
                        PaymentDate = x.PaymentDate,
                        Amount = x.Amount,
                        PaymentMethod = x.PaymentMethod,
                        PaymentStatus = x.PaymentStatus
                    }).ToList()
                }
            };
        }
        catch (Exception ex)
        {
            return new Result<PaymentListResponseModel>
            {
                IsSuccess = false,
                Message = ex.ToString()
            };
        }
    }

    public Result<PaymentModel> GetById(int id)
    {
        var payment = _db.TblPayments
            .AsNoTracking()
            .FirstOrDefault(x => x.PaymentId == id && x.IsDelete == false);

        if (payment is null)
        {
            return new Result<PaymentModel>
            {
                IsSuccess = false,
                Message = "Payment not found."
            };
        }

        return new Result<PaymentModel>
        {
            IsSuccess = true,
            Message = "Payment retrieved successfully.",
            Data = new PaymentModel
            {
                PaymentId = payment.PaymentId,
                RentalId = payment.RentalId,
                PaymentDate = payment.PaymentDate,
                Amount = payment.Amount,
                PaymentMethod = payment.PaymentMethod,
                PaymentStatus = payment.PaymentStatus
            }
        };
    }

    public Result<PaymentModel> Create(PaymentCreateRequestModel request)
    {
        var rental = _db.TblRentals
            .FirstOrDefault(x => x.RentalId == request.RentalId && x.IsDelete == false);

        if (rental is null)
        {
            return new Result<PaymentModel>
            {
                IsSuccess = false,
                Message = "Rental not found."
            };
        }

        var payment = new TblPayment
        {
            RentalId = request.RentalId,
            PaymentDate = request.PaymentDate,
            Amount = request.Amount,
            PaymentMethod = request.PaymentMethod,
            PaymentStatus = request.PaymentStatus,
            IsDelete = false
        };

        _db.TblPayments.Add(payment);
        _db.SaveChanges();

        return new Result<PaymentModel>
        {
            IsSuccess = true,
            Message = "Payment created successfully."
        };
    }

    public Result<PaymentModel> Update(int id, PaymentUpdateRequestModel request)
    {
        var payment = _db.TblPayments
            .FirstOrDefault(x => x.PaymentId == id && x.IsDelete == false);

        if (payment is null)
        {
            return new Result<PaymentModel>
            {
                IsSuccess = false,
                Message = "Payment not found."
            };
        }

        payment.RentalId = request.RentalId;
        payment.PaymentDate = request.PaymentDate;
        payment.Amount = request.Amount;
        payment.PaymentMethod = request.PaymentMethod;
        payment.PaymentStatus = request.PaymentStatus;
        payment.ModifiedDateTime = DateTime.Now;

        _db.SaveChanges();

        return new Result<PaymentModel>
        {
            IsSuccess = true,
            Message = "Payment updated successfully."
        };
    }

    public Result<PaymentModel> Delete(int id)
    {
        var payment = _db.TblPayments
            .FirstOrDefault(x => x.PaymentId == id && x.IsDelete == false);

        if (payment is null)
        {
            return new Result<PaymentModel>
            {
                IsSuccess = false,
                Message = "Payment not found."
            };
        }

        payment.IsDelete = true;
        payment.ModifiedDateTime = DateTime.Now;

        _db.SaveChanges();

        return new Result<PaymentModel>
        {
            IsSuccess = true,
            Message = "Payment deleted successfully."
        };
    }
}