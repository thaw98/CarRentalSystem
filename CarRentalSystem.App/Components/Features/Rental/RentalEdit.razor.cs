using CarRentalSystem.App.Services;
using CarRentalSystem.Domain.Features.Car.Models;
using CarRentalSystem.Domain.Features.Customer.Models;
using CarRentalSystem.Domain.Features.Rental.Models;
using Microsoft.AspNetCore.Components;

namespace CarRentalSystem.App.Components.Features.Rental;

public partial class RentalEdit
{
    [Inject]
    private ApiService ApiService { get; set; } = default!;

    [Inject]
    private NavigationManager Navigation { get; set; } = default!;

    [Parameter]
    public int Id { get; set; }

    private List<CustomerModel> customers = [];
    private List<CarModel> cars = [];

    private RentalUpdateRequestModel model = new()
    {
        Status = "Active"
    };

    private IEnumerable<CarModel> SelectableCars =>
        cars.Where(x =>
            x.Status == "Available" ||
            x.CarId == model.CarId);

    private bool isLoading = true;
    private bool isSaving;
    private string error = string.Empty;

    protected override async Task OnInitializedAsync()
    {
        try
        {
            var customerResult = await ApiService.GetCustomers(
                new CustomerListRequestModel
                {
                    PageNo = 1,
                    PageSize = 1000
                });

            var carResult = await ApiService.GetCars(
                new CarListRequestModel
                {
                    PageNo = 1,
                    PageSize = 1000
                });

            var rentalResult = await ApiService.GetRental(Id);

            if (!customerResult.IsSuccess)
            {
                error = customerResult.Message;
                return;
            }

            if (!carResult.IsSuccess)
            {
                error = carResult.Message;
                return;
            }

            if (!rentalResult.IsSuccess ||
                rentalResult.Data is null)
            {
                error = rentalResult.Message;
                return;
            }

            customers = customerResult.Data?.Customers ?? [];
            cars = carResult.Data?.Cars ?? [];

            model = new RentalUpdateRequestModel
            {
                CustomerId = rentalResult.Data.CustomerId,
                CarId = rentalResult.Data.CarId,
                RentalDate = rentalResult.Data.RentalDate,
                ReturnDate = rentalResult.Data.ReturnDate,
                TotalAmount = rentalResult.Data.TotalAmount,
                Status = rentalResult.Data.Status
            };
        }
        catch (Exception ex)
        {
            error = ex.Message;
        }
        finally
        {
            isLoading = false;
        }
    }

    private async Task Save()
    {
        error = string.Empty;

        if (model.CustomerId <= 0 || model.CarId <= 0)
        {
            error = "Customer and car are required.";
            return;
        }

        if (model.ReturnDate.HasValue &&
            model.ReturnDate.Value < model.RentalDate)
        {
            error = "Return date cannot be before rental date.";
            return;
        }

        if (model.TotalAmount < 0)
        {
            error = "Total amount cannot be negative.";
            return;
        }

        isSaving = true;

        try
        {
            var result = await ApiService.UpdateRental(Id, model);

            if (result.IsSuccess)
            {
                Navigation.NavigateTo("/rentals");
            }
            else
            {
                error = result.Message;
            }
        }
        catch (Exception ex)
        {
            error = ex.Message;
        }
        finally
        {
            isSaving = false;
        }
    }
}
