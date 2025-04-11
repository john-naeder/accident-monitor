var builder = DistributedApplication.CreateBuilder(args);

var sqlServer = builder.AddSqlServer("sqlServer");

var accidentsRecordDb = sqlServer.AddDatabase("AccidentMonitorDb");


builder.AddProject<Projects.AccidentMonitor_WebApi>("accident-monitor-api")
    .WithReference(accidentsRecordDb)
    .WaitFor(accidentsRecordDb);

builder.Build().Run();
