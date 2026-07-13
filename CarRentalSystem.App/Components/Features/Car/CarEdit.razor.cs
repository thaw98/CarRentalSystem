using CarRentalSystem.App.Services;
using CarRentalSystem.Domain.Features.Car.Models;
using Microsoft.AspNetCore.Components;

namespace CarRentalSystem.App.Components.Features.Car;

public partial class CarEdit
{
    [Inject]
    private ApiService ApiService { get; set; } = default!;

    [Inject]
    private NavigationManager Navigation { get; set; } = default!;

    [Parameter]
    public int Id { get; set; }

    private CarUpdateRequestModel model = new()
    {
        PlateNumber = string.Empty,
        Brand = string.Empty,
        Model = string.Empty,
        Status = "Available"
    };

    private bool isLoading = true;
    private bool isSaving;
    private string error = string.Empty;

    protected override async Task OnInitializedAsync()
    {
        try
        {
            var result = await ApiService.GetCar(Id);

            if (result.IsSuccess && result.Data is not null)
            {
                model = new CarUpdateRequestModel
                {
                    PlateNumber = result.Data.PlateNumber,
                    Brand = result.Data.Brand,
                    Model = result.Data.Model,
                    Year = result.Data.Year,
                    PricePerDay = result.Data.PricePerDay,
                    Status = result.Data.Status
                };
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
            isLoading = false;
        }
    }

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

        if (model.PricePerDay <= 0)
        {
            error = "Price per day must be greater than zero.";
            return;
        }

        isSaving = true;

        try
        {
            var result = await ApiService.UpdateCar(Id, model);

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
