using OnlineStore.Application;
using OnlineStore.Infrastructure;

WebApplicationOptions webApplicationOptions = new () { Args = args };
WebApplicationBuilder builder = WebApplication.CreateBuilder(webApplicationOptions);

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration);
builder.Services.AddControllers();

WebApplication app = builder.Build();

app.MapControllers();
app.UseHttpsRedirection();
app.UseCors((op) =>
{
    op.WithHeaders().AllowAnyHeader();
    op.WithMethods().AllowAnyMethod();
    op.WithOrigins().AllowAnyOrigin();
});

app.MapGet("/", () =>
{
    return "HELLO WORLD";
});

app.Run();