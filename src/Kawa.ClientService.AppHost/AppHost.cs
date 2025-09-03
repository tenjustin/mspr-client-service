using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var pgDb = builder.AddPostgres("postgres")
    .WithDataVolume()
    .WithPgAdmin()
    .AddDatabase("clientServiceDb");

var username = builder.AddParameter("username", "username");
var password = builder.AddParameter("password", "password");

var rabbitmq = builder.AddRabbitMQ("messaging", username, password)
                      .WithManagementPlugin();

var migration = builder.AddProject<Kawa_ClientService_MigrationService>("migration")
    .WithReference(pgDb)
    .WaitFor(pgDb);

var api = builder.AddProject<Kawa_ClientService_Api>("api")
    .WithReference(migration)
    .WaitFor(migration);

builder.Build().Run();