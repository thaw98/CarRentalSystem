using CarRentalSystem.App.Services;
using CarRentalSystem.Domain.Features.Customer.Models;
using Microsoft.AspNetCore.Components;

namespace CarRentalSystem.App.Components.Features.Customer;

public partial class CustomerCreate
{
    [Inject]
    private ApiService ApiService { get; set; } = default!;

    [Inject]
    private NavigationManager Navigation { get; set; } = default!;

    private CustomerCreateRequestModel model = new()
    {
        Name = string.Empty,
        Phone = string.Empty,
        LicenseNumber = string.Empty
    };

    private bool isSaving;
    private string error = string.Empty;

    private async Task Save()
    {
        error = string.Empty;

        if (string.IsNullOrWhiteSpace(model.Name))
        {
            error = "Customer name is required.";
            return;
        }

        if (string.IsNullOrWhiteSpace(model.Phone))
        {
            error = "Phone number is required.";
            return;
        }

        if (string.IsNullOrWhiteSpace(model.LicenseNumber))
        {
            error = "License number is required.";
            return;
        }

        isSaving = true;

        try
        {
            var result = await ApiService.CreateCustomer(model);

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
            isSaving = false;
        }
    }
}
