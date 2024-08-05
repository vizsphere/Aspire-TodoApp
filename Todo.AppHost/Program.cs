var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedisContainer("cache");


var apiService = builder.AddProject<Projects.TodoApi>("todo-api")
                 .WithReference(cache);

builder.AddProject<Projects.TodoWeb>("todo-web")
        .WithReference(cache)
        .WithReference(apiService);

builder.Build().Run();
