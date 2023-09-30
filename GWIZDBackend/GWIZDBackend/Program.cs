using GWIZDBackend;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSwaggerGen((g) =>
{
    g.SwaggerDoc("v1", new OpenApiInfo { Title = "AddingStuffAndCheckingI", Version = "v1" });
});

builder.Services.Configure<MongoDBSettings>(builder.Configuration.GetSection("MongoDB"));
builder.Services.AddSingleton<MongoDBService>();

builder.Services.AddControllers();
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.UseSwagger(new SwaggerOptions());
app.UseSwaggerUI();

app.MapControllers();
app.Run();