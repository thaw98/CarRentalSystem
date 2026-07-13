using CarRentalSystem.App.Services;
using CarRentalSystem.Domain.Features.Car.Models;
using Microsoft.AspNetCore.Components;

namespace CarRentalSystem.App.Components.Features.Car;

public partial class CarDelete
{
    [Inject]
    private ApiService ApiService { get; set; } = default!;

    [Inject]
    private NavigationManager Navigation { get; set; } = default!;

    [Parameter]
    public int Id { get; set; }

    private CarModel? car;
    private bool isLoading = true;
    private bool isDeleting;
    private string error = string.Empty;

    protected override async Task OnInitializedAsync()
    {
        try
        {
            var result = await ApiService.GetCar(Id);

            if (result.IsSuccess)
            {
                car = result.Data;
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

    private async Task Delete()
    {
        isDeleting = true;

        try
        {
            var result = await ApiService.DeleteCar(Id);

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
            isDeleting = false;
        }
    }
}
