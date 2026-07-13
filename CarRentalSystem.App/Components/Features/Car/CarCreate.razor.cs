using CarRentalSystem.App.Services;
using CarRentalSystem.Domain.Features.Car.Models;
using Microsoft.AspNetCore.Components;

namespace CarRentalSystem.App.Components.Features.Car;

public partial class CarCreate
{
    [Inject]
    private ApiService ApiService { get; set; } = default!;

    [Inject]
    private NavigationManager Navigation { get; set; } = default!;

    private CarCreateRequestModel model = new()
    {
        PlateNumber = string.Empty,
        Brand = string.Empty,
        Model = string.Empty,
        Year = DateTime.Now.Year,
        Status = "Available"
    };

    private bool isSaving;
    private string error = string.Empty;

    private async Task Save()
    {
        error = string.Empty;

        if (string.IsNullOrWhiteSpace(model.PlateNumber) ||
            string.IsNullOrWhiteSpace(model.Brand) ||
            string.IsNullOrWhiteSpace(model.Model))
        {
            error = "Plate number, brand and model are required.";
            return;
        }

        if (model.Year < 1900 ||
            model.Year > DateTime.Now.Year + 1)
        {
            error = "Please enter a valid car year.";
            return;
        }

        if (model.PricePerDay <= 0)
        {
            error = "Price per day must be greater than zero.";
            return;
        }

        isSaving = true;

        try
        {
            var result = await ApiService.CreateCar(model);

            if (result.IsSuccess)
            {
                Navigation.NavigateTo("/cars");
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
