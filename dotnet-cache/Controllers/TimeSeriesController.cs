using dotnetCache.Service;
using Microsoft.AspNetCore.Mvc;

namespace dotnetCache.Controllers;


[ApiController]
[Route("[controller]")]
public class TimeSeriesController(ITimeSeriesService timeSeriesService) : ControllerBase
{
    private readonly ITimeSeriesService _timeSeriesService = timeSeriesService;

    [HttpGet]
    public IActionResult GetProduct() => Ok(_timeSeriesService.GetTimeSeries());


}
