using OnlineStore.Application;
using OnlineStore.Infrastructure;
using OnlineStore.Api.Filters;

WebApplicationOptions webApplicationOptions = new () { Args = args };
WebApplicationBuilder builder = WebApplication.CreateBuilder(webApplicationOptions);

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration);
//builder.Services.AddControllers(options => options.Filters.Add<ExceptionHandlingFilterAttribute>());
builder.Services.AddControllers();
WebApplication app = builder.Build();

//app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseExceptionHandler("/error");
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