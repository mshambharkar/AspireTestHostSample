using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var appa=builder.AddProject<WebAppA>(nameof(WebAppA).ToLower());
builder.AddProject<WebAppB>(nameof(WebAppB)).WithExternalHttpEndpoints().WithReference(appa);


builder.Build().Run();
