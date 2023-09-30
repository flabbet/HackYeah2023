namespace GWIZDBackend;

public class AnimalReportService
{
    private readonly MongoDBService mongoService;
    public AnimalReportService(MongoDBService mongoDbService)
    {
        mongoService = mongoDbService;
    }

    public async Task SendAnimalReport(AnimalReport animalReport)
    {
        await mongoService.CreateReportAsync(animalReport);
    }

    public async Task<object?> GetAllReports()
    {
        return await mongoService.GetAsync();
    }
}