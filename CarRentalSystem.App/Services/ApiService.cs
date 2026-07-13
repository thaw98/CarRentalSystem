using System.Net.Http.Json;
using System.Text.Json;
using CarRentalSystem.Domain;
using CarRentalSystem.Domain.Features.Car.Models;
using CarRentalSystem.Domain.Features.Customer.Models;
using CarRentalSystem.Domain.Features.Payment.Models;
using CarRentalSystem.Domain.Features.Rental.Models;

namespace CarRentalSystem.App.Services;

public class ApiService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly string _baseUrl;

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public ApiService(
        IHttpClientFactory httpClientFactory,
        IConfiguration configuration)
    {
        _httpClientFactory = httpClientFactory;

        _baseUrl = configuration["BackendUrl"]
            ?? throw new InvalidOperationException(
                "BackendUrl is missing from appsettings.json.");
    }

    private HttpClient CreateClient()
    {
        var client = _httpClientFactory.CreateClient();

        client.BaseAddress = new Uri(
            $"{_baseUrl.TrimEnd('/')}/");

        return client;
    }

    private static async Task<Result<T>> ReadResult<T>(
        HttpResponseMessage response)
    {
        var result = await response.Content
            .ReadFromJsonAsync<Result<T>>(JsonOptions);

        return result ?? new Result<T>
        {
            IsSuccess = false,
            Message = "The API returned an empty response."
        };
    }

    // Customer

    public async Task<Result<CustomerListResponseModel>> GetCustomers(
        CustomerListRequestModel request)
    {
        var client = CreateClient();

        var response = await client.GetAsync(
            $"{ApiEndpoints.Customers}?pageNo={request.PageNo}&pageSize={request.PageSize}");

        return await ReadResult<CustomerListResponseModel>(response);
    }

    public async Task<Result<CustomerModel>> GetCustomer(int id)
    {
        var client = CreateClient();

        var response = await client.GetAsync(
            $"{ApiEndpoints.Customers}/{id}");

        return await ReadResult<CustomerModel>(response);
    }

    public async Task<Result<CustomerModel>> CreateCustomer(
        CustomerCreateRequestModel request)
    {
        var client = CreateClient();

        var response = await client.PostAsJsonAsync(
            ApiEndpoints.Customers,
            request);

        return await ReadResult<CustomerModel>(response);
    }

    public async Task<Result<CustomerModel>> UpdateCustomer(
        int id,
        CustomerUpdateRequestModel request)
    {
        var client = CreateClient();

        var response = await client.PutAsJsonAsync(
            $"{ApiEndpoints.Customers}/{id}",
            request);

        return await ReadResult<CustomerModel>(response);
    }

    public async Task<Result<CustomerModel>> DeleteCustomer(int id)
    {
        var client = CreateClient();

        var response = await client.DeleteAsync(
            $"{ApiEndpoints.Customers}/{id}");

        return await ReadResult<CustomerModel>(response);
    }

    // Car

    public async Task<Result<CarListResponseModel>> GetCars(
        CarListRequestModel request)
    {
        var client = CreateClient();

        var response = await client.GetAsync(
            $"{ApiEndpoints.Cars}?pageNo={request.PageNo}&pageSize={request.PageSize}");

        return await ReadResult<CarListResponseModel>(response);
    }

    public async Task<Result<CarModel>> GetCar(int id)
    {
        var client = CreateClient();

        var response = await client.GetAsync(
            $"{ApiEndpoints.Cars}/{id}");

        return await ReadResult<CarModel>(response);
    }

    public async Task<Result<CarModel>> CreateCar(
        CarCreateRequestModel request)
    {
        var client = CreateClient();

        var response = await client.PostAsJsonAsync(
            ApiEndpoints.Cars,
            request);

        return await ReadResult<CarModel>(response);
    }

    public async Task<Result<CarModel>> UpdateCar(
        int id,
        CarUpdateRequestModel request)
    {
        var client = CreateClient();

        var response = await client.PutAsJsonAsync(
            $"{ApiEndpoints.Cars}/{id}",
            request);

        return await ReadResult<CarModel>(response);
    }

    public async Task<Result<CarModel>> DeleteCar(int id)
    {
        var client = CreateClient();

        var response = await client.DeleteAsync(
            $"{ApiEndpoints.Cars}/{id}");

        return await ReadResult<CarModel>(response);
    }

    // Rental

    public async Task<Result<RentalListResponseModel>> GetRentals(
        RentalListRequestModel request)
    {
        var client = CreateClient();

        var response = await client.GetAsync(
            $"{ApiEndpoints.Rentals}?pageNo={request.PageNo}&pageSize={request.PageSize}");

        return await ReadResult<RentalListResponseModel>(response);
    }

    public async Task<Result<RentalModel>> GetRental(int id)
    {
        var client = CreateClient();

        var response = await client.GetAsync(
            $"{ApiEndpoints.Rentals}/{id}");

        return await ReadResult<RentalModel>(response);
    }

    public async Task<Result<RentalModel>> CreateRental(
        RentalCreateRequestModel request)
    {
        var client = CreateClient();

        var response = await client.PostAsJsonAsync(
            ApiEndpoints.Rentals,
            request);

        return await ReadResult<RentalModel>(response);
    }

    public async Task<Result<RentalModel>> UpdateRental(
        int id,
        RentalUpdateRequestModel request)
    {
        var client = CreateClient();

        var response = await client.PutAsJsonAsync(
            $"{ApiEndpoints.Rentals}/{id}",
            request);

        return await ReadResult<RentalModel>(response);
    }

    public async Task<Result<RentalModel>> DeleteRental(int id)
    {
        var client = CreateClient();

        var response = await client.DeleteAsync(
            $"{ApiEndpoints.Rentals}/{id}");

        return await ReadResult<RentalModel>(response);
    }

    // Payment

    public async Task<Result<PaymentListResponseModel>> GetPayments(
        PaymentListRequestModel request)
    {
        var client = CreateClient();

        var response = await client.GetAsync(
            $"{ApiEndpoints.Payments}?pageNo={request.PageNo}&pageSize={request.PageSize}");

        return await ReadResult<PaymentListResponseModel>(response);
    }

    public async Task<Result<PaymentModel>> GetPayment(int id)
    {
        var client = CreateClient();

        var response = await client.GetAsync(
            $"{ApiEndpoints.Payments}/{id}");

        return await ReadResult<PaymentModel>(response);
    }

    public async Task<Result<PaymentModel>> CreatePayment(
        PaymentCreateRequestModel request)
    {
        var client = CreateClient();

        var response = await client.PostAsJsonAsync(
            ApiEndpoints.Payments,
            request);

        return await ReadResult<PaymentModel>(response);
    }

    public async Task<Result<PaymentModel>> UpdatePayment(
        int id,
        PaymentUpdateRequestModel request)
    {
        var client = CreateClient();

        var response = await client.PutAsJsonAsync(
            $"{ApiEndpoints.Payments}/{id}",
            request);

        return await ReadResult<PaymentModel>(response);
    }

    public async Task<Result<PaymentModel>> DeletePayment(int id)
    {
        var client = CreateClient();

        var response = await client.DeleteAsync(
            $"{ApiEndpoints.Payments}/{id}");

        return await ReadResult<PaymentModel>(response);
    }
}

public static class ApiEndpoints
{
    public const string Customers = "api/customer";
    public const string Cars = "api/car";
    public const string Rentals = "api/rental";
    public const string Payments = "api/payment";
}