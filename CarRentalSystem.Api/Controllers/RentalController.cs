using Microsoft.AspNetCore.Mvc;
using CarRentalSystem.Domain.Features.Rental;
using CarRentalSystem.Domain.Features.Rental.Models;

namespace CarRentalSystem.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RentalController : BaseController
{
    private readonly RentalService _rentalService;

    public RentalController(RentalService rentalService)
    {
        _rentalService = rentalService;
    }

    [HttpGet]
    public IActionResult GetList([FromQuery] RentalListRequestModel request)
    {
        var result = _rentalService.GetList(request);
        return Execute(result);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var result = _rentalService.GetById(id);
        return Execute(result);
    }

    [HttpPost]
    public IActionResult Create(RentalCreateRequestModel request)
    {
        var result = _rentalService.Create(request);
        return Execute(result);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, RentalUpdateRequestModel request)
    {
        var result = _rentalService.Update(id, request);
        return Execute(result);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var result = _rentalService.Delete(id);
        return Execute(result);
    }
}