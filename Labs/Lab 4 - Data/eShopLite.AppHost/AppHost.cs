var builder = DistributedApplication.CreateBuilder(args);

var redis = builder.AddRedis("redis");

var productsdb = builder.AddPostgres("pg")
                            .WithPgAdmin()
                            .AddDatabase("productsdb");

var products = builder.AddProject<Projects.Products>("products");

builder.AddProject<Projects.Store>("store")
       .WithExternalHttpEndpoints()
       .WithReference(products)
       .WithReference(redis)
       .WithReference(productsdb)
       .WaitFor(products)
       .WaitFor(redis)
       .WaitFor(productsdb);
 
builder.Build().Run();
