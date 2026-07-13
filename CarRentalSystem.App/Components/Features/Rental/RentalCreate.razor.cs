using CarRentalSystem.App.Services;
using CarRentalSystem.Domain.Features.Car.Models;
using CarRentalSystem.Domain.Features.Customer.Models;
using CarRentalSystem.Domain.Features.Rental.Models;
using Microsoft.AspNetCore.Components;

namespace CarRentalSystem.App.Components.Features.Rental;

public partial class RentalCreate
{
    [Inject]
    private ApiService ApiService { get; set; } = default!;

    [Inject]
    private NavigationManager Navigation { get; set; } = default!;

    private List<CustomerModel> customers = [];
    private List<CarModel> cars = [];

    private RentalCreateRequestModel model = new()
    {
        RentalDate = DateTime.Now,
        Status = "Active"
    };

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

            customers = customerResult.Data?.Customers ?? [];

            cars = carResult.Data?.Cars
                .Where(x => x.Status == "Available")
                .ToList() ?? [];
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

        if (model.CustomerId <= 0)
        {
            error = "Please select a customer.";
            return;
        }

        if (model.CarId <= 0)
        {
            error = "Please select a car.";
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

        model.Status = "Active";
        isSaving = true;

        try
        {
            var result = await ApiService.CreateRental(model);

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
