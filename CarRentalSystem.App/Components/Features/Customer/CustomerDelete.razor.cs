using CarRentalSystem.App.Services;
using CarRentalSystem.Domain.Features.Customer.Models;
using Microsoft.AspNetCore.Components;

namespace CarRentalSystem.App.Components.Features.Customer;

public partial class CustomerDelete
{
    [Inject]
    private ApiService ApiService { get; set; } = default!;

    [Inject]
    private NavigationManager Navigation { get; set; } = default!;

    [Parameter]
    public int Id { get; set; }

    private CustomerModel? customer;
    private bool isLoading = true;
    private bool isDeleting;
    private string error = string.Empty;

    protected override async Task OnInitializedAsync()
    {
        try
        {
            var result = await ApiService.GetCustomer(Id);

            if (result.IsSuccess)
            {
                customer = result.Data;
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
            var result = await ApiService.DeleteCustomer(Id);

            if (result.IsSuccess)
            {
                Navigation.NavigateTo("/customers");
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
