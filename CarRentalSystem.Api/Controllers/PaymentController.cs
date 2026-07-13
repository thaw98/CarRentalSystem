using Microsoft.AspNetCore.Mvc;
using CarRentalSystem.Domain.Features.Payment;
using CarRentalSystem.Domain.Features.Payment.Models;

namespace CarRentalSystem.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PaymentController : BaseController
{
    private readonly PaymentService _paymentService;

    public PaymentController(PaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    [HttpGet]
    public IActionResult GetList([FromQuery] PaymentListRequestModel request)
    {
        var result = _paymentService.GetList(request);
        return Execute(result);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var result = _paymentService.GetById(id);
        return Execute(result);
    }

    [HttpPost]
    public IActionResult Create(PaymentCreateRequestModel request)
    {
        var result = _paymentService.Create(request);
        return Execute(result);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, PaymentUpdateRequestModel request)
    {
        var result = _paymentService.Update(id, request);
        return Execute(result);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var result = _paymentService.Delete(id);
        return Execute(result);
    }
}