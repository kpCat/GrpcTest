using GrpcServer;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddServiceModelGrpc();

var app = builder.Build();
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    // bind Calculator service
    endpoints.MapGrpcService<TestDao>();
});
app.MapGet("/", () => "Hello World!");

app.Run();