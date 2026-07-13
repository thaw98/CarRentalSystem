using Microsoft.AspNetCore.Mvc;
using CarRentalSystem.Domain.Features.Customer;
using CarRentalSystem.Domain.Features.Customer.Models;

namespace CarRentalSystem.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomerController : BaseController
{
    private readonly CustomerService _customerService;

    public CustomerController(CustomerService customerService)
    {
        _customerService = customerService;
    }

    [HttpGet]
    public IActionResult GetList([FromQuery] CustomerListRequestModel request)
    {
        var result = _customerService.GetList(request);
        return Execute(result);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var result = _customerService.GetById(id);
        return Execute(result);
    }

    [HttpPost]
    public IActionResult Create(CustomerCreateRequestModel request)
    {
        var result = _customerService.Create(request);
        return Execute(result);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, CustomerUpdateRequestModel request)
    {
        var result = _customerService.Update(id, request);
        return Execute(result);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var result = _customerService.Delete(id);
        return Execute(result);
    }
}