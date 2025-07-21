var builder = DistributedApplication.CreateBuilder(args);

var products = builder.AddProject<Projects.Products>("products");

var redis = builder.AddRedis("redis");

builder.AddProject<Projects.Store>("store")
       .WithReference(products)
       .WithReference(redis)
       .WaitFor(products)
       .WaitFor(redis);
    
builder.Build().Run();
