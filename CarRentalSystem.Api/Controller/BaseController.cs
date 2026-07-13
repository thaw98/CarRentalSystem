using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using CarRentalSystem.Domain;

namespace CarRentalSystem.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BaseController : ControllerBase
{
    [NonAction]
    public IActionResult Execute(object data)
    {
        string json = JsonConvert.SerializeObject(data);
        Result<object> result = JsonConvert.DeserializeObject<Result<object>>(json)!;

        if (result.IsSuccess)
        {
            return Ok(data);
        }

        return StatusCode(400, data);
    }
}