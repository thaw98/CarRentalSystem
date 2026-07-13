using CarRentalSystem.App.Services;
using CarRentalSystem.Domain.Features.Rental.Models;
using Microsoft.AspNetCore.Components;

namespace CarRentalSystem.App.Components.Features.Rental;

public partial class RentalDelete
{
    [Inject]
    private ApiService ApiService { get; set; } = default!;

    [Inject]
    private NavigationManager Navigation { get; set; } = default!;

    [Parameter]
    public int Id { get; set; }

    private RentalModel? rental;
    private bool isLoading = true;
    private bool isDeleting;
    private string error = string.Empty;

    protected override async Task OnInitializedAsync()
    {
        try
        {
            var result = await ApiService.GetRental(Id);

            if (result.IsSuccess)
            {
                rental = result.Data;
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
            var result = await ApiService.DeleteRental(Id);

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
            isDeleting = false;
        }
    }
}
