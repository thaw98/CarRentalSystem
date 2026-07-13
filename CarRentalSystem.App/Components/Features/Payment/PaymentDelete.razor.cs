using CarRentalSystem.App.Services;
using CarRentalSystem.Domain.Features.Payment.Models;
using Microsoft.AspNetCore.Components;

namespace CarRentalSystem.App.Components.Features.Payment;

public partial class PaymentDelete
{
    [Inject]
    private ApiService ApiService { get; set; } = default!;

    [Inject]
    private NavigationManager Navigation { get; set; } = default!;

    [Parameter]
    public int Id { get; set; }

    private PaymentModel? payment;
    private bool isLoading = true;
    private bool isDeleting;
    private string error = string.Empty;

    protected override async Task OnInitializedAsync()
    {
        try
        {
            var result = await ApiService.GetPayment(Id);

            if (result.IsSuccess)
            {
                payment = result.Data;
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
            var result = await ApiService.DeletePayment(Id);

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
            isDeleting = false;
        }
    }
}
