using CarRentalSystem.Domain.Features.Car;
using CarRentalSystem.Domain.Features.Car.Models;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace CarRentalSystem.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CarController : BaseController
{
    private readonly CarService _carService;

    public CarController(CarService carService)
    {
        _carService = carService;
    }

    [HttpGet]
    public IActionResult GetList([FromQuery] CarListRequestModel request)
    {
        var result = _carService.GetList(request);
        return Execute(result);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var result = _carService.GetById(id);
        return Execute(result);
    }

    [HttpPost]
    public IActionResult Create(CarCreateRequestModel request)
    {
        var result = _carService.Create(request);
        return Execute(result);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, CarUpdateRequestModel request)
    {
        var result = _carService.Update(id, request);
        return Execute(result);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var result = _carService.Delete(id);
        return Execute(result);
    }
}