var builder = DistributedApplication.CreateBuilder(args);

//var sql = builder.AddSqlServer("sql");

//var database = sql.AddDatabase("AccidentMonitoringDb");

builder.AddProject<Projects.Web>("web");
    //.WithReference(database)
    //.WaitFor(database);

builder.Build().Run();
