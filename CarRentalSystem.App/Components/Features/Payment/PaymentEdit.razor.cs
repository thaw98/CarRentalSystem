using CarRentalSystem.App.Services;
using CarRentalSystem.Domain.Features.Payment.Models;
using CarRentalSystem.Domain.Features.Rental.Models;
using Microsoft.AspNetCore.Components;

namespace CarRentalSystem.App.Components.Features.Payment;

public partial class PaymentEdit
{
    [Inject]
    private ApiService ApiService { get; set; } = default!;

    [Inject]
    private NavigationManager Navigation { get; set; } = default!;

    [Parameter]
    public int Id { get; set; }

    private List<RentalModel> rentals = [];

    private PaymentUpdateRequestModel model = new()
    {
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
            var rentalsResult = await ApiService.GetRentals(
                new RentalListRequestModel
                {
                    PageNo = 1,
                    PageSize = 1000
                });

            var paymentResult = await ApiService.GetPayment(Id);

            if (!rentalsResult.IsSuccess)
            {
                error = rentalsResult.Message;
                return;
            }

            if (!paymentResult.IsSuccess ||
                paymentResult.Data is null)
            {
                error = paymentResult.Message;
                return;
            }

            rentals = rentalsResult.Data?.Rentals ?? [];

            model = new PaymentUpdateRequestModel
            {
                RentalId = paymentResult.Data.RentalId,
                PaymentDate = paymentResult.Data.PaymentDate,
                Amount = paymentResult.Data.Amount,
                PaymentMethod = paymentResult.Data.PaymentMethod,
                PaymentStatus = paymentResult.Data.PaymentStatus
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
            var result = await ApiService.UpdatePayment(Id, model);

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
