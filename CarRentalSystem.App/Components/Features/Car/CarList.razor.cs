using CarRentalSystem.App.Services;
using CarRentalSystem.Domain.Features.Car.Models;
using Microsoft.AspNetCore.Components;

namespace CarRentalSystem.App.Components.Features.Car;

public partial class CarList
{
    [Inject]
    private ApiService ApiService { get; set; } = default!;

    private List<CarModel> cars = [];
    private bool isLoading = true;
    private string error = string.Empty;

    protected override async Task OnInitializedAsync()
    {
        try
        {
            var result = await ApiService.GetCars(
                new CarListRequestModel
                {
                    PageNo = 1,
                    PageSize = 100
                });

            if (result.IsSuccess)
            {
                cars = result.Data?.Cars ?? [];
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

    private static string GetStatusClass(string status)
    {
        return status == "Available"
            ? "badge bg-success"
            : "badge bg-danger";
    }
}
