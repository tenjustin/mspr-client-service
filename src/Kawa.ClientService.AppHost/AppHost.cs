using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var pgDb = builder.AddPostgres("postgres")
    .WithDataVolume()
    .WithPgAdmin()
    .AddDatabase("clientServiceDb");

var api = builder.AddProject<Kawa_ClientService_Api>("api")
    .WithReference(pgDb)
    .WaitFor(pgDb);

builder.Build().Run();