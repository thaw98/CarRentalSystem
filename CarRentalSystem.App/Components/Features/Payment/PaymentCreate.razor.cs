using CarRentalSystem.App.Services;
using CarRentalSystem.Domain.Features.Payment.Models;
using CarRentalSystem.Domain.Features.Rental.Models;
using Microsoft.AspNetCore.Components;

namespace CarRentalSystem.App.Components.Features.Payment;

public partial class PaymentCreate
{
    [Inject]
    private ApiService ApiService { get; set; } = default!;

    [Inject]
    private NavigationManager Navigation { get; set; } = default!;

    private List<RentalModel> rentals = [];

    private PaymentCreateRequestModel model = new()
    {
        PaymentDate = DateTime.Now,
        PaymentMethod = string.Empty,
        PaymentStatus = "Pending"
    };

    private bool isLoading = true;
    private bool isSaving;
    private string error = string.Empty;

    protected override async Task OnInitializedAsync()
    {
        try
        {
            var result = await ApiService.GetRentals(
                new RentalListRequestModel
                {
                    PageNo = 1,
                    PageSize = 1000
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

    private async Task Save()
    {
        error = string.Empty;

        if (model.RentalId <= 0)
        {
            error = "Please select a rental.";
            return;
        }

        if (model.Amount <= 0)
        {
            error = "Payment amount must be greater than zero.";
            return;
        }

        if (string.IsNullOrWhiteSpace(model.PaymentMethod))
        {
            error = "Please select a payment method.";
            return;
        }

        isSaving = true;

        try
        {
            var result = await ApiService.CreatePayment(model);

            if (result.IsSuccess)
            {
                Navigation.NavigateTo("/payments");
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
