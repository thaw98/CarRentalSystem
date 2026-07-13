using CarRentalSystem.App.Services;
using CarRentalSystem.Domain.Features.Customer.Models;
using Microsoft.AspNetCore.Components;

namespace CarRentalSystem.App.Components.Features.Customer;

public partial class CustomerList
{
    [Inject]
    private ApiService ApiService { get; set; } = default!;

    private List<CustomerModel> customers = [];
    private bool isLoading = true;
    private string error = string.Empty;

    protected override async Task OnInitializedAsync()
    {
        try
        {
            var result = await ApiService.GetCustomers(
                new CustomerListRequestModel
                {
                    PageNo = 1,
                    PageSize = 100
                });

            if (result.IsSuccess)
            {
                customers = result.Data?.Customers ?? [];
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
}
