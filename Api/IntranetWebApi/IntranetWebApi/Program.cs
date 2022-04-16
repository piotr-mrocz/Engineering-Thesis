using FluentValidation.AspNetCore;
using IntranetWebApi.Application;
using IntranetWebApi.Data;
using IntranetWebApi.Middleware;
using IntranetWebApi.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

#region Add services to the container
builder.Services.AddControllers().AddFluentValidation();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo { Title = "Intranet API", Version = "v1" }));
builder.Services.AddApplicationLayer();
#endregion Add services to the container

#region Database
builder.Services.AddDbContext<IntranetDbContext>(x =>
    x.UseSqlServer(builder.Configuration.GetConnectionString(nameof(ConnectionString.JDPIntranet))));
#endregion Database

var app = builder.Build();

#region Swagger
app.UseSwaggerUI(c =>
{
    // run automatically swagger when run project
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Intranet API v1");
    c.RoutePrefix = "";
});
app.UseSwagger(x => x.SerializeAsV2 = true);
#endregion Swagger

#region Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
#endregion Configure the HTTP request pipeline.

app.UseHttpsRedirection();
app.MapControllers();
app.UseMiddleware<ErrorHandlerMiddleware>();
app.Run();
