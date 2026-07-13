using CarRentalSystem.App.Services;
using CarRentalSystem.Domain.Features.Rental.Models;
using Microsoft.AspNetCore.Components;

namespace CarRentalSystem.App.Components.Features.Rental;

public partial class RentalList
{
    [Inject]
    private ApiService ApiService { get; set; } = default!;

    private List<RentalModel> rentals = [];
    private bool isLoading = true;
    private string error = string.Empty;

    protected override async Task OnInitializedAsync()
    {
        try
        {
            var result = await ApiService.GetRentals(
                new RentalListRequestModel
                {
                    PageNo = 1,
                    PageSize = 100
                });

            if (result.IsSuccess)
            {
                rentals = result.Data?.Rentals ?? [];
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
        return status switch
        {
            "Active" => "badge bg-primary",
            "Completed" => "badge bg-success",
            "Cancelled" => "badge bg-danger",
            _ => "badge bg-secondary"
        };
    }
}
