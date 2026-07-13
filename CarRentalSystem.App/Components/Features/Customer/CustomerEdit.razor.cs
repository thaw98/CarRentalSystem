using CarRentalSystem.App.Services;
using CarRentalSystem.Domain.Features.Customer.Models;
using Microsoft.AspNetCore.Components;

namespace CarRentalSystem.App.Components.Features.Customer;

public partial class CustomerEdit
{
    [Inject]
    private ApiService ApiService { get; set; } = default!;

    [Inject]
    private NavigationManager Navigation { get; set; } = default!;

    [Parameter]
    public int Id { get; set; }

    private CustomerUpdateRequestModel model = new()
    {
        Name = string.Empty,
        Phone = string.Empty,
        LicenseNumber = string.Empty
    };

    private bool isLoading = true;
    private bool isSaving;
    private string error = string.Empty;

    protected override async Task OnInitializedAsync()
    {
        try
        {
            var result = await ApiService.GetCustomer(Id);

            if (result.IsSuccess && result.Data is not null)
            {
                model = new CustomerUpdateRequestModel
                {
                    Name = result.Data.Name,
                    Phone = result.Data.Phone,
                    Email = result.Data.Email,
                    Nrc = result.Data.Nrc,
                    Address = result.Data.Address,
                    LicenseNumber = result.Data.LicenseNumber
                };
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

        if (string.IsNullOrWhiteSpace(model.Name) ||
            string.IsNullOrWhiteSpace(model.Phone) ||
            string.IsNullOrWhiteSpace(model.LicenseNumber))
        {
            error = "Name, phone and license number are required.";
            return;
        }

        isSaving = true;

        try
        {
            var result = await ApiService.UpdateCustomer(Id, model);

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
