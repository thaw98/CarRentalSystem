using CarRentalSystem.App.Services;
using CarRentalSystem.Domain.Features.Payment.Models;
using Microsoft.AspNetCore.Components;

namespace CarRentalSystem.App.Components.Features.Payment;

public partial class PaymentList
{
    [Inject]
    private ApiService ApiService { get; set; } = default!;

    private List<PaymentModel> payments = [];
    private bool isLoading = true;
    private string error = string.Empty;

    protected override async Task OnInitializedAsync()
    {
        try
        {
            var result = await ApiService.GetPayments(
                new PaymentListRequestModel
                {
                    PageNo = 1,
                    PageSize = 100
                });

            if (result.IsSuccess)
            {
                payments = result.Data?.Payments ?? [];
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
        return status == "Paid"
            ? "badge bg-success"
            : "badge bg-warning text-dark";
    }
}
