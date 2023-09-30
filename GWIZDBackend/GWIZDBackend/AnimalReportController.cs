using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace GWIZDBackend;

[Controller]
[Route("api/[controller]")]
public class AnimalReportController : Controller
{
    private AnimalReportService _reportService;

    public AnimalReportController(MongoDBService mongoDBService)
    {
        _reportService = new AnimalReportService(mongoDBService);
    }

    [HttpPut]
    [Route("animal-report")]
    public async Task<IActionResult> Post([FromBody] AnimalReport animalReport)
    {
        await _reportService.SendAnimalReport(animalReport);
        return Ok();
    }

    [HttpGet]
    [Route("get-all-reports")]
    public async Task<IActionResult> Get()
    {
        return Ok(await _reportService.GetAllReports());
    }
}